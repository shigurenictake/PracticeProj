
using System;
using System.Collections.Specialized;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;


namespace PracticeProj.Src.Cont
{
    internal class BbbDmng : Dmng
    {

        public override void InitData()
        {
            MessageBox.Show("BbbDmng : InitData\n");

            // ファイルパスを指定
            string filePath = @"..\..\Data\properties\file.properties";

            // プロパティを格納する NameValueCollection を作成
            NameValueCollection properties = new NameValueCollection();

            // ファイルが存在するかを確認
            if (File.Exists(filePath))
            {
                // ファイルを一行ずつ読み込み、プロパティと値を NameValueCollection に追加
                foreach (string line in File.ReadLines(filePath))
                {
                    // コメントと空行をスキップ
                    if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("#") || line.Trim().StartsWith("!"))
                        continue;

                    // プロパティと値を分割
                    string[] parts = line.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        // プロパティと値を追加
                        properties[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
            else
            {
                Console.WriteLine("ファイルが見つかりませんでした。");
            }

            // 読み込んだプロパティを表示
            foreach (string key in properties.AllKeys)
            {
                Console.WriteLine($"Key: {key}, Value: {properties[key]}");
            }

        }

        /// <summary>
        /// モデル取得
        /// </summary>
        /// <param name="sOut"></param>
        public override void GetMdl(ref string[] sOut)
        {
            MessageBox.Show("BbbDmng : GetMdl\n");
        }

        /// <summary>
        /// 結果取得
        /// </summary>
        /// <param name="sOut"></param>
        /// <param name="sCond"></param>
        public override void GetRslt(ref string[] sOut, string sCond)
        {
            MessageBox.Show("BbbDmng : GetRslt\n");
        }

    }
}
