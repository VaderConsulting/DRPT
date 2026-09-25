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
    public partial class frmOptions : Form
    {
        private Form _ParentForm = null;
        private bool _LoadComplete = false;
        private List<BusinessApplication> _Services = new List<BusinessApplication>();
        private List<Server> _Servers = new List<Server>();

        public frmOptions()
        {
            InitializeComponent();
        }

        public frmOptions(IMainForm ParentForm, List<BusinessApplication> Services, List<Server> Servers)
        {
            InitializeComponent();

            _Services = Services;
            _Servers = Servers;
        }

        #region Event Handlers

        private void frmOptions_Load(object sender, EventArgs e)
        {
            LoadOptionSettings();

            _LoadComplete = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveOptionSettings();

            this.Close();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            string LocalAppDataFolderName = Environment.ExpandEnvironmentVariables("%localappdata%" + @"\Vader_Consulting");
            //frmMain Parent = (frmMain)_ParentForm;

            System.IO.Directory.Delete(LocalAppDataFolderName, true);

            Properties.Settings.Default.Reload();

            Properties.Settings.Default.Reset();

            Main.LoadSettings(txtSettingsFilename.Text);

            Properties.Settings.Default.Save();

            LoadOptionSettings();
        }

        private void btnBrowseFilename_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Browser = new FolderBrowserDialog();

            Browser.SelectedPath = System.IO.Path.GetDirectoryName(Properties.Settings.Default.ExportFolderName);
            Browser.Description = "Select the folder where export files are to be saved";
            DialogResult Result = Browser.ShowDialog();

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.ExportFolderName = Browser.SelectedPath;
                txtExportFolderName.Text = Browser.SelectedPath;
            }
        }

        private void chkShowPossibleServerLocations_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSetDrawingsQuery_Click(object sender, EventArgs e)
        {
            frmOptionSetting OptionValueForm = new frmOptionSetting(txtDrawingsQuery.Text, "Get Drawings");

            DialogResult Result = OptionValueForm.ShowDialog();

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                txtDrawingsQuery.Text = OptionValueForm.ResultantValue;
            }

            OptionValueForm = null;
        }

        private void btnSetFocusedServicesQuery_Click(object sender, EventArgs e)
        {
            frmOptionSetting OptionValueForm = new frmOptionSetting(txtFocusedServiceQuery.Text, "Get Focused Service");

            DialogResult Result = OptionValueForm.ShowDialog();

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                txtFocusedServiceQuery.Text = OptionValueForm.ResultantValue;
            }

            OptionValueForm = null;
        }

        private void btnSetRelationshipsQuery_Click(object sender, EventArgs e)
        {
            frmOptionSetting OptionValueForm = new frmOptionSetting(txtRelationshipsQuery.Text, "Get Relationships");

            DialogResult Result = OptionValueForm.ShowDialog();

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                txtRelationshipsQuery.Text = OptionValueForm.ResultantValue;
            }

            OptionValueForm = null;
        }

        private void btnSetBusinessApplicationsDetail_Click(object sender, EventArgs e)
        {
            frmOptionSetting OptionValueForm = new frmOptionSetting(txtBusinessApplicationsDetailQuery.Text, "Get Business Application Details");

            DialogResult Result = OptionValueForm.ShowDialog();

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                txtBusinessApplicationsDetailQuery.Text = OptionValueForm.ResultantValue;
            }

            OptionValueForm = null;
        }

        //private void btnSetPhysicalSiteQuery_Click(object sender, EventArgs e)
        //{
        //    frmOptionSetting OptionValueForm = new frmOptionSetting(txtPhysicalSiteQuery.Text, "Get Server Physical Site");

        //    DialogResult Result = OptionValueForm.ShowDialog();

        //    if (Result == System.Windows.Forms.DialogResult.OK)
        //    {
        //        txtPhysicalSiteQuery.Text = OptionValueForm.ResultantValue;
        //    }

        //    OptionValueForm = null;
        //}

        private void btnSetServerDetailsQuery_Click(object sender, EventArgs e)
        {
            frmOptionSetting OptionValueForm = new frmOptionSetting(txtServerDetailsQuery.Text, "Get Server Details");

            DialogResult Result = OptionValueForm.ShowDialog();

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                txtServerDetailsQuery.Text = OptionValueForm.ResultantValue;
            }

            OptionValueForm = null;
        }

        private void chkConnectProductionUponStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (!_LoadComplete)
                return;

            if (chkConnectProductionUponStartup.Checked)
                chkConnectDevelopmentUponStartup.Checked = false;
        }

        private void chkConnectDevelopmentUponStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (!_LoadComplete)
                return;

            if (chkConnectDevelopmentUponStartup.Checked)
                chkConnectProductionUponStartup.Checked = false;
        }

        private void btnvCenterInformationQuery_Click(object sender, EventArgs e)
        {
            frmOptionSetting OptionValueForm = new frmOptionSetting(txtvCenterInformationQuery.Text, "vCenter Information Query");

            DialogResult Result = OptionValueForm.ShowDialog();

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                txtvCenterInformationQuery.Text = OptionValueForm.ResultantValue;
            }

            OptionValueForm = null;
        }

        private void btnvCenterStorageQuery_Click(object sender, EventArgs e)
        {
            frmOptionSetting OptionValueForm = new frmOptionSetting(txtvCenterStorageQuery.Text, "vCenter Storage Query");

            DialogResult Result = OptionValueForm.ShowDialog();

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                txtvCenterStorageQuery.Text = OptionValueForm.ResultantValue;
            }

            OptionValueForm = null;
        }

        #region Stream Radio Buttons

        private void rbStreamATimeBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamATimeBased.Checked)
            {
                txtStreamATime.Enabled = true;
                nudStreamAStorage.Enabled = false;
                txtStreamATAndSMinutes.Enabled = false;
                nudStreamATAndS.Enabled = false;
            }
        }

        private void rbStreamBTimeBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamBTimeBased.Checked)
            {
                txtStreamBTime.Enabled = true;
                nudStreamBStorage.Enabled = false;
                txtStreamBTAndSMinutes.Enabled = false;
                nudStreamBTAndS.Enabled = false;
            }
        }

        private void rbStreamCTimeBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamCTimeBased.Checked)
            {
                txtStreamCTime.Enabled = true;
                nudStreamCStorage.Enabled = false;
                txtStreamCTAndSMinutes.Enabled = false;
                nudStreamCTAndS.Enabled = false;
            }
        }

        private void rbStreamDTimeBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamDTimeBased.Checked)
            {
                txtStreamDTime.Enabled = true;
                nudStreamDStorage.Enabled = false;
                txtStreamDTAndSMinutes.Enabled = false;
                nudStreamDTAndS.Enabled = false;
            }
        }

        private void rbStreamETimeBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamETimeBased.Checked)
            {
                txtStreamETime.Enabled = true;
                nudStreamEStorage.Enabled = false;
                txtStreamETAndSMinutes.Enabled = false;
                nudStreamETAndS.Enabled = false;
            }
        }

        private void rbStreamAStorageBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamAStorageBased.Checked)
            {
                txtStreamATime.Enabled = false;
                nudStreamAStorage.Enabled = true;
                txtStreamATAndSMinutes.Enabled = false;
                nudStreamATAndS.Enabled = false;
            }
        }

        private void rbStreamBStorageBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamBStorageBased.Checked)
            {
                txtStreamBTime.Enabled = false;
                nudStreamBStorage.Enabled = true;
                txtStreamBTAndSMinutes.Enabled = false;
                nudStreamBTAndS.Enabled = false;
            }
        }

        private void rbStreamCStorageBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamCStorageBased.Checked)
            {
                txtStreamCTime.Enabled = false;
                nudStreamCStorage.Enabled = true;
                txtStreamCTAndSMinutes.Enabled = false;
                nudStreamCTAndS.Enabled = false;
            }
        }

        private void rbStreamDStorageBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamDStorageBased.Checked)
            {
                txtStreamDTime.Enabled = false;
                nudStreamDStorage.Enabled = true;
                txtStreamDTAndSMinutes.Enabled = false;
                nudStreamDTAndS.Enabled = false;
            }
        }

        private void rbStreamEStorageBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamEStorageBased.Checked)
            {
                txtStreamETime.Enabled = false;
                nudStreamEStorage.Enabled = true;
                txtStreamETAndSMinutes.Enabled = false;
                nudStreamETAndS.Enabled = false;
            }
        }

        private void rbStreamATimeAndStorageBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamATimeAndStorageBased.Checked)
            {
                txtStreamATime.Enabled = false;
                nudStreamAStorage.Enabled = false;
                txtStreamATAndSMinutes.Enabled = true;
                nudStreamATAndS.Enabled = true;
            }
        }

        private void rbStreamBTimeAndStorageBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamBTimeAndStorageBased.Checked)
            {
                txtStreamBTime.Enabled = false;
                nudStreamBStorage.Enabled = false;
                txtStreamBTAndSMinutes.Enabled = true;
                nudStreamBTAndS.Enabled = true;
            }
        }

        private void rbStreamCTimeAndStorageBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamCTimeAndStorageBased.Checked)
            {
                txtStreamCTime.Enabled = false;
                nudStreamCStorage.Enabled = false;
                txtStreamCTAndSMinutes.Enabled = true;
                nudStreamCTAndS.Enabled = true;
            }
        }

        private void rbStreamDTimeAndStorageBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamDTimeAndStorageBased.Checked)
            {
                txtStreamDTime.Enabled = false;
                nudStreamDStorage.Enabled = false;
                txtStreamDTAndSMinutes.Enabled = true;
                nudStreamDTAndS.Enabled = true;
            }
        }

        private void rbStreamETimeAndStorageBased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStreamETimeAndStorageBased.Checked)
            {
                txtStreamETime.Enabled = false;
                nudStreamEStorage.Enabled = false;
                txtStreamETAndSMinutes.Enabled = true;
                nudStreamETAndS.Enabled = true;
            }
        }

        #endregion

        private void chkEnablevCenterQueries_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnablevCenterQueries.Checked)
            {
                tabOptions.TabPages[3].Enabled = true;
            }
            else
            {
                tabOptions.TabPages[3].Enabled = false;
            }
        }

        private void chkEnableSystemCenterFunctions_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableSystemCenterFunctions.Checked)
            {
                tabOptions.TabPages[2].Enabled = true;
            }
            else
            {
                tabOptions.TabPages[2].Enabled = false;
            }
        }

        private void frmOptions_Shown(object sender, EventArgs e)
        {
            //Set tab pages as per config
            if (Properties.Settings.Default.EnableSystemCenterFunctions)
            {
                tabOptions.TabPages[2].Enabled = true;
            }
            else
            {
                tabOptions.TabPages[2].Enabled = false;
            }

            if (Properties.Settings.Default.EnablevCenterQueries)
            {
                tabOptions.TabPages[3].Enabled = true;
            }
            else
            {
                tabOptions.TabPages[3].Enabled = false;
            }
        }

        private void cmbiServerVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbiServerVersion.SelectedIndex)
            {
                case 0: // 23307
                    txtDrawingsQuery.Text = Properties.Settings.Default.iServer23307_GetDrawingsQuery;
                    txtFocusedServiceQuery.Text = Properties.Settings.Default.iServer23307_GetFocusedBusinessApplicationFromDrawingQuery;
                    txtRelationshipsQuery.Text = Properties.Settings.Default.iServer23307_RelationshipsQuery;
                    txtBusinessApplicationsDetailQuery.Text = Properties.Settings.Default.iServer23307_GetBusinessApplicationDetailsQuery;
                    txtServerDetailsQuery.Text = Properties.Settings.Default.iServer23307_GetServerDetailsQuery;
                    break;
                case 1: // 33400
                    txtDrawingsQuery.Text = Properties.Settings.Default.iServer33400_GetDrawingsQuery;
                    txtFocusedServiceQuery.Text = Properties.Settings.Default.iServer33400_GetFocusedBusinessApplicationFromDrawingQuery;
                    txtRelationshipsQuery.Text = Properties.Settings.Default.iServer33400_RelationshipsQuery;
                    txtBusinessApplicationsDetailQuery.Text = Properties.Settings.Default.iServer33400_GetBusinessApplicationDetailsQuery;
                    txtServerDetailsQuery.Text = Properties.Settings.Default.iServer33400_GetServerDetailsQuery;
                    break;
            }
        }

        private void chkShowExperimentalFeatures_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowExperimentalFeatures.Checked)
            {
                tabOptions.TabPages[6].Enabled = true;
            }
            else
            {
                tabOptions.TabPages[6].Enabled = false;
            }
        }

        private void btnDisablePDC_Click(object sender, EventArgs e)
        {
            foreach (BusinessApplication Service in _Services)
            {
                foreach (Server Server in Service.ComponentServers)
                {
                    //if (Server.PhysicalSiteName == "PDC")
                    if (Server.PhysicalSite.Name == "PDC")
                    {
                        Server.SystemStateIsEmulated = true;
                        Server.SetSystemHealthState(Global.HealthState.Error, "Site Failure");
                    }
                }
            }

            Main.DetermineAllBusinessApplicationState();

        }

        private void btnSetHARelationshipsQuery_Click(object sender, EventArgs e)
        {
            frmOptionSetting OptionValueForm = new frmOptionSetting(txtHARelationshipsQuery.Text, "Get HA Relationships");

            DialogResult Result = OptionValueForm.ShowDialog();

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                txtHARelationshipsQuery.Text = OptionValueForm.ResultantValue;
            }

            OptionValueForm = null;
        }

        private void btnGetInstance_Click(object sender, EventArgs e)
        {
            ISA.SystemCenter.ServerUtilities Utilities = new ISA.SystemCenter.ServerUtilities(Properties.Settings.Default.SystemCenter_SCOMServer, Properties.Settings.Default.SystemCenter_SCSMServer);

            //Utilities.GetObjectByBName("AD (DTF)", "System.Service");
            //Utilities.GetObjectByName("IR12228", "System.WorkItem.Incident");
            Utilities.GetObjectByName("IR12228");
        }

        private void btnCIQuery_Click(object sender, EventArgs e)
        {
            ISA.SystemCenter.ServerUtilities SCOMUtilities = new ISA.SystemCenter.ServerUtilities(Properties.Settings.Default.SystemCenter_SCOMServer, Properties.Settings.Default.SystemCenter_SCSMServer);
            string ServerResult = "";

            foreach (Server Server in _Servers)
            {
                string ServerID = SCOMUtilities.GetServerIDFromName(Server.Name);

                if (ServerID != "")
                {
                    ServerResult = Server.Name + ": Found Computer with this ID: (" + ServerID + ")";
                }
                else
                {
                    ServerResult = Server.Name + ": No Computer with this name";
                }

                Debug.WriteLine(ServerResult, "information");
            }
        }

        private void btnResetWindowLocations_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.frmBusinessApplicationImageLocation = new Point(0, 0);
            Properties.Settings.Default.frmBusinessApplicationLocation = new Point(0, 0);
            Properties.Settings.Default.frmDependencyMapLocation = new Point(0, 0);
            Properties.Settings.Default.frmInformationLocation = new Point(0, 0);
            Properties.Settings.Default.frmListLocation = new Point(0, 0);
            Properties.Settings.Default.frmProgressLocation = new Point(0, 0);
            ISA.Helper.Properties.frmProgressLocation = new Point(0, 0);
            Properties.Settings.Default.frmServerLocation = new Point(0, 0);
            Properties.Settings.Default.frmValidationRulesLocation = new Point(0, 0);
            Properties.Settings.Default.Save();

            // Stop form windows from saving their location for the remainder of this session...
            ISA.Helper.Properties.AllowSaveFormLocations = false;

            MessageBox.Show("To allow the Window locations to be reset, you will have to close and re-open the application", "Attention", MessageBoxButtons.OK);
        }

        #endregion

        private void LoadOptionSettings()
        {
            System.Array colorsArray = Enum.GetValues(typeof(KnownColor));
            KnownColor[] allColors = new KnownColor[colorsArray.Length];

            #region Colours
            Array.Copy(colorsArray, allColors, colorsArray.Length);

            foreach (KnownColor Colour in allColors)
            {
                ISA.Helper.ComboBoxItem Item = new ISA.Helper.ComboBoxItem(Colour.ToString());

                cmbServerColours.Items.Add(Item);
                cmbServiceColours.Items.Add(Item);
            }

            cmbServerColours.Refresh();
            cmbServiceColours.Refresh();

            #endregion

            #region Database versions

            cmbiServerVersion.Items.Add("233.07");
            cmbiServerVersion.Items.Add("334.00");
            //cmbiServerVersion.Items.Add("New");

            switch (Properties.Settings.Default.iServer_DatabaseVersion)
            {
                default:
                case "233.07":
                    ISA.Helper.Methods.SetComboBoxToTextIndex(cmbiServerVersion, "233.07");
                    break;
                case "334.00":
                    ISA.Helper.Methods.SetComboBoxToTextIndex(cmbiServerVersion, "334.00");
                    break;
            }

            #endregion

            string ServerBackgroundColour = Properties.Settings.Default.ServerBackgroundColour.ToString();
            string ServiceBackgroundColour = Properties.Settings.Default.BusinessApplicationBackgroundColour.ToString();


            if (_Services != null)
            {
                #region Stream prerequisites

                cmbStreamAServicePrerequisite.Enabled = true;
                cmbStreamBServicePrerequisite.Enabled = true;
                cmbStreamCServicePrerequisite.Enabled = true;
                cmbStreamDServicePrerequisite.Enabled = true;
                cmbStreamEServicePrerequisite.Enabled = true;

                ISA.Helper.ComboBoxItem Item = null;

                foreach (BusinessApplication Service in _Services)
                {
                    Item = new ISA.Helper.ComboBoxItem(Service.Name);

                    cmbStreamAServicePrerequisite.Items.Add(Item);
                    cmbStreamBServicePrerequisite.Items.Add(Item);
                    cmbStreamCServicePrerequisite.Items.Add(Item);
                    cmbStreamDServicePrerequisite.Items.Add(Item);
                    cmbStreamEServicePrerequisite.Items.Add(Item);
                }

                // Add the ability to select No Service prerequisite
                Item = new ISA.Helper.ComboBoxItem("[None]");

                cmbStreamAServicePrerequisite.Items.Add(Item);
                cmbStreamBServicePrerequisite.Items.Add(Item);
                cmbStreamCServicePrerequisite.Items.Add(Item);
                cmbStreamDServicePrerequisite.Items.Add(Item);
                cmbStreamEServicePrerequisite.Items.Add(Item);

                #endregion

                #region Experimental Sites

                #endregion
            }
            else
            {
                ISA.Helper.ComboBoxItem Item = new ISA.Helper.ComboBoxItem("[Drawings not loaded]");

                #region Stream prerequisites

                cmbStreamAServicePrerequisite.Items.Add(Item);
                cmbStreamBServicePrerequisite.Items.Add(Item);
                cmbStreamCServicePrerequisite.Items.Add(Item);
                cmbStreamDServicePrerequisite.Items.Add(Item);
                cmbStreamEServicePrerequisite.Items.Add(Item);

                cmbStreamAServicePrerequisite.SelectedIndex = 0;
                cmbStreamBServicePrerequisite.SelectedIndex = 0;
                cmbStreamCServicePrerequisite.SelectedIndex = 0;
                cmbStreamDServicePrerequisite.SelectedIndex = 0;
                cmbStreamEServicePrerequisite.SelectedIndex = 0;

                #endregion

                #region Experimental Sites

                #endregion
            }

            Hints.AutoPopDelay = Properties.Settings.Default.HelpHintTimemS;

            #region General

            nudHintTime.Text = Convert.ToInt16(Properties.Settings.Default.HelpHintTimemS / 1000).ToString();
            nudSQLQueryTimeout.Text = Convert.ToInt16(Properties.Settings.Default.SQLServerQueryTimeout).ToString();
            chkShowDrawingStatusOnService.Checked = Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication;
            chkConnectProductionUponStartup.Checked = Properties.Settings.Default.ConnectToProductionSQLUponStartup;
            chkConnectDevelopmentUponStartup.Checked = Properties.Settings.Default.ConnectToDevelopmentSQLUponStartup;
            chkShowDrawingLessServices.Checked = Properties.Settings.Default.ShowDrawingLessBusinessApplications;
            txtExportFolderName.Text = Properties.Settings.Default.ExportFolderName;
            txtExportFilename.Text = Properties.Settings.Default.ExportFilename;
            txtSettingsFilename.Text = Properties.Settings.Default.SettingsFilename;
            txtLocalDBConnectionString.Text = Properties.Settings.Default.LocalDBConnectionString;
            chkShowPossibleServerLocations.Checked = Properties.Settings.Default.ShowPossibleServerLocations;
            chkShowPossibleServiceLocations.Checked = Properties.Settings.Default.ShowPossibleServiceLocations;
            chkShowInScopeServicesOnly.Checked = Properties.Settings.Default.ShowInScopeServicesOnly;
            chkEnableP2PFeatures.Checked = Properties.Settings.Default.P2PEnabled;
            chkShowDebugInformation.Checked = Properties.Settings.Default.ShowDebugInformation;
            chkShowExperimentalFeatures.Checked = Properties.Settings.Default.ShowExperimentalFeatures;
            chkMDIInterface.Checked = Properties.Settings.Default.General_MDIInterface;

            #endregion

            #region Comboboxes...

            ISA.Helper.Methods.SetComboBoxToTextIndex(cmbServerColours, ServerBackgroundColour);
            ISA.Helper.Methods.SetComboBoxToTextIndex(cmbServiceColours, ServiceBackgroundColour);
            ISA.Helper.Methods.SetComboBoxToTextIndex(cmbStreamAServicePrerequisite, Properties.Settings.Default.Restore_StreamAServicePrerequisite);
            ISA.Helper.Methods.SetComboBoxToTextIndex(cmbStreamBServicePrerequisite, Properties.Settings.Default.Restore_StreamBServicePrerequisite);
            ISA.Helper.Methods.SetComboBoxToTextIndex(cmbStreamCServicePrerequisite, Properties.Settings.Default.Restore_StreamCServicePrerequisite);
            ISA.Helper.Methods.SetComboBoxToTextIndex(cmbStreamDServicePrerequisite, Properties.Settings.Default.Restore_StreamDServicePrerequisite);
            ISA.Helper.Methods.SetComboBoxToTextIndex(cmbStreamEServicePrerequisite, Properties.Settings.Default.Restore_StreamEServicePrerequisite);

            chkShowCheckboxesOnServiceList.Checked = Properties.Settings.Default.ShowCheckboxesOnServiceList;
            chkShowDrawingStatusColumn.Checked = Properties.Settings.Default.ShowDrawingStatusColumn;
            chkEnablevCenterQueries.Checked = Properties.Settings.Default.EnablevCenterQueries;
            chkEnableSystemCenterFunctions.Checked = Properties.Settings.Default.EnableSystemCenterFunctions;
            chkShowStreamDependenciesAsComponents.Checked = Properties.Settings.Default.ViewStreamDependenciesAsComponents;

            #endregion

            #region iServer

            txtiServerConnection.Text = Properties.Settings.Default.iServer_DatabaseConnectionString;
            txtiServerProductionRoot.Text = Properties.Settings.Default.iServer_ProductionDocumentRootID;
            txtiServerDevelopmentRoot.Text = Properties.Settings.Default.iServer_DevelopmentDocumentRootID;

            // Get iServer values per iServer version
            switch (Properties.Settings.Default.iServer_DatabaseVersion)
            {
                case "233.07":
                    txtDrawingsQuery.Text = Properties.Settings.Default.iServer23307_GetDrawingsQuery;
                    txtFocusedServiceQuery.Text = Properties.Settings.Default.iServer23307_GetFocusedBusinessApplicationFromDrawingQuery;
                    txtRelationshipsQuery.Text = Properties.Settings.Default.iServer23307_RelationshipsQuery;
                    txtBusinessApplicationsDetailQuery.Text = Properties.Settings.Default.iServer23307_GetBusinessApplicationDetailsQuery;
                    //txtPhysicalSiteQuery.Text = Properties.Settings.Default.iServer23307_GetServerDetailsQuery;
                    txtServerDetailsQuery.Text = Properties.Settings.Default.iServer23307_GetServerDetailsQuery;
                    txtHARelationshipsQuery.Text = Properties.Settings.Default.iServer23307_GetHARelationshipsQuery;
                    break;
                case "334.00":
                    txtDrawingsQuery.Text = Properties.Settings.Default.iServer33400_GetDrawingsQuery;
                    txtFocusedServiceQuery.Text = Properties.Settings.Default.iServer33400_GetFocusedBusinessApplicationFromDrawingQuery;
                    txtRelationshipsQuery.Text = Properties.Settings.Default.iServer33400_RelationshipsQuery;
                    txtBusinessApplicationsDetailQuery.Text = Properties.Settings.Default.iServer33400_GetBusinessApplicationDetailsQuery;
                    //txtPhysicalSiteQuery.Text = Properties.Settings.Default.iServer33400_GetServerDetailsQuery;
                    txtServerDetailsQuery.Text = Properties.Settings.Default.iServer33400_GetServerDetailsQuery;
                    txtHARelationshipsQuery.Text = Properties.Settings.Default.iServer33400_GetHARelationshipsQuery;
                    break;
            }

            txtBusinessApplicationID.Text = Properties.Settings.Default.iServer_BusinessApplicationID;
            txtServerID.Text = Properties.Settings.Default.iServer_ServerID;
            txtDrawingID.Text = Properties.Settings.Default.iServer_DrawingID;
            txtStreamID.Text = Properties.Settings.Default.iServer_StreamAttributeID;
            txtTierID.Text = Properties.Settings.Default.iServer_TierAttributeID;
            txtPhysicalSiteID.Text = Properties.Settings.Default.iServer_PhysicalSiteAttributeID;
            txtRunbookOrderID.Text = Properties.Settings.Default.iServer_RunbookOrderAttributeID;
            txtServiceDisplayNameID.Text = Properties.Settings.Default.iServer_ServiceDisplayNameAttributeID;
            txtShowInServiceCatalogID.Text = Properties.Settings.Default.iServer_ShowInServiceCatalogAttributeID;
            txtSubServiceID.Text = Properties.Settings.Default.iServer_SubServiceAttributeID;
            txtMandatoryID.Text = Properties.Settings.Default.iServer_MandatoryAttributeID;
            txtFocusedServiceID.Text = Properties.Settings.Default.iServer_FocusedServiceAttributeID;
            txtDrawingStatusID.Text = Properties.Settings.Default.iServer_DrawingStatusAttributeID;
            txtInScopeAttributeID.Text = Properties.Settings.Default.iServer_InScopeAttributeID;
            txtLastUpdateAttributeID.Text = Properties.Settings.Default.iServer_LastUpdateAttributeID;
            txtHighlyAvailableAttributeID.Text = Properties.Settings.Default.iServer_HighlyAvailableAttributeID;
            ISA.Helper.Methods.SetComboBoxToTextIndex(cmbiServerVersion, Properties.Settings.Default.iServer_DatabaseVersion);
            txtVirtualAttributeID.Text = Properties.Settings.Default.iServer_VirtualAttributeID;
            txtHAGroupNameAttributeID.Text = Properties.Settings.Default.iServer_HAGroupNameAttributeID;
            txtAddToManagementPackAttributeID.Text = Properties.Settings.Default.iServer_AddToMPAttributeID;

            #endregion

            #region System Center

            txtPRDSCOMServer.Text = Properties.Settings.Default.SystemCenter_SCOMServer;
            txtPRDSCSMServer.Text = Properties.Settings.Default.SystemCenter_SCSMServer;
            txtTSTSCOMServer.Text = Properties.Settings.Default.SystemCenter_TestSCOMServer;
            txtTSTSCSMServer.Text = Properties.Settings.Default.SystemCenter_TestSCSMServer;
            txtDEVSCOMServer.Text = Properties.Settings.Default.SystemCenter_DevSCOMServer;
            txtDEVSCSMServer.Text = Properties.Settings.Default.SystemCenter_DevSCSMServer;
            txtNamePrefix.Text = Properties.Settings.Default.SystemCenter_ManagementPackIDPrefix;
            txtFriendlyName.Text = Properties.Settings.Default.SystemCenter_ManagementPackFriendlyName;
            txtMPFileSaveFolderName.Text = Properties.Settings.Default.SystemCenter_ManagementPackSaveFolderName;
            txtMPViewFolderName.Text = Properties.Settings.Default.SystemCenter_ManagementPackViewFolderName;
            txtDescription.Text = Properties.Settings.Default.SystemCenter_ManagementPackDescription;
            txtPowershellModulePath.Text = Properties.Settings.Default.PowershellModulePath;
            txtFriendlyNamePrefix.Text = Properties.Settings.Default.SystemCenter_DistributedApplicationFriendlyNamePrefix;
            txtSealCommandLine.Text = Properties.Settings.Default.SystemCenter_ManagementPackSealCommandline;
            txtKeyfileFilename.Text = Properties.Settings.Default.KeyfileFilename;
            txtCompanyName.Text = Properties.Settings.Default.CompanyName;
            chkSealManagementPacks.Checked = Properties.Settings.Default.SystemCenter_SealManagementPacks;
            chkImportSCOMManagementPacks.Checked = Properties.Settings.Default.ImportSCOMManagementPacks;
            chkImportSCSMManagementPacks.Checked = Properties.Settings.Default.ImportSCSMManagementPacks;
            chkIgnoreManagementPackCreationErrors.Checked = Properties.Settings.Default.IgnoreManagementPackCreationErrors;
            chkAlwaysCreateManagementPacks.Checked = Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus;

            #endregion

            #region vCenter

            txtvCenterPrimaryConnection.Text = Properties.Settings.Default.vCenter_PrimaryDatabaseConnection;
            txtvCenterSecondaryConnection.Text = Properties.Settings.Default.vCenter_SecondaryDatabaseConnection;
            txtPrimaryvCenterSiteName.Text = Properties.Settings.Default.PrimaryvCenterSiteName;
            txtSecondaryvCenterSiteName.Text = Properties.Settings.Default.SecondaryvCenterSiteName;
            txtvCenterInformationQuery.Text = Properties.Settings.Default.vCenter_InformationQuery;
            txtvCenterStorageQuery.Text = Properties.Settings.Default.vCenter_StorageQuery;

            #endregion

            #region Restore

            #region Stream A

            txtStreamATime.Text = Properties.Settings.Default.Restore_StreamARestoreTime1.ToString();
            nudStreamAStorage.Value = Properties.Settings.Default.Restore_StreamARestoreRate1;
            txtStreamATAndSMinutes.Text = Properties.Settings.Default.Restore_StreamARestoreTime2.ToString();
            nudStreamATAndS.Value = Properties.Settings.Default.Restore_StreamARestoreRate2;

            switch (Properties.Settings.Default.Restore_StreamAAvailabilityPreditionMethod)
            {
                case 0:
                    rbStreamATimeBased.Checked = true;
                    break;
                case 1:
                    rbStreamAStorageBased.Checked = true;
                    break;
                case 2:
                    rbStreamATimeAndStorageBased.Checked = true;
                    break;
            }

            txtStreamAIntervalPeriod.Text = Properties.Settings.Default.Restore_StreamAIntervalPeriod.ToString();
            txtStreamAOverheadTime.Text = Properties.Settings.Default.Restore_StreamAOverheadTime.ToString();

            #endregion

            #region Stream B

            txtStreamBTime.Text = Properties.Settings.Default.Restore_StreamBRestoreTime1.ToString();
            nudStreamBStorage.Value = Properties.Settings.Default.Restore_StreamBRestoreRate1;
            txtStreamBTAndSMinutes.Text = Properties.Settings.Default.Restore_StreamBRestoreTime2.ToString();
            nudStreamBTAndS.Value = Properties.Settings.Default.Restore_StreamBRestoreRate2;

            switch (Properties.Settings.Default.Restore_StreamBAvailabilityPreditionMethod)
            {
                case 0:
                    rbStreamBTimeBased.Checked = true;
                    break;
                case 1:
                    rbStreamBStorageBased.Checked = true;
                    break;
                case 2:
                    rbStreamBTimeAndStorageBased.Checked = true;
                    break;
            }

            txtStreamBIntervalPeriod.Text = Properties.Settings.Default.Restore_StreamBIntervalPeriod.ToString();
            txtStreamBOverheadTime.Text = Properties.Settings.Default.Restore_StreamBOverheadTime.ToString();

            #endregion

            #region Stream C

            txtStreamCTime.Text = Properties.Settings.Default.Restore_StreamCRestoreTime1.ToString();
            nudStreamCStorage.Value = Properties.Settings.Default.Restore_StreamCRestoreRate1;
            txtStreamCTAndSMinutes.Text = Properties.Settings.Default.Restore_StreamCRestoreTime2.ToString();
            nudStreamCTAndS.Value = Properties.Settings.Default.Restore_StreamCRestoreRate2;

            switch (Properties.Settings.Default.Restore_StreamCAvailabilityPreditionMethod)
            {
                case 0:
                    rbStreamCTimeBased.Checked = true;
                    break;
                case 1:
                    rbStreamCStorageBased.Checked = true;
                    break;
                case 2:
                    rbStreamCTimeAndStorageBased.Checked = true;
                    break;
            }

            txtStreamCIntervalPeriod.Text = Properties.Settings.Default.Restore_StreamCIntervalPeriod.ToString();
            txtStreamCOverheadTime.Text = Properties.Settings.Default.Restore_StreamCOverheadTime.ToString();

            #endregion

            #region Stream D

            txtStreamDTime.Text = Properties.Settings.Default.Restore_StreamDRestoreTime1.ToString();
            nudStreamDStorage.Value = Properties.Settings.Default.Restore_StreamDRestoreRate1;
            txtStreamDTAndSMinutes.Text = Properties.Settings.Default.Restore_StreamDRestoreTime2.ToString();
            nudStreamDTAndS.Value = Properties.Settings.Default.Restore_StreamDRestoreRate2;

            switch (Properties.Settings.Default.Restore_StreamDAvailabilityPreditionMethod)
            {
                case 0:
                    rbStreamDTimeBased.Checked = true;
                    break;
                case 1:
                    rbStreamDStorageBased.Checked = true;
                    break;
                case 2:
                    rbStreamDTimeAndStorageBased.Checked = true;
                    break;
            }

            txtStreamDIntervalPeriod.Text = Properties.Settings.Default.Restore_StreamDIntervalPeriod.ToString();
            txtStreamDOverheadTime.Text = Properties.Settings.Default.Restore_StreamDOverheadTime.ToString();

            #endregion

            #region Stream E

            txtStreamETime.Text = Properties.Settings.Default.Restore_StreamERestoreTime1.ToString();
            nudStreamEStorage.Value = Properties.Settings.Default.Restore_StreamERestoreRate1;
            txtStreamETAndSMinutes.Text = Properties.Settings.Default.Restore_StreamERestoreTime2.ToString();
            nudStreamETAndS.Value = Properties.Settings.Default.Restore_StreamERestoreRate2;

            switch (Properties.Settings.Default.Restore_StreamEAvailabilityPreditionMethod)
            {
                case 0:
                    rbStreamETimeBased.Checked = true;
                    break;
                case 1:
                    rbStreamEStorageBased.Checked = true;
                    break;
                case 2:
                    rbStreamETimeAndStorageBased.Checked = true;
                    break;
            }

            txtStreamEIntervalPeriod.Text = Properties.Settings.Default.Restore_StreamEIntervalPeriod.ToString();
            txtStreamEOverheadTime.Text = Properties.Settings.Default.Restore_StreamEOverheadTime.ToString();

            #endregion

            nudParallelRestoreCount.Value = Properties.Settings.Default.Restore_ParallelRestoreCount;
            chkIncludeOptionalServices.Checked = Properties.Settings.Default.Restore_IncludeOptionalServicesWhenCalculatingRestoreTimes;
            txtRestoreOverheadTime.Text = Properties.Settings.Default.Restore_OverallOverheadTime.ToString();

            #endregion

            #region Security

            chkRestrictByMembership.Checked = Properties.Settings.Default.RestrictFeaturesByGroupMembership;
            txtMPAuthorGroup.Text = Properties.Settings.Default.Security_SystemCenterMPAuthorGroup;
            txtiServerROGroup.Text = Properties.Settings.Default.Security_DependencyDatabaseReadGroup;
            txtiServerRWGroup.Text = Properties.Settings.Default.Security_DependencyDatabaseWriteGroup;
            txtvCenterReadGroup.Text = Properties.Settings.Default.Security_vCenterSQLReadGroup;

            #endregion

            #region Experimental Features

            chkShowServersOnDependencyMaps.Checked = Properties.Settings.Default.ShowServersOnFullDependencyMap;
            chkShowStatesOnDependencyMaps.Checked = Properties.Settings.Default.ShowStatesOnDependencyMaps;

            #endregion

            #region Tooltips

            // GUI
            Hints.SetToolTip(btnDefault, "Clicking this button will load the DR Tool default values, losing all customisations.");
            Hints.SetToolTip(btnOK, "Clicking this button will save any changes you have made to the current configuration.");
            Hints.SetToolTip(btnCancel, "Clicking this button will close this Form, losing any changes you may have made.");
            Hints.SetToolTip(btnBrowseFilename, "Clicking this button will allow you to browse for a folder to save the export files.");

            // General
            Hints.SetToolTip(nudHintTime, "This is the time (in seconds) that Help Hints are displayed when the mouse is hovered over a control.");
            Hints.SetToolTip(lblHelpHintTimemS, "This is the time (in seconds) that Help Hints are displayed when the mouse is hovered over a control.");
            Hints.SetToolTip(lblMPAuthorGroup, "This is the Active Directory Group Name that the User must be a Member of, to be allowed to create and import SCOM Management Packs.");
            Hints.SetToolTip(txtMPAuthorGroup, "This is the Active Directory Group Name that the User must be a Member of, to be allowed to create and import SCOM Management Packs.");
            Hints.SetToolTip(lblShowDrawingStatusOnService, "Placing a check here will show the iServer Drawing Status superimposed on the Service Status.\n\nValid Statuses:\nNot_Specified\nDraft\nTo_Be_Reviewed\nApproved");
            Hints.SetToolTip(chkShowDrawingStatusOnService, "Placing a check here will show the iServer Drawing Status superimposed on the Service Status.\n\nValid Statuses:\nNot_Specified\nDraft\nTo_Be_Reviewed\nApproved");
            Hints.SetToolTip(lblAlwaysCreateManagementPacks, "Placing a check here will force the creation of Management Packs regardless of the Drawing Status.\nIf unchecked, only Approved Drawings will be used to create Management Packs.\n\nValid Statuses:\nNot_Specified\nDraft\nTo_Be_Reviewed\nApproved");
            Hints.SetToolTip(chkAlwaysCreateManagementPacks, "Placing a check here will force the creation of Management Packs regardless of the Drawing Status.\nIf unchecked, only Approved Drawings will be used to create Management Packs.\n\nValid Statuses:\nNot_Specified\nDraft\nTo_Be_Reviewed\nApproved");
            Hints.SetToolTip(lblConnectProductionUponStartup, "Placing a check here will cause connection to the iServer Database (Production Document Root) upon Application Startup.");
            Hints.SetToolTip(chkConnectProductionUponStartup, "Placing a check here will cause connection to the iServer Database (Production Document Root) upon Application Startup.");
            Hints.SetToolTip(lblShowDrawingLessServices, "Placing a check here will force the display of Business Applications when there is no Drawing for that Business Application.");
            Hints.SetToolTip(chkShowDrawingLessServices, "Placing a check here will force the display of Business Applications when there is no Drawing for that Business Application.");
            Hints.SetToolTip(txtExportFolderName, "This is the folder where the export lists are saved.");
            Hints.SetToolTip(lblExportFolderName, "This is the folder where the export lists are saved.");
            Hints.SetToolTip(txtExportFilename, "This is the filename used when exporting lists.\nUsable Tags include:\n#LISTNAME#\n#DESKTOP#\n#MYDOCS#\n#HOUR#\n#MINUTE#\n#DAY#\n#MONTH#\n#YEAR#");
            Hints.SetToolTip(lblExportFilename, "This is the filename used when exporting lists.\nUsable Tags include:\n#LISTNAME#\n#DESKTOP#\n#MYDOCS#\n#HOUR#\n#MINUTE#\n#DAY#\n#MONTH#\n#YEAR#");
            Hints.SetToolTip(lblShowPossibleServerLocations, "Placing a check here will cause colouring of the possible locations where a Server may be moved, when viewing the Server Runbook list.");
            Hints.SetToolTip(chkShowPossibleServerLocations, "Placing a check here will cause colouring of the possible locations where a Server may be moved, when viewing the Server Runbook list.");
            Hints.SetToolTip(lblServerRunbookColour, "If the previous option is enabled, this is the colour that will be used in the Listview.");
            Hints.SetToolTip(cmbServerColours, "If the previous option is enabled, this is the colour that will be used in the Listview.");
            Hints.SetToolTip(lblEnableP2PFeatures, "Enable P2P features.  This will not work if your environment utilises Windows Firewall.\nNote: Net Framework 4.0.2 or above is also required.\n\nThis feature is currently not implemented.  Please contact the Author if you wish to have it added.");
            Hints.SetToolTip(chkEnableP2PFeatures, "Enable P2P features.  This will not work if your environment utilises Windows Firewall.\nNote: Net Framework 4.0.2 or above is also required.\n\nThis feature is currently not implemented.  Please contact the Author if you wish to have it added.");

            // iServer
            Hints.SetToolTip(txtiServerConnection, "This is the iServer Database Connection String.");
            Hints.SetToolTip(lblIServerConnection, "This is the iServer Database Connection String.");
            Hints.SetToolTip(txtDrawingsQuery, "This is the SQL Server Database query that retrieves the iServer Drawings.");
            Hints.SetToolTip(lblDrawingsQuery, "This is the SQL Server Database query that retrieves the iServer Drawings.");
            Hints.SetToolTip(txtiServerProductionRoot, "This is the value used to filter iServer Drawings to the Production scope.");
            Hints.SetToolTip(lbliServerProductionRoot, "This is the value used to filter iServer Drawings to the Production scope.");
            Hints.SetToolTip(txtiServerDevelopmentRoot, "This is the value used to filter iServer Drawings to the Development scope.");
            Hints.SetToolTip(lbliServerDevelopmentRoot, "This is the value used to filter iServer Drawings to the Development scope.");
            Hints.SetToolTip(txtFocusedServiceQuery, "This is the SQL Server Database query that retrieves the focused Service.");
            Hints.SetToolTip(lblFocusedServiceQuery, "This is the SQL Server Database query that retrieves the focused Service.");
            Hints.SetToolTip(txtRelationshipsQuery, "This is the SQL Server Database query that retrieves the iServer Relationships,\nand thus each Business Application Component (i.e. Servers and Business Applications), including the Stream and Tier of each Business Application.");
            Hints.SetToolTip(lblRelationshipsQuery, "This is the SQL Server Database query that retrieves the iServer Relationships,\nand thus each Business Application Component (i.e. Servers and Business Applications), including the Stream and Tier of each Business Application.");
            Hints.SetToolTip(txtBusinessApplicationsDetailQuery, "This is the SQL Server Database query that retrieves the Business Application details.");
            Hints.SetToolTip(lblBusinessApplicationsDetailQuery, "This is the SQL Server Database query that retrieves the Business Application details.");
            //Hints.SetToolTip(txtPhysicalSiteQuery, "This is the SQL Server Database query that retrieves the iServer assigned Site for each Server.");
            //Hints.SetToolTip(lblPhysicalSiteQuery, "This is the SQL Server Database query that retrieves the iServer assigned Site for each Server.");

            // System Center
            Hints.SetToolTip(txtPRDSCOMServer, "This is the name of the Connected SCOM Server.");
            Hints.SetToolTip(lblSCOMServer, "This is the name of the Connected SCOM Server.");
            Hints.SetToolTip(txtNamePrefix, "This is the prefix that is applied to each Management Pack name.");
            Hints.SetToolTip(lblNamePrefix, "This is the prefix that is applied to each Management Pack name.");
            Hints.SetToolTip(txtFriendlyName, "This is the format used to create the Management Pack friendly name.\n#SERVICENAME# will be replaced for each Business Application.");
            Hints.SetToolTip(lblFriendlyName, "This is the format used to create the Management Pack friendly name.\n#SERVICENAME# will be replaced for each Business Application.");
            Hints.SetToolTip(txtMPFileSaveFolderName, "This is the folder where each Management Pack is to be saved.\n#MYDOCS# will be replaced for your My Documents folder.");
            Hints.SetToolTip(lblFilename, "This is the folder where each Management Pack is to be saved.\n#MYDOCS# will be replaced for your My Documents folder.");
            Hints.SetToolTip(txtDescription, "This is the description applied to each Management Pack.\n#SERVICENAME# will be replaced for each Business Application Name.");
            Hints.SetToolTip(lblDescription, "This is the description applied to each Management Pack.\n#SERVICENAME# will be replaced for each Business Application Name.");
            //Hints.SetToolTip(txtClassName, "This is the ID used for each Distributed Application Class.\n#SERVICENAME# will be replaced for each Business Application Name.");
            //Hints.SetToolTip(lblClassName, "This is the ID used for each Distributed Application Class.\n#SERVICENAME# will be replaced for each Business Application Name.");
            //Hints.SetToolTip(txtComponentName, "This is the ID used within the Management Pack for each Distributed Application Component.\n#CLASSID# will be replaced for each Business Application from the Distributed Application Class ID.");
            //Hints.SetToolTip(lblComponentName, "This is the ID used within the Management Pack for each Distributed Application Component.\n#CLASSID# will be replaced for each Business Application from the Distributed Application Class ID.");
            //Hints.SetToolTip(txtComponentDisplayName, "This is the Name used within the Management Pack to describe each Distributed Application Component.\n#SERVICENAME# will be replaced for each Business Application.");
            //Hints.SetToolTip(lblComponentDisplayName, "This is the Name used within the Management Pack to describe each Distributed Application Component.\n#SERVICENAME# will be replaced for each Business Application.");
            Hints.SetToolTip(chkSealManagementPacks, "Placing a check here will cause each Management Pack to be Sealed after creation.\nSealed Management Packs are saved in the same location as the normal unsealed Management Packs, but with a .mp extension.\nManagement Packs must be sealed before they can be loaded into SCSM by the System Center Operations Manager CI Connector.");
            Hints.SetToolTip(lblSealManagementPacks, "Placing a check here will cause each Management Pack to be Sealed after creation.\nSealed Management Packs are saved in the same location as the normal unsealed Management Packs, but with a .mp extension.\nManagement Packs must be sealed before they can be loaded into SCSM by the System Center Operations Manager CI Connector.");
            Hints.SetToolTip(txtPowershellModulePath, "This is the path to the SCSM Powershell module.");
            Hints.SetToolTip(lblPowershellModulePath, "This is the path to the SCSM Powershell module.");
            Hints.SetToolTip(txtFriendlyNamePrefix, "This is the prefix that is applied to each Management Pack Friendly name, and is what is shown within the SCOM Management Packs tab.");
            Hints.SetToolTip(lblFriendlyNamePrefix, "This is the prefix that is applied to each Management Pack Friendly name, and is what is shown within the SCOM Management Packs tab.");
            Hints.SetToolTip(txtSealCommandLine, "This is the PowerShell command that is executed to seal each Management Pack.\n#MPFILENAME#, #KEYFILEFILENAME#, #KEYCOMPANYNAME# and #MPPATH# are all replaced at runtime.");
            Hints.SetToolTip(lblSealCommandLine, "This is the PowerShell command that is executed to seal each Management Pack.\n#MPFILENAME#, #KEYFILEFILENAME#, #KEYCOMPANYNAME# and #MPPATH# are all replaced at runtime.");
            //Hints.SetToolTip(txtReferencePath, "This is the path to the referenced Microsoft-provided System Center Management Packs.");
            //Hints.SetToolTip(lblReferencePath, "This is the path to the referenced Microsoft-provided System Center Management Packs.");
            //Hints.SetToolTip(txtServiceBaseClass, "This is the System Center Distributed Application base-class used when creating the Management Packs for each Service.");
            //Hints.SetToolTip(lblServiceBaseClass, "This is the System Center Distributed Application base-class used when creating the Management Packs for each Service.");
            Hints.SetToolTip(txtKeyfileFilename, "This is the full path to the .snk file required to seal each Management Pack.");
            Hints.SetToolTip(lblKeyfileFilename, "This is the full path to the .snk file required to seal each Management Pack.");
            Hints.SetToolTip(txtCompanyName, "This is the Company Name used to seal the Management Pack, and must be the same as when the .snk file was created.");
            Hints.SetToolTip(lblCompanyName, "This is the Company Name used to seal the Management Pack, and must be the same as when the .snk file was created.");
            Hints.SetToolTip(chkImportSCOMManagementPacks, "Placing a check here will cause each Management Pack to be imported after creation.");
            Hints.SetToolTip(lblImportSCOMManagementPacks, "Placing a check here will cause each Management Pack to be imported after creation.");

            #endregion

        }

        private void SaveOptionSettings()
        {

            #region General

            Properties.Settings.Default.HelpHintTimemS = Convert.ToInt16(nudHintTime.Text) * 1000;
            Properties.Settings.Default.SQLServerQueryTimeout = Convert.ToInt16(nudSQLQueryTimeout.Text);
            Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication = chkShowDrawingStatusOnService.Checked;
            Properties.Settings.Default.ConnectToProductionSQLUponStartup = chkConnectProductionUponStartup.Checked;
            Properties.Settings.Default.ConnectToDevelopmentSQLUponStartup = chkConnectDevelopmentUponStartup.Checked;
            Properties.Settings.Default.ShowDrawingLessBusinessApplications = chkShowDrawingLessServices.Checked;
            Properties.Settings.Default.ExportFolderName = txtExportFolderName.Text;
            Properties.Settings.Default.ExportFilename = txtExportFilename.Text;
            Properties.Settings.Default.ServerBackgroundColour = cmbServerColours.Text;
            Properties.Settings.Default.BusinessApplicationBackgroundColour = cmbServiceColours.Text;
            Properties.Settings.Default.ShowPossibleServerLocations = chkShowPossibleServerLocations.Checked;
            Properties.Settings.Default.ShowPossibleServiceLocations = chkShowPossibleServiceLocations.Checked;
            Properties.Settings.Default.ShowCheckboxesOnServiceList = chkShowCheckboxesOnServiceList.Checked;
            Properties.Settings.Default.ShowDrawingStatusColumn = chkShowDrawingStatusColumn.Checked;
            Properties.Settings.Default.EnablevCenterQueries = chkEnablevCenterQueries.Checked;
            Properties.Settings.Default.EnableSystemCenterFunctions = chkEnableSystemCenterFunctions.Checked;
            Properties.Settings.Default.ViewStreamDependenciesAsComponents = chkShowStreamDependenciesAsComponents.Checked;
            Properties.Settings.Default.ShowInScopeServicesOnly = chkShowInScopeServicesOnly.Checked;
            Properties.Settings.Default.P2PEnabled = chkEnableP2PFeatures.Checked;
            Properties.Settings.Default.ShowDebugInformation = chkShowDebugInformation.Checked;
            Properties.Settings.Default.ShowExperimentalFeatures = chkShowExperimentalFeatures.Checked;
            Properties.Settings.Default.SettingsFilename = txtSettingsFilename.Text;
            Properties.Settings.Default.LocalDBConnectionString = txtLocalDBConnectionString.Text;
            Properties.Settings.Default.General_MDIInterface = chkMDIInterface.Checked;

            #endregion

            #region iServer

            Properties.Settings.Default.iServer_DatabaseConnectionString = txtiServerConnection.Text;
            Properties.Settings.Default.iServer_ProductionDocumentRootID = txtiServerProductionRoot.Text;
            Properties.Settings.Default.iServer_DevelopmentDocumentRootID = txtiServerDevelopmentRoot.Text;

            switch (cmbiServerVersion.SelectedIndex)
            {
                case 0: // 23307
                    Properties.Settings.Default.iServer23307_GetDrawingsQuery = txtDrawingsQuery.Text;
                    Properties.Settings.Default.iServer23307_GetFocusedBusinessApplicationFromDrawingQuery = txtFocusedServiceQuery.Text;
                    Properties.Settings.Default.iServer23307_RelationshipsQuery = txtRelationshipsQuery.Text;
                    Properties.Settings.Default.iServer23307_GetBusinessApplicationDetailsQuery = txtBusinessApplicationsDetailQuery.Text;
                    Properties.Settings.Default.iServer23307_GetHARelationshipsQuery = txtHARelationshipsQuery.Text;
                    Properties.Settings.Default.iServer23307_GetServerDetailsQuery = txtServerDetailsQuery.Text;
                    Properties.Settings.Default.iServer_DatabaseVersion = "233.07";
                    break;
                case 1: // 33400
                    Properties.Settings.Default.iServer33400_GetDrawingsQuery = txtDrawingsQuery.Text;
                    Properties.Settings.Default.iServer33400_GetFocusedBusinessApplicationFromDrawingQuery = txtFocusedServiceQuery.Text;
                    Properties.Settings.Default.iServer33400_RelationshipsQuery = txtRelationshipsQuery.Text;
                    Properties.Settings.Default.iServer33400_GetBusinessApplicationDetailsQuery = txtBusinessApplicationsDetailQuery.Text;
                    //Properties.Settings.Default.iServer33400_GetServerDetailsQuery = txtPhysicalSiteQuery.Text;
                    Properties.Settings.Default.iServer33400_GetHARelationshipsQuery = txtHARelationshipsQuery.Text;
                    Properties.Settings.Default.iServer33400_GetServerDetailsQuery = txtServerDetailsQuery.Text;
                    Properties.Settings.Default.iServer_DatabaseVersion = "334.00";
                    break;
            }

            Properties.Settings.Default.iServer_BusinessApplicationID = txtBusinessApplicationID.Text;
            Properties.Settings.Default.iServer_ServerID = txtServerID.Text;
            Properties.Settings.Default.iServer_DrawingID = txtDrawingID.Text;
            Properties.Settings.Default.iServer_StreamAttributeID = txtStreamID.Text;
            Properties.Settings.Default.iServer_TierAttributeID = txtTierID.Text;
            Properties.Settings.Default.iServer_PhysicalSiteAttributeID = txtPhysicalSiteID.Text;
            Properties.Settings.Default.iServer_RunbookOrderAttributeID = txtRunbookOrderID.Text;
            Properties.Settings.Default.iServer_ServiceDisplayNameAttributeID = txtServiceDisplayNameID.Text;
            Properties.Settings.Default.iServer_ShowInServiceCatalogAttributeID = txtShowInServiceCatalogID.Text;
            Properties.Settings.Default.iServer_SubServiceAttributeID = txtSubServiceID.Text;
            Properties.Settings.Default.iServer_MandatoryAttributeID = txtMandatoryID.Text;
            Properties.Settings.Default.iServer_FocusedServiceAttributeID = txtFocusedServiceID.Text;
            Properties.Settings.Default.iServer_DrawingStatusAttributeID = txtDrawingStatusID.Text;
            Properties.Settings.Default.iServer_InScopeAttributeID = txtInScopeAttributeID.Text;
            Properties.Settings.Default.iServer_LastUpdateAttributeID = txtLastUpdateAttributeID.Text;
            Properties.Settings.Default.iServer_VirtualAttributeID = txtVirtualAttributeID.Text;
            Properties.Settings.Default.iServer_HighlyAvailableAttributeID = txtHighlyAvailableAttributeID.Text;
            Properties.Settings.Default.iServer_HAGroupNameAttributeID = txtHAGroupNameAttributeID.Text;
            Properties.Settings.Default.iServer_AddToMPAttributeID = txtAddToManagementPackAttributeID.Text;

            #endregion

            #region System Center

            Properties.Settings.Default.SystemCenter_SCOMServer = txtPRDSCOMServer.Text;
            Properties.Settings.Default.SystemCenter_SCSMServer = txtPRDSCSMServer.Text;
            Properties.Settings.Default.SystemCenter_TestSCOMServer = txtTSTSCOMServer.Text;
            Properties.Settings.Default.SystemCenter_TestSCSMServer = txtTSTSCSMServer.Text;
            Properties.Settings.Default.SystemCenter_DevSCOMServer = txtDEVSCOMServer.Text;
            Properties.Settings.Default.SystemCenter_DevSCSMServer = txtDEVSCSMServer.Text;
            Properties.Settings.Default.SystemCenter_ManagementPackIDPrefix = txtNamePrefix.Text;
            Properties.Settings.Default.SystemCenter_ManagementPackFriendlyName = txtFriendlyName.Text;
            Properties.Settings.Default.SystemCenter_ManagementPackSaveFolderName = txtMPFileSaveFolderName.Text;
            Properties.Settings.Default.SystemCenter_ManagementPackViewFolderName = txtMPViewFolderName.Text;
            Properties.Settings.Default.SystemCenter_ManagementPackDescription = txtDescription.Text;
            Properties.Settings.Default.PowershellModulePath = txtPowershellModulePath.Text;
            Properties.Settings.Default.SystemCenter_DistributedApplicationFriendlyNamePrefix = txtFriendlyNamePrefix.Text;
            Properties.Settings.Default.SystemCenter_ManagementPackSealCommandline = txtSealCommandLine.Text;
            Properties.Settings.Default.KeyfileFilename = txtKeyfileFilename.Text;
            Properties.Settings.Default.CompanyName = txtCompanyName.Text;
            Properties.Settings.Default.SystemCenter_SealManagementPacks = chkSealManagementPacks.Checked;
            Properties.Settings.Default.ImportSCOMManagementPacks = chkImportSCOMManagementPacks.Checked;
            Properties.Settings.Default.ImportSCSMManagementPacks = chkImportSCSMManagementPacks.Checked;
            Properties.Settings.Default.IgnoreManagementPackCreationErrors = chkIgnoreManagementPackCreationErrors.Checked;
            Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus = chkAlwaysCreateManagementPacks.Checked;

            #endregion

            #region vCenter

            Properties.Settings.Default.vCenter_PrimaryDatabaseConnection = txtvCenterPrimaryConnection.Text;
            Properties.Settings.Default.vCenter_SecondaryDatabaseConnection = txtvCenterSecondaryConnection.Text;
            Properties.Settings.Default.PrimaryvCenterSiteName = txtPrimaryvCenterSiteName.Text;
            Properties.Settings.Default.SecondaryvCenterSiteName = txtSecondaryvCenterSiteName.Text;
            Properties.Settings.Default.vCenter_InformationQuery = txtvCenterInformationQuery.Text;
            Properties.Settings.Default.vCenter_StorageQuery = txtvCenterStorageQuery.Text;

            #endregion

            #region Restore

            #region Stream A

            Properties.Settings.Default.Restore_StreamARestoreTime1 = TimeSpan.Parse(txtStreamATime.Text);
            Properties.Settings.Default.Restore_StreamARestoreRate1 = (int)nudStreamAStorage.Value;
            Properties.Settings.Default.Restore_StreamARestoreTime2 = TimeSpan.Parse(txtStreamATAndSMinutes.Text);
            Properties.Settings.Default.Restore_StreamARestoreRate2 = (int)nudStreamATAndS.Value;
            Properties.Settings.Default.Restore_StreamAOverheadTime = TimeSpan.Parse(txtStreamAOverheadTime.Text);

            if (rbStreamATimeBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamAAvailabilityPreditionMethod = 0;
            }
            else if (rbStreamAStorageBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamAAvailabilityPreditionMethod = 1;
            }
            else if (rbStreamATimeAndStorageBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamAAvailabilityPreditionMethod = 2;
            }

            Properties.Settings.Default.Restore_StreamAIntervalPeriod = TimeSpan.Parse(txtStreamAIntervalPeriod.Text);

            if (cmbStreamAServicePrerequisite.Text != "[Drawings not loaded]")
            {
                Properties.Settings.Default.Restore_StreamAServicePrerequisite = cmbStreamAServicePrerequisite.Text;
            }

            #endregion

            #region Stream B

            Properties.Settings.Default.Restore_StreamBRestoreTime1 = TimeSpan.Parse(txtStreamBTime.Text);
            Properties.Settings.Default.Restore_StreamBRestoreRate1 = (int)nudStreamBStorage.Value;
            Properties.Settings.Default.Restore_StreamBRestoreTime2 = TimeSpan.Parse(txtStreamBTAndSMinutes.Text);
            Properties.Settings.Default.Restore_StreamBRestoreRate2 = (int)nudStreamBTAndS.Value;
            Properties.Settings.Default.Restore_StreamBOverheadTime = TimeSpan.Parse(txtStreamBOverheadTime.Text);

            if (rbStreamBTimeBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamBAvailabilityPreditionMethod = 0;
            }
            else if (rbStreamBStorageBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamBAvailabilityPreditionMethod = 1;
            }
            else if (rbStreamBTimeAndStorageBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamBAvailabilityPreditionMethod = 2;
            }

            Properties.Settings.Default.Restore_StreamBIntervalPeriod = TimeSpan.Parse(txtStreamBIntervalPeriod.Text);

            if (cmbStreamBServicePrerequisite.Text != "[Drawings not loaded]")
            {
                Properties.Settings.Default.Restore_StreamBServicePrerequisite = cmbStreamBServicePrerequisite.Text;
            }

            #endregion

            #region Stream C

            Properties.Settings.Default.Restore_StreamCRestoreTime1 = TimeSpan.Parse(txtStreamCTime.Text);
            Properties.Settings.Default.Restore_StreamCRestoreRate1 = (int)nudStreamCStorage.Value;
            Properties.Settings.Default.Restore_StreamCRestoreTime2 = TimeSpan.Parse(txtStreamCTAndSMinutes.Text);
            Properties.Settings.Default.Restore_StreamCRestoreRate2 = (int)nudStreamCTAndS.Value;
            Properties.Settings.Default.Restore_StreamCOverheadTime = TimeSpan.Parse(txtStreamCOverheadTime.Text);

            if (rbStreamCTimeBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamCAvailabilityPreditionMethod = 0;
            }
            else if (rbStreamCStorageBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamCAvailabilityPreditionMethod = 1;
            }
            else if (rbStreamCTimeAndStorageBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamCAvailabilityPreditionMethod = 2;
            }

            Properties.Settings.Default.Restore_StreamCIntervalPeriod = TimeSpan.Parse(txtStreamCIntervalPeriod.Text);

            if (cmbStreamCServicePrerequisite.Text != "[Drawings not loaded]")
            {
                Properties.Settings.Default.Restore_StreamCServicePrerequisite = cmbStreamCServicePrerequisite.Text;
            }

            #endregion

            #region Stream D

            Properties.Settings.Default.Restore_StreamDRestoreTime1 = TimeSpan.Parse(txtStreamDTime.Text);
            Properties.Settings.Default.Restore_StreamDRestoreRate1 = (int)nudStreamDStorage.Value;
            Properties.Settings.Default.Restore_StreamDRestoreTime2 = TimeSpan.Parse(txtStreamDTAndSMinutes.Text);
            Properties.Settings.Default.Restore_StreamDRestoreRate2 = (int)nudStreamDTAndS.Value;
            Properties.Settings.Default.Restore_StreamDOverheadTime = TimeSpan.Parse(txtStreamDOverheadTime.Text);

            if (rbStreamDTimeBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamDAvailabilityPreditionMethod = 0;
            }
            else if (rbStreamDStorageBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamDAvailabilityPreditionMethod = 1;
            }
            else if (rbStreamDTimeAndStorageBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamDAvailabilityPreditionMethod = 2;
            }

            Properties.Settings.Default.Restore_StreamDIntervalPeriod = TimeSpan.Parse(txtStreamDIntervalPeriod.Text);

            if (cmbStreamDServicePrerequisite.Text != "[Drawings not loaded]")
            {
                Properties.Settings.Default.Restore_StreamDServicePrerequisite = cmbStreamDServicePrerequisite.Text;
            }

            #endregion

            #region Stream E

            Properties.Settings.Default.Restore_StreamERestoreTime1 = TimeSpan.Parse(txtStreamETime.Text);
            Properties.Settings.Default.Restore_StreamERestoreRate1 = (int)nudStreamEStorage.Value;
            Properties.Settings.Default.Restore_StreamERestoreTime2 = TimeSpan.Parse(txtStreamETAndSMinutes.Text);
            Properties.Settings.Default.Restore_StreamERestoreRate2 = (int)nudStreamETAndS.Value;
            Properties.Settings.Default.Restore_StreamEOverheadTime = TimeSpan.Parse(txtStreamEOverheadTime.Text);

            if (rbStreamETimeBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamEAvailabilityPreditionMethod = 0;
            }
            else if (rbStreamEStorageBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamEAvailabilityPreditionMethod = 1;
            }
            else if (rbStreamETimeAndStorageBased.Checked)
            {
                Properties.Settings.Default.Restore_StreamEAvailabilityPreditionMethod = 2;
            }

            Properties.Settings.Default.Restore_StreamEIntervalPeriod = TimeSpan.Parse(txtStreamEIntervalPeriod.Text);

            if (cmbStreamEServicePrerequisite.Text != "[Drawings not loaded]")
            {
                Properties.Settings.Default.Restore_StreamEServicePrerequisite = cmbStreamEServicePrerequisite.Text;
            }

            #endregion

            Properties.Settings.Default.Restore_ParallelRestoreCount = (int)nudParallelRestoreCount.Value;
            Properties.Settings.Default.Restore_IncludeOptionalServicesWhenCalculatingRestoreTimes = chkIncludeOptionalServices.Checked;
            Properties.Settings.Default.Restore_OverallOverheadTime = TimeSpan.Parse(txtRestoreOverheadTime.Text);

            #endregion

            #region Security

            Properties.Settings.Default.RestrictFeaturesByGroupMembership = chkRestrictByMembership.Checked;
            Properties.Settings.Default.Security_SystemCenterMPAuthorGroup = txtMPAuthorGroup.Text;
            Properties.Settings.Default.Security_DependencyDatabaseReadGroup = txtiServerROGroup.Text;
            Properties.Settings.Default.Security_DependencyDatabaseWriteGroup = txtiServerRWGroup.Text;
            Properties.Settings.Default.Security_vCenterSQLReadGroup = txtvCenterReadGroup.Text;

            #endregion

            #region Experimental Features

            Properties.Settings.Default.ShowServersOnFullDependencyMap = chkShowServersOnDependencyMaps.Checked;
            Properties.Settings.Default.ShowStatesOnDependencyMaps = chkShowStatesOnDependencyMaps.Checked;

            #endregion

            Properties.Settings.Default.Save();

            #region Set Static Properties

            ISA.Helper.Properties.vCenterFunctionsEnabled = chkEnablevCenterQueries.Checked;
            ISA.Helper.Properties.SystemCenterFunctionsEnabled = chkEnableSystemCenterFunctions.Checked;
            ISA.Helper.Properties.ShowDrawingStatusOnBusinessApplication = chkShowDrawingStatusOnService.Checked;

            #endregion
        }


    }
}
