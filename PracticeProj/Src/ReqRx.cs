using Microsoft.Web.WebView2.WinForms;
using PracticeProj.Src.Cont;

namespace PracticeProj.Src
{
    /// <summary>WebView2に読み込ませるためのJsで実行する関数を保持させたクラス</summary>
    //[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.AutoDual)]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ReqRx
    {
        private FormMain m_cFormMain;
        private WebView2 m_cWebView;
        private ReqTx m_cReqTx;
        private UctrlMap m_cUctrlMap;

        private Dmng m_cDmng;
        private TopMenuDmng m_cTopMenuDmng;
        private AaaDmng m_cAaaDmng;
        private BbbDmng m_cBbbDmng;
        private CccDmng m_cCccDmng;

        private WakeMng m_cWakeMng;

        private string m_sPrepArg;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cFormMain"></param>
        /// <param name="cWebView"></param>
        /// <param name="cReqTx"></param>
        /// <param name="cUctrlMap"></param>
        public ReqRx(FormMain cFormMain, WebView2 cWebView, ReqTx cReqTx, UctrlMap cUctrlMap)
        {
            m_cFormMain = cFormMain;
            m_cWebView = cWebView;
            m_cReqTx = cReqTx;
            m_cUctrlMap = cUctrlMap;
        }

        /// <summary>
        /// 準備引数セット
        /// </summary>
        /// <param name="sPrepArg"></param>
        public void SetPrepArg(string sPrepArg)
        {
            m_sPrepArg = sPrepArg;
        }

        /// <summary>
        /// レイアウト更新要求
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="iSizeX"></param>
        /// <param name="iSizeY"></param>
        public void ReqUpdLayout(string sType, int iSizeX, int iSizeY)
        {
            switch (sType)
            {
                case "normal":
                    m_cFormMain.UpdLayoutNl(sType, iSizeX, iSizeY);
                    break;
                case "full":
                    m_cFormMain.UpdLayoutNl(sType, iSizeX, iSizeY);
                    break;
                case "map":
                    m_cFormMain.UpdLayoutMap(iSizeX, iSizeY);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// データ初期化要求
        /// </summary>
        /// <param name="sContName"></param>
        public void ReqInitData(string sContName)
        {
            m_cDmng = null;
            m_cTopMenuDmng = null;
            m_cAaaDmng = null;
            m_cBbbDmng = null;

            switch (sContName)
            {
                case "TopMenu":
                    m_cTopMenuDmng = new TopMenuDmng();
                    m_cDmng = m_cTopMenuDmng;
                    break;

                case "Aaa":
                    m_cAaaDmng = new AaaDmng();
                    m_cDmng = m_cAaaDmng;
                    break;

                case "Bbb":
                    m_cBbbDmng = new BbbDmng();
                    m_cDmng = m_cBbbDmng;
                    break;

                case "Ccc":
                    m_cCccDmng = new CccDmng();
                    m_cWakeMng = new WakeMng();

                    m_cDmng = m_cCccDmng;
                    m_cWakeMng.SetUctrlMap(m_cUctrlMap);
                    m_cCccDmng.SetWakeMng(m_cWakeMng);
                    break;

                default:
                    break;
            }

            m_cDmng.SetArg(m_sPrepArg);
            m_cDmng.InitData();
        }

        /// <summary>
        /// モデル取得要求
        /// </summary>
        /// <param name="sContName"></param>
        /// <returns></returns>
        public string[] ReqGetMdl(string sContName)
        {
            string[] sOut=null;
            m_cDmng.GetMdl(ref sOut);
            return sOut;
        }

        /// <summary>
        /// 結果取得要求
        /// </summary>
        /// <param name="sContName"></param>
        /// <param name="sCond"></param>
        /// <returns></returns>
        public string[] ReqGetRslt(string sContName, string sCond)
        {
            string[] sOut = null;
            m_cDmng.GetRslt(ref sOut, sCond);
            return sOut;
        }

        /// <summary>
        /// FormMain複製
        /// </summary>
        /// <param name="sFormName"></param>
        /// <param name="sUri"></param>
        /// <param name="sPrepArg"></param>
        /// <param name="bMulti"></param>
        public void ReqRepsForm(string sFormName, string sUri, string sPrepArg, bool bMulti)
        {
            m_cFormMain.RepsForm(sFormName, sUri, sPrepArg, bMulti);
        }

        //航跡初期化
        public void ReqInitMap()
        {
            m_cCccDmng.InitMap();
        }
    }
}
