namespace DRPlanningTool
{
    partial class frmDrawings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDrawings));
            this.grpDrawings = new System.Windows.Forms.GroupBox();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lvwDrawings = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlMain = new System.Windows.Forms.ImageList(this.components);
            this.imlDrawingStatus = new System.Windows.Forms.ImageList(this.components);
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.DrawingContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createManagementPackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createStubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productionStubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testStubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developmentStubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ApprovedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewDependencyMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpDrawings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.DrawingContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDrawings
            // 
            this.grpDrawings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDrawings.Controls.Add(this.btnClearFilter);
            this.grpDrawings.Controls.Add(this.btnFilter);
            this.grpDrawings.Controls.Add(this.txtFilter);
            this.grpDrawings.Controls.Add(this.lblFilter);
            this.grpDrawings.Controls.Add(this.pictureBox);
            this.grpDrawings.Controls.Add(this.lvwDrawings);
            this.grpDrawings.Location = new System.Drawing.Point(10, 11);
            this.grpDrawings.Name = "grpDrawings";
            this.grpDrawings.Size = new System.Drawing.Size(321, 580);
            this.grpDrawings.TabIndex = 3;
            this.grpDrawings.TabStop = false;
            this.grpDrawings.Text = "Drawings";
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFilter.Enabled = false;
            this.btnClearFilter.Image = global::DRPlanningTool.Properties.Resources.Close_16;
            this.btnClearFilter.Location = new System.Drawing.Point(259, 19);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(25, 25);
            this.btnClearFilter.TabIndex = 2;
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.Enabled = false;
            this.btnFilter.Image = global::DRPlanningTool.Properties.Resources.Ok_16;
            this.btnFilter.Location = new System.Drawing.Point(290, 19);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(25, 25);
            this.btnFilter.TabIndex = 3;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Enabled = false;
            this.txtFilter.Location = new System.Drawing.Point(41, 22);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(212, 20);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(6, 25);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(29, 13);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Filter";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox.Location = new System.Drawing.Point(6, 402);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(309, 172);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDoubleClick);
            // 
            // lvwDrawings
            // 
            this.lvwDrawings.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.lvwDrawings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwDrawings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lvwDrawings.FullRowSelect = true;
            this.lvwDrawings.Location = new System.Drawing.Point(6, 50);
            this.lvwDrawings.Name = "lvwDrawings";
            this.lvwDrawings.Size = new System.Drawing.Size(308, 345);
            this.lvwDrawings.SmallImageList = this.imlMain;
            this.lvwDrawings.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwDrawings.TabIndex = 4;
            this.lvwDrawings.UseCompatibleStateImageBehavior = false;
            this.lvwDrawings.View = System.Windows.Forms.View.Details;
            this.lvwDrawings.ItemActivate += new System.EventHandler(this.lvwDrawings_ItemActivate);
            this.lvwDrawings.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvwDrawings_ItemSelectionChanged);
            this.lvwDrawings.SelectedIndexChanged += new System.EventHandler(this.lvwDrawings_SelectedIndexChanged);
            this.lvwDrawings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwDrawings_KeyDown);
            this.lvwDrawings.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwDrawings_MouseClick);
            this.lvwDrawings.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwDrawings_MouseDoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 280;
            // 
            // imlMain
            // 
            this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
            this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imlMain.Images.SetKeyName(0, "gear_blue_32.png");
            this.imlMain.Images.SetKeyName(1, "gear_grey_32.png");
            this.imlMain.Images.SetKeyName(2, "gear_red_32.png");
            this.imlMain.Images.SetKeyName(3, "gear_orange_32.png");
            this.imlMain.Images.SetKeyName(4, "gear_green_32.png");
            this.imlMain.Images.SetKeyName(5, "Server2_blue_32.png");
            this.imlMain.Images.SetKeyName(6, "Server2_grey_32.png");
            this.imlMain.Images.SetKeyName(7, "Server2_red_32.png");
            this.imlMain.Images.SetKeyName(8, "Server2_orange_32.png");
            this.imlMain.Images.SetKeyName(9, "Server2_green_32.png");
            this.imlMain.Images.SetKeyName(10, "Blueprint_blue_32.png");
            this.imlMain.Images.SetKeyName(11, "Blueprint_none_32.png");
            this.imlMain.Images.SetKeyName(12, "Blueprint_grey_32.png");
            this.imlMain.Images.SetKeyName(13, "Blueprint_red_32.png");
            this.imlMain.Images.SetKeyName(14, "Blueprint_orange_32.png");
            this.imlMain.Images.SetKeyName(15, "Blueprint_green_32.png");
            // 
            // imlDrawingStatus
            // 
            this.imlDrawingStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlDrawingStatus.ImageStream")));
            this.imlDrawingStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imlDrawingStatus.Images.SetKeyName(0, "No_128.png");
            this.imlDrawingStatus.Images.SetKeyName(1, "Draft_128.png");
            this.imlDrawingStatus.Images.SetKeyName(2, "PendingReview_128.png");
            this.imlDrawingStatus.Images.SetKeyName(3, "Approved_128.png");
            this.imlDrawingStatus.Images.SetKeyName(4, "OutOfScope_128.png");
            // 
            // DrawingContextMenu
            // 
            this.DrawingContextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.DrawingContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyNamesToolStripMenuItem,
            this.createManagementPackToolStripMenuItem,
            this.openToolStripMenuItem,
            this.StatusToolStripMenuItem,
            this.ViewDependencyMapToolStripMenuItem});
            this.DrawingContextMenu.Name = "DrawingContextMenu";
            this.DrawingContextMenu.Size = new System.Drawing.Size(204, 154);
            this.DrawingContextMenu.Text = "Drawing";
            // 
            // copyNamesToolStripMenuItem
            // 
            this.copyNamesToolStripMenuItem.Enabled = false;
            this.copyNamesToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.papers_32;
            this.copyNamesToolStripMenuItem.Name = "copyNamesToolStripMenuItem";
            this.copyNamesToolStripMenuItem.Size = new System.Drawing.Size(203, 30);
            this.copyNamesToolStripMenuItem.Text = "Copy Names";
            this.copyNamesToolStripMenuItem.Click += new System.EventHandler(this.copyNamesToolStripMenuItem_Click);
            // 
            // createManagementPackToolStripMenuItem
            // 
            this.createManagementPackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.createStubToolStripMenuItem});
            this.createManagementPackToolStripMenuItem.Enabled = false;
            this.createManagementPackToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_32;
            this.createManagementPackToolStripMenuItem.Name = "createManagementPackToolStripMenuItem";
            this.createManagementPackToolStripMenuItem.Size = new System.Drawing.Size(203, 30);
            this.createManagementPackToolStripMenuItem.Text = "Management Pack";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productionToolStripMenuItem,
            this.testToolStripMenuItem,
            this.developmentToolStripMenuItem});
            this.createToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_full_32;
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // productionToolStripMenuItem
            // 
            this.productionToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_american_32;
            this.productionToolStripMenuItem.Name = "productionToolStripMenuItem";
            this.productionToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.productionToolStripMenuItem.Text = "Production";
            this.productionToolStripMenuItem.Click += new System.EventHandler(this.productionToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_programmer_32;
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // developmentToolStripMenuItem
            // 
            this.developmentToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_machine_operator_32;
            this.developmentToolStripMenuItem.Name = "developmentToolStripMenuItem";
            this.developmentToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.developmentToolStripMenuItem.Text = "Development";
            this.developmentToolStripMenuItem.Click += new System.EventHandler(this.developmentToolStripMenuItem_Click);
            // 
            // createStubToolStripMenuItem
            // 
            this.createStubToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productionStubToolStripMenuItem,
            this.testStubToolStripMenuItem,
            this.developmentStubToolStripMenuItem});
            this.createStubToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_empty_32;
            this.createStubToolStripMenuItem.Name = "createStubToolStripMenuItem";
            this.createStubToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.createStubToolStripMenuItem.Text = "Create stub";
            // 
            // productionStubToolStripMenuItem
            // 
            this.productionStubToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_american_32;
            this.productionStubToolStripMenuItem.Name = "productionStubToolStripMenuItem";
            this.productionStubToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.productionStubToolStripMenuItem.Text = "Production";
            this.productionStubToolStripMenuItem.Click += new System.EventHandler(this.productionStubToolStripMenuItem_Click);
            // 
            // testStubToolStripMenuItem
            // 
            this.testStubToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_programmer_32;
            this.testStubToolStripMenuItem.Name = "testStubToolStripMenuItem";
            this.testStubToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.testStubToolStripMenuItem.Text = "Test";
            this.testStubToolStripMenuItem.Click += new System.EventHandler(this.testStubToolStripMenuItem_Click);
            // 
            // developmentStubToolStripMenuItem
            // 
            this.developmentStubToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_machine_operator_32;
            this.developmentStubToolStripMenuItem.Name = "developmentStubToolStripMenuItem";
            this.developmentStubToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.developmentStubToolStripMenuItem.Text = "Development";
            this.developmentStubToolStripMenuItem.Click += new System.EventHandler(this.developmentStubToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Enabled = false;
            this.openToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Blueprint_grey_32;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(203, 30);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // StatusToolStripMenuItem
            // 
            this.StatusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ApprovedToolStripMenuItem,
            this.PendingToolStripMenuItem,
            this.DraftToolStripMenuItem});
            this.StatusToolStripMenuItem.Enabled = false;
            this.StatusToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Blueprint_blue_32;
            this.StatusToolStripMenuItem.Name = "StatusToolStripMenuItem";
            this.StatusToolStripMenuItem.Size = new System.Drawing.Size(203, 30);
            this.StatusToolStripMenuItem.Text = "Status";
            // 
            // ApprovedToolStripMenuItem
            // 
            this.ApprovedToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Blueprint_green_32;
            this.ApprovedToolStripMenuItem.Name = "ApprovedToolStripMenuItem";
            this.ApprovedToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.ApprovedToolStripMenuItem.Text = "Approved";
            this.ApprovedToolStripMenuItem.Click += new System.EventHandler(this.ApprovedToolStripMenuItem_Click);
            // 
            // PendingToolStripMenuItem
            // 
            this.PendingToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Blueprint_orange_32;
            this.PendingToolStripMenuItem.Name = "PendingToolStripMenuItem";
            this.PendingToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.PendingToolStripMenuItem.Text = "Pending";
            this.PendingToolStripMenuItem.Click += new System.EventHandler(this.PendingToolStripMenuItem_Click);
            // 
            // DraftToolStripMenuItem
            // 
            this.DraftToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Blueprint_grey_32;
            this.DraftToolStripMenuItem.Name = "DraftToolStripMenuItem";
            this.DraftToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.DraftToolStripMenuItem.Text = "Draft";
            this.DraftToolStripMenuItem.Click += new System.EventHandler(this.DraftToolStripMenuItem_Click);
            // 
            // ViewDependencyMapToolStripMenuItem
            // 
            this.ViewDependencyMapToolStripMenuItem.Enabled = false;
            this.ViewDependencyMapToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Globe_32;
            this.ViewDependencyMapToolStripMenuItem.Name = "ViewDependencyMapToolStripMenuItem";
            this.ViewDependencyMapToolStripMenuItem.Size = new System.Drawing.Size(203, 30);
            this.ViewDependencyMapToolStripMenuItem.Text = "View Dependency Map";
            this.ViewDependencyMapToolStripMenuItem.Click += new System.EventHandler(this.ViewDependencyMapToolStripMenuItem_Click);
            // 
            // frmDrawings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 601);
            this.Controls.Add(this.grpDrawings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(795, 1986);
            this.MinimumSize = new System.Drawing.Size(296, 387);
            this.Name = "frmDrawings";
            this.Text = "Drawings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDrawings_FormClosing);
            this.Resize += new System.EventHandler(this.frmDrawings_Resize);
            this.grpDrawings.ResumeLayout(false);
            this.grpDrawings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.DrawingContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDrawings;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imlDrawingStatus;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.ContextMenuStrip DrawingContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createManagementPackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem developmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createStubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productionStubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testStubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem developmentStubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ApprovedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PendingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DraftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewDependencyMapToolStripMenuItem;
        public System.Windows.Forms.PictureBox pictureBox;
        public System.Windows.Forms.ListView lvwDrawings;
        private System.Windows.Forms.ImageList imlMain;
    }
}