using System;

using Foundation;
using CoreBluetooth;
using MTSCRA_Bindings.iOS;

namespace XFMagTek.iOS
{
    public delegate void OnARQCReceivedDelegate(NSData data);
    public delegate void OnBleReaderConnectedDelegate(CBPeripheral peripheral);
    public delegate void OnBleReaderDidDiscoverPeripheralDelegate();
    public delegate void OnBleReaderStateUpdatedDelegate(int state);
    public delegate void OnCardSwipeDidGetTransErrorDelegate();
    public delegate void OnCardSwipeDidStartDelegate(NSObject instance);
    public delegate void OnDataReceivedDelegate(MTCardData cardDataObj, NSObject instance);
    public delegate void OnDeviceConnectionDidChangeDelegate(nuint deviceType, bool connected, NSObject instance);
    public delegate void OnDeviceErrorDelegate(NSError error);
    public delegate void OnDeviceExtendedResponseDelegate(string data);
    public delegate void OnDeviceListDelegate(NSObject instance, nuint connectionType, NSObject[] deviceList);
    public delegate void OnDeviceNotPairedDelegate();
    public delegate void OnDeviceResponseDelegate(NSData data);
    public delegate void OnDisplayMessageRequestDelegate(NSData data);
    public delegate void OnEMVCommandResultDelegate(NSData data);
    public delegate void OnTransactionResultDelegate(NSData data);
    public delegate void OnTransactionStatusDelegate(NSData data);
    public delegate void OnUserSelectionRequestDelegate(NSData data);

    public class eDynamoDelegates_iOS : MTSCRAEventDelegate
    {
        public event OnARQCReceivedDelegate OnARQCReceivedDelegate;
        public event OnBleReaderConnectedDelegate OnBleReaderConnectedDelegate;
        public event OnBleReaderDidDiscoverPeripheralDelegate OnBleReaderDidDiscoverPeripheralDelegate;
        public event OnBleReaderStateUpdatedDelegate OnBleReaderStateUpdatedDelegate;
        public event OnCardSwipeDidGetTransErrorDelegate OnCardSwipeDidGetTransErrorDelegate;
        public event OnCardSwipeDidStartDelegate OnCardSwipeDidStartDelegate;
        public event OnDataReceivedDelegate OnDataReceivedDelegate;
        public event OnDeviceConnectionDidChangeDelegate OnDeviceConnectionDidChangeDelegate;
        public event OnDeviceErrorDelegate OnDeviceErrorDelegate;
        public event OnDeviceExtendedResponseDelegate OnDeviceExtendedResponseDelegate;
        public event OnDeviceListDelegate OnDeviceListDelegate;
        public event OnDeviceNotPairedDelegate OnDeviceNotPairedDelegate;
        public event OnDeviceResponseDelegate OnDeviceResponseDelegate;
        public event OnDisplayMessageRequestDelegate OnDisplayMessageRequestDelegate;
        public event OnEMVCommandResultDelegate OnEMVCommandResultDelegate;
        public event OnTransactionResultDelegate OnTransactionResultDelegate;
        public event OnTransactionStatusDelegate OnTransactionStatusDelegate;
        public event OnUserSelectionRequestDelegate OnUserSelectionRequestDelegate;

        public eDynamoDelegates_iOS()
        {

        }

        #region Override methods

        public override void OnCardSwipeDidStart(NSObject instance)
        {
            OnCardSwipeDidStartDelegate?.Invoke(instance);
        }

        public override void OnDeviceResponse(NSData data)
        {
            OnDeviceResponseDelegate?.Invoke(data);
        }

        public override void OnDataReceived(MTCardData cardDataObj, NSObject instance)
        {
            OnDataReceivedDelegate?.Invoke(cardDataObj, instance);
        }

        public override void OnARQCReceived(NSData data)
        {
            OnARQCReceivedDelegate?.Invoke(data);
        }

        public override void OnBleReaderConnected(CBPeripheral peripheral)
        {
            OnBleReaderConnectedDelegate?.Invoke(peripheral);
        }

        public override void OnBleReaderStateUpdated(int state)
        {
            OnBleReaderStateUpdatedDelegate?.Invoke(state);
        }

        public override void OnCardSwipeDidGetTransError()
        {
            OnCardSwipeDidGetTransErrorDelegate?.Invoke();
        }

        public override void OnDeviceConnectionDidChange(nuint deviceType, bool connected, NSObject instance)
        {
            OnDeviceConnectionDidChangeDelegate?.Invoke(deviceType, connected, instance);
        }

        public override void OnDeviceError(NSError error)
        {
            OnDeviceErrorDelegate?.Invoke(error);
        }

        public override void OnDeviceExtendedResponse(string data)
        {
            OnDeviceExtendedResponseDelegate?.Invoke(data);
        }

        public override void OnDeviceList(NSObject instance, nuint connectionType, NSObject[] deviceList)
        {
            OnDeviceListDelegate?.Invoke(instance, connectionType, deviceList);
        }

        public override void OnDeviceNotPaired()
        {
            OnDeviceNotPairedDelegate?.Invoke();
        }

        public override void OnDisplayMessageRequest(NSData data)
        {
            OnDisplayMessageRequestDelegate?.Invoke(data);
        }

        public override void OnEMVCommandResult(NSData data)
        {
            OnEMVCommandResultDelegate?.Invoke(data);
        }

        public override void OnTransactionResult(NSData data)
        {
            OnTransactionResultDelegate?.Invoke(data);
        }

        public override void OnTransactionStatus(NSData data)
        {
            OnTransactionStatusDelegate?.Invoke(data);
        }

        public override void OnUserSelectionRequest(NSData data)
        {
            OnUserSelectionRequestDelegate?.Invoke(data);
        }

        public override void OnBleReaderDidDiscoverPeripheral()
        {
            OnBleReaderDidDiscoverPeripheralDelegate?.Invoke();
        }
        #endregion
    }
}