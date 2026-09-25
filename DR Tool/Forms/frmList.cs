using Microsoft.AGL.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ISA.Dependency;

namespace DRPlanningTool
{
    public partial class frmList : Form
    {
        #region Fields

        private List<BusinessApplication> _BusinessApplicationRunbook = null;
        private List<Server> _ServerRunbook = null;
        private List<Server> _ServerList = null;
        private bool _OKToBuildList = false;
        //private Form _ParentForm = null;
        private string _ListType = "Server List";
        private string _SearchFilter = "";

        #endregion

        #region Constructors

        public frmList()
        {
            InitializeComponent();
        }

        public frmList(string SRMType, List<Server> ServerRunbook, List<Server> ServerList, List<BusinessApplication> BusinessApplicationRunbook, Form ParentForm)
        {
            InitializeComponent();

            _ListType = SRMType;
            _ServerRunbook = ServerRunbook;
            _BusinessApplicationRunbook = BusinessApplicationRunbook;
            _ServerList = ServerList;
            //_ParentForm = ParentForm;
        }

        #endregion

        #region Event Handlers

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearchFilter.Clear();
            _SearchFilter = "";
            BuildList();

            txtSearchFilter.Focus();

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            StringBuilder Builder = new StringBuilder();

            foreach (ListViewItem Item in lvwRunbook.Items)
            {
                Builder.AppendLine(Item.Text);
            }

            if (Builder.Length > 0)
            {
                Clipboard.SetText(Builder.ToString());

                MessageBox.Show("Detail copied to clipboard", "Message");
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            // Move the item down

            Int32 SelectedItemIndex = lvwRunbook.SelectedItems[0].Index;
            ListViewItem SelectedItem = (ListViewItem)lvwRunbook.Items[SelectedItemIndex].Clone();

            lvwRunbook.BeginUpdate();

            lvwRunbook.Items[SelectedItemIndex].Remove();
            lvwRunbook.Items.Insert(SelectedItemIndex + 1, SelectedItem);
            lvwRunbook.Items[SelectedItemIndex + 1].Selected = true;

            lvwRunbook.EndUpdate();

            // Update the runbook

            switch (_ListType)
            {
                case "Server Runbook":

                    Server OriginalServer = _ServerRunbook[SelectedItemIndex];

                    _ServerRunbook.RemoveAt(SelectedItemIndex);
                    _ServerRunbook.Insert(SelectedItemIndex + 1, OriginalServer);
                    break;
                case "Business Application Runbook":
                    BusinessApplication OriginalService = _BusinessApplicationRunbook[SelectedItemIndex];

                    _BusinessApplicationRunbook.RemoveAt(SelectedItemIndex);
                    _BusinessApplicationRunbook.Insert(SelectedItemIndex + 1, OriginalService);
                    break;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string OutputHeader = "";
            System.IO.StreamWriter OutputWriter = null;
            string OutputFolder = Properties.Settings.Default.ExportFolderName;
            string Filename = Properties.Settings.Default.ExportFilename;
            string FullExportFilename = "";
            int GroupNumber = 0;
            bool TempFlag = false;

            // Replace Tags in the filename
            Filename = ISA.Helper.Methods.ReplaceTags(Filename, DateTime.Now, true).Replace("#LISTNAME#", _ListType);

            FullExportFilename = System.IO.Path.Combine(OutputFolder, Filename);

            DialogResult result = MessageBox.Show("Full filename for this export will be:\n\n'" + FullExportFilename + "'\n\nContinue?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                OutputWriter = new System.IO.StreamWriter(FullExportFilename);

                if (rbDisplayByBusinessApplication.Checked)
                {
                    OutputHeader = "Business Application|Component Servers|Component Business Applications|Stream|Desired Tier|Actual Tier|MP Version|Drawing Status|WI Name";
                }
                else if (rbDisplayByServer.Checked)
                {
                    if (lvwRunbook.ShowGroups)
                    {
                        OutputHeader = "#|Business Application|Server|Provided Business Applications|Dependent Business Applications|Dependent Business Application Component Servers|Dependent Business Application Component Services|Site|Operating System|Disks";
                    }
                    else
                    {
                        OutputHeader = "Server|Provided Business Applications|Dependent Business Applications|Dependent Business Application Component Servers|Dependent Business Application Component Services|Site|Operating System|Disks|Stream|Comment";
                    }
                }
                try
                {
                    OutputWriter.WriteLine(OutputHeader);

                    if (lvwRunbook.ShowGroups)
                    {
                        foreach (ListViewGroup Group in lvwRunbook.Groups)
                        {
                            #region Display by Application output

                            if (rbDisplayByBusinessApplication.Checked)
                            {
                                if (Group.Items.Count > 0)
                                {
                                    GroupNumber++;
                                }

                                foreach (ListViewItem Item in Group.Items)
                                {
                                    BusinessApplication BusinessApplication = (BusinessApplication)Item.Tag;

                                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                    StringBuilder Output = new StringBuilder();

                                    Output.Append((char)34 + BusinessApplication.Name + (char)34);
                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);

                                    // Get the Servers that are not in the Protected site
                                    List<Server> Servers = BusinessApplication.ComponentServers.Where(s => s.Stream != Global.RecoveryStream.A &&
                                                                                                      s.PhysicalSite.SiteType != PhysicalSite.PhysicalSiteType.Protected
                                                                                                     )
                                                                                                     .OrderBy(s => s.Name)
                                                                                                     .ToList();

                                    foreach (Server ComponentServer in Servers) //BusinessApplication.ComponentServers)
                                    {
                                        Output.Append(ComponentServer.Name);
                                        Output.Append(",");

                                    }

                                    if (Servers.Count > 0) //BusinessApplication.ComponentServers.Count > 0)
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                    }
                                    Output.Append((char)34);
                                    //////////////////////////////////

                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);
                                    foreach (BusinessApplication ComponentService in BusinessApplication.ComponentBusinessApplications)
                                    {
                                        Output.Append(ComponentService.Name);
                                        Output.Append(",");
                                    }

                                    if (BusinessApplication.ComponentBusinessApplications.Count > 0)
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                    }
                                    Output.Append((char)34);
                                    //////////////////////////////////

                                    Output.Append("|");

                                    ////////////////////////////////////
                                    Output.Append((char)34);
                                    switch (BusinessApplication.CalculatedStream)
                                    {
                                        case Global.RecoveryStream.A:
                                            Output.Append("A");
                                            break;
                                        case Global.RecoveryStream.B:
                                            Output.Append("B");
                                            break;
                                        case Global.RecoveryStream.C:
                                            Output.Append("C");
                                            break;
                                        case Global.RecoveryStream.D:
                                            Output.Append("D");
                                            break;
                                        case Global.RecoveryStream.E:
                                            Output.Append("E");
                                            break;
                                    }
                                    Output.Append((char)34);
                                    Output.Append("|");

                                    ////////////////////////////////////
                                    Output.Append((char)34);
                                    switch (BusinessApplication.DesiredTier)
                                    {
                                        case Global.RecoveryTier.Zero:
                                            Output.Append("0");
                                            break;
                                        case Global.RecoveryTier.One:
                                            Output.Append("1");
                                            break;
                                        case Global.RecoveryTier.Two:
                                            Output.Append("2");
                                            break;
                                        case Global.RecoveryTier.Three:
                                            Output.Append("3");
                                            break;
                                        case Global.RecoveryTier.Four:
                                            Output.Append("4");
                                            break;
                                        case Global.RecoveryTier.Undefined:
                                            Output.Append("X");
                                            break;
                                    }

                                    Output.Append((char)34);
                                    Output.Append("|");

                                    ////////////////////////////////////
                                    Output.Append((char)34);
                                    switch (BusinessApplication.ActualTier)
                                    {
                                        case Global.RecoveryTier.Zero:
                                            Output.Append("0");
                                            break;
                                        case Global.RecoveryTier.One:
                                            Output.Append("1");
                                            break;
                                        case Global.RecoveryTier.Two:
                                            Output.Append("2");
                                            break;
                                        case Global.RecoveryTier.Three:
                                            Output.Append("3");
                                            break;
                                        case Global.RecoveryTier.Four:
                                            Output.Append("4");
                                            break;
                                        case Global.RecoveryTier.Undefined:
                                            Output.Append("X");
                                            break;
                                    }

                                    Output.Append((char)34);
                                    Output.Append("|");
                                    Version Version = new Version(Application.ProductVersion);

                                    ////////////////////////////////////
                                    Output.Append((char)34);
                                    Output.Append(Version.Major + "." + Version.Minor + "." + BusinessApplication.Drawing.Version + "." + BusinessApplication.Version);
                                    Output.Append((char)34);
                                    Output.Append("|");
                                    ////////////////////////////////////
                                    Output.Append((char)34);
                                    Output.Append(BusinessApplication.Drawing.Status.ToString());
                                    Output.Append((char)34);
                                    Output.Append("|");
                                    ////////////////////////////////////
                                    Output.Append((char)34);
                                    Output.Append(BusinessApplication.WorkInstructionName);
                                    Output.Append((char)34);

                                    OutputWriter.WriteLine(Output);

                                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                }
                            }

                            #endregion

                            #region Display by Server output

                            else if (rbDisplayByServer.Checked)
                            {
                                if (Group.Items.Count > 0)
                                {
                                    GroupNumber++;
                                }

                                foreach (ListViewItem Item in Group.Items)
                                {
                                    Server Server = (Server)Item.Tag;
                                    StringBuilder Output = new StringBuilder();

                                    Output.Append((char)34 + GroupNumber.ToString() + (char)34);
                                    Output.Append("|");

                                    //////////////////////////////////

                                    Output.Append((char)34 + Group.Header + (char)34);
                                    Output.Append("|");

                                    //////////////////////////////////

                                    Output.Append((char)34 + Server.Name + (char)34);
                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);
                                    foreach (BusinessApplication Service in Server.ProvidedBusinessApplications)
                                    {
                                        Output.Append(Service.Name);
                                        Output.Append(",");

                                    }

                                    if (Server.ProvidedBusinessApplications.Count > 0)
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                    }
                                    Output.Append((char)34);
                                    //////////////////////////////////

                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);
                                    foreach (BusinessApplication DependentService in Server.DependentBusinessApplications)
                                    {
                                        Output.Append(DependentService.Name);
                                        Output.Append(",");
                                    }

                                    if (Server.DependentBusinessApplications.Count > 0)
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                    }
                                    Output.Append((char)34);
                                    //////////////////////////////////

                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);
                                    foreach (BusinessApplication DependentService in Server.DependentBusinessApplications)
                                    {
                                        foreach (Server AnotherServer in DependentService.ComponentServers)
                                        {
                                            Output.Append(AnotherServer.Name);
                                            Output.Append(",");
                                        }
                                    }

