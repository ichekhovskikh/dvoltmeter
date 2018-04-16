namespace DigitalVoltmeter.Forms
{
    partial class GraphForm
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
            this.dataGridViewVect = new System.Windows.Forms.DataGridView();
            this.contextMenuStripGridCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupRotation = new System.Windows.Forms.GroupBox();
            this.labelZoom = new System.Windows.Forms.Label();
            this.trackBarZoom = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarX = new System.Windows.Forms.TrackBar();
            this.trackBarY = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVect)).BeginInit();
            this.contextMenuStripGridCopy.SuspendLayout();
            this.GroupRotation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewVect
            // 
            this.dataGridViewVect.AllowUserToAddRows = false;
            this.dataGridViewVect.AllowUserToDeleteRows = false;
            this.dataGridViewVect.AllowUserToOrderColumns = true;
            this.dataGridViewVect.AllowUserToResizeColumns = false;
            this.dataGridViewVect.AllowUserToResizeRows = false;
            this.dataGridViewVect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewVect.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridViewVect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVect.ContextMenuStrip = this.contextMenuStripGridCopy;
            this.dataGridViewVect.Location = new System.Drawing.Point(608, 14);
            this.dataGridViewVect.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewVect.Name = "dataGridViewVect";
            this.dataGridViewVect.ReadOnly = true;
            this.dataGridViewVect.RowHeadersVisible = false;
            this.dataGridViewVect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewVect.ShowCellToolTips = false;
            this.dataGridViewVect.Size = new System.Drawing.Size(312, 310);
            this.dataGridViewVect.TabIndex = 36;
            this.dataGridViewVect.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVect_CellDoubleClick);
            // 
            // contextMenuStripGridCopy
            // 
            this.contextMenuStripGridCopy.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripGridCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy});
            this.contextMenuStripGridCopy.Name = "contextMenuStripGridCopy";
            this.contextMenuStripGridCopy.Size = new System.Drawing.Size(163, 28);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(162, 24);
            this.toolStripMenuItemCopy.Text = "Копировать";
            this.toolStripMenuItemCopy.Visible = false;
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
            // 
            // GroupRotation
            // 
            this.GroupRotation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupRotation.Controls.Add(this.labelZoom);
            this.GroupRotation.Controls.Add(this.trackBarZoom);
            this.GroupRotation.Controls.Add(this.label4);
            this.GroupRotation.Controls.Add(this.label3);
            this.GroupRotation.Controls.Add(this.trackBarX);
            this.GroupRotation.Controls.Add(this.trackBarY);
            this.GroupRotation.Location = new System.Drawing.Point(608, 332);
            this.GroupRotation.Margin = new System.Windows.Forms.Padding(4);
            this.GroupRotation.Name = "GroupRotation";
            this.GroupRotation.Padding = new System.Windows.Forms.Padding(4);
            this.GroupRotation.Size = new System.Drawing.Size(311, 220);
            this.GroupRotation.TabIndex = 37;
            this.GroupRotation.TabStop = false;
            this.GroupRotation.Text = "Rotation";
            // 
            // labelZoom
            // 
            this.labelZoom.AutoSize = true;
            this.labelZoom.Location = new System.Drawing.Point(8, 141);
            this.labelZoom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelZoom.Name = "labelZoom";
            this.labelZoom.Size = new System.Drawing.Size(44, 17);
            this.labelZoom.TabIndex = 4;
            this.labelZoom.Text = "Zoom";
            // 
            // trackBarZoom
            // 
            this.trackBarZoom.Location = new System.Drawing.Point(8, 161);
            this.trackBarZoom.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarZoom.Maximum = 3000;
            this.trackBarZoom.Minimum = -4000;
            this.trackBarZoom.Name = "trackBarZoom";
            this.trackBarZoom.Size = new System.Drawing.Size(292, 56);
            this.trackBarZoom.TabIndex = 3;
            this.trackBarZoom.Value = -4000;
            this.trackBarZoom.Scroll += new System.EventHandler(this.trackBarZoom_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 82);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "X";
            // 
            // trackBarX
            // 
            this.trackBarX.Location = new System.Drawing.Point(8, 39);
            this.trackBarX.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarX.Maximum = 360;
            this.trackBarX.Name = "trackBarX";
            this.trackBarX.Size = new System.Drawing.Size(292, 56);
            this.trackBarX.TabIndex = 0;
            this.trackBarX.Scroll += new System.EventHandler(this.trackBarX_Scroll);
            // 
            // trackBarY
            // 
            this.trackBarY.Location = new System.Drawing.Point(8, 102);
            this.trackBarY.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarY.Maximum = 360;
            this.trackBarY.Name = "trackBarY";
            this.trackBarY.Size = new System.Drawing.Size(292, 56);
            this.trackBarY.TabIndex = 0;
            this.trackBarY.Scroll += new System.EventHandler(this.trackBarY_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 534);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 17);
            this.label1.TabIndex = 38;
            this.label1.Text = "Цена деления по осям: ΔK=0.01 ; Δi=0.01 ;  δсм=0.01";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(12, 11);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(588, 516);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 30;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(932, 565);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GroupRotation);
            this.Controls.Add(this.dataGridViewVect);
            this.Controls.Add(this.pictureBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GraphForm";
            this.Text = "GraphForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVect)).EndInit();
            this.contextMenuStripGridCopy.ResumeLayout(false);
            this.GroupRotation.ResumeLayout(false);
            this.GroupRotation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.DataGridView dataGridViewVect;
        private System.Windows.Forms.GroupBox GroupRotation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarY;
        private System.Windows.Forms.TrackBar trackBarX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelZoom;
        private System.Windows.Forms.TrackBar trackBarZoom;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGridCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
    }
}