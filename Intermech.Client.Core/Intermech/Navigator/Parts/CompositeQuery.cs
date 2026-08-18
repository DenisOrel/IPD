
// Type: Intermech.Navigator.Parts.CompositeQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.Parts;

/// <summary>Реализует композитный запрос.</summary>
public class CompositeQuery : INodeQuery
{
  protected List<QuerySlot> _subqueries;
  protected List<QuerySlot> _resultQueries;
  protected List<int> _resultCounts;
  protected List<long> _totalCounts;
  protected NodeQueryOptions _options;
  protected int _count;
  private CompositeBookmark _bookmark;

  public CompositeQuery(List<QuerySlot> subqueries)
  {
    this._subqueries = subqueries;
    this._resultQueries = (List<QuerySlot>) null;
    this._resultCounts = (List<int>) null;
    this._totalCounts = (List<long>) null;
    this._count = 0;
    this._bookmark = (CompositeBookmark) null;
  }

  public void AddColumn(NodeColumn column, INodeColumnTransform transform)
  {
    for (int index = 0; index < this._subqueries.Count; ++index)
      this._subqueries[index].Object.AddColumn(column, transform);
  }

  public void Execute(object bookmark, int count)
  {
    if (count == 0)
      return;
    int queryIndex = bookmark == null ? 0 : ((CompositeBookmark) bookmark).QueryIndex;
    object queryBookmark = bookmark == null ? (object) null : ((CompositeBookmark) bookmark).QueryBookmark;
    List<QuerySlot> querySlotList = new List<QuerySlot>();
    List<int> intList = new List<int>();
    List<long> longList = new List<long>();
    int count1 = count;
    if (queryIndex < this._subqueries.Count)
    {
      this._subqueries[queryIndex].Object.Execute(queryBookmark, count1);
      if (this._subqueries[queryIndex].Object.RecordCount > 0)
      {
        querySlotList.Add(this._subqueries[queryIndex]);
        intList.Add(this._subqueries[queryIndex].Object.RecordCount);
        longList.Add((long) this._subqueries[queryIndex].Object.RecordCount);
      }
      if (count1 > 0)
        count1 -= this._subqueries[queryIndex].Object.RecordCount;
      for (++queryIndex; queryIndex < this._subqueries.Count && count1 > 0; ++queryIndex)
      {
        this._subqueries[queryIndex].Object.Execute((object) null, count1);
        if (this._subqueries[queryIndex].Object.RecordCount > 0)
        {
          querySlotList.Add(this._subqueries[queryIndex]);
          intList.Add(this._subqueries[queryIndex].Object.RecordCount);
        }
        count1 -= this._subqueries[queryIndex].Object.RecordCount;
      }
    }
    if (bookmark == null)
      this._totalCounts = longList;
    this._count = count > 0 ? count - count1 : Convert.ToInt32(this.TotalRecordCount);
    if (this._count <= 0)
      return;
    this._resultQueries = querySlotList;
    this._resultCounts = intList;
    if (this._subqueries[queryIndex - 1].Object.Bookmark == null)
    {
      if (queryIndex >= this._subqueries.Count)
        return;
      this._bookmark = new CompositeBookmark(queryIndex, (object) null);
    }
    else
      this._bookmark = new CompositeBookmark(queryIndex - 1, this._subqueries[queryIndex - 1].Object.Bookmark);
  }

  public void Execute(NodeIDCollection nodeIDs)
  {
    if (nodeIDs == null || nodeIDs.Count <= 0)
      return;
    NodeIDCollection[] nodeIdCollectionArray = new NodeIDCollection[this._subqueries.Count];
    for (int index = 0; index < this._subqueries.Count; ++index)
      nodeIdCollectionArray[index] = new NodeIDCollection();
    for (int index1 = 0; index1 < nodeIDs.Count; ++index1)
    {
      for (int index2 = 0; index2 < this._subqueries.Count; ++index2)
      {
        int partId = ((PartCookie) nodeIDs[index1].Cookie).PartId;
        if (this._subqueries[index2].UniqueId == partId)
        {
          nodeIdCollectionArray[index2].Add(nodeIDs[index1]);
          break;
        }
      }
    }
    List<QuerySlot> querySlotList = new List<QuerySlot>();
    List<int> intList = new List<int>();
    for (int index = 0; index < this._subqueries.Count; ++index)
    {
      this._subqueries[index].Object.Execute(nodeIdCollectionArray[index]);
      if (this._subqueries[index].Object.RecordCount > 0)
      {
        querySlotList.Add(this._subqueries[index]);
        intList.Add(this._subqueries[index].Object.RecordCount);
      }
      this._count += this._subqueries[index].Object.RecordCount;
    }
    if (this._count <= 0)
      return;
    this._resultQueries = querySlotList;
    this._resultCounts = intList;
  }

  public object Bookmark
  {
    [DebuggerStepThrough] get => (object) this._bookmark;
  }

  public int RecordCount
  {
    [DebuggerStepThrough] get => this._count;
  }

  /// <summary>Условия выполнения запросов</summary>
  public NodeQueryOptions Options
  {
    [DebuggerStepThrough] get => this._options;
    [DebuggerStepThrough] set => this._options = value;
  }

  /// <summary>
  /// Возвращает количество всех элементов, которые могут быть получены с помощью данного запроса.
  /// Значение свойства будет определено только после первого пакетного чтения, при условии, что
  /// в опциях задан флажок ReceiveTotalRecordsCount. Иначе свойство будет равно значению RecordCount.
  /// </summary>
  public long TotalRecordCount
  {
    get
    {
      if (this._totalCounts == null || this._totalCounts.Count == 0)
        return 0;
      long totalRecordCount = 0;
      for (int index = 0; index < this._totalCounts.Count; ++index)
        totalRecordCount += this._totalCounts[index];
      return totalRecordCount;
    }
  }

  public INodeID GetRecordNodeID(int index)
  {
    if (index < 0 || index >= this._count)
      throw new IndexOutOfRangeException();
    int index1 = 0;
    int num;
    for (num = 0; this._resultQueries[index1].Object.RecordCount + num <= index; ++index1)
      num += this._resultQueries[index1].Object.RecordCount;
    INodeID recordNodeId = this._resultQueries[index1].Object.GetRecordNodeID(index - num);
    if (recordNodeId.Cookie == null)
      recordNodeId.Cookie = (object) new PartCookie();
    ((PartCookie) recordNodeId.Cookie).PartId = this._resultQueries[index1].UniqueId;
    return recordNodeId;
  }

  public object[] GetRecordValues(int index)
  {
    if (index < 0 || index >= this._count)
      throw new IndexOutOfRangeException();
    int index1 = 0;
    int num;
    for (num = 0; this._resultQueries[index1].Object.RecordCount + num <= index; ++index1)
      num += this._resultQueries[index1].Object.RecordCount;
    return this._resultQueries[index1].Object.GetRecordValues(index - num);
  }

  /// <summary>
  /// Возвращает исходные значения колонок дочернего элемента по его порядковому номеру.
  /// </summary>
  /// <param name="index">Порядковый номер дочернего элемента</param>
  /// <returns>Массив исходных значений колонок</returns>
  public object[] GetRawRecordValues(int index)
  {
    if (index < 0 || index >= this._count)
      throw new IndexOutOfRangeException();
    int index1 = 0;
    int num;
    for (num = 0; this._resultQueries[index1].Object.RecordCount + num <= index; ++index1)
      num += this._resultQueries[index1].Object.RecordCount;
    return this._resultQueries[index1].Object.GetRawRecordValues(index - num);
  }
}
