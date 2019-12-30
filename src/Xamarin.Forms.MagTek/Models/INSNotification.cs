using System.Collections.Generic;

namespace Xamarin.Forms.MagTek.Models
{
    public interface INSNotification
    {
        string Name { get; }
        Dictionary<string, string> UserInfo { get; }
    }
}
