using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DRPlanningTool
{
    public partial class frmBusinessApplicationImage : Form
    {
        private System.Drawing.Image _Image = null;
        private Form _ParentForm = null;
        private string _DrawingName = "";
        private bool _Loading = false;

        public frmBusinessApplicationImage()
        {
            InitializeComponent();
        }

        public frmBusinessApplicationImage(System.Drawing.Image Image, Form ParentForm, string DrawingName)
        {
            InitializeComponent();

            _Image = Image;

            picImage.Image = Image;
            _ParentForm = ParentForm;
            _DrawingName = DrawingName;
            _Loading = true;

            this.Text = "Business Application Image: " + _DrawingName;
        }

        public System.Drawing.Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
            }
        }

        private void frmBusinessApplicationImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            //frmMain MainForm = (frmMain)_ParentForm;

            Main.RemoveBusinessApplicationImageFormFromList(this);

            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.frmBusinessApplicationImageSize = this.Size;

                if (ISA.Helper.Properties.AllowSaveFormLocations)
                {
                    Properties.Settings.Default.frmBusinessApplicationImageLocation = this.Location;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {            
            PrintImage();
        }

        private void PrintImage()
        {
            PrintDocument Document = new PrintDocument();

            Document.DefaultPageSettings.Margins = new Margins(30, 30, 30, 30);
            Document.OriginAtMargins = false;
            Document.DefaultPageSettings.Landscape = true;

            Document.PrintPage += (sender, args) =>
            {
                Rectangle Size = args.MarginBounds;

                if ((double)_Image.Width / (double)_Image.Height > (double)Size.Width / (double)Size.Height) // image is wider
                {
                    Size.Height = (int)((double)_Image.Height / (double)_Image.Width * (double)Size.Width);
                }
                else
                {
                    Size.Width = (int)((double)_Image.Width / (double)_Image.Height * (double)Size.Height);
                }
                args.Graphics.DrawImage(_Image, Size);
            };


            try
            {
                Document.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "Error:  \n" + e.ToString());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string Filename = System.IO.Path.Combine(Properties.Settings.Default.ExportFolderName,_DrawingName) + ".png";

            try
            {
                _Image.Save(Filename, System.Drawing.Imaging.ImageFormat.Png);

                MessageBox.Show("Image saved as " + Filename, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Could not save Image to " + Filename, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(_Image);
        }

        private void frmBusinessApplicationImage_Shown(object sender, EventArgs e)
        {
            // Set location and size
            if (Properties.Settings.Default.frmBusinessApplicationImageLocation.X != 0 && Properties.Settings.Default.frmBusinessApplicationImageLocation.Y != 0)
            {
                this.Location = Properties.Settings.Default.frmBusinessApplicationImageLocation;
            }

            if (Properties.Settings.Default.frmBusinessApplicationImageSize.Width != 0 && Properties.Settings.Default.frmBusinessApplicationImageSize.Height != 0)
            {
                this.Size = Properties.Settings.Default.frmBusinessApplicationImageSize;
            }

            _Loading = false;
            this.Activate();
        }

        private void frmBusinessApplicationImage_Move(object sender, EventArgs e)
        {
            if (!_Loading)
            {
                Properties.Settings.Default.frmBusinessApplicationImageLocation = this.Location;
            }
        }

        private void frmBusinessApplicationImage_ResizeEnd(object sender, EventArgs e)
        {
            if (!_Loading)
            {
                Properties.Settings.Default.frmBusinessApplicationImageSize = this.Size;
            }
        }

    }
}
