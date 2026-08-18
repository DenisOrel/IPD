
// Type: Intermech.Search.ObjectGroups.ObjectAndObjectGroupNodeQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectAndObjectGroupNodeQuery : INodeQuery
{
  private INodeQuery _nodeQuery;
  private ObjectGroupNodeQuery[] _objectGroupNodeQueries;
  private int _recordCount;
  private long _totalRecordCount;
  private INodeID[] _nodeIds;
  private object[][] _recordValuesArray;
  private object[][] _rawRecordValuesArray;

  public ObjectAndObjectGroupNodeQuery(
    INodeQuery nodeQuery,
    ObjectGroupNodeQuery[] objectGroupNodeQueries)
  {
    if (nodeQuery == null)
      throw new ArgumentNullException(nameof (nodeQuery));
    if (objectGroupNodeQueries == null)
      throw new ArgumentNullException(nameof (objectGroupNodeQueries));
    this._nodeQuery = nodeQuery;
    this._objectGroupNodeQueries = objectGroupNodeQueries;
  }

  public void AddColumn(NodeColumn column, INodeColumnTransform transform)
  {
    this._nodeQuery.AddColumn(column, transform);
    foreach (ObjectGroupNodeQuery objectGroupNodeQuery in this._objectGroupNodeQueries)
      objectGroupNodeQuery.AddColumn(column, transform);
  }

  public void Execute(object bookmark, int count)
  {
    this._nodeQuery.Execute(bookmark, count);
    List<Tuple<INodeID, object[], object[]>> source = new List<Tuple<INodeID, object[], object[]>>();
    for (int index = 0; index < this._nodeQuery.RecordCount; ++index)
      source.Add(new Tuple<INodeID, object[], object[]>(this._nodeQuery.GetRecordNodeID(index), this._nodeQuery.GetRecordValues(index), this._nodeQuery.GetRawRecordValues(index)));
    List<Tuple<INodeID, object[], object[]>> tupleList = new List<Tuple<INodeID, object[], object[]>>();
    foreach (ObjectGroupNodeQuery objectGroupNodeQuery in this._objectGroupNodeQueries)
    {
      int[] childrenAndSelfObjectTypeIds = this.GetChildrenAndSelfObjectTypeIds(objectGroupNodeQuery.PartTypeID);
      Tuple<INodeID, object[], object[]>[] array = source.Where<Tuple<INodeID, object[], object[]>>((Func<Tuple<INodeID, object[], object[]>, bool>) (o => o.Item1 is NodeID && ((IEnumerable<int>) childrenAndSelfObjectTypeIds).Contains<int>(((NodeID) o.Item1).ObjectTypeID))).ToArray<Tuple<INodeID, object[], object[]>>();
      tupleList.AddRange((IEnumerable<Tuple<INodeID, object[], object[]>>) array);
      if (array.Length != 0)
      {
        objectGroupNodeQuery.Execute((object) null, 1);
        Tuple<INodeID, object[], object[]> tuple = new Tuple<INodeID, object[], object[]>(objectGroupNodeQuery.GetRecordNodeID(0), objectGroupNodeQuery.GetRecordValues(0), objectGroupNodeQuery.GetRawRecordValues(0));
        source.Insert(source.IndexOf(array[0]), tuple);
      }
    }
    foreach (Tuple<INodeID, object[], object[]> tuple in tupleList)
      source.Remove(tuple);
    this._recordCount = source.Count;
    this._totalRecordCount = (long) source.Count;
    this._nodeIds = source.Select<Tuple<INodeID, object[], object[]>, INodeID>((Func<Tuple<INodeID, object[], object[]>, INodeID>) (o => o.Item1)).ToArray<INodeID>();
    this._recordValuesArray = source.Select<Tuple<INodeID, object[], object[]>, object[]>((Func<Tuple<INodeID, object[], object[]>, object[]>) (o => o.Item2)).ToArray<object[]>();
    this._rawRecordValuesArray = source.Select<Tuple<INodeID, object[], object[]>, object[]>((Func<Tuple<INodeID, object[], object[]>, object[]>) (o => o.Item3)).ToArray<object[]>();
  }

  public void Execute(NodeIDCollection nodeIDs)
  {
    NodeIDCollection nodeIDs1 = new NodeIDCollection();
    nodeIDs1.AddRange(nodeIDs.Where<INodeID>((Func<INodeID, bool>) (o => o is NodeID)));
    this._nodeQuery.Execute(nodeIDs1);
    List<Tuple<INodeID, object[], object[]>> source = new List<Tuple<INodeID, object[], object[]>>();
    for (int index = 0; index < this._nodeQuery.RecordCount; ++index)
      source.Add(new Tuple<INodeID, object[], object[]>(this._nodeQuery.GetRecordNodeID(index), this._nodeQuery.GetRecordValues(index), this._nodeQuery.GetRawRecordValues(index)));
    foreach (INodeID nodeId in (List<INodeID>) nodeIDs)
    {
      if (nodeId is ObjectGroupNodeID)
      {
        ObjectGroupNodeID objectGroupNodeID = (ObjectGroupNodeID) nodeId;
        ObjectGroupNodeQuery objectGroupNodeQuery = ((IEnumerable<ObjectGroupNodeQuery>) this._objectGroupNodeQueries).FirstOrDefault<ObjectGroupNodeQuery>((Func<ObjectGroupNodeQuery, bool>) (o => o.ProjectTypeID == objectGroupNodeID.ProjectTypeID && o.RelationTypeID == objectGroupNodeID.RelationTypeID && o.PartTypeID == objectGroupNodeID.PartTypeID && o.ProjectVersionID == objectGroupNodeID.ProjectVersionID));
        if (objectGroupNodeQuery != null)
        {
          objectGroupNodeQuery.Execute((object) null, 1);
          source.Add(new Tuple<INodeID, object[], object[]>(objectGroupNodeQuery.GetRecordNodeID(0), objectGroupNodeQuery.GetRecordValues(0), objectGroupNodeQuery.GetRawRecordValues(0)));
        }
      }
    }
    this._recordCount = source.Count;
    this._totalRecordCount = (long) source.Count;
    this._nodeIds = source.Select<Tuple<INodeID, object[], object[]>, INodeID>((Func<Tuple<INodeID, object[], object[]>, INodeID>) (o => o.Item1)).ToArray<INodeID>();
    this._recordValuesArray = source.Select<Tuple<INodeID, object[], object[]>, object[]>((Func<Tuple<INodeID, object[], object[]>, object[]>) (o => o.Item2)).ToArray<object[]>();
    this._rawRecordValuesArray = source.Select<Tuple<INodeID, object[], object[]>, object[]>((Func<Tuple<INodeID, object[], object[]>, object[]>) (o => o.Item3)).ToArray<object[]>();
  }

  public object Bookmark => this._nodeQuery.Bookmark;

  public int RecordCount => this._recordCount;

  public NodeQueryOptions Options
  {
    get => this._nodeQuery.Options;
    set => this._nodeQuery.Options = value;
  }

  public long TotalRecordCount => this._totalRecordCount;

  public INodeID GetRecordNodeID(int index)
  {
    return index >= 0 && index < this._recordCount ? this._nodeIds[index] : throw new ArgumentException();
  }

  public object[] GetRecordValues(int index)
  {
    return index >= 0 && index < this._recordCount ? this._recordValuesArray[index] : throw new ArgumentException();
  }

  public object[] GetRawRecordValues(int index)
  {
    return index >= 0 && index < this._recordCount ? this._rawRecordValuesArray[index] : throw new ArgumentException();
  }

  private int[] GetChildrenAndSelfObjectTypeIds(int objectTypeID)
  {
    List<int> intList = new List<int>(objectTypeID);
    intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeID));
    return intList.ToArray();
  }
}
