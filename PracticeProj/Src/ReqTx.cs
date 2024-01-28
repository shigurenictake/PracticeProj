using Microsoft.Web.WebView2.WinForms;

namespace PracticeProj.Src
{
    public class ReqTx
    {
        private FormMain m_cFormMain;
        private WebView2 m_cWebView;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cFormMain"></param>
        /// <param name="cWebView"></param>
        public ReqTx(FormMain cFormMain, WebView2 cWebView)
        {
            m_cFormMain = cFormMain;
            m_cWebView = cWebView;
        }


    }
}
