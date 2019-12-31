using Xamarin.MagTek.Forms.Enums;
using Xamarin.MagTek.Forms.Models;

namespace Xamarin.MagTek.Forms.Models
{
    internal class Dynamax : MagTekDevice
    {
        public Dynamax(IeDynamoService magTekService,
            string address,
            string id,
            string name) : base(magTekService, address, id, name)
        {
        }

        public override DeviceType DeviceType => DeviceType.MAGTEKDYNAMAX;

        public override ConnectionType ConnectionType => ConnectionType.Audio; // todo: is that correct connection Type
    }
}