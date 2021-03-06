﻿using Foundation;
using MTSCRA_Bindings.iOS;
using System.Collections.Generic;
using System;
using XFMagTek.iOS;
using Xamarin.MagTek.Forms.Models;
using Xamarin.MagTek.Forms.Enums;
using CoreBluetooth;

[assembly: Xamarin.Forms.Dependency(typeof(eDynamoService_iOS))]
namespace XFMagTek.iOS
{
    public static class MagTekeDynamoApi
    {
        public static MTSCRA MTSCRA;
        public static void Init()
        {
            MTSCRA = new MTSCRA();
        }
    }

    public class eDynamoService_iOS : IeDynamoService
    {
        private readonly MTSCRA _cardReader;
        private ConnectionState currentState = ConnectionState.Disconnected;

        public event Xamarin.MagTek.Forms.Delegates.OnARQCReceivedDelegate OnARQCReceivedDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnBleReaderConnectedDelegate OnBleReaderConnectedDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnBleReaderDidDiscoverPeripheralDelegate OnBleReaderDidDiscoverPeripheralDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnBleReaderStateUpdatedDelegate OnBleReaderStateUpdatedDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnCardSwipeDidGetTransErrorDelegate OnCardSwipeDidGetTransErrorDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnCardSwipeDidStartDelegate OnCardSwipeDidStartDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDataReceivedDelegate OnDataReceivedDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDeviceConnectionDidChangeDelegate OnDeviceConnectionDidChangeDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDeviceErrorDelegate OnDeviceErrorDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDeviceExtendedResponseDelegate OnDeviceExtendedResponseDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDeviceListDelegate OnDeviceListDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDeviceNotPairedDelegate OnDeviceNotPairedDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDeviceResponseDelegate OnDeviceResponseDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnDisplayMessageRequestDelegate OnDisplayMessageRequestDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnEMVCommandResultDelegate OnEMVCommandResultDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnTransactionResultDelegate OnTransactionResultDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnTransactionStatusDelegate OnTransactionStatusDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnUserSelectionRequestDelegate OnUserSelectionRequestDelegate;
        public event Xamarin.MagTek.Forms.Delegates.OnBlueToothBondChangedDelegate OnBlueToothBondChangedDelegate;

        public eDynamoService_iOS()
        {
            _cardReader = MagTekeDynamoApi.MTSCRA;
            _cardReader.Delegate = new eDynamoDelegates_iOS();

            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnARQCReceivedDelegate += EDynamoService_iOS_OnARQCReceivedDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnBleReaderConnectedDelegate += EDynamoService_iOS_OnBleReaderConnectedDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnBleReaderDidDiscoverPeripheralDelegate += EDynamoService_iOS_OnBleReaderDidDiscoverPeripheralDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnBleReaderStateUpdatedDelegate += EDynamoService_iOS_OnBleReaderStateUpdatedDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnCardSwipeDidGetTransErrorDelegate += EDynamoService_iOS_OnCardSwipeDidGetTransErrorDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnCardSwipeDidStartDelegate += EDynamoService_iOS_OnCardSwipeDidStartDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnDataReceivedDelegate += EDynamoService_iOS_OnDataReceivedDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnDeviceConnectionDidChangeDelegate += EDynamoService_iOS_OnDeviceConnectionDidChangeDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnDeviceErrorDelegate += EDynamoService_iOS_OnDeviceErrorDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnDeviceExtendedResponseDelegate += EDynamoService_iOS_OnDeviceExtendedResponseDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnDeviceListDelegate += EDynamoService_iOS_OnDeviceListDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnDeviceNotPairedDelegate += EDynamoService_iOS_OnDeviceNotPairedDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnDeviceResponseDelegate += EDynamoService_iOS_OnDeviceResponseDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnDisplayMessageRequestDelegate += EDynamoService_iOS_OnDisplayMessageRequestDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnEMVCommandResultDelegate += EDynamoService_iOS_OnEMVCommandResultDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnTransactionResultDelegate += EDynamoService_iOS_OnTransactionResultDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnTransactionStatusDelegate += EDynamoService_iOS_OnTransactionStatusDelegate;
            ((eDynamoDelegates_iOS)_cardReader.Delegate).OnUserSelectionRequestDelegate += EDynamoService_iOS_OnUserSelectionRequestDelegate;
        }

        #region Interface implementation

