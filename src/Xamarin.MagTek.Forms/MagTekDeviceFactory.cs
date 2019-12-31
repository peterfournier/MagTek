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
        public ObservableCollection<IMagTekDevice> Devices { get; set; } = new ObservableCollection<IMagTekDevice>();
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
        private bool IsDeviceAdded(IMagTekDevice device)
        {
            return Devices.Any(x => x.Id == device.Id) || Devices.Any(x => x.Name == device.Name);
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
                    var mtDevice = CreateDevice(discoveredDevice.Address, discoveredDevice.DeviceType, discoveredDevice.Id, discoveredDevice.Name, discoveredDevice.Bond);

                    if (!IsDeviceAdded(mtDevice))
                    {
                        if (mtDevice is IMagTekDevice)
                        {
                            Devices.Add(mtDevice);
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
            string name,
            Bond bond
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
                    return new EDynamo(MagtekService, address, id, name, bond);
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
