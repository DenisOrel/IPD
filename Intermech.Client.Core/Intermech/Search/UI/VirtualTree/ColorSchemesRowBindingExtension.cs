
// Type: Intermech.Search.UI.VirtualTree.ColorSchemesRowBindingExtension
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Navigator;
using Intermech.Search.ColorSchemes;
using System;
using System.Drawing;


namespace Intermech.Search.UI.VirtualTree;

public sealed class ColorSchemesRowBindingExtension : RowBindingExtensionBase
{
  public override void GetCellData(Row row, ColumnBase column, CellData cellData)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    if (column == null)
      throw new ArgumentNullException(nameof (column));
    if (cellData == null)
      throw new ArgumentNullException(nameof (cellData));
    NavGradientBrush navGradientBrush = ColorSchemesClientHelper.GetNavGradientBrush(row.Item, new Rectangle());
    if (navGradientBrush == null)
      return;
    StyleDelta delta = new StyleDelta()
    {
      BackColor = navGradientBrush.StartColor,
      GradientColor = navGradientBrush.EndColor,
      GradientMode = navGradientBrush.Mode
    };
    cellData.EvenStyle = new Style(cellData.EvenStyle, delta);
    cellData.OddStyle = new Style(cellData.OddStyle, delta);
  }
}
