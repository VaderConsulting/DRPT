using Microsoft.AGL;
using Microsoft.AGL.Drawing;
using Microsoft.AGL.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISA.Dependency;

namespace DRPlanningTool
{
    public partial class frmDependencyMap : Form
    {
        #region Event Handlers

        private void btnApply_Click(object sender, EventArgs e)
        {
            _ShowDependents = chkShowDependents.Checked;
            _ShowServers = chkShowServers.Checked;
            _BundleSplines = chkBundledSplines.Checked;

            Main.CreateDependencyMapForm(_Services, _ParentForm, _Relationships, _FocusedServices, _ShowDependents, _ShowServers, _Layout, _BundleSplines);

            this.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Microsoft.AGL.GraphViewerGdi.GraphRenderer renderer = new Microsoft.AGL.GraphViewerGdi.GraphRenderer(GraphViewer.Graph);

            renderer.CalculateLayout();

            int width = 8192;
            Bitmap bitmap = new Bitmap(width, (int)(GraphViewer.Graph.Height * (width / GraphViewer.Graph.Width)), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);

            Clipboard.SetImage(bitmap);

            MessageBox.Show("Image copied to clipboard", "Image copied", MessageBoxButtons.OK);
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            string Filename = System.IO.Path.Combine(Properties.Settings.Default.ExportFolderName, "Dependency Map.png");
            double Width = 8192; // GraphViewer.Graph.Width;

            Microsoft.AGL.GraphViewerGdi.GraphRenderer Renderer = new Microsoft.AGL.GraphViewerGdi.GraphRenderer(GraphViewer.Graph);

            Renderer.CalculateLayout();

            Bitmap bitmap = new Bitmap((int)Width, (int)(GraphViewer.Graph.Height * ((int)Width / GraphViewer.Graph.Width)), PixelFormat.Format32bppPArgb);
            Renderer.Render(bitmap);

            bitmap.Save(Filename, System.Drawing.Imaging.ImageFormat.Png);

            MessageBox.Show("Image saved to " + Filename, "Image saved", MessageBoxButtons.OK);
        }

