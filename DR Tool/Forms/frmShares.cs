using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace DRPlanningTool
{
    public partial class frmShares : Form
    {
        private string _ServerName = "";

        public frmShares()
        {
            InitializeComponent();
        }

        public frmShares(string ServerName)
        {
            InitializeComponent();

            this.Refresh();

            _ServerName = ServerName;

            txtServerName.Text = ServerName;
        }

        private List<string> GetShareNames()
        {
            ManagementScope scope = null;
            List<string> Names = new List<string>();

            lblStatus.Text = "Connecting...";

            Cursor.Current = Cursors.WaitCursor;

            string Path = "";
            ObjectQuery Query = new ObjectQuery("select * from win32_share");


            lstShares.Items.Clear();

            try
            {
                if (radioButtonAs.Checked)
                {
                    string Domain = Environment.ExpandEnvironmentVariables("USERDOMAIN"); // "CORP";

                    if (txtUsername.Text.Contains(@"\"))
                    {
                        Domain = txtUsername.Text.Substring(0, txtUsername.Text.IndexOf(@"\")).Trim();
                    }

                    ConnectionOptions Credentials = new ConnectionOptions();
                    Credentials.Authority = "ntlmdomain:" + Domain;
                    Credentials.Username = txtUsername.Text.Substring(txtUsername.Text.IndexOf(@"\") + 1).Trim();
                    Credentials.Password = txtPassword.Text;
                    Credentials.Impersonation = ImpersonationLevel.Impersonate;
                    //Credentials.Authentication = AuthenticationLevel.PacketPrivacy;

                    //Path = string.Format(@"\\{0}\root\cimv2", txtServerName.Text);

                    //define the WMI root name space
                    scope = new ManagementScope(@"\\" + txtServerName.Text + @"\root\cimv2", Credentials);

                    scope.Connect();
                }
                else
                {
                    Path = string.Format(@"\\{0}\root\cimv2", txtServerName.Text);

                    scope = new ManagementScope(Path);
                    scope.Connect();
                }

                ManagementObjectSearcher Searcher = new ManagementObjectSearcher(scope, Query);

                var Shares = Searcher.Get();

                foreach (ManagementObject Share in Shares)
                {
                    lstShares.Items.Add(Share["Name"] + " = " + Share["Path"] + "\n");
                }

                lblStatus.Text = "Complete";
            }
            catch (UnauthorizedAccessException)
            {
                lblStatus.Text = "Access denied";
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                lblStatus.Text = "Connection timeout";
            }
            catch (Exception)
            {
                lblStatus.Text = "Error";
            }

            Cursor.Current = Cursors.Default;

            return Names;
        }

        private void frmShares_Load(object sender, EventArgs e)
        {
            if (_ServerName != "")
            {
                //txtServerName.Text = _ServerName;
                GetShareNames();
            }

            this.Activate();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            GetShareNames();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            StringBuilder Builder = new StringBuilder();

            foreach (string Item in lstShares.Items)
            {
                Builder.Append(Item);
            }

            Clipboard.SetText(Builder.ToString());
        }

        private void radioButtonAs_CheckedChanged(object sender, EventArgs e)
        {
            txtUsername.Enabled = radioButtonAs.Checked;
            txtPassword.Enabled = radioButtonAs.Checked;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;

                GetShareNames();
            }
        }

    }
}
