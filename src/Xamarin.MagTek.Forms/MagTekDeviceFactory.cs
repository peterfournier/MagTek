using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.MagTek.Forms.Enums;
using Xamarin.MagTek.Forms.Models;

namespace Xamarin.MagTek.Forms
{
    public class MagTekDeviceFactory : IMagTekFactoryService
    {
        public ObservableCollection<IMagTekDevice> FoundDevices { get; set; } = new ObservableCollection<IMagTekDevice>();
        public ObservableCollection<IMagTekDevice> RegisteredDevices { get; set; } = new ObservableCollection<IMagTekDevice>();
        public IeDynamoService MagtekService { get; private set; }

        #region Con/De structors

        public MagTekDeviceFactory(IeDynamoService magTekService)
        {
            if (magTekService == null)
                throw new ArgumentNullException(nameof(magTekService));

            MagtekService = magTekService;
        }

        #endregion


        #region Public Methods
        private bool IsDeviceRegistered(IMagTekDevice device)
        {
            return RegisteredDevices.Any(x => x.Id == device.Id) || RegisteredDevices.Any(x => x.Name == device.Name);
        }
        private bool IsDeviceAdded(IMagTekDevice device)
        {
            return FoundDevices.Any(x => x.Id == device.Id) || FoundDevices.Any(x => x.Name == device.Name);
        }
        public void RegisterDevice(IMagTekDevice device)
        {
            if (device == null || device.IsDeviceRegisteredToClient || device.IsDeviceIsAlreadyConnected() == false) return;

            RegisteredDevices.Add(device);
            device.IsDeviceRegisteredToClient = true;
        }
        public void UnRegisterDevice(IMagTekDevice device)
        {
            if (device == null) return;

            device.DisconnectDevice();
            RegisteredDevices.Remove(device);
            device.IsDeviceRegisteredToClient = false;
        }
        public ObservableCollection<IMagTekDevice> ConnectedDevices()
        {
            var connectedDevices = new ObservableCollection<IMagTekDevice>();
            var list = RegisteredDevices.Where(x => x.IsDeviceIsAlreadyConnected());
            foreach (var i in list)
            {
                connectedDevices.Add(i);
            }
            return connectedDevices;
        }
        public async Task ScanForDevicesCommand()
        {
            try
            {
                MagtekService.SetDeviceType(DeviceType.MAGTEKEDYNAMO.GetHashCode());
                await Task.Delay(500);

                MagtekService.StartScanningForPeripherals();
                await Task.Delay(500);

                var discoveredMagTekDevices = MagtekService.GetDiscoveredPeripherals();
                await Task.Delay(500);

                foreach (var discoveredDevice in discoveredMagTekDevices)
                {
                    var mtDevice = CreateDevice(discoveredDevice.Address, discoveredDevice.DeviceType, discoveredDevice.Id, discoveredDevice.Name);

                    if (IsDeviceRegistered(mtDevice))
                    {
                        mtDevice.IsDeviceRegisteredToClient = true;
                    }

                    if (!IsDeviceAdded(mtDevice))
                    {
                        if (mtDevice is IMagTekDevice)
                        {
                            FoundDevices.Add(mtDevice);
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                MagtekService.StopScanningForPeripherals();
            }
            #endregion
        }
        public IMagTekDevice CreateDevice(
            string address,
            DeviceType deviceType,
            string id,
            string name
            )
        {
            switch (deviceType)
            {
                case DeviceType.MAGTEKAUDIOREADER:
                    return new AudioReader(MagtekService, address, id, name);
                case DeviceType.MAGTEKIDYNAMO:
                    return new IDynamo(MagtekService, address, id, name);
                case DeviceType.MAGTEKDYNAMAX:
                    return new Dynamax(MagtekService, address, id, name);
                case DeviceType.MAGTEKEDYNAMO:
                    return new EDynamo(MagtekService, address, id, name);
                case DeviceType.MAGTEKUSBMSR:
                    return new USBMsr(MagtekService, address, id, name);
                case DeviceType.MAGTEKKDYNAMO:
                    return new KDynamo(MagtekService, address, id, name);
                case DeviceType.MAGTEKNONE:
                default:
                    throw new Exception("Device not supported with this assembly.");
            }
        }
    }
}