        private void chkShowProperties_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowProperties.Checked)
            {
                GraphViewer.Width = this.Width - 402;
            }
            else
            {
                GraphViewer.Width = this.Width - 40;
            }
        }

        private void cmbLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbLayout.SelectedItem.ToString())
            {
                case "Incremental":
                    _Layout = LayoutMethod.IncrementalLayout;
                    break;
                case "MDS":
                    _Layout = LayoutMethod.MDS;
                    break;
                case "Ranking":
                    _Layout = LayoutMethod.Ranking;
                    break;
                case "Sugiyama":
                    _Layout = LayoutMethod.SugiyamaScheme;
                    break;
            }
        }

        private void frmDependencyMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.RemoveBusinessApplicationDependencyMapFormFromList(this);

            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.frmDependencyMapSize = this.Size;

                if (ISA.Helper.Properties.AllowSaveFormLocations)
                {
                    Properties.Settings.Default.frmDependencyMapLocation = this.Location;
                }
            }
        }

        private void frmDependencyMap_Load(object sender, EventArgs e)
        {
            chkShowDependents.Checked = _ShowDependents;
            chkShowServers.Checked = _ShowServers;
            chkBundledSplines.Checked = _BundleSplines;

            cmbLayout.Items.Add("Incremental");
            cmbLayout.Items.Add("MDS");
            cmbLayout.Items.Add("Ranking");
            cmbLayout.Items.Add("Sugiyama");

            switch (_Layout)
            {
                case LayoutMethod.IncrementalLayout:
                    cmbLayout.SelectedItem = cmbLayout.Items[0];
                    break;
                case LayoutMethod.MDS:
                    cmbLayout.SelectedItem = cmbLayout.Items[1];
                    break;
                case LayoutMethod.Ranking:
                    cmbLayout.SelectedItem = cmbLayout.Items[2];
                    break;
                case LayoutMethod.SugiyamaScheme:
                    cmbLayout.SelectedItem = cmbLayout.Items[3];
                    break;
            }

            CreateGraph();
        }

        private void frmDependencyMap_Move(object sender, EventArgs e)
        {
            if (!_Loading)
            {
                Properties.Settings.Default.frmDependencyMapLocation = this.Location;
            }
        }

        private void frmDependencyMap_ResizeEnd(object sender, EventArgs e)
        {
            if (!_Loading)
            {
                Properties.Settings.Default.frmDependencyMapSize = this.Size;
            }
        }

        private void frmDependencyMap_Shown(object sender, EventArgs e)
        {
            // Set location and size
            if (Properties.Settings.Default.frmDependencyMapLocation.X != 0 && Properties.Settings.Default.frmDependencyMapLocation.Y != 0)
            {
                this.Location = Properties.Settings.Default.frmDependencyMapLocation;
            }

            if (Properties.Settings.Default.frmDependencyMapSize.Width != 0 && Properties.Settings.Default.frmDependencyMapSize.Height != 0)
            {
                this.Size = Properties.Settings.Default.frmDependencyMapSize;
            }

            _Loading = false;
            this.Activate();
        }

        private void GraphViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (GraphViewer.ObjectUnderMouseCursor != null)
                {
                    Object ServiceServerRelationship = GraphViewer.ObjectUnderMouseCursor.DrawingObject.UserData;

                    if (ServiceServerRelationship is BusinessApplication)
                    {
                        BusinessApplication Service = (BusinessApplication)ServiceServerRelationship;

                        lblSelectedObjectName.Text = Service.Name;

                        SelectService(Service);

                        GraphViewer.Invalidate();
                    }

                    if (ServiceServerRelationship is Server)
                    {
                        Server Server = (Server)ServiceServerRelationship;

                        lblSelectedObjectName.Text = Server.Name;

                        pgdObject.SelectedObject = Server;
                    }

                    if (ServiceServerRelationship is Relationship)
                    {
                        Relationship Relationship = (Relationship)ServiceServerRelationship;

                        lblSelectedObjectName.Text = Relationship.Name;

                        pgdObject.SelectedObject = Relationship;
                    }
                }
            }
        }

        private void GraphViewer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Object ServiceServerRelationship = GraphViewer.ObjectUnderMouseCursor.DrawingObject.UserData;

                if (ServiceServerRelationship is BusinessApplication)
                {
                    BusinessApplication Service = (BusinessApplication)ServiceServerRelationship;

                    Main.CreateOrSelectBusinessApplicationForm(Service.Name, Service);
                }

                if (ServiceServerRelationship is Server)
                {
                    Server Server = (Server)ServiceServerRelationship;

                    Main.CreateOrSelectServerForm(ref Server);
                }

                if (ServiceServerRelationship is Relationship)
                {
                    Relationship Relationship = (Relationship)ServiceServerRelationship;

                    //MainForm.CreateOrSelectBusinessApplicationForm(Relationship);
                }
            }
        }

        #endregion

        #region Fields

        private int _ErrorCounter = 0;
        private Microsoft.AGL.Drawing.Color _FocusedServiceNodeColour = Microsoft.AGL.Drawing.Color.LightBlue;
        private List<BusinessApplication> _FocusedServices = new List<BusinessApplication>();
        private Microsoft.AGL.Drawing.Color _InScopeNodeColour = Microsoft.AGL.Drawing.Color.Blue;
        private LayoutMethod _Layout = LayoutMethod.InheritFromGraph;
        private bool _Loading = false;
        private Microsoft.AGL.Drawing.Color _MandatoryComponentNodeColour = Microsoft.AGL.Drawing.Color.Peru;
        private Microsoft.AGL.Drawing.Color _MandatoryConnectionColour = Microsoft.AGL.Drawing.Color.Green;
        private double _MandatoryConnectionLineWidth = 2;
        private Microsoft.AGL.Drawing.Color _MandatoryDependentNodeColour = Microsoft.AGL.Drawing.Color.DarkKhaki;
        private Microsoft.AGL.Drawing.Color _NotInScopeNodeColour = Microsoft.AGL.Drawing.Color.Black;
        private Microsoft.AGL.Drawing.Color _OptionalComponentNodeColour = Microsoft.AGL.Drawing.Color.Orange;
        private Microsoft.AGL.Drawing.Color _OptionalConnectionColour = Microsoft.AGL.Drawing.Color.Green;
        private double _OptionalConnectionLineWidth = 0.5;
        private Microsoft.AGL.Drawing.Color _OptionalDependentNodeColour = Microsoft.AGL.Drawing.Color.Khaki;
        private Form _ParentForm = null;
        private List<Relationship> _Relationships = null;
        private List<Relationship> _RelationshipsOnMap = new List<Relationship>();
        private List<BusinessApplication> _Services = null;
        private bool _ShowDependents = true;
        private bool _ShowMDSLayout = false;
        private bool _ShowServers = false;
        private bool _BundleSplines = false;

        #endregion

        #region Constructors

        public frmDependencyMap(List<BusinessApplication> Services, Form ParentForm, List<Relationship> Relationships, List<BusinessApplication> FocusedServices, bool ShowDependents, bool ShowServers, LayoutMethod Layout, bool BundleSplines)
        {
            InitializeComponent();

            _Loading = true;

            _Services = Services;
            _ParentForm = ParentForm;
            _Relationships = Relationships;
            _BundleSplines = BundleSplines;

            if (FocusedServices != null)
            {
                _FocusedServices = FocusedServices;
            }

            _ShowDependents = ShowDependents;
            _ShowServers = ShowServers;
            _Layout = Layout;
        }

        #endregion

        #region Properties

        public Microsoft.AGL.Drawing.Color FocusedServiceNodeColour
        {
            get
            {
                return _FocusedServiceNodeColour;
            }
            set
            {
                _FocusedServiceNodeColour = value;
            }
        }

        public List<BusinessApplication> FocusedServices
        {
            get
            {
                return _FocusedServices;
            }
            set
            {
                _FocusedServices = value;
            }
        }

        public bool IncludeDependents
        {
            get
            {
                return _ShowDependents;
            }
            set
            {
                _ShowDependents = value;
            }
        }

        public bool IncludeServers
        {
            get
            {
                return _ShowServers;
            }
            set
            {
                _ShowServers = value;
            }
        }

        public Microsoft.AGL.Drawing.Color InScopeNodeColour
        {
            get
            {
                return _InScopeNodeColour;
            }
            set
            {
                _InScopeNodeColour = value;
            }
        }

        public Microsoft.AGL.Drawing.Color MandatoryConnectionColour
        {
            get
            {
                return _MandatoryConnectionColour;
            }
            set
            {
                _MandatoryConnectionColour = value;
            }
        }

        public double MandatoryConnectionLineWidth
        {
            get
            {
                return _MandatoryConnectionLineWidth;
            }
            set
            {
                _MandatoryConnectionLineWidth = value;
            }
        }

        public Microsoft.AGL.Drawing.Color NotInScopeNodeColour
        {
            get
            {
                return _NotInScopeNodeColour;
            }
            set
            {
                _NotInScopeNodeColour = value;
            }
        }

        public Microsoft.AGL.Drawing.Color OptionalConnectionColour
        {
            get
            {
                return _OptionalConnectionColour;
            }
            set
            {
                _OptionalConnectionColour = value;
            }
        }

        public double OptionalConnectionLineWidth
        {
            get
            {
                return _OptionalConnectionLineWidth;
            }
            set
            {
                _OptionalConnectionLineWidth = value;
            }
        }

        public List<Relationship> Relationships
        {
            get
            {
                return _Relationships;
            }
            set
            {
                _Relationships = value;
            }
        }

        public List<Relationship> RelationshipsOnMap
        {
            get
            {
                return _RelationshipsOnMap;
            }
            set
            {
                _RelationshipsOnMap = value;
            }
        }

        public List<BusinessApplication> Services
        {
            get
            {
                return _Services;
            }
            set
            {
                _Services = value;
            }
        }

        public bool ShowMDSLayout
        {
            get
            {
                return _ShowMDSLayout;
            }
            set
            {
                _ShowMDSLayout = value;
            }
        }

        #endregion

        #region Private Methods

        private void AddConnection(Graph Graph, Relationship Relationship, Microsoft.AGL.Drawing.Color MandatoryComponentColour, Microsoft.AGL.Drawing.Color OptionalComponentColour)
        {
            Edge Connection = null;
            Node SourceNode = null;
            Node DestinationNode = null;

            if (Global.GetExistingRelationship(Relationship, _RelationshipsOnMap) == null)
            {
                if (!Relationship.StreamDependency)
                {
                    if (Relationship.ToType == ISA.Dependency.Relationship.ComponentType.BusinessApplication)
                    {
                        // Do the nodes already exist?
                        SourceNode = Graph.FindNode(Relationship.FromBusinessApplication.Name);
                        DestinationNode = Graph.FindNode(Relationship.ToBusinessApplication.Name);

                        if (SourceNode == null)
                        {
                            SourceNode = Graph.AddNode(Relationship.FromBusinessApplication.Name);
                        }

                        SourceNode.Attribute.Shape = Shape.Ellipse;
                        SourceNode.UserData = Relationship.FromBusinessApplication;

                        if (!_FocusedServices.Contains(Relationship.FromBusinessApplication))
                        {
                            if (Relationship.SystemMandatory)
                            {
                                SourceNode.Attribute.FillColor = MandatoryComponentColour;
                            }
                            else
                            {
                                SourceNode.Attribute.FillColor = OptionalComponentColour;
                            }
                        }
                        else
                        {
                            SourceNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.LightBlue;
                        }

                        //if (Properties.Settings.Default.ShowStatesOnDependencyMaps)
                        //{
                        //    switch (Relationship.FromBusinessApplication.SystemState)
                        //    {
                        //        case Global.SystemHealthState.OK:
                        //            SourceNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Green;
                        //            break;
                        //        case Global.SystemHealthState.Degraded:
                        //            SourceNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Orange;
                        //            break;
                        //        case Global.SystemHealthState.Error:
                        //            SourceNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Red;
                        //            break;
                        //        case Global.SystemHealthState.Unknown:
                        //            SourceNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Yellow;
                        //            break;
                        //    }
                        //}

                        if (DestinationNode == null)
                        {
                            DestinationNode = Graph.AddNode(Relationship.ToBusinessApplication.Name);
                        }

                        DestinationNode.Attribute.Shape = Shape.Ellipse;
                        DestinationNode.UserData = Relationship.ToBusinessApplication;

                        if (Properties.Settings.Default.ShowStatesOnDependencyMaps)
                        {
                            switch (Relationship.ToBusinessApplication.SystemState)
                            {
                                case Global.HealthState.OK:
                                    DestinationNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Green;
                                    break;
                                case Global.HealthState.Degraded:
                                    DestinationNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Orange;
                                    break;
                                case Global.HealthState.Error:
                                    DestinationNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Red;
                                    break;
                                case Global.HealthState.Unknown:
                                    DestinationNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Yellow;
                                    break;
                            }
                        }
                        else
                        {
                            if (!_FocusedServices.Contains(Relationship.ToBusinessApplication))
                            {
                                if (Relationship.SystemMandatory)
                                {
                                    DestinationNode.Attribute.FillColor = MandatoryComponentColour;
                                }
                                else
                                {
                                    DestinationNode.Attribute.FillColor = _OptionalComponentNodeColour;
                                }
                            }
                            else
                            {
                                DestinationNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.LightBlue;
                            }
                        }

                        if (!_RelationshipsOnMap.Contains(Relationship))
                        {
                            // Create a new edge
                            Connection = Graph.AddEdge(Relationship.FromBusinessApplication.Name, Relationship.ToBusinessApplication.Name);
                            Connection.UserData = Relationship;

                            if (_FocusedServices.Contains(Relationship.FromBusinessApplication))
                            {
                                SourceNode.Attribute.FillColor = _FocusedServiceNodeColour;
                            }

                            if (Relationship.FromBusinessApplication.InScope)
                            {
                                SourceNode.Attribute.Color = _InScopeNodeColour;
                            }
                            else
                            {
                                SourceNode.Attribute.Color = _NotInScopeNodeColour;
                            }

                            //if (_FocusedServices.Contains(Relationship.ToBusinessApplication))
                            //{
                            //    DestinationNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.LightBlue;
                            //}

                            if (Relationship.ToBusinessApplication.InScope)
                            {
                                DestinationNode.Attribute.Color = _InScopeNodeColour;
                            }
                            else
                            {
                                DestinationNode.Attribute.Color = _NotInScopeNodeColour;
                            }

                            Connection.Attribute.ArrowheadAtSource = ArrowStyle.None;
                            Connection.Attribute.ArrowheadAtTarget = ArrowStyle.Normal;

                            //Connection.LabelText = FromService.Name + " depends on " + ToService.Name;
                            //Connection.LabelText = "Depends on";

                            if (Relationship.SystemMandatory || Relationship.UserMandatory) // UserMandatory defaults to false
                            {
                                Connection.Attribute.Color = _MandatoryConnectionColour;
                                Connection.Attribute.LineWidth = _MandatoryConnectionLineWidth;

                                //DestinationNode.Attribute.FillColor = MandatoryComponentColour;
                            }
                            else
                            {
                                Connection.Attribute.Color = _OptionalConnectionColour;
                                Connection.Attribute.LineWidth = _OptionalConnectionLineWidth;

                                //DestinationNode.Attribute.FillColor = OptionalComponentColour;
                                Style Style = new Style();

                                Style Dashed = Microsoft.AGL.Drawing.Style.Dashed;

                                Style = Dashed;

                                Connection.Attribute.AddStyle(Dashed);
                            }

                            // Add the connection to our list for tracking purposes
                            _RelationshipsOnMap.Add(Relationship);

                        }

                        foreach (Relationship DownstreamRelationship in Relationship.ToBusinessApplication.Relationships)
                        {
                            if (DownstreamRelationship.ToType == ISA.Dependency.Relationship.ComponentType.BusinessApplication)
                            {
                                if (Global.GetExistingRelationship(DownstreamRelationship, _RelationshipsOnMap) == null)
                                {
                                    
                                    AddConnection(Graph, DownstreamRelationship, _MandatoryComponentNodeColour, _OptionalComponentNodeColour);
                                    _RelationshipsOnMap.Add(DownstreamRelationship);
                                }
                                else
                                {
                                    _ErrorCounter++;
                                }
                            }
                            else
                            {
                                if (_ShowServers)
                                {
                                    if (Global.GetExistingRelationship(DownstreamRelationship, _RelationshipsOnMap) == null)
                                    {
                                        
                                        AddConnection(Graph, DownstreamRelationship, _MandatoryComponentNodeColour, _OptionalComponentNodeColour);
                                        _RelationshipsOnMap.Add(DownstreamRelationship);
                                    }
                                    else
                                    {
                                        _ErrorCounter++;
                                    }
                                }
                            }

                            if (_ErrorCounter > 0)
                            {
                                _ErrorCounter = 0;
                                break;
                            }
                        }
                    }
                    else // ToType = Server
                    {
                        if (_ShowServers)
                        {
                            // Do the nodes already exist?
                            SourceNode = Graph.FindNode(Relationship.FromBusinessApplication.Name);
                            DestinationNode = Graph.FindNode(Relationship.ToServer.Name);

                            if (SourceNode == null)
                            {
                                SourceNode = Graph.AddNode(Relationship.FromBusinessApplication.Name);
                            }

                            SourceNode.Attribute.Shape = Shape.Ellipse;
                            SourceNode.UserData = Relationship.FromBusinessApplication;

                            if (Properties.Settings.Default.ShowStatesOnDependencyMaps)
                            {
                                switch (Relationship.FromBusinessApplication.SystemState)
                                {
                                    case Global.HealthState.OK:
                                        SourceNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Green;
                                        break;
                                    case Global.HealthState.Degraded:
                                        SourceNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Orange;
                                        break;
                                    case Global.HealthState.Error:
                                        SourceNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Red;
                                        break;
                                    case Global.HealthState.Unknown:
                                        SourceNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Yellow;
                                        break;
                                }
                            }

                            if (DestinationNode == null)
                            {
                                DestinationNode = Graph.AddNode(Relationship.ToServer.Name);
                            }

                            DestinationNode.Attribute.Shape = Shape.Box;
                            DestinationNode.UserData = Relationship.ToServer;

                            if (Properties.Settings.Default.ShowStatesOnDependencyMaps)
                            {
                                switch (Relationship.ToServer.SystemState)
                                {
                                    case Global.HealthState.OK:
                                        DestinationNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Green;
                                        break;
                                    case Global.HealthState.Degraded:
                                        DestinationNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Orange;
                                        break;
                                    case Global.HealthState.Error:
                                        DestinationNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Red;
                                        break;
                                    case Global.HealthState.Unknown:
                                        DestinationNode.Attribute.FillColor = Microsoft.AGL.Drawing.Color.Yellow;
                                        break;
                                }
                            }

                            // Create a new edge
                            Connection = Graph.AddEdge(Relationship.FromBusinessApplication.Name, Relationship.ToServer.Name);
                            Connection.UserData = Relationship;

                            if (_FocusedServices.Contains(Relationship.FromBusinessApplication))
                            {
                                SourceNode.Attribute.FillColor = _FocusedServiceNodeColour;
                            }

                            if (Relationship.FromBusinessApplication.InScope)
                            {
                                SourceNode.Attribute.Color = _InScopeNodeColour;
                            }
                            else
                            {
                                SourceNode.Attribute.Color = _NotInScopeNodeColour;
                            }

                            Connection.Attribute.ArrowheadAtSource = ArrowStyle.None;
                            Connection.Attribute.ArrowheadAtTarget = ArrowStyle.Normal;

                            if (Relationship.SystemMandatory || Relationship.UserMandatory)
                            {
                                Connection.Attribute.Color = _MandatoryConnectionColour;
                                Connection.Attribute.LineWidth = _MandatoryConnectionLineWidth;

                                //DestinationNode.Attribute.FillColor = MandatoryComponentColour;
                            }
                            else
                            {
                                Connection.Attribute.Color = _OptionalConnectionColour;
                                Connection.Attribute.LineWidth = _OptionalConnectionLineWidth;

                                //DestinationNode.Attribute.FillColor = OptionalComponentColour;
                                Style Style = new Style();

                                Style Dashed = Microsoft.AGL.Drawing.Style.Dashed;

                                Style = Dashed;

                                Connection.Attribute.AddStyle(Dashed);
                            }
                        }
                    }
                }
            }
        }

        private void CreateGraph()
        {
            Cursor.Current = Cursors.AppStarting;

            _Loading = true;

            Graph Graph = new Graph("Graph");

            GraphViewer.Name = "Viewer";

            foreach (BusinessApplication Service in _Services)
            {
                if (_ShowDependents)
                {
                    foreach (BusinessApplication Dependent in Service.DependentBusinessApplications)
                    {
                        Relationship Relationship = Global.GetRelationship(Service, Dependent, _Relationships);

                        if (Relationship != null)
                        {
                            if (!Relationship.StreamDependency)
                            {
                                if (Global.GetExistingRelationship(Relationship, _RelationshipsOnMap) == null)
                                {
                                    AddConnection(Graph, Relationship, _MandatoryDependentNodeColour, _OptionalDependentNodeColour);
                                    _RelationshipsOnMap.Add(Relationship);
                                }
                                else
                                {
                                    _ErrorCounter++;
                                }
                            }
                        }
                        else
                        {
                            Relationship = Global.GetRelationship(Dependent, Service, _Relationships);

                            if (Relationship != null)
                            {
                                if (!Relationship.StreamDependency)
                                {
                                    if (Global.GetExistingRelationship(Relationship, _RelationshipsOnMap) == null)
                                    {
                                        AddConnection(Graph, Relationship, _MandatoryDependentNodeColour, _OptionalDependentNodeColour);
                                        _RelationshipsOnMap.Add(Relationship);
                                    }
                                    else
                                    {
                                        _ErrorCounter++;
                                    }
                                }
                            }
                            else
                            {
                                Debug.WriteLine("[ERR1023] Could not add relationship between " + Dependent.Name + " (Business Application) and " + Service.Name + " (Server) on the Dependency Map.  This should not occur!", "error");
                            } 
                        }
                    }
                }

                foreach (Relationship Relationship in Service.Relationships)
                {
                    if (Relationship.ToType == ISA.Dependency.Relationship.ComponentType.BusinessApplication)
                    {
                        if (!Relationship.StreamDependency)
                        {
                            if (Global.GetExistingRelationship(Relationship, _RelationshipsOnMap) == null)
                            {
                                AddConnection(Graph, Relationship, _MandatoryComponentNodeColour, _OptionalComponentNodeColour);
                                _RelationshipsOnMap.Add(Relationship);
                            }
                            else
                            {
                                // Relationship already exists on the Map
                                _ErrorCounter++;
                            }
                        }
                    }
                    else
                    {
                        if (_ShowServers)
                        {
                            if (Global.GetExistingRelationship(Relationship, _RelationshipsOnMap) == null)
                            {
                                AddConnection(Graph, Relationship, _MandatoryComponentNodeColour, _OptionalComponentNodeColour);
                                _RelationshipsOnMap.Add(Relationship);
                            }
                            else
                            {
                                // Relationship already exists on the Map
                                _ErrorCounter++;
                            }
                        }
                    }
                }
            }
          
            // default layout method
            GraphViewer.CurrentLayoutMethod = _Layout;

            if (_BundleSplines)
            {
                Graph.LayoutAlgorithmSettings.EdgeRoutingSettings.EdgeRoutingMode = Microsoft.AGL.Core.Routing.EdgeRoutingMode.SplineBundling;
            }

            GraphViewer.PerformLayout();
            GraphViewer.Graph = Graph;
            
            Cursor.Current = Cursors.Default;

            _Loading = false;
        }

        private void DeselectAllNodes(BusinessApplication Service)
        {
            foreach (Node Node in GraphViewer.Graph.Nodes)
            {
                if (Node != null)
                {
                    if (Node.LabelText != Service.Name)
                    {
                        Node.Attribute.FillColor = Microsoft.AGL.Drawing.Color.White; // Microsoft.AGL.Drawing.Color.LightBlue;
                    }
                }
            }
        }

        private void SelectAllComponents(BusinessApplication Service)
        {
            if (_ShowServers)
            {
                foreach (Server Server in Service.ComponentServers)
                {
                    Node Node = GraphViewer.Graph.FindNode(Server.Name);

                    if (Node != null)
                    {
                        Relationship Relationship = Global.GetRelationship(Service, Server, _Relationships);

                        if (Relationship != null)
                        {
                            if (!Relationship.StreamDependency)
                            {
                                if (Relationship.SystemMandatory)
                                {
                                    Node.Attribute.FillColor = _MandatoryComponentNodeColour;
                                }
                                else
                                {
                                    Node.Attribute.FillColor = _OptionalComponentNodeColour;
                                }
                            }
                        }
                    }
                }
            }

            foreach (BusinessApplication ComponentService in Service.ComponentBusinessApplications)
            {
                Node Node = GraphViewer.Graph.FindNode(ComponentService.Name);

                if (Node != null)
                {
                    Relationship Relationship = Global.GetRelationship(Service, ComponentService, _Relationships);

                    if (Relationship != null)
                    {
                        if (!Relationship.StreamDependency)
                        {
                            if (Relationship.SystemMandatory)
                            {
                                Node.Attribute.FillColor = _MandatoryComponentNodeColour;
                            }
                            else
                            {
                                Node.Attribute.FillColor = _OptionalComponentNodeColour;
                            }
                        }
                    }
                }
                else
                {
                    //SelectAllComponents(ComponentService);
                }

            }
        }

        private void SelectAllDependents(BusinessApplication Service)
        {
            if (_ShowDependents)
            {
                foreach (BusinessApplication ComponentService in Service.DependentBusinessApplications)
                {
                    Node Node = GraphViewer.Graph.FindNode(ComponentService.Name);

                    if (Node != null)
                    {
                        Relationship Relationship = Global.GetRelationship(ComponentService, Service, _Relationships);

                        if (Relationship != null)
                        {
                            if (!Relationship.StreamDependency)
                            {
                                if (Relationship.SystemMandatory)
                                {
                                    Node.Attribute.FillColor = _MandatoryDependentNodeColour;
                                }
                                else
                                {
                                    Node.Attribute.FillColor = _OptionalDependentNodeColour;
                                }

                                //SelectAllDependents(ComponentService);
                            }
                        }
                        //SelectAllComponents(ComponentService);

                    }
                    else
                    {
                        //SelectAllDependents(ComponentService);
                    }
                }
            }
        }

        private void SelectService(BusinessApplication Service)
        {
            pgdObject.SelectedObject = Service;

            DeselectAllNodes(Service);

            SelectAllComponents(Service);

            SelectAllDependents(Service);

            SelectThisNode(Service);
        }

        private void SelectThisNode(BusinessApplication Service)
        {
            Node Node = GraphViewer.Graph.FindNode(Service.Name);

            if (Node != null)
            {
                Node.Attribute.FillColor = Microsoft.AGL.Drawing.Color.LightBlue;
            }
        }

        #endregion

        #region Public Methods
        
        #endregion

    }
}
