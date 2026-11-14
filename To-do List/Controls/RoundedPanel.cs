using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace To_do_List
{
    /// <summary>
    /// Panneau à coins arrondis ; les coins prennent la couleur transparente de la fenêtre.
    /// </summary>
    public class RoundedPanel : Panel
    {
        private Color fillColor = Theme.Card;

        public RoundedPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            BackColor = Theme.TransparentKey;
        }

        [Category("Apparence")]
        public Color FillColor
        {
            get => fillColor;
            set { fillColor = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsHelper.FillRoundedRect(e.Graphics, ClientRectangle, Theme.Radius, fillColor);
        }
    }
}
