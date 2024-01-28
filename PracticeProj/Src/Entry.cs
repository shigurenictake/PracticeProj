using System;
using System.Windows.Forms;

namespace PracticeProj
{
    internal static class Entry
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string sUri = System.IO.Path.GetFullPath(@"..\..\Html\TopMenu.html");
            string sPrepArg = "";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(sUri, sPrepArg));
        }
    }
}
