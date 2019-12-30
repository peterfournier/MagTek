using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.MagTek.Enums;

namespace Xamarin.Forms.MagTek.Models
{
    internal class EDynamo : MagTekDevice
    {
        public override DeviceType DeviceType => DeviceType.MAGTEKEDYNAMO;
        public override ConnectionType ConnectionType => ConnectionType.BLEEMV;

        public EDynamo(IeDynamoService magTekService) : base(magTekService)
        {
        }

        public override void TryToConnectToDevice()
        {
            base.TryToConnectToDevice();

            _MagtekService.SetAddress(Address);

            _MagtekService.OpenDevice();
        }
    }
}
