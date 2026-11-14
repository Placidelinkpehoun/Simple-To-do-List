using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace To_do_List
{
    /// <summary>
    /// Gère le lancement automatique au démarrage de Windows (clé Run de l'utilisateur courant).
    /// </summary>
    internal static class StartupManager
    {
        private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string ValueName = "To-do List";

        // Présent uniquement si l'utilisateur a désactivé le démarrage automatique depuis le menu.
        private static readonly string DisabledFlag = Path.Combine(TaskStorage.DataFolder, "startup-disabled");

        private static string Command => "\"" + Path.GetFullPath(Application.ExecutablePath) + "\"";

        public static bool IsEnabled
        {
            get
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RunKey))
                    return key?.GetValue(ValueName) != null;
            }
        }

        /// <summary>
        /// Appelé à chaque lancement : inscrit l'application au démarrage
        /// (et met à jour le chemin de l'exe) sauf si l'utilisateur l'a désactivé.
        /// </summary>
        public static void ApplyPreference()
        {
            if (!File.Exists(DisabledFlag))
                SetEnabled(true);
        }

        public static void SetEnabled(bool enabled)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(RunKey))
            {
                if (enabled)
                    key.SetValue(ValueName, Command);
                else
                    key.DeleteValue(ValueName, false);
            }

            Directory.CreateDirectory(TaskStorage.DataFolder);
            if (enabled)
                File.Delete(DisabledFlag);
            else
                File.WriteAllText(DisabledFlag, "");
        }
    }
}
