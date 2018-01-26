using System;
using System.Threading.Tasks;

using AVFoundation;
using AudioToolbox;
using AudioUnit;
using CoreAnimation;
//using CoreAudio;
using CoreBluetooth;
using CoreFoundation;
using CoreGraphics;
using CoreImage;
//using CoreMIDI;
using CoreMedia;
using CoreVideo;
using ExternalAccessory;
using Foundation;
using ImageIO;
using MediaToolbox;
using Metal;
using ObjCRuntime;
using OpenGLES;
using Security;
using UIKit;


namespace MTSCRA_Bindings.iOS
{
    //    interface MTSCRA : INSStreamDelegate
    //[BaseType(typeof(NSObject), Delegates = new[] { "WeakDelegate" },
    //    Events = new[] { typeof(MTSCRAEventDelegate) })]
    [BaseType(typeof(NSObject))]
    interface MTSCRA : INSStreamDelegate
    {
        // -(BOOL)openDevice;
        [Export("openDevice")]
        bool OpenDevice();

        // -(BOOL)closeDevice;
        [Export("closeDevice")]
        bool CloseDevice();

        // -(BOOL)isDeviceConnected;
        [Export("isDeviceConnected")]
        bool IsDeviceConnected();

        // -(NSString *)getTrack1Masked;
        [Export("getTrack1Masked")]
        string GetTrack1Masked();

        // -(NSString *)getTrack2Masked;
        [Export("getTrack2Masked")]
        string GetTrack2Masked();

        // -(NSString *)getTrack3Masked;
        [Export("getTrack3Masked")]
        string GetTrack3Masked();

        // -(NSString *)getMaskedTracks;
        [Export("getMaskedTracks")]
        string GetMaskedTracks();

        // -(NSString *)getTrack1;
        [Export("getTrack1")]
        string GetTrack1();

        // -(NSString *)getTrack2;
        [Export("getTrack2")]
        string GetTrack2();

        // -(NSString *)getTrack3;
        [Export("getTrack3")]
        string GetTrack3();

        // -(NSString *)getMagnePrint;
        [Export("getMagnePrint")]
        string GetMagnePrint();

        // -(NSString *)getMagnePrintStatus;
        [Export("getMagnePrintStatus")]
        string GetMagnePrintStatus();

        // -(NSString *)getDeviceSerial;
        [Export("getDeviceSerial")]
        string GetDeviceSerial();

        // -(NSString *)getMagTekDeviceSerial;
        [Export("getMagTekDeviceSerial")]
        string GetMagTekDeviceSerial();

        // -(NSString *)getFirmware;
        [Export("getFirmware")]
        string GetFirmware();

        // -(NSString *)getDeviceName;
        [Export("getDeviceName")]
        string GetDeviceName();

        // -(NSString *)getDeviceCaps;
        [Export("getDeviceCaps")]
        string GetDeviceCaps();

        // -(NSString *)getDeviceStatus;
        [Export("getDeviceStatus")]
        string GetDeviceStatus();

        // -(NSString *)getTLVVersion;
        [Export("getTLVVersion")]
        string GetTLVVersion();

        // -(NSString *)getDevicePartNumber;
        [Export("getDevicePartNumber")]
        string GetDevicePartNumber();

        // -(NSString *)getKSN;
        [Export("getKSN")]
        string GetKSN();

        // -(NSString *)getTagValue:(UInt32)tag;
        [Export("getTagValue:")]
        string GetTagValue(int tag);

        // -(NSString *)getCapMSR;
        [Export("getCapMSR")]
        string GetCapMSR();

        // -(NSString *)getCapTracks;
        [Export("getCapTracks")]
        string GetCapTracks();

        // -(NSString *)getCapMagStripeEncryption;
        [Export("getCapMagStripeEncryption")]
        string GetCapMagStripeEncryption();

        // -(int)getMagnePrintLength;
        [Export("getMagnePrintLength")]
        int GetMagnePrintLength();

