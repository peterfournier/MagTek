using Android.Bluetooth;
using Android.OS;
using Android.Widget;
using static Android.Bluetooth.BluetoothAdapter;
using System.Collections.Generic;
using System;
using Com.Magtek.Mobile.Android.Mtlib;
using XFMagTek.Droid;
using System.Linq;
using XFMagTek.Droid.Custom;
using Xamarin.MagTek.Forms.Models;
using Xamarin.MagTek.Forms.Delegates;
using Android.Content;

[assembly: Xamarin.Forms.Dependency(typeof(MTSCRA_Service_droid))]
namespace XFMagTek.Droid
{
    public class MTSCRA_Service_droid : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IeDynamoService
    {
        private bool isScanning;
        private const long SCAN_PERIOD = 10000;
        private MyLeScanCallBack _myLeCallBack;
        //private MyBroadcastReceiver _myReceiver;
        private readonly MTSCRA _cardReader;
        private readonly BluetoothAdapter _bluetoothAdapter;
        private static Android.Content.Context _context = Android.App.Application.Context;        


        public event OnARQCReceivedDelegate OnARQCReceivedDelegate;
        public event OnBleReaderConnectedDelegate OnBleReaderConnectedDelegate;
        public event OnBleReaderDidDiscoverPeripheralDelegate OnBleReaderDidDiscoverPeripheralDelegate;
        public event OnBleReaderStateUpdatedDelegate OnBleReaderStateUpdatedDelegate;
        public event OnCardSwipeDidGetTransErrorDelegate OnCardSwipeDidGetTransErrorDelegate;
        public event OnCardSwipeDidStartDelegate OnCardSwipeDidStartDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDataReceivedDelegate OnDataReceivedDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDeviceConnectionDidChangeDelegate OnDeviceConnectionDidChangeDelegate;
        public event OnDeviceErrorDelegate OnDeviceErrorDelegate;
        public event OnDeviceExtendedResponseDelegate OnDeviceExtendedResponseDelegate;
        public event OnDeviceListDelegate OnDeviceListDelegate;
        public event OnDeviceNotPairedDelegate OnDeviceNotPairedDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDeviceResponseDelegate OnDeviceResponseDelegate;
        public event OnDisplayMessageRequestDelegate OnDisplayMessageRequestDelegate;
        public event OnEMVCommandResultDelegate OnEMVCommandResultDelegate;
        public event OnTransactionResultDelegate OnTransactionResultDelegate;
        public event OnTransactionStatusDelegate OnTransactionStatusDelegate;
        public event OnUserSelectionRequestDelegate OnUserSelectionRequestDelegate;
        public event OnBlueToothBondChangedDelegate OnBlueToothBondChangedDelegate;

        public MTSCRA_Service_droid()
        {
            _cardReader = MagTekApi.MTSCRA;
            _cardReader.SetConnectionRetry(true);
            _bluetoothAdapter = MagTekApi.BluetoothAdapter;
            _myLeCallBack = new MyLeScanCallBack();

            MagTekApi.MTSCRA_delegates.OnCardDataStateChangedDelegate += MTSCRA_delegates_OnCardDataStateChangedDelegate;
            MagTekApi.MTSCRA_delegates.OnDataReceivedDelegate += MTSCRA_delegates_OnDataReceivedDelegate;
            MagTekApi.MTSCRA_delegates.OnDeviceConnectionDidChangeDelegate += MTSCRA_delegates_OnDeviceConnectionDidChangeDelegate;
            MagTekApi.MTSCRA_delegates.OnDeviceResponseDelegate += MTSCRA_delegates_OnDeviceResponseDelegate;
            MagTekApi.MTSCRA_delegates.OnBleReaderDidDiscoverPeripheralDelegate += MTSCRA_delegates_OnBleReaderDidDiscoverPeripheralDelegate;
        }

        ~MTSCRA_Service_droid()
        {
            MagTekApi.MTSCRA_delegates.OnCardDataStateChangedDelegate -= MTSCRA_delegates_OnCardDataStateChangedDelegate;
            MagTekApi.MTSCRA_delegates.OnDataReceivedDelegate -= MTSCRA_delegates_OnDataReceivedDelegate;
            MagTekApi.MTSCRA_delegates.OnDeviceConnectionDidChangeDelegate -= MTSCRA_delegates_OnDeviceConnectionDidChangeDelegate;
            MagTekApi.MTSCRA_delegates.OnDeviceResponseDelegate -= MTSCRA_delegates_OnDeviceResponseDelegate;
            MagTekApi.MTSCRA_delegates.OnBleReaderDidDiscoverPeripheralDelegate -= MTSCRA_delegates_OnBleReaderDidDiscoverPeripheralDelegate;
        }

