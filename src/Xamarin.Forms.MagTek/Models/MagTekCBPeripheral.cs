using Xamarin.Forms.MagTek.Enums;

namespace Xamarin.Forms.MagTek.Models
{
    public class MagTekCBPeripheral : ICBPeripheral
    {
        private readonly string _name;
        private readonly string _rssIsStringValue;
        private readonly ConnectionState _state;

        public string Name => _name;

        public string RSSIstringValue => _rssIsStringValue;

        public ConnectionState State => _state;

        public MagTekCBPeripheral(string name, string rssIsStringValue, ConnectionState state)
        {
            _name = name;
            _rssIsStringValue = rssIsStringValue;
            _state = state;
        }
    }
}
