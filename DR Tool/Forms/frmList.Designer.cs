namespace DRPlanningTool
{
    partial class frmList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmList));
            this.cmbRunbook = new System.Windows.Forms.ComboBox();
            this.lvwRunbook = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlMain = new System.Windows.Forms.ImageList(this.components);
            this.btnExport = new System.Windows.Forms.Button();
            this.lblChooseAList = new System.Windows.Forms.Label();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.grpSortBy = new System.Windows.Forms.GroupBox();
            this.rbSortByNone = new System.Windows.Forms.RadioButton();
            this.rbDescending = new System.Windows.Forms.RadioButton();
            this.rbAscending = new System.Windows.Forms.RadioButton();
            this.grpFilterOn = new System.Windows.Forms.GroupBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.grpGroupBy = new System.Windows.Forms.GroupBox();
            this.rbGroupByTier = new System.Windows.Forms.RadioButton();
            this.rbGroupByStream = new System.Windows.Forms.RadioButton();
            this.rbGroupByBusinessApplication = new System.Windows.Forms.RadioButton();
            this.rbGroupByNone = new System.Windows.Forms.RadioButton();
            this.grpDisplayBy = new System.Windows.Forms.GroupBox();
            this.rbDisplayByBusinessApplication = new System.Windows.Forms.RadioButton();
            this.rbDisplayByServer = new System.Windows.Forms.RadioButton();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.lblSelectedText = new System.Windows.Forms.Label();
            this.lblComponentText = new System.Windows.Forms.Label();
            this.lblDependentText = new System.Windows.Forms.Label();
            this.lblMovementText = new System.Windows.Forms.Label();
            this.pnlSelected = new System.Windows.Forms.Panel();
            this.pnlMovement = new System.Windows.Forms.Panel();
            this.pnlMandatoryComponent = new System.Windows.Forms.Panel();
            this.pnlMandatoryDependent = new System.Windows.Forms.Panel();
            this.lblOptionalDependent = new System.Windows.Forms.Label();
            this.pnlOptionalDependent = new System.Windows.Forms.Panel();
            this.pnlOptionalComponent = new System.Windows.Forms.Panel();
            this.lblOptionalComponent = new System.Windows.Forms.Label();
            this.lblMandatoryComponent = new System.Windows.Forms.Label();
            this.lblMandatoryDependent = new System.Windows.Forms.Label();
            this.DrawingContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createManagementPackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createStubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewDependencyMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtSearchFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpOptions.SuspendLayout();
            this.grpSortBy.SuspendLayout();
            this.grpFilterOn.SuspendLayout();
            this.grpGroupBy.SuspendLayout();
            this.grpDisplayBy.SuspendLayout();
            this.DrawingContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbRunbook
            // 
            this.cmbRunbook.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRunbook.FormattingEnabled = true;
            this.cmbRunbook.Location = new System.Drawing.Point(106, 6);
            this.cmbRunbook.Name = "cmbRunbook";
            this.cmbRunbook.Size = new System.Drawing.Size(168, 21);
            this.cmbRunbook.TabIndex = 0;
            this.cmbRunbook.SelectedIndexChanged += new System.EventHandler(this.cmbRunbook_SelectedIndexChanged);
            // 
            // lvwRunbook
            // 
            this.lvwRunbook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwRunbook.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvwRunbook.FullRowSelect = true;
            this.lvwRunbook.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwRunbook.HideSelection = false;
            this.lvwRunbook.Location = new System.Drawing.Point(12, 61);
            this.lvwRunbook.Name = "lvwRunbook";
            this.lvwRunbook.Size = new System.Drawing.Size(332, 253);
            this.lvwRunbook.SmallImageList = this.imlMain;
            this.lvwRunbook.TabIndex = 7;
            this.lvwRunbook.UseCompatibleStateImageBehavior = false;
            this.lvwRunbook.View = System.Windows.Forms.View.Details;
            this.lvwRunbook.SelectedIndexChanged += new System.EventHandler(this.lvwRunbook_SelectedIndexChanged);
            this.lvwRunbook.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwRunbook_KeyDown);
            this.lvwRunbook.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwRunbook_MouseClick);
            this.lvwRunbook.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwRunbook_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 220;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tier";
            this.columnHeader2.Width = 30;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Stream";
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
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(297, 377);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 25;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblChooseAList
            // 
            this.lblChooseAList.Location = new System.Drawing.Point(9, 9);
            this.lblChooseAList.Name = "lblChooseAList";
            this.lblChooseAList.Size = new System.Drawing.Size(87, 17);
            this.lblChooseAList.TabIndex = 1;
            this.lblChooseAList.Text = "Choose a List...";
            // 
            // grpOptions
            // 
            this.grpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOptions.Controls.Add(this.grpSortBy);
            this.grpOptions.Controls.Add(this.grpFilterOn);
            this.grpOptions.Controls.Add(this.grpGroupBy);
            this.grpOptions.Controls.Add(this.grpDisplayBy);
            this.grpOptions.Enabled = false;
            this.grpOptions.Location = new System.Drawing.Point(12, 101);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(199, 212);
            this.grpOptions.TabIndex = 9;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // grpSortBy
            // 
            this.grpSortBy.Controls.Add(this.rbSortByNone);
            this.grpSortBy.Controls.Add(this.rbDescending);
            this.grpSortBy.Controls.Add(this.rbAscending);
            this.grpSortBy.Location = new System.Drawing.Point(6, 149);
            this.grpSortBy.Name = "grpSortBy";
            this.grpSortBy.Size = new System.Drawing.Size(247, 53);
            this.grpSortBy.TabIndex = 3;
            this.grpSortBy.TabStop = false;
            this.grpSortBy.Text = "Sort By";
            // 
            // rbSortByNone
            // 
            this.rbSortByNone.AutoSize = true;
            this.rbSortByNone.Location = new System.Drawing.Point(7, 19);
            this.rbSortByNone.Name = "rbSortByNone";
            this.rbSortByNone.Size = new System.Drawing.Size(51, 17);
            this.rbSortByNone.TabIndex = 0;
            this.rbSortByNone.TabStop = true;
            this.rbSortByNone.Text = "None";
            this.rbSortByNone.UseVisualStyleBackColor = true;
            this.rbSortByNone.CheckedChanged += new System.EventHandler(this.rbSortByNone_CheckedChanged);
            // 
            // rbDescending
            // 
            this.rbDescending.AutoSize = true;
            this.rbDescending.Location = new System.Drawing.Point(115, 19);
            this.rbDescending.Name = "rbDescending";
            this.rbDescending.Size = new System.Drawing.Size(47, 17);
            this.rbDescending.TabIndex = 2;
            this.rbDescending.TabStop = true;
            this.rbDescending.Text = "Des.";
            this.rbDescending.UseVisualStyleBackColor = true;
            this.rbDescending.CheckedChanged += new System.EventHandler(this.rbDescending_CheckedChanged);
            // 
            // rbAscending
            // 
            this.rbAscending.AutoSize = true;
            this.rbAscending.Location = new System.Drawing.Point(64, 19);
            this.rbAscending.Name = "rbAscending";
            this.rbAscending.Size = new System.Drawing.Size(46, 17);
            this.rbAscending.TabIndex = 1;
            this.rbAscending.TabStop = true;
            this.rbAscending.Text = "Asc.";
            this.rbAscending.UseVisualStyleBackColor = true;
            this.rbAscending.CheckedChanged += new System.EventHandler(this.rbAscending_CheckedChanged);
            // 
            // grpFilterOn
            // 
            this.grpFilterOn.Controls.Add(this.btnGo);
            this.grpFilterOn.Controls.Add(this.txtFilter);
            this.grpFilterOn.Controls.Add(this.cmbFilter);
            this.grpFilterOn.Location = new System.Drawing.Point(6, 90);
            this.grpFilterOn.Name = "grpFilterOn";
            this.grpFilterOn.Size = new System.Drawing.Size(247, 53);
            this.grpFilterOn.TabIndex = 2;
            this.grpFilterOn.TabStop = false;
            this.grpFilterOn.Text = "Filter on";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(206, 18);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(35, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(97, 20);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(103, 20);
            this.txtFilter.TabIndex = 1;
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Items.AddRange(new object[] {
            "Service",
            "Stream",
            "Tier",
            "None"});
            this.cmbFilter.Location = new System.Drawing.Point(7, 20);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(84, 21);
            this.cmbFilter.TabIndex = 0;
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            // 
            // grpGroupBy
            // 
            this.grpGroupBy.Controls.Add(this.rbGroupByTier);
            this.grpGroupBy.Controls.Add(this.rbGroupByStream);
            this.grpGroupBy.Controls.Add(this.rbGroupByBusinessApplication);
            this.grpGroupBy.Controls.Add(this.rbGroupByNone);
            this.grpGroupBy.Location = new System.Drawing.Point(103, 19);
            this.grpGroupBy.Name = "grpGroupBy";
            this.grpGroupBy.Size = new System.Drawing.Size(150, 65);
            this.grpGroupBy.TabIndex = 1;
            this.grpGroupBy.TabStop = false;
            this.grpGroupBy.Text = "Group by";
            // 
            // rbGroupByTier
            // 
            this.rbGroupByTier.AutoSize = true;
            this.rbGroupByTier.Location = new System.Drawing.Point(83, 42);
            this.rbGroupByTier.Name = "rbGroupByTier";
            this.rbGroupByTier.Size = new System.Drawing.Size(43, 17);
            this.rbGroupByTier.TabIndex = 3;
            this.rbGroupByTier.TabStop = true;
            this.rbGroupByTier.Text = "Tier";
            this.rbGroupByTier.UseVisualStyleBackColor = true;
            this.rbGroupByTier.CheckedChanged += new System.EventHandler(this.rbGroupByTier_CheckedChanged);
            // 
            // rbGroupByStream
            // 
            this.rbGroupByStream.AutoSize = true;
            this.rbGroupByStream.Location = new System.Drawing.Point(18, 42);
            this.rbGroupByStream.Name = "rbGroupByStream";
            this.rbGroupByStream.Size = new System.Drawing.Size(58, 17);
            this.rbGroupByStream.TabIndex = 2;
            this.rbGroupByStream.TabStop = true;
            this.rbGroupByStream.Text = "Stream";
            this.rbGroupByStream.UseVisualStyleBackColor = true;
            this.rbGroupByStream.CheckedChanged += new System.EventHandler(this.rbGroupByStream_CheckedChanged);
            // 
            // rbGroupByBusinessApplication
            // 
            this.rbGroupByBusinessApplication.AutoSize = true;
            this.rbGroupByBusinessApplication.Location = new System.Drawing.Point(83, 20);
            this.rbGroupByBusinessApplication.Name = "rbGroupByBusinessApplication";
            this.rbGroupByBusinessApplication.Size = new System.Drawing.Size(61, 17);
            this.rbGroupByBusinessApplication.TabIndex = 1;
            this.rbGroupByBusinessApplication.TabStop = true;
            this.rbGroupByBusinessApplication.Text = "Service";
            this.rbGroupByBusinessApplication.UseVisualStyleBackColor = true;
            this.rbGroupByBusinessApplication.CheckedChanged += new System.EventHandler(this.rbGroupByBusinessApplication_CheckedChanged);
            // 
            // rbGroupByNone
            // 
            this.rbGroupByNone.AutoSize = true;
            this.rbGroupByNone.Location = new System.Drawing.Point(18, 19);
            this.rbGroupByNone.Name = "rbGroupByNone";
            this.rbGroupByNone.Size = new System.Drawing.Size(51, 17);
            this.rbGroupByNone.TabIndex = 0;
            this.rbGroupByNone.TabStop = true;
            this.rbGroupByNone.Text = "None";
            this.rbGroupByNone.UseVisualStyleBackColor = true;
            this.rbGroupByNone.CheckedChanged += new System.EventHandler(this.rbGroupByNone_CheckedChanged);
            // 
            // grpDisplayBy
            // 
            this.grpDisplayBy.Controls.Add(this.rbDisplayByBusinessApplication);
            this.grpDisplayBy.Controls.Add(this.rbDisplayByServer);
            this.grpDisplayBy.Location = new System.Drawing.Point(6, 19);
            this.grpDisplayBy.Name = "grpDisplayBy";
            this.grpDisplayBy.Size = new System.Drawing.Size(91, 65);
            this.grpDisplayBy.TabIndex = 0;
            this.grpDisplayBy.TabStop = false;
            this.grpDisplayBy.Text = "Display by";
            // 
            // rbDisplayByBusinessApplication
            // 
            this.rbDisplayByBusinessApplication.AutoSize = true;
            this.rbDisplayByBusinessApplication.Location = new System.Drawing.Point(18, 42);
            this.rbDisplayByBusinessApplication.Name = "rbDisplayByBusinessApplication";
            this.rbDisplayByBusinessApplication.Size = new System.Drawing.Size(61, 17);
            this.rbDisplayByBusinessApplication.TabIndex = 1;
            this.rbDisplayByBusinessApplication.TabStop = true;
            this.rbDisplayByBusinessApplication.Text = "Service";
            this.rbDisplayByBusinessApplication.UseVisualStyleBackColor = true;
            this.rbDisplayByBusinessApplication.CheckedChanged += new System.EventHandler(this.rbDisplayByBusinessApplication_CheckedChanged);
            // 
            // rbDisplayByServer
            // 
            this.rbDisplayByServer.AutoSize = true;
            this.rbDisplayByServer.Location = new System.Drawing.Point(18, 20);
            this.rbDisplayByServer.Name = "rbDisplayByServer";
            this.rbDisplayByServer.Size = new System.Drawing.Size(56, 17);
            this.rbDisplayByServer.TabIndex = 0;
            this.rbDisplayByServer.TabStop = true;
            this.rbDisplayByServer.Text = "Server";
            this.rbDisplayByServer.UseVisualStyleBackColor = true;
            this.rbDisplayByServer.CheckedChanged += new System.EventHandler(this.rbDisplayByServer_CheckedChanged);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Enabled = false;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(350, 83);
            this.btnDown.Margin = new System.Windows.Forms.Padding(0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(25, 25);
            this.btnDown.TabIndex = 8;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Enabled = false;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(350, 58);
            this.btnUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(25, 25);
            this.btnUp.TabIndex = 6;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // lblSelectedText
            // 
            this.lblSelectedText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSelectedText.Location = new System.Drawing.Point(11, 335);
            this.lblSelectedText.Name = "lblSelectedText";
            this.lblSelectedText.Size = new System.Drawing.Size(62, 15);
            this.lblSelectedText.TabIndex = 12;
            this.lblSelectedText.Text = "Selected";
            // 
            // lblComponentText
            // 
            this.lblComponentText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblComponentText.Location = new System.Drawing.Point(133, 317);
            this.lblComponentText.Name = "lblComponentText";
            this.lblComponentText.Size = new System.Drawing.Size(97, 15);
            this.lblComponentText.TabIndex = 10;
            this.lblComponentText.Text = "Components";
            this.lblComponentText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDependentText
            // 
            this.lblDependentText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDependentText.Location = new System.Drawing.Point(247, 317);
            this.lblDependentText.Name = "lblDependentText";
            this.lblDependentText.Size = new System.Drawing.Size(97, 15);
            this.lblDependentText.TabIndex = 11;
            this.lblDependentText.Text = "Dependents";
            this.lblDependentText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblMovementText
            // 
            this.lblMovementText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMovementText.Location = new System.Drawing.Point(9, 356);
            this.lblMovementText.Name = "lblMovementText";
            this.lblMovementText.Size = new System.Drawing.Size(62, 15);
            this.lblMovementText.TabIndex = 18;
            this.lblMovementText.Text = "Movement";
            // 
            // pnlSelected
            // 
            this.pnlSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSelected.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pnlSelected.Location = new System.Drawing.Point(79, 335);
            this.pnlSelected.Name = "pnlSelected";
            this.pnlSelected.Size = new System.Drawing.Size(32, 15);
            this.pnlSelected.TabIndex = 13;
            // 
            // pnlMovement
            // 
            this.pnlMovement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMovement.Location = new System.Drawing.Point(79, 356);
            this.pnlMovement.Name = "pnlMovement";
            this.pnlMovement.Size = new System.Drawing.Size(32, 15);
            this.pnlMovement.TabIndex = 19;
            // 
            // pnlMandatoryComponent
            // 
            this.pnlMandatoryComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMandatoryComponent.BackColor = System.Drawing.Color.Peru;
            this.pnlMandatoryComponent.Location = new System.Drawing.Point(198, 335);
            this.pnlMandatoryComponent.Name = "pnlMandatoryComponent";
            this.pnlMandatoryComponent.Size = new System.Drawing.Size(32, 15);
            this.pnlMandatoryComponent.TabIndex = 15;
            // 
            // pnlMandatoryDependent
            // 
            this.pnlMandatoryDependent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMandatoryDependent.BackColor = System.Drawing.Color.DarkKhaki;
            this.pnlMandatoryDependent.Location = new System.Drawing.Point(312, 335);
            this.pnlMandatoryDependent.Name = "pnlMandatoryDependent";
            this.pnlMandatoryDependent.Size = new System.Drawing.Size(32, 15);
            this.pnlMandatoryDependent.TabIndex = 17;
            // 
            // lblOptionalDependent
            // 
            this.lblOptionalDependent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOptionalDependent.Location = new System.Drawing.Point(244, 356);
            this.lblOptionalDependent.Name = "lblOptionalDependent";
            this.lblOptionalDependent.Size = new System.Drawing.Size(62, 15);
            this.lblOptionalDependent.TabIndex = 22;
            this.lblOptionalDependent.Text = "Optional";
            // 
            // pnlOptionalDependent
            // 
            this.pnlOptionalDependent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlOptionalDependent.BackColor = System.Drawing.Color.Khaki;
            this.pnlOptionalDependent.Location = new System.Drawing.Point(312, 356);
            this.pnlOptionalDependent.Name = "pnlOptionalDependent";
            this.pnlOptionalDependent.Size = new System.Drawing.Size(32, 15);
            this.pnlOptionalDependent.TabIndex = 23;
            // 
            // pnlOptionalComponent
            // 
            this.pnlOptionalComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlOptionalComponent.BackColor = System.Drawing.Color.Orange;
            this.pnlOptionalComponent.Location = new System.Drawing.Point(198, 356);
            this.pnlOptionalComponent.Name = "pnlOptionalComponent";
            this.pnlOptionalComponent.Size = new System.Drawing.Size(32, 15);
            this.pnlOptionalComponent.TabIndex = 21;
            // 
            // lblOptionalComponent
            // 
            this.lblOptionalComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOptionalComponent.Location = new System.Drawing.Point(130, 356);
            this.lblOptionalComponent.Name = "lblOptionalComponent";
            this.lblOptionalComponent.Size = new System.Drawing.Size(62, 15);
            this.lblOptionalComponent.TabIndex = 20;
            this.lblOptionalComponent.Text = "Optional";
            // 
            // lblMandatoryComponent
            // 
            this.lblMandatoryComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMandatoryComponent.Location = new System.Drawing.Point(130, 335);
            this.lblMandatoryComponent.Name = "lblMandatoryComponent";
            this.lblMandatoryComponent.Size = new System.Drawing.Size(62, 15);
            this.lblMandatoryComponent.TabIndex = 14;
            this.lblMandatoryComponent.Text = "Mandatory";
            // 
            // lblMandatoryDependent
            // 
            this.lblMandatoryDependent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMandatoryDependent.Location = new System.Drawing.Point(244, 335);
            this.lblMandatoryDependent.Name = "lblMandatoryDependent";
            this.lblMandatoryDependent.Size = new System.Drawing.Size(62, 15);
            this.lblMandatoryDependent.TabIndex = 16;
            this.lblMandatoryDependent.Text = "Mandatory";
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
            this.openToolStripMenuItem.Enabled = false;
            this.openToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Blueprint_blue_32;
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
            // btnClearFilter
            // 
            this.btnClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFilter.Image = global::DRPlanningTool.Properties.Resources.Close_16;
            this.btnClearFilter.Location = new System.Drawing.Point(319, 30);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(25, 25);
            this.btnClearFilter.TabIndex = 4;
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.Image = global::DRPlanningTool.Properties.Resources.Ok_16;
            this.btnFilter.Location = new System.Drawing.Point(350, 30);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(25, 25);
            this.btnFilter.TabIndex = 5;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtSearchFilter
            // 
            this.txtSearchFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchFilter.Location = new System.Drawing.Point(47, 33);
            this.txtSearchFilter.Name = "txtSearchFilter";
            this.txtSearchFilter.Size = new System.Drawing.Size(266, 20);
            this.txtSearchFilter.TabIndex = 3;
            this.txtSearchFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchFilter_KeyDown);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(12, 36);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(29, 13);
            this.lblFilter.TabIndex = 2;
            this.lblFilter.Text = "Filter";
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(216, 377);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 24;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.Location = new System.Drawing.Point(9, 382);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(201, 18);
            this.lblInfo.TabIndex = 26;
            this.lblInfo.Text = "Idle";
            // 
            // frmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 412);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnClearFilter);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.txtSearchFilter);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblMandatoryDependent);
            this.Controls.Add(this.lblMandatoryComponent);
            this.Controls.Add(this.lblOptionalComponent);
            this.Controls.Add(this.pnlOptionalComponent);
            this.Controls.Add(this.pnlOptionalDependent);
            this.Controls.Add(this.lblOptionalDependent);
            this.Controls.Add(this.pnlMovement);
            this.Controls.Add(this.pnlMandatoryComponent);
            this.Controls.Add(this.pnlMandatoryDependent);
            this.Controls.Add(this.pnlSelected);
            this.Controls.Add(this.lblMovementText);
            this.Controls.Add(this.lblDependentText);
            this.Controls.Add(this.lblComponentText);
            this.Controls.Add(this.lblSelectedText);
            this.Controls.Add(this.lvwRunbook);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.lblChooseAList);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.cmbRunbook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(800, 1997);
            this.MinimumSize = new System.Drawing.Size(399, 449);
            this.Name = "frmList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRunbook_FormClosing);
            this.Load += new System.EventHandler(this.frmRunbook_Load);
            this.Shown += new System.EventHandler(this.frmList_Shown);
            this.Resize += new System.EventHandler(this.frmList_Resize);
            this.grpOptions.ResumeLayout(false);
            this.grpSortBy.ResumeLayout(false);
            this.grpSortBy.PerformLayout();
            this.grpFilterOn.ResumeLayout(false);
            this.grpFilterOn.PerformLayout();
            this.grpGroupBy.ResumeLayout(false);
            this.grpGroupBy.PerformLayout();
            this.grpDisplayBy.ResumeLayout(false);
            this.grpDisplayBy.PerformLayout();
            this.DrawingContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRunbook;
        private System.Windows.Forms.ListView lvwRunbook;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imlMain;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblChooseAList;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.GroupBox grpFilterOn;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.GroupBox grpGroupBy;
        private System.Windows.Forms.RadioButton rbGroupByTier;
        private System.Windows.Forms.RadioButton rbGroupByStream;
        private System.Windows.Forms.RadioButton rbGroupByBusinessApplication;
        private System.Windows.Forms.RadioButton rbGroupByNone;
        private System.Windows.Forms.GroupBox grpDisplayBy;
        private System.Windows.Forms.RadioButton rbDisplayByBusinessApplication;
        private System.Windows.Forms.RadioButton rbDisplayByServer;
        private System.Windows.Forms.GroupBox grpSortBy;
        private System.Windows.Forms.RadioButton rbDescending;
        private System.Windows.Forms.RadioButton rbAscending;
        private System.Windows.Forms.RadioButton rbSortByNone;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblSelectedText;
        private System.Windows.Forms.Label lblComponentText;
        private System.Windows.Forms.Label lblDependentText;
        private System.Windows.Forms.Label lblMovementText;
        private System.Windows.Forms.Panel pnlSelected;
        private System.Windows.Forms.Panel pnlMovement;
        private System.Windows.Forms.Panel pnlMandatoryComponent;
        private System.Windows.Forms.Panel pnlMandatoryDependent;
        private System.Windows.Forms.Label lblOptionalDependent;
        private System.Windows.Forms.Panel pnlOptionalDependent;
        private System.Windows.Forms.Panel pnlOptionalComponent;
        private System.Windows.Forms.Label lblOptionalComponent;
        private System.Windows.Forms.Label lblMandatoryComponent;
        private System.Windows.Forms.Label lblMandatoryDependent;
        private System.Windows.Forms.ContextMenuStrip DrawingContextMenu;
        private System.Windows.Forms.ToolStripMenuItem createManagementPackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewDependencyMapToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtSearchFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createStubToolStripMenuItem;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ToolStripMenuItem CopyNamesToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label lblInfo;
    }
}