                                    if (Server.DependentBusinessApplications.Count > 0)
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                    }
                                    Output.Append((char)34);

                                    //////////////////////////////////

                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);
                                    List<BusinessApplication> DependentServices = new List<BusinessApplication>();
                                    foreach (BusinessApplication DependentService in Server.DependentBusinessApplications)
                                    {
                                        foreach (BusinessApplication AnotherService in DependentService.ComponentBusinessApplications)
                                        {
                                            if (AnotherService.Name != DependentService.Name)
                                            {
                                                if (Global.GetExistingBusinessApplication(AnotherService, DependentServices) == null)
                                                {
                                                    DependentServices.Add(AnotherService);
                                                    Output.Append(AnotherService.Name);
                                                    Output.Append(",");
                                                }
                                            }
                                        }
                                    }

                                    if (Server.DependentBusinessApplications.Count > 0)
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                    }
                                    Output.Append((char)34);

                                    //////////////////////////////////

                                    Output.Append("|");

                                    ////////////////////////////////////

                                    Output.Append((char)34 + Server.PhysicalSite.Name + (char)34);

                                    //////////////////////////////////

                                    Output.Append("|");

                                    ////////////////////////////////////

                                    Output.Append((char)34 + Server.OperatingSystem + (char)34);

                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);
                                    foreach (Disk Disk in Server.Disks)
                                    {
                                        Output.Append(Disk.Name + " = " + Disk.StorageName + " (" + Disk.TotalAllocatedMB + " GB)");
                                        Output.Append(",");

                                    }
                                    Output.Remove(Output.Length - 1, 1);
                                    Output.Append((char)34);

                                    Output.Append("|");

                                    ////////////////////////////////////

                                    Output.Append((char)34 + Server.Stream + (char)34);

                                    //////////////////////////////////

                                    Output.Append("|");

                                    ////////////////////////////////////
                                    Output.Append((char)34);

                                    Output.Append(ServerAdminText(Server));

                                    Output.Append((char)34);

                                    //////////////////////////////////

                                    OutputWriter.WriteLine(Output);
                                }
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        #region Display by Application output

                        if (rbDisplayByBusinessApplication.Checked)
                        {
                            foreach (ListViewItem Item in lvwRunbook.Items)
                            {
                                BusinessApplication BusinessApplication = (BusinessApplication)Item.Tag;

                                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                StringBuilder Output = new StringBuilder();

                                Output.Append((char)34 + BusinessApplication.Name + (char)34);
                                Output.Append("|");

                                //////////////////////////////////
                                Output.Append((char)34);

                                // Get the Servers that are not in the Protected site
                                List<Server> Servers = BusinessApplication.ComponentServers.Where(s => s.Stream != Global.RecoveryStream.A &&
                                                                                                  s.PhysicalSite.SiteType != PhysicalSite.PhysicalSiteType.Protected
                                                                                                 )
                                                                                                 .OrderBy(s => s.Name)
                                                                                                 .ToList();

                                foreach (Server ComponentServer in Servers) //BusinessApplication.ComponentServers)
                                {
                                    Output.Append(ComponentServer.Name);
                                    Output.Append(",");

                                }

                                if (Servers.Count > 0) //BusinessApplication.ComponentServers.Count > 0)
                                {
                                    Output.Remove(Output.Length - 1, 1);
                                }
                                Output.Append((char)34);
                                //////////////////////////////////

                                Output.Append("|");

                                //////////////////////////////////
                                Output.Append((char)34);
                                foreach (BusinessApplication ComponentService in BusinessApplication.ComponentBusinessApplications)
                                {
                                    Output.Append(ComponentService.Name);
                                    Output.Append(",");
                                }

                                if (BusinessApplication.ComponentBusinessApplications.Count > 0)
                                {
                                    Output.Remove(Output.Length - 1, 1);
                                }
                                Output.Append((char)34);
                                //////////////////////////////////

                                Output.Append("|");

                                ////////////////////////////////////
                                Output.Append((char)34);
                                switch (BusinessApplication.CalculatedStream)
                                {
                                    case Global.RecoveryStream.A:
                                        Output.Append("A");
                                        break;
                                    case Global.RecoveryStream.B:
                                        Output.Append("B");
                                        break;
                                    case Global.RecoveryStream.C:
                                        Output.Append("C");
                                        break;
                                    case Global.RecoveryStream.D:
                                        Output.Append("D");
                                        break;
                                    case Global.RecoveryStream.E:
                                        Output.Append("E");
                                        break;
                                }
                                Output.Append((char)34);
                                Output.Append("|");

                                Output.Append((char)34);
                                switch (BusinessApplication.DesiredTier)
                                {
                                    case Global.RecoveryTier.Zero:
                                        Output.Append("0");
                                        break;
                                    case Global.RecoveryTier.One:
                                        Output.Append("1");
                                        break;
                                    case Global.RecoveryTier.Two:
                                        Output.Append("2");
                                        break;
                                    case Global.RecoveryTier.Three:
                                        Output.Append("3");
                                        break;
                                    case Global.RecoveryTier.Four:
                                        Output.Append("4");
                                        break;
                                    case Global.RecoveryTier.Undefined:
                                        Output.Append("X");
                                        break;
                                }

                                Output.Append((char)34);
                                Output.Append("|");

                                Output.Append((char)34);
                                switch (BusinessApplication.ActualTier)
                                {
                                    case Global.RecoveryTier.Zero:
                                        Output.Append("0");
                                        break;
                                    case Global.RecoveryTier.One:
                                        Output.Append("1");
                                        break;
                                    case Global.RecoveryTier.Two:
                                        Output.Append("2");
                                        break;
                                    case Global.RecoveryTier.Three:
                                        Output.Append("3");
                                        break;
                                    case Global.RecoveryTier.Four:
                                        Output.Append("4");
                                        break;
                                    case Global.RecoveryTier.Undefined:
                                        Output.Append("X");
                                        break;
                                }

                                Output.Append((char)34);
                                Output.Append("|");
                                Version Version = new Version(Application.ProductVersion);

                                ////////////////////////////////////
                                Output.Append((char)34);
                                Output.Append(Version.Major + "." + Version.Minor + "." + BusinessApplication.Drawing.Version + "." + BusinessApplication.Version);
                                Output.Append((char)34);
                                Output.Append("|");
                                ////////////////////////////////////
                                Output.Append((char)34);
                                Output.Append(BusinessApplication.Drawing.Status.ToString());
                                Output.Append((char)34);
                                Output.Append("|");
                                ////////////////////////////////////
                                Output.Append((char)34);
                                Output.Append(BusinessApplication.WorkInstructionName);
                                Output.Append((char)34);

                                OutputWriter.WriteLine(Output);

                                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                        }

                        #endregion

                        #region Display by Server output

                        else if (rbDisplayByServer.Checked)
                        {
                            foreach (ListViewItem Item in lvwRunbook.Items)
                            {
                                Server Server = (Server)Item.Tag;
                                StringBuilder Output = new StringBuilder();

                                if (true || Server.Stream != Global.RecoveryStream.A && Server.PhysicalSite.SiteType != PhysicalSite.PhysicalSiteType.Protected)
                                {
                                    Output.Append((char)34 + Server.Name + (char)34);
                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);
                                    foreach (BusinessApplication BusinessApplication in Server.ProvidedBusinessApplications)
                                    {
                                        Output.Append(BusinessApplication.Name);
                                        Output.Append(",");
                                        TempFlag = true;
                                    }
                                    if (TempFlag) // We only want to remove the last character if we added a comma last
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                        TempFlag = false;
                                    }
                                    Output.Append((char)34);
                                    //////////////////////////////////

                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);

                                    foreach (BusinessApplication DependentBusinessApplication in Server.DependentBusinessApplications)
                                    {
                                        Output.Append(DependentBusinessApplication.Name);
                                        Output.Append(",");
                                    }
                                    if (TempFlag) // We only want to remove the last character if we added a comma last
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                        TempFlag = false;
                                    }
                                    Output.Append((char)34);
                                    //////////////////////////////////

                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);
                                    foreach (BusinessApplication DependentBusinessApplication in Server.DependentBusinessApplications)
                                    {
                                        foreach (Server AnotherServer in DependentBusinessApplication.ComponentServers)
                                        {
                                            Output.Append(AnotherServer.Name);
                                            Output.Append(",");
                                        }
                                    }
                                    if (TempFlag) // We only want to remove the last character if we added a comma last
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                        TempFlag = false;
                                    }
                                    Output.Append((char)34);

                                    //////////////////////////////////

                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);
                                    List<BusinessApplication> DependentBusinessApplications = new List<BusinessApplication>();
                                    foreach (BusinessApplication DependentBusinessApplication in Server.DependentBusinessApplications)
                                    {
                                        foreach (BusinessApplication AnotherService in DependentBusinessApplication.ComponentBusinessApplications)
                                        {
                                            if (AnotherService.Name != DependentBusinessApplication.Name)
                                            {
                                                if (Global.GetExistingBusinessApplication(AnotherService, DependentBusinessApplications) == null)
                                                {
                                                    DependentBusinessApplications.Add(AnotherService);
                                                    Output.Append(AnotherService.Name);
                                                    Output.Append(",");
                                                }
                                            }
                                        }
                                    }
                                    if (TempFlag) // We only want to remove the last character if we added a comma last
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                        TempFlag = false;
                                    }
                                    Output.Append((char)34);

                                    //////////////////////////////////

                                    Output.Append("|");

                                    ////////////////////////////////////

                                    Output.Append((char)34 + Server.PhysicalSite.Name + (char)34);

                                    //////////////////////////////////

                                    Output.Append("|");

                                    ////////////////////////////////////

                                    Output.Append((char)34 + Server.OperatingSystem + (char)34);

                                    Output.Append("|");

                                    //////////////////////////////////
                                    Output.Append((char)34);
                                    foreach (Disk Disk in Server.Disks)
                                    {
                                        Output.Append(Disk.Name + " = " + Disk.StorageName + " (" + Disk.TotalAllocatedMB + " GB)");
                                        Output.Append(",");

                                    }
                                    if (TempFlag) // We only want to remove the last character if we added a comma last
                                    {
                                        Output.Remove(Output.Length - 1, 1);
                                        TempFlag = false;
                                    }
                                    Output.Append((char)34);

                                    Output.Append("|");

                                    ////////////////////////////////////

                                    Output.Append((char)34 + Server.Stream.ToString() + (char)34);

                                    //////////////////////////////////

                                    Output.Append("|");

                                    ////////////////////////////////////
                                    Output.Append((char)34);

                                    Output.Append(ServerAdminText(Server));

                                    Output.Append((char)34);

                                    //////////////////////////////////

                                    OutputWriter.WriteLine(Output);
                                }
                            }
                        }

                        #endregion
                    }

                    OutputWriter.Flush();
                    OutputWriter.Close();
                    OutputWriter.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error whilst exporting file:\n\n" + ex.ToString());
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            _SearchFilter = txtSearchFilter.Text.Trim();
            BuildList();

            txtSearchFilter.Focus();
            txtSearchFilter.SelectionStart = 0;
            txtSearchFilter.SelectAll();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            BuildList();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            //foreach (int SelectedItemIndex in lvwRunbook.SelectedIndices)
            //{
            // Move the item up

            Int32 SelectedItemIndex = lvwRunbook.SelectedItems[0].Index;
            ListViewItem SelectedItem = (ListViewItem)lvwRunbook.Items[SelectedItemIndex].Clone();

            lvwRunbook.BeginUpdate();

            lvwRunbook.Items[SelectedItemIndex].Remove();
            lvwRunbook.Items.Insert(SelectedItemIndex - 1, SelectedItem);
            lvwRunbook.Items[SelectedItemIndex - 1].Selected = true;

            lvwRunbook.EndUpdate();

            // Update the runbook

            switch (_ListType)
            {
                case "Server Runbook":

                    Server OriginalServer = _ServerRunbook[SelectedItemIndex];

                    _ServerRunbook.RemoveAt(SelectedItemIndex);
                    _ServerRunbook.Insert(SelectedItemIndex - 1, OriginalServer);
                    break;
                case "Business Application Runbook":

                    BusinessApplication OriginalApplication = _BusinessApplicationRunbook[SelectedItemIndex];

                    _BusinessApplicationRunbook.RemoveAt(SelectedItemIndex);
                    _BusinessApplicationRunbook.Insert(SelectedItemIndex - 1, OriginalApplication);
                    break;
            }
            //}
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.SelectedIndex > -1 && _OKToBuildList) BuildList();
        }

        private void cmbRunbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ListType = cmbRunbook.Text;
            PerformUpdate();
        }

        private void createManagementPackToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmList_Resize(object sender, EventArgs e)
        {
            ResizeColumns();
        }

        private void frmList_Shown(object sender, EventArgs e)
        {
            // Set location and size
            if (Properties.Settings.Default.frmListLocation.X != 0 && Properties.Settings.Default.frmListLocation.Y != 0)
            {
                this.Location = Properties.Settings.Default.frmListLocation;
            }

            if (Properties.Settings.Default.frmListSize.Width != 0 && Properties.Settings.Default.frmListSize.Height != 0)
            {
                this.Size = Properties.Settings.Default.frmListSize;
            }

            switch (lvwRunbook.Columns.Count)
            {
                case 1:
                    lvwRunbook.Columns[0].Width = lvwRunbook.Size.Width - 28;
                    break;
                case 2:
                    lvwRunbook.Columns[0].Width = lvwRunbook.Size.Width - 28 - (lvwRunbook.Columns[1].Width);
                    break;
                case 3:
                    lvwRunbook.Columns[0].Width = lvwRunbook.Size.Width - 28 - (lvwRunbook.Columns[1].Width + lvwRunbook.Columns[2].Width);
                    break;
            }

            this.Activate();
        }

        private void frmRunbook_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.RemoveRunbookFormFromList(this);

            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.frmListSize = this.Size;

                if (ISA.Helper.Properties.AllowSaveFormLocations)
                {
                    Properties.Settings.Default.frmListLocation = this.Location;
                }
            }
        }

        private void frmRunbook_Load(object sender, EventArgs e)
        {
            cmbRunbook.Items.Add("Business Application Runbook");
            cmbRunbook.Items.Add("Server List");
            cmbRunbook.Items.Add("Server Runbook");
            cmbRunbook.Items.Add("SRM Runbook");

            //cmbRunbook.Items.Add("Custom");

            // Set the combo-box according to the list type specified
            switch (_ListType)
            {
                case "Server Runbook":
                    cmbRunbook.SelectedIndex = 2;

                    break;
                case "Business Application Runbook":
                    cmbRunbook.SelectedIndex = 0;

                    break;
                case "SRM Runbook":
                    cmbRunbook.SelectedIndex = 3;

                    break;
                case "Server List":
                    cmbRunbook.SelectedIndex = 1;

                    break;
            }

            lvwRunbook.Select();
        }

        private void lvwRunbook_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && (_ListType == "Business Application Runbook" || _ListType == "Server Runbook" || _ListType == "Server List"))
            {
                if (lvwRunbook.SelectedItems.Count > 0)
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

        private void lvwRunbook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwRunbook.SelectedItems[0] != null)
            {
                //frmMain MainForm = (frmMain)_ParentForm;

                if (rbDisplayByServer.Checked)
                {
                    Server Server = (Server)lvwRunbook.SelectedItems[0].Tag;
                    Main.CreateOrSelectServerForm(ref Server);
                }
                else
                {
                    BusinessApplication Service = (BusinessApplication)lvwRunbook.SelectedItems[0].Tag;
                    Main.CreateOrSelectBusinessApplicationForm(lvwRunbook.SelectedItems[0].Text, Service);
                }
            }
        }

        private void lvwRunbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            Server LoopedServer = null;
            BusinessApplication LoopedService = null;

            // Reset all background colours

            if (lvwRunbook.SelectedItems.Count > 1)
            {
                lvwRunbook.BeginUpdate();
                for (i = 0; i < lvwRunbook.Items.Count; i++)
                {
                    lvwRunbook.Items[i].BackColor = SystemColors.Window;
                }
                lvwRunbook.EndUpdate();
            }

            switch (_ListType)
            {
                case "Server Runbook":

                    #region Server Runbook

                    if (lvwRunbook.SelectedItems.Count == 0)
                    {
                        lvwRunbook.BeginUpdate();
                        for (i = 0; i < lvwRunbook.Items.Count; i++)
                        {
                            lvwRunbook.Items[i].BackColor = SystemColors.Window;
                        }
                        lvwRunbook.EndUpdate();

                        btnUp.Enabled = false;
                        btnDown.Enabled = false;
                    }
                    else
                    {
                        foreach (ListViewItem Item in lvwRunbook.SelectedItems)
                        {
                            //ListViewItem Item = lvwRunbook.SelectedItems[0];
                            btnUp.Enabled = false;
                            btnDown.Enabled = false;

                            string ItemTypeName = Item.Tag.GetType().Name;
                            Server SelectedServer = (Server)Item.Tag;
                            Int32 Index = Item.Index;

                            if (lvwRunbook.SelectedItems.Count == 1)
                            {
                                // Find out what the Item before this one is
                                if (Index > 0)
                                {
                                    Server ServerBeforeThisOne = (Server)lvwRunbook.Items[Index - 1].Tag;

                                    if (ServerBeforeThisOne.SRMRecoveryIndex == SelectedServer.SRMRecoveryIndex)
                                    {
                                        btnUp.Enabled = true;
                                    }
                                    else
                                    {
                                        btnUp.Enabled = false;
                                    }
                                }
                                else
                                {
                                    btnUp.Enabled = false;
                                }
                            }

                            if (Index < lvwRunbook.Items.Count - 1)
                            {
                                Server ServerAfterThisOne = (Server)lvwRunbook.Items[Index + 1].Tag;

                                if (lvwRunbook.SelectedItems.Count == 1)
                                {
                                    if (ServerAfterThisOne.SRMRecoveryIndex == SelectedServer.SRMRecoveryIndex)
                                    {
                                        btnDown.Enabled = true;
                                    }
                                    else
                                    {
                                        btnDown.Enabled = false;
                                    }
                                }
                                else
                                {
                                    btnDown.Enabled = false;
                                }
                            }

                            if (Properties.Settings.Default.ShowPossibleServerLocations && lvwRunbook.SelectedItems.Count == 1)
                            {
                                lvwRunbook.BeginUpdate();

                                i = Item.Index;
                                LoopedServer = (Server)lvwRunbook.Items[i].Tag;

                                // Colour Down
                                while (i > -1 && LoopedServer.SRMRecoveryIndex == SelectedServer.SRMRecoveryIndex)
                                {
                                    lvwRunbook.Items[i].BackColor = Color.FromName(Properties.Settings.Default.ServerBackgroundColour);
                                    i--;
                                    if (i > -1) LoopedServer = (Server)lvwRunbook.Items[i].Tag;
                                }

                                i = Item.Index;
                                LoopedServer = (Server)lvwRunbook.Items[i].Tag;

                                // Colour Up
                                while (i < lvwRunbook.Items.Count && LoopedServer.SRMRecoveryIndex == SelectedServer.SRMRecoveryIndex)
                                {
                                    lvwRunbook.Items[i].BackColor = Color.FromName(Properties.Settings.Default.ServerBackgroundColour);
                                    i++;
                                    if (i < lvwRunbook.Items.Count - 1) LoopedServer = (Server)lvwRunbook.Items[i].Tag;
                                }

                                lvwRunbook.EndUpdate();
                            }
                        }
                    }

                    #endregion

                    break;
                case "Business Application Runbook":

                    #region Business Application Runbook

                    if (lvwRunbook.SelectedItems.Count == 0)
                    {
                        lvwRunbook.BeginUpdate();
                        for (i = 0; i < lvwRunbook.Items.Count; i++)
                        {
                            lvwRunbook.Items[i].BackColor = SystemColors.Window;
                        }
                        lvwRunbook.EndUpdate();

                        btnUp.Enabled = false;
                        btnDown.Enabled = false;
                    }
                    else
                    {
                        foreach (ListViewItem Item in lvwRunbook.SelectedItems)
                        {
                            //ListViewItem Item = lvwRunbook.SelectedItems[0];
                            btnUp.Enabled = false;
                            btnDown.Enabled = false;

                            string ItemTypeName = Item.Tag.GetType().Name;
                            BusinessApplication SelectedBusinessApplication = (BusinessApplication)Item.Tag;
                            Int32 Index = Item.Index;

                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("Business Application: " + SelectedBusinessApplication.Name + ". Runbook Order: " + SelectedBusinessApplication.RunbookRegion, "information");
                            }

                            if (lvwRunbook.SelectedItems.Count > 0)
                            {
                                // Find out what the Item before this one is
                                if (Index > 0)
                                {
                                    BusinessApplication ServiceBeforeThisOne = (BusinessApplication)lvwRunbook.Items[Index - 1].Tag;

                                    if (ServiceBeforeThisOne.RunbookRegion == SelectedBusinessApplication.RunbookRegion)
                                    {
                                        btnUp.Enabled = true;
                                    }
                                    else
                                    {
                                        btnUp.Enabled = false;
                                    }
                                }
                                else
                                {
                                    btnUp.Enabled = false;
                                }

                                if (Index < lvwRunbook.Items.Count - 1)
                                {
                                    BusinessApplication ServiceAfterThisOne = (BusinessApplication)lvwRunbook.Items[Index + 1].Tag;

                                    if (ServiceAfterThisOne.RunbookRegion == SelectedBusinessApplication.RunbookRegion)
                                    {
                                        btnDown.Enabled = true;
                                    }
                                    else
                                    {
                                        btnDown.Enabled = false;
                                    }
                                }
                                else
                                {
                                    btnDown.Enabled = false;
                                }
                            }

                            if (Properties.Settings.Default.ShowPossibleServiceLocations && lvwRunbook.SelectedItems.Count == 1)
                            {
                                lvwRunbook.BeginUpdate();

                                i = Item.Index;
                                LoopedService = (BusinessApplication)lvwRunbook.Items[i].Tag;

                                // Colour Down
                                while (i > -1 && LoopedService.RunbookRegion == SelectedBusinessApplication.RunbookRegion)
                                {
                                    lvwRunbook.Items[i].BackColor = Color.FromName(Properties.Settings.Default.BusinessApplicationBackgroundColour);
                                    i--;
                                    if (i > -1) LoopedService = (BusinessApplication)lvwRunbook.Items[i].Tag;
                                }

                                i = Item.Index;
                                LoopedService = (BusinessApplication)lvwRunbook.Items[i].Tag;

                                // Colour Up
                                while (i < lvwRunbook.Items.Count && LoopedService.RunbookRegion == SelectedBusinessApplication.RunbookRegion)
                                {
                                    lvwRunbook.Items[i].BackColor = Color.FromName(Properties.Settings.Default.BusinessApplicationBackgroundColour);
                                    i++;
                                    if (i < lvwRunbook.Items.Count - 1) LoopedService = (BusinessApplication)lvwRunbook.Items[i].Tag;
                                }

                                lvwRunbook.EndUpdate();
                            }

                            if (true) // Placeholder for settings based colouring of Dependents and components
                            {
                                // Colour components and Dependents

                                if (lvwRunbook.SelectedItems.Count > 0)
                                {
                                    Index = 0;

                                    while (Index < lvwRunbook.Items.Count)
                                    {
                                        BusinessApplication ServiceUnderInspection = (BusinessApplication)lvwRunbook.Items[Index].Tag;

                                        Color CurrentColour = lvwRunbook.Items[Index].BackColor;

                                        if (SelectedBusinessApplication.ComponentBusinessApplications.Contains(ServiceUnderInspection))
                                        {
                                            Relationship Relationship = SelectedBusinessApplication.GetRelationship(ServiceUnderInspection);

                                            if (Relationship != null)
                                            {
                                                if (Relationship.SystemMandatory)
                                                {
                                                    lvwRunbook.Items[Index].BackColor = Color.Peru;
                                                }
                                                else
                                                {
                                                    lvwRunbook.Items[Index].BackColor = Color.Orange;
                                                }
                                            }
                                        }

                                        if (SelectedBusinessApplication.DependentBusinessApplications.Contains(ServiceUnderInspection))
                                        {
                                            Relationship Relationship = ServiceUnderInspection.GetRelationship(SelectedBusinessApplication);

                                            if (Relationship != null)
                                            {
                                                if (Relationship.SystemMandatory)
                                                {
                                                    lvwRunbook.Items[Index].BackColor = Color.DarkKhaki;
                                                }
                                                else
                                                {
                                                    lvwRunbook.Items[Index].BackColor = Color.Khaki;
                                                }
                                            }
                                        }

                                        Index++;
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    break;

                case "Server List":

                    break;

            }

            // For now, prevent moving multiple items at the same time
            if (lvwRunbook.SelectedItems.Count > 1)
            {
                btnUp.Enabled = false;
                btnDown.Enabled = false;
            }

        }

        private void rbAscending_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAscending.Checked && _OKToBuildList) BuildList();
        }

        private void rbDescending_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDescending.Checked && _OKToBuildList) BuildList();
        }

        private void rbDisplayByServer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDisplayByServer.Checked && _OKToBuildList) BuildList();
        }

        private void rbDisplayByBusinessApplication_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDisplayByBusinessApplication.Checked && _OKToBuildList) BuildList();
        }

        private void rbGroupByNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGroupByNone.Checked && _OKToBuildList) BuildList();
        }

        private void rbGroupByBusinessApplication_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGroupByBusinessApplication.Checked && _OKToBuildList) BuildList();
        }

        private void rbGroupByStream_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGroupByStream.Checked && _OKToBuildList) BuildList();
        }

        private void rbGroupByTier_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGroupByTier.Checked && _OKToBuildList) BuildList();
        }

        private void rbSortByNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSortByNone.Checked && _OKToBuildList) BuildList();
        }

        private void ViewDependencyMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<BusinessApplication> Services = new List<BusinessApplication>();

            if (_ListType == "Server Runbook" || _ListType == "Server List")
            {
                foreach (ListViewItem Item in lvwRunbook.SelectedItems)
                {
                    foreach (BusinessApplication Service in ((Server)Item.Tag).ProvidedBusinessApplications)
                    {
                        Services.Add(Service);
                    }
                }
            }

            if (_ListType == "Business Application Runbook")
            {
                foreach (ListViewItem Item in lvwRunbook.SelectedItems)
                {
                    Services.Add((BusinessApplication)Item.Tag);
                }
            }

            Main.CreateOrSelectDependencyMapForm(Services, true, Services, LayoutMethod.SugiyamaScheme, false);

        }

        private void txtSearchFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                _SearchFilter = txtSearchFilter.Text;

                BuildList();

                txtSearchFilter.SelectAll();
            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Result = false;

            foreach (ListViewItem Item in lvwRunbook.SelectedItems)
            {
                BusinessApplication Service = (BusinessApplication)Item.Tag;

                //frmMain MainForm = (frmMain)_ParentForm;

                if (Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus)
                {
                    Debug.WriteLine("..." + Service.Name + "...", "information");
                    Result = Main.CreateManagementPackFromBusinessApplication(Service, false, Global.Environment.Production);
                }
                else if (Service.Drawing != null && Service.Drawing.Status == Global.DrawingStatus.Approved)
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

            Debug.WriteLine("Management Pack creation request complete", "information");
            Debug.WriteLine("======= " + DateTime.Now.ToString() + " ================================================================\n", "information");
        }

        private void createStubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Result = false;

            foreach (ListViewItem Item in lvwRunbook.SelectedItems)
            {
                BusinessApplication Service = (BusinessApplication)Item.Tag;

                //frmMain MainForm = (frmMain)_ParentForm;

                if (Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus)
                {
                    Debug.WriteLine("..." + Service.Name + "...", "information");
                    Result = Main.CreateManagementPackFromBusinessApplication(Service, true, Global.Environment.Production);
                }
                else if (Service.Drawing != null && Service.Drawing.Status == Global.DrawingStatus.Approved)
                {
                    Debug.WriteLine("..." + Service.Name + "...", "information");
                    Result = Main.CreateManagementPackFromBusinessApplication(Service, true, Global.Environment.Production);
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
                    Debug.WriteLine("A problem was encountered whilst updating or creating a Management Pack", "information");
                }
            }

            Debug.WriteLine("Management Pack stub creation request complete", "information");
            Debug.WriteLine("======= " + DateTime.Now.ToString() + " ================================================================\n", "information");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwRunbook.SelectedItems[0] != null)
            {
                //frmMain MainForm = (frmMain)_ParentForm;

                if (rbDisplayByServer.Checked)
                {
                    Server Server = (Server)lvwRunbook.SelectedItems[0].Tag;
                    Main.CreateOrSelectServerForm(ref Server);
                }
                else
                {
                    BusinessApplication Service = (BusinessApplication)lvwRunbook.SelectedItems[0].Tag;
                    Main.CreateOrSelectBusinessApplicationForm(lvwRunbook.SelectedItems[0].Text, Service);
                }
            }
        }

        private void lvwRunbook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            {
                //frmMain MainForm = (frmMain)_ParentForm;

                if (lvwRunbook.SelectedItems[0].Tag is BusinessApplication)
                {
                    BusinessApplication Service = (BusinessApplication)lvwRunbook.SelectedItems[0].Tag;
                    Main.CreateOrSelectBusinessApplicationForm(lvwRunbook.SelectedItems[0].Text, Service);
                }
                else if (lvwRunbook.SelectedItems[0].Tag is Server)
                {
                    Server ThisServer = (Server)lvwRunbook.SelectedItems[0].Tag;
                    Main.CreateOrSelectServerForm(ref ThisServer);
                }

            }

            if (e.KeyCode == Keys.A && e.Control)
            {
                foreach (ListViewItem Item in lvwRunbook.Items)
                {
                    Item.Selected = true;
                }

            }
        }

        private void CopyNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder Builder = new StringBuilder();

            if (lvwRunbook.SelectedItems.Count > 0)
            {
                // Copy the selected names

                foreach (ListViewItem Item in lvwRunbook.SelectedItems)
                {
                    Builder.AppendLine(Item.Text);
                }


            }
            else
            {
                // Copy all names

                foreach (ListViewItem Item in lvwRunbook.Items)
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

        #endregion

        #region Private Methods

        private void BuildList()
        {
            ListViewGroup Group = null;
            ListViewItem Item = null;
            List<Server> Servers = null;

            if (!_OKToBuildList) return;

            lvwRunbook.Groups.Clear();
            lvwRunbook.Items.Clear();

            if (_ListType == "Server List")
            {
                Servers = _ServerList;
            }
            else
            {
                Servers = _ServerRunbook;
            }

            #region Display By Server

            if (rbDisplayByServer.Checked) // Server List and Server Runbook
            {
                #region Group By [None]

                if (rbGroupByNone.Checked) // Server List
                {
                    lvwRunbook.ShowGroups = false;

                    lvwRunbook.Items.Clear();

                    lvwRunbook.Columns.Clear();
                    lvwRunbook.Columns.Add("Name");
                    lvwRunbook.Columns.Add("Stream");
                    lvwRunbook.Columns[1].Width = 60;

                    ResizeColumns();

                    foreach (Server Server in Servers)
                    {
                        bool AddServerToList = false;

                        Item = new ListViewItem(Server.Name);
                        Item.Tag = Server;
                        Item.ImageIndex = Global.GetServerStateImageIndex(Server);

                        switch (_ListType)
                        {
                            case "Server Runbook":

                                if (Server.PhysicalSite.SiteType == PhysicalSite.PhysicalSiteType.Protected && (Server.Stream == Global.RecoveryStream.B || Server.Stream == Global.RecoveryStream.C) && Server.Virtual == true)
                                {
                                    // Determine if the applications this server provides are InScope
                                    AddServerToList = Server.ProvidedBusinessApplications.Any(i => i.InScope || i.OverrideInScope);

                                    #region Filter By ...

                                    switch (cmbFilter.Text)
                                    {
                                        case "Stream":
                                            AddServerToList = GetAddToListResult(Server.Stream, Server.Name, _SearchFilter);
                                            break;
                                        case "Tier":
                                            AddServerToList = GetAddToListResult(Server.Tier, Server.Name, _SearchFilter);
                                            break;
                                        case "Service":
                                            foreach (BusinessApplication ProvidedService in Server.ProvidedBusinessApplications)
                                            {
                                                if (ProvidedService.Name.ToLower().Trim().Contains(txtFilter.Text.ToLower().Trim()))
                                                {
                                                    AddServerToList = true;
                                                }
                                            }
                                            break;
                                        case "None":
                                            AddServerToList = GetAddToListResult(Server, _SearchFilter);
                                            break;
                                    }

                                    #endregion
                                }
                                else
                                {
                                    //Debug.Print("Not adding " + Server.Name + " to Runbook list");
                                }
                                break;
                            case "Server List":

                                #region Filter By ...

                                switch (cmbFilter.Text)
                                {
                                    case "Stream":
                                        AddServerToList = GetAddToListResult(Server.Stream, Server.Name, _SearchFilter);
                                        break;
                                    case "Tier":
                                        AddServerToList = GetAddToListResult(Server.Tier, Server.Name, _SearchFilter);
                                        break;
                                    case "Service":
                                        foreach (BusinessApplication ProvidedService in Server.ProvidedBusinessApplications)
                                        {
                                            if (ProvidedService.Name.ToLower().Trim().Contains(txtFilter.Text.ToLower().Trim()))
                                            {
                                                AddServerToList = true;
                                            }
                                        }
                                        break;
                                    case "None":
                                        AddServerToList = GetAddToListResult(Server, _SearchFilter);
                                        break;
                                }

                                #endregion

                                break;
                        }

                        if (AddServerToList)
                        {
                            Item.SubItems.Add(Global.GetServerStreamName(Server));
                            lvwRunbook.Items.Add(Item);
                        }
                    }
                }

                #endregion

                #region Group By [Business Application]

                else if (rbGroupByBusinessApplication.Checked)
                {
                    lvwRunbook.ShowGroups = true;

                    lvwRunbook.Columns.Clear();
                    lvwRunbook.Columns.Add("Name");

                    ResizeColumns();

                    foreach (BusinessApplication Service in _BusinessApplicationRunbook)
                    {
                        Group = null;

                        foreach (ListViewGroup ExistingGroup in lvwRunbook.Groups)
                        {
                            if (ExistingGroup.Header == Service.Name)
                            {
                                Group = ExistingGroup;
                                break;
                            }
                        }

                        if (Group == null)
                        {
                            Group = new ListViewGroup(Service.Name);
                            lvwRunbook.Groups.Add(Group);
                        }

                        foreach (Server Server in Service.ComponentServers)
                        {
                            bool AddServerToList = false;

                            Item = new ListViewItem { Text = Server.Name, Tag = Server };
                            Item.ImageIndex = Global.GetServerStateImageIndex(Server);

                            #region Filter By ...

                            switch (cmbFilter.Text)
                            {
                                case "Stream":
                                    AddServerToList = GetAddToListResult(Server.Stream, Server.Name, _SearchFilter);
                                    break;
                                case "Tier":
                                    AddServerToList = GetAddToListResult(Server.Tier, Server.Name, _SearchFilter);
                                    break;
                                case "Service":
                                    foreach (BusinessApplication ProvidedService in Server.ProvidedBusinessApplications)
                                    {
                                        if (ProvidedService.Name.ToLower().Trim().Contains(txtFilter.Text.ToLower().Trim()))
                                        {
                                            AddServerToList = true;
                                        }
                                    }
                                    break;
                                case "None":
                                    AddServerToList = true;
                                    break;
                            }

                            #endregion

                            if (AddServerToList)
                            {
                                Item.Group = Group; // lvwRunbook.Groups[GroupCounter];
                                lvwRunbook.Items.Add(Item);
                            }
                        }

                        //GroupCounter++;

                        lvwRunbook.Groups.Add(Group);
                        //}
                    }
                }
                #endregion

                #region Group By [Stream]
                else if (rbGroupByStream.Checked)
                {
                    lvwRunbook.ShowGroups = true;

                    // Prepopulate Groups
                    lvwRunbook.Groups.Add(new ListViewGroup("?"));
                    lvwRunbook.Groups.Add(new ListViewGroup(" "));
                    lvwRunbook.Groups.Add(new ListViewGroup("A"));
                    lvwRunbook.Groups.Add(new ListViewGroup("B"));
                    lvwRunbook.Groups.Add(new ListViewGroup("C"));
                    lvwRunbook.Groups.Add(new ListViewGroup("D"));
                    lvwRunbook.Groups.Add(new ListViewGroup("E"));
                    lvwRunbook.Groups.Add(new ListViewGroup("O"));
                    lvwRunbook.Groups.Add(new ListViewGroup("None"));

                    string GroupName = "";

                    foreach (BusinessApplication Service in _BusinessApplicationRunbook)
                    {
                        Group = null;

                        //if (Service.ShowInRunbook)
                        //{
                        GroupName = Global.GetBusinessApplicationStreamName(Service.CalculatedStream);
                        //switch (Service.CalculatedStream)
                        //{
                        //    case Global.RecoveryStream.A:
                        //        GroupName = " ";
                        //        break;
                        //    case Global.RecoveryStream.B:
                        //        GroupName = "B";
                        //        break;
                        //    case Global.RecoveryStream.C:
                        //        GroupName = "C";
                        //        break;
                        //    case Global.RecoveryStream.D:
                        //        GroupName = "D";
                        //        break;
                        //    case Global.RecoveryStream.E:
                        //        GroupName = "E";
                        //        break;
                        //    case Global.RecoveryStream.None3:
                        //        GroupName = "A";
                        //        break;
                        //}

                        foreach (ListViewGroup ExistingGroup in lvwRunbook.Groups)
                        {
                            if (ExistingGroup.Header == GroupName)
                            {
                                Group = ExistingGroup;
                                break;
                            }
                        }

                        if (Group == null)
                        {
                            Group = new ListViewGroup(GroupName);
                            lvwRunbook.Groups.Add(Group);
                            //GroupCounter++;
                        }

                        foreach (Server Server in Service.ComponentServers)
                        {
                            bool AddServerToList = false;

                            //if (Server.ShowInRunbook)
                            //{
                            Item = new ListViewItem { Text = Server.Name, Tag = Server };
                            Item.ImageIndex = Global.GetServerStateImageIndex(Server);

                            #region Filter By ...

                            switch (cmbFilter.Text)
                            {
                                case "Stream":
                                    AddServerToList = GetAddToListResult(Server.Stream, Server.Name, _SearchFilter);
                                    break;
                                case "Tier":
                                    AddServerToList = GetAddToListResult(Server.Tier, Server.Name, _SearchFilter);
                                    break;
                                case "Service":
                                    foreach (BusinessApplication ProvidedService in Server.ProvidedBusinessApplications)
                                    {
                                        if (ProvidedService.Name.ToLower().Trim().Contains(txtFilter.Text.ToLower().Trim()))
                                        {
                                            AddServerToList = true;
                                        }
                                    }
                                    break;
                                case "None":
                                    AddServerToList = true;
                                    break;
                            }

                            #endregion

                            if (AddServerToList) // && Server.ShowInRunbook)
                            {
                                Item.Group = Group; // lvwRunbook.Groups[GroupCounter];
                                lvwRunbook.Items.Add(Item);
                            }
                            //}
                        }

                        lvwRunbook.Groups.Add(Group);
                        //}
                    }
                }
                #endregion

                #region Group By [Tier]
                else if (rbGroupByTier.Checked)
                {
                    lvwRunbook.ShowGroups = true;

                    // Prepopulate Groups
                    lvwRunbook.Groups.Add(new ListViewGroup("-"));
                    lvwRunbook.Groups.Add(new ListViewGroup("0"));
                    lvwRunbook.Groups.Add(new ListViewGroup("1"));
                    lvwRunbook.Groups.Add(new ListViewGroup("2"));
                    lvwRunbook.Groups.Add(new ListViewGroup("3"));
                    lvwRunbook.Groups.Add(new ListViewGroup("4"));

                    string GroupName = "";

                    foreach (BusinessApplication Service in _BusinessApplicationRunbook)
                    {
                        Group = null;

                        //if (Service.ShowInRunbook)
                        //{

                        switch (Service.DesiredTier)
                        {
                            case Global.RecoveryTier.Zero:
                                GroupName = "0";
                                break;
                            case Global.RecoveryTier.One:
                                GroupName = "1";
                                break;
                            case Global.RecoveryTier.Two:
                                GroupName = "2";
                                break;
                            case Global.RecoveryTier.Three:
                                GroupName = "3";
                                break;
                            case Global.RecoveryTier.Four:
                                GroupName = "4";
                                break;
                            case Global.RecoveryTier.Undefined:
                                GroupName = "X";
                                break;
                        }

                        foreach (ListViewGroup ExistingGroup in lvwRunbook.Groups)
                        {
                            if (ExistingGroup.Header == GroupName)
                            {
                                Group = ExistingGroup;
                                break;
                            }
                        }

                        if (Group == null)
                        {
                            Group = new ListViewGroup(GroupName);
                            lvwRunbook.Groups.Add(Group);
                            //GroupCounter++;
                        }

                        foreach (Server Server in Service.ComponentServers)
                        {
                            bool AddServerToList = false;

                            //if (Server.ShowInRunbook)
                            //{
                            Item = new ListViewItem { Text = Server.Name, Tag = Server };
                            Item.ImageIndex = Global.GetServerStateImageIndex(Server);

                            #region Filter By ...

                            switch (cmbFilter.Text)
                            {
                                case "Stream":
                                    AddServerToList = GetAddToListResult(Server.Stream, Server.Name, _SearchFilter);
                                    break;
                                case "Tier":
                                    AddServerToList = GetAddToListResult(Server.Tier, Server.Name, _SearchFilter);
                                    break;
                                case "Service":
                                    foreach (BusinessApplication ProvidedService in Server.ProvidedBusinessApplications)
                                    {
                                        //if (ProvidedService.ShowInRunbook)
                                        //{
                                        if (ProvidedService.Name.ToLower().Trim().Contains(txtFilter.Text.ToLower().Trim()))
                                        {
                                            AddServerToList = true;
                                        }
                                        //}
                                    }
                                    break;
                                case "None":
                                    AddServerToList = true;
                                    break;
                            }

                            #endregion

                            if (AddServerToList)
                            {
                                Item.Group = Group; // lvwRunbook.Groups[GroupCounter];
                                lvwRunbook.Items.Add(Item);
                            }
                            //}
                        }

                        //GroupCounter++;

                        lvwRunbook.Groups.Add(Group);

                        //}
                    }
                }
                #endregion

            }
            #endregion

            #region Display By Business Application

            else if (rbDisplayByBusinessApplication.Checked) // Business Application Runbook
            {
                #region Group By [None] (Business Application Runbook)

                if (rbGroupByNone.Checked)
                {
                    int Counter = 0;

                    lvwRunbook.Columns.Clear();

                    lvwRunbook.Columns.Add("Name");
                    lvwRunbook.Columns.Add("Tier");
                    lvwRunbook.Columns.Add("Stream");
                    lvwRunbook.Columns[1].Width = 30;
                    lvwRunbook.Columns[2].Width = 60;

                    ResizeColumns();

                    foreach (BusinessApplication App in _BusinessApplicationRunbook)
                    {
                        Counter++;

                        Item = new ListViewItem(App.Name);
                        Item.SubItems.Add(Global.GetTierName(App.DesiredTier));
                        Item.SubItems.Add(Global.GetBusinessApplicationStreamName(App.CalculatedStream));
                        Item.Tag = App;
                        Item.ImageIndex = Global.GetBusinessApplicationStateImageIndex(App, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication);

                        if (App.CalculatedStream != Global.RecoveryStream.E)
                        {
                            // Filtering
                            if (GetAddToListResult(App, _SearchFilter))
                            {
                                lvwRunbook.Items.Add(Item);
                            }
                        }
                    }
                }
                #endregion

                #region Group By [Stream]

                if (rbGroupByStream.Checked)
                {
                    lvwRunbook.ShowGroups = true;

                    // Prepopulate Groups
                    lvwRunbook.Groups.Add(new ListViewGroup("?"));
                    lvwRunbook.Groups.Add(new ListViewGroup(" "));
                    lvwRunbook.Groups.Add(new ListViewGroup("A"));
                    lvwRunbook.Groups.Add(new ListViewGroup("B"));
                    lvwRunbook.Groups.Add(new ListViewGroup("C"));
                    lvwRunbook.Groups.Add(new ListViewGroup("D"));
                    lvwRunbook.Groups.Add(new ListViewGroup("E"));
                    lvwRunbook.Groups.Add(new ListViewGroup("O"));
                    lvwRunbook.Groups.Add(new ListViewGroup("None"));

                    string GroupName = "";

                    foreach (BusinessApplication Application in _BusinessApplicationRunbook)
                    {
                        Group = null;
                        bool AddServiceToList = false;

                        GroupName = Global.GetBusinessApplicationStreamName(Application.CalculatedStream);

                        foreach (ListViewGroup ExistingGroup in lvwRunbook.Groups)
                        {
                            if (ExistingGroup.Header == GroupName)
                            {
                                Group = ExistingGroup;
                                break;
                            }
                        }

                        if (Group == null)
                        {
                            Group = new ListViewGroup(GroupName);
                            lvwRunbook.Groups.Add(Group);
                        }

                        Item = new ListViewItem { Text = Application.Name, Tag = Application };
                        Item.ImageIndex = Global.GetBusinessApplicationStateImageIndex(Application, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication);

                        #region Filter By ...

                        switch (cmbFilter.Text)
                        {
                            case "Stream":
                                AddServiceToList = GetAddToListResult(Application.CalculatedStream, Application.Name, _SearchFilter);
                                break;
                            case "Tier":
                                AddServiceToList = GetAddToListResult(Application.DesiredTier, Application.Name, _SearchFilter);
                                break;
                            case "Service":
                                if (Application.Name.ToLower().Trim().Contains(txtFilter.Text.ToLower().Trim()))
                                {
                                    AddServiceToList = true;
                                }

                                break;
                            case "None":
                                AddServiceToList = true;
                                break;
                        }

                        #endregion

                        if (AddServiceToList) // && Service.ShowInRunbook)
                        {
                            Item.Group = Group; // lvwRunbook.Groups[GroupCounter];
                            lvwRunbook.Items.Add(Item);
                        }

                        lvwRunbook.Groups.Add(Group);
                        //}
                    }

                }

                #endregion

                #region Group By [Business Application]

                if (rbGroupByBusinessApplication.Checked)
                {
                    foreach (BusinessApplication Service in _BusinessApplicationRunbook)
                    {
                        //if (Service.ShowInRunbook)
                        //{
                        //}
                    }
                }

                #endregion

                #region Group By [Tier]

                if (rbGroupByTier.Checked)
                {
                    lvwRunbook.ShowGroups = true;

                    // Prepopulate Groups
                    lvwRunbook.Groups.Add(new ListViewGroup("-"));
                    lvwRunbook.Groups.Add(new ListViewGroup("0"));
                    lvwRunbook.Groups.Add(new ListViewGroup("1"));
                    lvwRunbook.Groups.Add(new ListViewGroup("2"));
                    lvwRunbook.Groups.Add(new ListViewGroup("3"));
                    lvwRunbook.Groups.Add(new ListViewGroup("4"));

                    //string GroupName = "";
                    foreach (BusinessApplication Service in _BusinessApplicationRunbook)
                    {
                        //if (Service.ShowInRunbook)
                        //{
                        //}
                    }
                }

                #endregion
            }

            #endregion

            #region Sort order

            if (rbSortByNone.Checked)
            {
                lvwRunbook.Sorting = SortOrder.None;
            }
            else if (rbAscending.Checked)
            {
                lvwRunbook.Sorting = SortOrder.Ascending;
            }
            else if (rbDescending.Checked)
            {
                lvwRunbook.Sorting = SortOrder.Descending;
            }

            lvwRunbook.Sort();

            #endregion

            switch (_ListType)
            {
                case "Server List":
                    lblInfo.Text = lvwRunbook.Items.Count + " Servers listed";
                    break;
                case "Business Application Runbook":
                    lblInfo.Text = lvwRunbook.Items.Count + " Applications listed";
                    break;
                case "Server Runbook":
                    lblInfo.Text = lvwRunbook.Items.Count + " Servers listed";
                    break;
                case "SRM Runbook":
                    //lblInfo.Text = lvwRunbook.Groups.Count + " Groups and " + lvwRunbook.Items.Count + " Servers listed";
                    lblInfo.Text = (from ListViewGroup x in lvwRunbook.Groups
                                   where x.Items.Count > 0
                                   select x).Count().ToString() + " Applications and " + lvwRunbook.Items.Count + " Servers listed";
                    break;
            }

            lblInfo.Refresh();

            if (ISA.Helper.Properties.ReloadNecessary)
            {
                //btnExport.Enabled = false;
            }
        }

        private bool GetAddToListResult(Global.RecoveryStream Stream, string Name, string SearchFilter)
        {
            if (
                (txtFilter.Text.Trim() == "X" && Stream == Global.RecoveryStream.None3) ||
                (txtFilter.Text.Trim() == "A" && Stream == Global.RecoveryStream.A) ||
                (txtFilter.Text.Trim() == "B" && Stream == Global.RecoveryStream.B) ||
                (txtFilter.Text.Trim() == "C" && Stream == Global.RecoveryStream.C) ||
                (txtFilter.Text.Trim() == "D" && Stream == Global.RecoveryStream.D) ||
                (txtFilter.Text.Trim() == "E" && Stream == Global.RecoveryStream.E) ||
                (txtFilter.Text.Trim() == "")
                )
            {
                if (SearchFilter == "")
                {
                    return true;
                }
                else
                {
                    if (Name.ToUpper().Contains(SearchFilter.ToUpper()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool GetAddToListResult(Global.RecoveryTier Tier, string Name, string SearchFilter)
        {
            if (
                (txtFilter.Text.Trim() == "X" && Tier == Global.RecoveryTier.Undefined) ||
                (txtFilter.Text.Trim() == "0" && Tier == Global.RecoveryTier.Zero) ||
                (txtFilter.Text.Trim() == "1" && Tier == Global.RecoveryTier.One) ||
                (txtFilter.Text.Trim() == "2" && Tier == Global.RecoveryTier.Two) ||
                (txtFilter.Text.Trim() == "3" && Tier == Global.RecoveryTier.Three) ||
                (txtFilter.Text.Trim() == "4" && Tier == Global.RecoveryTier.Four) ||
                (txtFilter.Text.Trim() == "")
                )
            {
                if (SearchFilter == "")
                {
                    return true;
                }
                else
                {
                    if (Name.ToUpper().Contains(SearchFilter.ToUpper()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool GetAddToListResult(Server Server, string SearchFilter)
        {

            if (SearchFilter == "")
            {
                if (_ListType == "Server Runbook")
                {
                    if (Server.PhysicalSite.SiteType == PhysicalSite.PhysicalSiteType.Protected && (Server.Stream == Global.RecoveryStream.B || Server.Stream == Global.RecoveryStream.C) && Server.Virtual == true)
                    {
                        return true; // Stream A Servers at the PDC do not get recovered
                    }
                    else
                    {
                        Debug.Print("Not adding " + Server.Name + " to Runbook list");
                    }
                }

                return true;
            }
            else
            {
                if (Server.Name.ToUpper().Contains(SearchFilter.ToUpper()))
                {
                    return true;
                }
            }

            return false;
        }

        private bool GetAddToListResult(BusinessApplication Application, string SearchFilter)
        {

            if (SearchFilter == "")
            {
                if (_ListType == "Server Runbook")
                {
                    return true;
                }

                return true;
            }

            return false;
        }

        private void ListRunbook(string Runbook)
        {
            _OKToBuildList = false;

            switch (Runbook)
            {
                case "Server Runbook":
                    grpOptions.Enabled = false;
                    rbDisplayByServer.Checked = true;
                    rbGroupByNone.Checked = true;
                    lvwRunbook.ShowGroups = !rbGroupByNone.Checked;
                    cmbFilter.Text = "None";
                    txtFilter.Text = "";
                    rbSortByNone.Checked = true;

                    lblSelectedText.Visible = true;
                    pnlSelected.Visible = true;
                    lblMovementText.Visible = true;
                    pnlMovement.Visible = true;
                    pnlMovement.BackColor = Color.FromName(Properties.Settings.Default.ServerBackgroundColour);
                    lblComponentText.Visible = false;
                    lblDependentText.Visible = false;

                    lblMandatoryComponent.Visible = false;
                    pnlMandatoryComponent.Visible = false;

                    lblOptionalComponent.Visible = false;
                    pnlOptionalComponent.Visible = false;

                    lblMandatoryDependent.Visible = false;
                    pnlMandatoryDependent.Visible = false;

                    lblOptionalDependent.Visible = false;
                    pnlOptionalDependent.Visible = false;

                    break;
                case "Business Application Runbook":
                    grpOptions.Enabled = false;
                    rbDisplayByBusinessApplication.Checked = true;
                    rbGroupByNone.Checked = true;
                    lvwRunbook.ShowGroups = !rbGroupByNone.Checked;
                    cmbFilter.Text = "None";
                    txtFilter.Text = "";
                    rbSortByNone.Checked = true;

                    lblSelectedText.Visible = true;
                    pnlSelected.Visible = true;
                    lblMovementText.Visible = true;
                    pnlMovement.Visible = true;
                    pnlMovement.BackColor = Color.FromName(Properties.Settings.Default.BusinessApplicationBackgroundColour);
                    lblComponentText.Visible = true;
                    lblDependentText.Visible = true;

                    lblMandatoryComponent.Visible = true;
                    pnlMandatoryComponent.Visible = true;

                    lblOptionalComponent.Visible = true;
                    pnlOptionalComponent.Visible = true;

                    lblMandatoryDependent.Visible = true;
                    pnlMandatoryDependent.Visible = true;

                    lblOptionalDependent.Visible = true;
                    pnlOptionalDependent.Visible = true;

                    break;
                case "SRM Runbook":
                    grpOptions.Enabled = false;
                    rbDisplayByServer.Checked = true;
                    rbGroupByBusinessApplication.Checked = true;
                    lvwRunbook.ShowGroups = !rbGroupByNone.Checked;
                    cmbFilter.Text = "Stream";
                    txtFilter.Text = "B";
                    rbSortByNone.Checked = true;

                    lblSelectedText.Visible = false;
                    pnlSelected.Visible = false;
                    lblMovementText.Visible = false;
                    pnlMovement.Visible = false;
                    lblComponentText.Visible = false;
                    lblDependentText.Visible = false;

                    lblMandatoryComponent.Visible = false;
                    pnlMandatoryComponent.Visible = false;

                    lblOptionalComponent.Visible = false;
                    pnlOptionalComponent.Visible = false;

                    lblMandatoryDependent.Visible = false;
                    pnlMandatoryDependent.Visible = false;

                    lblOptionalDependent.Visible = false;
                    pnlOptionalDependent.Visible = false;

                    break;
                case "Server List":
                    grpOptions.Enabled = false;
                    rbDisplayByServer.Checked = true;
                    rbGroupByNone.Checked = true;
                    lvwRunbook.ShowGroups = !rbGroupByNone.Checked;
                    cmbFilter.Text = "None";
                    txtFilter.Text = "";
                    rbAscending.Checked = true;

                    lblSelectedText.Visible = false;
                    pnlSelected.Visible = false;
                    lblMovementText.Visible = false;
                    pnlMovement.Visible = false;
                    lblComponentText.Visible = false;
                    lblDependentText.Visible = false;

                    lblMandatoryComponent.Visible = false;
                    pnlMandatoryComponent.Visible = false;

                    lblOptionalComponent.Visible = false;
                    pnlOptionalComponent.Visible = false;

                    lblMandatoryDependent.Visible = false;
                    pnlMandatoryDependent.Visible = false;

                    lblOptionalDependent.Visible = false;
                    pnlOptionalDependent.Visible = false;

                    break;
                case "Custom":
                    grpOptions.Enabled = true;
                    rbDisplayByServer.Checked = true;
                    rbGroupByNone.Checked = true;
                    lvwRunbook.ShowGroups = !rbGroupByNone.Checked;
                    cmbFilter.Text = "None";
                    txtFilter.Text = "";
                    rbSortByNone.Checked = true;

                    // TODO: The following have to be set appropriately
                    lblSelectedText.Visible = false;
                    pnlSelected.Visible = false;
                    lblMovementText.Visible = false;
                    pnlMovement.Visible = false;
                    lblComponentText.Visible = false;
                    lblDependentText.Visible = false;

                    lblMandatoryComponent.Visible = false;
                    pnlMandatoryComponent.Visible = false;

                    lblOptionalComponent.Visible = false;
                    pnlOptionalComponent.Visible = false;

                    lblMandatoryDependent.Visible = false;
                    pnlMandatoryDependent.Visible = false;

                    lblOptionalDependent.Visible = false;
                    pnlOptionalDependent.Visible = false;
                    break;
            }

            _OKToBuildList = true;
            BuildList();
        }

        private void ResizeColumns()
        {
            switch (lvwRunbook.Columns.Count)
            {
                case 1:
                    lvwRunbook.Columns[0].Width = lvwRunbook.Size.Width - 28;
                    break;
                case 2:
                    lvwRunbook.Columns[0].Width = lvwRunbook.Size.Width - 28 - lvwRunbook.Columns[1].Width;
                    break;
                case 3:
                    lvwRunbook.Columns[0].Width = lvwRunbook.Size.Width - 28 - (lvwRunbook.Columns[1].Width + lvwRunbook.Columns[2].Width);
                    break;
            }

            Properties.Settings.Default.frmListSize = this.Size;
        }

        private string ServerAdminText(Server Server)
        {
            string Result = "";

            if (!Server.PhysicalSite.Internal)
            {
                Result = "Recovery not performed (External Site)";
            }
            else
            {
                if (Server.Stream == Global.RecoveryStream.None1)
                {
                    Result = "Recovery not controlled";
                }

                if (Server.Stream == Global.RecoveryStream.None2)
                {
                    if (Server.Virtual)
                    {
                        switch (Server.PhysicalSite.SiteType)
                        {
                            case PhysicalSite.PhysicalSiteType.Recovery:
                                Result = "Recovery not necessary (Server is at the Recovery Site)";
                                break;
                            case PhysicalSite.PhysicalSiteType.NotProtected:
                                Result = "Recovery not possible (Site not protected)";
                                break;
                        }
                    }
                    else
                    {
                        switch (Server.PhysicalSite.SiteType)
                        {
                            case PhysicalSite.PhysicalSiteType.Recovery:
                                Result = "Recovery not necessary (Server is at the Recovery Site)";
                                break;
                            case PhysicalSite.PhysicalSiteType.NotProtected:
                                Result = "Recovery not possible (Site not protected)";
                                break;
                            case PhysicalSite.PhysicalSiteType.Protected:
                                Result = "Recovery not possible (Server not virtual)";
                                break;
                        }
                    }
                }

                if (Server.Stream == Global.RecoveryStream.None3)
                {
                    Result = "Recovery not desired";
                }

                if (Server.Stream == Global.RecoveryStream.None4)
                {
                    Result = "Recovery not necessary";
                }
            }

            return Result;
        }

        #endregion

        #region Public Methods

        public void PerformUpdate()
        {
            lvwRunbook.BeginUpdate();
            lvwRunbook.Items.Clear();

            for (int i = 0; i < lvwRunbook.Items.Count; i++)
            {
                lvwRunbook.Items[i].BackColor = SystemColors.Window;
            }

            btnUp.Enabled = false;
            btnDown.Enabled = false;

            lvwRunbook.Sorting = SortOrder.None;

            ListRunbook(cmbRunbook.SelectedItem.ToString());
            lvwRunbook.EndUpdate();
        }

        #endregion

    }
}
