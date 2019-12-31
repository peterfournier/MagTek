using Xamarin.MagTek.Forms.Enums;

namespace Xamarin.MagTek.Forms.Models
{
    public interface IDiscoveredDevice
    {
        Bond Bond { get; set; }
        DeviceType DeviceType { get; set; }
        string Address { get; set; }
        string Id { get; set; }
        string Name { get; set; }
    }
}
