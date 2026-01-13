using System.Drawing;

namespace HenikenERP.Presentation.UI.Theme
{
    /// <summary>
    /// Typography system for the application
    /// Uses Segoe UI as primary font family
    /// </summary>
    public static class ThemeFonts
    {
        private static readonly string FontFamily = "Segoe UI";

        // Title Fonts
        public static readonly Font TitleLarge = new Font(FontFamily, 16F, FontStyle.Bold);
        public static readonly Font TitleMedium = new Font(FontFamily, 15F, FontStyle.Bold);
        public static readonly Font TitleSmall = new Font(FontFamily, 12F, FontStyle.Bold);

        // Body Fonts
        public static readonly Font BodyLarge = new Font(FontFamily, 11F, FontStyle.Regular);
        public static readonly Font BodyMedium = new Font(FontFamily, 10F, FontStyle.Regular);
        public static readonly Font BodySmall = new Font(FontFamily, 9F, FontStyle.Regular);

        // Caption Fonts
        public static readonly Font Caption = new Font(FontFamily, 8.5F, FontStyle.Regular);
        public static readonly Font CaptionBold = new Font(FontFamily, 8.5F, FontStyle.Bold);

        // Button Fonts
        public static readonly Font ButtonLarge = new Font(FontFamily, 10.5F, FontStyle.Regular);
        public static readonly Font ButtonMedium = new Font(FontFamily, 10F, FontStyle.Regular);

        // Label Fonts
        public static readonly Font Label = new Font(FontFamily, 10F, FontStyle.Regular);
        public static readonly Font LabelBold = new Font(FontFamily, 10F, FontStyle.Bold);

        // Menu Fonts
        public static readonly Font MenuItem = new Font(FontFamily, 10F, FontStyle.Regular);
    }
}

