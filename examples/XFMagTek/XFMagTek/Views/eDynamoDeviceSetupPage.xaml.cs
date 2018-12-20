using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFMagTek.Enums;
using XFMagTek.Interfaces.MagTek;
using XFMagTek.Models.MagTek;
using XFMagTek.ViewModels;

namespace XFMagTek.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class eDynamoDeviceSetupPage : ContentPage
    {
        public eDynamoDeviceSetupViewModel ViewModel { get; set; } = new eDynamoDeviceSetupViewModel();
        public eDynamoDeviceSetupPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
            ViewModel.Initialize();
        }

        protected override void OnDisappearing()
        {
            ViewModel.CloseAnyExistingConnections();
            base.OnDisappearing();
        }
    }
    

    public class eDynamoDeviceSetupViewModel : BaseViewModel
    {
        #region Fields & Properties        
        private bool _scanning;
        private bool _showSetupSteps;
        private bool _showNoDevicesFound;
        private Color _magTekDeviceStateColor;
        private ICommand _startScanningCommand;
        private IeDynamoService _cardReaderService = DependencyService.Get<IeDynamoService>();        
        private IMagTekDevice _selectedDevice;
        private ObservableCollection<IMagTekDevice> _devices;
        private string _connectionStatus;
        private string _dataResponse;

        public bool ShowSetupSteps
        {
            get { return _showSetupSteps; }
            set { SetProperty(ref _showSetupSteps, value, nameof(ShowSetupSteps)); }
        }
        public bool Scanning
        {
            get { return _scanning; }
            set
            {
                SetProperty(ref _scanning, value, nameof(Scanning));
                UpdateShowDevicesMessage();
            }
        }
        public bool ShowNoDevicesFound
        {
            get { return _showNoDevicesFound; }
            set
            {
                SetProperty(ref _showNoDevicesFound, value, nameof(ShowNoDevicesFound));
            }
        }
        public Color MagTekDeviceStateColor
        {
            get
            {
                return _magTekDeviceStateColor;
            }
            set
            {
                SetProperty(ref _magTekDeviceStateColor, value);
            }
        }
        public ICommand OnRegisterDeviceTapCommand => new Command(() =>
        {
            if (!IsDeviceRegistered(SelectedDevice))
            {
                if (SelectedDevice.DeviceType == MTDeviceType.MAGTEKIDYNAMO)
                {
                    string deviceSerial = _cardReaderService.MagTekDeviceSerial();

                    SelectedDevice.Name = !string.IsNullOrWhiteSpace(deviceSerial) ? $"iDynamo - {deviceSerial}" : "iDynamo";
                    SelectedDevice.Id = deviceSerial;
                }
                AddDevice(SelectedDevice);
            }
        }, () => !IsBusy);
        public ICommand ConnectToDeviceCommand => new Command(async () =>
        {
            IsBusy = true;
            if (SelectedDevice != null)
            {
                await SelectedDevice.ConnectToDevice(_cardReaderService);
                UpdateDeviceState();

            }
        }, () => !IsBusy);
        public ICommand DisconnectDevice => new Command(async () =>
        {
            IsBusy = true;
            if (SelectedDevice != null)
                await SelectedDevice.DisconnectDevice(_cardReaderService);
            UpdateDeviceState();
            IsBusy = false;
        }, () => !IsBusy);
        public ICommand ScanForPeripheralsCommand
        {
            get
            {
                return _startScanningCommand ??
                    (_startScanningCommand = new Command(async () => await ExecuteScanCommand(), () => { return !IsBusy; }));
            }
        }
      
        public IMagTekDevice SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                SetProperty(ref _selectedDevice, value, nameof(SelectedDevice));
                if (_selectedDevice == null)
                {
                    ShowSetupSteps = false;
                }
                else
                {
                    ShowSetupSteps = true;
                }
            }
        }
        //public IMagTekDevice CurrentEditingMagTekDevice
        //{
        //    get { return _currentEditingMagTekDevice; }
        //    set
        //    {
        //        SetProperty(ref _currentEditingMagTekDevice, value, nameof(CurrentEditingMagTekDevice));
        //        if (_currentEditingMagTekDevice == null) return;
        //    }
        //}
        public ObservableCollection<IMagTekDevice> DevicesList
        {
            get { return _devices; }
            set { SetProperty(ref _devices, value, nameof(DevicesList)); }
        }
        public string ConnectionStatus
        {
            get { return _connectionStatus; }
            set { SetProperty(ref _connectionStatus, value, nameof(ConnectionStatus)); }
        }
        public string DataResponse
        {
            get
            {
                return _dataResponse;
            }
            set
            {
                _dataResponse = value;
                SetProperty(ref _dataResponse, value);
            }
        }
        private string _deviceInfo;
        public string DeviceInfo
        {
            get
            {
                return _deviceInfo;
            }
            set
            {
                SetProperty(ref _deviceInfo, value);
            }
        }

        #endregion

        public eDynamoDeviceSetupViewModel()
        {

        }


        #region Methods
        private async Task ExecuteScanCommand()
        {
            Scanning = true;

            // set device type we're searching for
            _cardReaderService.SetDeviceType((int)MTDeviceType.MAGTEKEDYNAMO);
            await Task.Delay(500); // little hack since this services aren't async

            // start scanning
            _cardReaderService.StartScanningForPeripherals();
            await Task.Delay(500); // little hack since this services aren't async

            // get list of any devices found during scan
            var discoveredMagTekDevices = _cardReaderService.GetDiscoveredPeripherals();
            await Task.Delay(500); // for animation

            foreach (var item in discoveredMagTekDevices)
            {
                if (IsDeviceRegistered(item))
                {
                    item.IsDeviceRegisteredToHost = true;
                }
                if (!IsDeviceAdded(item))
                {
                    item.DeviceType = MTDeviceType.MAGTEKEDYNAMO;
                    DevicesList.Add(item);
                }
            }
            // stop scanning
            _cardReaderService.StopScanningForPeripherals();

            Scanning = false;
        }
        private bool IsDeviceAdded(IMagTekDevice device)
        {
            if (DevicesList != null)
                return DevicesList.Any(x => x.Id == device.Id) || DevicesList.Any(x => x.Name == device.Name);
            return false;
        }
        private bool IsDeviceRegistered(IMagTekDevice device)
        {
            return false;
            //return Settings.RegisteredMagTekDevices.Any(x => string.Equals(x.Id, device.Id, StringComparison.CurrentCultureIgnoreCase))
            //    || Settings.RegisteredMagTekDevices.Any(x => string.Equals(x.Name, device.Name, StringComparison.CurrentCultureIgnoreCase));
        }
        private void UpdateShowDevicesMessage()
        {
            ShowNoDevicesFound = (DevicesList == null || DevicesList.Count == 0) && !Scanning;
        }
        //private void SetMyDeviceListView()
        //{
        //    DevicesList.Clear();
        //    foreach (var item in Settings.RegisteredMagTekDevices)
        //    {
        //        DevicesList.Add(item);
        //    }
        //}
        public async void AddDevice(IMagTekDevice device)
        {
            if (IsDeviceRegistered(device))
            {
                await Application.Current.MainPage.DisplayAlert("Oops", "Unable to add device.", "Ok");
            }
            else
            {
                //Settings.RegisterMagTekDevice(device);
                //SetMyDeviceListView();
                SelectedDevice = device;
            }
        }
        public async Task StartAddNewDeviceFlow(MTDeviceType selectedMtDevice)
        {
            if (selectedMtDevice == MTDeviceType.MAGTEKEDYNAMO)
            {
                await ExecuteScanCommand();
            }
            else
            {
                var newIdynamo = new MagTekDevice()
                {
                    DeviceType = selectedMtDevice,
                    State = MTConnectionState.Disconnected,
                    Name = "iDynamo"
                };
                DevicesList.Add(newIdynamo);
            }
            UpdateDeviceState();
        }
       
        private void UpdateDeviceState()
        {
            switch (SelectedDevice?.State)
            {
                case MTConnectionState.Error:
                    MagTekDeviceStateColor = Color.Maroon;
                    ConnectionStatus = "Something went wrong please try again.";
                    IsBusy = false;
                    break;
                case MTConnectionState.Connected:
                    MagTekDeviceStateColor = Color.Green;
                    ConnectionStatus = "Your device is ready to use.";
                    IsBusy = false;
                    break;
                case MTConnectionState.Connecting:
                    ConnectionStatus = "Attempting to connect...";
                    MagTekDeviceStateColor = Color.Blue;
                    IsBusy = true;
                    break;
                case MTConnectionState.Disconnecting:
                case MTConnectionState.Disconnected:
                default:
                    MagTekDeviceStateColor = Color.Gold;
                    ConnectionStatus = "Device not Connected";
                    if (SelectedDevice != null && SelectedDevice.IsDeviceRegisteredToHost)
                    {
                        ConnectionStatus += Environment.NewLine;
                        ConnectionStatus += "You may need to remove and repair this device.";
                    }
                    IsBusy = false;
                    break;
            }
            SetDeviceInfo();
        }
        private void WireUpCardReaderEvents()
        {
            try
            {
                _cardReaderService.OnCardSwipeDidStartDelegate -= _cardReaderService_OnCardSwipeDidStartDelegate;
                _cardReaderService.OnDataReceivedDelegate -= _cardReaderService_OnDataReceivedDelegate;
                _cardReaderService.OnDeviceConnectionDidChangeDelegate -= _cardReaderService_OnDeviceConnectionDidChangeDelegate;
                _cardReaderService.OnDeviceErrorDelegate -= _cardReaderService_OnDeviceErrorDelegate;
                //_cardReaderService.OnBleReaderStateUpdatedDelegate -= _cardReaderService_OnBleReaderStateUpdatedDelegate;


                //_cardReaderService.OnBleReaderStateUpdatedDelegate += _cardReaderService_OnBleReaderStateUpdatedDelegate;
                _cardReaderService.OnCardSwipeDidStartDelegate += _cardReaderService_OnCardSwipeDidStartDelegate;
                _cardReaderService.OnDataReceivedDelegate += _cardReaderService_OnDataReceivedDelegate;
                _cardReaderService.OnDeviceConnectionDidChangeDelegate += _cardReaderService_OnDeviceConnectionDidChangeDelegate;
                _cardReaderService.OnDeviceErrorDelegate += _cardReaderService_OnDeviceErrorDelegate;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void _cardReaderService_OnBleReaderStateUpdatedDelegate(int state)
        {
            if (SelectedDevice != null)
            {
                SelectedDevice.State = (MTConnectionState)state;
                UpdateDeviceState();
            }
        }

        public async void CloseAnyExistingConnections()
        {
            // disconnect device
            try
            {
                await SelectedDevice?.DisconnectDevice(_cardReaderService);
                UpdateDeviceState();
            }
            catch (Exception ex)
            {
            }
        }
        public void Initialize()
        {
            PropertyChanged += EDynamoDeviceSetupViewModel_PropertyChanged;
            DevicesList = new ObservableCollection<IMagTekDevice>();
            DevicesList.CollectionChanged += (sender, args) => { UpdateShowDevicesMessage(); };
            WireUpCardReaderEvents();
            //SetMyDeviceListView();
            UpdateShowDevicesMessage();
            UpdateDeviceState();
        }
        private async Task HandleSelectedDeviceChanged()
        {
            if (SelectedDevice != null)
            {
                IsBusy = true;
                CloseAnyExistingConnections();
                DataResponse = string.Empty;
                if (SelectedDevice.IsDeviceRegisteredToHost)
                {
                    await SelectedDevice.ConnectToDevice(_cardReaderService);
                }
                UpdateDeviceState();                
            }
        }
        private void SetDeviceInfo(string additionalInfo = "")
        {
            DeviceInfo = $"Is Open: {_cardReaderService.IsDeviceOpened()}";
            DeviceInfo += Environment.NewLine;
            DeviceInfo += $"Is Connected: {_cardReaderService.IsDeviceConnected()}";
            DeviceInfo += Environment.NewLine;
            DeviceInfo += $"Device type: {_cardReaderService.DeviceType()} = {((MTDeviceType)_cardReaderService.DeviceType()).ToString()}";
            DeviceInfo += Environment.NewLine;
            DeviceInfo += $"Connection Type: {_cardReaderService.ConnectionType()} = {((MTConnectionType)_cardReaderService.ConnectionType()).ToString()}";
            DeviceInfo += Environment.NewLine;
            DeviceInfo += $"Additional Info: {additionalInfo}";
        }

        #region events      
        private void _cardReaderService_OnDeviceErrorDelegate(INSError error)
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
        private void _cardReaderService_OnDeviceConnectionDidChangeDelegate(int deviceType, bool connected, object instance, MTConnectionState state)
        {
            string statusDidChange;
            try
            {
                DataResponse = string.Empty;
                if (SelectedDevice != null)
                {
                    if (connected)
                    {

                        statusDidChange = $"Connected";
                        SelectedDevice.State = MTConnectionState.Connected;
                    }
                    else
                    {
                        statusDidChange = $"Disconnected";
                        SelectedDevice.State = state;
                    }
                }
                UpdateDeviceState();
            }
            catch (Exception)
            {
                if (SelectedDevice != null)
                    SelectedDevice.State = MTConnectionState.Error;
            }
        }
        private void _cardReaderService_OnDataReceivedDelegate(IMTCardData cardDataObj, object instance)
        {
            try
            {
                DataResponse = "Data recieved. Your device is working correctly and is ready for use.";
                //DataResponse += $"CardDataObj: {JsonConvert.SerializeObject(cardDataObj, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}";
                //DataResponse += Environment.NewLine;
                //DataResponse += $"InstanceObj: {JsonConvert.SerializeObject(instance, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}";
            }
            catch (Exception)
            {
                //DataResponse = $"Message: {ex.Message}{Environment.NewLine} Stacktrace: {ex.StackTrace}";
            }
        }
        private void _cardReaderService_OnCardSwipeDidStartDelegate(object instance)
        {
            try
            {
                DataResponse = "Swiped...";
                //DataResponse = $"_cardReaderService_OnCardSwipeDidStartDelegate: {instance}";
                //DataResponse += Environment.NewLine;
            }
            catch (Exception ex)
            {
                //DataResponse = $"Message: {ex.Message}{Environment.NewLine} Stacktrace: {ex.StackTrace}";
            }
        }
        private async void EDynamoDeviceSetupViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedDevice))
            {
                await HandleSelectedDeviceChanged();
            }
        }

        #endregion

        #endregion
    }

}
