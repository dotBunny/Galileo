namespace Galileo.Client.Win
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Panel panelMenu;
            System.Windows.Forms.Panel panelSystemDivider;
            System.Windows.Forms.TableLayoutPanel preferencesTable;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
            System.Windows.Forms.TableLayoutPanel preferencesDefaultsTable;
            System.Windows.Forms.Panel preferencesDefaultsTargetFolderInsetPanel;
            System.Windows.Forms.TableLayoutPanel preferencesUpdatesTable;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
            System.Windows.Forms.TableLayoutPanel updatesTable;
            System.Windows.Forms.TableLayoutPanel updatesHeaderTable;
            System.Windows.Forms.TableLayoutPanel noUpdateTable;
            System.Windows.Forms.TableLayoutPanel noUpdateFooterTable;
            System.Windows.Forms.TableLayoutPanel processTable;
            System.Windows.Forms.TableLayoutPanel processTargetFolderTable;
            System.Windows.Forms.Panel processTargetFolderInsetPanel;
            System.Windows.Forms.TableLayoutPanel processFooterTable;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMenuHeader = new System.Windows.Forms.Panel();
            this.menuBranding = new System.Windows.Forms.PictureBox();
            this.menuVersionLabel = new System.Windows.Forms.Label();
            this.menuProcessButtonPanel = new System.Windows.Forms.Panel();
            this.menuProcessButtonLabel = new System.Windows.Forms.Label();
            this.menuProcessIconLabel = new System.Windows.Forms.Label();
            this.pictureSystemDividerLine = new System.Windows.Forms.PictureBox();
            this.menuSystemSectionLabel = new System.Windows.Forms.Label();
            this.menuPreferencesButtonPanel = new System.Windows.Forms.Panel();
            this.menuPreferencesButtonLabel = new System.Windows.Forms.Label();
            this.menuPreferencesIconLabel = new System.Windows.Forms.Label();
            this.menuUpdatesButtonPanel = new System.Windows.Forms.Panel();
            this.menuUpdatesButtonLabel = new System.Windows.Forms.Label();
            this.menuUpdatesIconLabel = new System.Windows.Forms.Label();
            this.preferencesTitleLabel = new System.Windows.Forms.Label();
            this.preferencesTabs = new System.Windows.Forms.TabControl();
            this.preferencesGeneralTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPreferencesGeneral = new System.Windows.Forms.TableLayoutPanel();
            this.preferencesGeneralRestoreDefaultsButton = new System.Windows.Forms.Button();
            this.preferencesGeneralReportAutomaticOpenCheck = new System.Windows.Forms.CheckBox();
            this.preferencesGeneralSendUsageDataCheck = new System.Windows.Forms.CheckBox();
            this.preferencesGeneralDataExplanationLabel = new System.Windows.Forms.Label();
            this.preferencesGeneralLocaleLabel = new System.Windows.Forms.Label();
            this.preferencesGeneralLocaleCombo = new System.Windows.Forms.ComboBox();
            this.preferencesGeneralMessageLabel = new System.Windows.Forms.Label();
            this.preferencesGeneralDividerPicture = new System.Windows.Forms.PictureBox();
            this.preferencesProcessTab = new System.Windows.Forms.TabPage();
            this.preferencesDefaultsFooterTable = new System.Windows.Forms.TableLayoutPanel();
            this.preferencesDefaultsExportButton = new System.Windows.Forms.Button();
            this.preferencesDefaultsRestoreDefaultsButton = new System.Windows.Forms.Button();
            this.preferencesDefaultsImportButton = new System.Windows.Forms.Button();
            this.preferencesDefaultsGridPanel = new System.Windows.Forms.Panel();
            this.preferencesDefaultsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.preferencesDefaultsMessageLabel = new System.Windows.Forms.Label();
            this.preferencesDefaultsTargetFolderTable = new System.Windows.Forms.TableLayoutPanel();
            this.preferencesDefaultsTargetFolderLabel = new System.Windows.Forms.Label();
            this.preferencesDefaultsTargetFolderButton = new System.Windows.Forms.Button();
            this.preferencesDefaultsTargetFolderPanel = new System.Windows.Forms.Panel();
            this.preferencesDefaultsTargetFolderText = new System.Windows.Forms.TextBox();
            this.preferencesUpdatesTab = new System.Windows.Forms.TabPage();
            this.preferencesUpdatesCheckFrequencyLabel = new System.Windows.Forms.Label();
            this.preferencesUpdatesChannelCombo = new System.Windows.Forms.ComboBox();
            this.preferencesUpdatesDividerPicture = new System.Windows.Forms.PictureBox();
            this.preferencesUpdatesEnableYesButton = new System.Windows.Forms.RadioButton();
            this.preferencesUpdatesEnableNoButton = new System.Windows.Forms.RadioButton();
            this.preferencesUpdatesEnableLabel = new System.Windows.Forms.Label();
            this.preferencesUpdatesFrequencyCombo = new System.Windows.Forms.ComboBox();
            this.preferencesUpdateChannelLabel = new System.Windows.Forms.Label();
            this.preferencesUpdatesRestoreDefaultsButton = new System.Windows.Forms.Button();
            this.preferencesUpdatesMessageLabel = new System.Windows.Forms.Label();
            this.updatesMessageLabel = new System.Windows.Forms.Label();
            this.updatesTextBackgroundPanel = new System.Windows.Forms.Panel();
            this.updatesChangelogRichText = new System.Windows.Forms.RichTextBox();
            this.updatesDateLabel = new System.Windows.Forms.Label();
            this.updatesTitleLabel = new System.Windows.Forms.Label();
            this.updatesFooterTable = new System.Windows.Forms.TableLayoutPanel();
            this.updatesDownloadButton = new System.Windows.Forms.Button();
            this.updatesIgnoreButton = new System.Windows.Forms.Button();
            this.noUpdateTitleLabel = new System.Windows.Forms.Label();
            this.noUpdateIgnoreVersionLabel = new System.Windows.Forms.Label();
            this.noUpdateCheckForUpdatesButton = new System.Windows.Forms.Button();
            this.noUpdateMessageLabel = new System.Windows.Forms.Label();
            this.noUpdateNextUpdateCheckLabel = new System.Windows.Forms.Label();
            this.processMessageLabel = new System.Windows.Forms.Label();
            this.processTitleLabel = new System.Windows.Forms.Label();
            this.processReminderLabel = new System.Windows.Forms.Label();
            this.processTabs = new System.Windows.Forms.TabControl();
            this.processLogTab = new System.Windows.Forms.TabPage();
            this.processLogRichText = new System.Windows.Forms.RichTextBox();
            this.processOptionsTab = new System.Windows.Forms.TabPage();
            this.processPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.processTargetButton = new System.Windows.Forms.Button();
            this.processTargetFolderPanel = new System.Windows.Forms.Panel();
            this.processTargetText = new System.Windows.Forms.TextBox();
            this.processReportButton = new System.Windows.Forms.Button();
            this.processProcessButton = new System.Windows.Forms.Button();
            this.progressTotalProgressBar = new System.Windows.Forms.ProgressBar();
            this.aboutClientVersionLabel = new System.Windows.Forms.Label();
            this.aboutClientInfoLabel = new System.Windows.Forms.Label();
            this.aboutSystemInfoLabel = new System.Windows.Forms.Label();
            this.aboutTitleLabel = new System.Windows.Forms.Label();
            this.aboutMessageLabel = new System.Windows.Forms.Label();
            this.aboutThirdPartyLicensesButton = new System.Windows.Forms.Button();
            this.aboutEULAButton = new System.Windows.Forms.Button();
            this.aboutLogsButton = new System.Windows.Forms.Button();
            this.screenTabs = new System.Windows.Forms.TabControl();
            this.processTab = new System.Windows.Forms.TabPage();
            this.preferencesTab = new System.Windows.Forms.TabPage();
            this.updatesTab = new System.Windows.Forms.TabPage();
            this.noUpdateTab = new System.Windows.Forms.TabPage();
            this.aboutTab = new System.Windows.Forms.TabPage();
            this.aboutTable = new System.Windows.Forms.TableLayoutPanel();
            this.aboutCoreLibraryVersionLabel = new System.Windows.Forms.Label();
            this.aboutClientLibraryVersionLabel = new System.Windows.Forms.Label();
            this.aboutSystemInfoPanel = new System.Windows.Forms.Panel();
            this.aboutSystemInformationText = new System.Windows.Forms.RichTextBox();
            this.aboutFooterTable = new System.Windows.Forms.TableLayoutPanel();
            this.blankTab = new System.Windows.Forms.TabPage();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            panelMenu = new System.Windows.Forms.Panel();
            panelSystemDivider = new System.Windows.Forms.Panel();
            preferencesTable = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            preferencesDefaultsTable = new System.Windows.Forms.TableLayoutPanel();
            preferencesDefaultsTargetFolderInsetPanel = new System.Windows.Forms.Panel();
            preferencesUpdatesTable = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            updatesTable = new System.Windows.Forms.TableLayoutPanel();
            updatesHeaderTable = new System.Windows.Forms.TableLayoutPanel();
            noUpdateTable = new System.Windows.Forms.TableLayoutPanel();
            noUpdateFooterTable = new System.Windows.Forms.TableLayoutPanel();
            processTable = new System.Windows.Forms.TableLayoutPanel();
            processTargetFolderTable = new System.Windows.Forms.TableLayoutPanel();
            processTargetFolderInsetPanel = new System.Windows.Forms.Panel();
            processFooterTable = new System.Windows.Forms.TableLayoutPanel();
            panelMenu.SuspendLayout();
            this.menuFlowPanel.SuspendLayout();
            this.panelMenuHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuBranding)).BeginInit();
            this.menuProcessButtonPanel.SuspendLayout();
            panelSystemDivider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSystemDividerLine)).BeginInit();
            this.menuPreferencesButtonPanel.SuspendLayout();
            this.menuUpdatesButtonPanel.SuspendLayout();
            preferencesTable.SuspendLayout();
            this.preferencesTabs.SuspendLayout();
            this.preferencesGeneralTab.SuspendLayout();
            this.tableLayoutPreferencesGeneral.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preferencesGeneralDividerPicture)).BeginInit();
            this.preferencesProcessTab.SuspendLayout();
            preferencesDefaultsTable.SuspendLayout();
            this.preferencesDefaultsFooterTable.SuspendLayout();
            this.preferencesDefaultsGridPanel.SuspendLayout();
            this.preferencesDefaultsTargetFolderTable.SuspendLayout();
            this.preferencesDefaultsTargetFolderPanel.SuspendLayout();
            preferencesDefaultsTargetFolderInsetPanel.SuspendLayout();
            this.preferencesUpdatesTab.SuspendLayout();
            preferencesUpdatesTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preferencesUpdatesDividerPicture)).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            updatesTable.SuspendLayout();
            this.updatesTextBackgroundPanel.SuspendLayout();
            updatesHeaderTable.SuspendLayout();
            this.updatesFooterTable.SuspendLayout();
            noUpdateTable.SuspendLayout();
            noUpdateFooterTable.SuspendLayout();
            processTable.SuspendLayout();
            this.processTabs.SuspendLayout();
            this.processLogTab.SuspendLayout();
            this.processOptionsTab.SuspendLayout();
            processTargetFolderTable.SuspendLayout();
            this.processTargetFolderPanel.SuspendLayout();
            processTargetFolderInsetPanel.SuspendLayout();
            processFooterTable.SuspendLayout();
            this.screenTabs.SuspendLayout();
            this.processTab.SuspendLayout();
            this.preferencesTab.SuspendLayout();
            this.updatesTab.SuspendLayout();
            this.noUpdateTab.SuspendLayout();
            this.aboutTab.SuspendLayout();
            this.aboutTable.SuspendLayout();
            this.aboutSystemInfoPanel.SuspendLayout();
            this.aboutFooterTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.AutoSize = true;
            panelMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(40)))));
            panelMenu.Controls.Add(this.menuFlowPanel);
            panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            panelMenu.Location = new System.Drawing.Point(0, 0);
            panelMenu.Margin = new System.Windows.Forms.Padding(0);
            panelMenu.MaximumSize = new System.Drawing.Size(412, 0);
            panelMenu.MinimumSize = new System.Drawing.Size(412, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new System.Drawing.Size(412, 709);
            panelMenu.TabIndex = 1;
            // 
            // menuFlowPanel
            // 
            this.menuFlowPanel.AutoSize = true;
            this.menuFlowPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.menuFlowPanel.BackColor = System.Drawing.Color.Transparent;
            this.menuFlowPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuFlowPanel.Controls.Add(this.panelMenuHeader);
            this.menuFlowPanel.Controls.Add(this.menuProcessButtonPanel);
            this.menuFlowPanel.Controls.Add(panelSystemDivider);
            this.menuFlowPanel.Controls.Add(this.menuPreferencesButtonPanel);
            this.menuFlowPanel.Controls.Add(this.menuUpdatesButtonPanel);
            this.menuFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuFlowPanel.Location = new System.Drawing.Point(0, 0);
            this.menuFlowPanel.Margin = new System.Windows.Forms.Padding(0);
            this.menuFlowPanel.MaximumSize = new System.Drawing.Size(412, 0);
            this.menuFlowPanel.MinimumSize = new System.Drawing.Size(412, 538);
            this.menuFlowPanel.Name = "menuFlowPanel";
            this.menuFlowPanel.Size = new System.Drawing.Size(412, 709);
            this.menuFlowPanel.TabIndex = 3;
            this.menuFlowPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MenuFlowPanel_Paint);
            // 
            // panelMenuHeader
            // 
            this.panelMenuHeader.Controls.Add(this.menuBranding);
            this.panelMenuHeader.Controls.Add(this.menuVersionLabel);
            this.panelMenuHeader.Location = new System.Drawing.Point(0, 0);
            this.panelMenuHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panelMenuHeader.Name = "panelMenuHeader";
            this.panelMenuHeader.Size = new System.Drawing.Size(412, 123);
            this.panelMenuHeader.TabIndex = 4;
            // 
            // menuBranding
            // 
            this.menuBranding.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.menuBranding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuBranding.BackColor = System.Drawing.Color.Transparent;
            this.menuBranding.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.menuBranding.Image = global::Galileo.Client.Win.Properties.Resources.GalileoText;
            this.menuBranding.InitialImage = null;
            this.menuBranding.Location = new System.Drawing.Point(225, 18);
            this.menuBranding.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.menuBranding.Name = "menuBranding";
            this.menuBranding.Size = new System.Drawing.Size(183, 55);
            this.menuBranding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.menuBranding.TabIndex = 0;
            this.menuBranding.TabStop = false;
            this.menuBranding.Click += new System.EventHandler(this.MenuBranding_Click);
            // 
            // menuVersionLabel
            // 
            this.menuVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuVersionLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuVersionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.menuVersionLabel.Location = new System.Drawing.Point(166, 80);
            this.menuVersionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.menuVersionLabel.Name = "menuVersionLabel";
            this.menuVersionLabel.Size = new System.Drawing.Size(234, 35);
            this.menuVersionLabel.TabIndex = 2;
            this.menuVersionLabel.Text = "2018.1";
            this.menuVersionLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // menuProcessButtonPanel
            // 
            this.menuProcessButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.menuProcessButtonPanel.Controls.Add(this.menuProcessButtonLabel);
            this.menuProcessButtonPanel.Controls.Add(this.menuProcessIconLabel);
            this.menuProcessButtonPanel.Location = new System.Drawing.Point(0, 123);
            this.menuProcessButtonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.menuProcessButtonPanel.Name = "menuProcessButtonPanel";
            this.menuProcessButtonPanel.Size = new System.Drawing.Size(412, 85);
            this.menuProcessButtonPanel.TabIndex = 0;
            // 
            // menuProcessButtonLabel
            // 
            this.menuProcessButtonLabel.AutoSize = true;
            this.menuProcessButtonLabel.BackColor = System.Drawing.Color.Transparent;
            this.menuProcessButtonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuProcessButtonLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.menuProcessButtonLabel.Location = new System.Drawing.Point(100, 28);
            this.menuProcessButtonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.menuProcessButtonLabel.Name = "menuProcessButtonLabel";
            this.menuProcessButtonLabel.Size = new System.Drawing.Size(106, 30);
            this.menuProcessButtonLabel.TabIndex = 1;
            this.menuProcessButtonLabel.Text = "Process";
            // 
            // menuProcessIconLabel
            // 
            this.menuProcessIconLabel.BackColor = System.Drawing.Color.Transparent;
            this.menuProcessIconLabel.Location = new System.Drawing.Point(18, 5);
            this.menuProcessIconLabel.Margin = new System.Windows.Forms.Padding(0);
            this.menuProcessIconLabel.Name = "menuProcessIconLabel";
            this.menuProcessIconLabel.Size = new System.Drawing.Size(69, 75);
            this.menuProcessIconLabel.TabIndex = 0;
            this.menuProcessIconLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelSystemDivider
            // 
            panelSystemDivider.Controls.Add(this.pictureSystemDividerLine);
            panelSystemDivider.Controls.Add(this.menuSystemSectionLabel);
            panelSystemDivider.Location = new System.Drawing.Point(0, 208);
            panelSystemDivider.Margin = new System.Windows.Forms.Padding(0);
            panelSystemDivider.Name = "panelSystemDivider";
            panelSystemDivider.Size = new System.Drawing.Size(412, 38);
            panelSystemDivider.TabIndex = 4;
            // 
            // pictureSystemDividerLine
            // 
            this.pictureSystemDividerLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.pictureSystemDividerLine.Location = new System.Drawing.Point(105, 18);
            this.pictureSystemDividerLine.Margin = new System.Windows.Forms.Padding(0);
            this.pictureSystemDividerLine.Name = "pictureSystemDividerLine";
            this.pictureSystemDividerLine.Size = new System.Drawing.Size(288, 2);
            this.pictureSystemDividerLine.TabIndex = 5;
            this.pictureSystemDividerLine.TabStop = false;
            // 
            // menuSystemSectionLabel
            // 
            this.menuSystemSectionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.menuSystemSectionLabel.AutoEllipsis = true;
            this.menuSystemSectionLabel.BackColor = System.Drawing.Color.Transparent;
            this.menuSystemSectionLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuSystemSectionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.menuSystemSectionLabel.Location = new System.Drawing.Point(4, 9);
            this.menuSystemSectionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.menuSystemSectionLabel.Name = "menuSystemSectionLabel";
            this.menuSystemSectionLabel.Size = new System.Drawing.Size(100, 29);
            this.menuSystemSectionLabel.TabIndex = 4;
            this.menuSystemSectionLabel.Text = "SYSTEM";
            this.menuSystemSectionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // menuPreferencesButtonPanel
            // 
            this.menuPreferencesButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.menuPreferencesButtonPanel.Controls.Add(this.menuPreferencesButtonLabel);
            this.menuPreferencesButtonPanel.Controls.Add(this.menuPreferencesIconLabel);
            this.menuPreferencesButtonPanel.Location = new System.Drawing.Point(0, 246);
            this.menuPreferencesButtonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.menuPreferencesButtonPanel.Name = "menuPreferencesButtonPanel";
            this.menuPreferencesButtonPanel.Size = new System.Drawing.Size(412, 85);
            this.menuPreferencesButtonPanel.TabIndex = 3;
            // 
            // menuPreferencesButtonLabel
            // 
            this.menuPreferencesButtonLabel.AutoSize = true;
            this.menuPreferencesButtonLabel.BackColor = System.Drawing.Color.Transparent;
            this.menuPreferencesButtonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuPreferencesButtonLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.menuPreferencesButtonLabel.Location = new System.Drawing.Point(100, 28);
            this.menuPreferencesButtonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.menuPreferencesButtonLabel.Name = "menuPreferencesButtonLabel";
            this.menuPreferencesButtonLabel.Size = new System.Drawing.Size(151, 30);
            this.menuPreferencesButtonLabel.TabIndex = 1;
            this.menuPreferencesButtonLabel.Text = "Preferences";
            // 
            // menuPreferencesIconLabel
            // 
            this.menuPreferencesIconLabel.BackColor = System.Drawing.Color.Transparent;
            this.menuPreferencesIconLabel.Location = new System.Drawing.Point(18, 5);
            this.menuPreferencesIconLabel.Margin = new System.Windows.Forms.Padding(0);
            this.menuPreferencesIconLabel.Name = "menuPreferencesIconLabel";
            this.menuPreferencesIconLabel.Size = new System.Drawing.Size(69, 75);
            this.menuPreferencesIconLabel.TabIndex = 0;
            this.menuPreferencesIconLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuUpdatesButtonPanel
            // 
            this.menuUpdatesButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.menuUpdatesButtonPanel.Controls.Add(this.menuUpdatesButtonLabel);
            this.menuUpdatesButtonPanel.Controls.Add(this.menuUpdatesIconLabel);
            this.menuUpdatesButtonPanel.Location = new System.Drawing.Point(0, 331);
            this.menuUpdatesButtonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.menuUpdatesButtonPanel.Name = "menuUpdatesButtonPanel";
            this.menuUpdatesButtonPanel.Size = new System.Drawing.Size(412, 85);
            this.menuUpdatesButtonPanel.TabIndex = 4;
            // 
            // menuUpdatesButtonLabel
            // 
            this.menuUpdatesButtonLabel.AutoSize = true;
            this.menuUpdatesButtonLabel.BackColor = System.Drawing.Color.Transparent;
            this.menuUpdatesButtonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuUpdatesButtonLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.menuUpdatesButtonLabel.Location = new System.Drawing.Point(100, 28);
            this.menuUpdatesButtonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.menuUpdatesButtonLabel.Name = "menuUpdatesButtonLabel";
            this.menuUpdatesButtonLabel.Size = new System.Drawing.Size(108, 30);
            this.menuUpdatesButtonLabel.TabIndex = 1;
            this.menuUpdatesButtonLabel.Text = "Updates";
            // 
            // menuUpdatesIconLabel
            // 
            this.menuUpdatesIconLabel.BackColor = System.Drawing.Color.Transparent;
            this.menuUpdatesIconLabel.Location = new System.Drawing.Point(18, 5);
            this.menuUpdatesIconLabel.Margin = new System.Windows.Forms.Padding(0);
            this.menuUpdatesIconLabel.Name = "menuUpdatesIconLabel";
            this.menuUpdatesIconLabel.Size = new System.Drawing.Size(69, 75);
            this.menuUpdatesIconLabel.TabIndex = 0;
            this.menuUpdatesIconLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // preferencesTable
            // 
            preferencesTable.AutoSize = true;
            preferencesTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            preferencesTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            preferencesTable.ColumnCount = 1;
            preferencesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            preferencesTable.Controls.Add(this.preferencesTitleLabel, 0, 0);
            preferencesTable.Controls.Add(this.preferencesTabs, 0, 1);
            preferencesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            preferencesTable.Location = new System.Drawing.Point(0, 0);
            preferencesTable.Margin = new System.Windows.Forms.Padding(0);
            preferencesTable.Name = "preferencesTable";
            preferencesTable.RowCount = 2;
            preferencesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            preferencesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            preferencesTable.Size = new System.Drawing.Size(756, 683);
            preferencesTable.TabIndex = 4;
            // 
            // preferencesTitleLabel
            // 
            this.preferencesTitleLabel.AutoSize = true;
            this.preferencesTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferencesTitleLabel.Location = new System.Drawing.Point(30, 45);
            this.preferencesTitleLabel.Margin = new System.Windows.Forms.Padding(30, 45, 30, 0);
            this.preferencesTitleLabel.Name = "preferencesTitleLabel";
            this.preferencesTitleLabel.Size = new System.Drawing.Size(179, 32);
            this.preferencesTitleLabel.TabIndex = 0;
            this.preferencesTitleLabel.Text = "Preferences";
            // 
            // preferencesTabs
            // 
            this.preferencesTabs.Controls.Add(this.preferencesGeneralTab);
            this.preferencesTabs.Controls.Add(this.preferencesProcessTab);
            this.preferencesTabs.Controls.Add(this.preferencesUpdatesTab);
            this.preferencesTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesTabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.preferencesTabs.Location = new System.Drawing.Point(30, 92);
            this.preferencesTabs.Margin = new System.Windows.Forms.Padding(30, 15, 30, 95);
            this.preferencesTabs.Name = "preferencesTabs";
            this.preferencesTabs.Padding = new System.Drawing.Point(20, 8);
            this.preferencesTabs.SelectedIndex = 0;
            this.preferencesTabs.Size = new System.Drawing.Size(696, 496);
            this.preferencesTabs.TabIndex = 0;
            this.preferencesTabs.TabStop = false;
            // 
            // preferencesGeneralTab
            // 
            this.preferencesGeneralTab.BackColor = System.Drawing.Color.White;
            this.preferencesGeneralTab.Controls.Add(this.tableLayoutPreferencesGeneral);
            this.preferencesGeneralTab.Location = new System.Drawing.Point(4, 39);
            this.preferencesGeneralTab.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesGeneralTab.Name = "preferencesGeneralTab";
            this.preferencesGeneralTab.Size = new System.Drawing.Size(688, 453);
            this.preferencesGeneralTab.TabIndex = 0;
            this.preferencesGeneralTab.Text = "General";
            // 
            // tableLayoutPreferencesGeneral
            // 
            this.tableLayoutPreferencesGeneral.ColumnCount = 1;
            this.tableLayoutPreferencesGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPreferencesGeneral.Controls.Add(this.preferencesGeneralRestoreDefaultsButton, 0, 6);
            this.tableLayoutPreferencesGeneral.Controls.Add(this.preferencesGeneralReportAutomaticOpenCheck, 0, 3);
            this.tableLayoutPreferencesGeneral.Controls.Add(this.preferencesGeneralSendUsageDataCheck, 0, 4);
            this.tableLayoutPreferencesGeneral.Controls.Add(this.preferencesGeneralDataExplanationLabel, 0, 5);
            this.tableLayoutPreferencesGeneral.Controls.Add(tableLayoutPanel5, 0, 1);
            this.tableLayoutPreferencesGeneral.Controls.Add(this.preferencesGeneralMessageLabel, 0, 0);
            this.tableLayoutPreferencesGeneral.Controls.Add(this.preferencesGeneralDividerPicture, 0, 2);
            this.tableLayoutPreferencesGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPreferencesGeneral.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPreferencesGeneral.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPreferencesGeneral.Name = "tableLayoutPreferencesGeneral";
            this.tableLayoutPreferencesGeneral.RowCount = 7;
            this.tableLayoutPreferencesGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPreferencesGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPreferencesGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPreferencesGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPreferencesGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPreferencesGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPreferencesGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPreferencesGeneral.Size = new System.Drawing.Size(688, 453);
            this.tableLayoutPreferencesGeneral.TabIndex = 0;
            // 
            // preferencesGeneralRestoreDefaultsButton
            // 
            this.preferencesGeneralRestoreDefaultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesGeneralRestoreDefaultsButton.AutoSize = true;
            this.preferencesGeneralRestoreDefaultsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.preferencesGeneralRestoreDefaultsButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.preferencesGeneralRestoreDefaultsButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.preferencesGeneralRestoreDefaultsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferencesGeneralRestoreDefaultsButton.Location = new System.Drawing.Point(489, 377);
            this.preferencesGeneralRestoreDefaultsButton.Margin = new System.Windows.Forms.Padding(0, 28, 30, 28);
            this.preferencesGeneralRestoreDefaultsButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.preferencesGeneralRestoreDefaultsButton.Name = "preferencesGeneralRestoreDefaultsButton";
            this.preferencesGeneralRestoreDefaultsButton.Size = new System.Drawing.Size(169, 48);
            this.preferencesGeneralRestoreDefaultsButton.TabIndex = 2;
            this.preferencesGeneralRestoreDefaultsButton.Text = "Restore Defaults";
            this.preferencesGeneralRestoreDefaultsButton.UseVisualStyleBackColor = false;
            this.preferencesGeneralRestoreDefaultsButton.Click += new System.EventHandler(this.PreferencesGeneralRestoreDefaultsButton_Click);
            // 
            // preferencesGeneralReportAutomaticOpenCheck
            // 
            this.preferencesGeneralReportAutomaticOpenCheck.AutoSize = true;
            this.preferencesGeneralReportAutomaticOpenCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesGeneralReportAutomaticOpenCheck.Location = new System.Drawing.Point(45, 173);
            this.preferencesGeneralReportAutomaticOpenCheck.Margin = new System.Windows.Forms.Padding(45, 0, 45, 0);
            this.preferencesGeneralReportAutomaticOpenCheck.Name = "preferencesGeneralReportAutomaticOpenCheck";
            this.preferencesGeneralReportAutomaticOpenCheck.Size = new System.Drawing.Size(366, 29);
            this.preferencesGeneralReportAutomaticOpenCheck.TabIndex = 0;
            this.preferencesGeneralReportAutomaticOpenCheck.Text = "Open report when finished processing";
            this.preferencesGeneralReportAutomaticOpenCheck.UseVisualStyleBackColor = true;
            this.preferencesGeneralReportAutomaticOpenCheck.CheckedChanged += new System.EventHandler(this.PreferencesGeneralReportAutomaticOpenCheck_CheckedChanged);
            // 
            // preferencesGeneralSendUsageDataCheck
            // 
            this.preferencesGeneralSendUsageDataCheck.AutoSize = true;
            this.preferencesGeneralSendUsageDataCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesGeneralSendUsageDataCheck.Location = new System.Drawing.Point(45, 217);
            this.preferencesGeneralSendUsageDataCheck.Margin = new System.Windows.Forms.Padding(45, 15, 45, 0);
            this.preferencesGeneralSendUsageDataCheck.Name = "preferencesGeneralSendUsageDataCheck";
            this.preferencesGeneralSendUsageDataCheck.Size = new System.Drawing.Size(294, 29);
            this.preferencesGeneralSendUsageDataCheck.TabIndex = 1;
            this.preferencesGeneralSendUsageDataCheck.Text = "Send anonymous usage data";
            this.preferencesGeneralSendUsageDataCheck.UseVisualStyleBackColor = true;
            this.preferencesGeneralSendUsageDataCheck.CheckedChanged += new System.EventHandler(this.PreferencesGeneralSendUsageDataCheck_CheckedChanged);
            // 
            // preferencesGeneralDataExplanationLabel
            // 
            this.preferencesGeneralDataExplanationLabel.AutoSize = true;
            this.preferencesGeneralDataExplanationLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.preferencesGeneralDataExplanationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.preferencesGeneralDataExplanationLabel.Location = new System.Drawing.Point(69, 252);
            this.preferencesGeneralDataExplanationLabel.Margin = new System.Windows.Forms.Padding(69, 6, 30, 15);
            this.preferencesGeneralDataExplanationLabel.Name = "preferencesGeneralDataExplanationLabel";
            this.preferencesGeneralDataExplanationLabel.Size = new System.Drawing.Size(589, 40);
            this.preferencesGeneralDataExplanationLabel.TabIndex = 9;
            this.preferencesGeneralDataExplanationLabel.Text = "This information focuses on how individual checks are performing and does not con" +
    "tain any student identifiable information.";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            tableLayoutPanel5.Controls.Add(this.preferencesGeneralLocaleLabel, 0, 0);
            tableLayoutPanel5.Controls.Add(this.preferencesGeneralLocaleCombo, 1, 0);
            tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel5.Location = new System.Drawing.Point(0, 96);
            tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0, 15, 0, 15);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel5.Size = new System.Drawing.Size(688, 37);
            tableLayoutPanel5.TabIndex = 10;
            // 
            // preferencesGeneralLocaleLabel
            // 
            this.preferencesGeneralLocaleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesGeneralLocaleLabel.Location = new System.Drawing.Point(0, 0);
            this.preferencesGeneralLocaleLabel.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesGeneralLocaleLabel.MaximumSize = new System.Drawing.Size(240, 37);
            this.preferencesGeneralLocaleLabel.Name = "preferencesGeneralLocaleLabel";
            this.preferencesGeneralLocaleLabel.Size = new System.Drawing.Size(240, 37);
            this.preferencesGeneralLocaleLabel.TabIndex = 0;
            this.preferencesGeneralLocaleLabel.Text = "Locale:";
            this.preferencesGeneralLocaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // preferencesGeneralLocaleCombo
            // 
            this.preferencesGeneralLocaleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.preferencesGeneralLocaleCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesGeneralLocaleCombo.FormattingEnabled = true;
            this.preferencesGeneralLocaleCombo.Location = new System.Drawing.Point(240, 0);
            this.preferencesGeneralLocaleCombo.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesGeneralLocaleCombo.Name = "preferencesGeneralLocaleCombo";
            this.preferencesGeneralLocaleCombo.Size = new System.Drawing.Size(180, 33);
            this.preferencesGeneralLocaleCombo.TabIndex = 1;
            this.preferencesGeneralLocaleCombo.SelectedIndexChanged += new System.EventHandler(this.PreferencesGeneralLocaleCombo_SelectedIndexChanged);
            // 
            // preferencesGeneralMessageLabel
            // 
            this.preferencesGeneralMessageLabel.AutoSize = true;
            this.preferencesGeneralMessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesGeneralMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesGeneralMessageLabel.Location = new System.Drawing.Point(30, 31);
            this.preferencesGeneralMessageLabel.Margin = new System.Windows.Forms.Padding(30, 31, 30, 0);
            this.preferencesGeneralMessageLabel.Name = "preferencesGeneralMessageLabel";
            this.preferencesGeneralMessageLabel.Size = new System.Drawing.Size(628, 50);
            this.preferencesGeneralMessageLabel.TabIndex = 11;
            this.preferencesGeneralMessageLabel.Text = "Galileo is language agnostic, and can be tailored to your own customized experien" +
    "ce.";
            // 
            // preferencesGeneralDividerPicture
            // 
            this.preferencesGeneralDividerPicture.BackColor = System.Drawing.Color.DarkGray;
            this.preferencesGeneralDividerPicture.Dock = System.Windows.Forms.DockStyle.Top;
            this.preferencesGeneralDividerPicture.Location = new System.Drawing.Point(30, 148);
            this.preferencesGeneralDividerPicture.Margin = new System.Windows.Forms.Padding(30, 0, 30, 23);
            this.preferencesGeneralDividerPicture.MaximumSize = new System.Drawing.Size(0, 2);
            this.preferencesGeneralDividerPicture.Name = "preferencesGeneralDividerPicture";
            this.preferencesGeneralDividerPicture.Size = new System.Drawing.Size(628, 2);
            this.preferencesGeneralDividerPicture.TabIndex = 12;
            this.preferencesGeneralDividerPicture.TabStop = false;
            // 
            // preferencesProcessTab
            // 
            this.preferencesProcessTab.BackColor = System.Drawing.Color.White;
            this.preferencesProcessTab.Controls.Add(preferencesDefaultsTable);
            this.preferencesProcessTab.Location = new System.Drawing.Point(4, 39);
            this.preferencesProcessTab.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesProcessTab.Name = "preferencesProcessTab";
            this.preferencesProcessTab.Size = new System.Drawing.Size(688, 453);
            this.preferencesProcessTab.TabIndex = 1;
            this.preferencesProcessTab.Text = "Defaults";
            // 
            // preferencesDefaultsTable
            // 
            preferencesDefaultsTable.AutoSize = true;
            preferencesDefaultsTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            preferencesDefaultsTable.ColumnCount = 1;
            preferencesDefaultsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            preferencesDefaultsTable.Controls.Add(this.preferencesDefaultsFooterTable, 0, 3);
            preferencesDefaultsTable.Controls.Add(this.preferencesDefaultsGridPanel, 0, 2);
            preferencesDefaultsTable.Controls.Add(this.preferencesDefaultsMessageLabel, 0, 0);
            preferencesDefaultsTable.Controls.Add(this.preferencesDefaultsTargetFolderTable, 0, 1);
            preferencesDefaultsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            preferencesDefaultsTable.Location = new System.Drawing.Point(0, 0);
            preferencesDefaultsTable.Margin = new System.Windows.Forms.Padding(0);
            preferencesDefaultsTable.Name = "preferencesDefaultsTable";
            preferencesDefaultsTable.RowCount = 4;
            preferencesDefaultsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            preferencesDefaultsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            preferencesDefaultsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            preferencesDefaultsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            preferencesDefaultsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            preferencesDefaultsTable.Size = new System.Drawing.Size(688, 453);
            preferencesDefaultsTable.TabIndex = 0;
            // 
            // preferencesDefaultsFooterTable
            // 
            this.preferencesDefaultsFooterTable.AutoSize = true;
            this.preferencesDefaultsFooterTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.preferencesDefaultsFooterTable.ColumnCount = 3;
            this.preferencesDefaultsFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.preferencesDefaultsFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.preferencesDefaultsFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.preferencesDefaultsFooterTable.Controls.Add(this.preferencesDefaultsExportButton, 1, 0);
            this.preferencesDefaultsFooterTable.Controls.Add(this.preferencesDefaultsRestoreDefaultsButton, 2, 0);
            this.preferencesDefaultsFooterTable.Controls.Add(this.preferencesDefaultsImportButton, 0, 0);
            this.preferencesDefaultsFooterTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesDefaultsFooterTable.Location = new System.Drawing.Point(0, 349);
            this.preferencesDefaultsFooterTable.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesDefaultsFooterTable.Name = "preferencesDefaultsFooterTable";
            this.preferencesDefaultsFooterTable.RowCount = 1;
            this.preferencesDefaultsFooterTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.preferencesDefaultsFooterTable.Size = new System.Drawing.Size(688, 104);
            this.preferencesDefaultsFooterTable.TabIndex = 3;
            // 
            // preferencesDefaultsExportButton
            // 
            this.preferencesDefaultsExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.preferencesDefaultsExportButton.AutoSize = true;
            this.preferencesDefaultsExportButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.preferencesDefaultsExportButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.preferencesDefaultsExportButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.preferencesDefaultsExportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferencesDefaultsExportButton.Location = new System.Drawing.Point(125, 28);
            this.preferencesDefaultsExportButton.Margin = new System.Windows.Forms.Padding(15, 28, 0, 28);
            this.preferencesDefaultsExportButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.preferencesDefaultsExportButton.Name = "preferencesDefaultsExportButton";
            this.preferencesDefaultsExportButton.Size = new System.Drawing.Size(82, 48);
            this.preferencesDefaultsExportButton.TabIndex = 5;
            this.preferencesDefaultsExportButton.Text = "Export";
            this.preferencesDefaultsExportButton.UseVisualStyleBackColor = false;
            this.preferencesDefaultsExportButton.Click += new System.EventHandler(this.PreferencesDefaultsExportButton_Click);
            // 
            // preferencesDefaultsRestoreDefaultsButton
            // 
            this.preferencesDefaultsRestoreDefaultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesDefaultsRestoreDefaultsButton.AutoSize = true;
            this.preferencesDefaultsRestoreDefaultsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.preferencesDefaultsRestoreDefaultsButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.preferencesDefaultsRestoreDefaultsButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.preferencesDefaultsRestoreDefaultsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferencesDefaultsRestoreDefaultsButton.Location = new System.Drawing.Point(489, 28);
            this.preferencesDefaultsRestoreDefaultsButton.Margin = new System.Windows.Forms.Padding(0, 28, 30, 28);
            this.preferencesDefaultsRestoreDefaultsButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.preferencesDefaultsRestoreDefaultsButton.Name = "preferencesDefaultsRestoreDefaultsButton";
            this.preferencesDefaultsRestoreDefaultsButton.Size = new System.Drawing.Size(169, 48);
            this.preferencesDefaultsRestoreDefaultsButton.TabIndex = 6;
            this.preferencesDefaultsRestoreDefaultsButton.Text = "Restore Defaults";
            this.preferencesDefaultsRestoreDefaultsButton.UseVisualStyleBackColor = false;
            this.preferencesDefaultsRestoreDefaultsButton.Click += new System.EventHandler(this.PreferencesDefaultsRestoreDefaultsButton_Click);
            // 
            // preferencesDefaultsImportButton
            // 
            this.preferencesDefaultsImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.preferencesDefaultsImportButton.AutoSize = true;
            this.preferencesDefaultsImportButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.preferencesDefaultsImportButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.preferencesDefaultsImportButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.preferencesDefaultsImportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferencesDefaultsImportButton.Location = new System.Drawing.Point(30, 28);
            this.preferencesDefaultsImportButton.Margin = new System.Windows.Forms.Padding(30, 28, 0, 28);
            this.preferencesDefaultsImportButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.preferencesDefaultsImportButton.Name = "preferencesDefaultsImportButton";
            this.preferencesDefaultsImportButton.Size = new System.Drawing.Size(80, 48);
            this.preferencesDefaultsImportButton.TabIndex = 4;
            this.preferencesDefaultsImportButton.Text = "Import";
            this.preferencesDefaultsImportButton.UseVisualStyleBackColor = false;
            this.preferencesDefaultsImportButton.Click += new System.EventHandler(this.PreferencesDefaultsImportButton_Click);
            // 
            // preferencesDefaultsGridPanel
            // 
            this.preferencesDefaultsGridPanel.AutoSize = true;
            this.preferencesDefaultsGridPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.preferencesDefaultsGridPanel.Controls.Add(this.preferencesDefaultsPropertyGrid);
            this.preferencesDefaultsGridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesDefaultsGridPanel.Location = new System.Drawing.Point(30, 148);
            this.preferencesDefaultsGridPanel.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.preferencesDefaultsGridPanel.Name = "preferencesDefaultsGridPanel";
            this.preferencesDefaultsGridPanel.Padding = new System.Windows.Forms.Padding(2);
            this.preferencesDefaultsGridPanel.Size = new System.Drawing.Size(628, 201);
            this.preferencesDefaultsGridPanel.TabIndex = 4;
            // 
            // preferencesDefaultsPropertyGrid
            // 
            this.preferencesDefaultsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesDefaultsPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.preferencesDefaultsPropertyGrid.Location = new System.Drawing.Point(2, 2);
            this.preferencesDefaultsPropertyGrid.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesDefaultsPropertyGrid.Name = "preferencesDefaultsPropertyGrid";
            this.preferencesDefaultsPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.preferencesDefaultsPropertyGrid.Size = new System.Drawing.Size(624, 197);
            this.preferencesDefaultsPropertyGrid.TabIndex = 1;
            this.preferencesDefaultsPropertyGrid.ToolbarVisible = false;
            // 
            // preferencesDefaultsMessageLabel
            // 
            this.preferencesDefaultsMessageLabel.AutoSize = true;
            this.preferencesDefaultsMessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesDefaultsMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesDefaultsMessageLabel.Location = new System.Drawing.Point(30, 31);
            this.preferencesDefaultsMessageLabel.Margin = new System.Windows.Forms.Padding(30, 31, 30, 0);
            this.preferencesDefaultsMessageLabel.Name = "preferencesDefaultsMessageLabel";
            this.preferencesDefaultsMessageLabel.Size = new System.Drawing.Size(628, 50);
            this.preferencesDefaultsMessageLabel.TabIndex = 5;
            this.preferencesDefaultsMessageLabel.Text = "The default process options used by Galileo when evaluating submissions.";
            // 
            // preferencesDefaultsTargetFolderTable
            // 
            this.preferencesDefaultsTargetFolderTable.AutoSize = true;
            this.preferencesDefaultsTargetFolderTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.preferencesDefaultsTargetFolderTable.ColumnCount = 3;
            this.preferencesDefaultsTargetFolderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.preferencesDefaultsTargetFolderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.preferencesDefaultsTargetFolderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.preferencesDefaultsTargetFolderTable.Controls.Add(this.preferencesDefaultsTargetFolderLabel, 0, 0);
            this.preferencesDefaultsTargetFolderTable.Controls.Add(this.preferencesDefaultsTargetFolderButton, 2, 0);
            this.preferencesDefaultsTargetFolderTable.Controls.Add(this.preferencesDefaultsTargetFolderPanel, 1, 0);
            this.preferencesDefaultsTargetFolderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesDefaultsTargetFolderTable.Location = new System.Drawing.Point(30, 96);
            this.preferencesDefaultsTargetFolderTable.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.preferencesDefaultsTargetFolderTable.MaximumSize = new System.Drawing.Size(0, 37);
            this.preferencesDefaultsTargetFolderTable.MinimumSize = new System.Drawing.Size(0, 37);
            this.preferencesDefaultsTargetFolderTable.Name = "preferencesDefaultsTargetFolderTable";
            this.preferencesDefaultsTargetFolderTable.RowCount = 1;
            this.preferencesDefaultsTargetFolderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.preferencesDefaultsTargetFolderTable.Size = new System.Drawing.Size(628, 37);
            this.preferencesDefaultsTargetFolderTable.TabIndex = 6;
            // 
            // preferencesDefaultsTargetFolderLabel
            // 
            this.preferencesDefaultsTargetFolderLabel.AutoSize = true;
            this.preferencesDefaultsTargetFolderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesDefaultsTargetFolderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesDefaultsTargetFolderLabel.Location = new System.Drawing.Point(0, 0);
            this.preferencesDefaultsTargetFolderLabel.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesDefaultsTargetFolderLabel.Name = "preferencesDefaultsTargetFolderLabel";
            this.preferencesDefaultsTargetFolderLabel.Size = new System.Drawing.Size(135, 37);
            this.preferencesDefaultsTargetFolderLabel.TabIndex = 0;
            this.preferencesDefaultsTargetFolderLabel.Text = "Target Folder:";
            this.preferencesDefaultsTargetFolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // preferencesDefaultsTargetFolderButton
            // 
            this.preferencesDefaultsTargetFolderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesDefaultsTargetFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.preferencesDefaultsTargetFolderButton.Location = new System.Drawing.Point(590, 0);
            this.preferencesDefaultsTargetFolderButton.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesDefaultsTargetFolderButton.Name = "preferencesDefaultsTargetFolderButton";
            this.preferencesDefaultsTargetFolderButton.Size = new System.Drawing.Size(38, 37);
            this.preferencesDefaultsTargetFolderButton.TabIndex = 2;
            this.preferencesDefaultsTargetFolderButton.Text = "...";
            this.preferencesDefaultsTargetFolderButton.UseVisualStyleBackColor = true;
            this.preferencesDefaultsTargetFolderButton.Click += new System.EventHandler(this.PreferencesDefaultsTargetFolderSelectButton_Click);
            // 
            // preferencesDefaultsTargetFolderPanel
            // 
            this.preferencesDefaultsTargetFolderPanel.AutoSize = true;
            this.preferencesDefaultsTargetFolderPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.preferencesDefaultsTargetFolderPanel.BackColor = System.Drawing.Color.DarkGray;
            this.preferencesDefaultsTargetFolderPanel.Controls.Add(preferencesDefaultsTargetFolderInsetPanel);
            this.preferencesDefaultsTargetFolderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesDefaultsTargetFolderPanel.Location = new System.Drawing.Point(135, 0);
            this.preferencesDefaultsTargetFolderPanel.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesDefaultsTargetFolderPanel.Name = "preferencesDefaultsTargetFolderPanel";
            this.preferencesDefaultsTargetFolderPanel.Padding = new System.Windows.Forms.Padding(2);
            this.preferencesDefaultsTargetFolderPanel.Size = new System.Drawing.Size(455, 37);
            this.preferencesDefaultsTargetFolderPanel.TabIndex = 4;
            // 
            // preferencesDefaultsTargetFolderInsetPanel
            // 
            preferencesDefaultsTargetFolderInsetPanel.AutoSize = true;
            preferencesDefaultsTargetFolderInsetPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            preferencesDefaultsTargetFolderInsetPanel.BackColor = System.Drawing.Color.White;
            preferencesDefaultsTargetFolderInsetPanel.Controls.Add(this.preferencesDefaultsTargetFolderText);
            preferencesDefaultsTargetFolderInsetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            preferencesDefaultsTargetFolderInsetPanel.Location = new System.Drawing.Point(2, 2);
            preferencesDefaultsTargetFolderInsetPanel.Margin = new System.Windows.Forms.Padding(0);
            preferencesDefaultsTargetFolderInsetPanel.Name = "preferencesDefaultsTargetFolderInsetPanel";
            preferencesDefaultsTargetFolderInsetPanel.Padding = new System.Windows.Forms.Padding(6, 5, 4, 5);
            preferencesDefaultsTargetFolderInsetPanel.Size = new System.Drawing.Size(451, 33);
            preferencesDefaultsTargetFolderInsetPanel.TabIndex = 0;
            // 
            // preferencesDefaultsTargetFolderText
            // 
            this.preferencesDefaultsTargetFolderText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.preferencesDefaultsTargetFolderText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesDefaultsTargetFolderText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesDefaultsTargetFolderText.Location = new System.Drawing.Point(6, 5);
            this.preferencesDefaultsTargetFolderText.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesDefaultsTargetFolderText.MinimumSize = new System.Drawing.Size(6, 22);
            this.preferencesDefaultsTargetFolderText.Name = "preferencesDefaultsTargetFolderText";
            this.preferencesDefaultsTargetFolderText.Size = new System.Drawing.Size(441, 23);
            this.preferencesDefaultsTargetFolderText.TabIndex = 1;
            this.preferencesDefaultsTargetFolderText.WordWrap = false;
            this.preferencesDefaultsTargetFolderText.TextChanged += new System.EventHandler(this.PreferencesDefaultsTargetFolderSelection_TextChanged);
            this.preferencesDefaultsTargetFolderText.DoubleClick += new System.EventHandler(this.PreferencesDefaultsTargetFolderSelectButton_Click);
            // 
            // preferencesUpdatesTab
            // 
            this.preferencesUpdatesTab.BackColor = System.Drawing.Color.White;
            this.preferencesUpdatesTab.Controls.Add(preferencesUpdatesTable);
            this.preferencesUpdatesTab.Location = new System.Drawing.Point(4, 39);
            this.preferencesUpdatesTab.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesUpdatesTab.Name = "preferencesUpdatesTab";
            this.preferencesUpdatesTab.Size = new System.Drawing.Size(688, 453);
            this.preferencesUpdatesTab.TabIndex = 3;
            this.preferencesUpdatesTab.Text = "Updates";
            // 
            // preferencesUpdatesTable
            // 
            preferencesUpdatesTable.ColumnCount = 4;
            preferencesUpdatesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            preferencesUpdatesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            preferencesUpdatesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            preferencesUpdatesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            preferencesUpdatesTable.Controls.Add(this.preferencesUpdatesCheckFrequencyLabel, 1, 3);
            preferencesUpdatesTable.Controls.Add(this.preferencesUpdatesChannelCombo, 2, 4);
            preferencesUpdatesTable.Controls.Add(this.preferencesUpdatesDividerPicture, 1, 2);
            preferencesUpdatesTable.Controls.Add(tableLayoutPanel2, 2, 1);
            preferencesUpdatesTable.Controls.Add(this.preferencesUpdatesEnableLabel, 1, 1);
            preferencesUpdatesTable.Controls.Add(this.preferencesUpdatesFrequencyCombo, 2, 3);
            preferencesUpdatesTable.Controls.Add(this.preferencesUpdateChannelLabel, 1, 4);
            preferencesUpdatesTable.Controls.Add(this.preferencesUpdatesRestoreDefaultsButton, 1, 5);
            preferencesUpdatesTable.Controls.Add(this.preferencesUpdatesMessageLabel, 1, 0);
            preferencesUpdatesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            preferencesUpdatesTable.Location = new System.Drawing.Point(0, 0);
            preferencesUpdatesTable.Margin = new System.Windows.Forms.Padding(0);
            preferencesUpdatesTable.Name = "preferencesUpdatesTable";
            preferencesUpdatesTable.RowCount = 6;
            preferencesUpdatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            preferencesUpdatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            preferencesUpdatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            preferencesUpdatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            preferencesUpdatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            preferencesUpdatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            preferencesUpdatesTable.Size = new System.Drawing.Size(688, 453);
            preferencesUpdatesTable.TabIndex = 17;
            // 
            // preferencesUpdatesCheckFrequencyLabel
            // 
            this.preferencesUpdatesCheckFrequencyLabel.AutoSize = true;
            this.preferencesUpdatesCheckFrequencyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesUpdatesCheckFrequencyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesUpdatesCheckFrequencyLabel.Location = new System.Drawing.Point(30, 181);
            this.preferencesUpdatesCheckFrequencyLabel.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.preferencesUpdatesCheckFrequencyLabel.Name = "preferencesUpdatesCheckFrequencyLabel";
            this.preferencesUpdatesCheckFrequencyLabel.Size = new System.Drawing.Size(240, 33);
            this.preferencesUpdatesCheckFrequencyLabel.TabIndex = 9;
            this.preferencesUpdatesCheckFrequencyLabel.Text = "Check frequency:";
            this.preferencesUpdatesCheckFrequencyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // preferencesUpdatesChannelCombo
            // 
            this.preferencesUpdatesChannelCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesUpdatesChannelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.preferencesUpdatesChannelCombo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.preferencesUpdatesChannelCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesUpdatesChannelCombo.FormattingEnabled = true;
            this.preferencesUpdatesChannelCombo.Items.AddRange(new object[] {
            "Release",
            "Beta"});
            this.preferencesUpdatesChannelCombo.Location = new System.Drawing.Point(274, 229);
            this.preferencesUpdatesChannelCombo.Margin = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.preferencesUpdatesChannelCombo.Name = "preferencesUpdatesChannelCombo";
            this.preferencesUpdatesChannelCombo.Size = new System.Drawing.Size(324, 33);
            this.preferencesUpdatesChannelCombo.TabIndex = 4;
            // 
            // preferencesUpdatesDividerPicture
            // 
            this.preferencesUpdatesDividerPicture.BackColor = System.Drawing.Color.DarkGray;
            preferencesUpdatesTable.SetColumnSpan(this.preferencesUpdatesDividerPicture, 2);
            this.preferencesUpdatesDividerPicture.Dock = System.Windows.Forms.DockStyle.Top;
            this.preferencesUpdatesDividerPicture.ErrorImage = null;
            this.preferencesUpdatesDividerPicture.InitialImage = null;
            this.preferencesUpdatesDividerPicture.Location = new System.Drawing.Point(30, 156);
            this.preferencesUpdatesDividerPicture.Margin = new System.Windows.Forms.Padding(0, 0, 0, 23);
            this.preferencesUpdatesDividerPicture.Name = "preferencesUpdatesDividerPicture";
            this.preferencesUpdatesDividerPicture.Size = new System.Drawing.Size(568, 2);
            this.preferencesUpdatesDividerPicture.TabIndex = 14;
            this.preferencesUpdatesDividerPicture.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(this.preferencesUpdatesEnableYesButton, 1, 0);
            tableLayoutPanel2.Controls.Add(this.preferencesUpdatesEnableNoButton, 2, 0);
            tableLayoutPanel2.Location = new System.Drawing.Point(274, 104);
            tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 23, 0, 23);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(141, 29);
            tableLayoutPanel2.TabIndex = 11;
            // 
            // preferencesUpdatesEnableYesButton
            // 
            this.preferencesUpdatesEnableYesButton.AutoSize = true;
            this.preferencesUpdatesEnableYesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesUpdatesEnableYesButton.Location = new System.Drawing.Point(0, 0);
            this.preferencesUpdatesEnableYesButton.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesUpdatesEnableYesButton.Name = "preferencesUpdatesEnableYesButton";
            this.preferencesUpdatesEnableYesButton.Size = new System.Drawing.Size(71, 29);
            this.preferencesUpdatesEnableYesButton.TabIndex = 0;
            this.preferencesUpdatesEnableYesButton.TabStop = true;
            this.preferencesUpdatesEnableYesButton.Text = "Yes";
            this.preferencesUpdatesEnableYesButton.UseVisualStyleBackColor = true;
            // 
            // preferencesUpdatesEnableNoButton
            // 
            this.preferencesUpdatesEnableNoButton.AutoSize = true;
            this.preferencesUpdatesEnableNoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesUpdatesEnableNoButton.Location = new System.Drawing.Point(79, 0);
            this.preferencesUpdatesEnableNoButton.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.preferencesUpdatesEnableNoButton.Name = "preferencesUpdatesEnableNoButton";
            this.preferencesUpdatesEnableNoButton.Size = new System.Drawing.Size(62, 29);
            this.preferencesUpdatesEnableNoButton.TabIndex = 2;
            this.preferencesUpdatesEnableNoButton.TabStop = true;
            this.preferencesUpdatesEnableNoButton.Text = "No";
            this.preferencesUpdatesEnableNoButton.UseVisualStyleBackColor = true;
            // 
            // preferencesUpdatesEnableLabel
            // 
            this.preferencesUpdatesEnableLabel.AutoSize = true;
            this.preferencesUpdatesEnableLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesUpdatesEnableLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesUpdatesEnableLabel.Location = new System.Drawing.Point(30, 81);
            this.preferencesUpdatesEnableLabel.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.preferencesUpdatesEnableLabel.Name = "preferencesUpdatesEnableLabel";
            this.preferencesUpdatesEnableLabel.Size = new System.Drawing.Size(240, 75);
            this.preferencesUpdatesEnableLabel.TabIndex = 9;
            this.preferencesUpdatesEnableLabel.Text = "Enable:";
            this.preferencesUpdatesEnableLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // preferencesUpdatesFrequencyCombo
            // 
            this.preferencesUpdatesFrequencyCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesUpdatesFrequencyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.preferencesUpdatesFrequencyCombo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.preferencesUpdatesFrequencyCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesUpdatesFrequencyCombo.FormattingEnabled = true;
            this.preferencesUpdatesFrequencyCombo.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Monthly"});
            this.preferencesUpdatesFrequencyCombo.Location = new System.Drawing.Point(274, 181);
            this.preferencesUpdatesFrequencyCombo.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesUpdatesFrequencyCombo.Name = "preferencesUpdatesFrequencyCombo";
            this.preferencesUpdatesFrequencyCombo.Size = new System.Drawing.Size(324, 33);
            this.preferencesUpdatesFrequencyCombo.TabIndex = 3;
            // 
            // preferencesUpdateChannelLabel
            // 
            this.preferencesUpdateChannelLabel.AutoSize = true;
            this.preferencesUpdateChannelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesUpdateChannelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesUpdateChannelLabel.Location = new System.Drawing.Point(30, 214);
            this.preferencesUpdateChannelLabel.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.preferencesUpdateChannelLabel.MinimumSize = new System.Drawing.Size(240, 0);
            this.preferencesUpdateChannelLabel.Name = "preferencesUpdateChannelLabel";
            this.preferencesUpdateChannelLabel.Size = new System.Drawing.Size(240, 63);
            this.preferencesUpdateChannelLabel.TabIndex = 9;
            this.preferencesUpdateChannelLabel.Text = "Update channel:";
            this.preferencesUpdateChannelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // preferencesUpdatesRestoreDefaultsButton
            // 
            this.preferencesUpdatesRestoreDefaultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesUpdatesRestoreDefaultsButton.AutoSize = true;
            this.preferencesUpdatesRestoreDefaultsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.preferencesUpdatesRestoreDefaultsButton.BackColor = System.Drawing.SystemColors.ControlLight;
            preferencesUpdatesTable.SetColumnSpan(this.preferencesUpdatesRestoreDefaultsButton, 3);
            this.preferencesUpdatesRestoreDefaultsButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.preferencesUpdatesRestoreDefaultsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferencesUpdatesRestoreDefaultsButton.Location = new System.Drawing.Point(489, 377);
            this.preferencesUpdatesRestoreDefaultsButton.Margin = new System.Windows.Forms.Padding(0, 28, 30, 28);
            this.preferencesUpdatesRestoreDefaultsButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.preferencesUpdatesRestoreDefaultsButton.Name = "preferencesUpdatesRestoreDefaultsButton";
            this.preferencesUpdatesRestoreDefaultsButton.Size = new System.Drawing.Size(169, 48);
            this.preferencesUpdatesRestoreDefaultsButton.TabIndex = 15;
            this.preferencesUpdatesRestoreDefaultsButton.Text = "Restore Defaults";
            this.preferencesUpdatesRestoreDefaultsButton.UseVisualStyleBackColor = false;
            this.preferencesUpdatesRestoreDefaultsButton.Click += new System.EventHandler(this.PreferencesUpdatesRestoreDefaultsButton_Click);
            // 
            // preferencesUpdatesMessageLabel
            // 
            this.preferencesUpdatesMessageLabel.AutoSize = true;
            preferencesUpdatesTable.SetColumnSpan(this.preferencesUpdatesMessageLabel, 3);
            this.preferencesUpdatesMessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesUpdatesMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.preferencesUpdatesMessageLabel.Location = new System.Drawing.Point(30, 31);
            this.preferencesUpdatesMessageLabel.Margin = new System.Windows.Forms.Padding(0, 31, 30, 0);
            this.preferencesUpdatesMessageLabel.Name = "preferencesUpdatesMessageLabel";
            this.preferencesUpdatesMessageLabel.Size = new System.Drawing.Size(628, 50);
            this.preferencesUpdatesMessageLabel.TabIndex = 6;
            this.preferencesUpdatesMessageLabel.Text = "If enabled, when Galileo starts it will check to see if there are any updates ava" +
    "ilable for the selected update channel.\r\n";
            // 
            // updatesTable
            // 
            updatesTable.AutoSize = true;
            updatesTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            updatesTable.BackColor = System.Drawing.Color.Transparent;
            updatesTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            updatesTable.ColumnCount = 1;
            updatesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            updatesTable.Controls.Add(this.updatesMessageLabel, 0, 1);
            updatesTable.Controls.Add(this.updatesTextBackgroundPanel, 0, 2);
            updatesTable.Controls.Add(updatesHeaderTable, 0, 0);
            updatesTable.Controls.Add(this.updatesFooterTable, 0, 3);
            updatesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            updatesTable.Location = new System.Drawing.Point(0, 0);
            updatesTable.Margin = new System.Windows.Forms.Padding(0);
            updatesTable.Name = "updatesTable";
            updatesTable.RowCount = 4;
            updatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            updatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            updatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            updatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            updatesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            updatesTable.Size = new System.Drawing.Size(756, 683);
            updatesTable.TabIndex = 3;
            // 
            // updatesMessageLabel
            // 
            this.updatesMessageLabel.AutoSize = true;
            this.updatesMessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updatesMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatesMessageLabel.Location = new System.Drawing.Point(30, 92);
            this.updatesMessageLabel.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.updatesMessageLabel.Name = "updatesMessageLabel";
            this.updatesMessageLabel.Size = new System.Drawing.Size(696, 50);
            this.updatesMessageLabel.TabIndex = 3;
            this.updatesMessageLabel.Text = "Free updates for everyone, then why isn\'t it always showing. The long update desc" +
    "ription.";
            // 
            // updatesTextBackgroundPanel
            // 
            this.updatesTextBackgroundPanel.BackColor = System.Drawing.Color.DimGray;
            this.updatesTextBackgroundPanel.Controls.Add(this.updatesChangelogRichText);
            this.updatesTextBackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updatesTextBackgroundPanel.Location = new System.Drawing.Point(30, 157);
            this.updatesTextBackgroundPanel.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.updatesTextBackgroundPanel.Name = "updatesTextBackgroundPanel";
            this.updatesTextBackgroundPanel.Padding = new System.Windows.Forms.Padding(2);
            this.updatesTextBackgroundPanel.Size = new System.Drawing.Size(696, 431);
            this.updatesTextBackgroundPanel.TabIndex = 7;
            // 
            // updatesChangelogRichText
            // 
            this.updatesChangelogRichText.BackColor = System.Drawing.Color.White;
            this.updatesChangelogRichText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.updatesChangelogRichText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updatesChangelogRichText.Location = new System.Drawing.Point(2, 2);
            this.updatesChangelogRichText.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.updatesChangelogRichText.Name = "updatesChangelogRichText";
            this.updatesChangelogRichText.ReadOnly = true;
            this.updatesChangelogRichText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.updatesChangelogRichText.Size = new System.Drawing.Size(692, 427);
            this.updatesChangelogRichText.TabIndex = 4;
            this.updatesChangelogRichText.Text = "";
            // 
            // updatesHeaderTable
            // 
            updatesHeaderTable.AutoSize = true;
            updatesHeaderTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            updatesHeaderTable.ColumnCount = 2;
            updatesHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            updatesHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            updatesHeaderTable.Controls.Add(this.updatesDateLabel, 1, 0);
            updatesHeaderTable.Controls.Add(this.updatesTitleLabel, 0, 0);
            updatesHeaderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            updatesHeaderTable.Location = new System.Drawing.Point(0, 0);
            updatesHeaderTable.Margin = new System.Windows.Forms.Padding(0);
            updatesHeaderTable.Name = "updatesHeaderTable";
            updatesHeaderTable.RowCount = 1;
            updatesHeaderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            updatesHeaderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            updatesHeaderTable.Size = new System.Drawing.Size(756, 77);
            updatesHeaderTable.TabIndex = 8;
            // 
            // updatesDateLabel
            // 
            this.updatesDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updatesDateLabel.AutoSize = true;
            this.updatesDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.updatesDateLabel.Location = new System.Drawing.Point(610, 49);
            this.updatesDateLabel.Margin = new System.Windows.Forms.Padding(0, 0, 30, 3);
            this.updatesDateLabel.Name = "updatesDateLabel";
            this.updatesDateLabel.Size = new System.Drawing.Size(116, 25);
            this.updatesDateLabel.TabIndex = 6;
            this.updatesDateLabel.Text = "March 2018";
            this.updatesDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // updatesTitleLabel
            // 
            this.updatesTitleLabel.AutoSize = true;
            this.updatesTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatesTitleLabel.Location = new System.Drawing.Point(30, 45);
            this.updatesTitleLabel.Margin = new System.Windows.Forms.Padding(30, 45, 30, 0);
            this.updatesTitleLabel.Name = "updatesTitleLabel";
            this.updatesTitleLabel.Size = new System.Drawing.Size(215, 32);
            this.updatesTitleLabel.TabIndex = 0;
            this.updatesTitleLabel.Text = "Galileo 2018.1";
            // 
            // updatesFooterTable
            // 
            this.updatesFooterTable.AutoSize = true;
            this.updatesFooterTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.updatesFooterTable.ColumnCount = 2;
            this.updatesFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.updatesFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.updatesFooterTable.Controls.Add(this.updatesDownloadButton, 1, 0);
            this.updatesFooterTable.Controls.Add(this.updatesIgnoreButton, 0, 0);
            this.updatesFooterTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updatesFooterTable.Location = new System.Drawing.Point(0, 588);
            this.updatesFooterTable.Margin = new System.Windows.Forms.Padding(0);
            this.updatesFooterTable.Name = "updatesFooterTable";
            this.updatesFooterTable.RowCount = 1;
            this.updatesFooterTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.updatesFooterTable.Size = new System.Drawing.Size(756, 95);
            this.updatesFooterTable.TabIndex = 9;
            // 
            // updatesDownloadButton
            // 
            this.updatesDownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updatesDownloadButton.AutoSize = true;
            this.updatesDownloadButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.updatesDownloadButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.updatesDownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.updatesDownloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatesDownloadButton.Location = new System.Drawing.Point(613, 19);
            this.updatesDownloadButton.Margin = new System.Windows.Forms.Padding(0, 0, 30, 28);
            this.updatesDownloadButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.updatesDownloadButton.Name = "updatesDownloadButton";
            this.updatesDownloadButton.Size = new System.Drawing.Size(113, 48);
            this.updatesDownloadButton.TabIndex = 0;
            this.updatesDownloadButton.Text = "Download";
            this.updatesDownloadButton.UseVisualStyleBackColor = false;
            this.updatesDownloadButton.Click += new System.EventHandler(this.UpdatesDownloadButton_Click);
            // 
            // updatesIgnoreButton
            // 
            this.updatesIgnoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updatesIgnoreButton.AutoSize = true;
            this.updatesIgnoreButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.updatesIgnoreButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.updatesIgnoreButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.updatesIgnoreButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatesIgnoreButton.Location = new System.Drawing.Point(517, 19);
            this.updatesIgnoreButton.Margin = new System.Windows.Forms.Padding(45, 0, 15, 28);
            this.updatesIgnoreButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.updatesIgnoreButton.Name = "updatesIgnoreButton";
            this.updatesIgnoreButton.Size = new System.Drawing.Size(81, 48);
            this.updatesIgnoreButton.TabIndex = 1;
            this.updatesIgnoreButton.Text = "Ignore";
            this.updatesIgnoreButton.UseVisualStyleBackColor = false;
            this.updatesIgnoreButton.Click += new System.EventHandler(this.UpdatesIgnoreButton_Click);
            // 
            // noUpdateTable
            // 
            noUpdateTable.AutoSize = true;
            noUpdateTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            noUpdateTable.BackColor = System.Drawing.Color.Transparent;
            noUpdateTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            noUpdateTable.ColumnCount = 1;
            noUpdateTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            noUpdateTable.Controls.Add(this.noUpdateTitleLabel, 0, 0);
            noUpdateTable.Controls.Add(noUpdateFooterTable, 0, 3);
            noUpdateTable.Controls.Add(this.noUpdateMessageLabel, 0, 1);
            noUpdateTable.Controls.Add(this.noUpdateNextUpdateCheckLabel, 0, 2);
            noUpdateTable.Dock = System.Windows.Forms.DockStyle.Fill;
            noUpdateTable.Location = new System.Drawing.Point(0, 0);
            noUpdateTable.Margin = new System.Windows.Forms.Padding(0);
            noUpdateTable.Name = "noUpdateTable";
            noUpdateTable.RowCount = 4;
            noUpdateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            noUpdateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            noUpdateTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            noUpdateTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            noUpdateTable.Size = new System.Drawing.Size(756, 683);
            noUpdateTable.TabIndex = 5;
            // 
            // noUpdateTitleLabel
            // 
            this.noUpdateTitleLabel.AutoSize = true;
            this.noUpdateTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noUpdateTitleLabel.Location = new System.Drawing.Point(30, 45);
            this.noUpdateTitleLabel.Margin = new System.Windows.Forms.Padding(30, 45, 30, 0);
            this.noUpdateTitleLabel.Name = "noUpdateTitleLabel";
            this.noUpdateTitleLabel.Size = new System.Drawing.Size(268, 32);
            this.noUpdateTitleLabel.TabIndex = 0;
            this.noUpdateTitleLabel.Text = "No Updates Found";
            // 
            // noUpdateFooterTable
            // 
            noUpdateFooterTable.AutoSize = true;
            noUpdateFooterTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            noUpdateFooterTable.ColumnCount = 2;
            noUpdateFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            noUpdateFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            noUpdateFooterTable.Controls.Add(this.noUpdateIgnoreVersionLabel, 0, 0);
            noUpdateFooterTable.Controls.Add(this.noUpdateCheckForUpdatesButton, 1, 0);
            noUpdateFooterTable.Dock = System.Windows.Forms.DockStyle.Fill;
            noUpdateFooterTable.Location = new System.Drawing.Point(0, 588);
            noUpdateFooterTable.Margin = new System.Windows.Forms.Padding(0);
            noUpdateFooterTable.Name = "noUpdateFooterTable";
            noUpdateFooterTable.RowCount = 1;
            noUpdateFooterTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            noUpdateFooterTable.Size = new System.Drawing.Size(756, 95);
            noUpdateFooterTable.TabIndex = 7;
            // 
            // noUpdateIgnoreVersionLabel
            // 
            this.noUpdateIgnoreVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.noUpdateIgnoreVersionLabel.AutoSize = true;
            this.noUpdateIgnoreVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noUpdateIgnoreVersionLabel.Location = new System.Drawing.Point(30, 30);
            this.noUpdateIgnoreVersionLabel.Margin = new System.Windows.Forms.Padding(30, 0, 0, 40);
            this.noUpdateIgnoreVersionLabel.Name = "noUpdateIgnoreVersionLabel";
            this.noUpdateIgnoreVersionLabel.Size = new System.Drawing.Size(43, 25);
            this.noUpdateIgnoreVersionLabel.TabIndex = 2;
            this.noUpdateIgnoreVersionLabel.Text = "Idle";
            // 
            // noUpdateCheckForUpdatesButton
            // 
            this.noUpdateCheckForUpdatesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.noUpdateCheckForUpdatesButton.AutoSize = true;
            this.noUpdateCheckForUpdatesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.noUpdateCheckForUpdatesButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.noUpdateCheckForUpdatesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.noUpdateCheckForUpdatesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noUpdateCheckForUpdatesButton.Location = new System.Drawing.Point(531, 19);
            this.noUpdateCheckForUpdatesButton.Margin = new System.Windows.Forms.Padding(0, 0, 30, 28);
            this.noUpdateCheckForUpdatesButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.noUpdateCheckForUpdatesButton.Name = "noUpdateCheckForUpdatesButton";
            this.noUpdateCheckForUpdatesButton.Size = new System.Drawing.Size(195, 48);
            this.noUpdateCheckForUpdatesButton.TabIndex = 0;
            this.noUpdateCheckForUpdatesButton.Text = "Check For Updates";
            this.noUpdateCheckForUpdatesButton.UseVisualStyleBackColor = false;
            this.noUpdateCheckForUpdatesButton.Click += new System.EventHandler(this.NoUpdateCheckForUpdateButton_Click);
            // 
            // noUpdateMessageLabel
            // 
            this.noUpdateMessageLabel.AutoSize = true;
            this.noUpdateMessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noUpdateMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noUpdateMessageLabel.Location = new System.Drawing.Point(30, 92);
            this.noUpdateMessageLabel.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.noUpdateMessageLabel.Name = "noUpdateMessageLabel";
            this.noUpdateMessageLabel.Size = new System.Drawing.Size(696, 50);
            this.noUpdateMessageLabel.TabIndex = 6;
            this.noUpdateMessageLabel.Text = "This is GREAT news! Our goal is to update frequently with new features and perfor" +
    "mance gains; so make sure to update when notified.";
            // 
            // noUpdateNextUpdateCheckLabel
            // 
            this.noUpdateNextUpdateCheckLabel.AutoSize = true;
            this.noUpdateNextUpdateCheckLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noUpdateNextUpdateCheckLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.noUpdateNextUpdateCheckLabel.Location = new System.Drawing.Point(30, 172);
            this.noUpdateNextUpdateCheckLabel.Margin = new System.Windows.Forms.Padding(30, 15, 30, 0);
            this.noUpdateNextUpdateCheckLabel.Name = "noUpdateNextUpdateCheckLabel";
            this.noUpdateNextUpdateCheckLabel.Size = new System.Drawing.Size(696, 416);
            this.noUpdateNextUpdateCheckLabel.TabIndex = 8;
            // 
            // processTable
            // 
            processTable.AutoSize = true;
            processTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            processTable.BackColor = System.Drawing.Color.Transparent;
            processTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            processTable.ColumnCount = 1;
            processTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            processTable.Controls.Add(this.processMessageLabel, 0, 1);
            processTable.Controls.Add(this.processTitleLabel, 0, 0);
            processTable.Controls.Add(this.processReminderLabel, 0, 3);
            processTable.Controls.Add(this.processTabs, 0, 4);
            processTable.Controls.Add(processTargetFolderTable, 0, 2);
            processTable.Controls.Add(processFooterTable, 0, 5);
            processTable.Dock = System.Windows.Forms.DockStyle.Fill;
            processTable.Location = new System.Drawing.Point(0, 0);
            processTable.Margin = new System.Windows.Forms.Padding(0);
            processTable.Name = "processTable";
            processTable.RowCount = 6;
            processTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            processTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            processTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            processTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            processTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            processTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            processTable.Size = new System.Drawing.Size(756, 683);
            processTable.TabIndex = 6;
            // 
            // processMessageLabel
            // 
            this.processMessageLabel.AutoSize = true;
            this.processMessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processMessageLabel.Location = new System.Drawing.Point(30, 92);
            this.processMessageLabel.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.processMessageLabel.Name = "processMessageLabel";
            this.processMessageLabel.Size = new System.Drawing.Size(696, 75);
            this.processMessageLabel.TabIndex = 6;
            this.processMessageLabel.Text = "Click the \"...\" button below to open a folder selection dialog. Galileo will sear" +
    "ch the target folder and its subfolders for content. To begin the analysis, clic" +
    "k the Process button.";
            // 
            // processTitleLabel
            // 
            this.processTitleLabel.AutoSize = true;
            this.processTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processTitleLabel.Location = new System.Drawing.Point(30, 45);
            this.processTitleLabel.Margin = new System.Windows.Forms.Padding(30, 45, 30, 0);
            this.processTitleLabel.Name = "processTitleLabel";
            this.processTitleLabel.Size = new System.Drawing.Size(292, 32);
            this.processTitleLabel.TabIndex = 0;
            this.processTitleLabel.Text = "Select Target Folder";
            // 
            // processReminderLabel
            // 
            this.processReminderLabel.AutoSize = true;
            this.processReminderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processReminderLabel.Location = new System.Drawing.Point(30, 234);
            this.processReminderLabel.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.processReminderLabel.Name = "processReminderLabel";
            this.processReminderLabel.Size = new System.Drawing.Size(696, 40);
            this.processReminderLabel.TabIndex = 12;
            this.processReminderLabel.Text = "Please remember that Galileo is a tool meant for flagging possible instances of p" +
    "lagiarism and/or cheating. Nothing can replace the intuition and understanding o" +
    "f a good educator.";
            // 
            // processTabs
            // 
            this.processTabs.Controls.Add(this.processLogTab);
            this.processTabs.Controls.Add(this.processOptionsTab);
            this.processTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processTabs.Location = new System.Drawing.Point(30, 289);
            this.processTabs.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.processTabs.Name = "processTabs";
            this.processTabs.Padding = new System.Drawing.Point(20, 8);
            this.processTabs.SelectedIndex = 0;
            this.processTabs.Size = new System.Drawing.Size(696, 299);
            this.processTabs.TabIndex = 5;
            this.processTabs.TabStop = false;
            // 
            // processLogTab
            // 
            this.processLogTab.BackColor = System.Drawing.Color.White;
            this.processLogTab.Controls.Add(this.processLogRichText);
            this.processLogTab.Location = new System.Drawing.Point(4, 39);
            this.processLogTab.Margin = new System.Windows.Forms.Padding(0);
            this.processLogTab.Name = "processLogTab";
            this.processLogTab.Size = new System.Drawing.Size(688, 256);
            this.processLogTab.TabIndex = 0;
            this.processLogTab.Text = "Log";
            // 
            // processLogRichText
            // 
            this.processLogRichText.BackColor = System.Drawing.Color.White;
            this.processLogRichText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.processLogRichText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processLogRichText.Location = new System.Drawing.Point(0, 0);
            this.processLogRichText.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.processLogRichText.Name = "processLogRichText";
            this.processLogRichText.ReadOnly = true;
            this.processLogRichText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.processLogRichText.Size = new System.Drawing.Size(688, 256);
            this.processLogRichText.TabIndex = 9;
            this.processLogRichText.TabStop = false;
            this.processLogRichText.Text = "";
            // 
            // processOptionsTab
            // 
            this.processOptionsTab.BackColor = System.Drawing.Color.White;
            this.processOptionsTab.Controls.Add(this.processPropertyGrid);
            this.processOptionsTab.Location = new System.Drawing.Point(4, 39);
            this.processOptionsTab.Margin = new System.Windows.Forms.Padding(0);
            this.processOptionsTab.Name = "processOptionsTab";
            this.processOptionsTab.Size = new System.Drawing.Size(688, 256);
            this.processOptionsTab.TabIndex = 1;
            this.processOptionsTab.Text = "Options";
            // 
            // processPropertyGrid
            // 
            this.processPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.processPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.processPropertyGrid.Margin = new System.Windows.Forms.Padding(0);
            this.processPropertyGrid.Name = "processPropertyGrid";
            this.processPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.processPropertyGrid.Size = new System.Drawing.Size(688, 256);
            this.processPropertyGrid.TabIndex = 0;
            this.processPropertyGrid.ToolbarVisible = false;
            // 
            // processTargetFolderTable
            // 
            processTargetFolderTable.AutoSize = true;
            processTargetFolderTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            processTargetFolderTable.ColumnCount = 2;
            processTargetFolderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            processTargetFolderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            processTargetFolderTable.Controls.Add(this.processTargetButton, 1, 0);
            processTargetFolderTable.Controls.Add(this.processTargetFolderPanel, 0, 0);
            processTargetFolderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            processTargetFolderTable.Location = new System.Drawing.Point(30, 182);
            processTargetFolderTable.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            processTargetFolderTable.Name = "processTargetFolderTable";
            processTargetFolderTable.RowCount = 1;
            processTargetFolderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            processTargetFolderTable.Size = new System.Drawing.Size(696, 37);
            processTargetFolderTable.TabIndex = 8;
            // 
            // processTargetButton
            // 
            this.processTargetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processTargetButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.processTargetButton.Location = new System.Drawing.Point(658, 0);
            this.processTargetButton.Margin = new System.Windows.Forms.Padding(0);
            this.processTargetButton.Name = "processTargetButton";
            this.processTargetButton.Size = new System.Drawing.Size(38, 37);
            this.processTargetButton.TabIndex = 1;
            this.processTargetButton.Text = "...";
            this.processTargetButton.UseVisualStyleBackColor = true;
            this.processTargetButton.Click += new System.EventHandler(this.ProcessTargetButton_Click);
            // 
            // processTargetFolderPanel
            // 
            this.processTargetFolderPanel.AutoSize = true;
            this.processTargetFolderPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.processTargetFolderPanel.Controls.Add(processTargetFolderInsetPanel);
            this.processTargetFolderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processTargetFolderPanel.Location = new System.Drawing.Point(0, 0);
            this.processTargetFolderPanel.Margin = new System.Windows.Forms.Padding(0);
            this.processTargetFolderPanel.Name = "processTargetFolderPanel";
            this.processTargetFolderPanel.Padding = new System.Windows.Forms.Padding(2);
            this.processTargetFolderPanel.Size = new System.Drawing.Size(658, 37);
            this.processTargetFolderPanel.TabIndex = 4;
            // 
            // processTargetFolderInsetPanel
            // 
            processTargetFolderInsetPanel.AutoSize = true;
            processTargetFolderInsetPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            processTargetFolderInsetPanel.BackColor = System.Drawing.Color.White;
            processTargetFolderInsetPanel.Controls.Add(this.processTargetText);
            processTargetFolderInsetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            processTargetFolderInsetPanel.Location = new System.Drawing.Point(2, 2);
            processTargetFolderInsetPanel.Margin = new System.Windows.Forms.Padding(0);
            processTargetFolderInsetPanel.Name = "processTargetFolderInsetPanel";
            processTargetFolderInsetPanel.Padding = new System.Windows.Forms.Padding(6, 5, 4, 5);
            processTargetFolderInsetPanel.Size = new System.Drawing.Size(654, 33);
            processTargetFolderInsetPanel.TabIndex = 0;
            // 
            // processTargetText
            // 
            this.processTargetText.BackColor = System.Drawing.Color.White;
            this.processTargetText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.processTargetText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processTargetText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.processTargetText.Location = new System.Drawing.Point(6, 5);
            this.processTargetText.Margin = new System.Windows.Forms.Padding(0);
            this.processTargetText.MinimumSize = new System.Drawing.Size(6, 22);
            this.processTargetText.Name = "processTargetText";
            this.processTargetText.Size = new System.Drawing.Size(644, 23);
            this.processTargetText.TabIndex = 0;
            this.processTargetText.WordWrap = false;
            this.processTargetText.TextChanged += new System.EventHandler(this.ProcessTargetText_TextChanged);
            this.processTargetText.DoubleClick += new System.EventHandler(this.ProcessTargetText_DoubleClick);
            // 
            // processFooterTable
            // 
            processFooterTable.AutoSize = true;
            processFooterTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            processFooterTable.ColumnCount = 3;
            processFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            processFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            processFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            processFooterTable.Controls.Add(this.processReportButton, 2, 0);
            processFooterTable.Controls.Add(this.processProcessButton, 1, 0);
            processFooterTable.Controls.Add(this.progressTotalProgressBar, 0, 0);
            processFooterTable.Dock = System.Windows.Forms.DockStyle.Fill;
            processFooterTable.Location = new System.Drawing.Point(0, 588);
            processFooterTable.Margin = new System.Windows.Forms.Padding(0);
            processFooterTable.Name = "processFooterTable";
            processFooterTable.RowCount = 1;
            processFooterTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            processFooterTable.Size = new System.Drawing.Size(756, 95);
            processFooterTable.TabIndex = 13;
            // 
            // processReportButton
            // 
            this.processReportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.processReportButton.AutoSize = true;
            this.processReportButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.processReportButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.processReportButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.processReportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processReportButton.Location = new System.Drawing.Point(643, 19);
            this.processReportButton.Margin = new System.Windows.Forms.Padding(0, 0, 30, 28);
            this.processReportButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.processReportButton.Name = "processReportButton";
            this.processReportButton.Size = new System.Drawing.Size(83, 48);
            this.processReportButton.TabIndex = 3;
            this.processReportButton.Text = "Report";
            this.processReportButton.UseVisualStyleBackColor = false;
            this.processReportButton.Click += new System.EventHandler(this.ProcessReportButton_Click);
            // 
            // processProcessButton
            // 
            this.processProcessButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.processProcessButton.AutoSize = true;
            this.processProcessButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.processProcessButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.processProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.processProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processProcessButton.Location = new System.Drawing.Point(531, 19);
            this.processProcessButton.Margin = new System.Windows.Forms.Padding(15, 0, 15, 28);
            this.processProcessButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.processProcessButton.Name = "processProcessButton";
            this.processProcessButton.Size = new System.Drawing.Size(97, 48);
            this.processProcessButton.TabIndex = 2;
            this.processProcessButton.Text = "Process";
            this.processProcessButton.UseVisualStyleBackColor = false;
            this.processProcessButton.Click += new System.EventHandler(this.ProcessProcessButton_Click);
            // 
            // progressTotalProgressBar
            // 
            this.progressTotalProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressTotalProgressBar.Location = new System.Drawing.Point(30, 34);
            this.progressTotalProgressBar.Margin = new System.Windows.Forms.Padding(30, 34, 0, 40);
            this.progressTotalProgressBar.Name = "progressTotalProgressBar";
            this.progressTotalProgressBar.Size = new System.Drawing.Size(486, 21);
            this.progressTotalProgressBar.TabIndex = 1;
            // 
            // aboutClientVersionLabel
            // 
            this.aboutClientVersionLabel.AutoSize = true;
            this.aboutClientVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.aboutClientVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.aboutClientVersionLabel.Location = new System.Drawing.Point(52, 173);
            this.aboutClientVersionLabel.Margin = new System.Windows.Forms.Padding(52, 8, 30, 0);
            this.aboutClientVersionLabel.Name = "aboutClientVersionLabel";
            this.aboutClientVersionLabel.Size = new System.Drawing.Size(233, 25);
            this.aboutClientVersionLabel.TabIndex = 13;
            this.aboutClientVersionLabel.Text = "Galileo.Client.Win-0.9.0.0";
            // 
            // aboutClientInfoLabel
            // 
            this.aboutClientInfoLabel.AutoSize = true;
            this.aboutClientInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.aboutClientInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.aboutClientInfoLabel.Location = new System.Drawing.Point(30, 140);
            this.aboutClientInfoLabel.Margin = new System.Windows.Forms.Padding(30, 15, 30, 0);
            this.aboutClientInfoLabel.Name = "aboutClientInfoLabel";
            this.aboutClientInfoLabel.Size = new System.Drawing.Size(181, 25);
            this.aboutClientInfoLabel.TabIndex = 12;
            this.aboutClientInfoLabel.Text = "Client Information";
            // 
            // aboutSystemInfoLabel
            // 
            this.aboutSystemInfoLabel.AutoSize = true;
            this.aboutSystemInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.aboutSystemInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.aboutSystemInfoLabel.Location = new System.Drawing.Point(30, 287);
            this.aboutSystemInfoLabel.Margin = new System.Windows.Forms.Padding(30, 15, 30, 0);
            this.aboutSystemInfoLabel.Name = "aboutSystemInfoLabel";
            this.aboutSystemInfoLabel.Size = new System.Drawing.Size(197, 25);
            this.aboutSystemInfoLabel.TabIndex = 16;
            this.aboutSystemInfoLabel.Text = "System Information";
            // 
            // aboutTitleLabel
            // 
            this.aboutTitleLabel.AutoSize = true;
            this.aboutTitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.aboutTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutTitleLabel.Location = new System.Drawing.Point(30, 45);
            this.aboutTitleLabel.Margin = new System.Windows.Forms.Padding(30, 45, 30, 0);
            this.aboutTitleLabel.Name = "aboutTitleLabel";
            this.aboutTitleLabel.Size = new System.Drawing.Size(459, 32);
            this.aboutTitleLabel.TabIndex = 1;
            this.aboutTitleLabel.Text = "Copyright (c) 2018 dotBunny Inc.";
            // 
            // aboutMessageLabel
            // 
            this.aboutMessageLabel.AutoSize = true;
            this.aboutMessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.aboutMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.aboutMessageLabel.Location = new System.Drawing.Point(33, 85);
            this.aboutMessageLabel.Margin = new System.Windows.Forms.Padding(33, 8, 30, 15);
            this.aboutMessageLabel.Name = "aboutMessageLabel";
            this.aboutMessageLabel.Size = new System.Drawing.Size(186, 25);
            this.aboutMessageLabel.TabIndex = 11;
            this.aboutMessageLabel.Text = "All Rights Reserved.";
            // 
            // aboutThirdPartyLicensesButton
            // 
            this.aboutThirdPartyLicensesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutThirdPartyLicensesButton.AutoSize = true;
            this.aboutThirdPartyLicensesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.aboutThirdPartyLicensesButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.aboutThirdPartyLicensesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.aboutThirdPartyLicensesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutThirdPartyLicensesButton.Location = new System.Drawing.Point(522, 19);
            this.aboutThirdPartyLicensesButton.Margin = new System.Windows.Forms.Padding(0, 0, 30, 28);
            this.aboutThirdPartyLicensesButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.aboutThirdPartyLicensesButton.Name = "aboutThirdPartyLicensesButton";
            this.aboutThirdPartyLicensesButton.Size = new System.Drawing.Size(204, 48);
            this.aboutThirdPartyLicensesButton.TabIndex = 2;
            this.aboutThirdPartyLicensesButton.Text = "Third Party Licenses";
            this.aboutThirdPartyLicensesButton.UseVisualStyleBackColor = false;
            this.aboutThirdPartyLicensesButton.Click += new System.EventHandler(this.AboutThirdPartyLicensesButton_Click);
            // 
            // aboutEULAButton
            // 
            this.aboutEULAButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutEULAButton.AutoSize = true;
            this.aboutEULAButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.aboutEULAButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.aboutEULAButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.aboutEULAButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutEULAButton.Location = new System.Drawing.Point(429, 19);
            this.aboutEULAButton.Margin = new System.Windows.Forms.Padding(0, 0, 15, 28);
            this.aboutEULAButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.aboutEULAButton.Name = "aboutEULAButton";
            this.aboutEULAButton.Size = new System.Drawing.Size(78, 48);
            this.aboutEULAButton.TabIndex = 1;
            this.aboutEULAButton.Text = "EULA";
            this.aboutEULAButton.UseVisualStyleBackColor = false;
            this.aboutEULAButton.Click += new System.EventHandler(this.AboutEULAButton_Click);
            // 
            // aboutLogsButton
            // 
            this.aboutLogsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutLogsButton.AutoSize = true;
            this.aboutLogsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.aboutLogsButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.aboutLogsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.aboutLogsButton.Location = new System.Drawing.Point(345, 19);
            this.aboutLogsButton.Margin = new System.Windows.Forms.Padding(0, 0, 15, 28);
            this.aboutLogsButton.MinimumSize = new System.Drawing.Size(0, 48);
            this.aboutLogsButton.Name = "aboutLogsButton";
            this.aboutLogsButton.Size = new System.Drawing.Size(69, 48);
            this.aboutLogsButton.TabIndex = 0;
            this.aboutLogsButton.Text = "Logs";
            this.aboutLogsButton.UseVisualStyleBackColor = true;
            this.aboutLogsButton.Click += new System.EventHandler(this.AboutLogsButton_Click);
            // 
            // screenTabs
            // 
            this.screenTabs.Controls.Add(this.processTab);
            this.screenTabs.Controls.Add(this.preferencesTab);
            this.screenTabs.Controls.Add(this.updatesTab);
            this.screenTabs.Controls.Add(this.noUpdateTab);
            this.screenTabs.Controls.Add(this.aboutTab);
            this.screenTabs.Controls.Add(this.blankTab);
            this.screenTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenTabs.ItemSize = new System.Drawing.Size(42, 18);
            this.screenTabs.Location = new System.Drawing.Point(412, 0);
            this.screenTabs.Margin = new System.Windows.Forms.Padding(0);
            this.screenTabs.Name = "screenTabs";
            this.screenTabs.Padding = new System.Drawing.Point(0, 0);
            this.screenTabs.SelectedIndex = 0;
            this.screenTabs.Size = new System.Drawing.Size(764, 709);
            this.screenTabs.TabIndex = 0;
            this.screenTabs.TabStop = false;
            // 
            // processTab
            // 
            this.processTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.processTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.processTab.Controls.Add(processTable);
            this.processTab.Location = new System.Drawing.Point(4, 22);
            this.processTab.Margin = new System.Windows.Forms.Padding(0);
            this.processTab.Name = "processTab";
            this.processTab.Size = new System.Drawing.Size(756, 683);
            this.processTab.TabIndex = 2;
            this.processTab.Text = "Process";
            // 
            // preferencesTab
            // 
            this.preferencesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.preferencesTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.preferencesTab.Controls.Add(preferencesTable);
            this.preferencesTab.Location = new System.Drawing.Point(4, 22);
            this.preferencesTab.Margin = new System.Windows.Forms.Padding(0);
            this.preferencesTab.Name = "preferencesTab";
            this.preferencesTab.Size = new System.Drawing.Size(756, 683);
            this.preferencesTab.TabIndex = 3;
            this.preferencesTab.Text = "Preferences";
            // 
            // updatesTab
            // 
            this.updatesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.updatesTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.updatesTab.Controls.Add(updatesTable);
            this.updatesTab.Location = new System.Drawing.Point(4, 22);
            this.updatesTab.Margin = new System.Windows.Forms.Padding(0);
            this.updatesTab.Name = "updatesTab";
            this.updatesTab.Size = new System.Drawing.Size(756, 683);
            this.updatesTab.TabIndex = 4;
            this.updatesTab.Text = "Updates";
            // 
            // noUpdateTab
            // 
            this.noUpdateTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.noUpdateTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.noUpdateTab.Controls.Add(noUpdateTable);
            this.noUpdateTab.Location = new System.Drawing.Point(4, 22);
            this.noUpdateTab.Margin = new System.Windows.Forms.Padding(0);
            this.noUpdateTab.Name = "noUpdateTab";
            this.noUpdateTab.Size = new System.Drawing.Size(756, 683);
            this.noUpdateTab.TabIndex = 5;
            this.noUpdateTab.Text = "NoUpdate";
            // 
            // aboutTab
            // 
            this.aboutTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.aboutTab.Controls.Add(this.aboutTable);
            this.aboutTab.Location = new System.Drawing.Point(4, 22);
            this.aboutTab.Margin = new System.Windows.Forms.Padding(0);
            this.aboutTab.Name = "aboutTab";
            this.aboutTab.Size = new System.Drawing.Size(756, 683);
            this.aboutTab.TabIndex = 6;
            this.aboutTab.Text = "About";
            // 
            // aboutTable
            // 
            this.aboutTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.aboutTable.ColumnCount = 1;
            this.aboutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.aboutTable.Controls.Add(this.aboutCoreLibraryVersionLabel, 0, 5);
            this.aboutTable.Controls.Add(this.aboutClientLibraryVersionLabel, 0, 4);
            this.aboutTable.Controls.Add(this.aboutClientVersionLabel, 0, 3);
            this.aboutTable.Controls.Add(this.aboutTitleLabel, 0, 0);
            this.aboutTable.Controls.Add(this.aboutMessageLabel, 0, 1);
            this.aboutTable.Controls.Add(this.aboutClientInfoLabel, 0, 2);
            this.aboutTable.Controls.Add(this.aboutSystemInfoLabel, 0, 6);
            this.aboutTable.Controls.Add(this.aboutSystemInfoPanel, 0, 7);
            this.aboutTable.Controls.Add(this.aboutFooterTable, 0, 8);
            this.aboutTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutTable.Location = new System.Drawing.Point(0, 0);
            this.aboutTable.Margin = new System.Windows.Forms.Padding(0);
            this.aboutTable.Name = "aboutTable";
            this.aboutTable.RowCount = 9;
            this.aboutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.aboutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.aboutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.aboutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.aboutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.aboutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.aboutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.aboutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.aboutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.aboutTable.Size = new System.Drawing.Size(756, 683);
            this.aboutTable.TabIndex = 0;
            this.aboutTable.Paint += new System.Windows.Forms.PaintEventHandler(this.AboutTable_Paint);
            // 
            // aboutCoreLibraryVersionLabel
            // 
            this.aboutCoreLibraryVersionLabel.AutoSize = true;
            this.aboutCoreLibraryVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.aboutCoreLibraryVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.aboutCoreLibraryVersionLabel.Location = new System.Drawing.Point(52, 239);
            this.aboutCoreLibraryVersionLabel.Margin = new System.Windows.Forms.Padding(52, 8, 30, 8);
            this.aboutCoreLibraryVersionLabel.Name = "aboutCoreLibraryVersionLabel";
            this.aboutCoreLibraryVersionLabel.Size = new System.Drawing.Size(186, 25);
            this.aboutCoreLibraryVersionLabel.TabIndex = 15;
            this.aboutCoreLibraryVersionLabel.Text = "Galileo.Core-0.9.0.0";
            // 
            // aboutClientLibraryVersionLabel
            // 
            this.aboutClientLibraryVersionLabel.AutoSize = true;
            this.aboutClientLibraryVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.aboutClientLibraryVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.aboutClientLibraryVersionLabel.Location = new System.Drawing.Point(52, 206);
            this.aboutClientLibraryVersionLabel.Margin = new System.Windows.Forms.Padding(52, 8, 30, 0);
            this.aboutClientLibraryVersionLabel.Name = "aboutClientLibraryVersionLabel";
            this.aboutClientLibraryVersionLabel.Size = new System.Drawing.Size(193, 25);
            this.aboutClientLibraryVersionLabel.TabIndex = 14;
            this.aboutClientLibraryVersionLabel.Text = "Galileo.Client-0.9.0.0";
            // 
            // aboutSystemInfoPanel
            // 
            this.aboutSystemInfoPanel.BackColor = System.Drawing.Color.DimGray;
            this.aboutSystemInfoPanel.Controls.Add(this.aboutSystemInformationText);
            this.aboutSystemInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutSystemInfoPanel.Location = new System.Drawing.Point(30, 320);
            this.aboutSystemInfoPanel.Margin = new System.Windows.Forms.Padding(30, 8, 30, 0);
            this.aboutSystemInfoPanel.Name = "aboutSystemInfoPanel";
            this.aboutSystemInfoPanel.Padding = new System.Windows.Forms.Padding(2);
            this.aboutSystemInfoPanel.Size = new System.Drawing.Size(696, 268);
            this.aboutSystemInfoPanel.TabIndex = 19;
            // 
            // aboutSystemInformationText
            // 
            this.aboutSystemInformationText.BackColor = System.Drawing.Color.White;
            this.aboutSystemInformationText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.aboutSystemInformationText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutSystemInformationText.Location = new System.Drawing.Point(2, 2);
            this.aboutSystemInformationText.Margin = new System.Windows.Forms.Padding(0);
            this.aboutSystemInformationText.Name = "aboutSystemInformationText";
            this.aboutSystemInformationText.ReadOnly = true;
            this.aboutSystemInformationText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.aboutSystemInformationText.Size = new System.Drawing.Size(692, 264);
            this.aboutSystemInformationText.TabIndex = 17;
            this.aboutSystemInformationText.TabStop = false;
            this.aboutSystemInformationText.Text = "";
            // 
            // aboutFooterTable
            // 
            this.aboutFooterTable.AutoSize = true;
            this.aboutFooterTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.aboutFooterTable.ColumnCount = 3;
            this.aboutFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.aboutFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.aboutFooterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.aboutFooterTable.Controls.Add(this.aboutThirdPartyLicensesButton, 2, 0);
            this.aboutFooterTable.Controls.Add(this.aboutEULAButton, 1, 0);
            this.aboutFooterTable.Controls.Add(this.aboutLogsButton, 0, 0);
            this.aboutFooterTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutFooterTable.Location = new System.Drawing.Point(0, 588);
            this.aboutFooterTable.Margin = new System.Windows.Forms.Padding(0);
            this.aboutFooterTable.Name = "aboutFooterTable";
            this.aboutFooterTable.RowCount = 1;
            this.aboutFooterTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.aboutFooterTable.Size = new System.Drawing.Size(756, 95);
            this.aboutFooterTable.TabIndex = 20;
            // 
            // blankTab
            // 
            this.blankTab.Location = new System.Drawing.Point(4, 22);
            this.blankTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.blankTab.Name = "blankTab";
            this.blankTab.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.blankTab.Size = new System.Drawing.Size(756, 683);
            this.blankTab.TabIndex = 7;
            this.blankTab.Text = "Blank";
            this.blankTab.UseVisualStyleBackColor = true;
            // 
            // folderDialog
            // 
            this.folderDialog.Description = "Select Target Folder";
            this.folderDialog.ShowNewFolderButton = false;
            // 
            // saveDialog
            // 
            this.saveDialog.DefaultExt = "json";
            this.saveDialog.FileName = "Galielo_ProcessOptions.json";
            this.saveDialog.Filter = "Process Options|*.json|All Files|*.*";
            this.saveDialog.Title = "Export Process Config";
            // 
            // openDialog
            // 
            this.openDialog.Filter = "Process Options|*.json|All Files|*.*";
            this.openDialog.Title = "Import Process Config";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Galileo";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.NotifyIcon_BalloonTipClicked);
            // 
            // MainForm
            // 
            this.AccessibleName = "Galileo";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1176, 709);
            this.Controls.Add(this.screenTabs);
            this.Controls.Add(panelMenu);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(40)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1189, 739);
            this.Name = "MainForm";
            this.Text = "Galileo";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            this.menuFlowPanel.ResumeLayout(false);
            this.panelMenuHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.menuBranding)).EndInit();
            this.menuProcessButtonPanel.ResumeLayout(false);
            this.menuProcessButtonPanel.PerformLayout();
            panelSystemDivider.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureSystemDividerLine)).EndInit();
            this.menuPreferencesButtonPanel.ResumeLayout(false);
            this.menuPreferencesButtonPanel.PerformLayout();
            this.menuUpdatesButtonPanel.ResumeLayout(false);
            this.menuUpdatesButtonPanel.PerformLayout();
            preferencesTable.ResumeLayout(false);
            preferencesTable.PerformLayout();
            this.preferencesTabs.ResumeLayout(false);
            this.preferencesGeneralTab.ResumeLayout(false);
            this.tableLayoutPreferencesGeneral.ResumeLayout(false);
            this.tableLayoutPreferencesGeneral.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.preferencesGeneralDividerPicture)).EndInit();
            this.preferencesProcessTab.ResumeLayout(false);
            this.preferencesProcessTab.PerformLayout();
            preferencesDefaultsTable.ResumeLayout(false);
            preferencesDefaultsTable.PerformLayout();
            this.preferencesDefaultsFooterTable.ResumeLayout(false);
            this.preferencesDefaultsFooterTable.PerformLayout();
            this.preferencesDefaultsGridPanel.ResumeLayout(false);
            this.preferencesDefaultsTargetFolderTable.ResumeLayout(false);
            this.preferencesDefaultsTargetFolderTable.PerformLayout();
            this.preferencesDefaultsTargetFolderPanel.ResumeLayout(false);
            this.preferencesDefaultsTargetFolderPanel.PerformLayout();
            preferencesDefaultsTargetFolderInsetPanel.ResumeLayout(false);
            preferencesDefaultsTargetFolderInsetPanel.PerformLayout();
            this.preferencesUpdatesTab.ResumeLayout(false);
            preferencesUpdatesTable.ResumeLayout(false);
            preferencesUpdatesTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preferencesUpdatesDividerPicture)).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            updatesTable.ResumeLayout(false);
            updatesTable.PerformLayout();
            this.updatesTextBackgroundPanel.ResumeLayout(false);
            updatesHeaderTable.ResumeLayout(false);
            updatesHeaderTable.PerformLayout();
            this.updatesFooterTable.ResumeLayout(false);
            this.updatesFooterTable.PerformLayout();
            noUpdateTable.ResumeLayout(false);
            noUpdateTable.PerformLayout();
            noUpdateFooterTable.ResumeLayout(false);
            noUpdateFooterTable.PerformLayout();
            processTable.ResumeLayout(false);
            processTable.PerformLayout();
            this.processTabs.ResumeLayout(false);
            this.processLogTab.ResumeLayout(false);
            this.processOptionsTab.ResumeLayout(false);
            processTargetFolderTable.ResumeLayout(false);
            processTargetFolderTable.PerformLayout();
            this.processTargetFolderPanel.ResumeLayout(false);
            this.processTargetFolderPanel.PerformLayout();
            processTargetFolderInsetPanel.ResumeLayout(false);
            processTargetFolderInsetPanel.PerformLayout();
            processFooterTable.ResumeLayout(false);
            processFooterTable.PerformLayout();
            this.screenTabs.ResumeLayout(false);
            this.processTab.ResumeLayout(false);
            this.processTab.PerformLayout();
            this.preferencesTab.ResumeLayout(false);
            this.preferencesTab.PerformLayout();
            this.updatesTab.ResumeLayout(false);
            this.updatesTab.PerformLayout();
            this.noUpdateTab.ResumeLayout(false);
            this.noUpdateTab.PerformLayout();
            this.aboutTab.ResumeLayout(false);
            this.aboutTable.ResumeLayout(false);
            this.aboutTable.PerformLayout();
            this.aboutSystemInfoPanel.ResumeLayout(false);
            this.aboutFooterTable.ResumeLayout(false);
            this.aboutFooterTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox menuBranding;
        private System.Windows.Forms.TabPage processTab;
        private System.Windows.Forms.TabPage preferencesTab;
        private System.Windows.Forms.TabPage updatesTab;
        private System.Windows.Forms.TabPage noUpdateTab;
        private System.Windows.Forms.Panel menuProcessButtonPanel;
        private System.Windows.Forms.Label menuProcessIconLabel;
        private System.Windows.Forms.Label menuProcessButtonLabel;
        private System.Windows.Forms.Panel menuPreferencesButtonPanel;
        private System.Windows.Forms.Label menuPreferencesButtonLabel;
        private System.Windows.Forms.Label menuPreferencesIconLabel;
        private System.Windows.Forms.Panel menuUpdatesButtonPanel;
        private System.Windows.Forms.Label menuUpdatesButtonLabel;
        private System.Windows.Forms.Label menuUpdatesIconLabel;
        private System.Windows.Forms.Label menuSystemSectionLabel;
        private System.Windows.Forms.PictureBox pictureSystemDividerLine;
        private System.Windows.Forms.FlowLayoutPanel menuFlowPanel;
        private System.Windows.Forms.Panel panelMenuHeader;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.Button updatesIgnoreButton;
        private System.Windows.Forms.Button updatesDownloadButton;
        private System.Windows.Forms.RichTextBox updatesChangelogRichText;
        private System.Windows.Forms.Label updatesMessageLabel;
        private System.Windows.Forms.Label updatesTitleLabel;
        private System.Windows.Forms.Button noUpdateCheckForUpdatesButton;
        private System.Windows.Forms.Button preferencesDefaultsRestoreDefaultsButton;
        private System.Windows.Forms.TabControl preferencesTabs;
        private System.Windows.Forms.TabPage preferencesGeneralTab;
        private System.Windows.Forms.TabPage preferencesProcessTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPreferencesGeneral;
        private System.Windows.Forms.TextBox preferencesDefaultsTargetFolderText;
        private System.Windows.Forms.Button processTargetButton;
        private System.Windows.Forms.TextBox processTargetText;
        private System.Windows.Forms.RichTextBox processLogRichText;
        private System.Windows.Forms.Button processProcessButton;
        private System.Windows.Forms.Button processReportButton;
        private System.Windows.Forms.ProgressBar progressTotalProgressBar;
        private System.Windows.Forms.TabControl screenTabs;
        private System.Windows.Forms.Label updatesDateLabel;
        private System.Windows.Forms.Label menuVersionLabel;
        private System.Windows.Forms.TabControl processTabs;
        private System.Windows.Forms.TabPage processLogTab;
        private System.Windows.Forms.TabPage processOptionsTab;
        private System.Windows.Forms.CheckBox preferencesGeneralReportAutomaticOpenCheck;
        private System.Windows.Forms.CheckBox preferencesGeneralSendUsageDataCheck;
        private System.Windows.Forms.Label preferencesGeneralDataExplanationLabel;
        private System.Windows.Forms.Button preferencesDefaultsImportButton;
        private System.Windows.Forms.Button preferencesDefaultsExportButton;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.TabPage aboutTab;
        private System.Windows.Forms.TableLayoutPanel aboutTable;
        private System.Windows.Forms.Label aboutCoreLibraryVersionLabel;
        private System.Windows.Forms.Label aboutClientLibraryVersionLabel;
        private System.Windows.Forms.RichTextBox aboutSystemInformationText;
        private System.Windows.Forms.Button aboutThirdPartyLicensesButton;
        private System.Windows.Forms.Button aboutEULAButton;
        private System.Windows.Forms.Button aboutLogsButton;
        private System.Windows.Forms.TabPage preferencesUpdatesTab;
        private System.Windows.Forms.Panel updatesTextBackgroundPanel;
        private System.Windows.Forms.Panel aboutSystemInfoPanel;
        private System.Windows.Forms.TableLayoutPanel aboutFooterTable;
        private System.Windows.Forms.Label noUpdateIgnoreVersionLabel;
        private System.Windows.Forms.TableLayoutPanel updatesFooterTable;
        private System.Windows.Forms.Label aboutClientInfoLabel;
        private System.Windows.Forms.Label aboutTitleLabel;
        private System.Windows.Forms.Label aboutMessageLabel;
        private System.Windows.Forms.Label aboutSystemInfoLabel;
        private System.Windows.Forms.Label noUpdateTitleLabel;
        private System.Windows.Forms.Label noUpdateMessageLabel;
        private System.Windows.Forms.Label processMessageLabel;
        private System.Windows.Forms.Label processReminderLabel;
        private System.Windows.Forms.Label aboutClientVersionLabel;
        private System.Windows.Forms.Panel processTargetFolderPanel;
        private System.Windows.Forms.Label preferencesTitleLabel;
        private System.Windows.Forms.TableLayoutPanel preferencesDefaultsFooterTable;
        private System.Windows.Forms.Panel preferencesDefaultsGridPanel;
        private System.Windows.Forms.Label preferencesDefaultsMessageLabel;
        private System.Windows.Forms.TableLayoutPanel preferencesDefaultsTargetFolderTable;
        private System.Windows.Forms.Label preferencesDefaultsTargetFolderLabel;
        private System.Windows.Forms.Button preferencesDefaultsTargetFolderButton;
        private System.Windows.Forms.Panel preferencesDefaultsTargetFolderPanel;
        private System.Windows.Forms.Button preferencesGeneralRestoreDefaultsButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button preferencesUpdatesRestoreDefaultsButton;
        private System.Windows.Forms.TabPage blankTab;
        private System.Windows.Forms.Label processTitleLabel;
        private System.Windows.Forms.Label noUpdateNextUpdateCheckLabel;
        private System.Windows.Forms.Label preferencesGeneralLocaleLabel;
        private System.Windows.Forms.ComboBox preferencesGeneralLocaleCombo;
        private System.Windows.Forms.Label preferencesGeneralMessageLabel;
        private System.Windows.Forms.PictureBox preferencesGeneralDividerPicture;
        private System.Windows.Forms.Label preferencesUpdatesCheckFrequencyLabel;
        private System.Windows.Forms.Label preferencesUpdatesMessageLabel;
        private System.Windows.Forms.ComboBox preferencesUpdatesChannelCombo;
        private System.Windows.Forms.PictureBox preferencesUpdatesDividerPicture;
        private System.Windows.Forms.RadioButton preferencesUpdatesEnableYesButton;
        private System.Windows.Forms.RadioButton preferencesUpdatesEnableNoButton;
        private System.Windows.Forms.Label preferencesUpdatesEnableLabel;
        private System.Windows.Forms.ComboBox preferencesUpdatesFrequencyCombo;
        private System.Windows.Forms.Label preferencesUpdateChannelLabel;
        private System.Windows.Forms.PropertyGrid processPropertyGrid;
        private System.Windows.Forms.PropertyGrid preferencesDefaultsPropertyGrid;
    }
}