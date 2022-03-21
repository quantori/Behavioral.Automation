using JetBrains.Annotations;

namespace Behavioral.Automation.Services
{
    public interface IDriverServiceBase
    {
        string CurrentUrl { [NotNull] get; }

        string Title { [CanBeNull] get; }
        
         void ExecuteScript(string script, params object[] args);

        /// <summary>
        /// Refresh opened page
        /// </summary>
        public void Refresh();

        /// <summary>
        /// Navigate to URL
        /// </summary>
        /// <param name="url">URL</param>
        void Navigate([NotNull] string url);

        /// <summary>
        /// Remove focus from the element (for example text field or input)
        /// </summary>
        void RemoveFocusFromActiveElement();

        /// <summary>
        /// Close active element
        /// </summary>
        void CloseActiveElement();

        /// <summary>
        /// Print current page layout into console
        /// </summary>
        void DebugDumpPage();

        /// <summary>
        /// Open relative URL
        /// </summary>
        /// <param name="url">Relative URL</param>
        void NavigateToRelativeUrl(string url);

        /// <summary>
        /// Saves browser console log
        /// </summary>
        /// <returns>Path to saved log</returns>
        string SaveBrowserLog();
        
        /// <summary>
        /// Make screenshot
        /// </summary>
        /// <returns>Name of the screenshot file</returns>
        string MakeScreenShot();
        
        /// <summary>
        /// Change size of opened browser window
        /// </summary>
        /// <param name="Height">Desired height</param>
        /// <param name="Width">Desired width</param>
        void ResizeWindow(int Height, int Width);
        
        /// <summary>
        /// Switch to the last opened window
        /// </summary>
        void SwitchToTheLastWindow();

        /// <summary>
        /// Switch to the window which was opened first
        /// </summary>
        void SwitchToTheFirstWindow();
    }
}