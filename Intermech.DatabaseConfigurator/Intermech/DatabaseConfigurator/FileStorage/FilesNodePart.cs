// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FilesNodePart
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class FilesNodePart : FileNodeItems, INodePart, INodeItems, INodeQuerySupport
{
  private ConditionStructure[] conditions;
  private object owner;
  private long _storageID;

  public FilesNodePart(ConditionStructure[] conditions, long storageID)
  {
    this.conditions = conditions;
    this._storageID = storageID;
  }

  public object Owner
  {
    get => this.owner;
    set => this.owner = value;
  }

  public INodeQuery GetQuery()
  {
    if ((FileStorageNode) this.owner != null)
      this.conditions = ((FileStorageNode) this.owner).ConditionStructures;
    return (INodeQuery) new FilesQuery((INodeQuerySupport) this, this.conditions, this._storageID);
  }

  public NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.CollectColumns(columns);
    return columns;
  }

  public NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.CollectColumns(columns);
    return columns;
  }

  public List<string> GetSupportedColumnSetNames() => (List<string>) null;

  public object MapColumnToField(NodeColumn column)
  {
    return !(column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid) ? (object) null : column.ID;
  }

  public List<object> GetSpecialFields()
  {
    return new List<object>()
    {
      (object) ObligatoryObjectAttributes.F_FILE_ID,
      (object) ObligatoryObjectAttributes.F_ZIPSIZE
    };
  }

  public INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int fieldIndex1 = adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_FILE_ID);
    long int64_1 = Convert.ToInt64(fieldValues[fieldIndex1]);
    int fieldIndex2 = adapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_ZIPSIZE);
    long int64_2 = Convert.ToInt64(fieldValues[fieldIndex2]);
    return (INodeID) new FileNodeID(int64_1, int64_2);
  }

  public object CreateRecordId(INodeID nodeId) => (object) ((FileNodeID) nodeId).FileID;
}
