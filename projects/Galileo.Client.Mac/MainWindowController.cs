using System;
using Foundation;
using AppKit;
using Galileo.Client.Update;
using Galileo.Client.Screens;
using Galileo.Core;
using System.IO;
using System.Linq;
using I18NPortable;
using CoreFoundation;

namespace Galileo.Client.Mac
{
    /// <summary>
    /// Galileo Window Controller
    /// </summary>
    public partial class MainWindowController : NSWindowController, IClient
    {
        #region Fields

        /// <summary>
        /// Paste Board Identifier
        /// </summary>
        static readonly string[] s_pasteBoardTypes = { "NSStringPboardType" };

        /// <summary>
        /// Color - Light Blue
        /// </summary>
        readonly NSColor _colorLightBlue = NSColor.FromRgb(234, 246, 255);

        /// <summary>
        /// Washedo ut orange color for menu
        /// </summary>
        readonly NSColor _colorMenuOrange = NSColor.FromRgb(244, 169, 71);

        /// <summary>
        /// Custom font used for icons on the menu
        /// </summary>
        readonly NSFont _fontAwesomeMenuIcons = NSFont.FromFontName(FontAwesome.FontFamilyName, 28);

        /// <summary>
        /// Should the process button be animated?
        /// </summary>
        bool _animateProcessIcon = false;

        DispatchSource.Timer _animateProcessTimer;

        /// <summary>
        /// Data source used for the process' config table.
        /// </summary>
        /// <remarks>
        /// Not instanced and won't be GC'd due to inactivity.
        /// </remarks>
        ConfigDataSource _hunterConfigDataSource;

        /// <summary>
        /// The configuration storage used in the IDE before it is written out per process
        /// </summary>
        HunterConfig _hunterConfigTemp = Settings.DefaultConfig;

        /// <summary>
        /// The currently selected hunter handler's index
        /// </summary>
        string _huntIndex = "";

        /// <summary>
        /// Notification pair key used when sending notification on complete
        /// </summary>
        readonly NSString _notificationKey = new NSString("Run");

        /// <summary>
        /// The dynamic update icon to use in the menu
        /// </summary>
        string _updateIcon = Resources.UpdatesIconNone;

        /// <summary>
        /// Cached reference to the update link for the platform
        /// </summary>
        string _updateLinkCache = "";

        /// <summary>
        /// Are there any updates pending?
        /// </summary>
        bool _updatePending;

        /// <summary>
        /// Cached version existing/found information
        /// </summary>
        string _updateVersionCache = "";

        /// <summary>
        /// Action triggered when the window finishes waking up.
        /// </summary>
        public Action OnAwake;

        /// <summary>
        /// The applications root delegate
        /// </summary>
        public AppDelegate App
        {
            get
            {
                return (AppDelegate)NSApplication.SharedApplication.Delegate;
            }
        }

        /// <summary>
        /// The current view state of the window
        /// </summary>
        public Instance.State CurrentState { get; private set; }

        /// <summary>
        /// The framed window reference
        /// </summary>
        /// <value>The window</value>
        public new MainWindow Window 
        { 
            get 
            { 
                return (MainWindow)base.Window; 
            }
        }

