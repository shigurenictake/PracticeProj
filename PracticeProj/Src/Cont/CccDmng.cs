using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace PracticeProj.Src.Cont
{
    internal class CccDmng : Dmng
    {
        private List<Dictionary<string, string>> cTblCccWake; //テーブル
        private List<CccWakeMdl> cCccWakeMdlList; //モデルリスト
        private WakeMng m_cWakeMng; //航跡管理

        /// <summary>
        /// データ初期化
        /// </summary>
        public override void InitData()
        {
            MessageBox.Show("CccDmng : InitData\n");

            cTblCccWake = new List<Dictionary<string, string>>();

            string[] primaryKeys = null;

            //テーブル取得
            primaryKeys = new string[] {};
            (new FileMng()).GetTbl(ref cTblCccWake, "Ccc", "TblCccWake", primaryKeys);
            //cTblCccWake.Sort((x, y) => x["PU_NO"].CompareTo(y["PU_NO"]));
        }

        /// <summary>
        /// モデル取得
        /// </summary>
        /// <param name="sOut"></param>
        public override void GetMdl(ref string[] sOut)
        {
            cCccWakeMdlList = new List<CccWakeMdl>();

            CccWakeMdl cTmpCccWakeMdl = new CccWakeMdl();
            cTmpCccWakeMdl.row = -1; //初期値

            for(int i = 0; i < cTblCccWake.Count; i++)
            {
                //テーブルからレコード取得
                Dictionary<string, string> cRecord = cTblCccWake[i];
                //テーブルから次レコードを取得。ループ末尾の時、ループ末尾フラグ=true、次レコード=nullとする。
                Dictionary<string, string> cNextRecord = null;
                bool bLoopLast;
                if (i < cTblCccWake.Count - 1)
                {
                    bLoopLast = false;
                    cNextRecord = cTblCccWake[i + 1];
                }
                else
                {
                    bLoopLast = true;
                }

                //WAKE_IDに初回または変化した場合、一時モデルを初期化、WAKE_IDを設定
                if ( Int32.TryParse(cRecord["WAKE_ID"], out int wakeId) && (wakeId != cTmpCccWakeMdl.row) )
                {
                    cTmpCccWakeMdl = new CccWakeMdl();
                    cTmpCccWakeMdl.row = wakeId;
                }

                //Pos1組分を取得
                CccWakeMdl.Pos tmpPos = new CccWakeMdl.Pos();
                Int32.TryParse(cRecord["NO"], out int no);
                tmpPos.no = no;
                float.TryParse(cRecord["X"], out tmpPos.x);
                float.TryParse(cRecord["Y"], out tmpPos.y);
                tmpPos.time = cRecord["TIME"];
                //一時モデルにPosを追加
                cTmpCccWakeMdl.pos.Add(tmpPos);

                //次レコードのNOが1の時、またはループ末尾の時、モデルリストに一時モデルを追加
                int inextNo = -1;
                if (cNextRecord != null)
                {
                    Int32.TryParse(cNextRecord["NO"], out inextNo);
                }
                if ( (inextNo == 1) || (bLoopLast == true) )
                {
                    cCccWakeMdlList.Add(cTmpCccWakeMdl);
                }
            }
        }

        /// <summary>
        /// 結果取得
        /// </summary>
        /// <param name="sOut"></param>
        /// <param name="sCond"></param>
        public override void GetRslt(ref string[] sOut, string sCond)
        {
            //MessageBox.Show("\nAaaDmng : GetMdl >");
        }

        //航跡管理セット
        public void SetWakeMng(WakeMng cWakeMng)
        {
            m_cWakeMng = cWakeMng;
        }

        /// <summary>
        /// 航跡初期化
        /// </summary>
        public void InitMap()
        {
            var mapDict = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            foreach (var wakeMdl in cCccWakeMdlList)
            {
                var wakeDict = new Dictionary<string, Dictionary<string, string>>
                {
                    { "info", new Dictionary<string, string>(){ { "row", wakeMdl.row.ToString() } } }
                };

                foreach (var pos in wakeMdl.pos)
                {
                    var posDict = new Dictionary<string, string>
                    {
                        { "x", pos.x.ToString() }, { "y", pos.y.ToString() }, { "time", pos.time }
                    };

                    wakeDict.Add($"pos{pos.no}", posDict);
                }

                mapDict.Add($"aWake{wakeMdl.row}", wakeDict);
            }

            m_cWakeMng.InitWake("Ccc", mapDict, null, null, null);
        }
    }
}
