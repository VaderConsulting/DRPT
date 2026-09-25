namespace DRPlanningTool
{
    partial class frmEnvironment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnvironment));
            this.label1 = new System.Windows.Forms.Label();
            this.tabEnvironment = new System.Windows.Forms.TabControl();
            this.tpTest = new System.Windows.Forms.TabPage();
            this.tpProduction = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tpDevelopment = new System.Windows.Forms.TabPage();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabEnvironment.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please choose an environment";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabEnvironment
            // 
            this.tabEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabEnvironment.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabEnvironment.Controls.Add(this.tpDevelopment);
            this.tabEnvironment.Controls.Add(this.tpTest);
            this.tabEnvironment.Controls.Add(this.tpProduction);
            this.tabEnvironment.ImageList = this.imageList1;
            this.tabEnvironment.Location = new System.Drawing.Point(12, 43);
            this.tabEnvironment.Name = "tabEnvironment";
            this.tabEnvironment.SelectedIndex = 0;
            this.tabEnvironment.Size = new System.Drawing.Size(300, 303);
            this.tabEnvironment.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabEnvironment.TabIndex = 1;
            this.tabEnvironment.SelectedIndexChanged += new System.EventHandler(this.tabEnvironment_SelectedIndexChanged);
            // 
            // tpTest
            // 
            this.tpTest.BackgroundImage = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_programmer_256;
            this.tpTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tpTest.Location = new System.Drawing.Point(4, 42);
            this.tpTest.Name = "tpTest";
            this.tpTest.Padding = new System.Windows.Forms.Padding(3);
            this.tpTest.Size = new System.Drawing.Size(292, 257);
            this.tpTest.TabIndex = 1;
            this.tpTest.Text = "Test";
            this.tpTest.UseVisualStyleBackColor = true;
            // 
            // tpProduction
            // 
            this.tpProduction.BackgroundImage = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_american_256;
            this.tpProduction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tpProduction.Location = new System.Drawing.Point(4, 42);
            this.tpProduction.Name = "tpProduction";
            this.tpProduction.Size = new System.Drawing.Size(292, 257);
            this.tpProduction.TabIndex = 2;
            this.tpProduction.Text = "Production";
            this.tpProduction.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "supervista_jobsicons_machine_operator_32.png");
            this.imageList1.Images.SetKeyName(1, "supervista_jobsicons_programmer_32.png");
            this.imageList1.Images.SetKeyName(2, "supervista_jobsicons_american_32.png");
            // 
            // tpDevelopment
            // 
            this.tpDevelopment.BackgroundImage = global::DRPlanningTool.Properties.Resources.supervista_jobsicons_machine_operator_256;
            this.tpDevelopment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tpDevelopment.Location = new System.Drawing.Point(4, 42);
            this.tpDevelopment.Name = "tpDevelopment";
            this.tpDevelopment.Padding = new System.Windows.Forms.Padding(3);
            this.tpDevelopment.Size = new System.Drawing.Size(292, 257);
            this.tpDevelopment.TabIndex = 0;
            this.tpDevelopment.Text = "Development";
            this.tpDevelopment.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(232, 352);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(151, 352);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmEnvironment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 387);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabEnvironment);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmEnvironment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Environment";
            this.tabEnvironment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabEnvironment;
        private System.Windows.Forms.TabPage tpDevelopment;
        private System.Windows.Forms.TabPage tpTest;
        private System.Windows.Forms.TabPage tpProduction;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}