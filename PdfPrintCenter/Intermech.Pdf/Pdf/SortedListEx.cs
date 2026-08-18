// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.SortedListEx
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf;

internal class SortedListEx : IDictionary, ICollection, IEnumerable, ICloneable
{
  private const int _defaultCapacity = 16 /*0x10*/;
  private int _size;
  private IComparer comparer;
  private SortedListEx.KeyList keyList;
  private object[] keys;
  private SortedListEx.ValueList valueList;
  private Dictionary<object, object> values;
  private int version;

  public SortedListEx()
  {
    this.keys = new object[16 /*0x10*/];
    this.values = new Dictionary<object, object>(16 /*0x10*/);
  }

  public SortedListEx(IComparer comparer)
    : this()
  {
    if (comparer == null)
      return;
    this.comparer = comparer;
  }

  public SortedListEx(IDictionary d)
    : this(d, (IComparer) null)
  {
  }

  public SortedListEx(int initialCapacity)
  {
    this.keys = initialCapacity >= 0 ? new object[initialCapacity] : throw new ArgumentOutOfRangeException(nameof (initialCapacity));
    this.values = new Dictionary<object, object>(initialCapacity);
  }

  public SortedListEx(IComparer comparer, int capacity)
    : this(comparer)
  {
    this.Capacity = capacity;
  }

  public SortedListEx(IDictionary d, IComparer comparer)
    : this(comparer, d != null ? d.Count : 0)
  {
    if (d == null)
      throw new ArgumentNullException(nameof (d));
    throw new NotImplementedException();
  }

