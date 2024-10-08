﻿namespace PracticeProj
{
    partial class UctrlMap
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxLegend = new System.Windows.Forms.PictureBox();
            this.mapBox = new SharpMap.Forms.MapBox();
            this.splitContainerUD = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLegend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerUD)).BeginInit();
            this.splitContainerUD.Panel1.SuspendLayout();
            this.splitContainerUD.Panel2.SuspendLayout();
            this.splitContainerUD.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxLegend
            // 
            this.pictureBoxLegend.Location = new System.Drawing.Point(5, 5);
            this.pictureBoxLegend.Name = "pictureBoxLegend";
            this.pictureBoxLegend.Size = new System.Drawing.Size(414, 118);
            this.pictureBoxLegend.TabIndex = 0;
            this.pictureBoxLegend.TabStop = false;
            // 
            // mapBox
            // 
            this.mapBox.ActiveTool = SharpMap.Forms.MapBox.Tools.None;
            this.mapBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mapBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.mapBox.CustomTool = null;
            this.mapBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapBox.FineZoomFactor = 10D;
            this.mapBox.Location = new System.Drawing.Point(0, 0);
            this.mapBox.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.mapBox.Margin = new System.Windows.Forms.Padding(0);
            this.mapBox.Name = "mapBox";
            this.mapBox.QueryGrowFactor = 5F;
            this.mapBox.QueryLayerIndex = 0;
            this.mapBox.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox.ShowProgressUpdate = false;
            this.mapBox.Size = new System.Drawing.Size(435, 399);
            this.mapBox.TabIndex = 0;
            this.mapBox.Text = "mapBox1";
            this.mapBox.WheelZoomMagnitude = -2D;
            this.mapBox.MouseDown += new SharpMap.Forms.MapBox.MouseEventHandler(this.mapBox_MouseDown);
            this.mapBox.MouseUp += new SharpMap.Forms.MapBox.MouseEventHandler(this.mapBox_MouseUp);
            this.mapBox.MapZoomChanged += new SharpMap.Forms.MapBox.MapZoomHandler(this.mapBox_MapZoomChanged);
            this.mapBox.MapCenterChanged += new SharpMap.Forms.MapBox.MapCenterChangedHandler(this.mapBox_MapCenterChanged);
            // 
            // splitContainerUD
            // 
            this.splitContainerUD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerUD.IsSplitterFixed = true;
            this.splitContainerUD.Location = new System.Drawing.Point(0, 0);
            this.splitContainerUD.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerUD.Name = "splitContainerUD";
            this.splitContainerUD.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerUD.Panel1
            // 
            this.splitContainerUD.Panel1.Controls.Add(this.textBox1);
            this.splitContainerUD.Panel1.Controls.Add(this.mapBox);
            // 
            // splitContainerUD.Panel2
            // 
            this.splitContainerUD.Panel2.Controls.Add(this.pictureBoxLegend);
            this.splitContainerUD.Size = new System.Drawing.Size(435, 533);
            this.splitContainerUD.SplitterDistance = 399;
            this.splitContainerUD.SplitterWidth = 1;
            this.splitContainerUD.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(414, 19);
            this.textBox1.TabIndex = 1;
            // 
            // UctrlMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerUD);
            this.Name = "UctrlMap";
            this.Size = new System.Drawing.Size(435, 533);
            this.Load += new System.EventHandler(this.UctrlMap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLegend)).EndInit();
            this.splitContainerUD.Panel1.ResumeLayout(false);
            this.splitContainerUD.Panel1.PerformLayout();
            this.splitContainerUD.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerUD)).EndInit();
            this.splitContainerUD.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLegend;
        public SharpMap.Forms.MapBox mapBox;
        private System.Windows.Forms.SplitContainer splitContainerUD;
        public System.Windows.Forms.TextBox textBox1;
    }
}
