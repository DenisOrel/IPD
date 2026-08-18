
// Type: Intermech.Navigator.Parts.DescriptorsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.Parts;

public class DescriptorsQuery : INodeQuery
{
  private DescriptorCollection _descriptors;
  protected bool _sortedQuery;
  protected NodeColumnCollection _columns;
  private IList _transforms;
  private DescriptorsQuery.ResultRowCollection _rows;
  private NodeQueryOptions _options;
  private long _totalCount;
  private object _nextBookmark;
  private bool _locked;

  public DescriptorsQuery(DescriptorCollection descriptors, bool sortedQuery)
  {
    this._descriptors = descriptors;
    this._sortedQuery = sortedQuery;
    this._columns = new NodeColumnCollection();
    this._transforms = (IList) new ArrayList();
    this._options = NodeQueryOptions.None;
    this._totalCount = 0L;
    this._nextBookmark = (object) null;
    this._rows = new DescriptorsQuery.ResultRowCollection();
    this._locked = false;
  }

  public void AddColumn(NodeColumn column, INodeColumnTransform transform)
  {
    this._columns.Add(column);
    this._transforms.Add((object) transform);
  }

  public virtual void Execute(object bookmark, int count)
  {
    try
    {
      if (count <= 0)
        return;
      for (int index = 0; index < this._descriptors.Count; ++index)
      {
        INodeID recordNodeId = this._descriptors[index].GetRecordNodeID();
        if (recordNodeId != null)
        {
          recordNodeId.Cookie = (object) new DescriptorCookie(this._descriptors.GetUniqueId(index));
          DescriptorsQuery.ResultRow row = new DescriptorsQuery.ResultRow();
          this.QueryRow(row, this._descriptors[index], recordNodeId);
          this._rows.Add(row);
        }
      }
      if (this._sortedQuery)
        this._rows.Sort((IComparer) new DescriptorsQuery.ResultRowComparer(this._columns));
      if (bookmark != null)
      {
        while (this._rows.Count > 0 && !((DescriptorCookie) this._rows[0].NodeID.Cookie).DescriptorId.Equals(bookmark))
          this._rows.RemoveAt(0);
      }
      this._nextBookmark = count < this._rows.Count ? (object) ((DescriptorCookie) this._rows[count].NodeID.Cookie).DescriptorId : (object) null;
      while (this._rows.Count > count)
        this._rows.RemoveAt(this._rows.Count - 1);
    }
    finally
    {
      this._locked = true;
    }
  }

  public void Execute(NodeIDCollection nodeIDs)
  {
    try
    {
      if (nodeIDs == null || nodeIDs.Count <= 0)
        return;
      for (int index = 0; index < nodeIDs.Count; ++index)
      {
        IDescriptor descriptor = this.GetDescriptor(nodeIDs[index]);
        if (descriptor != null)
        {
          INodeID recordNodeId = descriptor.GetRecordNodeID();
          if (recordNodeId != null)
          {
            recordNodeId.Cookie = (object) new DescriptorCookie(this._descriptors.GetUniqueId(descriptor));
            DescriptorsQuery.ResultRow row = new DescriptorsQuery.ResultRow();
            this.QueryRow(row, descriptor, recordNodeId);
            this._rows.Add(row);
          }
        }
      }
    }
    finally
    {
      this._locked = true;
    }
  }

  public object Bookmark => this._nextBookmark;

  public int RecordCount => this._rows.Count;

  /// <summary>Условия выполнения запросов</summary>
  public NodeQueryOptions Options
  {
    get => this._options;
    set => this._options = value;
  }

  /// <summary>
  /// Возвращает количество всех элементов, которые могут быть получены с помощью данного запроса.
  /// Значение свойства будет определено только после первого пакетного чтения, при условии, что
  /// в опциях задан флажок ReceiveTotalRecordsCount. Иначе свойство будет равно значению RecordCount.
  /// </summary>
  public long TotalRecordCount => this._totalCount;

  public INodeID GetRecordNodeID(int index)
  {
    if (this._locked && index >= 0 && index < this._rows.Count)
      return this._rows[index].NodeID;
    throw new IndexOutOfRangeException();
  }

  public object[] GetRecordValues(int index)
  {
    if (this._locked && index >= 0 && index < this._rows.Count)
      return this._rows[index].Values;
    throw new IndexOutOfRangeException();
  }

  /// <summary>
  /// Возвращает исходные значения колонок дочернего элемента по его порядковому номеру.
  /// </summary>
  /// <param name="index">Порядковый номер дочернего элемента</param>
  /// <returns>Массив исходных значений колонок</returns>
  public object[] GetRawRecordValues(int index)
  {
    if (this._locked && index >= 0 && index < this._rows.Count)
      return this._rows[index].RawValues;
    throw new IndexOutOfRangeException();
  }

