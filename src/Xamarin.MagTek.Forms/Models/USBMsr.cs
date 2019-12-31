using Xamarin.MagTek.Forms.Enums;
using Xamarin.MagTek.Forms.Models;

namespace Xamarin.MagTek.Forms.Models
{
    internal class USBMsr : MagTekDevice
    {
        public USBMsr(IeDynamoService magTekService,
            string address,
            string id,
            string name) : base(magTekService, address, id, name)
        {
        }

        public override DeviceType DeviceType => DeviceType.MAGTEKUSBMSR;

        public override ConnectionType ConnectionType => ConnectionType.USB;
    }
}