  public virtual void Add(object key, object value)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    if (this.values.ContainsKey(key))
      throw new ArgumentException("Duplicated");
    this.Insert(~Array.BinarySearch((Array) this.keys, 0, this._size, key, this.comparer), key, value);
  }

  public virtual void Clear()
  {
    ++this.version;
    this._size = 0;
    this.keys = new object[16 /*0x10*/];
    this.values = new Dictionary<object, object>(16 /*0x10*/);
  }

  public virtual object Clone()
  {
    SortedListEx sortedListEx = new SortedListEx(this._size);
    Array.Copy((Array) this.keys, 0, (Array) sortedListEx.keys, 0, this._size);
    sortedListEx.values = new Dictionary<object, object>((IDictionary<object, object>) this.values);
    sortedListEx._size = this._size;
    sortedListEx.version = this.version;
    sortedListEx.comparer = this.comparer;
    return (object) sortedListEx;
  }

  public SortedListEx CloneAll()
  {
    int count = this.Count;
    SortedListEx sortedListEx = new SortedListEx(count + 1);
    for (int index = 0; index < count; ++index)
    {
      object obj = ((ICloneable) this.GetByIndex(index)).Clone();
      sortedListEx.Add(this.GetKey(index), obj);
    }
    return sortedListEx;
  }

  public virtual bool Contains(object key) => this.values.ContainsKey(key);

  public virtual bool ContainsKey(object key) => this.values.ContainsKey(key);

  public virtual bool ContainsValue(object value) => this.values.ContainsValue(value);

  public virtual void CopyTo(Array array, int arrayIndex)
  {
    if (array == null)
      throw new ArgumentNullException(nameof (array));
    if (array.Rank != 1)
      throw new ArgumentException();
    if (arrayIndex < 0)
      throw new ArgumentOutOfRangeException(nameof (arrayIndex));
    if (array.Length - arrayIndex < this.Count)
      throw new ArgumentException();
    for (int index = 0; index < this.Count; ++index)
    {
      DictionaryEntry dictionaryEntry = new DictionaryEntry(this.keys[index], this.values[this.keys[index]]);
      array.SetValue((object) dictionaryEntry, index + arrayIndex);
    }
  }

  private void EnsureCapacity(int min)
  {
    int num = this.keys.Length == 0 ? 16 /*0x10*/ : this.keys.Length * 2;
    if (num < min)
      num = min;
    this.Capacity = num;
  }

  public virtual object GetByIndex(int index)
  {
    if (index < 0 || index >= this._size)
      throw new ArgumentOutOfRangeException(nameof (index));
    return this.values[this.keys[index]];
  }

  public virtual IDictionaryEnumerator GetEnumerator()
  {
    return (IDictionaryEnumerator) new SortedListEx.SortedListExEnumerator(this, 0, this._size, 3);
  }

  public virtual object GetKey(int index)
  {
    return index >= 0 && index < this._size ? this.keys[index] : throw new ArgumentOutOfRangeException(nameof (index));
  }

  public virtual IList GetKeyList()
  {
    if (this.keyList == null)
      this.keyList = new SortedListEx.KeyList(this);
    return (IList) this.keyList;
  }

  public virtual IList GetValueList()
  {
    if (this.valueList == null)
      this.valueList = new SortedListEx.ValueList(this);
    else
      this.valueList.UpdateValues();
    return (IList) this.valueList;
  }

  public virtual int IndexOfKey(object key)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    int num = Array.BinarySearch((Array) this.keys, 0, this._size, key, this.comparer);
    return num < 0 ? -1 : num;
  }

  public virtual int IndexOfValue(object value)
  {
    object obj = (object) null;
    IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this.values.GetEnumerator();
    enumerator.Reset();
    while (enumerator.MoveNext())
    {
      if (enumerator.Value.Equals(value))
      {
        obj = enumerator.Key;
        break;
      }
    }
    return obj == null ? -1 : Array.IndexOf<object>(this.keys, obj, 0, this._size);
  }

  private void Insert(int index, object key, object value)
  {
    if (this._size == this.keys.Length)
      this.EnsureCapacity(this._size + 1);
    if (index < this._size)
      Array.Copy((Array) this.keys, index, (Array) this.keys, index + 1, this._size - index);
    this.keys[index] = key;
    this.values[key] = value;
    ++this._size;
    ++this.version;
  }

  public virtual void Remove(object key)
  {
    int index = this.IndexOfKey(key);
    if (index < 0)
      return;
    this.RemoveAt(index);
  }

  public virtual void RemoveAt(int index)
  {
    if (index < 0 || index >= this._size)
      throw new ArgumentOutOfRangeException(nameof (index));
    --this._size;
    object key = this.keys[index];
    if (index < this._size)
      Array.Copy((Array) this.keys, index + 1, (Array) this.keys, index, this._size - index);
    this.keys[this._size] = (object) null;
    this.values.Remove(key);
    ++this.version;
  }

  public virtual void SetByIndex(int index, object value)
  {
    if (index < 0 || index >= this._size)
      throw new ArgumentOutOfRangeException(nameof (index));
    this.values[this.keys[index]] = value;
    ++this.version;
  }

  public static SortedListEx Synchronized(SortedListEx list)
  {
    return list != null ? (SortedListEx) new SortedListEx.SyncSortedListEx(list) : throw new ArgumentNullException(nameof (list));
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) new SortedListEx.SortedListExEnumerator(this, 0, this._size, 3);
  }

  public virtual void TrimToSize() => this.Capacity = this._size;

  public virtual int Capacity
  {
    get => this.keys.Length;
    set
    {
      if (value == this.keys.Length)
        return;
      if (value < this._size)
        throw new ArgumentOutOfRangeException(nameof (value));
      if (value > 0)
      {
        object[] destinationArray = new object[value];
        if (this._size > 0)
          Array.Copy((Array) this.keys, 0, (Array) destinationArray, 0, this._size);
        this.keys = destinationArray;
      }
      else
        this.keys = new object[16 /*0x10*/];
    }
  }

  public virtual int Count => this._size;

  public virtual bool IsFixedSize => false;

  public virtual bool IsReadOnly => false;

  public virtual bool IsSynchronized => false;

  public virtual object this[object key]
  {
    get => this.values[key];
    set
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (this.values.ContainsKey(key))
        this.values[key] = value;
      else
        this.Add(key, value);
      ++this.version;
    }
  }

  public virtual ICollection Keys => (ICollection) this.GetKeyList();

  public virtual object SyncRoot => (object) this;

  public virtual ICollection Values => (ICollection) this.GetValueList();

  private class KeyList : IList, ICollection, IEnumerable
  {
    private SortedListEx SortedListEx;

    internal KeyList(SortedListEx SortedListEx) => this.SortedListEx = SortedListEx;

    public virtual int Add(object key) => throw new NotSupportedException();

    public virtual void Clear() => throw new NotSupportedException();

    public virtual bool Contains(object key) => this.SortedListEx.Contains(key);

    public virtual void CopyTo(Array array, int arrayIndex)
    {
      if (array != null && array.Rank != 1)
        throw new ArgumentException();
      Array.Copy((Array) this.SortedListEx.keys, 0, array, arrayIndex, this.SortedListEx.Count);
    }

    public virtual IEnumerator GetEnumerator()
    {
      return (IEnumerator) new SortedListEx.SortedListExEnumerator(this.SortedListEx, 0, this.SortedListEx.Count, 1);
    }

    public virtual int IndexOf(object key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      int num = Array.BinarySearch((Array) this.SortedListEx.keys, 0, this.SortedListEx.Count, key, this.SortedListEx.comparer);
      return num >= 0 ? num : -1;
    }

    public virtual void Insert(int index, object value) => throw new NotSupportedException();

    public virtual void Remove(object key) => throw new NotSupportedException();

    public virtual void RemoveAt(int index) => throw new NotSupportedException();

    public virtual int Count => this.SortedListEx._size;

    public virtual bool IsFixedSize => true;

    public virtual bool IsReadOnly => true;

    public virtual bool IsSynchronized => this.SortedListEx.IsSynchronized;

    public virtual object this[int index]
    {
      get => this.SortedListEx.GetKey(index);
      set => throw new NotSupportedException();
    }

    public virtual object SyncRoot => this.SortedListEx.SyncRoot;
  }

  private class SortedListExEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
  {
    private bool current;
    internal const int DictEntry = 3;
    private int endIndex;
    private int getObjectRetType;
    private int index;
    private object key;
    internal const int Keys = 1;
    private SortedListEx SortedListEx;
    private int startIndex;
    private object value;
    internal const int Values = 2;
    private int version;

    internal SortedListExEnumerator(
      SortedListEx SortedListEx,
      int index,
      int count,
      int getObjRetType)
    {
      this.SortedListEx = SortedListEx;
      this.index = index;
      this.startIndex = index;
      this.endIndex = index + count;
      this.version = SortedListEx.version;
      this.getObjectRetType = getObjRetType;
      this.current = false;
    }

    public object Clone() => this.MemberwiseClone();

    public virtual bool MoveNext()
    {
      if (this.version != this.SortedListEx.version)
        throw new InvalidOperationException();
      if (this.index < this.endIndex)
      {
        this.key = this.SortedListEx.keys[this.index];
        this.value = this.SortedListEx.values[this.key];
        ++this.index;
        this.current = true;
        return true;
      }
      this.key = (object) null;
      this.value = (object) null;
      this.current = false;
      return false;
    }

    public virtual void Reset()
    {
      if (this.version != this.SortedListEx.version)
        throw new InvalidOperationException();
      this.index = this.startIndex;
      this.current = false;
      this.key = (object) null;
      this.value = (object) null;
    }

    public virtual object Current
    {
      get
      {
        if (!this.current)
          throw new InvalidOperationException();
        if (this.getObjectRetType == 1)
          return this.key;
        return this.getObjectRetType == 2 ? this.value : (object) new DictionaryEntry(this.key, this.value);
      }
    }

    public virtual DictionaryEntry Entry
    {
      get
      {
        if (this.version != this.SortedListEx.version)
          throw new InvalidOperationException();
        if (!this.current)
          throw new InvalidOperationException();
        return new DictionaryEntry(this.key, this.value);
      }
    }

    public virtual object Key
    {
      get
      {
        if (this.version != this.SortedListEx.version)
          throw new InvalidOperationException();
        if (!this.current)
          throw new InvalidOperationException();
        return this.key;
      }
    }

    public virtual object Value
    {
      get
      {
        if (this.version != this.SortedListEx.version)
          throw new InvalidOperationException();
        if (!this.current)
          throw new InvalidOperationException();
        return this.value;
      }
    }
  }

  private class SyncSortedListEx : SortedListEx
  {
    private SortedListEx _list;
    private object _root;

    internal SyncSortedListEx(SortedListEx list)
    {
      this._list = list;
      this._root = list.SyncRoot;
    }

    public override void Add(object key, object value)
    {
      lock (this._root)
        this._list.Add(key, value);
    }

    public override void Clear()
    {
      lock (this._root)
        this._list.Clear();
    }

    public override object Clone()
    {
      lock (this._root)
        return this._list.Clone();
    }

    public override bool Contains(object key)
    {
      lock (this._root)
        return this._list.Contains(key);
    }

    public override bool ContainsKey(object key)
    {
      lock (this._root)
        return this._list.ContainsKey(key);
    }

    public override bool ContainsValue(object value)
    {
      lock (this._root)
        return this._list.ContainsValue(value);
    }

    public override void CopyTo(Array array, int index)
    {
      lock (this._root)
        this._list.CopyTo(array, index);
    }

    public override object GetByIndex(int index)
    {
      lock (this._root)
        return this._list.GetByIndex(index);
    }

    public override IDictionaryEnumerator GetEnumerator()
    {
      lock (this._root)
        return this._list.GetEnumerator();
    }

    public override object GetKey(int index)
    {
      lock (this._root)
        return this._list.GetKey(index);
    }

    public override IList GetKeyList()
    {
      lock (this._root)
        return this._list.GetKeyList();
    }

    public override IList GetValueList()
    {
      lock (this._root)
        return this._list.GetValueList();
    }

    public override int IndexOfKey(object key)
    {
      lock (this._root)
        return this._list.IndexOfKey(key);
    }

    public override int IndexOfValue(object value)
    {
      lock (this._root)
        return this._list.IndexOfValue(value);
    }

    public override void Remove(object key)
    {
      lock (this._root)
        this._list.Remove(key);
    }

    public override void RemoveAt(int index)
    {
      lock (this._root)
        this._list.RemoveAt(index);
    }

    public override void SetByIndex(int index, object value)
    {
      lock (this._root)
        this._list.SetByIndex(index, value);
    }

    public override void TrimToSize()
    {
      lock (this._root)
        this._list.TrimToSize();
    }

    public override int Capacity
    {
      get
      {
        lock (this._root)
          return this._list.Capacity;
      }
    }

    public override int Count
    {
      get
      {
        lock (this._root)
          return this._list.Count;
      }
    }

    public override bool IsFixedSize => this._list.IsFixedSize;

    public override bool IsReadOnly => this._list.IsReadOnly;

    public override bool IsSynchronized => true;

    public override object this[object key]
    {
      get
      {
        lock (this._root)
          return this._list[key];
      }
      set
      {
        lock (this._root)
          this._list[key] = value;
      }
    }

    public override object SyncRoot => this._root;
  }

  private class ValueList : IList, ICollection, IEnumerable
  {
    private SortedListEx SortedListEx;
    private Array vals;

    internal ValueList(SortedListEx SortedListEx)
    {
      this.SortedListEx = SortedListEx;
      this.UpdateValues();
    }

    public virtual int Add(object key) => throw new NotSupportedException();

    public virtual void Clear() => throw new NotSupportedException();

    public virtual bool Contains(object value) => this.SortedListEx.ContainsValue(value);

    public virtual void CopyTo(Array array, int arrayIndex)
    {
      if (array != null && array.Rank != 1)
        throw new ArgumentException();
      Array.Copy(this.vals, 0, array, arrayIndex, this.SortedListEx.Count);
    }

    public virtual IEnumerator GetEnumerator()
    {
      return (IEnumerator) new SortedListEx.SortedListExEnumerator(this.SortedListEx, 0, this.SortedListEx.Count, 2);
    }

    public virtual int IndexOf(object value)
    {
      return Array.IndexOf(this.vals, value, 0, this.SortedListEx.Count);
    }

    public virtual void Insert(int index, object value) => throw new NotSupportedException();

    public virtual void Remove(object value) => throw new NotSupportedException();

    public virtual void RemoveAt(int index) => throw new NotSupportedException();

    public virtual void UpdateValues()
    {
      this.vals = (Array) new object[this.SortedListEx.Count];
      throw new NotImplementedException();
    }

    public virtual int Count => this.SortedListEx._size;

    public virtual bool IsFixedSize => true;

    public virtual bool IsReadOnly => true;

    public virtual bool IsSynchronized => this.SortedListEx.IsSynchronized;

    public virtual object this[int index]
    {
      get => this.SortedListEx.GetByIndex(index);
      set => this.SortedListEx.SetByIndex(index, value);
    }

    public virtual object SyncRoot => this.SortedListEx.SyncRoot;
  }
}
