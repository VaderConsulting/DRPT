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
    public partial class frmDisks : Form
    {
        Server _Server = null;

        public frmDisks(Server Server)
        {
            InitializeComponent();

            _Server = Server;
        }

        private void frmDisks_Load(object sender, EventArgs e)
        {
            this.Text = "Disks: " + _Server.Name;

            lvwDisks.BeginUpdate();

            foreach (Disk Disk in _Server.Disks)
            {
                ListViewItem Item = new ListViewItem();

                Item.Text = Disk.Name;
                Item.Name = Disk.Name;

                Item.SubItems.Add(Disk.StorageName);
                Item.SubItems.Add((Disk.TotalAllocatedMB / 1024).ToString("#,#.000"));
                Item.SubItems.Add(ISA.Helper.Methods.FormattedTime(Disk.RestoreTime));

                lvwDisks.Items.Add(Item);
            }

            lvwDisks.Sorting = SortOrder.Ascending;
            lvwDisks.Sort();

            lvwDisks.EndUpdate();

            this.Activate();
        }
    }
}
