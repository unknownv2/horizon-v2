namespace Horizon.PackageEditors.Modern_Warfare_3
{
    partial class MW3CampaignSave
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
            this.tabAdvanced = new DevComponents.DotNetBar.RibbonTabItem();
            this.panelAdvanced = new DevComponents.DotNetBar.RibbonPanel();
            this.cmdSearch = new DevComponents.DotNetBar.ButtonX();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelAdvancedEntry = new DevComponents.DotNetBar.PanelEx();
            this.txtDvar = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cmdRemoveEntry = new DevComponents.DotNetBar.ButtonX();
            this.cmdAddEntry = new DevComponents.DotNetBar.ButtonX();
            this.txtValue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.listDvars = new System.Windows.Forms.ListView();
            this.columnDvar = new System.Windows.Forms.ColumnHeader();
            this.columnValue = new System.Windows.Forms.ColumnHeader();
            this.tabSegments = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.panelSegmentOperations = new DevComponents.DotNetBar.PanelEx();
            this.cmdExtractSegments = new DevComponents.DotNetBar.ButtonX();
            this.cmdReplaceSegment = new DevComponents.DotNetBar.ButtonX();
            this.cmdExtractSegment = new DevComponents.DotNetBar.ButtonX();
            this.listSegments = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.cmdMaxHealth = new Horizon.Controls.DvarButton();
            this.cmdJumpSlowdown = new Horizon.Controls.DvarButton();
            this.cmdGiveAll = new Horizon.Controls.DvarButton();
            this.cmdDisableAI = new Horizon.Controls.DvarButton();
            this.cmdDebugUI = new Horizon.Controls.DvarButton();
            this.dvarSlider2 = new Horizon.Controls.DvarSlider();
            this.dvarSlider5 = new Horizon.Controls.DvarSlider();
            this.dvarSlider1 = new Horizon.Controls.DvarSlider();
            this.cmdUnlimitedSprint = new Horizon.Controls.DvarButton();
            this.dvarSlider3 = new Horizon.Controls.DvarSlider();
            this.cmdHideHud = new Horizon.Controls.DvarButton();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelAdvanced.SuspendLayout();
            this.panelAdvancedEntry.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            this.panelSegmentOperations.SuspendLayout();
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
            this.rbPackageEditor.Controls.Add(this.panelAdvanced);
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabAdvanced,
            this.tabSegments});
            this.rbPackageEditor.Size = new System.Drawing.Size(538, 311);
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
            this.rbPackageEditor.SelectedRibbonTabChanged += new System.EventHandler(this.rbPackageEditor_SelectedRibbonTabChanged);
            this.rbPackageEditor.Controls.SetChildIndex(this.panelAdvanced, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.ribbonPanel1, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.panelMain, 0);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.cmdHideHud);
            this.panelMain.Controls.Add(this.dvarSlider3);
            this.panelMain.Controls.Add(this.cmdUnlimitedSprint);
            this.panelMain.Controls.Add(this.dvarSlider1);
            this.panelMain.Controls.Add(this.dvarSlider5);
            this.panelMain.Controls.Add(this.dvarSlider2);
            this.panelMain.Controls.Add(this.cmdDebugUI);
            this.panelMain.Controls.Add(this.cmdDisableAI);
            this.panelMain.Controls.Add(this.cmdGiveAll);
            this.panelMain.Controls.Add(this.cmdJumpSlowdown);
            this.panelMain.Controls.Add(this.cmdMaxHealth);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(538, 256);
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
            this.tabMain.Text = "Player";
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Panel = this.panelAdvanced;
            this.tabAdvanced.Text = "Settings";
            // 
            // panelAdvanced
            // 
            this.panelAdvanced.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelAdvanced.Controls.Add(this.cmdSearch);
            this.panelAdvanced.Controls.Add(this.txtSearch);
            this.panelAdvanced.Controls.Add(this.panelAdvancedEntry);
            this.panelAdvanced.Controls.Add(this.listDvars);
            this.panelAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAdvanced.Location = new System.Drawing.Point(0, 53);
            this.panelAdvanced.Name = "panelAdvanced";
            this.panelAdvanced.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panelAdvanced.Size = new System.Drawing.Size(538, 256);
            // 
            // 
            // 
            this.panelAdvanced.Style.Class = "";
            this.panelAdvanced.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelAdvanced.StyleMouseDown.Class = "";
            this.panelAdvanced.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelAdvanced.StyleMouseOver.Class = "";
            this.panelAdvanced.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.panelAdvanced.TabIndex = 2;
            this.panelAdvanced.Visible = false;
            // 
            // cmdSearch
            // 
            this.cmdSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSearch.FocusCuesEnabled = false;
            this.cmdSearch.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.cmdSearch.Location = new System.Drawing.Point(6, 226);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSearch.Size = new System.Drawing.Size(151, 24);
            this.cmdSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSearch.TabIndex = 9;
            this.cmdSearch.Text = "Next";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtSearch
            // 
            // 
            // 
            // 
            this.txtSearch.Border.Class = "TextBoxBorder";
            this.txtSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSearch.Location = new System.Drawing.Point(7, 200);
            this.txtSearch.MaxLength = 1024;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(150, 20);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.WatermarkText = "Search...";
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // panelAdvancedEntry
            // 
            this.panelAdvancedEntry.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelAdvancedEntry.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelAdvancedEntry.Controls.Add(this.txtDvar);
            this.panelAdvancedEntry.Controls.Add(this.cmdRemoveEntry);
            this.panelAdvancedEntry.Controls.Add(this.cmdAddEntry);
            this.panelAdvancedEntry.Controls.Add(this.txtValue);
            this.panelAdvancedEntry.Location = new System.Drawing.Point(3, 2);
            this.panelAdvancedEntry.Name = "panelAdvancedEntry";
            this.panelAdvancedEntry.Size = new System.Drawing.Size(157, 112);
            this.panelAdvancedEntry.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelAdvancedEntry.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelAdvancedEntry.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelAdvancedEntry.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelAdvancedEntry.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelAdvancedEntry.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelAdvancedEntry.Style.GradientAngle = 90;
            this.panelAdvancedEntry.TabIndex = 7;
            // 
            // txtDvar
            // 
            // 
            // 
            // 
            this.txtDvar.Border.Class = "TextBoxBorder";
            this.txtDvar.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDvar.Location = new System.Drawing.Point(3, 3);
            this.txtDvar.MaxLength = 1024;
            this.txtDvar.Name = "txtDvar";
            this.txtDvar.Size = new System.Drawing.Size(151, 20);
            this.txtDvar.TabIndex = 1;
            this.txtDvar.WatermarkText = "DVAR";
            this.txtDvar.TextChanged += new System.EventHandler(this.txtDvar_TextChanged);
            // 
            // cmdRemoveEntry
            // 
            this.cmdRemoveEntry.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdRemoveEntry.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdRemoveEntry.FocusCuesEnabled = false;
            this.cmdRemoveEntry.Location = new System.Drawing.Point(3, 85);
            this.cmdRemoveEntry.Name = "cmdRemoveEntry";
            this.cmdRemoveEntry.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdRemoveEntry.Size = new System.Drawing.Size(151, 24);
            this.cmdRemoveEntry.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdRemoveEntry.TabIndex = 4;
            this.cmdRemoveEntry.Text = "Remove Selected";
            this.cmdRemoveEntry.Click += new System.EventHandler(this.cmdRemoveEntry_Click);
            // 
            // cmdAddEntry
            // 
            this.cmdAddEntry.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdAddEntry.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdAddEntry.FocusCuesEnabled = false;
            this.cmdAddEntry.Location = new System.Drawing.Point(3, 55);
            this.cmdAddEntry.Name = "cmdAddEntry";
            this.cmdAddEntry.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdAddEntry.Size = new System.Drawing.Size(151, 24);
            this.cmdAddEntry.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdAddEntry.TabIndex = 3;
            this.cmdAddEntry.Text = "Add Entry";
            this.cmdAddEntry.Click += new System.EventHandler(this.cmdAddEntry_Click);
            // 
            // txtValue
            // 
            // 
            // 
            // 
            this.txtValue.Border.Class = "TextBoxBorder";
            this.txtValue.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtValue.Location = new System.Drawing.Point(3, 29);
            this.txtValue.MaxLength = 1024;
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(151, 20);
            this.txtValue.TabIndex = 2;
            this.txtValue.WatermarkText = "Value";
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            // 
            // listDvars
            // 
            this.listDvars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDvar,
            this.columnValue});
            this.listDvars.FullRowSelect = true;
            this.listDvars.GridLines = true;
            this.listDvars.Location = new System.Drawing.Point(162, 2);
            this.listDvars.Name = "listDvars";
            this.listDvars.Size = new System.Drawing.Size(373, 251);
            this.listDvars.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listDvars.TabIndex = 6;
            this.listDvars.UseCompatibleStateImageBehavior = false;
            this.listDvars.View = System.Windows.Forms.View.Details;
            this.listDvars.SelectedIndexChanged += new System.EventHandler(this.listDvars_SelectedIndexChanged);
            // 
            // columnDvar
            // 
            this.columnDvar.Text = "DVAR";
            this.columnDvar.Width = 225;
            // 
            // columnValue
            // 
            this.columnValue.Text = "Value";
            this.columnValue.Width = 110;
            // 
            // tabSegments
            // 
            this.tabSegments.Name = "tabSegments";
            this.tabSegments.Panel = this.ribbonPanel1;
            this.tabSegments.Text = "Advanced";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.panelSegmentOperations);
            this.ribbonPanel1.Controls.Add(this.listSegments);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(538, 256);
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
            this.ribbonPanel1.TabIndex = 3;
            this.ribbonPanel1.Visible = false;
            // 
            // panelSegmentOperations
            // 
            this.panelSegmentOperations.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelSegmentOperations.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelSegmentOperations.Controls.Add(this.cmdExtractSegments);
            this.panelSegmentOperations.Controls.Add(this.cmdReplaceSegment);
            this.panelSegmentOperations.Controls.Add(this.cmdExtractSegment);
            this.panelSegmentOperations.Location = new System.Drawing.Point(3, 2);
            this.panelSegmentOperations.Name = "panelSegmentOperations";
            this.panelSegmentOperations.Size = new System.Drawing.Size(157, 251);
            this.panelSegmentOperations.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelSegmentOperations.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelSegmentOperations.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelSegmentOperations.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelSegmentOperations.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelSegmentOperations.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelSegmentOperations.Style.GradientAngle = 90;
            this.panelSegmentOperations.TabIndex = 6;
            // 
            // cmdExtractSegments
            // 
            this.cmdExtractSegments.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExtractSegments.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdExtractSegments.FocusCuesEnabled = false;
            this.cmdExtractSegments.Location = new System.Drawing.Point(3, 205);
            this.cmdExtractSegments.Name = "cmdExtractSegments";
            this.cmdExtractSegments.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdExtractSegments.Size = new System.Drawing.Size(151, 43);
            this.cmdExtractSegments.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExtractSegments.TabIndex = 4;
            this.cmdExtractSegments.Text = "Extract All";
            this.cmdExtractSegments.Click += new System.EventHandler(this.cmdExtractSegments_Click);
            // 
            // cmdReplaceSegment
            // 
            this.cmdReplaceSegment.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdReplaceSegment.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdReplaceSegment.FocusCuesEnabled = false;
            this.cmdReplaceSegment.Location = new System.Drawing.Point(3, 33);
            this.cmdReplaceSegment.Name = "cmdReplaceSegment";
            this.cmdReplaceSegment.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdReplaceSegment.Size = new System.Drawing.Size(151, 24);
            this.cmdReplaceSegment.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdReplaceSegment.TabIndex = 3;
            this.cmdReplaceSegment.Text = "Replace Segment";
            this.cmdReplaceSegment.Click += new System.EventHandler(this.cmdReplaceSegment_Click);
            // 
            // cmdExtractSegment
            // 
            this.cmdExtractSegment.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExtractSegment.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdExtractSegment.FocusCuesEnabled = false;
            this.cmdExtractSegment.Location = new System.Drawing.Point(3, 3);
            this.cmdExtractSegment.Name = "cmdExtractSegment";
            this.cmdExtractSegment.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdExtractSegment.Size = new System.Drawing.Size(151, 24);
            this.cmdExtractSegment.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExtractSegment.TabIndex = 2;
            this.cmdExtractSegment.Text = "Extract Segment";
            this.cmdExtractSegment.Click += new System.EventHandler(this.cmdExtractSegment_Click);
            // 
            // listSegments
            // 
            this.listSegments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listSegments.FullRowSelect = true;
            this.listSegments.GridLines = true;
            this.listSegments.Location = new System.Drawing.Point(162, 2);
            this.listSegments.MultiSelect = false;
            this.listSegments.Name = "listSegments";
            this.listSegments.Size = new System.Drawing.Size(373, 251);
            this.listSegments.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listSegments.TabIndex = 5;
            this.listSegments.UseCompatibleStateImageBehavior = false;
            this.listSegments.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Segment";
            this.columnHeader1.Width = 248;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 94;
            // 
            // cmdMaxHealth
            // 
            this.cmdMaxHealth.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxHealth.AutoCheckOnClick = true;
            this.cmdMaxHealth.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxHealth.FocusCuesEnabled = false;
            this.cmdMaxHealth.Location = new System.Drawing.Point(12, 8);
            this.cmdMaxHealth.Name = "cmdMaxHealth";
            this.cmdMaxHealth.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxHealth.Size = new System.Drawing.Size(254, 31);
            this.cmdMaxHealth.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxHealth.TabIndex = 0;
            this.cmdMaxHealth.Text = "Max Health - Sometimes Works";
            this.cmdMaxHealth.Visible = false;
            this.cmdMaxHealth.CheckedChanged += new System.EventHandler(this.cmdDvar_CheckedChanged);
            // 
            // cmdJumpSlowdown
            // 
            this.cmdJumpSlowdown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdJumpSlowdown.AutoCheckOnClick = true;
            this.cmdJumpSlowdown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdJumpSlowdown.FocusCuesEnabled = false;
            this.cmdJumpSlowdown.Location = new System.Drawing.Point(359, 45);
            this.cmdJumpSlowdown.Name = "cmdJumpSlowdown";
            this.cmdJumpSlowdown.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdJumpSlowdown.Size = new System.Drawing.Size(167, 31);
            this.cmdJumpSlowdown.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdJumpSlowdown.TabIndex = 1;
            this.cmdJumpSlowdown.Text = "Disable Jump Slowdown";
            this.cmdJumpSlowdown.CheckedChanged += new System.EventHandler(this.cmdDvar_CheckedChanged);
            // 
            // cmdGiveAll
            // 
            this.cmdGiveAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdGiveAll.AutoCheckOnClick = true;
            this.cmdGiveAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdGiveAll.FocusCuesEnabled = false;
            this.cmdGiveAll.Location = new System.Drawing.Point(12, 45);
            this.cmdGiveAll.Name = "cmdGiveAll";
            this.cmdGiveAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdGiveAll.Size = new System.Drawing.Size(167, 31);
            this.cmdGiveAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdGiveAll.TabIndex = 2;
            this.cmdGiveAll.Text = "Give All Guns";
            this.cmdGiveAll.CheckedChanged += new System.EventHandler(this.cmdDvar_CheckedChanged);
            // 
            // cmdDisableAI
            // 
            this.cmdDisableAI.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdDisableAI.AutoCheckOnClick = true;
            this.cmdDisableAI.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdDisableAI.FocusCuesEnabled = false;
            this.cmdDisableAI.Location = new System.Drawing.Point(272, 82);
            this.cmdDisableAI.Name = "cmdDisableAI";
            this.cmdDisableAI.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdDisableAI.Size = new System.Drawing.Size(254, 32);
            this.cmdDisableAI.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdDisableAI.TabIndex = 3;
            this.cmdDisableAI.Text = "Disable AI";
            this.cmdDisableAI.CheckedChanged += new System.EventHandler(this.cmdDvar_CheckedChanged);
            // 
            // cmdDebugUI
            // 
            this.cmdDebugUI.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdDebugUI.AutoCheckOnClick = true;
            this.cmdDebugUI.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdDebugUI.FocusCuesEnabled = false;
            this.cmdDebugUI.Location = new System.Drawing.Point(12, 82);
            this.cmdDebugUI.Name = "cmdDebugUI";
            this.cmdDebugUI.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdDebugUI.Size = new System.Drawing.Size(254, 31);
            this.cmdDebugUI.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdDebugUI.TabIndex = 4;
            this.cmdDebugUI.Text = "Show Debug UI";
            this.cmdDebugUI.CheckedChanged += new System.EventHandler(this.cmdDvar_CheckedChanged);
            // 
            // dvarSlider2
            // 
            this.dvarSlider2.BackColor = System.Drawing.Color.Transparent;
            this.dvarSlider2.Checked = true;
            this.dvarSlider2.DecimalPlaces = 0;
            this.dvarSlider2.Dvar = "g_speed";
            this.dvarSlider2.Location = new System.Drawing.Point(12, 164);
            this.dvarSlider2.Maximum = 1000F;
            this.dvarSlider2.Minimum = 0F;
            this.dvarSlider2.Name = "dvarSlider2";
            this.dvarSlider2.Size = new System.Drawing.Size(254, 39);
            this.dvarSlider2.TabIndex = 18;
            this.dvarSlider2.Title = "Player Speed";
            this.dvarSlider2.Value = 300F;
            // 
            // dvarSlider5
            // 
            this.dvarSlider5.BackColor = System.Drawing.Color.Transparent;
            this.dvarSlider5.Checked = false;
            this.dvarSlider5.DecimalPlaces = 0;
            this.dvarSlider5.Dvar = "jump_height";
            this.dvarSlider5.Location = new System.Drawing.Point(12, 209);
            this.dvarSlider5.Maximum = 1000F;
            this.dvarSlider5.Minimum = 0F;
            this.dvarSlider5.Name = "dvarSlider5";
            this.dvarSlider5.Size = new System.Drawing.Size(514, 39);
            this.dvarSlider5.TabIndex = 21;
            this.dvarSlider5.Title = "Jump Height";
            this.dvarSlider5.Value = 800F;
            // 
            // dvarSlider1
            // 
            this.dvarSlider1.BackColor = System.Drawing.Color.Transparent;
            this.dvarSlider1.Checked = true;
            this.dvarSlider1.DecimalPlaces = 1;
            this.dvarSlider1.Dvar = "timescale";
            this.dvarSlider1.Location = new System.Drawing.Point(12, 119);
            this.dvarSlider1.Maximum = 10F;
            this.dvarSlider1.Minimum = 0F;
            this.dvarSlider1.Name = "dvarSlider1";
            this.dvarSlider1.Size = new System.Drawing.Size(514, 39);
            this.dvarSlider1.TabIndex = 22;
            this.dvarSlider1.Title = "Game Speed Multiplier";
            this.dvarSlider1.Value = 1F;
            // 
            // cmdUnlimitedSprint
            // 
            this.cmdUnlimitedSprint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdUnlimitedSprint.AutoCheckOnClick = true;
            this.cmdUnlimitedSprint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdUnlimitedSprint.FocusCuesEnabled = false;
            this.cmdUnlimitedSprint.Location = new System.Drawing.Point(12, 8);
            this.cmdUnlimitedSprint.Name = "cmdUnlimitedSprint";
            this.cmdUnlimitedSprint.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdUnlimitedSprint.Size = new System.Drawing.Size(514, 31);
            this.cmdUnlimitedSprint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdUnlimitedSprint.TabIndex = 23;
            this.cmdUnlimitedSprint.Text = "Unlimited Sprint";
            this.cmdUnlimitedSprint.CheckedChanged += new System.EventHandler(this.cmdDvar_CheckedChanged);
            // 
            // dvarSlider3
            // 
            this.dvarSlider3.BackColor = System.Drawing.Color.Transparent;
            this.dvarSlider3.Checked = true;
            this.dvarSlider3.DecimalPlaces = 1;
            this.dvarSlider3.Dvar = "player_sprintSpeedScale";
            this.dvarSlider3.Location = new System.Drawing.Point(272, 164);
            this.dvarSlider3.Maximum = 10F;
            this.dvarSlider3.Minimum = 0F;
            this.dvarSlider3.Name = "dvarSlider3";
            this.dvarSlider3.Size = new System.Drawing.Size(254, 39);
            this.dvarSlider3.TabIndex = 24;
            this.dvarSlider3.Title = "Sprint Speed Multiplier";
            this.dvarSlider3.Value = 4F;
            // 
            // cmdHideHud
            // 
            this.cmdHideHud.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdHideHud.AutoCheckOnClick = true;
            this.cmdHideHud.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdHideHud.FocusCuesEnabled = false;
            this.cmdHideHud.Location = new System.Drawing.Point(185, 45);
            this.cmdHideHud.Name = "cmdHideHud";
            this.cmdHideHud.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdHideHud.Size = new System.Drawing.Size(168, 31);
            this.cmdHideHud.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdHideHud.TabIndex = 25;
            this.cmdHideHud.Text = "Hide HUD";
            this.cmdHideHud.CheckedChanged += new System.EventHandler(this.cmdDvar_CheckedChanged);
            // 
            // MW3CampaignSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 314);
            this.Name = "MW3CampaignSave";
            this.Text = "Modern Warfare 3 Campaign";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelAdvanced.ResumeLayout(false);
            this.panelAdvancedEntry.ResumeLayout(false);
            this.ribbonPanel1.ResumeLayout(false);
            this.panelSegmentOperations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonPanel panelAdvanced;
        private DevComponents.DotNetBar.RibbonTabItem tabAdvanced;
        private DevComponents.DotNetBar.PanelEx panelAdvancedEntry;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDvar;
        private DevComponents.DotNetBar.ButtonX cmdRemoveEntry;
        private DevComponents.DotNetBar.ButtonX cmdAddEntry;
        private DevComponents.DotNetBar.Controls.TextBoxX txtValue;
        internal System.Windows.Forms.ListView listDvars;
        private System.Windows.Forms.ColumnHeader columnDvar;
        private System.Windows.Forms.ColumnHeader columnValue;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonTabItem tabSegments;
        private DevComponents.DotNetBar.PanelEx panelSegmentOperations;
        private DevComponents.DotNetBar.ButtonX cmdReplaceSegment;
        private DevComponents.DotNetBar.ButtonX cmdExtractSegment;
        private System.Windows.Forms.ListView listSegments;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private DevComponents.DotNetBar.ButtonX cmdExtractSegments;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private DevComponents.DotNetBar.ButtonX cmdSearch;
        private Horizon.Controls.DvarButton cmdDebugUI;
        private Horizon.Controls.DvarButton cmdDisableAI;
        private Horizon.Controls.DvarButton cmdGiveAll;
        private Horizon.Controls.DvarButton cmdJumpSlowdown;
        private Horizon.Controls.DvarButton cmdMaxHealth;
        private Horizon.Controls.DvarButton cmdUnlimitedSprint;
        private Horizon.Controls.DvarSlider dvarSlider1;
        private Horizon.Controls.DvarSlider dvarSlider5;
        private Horizon.Controls.DvarSlider dvarSlider2;
        private Horizon.Controls.DvarSlider dvarSlider3;
        private Horizon.Controls.DvarButton cmdHideHud;
    }
}