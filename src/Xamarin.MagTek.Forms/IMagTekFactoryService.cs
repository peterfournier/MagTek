using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.MagTek.Forms.Enums;
using Xamarin.MagTek.Forms.Models;

namespace Xamarin.MagTek.Forms
{
    public interface IMagTekFactoryService
    {
        IMagTekDevice CreateDevice(
            string address,
            DeviceType deviceType,
            string id,
            string name
            );
        ObservableCollection<IMagTekDevice> FoundDevices { get; set; }
        ObservableCollection<IMagTekDevice> RegisteredDevices { get; set; }
        ObservableCollection<IMagTekDevice> ConnectedDevices();
        Task ScanForDevicesCommand();
        void RegisterDevice(IMagTekDevice device);
        void UnRegisterDevice(IMagTekDevice device);
    }
}