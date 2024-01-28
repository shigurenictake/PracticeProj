namespace PracticeProj
{
    partial class FormMain
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelForUCCon = new System.Windows.Forms.Panel();
            this.buttonGoDetail = new System.Windows.Forms.Button();
            this.splitContainerRightUD = new System.Windows.Forms.SplitContainer();
            this.panelBHC = new System.Windows.Forms.Panel();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.m_cUctrlMap = new PracticeProj.UctrlMap();
            this.splitContainerLeftUD = new System.Windows.Forms.SplitContainer();
            this.m_cWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.splitContainerLR = new System.Windows.Forms.SplitContainer();
            this.panelForUCCon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRightUD)).BeginInit();
            this.splitContainerRightUD.Panel1.SuspendLayout();
            this.splitContainerRightUD.Panel2.SuspendLayout();
            this.splitContainerRightUD.SuspendLayout();
            this.panelBHC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftUD)).BeginInit();
            this.splitContainerLeftUD.Panel2.SuspendLayout();
            this.splitContainerLeftUD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cWebView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLR)).BeginInit();
            this.splitContainerLR.Panel1.SuspendLayout();
            this.splitContainerLR.Panel2.SuspendLayout();
            this.splitContainerLR.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForUCCon
            // 
            this.panelForUCCon.BackColor = System.Drawing.SystemColors.Control;
            this.panelForUCCon.Controls.Add(this.buttonGoDetail);
            this.panelForUCCon.Location = new System.Drawing.Point(0, 0);
            this.panelForUCCon.Margin = new System.Windows.Forms.Padding(0);
            this.panelForUCCon.Name = "panelForUCCon";
            this.panelForUCCon.Size = new System.Drawing.Size(200, 26);
            this.panelForUCCon.TabIndex = 17;
            // 
            // buttonGoDetail
            // 
            this.buttonGoDetail.Location = new System.Drawing.Point(3, 3);
            this.buttonGoDetail.Margin = new System.Windows.Forms.Padding(0);
            this.buttonGoDetail.Name = "buttonGoDetail";
            this.buttonGoDetail.Size = new System.Drawing.Size(75, 23);
            this.buttonGoDetail.TabIndex = 0;
            this.buttonGoDetail.Text = "詳細";
            this.buttonGoDetail.UseVisualStyleBackColor = true;
            // 
            // splitContainerRightUD
            // 
            this.splitContainerRightUD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRightUD.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerRightUD.IsSplitterFixed = true;
            this.splitContainerRightUD.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRightUD.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerRightUD.Name = "splitContainerRightUD";
            this.splitContainerRightUD.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRightUD.Panel1
            // 
            this.splitContainerRightUD.Panel1.Controls.Add(this.panelForUCCon);
            this.splitContainerRightUD.Panel1.Controls.Add(this.panelBHC);
            this.splitContainerRightUD.Panel1MinSize = 26;
            // 
            // splitContainerRightUD.Panel2
            // 
            this.splitContainerRightUD.Panel2.AutoScroll = true;
            this.splitContainerRightUD.Panel2.AutoScrollMinSize = new System.Drawing.Size(0, 600);
            this.splitContainerRightUD.Panel2.Controls.Add(this.m_cUctrlMap);
            this.splitContainerRightUD.Size = new System.Drawing.Size(488, 446);
            this.splitContainerRightUD.SplitterDistance = 26;
            this.splitContainerRightUD.SplitterWidth = 1;
            this.splitContainerRightUD.TabIndex = 0;
            // 
            // panelBHC
            // 
            this.panelBHC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBHC.BackColor = System.Drawing.SystemColors.Control;
            this.panelBHC.Controls.Add(this.buttonHelp);
            this.panelBHC.Controls.Add(this.buttonBack);
            this.panelBHC.Controls.Add(this.buttonClose);
            this.panelBHC.Location = new System.Drawing.Point(301, 0);
            this.panelBHC.Margin = new System.Windows.Forms.Padding(0);
            this.panelBHC.Name = "panelBHC";
            this.panelBHC.Size = new System.Drawing.Size(187, 26);
            this.panelBHC.TabIndex = 13;
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelp.Location = new System.Drawing.Point(64, 3);
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(0);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(60, 23);
            this.buttonHelp.TabIndex = 17;
            this.buttonHelp.Text = "ヘルプ";
            this.buttonHelp.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBack.Location = new System.Drawing.Point(4, 3);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(0);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(60, 23);
            this.buttonBack.TabIndex = 18;
            this.buttonBack.Text = "戻る";
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(124, 3);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(60, 23);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "閉じる";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // m_cUctrlMap
            // 
            this.m_cUctrlMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cUctrlMap.Location = new System.Drawing.Point(0, 0);
            this.m_cUctrlMap.Name = "m_cUctrlMap";
            this.m_cUctrlMap.Size = new System.Drawing.Size(471, 600);
            this.m_cUctrlMap.TabIndex = 0;
            // 
            // splitContainerLeftUD
            // 
            this.splitContainerLeftUD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeftUD.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerLeftUD.IsSplitterFixed = true;
            this.splitContainerLeftUD.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeftUD.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerLeftUD.Name = "splitContainerLeftUD";
            this.splitContainerLeftUD.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerLeftUD.Panel1Collapsed = true;
            this.splitContainerLeftUD.Panel1MinSize = 26;
            // 
            // splitContainerLeftUD.Panel2
            // 
            this.splitContainerLeftUD.Panel2.Controls.Add(this.m_cWebView);
            this.splitContainerLeftUD.Size = new System.Drawing.Size(300, 446);
            this.splitContainerLeftUD.SplitterDistance = 26;
            this.splitContainerLeftUD.SplitterWidth = 1;
            this.splitContainerLeftUD.TabIndex = 0;
            // 
            // m_cWebView
            // 
            this.m_cWebView.AllowExternalDrop = true;
            this.m_cWebView.CreationProperties = null;
            this.m_cWebView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.m_cWebView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cWebView.Location = new System.Drawing.Point(0, 0);
            this.m_cWebView.Name = "m_cWebView";
            this.m_cWebView.Size = new System.Drawing.Size(300, 446);
            this.m_cWebView.TabIndex = 0;
            this.m_cWebView.ZoomFactor = 1D;
            // 
            // splitContainerLR
            // 
            this.splitContainerLR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerLR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLR.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLR.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerLR.Name = "splitContainerLR";
            // 
            // splitContainerLR.Panel1
            // 
            this.splitContainerLR.Panel1.Controls.Add(this.splitContainerLeftUD);
            // 
            // splitContainerLR.Panel2
            // 
            this.splitContainerLR.Panel2.Controls.Add(this.splitContainerRightUD);
            this.splitContainerLR.Panel2MinSize = 450;
            this.splitContainerLR.Size = new System.Drawing.Size(800, 450);
            this.splitContainerLR.SplitterDistance = 304;
            this.splitContainerLR.TabIndex = 15;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainerLR);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.panelForUCCon.ResumeLayout(false);
            this.splitContainerRightUD.Panel1.ResumeLayout(false);
            this.splitContainerRightUD.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRightUD)).EndInit();
            this.splitContainerRightUD.ResumeLayout(false);
            this.panelBHC.ResumeLayout(false);
            this.splitContainerLeftUD.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftUD)).EndInit();
            this.splitContainerLeftUD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_cWebView)).EndInit();
            this.splitContainerLR.Panel1.ResumeLayout(false);
            this.splitContainerLR.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLR)).EndInit();
            this.splitContainerLR.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelForUCCon;
        private System.Windows.Forms.Button buttonGoDetail;
        private System.Windows.Forms.SplitContainer splitContainerRightUD;
        private System.Windows.Forms.Panel panelBHC;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.SplitContainer splitContainerLeftUD;
        private System.Windows.Forms.SplitContainer splitContainerLR;
        private Microsoft.Web.WebView2.WinForms.WebView2 m_cWebView;
        private UctrlMap m_cUctrlMap;
    }
}

