using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;

namespace Vision
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            Mutex instance = new Mutex(initiallyOwned: true, "MutexName", out createdNew);
            string AllowCreatedMore = ConfigurationManager.AppSettings["AllowCreatedMore"];
            if ((string.IsNullOrWhiteSpace(AllowCreatedMore) || AllowCreatedMore.ToLower() == "false") && !createdNew)
            {
                MessageBox.Show("已经启动了一个程序，请先退出！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(defaultValue: false);
                Application.Run(new MainForm());
            }
        }
    }
}
