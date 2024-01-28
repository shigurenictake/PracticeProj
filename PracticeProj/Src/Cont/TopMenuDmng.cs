using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PracticeProj.Src.Cont
{
    internal class TopMenuDmng : Dmng
    {
        //テーブル
        List<Dictionary<string, string>> m_cTblTopMenu;

        /// <summary>
        /// データ初期化
        /// </summary>
        public override void InitData()
        {
            m_cTblTopMenu = new List<Dictionary<string, string>>();

            string[] sPriKeys;

            //テーブル取得
            sPriKeys = new string[] { "ID" };
            (new FileMng()).GetTbl( ref m_cTblTopMenu, "TopMenu", "TblTopMenu", sPriKeys );
            //ソート
            m_cTblTopMenu.Sort((x, y) => x["ID"].CompareTo(y["ID"]));
        }

        /// <summary>
        /// モデル取得
        /// </summary>
        /// <param name="sOut"></param>
        public override void GetMdl(ref string[] sOut)
        {
            var cTbl = m_cTblTopMenu;

            List<string> cOut = new List<string>();
            foreach (Dictionary<string, string> cRecord in cTbl)
            {
                string sRecord;
                sRecord = sRecord = "{ ";
                sRecord += string.Join(", ", cRecord.Select(kv => $"{kv.Key}: '{kv.Value}'"));
                sRecord += " }";

                cOut.Add( sRecord );
            }
            sOut = cOut.ToArray();
        }

        public override void GetRslt(ref string[] sOut, string sCond)
        {
            MessageBox.Show("TopMenuDmng : GetRslt\n【T.B.D.】");
        }

    }
}
