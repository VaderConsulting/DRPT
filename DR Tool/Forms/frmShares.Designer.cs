namespace DRPlanningTool
{
    partial class frmShares
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShares));
            this.lblServerName = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.lstShares = new System.Windows.Forms.ListBox();
            this.lblShares = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.grpAuthentication = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.radioButtonAs = new System.Windows.Forms.RadioButton();
            this.radioButtonCurrentUser = new System.Windows.Forms.RadioButton();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpAuthentication.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblServerName
            // 
            this.lblServerName.Location = new System.Drawing.Point(16, 18);
            this.lblServerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(133, 21);
            this.lblServerName.TabIndex = 0;
            this.lblServerName.Text = "Server Name";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(157, 15);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtServerName.Multiline = true;
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(159, 24);
            this.txtServerName.TabIndex = 1;
            // 
            // lstShares
            // 
            this.lstShares.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstShares.FormattingEnabled = true;
            this.lstShares.ItemHeight = 16;
            this.lstShares.Location = new System.Drawing.Point(93, 186);
            this.lstShares.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstShares.Name = "lstShares";
            this.lstShares.Size = new System.Drawing.Size(335, 148);
            this.lstShares.TabIndex = 5;
            // 
            // lblShares
            // 
            this.lblShares.Location = new System.Drawing.Point(16, 186);
            this.lblShares.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShares.Name = "lblShares";
            this.lblShares.Size = new System.Drawing.Size(69, 21);
            this.lblShares.TabIndex = 4;
            this.lblShares.Text = "Shares";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(329, 143);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 28);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(329, 341);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(100, 28);
            this.btnCopy.TabIndex = 7;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // grpAuthentication
            // 
            this.grpAuthentication.Controls.Add(this.txtPassword);
            this.grpAuthentication.Controls.Add(this.txtUsername);
            this.grpAuthentication.Controls.Add(this.lblPassword);
            this.grpAuthentication.Controls.Add(this.lblUsername);
            this.grpAuthentication.Controls.Add(this.radioButtonAs);
            this.grpAuthentication.Controls.Add(this.radioButtonCurrentUser);
            this.grpAuthentication.Location = new System.Drawing.Point(16, 47);
            this.grpAuthentication.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAuthentication.Name = "grpAuthentication";
            this.grpAuthentication.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAuthentication.Size = new System.Drawing.Size(413, 89);
            this.grpAuthentication.TabIndex = 2;
            this.grpAuthentication.TabStop = false;
            this.grpAuthentication.Text = "Authentication";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(276, 55);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(128, 24);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(276, 23);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUsername.Multiline = true;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(128, 24);
            this.txtUsername.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(188, 59);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(80, 21);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(188, 26);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(80, 21);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Username";
            // 
            // radioButtonAs
            // 
            this.radioButtonAs.AutoSize = true;
            this.radioButtonAs.Location = new System.Drawing.Point(128, 23);
            this.radioButtonAs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonAs.Name = "radioButtonAs";
            this.radioButtonAs.Size = new System.Drawing.Size(57, 21);
            this.radioButtonAs.TabIndex = 1;
            this.radioButtonAs.Text = "As...";
            this.radioButtonAs.UseVisualStyleBackColor = true;
            this.radioButtonAs.CheckedChanged += new System.EventHandler(this.radioButtonAs_CheckedChanged);
            // 
            // radioButtonCurrentUser
            // 
            this.radioButtonCurrentUser.AutoSize = true;
            this.radioButtonCurrentUser.Checked = true;
            this.radioButtonCurrentUser.Location = new System.Drawing.Point(8, 23);
            this.radioButtonCurrentUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonCurrentUser.Name = "radioButtonCurrentUser";
            this.radioButtonCurrentUser.Size = new System.Drawing.Size(110, 21);
            this.radioButtonCurrentUser.TabIndex = 0;
            this.radioButtonCurrentUser.TabStop = true;
            this.radioButtonCurrentUser.Text = "Current User";
            this.radioButtonCurrentUser.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.Location = new System.Drawing.Point(17, 347);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 21);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Idle";
            // 
            // frmShares
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 384);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.grpAuthentication);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblShares);
            this.Controls.Add(this.lstShares);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.lblServerName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(461, 420);
            this.Name = "frmShares";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shares";
            this.Load += new System.EventHandler(this.frmShares_Load);
            this.grpAuthentication.ResumeLayout(false);
            this.grpAuthentication.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.ListBox lstShares;
        private System.Windows.Forms.Label lblShares;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.GroupBox grpAuthentication;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.RadioButton radioButtonAs;
        private System.Windows.Forms.RadioButton radioButtonCurrentUser;
        private System.Windows.Forms.Label lblStatus;
    }
}