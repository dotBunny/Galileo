using Galileo.Client.Screens;
using Galileo.Client.Update;
using Galileo.Core;
using I18NPortable;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;



namespace Galileo.Client.Win

{

    /// <summary>

    /// Galileo Main Form

    /// </summary>

    public partial class MainForm : Form, IClient

    {

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]

        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);



        #region Fields



        /// <summary>

        /// Cached light grey border color

        /// </summary>

        Color _colorGreyControlBorder;



        /// <summary>

        /// Cached light blue color

        /// </summary>

        Color _colorLightBlue;



        /// <summary>

        /// Cached orange color

        /// </summary>

        Color _colorOrange;



        /// <summary>

        /// Washed out orange color for menu

        /// </summary>

        Color _colorMenuOrange;



        /// <summary>

        /// The current view state of the window

        /// </summary>

        Instance.State _currentState;



        /// <summary>

        /// Font collection used to house embeded fonts

        /// </summary>

        PrivateFontCollection _fontCollection = new PrivateFontCollection();



        /// <summary>

        /// Cached reference to icon font

        /// </summary>

        Font _fontAwesomeMenuIcons;



        /// <summary>

        /// Has the form finished loading?

        /// </summary>

        /// <remarks>

        /// We use this to hold off some of our updates from occuring during load

        /// </remarks>

        bool _formLoaded = false;



        /// <summary>

        /// The currently selected hunter handler's index

        /// </summary>

        string _huntIndex = "";



        /// <summary>

        /// The last hunter to drive a notification

        /// </summary>

        string _notifyHuntIndex;



        /// <summary>

        /// The configuration storage used in the IDE for the default process config.

        /// </summary>

        HunterConfig _preferencesDefaultsConfigTemp = Settings.DefaultConfig;





        /// <summary>

        /// The configuration storage used in the IDE before it is written out per process.

        /// </summary>

        HunterConfig _processConfigTemp = Settings.DefaultConfig;



        /// <summary>

        /// The dynamic update icon to use in the menu.

        /// </summary>

        string _updateIcon = Resources.UpdatesIconNone;



        /// <summary>

        /// Cached reference to the update link for the platform.

        /// </summary>

        string _updateLinkCache = "";



        /// <summary>

        /// Are there any updates pending?

        /// </summary>

        bool _updatePending = false;



        /// <summary>

        /// Cached version existing/found information

        /// </summary>

        string _updateVersionCache = "";



        /// <summary>

        /// Action triggered when the window finishes waking up.

        /// </summary>

        public Action OnAwake;



        #endregion



        /// <summary>

        /// Initializes a new instance of the <see cref="T:Galileo.Client.Win.MainForm"/> class.

        /// </summary>

        public MainForm()

        {

            InitializeComponent();



            ResizeRedraw = true;



            // Stop flickering during form draw process

            DoubleBuffered = true;



            _colorLightBlue = Color.FromArgb(234, 246, 255);

            _colorOrange = Color.FromArgb(255, 166, 35);

            _colorMenuOrange = Color.FromArgb(244, 169, 71);



            // this is the tab controller color on windows, we use to match to all the other borders

            _colorGreyControlBorder = Color.FromArgb(217, 217, 217);



            // Set color scheme

            processTargetFolderPanel.BackColor = _colorGreyControlBorder;

            updatesTextBackgroundPanel.BackColor = _colorGreyControlBorder;

            aboutSystemInfoPanel.BackColor = _colorGreyControlBorder;

            preferencesDefaultsGridPanel.BackColor = _colorGreyControlBorder;

            preferencesDefaultsTargetFolderPanel.BackColor = _colorGreyControlBorder;

            preferencesUpdatesDividerPicture.BackColor = _colorGreyControlBorder;



            // Load font awesome

            Stream fontStream = this.GetType().Assembly.GetManifestResourceStream("Galileo.Client.Win.Resources.font-awesome.otf");

            byte[] fontData = new byte[fontStream.Length];

            int fontLength = (int)fontStream.Length;

            fontStream.Read(fontData, 0, (int)fontStream.Length);

            fontStream.Close();



            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);

            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);



            uint dummy = 0;

            _fontCollection.AddMemoryFont(fontPtr, fontLength);

            AddFontMemResourceEx(fontPtr, (uint)fontLength, IntPtr.Zero, ref dummy);

            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);



            _fontAwesomeMenuIcons = new Font(_fontCollection.Families[0], 26.0F);

            menuProcessIconLabel.Font = _fontAwesomeMenuIcons;            

            menuPreferencesIconLabel.Font = _fontAwesomeMenuIcons;

            menuUpdatesIconLabel.Font = _fontAwesomeMenuIcons;

        }



        /// <summary>

        /// Main form class destructor

        /// </summary>

        ~MainForm()

        {

            notifyIcon.Dispose();

        }



        #region Events



        /// <summary>

        /// Open the EULA when the "EULA" button is clicked

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void AboutEULAButton_Click(object sender, EventArgs e)

        {

            AboutScreen.EULAButton_Click();

        }



        /// <summary>

        /// Open the log folder when the "Logs" button is clicked

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void AboutLogsButton_Click(object sender, EventArgs e)

        {

            AboutScreen.LogsButton_Click();

        }



        /// <summary>

        /// About table paint event

        /// </summary>

        /// <param name="sender">Table Reference</param>

        /// <param name="e">Event Arguments</param>

        private void AboutTable_Paint(object sender, PaintEventArgs e)

        {

            Control local = (Control)sender;



            int size = 150;



            e.Graphics.DrawImage(Properties.Resources.dotbunny,

                new Rectangle(local.Size.Width - size - 15, 25, size, size));

        }



        /// <summary>

        /// Open the Third Party Licenses when the "Third Party Licenses" button is clicked

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void AboutThirdPartyLicensesButton_Click(object sender, EventArgs e)

        {

            AboutScreen.ThirdPartyLicensesButton_Click();

        }





        /// <summary>

        /// MainForm Load Event

        /// </summary>

        /// <param name="sender">Window Reference</param>

        /// <param name="e">Event Arguments</param>

        private void MainForm_Load(object sender, EventArgs e)

        {

            Instance.Log("Client.Win.MainForm.MainForm_Load", "A monster in the deep stirs ...");



            // Localize window

            Localize();



            // Handle Location / Size

            Types.Vector2<int> size = Settings.WindowSize;

            Types.Vector2<int> location = Settings.WindowLocation;

            Screen screen = Screen.FromControl(this);



            Size = new Size(size.X, size.Y);

            if (location.X == -1 && location.Y == -1)

            {

                this.SetDesktopLocation(

                    (int)((screen.Bounds.Width / 2f) - (size.X / 2f)),

                    (int)((screen.Bounds.Height / 2f) - (size.Y / 2f)));

            }

            else

            {

                this.SetDesktopLocation(location.X, location.Y);

            }

            Instance.Log("Client.Win.MainForm.MainForm_Load", "Window Framed ...");



            // Hide content tab header

            screenTabs.Appearance = TabAppearance.FlatButtons;

            screenTabs.SizeMode = TabSizeMode.Fixed;

            screenTabs.ItemSize = new Size(0, 1);

            screenTabs.Dock = DockStyle.None;



            MainForm_Resize(sender, null);



            // Setup menu button connections

            menuProcessButtonPanel.Click += MenuProcessButton_Click;

            menuProcessButtonLabel.Click += MenuProcessButton_Click;

            menuProcessIconLabel.Click += MenuProcessButton_Click;



            SetMenuButtonState(menuProcessButtonPanel, menuProcessButtonLabel, menuProcessIconLabel, Resources.ProcessIcon);


            menuPreferencesButtonPanel.Click += MenuPreferencesButton_Click;

            menuPreferencesButtonLabel.Click += MenuPreferencesButton_Click;

            menuPreferencesIconLabel.Click += MenuPreferencesButton_Click;

            SetMenuButtonState(menuPreferencesButtonPanel, menuPreferencesButtonLabel, menuPreferencesIconLabel, Resources.PreferencesIcon);



            menuUpdatesButtonPanel.Click += MenuUpdatesButton_Click;

            menuUpdatesButtonLabel.Click += MenuUpdatesButton_Click;

            menuUpdatesIconLabel.Click += MenuUpdatesButton_Click;

            SetMenuButtonState(menuUpdatesButtonPanel, menuUpdatesButtonLabel, menuUpdatesIconLabel, Resources.UpdatesIconNone);



            // Create our default working hunter (we will need to expand out on this for multiple hunters)

            _huntIndex = HuntHandler.CreateID();

            Instance.Hunters.Add(_huntIndex, HuntHandler.Create(_huntIndex, Instance.Profile));

            Instance.Hunters[_huntIndex].OnProcessUpdate += OnHuntHandlerUpdate;

            Instance.Hunters[_huntIndex].OnProcessComplete += OnHuntHandlerComplete;

            Instance.Hunters[_huntIndex].OnProcessLogEvent += OnHuntHandlerLogEvent;

            Instance.Log("Client.Win.MainForm.MainForm_Load", "Hunter Initialized ...");



            // Set label version

            menuVersionLabel.Text = Instance.Profile.PackageVersion;





            // Populate locale list

            preferencesGeneralLocaleCombo.Items.Clear();

            foreach (string s in Localization.LocalizationProvider.SupportedLocalesDescription)

            {

                preferencesGeneralLocaleCombo.Items.Add(s);

            }


            SetWindowState(Instance.State.Hunt);



            // Foce update of thigns

            SetHuntIndex(_huntIndex, true);





            _formLoaded = true;

            Instance.Log("Client.Win.MainForm.MainForm_Load", "Galileo Ready.");



            // Call OnAwake

            OnAwake?.Invoke();

        }



        /// <summary>

        /// MainForm Resize Event

        /// </summary>

        /// <param name="sender">Window Reference</param>

        /// <param name="e">Event Argument</param>

        private void MainForm_Resize(object sender, EventArgs e)

        {

            // This might need to be rethought based on the fact that were using the left border adjustment

            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);



            // Update border adjustment

            int borderLeft = screenRectangle.Left - this.Left;

            int spec = borderLeft / 2;





            screenTabs.Size = new Size(

                screenRectangle.Width - 275 + spec,// - borderLeft - borderRight,

                screenRectangle.Height - screenTabs.ItemSize.Height + borderLeft); // - borderTop - borderBottom);



            screenTabs.Location = new Point(275, 0 - screenTabs.ItemSize.Height - spec);



            // Save Size

            if (_formLoaded)

            {

                Settings.WindowSize = new Types.Vector2<int>(this.Width, this.Height);

            }

        }



        /// <summary>

        /// MainForm Move Event

        /// </summary>

        /// <param name="sender">Window Reference</param>

        /// <param name="e">Event Arguments</param>

        private void MainForm_Move(object sender, EventArgs e)

        {

            if (_formLoaded)

            {

                Settings.WindowLocation = new Types.Vector2<int>(this.DesktopLocation.X, this.DesktopLocation.Y);

            }

        }



        /// <summary>

        /// Show the About screen when the "Galileo Branding" is clicked in the menu

        /// </summary>

        /// <param name="sender">Picture Reference</param>

        /// <param name="e">Event Arguments</param>

        private void MenuBranding_Click(object sender, EventArgs e)

        {

            SetWindowState(Instance.State.About);

        }



        /// <summary>

        /// Menu background painting event

        /// </summary>

        /// <param name="sender">Flow Panel Reference</param>

        /// <param name="e">Event Arguments</param>

        private void MenuFlowPanel_Paint(object sender, PaintEventArgs e)

        {

            // this stretches the image, it needs to be zoomed

            e.Graphics.DrawImage(Properties.Resources.GalileoShieldBackground,

                new Rectangle(-145, 135, 289, 316));

        }



        /// <summary>

        /// Show the Preferences screen when the "Preferences" button is clicked in the menu

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void MenuPreferencesButton_Click(object sender, EventArgs e)

        {

            SetWindowState(Instance.State.Preferences);

        }



        /// <summary>

        /// Show the Process screen when the "Process" button is clicked in the menu

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void MenuProcessButton_Click(object sender, EventArgs e)

        {

            SetWindowState(Instance.State.Hunt);

        }



        /// <summary>

        /// Show the Updates screen when the "Updates" button is clicked in the menu

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void MenuUpdatesButton_Click(object sender, EventArgs e)

        {

            SetWindowState(Instance.State.Updates);

        }



        /// <summary>

        /// No update's check for updates button

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void NoUpdateCheckForUpdateButton_Click(object sender, EventArgs e)

        {

            UpdatesScreen.CheckForUpdatesButton_Click();



            noUpdateIgnoreVersionLabel.Text = string.Empty;

            Program.CheckForUpdates(false, true);

        }



        /// <summary>

        /// Event fired when a notification is clicked in the system tray

        /// </summary>

        /// <param name="sender">System Tray Reference</param>

        /// <param name="e">Event Arguments</param>

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)

        {

            Instance.Hunters[_notifyHuntIndex].OpenReport();

        }



        /// <summary>

        /// Subscribed event called when a HuntHandler fires off it's completed action

        /// </summary>

        /// <param name="ID">The Hunt Handler identification</param>

        void OnHuntHandlerComplete(string ID)

        {

            if (processProcessButton == null) return;



            // Open Report - Even In Background

            processProcessButton.Invoke((MethodInvoker)delegate

            {

                if (Instance.Hunters[_huntIndex].HasReport())

                {

                    Instance.Hunters[_huntIndex].OpenReport();

                }



                _notifyHuntIndex = ID;

                notifyIcon.ShowBalloonTip(1000, "Galileo".Translate(), "Client.Process.Complete.Notification".Translate(), ToolTipIcon.Info);

            });







            if (_huntIndex != ID) return;



            processProcessButton.Invoke((MethodInvoker)delegate

            {

                processProcessButton.Text = "Client.Process.Process".Translate();



                if (Instance.Hunters[_huntIndex].HasReport())

                {

                    processReportButton.Enabled = true;

                }



                progressTotalProgressBar.Value = 100;

                progressTotalProgressBar.Visible = false;

                processTargetText.Enabled = true;

                processTargetButton.Enabled = true;



            });

        }



        /// <summary>

        /// Subscribed event called when a HuntHandler fires off it's log event action

        /// </summary>

        /// <param name="ID">The Hunt Handler identification</param>

        /// <param name="line">The log entry</param>

        void OnHuntHandlerLogEvent(string ID, string line)

        {

            if (_huntIndex != ID || processLogRichText == null) return;



            processLogRichText.Invoke((MethodInvoker)delegate

            {

                processLogRichText.AppendText(line);

                processLogRichText.SelectionStart = processLogRichText.Text.Length;

                processLogRichText.ScrollToCaret();

            });

        }



        /// <summary>

        /// Update Callback from Hunter

        /// </summary>

        /// <param name="ID">The Assigned Hunter ID</param>

        void OnHuntHandlerUpdate(string ID)

        {

            if (_huntIndex != ID) return;



            progressTotalProgressBar.Invoke((MethodInvoker)delegate

            {

                progressTotalProgressBar.Value = (int)(Instance.Hunters[_huntIndex].ProgressPercentage * 100);

            });

        }



        /// <summary>

        /// Open a dialogue to select the default target folder

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesDefaultsTargetFolderSelectButton_Click(object sender, EventArgs e)

        {

            folderDialog.SelectedPath = Settings.DefaultFolder;

            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))

            {

                PreferencesScreen.PreferencesDefaultsTargetFolderPath_Click(folderDialog.SelectedPath);

                preferencesDefaultsTargetFolderText.Text = folderDialog.SelectedPath;

            }

        }



        /// <summary>

        /// Update default target folder when the text is edited

        /// </summary>

        /// <param name="sender">Text Reference</param>

        /// <param name="e">Event Arguements</param>

        private void PreferencesDefaultsTargetFolderSelection_TextChanged(object sender, EventArgs e)

        {

            // Don't do anything unless we are on the tab (visible)

            if (screenTabs.SelectedTab != preferencesTab || preferencesTabs.SelectedTab != preferencesProcessTab) return;



            PreferencesScreen.PreferencesDefaultsTargetFolderPath_Click(preferencesDefaultsTargetFolderText.Text);

        }



        /// <summary>

        /// Export to a file the default process config

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesDefaultsExportButton_Click(object sender, EventArgs e)

        {

            DialogResult result = saveDialog.ShowDialog();



            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(saveDialog.FileName))

            {

                Alert(PreferencesScreen.DefaultsExportButton_Click(saveDialog.FileName));

            }

        }



        /// <summary>

        /// Import a file as the process config defaults

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesDefaultsImportButton_Click(object sender, EventArgs e)

        {

            DialogResult result = openDialog.ShowDialog();



            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openDialog.FileName))

            {

                if (File.Exists(openDialog.FileName))

                {

                    Alert(PreferencesScreen.DefaultsImportButton_Click(openDialog.FileName));

                    SetWindowState(Instance.State.Preferences, true);

                }

            }

        }



        /// <summary>

        /// "Client.Preferences.Reset".Translate()

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesDefaultsRestoreDefaultsButton_Click(object sender, EventArgs e)

        {



            DialogResult dialogResult =

            MessageBox.Show("Client.Preferences.ResetMessage".Translate("Client.Preferences.Defaults".Translate()),

                               "Client.Preferences.Reset".Translate(),

                               MessageBoxButtons.YesNo);



            if (dialogResult == DialogResult.Yes)

            {

                SetWindowState(Instance.State.Blank);

                PreferencesScreen.DefaultsRestoreDefaultsButton_Click();

                SetWindowState(Instance.State.Preferences);

            }

        }



        /// <summary>

        /// Set the locale of the client

        /// </summary>

        /// <param name="sender">Combo Box Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesGeneralLocaleCombo_SelectedIndexChanged(object sender, EventArgs e)

        {

            // Don't do anything unless we are on the tab (visible)

            if (screenTabs.SelectedTab != preferencesTab || preferencesTabs.SelectedTab != preferencesGeneralTab) return;



            PreferencesScreen.SetLocale(preferencesGeneralLocaleCombo.SelectedIndex, this);

        }



        /// <summary>

        /// Set if reports should be opened automatically when processing finishes

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesGeneralReportAutomaticOpenCheck_CheckedChanged(object sender, EventArgs e)

        {

            // Don't do anything unless we are on the tab (visible)

            if (screenTabs.SelectedTab != preferencesTab || preferencesTabs.SelectedTab != preferencesGeneralTab) return;



            PreferencesScreen.GeneralReportAutomaticOpenCheckButton_Click(preferencesGeneralReportAutomaticOpenCheck.Checked);

        }



        /// <summary>

        /// Restore defaults for the General tab of preferences

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesGeneralRestoreDefaultsButton_Click(object sender, EventArgs e)

        {

            DialogResult dialogResult =

            MessageBox.Show("Client.Preferences.ResetMessage".Translate("Client.Preferences.General".Translate()),

                              "Client.Preferences.Reset".Translate(),

                               MessageBoxButtons.YesNo);



            if (dialogResult == DialogResult.Yes)

            {

                SetWindowState(Instance.State.Blank);

                PreferencesScreen.GeneralRestoreDefaultsButton_Click(this);

            }

        }



        /// <summary>

        /// Set the sending of usage data from the Preferences screen

        /// </summary>

        /// <param name="sender">Check Reference</param>

        /// <param name="e">Event Arguements</param>

        private void PreferencesGeneralSendUsageDataCheck_CheckedChanged(object sender, EventArgs e)

        {

            // Don't do anything unless we are on the tab (visible)

            if (screenTabs.SelectedTab != preferencesTab || preferencesTabs.SelectedTab != preferencesGeneralTab) return;



            PreferencesScreen.GeneralSendUsageDataButton_Click(preferencesGeneralSendUsageDataCheck.Checked);

        }


        /// <summary>

        /// Set the update channel

        /// </summary>

        /// <param name="sender">Combo Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesUpdatesChannelCombo_SelectedIndexChanged(object sender, EventArgs e)

        {

            // Don't do anything unless we are on the tab (visible)

            if (screenTabs.SelectedTab != preferencesTab || preferencesTabs.SelectedTab != preferencesUpdatesTab) return;



            switch (preferencesUpdatesChannelCombo.SelectedIndex)

            {

                case 1:

                    PreferencesScreen.SetUpdateChannel(UpdateProvider.Channel.Beta);

                    break;

                default:

                    PreferencesScreen.SetUpdateChannel(UpdateProvider.Channel.Release);

                    break;

            }

        }



        /// <summary>

        /// Enable update checks

        /// </summary>

        /// <param name="sender">Radio Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesUpdatesEnableYesButton_CheckedChanged(object sender, EventArgs e)

        {

            // Don't do anything unless we are on the tab (visible)

            if (screenTabs.SelectedTab != preferencesTab || preferencesTabs.SelectedTab != preferencesUpdatesTab) return;



            if (preferencesUpdatesEnableYesButton.Checked)

            {

                PreferencesScreen.SetCheckForUpdates(true);

                preferencesUpdatesEnableNoButton.Checked = false;

            }

        }



        /// <summary>

        /// Disable update checks

        /// </summary>

        /// <param name="sender">Radio Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesUpdatesEnableNoButton_CheckedChanged(object sender, EventArgs e)

        {

            // Don't do anything unless we are on the tab (visible)

            if (screenTabs.SelectedTab != preferencesTab || preferencesTabs.SelectedTab != preferencesUpdatesTab) return;



            if (preferencesUpdatesEnableNoButton.Checked)

            {

                PreferencesScreen.SetCheckForUpdates(false);

                preferencesUpdatesEnableYesButton.Checked = false;

            }

        }



        /// <summary>

        /// Set the frequency which updates are checked

        /// </summary>

        /// <param name="sender">Combo Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesUpdatesFrequencyCombo_SelectedIndexChanged(object sender, EventArgs e)

        {

            // Don't do anything unless we are on the tab (visible)

            if (screenTabs.SelectedTab != preferencesTab || preferencesTabs.SelectedTab != preferencesUpdatesTab) return;



            switch (preferencesUpdatesFrequencyCombo.SelectedIndex)

            {

                case 0:

                    PreferencesScreen.SetUpdateFrequency(UpdateProvider.Frequency.Daily);

                    break;

                case 2:

                    PreferencesScreen.SetUpdateFrequency(UpdateProvider.Frequency.Monthly);

                    break;

                default:

                    PreferencesScreen.SetUpdateFrequency(UpdateProvider.Frequency.Weekly);

                    break;

            }

        }



        /// <summary>

        /// Restore the Update preferences to their default values

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void PreferencesUpdatesRestoreDefaultsButton_Click(object sender, EventArgs e)

        {

            DialogResult dialogResult =

                       MessageBox.Show("Client.Preferences.ResetMessage".Translate("Client.Preferences.Updates".Translate()),

                                          "Client.Preferences.Reset".Translate(),

                                          MessageBoxButtons.YesNo);



            if (dialogResult == DialogResult.Yes)

            {

                SetWindowState(Instance.State.Blank);

                PreferencesScreen.UpdatesRestoreDefaultsButton_Click();

                SetWindowState(Instance.State.Preferences);

            }

        }



        /// <summary>

        /// Instructs the currently selected Process to begin

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void ProcessProcessButton_Click(object sender, EventArgs e)

        {



            // Disable input while were dealing with all this because of threading

            processProcessButton.Enabled = false;



            if (!ProcessScreen.ProcessButton_Click(_huntIndex))

            {

                progressTotalProgressBar.Visible = false;

                progressTotalProgressBar.Value = 0;

                processProcessButton.Text = "Client.Process.Process".Translate();

                processTargetText.Enabled = true;

                processTargetButton.Enabled = true;

            }

            else

            {

                // Clear Log

                processLogRichText.Clear();



                progressTotalProgressBar.Visible = true;

                progressTotalProgressBar.Value = 0;

                processProcessButton.Text = "Client.Process.Cancel".Translate();

                processTargetText.Enabled = false;

                processTargetButton.Enabled = false;

            }



            // Reenable input after everything is setup

            processProcessButton.Enabled = true;

        }



        /// <summary>

        /// Open any report that is present for the currently selected report

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void ProcessReportButton_Click(object sender, EventArgs e)

        {

            ProcessScreen.ReportButton_Click(_huntIndex);

        }



        /// <summary>

        /// Display a dialogue to select the current process' target folder

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void ProcessTargetButton_Click(object sender, EventArgs e)

        {

            folderDialog.SelectedPath = Instance.Hunters[_huntIndex].WorkingDirectory;

            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))

            {

                Instance.Hunters[_huntIndex].UpdateWorkingDirectory(folderDialog.SelectedPath);

                processTargetText.Text = Instance.Hunters[_huntIndex].WorkingDirectory;

            }

        }



        /// <summary>

        /// Trigger the displaying of a folder select dialogue

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void ProcessTargetText_DoubleClick(object sender, EventArgs e)

        {

            ProcessTargetButton_Click(sender, e);

        }



        /// <summary>

        /// Update the current process target folder based on changes

        /// </summary>

        /// <param name="sender">Text Box Reference</param>

        /// <param name="e">Event Arguments</param>

        private void ProcessTargetText_TextChanged(object sender, EventArgs e)

        {

            Instance.Hunters[_huntIndex].UpdateWorkingDirectory(processTargetText.Text);





            // Update the config table for the process

            UpdateProcessConfigTable(_huntIndex);



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



        /// <summary>

        /// Update tab's download button

        /// </summary>

        /// <param name="sender">Button Reference</param>

        /// <param name="e">Event Arguments</param>

        private void UpdatesDownloadButton_Click(object sender, EventArgs e)

        {

            UpdatesScreen.DownloadButton_Click(_updateLinkCache);

        }



        /// <summary>

        /// Update tab's ingore button

        /// </summary>

        /// <param name="sender">Button</param>

        /// <param name="e">Event Arguments</param>

        private void UpdatesIgnoreButton_Click(object sender, EventArgs e)

        {

            UpdatesScreen.IgnoreButton_Click(_updateVersionCache);



            _updatePending = false;

            SetWindowState(Instance.State.Updates, true);

        }

        #endregion



        #region Methods



        /// <summary>

        /// Localize Form

        /// </summary>

        public void Localize()

        {

            // Cache current setting

            Instance.State stateCache = _currentState;



            // Blank state

            SetWindowState(Instance.State.Blank, true);



            // Menu

            menuProcessButtonLabel.Text = "Client.Menu.Process".Translate();

            menuSystemSectionLabel.Text = "Client.Menu.System".Translate().ToUpper();

            menuPreferencesButtonLabel.Text = "Client.Menu.Preferences".Translate();

            menuUpdatesButtonLabel.Text = "Client.Menu.Updates".Translate();



            // About Tab

            aboutTitleLabel.Text = "Client.About.Title".Translate();

            aboutMessageLabel.Text = "Client.About.Message".Translate();

            aboutClientInfoLabel.Text = "Client.About.ClientInfo".Translate();

            aboutSystemInfoLabel.Text = "Client.About.SysInfo".Translate();

            aboutThirdPartyLicensesButton.Text = "Client.About.ThirdPartyLicenses".Translate();

            aboutEULAButton.Text = "Client.About.EULA".Translate();

            aboutLogsButton.Text = "Client.About.Logs".Translate();



            // Process Tab

            processTitleLabel.Text = "Client.Process.Title".Translate();

            processMessageLabel.Text = "Client.Process.Message".Translate();

            processReminderLabel.Text = "Client.Process.Reminder".Translate();

            processLogTab.Text = "Client.Process.Log".Translate();

            processOptionsTab.Text = "Client.Process.Options".Translate();

            processProcessButton.Text = "Client.Process.Process".Translate();

            processReportButton.Text = "Client.Process.Report".Translate();



            // Updates Tab

            updatesIgnoreButton.Text = "Client.Updates.Ignore".Translate();

            updatesDownloadButton.Text = "Client.Updates.Download".Translate();



            // No Updates Tab

            noUpdateCheckForUpdatesButton.Text = "Client.NoUpdate.CheckForUpdates".Translate();

            noUpdateTitleLabel.Text = "Client.NoUpdate.Title".Translate();

            noUpdateMessageLabel.Text = "Client.NoUpdate.Message".Translate();

            noUpdateNextUpdateCheckLabel.Text = UpdatesScreen.GetNextCheckText();



            // Preferences Tab

            preferencesTitleLabel.Text = "Client.Preferences.Title".Translate();



            preferencesGeneralTab.Text = "Client.Preferences.General".Translate();

            preferencesGeneralMessageLabel.Text = "Client.Preferences.General.Message".Translate();

            preferencesGeneralLocaleLabel.Text = "Client.Preferences.General.Locale".Translate();

            preferencesGeneralRestoreDefaultsButton.Text = "Client.Preferences.RestoreDefaults".Translate();

            preferencesGeneralReportAutomaticOpenCheck.Text = "Client.Preferences.General.OpenReports".Translate();

            preferencesGeneralSendUsageDataCheck.Text = "Client.Preferences.General.SendData".Translate();

            preferencesGeneralDataExplanationLabel.Text = "Client.Preferences.Genearl.SendDataMessage".Translate();



            preferencesProcessTab.Text = "Client.Preferences.Defaults".Translate();

            preferencesDefaultsMessageLabel.Text = "Client.Preferences.Defaults.Message".Translate();

            preferencesDefaultsTargetFolderLabel.Text = "Client.Preferences.Defaults.TargetFolder".Translate();

            preferencesDefaultsImportButton.Text = "Client.Preferences.Defaults.Import".Translate();

            preferencesDefaultsExportButton.Text = "Client.Preferences.Defaults.Export".Translate();

            preferencesDefaultsRestoreDefaultsButton.Text = "Client.Preferences.RestoreDefaults".Translate();



            preferencesUpdatesTab.Text = "Client.Preferences.Updates".Translate();

            preferencesUpdatesMessageLabel.Text = "Client.Preferences.Updates.Message".Translate();

            preferencesUpdatesEnableLabel.Text = "Client.Preferences.Updates.Enable".Translate();

            preferencesUpdatesEnableYesButton.Text = "Client.Preferences.Updates.Yes".Translate();

            preferencesUpdatesEnableNoButton.Text = "Client.Preferences.Updates.No".Translate();



            preferencesUpdatesCheckFrequencyLabel.Text = "Client.Preferences.Updates.CheckFrequency".Translate();

            preferencesUpdatesFrequencyCombo.Items[0] = "Client.Preferences.Updates.CheckFrequency.Daily".Translate();

            preferencesUpdatesFrequencyCombo.Items[1] = "Client.Preferences.Updates.CheckFrequency.Weekly".Translate();

            preferencesUpdatesFrequencyCombo.Items[2] = "Client.Preferences.Updates.CheckFrequency.Monthly".Translate();



            preferencesUpdateChannelLabel.Text = "Client.Preferences.Updates.Channel".Translate();

            preferencesUpdatesChannelCombo.Items[0] = "Client.Preferences.Updates.Channel.Release".Translate();

            preferencesUpdatesChannelCombo.Items[1] = "Client.Preferences.Updates.Channel.Beta".Translate();



            preferencesUpdatesRestoreDefaultsButton.Text = "Client.Preferences.RestoreDefaults".Translate();



            // Dialogues

            folderDialog.Description = "Client.Process.Title".Translate();



            // Reset state

            SetWindowState(stateCache, true);



            Instance.Log("Client.Win.MainForm.Localize", "Localized to " + Localization.LocalizationProvider.GetCulture());

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

                // Update information about the update (on the UI thread)

                updatesChangelogRichText.Invoke((MethodInvoker)delegate
                {

                    updatesChangelogRichText.Text = update.Changelog;



                    updatesTitleLabel.Text = "Galileo.Version".Translate(update.Current.Version);

                    updatesMessageLabel.Text = update.Specification.Message;

                    updatesDateLabel.Text = update.Specification.ReleaseDate;



                    // Change Icon To Warning - THIS does not get translated its a resource identifier

                    _updateIcon = "Warning";



                    // Cache link/version

                    _updateLinkCache = update.Specification.WindowsLink;

                    _updateVersionCache = update.Current.Version;



                    // Update the window OR the icon depending

                    if (_currentState == Instance.State.Updates)

                    {

                        // Forcably refresh the current window to new licensing

                        SetWindowState(Instance.State.Updates, true);

                    }

                    else

                    {

                        // Set icon

                        menuUpdatesIconLabel.Text = Resources.UpdatesIconFound;

                    }

                });

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

            if (_currentState == newState && !forceRefresh) return false;



            switch (newState)

            {

                case Instance.State.Hunt:



                    // Update Menu

                    SetMenuButtonState(menuProcessButtonPanel, menuProcessButtonLabel, menuProcessIconLabel, Resources.ProcessIcon, true);

                    SetMenuButtonState(menuPreferencesButtonPanel, menuPreferencesButtonLabel, menuPreferencesIconLabel, Resources.PreferencesIcon);

                    SetMenuButtonState(menuUpdatesButtonPanel, menuUpdatesButtonLabel, menuUpdatesIconLabel, _updateIcon);



                    SetHuntIndex(_huntIndex, true);

                    screenTabs.SelectTab(processTab);

                    _currentState = Instance.State.Hunt;

                    break;



                case Instance.State.Preferences:



                    // Update Menu 
                    SetMenuButtonState(menuProcessButtonPanel, menuProcessButtonLabel, menuProcessIconLabel, Resources.ProcessIcon);

                    SetMenuButtonState(menuPreferencesButtonPanel, menuPreferencesButtonLabel, menuPreferencesIconLabel, Resources.PreferencesIcon, true);

                    SetMenuButtonState(menuUpdatesButtonPanel, menuUpdatesButtonLabel, menuUpdatesIconLabel, _updateIcon);



                    // Load settings to preferences

                    preferencesDefaultsTargetFolderText.Text = Settings.DefaultFolder;



                    // Update the locale

                    for (int i = 0; i < preferencesGeneralLocaleCombo.Items.Count; i++)

                    {



                        if (Localization.LocalizationProvider.SupportedLocales[i] == Settings.Localization)

                        {

                            preferencesGeneralLocaleCombo.SelectedIndex = i;

                        }

                    }



                    preferencesGeneralReportAutomaticOpenCheck.Checked = Settings.OpenReportsAutomatically;

                    preferencesGeneralSendUsageDataCheck.Checked = Settings.AnonymousUsageStats;



                    // Load default settings into the table

                    preferencesDefaultsPropertyGrid.PropertyValueChanged -= delegate { };



                    _preferencesDefaultsConfigTemp = Settings.DefaultConfig;

                    preferencesDefaultsPropertyGrid.SelectedObject = _preferencesDefaultsConfigTemp;

                    preferencesDefaultsPropertyGrid.PropertyValueChanged += delegate

                    {

                        Settings.DefaultConfig = _preferencesDefaultsConfigTemp;

                    };



                    if (Settings.ShouldCheckForUpdates)

                    {

                        preferencesUpdatesEnableYesButton.Checked = true;

                        preferencesUpdatesEnableNoButton.Checked = false;

                    }

                    else

                    {

                        preferencesUpdatesEnableNoButton.Checked = true;

                        preferencesUpdatesEnableYesButton.Checked = false;

                    }



                    switch (Settings.UpdateCheckFrequency)

                    {

                        case UpdateProvider.Frequency.Daily:

                            preferencesUpdatesFrequencyCombo.SelectedIndex = 0;

                            break;

                        case UpdateProvider.Frequency.Monthly:

                            preferencesUpdatesFrequencyCombo.SelectedIndex = 2;

                            break;

                        default:

                            preferencesUpdatesFrequencyCombo.SelectedIndex = 1;

                            break;

                    }



                    switch (Settings.UpdatesChannel)

                    {

                        case UpdateProvider.Channel.Beta:

                            preferencesUpdatesChannelCombo.SelectedIndex = 1;

                            break;

                        default:

                            preferencesUpdatesChannelCombo.SelectedIndex = 0;

                            break;

                    }



                    // Select tab

                    screenTabs.SelectTab(preferencesTab);



                    // Set the state

                    _currentState = Instance.State.Preferences;



                    break;



                case Instance.State.Updates:



                    // Update Menu

                    SetMenuButtonState(menuProcessButtonPanel, menuProcessButtonLabel, menuProcessIconLabel, Resources.ProcessIcon);
                    SetMenuButtonState(menuPreferencesButtonPanel, menuPreferencesButtonLabel, menuPreferencesIconLabel, Resources.PreferencesIcon);
                    SetMenuButtonState(menuUpdatesButtonPanel, menuUpdatesButtonLabel, menuUpdatesIconLabel, _updateIcon, true);



                    // TODO CHECK IF WE HAVE UPDATES

                    if (_updatePending)

                    {

                        screenTabs.SelectTab(updatesTab);

                    }

                    else

                    {

                        if (!string.IsNullOrEmpty(Settings.IgnoreVersion))

                        {

                            noUpdateIgnoreVersionLabel.Text = "Client.NoUpdate.IgnoredVersion".Translate(Settings.IgnoreVersion);



                        }

                        else

                        {

                            noUpdateIgnoreVersionLabel.Text = string.Empty;

                        }



                        noUpdateNextUpdateCheckLabel.Text = UpdatesScreen.GetNextCheckText();



                        screenTabs.SelectTab(noUpdateTab);

                    }



                    _currentState = Instance.State.Updates;

                    break;






                case Instance.State.About:



                    // Update Menu (No Selection)

                    SetMenuButtonState(menuProcessButtonPanel, menuProcessButtonLabel, menuProcessIconLabel, Resources.ProcessIcon);

                    SetMenuButtonState(menuPreferencesButtonPanel, menuPreferencesButtonLabel, menuPreferencesIconLabel, Resources.PreferencesIcon);

                    SetMenuButtonState(menuUpdatesButtonPanel, menuUpdatesButtonLabel, menuUpdatesIconLabel, _updateIcon);



                    // Set Versions

                    aboutClientVersionLabel.Text = AboutScreen.ClientVersionString(Instance.Profile);

                    aboutClientLibraryVersionLabel.Text = AboutScreen.ClientLibraryVersionString(Instance.Profile);

                    aboutCoreLibraryVersionLabel.Text = AboutScreen.CoreLibraryVersionString(Instance.Profile);



                    // Set Debug Information

                    aboutSystemInformationText.Text = AboutScreen.DebugInformation();



                    // Show Tab

                    screenTabs.SelectTab(aboutTab);

                    _currentState = Instance.State.About;

                    break;

                case Instance.State.Blank:                    

                    SetMenuButtonState(menuProcessButtonPanel, menuProcessButtonLabel, menuProcessIconLabel, Resources.ProcessIcon);

                    SetMenuButtonState(menuPreferencesButtonPanel, menuPreferencesButtonLabel, menuPreferencesIconLabel, Resources.PreferencesIcon);

                    SetMenuButtonState(menuUpdatesButtonPanel, menuUpdatesButtonLabel, menuUpdatesIconLabel, _updateIcon);



                    screenTabs.SelectTab(blankTab);

                    _currentState = Instance.State.Blank;

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

            MessageBox.Show(message, "Galileo".Translate(), MessageBoxButtons.OK, MessageBoxIcon.Information);

            Instance.Log("Client.Win.MainForm.Alert", message);

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

            var logEntries = Instance.Hunters[_huntIndex].Log.Reverse().ToArray();

            for (int i = 0; i < logEntries.Length; i++)

            {

                logEntries[i] = logEntries[i].TrimEnd(Localization.LocalizationCache.LineEndings);

            }

            processLogRichText.Lines = logEntries;

            processLogRichText.SelectionStart = processLogRichText.Text.Length;

            processLogRichText.ScrollToCaret();



            processTargetText.Text = Instance.Hunters[_huntIndex].WorkingDirectory;



            // Update Config

            UpdateProcessConfigTable(_huntIndex);



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

                progressTotalProgressBar.Value = (int)(Instance.Hunters[_huntIndex].ProgressPercentage * 100);

                progressTotalProgressBar.Visible = true;

                processProcessButton.Text = "Client.Process.Cancel".Translate();

                processTargetText.Enabled = false;

                processTargetButton.Enabled = false;

            }

            else

            {

                progressTotalProgressBar.Visible = false;

                progressTotalProgressBar.Value = 0;

                processProcessButton.Text = "Client.Process.Process".Translate();

                processTargetText.Enabled = true;

                processTargetButton.Enabled = true;

            }

        }


        /// <summary>

        /// Set the state of our "posh" menu buttons

        /// </summary>

        /// <param name="background">Item background reference</param>

        /// <param name="text">Item text reference</param>

        /// <param name="icon">Item icon reference</param>

        /// <param name="iconName">Name of icon resource to use</param>

        /// <param name="selected">If item is selected?</param>

        void SetMenuButtonState(Panel background, Label text, Label icon, string iconName = Resources.ActivateIcon, bool selected = false)

        {

            if (selected)

            {

                background.BackColor = _colorOrange;

                text.ForeColor = Color.White;

                icon.ForeColor = Color.White;

            }

            else

            {

                background.BackColor = Color.FromArgb(0, 0, 0, 0);

                text.ForeColor = _colorLightBlue;

                icon.ForeColor = _colorMenuOrange;

            }



            // Update Icon

            icon.Text = iconName;

        }



        /// <summary>

        /// Update the Config Table with the the relevant configuration.

        /// </summary>

        /// <param name="folder">The folder where to look for, and store a config file.</param>

        void UpdateProcessConfigTable(string huntIndex)

        {

            // Clear previous delegate

            processPropertyGrid.PropertyValueChanged -= delegate { };



            _processConfigTemp = Instance.Hunters[huntIndex].GetConfigFromSession();



            processPropertyGrid.SelectedObject = _processConfigTemp;



            // Create delegate 

            processPropertyGrid.PropertyValueChanged += delegate

            {

                Instance.Hunters[huntIndex].UpdateSessionConfig(_processConfigTemp);

            };

        }



        #endregion

    }

}