        // -(int)sendCommandToDevice:(NSString *)pData __attribute__((deprecated("use sendcommandWithLength instead."))) __attribute__((deprecated("")));
        [Export("sendCommandToDevice:")]
        int SendCommandToDevice(string pData);

        // -(int)sendcommandWithLength:(NSString *)command;
        [Export("sendcommandWithLength:")]
        int SendcommandWithLength(string command);

        // -(void)setDeviceProtocolString:(NSString *)pData;
        [Export("setDeviceProtocolString:")]
        void SetDeviceProtocolString(string pData);

        // -(void)setConfigurationParams:(NSString *)pData;
        [Export("setConfigurationParams:")]
        void SetConfigurationParams(string pData);

        // -(void)listenForEvents:(UInt32)event;
        [Export("listenForEvents:")]
        void ListenForEvents(int @event);

        // -(long)getDeviceType;
        [Export("getDeviceType")]
        long GetDeviceType();

        // -(NSString *)getCardPAN;
        [Export("getCardPAN")]
        string GetCardPAN();

        // -(int)getCardPANLength;
        [Export("getCardPANLength")]
        int GetCardPANLength();

        // -(NSString *)getSessionID;
        [Export("getSessionID")]
        string GetSessionID();

        // -(NSString *)getResponseData;
        [Export("getResponseData")]
        string GetResponseData();

        // -(NSString *)getCardName;
        [Export("getCardName")]
        string GetCardName();

        // -(NSString *)getCardIIN;
        [Export("getCardIIN")]
        string GetCardIIN();

        // -(NSString *)getCardLast4;
        [Export("getCardLast4")]
        string GetCardLast4();

        // -(NSString *)getCardExpDate;
        [Export("getCardExpDate")]
        string GetCardExpDate();

        // -(NSString *)getExpDateMonth;
        [Export("getExpDateMonth")]
        string GetExpDateMonth();

        // -(NSString *)getExpDateYear;
        [Export("getExpDateYear")]
        string GetExpDateYear();

        // -(NSString *)getCardServiceCode;
        [Export("getCardServiceCode")]
        string GetCardServiceCode();

        // -(NSString *)getCardStatus;
        [Export("getCardStatus")]
        string GetCardStatus();

        // -(NSString *)getTrackDecodeStatus;
        [Export("getTrackDecodeStatus")]
        string GetTrackDecodeStatus();

        // -(NSString *)getTrack1DecodeStatus;
        [Export("getTrack1DecodeStatus")]
        string GetTrack1DecodeStatus();

        // -(NSString *)getTrack2DecodeStatus;
        [Export("getTrack2DecodeStatus")]
        string GetTrack2DecodeStatus();

        // -(NSString *)getTrack3DecodeStatus;
        [Export("getTrack3DecodeStatus")]
        string GetTrack3DecodeStatus();

        // -(NSString *)getResponseType;
        [Export("getResponseType")]
        string GetResponseType();

        // -(void)setDeviceType:(UInt32)deviceType;
        [Export("setDeviceType:")]
        void SetDeviceType(int deviceType);

        // -(BOOL)isDeviceOpened;
        [Export("isDeviceOpened")]
        bool IsDeviceOpened();

        // -(void)clearBuffers;
        [Export("clearBuffers")]
        void ClearBuffers();

        // -(long)getBatteryLevel;
        [Export("getBatteryLevel")]
        long GetBatteryLevel();

        // -(long)getSwipeCount;
        [Export("getSwipeCount")]
        long GetSwipeCount();

        // -(NSString *)getSDKVersion;
        [Export("getSDKVersion")]
        string GetSDKVersion();

        // -(NSString *)getOperationStatus;
        [Export("getOperationStatus")]
        string GetOperationStatus();

        // -(NSString *)getEncryptionStatus;
        [Export("getEncryptionStatus")]
        string GetEncryptionStatus();

        // -(void)stopScanningForPeripherals;
        [Export("stopScanningForPeripherals")]
        void StopScanningForPeripherals();

        // -(void)startScanningForPeripherals;
        [Export("startScanningForPeripherals")]
        void StartScanningForPeripherals();

