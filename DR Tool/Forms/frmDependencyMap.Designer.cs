namespace DRPlanningTool
{
    partial class frmDependencyMap
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
            Microsoft.AGL.Core.Geometry.Curves.PlaneTransformation planeTransformation1 = new Microsoft.AGL.Core.Geometry.Curves.PlaneTransformation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDependencyMap));
            this.GraphViewer = new Microsoft.AGL.GraphViewerGdi.GViewer();
            this.pgdObject = new Rajeev.Windows.Forms.RPropertyGrid();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.chkShowServers = new System.Windows.Forms.CheckBox();
            this.chkShowDependents = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.chkShowProperties = new System.Windows.Forms.CheckBox();
            this.cmbLayout = new System.Windows.Forms.ComboBox();
            this.lblMandatoryDependent = new System.Windows.Forms.Label();
            this.lblMandatoryComponent = new System.Windows.Forms.Label();
            this.lblOptionalComponent = new System.Windows.Forms.Label();
            this.pnlOptionalComponent = new System.Windows.Forms.Panel();
            this.pnlOptionalDependent = new System.Windows.Forms.Panel();
            this.lblOptionalDependent = new System.Windows.Forms.Label();
            this.pnlMandatoryComponent = new System.Windows.Forms.Panel();
            this.pnlMandatoryDependent = new System.Windows.Forms.Panel();
            this.lblDependentText = new System.Windows.Forms.Label();
            this.lblComponentText = new System.Windows.Forms.Label();
            this.lblLayoutMethod = new System.Windows.Forms.Label();
            this.lblSelectedObjectName = new System.Windows.Forms.Label();
            this.chkBundledSplines = new System.Windows.Forms.CheckBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GraphViewer
            // 
            this.GraphViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphViewer.ArrowheadLength = 10D;
            this.GraphViewer.AsyncLayout = true;
            this.GraphViewer.AutoScroll = true;
            this.GraphViewer.BackwardEnabled = false;
            this.GraphViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GraphViewer.BuildHitTree = true;
            this.GraphViewer.CurrentLayoutMethod = Microsoft.AGL.GraphViewerGdi.LayoutMethod.SugiyamaScheme;
            this.GraphViewer.FileName = "";
            this.GraphViewer.ForwardEnabled = false;
            this.GraphViewer.Graph = null;
            this.GraphViewer.InsertingEdge = false;
            this.GraphViewer.LayoutAlgorithmSettingsButtonVisible = true;
            this.GraphViewer.LayoutEditingEnabled = true;
            this.GraphViewer.Location = new System.Drawing.Point(12, 12);
            this.GraphViewer.LooseOffsetForRouting = 0.25D;
            this.GraphViewer.MouseHitDistance = 0.05D;
            this.GraphViewer.Name = "GraphViewer";
            this.GraphViewer.NavigationVisible = false;
            this.GraphViewer.NeedToCalculateLayout = true;
            this.GraphViewer.OffsetForRelaxingInRouting = 0.6D;
            this.GraphViewer.PaddingForEdgeRouting = 8D;
            this.GraphViewer.PanButtonPressed = false;
            this.GraphViewer.SaveAsImageEnabled = false;
            this.GraphViewer.SaveAsMsaglEnabled = false;
            this.GraphViewer.SaveButtonVisible = false;
            this.GraphViewer.SaveGraphButtonVisible = false;
            this.GraphViewer.SaveInVectorFormatEnabled = true;
            this.GraphViewer.Size = new System.Drawing.Size(760, 483);
            this.GraphViewer.TabIndex = 1;
            this.GraphViewer.TightOffsetForRouting = 0.125D;
            this.GraphViewer.ToolBarIsVisible = false;
            this.GraphViewer.Transform = planeTransformation1;
            this.GraphViewer.WindowZoomButtonPressed = false;
            this.GraphViewer.ZoomF = 1D;
            this.GraphViewer.ZoomFraction = 0.5D;
            this.GraphViewer.ZoomWhenMouseWheelScroll = true;
            this.GraphViewer.ZoomWindowThreshold = 0.05D;
            this.GraphViewer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GraphViewer_MouseClick);
            this.GraphViewer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GraphViewer_MouseDoubleClick);
            // 
            // pgdObject
            // 
            this.pgdObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgdObject.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pgdObject.Location = new System.Drawing.Point(416, 35);
            this.pgdObject.Name = "pgdObject";
            this.pgdObject.ReadOnly = true;
            this.pgdObject.Size = new System.Drawing.Size(356, 460);
            this.pgdObject.TabIndex = 2;
            this.pgdObject.ToolbarVisible = false;
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveImage.Location = new System.Drawing.Point(697, 501);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(75, 23);
            this.btnSaveImage.TabIndex = 5;
            this.btnSaveImage.Text = "Save Image";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // chkShowServers
            // 
            this.chkShowServers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowServers.AutoSize = true;
            this.chkShowServers.Location = new System.Drawing.Point(370, 515);
            this.chkShowServers.Name = "chkShowServers";
            this.chkShowServers.Size = new System.Drawing.Size(92, 17);
            this.chkShowServers.TabIndex = 11;
            this.chkShowServers.Text = "Show Servers";
            this.chkShowServers.UseVisualStyleBackColor = true;
            this.chkShowServers.Visible = false;
            // 
            // chkShowDependents
            // 
            this.chkShowDependents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowDependents.AutoSize = true;
            this.chkShowDependents.Location = new System.Drawing.Point(250, 538);
            this.chkShowDependents.Name = "chkShowDependents";
            this.chkShowDependents.Size = new System.Drawing.Size(114, 17);
            this.chkShowDependents.TabIndex = 17;
            this.chkShowDependents.Text = "Show Dependents";
            this.chkShowDependents.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(697, 534);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 19;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // chkShowProperties
            // 
            this.chkShowProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowProperties.AutoSize = true;
            this.chkShowProperties.Location = new System.Drawing.Point(250, 515);
            this.chkShowProperties.Name = "chkShowProperties";
            this.chkShowProperties.Size = new System.Drawing.Size(103, 17);
            this.chkShowProperties.TabIndex = 10;
            this.chkShowProperties.Text = "Show Properties";
            this.chkShowProperties.UseVisualStyleBackColor = true;
            this.chkShowProperties.CheckedChanged += new System.EventHandler(this.chkShowProperties_CheckedChanged);
            // 
            // cmbLayout
            // 
            this.cmbLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbLayout.FormattingEnabled = true;
            this.cmbLayout.Location = new System.Drawing.Point(482, 536);
            this.cmbLayout.Name = "cmbLayout";
            this.cmbLayout.Size = new System.Drawing.Size(103, 21);
            this.cmbLayout.TabIndex = 18;
            this.cmbLayout.SelectedIndexChanged += new System.EventHandler(this.cmbLayout_SelectedIndexChanged);
            // 
            // lblMandatoryDependent
            // 
            this.lblMandatoryDependent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMandatoryDependent.Location = new System.Drawing.Point(126, 516);
            this.lblMandatoryDependent.Name = "lblMandatoryDependent";
            this.lblMandatoryDependent.Size = new System.Drawing.Size(62, 15);
            this.lblMandatoryDependent.TabIndex = 8;
            this.lblMandatoryDependent.Text = "Mandatory";
            // 
            // lblMandatoryComponent
            // 
            this.lblMandatoryComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMandatoryComponent.Location = new System.Drawing.Point(12, 516);
            this.lblMandatoryComponent.Name = "lblMandatoryComponent";
            this.lblMandatoryComponent.Size = new System.Drawing.Size(62, 15);
            this.lblMandatoryComponent.TabIndex = 6;
            this.lblMandatoryComponent.Text = "Mandatory";
            // 
            // lblOptionalComponent
            // 
            this.lblOptionalComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOptionalComponent.Location = new System.Drawing.Point(12, 537);
            this.lblOptionalComponent.Name = "lblOptionalComponent";
            this.lblOptionalComponent.Size = new System.Drawing.Size(62, 15);
            this.lblOptionalComponent.TabIndex = 13;
            this.lblOptionalComponent.Text = "Optional";
            // 
            // pnlOptionalComponent
            // 
            this.pnlOptionalComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlOptionalComponent.BackColor = System.Drawing.Color.Orange;
            this.pnlOptionalComponent.Location = new System.Drawing.Point(80, 537);
            this.pnlOptionalComponent.Name = "pnlOptionalComponent";
            this.pnlOptionalComponent.Size = new System.Drawing.Size(32, 15);
            this.pnlOptionalComponent.TabIndex = 14;
            // 
            // pnlOptionalDependent
            // 
            this.pnlOptionalDependent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlOptionalDependent.BackColor = System.Drawing.Color.Khaki;
            this.pnlOptionalDependent.Location = new System.Drawing.Point(194, 537);
            this.pnlOptionalDependent.Name = "pnlOptionalDependent";
            this.pnlOptionalDependent.Size = new System.Drawing.Size(32, 15);
            this.pnlOptionalDependent.TabIndex = 16;
            // 
            // lblOptionalDependent
            // 
            this.lblOptionalDependent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOptionalDependent.Location = new System.Drawing.Point(126, 537);
            this.lblOptionalDependent.Name = "lblOptionalDependent";
            this.lblOptionalDependent.Size = new System.Drawing.Size(62, 15);
            this.lblOptionalDependent.TabIndex = 15;
            this.lblOptionalDependent.Text = "Optional";
            // 
            // pnlMandatoryComponent
            // 
            this.pnlMandatoryComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMandatoryComponent.BackColor = System.Drawing.Color.Peru;
            this.pnlMandatoryComponent.Location = new System.Drawing.Point(80, 516);
            this.pnlMandatoryComponent.Name = "pnlMandatoryComponent";
            this.pnlMandatoryComponent.Size = new System.Drawing.Size(32, 15);
            this.pnlMandatoryComponent.TabIndex = 7;
            // 
            // pnlMandatoryDependent
            // 
            this.pnlMandatoryDependent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMandatoryDependent.BackColor = System.Drawing.Color.DarkKhaki;
            this.pnlMandatoryDependent.Location = new System.Drawing.Point(194, 516);
            this.pnlMandatoryDependent.Name = "pnlMandatoryDependent";
            this.pnlMandatoryDependent.Size = new System.Drawing.Size(32, 15);
            this.pnlMandatoryDependent.TabIndex = 9;
            // 
            // lblDependentText
            // 
            this.lblDependentText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDependentText.Location = new System.Drawing.Point(129, 498);
            this.lblDependentText.Name = "lblDependentText";
            this.lblDependentText.Size = new System.Drawing.Size(97, 15);
            this.lblDependentText.TabIndex = 4;
            this.lblDependentText.Text = "Dependents";
            this.lblDependentText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblComponentText
            // 
            this.lblComponentText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblComponentText.Location = new System.Drawing.Point(15, 498);
            this.lblComponentText.Name = "lblComponentText";
            this.lblComponentText.Size = new System.Drawing.Size(97, 15);
            this.lblComponentText.TabIndex = 3;
            this.lblComponentText.Text = "Components";
            this.lblComponentText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblLayoutMethod
            // 
            this.lblLayoutMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLayoutMethod.Location = new System.Drawing.Point(479, 516);
            this.lblLayoutMethod.Name = "lblLayoutMethod";
            this.lblLayoutMethod.Size = new System.Drawing.Size(106, 16);
            this.lblLayoutMethod.TabIndex = 12;
            this.lblLayoutMethod.Text = "Layout Method";
            this.lblLayoutMethod.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSelectedObjectName
            // 
            this.lblSelectedObjectName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedObjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedObjectName.Location = new System.Drawing.Point(413, 9);
            this.lblSelectedObjectName.Name = "lblSelectedObjectName";
            this.lblSelectedObjectName.Size = new System.Drawing.Size(359, 23);
            this.lblSelectedObjectName.TabIndex = 0;
            this.lblSelectedObjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkBundledSplines
            // 
            this.chkBundledSplines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBundledSplines.AutoSize = true;
            this.chkBundledSplines.Location = new System.Drawing.Point(370, 540);
            this.chkBundledSplines.Name = "chkBundledSplines";
            this.chkBundledSplines.Size = new System.Drawing.Size(96, 17);
            this.chkBundledSplines.TabIndex = 20;
            this.chkBundledSplines.Text = "Bundle Splines";
            this.chkBundledSplines.UseVisualStyleBackColor = true;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(616, 501);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 21;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // frmDependencyMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.chkBundledSplines);
            this.Controls.Add(this.GraphViewer);
            this.Controls.Add(this.lblSelectedObjectName);
            this.Controls.Add(this.lblLayoutMethod);
            this.Controls.Add(this.lblMandatoryDependent);
            this.Controls.Add(this.lblMandatoryComponent);
            this.Controls.Add(this.lblOptionalComponent);
            this.Controls.Add(this.pnlOptionalComponent);
            this.Controls.Add(this.pnlOptionalDependent);
            this.Controls.Add(this.lblOptionalDependent);
            this.Controls.Add(this.pnlMandatoryComponent);
            this.Controls.Add(this.pnlMandatoryDependent);
            this.Controls.Add(this.lblDependentText);
            this.Controls.Add(this.lblComponentText);
            this.Controls.Add(this.cmbLayout);
            this.Controls.Add(this.chkShowProperties);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.chkShowServers);
            this.Controls.Add(this.chkShowDependents);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.pgdObject);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmDependencyMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dependency Map";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDependencyMap_FormClosing);
            this.Load += new System.EventHandler(this.frmDependencyMap_Load);
            this.Shown += new System.EventHandler(this.frmDependencyMap_Shown);
            this.ResizeEnd += new System.EventHandler(this.frmDependencyMap_ResizeEnd);
            this.Move += new System.EventHandler(this.frmDependencyMap_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.AGL.GraphViewerGdi.GViewer GraphViewer;
        private Rajeev.Windows.Forms.RPropertyGrid pgdObject;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.CheckBox chkShowServers;
        private System.Windows.Forms.CheckBox chkShowDependents;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox chkShowProperties;
        private System.Windows.Forms.ComboBox cmbLayout;
        private System.Windows.Forms.Label lblMandatoryDependent;
        private System.Windows.Forms.Label lblMandatoryComponent;
        private System.Windows.Forms.Label lblOptionalComponent;
        private System.Windows.Forms.Panel pnlOptionalComponent;
        private System.Windows.Forms.Panel pnlOptionalDependent;
        private System.Windows.Forms.Label lblOptionalDependent;
        private System.Windows.Forms.Panel pnlMandatoryComponent;
        private System.Windows.Forms.Panel pnlMandatoryDependent;
        private System.Windows.Forms.Label lblDependentText;
        private System.Windows.Forms.Label lblComponentText;
        private System.Windows.Forms.Label lblLayoutMethod;
        private System.Windows.Forms.Label lblSelectedObjectName;
        private System.Windows.Forms.CheckBox chkBundledSplines;
        private System.Windows.Forms.Button btnCopy;
    }
}