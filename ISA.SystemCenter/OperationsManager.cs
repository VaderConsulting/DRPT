// The SCOM SDK Binaries are installed when you install the SCOM Console.
// Located at:  C:\Program Files\System Center Operations Manager 2012\Console\SDK Binaries
using Microsoft.EnterpriseManagement;
using Microsoft.EnterpriseManagement.Common;
using Microsoft.EnterpriseManagement.Configuration;  // Required to get access to the ManagementPack class etc
using Microsoft.EnterpriseManagement.Configuration.IO;
using Microsoft.EnterpriseManagement.Monitoring;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;  // Powershell - C:\windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35\System.Management.Automation.dll
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using VaderConsulting.Helper;

namespace VaderConsulting.SystemCenter
{
    public class OperationsManager
    {
        #region Classes

        public class ServiceInfo
        {
            public string ServiceBaseClass = "Microsoft.SystemCenter.ServiceDesigner.GenericService";
            public string ServiceFriendlyName = "";
            public string ServiceName = "";
        }

        public class ConnectionInfo
        {
            public string ManagementGroupServerName = "";
            public string ReferencePath = "";
        }

        public class ManagementPackSealOptions
        {
            public bool SealManagementPack = false;
            public string ManagementPackSealCommandLine = "";
            public string KeyfileFilename = "";
            public string KeyCompanyName = "Your Organisation";
            public string PowerShellModulePath = "";
        }

        public class ManagementPackCreationOptions
        {
            public string DistributedApplicationClassID = "";
            public string DistributedApplicationComponentName = "";
            public string DistributedApplicationComponentID = "";
            public string ManagementPackDescription = "";
            public string ManagementPackFilename = "";
            public string ManagementPackName = "";
            //public string ManagementPackID = "";
            public string ManagementPackID = "";
            public Version ManagementPackVersion = null;
            public bool ImportManagementPack = false;
            public bool UseDynamicDiscovery = false;
            public string RelationshipName = "";
            public string ManagementPackEmptyTemplate = "";
            public string ManagementPackServersAndServicesTemplate = "";
            public string ManagementPackServersTemplate = "";
            public string ManagementPackServicesTemplate = "";

            public ServiceInfo ServiceInfo = new ServiceInfo();
            public ConnectionInfo ConnectionInfo = new ConnectionInfo();
            public ManagementPackSealOptions SealOptions = new ManagementPackSealOptions();
        }

        #endregion

        #region Constructors

        public OperationsManager(string SCOMServer)
        {
            try
            {
                _SCOMServerConnection = new ManagementGroup(SCOMServer);
            }
            catch
            {

            }
        }

        #endregion

        #region Private Types

        private ManagementPackClass _ComponentServiceClass;
        private ManagementPack _HealthMP;
        private ManagementPack _ImagesMP;
        //private string _ManagementPackClassName = "";
        private string _ManagementPackComponentDisplayName = "";
        //private string _ManagementPackComponentName = "";
        private string _ManagementPackDescription = "";
        private string _ManagementPackFriendlyName = "";
        private string _ManagementPackName = "";
        private string _SealedFilename = "";
        private ManagementPackFileStore _MPStore = new ManagementPackFileStore();
        private ManagementPack _OriginalManagementPack = null;
        private ManagementGroup _SCOMServerConnection;
        private ManagementPackClass _ServiceComponentGroupClass;
        //private ManagementPackClass _ServerComponentGroupClass;
        private ManagementPackRelationship _ServiceComponentGroupRelationship;
        //private ManagementPackRelationship _ServerComponentGroupRelationship;
        private string _ServiceComponentGroupRelationshipName;
        private ManagementPackRelationship _ServiceRelationship;
        private ManagementPackRelationship _ServerRelationship;
        private ManagementPack _SystemCenterMP;
        private ManagementPack _SystemCenterServiceDesignerMP;
        private ManagementPackClass _SystemComputerClass;
        private ManagementPackRelationship _SystemComputerRelationship;
        private string _SystemComputerRelationshipName;
        private ManagementPack _SystemMP;
        private ManagementPack _TargetManagementPack;
        private ManagementPackClass _ThisServiceClass;


        #endregion

        #region Properties

        //public string ManagementPackClassName
        //{
        //    get
        //    {
        //        return _ManagementPackClassName;
        //    }
        //    set
        //    {
        //        _ManagementPackClassName = value;
        //    }
        //}

        public string ManagementPackComponentDisplayName
        {
            get
            {
                return _ManagementPackComponentDisplayName;
            }
            set
            {
                _ManagementPackComponentDisplayName = value;
            }
        }

        //public string ManagementPackComponentName
        //{
        //    get
        //    {
        //        return _ManagementPackComponentName;
        //    }
        //    set
        //    {
        //        _ManagementPackComponentName = value;
        //    }
        //}

        public string ManagementPackDescription
        {
            get
            {
                return _ManagementPackDescription;
            }
            set
            {
                _ManagementPackDescription = value;
            }
        }

        public string ManagementPackFriendlyName
        {
            get
            {
                return _ManagementPackFriendlyName;
            }
            set
            {
                _ManagementPackFriendlyName = value;
            }
        }

        public string ManagementPackName
        {
            get
            {
                return _ManagementPackName;
            }
            set
            {
                _ManagementPackName = value;
            }
        }

        public ManagementPack OriginalManagementPack
        {
            get
            {
                return _OriginalManagementPack;
            }
            set
            {
                _OriginalManagementPack = value;
            }
        }

        public string ServiceComponentGroupRelationshipName
        {
            get
            {
                return _ServiceComponentGroupRelationshipName;
            }
            set
            {
                _ServiceComponentGroupRelationshipName = value;
            }
        }

        public string SystemComputerRelationshipName
        {
            get
            {
                return _SystemComputerRelationshipName;
            }
            set
            {
                _SystemComputerRelationshipName = value;
            }
        }

        public ManagementPack TargetManagementPack
        {
            get
            {
                return _TargetManagementPack;
            }
            set
            {
                _TargetManagementPack = value;
            }
        }

        #endregion

        #region Private Methods

        private void AddClassesToMP(ManagementPackCreationOptions CreationOptions)
        {
            string ServiceClassIdentifier = CreationOptions.DistributedApplicationClassID.Replace("#GUID#", BuildRelationshipID(CreationOptions.DistributedApplicationClassID));

            _ThisServiceClass = new ManagementPackClass(_TargetManagementPack, ServiceClassIdentifier, ManagementPackAccessibility.Public);
            _ThisServiceClass.Abstract = false;
            _ThisServiceClass.Singleton = true;
            _ThisServiceClass.Hosted = false;
            _ThisServiceClass.Extension = false;
            _ThisServiceClass.Base = _SystemCenterServiceDesignerMP.GetClass(CreationOptions.ServiceInfo.ServiceBaseClass);
            _ThisServiceClass.DisplayName = CreationOptions.ServiceInfo.ServiceFriendlyName;

            string ComponentClassIdentifier = CreationOptions.DistributedApplicationComponentID.Replace("#GUID#", BuildRelationshipID(CreationOptions.DistributedApplicationComponentID)).Replace("#CLASSID#", _ThisServiceClass.Name);

            _ComponentServiceClass = new ManagementPackClass(_TargetManagementPack, ComponentClassIdentifier, ManagementPackAccessibility.Public);
            _ComponentServiceClass.Abstract = false;
            _ComponentServiceClass.Singleton = true;
            _ComponentServiceClass.Hosted = false;
            _ComponentServiceClass.Extension = false;
            _ComponentServiceClass.Base = _SystemCenterServiceDesignerMP.GetClass("Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup");
            _ComponentServiceClass.DisplayName = CreationOptions.DistributedApplicationComponentName; // .ServiceOptions.ServiceFriendlyName;
        }

