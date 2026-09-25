using Microsoft.AGL.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISA.Dependency;
using ISA.SystemCenter;

namespace DRPlanningTool
{
    public partial class frmServer : Form
    {

        #region Fields

        List<BusinessApplication> _BusinessApplicationRunbook = null;
        private bool _Loading = false;
        private Form _ParentForm = null;
        private BusinessApplication _SelectedBusinessApplication = null;
        private Server _Server = null;
        List<Server> _ServerRunbook = null;
        private ISA.Database.SQLServer _SQL = null;
        ListView _SelectedListview = null;

        #endregion

        #region Constructors

        public frmServer()
        {
            InitializeComponent();
        }

        public frmServer(Server Server)
        {
            InitializeComponent();

            _Server = Server;
        }

        public frmServer(Server Server, List<Server> ServerRunbook, List<BusinessApplication> BusinessApplicationRunbook, Form ParentForm, ISA.Database.SQLServer SQL)
        {
            InitializeComponent();

            _Server = Server;
            _ServerRunbook = ServerRunbook;
            _BusinessApplicationRunbook = BusinessApplicationRunbook;
            _ParentForm = ParentForm;
            _Loading = true;
            _SQL = SQL;
        }

        #endregion

        #region Event Handlers

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ServerUtilities SCOMUtilities = new ServerUtilities(Properties.Settings.Default.SystemCenter_SCOMServer, Properties.Settings.Default.SystemCenter_SCSMServer);
            string ServerHealth = SCOMUtilities.GetServerHealth(_Server.Name);

            switch (ServerHealth)
            {
                case "Error":
                    _Server.SetSystemHealthState(Global.HealthState.Error, "Loaded from SCOM data");
                    Debug.WriteLine(Server.Name + " Health state (Error) read by SCOM and set to Error.", "information");
                    break;
                case "Success":
                    _Server.SetSystemHealthState(Global.HealthState.OK, "Loaded from SCOM data");
                    Debug.WriteLine(Server.Name + " Health state (Success) read by SCOM and set to OK.", "information");
                    break;
                case "Uninitialized":
                    _Server.SetSystemHealthState(Global.HealthState.Degraded, "Loaded from SCOM data");
                    Debug.WriteLine(Server.Name + " Health state (Uninitialized) read by SCOM and set to Degraded.", "information");
                    break;
                case "Warning":
                    _Server.SetSystemHealthState(Global.HealthState.Degraded, "Loaded from SCOM data");
                    Debug.WriteLine(Server.Name + " Health state (Warning) read by SCOM and set to Degraded.", "information");
                    break;
                case "":
                    Debug.WriteLine("Could not set Health state for " + Server.Name, "error");
                    break;
            }

            //frmMain MainForm = (frmMain)_ParentForm;

            if (Main.MainForm == null)
            {
                Main.DetermineAllBusinessApplicationState(Main.DrawingsForm.lvwDrawings);
            }
            else
            {
                Main.DetermineAllBusinessApplicationState(Main.MainForm.lvwDrawings);
            }

        }

        private void btnOpenRecoveryTasks_Click(object sender, EventArgs e)
        {
            frmRecoveryTasks RecoveryTasks = new frmRecoveryTasks();

            RecoveryTasks.Tasks = _Server.RecoveryTasks;
            RecoveryTasks.Caption = "Recovery Tasks: " + _Server.Name;

            if (Main.MainForm == null)
            {
                RecoveryTasks.Show(Main.MDIForm);
            }
            else
            {
                RecoveryTasks.Show(this);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();

            this.Close();
        }

        private void btnShares_Click(object sender, EventArgs e)
        {
            frmShares SharesForm = new frmShares(_Server.Name);

            if (Main.MainForm == null)
            {
                SharesForm.Show(Main.MDIForm);
            }
            else
            {
                SharesForm.Show(this);
            }
        }

        private void btnShowDisks_Click(object sender, EventArgs e)
        {
            frmDisks DisksForm = new frmDisks(_Server);

            if (Main.MainForm == null)
            {
                DisksForm.Show(Main.MDIForm);
            }
            else
            {
                DisksForm.Show(this);
            }

            DisksForm = null;
        }

        private void chkInScope_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void createManagementPackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateMP(_SelectedBusinessApplication);
        }

        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.RemoveServerFormFromList(this);

            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.frmServerSize = this.Size;

