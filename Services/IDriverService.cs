using System.Collections.ObjectModel;
using Behavioral.Automation.Elements;
using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Behavioral.Automation.Services
{
    public interface IDriverService 
    {
        string CurrentUrl { [NotNull] get; }

        string Title { [CanBeNull] get; }

        [NotNull]
        IWebElement FindElement([NotNull] string id);

        [NotNull]
        IWebElement FindElement([NotNull] string id, [NotNull] string subpath);

        [NotNull]
        IWebElement FindElementByXpath([NotNull] string path);

        ReadOnlyCollection<IWebElement> FindElements([NotNull] string id);

        void Navigate([NotNull] string url);

        void ScrollTo(IWebElement element);

        void MouseClick();

        void RemoveFocusFromActiveElement();

        void CloseActiveElement();

        void DebugDumpPage();
        
        void NavigateToRelativeUrl(string url);
        void SwitchToTheLastWindow();
        void SwitchToTheFirstWindow();
        void ResizeWindow(int Height, int Width);
        string MakeScreenShot();
    }
}