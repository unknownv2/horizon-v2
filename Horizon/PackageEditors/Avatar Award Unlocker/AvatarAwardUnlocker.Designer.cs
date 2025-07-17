namespace Horizon.PackageEditors.Avatar_Award_Unlocker
{
    partial class AvatarAwardUnlocker
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
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabAward = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.panelAward = new DevComponents.DotNetBar.PanelEx();
            this.cmdUnlockAward = new DevComponents.DotNetBar.ButtonX();
            this.dateUnlocked = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.ckUnlockedOffline = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckUnlockedOnline = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblUnlockedDescription = new DevComponents.DotNetBar.LabelX();
            this.lblLockedDescription = new DevComponents.DotNetBar.LabelX();
            this.pbAward = new System.Windows.Forms.PictureBox();
            this.pbGame = new System.Windows.Forms.PictureBox();
            this.cmdUnlockAll = new DevComponents.DotNetBar.ButtonX();
            this.cmdUnlockAllAwards = new DevComponents.DotNetBar.ButtonX();
            this.pTotal = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.listGames = new System.Windows.Forms.ListView();
            this.col1 = new System.Windows.Forms.ColumnHeader();
            this.col2 = new System.Windows.Forms.ColumnHeader();
            this.col3 = new System.Windows.Forms.ColumnHeader();
            this.listAwards = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.colAward = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuExtract = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractAwardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pTotalTitle = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.pbMarketplace = new System.Windows.Forms.PictureBox();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            this.panelAward.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateUnlocked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAwards)).BeginInit();
            this.menuExtract.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMarketplace)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Controls.Add(this.ribbonPanel1);
            this.rbPackageEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabAward});
            this.rbPackageEditor.Size = new System.Drawing.Size(696, 125);
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
            this.rbPackageEditor.Controls.SetChildIndex(this.ribbonPanel1, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.panelMain, 0);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.pTotalTitle);
            this.panelMain.Controls.Add(this.pTotal);
            this.panelMain.Controls.Add(this.cmdUnlockAllAwards);
            this.panelMain.Controls.Add(this.pbGame);
            this.panelMain.Controls.Add(this.cmdUnlockAll);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(696, 70);
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
            this.panelMain.Visible = true;
            // 
            // tabMain
            // 
            this.tabMain.Text = "Game";
            // 
            // tabAward
            // 
            this.tabAward.Name = "tabAward";
            this.tabAward.Panel = this.ribbonPanel1;
            this.tabAward.Text = "Award";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.panelAward);
            this.ribbonPanel1.Controls.Add(this.lblUnlockedDescription);
            this.ribbonPanel1.Controls.Add(this.lblLockedDescription);
            this.ribbonPanel1.Controls.Add(this.pbAward);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(696, 70);
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
            // panelAward
            // 
            this.panelAward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAward.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelAward.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelAward.Controls.Add(this.cmdUnlockAward);
            this.panelAward.Controls.Add(this.dateUnlocked);
            this.panelAward.Controls.Add(this.ckUnlockedOffline);
            this.panelAward.Controls.Add(this.ckUnlockedOnline);
            this.panelAward.Location = new System.Drawing.Point(448, 1);
            this.panelAward.Name = "panelAward";
            this.panelAward.Size = new System.Drawing.Size(251, 68);
            this.panelAward.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelAward.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelAward.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelAward.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelAward.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelAward.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelAward.Style.GradientAngle = 90;
            this.panelAward.TabIndex = 17;
            // 
            // cmdUnlockAward
            // 
            this.cmdUnlockAward.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdUnlockAward.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdUnlockAward.FocusCuesEnabled = false;
            this.cmdUnlockAward.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUnlockAward.Location = new System.Drawing.Point(115, 6);
            this.cmdUnlockAward.Name = "cmdUnlockAward";
            this.cmdUnlockAward.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdUnlockAward.Size = new System.Drawing.Size(129, 33);
            this.cmdUnlockAward.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdUnlockAward.TabIndex = 5;
            this.cmdUnlockAward.Text = "Unlock Award";
            this.cmdUnlockAward.Click += new System.EventHandler(this.cmdUnlockAward_Click);
            // 
            // dateUnlocked
            // 
            // 
            // 
            // 
            this.dateUnlocked.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateUnlocked.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateUnlocked.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dateUnlocked.ButtonDropDown.Visible = true;
            this.dateUnlocked.CustomFormat = "MM/dd/yyyy h:m:s tt";
            this.dateUnlocked.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dateUnlocked.Location = new System.Drawing.Point(5, 45);
            this.dateUnlocked.MaxDate = new System.DateTime(2015, 12, 31, 0, 0, 0, 0);
            this.dateUnlocked.MinDate = new System.DateTime(2005, 9, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.dateUnlocked.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateUnlocked.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dateUnlocked.MonthCalendar.BackgroundStyle.Class = "";
            this.dateUnlocked.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateUnlocked.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateUnlocked.MonthCalendar.DisplayMonth = new System.DateTime(2010, 7, 1, 0, 0, 0, 0);
            this.dateUnlocked.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateUnlocked.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateUnlocked.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateUnlocked.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateUnlocked.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateUnlocked.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dateUnlocked.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateUnlocked.MonthCalendar.TodayButtonVisible = true;
            this.dateUnlocked.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dateUnlocked.Name = "dateUnlocked";
            this.dateUnlocked.Size = new System.Drawing.Size(239, 20);
            this.dateUnlocked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dateUnlocked.TabIndex = 3;
            // 
            // ckUnlockedOffline
            // 
            // 
            // 
            // 
            this.ckUnlockedOffline.BackgroundStyle.Class = "";
            this.ckUnlockedOffline.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckUnlockedOffline.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckUnlockedOffline.FocusCuesEnabled = false;
            this.ckUnlockedOffline.Location = new System.Drawing.Point(3, 2);
            this.ckUnlockedOffline.Name = "ckUnlockedOffline";
            this.ckUnlockedOffline.Size = new System.Drawing.Size(106, 20);
            this.ckUnlockedOffline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckUnlockedOffline.TabIndex = 0;
            this.ckUnlockedOffline.Text = "Unlocked Offline";
            this.ckUnlockedOffline.CheckedChanged += new System.EventHandler(this.ckUnlocked_CheckedChanged);
            // 
            // ckUnlockedOnline
            // 
            // 
            // 
            // 
            this.ckUnlockedOnline.BackgroundStyle.Class = "";
            this.ckUnlockedOnline.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckUnlockedOnline.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckUnlockedOnline.FocusCuesEnabled = false;
            this.ckUnlockedOnline.Location = new System.Drawing.Point(3, 23);
            this.ckUnlockedOnline.Name = "ckUnlockedOnline";
            this.ckUnlockedOnline.Size = new System.Drawing.Size(106, 20);
            this.ckUnlockedOnline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckUnlockedOnline.TabIndex = 1;
            this.ckUnlockedOnline.Text = "Unlocked Online";
            this.ckUnlockedOnline.CheckedChanged += new System.EventHandler(this.ckUnlocked_CheckedChanged);
            // 
            // lblUnlockedDescription
            // 
            this.lblUnlockedDescription.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblUnlockedDescription.BackgroundStyle.Class = "";
            this.lblUnlockedDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblUnlockedDescription.Location = new System.Drawing.Point(76, 33);
            this.lblUnlockedDescription.Name = "lblUnlockedDescription";
            this.lblUnlockedDescription.Size = new System.Drawing.Size(366, 30);
            this.lblUnlockedDescription.TabIndex = 16;
            this.lblUnlockedDescription.UseMnemonic = false;
            // 
            // lblLockedDescription
            // 
            this.lblLockedDescription.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblLockedDescription.BackgroundStyle.Class = "";
            this.lblLockedDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblLockedDescription.Location = new System.Drawing.Point(76, 2);
            this.lblLockedDescription.Name = "lblLockedDescription";
            this.lblLockedDescription.Size = new System.Drawing.Size(366, 30);
            this.lblLockedDescription.TabIndex = 9;
            this.lblLockedDescription.UseMnemonic = false;
            // 
            // pbAward
            // 
            this.pbAward.BackColor = System.Drawing.Color.Transparent;
            this.pbAward.Image = global::Horizon.Properties.Resources.Console;
            this.pbAward.Location = new System.Drawing.Point(6, 2);
            this.pbAward.Name = "pbAward";
            this.pbAward.Size = new System.Drawing.Size(64, 64);
            this.pbAward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAward.TabIndex = 15;
            this.pbAward.TabStop = false;
            // 
            // pbGame
            // 
            this.pbGame.BackColor = System.Drawing.Color.Transparent;
            this.pbGame.Image = global::Horizon.Properties.Resources.Console;
            this.pbGame.Location = new System.Drawing.Point(6, 2);
            this.pbGame.Name = "pbGame";
            this.pbGame.Size = new System.Drawing.Size(64, 64);
            this.pbGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGame.TabIndex = 14;
            this.pbGame.TabStop = false;
            // 
            // cmdUnlockAll
            // 
            this.cmdUnlockAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdUnlockAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdUnlockAll.FocusCuesEnabled = false;
            this.cmdUnlockAll.Location = new System.Drawing.Point(76, 2);
            this.cmdUnlockAll.Name = "cmdUnlockAll";
            this.cmdUnlockAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdUnlockAll.Size = new System.Drawing.Size(149, 31);
            this.cmdUnlockAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdUnlockAll.TabIndex = 19;
            this.cmdUnlockAll.Text = "Unlock All Displayed";
            this.cmdUnlockAll.Click += new System.EventHandler(this.cmdUnlockAll_Click);
            // 
            // cmdUnlockAllAwards
            // 
            this.cmdUnlockAllAwards.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdUnlockAllAwards.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdUnlockAllAwards.FocusCuesEnabled = false;
            this.cmdUnlockAllAwards.Location = new System.Drawing.Point(76, 35);
            this.cmdUnlockAllAwards.Name = "cmdUnlockAllAwards";
            this.cmdUnlockAllAwards.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdUnlockAllAwards.Size = new System.Drawing.Size(149, 31);
            this.cmdUnlockAllAwards.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdUnlockAllAwards.TabIndex = 20;
            this.cmdUnlockAllAwards.Text = "Unlock All Awards";
            this.cmdUnlockAllAwards.Click += new System.EventHandler(this.cmdUnlockAllAwards_Click);
            // 
            // pTotal
            // 
            this.pTotal.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pTotal.BackgroundStyle.Class = "";
            this.pTotal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pTotal.Location = new System.Drawing.Point(228, 35);
            this.pTotal.Name = "pTotal";
            this.pTotal.Size = new System.Drawing.Size(464, 31);
            this.pTotal.TabIndex = 21;
            this.pTotal.Text = "Total Awards";
            this.pTotal.TextVisible = true;
            // 
            // listGames
            // 
            this.listGames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1,
            this.col2,
            this.col3});
            this.listGames.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listGames.FullRowSelect = true;
            this.listGames.Location = new System.Drawing.Point(5, 126);
            this.listGames.MultiSelect = false;
            this.listGames.Name = "listGames";
            this.listGames.Size = new System.Drawing.Size(225, 229);
            this.listGames.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listGames.TabIndex = 6;
            this.listGames.UseCompatibleStateImageBehavior = false;
            this.listGames.View = System.Windows.Forms.View.Tile;
            this.listGames.SelectedIndexChanged += new System.EventHandler(this.listGames_SelectedIndexChanged);
            // 
            // listAwards
            // 
            this.listAwards.AllowUserToAddRows = false;
            this.listAwards.AllowUserToDeleteRows = false;
            this.listAwards.AllowUserToResizeRows = false;
            this.listAwards.BackgroundColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.listAwards.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.listAwards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listAwards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIcon,
            this.colAward,
            this.colDescription});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.listAwards.DefaultCellStyle = dataGridViewCellStyle4;
            this.listAwards.Dock = System.Windows.Forms.DockStyle.Right;
            this.listAwards.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.listAwards.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.listAwards.HighlightSelectedColumnHeaders = false;
            this.listAwards.Location = new System.Drawing.Point(229, 126);
            this.listAwards.Name = "listAwards";
            this.listAwards.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.listAwards.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.listAwards.RowHeadersVisible = false;
            this.listAwards.SelectAllSignVisible = false;
            this.listAwards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listAwards.ShowCellErrors = false;
            this.listAwards.ShowCellToolTips = false;
            this.listAwards.ShowEditingIcon = false;
            this.listAwards.ShowRowErrors = false;
            this.listAwards.Size = new System.Drawing.Size(472, 357);
            this.listAwards.TabIndex = 8;
            this.listAwards.SelectionChanged += new System.EventHandler(this.listAwards_SelectionChanged);
            // 
            // colIcon
            // 
            this.colIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colIcon.HeaderText = "Icon";
            this.colIcon.MinimumWidth = 66;
            this.colIcon.Name = "colIcon";
            this.colIcon.ReadOnly = true;
            this.colIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colIcon.Width = 66;
            // 
            // colAward
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F);
            this.colAward.DefaultCellStyle = dataGridViewCellStyle2;
            this.colAward.HeaderText = "Avatar Award";
            this.colAward.Name = "colAward";
            this.colAward.ReadOnly = true;
            this.colAward.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAward.Width = 175;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F);
            this.colDescription.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // menuExtract
            // 
            this.menuExtract.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractAwardsToolStripMenuItem});
            this.menuExtract.Name = "menuExtract";
            this.menuExtract.Size = new System.Drawing.Size(161, 26);
            // 
            // extractAwardsToolStripMenuItem
            // 
            this.extractAwardsToolStripMenuItem.Image = global::Horizon.Properties.Resources.SaveIcon;
            this.extractAwardsToolStripMenuItem.Name = "extractAwardsToolStripMenuItem";
            this.extractAwardsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.extractAwardsToolStripMenuItem.Text = "Extract Awards...";
            this.extractAwardsToolStripMenuItem.Click += new System.EventHandler(this.extractAwardsToolStripMenuItem_Click);
            // 
            // pTotalTitle
            // 
            this.pTotalTitle.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pTotalTitle.BackgroundStyle.Class = "";
            this.pTotalTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pTotalTitle.Location = new System.Drawing.Point(228, 2);
            this.pTotalTitle.Name = "pTotalTitle";
            this.pTotalTitle.Size = new System.Drawing.Size(464, 31);
            this.pTotalTitle.TabIndex = 22;
            this.pTotalTitle.Text = "Awards";
            this.pTotalTitle.TextVisible = true;
            // 
            // pbMarketplace
            // 
            this.pbMarketplace.BackColor = System.Drawing.Color.Gainsboro;
            this.pbMarketplace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbMarketplace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMarketplace.Location = new System.Drawing.Point(5, 354);
            this.pbMarketplace.Name = "pbMarketplace";
            this.pbMarketplace.Size = new System.Drawing.Size(225, 129);
            this.pbMarketplace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbMarketplace.TabIndex = 9;
            this.pbMarketplace.TabStop = false;
            // 
            // AvatarAwardUnlocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 485);
            this.Controls.Add(this.pbMarketplace);
            this.Controls.Add(this.listGames);
            this.Controls.Add(this.listAwards);
            this.Name = "AvatarAwardUnlocker";
            this.Text = "Avatar Award Unlocker";
            this.Controls.SetChildIndex(this.rbPackageEditor, 0);
            this.Controls.SetChildIndex(this.listAwards, 0);
            this.Controls.SetChildIndex(this.listGames, 0);
            this.Controls.SetChildIndex(this.pbMarketplace, 0);
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ribbonPanel1.ResumeLayout(false);
            this.panelAward.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateUnlocked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAwards)).EndInit();
            this.menuExtract.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMarketplace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonTabItem tabAward;
        private System.Windows.Forms.PictureBox pbGame;
        private DevComponents.DotNetBar.ButtonX cmdUnlockAll;
        private DevComponents.DotNetBar.ButtonX cmdUnlockAllAwards;
        private DevComponents.DotNetBar.Controls.ProgressBarX pTotal;
        private System.Windows.Forms.PictureBox pbAward;
        private DevComponents.DotNetBar.LabelX lblLockedDescription;
        private DevComponents.DotNetBar.LabelX lblUnlockedDescription;
        private DevComponents.DotNetBar.PanelEx panelAward;
        private DevComponents.DotNetBar.ButtonX cmdUnlockAward;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateUnlocked;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckUnlockedOffline;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckUnlockedOnline;
        private System.Windows.Forms.ListView listGames;
        private System.Windows.Forms.ColumnHeader col1;
        private System.Windows.Forms.ColumnHeader col2;
        private System.Windows.Forms.ColumnHeader col3;
        private DevComponents.DotNetBar.Controls.DataGridViewX listAwards;
        private System.Windows.Forms.DataGridViewImageColumn colIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAward;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.ContextMenuStrip menuExtract;
        private System.Windows.Forms.ToolStripMenuItem extractAwardsToolStripMenuItem;
        private DevComponents.DotNetBar.Controls.ProgressBarX pTotalTitle;
        private System.Windows.Forms.PictureBox pbMarketplace;
    }
}