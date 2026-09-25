using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

/* ErrorCodes.xml structure
 <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<ErrorCodes xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	<ErrorCode>
		<Type>ERR</Type>
		<Code>1001</Code>
		<Message>[ERR1001] Error adding connection between [Business Application] (Business Application) and new [Server] (Server)</Message>
		<Cause>Could not add connection between Business Application and new Server.</Cause>
		<Resolution>Contact Support</Resolution>
	</ErrorCode>
 </ErrorCodes>
*/

namespace DRPlanningTool
{
    public partial class frmMessageCodes : Form
    {
        private List<ErrorCode> _Codes = new List<ErrorCode>();

        public frmMessageCodes()
        {
            InitializeComponent();
        }

        private void frmErrorCodes_Load(object sender, EventArgs e)
        {
            string Filename = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "ErrorCodes.xml");
            
            if (System.IO.File.Exists(Filename))
            {
                var document = XDocument.Load(Filename);

                var Codes = from Code in document.Element("ErrorCodes").Descendants("ErrorCode")
                            select new ErrorCode
                            {
                                Type = (string)Code.Element("Type"),
                                Code = (string)Code.Element("Code"),
                                Message = (string)Code.Element("Message"),
                                Cause = (string)Code.Element("Cause"),
                                Resolution = (string)Code.Element("Resolution")
                            };

                _Codes = Codes.ToList();
            }
            else
            {
                MessageBox.Show("ErrorCodes.xml file does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGetCode.Enabled = false;
            }
        }

        private void btnGetCode_Click(object sender, EventArgs e)
        {
            DoErrorCodeSearch();
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                DoErrorCodeSearch();
            }
        }

        private void DoErrorCodeSearch()
        {
            if (txtCode.Text != "")
            {
                var Code = (from c in _Codes
                            where c.Code == txtCode.Text
                            select c).FirstOrDefault();

                if (Code != null)
                {
                    ErrorCode FoundCode = (ErrorCode)Code;

                    lblCause.Text = ((ErrorCode)Code).Cause;
                    lblErrorMessage.Text = ((ErrorCode)Code).Message;
                    lblSolution.Text = ((ErrorCode)Code).Resolution;

                    switch (Code.Type)
                    {
                        case "INF":
                            picType.Image = imlMain.Images[1];
                            break;
                        case "ERR":
                            picType.Image = imlMain.Images[2];
                            break;
                    }
                }
                else
                {
                    lblCause.Text = "";
                    lblErrorMessage.Text = "";
                    lblSolution.Text = "";

                    picType.Image = imlMain.Images[0];
                }
            }

            txtCode.Focus();
            txtCode.SelectAll();
        }
    }
}
