## MTSCRA_Bindings.Android
#### Easy to use bindings

## Getting started
To use the bindings, you need to:

  - **Install the [nuget package here](https://www.nuget.org/packages/Xamarin.Bindings.MagTek.Android/)**.

  - **Wire up MTSCRA in your Android project**:
  I created a new class, which implements a service so that I then use [Dependency Injection](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/dependency-service/introduction/) 
  to use in my Xamarmin.Forms project.

**Add using**
```csharp
using MTSCRA_Bindings.Android;
```
**MainActivity.cs**
```csharp
 protected override void OnCreate(Bundle bundle)
{
     ...
     global::Xamarin.Forms.Forms.Init(this, bundle);
     
     MagTekApi.Init();
}
```

**MTSCRAService_Android.cs**
```csharp
public static class MagTekApi
{
    private static Android.Content.Context _context = Android.App.Application.Context;
    internal readonly static MTSCRA_delegates_droid MTSCRA_delegates = new MTSCRA_delegates_droid();

    public static MTSCRA MTSCRA;
    public static void Init()
    {
        MTSCRA = new MTSCRA(_context, new Handler(MTSCRA_delegates));
    }
}


public class MTSCRAService_Android : IMTSCRAService
{
    private bool isScanning;
    private const long SCAN_PERIOD = 10000;
    private MyLeScanCallBack _myLeCallBack;    
    private readonly MTSCRA _cardReader;
    private readonly BluetoothAdapter _bluetoothAdapter;
    private static Android.Content.Context _context = Android.App.Application.Context;

    public MTSCRAService_Android()
    {
		_cardReader = MagTekApi.MTSCRA;        
        _cardReader.SetConnectionRetry(true);

		// Not sure the bluetooth scanning is implemented correcly, but it works
        _myLeCallBack = new MyLeScanCallBack();
        BluetoothManager bluetoothManager = ((BluetoothManager)_context.GetSystemService(Context.BluetoothService));
        _bluetoothAdapter = bluetoothManager.Adapter;
        if ((_bluetoothAdapter == null))
        {
            Toast.MakeText(_context, 0, ToastLength.Short).Show();
        }
        _bluetoothAdapter.Enable();
	
		MagTekApi.MTSCRA_delegates.OnDataReceivedDelegate += MTSCRA_delegates_OnDataReceivedDelegate;
    }
    
	// IMTSCRAService defines this method
	public void StartScanningForPeripherals()
    {
        // cannot scan if bluetooth is not enabled
        if (!_bluetoothAdapter.IsEnabled)
        {
            return;
        }

        if (isScanning)
        {
            _bluetoothAdapter.StopLeScan(_myLeCallBack);
            isScanning = false;
        }


        //
        isScanning = true;
        _bluetoothAdapter.StartLeScan(_myLeCallBack);
    }

	// IMTSCRAService defines this method
	public void StopScanningForPeripherals()
        {

            if (isScanning)
            {
                _bluetoothAdapter.StopLeScan(_myLeCallBack);
                isScanning = false;
            }
        }

    private void MTSCRA_delegates_OnDataReceivedDelegate(Com.Magtek.Mobile.Android.Mtlib.IMTCardData cardDataObj)
    {
        OnDataReceivedDelegate?.Invoke(getCardData(cardDataObj), null);
    }

	private YOUR_PCL.Interfaces.IMTCardData getCardData(Com.Magtek.Mobile.Android.Mtlib.IMTCardData cardDataObj)
    {
        string strYear = null, strMonth = null;

        try
        {
            if (cardDataObj.CardExpDate != null && cardDataObj.CardExpDate.Length == 4)
            {
                strYear = cardDataObj.CardExpDate.Substring(0,2);
                strMonth = cardDataObj.CardExpDate.Substring(2, 2);
            }
        }
        catch (Exception)
        {
        }

        return new MagTekCardData()
        {
            AdditionalInfoTrack1 = null,
            AdditionalInfoTrack2 = null,
            BatteryLevel = (int)cardDataObj.BatteryLevel,
            CapMagStripeEncryption = cardDataObj.CapMagStripeEncryption,
            CapMSR = cardDataObj.CapMSR,
            CapTracks = cardDataObj.CapTracks,
            CardData = null,
            CardExpDate = cardDataObj.CardExpDate,
            CardExpDateMonth = strMonth,
            CardExpDateYear = strYear,
            CardFirstName = null,
            CardIIN = cardDataObj.CardIIN,
            CardLast4 = cardDataObj.CardLast4,
            CardLastName = null,
            CardMiddleName = null,
            CardName = cardDataObj.CardName,
            CardPAN = cardDataObj.CardPAN,
            CardPANLength = (int)cardDataObj.CardPANLength,
            CardServiceCode = cardDataObj.CardServiceCode,
            CardStatus = cardDataObj.CardStatus,
            CardType = 0,
            DeviceCaps = "",
            DeviceFirmware = cardDataObj.Firmware,
            DeviceKSN = cardDataObj.KSN,
            DeviceName = cardDataObj.DeviceName,
            DevicePartNumber = "",
            DeviceSerialNumber = cardDataObj.DeviceSerial,
            DeviceSerialNumberMagTek = cardDataObj.MagTekDeviceSerial,
            DeviceStatus = "",
            EncrypedSessionID = cardDataObj.SessionID,
            EncryptedMagneprint = cardDataObj.MagnePrint,
            EncryptedTrack1 = cardDataObj.Track1,
            EncryptedTrack2 = cardDataObj.Track2,
            EncryptedTrack3 = cardDataObj.Track3,
            EncryptionStatus = cardDataObj.EncryptionStatus,
            Firmware = cardDataObj.Firmware,
            MagnePrintLength = cardDataObj.MagnePrint?.Length ?? 0,
            MagneprintStatus = cardDataObj.MagnePrintStatus,
            MaskedPAN = "",
            MaskedTrack1 = cardDataObj.Track1Masked,
            MaskedTrack2 = cardDataObj.Track2Masked,
            MaskedTrack3 = cardDataObj.Track3Masked,
            MaskedTracks = cardDataObj.MaskedTracks,
            ResponseData = "",
            ResponseType = cardDataObj.ResponseType,
            SwipeCount = (int)cardDataObj.SwipeCount,
            TagValue = cardDataObj.GetTagValue("", ""),
            TlvVersion = cardDataObj.TLVVersion,
            Track1DecodeStatus = "",
            Track2DecodeStatus = "",
            Track3DecodeStatus = "",
            TrackDecodeStatus = cardDataObj.TrackDecodeStatus
        };
    }

	class MyLeScanCallBack : Java.Lang.Object, ILeScanCallback
    {
        public List<IMagTekDevice> DevicesFound { get; set; }

        public MyLeScanCallBack()
        {
            DevicesFound = new List<IMagTekDevice>();
        }

        public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
        {
            if (scanRecord != null && !string.IsNullOrWhiteSpace(device.Name))
            {
                DevicesFound.Add(new MagTekDevice()
                {
                    Address = device.Address,
                    DeviceType = device.Type != BluetoothDeviceType.Unknown ? YOUR_PCL.Enums.MTDeviceType.MAGTEKEDYNAMO : YOUR_PCL.Enums.MTDeviceType.MAGTEKNONE,
                    Id = device.Address,
                    Name = device.Name,
                    State = device.BondState == Bond.Bonded ? YOUR_PCL.Enums.MTConnectionState.Connected : YOUR_PCL.Enums.MTConnectionState.Disconnected
                });
            }
        }
    }
}
```

**Wire up MTSCRA events for swiping, connecting, ect..**
```csharp
public delegate void OnCardDataStateChangedDelegate(MTCardDataState cardState);
public delegate void OnDataReceivedDelegate(IMTCardData cardDataObj);
public delegate void OnDeviceConnectionDidChangeDelegate(MTConnectionState connectionState);
public delegate void OnDeviceResponseDelegate(string response);


public class MTSCRA_delegates_droid : Com.Magtek.Mobile.Android.Mtlib.MTSCRAEvent, ICallback
{
    public event OnCardDataStateChangedDelegate OnCardDataStateChangedDelegate;
    public event OnDataReceivedDelegate OnDataReceivedDelegate;
    public event OnDeviceConnectionDidChangeDelegate OnDeviceConnectionDidChangeDelegate;
    public event OnDeviceResponseDelegate OnDeviceResponseDelegate;
    public event OnBleReaderDidDiscoverPeripheralDelegate OnBleReaderDidDiscoverPeripheralDelegate;

    public bool HandleMessage(Message msg)
    {
        switch (msg.What)
        {
            case OnCardDataStateChanged:
                OnCardDataStateChangedDelegate?.Invoke((MTCardDataState)msg.Obj);
                break;
            case OnDataReceived:
                OnDataReceivedDelegate?.Invoke((IMTCardData)msg.Obj);
                break;
            case OnDeviceConnectionStateChanged:
                OnDeviceConnectionDidChangeDelegate?.Invoke((MTConnectionState)msg.Obj);
                break;
            case OnDeviceResponse:
                OnDeviceResponseDelegate?.Invoke(msg.Obj.ToString());
                break;
        }
        return true;
    }
}
```
**Xamarin.Forms project**
```csharp
public class YourPage : ContentPage
{
     private IMTSCRAService _cardReaderService = DependencyService.Get<IMTSCRAService>();
     
     public YourPage()
     {
          WireUpMagTekEvents();
     }
     
     private WireUpMagTekEvents()
     {     	 
          _cardReaderService.OnDataReceivedDelegate += _cardReaderService_OnDataReceivedDelegate;
     }
}
```
> **Note:** refer to MagTeks API documentation for connecting/disconnecting from device. I will have examples and more plug and play features coming soon!
