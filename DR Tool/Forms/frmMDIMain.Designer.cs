namespace DRPlanningTool
{
    partial class frmMDIMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMDIMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sRMRunbookToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceRunbookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverRunbookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sRMRunbookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.DependencyMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recoveryEstimatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceFormsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BusinessApplicationImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runbookFormsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverFormsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetServerStatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retrieveSCOMServerHealthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findSystemCenterComponentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverSharesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulkDatabaseUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorCodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoadiServerProduction = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoadiServerDevelopment = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCreateManagementPacks = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResetServerStates = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGetServerHealth = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tmrUpdates = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1285, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iServerToolStripMenuItem});
            this.loadToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.connect_32_blue;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // iServerToolStripMenuItem
            // 
            this.iServerToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.i_32_green;
            this.iServerToolStripMenuItem.Name = "iServerToolStripMenuItem";
            this.iServerToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.iServerToolStripMenuItem.Text = "iServer";
            this.iServerToolStripMenuItem.Click += new System.EventHandler(this.iServerToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailsToolStripMenuItem,
            this.sRMRunbookToolStripMenuItem1});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Visible = false;
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.detailsToolStripMenuItem.Text = "Details";
            // 
            // sRMRunbookToolStripMenuItem1
            // 
            this.sRMRunbookToolStripMenuItem1.Name = "sRMRunbookToolStripMenuItem1";
            this.sRMRunbookToolStripMenuItem1.Size = new System.Drawing.Size(149, 22);
            this.sRMRunbookToolStripMenuItem1.Text = "SRM Runbook";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(153, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.exitToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.beta_general_stop_32;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviceRunbookToolStripMenuItem,
            this.serverListToolStripMenuItem,
            this.serverRunbookToolStripMenuItem,
            this.sRMRunbookToolStripMenuItem,
            this.toolStripMenuItem1,
            this.DependencyMapToolStripMenuItem,
            this.recoveryEstimatesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.closeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // serviceRunbookToolStripMenuItem
            // 
            this.serviceRunbookToolStripMenuItem.Enabled = false;
            this.serviceRunbookToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.stamped_document_32_blue;
            this.serviceRunbookToolStripMenuItem.Name = "serviceRunbookToolStripMenuItem";
            this.serviceRunbookToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.serviceRunbookToolStripMenuItem.Text = "Business Application Runbook...";
            this.serviceRunbookToolStripMenuItem.Click += new System.EventHandler(this.serviceRunbookToolStripMenuItem_Click);
            // 
            // serverListToolStripMenuItem
            // 
            this.serverListToolStripMenuItem.Enabled = false;
            this.serverListToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.stamped_document_32_pink;
            this.serverListToolStripMenuItem.Name = "serverListToolStripMenuItem";
            this.serverListToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.serverListToolStripMenuItem.Text = "Server List...";
            this.serverListToolStripMenuItem.Click += new System.EventHandler(this.serverListToolStripMenuItem_Click);
            // 
            // serverRunbookToolStripMenuItem
            // 
            this.serverRunbookToolStripMenuItem.Enabled = false;
            this.serverRunbookToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.stamped_document_32_purple;
            this.serverRunbookToolStripMenuItem.Name = "serverRunbookToolStripMenuItem";
            this.serverRunbookToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.serverRunbookToolStripMenuItem.Text = "Server Runbook...";
            this.serverRunbookToolStripMenuItem.Click += new System.EventHandler(this.serverRunbookToolStripMenuItem_Click);
            // 
            // sRMRunbookToolStripMenuItem
            // 
            this.sRMRunbookToolStripMenuItem.Enabled = false;
            this.sRMRunbookToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.stamped_document_32_lightblue;
            this.sRMRunbookToolStripMenuItem.Name = "sRMRunbookToolStripMenuItem";
            this.sRMRunbookToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.sRMRunbookToolStripMenuItem.Text = "SRM Runbook...";
            this.sRMRunbookToolStripMenuItem.Click += new System.EventHandler(this.sRMRunbookToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(240, 6);
            this.toolStripMenuItem1.Visible = false;
            // 
            // DependencyMapToolStripMenuItem
            // 
            this.DependencyMapToolStripMenuItem.Enabled = false;
            this.DependencyMapToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Globe_32;
            this.DependencyMapToolStripMenuItem.Name = "DependencyMapToolStripMenuItem";
            this.DependencyMapToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.DependencyMapToolStripMenuItem.Text = "Dependency Map";
            this.DependencyMapToolStripMenuItem.Visible = false;
            this.DependencyMapToolStripMenuItem.Click += new System.EventHandler(this.DependencyMapToolStripMenuItem_Click);
            // 
            // recoveryEstimatesToolStripMenuItem
            // 
            this.recoveryEstimatesToolStripMenuItem.Enabled = false;
            this.recoveryEstimatesToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.clock_32;
            this.recoveryEstimatesToolStripMenuItem.Name = "recoveryEstimatesToolStripMenuItem";
            this.recoveryEstimatesToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.recoveryEstimatesToolStripMenuItem.Text = "Recovery Estimates";
            this.recoveryEstimatesToolStripMenuItem.Visible = false;
            this.recoveryEstimatesToolStripMenuItem.Click += new System.EventHandler(this.recoveryEstimatesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(240, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviceFormsToolStripMenuItem,
            this.BusinessApplicationImagesToolStripMenuItem,
            this.runbookFormsToolStripMenuItem,
            this.serverFormsToolStripMenuItem});
            this.closeToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Close_32;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.ToolTipText = "Close windows";
            // 
            // serviceFormsToolStripMenuItem
            // 
            this.serviceFormsToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.software_32_close_blue;
            this.serviceFormsToolStripMenuItem.Name = "serviceFormsToolStripMenuItem";
            this.serviceFormsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.serviceFormsToolStripMenuItem.Text = "Business Application Details";
            this.serviceFormsToolStripMenuItem.ToolTipText = "Close all Business Application details windows";
            this.serviceFormsToolStripMenuItem.Click += new System.EventHandler(this.serviceFormsToolStripMenuItem_Click);
            // 
            // BusinessApplicationImagesToolStripMenuItem
            // 
            this.BusinessApplicationImagesToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.picture_landscape_close_32_blue;
            this.BusinessApplicationImagesToolStripMenuItem.Name = "BusinessApplicationImagesToolStripMenuItem";
            this.BusinessApplicationImagesToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.BusinessApplicationImagesToolStripMenuItem.Text = "Business Application Images";
            this.BusinessApplicationImagesToolStripMenuItem.ToolTipText = "Close all Business Application Image windows";
            this.BusinessApplicationImagesToolStripMenuItem.Click += new System.EventHandler(this.BusinessApplicationImagesToolStripMenuItem_Click);
            // 
            // runbookFormsToolStripMenuItem
            // 
            this.runbookFormsToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.script_32_close_blue;
            this.runbookFormsToolStripMenuItem.Name = "runbookFormsToolStripMenuItem";
            this.runbookFormsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.runbookFormsToolStripMenuItem.Text = "Lists";
            this.runbookFormsToolStripMenuItem.ToolTipText = "Close all List windows";
            this.runbookFormsToolStripMenuItem.Click += new System.EventHandler(this.runbookFormsToolStripMenuItem_Click);
            // 
            // serverFormsToolStripMenuItem
            // 
            this.serverFormsToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.server_32_close_blue;
            this.serverFormsToolStripMenuItem.Name = "serverFormsToolStripMenuItem";
            this.serverFormsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.serverFormsToolStripMenuItem.Text = "Server Details";
            this.serverFormsToolStripMenuItem.ToolTipText = "Close all Server details windows";
            this.serverFormsToolStripMenuItem.Click += new System.EventHandler(this.serverFormsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetServerStatesToolStripMenuItem,
            this.retrieveSCOMServerHealthToolStripMenuItem,
            this.findSystemCenterComponentToolStripMenuItem,
            this.serverSharesToolStripMenuItem,
            this.simulationToolStripMenuItem,
            this.toolStripMenuItem3,
            this.optionsToolStripMenuItem,
            this.bulkDatabaseUpdateToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // resetServerStatesToolStripMenuItem
            // 
            this.resetServerStatesToolStripMenuItem.Enabled = false;
            this.resetServerStatesToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.Server2_green_32;
            this.resetServerStatesToolStripMenuItem.Name = "resetServerStatesToolStripMenuItem";
            this.resetServerStatesToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.resetServerStatesToolStripMenuItem.Text = "Reset Server States";
            this.resetServerStatesToolStripMenuItem.ToolTipText = "Reset all Server states back to the default";
            this.resetServerStatesToolStripMenuItem.Visible = false;
            this.resetServerStatesToolStripMenuItem.Click += new System.EventHandler(this.resetServerStatesToolStripMenuItem_Click);
            // 
            // retrieveSCOMServerHealthToolStripMenuItem
            // 
            this.retrieveSCOMServerHealthToolStripMenuItem.Enabled = false;
            this.retrieveSCOMServerHealthToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.thermometer_32_blue;
            this.retrieveSCOMServerHealthToolStripMenuItem.Name = "retrieveSCOMServerHealthToolStripMenuItem";
            this.retrieveSCOMServerHealthToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.retrieveSCOMServerHealthToolStripMenuItem.Text = "Retrieve SCOM Server Health";
            this.retrieveSCOMServerHealthToolStripMenuItem.ToolTipText = "Query System Center Operations Manager for the Health of each Server";
            this.retrieveSCOMServerHealthToolStripMenuItem.Visible = false;
            this.retrieveSCOMServerHealthToolStripMenuItem.Click += new System.EventHandler(this.retrieveSCOMServerHealthToolStripMenuItem_Click);
            // 
            // findSystemCenterComponentToolStripMenuItem
            // 
            this.findSystemCenterComponentToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.database_blue_32;
            this.findSystemCenterComponentToolStripMenuItem.Name = "findSystemCenterComponentToolStripMenuItem";
            this.findSystemCenterComponentToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.findSystemCenterComponentToolStripMenuItem.Text = "Find System Center Component";
            this.findSystemCenterComponentToolStripMenuItem.Click += new System.EventHandler(this.findSystemCenterComponentToolStripMenuItem_Click);
            // 
            // serverSharesToolStripMenuItem
            // 
            this.serverSharesToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.folder_32_blue;
            this.serverSharesToolStripMenuItem.Name = "serverSharesToolStripMenuItem";
            this.serverSharesToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.serverSharesToolStripMenuItem.Text = "Server Share Information...";
            this.serverSharesToolStripMenuItem.Click += new System.EventHandler(this.serverSharesToolStripMenuItem_Click);
            // 
            // simulationToolStripMenuItem
            // 
            this.simulationToolStripMenuItem.Enabled = false;
            this.simulationToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.play_blue_32;
            this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
            this.simulationToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.simulationToolStripMenuItem.Text = "Simulation...";
            this.simulationToolStripMenuItem.Visible = false;
            this.simulationToolStripMenuItem.Click += new System.EventHandler(this.simulationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(240, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.audio_equalizer_32;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.ToolTipText = "View and/or change DR Tool options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // bulkDatabaseUpdateToolStripMenuItem
            // 
            this.bulkDatabaseUpdateToolStripMenuItem.Name = "bulkDatabaseUpdateToolStripMenuItem";
            this.bulkDatabaseUpdateToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.bulkDatabaseUpdateToolStripMenuItem.Text = "Bulk Database Update";
            this.bulkDatabaseUpdateToolStripMenuItem.Visible = false;
            this.bulkDatabaseUpdateToolStripMenuItem.Click += new System.EventHandler(this.bulkDatabaseUpdateToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorCodesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // errorCodesToolStripMenuItem
            // 
            this.errorCodesToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.i_32_blue;
            this.errorCodesToolStripMenuItem.Name = "errorCodesToolStripMenuItem";
            this.errorCodesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.errorCodesToolStripMenuItem.Text = "Message Codes...";
            this.errorCodesToolStripMenuItem.Click += new System.EventHandler(this.errorCodesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::DRPlanningTool.Properties.Resources.help_32_blue;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExit,
            this.toolStripButtonLoadiServerProduction,
            this.toolStripButtonLoadiServerDevelopment,
            this.toolStripButtonCreateManagementPacks,
            this.toolStripButtonResetServerStates,
            this.toolStripButtonGetServerHealth});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1285, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // toolStripButtonExit
            // 
            this.toolStripButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExit.Image = global::DRPlanningTool.Properties.Resources.beta_general_stop_32;
            this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExit.Name = "toolStripButtonExit";
            this.toolStripButtonExit.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonExit.ToolTipText = "Exit";
            this.toolStripButtonExit.Click += new System.EventHandler(this.toolStripButtonExit_Click);
            // 
            // toolStripButtonLoadiServerProduction
            // 
            this.toolStripButtonLoadiServerProduction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLoadiServerProduction.Image = global::DRPlanningTool.Properties.Resources.i_32_green;
            this.toolStripButtonLoadiServerProduction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadiServerProduction.Name = "toolStripButtonLoadiServerProduction";
            this.toolStripButtonLoadiServerProduction.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonLoadiServerProduction.ToolTipText = "Load Dependency Data";
            this.toolStripButtonLoadiServerProduction.Click += new System.EventHandler(this.toolStripButtonLoadiServerProduction_Click);
            // 
            // toolStripButtonLoadiServerDevelopment
            // 
            this.toolStripButtonLoadiServerDevelopment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLoadiServerDevelopment.Image = global::DRPlanningTool.Properties.Resources.i_32_light_blue;
            this.toolStripButtonLoadiServerDevelopment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadiServerDevelopment.Name = "toolStripButtonLoadiServerDevelopment";
            this.toolStripButtonLoadiServerDevelopment.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonLoadiServerDevelopment.ToolTipText = "Load iServer (Development)";
            this.toolStripButtonLoadiServerDevelopment.Visible = false;
            // 
            // toolStripButtonCreateManagementPacks
            // 
            this.toolStripButtonCreateManagementPacks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCreateManagementPacks.Enabled = false;
            this.toolStripButtonCreateManagementPacks.Image = global::DRPlanningTool.Properties.Resources.Box_full_32;
            this.toolStripButtonCreateManagementPacks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreateManagementPacks.Name = "toolStripButtonCreateManagementPacks";
            this.toolStripButtonCreateManagementPacks.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonCreateManagementPacks.ToolTipText = "Create Management Packs";
            // 
            // toolStripButtonResetServerStates
            // 
            this.toolStripButtonResetServerStates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonResetServerStates.Enabled = false;
            this.toolStripButtonResetServerStates.Image = global::DRPlanningTool.Properties.Resources.Server2_green_32;
            this.toolStripButtonResetServerStates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResetServerStates.Name = "toolStripButtonResetServerStates";
            this.toolStripButtonResetServerStates.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonResetServerStates.ToolTipText = "Reset Server States";
            this.toolStripButtonResetServerStates.Visible = false;
            // 
            // toolStripButtonGetServerHealth
            // 
            this.toolStripButtonGetServerHealth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGetServerHealth.Enabled = false;
            this.toolStripButtonGetServerHealth.Image = global::DRPlanningTool.Properties.Resources.thermometer_32_blue;
            this.toolStripButtonGetServerHealth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGetServerHealth.Name = "toolStripButtonGetServerHealth";
            this.toolStripButtonGetServerHealth.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonGetServerHealth.ToolTipText = "Retrieve SCOM Server Health";
            this.toolStripButtonGetServerHealth.Visible = false;
            this.toolStripButtonGetServerHealth.Click += new System.EventHandler(this.toolStripButtonGetServerHealth_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 577);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1285, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // tmrUpdates
            // 
            this.tmrUpdates.Interval = 10000;
            this.tmrUpdates.Tick += new System.EventHandler(this.tmrUpdates_Tick);
            // 
            // frmMDIMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 599);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(296, 387);
            this.Name = "frmMDIMain";
            this.Text = "DR Planning Tool";
            this.Load += new System.EventHandler(this.frmMDIMain_Load);
            this.Shown += new System.EventHandler(this.frmMDIMain_Shown);
            this.Resize += new System.EventHandler(this.frmMDIMain_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceRunbookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverRunbookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sRMRunbookToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem DependencyMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recoveryEstimatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceFormsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BusinessApplicationImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runbookFormsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverFormsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetServerStatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retrieveSCOMServerHealthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findSystemCenterComponentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverSharesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem bulkDatabaseUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorCodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonExit;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadiServerProduction;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadiServerDevelopment;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreateManagementPacks;
        private System.Windows.Forms.ToolStripButton toolStripButtonResetServerStates;
        private System.Windows.Forms.ToolStripButton toolStripButtonGetServerHealth;
        private System.Windows.Forms.Timer tmrUpdates;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sRMRunbookToolStripMenuItem1;
        public System.Windows.Forms.StatusStrip statusStrip;
    }
}



