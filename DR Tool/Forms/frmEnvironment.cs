using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISA.Dependency;

namespace DRPlanningTool
{
    public partial class frmEnvironment : Form
    {
        #region Fields  

        private Global.Environment _SelectedEnvironment = ISA.Dependency.Global.Environment.Development;

        #endregion

        #region Properties

        public Global.Environment SelectedEnvironment
        {
            get
            {
                return _SelectedEnvironment;
            }

            set
            {
                _SelectedEnvironment = value;
            }
        }

        #endregion

        #region Constructors

        public frmEnvironment()
        {
            InitializeComponent();
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _SelectedEnvironment = Global.Environment.None;
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void tabEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabEnvironment.SelectedIndex)
            {
                case 0: // Development
                    _SelectedEnvironment = Global.Environment.Development;
                    break;
                case 1:  // Test
                    _SelectedEnvironment = Global.Environment.Test;
                    break;
                case 2: // Production
                    _SelectedEnvironment = Global.Environment.Production;
                    break;
            }
        }
    }
}
