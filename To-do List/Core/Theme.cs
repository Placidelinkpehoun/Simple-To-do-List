using System.Drawing;

namespace To_do_List
{
    /// <summary>
    /// Couleurs, polices et dimensions communes de l'application (tirées de la maquette).
    /// </summary>
    internal static class Theme
    {
        /// <summary>
        /// Couleur rendue transparente par la fenêtre (Form.TransparencyKey).
        /// Volontairement très sombre : les pixels anticrénelés en bordure des formes
        /// se fondent vers le noir, ce qui donne une légère ombre au lieu d'un liseré coloré.
        /// </summary>
        public static readonly Color TransparentKey = Color.FromArgb(16, 16, 16);

        public static readonly Color Card = Color.FromArgb(43, 43, 43);
        public static readonly Color CardHover = Color.FromArgb(52, 52, 52);
        public static readonly Color Accent = Color.FromArgb(10, 99, 180);
        public static readonly Color AccentHover = Color.FromArgb(24, 116, 200);
        public static readonly Color AccentPressed = Color.FromArgb(8, 80, 148);
        public static readonly Color Text = Color.FromArgb(240, 240, 240);
        public static readonly Color TextDone = Color.FromArgb(115, 115, 115);
        public static readonly Color Placeholder = Color.FromArgb(150, 150, 150);
        public static readonly Color CircleIdle = Color.FromArgb(112, 112, 112);
        public static readonly Color CircleBorder = Color.FromArgb(28, 28, 28);
        public static readonly Color DeleteIcon = Color.FromArgb(205, 205, 205);
        public static readonly Color DeleteIconHover = Color.FromArgb(255, 105, 105);
        public static readonly Color DeleteBack = Color.FromArgb(38, 38, 38);
        public static readonly Color DeleteBackHover = Color.FromArgb(58, 58, 58);
        public static readonly Color ScrollThumb = Color.FromArgb(225, 225, 225);

        public const int Radius = 5;

        public static readonly Font TaskFont = new Font("Segoe UI", 17F, FontStyle.Regular, GraphicsUnit.Pixel);
        public static readonly Font TaskFontDone = new Font("Segoe UI", 17F, FontStyle.Strikeout, GraphicsUnit.Pixel);
    }
}
