using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PracticeProj.Src
{
    internal class FileMng
    {
        // テーブルの取得
        public void GetTbl(
            ref List<Dictionary<string, string>> rcTbl,
            string sContName,
            string sTblName,
            string[] sPriKeys)
        {
            string sFolderPath = @"..\..\Data";

            //コンテンツフォルダパスリストを作る
            List<string> cContPathList = new List<string>();
            //直下のサブフォルダパス一覧の取得
            string[] sFirstFloorFolderPathList = Directory.GetDirectories(sFolderPath);
            foreach (string sFirstFloorFolderPath in sFirstFloorFolderPathList)
            {
                //Console.WriteLine(sFirstFloorFolderPath);

                //コンテンツフォルダパス一覧を作成
                string[] sContFolderPathList = Directory.GetDirectories(sFirstFloorFolderPath);
                foreach (string sContFolderPath in sContFolderPathList)
                {
                    if (Path.GetFileName(sContFolderPath) == sContName)
                    {
                        cContPathList.Add(sContFolderPath);
                        break;
                    }
                }
            }

            //Console.WriteLine("cContPathList = ");
            //foreach (string c in cContPathList) { Console.WriteLine(c); }

            //指定テーブル名のファイルパスリストを作る
            List<string> cFilePathList = new List<string>();
            foreach (string sContPath in cContPathList)
            {
                //ファイルパスリスト取得(サブフォルダ含む)
                string[] sFilePathListAll = Directory.GetFiles(sContPath, "*.tsv", SearchOption.AllDirectories);
                //string[] cFilePathList = Directory.GetFiles(sContPath, "$*_Ref.csv", SearchOption.AllDirectories);

                //loop ファイルパスリストの件数
                foreach (string sFilePath in sFilePathListAll)
                {
                    //テーブル名の取得 ※ファイル名から「yyyymmddhhmmss_」と「.tsv」を削除
                    string sTabelName = Path.GetFileName(sFilePath).Remove(0, 15);
                    sTabelName = sTabelName.Remove(sTabelName.Length - 4, 4);

                    //Console.WriteLine($"sTabelName={sTabelName}");

                    if (sTabelName == sTblName)
                    {
                        cFilePathList.Add(sFilePath);
                    }
                }
            }

            //ファイルパスリストを降順にする
            cFilePathList.Sort();    //昇順にソートして
            cFilePathList.Reverse(); //反転する

            //表示グリッドデータを作成
            //csvファイルを読み取る
            //loop ファイルパスリストの件数
            foreach (string sFilePath in cFilePathList)
            {
                //Console.WriteLine($"sFilePath={sFilePath}");

                //csvファイルから最新データを取得する
                GetTsv(
                    ref rcTbl,
                    sFilePath,
                    sPriKeys);
            }
        }

        /// <summary>
        /// csvデータ取得
        /// </summary>
        /// <param name="filePath"></param>
        private void GetTsv(
            ref List<Dictionary<string, string>> dictionaryList,
            string filePath,
            string[] primaryKeys)
        {
            //ヘッダ部初期化
            List<string> headerList = new List<string>();

            //ファイルオブジェクト取得
            StreamReader fileObj = new StreamReader(filePath, Encoding.GetEncoding("Shift_JIS"));

            //loop ファイルオブジェクトの件数分 (行末まで繰り返す)
            int i, j;
            for (i = 0; !fileObj.EndOfStream; i++)
            {
                //行データ = 1行ずつファイルオブジェクトの読み込み
                string linedata = fileObj.ReadLine();

                //読み込んだ1行をカンマ毎に分けて配列に格納する
                string[] values = linedata.Split('\t');

                //データ存在フラグ算出
                bool isExistValue = false;
                foreach (string value in values)
                {
                    if (value != "")
                    {
                        isExistValue = true;
                        break;
                    }
                }
                //データが存在しない場合は次のループへ
                if (!isExistValue) { continue; }

                //opt ループカウンタが0の場合
                if (i == 0)
                {
                    //ヘッダ部 = 行データの要素をリスト形式で追加
                    headerList.AddRange(values);
                    //次のループへ
                    continue;
                }

                //1行分のDictionary
                Dictionary<string, string> dictLine = new Dictionary<string, string>();
                //loop ヘッダ部の要素数分
                for (j = 0; j < headerList.Count; j++)
                {
                    //1行分のDictionary
                    dictLine.Add(headerList[j], values[j]);
                }

                //primaryKeyが未読み込みならばリストに追加する
                bool isExistPriKey = false;
                foreach (Dictionary<string, string> dictionary in dictionaryList)
                {
                    if (IsPrimaryKeys(primaryKeys, dictionary, dictLine) == true)
                    {
                        isExistPriKey = true;
                        break;
                    }
                }
                if (isExistPriKey == false)
                {
                    //1行分のDictionary
                    dictionaryList.Add(dictLine);
                }
            }
            fileObj.Close();
        }

        private bool IsPrimaryKeys(
            string[] primaryKeys,
            Dictionary<string, string> dictionary,
            Dictionary<string, string> dictLine
            )
        {
            bool isPrimaryKeys = false;
            int cnt = 0;

            if ( primaryKeys != null ) { return false; }

            foreach (string key in primaryKeys)
            {
                if (dictionary[key] == dictLine[key])
                {
                    cnt++;
                }
            }
            if (cnt == primaryKeys.Length)
            {
                isPrimaryKeys = true;
            }
            return isPrimaryKeys;
        }

    }
}