        public ICollection<IDiscoveredDevice> GetDiscoveredPeripherals()
        {
            var returnList = new List<IDiscoveredDevice>();

            var devices = _cardReader.GetDiscoveredPeripherals();
            foreach (var foundDevice in devices)
            {
                if (foundDevice is CBPeripheral peripheral)
                {
                    var device = new DiscoveredDevice
                    {
                        Id = peripheral.Identifier.ToString(),
                        Name = peripheral.Name,
                        Address = peripheral.Identifier.ToString(),
                        DeviceType = Xamarin.MagTek.Forms.Enums.DeviceType.MAGTEKEDYNAMO,
                        Bond = peripheral.State == CBPeripheralState.Connected ? Bond.Bonded : Bond.None,
                    };

                    returnList.Add(device as IDiscoveredDevice);
                }


            }


            return returnList;
        }

        public bool CloseDevice()
        {
            return _cardReader.CloseDevice();
        }

        public bool IsDeviceConnected()
        {
            return _cardReader.IsDeviceConnected();
        }

        public bool IsDeviceEMV()
        {
            return _cardReader.IsDeviceEMV();
        }

        public bool IsDeviceOpened()
        {
            return _cardReader.IsDeviceOpened();
        }

        public bool OpenDevice()
        {
            return _cardReader.OpenDevice();
        }

        public int CancelTransaction()
        {
            return _cardReader.CancelTransaction();
        }

        public int CardPANLength()
        {
            return _cardReader.GetCardPANLength();
        }

        public int MagnePrintLength()
        {
            return _cardReader.GetMagnePrintLength();
        }

        public int ProductID()
        {
            return _cardReader.GetProductID();
        }

        public int SendCommandToDevice(string pData)
        {
            return _cardReader.SendCommandToDevice(pData);
        }

        public int SendcommandWithLength(string command)
        {
            return _cardReader.SendcommandWithLength(command);
        }

        public int SendExtendedCommand(string commandIn)
        {
            return _cardReader.SendExtendedCommand(commandIn);
        }

        public int SetAcquirerResponse(byte response, int length)
        {
            return _cardReader.SetAcquirerResponse(response, length);
        }

        public int SetUserSelectionResult(byte status, byte selection)
        {
            return _cardReader.SetUserSelectionResult(status, selection);
        }

        public int StartTransaction(byte timeLimit, byte cardType, byte option, byte amount, byte transactionType, byte cashBack, byte currencyCode, byte reportingOption)
        {
            return _cardReader.StartTransaction(timeLimit, cardType, option, amount, transactionType, cashBack, currencyCode, reportingOption);
        }

        public long BatteryLevel()
        {
            return _cardReader.GetBatteryLevel();
        }

        public long ConnectionType()
        {
            return _cardReader.GetConnectionType();
        }

        public long DeviceType()
        {
            return _cardReader.GetDeviceType();
        }

        public long SwipeCount()
        {
            return _cardReader.GetSwipeCount();
        }

        public object GetDeviceInformationDictionary()
        {
            return _cardReader.GetDeviceInformationDictionary();
        }

        public string CapMagStripeEncryption()
        {
            return _cardReader.GetCapMagStripeEncryption();
        }

        public string CapMSR()
        {
            return _cardReader.GetCapMSR();
        }

        public string CapTracks()
        {
            return _cardReader.GetCapTracks();
        }

        public string CardExpDate()
        {
            return _cardReader.GetCardExpDate();
        }

        public string CardIIN()
        {
            return _cardReader.GetCardIIN();
        }

        public string CardLast4()
        {
            return _cardReader.GetCardLast4();
        }

        public string CardName()
        {
            return _cardReader.GetCardName();
        }

        public string CardPAN()
        {
            return _cardReader.GetCardPAN();
        }

        public string CardServiceCode()
        {
            return _cardReader.GetCardServiceCode();
        }

        public string CardStatus()
        {
            return _cardReader.GetCardStatus();
        }

        public string DeviceCaps()
        {
            return _cardReader.GetDeviceCaps();
        }

        public string DeviceName()
        {
            return _cardReader.GetDeviceName();
        }

        public string DevicePartNumber()
        {
            return _cardReader.GetDevicePartNumber();
        }

        public string DeviceSerial()
        {
            return _cardReader.GetDeviceSerial();
        }

