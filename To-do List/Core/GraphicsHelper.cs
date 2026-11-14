using System.Drawing;
using System.Drawing.Drawing2D;

namespace To_do_List
{
    internal static class GraphicsHelper
    {
        /// <summary>
        /// Construit un rectangle aux coins arrondis contenu entièrement dans <paramref name="bounds"/>.
        /// </summary>
        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d - 1, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d - 1, bounds.Bottom - d - 1, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d - 1, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void FillRoundedRect(Graphics g, Rectangle bounds, int radius, Color color)
        {
            using (var path = RoundedRect(bounds, radius))
            using (var brush = new SolidBrush(color))
            {
                g.FillPath(brush, path);
            }
        }
    }
}
