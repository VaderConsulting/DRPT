namespace DRPlanningTool
{
    partial class frmSystemCenterQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSystemCenterQuery));
            this.lblComponentName = new System.Windows.Forms.Label();
            this.txtComponentName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblServerResult = new System.Windows.Forms.Label();
            this.lblServiceResult = new System.Windows.Forms.Label();
            this.lblObjectResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblComponentName
            // 
            this.lblComponentName.AutoSize = true;
            this.lblComponentName.Location = new System.Drawing.Point(16, 11);
            this.lblComponentName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComponentName.Name = "lblComponentName";
            this.lblComponentName.Size = new System.Drawing.Size(121, 17);
            this.lblComponentName.TabIndex = 0;
            this.lblComponentName.Text = "Component Name";
            // 
            // txtComponentName
            // 
            this.txtComponentName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComponentName.Location = new System.Drawing.Point(20, 31);
            this.txtComponentName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtComponentName.Name = "txtComponentName";
            this.txtComponentName.Size = new System.Drawing.Size(433, 22);
            this.txtComponentName.TabIndex = 1;
            this.txtComponentName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtComponentName_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(463, 28);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblServerResult
            // 
            this.lblServerResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServerResult.Location = new System.Drawing.Point(17, 64);
            this.lblServerResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerResult.Name = "lblServerResult";
            this.lblServerResult.Size = new System.Drawing.Size(545, 28);
            this.lblServerResult.TabIndex = 3;
            // 
            // lblServiceResult
            // 
            this.lblServiceResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServiceResult.Location = new System.Drawing.Point(17, 92);
            this.lblServiceResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServiceResult.Name = "lblServiceResult";
            this.lblServiceResult.Size = new System.Drawing.Size(545, 28);
            this.lblServiceResult.TabIndex = 4;
            // 
            // lblObjectResult
            // 
            this.lblObjectResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblObjectResult.Location = new System.Drawing.Point(16, 121);
            this.lblObjectResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjectResult.Name = "lblObjectResult";
            this.lblObjectResult.Size = new System.Drawing.Size(545, 28);
            this.lblObjectResult.TabIndex = 5;
            // 
            // frmSystemCenterQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 164);
            this.Controls.Add(this.lblObjectResult);
            this.Controls.Add(this.lblServiceResult);
            this.Controls.Add(this.lblServerResult);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtComponentName);
            this.Controls.Add(this.lblComponentName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(1061, 211);
            this.Name = "frmSystemCenterQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Center Query";
            this.Load += new System.EventHandler(this.frmSystemCenterQuery_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblComponentName;
        private System.Windows.Forms.TextBox txtComponentName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblServerResult;
        private System.Windows.Forms.Label lblServiceResult;
        private System.Windows.Forms.Label lblObjectResult;
    }
}