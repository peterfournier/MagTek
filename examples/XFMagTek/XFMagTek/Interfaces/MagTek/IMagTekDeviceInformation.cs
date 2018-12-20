using XFMagTek.Enums;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace XFMagTek.Interfaces.MagTek
{
    public interface IMagTekDevice : INotifyPropertyChanged
    {
        bool IsDeviceRegisteredToHost { get; set; }
        MTConnectionState State { get; set; }
        string Address { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        int ProductId { get; set; }
        string GroupingLetter { get; }
        MTDeviceType DeviceType { get; set; }
        Task<bool> ConnectToDevice(IeDynamoService magtekService);
        Task<bool> DisconnectDevice(IeDynamoService magtekService);
        bool IsDeviceIsAlreadyConnected(IeDynamoService magtekService);
    }
}
