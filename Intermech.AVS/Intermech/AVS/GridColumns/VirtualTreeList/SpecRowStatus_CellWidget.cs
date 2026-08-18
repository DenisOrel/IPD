// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.SpecRowStatus_CellWidget
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using System.Drawing;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

/// <summary>Ячейка для статуса</summary>
public sealed class SpecRowStatus_CellWidget(Infralution.Controls.VirtualTree.RowWidget rowWidget, Column column) : 
  SpecRow_CellWidget(rowWidget, column)
{
  protected override void PaintForeground(Graphics graphics, Style style, bool printing)
  {
    if (!(this.CellData.Value is Image image))
      return;
    Point point;
    ref Point local = ref point;
    Rectangle bounds = this.Bounds;
    int x = bounds.Right - 17;
    bounds = this.Bounds;
    int y = bounds.Top + 1;
    local = new Point(x, y);
    graphics.DrawImageUnscaled(image, point);
  }
}
