using System;
using System.Windows.Forms;


namespace TG.CaseStudy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Construction.SubMain();

            //Program.SubMain();
        }

        private static void SubMain()
        {
        }
    }
}