        public long BatteryLevel()
        {
            return _cardReader.BatteryLevel;
        }

        public int CancelTransaction()
        {
            return _cardReader.CancelTransaction();
        }

        public string CapMagStripeEncryption()
        {
            return _cardReader.CapMagStripeEncryption;
        }

        public string CapMSR()
        {
            return _cardReader.CapMSR;
        }

        public string CapTracks()
        {
            return _cardReader.CapTracks;
        }

        public string CardExpDate()
        {
            return _cardReader.CardExpDate;
        }

        public string CardIIN()
        {
            return _cardReader.CardIIN;
        }

        public string CardLast4()
        {
            return _cardReader.CardLast4;
        }

        public string CardName()
        {
            return _cardReader.CardName;
        }

        public string CardPAN()
        {
            return _cardReader.CardPAN;
        }

        public int CardPANLength()
        {
            return _cardReader.CardPANLength;
        }

        public string CardServiceCode()
        {
            return _cardReader.CardServiceCode;
        }

        public string CardStatus()
        {
            return _cardReader.CardStatus;
        }

        public void ClearBuffers()
        {
            _cardReader.ClearBuffers();
        }

        public bool CloseDevice()
        {
            _cardReader.CloseDevice();
            return true;
        }

        public long ConnectionType()
        {
            return 0; // N/A for droid
        }

        public string DeviceCaps()
        {
            return ""; // n/a
        }

        public string DeviceName()
        {
            return _cardReader.DeviceName;
        }

        public string DevicePartNumber()
        {
            return "";
        }

        public string DeviceSerial()
        {
            return _cardReader.DeviceSerial;
        }

        public string DeviceStatus()
        {
            return "";
        }

        public long DeviceType()
        {
            return 0;
        }

        public string EncryptionStatus()
        {
            return _cardReader.EncryptionStatus;
        }

        public string ExpDateMonth()
        {
            return "";
        }

        public string ExpDateYear()
        {
            return "";
        }

        public string Firmware()
        {
            return _cardReader.Firmware;
        }

        public object GetDeviceInformationDictionary()
        {
            return new object();
        }

        public ICollection<IDiscoveredDevice> GetDiscoveredPeripherals()
        {
            return _myLeCallBack.DevicesFound;
        }

        public string GetTagValue(int tag)
        {
            return _cardReader.GetTagValue(tag.ToString(), "");
        }

        public bool IsDeviceConnected()
        {
            return _cardReader.IsDeviceConnected;
        }

        public bool IsDeviceEMV()
        {
            return _cardReader.IsDeviceEMV;
        }

        public bool IsDeviceOpened()
        {
            return _cardReader.IsDeviceConnected;
        }

        public string KSN()
        {
            return _cardReader.KSN;
        }

        public void ListenForEvents(int @event)
        {

        }

        public string MagnePrint()
        {
            return _cardReader.MagnePrint;
        }

        public int MagnePrintLength()
        {
            return _cardReader.MagnePrint?.Length ?? 0;
        }

        public string MagnePrintStatus()
        {
            return _cardReader.MagnePrintStatus;
        }

        public string MagTekDeviceSerial()
        {
            return _cardReader.MagTekDeviceSerial;
        }

        public string MaskedTracks()
        {
            return _cardReader.MaskedTracks;
        }

        public bool OpenDevice()
        {
            try
            {
                if (!_cardReader.IsDeviceConnected)
                {
                    //_bluetoothAdapter.GetRemoteDevice(_cardReader.add)
                    _cardReader.OpenDevice();
                }
            }
            catch (Exception ex)
            {

            }
            return _cardReader.IsDeviceConnected;
        }

        public string OperationStatus()
        {
            return "";
        }

        public int ProductID()
        {
            return 0;
        }

        public void RequestDeviceList(int type)
        {

        }

        public string ResponseData()
        {
            return _cardReader.ResponseData;
        }

        public string ResponseType()
        {
            return _cardReader.ResponseType;
        }

        public string SDKVersion()
        {
            return _cardReader.SDKVersion;
        }

        public int SendCommandToDevice(string pData)
        {
            return _cardReader.SendCommandToDevice(pData);
        }

        public int SendcommandWithLength(string command)
        {
            return 0;
        }

        public int SendExtendedCommand(string commandIn)
        {
            return _cardReader.SendExtendedCommand(commandIn);
        }

