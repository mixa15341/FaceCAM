using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
      
            DateTime end = DateTime.Now + TimeSpan.FromSeconds(5);
            while(end > DateTime.Now)
            {
                Application.DoEvents();
            }
            Application.Run(new Form1());
        }
    }
}
