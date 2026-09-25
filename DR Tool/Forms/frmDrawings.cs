using ISA.Dependency;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DRPlanningTool
{
    public partial class frmDrawings : Form, IDrawingForm
    {

        #region Fields

        private static string _SearchFilter = "";

        #endregion

        public frmDrawings()
        {
            InitializeComponent();
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Clear();
            _SearchFilter = "";

            FilterList();

            txtFilter.Focus();
            txtFilter.SelectionStart = 0;
            txtFilter.SelectAll();
        }

        public void FilterList()
        {
            lvwDrawings.Items.Clear();
            lvwDrawings.BeginUpdate();

            foreach (BusinessApplication Service in Main.DependencyCollection.Applications)
            {
                if (Service.Name.ToUpper().Contains(_SearchFilter.ToUpper()) || _SearchFilter.Trim() == "")
                {
                    Main.AddBusinessApplicationToListView(Service, Service.Drawing.Status.ToString(), lvwDrawings);
                }
            }

            lvwDrawings.EndUpdate();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            _SearchFilter = txtFilter.Text.Trim();

            FilterList();

            txtFilter.Focus();
            txtFilter.SelectionStart = 0;
            txtFilter.SelectAll();
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                _SearchFilter = txtFilter.Text;

                FilterList();

                txtFilter.SelectAll();
            }
        }

        private void lvwDrawings_ItemActivate(object sender, EventArgs e)
        {
            DoSelectionChange();
        }

        private void DoSelectionChange()
        {
            // Get the first Service form in our list.
            if (Main.BusinessApplicationForms.Count > 0)
            {
                if (lvwDrawings.SelectedItems.Count > 0)
                {
                    BusinessApplication Service = (BusinessApplication)lvwDrawings.SelectedItems[0].Tag;

                    Main.BusinessApplicationForms[0].BusinessApplication = Service;

                    Main.BusinessApplicationForms[0].DrawingName = lvwDrawings.SelectedItems[0].Text;

                    Main.BusinessApplicationForms[0].PerformUpdate();

                    //_SelectionHandled = true;
                }

            }
            else
            {
                // No service forms are open, so ignore the selection change
            }
        }

        private void lvwDrawings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwDrawings.SelectedItems.Count > 0)
            {
                //_SelectedItems.Add(lvwDrawings.Items[lvwDrawings.SelectedIndexChanged]

                BusinessApplication Service = (BusinessApplication)lvwDrawings.SelectedItems[0].Tag;

                if (Service == null)
                {
                    pictureBox.BackgroundImage = null;
                    pictureBox.Cursor = Cursors.Default;
                    toolTips.SetToolTip(pictureBox, "No Image");
                    pictureBox.Image = imlDrawingStatus.Images[0];
                    pictureBox.SizeMode = PictureBoxSizeMode.Normal;

                    return;
                }

                System.Drawing.Image Image = Service.Drawing.Image; // Service.Image

                if (Image != null)
                {
                    pictureBox.BackgroundImage = Image;
                    pictureBox.BackgroundImageLayout = ImageLayout.Zoom;
                    pictureBox.Cursor = Cursors.Hand;
                    toolTips.SetToolTip(pictureBox, "Double click for larger image");

                    if (Service.Drawing != null)
                    {
                        switch (Service.Drawing.Status)
                        {
                            case Global.DrawingStatus.Not_Applicable:
                                pictureBox.Image = imlDrawingStatus.Images[0];
                                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                                break;
                            case Global.DrawingStatus.Draft:
                                pictureBox.Image = imlDrawingStatus.Images[1];
                                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                                break;
                            case Global.DrawingStatus.Pending_Review:
                                pictureBox.Image = imlDrawingStatus.Images[2];
                                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                                break;
                            case Global.DrawingStatus.Approved:
                                pictureBox.Image = imlDrawingStatus.Images[3];
                                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                                break;
                        }

                        // Check if the Service is In Scope
                        if (!Service.InScope && !Service.OverrideInScope)
                        {
                            pictureBox.Image = imlDrawingStatus.Images[4];
                            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                        }
                    }
                    else
                    {
                        toolTips.SetToolTip(pictureBox, "No Image");
                        pictureBox.Image = imlDrawingStatus.Images[0];
                        pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    }
                }
                else
                {
                    pictureBox.BackgroundImage = null;
                    pictureBox.Cursor = Cursors.Default;
                    toolTips.SetToolTip(pictureBox, "No Image");
                    pictureBox.Image = imlDrawingStatus.Images[0];
                    pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                }
            }
        }

        private void lvwDrawings_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            DoSelectionChange();
        }

        private void lvwDrawings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            {
                BusinessApplication Service = (BusinessApplication)lvwDrawings.SelectedItems[0].Tag;

                Main.CreateOrSelectBusinessApplicationForm(lvwDrawings.SelectedItems[0].Text, Service);
            }

            if (e.KeyCode == Keys.A && e.Control)
            {
                foreach (ListViewItem Item in lvwDrawings.Items)
                {
                    Item.Selected = true;
                }

            }
        }

        private void lvwDrawings_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lvwDrawings.SelectedItems.Count > 0)
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
                    copyNamesToolStripMenuItem.Enabled = true;
                    ViewDependencyMapToolStripMenuItem.Enabled = true;
                    StatusToolStripMenuItem.Enabled = true;
                    DrawingContextMenu.Show(Cursor.Position);
                }
                else
                {
                    openToolStripMenuItem.Enabled = false;
                    copyNamesToolStripMenuItem.Enabled = false;
                    ViewDependencyMapToolStripMenuItem.Enabled = false;
                    StatusToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void copyNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder Builder = new StringBuilder();

            if (lvwDrawings.SelectedItems.Count > 0)
            {
                // Copy the selected names

                foreach (ListViewItem Item in lvwDrawings.SelectedItems)
                {
                    Builder.AppendLine(Item.Text);
                }


            }
            else
            {
                // Copy all names

                foreach (ListViewItem Item in lvwDrawings.Items)
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

        private void productionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(false, Global.Environment.Production, lvwDrawings);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(false, Global.Environment.Test, lvwDrawings);
        }

        private void developmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(false, Global.Environment.Development, lvwDrawings);
        }

        private void productionStubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(true, Global.Environment.Production, lvwDrawings);
        }

        private void testStubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(true, Global.Environment.Test, lvwDrawings);
        }

        private void developmentStubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(true, Global.Environment.Development, lvwDrawings);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusinessApplication Service = (BusinessApplication)lvwDrawings.SelectedItems[0].Tag;

            Main.CreateOrSelectBusinessApplicationForm(lvwDrawings.SelectedItems[0].Text, Service);
        }

        private void ApprovedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string CurrentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            foreach (ListViewItem Item in lvwDrawings.SelectedItems)
            {
                BusinessApplication Service = (BusinessApplication)Item.Tag;
                string Now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("en-AU"));
                string LastUpdateText = "[INF1211] Status set to Approved by " + CurrentUserName + " at " + Now;
                Service.Drawing.LastUpdate = LastUpdateText;

                Service.Drawing.Status = Global.DrawingStatus.Approved;
                Service.Save(Main.DependencyDatabase);

                Debug.WriteLine("[INF1212] " + Service.Name + " Drawing saved", "information");
            }

            FilterList();

            Cursor.Current = Cursors.Default;
        }

        private void PendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string CurrentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            foreach (ListViewItem Item in lvwDrawings.SelectedItems)
            {
                BusinessApplication Service = (BusinessApplication)Item.Tag;
                string Now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("en-AU"));
                string LastUpdateText = "[INF1215] Status set to Pending by " + CurrentUserName + " at " + Now;
                Service.Drawing.LastUpdate = LastUpdateText;

                Service.Drawing.Status = Global.DrawingStatus.Pending_Review;
                Service.Save(Main.DependencyDatabase);

                Debug.WriteLine("[INF1216] " + Service.Name + " Drawing saved", "information");
            }

            FilterList();

            Cursor.Current = Cursors.Default;
        }

        private void DraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string CurrentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            foreach (ListViewItem Item in lvwDrawings.SelectedItems)
            {
                BusinessApplication Service = (BusinessApplication)Item.Tag;
                string Now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("en-AU"));
                string LastUpdateText = "[INF1213] Status set to Draft by " + CurrentUserName + " at " + Now;
                Service.Drawing.LastUpdate = LastUpdateText;

                Service.Drawing.Status = Global.DrawingStatus.Draft;
                Service.Save(Main.DependencyDatabase);

                Debug.WriteLine("[INF1214] " + Service.Name + " Drawing saved", "information");
            }

            FilterList();

            Cursor.Current = Cursors.Default;
        }

        private void ViewDependencyMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<BusinessApplication> Services = new List<BusinessApplication>();

            foreach (ListViewItem Item in lvwDrawings.SelectedItems)
            {
                Services.Add((BusinessApplication)Item.Tag);
            }

            Main.CreateOrSelectDependencyMapForm(Services, true, Services, LayoutMethod.SugiyamaScheme, false);
        }

        #region Interface Methods

        public void SetupContextMenuStrip()
        {
            // Normal MP's
            productionToolStripMenuItem.Enabled = Properties.Settings.Default.SystemCenter_SCOMServer != "" || Properties.Settings.Default.SystemCenter_SCSMServer != "";
            testToolStripMenuItem.Enabled = Properties.Settings.Default.SystemCenter_TestSCOMServer != "" || Properties.Settings.Default.SystemCenter_TestSCSMServer != "";
            developmentToolStripMenuItem.Enabled = Properties.Settings.Default.SystemCenter_DevSCOMServer != "" || Properties.Settings.Default.SystemCenter_DevSCSMServer != "";

            // Stub MP's
            productionStubToolStripMenuItem.Enabled = Properties.Settings.Default.SystemCenter_SCOMServer != "" || Properties.Settings.Default.SystemCenter_SCSMServer != "";
            testStubToolStripMenuItem.Enabled = Properties.Settings.Default.SystemCenter_TestSCOMServer != "" || Properties.Settings.Default.SystemCenter_TestSCSMServer != "";
            developmentStubToolStripMenuItem.Enabled = Properties.Settings.Default.SystemCenter_DevSCOMServer != "" || Properties.Settings.Default.SystemCenter_DevSCSMServer != "";
        }

        public void SetupListView()
        {
            if (Properties.Settings.Default.ShowDrawingStatusColumn)
            {
                lvwDrawings.Columns.Add("Status");
                lvwDrawings.Columns[0].Width = 200;
                lvwDrawings.Columns[1].Width = 80;
            }
            else
            {
                lvwDrawings.Columns[0].Width = 280;
            }
        }

        public void SetupCheckboxes()
        {
            lvwDrawings.CheckBoxes = Properties.Settings.Default.ShowCheckboxesOnServiceList;
        }

        public void DisableGUI()
        {
            lvwDrawings.Items.Clear();
            lvwDrawings.BeginUpdate();

            btnClearFilter.Enabled = false;
            btnFilter.Enabled = false;
            txtFilter.Enabled = false;
        }

        public void AddBusinessApplicationsToListView()
        {
            lvwDrawings.Items.Clear();

            lvwDrawings.BeginUpdate();
            foreach (BusinessApplication Application in Main.DataLayer.Collection.Applications)
            {
                if (Application.InScope || Application.OverrideInScope)
                {
                    if (Application.Drawing != null)
                    {
                        Main.AddBusinessApplicationToListView(Application, Application.Drawing.StatusText, lvwDrawings);
                    }
                    else
                    {
                        Main.AddBusinessApplicationToListView(Application, "N/A", lvwDrawings);
                    }
                }
            }
            lvwDrawings.EndUpdate();

            this.Refresh();
        }

        public void AddBusinessApplicationToListView(BusinessApplication Application, string Text)
        {
            Main.AddBusinessApplicationToListView(Application, Text, lvwDrawings);
        }

        public void BeginUpdate()
        {
            lvwDrawings.BeginUpdate();
        }

        public void EndUpdate()
        {
            lvwDrawings.EndUpdate();
        }

        public void EnableGUI()
        {
            btnClearFilter.Enabled = true;
            btnFilter.Enabled = true;
            txtFilter.Enabled = true;
        }

        //public void DetermineBusinessApplicationState(BusinessApplication BusinessApplication)
        //{
        //    BusinessApplication.CalculateState("");

        //    if (Properties.Settings.Default.ShowDebugInformation)
        //    {
        //        Debug.WriteLine("..." + BusinessApplication.Name + ": " + BusinessApplication.SystemState, "information");
        //    }

        //    // Find matching node in the ListView...
        //    ListViewItem BusinessApplicationItem = lvwDrawings.FindItemWithText(BusinessApplication.Name);

        //    if (BusinessApplicationItem != null)
        //    {
        //        if (BusinessApplication.Drawing != null)
        //        {
        //            switch (BusinessApplication.Drawing.Status)
        //            {
        //                case Global.DrawingStatus.Not_Applicable:
        //                    BusinessApplicationItem.SubItems[1].Text = "N/A";
        //                    break;
        //                case Global.DrawingStatus.Draft:
        //                    BusinessApplicationItem.SubItems[1].Text = "Draft";
        //                    break;
        //                case Global.DrawingStatus.Pending_Review:
        //                    BusinessApplicationItem.SubItems[1].Text = "Pending";
        //                    break;
        //                case Global.DrawingStatus.Approved:
        //                    BusinessApplicationItem.SubItems[1].Text = "Approved";
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            BusinessApplicationItem.SubItems[1].Text = "N/A";
        //        }
        //    }
        //}

        public void Reset()
        {
            pictureBox.Image = null;
            pictureBox.BackgroundImage = null;
            lvwDrawings.Items.Clear();
            this.Refresh();
        }

        #endregion

        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwDrawings.SelectedItems.Count > 0)
            {
                BusinessApplication Service = (BusinessApplication)lvwDrawings.SelectedItems[0].Tag;

                Main.CreateOrSelectBusinessApplicationImageForm(Service);

                //LoadBusinessApplicationImage(Service);
            }
        }

        private void GetServerHealth()
        {
            // This uses the TPL to perform a parallel task, that then is able to get the correct context that allows the GUI to be updated

            Debug.WriteLine("[INF1165] Determining SCOM Server Health", "information");

            Cursor.Current = Cursors.AppStarting;

            var context = TaskScheduler.FromCurrentSynchronizationContext();

            Task task = Task.Factory.StartNew(() =>
            {

                ServerUtilities SCOMUtilities = new ServerUtilities(Main.SCOMServerName, Main.SCSMServerName);

                foreach (Server Server in Main.DependencyCollection.Servers)
                {
                    string ServerHealth = SCOMUtilities.GetServerHealth(Server.Name);

                    switch (ServerHealth)
                    {
                        case "Error":
                            Server.SetSystemHealthState(Global.HealthState.Error, "Loaded from SCOM data");
                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("[INF1166] " + Server.Name + " (Server) Health state (Error) read by SCOM and set to Error", "information");
                            }
                            break;
                        case "Success":
                            Server.SetSystemHealthState(Global.HealthState.OK, "Loaded from SCOM data");
                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("[INF1167] " + Server.Name + " (Server) Health state (Success) read by SCOM and set to OK", "information");
                            }
                            break;
                        case "Uninitialized":
                            Server.SetSystemHealthState(Global.HealthState.Degraded, "Loaded from SCOM data");
                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("[INF1168] " + Server.Name + " (Server) Health state (Uninitialized) read by SCOM and set to Degraded", "information");
                            }
                            break;
                        case "Warning":
                            Server.SetSystemHealthState(Global.HealthState.Degraded, "Loaded from SCOM data");
                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("[INF1169] " + Server.Name + " (Server) Health state (Warning) read by SCOM and set to Degraded", "information");
                            }
                            break;
                        case "":
                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("[ERR1107] Could not set Health state for " + Server.Name + " (Server)", "error");
                            }
                            break;
                    }
                }
            })
            //.ContinueWith(_ => DetermineAllBusinessApplicationState(), context)
            .ContinueWith(_ => Debug.WriteLine("[INF1170] SCOM Server health task returned!"), context);

            Cursor.Current = Cursors.Default;
        }

        private void lvwDrawings_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BusinessApplication Service = (BusinessApplication)lvwDrawings.SelectedItems[0].Tag;

            Main.CreateOrSelectBusinessApplicationForm(lvwDrawings.SelectedItems[0].Text, Service);
        }

        private void frmDrawings_Resize(object sender, EventArgs e)
        {
            if (lvwDrawings.Columns.Count > 1)
            {
                lvwDrawings.Columns[0].Width = lvwDrawings.Size.Width - lvwDrawings.Columns[1].Width - 28;
            }
            else
            {
                lvwDrawings.Columns[0].Width = lvwDrawings.Size.Width - 28;
            }
        }

        private void frmDrawings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }

        public void SetLocation(Point Location)
        {
            base.Location = Location;
        }

        public void SetSize(Size Size)
        {
            base.Size = Size;
        }
    }
}
