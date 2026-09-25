using ISA.Dependency;
using ISA.Orbus;
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
    public partial class frmMDIMain : Form, IMainForm
    {
        #region Constructors

        public frmMDIMain()
        {
            InitializeComponent();

            Main.DrawingsForm = new frmDrawings();
            Main.DrawingsForm.MdiParent = this;
            Main.DrawingsForm.Show();
            Main.MainForm = null;
            Main.MDIForm = this;

        }

        #endregion

        #region Event Handlers

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.DoExit();
        }

        private void iServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.DoSetup();

            Main.LoadDependencyData(Properties.Settings.Default.iServer_ProductionDocumentRootID);
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

        #endregion

        #region Fields

        #endregion

        #region Interface Methods

        public void DisableGUI()
        {
            toolStripButtonLoadiServerProduction.Enabled = false;
        }

        public void EnableDependencyMapToolstrip()
        {
            DependencyMapToolStripMenuItem.Visible = true;
        }

        public void EnableGUI()
        {
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

            tmrUpdates.Enabled = true;
        }

        public void EnableSystemCenterToolStrip()
        {
            retrieveSCOMServerHealthToolStripMenuItem.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
            toolStripButtonGetServerHealth.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
        }

        public void Reset()
        {
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout AboutForm = new frmAbout();

            AboutForm.Show(this);
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

        private void DependencyMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.CreateOrSelectDependencyMapForm(Main.BusinessApplicationRunbook, Properties.Settings.Default.ShowServersOnFullDependencyMap, null, LayoutMethod.MDS, true); // full map
        }

        private void errorCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessageCodes ErrorCodes = new frmMessageCodes();

            ErrorCodes.Show(this);
            ErrorCodes.Activate();
        }

        private void findSystemCenterComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSystemCenterQuery SystemCenterQuery = new frmSystemCenterQuery(Main.SCOMServerName, Main.SCSMServerName);

            SystemCenterQuery.Show();
        }

        private void frmMDIMain_Load(object sender, EventArgs e)
        {
            Main.Load();
        }

        private void frmMDIMain_Resize(object sender, EventArgs e)
        {
            
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

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.DoToolsOptions();
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

        private void sRMRunbookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.ViewSRMRunbook();
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            Main.DoExit();
        }

        private void toolStripButtonGetServerHealth_Click(object sender, EventArgs e)
        {
            GetServerHealth();
        }

        private void toolStripButtonLoadiServerProduction_Click(object sender, EventArgs e)
        {
            Main.DoSetup();

            Main.LoadDependencyData(Properties.Settings.Default.iServer_ProductionDocumentRootID);
        }

        private void frmMDIMain_Shown(object sender, EventArgs e)
        {
            //if (!ISA.Helper.Methods.IsOnScreen(this))
            //{
            //    this.Location = new Point(10, 10);
            //}
        }
    }
}
