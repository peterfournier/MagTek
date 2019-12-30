using Xamarin.Forms.MagTek.Enums;

namespace Xamarin.Forms.MagTek.Models
{
    public interface ICBPeripheral
    {
        string Name { get; }
        string RSSIstringValue { get; }
        ConnectionState State { get; }
    }
}
