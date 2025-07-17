namespace Horizon.PackageEditors.State_of_Decay.Controls
{
    partial class NSCharacterObject
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
            this.treeSkills = new DevComponents.AdvTree.AdvTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader3 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.cmbLastNames = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbFirstNames = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbCharacterModels = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.chkUnlockAsFriend = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.intFriendProg = new DevComponents.Editors.IntegerInput();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnMaxStamina = new DevComponents.DotNetBar.ButtonX();
            this.btnMaxHealth = new DevComponents.DotNetBar.ButtonX();
            this.fStamina = new DevComponents.Editors.DoubleInput();
            this.fHealth = new DevComponents.Editors.DoubleInput();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnClearTraits = new DevComponents.DotNetBar.ButtonX();
            this.treeTraits = new DevComponents.AdvTree.AdvTree();
            this.columnHeader4 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.rTxtTraitDesc = new System.Windows.Forms.RichTextBox();
            this.cmbTraitChoice = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.treeSkills)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intFriendProg)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fStamina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fHealth)).BeginInit();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeTraits)).BeginInit();
            this.SuspendLayout();
            // 
            // treeSkills
            // 
            this.treeSkills.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeSkills.AllowDrop = true;
            this.treeSkills.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeSkills.BackgroundStyle.Class = "TreeBorderKey";
            this.treeSkills.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeSkills.Columns.Add(this.columnHeader1);
            this.treeSkills.Columns.Add(this.columnHeader3);
            this.treeSkills.Columns.Add(this.columnHeader2);
            this.treeSkills.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeSkills.Location = new System.Drawing.Point(6, 193);
            this.treeSkills.Name = "treeSkills";
            this.treeSkills.NodesConnector = this.nodeConnector1;
            this.treeSkills.NodeStyle = this.elementStyle1;
            this.treeSkills.PathSeparator = ";";
            this.treeSkills.Size = new System.Drawing.Size(407, 214);
            this.treeSkills.Styles.Add(this.elementStyle1);
            this.treeSkills.TabIndex = 122;
            this.treeSkills.Text = "advTree1";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Skill";
            this.columnHeader1.Width.Absolute = 170;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "Level";
            this.columnHeader3.Width.Absolute = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width.Absolute = 150;
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
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.cmbLastNames);
            this.panelEx2.Controls.Add(this.cmbFirstNames);
            this.panelEx2.Controls.Add(this.cmbCharacterModels);
            this.panelEx2.Controls.Add(this.chkUnlockAsFriend);
            this.panelEx2.Controls.Add(this.intFriendProg);
            this.panelEx2.Controls.Add(this.labelX6);
            this.panelEx2.Controls.Add(this.labelX5);
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Location = new System.Drawing.Point(6, 5);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(407, 96);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 123;
            // 
            // cmbLastNames
            // 
            this.cmbLastNames.DisplayMember = "Text";
            this.cmbLastNames.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLastNames.FormattingEnabled = true;
            this.cmbLastNames.ItemHeight = 14;
            this.cmbLastNames.Location = new System.Drawing.Point(274, 66);
            this.cmbLastNames.Name = "cmbLastNames";
            this.cmbLastNames.Size = new System.Drawing.Size(127, 20);
            this.cmbLastNames.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbLastNames.TabIndex = 3;
            // 
            // cmbFirstNames
            // 
            this.cmbFirstNames.DisplayMember = "Text";
            this.cmbFirstNames.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFirstNames.FormattingEnabled = true;
            this.cmbFirstNames.ItemHeight = 14;
            this.cmbFirstNames.Location = new System.Drawing.Point(72, 66);
            this.cmbFirstNames.Name = "cmbFirstNames";
            this.cmbFirstNames.Size = new System.Drawing.Size(127, 20);
            this.cmbFirstNames.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbFirstNames.TabIndex = 3;
            // 
            // cmbCharacterModels
            // 
            this.cmbCharacterModels.DisplayMember = "Text";
            this.cmbCharacterModels.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCharacterModels.FormattingEnabled = true;
            this.cmbCharacterModels.ItemHeight = 14;
            this.cmbCharacterModels.Location = new System.Drawing.Point(71, 37);
            this.cmbCharacterModels.Name = "cmbCharacterModels";
            this.cmbCharacterModels.Size = new System.Drawing.Size(330, 20);
            this.cmbCharacterModels.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbCharacterModels.TabIndex = 3;
            // 
            // chkUnlockAsFriend
            // 
            // 
            // 
            // 
            this.chkUnlockAsFriend.BackgroundStyle.Class = "";
            this.chkUnlockAsFriend.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkUnlockAsFriend.Location = new System.Drawing.Point(304, 7);
            this.chkUnlockAsFriend.Name = "chkUnlockAsFriend";
            this.chkUnlockAsFriend.Size = new System.Drawing.Size(80, 23);
            this.chkUnlockAsFriend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkUnlockAsFriend.TabIndex = 2;
            this.chkUnlockAsFriend.Text = "Is Friend";
            // 
            // intFriendProg
            // 
            // 
            // 
            // 
            this.intFriendProg.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intFriendProg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intFriendProg.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intFriendProg.Location = new System.Drawing.Point(174, 8);
            this.intFriendProg.MaxValue = 100;
            this.intFriendProg.MinValue = 0;
            this.intFriendProg.Name = "intFriendProg";
            this.intFriendProg.ShowUpDown = true;
            this.intFriendProg.Size = new System.Drawing.Size(80, 20);
            this.intFriendProg.TabIndex = 1;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(208, 65);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(60, 23);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "Last Name:";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(8, 65);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(58, 23);
            this.labelX5.TabIndex = 0;
            this.labelX5.Text = "First Name:";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(9, 36);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(56, 23);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "Model:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(49, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(105, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Progress as Friend:";
            // 
            // groupPanel1
            // 
            this.groupPanel1.AutoScroll = true;
            this.groupPanel1.AutoSize = true;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.groupPanel2);
            this.groupPanel1.Controls.Add(this.panelEx1);
            this.groupPanel1.Controls.Add(this.panelEx2);
            this.groupPanel1.Controls.Add(this.treeSkills);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(423, 704);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 124;
            this.groupPanel1.Text = "Character Object Values";
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.btnMaxStamina);
            this.groupPanel2.Controls.Add(this.btnMaxHealth);
            this.groupPanel2.Controls.Add(this.fStamina);
            this.groupPanel2.Controls.Add(this.fHealth);
            this.groupPanel2.Controls.Add(this.labelX3);
            this.groupPanel2.Controls.Add(this.labelX2);
            this.groupPanel2.Location = new System.Drawing.Point(6, 107);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(407, 78);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 127;
            this.groupPanel2.Text = "Health and Stamina";
            // 
            // btnMaxStamina
            // 
            this.btnMaxStamina.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxStamina.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxStamina.Location = new System.Drawing.Point(330, 32);
            this.btnMaxStamina.Name = "btnMaxStamina";
            this.btnMaxStamina.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxStamina.Size = new System.Drawing.Size(70, 23);
            this.btnMaxStamina.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxStamina.TabIndex = 1;
            this.btnMaxStamina.Text = "Max Stamina";
            // 
            // btnMaxHealth
            // 
            this.btnMaxHealth.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxHealth.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxHealth.Location = new System.Drawing.Point(330, 3);
            this.btnMaxHealth.Name = "btnMaxHealth";
            this.btnMaxHealth.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxHealth.Size = new System.Drawing.Size(70, 23);
            this.btnMaxHealth.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxHealth.TabIndex = 1;
            this.btnMaxHealth.Text = "Max Health";
            // 
            // fStamina
            // 
            // 
            // 
            // 
            this.fStamina.BackgroundStyle.Class = "DateTimeInputBackground";
            this.fStamina.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fStamina.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.fStamina.Increment = 1;
            this.fStamina.Location = new System.Drawing.Point(63, 34);
            this.fStamina.MaxValue = 3.402823E+38;
            this.fStamina.MinValue = 0;
            this.fStamina.Name = "fStamina";
            this.fStamina.ShowUpDown = true;
            this.fStamina.Size = new System.Drawing.Size(261, 20);
            this.fStamina.TabIndex = 0;
            // 
            // fHealth
            // 
            // 
            // 
            // 
            this.fHealth.BackgroundStyle.Class = "DateTimeInputBackground";
            this.fHealth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fHealth.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.fHealth.Increment = 1;
            this.fHealth.Location = new System.Drawing.Point(63, 5);
            this.fHealth.MaxValue = 3.402823E+38;
            this.fHealth.MinValue = 0;
            this.fHealth.Name = "fHealth";
            this.fHealth.ShowUpDown = true;
            this.fHealth.Size = new System.Drawing.Size(261, 20);
            this.fHealth.TabIndex = 0;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(10, 32);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(47, 23);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "Stamina:";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(10, 3);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(43, 23);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "Health:";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.cmbTraitChoice);
            this.panelEx1.Controls.Add(this.btnClearTraits);
            this.panelEx1.Controls.Add(this.treeTraits);
            this.panelEx1.Controls.Add(this.rTxtTraitDesc);
            this.panelEx1.Location = new System.Drawing.Point(6, 413);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(407, 268);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 126;
            // 
            // btnClearTraits
            // 
            this.btnClearTraits.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClearTraits.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClearTraits.Location = new System.Drawing.Point(9, 240);
            this.btnClearTraits.Name = "btnClearTraits";
            this.btnClearTraits.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnClearTraits.Size = new System.Drawing.Size(388, 23);
            this.btnClearTraits.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClearTraits.TabIndex = 126;
            this.btnClearTraits.Text = "Clear Traits";
            this.btnClearTraits.Click += new System.EventHandler(this.BtnClick_ClearAllTraits);
            // 
            // treeTraits
            // 
            this.treeTraits.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeTraits.AllowDrop = true;
            this.treeTraits.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeTraits.BackgroundStyle.Class = "TreeBorderKey";
            this.treeTraits.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeTraits.Columns.Add(this.columnHeader4);
            this.treeTraits.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeTraits.Location = new System.Drawing.Point(9, 8);
            this.treeTraits.Name = "treeTraits";
            this.treeTraits.NodesConnector = this.nodeConnector2;
            this.treeTraits.NodeStyle = this.elementStyle2;
            this.treeTraits.PathSeparator = ";";
            this.treeTraits.Size = new System.Drawing.Size(388, 155);
            this.treeTraits.Styles.Add(this.elementStyle2);
            this.treeTraits.TabIndex = 124;
            this.treeTraits.Text = "advTree1";
            this.treeTraits.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.TraitTree_NodeSelected);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Name = "columnHeader4";
            this.columnHeader4.Text = "Trait";
            this.columnHeader4.Width.Absolute = 300;
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
            // rTxtTraitDesc
            // 
            this.rTxtTraitDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rTxtTraitDesc.Location = new System.Drawing.Point(9, 196);
            this.rTxtTraitDesc.Name = "rTxtTraitDesc";
            this.rTxtTraitDesc.Size = new System.Drawing.Size(388, 38);
            this.rTxtTraitDesc.TabIndex = 125;
            this.rTxtTraitDesc.Text = "";
            // 
            // cmbTraitChoice
            // 
            this.cmbTraitChoice.DisplayMember = "Text";
            this.cmbTraitChoice.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTraitChoice.FormattingEnabled = true;
            this.cmbTraitChoice.ItemHeight = 14;
            this.cmbTraitChoice.Location = new System.Drawing.Point(11, 170);
            this.cmbTraitChoice.Name = "cmbTraitChoice";
            this.cmbTraitChoice.Size = new System.Drawing.Size(386, 20);
            this.cmbTraitChoice.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTraitChoice.TabIndex = 127;
            // 
            // NSCharacterObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupPanel1);
            this.Name = "NSCharacterObject";
            this.Size = new System.Drawing.Size(423, 704);
            ((System.ComponentModel.ISupportInitialize)(this.treeSkills)).EndInit();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intFriendProg)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fStamina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fHealth)).EndInit();
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeTraits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.AdvTree.AdvTree treeSkills;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.AdvTree.ColumnHeader columnHeader3;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.IntegerInput intFriendProg;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkUnlockAsFriend;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.AdvTree.AdvTree treeTraits;
        private DevComponents.AdvTree.ColumnHeader columnHeader4;
        private DevComponents.AdvTree.NodeConnector nodeConnector2;
        private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.RichTextBox rTxtTraitDesc;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.Editors.DoubleInput fStamina;
        private DevComponents.Editors.DoubleInput fHealth;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnClearTraits;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbCharacterModels;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbLastNames;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbFirstNames;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnMaxHealth;
        private DevComponents.DotNetBar.ButtonX btnMaxStamina;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTraitChoice;
    }
}
