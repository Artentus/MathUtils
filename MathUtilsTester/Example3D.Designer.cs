namespace MathUtilsTester
{
    partial class Example3D
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
            this.xTrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.yTrackBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.zTrackBar = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.scalingTrackBar = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.outputPanel = new MathUtilsTester.DoubleBufferedPanel();
            ((System.ComponentModel.ISupportInitialize)(this.xTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scalingTrackBar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "X-Rotation";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xTrackBar
            // 
            this.xTrackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.xTrackBar.Location = new System.Drawing.Point(3, 26);
            this.xTrackBar.Maximum = 1000;
            this.xTrackBar.Name = "xTrackBar";
            this.xTrackBar.Size = new System.Drawing.Size(183, 45);
            this.xTrackBar.TabIndex = 1;
            this.xTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.xTrackBar.Scroll += new System.EventHandler(this.xTrackBar_Scroll);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y-Rotation";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // yTrackBar
            // 
            this.yTrackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.yTrackBar.Location = new System.Drawing.Point(3, 100);
            this.yTrackBar.Maximum = 1000;
            this.yTrackBar.Name = "yTrackBar";
            this.yTrackBar.Size = new System.Drawing.Size(183, 45);
            this.yTrackBar.TabIndex = 3;
            this.yTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.yTrackBar.Scroll += new System.EventHandler(this.yTrackBar_Scroll);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Z-Rotation";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // zTrackBar
            // 
            this.zTrackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.zTrackBar.Location = new System.Drawing.Point(3, 174);
            this.zTrackBar.Maximum = 1000;
            this.zTrackBar.Name = "zTrackBar";
            this.zTrackBar.Size = new System.Drawing.Size(183, 45);
            this.zTrackBar.TabIndex = 5;
            this.zTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.zTrackBar.Scroll += new System.EventHandler(this.zTrackBar_Scroll);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Skalierung";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scalingTrackBar
            // 
            this.scalingTrackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.scalingTrackBar.Location = new System.Drawing.Point(3, 248);
            this.scalingTrackBar.Maximum = 1000;
            this.scalingTrackBar.Name = "scalingTrackBar";
            this.scalingTrackBar.Size = new System.Drawing.Size(183, 45);
            this.scalingTrackBar.TabIndex = 7;
            this.scalingTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.scalingTrackBar.Value = 500;
            this.scalingTrackBar.Scroll += new System.EventHandler(this.scalingTrackBar_Scroll);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.scalingTrackBar, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.zTrackBar, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.yTrackBar, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.xTrackBar, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(189, 471);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.outputPanel);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(15);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(15);
            this.splitContainer1.Size = new System.Drawing.Size(964, 501);
            this.splitContainer1.SplitterDistance = 744;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.TabStop = false;
            // 
            // outputPanel
            // 
            this.outputPanel.BackColor = System.Drawing.Color.Black;
            this.outputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputPanel.ForeColor = System.Drawing.Color.White;
            this.outputPanel.Location = new System.Drawing.Point(15, 15);
            this.outputPanel.Name = "outputPanel";
            this.outputPanel.Size = new System.Drawing.Size(714, 471);
            this.outputPanel.TabIndex = 2;
            this.outputPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.outputPanel_Paint);
            // 
            // Example3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(964, 501);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Example3D";
            this.ShowIcon = false;
            this.Text = "3D-Beispiel";
            ((System.ComponentModel.ISupportInitialize)(this.xTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scalingTrackBar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar xTrackBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar yTrackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar zTrackBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar scalingTrackBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DoubleBufferedPanel outputPanel;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}