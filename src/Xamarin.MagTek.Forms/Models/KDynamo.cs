using Xamarin.MagTek.Forms.Enums;
using Xamarin.MagTek.Forms.Models;

namespace Xamarin.MagTek.Forms.Models
{
    internal class KDynamo : MagTekDevice
    {
        public KDynamo(IeDynamoService magTekService,
            string address,
            string id,
            string name) : base(magTekService, address, id, name)
        {
        }

        public override DeviceType DeviceType => DeviceType.MAGTEKKDYNAMO;

        public override ConnectionType ConnectionType => ConnectionType.USB; // todo: is that correct connection Type
    }
}