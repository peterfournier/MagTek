using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms.MagTek.Enums;

namespace Xamarin.Forms.MagTek.Models
{
    public interface IMagTekDevice : INotifyPropertyChanged
    {
        bool IsDeviceRegisteredToHost { get; set; }
        ConnectionState State { get; }
        string Address { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        int ProductId { get; set; }
        string GroupingLetter { get; }
        DeviceType DeviceType { get; }
        void TryToConnectToDevice();
        void DisconnectDevice();
        bool IsDeviceIsAlreadyConnected();
    }
}
