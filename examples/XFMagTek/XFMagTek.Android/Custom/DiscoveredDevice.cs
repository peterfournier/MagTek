using Xamarin.MagTek.Forms.Enums;

namespace XFMagTek.Droid.Custom
{
    class DiscoveredDevice : Xamarin.MagTek.Forms.Models.IDiscoveredDevice
    {
        public Bond Bond { get; set; }
        public DeviceType DeviceType { get; set; }
        public string Address { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}