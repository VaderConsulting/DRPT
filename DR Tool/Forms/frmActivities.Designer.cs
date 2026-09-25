namespace DRPlanningTool
{
    partial class frmActivities
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
            this.GraphViewer = new Microsoft.AGL.GraphViewerGdi.GViewer();
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
            this.GraphViewer.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.GraphViewer.BackwardEnabled = false;
            this.GraphViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GraphViewer.BuildHitTree = true;
            this.GraphViewer.CurrentLayoutMethod = Microsoft.AGL.GraphViewerGdi.LayoutMethod.IncrementalLayout;
            this.GraphViewer.FileName = "";
            this.GraphViewer.ForwardEnabled = false;
            this.GraphViewer.Graph = null;
            this.GraphViewer.InsertingEdge = false;
            this.GraphViewer.LayoutAlgorithmSettingsButtonVisible = true;
            this.GraphViewer.LayoutEditingEnabled = true;
            this.GraphViewer.Location = new System.Drawing.Point(16, 15);
            this.GraphViewer.LooseOffsetForRouting = 0.25D;
            this.GraphViewer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.GraphViewer.Size = new System.Drawing.Size(507, 442);
            this.GraphViewer.TabIndex = 0;
            this.GraphViewer.TightOffsetForRouting = 0.125D;
            this.GraphViewer.ToolBarIsVisible = true;
            this.GraphViewer.Transform = planeTransformation1;
            this.GraphViewer.WindowZoomButtonPressed = false;
            this.GraphViewer.ZoomF = 1D;
            this.GraphViewer.ZoomFraction = 0.5D;
            this.GraphViewer.ZoomWhenMouseWheelScroll = true;
            this.GraphViewer.ZoomWindowThreshold = 0.05D;
            this.GraphViewer.GraphChanged += new System.EventHandler(this.GraphViewer_GraphChanged);
            // 
            // frmActivities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 694);
            this.Controls.Add(this.GraphViewer);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmActivities";
            this.Text = "frmActivities";
            this.Load += new System.EventHandler(this.Activities_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.AGL.GraphViewerGdi.GViewer GraphViewer;
    }
}