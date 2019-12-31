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
            string name,
            Bond bond
            );
        ObservableCollection<IMagTekDevice> Devices { get; set; }
        Task ScanForDevicesCommand();
    }
}