        public string SessionID()
        {
            return _cardReader.SessionID;
        }

        public int SetAcquirerResponse(byte response, int length)
        {
            byte[] array = new byte[1] { response };
            return _cardReader.SetAcquirerResponse(array);
        }

        public void CreateBond(string address)
        {
            _cardReader.SetAddress(address);

            var device = _bluetoothAdapter.GetRemoteDevice(address);

            IntentFilter filter = new IntentFilter(BluetoothDevice.ActionBondStateChanged);
            var myBroadCastReciever = new MyBroadCastReciever();
            myBroadCastReciever.BondChanged += OnBlueToothBondChangedDelegate_eventHandler;
            Android.App.Application.Context.RegisterReceiver(myBroadCastReciever, filter);

            device.CreateBond();
        }

        public void SetConfigurationParams(string pData)
        {
            _cardReader.SetDeviceConfiguration(pData);
        }

        public void SetConnectionType(int connectionType)
        {
            Xamarin.MagTek.Forms.Enums.ConnectionType mTConnectionType = (Xamarin.MagTek.Forms.Enums.ConnectionType)connectionType;

            switch (mTConnectionType)
            {
                case Xamarin.MagTek.Forms.Enums.ConnectionType.BLE:
                    _cardReader.SetConnectionType(MTConnectionType.Ble);
                    break;
                case Xamarin.MagTek.Forms.Enums.ConnectionType.BLE_EMV:
                    _cardReader.SetConnectionType(MTConnectionType.Bleemv);
                    break;
                //case Xamarin.MagTek.Forms.Enums.ConnectionType.Bluetooth:
                //    _cardReader.SetConnectionType(MTConnectionType.Bluetooth);
                //    break;
                case Xamarin.MagTek.Forms.Enums.ConnectionType.USB:
                    _cardReader.SetConnectionType(MTConnectionType.Usb);
                    break;
                //case Xamarin.MagTek.Forms.Enums.ConnectionType.Audio:
                //    _cardReader.SetConnectionType(MTConnectionType.Audio);
                //    break;
                //case Xamarin.MagTek.Forms.Enums.ConnectionType.Lightning:
                //    break;
            }
        }

        public void SetDeviceProtocolString(string pData)
        {
        }

        public void SetDeviceType(int deviceType)
        {
            //_cardReader.setde
            //_cardReader.device//
        }

        public int SetUserSelectionResult(byte status, byte selection)
        {
            return _cardReader.SetUserSelectionResult(status.ToSByte(), selection.ToSByte());
        }

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
            //var scanner = _bluetoothAdapter.BluetoothLeScanner;
            //scanner.StartScan();
            _bluetoothAdapter.StartLeScan(_myLeCallBack);
            if (_bluetoothAdapter.BondedDevices != null)
            {
                foreach (var device in _bluetoothAdapter.BondedDevices)
                {
                    _myLeCallBack.AddDevice(device);
                }
            }

        }

        public int StartTransaction(byte timeLimit, byte cardType, byte option, byte amount, byte transactionType, byte cashBack, byte currencyCode, byte reportingOption)
        {
            return _cardReader.StartTransaction(timeLimit.ToSByte(), cardType.ToSByte(), option.ToSByte(), amount.ToByteArray(), transactionType.ToSByte(), cashBack.ToByteArray(), currencyCode.ToByteArray(), reportingOption.ToSByte());
        }

        public void StopScanningForPeripherals()
        {

            if (isScanning)
            {
                _bluetoothAdapter.StopLeScan(_myLeCallBack);
                isScanning = false;
            }
        }

        public long SwipeCount()
        {
            return _cardReader.SwipeCount;
        }

        public string TLVPayload()
        {
            return "";
        }

        public string TLVVersion()
        {
            return _cardReader.TLVVersion;
        }

        public string Track1()
        {
            return _cardReader.Track1;
        }

        public string Track1DecodeStatus()
        {
            return "";
        }

        public string Track1Masked()
        {
            return _cardReader.Track1Masked;
        }

        public string Track2()
        {
            return _cardReader.Track2;
        }

        public string Track2DecodeStatus()
        {
            return "";
        }

        public string Track2Masked()
        {
            return _cardReader.Track2Masked;
        }

        public string Track3()
        {
            return _cardReader.Track3;
        }

        public string Track3DecodeStatus()
        {
            return "";
        }

        public string Track3Masked()
        {
            return _cardReader.Track3Masked;
        }

        public string TrackDecodeStatus()
        {
            return "";
        }

