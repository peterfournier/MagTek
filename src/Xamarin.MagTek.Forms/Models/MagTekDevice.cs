using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.MagTek.Forms.Enums;

namespace Xamarin.MagTek.Forms.Models
{
    abstract class MagTekDevice : BaseNotify, IMagTekDevice, INotifyPropertyChanged
    {
        #region Fields and Properties        
        private string _id;
        private string _name;
        private string _address;
        private int _productId;
        private bool _deviceNotPairedButConnectedAndOpend;
        private Bond _bond = Bond.None;
        private ConnectionState _state;
        private string _connectionStatusMessaage;

        public string Id { get { return _id; } set { SetPropertyChanged(ref _id, value, nameof(Id)); } }
        public string Name { get { return _name; } set { SetPropertyChanged(ref _name, value, nameof(Name)); } }
        public string Address { get { return _address; } set { SetPropertyChanged(ref _address, value, nameof(Address)); } }
        public int ProductId { get { return _productId; } set { SetPropertyChanged(ref _productId, value, nameof(ProductId)); } }
        public Bond Bond
        {
            get { return _bond; }
            set { SetPropertyChanged(ref _bond, value); }
        }
        public ConnectionState State
        {
            get { return _state; }
            private set
            {
                if (SetPropertyChanged(ref _state, value, nameof(State)))
                {
                    OnDeviceConnectionStateChanged?.Invoke(State);
                    UpdateDeviceMessages();
                }
            }
        }
        public string GroupingLetter => Bond == Bond.Bonded || State == ConnectionState.Connected ? "Paired" : "Not Paired";
        public string ConnectionStatusMessage
        {
            get { return _connectionStatusMessaage ?? (_connectionStatusMessaage = string.Empty); }
            protected set
            {
                SetPropertyChanged(ref _connectionStatusMessaage, value);
            }
        }
        public abstract DeviceType DeviceType { get; }
        public abstract ConnectionType ConnectionType { get; }
        public IMTCardData CardData { get; private set; }
        protected readonly IeDynamoService MagtekService;

        public Action OnCardSwiped { get; set; }
        /// <summary>
        /// IMTCardData cardDataObj, object instance
        /// </summary>
        public Action<IMTCardData, object> OnDataRecievedFromDevice { get; set; }
        /// <summary>
        /// int deviceType, bool connected, object instance, ConnectionState connectionState
        /// </summary>
        public Action<ConnectionState> OnDeviceConnectionStateChanged { get; set; }
        /// <summary>
        /// INSError error from device
        /// </summary>
        public Action<INSError> OnDeviceError { get; set; }
        /// <summary>
        /// Only fires for iOS it seems
        /// </summary>
        public Action OnDeviceNotPaired { get; set; }
        #endregion


        #region Events

        #endregion


        #region Con/De structors

        public MagTekDevice(IeDynamoService magTekService) : this(magTekService, string.Empty, string.Empty, string.Empty) { }
        public MagTekDevice(
            IeDynamoService magTekService,
            string address,
            string id,
            string name
            )
        {
            if (magTekService == null)
                throw new ArgumentNullException(nameof(magTekService));

            MagtekService = magTekService;
            registerCardReaderEvents();
            Address = address;
            Id = id;
            Name = name;
        }

        ~MagTekDevice()
        {
            removeCardReaderEvents();
        }

        #endregion


