// Decompiled with JetBrains decompiler
// Type: Intermech.WinApi
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech;

public class WinApi
{
  public const int HWND_BOTTOM = 1;
  public const uint SWP_NOSIZE = 1;
  public const uint SWP_NOMOVE = 2;
  public const uint SWP_NOREDRAW = 8;
  public const uint SWP_NOACTIVATE = 16 /*0x10*/;
  public const uint SWP_SHOWWINDOW = 64 /*0x40*/;
  public const uint SWP_HIDEWINDOW = 128 /*0x80*/;
  public const uint SWP_NOOWNERZORDER = 512 /*0x0200*/;
  public const uint SWP_NOREPOSITION = 512 /*0x0200*/;
  public const uint BrowserHideFlags = 667;
  public const uint BrowserShowFlags = 603;
  public const int WM_MOUSEWHEEL = 522;

  [DllImport("user32.dll")]
  public static extern bool SetWindowPos(
    int hWnd,
    int hWndInsertAfter,
    int X,
    int Y,
    int cx,
    int cy,
    uint uFlags);

  [DllImport("User32.dll")]
  public static extern IntPtr SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);

  [DllImport("user32.dll")]
  public static extern IntPtr GetForegroundWindow();
}
