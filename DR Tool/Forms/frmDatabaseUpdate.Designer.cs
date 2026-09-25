namespace DRPlanningTool
{
    partial class frmDatabaseUpdate
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
            this.label1 = new System.Windows.Forms.Label();
            this.radBusinessApplications = new System.Windows.Forms.RadioButton();
            this.radServers = new System.Windows.Forms.RadioButton();
            this.cmbProperties = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStringValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scope";
            // 
            // radBusinessApplications
            // 
            this.radBusinessApplications.AutoSize = true;
            this.radBusinessApplications.Location = new System.Drawing.Point(102, 17);
            this.radBusinessApplications.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radBusinessApplications.Name = "radBusinessApplications";
            this.radBusinessApplications.Size = new System.Drawing.Size(189, 24);
            this.radBusinessApplications.TabIndex = 1;
            this.radBusinessApplications.TabStop = true;
            this.radBusinessApplications.Text = "Business Applications";
            this.radBusinessApplications.UseVisualStyleBackColor = true;
            this.radBusinessApplications.CheckedChanged += new System.EventHandler(this.radBusinessApplications_CheckedChanged);
            // 
            // radServers
            // 
            this.radServers.AutoSize = true;
            this.radServers.Location = new System.Drawing.Point(102, 52);
            this.radServers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radServers.Name = "radServers";
            this.radServers.Size = new System.Drawing.Size(88, 24);
            this.radServers.TabIndex = 2;
            this.radServers.TabStop = true;
            this.radServers.Text = "Servers";
            this.radServers.UseVisualStyleBackColor = true;
            this.radServers.CheckedChanged += new System.EventHandler(this.radServers_CheckedChanged);
            // 
            // cmbProperties
            // 
            this.cmbProperties.FormattingEnabled = true;
            this.cmbProperties.Location = new System.Drawing.Point(102, 88);
            this.cmbProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbProperties.Name = "cmbProperties";
            this.cmbProperties.Size = new System.Drawing.Size(412, 28);
            this.cmbProperties.TabIndex = 4;
            this.cmbProperties.SelectedIndexChanged += new System.EventHandler(this.cmbProperties_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Property";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 134);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Value";
            // 
            // txtStringValue
            // 
            this.txtStringValue.Location = new System.Drawing.Point(102, 129);
            this.txtStringValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStringValue.Name = "txtStringValue";
            this.txtStringValue.Size = new System.Drawing.Size(412, 26);
            this.txtStringValue.TabIndex = 6;
            // 
            // frmDatabaseUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 403);
            this.Controls.Add(this.txtStringValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbProperties);
            this.Controls.Add(this.radServers);
            this.Controls.Add(this.radBusinessApplications);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmDatabaseUpdate";
            this.Text = "Bulk Database Update";
            this.Shown += new System.EventHandler(this.frmDatabaseUpdate_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radBusinessApplications;
        private System.Windows.Forms.RadioButton radServers;
        private System.Windows.Forms.ComboBox cmbProperties;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStringValue;
    }
}