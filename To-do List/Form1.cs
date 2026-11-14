using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace To_do_List
{
    public partial class Form1 : Form
    {
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        public Form1()
        {
            InitializeComponent();

            Icon = AppIcon.Create();
            notifyIcon.Icon = Icon;

            foreach (var task in TaskStorage.Load())
                taskList.AddTask(task, atTop: false);
        }

        /// <summary>
        /// Fenêtre « outil » : n'apparaît ni dans la barre des tâches ni dans Alt+Tab.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        private void AddTaskFromInput()
        {
            string text = TaskStorage.Sanitize(txtTask.Text);
            if (text.Length == 0)
                return;

            taskList.AddTask(new TaskItem { Text = text }, atTop: true);
            txtTask.Clear();
            SaveTasks();
        }

        private void SaveTasks()
        {
            TaskStorage.Save(taskList.Tasks);
        }

        private void ShowWindow()
        {
            Show();
            WindowState = FormWindowState.Normal;
            CenterToScreen();
            Activate();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            AddTaskFromInput();
        }

        private void txtTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                AddTaskFromInput();
            }
        }

        private void inputPanel_Click(object sender, EventArgs e)
        {
            txtTask.Focus();
        }

        private void taskList_TasksChanged(object sender, EventArgs e)
        {
            SaveTasks();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ShowWindow();
        }

        private void trayMenu_Opening(object sender, CancelEventArgs e)
        {
            menuStartup.Checked = StartupManager.IsEnabled;
        }

        private void menuShow_Click(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void menuStartup_Click(object sender, EventArgs e)
        {
            StartupManager.SetEnabled(menuStartup.Checked);
        }

        private void menuQuit_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
