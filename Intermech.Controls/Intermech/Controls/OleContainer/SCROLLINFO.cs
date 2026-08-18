
// Type: Intermech.Controls.OleContainer.SCROLLINFO
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class SCROLLINFO
{
  public int cbSize;
  public int fMask;
  public int nMin;
  public int nMax;
  public int nPage;
  public int nPos;
  public int nTrackPos;

  public SCROLLINFO() => this.cbSize = Marshal.SizeOf(typeof (SCROLLINFO));

  public SCROLLINFO(int mask, int min, int max, int page, int pos)
  {
    this.cbSize = Marshal.SizeOf(typeof (SCROLLINFO));
    this.fMask = mask;
    this.nMin = min;
    this.nMax = max;
    this.nPage = page;
    this.nPos = pos;
  }
}