        public string DeviceStatus()
        {
            return _cardReader.GetDeviceStatus();
        }

        public string EncryptionStatus()
        {
            return _cardReader.GetEncryptionStatus();
        }

        public string ExpDateMonth()
        {
            return _cardReader.GetExpDateMonth();
        }

        public string ExpDateYear()
        {
            return _cardReader.GetExpDateYear();
        }

        public string Firmware()
        {
            return _cardReader.GetFirmware();
        }

        public string GetTagValue(int tag)
        {
            return _cardReader.GetTagValue(tag);
        }

        public string KSN()
        {
            return _cardReader.GetKSN();
        }

        public string MagnePrint()
        {
            return _cardReader.GetMagnePrint();
        }

        public string MagnePrintStatus()
        {
            return _cardReader.GetMagnePrintStatus();
        }

        public string MagTekDeviceSerial()
        {
            return _cardReader.GetMagTekDeviceSerial();
        }

        public string MaskedTracks()
        {
            return _cardReader.GetMaskedTracks();
        }

        public string OperationStatus()
        {
            return _cardReader.GetOperationStatus();
        }

        public string ResponseData()
        {
            return _cardReader.GetResponseData();
        }

        public string ResponseType()
        {
            return _cardReader.GetResponseType();
        }

        public string SDKVersion()
        {
            return _cardReader.GetSDKVersion();
        }

        public string SessionID()
        {
            return _cardReader.GetSessionID();
        }

        public string TLVPayload()
        {
            return _cardReader.GetTLVPayload();
        }

        public string TLVVersion()
        {
            return _cardReader.GetTLVVersion();
        }

        public string Track1()
        {
            return _cardReader.GetTrack1();
        }

        public string Track1DecodeStatus()
        {
            return _cardReader.GetTrack1DecodeStatus();
        }

        public string Track1Masked()
        {
            return _cardReader.GetTrack1Masked();
        }

        public string Track2()
        {
            return _cardReader.GetTrack2();
        }

        public string Track2DecodeStatus()
        {
            return _cardReader.GetTrack2DecodeStatus();
        }

        public string Track2Masked()
        {
            return _cardReader.GetTrack2Masked();
        }

        public string Track3()
        {
            return _cardReader.GetTrack3();
        }

        public string Track3DecodeStatus()
        {
            return _cardReader.GetTrack3DecodeStatus();
        }

        public string Track3Masked()
        {
            return _cardReader.GetTrack3Masked();
        }

        public string TrackDecodeStatus()
        {
            return _cardReader.GetTrackDecodeStatus();
        }

        public void ClearBuffers()
        {
            _cardReader.ClearBuffers();
        }

        public void ListenForEvents(int @event)
        {
            _cardReader.ListenForEvents(@event);
        }

        public void RequestDeviceList(int type)
        {
            _cardReader.RequestDeviceList(type);
        }

        public void CreateBond(string address)
        {
            _cardReader.SetAddress(address);

            //var manager = new CBPeripheralManager();
            //var device = manager.

            //return _cardReader.OpenDevice();
        }

        public void SetConfigurationParams(string pData)
        {
            _cardReader.SetConfigurationParams(pData);
        }

        public void SetConnectionType(int connectionType)
        {
            //Xamarin.MagTek.Forms.Enums.ConnectionType mTConnectionType = (Xamarin.MagTek.Forms.Enums.ConnectionType)connectionType;

            //switch (connectionType)
            //{
            //    case Xamarin.MagTek.Forms.Enums.ConnectionType.BLE:
            //        _cardReader.SetConnectionType(MTConnectionType.Ble);
            //        break;
            //    case Xamarin.MagTek.Forms.Enums.ConnectionType.BLEEMV:
            //        _cardReader.SetConnectionType(MTConnectionType.Bleemv);
            //        break;
            //    case Xamarin.MagTek.Forms.Enums.ConnectionType.Bluetooth:
            //        _cardReader.SetConnectionType(MTConnectionType.Bluetooth);
            //        break;
            //    case Xamarin.MagTek.Forms.Enums.ConnectionType.USB:
            //        _cardReader.SetConnectionType(MTConnectionType.Usb);
            //        break;
            //    case Xamarin.MagTek.Forms.Enums.ConnectionType.Audio:
            //        _cardReader.SetConnectionType(MTConnectionType.Audio);
            //        break;
            //    case Xamarin.MagTek.Forms.Enums.ConnectionType.Lightning:
            //        break;
            //}

            _cardReader.SetConnectionType(connectionType);
        }

