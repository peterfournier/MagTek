using Xamarin.MagTek.Forms.Enums;
using Xamarin.MagTek.Forms.Models;

namespace Xamarin.MagTek.Forms.Models
{
    internal class AudioReader : MagTekDevice
    {
        public AudioReader(IeDynamoService magTekService,
            string address,
            string id,
            string name) : base(magTekService, address, id, name)
        {
        }

        public override DeviceType DeviceType => DeviceType.MAGTEKAUDIOREADER;

        public override ConnectionType ConnectionType => ConnectionType.Audio; // todo: is that correct connection Type
    }
}