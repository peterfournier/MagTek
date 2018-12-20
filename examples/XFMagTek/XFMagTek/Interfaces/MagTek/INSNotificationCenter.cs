using XFMagTek.Enums;
using System;

namespace XFMagTek.Interfaces.MagTek
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
