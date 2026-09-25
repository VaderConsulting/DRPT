using ISA.SystemCenter;
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

namespace DRPlanningTool
{
    public partial class frmBusinessApplication : Form
    {

        #region Fields

        private BusinessApplication _BusinessApplication = null;
        List<BusinessApplication> _BusinessApplicationRunbook = null;
        private string _DrawingName = "";
        private bool _Loading = false;
        private Form _ParentForm = null;
        private BusinessApplication _SelectedBusinessApplication = null;
        private ISA.Database.SQLServer _SQL = null;
        private List<Relationship> _AllRelationships = new List<Relationship>();
        private ListView _SelectedListview = null;

        #endregion

        #region Properties

        public BusinessApplication BusinessApplication
        {
            get
            {
                return _BusinessApplication;
            }
            set
            {
                _BusinessApplication = value;
            }
        }

        public string DrawingName
        {
            get
            {
                return _DrawingName;
            }
            set
            {
                _DrawingName = value;
            }
        }

        #endregion

        #region Constructors

        public frmBusinessApplication()
        {
            InitializeComponent();

            _Loading = true;
        }

        public frmBusinessApplication(string DrawingName, BusinessApplication BusinessApplication, List<BusinessApplication> BusinessApplicationRunbook, List<Relationship> Relationships, Form ParentForm, ISA.Database.SQLServer SQL)
        {
            InitializeComponent();

            _BusinessApplication = BusinessApplication;
            _BusinessApplicationRunbook = BusinessApplicationRunbook;
            _ParentForm = ParentForm;
            _Loading = true;
            _DrawingName = DrawingName;
            _AllRelationships = Relationships;
            _SQL = SQL;

            this.Text = "Unknown Business Application (" + _DrawingName + ")";
        }

        #endregion

        #region Event Handlers

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnApplyDrawing_Click(object sender, EventArgs e)
        {
            SaveDrawingDetails();
        }

        private void btnOpenRecoveryTasks_Click(object sender, EventArgs e)
        {
            frmRecoveryTasks RecoveryTasks = new frmRecoveryTasks();

            RecoveryTasks.Tasks = _BusinessApplication.RecoveryTasks;
            RecoveryTasks.Caption = "Singular Recovery Tasks: " + _BusinessApplication.Name;

            if (Main.MainForm == null)
            {
                RecoveryTasks.Show(Main.MDIForm);
            }
            else
            {
                RecoveryTasks.Show(Main.MainForm);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();

            this.Close();
        }

        private void btnSaveDrawing_Click(object sender, EventArgs e)
        {
            SaveDrawingDetails();

            this.Close();
        }

        private void btnUseBusinessApplicationName_Click(object sender, EventArgs e)
        {
            txtDisplayName.Text = _BusinessApplication.Name;
        }

        private void BusinessApplicationState_Changed(object Sender, ISA.Dependency.StateChangeEventArgs e)
        {
            Console.WriteLine("The Business Application State has changed");
        }

        private void cmbDrawingStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_BusinessApplication.Drawing != null)
            {
                if (!_Loading)
                {
                    switch (cmbDrawingStatus.SelectedIndex)
                    {
                        case 0:
                            _BusinessApplication.Drawing.Status = Global.DrawingStatus.Not_Applicable;
                            break;
                        case 1:
                            _BusinessApplication.Drawing.Status = Global.DrawingStatus.Draft;
                            break;
                        case 2:
                            _BusinessApplication.Drawing.Status = Global.DrawingStatus.Pending_Review;
                            break;
                        case 3:
                            _BusinessApplication.Drawing.Status = Global.DrawingStatus.Approved;
                            break;
                    }

                    UpdateDrawingState();
                }
            }
        }

