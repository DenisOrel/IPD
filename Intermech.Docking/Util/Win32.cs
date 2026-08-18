
// Type: Intermech.Util.Win32
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Util;

internal class Win32
{
  public const int _a = 4114;
  public const int aa = 256 /*0x0100*/;
  public const int ab = 260;
  public const int ac = 258;
  public const int ad = 33;
  public const int ae = 78;
  public const int af = 273;
  public const int ag = 533;
  public const int WM_MDISETMENU = 560;
  public const int ai = 2;
  public const int WM_CONTEXTMENU = 123;
  public const int ak = 32 /*0x20*/;
  public const int al = 3;
  public const int am = 4;
  public const int an = 61696;
  public const int ao = -2147483648 /*0x80000000*/;
  public const int ap = 1073741824 /*0x40000000*/;
  public const int aq = 8;
  public const int ar = 38;
  public const int @as = 40;
  public const int at = 37;
  public const int au = 39;
  public const int av = 27;
  public const int aw = 18;
  public const int _b = 106;
  public const int _c = 4098;
  public const int _d = 16 /*0x10*/;
  public const int e = 64 /*0x40*/;
  public const int f = 1;
  public const int g = 2;
  public const int h = 4;
  public const int i = 4132;
  public const int j = 15;
  public const int k = 274;
  public const int _l = 161;
  public const int m = 164;
  public const int n = 160 /*0xA0*/;
  public const int o = 512 /*0x0200*/;
  public const int p = 521;
  public const int q = 256 /*0x0100*/;
  public const int r = 264;
  public const int s = 513;
  public const int t = 516;
  public const int u = 515;
  public const int v = 518;
  public const int w = 512 /*0x0200*/;
  public const int x = 275;
  public const int y = 514;
  public const int z = 517;

  internal static bool IsWin2K()
  {
    bool flag = false;
    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
      flag = Environment.OSVersion.Version >= new Version(5, 0, 0, 0);
    return flag;
  }

  internal static bool IsXP()
  {
    bool flag = false;
    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
      flag = Environment.OSVersion.Version >= new Version(5, 1, 0, 0);
    return flag;
  }

  internal static Color GradientActiveCaption() => ColorTranslator.FromWin32(Win32.GetSysColor(27));

  public static int LoWorld(int value) => (int) (short) (value & (int) ushort.MaxValue);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern int GetSysColor(int A_0);

  [DllImport("user32.dll")]
  public static extern bool AnimateWindow(IntPtr A_0, int A_1, Win32.ANIMATE_FLAGS A_2);

  public static int HiWord(int value)
  {
    return (int) (short) (value >> 16 /*0x10*/ & (int) ushort.MaxValue);
  }

  [DllImport("user32.dll")]
  public static extern int ClientToScreen(IntPtr A_0, out Win32.POINT A_1);

  [DllImport("user32.dll", CharSet = CharSet.Ansi)]
  public static extern IntPtr DispatchMessageA(ref Win32.MSG A_0);

  [DllImport("user32.dll")]
  public static extern IntPtr GetForegroundWindow();

  [DllImport("user32.dll", CharSet = CharSet.Ansi)]
  public static extern int GetMessageA(out Win32.MSG A_0, IntPtr A_1, int A_2, int A_3);

  [DllImport("user32.dll")]
  public static extern bool HideCaret(IntPtr A_0);

  [DllImport("user32.dll")]
  public static extern bool KillTimer(IntPtr A_0, int A_1);

  [DllImport("user32.dll")]
  public static extern bool ReleaseCapture();

  [DllImport("user32.dll")]
  public static extern IntPtr SendMessage(IntPtr A_0, int A_1, int A_2, int A_3);

  [DllImport("user32.dll")]
  public static extern IntPtr SetParent(IntPtr A_0, IntPtr A_1);

  [DllImport("user32.dll")]
  public static extern IntPtr SetTimer(IntPtr A_0, int A_1, int A_2, Win32.TIMERPROC A_3);

  [DllImport("user32.dll")]
  public static extern bool SetWindowPos(
    IntPtr A_0,
    int A_1,
    int A_2,
    int A_3,
    int A_4,
    int A_5,
    int A_6);

  [DllImport("user32.dll")]
  public static extern bool ShowCaret(IntPtr A_0);

