## MTSCRA_Bindings.iOS
#### Easy to use bindings

## Getting started
To use the bindings, you need to:

  - **Install the [nuget package here](https://www.nuget.org/packages/Xamarin.Bindings.MagTek.iOS/)**.

  - **Wire up MTSCRA in your iOS project**:
  I created a new class, which implements a service so that I then use [Dependency Injection](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/dependency-service/introduction/) 
  to use in my Xamarmin.Forms project.

**Add using**
```csharp
using MTSCRA_Bindings.iOS;
```
**AppDelegate.cs**
```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
     ...
     Forms.Init();
     
     MagTekApi.Init();
     
     ...
     
     LoadApplication(new App());

     return base.FinishedLaunching(app, options);
}
```

**MTSCRAService_iOS.cs**
```csharp
public static class MagTekApi
{
    public static MTSCRA MTSCRA;
    public static void Init()
    {
        MTSCRA = new MTSCRA();
    }
}

public event YOUR_PCL.Delegates.MagTek.OnDataReceivedDelegate OnDataReceivedDelegate;
public class MTSCRAService_iOS : IMTSCRAService
{
    private readonly MTSCRA _cardReader;

    public MTSCRAService_iOS()
    {
	_cardReader = MagTekeDynamoApi.MTSCRA;
	_cardReader.Delegate = new MTSCRADelegates_iOS();
	
	((MTSCRADelegates_iOS)_cardReader.Delegate).OnDataReceivedDelegate += MTSCRAService_iOS_OnDataReceivedDelegate;
    }
    
    private void MTSCRAService_iOS_OnDataReceivedDelegate(MTCardData cardDataObj, NSObject instance)
    {
        OnDataReceivedDelegate?.Invoke(getCardData(cardDataObj), instance);
    }
}
```

**Wire up MTSCRA events for swiping, connecting, ect..**
```csharp
public delegate void OnDataReceivedDelegate(MTCardData cardDataObj, NSObject instance);

public class MTSCRADelegates_iOS : MTSCRAEventDelegate
{
     public event OnDataReceivedDelegate OnDataReceivedDelegate;

     // There are many more events you can override
     public override void OnDataReceived(MTCardData cardDataObj, NSObject instance)
     {
    	OnDataReceivedDelegate?.Invoke(cardDataObj, instance);
     }
     
     // ... other events ommitted
}
```
**Xamarin.Forms project**
```csharp
public class YourPage : ContentPage
{
     private IMTSCRAService _cardReaderService = DependencyService.Get<IMTSCRAService>();
     
     public YourPage()
     {
          WireUpMagTekEvents();
     }
     
     private WireUpMagTekEvents()
     {     	 
          _cardReaderService.OnDataReceivedDelegate += _cardReaderService_OnDataReceivedDelegate;
     }
}
```
> **Note:** refer to MagTeks API documentation for connecting/disconnecting from device. I will have examples and more plug and play features coming soon!

