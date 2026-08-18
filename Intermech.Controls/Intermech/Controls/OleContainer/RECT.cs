
// Type: Intermech.Controls.OleContainer.RECT
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;


namespace Intermech.Controls.OleContainer;

public struct RECT
{
  public int left;
  public int top;
  public int right;
  public int bottom;

  public RECT(int left, int top, int right, int bottom)
  {
    this.left = left;
    this.top = top;
    this.right = right;
    this.bottom = bottom;
  }

  public RECT(Rectangle r)
  {
    this.left = r.Left;
    this.top = r.Top;
    this.right = r.Right;
    this.bottom = r.Bottom;
  }

  public static RECT FromXYWH(int x, int y, int width, int height)
  {
    return new RECT(x, y, x + width, y + height);
  }

  public Size Size => new Size(this.right - this.left, this.bottom - this.top);
}