        private void AddComponentDiscoveryToMP()
        {
            ManagementPackDiscovery ComponentDiscovery = new ManagementPackDiscovery(_TargetManagementPack, _ThisServiceClass.Name + "_SCPopulation");
            ComponentDiscovery.Category = ManagementPackCategoryType.Discovery;
            ComponentDiscovery.Enabled = ManagementPackMonitoringLevel.@true;
            ComponentDiscovery.DisplayName = "Distributed Application Membership Discovery";
            ComponentDiscovery.Description = "This discovery will find which Components are members of this Distributed Application.";
            ComponentDiscovery.ConfirmDelivery = false;
            ComponentDiscovery.Remotable = true;
            ComponentDiscovery.Priority = ManagementPackWorkflowPriority.Normal;
            ComponentDiscovery.Target = (ManagementPackElementReference<ManagementPackClass>)_ThisServiceClass;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<RuleId>$MPElement$</RuleId>");
            stringBuilder.Append("<GroupInstanceId>$Target/Id$</GroupInstanceId>");
            stringBuilder.Append("<MembershipRules>");
            stringBuilder.Append("<MembershipRule>");
            stringBuilder.Append("<MonitoringClass>$MPElement[Name=\"" + _ComponentServiceClass.Name + "\"]$</MonitoringClass>");
            stringBuilder.Append("<RelationshipClass>$MPElement[Name=\"" + _ServiceRelationship.Name + "\"]$</RelationshipClass>");
            stringBuilder.Append("</MembershipRule>");
            stringBuilder.Append("</MembershipRules>");

            ManagementPackModuleType ModuleType = _SystemCenterMP.GetModuleType("Microsoft.SystemCenter.GroupPopulator");

            ComponentDiscovery.DataSource = new ManagementPackDataSourceModule((ManagementPackElement)ComponentDiscovery, "DS")
            {
                TypeID = (ManagementPackElementReference<ManagementPackDataSourceModuleType>)((ManagementPackDataSourceModuleType)ModuleType)
            };

            ComponentDiscovery.DataSource.Configuration = stringBuilder.ToString();

            ComponentDiscovery.Status = ManagementPackElementStatus.PendingAdd;

            /* 
            <Discovery ID="AD_DTFPopulation" Enabled="true" Target="Service_AD_DTF" ConfirmDelivery="false" Remotable="true" Priority="Normal">
                <Category>Discovery</Category>
                <DiscoveryTypes />
                    <DataSource ID="GroupPopulationDataSource" TypeID="SystemCenter!Microsoft.SystemCenter.GroupPopulator">
                    <RuleId>$MPElement$</RuleId>
                    <GroupInstanceId>$Target/Id$</GroupInstanceId>
                    <MembershipRules>
                        <MembershipRule>
                            <MonitoringClass>$MPElement[Name="ComponentGroup_Service_AD_DTF"]$</MonitoringClass>
                            <RelationshipClass>$MPElement[Name="ComponentRelationship"]$</RelationshipClass>
                        </MembershipRule>
                    </MembershipRules>
                </DataSource>
            </Discovery>
            */
        }

        private void AddComponentRelationshipsToMP()
        {
            string ManagementPackRelationshipIdentifier = AddServiceComponentRelationshipToMP();

            AddServiceComputerRelationshipToMP(ManagementPackRelationshipIdentifier);

            AddServerRelationshipToMP();
            //AddServiceRelationshipToMP();

        }

        private void AddReference(string Alias, ManagementPack Reference)
        {
            ManagementPackReference MPReference = new ManagementPackReference(Reference);
            KeyValuePair<string, ManagementPackReference> kvp = new KeyValuePair<string, ManagementPackReference>(Alias, MPReference);

            // Check if the MP already contains this reference...
            if (!_TargetManagementPack.References.Contains(kvp))
                _TargetManagementPack.References.Add(Alias, new ManagementPackReference(Reference));
        }

        private void AddReferencesToMP(string ReferencePath)
        {
            _SystemCenterMP = new ManagementPack(System.IO.Path.Combine(ReferencePath, @"Microsoft.SystemCenter.Library.mp"), _MPStore);
            AddReference("SystemCenter", _SystemCenterMP);

            _SystemCenterServiceDesignerMP = new ManagementPack(System.IO.Path.Combine(ReferencePath, @"Microsoft.SystemCenter.ServiceDesigner.Library.mp"), _MPStore);
            AddReference("MicrosoftSystemCenterServiceDesignerLibrary7084320", _SystemCenterServiceDesignerMP);

            _SystemMP = new ManagementPack(System.IO.Path.Combine(ReferencePath, @"System.Library.mp"), _MPStore);
            AddReference("SystemLibrary7585010", _SystemMP);

            _HealthMP = new ManagementPack(System.IO.Path.Combine(ReferencePath, @"System.Health.Library.mp"), _MPStore);
            AddReference("SystemHealthLibrary7084320", _HealthMP);

            _ImagesMP = new ManagementPack(System.IO.Path.Combine(ReferencePath, @"System.Image.Library.mp"), _MPStore);
            AddReference("Image", _ImagesMP);
        }

        public void AddRelationshipsToMP(List<string> ServerList, List<string> ServiceList, ManagementPackCreationOptions CreationOptions, ref string ErrorMessage)
        {
            // Setup objects to allow searching for an object based on it's Name
            MonitoringClassCriteria ServerClassCriteria = new MonitoringClassCriteria("Name = 'System.Computer'");
            ReadOnlyCollection<MonitoringClass> ServerMonitoringClasses = _SCOMServerConnection.GetMonitoringClasses(ServerClassCriteria);

            ManagementPackDiscovery ComponentDiscovery = new ManagementPackDiscovery(_TargetManagementPack, _ComponentServiceClass.Name + "_ItemPopulation");
            ComponentDiscovery.Category = ManagementPackCategoryType.Discovery;
            ComponentDiscovery.Enabled = ManagementPackMonitoringLevel.@true;
            ComponentDiscovery.DisplayName = "Component Membership Discovery";
            ComponentDiscovery.Description = "This discovery will find which Objects are members of this Component.";
            ComponentDiscovery.ConfirmDelivery = false;
            ComponentDiscovery.Remotable = true;
            ComponentDiscovery.Priority = ManagementPackWorkflowPriority.Normal;
            ComponentDiscovery.Target = (ManagementPackElementReference<ManagementPackClass>)_ComponentServiceClass; // _ThisServiceClass;

            ManagementPackModuleType ModuleType = _SystemCenterMP.GetModuleType("Microsoft.SystemCenter.GroupPopulator");

            ComponentDiscovery.DataSource = new ManagementPackDataSourceModule((ManagementPackElement)ComponentDiscovery, "DS")
            {
                TypeID = (ManagementPackElementReference<ManagementPackDataSourceModuleType>)((ManagementPackDataSourceModuleType)ModuleType)
            };

            ComponentDiscovery.DataSource.Configuration = GetDatasource(ServerList, ServiceList, CreationOptions, ref ErrorMessage);

            if (ErrorMessage == "")
            {
                ComponentDiscovery.Status = ManagementPackElementStatus.PendingAdd;
            }
        }

        private string AddServiceComponentRelationshipToMP()
        {
            string ManagementPackRelationshipIdentifier = "SCIMembership_" + BuildRelationshipID(_ComponentServiceClass.Name);

            _SystemComputerClass = _SystemMP.GetClass("System.Computer");
            _ServiceComponentGroupClass = _SystemCenterServiceDesignerMP.GetClass("Microsoft.SystemCenter.ServiceDesigner.Service");

            _ServiceComponentGroupRelationship = new ManagementPackRelationship(_TargetManagementPack, ManagementPackRelationshipIdentifier, ManagementPackAccessibility.Public);
            _ServiceComponentGroupRelationship.Abstract = false;
            _ServiceComponentGroupRelationship.Base = (ManagementPackElementReference<ManagementPackRelationship>)_SystemMP.GetRelationship("System.Containment");
            _ServiceComponentGroupRelationship.Source = new ManagementPackRelationshipEndpoint((ManagementPackElement)_ComponentServiceClass, "source")
            {
                Type = (ManagementPackElementReference<ManagementPackClass>)_ComponentServiceClass
            };

            _ServiceComponentGroupRelationship.Target = new ManagementPackRelationshipEndpoint((ManagementPackElement)_ServiceComponentGroupClass, "target")
            {
                Type = (ManagementPackElementReference<ManagementPackClass>)_ServiceComponentGroupClass
            };

            _ServiceComponentGroupRelationship.Status = ManagementPackElementStatus.PendingAdd;

            return ManagementPackRelationshipIdentifier;
        }

