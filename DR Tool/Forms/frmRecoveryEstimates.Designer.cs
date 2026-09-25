namespace DRPlanningTool
{
    partial class frmRecoveryEstimates
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecoveryEstimates));
            this.lvwRunbook = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlSmall = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvwRunbook
            // 
            this.lvwRunbook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwRunbook.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvwRunbook.FullRowSelect = true;
            this.lvwRunbook.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwRunbook.HideSelection = false;
            this.lvwRunbook.Location = new System.Drawing.Point(12, 12);
            this.lvwRunbook.MultiSelect = false;
            this.lvwRunbook.Name = "lvwRunbook";
            this.lvwRunbook.Size = new System.Drawing.Size(384, 507);
            this.lvwRunbook.SmallImageList = this.imlSmall;
            this.lvwRunbook.TabIndex = 0;
            this.lvwRunbook.UseCompatibleStateImageBehavior = false;
            this.lvwRunbook.View = System.Windows.Forms.View.Details;
            this.lvwRunbook.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwRunbook_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 185;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Restore Time";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 75;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Time";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 100;
            // 
            // imlSmall
            // 
            this.imlSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSmall.ImageStream")));
            this.imlSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imlSmall.Images.SetKeyName(0, "gear_blue_32.png");
            this.imlSmall.Images.SetKeyName(1, "gear_grey_32.png");
            this.imlSmall.Images.SetKeyName(2, "gear_red_32.png");
            this.imlSmall.Images.SetKeyName(3, "gear_orange_32.png");
            this.imlSmall.Images.SetKeyName(4, "gear_green_32.png");
            this.imlSmall.Images.SetKeyName(5, "list-numbered_32_blue.png");
            this.imlSmall.Images.SetKeyName(6, "clock_32.png");
            this.imlSmall.Images.SetKeyName(7, "gear_3_colour_32.png");
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(321, 525);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDisclaimer.ForeColor = System.Drawing.Color.Red;
            this.lblDisclaimer.Location = new System.Drawing.Point(12, 530);
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.Size = new System.Drawing.Size(223, 18);
            this.lblDisclaimer.TabIndex = 1;
            this.lblDisclaimer.Text = "Note:  All times shown are estimates only";
            // 
            // frmRecoveryEstimates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 560);
            this.Controls.Add(this.lblDisclaimer);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvwRunbook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(424, 300);
            this.Name = "frmRecoveryEstimates";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recovery Estimates";
            this.Load += new System.EventHandler(this.frmRecoveryEstimates_Load);
            this.Resize += new System.EventHandler(this.frmRecoveryEstimates_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwRunbook;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ImageList imlSmall;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label lblDisclaimer;
    }
}