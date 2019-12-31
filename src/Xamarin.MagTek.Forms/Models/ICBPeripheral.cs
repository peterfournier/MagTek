using Xamarin.MagTek.Forms.Enums;

namespace Xamarin.MagTek.Forms.Models
{
    public interface ICBPeripheral
    {
        string Name { get; }
        string RSSIstringValue { get; }
        ConnectionState State { get; }
    }
}
