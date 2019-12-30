namespace Xamarin.Forms.MagTek.Enums
{
    public enum ConnectionState
    {
        Disconnected = 0,
        Connecting = 1,
        Error = 2,
        Connected = 3,
        Disconnecting = 4,

        // Custom: not from MagTek API
        // jumping to 101 to avoid conflicts
        DeviceReadyToPair = 101
    }
    public enum ConnectionType
    {
        Audio = 0,
        BLE = 1,
        BLEEMV = 2,
        Bluetooth = 3,
        USB = 4,
        Lightning = 5
    }

    public enum DeviceType
    {
        MAGTEKAUDIOREADER, //iOS Only
        MAGTEKIDYNAMO,
        MAGTEKDYNAMAX,
        MAGTEKEDYNAMO,
        MAGTEKUSBMSR, //OSX Only
        MAGTEKKDYNAMO,
        MAGTEKNONE
    }

    public enum MTSCRATransactionEvent
    {
        TRANS_EVENT_OK = 1,
        TRANS_EVENT_ERROR = 2,
        TRANS_EVENT_START = 4
    }

    public enum MTSCRATransactionStatus
    {
        OK,
        Start,
        Error,
    }
}
