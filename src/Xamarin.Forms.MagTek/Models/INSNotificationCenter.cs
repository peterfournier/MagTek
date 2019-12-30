using System;
using Xamarin.Forms.MagTek.Enums;

namespace Xamarin.Forms.MagTek.Models
{
    public interface INSNotificationCenter
    {
        event Action<MTSCRATransactionStatus> OnDataEvent;
        event EventHandler DeviceConnStatusChange;
        void Listen();
        void StopListening();
        string TestingTrackDataReady { get; set; }

    }
}
