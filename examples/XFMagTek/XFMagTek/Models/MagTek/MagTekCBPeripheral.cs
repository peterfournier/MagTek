using XFMagTek.Enums;
using XFMagTek.Interfaces.MagTek;
using System;

namespace XFMagTek.Models.MagTek
{
    public class MagTekCBPeripheral : ICBPeripheral
    {
        private readonly string _name;
        private readonly string _rssIsStringValue;
        private readonly MTConnectionState _state;

        public string Name => _name;

        public string RSSIstringValue => _rssIsStringValue;

        public MTConnectionState State => _state;

        public MagTekCBPeripheral(string name, string rssIsStringValue, MTConnectionState state)
        {
            _name = name;
            _rssIsStringValue = rssIsStringValue;
            _state = state;
        }
    }
}
