using AppKit;
using Foundation;

namespace Galileo.Client.Mac
{
    /// <summary>
    /// Main Window Delegate
    /// </summary>
    public class MainWindowDelegate : NSWindowDelegate
    {
        /// <summary>
        /// Cached Window Reference
        /// </summary>
        readonly MainWindowController _window;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Client.Mac.MainWindowDelegate"/> class.
        /// </summary>
        /// <param name="windowController">Window Controller Reference</param>
        public MainWindowDelegate(MainWindowController windowController)
        {
            _window = windowController;
        }

        #region Events

        /// <summary>
        /// Event fired when the window resizes
        /// </summary>
        /// <param name="notification">OS Notification</param>
        public override void DidEndLiveResize(NSNotification notification)
        {
            Settings.WindowSize = new Core.Types.Vector2<int>((int)_window.Window.Frame.Size.Width, (int)_window.Window.Frame.Size.Height);
        }

        /// <summary>
        /// Event fired when the window moves
        /// </summary>
        /// <param name="notification">OS Notification</param>
        public override void DidMove(NSNotification notification)
        {
            Settings.WindowLocation = new Core.Types.Vector2<int>((int)_window.Window.Frame.Location.X, (int)_window.Window.Frame.Location.Y);
        }

        #endregion
    }
}
