namespace Xamarin.MagTek.Forms.Enums
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
        // OLD

        //Audio = 0,
        //BLE = 1,
        //BLEEMV = 2,
        //Bluetooth = 3,
        //USB = 4,
        //Lightning = 5

        BLE,
        BLE_EMV,
        USB,
        NONE,
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

    //
    // Summary:
    //     Enumerates values returned by several types.
    //
    // Remarks:
    //     Enumerates value returned by the following:
    //     • Android.Bluetooth.BluetoothDevice.BondState –
    //     • Android.Bluetooth.Bond.Bonded –
    //     • Android.Bluetooth.Bond.Bonding –
    //     • Android.Bluetooth.Bond.None –
    //     .
    public enum Bond
    {
        //
        // Summary:
        //     There is no shared link key with the remote device, so communication (if it is
        //     allowed at all) will be unauthenticated and unencrypted.
        None = 10,
        //
        // Summary:
        //     Indicates bonding (pairing) is in progress with the remote device.
        Bonding = 11,
        //
        // Summary:
        //     Being bonded (paired) with a remote device does not necessarily mean the device
        //     is currently connected. It just means that the pending procedure was completed
        //     at some earlier time, and the link key is still stored locally, ready to use
        //     on the next connection.
        Bonded = 12
    }
}
