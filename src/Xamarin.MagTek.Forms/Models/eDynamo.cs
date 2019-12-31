using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.MagTek.Forms.Enums;

namespace Xamarin.MagTek.Forms.Models
{
    internal class EDynamo : MagTekDevice
    {
        public override DeviceType DeviceType => DeviceType.MAGTEKEDYNAMO;
        public override ConnectionType ConnectionType => ConnectionType.BLEEMV;

        public EDynamo(IeDynamoService magTekService,
            string address,
            string id,
            string name) : base(magTekService, address, id, name)
        {
        }

        public override void TryToConnectToDevice()
        {
            base.TryToConnectToDevice();

            var bonded = MagtekService.CreateBond(Address);

            var opened = MagtekService.OpenDevice();

            Bond = bonded && opened ? Bond.Bonded : Bond.None;
        }
    }
}
