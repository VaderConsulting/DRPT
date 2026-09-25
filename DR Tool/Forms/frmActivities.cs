using Microsoft.AGL.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DRPlanningTool
{
    public partial class frmActivities : Form
    {
        public frmActivities()
        {
            InitializeComponent();
        }

        private void Activities_Load(object sender, EventArgs e)
        {
            //Graph Graph = new Graph("Graph");

            //GraphViewer.Name = "Viewer";

            //Graph.AddNode("Review Emergency Change");
            //Graph.AddNode("Approve Emergency Change Request");
            //Graph.AddNode("DR Activities");
            //Graph.AddNode("DR Impact Assessment");
            //Graph.AddNode("iServer Drawing Tasks");
            //Graph.AddNode("Update iServer Drawing");
            //Graph.AddNode("Review and Approve Drawing");
            //Graph.AddNode("Application Updates");
            //Graph.AddNode("Update CMDB");
            //Graph.AddNode("Update System Center");
            //Graph.AddNode("Update SRM");
            //Graph.AddNode("Update DR Documentation");

            //Graph.AddEdge("Review Emergency Change", "Approve Emergency Change Request");
            //Graph.AddEdge("Approve Emergency Change Request", "DR Activities");
            //Graph.AddEdge("DR Activities", "DR Impact Assessment");
            //Graph.AddEdge("DR Impact Assessment", "iServer Drawing Tasks");
            //Graph.AddEdge("iServer Drawing Tasks", "Update iServer Drawing");
            //Graph.AddEdge("Update iServer Drawing", "Review and Approve Drawing");
            //Graph.AddEdge("Review and Approve Drawing", "Application Updates");
            //Graph.AddEdge("Application Updates", "Update CMDB");
            //Graph.AddEdge("Update CMDB", "Update System Center");
            //Graph.AddEdge("Update CMDB", "Update SRM");
            //Graph.AddEdge("Update System Center", "Update DR Documentation");
            //Graph.AddEdge("Update SRM", "Update DR Documentation");

            ////GraphViewer.PerformLayout();
            //GraphViewer.Graph = Graph;
        }

        private void GraphViewer_GraphChanged(object sender, EventArgs e)
        {
            GraphViewer.Invalidate();
        }
    }
}
