// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.TreeHeaderCellWidget
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using System.Drawing;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

/// <summary>Ячейка заголовка</summary>
public sealed class TreeHeaderCellWidget : CellWidget
{
  public TreeHeaderCellWidget(Infralution.Controls.VirtualTree.RowWidget rowWidget, Column column)
    : base(rowWidget, column)
  {
    this.CellData.EvenStyle = new Style(this.CellData.EvenStyle);
    this.CellData.EvenStyle.BackColor = Color.LightGray;
    this.CellData.OddStyle = new Style(this.CellData.OddStyle);
    this.CellData.OddStyle.BackColor = Color.LightGray;
  }

  protected override void PaintBackground(
    Graphics graphics,
    Style rowStyle,
    Style cellStyle,
    bool printing)
  {
    Color highlight = SystemColors.Highlight;
    base.PaintBackground(graphics, rowStyle, cellStyle, printing);
    using (SolidBrush solidBrush = new SolidBrush(!this.Row.Selected ? (!(this.Row.Item is SpecificationSection) ? Color.LightGray : Color.GhostWhite) : SystemColors.Highlight))
      graphics.FillRectangle((Brush) solidBrush, this.Bounds);
  }

  public override int GetOptimalHeight(Graphics graphics) => base.GetOptimalHeight(graphics);

  protected override void PaintForeground(Graphics graphics, Style style, bool printing)
  {
  }
}