        private void createManagementPackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateMP(_SelectedBusinessApplication, false);
        }

        private void frmBusinessApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.RemoveBusinessApplicationFormFromList(this);

            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.frmBusinessApplicationSize = this.Size;

                if (ISA.Helper.Properties.AllowSaveFormLocations)
                {
                    Properties.Settings.Default.frmBusinessApplicationLocation = this.Location;
                }
            }
        }

        private void frmBusinessApplication_Load(object sender, EventArgs e)
        {
            //if (_BusinessApplication != null)
            //{
            //    BusinessApplication.SystemStateChanged += new Finance.Dependency.ComponentBase.SystemStateChangeEventHandler(BusinessApplicationState_Changed);
            //}

            cmbDrawingStatus.Items.Add("Not applicable");
            cmbDrawingStatus.Items.Add("Draft");
            cmbDrawingStatus.Items.Add("Pending");
            cmbDrawingStatus.Items.Add("Approved");

            // Hide Management Packs tab for now
            tabBusinessApplication.TabPages.RemoveAt(4);

            PerformUpdate();

            btnApply.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            btnSave.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            cmbDrawingStatus.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            chkShowInCatalogue.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            //chkShowInRunbook.Enabled = ISA.Helper.Properties.iServerRW;
            chkSubService.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            chkInScope.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            btnUseBusinessApplicationName.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            btnApplyDrawing.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            btnSaveDrawing.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            txtDisplayName.Enabled = ISA.Helper.Properties.DependencyDatabaseRW;
            btnLoad.Enabled = ISA.Helper.Properties.SystemCenterFunctionsEnabled;
            btnCreateMP.Enabled = (ISA.Helper.Properties.ManagementPackAuthor && (Properties.Settings.Default.ImportSCOMManagementPacks || Properties.Settings.Default.ImportSCSMManagementPacks));

            //_Loading = false;
        }

        private void lvwBusinessDependentServices_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvwDependentBusinessApplications.SelectedItems.Count > 0)
            {
                _SelectedBusinessApplication = (BusinessApplication)lvwDependentBusinessApplications.SelectedItems[0].Tag;

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    //if (ISA.Helper.Properties.ManagementPackAuthor)
                    //{
                    //    createManagementPackToolStripMenuItem.Enabled = true;
                    //}
                    openToolStripMenuItem.Enabled = true;
                    DrawingContextMenu.Show(Cursor.Position);
                }
            }
        }

        private void lvwBusinessApplicationComponentServices_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvwBusinessApplicationComponentServices.SelectedItems.Count > 0)
            {
                _SelectedBusinessApplication = (BusinessApplication)lvwBusinessApplicationComponentServices.SelectedItems[0].Tag;

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    //if (ISA.Helper.Properties.ManagementPackAuthor)
                    //{
                    //    createManagementPackToolStripMenuItem.Enabled = true;
                    //}
                    openToolStripMenuItem.Enabled = true;
                    DrawingContextMenu.Show(Cursor.Position);
                }
            }
        }

        private void lvwBusinessApplicationComponentServices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvwRunbook_MouseDown(object sender, MouseEventArgs e)
        {
            _SelectedListview = lvwRunbook;

            if (ISA.Helper.Properties.ManagementPackAuthor)
            {
                createManagementPackToolStripMenuItem.Enabled = true;
            }

            if (_SelectedListview.Items.Count > 0)
            {
                CopyNamesToolStripMenuItem.Enabled = true;
            }
        }

        private void lvwBusinessApplicationComponentServices_MouseDown(object sender, MouseEventArgs e)
        {
            _SelectedListview = lvwBusinessApplicationComponentServices;

            if (ISA.Helper.Properties.ManagementPackAuthor)
            {
                createManagementPackToolStripMenuItem.Enabled = true;
            }

            if (_SelectedListview.Items.Count > 0)
            {
                CopyNamesToolStripMenuItem.Enabled = true;
            }
        }

        private void lvwBusinessApplicationComponentServers_MouseDown(object sender, MouseEventArgs e)
        {
            _SelectedListview = lvwBusinessApplicationComponentServers;
            createManagementPackToolStripMenuItem.Enabled = false;

            if (_SelectedListview.Items.Count > 0)
            {
                CopyNamesToolStripMenuItem.Enabled = true;
            }
        }

        private void lvwDependentBusinessApplications_MouseDown(object sender, MouseEventArgs e)
        {
            _SelectedListview = lvwDependentBusinessApplications;

            if (ISA.Helper.Properties.ManagementPackAuthor)
            {
                createManagementPackToolStripMenuItem.Enabled = true;
            }

            if (_SelectedListview.Items.Count > 0)
            {
                CopyNamesToolStripMenuItem.Enabled = true;
            }
        }

        private void lvwAffectedServers_MouseDown(object sender, MouseEventArgs e)
        {
            _SelectedListview = lvwAffectedServers;
            createManagementPackToolStripMenuItem.Enabled = false;

            if (_SelectedListview.Items.Count > 0)
            {
                CopyNamesToolStripMenuItem.Enabled = true;
            }
        }

        private void lvwDependentServers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Server Server = (Server)lvwAffectedServers.SelectedItems[0].Tag;
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.CreateOrSelectServerForm(ref Server);
        }

        private void lvwDependentServices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BusinessApplication BusinessApplication = (BusinessApplication)lvwDependentBusinessApplications.SelectedItems[0].Tag;
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.CreateOrSelectBusinessApplicationForm(lvwDependentBusinessApplications.SelectedItems[0].Text, BusinessApplication);
        }

        private void lvwRunbook_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvwRunbook.SelectedItems.Count > 0)
            {
                _SelectedBusinessApplication = (BusinessApplication)lvwRunbook.SelectedItems[0].Tag;

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    //if (ISA.Helper.Properties.ManagementPackAuthor)
                    //{
                    //    createManagementPackToolStripMenuItem.Enabled = true;
                    //}
                    openToolStripMenuItem.Enabled = true;
                    DrawingContextMenu.Show(Cursor.Position);
                }
            }
        }

        private void lvwRunbook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BusinessApplication BusinessApplication = (BusinessApplication)lvwRunbook.SelectedItems[0].Tag;
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.CreateOrSelectBusinessApplicationForm(lvwRunbook.SelectedItems[0].Text, BusinessApplication);
        }

        private void lvwServiceComponentServers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Server Server = (Server)lvwBusinessApplicationComponentServers.SelectedItems[0].Tag;

            Main.CreateOrSelectServerForm(ref Server);
            //frmMain MainForm = (frmMain)_ParentForm;

            //MainForm.CreateOrSelectServerForm(ref Server);
        }

        private void lvwServiceComponentServices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BusinessApplication BusinessApplication = (BusinessApplication)lvwBusinessApplicationComponentServices.SelectedItems[0].Tag;
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.CreateOrSelectBusinessApplicationForm(lvwBusinessApplicationComponentServices.SelectedItems[0].Text, BusinessApplication);
        }

        private void picDrawingPreview_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //frmMain ParentForm = (frmMain)_ParentForm;

            //ParentForm.CreateOrSelectBusinessApplicationImageForm(_BusinessApplication);

            Main.CreateOrSelectBusinessApplicationImageForm(_BusinessApplication);
        }

        private void radioButtonActual_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonActual.Checked && !_Loading)
            {
                _BusinessApplication.SystemStateIsEmulated = false;
                _BusinessApplication.StateSetAtApplicationLevel = false;

                //UpdateImageAndStateDescription();

                DetermineDependentBusinessApplicationState();
            }
        }

        private void radioButtonDegraded_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDegraded.Checked && !_Loading)
            {
                _BusinessApplication.SystemStateIsEmulated = true;
                _BusinessApplication.StateSetAtApplicationLevel = true;
                _BusinessApplication.SetSystemHealthState(Global.HealthState.Degraded, "Emulated");

                //UpdateImageAndStateDescription();

                _Loading = true;
                DetermineDependentBusinessApplicationState();
                _Loading = false;
            }
        }

        private void radioButtonEmulated_CheckedChanged(object sender, EventArgs e)
        {
            grpEmulated.Enabled = radioButtonEmulated.Checked;

            if (radioButtonEmulated.Checked && !_Loading)
            {
                _BusinessApplication.SystemStateIsEmulated = true;
                _BusinessApplication.StateSetAtApplicationLevel = true;
                _BusinessApplication.SetSystemHealthState(Global.HealthState.OK, "Emulated");

                //UpdateImageAndStateDescription();
                DetermineDependentBusinessApplicationState();
            }
        }

        private void radioButtonError_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonError.Checked && !_Loading)
            {
                _BusinessApplication.SystemStateIsEmulated = true;
                _BusinessApplication.StateSetAtApplicationLevel = true;
                _BusinessApplication.SetSystemHealthState(Global.HealthState.Error, "Emulated");

                //UpdateImageAndStateDescription();

                _Loading = true;
                DetermineDependentBusinessApplicationState();
                _Loading = false;
            }
        }

        private void radioButtonOK_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOK.Checked && !_Loading)
            {
                _BusinessApplication.SystemStateIsEmulated = true;
                _BusinessApplication.StateSetAtApplicationLevel = true;
                _BusinessApplication.SetSystemHealthState(Global.HealthState.OK, "Emulated");

                //UpdateImageAndStateDescription();

                _Loading = true;
                DetermineDependentBusinessApplicationState();
                _Loading = false;
            }
        }

        private void chkInScope_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void frmBusinessApplication_Shown(object sender, EventArgs e)
        {
            // Set location and size
            if (Properties.Settings.Default.frmBusinessApplicationLocation.X != 0 && Properties.Settings.Default.frmBusinessApplicationLocation.Y != 0)
            {
                //this.Location = new Point(Properties.Settings.Default.frmBusinessApplicationLocation.X + Properties.Settings.Default.FormOpenXOffset, Properties.Settings.Default.frmBusinessApplicationLocation.Y + Properties.Settings.Default.FormOpenYOffset);
                this.Location = Properties.Settings.Default.frmBusinessApplicationLocation;
            }

            if (Properties.Settings.Default.frmBusinessApplicationSize.Width != 0 && Properties.Settings.Default.frmBusinessApplicationSize.Height != 0)
            {
                this.Size = Properties.Settings.Default.frmBusinessApplicationSize;
            }

            _Loading = false;
            this.Activate();
        }

        private void frmBusinessApplication_ResizeEnd(object sender, EventArgs e)
        {
            if (!_Loading)
            {
                Properties.Settings.Default.frmBusinessApplicationSize = this.Size;
            }
        }

        private void frmBusinessApplication_Move(object sender, EventArgs e)
        {
            if (!_Loading)
            {
                Properties.Settings.Default.frmBusinessApplicationLocation = this.Location;
            }
        }

        private void ViewDependencyMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<BusinessApplication> Services = new List<BusinessApplication>();

            if (lvwBusinessApplicationComponentServices.Focused)
            {
                foreach (ListViewItem Item in lvwBusinessApplicationComponentServices.SelectedItems)
                {
                    Services.Add((BusinessApplication)Item.Tag);
                }

                //((frmMain)_ParentForm).CreateOrSelectDependencyMapForm((BusinessApplication)lvwBusinessApplicationComponentServices.SelectedItems[0].Tag, true);
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

            if (lvwBusinessApplicationComponentServers.Focused)
            {
                foreach (ListViewItem Item in lvwBusinessApplicationComponentServers.SelectedItems)
                {
                    foreach (BusinessApplication Service in ((Server)Item.Tag).ProvidedBusinessApplications)
                    {
                        Services.Add(Service);
                    }
                }
            }

            if (lvwAffectedServers.Focused)
            {
                foreach (ListViewItem Item in lvwAffectedServers.SelectedItems)
                {
                    foreach (BusinessApplication Service in ((Server)Item.Tag).ProvidedBusinessApplications)
                    {
                        Services.Add(Service);
                    }
                }
            }

            Main.CreateOrSelectDependencyMapForm(Services, true, Services, LayoutMethod.SugiyamaScheme, false);

        }

        private void picWarnings_Click(object sender, EventArgs e)
        {
            int Index = tabBusinessApplication.TabPages.Count - 1;
            TabPage WarningsTab = tabBusinessApplication.TabPages[Index];

            tabBusinessApplication.SelectedTab = WarningsTab;
        }

        private void picWarnings_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void lvwBusinessApplicationComponentServers_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvwBusinessApplicationComponentServers.SelectedItems.Count > 0)
            {
                //_SelectedBusinessApplication = (BusinessApplication)lvwBusinessApplicationComponentServers.SelectedItems[0].Tag;

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    openToolStripMenuItem.Enabled = true;
                    DrawingContextMenu.Show(Cursor.Position);
                }
            }
        }

        private void lvwAffectedServers_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvwAffectedServers.SelectedItems.Count > 0)
            {
                //_SelectedBusinessApplication = (BusinessApplication)lvwBusinessApplicationComponentServers.SelectedItems[0].Tag;

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    openToolStripMenuItem.Enabled = true;
                    DrawingContextMenu.Show(Cursor.Position);
                }
            }
        }

        private void btnCreateMP_Click(object sender, EventArgs e)
        {
            CreateMP(_BusinessApplication, false);
        }

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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusinessApplication BusinessApplication = null;
            Server Server = null;
            String BusinessApplicationName = "";
            String ServerName = "";

            if (lvwBusinessApplicationComponentServices.Focused)
            {
                BusinessApplication = (BusinessApplication)lvwBusinessApplicationComponentServices.SelectedItems[0].Tag;
                BusinessApplicationName = lvwBusinessApplicationComponentServices.SelectedItems[0].Text;
            }
            else if (lvwRunbook.Focused)
            {
                BusinessApplication = (BusinessApplication)lvwRunbook.SelectedItems[0].Tag;
                BusinessApplicationName = lvwRunbook.SelectedItems[0].Text;
            }
            else if (lvwDependentBusinessApplications.Focused)
            {
                BusinessApplication = (BusinessApplication)lvwDependentBusinessApplications.SelectedItems[0].Tag;
                BusinessApplicationName = lvwDependentBusinessApplications.SelectedItems[0].Text;
            }
            else if (lvwBusinessApplicationComponentServers.Focused)
            {
                Server = (Server)lvwBusinessApplicationComponentServers.SelectedItems[0].Tag;
                ServerName = lvwBusinessApplicationComponentServers.SelectedItems[0].Text;
            }
            else if (lvwAffectedServers.Focused)
            {
                Server = (Server)lvwAffectedServers.SelectedItems[0].Tag;
                ServerName = lvwAffectedServers.SelectedItems[0].Text;
            }

            if (BusinessApplication != null)
            {
                //frmMain MainForm = (frmMain)_ParentForm;

                Main.CreateOrSelectBusinessApplicationForm(BusinessApplicationName, BusinessApplication);
            }
            else if (Server != null)
            {
                //frmMain MainForm = (frmMain)_ParentForm;

                Main.CreateOrSelectServerForm(ref Server);
            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //bool Result = false;

            //if (_SelectedListview != null)
            //{
            //    foreach (ListViewItem Item in _SelectedListview.SelectedItems)
            //    {
            //        BusinessApplication Service = (BusinessApplication)Item.Tag;

            //        frmMain MainForm = (frmMain)_ParentForm;

            //        if (Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus)
            //        {
            //            Debug.WriteLine("..." + Service.Name + "...", "information");
            //            Result = MainForm.CreateManagementPackFromBusinessApplication(Service, false, Global.Environment.Production);
            //        }
            //        else if (Service.Drawing != null && Service.Drawing.Status == Global.DrawingStatus.Approved)
            //        {
            //            Debug.WriteLine("..." + Service.Name + "...", "information");
            //            Result = MainForm.CreateManagementPackFromBusinessApplication(Service, false, Global.Environment.Production);
            //        }
            //        else
            //        {
            //            Debug.WriteLine("..." + Service.Name + " Drawing status is not Approved, so a Management Pack for this Service will not be created.", "information");
            //            Result = true;
            //        }

            //        if (Result)  // Success
            //        {

            //        }
            //        else         // Failure
            //        {
            //            Debug.WriteLine("A problem was encountered whilst updating or creating a Management Pack", "error");
            //        }
            //    }

            //    Debug.WriteLine("Management Pack creation request complete", "information");
            //    Debug.WriteLine("======= " + DateTime.Now.ToString() + " ================================================================\n", "information");
            //}
        }

        private void createStubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //bool Result = false;

            //if (_SelectedListview != null)
            //{
            //    foreach (ListViewItem Item in _SelectedListview.SelectedItems)
            //    {
            //        BusinessApplication Service = (BusinessApplication)Item.Tag;

            //        frmMain MainForm = (frmMain)_ParentForm;

            //        if (Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus)
            //        {
            //            Debug.WriteLine("..." + Service.Name + "...", "information");
            //            Result = MainForm.CreateManagementPackFromBusinessApplication(Service, true, Global.Environment.Production);
            //        }
            //        else if (Service.Drawing != null && Service.Drawing.Status == Global.DrawingStatus.Approved)
            //        {
            //            Debug.WriteLine("..." + Service.Name + "...", "information");
            //            Result = MainForm.CreateManagementPackFromBusinessApplication(Service, true, Global.Environment.Production);
            //        }
            //        else
            //        {
            //            Debug.WriteLine("..." + Service.Name + " Drawing status is not Approved, so a Management Pack for this Service will not be created.", "information");
            //            Result = true;
            //        }

            //        if (Result)  // Success
            //        {

            //        }
            //        else         // Failure
            //        {
            //            Debug.WriteLine("A problem was encountered whilst updating or creating a Management Pack", "error");
            //        }
            //    }

            //    Debug.WriteLine("Management Pack stub creation request complete", "information");
            //    Debug.WriteLine("======= " + DateTime.Now.ToString() + " ================================================================\n", "information");
            //}
        }

        private void productionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(false, Global.Environment.Production);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(false, Global.Environment.Test);
        }

        private void developmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(false, Global.Environment.Development);
        }

        private void productionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(true, Global.Environment.Production);
        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(true, Global.Environment.Test);
        }

        private void developmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(true, Global.Environment.Development);
        }

        #endregion

        #region Private Methods

        private void ClearApplicationDetails()
        {
            lvwBusinessApplicationComponentServers.Items.Clear();
            lvwBusinessApplicationComponentServices.Items.Clear();
            lvwDependentBusinessApplications.Items.Clear();
            lvwAffectedServers.Items.Clear();
            lvwRunbook.Items.Clear();
            picService.BackgroundImage = null;
            txtStream.Text = "";
            txtDesiredTier.Text = "";
            txtActualTier.Text = "";
            txtID.Text = "";
            txtLastUpdate.Text = "";
            txtDrawingNotes.Text = "";
            txtComment1.Text = "";
            this.Text = "Unknown Business Application (" + _DrawingName + ")";
            lblStateDescription.Text = "Focused Business Application not set.  Update Drawing.";
            lblInScopeOverride.Visible = false;
            txtRecoveryTime.Text = "";
            cmbDrawingStatus.Enabled = false;
            chkShowInCatalogue.Checked = false;
            chkInScope.Checked = false;
            chkSubService.Checked = false;
            chkHA.Checked = false;
            picWarnings.Visible = false;
            txtComment1.Enabled = false;
            lstWarnings.Items.Clear();
            picImageState.Image = null;
            lblMPVersion.Text = "0.0.0.0";
            txtWorkInstructionName.Text = "";
        }

        private bool CreateMP(BusinessApplication Service, bool CreateAsStub)
        {
            bool Result = false;

            if (Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus || Service.Drawing.Status == Global.DrawingStatus.Approved)
            {
                Debug.WriteLine("..." + Service.Name + "...", "information");
                Result = Main.CreateManagementPackFromBusinessApplication(Service, CreateAsStub, Global.Environment.Production);
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
                Debug.WriteLine("[INF1119] A problem was encountered whilst updating or creating a Management Pack", "error");
            }

            Debug.WriteLine("Management Pack creation request complete", "information");
            Debug.WriteLine("======= " + DateTime.Now.ToString() + " =================================================\n", "information");

            return Result;
        }

        private List<BusinessApplication> DependentBusinessApplicationList(BusinessApplication Service, List<BusinessApplication> ExistingList)
        {
            List<BusinessApplication> DependentServiceList = new List<BusinessApplication>();

            // The first time into this function, ExistingList will be null
            if (ExistingList == null)
            {
                ExistingList = new List<BusinessApplication>();
            }

            foreach (BusinessApplication DependentService in Service.DependentBusinessApplications)
            {
                if (!ExistingList.Contains(DependentService))
                {
                    ExistingList.Add(DependentService);

                    if (DependentService == Service)
                    {
                        Main.AddException("Circular dependency found (" + Service.Name + ")");
                    }
                    else
                    {
                        if (DependentService.DependentBusinessApplications.Count > 0)
                        {
                            DependentServiceList = DependentBusinessApplicationList(DependentService, ExistingList);
                        }
                    }

                    foreach (BusinessApplication AdditionalService in DependentServiceList)
                    {
                        if (!ExistingList.Contains(AdditionalService))
                        {
                            ExistingList.Add(AdditionalService);
                        }
                    }
                }
            }

            return ExistingList;
        }

        private void DetermineDependentBusinessApplicationState()
        {
            //frmMain MainForm = (frmMain)_ParentForm;

            if (Main.MainForm == null)
            {
                Main.DetermineAllBusinessApplicationState(Main.DrawingsForm.lvwDrawings);
            }
            else
            {
                Main.DetermineAllBusinessApplicationState(Main.MainForm.lvwDrawings);
            }

            UpdateImageAndStateDescription();
        }

        private void SaveData()
        {
            Cursor.Current = Cursors.WaitCursor;

            _BusinessApplication.SubApplication = chkSubService.Checked;

            _BusinessApplication.ShowInServiceCatalogue = chkShowInCatalogue.Checked;
            _BusinessApplication.DisplayName = txtDisplayName.Text.Trim();
            _BusinessApplication.Description = txtDescription.Text.Trim();
            _BusinessApplication.Comment1 = txtComment1.Text.Trim();
            _BusinessApplication.WorkInstructionName = txtWorkInstructionName.Text;

            if (chkInScope.Checked != _BusinessApplication.InScope)
            {
                #region If InScope checkbox has changed

                if (!chkInScope.Checked) // Setting the Service to InScope: No
                {
                    #region With Dependents

                    if (_BusinessApplication.DependentBusinessApplications.Count > 0)
                    {
                        StringBuilder Message = new StringBuilder();

                        Message.AppendLine("This will affect runbook order calculation for all Dependent services, including Dependents of those.");
                        Message.AppendLine("You have 3 choices...");
                        Message.AppendLine("1. Choose 'Yes' to apply this to " + _BusinessApplication.Name + " only,");
                        Message.AppendLine("2. Choose 'No' to also apply to all Dependent Services,");
                        Message.AppendLine("3. Choose 'Cancel' to not apply this change.");
                        Message.AppendLine();
                        Message.AppendLine("Dependent Services:");

                        List<BusinessApplication> Dependents = DependentBusinessApplicationList(_BusinessApplication, null);

                        // Add each Dependent name to the dialogue box
                        foreach (BusinessApplication Service in Dependents)
                        {
                            Message.AppendLine(Service.Name);
                        }

                        DialogResult Result = MessageBox.Show(Message.ToString(), "Confirmation required", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        switch (Result)
                        {
                            case System.Windows.Forms.DialogResult.Yes:
                                // Set the Scope for this service only
                                SetScope(BusinessApplication, chkInScope.Checked, false);

                                if (Main.MainForm == null)
                                {
                                    Main.DetermineAllBusinessApplicationInFocus(Main.DrawingsForm.lvwDrawings);
                                }
                                else
                                {
                                    Main.DetermineAllBusinessApplicationInFocus(Main.MainForm.lvwDrawings);
                                }

                                Main.AddException("[INF1110] Reload necessary due to 'In Scope' change");

                                break;
                            case System.Windows.Forms.DialogResult.No:
                                // Set the Scope for this service and all Dependents
                                SetScope(BusinessApplication, chkInScope.Checked, true);

                                if (Main.MainForm == null)
                                {
                                    Main.DetermineAllBusinessApplicationInFocus(Main.DrawingsForm.lvwDrawings);
                                }
                                else
                                {
                                    Main.DetermineAllBusinessApplicationInFocus(Main.MainForm.lvwDrawings);
                                }

                                Main.AddException("[INF1111] Reload necessary due to 'In Scope' change");
                                break;
                            case System.Windows.Forms.DialogResult.Cancel:
                                // Don't apply the change
                                chkInScope.Checked = BusinessApplication.InScope;
                                break;
                        }
                    }

                    #endregion

                    #region No Dependents

                    else
                    {
                        SetScope(BusinessApplication, chkInScope.Checked, true);

                        if (Main.MainForm == null)
                        {
                            Main.DetermineAllBusinessApplicationInFocus(Main.DrawingsForm.lvwDrawings);
                        }
                        else
                        {
                            Main.DetermineAllBusinessApplicationInFocus(Main.MainForm.lvwDrawings);
                        }

                        Main.AddException("[INF1112] Reload necessary due to 'In Scope' change");
                    }

                    #endregion
                }
                else
                {
                    #region With Dependents

                    if (BusinessApplication.DependentBusinessApplications.Count > 0)
                    {
                        DialogResult Result = MessageBox.Show("Choose 'Yes' to apply this change to " + BusinessApplication.Name + " only,\nChoose 'Cancel' to not apply this change.", "Confirmation required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        switch (Result)
                        {
                            case System.Windows.Forms.DialogResult.OK:
                                // Set the Scope for this service only
                                SetScope(BusinessApplication, chkInScope.Checked, false);

                                if (Main.MainForm == null)
                                {
                                    Main.DetermineAllBusinessApplicationInFocus(Main.DrawingsForm.lvwDrawings);
                                }
                                else
                                {
                                    Main.DetermineAllBusinessApplicationInFocus(Main.MainForm.lvwDrawings);
                                }

                                Main.AddException("[INF1113] Reload necessary due to 'In Scope' change");
                                break;
                            case System.Windows.Forms.DialogResult.Cancel:
                                // Don't apply the change
                                chkInScope.Checked = BusinessApplication.InScope;
                                break;
                        }
                    }

                    #endregion

                    #region No Dependents

                    else
                    {
                        // Set the Scope for this service and all Dependents
                        SetScope(BusinessApplication, chkInScope.Checked, true);

                        if (Main.MainForm == null)
                        {
                            Main.DetermineAllBusinessApplicationInFocus(Main.DrawingsForm.lvwDrawings);
                        }
                        else
                        {
                            Main.DetermineAllBusinessApplicationInFocus(Main.MainForm.lvwDrawings);
                        }

                        Main.AddException("[INF1114] Reload necessary due to 'In Scope' change");
                    }

                    #endregion
                }

                #endregion
            }

            _BusinessApplication.Save(_SQL);

            Cursor.Current = Cursors.Default;
        }

        private void SaveDrawingDetails()
        {
            Cursor.Current = Cursors.WaitCursor;

            string CurrentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string Now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("en-AU"));

            if (_BusinessApplication.Drawing != null)
            {
                string LastUpdateText = "[INF1116] Status set to " + _BusinessApplication.Drawing.Status.ToString() + " by " + CurrentUserName + " at " + Now;
                _BusinessApplication.Drawing.LastUpdate = LastUpdateText;
                txtLastUpdate.Text = LastUpdateText;

                _BusinessApplication.Drawing.Save(_SQL);

                Debug.WriteLine("[INF1117] " + _BusinessApplication.Name + " Drawing saved", "information");
            }

            ((frmMain)_ParentForm).FilterList();

            if (Properties.Settings.Default.FlagDrawingErrors)
            {
                if (BusinessApplication.Drawing.InError)
                {
                    picImageState.Image = Properties.Resources.Blueprint_red_256;
                }
                else
                {
                    switch (BusinessApplication.Drawing.Status)
                    {
                        case Global.DrawingStatus.Not_Applicable:
                            picImageState.Image = Properties.Resources.Blueprint_none_256;
                            break;
                        case Global.DrawingStatus.Draft:
                            picImageState.Image = Properties.Resources.Blueprint_grey_256;
                            break;
                        case Global.DrawingStatus.Pending_Review:
                            picImageState.Image = Properties.Resources.Blueprint_orange_256;
                            break;
                        case Global.DrawingStatus.Approved:
                            picImageState.Image = Properties.Resources.Blueprint_green_256;
                            break;
                    }
                }
            }
            else
            {
                switch (BusinessApplication.Drawing.Status)
                {
                    case Global.DrawingStatus.Not_Applicable:
                        picImageState.Image = Properties.Resources.Blueprint_none_256;
                        break;
                    case Global.DrawingStatus.Draft:
                        picImageState.Image = Properties.Resources.Blueprint_grey_256;
                        break;
                    case Global.DrawingStatus.Pending_Review:
                        picImageState.Image = Properties.Resources.Blueprint_orange_256;
                        break;
                    case Global.DrawingStatus.Approved:
                        picImageState.Image = Properties.Resources.Blueprint_green_256;
                        break;
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void SetScope(BusinessApplication Service, bool InScope, bool CascadeDown)
        {
            bool GoingFromInScopeToOutOfScope = false;
            bool GoingFromOutOfScopeToInScope = false;

            if (Properties.Settings.Default.ShowDebugInformation)
            {
                Debug.WriteLine("[INF1115] " + Service.Name + " (Business Application): In Scope set to " + InScope.ToString(), "information");
            }

            // Determine what direction we are going...
            if (Service.InScope && !InScope)
            {
                GoingFromInScopeToOutOfScope = true;
            }

            if (!Service.InScope && InScope)
            {
                GoingFromOutOfScopeToInScope = true;
            }

            // Save the valiue...
            Service.InScope = InScope;

            // Update the Messages form...
            if (GoingFromInScopeToOutOfScope)
            {
                Main.AddOutOfScopeApplication(Service);
            }

            if (GoingFromOutOfScopeToInScope)
            {
                Main.RemoveOutOfScopeApplication(Service);
            }

            // Save the Service
            Service.Save(_SQL);

            if (CascadeDown)
            {
                foreach (BusinessApplication DependentService in Service.DependentBusinessApplications)
                {
                    SetScope(DependentService, InScope, CascadeDown);
                }
            }
        }

        private void UpdateBusinessApplicationState()
        {
            if (!_Loading)
            {
                switch (_BusinessApplication.SystemState)
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
            }

            UpdateImageAndStateDescription();
        }

        private void UpdateDrawingState()
        {
            if (BusinessApplication.Drawing != null)
            {
                switch (BusinessApplication.Drawing.Status)
                {
                    case Global.DrawingStatus.Not_Applicable:
                        cmbDrawingStatus.SelectedIndex = 0;
                        break;
                    case Global.DrawingStatus.Draft:
                        cmbDrawingStatus.SelectedIndex = 1;
                        break;
                    case Global.DrawingStatus.Pending_Review:
                        cmbDrawingStatus.SelectedIndex = 2;
                        break;
                    case Global.DrawingStatus.Approved:
                        cmbDrawingStatus.SelectedIndex = 3;
                        break;
                }
            }
        }

        private void UpdateImageAndStateDescription()
        {
            picService.BackgroundImage = imlLarge.Images[Global.GetBusinessApplicationStateImageIndex(_BusinessApplication, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication)];
            lblStateDescription.Text = _BusinessApplication.SystemStateDescription;
        }

        private bool CreateManagementPackFromBusinessApplication(BusinessApplication BusinessApplication, bool CreateAsStub, Global.Environment Environment)
        {
            #region Variables

            ServerUtilities SCOMUtilities = null; // new ServerUtilities(_SCOMServerName, _SCSMServerName);
            bool Result = false;
            List<string> ComponentServers = new List<string>();
            List<string> ComponentServices = new List<string>();
            int BusinessApplicationDrawingVersion = 0;
            string ErrorMessage = "";
            string ManagementPackXML = "";
            Version NewVersion = null;

            string ManagementPackID = "";
            string ManagementPackNamePrefix = Properties.Settings.Default.SystemCenter_ManagementPackIDPrefix; // "FIN.DistributedApplication.";
            string ManagementPackFriendlyName = Properties.Settings.Default.SystemCenter_ManagementPackFriendlyName; // "Finance Distributed Application - #SERVICENAME#";           
            string ManagementPackFilename = ISA.Helper.Methods.ReplaceTags(Properties.Settings.Default.SystemCenter_ManagementPackSaveFolderName, DateTime.Now, true); // @"C:\SVN\SCOM\Management Packs\FIN.DistributedApplication.#SERVICENAME#.xml";
            string ManagementPackDescription = Properties.Settings.Default.SystemCenter_ManagementPackDescription; // "This management pack defines the #SERVICENAME# Distributed Application.";
            string ManagementPackFriendlyNamePrefix = Properties.Settings.Default.SystemCenter_DistributedApplicationFriendlyNamePrefix; // "Finance - ";

            #endregion

            switch (Environment)
            {
                case Global.Environment.Development:
                    SCOMUtilities = new ServerUtilities(Properties.Settings.Default.SystemCenter_DevSCOMServer, Properties.Settings.Default.SystemCenter_DevSCSMServer);
                    break;
                case Global.Environment.Test:
                    SCOMUtilities = new ServerUtilities(Properties.Settings.Default.SystemCenter_TestSCOMServer, Properties.Settings.Default.SystemCenter_TestSCSMServer);
                    break;
                case Global.Environment.Production:
                    SCOMUtilities = new ServerUtilities(Properties.Settings.Default.SystemCenter_SCOMServer, Properties.Settings.Default.SystemCenter_SCSMServer);
                    break;
            }

            // Management Pack Version will be in the following form:
            // App Major . App Minor . Drawing Version . Service Version

            if (BusinessApplication.Drawing == null)
            {
                BusinessApplicationDrawingVersion = 1;
            }
            else
            {
                BusinessApplicationDrawingVersion = BusinessApplication.Drawing.Version;
            }

            if (!CreateAsStub)
            {
                NewVersion = new Version(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major,
                                         System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor,
                                         BusinessApplicationDrawingVersion,
                                         BusinessApplication.Version
                                        );
            }
            else
            {
                // We need to create a Stub.  To allow over-writing of this stub to be easy, we will mockup the version number
                // The Stub version number will be:
                // 0 . AppMinor . DayOfYear . SecondsSinceMidnight

                NewVersion = new Version(0,
                                         System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor,
                                         DateTime.Now.DayOfYear,
                                         Convert.ToInt32(DateTime.Now.TimeOfDay.TotalSeconds)
                                         );

                // Note: this will make it next to impossible to over-write an actual MP with a stub
            }

            //if (Properties.Settings.Default.ImportSCOMManagementPacks || Properties.Settings.Default.ImportSCSMManagementPacks)
            //{
            ManagementPackID = ManagementPackNamePrefix + BusinessApplication.Name.Replace(" ", "_").Replace("(", "").Replace(")", "").Replace("-", "_");

            string ManagementPackFullPath = System.IO.Path.Combine(ManagementPackFilename, ManagementPackID + ".xml");

            if (!CreateAsStub)
            {
                // Build a list of Server names for the Management Pack
                foreach (Server ComponentServer in BusinessApplication.ComponentServers)
                {
                    Relationship Relationship = Global.GetRelationship(BusinessApplication, ComponentServer, BusinessApplication.Relationships);

                    if (!Relationship.StreamDependency) // Exclude Stream dependencies.  Currently not possible for Server <-> Service, but let's make sure
                    {
                        if (ComponentServer.AddToManagementPack) // Exclude Servers that were explicitly marked to not be added
                        {
                            ComponentServers.Add(ComponentServer.Name);
                        }
                    }
                }

                // Build a list of Service names for the Management Pack
                foreach (BusinessApplication ComponentService in BusinessApplication.ComponentBusinessApplications)
                {
                    Relationship Relationship = Global.GetRelationship(BusinessApplication, ComponentService, BusinessApplication.Relationships);

                    if (!Relationship.StreamDependency) // Exclude Stream dependencies
                    {
                        ComponentServices.Add(ComponentService.Name);
                    }
                }
            }

            ManagementPackCreationOptions CreationOptions = new ManagementPackCreationOptions();

            CreationOptions.ImportSCOMManagementPacks = Properties.Settings.Default.ImportSCOMManagementPacks; // true;
            CreationOptions.ImportSCSMManagementPacks = Properties.Settings.Default.ImportSCSMManagementPacks; // true;

            switch (Environment)
            {
                case Global.Environment.Development:
                    if (Properties.Settings.Default.SystemCenter_DevSCOMServer != "")
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_DevSCOMServer;
                    }
                    else if (Properties.Settings.Default.SystemCenter_DevSCSMServer != "")
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_DevSCSMServer;
                    }
                    else
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = "";
                    }
                    break;
                case Global.Environment.Test:
                    if (Properties.Settings.Default.SystemCenter_DevSCOMServer != "")
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_TestSCOMServer;
                    }
                    else if (Properties.Settings.Default.SystemCenter_DevSCSMServer != "")
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_TestSCSMServer;
                    }
                    else
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = "";
                    }
                    break;
                case Global.Environment.Production:
                    if (Properties.Settings.Default.SystemCenter_DevSCOMServer != "")
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_SCOMServer;
                    }
                    else if (Properties.Settings.Default.SystemCenter_DevSCSMServer != "")
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_SCSMServer;
                    }
                    else
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = "";
                    }
                    break;
            }

            CreationOptions.ManagementPackID = ManagementPackNamePrefix + BusinessApplication.Name.Replace(@" ", "_")
                                                                               .Replace(@"(", "")
                                                                               .Replace(@")", "")
                                                                               .Replace(@"/", "_")
                                                                               .Replace(@"\", "_")
                                                                               .Replace(@"-", "_");

            CreationOptions.ManagementPackFolderName = Properties.Settings.Default.SystemCenter_ManagementPackViewFolderName;
            CreationOptions.ManagementPackName = ManagementPackFriendlyName.Replace("#SERVICENAME#", BusinessApplication.Name);
            CreationOptions.ManagementPackDescription = ManagementPackDescription.Replace("#SERVICENAME#", BusinessApplication.Name);
            CreationOptions.ManagementPackFilename = ManagementPackFullPath;
            CreationOptions.ServiceInfo.ServiceName = BusinessApplication.Name;
            CreationOptions.ServiceInfo.ServiceFriendlyName = ManagementPackFriendlyNamePrefix + BusinessApplication.Name;
            CreationOptions.ManagementPackVersion = NewVersion;
            CreationOptions.ManagementPackEmptyTemplate = Properties.Settings.Default.SystemCenter_MPEmptyTemplate;
            CreationOptions.ManagementPackServersAndServicesTemplate = Properties.Settings.Default.SystemCenter_MPServersAndServicesTemplate;
            CreationOptions.ManagementPackServersTemplate = Properties.Settings.Default.SystemCenter_MPServersTemplate;
            CreationOptions.ManagementPackServicesTemplate = Properties.Settings.Default.SystemCenter_MPServicesTemplate;
            CreationOptions.AlwaysImportManagementPacks = Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus;

            Result = SCOMUtilities.CreateOrUpdateManagementPack(ManagementPackID, NewVersion, ComponentServers, ComponentServices, CreationOptions, out ErrorMessage, out ManagementPackXML);

            BusinessApplication.ManagementPackXML = ManagementPackXML;

            if (ErrorMessage != "")
            {
                Debug.WriteLine(ErrorMessage);

                Main.AddWarning(ErrorMessage, BusinessApplication);
            }

            //}
            //else
            //{
            //    Debug.WriteLine("[INF1024] User options prevent importing Management Packs into either System Center product", "warning");
            //    Result = false;
            //}

            return Result;
        }

        private void CreateManagementPacksFromSelectedItems(bool AsStub, Global.Environment Environment)
        {
            bool Result = false;

            foreach (ListViewItem Item in lvwBusinessApplicationComponentServices.SelectedItems)
            {
                BusinessApplication Service = (BusinessApplication)Item.Tag;

                if (Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus || Service.Drawing.Status == Global.DrawingStatus.Approved)
                {
                    Debug.WriteLine("..." + Service.Name + "...", "information");
                    Result = CreateManagementPackFromBusinessApplication(Service, AsStub, Environment);
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
                    Debug.WriteLine("[ERR1035] A problem was encountered whilst updating or creating a Management Pack", "error");
                }
            }

            Debug.WriteLine("Management Pack creation request complete", "information");
            Debug.WriteLine("======= " + DateTime.Now.ToString() + " ================================================================\n", "information");
        }

        #endregion

        #region Public Methods

        public void PerformUpdate()
        {
            int RunbookPosition = 0;
            int ThisPosition = 0;

            Cursor.Current = Cursors.WaitCursor;

            Hints.RemoveAll();

            Hints.AutoPopDelay = Properties.Settings.Default.HelpHintTimemS;

            lvwAffectedServers.BeginUpdate();
            lvwDependentBusinessApplications.BeginUpdate();
            lvwRunbook.BeginUpdate();
            lvwBusinessApplicationComponentServers.BeginUpdate();
            lvwBusinessApplicationComponentServices.BeginUpdate();

            ClearApplicationDetails();

            if (BusinessApplication != null)
            {
                string DrawingState = "Drawing: None";
                BusinessApplication SelectedService = _BusinessApplication;
                string ServiceState = "";

                this.Text = "Business Application: " + SelectedService.Name;

                //if (_BusinessApplication.HARelationships.Count > 0 && Properties.Settings.Default.ShowDebugInformation)
                //{
                //    Debug.WriteLine("HA Relationships: " + _BusinessApplication.HARelationships.Count);
                //}

                picService.BackgroundImage = imlLarge.Images[Global.GetBusinessApplicationStateImageIndex(BusinessApplication, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication)];

                if (_BusinessApplication.InScope)
                {
                    if (BusinessApplication.SystemState == Global.HealthState.OK)
                    {
                        ServiceState = "Business Application: OK";
                    }
                    else
                    {
                        ServiceState = "Business Application: " + BusinessApplication.SystemState + " (" + _BusinessApplication.SystemStateDescription + ")";
                    }
                }
                else
                {
                    if (_BusinessApplication.OverrideInScope)
                    {
                        lblInScopeOverride.Visible = true;

                        ServiceState = "Business Application: Out Of Scope (Over-ridden)";
                    }
                    else
                    {
                        lblInScopeOverride.Visible = false;

                        ServiceState = "Business Application: Out Of Scope";
                    }
                }

                UpdateDrawingState();

                txtStream.Text = Global.GetBusinessApplicationStreamName(BusinessApplication.CalculatedStream);
                txtDesiredTier.Text = Global.GetTierName(BusinessApplication.DesiredTier);
                txtActualTier.Text = Global.GetTierName(BusinessApplication.ActualTier);
                txtID.Text = _BusinessApplication.ID.ToString();
                txtRecoveryTime.Text = ISA.Helper.Methods.FormattedTime(_BusinessApplication.CalculatedRecoveryTime);
                txtComment1.Text = BusinessApplication.Comment1;
                txtWorkInstructionName.Text = BusinessApplication.WorkInstructionName;
                lblMPVersion.Text = BusinessApplication.ManagementPackVersion.ToString();

                picWarnings.Visible = _BusinessApplication.Warnings.Count > 0;

                foreach (string Message in BusinessApplication.Warnings)
                {
                    lstWarnings.Items.Add(Message);
                }

                if (BusinessApplication.Drawing != null)
                {
                    txtDrawingDescription.Text = _BusinessApplication.Drawing.Description;
                    txtDrawingID.Text = _BusinessApplication.Drawing.ID;
                    txtDrawingVersion.Text = _BusinessApplication.Drawing.Version.ToString();
                    txtLastUpdate.Text = _BusinessApplication.Drawing.LastUpdate;
                    txtDrawingNotes.Text = _BusinessApplication.Drawing.Notes;

                    if (ISA.Helper.Properties.DependencyDatabaseRW)
                    {
                        cmbDrawingStatus.Enabled = true;
                        txtComment1.Enabled = true;
                    }

                    DrawingState = "Drawing: " + Enum.GetName(typeof(Global.DrawingStatus), BusinessApplication.Drawing.Status);

                    picDrawingPreview.Image = _BusinessApplication.Drawing.Image;
                    picDrawingPreview.SizeMode = PictureBoxSizeMode.Zoom;
                    picDrawingPreview.Cursor = Cursors.Hand;

                    if (Properties.Settings.Default.FlagDrawingErrors)
                    {
                        if (BusinessApplication.Drawing.InError)
                        {
                            picImageState.Image = Properties.Resources.Blueprint_red_256;
                        }
                        else
                        {
                            switch (BusinessApplication.Drawing.Status)
                            {
                                case Global.DrawingStatus.Not_Applicable:
                                    picImageState.Image = Properties.Resources.Blueprint_none_256;
                                    break;
                                case Global.DrawingStatus.Draft:
                                    picImageState.Image = Properties.Resources.Blueprint_grey_256;
                                    break;
                                case Global.DrawingStatus.Pending_Review:
                                    picImageState.Image = Properties.Resources.Blueprint_orange_256;
                                    break;
                                case Global.DrawingStatus.Approved:
                                    picImageState.Image = Properties.Resources.Blueprint_green_256;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (BusinessApplication.Drawing.Status)
                        {
                            case Global.DrawingStatus.Not_Applicable:
                                picImageState.Image = Properties.Resources.Blueprint_none_256;
                                break;
                            case Global.DrawingStatus.Draft:
                                picImageState.Image = Properties.Resources.Blueprint_grey_256;
                                break;
                            case Global.DrawingStatus.Pending_Review:
                                picImageState.Image = Properties.Resources.Blueprint_orange_256;
                                break;
                            case Global.DrawingStatus.Approved:
                                picImageState.Image = Properties.Resources.Blueprint_green_256;
                                break;
                        }
                    }

                    toolTips.SetToolTip(picDrawingPreview, "Double click for larger image");

                    //if (Properties.Settings.Default.ShowDebugInformation)
                    //{
                    //    Debug.WriteLine("Version ID for " + BusinessApplication.Drawing.Name + " is " + BusinessApplication.Drawing.VersionID);
                    //}

                    if (BusinessApplication.Drawing.InError)
                    {
                        DrawingState = "Drawing: ERROR!";
                    }
                }

                lblStateDescription.Text = ServiceState + ", " + DrawingState;

                foreach (Relationship ThisRelationship in BusinessApplication.Relationships)
                {
                    if ((Properties.Settings.Default.ViewStreamDependenciesAsComponents && ThisRelationship.StreamDependency) || !ThisRelationship.StreamDependency)
                    {
                        if (!ThisRelationship.StreamDependency)
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

                            #region Populate Component Services and Servers

                            if (ThisRelationship.ToType == ISA.Dependency.Relationship.ComponentType.BusinessApplication)
                            {
                                Item.Text = ThisRelationship.ToBusinessApplication.Name;
                                Item.Name = ThisRelationship.ToBusinessApplication.Name;
                                Item.Tag = ThisRelationship.ToBusinessApplication;

                                string SubItem1 = RelationshipType;
                                string SubItem2 = Global.GetBusinessApplicationStreamName(ThisRelationship.ToBusinessApplication.CalculatedStream);
                                string SubItem3 = Global.GetTierName(ThisRelationship.ToBusinessApplication.DesiredTier);

                                Item.ImageIndex = Global.GetBusinessApplicationStateImageIndex(ThisRelationship.ToBusinessApplication, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication);

                                Item.SubItems.Add(SubItem1);
                                Item.SubItems.Add(SubItem2);
                                Item.SubItems.Add(SubItem3);

                                if ((Properties.Settings.Default.ShowDrawingLessBusinessApplications && ThisRelationship.ToBusinessApplication.Drawing == null) || (ThisRelationship.ToBusinessApplication.Drawing != null))
                                {
                                    lvwBusinessApplicationComponentServices.Items.Add(Item);
                                }
                            }
                            else  // Server
                            {
                                bool ThisServerIsHA = false;
                                // This server may be participating in one or more HA relationships
                                // If so, check if one of the HA relationships is with the current Service
                                if (_BusinessApplication.HARelationships.Count > 0)
                                {
                                    if (SelectedService.HAServers.Contains(ThisRelationship.ToServer))
                                    {
                                        ThisServerIsHA = true;
                                    }
                                }

                                Item.Text = ThisRelationship.ToServer.Name;
                                Item.Name = ThisRelationship.ToServer.Name;
                                Item.Tag = ThisRelationship.ToServer;

                                string SubItem1 = RelationshipType;
                                string SubItem2 = Global.GetServerStreamName(ThisRelationship.ToServer);
                                string SubItem3 = "";

                                if (ThisRelationship.ToServer.PhysicalSite != null)
                                {
                                    SubItem3 = ThisRelationship.ToServer.PhysicalSite.Name;
                                }
                                else
                                {
                                    SubItem3 = "N/A";
                                }

                                Item.ImageIndex = Global.GetServerStateImageIndex(ThisRelationship.ToServer);

                                Item.SubItems.Add(SubItem1);
                                Item.SubItems.Add(SubItem2);
                                Item.SubItems.Add(SubItem3);

                                if (ThisServerIsHA)
                                {
                                    Item.SubItems.Add("*");
                                }

                                lvwBusinessApplicationComponentServers.Items.Add(Item);
                            }

                            #endregion
                        }
                    }
                }

                #region Populate Runbook position

                lvwRunbook.Items.Add("[Top of Runbook]");
                foreach (BusinessApplication ThisService in _BusinessApplicationRunbook)
                {
                    string Item = ThisService.Name;
                    ThisPosition++;

                    ListViewItem NewItem = new ListViewItem(Item);
                    NewItem.Tag = ThisService;

                    NewItem.ImageIndex = Global.GetBusinessApplicationStateImageIndex(ThisService, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication);

                    if (BusinessApplication.Drawing != null)
                    {
                        tabBusinessApplication.TabPages[3].Enabled = true;

                        if ((BusinessApplication.Drawing.Image == null && Properties.Settings.Default.ShowDrawingLessBusinessApplications) || (BusinessApplication.Drawing.Image != null))
                        {
                            lvwRunbook.Items.Add(NewItem);
                        }

                        if (ThisService.Name == SelectedService.Name)
                        {
                            RunbookPosition = ThisPosition;
                        }
                    }
                    else
                    {
                        tabBusinessApplication.TabPages[3].Enabled = false;
                    }
                }

                //lvwRunbook.Scrollable = true;
                lvwRunbook.Items.Add("[Bottom of Runbook]");
                lvwRunbook.EnsureVisible(RunbookPosition + 1);
                lvwRunbook.Items[RunbookPosition].Selected = true;
                //lvwRunbook.Scrollable = false;

                #endregion

                #region Delete this code
                //#region Populate Component Services

                //foreach (BusinessApplication ComponentService in SelectedService.ComponentBusinessApplications)
                //{
                //    ListViewItem Item = new ListViewItem();
                //    Item.Text = ComponentService.Name;
                //    Item.Name = ComponentService.Name;
                //    Item.Tag = ComponentService;

                //    Item.ImageIndex = Global.GetBusinessApplicationStateImageIndex(ComponentService, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication);

                //    string SubItem1 = "";
                //    if (ComponentService.Relationships[0].Mandatory)
                //    {
                //        SubItem1 = "M";
                //    }
                //    else
                //    {
                //        SubItem1 = "O";
                //    }
                //    string SubItem2 = Global.GetStreamName(ComponentService.Stream);
                //    string SubItem3 = Global.GetTierName(ComponentService.Tier);

                //    Item.SubItems.Add(SubItem1);
                //    Item.SubItems.Add(SubItem2);
                //    Item.SubItems.Add(SubItem3);

                //    if ((ComponentService.Image == null && Properties.Settings.Default.ShowDrawingLessBusinessApplications) || (ComponentService.Image != null))
                //    {
                //        lvwBusinessApplicationComponentServices.Items.Add(Item);
                //    }
                //}

                //#endregion

                //#region Populate Component Servers

                //foreach (Server Server in SelectedService.ComponentServers)
                //{
                //    ListViewItem Item = new ListViewItem();
                //    Item.Text = Server.Name;
                //    Item.Name = Server.Name;
                //    Item.Tag = Server;

                //    string SubItem1 = "";
                //    if (Server.Relationships[0].Mandatory)
                //    {
                //        SubItem1 = "M";
                //    }
                //    else
                //    {
                //        SubItem1 = "O";
                //    }

                //    string SubItem2 = Server.PhysicalSiteName.ToString();

                //    Item.ImageIndex = Global.GetServerStateImageIndex(Server);

                //    Item.SubItems.Add(SubItem1);
                //    Item.SubItems.Add(SubItem2);

                //    lvwServiceComponentServers.Items.Add(Item);
                //}

                //#endregion

                #endregion

                #region Populate Dependent Services and Affected Servers

                foreach (BusinessApplication DependentService in SelectedService.DependentBusinessApplications)
                {
                    ListViewItem Item1 = new ListViewItem();
                    Item1.Text = DependentService.Name;
                    Item1.Name = DependentService.Name;
                    Item1.Tag = DependentService;

                    List<Relationship> TargetRelationships = SelectedService.Relationships.Where(
                                                             r => r.ToType == Relationship.ComponentType.BusinessApplication
                                                             && r.ToBusinessApplication.Name == SelectedService.Name)
                                                             .ToList<Relationship>();

                    Relationship DependentRelationship = Global.GetRelationship(DependentService, SelectedService, _AllRelationships);

                    if (!DependentRelationship.StreamDependency)
                    {
                        Item1.ImageIndex = Global.GetBusinessApplicationStateImageIndex(DependentService, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication);

                        string SubItem1 = Global.GetBusinessApplicationStreamName(DependentService.CalculatedStream);
                        string SubItem2 = Global.GetTierName(DependentService.DesiredTier);

                        Item1.SubItems.Add(SubItem1);
                        Item1.SubItems.Add(SubItem2);

                        if ((DependentService.Drawing.Image == null && Properties.Settings.Default.ShowDrawingLessBusinessApplications) || (DependentService.Drawing.Image != null))
                        {

                            lvwDependentBusinessApplications.Items.Add(Item1);

                            #region Affected Servers

                            foreach (Server Server in DependentService.ComponentServers)
                            {
                                bool ItemExists = false;
                                ListViewItem Item3 = new ListViewItem();
                                Item3.Text = Server.Name;
                                Item3.Name = Server.Name;
                                Item3.Tag = Server;

                                Item3.ImageIndex = Global.GetServerStateImageIndex(Server);

                                string SubItem3 = Global.GetServerStreamName(Server); // Server.Stream.ToString();
                                string SubItem4 = Server.PhysicalSite.Name.ToString(); // Server.PhysicalSiteName.ToString()

                                Item3.SubItems.Add(SubItem3);
                                Item3.SubItems.Add(SubItem4);

                                foreach (ListViewItem ExistingItem in lvwAffectedServers.Items)
                                {
                                    if (ExistingItem.Text == Item3.Text)
                                    {
                                        ItemExists = true;
                                        break;
                                    }
                                }

                                if (!ItemExists)
                                {
                                    lvwAffectedServers.Items.Add(Item3);
                                }
                            }

                            #endregion
                        }
                    }
                }

                #endregion

                #region Details

                txtDisplayName.Text = SelectedService.DisplayName;
                txtDescription.Text = SelectedService.Description;
                txtVersion.Text = SelectedService.Version.ToString();

                if (SelectedService.RunbookRegion == int.MaxValue)
                {
                    txtRunbookOrder.Text = "N/A";
                }
                else
                {
                    txtRunbookOrder.Text = SelectedService.RunbookRegion.ToString();
                }
                //chkShowInRunbook.Checked = SelectedService.ShowInRunbook;
                chkShowInCatalogue.Checked = SelectedService.ShowInServiceCatalogue;
                chkSubService.Checked = SelectedService.SubApplication;
                chkInScope.Checked = SelectedService.InScope;
                chkHA.Checked = SelectedService.HighlyAvailable;

                #endregion

                #region Tooltips

                Hints.SetToolTip(lvwBusinessApplicationComponentServices, "All of these Component Business Applications are required for " + BusinessApplication.Name + " to function.");
                Hints.SetToolTip(lvwBusinessApplicationComponentServers, "All of these Component Servers are required for " + BusinessApplication.Name + " to function.");
                Hints.SetToolTip(lvwDependentBusinessApplications, "If " + BusinessApplication.Name + " is degraded or in an error state, these Business Applications will be negatively affected.");
                Hints.SetToolTip(lvwAffectedServers, "If " + BusinessApplication.Name + " is degraded or in an error state, Business Applications provided by these Servers will be negatively affected.");
                Hints.SetToolTip(lvwRunbook, "This shows where in the Business Application Runbook " + BusinessApplication.Name + " is required.");

                if (BusinessApplication.SystemStateIsEmulated)
                {
                    Hints.SetToolTip(picService, BusinessApplication.Name + " is in a Emulated " + BusinessApplication.SystemState + " state.");
                }
                else
                {
                    Hints.SetToolTip(picService, BusinessApplication.Name + " is in an " + BusinessApplication.SystemState + " state.");
                }

                Hints.SetToolTip(txtStream, "Streams:\nA: Highly Available\nB: Recovered using SRM\nC: Recovered from a Backup Restore\nD: Reserved for future use\nE: Recovery provided by an external party\nNone: No recovery possible\nExt.: Recovery not controlled\n?: Recovery is undefined");
                Hints.SetToolTip(txtDesiredTier, "Tiers:\n0: Provides one or more ICT Foundation services\n1-4: Provides one or more Departmental Business Applications\n[blank]: Tier is undefined");

                #endregion

            }
            else
            {
                this.Text = "Unknown Business Application (" + _DrawingName + ")";
            }

            #region Security

            #endregion

            if (_BusinessApplication != null)
            {
                if (_BusinessApplication.SystemStateIsEmulated)
                {
                    radioButtonEmulated.Checked = true;
                    UpdateBusinessApplicationState();
                }
            }
            lvwAffectedServers.EndUpdate();
            lvwDependentBusinessApplications.EndUpdate();
            lvwRunbook.EndUpdate();
            lvwBusinessApplicationComponentServers.EndUpdate();
            lvwBusinessApplicationComponentServices.EndUpdate();

            Cursor.Current = Cursors.Default;
        }

        #endregion

    }
}
