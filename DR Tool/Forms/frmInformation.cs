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
    public partial class frmInformation : Form
    {
        #region Fields

        private bool _LoadComplete = false;
        private IMainForm _ParentForm = null;

        private List<string> _DatabaseWarnings = new List<string>();
        private List<string> _DrawingExceptions = new List<string>();
        private List<string> _Warnings = new List<string>();
        private List<string> _Exceptions = new List<string>();
        private List<Server> _OutOfScopeServers = new List<Server>();
        private List<BusinessApplication> _OutOfScopeServices = new List<BusinessApplication>();

        #endregion

        #region Constructors

        public frmInformation()
        {
            InitializeComponent();

            if (Main.MainForm == null)
            {
                //_ParentForm = Main.MDIForm;
                this.MdiParent = (frmMDIMain)Main.MDIForm;
            }
            else
            {
                _ParentForm = Main.MainForm;
            }
            
            this.Show();
            this.Refresh();
        }

        #endregion

        #region Event Handlers

        private void frmInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.frmInformationSize = this.Size;
                Properties.Settings.Default.frmInformationLocation = this.Location;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
        }

        private void frmInformation_Load(object sender, EventArgs e)
        {
            _LoadComplete = true;
        }

        private void frmInformation_Move(object sender, EventArgs e)
        {
            if (_LoadComplete && ISA.Helper.Properties.AllowSaveFormLocations)
            {
                Properties.Settings.Default.frmInformationLocation = this.Location;
            }
        }

        private void frmInformation_Resize(object sender, EventArgs e)
        {
            lvwOutOfScope.Columns[0].Width = tabInformation.Size.Width - 50;
            //lvwWarningsAndExceptions.Columns[0].Width = tabInformation.Size.Width - 50;
        }

        private void frmInformation_ResizeEnd(object sender, EventArgs e)
        {
            if (_LoadComplete)
            {
                Properties.Settings.Default.frmInformationSize = this.Size;
            }
        }

        private void frmInformation_Shown(object sender, EventArgs e)
        {
            // Set location and size
            if (Properties.Settings.Default.frmInformationLocation.X != 0 && Properties.Settings.Default.frmInformationLocation.Y != 0 && ISA.Helper.Properties.AllowSaveFormLocations)
            {
                this.Location = Properties.Settings.Default.frmInformationLocation;
            }

            if (Properties.Settings.Default.frmInformationSize.Width != 0 && Properties.Settings.Default.frmInformationSize.Height != 0)
            {
                this.Size = Properties.Settings.Default.frmInformationSize;
            }
        }

        private void ViewDependencyMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<BusinessApplication> Services = new List<BusinessApplication>();

            foreach (ListViewItem Item in lvwOutOfScope.SelectedItems)
            {
                Services.Add((BusinessApplication)Item.Tag);
            }

            Main.CreateOrSelectDependencyMapForm(Services, true, Services, LayoutMethod.SugiyamaScheme, false);
        }

        private void lvwWarningsAndExceptions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //ListViewItem SelectedItem = null;
            TreeNode Node = null;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (radTarget.Checked)
                {
                    Node = tvwApplications.SelectedNode;
                }
                else
                {
                    Node = tvwMessages.SelectedNode;
                }
                //SelectedItem = lvwWarningsAndExceptions.SelectedItems[0];

                if (Node.Tag != null)
                {
                    if (Node.Tag is BusinessApplication)
                    {
                        BusinessApplication Application = (BusinessApplication)Node.Tag;

                        //frmMain MainForm = (frmMain)_ParentForm;

                        if (Application.Drawing != null)
                        {
                            Main.CreateOrSelectBusinessApplicationForm(Application.Drawing.Name, Application);
                        }
                        else
                        {
                            Main.CreateOrSelectBusinessApplicationForm(Application.Name, Application);
                        }
                    }

                    else if (Node.Tag is Server)
                    {
                        Server Server = (Server)Node.Tag;

                        //frmMain MainForm = (frmMain)_ParentForm;

                        Main.CreateOrSelectServerForm(ref Server);
                    }

                }
            }
        }

        private void lvwOutOfScope_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem SelectedItem = null;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                SelectedItem = lvwOutOfScope.SelectedItems[0];

                if (SelectedItem.Tag != null)
                {
                    if (SelectedItem.Tag is BusinessApplication)
                    {
                        BusinessApplication Service = (BusinessApplication)SelectedItem.Tag;

                        //frmMain MainForm = (frmMain)_ParentForm;

                        if (Service.Drawing != null)
                        {
                            Main.CreateOrSelectBusinessApplicationForm(Service.Drawing.Name, Service);
                        }
                        else
                        {
                            Main.CreateOrSelectBusinessApplicationForm(Service.Name, Service);
                        }
                    }

                    else if (SelectedItem.Tag is Server)
                    {
                        Server Server = (Server)SelectedItem.Tag;

                        //frmMain MainForm = (frmMain)_ParentForm;

                        Main.CreateOrSelectServerForm(ref Server);
                    }

                }
            }
        }

        private void lvwOutOfScope_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lvwOutOfScope.SelectedItems.Count > 0)
                {
                    if (ISA.Helper.Properties.ManagementPackAuthor && (Properties.Settings.Default.ImportSCOMManagementPacks || Properties.Settings.Default.ImportSCSMManagementPacks))
                    {
                        createManagementPackToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        createManagementPackToolStripMenuItem.Enabled = false;
                    }
                    openToolStripMenuItem.Enabled = true;
                    ViewDependencyMapToolStripMenuItem.Enabled = true;
                    DrawingContextMenu.Show(Cursor.Position);
                }
            }
        }

        private void createManagementPackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //bool Result = false;
            //BusinessApplication Service = (BusinessApplication)lvwOutOfScope.SelectedItems[0].Tag;
            //frmMain MainForm = (frmMain)_ParentForm;
            //bool CreateMP = false;
            //string ErrorMessage = "";

            //// If there is no drawing, but the User has enabled creation of MP's regardless, then allow it
            //if (Service.Drawing == null && Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus)
            //{
            //    CreateMP = true;
            //}
            //else
            //{
            //    //Drawing exists and set to over-ride
            //}

            //// If there is a drawing, and the drawing is approved, then allow it
            //if (Service.Drawing != null && Service.Drawing.Status == Global.DrawingStatus.Approved)
            //{
            //    CreateMP = true;
            //}
            //else
            //{
            //    if (Service.Drawing == null)
            //    {
            //        ErrorMessage = "..." + Service.Name + " Drawing doesn't exist, so a Management Pack for this Service will not be created.";
            //    }
            //    else
            //    {
            //        ErrorMessage = "..." + Service.Name + " Drawing status is not Approved, so a Management Pack for this Service will not be created.";
            //    }
            //}

            //// If there is a drawing, and the User has enabled creation of MP's regardless, then allow it
            //if (Service.Drawing != null && Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus)
            //{
            //    CreateMP = true;
            //}
            //else
            //{
            //    if (Service.Drawing == null)
            //    {
            //        ErrorMessage = "..." + Service.Name + " Drawing doesn't exist, so a Management Pack for this Service will not be created.";
            //    }
            //    else
            //    {
            //        ErrorMessage = "..." + Service.Name + " Drawing status is not Approved, so a Management Pack for this Service will not be created.";
            //    }
            //}

            //if (CreateMP)
            //{
            //    Debug.WriteLine("..." + Service.Name + "...");
            //    Result = MainForm.CreateManagementPackFromBusinessApplication(Service, false);
            //}
            //else
            //{
            //    Debug.WriteLine(ErrorMessage);
            //    Result = false;
            //}


            //if (Result)  // Success
            //{

            //}
            //else         // Failure
            //{
            //    Debug.WriteLine("A problem was encountered whilst updating or creating a Management Pack");
            //}
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem SelectedItem = null;

            SelectedItem = lvwOutOfScope.SelectedItems[0];

            if (SelectedItem.Tag != null)
            {
                if (SelectedItem.Tag is BusinessApplication)
                {
                    BusinessApplication Service = (BusinessApplication)SelectedItem.Tag;

                    //frmMain MainForm = (frmMain)_ParentForm;

                    if (Service.Drawing != null)
                    {
                        Main.CreateOrSelectBusinessApplicationForm(Service.Drawing.Name, Service);
                    }
                    else
                    {
                        Main.CreateOrSelectBusinessApplicationForm(Service.Name, Service);
                    }
                }

                else if (SelectedItem.Tag is Server)
                {
                    Server Server = (Server)SelectedItem.Tag;

                    //frmMain MainForm = (frmMain)_ParentForm;

                    Main.CreateOrSelectServerForm(ref Server);
                }

            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void createStubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCopyValidationErrors_Click(object sender, EventArgs e)
        {
            //StringBuilder Builder = new StringBuilder();

            //foreach (ListViewItem Item in lvwWarningsAndExceptions.Items)
            //{
            //    Builder.AppendLine(Item.Text);
            //}

            //if (Builder.Length > 0)
            //{
            //    Clipboard.SetText(Builder.ToString());

            //    MessageBox.Show("Validation errors copied to clipboard","Message");
            //}

        }

        private void btnCopyOutOfScopeServices_Click(object sender, EventArgs e)
        {
            StringBuilder Builder = new StringBuilder();

            foreach (ListViewItem Item in lvwOutOfScope.Items)
            {
                Builder.AppendLine(Item.Text);
            }

            if (Builder.Length > 0)
            {
                Clipboard.SetText(Builder.ToString());

                MessageBox.Show("Out of Scope Business Applications copied to clipboard", "Message");
            }
        }

        private void CopyNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder Builder = new StringBuilder();

            if (lvwOutOfScope.SelectedItems.Count > 0)
            {
                // Copy the selected names

                foreach (ListViewItem Item in lvwOutOfScope.SelectedItems)
                {
                    Builder.AppendLine(Item.Text);
                }


            }
            else
            {
                // Copy all names

                foreach (ListViewItem Item in lvwOutOfScope.Items)
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

        private void lvwOutOfScope_MouseDown(object sender, MouseEventArgs e)
        {
            CopyNamesToolStripMenuItem.Enabled = true;
        }

        private void radMessage_CheckedChanged(object sender, EventArgs e)
        {
            tvwMessages.Visible = radMessage.Checked;
            tvwApplications.Visible = !radMessage.Checked;
        }

        private void radBusinessApplication_CheckedChanged(object sender, EventArgs e)
        {
            tvwApplications.Visible = radTarget.Checked;
            tvwMessages.Visible = !radTarget.Checked;
        }


        private void xxlvwOutOfScope_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lvwOutOfScope.SelectedItems.Count > 0)
                {
                    if (ISA.Helper.Properties.ManagementPackAuthor)
                    {
                        createManagementPackToolStripMenuItem.Enabled = true;
                    }
                    openToolStripMenuItem.Enabled = true;
                    ViewDependencyMapToolStripMenuItem.Enabled = true;
                    DrawingContextMenu.Show(Cursor.Position);
                }
            }
        }

        private void xxlvwOutOfScope_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BusinessApplication BusinessApplication = (BusinessApplication)lvwOutOfScope.SelectedItems[0].Tag;
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.CreateOrSelectBusinessApplicationForm(lvwOutOfScope.SelectedItems[0].Text, BusinessApplication);
        }

        #endregion

        #region Public Methods

        public void AddOutOfScopeServer(Server Server)
        {
            if (!_OutOfScopeServers.Contains(Server))
            {
                _OutOfScopeServers.Add(Server);

                ListViewItem Item = new ListViewItem();
                Item.Text = Server.Name;
                Item.Name = Server.Name;
                Item.Tag = Server;
                Item.ImageIndex = Global.GetServerStateImageIndex(Server);

                lvwOutOfScope.Items.Add(Item);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            this.Refresh();
        }

        public void AddOutOfScopeApplication(BusinessApplication Application)
        {
            if (!_OutOfScopeServices.Contains(Application))
            {
                _OutOfScopeServices.Add(Application);

                ListViewItem Item = new ListViewItem();
                Item.Text = Application.Name;
                Item.Name = Application.Name;
                Item.Tag = Application;
                Item.ImageIndex = Global.GetBusinessApplicationStateImageIndex(Application, true);

                lvwOutOfScope.Items.Add(Item);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            this.Refresh();
        }

        //public void AddWarning(string Message)
        //{
        //    if (!_Warnings.Contains(Message))
        //    {
        //        _Warnings.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 0;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();
        //}

        //public void AddWarning(string Message, BusinessApplication Service)
        //{
        //    if (!_Warnings.Contains(Message))
        //    {
        //        _Warnings.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 0;
        //        Item.Tag = Service;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();
        //}

        //public void AddWarning(string Message, Server Server)
        //{
        //    if (!_Warnings.Contains(Message))
        //    {
        //        _Warnings.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 0;
        //        Item.Tag = Server;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();
        //}

        //public void Clear()
        //{
        //    _Warnings.Clear();
        //    _Exceptions.Clear();
        //    _OutOfScopeServers.Clear();
        //    _OutOfScopeServices.Clear();
        //    _DrawingExceptions.Clear();
        //    _DatabaseWarnings.Clear();
        //    lvwOutOfScope.Items.Clear();
        //    lvwWarningsAndExceptions.Items.Clear();

        //    //this.Hide();
        //}

        //public void AddDatabaseWarning(string Message)
        //{
        //    if (!_DatabaseWarnings.Contains(Message))
        //    {
        //        _DatabaseWarnings.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 3;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();
        //}

        //public void AddDatabaseWarning(string Message, BusinessApplication Service)
        //{
        //    if (!_DatabaseWarnings.Contains(Message))
        //    {
        //        _DatabaseWarnings.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 4;
        //        Item.Tag = Service;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();
        //}

        //public void AddDatabaseWarning(string Message, Server Server)
        //{
        //    if (!_DatabaseWarnings.Contains(Message))
        //    {
        //        _DatabaseWarnings.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 4;
        //        Item.Tag = Server;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();
        //}

        //public void AddDrawingException(string Message)
        //{
        //    if (!_DrawingExceptions.Contains(Message))
        //    {
        //        _DrawingExceptions.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 2;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();
        //}

        //public void AddDrawingException(string Message, BusinessApplication Service)
        //{
        //    if (!_DrawingExceptions.Contains(Message))
        //    {
        //        _DrawingExceptions.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 2;
        //        Item.Tag = Service;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();
        //}

        //public void AddDrawingException(string Message, Server Server)
        //{
        //    if (!_DrawingExceptions.Contains(Message))
        //    {
        //        _DrawingExceptions.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 2;
        //        Item.Tag = Server;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();
        //}

        //public void AddException(string Message)
        //{
        //    if (!_Exceptions.Contains(Message))
        //    {
        //        _Exceptions.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 1;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();

        //}

        //public void AddException(string Message, BusinessApplication Service)
        //{
        //    if (!_Exceptions.Contains(Message))
        //    {
        //        _Exceptions.Add(Message);

        //        ListViewItem Item = new ListViewItem();
        //        Item.Text = Message;
        //        Item.Name = Message;
        //        Item.ImageIndex = 1;
        //        Item.Tag = Service;

        //        lvwWarningsAndExceptions.Items.Add(Item);
        //    }

        //    if (!this.Visible)
        //    {
        //        this.Show();
        //    }

        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //    }

        //    this.Refresh();

        //}

        public void RemoveOutOfScopeServer(Server Server)
        {
            _OutOfScopeServers.Remove(Server);

            ListViewItem Item = lvwOutOfScope.FindItemWithText(Server.Name);
            Item.Tag = Server;

            lvwOutOfScope.Items.Remove(Item);

            this.Refresh();
        }

        public void RemoveOutOfScopeApplication(BusinessApplication Application)
        {
            _OutOfScopeServices.Remove(Application);

            ListViewItem Item = lvwOutOfScope.FindItemWithText(Application.Name);
            Item.Tag = Application;

            lvwOutOfScope.Items.Remove(Item);

            this.Refresh();
        }

        public void AddDatabaseWarning(string Message)
        {
            if (!_DatabaseWarnings.Contains(Message))
            {
                _DatabaseWarnings.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 3;
                AppNode.SelectedImageIndex = 3;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 3;
                MsgNode.SelectedImageIndex = 3;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, "Database Warnings", 4);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 0);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();
        }

        public void AddDatabaseWarning(string Message, BusinessApplication Application)
        {
            Application.Drawing.InError = true;

            if (!_DatabaseWarnings.Contains(Message))
            {
                _DatabaseWarnings.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 4;
                AppNode.SelectedImageIndex = 4;
                AppNode.Tag = Application;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 4;
                MsgNode.SelectedImageIndex = 4;
                MsgNode.Tag = Application;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, Application.Name, 6);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 0);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();
        }

        public void AddDatabaseWarning(string Message, Server Server)
        {
            if (!_DatabaseWarnings.Contains(Message))
            {
                _DatabaseWarnings.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 2;
                AppNode.SelectedImageIndex = 2;
                AppNode.Tag = Server;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 2;
                MsgNode.SelectedImageIndex = 2;
                MsgNode.Tag = Server;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, Server.Name, 7);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 0);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();
        }

        public void AddDrawingException(string Message)
        {
            if (!_DrawingExceptions.Contains(Message))
            {
                _DrawingExceptions.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 2;
                AppNode.SelectedImageIndex = 2;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 2;
                MsgNode.SelectedImageIndex = 2;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, "Drawing Exceptions", 5);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 1);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();
        }

        public void AddDrawingException(string Message, BusinessApplication Application)
        {
            Application.Drawing.InError = true;

            if (!_DrawingExceptions.Contains(Message))
            {
                _DrawingExceptions.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 2;
                AppNode.SelectedImageIndex = 2;
                AppNode.Tag = Application;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 2;
                MsgNode.SelectedImageIndex = 2;
                MsgNode.Tag = Application;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, Application.Name, 6);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 1);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();
        }

        public void AddDrawingException(string Message, Server Server)
        {
            if (!_DrawingExceptions.Contains(Message))
            {
                _DrawingExceptions.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 2;
                AppNode.SelectedImageIndex = 2;
                AppNode.Tag = Server;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 2;
                MsgNode.SelectedImageIndex = 2;
                MsgNode.Tag = Server;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, Server.Name, 7);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 1);

                // Add the nodes to each of the Parent Nodes

                //if (!tvwApplications.Nodes.Contains(AppNode))
                //{
                AppParentNode.Nodes.Add(AppNode);
                //}
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();
        }

        public void AddDrawingCriticalException(string Message, BusinessApplication Application)
        {
            Application.Drawing.InError = true;

            if (!_DrawingExceptions.Contains(Message))
            {
                _DrawingExceptions.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 5;
                AppNode.SelectedImageIndex = 5;
                AppNode.Tag = Application;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 5;
                MsgNode.SelectedImageIndex = 5;
                MsgNode.Tag = Application;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, Application.Name, 9);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 8);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();
        }

        public void AddException(string Message)
        {
            if (!_Exceptions.Contains(Message))
            {
                _Exceptions.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 1;
                AppNode.SelectedImageIndex = 1;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 1;
                MsgNode.SelectedImageIndex = 1;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, "Exceptions", 5);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 1);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();

        }

        public void AddException(string Message, BusinessApplication Application)
        {
            Application.Drawing.InError = true;

            if (!_Exceptions.Contains(Message))
            {
                _Exceptions.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 1;
                AppNode.SelectedImageIndex = 1;
                AppNode.Tag = Application;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 1;
                MsgNode.SelectedImageIndex = 1;
                MsgNode.Tag = Application;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, Application.Name, 6);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 1);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();

        }

        public void AddWarning(string Message)
        {
            if (!_Warnings.Contains(Message))
            {
                _Warnings.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 0;
                AppNode.SelectedImageIndex = 0;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 0;
                MsgNode.SelectedImageIndex = 0;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, "Warnings", 0);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 0);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();
        }

        public void AddWarning(string Message, BusinessApplication Application)
        {
            if (!_Warnings.Contains(Message))
            {
                _Warnings.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 0;
                AppNode.SelectedImageIndex = 0;
                AppNode.Tag = Application;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 0;
                MsgNode.SelectedImageIndex = 0;
                MsgNode.Tag = Application;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, Application.Name, 6);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 0);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();
        }

        public void AddWarning(string Message, Server Server)
        {
            if (!_Warnings.Contains(Message))
            {
                _Warnings.Add(Message);

                TreeNode AppParentNode = null;
                TreeNode MsgParentNode = null;
                TreeNode AppNode = new TreeNode();
                TreeNode MsgNode = new TreeNode();

                AppNode.Text = Message;
                AppNode.Name = Message;
                AppNode.ImageIndex = 0;
                AppNode.SelectedImageIndex = 0;
                AppNode.Tag = Server;

                MsgNode.Text = Message;
                MsgNode.Name = Message;
                MsgNode.ImageIndex = 0;
                MsgNode.SelectedImageIndex = 0;
                MsgNode.Tag = Server;

                string Code = GetCodeFromMessage(Message);
                AppParentNode = GetOrCreateParentNode(tvwApplications, Server.Name, 7);
                MsgParentNode = GetOrCreateParentNode(tvwMessages, Code, 0);

                // Add the nodes to each of the Parent Nodes
                AppParentNode.Nodes.Add(AppNode);
                MsgParentNode.Nodes.Add(MsgNode);
            }

            if (!this.Visible)
            {
                this.Show();
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            tvwApplications.Sort();
            tvwMessages.Sort();

            this.Refresh();
        }

        public void Clear()
        {
            _Warnings.Clear();
            _Exceptions.Clear();
            _DrawingExceptions.Clear();
            _DatabaseWarnings.Clear();
            _OutOfScopeServers.Clear();
            _OutOfScopeServices.Clear();
            tvwApplications.Nodes.Clear();
            tvwMessages.Nodes.Clear();
            lvwOutOfScope.Items.Clear();

            //this.Hide();
        }

        #endregion

        #region Private Methods

        private TreeNode GetOrCreateParentNode(TreeView TreeView, string Name, int ImageIndex)
        {
            TreeNode Result = null;

            TreeNode[] SearchResults = TreeView.Nodes.Find(Name, false);

            if (SearchResults.Length > 0)
            {
                Result = TreeView.Nodes.Find(Name, false)[0];
            }
            else
            {
                Result = TreeView.Nodes.Add(Name, Name, ImageIndex, ImageIndex);
            }

            return Result;
        }

        private string GetCodeFromMessage(string Message)
        {
            // Remove leading dots...
            string Code = Message.TrimStart('.');

            // The next characters should be [XXXYYYY], where YYYY is a number.  Grab these 4 digits
            Code = Code.Substring(4, 4);

            return Code;
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
                AddWarning(ErrorMessage, BusinessApplication);
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

            foreach (ListViewItem Item in lvwOutOfScope.SelectedItems)
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

        private void productionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(false, Global.Environment.Production);

            //bool Result = false;

            //foreach (ListViewItem Item in lvwOutOfScope.SelectedItems)
            //{
            //    BusinessApplication Service = (BusinessApplication)Item.Tag;

            //    frmMain MainForm = (frmMain)_ParentForm;

            //    if (Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus)
            //    {
            //        Debug.WriteLine("..." + Service.Name + "...", "information");
            //        Result = MainForm.CreateManagementPackFromBusinessApplication(Service, false, Global.Environment.Production);
            //    }
            //    else if (Service.Drawing != null && Service.Drawing.Status == Global.DrawingStatus.Approved)
            //    {
            //        Debug.WriteLine("..." + Service.Name + "...", "information");
            //        Result = MainForm.CreateManagementPackFromBusinessApplication(Service, false, Global.Environment.Production);
            //    }
            //    else
            //    {
            //        Debug.WriteLine("..." + Service.Name + " Drawing status is not Approved, so a Management Pack for this Service will not be created.", "information");
            //        Result = true;
            //    }

            //    if (Result)  // Success
            //    {

            //    }
            //    else         // Failure
            //    {
            //        Debug.WriteLine("A problem was encountered whilst updating or creating a Management Pack", "error");
            //    }
            //}

            //Debug.WriteLine("Management Pack creation request complete", "information");
        }

        private void productionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(true, Global.Environment.Production);

            //bool Result = false;

            //foreach (ListViewItem Item in lvwOutOfScope.SelectedItems)
            //{
            //    BusinessApplication Service = (BusinessApplication)Item.Tag;

            //    frmMain MainForm = (frmMain)_ParentForm;

            //    if (Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus)
            //    {
            //        Debug.WriteLine("..." + Service.Name + "...", "information");
            //        Result = MainForm.CreateManagementPackFromBusinessApplication(Service, true, Global.Environment.Production);
            //    }
            //    else if (Service.Drawing != null && Service.Drawing.Status == Global.DrawingStatus.Approved)
            //    {
            //        Debug.WriteLine("..." + Service.Name + "...", "information");
            //        Result = MainForm.CreateManagementPackFromBusinessApplication(Service, true, Global.Environment.Production);
            //    }
            //    else
            //    {
            //        Debug.WriteLine("..." + Service.Name + " Drawing status is not Approved, so a Management Pack for this Service will not be created.", "information");
            //        Result = true;
            //    }

            //    if (Result)  // Success
            //    {

            //    }
            //    else         // Failure
            //    {
            //        Debug.WriteLine("A problem was encountered whilst updating or creating a Management Pack", "error");
            //    }
            //}

            //Debug.WriteLine("Management Pack creation request complete", "information");
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(false, Global.Environment.Test);
        }

        private void developmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(false, Global.Environment.Development);
        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(true, Global.Environment.Test);
        }

        private void developmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateManagementPacksFromSelectedItems(true, Global.Environment.Development);
        }

        private void btnCopyMessages_Click(object sender, EventArgs e)
        {
            if (radMessage.Checked)
            {
            }
            else
            {
            }
        }
    }
}
