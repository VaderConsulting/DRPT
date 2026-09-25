namespace DRPlanningTool
{
    partial class frmInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInformation));
            this.imlServices = new System.Windows.Forms.ImageList(this.components);
            this.imlWarnings = new System.Windows.Forms.ImageList(this.components);
            this.DrawingContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createManagementPackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createStubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.developmentToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewDependencyMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvwOutOfScope = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabInformation = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnCopyMessages = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tvwApplications = new System.Windows.Forms.TreeView();
            this.tvwMessages = new System.Windows.Forms.TreeView();
            this.radTarget = new System.Windows.Forms.RadioButton();
            this.radMessage = new System.Windows.Forms.RadioButton();
            this.btnCopyValidationErrors = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCopyOutOfScopeServices = new System.Windows.Forms.Button();
            this.lblDRContext = new System.Windows.Forms.Label();
            this.DrawingContextMenu.SuspendLayout();
            this.tabInformation.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imlServices
            // 
            this.imlServices.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlServices.ImageStream")));
            this.imlServices.TransparentColor = System.Drawing.Color.Transparent;
            this.imlServices.Images.SetKeyName(0, "gear_blue_32.png");
            this.imlServices.Images.SetKeyName(1, "gear_grey_32.png");
            this.imlServices.Images.SetKeyName(2, "gear_red_32.png");
            this.imlServices.Images.SetKeyName(3, "gear_orange_32.png");
            this.imlServices.Images.SetKeyName(4, "gear_green_32.png");
            // 
            // imlWarnings
            // 
            this.imlWarnings.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlWarnings.ImageStream")));
            this.imlWarnings.TransparentColor = System.Drawing.Color.Transparent;
            this.imlWarnings.Images.SetKeyName(0, "i_32_yellowblack.png");
            this.imlWarnings.Images.SetKeyName(1, "Close_16.png");
            this.imlWarnings.Images.SetKeyName(2, "Visio_Blue_16.png");
            this.imlWarnings.Images.SetKeyName(3, "DatabaseRefresh_Blue_16.png");
            this.imlWarnings.Images.SetKeyName(4, "database_blue_16.png");
            this.imlWarnings.Images.SetKeyName(5, "Visio_Red_16.png");
            this.imlWarnings.Images.SetKeyName(6, "Blueprint_blue_16.png");
            this.imlWarnings.Images.SetKeyName(7, "Server2_blue_16.png");
            this.imlWarnings.Images.SetKeyName(8, "CriticalError_16.png");
            this.imlWarnings.Images.SetKeyName(9, "Blueprint_red_32.png");
            // 
            // DrawingContextMenu
            // 
            this.DrawingContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.DrawingContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyNamesToolStripMenuItem,
            this.createManagementPackToolStripMenuItem,
            this.openToolStripMenuItem,
            this.ViewDependencyMapToolStripMenuItem});
            this.DrawingContextMenu.Name = "DrawingContextMenu";
            this.DrawingContextMenu.Size = new System.Drawing.Size(200, 108);
            this.DrawingContextMenu.Text = "Drawing";
            // 
            // CopyNamesToolStripMenuItem
            // 
            this.CopyNamesToolStripMenuItem.Enabled = false;
            this.CopyNamesToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.papers_32;
            this.CopyNamesToolStripMenuItem.Name = "CopyNamesToolStripMenuItem";
            this.CopyNamesToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.CopyNamesToolStripMenuItem.Text = "Copy Names";
            this.CopyNamesToolStripMenuItem.Click += new System.EventHandler(this.CopyNamesToolStripMenuItem_Click);
            // 
            // createManagementPackToolStripMenuItem
            // 
            this.createManagementPackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.createStubToolStripMenuItem});
            this.createManagementPackToolStripMenuItem.Enabled = false;
            this.createManagementPackToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_32;
            this.createManagementPackToolStripMenuItem.Name = "createManagementPackToolStripMenuItem";
            this.createManagementPackToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.createManagementPackToolStripMenuItem.Text = "Management Pack";
            this.createManagementPackToolStripMenuItem.Click += new System.EventHandler(this.createManagementPackToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productionToolStripMenuItem,
            this.testToolStripMenuItem,
            this.developmentToolStripMenuItem});
            this.createToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_full_32;
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // productionToolStripMenuItem
            // 
            this.productionToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_american_32;
            this.productionToolStripMenuItem.Name = "productionToolStripMenuItem";
            this.productionToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.productionToolStripMenuItem.Text = "Production";
            this.productionToolStripMenuItem.Click += new System.EventHandler(this.productionToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_programmer_32;
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // developmentToolStripMenuItem
            // 
            this.developmentToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_machine_operator_32;
            this.developmentToolStripMenuItem.Name = "developmentToolStripMenuItem";
            this.developmentToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.developmentToolStripMenuItem.Text = "Development";
            this.developmentToolStripMenuItem.Click += new System.EventHandler(this.developmentToolStripMenuItem_Click);
            // 
            // createStubToolStripMenuItem
            // 
            this.createStubToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productionToolStripMenuItem1,
            this.testToolStripMenuItem1,
            this.developmentToolStripMenuItem1});
            this.createStubToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_empty_32;
            this.createStubToolStripMenuItem.Name = "createStubToolStripMenuItem";
            this.createStubToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.createStubToolStripMenuItem.Text = "Create Stub";
            this.createStubToolStripMenuItem.Click += new System.EventHandler(this.createStubToolStripMenuItem_Click);
            // 
            // productionToolStripMenuItem1
            // 
            this.productionToolStripMenuItem1.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_american_32;
            this.productionToolStripMenuItem1.Name = "productionToolStripMenuItem1";
            this.productionToolStripMenuItem1.Size = new System.Drawing.Size(156, 26);
            this.productionToolStripMenuItem1.Text = "Production";
            this.productionToolStripMenuItem1.Click += new System.EventHandler(this.productionToolStripMenuItem1_Click);
            // 
            // testToolStripMenuItem1
            // 
            this.testToolStripMenuItem1.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_programmer_32;
            this.testToolStripMenuItem1.Name = "testToolStripMenuItem1";
            this.testToolStripMenuItem1.Size = new System.Drawing.Size(156, 26);
            this.testToolStripMenuItem1.Text = "Test";
            this.testToolStripMenuItem1.Click += new System.EventHandler(this.testToolStripMenuItem1_Click);
            // 
            // developmentToolStripMenuItem1
            // 
            this.developmentToolStripMenuItem1.Image = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_machine_operator_32;
            this.developmentToolStripMenuItem1.Name = "developmentToolStripMenuItem1";
            this.developmentToolStripMenuItem1.Size = new System.Drawing.Size(156, 26);
            this.developmentToolStripMenuItem1.Text = "Development";
            this.developmentToolStripMenuItem1.Click += new System.EventHandler(this.developmentToolStripMenuItem1_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Enabled = false;
            this.openToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Blueprint_blue_32;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // ViewDependencyMapToolStripMenuItem
            // 
            this.ViewDependencyMapToolStripMenuItem.Enabled = false;
            this.ViewDependencyMapToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Globe_32;
            this.ViewDependencyMapToolStripMenuItem.Name = "ViewDependencyMapToolStripMenuItem";
            this.ViewDependencyMapToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.ViewDependencyMapToolStripMenuItem.Text = "View Dependency Map";
            this.ViewDependencyMapToolStripMenuItem.Click += new System.EventHandler(this.ViewDependencyMapToolStripMenuItem_Click);
            // 
            // lvwOutOfScope
            // 
            this.lvwOutOfScope.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwOutOfScope.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwOutOfScope.Location = new System.Drawing.Point(6, 6);
            this.lvwOutOfScope.Name = "lvwOutOfScope";
            this.lvwOutOfScope.Size = new System.Drawing.Size(340, 156);
            this.lvwOutOfScope.SmallImageList = this.imlServices;
            this.lvwOutOfScope.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwOutOfScope.TabIndex = 0;
            this.lvwOutOfScope.UseCompatibleStateImageBehavior = false;
            this.lvwOutOfScope.View = System.Windows.Forms.View.Details;
            this.lvwOutOfScope.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwOutOfScope_MouseClick);
            this.lvwOutOfScope.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwOutOfScope_MouseDoubleClick);
            this.lvwOutOfScope.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwOutOfScope_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Business Application";
            this.columnHeader1.Width = 326;
            // 
            // tabInformation
            // 
            this.tabInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabInformation.Controls.Add(this.tabPage1);
            this.tabInformation.Controls.Add(this.tabPage2);
            this.tabInformation.Location = new System.Drawing.Point(12, 12);
            this.tabInformation.Name = "tabInformation";
            this.tabInformation.SelectedIndex = 0;
            this.tabInformation.Size = new System.Drawing.Size(360, 223);
            this.tabInformation.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCopyMessages);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tvwApplications);
            this.tabPage1.Controls.Add(this.tvwMessages);
            this.tabPage1.Controls.Add(this.radTarget);
            this.tabPage1.Controls.Add(this.radMessage);
            this.tabPage1.Controls.Add(this.btnCopyValidationErrors);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(352, 197);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Messages";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnCopyMessages
            // 
            this.btnCopyMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyMessages.Location = new System.Drawing.Point(271, 168);
            this.btnCopyMessages.Name = "btnCopyMessages";
            this.btnCopyMessages.Size = new System.Drawing.Size(75, 23);
            this.btnCopyMessages.TabIndex = 7;
            this.btnCopyMessages.Text = "Copy";
            this.btnCopyMessages.UseVisualStyleBackColor = true;
            this.btnCopyMessages.Visible = false;
            this.btnCopyMessages.Click += new System.EventHandler(this.btnCopyMessages_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Show by:";
            // 
            // tvwApplications
            // 
            this.tvwApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwApplications.ImageIndex = 0;
            this.tvwApplications.ImageList = this.imlWarnings;
            this.tvwApplications.Location = new System.Drawing.Point(7, 32);
            this.tvwApplications.Name = "tvwApplications";
            this.tvwApplications.SelectedImageIndex = 0;
            this.tvwApplications.Size = new System.Drawing.Size(338, 130);
            this.tvwApplications.StateImageList = this.imlWarnings;
            this.tvwApplications.TabIndex = 5;
            this.tvwApplications.Visible = false;
            // 
            // tvwMessages
            // 
            this.tvwMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwMessages.ImageIndex = 0;
            this.tvwMessages.ImageList = this.imlWarnings;
            this.tvwMessages.Location = new System.Drawing.Point(7, 32);
            this.tvwMessages.Name = "tvwMessages";
            this.tvwMessages.SelectedImageIndex = 0;
            this.tvwMessages.Size = new System.Drawing.Size(338, 130);
            this.tvwMessages.StateImageList = this.imlWarnings;
            this.tvwMessages.TabIndex = 4;
            // 
            // radTarget
            // 
            this.radTarget.AutoSize = true;
            this.radTarget.Location = new System.Drawing.Point(140, 8);
            this.radTarget.Name = "radTarget";
            this.radTarget.Size = new System.Drawing.Size(56, 17);
            this.radTarget.TabIndex = 3;
            this.radTarget.Text = "Target";
            this.radTarget.UseVisualStyleBackColor = true;
            this.radTarget.CheckedChanged += new System.EventHandler(this.radBusinessApplication_CheckedChanged);
            // 
            // radMessage
            // 
            this.radMessage.AutoSize = true;
            this.radMessage.Checked = true;
            this.radMessage.Location = new System.Drawing.Point(66, 8);
            this.radMessage.Name = "radMessage";
            this.radMessage.Size = new System.Drawing.Size(68, 17);
            this.radMessage.TabIndex = 2;
            this.radMessage.TabStop = true;
            this.radMessage.Text = "Message";
            this.radMessage.UseVisualStyleBackColor = true;
            this.radMessage.CheckedChanged += new System.EventHandler(this.radMessage_CheckedChanged);
            // 
            // btnCopyValidationErrors
            // 
            this.btnCopyValidationErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyValidationErrors.Location = new System.Drawing.Point(271, 168);
            this.btnCopyValidationErrors.Name = "btnCopyValidationErrors";
            this.btnCopyValidationErrors.Size = new System.Drawing.Size(75, 23);
            this.btnCopyValidationErrors.TabIndex = 1;
            this.btnCopyValidationErrors.Text = "Copy";
            this.btnCopyValidationErrors.UseVisualStyleBackColor = true;
            this.btnCopyValidationErrors.Visible = false;
            this.btnCopyValidationErrors.Click += new System.EventHandler(this.btnCopyValidationErrors_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCopyOutOfScopeServices);
            this.tabPage2.Controls.Add(this.lvwOutOfScope);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(352, 197);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Out Of Scope Business Applications";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCopyOutOfScopeServices
            // 
            this.btnCopyOutOfScopeServices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyOutOfScopeServices.Location = new System.Drawing.Point(271, 168);
            this.btnCopyOutOfScopeServices.Name = "btnCopyOutOfScopeServices";
            this.btnCopyOutOfScopeServices.Size = new System.Drawing.Size(75, 23);
            this.btnCopyOutOfScopeServices.TabIndex = 1;
            this.btnCopyOutOfScopeServices.Text = "Copy";
            this.btnCopyOutOfScopeServices.UseVisualStyleBackColor = true;
            this.btnCopyOutOfScopeServices.Click += new System.EventHandler(this.btnCopyOutOfScopeServices_Click);
            // 
            // lblDRContext
            // 
            this.lblDRContext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDRContext.ForeColor = System.Drawing.Color.Red;
            this.lblDRContext.Location = new System.Drawing.Point(12, 238);
            this.lblDRContext.Name = "lblDRContext";
            this.lblDRContext.Size = new System.Drawing.Size(360, 15);
            this.lblDRContext.TabIndex = 1;
            this.lblDRContext.Text = "All messages are raised primarily in the context of Disaster Recovery";
            // 
            // frmInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 262);
            this.Controls.Add(this.lblDRContext);
            this.Controls.Add(this.tabInformation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(399, 298);
            this.Name = "frmInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Information";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInformation_FormClosing);
            this.Load += new System.EventHandler(this.frmInformation_Load);
            this.Shown += new System.EventHandler(this.frmInformation_Shown);
            this.ResizeEnd += new System.EventHandler(this.frmInformation_ResizeEnd);
            this.Move += new System.EventHandler(this.frmInformation_Move);
            this.Resize += new System.EventHandler(this.frmInformation_Resize);
            this.DrawingContextMenu.ResumeLayout(false);
            this.tabInformation.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imlServices;
        private System.Windows.Forms.ImageList imlWarnings;
        private System.Windows.Forms.ContextMenuStrip DrawingContextMenu;
        private System.Windows.Forms.ToolStripMenuItem createManagementPackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewDependencyMapToolStripMenuItem;
        private System.Windows.Forms.ListView lvwOutOfScope;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabControl tabInformation;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblDRContext;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createStubToolStripMenuItem;
        private System.Windows.Forms.Button btnCopyValidationErrors;
        private System.Windows.Forms.Button btnCopyOutOfScopeServices;
        private System.Windows.Forms.ToolStripMenuItem CopyNamesToolStripMenuItem;
        private System.Windows.Forms.RadioButton radTarget;
        private System.Windows.Forms.RadioButton radMessage;
        private System.Windows.Forms.TreeView tvwApplications;
        private System.Windows.Forms.TreeView tvwMessages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem productionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem developmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem developmentToolStripMenuItem1;
        private System.Windows.Forms.Button btnCopyMessages;
    }
}