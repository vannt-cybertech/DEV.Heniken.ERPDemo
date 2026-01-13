using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace HenikenERP.Presentation.UI.Theme
{
    public static class BrandingAssets
    {
        public static Bitmap TryGetAppLogo()
        {
            try
            {
                // Note: This is intentionally NOT strongly-typed to avoid needing a regenerated Resources.Designer.cs.
                return HenikenERP.Presentation.Properties.Resources.ResourceManager.GetObject("AppLogo") as Bitmap;
            }
            catch
            {
                return null;
            }
        }

        public static Icon TryGetAppIcon(int size = 32)
        {
            var logo = TryGetAppLogo();
            if (logo == null) return null;

            Bitmap resized = null;
            try
            {
                resized = new Bitmap(logo, new Size(size, size));
                return CreateIconFromBitmap(resized);
            }
            finally
            {
                resized?.Dispose();
            }
        }

        private static Icon CreateIconFromBitmap(Bitmap bitmap)
        {
            IntPtr hIcon = IntPtr.Zero;
            try
            {
                hIcon = bitmap.GetHicon();
                using (var tmp = Icon.FromHandle(hIcon))
                {
                    // Clone so we can safely destroy the native handle afterwards.
                    return (Icon)tmp.Clone();
                }
            }
            finally
            {
                if (hIcon != IntPtr.Zero)
                {
                    DestroyIcon(hIcon);
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr handle);
    }
}


