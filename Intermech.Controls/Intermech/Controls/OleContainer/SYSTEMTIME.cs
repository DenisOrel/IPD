
// Type: Intermech.Controls.OleContainer.SYSTEMTIME
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class SYSTEMTIME
{
  public short wYear;
  public short wMonth;
  public short wDayOfWeek;
  public short wDay;
  public short wHour;
  public short wMinute;
  public short wSecond;
  public short wMilliseconds;

  public override string ToString()
  {
    return $"[SYSTEMTIME: {this.wDay.ToString()}/{this.wMonth.ToString()}/{this.wYear.ToString()} {this.wHour.ToString()}:{this.wMinute.ToString()}:{this.wSecond.ToString()}]";
  }
}