        #region Event Handlers
        private void MTSCRA_delegates_OnDeviceResponseDelegate(string response)
        {
            OnDeviceResponseDelegate?.Invoke(null);
        }

        private void MTSCRA_delegates_OnDeviceConnectionDidChangeDelegate(MTConnectionState connectionState)
        {
            // Translate to common ConnectionState
            Xamarin.MagTek.Forms.Enums.ConnectionState returnState;
            if (connectionState == MTConnectionState.Connected)
            {
                returnState = Xamarin.MagTek.Forms.Enums.ConnectionState.Connected;
            }
            else if (connectionState == MTConnectionState.Connecting)
            {
                returnState = Xamarin.MagTek.Forms.Enums.ConnectionState.Connecting;
            }
            else if (connectionState == MTConnectionState.Disconnected)
            {
                returnState = Xamarin.MagTek.Forms.Enums.ConnectionState.Disconnected;
            }
            else if (connectionState == MTConnectionState.Disconnecting)
            {
                returnState = Xamarin.MagTek.Forms.Enums.ConnectionState.Disconnecting;
            }
            else
            {
                returnState = Xamarin.MagTek.Forms.Enums.ConnectionState.Error;
            }

            OnDeviceConnectionDidChangeDelegate?.Invoke(Xamarin.MagTek.Forms.Enums.DeviceType.MAGTEKEDYNAMO.GetHashCode()
                , (connectionState == MTConnectionState.Connected)
                , null
                , returnState);
        }

        private void MTSCRA_delegates_OnDataReceivedDelegate(Com.Magtek.Mobile.Android.Mtlib.IMTCardData cardDataObj)
        {
            OnDataReceivedDelegate?.Invoke(getCardData(cardDataObj), null);
        }

        private void MTSCRA_delegates_OnCardDataStateChangedDelegate(MTCardDataState cardState)
        {
            OnCardSwipeDidStartDelegate?.Invoke(cardState);
        }

        private void MTSCRA_delegates_OnBleReaderDidDiscoverPeripheralDelegate()
        {

        }

        private void OnBlueToothBondChangedDelegate_eventHandler(object sender, Bond bond)
        {
            OnBlueToothBondChangedDelegate?.Invoke(ConversionHelper.GetBondState(bond));

            if (sender is MyBroadCastReciever broadCastReciever)
            {
                // Okay, disabling the unsubscribe because if the user disconnects from the settings and returns the app, it get's out of sync, what happens with multiple?
                //if (bond != Bond.Bonding)
                //    broadCastReciever.BondChanged-= OnBlueToothBondChangedDelegate_eventHandler;
            }
        }
        #endregion


