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
    public partial class frmOptionSetting : Form
    {
        private string _OriginalValue = "";
        private string _ResultantValue = "";
        private string _SettingName = "";

        #region Properties

        public string OriginalValue
        {
            get
            {
                return _OriginalValue;
            }
            set
            {
                _OriginalValue = value;
            }
        }

        public string ResultantValue
        {
            get
            {
                return _ResultantValue;
            }
            set
            {
                _ResultantValue = value;
            }
        }

        public string SettingName
        {
            get
            {
                return _SettingName;
            }
            set
            {
                _SettingName = value;
            }
        }

        #endregion

        public frmOptionSetting(string OriginalValue, string SettingName)
        {
            InitializeComponent();

            _OriginalValue = OriginalValue;
            _SettingName = SettingName;

            lblSettingName.Text = _SettingName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _ResultantValue = _OriginalValue;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _ResultantValue = txtValue.Text.Trim();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void frmOptionSetting_Load(object sender, EventArgs e)
        {
            lblSettingName.Text = _SettingName;
            txtValue.Text = _OriginalValue;
            txtValue.Select();
            txtValue.SelectionLength = 0;
            txtValue.SelectionStart = 0;
        }
    }
}
