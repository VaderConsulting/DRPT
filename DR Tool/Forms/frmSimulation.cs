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
    public partial class frmSimulation : Form
    {
        private List<BusinessApplication> _Services = null;
        private List<Server> _Servers = null;
        private List<PhysicalSite> _Sites = null;
        private List<Relationship> _Relationships = null;

        public frmSimulation(List<BusinessApplication> Services, List<Server> Servers, List<Relationship> Relationships, List<PhysicalSite> Sites)
        {
            InitializeComponent();

            _Services = Services;
            _Servers = Servers;
            _Relationships = Relationships;
            _Sites = Sites;

            AddServers();
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvwOutage.Items.Clear();

            switch (tabMain.SelectedIndex)
            {
                case 0: // Server
                    AddServers();
                    break;
                case 1: // Business Application
                    AddServices();
                    break;
                case 2: // Site
                    AddSites();
                    break;
            }
        }

        private void AddServers()
        {
            foreach (Server Server in _Servers)
            {
                ListViewItem Item = new ListViewItem();

                Item.Text = Server.Name;
                Item.Tag = Server;
                Item.ImageIndex = 3;

                lvwOutage.Items.Add(Item);
            }
        }

        private void AddServices()
        {
            foreach (BusinessApplication Service in _Services)
            {
                ListViewItem Item = new ListViewItem();

                Item.Text = Service.Name;
                Item.Tag = Service;
                Item.ImageIndex = 0;

                lvwOutage.Items.Add(Item);
            }
        }

        private void AddSites()
        {
            foreach (PhysicalSite Site in _Sites)
            {
                ListViewItem Item = new ListViewItem();

                Item.Text = Site.Name;
                Item.Tag = Site;
                Item.ImageIndex = 6;

                lvwOutage.Items.Add(Item);
            }
        }

        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //lvwDirectlyAffected.Items.Clear();
            
            switch (tabMain.SelectedIndex)
            {
                case 0: // Server
                    foreach (ListViewItem Item in lvwOutage.SelectedItems)
                    {
                        Item.ImageIndex = 5;

                        Server Server = (Server)Item.Tag;

                        Disable(Server);
                    }
                    break;
                case 1: // Business Application
                    foreach (ListViewItem Item in lvwOutage.SelectedItems)
                    {
                        Item.ImageIndex = 2;

                        BusinessApplication Service = (BusinessApplication)Item.Tag;

                        Disable(Service);
                    }
                    break;
                case 2: // Site
                    foreach (ListViewItem Item in lvwOutage.SelectedItems)
                    {
                        Item.ImageIndex = 7;

                        PhysicalSite Site = (PhysicalSite)Item.Tag;

                        Disable(Site);
                    }
                    break;
            }
        }

        private void enableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (tabMain.SelectedIndex)
            {
                case 0: // Server
                    foreach (ListViewItem Item in lvwOutage.SelectedItems)
                    {
                        Item.ImageIndex = 3;

                        Server Server = (Server)Item.Tag;

                        Enable(Server);
                    }
                    break;
                case 1: // Business Application
                    foreach (ListViewItem Item in lvwOutage.SelectedItems)
                    {
                        Item.ImageIndex = 0;

                        BusinessApplication Service = (BusinessApplication)Item.Tag;

                        Enable(Service);
                    }
                    break;
                case 2: // Site
                    foreach (ListViewItem Item in lvwOutage.SelectedItems)
                    {
                        Item.ImageIndex = 6;

                        PhysicalSite Site = (PhysicalSite)Item.Tag;

                        Enable(Site);
                    }
                    break;
            }
        }

        private void Disable(Server Server)
        {
            
        }

        private void Disable(BusinessApplication Service)
        {
            foreach (BusinessApplication Dependent in Service.DependentBusinessApplications)
            {
                Relationship Relationship = ISA.Dependency.Global.GetRelationship(Dependent, Service, _Relationships);

                if (Relationship != null)
                {
                    if (!Relationship.StreamDependency)
                    {
                        if (Relationship.SystemMandatory && Relationship.ToType == ISA.Dependency.Relationship.ComponentType.BusinessApplication)
                        {
                            ListViewItem Item = new ListViewItem();

                            Item.Text = Relationship.FromBusinessApplication.Name;
                            Item.Tag = Relationship.FromBusinessApplication;
                            Item.ImageIndex = 0;

                            if (!lvwDirectlyAffected.Items.Contains(Item))
                            {
                                lvwDirectlyAffected.Items.Add(Item);
                            }
                        }
                    }
                }
            }
        }

        private void Disable(PhysicalSite Site)
        {
        }

        private void Enable(Server Server)
        {
        }

        private void Enable(BusinessApplication Service)
        {
            foreach (BusinessApplication Dependent in Service.DependentBusinessApplications)
            {
                Relationship Relationship = ISA.Dependency.Global.GetRelationship(Dependent, Service, _Relationships);

                if (Relationship != null)
                {
                    if (!Relationship.StreamDependency)
                    {
                        if (Relationship.SystemMandatory && Relationship.ToType == ISA.Dependency.Relationship.ComponentType.BusinessApplication)
                        {
                            ListViewItem Item = new ListViewItem();

                            Item.Text = Relationship.FromBusinessApplication.Name;
                            Item.Tag = Relationship.FromBusinessApplication;
                            Item.ImageIndex = 0;

                            if (!lvwDirectlyAffected.Items.Contains(Item))
                            {
                                lvwDirectlyAffected.Items.Remove(Item);
                            }
                        }
                    }
                }
            }
        }

        private void Enable(PhysicalSite Site)
        {
        }
    }
}
