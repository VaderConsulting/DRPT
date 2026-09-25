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
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void linkLabelEmailAddress_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:" + linkLabelEmailAddress.Text);
            }
            catch (Exception)
            {

            }
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "Version " + ProductVersion;
        }
    }
}
