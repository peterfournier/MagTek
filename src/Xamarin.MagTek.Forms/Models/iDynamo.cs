using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.MagTek.Forms.Enums;

namespace Xamarin.MagTek.Forms.Models
{
    internal class IDynamo : MagTekDevice
    {
        private const string MagTekDeviceProtocolString = "com.magtek.idynamo";

        public override DeviceType DeviceType => DeviceType.MAGTEKIDYNAMO;
        public override ConnectionType ConnectionType => ConnectionType.Lightning;

        public IDynamo(IeDynamoService magTekService,
            string address,
            string id,
            string name) : base(magTekService, address, id, name)
        {
            if (Device.RuntimePlatform == Device.Android)
                throw new InvalidOperationException("Invalid runtime platform. iDyanamo is not supported with Android devices.");
        }

        public override void TryToConnectToDevice()
        {
            base.TryToConnectToDevice();

            MagtekService.SetDeviceProtocolString(MagTekDeviceProtocolString);

            MagtekService.OpenDevice();

            updateBond();
        }

        private void updateBond()
        {
            if (State == ConnectionState.Connected)
                Bond = Bond.Bonded;
            else
                Bond = Bond.None;
        }
    }
}
