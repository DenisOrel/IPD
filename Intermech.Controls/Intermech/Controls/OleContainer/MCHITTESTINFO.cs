
// Type: Intermech.Controls.OleContainer.MCHITTESTINFO
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class MCHITTESTINFO
{
  public int cbSize;
  public int pt_x;
  public int pt_y;
  public int uHit;
  public short st_wYear;
  public short st_wMonth;
  public short st_wDayOfWeek;
  public short st_wDay;
  public short st_wHour;
  public short st_wMinute;
  public short st_wSecond;
  public short st_wMilliseconds;

  public MCHITTESTINFO() => this.cbSize = Marshal.SizeOf(typeof (MCHITTESTINFO));
}
