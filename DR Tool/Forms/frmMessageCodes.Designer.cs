namespace DRPlanningTool
{
    partial class frmMessageCodes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMessageCodes));
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnGetCode = new System.Windows.Forms.Button();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.lblSolution = new System.Windows.Forms.Label();
            this.lblCause = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.picType = new System.Windows.Forms.PictureBox();
            this.imlMain = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picType)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(67, 15);
            this.lblCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(41, 17);
            this.lblCode.TabIndex = 2;
            this.lblCode.Text = "Code";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(117, 11);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCode.MaxLength = 4;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(93, 22);
            this.txtCode.TabIndex = 0;
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(113, 39);
            this.lblHint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(100, 17);
            this.lblHint.TabIndex = 3;
            this.lblHint.Text = "(Numeric only)";
            // 
            // btnGetCode
            // 
            this.btnGetCode.Location = new System.Drawing.Point(220, 9);
            this.btnGetCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGetCode.Name = "btnGetCode";
            this.btnGetCode.Size = new System.Drawing.Size(100, 28);
            this.btnGetCode.TabIndex = 1;
            this.btnGetCode.Text = "Ok";
            this.btnGetCode.UseVisualStyleBackColor = true;
            this.btnGetCode.Click += new System.EventHandler(this.btnGetCode_Click);
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblErrorMessage.Location = new System.Drawing.Point(111, 0);
            this.lblErrorMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(632, 90);
            this.lblErrorMessage.TabIndex = 1;
            // 
            // lblSolution
            // 
            this.lblSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSolution.Location = new System.Drawing.Point(111, 180);
            this.lblSolution.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSolution.Name = "lblSolution";
            this.lblSolution.Size = new System.Drawing.Size(632, 91);
            this.lblSolution.TabIndex = 5;
            // 
            // lblCause
            // 
            this.lblCause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCause.Location = new System.Drawing.Point(111, 90);
            this.lblCause.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCause.Name = "lblCause";
            this.lblCause.Size = new System.Drawing.Size(632, 90);
            this.lblCause.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblSolution, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCause, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblErrorMessage, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 62);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(747, 271);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cause";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 180);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Resolution";
            // 
            // picType
            // 
            this.picType.Image = global::DRPlanningTool.Properties.Resources.help_32_blue;
            this.picType.Location = new System.Drawing.Point(16, 15);
            this.picType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picType.Name = "picType";
            this.picType.Size = new System.Drawing.Size(43, 39);
            this.picType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picType.TabIndex = 4;
            this.picType.TabStop = false;
            // 
            // imlMain
            // 
            this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
            this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imlMain.Images.SetKeyName(0, "help_32_blue.png");
            this.imlMain.Images.SetKeyName(1, "Ok_32.png");
            this.imlMain.Images.SetKeyName(2, "Close_32.png");
            // 
            // frmMessageCodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 347);
            this.Controls.Add(this.picType);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnGetCode);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(527, 383);
            this.Name = "frmMessageCodes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message Codes";
            this.Load += new System.EventHandler(this.frmErrorCodes_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnGetCode;
        private System.Windows.Forms.PictureBox picType;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.Label lblSolution;
        private System.Windows.Forms.Label lblCause;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ImageList imlMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}