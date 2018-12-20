using System.Collections.Generic;

namespace XFMagTek.Interfaces.MagTek
{
    public interface INSError
    {
        Dictionary<string, object> UserInfo { get; }
        int Code { get; }
        string Domain { get; }
        string HelpAnchor { get; }
        string LocalizedDescription { get; }
        string LocalizedFailureReason { get; }
        string LocalizedRecoverySuggestion { get; }
        string[] LocalizedRecoveryOptions { get; }
    }
}
