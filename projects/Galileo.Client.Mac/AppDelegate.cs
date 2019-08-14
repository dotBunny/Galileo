using AppKit;
using Foundation;
using System;
using System.Reflection;
using System.Threading;

namespace Galileo.Client.Mac
{
    /// <summary>
    /// Application Delegate
    /// </summary>
    [Register("AppDelegate")]
    public partial class AppDelegate : NSApplicationDelegate
    {

        #region Fields

        /// <summary>
        /// Reference to the main window used by the application
        /// </summary>
        MainWindowController _mainWindow;

        /// <summary>
        /// Updates Provider
        /// </summary>
        Update.UpdateProvider _updates;

        #endregion

        #region Events

        /// <summary>
        /// Handle OS Level Cut Event
        /// </summary>
        /// <param name="sender">Native Object</param>
        [Action("cut:")]
        void Cut(NSObject sender)
        {
            if (_mainWindow == null) return;
            _mainWindow.OnCutEvent();
        }

        /// <summary>
        /// Handle OS Level Copy Event
        /// </summary>
        /// <param name="sender">Native Object</param>
        [Action("copy:")]
        void Copy(NSObject sender)
        {
            if (_mainWindow == null) return;
            _mainWindow.OnCopyEvent();
        }

        /// <summary>
        /// Event fired when finished loading the application framework
        /// </summary>
        /// <param name="notification">OS Notification</param>
        public override void DidFinishLaunching(NSNotification notification)
        {
            // Start up Galileo
            Instance.Initialize(
                new Core.HunterProfile(NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString(),
                                       Assembly.GetAssembly(typeof(Core.HunterConfig)).GetName().Version,
                                       Assembly.GetAssembly(typeof(Settings)).GetName().Version,
                                       Assembly.GetAssembly(typeof(MainWindowController)).GetName().Version,
                                       Settings.Localization));

            // Create our window
            CreateMainWindow();
        }

        /// <summary>
        /// Menu About Delegate
        /// </summary>
        /// <param name="sender">Sender.</param>
        partial void OnMenuAbout(NSMenuItem sender)
        {
            if (_mainWindow == null || _mainWindow.Window == null)
            {
                CreateMainWindow();
            }
            _mainWindow.SetWindowState(Instance.State.About);
        }

        /// <summary>
        /// Menu Preferences Delegate
        /// </summary>
        /// <param name="sender">Sender.</param>
        partial void OnMenuPreferences(NSMenuItem sender)
        {
            if (_mainWindow == null || _mainWindow.Window == null)
            {
                CreateMainWindow();
            }
            _mainWindow.SetWindowState(Instance.State.Preferences);
        }

        /// <summary>
        /// Menu Check For Updates Delegate
        /// </summary>
        /// <param name="sender">Sender.</param>
        partial void OnMenuCheckForUpdates(NSMenuItem sender)
        {
            if (_mainWindow == null || _mainWindow.Window == null)
            {
                CreateMainWindow();
            }

            CheckForUpdates(false, true);
            _mainWindow.SetWindowState(Instance.State.Updates);
        }

        /// <summary>
        /// Update Found Callback
        /// </summary>
        void OnUpdateFound()
        {
            InvokeOnMainThread(() =>
            {
                // Update main window with updates
                _mainWindow.SetUpdateState(true, _updates);
            });
        }

        /// <summary>
        /// Handle OS Level Paste Event
        /// </summary>
        /// <param name="sender">Native Object</param>
        [Action("paste:")]
        void Paste(NSObject sender)
        {
            if (_mainWindow == null) return;
            _mainWindow.OnPasteEvent();
        }

        /// <summary>
        /// Will the application become active, an event fired when clicking the dock icon, or the window itself
        /// </summary>
        /// <param name="notification">OS Notification</param>
        public override void WillBecomeActive(NSNotification notification)
        {
            // Check if we've got a window, if not make one
            if (_mainWindow == null || _mainWindow.Window == null)
            {
                CreateMainWindow();
            }
            else
            {
                _mainWindow.Window.MakeKeyAndOrderFront(this);
            }
        }

        /// <summary>
        /// Application termination pre-event
        /// </summary>
        /// <param name="notification">OS Notification</param>
        public override void WillTerminate(NSNotification notification)
        {
            Instance.Shutdown();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Check for updates to the application
        /// </summary>
        /// <param name="useIgnore">Should we ignore a previously ignored version?</param>
        public void CheckForUpdates(bool useIgnore = true, bool force = false)
        {
            // Check if we have a provider made yet
            if (_updates == null)
            {
                _updates = new Update.UpdateProvider();
                _updates.OnUpdateFound += OnUpdateFound;
            }

			ThreadStart start = () =>
			   {
				   // Check for update
				   if (useIgnore)
				   {
					   _updates.Check(Settings.IgnoreVersion, force);
				   }
				   else
				   {
					   _updates.Check(string.Empty, force);
				   }
			   };
			Thread thread = new Thread(start);
            thread.Start();         
        }

        /// <summary>
        /// Properly quit the application
        /// </summary>
        /// <remarks>This will trigger the terminate delegate</remarks>
        public void Quit()
        {
            NSApplication.SharedApplication.Terminate(NSApplication.SharedApplication);
        }

        /// <summary>
        /// Create the main Galileo window
        /// </summary>
        void CreateMainWindow()
        {
            Instance.Log("Client.Mac.AppDelegate.CreateMainWindow", "Initializing Window ...");

            // Create window instance
            _mainWindow = new MainWindowController();
            _mainWindow.OnAwake += OnMainWindowAwake;
        }

        /// <summary>
        /// Triggered when the main window has loaded.
        /// </summary>
        void OnMainWindowAwake()
        {
            // Initialize update backend
            CheckForUpdates();
        }

        #endregion
    }
}