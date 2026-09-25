INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','SystemCenter_ManagementPackSaveFolderName','String','#MYDOCS#')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','PowershellModulePath','String','')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ImportSCOMManagementPacks','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ImportSCSMManagementPacks','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','HelpHintTimemS','Integer','8000')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowDrawingStatusOnBusinessApplication','Boolean','True')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','SystemCenter_CreateManagementPacksRegardlessOfDocumentStatus','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ConnectToProductionSQLUponStartup','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowDrawingLessBusinessApplications','Boolean','True')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ExportFolderName','String','')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ExportFilename','String','#LISTNAME#-#YEAR##MONTH##DAY#-#HOUR##MINUTE#.txt')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ServerBackgroundColour','String','LightGreen')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowPossibleServerLocations','Boolean','True')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','BusinessApplicationBackgroundColour','String','LightGreen')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowPossibleServiceLocations','Boolean','True')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ConnectToDevelopmentSQLUponStartup','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','IgnoreManagementPackCreationErrors','Boolean','True')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','EnablevCenterQueries','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','EnableSystemCenterFunctions','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowCheckboxesOnServiceList','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowDrawingStatusColumn','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ViewStreamDependenciesAsComponents','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowInScopeServicesOnly','Boolean','True')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowDebugInformation','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowExperimentalFeatures','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowServersOnFullDependencyMap','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','ShowStatesOnDependencyMaps','Boolean','False')
INSERT INTO UserConfig (Username,ValueName,ValueType,ValueData) VALUES ('Default','FlagDrawingErrors','Boolean','True')

INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('FormOpenXOffset','Integer','20')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('FormOpenYOffset','Integer','20')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('iServer_DatabaseConnectionString','String','Server=iServerDB;Database=iServerDB;Trusted_Connection=true;')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_SCOMServer','String','CORPSCOMPRD001')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_SCSMServer','String','CORPSCSMPRD001')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_TestSCOMServer','String','CORPSCOMTST001')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_TestSCSMServer','String','CORPSCSMTST001')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_DevSCOMServer','String','')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_DevSCSMServer','String','CORPSCSMDEV001')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_ManagementPackIDPrefix','String','FIN.DistributedApplication.')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_ManagementPackFriendlyName','String','Finance Distributed Application - #SERVICENAME#')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_ManagementPackDescription','String','This management pack defines the #SERVICENAME# Distributed Application.')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_DistributedApplicationFriendlyNamePrefix','String','')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_ManagementPackSealCommandline','String','Protect-SCSMManagementPack -ManagementPackFile "#MPFILENAME#" -KeyFilePath "#KEYFILEFILENAME#" -CompanyName "#KEYCOMPANYNAME#" -OutputDirectory "#MPPATH#"')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_ManagementPackViewFolderName','String','Finance Distributed Applications')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('KeyfileFilename','String','')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('CompanyName','String','Your Organisation')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Security_SystemCenterMPAuthorGroup','String','USR - DR System Centre Admin')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('vCenter_PrimaryDatabaseConnection','String','Server=CORPSQLPRD001;Database=VIM_VCDB;Trusted_Connection=true;')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('vCenter_SecondaryDatabaseConnection','String','Server=CORPSQLPRD011;Database=VMWARE_VCDB;Trusted_Connection=true;')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_IncludeOptionalServicesWhenCalculatingRestoreTimes','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamAAvailabilityPreditionMethod','Integer','0')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamBAvailabilityPreditionMethod','Integer','0')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamCAvailabilityPreditionMethod','Integer','1')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamDAvailabilityPreditionMethod','Integer','0')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamEAvailabilityPreditionMethod','Integer','0')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamARestoreTime1','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamBRestoreTime1','Time','00:08:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamCRestoreTime1','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamDRestoreTime1','Time','00:10:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamERestoreTime1','Time','00:10:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamARestoreRate1','Integer','10000')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamBRestoreRate1','Integer','10000')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamCRestoreRate1','Integer','10000')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamDRestoreRate1','Integer','10000')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamERestoreRate1','Integer','10000')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamARestoreTime2','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamBRestoreTime2','Time','00:08:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamCRestoreTime2','Time','00:10:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamDRestoreTime2','Time','00:15:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamERestoreTime2','Time','00:15:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamARestoreRate2','Integer','10000')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamBRestoreRate2','Integer','10000')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamCRestoreRate2','Integer','10000')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamDRestoreRate2','Integer','10000')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamERestoreRate2','Integer','10000')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamAIntervalPeriod','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamBIntervalPeriod','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamCIntervalPeriod','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamDIntervalPeriod','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamEIntervalPeriod','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_OverallOverheadTime','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamAOverheadTime','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamBOverheadTime','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamCOverheadTime','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamDOverheadTime','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamEOverheadTime','Time','00:00:00')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamAServicePrerequisite','String','[None]')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamBServicePrerequisite','String','[None]')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamCServicePrerequisite','String','[None]')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamDServicePrerequisite','String','[None]')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Restore_StreamEServicePrerequisite','String','[None]')

INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_MPEmptyTemplate','String','<ManagementPack ContentReadable="true" SchemaVersion="2.0" OriginalSchemaVersion="1.1" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <Manifest>
        <Identity>
            <ID>#MANAGEMENTPACK_ID#</ID>
            <Version>#MANAGEMENTPACK_VERSION#</Version>
        </Identity>
        <Name>#MANAGEMENTPACK_NAME#</Name>
        <References>
            <Reference Alias="SystemCenter">
                <ID>Microsoft.SystemCenter.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="MicrosoftSystemCenterServiceDesignerLibrary7084320">
                <ID>Microsoft.SystemCenter.ServiceDesigner.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="SystemLibrary7585010">
                <ID>System.Library</ID>
                <Version>7.5.8501.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="SystemHealthLibrary7084320">
                <ID>System.Health.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="Image">
                <ID>System.Image.Library</ID>
                <Version>7.5.8501.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="FinanceCustomisation">
                <ID>FIN.DistributedApplications.Core.Customisations</ID>
                <Version>1.0.0.0</Version>
                <PublicKeyToken>5e81ba032cd019e2</PublicKeyToken>
            </Reference>
        </References>
    </Manifest>
    <TypeDefinitions>
        <EntityTypes>
            <ClassTypes>
                <ClassType ID="#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.GenericService" Hosted="false" Singleton="true" Extension="false" />
                <ClassType ID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" Accessibility="Public" Abstract="false" Base="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup" Hosted="false" Singleton="true" Extension="false" />
            </ClassTypes>
            <RelationshipTypes>
                <RelationshipType ID="CGDA_#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                    <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#" />
                    <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" />
                </RelationshipType>
            </RelationshipTypes>
        </EntityTypes>
    </TypeDefinitions>
    <Monitoring>
        <Discoveries>
            <Discovery ID="#MANAGEMENTPACK_SHORTNAME#_SCPopulation" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" ConfirmDelivery="false" Remotable="true" Priority="Normal">
                <Category>Discovery</Category>
                <DiscoveryTypes />
                <DataSource ID="DS" TypeID="SystemCenter!Microsoft.SystemCenter.GroupPopulator">
                    <RuleId>$MPElement$</RuleId>
                    <GroupInstanceId>$Target/Id$</GroupInstanceId>
                    <MembershipRules>
                        <MembershipRule>
                            <MonitoringClass>$MPElement[Name="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup"]$</MonitoringClass>
                            <RelationshipClass>$MPElement[Name="CGDA_#MANAGEMENTPACK_SHORTNAME#"]$</RelationshipClass>
                        </MembershipRule>
                    </MembershipRules>
                </DataSource>
            </Discovery>
        </Discoveries>
    </Monitoring>
    <Presentation>
        <Views>
            <View ID="#MANAGEMENTPACK_ID#.ServiceStateView" Accessibility="Public" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" TypeID="SystemCenter!Microsoft.SystemCenter.StateViewType" Visible="true">
                <Category>Operations</Category>
                <Criteria>
                    <InMaintenanceMode>false</InMaintenanceMode>
                </Criteria>
            </View>
            <View ID="#MANAGEMENTPACK_ID#.DiagramStateView" Accessibility="Public" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" TypeID="SystemCenter!Microsoft.SystemCenter.DiagramViewType" Visible="true">
                <Category>Operations</Category>
            </View>
        </Views>
        <Folders>
            <Folder ID="FIN.DA.VIEWFOLDER.ViewFolder" Accessibility="Public" ParentFolder="FinanceCustomisation!FIN.DistributedApplications.Core.ViewFolder" />
        </Folders>
        <FolderItems>
            <FolderItem ElementID="#MANAGEMENTPACK_ID#.ServiceStateView" ID="#MANAGEMENTPACK_ID#.ServiceStateView" Folder="FIN.DA.VIEWFOLDER.ViewFolder" />
            <FolderItem ElementID="#MANAGEMENTPACK_ID#.DiagramStateView" ID="#MANAGEMENTPACK_ID#.DiagramStateView" Folder="FIN.DA.VIEWFOLDER.ViewFolder" />
        </FolderItems>
    </Presentation>
    <LanguagePacks>
        <LanguagePack ID="ENU" IsDefault="true">
            <DisplayStrings>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#">
                    <Name>#MANAGEMENTPACK_NAME#</Name>
                    <Description>This management pack defines the #SERVICE_NAME# Distributed Application.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#">
                    <Name>#SERVICE_NAME#</Name>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup">
                    <Name>#SERVICE_NAME# Component Group</Name>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_SCPopulation">
                    <Name>Distributed Application Membership Discovery</Name>
                    <Description>This discovery will find which Components are members of this Distributed Application.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#.ServiceStateView">
                    <Name>#SERVICE_NAME# Service State</Name>
                    <Description>#SERVICE_NAME# Service State View</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#.DiagramStateView">
                    <Name>#SERVICE_NAME# Service Diagram</Name>
                    <Description>#SERVICE_NAME# Service Diagram View</Description>
                </DisplayString>
                <DisplayString ElementID="FIN.DA.VIEWFOLDER.ViewFolder">
                    <Name>#SERVICE_NAME#</Name>
                    <Description>#SERVICE_NAME#</Description>
                </DisplayString>
            </DisplayStrings>
        </LanguagePack>
    </LanguagePacks>
