using XFMagTek.Interfaces.MagTek;
using XFMagTek.Enums;
using System;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;

namespace XFMagTek.Models.MagTek
{
    public class MagTekDevice : BaseNotify, IMagTekDevice, INotifyPropertyChanged
    {
        const string MagTekDeviceProtocolString = "com.magtek.idynamo";
        private string _id;
        private string _name;
        private string _address;
        private int _productId;
        private bool _isDeviceRegisteredToHost;
        private MTDeviceType _deviceType;


        public string Id { get { return _id; } set { SetPropertyChanged(ref _id, value, nameof(Id)); } }
        public string Name { get { return _name; } set { SetPropertyChanged(ref _name, value, nameof(Name)); } }
        public string Address { get { return _address; } set { SetPropertyChanged(ref _address, value, nameof(Address)); } }
        public int ProductId { get { return _productId; } set { SetPropertyChanged(ref _productId, value, nameof(ProductId)); } }
        private MTConnectionState _state;

        public MTConnectionState State
        {
            get
            {
                return _state;
            }
            set
            {
                SetPropertyChanged(ref _state, value, nameof(State));
            }
        }
        public bool IsDeviceRegisteredToHost { get { return _isDeviceRegisteredToHost; } set { SetPropertyChanged(ref _isDeviceRegisteredToHost, value, nameof(IsDeviceRegisteredToHost)); } }
        public string GroupingLetter => IsDeviceRegisteredToHost ? "Registered" : "Not registered";

        public MTDeviceType DeviceType { get { return _deviceType; } set { SetPropertyChanged(ref _deviceType, value, nameof(DeviceType)); } }

        public async Task<bool> ConnectToDevice(IeDynamoService magtekService)
        {
            if (magtekService == null)
                throw new ArgumentNullException();

            try
            {
                bool isSwitchingDevice = IsSwitchingDevice(magtekService);

                if (magtekService.IsDeviceOpened()
                    && magtekService.IsDeviceConnected()
                    && !isSwitchingDevice)
                {
                    this.State = MTConnectionState.Connected;
                }
                else
                {
                    if (magtekService.IsDeviceOpened())
                        magtekService.CloseDevice(); // close current device

                    if (this.DeviceType == MTDeviceType.MAGTEKEDYNAMO)
                    {
                        // set device type we're searching for
                        magtekService.SetDeviceType((int)MTDeviceType.MAGTEKEDYNAMO);
                        await Task.Delay(100);
                        // set connection type
                        magtekService.SetConnectionType((int)MTConnectionType.BLEEMV);
                        await Task.Delay(100);
                        // set address
                        magtekService.SetAddress(this.Address);
                        await Task.Delay(100);

                    }
                    else if (this.DeviceType == MTDeviceType.MAGTEKIDYNAMO)
                    {
                        magtekService.SetDeviceType((int)MTDeviceType.MAGTEKIDYNAMO);
                        await Task.Delay(100);
                        // set connection type
                        magtekService.SetConnectionType((int)MTConnectionType.Lightning);
                        await Task.Delay(100);
                        magtekService.SetDeviceProtocolString(MagTekDeviceProtocolString);
                        await Task.Delay(100);
                    }
                    else
                    {
                        return false;
                    }

                    // open device
                    magtekService.OpenDevice();
                    await Task.Delay(100);
                }

                bool isOpen = magtekService.IsDeviceOpened() && magtekService.IsDeviceConnected();
                State = isOpen ? MTConnectionState.Connected : MTConnectionState.Disconnected;
                return isOpen;
            }
            catch (Exception ex)
            {
                State = MTConnectionState.Error;
            }
            return false;
        }

        public bool IsDeviceIsAlreadyConnected(IeDynamoService magtekService)
        {
            if (magtekService.IsDeviceOpened()
                && (magtekService.IsDeviceConnected())
                && magtekService.DeviceType() == (short)this.DeviceType)
            {
                this.State = MTConnectionState.Connected;
                return true;
            }
            else
            {
                this.State = MTConnectionState.Disconnected;
                return false;
            }
        }

        public async Task<bool> DisconnectDevice(IeDynamoService magtekService)
        {
            if (magtekService == null)
                throw new ArgumentNullException();

            try
            {

                // close device
                if (magtekService.IsDeviceOpened())
                {
                    magtekService.CloseDevice();
                }


                if (Device.RuntimePlatform == Device.iOS)
                {
                    // set device type to none
                    magtekService.SetDeviceType((int)MTDeviceType.MAGTEKNONE);

                    // set device type to none
                    magtekService.SetConnectionType((int)MTDeviceType.MAGTEKAUDIOREADER);
                }

                State = MTConnectionState.Disconnected;

                // just for animation purposes
                await Task.Delay(200);

                return true;
            }
            catch (Exception)
            {
                State = MTConnectionState.Error;
            }
            return false;
        }

        private bool IsSwitchingDevice(IeDynamoService magtekService)
        {
            if (magtekService.DeviceType() == (short)this.DeviceType)
            {
                return false;
            }
            return true;
        }
    }
}
