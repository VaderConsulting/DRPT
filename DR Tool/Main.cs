using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISA.CommandLine;
using ISA.Database;
using ISA.Dependency;
using ISA.Helper;
using ISA.Orbus;
using ISA.SystemCenter;
using System.Diagnostics;
using Microsoft.AGL.GraphViewerGdi;
using System.Data;
using System.Configuration;
using System.Drawing;

namespace DRPlanningTool
{
    public static class Main
    {
        #region Fields

        private static List<frmBusinessApplicationImage> _BusinessApplicationImageForms = new List<frmBusinessApplicationImage>();
        private static List<frmDependencyMap> _DependencyMapForms = new List<frmDependencyMap>();
        private static List<frmList> _ListForms = new List<frmList>();
        private static SQLServer _LocalDatabase = new SQLServer(Properties.Settings.Default.SQLServerQueryTimeout);
        private static List<BusinessApplication> _OutOfScopeApplicationsToShow = new List<BusinessApplication>();
        private static SQLServer _PrimaryvCenterDatabase = new SQLServer(Properties.Settings.Default.SQLServerQueryTimeout);
        private static SQLServer _SecondaryvCenterDatabase = new SQLServer(Properties.Settings.Default.SQLServerQueryTimeout);
        private static List<frmServer> _ServerForms = new List<frmServer>();

        #endregion

        #region Public variables

        public static List<frmBusinessApplication> BusinessApplicationForms = new List<frmBusinessApplication>();
        public static iServer DataLayer = null;
        public static TextTraceListener[] DebugWriter = null;
        public static Collection DependencyCollection = null;
        public static SQLServer DependencyDatabase = new SQLServer(Properties.Settings.Default.SQLServerQueryTimeout);
        public static iServer.ObjectChanges LastDrawingDatabaseTimeStamp = new iServer.ObjectChanges();
        public static string SCOMServerName = Properties.Settings.Default.SystemCenter_SCOMServer;
        public static string SCSMServerName = Properties.Settings.Default.SystemCenter_SCSMServer;
        public static List<Server> ServerRunbook = null;
        public static List<BusinessApplication> BusinessApplicationRunbook = null;
        public static frmInformation InformationForm = null;
        public static frmDrawings DrawingsForm = null;
        public static frmMain MainForm = null;
        public static frmMDIMain MDIForm = null;
        public static bool MDIInterface = false;

        #endregion

        // No constructor for Static classes

        #region Public Methods

        public static void AddBusinessApplicationToListView(BusinessApplication App, string DrawingStatusText, ListView Listview)
        {
            ListViewItem Item = new ListViewItem();

            if (App != null)
            {
                if (App.Drawing != null)
                {
                    Item.Text = App.Drawing.Name;
                    Item.Name = App.Drawing.Name;
                    Item.Tag = App;

                    Item.SubItems.Add(DrawingStatusText);

                    // By default, set each Drawing (i.e. Item) as Approved
                    Item.ImageIndex = Global.GetDrawingImageIndex(App.Drawing);

                    if (App != null)
                    {
                        if ((App.Drawing.Image == null && Properties.Settings.Default.ShowDrawingLessBusinessApplications) ||
                            (App.Drawing.Image != null))
                        {
                            if (App.InScope || App.OverrideInScope)
                            {
                                Listview.Items.Add(Item);
                            }
                        }
                    }
                    else
                    {
                        Listview.Items.Add(Item);
                    }
                }
                else
                {
                    AddDrawingException(App.Name + ": Could not add the blank Drawing to the Main listview", App);
                }
            }
            else
            {
                AddDrawingException("Could not add the [un-named] Business Application to the Main listview", App);
            }
        }

        public static void AddBusinessApplicationsToListView(ListView Listview)
        {
            Listview.Items.Clear();

            Listview.BeginUpdate();
            foreach (BusinessApplication Application in Main.DataLayer.Collection.Applications)
            {
                if (Application.InScope || Application.OverrideInScope)
                {
                    if (Application.Drawing != null)
                    {
                        Main.AddBusinessApplicationToListView(Application, Application.Drawing.StatusText, Listview);
                    }
                    else
                    {
                        Main.AddBusinessApplicationToListView(Application, "N/A", Listview);
                    }
                }
            }
            Listview.EndUpdate();

            //this.Refresh();
        }

        public static void AddDatabaseUpdate(string Message)
        {
            InformationForm.AddDatabaseWarning(Message);
        }

        public static void AddDatabaseWarning(string Message, BusinessApplication Application)
        {
            Application.Warnings.Add(Message);
            InformationForm.AddDatabaseWarning(Message, Application);
        }

        public static void AddDatabaseWarning(string Message, Server Server)
        {
            if (Server != null)
            {
                Server.Warnings.Add(Message);
            }
            InformationForm.AddDatabaseWarning(Message, Server);
        }

        public static void AddDrawingCriticalException(string Message, BusinessApplication Application)
        {
            Application.Warnings.Add(Message);
            InformationForm.AddDrawingCriticalException(Message, Application);
        }

        public static void AddDrawingException(string Message)
        {
            InformationForm.AddDrawingException(Message);
        }

        public static void AddDrawingException(string Message, BusinessApplication Application)
        {
            Application.Warnings.Add(Message);
            InformationForm.AddDrawingException(Message, Application);
        }

        public static void AddDrawingException(string Message, Server Server)
        {
            Server.Warnings.Add(Message);
            InformationForm.AddDrawingException(Message, Server);
        }

        public static void AddException(string Message)
        {
            InformationForm.AddException(Message);
        }

        public static void AddException(string Message, BusinessApplication Application)
        {
            Application.Warnings.Add(Message);
            InformationForm.AddException(Message, Application);
        }

        public static void AddOutOfScopeServer(Server Server)
        {
            InformationForm.AddOutOfScopeServer(Server);
            ISA.Helper.Properties.ReloadNecessary = true;
        }

        public static void AddOutOfScopeApplication(BusinessApplication Application)
        {
            InformationForm.AddOutOfScopeApplication(Application);
            ISA.Helper.Properties.ReloadNecessary = true;
        }

        public static void AddOutOfScopeApplications()
        {
            foreach (BusinessApplication Service in DependencyCollection.Applications)
            {
                if (!Service.InScope && !Service.OverrideInScope)
                {
                    if (Properties.Settings.Default.ShowDebugInformation)
                    {
                        Debug.WriteLine("..." + Service.Name, "information");
                    }

                    AddOutOfScopeApplication(Service);
                }
            }
        }

        public static void AddStreamDependencies()
        {
            #region Stream A

            if (Properties.Settings.Default.Restore_StreamAServicePrerequisite != "[None]")
            {
                // Convert the stream dependency name to the Service
                BusinessApplication StreamADependency = Global.GetExistingBusinessApplication(Properties.Settings.Default.Restore_StreamAServicePrerequisite, DependencyCollection.Applications);

                if (StreamADependency != null)
                {
                    // Now go through and find the first Service that is Stream A, and make this Service a StreamDependency
                    foreach (BusinessApplication Application in DependencyCollection.Applications) //_BusinessApplicationRunbook)
                    {
                        if (Application.CalculatedStream == Global.RecoveryStream.A)
                        {
                            BusinessApplication ThisApplication = Application;

                            Relationship Prerequisite = new Relationship(ThisApplication, StreamADependency);
                            Prerequisite.StreamDependency = true;

                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("..." + ThisApplication.Name + "...", "information");
                            }

                            if (!DependencyCollection.AddRelationship(ref Prerequisite))
                            {
                                AddDrawingCriticalException("[ERR1084] " + Application.Name + " (Business Application): Check for circular dependency with " + StreamADependency.Name + " (Business Application)", Application);
                            }

                            //break;
                        }
                    }
                }
            }

            #endregion

            #region Stream B

            if (Properties.Settings.Default.Restore_StreamBServicePrerequisite != "[None]")
            {
                // Convert the stream dependency name to the Service
                BusinessApplication StreamBDependency = Global.GetExistingBusinessApplication(Properties.Settings.Default.Restore_StreamBServicePrerequisite, DependencyCollection.Applications);

                if (StreamBDependency != null)
                {
                    // Now go through and find the first Service that is Stream B, and make this Service a StreamDependency
                    foreach (BusinessApplication Application in DependencyCollection.Applications) //_BusinessApplicationRunbook)
                    {
                        if (Application.CalculatedStream == Global.RecoveryStream.B)
                        {
                            BusinessApplication ThisApplication = Application;

                            Relationship Prerequisite = new Relationship(ThisApplication, StreamBDependency);
                            Prerequisite.StreamDependency = true;

                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("..." + ThisApplication.Name + "...", "information");
                            }

                            if (!DependencyCollection.AddRelationship(ref Prerequisite))
                            {
                                AddDrawingCriticalException("[ERR1085] " + Application.Name + " (Business Application): Check for circular dependency with " + StreamBDependency.Name + " (Business Application)", Application);
                            }

                            //break;
                        }
                    }
                }
            }

            #endregion

            #region Stream C

