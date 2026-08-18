
// Type: Intermech.Search.UI.VirtualTree.NodeColumnTransformRowBindingExtension
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Navigator.Interfaces;
using Intermech.Search.NodeColumnTransforms;
using System;


namespace Intermech.Search.UI.VirtualTree;

public sealed class NodeColumnTransformRowBindingExtension : RowBindingExtensionBase
{
  public override void GetCellData(Row row, ColumnBase column, CellData cellData)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    if (column == null)
      throw new ArgumentNullException(nameof (column));
    if (cellData == null)
      throw new ArgumentNullException(nameof (cellData));
    if (!(column.Tag is NodeColumn))
      return;
    cellData.Value = NodeColumnTransformsClientHelper.GetCellValue(row.Item, (NodeColumn) column.Tag);
  }
}