        // -(void)setUUIDString:(NSString *)uuidString __attribute__((deprecated("setUUIDString will be deprecated in future, use setAddress instead.")));
        [Export("setUUIDString:")]
        void SetUUIDString(string uuidString);

        // -(CBPeripheral *)getConnectedPeripheral;
        [Export("getConnectedPeripheral")]
        CBPeripheral GetConnectedPeripheral();

        // -(NSMutableArray *)getDiscoveredPeripherals;
        [Export("getDiscoveredPeripherals")]
        NSObject[] GetDiscoveredPeripherals();

        // -(NSDictionary *)getDeviceInformationDictionary;
        [Export("getDeviceInformationDictionary")]
        NSDictionary GetDeviceInformationDictionary();

        // -(int)startTransaction:(Byte)timeLimit cardType:(Byte)cardType option:(Byte)option amount:(Byte *)amount transactionType:(Byte)transactionType cashBack:(Byte *)cashBack currencyCode:(Byte *)currencyCode reportingOption:(Byte)reportingOption;
        [Export("startTransaction:cardType:option:amount:transactionType:cashBack:currencyCode:reportingOption:")]
        unsafe int StartTransaction(byte timeLimit, byte cardType, byte option, byte amount, byte transactionType, byte cashBack, byte currencyCode, byte reportingOption);

        // -(int)setUserSelectionResult:(Byte)status selection:(Byte)selection;
        [Export("setUserSelectionResult:selection:")]
        int SetUserSelectionResult(byte status, byte selection);

        // -(int)setAcquirerResponse:(Byte *)response length:(int)length;
        [Export("setAcquirerResponse:length:")]
        unsafe int SetAcquirerResponse(byte response, int length);

        // -(int)cancelTransaction;
        [Export("cancelTransaction")]
        int CancelTransaction();

        // -(BOOL)isDeviceEMV;
        [Export("isDeviceEMV")]
        bool IsDeviceEMV();

        // -(int)sendExtendedCommand:(NSString *)commandIn;
        [Export("sendExtendedCommand:")]
        int SendExtendedCommand(string commandIn);

        // -(NSString *)getTLVPayload;
        [Export("getTLVPayload")]
        string GetTLVPayload();

        // -(void)setConnectionType:(MTSCRAConnectionType)connectionType;
        [Export("setConnectionType:")]
        void SetConnectionType(int connectionType);

        // -(MTSCRAConnectionType)getConnectionType;
        [Export("getConnectionType")]
        long GetConnectionType();

        [Wrap("WeakDelegate")]
        MTSCRAEventDelegate Delegate { get; set; }

        //// @property (nonatomic, weak) id<MTSCRAEventDelegate> delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // -(void)requestDeviceList:(MTSCRAConnectionType)type;
        [Export("requestDeviceList:")]
        void RequestDeviceList(int type);

        // -(void)setAdress:(NSString *)address;
        [Export("setAdress:")]
        void SetAddress(string address);

        // -(int)getProductID;
        [Export("getProductID")]
        int GetProductID();
    }

