﻿using System;
using System.ComponentModel;
using System.Text;
using Xamarin.MagTek.Forms.Enums;

namespace Xamarin.MagTek.Forms.Models
{
    public interface IMagTekDevice : INotifyPropertyChanged
    {
        bool IsDeviceRegisteredToClient { get; set; }
        ConnectionState State { get; }
        Bond Bond { get; }
        string Address { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        int ProductId { get; set; }
        string GroupingLetter { get; }
        DeviceType DeviceType { get; }
        void TryToConnectToDevice();
        void DisconnectDevice();
        bool IsDeviceIsAlreadyConnected();
        IMTCardData CardData { get; }
        StringBuilder ConnectionStatusMessage { get; }
        Action OnCardSwiped { get; set; }
        /// <summary>
        /// IMTCardData cardDataObj, object instance
        /// </summary>
        Action<IMTCardData, object> OnDataRecievedFromDevice { get; set; }
        /// <summary>
        /// int deviceType, bool connected, object instance, ConnectionState connectionState
        /// </summary>
        Action<int, bool, object, ConnectionState> OnDeviceConnectionStateChanged { get; set; }
        /// <summary>
        /// INSError error from device
        /// </summary>
        Action<INSError> OnDeviceError { get; set; }
    }
}