        /// <summary>
        /// The OS level notification manager
        /// </summary>
        /// <value>The notification center</value>
        NSUserNotificationCenter _notificationCenter { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Client.Mac.MainWindowController"/> class.
        /// </summary>
        /// <param name="handle">Window Handle</param>
        public MainWindowController(IntPtr handle) : base(handle)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Client.Mac.MainWindowController"/> class.
        /// </summary>
        /// <param name="coder">Coder Reference</param>
        [Export("initWithCoder:")]
        public MainWindowController(NSCoder coder) : base(coder)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Client.Mac.MainWindowController"/> class.
        /// </summary>
        public MainWindowController() : base("MainWindow")
        {
        }

        #region Events

        /// <summary>
        /// Open the EULA when the "EULA" button is clicked
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void AboutEULAButton_Click(NSButton sender)
        {
            AboutScreen.EULAButton_Click();
        }

        /// <summary>
        /// Open the log folder when the "Logs" button is clicked
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void AboutLogsButton_Click(NSButton sender)
        {
            AboutScreen.LogsButton_Click();
        }

        /// <summary>
        /// Open the Third Party Licenses when the "Third Party Licenses" button is clicked
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void AboutThirdPartyLicensesButton_Click(NSButton sender)
        {
            AboutScreen.ThirdPartyLicensesButton_Click();
        }

        /// <summary>
        /// Contact licensing server to validate provided license when "Activate" button is clicked on the Auth screen.
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void AuthActivateButton_Click(NSButton sender)
        {
            // Validate that the entered key is not empty
            if (authLicenseKeyText.StringValue != string.Empty)
            {
                if (LicensingScreen.ActivateButton_Click(authLicenseKeyText.StringValue))
                {
                    // Meaningless Messsage
                    authLicenseStatusText.StringValue = "Client.Auth.Activate.Valid".Translate();

                    // Licensed
                    SetLicensingState(true);

                    // Change view to preferences
                    SetWindowState(Instance.State.Hunt);

                    Alert("Client.Auth.Activate.ThankYou".Translate());
                }
                else
                {
                    Alert("Client.Auth.Activate.Exception".Translate(LicensingScreen.GetStatusMessage()));
                    SetLicensingState(false);
                }
            }
        }

        /// <summary>
        /// Open the purchase website when the "Purchase" button is clicked on the Auth screen.
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void AuthPurchaseButton_Click(NSButton sender)
        {
            LicensingScreen.PurchaseButton_Click();
        }

        /// <summary>
        /// Show the Activate screen when the "Activate" button is clicked in the menu
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void MenuActivateButton_Click(NSButton sender)
        {
            SetWindowState(Instance.State.Licensing);
        }

        /// <summary>
        /// Show the About screen when the "Galileo Branding" is clicked in the menu
        /// </summary>
        /// <param name="sender">Galileo Logo</param>
        partial void MenuBranding_Click(NSButton sender)
        {
            SetWindowState(Instance.State.About);
        }

        /// <summary>
        /// Show the Preferences screen when the "Preferences" button is clicked in the menu
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void MenuPreferencesButton_Click(NSButton sender)
        {
            SetWindowState(Instance.State.Preferences);
        }

        /// <summary>
        /// Show the Process screen when the "Process" button is clicked in the menu
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void MenuProcessButton_Click(NSButton sender)
        {
            SetWindowState(Instance.State.Hunt);
        }

        /// <summary>
        /// Show the Updates screen when the "Updates" button is clicked in the menu
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void MenuUpdatesButton_Click(NSButton sender)
        {
            SetWindowState(Instance.State.Updates);
        }

        /// <summary>
        /// Force an update when "Check For Updates" is clicked on the No Update screen
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void NoUpdatesCheckForUpdatesButton_Click(NSButton sender)
        {
            UpdatesScreen.CheckForUpdatesButton_Click();

            noUpdateIgnoreVersionText.StringValue = string.Empty;
            App.CheckForUpdates(false, true);
        }

        /// <summary>
        /// Native Copy Event
        /// </summary>
        public void OnCopyEvent()
        {
            switch (CurrentState)
            {
                // Copy System Information
                case Instance.State.About:
                    NSPasteboard.GeneralPasteboard.DeclareTypes(s_pasteBoardTypes, null);
                    NSPasteboard.GeneralPasteboard.SetStringForType(aboutSystemInformationText.String, s_pasteBoardTypes[0]);
                    break;
            }
        }

        /// <summary>
        /// Native Cut Event
        /// </summary>
        public void OnCutEvent()
        {
            switch (CurrentState)
            {
                // Copy System Information
                case Instance.State.About:
                    NSPasteboard.GeneralPasteboard.DeclareTypes(s_pasteBoardTypes, null);
                    NSPasteboard.GeneralPasteboard.SetStringForType(aboutSystemInformationText.String, s_pasteBoardTypes[0]);
                    break;
            }
        }

        /// <summary>
        /// Subscribed event called when a HuntHandler fires off it's completed action
        /// </summary>
        /// <param name="ID">The Hunt Handler identification</param>
        void OnHuntHandlerComplete(string ID)
        {
            // Open Report - Even In Background
            if (Settings.OpenReportsAutomatically)
            {
                Instance.Hunters[_huntIndex].OpenReport();
            }

            // Check to see if we should stop animating the progress wheel
            if(!Instance.IsWorking)
            {
                StopProcessAnimation();
            }

            if (_huntIndex != ID) return;

            InvokeOnMainThread(() =>
            {
                // Set Button Title
                processProcessButton.Title = "Client.Process.Process".Translate();

                // Reset Progress Bars
                processTotalProgressBar.DoubleValue = 100;

                // Hide them all
                processTotalProgressBar.Hidden = true;

                // Trigger a local notification after the time has elapsed
                var notification = new NSUserNotification
                {
                    // Add text and sound to the notification
                    Title = "Galileo".Translate(),
                    InformativeText = "Client.Process.Complete.Notification".Translate(),
                    SoundName = NSUserNotification.NSUserNotificationDefaultSoundName,
                    HasActionButton = true,
                    ActionButtonTitle = "Client.Process.Complete.NotificationAction".Translate(),
                    HasReplyButton = false
                };

                if (Instance.Hunters[_huntIndex].HasReport())
                {
                    
                    notification.UserInfo = NSDictionary.FromObjectsAndKeys(new string[] { _huntIndex }, new string[] { "Run" });
                    processReportButton.Enabled = true;
                }

                _notificationCenter.DeliverNotification(notification);

            });
        }

        /// <summary>
        /// Subscribed event called when a HuntHandler fires off it's log event action
        /// </summary>
        /// <param name="ID">The Hunt Handler identification</param>
        /// <param name="line">The log entry</param>
        void OnHuntHandlerLogEvent(string ID, string line)
        {
            if (_huntIndex != ID) return;

            InvokeOnMainThread(() =>
            {
                processLogText.TextStorage.Append(new NSAttributedString(line));

                var range = new NSRange(processLogText.String.Length, 0);
                processLogText.ScrollRangeToVisible(range);
            });
        }

        /// <summary>
        /// Subscribed event called when a HuntHandler fires off it's update action
        /// </summary>
        /// <param name="ID">The Hunt Handler identification</param>
        void OnHuntHandlerUpdate(string ID)
        {
            if (_huntIndex != ID) return;
            InvokeOnMainThread(() =>
            {
                processTotalProgressBar.DoubleValue = Instance.Hunters[_huntIndex].ProgressPercentage * 100;
            });
        }

        /// <summary>
        /// Native Paste Event
        /// </summary>
        public void OnPasteEvent()
        {
            switch (CurrentState)
            {
                // Paste Key
                case Instance.State.Licensing:
                    if (!LicensingScreen.IsGenuine() && NSPasteboard.GeneralPasteboard.GetStringForType(s_pasteBoardTypes[0]) != null)
                    {
                        authLicenseKeyText.StringValue = NSPasteboard.GeneralPasteboard.GetStringForType(s_pasteBoardTypes[0]);
                    }
                    break;
            }
        }

        /// <summary>
        /// Export to a file the default process config
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void PreferencesDefaultsExportButton_Click(NSButton sender)
        {
            // Pop File Same
            var dlg = NSSavePanel.SavePanel;
            dlg.Prompt = "Client.Preferences.Defaults.Export".Translate();

            dlg.NameFieldStringValue = "Galileo_ProcessOptions.json";
            dlg.AllowsOtherFileTypes = true;
            dlg.CanCreateDirectories = true;

            if (dlg.RunModal() == 1)
            {
                // TODO: VAlidate this
                Alert(PreferencesScreen.DefaultsExportButton_Click(dlg.Url.FilePathUrl.AbsoluteString));
            }
        }

        /// <summary>
        /// Import a file as the process config defaults
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void PreferencesDefaultsImportButton_Click(NSButton sender)
        {
            // Pop File Same
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.Prompt = "Client.Preferences.Defaults.Import".Translate();

            // Pop Dialog
            if (dlg.RunModal() == 1)
            {
                if (File.Exists(dlg.Url.FilePathUrl.AbsoluteString))
                {
                    // TODO: Validate this
                    Alert(PreferencesScreen.DefaultsImportButton_Click(dlg.Url.FilePathUrl.AbsoluteString));
                    SetWindowState(Instance.State.Preferences, true);
                }
            }

        }

        /// <summary>
        /// Restore defaults for the Defaults tab of preferences
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void PreferencesDefaultsRestoreDefaultsButton_Click(NSButton sender)
        {
            // Create Alert Sheet
            var alert = new NSAlert
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = "Client.Preferences.ResetMessage".Translate("Client.Preferences.Defaults".Translate()),
                MessageText = "Client.Preferences.Reset".Translate()
            };

            // Add buttons
            alert.AddButton("Client.Preferences.Reset.Yes".Translate());
            alert.AddButton("Client.Preferences.Reset.No".Translate());

            // Execute
            alert.BeginSheetForResponse(Window, (result) =>
            {

                if (result == 1000)
                {
                    PreferencesScreen.DefaultsRestoreDefaultsButton_Click();

                    // Refresh Preferences
                    SetWindowState(Instance.State.Preferences, true);
                }
            });
        }