    // @protocol MTSCRAEventDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface MTSCRAEventDelegate
    {
        // @optional -(void)onDataReceived:(MTCardData *)cardDataObj instance:(id)instance;
        [Abstract]
        [Export("onDataReceived:instance:")]
        void OnDataReceived(MTCardData cardDataObj, NSObject instance);

        // @optional -(void)cardSwipeDidStart:(id)instance;
        [Abstract]
        [Export("cardSwipeDidStart:")]
        void OnCardSwipeDidStart(NSObject instance);

        // @optional -(void)cardSwipeDidGetTransError;
        [Abstract]
        [Export("cardSwipeDidGetTransError")]
        void OnCardSwipeDidGetTransError();

        // @optional -(void)onDeviceConnectionDidChange:(MTSCRADeviceType)deviceType connected:(BOOL)connected instance:(id)instance;
        [Abstract]
        [Export("onDeviceConnectionDidChange:connected:instance:")]
        void OnDeviceConnectionDidChange(nuint deviceType, bool connected, NSObject instance);

        // @optional -(void)bleReaderConnected:(CBPeripheral *)peripheral;
        [Abstract]
        [Export("bleReaderConnected:")]
        void OnBleReaderConnected(CBPeripheral peripheral);

        // @optional -(void)bleReaderDidDiscoverPeripheral;
        [Abstract]
        [Export("bleReaderDidDiscoverPeripheral")]
        void OnBleReaderDidDiscoverPeripheral();

        // @optional -(void)bleReaderStateUpdated:(MTSCRABLEState)state;
        [Abstract]
        [Export("bleReaderStateUpdated:")]
        void OnBleReaderStateUpdated(int state);

        // @optional -(void)onDeviceResponse:(NSData *)data;
        [Abstract]
        [Export("onDeviceResponse:")]
        void OnDeviceResponse(NSData data);

        // @optional -(void)onDeviceError:(NSError *)error;
        [Abstract]
        [Export("onDeviceError:")]
        void OnDeviceError(NSError error);

        // @optional -(void)OnTransactionStatus:(NSData *)data;
        [Abstract]
        [Export("OnTransactionStatus:")]
        void OnTransactionStatus(NSData data);

        // @optional -(void)OnDisplayMessageRequest:(NSData *)data;
        [Abstract]
        [Export("OnDisplayMessageRequest:")]
        void OnDisplayMessageRequest(NSData data);

        // @optional -(void)OnUserSelectionRequest:(NSData *)data;
        [Abstract]
        [Export("OnUserSelectionRequest:")]
        void OnUserSelectionRequest(NSData data);

        // @optional -(void)OnARQCReceived:(NSData *)data;
        [Abstract]
        [Export("OnARQCReceived:")]
        void OnARQCReceived(NSData data);

        // @optional -(void)OnTransactionResult:(NSData *)data;
        [Abstract]
        [Export("OnTransactionResult:")]
        void OnTransactionResult(NSData data);

        // @optional -(void)OnEMVCommandResult:(NSData *)data;
        [Abstract]
        [Export("OnEMVCommandResult:")]
        void OnEMVCommandResult(NSData data);

        // @optional -(void)onDeviceExtendedResponse:(NSString *)data;
        [Abstract]
        [Export("onDeviceExtendedResponse:")]
        void OnDeviceExtendedResponse(string data);

        // @optional -(void)deviceNotPaired;
        [Abstract]
        [Export("deviceNotPaired")]
        void OnDeviceNotPaired();

        // @optional -(void)onDeviceList:(id)instance connectionType:(MTSCRAConnectionType)connectionType deviceList:(NSArray *)deviceList;
        [Abstract]
        [Export("onDeviceList:connectionType:deviceList:")]
        void OnDeviceList(NSObject instance, nuint connectionType, NSObject[] deviceList);
    }

    // @interface MTCardData : NSObject
    [BaseType(typeof(NSObject))]
    interface MTCardData
    {
        // -(id)initWithCardData:(NSString *)cardData;
        [Export("initWithCardData:")]
        IntPtr Constructor(string cardData);

        // @property (nonatomic, strong) NSString * cardIIN;
        [Export("cardIIN", ArgumentSemantic.Strong)]
        string CardIIN { get; set; }

        // @property (nonatomic, strong) NSString * cardData;
        [Export("cardData", ArgumentSemantic.Strong)]
        string CardData { get; set; }

        // @property (nonatomic, strong) NSString * cardLast4;
        [Export("cardLast4", ArgumentSemantic.Strong)]
        string CardLast4 { get; set; }

        // @property (nonatomic, strong) NSString * cardName;
        [Export("cardName", ArgumentSemantic.Strong)]
        string CardName { get; set; }

        // @property (nonatomic, strong) NSString * cardLastName;
        [Export("cardLastName", ArgumentSemantic.Strong)]
        string CardLastName { get; set; }

        // @property (nonatomic, strong) NSString * cardMiddleName;
        [Export("cardMiddleName", ArgumentSemantic.Strong)]
        string CardMiddleName { get; set; }

