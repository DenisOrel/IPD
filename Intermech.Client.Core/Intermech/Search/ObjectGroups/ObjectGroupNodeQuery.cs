
// Type: Intermech.Search.ObjectGroups.ObjectGroupNodeQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectGroupNodeQuery : INodeQuery
{
  private List<NodeColumn> _nodeColumns = new List<NodeColumn>();
  private int _recordCount;
  private ObjectGroupNodeID _objectGroupNodeID;
  private object[] _recordValues;
  private object[] _rawRecordValues;

  public ObjectGroupNodeQuery(
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

  public void AddColumn(NodeColumn column, INodeColumnTransform transform)
  {
    if (column == null)
      throw new ArgumentNullException(nameof (column));
    this._nodeColumns.Add(column);
  }

  public void Execute(object bookmark, int count)
  {
    if (count <= 0)
      return;
    this.Execute();
  }

  public void Execute(NodeIDCollection nodeIDs)
  {
    if (nodeIDs == null)
      throw new ArgumentNullException(nameof (nodeIDs));
    if (!nodeIDs.Contains((INodeID) new ObjectGroupNodeID(this.ProjectTypeID, this.RelationTypeID, this.PartTypeID, this.ProjectVersionID)))
      return;
    this.Execute();
  }

  public object Bookmark => (object) null;

  public int RecordCount => this._recordCount;

  public NodeQueryOptions Options { get; set; }

  public long TotalRecordCount => 1;

  public INodeID GetRecordNodeID(int index)
  {
    if (index != 0)
      throw new ArgumentOutOfRangeException();
    return (INodeID) this._objectGroupNodeID;
  }

  public object[] GetRecordValues(int index)
  {
    if (index != 0)
      throw new ArgumentOutOfRangeException();
    return this._recordValues;
  }

  public object[] GetRawRecordValues(int index)
  {
    if (index != 0)
      throw new ArgumentOutOfRangeException();
    return this._rawRecordValues;
  }

  private void Execute()
  {
    this._recordCount = 1;
    this._objectGroupNodeID = new ObjectGroupNodeID(this.ProjectTypeID, this.RelationTypeID, this.PartTypeID, this.ProjectVersionID);
    this._recordValues = this._rawRecordValues = (object[]) this._nodeColumns.Select<NodeColumn, string>((Func<NodeColumn, string>) (o => !object.Equals(o.ID, (object) "CAPTION") && !object.Equals(o.ID, (object) "F_CAPTION") ? (string) null : MetaDataHelper.GetObjectTypeName(this.PartTypeID))).ToArray<string>();
  }
}