</ManagementPack>')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_MPServersAndServicesTemplate','String','<ManagementPack ContentReadable="true" SchemaVersion="2.0" OriginalSchemaVersion="1.1" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <Manifest>
        <Identity>
            <ID>#MANAGEMENTPACK_ID#</ID>
            <Version>#MANAGEMENTPACK_VERSION#</Version>
        </Identity>
        <Name>#MANAGEMENTPACK_NAME#</Name>
        <References>
            <Reference Alias="SystemCenter">
                <ID>Microsoft.SystemCenter.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="MicrosoftSystemCenterServiceDesignerLibrary7084320">
                <ID>Microsoft.SystemCenter.ServiceDesigner.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="SystemLibrary7585010">
                <ID>System.Library</ID>
                <Version>7.5.8501.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="SystemHealthLibrary7084320">
                <ID>System.Health.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="Image">
                <ID>System.Image.Library</ID>
                <Version>7.5.8501.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="FinanceCustomisation">
                <ID>FIN.DistributedApplications.Core.Customisations</ID>
                <Version>1.0.0.0</Version>
                <PublicKeyToken>5e81ba032cd019e2</PublicKeyToken>
            </Reference>
        </References>
    </Manifest>
    <TypeDefinitions>
        <EntityTypes>
            <ClassTypes>
                <ClassType ID="#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.GenericService" Hosted="false" Singleton="true" Extension="false" />
                <ClassType ID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" Accessibility="Public" Abstract="false" Base="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup" Hosted="false" Singleton="true" Extension="false" />
            </ClassTypes>
            <RelationshipTypes>
                <RelationshipType ID="CGSERVICE_#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                    <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" />
                    <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.Service" />
                </RelationshipType>
                <RelationshipType ID="CGSERVER_#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                    <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" />
                    <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="SystemLibrary7585010!System.Computer" />
                </RelationshipType>
                <RelationshipType ID="CGDA_#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                    <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#" />
                    <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" />
                </RelationshipType>
            </RelationshipTypes>
        </EntityTypes>
    </TypeDefinitions>
    <Monitoring>
        <Discoveries>
            <Discovery ID="#MANAGEMENTPACK_SHORTNAME#_SCPopulation" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" ConfirmDelivery="false" Remotable="true" Priority="Normal">
                <Category>Discovery</Category>
                <DiscoveryTypes />
                <DataSource ID="DS" TypeID="SystemCenter!Microsoft.SystemCenter.GroupPopulator">
                    <RuleId>$MPElement$</RuleId>
                    <GroupInstanceId>$Target/Id$</GroupInstanceId>
                    <MembershipRules>
                        <MembershipRule>
                            <MonitoringClass>$MPElement[Name="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup"]$</MonitoringClass>
                            <RelationshipClass>$MPElement[Name="CGDA_#MANAGEMENTPACK_SHORTNAME#"]$</RelationshipClass>
                        </MembershipRule>
                    </MembershipRules>
                </DataSource>
            </Discovery>
            <Discovery ID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup_ItemPopulation" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" ConfirmDelivery="false" Remotable="true" Priority="Normal">
                <Category>Discovery</Category>
                <DiscoveryTypes />
                <DataSource ID="DS" TypeID="SystemCenter!Microsoft.SystemCenter.GroupPopulator">
                    <RuleId>$MPElement$</RuleId>
                    <GroupInstanceId>$Target/Id$</GroupInstanceId>
                    <MembershipRules>
                        <MembershipRule>
                            <MonitoringClass>$MPElement[Name="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.Service"]$</MonitoringClass>
                            <RelationshipClass>$MPElement[Name="CGSERVICE_#MANAGEMENTPACK_SHORTNAME#"]$</RelationshipClass>
                            #SERVICE_INCLUDELIST#
                        </MembershipRule>
                        <MembershipRule>
                            <MonitoringClass>$MPElement[Name="SystemLibrary7585010!System.Computer"]$</MonitoringClass>
                            <RelationshipClass>$MPElement[Name="CGSERVER_#MANAGEMENTPACK_SHORTNAME#"]$</RelationshipClass>
                            #SERVER_INCLUDELIST#
                        </MembershipRule>
                    </MembershipRules>
                </DataSource>
            </Discovery>
        </Discoveries>
    </Monitoring>
    <Presentation>
        <Views>
            <View ID="#MANAGEMENTPACK_ID#.ServiceStateView" Accessibility="Public" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" TypeID="SystemCenter!Microsoft.SystemCenter.StateViewType" Visible="true">
                <Category>Operations</Category>
                <Criteria>
                    <InMaintenanceMode>false</InMaintenanceMode>
                </Criteria>
            </View>
            <View ID="#MANAGEMENTPACK_ID#.DiagramStateView" Accessibility="Public" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" TypeID="SystemCenter!Microsoft.SystemCenter.DiagramViewType" Visible="true">
                <Category>Operations</Category>
            </View>
        </Views>
        <Folders>
            <Folder ID="FIN.DA.VIEWFOLDER.ViewFolder" Accessibility="Public" ParentFolder="FinanceCustomisation!FIN.DistributedApplications.Core.ViewFolder" />
        </Folders>
        <FolderItems>
            <FolderItem ElementID="#MANAGEMENTPACK_ID#.ServiceStateView" ID="#MANAGEMENTPACK_ID#.ServiceStateView" Folder="FIN.DA.VIEWFOLDER.ViewFolder" />
            <FolderItem ElementID="#MANAGEMENTPACK_ID#.DiagramStateView" ID="#MANAGEMENTPACK_ID#.DiagramStateView" Folder="FIN.DA.VIEWFOLDER.ViewFolder" />
        </FolderItems>
    </Presentation>
    <LanguagePacks>
        <LanguagePack ID="ENU" IsDefault="true">
            <DisplayStrings>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#">
                    <Name>#MANAGEMENTPACK_NAME#</Name>
                    <Description>This management pack defines the #SERVICE_NAME# Distributed Application.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#">
                    <Name>#SERVICE_NAME#</Name>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup">
                    <Name>#SERVICE_NAME# Component Group</Name>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_SCPopulation">
                    <Name>Distributed Application Membership Discovery</Name>
                    <Description>This discovery will find which Components are members of this Distributed Application.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup_ItemPopulation">
                    <Name>Component Membership Discovery</Name>
                    <Description>This discovery will find which Objects are members of this Component.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#.ServiceStateView">
                    <Name>#SERVICE_NAME# Service State</Name>
                    <Description>#SERVICE_NAME# Service State View</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#.DiagramStateView">
                    <Name>#SERVICE_NAME# Service Diagram</Name>
                    <Description>#SERVICE_NAME# Service Diagram View</Description>
                </DisplayString>
                <DisplayString ElementID="FIN.DA.VIEWFOLDER.ViewFolder">
                    <Name>#SERVICE_NAME#</Name>
                    <Description>#SERVICE_NAME#</Description>
                </DisplayString>
            </DisplayStrings>
        </LanguagePack>
    </LanguagePacks>
