
// Type: Intermech.Controls.OleContainer.WNDCLASS_D
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class WNDCLASS_D
{
  public int style;
  public WndProc lpfnWndProc;
  public int cbClsExtra;
  public int cbWndExtra;
  public IntPtr hInstance;
  public IntPtr hIcon;
  public IntPtr hCursor;
  public IntPtr hbrBackground;
  public string lpszMenuName;
  public string lpszClassName;

  public WNDCLASS_D()
  {
    this.hInstance = IntPtr.Zero;
    this.hIcon = IntPtr.Zero;
    this.hCursor = IntPtr.Zero;
    this.hbrBackground = IntPtr.Zero;
  }
}
