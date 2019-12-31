using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.MagTek.Forms.Enums;
using Xamarin.MagTek.Forms.Models;
using Xamarin.Forms.Xaml;
using XFMagTek.ViewModels;
using Xamarin.MagTek.Forms;
using Newtonsoft.Json;

namespace XFMagTek.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class eDynamoDeviceSetupPage : ContentPage
    {
        eDynamoDeviceSetupViewModel ViewModel;

        public eDynamoDeviceSetupPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new eDynamoDeviceSetupViewModel();
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
                if (SetProperty(ref _scanning, value, nameof(Scanning)))
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
            if (SelectedDevice.IsDeviceRegisteredToClient == false)
            {
                if (SelectedDevice.DeviceType == DeviceType.MAGTEKIDYNAMO)
                {
                    string deviceSerial = _cardReaderService.MagTekDeviceSerial();

                    SelectedDevice.Name = !string.IsNullOrWhiteSpace(deviceSerial) ? $"iDynamo - {deviceSerial}" : "iDynamo";
                    SelectedDevice.Id = deviceSerial;
                }
                MagTekFactory.RegisterDevice(SelectedDevice);
            }
        }, () => !IsBusy);
        public ICommand ConnectToDeviceCommand => new Command(() =>
        {
            IsBusy = true;
            if (SelectedDevice != null)
            {
                SelectedDevice.TryToConnectToDevice();
            }
        }, () => !IsBusy);
        public ICommand DisconnectDevice => new Command(() =>
        {
            IsBusy = true;

            if (SelectedDevice != null)
                SelectedDevice.DisconnectDevice();

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
                    WireUpEvents();
                    ShowSetupSteps = true;
                }
            }
        }
        public IMagTekFactoryService MagTekFactory { get; set; } = App.Instance.MagTekFactory;
        public ObservableCollection<IMagTekDevice> FoundDevices { get; set; } = new ObservableCollection<IMagTekDevice>();
        public string DataResponse
        {
            get { return _dataResponse; }
            set { SetProperty(ref _dataResponse, value); }
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

        //private string _deviceInfo;
        //public string DeviceInfo
        //{
        //    get
        //    {
        //        return _deviceInfo;
        //    }
        //    set
        //    {
        //        SetProperty(ref _deviceInfo, value);
        //    }
        //}

        #endregion

        public eDynamoDeviceSetupViewModel()
        {

        }


        #region Methods
        private async Task ExecuteScanCommand()
        {
            Scanning = true;
            try
            {
                IsBusy = true;
                await MagTekFactory.ScanForDevicesCommand();
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Scanning = false;
            }
        }

        private void UpdateShowDevicesMessage()
        {
            ShowNoDevicesFound = FoundDevices.Count < 1 && !Scanning;
        }
        //private void SetMyDeviceListView()
        //{
        //    DevicesList.Clear();
        //    foreach (var item in Settings.RegisteredMagTekDevices)
        //    {
        //        DevicesList.Add(item);
        //    }
        //}
        //public async void AddDevice(IMagTekDevice device)
        //{
        //    if (IsDeviceRegistered(device))
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Oops", "Unable to add device.", "Ok");
        //    }
        //    else
        //    {
        //        MagTekFactory.adreg
        //        //Settings.RegisterMagTekDevice(device);
        //        //SetMyDeviceListView();
        //        SelectedDevice = device;
        //    }
        //}
        //public async Task StartAddNewDeviceFlow(DeviceType selectedMtDevice)
        //{
        //    if (selectedMtDevice == DeviceType.MAGTEKEDYNAMO)
        //    {
        //        await ExecuteScanCommand();
        //    }
        //    else
        //    {
        //        var newIdynamo = new MagTekDevice()
        //        {
        //            DeviceType = selectedMtDevice,
        //            State = ConnectionState.Disconnected,
        //            Name = "iDynamo"
        //        };
        //        DevicesList.Add(newIdynamo);
        //    }
        //    //UpdateDeviceState();
        //}



        //private void _cardReaderService_OnBleReaderStateUpdatedDelegate(int state)
        //{
        //    if (SelectedDevice != null)
        //    {
        //        SelectedDevice.State = (ConnectionState)state;
        //        UpdateDeviceState();
        //    }
        //}

        public async void CloseAnyExistingConnections()
        {
            // disconnect device
            try
            {
                SelectedDevice?.DisconnectDevice();
                //UpdateDeviceState();
            }
            catch (Exception ex)
            {
            }
        }
        public void Initialize()
        {
            PropertyChanged += EDynamoDeviceSetupViewModel_PropertyChanged;
            MagTekFactory.FoundDevices.CollectionChanged += (sender, args) =>
            {
                FoundDevices.Clear();
                foreach (var device in MagTekFactory.FoundDevices)
                {
                    FoundDevices.Add(device);
                    DataResponse = FoundDevices.Count.ToString();
                }
                UpdateShowDevicesMessage();
            };
            //WireUpCardReaderEvents();
            //SetMyDeviceListView();
            UpdateShowDevicesMessage();
            //UpdateDeviceState();
        }
        private async Task HandleSelectedDeviceChanged()
        {
            if (SelectedDevice != null)
            {
                IsBusy = true;
                CloseAnyExistingConnections();
                if (SelectedDevice.IsDeviceRegisteredToClient)
                {
                    SelectedDevice.TryToConnectToDevice();
                }
            }
        }
        //private void SetDeviceInfo(string additionalInfo = "")
        //{
        //    DeviceInfo = $"Is Open: {_cardReaderService.IsDeviceOpened()}";
        //    DeviceInfo += Environment.NewLine;
        //    DeviceInfo += $"Is Connected: {_cardReaderService.IsDeviceConnected()}";
        //    DeviceInfo += Environment.NewLine;
        //    DeviceInfo += $"Device type: {_cardReaderService.DeviceType()} = {((DeviceType)_cardReaderService.DeviceType()).ToString()}";
        //    DeviceInfo += Environment.NewLine;
        //    DeviceInfo += $"Connection Type: {_cardReaderService.ConnectionType()} = {((ConnectionType)_cardReaderService.ConnectionType()).ToString()}";
        //    DeviceInfo += Environment.NewLine;
        //    DeviceInfo += $"Additional Info: {additionalInfo}";
        //}
        private void WireUpEvents()
        {
            SelectedDevice.OnCardSwiped = onCardSwiped;
            SelectedDevice.OnDataRecievedFromDevice = onDataRecievedFromDevice;
            SelectedDevice.OnDeviceConnectionStateChanged = onDeviceConnectionStateChanged;
            SelectedDevice.OnDeviceError = async (error) => { await onDeviceError(error); };
        }

        private async Task onDeviceError(INSError obj)
        {
            await Application.Current.MainPage.DisplayAlert("Oops", obj?.LocalizedDescription, "Ok");
            IsBusy = false;
        }

        private void onDeviceConnectionStateChanged(int deviceType, bool connected, object instance, ConnectionState connectionState)
        {
            switch (connectionState)
            {
                case ConnectionState.Error:
                    MagTekDeviceStateColor = Color.Maroon;
                    break;
                case ConnectionState.Connecting:
                    MagTekDeviceStateColor = Color.Blue;
                    break;
                case ConnectionState.Connected:
                    MagTekDeviceStateColor = Color.Green;
                    break;
                case ConnectionState.DeviceReadyToPair:
                    break;
                case ConnectionState.Disconnected:
                case ConnectionState.Disconnecting:
                default:
                    MagTekDeviceStateColor = Color.Gold;
                    break;
            }

            IsBusy = false;
        }

        private void onDataRecievedFromDevice(IMTCardData cardDataObject, object instance)
        {
            Console.WriteLine(JsonConvert.SerializeObject(cardDataObject));
            IsBusy = false;
        }

        private void onCardSwiped()
        {
            //Console.WriteLine(JsonConvert.SerializeObject(cardDataObject));
            IsBusy = false;
        }
        #region events      




        private async void EDynamoDeviceSetupViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedDevice))
            {
                //await HandleSelectedDeviceChanged();
                //await HandleSelectedDeviceChanged();
            }
        }

        #endregion

        #endregion
    }

}