  [DllImport("user32.dll")]
  public static extern bool ShowWindow(IntPtr A_0, int A_1);

  [DllImport("user32.dll")]
  public static extern bool SystemParametersInfo(int A_0, int A_1, ref int A_2, int A_3);

  [DllImport("user32.dll")]
  public static extern bool TranslateMessage(out Win32.MSG A_0);

  [DllImport("user32.dll")]
  public static extern IntPtr GetDesktopWindow();

  [DllImport("gdi32.dll")]
  public static extern int GetPixel(IntPtr A_0, int A_1, int A_2);

  [DllImport("user32.dll")]
  public static extern IntPtr GetWindowDC(IntPtr A_0);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetWindowRect(IntPtr A_0, out Win32.RECT A_1);

  [DllImport("user32.dll", EntryPoint = "ReleaseDC", CharSet = CharSet.Auto)]
  public static extern int IntReleaseDC(IntPtr A_0, IntPtr A_1);

  [DllImport("gdi32.dll")]
  internal static extern IntPtr CreateBitmap(int A_0, int A_1, int A_2, int A_3, short[] A_4);

  [DllImport("gdi32.dll")]
  internal static extern IntPtr CreateBrushIndirect(Win32.LOGBRUSH A_0);

  [DllImport("gdi32.dll")]
  internal static extern bool DeleteObject(IntPtr A_0);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  internal static extern IntPtr GetDC(IntPtr A_0);

  [DllImport("gdi32.dll")]
  internal static extern bool PatBlt(HandleRef A_0, int A_1, int A_2, int A_3, int A_4, int A_5);

  [DllImport("user32.dll")]
  internal static extern int ReleaseDC(IntPtr p1, IntPtr p2);

  [DllImport("gdi32.dll")]
  internal static extern IntPtr SelectObject(IntPtr A_0, IntPtr A_1);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  internal static extern bool SetLayeredWindowAttributes(IntPtr A_0, int A_1, byte A_2, int A_3);

  [DllImport("gdi32.dll", SetLastError = true)]
  internal static extern bool DeleteDC(IntPtr A_0);

  [DllImport("gdi32.dll", SetLastError = true)]
  internal static extern IntPtr CreateCompatibleDC(IntPtr A_0);

  [DllImport("user32.dll", SetLastError = true)]
  internal static extern bool UpdateLayeredWindow(
    IntPtr A_0,
    IntPtr A_1,
    ref Win32.POINT A_2,
    ref Win32.SIZE A_3,
    IntPtr A_4,
    ref Win32.POINT A_5,
    int A_6,
    ref Win32.BLENDFUNCTION A_7,
    int A_8);

  public struct MSG
  {
    public IntPtr hwnd;
    public int message;
    public IntPtr wParam;
    public IntPtr lParam;
    public int time;
    public int pointX;
    public int pointY;
  }

  public struct POINT
  {
    public int X;
    public int Y;

    internal POINT(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }
  }

  internal struct SIZE
  {
    internal int _cx;
    internal int _cy;

    internal SIZE(int cx, int cy)
    {
      this._cx = cx;
      this._cy = cy;
    }
  }

  public struct RECT
  {
    public int _left;
    public int _top;
    public int _right;
    public int _bottom;
  }

  [StructLayout(LayoutKind.Sequential)]
  internal class LOGBRUSH
  {
    internal int _style;
    internal int _color;
    internal IntPtr _hatch;

    internal LOGBRUSH()
    {
    }
  }

  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  internal struct BLENDFUNCTION
  {
    internal byte _blendOp;
    internal byte _blendFlags;
    internal byte _sourceConstantAlpha;
    internal byte _alphaFormat;
  }

  public delegate void TIMERPROC(IntPtr A_0, int A_1, IntPtr A_2, IntPtr A_3);

  public enum ANIMATE_FLAGS : uint
  {
    AW_HOR_POSITIVE = 1,
    AW_HOR_NEGATIVE = 2,
    AW_VER_POSITIVE = 4,
    AW_VER_NEGATIVE = 8,
    AW_CENTER = 16, // 0x00000010
    AW_HIDE = 65536, // 0x00010000
    AW_ACTIVATE = 131072, // 0x00020000
    AW_SLIDE = 262144, // 0x00040000
    AW_BLEND = 524288, // 0x00080000
  }
}
