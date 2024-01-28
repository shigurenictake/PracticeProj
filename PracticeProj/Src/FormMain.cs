using PracticeProj.Src;
using System;
using System.Windows.Forms;

namespace PracticeProj
{
    public partial class FormMain : Form
    {
        //クラス変数定義
        private ReqTx m_cReqTx;
        private ReqRx m_cReqRx;
        //private WebView2 m_cWebView; //.Designer.csで定義
        //private UctrlMap m_cUctrlMap; //.Designer.csで定義

        /// <summary>
        /// メインフォーム
        /// </summary>
        /// <param name="sUri"></param>
        /// <param name="sPrepArg"></param>
        public FormMain(string sUri, string sPrepArg)
        {
            //デザイン初期化
            InitializeComponent();

            //クラスのインスタンス生成
            m_cReqTx = new ReqTx(this, m_cWebView);
            m_cReqRx = new ReqRx(this, m_cWebView, m_cReqTx, m_cUctrlMap);
            m_cReqRx.SetPrepArg(sPrepArg);

            //WebView2 URLを設定
            m_cWebView.Source = new Uri(sUri);
            //WebView2イベント登録
            m_cWebView.NavigationStarting += webView_NavigationStarting;
            m_cWebView.SourceChanged += webView_SourceChanged;
            m_cWebView.NavigationCompleted += webView_NavigationCompleted;
        }

        /// <summary>
        /// WebView2イベント NavigationStarting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webView_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            BefLoad();
        }

        /// <summary>
        /// WebView2イベント SourceChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webView_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            BefLoad();
        }

        //イベント WebView2 ロード完了
        private void webView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            try
            {
                if (m_cWebView.CoreWebView2 != null)
                {

                }
                else MessageBox.Show("CoreWebView2==null");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// ロード前処理
        /// </summary>
        private void BefLoad()
        {
            try
            {
                if (m_cWebView.CoreWebView2 != null)
                {
                    //プロジェクトフラグ = true JavaScriptへ送信　※速度重視のためこの１行を直で送信。
                    m_cWebView.ExecuteScriptAsync("const bPracticeProjSta = Boolean(1);");

                    //JavaScriptからC#のメソッドが実行できる様に仕込む
                    m_cWebView.CoreWebView2.AddHostObjectToScript("ReqRx", m_cReqRx);
                }
                else MessageBox.Show("CoreWebView2==null");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// レイアウト更新 ノーマル
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="iSizeX"></param>
        /// <param name="iSizeY"></param>
        public void UpdLayoutNl(string sType, int iSizeX, int iSizeY)
        {
            //フォームサイズ更新
            if (sType == "full")
            {
                this.Size = new System.Drawing.Size(iSizeX, iSizeY);
            }
            else
            {
                this.Size = new System.Drawing.Size(iSizeX, iSizeY + 30);
            }

            //コントロールのレイアウトを一時中断
            this.SuspendLayout();

            //右パネルを非表示
            this.splitContainerLR.Panel2Collapsed = true;

            if (sType == "full")
            {
                //左上パネルを非表示
                this.splitContainerLeftUD.Panel1Collapsed = true;
            }
            else
            {
                //左上パネルを表示
                this.splitContainerLeftUD.Panel1Collapsed = false;
            }

            //「戻る/ヘルプ/閉じる」パネルを左上パネルに移動
            this.splitContainerRightUD.Panel1.Controls.Remove(this.panelBHC);
            this.splitContainerLeftUD.Panel1.Controls.Add(this.panelBHC);
            this.panelBHC.Location = new System.Drawing.Point(
                this.splitContainerLeftUD.Panel1.Width - this.panelBHC.Width,
                this.panelBHC.Location.Y);

            //コントロールのレイアウトを再開
            this.ResumeLayout(false);
        }

        /// <summary>
        /// レイアウト更新 地図
        /// </summary>
        /// <param name="iSizeX"></param>
        /// <param name="iSizeY"></param>
        public void UpdLayoutMap(int iSizeX, int iSizeY)
        {
            //フォームサイズ更新
            this.Size = new System.Drawing.Size(iSizeX, iSizeY + 30);

            //コントロールのレイアウトを一時中断
            this.SuspendLayout();

            //右パネルを表示
            this.splitContainerLR.Panel2Collapsed = false;

            //左上パネルを非表示
            this.splitContainerLeftUD.Panel1Collapsed = true;

            //「戻る/ヘルプ/閉じる」パネルを右上パネルに移動
            this.splitContainerLeftUD.Panel1.Controls.Remove(this.panelBHC);
            this.splitContainerRightUD.Panel1.Controls.Add(this.panelBHC);
            this.panelBHC.Location = new System.Drawing.Point(
                this.splitContainerRightUD.Panel1.Width - this.panelBHC.Width,
                this.panelBHC.Location.Y);

            //UctrlMap初期化
            m_cUctrlMap.ClearMapBox();


            //コントロールのレイアウトを再開
            this.ResumeLayout(false);
        }

        /// <summary>
        /// FormMain複製
        /// </summary>
        /// <param name="sFormName"></param>
        /// <param name="sUri"></param>
        /// <param name="sPrepArg"></param>
        public void RepsForm(string sFormName, string sUri, string sPrepArg, bool bMulti)
        {
            FormMain cRepsForm;

            if (bMulti)
            {
                cRepsForm = new FormMain(sUri, sPrepArg);
                //フォーム名を設定
                cRepsForm.Name = sFormName;
            }
            else
            {
                //指定した名前のフォームがあれば取得する
                cRepsForm = this.GetFormMain(sFormName);

                //二重起動防止
                //ヌル、または破棄されているか判定
                if (cRepsForm == null || cRepsForm.IsDisposed)
                {
                    //新しいフォームを生成
                    cRepsForm = new FormMain(sUri, sPrepArg);
                    //フォーム名を設定
                    cRepsForm.Name = sFormName;
                }
                else
                {
                    //複製フォームリロード
                    cRepsForm.RepsFormReload(sUri, sPrepArg);
                }
            }

            //フォームにフォーカスを当てる。
            if (!cRepsForm.Visible)
            {
                cRepsForm.Show();
            }
            else
            {
                cRepsForm.Activate();
            }
        }

        /// <summary>
        /// FormMainオブジェクト取得
        /// </summary>
        /// <param name="sFormName"></param>
        /// <returns></returns>
        public FormMain GetFormMain(string sFormName)
        {
            FormMain cRetForm = null;
            foreach (FormMain cForm in Application.OpenForms)
            {
                if (cForm.Name == sFormName)
                {
                    cRetForm = cForm;
                    break;
                }
            }
            return cRetForm;
        }

        /// <summary>
        /// 複製フォームリロード
        /// </summary>
        /// <param name="sUri"></param>
        /// <param name="sPrepArg"></param>
        public void RepsFormReload(string sUri, string sPrepArg)
        {
            m_cReqRx.SetPrepArg(sPrepArg);
            m_cWebView.CoreWebView2.Navigate(sUri);
        }
    }
}
