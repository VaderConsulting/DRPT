namespace DRPlanningTool
{
    partial class frmRecoveryTasks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecoveryTasks));
            this.lvwTasks = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvwTasks
            // 
            this.lvwTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvwTasks.Location = new System.Drawing.Point(12, 12);
            this.lvwTasks.Name = "lvwTasks";
            this.lvwTasks.Size = new System.Drawing.Size(392, 238);
            this.lvwTasks.TabIndex = 0;
            this.lvwTasks.UseCompatibleStateImageBehavior = false;
            this.lvwTasks.View = System.Windows.Forms.View.Details;
            this.lvwTasks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwTasks_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 260;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Estimated Time";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 100;
            // 
            // frmRecoveryTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 262);
            this.Controls.Add(this.lvwTasks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(432, 300);
            this.Name = "frmRecoveryTasks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recovery Tasks";
            this.Load += new System.EventHandler(this.frmRecoveryTasks_Load);
            this.Resize += new System.EventHandler(this.frmRecoveryTasks_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwTasks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;

    }
}