
// Type: Intermech.Navigator.Controls.ChildrenViewCellData
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Controls;

public sealed class ChildrenViewCellData
{
  public ChildrenViewCellData(ChildrenViewRowData rowData, NodeColumn nodeColumn)
  {
    if (rowData == null)
      throw new ArgumentNullException(nameof (rowData));
    if (nodeColumn == null)
      throw new ArgumentException();
    this.RowData = rowData;
    this.NodeColumn = nodeColumn;
  }

  public ChildrenViewRowData RowData { get; private set; }

  public NodeColumn NodeColumn { get; private set; }

  public object RawValue { get; set; }

  public object Value { get; set; }

  public bool? ReadOnly { get; set; }

  public bool RawValueChanged { get; set; }
}