            if (Properties.Settings.Default.Restore_StreamCServicePrerequisite != "[None]")
            {
                // Convert the stream dependency name to the Service
                BusinessApplication StreamCDependency = Global.GetExistingBusinessApplication(Properties.Settings.Default.Restore_StreamCServicePrerequisite, DependencyCollection.Applications);

                if (StreamCDependency != null)
                {
                    // Now go through and find the first Service that is Stream C, and make this Service a StreamDependency
                    foreach (BusinessApplication Application in DependencyCollection.Applications) //_BusinessApplicationRunbook)
                    {
                        if (Application.CalculatedStream == Global.RecoveryStream.C)
                        {
                            BusinessApplication ThisApplication = Application;

                            Relationship Prerequisite = new Relationship(ThisApplication, StreamCDependency);
                            Prerequisite.StreamDependency = true;

                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("..." + ThisApplication.Name + "...", "information");
                            }

                            if (!DependencyCollection.AddRelationship(ref Prerequisite))
                            {
                                AddDrawingCriticalException("[ERR1086] " + Application.Name + " (Business Application): Check for circular dependency with " + StreamCDependency.Name + " (Business Application)", Application);
                            }

                            //break;
                        }
                    }
                }
            }

            #endregion

            #region Stream D

            if (Properties.Settings.Default.Restore_StreamDServicePrerequisite != "[None]")
            {
                // Convert the stream dependency name to the Service
                BusinessApplication StreamDDependency = Global.GetExistingBusinessApplication(Properties.Settings.Default.Restore_StreamDServicePrerequisite, DependencyCollection.Applications);

                if (StreamDDependency != null)
                {
                    // Now go through and find the first Service that is Stream D, and make this Service a StreamDependency
                    foreach (BusinessApplication Application in DependencyCollection.Applications) //_BusinessApplicationRunbook)
                    {
                        if (Application.CalculatedStream == Global.RecoveryStream.D)
                        {
                            BusinessApplication ThisApplication = Application;

                            Relationship Prerequisite = new Relationship(ThisApplication, StreamDDependency);
                            Prerequisite.StreamDependency = true;

                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("..." + ThisApplication.Name + "...", "information");
                            }

                            if (!DependencyCollection.AddRelationship(ref Prerequisite))
                            {
                                AddDrawingCriticalException("[ERR1087] " + Application.Name + " (Business Application): Check for circular dependency with " + StreamDDependency.Name + " (Business Application)", Application);
                            }

                            //break;
                        }
                    }
                }
            }

            #endregion

            #region Stream E

            if (Properties.Settings.Default.Restore_StreamEServicePrerequisite != "[None]")
            {
                // Convert the stream dependency name to the Service
                BusinessApplication StreamEDependency = Global.GetExistingBusinessApplication(Properties.Settings.Default.Restore_StreamEServicePrerequisite, DependencyCollection.Applications);

                if (StreamEDependency != null)
                {
                    // Now go through and find the first Service that is Stream E, and make this Service a StreamDependency
                    foreach (BusinessApplication Application in DependencyCollection.Applications) //_BusinessApplicationRunbook)
                    {
                        if (Application.CalculatedStream == Global.RecoveryStream.E)
                        {
                            BusinessApplication ThisApplication = Application;

                            Relationship Prerequisite = new Relationship(ThisApplication, StreamEDependency);
                            Prerequisite.StreamDependency = true;

                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("..." + ThisApplication.Name + "...", "information");
                            }

                            if (!DependencyCollection.AddRelationship(ref Prerequisite))
                            {
                                AddDrawingCriticalException("[ERR1088] " + Application.Name + " (Business Application): Check for circular dependency with " + StreamEDependency.Name + " (Business Application)", Application);
                            }

                            //break;
                        }
                    }
                }
            }

            #endregion

        }

        public static void AddWarning(string Message)
        {
            InformationForm.AddWarning(Message);
        }

        public static void AddWarning(string Message, BusinessApplication Application)
        {
            Application.Warnings.Add(Message);
            InformationForm.AddWarning(Message, Application);
        }

        public static void AddWarning(string Message, Server Server)
        {
            Server.Warnings.Add(Message);
            InformationForm.AddWarning(Message, Server);
        }

        public static void ClearWarnings()
        {
            InformationForm.Clear();
            InformationForm.Refresh();
        }

        public static void CreateAllManagementPacks(Global.Environment Environment, bool CreateAsStub)
        {
            Cursor.Current = Cursors.WaitCursor;

            bool Result = false;

            Debug.WriteLine("[INF1190] Creating Management Packs...", "information");

            foreach (BusinessApplication Service in BusinessApplicationRunbook)  /// Create in runbook order
            {
                if (Properties.Settings.Default.SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus || Service.Drawing.Status == Global.DrawingStatus.Approved)
                {
                    if (Properties.Settings.Default.ShowDebugInformation)
                    {
                        Debug.WriteLine("..." + Service.Name + "...", "information");
                    }
                    Result = CreateManagementPackFromBusinessApplication(Service, CreateAsStub, Environment);
                }
                else
                {
                    Debug.WriteLine("...[INF1081]" + Service.Name + " Drawing status is not Approved, so a Management Pack for this Business Application will not be created", "information");
                    Result = true;
                }

                if (Result)  // Success
                {

                }
                else         // Failure
                {
                    Main.AddWarning("[ERR1029] A problem was encountered whilst updating or creating a Management Pack", Service);

                    if (!Properties.Settings.Default.IgnoreManagementPackCreationErrors)
                    {
                        break;
                    }
                }
            }

            Cursor.Current = Cursors.Default;
        }

        public static void CreateDependencyMapForm(List<BusinessApplication> Applications, Form ParentForm, List<Relationship> Relationships, List<BusinessApplication> FocusedServices, bool ShowDependents, bool ShowServers, LayoutMethod Layout, bool BundleSplines)
        {
            frmDependencyMap MapForm = new frmDependencyMap(Applications, ParentForm, Relationships, FocusedServices, ShowDependents, ShowServers, Layout, BundleSplines);

            MapForm.Owner = ParentForm;

            _DependencyMapForms.Add(MapForm);

            MapForm.Show(ParentForm);

        }

        public static bool CreateManagementPackFromBusinessApplication(BusinessApplication BusinessApplication, bool CreateAsStub, Global.Environment Environment)
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
                    if (Properties.Settings.Default.SystemCenter_TestSCOMServer != "")
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_TestSCOMServer;
                    }
                    else if (Properties.Settings.Default.SystemCenter_TestSCSMServer != "")
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_TestSCSMServer;
                    }
                    else
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = "";
                    }
                    break;
                case Global.Environment.Production:
                    if (Properties.Settings.Default.SystemCenter_SCOMServer != "")
                    {
                        CreationOptions.ConnectionInfo.ManagementGroupServerName = Properties.Settings.Default.SystemCenter_SCOMServer;
                    }
                    else if (Properties.Settings.Default.SystemCenter_SCSMServer != "")
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

        public static void CreateManagementPacksFromSelectedItems(bool AsStub, Global.Environment Environment, ListView Listview)
        {
            bool Result = false;

            foreach (ListViewItem Item in Listview.SelectedItems)
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

        public static void CreateOrSelectBusinessApplicationForm(string DrawingName, BusinessApplication Application)
        {
            frmBusinessApplication SelectedBusinessApplicationForm = null;

            foreach (frmBusinessApplication ServiceForm in BusinessApplicationForms)
            {
                if (ServiceForm.BusinessApplication == Application)
                {
                    SelectedBusinessApplicationForm = ServiceForm;
                }
            }

            if (SelectedBusinessApplicationForm == null)
            {
                if (MainForm == null)
                {
                    frmBusinessApplication NewBusinessApplicationForm = new frmBusinessApplication(DrawingName, Application, DependencyCollection.Applications, DependencyCollection.Relationships, MDIForm, DependencyDatabase);

                    NewBusinessApplicationForm.Owner = MDIForm;
                    NewBusinessApplicationForm.MdiParent = MDIForm;

                    BusinessApplicationForms.Add(NewBusinessApplicationForm);

                    NewBusinessApplicationForm.Show();
                    NewBusinessApplicationForm.Activate();
                }
                else
                {
                    frmBusinessApplication NewBusinessApplicationForm = new frmBusinessApplication(DrawingName, Application, DependencyCollection.Applications, DependencyCollection.Relationships, MainForm, DependencyDatabase);

                    NewBusinessApplicationForm.Owner = MainForm;

                    BusinessApplicationForms.Add(NewBusinessApplicationForm);

                    NewBusinessApplicationForm.Show(MainForm);
                    NewBusinessApplicationForm.Activate();
                }
            }
            else
            {
                SelectedBusinessApplicationForm.Show();
                SelectedBusinessApplicationForm.BringToFront();
                SelectedBusinessApplicationForm.Activate();
                FlashWindow.Flash(SelectedBusinessApplicationForm, 5);
            }
        }

        public static void CreateOrSelectBusinessApplicationImageForm(BusinessApplication Application)
        {
            frmBusinessApplicationImage SelectedBusinessApplicationImageForm = null;

            foreach (frmBusinessApplicationImage ServiceForm in _BusinessApplicationImageForms)
            {
                if (ServiceForm.Image == Application.Drawing.Image)
                {
                    SelectedBusinessApplicationImageForm = ServiceForm;
                }
            }

            if (SelectedBusinessApplicationImageForm == null)
            {
                if (MainForm == null)
                {
                    frmBusinessApplicationImage NewBusinessApplicationImageForm = new frmBusinessApplicationImage(Application.Drawing.Image, MDIForm, Application.Name);

                    NewBusinessApplicationImageForm.Owner = MDIForm;
                    NewBusinessApplicationImageForm.MdiParent = MDIForm;

                    _BusinessApplicationImageForms.Add(NewBusinessApplicationImageForm);

                    NewBusinessApplicationImageForm.Show();
                    NewBusinessApplicationImageForm.Activate();
                }
                else
                {
                    frmBusinessApplicationImage NewBusinessApplicationImageForm = null;


                    NewBusinessApplicationImageForm = new frmBusinessApplicationImage(Application.Drawing.Image, MainForm, Application.Name);

                    NewBusinessApplicationImageForm.Owner = MainForm;

                    _BusinessApplicationImageForms.Add(NewBusinessApplicationImageForm);

                    NewBusinessApplicationImageForm.Show(MainForm);
                    NewBusinessApplicationImageForm.Activate();
                }
            }
            else
            {
                SelectedBusinessApplicationImageForm.Show();
                SelectedBusinessApplicationImageForm.BringToFront();
                SelectedBusinessApplicationImageForm.Activate();
                FlashWindow.Flash(SelectedBusinessApplicationImageForm, 5);
            }
        }

        public static void CreateOrSelectDependencyMapForm(List<BusinessApplication> Applications, bool IncludeServers, List<BusinessApplication> FocusedApplications, LayoutMethod Layout, bool BundleSplines)
        {
            frmDependencyMap SelectedMapForm = null;

            foreach (frmDependencyMap MapForm in _DependencyMapForms)
            {
                if (MapForm.Services.Count == 0 && MapForm.Services == Applications)
                {
                    SelectedMapForm = MapForm;
                }
            }

            if (SelectedMapForm == null)
            {
                if (MainForm == null)
                {
                    frmDependencyMap MapForm = new frmDependencyMap(Applications, MDIForm, DependencyCollection.Relationships, FocusedApplications, true, IncludeServers, Layout, BundleSplines);

                    MapForm.Owner = MDIForm;
                    MapForm.MdiParent = MDIForm;

                    _DependencyMapForms.Add(MapForm);

                    MapForm.Show();
                    MapForm.Activate();
                }
                else
                {
                    frmDependencyMap MapForm = new frmDependencyMap(Applications, MainForm, DependencyCollection.Relationships, FocusedApplications, true, IncludeServers, Layout, BundleSplines);

                    MapForm.Owner = MainForm;

                    _DependencyMapForms.Add(MapForm);

                    MapForm.Show(MainForm);
                    MapForm.Activate();
                }
            }
            else
            {
                SelectedMapForm.Show();
                SelectedMapForm.BringToFront();
                SelectedMapForm.Activate();
                FlashWindow.Flash(SelectedMapForm, 5);
            }
        }

        public static void CreateOrSelectServerForm(ref Server Server)
        {
            frmServer SelectedServerForm = null;

            foreach (frmServer ServerForm in _ServerForms)
            {
                if (ServerForm.Server == Server)
                {
                    SelectedServerForm = ServerForm;
                }
            }

            if (SelectedServerForm == null)
            {
                if (MainForm == null)
                {
                    frmServer NewServerForm = new frmServer(Server, DependencyCollection.Servers, DependencyCollection.Applications, MDIForm, DependencyDatabase);

                    NewServerForm.Owner = MDIForm;
                    NewServerForm.MdiParent = MDIForm;

                    _ServerForms.Add(NewServerForm);

                    NewServerForm.Show();
                    NewServerForm.Activate();
                }
                else
                {
                    frmServer NewServerForm = new frmServer(Server, DependencyCollection.Servers, DependencyCollection.Applications, MainForm, DependencyDatabase);

                    NewServerForm.Owner = MainForm;

                    _ServerForms.Add(NewServerForm);

                    NewServerForm.Show(MainForm);
                    NewServerForm.Activate();
                }
            }
            else
            {
                SelectedServerForm.Show();
                SelectedServerForm.BringToFront();
                SelectedServerForm.Activate();
                FlashWindow.Flash(SelectedServerForm, 5);
            }
        }

        public static void DetermineAllBusinessApplicationInFocus(ListView Listview)
        {
            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < BusinessApplicationRunbook.Count; i++)
            {
                BusinessApplication s = BusinessApplicationRunbook[i];
                DetermineBusinessApplicationInFocus(ref s, Listview);
                BusinessApplicationRunbook[i] = s;
            }

            UpdateChildForms();

            Cursor.Current = Cursors.Default;
        }

        public static void DetermineAllBusinessApplicationRecoveryTasks()
        {
            Cursor.Current = Cursors.WaitCursor;

            foreach (BusinessApplication BusinessApplication in BusinessApplicationRunbook) // Perform in runbook order
            {
                DetermineBusinessApplicationRecoveryTasks(BusinessApplication);
            }

            UpdateChildForms();

            Cursor.Current = Cursors.Default;
        }

        public static void DetermineAllBusinessApplicationState(ListView Listview)
        {
            Cursor.Current = Cursors.WaitCursor;

            Listview.BeginUpdate();

            if (Properties.Settings.Default.ShowDebugInformation)
            {
                Debug.WriteLine("[INF1198] Determining Business Application State...", "information");
            }

            // The Business Application Runbook is already in an order that ensures that dependencies are processed correctly
            foreach (BusinessApplication BusinessApplication in BusinessApplicationRunbook) //_DC.Services
            {
                // If the state hasn't been set explicitly at the Service Level (it should be a combination of the states of it's Components),
                // then work out it's state.
                if (!BusinessApplication.StateSetAtApplicationLevel)
                {
                    DetermineBusinessApplicationState(BusinessApplication, Listview);
                }
            }

            // Now we have to ensure that the State of any Service that isn't in the ServiceRunbook is also calculated
            foreach (BusinessApplication Service in DependencyCollection.Applications)
            {
                if (Global.GetExistingBusinessApplication(Service, BusinessApplicationRunbook) == null)
                    DetermineBusinessApplicationState(Service, Listview);
            }

            UpdateChildForms();

            Listview.EndUpdate();

            Cursor.Current = Cursors.Default;

            Application.DoEvents();
        }

        public static void DetermineBusinessApplicationRecoveryTasks(BusinessApplication BusinessApplication)
        {
            TimeSpan DefaultRestoreTime = new TimeSpan(21, 0, 0, 0);
            TimeSpan ThisRecoveryTime = new TimeSpan(0, 0, 0, 0);
            List<RecoveryTask> Tasks = new List<RecoveryTask>();


            foreach (Relationship Relationship in BusinessApplication.Relationships)
            {
                if (Relationship.SystemMandatory ||
                   (!Relationship.SystemMandatory && Properties.Settings.Default.Restore_IncludeOptionalServicesWhenCalculatingRestoreTimes)
                   )
                {
                    if (!Relationship.StreamDependency)
                    {
                        switch (Relationship.ToType)
                        {
                            case ISA.Dependency.Relationship.ComponentType.Server:

                                Server ComponentServer = Relationship.ToServer;

                                if (ComponentServer.Stream != Global.RecoveryStream.A)
                                {
                                    foreach (RecoveryTask Task in ComponentServer.RecoveryTasks)
                                    {
                                        if (Task != null)
                                        {
                                            Tasks.Add(Task);
                                        }
                                    }

                                    ThisRecoveryTime += ComponentServer.RecoveryTime;
                                }

                                break;
                            case ISA.Dependency.Relationship.ComponentType.BusinessApplication:

                                BusinessApplication ComponentBusinessApplication = Relationship.ToBusinessApplication;

                                if (ComponentBusinessApplication.CalculatedStream != Global.RecoveryStream.A)
                                {
                                    if (ComponentBusinessApplication.CalculatedRecoveryTime == DefaultRestoreTime)
                                    {
                                        // Recursive call
                                        DetermineBusinessApplicationRecoveryTasks(ComponentBusinessApplication);
                                    }

                                    foreach (RecoveryTask Task in ComponentBusinessApplication.RecoveryTasks)
                                    {
                                        if (Task != null)
                                        {
                                            Tasks.Add(Task);
                                        }
                                    }

                                    ThisRecoveryTime += ComponentBusinessApplication.CalculatedRecoveryTime;
                                }

                                break;
                        }
                    }
                }
            }

            BusinessApplication.RecoveryTasks = Tasks;
            BusinessApplication.CalculatedRecoveryTime = ThisRecoveryTime;
        }

        public static void DetermineBusinessApplicationState(BusinessApplication BusinessApplication, ListView Listview)
        {
            BusinessApplication.CalculateState("");

            if (Properties.Settings.Default.ShowDebugInformation)
            {
                Debug.WriteLine("..." + BusinessApplication.Name + ": " + BusinessApplication.SystemState, "information");
            }

            // Find matching node in the ListView...
            ListViewItem BusinessApplicationItem = Listview.FindItemWithText(BusinessApplication.Name);

            if (BusinessApplicationItem != null)
            {
                if (BusinessApplication.Drawing != null)
                {
                    switch (BusinessApplication.Drawing.Status)
                    {
                        case Global.DrawingStatus.Not_Applicable:
                            BusinessApplicationItem.SubItems[1].Text = "N/A";
                            break;
                        case Global.DrawingStatus.Draft:
                            BusinessApplicationItem.SubItems[1].Text = "Draft";
                            break;
                        case Global.DrawingStatus.Pending_Review:
                            BusinessApplicationItem.SubItems[1].Text = "Pending";
                            break;
                        case Global.DrawingStatus.Approved:
                            BusinessApplicationItem.SubItems[1].Text = "Approved";
                            break;
                    }
                }
                else
                {
                    BusinessApplicationItem.SubItems[1].Text = "N/A";
                }
            }
        }

        public static void DoExit()
        {
            if (MainForm == null)
            {
                // MDI Form
                if (MDIForm.WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.frmMDIMainSize = MDIForm.Size;

                    if (ISA.Helper.Properties.AllowSaveFormLocations)
                    {
                        Properties.Settings.Default.frmMDIMainLocation = MDIForm.Location;
                    }
                }

                // Drawings Form
                if (DrawingsForm.WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.frmDrawingsSize = DrawingsForm.Size;

                    if (ISA.Helper.Properties.AllowSaveFormLocations)
                    {
                        Properties.Settings.Default.frmDrawingsLocation = DrawingsForm.Location;
                    }
                }
            }
            else
            {
                if (MainForm.WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.frmMainSize = MainForm.Size;

                    if (ISA.Helper.Properties.AllowSaveFormLocations)
                    {
                        Properties.Settings.Default.frmMainLocation = MainForm.Location;
                    }
                }
            }

            if (ISA.Helper.Properties.ProgressWindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.frmProgressSize = ISA.Helper.Properties.frmProgressSize;

                if (ISA.Helper.Properties.AllowSaveFormLocations)
                {
                    Properties.Settings.Default.frmProgressLocation = ISA.Helper.Properties.frmProgressLocation;
                }
            }

            try
            {
                Properties.Settings.Default.Save();

                Environment.Exit(0);
            }
            catch
            {
                try
                {
                    Application.Exit();
                }
                catch
                {
                }
            }

        }

        public static void DoSetup()
        {
            //if (MainForm == null)
            //{
            //    DrawingsForm = (frmMain)DrawingsForm;
            //}
            //else
            //{
            //    DrawingsForm = (frmDrawings)DrawingsForm;
            //}

            #region Load settings from central database

            if (LoadSettings(Properties.Settings.Default.SettingsFilename))
            {
                ISA.Helper.Properties.StreamAServiceName = Properties.Settings.Default.Restore_StreamAServicePrerequisite;
                ISA.Helper.Properties.StreamBServiceName = Properties.Settings.Default.Restore_StreamBServicePrerequisite;
                ISA.Helper.Properties.StreamCServiceName = Properties.Settings.Default.Restore_StreamCServicePrerequisite;
                ISA.Helper.Properties.StreamDServiceName = Properties.Settings.Default.Restore_StreamDServicePrerequisite;
                ISA.Helper.Properties.StreamEServiceName = Properties.Settings.Default.Restore_StreamEServicePrerequisite;

                ISA.Helper.Properties.FlagDrawingErrors = Properties.Settings.Default.FlagDrawingErrors;

                if (MainForm == null)
                {
                    DrawingsForm.SetupContextMenuStrip();
                    DrawingsForm.SetupListView();
                }
                else
                {
                    MainForm.SetupContextMenuStrip();
                    MainForm.SetupListView();
                }

                #region Define default paths

                if (Properties.Settings.Default.ExportFolderName == "")
                {
                    Properties.Settings.Default.ExportFolderName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }

                if (Properties.Settings.Default.SystemCenter_ManagementPackSaveFolderName == "")
                {
                    Properties.Settings.Default.SystemCenter_ManagementPackSaveFolderName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"#SERVICENAME#.xml");
                }

                if (Properties.Settings.Default.PowershellModulePath == "")
                {
                    Properties.Settings.Default.PowershellModulePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Vader Consulting\DR Tool\Powershell\System.Center.Service.Manager.psd1");
                }

                #endregion

                if (MainForm == null)
                {
                    MDIForm.EnableDependencyMapToolstrip();
                    DrawingsForm.SetupCheckboxes();
                }
                else
                {
                    MainForm.EnableDependencyMapToolstrip();
                    MainForm.SetupCheckboxes();
                }
                //DependencyMapToolStripMenuItem.Visible = true;

                //lvwDrawings.CheckBoxes = Properties.Settings.Default.ShowCheckboxesOnServiceList;


            }
            else
            {
                // Could not load settings
                AddException("Could not load settings from " + Properties.Settings.Default.SettingsFilename);
            }

            #endregion

            #region Setup DataLayer

            ISA.Helper.Enums.DataLayerType DataLayerType = (ISA.Helper.Enums.DataLayerType)Enum.Parse(typeof(ISA.Helper.Enums.DataLayerType), Convert.ToString(Properties.Settings.Default.DataLayerType));
            ISA.Helper.Properties.DataLayer = DataLayerType;

            switch (DataLayerType)
            {
                case Enums.DataLayerType.iServer:
                    DataLayer = new iServer(Properties.Settings.Default.iServer_ProductionDocumentRootID, Properties.Settings.Default.iServer_DatabaseConnectionString, Properties.Settings.Default.SQLServerQueryTimeout);
                    DataLayer.GetVersionQuery = Properties.Settings.Default.iServer_GetDatabaseVersion;
                    DataLayer.ShowDebugMessages = Properties.Settings.Default.ShowDebugInformation;

                    DataLayer.AnnouncementHandler += new iServer.AnnouncementEventHandler(iServer_AnnouncementReceived); // Add connection to NewMessage event of iServer object

                    break;
                case Enums.DataLayerType.SQL:
                    break;
            }

            DataLayer.Initialise();

            #endregion

            #region P2P

            //if (Properties.Settings.Default.P2PEnabled)
            //{
            //    // Connect to the local Database
            //    string LocalDbPath = Properties.Settings.Default.P2P_LocalDBConnectionString.Replace("#APPPATH#", Application.StartupPath);

            //    _LocalDatabase.ConnectionString = LocalDbPath;

            //    if (Properties.Settings.Default.ShowDebugInformation)
            //    {
            //        Debug.WriteLine("Connecting to LocalDB...", "information");
            //    }
            //    Application.DoEvents();

            //    if (_LocalDatabase.OpenConnection())
            //    {
            //        if (Properties.Settings.Default.ShowDebugInformation)
            //        {
            //            Debug.WriteLine("...successful.", "information");
            //        }
            //    }
            //    else
            //    {
            //        if (Properties.Settings.Default.ShowDebugInformation)
            //        {
            //            Debug.WriteLine("...failed.", "error");
            //        }
            //    }
            //}

            #endregion

            #region Security

            ISA.Helper.Properties.RestrictGUIByGroupMembership = Properties.Settings.Default.RestrictFeaturesByGroupMembership;
            ISA.Helper.Properties.ShowDebugInformation = Properties.Settings.Default.ShowDebugInformation;

            // Check what rights the User has to the Dependency data
            UpdateDependencyDatabaseROStatus();

            UpdateDependencyDatabaseRWStatus();

            // Check if System Center functions are enabled
            //ISA.Helper.Properties.SystemCenterFunctionsEnabled = Properties.Settings.Default.EnableSystemCenterFunctions;
            //retrieveSCOMServerHealthToolStripMenuItem.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
            //toolStripButtonGetServerHealth.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
            UpdateSystemCenterToolStrip();

            // Check if vCenter functions are enabled
            UpdatevCenterReadStatus();

            #endregion

        }

        public static void ExportDetails()
        {
            string OutputHeader = "";
            System.IO.StreamWriter OutputWriter = null;
            string OutputPath = Properties.Settings.Default.ExportFolderName; // System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "output.txt");

            OutputWriter = new System.IO.StreamWriter(OutputPath);

            OutputHeader = "Server|Provided Services|Dependent Services|Dependent Service Component Servers|Dependent Service Component Services|Runbook Index|Site";
            OutputWriter.WriteLine(OutputHeader);
            Debug.WriteLine(OutputHeader, "information");

            foreach (Server Server in DependencyCollection.Servers)
            {
                #region Build Output

                StringBuilder Output = new StringBuilder();

                Output.Append((char)34 + Server.Name + (char)34);
                Output.Append("|");

                //////////////////////////////////
                Output.Append((char)34);
                foreach (BusinessApplication Service in Server.ProvidedBusinessApplications)
                {
                    Output.Append(Service.Name);
                    Output.Append(",");

                }
                Output.Remove(Output.Length - 1, 1);
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
                Output.Remove(Output.Length - 1, 1);
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
                Output.Remove(Output.Length - 1, 1);
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
                Output.Remove(Output.Length - 1, 1);
                Output.Append((char)34);

                //////////////////////////////////

                Output.Append("|");

                //////////////////////////////////////

                Output.Append(Server.SRMRecoveryIndex);
                //Output.Append("|");
                //Output.Append(Server.SRMPriorityGroupIndex);

                Output.Append("|");

                //Output.Append((char)34 + Server.PhysicalSiteName + (char)34);
                Output.Append((char)34 + Server.PhysicalSite.Name + (char)34);

                #endregion

                OutputWriter.WriteLine(Output);
            }

            OutputWriter.Flush();
            OutputWriter.Close();
            OutputWriter.Dispose();

        }

        public static void ExportSRMRunbook()
        {
            throw new NotImplementedException();
        }

        public static void GetServerHealth()
        {
            // This uses the TPL to perform a parallel task, that then is able to get the correct context that allows the GUI to be updated

            Debug.WriteLine("[INF1165] Determining SCOM Server Health", "information");

            Cursor.Current = Cursors.AppStarting;

            var context = TaskScheduler.FromCurrentSynchronizationContext();

            Task task = Task.Factory.StartNew(() =>
            {

                ServerUtilities SCOMUtilities = new ServerUtilities(SCOMServerName, SCSMServerName);

                foreach (Server Server in DependencyCollection.Servers)
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

        public static void Load()
        {
            #region Set Trace Listener output form size and location

            // It is important to do these first
            ISA.Helper.Properties.frmProgressLocation = Properties.Settings.Default.frmProgressLocation;
            ISA.Helper.Properties.frmProgressSize = Properties.Settings.Default.frmProgressSize;

            #endregion

            #region Trace Listener


            // Code to allow Debug.WriteLine to work across all Assemblies
            TextWriterTraceListener[] listeners = null;

            if (MainForm == null)
            {
                listeners = new TextWriterTraceListener[] {
                                                       new TextTraceListener(MDIForm.statusStrip, MDIForm, MDIForm),
                                                       new TextWriterTraceListener(Console.Out)
                                                      };
            }
            else
            {
                listeners = new TextWriterTraceListener[] {
                                                       new TextTraceListener(MainForm.statusStrip, MainForm, null),
                                                       new TextWriterTraceListener(Console.Out)
                                                      };
            }
            Debug.Listeners.AddRange(listeners);

            #endregion

            #region Setup Information form

            InformationForm = new frmInformation();

            if (Properties.Settings.Default.frmInformationSize.Width != 0 && Properties.Settings.Default.frmInformationSize.Height != 0)
            {
                InformationForm.Size = Properties.Settings.Default.frmInformationSize;
            }

            if (Properties.Settings.Default.frmInformationLocation.X != 0 && Properties.Settings.Default.frmInformationLocation.Y != 0)
            {
                InformationForm.Location = Properties.Settings.Default.frmInformationLocation;
            }

            #endregion

            ISA.Helper.Properties.AllowSaveFormLocations = true;

            #region Set Main or MDI Form location and size

            if (Properties.Settings.Default.General_MDIInterface) // MDI
            {
                // Firstly set the MDI Parent window properties
                // Location
                if (Properties.Settings.Default.frmMDIMainLocation.X != 0 && Properties.Settings.Default.frmMDIMainLocation.Y != 0)
                {
                    MDIForm.SetLocation(Properties.Settings.Default.frmMDIMainLocation);
                }
                else
                {
                    MDIForm.SetLocation(new Point(0, 0));
                }

                // Size
                if (Properties.Settings.Default.frmMDIMainSize.Width != 0 && Properties.Settings.Default.frmMDIMainSize.Height != 0)
                {
                    MDIForm.SetSize(Properties.Settings.Default.frmMDIMainSize);
                }
                else
                {
                    MDIForm.SetSize(new Size(0, 0));
                }

                // Now set the Drawings Form properties
                // Location
                if (Properties.Settings.Default.frmDrawingsLocation.X != 0 && Properties.Settings.Default.frmDrawingsLocation.Y != 0)
                {
                    DrawingsForm.SetLocation(Properties.Settings.Default.frmDrawingsLocation);
                }
                else
                {
                    DrawingsForm.SetLocation(new Point(0, 0));
                }

                // Size
                if (Properties.Settings.Default.frmDrawingsSize.Width != 0 && Properties.Settings.Default.frmDrawingsSize.Height != 0)
                {
                    DrawingsForm.SetSize(Properties.Settings.Default.frmDrawingsSize);
                }
                else
                {
                    DrawingsForm.SetSize(new Size(0, 0));
                }
            }
            else // SDI
            {
                // Location
                if (Properties.Settings.Default.frmMainLocation.X != 0 && Properties.Settings.Default.frmMainLocation.Y != 0)
                {
                    MainForm.SetLocation(Properties.Settings.Default.frmMainLocation);
                }
                else
                {
                    MainForm.SetLocation(new Point(0, 0));
                }

                // Size
                if (Properties.Settings.Default.frmMainSize.Width != 0 && Properties.Settings.Default.frmMainSize.Height != 0)
                {
                    MainForm.SetSize(Properties.Settings.Default.frmMainSize);
                }
                else
                {
                    MainForm.SetSize(new Size(0, 0));
                }
            }

            //if (Properties.Settings.Default.frmMainLocation.X != 0 && Properties.Settings.Default.frmMainLocation.Y != 0)
            //{
            //    MainForm.SetLocation(Properties.Settings.Default.frmMainLocation);
            //}
            //else
            //{
            //    if (MainForm == null)
            //    {
            //        MDIForm.SetLocation(new Point(0, 0));
            //    }
            //    else
            //    {
            //        MainForm.SetLocation(new Point(0, 0));
            //    }
            //}

            //if (Properties.Settings.Default.frmMainSize.Width != 0 && Properties.Settings.Default.frmMainSize.Height != 0)
            //{
            //    if (MainForm == null)
            //    {
            //        MDIForm.SetSize(Properties.Settings.Default.frmMainSize);
            //    }
            //    else
            //    {
            //        MainForm.SetSize(Properties.Settings.Default.frmMainSize);
            //    }
            //}
            //else
            //{
            //    if (MainForm == null)
            //    {
            //        MDIForm.SetHeight(Screen.PrimaryScreen.WorkingArea.Height);
            //    }
            //    else
            //    {
            //        MainForm.SetHeight(Screen.PrimaryScreen.WorkingArea.Height);
            //    }
            //}

            #endregion

            Debug.WriteLine("Startup complete", "information");

            if (Properties.Settings.Default.ConnectToProductionSQLUponStartup && (ISA.Helper.Properties.DependencyDatabaseRO || ISA.Helper.Properties.DependencyDatabaseRW))
            {
                Main.DoSetup();

                Main.LoadDependencyData(Properties.Settings.Default.iServer_ProductionDocumentRootID);
            }

            if (DrawingsForm != null)
            {
                DrawingsForm.Refresh();
            }
            else
            {
                MainForm.Refresh();
            }
        }

        public static void LoadDependencyData(string DocumentRootID)
        {
            //lvwDrawings.Items.Clear();

            //toolStripButtonLoadiServerProduction.Enabled = false;
            //btnClearFilter.Enabled = false;
            //btnFilter.Enabled = false;
            //txtFilter.Enabled = false;
            if (MainForm == null)
            {
                MDIForm.DisableGUI();
                DrawingsForm.DisableGUI();
            }
            else
            {
                MainForm.DisableGUI();
            }

            List<BusinessApplication> TempApplicationList = null;

            Cursor.Current = Cursors.WaitCursor;

            //if (MainForm == null)
            //{
            //    ResetApplicationState(MDIForm, DrawingsForm);
            //}
            //else
            //{
            //    ResetApplicationState(MainForm, null);
            //}
            ResetApplicationState();

            Debug.WriteLine("[INF1171] Loading Dependency Data...", "information");

            List<BusinessApplication> Applications = DataLayer.GetBusinessApplications();

            //DrawingsForm.AddBusinessApplicationsToListView();
            ListView Listview = null;

            if (MainForm == null)
            {
                Listview = DrawingsForm.lvwDrawings;
            }
            else
            {
                Listview = MainForm.lvwDrawings;
            }

            AddBusinessApplicationsToListView(Listview);

            //lvwDrawings.BeginUpdate();
            //foreach (BusinessApplication Application in _DataLayer.Collection.Applications)
            //{
            //    if (Application.InScope || Application.OverrideInScope)
            //    {
            //        if (Application.Drawing != null)
            //        {
            //            AddBusinessApplicationToListView(Application, Application.Drawing.StatusText);
            //        }
            //        else
            //        {
            //            AddBusinessApplicationToListView(Application, "N/A");
            //        }
            //    }
            //}
            //lvwDrawings.EndUpdate();

            //this.Refresh();
            Listview.Refresh();
            DependencyCollection = DataLayer.Collection;

            // Relationships and HA Relationships have now been determined.
            // Time to work out the resultant Stream for each relationship
            //Debug.WriteLine("...[INF1172] Determining resultant Stream...", "information");
            //Application.DoEvents();
            TempApplicationList = DependencyCollection.Applications;

            #region Over-ride InScope

            // If the user has decided to also show Applications that are not in scope, AND there are no Applications in this list, prompt now
            if (!Properties.Settings.Default.ShowInScopeServicesOnly && _OutOfScopeApplicationsToShow.Count == 0)
            {
                frmShowNotInScopeServices ServicesNotInScopeForm = null;

                if (MainForm == null)
                {
                    ServicesNotInScopeForm = new frmShowNotInScopeServices(DependencyCollection.Applications, MDIForm);
                }
                else
                {
                    ServicesNotInScopeForm = new frmShowNotInScopeServices(DependencyCollection.Applications, MainForm);
                }

                ServicesNotInScopeForm.ShowDialog();

                Debug.WriteLine("[INF1173] Recalculating dependencies...", "information");

                foreach (BusinessApplication Application in DependencyCollection.Applications)
                {
                    if (Application.OverrideInScope)
                    {
                        string DrawingStatusText = "Unknown";

                        if (Application.Drawing != null)
                        {
                            DrawingStatusText = "N/A";
                        }
                        else
                        {
                            DrawingStatusText = Application.Drawing.Status.ToString();
                        }

                        // Add this Application to the main list
                        if (MainForm == null)
                        {
                            AddBusinessApplicationToListView(Application, DrawingStatusText, DrawingsForm.lvwDrawings);
                        }
                        else
                        {
                            AddBusinessApplicationToListView(Application, DrawingStatusText, MainForm.lvwDrawings);
                        }

                        if (Properties.Settings.Default.ShowDebugInformation)
                        {
                            Debug.WriteLine("..." + Application.Name, "information");
                        }

                        for (int i = 0; i < Application.Relationships.Count; i++)
                        {
                            Relationship r = Application.Relationships[i];

                            if (!DependencyCollection.AddRelationship(ref r))
                            {
                                if (r.ToType == ISA.Dependency.Relationship.ComponentType.BusinessApplication)
                                {
                                    AddWarning("[ERR1030] " + Application.Name + " (Business Application): Could not add relationship with " + r.ToBusinessApplication.Name + " (Business Application)");
                                }
                                else
                                {
                                    AddWarning("[ERR1031] " + Application.Name + " (Business Application): Could not add relationship with " + r.ToServer.Name + " (Server)");
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            Debug.WriteLine("[INF1174] Adding Not In Scope Business Applications to Business Application List...", "information");
            AddOutOfScopeApplications();

            Debug.WriteLine("[INF1175] Determining Business Application Stream for Stream Dependencies...", "information");
            System.Windows.Forms.Application.DoEvents();
            TempApplicationList = DependencyCollection.Applications;
            DependencyCollection.SetApplicationRecoveryStream(ref TempApplicationList);

            Debug.WriteLine("[INF1176] Adding Stream dependencies...", "information");
            AddStreamDependencies();

            // Sort the runbook ** THIS CAN ONLY BE DONE ONCE!!
            SortRunbook();

            // Don't need a status update here as it is performed within the called Method
            BuildRunbooks();
            CalculateRunbookOrder();

            Debug.WriteLine("[INF1177] Determining Business Application Stream for Runbook...", "information");
            System.Windows.Forms.Application.DoEvents();
            DependencyCollection.SetApplicationRecoveryStream(ref BusinessApplicationRunbook);

            ReOrderApplications();

            // Rebuild the runbooks as the order has changed
            //BuildRunbooks();

            // Recalculate the Runbook order
            CalculateRunbookOrder();

            // Don't need a status update here as it is performed within the called Method
            DetermineAllBusinessApplicationState(); // <-- Also updates Drawing Status

            // If appropriate, load additional vCenter data (Storage, CPU, RAM etc)
            GetvCenterData();

            // Determine each server's Recovery tasks
            Debug.WriteLine("[INF1178] Calculating Server Restore tasks...", "information");
            System.Windows.Forms.Application.DoEvents();

            CalculateAllServerRecoveryTasks();

            // Determine each Service's Recovery tasks
            Debug.WriteLine("[INF1179] Calculating Business Application Restore tasks...", "information");
            System.Windows.Forms.Application.DoEvents();

            DetermineAllBusinessApplicationRecoveryTasks();

            // Determine all Management Pack information
            if (Properties.Settings.Default.EnableSystemCenterFunctions)
            {
                GetManagementPackInformation();
            }

            // Run Drawing Validation Tests
            DoValidationTests();

            // If appropriate, enable the Create MP functionality
            UpdateSystemCenterAuthorStatus();

            // Enable menu items to provide full functionality
            //MainForm.EnableToolstripAndMenuItems();

            // Update the Drawing list, taking into account any Validation errors...
            if (MainForm == null)
            {
                DrawingsForm.FilterList();
            }
            else
            {
                MainForm.FilterList();
            }


            Listview.EndUpdate();
            Listview.Refresh();

            Debug.WriteLine("[INF1180] Load complete", "information");
            Mark();

            // Enable retrieving iServer update notifications
            //tmrUpdates.Enabled = true;
            if (MainForm == null)
            {
                MDIForm.EnableGUI();
                DrawingsForm.EnableGUI();
            }
            else
            {
                MainForm.EnableGUI();
            }


            //btnClearFilter.Enabled = true;
            //btnFilter.Enabled = true;
            //txtFilter.Enabled = true;

            Cursor.Current = Cursors.Default;
        }

        public static bool LoadSettings(string RemoteFilename)
        {
            bool Result = false;
            string LastLocalFilename = Properties.Settings.Default.LocalSettingsFilename;
            string ThisApplicationAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ISA Technologies\\DR Planning Tool";
            string LocalFilename = System.IO.Path.Combine(ThisApplicationAppDataPath, System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName()));

            // Create AppData path specific to the app.
            System.IO.Directory.CreateDirectory(ThisApplicationAppDataPath);

            // Copy to the local PC
            try
            {
                if (System.IO.File.Exists(RemoteFilename))
                {

                    if (System.IO.File.Exists(LastLocalFilename + ".mdf"))
                    {
                        try
                        {
                            // Remove the previous copy of the data and log file
                            System.IO.File.Delete(LastLocalFilename + ".mdf");
                            System.IO.File.Delete(LastLocalFilename + "_log.ldf");

                            if (Properties.Settings.Default.ShowDebugInformation)
                            {
                                Debug.WriteLine("[INF1240] Local config database file(s) deleted successfully", "information");
                            }
                        }
                        catch (Exception)
                        {
                            AddException("[ERR1241] Could not delete local config database file(s)");
                        }
                    }

                    try
                    {
                        // Copy the remote data file to the local PC
                        System.IO.File.Copy(RemoteFilename, LocalFilename + ".mdf");

                        if (Properties.Settings.Default.ShowDebugInformation)
                        {
                            Debug.WriteLine("[INF1242] Config database file copied successfully", "information");
                        }
                    }
                    catch (Exception)
                    {
                        AddException("[ERR1243] Could not copy Config database to local PC");
                    }
                }
            }
            catch (Exception)
            {
                AddException("[ERR1244] Unexpected error copying Config database to local PC");
            }

            if (System.IO.File.Exists(LocalFilename + ".mdf"))
            {

                string ConnectionString = ""; // "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + LocalFilename + ".mdf;Integrated Security=True;Connect Timeout=30";
                                              //string ConnectionString = "Data Source=(LocalDB)\v11.0;AttachDbFilename=" + LocalFilename + ".mdf;Integrated Security=True;Connect Timeout=30";
                string Query = "";

                ConnectionString = Properties.Settings.Default.LocalDBConnectionString.Replace("#LOCALDBFILENAME#", LocalFilename);

                try
                {
                    // Load settings...
                    SQLServer ConfigDatabase = new SQLServer(30); // 30 second timeout

                    ConfigDatabase.OpenConnection(ConnectionString);

                    // System settings
                    Query = "SELECT * FROM SystemConfig";

                    DataTable SystemResults = ConfigDatabase.Execute(Query);

                    if (SystemResults != null && SystemResults.Rows.Count > 0)
                    {
                        // Now go through each returned row and replace the local configuration settings with whatever is in the database

                        foreach (DataRow Row in SystemResults.Rows)
                        {
                            string ID = Row[0].ToString();
                            string Name = Row[1].ToString();  // Get Property name
                            string Type = Row[2].ToString();  // Get Property type
                            string Value = Row[3].ToString(); // Get Property value

                            // Get corresponding Property
                            SettingsPropertyValue prop = Properties.Settings.Default.PropertyValues[Name];

                            // Set the property with the value from the database
                            switch (Type)
                            {
                                case "String":
                                    prop.PropertyValue = Value;
                                    break;
                                case "Integer":
                                    prop.PropertyValue = Convert.ToInt32(Value);
                                    break;
                                case "Time":
                                    prop.PropertyValue = Convert.ToDateTime(Value).TimeOfDay;
                                    break;
                                case "Boolean":
                                    prop.PropertyValue = Convert.ToBoolean(Value);
                                    break;
                                default:
                                    Console.WriteLine("Unsupported value!");
                                    break;
                            }
                        }
                    }

                    ConfigDatabase.CloseConnection();
                    ConfigDatabase = null;

                    Result = true;
                }
                catch (Exception)
                {
                    AddException("[ERR1193] Error loading settings from " + RemoteFilename);
                }
            }
            else
            {
                AddException("[ERR1245] Could not find local Config Database");
            }

            Properties.Settings.Default.LocalSettingsFilename = LocalFilename;
            Properties.Settings.Default.Save();

            return Result;
        }

        public static void Mark()
        {
            Debug.WriteLine("======= " + DateTime.Now.ToString() + " ================================================================\n", "information");
        }

        public static void RemoveBusinessApplicationDependencyMapFormFromList(frmDependencyMap BusinessApplicationDependencyMapForm)
        {
            _DependencyMapForms.Remove(BusinessApplicationDependencyMapForm);
        }

        public static void RemoveBusinessApplicationFormFromList(frmBusinessApplication BusinessApplicationForm)
        {
            BusinessApplicationForms.Remove(BusinessApplicationForm);
        }

        public static void RemoveBusinessApplicationImageFormFromList(frmBusinessApplicationImage BusinessApplicationImageForm)
        {
            _BusinessApplicationImageForms.Remove(BusinessApplicationImageForm);
        }

        public static void RemoveOutOfScopeServer(Server Server)
        {
            InformationForm.RemoveOutOfScopeServer(Server);
            ISA.Helper.Properties.ReloadNecessary = true;
        }

        public static void RemoveOutOfScopeApplication(BusinessApplication Application)
        {
            InformationForm.RemoveOutOfScopeApplication(Application);
            ISA.Helper.Properties.ReloadNecessary = true;
        }

        public static void RemoveRunbookFormFromList(frmList RunbookForm)
        {
            _ListForms.Remove(RunbookForm);
        }

        public static void RemoveServerFormFromList(frmServer ServerForm)
        {
            _ServerForms.Remove(ServerForm);
        }

        public static void ResetApplicationState()
        {
            CloseListFormWindows();
            CloseServerFormWindows();
            CloseBusinessApplicationFormWindows();
            CloseBusinessApplicationImageFormWindows();
            CloseDependencyMapFormWindows();
            //pictureBox.Image = null;
            //pictureBox.BackgroundImage = null;

            ClearWarnings();

            DependencyCollection = new Collection();
            ServerRunbook = null;
            BusinessApplicationRunbook = null;

            DataLayer.Collection = new Collection();
            DataLayer.Servers.Clear();
            DataLayer.Applications.Clear();

            ISA.Helper.Properties.ManagementPackAuthor = false;
            ISA.Helper.Properties.ReloadNecessary = false;

            if (MainForm == null)
            {
                MDIForm.DisableGUI();
                DrawingsForm.Reset();
            }
            else
            {
                MainForm.DisableGUI();
                MainForm.Reset();
            }

            //toolStripButtonCreateManagementPacks.Enabled = false;
            //toolStripButtonResetServerStates.Enabled = false;
            //toolStripButtonGetServerHealth.Enabled = false;

            //detailsToolStripMenuItem.Enabled = false;
            //sRMRunbookToolStripMenuItem1.Enabled = false;
            //serverListToolStripMenuItem.Enabled = false;
            //serverRunbookToolStripMenuItem.Enabled = false;
            //serviceRunbookToolStripMenuItem.Enabled = false;
            //sRMRunbookToolStripMenuItem.Enabled = false;
            //resetServerStatesToolStripMenuItem.Enabled = false;
            //retrieveSCOMServerHealthToolStripMenuItem.Enabled = false;
            //recoveryEstimatesToolStripMenuItem.Enabled = false;
            //DependencyMapToolStripMenuItem.Enabled = false;
            //simulationToolStripMenuItem.Enabled = false;



            //lvwDrawings.Items.Clear();

            //MainForm.Refresh();
        }

        public static void ResetBusinessApplicationShowInRunbook()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (DependencyCollection.Applications != null)
            {
                foreach (BusinessApplication BusinessApplication in DependencyCollection.Applications) // _BusinessApplicationRunbook)
                {
                    BusinessApplication.Save(DependencyDatabase);
                }
            }

            Cursor.Current = Cursors.Default;
        }

        public static void ResetServerShowInRunbook()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (DependencyCollection.Servers != null)
            {
                foreach (Server Server in DependencyCollection.Servers)
                {
                    Server.Save(DependencyDatabase);
                }
            }

            Cursor.Current = Cursors.Default;
        }

        public static void ResetServerStates()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (DependencyCollection.Servers != null)
            {

                foreach (Server Server in DependencyCollection.Servers)
                {
                    Server.SystemStateIsEmulated = false;
                }

                //_EmulatedData = false;
                //toolStripStatusLabelData.Text = "Actual Data";

                if (MainForm == null)
                {
                    DetermineAllBusinessApplicationState(DrawingsForm.lvwDrawings);
                }
                else
                {
                    DetermineAllBusinessApplicationState(MainForm.lvwDrawings);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        public static void SetEmulatedLabel(string Text)
        {
            //toolStripStatusLabelData.Text = Text;
        }

        public static void DoToolsOptions()
        {
            bool OriginalEmulatedState = ISA.Helper.Properties.ShowEmulatedState;
            bool OriginalShowDrawingStatusOnServiceValue = Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication;
            bool OriginalShowDrawingLessServicesValue = Properties.Settings.Default.ShowDrawingLessBusinessApplications;
            bool OriginalShowDrawingStateColumnValue = Properties.Settings.Default.ShowDrawingStatusColumn;
            bool OriginalSystemCenterUsageValue = Properties.Settings.Default.EnableSystemCenterFunctions;
            bool OriginalInScopeValue = Properties.Settings.Default.ShowInScopeServicesOnly;
            bool OriginalMDIOption = Properties.Settings.Default.General_MDIInterface;

            string OriginalStreamAServiceDependency = Properties.Settings.Default.Restore_StreamAServicePrerequisite;
            string OriginalStreamBServiceDependency = Properties.Settings.Default.Restore_StreamBServicePrerequisite;
            string OriginalStreamCServiceDependency = Properties.Settings.Default.Restore_StreamCServicePrerequisite;
            string OriginalStreamDServiceDependency = Properties.Settings.Default.Restore_StreamDServicePrerequisite;
            string OriginalStreamEServiceDependency = Properties.Settings.Default.Restore_StreamEServicePrerequisite;

            bool RebuildRunbooks = false;
            bool RestartRequired = false;

            frmOptions OptionsForm = null;

            if (DependencyCollection == null)
            {
                OptionsForm = new frmOptions(MainForm, null, null); //_BusinessApplicationRunbook);
            }
            else
            {
                OptionsForm = new frmOptions(MainForm, DependencyCollection.Applications, DependencyCollection.Servers); //_BusinessApplicationRunbook);
            }

            OptionsForm.ShowDialog();
            OptionsForm.Dispose();
            OptionsForm = null;

            ISA.Helper.Properties.ShowDebugInformation = Properties.Settings.Default.ShowDebugInformation;

            if (OriginalMDIOption != Properties.Settings.Default.General_MDIInterface)
            {
                RestartRequired = true;
            }

            if (MainForm == null)
            {
                DrawingsForm.lvwDrawings.CheckBoxes = Properties.Settings.Default.ShowCheckboxesOnServiceList;
            }
            else
            {
                MainForm.lvwDrawings.CheckBoxes = Properties.Settings.Default.ShowCheckboxesOnServiceList;
            }

            if (OriginalShowDrawingLessServicesValue != Properties.Settings.Default.ShowDrawingLessBusinessApplications)
            {
                LoadDependencyData(Properties.Settings.Default.iServer_ProductionDocumentRootID);
            }

            if (OriginalShowDrawingStatusOnServiceValue != Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication)
            {
                //ResetServerStates();
            }

            if ((OriginalSystemCenterUsageValue != Properties.Settings.Default.EnableSystemCenterFunctions) && (Properties.Settings.Default.EnableSystemCenterFunctions))
            {
                UpdateSystemCenterAuthorStatus();
                ISA.Helper.Properties.SystemCenterFunctionsEnabled = Properties.Settings.Default.EnableSystemCenterFunctions;

                //retrieveSCOMServerHealthToolStripMenuItem.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
                //toolStripButtonGetServerHealth.Enabled = Properties.Settings.Default.EnableSystemCenterFunctions;
                MainForm.EnableSystemCenterToolStrip();
            }

            //DependencyMapToolStripMenuItem.Visible = true;
            if (MainForm == null)
            {
                MDIForm.EnableDependencyMapToolstrip();
            }
            else
            {
                MainForm.EnableDependencyMapToolstrip();
            }

            bool ResultantEmulatedState = ISA.Helper.Properties.ShowEmulatedState;

            // Check to see if the emulation state has changed
            if (OriginalEmulatedState != ResultantEmulatedState)
            {
                // Yes, it has changed - update all services to reflect the new state
                //DetermineAllBusinessApplicationState();
            }

            if (ISA.Helper.Properties.ShowEmulatedState)
            {
                //StateLabel.Text = "State: Emulated";
                //StateLabel.ForeColor = Color.Red;
            }
            else
            {
                //StateLabel.Text = "State: Actual";
                //StateLabel.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            }

            if (OriginalShowDrawingStateColumnValue != Properties.Settings.Default.ShowDrawingStatusColumn)
            {
                if (Properties.Settings.Default.ShowDrawingStatusColumn)
                {
                    if (MainForm == null)
                    {
                        DrawingsForm.lvwDrawings.Columns.Add("Status");
                        DrawingsForm.lvwDrawings.Columns[0].Width = 200;
                        DrawingsForm.lvwDrawings.Columns[1].Width = 80;
                    }
                    else
                    {
                        MainForm.lvwDrawings.Columns.Add("Status");
                        MainForm.lvwDrawings.Columns[0].Width = 200;
                        MainForm.lvwDrawings.Columns[1].Width = 80;
                    }
                }
                else
                {
                    if (MainForm == null)
                    {
                        DrawingsForm.lvwDrawings.Columns.RemoveAt(1);
                        DrawingsForm.lvwDrawings.Columns[0].Width = 280;
                    }
                    else
                    {
                        MainForm.lvwDrawings.Columns.RemoveAt(1);
                        MainForm.lvwDrawings.Columns[0].Width = 280;
                    }
                }
            }

            // Check if the Stream dependencies have changed, and if so, rebuild the runbooks
            if (
                (OriginalStreamAServiceDependency != Properties.Settings.Default.Restore_StreamAServicePrerequisite) ||
                (OriginalStreamBServiceDependency != Properties.Settings.Default.Restore_StreamBServicePrerequisite) ||
                (OriginalStreamCServiceDependency != Properties.Settings.Default.Restore_StreamCServicePrerequisite) ||
                (OriginalStreamDServiceDependency != Properties.Settings.Default.Restore_StreamDServicePrerequisite) ||
                (OriginalStreamEServiceDependency != Properties.Settings.Default.Restore_StreamEServicePrerequisite)
                )
            {
                RebuildRunbooks = true;

                ISA.Helper.Properties.StreamAServiceName = Properties.Settings.Default.Restore_StreamAServicePrerequisite;
                ISA.Helper.Properties.StreamBServiceName = Properties.Settings.Default.Restore_StreamBServicePrerequisite;
                ISA.Helper.Properties.StreamCServiceName = Properties.Settings.Default.Restore_StreamCServicePrerequisite;
                ISA.Helper.Properties.StreamDServiceName = Properties.Settings.Default.Restore_StreamDServicePrerequisite;
                ISA.Helper.Properties.StreamEServiceName = Properties.Settings.Default.Restore_StreamEServicePrerequisite;
            }

            // Check if the Show In Scope Services option has changed, and if so, rebuild the Runbooks
            if (OriginalInScopeValue != Properties.Settings.Default.ShowInScopeServicesOnly)
            {
                RebuildRunbooks = true;

                if (!Properties.Settings.Default.ShowInScopeServicesOnly && DependencyCollection != null)
                {
                    frmShowNotInScopeServices ServicesNotInScopeForm = null;

                    if (MainForm == null)
                    {
                        ServicesNotInScopeForm = new frmShowNotInScopeServices(DependencyCollection.Applications, DrawingsForm);
                    }
                    else
                    {
                        ServicesNotInScopeForm = new frmShowNotInScopeServices(DependencyCollection.Applications, MainForm);
                    }

                    ServicesNotInScopeForm.ShowDialog();
                }
            }

            if (RebuildRunbooks || RestartRequired)
            {
                // for now we will just restart the app as the topo sort object is cleared, and we don't want to have to rebuild it
                // from scratch

                MessageBox.Show("The DR Planning Tool must now be restarted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                Process.Start(Application.ExecutablePath);

                Application.Exit();
            }
        }

        public static void ViewBusinessApplicationRunbook()
        {
            frmList ListForm = null;

            if (MainForm == null)
            {
                ListForm = new frmList("Business Application Runbook", ServerRunbook, DependencyCollection.Servers, BusinessApplicationRunbook, MDIForm);

                ListForm.Owner = MDIForm;
                ListForm.MdiParent = MDIForm;

                _ListForms.Add(ListForm);
                ListForm.Show();
            }
            else
            {
                ListForm = new frmList("Business Application Runbook", ServerRunbook, DependencyCollection.Servers, BusinessApplicationRunbook, MainForm);

                ListForm.Owner = MainForm;

                _ListForms.Add(ListForm);
                ListForm.Show(MainForm);
            }
        }

        public static void ViewServerList()
        {
            frmList ListForm = null;

            if (MainForm == null)
            {
                ListForm = new frmList("Server List", ServerRunbook, DependencyCollection.Servers, BusinessApplicationRunbook, MDIForm);

                ListForm.Owner = MDIForm;
                ListForm.MdiParent = MDIForm;

                _ListForms.Add(ListForm);
                ListForm.Show();
            }
            else
            {
                ListForm = new frmList("Server List", ServerRunbook, DependencyCollection.Servers, BusinessApplicationRunbook, MainForm);
                ListForm.Owner = MainForm;

                _ListForms.Add(ListForm);
                ListForm.Show(MainForm);
            }

        }

        public static void ViewServerRunbook()
        {
            frmList ListForm = null;

            if (MainForm == null)
            {
                ListForm = new frmList("Server Runbook", ServerRunbook, DependencyCollection.Servers, BusinessApplicationRunbook, MDIForm);

                ListForm.Owner = MDIForm;
                ListForm.MdiParent = MDIForm;

                _ListForms.Add(ListForm);
                ListForm.Show();
            }
            else
            {
                ListForm = new frmList("Server Runbook", ServerRunbook, DependencyCollection.Servers, BusinessApplicationRunbook, MainForm);

                ListForm.Owner = MainForm;

                _ListForms.Add(ListForm);
                ListForm.Show(MainForm);
            }

        }

        public static void ViewSRMRunbook()
        {
            frmList ListForm = null;

            if (MainForm == null)
            {
                ListForm = new frmList("SRM Runbook", ServerRunbook, DependencyCollection.Servers, BusinessApplicationRunbook, MDIForm);

                ListForm.Owner = MDIForm;
                ListForm.MdiParent = MDIForm;

                _ListForms.Add(ListForm);
                ListForm.Show();
            }
            else
            {
                ListForm = new frmList("SRM Runbook", ServerRunbook, DependencyCollection.Servers, BusinessApplicationRunbook, MainForm);

                ListForm.Owner = MainForm;

                _ListForms.Add(ListForm);
                ListForm.Show(MainForm);
            }

        }

        #endregion

        #region Private Methods

        private static void CalculateAllServerRecoveryTasks()
        {
            foreach (Server Server in DependencyCollection.Servers)
            {
                Server.CalculateRecoveryTasks();
            }
        }

        private static void CalculateRunbookOrder()
        {
            Debug.WriteLine("[INF1163] Calculating Business Application Runbook order...", "information");
            Application.DoEvents();

            List<BusinessApplication> r = BusinessApplicationRunbook;
            DependencyCollection.CalculateRunbookRegions(ref r);
            BusinessApplicationRunbook = r;

            Debug.WriteLine("[INF1164] Determining Upstream Dependencies...", "information");
            Application.DoEvents();
            for (int i = 0; i < ServerRunbook.Count; i++)
            {
                Server s = ServerRunbook[i];
                DependencyCollection.DetermineUpstreamApplications(ref s);
                ServerRunbook[i] = s;
            }

            //foreach (Server Server in _ServerRunbook)
            //{
            //    Debug.WriteLine (Server.Name + " has " + Server.DependentBusinessApplications.Count);
            //    Server s = Server;
            //    _DependencyCollection.DetermineUpstreamServices(ref s);
            //    Debug.WriteLine(" ==> " + s.Name + " has " + s.DependentBusinessApplications.Count);
            //}

            //Debug.WriteLine("Calculating Service Runbook order...");
            //Application.DoEvents();
            //_DependencyCollection.CalculateServiceRunbook(_BusinessApplicationRunbook);

            #region Determine boundaries per Stream

            //if (false) // Placeholder to allow this to be controlled via Tools | Options
            //{
            //    // Find the index of the first Service for each stream

            //    for (RunbookIndex = 0; RunbookIndex < _BusinessApplicationRunbook.Count; RunbookIndex++)
            //    {
            //        BusinessApplication Service = _BusinessApplicationRunbook[RunbookIndex];

            //        if (Service.CalculatedStream == Global.RecoveryStream.A)
            //        {
            //            StreamAFirstIndex = RunbookIndex;
            //            break;
            //        }
            //    }

            //    for (RunbookIndex = 0; RunbookIndex < _BusinessApplicationRunbook.Count; RunbookIndex++)
            //    {
            //        BusinessApplication Service = _BusinessApplicationRunbook[RunbookIndex];

            //        if (Service.CalculatedStream == Global.RecoveryStream.B)
            //        {
            //            StreamBFirstIndex = RunbookIndex;
            //            break;
            //        }
            //    }

            //    for (RunbookIndex = 0; RunbookIndex < _BusinessApplicationRunbook.Count; RunbookIndex++)
            //    {
            //        BusinessApplication Service = _BusinessApplicationRunbook[RunbookIndex];

            //        if (Service.CalculatedStream == Global.RecoveryStream.C)
            //        {
            //            StreamCFirstIndex = RunbookIndex;
            //            break;
            //        }
            //    }

            //    for (RunbookIndex = 0; RunbookIndex < _BusinessApplicationRunbook.Count; RunbookIndex++)
            //    {
            //        BusinessApplication Service = _BusinessApplicationRunbook[RunbookIndex];

            //        if (Service.CalculatedStream == Global.RecoveryStream.D)
            //        {
            //            StreamDFirstIndex = RunbookIndex;
            //            break;
            //        }
            //    }

            //    for (RunbookIndex = 0; RunbookIndex < _BusinessApplicationRunbook.Count; RunbookIndex++)
            //    {
            //        BusinessApplication Service = _BusinessApplicationRunbook[RunbookIndex];

            //        if (Service.CalculatedStream == Global.RecoveryStream.E)
            //        {
            //            StreamEFirstIndex = RunbookIndex;
            //            break;
            //        }
            //    }
            //}

            #endregion

            //if (false) // Placeholder to allow this to be controlled via Tools | Options
            //{
            //    RunbookIndex = 0;

            //    Debug.WriteLine("Reordering Dependent-less Stream A Services...");
            //    // Loop through the runbooks, taking account of the stream, and move each Service appropriately
            //    for (RunbookIndex = 0; RunbookIndex < _BusinessApplicationRunbook.Count; RunbookIndex++)
            //    {
            //        BusinessApplication Service = _BusinessApplicationRunbook[RunbookIndex];

            //        if (Service.Stream == Global.RecoveryStream.A)
            //        {
            //            // Does it have any Dependents?

            //            if (Service.DependentBusinessApplications.Count == 0)
            //            {
            //                // No Dependents, so move it to the bottom of the list
            //                if (Properties.Settings.Default.ShowDebugInformation)
            //                {
            //                    Debug.WriteLine("..." + Service.Name);
            //                }
            //                BusinessApplication ServiceToMove = _BusinessApplicationRunbook[RunbookIndex];

            //                _BusinessApplicationRunbook.RemoveAt(RunbookIndex);

            //                _BusinessApplicationRunbook.Insert(0, ServiceToMove);
            //            }
            //        }
            //    }

            //}
        }

        public static void CloseBusinessApplicationFormWindows()
        {
            while (BusinessApplicationForms.Count > 0)
            {
                BusinessApplicationForms[0].Close();
            }

            BusinessApplicationForms.Clear();
        }

        public static void CloseBusinessApplicationImageFormWindows()
        {
            while (_BusinessApplicationImageForms.Count > 0)
            {
                _BusinessApplicationImageForms[0].Close();
            }

            _BusinessApplicationImageForms.Clear();
        }

        private static void CloseDependencyMapFormWindows()
        {
            while (_DependencyMapForms.Count > 0)
            {
                _DependencyMapForms[0].Close();
            }

            _DependencyMapForms.Clear();
        }

        public static void CloseListFormWindows()
        {
            while (_ListForms.Count > 0)
            {
                _ListForms[0].Close();
            }

            _ListForms.Clear();
        }

        public static void CloseServerFormWindows()
        {
            while (_ServerForms.Count > 0)
            {
                _ServerForms[0].Close();
            }

            _ServerForms.Clear();
        }

        public static void DetermineAllBusinessApplicationState()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (MainForm == null)
            {
                DrawingsForm.BeginUpdate();
            }
            else
            {
                MainForm.BeginUpdate();
            }

            if (Properties.Settings.Default.ShowDebugInformation)
            {
                Debug.WriteLine("[INF1198] Determining Business Application State...", "information");
            }

            // The Business Application Runbook is already in an order that ensures that dependencies are processed correctly
            foreach (BusinessApplication BusinessApplication in BusinessApplicationRunbook) //_DC.Services
            {
                // If the state hasn't been set explicitly at the Service Level (it should be a combination of the states of it's Components),
                // then work out it's state.
                if (!BusinessApplication.StateSetAtApplicationLevel)
                {
                    if (MainForm == null)
                    {
                        Main.DetermineBusinessApplicationState(BusinessApplication, DrawingsForm.lvwDrawings);
                    }
                    else
                    {
                        Main.DetermineBusinessApplicationState(BusinessApplication, MainForm.lvwDrawings);
                    }
                }
            }

            // Now we have to ensure that the State of any Service that isn't in the ServiceRunbook is also calculated
            foreach (BusinessApplication App in DependencyCollection.Applications)
            {
                if (Global.GetExistingBusinessApplication(App, BusinessApplicationRunbook) == null)
                {
                    if (MainForm == null)
                    {
                        Main.DetermineBusinessApplicationState(App, DrawingsForm.lvwDrawings);
                    }
                    else
                    {
                        Main.DetermineBusinessApplicationState(App, MainForm.lvwDrawings);
                    }
                }
            }

            UpdateChildForms();

            if (MainForm == null)
            {
                DrawingsForm.EndUpdate();
            }
            else
            {
                MainForm.EndUpdate();
            }

            Cursor.Current = Cursors.Default;

            Application.DoEvents();
        }

        private static void DetermineBusinessApplicationInFocus(ref BusinessApplication BusinessApplication, ListView Listview)
        {
            // Find matching node in the ListView...
            ListViewItem BusinessApplicationItem = Listview.FindItemWithText(BusinessApplication.Name);

            if (BusinessApplicationItem != null)
            {
                BusinessApplicationItem.ImageIndex = Global.GetBusinessApplicationStateImageIndex(BusinessApplication, Properties.Settings.Default.ShowDrawingStatusOnBusinessApplication);
            }
        }

        private static void BuildRunbooks()
        {
            Debug.WriteLine("[INF1162] Building Runbooks...", "information");
            Application.DoEvents();

            ServerRunbook = DependencyCollection.ServerRunbook();
            BusinessApplicationRunbook = DependencyCollection.BusinessApplicationRunbook();

            // If any Services do not have any Servers, then they are not shown in the Services Runbook.
            // Correct this problem now by adding these Services to the end of the Services Runbook

            foreach (BusinessApplication BusinessApplication in DependencyCollection.Applications)
            {
                if (BusinessApplication.ComponentServers.Count == 0)
                {
                    if (Global.GetExistingBusinessApplication(BusinessApplication.Name, DependencyCollection.Applications) == null)
                    {
                        if (BusinessApplication.InScope || BusinessApplication.OverrideInScope)
                        {
                            BusinessApplicationRunbook.Add(BusinessApplication);
                        }
                    }
                }
            }
        }

        private static void DoValidationTests()
        {
            TimeSpan CumulativeTime = new TimeSpan();
            bool StreamAAdded = false;
            bool StreamBAdded = false;
            bool StreamCAdded = false;
            bool StreamDAdded = false;
            bool StreamEAdded = false;
            bool InvalidStreamTierCombination = false;
            List<Server> ServersAdded = new List<Server>();
            TimeSpan RecoveryTime = TimeSpan.Zero;
            ISA.Dependency.Global.RecoveryTier CurrentTier = Global.RecoveryTier.Undefined;
            ISA.Dependency.Global.RecoveryTier PreviousTier = Global.RecoveryTier.Undefined;
            BusinessApplication PreviousApplication = null;
            ServerUtilities SCOMUtilities = new ServerUtilities(SCOMServerName, SCSMServerName);

            Debug.WriteLine("[INF1189] Running validation tests...", "information");

            // Get any existing errors raised by the Dependency class and add those here
            foreach (String Error in DependencyCollection.Errors)
            {
                AddDrawingException(Error);
            }

            DependencyCollection.Errors.Clear();

            if (Properties.Settings.Default.Restore_OverallOverheadTime.TotalSeconds > 0)
            {
                CumulativeTime += Properties.Settings.Default.Restore_OverallOverheadTime;
            }

            foreach (BusinessApplication Application in DependencyCollection.Applications)
            {
                InvalidStreamTierCombination = false;

                #region Check for a matching Drawing

                if (Application.InScope && Application.Drawing == null)
                {
                    AddDrawingException("[ERR1101] " + Application.Name + " (Business Application) is In Scope, but does not have a Drawing", Application);
                }

                #endregion

                #region Check the Application has Components

                if (Application.InScope && Application.ComponentBusinessApplications.Count == 0 && Application.ComponentServers.Count == 0)
                {
                    AddDrawingException("[ERR1102] " + Application.Name + " (Business Application) is In Scope, but does not have any Components", Application);
                }

                #endregion

                #region Check for a valid Recovery Stream

                if (Application.InScope && (Application.CalculatedStream == Global.RecoveryStream.Undefined))
                {
                    AddDrawingException("[ERR1103] " + Application.Name + " (Business Application) is In Scope, but a Recovery stream could not be determined", Application);
                }

                #endregion

                #region Check Stream A has HA Components

                if (Application.CalculatedStream == Global.RecoveryStream.A && Application.HARelationships.Count == 0)
                {
                    AddDrawingException("[ERR1104] " + Application.Name + " (Business Application) is Stream A, but has no HA components", Application);
                }

                #endregion

                #region Check HA components have a matching peer between sites

                if (Application.HARelationships.Count > 0)
                {
                    bool FoundProtectedSite = false;
                    bool FoundRecoverySite = false;

                    foreach (HARelationship Relationship in Application.HARelationships)
                    {
                        if (Relationship.Server1.PhysicalSite.SiteType == PhysicalSite.PhysicalSiteType.Protected || Relationship.Server2.PhysicalSite.SiteType == PhysicalSite.PhysicalSiteType.Protected)
                        {
                            FoundProtectedSite = true;
                        }

                        if (Relationship.Server1.PhysicalSite.SiteType == PhysicalSite.PhysicalSiteType.Recovery || Relationship.Server2.PhysicalSite.SiteType == PhysicalSite.PhysicalSiteType.Recovery)
                        {
                            FoundRecoverySite = true;
                        }
                    }

                    if (!FoundProtectedSite || !FoundRecoverySite)
                    {
                        AddDrawingException("[ERR1105] HA relationships of " + Application.Name + " (Business Application) do not include both recovery and protected site located components, so HA is ineffective", Application);
                    }
                }

                #endregion

                #region Check Tier / Stream combination

                if (Application.InScope)
                {
                    switch (Application.CalculatedStream)
                    {
                        case Global.RecoveryStream.None1:
                        case Global.RecoveryStream.None2:
                        case Global.RecoveryStream.None3:
                        case Global.RecoveryStream.None4:
                        case Global.RecoveryStream.A:
                            switch (Application.DesiredTier)
                            {
                                case Global.RecoveryTier.Zero:
                                    if (!Properties.Settings.Default.Rule_StreamATier0)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.One:
                                    if (!Properties.Settings.Default.Rule_StreamATier1)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Two:
                                    if (!Properties.Settings.Default.Rule_StreamATier2)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Three:
                                    if (!Properties.Settings.Default.Rule_StreamATier3)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Four:
                                    if (!Properties.Settings.Default.Rule_StreamATier4)
                                        InvalidStreamTierCombination = true;
                                    break;
                            }
                            break;
                        case Global.RecoveryStream.B:
                            switch (Application.DesiredTier)
                            {
                                case Global.RecoveryTier.Zero:
                                    if (!Properties.Settings.Default.Rule_StreamBTier0)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.One:
                                    if (!Properties.Settings.Default.Rule_StreamBTier1)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Two:
                                    if (!Properties.Settings.Default.Rule_StreamBTier2)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Three:
                                    if (!Properties.Settings.Default.Rule_StreamBTier3)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Four:
                                    if (!Properties.Settings.Default.Rule_StreamBTier4)
                                        InvalidStreamTierCombination = true;
                                    break;
                            }
                            break;
                        case Global.RecoveryStream.C:
                            switch (Application.DesiredTier)
                            {
                                case Global.RecoveryTier.Zero:
                                    if (!Properties.Settings.Default.Rule_StreamCTier0)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.One:
                                    if (!Properties.Settings.Default.Rule_StreamCTier1)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Two:
                                    if (!Properties.Settings.Default.Rule_StreamCTier2)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Three:
                                    if (!Properties.Settings.Default.Rule_StreamCTier3)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Four:
                                    if (!Properties.Settings.Default.Rule_StreamCTier4)
                                        InvalidStreamTierCombination = true;
                                    break;
                            }
                            break;
                        case Global.RecoveryStream.D:
                            switch (Application.DesiredTier)
                            {
                                case Global.RecoveryTier.Zero:
                                    if (!Properties.Settings.Default.Rule_StreamDTier0)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.One:
                                    if (!Properties.Settings.Default.Rule_StreamDTier1)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Two:
                                    if (!Properties.Settings.Default.Rule_StreamDTier2)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Three:
                                    if (!Properties.Settings.Default.Rule_StreamDTier3)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Four:
                                    if (!Properties.Settings.Default.Rule_StreamDTier4)
                                        InvalidStreamTierCombination = true;
                                    break;
                            }
                            break;
                        case Global.RecoveryStream.E:
                            switch (Application.DesiredTier)
                            {
                                case Global.RecoveryTier.Zero:
                                    if (!Properties.Settings.Default.Rule_StreamETier0)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.One:
                                    if (!Properties.Settings.Default.Rule_StreamETier1)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Two:
                                    if (!Properties.Settings.Default.Rule_StreamETier2)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Three:
                                    if (!Properties.Settings.Default.Rule_StreamETier3)
                                        InvalidStreamTierCombination = true;
                                    break;
                                case Global.RecoveryTier.Four:
                                    if (!Properties.Settings.Default.Rule_StreamETier4)
                                        InvalidStreamTierCombination = true;
                                    break;
                            }
                            break;
                    }

                    if (InvalidStreamTierCombination)
                    {
                        AddDrawingException("[ERR1237] The Stream / Tier combination for " + Application.Name + " (Business Application) is invalid", Application);

                        // Determine what the Tier actually is

                        switch (Application.CalculatedStream)
                        {
                            case Global.RecoveryStream.None4:
                            case Global.RecoveryStream.None3:
                            case Global.RecoveryStream.None2:
                            case Global.RecoveryStream.None1:
                            case Global.RecoveryStream.A:
                                Application.ActualTier = Global.RecoveryTier.One;
                                break;
                            case Global.RecoveryStream.B:
                                Application.ActualTier = Global.RecoveryTier.Two;
                                break;
                            case Global.RecoveryStream.C:
                                Application.ActualTier = Global.RecoveryTier.Three;
                                break;
                            case Global.RecoveryStream.D:
                                Application.ActualTier = Application.DesiredTier;
                                break;
                            case Global.RecoveryStream.E:
                                Application.ActualTier = Application.DesiredTier;
                                break;
                        }
                    }
                }

                #endregion

                #region Check if a matching Management Pack is the correct version

                if (Properties.Settings.Default.EnableSystemCenterFunctions)
                {
                    Version NewVersion = null;
                    int BusinessApplicationDrawingVersion = 0;

                    // Management Pack Version will be in the following form:
                    // App Major . App Minor . Drawing Version . Service Version

                    if (Application.Drawing == null)
                    {
                        BusinessApplicationDrawingVersion = 1;
                    }
                    else
                    {
                        BusinessApplicationDrawingVersion = Application.Drawing.Version;
                    }

                    NewVersion = new Version(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major,
                                             System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor,
                                             BusinessApplicationDrawingVersion,
                                             Application.Version
                                            );

                    if (Application.ManagementPackVersion.ToString() != "0.0.0.0")
                    {
                        if (Application.ManagementPackVersion != NewVersion)
                        {
                            AddDrawingException("[ERR1247] " + Application.Name + ": [Drawing] Management Pack version in SCOM (" + Application.ManagementPackVersion.ToString() + ") is inconsistent with the Drawing Set (" + NewVersion.ToString() + ")", Application);
                        }
                    }

                    //Version CurrentVersion = SCOMUtilities.GetManagementPackVersion(Properties.Settings.Default.SystemCenter_ManagementPackIDPrefix + Application.Name);

                    //if (CurrentVersion != null)
                    //{
                    //    if (CurrentVersion != NewVersion)
                    //    {
                    //        AddDrawingException("[ERR1247] " + Application.Name + ": [Drawing] Management Pack version in SCOM (" + CurrentVersion.ToString() + ") is inconsistent with the Drawing Set (" + NewVersion.ToString() + ")", Application);
                    //    }
                    //}
                    //else
                    //{
                    //    // No Management Pack yet
                    //}
                }

                #endregion

                #region Check components are in the Runbook prior to the application

                // Ensure it doesn't have any components that are in a higher Tier
                foreach (BusinessApplication Component in Application.ComponentBusinessApplications)
                {
                    Relationship r = Global.GetRelationship(Application, Component, DependencyCollection.Relationships);
                    int ComponentIndex = DependencyCollection.Applications.IndexOf(Component);
                    int ApplicationIndex = DependencyCollection.Applications.IndexOf(Component);

                    if ((Component.DesiredTier > Application.DesiredTier && r.SystemMandatory && Application.InScope) || (ComponentIndex < ApplicationIndex))
                    {
                        // This is a problem.  You must recover an applications mandatory components first
                        AddDrawingCriticalException("[ERR1253] " + Application.Name + " (Business Application) has at least one component that is recovered after it (" + Component.Name + ")!  This will cause the Runbook to be incorrect!", Application);
                    }
                }

                #endregion
            }

            #region Runbook position

            foreach (BusinessApplication App in BusinessApplicationRunbook)
            {
                if (App.InScope)
                {
                    CurrentTier = App.DesiredTier;

                    if (PreviousTier != Global.RecoveryTier.Undefined)
                    {
                        if (CurrentTier < PreviousTier)
                        {
                            AddException("[ERR1246] The Tier of " + PreviousApplication.Name + " (Business Application) may be incorrectly defined", PreviousApplication);
                        }
                    }

                    PreviousTier = CurrentTier;
                    PreviousApplication = App;
                }
            }

            #endregion

            #region Recovery Times

            if (Properties.Settings.Default.EnablevCenterQueries)
            {
                foreach (BusinessApplication Service in BusinessApplicationRunbook)
                {
                    if (Service.InScope)
                    {
                        switch (Service.CalculatedStream)
                        {
                            case Global.RecoveryStream.A:
                                if (!StreamAAdded && Properties.Settings.Default.Restore_StreamAServicePrerequisite.ToUpper() != "[NONE]")
                                {
                                    CumulativeTime += Properties.Settings.Default.Restore_StreamAOverheadTime;
                                    StreamAAdded = true;
                                }
                                break;
                            case Global.RecoveryStream.B:
                                if (!StreamBAdded && Properties.Settings.Default.Restore_StreamBServicePrerequisite.ToUpper() != "[NONE]")
                                {
                                    CumulativeTime += Properties.Settings.Default.Restore_StreamBOverheadTime;
                                    StreamBAdded = true;
                                }
                                break;
                            case Global.RecoveryStream.C:
                                if (!StreamCAdded && Properties.Settings.Default.Restore_StreamCServicePrerequisite.ToUpper() != "[NONE]")
                                {
                                    CumulativeTime += Properties.Settings.Default.Restore_StreamCOverheadTime;
                                    StreamCAdded = true;
                                }
                                break;
                            case Global.RecoveryStream.D:
                                if (!StreamDAdded && Properties.Settings.Default.Restore_StreamDServicePrerequisite.ToUpper() != "[NONE]")
                                {
                                    CumulativeTime += Properties.Settings.Default.Restore_StreamDOverheadTime;
                                    StreamDAdded = true;
                                }
                                break;
                            case Global.RecoveryStream.E:
                                if (!StreamEAdded && Properties.Settings.Default.Restore_StreamEServicePrerequisite.ToUpper() != "[NONE]")
                                {
                                    CumulativeTime += Properties.Settings.Default.Restore_StreamEOverheadTime;
                                    StreamBAdded = true;
                                }
                                break;
                        }

                        RecoveryTime = TimeSpan.Zero;

                        foreach (Server Server in Service.ComponentServers)
                        {
                            if (!ServersAdded.Contains(Server)) // if this Server's recovery time hasn't already been added, do so now
                            {
                                ServersAdded.Add(Server);

                                foreach (RecoveryTask Task in Server.RecoveryTasks)
                                {
                                    RecoveryTime += Task.Time;

                                    Task.Completed = false;
                                }
                            }
                            else
                            {
                                foreach (RecoveryTask Task in Server.RecoveryTasks)
                                {
                                    Task.Completed = true;
                                }
                            }
                        }

                        CumulativeTime += RecoveryTime;

                        if (CumulativeTime.TotalSeconds > 0)
                        {
                            switch (Service.DesiredTier)
                            {
                                case Global.RecoveryTier.Zero:
                                    {
                                        TimeSpan Minimum = new TimeSpan(Properties.Settings.Default.Tier0RTOMinimum, 0, 0);
                                        TimeSpan Maximum = new TimeSpan(Properties.Settings.Default.Tier0RTOMaximum, 0, 0);

                                        if (Maximum > TimeSpan.FromSeconds(0))
                                        {
                                            if ((CumulativeTime >= Minimum) && (CumulativeTime < Maximum))
                                            {
                                                // Do nothing - this is the desired result
                                            }
                                            else if (CumulativeTime >= Maximum)
                                            {
                                                AddException("[ERR1226] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") is too long for this Tier 0 Business Application: " + Service.Name, Service);
                                            }
                                            else
                                            {
                                                // The Business Application recovery time is actually placing itself into the bounds of another Tier
                                                AddWarning("[INF1231] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") has the result that this Tier 0 Business Application is being recovered before it is required: " + Service.Name, Service);
                                            }

                                            //if (CumulativeTime > new TimeSpan(Properties.Settings.Default.Tier0RTOMinimum, 0, 0))
                                            //{
                                            //    AddException("[ERR1226] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") is too long for this Tier 0 Business Application: " + Service.Name, Service);
                                            //}
                                        }
                                        break;
                                    }
                                case Global.RecoveryTier.One:
                                    {
                                        TimeSpan Minimum = new TimeSpan(Properties.Settings.Default.Tier1RTOMinimum, 0, 0);
                                        TimeSpan Maximum = new TimeSpan(Properties.Settings.Default.Tier1RTOMaximum, 0, 0);

                                        if (Maximum > TimeSpan.FromSeconds(0))
                                        {
                                            if ((CumulativeTime >= Minimum) && (CumulativeTime < Maximum))
                                            {
                                                // Do nothing - this is the desired result
                                            }
                                            else if (CumulativeTime >= Maximum)
                                            {
                                                AddException("[ERR1227] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") is too long for this Tier 1 Business Application: " + Service.Name, Service);
                                            }
                                            else
                                            {
                                                // The Business Application recovery time is actually placing itself into the bounds of another Tier
                                                AddWarning("[INF1232] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") has the result that this Tier 1 Business Application is being recovered before it is required: " + Service.Name, Service);
                                            }

                                            //if (CumulativeTime > new TimeSpan(Properties.Settings.Default.Tier1RTOMinimum, 0, 0))
                                            //{
                                            //    AddException("[ERR1227] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") is too long for this Tier 1 Business Application: " + Service.Name, Service);
                                            //}
                                        }
                                        break;
                                    }
                                case Global.RecoveryTier.Two:
                                    {
                                        TimeSpan Minimum = new TimeSpan(Properties.Settings.Default.Tier2RTOMinimum, 0, 0);
                                        TimeSpan Maximum = new TimeSpan(Properties.Settings.Default.Tier2RTOMaximum, 0, 0);

                                        if (Maximum > TimeSpan.FromSeconds(0))
                                        {
                                            if ((CumulativeTime >= Minimum) && (CumulativeTime < Maximum))
                                            {
                                                // Do nothing - this is the desired result
                                            }
                                            else if (CumulativeTime >= Maximum)
                                            {
                                                AddException("[ERR1228] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") is too long for this Tier 2 Business Application: " + Service.Name, Service);
                                            }
                                            else
                                            {
                                                // The Business Application recovery time is actually placing itself into the bounds of another Tier
                                                AddWarning("[INF1233] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") has the result that this Tier 2 Business Application is being recovered before it is required: " + Service.Name, Service);
                                            }

                                            //if (CumulativeTime > new TimeSpan(Properties.Settings.Default.Tier1RTOMinimum, 0, 0))
                                            //{
                                            //    AddException("[ERR1228] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") is too long for this Tier 2 Business Application: " + Service.Name, Service);
                                            //}
                                        }
                                        break;
                                    }
                                case Global.RecoveryTier.Three:
                                    {
                                        TimeSpan Minimum = new TimeSpan(Properties.Settings.Default.Tier3RTOMinimum, 0, 0);
                                        TimeSpan Maximum = new TimeSpan(Properties.Settings.Default.Tier3RTOMaximum, 0, 0);

                                        if (Maximum > TimeSpan.FromSeconds(0))
                                        {
                                            if ((CumulativeTime >= Minimum) && (CumulativeTime < Maximum))
                                            {
                                                // Do nothing - this is the desired result
                                            }
                                            else if (CumulativeTime >= Maximum)
                                            {
                                                AddException("[ERR1229] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") is too long for this Tier 3 Business Application: " + Service.Name, Service);
                                            }
                                            else
                                            {
                                                // The Business Application recovery time is actually placing itself into the bounds of another Tier
                                                AddWarning("[INF1234] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") has the result that this Tier 3 Business Application is being recovered before it is required: " + Service.Name, Service);
                                            }

                                            //if (CumulativeTime > new TimeSpan(Properties.Settings.Default.Tier1RTOMinimum, 0, 0))
                                            //{
                                            //    AddException("[ERR1229] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") is too long for this Tier 3 Business Application: " + Service.Name, Service);
                                            //}
                                        }
                                        break;
                                    }
                                case Global.RecoveryTier.Four:
                                    {
                                        TimeSpan Minimum = new TimeSpan(Properties.Settings.Default.Tier4RTOMinimum, 0, 0);
                                        TimeSpan Maximum = new TimeSpan(Properties.Settings.Default.Tier4RTOMaximum, 0, 0);

                                        if (Maximum > TimeSpan.FromSeconds(0))
                                        {
                                            if ((CumulativeTime >= Minimum) && (CumulativeTime < Maximum))
                                            {
                                                // Do nothing - this is the desired result
                                            }
                                            else if (CumulativeTime >= Maximum)
                                            {
                                                AddException("[ERR1230] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") is too long for this Tier 4 Business Application: " + Service.Name, Service);
                                            }
                                            else
                                            {
                                                // The Business Application recovery time is actually placing itself into the bounds of another Tier
                                                AddWarning("[INF1235] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") has the result that this Tier 4 Business Application is being recovered before it is required: " + Service.Name, Service);
                                            }

                                            //if (CumulativeTime > new TimeSpan(Properties.Settings.Default.Tier1RTOMinimum, 0, 0))
                                            //{
                                            //    AddException("[ERR1230] The calculated recovery time (" + ISA.Helper.Methods.FormattedTime(CumulativeTime) + ") is too long for this Tier 42 Business Application: " + Service.Name, Service);
                                            //}
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }

            #endregion

            #region Servers

            foreach (Server Server in DependencyCollection.Servers)
            {
                if (Server.Stream == Global.RecoveryStream.A)
                {
                    AddDrawingException("[ERR1106] " + Server.Name + " (Server) Stream is set to 'A' which is an invalid Server Stream", Server);
                }

                if (!Server.AddToManagementPack)
                {
                    AddDatabaseWarning("[INF1238] " + Server.Name + " (Server) will not be added to Management Packs", Server);
                }

                if (Server.Stream == Global.RecoveryStream.B && Server.Virtual == false)
                {
                    AddDrawingException("[ERR1250] " + Server.Name + " (Server) Stream is set to 'B' and Virtual is set to 'false' which is an invalid combination", Server);
                }
            }

            #endregion

            // TODO:  Remove this code from GetRelationshipData and make it a test here
            //if (Relationship.Mandatory && !ComponentBusinessApplication.InScope && BusinessApplication.InScope)
            //{
            //    _MessagesForm.AddDrawingException(ComponentBusinessApplication.Name + " is not In Scope, but is still a Mandatory Component of " + BusinessApplication.Name + ".  The Runbook may not reflect the desired state");
            //}
        }

        private static void GetManagementPackInformation()
        {
            ServerUtilities SCOMUtilities = new ServerUtilities(SCOMServerName, SCSMServerName);

            foreach (BusinessApplication Application in DependencyCollection.Applications)
            {
                {
                    //string MPContents = SCOMUtilities.GetManagementPackXML(Properties.Settings.Default.SystemCenter_ManagementPackIDPrefix + Application.Name);
                    Version CurrentVersion = SCOMUtilities.GetManagementPackVersion(Properties.Settings.Default.SystemCenter_ManagementPackIDPrefix + Application.Name);

                    if (CurrentVersion != null)
                    {
                        Application.ManagementPackVersion = CurrentVersion;
                        //Application.ManagementPackXML = MPContents;
                    }
                    else
                    {
                        // No Management Pack yet
                    }
                }
            }
        }

        private static void GetvCenterData()
        {
            string PrimaryvCenterConnectionString = Properties.Settings.Default.vCenter_PrimaryDatabaseConnection;
            string SecondaryvCenterConnectionString = Properties.Settings.Default.vCenter_SecondaryDatabaseConnection;

            #region vCenter detailed information

            if (Properties.Settings.Default.EnablevCenterQueries)
            {
                Debug.WriteLine("[INF1191] Querying vCenter for additional Server data...", "information");
                Application.DoEvents();

                string AdditionalServerDataQuery = Properties.Settings.Default.vCenter_InformationQuery;

                // Perform this twice, once for each Data Centre

                for (int i = 0; i < 2; i++)
                {
                    DataTable vCenterData = null;

                    switch (i)
                    {
                        case 0:
                            if (_PrimaryvCenterDatabase.OpenConnection(PrimaryvCenterConnectionString))
                            {
                                //vCenterData = _PrimaryvCenterDatabase.PopulateDataTable(AdditionalServerDataQuery, _PrimaryvCenterDatabase.Connection);
                                vCenterData = _PrimaryvCenterDatabase.Execute(AdditionalServerDataQuery);
                                ProcessvCenterData(vCenterData);
                            }
                            else
                            {
                                AddException("[ERR1108] Could not load vCenter data for Site A from SQL Server");
                            }
                            break;
                        case 1:
                            if (SecondaryvCenterConnectionString.Trim().Length > 0)
                            {
                                if (_SecondaryvCenterDatabase.OpenConnection(SecondaryvCenterConnectionString))
                                {
                                    //vCenterData = _SecondaryvCenterDatabase.PopulateDataTable(AdditionalServerDataQuery, _SecondaryvCenterDatabase.Connection);
                                    vCenterData = _SecondaryvCenterDatabase.Execute(AdditionalServerDataQuery);
                                    ProcessvCenterData(vCenterData);
                                }
                                else
                                {
                                    AddException("[ERR1109] Could not load vCenter data for Site B from SQL Server");
                                }
                            }
                            break;
                    }
                }

                #endregion

                #region vCenter Storage

                if (Properties.Settings.Default.ShowDebugInformation)
                {
                    Debug.WriteLine("[INF1192] Querying Storage data...", "information");
                }
                Application.DoEvents();

                string StorageDataDataQuery = Properties.Settings.Default.vCenter_StorageQuery;

                // Perform this twice, once for each Data Centre

                for (int i = 0; i < 2; i++)
                {
                    DataTable vCenterStorageData = null;

                    switch (i)
                    {
                        case 0:
                            //vCenterStorageData = _PrimaryvCenterDatabase.PopulateDataTable(StorageDataDataQuery, _PrimaryvCenterDatabase.Connection);
                            vCenterStorageData = _PrimaryvCenterDatabase.Execute(StorageDataDataQuery);

                            if (vCenterStorageData != null)
                            {
                                foreach (DataRow vCenterDataRow in vCenterStorageData.Rows)
                                {
                                    ProcessvCenterStorageData(vCenterDataRow, vCenterStorageData);
                                }
                            }
                            break;
                        case 1:
                            if (SecondaryvCenterConnectionString.Trim().Length > 0)
                            {
                                //vCenterStorageData = _SecondaryvCenterDatabase.PopulateDataTable(StorageDataDataQuery, _SecondaryvCenterDatabase.Connection);
                                vCenterStorageData = _SecondaryvCenterDatabase.Execute(StorageDataDataQuery);

                                if (vCenterStorageData != null)
                                {
                                    if (vCenterStorageData != null)
                                    {
                                        foreach (DataRow vCenterDataRow in vCenterStorageData.Rows)
                                        {
                                            ProcessvCenterStorageData(vCenterDataRow, vCenterStorageData);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            #endregion

            // Now set the Server disk space totals...
            foreach (Server Server in DependencyCollection.Servers)
            {
                Decimal TotalMB = 0.0M;

                foreach (Disk Disk in Server.Disks)
                {
                    TotalMB += Disk.TotalAllocatedMB;
                }

                Server.TotalAllocatedDiskGB = TotalMB / 1024;
            }
        }

        private static void MoveAppToBottomOfTier(ref BusinessApplication App, ref int Index)
        {
            BusinessApplication LastApplication = null;
            int PositionAddition = 1;
            ISA.Dependency.Global.RecoveryTier ThisTier = App.DesiredTier;

            // Find the last application with the same Tier, so long as it has dependents and hasn't been moved
            LastApplication = BusinessApplicationRunbook.LastOrDefault<BusinessApplication>(
                                                                                             a => a.DesiredTier == ThisTier &&
                                                                                             a.DependentBusinessApplications.Count > 0 &&
                                                                                             a.Flag == false
                                                                                            );
            PositionAddition = 1; // Add 1 to the Position

            if (LastApplication == null)
            {
                // There wasn't a last Application that fit the criteria, so find the first of the next Tier (if it hasn't been moved)

                if (ThisTier != Global.RecoveryTier.Four)
                {
                    LastApplication = BusinessApplicationRunbook.FirstOrDefault<BusinessApplication>(
                                                                                                      a => a.DesiredTier == ThisTier + 1 &&
                                                                                                      a.Flag == false
                                                                                                     );
                    PositionAddition = -1;  // Remove 1 from the position
                }
                else
                {
                    // The Application is Tier 4, so put it at the end of the list
                    LastApplication = BusinessApplicationRunbook.Last<BusinessApplication>(
                                                                                            a => a.DesiredTier == Global.RecoveryTier.Four &&
                                                                                            a.Flag == false
                                                                                           );
                    PositionAddition = 1;  // Add 1 to the position
                }
            }

            int CurrentIndex = BusinessApplicationRunbook.IndexOf(App);

            if (LastApplication != null)
            {
                int NewIndex = BusinessApplicationRunbook.IndexOf(LastApplication) + PositionAddition;

                if (NewIndex > CurrentIndex + 1 || (LastApplication.DesiredTier > App.DesiredTier && (NewIndex != CurrentIndex) && (NewIndex != CurrentIndex + 1)))
                {
                    Program.DebugPrint("......[INF1236] Placing " + App.Name + " (Business Application) immediately before " + LastApplication.Name + " (Business Application)", "information", false);

                    BusinessApplicationRunbook.RemoveAt(CurrentIndex);
                    BusinessApplicationRunbook.Insert(NewIndex, App);

                    Index--;

                    App.Flag = true;
                }
                else
                {
                    //Debug.WriteLine(i + "+++ (" + Service.Name + ")");
                }
            }
            else
            {
                // If we don't know where to move it, find the last of it's Tier

                Program.DebugPrint("......[INF1252] Moving " + App.Name + " (Business Application) to Index " + (BusinessApplicationRunbook.Count - 1).ToString(), "information", false);
                BusinessApplicationRunbook.RemoveAt(CurrentIndex);
                BusinessApplicationRunbook.Insert(BusinessApplicationRunbook.Count - 1, App);

                App.Flag = true;
            }
        }

        private static void ProcessvCenterData(DataTable vCenterData)
        {
            foreach (DataRow vCenterDataRow in vCenterData.Rows)
            {
                string ServerName = vCenterDataRow[8].ToString().Trim();

                if (ServerName != "")
                {
                    Server Server = new Server(ServerName);

                    Server ExistingServer = Global.GetExistingServer(Server, DependencyCollection.Servers);

                    if (ExistingServer != null)
                    {
                        if (Properties.Settings.Default.ShowDebugInformation)
                        {
                            Debug.WriteLine("..." + ServerName, "information");
                        }

                        if (vCenterDataRow[9] != null && (string)vCenterDataRow[9] != "")
                            ExistingServer.IPAddressList.Add(System.Net.IPAddress.Parse(vCenterDataRow[9].ToString()));

                        if (vCenterDataRow[5] != null && (string)vCenterDataRow[5] != "")
                            ExistingServer.OperatingSystem = vCenterDataRow[5].ToString();

                        if (vCenterDataRow[2] != null && (int)vCenterDataRow[2] != 0)
                            ExistingServer.Memory = Convert.ToInt32(vCenterDataRow[2].ToString());

                        if (vCenterDataRow[3] != null && (int)vCenterDataRow[3] != 0)
                            ExistingServer.CPUCount = Convert.ToInt32(vCenterDataRow[3].ToString());

                        if (vCenterDataRow[1] != null && (string)vCenterDataRow[1] != "")
                            ExistingServer.VCenterName = vCenterDataRow[1].ToString();

                        if (vCenterDataRow[14] != null && (string)vCenterDataRow[14] != "")
                            ExistingServer.VCenterDescription = vCenterDataRow[14].ToString();

                        //if (vCenterDataRow[16] != null && vCenterDataRow[16] != "")
                        //    ExistingServer.TotalAllocatedDisk = Convert.ToInt32(vCenterDataRow[16].ToString());
                    }

                    Server = null;
                }
            }
        }

        private static void ProcessvCenterStorageData(DataRow vCenterDataRow, DataTable vCenterStorageData)
        {
            string ServerName = vCenterDataRow[1].ToString().Trim();

            if (ServerName != "")
            {
                Global.AvailabilityPredictionMethod Method = Global.AvailabilityPredictionMethod.Storage;
                Server Server = new Server(ServerName);

                Server ExistingServer = Global.GetExistingServer(Server, DependencyCollection.Servers);

                if (ExistingServer != null)
                {
                    Disk Disk = new Disk();

                    if (vCenterDataRow[2] != null && (string)vCenterDataRow[2] != "")
                        Disk.Name = vCenterDataRow[2].ToString();

                    if (vCenterDataRow[3] != null && (long)vCenterDataRow[3] != 0)
                        Disk.TotalAllocatedMB = Convert.ToInt32(vCenterDataRow[3].ToString());

                    if (vCenterDataRow[4] != null && (string)vCenterDataRow[4] != "")
                        Disk.StorageName = vCenterDataRow[4].ToString();

                    ExistingServer.Disks.Add(Disk);

                    switch (ExistingServer.Stream)
                    {
                        case Global.RecoveryStream.A:
                            switch (Properties.Settings.Default.Restore_StreamAAvailabilityPreditionMethod)
                            {
                                case 0: // Time
                                    Method = Global.AvailabilityPredictionMethod.Time;
                                    break;
                                case 1: // Storage
                                    Method = Global.AvailabilityPredictionMethod.Storage;
                                    break;
                                case 2: // Time + Storage
                                    Method = Global.AvailabilityPredictionMethod.TimeAndStorage;
                                    break;
                            }
                            Disk.CalculateRestoreTime(Properties.Settings.Default.Restore_StreamARestoreTime1, Properties.Settings.Default.Restore_StreamARestoreTime2, Properties.Settings.Default.Restore_StreamARestoreRate1, Properties.Settings.Default.Restore_StreamARestoreRate2, Properties.Settings.Default.Restore_StreamAIntervalPeriod, Method);
                            break;
                        case Global.RecoveryStream.B:
                            switch (Properties.Settings.Default.Restore_StreamBAvailabilityPreditionMethod)
                            {
                                case 0: // Time
                                    Method = Global.AvailabilityPredictionMethod.Time;
                                    break;
                                case 1: // Storage
                                    Method = Global.AvailabilityPredictionMethod.Storage;
                                    break;
                                case 2: // Time + Storage
                                    Method = Global.AvailabilityPredictionMethod.TimeAndStorage;
                                    break;
                            }
                            Disk.CalculateRestoreTime(Properties.Settings.Default.Restore_StreamBRestoreTime1, Properties.Settings.Default.Restore_StreamBRestoreTime2, Properties.Settings.Default.Restore_StreamBRestoreRate1, Properties.Settings.Default.Restore_StreamBRestoreRate2, Properties.Settings.Default.Restore_StreamBIntervalPeriod, Method);
                            break;
                        case Global.RecoveryStream.C:
                            switch (Properties.Settings.Default.Restore_StreamCAvailabilityPreditionMethod)
                            {
                                case 0: // Time
                                    Method = Global.AvailabilityPredictionMethod.Time;
                                    break;
                                case 1: // Storage
                                    Method = Global.AvailabilityPredictionMethod.Storage;
                                    break;
                                case 2: // Time + Storage
                                    Method = Global.AvailabilityPredictionMethod.TimeAndStorage;
                                    break;
                            }
                            Disk.CalculateRestoreTime(Properties.Settings.Default.Restore_StreamCRestoreTime1, Properties.Settings.Default.Restore_StreamCRestoreTime2, Properties.Settings.Default.Restore_StreamCRestoreRate1, Properties.Settings.Default.Restore_StreamCRestoreRate2, Properties.Settings.Default.Restore_StreamCIntervalPeriod, Method);
                            break;
                        case Global.RecoveryStream.D:
                            switch (Properties.Settings.Default.Restore_StreamDAvailabilityPreditionMethod)
                            {
                                case 0: // Time
                                    Method = Global.AvailabilityPredictionMethod.Time;
                                    break;
                                case 1: // Storage
                                    Method = Global.AvailabilityPredictionMethod.Storage;
                                    break;
                                case 2: // Time + Storage
                                    Method = Global.AvailabilityPredictionMethod.TimeAndStorage;
                                    break;
                            }
                            Disk.CalculateRestoreTime(Properties.Settings.Default.Restore_StreamDRestoreTime1, Properties.Settings.Default.Restore_StreamDRestoreTime2, Properties.Settings.Default.Restore_StreamDRestoreRate1, Properties.Settings.Default.Restore_StreamDRestoreRate2, Properties.Settings.Default.Restore_StreamDIntervalPeriod, Method);
                            break;
                        case Global.RecoveryStream.E:
                            switch (Properties.Settings.Default.Restore_StreamEAvailabilityPreditionMethod)
                            {
                                case 0: // Time
                                    Method = Global.AvailabilityPredictionMethod.Time;
                                    break;
                                case 1: // Storage
                                    Method = Global.AvailabilityPredictionMethod.Storage;
                                    break;
                                case 2: // Time + Storage
                                    Method = Global.AvailabilityPredictionMethod.TimeAndStorage;
                                    break;
                            }
                            Disk.CalculateRestoreTime(Properties.Settings.Default.Restore_StreamERestoreTime1, Properties.Settings.Default.Restore_StreamERestoreTime2, Properties.Settings.Default.Restore_StreamERestoreRate1, Properties.Settings.Default.Restore_StreamERestoreRate2, Properties.Settings.Default.Restore_StreamEIntervalPeriod, Method);
                            break;
                    }

                    if (Properties.Settings.Default.ShowDebugInformation)
                    {
                        Debug.WriteLine("...[INF1194] " + ExistingServer.Name + " (" + Disk.Name + ") Restore time: " + Disk.RestoreTime + " (Stream " + ExistingServer.Stream + ")", "information");
                    }

                }

                Server = null;
            }
        }

        private static void ReOrderApplications()
        {
            //BusinessApplication LastApplication = null;
            //int PositionAddition = 1;

            Program.DebugPrint("[INF1195] Reordering Business Applications by SRM Group, Tier, Stream, Dependent Count...", "information", true);
            Application.DoEvents();

            if (Properties.Settings.Default.ShowDebugInformation)
            {
                int Counter = 0;

                Program.DebugPrint("...[INF1196] Initial result...", "information", true);
                foreach (BusinessApplication Service in BusinessApplicationRunbook)
                {
                    Program.DebugPrint("......" + Counter + ". " + Service.Name + ": " + Service.RunbookRegion + " = Stream " + Service.CalculatedStream + ", Dependent Count = " + Service.DependentBusinessApplications.Count + ", Tier = " + Service.DesiredTier, "information", true);

                    Counter++;
                }
            }

            BusinessApplicationRunbook = BusinessApplicationRunbook.OrderBy(Service => Service.RunbookRegion)
                                                                     .ThenBy(Service => (int)Service.DesiredTier)
                                                                     .ThenBy(Service => Service.CalculatedStream)
                                                                     .ThenByDescending(Service => Service.DependentBusinessApplications.Count)
                                                                     .ToList();

            #region Move unreferenced Applications to end of Tier

            Program.DebugPrint("...[INF1196] Moving unreferenced Business Applications to the end of their Tier...", "information", true);
            Application.DoEvents();

            // Reset the Flag property for each Application...
            foreach (BusinessApplication App in BusinessApplicationRunbook)
            {
                App.Flag = false;
            }

            for (int i = 0; i < BusinessApplicationRunbook.Count; i++)
            {
                BusinessApplication App = BusinessApplicationRunbook[i];

                if (App.DependentBusinessApplications.Count == 0)//&& Service.Flag == false)
                {
                    MoveAppToBottomOfTier(ref App, ref i);
                }
                else
                {
                    //Program.DebugPrint(i + " Untargetted ---> " + App.Name );
                }
            }

            #endregion

            #region Move applications to as late as necessary

            //Global.RecoveryTier PreviousTier = Global.RecoveryTier.Undefined;
            //Global.RecoveryTier CurrentTier = Global.RecoveryTier.Undefined;
            //Global.RecoveryTier NextTier = Global.RecoveryTier.Undefined;
            //BusinessApplication PreviousApplication = null;
            ////int AppIndex = 0;

            //for (int i = 0; i < _BusinessApplicationRunbook.Count; i++)
            //{
            //    BusinessApplication App = _BusinessApplicationRunbook[i];

            //    if (App.InScope)
            //    {
            //        CurrentTier = App.Tier;

            //        if (i < _BusinessApplicationRunbook.Count - 1)
            //        {
            //            NextTier = (_BusinessApplicationRunbook[i + 1]).Tier;
            //        }
            //        else
            //        {
            //            // There is no next Tier
            //            NextTier = Global.RecoveryTier.Undefined;
            //        }

            //        if (PreviousTier != Global.RecoveryTier.Undefined)
            //        {
            //            if (CurrentTier < PreviousTier)
            //            {
            //                // This Application doesn't belong here.  Move it down the list so that it is immediately before whatever needs it

            //                //int p = 0;
            //                Program.DebugPrint("-----> To do:  Move " + App.Name + " to where it is required","Information", true);
            //            }
            //            else
            //            {
            //                // Check the next app is the same or higher Tier
            //                if (NextTier != Global.RecoveryTier.Undefined && CurrentTier > NextTier)
            //                {
            //                    // The previous Application doesn't belong here.  Move it down the list so that it is immediately before whatever needs it

            //                    BusinessApplication TargetApplication = _BusinessApplicationRunbook[i - 1];

            //                    // Find it's earliest Component...
            //                    if (TargetApplication.ComponentBusinessApplications.Count > 0)
            //                    {
            //                        //int p = 0;
            //                        Program.DebugPrint("-----> To do:  Move " + App.Name + " to where it is required", "information", true);
            //                    }
            //                    else
            //                    {
            //                        // No components - move it to the end of it's Tier
            //                        //int p = 0;
            //                        MoveAppToBottomOfTier(ref TargetApplication, ref i);
            //                    }
            //                }
            //            }
            //        }

            //        PreviousTier = CurrentTier;
            //        PreviousApplication = App;
            //    }

            //    //i++;
            //}

            #endregion

            if (Properties.Settings.Default.ShowDebugInformation)
            {
                int Counter = 0;

                Program.DebugPrint("...[INF1197] Final result...", "information", true);

                foreach (BusinessApplication Service in BusinessApplicationRunbook)
                {
                    Program.DebugPrint("......" + Counter + ". " + Service.Name + ": " + Service.RunbookRegion + " = Stream " + Service.CalculatedStream + ", Dependent Count = " + Service.DependentBusinessApplications.Count + ", Tier = " + Service.DesiredTier, "information", true);

                    Counter++;
                }
            }
        }

        public static void SortRunbook()
        {
            Debug.WriteLine("[INF1161] Sorting the Primary Runbook...", "information");
            Application.DoEvents();
            DependencyCollection.SortRunbook();
        }

        private static void UpdateChildForms()
        {
            foreach (frmBusinessApplication ServiceForm in BusinessApplicationForms)
            {
                ServiceForm.PerformUpdate();
            }

            foreach (frmServer ServerForm in _ServerForms)
            {
                ServerForm.PerformUpdate();
            }

            foreach (frmList RunbookForm in _ListForms)
            {
                RunbookForm.PerformUpdate();
            }
        }

        private static void UpdateDependencyDatabaseROStatus()
        {
            if (Properties.Settings.Default.Security_DependencyDatabaseReadGroup.Trim().Length > 0)
            {

                // This uses the TPL to perform a parallel task, that then is able to get the correct context that allows the GUI to be updated

                //if (Properties.Settings.Default.ShowDebugInformation)
                //{
                Debug.WriteLine("[INF1181] Determining iServer Read Only status...", "information");
                //}

                //var context = TaskScheduler.FromCurrentSynchronizationContext();

                //Task task = Task.Factory.StartNew(() =>
                //{
                ISA.Helper.Properties.DependencyDatabaseRO = ISA.Helper.Methods.IsGroupMember(Properties.Settings.Default.Security_DependencyDatabaseReadGroup);
                //}
                //).ContinueWith(_ => Debug.WriteLine("...[INF1181] iServer Read Only status returned!"), context);

                Debug.WriteLine("...[INF1182] iServer Read Only status returned!", "information");

                if (MainForm == null)
                {
                    MDIForm.UpdateLoadDependencyDataToolstripForReadOnly();
                }
                else
                {
                    MainForm.UpdateLoadDependencyDataToolstripForReadOnly();
                }
                //toolStripButtonLoadiServerProduction.Enabled = ISA.Helper.Properties.DependencyDatabaseRO;
                //iServerToolStripMenuItem.Enabled = ISA.Helper.Properties.DependencyDatabaseRO;

            }
        }

        private static void UpdateDependencyDatabaseRWStatus()
        {
            if (Properties.Settings.Default.Security_DependencyDatabaseReadGroup.Trim().Length > 0)
            {
                // NEW:  https://gist.github.com/dgrunwald/1961087
                // This uses the TPL to perform a parallel task, that then is able to get the correct context that allows the GUI to be updated

                if (Properties.Settings.Default.ShowDebugInformation)
                {
                    Debug.WriteLine("[INF1183] Determining iServer Read Write status...", "information"); // (as a background task)...");
                }

                //var context = TaskScheduler.FromCurrentSynchronizationContext();

                //Task task = Task.Factory.StartNew(() =>
                //{
                ISA.Helper.Properties.DependencyDatabaseRW = true; // (ISA.Helper.Methods.IsGroupMember(Properties.Settings.Default.Security_DependencyDatabaseWriteGroup));
                                                                   //}
                                                                   //).ContinueWith(_ => Debug.WriteLine("...iServer Read Write status returned!"), context);

                if (Properties.Settings.Default.ShowDebugInformation)
                {
                    Debug.WriteLine("...[INF1184] iServer Read Write status returned!", "information");
                }

                if (MainForm == null)
                {
                    MDIForm.UpdateLoadDependencyDataToolstripForReadWrite();
                }
                else
                {
                    MainForm.UpdateLoadDependencyDataToolstripForReadWrite();
                }
                //toolStripButtonLoadiServerProduction.Enabled = ISA.Helper.Properties.DependencyDatabaseRO || ISA.Helper.Properties.DependencyDatabaseRW;
                //iServerToolStripMenuItem.Enabled = ISA.Helper.Properties.DependencyDatabaseRO || ISA.Helper.Properties.DependencyDatabaseRW;

            }
        }

        private static void UpdateSystemCenterAuthorStatus()
        {
            // NEW:  https://gist.github.com/dgrunwald/1961087

            if (Properties.Settings.Default.EnableSystemCenterFunctions)
            {

                // This uses the TPL to perform a parallel task, that then is able to get the correct context that allows the GUI to be updated

                if (Properties.Settings.Default.ShowDebugInformation)
                {
                    Debug.WriteLine("[INF1185] Determining System Center Author status...", "information"); // (as a background task)...");
                }

                //var context = TaskScheduler.FromCurrentSynchronizationContext();

                //Task task = Task.Factory.StartNew(() =>
                //{
                ISA.Helper.Properties.ManagementPackAuthor = true; // ISA.Helper.Methods.IsGroupMember(Properties.Settings.Default.Security_SystemCenterMPAuthorGroup);
                                                                   //})
                                                                   //.ContinueWith(_ => toolStripButtonCreateManagementPacks.Enabled = ISA.Helper.Properties.ManagementPackAuthor, context)
                                                                   //.ContinueWith(_ => Debug.WriteLine("...System Center Author status task returned!"), context)
                                                                   //;

                if (MainForm == null)
                {
                    MDIForm.UpdateCreateManagementPacksToolstrip();
                }
                else
                {
                    MainForm.UpdateCreateManagementPacksToolstrip();
                }

                if (Properties.Settings.Default.ShowDebugInformation)
                {
                    Debug.WriteLine("...[INF1186] System Center Author status task returned!", "information");
                }
            }
        }

        private static void UpdateSystemCenterToolStrip()
        {
            if (MainForm == null)
            {
                MDIForm.UpdateSystemCenterToolStrip();
            }
            else
            {
                MainForm.UpdateSystemCenterToolStrip();
            }
        }

        private static void UpdatevCenterReadStatus()
        {
            // NEW:  https://gist.github.com/dgrunwald/1961087

            if (Properties.Settings.Default.EnablevCenterQueries)
            {
                // This uses the TPL to perform a parallel task, that then is able to get the correct context that allows the GUI to be updated

                if (Properties.Settings.Default.ShowDebugInformation)
                {
                    Debug.WriteLine("[INF1187] Determining vCenter Read status...", "information"); // (as a background task)...");
                }

                //var context = TaskScheduler.FromCurrentSynchronizationContext();

                //Task task = Task.Factory.StartNew(
                //() =>
                //{
                ISA.Helper.Properties.vCenterFunctionsEnabled = ISA.Helper.Methods.IsGroupMember(Properties.Settings.Default.Security_vCenterSQLReadGroup);
                //}
                //).ContinueWith(_ => Debug.WriteLine("...vCenter Read status task returned!"), context);

                if (Properties.Settings.Default.ShowDebugInformation)
                {
                    Debug.WriteLine("...[INF1188] vCenter Read status task returned!", "information");
                }
            }

        }

        #endregion

        #region Event Handlers

        private static void iServer_AnnouncementReceived(object Sender, ISA.DataLayer.AnnouncementEventArgs e)
        {
            Debug.WriteLine(e.Text, "information");

            if (e.RelatedObject is BusinessApplication)
            {
                AddDrawingException(e.Text.TrimStart('.'), (BusinessApplication)e.RelatedObject);
            }
            else if (e.RelatedObject is Server)
            {
                AddDrawingException(e.Text.TrimStart('.'), (Server)e.RelatedObject);
            }
            else if (e.RelatedObject is Drawing)
            {
                AddDrawingException(e.Text.TrimStart('.'));
            }
            else
            {
                AddDrawingException(e.Text.TrimStart('.'));
            }
        }

        #endregion
    }
}
