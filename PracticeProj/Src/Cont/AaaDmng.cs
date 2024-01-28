using System;
using System.Collections.Generic;

namespace PracticeProj.Src.Cont
{
    internal class AaaDmng : Dmng
    {
        //テーブル
        List<Dictionary<string, string>> cTblAaa;
        //List<Dictionary<string, string>> cTblAaaToc;
        //List<Dictionary<string, string>> cTblAaaTocFile;

        //モデル
        List<AaaMdl> cAaaMdlList = new List<AaaMdl>();

        /// <summary>
        /// データ初期化
        /// </summary>
        public override void InitData()
        {
            //Console.WriteLine("\nAaaDmng : InitData >");

            cTblAaa = new List<Dictionary<string, string>>();
            //cTblAaaToc = new List<Dictionary<string, string>>();
            //cTblAaaTocFile = new List<Dictionary<string, string>>();

            string[] primaryKeys = null;

            //テーブル取得
            primaryKeys = new string[] { "PU_NO" };
            (new FileMng()).GetTbl(ref cTblAaa, "Aaa", "TblAaa", primaryKeys);
            cTblAaa.Sort((x, y) => x["PU_NO"].CompareTo(y["PU_NO"]));

            //Console.WriteLine("\n"+"cTblAaa >");
            //Dubug_ConsoleOut(cTblAaa); //デバッグ
        }

        /// <summary>
        /// モデル取得
        /// </summary>
        /// <param name="sOut"></param>
        public override void GetMdl(ref string[] sOut)
        {
            //MessageBox.Show("\nAaaDmng : GetMdl >");

            foreach (Dictionary<string, string> cDict in cTblAaa)
            {
                AaaMdl cAaaMdl = new AaaMdl();
                Int32.TryParse(cDict["PU_NO"], out cAaaMdl.PuNo);
                Int32.TryParse(cDict["SH_ID"], out cAaaMdl.ShId);
                Int32.TryParse(cDict["CL_ID"], out cAaaMdl.ClId);
                Int32.TryParse(cDict["NA_ID"], out cAaaMdl.NaId);
                cAaaMdl.Name = cDict["NAME"];

                cAaaMdlList.Add(cAaaMdl);
            }

            Console.WriteLine("");
            List<string> sOutList = new List<string>();
            foreach (AaaMdl cAaaMdl in cAaaMdlList)
            {
                sOutList.Add(
                    nameof(cAaaMdl.PuNo) + ":" + cAaaMdl.PuNo.ToString() + " , " +
                    nameof(cAaaMdl.ShId) + ":" + cAaaMdl.ShId.ToString() + " , " +
                    nameof(cAaaMdl.ClId) + ":" + cAaaMdl.ClId.ToString() + " , " +
                    nameof(cAaaMdl.NaId) + ":" + cAaaMdl.NaId.ToString() + " , " +
                    nameof(cAaaMdl.Name) + ":" + cAaaMdl.Name.ToString()
                    );
            }
            sOut = sOutList.ToArray();
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

        //デバッグ
        public void Dubug_ConsoleOut(List<Dictionary<string, string>> cDictList)
        {

            Console.WriteLine("テーブル出力 start >");
            foreach (Dictionary<string, string> cDict in cDictList)
            {
                Console.Write("{ ");
                foreach (KeyValuePair<string, string> item in cDict)
                {
                    Console.Write("{0}: '{1}', ", item.Key, item.Value);
                }
                Console.Write("},\n");
            }

            Console.WriteLine("end >");
        }

    }
}