</ManagementPack>')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_MPServersTemplate','String','<ManagementPack ContentReadable="true" SchemaVersion="2.0" OriginalSchemaVersion="1.1" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <Manifest>
        <Identity>
            <ID>#MANAGEMENTPACK_ID#</ID>
            <Version>#MANAGEMENTPACK_VERSION#</Version>
        </Identity>
        <Name>#MANAGEMENTPACK_NAME#</Name>
        <References>
            <Reference Alias="SystemCenter">
                <ID>Microsoft.SystemCenter.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="MicrosoftSystemCenterServiceDesignerLibrary7084320">
                <ID>Microsoft.SystemCenter.ServiceDesigner.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="SystemLibrary7585010">
                <ID>System.Library</ID>
                <Version>7.5.8501.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="SystemHealthLibrary7084320">
                <ID>System.Health.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="Image">
                <ID>System.Image.Library</ID>
                <Version>7.5.8501.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="FinanceCustomisation">
                <ID>FIN.DistributedApplications.Core.Customisations</ID>
                <Version>1.0.0.0</Version>
                <PublicKeyToken>5e81ba032cd019e2</PublicKeyToken>
            </Reference>
        </References>
    </Manifest>
    <TypeDefinitions>
        <EntityTypes>
            <ClassTypes>
                <ClassType ID="#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.GenericService" Hosted="false" Singleton="true" Extension="false" />
                <ClassType ID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" Accessibility="Public" Abstract="false" Base="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup" Hosted="false" Singleton="true" Extension="false" />
            </ClassTypes>
            <RelationshipTypes>
                <RelationshipType ID="CGSERVER_#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                    <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" />
                    <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="SystemLibrary7585010!System.Computer" />
                </RelationshipType>
                <RelationshipType ID="CGDA_#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                    <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#" />
                    <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" />
                </RelationshipType>
            </RelationshipTypes>
        </EntityTypes>
    </TypeDefinitions>
    <Monitoring>
        <Discoveries>
            <Discovery ID="#MANAGEMENTPACK_SHORTNAME#_SCPopulation" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" ConfirmDelivery="false" Remotable="true" Priority="Normal">
                <Category>Discovery</Category>
                <DiscoveryTypes />
                <DataSource ID="DS" TypeID="SystemCenter!Microsoft.SystemCenter.GroupPopulator">
                    <RuleId>$MPElement$</RuleId>
                    <GroupInstanceId>$Target/Id$</GroupInstanceId>
                    <MembershipRules>
                        <MembershipRule>
                            <MonitoringClass>$MPElement[Name="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup"]$</MonitoringClass>
                            <RelationshipClass>$MPElement[Name="CGDA_#MANAGEMENTPACK_SHORTNAME#"]$</RelationshipClass>
                        </MembershipRule>
                    </MembershipRules>
                </DataSource>
            </Discovery>
            <Discovery ID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup_ItemPopulation" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" ConfirmDelivery="false" Remotable="true" Priority="Normal">
                <Category>Discovery</Category>
                <DiscoveryTypes />
                <DataSource ID="DS" TypeID="SystemCenter!Microsoft.SystemCenter.GroupPopulator">
                    <RuleId>$MPElement$</RuleId>
                    <GroupInstanceId>$Target/Id$</GroupInstanceId>
                    <MembershipRules>
                        <MembershipRule>
                            <MonitoringClass>$MPElement[Name="SystemLibrary7585010!System.Computer"]$</MonitoringClass>
                            <RelationshipClass>$MPElement[Name="CGSERVER_#MANAGEMENTPACK_SHORTNAME#"]$</RelationshipClass>
                            #SERVER_INCLUDELIST#
                        </MembershipRule>
                    </MembershipRules>
                </DataSource>
            </Discovery>
        </Discoveries>
    </Monitoring>
    <Presentation>
        <Views>
            <View ID="#MANAGEMENTPACK_ID#.ServiceStateView" Accessibility="Public" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" TypeID="SystemCenter!Microsoft.SystemCenter.StateViewType" Visible="true">
                <Category>Operations</Category>
                <Criteria>
                    <InMaintenanceMode>false</InMaintenanceMode>
                </Criteria>
            </View>
            <View ID="#MANAGEMENTPACK_ID#.DiagramStateView" Accessibility="Public" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" TypeID="SystemCenter!Microsoft.SystemCenter.DiagramViewType" Visible="true">
                <Category>Operations</Category>
            </View>
        </Views>
        <Folders>
            <Folder ID="FIN.DA.VIEWFOLDER.ViewFolder" Accessibility="Public" ParentFolder="FinanceCustomisation!FIN.DistributedApplications.Core.ViewFolder" />
        </Folders>
        <FolderItems>
            <FolderItem ElementID="#MANAGEMENTPACK_ID#.ServiceStateView" ID="#MANAGEMENTPACK_ID#.ServiceStateView" Folder="FIN.DA.VIEWFOLDER.ViewFolder" />
            <FolderItem ElementID="#MANAGEMENTPACK_ID#.DiagramStateView" ID="#MANAGEMENTPACK_ID#.DiagramStateView" Folder="FIN.DA.VIEWFOLDER.ViewFolder" />
        </FolderItems>
    </Presentation>
    <LanguagePacks>
        <LanguagePack ID="ENU" IsDefault="true">
            <DisplayStrings>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#">
                    <Name>#MANAGEMENTPACK_NAME#</Name>
                    <Description>This management pack defines the #SERVICE_NAME# Distributed Application.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#">
                    <Name>#SERVICE_NAME#</Name>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup">
                    <Name>#SERVICE_NAME# Component Group</Name>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_SCPopulation">
                    <Name>Distributed Application Membership Discovery</Name>
                    <Description>This discovery will find which Components are members of this Distributed Application.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup_ItemPopulation">
                    <Name>Component Membership Discovery</Name>
                    <Description>This discovery will find which Objects are members of this Component.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#.ServiceStateView">
                    <Name>#SERVICE_NAME# Service State</Name>
                    <Description>#SERVICE_NAME# Service State View</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#.DiagramStateView">
                    <Name>#SERVICE_NAME# Service Diagram</Name>
                    <Description>#SERVICE_NAME# Service Diagram View</Description>
                </DisplayString>
                <DisplayString ElementID="FIN.DA.VIEWFOLDER.ViewFolder">
                    <Name>#SERVICE_NAME#</Name>
                    <Description>#SERVICE_NAME#</Description>
                </DisplayString>
            </DisplayStrings>
        </LanguagePack>
    </LanguagePacks>