        // @property (nonatomic, strong) NSString * cardFirstName;
        [Export("cardFirstName", ArgumentSemantic.Strong)]
        string CardFirstName { get; set; }

        // @property (nonatomic, strong) NSString * cardExpDate;
        [Export("cardExpDate", ArgumentSemantic.Strong)]
        string CardExpDate { get; set; }

        // @property (nonatomic, strong) NSString * cardServiceCode;
        [Export("cardServiceCode", ArgumentSemantic.Strong)]
        string CardServiceCode { get; set; }

        // @property (nonatomic, strong) NSString * cardStatus;
        [Export("cardStatus", ArgumentSemantic.Strong)]
        string CardStatus { get; set; }

        // @property (nonatomic, strong) NSString * responseData;
        [Export("responseData", ArgumentSemantic.Strong)]
        string ResponseData { get; set; }

        // @property (nonatomic, strong) NSString * maskedTracks;
        [Export("maskedTracks", ArgumentSemantic.Strong)]
        string MaskedTracks { get; set; }

        // @property (nonatomic, strong) NSString * encryptedTrack1;
        [Export("encryptedTrack1", ArgumentSemantic.Strong)]
        string EncryptedTrack1 { get; set; }

        // @property (nonatomic, strong) NSString * encryptedTrack2;
        [Export("encryptedTrack2", ArgumentSemantic.Strong)]
        string EncryptedTrack2 { get; set; }

        // @property (nonatomic, strong) NSString * encryptedTrack3;
        [Export("encryptedTrack3", ArgumentSemantic.Strong)]
        string EncryptedTrack3 { get; set; }

        // @property (nonatomic, strong) NSString * encryptionStatus;
        [Export("encryptionStatus", ArgumentSemantic.Strong)]
        string EncryptionStatus { get; set; }

        // @property (nonatomic, strong) NSString * maskedTrack1;
        [Export("maskedTrack1", ArgumentSemantic.Strong)]
        string MaskedTrack1 { get; set; }

        // @property (nonatomic, strong) NSString * maskedTrack2;
        [Export("maskedTrack2", ArgumentSemantic.Strong)]
        string MaskedTrack2 { get; set; }

        // @property (nonatomic, strong) NSString * maskedTrack3;
        [Export("maskedTrack3", ArgumentSemantic.Strong)]
        string MaskedTrack3 { get; set; }

        // @property (nonatomic, strong) NSString * trackDecodeStatus;
        [Export("trackDecodeStatus", ArgumentSemantic.Strong)]
        string TrackDecodeStatus { get; set; }

        // @property (nonatomic, strong) NSString * encryptedMagneprint;
        [Export("encryptedMagneprint", ArgumentSemantic.Strong)]
        string EncryptedMagneprint { get; set; }

        // @property (nonatomic, strong) NSString * magneprintStatus;
        [Export("magneprintStatus", ArgumentSemantic.Strong)]
        string MagneprintStatus { get; set; }

        // @property (nonatomic, strong) NSString * deviceSerialNumber;
        [Export("deviceSerialNumber", ArgumentSemantic.Strong)]
        string DeviceSerialNumber { get; set; }

        // @property (nonatomic, strong) NSString * deviceSerialNumberMagTek;
        [Export("deviceSerialNumberMagTek", ArgumentSemantic.Strong)]
        string DeviceSerialNumberMagTek { get; set; }

        // @property (nonatomic, strong) NSString * encrypedSessionID;
        [Export("encrypedSessionID", ArgumentSemantic.Strong)]
        string EncrypedSessionID { get; set; }

        // @property (nonatomic, strong) NSString * deviceKSN;
        [Export("deviceKSN", ArgumentSemantic.Strong)]
        string DeviceKSN { get; set; }

        // @property (nonatomic, strong) NSString * deviceFirmware;
        [Export("deviceFirmware", ArgumentSemantic.Strong)]
        string DeviceFirmware { get; set; }

        // @property (nonatomic, strong) NSString * deviceName;
        [Export("deviceName", ArgumentSemantic.Strong)]
        string DeviceName { get; set; }

