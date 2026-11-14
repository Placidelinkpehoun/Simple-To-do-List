using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace To_do_List
{
    /// <summary>
    /// TextBox qui affiche un texte indicatif en italique tant qu'elle est vide.
    /// </summary>
    public class PlaceholderTextBox : TextBox
    {
        private const int WM_PAINT = 0x000F;

        private string placeholder = "";
        private Font placeholderFont;

        [Category("Apparence")]
        public string Placeholder
        {
            get => placeholder;
            set { placeholder = value ?? ""; Invalidate(); }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT && TextLength == 0 && placeholder.Length > 0)
            {
                if (placeholderFont == null)
                    placeholderFont = new Font(Font, FontStyle.Italic);

                using (var g = Graphics.FromHwnd(Handle))
                {
                    var bounds = new Rectangle(1, 0, ClientSize.Width - 1, ClientSize.Height);
                    TextRenderer.DrawText(g, placeholder, placeholderFont, bounds, Theme.Placeholder, BackColor,
                        TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.NoPrefix |
                        TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
                }
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            placeholderFont?.Dispose();
            placeholderFont = null;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                placeholderFont?.Dispose();
            base.Dispose(disposing);
        }
    }
}