        public void SetDeviceProtocolString(string pData)
        {
            _cardReader.SetDeviceProtocolString(pData);
        }

        public void SetDeviceType(int deviceType)
        {
            _cardReader.SetDeviceType(deviceType);
        }

        public void StartScanningForPeripherals()
        {
            _cardReader.StartScanningForPeripherals();
        }

        public void StopScanningForPeripherals()
        {
            _cardReader.StopScanningForPeripherals();
        }

        #endregion


        #region Private methods
        private void EDynamoService_iOS_OnBleReaderDidDiscoverPeripheralDelegate()
        {
            OnBleReaderDidDiscoverPeripheralDelegate?.Invoke();
        }

        private void EDynamoService_iOS_OnUserSelectionRequestDelegate(NSData data)
        {
            OnUserSelectionRequestDelegate?.Invoke(getMagTekData(data));
        }

        private void EDynamoService_iOS_OnTransactionStatusDelegate(NSData data)
        {
            OnTransactionStatusDelegate?.Invoke(getMagTekData(data));
        }

        private void EDynamoService_iOS_OnTransactionResultDelegate(NSData data)
        {
            OnTransactionResultDelegate?.Invoke(getMagTekData(data));
        }

        private void EDynamoService_iOS_OnEMVCommandResultDelegate(NSData data)
        {
            OnEMVCommandResultDelegate?.Invoke(getMagTekData(data));
        }

        private void EDynamoService_iOS_OnDisplayMessageRequestDelegate(NSData data)
        {
            OnDisplayMessageRequestDelegate?.Invoke(getMagTekData(data));
        }

        private void EDynamoService_iOS_OnDeviceResponseDelegate(NSData data)
        {
            OnDeviceResponseDelegate?.Invoke(getMagTekData(data));
        }

        private void EDynamoService_iOS_OnDeviceNotPairedDelegate()
        {
            OnDeviceNotPairedDelegate?.Invoke();
        }

        private void EDynamoService_iOS_OnDeviceListDelegate(NSObject instance, nuint connectionType, NSObject[] deviceList)
        {
            OnDeviceListDelegate?.Invoke(instance, (int)connectionType, deviceList);
        }

        private void EDynamoService_iOS_OnDeviceExtendedResponseDelegate(string data)
        {
            OnDeviceExtendedResponseDelegate?.Invoke(data);
        }

        private void EDynamoService_iOS_OnDeviceErrorDelegate(NSError error)
        {
            OnDeviceErrorDelegate?.Invoke(error as INSError);
        }

        private void EDynamoService_iOS_OnDeviceConnectionDidChangeDelegate(nuint deviceType, bool connected, NSObject instance)
        {
            var state = currentState == ConnectionState.Disconnected
                            ? (connected ? ConnectionState.Connected : ConnectionState.Disconnected)
                            : currentState;

            OnDeviceConnectionDidChangeDelegate?.Invoke(
                (int)deviceType, 
                connected, 
                instance, 
                state);
        }

        private void EDynamoService_iOS_OnDataReceivedDelegate(MTCardData cardDataObj, NSObject instance)
        {
            OnDataReceivedDelegate?.Invoke(getCardData(cardDataObj), instance);
        }

        private void EDynamoService_iOS_OnCardSwipeDidStartDelegate(NSObject instance)
        {
            OnCardSwipeDidStartDelegate?.Invoke(instance);
        }

        private void EDynamoService_iOS_OnCardSwipeDidGetTransErrorDelegate()
        {
            OnCardSwipeDidGetTransErrorDelegate?.Invoke();
        }

        private void EDynamoService_iOS_OnBleReaderStateUpdatedDelegate(int state)
        {
            currentState = (ConnectionState)state;
            OnBleReaderStateUpdatedDelegate?.Invoke(state);
        }

        private void EDynamoService_iOS_OnBleReaderConnectedDelegate(CoreBluetooth.CBPeripheral peripheral)
        {
            OnBleReaderConnectedDelegate?.Invoke(peripheral as ICBPeripheral);
        }

        private void EDynamoService_iOS_OnARQCReceivedDelegate(NSData data)
        {
            OnARQCReceivedDelegate?.Invoke(getMagTekData(data));
        }