        #region Methods
        private void closeDeviceIfAlreadyOpenButNotConnected()
        {
            if (MagtekService.IsDeviceOpened())
            {
                MagtekService.ClearBuffers();
                MagtekService.CloseDevice();
            }
        }
        private bool deviceIsAlreadyConnectedAndOpen()
        {
            if (MagtekService.IsDeviceOpened()
                && MagtekService.IsDeviceConnected()
                && !IsSwitchingDevice()
                )
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

                MagtekService.OnCardSwipeDidStartDelegate += MagtekService_OnCardSwipeDidStartDelegate;
                MagtekService.OnDataReceivedDelegate += MagtekService_OnDataReceivedDelegate;
                MagtekService.OnDeviceConnectionDidChangeDelegate += MagtekService_OnDeviceConnectionDidChangeDelegate;
                MagtekService.OnDeviceErrorDelegate += MagtekService_OnDeviceErrorDelegate;
                MagtekService.OnDeviceNotPairedDelegate += MagtekService_OnDeviceNotPairedDelegate;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void removeCardReaderEvents()
        {
            MagtekService.OnCardSwipeDidStartDelegate -= MagtekService_OnCardSwipeDidStartDelegate;
            MagtekService.OnDataReceivedDelegate -= MagtekService_OnDataReceivedDelegate;
            MagtekService.OnDeviceConnectionDidChangeDelegate -= MagtekService_OnDeviceConnectionDidChangeDelegate;
            MagtekService.OnDeviceErrorDelegate -= MagtekService_OnDeviceErrorDelegate;
            MagtekService.OnDeviceNotPairedDelegate -= MagtekService_OnDeviceNotPairedDelegate;

        }
        private void MagtekService_OnCardSwipeDidStartDelegate(object instance)
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
        private void MagtekService_OnDataReceivedDelegate(IMTCardData cardDataObj, object instance)
        {
            try
            {
                CardData = cardDataObj;

                if (Debugger.IsLogging())
                {
                    Debugger.Log(0, "Trace", "_MagtekService_OnDataReceivedDelegate invoked");
                    Debugger.Log(0, "Trace", "Data recieved. Your device is working correctly and is ready for use.");
                    Debugger.Log(0, "Trace", $"CardDataObj: {JsonConvert.SerializeObject(cardDataObj, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");

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
        private void MagtekService_OnDeviceConnectionDidChangeDelegate(int deviceType, bool connected, object instance, ConnectionState connectionState)
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
            }
            catch (Exception)
            {
                State = ConnectionState.Error;
                throw;
            }
        }
        private void MagtekService_OnDeviceErrorDelegate(INSError error)
        {
            try
            {
                if (Debugger.IsLogging())
                {
                    Debugger.Log(0, "Trace", $"Is Open: {MagtekService.IsDeviceOpened()}");
                    Debugger.Log(0, "Trace", $"Is Connected: {MagtekService.IsDeviceConnected()}");
                    Debugger.Log(0, "Trace", $"Device type: {MagtekService.DeviceType()} = {((DeviceType)MagtekService.DeviceType()).ToString()}");
                    Debugger.Log(0, "Trace", $"Connection Type: {MagtekService.ConnectionType()} = {((ConnectionType)MagtekService.ConnectionType()).ToString()}");
                    Debugger.Log(0, "Trace", $"Error: {JsonConvert.SerializeObject(error)}");
                }
            }
            catch (Exception)
            {
                State = ConnectionState.Error;
                throw;
            }

            OnDeviceError?.Invoke(error);
        }
        private void MagtekService_OnDeviceNotPairedDelegate()
        {
            try
            {
                if (Debugger.IsLogging())
                {
                    Debugger.Log(0, "Trace", "MagtekService_OnDeviceNotPairedDelegate invoked");
                }

                _deviceNotPairedButConnectedAndOpend = true;

                OnDeviceNotPaired?.Invoke();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void UpdateDeviceMessages()
        {
            if (_deviceNotPairedButConnectedAndOpend)
                DisconnectDevice();// for some reason the connectionState is true, even if the user hits cancel when pair, which leads to a false positive. This is a little hack work around.

            ConnectionStatusMessage = null;
            switch (State)
            {
                case ConnectionState.Error:
                    ConnectionStatusMessage = "Something went wrong please try again.";
                    break;
                case ConnectionState.Connected:
                    if (Device.RuntimePlatform == Device.iOS)
                        Bond = Bond.Bonded;

                    ConnectionStatusMessage = "Your device is ready to use.";
                    break;
                case ConnectionState.Connecting:
                    ConnectionStatusMessage = "Attempting to connect...";
                    break;
                case ConnectionState.Disconnecting:
                case ConnectionState.Disconnected:
                default:
                    if (Bond == Bond.Bonding)
                    {
                        ConnectionStatusMessage = "Pairing Device";
                    }
                    else if (Bond == Bond.Bonded)
                    {
                        ConnectionStatusMessage = "Paired. Ready to connect.";
                    }
                    else
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                            Bond = Bond.None;

                        ConnectionStatusMessage = "Device not Connected";
                    }
                    break;
            }
        }
        public async virtual Task TryToConnectToDeviceAsync()
        {
            try
            {
                resetDeviceNotPaired();

                if (deviceIsAlreadyConnectedAndOpen())
                    return;

                closeDeviceIfAlreadyOpenButNotConnected();

                await Task.Delay(100);
                MagtekService.SetDeviceType(DeviceType.GetHashCode());

                await Task.Delay(100);
                MagtekService.SetConnectionType(ConnectionType.GetHashCode());
            }
            catch (Exception)
            {
                State = ConnectionState.Error;
                throw;
            }
        }
        public bool CheckIfDeviceIsAlreadyConnected()
        {
            if (MagtekService.IsDeviceOpened()
                && (MagtekService.IsDeviceConnected())
                && MagtekService.DeviceType() == DeviceType.GetHashCode())
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
                if (MagtekService.IsDeviceOpened())
                {
                    MagtekService.ClearBuffers();
                    if (MagtekService.CloseDevice())
                        State = ConnectionState.Disconnected;
                }
                else if (State == ConnectionState.Connected)
                {
                    State = ConnectionState.Disconnected;
                }

                if (Device.RuntimePlatform == Device.iOS)
                {
                    // set device type to none

                    MagtekService.SetDeviceType((int)DeviceType.MAGTEKNONE);

                    // set device type to none
                    MagtekService.SetConnectionType((int)DeviceType.MAGTEKAUDIOREADER);
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
            return (MagtekService.DeviceType() == (short)DeviceType) == false;
        }
        private void resetDeviceNotPaired()
        {
            _deviceNotPairedButConnectedAndOpend = false;
        }
        #endregion
    }
}
