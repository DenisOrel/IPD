// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.InformationRequest.NativeMethods
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.WindowsDll;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Interfaces.Client.InformationRequest;

internal static class NativeMethods
{
  public const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

  [DllImport("gdi32.dll")]
  public static extern bool BitBlt(
    IntPtr hdcDest,
    int nxDest,
    int nyDest,
    int nWidth,
    int nHeight,
    IntPtr hdcSrc,
    int nXSrc,
    int nYSrc,
    NativeMethods.RasterOperations dwRop);

  [DllImport("gdi32.dll")]
  public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

  [DllImport("gdi32.dll")]
  public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

  [DllImport("gdi32.dll")]
  public static extern IntPtr DeleteDC(IntPtr hdc);

  [DllImport("gdi32.dll")]
  public static extern IntPtr DeleteObject(IntPtr hObject);

  [DllImport("dwmapi.dll")]
  public static extern int DwmGetWindowAttribute(
    IntPtr hwnd,
    int dwAttribute,
    out Interop.RECT pvAttribute,
    int cbAttribute);

  [DllImport("user32.dll")]
  public static extern IntPtr GetDesktopWindow();

  [DllImport("user32.dll")]
  public static extern IntPtr GetForegroundWindow();

  [DllImport("user32.dll")]
  public static extern IntPtr GetWindowDC(IntPtr hWnd);

  [DllImport("user32.dll", SetLastError = true)]
  public static extern bool GetWindowRect(IntPtr hwnd, out Interop.RECT lpRect);

  [DllImport("user32.dll")]
  public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);

  [DllImport("User32.dll")]
  public static extern IntPtr GetDC(IntPtr hwnd);

  [DllImport("gdi32.dll")]
  public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

  [DllImport("gdi32.dll")]
  public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool GetMonitorInfo(IntPtr hMonitor, ref NativeMethods.MonitorInfoEx lpmi);

  [DllImport("user32.dll")]
  public static extern bool EnumDisplayMonitors(
    IntPtr hdc,
    IntPtr lprcClip,
    NativeMethods.MonitorEnumProc lpfnEnum,
    IntPtr dwData);

  [DllImport("gdi32.dll")]
  public static extern IntPtr CreateDC(
    string lpszDriver,
    string lpszDevice,
    string lpszOutput,
    IntPtr lpInitData);

  public delegate bool MonitorEnumProc(
    IntPtr hMonitor,
    IntPtr hdcMonitor,
    ref Interop.RECT lprcMonitor,
    IntPtr dwData);

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  internal struct MonitorInfoEx
  {
    private const int CCHDEVICENAME = 32 /*0x20*/;
    /// <summary>
    /// The size, in bytes, of the structure. Set this member to sizeof(MONITORINFOEX) (72) before calling the GetMonitorInfo function.
    /// Doing so lets the function determine the type of structure you are passing to it.
    /// </summary>
    public uint Size;
    /// <summary>
    /// A RECT structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates.
    /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
    /// </summary>
    public Interop.RECT Monitor;
    /// <summary>
    /// A RECT structure that specifies the work area rectangle of the display monitor that can be used by applications,
    /// expressed in virtual-screen coordinates. Windows uses this rectangle to maximize an application on the monitor.
    /// The rest of the area in rcMonitor contains system windows such as the task bar and side bars.
    /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
    /// </summary>
    public Interop.RECT WorkArea;
    /// <summary>
    /// The attributes of the display monitor.
    /// 
    /// This member can be the following value:
    ///   1 : MONITORINFOF_PRIMARY
    /// </summary>
    public uint Flags;
    /// <summary>
    /// A string that specifies the device name of the monitor being used. Most applications have no use for a display monitor name,
    /// and so can save some bytes by using a MONITORINFO structure.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
    public string DeviceName;

    public void Init()
    {
      this.Size = 104U;
      this.DeviceName = string.Empty;
    }
  }

  [Flags]
  public enum RasterOperations
  {
    SRCCOPY = 13369376, // 0x00CC0020
    SRCPAINT = 15597702, // 0x00EE0086
    SRCAND = 8913094, // 0x008800C6
    SRCINVERT = 6684742, // 0x00660046
    SRCERASE = 4457256, // 0x00440328
    NOTSRCCOPY = 3342344, // 0x00330008
    NOTSRCERASE = 1114278, // 0x001100A6
    MERGECOPY = 12583114, // 0x00C000CA
    MERGEPAINT = 12255782, // 0x00BB0226
    PATCOPY = 15728673, // 0x00F00021
    PATPAINT = 16452105, // 0x00FB0A09
    PATINVERT = 5898313, // 0x005A0049
    DSTINVERT = 5570569, // 0x00550009
    BLACKNESS = 66, // 0x00000042
    WHITENESS = 16711778, // 0x00FF0062
    CAPTUREBLT = 1073741824, // 0x40000000
  }
}
