using System.Drawing;

namespace HenikenERP.Presentation.UI.Theme
{
    /// <summary>
    /// Color palette for the application theme
    /// Primary color: #0D47A1 (Material Blue 900)
    /// </summary>
    public static class ThemeColors
    {
        // Primary Colors
        public static readonly Color Primary = Color.FromArgb(13, 71, 161);        // #0D47A1
        public static readonly Color PrimaryHover = Color.FromArgb(11, 61, 143);   // #0B3D8F
        public static readonly Color PrimaryPressed = Color.FromArgb(8, 48, 107);  // #08306B
        public static readonly Color PrimaryLight = Color.FromArgb(66, 165, 245);  // #42A5F5
        public static readonly Color PrimaryDark = Color.FromArgb(25, 118, 210);   // #1976D2

        // Background & Surface
        public static readonly Color Background = Color.FromArgb(246, 248, 251);   // #F6F8FB
        public static readonly Color Surface = Color.FromArgb(255, 255, 255);      // #FFFFFF
        public static readonly Color Card = Color.FromArgb(255, 255, 255);         // #FFFFFF

        // Borders & Dividers
        public static readonly Color Border = Color.FromArgb(227, 232, 239);       // #E3E8EF
        public static readonly Color Divider = Color.FromArgb(229, 231, 235);      // #E5E7EB

        // Text Colors
        public static readonly Color TextPrimary = Color.FromArgb(17, 24, 39);     // #111827
        public static readonly Color TextSecondary = Color.FromArgb(107, 114, 128); // #6B7280
        public static readonly Color TextTertiary = Color.FromArgb(156, 163, 175); // #9CA3AF
        public static readonly Color TextOnPrimary = Color.FromArgb(255, 255, 255); // #FFFFFF

        // Status Colors
        public static readonly Color Success = Color.FromArgb(34, 197, 94);        // #22C55E
        public static readonly Color Warning = Color.FromArgb(251, 191, 36);       // #FBBF24
        public static readonly Color Danger = Color.FromArgb(239, 68, 68);         // #EF4444
        public static readonly Color Info = Color.FromArgb(59, 130, 246);          // #3B82F6

        // DataGridView
        public static readonly Color GridHeaderBg = Color.FromArgb(249, 250, 251); // #F9FAFB
        public static readonly Color GridHeaderText = Color.FromArgb(17, 24, 39);  // #111827
        public static readonly Color GridRowAlternate = Color.FromArgb(249, 250, 251); // #F9FAFB
        public static readonly Color GridSelection = Color.FromArgb(219, 234, 254); // #DBEAFE
        public static readonly Color GridLine = Color.FromArgb(229, 231, 235);     // #E5E7EB
    }
}

