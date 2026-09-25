using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISA.Dependency;

namespace DRPlanningTool
{
    public partial class frmShowNotInScopeServices : Form
    {
        private List<BusinessApplication> _Services = null;
        private Form _ParentForm = null;

        public frmShowNotInScopeServices(List<BusinessApplication> Services, Form ParentForm)
        {
            InitializeComponent();

            _Services = Services;
            _ParentForm = ParentForm;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool CreateWarningMessage = false;

            foreach (ListViewItem Item in lvwServices.Items)
            {
                if (Item.Checked)
                {
                    BusinessApplication Service = (BusinessApplication)Item.Tag;

                    Service.OverrideInScope = true;

                    //foreach (Server Server in Service.ComponentServers)
                    //{
                    //    Server.OverrideInScope = true;
                    //}

                    CreateWarningMessage = true;
                }
            }

            if (CreateWarningMessage)
            {
                Main.AddWarning("[INF1032] One or more Business Applications have their In Scope over-ridden.  Reload to reset");
            }
            
            this.Close();
        }

        private void frmShowNotInScopeServices_Load(object sender, EventArgs e)
        {
            ListViewItem Item = null;
            
            foreach (BusinessApplication Service in _Services)
            {
                if (!Service.InScope || Service.OverrideInScope)
                {
                    Item = new ListViewItem(Service.Name);
                    Item.Tag = Service;
                    Item.ImageIndex = Global.GetBusinessApplicationStateImageIndex(Service, ISA.Helper.Properties.ShowDrawingStatusOnBusinessApplication);

                    lvwServices.Items.Add(Item);
                }
            }
        }
    }
}
