using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace SocketTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());

            //hello word!  政府的规定是否噶舒服撒饭都vs 的所发生的 
            //hello word!  




        }
    }
}
