using System.Collections.Generic;

namespace Xamarin.MagTek.Forms.Models
{
    public interface INSNotification
    {
        string Name { get; }
        Dictionary<string, string> UserInfo { get; }
    }
}
