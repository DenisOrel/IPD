
// Type: SuperTooltips.Win32API
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Runtime.InteropServices;


namespace SuperTooltips
{
    internal class Win32API
    {
      public static bool ShowShadow = true;
      public static bool AlphaShadow = true;

      [DllImport("user32")]
      public static extern IntPtr GetActiveWindow();

      [DllImport("user32")]
      public static extern bool TrackMouseEvent(ref Win32API.TRACKMOUSEEVENT val);

      [DllImport("user32")]
      public static extern bool SetWindowPos(
        int a52T,
        int a52U,
        int a52V,
        int a52W,
        int a52X,
        int a52Y,
        int a52Z);

      [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern int AnimateWindow(int a54N, int a54O, int a54P);

      [DllImport("User32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr GetDC(IntPtr a53O);

      [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr CreateCompatibleDC(IntPtr a53U);

      [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr SelectObject(IntPtr a53S, IntPtr a53T);

      [DllImport("User32.dll", CharSet = CharSet.Auto)]
      public static extern bool UpdateLayeredWindow(
        IntPtr a549,
        IntPtr a54A,
        ref Win32API.POINT a54B,
        ref Win32API.SIZE a54C,
        IntPtr a54D,
        ref Win32API.POINT a54E,
        int a54F,
        ref Win32API.BLENDFUNCTION a54G,
        int a54H);

      [DllImport("User32.dll", CharSet = CharSet.Auto)]
      public static extern int ReleaseDC(IntPtr a53P, IntPtr a53Q);

      [DllImport("gdi32")]
      public static extern bool DeleteObject(IntPtr a53F);

      [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
      public static extern bool DeleteDC(IntPtr a53R);

      [DllImport("user32.dll")]
      public static extern IntPtr GetFocus();

      [DllImport("user32.dll")]
      public static extern IntPtr SetFocus(IntPtr a51X);

      public struct TRACKMOUSEEVENT
      {
        public int cbSize;
        public uint wsFlags;
        public int hwndTrack;
        public int dwHoverTime;
      }

      public struct SIZE(int x, int y)
      {
        public int cx = x;
        public int cy = y;
      }

      public struct POINT(int ax, int ay)
      {
        public int x = ax;
        public int y = ay;
      }

      [StructLayout(LayoutKind.Sequential, Pack = 1)]
      public struct BLENDFUNCTION
      {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
      }
    }
}
