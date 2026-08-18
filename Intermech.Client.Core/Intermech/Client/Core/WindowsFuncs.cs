
// Type: Intermech.Client.Core.WindowsFuncs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public static class WindowsFuncs
{
  [DllImport("user32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  private static extern bool GetWindowPlacement(
    IntPtr hWnd,
    ref WindowsFuncs.WINDOWPLACEMENT lpwndpl);

  /// <summary>
  /// Восстанавливает форму в предыдущее до минимизирования состояние
  /// </summary>
  /// <param name="form"></param>
  public static void Restore(this Form form)
  {
    if (form.WindowState != FormWindowState.Minimized)
      return;
    WindowsFuncs.WINDOWPLACEMENT lpwndpl = new WindowsFuncs.WINDOWPLACEMENT();
    lpwndpl.length = Marshal.SizeOf<WindowsFuncs.WINDOWPLACEMENT>(lpwndpl);
    WindowsFuncs.GetWindowPlacement(form.Handle, ref lpwndpl);
    if ((lpwndpl.flags & 2) == 2)
      form.WindowState = FormWindowState.Maximized;
    else
      form.WindowState = FormWindowState.Normal;
  }

  private struct WINDOWPLACEMENT
  {
    public int length;
    public int flags;
    public int showCmd;
    public Point ptMinPosition;
    public Point ptMaxPosition;
    public Rectangle rcNormalPosition;
  }
}
