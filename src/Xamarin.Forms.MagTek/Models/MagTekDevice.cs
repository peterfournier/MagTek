using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms.MagTek.Enums;

namespace Xamarin.Forms.MagTek.Models
{
    abstract class MagTekDevice : BaseNotify, IMagTekDevice, INotifyPropertyChanged
    {
        #region Fields and Properties        
        private string _id;
        private string _name;
        private string _address;
        private int _productId;
        private bool _isDeviceRegisteredToHost;
        private ConnectionState _state;

        public string Id { get { return _id; } set { SetPropertyChanged(ref _id, value, nameof(Id)); } }
        public string Name { get { return _name; } set { SetPropertyChanged(ref _name, value, nameof(Name)); } }
        public string Address { get { return _address; } set { SetPropertyChanged(ref _address, value, nameof(Address)); } }
        public int ProductId { get { return _productId; } set { SetPropertyChanged(ref _productId, value, nameof(ProductId)); } }
        public ConnectionState State { get { return _state; } private set { SetPropertyChanged(ref _state, value, nameof(State)); } }
        public bool IsDeviceRegisteredToHost { get { return _isDeviceRegisteredToHost; } set { SetPropertyChanged(ref _isDeviceRegisteredToHost, value, nameof(IsDeviceRegisteredToHost)); } }
        public string GroupingLetter => IsDeviceRegisteredToHost ? "Registered" : "Not registered";
        public abstract DeviceType DeviceType { get; }
        public abstract ConnectionType ConnectionType { get; }
        public IMTCardData CardData { get; private set; }
        protected readonly IeDynamoService _MagtekService;

        public Action OnCardSwiped { get; set; }
        public Action<IMTCardData, object> OnDataRecievedFromDevice { get; set; }

        #endregion


        #region Events

        #endregion

        #region Con/De structors

        public MagTekDevice(IeDynamoService magTekService)
        {
            if (magTekService == null)
                throw new ArgumentNullException(nameof(magTekService));

            _MagtekService = magTekService;
            registerCardReaderEvents();
        }

        ~MagTekDevice()
        {
            removeCardReaderEvents();
        }

        #endregion


