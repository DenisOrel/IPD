
// Type: Intermech.Client.Core.Organizer.NativeMethods
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Client.Core.Organizer;

public class NativeMethods
{
  public const int AW_HIDE = 65536 /*0x010000*/;
  public const int AW_ACTIVATE = 131072 /*0x020000*/;
  public const int AW_HOR_POSITIVE = 1;
  public const int AW_HOR_NEGATIVE = 2;
  public const int AW_SLIDE = 262144 /*0x040000*/;
  public const int AW_BLEND = 524288 /*0x080000*/;
  public const int MA_NOACTIVATE = 3;
  public const int WS_EX_NOACTIVATE = 134217728 /*0x08000000*/;
  public const int WS_EX_TOOLWINDOW = 128 /*0x80*/;
  public const int WM_ACTIVATE = 6;
  public const int WM_ACTIVATEAPP = 28;
  public const int WM_GETMINMAXINFO = 36;
  public const int WM_NCHITTEST = 132;
  public const int WM_NCACTIVATE = 134;
  public const int WM_MOUSEACTIVATE = 33;
  public const int WM_CAPTURECHANGED = 533;
  public const int WM_LBUTTONDOWN = 513;
  public const int WM_LBUTTONUP = 514;
  public const int WM_RBUTTONDOWN = 516;
  public const int WM_MBUTTONDOWN = 519;
  public const int WM_NCLBUTTONDOWN = 161;
  public const int WM_NCRBUTTONDOWN = 164;
  public const int WM_NCMBUTTONDOWN = 167;
  public const int KEYEVENTF_KEYUP = 2;
  public const int HTTRANSPARENT = -1;
  public const int HTLEFT = 10;
  public const int HTRIGHT = 11;
  public const int HTTOP = 12;
  public const int HTTOPLEFT = 13;
  public const int HTTOPRIGHT = 14;
  public const int HTBOTTOM = 15;
  public const int HTBOTTOMLEFT = 16 /*0x10*/;
  public const int HTBOTTOMRIGHT = 17;
  public const int CS_DROPSHADOW = 131072 /*0x020000*/;

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int AnimateWindow(IntPtr hwand, int dwTime, int dwFlags);

  [DllImport("user32", CharSet = CharSet.Auto)]
  public static extern int SendMessage(IntPtr handle, int msg, int wParam, IntPtr lParam);

  [DllImport("user32", CharSet = CharSet.Auto)]
  public static extern int PostMessage(IntPtr handle, int msg, int wParam, IntPtr lParam);

  [DllImport("user32")]
  public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

  public static int HiWord(int n) => n >> 16 /*0x10*/ & (int) ushort.MaxValue;

  public static int HiWord(IntPtr n) => NativeMethods.HiWord((int) (long) n);

  public static int LoWord(int n) => n & (int) ushort.MaxValue;

  public static int LoWord(IntPtr n) => NativeMethods.LoWord((int) (long) n);

  public enum SHOWWINDOW : uint
  {
    SW_HIDE = 0,
    SW_NORMAL = 1,
    SW_SHOWNORMAL = 1,
    SW_SHOWMINIMIZED = 2,
    SW_MAXIMIZE = 3,
    SW_SHOWMAXIMIZED = 3,
    SW_SHOWNOACTIVATE = 4,
    SW_SHOW = 5,
    SW_MINIMIZE = 6,
    SW_SHOWMINNOACTIVE = 7,
    SW_SHOWNA = 8,
    SW_RESTORE = 9,
    SW_SHOWDEFAULT = 10, // 0x0000000A
    SW_FORCEMINIMIZE = 11, // 0x0000000B
    SW_MAX = 11, // 0x0000000B
  }

  public struct MINMAXINFO
  {
    public Point reserved;
    public Size maxSize;
    public Point maxPosition;
    public Size minTrackSize;
    public Size maxTrackSize;
  }
}
