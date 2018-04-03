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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.dataGridViewVect = new System.Windows.Forms.DataGridView();
            this.GroupRotation = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarX = new System.Windows.Forms.TrackBar();
            this.trackBarY = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVect)).BeginInit();
            this.GroupRotation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarY)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(14, 14);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(662, 634);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 30;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
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
            this.dataGridViewVect.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewVect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVect.Location = new System.Drawing.Point(684, 17);
            this.dataGridViewVect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewVect.MultiSelect = false;
            this.dataGridViewVect.Name = "dataGridViewVect";
            this.dataGridViewVect.ReadOnly = true;
            this.dataGridViewVect.RowHeadersVisible = false;
            this.dataGridViewVect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewVect.ShowCellToolTips = false;
            this.dataGridViewVect.Size = new System.Drawing.Size(351, 412);
            this.dataGridViewVect.TabIndex = 36;
            this.dataGridViewVect.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVect_CellDoubleClick);
            // 
            // GroupRotation
            // 
            this.GroupRotation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupRotation.Controls.Add(this.label4);
            this.GroupRotation.Controls.Add(this.label3);
            this.GroupRotation.Controls.Add(this.trackBarX);
            this.GroupRotation.Controls.Add(this.trackBarY);
            this.GroupRotation.Location = new System.Drawing.Point(684, 438);
            this.GroupRotation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupRotation.Name = "GroupRotation";
            this.GroupRotation.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupRotation.Size = new System.Drawing.Size(346, 206);
            this.GroupRotation.TabIndex = 37;
            this.GroupRotation.TabStop = false;
            this.GroupRotation.Text = "Rotation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 103);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "X";
            // 
            // trackBarX
            // 
            this.trackBarX.Location = new System.Drawing.Point(9, 49);
            this.trackBarX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trackBarX.Maximum = 360;
            this.trackBarX.Name = "trackBarX";
            this.trackBarX.Size = new System.Drawing.Size(328, 45);
            this.trackBarX.TabIndex = 0;
            this.trackBarX.Scroll += new System.EventHandler(this.trackBarX_Scroll);
            // 
            // trackBarY
            // 
            this.trackBarY.Location = new System.Drawing.Point(9, 128);
            this.trackBarY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trackBarY.Maximum = 360;
            this.trackBarY.Name = "trackBarY";
            this.trackBarY.Size = new System.Drawing.Size(328, 45);
            this.trackBarY.TabIndex = 0;
            this.trackBarY.Scroll += new System.EventHandler(this.trackBarY_Scroll);
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1048, 663);
            this.Controls.Add(this.GroupRotation);
            this.Controls.Add(this.dataGridViewVect);
            this.Controls.Add(this.pictureBox);
            this.Name = "GraphForm";
            this.Text = "GraphForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVect)).EndInit();
            this.GroupRotation.ResumeLayout(false);
            this.GroupRotation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarY)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.DataGridView dataGridViewVect;
        private System.Windows.Forms.GroupBox GroupRotation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarY;
        private System.Windows.Forms.TrackBar trackBarX;
    }
}