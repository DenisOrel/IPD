
// Type: Intermech.Controls.OleContainer.SIZE
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class SIZE
{
  public int cx;
  public int cy;

  public SIZE()
  {
  }

  public SIZE(int cx, int cy)
  {
    this.cx = cx;
    this.cy = cy;
  }

  public Size ToSize() => new Size(this.cx, this.cy);
}