        private void AddServiceComputerRelationshipToMP(string ManagementPackRelationshipIdentifier)
        {
            _SystemComputerRelationship = new ManagementPackRelationship(_TargetManagementPack, ManagementPackRelationshipIdentifier, ManagementPackAccessibility.Public);
            _SystemComputerRelationship.Abstract = false;
            _SystemComputerRelationship.Base = (ManagementPackElementReference<ManagementPackRelationship>)_SystemMP.GetRelationship("System.Containment");
            _SystemComputerRelationship.Source = new ManagementPackRelationshipEndpoint((ManagementPackElement)_ComponentServiceClass, "source")
            {
                Type = (ManagementPackElementReference<ManagementPackClass>)_ComponentServiceClass
            };

            _SystemComputerRelationship.Target = new ManagementPackRelationshipEndpoint((ManagementPackElement)_SystemComputerClass, "target")
            {
                Type = (ManagementPackElementReference<ManagementPackClass>)_SystemComputerClass
            };

            _SystemComputerRelationship.Status = ManagementPackElementStatus.PendingAdd;
        }

        private string AddServiceRelationshipToMP()
        {
            string ServiceRelationshipIdentifier = "SCMembership_" + BuildRelationshipID(_ThisServiceClass.Name);
            // This creates the bottom-most RelationshipType
            _ServiceRelationship = new ManagementPackRelationship(_TargetManagementPack, ServiceRelationshipIdentifier, ManagementPackAccessibility.Public);
            _ServiceRelationship.Abstract = false;
            _ServiceRelationship.Base = (ManagementPackElementReference<ManagementPackRelationship>)_SystemMP.GetRelationship("System.Containment");
            _ServiceRelationship.Source = new ManagementPackRelationshipEndpoint((ManagementPackElement)_ComponentServiceClass, "source") // _ComponentServiceClass replaced _ThisServiceClass
            {
                Type = (ManagementPackElementReference<ManagementPackClass>)_ComponentServiceClass
            };

            _ServiceRelationship.Target = new ManagementPackRelationshipEndpoint((ManagementPackElement)_SystemComputerClass, "target") // _SystemComputerClass replaced _ComponentServiceClass
            {
                Type = (ManagementPackElementReference<ManagementPackClass>)_SystemComputerClass
            };

            _ServiceRelationship.Status = ManagementPackElementStatus.PendingAdd;

            return ServiceRelationshipIdentifier;

            /*
            <RelationshipType ID="SCMembership_41d2fb1642fe41d39f75b23703c74c87" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="Service_7951fceff3f040b1abd3924e054b6352" />
                <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="SC_0b3690794f384a6282d844b4ff71105e_Service_7951fceff3f040b1abd3924e054b6352" />
            </RelationshipType> 
             * 
             * 
            <DisplayString ElementID="SCMembership_41d2fb1642fe41d39f75b23703c74c87">
                <Name>Service Relationship</Name>
            </DisplayString>
             */
        }

        private string AddServerRelationshipToMP()
        {
            string ServerRelationshipIdentifier = "ServiceComputerMembership_" + BuildRelationshipID(_ThisServiceClass.Name);
            // This creates the middle RelationshipType
            _ServerRelationship = new ManagementPackRelationship(_TargetManagementPack, ServerRelationshipIdentifier, ManagementPackAccessibility.Public);
            _ServerRelationship.Abstract = false;
            _ServerRelationship.Base = (ManagementPackElementReference<ManagementPackRelationship>)_SystemMP.GetRelationship("System.Containment");
            _ServerRelationship.Source = new ManagementPackRelationshipEndpoint((ManagementPackElement)_ComponentServiceClass, "source")
            {
                Type = (ManagementPackElementReference<ManagementPackClass>)_ThisServiceClass
            };

            _ServerRelationship.Target = new ManagementPackRelationshipEndpoint((ManagementPackElement)_SystemComputerClass, "target") //_ComponentServiceClass, "target")
            {
                Type = (ManagementPackElementReference<ManagementPackClass>)_ComponentServiceClass
            };

            _ServerRelationship.Status = ManagementPackElementStatus.PendingAdd;

            return ServerRelationshipIdentifier;

            /*
            <RelationshipType ID="NEW_RELATIONSHIP" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
              <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="FIN.DA.ADDTF_ComponentGroup" />
              <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="SystemLibrary7585010!System.Computer" />
            </RelationshipType> 
             * 
             * 
            <DisplayString ElementID="SCMembership_41d2fb1642fe41d39f75b23703c74c87">
                <Name>Service Relationship</Name>
            </DisplayString>
             */
        }

        private void AddViews()
        {
            ManagementPackFolderItem ServiceStateFolderItem;
            ManagementPackFolder ViewFolder;
            ManagementPackView ServiceStateView;
            ManagementPackView DiagramStateView;

            ServiceStateView = new ManagementPackView(_TargetManagementPack, _TargetManagementPack.Name + ".ServiceStateView", ManagementPackAccessibility.Public);
            DiagramStateView = new ManagementPackView(_TargetManagementPack, _TargetManagementPack.Name + ".DiagramStateView", ManagementPackAccessibility.Public);
            ViewFolder = new ManagementPackFolder(_TargetManagementPack, _TargetManagementPack.Name + ".ViewFolder", ManagementPackAccessibility.Public);

            ViewFolder.Description = _TargetManagementPack.Name + " View Folder";
            ViewFolder.DisplayName = _TargetManagementPack.Name;
            ViewFolder.Status = ManagementPackElementStatus.PendingAdd;
            ViewFolder.ParentFolder = _SystemCenterMP.GetFolder("Microsoft.SystemCenter.Monitoring.ViewFolder.Root");

            ServiceStateView.Target = _ThisServiceClass;
            ServiceStateView.TypeID = _SystemCenterMP.GetViewType("Microsoft.SystemCenter.StateViewType");
            ServiceStateView.Description = _TargetManagementPack.Name + " Service State View";
            ServiceStateView.DisplayName = _TargetManagementPack.Name + " Service State View";
            ServiceStateView.Category = "Operations";
            ServiceStateView.Configuration = @"<Criteria><InMaintenanceMode>false</InMaintenanceMode></Criteria>";

            ServiceStateFolderItem = new ManagementPackFolderItem(ServiceStateView, ViewFolder);

            DiagramStateView.Target = _ThisServiceClass;
            DiagramStateView.TypeID = _SystemCenterMP.GetViewType("Microsoft.SystemCenter.DiagramViewType");
            DiagramStateView.Description = _TargetManagementPack.Name + " Service Diagram View";
            DiagramStateView.DisplayName = _TargetManagementPack.Name + " Service Diagram View";
            DiagramStateView.Category = "Operations";
            ServiceStateFolderItem = new ManagementPackFolderItem(DiagramStateView, ViewFolder);

            ServiceStateFolderItem.Status = ManagementPackElementStatus.PendingAdd;
            ServiceStateView.Status = ManagementPackElementStatus.PendingAdd;
            DiagramStateView.Status = ManagementPackElementStatus.PendingAdd;
        }

