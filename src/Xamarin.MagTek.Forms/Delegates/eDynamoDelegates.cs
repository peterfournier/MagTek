using Xamarin.MagTek.Forms.Enums;
using Xamarin.MagTek.Forms.Models;

namespace Xamarin.MagTek.Forms.Delegates
{
    public delegate void OnARQCReceivedDelegate(INSData data);
    public delegate void OnBleReaderConnectedDelegate(ICBPeripheral peripheral);
    public delegate void OnBleReaderDidDiscoverPeripheralDelegate();
    public delegate void OnBleReaderStateUpdatedDelegate(int state);
    public delegate void OnCardSwipeDidGetTransErrorDelegate();
    public delegate void OnCardSwipeDidStartDelegate(object instance);
    public delegate void OnDataReceivedDelegate(IMTCardData cardDataObj, object instance);
    public delegate void OnDeviceConnectionDidChangeDelegate(int deviceType, bool connected, object instance, ConnectionState mTConnectionState);
    public delegate void OnDeviceErrorDelegate(INSError error);
    public delegate void OnDeviceExtendedResponseDelegate(string data);
    public delegate void OnDeviceListDelegate(object instance, int connectionType, object[] deviceList);
    public delegate void OnDeviceNotPairedDelegate();
    public delegate void OnDeviceResponseDelegate(INSData data);
    public delegate void OnDisplayMessageRequestDelegate(INSData data);
    public delegate void OnEMVCommandResultDelegate(INSData data);
    public delegate void OnTransactionResultDelegate(INSData data);
    public delegate void OnTransactionStatusDelegate(INSData data);
    public delegate void OnUserSelectionRequestDelegate(INSData data);
    public delegate void OnDataEvent(MTSCRATransactionStatus status);
}
