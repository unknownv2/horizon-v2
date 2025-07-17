namespace Horizon.PackageEditors.State_of_Decay.Controls
{
    partial class NSInventoryListObject
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
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cmbItem = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbCategory = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnMaxAllWeaponAttr = new DevComponents.DotNetBar.ButtonX();
            this.treeInventoryList = new DevComponents.AdvTree.AdvTree();
            this.columnHeader5 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader6 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.btnMaxAllSelected = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeInventoryList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.cmbItem);
            this.groupPanel1.Controls.Add(this.cmbCategory);
            this.groupPanel1.Controls.Add(this.btnMaxAllSelected);
            this.groupPanel1.Controls.Add(this.btnMaxAllWeaponAttr);
            this.groupPanel1.Controls.Add(this.treeInventoryList);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(424, 289);
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
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "Inventory List";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(263, 168);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(56, 22);
            this.labelX2.TabIndex = 130;
            this.labelX2.Text = "Item:";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(60, 167);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(56, 23);
            this.labelX1.TabIndex = 130;
            this.labelX1.Text = "Category:";
            // 
            // cmbItem
            // 
            this.cmbItem.DisplayMember = "Text";
            this.cmbItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.ItemHeight = 14;
            this.cmbItem.Location = new System.Drawing.Point(175, 196);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(239, 20);
            this.cmbItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbItem.TabIndex = 129;
            this.cmbItem.SelectedIndexChanged += new System.EventHandler(this.InvCmbItem_SelectedIndexChanged);
            // 
            // cmbCategory
            // 
            this.cmbCategory.DisplayMember = "Text";
            this.cmbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.ItemHeight = 14;
            this.cmbCategory.Location = new System.Drawing.Point(13, 196);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(136, 20);
            this.cmbCategory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbCategory.TabIndex = 128;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.InvCmbCategory_SelectedIndexChanged);
            // 
            // btnMaxAllWeaponAttr
            // 
            this.btnMaxAllWeaponAttr.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxAllWeaponAttr.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxAllWeaponAttr.Image = global::Horizon.Properties.Resources.Plus;
            this.btnMaxAllWeaponAttr.Location = new System.Drawing.Point(13, 244);
            this.btnMaxAllWeaponAttr.Name = "btnMaxAllWeaponAttr";
            this.btnMaxAllWeaponAttr.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxAllWeaponAttr.Size = new System.Drawing.Size(401, 23);
            this.btnMaxAllWeaponAttr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxAllWeaponAttr.TabIndex = 127;
            this.btnMaxAllWeaponAttr.Text = "Max All Items";
            this.btnMaxAllWeaponAttr.Click += new System.EventHandler(this.BtnClick_MaxAllWeaponAttributes);
            // 
            // treeInventoryList
            // 
            this.treeInventoryList.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeInventoryList.AllowDrop = true;
            this.treeInventoryList.AllowUserToReorderColumns = true;
            this.treeInventoryList.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeInventoryList.BackgroundStyle.Class = "TreeBorderKey";
            this.treeInventoryList.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeInventoryList.Columns.Add(this.columnHeader5);
            this.treeInventoryList.Columns.Add(this.columnHeader6);
            this.treeInventoryList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeInventoryList.Location = new System.Drawing.Point(7, 6);
            this.treeInventoryList.MultiSelect = true;
            this.treeInventoryList.Name = "treeInventoryList";
            this.treeInventoryList.NodesConnector = this.nodeConnector2;
            this.treeInventoryList.NodeStyle = this.elementStyle2;
            this.treeInventoryList.PathSeparator = ";";
            this.treeInventoryList.Size = new System.Drawing.Size(407, 157);
            this.treeInventoryList.Styles.Add(this.elementStyle2);
            this.treeInventoryList.TabIndex = 126;
            this.treeInventoryList.Text = "advTree1";
            this.treeInventoryList.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.AdvTree_AfterInvItemSelect);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Name = "columnHeader5";
            this.columnHeader5.Text = "Category";
            this.columnHeader5.Width.Absolute = 170;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Name = "columnHeader6";
            this.columnHeader6.Text = "Item";
            this.columnHeader6.Width.Absolute = 180;
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
            // btnMaxAllSelected
            // 
            this.btnMaxAllSelected.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxAllSelected.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxAllSelected.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxAllSelected.Location = new System.Drawing.Point(13, 222);
            this.btnMaxAllSelected.Name = "btnMaxAllSelected";
            this.btnMaxAllSelected.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxAllSelected.Size = new System.Drawing.Size(401, 23);
            this.btnMaxAllSelected.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxAllSelected.TabIndex = 127;
            this.btnMaxAllSelected.Text = "Max Only Items Selected";
            this.btnMaxAllSelected.Click += new System.EventHandler(this.BtnClick_MaxAllSelected);
            // 
            // NSInventoryListObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupPanel1);
            this.Name = "NSInventoryListObject";
            this.Size = new System.Drawing.Size(424, 289);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeInventoryList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.AdvTree.AdvTree treeInventoryList;
        private DevComponents.AdvTree.ColumnHeader columnHeader5;
        private DevComponents.AdvTree.ColumnHeader columnHeader6;
        private DevComponents.AdvTree.NodeConnector nodeConnector2;
        private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private DevComponents.DotNetBar.ButtonX btnMaxAllWeaponAttr;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbItem;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbCategory;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnMaxAllSelected;
    }
}
