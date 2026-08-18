
// Type: Intermech.Navigator.Controls.ChildrenViewRowData
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.Controls;

public sealed class ChildrenViewRowData
{
  public ChildrenViewRowData(INodeID nodeID)
  {
    this.NodeID = nodeID != null ? nodeID : throw new ArgumentNullException(nameof (nodeID));
    this.CellDataDictionary = new Dictionary<string, ChildrenViewCellData>();
  }

  public INodeID NodeID { get; private set; }

  public Dictionary<string, ChildrenViewCellData> CellDataDictionary { get; private set; }

  public object Tag { get; set; }

  public bool Changed
  {
    get
    {
      return this.CellDataDictionary.Values.Any<ChildrenViewCellData>((Func<ChildrenViewCellData, bool>) (o => o.RawValueChanged));
    }
  }
}
