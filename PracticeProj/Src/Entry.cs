using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PracticeProj
{
    internal static class Entry
    {
        // Windows API 関数のインポート
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        //ウィンドウをアクティブにして表示する設定値
        private const int SW_RESTORE = 9;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ユニークなミューテックス名を設定
            string mutexName = "PracticeProjMutex";
            bool createdNew;

            // ミューテックスを作成
            using (Mutex mutex = new Mutex(true, mutexName, out createdNew))
            {
                //ミューテックスの初期所有権が付与されたか判定
                if (createdNew)
                {
                    //メイン処理
                    string sUri = System.IO.Path.GetFullPath(@"..\..\Html\TopMenu.html");
                    string sPrepArg = "";
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain(sUri, sPrepArg));
                }
                else
                {
                    //既存インスタンスを前面に設定
                    BringExistingInstanceToFront();
                }
            }
        }

        //既存インスタンスを前面に設定
        static void BringExistingInstanceToFront()
        {
            //現在のプロセスを取得
            Process currentProcess = Process.GetCurrentProcess();
            //同じ名前の他のプロセスを取得
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            //同じ名前の他のプロセス数ループ
            foreach (Process process in Processes)
            {
                //既存のプロセスか判定
                if (process.Id != currentProcess.Id)
                {
                    //既存のプロセスのウィンドウハンドルを取得
                    IntPtr handle = process.MainWindowHandle;
                    //ウィンドウハンドルがゼロポインタではない
                    if (handle != IntPtr.Zero)
                    {
                        //ウィンドウが最小化されている場合は復元
                        if (IsIconic(handle))
                        {
                            ShowWindow(handle, SW_RESTORE);
                        }
                        // ウィンドウをフォアグラウンドに設定
                        SetForegroundWindow(handle);
                    }
                    break;
                }
            }
        }




    }
}
