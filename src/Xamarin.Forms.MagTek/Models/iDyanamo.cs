using System;
using Xamarin.Forms.MagTek.Enums;

namespace Xamarin.Forms.MagTek.Models
{
    internal class IDyanamo : MagTekDevice
    {
        private const string MagTekDeviceProtocolString = "com.magtek.idynamo";

        public override DeviceType DeviceType => DeviceType.MAGTEKIDYNAMO;
        public override ConnectionType ConnectionType => ConnectionType.Lightning;

        public IDyanamo(IeDynamoService magTekService) : base(magTekService)
        {
            if (Device.RuntimePlatform == Device.Android)
                throw new InvalidOperationException("Invalid runtime platform. iDyanamo is not supported with Android devices.");
        }

        public override void TryToConnectToDevice()
        {
            base.TryToConnectToDevice();

            _MagtekService.SetDeviceProtocolString(MagTekDeviceProtocolString);

        }
    }
}
