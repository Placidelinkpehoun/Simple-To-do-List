using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace To_do_List
{
    /// <summary>
    /// Sauvegarde les tâches dans %AppData%\To-do List\tasks.txt
    /// (une tâche par ligne : "0" ou "1" pour l'état, une tabulation, puis le texte).
    /// </summary>
    internal static class TaskStorage
    {
        public static readonly string DataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "To-do List");

        private static readonly string FilePath = Path.Combine(DataFolder, "tasks.txt");

        public static List<TaskItem> Load()
        {
            var tasks = new List<TaskItem>();
            if (!File.Exists(FilePath))
                return tasks;

            foreach (var line in File.ReadAllLines(FilePath, Encoding.UTF8))
            {
                if (line.Length < 2 || line[1] != '\t')
                    continue;
                tasks.Add(new TaskItem { Done = line[0] == '1', Text = line.Substring(2) });
            }
            return tasks;
        }

        public static void Save(IEnumerable<TaskItem> tasks)
        {
            Directory.CreateDirectory(DataFolder);
            var lines = tasks.Select(t => (t.Done ? "1" : "0") + "\t" + Sanitize(t.Text));
            File.WriteAllLines(FilePath, lines, Encoding.UTF8);
        }

        public static string Sanitize(string text)
        {
            return text.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Trim();
        }
    }
}