        [Obsolete ("This method is obsolete")]
        private bool CreateNewManagementPack(Version NewVersion, List<string> ComponentServerNames, List<string> ComponentServiceNames, ManagementPackCreationOptions CreationOptions)
        {
            bool Result = false;
            ManagementPack MP = null;
            string ManagementPackFilename = CreationOptions.ManagementPackFilename.Replace("#SERVICENAME#", CreationOptions.ServiceInfo.ServiceName.Replace(" ", "_").Replace("(", "").Replace(")", ""));
            string DestinationPath = "";
            string SealedMPFilename = "";
            string FullSealedMPFilename = "";
            string ServiceRelationshipName = "";
            string ErrorMessage = "";
            Int32 ProgressCount = 0;

            _MPStore.AddDirectory(CreationOptions.ConnectionInfo.ReferencePath);

            try
            {
                ProgressCount = 1;
                MP = new ManagementPack(CreationOptions.ManagementPackID, CreationOptions.ManagementPackName, NewVersion, _MPStore);

                ProgressCount = 2;
                MP.Description = CreationOptions.ManagementPackDescription;
                MP.DisplayName = CreationOptions.ManagementPackName;
                MP.DefaultLanguageCode = "ENU";

                ProgressCount = 3;
                DestinationPath = System.IO.Path.GetDirectoryName(ManagementPackFilename);
                SealedMPFilename = System.IO.Path.GetFileNameWithoutExtension(ManagementPackFilename) + ".mp";
                FullSealedMPFilename = System.IO.Path.Combine(DestinationPath, SealedMPFilename);

                ProgressCount = 4;
                _TargetManagementPack = MP;
                AddReferencesToMP(CreationOptions.ConnectionInfo.ReferencePath);

                ProgressCount = 5;
                #region Management Pack detail

                // Adds the following:
                /*
                  <ClassTypes>
                    <ClassType ID="FIN.DA.ADDTF" Accessibility="Public" Abstract="false" Base="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.GenericService" Hosted="false" Singleton="true" Extension="false" />
                    <ClassType ID="FIN.DA.ADDTF_ComponentGroup" Accessibility="Public" Abstract="false" Base="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup" Hosted="false" Singleton="true" Extension="false" />
                  </ClassTypes>
                 * */

                #endregion
                AddClassesToMP(CreationOptions);

                ProgressCount = 6;
                #region Management Pack detail
                // Adds the following:
                /*
                  <RelationshipTypes>
                    <RelationshipType ID="SCIMembership_46494E444141444454465F436F6D706F6E656E7447726F7570315F5F5F5F5F5F46494E444141444454465F436F6D706F6E656E7447726F7570325F5F5F5F5F5F" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                      <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="FIN.DA.ADDTF_ComponentGroup" />
                      <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="SystemLibrary7585010!System.Computer" />
                    </RelationshipType>
                  </RelationshipTypes> 
                 */
                #endregion
                AddComponentRelationshipsToMP();

                if (ComponentServiceNames.Count > 0)
                {
                    ProgressCount = 7;
                    //CreationOptions.RelationshipName = AddServerRelationshipToMP();
                }

                ProgressCount = 8;
                #region Management Pack detail
                // Adds the following:
                /*
                    <RelationshipType ID="SCMembership_46494E44414144445446315F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F46494E44414144445446325F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                      <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="FIN.DA.ADDTF" />
                      <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="FIN.DA.ADDTF_ComponentGroup" />
                    </RelationshipType> 
                 */
                #endregion
                AddServiceRelationshipToMP();

                ProgressCount = 9;
                #region Management Pack detail
                // Adds the following:
                /*
                  <Monitoring>
                    <Discoveries>
                      <Discovery ID="FIN.DA.ADDTF_SCPopulation" Enabled="true" Target="FIN.DA.ADDTF" ConfirmDelivery="false" Remotable="true" Priority="Normal">
                        <Category>Discovery</Category>
                        <DiscoveryTypes />
                        <DataSource ID="DS" TypeID="SystemCenter!Microsoft.SystemCenter.GroupPopulator">
                          <RuleId>$MPElement$</RuleId>
                          <GroupInstanceId>$Target/Id$</GroupInstanceId>
                          <MembershipRules>
                            <MembershipRule>
                              <MonitoringClass>$MPElement[Name="FIN.DA.ADDTF_ComponentGroup"]$</MonitoringClass>
                              <RelationshipClass>$MPElement[Name="SCMembership_46494E44414144445446315F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F46494E44414144445446325F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F5F"]$</RelationshipClass>
                            </MembershipRule>
                          </MembershipRules>
                        </DataSource>
                      </Discovery>
                    </Discoveries>
                  </Monitoring>
                */
                #endregion
                AddComponentDiscoveryToMP();

                ProgressCount = 10;
                #region Management Pack detail
                // Adds the following:
                /*
                  <Discovery ID="FIN.DA.ADDTF_ComponentGroup_ItemPopulation" Enabled="true" Target="FIN.DA.ADDTF_ComponentGroup" ConfirmDelivery="false" Remotable="true" Priority="Normal">
                    <Category>Discovery</Category>
                    <DiscoveryTypes />
                    <DataSource ID="DS" TypeID="SystemCenter!Microsoft.SystemCenter.GroupPopulator">
                      <RuleId>$MPElement$</RuleId>
                      <GroupInstanceId>$Target/Id$</GroupInstanceId>
                      <MembershipRules>
                        <MembershipRule>
                          <MonitoringClass>$MPElement[Name="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.Service"]$</MonitoringClass>
                          <RelationshipClass>$MPElement[Name="SCIMembership_46494E444141444454465F436F6D706F6E656E7447726F7570315F5F5F5F5F5F46494E444141444454465F436F6D706F6E656E7447726F7570325F5F5F5F5F5F"]$</RelationshipClass>
                          <IncludeList>
                            <MonitoringObjectId>{9ff8ee98-8749-2007-1a46-3709e9b3e771}</MonitoringObjectId>
                            <MonitoringObjectId>{3b4954d7-a9f6-833d-bbd4-1ef9709cc393}</MonitoringObjectId>
                          </IncludeList>
                        </MembershipRule>
                        <MembershipRule>
                          <MonitoringClass>$MPElement[Name="SystemLibrary7585010!System.Computer"]$</MonitoringClass>
                          <RelationshipClass>$MPElement[Name="SCIMembership_46494E444141444454465F436F6D706F6E656E7447726F7570315F5F5F5F5F5F46494E444141444454465F436F6D706F6E656E7447726F7570325F5F5F5F5F5F"]$</RelationshipClass>
                          <IncludeList>
                            <MonitoringObjectId>{b804de67-3077-3743-3f88-cf2bf8c3e203}</MonitoringObjectId>
                            <MonitoringObjectId>{d07961e3-0e2a-8789-21db-b8cb358ac11b}</MonitoringObjectId>
                            <MonitoringObjectId>{f1297eb7-f278-8c11-49e4-5ddb7c13739d}</MonitoringObjectId>
                            <MonitoringObjectId>{3689fd86-ddee-d6d7-1b86-b146f9bb0678}</MonitoringObjectId>
                          </IncludeList>
                        </MembershipRule>
                      </MembershipRules>
                    </DataSource>
                  </Discovery>
                */
                #endregion
                AddRelationshipsToMP(ComponentServerNames, ComponentServiceNames, CreationOptions, ref ErrorMessage);

                if (ErrorMessage != "")
                {
                    Debug.WriteLine(ErrorMessage);
                    Result = false;
                }
                else
                {
                    ProgressCount = 11;
                    AddViews();

                    ProgressCount = 12;
                    Result = SaveManagementPack(ManagementPackFilename);

                }
            }
            catch (Exception e)
            {
                switch (ProgressCount)
                {
                    case 0:
                        Debug.WriteLine("An unknown error has occured creating the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                    case 1:
                        Debug.WriteLine("An error has occured creating the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                    case 2:
                        Debug.WriteLine("An error has occured setting the new Management Pack properties.  The error was:\n" + e.ToString());
                        break;
                    case 3:
                        Debug.WriteLine("An error has occured setting the new Management Pack filename.  The error was:\n" + e.ToString());
                        break;
                    case 4:
                        Debug.WriteLine("An error has occured adding references to the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                    case 5:
                        Debug.WriteLine("An error has occured adding classes to the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                    case 6:
                        Debug.WriteLine("An error has occured adding component relationships to the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                    case 7:
                        Debug.WriteLine("An error has occured adding server relationships to the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                    case 8:
                        Debug.WriteLine("An error has occured adding service relationships to the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                    case 9:
                        Debug.WriteLine("An error has occured adding component discovery to the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                    case 10:
                        Debug.WriteLine("An error has occured adding relationships to the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                    case 11:
                        Debug.WriteLine("An error has occured adding views to the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                    case 12:
                        Debug.WriteLine("An error has occured saving the new Management Pack.  The error was:\n" + e.ToString());
                        break;
                }
            }
            return Result;
        }

        private bool CreateManagementPackXML(Version NewVersion, List<string> ComponentServerNames, List<string> ComponentServiceNames, ManagementPackCreationOptions CreationOptions, out string ErrorMessage, out string OutputXML)
        {
            bool Result = false;
            int LoopCounter = 0;

            ErrorMessage = "";

            switch (ComponentServerNames.Count)
            {
                case 0:
                    switch (ComponentServiceNames.Count)
                    {
                        case 0:
                            OutputXML = CreationOptions.ManagementPackEmptyTemplate;
                            break;
                        default:
                            OutputXML = CreationOptions.ManagementPackServicesTemplate;
                            break;
                    }
                    break;
                default:
                    switch (ComponentServiceNames.Count)
                    {
                        case 0:
                            OutputXML = CreationOptions.ManagementPackServersTemplate;
                            break;
                        default:
                            OutputXML = CreationOptions.ManagementPackServersAndServicesTemplate;
                            break;
                    }
                    break;
            }

            StringBuilder Services = new StringBuilder();

            foreach (string ServiceName in ComponentServiceNames)
            {
                LoopCounter++;

                // TODO:  If the dependant service does not exist, retry as it should be in there...

                // Search
                string ServiceID = GetServiceIDFromName(ServiceName);
                if (ServiceID != "")
                {
                    Services.Append("<MonitoringObjectId>{" + ServiceID + "}</MonitoringObjectId>\r\n");
                }
                else
                {
                    ErrorMessage = "Could not find dependant (" + ServiceName + ") in SCOM Database .  This Management Pack (" + CreationOptions.ManagementPackName + ") for " + CreationOptions.ServiceInfo.ServiceName + " cannot be created.";
                    OutputXML = "";
                    return false;
                }
            }

            StringBuilder ServiceIncludeList = new StringBuilder();
            ServiceIncludeList.Append("<IncludeList>\r\n");
            ServiceIncludeList.Append(Services.ToString());
            ServiceIncludeList.Append("</IncludeList>\r\n");

            StringBuilder Servers = new StringBuilder();

            foreach (string ServerName in ComponentServerNames)
            {
                LoopCounter++;

                // TODO:  If the dependant service does not exist, retry as it should be in there...

                // Search
                string ServerID = GetServerIDFromName(ServerName);
                if (ServerID != "")
                {
                    Servers.Append("<MonitoringObjectId>{" + ServerID + "}</MonitoringObjectId>\r\n");
                }
                else
                {
                    ErrorMessage = "Could not find dependant (" + ServerName + ") in SCOM Database .  This Management Pack (" + CreationOptions.ManagementPackName + ") for " + CreationOptions.ServiceInfo.ServiceName + " cannot be created.";
                    OutputXML = "";
                    return false;
                }
            }

            StringBuilder ServerIncludeList = new StringBuilder();
            ServerIncludeList.Append("<IncludeList>\r\n");
            ServerIncludeList.Append(Servers.ToString());
            ServerIncludeList.Append("</IncludeList>\r\n");
            

            // Start replacing the tags...
            OutputXML = OutputXML.Replace("#MANAGEMENTPACK_ID#", CreationOptions.ManagementPackID);
            OutputXML = OutputXML.Replace("#MANAGEMENTPACK_VERSION#", CreationOptions.ManagementPackVersion.ToString());
            OutputXML = OutputXML.Replace("#MANAGEMENTPACK_NAME#", CreationOptions.ManagementPackName);
            OutputXML = OutputXML.Replace("#MANAGEMENTPACK_SHORTNAME#", CreationOptions.ServiceInfo.ServiceName.Replace(" ", "")
                                                                                                               .Replace("(", "")
                                                                                                               .Replace(")", "")
                                                                                                               .Replace(@"/", "_")
                                                                                                               .Replace(@"\", "_"));

            OutputXML = OutputXML.Replace("#GUID1#", Guid.NewGuid().ToString());
            OutputXML = OutputXML.Replace("#GUID2#", Guid.NewGuid().ToString());
            OutputXML = OutputXML.Replace("#GUID3#", Guid.NewGuid().ToString());
            OutputXML = OutputXML.Replace("#SERVICE_NAME#", CreationOptions.ServiceInfo.ServiceName);

            OutputXML = OutputXML.Replace("#SERVER_INCLUDELIST#", ServerIncludeList.ToString());
            OutputXML = OutputXML.Replace("#SERVICE_INCLUDELIST#", ServiceIncludeList.ToString());

            System.IO.File.WriteAllText(CreationOptions.ManagementPackFilename, OutputXML);

            _TargetManagementPack = new ManagementPack(CreationOptions.ManagementPackFilename);

            Result = true;

            return Result;
        }

        private ManagementPackCriteria GetCriteria(string Name)
        {
            string Query = "Name Like '%" + Name + "%'";

            return new ManagementPackCriteria(Query);
        }

        private ManagementPack GetManagementPack(string Name)
        {
            ManagementPackCriteria Criteria = GetCriteria(Name);
            IList<ManagementPack> Packs = null;

            Packs = _SCOMServerConnection.ManagementPacks.GetManagementPacks(Criteria);

            if (Packs.Count > 0)
            {
                return Packs[0];
            }

            return null;
        }

        private string GetServerIDFromName(string ServerName)
        {
            string ServerID = "";

            MonitoringClassCriteria ServerClassCriteria = new MonitoringClassCriteria("Name = 'System.Computer'");
            ReadOnlyCollection<MonitoringClass> ServerMonitoringClasses = _SCOMServerConnection.GetMonitoringClasses(ServerClassCriteria);

            MonitoringObjectCriteria ServerObjectCriteria = new MonitoringObjectCriteria("Name like '" + ServerName + "%'", ServerMonitoringClasses[0]);
            ReadOnlyCollection<MonitoringObject> ServiceObjects = _SCOMServerConnection.GetMonitoringObjects(ServerObjectCriteria);

            if (ServiceObjects.Count > 0)
            {
                ServerID = ServiceObjects[0].Id.ToString();
            }

            return ServerID;

        }

        private string GetServerNameFromID(string ServerID)
        {
            // http://blog.coretech.dk/jgs/scom2012-using-the-get-scomalert-criteria-parameter-complete-reference/

            string ServerName = "";

            MonitoringClassCriteria ServerClassCriteria = new MonitoringClassCriteria("Name = 'System.Computer'");
            ReadOnlyCollection<MonitoringClass> ServerMonitoringClasses = _SCOMServerConnection.GetMonitoringClasses(ServerClassCriteria);

            try
            {
                MonitoringObjectCriteria ServiceObjectCriteria = new MonitoringObjectCriteria("Id = '" + ServerID + "'", ServerMonitoringClasses[0]);
                ReadOnlyCollection<MonitoringObject> ServiceObjects = _SCOMServerConnection.GetMonitoringObjects(ServiceObjectCriteria);

                if (ServiceObjects.Count > 0)
                {
                    ServerName = ServiceObjects[0].Name.ToString();
                }

                if (ServerName.Contains("."))
                {
                    // Remove everything to the right of the first "." in the name
                    ServerName = ServerName.Substring(0, ServerName.IndexOf("."));
                }

                return ServerName;
            }
            catch (Exception e)
            {
                return "";
            }

        }

        private string GetServiceIDFromName(string ServiceName)
        {
            string ServiceID = "";
            MonitoringClassCriteria ServiceClassCriteria = new MonitoringClassCriteria("Name = 'Microsoft.SystemCenter.ServiceDesigner.GenericService'");  // DisplayName: Distributed Application Component

            try
            {
            ReadOnlyCollection<MonitoringClass> ServiceMonitoringClasses = _SCOMServerConnection.GetMonitoringClasses(ServiceClassCriteria);

            //if (ServiceName == "AD (DTF)")
            //{
            //    int i = 1;
            //}

            MonitoringObjectCriteria ServiceObjectCriteria = new MonitoringObjectCriteria("DisplayName Like '%" + ServiceName + "%'", ServiceMonitoringClasses[0]);

            
                ReadOnlyCollection<MonitoringObject> ServiceObjects = _SCOMServerConnection.GetMonitoringObjects(ServiceObjectCriteria);

                if (ServiceObjects.Count > 0)
                {
                    ServiceID = ServiceObjects[0].Id.ToString();
                }
            }
            catch (ServerDisconnectedException ex)
            {
                _SCOMServerConnection.Reconnect();
            }
            catch (Exception ex)
            {

            }
            return ServiceID;

        }

        private string GetServiceIDFromName_new(string ServiceName)
        {
            string ServiceID = "";

            IList<ManagementPack> Packs = null;
            //ManagementPackCriteria Criteria = GetCriteria(ServiceName);

            //Packs = _SCOMServerConnection.ManagementPacks.GetManagementPacks(Criteria);
            Packs = _SCOMServerConnection.ManagementPacks.GetManagementPacks(new ManagementPackCriteria("DisplayName Like '%" + ServiceName.Replace("(", "_").Replace(")", "_") + "%'"));

            if (Packs.Count > 0)
            {
                ServiceID = Packs[0].Id.ToString();
            }

            return ServiceID;

        }

        private string GetServiceNameFromID(string ServiceID)
        {
            // http://blog.coretech.dk/jgs/scom2012-using-the-get-scomalert-criteria-parameter-complete-reference/

            string ServiceName = "";
            MonitoringClassCriteria ServiceClassCriteria = new MonitoringClassCriteria("Name = 'Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup'");
            ReadOnlyCollection<MonitoringClass> ServiceMonitoringClasses = _SCOMServerConnection.GetMonitoringClasses(ServiceClassCriteria);

            try
            {
                MonitoringObjectCriteria ServiceObjectCriteria = new MonitoringObjectCriteria("Id = '" + ServiceID + "'", ServiceMonitoringClasses[0]);
                ReadOnlyCollection<MonitoringObject> ServiceObjects = _SCOMServerConnection.GetMonitoringObjects(ServiceObjectCriteria);

                if (ServiceObjects.Count > 0)
                {
                    ServiceName = ServiceObjects[0].DisplayName.ToString();
                }
            }
            catch (Exception e)
            {
                return "";
            }

            return ServiceName;

        }

        private bool ImportManagementPack(bool Sealed)
        {
            bool Result = false;

            Debug.WriteLine("Importing...");

            try
            {
                if (Sealed)
                {
                    ManagementPack SealedMP = new ManagementPack(_SealedFilename);
                    _SCOMServerConnection.ManagementPacks.ImportManagementPack(SealedMP);
                }
                else
                {
                    _SCOMServerConnection.ManagementPacks.ImportManagementPack(_TargetManagementPack);
                }
                Result = true;
                Debug.WriteLine("Success");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failure: " + e.ToString());
            }

            return Result;
        }

        private bool SaveManagementPack(string Path)
        {
            bool Result = false;
            string MPDirectory = System.IO.Path.GetDirectoryName(Path);
            ManagementPackXmlWriter MPxmlWriter = new ManagementPackXmlWriter(MPDirectory);
            string XMLFilename = "";
            string XMLText = "";

            try
            {
                _TargetManagementPack.AcceptChanges();
            }

            catch (Exception e)
            {
                Debug.WriteLine("Error whilst performing ManagementPack.AcceptChanges().  The error was:\n" + e.ToString());

                return false;
            }

            try
            {
                MPxmlWriter.WriteManagementPack(_TargetManagementPack);
            }

            catch (Exception e)
            {
                Debug.WriteLine("Error whilst writing ManagementPack XML to file");
                return false;
            }

            try
            {
                /* The following is a Kludge to fix the problem with unreferenced elements
                 * i.e. <Target ID="Target" MinCardinality="0" MaxCardinality="2147483647" Type="Microsoft.Windows.Server.Computer" />
                 * -->  <Target ID="Target" MinCardinality="0" MaxCardinality="2147483647" Type="Windows!Microsoft.Windows.Server.Computer" />
                 * */
                XMLFilename = System.IO.Path.Combine(MPDirectory, _TargetManagementPack.Name + ".xml");
                XMLText = System.IO.File.ReadAllText(XMLFilename);

                XMLText = XMLText.Replace("Type=\"System.Computer", "Type=\"SystemLibrary7585010!System.Computer");
                //XMLText = XMLText.Replace("Type=\"Microsoft.Unix.", "Type=\"Unix!Microsoft.Unix.");
                XMLText = XMLText.Replace("Type=\"Microsoft.SystemCenter.", "Type=\"MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.");
                System.IO.File.WriteAllText(XMLFilename, XMLText);

                Debug.WriteLine("Management pack saved to " + MPDirectory + @"\" + _TargetManagementPack.Name + ".xml");

                // Load the newly saved MP into memory, replacing the in-memory one
                // This is necessary as without this step, the MP import fails.
                _TargetManagementPack.Dispose();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error whilst replacing Management Pack reference text");
                return false;
            }

            try
            {
                _TargetManagementPack = new ManagementPack(XMLFilename);
                Result = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error whilst loading replaced Management Pack");
                return false;
            }

            return Result;


            // Experiment:  Rename, then save again
            //File.Move(XMLFilename, System.IO.Path.Combine(MPDirectory, _NewMP.Name + " (1).xml"));
            //namespace MPxmlWriter.WriteManagementPack(_NewMP);
            //File.Move(XMLFilename, System.IO.Path.Combine(MPDirectory, _NewMP.Name + " (2).xml"));
        }

        private bool SealManagementPack(ManagementPackCreationOptions CreationOptions)
        {
            bool Result = false;
            string ManagementPackFilename = CreationOptions.ManagementPackFilename.Replace("#SERVICENAME#", CreationOptions.ServiceInfo.ServiceName.Replace(" ", "_").Replace("(", "").Replace(")", ""));
            string DestinationPath = System.IO.Path.GetDirectoryName(CreationOptions.ManagementPackFilename);
            string SealedMPFilename = System.IO.Path.GetFileNameWithoutExtension(ManagementPackFilename) + ".mp";
            string FullSealedMPFilename = System.IO.Path.Combine(DestinationPath, SealedMPFilename);

            Debug.WriteLine("...Sealing...");

            string Commandline = CreationOptions.SealOptions.ManagementPackSealCommandLine.Replace("#MPFILENAME#", CreationOptions.ManagementPackFilename).Replace("#KEYFILEFILENAME#", CreationOptions.SealOptions.KeyfileFilename).Replace("#KEYCOMPANYNAME#", CreationOptions.SealOptions.KeyCompanyName).Replace("#MPPATH#", DestinationPath);

            InitialSessionState Session = InitialSessionState.CreateDefault();

            Session.ImportPSModule(new string[] { CreationOptions.SealOptions.PowerShellModulePath });
            Runspace runspace = RunspaceFactory.CreateRunspace(Session);
            runspace.Open();

            PowerShell ps = PowerShell.Create();
            ps.Runspace = runspace;
            ps.AddScript(Commandline);
            ps.Invoke();

            if (ps.Streams.Error.Count > 0)
            {
                Result = false;
                Debug.WriteLine("Failure");
            }
            else
            {
                _SealedFilename = FullSealedMPFilename;
                Result = true;
                Debug.WriteLine("Success...");
            }

            return Result;
        }

        private bool UpdateExistingManagementPack(Version NewVersion, List<string> ComponentServerNames, List<string> ComponentServiceNames, ManagementPackCreationOptions CreationOptions)
        {
            bool Result = false;
            ManagementPack ExistingManagementPack = GetManagementPack(CreationOptions.ManagementPackID);
            bool FoundAtLeast1RelationshipName = false;
            string ServiceComponentGroupClassTypeID = "";
            int FoundDependants = 0;
            string DiscoveryName = "";
            int DependantObjectCount = 0;
            bool ManagementPackUpdateRequired = false;
            string OtherRelationshipName = "";

            if (ExistingManagementPack.GetRelationships().Count == 0)
            {
                // No existing Relationships
                ManagementPackUpdateRequired = true;
            }
            {
                // To update the MP, we need to use the existing MP names
                foreach (ManagementPackRelationship Relationship in ExistingManagementPack.GetRelationships())
                {
                    switch (Relationship.Target.Type.Identifier.Path[0])
                    {
                        case "System.Computer":
                            _SystemComputerRelationshipName = Relationship.Name;
                            FoundAtLeast1RelationshipName = true;
                            break;
                        case "Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup":
                            _ServiceComponentGroupRelationshipName = Relationship.Name;
                            FoundAtLeast1RelationshipName = true;
                            break;
                        default:
                            OtherRelationshipName = Relationship.Name;
                            FoundAtLeast1RelationshipName = true;
                            break;
                    }
                }

                if (!FoundAtLeast1RelationshipName)
                {
                    Debug.WriteLine("------Management Pack relationships could not be matched between the existing MP and the update required.");
                    return false;
                }

                ManagementPackElementCollection<ManagementPackClass> Classes = ExistingManagementPack.GetClasses();

                foreach (ManagementPackClass Class in Classes)
                {
                    if (Class.Base.Identifier.Path[0].ToString() == "Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup")
                    {
                        ServiceComponentGroupClassTypeID = Class.Identifier.Path[0].ToString();
                    }
                }

                int DiscoveryCount = ExistingManagementPack.GetDiscoveries().Count;

                if (DiscoveryCount > 0)
                {
                    foreach (ManagementPackDiscovery Discovery in ExistingManagementPack.GetDiscoveries())
                    {
                        string DiscoveryTarget = Discovery.Target.Identifier.Path[0].ToString();

                        FoundDependants = 0;

                        if (DiscoveryTarget == ServiceComponentGroupClassTypeID)
                        {
                            DiscoveryName = Discovery.Name;

                            string DiscoveryConfiguration = Discovery.DataSource.Configuration;
                            var doc = XElement.Parse("<root>" + DiscoveryConfiguration + "</root>");

                            IEnumerable<XElement> MonitoringObjectList = doc.Descendants("MembershipRules").Descendants("MembershipRule").Descendants("IncludeList").Descendants();

                            // First check... Is there the same number of dependant objects?
                            if (MonitoringObjectList.Count() != DependantObjectCount)
                            {
                                ManagementPackUpdateRequired = true;
                            }
                            else
                            {
                                // Same number of dependant objects - are they the same though?

                                // Get the names of these objects...
                                List<string> DependantServerNames = new List<string>();
                                List<string> DependantServiceNames = new List<string>();

                                foreach (XElement MonitoringObject in MonitoringObjectList)
                                {
                                    string DependantObjectID = MonitoringObject.Value.Replace("{", "").Replace("}", "");
                                    string DependantServerName = "";
                                    string DependantServiceName = "";
                                    bool FoundDependantObject = false;

                                    // Assume the object is a Server and search for that first
                                    DependantServerName = GetServerNameFromID(DependantObjectID);

                                    if (DependantServerName == "")
                                    {
                                        // If a Server of that name was not found, then search for a Service by that name
                                        DependantServiceName = GetServiceNameFromID(DependantObjectID);
                                        if (DependantServiceName != "")
                                        {
                                            DependantServiceNames.Add(DependantServiceName.ToUpper());
                                            FoundDependantObject = true;
                                        }
                                    }
                                    else
                                    {
                                        DependantServerNames.Add(DependantServerName.ToUpper());
                                        FoundDependantObject = true;
                                    }

                                    if (FoundDependantObject)
                                    {
                                        ManagementPackUpdateRequired = true;
                                    }
                                    else
                                    {
                                        ManagementPackUpdateRequired = false;
                                        Result = true;
                                    }
                                }

                                if (DependantServiceNames.Count + DependantServerNames.Count == DependantObjectCount)
                                {
                                    // Ok, so we expected to find x Objects, and did so

                                    // Finally, check that the names of the objects we found correspond with what we expected
                                    FoundDependants = 0;
                                    foreach (string DependantServerName in ComponentServiceNames)
                                    {
                                        if (DependantServerNames.Contains(DependantServerName))
                                        {
                                            FoundDependants++;
                                        }
                                    }

                                    foreach (string DependantServiceName in ComponentServiceNames)
                                    {
                                        string Searchstring = DependantServiceName;
                                        if (DependantServiceNames.Contains(Searchstring.ToUpper()))
                                        {
                                            FoundDependants++;
                                        }
                                    }

                                    ManagementPackUpdateRequired = false;
                                    Result = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ComponentServiceNames.Count > 0 || ComponentServerNames.Count > 0)
                    {
                        // Discoveries = 0 but there is 1 or more Services or Servers, so the existing MP needs to be updated
                        ManagementPackUpdateRequired = true;
                    }
                }
            }

            if (ManagementPackUpdateRequired)
            {
                Debug.WriteLine(".........Creating new Management Pack as an update could not be performed");
                //Result = CreateNewManagementPack(NewVersion, ComponentServerNames, ComponentServiceNames, CreationOptions);

                string XML = "";
                string Error = "";

                Result = CreateManagementPackXML(NewVersion, ComponentServerNames, ComponentServiceNames, CreationOptions, out Error, out XML);
            }

            return Result;
        }

        #endregion

        #region Public Methods

        public static string BuildRelationshipID(string RelationshipName)
        {
            return BuildRelationshipID(RelationshipName + "1", RelationshipName + "2");
        }

        public static string BuildRelationshipID(string RelationshipName1, string RelationshipName2)
        {
            //return (Guid.NewGuid()).ToString();
            Guid result = Guid.Empty;

            //StringBuilder sb = new StringBuilder();

            //string Prefix = RelationshipName1.Replace(".", "").PadRight(32, Convert.ToChar("_")).Substring(0, 32);
            //string Suffix = RelationshipName2.Replace(".", "").PadRight(32, Convert.ToChar("_")).Substring(0, 32);

            //byte[] PrefixBytes = Encoding.ASCII.GetBytes(Prefix);  // Turn this into an array of ASCII digits
            //byte[] SuffixBytes = Encoding.ASCII.GetBytes(Suffix);  // Turn this into an array of ASCII digits

            //// Now turn the 32 bytes into 64 characters by converting each byte into a string representation of it's hexadecimal value
            //foreach (byte Character in PrefixBytes)
            //{
            //    sb.Append(Character.ToHex());
            //}

            //foreach (byte Character in SuffixBytes)
            //{
            //    sb.Append(Character.ToHex());
            //}










            // Create a Guid from the Hash of the input strings...
            string input = RelationshipName1 + RelationshipName2;
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(input));
                result = new Guid(hash);
            }













            //return sb.ToString();
            return result.ToString();
        }

        public bool CreateOrUpdateManagementPack(string Name, Version Version, List<string> ComponentServerNames, List<string> ComponentServiceNames, ManagementPackCreationOptions CreationOptions, out string ErrorMessage)
        {
            bool Result = false;
            Version CurrentVersion = null;
            string ManagementPackXML = "";

            ErrorMessage = "";

            // There are 3 Versions to take into account..
            // 'Version' is the Version that we wish[ed] to use
            // 'CurrentVersion' is the version of the existing Management Pack
            // 'NewVersion' is the version of the Updated Management Pack

            if (ManagementPackExists(CreationOptions.ManagementPackID, ref CurrentVersion))
            {
                Debug.WriteLine("......Existing MP...");

                if (Version <= CurrentVersion)
                {
                    //Nothing to do
                    Debug.WriteLine("......Management Pack update not required... (" + Version + " <= " + CurrentVersion + ")");
                    Result = true;
                    return Result;
                }
                else
                {
                    Debug.WriteLine("......Management Pack update required...(" + Version + "> " + CurrentVersion + ")");
                    Result = UpdateExistingManagementPack(Version, ComponentServerNames, ComponentServiceNames, CreationOptions);
                }
            }
            else
            {
                Debug.WriteLine("......Creating new MP...");

                //Result = CreateNewManagementPack(Version, ComponentServerNames, ComponentServiceNames, CreationOptions);

                Result = CreateManagementPackXML(Version, ComponentServerNames, ComponentServiceNames, CreationOptions, out ErrorMessage, out ManagementPackXML);
            }

            // Seal and/or Import the Management Pack
            if (Result)
            {
                if (CreationOptions.SealOptions.SealManagementPack)
                {
                    if (CreationOptions.SealOptions.KeyfileFilename.Trim() != "" && System.IO.File.Exists(CreationOptions.SealOptions.KeyfileFilename.Trim()))
                    {
                        Result = SealManagementPack(CreationOptions);

                        if (Result)
                        {
                            if (CreationOptions.ImportManagementPack)
                            {
                                Result = ImportManagementPack(Sealed: true);
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine("...Unable to seal MP:  No valid Keyfile Filename specified");
                        Result = false;
                    }
                }
                else
                {
                    // Not going to seal
                    if (CreationOptions.ImportManagementPack)
                    {
                        Result = ImportManagementPack(Sealed: false);
                    }
                }
            }

            if (Result)
            {
                Debug.WriteLine("......Success");
            }
            else
            {
                Debug.WriteLine("......Failure");
            }

            return Result;
        }

        public string GetDatasource(List<string> ServerList, List<string> ServiceList, ManagementPackCreationOptions CreationOptions, ref string Errormessage)
        {
            int LoopCounter = 0;
            bool FoundAtLeast1Service = false;
            bool FoundAtLeast1Server = false;
            string ComponentGroupRelationshipName = "";
            string ComputerRelationshipName = "";

            if (_ServiceComponentGroupRelationshipName != null && _ServiceComponentGroupRelationshipName != "")
            {
                ComponentGroupRelationshipName = _ServiceComponentGroupRelationshipName;
            }

            if (_SystemComputerRelationshipName != null && _SystemComputerRelationshipName != "")
            {
                ComputerRelationshipName = _SystemComputerRelationshipName;
            }

            if (_ServiceComponentGroupRelationshipName != "" && ServiceList.Count > 0)
            {
                // We have Services to depend upon, but the existing MP doesn't have any Services.
                // This is a big change to the MP, but one that has to be catered for
                // Steps required:
                // Add References
                if (_SystemMP == null)
                {
                    _TargetManagementPack = _OriginalManagementPack;
                    AddReferencesToMP(CreationOptions.ConnectionInfo.ReferencePath);
                    AddClassesToMP(CreationOptions);

                }
                // Add a new Relationship (MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup)

                _ServiceComponentGroupRelationshipName = AddServiceComponentRelationshipToMP();
                // Add Discovery
                // Add Service(s) to IncludeList
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<RuleId>$MPElement$</RuleId>");
            stringBuilder.Append("<GroupInstanceId>$Target/Id$</GroupInstanceId>");
            stringBuilder.Append("<MembershipRules>");

            if (ServiceList.Count > 0)
            {
                MonitoringClassCriteria ServiceClassCriteria = new MonitoringClassCriteria("Name = 'Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup'");  // 'System.Service'
                ReadOnlyCollection<MonitoringClass> ServiceMonitoringClasses = _SCOMServerConnection.GetMonitoringClasses(ServiceClassCriteria);
                StringBuilder Services = new StringBuilder();

                foreach (string ServiceName in ServiceList)
                {
                    LoopCounter++;

                    // TODO:  If the dependant service does not exist, retry as it should be in there...

                    // Search
                    string ServiceID = GetServiceIDFromName(ServiceName);
                    if (ServiceID != "")
                    {
                        Services.Append("<MonitoringObjectId>{" + ServiceID + "}</MonitoringObjectId>");
                        FoundAtLeast1Service = true;
                    }
                    else
                    {
                        Errormessage = "Could not find dependant (" + ServiceName + ") in SCOM Database .  This Management Pack (" + CreationOptions.ManagementPackID + ") for " + CreationOptions.ServiceInfo.ServiceName + " cannot be created.";
                        return "";
                    }
                }

                // Because it is possible that we did not find any Service, we need to check...
                if (FoundAtLeast1Service)
                {
                    stringBuilder.Append("<MembershipRule>");
                    stringBuilder.Append("<MonitoringClass>$MPElement[Name=\"MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.Service\"]$</MonitoringClass>");  // MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup

                    if (ComponentGroupRelationshipName != "")
                    {
                        // Use the pre-allocated name
                        stringBuilder.Append("<RelationshipClass>$MPElement[Name=\"" + ComponentGroupRelationshipName + "\"]$</RelationshipClass>");
                    }
                    else
                    {
                        stringBuilder.Append("<RelationshipClass>$MPElement[Name=\"" + _ServiceComponentGroupRelationship.Name + "\"]$</RelationshipClass>");
                    }

                    if (CreationOptions.UseDynamicDiscovery)
                    {
                        // The following will allow us to add a single entity name, but not multiple

                        /*
                          stringBuilder.Append("<Expression>");
                            stringBuilder.Append("<SimpleExpression>");
                              stringBuilder.Append("<ValueExpression>");
                                stringBuilder.Append("<Property>$MPElement[Name="System!System.Entity"]/DisplayName$</Property>");
                              stringBuilder.Append("</ValueExpression>");
                              stringBuilder.Append("<Operator>Equal</Operator>");
                              stringBuilder.Append("<ValueExpression>");
                                stringBuilder.Append("<Value>Gargantua</Value>");
                              stringBuilder.Append("</ValueExpression>");
                            stringBuilder.Append("</SimpleExpression>");
                          stringBuilder.Append("</Expression>");
                       */

                        // For now, just do exactly the same as before.
                        stringBuilder.Append("<IncludeList>");
                        stringBuilder.Append(Services.ToString());
                        stringBuilder.Append("</IncludeList>");
                        stringBuilder.Append("</MembershipRule>");
                    }
                    else
                    {
                        stringBuilder.Append("<IncludeList>");
                        stringBuilder.Append(Services.ToString());
                        stringBuilder.Append("</IncludeList>");
                        stringBuilder.Append("</MembershipRule>");
                    }
                }
            }

            stringBuilder.Append("<MembershipRule>");
            stringBuilder.Append("<MonitoringClass>$MPElement[Name=\"SystemLibrary7585010!System.Computer\"]$</MonitoringClass>");

            if (ComputerRelationshipName != "")
            //if (_ServiceRelationship.Name != "")
            {
                // Use the pre-allocated name
                stringBuilder.Append("<RelationshipClass>$MPElement[Name=\"" + ComputerRelationshipName + "\"]$</RelationshipClass>");
                //stringBuilder.Append("<RelationshipClass>$MPElement[Name=\"" + _ServiceRelationship.Name + "\"]$</RelationshipClass>");
            }
            else
            {
                stringBuilder.Append("<RelationshipClass>$MPElement[Name=\"" + _SystemComputerRelationship.Name + "\"]$</RelationshipClass>");
            }

            if (ServerList.Count > 0)
            {
                StringBuilder Servers = new StringBuilder();

                foreach (string ServerName in ServerList)
                {
                    LoopCounter++;

                    // Search
                    string ServerID = GetServerIDFromName(ServerName);
                    if (ServerID != "")
                    {
                        Servers.Append("<MonitoringObjectId>{" + ServerID + "}</MonitoringObjectId>");
                        FoundAtLeast1Server = true;
                    }
                    else
                    {
                        Errormessage = "Could not find dependant (" + ServerName + ") in SCOM Database .  This Management Pack (" + CreationOptions.ManagementPackID + ") for " + CreationOptions.ServiceInfo.ServiceName + " cannot be created.";
                        return "";
                    }
                }

                // Because it is possible that we did not find any Server, we need to check...
                if (FoundAtLeast1Server)
                {
                    if (CreationOptions.UseDynamicDiscovery)
                    {
                        // The following will allow us to add a single entity name, but not multiple

                        /*
                          stringBuilder.Append("<Expression>");
                            stringBuilder.Append("<SimpleExpression>");
                              stringBuilder.Append("<ValueExpression>");
                                stringBuilder.Append("<Property>$MPElement[Name="System!System.Entity"]/DisplayName$</Property>");
                              stringBuilder.Append("</ValueExpression>");
                              stringBuilder.Append("<Operator>Equal</Operator>");
                              stringBuilder.Append("<ValueExpression>");
                                stringBuilder.Append("<Value>Gargantua</Value>");
                              stringBuilder.Append("</ValueExpression>");
                            stringBuilder.Append("</SimpleExpression>");
                          stringBuilder.Append("</Expression>");
                       */

                        // For now, just do exactly the same as before.
                        stringBuilder.Append("<IncludeList>");
                        stringBuilder.Append(Servers.ToString());
                        stringBuilder.Append("</IncludeList>");
                    }
                    else
                    {
                        stringBuilder.Append("<IncludeList>");
                        stringBuilder.Append(Servers.ToString());
                        stringBuilder.Append("</IncludeList>");
                    }
                }
            }

            stringBuilder.Append("</MembershipRule>");
            stringBuilder.Append("</MembershipRules>");


            return stringBuilder.ToString();
        }

        public string GetServerHealth(String ServerName)
        {
            string Health = "";

            MonitoringClassCriteria ServerClassCriteria = new MonitoringClassCriteria("Name = 'System.Computer'");

            try
            {
                ReadOnlyCollection<MonitoringClass> ServerMonitoringClasses = _SCOMServerConnection.GetMonitoringClasses(ServerClassCriteria);

                MonitoringObjectCriteria ServiceObjectCriteria = new MonitoringObjectCriteria("DisplayName like '" + ServerName + "%'", ServerMonitoringClasses[0]);
                ReadOnlyCollection<MonitoringObject> ServiceObjects = _SCOMServerConnection.GetMonitoringObjects(ServiceObjectCriteria);

                if (ServiceObjects.Count > 0)
                {
                    Health = ServiceObjects[0].HealthState.ToString();

                    if (!ServiceObjects[0].IsAvailable)
                    {
                        Health = "Error";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Health;

        }

        public bool ManagementPackExists(string Name, ref Version CurrentVersion)
        {
            IList<ManagementPack> ExistingPacks = null;
            ManagementPackCriteria Criteria = GetCriteria(Name);

            try
            {

                ExistingPacks = _SCOMServerConnection.ManagementPacks.GetManagementPacks(Criteria);

                if (ExistingPacks.Count > 0)
                {
                    CurrentVersion = ExistingPacks[0].Version;

                    return true;
                }
            }
            catch (Exception e)
            {
            }

            return false;
        }

        #endregion
    }
}
