
// Type: Intermech.Controls.OleContainer.NONCLIENTMETRICS
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class NONCLIENTMETRICS
{
  public int cbSize;
  public int iBorderWidth;
  public int iScrollWidth;
  public int iScrollHeight;
  public int iCaptionWidth;
  public int iCaptionHeight;
  [MarshalAs(UnmanagedType.Struct)]
  public LOGFONT lfCaptionFont;
  public int iSmCaptionWidth;
  public int iSmCaptionHeight;
  [MarshalAs(UnmanagedType.Struct)]
  public LOGFONT lfSmCaptionFont;
  public int iMenuWidth;
  public int iMenuHeight;
  [MarshalAs(UnmanagedType.Struct)]
  public LOGFONT lfMenuFont;
  [MarshalAs(UnmanagedType.Struct)]
  public LOGFONT lfStatusFont;
  [MarshalAs(UnmanagedType.Struct)]
  public LOGFONT lfMessageFont;

  public NONCLIENTMETRICS() => this.cbSize = Marshal.SizeOf(typeof (NONCLIENTMETRICS));
}
