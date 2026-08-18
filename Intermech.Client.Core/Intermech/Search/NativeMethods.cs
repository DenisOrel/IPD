
// Type: Intermech.Search.NativeMethods
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.Search;

public static class NativeMethods
{
  public const int NULL = 0;
  public const int WM_CLOSE = 16 /*0x10*/;
  public const int WM_LBUTTONDOWN = 513;
  public const int WM_LBUTTONUP = 514;
  public const int MK_LBUTTON = 1;
  public const int WM_MOUSEHOVER = 673;
  public const int WM_MOUSELEAVE = 675;

  [DllImport("user32.dll", SetLastError = true)]
  public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern IntPtr FindWindowEx(
    IntPtr hwndParent,
    IntPtr hwndChildAfter,
    string lpszClass,
    string lpszWindow);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

  [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
  public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern int GetWindowTextLength(IntPtr hWnd);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern bool EnumChildWindows(
    IntPtr hWndParent,
    NativeMethods.EnumChildProc lpEnumFunc,
    int lParam);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern IntPtr GetTopWindow(IntPtr hwnd);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern IntPtr GetDesktopWindow();

  [DllImport("user32.dll", SetLastError = true)]
  public static extern int GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

  public static void PerformClick(IntPtr handle, short x, short y)
  {
    if (handle == IntPtr.Zero)
      throw new ArgumentException();
    NativeMethods.SendMessage(new HandleRef((object) null, handle), 513U, new IntPtr(1), NativeMethods.CreateLParamForMouseEvent(x, y));
    NativeMethods.SendMessage(new HandleRef((object) null, handle), 514U, new IntPtr(1), NativeMethods.CreateLParamForMouseEvent(x, y));
  }

  public static IntPtr CreateLParamForMouseEvent(short x, short y)
  {
    return (IntPtr) ((int) y << 16 /*0x10*/ | (int) x);
  }

  public static IntPtr[] FindAllWindowHandlesForCurrentProcess(string windowClassName)
  {
    if (string.IsNullOrEmpty(windowClassName))
      throw new ArgumentException();
    List<IntPtr> source = new List<IntPtr>();
    int id = Process.GetCurrentProcess().Id;
    foreach (IntPtr childrenWindowHandle in NativeMethods.FindAllChildrenWindowHandles(IntPtr.Zero, windowClassName))
    {
      uint lpdwProcessId;
      NativeMethods.GetWindowThreadProcessId(childrenWindowHandle, out lpdwProcessId);
      if ((long) id == (long) lpdwProcessId)
        source.Add(childrenWindowHandle);
    }
    source.AddRange((IEnumerable<IntPtr>) NativeMethods.FindAllChildrenWindowHandles(Process.GetCurrentProcess().MainWindowHandle, windowClassName));
    return source.Distinct<IntPtr>().ToArray<IntPtr>();
  }

  public static IntPtr[] FindAllChildrenWindowHandles(
    IntPtr windowHandle,
    string childWindowClassName)
  {
    List<IntPtr> numList = new List<IntPtr>();
    IntPtr hwndChildAfter = IntPtr.Zero;
    while (true)
    {
      hwndChildAfter = NativeMethods.FindWindowEx(windowHandle, hwndChildAfter, childWindowClassName, (string) null);
      if (!(hwndChildAfter == IntPtr.Zero))
        numList.Add(hwndChildAfter);
      else
        break;
    }
    return numList.ToArray();
  }

  [DllImport("Oleacc.dll", SetLastError = true)]
  public static extern int GetProcessHandleFromHwnd(int hwnd);

  [DllImport("Kernel32.dll", SetLastError = true)]
  public static extern bool TerminateProcess(int handle, uint exitCode);

  public delegate bool EnumChildProc(IntPtr hwnd, int lParam);
}
