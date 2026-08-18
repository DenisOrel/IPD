
// Type: Intermech.Controls.OleContainer.COMRECT
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class COMRECT
{
  public int left;
  public int top;
  public int right;
  public int bottom;

  public COMRECT()
  {
  }

  public COMRECT(Rectangle r)
  {
    this.left = r.X;
    this.top = r.Y;
    this.right = r.Right;
    this.bottom = r.Bottom;
  }

  public COMRECT(RECT rect)
  {
    this.left = rect.left;
    this.top = rect.top;
    this.bottom = rect.bottom;
    this.right = rect.right;
  }

  public COMRECT(int left, int top, int right, int bottom)
  {
    this.left = left;
    this.top = top;
    this.right = right;
    this.bottom = bottom;
  }

  public void CopyTo(COMRECT destRect)
  {
    destRect.left = this.left;
    destRect.right = this.right;
    destRect.top = this.top;
    destRect.bottom = this.bottom;
  }

  public RECT ToRECT() => new RECT(this.left, this.top, this.right, this.bottom);

  public static COMRECT FromXYWH(int x, int y, int width, int height)
  {
    return new COMRECT(x, y, x + width, y + height);
  }

  public override string ToString()
  {
    return $"Left = {(object) this.left} Top {(object) this.top} Right = {(object) this.right} Bottom = {(object) this.bottom}";
  }
}
