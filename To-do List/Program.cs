using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace To_do_List
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Une seule instance à la fois (évite les doublons si on relance l'exe).
            using (new Mutex(true, "To-do List_SingleInstance", out bool isFirstInstance))
            {
                if (!isFirstInstance)
                    return;

                // Fenêtre en pixels réels (630x450) et rendu net même avec une mise à l'échelle Windows > 100 %.
                SetProcessDPIAware();
                StartupManager.ApplyPreference();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
