// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.InformationRequest.ScreenshotCapture
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.WindowsDll;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client.InformationRequest;

internal sealed class ScreenshotCapture
{
  public static List<DisplayInfo> GetDisplays()
  {
    List<DisplayInfo> col = new List<DisplayInfo>();
    NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (NativeMethods.MonitorEnumProc) ((IntPtr hMonitor, IntPtr hdcMonitor, ref Interop.RECT lprcMonitor, IntPtr dwData) =>
    {
      NativeMethods.MonitorInfoEx lpmi = new NativeMethods.MonitorInfoEx();
      lpmi.Size = (uint) Marshal.SizeOf<NativeMethods.MonitorInfoEx>(lpmi);
      int nIndex1 = 117;
      int nIndex2 = 118;
      if (NativeMethods.GetMonitorInfo(hMonitor, ref lpmi))
      {
        IntPtr dc = NativeMethods.CreateDC((string) null, lpmi.DeviceName, (string) null, IntPtr.Zero);
        try
        {
          int deviceCaps1 = NativeMethods.GetDeviceCaps(dc, nIndex2);
          int deviceCaps2 = NativeMethods.GetDeviceCaps(dc, nIndex1);
          col.Add(new DisplayInfo()
          {
            ScreenWidth = deviceCaps1.ToString(),
            ScreenHeight = deviceCaps2.ToString(),
            MonitorArea = new Interop.RECT(lpmi.Monitor.Left, lpmi.Monitor.Top, deviceCaps1, deviceCaps2),
            WorkArea = lpmi.WorkArea,
            Availability = lpmi.Flags.ToString()
          });
        }
        finally
        {
          NativeMethods.DeleteDC(dc);
        }
      }
      return true;
    }), IntPtr.Zero);
    return col;
  }

  public Bitmap CaptureActiveWindow() => this.CaptureWindow(NativeMethods.GetForegroundWindow());

  public Bitmap CaptureDesktop() => this.CaptureDesktop(false);

  public Bitmap CaptureDesktop(bool workingAreaOnly)
  {
    return this.CaptureDesktop(workingAreaOnly, Color.Transparent);
  }

  public Bitmap CaptureDesktop(Color invalidColor) => this.CaptureDesktop(false, invalidColor);

  public Bitmap CaptureDesktop(bool workingAreaOnly, Color invalidColor)
  {
    return this.CaptureDesktop(workingAreaOnly, invalidColor, (Predicate<int>) (index => true));
  }

  public Bitmap CaptureDesktop(Predicate<int> includeScreen)
  {
    return this.CaptureDesktop(false, Color.Transparent, includeScreen);
  }

  public Bitmap CaptureDesktop(
    bool workingAreaOnly,
    Color invalidColor,
    Predicate<int> includeScreen)
  {
    DesktopLayout desktopLayout = new DesktopLayout()
    {
      WorkingAreaOnly = workingAreaOnly
    };
    Bitmap bitmap1 = new Bitmap(desktopLayout.Width, desktopLayout.Height, PixelFormat.Format32bppArgb);
    using (Graphics graphics = Graphics.FromImage((Image) bitmap1))
    {
      graphics.Clear(invalidColor);
      for (int index = 0; index < desktopLayout.Count; ++index)
      {
        if (includeScreen(index))
        {
          Rectangle displayBounds = desktopLayout.GetDisplayBounds(index);
          using (Bitmap bitmap2 = this.CaptureRegion(displayBounds))
            graphics.DrawImageUnscaled((Image) bitmap2, desktopLayout.GetNormalizedDisplayBounds(displayBounds));
        }
      }
    }
    return bitmap1;
  }

  public Bitmap CaptureMonitor(DisplayInfo monitor) => this.CaptureMonitor(monitor, false);

  public Bitmap CaptureMonitor(DisplayInfo monitor, bool workingAreaOnly)
  {
    return this.CaptureRegion((Rectangle) (workingAreaOnly ? monitor.WorkArea : monitor.MonitorArea));
  }

  public Bitmap CaptureMonitor(int index) => this.CaptureMonitor(index, false);

  public Bitmap CaptureMonitor(int index, bool workingAreaOnly)
  {
    List<DisplayInfo> displays = ScreenshotCapture.GetDisplays();
    if (displays.Count > index)
      return this.CaptureMonitor(displays[index], workingAreaOnly);
    throw new KernelException($"Запрашивается монитор с номером {index + 1} однако в системе только {displays.Count} мониторов.");
  }

  public Bitmap CaptureRegion(Rectangle region)
  {
    IntPtr desktopWindow = NativeMethods.GetDesktopWindow();
    IntPtr windowDc = NativeMethods.GetWindowDC(desktopWindow);
    IntPtr compatibleDc = NativeMethods.CreateCompatibleDC(windowDc);
    IntPtr compatibleBitmap = NativeMethods.CreateCompatibleBitmap(windowDc, region.Width, region.Height);
    IntPtr hObject = NativeMethods.SelectObject(compatibleDc, compatibleBitmap);
    bool flag = NativeMethods.BitBlt(compatibleDc, 0, 0, region.Width, region.Height, windowDc, region.Left, region.Top, NativeMethods.RasterOperations.SRCCOPY | NativeMethods.RasterOperations.CAPTUREBLT);
    try
    {
      return !flag ? new Bitmap(1, 1) : Image.FromHbitmap(compatibleBitmap);
    }
    finally
    {
      NativeMethods.SelectObject(compatibleDc, hObject);
      NativeMethods.DeleteObject(compatibleBitmap);
      NativeMethods.DeleteDC(compatibleDc);
      NativeMethods.ReleaseDC(desktopWindow, windowDc);
    }
  }

  public Bitmap CaptureWindow(IntPtr hWnd)
  {
    Interop.RECT rect;
    if (Environment.OSVersion.Version.Major < 6)
      NativeMethods.GetWindowRect(hWnd, out rect);
    else if (NativeMethods.DwmGetWindowAttribute(hWnd, 9, out rect, Marshal.SizeOf(typeof (Interop.RECT))) != 0)
      NativeMethods.GetWindowRect(hWnd, out rect);
    return this.CaptureRegion(Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom));
  }

  public Bitmap CaptureWindow(Form form) => this.CaptureWindow(form.Handle);
}
