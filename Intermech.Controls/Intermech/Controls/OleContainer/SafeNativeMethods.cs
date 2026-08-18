
// Type: Intermech.Controls.OleContainer.SafeNativeMethods
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;
using System.Security;


namespace Intermech.Controls.OleContainer;

[SuppressUnmanagedCodeSecurity]
internal class SafeNativeMethods
{
  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool DrawMenuBar(HandleRef hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetWindowPos(
    HandleRef hWnd,
    HandleRef hWndInsertAfter,
    int x,
    int y,
    int cx,
    int cy,
    int flags);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int RegisterWindowMessage(string msg);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);
}
