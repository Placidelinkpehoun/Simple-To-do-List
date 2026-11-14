namespace To_do_List
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.inputPanel = new To_do_List.RoundedPanel();
            this.txtTask = new To_do_List.PlaceholderTextBox();
            this.btnCreate = new To_do_List.RoundedButton();
            this.taskList = new To_do_List.TaskListPanel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.inputPanel.SuspendLayout();
            this.trayMenu.SuspendLayout();
            this.SuspendLayout();
            //
            // inputPanel
            //
            this.inputPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.inputPanel.Controls.Add(this.txtTask);
            this.inputPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.inputPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.inputPanel.Location = new System.Drawing.Point(21, 21);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(399, 56);
            this.inputPanel.TabIndex = 0;
            this.inputPanel.Click += new System.EventHandler(this.inputPanel_Click);
            //
            // txtTask
            //
            this.txtTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.txtTask.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTask.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTask.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtTask.Location = new System.Drawing.Point(17, 16);
            this.txtTask.MaxLength = 200;
            this.txtTask.Name = "txtTask";
            this.txtTask.Placeholder = "Faire un push sur github...";
            this.txtTask.Size = new System.Drawing.Size(366, 23);
            this.txtTask.TabIndex = 0;
            this.txtTask.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTask_KeyDown);
            //
            // btnCreate
            //
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.btnCreate.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(430, 21);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(181, 56);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Créer";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            //
            // taskList
            //
            this.taskList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.taskList.Location = new System.Drawing.Point(21, 102);
            this.taskList.Name = "taskList";
            this.taskList.Size = new System.Drawing.Size(597, 327);
            this.taskList.TabIndex = 2;
            this.taskList.TasksChanged += new System.EventHandler(this.taskList_TasksChanged);
            //
            // notifyIcon
            //
            this.notifyIcon.ContextMenuStrip = this.trayMenu;
            this.notifyIcon.Text = "To-do List";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            //
            // trayMenu
            //
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuShow,
            this.menuStartup,
            this.menuSeparator,
            this.menuQuit});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(250, 76);
            this.trayMenu.Opening += new System.ComponentModel.CancelEventHandler(this.trayMenu_Opening);
            //
            // menuShow
            //
            this.menuShow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuShow.Name = "menuShow";
            this.menuShow.Size = new System.Drawing.Size(249, 22);
            this.menuShow.Text = "Afficher";
            this.menuShow.Click += new System.EventHandler(this.menuShow_Click);
            //
            // menuStartup
            //
            this.menuStartup.CheckOnClick = true;
            this.menuStartup.Name = "menuStartup";
            this.menuStartup.Size = new System.Drawing.Size(249, 22);
            this.menuStartup.Text = "Lancer au démarrage de Windows";
            this.menuStartup.Click += new System.EventHandler(this.menuStartup_Click);
            //
            // menuSeparator
            //
            this.menuSeparator.Name = "menuSeparator";
            this.menuSeparator.Size = new System.Drawing.Size(246, 6);
            //
            // menuQuit
            //
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.Size = new System.Drawing.Size(249, 22);
            this.menuQuit.Text = "Quitter";
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            //
            // Form1
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(630, 450);
            this.Controls.Add(this.taskList);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.inputPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "To-do List";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.trayMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPanel inputPanel;
        private PlaceholderTextBox txtTask;
        private RoundedButton btnCreate;
        private TaskListPanel taskList;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem menuShow;
        private System.Windows.Forms.ToolStripMenuItem menuStartup;
        private System.Windows.Forms.ToolStripSeparator menuSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuQuit;
    }
}