</ManagementPack>')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SystemCenter_MPServicesTemplate','String','<ManagementPack ContentReadable="true" SchemaVersion="2.0" OriginalSchemaVersion="1.1" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <Manifest>
        <Identity>
            <ID>#MANAGEMENTPACK_ID#</ID>
            <Version>#MANAGEMENTPACK_VERSION#</Version>
        </Identity>
        <Name>#MANAGEMENTPACK_NAME#</Name>
        <References>
            <Reference Alias="SystemCenter">
                <ID>Microsoft.SystemCenter.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="MicrosoftSystemCenterServiceDesignerLibrary7084320">
                <ID>Microsoft.SystemCenter.ServiceDesigner.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="SystemLibrary7585010">
                <ID>System.Library</ID>
                <Version>7.5.8501.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="SystemHealthLibrary7084320">
                <ID>System.Health.Library</ID>
                <Version>7.0.8432.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="Image">
                <ID>System.Image.Library</ID>
                <Version>7.5.8501.0</Version>
                <PublicKeyToken>31bf3856ad364e35</PublicKeyToken>
            </Reference>
            <Reference Alias="FinanceCustomisation">
                <ID>FIN.DistributedApplications.Core.Customisations</ID>
                <Version>1.0.0.0</Version>
                <PublicKeyToken>5e81ba032cd019e2</PublicKeyToken>
            </Reference>
        </References>
    </Manifest>
    <TypeDefinitions>
        <EntityTypes>
            <ClassTypes>
                <ClassType ID="#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.GenericService" Hosted="false" Singleton="true" Extension="false" />
                <ClassType ID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" Accessibility="Public" Abstract="false" Base="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.ServiceComponentGroup" Hosted="false" Singleton="true" Extension="false" />
            </ClassTypes>
            <RelationshipTypes>
                <RelationshipType ID="CGSERVICE_#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                    <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" />
                    <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.Service" />
                </RelationshipType>
                <RelationshipType ID="CGDA_#MANAGEMENTPACK_SHORTNAME#" Accessibility="Public" Abstract="false" Base="SystemLibrary7585010!System.Containment">
                    <Source ID="source" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#" />
                    <Target ID="target" MinCardinality="0" MaxCardinality="2147483647" Type="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" />
                </RelationshipType>
            </RelationshipTypes>
        </EntityTypes>
    </TypeDefinitions>
    <Monitoring>
        <Discoveries>
            <Discovery ID="#MANAGEMENTPACK_SHORTNAME#_SCPopulation" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" ConfirmDelivery="false" Remotable="true" Priority="Normal">
                <Category>Discovery</Category>
                <DiscoveryTypes />
                <DataSource ID="DS" TypeID="SystemCenter!Microsoft.SystemCenter.GroupPopulator">
                    <RuleId>$MPElement$</RuleId>
                    <GroupInstanceId>$Target/Id$</GroupInstanceId>
                    <MembershipRules>
                        <MembershipRule>
                            <MonitoringClass>$MPElement[Name="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup"]$</MonitoringClass>
                            <RelationshipClass>$MPElement[Name="CGDA_#MANAGEMENTPACK_SHORTNAME#"]$</RelationshipClass>
                        </MembershipRule>
                    </MembershipRules>
                </DataSource>
            </Discovery>
            <Discovery ID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup_ItemPopulation" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup" ConfirmDelivery="false" Remotable="true" Priority="Normal">
                <Category>Discovery</Category>
                <DiscoveryTypes />
                <DataSource ID="DS" TypeID="SystemCenter!Microsoft.SystemCenter.GroupPopulator">
                    <RuleId>$MPElement$</RuleId>
                    <GroupInstanceId>$Target/Id$</GroupInstanceId>
                    <MembershipRules>
                        <MembershipRule>
                            <MonitoringClass>$MPElement[Name="MicrosoftSystemCenterServiceDesignerLibrary7084320!Microsoft.SystemCenter.ServiceDesigner.Service"]$</MonitoringClass>
                            <RelationshipClass>$MPElement[Name="CGSERVICE_#MANAGEMENTPACK_SHORTNAME#"]$</RelationshipClass>
                            #SERVICE_INCLUDELIST#
                        </MembershipRule>
                    </MembershipRules>
                </DataSource>
            </Discovery>
        </Discoveries>
    </Monitoring>
    <Presentation>
        <Views>
            <View ID="#MANAGEMENTPACK_ID#.ServiceStateView" Accessibility="Public" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" TypeID="SystemCenter!Microsoft.SystemCenter.StateViewType" Visible="true">
                <Category>Operations</Category>
                <Criteria>
                    <InMaintenanceMode>false</InMaintenanceMode>
                </Criteria>
            </View>
            <View ID="#MANAGEMENTPACK_ID#.DiagramStateView" Accessibility="Public" Enabled="true" Target="#MANAGEMENTPACK_SHORTNAME#" TypeID="SystemCenter!Microsoft.SystemCenter.DiagramViewType" Visible="true">
                <Category>Operations</Category>
            </View>
        </Views>
        <Folders>
            <Folder ID="FIN.DA.VIEWFOLDER.ViewFolder" Accessibility="Public" ParentFolder="FinanceCustomisation!FIN.DistributedApplications.Core.ViewFolder" />
        </Folders>
        <FolderItems>
            <FolderItem ElementID="#MANAGEMENTPACK_ID#.ServiceStateView" ID="#MANAGEMENTPACK_ID#.ServiceStateView" Folder="FIN.DA.VIEWFOLDER.ViewFolder" />
            <FolderItem ElementID="#MANAGEMENTPACK_ID#.DiagramStateView" ID="#MANAGEMENTPACK_ID#.DiagramStateView" Folder="FIN.DA.VIEWFOLDER.ViewFolder" />
        </FolderItems>
    </Presentation>
    <LanguagePacks>
        <LanguagePack ID="ENU" IsDefault="true">
            <DisplayStrings>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#">
                    <Name>#MANAGEMENTPACK_NAME#</Name>
                    <Description>This management pack defines the #SERVICE_NAME# Distributed Application.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#">
                    <Name>#SERVICE_NAME#</Name>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup">
                    <Name>#SERVICE_NAME# Component Group</Name>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_SCPopulation">
                    <Name>Distributed Application Membership Discovery</Name>
                    <Description>This discovery will find which Components are members of this Distributed Application.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_SHORTNAME#_ComponentGroup_ItemPopulation">
                    <Name>Component Membership Discovery</Name>
                    <Description>This discovery will find which Objects are members of this Component.</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#.ServiceStateView">
                    <Name>#SERVICE_NAME# Service State</Name>
                    <Description>#SERVICE_NAME# Service State View</Description>
                </DisplayString>
                <DisplayString ElementID="#MANAGEMENTPACK_ID#.DiagramStateView">
                    <Name>#SERVICE_NAME# Service Diagram</Name>
                    <Description>#SERVICE_NAME# Service Diagram View</Description>
                </DisplayString>
                <DisplayString ElementID="FIN.DA.VIEWFOLDER.ViewFolder">
                    <Name>#SERVICE_NAME#</Name>
                    <Description>#SERVICE_NAME#</Description>
                </DisplayString>
            </DisplayStrings>
        </LanguagePack>
    </LanguagePacks>
