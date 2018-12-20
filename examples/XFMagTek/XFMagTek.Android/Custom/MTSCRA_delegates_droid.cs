using Android.OS;
using Com.Magtek.Mobile.Android.Mtlib;
using XFMagTek.Delegates.MagTek;
using static Android.OS.Handler;

namespace XFMagTek.Droid
{
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
}