        // @property (nonatomic, strong) NSString * deviceCaps;
        [Export("deviceCaps", ArgumentSemantic.Strong)]
        string DeviceCaps { get; set; }

        // @property (nonatomic, strong) NSString * deviceStatus;
        [Export("deviceStatus", ArgumentSemantic.Strong)]
        string DeviceStatus { get; set; }

        // @property (nonatomic, strong) NSString * tlvVersion;
        [Export("tlvVersion", ArgumentSemantic.Strong)]
        string TlvVersion { get; set; }

        // @property (nonatomic, strong) NSString * devicePartNumber;
        [Export("devicePartNumber", ArgumentSemantic.Strong)]
        string DevicePartNumber { get; set; }

        // @property (nonatomic, strong) NSString * capMSR;
        [Export("capMSR", ArgumentSemantic.Strong)]
        string CapMSR { get; set; }

        // @property (nonatomic, strong) NSString * capTracks;
        [Export("capTracks", ArgumentSemantic.Strong)]
        string CapTracks { get; set; }

        // @property (nonatomic, strong) NSString * capMagStripeEncryption;
        [Export("capMagStripeEncryption", ArgumentSemantic.Strong)]
        string CapMagStripeEncryption { get; set; }

        // @property (nonatomic, strong) NSString * maskedPAN;
        [Export("maskedPAN", ArgumentSemantic.Strong)]
        string MaskedPAN { get; set; }

        // @property (nonatomic) long cardPANLength;
        [Export("cardPANLength")]
        nint CardPANLength { get; set; }

        // @property (nonatomic, strong) NSString * additionalInfoTrack1;
        [Export("additionalInfoTrack1", ArgumentSemantic.Strong)]
        string AdditionalInfoTrack1 { get; set; }

        // @property (nonatomic, strong) NSString * additionalInfoTrack2;
        [Export("additionalInfoTrack2", ArgumentSemantic.Strong)]
        string AdditionalInfoTrack2 { get; set; }

        // @property (nonatomic, strong) NSString * responseType;
        [Export("responseType", ArgumentSemantic.Strong)]
        string ResponseType { get; set; }

        // @property (nonatomic) long batteryLevel;
        [Export("batteryLevel")]
        nint BatteryLevel { get; set; }

        // @property (nonatomic) long swipeCount;
        [Export("swipeCount")]
        nint SwipeCount { get; set; }

        // @property (nonatomic, strong) NSString * firmware;
        [Export("firmware", ArgumentSemantic.Strong)]
        string Firmware { get; set; }

        // @property (nonatomic, strong) NSString * tagValue;
        [Export("tagValue", ArgumentSemantic.Strong)]
        string TagValue { get; set; }

        // @property (nonatomic) int magnePrintLength;
        [Export("magnePrintLength")]
        int MagnePrintLength { get; set; }

        // @property (nonatomic) int cardType;
        [Export("cardType")]
        int CardType { get; set; }

        // @property (nonatomic, strong) NSString * cardExpDateMonth;
        [Export("cardExpDateMonth", ArgumentSemantic.Strong)]
        string CardExpDateMonth { get; set; }

        // @property (nonatomic, strong) NSString * cardExpDateYear;
        [Export("cardExpDateYear", ArgumentSemantic.Strong)]
        string CardExpDateYear { get; set; }

        // @property (nonatomic, strong) NSString * cardPAN;
        [Export("cardPAN", ArgumentSemantic.Strong)]
        string CardPAN { get; set; }

        // @property (nonatomic, strong) NSString * track1DecodeStatus;
        [Export("track1DecodeStatus", ArgumentSemantic.Strong)]
        string Track1DecodeStatus { get; set; }

        // @property (nonatomic, strong) NSString * track2DecodeStatus;
        [Export("track2DecodeStatus", ArgumentSemantic.Strong)]
        string Track2DecodeStatus { get; set; }

        // @property (nonatomic, strong) NSString * track3DecodeStatus;
        [Export("track3DecodeStatus", ArgumentSemantic.Strong)]
        string Track3DecodeStatus { get; set; }
    }
}