        #region Methods
        private void closeDeviceIfAlreadyOpenButNotConnected()
        {
            if (_MagtekService.IsDeviceOpened())
                _MagtekService.CloseDevice();
        }
        private bool deviceIsAlreadyConnectedAndOpen()
        {
            if (_MagtekService.IsDeviceOpened()
                && _MagtekService.IsDeviceConnected()
                && !IsSwitchingDevice())
            {
                State = ConnectionState.Connected;
                return true;
            }
            return false;
        }
        private void registerCardReaderEvents()
        {
            try
            {
                removeCardReaderEvents();

                _MagtekService.OnCardSwipeDidStartDelegate += _MagtekService_OnCardSwipeDidStartDelegate;
                _MagtekService.OnDataReceivedDelegate += _MagtekService_OnDataReceivedDelegate;
                _MagtekService.OnDeviceConnectionDidChangeDelegate += _MagtekService_OnDeviceConnectionDidChangeDelegate;
                _MagtekService.OnDeviceErrorDelegate += _MagtekService_OnDeviceErrorDelegate;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void removeCardReaderEvents()
        {
            _MagtekService.OnCardSwipeDidStartDelegate -= _MagtekService_OnCardSwipeDidStartDelegate;
            _MagtekService.OnDataReceivedDelegate -= _MagtekService_OnDataReceivedDelegate;
            _MagtekService.OnDeviceConnectionDidChangeDelegate -= _MagtekService_OnDeviceConnectionDidChangeDelegate;
            _MagtekService.OnDeviceErrorDelegate -= _MagtekService_OnDeviceErrorDelegate;
        }
        private void _MagtekService_OnCardSwipeDidStartDelegate(object instance)
        {
            try
            {
                if (Debugger.IsLogging())
                {
                    Debugger.Log(0, "Trace", "_MagtekService_OnCardSwipeDidStartDelegate invoked");
                }

                OnCardSwiped?.Invoke();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void _MagtekService_OnDataReceivedDelegate(IMTCardData cardDataObj, object instance)
        {
            try
            {
                CardData = cardDataObj;

                if (Debugger.IsLogging())
                {
                    Debugger.Log(0, "Trace", "_MagtekService_OnDataReceivedDelegate invoked");
                    Debugger.Log(0, "Trace", "Data recieved. Your device is working correctly and is ready for use.");
                    Debugger.Log(0, "Trace", $"CardDataObj: {JsonConvert.SerializeObject(cardDataObj, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}";

                    // "instance" always seems to be null, not sure what it's used for.
                    Debugger.Log(0, "Trace", $"InstanceObj: {JsonConvert.SerializeObject(instance, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                }

                OnDataRecievedFromDevice?.Invoke(cardDataObj, instance);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void _MagtekService_OnDeviceConnectionDidChangeDelegate(int deviceType, bool connected, object instance, ConnectionState connectionState)
        {
            try
            {
                if (Debugger.IsLogging())
                {
                    Debugger.Log(0, "Trace", "_MagtekService_OnDeviceConnectionDidChangeDelegate invoked");
                    Debugger.Log(0, "Trace", $"deviceType: {deviceType}");
                    Debugger.Log(0, "Trace", $"connected: {connected}");
                    Debugger.Log(0, "Trace", $"InstanceObj: {JsonConvert.SerializeObject(instance, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    Debugger.Log(0, "Trace", $"state: {connectionState}");
                }

                State = connectionState;

                updateDeviceState();

                OnDataRecieveddFromDevice?.Invoke(cardDataObj, instance);
            }
            catch (Exception)
            {
                State = ConnectionState.Error;
                throw;
            }
        }
        private void _MagtekService_OnDeviceErrorDelegate(INSError error)
        {
            try
            {
                SetDeviceInfo(JsonConvert.SerializeObject(error));
            }
            catch (Exception)
            {
                //DataResponse = $"Message: {ex.Message}{Environment.NewLine} Stacktrace: {ex.StackTrace}";
            }
        }
        public virtual void TryToConnectToDevice()
        {
            try
            {
                if (deviceIsAlreadyConnectedAndOpen())
                    return;

                closeDeviceIfAlreadyOpenButNotConnected();

                //await Task.Delay(100);
                _MagtekService.SetDeviceType(DeviceType.GetHashCode());

                //await Task.Delay(100);
                _MagtekService.SetConnectionType(ConnectionType.GetHashCode());
            }
            catch (Exception)
            {
                State = ConnectionState.Error;
                throw;
            }
        }

        public bool IsDeviceIsAlreadyConnected()
        {
            if (_MagtekService.IsDeviceOpened()
                && (_MagtekService.IsDeviceConnected())
                && _MagtekService.DeviceType() == (short)this.DeviceType)
            {
                State = ConnectionState.Connected;
                return true;
            }
            else
            {
                State = ConnectionState.Disconnected;
                return false;
            }
        }

        public void DisconnectDevice()
        {
            try
            {
                // close device
                if (_MagtekService.IsDeviceOpened())
                {
                    _MagtekService.CloseDevice();
                }

                if (Device.RuntimePlatform == Device.iOS)
                {
                    // set device type to none
                    _MagtekService.SetDeviceType((int)DeviceType.MAGTEKNONE);

                    // set device type to none
                    _MagtekService.SetConnectionType((int)DeviceType.MAGTEKAUDIOREADER);
                }
            }
            catch (Exception)
            {
                State = ConnectionState.Error;
                throw;
            }
        }

        protected bool IsSwitchingDevice()
        {
            return (_MagtekService.DeviceType() == (short)DeviceType) == false;
        }

        #endregion
    }
}
