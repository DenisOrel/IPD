
// Type: Intermech.Controls.Grid.RECT
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Controls.Grid;

/// <summary>
/// Internal struct for use with the header style flat only
/// </summary>
[StructLayout(LayoutKind.Explicit)]
internal struct RECT
{
  [FieldOffset(0)]
  public int Left;
  [FieldOffset(4)]
  public int Top;
  [FieldOffset(8)]
  public int Right;
  [FieldOffset(12)]
  public int Bottom;

  public RECT(int left, int top, int right, int bottom)
  {
    this.Left = left;
    this.Top = top;
    this.Right = right;
    this.Bottom = bottom;
  }

  public RECT(Rectangle rect)
  {
    this.Left = rect.Left;
    this.Top = rect.Top;
    this.Right = rect.Right;
    this.Bottom = rect.Bottom;
  }

  public Rectangle ToRectangle() => new Rectangle(this.Left, this.Top, this.Right, this.Bottom - 1);
}
