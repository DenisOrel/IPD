// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.BindingListView`1
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions;

public class BindingListView<T> : 
  IBindingListView,
  IBindingList,
  IList,
  ICollection,
  IEnumerable,
  ITypedList
{
  private IList _innerList;
  private ListSortDescriptionCollection _sortDescriptions;
  private int[] _sortIndices;
  private int[] _filterIndices;
  private DataTable _filterTable;
  private string _currentFilterExpression = string.Empty;
  private PropertyDescriptorCollection _properties;

  public BindingListView()
    : this((IList) new ArrayList())
  {
  }

  public BindingListView(IList list)
  {
    this._innerList = list;
    this.RemoveSort();
    this.InitializeFiltering();
  }

  public IList InnerList => this._innerList;

  public void RaiseListChanged()
  {
    this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
  }

  public void RaiseListChanged(ListChangedEventArgs args) => this.OnListChanged(args);

  protected virtual void OnListChanged(ListChangedEventArgs args)
  {
    ListChangedEventHandler listChanged = this.ListChanged;
    if (listChanged == null)
      return;
    listChanged((object) this, args);
  }

  private void InitializeFiltering()
  {
    this._properties = ListBindingHelper.GetListItemProperties((object) typeof (T));
    this._filterTable = new DataTable("FilterTable");
    foreach (PropertyDescriptor property in this._properties)
      this._filterTable.Columns.Add(property.Name, property.PropertyType);
  }

  public void ApplySort(ListSortDescriptionCollection sorts)
  {
    this._sortDescriptions = sorts;
    this._sortIndices = new int[this._innerList.Count];
    object[] keys = new object[this._innerList.Count];
    for (int index = 0; index < this._sortIndices.Length; ++index)
    {
      this._sortIndices[index] = index;
      keys[index] = this._innerList[index];
    }
    Array.Sort((Array) keys, (Array) this._sortIndices, (IComparer) new GenericComparer(sorts));
    this.Filter = this._currentFilterExpression;
  }

  public string Filter
  {
    get => this._currentFilterExpression;
    set
    {
      this._filterIndices = (int[]) null;
      this._currentFilterExpression = string.Empty;
      if (value.Length > 0)
      {
        DataFilter dataFilter = new DataFilter(value, this._filterTable);
        List<int> intList = new List<int>();
        int count1 = this.Count;
        int count2 = this._properties.Count;
        DataRow row = this._filterTable.NewRow();
        for (int index1 = 0; index1 < this.Count; ++index1)
        {
          object component = this[index1];
          for (int index2 = 0; index2 < count2; ++index2)
            row[index2] = this._properties[index2].GetValue(component);
          if (dataFilter.Invoke(row))
            intList.Add(index1);
        }
        this._filterIndices = intList.ToArray();
        this._currentFilterExpression = value;
      }
      this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
    }
  }

  public void RemoveFilter() => this.Filter = string.Empty;

  public ListSortDescriptionCollection SortDescriptions => this._sortDescriptions;

  public bool SupportsAdvancedSorting => true;

  public bool SupportsFiltering => true;

  public event ListChangedEventHandler ListChanged;

  public bool AllowEdit => true;

  public bool AllowNew => false;

  public bool AllowRemove => true;

  public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
  {
    this.ApplySort(new ListSortDescriptionCollection(new ListSortDescription[1]
    {
      new ListSortDescription(property, direction)
    }));
  }

  public bool IsSorted => this._sortDescriptions.Count > 0;

  public void RemoveSort()
  {
    this._sortDescriptions = new ListSortDescriptionCollection();
    this._sortIndices = (int[]) null;
  }

  public ListSortDirection SortDirection
  {
    get
    {
      return this._sortDescriptions.Count != 1 ? ListSortDirection.Ascending : this._sortDescriptions[0].SortDirection;
    }
  }

  public PropertyDescriptor SortProperty
  {
    get
    {
      return this._sortDescriptions.Count != 1 ? (PropertyDescriptor) null : this._sortDescriptions[0].PropertyDescriptor;
    }
  }

  public bool SupportsChangeNotification => true;

  public bool SupportsSearching => false;

  public bool SupportsSorting => true;

  public void AddIndex(PropertyDescriptor property)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public object AddNew() => throw new Exception("The method or operation is not implemented.");

  public int Find(PropertyDescriptor property, object key)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public void RemoveIndex(PropertyDescriptor property)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public int Add(object value)
  {
    int newIndex = value == null || typeof (T).IsAssignableFrom(value.GetType()) ? this._innerList.Add(value) : throw new ArgumentException("Given instance doesn't match needed type.");
    this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, newIndex));
    return newIndex;
  }

  public void Clear()
  {
    this._innerList.Clear();
    this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
  }

  public bool Contains(object value) => this._innerList.Contains(value);

  public int IndexOf(object value) => this._innerList.IndexOf(value);

  public void Insert(int index, object value)
  {
    if (value != null && !typeof (T).IsAssignableFrom(value.GetType()))
      throw new ArgumentException("Given instance doesn't match needed type.");
    this._innerList.Insert(index, value);
    this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
  }

  public bool IsFixedSize => this._innerList.IsFixedSize;

  public bool IsReadOnly => this._innerList.IsReadOnly;

  public void Remove(object value)
  {
    int newIndex = this.IndexOf(value);
    this._innerList.Remove(value);
    this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, newIndex));
  }

  public void RemoveAt(int index)
  {
    this._innerList.RemoveAt(index);
    this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
  }

  public object this[int index]
  {
    get
    {
      if (this._filterIndices != null)
        index = this._filterIndices[index];
      if (this._sortIndices != null && index < this._sortIndices.Length)
        index = this._sortIndices[index];
      return this._innerList[index];
    }
    set
    {
      this._innerList[index] = value;
      this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
    }
  }

  public void CopyTo(Array array, int index) => this._innerList.CopyTo(array, index);

  public int Count
  {
    get => this._filterIndices != null ? this._filterIndices.Length : this._innerList.Count;
  }

  public bool IsSynchronized => this._innerList.IsSynchronized;

  public object SyncRoot => this._innerList.SyncRoot;

  public IEnumerator GetEnumerator() => this._innerList.GetEnumerator();

  public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
  {
    return ListBindingHelper.GetListItemProperties((object) typeof (T));
  }

  public string GetListName(PropertyDescriptor[] listAccessors) => this.GetType().Name;
}
