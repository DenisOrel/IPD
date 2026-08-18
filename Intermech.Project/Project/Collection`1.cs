// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Collection`1
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class Collection<T> : BindingList<T>, ITypedList, IDeserializationCallback where T : Entity
{
  [NotNull]
  private Type _entityType = typeof (T);
  [NonSerialized]
  private bool _isSorted;
  [NonSerialized]
  private bool _raiseItemChangeEvents;
  [NonSerialized]
  private bool _raiseItemEvents;
  [NonSerialized]
  private ListSortDirection _sortDirection;
  [CanBeNull]
  [NonSerialized]
  private PropertyDescriptor _sortProperty;

  [field: NonSerialized]
  public event EventHandler<ItemEventArgs<T>> ItemAdded;

  [field: NonSerialized]
  public event EventHandler<ItemEventArgs<T>> ItemAdding;

  [field: NonSerialized]
  public event EventHandler<ItemEventArgs<T>> ItemRemoved;

  [field: NonSerialized]
  public event EventHandler<ItemEventArgs<T>> ItemRemoving;

  public Collection()
    : this(false)
  {
  }

  public Collection(bool calcIndexes)
  {
    this.CalcIndexes = calcIndexes;
    this.InitializeClear();
  }

  [NotNull]
  protected override object AddNewCore()
  {
    T obj = this.NewElement();
    this.AddNewCoreInit(obj);
    this.Add(obj);
    return (object) obj;
  }

  protected virtual void AddNewCoreInit([NotNull] T item)
  {
  }

  protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
  {
    if (this.Items is List<T> items)
    {
      Collection<T>.PropertyComparer<T> propertyComparer = new Collection<T>.PropertyComparer<T>(prop, direction);
      items.Sort((IComparer<T>) propertyComparer);
      this._isSorted = true;
    }
    else
      this._isSorted = false;
    this._sortProperty = prop;
    this._sortDirection = direction;
    this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
  }

  protected override void ClearItems()
  {
    List<T> objList = new List<T>((IEnumerable<T>) this);
    foreach (T obj in objList)
      this.OnItemRemoving(obj);
    base.ClearItems();
    foreach (T obj in objList)
      this.OnItemRemoved(obj);
  }

  protected override int FindCore([CanBeNull] PropertyDescriptor prop, [CanBeNull] object key)
  {
    if (prop != null && key is IComparable comparable1)
    {
      foreach (T component in this.Items as List<T>)
      {
        IComparable comparable = prop.GetValue((object) component) as IComparable;
        if (comparable1.CompareTo((object) comparable) == 0)
          return this.IndexOf(component);
      }
    }
    return -1;
  }

  [CanBeNull]
  public PropertyDescriptorCollection GetItemProperties([CanBeNull] PropertyDescriptor[] listAccessors)
  {
    return listAccessors != null && listAccessors.Length != 0 ? (PropertyDescriptorCollection) null : TypeDescriptor.GetProperties(this.EntityType);
  }

  [CanBeNull]
  public string GetListName([CanBeNull] PropertyDescriptor[] listAccessors)
  {
    return listAccessors != null && listAccessors.Length != 0 ? (string) null : this.EntityType.Name;
  }

  protected virtual void Initialize()
  {
    this._raiseItemEvents = true;
    this._raiseItemChangeEvents = true;
  }

  private void InitializeClear() => this.Initialize();

  protected override void InsertItem(int index, [NotNull] T item)
  {
    this.OnItemAdding(index, item);
    base.InsertItem(index, item);
    this.OnItemAdded(item);
  }

  protected override void OnListChanged([NotNull] ListChangedEventArgs e)
  {
    if (this.CalcIndexes)
    {
      if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted)
        this.RecalcIndexes(e.NewIndex);
      else if (e.ListChangedType == ListChangedType.ItemMoved)
        this.RecalcIndexes(Math.Min(e.OldIndex, e.NewIndex));
    }
    base.OnListChanged(e);
  }

  [NotNull]
  public T NewElement()
  {
    return (T) Activator.CreateInstance(this.EntityType ?? throw new NullReferenceException("EntityType"));
  }

  public void OnDeserialization([CanBeNull] object sender) => this.Initialize();

  protected virtual void OnItemAdded([NotNull] T item)
  {
    if (!this.RaiseItemEvents)
      return;
    EventHandler<ItemEventArgs<T>> itemAdded = this.ItemAdded;
    if (itemAdded == null)
      return;
    itemAdded((object) this, new ItemEventArgs<T>(item));
  }

  protected void OnItemAdding(int index, [NotNull] T item)
  {
    if (!this.RaiseItemEvents)
      return;
    EventHandler<ItemEventArgs<T>> itemAdding = this.ItemAdding;
    if (itemAdding == null)
      return;
    itemAdding((object) this, new ItemEventArgs<T>(item, index));
  }

  protected virtual void OnItemRemoved([NotNull] T item)
  {
    if (!this.RaiseItemEvents)
      return;
    EventHandler<ItemEventArgs<T>> itemRemoved = this.ItemRemoved;
    if (itemRemoved == null)
      return;
    itemRemoved((object) this, new ItemEventArgs<T>(item));
  }

  protected virtual void OnItemRemoving([NotNull] T item)
  {
    if (!this.RaiseItemEvents)
      return;
    EventHandler<ItemEventArgs<T>> itemRemoving = this.ItemRemoving;
    if (itemRemoving == null)
      return;
    itemRemoving((object) this, new ItemEventArgs<T>(item));
  }

  protected override void RemoveItem(int index)
  {
    T obj = this[index];
    this.OnItemRemoving(obj);
    base.RemoveItem(index);
    this.OnItemRemoved(obj);
  }

  protected override void RemoveSortCore()
  {
    this._isSorted = false;
    this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
  }

  protected override void SetItem(int index, T item)
  {
    T obj = this[index];
    this.OnItemRemoving(obj);
    this.OnItemAdding(index, item);
    base.SetItem(index, item);
    if (this.CalcIndexes)
      this.RecalcIndexes(index);
    this.OnItemRemoved(obj);
    this.OnItemAdded(item);
  }

  [NotNull]
  public Type EntityType
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._entityType;
    set
    {
      if (!(value != this.EntityType))
        return;
      this._entityType = value.IsSubclassOf(typeof (T)) ? value : throw new ArgumentException("Cannot set ElementType: it must be derived from the base collection type.");
    }
  }

  protected override bool IsSortedCore
  {
    [DebuggerStepThrough] get => this._isSorted;
  }

  public bool RaiseItemChangeEvents
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._raiseItemChangeEvents;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      if (value == this.RaiseItemChangeEvents)
        return;
      this._raiseItemChangeEvents = value;
    }
  }

  public bool RaiseItemEvents
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._raiseItemEvents;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      if (value == this.RaiseItemEvents)
        return;
      this._raiseItemEvents = value;
    }
  }

  protected override ListSortDirection SortDirectionCore
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._sortDirection;
    }
  }

  [CanBeNull]
  protected override PropertyDescriptor SortPropertyCore
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._sortProperty;
    }
  }

  protected override bool SupportsSearchingCore
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => true;
  }

  protected override bool SupportsSortingCore
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => true;
  }

  internal void RaiseListChanged([NotNull] ListChangedEventArgs e) => this.OnListChanged(e);

  /// <summary>Устанавливать Entity.Index при обновлении списка, или нет</summary>
  public bool CalcIndexes { get; }

  public void RecalcIndexes() => this.RecalcIndexes(0);

  private void RecalcIndexes(int fromIndex)
  {
    for (int index = fromIndex; index < this.Count; ++index)
      this[index].Index = index;
  }

  public new void Add([NotNull] T item)
  {
    base.Add(item);
    if (!this.CalcIndexes)
      return;
    item.Index = this.Count - 1;
  }

  public void AddRange([NotNull, ItemNotNull] IEnumerable<T> items)
  {
    foreach (T obj in items)
      this.Add(obj);
  }

  public void SafeAdd([NotNull] T item)
  {
    if (this.Contains(item))
      return;
    this.Add(item);
  }

  public void SafeAddRange([NotNull, ItemNotNull] IEnumerable<T> items)
  {
    foreach (T obj in items)
    {
      if (!this.Contains(obj))
        this.Add(obj);
    }
  }

  internal class PropertyComparer<U> : IComparer<U>
  {
    private readonly ListSortDirection _direction;
    [NotNull]
    private readonly PropertyDescriptor _property;
    private bool? _referenceType;

    public PropertyComparer([NotNull] PropertyDescriptor property, ListSortDirection direction)
    {
      this._property = property;
      this._direction = direction;
    }

    private bool ReferenceType
    {
      get => this._referenceType ?? (this._referenceType = new bool?(typeof (U).IsByRef)).Value;
    }

    public int Compare([CanBeNull] U xWord, [CanBeNull] U yWord)
    {
      if (this.ReferenceType)
      {
        if ((object) xWord == (object) yWord)
          return 0;
        if ((object) yWord == null)
          return 1;
        if ((object) xWord == null)
          return -1;
      }
      object propertyValue1 = Collection<T>.PropertyComparer<U>.GetPropertyValue(xWord, this._property.Name);
      object propertyValue2 = Collection<T>.PropertyComparer<U>.GetPropertyValue(yWord, this._property.Name);
      return this._direction != ListSortDirection.Ascending ? Collection<T>.PropertyComparer<U>.CompareDescending(propertyValue1, propertyValue2) : Collection<T>.PropertyComparer<U>.CompareAscending(propertyValue1, propertyValue2);
    }

    private static int CompareAscending([CanBeNull] object xValue, [CanBeNull] object yValue)
    {
      if (xValue is IComparable comparable)
        return comparable.CompareTo(yValue);
      if (xValue == yValue || xValue != null && xValue.Equals(yValue))
        return 0;
      return xValue == null ? -1 : string.Compare(xValue.ToString(), yValue?.ToString() ?? string.Empty, StringComparison.Ordinal);
    }

    private static int CompareDescending([CanBeNull] object xValue, [CanBeNull] object yValue)
    {
      return Collection<T>.PropertyComparer<U>.CompareAscending(xValue, yValue) * -1;
    }

    public bool Equals([CanBeNull] U xWord, [CanBeNull] U yWord)
    {
      if (this.ReferenceType)
      {
        if ((object) xWord == (object) yWord)
          return true;
        if ((object) yWord == null || (object) xWord == null)
          return false;
      }
      return xWord.Equals((object) yWord);
    }

    public int GetHashCode([NotNull] U obj) => obj.GetHashCode();

    [NotNull]
    private static object GetPropertyValue([NotNull] U value, [NotNull] string property)
    {
      return value.GetType().GetProperty(property).GetValue((object) value, (object[]) null);
    }
  }
}
