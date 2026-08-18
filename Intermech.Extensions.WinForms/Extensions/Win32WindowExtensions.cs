// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Win32WindowExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.WindowsDll;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions;

public static class Win32WindowExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Rectangle GetWindowRect([NotNull] this IWin32Window win32Window)
  {
    return (Rectangle) User32.GetWindowRect_ThrowWinErrors(win32Window.Handle);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Size GetClientAreaSize([NotNull] this IWin32Window win32Window)
  {
    return ((Rectangle) User32.GetClientRect_ThrowWinErrors(win32Window.Handle)).Size;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Rectangle GetClientAreaOnScreen([NotNull] this IWin32Window win32Window)
  {
    Interop.POINT pt = new Interop.POINT(0, 0);
    return new Rectangle((Point) User32.ClientToScreen_ThrowWinErrors(win32Window.Handle, in pt), win32Window.GetClientAreaSize());
  }

  public static bool IsInvokeRequired([NotNull] this IWin32Window win32Window)
  {
    return Kernel32.GetCurrentThreadId() != User32.GetWindowThreadProcessId_ThrowWinErrors(win32Window.Handle, IntPtr.Zero);
  }

  [NotNull]
  public static string GetClassName([NotNull] this IWin32Window win32Window)
  {
    return User32.GetClassName_ThrowWinErrors(win32Window.Handle);
  }

  [CanBeNull]
  public static User32.MonitorInfoEx GetMonitorInfo([CanBeNull] this IWin32Window win32Window)
  {
    if (win32Window == null)
    {
      IntPtr handle = new IntPtr();
      try
      {
        handle = Process.GetCurrentProcess().MainWindowHandle;
      }
      catch
      {
      }
      if (handle != IntPtr.Zero)
        win32Window = Win32Window.Create(handle);
    }
    return win32Window == null ? (User32.MonitorInfoEx) null : User32.GetMonitorInfo_ThrowWinErrors(User32.MonitorFromWindow(win32Window.Handle, User32.DefaultMonitor.DefaultToNearest));
  }

  public static void ShowWindow([NotNull] this IWin32Window win32Window, User32.ShowWindowCommand cmdShow = User32.ShowWindowCommand.SW_SHOW)
  {
    if (!(win32Window.Handle != IntPtr.Zero))
      return;
    User32.ShowWindow_HandleWinErros(win32Window.Handle, cmdShow);
  }

  public static bool TryGetWindowStyles(
    [NotNull] this IWin32Window win32Window,
    out User32.WindowStyles styles,
    out User32.WindowStylesEx stylesEx)
  {
    IntPtr handle = win32Window.Handle;
    if (handle == IntPtr.Zero)
    {
      styles = User32.WindowStyles.WS_OVERLAPPED;
      stylesEx = User32.WindowStylesEx.WS_EX_LEFT;
      return false;
    }
    User32.WINDOWINFO WindowInfo = new User32.WINDOWINFO();
    if (!User32.GetWindowInfo(handle, WindowInfo))
    {
      styles = User32.WindowStyles.WS_OVERLAPPED;
      stylesEx = User32.WindowStylesEx.WS_EX_LEFT;
      return false;
    }
    styles = WindowInfo.Style;
    stylesEx = WindowInfo.ExStyle;
    return true;
  }

  [NotNull]
  private static CultureInfo GetKeyboardLayoutCulture(
    [NotNull] this IWin32Window win32Window,
    bool throwExceptionIfNotFound = false)
  {
    IntPtr handle = win32Window.Handle;
    if (!(handle == IntPtr.Zero))
      return new CultureInfo(User32.GetKeyboardLayout(User32.GetWindowThreadProcessId(handle, IntPtr.Zero)).ToInt32() & (int) ushort.MaxValue);
    if (!throwExceptionIfNotFound)
      return CultureInfo.CurrentCulture;
    throw new InvalidOperationException("Can`t get Handle of the window");
  }

  private static HitTestValues NonClientHitTest(
    [NotNull] this IWin32Window win32Window,
    Point screenCoordsPoint)
  {
    IntPtr num1 = User32.SendMessage(win32Window.Handle, 132, 0, Interop.MakeLParam((short) screenCoordsPoint.X, (short) screenCoordsPoint.Y));
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    int num2 = !(num1 == IntPtr.Zero) ? num1.ToInt32() : throw WindowsApiException.GetLastForce("WM_NCHITTEST", (ArgumentDescriptor) @(typeof (short), (object) (short) screenCoordsPoint.X), (ArgumentDescriptor) @(typeof (short), (object) (short) screenCoordsPoint.Y));
    int num3 = (int) Intermech.Diagnostics.Check.EnumInRange<HitTestValues>((HitTestValues) num2, "result");
    return (HitTestValues) num2;
  }
}
