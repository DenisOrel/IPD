
// Type: Intermech.Search.ObjectGroups.ObjectGroupNodePart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectGroupNodePart : INodeItems, INodePart, IContextAware
{
  public ObjectGroupNodePart(
    int projectTypeID,
    int relationTypeID,
    int partTypeID,
    long projectVersionID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(projectTypeID))
      throw new ArgumentException();
    if (RelationTypeHelper.IsUnknownRelationTypeID(relationTypeID))
      throw new ArgumentException();
    if (ObjectTypeHelper.IsUnknownObjectTypeID(partTypeID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(projectVersionID))
      throw new ArgumentException();
    this.ProjectTypeID = projectTypeID;
    this.RelationTypeID = relationTypeID;
    this.PartTypeID = partTypeID;
    this.ProjectVersionID = projectVersionID;
  }

  public int ProjectTypeID { get; private set; }

  public int RelationTypeID { get; private set; }

  public int PartTypeID { get; private set; }

  public long ProjectVersionID { get; private set; }

  public ContentAttributes GetAttributesOf(INodeID nodeID)
  {
    if (nodeID == null)
      throw new ArgumentNullException(nameof (nodeID));
    return ContentAttributes.HasChildren;
  }

  public INode GetChild(INodeID nodeID)
  {
    if (nodeID == null)
      throw new ArgumentNullException(nameof (nodeID));
    if (!(nodeID is ObjectGroupNodeID))
      return (INode) null;
    ObjectGroupNodeID objectGroupNodeId = (ObjectGroupNodeID) nodeID;
    return (INode) new ObjectGroupNode(objectGroupNodeId.ProjectTypeID, objectGroupNodeId.RelationTypeID, objectGroupNodeId.PartTypeID, objectGroupNodeId.ProjectVersionID)
    {
      Services = this.Services
    };
  }

  public string GetAddress(INodeID nodeID)
  {
    if (nodeID == null)
      throw new ArgumentNullException(nameof (nodeID));
    return nodeID is ObjectGroupNodeID ? MetaDataHelper.GetObjectTypeName(((ObjectGroupNodeID) nodeID).PartTypeID) : (string) null;
  }

  public INodeID ParseAddress(string address)
  {
    if (string.IsNullOrEmpty(address))
      throw new ArgumentException();
    return (INodeID) new ObjectGroupNodeID(this.ProjectTypeID, this.RelationTypeID, this.PartTypeID, this.ProjectVersionID);
  }

  public PersistentState Serialize(INodeID nodeID)
  {
    if (nodeID == null)
      throw new ArgumentNullException(nameof (nodeID));
    if (!(nodeID is ObjectGroupNodeID))
      return (PersistentState) null;
    return new PersistentState();
  }

  public INodeID Deserialize(PersistentState persistNodeID)
  {
    if (persistNodeID == null)
      throw new ArgumentException();
    return (INodeID) new ObjectGroupNodeID(this.ProjectTypeID, this.RelationTypeID, this.PartTypeID, this.ProjectVersionID);
  }

  public object GetData(INodeID nodeID, Type dataFormat)
  {
    if (nodeID == null)
      throw new ArgumentNullException(nameof (nodeID));
    if (dataFormat == (Type) null)
      throw new ArgumentNullException(nameof (dataFormat));
    if (!(nodeID is ObjectGroupNodeID))
      return (object) null;
    return dataFormat == typeof (INodeID) ? (object) nodeID : (object) null;
  }

  public object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    if (nodeIDs == null)
      throw new ArgumentNullException(nameof (nodeIDs));
    if (dataFormat == (Type) null)
      throw new ArgumentNullException(nameof (dataFormat));
    return (object[]) null;
  }

  public IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    return (IUpdateAnalyser) null;
  }

  public object GetService(Type service) => (object) null;

  public object Owner { get; set; }

  public INodeQuery GetQuery()
  {
    return (INodeQuery) new ObjectGroupNodeQuery(this.ProjectTypeID, this.RelationTypeID, this.PartTypeID, this.ProjectVersionID);
  }

  public NodeColumnCollection GetDefaultColumns()
  {
    return new NodeColumnCollection((IEnumerable<NodeColumn>) Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
  }

  public NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    return new NodeColumnCollection((IEnumerable<NodeColumn>) Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
  }

  public List<string> GetSupportedColumnSetNames() => (List<string>) null;

  public IServiceProvider Services { get; set; }
}
