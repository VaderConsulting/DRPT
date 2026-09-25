using ISA.CommandLine;
using ISA.Database;
using ISA.Dependency;
using ISA.Helper;
using ISA.Orbus;
using ISA.SystemCenter;
using Microsoft.AGL.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 At some point it will be required to define a group of servers that act as a single entity.
 This group will work together to provide a service, but not all servers are required to provide that service.
 An example is Active Directory, where only 1 Domain Controller is required for day-to-day operations.  
 If any of these servers are not available, AD still operates, however in a reduced capacity.
 The string of queries to retieve these are:
 
 -- Get a Drawing
select v.ObjectID, v.ObjectVersionID from vwObject v where v.objectname like 'ad (dtf)' and v.TypeId = 571 AND v.IsDeleted = 0

-- Get all Components on that drawing
SELECT r.FromObjectID, r.ToObjectId, v.ObjectName AS ToObjectName
FROM relation r 
INNEr JOIN vwObject v ON v.ObjectID = r.ToObjectId
WHERE r.FromObjectId = '1BC038F0-8922-403A-B471-C561AA6BEBAA' AND r.DeleteFlag = 0-- AND v.TypeId = 314

-- Get the Focused Business Application for this Drawing
SELECT v.ObjectID AS ServiceID, v.ObjectName AS ServiceName, v.ObjectDescription AS ServiceDescription
FROM vwObject v
INNER JOIN (
SELECT Drawing.ObjectID AS DrawingObjectID, Drawing.ObjectName AS DrawingName, Drawing.ObjectDescription AS DrawingDescription, DrawingFocusedService.AttributeValue AS FocusedServiceName
FROM vwObject Drawing
LEFT JOIN AttributeValueText DrawingFocusedService ON DrawingFocusedService.AttributeId = 'CB48F04F-03AA-47A1-BE2C-43F4907438F5' 
                                                  AND DrawingFocusedService.VersionId = Drawing.ObjectVersionId
												  AND DrawingFocusedService.ObjectId = Drawing.ObjectID
LEFT JOIN vwObject v ON v.ObjectName = DrawingFocusedService.AttributeValue AND v.TypeId = 314
WHERE Drawing.TypeId = 571 AND Drawing.IsDeleted = 0
--ORDER BY Drawing.ObjectName
) d ON d.FocusedServiceName = v.ObjectName AND v.TypeId <> 571



-- Get further detail
SELECT r.FromObjectID, r.ToObjectId, v.ObjectName AS DrawingName, v2.ObjectName AS ComponentName, r.RelationshipId, x.* 
FROM Relation r
INNER JOIN (
SELECT Drawing.ObjectID, Drawing.ObjectName, ObjectDescription, DrawingFocusedService.AttributeValue AS FocusedService
FROM vwObject Drawing
LEFT JOIN AttributeValueText DrawingFocusedService ON DrawingFocusedService.AttributeId = 'CB48F04F-03AA-47A1-BE2C-43F4907438F5' 
                                                  AND DrawingFocusedService.VersionId = Drawing.ObjectVersionId
												  AND DrawingFocusedService.ObjectId = Drawing.ObjectID
WHERE Drawing.TypeId = 571 AND Drawing.IsDeleted = 0) x ON x.ObjectID = r.FromObjectId
INNER JOIN vwObject v ON v.ObjectID = x.ObjectID
LEFT JOIN vwObject v2 ON v2.ObjectID = r.ToObjectId
--WHERE r.FromObjectId = '1BC038F0-8922-403A-B471-C561AA6BEBAA' --AND r.ToObjectId <> '1BC038F0-8922-403A-B471-C561AA6BEBAA'
 
 SELECT vr.RelationshipId, r.FromObjectId, r.ToObjectId, vo.ObjectName AS FromName, vo2.ObjectName AS ToName, av.AttributeValue AS Mandatory
  FROM vwRelationshipDocument vr
  inner join relation r on r.RelationshipId = vr.RelationshipId
  inner join vwobject vo on vo.ObjectID = r.FromObjectId
  inner join vwobject vo2 on vo2.ObjectID = r.ToObjectId
  inner join AttributeValueBigInt av ON av.ObjectId = vr.RelationshipId
  where documentid = '1BC038F0-8922-403A-B471-C561AA6BEBAA' 
  -- Exclude the Focused Service as the source
  AND FromObjectId <> '57722EDF-9C45-46F6-8F89-8EAE9F40F92B'
  -- Exclude the Drawing as the source
  AND FromObjectId <> '1BC038F0-8922-403A-B471-C561AA6BEBAA'
 
*/

namespace DRPlanningTool
{
    public partial class frmMain : Form, IDrawingForm, IMainForm
    {
        #region Constructors

        public frmMain()
        {
            InitializeComponent();

            // Upgrade the settings
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
            }

            Main.MainForm = this;
            Main.MDIForm = null;
            Main.DrawingsForm = null;
        }

        #endregion

        #region Event Handlers

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout AboutForm = new frmAbout();

            AboutForm.Show(this);
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

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Clear();
            _SearchFilter = "";

            FilterList();

            txtFilter.Focus();
            txtFilter.SelectionStart = 0;
            txtFilter.SelectAll();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            _SearchFilter = txtFilter.Text.Trim();

            FilterList();

