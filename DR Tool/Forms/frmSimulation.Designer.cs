namespace DRPlanningTool
{
    partial class frmSimulation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSimulation));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lvwOutage = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsSimulation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imlMain = new System.Windows.Forms.ImageList(this.components);
            this.lvwDirectlyAffected = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblAffected = new System.Windows.Forms.Label();
            this.tabMain.SuspendLayout();
            this.cmsSimulation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Controls.Add(this.tabPage3);
            this.tabMain.Location = new System.Drawing.Point(16, 14);
            this.tabMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(364, 28);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(356, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Server";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(356, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Business Application";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(356, 0);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Site";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lvwOutage
            // 
            this.lvwOutage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvwOutage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwOutage.ContextMenuStrip = this.cmsSimulation;
            this.lvwOutage.Location = new System.Drawing.Point(16, 53);
            this.lvwOutage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvwOutage.Name = "lvwOutage";
            this.lvwOutage.Size = new System.Drawing.Size(365, 377);
            this.lvwOutage.SmallImageList = this.imlMain;
            this.lvwOutage.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwOutage.TabIndex = 2;
            this.lvwOutage.UseCompatibleStateImageBehavior = false;
            this.lvwOutage.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 155;
            // 
            // cmsSimulation
            // 
            this.cmsSimulation.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsSimulation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableToolStripMenuItem,
            this.enableToolStripMenuItem});
            this.cmsSimulation.Name = "cmsSimulation";
            this.cmsSimulation.Size = new System.Drawing.Size(139, 64);
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Close_32;
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(138, 30);
            this.disableToolStripMenuItem.Text = "Disable";
            this.disableToolStripMenuItem.Click += new System.EventHandler(this.disableToolStripMenuItem_Click);
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Ok_16;
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(138, 30);
            this.enableToolStripMenuItem.Text = "Enable";
            this.enableToolStripMenuItem.Click += new System.EventHandler(this.enableToolStripMenuItem_Click);
            // 
            // imlMain
            // 
            this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
            this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imlMain.Images.SetKeyName(0, "gear_green_32.png");
            this.imlMain.Images.SetKeyName(1, "gear_orange_32.png");
            this.imlMain.Images.SetKeyName(2, "gear_red_32.png");
            this.imlMain.Images.SetKeyName(3, "Server2_green_32.png");
            this.imlMain.Images.SetKeyName(4, "Server2_orange_32.png");
            this.imlMain.Images.SetKeyName(5, "Server2_red_32.png");
            this.imlMain.Images.SetKeyName(6, "Site_Green_32.png");
            this.imlMain.Images.SetKeyName(7, "Site_Red_32.png");
            // 
            // lvwDirectlyAffected
            // 
            this.lvwDirectlyAffected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvwDirectlyAffected.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvwDirectlyAffected.Location = new System.Drawing.Point(391, 53);
            this.lvwDirectlyAffected.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvwDirectlyAffected.Name = "lvwDirectlyAffected";
            this.lvwDirectlyAffected.Size = new System.Drawing.Size(365, 377);
            this.lvwDirectlyAffected.SmallImageList = this.imlMain;
            this.lvwDirectlyAffected.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwDirectlyAffected.TabIndex = 3;
            this.lvwDirectlyAffected.UseCompatibleStateImageBehavior = false;
            this.lvwDirectlyAffected.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 155;
            // 
            // lblAffected
            // 
            this.lblAffected.Location = new System.Drawing.Point(396, 14);
            this.lblAffected.Name = "lblAffected";
            this.lblAffected.Size = new System.Drawing.Size(359, 26);
            this.lblAffected.TabIndex = 1;
            this.lblAffected.Text = "Affected";
            this.lblAffected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 446);
            this.Controls.Add(this.lblAffected);
            this.Controls.Add(this.lvwDirectlyAffected);
            this.Controls.Add(this.lvwOutage);
            this.Controls.Add(this.tabMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(787, 2447);
            this.MinimumSize = new System.Drawing.Size(787, 477);
            this.Name = "frmSimulation";
            this.Text = "Simulation";
            this.tabMain.ResumeLayout(false);
            this.cmsSimulation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView lvwOutage;
        private System.Windows.Forms.ImageList imlMain;
        private System.Windows.Forms.ListView lvwDirectlyAffected;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip cmsSimulation;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
        private System.Windows.Forms.Label lblAffected;
    }
}