        /// <summary>
        /// Open dialog to choose the default target folder
        /// </summary>
        /// <param name="sender">Path Reference</param>
        partial void PreferencesDefaultsTargetFolderPath_Click(NSPathControl sender)
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = false;
            dlg.CanChooseDirectories = true;
            dlg.Prompt = "Client.Preferences.Defaults.SelectFolder".Translate();

            if (dlg.RunModal() == 1)
            {
                PreferencesScreen.PreferencesDefaultsTargetFolderPath_Click(dlg.DirectoryUrl.Path);

                preferencesDefaultsTargetFolderPath.StringValue = dlg.DirectoryUrl.Path;
            }
        }

        /// <summary>
        /// Event triggered when preferences changes the localization
        /// </summary>
        /// <param name="sender">Menu sender</param>
		partial void PreferencesGeneralLocaleCombo_Changed(NSPopUpButton sender)
		{
            PreferencesScreen.SetLocale(Localization.LocalizationProvider.GetLocaleIndex(sender.SelectedItem.Title), this);
		}


        /// <summary>
        /// Set if reports should be opened automatically when processing finishes
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void PreferencesGeneralReportAutomaticOpenCheckButton_Click(NSButton sender)
        {
            PreferencesScreen.GeneralReportAutomaticOpenCheckButton_Click(sender.State == NSCellStateValue.On);
        }

        /// <summary>
        /// Restore defaults for the General tab of preferences
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void PreferencesGeneralRestoreDefaultsButton_Click(NSButton sender)
        {
            // Create Alert Sheet
            var alert = new NSAlert
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = "Client.Preferences.ResetMessage".Translate("Client.Preferences.General".Translate()),
                MessageText = "Client.Preferences.Reset".Translate()
            };

            // Add buttons
            alert.AddButton("Client.Preferences.Reset.Yes".Translate());
            alert.AddButton("Client.Preferences.Reset.No".Translate());