            txtFilter.Focus();
            txtFilter.SelectionStart = 0;
            txtFilter.SelectAll();
        }

        private void bulkDatabaseUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDatabaseUpdate UpdateForm = new frmDatabaseUpdate();

            UpdateForm.Show(this);
        }

        private void BusinessApplicationImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CloseBusinessApplicationImageFormWindows();
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

        private void createManagementPackToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void createStubToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DependencyMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateOrSelectDependencyMapForm(Main.BusinessApplicationRunbook, Properties.Settings.Default.ShowServersOnFullDependencyMap, null, LayoutMethod.MDS, true); // full map
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.ExportDetails();
        }

        private void developmentStubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(true, Global.Environment.Development, lvwDrawings);
        }

        private void developmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(false, Global.Environment.Development, lvwDrawings);
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

        private void errorCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessageCodes ErrorCodes = new frmMessageCodes();

            ErrorCodes.Show(this);
            ErrorCodes.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.DoExit();
        }

        private void findSystemCenterComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSystemCenterQuery SystemCenterQuery = new frmSystemCenterQuery(Main.SCOMServerName, Main.SCSMServerName);

            SystemCenterQuery.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.DoExit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Main.Load();
            
            //#region Set Trace Listener output form size and location

            //// It is important to do these first
            //ISA.Helper.Properties.frmProgressLocation = Properties.Settings.Default.frmProgressLocation;
            //ISA.Helper.Properties.frmProgressSize = Properties.Settings.Default.frmProgressSize;

            //#endregion

            //#region Trace Listener

            //// Code to allow Debug.WriteLine to work across all Assemblies
            //TextWriterTraceListener[] listeners = new TextWriterTraceListener[] {
            //                                                                     new TextTraceListener(toolStripStatusLabel),
            //                                                                     new TextWriterTraceListener(Console.Out)
            //                                                                    };

            //Debug.Listeners.AddRange(listeners);

            //#endregion

            //#region Setup Information form

            //_InformationForm = new frmInformation(this);

            //if (Properties.Settings.Default.frmInformationSize.Width != 0 && Properties.Settings.Default.frmInformationSize.Height != 0)
            //{
            //    _InformationForm.Size = Properties.Settings.Default.frmInformationSize;
            //}

            //if (Properties.Settings.Default.frmInformationLocation.X != 0 && Properties.Settings.Default.frmInformationLocation.Y != 0)
            //{
            //    _InformationForm.Location = Properties.Settings.Default.frmInformationLocation;
            //}

            //#endregion

            //ISA.Helper.Properties.AllowSaveFormLocations = true;

            //#region Set Main Form location and size

            //if (Properties.Settings.Default.frmMainLocation.X != 0 && Properties.Settings.Default.frmMainLocation.Y != 0)
            //{
            //    this.Location = Properties.Settings.Default.frmMainLocation;
            //}
            //else
            //{
            //    this.Location = new Point(0, 0);
            //}

            //if (Properties.Settings.Default.frmMainSize.Width != 0 && Properties.Settings.Default.frmMainSize.Height != 0)
            //{
            //    this.Size = Properties.Settings.Default.frmMainSize;
            //}
            //else
            //{
            //    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            //}

            //#endregion

            //if (Properties.Settings.Default.ConnectToProductionSQLUponStartup && (ISA.Helper.Properties.DependencyDatabaseRO || ISA.Helper.Properties.DependencyDatabaseRW))
            //{
            //    DoSetup();

            //    LoadDependencyData(Properties.Settings.Default.iServer_ProductionDocumentRootID);
            //}

            //this.Activate();
            //this.Refresh();
        }

        private void frmMain_Resize(object sender, EventArgs e)
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

        private void iServerDevelopmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.LoadDependencyData(Properties.Settings.Default.iServer_DevelopmentDocumentRootID);
        }

        private void iServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.DoSetup();

            Main.LoadDependencyData(Properties.Settings.Default.iServer_ProductionDocumentRootID);
        }

        private void iServer_AnnouncementReceived(object Sender, ISA.DataLayer.AnnouncementEventArgs e)
        {
            Debug.WriteLine(e.Text, "information");

            if (e.RelatedObject is BusinessApplication)
            {
                Main.AddDrawingException(e.Text.TrimStart('.'), (BusinessApplication)e.RelatedObject);
            }
            else if (e.RelatedObject is Server)
            {
                Main.AddDrawingException(e.Text.TrimStart('.'), (Server)e.RelatedObject);
            }
            else if (e.RelatedObject is Drawing)
            {
                Main.AddDrawingException(e.Text.TrimStart('.'));
            }
            else
            {
                Main.AddDrawingException(e.Text.TrimStart('.'));
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lvwDrawings_ItemActivate(object sender, EventArgs e)
        {
            DoSelectionChange();
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

        private void lvwDrawings_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BusinessApplication Service = (BusinessApplication)lvwDrawings.SelectedItems[0].Tag;

            Main.CreateOrSelectBusinessApplicationForm(lvwDrawings.SelectedItems[0].Text, Service);
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusinessApplication Service = (BusinessApplication)lvwDrawings.SelectedItems[0].Tag;

            Main.CreateOrSelectBusinessApplicationForm(lvwDrawings.SelectedItems[0].Text, Service);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.DoToolsOptions();

            #region Delete me
            //bool OriginalEmulatedState = ISA.Helper.Properties.ShowEmulatedState;
            //bool OriginalShowDrawingStatusOnServiceValue = Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication;
            //bool OriginalShowDrawingLessServicesValue = Properties.Settings.Default.ShowDrawingLessBusinessApplications;
            //bool OriginalShowDrawingStateColumnValue = Properties.Settings.Default.ShowDrawingStatusColumn;
            //bool OriginalSystemCenterUsageValue = Properties.Settings.Default.EnableSystemCenterFunctions;
            //bool OriginalInScopeValue = Properties.Settings.Default.ShowInScopeServicesOnly;

            //string OriginalStreamAServiceDependency = Properties.Settings.Default.Restore_StreamAServicePrerequisite;
            //string OriginalStreamBServiceDependency = Properties.Settings.Default.Restore_StreamBServicePrerequisite;
            //string OriginalStreamCServiceDependency = Properties.Settings.Default.Restore_StreamCServicePrerequisite;
            //string OriginalStreamDServiceDependency = Properties.Settings.Default.Restore_StreamDServicePrerequisite;
            //string OriginalStreamEServiceDependency = Properties.Settings.Default.Restore_StreamEServicePrerequisite;

            //bool RebuildRunbooks = false;

            //frmOptions OptionsForm = null;

            //if (_DependencyCollection == null)
            //{
            //    OptionsForm = new frmOptions(this, null, null); //_BusinessApplicationRunbook);
            //}
            //else
            //{
            //    OptionsForm = new frmOptions(this, _DependencyCollection.Applications, _DependencyCollection.Servers); //_BusinessApplicationRunbook);
            //}

            //OptionsForm.ShowDialog();
            //OptionsForm.Dispose();
            //OptionsForm = null;

            //ISA.Helper.Properties.ShowDebugInformation = Properties.Settings.Default.ShowDebugInformation;

            //lvwDrawings.CheckBoxes = Properties.Settings.Default.ShowCheckboxesOnServiceList;

            //if (OriginalShowDrawingLessServicesValue != Properties.Settings.Default.ShowDrawingLessBusinessApplications)
            //{
            //    LoadDependencyData(Properties.Settings.Default.iServer_ProductionDocumentRootID);
            //}

            //if (OriginalShowDrawingStatusOnServiceValue != Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication)
            //{
            //    //ResetServerStates();
            //}

            //if ((OriginalSystemCenterUsageValue != Properties.Settings.Default.EnableSystemCenterFunctions) && (Properties.Settings.Default.EnableSystemCenterFunctions))
            //{
            //    UpdateSystemCenterAuthorStatus();
            //    ISA.Helper.Properties.SystemCenterFunctionsEnabled = Properties.Settings.Default.EnableSystemCenterFunctions;
            //    retrieveSCOMServerHealthToolStripMenuItem.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
            //    toolStripButtonGetServerHealth.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
            //}

            //if (Properties.Settings.Default.ShowExperimentalFeatures)
            //{
            //    DependencyMapToolStripMenuItem.Visible = true;
            //}
            //else
            //{
            //    DependencyMapToolStripMenuItem.Visible = false;

            //}

            //bool ResultantEmulatedState = ISA.Helper.Properties.ShowEmulatedState;

            //// Check to see if the emulation state has changed
            //if (OriginalEmulatedState != ResultantEmulatedState)
            //{
            //    // Yes, it has changed - update all services to reflect the new state
            //    //DetermineAllBusinessApplicationState();
            //}

            //if (ISA.Helper.Properties.ShowEmulatedState)
            //{
            //    //StateLabel.Text = "State: Emulated";
            //    //StateLabel.ForeColor = Color.Red;
            //}
            //else
            //{
            //    //StateLabel.Text = "State: Actual";
            //    //StateLabel.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            //}

            //if (OriginalShowDrawingStateColumnValue != Properties.Settings.Default.ShowDrawingStatusColumn)
            //{
            //    if (Properties.Settings.Default.ShowDrawingStatusColumn)
            //    {
            //        // Added
            //        lvwDrawings.Columns.Add("Status");
            //        lvwDrawings.Columns[0].Width = 200;
            //        lvwDrawings.Columns[1].Width = 80;
            //    }
            //    else
            //    {
            //        // removed
            //        lvwDrawings.Columns.RemoveAt(1);
            //        lvwDrawings.Columns[0].Width = 280;
            //    }
            //}

            //// Check if the Stream dependencies have changed, and if so, rebuild the runbooks
            //if (
            //    (OriginalStreamAServiceDependency != Properties.Settings.Default.Restore_StreamAServicePrerequisite) ||
            //    (OriginalStreamBServiceDependency != Properties.Settings.Default.Restore_StreamBServicePrerequisite) ||
            //    (OriginalStreamCServiceDependency != Properties.Settings.Default.Restore_StreamCServicePrerequisite) ||
            //    (OriginalStreamDServiceDependency != Properties.Settings.Default.Restore_StreamDServicePrerequisite) ||
            //    (OriginalStreamEServiceDependency != Properties.Settings.Default.Restore_StreamEServicePrerequisite)
            //    )
            //{
            //    RebuildRunbooks = true;

            //    ISA.Helper.Properties.StreamAServiceName = Properties.Settings.Default.Restore_StreamAServicePrerequisite;
            //    ISA.Helper.Properties.StreamBServiceName = Properties.Settings.Default.Restore_StreamBServicePrerequisite;
            //    ISA.Helper.Properties.StreamCServiceName = Properties.Settings.Default.Restore_StreamCServicePrerequisite;
            //    ISA.Helper.Properties.StreamDServiceName = Properties.Settings.Default.Restore_StreamDServicePrerequisite;
            //    ISA.Helper.Properties.StreamEServiceName = Properties.Settings.Default.Restore_StreamEServicePrerequisite;
            //}

            //// Check if the Show In Scope Services option has changed, and if so, rebuild the Runbooks
            //if (OriginalInScopeValue != Properties.Settings.Default.ShowInScopeServicesOnly)
            //{
            //    RebuildRunbooks = true;

            //    if (!Properties.Settings.Default.ShowInScopeServicesOnly && _DependencyCollection != null)
            //    {
            //        frmShowNotInScopeServices ServicesNotInScopeForm = new frmShowNotInScopeServices(_DependencyCollection.Applications, this);

            //        ServicesNotInScopeForm.ShowDialog();
            //    }
            //}

            //if (RebuildRunbooks)
            //{
            //    // for now we will just restart the app as the topo sort object is cleared, and we don't want to have to rebuild it
            //    // from scratch

            //    MessageBox.Show("The DR Planning Tool must now be restarted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            //    Process.Start(Application.ExecutablePath);

            //    Application.Exit();
            //}
            #endregion
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

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwDrawings.SelectedItems.Count > 0)
            {
                BusinessApplication Service = (BusinessApplication)lvwDrawings.SelectedItems[0].Tag;

                Main.CreateOrSelectBusinessApplicationImageForm(Service);

                //LoadBusinessApplicationImage(Service);
            }
        }

        private void productionStubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(true, Global.Environment.Production, lvwDrawings);
        }

        private void productionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(false, Global.Environment.Production, lvwDrawings);
        }

        private void recoveryEstimatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecoveryEstimates RecoveryForm = new frmRecoveryEstimates(this, Main.BusinessApplicationRunbook);

            RecoveryForm.Show(this);
        }

        private void resetServerStatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.ResetServerStates();
        }

        private void retrieveSCOMServerHealthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.GetServerHealth();
        }

        private void runbookFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CloseListFormWindows();
        }

        private void serverFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CloseServerFormWindows();
        }

        private void serverListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.ViewServerList();
        }

        private void serverRunbookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.ViewServerRunbook();
        }

        private void serverSharesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShares SharesForm = new frmShares();

            SharesForm.Owner = this;
            SharesForm.Show(this);
        }

        private void serviceFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CloseBusinessApplicationFormWindows();
        }

        private void serviceRunbookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.ViewBusinessApplicationRunbook();
        }

        private void simulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSimulation SimulationForm = new frmSimulation(Main.DependencyCollection.Applications, Main.DependencyCollection.Servers, Main.DependencyCollection.Relationships, Main.DependencyCollection.Sites);

            SimulationForm.Show();
        }

        private void sRMRunbookToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Main.ExportSRMRunbook();
        }

        private void sRMRunbookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.ViewSRMRunbook();
        }

        private void StatusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void testStubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(true, Global.Environment.Test, lvwDrawings);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateManagementPacksFromSelectedItems(false, Global.Environment.Test, lvwDrawings);
        }

        private void tmrUpdates_Tick(object sender, EventArgs e)
        {
            // Disable the timer
            tmrUpdates.Enabled = false;

            List<iServer.ObjectChanges> MultipleChangeData = new List<iServer.ObjectChanges>();
            string Query = "";

            if (Main.LastDrawingDatabaseTimeStamp.TimeStampID == "")  // If we have just started up, just get the last Timestamp
            {
                Query = "SELECT TOP 1 t.TimeStampID, t.DateCreated, t.CreatedBy, t.LastModified, t.LastModifiedBy, t.LastValidDate, t.LastValidStatus, u.Username, o.ObjectName FROM TimeStamp t LEFT JOIN [user] u ON t.LastModifiedBy = u.UserID LEFT JOIN Version v ON v.TimeStampID = t.TimeStampID LEFT JOIN Object o ON o.ObjectID = v.ObjectID ORDER BY t.LastModified DESC";
            }
            else // Otherwise, determine all the changes that have been made since we last looked
            {
                // This will tell us about every COMMITTED change
                Query = "SELECT t.TimeStampID, t.DateCreated, t.CreatedBy, t.LastModified, t.LastModifiedBy, t.LastValidDate, t.LastValidStatus, u.Username, o.ObjectName FROM TimeStamp t LEFT JOIN [user] u ON t.LastModifiedBy = u.UserID LEFT JOIN Version v ON v.TimeStampID = t.TimeStampID LEFT JOIN Object o ON o.ObjectID = v.ObjectID WHERE t.LastModified > '" + Main.LastDrawingDatabaseTimeStamp.LastModified + "' ORDER BY t.LastModified DESC";

                // If we want to know about ANY change, including the cancelation of "Check-In"'s, then we just need to compare the last value with the last notified value
                //Query = "SELECT TOP 1 t.TimeStampID, t.DateCreated, t.CreatedBy, t.LastModified, t.LastModifiedBy, t.LastValidDate, t.LastValidStatus, u.Username, o.ObjectName FROM TimeStamp t LEFT JOIN [iServerDB].[dbo].[user] u ON t.LastModifiedBy = u.UserID LEFT JOIN Version v ON v.TimeStampID = t.TimeStampID LEFT JOIN Object o ON o.ObjectID = v.ObjectID ORDER BY t.LastModified DESC";
            }

            // With the above queries, Row[0] is always the most recent change

            if (Main.DependencyDatabase.OpenConnection(Properties.Settings.Default.iServer_DatabaseConnectionString))
            {
                //DataSet ChangeData = _iServerDatabase.Execute(Query);
                DataTable ChangeData = Main.DependencyDatabase.Execute(Query);

                if (ChangeData != null)
                {
                    foreach (DataRow Row in ChangeData.Rows)
                    {
                        iServer.ObjectChanges ObjectChangeData = new iServer.ObjectChanges();

                        ObjectChangeData.TimeStampID = Row[0].ToString();
                        ObjectChangeData.DateCreated = Row[1].ToString();
                        ObjectChangeData.CreatedBy = Convert.ToInt16(Row[2]);
                        int ms = ((DateTime)Row[3]).Millisecond;
                        ObjectChangeData.LastModified = ((DateTime)Row[3]).ToString("yyyyMMdd HH:mm:ss.") + ms.ToString();
                        ObjectChangeData.LastModifiedBy = Convert.ToInt16(Row[4]);
                        ObjectChangeData.Username = Row[7].ToString();

                        ObjectChangeData.ObjectName = Row[8].ToString();

                        MultipleChangeData.Add(ObjectChangeData);
                    }

                    // If I have a Last Database TimeStamp, then I have run previously (i.e. in this session)
                    if (MultipleChangeData.Count > 0)
                    //if (_LastDrawingDatabaseTimeStamp.TimeStampID != "" && MultipleChangeData.Count > 0)
                    {
                        iServer.ObjectChanges LastTChanges = MultipleChangeData[0];

                        if (MultipleChangeData[0].TimeStampID != Main.LastDrawingDatabaseTimeStamp.TimeStampID)
                        {
                            //Someone has made a Change to the database

                            Main.LastDrawingDatabaseTimeStamp = MultipleChangeData[0];

                            //Debug.WriteLine(MultipleChangeData.Count.ToString() + " Drawing Database change(s) detected at " + MultipleChangeData[0].LastModified.ToString() + "!  You may need to reload your Drawing data.");
                            Main.AddDatabaseUpdate("[INF1078] " + MultipleChangeData.Count.ToString() + " Drawing Database change(s) detected at " + MultipleChangeData[0].LastModified.ToString() + "!  You may need to reload your Drawing data.");
                            // From this list we can work out what changes have occurred.

                            // TODO!!!

                            foreach (iServer.ObjectChanges Change in MultipleChangeData)
                            {
                                if (Change.ObjectName != "")
                                {
                                    //Debug.WriteLine("...Change to " + Change.ObjectName + " by " + Change.Username + " at " + Change.LastModified.ToString());
                                    Main.AddDatabaseUpdate("...[INF1079] Change to " + Change.ObjectName + " by " + Change.Username + " at " + Change.LastModified.ToString());
                                }
                                else
                                {
                                    //Debug.WriteLine("...Change by " + Change.Username + " at " + Change.LastModified.ToString());
                                    Main.AddDatabaseUpdate("...[INF1080] Change by " + Change.Username + " at " + Change.LastModified.ToString());
                                }
                            }
                        }
                    }
                    else // I haven't run previously, so just grab the last change ID
                    {
                        //Debug.WriteLine("Last Database change was at " + MultipleChangeData[0].LastModified + " by " + MultipleChangeData[0].LastModifiedBy);
                    }

                    if (MultipleChangeData.Count > 0)
                    {
                        Main.LastDrawingDatabaseTimeStamp = MultipleChangeData[0];
                    }
                }
            }

            //Enable the timer
            tmrUpdates.Enabled = true;
        }

        private void toolStripButtonCreateManagementPacks_Click(object sender, EventArgs e)
        {
            frmEnvironment EnvironmentForm = new frmEnvironment();
            Global.Environment SelectedEnvironment = Global.Environment.None;
            DialogResult EnvironmentResult = DialogResult.None;
            DialogResult StubOrFullResult = DialogResult.None;

            EnvironmentResult = EnvironmentForm.ShowDialog(this);
            SelectedEnvironment = EnvironmentForm.SelectedEnvironment;

            EnvironmentForm.Close();
            EnvironmentForm.Dispose();

            if (EnvironmentResult == DialogResult.OK && SelectedEnvironment != Global.Environment.None)
            {
                StubOrFullResult = MessageBox.Show("Create full Management Packs?\n\nSelect Yes for Full MP's, No for Stub MP's", "Please choose", MessageBoxButtons.YesNoCancel);

                bool CreateAsStub = StubOrFullResult == DialogResult.No;

                if (StubOrFullResult != DialogResult.Cancel)
                {
                    Main.CreateAllManagementPacks(SelectedEnvironment, CreateAsStub);
                }
            }
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            Main.DoExit();
        }

        private void toolStripButtonExportDetails_Click(object sender, EventArgs e)
        {
            Main.ExportDetails();
        }

        private void toolStripButtonExportSRMRunbook_Click(object sender, EventArgs e)
        {
            Main.ExportSRMRunbook();
        }

        private void toolStripButtonGetServerHealth_Click(object sender, EventArgs e)
        {
            Main.GetServerHealth();
        }

        private void toolStripButtonLoadiServerDevelopment_Click(object sender, EventArgs e)
        {
            Main.LoadDependencyData(Properties.Settings.Default.iServer_DevelopmentDocumentRootID);
        }

        private void toolStripButtonLoadiServerProduction_Click(object sender, EventArgs e)
        {
            Main.DoSetup();

            Main.LoadDependencyData(Properties.Settings.Default.iServer_ProductionDocumentRootID);
        }

        private void toolStripButtonResetServerStates_Click(object sender, EventArgs e)
        {
            Main.ResetServerStates();
        }

        private void toolStripButtonViewServerList_Click(object sender, EventArgs e)
        {
            Main.ViewServerList();
        }

        private void toolStripButtonViewServerRunbook_Click(object sender, EventArgs e)
        {
            Main.ViewServerRunbook();
        }

        private void toolStripButtonViewServiceRunbook_Click(object sender, EventArgs e)
        {
            Main.ViewBusinessApplicationRunbook();
        }

        private void toolStripButtonViewSRMRunbook_Click(object sender, EventArgs e)
        {
            Main.ViewSRMRunbook();
        }

        private void toolStripStatusLabel_TextChanged(object sender, EventArgs e)
        {
            //this.Refresh();
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

        //private void validateRulesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    LoadValidationRulesForm();
        //}

        private void ViewDependencyMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<BusinessApplication> Services = new List<BusinessApplication>();

            foreach (ListViewItem Item in lvwDrawings.SelectedItems)
            {
                Services.Add((BusinessApplication)Item.Tag);
            }

            Main.CreateOrSelectDependencyMapForm(Services, true, Services, LayoutMethod.SugiyamaScheme, false);
        }

        #endregion

        #region Fields

        //private List<frmBusinessApplication> _BusinessApplicationForms = new List<frmBusinessApplication>();
        //private List<frmBusinessApplicationImage> _BusinessApplicationImageForms = new List<frmBusinessApplicationImage>();
        //private List<BusinessApplication> _BusinessApplicationRunbook = null;
        //private iServer _DataLayer = null;
        //public TextTraceListener[] _DebugWriter = null;
        //private Collection _DependencyCollection = null;
        //private SQLServer _DependencyDatabase = new SQLServer(Properties.Settings.Default.SQLServerQueryTimeout);
        //private List<frmDependencyMap> _DependencyMapForms = new List<frmDependencyMap>();
        //private frmInformation _InformationForm = null;
        //private iServer.ObjectChanges _LastDrawingDatabaseTimeStamp = new iServer.ObjectChanges();
        //private List<frmList> _ListForms = new List<frmList>();
        //private SQLServer _LocalDatabase = new SQLServer(Properties.Settings.Default.SQLServerQueryTimeout);
        //private List<BusinessApplication> _OutOfScopeApplicationsToShow = new List<BusinessApplication>();
        //private SQLServer _PrimaryvCenterDatabase = new SQLServer(Properties.Settings.Default.SQLServerQueryTimeout);
        //private string _SCOMServerName = Properties.Settings.Default.SystemCenter_SCOMServer;
        //private string _SCSMServerName = Properties.Settings.Default.SystemCenter_SCSMServer;
        private string _SearchFilter = "";

        //private SQLServer _SecondaryvCenterDatabase = new SQLServer(Properties.Settings.Default.SQLServerQueryTimeout);
        //private List<frmServer> _ServerForms = new List<frmServer>();
        //private List<Server> _ServerRunbook = null;

        #endregion

        #region Interface Methods

        //public void AddBusinessApplicationsToListView()
        //{
        //    lvwDrawings.Items.Clear();

        //    lvwDrawings.BeginUpdate();
        //    foreach (BusinessApplication Application in Main.DataLayer.Collection.Applications)
        //    {
        //        if (Application.InScope || Application.OverrideInScope)
        //        {
        //            if (Application.Drawing != null)
        //            {
        //                Main.AddBusinessApplicationToListView(Application, Application.Drawing.StatusText, lvwDrawings);
        //            }
        //            else
        //            {
        //                Main.AddBusinessApplicationToListView(Application, "N/A", lvwDrawings);
        //            }
        //        }
        //    }
        //    lvwDrawings.EndUpdate();

        //    this.Refresh();
        //}

        //public void AddBusinessApplicationToListView(BusinessApplication Application, string Text)
        //{
        //    Main.AddBusinessApplicationToListView(Application, Text, lvwDrawings);
        //}

        public void BeginUpdate()
        {
            lvwDrawings.BeginUpdate();
        }

        public void DisableGUI()
        {
            toolStripButtonLoadiServerProduction.Enabled = false;
            btnClearFilter.Enabled = false;
            btnFilter.Enabled = false;
            txtFilter.Enabled = false;
        }

        public void EnableDependencyMapToolstrip()
        {
            DependencyMapToolStripMenuItem.Visible = true;
        }

        public void EnableGUI()
        {
            btnClearFilter.Enabled = true;
            btnFilter.Enabled = true;
            txtFilter.Enabled = true;

            serverRunbookToolStripMenuItem.Enabled = true;
            serviceRunbookToolStripMenuItem.Enabled = true;
            sRMRunbookToolStripMenuItem.Enabled = true;
            detailsToolStripMenuItem.Enabled = true;
            sRMRunbookToolStripMenuItem1.Enabled = true;
            serverListToolStripMenuItem.Enabled = true;
            resetServerStatesToolStripMenuItem.Enabled = true;
            retrieveSCOMServerHealthToolStripMenuItem.Enabled = true;
            toolStripButtonResetServerStates.Enabled = true;
            toolStripButtonGetServerHealth.Enabled = true;
            recoveryEstimatesToolStripMenuItem.Enabled = true;
            DependencyMapToolStripMenuItem.Enabled = true;
            simulationToolStripMenuItem.Enabled = true;

            toolStripButtonLoadiServerProduction.Enabled = true;
        }

        public void EnableSystemCenterToolStrip()
        {
            retrieveSCOMServerHealthToolStripMenuItem.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
            toolStripButtonGetServerHealth.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
        }

        public void EndUpdate()
        {
            lvwDrawings.EndUpdate();
        }

        public void Reset()
        {
            pictureBox.Image = null;
            pictureBox.BackgroundImage = null;
            lvwDrawings.Items.Clear();
            this.Refresh();
        }

        public void SetHeight(int Height)
        {
            base.Size = new Size(this.Size.Width, Height);
        }

        public void SetLocation(Point Location)
        {
            base.Location = Location;
        }

        public void SetSize(Size Size)
        {
            base.Size = Size;
        }

        public void SetupCheckboxes()
        {
            lvwDrawings.CheckBoxes = Properties.Settings.Default.ShowCheckboxesOnServiceList;
        }

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

        public void UpdateCreateManagementPacksToolstrip()
        {
            toolStripButtonCreateManagementPacks.Enabled = ISA.Helper.Properties.ManagementPackAuthor;
        }

        public void UpdateLoadDependencyDataToolstripForReadOnly()
        {
            toolStripButtonLoadiServerProduction.Enabled = ISA.Helper.Properties.DependencyDatabaseRO;
        }

        public void UpdateLoadDependencyDataToolstripForReadWrite()
        {
            toolStripButtonLoadiServerProduction.Enabled = ISA.Helper.Properties.DependencyDatabaseRO || ISA.Helper.Properties.DependencyDatabaseRW;
            iServerToolStripMenuItem.Enabled = ISA.Helper.Properties.DependencyDatabaseRO || ISA.Helper.Properties.DependencyDatabaseRW;
        }

        public void UpdateSystemCenterToolStrip()
        {
            ISA.Helper.Properties.SystemCenterFunctionsEnabled = Properties.Settings.Default.EnableSystemCenterFunctions;
            retrieveSCOMServerHealthToolStripMenuItem.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
            toolStripButtonGetServerHealth.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
        }

        #endregion

        #region Private Methods

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

        #endregion

        #region Properties

        //public new Size Size
        //{
        //   get
        //    {
        //        return base.Size;
        //    }

        //    protected set
        //    {
        //        base.Size = value;
        //    }

        //}

        //public new Point Location
        //{
        //    get
        //    {
        //        return base.Location;
        //    }

        //    protected set
        //    {
        //        base.Location = value;
        //    }

        //}

        //public new int Height
        //{
        //    get
        //    {
        //        return base.Size.Height;
        //    }

        //    protected set
        //    {
        //        this.Size = new Size(this.Width, value);
        //    }

        //}

        #endregion

        #region Public Methods

        //public void AddDatabaseUpdate(string Message)
        //{
        //    _InformationForm.AddDatabaseWarning(Message);
        //}

        //public void AddDatabaseWarning(string Message, BusinessApplication Application)
        //{
        //    Application.Warnings.Add(Message);
        //    _InformationForm.AddDatabaseWarning(Message, Application);
        //}

        //public void AddDatabaseWarning(string Message, Server Server)
        //{
        //    if (Server != null)
        //    {
        //        Server.Warnings.Add(Message);
        //    }
        //    _InformationForm.AddDatabaseWarning(Message, Server);
        //}

        //public void AddDrawingCriticalException(string Message, BusinessApplication Application)
        //{
        //    Application.Warnings.Add(Message);
        //    _InformationForm.AddDrawingCriticalException(Message, Application);
        //}

        //public void AddDrawingException(string Message)
        //{
        //    _InformationForm.AddDrawingException(Message);
        //}

        //public void AddDrawingException(string Message, BusinessApplication Application)
        //{
        //    Application.Warnings.Add(Message);
        //    _InformationForm.AddDrawingException(Message, Application);
        //}

        //public void AddDrawingException(string Message, Server Server)
        //{
        //    Server.Warnings.Add(Message);
        //    _InformationForm.AddDrawingException(Message, Server);
        //}

        //public void AddException(string Message)
        //{
        //    _InformationForm.AddException(Message);
        //}

        //public void AddException(string Message, BusinessApplication Application)
        //{
        //    Application.Warnings.Add(Message);
        //    _InformationForm.AddException(Message, Application);
        //}

        //public void AddOutOfScopeServer(Server Server)
        //{
        //    _InformationForm.AddOutOfScopeServer(Server);
        //    ISA.Helper.Properties.ReloadNecessary = true;
        //}

        //public void AddOutOfScopeApplication(BusinessApplication Application)
        //{
        //    _InformationForm.AddOutOfScopeApplication(Application);
        //    ISA.Helper.Properties.ReloadNecessary = true;
        //}

        //public void AddOutOfScopeApplications()
        //{
        //    foreach (BusinessApplication Service in _DependencyCollection.Applications)
        //    {
        //        if (!Service.InScope && !Service.OverrideInScope)
        //        {
        //            if (Properties.Settings.Default.ShowDebugInformation)
        //            {
        //                Debug.WriteLine("..." + Service.Name, "information");
        //            }

        //            AddOutOfScopeApplication(Service);
        //        }
        //    }
        //}

        //public void AddWarning(string Message)
        //{
        //    _InformationForm.AddWarning(Message);
        //}

        //public void AddWarning(string Message, BusinessApplication Application)
        //{
        //    Application.Warnings.Add(Message);
        //    _InformationForm.AddWarning(Message, Application);
        //}

        //public void AddWarning(string Message, Server Server)
        //{
        //    Server.Warnings.Add(Message);
        //    _InformationForm.AddWarning(Message, Server);
        //}

        //public void ClearWarnings()
        //{
        //    _InformationForm.Clear();
        //    _InformationForm.Refresh();
        //}

        //public void CreateDependencyMapForm(List<BusinessApplication> Applications, Form ParentForm, List<Relationship> Relationships, List<BusinessApplication> FocusedServices, bool ShowDependents, bool ShowServers, LayoutMethod Layout, bool BundleSplines)
        //{
        //    frmDependencyMap MapForm = new frmDependencyMap(Applications, this, Relationships, FocusedServices, ShowDependents, ShowServers, Layout, BundleSplines);

        //    MapForm.Owner = this;

        //    _DependencyMapForms.Add(MapForm);

        //    MapForm.Show(this);

        //}

        //public bool CreateManagementPackFromBusinessApplication(BusinessApplication BusinessApplication, bool CreateAsStub, Global.Environment Environment)
        //{
        //    #region Variables

        //    ServerUtilities SCOMUtilities = null; // new ServerUtilities(_SCOMServerName, _SCSMServerName);
        //    bool Result = false;
        //    List<string> ComponentServers = new List<string>();
        //    List<string> ComponentServices = new List<string>();
        //    int BusinessApplicationDrawingVersion = 0;
        //    string ErrorMessage = "";
        //    string ManagementPackXML = "";
        //    Version NewVersion = null;

        //    string ManagementPackID = "";
        //    string ManagementPackNamePrefix = Properties.Settings.Default.SystemCenter_ManagementPackIDPrefix; // "FIN.DistributedApplication.";
        //    string ManagementPackFriendlyName = Properties.Settings.Default.SystemCenter_ManagementPackFriendlyName; // "Finance Distributed Application - #SERVICENAME#";           
        //    string ManagementPackFilename = ISA.Helper.Methods.ReplaceTags(Properties.Settings.Default.SystemCenter_ManagementPackSaveFolderName, DateTime.Now, true); // @"C:\SVN\SCOM\Management Packs\FIN.DistributedApplication.#SERVICENAME#.xml";
        //    string ManagementPackDescription = Properties.Settings.Default.SystemCenter_ManagementPackDescription; // "This management pack defines the #SERVICENAME# Distributed Application.";
        //    string ManagementPackFriendlyNamePrefix = Properties.Settings.Default.SystemCenter_DistributedApplicationFriendlyNamePrefix; // "Finance - ";

        //    #endregion

        //    switch (Environment)
        //    {
        //        case Global.Environment.Development:
        //            SCOMUtilities = new ServerUtilities(Properties.Settings.Default.SystemCenter_DevSCOMServer, Properties.Settings.Default.SystemCenter_DevSCSMServer);
        //            break;
        //        case Global.Environment.Test:
        //            SCOMUtilities = new ServerUtilities(Properties.Settings.Default.SystemCenter_TestSCOMServer, Properties.Settings.Default.SystemCenter_TestSCSMServer);
        //            break;
        //        case Global.Environment.Production:
        //            SCOMUtilities = new ServerUtilities(Properties.Settings.Default.SystemCenter_SCOMServer, Properties.Settings.Default.SystemCenter_SCSMServer);
        //            break;
        //    }

        //    // Management Pack Version will be in the following form:
        //    // App Major . App Minor . Drawing Version . Service Version

        //    if (BusinessApplication.Drawing == null)
        //    {
        //        BusinessApplicationDrawingVersion = 1;
        //    }
        //    else
        //    {
        //        BusinessApplicationDrawingVersion = BusinessApplication.Drawing.Version;
        //    }

        //    if (!CreateAsStub)
        //    {
        //        NewVersion = new Version(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major,
        //                                 System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor,
        //                                 BusinessApplicationDrawingVersion,
        //                                 BusinessApplication.Version
        //                                );
        //    }
        //    else
        //    {
        //        // We need to create a Stub.  To allow over-writing of this stub to be easy, we will mockup the version number
        //        // The Stub version number will be:
        //        // 0 . AppMinor . DayOfYear . SecondsSinceMidnight

        //        NewVersion = new Version(0,
        //                                 System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor,
        //                                 DateTime.Now.DayOfYear,
        //                                 Convert.ToInt32(DateTime.Now.TimeOfDay.TotalSeconds)
        //                                 );

        //        // Note: this will make it next to impossible to over-write an actual MP with a stub
        //    }

        //    //if (Properties.Settings.Default.ImportSCOMManagementPacks || Properties.Settings.Default.ImportSCSMManagementPacks)
        //    //{
        //    ManagementPackID = ManagementPackNamePrefix + BusinessApplication.Name.Replace(" ", "_").Replace("(", "").Replace(")", "").Replace("-", "_");

        //    string ManagementPackFullPath = System.IO.Path.Combine(ManagementPackFilename, ManagementPackID + ".xml");

        //    if (!CreateAsStub)
        //    {
        //        // Build a list of Server names for the Management Pack
        //        foreach (Server ComponentServer in BusinessApplication.ComponentServers)
        //        {
        //            Relationship Relationship = Global.GetRelationship(BusinessApplication, ComponentServer, BusinessApplication.Relationships);

        //            if (!Relationship.StreamDependency) // Exclude Stream dependencies.  Currently not possible for Server <-> Service, but let's make sure
        //            {
        //                if (ComponentServer.AddToManagementPack) // Exclude Servers that were explicitly marked to not be added
        //                {
        //                    ComponentServers.Add(ComponentServer.Name);
        //                }
        //            }
        //        }

        //        // Build a list of Service names for the Management Pack
        //        foreach (BusinessApplication ComponentService in BusinessApplication.ComponentBusinessApplications)
        //        {
        //            Relationship Relationship = Global.GetRelationship(BusinessApplication, ComponentService, BusinessApplication.Relationships);

        //            if (!Relationship.StreamDependency) // Exclude Stream dependencies
        //            {
        //                ComponentServices.Add(ComponentService.Name);
        //            }
        //        }
        //    }

        //    ManagementPackCreationOptions CreationOptions = new ManagementPackCreationOptions();

        //    CreationOptions.ImportSCOMManagementPacks = Properties.Settings.Default.ImportSCOMManagementPacks; // true;
        //    CreationOptions.ImportSCSMManagementPacks = Properties.Settings.Default.ImportSCSMManagementPacks; // true;

        //    switch (Environment)
        //    {
        //        case Global.Environment.Development:
        //            if (Properties.Settings.Default.SystemCenter_DevSCOMServer != "")
        //            {
        //                CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_DevSCOMServer;
        //            }
        //            else if (Properties.Settings.Default.SystemCenter_DevSCSMServer != "")
        //            {
        //                CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_DevSCSMServer;
        //            }
        //            else
        //            {
        //                CreationOptions.ConnectionInfo.ManagementGroupServerName = "";
        //            }
        //            break;
        //        case Global.Environment.Test:
        //            if (Properties.Settings.Default.SystemCenter_DevSCOMServer != "")
        //            {
        //                CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_TestSCOMServer;
        //            }
        //            else if (Properties.Settings.Default.SystemCenter_DevSCSMServer != "")
        //            {
        //                CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_TestSCSMServer;
        //            }
        //            else
        //            {
        //                CreationOptions.ConnectionInfo.ManagementGroupServerName = "";
        //            }
        //            break;
        //        case Global.Environment.Production:
        //            if (Properties.Settings.Default.SystemCenter_DevSCOMServer != "")
        //            {
        //                CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_SCOMServer;
        //            }
        //            else if (Properties.Settings.Default.SystemCenter_DevSCSMServer != "")
        //            {
        //                CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_SCSMServer;
        //            }
        //            else
        //            {
        //                CreationOptions.ConnectionInfo.ManagementGroupServerName = "";
        //            }
        //            break;
        //    }

        //    CreationOptions.ManagementPackID = ManagementPackNamePrefix + BusinessApplication.Name.Replace(@" ", "_")
        //                                                                       .Replace(@"(", "")
        //                                                                       .Replace(@")", "")
        //                                                                       .Replace(@"/", "_")
        //                                                                       .Replace(@"\", "_")
        //                                                                       .Replace(@"-", "_");

        //    CreationOptions.ManagementPackFolderName = Properties.Settings.Default.SystemCenter_ManagementPackViewFolderName;
        //    CreationOptions.ManagementPackName = ManagementPackFriendlyName.Replace("#SERVICENAME#", BusinessApplication.Name);
        //    CreationOptions.ManagementPackDescription = ManagementPackDescription.Replace("#SERVICENAME#", BusinessApplication.Name);
        //    CreationOptions.ManagementPackFilename = ManagementPackFullPath;
        //    CreationOptions.ServiceInfo.ServiceName = BusinessApplication.Name;
        //    CreationOptions.ServiceInfo.ServiceFriendlyName = ManagementPackFriendlyNamePrefix + BusinessApplication.Name;
        //    CreationOptions.ManagementPackVersion = NewVersion;
        //    CreationOptions.ManagementPackEmptyTemplate = Properties.Settings.Default.SystemCenter_MPEmptyTemplate;
        //    CreationOptions.ManagementPackServersAndServicesTemplate = Properties.Settings.Default.SystemCenter_MPServersAndServicesTemplate;
        //    CreationOptions.ManagementPackServersTemplate = Properties.Settings.Default.SystemCenter_MPServersTemplate;
        //    CreationOptions.ManagementPackServicesTemplate = Properties.Settings.Default.SystemCenter_MPServicesTemplate;
        //    CreationOptions.AlwaysImportManagementPacks = Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus;

        //    Result = SCOMUtilities.CreateOrUpdateManagementPack(ManagementPackID, NewVersion, ComponentServers, ComponentServices, CreationOptions, out ErrorMessage, out ManagementPackXML);

        //    BusinessApplication.ManagementPackXML = ManagementPackXML;

        //    if (ErrorMessage != "")
        //    {
        //        Debug.WriteLine(ErrorMessage);
        //        Main.AddWarning(ErrorMessage, BusinessApplication);
        //    }

        //    //}
        //    //else
        //    //{
        //    //    Debug.WriteLine("[INF1024] User options prevent importing Management Packs into either System Center product", "warning");
        //    //    Result = false;
        //    //}

        //    return Result;
        //}

        //public void CreateOrSelectBusinessApplicationForm(string DrawingName, BusinessApplication Application)
        //{
        //    frmBusinessApplication SelectedBusinessApplicationForm = null;

        //    foreach (frmBusinessApplication ServiceForm in _BusinessApplicationForms)
        //    {
        //        if (ServiceForm.BusinessApplication == Application)
        //        {
        //            SelectedBusinessApplicationForm = ServiceForm;
        //        }
        //    }

        //    if (SelectedBusinessApplicationForm == null)
        //    {
        //        frmBusinessApplication NewBusinessApplicationForm = new frmBusinessApplication(DrawingName, Application, _DependencyCollection.Applications, _DependencyCollection.Relationships, this, _DependencyDatabase);

        //        NewBusinessApplicationForm.Owner = this;

        //        _BusinessApplicationForms.Add(NewBusinessApplicationForm);

        //        NewBusinessApplicationForm.Show(this);
        //        NewBusinessApplicationForm.Activate();
        //    }
        //    else
        //    {
        //        SelectedBusinessApplicationForm.Show();
        //        SelectedBusinessApplicationForm.BringToFront();
        //        SelectedBusinessApplicationForm.Activate();
        //        FlashWindow.Flash(SelectedBusinessApplicationForm, 5);
        //    }
        //}

        //public void CreateOrSelectBusinessApplicationImageForm(BusinessApplication Application)
        //{
        //    frmBusinessApplicationImage SelectedBusinessApplicationImageForm = null;

        //    foreach (frmBusinessApplicationImage ServiceForm in _BusinessApplicationImageForms)
        //    {
        //        if (ServiceForm.Image == Application.Drawing.Image)
        //        {
        //            SelectedBusinessApplicationImageForm = ServiceForm;
        //        }
        //    }

        //    if (SelectedBusinessApplicationImageForm == null)
        //    {
        //        frmBusinessApplicationImage NewBusinessApplicationImageForm = new frmBusinessApplicationImage(Application.Drawing.Image, this, Application.Name);

        //        NewBusinessApplicationImageForm.Owner = this;

        //        _BusinessApplicationImageForms.Add(NewBusinessApplicationImageForm);

        //        NewBusinessApplicationImageForm.Show(this);
        //        NewBusinessApplicationImageForm.Activate();
        //    }
        //    else
        //    {
        //        SelectedBusinessApplicationImageForm.Show();
        //        SelectedBusinessApplicationImageForm.BringToFront();
        //        SelectedBusinessApplicationImageForm.Activate();
        //        FlashWindow.Flash(SelectedBusinessApplicationImageForm, 5);
        //    }
        //}

        //public void CreateOrSelectDependencyMapForm(List<BusinessApplication> Applications, bool IncludeServers, List<BusinessApplication> FocusedApplications, LayoutMethod Layout, bool BundleSplines)
        //{
        //    frmDependencyMap SelectedMapForm = null;

        //    foreach (frmDependencyMap MapForm in _DependencyMapForms)
        //    {
        //        if (MapForm.Services.Count == 0 && MapForm.Services == Applications)
        //        {
        //            SelectedMapForm = MapForm;
        //        }
        //    }

        //    if (SelectedMapForm == null)
        //    {
        //        frmDependencyMap MapForm = new frmDependencyMap(Applications, this, _DependencyCollection.Relationships, FocusedApplications, true, IncludeServers, Layout, BundleSplines);

        //        MapForm.Owner = this;

        //        _DependencyMapForms.Add(MapForm);

        //        MapForm.Show(this);
        //        MapForm.Activate();
        //    }
        //    else
        //    {
        //        SelectedMapForm.Show();
        //        SelectedMapForm.BringToFront();
        //        SelectedMapForm.Activate();
        //        FlashWindow.Flash(SelectedMapForm, 5);
        //    }
        //}

        //public void CreateOrSelectServerForm(ref Server Server)
        //{
        //    frmServer SelectedServerForm = null;

        //    foreach (frmServer ServerForm in _ServerForms)
        //    {
        //        if (ServerForm.Server == Server)
        //        {
        //            SelectedServerForm = ServerForm;
        //        }
        //    }

        //    if (SelectedServerForm == null)
        //    {
        //        frmServer NewServerForm = new frmServer(Server, _DependencyCollection.Servers, _DependencyCollection.Applications, this, _DependencyDatabase);

        //        NewServerForm.Owner = this;

        //        _ServerForms.Add(NewServerForm);

        //        NewServerForm.Show(this);
        //        NewServerForm.Activate();
        //    }
        //    else
        //    {
        //        SelectedServerForm.Show();
        //        SelectedServerForm.BringToFront();
        //        SelectedServerForm.Activate();
        //        FlashWindow.Flash(SelectedServerForm, 5);
        //    }
        //}

        //public void DetermineAllBusinessApplicationInFocus()
        //{
        //    Cursor.Current = Cursors.WaitCursor;

        //    for (int i = 0; i < _BusinessApplicationRunbook.Count; i++)
        //    {
        //        BusinessApplication s = _BusinessApplicationRunbook[i];
        //        DetermineBusinessApplicationInFocus(ref s);
        //        _BusinessApplicationRunbook[i] = s;
        //    }

        //    UpdateChildForms();

        //    Cursor.Current = Cursors.Default;
        //}

        //public void DetermineAllBusinessApplicationRecoveryTasks()
        //{
        //    Cursor.Current = Cursors.WaitCursor;

        //    foreach (BusinessApplication BusinessApplication in _BusinessApplicationRunbook) // Perform in runbook order
        //    {
        //        DetermineBusinessApplicationRecoveryTasks(BusinessApplication);
        //    }

        //    UpdateChildForms();

        //    Cursor.Current = Cursors.Default;
        //}

        //public void DetermineAllBusinessApplicationState()
        //{
        //    Cursor.Current = Cursors.WaitCursor;

        //    lvwDrawings.BeginUpdate();

        //    if (Properties.Settings.Default.ShowDebugInformation)
        //    {
        //        Debug.WriteLine("[INF1198] Determining Business Application State...", "information");
        //    }

        //    // The Business Application Runbook is already in an order that ensures that dependencies are processed correctly
        //    foreach (BusinessApplication BusinessApplication in _BusinessApplicationRunbook) //_DC.Services
        //    {
        //        // If the state hasn't been set explicitly at the Service Level (it should be a combination of the states of it's Components),
        //        // then work out it's state.
        //        if (!BusinessApplication.StateSetAtApplicationLevel)
        //        {
        //            DetermineBusinessApplicationState(BusinessApplication);
        //        }
        //    }

        //    // Now we have to ensure that the State of any Service that isn't in the ServiceRunbook is also calculated
        //    foreach (BusinessApplication Service in _DependencyCollection.Applications)
        //    {
        //        if (Global.GetExistingBusinessApplication(Service, _BusinessApplicationRunbook) == null)
        //            DetermineBusinessApplicationState(Service);
        //    }

        //    UpdateChildForms();

        //    lvwDrawings.EndUpdate();

        //    Cursor.Current = Cursors.Default;

        //    Application.DoEvents();
        //}

        //public void ExportSRMRunbook()
        //{
        //    throw new NotImplementedException();
        //}

        public void FilterList()
        {
            lvwDrawings.Items.Clear();
            lvwDrawings.BeginUpdate();

            foreach (BusinessApplication Service in Main.DependencyCollection.Applications)
            {
                if (Service.Name.ToUpper().Contains(_SearchFilter.ToUpper()) || _SearchFilter.Trim() == "")
                {
                    Main.AddBusinessApplicationToListView(Service, Service.Drawing.Status.ToString(),lvwDrawings);
                }
            }

            lvwDrawings.EndUpdate();
        }

        //public void RemoveBusinessApplicationDependencyMapFormFromList(frmDependencyMap BusinessApplicationDependencyMapForm)
        //{
        //    _DependencyMapForms.Remove(BusinessApplicationDependencyMapForm);
        //}

        //public void RemoveBusinessApplicationFormFromList(frmBusinessApplication BusinessApplicationForm)
        //{
        //    _BusinessApplicationForms.Remove(BusinessApplicationForm);
        //}

        //public void RemoveBusinessApplicationImageFormFromList(frmBusinessApplicationImage BusinessApplicationImageForm)
        //{
        //    _BusinessApplicationImageForms.Remove(BusinessApplicationImageForm);
        //}

        //public void RemoveOutOfScopeServer(Server Server)
        //{
        //    _InformationForm.RemoveOutOfScopeServer(Server);
        //    ISA.Helper.Properties.ReloadNecessary = true;
        //}

        //public void RemoveOutOfScopeApplication(BusinessApplication Application)
        //{
        //    _InformationForm.RemoveOutOfScopeApplication(Application);
        //    ISA.Helper.Properties.ReloadNecessary = true;
        //}

        //public void RemoveRunbookFormFromList(frmList RunbookForm)
        //{
        //    _ListForms.Remove(RunbookForm);
        //}

        //public void RemoveServerFormFromList(frmServer ServerForm)
        //{
        //    _ServerForms.Remove(ServerForm);
        //}

        //public void ResetApplicationState()
        //{
        //    CloseListFormWindows();
        //    CloseServerFormWindows();
        //    CloseBusinessApplicationFormWindows();
        //    CloseBusinessApplicationImageFormWindows();
        //    CloseDependencyMapFormWindows();
        //    pictureBox.Image = null;
        //    pictureBox.BackgroundImage = null;

        //    Main.ClearWarnings();

        //    Main.DependencyCollection = new Collection();
        //    Main._ServerRunbook = null;
        //    Main._BusinessApplicationRunbook = null;
        //    //_SRMBusinessApplicationRunbook = null;
        //    //_EmulatedData = false;

        //    Main.DataLayer.Collection = new Collection();
        //    Main.DataLayer.Servers.Clear();
        //    Main.DataLayer.Applications.Clear();

        //    ISA.Helper.Properties.ManagementPackAuthor = false;
        //    ISA.Helper.Properties.ReloadNecessary = false;

        //    toolStripButtonCreateManagementPacks.Enabled = false;
        //    toolStripButtonResetServerStates.Enabled = false;
        //    toolStripButtonGetServerHealth.Enabled = false;

        //    detailsToolStripMenuItem.Enabled = false;
        //    sRMRunbookToolStripMenuItem1.Enabled = false;
        //    serverListToolStripMenuItem.Enabled = false;
        //    serverRunbookToolStripMenuItem.Enabled = false;
        //    serviceRunbookToolStripMenuItem.Enabled = false;
        //    sRMRunbookToolStripMenuItem.Enabled = false;
        //    resetServerStatesToolStripMenuItem.Enabled = false;
        //    retrieveSCOMServerHealthToolStripMenuItem.Enabled = false;
        //    recoveryEstimatesToolStripMenuItem.Enabled = false;
        //    DependencyMapToolStripMenuItem.Enabled = false;
        //    simulationToolStripMenuItem.Enabled = false;

        //    lvwDrawings.Items.Clear();

        //    this.Refresh();
        //}

        //public void ResetBusinessApplicationShowInRunbook()
        //{
        //    Cursor.Current = Cursors.WaitCursor;

        //    if (_DependencyCollection.Applications != null)
        //    {
        //        foreach (BusinessApplication BusinessApplication in _DependencyCollection.Applications) // _BusinessApplicationRunbook)
        //        {
        //            BusinessApplication.Save(_DependencyDatabase);
        //        }
        //    }

        //    Cursor.Current = Cursors.Default;
        //}

        //public void ResetServerShowInRunbook()
        //{
        //    Cursor.Current = Cursors.WaitCursor;

        //    if (_DependencyCollection.Servers != null)
        //    {
        //        foreach (Server Server in _DependencyCollection.Servers)
        //        {
        //            Server.Save(_DependencyDatabase);
        //        }
        //    }

        //    Cursor.Current = Cursors.Default;
        //}

        //public void SetEmulatedLabel(string Text)
        //{
        //    //toolStripStatusLabelData.Text = Text;
        //}

        #endregion

        private void frmMain_Shown(object sender, EventArgs e)
        {
            //if (!ISA.Helper.Methods.IsOnScreen(this))
            //{
            //    this.Location = new Point(10, 10);
            //}
        }
    }
}