</ManagementPack>')


INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('PrimaryvCenterSiteName','String','PDC')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SecondaryvCenterSiteName','String','SDC')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('P2P_LocalDBConnectionString','String','Data Source=(localdb)\v11.0;Integrated Security=true;AttachDbFileName=#APPPATH#\DRPlanningTool.mdf;')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Security_DependencyDatabaseReadGroup','String','USR - iServerDB Read Access for DR Tool')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Security_DependencyDatabaseWriteGroup','String','USR - iServerDB Write Access for DR Tool')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('RestrictFeaturesByGroupMembership','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Security_vCenterSQLReadGroup','String','')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('SQLServerQueryTimeout','Integer','20')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('DataLayerType','Integer','0')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Tier0RTOMinimum','Integer','0')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Tier1RTOMinimum','Integer','12')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Tier2RTOMinimum','Integer','24')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Tier3RTOMinimum','Integer','48')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Tier4RTOMinimum','Integer','504')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Tier0RTOMaximum','Integer','12')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Tier1RTOMaximum','Integer','24')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Tier2RTOMaximum','Integer','48')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Tier3RTOMaximum','Integer','504')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Tier4RTOMaximum','Integer','9999')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamATier0','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamBTier0','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamCTier0','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamDTier0','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamETier0','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamATier1','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamBTier1','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamCTier1','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamDTier1','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamETier1','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamATier2','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamBTier2','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamCTier2','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamDTier2','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamETier2','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamATier3','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamBTier3','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamCTier3','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamDTier3','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamETier3','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamATier4','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamBTier4','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamCTier4','Boolean','True')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamDTier4','Boolean','False')
INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('Rule_StreamETier4','Boolean','True')

