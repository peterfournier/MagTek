## MTSCRA_Bindings.iOS
#### Easy to use bindings

## Getting started
To use the bindings, you need to:

  - **Install the [nuget package here](https://www.nuget.org/packages/Xamarin.Bindings.MagTek.iOS/)**.

  - **Wire up MTSCRA** in you iOS project:
  I created a new class, which implements a service so that I then use [Dependency Injection](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/dependency-service/introduction/) 
  to use in my Xamarmin.Forms project.



```csharp
public static class MagTekApi
{
    public static MTSCRA MTSCRA;
    public static void Init()
    {
        MTSCRA = new MTSCRA();
    }
}

 public class MTSCRAService_iOS : IMTSCRAService
{
    private readonly MTSCRA _cardReader;


	 public MTSCRAService_iOS()
    {
		_cardReader = MagTekeDynamoApi.MTSCRA;
		_cardReader.Delegate = new MTSCRADelegates_iOS();
	}
}
```