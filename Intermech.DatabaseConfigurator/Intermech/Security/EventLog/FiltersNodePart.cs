// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FiltersNodePart
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Security.EventLog;

internal class FiltersNodePart : FiltersNodeItems, INodePart, INodeItems, INodeQuerySupport
{
  private object _owner;

  public object Owner
  {
    get => this._owner;
    set => this._owner = value;
  }

  public INodeQuery GetQuery() => (INodeQuery) new FiltersQuery((INodeQuerySupport) this);

  public NodeColumnCollection GetDefaultColumns()
  {
    return new NodeColumnCollection()
    {
      ((IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes))).CreateColumn(Intermech.Navigator.Consts.NameColumnSchemeGuid, (object) "F_CAPTION")
    };
  }

  public NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    return new NodeColumnCollection()
    {
      ((IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes))).CreateColumn(Intermech.Navigator.Consts.NameColumnSchemeGuid, (object) "F_CAPTION")
    };
  }

  public List<string> GetSupportedColumnSetNames() => (List<string>) null;

  public object MapColumnToField(NodeColumn column)
  {
    return column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && column.ID.Equals((object) "F_CAPTION") || column.SchemeGuid == Intermech.Navigator.Consts.NameColumnSchemeGuid ? column.ID : (object) null;
  }

  public List<object> GetSpecialFields()
  {
    return new List<object>() { (object) "F_GUID" };
  }

  public INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    return (INodeID) new FilterNodeID((Guid) fieldValues[adapter.GetFieldIndex((object) "F_GUID")]);
  }

  public object CreateRecordId(INodeID nodeId) => (object) ((FilterNodeID) nodeId).Guid;
}
