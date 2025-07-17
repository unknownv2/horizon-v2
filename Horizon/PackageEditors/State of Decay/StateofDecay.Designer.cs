namespace Horizon.PackageEditors.State_of_Decay
{
    partial class StateofDecay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateofDecay));
            this.rbPackageEditor = new DevComponents.DotNetBar.RibbonControl();
            this.cmdOpen = new DevComponents.DotNetBar.Office2007StartButton();
            this.cmdSave = new DevComponents.DotNetBar.Office2007StartButton();
            this.panelMain = new DevComponents.DotNetBar.RibbonPanel();
            this.tabMain = new DevComponents.DotNetBar.RibbonTabItem();
            this.tabInventory = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.treeInventory = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector3 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.stcInventory = new DevComponents.DotNetBar.SuperTabControl();
            this.dtTimeStamp = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.treePlayerStats = new DevComponents.AdvTree.AdvTree();
            this.columnHeader7 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader8 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.tabSupplyLocker = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel2 = new DevComponents.DotNetBar.RibbonPanel();
            this.treeSupplyLocker = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.stcSupplyLocker = new DevComponents.DotNetBar.SuperTabControl();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.fFame = new DevComponents.Editors.DoubleInput();
            this.fInfluence = new DevComponents.Editors.DoubleInput();
            this.btnExtractSave = new DevComponents.DotNetBar.ButtonItem();
            this.btnInjectSave = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel3 = new DevComponents.DotNetBar.RibbonPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cmbGameType = new DevComponents.DotNetBar.ComboBoxItem();
            this.btnInjectBreakdownSave = new DevComponents.DotNetBar.ButtonItem();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stcInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimeStamp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treePlayerStats)).BeginInit();
            this.ribbonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeSupplyLocker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stcSupplyLocker)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fFame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fInfluence)).BeginInit();
            this.ribbonPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            this.rbPackageEditor.AutoExpand = false;
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.CanCustomize = false;
            this.rbPackageEditor.CaptionVisible = true;
            this.rbPackageEditor.Controls.Add(this.panelMain);
            this.rbPackageEditor.Controls.Add(this.ribbonPanel3);
            this.rbPackageEditor.Controls.Add(this.ribbonPanel1);
            this.rbPackageEditor.Controls.Add(this.ribbonPanel2);
            this.rbPackageEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbPackageEditor.EnableQatPlacement = false;
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdSave,
            this.tabMain,
            this.tabInventory,
            this.tabSupplyLocker,
            this.ribbonTabItem1,
            this.btnExtractSave,
            this.btnInjectSave,
            this.btnInjectBreakdownSave,
            this.cmbGameType});
            this.rbPackageEditor.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.rbPackageEditor.Location = new System.Drawing.Point(5, 1);
            this.rbPackageEditor.Margin = new System.Windows.Forms.Padding(0);
            this.rbPackageEditor.Name = "rbPackageEditor";
            this.rbPackageEditor.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.rbPackageEditor.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdOpen});
            this.rbPackageEditor.Size = new System.Drawing.Size(762, 394);
            this.rbPackageEditor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rbPackageEditor.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.rbPackageEditor.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.rbPackageEditor.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.rbPackageEditor.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.rbPackageEditor.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.rbPackageEditor.SystemText.QatDialogAddButton = "&Add >>";
            this.rbPackageEditor.SystemText.QatDialogCancelButton = "Cancel";
            this.rbPackageEditor.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.rbPackageEditor.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.rbPackageEditor.SystemText.QatDialogOkButton = "OK";
            this.rbPackageEditor.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.rbPackageEditor.SystemText.QatDialogRemoveButton = "&Remove";
            this.rbPackageEditor.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.rbPackageEditor.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.rbPackageEditor.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.rbPackageEditor.TabGroupHeight = 14;
            this.rbPackageEditor.TabIndex = 1;
            // 
            // cmdOpen
            // 
            this.cmdOpen.CanCustomize = false;
            this.cmdOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.cmdOpen.FixedSize = new System.Drawing.Size(60, 23);
            this.cmdOpen.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.cmdOpen.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.cmdOpen.ImagePaddingHorizontal = 0;
            this.cmdOpen.ImagePaddingVertical = 0;
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.ShowSubItems = false;
            this.cmdOpen.Text = "Open";
            // 
            // cmdSave
            // 
            this.cmdSave.CanCustomize = false;
            this.cmdSave.FixedSize = new System.Drawing.Size(60, 23);
            this.cmdSave.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.cmdSave.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.cmdSave.ImagePaddingHorizontal = 0;
            this.cmdSave.ImagePaddingVertical = 0;
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.ShowSubItems = false;
            this.cmdSave.Text = "Save";
            // 
            // panelMain
            // 
            this.panelMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelMain.Controls.Add(this.treePlayerStats);
            this.panelMain.Controls.Add(this.panelEx2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panelMain.Size = new System.Drawing.Size(762, 339);
            // 
            // 
            // 
            this.panelMain.Style.Class = "";
            this.panelMain.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelMain.StyleMouseDown.Class = "";
            this.panelMain.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelMain.StyleMouseOver.Class = "";
            this.panelMain.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.panelMain.TabIndex = 1;
            this.panelMain.Visible = true;
            // 
            // tabMain
            // 
            this.tabMain.Checked = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.Panel = this.panelMain;
            this.tabMain.Text = "Main";
            this.tabMain.Click += new System.EventHandler(this.BtnTab_Click);
            // 
            // tabInventory
            // 
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Panel = this.ribbonPanel1;
            this.tabInventory.Text = "Inventory";
            this.tabInventory.Click += new System.EventHandler(this.BtnTab_Click);
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.treeInventory);
            this.ribbonPanel1.Controls.Add(this.stcInventory);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(762, 339);
            // 
            // 
            // 
            this.ribbonPanel1.Style.Class = "";
            this.ribbonPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel1.StyleMouseDown.Class = "";
            this.ribbonPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel1.StyleMouseOver.Class = "";
            this.ribbonPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel1.TabIndex = 2;
            this.ribbonPanel1.Visible = false;
            // 
            // treeInventory
            // 
            this.treeInventory.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeInventory.AllowDrop = true;
            this.treeInventory.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.treeInventory.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeInventory.BackgroundStyle.Class = "TreeBorderKey";
            this.treeInventory.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeInventory.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeInventory.Location = new System.Drawing.Point(6, 5);
            this.treeInventory.Name = "treeInventory";
            this.treeInventory.NodesConnector = this.nodeConnector3;
            this.treeInventory.NodeStyle = this.elementStyle3;
            this.treeInventory.PathSeparator = ";";
            this.treeInventory.Size = new System.Drawing.Size(283, 328);
            this.treeInventory.Styles.Add(this.elementStyle3);
            this.treeInventory.TabIndex = 0;
            this.treeInventory.Text = "advTree1";
            this.treeInventory.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.TreeInventory_AfterNodeSelect);
            this.treeInventory.AfterNodeDeselect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.TreeInventory_AfterNodeDeselect);
            // 
            // nodeConnector3
            // 
            this.nodeConnector3.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle3
            // 
            this.elementStyle3.Class = "";
            this.elementStyle3.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle3.Name = "elementStyle3";
            this.elementStyle3.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // stcInventory
            // 
            this.stcInventory.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stcInventory.CloseButtonOnTabsVisible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.stcInventory.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.stcInventory.ControlBox.MenuBox.Name = "";
            this.stcInventory.ControlBox.Name = "";
            this.stcInventory.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.stcInventory.ControlBox.MenuBox,
            this.stcInventory.ControlBox.CloseBox});
            this.stcInventory.Location = new System.Drawing.Point(295, 5);
            this.stcInventory.Name = "stcInventory";
            this.stcInventory.ReorderTabsEnabled = true;
            this.stcInventory.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.stcInventory.SelectedTabIndex = -1;
            this.stcInventory.Size = new System.Drawing.Size(461, 328);
            this.stcInventory.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stcInventory.TabIndex = 1;
            this.stcInventory.Text = "superTabControl1";
            this.stcInventory.TabItemClose += new System.EventHandler<DevComponents.DotNetBar.SuperTabStripTabItemCloseEventArgs>(this.SuperTabControl_TabItemClose);
            // 
            // dtTimeStamp
            // 
            // 
            // 
            // 
            this.dtTimeStamp.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtTimeStamp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTimeStamp.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtTimeStamp.ButtonDropDown.Visible = true;
            this.dtTimeStamp.CustomFormat = "\"at\' HH \'hours and\' mm \'minutes\"";
            this.dtTimeStamp.Location = new System.Drawing.Point(13, 163);
            // 
            // 
            // 
            this.dtTimeStamp.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtTimeStamp.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtTimeStamp.MonthCalendar.BackgroundStyle.Class = "";
            this.dtTimeStamp.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTimeStamp.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtTimeStamp.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtTimeStamp.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtTimeStamp.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtTimeStamp.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtTimeStamp.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtTimeStamp.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtTimeStamp.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtTimeStamp.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTimeStamp.MonthCalendar.DisplayMonth = new System.DateTime(2013, 6, 1, 0, 0, 0, 0);
            this.dtTimeStamp.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtTimeStamp.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtTimeStamp.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtTimeStamp.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtTimeStamp.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtTimeStamp.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtTimeStamp.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTimeStamp.MonthCalendar.TodayButtonVisible = true;
            this.dtTimeStamp.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtTimeStamp.Name = "dtTimeStamp";
            this.dtTimeStamp.Size = new System.Drawing.Size(159, 20);
            this.dtTimeStamp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtTimeStamp.TabIndex = 115;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 137);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 20);
            this.labelX1.TabIndex = 116;
            this.labelX1.Text = "Timestamp:";
            // 
            // treePlayerStats
            // 
            this.treePlayerStats.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treePlayerStats.AllowDrop = true;
            this.treePlayerStats.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treePlayerStats.BackgroundStyle.Class = "TreeBorderKey";
            this.treePlayerStats.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treePlayerStats.Columns.Add(this.columnHeader7);
            this.treePlayerStats.Columns.Add(this.columnHeader8);
            this.treePlayerStats.DoubleClickTogglesNode = false;
            this.treePlayerStats.DragDropEnabled = false;
            this.treePlayerStats.DragDropNodeCopyEnabled = false;
            this.treePlayerStats.ExpandButtonSize = new System.Drawing.Size(1, 15);
            this.treePlayerStats.ExpandWidth = 5;
            this.treePlayerStats.HotTracking = true;
            this.treePlayerStats.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treePlayerStats.Location = new System.Drawing.Point(6, 5);
            this.treePlayerStats.MultiNodeDragCountVisible = false;
            this.treePlayerStats.MultiNodeDragDropAllowed = false;
            this.treePlayerStats.Name = "treePlayerStats";
            this.treePlayerStats.NodeHorizontalSpacing = 5;
            this.treePlayerStats.NodesConnector = this.nodeConnector2;
            this.treePlayerStats.NodeSpacing = 5;
            this.treePlayerStats.NodeStyle = this.elementStyle2;
            this.treePlayerStats.PathSeparator = ";";
            this.treePlayerStats.Size = new System.Drawing.Size(405, 328);
            this.treePlayerStats.Styles.Add(this.elementStyle2);
            this.treePlayerStats.TabIndex = 133;
            // 
            // columnHeader7
            // 
            this.columnHeader7.MinimumWidth = 100;
            this.columnHeader7.Name = "columnHeader7";
            this.columnHeader7.Text = "Stat";
            this.columnHeader7.Width.Absolute = 220;
            // 
            // columnHeader8
            // 
            this.columnHeader8.EditorType = DevComponents.AdvTree.eCellEditorType.NumericDouble;
            this.columnHeader8.Name = "columnHeader8";
            this.columnHeader8.Text = "Value";
            this.columnHeader8.Width.Absolute = 140;
            // 
            // nodeConnector2
            // 
            this.nodeConnector2.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle2
            // 
            this.elementStyle2.Class = "";
            this.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle2.Name = "elementStyle2";
            this.elementStyle2.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // tabSupplyLocker
            // 
            this.tabSupplyLocker.Name = "tabSupplyLocker";
            this.tabSupplyLocker.Panel = this.ribbonPanel2;
            this.tabSupplyLocker.Text = "Supply Locker";
            this.tabSupplyLocker.Click += new System.EventHandler(this.BtnTab_Click);
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel2.Controls.Add(this.treeSupplyLocker);
            this.ribbonPanel2.Controls.Add(this.stcSupplyLocker);
            this.ribbonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel2.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel2.Size = new System.Drawing.Size(762, 339);
            // 
            // 
            // 
            this.ribbonPanel2.Style.Class = "";
            this.ribbonPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel2.StyleMouseDown.Class = "";
            this.ribbonPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel2.StyleMouseOver.Class = "";
            this.ribbonPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel2.TabIndex = 3;
            this.ribbonPanel2.Visible = false;
            // 
            // treeSupplyLocker
            // 
            this.treeSupplyLocker.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeSupplyLocker.AllowDrop = true;
            this.treeSupplyLocker.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.treeSupplyLocker.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeSupplyLocker.BackgroundStyle.Class = "TreeBorderKey";
            this.treeSupplyLocker.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeSupplyLocker.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeSupplyLocker.Location = new System.Drawing.Point(6, 5);
            this.treeSupplyLocker.Name = "treeSupplyLocker";
            this.treeSupplyLocker.NodesConnector = this.nodeConnector1;
            this.treeSupplyLocker.NodeStyle = this.elementStyle1;
            this.treeSupplyLocker.PathSeparator = ";";
            this.treeSupplyLocker.Size = new System.Drawing.Size(283, 328);
            this.treeSupplyLocker.Styles.Add(this.elementStyle1);
            this.treeSupplyLocker.TabIndex = 2;
            this.treeSupplyLocker.Text = "advTree1";
            this.treeSupplyLocker.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.TreeInventory_AfterNodeSelect);
            this.treeSupplyLocker.AfterNodeDeselect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.TreeInventory_AfterNodeDeselect);
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // stcSupplyLocker
            // 
            this.stcSupplyLocker.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stcSupplyLocker.CloseButtonOnTabsVisible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.stcSupplyLocker.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.stcSupplyLocker.ControlBox.MenuBox.Name = "";
            this.stcSupplyLocker.ControlBox.Name = "";
            this.stcSupplyLocker.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.stcSupplyLocker.ControlBox.MenuBox,
            this.stcSupplyLocker.ControlBox.CloseBox});
            this.stcSupplyLocker.Location = new System.Drawing.Point(295, 5);
            this.stcSupplyLocker.Name = "stcSupplyLocker";
            this.stcSupplyLocker.ReorderTabsEnabled = true;
            this.stcSupplyLocker.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.stcSupplyLocker.SelectedTabIndex = -1;
            this.stcSupplyLocker.Size = new System.Drawing.Size(461, 328);
            this.stcSupplyLocker.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stcSupplyLocker.TabIndex = 3;
            this.stcSupplyLocker.Text = "superTabControl1";
            this.stcSupplyLocker.TabItemClose += new System.EventHandler<DevComponents.DotNetBar.SuperTabStripTabItemCloseEventArgs>(this.SuperTabControl_TabItemClose);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Controls.Add(this.fFame);
            this.panelEx2.Controls.Add(this.fInfluence);
            this.panelEx2.Controls.Add(this.dtTimeStamp);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Location = new System.Drawing.Point(417, 5);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(184, 328);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 135;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(13, 8);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(142, 20);
            this.labelX3.TabIndex = 119;
            this.labelX3.Text = "Influence:";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(13, 70);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(142, 20);
            this.labelX4.TabIndex = 118;
            this.labelX4.Text = "Fame:";
            // 
            // fFame
            // 
            // 
            // 
            // 
            this.fFame.BackgroundStyle.Class = "DateTimeInputBackground";
            this.fFame.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fFame.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.fFame.Increment = 1;
            this.fFame.Location = new System.Drawing.Point(13, 96);
            this.fFame.MaxValue = 3.402823E+38;
            this.fFame.Name = "fFame";
            this.fFame.ShowUpDown = true;
            this.fFame.Size = new System.Drawing.Size(159, 20);
            this.fFame.TabIndex = 121;
            // 
            // fInfluence
            // 
            // 
            // 
            // 
            this.fInfluence.BackgroundStyle.Class = "DateTimeInputBackground";
            this.fInfluence.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fInfluence.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.fInfluence.Increment = 1;
            this.fInfluence.Location = new System.Drawing.Point(13, 34);
            this.fInfluence.MaxValue = 3.402823E+38;
            this.fInfluence.Name = "fInfluence";
            this.fInfluence.ShowUpDown = true;
            this.fInfluence.Size = new System.Drawing.Size(159, 20);
            this.fInfluence.TabIndex = 120;
            // 
            // btnExtractSave
            // 
            this.btnExtractSave.Name = "btnExtractSave";
            this.btnExtractSave.Text = "Extract";
            this.btnExtractSave.Visible = false;
            this.btnExtractSave.Click += new System.EventHandler(this.BtnExtractClick);
            // 
            // btnInjectSave
            // 
            this.btnInjectSave.Name = "btnInjectSave";
            this.btnInjectSave.Text = "Inject";
            this.btnInjectSave.Visible = false;
            this.btnInjectSave.Click += new System.EventHandler(this.BtnInjectClick);
            // 
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Panel = this.ribbonPanel3;
            this.ribbonTabItem1.Text = "Notes";
            this.ribbonTabItem1.Click += new System.EventHandler(this.BtnTab_Click);
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel3.Controls.Add(this.richTextBox1);
            this.ribbonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel3.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel3.Size = new System.Drawing.Size(762, 339);
            // 
            // 
            // 
            this.ribbonPanel3.Style.Class = "";
            this.ribbonPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel3.StyleMouseDown.Class = "";
            this.ribbonPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel3.StyleMouseOver.Class = "";
            this.ribbonPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel3.TabIndex = 4;
            this.ribbonPanel3.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox1.Size = new System.Drawing.Size(756, 336);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // cmbGameType
            // 
            this.cmbGameType.DropDownHeight = 106;
            this.cmbGameType.Name = "cmbGameType";
            this.cmbGameType.SelectedIndexChanged += new System.EventHandler(this.CmbGameTypeIndexChanged);
            // 
            // btnInjectBreakdownSave
            // 
            this.btnInjectBreakdownSave.Name = "btnInjectBreakdownSave";
            this.btnInjectBreakdownSave.Text = "Inject Breakdown";
            this.btnInjectBreakdownSave.Visible = false;
            this.btnInjectBreakdownSave.Click += new System.EventHandler(this.BtnInjectBreakdownSaveClick);
            // 
            // StateofDecay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(772, 397);
            this.Name = "StateofDecay";
            this.Text = "State of Decay";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ribbonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stcInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimeStamp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treePlayerStats)).EndInit();
            this.ribbonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeSupplyLocker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stcSupplyLocker)).EndInit();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fFame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fInfluence)).EndInit();
            this.ribbonPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonTabItem tabInventory;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtTimeStamp;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.AdvTree.AdvTree treePlayerStats;
        private DevComponents.AdvTree.ColumnHeader columnHeader7;
        private DevComponents.AdvTree.ColumnHeader columnHeader8;
        private DevComponents.AdvTree.NodeConnector nodeConnector2;
        private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private DevComponents.AdvTree.AdvTree treeInventory;
        private DevComponents.AdvTree.NodeConnector nodeConnector3;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private DevComponents.DotNetBar.SuperTabControl stcInventory;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel2;
        private DevComponents.AdvTree.AdvTree treeSupplyLocker;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.SuperTabControl stcSupplyLocker;
        private DevComponents.DotNetBar.RibbonTabItem tabSupplyLocker;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonItem btnExtractSave;
        private DevComponents.DotNetBar.ButtonItem btnInjectSave;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.Editors.DoubleInput fFame;
        private DevComponents.Editors.DoubleInput fInfluence;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem1;
        private DevComponents.DotNetBar.ComboBoxItem cmbGameType;
        private DevComponents.DotNetBar.ButtonItem btnInjectBreakdownSave;
        private DevComponents.DotNetBar.RibbonControl rbPackageEditor;
        private DevComponents.DotNetBar.RibbonPanel panelMain;
        private DevComponents.DotNetBar.Office2007StartButton cmdSave;
        private DevComponents.DotNetBar.RibbonTabItem tabMain;
        private DevComponents.DotNetBar.Office2007StartButton cmdOpen;

    }
}
