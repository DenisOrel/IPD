// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionType.AutoSelectionTypesPart
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionType;

public class AutoSelectionTypesPart : IContextAware, INodePart, INodeItems, INodeQuerySupport
{
  protected IServiceProvider services;
  protected object owner;

  public AutoSelectionTypesPart(IServiceProvider services) => this.services = services;

  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this.services;
    [DebuggerStepThrough] set => this.services = value;
  }

  public ContentAttributes GetAttributesOf(INodeID nodeId) => ContentAttributes.Folder;

  public INode GetChild(INodeID nodeId)
  {
    if (!(nodeId is AutoSelectionTypeNodeID nodeID))
      return (INode) null;
    return ((INodesFactory) ServicesManager.GetService(typeof (IFactory))).GetNode((INodeID) nodeID, (object) nodeID.TypeID);
  }

  public string GetAddress(INodeID nodeId)
  {
    return nodeId is AutoSelectionTypeNodeID selectionTypeNodeId ? selectionTypeNodeId._caption : string.Empty;
  }

  public INodeID ParseAddress(string address)
  {
    object enumValue = EnumDescConverter.GetEnumValue(typeof (AutoSelectionNodeType), address);
    return enumValue is AutoSelectionNodeType ? (INodeID) new AutoSelectionTypeNodeID((int) enumValue) : (INodeID) null;
  }

  public PersistentState Serialize(INodeID nodeId)
  {
    if (!(nodeId is AutoSelectionTypeNodeID selectionTypeNodeId))
      return (PersistentState) null;
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("id", (object) selectionTypeNodeId.TypeID);
    return persistentState;
  }

  public INodeID Deserialize(PersistentState persistNodeId)
  {
    return persistNodeId?.GetValue("id") is int type ? (INodeID) new AutoSelectionTypeNodeID(type) : (INodeID) null;
  }

  public object GetData(INodeID nodeId, Type dataFormat)
  {
    int num = dataFormat == typeof (IImageState) ? 1 : 0;
    return (object) null;
  }

  public object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    object[] data = new object[nodeIDs.Count];
    for (int index = 0; index < data.Length; ++index)
      data[index] = this.GetData(nodeIDs[index], dataFormat);
    return data;
  }

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
    [DebuggerStepThrough] get => this.owner;
    [DebuggerStepThrough] set => this.owner = value;
  }

  public INodeQuery GetQuery()
  {
    return (INodeQuery) new AutoSelectionTypesQuery((INodeQuerySupport) this);
  }

  public NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection defaultColumns = new NodeColumnCollection();
    IColumnSchemes service = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    if (service != null)
      defaultColumns.Add(service.CreateColumn(Intermech.Navigator.Consts.NameColumnSchemeGuid, (object) AutoSelectionTypesQuery._CAPTION));
    return defaultColumns;
  }

  public NodeColumnCollection GetSupportedColumns(string columnSetName) => this.GetDefaultColumns();

  public List<string> GetSupportedColumnSetNames() => (List<string>) null;

  public object MapColumnToField(NodeColumn column)
  {
    return column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && (column.ID.Equals((object) AutoSelectionTypesQuery._CAPTION) || column.ID.Equals((object) AutoSelectionTypesQuery._ncNodeType)) || column.SchemeGuid == Intermech.Navigator.Consts.NameColumnSchemeGuid ? column.ID : (object) null;
  }

  public List<object> GetSpecialFields()
  {
    return new List<object>()
    {
      (object) AutoSelectionTypesQuery._ncNodeType
    };
  }

  public INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    return !(fieldValues[adapter.GetFieldIndex((object) AutoSelectionTypesQuery._ncNodeType)] is AutoSelectionTypeRec fieldValue) ? (INodeID) null : (INodeID) new AutoSelectionTypeNodeID((int) fieldValue.Type);
  }

  public object CreateRecordId(INodeID nodeId) => (object) ((AutoSelectionTypeNodeID) nodeId)._type;
}
