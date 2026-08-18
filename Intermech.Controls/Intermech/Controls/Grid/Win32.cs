
// Type: Intermech.Controls.Grid.Win32
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.Grid;

/// <summary>Summary description for Win32.</summary>
internal class Win32
{
  public const int SWP_NOSIZE = 1;
  public const int SWP_NOMOVE = 2;
  public const int SWP_NOZORDER = 4;
  public const int SWP_NOACTIVATE = 16 /*0x10*/;
  public const int SWP_FRAMECHANGED = 32 /*0x20*/;
  public const int SWP_DRAWFRAME = 32 /*0x20*/;
  public const int SWP_SHOWWINDOW = 64 /*0x40*/;
  public const int SWP_HIDEWINDOW = 128 /*0x80*/;
  public const int SWP_NOCOPYBITS = 256 /*0x0100*/;
  public const int SWP_NOOWNERZORDER = 512 /*0x0200*/;
  public const int SWP_NOREPOSITION = 512 /*0x0200*/;
  public const int SWP_NOSENDCHANGING = 1024 /*0x0400*/;
  public const int SWP_DEFERERASE = 8192 /*0x2000*/;
  public const int SWP_ASYNCWINDOWPOS = 16384 /*0x4000*/;
  public static HandleRef HWND_TOP = new HandleRef((object) null, (IntPtr) 0);
  public static HandleRef HWND_BOTTOM = new HandleRef((object) null, (IntPtr) 1);
  public static HandleRef HWND_TOPMOST = new HandleRef((object) null, new IntPtr(-1));
  public static HandleRef HWND_NOTOPMOST = new HandleRef((object) null, new IntPtr(-2));
  public static HandleRef HWND_NULL = new HandleRef((object) null, (IntPtr) 0);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr BeginDeferWindowPos(int nNumWindows);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool EndDeferWindowPos(IntPtr hWinPosInfo);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr DeferWindowPos(
    IntPtr hWinPosInfo,
    HandleRef hwnd,
    HandleRef hwndInsertAfter,
    int x,
    int y,
    int cx,
    int cy,
    uint uFlags);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetWindowPos(
    HandleRef hwnd,
    HandleRef hwndInsertAfter,
    int x,
    int y,
    int cx,
    int cy,
    uint uFlags);
}
