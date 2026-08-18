// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseCatalogsPart
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

internal class ImbaseCatalogsPart : INodeItems, INodePart, INodeQuerySupport
{
  private object _owner;

  public ContentAttributes GetAttributesOf(INodeID nodeID) => ContentAttributes.Folder;

  public INode GetChild(INodeID nodeID)
  {
    INode node = (ServicesManager.GetService(typeof (IFactory)) as IFactory).GetNode(nodeID.CategoryID, nodeID.TypeID);
    (node as ICatalogsNode).Bind(((CatalogsNodeID) nodeID).CatalogName);
    return node;
  }

  public string GetAddress(INodeID nodeID) => (nodeID as CatalogsNodeID).CatalogName;

  public INodeID ParseAddress(string address) => (INodeID) new CatalogsNodeID(address);

  public PersistentState Serialize(INodeID nodeID)
  {
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("Catalog", (object) ((CatalogsNodeID) nodeID).CatalogName);
    return persistentState;
  }

  public INodeID Deserialize(PersistentState persistNodeID)
  {
    object obj = persistNodeID.GetValue("Catalog");
    return obj == null || !(obj is string) ? (INodeID) null : (INodeID) new CatalogsNodeID(Convert.ToString(obj));
  }

  public object GetData(INodeID nodeID, Type dataFormat) => (object) null;

  public object[] GetData(NodeIDCollection nodeIDs, Type dataFormat) => new object[nodeIDs.Count];

  public IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    return (IUpdateAnalyser) null;
  }

  public object GetService(Type service) => (object) null;

  public object Owner
  {
    get => this._owner;
    set => this._owner = value;
  }

  public INodeQuery GetQuery() => (INodeQuery) new ImbaseCatalogsQuery((INodeQuerySupport) this);

  public NodeColumnCollection GetDefaultColumns()
  {
    return new NodeColumnCollection()
    {
      (ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes).CreateColumn(Intermech.Navigator.Consts.NameColumnSchemeGuid, (object) "F_CAPTION")
    };
  }

  public NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    return new NodeColumnCollection()
    {
      (ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes).CreateColumn(Intermech.Navigator.Consts.NameColumnSchemeGuid, (object) "F_CAPTION")
    };
  }

  public List<string> GetSupportedColumnSetNames() => (List<string>) null;

  public object MapColumnToField(NodeColumn column)
  {
    object field = (object) null;
    if (column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && column.ID.Equals((object) "F_CAPTION"))
      field = column.ID;
    else if (column.SchemeGuid == Intermech.Navigator.Consts.NameColumnSchemeGuid)
      field = column.ID;
    return field;
  }

  public List<object> GetSpecialFields()
  {
    return new List<object>() { (object) "F_CAPTION" };
  }

  public INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    return (INodeID) new CatalogsNodeID(Convert.ToString(fieldValues[adapter.GetFieldIndex((object) "F_CAPTION")]));
  }

  public object CreateRecordId(INodeID nodeId) => (object) (nodeId as CatalogsNodeID).CatalogName;
}