        #region Private Methods
        private Xamarin.MagTek.Forms.Models.IMTCardData getCardData(Com.Magtek.Mobile.Android.Mtlib.IMTCardData cardDataObj)
        {
            string strYear = null, strMonth = null;

            try
            {
                if (cardDataObj.CardExpDate != null && cardDataObj.CardExpDate.Length == 4)
                {
                    strYear = cardDataObj.CardExpDate.Substring(0, 2);
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

        // Don't believe we need this, saving just in case
        //class MyBroadcastReceiver : BroadcastReceiver
        //{
        //    //var returnList = new List<IMagTekDevice>();
        //    public bool IsScanning { get; set; }
        //    public Action<List<IMagTekDevice>> DevicesFound { get; set; }
        //    public override void OnReceive(Context context, Intent intent)
        //    {
        //        string action = intent.Action;

        //        // When discovery finds a device
        //        if (BluetoothDevice.ActionFound.Equals(action))
        //        {
        //            // Get the BluetoothDevice object from the Intent
        //            BluetoothDevice device = intent.GetParcelableExtra(BluetoothDevice.ExtraDevice) as BluetoothDevice;
        //            // If it's already paired, skip it, because it's been listed already
        //            if (device?.BondState.ToString() != BluetoothDevice.ExtraBondState)
        //            {
        //                if (device.GetType() != typeof(BluetoothDevice))
        //                {
        //                    DevicesFound?.Invoke(new List<IMagTekDevice>()
        //                    {
        //                        new MagTekDevice()
        //                        {
        //                            Address = device.Address,
        //                            DeviceType = device.Type != BluetoothDeviceType.Unknown ? XFMagTek.Enums.MTDeviceType.MAGTEKEDYNAMO : XFMagTek.Enums.MTDeviceType.MAGTEKNONE,
        //                            Id = device.Address,
        //                            Name = device.Name,
        //                            State = device.BondState == Bond.Bonded ? Xamarin.MagTek.Forms.Enums.ConnectionState.Connected : Xamarin.MagTek.Forms.Enums.ConnectionState.Disconnected
        //                        }
        //                    });
        //                    //mDeviceListAdapter.addDevice(device);
        //                    //mDeviceListAdapter.notifyDataSetChanged();
        //                }
        //            }
        //            // When discovery is finished, change the Activity title
        //        }
        //        else if (BluetoothAdapter.ActionDiscoveryFinished.Equals(action))
        //        {
        //            IsScanning = false;
        //        }
        //    }
        //}

        //delegate void BondChangedDelegate(MyBroadCastReciever sender, Bond bond);

        class MyBroadCastReciever : BroadcastReceiver
        {
            public event EventHandler<Bond> BondChanged;

            public override void OnReceive(Context context, Intent intent)
            {
                string action = intent.Action;

                if (intent.Action.Equals(BluetoothDevice.ActionBondStateChanged))
                {
                    int state = intent.GetIntExtra(BluetoothDevice.ExtraBondState, BluetoothDevice.Error);

                    switch (state)
                    {
                        case (int)Bond.Bonding:
                            break;
                        case (int)Bond.Bonded:
                        case (int)Bond.None:
                        default:
                            // Okay, disabling the unsubscribe because if the user disconnects from the settings and returns the app, it get's out of sync, what happens with multiple??.
                            //Android.App.Application.Context.UnregisterReceiver(this);
                            break;
                    }

                    BondChanged?.Invoke(this, (Bond)state);
                }
            }
        }

        class MyLeScanCallBack : Java.Lang.Object, ILeScanCallback
        {
            public List<IDiscoveredDevice> DevicesFound { get; set; } = new List<IDiscoveredDevice>();

            public MyLeScanCallBack()
            {
            }

            public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
            {
                if (scanRecord != null
                    && !string.IsNullOrWhiteSpace(device.Name))
                {
                    AddDevice(device);
                }
            }

            public void AddDevice(BluetoothDevice device)
            {
                var existingDevice = DevicesFound.FirstOrDefault(x => x.Address == device.Address);

                if (!string.IsNullOrWhiteSpace(device.Name)
                    && existingDevice == null
                    )
                {
                    var newDevice = new DiscoveredDevice()
                    {
                        Address = device.Address,
                        DeviceType = device.Type == BluetoothDeviceType.Unknown ? Xamarin.MagTek.Forms.Enums.DeviceType.MAGTEKNONE : Xamarin.MagTek.Forms.Enums.DeviceType.MAGTEKEDYNAMO,
                        Id = device.Address,
                        Name = device.Name,
                        Bond = ConversionHelper.GetBondState(device.BondState)
                    };
                    DevicesFound.Add(newDevice);
                }
                else if (existingDevice != null)
                {
                    existingDevice.Bond = ConversionHelper.GetBondState(device.BondState);
                }
            }

        }
        #endregion
        

    }

    public static class MagTekApi
    {
        private static Android.Content.Context _context = Android.App.Application.Context;
        internal readonly static MTSCRA_delegates_droid MTSCRA_delegates = new MTSCRA_delegates_droid();

        public static MTSCRA MTSCRA;
        public static BluetoothAdapter BluetoothAdapter;
        public static void Init()
        {
            MTSCRA = new MTSCRA(_context, new Handler(MTSCRA_delegates));
            BluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            
            if (!BluetoothAdapter.IsEnabled)
            {
                BluetoothAdapter.Enable();
            }
            else
            {
                if ((BluetoothAdapter == null))
                {
                    Toast.MakeText(_context, 0, ToastLength.Short).Show();
                }
                BluetoothAdapter.Enable();
            }
        }
    }


    internal static class ConversionHelper
    {
        public static sbyte ToSByte(this byte @this)
        {
            SByte b = (sbyte)@this;
            return b;
        }

        public static byte[] ToByteArray(this byte @this)
        {
            byte[] array = new byte[1] { @this };
            return array;
        }

        public static Xamarin.MagTek.Forms.Enums.Bond GetBondState(Bond bondState)
        {
            switch (bondState)
            {
                case Bond.Bonded:
                    return Xamarin.MagTek.Forms.Enums.Bond.Bonded;
                case Bond.Bonding:
                    return Xamarin.MagTek.Forms.Enums.Bond.Bonding;
                case Bond.None:
                    return Xamarin.MagTek.Forms.Enums.Bond.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bondState), $"BondState {bondState} is not handled.");
            }
        }
    }
}