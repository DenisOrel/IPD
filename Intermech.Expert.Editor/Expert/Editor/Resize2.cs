// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Resize2
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using SourceGrid3;
using SourceGrid3.Cells.Controllers;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class Resize2(CellResizeMode p_Mode) : Resizable(p_Mode)
{
  public override void OnMouseUp(CellContext sender, MouseEventArgs e)
  {
    if (this.IsWidthResizing)
    {
      CellResize cellWidthChanged = this.CellWidthChanged;
      if (cellWidthChanged != null)
        cellWidthChanged(sender, e);
    }
    if (this.IsHeightResizing)
    {
      CellResize cellHeightChanged = this.CellHeightChanged;
      if (cellHeightChanged != null)
        cellHeightChanged(sender, e);
    }
    base.OnMouseUp(sender, e);
  }

  public event CellResize CellWidthChanged;

  public event CellResize CellHeightChanged;

  public static Resize2 WidthResizePerform(CellResize cr)
  {
    Resize2 resize2 = new Resize2(CellResizeMode.Width);
    resize2.CellWidthChanged += cr;
    return resize2;
  }

  public static Resize2 HeightResizePerform(CellResize cr)
  {
    Resize2 resize2 = new Resize2(CellResizeMode.Height);
    resize2.CellHeightChanged += cr;
    return resize2;
  }

  public static Resize2 AnyResizePerform(CellResize cr)
  {
    Resize2 resize2 = new Resize2(CellResizeMode.Both);
    resize2.CellWidthChanged += cr;
    resize2.CellHeightChanged += cr;
    return resize2;
  }
}
