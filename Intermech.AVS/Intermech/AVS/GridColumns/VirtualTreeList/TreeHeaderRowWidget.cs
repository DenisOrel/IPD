// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.TreeHeaderRowWidget
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

/// <summary>Строка заголовка</summary>
public sealed class TreeHeaderRowWidget : Infralution.Controls.VirtualTree.RowWidget
{
  public TreeHeaderRowWidget(PanelWidget panelWidget, Row row)
    : base(panelWidget, row)
  {
    this.RowData.EvenStyle = new Style();
    this.RowData.EvenStyle.BackColor = Color.LightGray;
    this.RowData.OddStyle = new Style();
    this.RowData.OddStyle.BackColor = Color.LightGray;
  }

  public override int GetOptimalRowHeight(Graphics graphics) => base.GetOptimalRowHeight(graphics);

  public override void OnLayout() => base.OnLayout();

  protected override void PaintBackground(Graphics graphics, Style style, bool printing)
  {
    base.PaintBackground(graphics, style, printing);
    using (SolidBrush solidBrush = new SolidBrush(Color.White))
      graphics.FillRectangle((Brush) solidBrush, this.Bounds);
  }

  protected override void PaintConnections(Graphics graphics, bool printing)
  {
    base.PaintConnections(graphics, printing);
  }

  public override void OnPaint(Graphics graphics)
  {
    int num = this.ChildWidgets.Count <= 0 ? 0 : (this.ChildWidgets[0] is ExpansionWidget ? 1 : 0);
    object obj = (object) null;
    if (num != 0)
      graphics.FillRectangle(Brushes.White, this.Bounds);
    else
      graphics.FillRectangle(Brushes.LightGray, this.Bounds);
    base.OnPaint(graphics);
    if (this.Columns.Count <= 0)
      return;
    CellWidget cellWidget = this.GetCellWidget(this.Columns[0]);
    if (cellWidget.CellData != null)
      obj = cellWidget.CellData.Value;
    base.OnPaint(graphics);
    string s = Convert.ToString(obj).Replace('\u0017', '-').Replace('\u000E', ' ');
    graphics.DrawString(s, this.Tree.Font, Brushes.Black, new PointF((float) this.Tree.Bounds.X + (float) this.Tree.Bounds.Width / 3f, (float) this.Bounds.Top));
  }
}
