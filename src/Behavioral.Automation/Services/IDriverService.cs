using System.Collections.ObjectModel;
using JetBrains.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Behavioral.Automation.Services
{
    public interface IDriverService
    {
        RemoteWebDriver Driver { get; }

        string CurrentUrl { [NotNull] get; }

        string Title { [CanBeNull] get; }

        [NotNull]
        IWebElement FindElement([NotNull] string id);

        [NotNull]
        IWebElement FindElement([NotNull] string id, [NotNull] string subpath);

        [NotNull]
        IWebElement FindElementByXpath([NotNull] string path);

        ReadOnlyCollection<IWebElement> FindElements([NotNull] string id);

        ReadOnlyCollection<IWebElement> FindElementsByXpath([NotNull] string path);

        public void Refresh();

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
        void ScrollElementTo(IWebElement element, int offset);
    }
}