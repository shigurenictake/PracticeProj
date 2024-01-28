using System.Windows.Forms;

namespace PracticeProj.Src.Cont
{
    /// <summary>
    /// データ管理クラス
    /// </summary>
    internal class Dmng
    {
        private string m_sArg; //引数

        //共通処理
        public void SetArg(string sArg)
        {
            m_sArg = sArg;
        }
        public string GetArg()
        {
            return m_sArg;
        }

        //virtual関数
        public virtual void InitData()
        {
            MessageBox.Show("Dmng::InitData");
        }
        public virtual void GetMdl(ref string[] sOut) 
        {
            MessageBox.Show("Dmng::GetMdl");
        }
        public virtual void GetRslt(ref string[] sOut, string sCond)
        {
            MessageBox.Show("Dmng::GetRslt");
        }



    }
}