                if (ISA.Helper.Properties.AllowSaveFormLocations)
                {
                    Properties.Settings.Default.frmServerLocation = this.Location;
                }
            }
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            PerformUpdate();
            UpdateState();

            btnApply.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            btnSave.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            //chkShowInRunbook.Enabled = ISA.Helper.Properties.iServerRW;
            chkVirtual.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;

            // If vCenter isn't enabled, remove the vCenter tab
            if (!ISA.Helper.Properties.vCenterFunctionsEnabled)
            {
                tabServer.TabPages.RemoveAt(2);
            }

            //_Loading = false;
        }

        private void frmServer_Move(object sender, EventArgs e)
        {
            if (!_Loading)
            {
                Properties.Settings.Default.frmServerLocation = this.Location;
            }
        }

        private void frmServer_ResizeEnd(object sender, EventArgs e)
        {
            if (!_Loading)
            {
                Properties.Settings.Default.frmServerSize = this.Size;
            }
        }

        private void frmServer_Shown(object sender, EventArgs e)
        {
            // Set location and size
            if (Properties.Settings.Default.frmServerLocation.X != 0 && Properties.Settings.Default.frmServerLocation.Y != 0)
            {
                this.Location = Properties.Settings.Default.frmServerLocation;
            }

            if (Properties.Settings.Default.frmServerSize.Width != 0 && Properties.Settings.Default.frmServerSize.Height != 0)
            {
                this.Size = Properties.Settings.Default.frmServerSize;
            }

            _Loading = false;
            this.Activate();
        }

        private void lvwDependentBusinessApplications_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvwDependentBusinessApplications.SelectedItems.Count > 0)
            {
                _SelectedBusinessApplication = (BusinessApplication)lvwDependentBusinessApplications.SelectedItems[0].Tag;

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (ISA.Helper.Properties.ManagementPackAuthor)
                    {
                        createManagementPackToolStripMenuItem.Enabled = true;
                    }
                    openToolStripMenuItem.Enabled = true;
                    RightClickMenu.Show(Cursor.Position);
                }
            }
        }

        private void lvwDependentBusinessApplications_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BusinessApplication Service = (BusinessApplication)lvwDependentBusinessApplications.SelectedItems[0].Tag;
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.CreateOrSelectBusinessApplicationForm(lvwDependentBusinessApplications.SelectedItems[0].Text, Service);
        }

        private void lvwProvidedBusinessApplications_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvwProvidedBusinessApplications.SelectedItems.Count > 0)
            {
                _SelectedBusinessApplication = (BusinessApplication)lvwProvidedBusinessApplications.SelectedItems[0].Tag;

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (ISA.Helper.Properties.ManagementPackAuthor)
                    {
                        createManagementPackToolStripMenuItem.Enabled = true;
                    }
                    openToolStripMenuItem.Enabled = true;
                    RightClickMenu.Show(Cursor.Position);
                }
            }
        }

        private void lvwProvidedBusinessApplications_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BusinessApplication Service = (BusinessApplication)lvwProvidedBusinessApplications.SelectedItems[0].Tag;
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.CreateOrSelectBusinessApplicationForm(lvwProvidedBusinessApplications.SelectedItems[0].Text, Service);
        }

        private void lvwRunbook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //frmMain MainForm = (frmMain)_ParentForm;
            Server Server = (Server)lvwRunbook.SelectedItems[0].Tag;
            Main.CreateOrSelectServerForm(ref Server);
        }

        private void picWarnings_Click(object sender, EventArgs e)
        {
            TabPage WarningsTab = tabServer.TabPages[5];

            tabServer.SelectedTab = WarningsTab;
        }

        private void picWarnings_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonActual_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonDegraded_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDegraded.Checked)
            {
                if (!_Loading)
                {
                    _Server.SystemStateIsEmulated = true;
                    _Server.SetSystemHealthState(Global.HealthState.Degraded, "Emulated");

                    DetermineDependentBusinessApplicationState();
                }
            }
        }

        private void radioButtonEmulated_CheckedChanged(object sender, EventArgs e)
        {
            grpEmulated.Enabled = radioButtonEmulated.Checked;

            if (!_Loading)
            {
                if (radioButtonEmulated.Checked)
                {
                    _Server.SystemStateIsEmulated = true;
                }
                else
                {
                    _Server.SystemStateIsEmulated = false;
                }
                DetermineDependentBusinessApplicationState();
            }
        }

        private void radioButtonError_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonError.Checked)
            {
                if (!_Loading)
                {
                    _Server.SystemStateIsEmulated = true;
                    _Server.SetSystemHealthState(Global.HealthState.Error, "Emulated");

                    DetermineDependentBusinessApplicationState();
                }
            }
        }

        private void radioButtonOK_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOK.Checked)
            {
                if (!_Loading)
                {
                    _Server.SystemStateIsEmulated = true;
                    _Server.SetSystemHealthState(Global.HealthState.OK, "Emulated");

                    DetermineDependentBusinessApplicationState();
                }
            }
        }

        private void ViewDependencyMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<BusinessApplication> Services = new List<BusinessApplication>();

            if (lvwProvidedBusinessApplications.Focused)
            {
                foreach (ListViewItem Item in lvwProvidedBusinessApplications.SelectedItems)
                {
                    Services.Add((BusinessApplication)Item.Tag);
                }

                //((frmMain)_ParentForm).CreateOrSelectDependencyMapForm((BusinessApplication)lvwProvidedBusinessApplications.SelectedItems[0].Tag, true);
            }

            if (lvwDependentBusinessApplications.Focused)
            {
                foreach (ListViewItem Item in lvwDependentBusinessApplications.SelectedItems)
                {
                    Services.Add((BusinessApplication)Item.Tag);
                }

                //((frmMain)_ParentForm).CreateOrSelectDependencyMapForm((BusinessApplication)lvwDependentBusinessApplications.SelectedItems[0].Tag, true);
            }

            if (lvwRunbook.Focused)
            {
                foreach (ListViewItem Item in lvwRunbook.SelectedItems)
                {
                    Services.Add((BusinessApplication)Item.Tag);
                }

                //((frmMain)_ParentForm).CreateOrSelectDependencyMapForm((BusinessApplication)lvwRunbook.SelectedItems[0].Tag, true);
            }

            Main.CreateOrSelectDependencyMapForm(Services, true, null, LayoutMethod.SugiyamaScheme, false);

        }

        #endregion

        #region Private Methods

        private void CreateMP(BusinessApplication Service)
        {
            bool Result = false;

            if (Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus || Service.Drawing.Status == Global.DrawingStatus.Approved)
            {
                Debug.WriteLine("..." + Service.Name + "...", "information");
                Result = Main.CreateManagementPackFromBusinessApplication(Service, false, Global.Environment.Production);
            }
            else
            {
                Debug.WriteLine("..." + Service.Name + " Drawing status is not Approved, so a Management Pack for this Service will not be created.", "information");
                Result = true;
            }

            if (Result)  // Success
            {

            }
            else         // Failure
            {
                Debug.WriteLine("A problem was encountered whilst updating or creating a Management Pack", "error");
            }
        }

        private void DetermineDependentBusinessApplicationState()
        {
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.DetermineAllBusinessApplicationState();

            UpdateImageAndStateDescription();

            if (_Server.SystemStateIsEmulated)
            {
                Main.SetEmulatedLabel("Emulated Data");
            }
        }

        private void SaveData()
        {
            Cursor.Current = Cursors.WaitCursor;

            //_Server.ShowInRunbook = chkShowInRunbook.Checked;
            _Server.Virtual = chkVirtual.Checked;

            _Server.Save(_SQL);

            DetermineDependentBusinessApplicationState();

            Cursor.Current = Cursors.Default;
        }

        private void UpdateImageAndStateDescription()
        {
            //picServer.BackgroundImage = imlLarge.Images[Global.GetServerStateImageIndex(_Server)];

            picServer.BackgroundImage = Global.GetServerImage(_Server, imlLarge);

            lblStateDescription.Text = _Server.SystemStateDescription;
        }

        private void UpdateState()
        {
            switch (_Server.SystemState)
            {
                case Global.HealthState.OK:
                    radioButtonOK.Checked = true;
                    break;
                case Global.HealthState.Degraded:
                    radioButtonDegraded.Checked = true;
                    break;
                case Global.HealthState.Error:
                    radioButtonError.Checked = true;
                    break;
            }

            UpdateImageAndStateDescription();
        }

        #endregion

        #region Public Methods

        public Server Server
        {
            get
            {
                return _Server;
            }
            set
            {
                _Server = value;
            }
        }

        public void PerformUpdate()
        {
            int RunbookPosition = 0;
            int ThisPosition = 0;

            Cursor.Current = Cursors.WaitCursor;

            Hints.AutoPopDelay = Properties.Settings.Default.HelpHintTimemS;

            lvwDependentBusinessApplications.BeginUpdate();
            lvwProvidedBusinessApplications.BeginUpdate();
            lvwRunbook.BeginUpdate();

            lvwProvidedBusinessApplications.Items.Clear();
            lvwDependentBusinessApplications.Items.Clear();
            lvwRunbook.Items.Clear();
            lstWarnings.Items.Clear();

            picWarnings.Visible = _Server.Warnings.Count > 0;

            foreach (string Message in _Server.Warnings)
            {
                lstWarnings.Items.Add(Message);
            }

            Server SelectedServer = _Server;

            lblStateDescription.Text = SelectedServer.SystemStateDescription;
            chkVirtual.Checked = SelectedServer.Virtual;
            chkAddToManagementPack.Checked = SelectedServer.AddToManagementPack;

            //btnShowDisks.Enabled = Properties.Settings.Default.EnablevCenterQueries;

            this.Text = "Server: " + SelectedServer.Name;
            txtStream.Text = Global.GetServerStreamName(SelectedServer);
            txtTier.Text = Global.GetTierName(SelectedServer.Tier);
            txtSite.Text = SelectedServer.PhysicalSite.Name; // SelectedServer.PhysicalSiteName
            txtID.Text = SelectedServer.ID.ToString();
            txtOperatingSystem.Text = SelectedServer.OperatingSystem;
            txtCPUCount.Text = SelectedServer.CPUCount.ToString();
            txtMemory.Text = SelectedServer.Memory.ToString();
            txtvCenterName.Text = SelectedServer.VCenterName;
            txtvCenterDescription.Text = SelectedServer.VCenterDescription;
            txtTotalAllocatedDiskspaceGB.Text = (SelectedServer.TotalAllocatedDiskGB).ToString("#,#.000");

            #region Populate Runbook position

            lvwRunbook.Items.Add("[Top of Runbook]");
            foreach (Server ThisServer in _ServerRunbook)
            {
                string Item = ThisServer.Name;
                ThisPosition++;

                ListViewItem NewItem = new ListViewItem(Item);
                NewItem.Tag = ThisServer;

                NewItem.ImageIndex = Global.GetServerStateImageIndex(ThisServer);

                lvwRunbook.Items.Add(NewItem);

                if (ThisServer.Name == SelectedServer.Name)
                {
                    RunbookPosition = ThisPosition;
                }
            }
            lvwRunbook.Items.Add("[Bottom of Runbook]");
            lvwRunbook.EnsureVisible(RunbookPosition + 1);
            lvwRunbook.Items[RunbookPosition].Selected = true;

            #endregion

            #region Populate Provided Business Applications

            foreach (Relationship ThisRelationship in _Server.Relationships)
            {
                ListViewItem Item = new ListViewItem();

                string RelationshipType = "";

                if (ThisRelationship.SystemMandatory)
                {
                    RelationshipType = "M";
                }
                else
                {
                    RelationshipType = "O";
                }

                if (ThisRelationship.ToType == Relationship.ComponentType.Server)
                {
                    BusinessApplication Service = ThisRelationship.FromBusinessApplication;

                    ListViewItem Item1 = new ListViewItem();
                    Item1.Text = Service.Name;
                    Item1.Name = Service.Name;
                    Item1.Tag = Service;

                    Item1.ImageIndex = Global.GetBusinessApplicationStateImageIndex(Service, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication);

                    string SubItem1 = RelationshipType;
                    string SubItem2 = Global.GetBusinessApplicationStreamName(Service.CalculatedStream);
                    string SubItem3 = Global.GetTierName(Service.DesiredTier);

                    Item1.SubItems.Add(SubItem1);
                    Item1.SubItems.Add(SubItem2);
                    Item1.SubItems.Add(SubItem3);

                    lvwProvidedBusinessApplications.Items.Add(Item1);
                }
            }

            #endregion

            #region Populate Dependent Business Applications

            foreach (BusinessApplication BusinessApplication in SelectedServer.DependentBusinessApplications)
            {
                ListViewItem Item1 = new ListViewItem();
                Item1.Text = BusinessApplication.Name;
                Item1.Name = BusinessApplication.Name;
                Item1.Tag = BusinessApplication;

                Item1.ImageIndex = Global.GetBusinessApplicationStateImageIndex(BusinessApplication, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication);

                string SubItem1 = Global.GetBusinessApplicationStreamName(BusinessApplication.CalculatedStream);
                string SubItem2 = Global.GetTierName(BusinessApplication.DesiredTier);

                Item1.SubItems.Add(SubItem1);
                Item1.SubItems.Add(SubItem2);

                if ((BusinessApplication.Drawing.Image == null && Properties.Settings.Default.ShowDrawingLessBusinessApplications) || (BusinessApplication.Drawing.Image != null))
                {
                    lvwDependentBusinessApplications.Items.Add(Item1);
                }
            }

            #endregion

            #region Details

            //txtRunbookOrder.Text = SelectedServer.RunbookOrder.ToString();
            txtRecoveryTime.Text = ISA.Helper.Methods.FormattedTime(SelectedServer.RecoveryTime);

            #endregion

            #region High Availability

            tvwHighAvailability.BeginUpdate();

            foreach (BusinessApplication Service in SelectedServer.HAApplications)
            {

                TreeNode ServiceNode = new TreeNode();

                ServiceNode.Text = Service.Name;
                ServiceNode.Tag = Service;
                ServiceNode.ImageIndex = Global.GetBusinessApplicationStateImageIndex(Service, false);
                ServiceNode.SelectedImageIndex = Global.GetBusinessApplicationStateImageIndex(Service, false);

                tvwHighAvailability.Nodes.Add(ServiceNode);

                foreach (Server HAServer in Service.HAServers)
                {
                    TreeNode ServerNode = new TreeNode();

                    ServerNode.Text = HAServer.Name;
                    ServerNode.Tag = HAServer;

                    if (HAServer == SelectedServer)
                    {
                        ServerNode.ImageIndex = 5;
                        ServerNode.SelectedImageIndex = 5;
                    }
                    else
                    {
                        ServerNode.ImageIndex = Global.GetServerStateImageIndex(HAServer);
                        ServerNode.SelectedImageIndex = Global.GetServerStateImageIndex(HAServer);
                    }

                    ServiceNode.Nodes.Add(ServerNode);
                }
            }

            tvwHighAvailability.Sort();
            tvwHighAvailability.ExpandAll();
            tvwHighAvailability.EndUpdate();

            #endregion

            #region Tooltips

            Hints.SetToolTip(lvwProvidedBusinessApplications, SelectedServer.Name + " must be available for all of these Component Services to function properly.");
            Hints.SetToolTip(lvwDependentBusinessApplications, "If " + SelectedServer.Name + " is degraded or in an error state, these Services will be negatively affected.");
            Hints.SetToolTip(lvwRunbook, "This shows where in the Server Runbook " + SelectedServer.Name + " is required.");

            if (SelectedServer.SystemStateIsEmulated)
            {
                Hints.SetToolTip(picServer, SelectedServer.Name + " is in a Emulated " + SelectedServer.SystemState + " state.");
            }
            else
            {
                Hints.SetToolTip(picServer, SelectedServer.Name + " is in an " + SelectedServer.SystemState + " state.");
            }

            Hints.SetToolTip(txtStream, "Streams:\nA: N/A to Servers\nB: Recovered using SRM\nC: Recovered from a Backup Restore\nD: Reserved for future use\nE: Recovery provided by an external party\nX: (None) No Recovery required / possible");
            Hints.SetToolTip(txtTier, "Tiers:\n0: Provides one or more ICT Foundation services\n1-4: Provides one or more Departmental services\nX: Tier cannot be calculated");
            Hints.SetToolTip(radioButtonActual, "The displayed state of " + SelectedServer.Name + " is as determined, utilising calculated dependency values.");
            Hints.SetToolTip(radioButtonEmulated, "If selected, the displayed state of " + SelectedServer.Name + " (and subsequently, Dependent Services) will be emulated.");
            Hints.SetToolTip(grpEmulated, "If selected, the displayed state of " + SelectedServer.Name + " (and subsequently, Dependent Services) will be emulated.");

            #endregion

            if (SelectedServer.SystemStateIsEmulated)
            {
                radioButtonEmulated.Checked = true;
            }

            if (SelectedServer.HARelationships.Count > 0)
            {
                tabServer.TabPages[4].Enabled = true;
            }
            else
            {
                tabServer.TabPages[4].Enabled = false;
            }

            UpdateState();

            lvwDependentBusinessApplications.EndUpdate();
            lvwProvidedBusinessApplications.EndUpdate();
            lvwRunbook.EndUpdate();

            Cursor.Current = Cursors.Default;
        }

        #endregion

        private void CopyNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder Builder = new StringBuilder();

            if (_SelectedListview.SelectedItems.Count > 0)
            {
                // Copy the selected names

                foreach (ListViewItem Item in _SelectedListview.SelectedItems)
                {
                    Builder.AppendLine(Item.Text);
                }
            }
            else
            {
                // Copy all names

                foreach (ListViewItem Item in _SelectedListview.Items)
                {
                    Builder.AppendLine(Item.Text);
                }
            }

            if (Builder.Length > 0)
            {
                Clipboard.SetText(Builder.ToString());

                MessageBox.Show("Names copied to clipboard", "Message");
            }
        }

        private void lvwRunbook_MouseDown(object sender, MouseEventArgs e)
        {
            _SelectedListview = lvwRunbook;

            if (_SelectedListview.Items.Count > 0)
            {
                CopyNamesToolStripMenuItem.Enabled = true;
            }
        }

        private void lvwProvidedBusinessApplications_MouseDown(object sender, MouseEventArgs e)
        {
            _SelectedListview = lvwProvidedBusinessApplications;

            if (_SelectedListview.Items.Count > 0)
            {
                CopyNamesToolStripMenuItem.Enabled = true;
            }
        }

        private void lvwDependentBusinessApplications_MouseDown(object sender, MouseEventArgs e)
        {
            _SelectedListview = lvwDependentBusinessApplications;

            if (_SelectedListview.Items.Count > 0)
            {
                CopyNamesToolStripMenuItem.Enabled = true;
            }
        }

    }
}
