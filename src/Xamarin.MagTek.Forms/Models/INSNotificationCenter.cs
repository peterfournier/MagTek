using System;
using Xamarin.MagTek.Forms.Enums;

namespace Xamarin.MagTek.Forms.Models
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
