using System;

namespace Behavioral.Automation.Services.Mapping.Contract
{
    public interface IScopeContextManager
    {
        void SwitchPage(Uri uri);
        IScopeContextRuntime UseControlScopeContextRuntime(ControlScopeSelector controlScopeSelector);
        void SwitchPage(string pageName);
        void SwitchToCurrentUrl(Uri currentUrl);
    }
}