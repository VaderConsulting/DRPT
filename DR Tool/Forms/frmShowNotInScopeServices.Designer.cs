namespace DRPlanningTool
{
    partial class frmShowNotInScopeServices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowNotInScopeServices));
            this.btnOK = new System.Windows.Forms.Button();
            this.lvwServices = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlSmall = new System.Windows.Forms.ImageList(this.components);
            this.lblInfo = new System.Windows.Forms.Label();
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManagementPackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewDependencyMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateStubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(197, 491);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lvwServices
            // 
            this.lvwServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwServices.CheckBoxes = true;
            this.lvwServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwServices.Location = new System.Drawing.Point(12, 48);
            this.lvwServices.Name = "lvwServices";
            this.lvwServices.Size = new System.Drawing.Size(260, 437);
            this.lvwServices.SmallImageList = this.imlSmall;
            this.lvwServices.TabIndex = 1;
            this.lvwServices.UseCompatibleStateImageBehavior = false;
            this.lvwServices.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 237;
            // 
            // imlSmall
            // 
            this.imlSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSmall.ImageStream")));
            this.imlSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imlSmall.Images.SetKeyName(0, "server_32_lime.png");
            this.imlSmall.Images.SetKeyName(1, "server_32_orange.png");
            this.imlSmall.Images.SetKeyName(2, "server_32_red.png");
            this.imlSmall.Images.SetKeyName(3, "server_32_blue.png");
            this.imlSmall.Images.SetKeyName(4, "software_32_lime.png");
            this.imlSmall.Images.SetKeyName(5, "software_32_orange.png");
            this.imlSmall.Images.SetKeyName(6, "software_32_red.png");
            this.imlSmall.Images.SetKeyName(7, "software_32_blue.png");
            this.imlSmall.Images.SetKeyName(8, "software_32_lime_lime.png");
            this.imlSmall.Images.SetKeyName(9, "software_32_lime_orange.png");
            this.imlSmall.Images.SetKeyName(10, "software_32_lime_red.png");
            this.imlSmall.Images.SetKeyName(11, "software_32_lime_grey.png");
            this.imlSmall.Images.SetKeyName(12, "software_32_orange_lime.png");
            this.imlSmall.Images.SetKeyName(13, "software_32_orange_orange.png");
            this.imlSmall.Images.SetKeyName(14, "software_32_orange_red.png");
            this.imlSmall.Images.SetKeyName(15, "software_32_orange_grey.png");
            this.imlSmall.Images.SetKeyName(16, "software_32_red_lime.png");
            this.imlSmall.Images.SetKeyName(17, "software_32_red_orange.png");
            this.imlSmall.Images.SetKeyName(18, "software_32_red_red.png");
            this.imlSmall.Images.SetKeyName(19, "software_32_red_grey.png");
            this.imlSmall.Images.SetKeyName(20, "software_32_grey_grey.png");
            this.imlSmall.Images.SetKeyName(21, "drawing_32_hollow.png");
            this.imlSmall.Images.SetKeyName(22, "server_32_grey.png");
            this.imlSmall.Images.SetKeyName(23, "server_32_limegrey.png");
            this.imlSmall.Images.SetKeyName(24, "server_32_orangegrey.png");
            this.imlSmall.Images.SetKeyName(25, "server_32_redgrey.png");
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.Location = new System.Drawing.Point(13, 13);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(259, 32);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Select one or more \"Out Of Scope\" Services you wish to include in Runbook calcula" +
    "tions";
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyNamesToolStripMenuItem,
            this.ManagementPackToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.ViewDependencyMapToolStripMenuItem});
            this.RightClickMenu.Name = "RightClickMenu";
            this.RightClickMenu.Size = new System.Drawing.Size(196, 92);
            // 
            // CopyNamesToolStripMenuItem
            // 
            this.CopyNamesToolStripMenuItem.Enabled = false;
            this.CopyNamesToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.papers_32;
            this.CopyNamesToolStripMenuItem.Name = "CopyNamesToolStripMenuItem";
            this.CopyNamesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.CopyNamesToolStripMenuItem.Text = "Copy Names";
            // 
            // ManagementPackToolStripMenuItem
            // 
            this.ManagementPackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateToolStripMenuItem,
            this.CreateStubToolStripMenuItem});
            this.ManagementPackToolStripMenuItem.Enabled = false;
            this.ManagementPackToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_32;
            this.ManagementPackToolStripMenuItem.Name = "ManagementPackToolStripMenuItem";
            this.ManagementPackToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.ManagementPackToolStripMenuItem.Text = "Management Pack";
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Enabled = false;
            this.OpenToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.gear_blue_32;
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.OpenToolStripMenuItem.Text = "Open";
            // 
            // ViewDependencyMapToolStripMenuItem
            // 
            this.ViewDependencyMapToolStripMenuItem.Enabled = false;
            this.ViewDependencyMapToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Globe_32;
            this.ViewDependencyMapToolStripMenuItem.Name = "ViewDependencyMapToolStripMenuItem";
            this.ViewDependencyMapToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.ViewDependencyMapToolStripMenuItem.Text = "View Dependency Map";
            // 
            // CreateToolStripMenuItem
            // 
            this.CreateToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_full_32;
            this.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem";
            this.CreateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CreateToolStripMenuItem.Text = "Create";
            // 
            // CreateStubToolStripMenuItem
            // 
            this.CreateStubToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_empty_32;
            this.CreateStubToolStripMenuItem.Name = "CreateStubToolStripMenuItem";
            this.CreateStubToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CreateStubToolStripMenuItem.Text = "Create Stub";
            // 
            // frmShowNotInScopeServices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 526);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lvwServices);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmShowNotInScopeServices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Services Not In Scope";
            this.Load += new System.EventHandler(this.frmShowNotInScopeServices_Load);
            this.RightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListView lvwServices;
        private System.Windows.Forms.ImageList imlSmall;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ContextMenuStrip RightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem CopyNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ManagementPackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewDependencyMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateStubToolStripMenuItem;
    }
}