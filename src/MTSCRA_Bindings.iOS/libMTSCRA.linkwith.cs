using System;
using ObjCRuntime;

[assembly: LinkWith("libMTSCRA.a",
    LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64,
    Frameworks = "CoreAudio Foundation AudioToolbox CoreBluetooth ExternalAccessory AVFoundation",
    ForceLoad = true,
    IsCxx = true)]
