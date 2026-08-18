// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.SpecRow_RowWidget
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using System.Drawing;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

/// <summary>Строка для записи спецификации</summary>
public sealed class SpecRow_RowWidget : Infralution.Controls.VirtualTree.RowWidget
{
  public SpecRow_RowWidget(PanelWidget panelWidget, Row row)
    : base(panelWidget, row)
  {
    this.RowData.EvenStyle = new Style();
    this.RowData.EvenStyle.BackColor = Color.White;
    this.RowData.OddStyle = new Style();
    this.RowData.OddStyle.BackColor = Color.White;
  }

  public override void OnLayout()
  {
    this.RowData.GetImage();
    base.OnLayout();
  }

  protected override void PaintBackground(Graphics graphics, Style style, bool printing)
  {
    base.PaintBackground(graphics, style, printing);
  }

  public override void OnPaint(Graphics graphics) => base.OnPaint(graphics);
}
