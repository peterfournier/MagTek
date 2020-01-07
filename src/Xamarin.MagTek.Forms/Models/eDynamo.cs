using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.MagTek.Forms.Enums;

namespace Xamarin.MagTek.Forms.Models
{
    internal class EDynamo : MagTekDevice
    {
        public override DeviceType DeviceType => DeviceType.MAGTEKEDYNAMO;
        public override ConnectionType ConnectionType => ConnectionType.BLE_EMV;

        public EDynamo(IeDynamoService magTekService,
            string address,
            string id,
            string name,
            Bond bond
            ) : base(magTekService, address, id, name)
        {
            Bond = bond;
        }

        ~EDynamo()
        {
            MagtekService.OnBlueToothBondChangedDelegate -= MagtekService_OnBlueToothBondChangedDelegate;
        }

        public async override Task TryToConnectToDeviceAsync()
        {
            await base.TryToConnectToDeviceAsync();

            await Task.Delay(100);
            MagtekService.CreateBond(Address);

            await Task.Delay(100);
            MagtekService.OpenDevice();

            if (Bond == Bond.None && Device.RuntimePlatform == Device.Android)
                MagtekService.OnBlueToothBondChangedDelegate += MagtekService_OnBlueToothBondChangedDelegate;
        }

        private void MagtekService_OnBlueToothBondChangedDelegate(Bond bond)
        {
            Bond = bond;
            
            UpdateDeviceMessages();

            // Okay, disabling the unsubscribe because if the user disconnects from the settings and returns the app, it get's out of sync, what happens with multiple??.
            //if (bond != Bond.Bonding)
            //    MagtekService.OnBlueToothBondChangedDelegate-= MagtekService_OnBlueToothBondChangedDelegate;
        }
    }
}
