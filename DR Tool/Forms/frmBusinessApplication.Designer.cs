namespace DRPlanningTool
{
    partial class frmBusinessApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBusinessApplication));
            this.txtDesiredTier = new System.Windows.Forms.TextBox();
            this.txtStream = new System.Windows.Forms.TextBox();
            this.lblDesiredTier = new System.Windows.Forms.Label();
            this.lblStream = new System.Windows.Forms.Label();
            this.grpState = new System.Windows.Forms.GroupBox();
            this.picWarnings = new System.Windows.Forms.PictureBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.radioButtonEmulated = new System.Windows.Forms.RadioButton();
            this.radioButtonActual = new System.Windows.Forms.RadioButton();
            this.grpEmulated = new System.Windows.Forms.GroupBox();
            this.radioButtonOK = new System.Windows.Forms.RadioButton();
            this.radioButtonDegraded = new System.Windows.Forms.RadioButton();
            this.radioButtonError = new System.Windows.Forms.RadioButton();
            this.picService = new System.Windows.Forms.PictureBox();
            this.lblStateDescription = new System.Windows.Forms.Label();
            this.lvwRunbook = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlLarge = new System.Windows.Forms.ImageList(this.components);
            this.imlSmall = new System.Windows.Forms.ImageList(this.components);
            this.lblRunbook = new System.Windows.Forms.Label();
            this.Hints = new System.Windows.Forms.ToolTip(this.components);
            this.txtRecoveryTime = new System.Windows.Forms.TextBox();
            this.tabBusinessApplication = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblHA = new System.Windows.Forms.Label();
            this.chkHA = new System.Windows.Forms.CheckBox();
            this.txtDrawingNotes = new System.Windows.Forms.TextBox();
            this.lblDrawingNotes = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblComponentServers = new System.Windows.Forms.Label();
            this.lblComponentServices = new System.Windows.Forms.Label();
            this.lvwBusinessApplicationComponentServices = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwBusinessApplicationComponentServers = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.lvwDependentBusinessApplications = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwAffectedServers = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.lblMPVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCreateMP = new System.Windows.Forms.Button();
            this.picImageState = new System.Windows.Forms.PictureBox();
            this.txtLastUpdate = new System.Windows.Forms.TextBox();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();
            this.btnApplyDrawing = new System.Windows.Forms.Button();
            this.btnSaveDrawing = new System.Windows.Forms.Button();
            this.cmbDrawingStatus = new System.Windows.Forms.ComboBox();
            this.lblDrawingStatus = new System.Windows.Forms.Label();
            this.txtDrawingID = new System.Windows.Forms.TextBox();
            this.lblDrawingID = new System.Windows.Forms.Label();
            this.txtDrawingDescription = new System.Windows.Forms.TextBox();
            this.lblDrawingDescription = new System.Windows.Forms.Label();
            this.txtDrawingVersion = new System.Windows.Forms.TextBox();
            this.lblDrawingVersion = new System.Windows.Forms.Label();
            this.picDrawingPreview = new System.Windows.Forms.PictureBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtRunbookOrder = new System.Windows.Forms.TextBox();
            this.lblRunbookOrder = new System.Windows.Forms.Label();
            this.lblNotImplemented = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUseBusinessApplicationName = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.chkSubService = new System.Windows.Forms.CheckBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.chkShowInCatalogue = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtComment1 = new System.Windows.Forms.TextBox();
            this.lblComment1 = new System.Windows.Forms.Label();
            this.lblInScopeOverride = new System.Windows.Forms.Label();
            this.chkInScope = new System.Windows.Forms.CheckBox();
            this.lblInScope = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblRecoveryTime = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblRecoveryTasks = new System.Windows.Forms.Label();
            this.btnOpenRecoveryTasks = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.lstWarnings = new System.Windows.Forms.ListBox();
            this.imlServerStateSmall = new System.Windows.Forms.ImageList(this.components);
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.DrawingContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createManagementPackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createStubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewDependencyMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtWorkInstructionName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtActualTier = new System.Windows.Forms.TextBox();
            this.lblActualTier = new System.Windows.Forms.Label();
            this.grpState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWarnings)).BeginInit();
            this.grpEmulated.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picService)).BeginInit();
            this.tabBusinessApplication.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImageState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDrawingPreview)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.DrawingContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDesiredTier
            // 
            this.txtDesiredTier.Location = new System.Drawing.Point(532, 32);
            this.txtDesiredTier.Name = "txtDesiredTier";
            this.txtDesiredTier.ReadOnly = true;
            this.txtDesiredTier.Size = new System.Drawing.Size(59, 20);
            this.txtDesiredTier.TabIndex = 6;
            this.txtDesiredTier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtStream
            // 
            this.txtStream.Location = new System.Drawing.Point(532, 6);
            this.txtStream.Name = "txtStream";
            this.txtStream.ReadOnly = true;
            this.txtStream.Size = new System.Drawing.Size(59, 20);
            this.txtStream.TabIndex = 4;
            this.txtStream.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDesiredTier
            // 
            this.lblDesiredTier.AutoSize = true;
            this.lblDesiredTier.Location = new System.Drawing.Point(462, 35);
            this.lblDesiredTier.Name = "lblDesiredTier";
            this.lblDesiredTier.Size = new System.Drawing.Size(64, 13);
            this.lblDesiredTier.TabIndex = 5;
            this.lblDesiredTier.Text = "Desired Tier";
            // 
            // lblStream
            // 
            this.lblStream.AutoSize = true;
            this.lblStream.Location = new System.Drawing.Point(486, 9);
            this.lblStream.Name = "lblStream";
            this.lblStream.Size = new System.Drawing.Size(40, 13);
            this.lblStream.TabIndex = 3;
            this.lblStream.Text = "Stream";
            // 
            // grpState
            // 
            this.grpState.Controls.Add(this.picWarnings);
            this.grpState.Controls.Add(this.btnLoad);
            this.grpState.Controls.Add(this.radioButtonEmulated);
            this.grpState.Controls.Add(this.radioButtonActual);
            this.grpState.Controls.Add(this.grpEmulated);
            this.grpState.Controls.Add(this.picService);
            this.grpState.Controls.Add(this.lblStateDescription);
            this.grpState.Location = new System.Drawing.Point(6, 6);
            this.grpState.Name = "grpState";
            this.grpState.Size = new System.Drawing.Size(234, 203);
            this.grpState.TabIndex = 0;
            this.grpState.TabStop = false;
            this.grpState.Text = "State";
            // 
            // picWarnings
            // 
            this.picWarnings.BackgroundImage = global::DRPlanningTool.Properties.Resources.Warning;
            this.picWarnings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picWarnings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picWarnings.Location = new System.Drawing.Point(6, 149);
            this.picWarnings.Name = "picWarnings";
            this.picWarnings.Size = new System.Drawing.Size(96, 48);
            this.picWarnings.TabIndex = 14;
            this.picWarnings.TabStop = false;
            this.picWarnings.Visible = false;
            this.picWarnings.VisibleChanged += new System.EventHandler(this.picWarnings_VisibleChanged);
            this.picWarnings.Click += new System.EventHandler(this.picWarnings_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(153, 174);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Visible = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // radioButtonEmulated
            // 
            this.radioButtonEmulated.AutoSize = true;
            this.radioButtonEmulated.Location = new System.Drawing.Point(67, 89);
            this.radioButtonEmulated.Name = "radioButtonEmulated";
            this.radioButtonEmulated.Size = new System.Drawing.Size(69, 17);
            this.radioButtonEmulated.TabIndex = 2;
            this.radioButtonEmulated.TabStop = true;
            this.radioButtonEmulated.Text = "Emulated";
            this.radioButtonEmulated.UseVisualStyleBackColor = true;
            this.radioButtonEmulated.Visible = false;
            this.radioButtonEmulated.CheckedChanged += new System.EventHandler(this.radioButtonEmulated_CheckedChanged);
            // 
            // radioButtonActual
            // 
            this.radioButtonActual.AutoSize = true;
            this.radioButtonActual.Checked = true;
            this.radioButtonActual.Location = new System.Drawing.Point(6, 89);
            this.radioButtonActual.Name = "radioButtonActual";
            this.radioButtonActual.Size = new System.Drawing.Size(60, 17);
            this.radioButtonActual.TabIndex = 1;
            this.radioButtonActual.TabStop = true;
            this.radioButtonActual.Text = "Original";
            this.radioButtonActual.UseVisualStyleBackColor = true;
            this.radioButtonActual.Visible = false;
            this.radioButtonActual.CheckedChanged += new System.EventHandler(this.radioButtonActual_CheckedChanged);
            // 
            // grpEmulated
            // 
            this.grpEmulated.Controls.Add(this.radioButtonOK);
            this.grpEmulated.Controls.Add(this.radioButtonDegraded);
            this.grpEmulated.Controls.Add(this.radioButtonError);
            this.grpEmulated.Enabled = false;
            this.grpEmulated.Location = new System.Drawing.Point(72, 89);
            this.grpEmulated.Name = "grpEmulated";
            this.grpEmulated.Size = new System.Drawing.Size(155, 41);
            this.grpEmulated.TabIndex = 3;
            this.grpEmulated.TabStop = false;
            this.grpEmulated.Visible = false;
            // 
            // radioButtonOK
            // 
            this.radioButtonOK.AutoSize = true;
            this.radioButtonOK.Location = new System.Drawing.Point(6, 19);
            this.radioButtonOK.Name = "radioButtonOK";
            this.radioButtonOK.Size = new System.Drawing.Size(40, 17);
            this.radioButtonOK.TabIndex = 2;
            this.radioButtonOK.TabStop = true;
            this.radioButtonOK.Text = "OK";
            this.radioButtonOK.UseVisualStyleBackColor = true;
            this.radioButtonOK.Visible = false;
            this.radioButtonOK.CheckedChanged += new System.EventHandler(this.radioButtonOK_CheckedChanged);
            // 
            // radioButtonDegraded
            // 
            this.radioButtonDegraded.AutoSize = true;
            this.radioButtonDegraded.Location = new System.Drawing.Point(49, 18);
            this.radioButtonDegraded.Name = "radioButtonDegraded";
            this.radioButtonDegraded.Size = new System.Drawing.Size(51, 17);
            this.radioButtonDegraded.TabIndex = 0;
            this.radioButtonDegraded.TabStop = true;
            this.radioButtonDegraded.Text = "Warn";
            this.radioButtonDegraded.UseVisualStyleBackColor = true;
            this.radioButtonDegraded.Visible = false;
            this.radioButtonDegraded.CheckedChanged += new System.EventHandler(this.radioButtonDegraded_CheckedChanged);
            // 
            // radioButtonError
            // 
            this.radioButtonError.AutoSize = true;
            this.radioButtonError.Location = new System.Drawing.Point(102, 18);
            this.radioButtonError.Name = "radioButtonError";
            this.radioButtonError.Size = new System.Drawing.Size(47, 17);
            this.radioButtonError.TabIndex = 1;
            this.radioButtonError.TabStop = true;
            this.radioButtonError.Text = "Error";
            this.radioButtonError.UseVisualStyleBackColor = true;
            this.radioButtonError.Visible = false;
            this.radioButtonError.CheckedChanged += new System.EventHandler(this.radioButtonError_CheckedChanged);
            // 
            // picService
            // 
            this.picService.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picService.Location = new System.Drawing.Point(6, 19);
            this.picService.Name = "picService";
            this.picService.Size = new System.Drawing.Size(64, 64);
            this.picService.TabIndex = 13;
            this.picService.TabStop = false;
            // 
            // lblStateDescription
            // 
            this.lblStateDescription.Location = new System.Drawing.Point(76, 19);
            this.lblStateDescription.Name = "lblStateDescription";
            this.lblStateDescription.Size = new System.Drawing.Size(152, 67);
            this.lblStateDescription.TabIndex = 0;
            this.lblStateDescription.Text = "Focused Bus. Service not set.  Update Drawing.";
            // 
            // lvwRunbook
            // 
            this.lvwRunbook.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.lvwRunbook.FullRowSelect = true;
            this.lvwRunbook.GridLines = true;
            this.lvwRunbook.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwRunbook.HideSelection = false;
            this.lvwRunbook.LargeImageList = this.imlLarge;
            this.lvwRunbook.Location = new System.Drawing.Point(246, 28);
            this.lvwRunbook.MultiSelect = false;
            this.lvwRunbook.Name = "lvwRunbook";
            this.lvwRunbook.Size = new System.Drawing.Size(210, 55);
            this.lvwRunbook.SmallImageList = this.imlSmall;
            this.lvwRunbook.TabIndex = 2;
            this.lvwRunbook.UseCompatibleStateImageBehavior = false;
            this.lvwRunbook.View = System.Windows.Forms.View.Details;
            this.lvwRunbook.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwRunbook_MouseClick);
            this.lvwRunbook.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwRunbook_MouseDoubleClick);
            this.lvwRunbook.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwRunbook_MouseDown);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            this.columnHeader4.Width = 185;
            // 
            // imlLarge
            // 
            this.imlLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlLarge.ImageStream")));
            this.imlLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imlLarge.Images.SetKeyName(0, "gear_blue_256.png");
            this.imlLarge.Images.SetKeyName(1, "gear_grey_256.png");
            this.imlLarge.Images.SetKeyName(2, "gear_red_256.png");
            this.imlLarge.Images.SetKeyName(3, "gear_orange_256.png");
            this.imlLarge.Images.SetKeyName(4, "gear_green_256.png");
            this.imlLarge.Images.SetKeyName(5, "Server2_blue_256.png");
            this.imlLarge.Images.SetKeyName(6, "Server2_grey_256.png");
            this.imlLarge.Images.SetKeyName(7, "Server2_red_256.png");
            this.imlLarge.Images.SetKeyName(8, "Server2_orange_256.png");
            this.imlLarge.Images.SetKeyName(9, "Server2_green_256.png");
            this.imlLarge.Images.SetKeyName(10, "Blueprint_blue_256.png");
            this.imlLarge.Images.SetKeyName(11, "Blueprint_none_256.png");
            this.imlLarge.Images.SetKeyName(12, "Blueprint_grey_256.png");
            this.imlLarge.Images.SetKeyName(13, "Blueprint_red_256.png");
            this.imlLarge.Images.SetKeyName(14, "Blueprint_orange_256.png");
            this.imlLarge.Images.SetKeyName(15, "Blueprint_green_256.png");
            // 
            // imlSmall
            // 
            this.imlSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSmall.ImageStream")));
            this.imlSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imlSmall.Images.SetKeyName(0, "gear_blue_32.png");
            this.imlSmall.Images.SetKeyName(1, "gear_grey_32.png");
            this.imlSmall.Images.SetKeyName(2, "gear_red_32.png");
            this.imlSmall.Images.SetKeyName(3, "gear_orange_32.png");
            this.imlSmall.Images.SetKeyName(4, "gear_green_32.png");
            this.imlSmall.Images.SetKeyName(5, "Server2_blue_32.png");
            this.imlSmall.Images.SetKeyName(6, "Server2_grey_32.png");
            this.imlSmall.Images.SetKeyName(7, "Server2_red_32.png");
            this.imlSmall.Images.SetKeyName(8, "Server2_orange_32.png");
            this.imlSmall.Images.SetKeyName(9, "Server2_green_32.png");
            this.imlSmall.Images.SetKeyName(10, "Blueprint_blue_32.png");
            this.imlSmall.Images.SetKeyName(11, "gear_blue_32.png");
            this.imlSmall.Images.SetKeyName(12, "Server2_blue_32.png");
            this.imlSmall.Images.SetKeyName(13, "gear_3_colour_32.png");
            this.imlSmall.Images.SetKeyName(14, "Box_32.png");
            this.imlSmall.Images.SetKeyName(15, "i_32_blue.png");
            this.imlSmall.Images.SetKeyName(16, "i_32_red.png");
            this.imlSmall.Images.SetKeyName(17, "component_32.png");
            // 
            // lblRunbook
            // 
            this.lblRunbook.Location = new System.Drawing.Point(246, 6);
            this.lblRunbook.Name = "lblRunbook";
            this.lblRunbook.Size = new System.Drawing.Size(236, 19);
            this.lblRunbook.TabIndex = 1;
            this.lblRunbook.Text = "Runbook Position";
            this.lblRunbook.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Hints
            // 
            this.Hints.IsBalloon = true;
            // 
            // txtRecoveryTime
            // 
            this.txtRecoveryTime.Location = new System.Drawing.Point(112, 60);
            this.txtRecoveryTime.Name = "txtRecoveryTime";
            this.txtRecoveryTime.ReadOnly = true;
            this.txtRecoveryTime.Size = new System.Drawing.Size(156, 20);
            this.txtRecoveryTime.TabIndex = 9;
            this.toolTips.SetToolTip(this.txtRecoveryTime, "This feature is currently not implemented.  Please contact the Author if you wish" +
        " to have it added.");
            // 
            // tabBusinessApplication
            // 
            this.tabBusinessApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabBusinessApplication.Controls.Add(this.tabPage1);
            this.tabBusinessApplication.Controls.Add(this.tabPage2);
            this.tabBusinessApplication.Controls.Add(this.tabPage3);
            this.tabBusinessApplication.Controls.Add(this.tabPage5);
            this.tabBusinessApplication.Controls.Add(this.tabPage6);
            this.tabBusinessApplication.Controls.Add(this.tabPage4);
            this.tabBusinessApplication.Controls.Add(this.tabPage7);
            this.tabBusinessApplication.ImageList = this.imlSmall;
            this.tabBusinessApplication.Location = new System.Drawing.Point(12, 8);
            this.tabBusinessApplication.Name = "tabBusinessApplication";
            this.tabBusinessApplication.SelectedIndex = 0;
            this.tabBusinessApplication.Size = new System.Drawing.Size(610, 237);
            this.tabBusinessApplication.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtActualTier);
            this.tabPage1.Controls.Add(this.lblActualTier);
            this.tabPage1.Controls.Add(this.lblHA);
            this.tabPage1.Controls.Add(this.chkHA);
            this.tabPage1.Controls.Add(this.txtDrawingNotes);
            this.tabPage1.Controls.Add(this.lblDrawingNotes);
            this.tabPage1.Controls.Add(this.grpState);
            this.tabPage1.Controls.Add(this.lvwRunbook);
            this.tabPage1.Controls.Add(this.txtDesiredTier);
            this.tabPage1.Controls.Add(this.txtStream);
            this.tabPage1.Controls.Add(this.lblDesiredTier);
            this.tabPage1.Controls.Add(this.lblRunbook);
            this.tabPage1.Controls.Add(this.lblStream);
            this.tabPage1.ImageIndex = 11;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(602, 210);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblHA
            // 
            this.lblHA.AutoSize = true;
            this.lblHA.Location = new System.Drawing.Point(504, 84);
            this.lblHA.Name = "lblHA";
            this.lblHA.Size = new System.Drawing.Size(22, 13);
            this.lblHA.TabIndex = 7;
            this.lblHA.Text = "HA";
            // 
            // chkHA
            // 
            this.chkHA.AutoSize = true;
            this.chkHA.Enabled = false;
            this.chkHA.Location = new System.Drawing.Point(532, 84);
            this.chkHA.Name = "chkHA";
            this.chkHA.Size = new System.Drawing.Size(15, 14);
            this.chkHA.TabIndex = 8;
            this.chkHA.UseVisualStyleBackColor = true;
            // 
            // txtDrawingNotes
            // 
            this.txtDrawingNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDrawingNotes.Location = new System.Drawing.Point(246, 102);
            this.txtDrawingNotes.Multiline = true;
            this.txtDrawingNotes.Name = "txtDrawingNotes";
            this.txtDrawingNotes.ReadOnly = true;
            this.txtDrawingNotes.Size = new System.Drawing.Size(345, 98);
            this.txtDrawingNotes.TabIndex = 10;
            // 
            // lblDrawingNotes
            // 
            this.lblDrawingNotes.Location = new System.Drawing.Point(246, 86);
            this.lblDrawingNotes.Name = "lblDrawingNotes";
            this.lblDrawingNotes.Size = new System.Drawing.Size(40, 13);
            this.lblDrawingNotes.TabIndex = 9;
            this.lblDrawingNotes.Text = "Notes";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblComponentServers);
            this.tabPage2.Controls.Add(this.lblComponentServices);
            this.tabPage2.Controls.Add(this.lvwBusinessApplicationComponentServices);
            this.tabPage2.Controls.Add(this.lvwBusinessApplicationComponentServers);
            this.tabPage2.ImageIndex = 17;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(602, 210);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Components";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblComponentServers
            // 
            this.lblComponentServers.Location = new System.Drawing.Point(302, 6);
            this.lblComponentServers.Name = "lblComponentServers";
            this.lblComponentServers.Size = new System.Drawing.Size(290, 17);
            this.lblComponentServers.TabIndex = 1;
            this.lblComponentServers.Text = "Component Servers";
            this.lblComponentServers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblComponentServices
            // 
            this.lblComponentServices.Location = new System.Drawing.Point(6, 6);
            this.lblComponentServices.Name = "lblComponentServices";
            this.lblComponentServices.Size = new System.Drawing.Size(290, 17);
            this.lblComponentServices.TabIndex = 0;
            this.lblComponentServices.Text = "Component Business Applications";
            this.lblComponentServices.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvwBusinessApplicationComponentServices
            // 
            this.lvwBusinessApplicationComponentServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwBusinessApplicationComponentServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader13,
            this.columnHeader7,
            this.columnHeader11});
            this.lvwBusinessApplicationComponentServices.FullRowSelect = true;
            this.lvwBusinessApplicationComponentServices.LargeImageList = this.imlLarge;
            this.lvwBusinessApplicationComponentServices.Location = new System.Drawing.Point(6, 26);
            this.lvwBusinessApplicationComponentServices.Name = "lvwBusinessApplicationComponentServices";
            this.lvwBusinessApplicationComponentServices.Size = new System.Drawing.Size(290, 177);
            this.lvwBusinessApplicationComponentServices.SmallImageList = this.imlSmall;
            this.lvwBusinessApplicationComponentServices.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwBusinessApplicationComponentServices.TabIndex = 2;
            this.lvwBusinessApplicationComponentServices.UseCompatibleStateImageBehavior = false;
            this.lvwBusinessApplicationComponentServices.View = System.Windows.Forms.View.Details;
            this.lvwBusinessApplicationComponentServices.SelectedIndexChanged += new System.EventHandler(this.lvwBusinessApplicationComponentServices_SelectedIndexChanged);
            this.lvwBusinessApplicationComponentServices.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwBusinessApplicationComponentServices_MouseClick);
            this.lvwBusinessApplicationComponentServices.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwServiceComponentServices_MouseDoubleClick);
            this.lvwBusinessApplicationComponentServices.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwBusinessApplicationComponentServices_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 155;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Type";
            this.columnHeader13.Width = 37;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Stream";
            this.columnHeader7.Width = 45;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Tier";
            this.columnHeader11.Width = 30;
            // 
            // lvwBusinessApplicationComponentServers
            // 
            this.lvwBusinessApplicationComponentServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwBusinessApplicationComponentServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader14,
            this.columnHeader16,
            this.columnHeader9,
            this.columnHeader3});
            this.lvwBusinessApplicationComponentServers.FullRowSelect = true;
            this.lvwBusinessApplicationComponentServers.LargeImageList = this.imlSmall;
            this.lvwBusinessApplicationComponentServers.Location = new System.Drawing.Point(302, 26);
            this.lvwBusinessApplicationComponentServers.Name = "lvwBusinessApplicationComponentServers";
            this.lvwBusinessApplicationComponentServers.Size = new System.Drawing.Size(290, 177);
            this.lvwBusinessApplicationComponentServers.SmallImageList = this.imlSmall;
            this.lvwBusinessApplicationComponentServers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwBusinessApplicationComponentServers.TabIndex = 3;
            this.lvwBusinessApplicationComponentServers.UseCompatibleStateImageBehavior = false;
            this.lvwBusinessApplicationComponentServers.View = System.Windows.Forms.View.Details;
            this.lvwBusinessApplicationComponentServers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwBusinessApplicationComponentServers_MouseClick);
            this.lvwBusinessApplicationComponentServers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwServiceComponentServers_MouseDoubleClick);
            this.lvwBusinessApplicationComponentServers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwBusinessApplicationComponentServers_MouseDown);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 118;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Type";
            this.columnHeader14.Width = 37;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Stream";
            this.columnHeader16.Width = 45;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Site";
            this.columnHeader9.Width = 40;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "HA";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 27;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.lvwDependentBusinessApplications);
            this.tabPage3.Controls.Add(this.lvwAffectedServers);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.ImageIndex = 13;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(602, 210);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Relationships";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(302, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Related Servers";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvwDependentBusinessApplications
            // 
            this.lvwDependentBusinessApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwDependentBusinessApplications.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader8,
            this.columnHeader12});
            this.lvwDependentBusinessApplications.FullRowSelect = true;
            this.lvwDependentBusinessApplications.LargeImageList = this.imlLarge;
            this.lvwDependentBusinessApplications.Location = new System.Drawing.Point(6, 26);
            this.lvwDependentBusinessApplications.Name = "lvwDependentBusinessApplications";
            this.lvwDependentBusinessApplications.Size = new System.Drawing.Size(290, 177);
            this.lvwDependentBusinessApplications.SmallImageList = this.imlSmall;
            this.lvwDependentBusinessApplications.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwDependentBusinessApplications.TabIndex = 2;
            this.lvwDependentBusinessApplications.UseCompatibleStateImageBehavior = false;
            this.lvwDependentBusinessApplications.View = System.Windows.Forms.View.Details;
            this.lvwDependentBusinessApplications.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwBusinessDependentServices_MouseClick);
            this.lvwDependentBusinessApplications.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwDependentServices_MouseDoubleClick);
            this.lvwDependentBusinessApplications.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwDependentBusinessApplications_MouseDown);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Name";
            this.columnHeader5.Width = 194;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Stream";
            this.columnHeader8.Width = 45;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Tier";
            this.columnHeader12.Width = 30;
            // 
            // lvwAffectedServers
            // 
            this.lvwAffectedServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwAffectedServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader15,
            this.columnHeader10});
            this.lvwAffectedServers.FullRowSelect = true;
            this.lvwAffectedServers.Location = new System.Drawing.Point(302, 26);
            this.lvwAffectedServers.Name = "lvwAffectedServers";
            this.lvwAffectedServers.Size = new System.Drawing.Size(290, 177);
            this.lvwAffectedServers.SmallImageList = this.imlSmall;
            this.lvwAffectedServers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwAffectedServers.TabIndex = 3;
            this.lvwAffectedServers.UseCompatibleStateImageBehavior = false;
            this.lvwAffectedServers.View = System.Windows.Forms.View.Details;
            this.lvwAffectedServers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwAffectedServers_MouseClick);
            this.lvwAffectedServers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwDependentServers_MouseDoubleClick);
            this.lvwAffectedServers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwAffectedServers_MouseDown);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Name";
            this.columnHeader6.Width = 157;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Stream";
            this.columnHeader15.Width = 45;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Site";
            this.columnHeader10.Width = 67;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dependent Business Applications";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lblMPVersion);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.btnCreateMP);
            this.tabPage5.Controls.Add(this.picImageState);
            this.tabPage5.Controls.Add(this.txtLastUpdate);
            this.tabPage5.Controls.Add(this.lblLastUpdate);
            this.tabPage5.Controls.Add(this.lblPreview);
            this.tabPage5.Controls.Add(this.btnApplyDrawing);
            this.tabPage5.Controls.Add(this.btnSaveDrawing);
            this.tabPage5.Controls.Add(this.cmbDrawingStatus);
            this.tabPage5.Controls.Add(this.lblDrawingStatus);
            this.tabPage5.Controls.Add(this.txtDrawingID);
            this.tabPage5.Controls.Add(this.lblDrawingID);
            this.tabPage5.Controls.Add(this.txtDrawingDescription);
            this.tabPage5.Controls.Add(this.lblDrawingDescription);
            this.tabPage5.Controls.Add(this.txtDrawingVersion);
            this.tabPage5.Controls.Add(this.lblDrawingVersion);
            this.tabPage5.Controls.Add(this.picDrawingPreview);
            this.tabPage5.ImageIndex = 10;
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(602, 210);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Drawing";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // lblMPVersion
            // 
            this.lblMPVersion.Location = new System.Drawing.Point(534, 61);
            this.lblMPVersion.Name = "lblMPVersion";
            this.lblMPVersion.Size = new System.Drawing.Size(57, 18);
            this.lblMPVersion.TabIndex = 30;
            this.lblMPVersion.Text = "0.0.0.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(430, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "SCOM MP Version:";
            // 
            // btnCreateMP
            // 
            this.btnCreateMP.Location = new System.Drawing.Point(516, 111);
            this.btnCreateMP.Name = "btnCreateMP";
            this.btnCreateMP.Size = new System.Drawing.Size(75, 23);
            this.btnCreateMP.TabIndex = 12;
            this.btnCreateMP.Text = "Create MP";
            this.btnCreateMP.UseVisualStyleBackColor = true;
            this.btnCreateMP.Click += new System.EventHandler(this.btnCreateMP_Click);
            // 
            // picImageState
            // 
            this.picImageState.Location = new System.Drawing.Point(22, 139);
            this.picImageState.Name = "picImageState";
            this.picImageState.Size = new System.Drawing.Size(64, 64);
            this.picImageState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImageState.TabIndex = 28;
            this.picImageState.TabStop = false;
            // 
            // txtLastUpdate
            // 
            this.txtLastUpdate.Location = new System.Drawing.Point(112, 85);
            this.txtLastUpdate.Name = "txtLastUpdate";
            this.txtLastUpdate.ReadOnly = true;
            this.txtLastUpdate.Size = new System.Drawing.Size(479, 20);
            this.txtLastUpdate.TabIndex = 10;
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.Location = new System.Drawing.Point(6, 88);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(100, 23);
            this.lblLastUpdate.TabIndex = 11;
            this.lblLastUpdate.Text = "Last Update";
            // 
            // lblPreview
            // 
            this.lblPreview.Location = new System.Drawing.Point(6, 113);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(100, 23);
            this.lblPreview.TabIndex = 13;
            this.lblPreview.Text = "Preview";
            // 
            // btnApplyDrawing
            // 
            this.btnApplyDrawing.Enabled = false;
            this.btnApplyDrawing.Location = new System.Drawing.Point(235, 57);
            this.btnApplyDrawing.Name = "btnApplyDrawing";
            this.btnApplyDrawing.Size = new System.Drawing.Size(75, 23);
            this.btnApplyDrawing.TabIndex = 7;
            this.btnApplyDrawing.Text = "Apply";
            this.btnApplyDrawing.UseVisualStyleBackColor = true;
            this.btnApplyDrawing.Click += new System.EventHandler(this.btnApplyDrawing_Click);
            // 
            // btnSaveDrawing
            // 
            this.btnSaveDrawing.Enabled = false;
            this.btnSaveDrawing.Location = new System.Drawing.Point(316, 57);
            this.btnSaveDrawing.Name = "btnSaveDrawing";
            this.btnSaveDrawing.Size = new System.Drawing.Size(75, 23);
            this.btnSaveDrawing.TabIndex = 8;
            this.btnSaveDrawing.Text = "Save";
            this.btnSaveDrawing.UseVisualStyleBackColor = true;
            this.btnSaveDrawing.Click += new System.EventHandler(this.btnSaveDrawing_Click);
            // 
            // cmbDrawingStatus
            // 
            this.cmbDrawingStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrawingStatus.Enabled = false;
            this.cmbDrawingStatus.FormattingEnabled = true;
            this.cmbDrawingStatus.Location = new System.Drawing.Point(112, 58);
            this.cmbDrawingStatus.Name = "cmbDrawingStatus";
            this.cmbDrawingStatus.Size = new System.Drawing.Size(117, 21);
            this.cmbDrawingStatus.TabIndex = 6;
            this.cmbDrawingStatus.SelectedIndexChanged += new System.EventHandler(this.cmbDrawingStatus_SelectedIndexChanged);
            // 
            // lblDrawingStatus
            // 
            this.lblDrawingStatus.Location = new System.Drawing.Point(6, 61);
            this.lblDrawingStatus.Name = "lblDrawingStatus";
            this.lblDrawingStatus.Size = new System.Drawing.Size(100, 23);
            this.lblDrawingStatus.TabIndex = 9;
            this.lblDrawingStatus.Text = "Status";
            // 
            // txtDrawingID
            // 
            this.txtDrawingID.Location = new System.Drawing.Point(228, 6);
            this.txtDrawingID.Name = "txtDrawingID";
            this.txtDrawingID.ReadOnly = true;
            this.txtDrawingID.Size = new System.Drawing.Size(363, 20);
            this.txtDrawingID.TabIndex = 1;
            // 
            // lblDrawingID
            // 
            this.lblDrawingID.Location = new System.Drawing.Point(196, 9);
            this.lblDrawingID.Name = "lblDrawingID";
            this.lblDrawingID.Size = new System.Drawing.Size(26, 17);
            this.lblDrawingID.TabIndex = 3;
            this.lblDrawingID.Text = "ID";
            // 
            // txtDrawingDescription
            // 
            this.txtDrawingDescription.Location = new System.Drawing.Point(112, 32);
            this.txtDrawingDescription.Name = "txtDrawingDescription";
            this.txtDrawingDescription.ReadOnly = true;
            this.txtDrawingDescription.Size = new System.Drawing.Size(479, 20);
            this.txtDrawingDescription.TabIndex = 4;
            // 
            // lblDrawingDescription
            // 
            this.lblDrawingDescription.Location = new System.Drawing.Point(6, 35);
            this.lblDrawingDescription.Name = "lblDrawingDescription";
            this.lblDrawingDescription.Size = new System.Drawing.Size(100, 17);
            this.lblDrawingDescription.TabIndex = 5;
            this.lblDrawingDescription.Text = "Description";
            // 
            // txtDrawingVersion
            // 
            this.txtDrawingVersion.Location = new System.Drawing.Point(112, 6);
            this.txtDrawingVersion.Name = "txtDrawingVersion";
            this.txtDrawingVersion.ReadOnly = true;
            this.txtDrawingVersion.Size = new System.Drawing.Size(43, 20);
            this.txtDrawingVersion.TabIndex = 0;
            // 
            // lblDrawingVersion
            // 
            this.lblDrawingVersion.Location = new System.Drawing.Point(6, 9);
            this.lblDrawingVersion.Name = "lblDrawingVersion";
            this.lblDrawingVersion.Size = new System.Drawing.Size(100, 17);
            this.lblDrawingVersion.TabIndex = 2;
            this.lblDrawingVersion.Text = "Version";
            // 
            // picDrawingPreview
            // 
            this.picDrawingPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDrawingPreview.Cursor = System.Windows.Forms.Cursors.Default;
            this.picDrawingPreview.Location = new System.Drawing.Point(112, 113);
            this.picDrawingPreview.Name = "picDrawingPreview";
            this.picDrawingPreview.Size = new System.Drawing.Size(172, 96);
            this.picDrawingPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDrawingPreview.TabIndex = 27;
            this.picDrawingPreview.TabStop = false;
            this.picDrawingPreview.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picDrawingPreview_MouseDoubleClick);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtRunbookOrder);
            this.tabPage6.Controls.Add(this.lblRunbookOrder);
            this.tabPage6.Controls.Add(this.lblNotImplemented);
            this.tabPage6.Controls.Add(this.label3);
            this.tabPage6.Controls.Add(this.btnUseBusinessApplicationName);
            this.tabPage6.Controls.Add(this.label7);
            this.tabPage6.Controls.Add(this.chkSubService);
            this.tabPage6.Controls.Add(this.lblDisplayName);
            this.tabPage6.Controls.Add(this.label4);
            this.tabPage6.Controls.Add(this.txtDisplayName);
            this.tabPage6.Controls.Add(this.chkShowInCatalogue);
            this.tabPage6.ImageIndex = 14;
            this.tabPage6.Location = new System.Drawing.Point(4, 23);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(602, 210);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Management Pack";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txtRunbookOrder
            // 
            this.txtRunbookOrder.Location = new System.Drawing.Point(106, 58);
            this.txtRunbookOrder.Name = "txtRunbookOrder";
            this.txtRunbookOrder.ReadOnly = true;
            this.txtRunbookOrder.Size = new System.Drawing.Size(43, 20);
            this.txtRunbookOrder.TabIndex = 8;
            // 
            // lblRunbookOrder
            // 
            this.lblRunbookOrder.Location = new System.Drawing.Point(3, 61);
            this.lblRunbookOrder.Name = "lblRunbookOrder";
            this.lblRunbookOrder.Size = new System.Drawing.Size(100, 17);
            this.lblRunbookOrder.TabIndex = 9;
            this.lblRunbookOrder.Text = "Runbook Order";
            // 
            // lblNotImplemented
            // 
            this.lblNotImplemented.BackColor = System.Drawing.Color.Cornsilk;
            this.lblNotImplemented.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNotImplemented.Location = new System.Drawing.Point(6, 194);
            this.lblNotImplemented.Name = "lblNotImplemented";
            this.lblNotImplemented.Size = new System.Drawing.Size(270, 16);
            this.lblNotImplemented.TabIndex = 10;
            this.lblNotImplemented.Text = "This feature is currently not implemented.";
            this.lblNotImplemented.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Show in Catalogue";
            // 
            // btnUseBusinessApplicationName
            // 
            this.btnUseBusinessApplicationName.Location = new System.Drawing.Point(519, 29);
            this.btnUseBusinessApplicationName.Name = "btnUseBusinessApplicationName";
            this.btnUseBusinessApplicationName.Size = new System.Drawing.Size(75, 23);
            this.btnUseBusinessApplicationName.TabIndex = 7;
            this.btnUseBusinessApplicationName.Text = "Use Name";
            this.btnUseBusinessApplicationName.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Cornsilk;
            this.label7.Location = new System.Drawing.Point(0, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(594, 55);
            this.label7.TabIndex = 0;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkSubService
            // 
            this.chkSubService.AutoSize = true;
            this.chkSubService.Location = new System.Drawing.Point(235, 10);
            this.chkSubService.Name = "chkSubService";
            this.chkSubService.Size = new System.Drawing.Size(15, 14);
            this.chkSubService.TabIndex = 4;
            this.chkSubService.UseVisualStyleBackColor = true;
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.Location = new System.Drawing.Point(3, 34);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(100, 17);
            this.lblDisplayName.TabIndex = 6;
            this.lblDisplayName.Text = "Display Name";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(158, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Sub Service";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Enabled = false;
            this.txtDisplayName.Location = new System.Drawing.Point(109, 31);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(398, 20);
            this.txtDisplayName.TabIndex = 5;
            // 
            // chkShowInCatalogue
            // 
            this.chkShowInCatalogue.Location = new System.Drawing.Point(109, 9);
            this.chkShowInCatalogue.Name = "chkShowInCatalogue";
            this.chkShowInCatalogue.Size = new System.Drawing.Size(15, 17);
            this.chkShowInCatalogue.TabIndex = 1;
            this.chkShowInCatalogue.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtWorkInstructionName);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.txtComment1);
            this.tabPage4.Controls.Add(this.lblComment1);
            this.tabPage4.Controls.Add(this.lblInScopeOverride);
            this.tabPage4.Controls.Add(this.chkInScope);
            this.tabPage4.Controls.Add(this.lblInScope);
            this.tabPage4.Controls.Add(this.txtRecoveryTime);
            this.tabPage4.Controls.Add(this.btnApply);
            this.tabPage4.Controls.Add(this.lblRecoveryTime);
            this.tabPage4.Controls.Add(this.txtID);
            this.tabPage4.Controls.Add(this.lblID);
            this.tabPage4.Controls.Add(this.btnSave);
            this.tabPage4.Controls.Add(this.lblRecoveryTasks);
            this.tabPage4.Controls.Add(this.btnOpenRecoveryTasks);
            this.tabPage4.Controls.Add(this.txtDescription);
            this.tabPage4.Controls.Add(this.lblDescription);
            this.tabPage4.Controls.Add(this.txtVersion);
            this.tabPage4.Controls.Add(this.lblVersion);
            this.tabPage4.ImageIndex = 15;
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(602, 210);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Details";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtComment1
            // 
            this.txtComment1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment1.Location = new System.Drawing.Point(112, 113);
            this.txtComment1.Multiline = true;
            this.txtComment1.Name = "txtComment1";
            this.txtComment1.Size = new System.Drawing.Size(479, 65);
            this.txtComment1.TabIndex = 14;
            // 
            // lblComment1
            // 
            this.lblComment1.AutoSize = true;
            this.lblComment1.Location = new System.Drawing.Point(6, 116);
            this.lblComment1.Name = "lblComment1";
            this.lblComment1.Size = new System.Drawing.Size(56, 13);
            this.lblComment1.TabIndex = 13;
            this.lblComment1.Text = "Comments";
            // 
            // lblInScopeOverride
            // 
            this.lblInScopeOverride.AutoSize = true;
            this.lblInScopeOverride.ForeColor = System.Drawing.Color.Red;
            this.lblInScopeOverride.Location = new System.Drawing.Point(537, 9);
            this.lblInScopeOverride.Name = "lblInScopeOverride";
            this.lblInScopeOverride.Size = new System.Drawing.Size(59, 13);
            this.lblInScopeOverride.TabIndex = 6;
            this.lblInScopeOverride.Text = "Overridden";
            this.lblInScopeOverride.Visible = false;
            // 
            // chkInScope
            // 
            this.chkInScope.AutoSize = true;
            this.chkInScope.Location = new System.Drawing.Point(516, 9);
            this.chkInScope.Name = "chkInScope";
            this.chkInScope.Size = new System.Drawing.Size(15, 14);
            this.chkInScope.TabIndex = 5;
            this.chkInScope.UseVisualStyleBackColor = true;
            this.chkInScope.CheckedChanged += new System.EventHandler(this.chkInScope_CheckedChanged);
            // 
            // lblInScope
            // 
            this.lblInScope.Location = new System.Drawing.Point(456, 9);
            this.lblInScope.Name = "lblInScope";
            this.lblInScope.Size = new System.Drawing.Size(54, 17);
            this.lblInScope.TabIndex = 4;
            this.lblInScope.Text = "In Scope";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(435, 184);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 15;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblRecoveryTime
            // 
            this.lblRecoveryTime.Location = new System.Drawing.Point(6, 63);
            this.lblRecoveryTime.Name = "lblRecoveryTime";
            this.lblRecoveryTime.Size = new System.Drawing.Size(87, 17);
            this.lblRecoveryTime.TabIndex = 10;
            this.lblRecoveryTime.Text = "Recovery Time";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(193, 6);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(246, 20);
            this.txtID.TabIndex = 1;
            // 
            // lblID
            // 
            this.lblID.Location = new System.Drawing.Point(161, 9);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(26, 17);
            this.lblID.TabIndex = 3;
            this.lblID.Text = "ID";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(516, 184);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblRecoveryTasks
            // 
            this.lblRecoveryTasks.Location = new System.Drawing.Point(423, 63);
            this.lblRecoveryTasks.Name = "lblRecoveryTasks";
            this.lblRecoveryTasks.Size = new System.Drawing.Size(87, 17);
            this.lblRecoveryTasks.TabIndex = 11;
            this.lblRecoveryTasks.Text = "Recovery Tasks";
            // 
            // btnOpenRecoveryTasks
            // 
            this.btnOpenRecoveryTasks.Location = new System.Drawing.Point(516, 58);
            this.btnOpenRecoveryTasks.Name = "btnOpenRecoveryTasks";
            this.btnOpenRecoveryTasks.Size = new System.Drawing.Size(75, 23);
            this.btnOpenRecoveryTasks.TabIndex = 12;
            this.btnOpenRecoveryTasks.Text = "Open";
            this.btnOpenRecoveryTasks.UseVisualStyleBackColor = true;
            this.btnOpenRecoveryTasks.Click += new System.EventHandler(this.btnOpenRecoveryTasks_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(112, 32);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(479, 20);
            this.txtDescription.TabIndex = 7;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(6, 35);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(100, 17);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = "Description";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(112, 6);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(43, 20);
            this.txtVersion.TabIndex = 0;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(6, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(100, 17);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Version";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.lstWarnings);
            this.tabPage7.ImageIndex = 16;
            this.tabPage7.Location = new System.Drawing.Point(4, 23);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(602, 210);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Warnings";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // lstWarnings
            // 
            this.lstWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstWarnings.FormattingEnabled = true;
            this.lstWarnings.HorizontalScrollbar = true;
            this.lstWarnings.Location = new System.Drawing.Point(3, 3);
            this.lstWarnings.Name = "lstWarnings";
            this.lstWarnings.Size = new System.Drawing.Size(596, 199);
            this.lstWarnings.TabIndex = 0;
            // 
            // imlServerStateSmall
            // 
            this.imlServerStateSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlServerStateSmall.ImageStream")));
            this.imlServerStateSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imlServerStateSmall.Images.SetKeyName(0, "Server2_blue_32.png");
            this.imlServerStateSmall.Images.SetKeyName(1, "Server2_grey_32.png");
            this.imlServerStateSmall.Images.SetKeyName(2, "Server2_red_32.png");
            this.imlServerStateSmall.Images.SetKeyName(3, "Server2_orange_32.png");
            this.imlServerStateSmall.Images.SetKeyName(4, "Server2_green_32.png");
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
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_full_32;
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // createStubToolStripMenuItem
            // 
            this.createStubToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Box_empty_32;
            this.createStubToolStripMenuItem.Name = "createStubToolStripMenuItem";
            this.createStubToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.createStubToolStripMenuItem.Text = "Create Stub";
            this.createStubToolStripMenuItem.Click += new System.EventHandler(this.createStubToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.gear_blue_32;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // ViewDependencyMapToolStripMenuItem
            // 
            this.ViewDependencyMapToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Globe_32;
            this.ViewDependencyMapToolStripMenuItem.Name = "ViewDependencyMapToolStripMenuItem";
            this.ViewDependencyMapToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
            this.ViewDependencyMapToolStripMenuItem.Text = "View Dependency Map";
            this.ViewDependencyMapToolStripMenuItem.Click += new System.EventHandler(this.ViewDependencyMapToolStripMenuItem_Click);
            // 
            // txtWorkInstructionName
            // 
            this.txtWorkInstructionName.Location = new System.Drawing.Point(112, 87);
            this.txtWorkInstructionName.Name = "txtWorkInstructionName";
            this.txtWorkInstructionName.Size = new System.Drawing.Size(479, 20);
            this.txtWorkInstructionName.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 17);
            this.label6.TabIndex = 19;
            this.label6.Text = "Work Instruction";
            // 
            // txtActualTier
            // 
            this.txtActualTier.Location = new System.Drawing.Point(532, 58);
            this.txtActualTier.Name = "txtActualTier";
            this.txtActualTier.ReadOnly = true;
            this.txtActualTier.Size = new System.Drawing.Size(59, 20);
            this.txtActualTier.TabIndex = 14;
            this.txtActualTier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblActualTier
            // 
            this.lblActualTier.AutoSize = true;
            this.lblActualTier.Location = new System.Drawing.Point(468, 61);
            this.lblActualTier.Name = "lblActualTier";
            this.lblActualTier.Size = new System.Drawing.Size(58, 13);
            this.lblActualTier.TabIndex = 13;
            this.lblActualTier.Text = "Actual Tier";
            // 
            // frmBusinessApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(634, 258);
            this.Controls.Add(this.tabBusinessApplication);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(650, 2098);
            this.MinimumSize = new System.Drawing.Size(650, 294);
            this.Name = "frmBusinessApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Business Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBusinessApplication_FormClosing);
            this.Load += new System.EventHandler(this.frmBusinessApplication_Load);
            this.Shown += new System.EventHandler(this.frmBusinessApplication_Shown);
            this.ResizeEnd += new System.EventHandler(this.frmBusinessApplication_ResizeEnd);
            this.Move += new System.EventHandler(this.frmBusinessApplication_Move);
            this.grpState.ResumeLayout(false);
            this.grpState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWarnings)).EndInit();
            this.grpEmulated.ResumeLayout(false);
            this.grpEmulated.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picService)).EndInit();
            this.tabBusinessApplication.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImageState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDrawingPreview)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.DrawingContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwRunbook;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label lblRunbook;
        private System.Windows.Forms.PictureBox picService;
        private System.Windows.Forms.Label lblStateDescription;
        private System.Windows.Forms.ImageList imlLarge;
        private System.Windows.Forms.GroupBox grpState;
        private System.Windows.Forms.TextBox txtDesiredTier;
        private System.Windows.Forms.TextBox txtStream;
        private System.Windows.Forms.Label lblDesiredTier;
        private System.Windows.Forms.Label lblStream;
        private System.Windows.Forms.ToolTip Hints;
        private System.Windows.Forms.TabControl tabBusinessApplication;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblComponentServers;
        private System.Windows.Forms.Label lblComponentServices;
        private System.Windows.Forms.ListView lvwBusinessApplicationComponentServices;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ListView lvwBusinessApplicationComponentServers;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvwDependentBusinessApplications;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ListView lvwAffectedServers;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblRecoveryTasks;
        private System.Windows.Forms.Button btnOpenRecoveryTasks;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ImageList imlServerStateSmall;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.RadioButton radioButtonEmulated;
        private System.Windows.Forms.RadioButton radioButtonActual;
        private System.Windows.Forms.GroupBox grpEmulated;
        private System.Windows.Forms.RadioButton radioButtonOK;
        private System.Windows.Forms.RadioButton radioButtonDegraded;
        private System.Windows.Forms.RadioButton radioButtonError;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox txtDrawingID;
        private System.Windows.Forms.Label lblDrawingID;
        private System.Windows.Forms.TextBox txtDrawingDescription;
        private System.Windows.Forms.Label lblDrawingDescription;
        private System.Windows.Forms.TextBox txtDrawingVersion;
        private System.Windows.Forms.Label lblDrawingVersion;
        private System.Windows.Forms.ComboBox cmbDrawingStatus;
        private System.Windows.Forms.Label lblDrawingStatus;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.Button btnApplyDrawing;
        private System.Windows.Forms.Button btnSaveDrawing;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TextBox txtRecoveryTime;
        private System.Windows.Forms.Label lblRecoveryTime;
        private System.Windows.Forms.CheckBox chkInScope;
        private System.Windows.Forms.Label lblInScope;
        private System.Windows.Forms.ImageList imlSmall;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.PictureBox picDrawingPreview;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.Label lblNotImplemented;
        private System.Windows.Forms.TextBox txtLastUpdate;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.TextBox txtDrawingNotes;
        private System.Windows.Forms.Label lblDrawingNotes;
        private System.Windows.Forms.Label lblInScopeOverride;
        private System.Windows.Forms.ContextMenuStrip DrawingContextMenu;
        private System.Windows.Forms.ToolStripMenuItem createManagementPackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewDependencyMapToolStripMenuItem;
        private System.Windows.Forms.PictureBox picWarnings;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.ListBox lstWarnings;
        private System.Windows.Forms.PictureBox picImageState;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label lblHA;
        private System.Windows.Forms.CheckBox chkHA;
        private System.Windows.Forms.Button btnCreateMP;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createStubToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUseBusinessApplicationName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkSubService;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.CheckBox chkShowInCatalogue;
        private System.Windows.Forms.TextBox txtComment1;
        private System.Windows.Forms.Label lblComment1;
        private System.Windows.Forms.TextBox txtRunbookOrder;
        private System.Windows.Forms.Label lblRunbookOrder;
        private System.Windows.Forms.ToolStripMenuItem CopyNamesToolStripMenuItem;
        private System.Windows.Forms.Label lblMPVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWorkInstructionName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtActualTier;
        private System.Windows.Forms.Label lblActualTier;
    }
}