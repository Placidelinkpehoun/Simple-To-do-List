using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace To_do_List
{
    /// <summary>
    /// Icône de l'application (carré bleu avec une coche), dessinée en code.
    /// </summary>
    internal static class AppIcon
    {
        [DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr handle);

        public static Icon Create()
        {
            using (var bmp = new Bitmap(32, 32))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(Color.Transparent);
                    GraphicsHelper.FillRoundedRect(g, new Rectangle(1, 1, 30, 30), 7, Theme.Accent);
                    using (var pen = new Pen(Color.White, 3.5f))
                    {
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        pen.LineJoin = LineJoin.Round;
                        g.DrawLines(pen, new[] { new Point(9, 16), new Point(14, 21), new Point(23, 11) });
                    }
                }

                IntPtr handle = bmp.GetHicon();
                var icon = (Icon)Icon.FromHandle(handle).Clone();
                DestroyIcon(handle);
                return icon;
            }
        }
    }
}
