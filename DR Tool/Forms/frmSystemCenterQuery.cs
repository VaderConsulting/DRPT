using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DRPlanningTool
{
    public partial class frmSystemCenterQuery : Form
    {
        private string _SCOMServerName = "";
        private string _SCSMServerName = "";

        public frmSystemCenterQuery(string SCOMServerName, string SCSMServerName)
        {
            InitializeComponent();

            _SCOMServerName = SCOMServerName;
            _SCSMServerName = SCSMServerName;
        }

        private void frmSystemCenterQuery_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void txtComponentName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                DoSearch();

            }
        }

        private void DoSearch()
        {
            Cursor.Current = Cursors.AppStarting;

            ISA.SystemCenter.ServerUtilities SCOMUtilities = new ISA.SystemCenter.ServerUtilities(_SCOMServerName, _SCSMServerName);
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            string ServerResult = "";
            string ServiceResult = "";
            string ObjectResult = "";

            lblServerResult.Text = "Computer...";
            lblServiceResult.Text = "Service...";
            lblObjectResult.Text = "Object...";

            Task task1 = Task.Factory.StartNew(() =>
            {
                string ServerID = SCOMUtilities.GetServerIDFromName(txtComponentName.Text);

                if (ServerID != "")
                {
                    ServerResult = "Found Computer with this ID: (" + ServerID + ")";
                }
                else
                {
                    ServerResult = "No Computer with this name";
                }

            })
            .ContinueWith(_ => lblServerResult.Text = ServerResult, context);

            Task task2 = Task.Factory.StartNew(() =>
            {
                string ServiceID = SCOMUtilities.GetServiceIDFromName(txtComponentName.Text);

                if (ServiceID != "")
                {
                    ServiceResult = "Found Service with this ID: (" + ServiceID + ")";
                }
                else
                {
                    ServiceResult = "No Service with this name";
                }

            })
            .ContinueWith(_ => lblServiceResult.Text = ServiceResult, context);

            Task task3 = Task.Factory.StartNew(() =>
            {
            string ObjectID = SCOMUtilities.GetObjectIDFromName(txtComponentName.Text); //, "Microsoft.Windows.Computer", "DisplayName");

                if (ObjectID != "")
                {
                    ObjectResult = "Found object with ID: (" + ObjectID + ")";
                }
                else
                {
                    ObjectResult = "No object with this name";
                }

            })
            .ContinueWith(_ => lblObjectResult.Text = ObjectResult, context);

            Cursor.Current = Cursors.Default;
        }
    }
}