INSERT INTO SystemConfig (ValueName,ValueType,ValueData) VALUES ('vCenter_StorageQuery','String',
'SELECT VM.NAME 
     , REPLACE(LEFT(ISNULL(UPPER(VM.DNS_NAME),''''), CHARINDEX(''.'', VM.DNS_NAME)),''.'','''') AS DNS_NAME
     , VVD.DEVICE_INFO_LABEL AS DISK_LABEL
     , CAST(VD.HARDWARE_DEVICE_CAPACITY_IN AS BIGINT) / (1024) AS DiskSizeMB
     , DS.NAME AS DATASTORE
FROM VPX_VDEVICE_FILE_BACKING_X     AS FBX WITH (NOLOCK, NOWAIT) 
INNER JOIN VPX_VDEVICE_FILE_BACKING AS FB  WITH (NOLOCK, NOWAIT)  ON FB.BACKING_ID  = FBX.BACKING_ID 
INNER JOIN VPXV_VMS                 AS VM  WITH (NOLOCK, NOWAIT)  ON VM.VMID        = FBX.VM_ID 
INNER JOIN VPXV_DATASTORE           AS DS  WITH (NOLOCK, NOWAIT)  ON DS.ID          = FB.DATASTORE_ID 
INNER JOIN VPX_VIRTUAL_DISK         AS VD  WITH (NOLOCK, NOWAIT)  ON FB.VM_ID       = VD.VM_ID AND FB.UPDATE_KEY = VD.UPDATE_KEY 
INNER JOIN VPX_VIRTUAL_DEVICE       AS VVD WITH (NOLOCK, NOWAIT)  ON VVD.VDEVICE_ID = VD.VDEVICE_ID
WHERE FBX.DIGEST_ENABLED IS NOT NULL
ORDER BY VM.NAME, Disk_Label')