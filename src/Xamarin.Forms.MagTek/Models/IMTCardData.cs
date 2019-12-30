namespace Xamarin.Forms.MagTek.Models
{
    public interface IMTCardData
    {
        int BatteryLevel { get; set; }
        int CardPANLength { get; set; }
        int CardType { get; set; }
        int MagnePrintLength { get; set; }
        int SwipeCount { get; set; }
        string AdditionalInfoTrack1 { get; set; }
        string AdditionalInfoTrack2 { get; set; }
        string CapMagStripeEncryption { get; set; }
        string CapMSR { get; set; }
        string CapTracks { get; set; }
        string CardData { get; set; }
        string CardExpDate { get; set; }
        string CardExpDateMonth { get; set; }
        string CardExpDateYear { get; set; }
        string CardFirstName { get; set; }
        string CardIIN { get; set; }
        string CardLast4 { get; set; }
        string CardLastName { get; set; }
        string CardMiddleName { get; set; }
        string CardName { get; set; }
        string CardPAN { get; set; }
        string CardServiceCode { get; set; }
        string CardStatus { get; set; }
        string DeviceCaps { get; set; }
        string DeviceFirmware { get; set; }
        string DeviceKSN { get; set; }
        string DeviceName { get; set; }
        string DevicePartNumber { get; set; }
        string DeviceSerialNumber { get; set; }
        string DeviceSerialNumberMagTek { get; set; }
        string DeviceStatus { get; set; }
        string EncrypedSessionID { get; set; }
        string EncryptedMagneprint { get; set; }
        string EncryptedTrack1 { get; set; }
        string EncryptedTrack2 { get; set; }
        string EncryptedTrack3 { get; set; }
        string EncryptionStatus { get; set; }
        string Firmware { get; set; }
        string MagneprintStatus { get; set; }
        string MaskedPAN { get; set; }
        string MaskedTrack1 { get; set; }
        string MaskedTrack2 { get; set; }
        string MaskedTrack3 { get; set; }
        string MaskedTracks { get; set; }
        string ResponseData { get; set; }
        string ResponseType { get; set; }
        string TagValue { get; set; }
        string TlvVersion { get; set; }
        string Track1DecodeStatus { get; set; }
        string Track2DecodeStatus { get; set; }
        string Track3DecodeStatus { get; set; }
        string TrackDecodeStatus { get; set; }
    }
}
