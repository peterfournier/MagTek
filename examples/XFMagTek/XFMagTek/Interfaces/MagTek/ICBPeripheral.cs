using XFMagTek.Enums;

namespace XFMagTek.Interfaces.MagTek
{
    public interface ICBPeripheral
    {
        string Name { get; }
        string RSSIstringValue { get; }
        MTConnectionState State { get; }
    }
}