  private void QueryRow(DescriptorsQuery.ResultRow row, IDescriptor descriptor, INodeID nodeID)
  {
    for (int index = 0; index < this._columns.Count; ++index)
      row.Mapping.RegisterColumn(this._columns[index], descriptor.MapColumnToField(this._columns[index]), (INodeColumnTransform) this._transforms[index]);
    if (descriptor is ISpecialFieldsSupported specialFieldsSupported)
    {
      List<object> specialFields = specialFieldsSupported.GetSpecialFields();
      if (specialFields != null)
      {
        foreach (object field in specialFields)
          row.Mapping.RegisterSpecialField(field);
      }
    }
    object[] recordValues = row.Mapping.Fields != null ? descriptor.GetRecordValues(nodeID, row.Mapping.Fields) : (object[]) null;
    if (recordValues != null)
    {
      RecordAdapter recordAdapter = new RecordAdapter(row.Mapping, row.Mapping.Fields);
      row.Setup(nodeID, recordAdapter.GetRawRecordValues(recordValues), recordAdapter.GetRecordValues(recordValues));
    }
    else
    {
      object[] objArray = new object[row.Mapping.Count];
      row.Setup(nodeID, objArray, objArray);
    }
  }

  private IDescriptor GetDescriptor(INodeID nodeID)
  {
    return this.GetDescriptor(((DescriptorCookie) nodeID.Cookie).DescriptorId);
  }

  private IDescriptor GetDescriptor(int uniqueId) => this._descriptors.FindDescriptor(uniqueId);

  /// <summary>
  /// Описывает строку таблицы результатов, которя содержит данные, полученные
  /// от каждого дескриптора.
  /// </summary>
  private class ResultRow
  {
    private RecordMapping _mapping;
    private INodeID _nodeID;
    private object[] _rawValues;
    private object[] _values;

    public ResultRow()
    {
      this._mapping = new RecordMapping();
      this._nodeID = (INodeID) null;
      this._rawValues = (object[]) null;
      this._values = (object[]) null;
    }

    public void Setup(INodeID nodeID, object[] rawFields, object[] values)
    {
      this._nodeID = nodeID;
      this._rawValues = rawFields;
      this._values = values;
    }

    public RecordMapping Mapping => this._mapping;

    public INodeID NodeID => this._nodeID;

    public object[] RawValues => this._rawValues;

    public object[] Values => this._values;
  }

  private class ResultRowCollection : CollectionBase
  {
    public void Add(DescriptorsQuery.ResultRow row) => this.List.Add((object) row);

    public void Insert(int index, DescriptorsQuery.ResultRow row)
    {
      this.List.Insert(index, (object) row);
    }

    public void Remove(DescriptorsQuery.ResultRow row) => this.List.Remove((object) row);

    public int IndexOf(DescriptorsQuery.ResultRow row) => this.List.IndexOf((object) row);

    public DescriptorsQuery.ResultRow this[int index]
    {
      get => (DescriptorsQuery.ResultRow) this.List[index];
      set => this.List[index] = (object) value;
    }

    public void Sort(IComparer comparer) => this.InnerList.Sort(comparer);
  }

  /// <summary>
  /// Используется для сортировки результатов выполнения запроса к
  /// коллекции дескрипторов.
  /// </summary>
  private class ResultRowComparer : IComparer
  {
    private NodeColumnCollection _columns;

    /// <summary>
    /// Создает объект, реализующий алгоритм сравнения результатов
    /// запроса в соответствии с порядками сортировки, указанными в
    /// коллекции виртуальных колонок.
    /// </summary>
    /// <param name="columns">Коллекция виртуальных колонок</param>
    public ResultRowComparer(NodeColumnCollection columns) => this._columns = columns;

    public int Compare(object x, object y)
    {
      object[] rawValues1 = ((DescriptorsQuery.ResultRow) x).RawValues;
      object[] rawValues2 = ((DescriptorsQuery.ResultRow) y).RawValues;
      for (int index = 0; index < this._columns.Count; ++index)
      {
        if (this._columns[index].SortOrder != NodeColumnSortOrder.None)
        {
          int num = Comparer.Default.Compare(rawValues1[index], rawValues2[index]);
          if (this._columns[index].SortOrder == NodeColumnSortOrder.Descending)
            num = -num;
          if (num != 0)
            return num;
        }
      }
      return 0;
    }
  }
}