        private ConnectionState getState(string state)
        {
            switch (state)
            {
                case "connecting":
                    return ConnectionState.Connecting;
                case "error":
                    return ConnectionState.Error;
                case "connected":
                    return ConnectionState.Connected;
                case "disconnecting":
                    return ConnectionState.Disconnecting;
                default:
                    return ConnectionState.Disconnected;
            }
        }

        private IMTCardData getCardData(MTCardData cardDataObj)
        {
            return new MagTekCardData()
            {
                AdditionalInfoTrack1 = cardDataObj.AdditionalInfoTrack1,
                AdditionalInfoTrack2 = cardDataObj.AdditionalInfoTrack2,
                BatteryLevel = (int)cardDataObj.BatteryLevel,
                CapMagStripeEncryption = cardDataObj.CapMagStripeEncryption,
                CapMSR = cardDataObj.CapMSR,
                CapTracks = cardDataObj.CapTracks,
                CardData = cardDataObj.CardData,
                CardExpDate = cardDataObj.CardExpDate,
                CardExpDateMonth = cardDataObj.CardExpDateMonth,
                CardExpDateYear = cardDataObj.CardExpDateYear,
                CardFirstName = cardDataObj.CardFirstName,
                CardIIN = cardDataObj.CardIIN,
                CardLast4 = cardDataObj.CardLast4,
                CardLastName = cardDataObj.CardLastName,
                CardMiddleName = cardDataObj.CardMiddleName,
                CardName = cardDataObj.CardName,
                CardPAN = cardDataObj.CardPAN,
                CardPANLength = (int)cardDataObj.CardPANLength,
                CardServiceCode = cardDataObj.CardServiceCode,
                CardStatus = cardDataObj.CardStatus,
                CardType = cardDataObj.CardType,
                DeviceCaps = cardDataObj.DeviceCaps,
                DeviceFirmware = cardDataObj.DeviceFirmware,
                DeviceKSN = cardDataObj.DeviceKSN,
                DeviceName = cardDataObj.DeviceName,
                DevicePartNumber = cardDataObj.DevicePartNumber,
                DeviceSerialNumber = cardDataObj.DeviceSerialNumber,
                DeviceSerialNumberMagTek = cardDataObj.DeviceSerialNumberMagTek,
                DeviceStatus = cardDataObj.DeviceStatus,
                EncrypedSessionID = cardDataObj.EncrypedSessionID,
                EncryptedMagneprint = cardDataObj.EncryptedMagneprint,
                EncryptedTrack1 = cardDataObj.EncryptedTrack1,
                EncryptedTrack2 = cardDataObj.EncryptedTrack2,
                EncryptedTrack3 = cardDataObj.EncryptedTrack3,
                EncryptionStatus = cardDataObj.EncryptionStatus,
                Firmware = cardDataObj.Firmware,
                MagnePrintLength = cardDataObj.MagnePrintLength,
                MagneprintStatus = cardDataObj.MagneprintStatus,
                MaskedPAN = cardDataObj.MaskedPAN,
                MaskedTrack1 = cardDataObj.MaskedTrack1,
                MaskedTrack2 = cardDataObj.MaskedTrack2,
                MaskedTrack3 = cardDataObj.MaskedTrack3,
                MaskedTracks = cardDataObj.MaskedTracks,
                ResponseData = cardDataObj.ResponseData,
                ResponseType = cardDataObj.ResponseType,
                SwipeCount = (int)cardDataObj.SwipeCount,
                TagValue = cardDataObj.TagValue,
                TlvVersion = cardDataObj.TlvVersion,
                Track1DecodeStatus = cardDataObj.Track1DecodeStatus,
                Track2DecodeStatus = cardDataObj.Track2DecodeStatus,
                Track3DecodeStatus = cardDataObj.Track3DecodeStatus,
                TrackDecodeStatus = cardDataObj.TrackDecodeStatus
            };
        }

        private INSData getMagTekData(NSData data)
        {
            return new MagTekNSData(data.ToArray(), (long)data.Length);
        }

        private ICBPeripheral getPeripheral(CoreBluetooth.CBPeripheral peripheral)
        {
            return new MagTekCBPeripheral(peripheral.Name, peripheral.RSSI.ToString(), (ConnectionState)peripheral.State);
        }
        #endregion
    }
}