            // Execute
            alert.BeginSheetForResponse(Window, (result) =>
            {

                if (result == 1000)
                {
                    PreferencesScreen.GeneralRestoreDefaultsButton_Click(this);
                }
            });
        }
        /// <summary>
        /// Set the sending of usage data from the Preferences screen
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void PreferencesGeneralSendUsageDataButton_Click(NSButton sender)
        {
            PreferencesScreen.GeneralSendUsageDataButton_Click(sender.State == NSCellStateValue.On);
        }

        /// <summary>
        /// Open the preferences overview when the "Help" button is clicked
        /// </summary>
        /// <param name="sender">Buttn Reference</param>
        partial void PreferencesHelpButton_Click(NSButtonCell sender)
        {
            PreferencesScreen.HelpButton_Click();
        }

        /// <summary>
        /// Deactivate the currently activate license
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void PreferencesLicenseDeactivateButton_Click(NSButton sender)
        {
            // Create Alert Sheet
            var alert = new NSAlert
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = "Client.Preferences.License.DialogMessage".Translate(),
                MessageText = "Client.Preferences.License.DialogTitle".Translate()
            };

            // Add buttons
            alert.AddButton("Client.Preferences.License.Yes".Translate());
            alert.AddButton("Client.Preferences.License.No".Translate());

            // Execute
            alert.BeginSheetForResponse(Window, (result) =>
            {

                if (result == 1000)
                {
                    if (PreferencesScreen.LicenseDeactivateButton_Click())
                    {
                        SetLicensingState(false);
                        SetWindowState(Instance.State.Licensing, true);
                    }
                    else
                    {
                        Alert("Client.Preferences.License.ErrorMessage".Translate(LicensingScreen.GetStatusMessage()));
                    }
                }
            });
        }

        /// <summary>
        /// Indicate that the "beta" channel should be used for updates
        /// </summary>
        /// <param name="sender">Menu Item Reference</param>
        partial void PreferencesUpdatesChannelBetaMenuItem_Click(NSMenuItem sender)
        {
            preferencesUpdateUpdateChannelMenuButton.SelectItem(preferencesUpdatesChannelBetaMenuItem);

            PreferencesScreen.SetUpdateChannel(UpdateProvider.Channel.Beta);
        }

        /// <summary>
        /// Indicate that the "release" channel should be used for updates
        /// </summary>
        /// <param name="sender">Menu Item Reference</param>
        partial void PreferencesUpdatesChannelReleaseMenuItem_Click(NSMenuItem sender)
        {
            preferencesUpdateUpdateChannelMenuButton.SelectItem(preferencesUpdatesChannelReleaseMenuItem);

            PreferencesScreen.SetUpdateChannel(UpdateProvider.Channel.Release);
        }

        /// <summary>
        /// Inidicate that "daily" checks for update should be made
        /// </summary>
        /// <param name="sender">Menu Item Reference</param>
        partial void PreferencesUpdatesCheckFrequencyDailyMenuItem_Click(NSMenuItem sender)
        {
            preferencesUpdatesCheckFrequencyButton.SelectItem(preferencesUpdatesCheckFrequencyDailyMenuItem);

            PreferencesScreen.SetUpdateFrequency(UpdateProvider.Frequency.Daily);
        }

        /// <summary>
        /// Inidicate that "monthly" checks for update should be made
        /// </summary>
        /// <param name="sender">Menu Item Reference</param>
        partial void PreferencesUpdatesCheckFrequencyMonthlyMenuItem_Click(NSMenuItem sender)
        {
            preferencesUpdatesCheckFrequencyButton.SelectItem(preferencesUpdatesCheckFrequencyMonthlyMenuItem);

            PreferencesScreen.SetUpdateFrequency(UpdateProvider.Frequency.Monthly);
        }

        /// <summary>
        /// Inidicate that "weekly" checks for update should be made
        /// </summary>
        /// <param name="sender">Menu Item Reference</param>
        partial void PreferencesUpdatesCheckFrequencyWeeklyMenuItem_Click(NSMenuItem sender)
        {
            preferencesUpdatesCheckFrequencyButton.SelectItem(preferencesUpdatesCheckFrequencyWeeklyMenuItem);

            PreferencesScreen.SetUpdateFrequency(UpdateProvider.Frequency.Weekly);
        }

        /// <summary>
        /// Inidicate that update checks should not happen
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void PreferencesUpdatesEnableNoButton_Click(NSButton sender)
        {
            preferencesUpdatesEnableYesButton.State = NSCellStateValue.Off;
            preferencesUpdatesEnableNoButton.State = NSCellStateValue.On;

            PreferencesScreen.SetCheckForUpdates(false);
        }

        /// <summary>
        /// Inidicate that update checks should happen
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void PreferencesUpdatesEnableYesButton_Click(NSButton sender)
        {
            preferencesUpdatesEnableYesButton.State = NSCellStateValue.On;
            preferencesUpdatesEnableNoButton.State = NSCellStateValue.Off;

            PreferencesScreen.SetCheckForUpdates(true);
        }

        /// <summary>
        /// Restore the Update preferences to their default values
        /// </summary>
        /// <param name="sender">Sender.</param>
        partial void PreferencesUpdatesRestoreDefaultsButton_Click(NSButton sender)
        {
            // Create Alert Sheet
            var alert = new NSAlert
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = "Client.Preferences.ResetMessage".Translate("Client.Preferences.Updates".Translate()),
                MessageText = "Client.Preferences.Reset".Translate()
            };

            // Add buttons
            alert.AddButton("Client.Preferences.Reset.Yes".Translate());
            alert.AddButton("Client.Preferences.Reset.No".Translate());

            // Execute
            alert.BeginSheetForResponse(Window, (result) =>
            {

                if (result == 1000)
                {
                    PreferencesScreen.UpdatesRestoreDefaultsButton_Click();

                    // Refresh Preferences
                    SetWindowState(Instance.State.Preferences, true);
                }
            });
        }


        /// <summary>
        /// Instructs the currently selected Process to begin
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void ProcessProcessButton_Click(NSButton sender)
        {
            // Disable input while were dealing with all this because of threading
            processProcessButton.Enabled = false;


            if (!ProcessScreen.ProcessButton_Click(_huntIndex))
            {
                processTotalProgressBar.Hidden = true;
                processTotalProgressBar.DoubleValue = 0;
                processTargetPath.Enabled = true;
                processProcessButton.Title = "Client.Process.Process".Translate();

                if ( !Instance.IsWorking ) {
                    StopProcessAnimation();
                }
            }
            else
            {
                // Clear Log
                processLogText.Value = string.Empty;
                processTotalProgressBar.Hidden = false;
                processTotalProgressBar.DoubleValue = 0;
                processTargetPath.Enabled = false;
                processProcessButton.Title = "Client.Process.Cancel".Translate();

                StartProcessAnimation();
            }

            // Reenable input after everything is setup
            processProcessButton.Enabled = true;
        }

        /// <summary>
        /// Open any report that is present for the currently selected report
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void ProcessReportButton_Click(NSButton sender)
        {
            ProcessScreen.ReportButton_Click(_huntIndex);
        }

        /// <summary>
        /// Show the correct tab on the Process screen when selected.
        /// </summary>
        /// <param name="sender">Selector Reference</param>
        partial void ProcessSegments_Click(NSSegmentedCell sender)
        {
            if (processSegments.SelectedSegment == 1)
            {
                processTabs.Select(processOptionsTab);
            }
            else
            {
                processTabs.Select(processLogTab);
            }
        }

        /// <summary>
        /// Display a dialogue to select the current process' target folder
        /// </summary>
        /// <param name="sender">Sender.</param>
        partial void ProcessTargetPath_Click(NSPathControl sender)
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = false;
            dlg.CanChooseDirectories = true;
            dlg.Prompt = "Client.Process.SelectFolder".Translate();

            if (dlg.RunModal() == 1)
            {
				Instance.Hunters[_huntIndex].UpdateWorkingDirectory(dlg.DirectoryUrl.Path);
				sender.StringValue = Instance.Hunters[_huntIndex].WorkingDirectory;

                // Update the config table for the process
                UpdateHuntConfigTable(_huntIndex);

                // Check if path has a report?
                if (Instance.Hunters[_huntIndex].HasReport())
                {
                    processReportButton.Enabled = true;
                }
                else
                {
                    processReportButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Open the download link when "Download" is clicked on the Update screen
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void UpdateDownloadButton_Click(NSButton sender)
        {
            UpdatesScreen.DownloadButton_Click(_updateLinkCache);
        }

        /// <summary>
        /// Ignore the update when "Ignore" is clicked on the Update screen.
        /// </summary>
        /// <param name="sender">Button Reference</param>
        partial void UpdateIgnoreButton_Click(NSButton sender)
        {
            UpdatesScreen.IgnoreButton_Click(_updateVersionCache);

            _updatePending = false;
            SetWindowState(Instance.State.Updates, true);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Window's Awake Event
        /// </summary>
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            Instance.Log("Client.Mac.MainWindowController.AwakeFromNib", "A monster in the deep stirs ...");

            // Localize window
            Localize();

            // Legacy support for < 10.13
            Window.Title = "Galileo.Version".Translate(Instance.Profile.PackageVersion);
            Window.TitlebarAppearsTransparent = true;
            Window.TitleVisibility = NSWindowTitleVisibility.Hidden;
            Window.StyleMask |= NSWindowStyle.FullSizeContentView;

            // Window Sizing
            Types.Vector2<int> size = Settings.WindowSize;
            Types.Vector2<int> location = Settings.WindowLocation;
            if (location.X == -1 && location.Y == -1)
            {
                Window.SetFrame(
                    new CoreGraphics.CGRect(
                        new CoreGraphics.CGPoint(
                            ((Window.Screen.Frame.Size.Width / 2f) - (size.X / 2f)),
                            ((Window.Screen.Frame.Size.Height / 2f) - (size.Y / 2f))
                        ),
                        new CoreGraphics.CGSize(size.X, size.Y)
                    ), true, false);
            }
            else
            {
                Window.SetFrame(
                   new CoreGraphics.CGRect(
                       new CoreGraphics.CGPoint(location.X, location.Y),
                       new CoreGraphics.CGSize(size.X, size.Y)
                   ), true, false);
                
            }

            // Assign delegate late allowing for moving before it is triggered
            Window.Delegate = new MainWindowDelegate(this);
            Instance.Log("Client.Mac.MainWindowController.AwakeFromNib", "Window Framed ...");

            // Create our default working hunter
            _huntIndex = HuntHandler.CreateID();
            Instance.Hunters.Add(_huntIndex, new HuntHandler(_huntIndex, Instance.Profile));
            Instance.Hunters[_huntIndex].OnProcessUpdate += OnHuntHandlerUpdate;
            Instance.Hunters[_huntIndex].OnProcessComplete += OnHuntHandlerComplete;
            Instance.Hunters[_huntIndex].OnProcessLogEvent += OnHuntHandlerLogEvent;
            Instance.Log("Client.Mac.MainWindowController.AwakeFromNib", "Hunter Initialized ...");

            // Assign version string
            menuVersionText.StringValue = Instance.Profile.PackageVersion;

            // Assign custom embeded fonts
            menuPreferencesButtonIconText.Font = _fontAwesomeMenuIcons;
            menuProcessButtonIconText.Font = _fontAwesomeMenuIcons;
            menuActivateButtonIconText.Font = _fontAwesomeMenuIcons;
            menuUpdatesButtonIconText.Font = _fontAwesomeMenuIcons;

            // Populate locale list
			preferencesGeneralLocaleCombo.AddItems(Localization.LocalizationProvider.SupportedLocalesDescription);

            // Check licensing - already initialized in Instance, this just shows right screen
            if (LicensingScreen.IsGenuine())
            {
                Instance.Log("Client.Mac.MainWindowController.AwakeFromNib", "Product Activated");
                SetLicensingState(true);
                SetWindowState(Instance.State.Hunt);
            }
            else
            {
                Instance.Log("Client.Mac.MainWindowController.AwakeFromNib", "Product NOT Activated");
                SetLicensingState(false);
                SetWindowState(Instance.State.Licensing);
            }

            // TOOD: Make window movable by clicking on background elements
            menuBackgroundImage.Window.MovableByWindowBackground = true;
            screenBackgroundImage.Window.MovableByWindowBackground = true;

            // Tell notiication center to display notifications even if the app is topmost
            _notificationCenter = NSUserNotificationCenter.DefaultUserNotificationCenter;
            _notificationCenter.ShouldPresentNotification = (c, n) => true;
            _notificationCenter.DidActivateNotification += (s, e) =>
            {
                switch (e.Notification.ActivationType)
                {
                    case NSUserNotificationActivationType.ActionButtonClicked:
                        if (e.Notification.UserInfo.ContainsKey(_notificationKey))
                        {
                            ProcessScreen.ReportButton_Click(e.Notification.UserInfo.ValueForKey(_notificationKey).ToString());
                        }
                        break;
                }
            };


            // Foce update of thigns
            SetHuntIndex(_huntIndex, true);

            // Move the window to the front because thats just how we roll
            Window.MakeKeyAndOrderFront(this);

            Instance.Log("Client.Mac.MainWindowController.AwakeFromNib", "Galileo Ready.");

            // Call OnAwake
            OnAwake?.Invoke();
        }
        
        /// <summary>
        /// Localize Window
        /// </summary>
        public void Localize()
        {
			// Cache current setting
			Instance.State stateCache = CurrentState;

            // Blank state
			SetWindowState(Instance.State.Blank, true);         

            // Menu
            menuProcessButtonText.StringValue = "Client.Menu.Process".Translate();
            menuSystemSectionText.StringValue = "Client.Menu.System".Translate().ToUpper();
            menuActivateButtonText.StringValue = "Client.Menu.Activate".Translate();
            menuPreferencesButtonText.StringValue = "Client.Menu.Preferences".Translate();
            menuUpdatesButtonText.StringValue = "Client.Menu.Updates".Translate();

            // About Tab
            aboutTitleText.StringValue = "Client.About.Title".Translate();
            aboutMessageText.StringValue = "Client.About.Message".Translate();
            aboutClientInfoText.StringValue = "Client.About.ClientInfo".Translate();
            aboutSystemInfoText.StringValue = "Client.About.SysInfo".Translate();
            aboutThirdPartyLicensesButton.Title = "Client.About.ThirdPartyLicenses".Translate();
            aboutEULAButton.Title = "Client.About.EULA".Translate();
            aboutLogsButton.Title = "Client.About.Logs".Translate();

            // Auth Tab
            authTitleText.StringValue = "Client.Auth.Title".Translate();
            authMessageText.StringValue = "Client.Auth.Message".Translate();
            authLicenseKeyText.PlaceholderString = "Client.Auth.PlaceHolder".Translate();
            authPriceMessageText.StringValue = "Client.Auth.PriceMessage".Translate();
            authPurchaseButton.Title = "Client.Auth.Purchase".Translate();
            authActivateButton.Title = "Client.Auth.Activate".Translate();

            // Process Tab
            processTitleText.StringValue = "Client.Process.Title".Translate();
            processMessageText.StringValue = "Client.Process.Message".Translate();
            processReminderText.StringValue = "Client.Process.Reminder".Translate();
            processSegments.SetLabel("Client.Process.Log".Translate(), 0);
            processSegments.SetLabel("Client.Process.Options".Translate(), 1);
            processProcessButton.Title = "Client.Process.Process".Translate();
            processReportButton.Title = "Client.Process.Report".Translate();
            processTableSettingColumn.Title = "Client.Process.ConfigTable.Setting".Translate();
            processTableValueColumn.Title = "Client.Process.ConfigTable.Value".Translate();
            processTableDescriptionColumn.Title = "Client.Process.ConfigTable.Description".Translate();                  

            // Updates Tab
            updateIgnoreButton.Title = "Client.Updates.Ignore".Translate();
            updateDownloadButton.Title = "Client.Updates.Download".Translate();

            // No Updates Tab
            noUpdateCheckForUpdatesButton.Title = "Client.NoUpdate.CheckForUpdates".Translate();
            noUpdateTitleText.StringValue = "Client.NoUpdate.Title".Translate();
            noUpdateMessageText.StringValue = "Client.NoUpdate.Message".Translate();
            noUpdateNextUpdateCheckText.StringValue = UpdatesScreen.GetNextCheckText();

            // Preferences Tab
            preferencesTitleText.StringValue = "Client.Preferences.Title".Translate();
            preferencesGeneralTab.Label = "Client.Preferences.General".Translate();
            preferencesGeneralMessageText.StringValue = "Client.Preferences.General.Message".Translate();
            preferencesGeneralLocaleText.StringValue = "Client.Preferences.General.Locale".Translate();

            preferencesGeneralRestoreDefaultsButton.Title = "Client.Preferences.RestoreDefaults".Translate();
            preferencesGeneralReportAutomaticOpenButton.Title = "Client.Preferences.General.OpenReports".Translate();
            preferencesGeneralSendUsageDataButton.Title = "Client.Preferences.General.SendData".Translate();
            preferencesGeneralDataExplanationText.StringValue = "Client.Preferences.Genearl.SendDataMessage".Translate();

            preferencesProcessTab.Label = "Client.Preferences.Defaults".Translate();
            preferencesDefaultsMessageText.StringValue = "Client.Preferences.Defaults.Message".Translate();
            preferencesDefaultsTargetFolderText.StringValue = "Client.Preferences.Defaults.TargetFolder".Translate();
            preferencesDefaultsImportButton.Title = "Client.Preferences.Defaults.Import".Translate();
            preferencesDefaultsExportButton.Title = "Client.Preferences.Defaults.Export".Translate();
            preferencesDefaultsRestoreDefaultsButton.Title = "Client.Preferences.RestoreDefaults".Translate();
            preferencesDefaultsTableSettingsColumn.Title = "Client.Process.ConfigTable.Setting".Translate();
            preferencesDefaultsTableSettingsDefaultValueColumn.Title = "Client.Preferences.Defaults.ConfigTable.DefaultValue".Translate();
            preferencesDefaultsTableSettingsDescriptionColumn.Title = "Client.Process.ConfigTable.Description".Translate();         

            preferencesUpdatesTab.Label = "Client.Preferences.Updates".Translate();
            preferencesUpdatesMessageText.StringValue = "Client.Preferences.Updates.Message".Translate();
            preferencesUpdatesEnableText.StringValue = "Client.Preferences.Updates.Enable".Translate();
            preferencesUpdatesEnableYesButton.Title = "Client.Preferences.Updates.Yes".Translate();
            preferencesUpdatesEnableNoButton.Title = "Client.Preferences.Updates.No".Translate();

            preferencesUpdateCheckFrequencyText.StringValue = "Client.Preferences.Updates.CheckFrequency".Translate();

            preferencesUpdatesCheckFrequencyDailyMenuItem.Title = "Client.Preferences.Updates.CheckFrequency.Daily".Translate();
            preferencesUpdatesCheckFrequencyWeeklyMenuItem.Title = "Client.Preferences.Updates.CheckFrequency.Weekly".Translate();
            preferencesUpdatesCheckFrequencyMonthlyMenuItem.Title = "Client.Preferences.Updates.CheckFrequency.Monthly".Translate();

            preferencesUpdateChannelText.StringValue = "Client.Preferences.Updates.Channel".Translate();

            preferencesUpdatesChannelBetaMenuItem.Title = "Client.Preferences.Updates.Channel.Beta".Translate();
            preferencesUpdatesChannelReleaseMenuItem.Title = "Client.Preferences.Updates.Channel.Release".Translate();

            preferencesUpdateRestoreDefaultsButton.Title = "Client.Preferences.RestoreDefaults".Translate();

            preferencesLicenseTab.Label = "Client.Preferences.License".Translate();
            preferencesLicenseMessageText.StringValue = "Client.Preferences.License.Message".Translate();
            preferencesLicenseDeactivateButton.Title = "Client.Preferences.License.DeactivateInstall".Translate();

            // Reset state
			SetWindowState(stateCache, true);

            Instance.Log("Client.Mac.MainWindowController.Localize", "Localized to " + Localization.LocalizationProvider.GetCulture());
        }

        /// <summary>
        /// Set the update state for Galileo
        /// </summary>
        /// <param name="found">Was an update found?</param>
        /// <param name="update">The encapsulated update provider</param>
        public void SetUpdateState(bool found, UpdateProvider update)
        {
            _updatePending = found;

            if (_updatePending)
            {
                // Update information about the update
                updateChangelogTextView.Value = update.Changelog;
                updateTitleText.StringValue = "Galileo.Version".Translate(update.Current.Version);
                updateMessageText.StringValue = update.Specification.Message;
                updateDateText.StringValue = update.Specification.ReleaseDate;

                // Change Icon To Warning
                _updateIcon = Resources.UpdatesIconFound;

                // Cache link/version
                _updateLinkCache = update.Specification.MacLink;
                _updateVersionCache = update.Current.Version;

                // Update the window OR the icon depending
                if (CurrentState == Instance.State.Updates)
                {
                    // Forcably refresh the current window to new licensing
                    SetWindowState(Instance.State.Updates, true);
                }
                else
                {
                    menuUpdatesButtonIconText.StringValue = FontAwesome.FAExclamationTriangle;
                }
            }
        }

        /// <summary>
        /// Set the state of the window (the screen)
        /// </summary>
        /// <returns>Was the window state updated?</returns>
        /// <param name="newState">The state which should be changed too</param>
        /// <param name="forceRefresh">Should the update befored?</param>
        public bool SetWindowState(Instance.State newState, bool forceRefresh = false)
        {
            // Check if we are already viewing the state, or if its a forced refresh
            if (CurrentState == newState && !forceRefresh) return false;

            switch (newState)
            {
                case Instance.State.Hunt:

                    // Update Menu
                    SetMenuButtonState(menuActivateButtonOverlayImage, menuActivateButtonText, menuActivateButtonIconText,Resources.ActivateIcon);
                    SetMenuButtonState(menuProcessButtonOverlayImage, menuProcessButtonText, menuProcessButtonIconText, Resources.ProcessIcon, true);
                    SetMenuButtonState(menuPreferencesButtonOverlayImage, menuPreferencesButtonText, menuPreferencesButtonIconText, Resources.PreferencesIcon);
                    SetMenuButtonState(menuUpdatesButtonOverlayImage, menuUpdatesButtonText, menuUpdatesButtonIconText, _updateIcon);

                    SetHuntIndex(_huntIndex, true);

                    if (processTabs.Selected == processLogTab)
                    {
                        processSegments.SetSelected(true, 0);
                        processSegments.SetSelected(false, 1);
                    }
                    else
                    {
                        processSegments.SetSelected(false, 0);
                        processSegments.SetSelected(true, 1);
                    }

                    screenTabs.Select(processTab);
                    CurrentState = Instance.State.Hunt;
                    break;

                case Instance.State.Preferences:

                    // Update Menu 
                    SetMenuButtonState(menuActivateButtonOverlayImage, menuActivateButtonText, menuActivateButtonIconText, Resources.ActivateIcon);
                    SetMenuButtonState(menuProcessButtonOverlayImage, menuProcessButtonText, menuProcessButtonIconText, Resources.ProcessIcon);
                    SetMenuButtonState(menuPreferencesButtonOverlayImage, menuPreferencesButtonText, menuPreferencesButtonIconText, Resources.PreferencesIcon, true);
                    SetMenuButtonState(menuUpdatesButtonOverlayImage, menuUpdatesButtonText, menuUpdatesButtonIconText, _updateIcon);

                    // Handle General Tab
                    if (Settings.OpenReportsAutomatically)
                    {
                        preferencesGeneralReportAutomaticOpenButton.State = NSCellStateValue.On;
                    }
                    else
                    {
                        preferencesGeneralReportAutomaticOpenButton.State = NSCellStateValue.Off;
                    }

                    if (Settings.AnonymousUsageStats)
                    {
                        preferencesGeneralSendUsageDataButton.State = NSCellStateValue.On;
                    }
                    else
                    {
                        preferencesGeneralSendUsageDataButton.State = NSCellStateValue.Off;
                    }

                    // Handle Defaults Tab
                    preferencesDefaultsTargetFolderPath.StringValue = Settings.DefaultFolder;

                    // Select locale
                    var items = preferencesGeneralLocaleCombo.Items();
                    for (int i = 0; i < preferencesGeneralLocaleCombo.ItemCount; i++)
                    {
						if ( Localization.LocalizationProvider.SupportedLocales[i]== Settings.Localization ) {
                            preferencesGeneralLocaleCombo.SelectItem(items[i]);
                        }
                    }

                    // Load default settings into the table
                    var configSource = new ConfigDataSource(Settings.DefaultConfig);
                    configSource.OnChanged += delegate
                    {
                        Settings.DefaultConfig = ProcessConfig.FromDataSourceObjects(configSource.Items);
                    };
                    preferencesProcessConfigTable.DataSource = configSource;
                    preferencesProcessConfigTable.Delegate = new ConfigDelegate(configSource);

                    // Handle Updates Tab
                    if (Settings.ShouldCheckForUpdates)
                    {
                        preferencesUpdatesEnableYesButton.State = NSCellStateValue.On;
                        preferencesUpdatesEnableNoButton.State = NSCellStateValue.Off;
                    }
                    else
                    {
                        preferencesUpdatesEnableYesButton.State = NSCellStateValue.Off;
                        preferencesUpdatesEnableNoButton.State = NSCellStateValue.On;
                    }

                    switch (Settings.UpdateCheckFrequency)
                    {
                        case UpdateProvider.Frequency.Daily:
                            preferencesUpdatesCheckFrequencyButton.SelectItem(preferencesUpdatesCheckFrequencyDailyMenuItem);
                            break;
                        case UpdateProvider.Frequency.Weekly:
                            preferencesUpdatesCheckFrequencyButton.SelectItem(preferencesUpdatesCheckFrequencyWeeklyMenuItem);
                            break;
                        case UpdateProvider.Frequency.Monthly:
                            preferencesUpdatesCheckFrequencyButton.SelectItem(preferencesUpdatesCheckFrequencyMonthlyMenuItem);
                            break;
                    }

                    if (Settings.UpdatesChannel == UpdateProvider.Channel.Beta)
                    {
                        preferencesUpdateUpdateChannelMenuButton.SelectItem(preferencesUpdatesChannelBetaMenuItem);
                    }
                    else
                    {
                        preferencesUpdateUpdateChannelMenuButton.SelectItem(preferencesUpdatesChannelReleaseMenuItem);
                    }

                    // Select tab
                    screenTabs.Select(preferencesTab);

                    // Set the state
                    CurrentState = Instance.State.Preferences;

                    break;

                case Instance.State.Updates:

                    // Update Menu
                    SetMenuButtonState(menuActivateButtonOverlayImage, menuActivateButtonText, menuActivateButtonIconText, Resources.ActivateIcon);
                    SetMenuButtonState(menuProcessButtonOverlayImage, menuProcessButtonText, menuProcessButtonIconText, Resources.ProcessIcon);
                    SetMenuButtonState(menuPreferencesButtonOverlayImage, menuPreferencesButtonText, menuPreferencesButtonIconText, Resources.PreferencesIcon);
                    SetMenuButtonState(menuUpdatesButtonOverlayImage, menuUpdatesButtonText, menuUpdatesButtonIconText, _updateIcon, true);

                    if (_updatePending)
                    {
                        screenTabs.Select(updateTab);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Settings.IgnoreVersion))
                        {
                            noUpdateIgnoreVersionText.StringValue = "Client.NoUpdate.IgnoredVersion".Translate(Settings.IgnoreVersion);
                        }
                        else
                        {
                            noUpdateIgnoreVersionText.StringValue = string.Empty;
                        }

                        // Update last check string
                        noUpdateNextUpdateCheckText.StringValue = UpdatesScreen.GetNextCheckText();

                        screenTabs.Select(noUpdateTab);
                    }

                    CurrentState = Instance.State.Updates;
                    break;

                case Instance.State.Licensing:

                    // Update Menu
                    SetMenuButtonState(menuActivateButtonOverlayImage, menuActivateButtonText, menuActivateButtonIconText, Resources.ActivateIcon, true);
                    SetMenuButtonState(menuProcessButtonOverlayImage, menuProcessButtonText, menuProcessButtonIconText, Resources.ProcessIcon);
                    SetMenuButtonState(menuPreferencesButtonOverlayImage, menuPreferencesButtonText, menuPreferencesButtonIconText, Resources.PreferencesIcon);
                    SetMenuButtonState(menuUpdatesButtonOverlayImage, menuUpdatesButtonText, menuUpdatesButtonIconText, _updateIcon);

                    CurrentState = Instance.State.Licensing;

                    if (LicensingScreen.IsGenuine())
                    {
                        // Switch to preferences because wtf?
                        SetWindowState(Instance.State.Hunt);
                    }
                    else
                    {
                        screenTabs.Select(authTab);
                    }
                    break;

                case Instance.State.About:

                    // Update Menu (No Selection)
                    SetMenuButtonState(menuActivateButtonOverlayImage, menuActivateButtonText, menuActivateButtonIconText, Resources.ActivateIcon);
                    SetMenuButtonState(menuProcessButtonOverlayImage, menuProcessButtonText, menuProcessButtonIconText, Resources.ProcessIcon);
                    SetMenuButtonState(menuPreferencesButtonOverlayImage, menuPreferencesButtonText, menuPreferencesButtonIconText, Resources.PreferencesIcon);
                    SetMenuButtonState(menuUpdatesButtonOverlayImage, menuUpdatesButtonText, menuUpdatesButtonIconText, _updateIcon);

                    // Set Versions
                    aboutClientVersionText.StringValue = AboutScreen.ClientVersionString(Instance.Profile);
                    aboutClientLibraryVersionText.StringValue = AboutScreen.ClientLibraryVersionString(Instance.Profile);
                    aboutCoreLibraryVersionText.StringValue = AboutScreen.CoreLibraryVersionString(Instance.Profile);

                    // Set Debug Information
                    aboutSystemInformationText.Value = AboutScreen.DebugInformation();

                    // Show Tab
                    screenTabs.Select(aboutTab);
                    CurrentState = Instance.State.About;
                    break;

				case Instance.State.Blank:
					screenTabs.Select(blankTab);
					CurrentState = Instance.State.Blank;
					break;
            }

            return true;

        }

        /// <summary>
        /// Notify user via native alert
        /// </summary>
        /// <param name="message">The message to show on the alert</param>
        void Alert(string message)
        {
            // Force call to happen on the UI thread
            InvokeOnMainThread(() =>
            {

                // Create alert
                var alert = new NSAlert
                {
                    AlertStyle = NSAlertStyle.Informational,
                    MessageText = message
                };

                // Show alert
                alert.BeginSheet(Window);
            });

            Instance.Log("Client.Mac.MainWindowController.Alert", message);
        }

        /// <summary>
        /// Update the Process screen to have the proper process information
        /// </summary>
        /// <param name="index">Hunt Handler Index</param>
        /// <param name="force">Should this update be forced?</param>
        void SetHuntIndex(string index, bool force = false)
        {

            if (index == _huntIndex && !force)
            {
                return;
            }

            // Set Index
            _huntIndex = index;

            // Update Log
            processLogText.Value = string.Empty;
            foreach (string s in Instance.Hunters[_huntIndex].Log.ToArray().Reverse())
            {
                processLogText.TextStorage.Append(new NSAttributedString(s));
            }

            // Scroll
            var range = new NSRange(processLogText.String.Length, 0);
            processLogText.ScrollRangeToVisible(range);

            // Update target folder
            processTargetPath.StringValue = Instance.Hunters[_huntIndex].WorkingDirectory;

            // Update Config
            UpdateHuntConfigTable(_huntIndex);

            // Check if path has a report?
            if (Instance.Hunters[_huntIndex].HasReport())
            {
                processReportButton.Enabled = true;
            }
            else
            {
                processReportButton.Enabled = false;
            }

            // Hide things just incase
            if (Instance.Hunters[_huntIndex].IsWorking)
            {
                processTargetPath.Enabled = false;
                processTotalProgressBar.Hidden = false;
                processTotalProgressBar.DoubleValue = Instance.Hunters[_huntIndex].ProgressPercentage * 100;

                processProcessButton.Title = "Client.Process.Cancel".Translate();
            }
            else
            {
                processTargetPath.Enabled = true;
                processTotalProgressBar.Hidden = true;
                processProcessButton.Title = "Client.Process.Process".Translate();
            }
        }

        /// <summary>
        /// Set the licensed state of the application
        /// </summary>
        /// <param name="licensed">Are we licensed?</param>
        void SetLicensingState(bool licensed)
        {
            if (licensed)
            {
                menuActivateButton.Hidden = true;

                // Disable the Process Process button
                processProcessButton.Enabled = true;
                preferencesLicenseDeactivateButton.Enabled = true;
            }
            else
            {
                menuActivateButton.Hidden = false;

                // Enable the Process Process button
                processProcessButton.Enabled = false;
                preferencesLicenseDeactivateButton.Enabled = false;
            }
        }

        /// <summary>
        /// Set the state of our "posh" menu buttons
        /// </summary>
        /// <param name="background">Item background reference</param>
        /// <param name="text">Item text reference</param>
        /// <param name="icon">Item icon image reference</param>
        /// <param name="iconName">Name of icon resource to use</param>
        /// <param name="selected">If item is selected?</param>
        void SetMenuButtonState(NSImageView background, NSTextField text, NSTextField icon, string iconName = Resources.ProcessIcon, bool selected = false)
        {
            if (selected)
            {
                background.Hidden = false;
                text.TextColor = NSColor.White;
                icon.TextColor = NSColor.White;
            }
            else
            {
                background.Hidden = true;
                text.TextColor = _colorLightBlue;
                icon.TextColor = _colorMenuOrange;
            }

            // Update Icon
            icon.StringValue = iconName;
        }

        /// <summary>
        /// Start the spinning of the process icon
        /// </summary>
        void StartProcessAnimation()
        {
            if ( _animateProcessIcon ) {
                return;
            }
            _animateProcessIcon = true;

            _animateProcessTimer = new DispatchSource.Timer(strict: true, queue: DispatchQueue.MainQueue);

            // 30 FPS
            _animateProcessTimer.SetTimer(DispatchTime.Now, 33333333, 33333334);

            _animateProcessTimer.SetEventHandler(() => {
                InvokeOnMainThread(() =>
                {
                    //menuProcessButtonIconText.RotateByAngle(5);
                });
            });

            _animateProcessTimer.Resume();
        }

        /// <summary>
        /// Stop the spinning of the process icon
        /// </summary>
        void StopProcessAnimation()
        {
            if (!_animateProcessIcon) return;
            _animateProcessIcon = false;

            _animateProcessTimer.Cancel();
            _animateProcessTimer.Dispose();
        }

        /// <summary>
        /// Update the Config Table with the the relevant configuration.
        /// </summary>
		/// <param name="hunterIndex">Index of <see cref="HuntHandler" /> to set the config table too.</param>
		void UpdateHuntConfigTable(string hunterIndex)
        {
			// Create new data source from session config
			_hunterConfigDataSource = new ConfigDataSource(Instance.Hunters[hunterIndex].GetConfigFromSession());
			                                               
            if (_hunterConfigDataSource != null)
            {
                _hunterConfigDataSource.OnChanged += delegate
                {
                    // Store in temp holding
                    _hunterConfigTemp = ProcessConfig.FromDataSourceObjects(_hunterConfigDataSource.Items);               

					// Update session config
					Instance.Hunters[_huntIndex].UpdateSessionConfig(_hunterConfigTemp);

                };
                
                processConfigTable.Delegate = new ConfigDelegate(_hunterConfigDataSource);
                processConfigTable.DataSource = _hunterConfigDataSource;
            }
            else
            {
                Instance.Log("Client.Mac.MainWindowController.UpdateHuntConfigTable", "Unable to create config data source.");
            }

        }

        #endregion
    }
}
