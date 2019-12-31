namespace Xamarin.MagTek.Forms.Models
{
    public class MagTekCardData : IMTCardData
    {
        public int BatteryLevel { get; set; }
        public int CardPANLength { get; set; }
        public int CardType { get; set; }
        public int MagnePrintLength { get; set; }
        public int SwipeCount { get; set; }
        public string AdditionalInfoTrack1 { get; set; }
        public string AdditionalInfoTrack2 { get; set; }
        public string CapMagStripeEncryption { get; set; }
        public string CapMSR { get; set; }
        public string CapTracks { get; set; }
        public string CardData { get; set; }
        public string CardExpDate { get; set; }
        public string CardExpDateMonth { get; set; }
        public string CardExpDateYear { get; set; }
        public string CardFirstName { get; set; }
        public string CardIIN { get; set; }
        public string CardLast4 { get; set; }
        public string CardLastName { get; set; }
        public string CardMiddleName { get; set; }
        public string CardName { get; set; }
        public string CardPAN { get; set; }
        public string CardServiceCode { get; set; }
        public string CardStatus { get; set; }
        public string DeviceCaps { get; set; }
        public string DeviceFirmware { get; set; }
        public string DeviceKSN { get; set; }
        public string DeviceName { get; set; }
        public string DevicePartNumber { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string DeviceSerialNumberMagTek { get; set; }
        public string DeviceStatus { get; set; }
        public string EncrypedSessionID { get; set; }
        public string EncryptedMagneprint { get; set; }
        public string EncryptedTrack1 { get; set; }
        public string EncryptedTrack2 { get; set; }
        public string EncryptedTrack3 { get; set; }
        public string EncryptionStatus { get; set; }
        public string Firmware { get; set; }
        public string MagneprintStatus { get; set; }
        public string MaskedPAN { get; set; }
        public string MaskedTrack1 { get; set; }
        public string MaskedTrack2 { get; set; }
        public string MaskedTrack3 { get; set; }
        public string MaskedTracks { get; set; }
        public string ResponseData { get; set; }
        public string ResponseType { get; set; }
        public string TagValue { get; set; }
        public string TlvVersion { get; set; }
        public string Track1DecodeStatus { get; set; }
        public string Track2DecodeStatus { get; set; }
        public string Track3DecodeStatus { get; set; }
        public string TrackDecodeStatus { get; set; }
    }
}
