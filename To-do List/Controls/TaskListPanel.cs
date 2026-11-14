using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace To_do_List
{
    /// <summary>
    /// Liste de tâches défilable (molette ou glisser de la barre), avec une barre de défilement fine
    /// dessinée à la main à la place de la barre Windows standard.
    /// </summary>
    public class TaskListPanel : Control, IMessageFilter
    {
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int ItemGap = 10;
        private const int ScrollBarWidth = 4;
        private const int ScrollBarMargin = 5;
        private const int ScrollStep = 48;
        private const int MinThumbHeight = 30;

        private readonly List<TaskItemControl> items = new List<TaskItemControl>();
        private int scrollOffset;
        private int contentHeight;
        private bool draggingThumb;
        private int dragStartY;
        private int dragStartOffset;
        private bool filterRegistered;

        public TaskListPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, false);
            BackColor = Theme.TransparentKey;
        }

        /// <summary>Déclenché quand une tâche est cochée/décochée ou supprimée.</summary>
        public event EventHandler TasksChanged;

        public IEnumerable<TaskItem> Tasks => items.Select(i => i.Task);

        private int MaxScroll => Math.Max(0, contentHeight - Height);

        private Rectangle ThumbRect
        {
            get
            {
                if (MaxScroll == 0)
                    return Rectangle.Empty;
                int height = Math.Max(MinThumbHeight, Height * Height / contentHeight);
                int y = (Height - height) * scrollOffset / MaxScroll;
                return new Rectangle(Width - ScrollBarWidth, y, ScrollBarWidth, height);
            }
        }

        public void AddTask(TaskItem task, bool atTop)
        {
            var item = new TaskItemControl(task);
            item.ToggleRequested += (s, e) =>
            {
                task.Done = !task.Done;
                item.Invalidate();
                TasksChanged?.Invoke(this, EventArgs.Empty);
            };
            // Différé : on ne détruit pas le contrôle pendant son propre gestionnaire de clic.
            item.DeleteRequested += (s, e) => BeginInvoke((Action)(() => RemoveItem(item)));

            if (atTop)
            {
                items.Insert(0, item);
                scrollOffset = 0;
            }
            else
            {
                items.Add(item);
            }
            Controls.Add(item);
            LayoutItems();
        }

        private void RemoveItem(TaskItemControl item)
        {
            items.Remove(item);
            Controls.Remove(item);
            item.Dispose();
            LayoutItems();
            TasksChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ScrollTo(int offset)
        {
            offset = Math.Max(0, Math.Min(offset, MaxScroll));
            if (offset == scrollOffset)
                return;
            scrollOffset = offset;
            LayoutItems();
        }

        private void LayoutItems()
        {
            contentHeight = items.Sum(i => i.Height) + Math.Max(0, items.Count - 1) * ItemGap;
            scrollOffset = Math.Max(0, Math.Min(scrollOffset, MaxScroll));

            int itemWidth = Width - ScrollBarWidth - ScrollBarMargin;
            int y = -scrollOffset;
            SuspendLayout();
            foreach (var item in items)
            {
                item.SetBounds(0, y, itemWidth, item.Height);
                y += item.Height + ItemGap;
            }
            ResumeLayout(false);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutItems();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            var thumb = ThumbRect;
            if (thumb.IsEmpty)
                return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsHelper.FillRoundedRect(e.Graphics, thumb, ScrollBarWidth / 2, Theme.ScrollThumb);
        }

        // --- Glisser la barre de défilement ---

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && ThumbRect.Contains(e.Location))
            {
                draggingThumb = true;
                dragStartY = e.Y;
                dragStartOffset = scrollOffset;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!draggingThumb)
                return;
            int track = Math.Max(1, Height - ThumbRect.Height);
            ScrollTo(dragStartOffset + (e.Y - dragStartY) * MaxScroll / track);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            draggingThumb = false;
        }

        // --- Molette : interceptée au niveau de l'application, quel que soit le contrôle sous le curseur ---

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode && !filterRegistered)
            {
                Application.AddMessageFilter(this);
                filterRegistered = true;
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (filterRegistered)
            {
                Application.RemoveMessageFilter(this);
                filterRegistered = false;
            }
            base.OnHandleDestroyed(e);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != WM_MOUSEWHEEL || !Visible || MaxScroll == 0)
                return false;
            if (!RectangleToScreen(ClientRectangle).Contains(Cursor.Position))
                return false;

            int delta = (short)((m.WParam.ToInt64() >> 16) & 0xFFFF);
            ScrollTo(scrollOffset - delta * ScrollStep / 120);
            return true;
        }
    }
}
