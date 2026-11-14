using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace To_do_List
{
    /// <summary>
    /// Une ligne de la liste : carte sombre (pastille d'état + texte) et croix de suppression à droite.
    /// Un clic sur la carte coche/décoche la tâche, un clic sur la croix la supprime.
    /// </summary>
    public class TaskItemControl : Control
    {
        public const int CardWidth = 553;

        private const int MinHeight = 44;
        private const int PaddingV = 11;
        private const int CircleCenterX = 21;
        private const int CircleRadius = 7;
        private const int TextLeft = 46;
        private const int TextRight = 16;
        private const int DeleteSize = 14;
        private const int DeleteHitSize = 30;

        private const TextFormatFlags TextFlags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl |
                                                  TextFormatFlags.NoPrefix | TextFormatFlags.Left | TextFormatFlags.Top;

        private readonly int textHeight;
        private bool hovered;
        private bool deleteHovered;

        public TaskItemControl(TaskItem task)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable | ControlStyles.StandardDoubleClick, false);
            BackColor = Theme.TransparentKey;

            Task = task;
            textHeight = TextRenderer.MeasureText(task.Text, Theme.TaskFont,
                new Size(TextWidth, int.MaxValue), TextFlags).Height;
            Height = Math.Max(MinHeight, textHeight + 2 * PaddingV);
        }

        public TaskItem Task { get; }

        public event EventHandler ToggleRequested;
        public event EventHandler DeleteRequested;

        private static int TextWidth => CardWidth - TextLeft - TextRight;

        private Rectangle CardRect => new Rectangle(0, 0, CardWidth, Height);

        private Point DeleteCenter => new Point(CardWidth + (Width - CardWidth) / 2, Height / 2);

        private Rectangle DeleteHitRect
        {
            get
            {
                var c = DeleteCenter;
                return new Rectangle(c.X - DeleteHitSize / 2, c.Y - DeleteHitSize / 2, DeleteHitSize, DeleteHitSize);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var cardColor = hovered && !deleteHovered ? Theme.CardHover : Theme.Card;
            GraphicsHelper.FillRoundedRect(g, CardRect, Theme.Radius, cardColor);

            var circle = new Rectangle(CircleCenterX - CircleRadius, Height / 2 - CircleRadius,
                                       CircleRadius * 2, CircleRadius * 2);
            using (var brush = new SolidBrush(Task.Done ? Theme.Accent : Theme.CircleIdle))
                g.FillEllipse(brush, circle);
            using (var pen = new Pen(Theme.CircleBorder, 1.5f))
                g.DrawEllipse(pen, circle);

            var textRect = new Rectangle(TextLeft, (Height - textHeight) / 2, TextWidth, textHeight);
            TextRenderer.DrawText(g, Task.Text, Task.Done ? Theme.TaskFontDone : Theme.TaskFont, textRect,
                Task.Done ? Theme.TextDone : Theme.Text, cardColor, TextFlags);

            // Les pixels de la couleur transparente laissent passer les clics vers le bureau :
            // au survol de la ligne on dessine un disque derrière la croix pour qu'elle soit facile à cliquer.
            if (hovered)
            {
                using (var brush = new SolidBrush(deleteHovered ? Theme.DeleteBackHover : Theme.DeleteBack))
                    g.FillEllipse(brush, DeleteHitRect);
            }

            var center = DeleteCenter;
            int half = DeleteSize / 2;
            using (var pen = new Pen(deleteHovered ? Theme.DeleteIconHover : Theme.DeleteIcon, 2f))
            {
                g.DrawLine(pen, center.X - half, center.Y - half, center.X + half, center.Y + half);
                g.DrawLine(pen, center.X - half, center.Y + half, center.X + half, center.Y - half);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool overDelete = DeleteHitRect.Contains(e.Location);
            Cursor = overDelete || CardRect.Contains(e.Location) ? Cursors.Hand : Cursors.Default;
            SetHover(true, overDelete);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SetHover(false, false);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.Left)
                return;

            if (DeleteHitRect.Contains(e.Location))
                DeleteRequested?.Invoke(this, EventArgs.Empty);
            else if (CardRect.Contains(e.Location))
                ToggleRequested?.Invoke(this, EventArgs.Empty);
        }

        private void SetHover(bool row, bool delete)
        {
            if (row == hovered && delete == deleteHovered)
                return;
            hovered = row;
            deleteHovered = delete;
            Invalidate();
        }
    }
}
