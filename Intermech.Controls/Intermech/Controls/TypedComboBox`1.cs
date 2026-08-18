
// Type: Intermech.Controls.TypedComboBox`1
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Controls;

public abstract class TypedComboBox<TItemType> : ComboBoxAdv
{
  protected TypedComboBox()
  {
    this.Items = new TypedComboBox<TItemType>.ObjectCollectionTypedWrapper(this);
    this.DropDownStyle = ComboBoxStyle.DropDownList;
  }

  [NotNull]
  [ItemCanBeNull]
  public ComboBox.ObjectCollection ObjectCollectionLink
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => base.Items;
  }

  /// <summary>Gets an object representing the collection of the items contained in this System.Windows.Forms.ComboBox</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [Bindable(true)]
  [NotNull]
  [ItemNotNull]
  public TypedComboBox<TItemType>.ObjectCollectionTypedWrapper Items { get; }

  /// <summary>Gets or sets currently selected item in the System.Windows.Forms.ComboBox</summary>
  [Bindable(true)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public TItemType SelectedItem
  {
    get => (TItemType) base.SelectedItem;
    set => this.SelectedItem = (object) value;
  }

  protected override bool GetItemCaption([NotNull] object item, out string caption)
  {
    return this.GetItemCaption((TItemType) item, out caption);
  }

  protected override bool GetItemRemarks([NotNull] object item, out string remarks)
  {
    return this.GetItemRemarks((TItemType) item, out remarks);
  }

  protected override bool GetItemIcon([NotNull] object item, out Icon icon)
  {
    return this.GetItemIcon((TItemType) item, out icon);
  }

  protected override bool GetItemImage([NotNull] object item, out Image image)
  {
    return this.GetItemImage((TItemType) item, out image);
  }

  [ContractAnnotation("=> true, caption: notnull; => false, caption: null")]
  protected virtual bool GetItemCaption([NotNull] TItemType item, out string caption)
  {
    return base.GetItemCaption((object) item, out caption);
  }

  [ContractAnnotation("=> true, remarks: notnull; => false, remarks: null")]
  protected virtual bool GetItemRemarks([NotNull] TItemType item, out string remarks)
  {
    return base.GetItemRemarks((object) item, out remarks);
  }

  [ContractAnnotation("=> true, icon: notnull; => false, icon: null")]
  protected virtual bool GetItemIcon([NotNull] TItemType item, out Icon icon)
  {
    return base.GetItemIcon((object) item, out icon);
  }

  [ContractAnnotation("=> true, image: notnull; => false, image: null")]
  protected virtual bool GetItemImage([NotNull] TItemType item, out Image image)
  {
    return base.GetItemImage((object) item, out image);
  }

  public class ObjectCollectionTypedWrapper : 
    IList,
    ICollection,
    IEnumerable,
    IList<TItemType>,
    ICollection<TItemType>,
    IEnumerable<TItemType>
  {
    [NotNull]
    [NonSerialized]
    private ComboBox.ObjectCollection _objectCollection;
    [NotNull]
    [NonSerialized]
    private readonly TypedComboBox<TItemType> _owner;

    public ObjectCollectionTypedWrapper([NotNull] TypedComboBox<TItemType> owner)
    {
      this._owner = owner;
      this._objectCollection = owner.ObjectCollectionLink;
    }

    /// <summary>Gets the number of items in the collection</summary>
    public int Count
    {
      [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return this._objectCollection.Count;
      }
    }

    /// <summary>Gets a value indicating whether this collection can be modified</summary>
    public bool IsReadOnly
    {
      [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return this._objectCollection.IsReadOnly;
      }
    }

    /// <summary>Retrieves the item at the specified index within the collection</summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The index was less than zero.-or- The index was greater than the
    /// count of items in the collection.</exception>
    /// <param name="index">The index of the item in the collection to retrieve</param>
    /// <returns>An object representing the item located at the specified index within the collection</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [NotNull]
    public virtual TItemType this[int index]
    {
      get
      {
        this.CheckItems();
        return (TItemType) this._objectCollection[index];
      }
      set
      {
        this.CheckItems();
        this._objectCollection[index] = (object) value;
      }
    }

    public void CheckItems()
    {
      if (this._objectCollection == this._owner.ObjectCollectionLink)
        return;
      this._objectCollection = this._owner.ObjectCollectionLink;
    }

    /// <summary>Adds an item to the list of items for a System.Windows.Forms.ComboBox</summary>
    /// <param name="item">An object representing the item to add to the collection</param>
    /// <returns>The zero-based index of the item in the collection</returns>
    /// <exception cref="T:System.ArgumentNullException">The item parameter was null</exception>
    public int Add([NotNull] TItemType item)
    {
      this.CheckItems();
      return this._objectCollection.Add((object) item);
    }

    /// <summary>Adds an array of items to the list of items for a System.Windows.Forms.ComboBox</summary>
    /// <param name="items">An array of objects to add to the list</param>
    /// <exception cref="T:System.ArgumentNullException">An item in the items parameter was null</exception>
    public void AddRange([NotNull, ItemNotNull] IEnumerable<TItemType> items)
    {
      this.CheckItems();
      this._objectCollection.AddRange(items.Cast<object>().ToArray<object>());
    }

    /// <summary>Removes all items from the System.Windows.Forms.ComboBox</summary>
    public void Clear()
    {
      this.CheckItems();
      this._objectCollection.Clear();
    }

    /// <summary>Determines if the specified item is located within the collection</summary>
    /// <param name="value">An object representing the item to locate in the collection</param>
    /// <returns>true if the item is located within the collection; otherwise, false</returns>
    public bool Contains([NotNull] TItemType value)
    {
      this.CheckItems();
      return this._objectCollection.Contains((object) value);
    }

    /// <summary>Copies the entire collection into an existing array of objects at a specified location within the array</summary>
    /// <param name="destination">The object array to copy the collection to</param>
    /// <param name="arrayIndex">The location in the destination array to copy the collection to</param>
    public void CopyTo([NotNull] object[] destination, int arrayIndex)
    {
      this.CheckItems();
      this._objectCollection.CopyTo(destination, arrayIndex);
    }

    /// <summary>Returns an enumerator that can be used to iterate through the item collection</summary>
    /// <returns>An System.Collections.IEnumerator that represents the item collection</returns>
    [NotNull]
    public IEnumerator GetEnumerator()
    {
      this.CheckItems();
      return this._objectCollection.GetEnumerator();
    }

    /// <summary>Retrieves the index within the collection of the specified item</summary>
    /// <param name="value">An object representing the item to locate in the collection</param>
    /// <returns>The zero-based index where the item is located within the collection; otherwise, -1</returns>
    /// <exception cref="T:System.ArgumentNullException">The value parameter was null</exception>
    public int IndexOf(TItemType value)
    {
      this.CheckItems();
      return this._objectCollection.IndexOf((object) value);
    }

    /// <summary>Inserts an item into the collection at the specified index</summary>
    /// <param name="index">The zero-based index location where the item is inserted</param>
    /// <param name="item">An object representing the item to insert</param>
    /// <exception cref="T:System.ArgumentNullException">The item was null</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The index was less than zero.-or- The index was greater than the count of items in the collection</exception>
    public void Insert(int index, TItemType item)
    {
      this.CheckItems();
      this._objectCollection.Insert(index, (object) item);
    }

    /// <summary>Removes the specified item from the System.Windows.Forms.ComboBox</summary>
    /// <param name="value">The System.Object to remove from the list</param>
    public bool Remove([NotNull] TItemType value)
    {
      int index = this.IndexOf(value);
      if (index < 0)
        return false;
      this.CheckItems();
      this._objectCollection.RemoveAt(index);
      return true;
    }

    /// <summary>Removes an item from the System.Windows.Forms.ComboBox at the specified index</summary>
    /// <param name="index">The index of the item to remove</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The value parameter was less than zero.-or- The value parameter was greater than or equal to the count of items in the collection</exception>
    public void RemoveAt(int index)
    {
      this.CheckItems();
      this._objectCollection.RemoveAt(index);
    }

    int IList.Add(object value) => this.Add((TItemType) value);

    void IList.Clear() => this.Clear();

    bool IList.Contains(object value) => this.Contains((TItemType) value);

    int IList.IndexOf([NotNull] object value) => this.IndexOf((TItemType) value);

    void IList.Insert(int index, [NotNull] object value) => this.Insert(index, (TItemType) value);

    bool IList.IsFixedSize
    {
      get
      {
        this.CheckItems();
        return ((IList) this._objectCollection).IsFixedSize;
      }
    }

    bool IList.IsReadOnly => this.IsReadOnly;

    void IList.Remove([NotNull] object value) => this.Remove((TItemType) value);

    void IList.RemoveAt(int index) => this.RemoveAt(index);

    [NotNull]
    object IList.this[int index]
    {
      get => (object) this[index];
      set => this[index] = (TItemType) value;
    }

    void ICollection.CopyTo(Array array, int index) => this.CopyTo((object[]) array, index);

    int ICollection.Count => this.Count;

    bool ICollection.IsSynchronized
    {
      get
      {
        this.CheckItems();
        return ((ICollection) this._objectCollection).IsSynchronized;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        this.CheckItems();
        return ((ICollection) this._objectCollection).SyncRoot;
      }
    }

    IEnumerator<TItemType> IEnumerable<TItemType>.GetEnumerator()
    {
      this.CheckItems();
      return this._objectCollection.Cast<TItemType>().GetEnumerator();
    }

    void ICollection<TItemType>.Add(TItemType item) => this.Add(item);

    void ICollection<TItemType>.Clear() => this.Clear();

    bool ICollection<TItemType>.Contains(TItemType item) => this.Contains(item);

    void ICollection<TItemType>.CopyTo(TItemType[] array, int arrayIndex)
    {
      throw new NotImplementedException($"{this.GetType()}.CopyTo(TItemType[] array, int arrayIndex)");
    }

    int ICollection<TItemType>.Count => this.Count;

    bool ICollection<TItemType>.IsReadOnly => this.IsReadOnly;

    bool ICollection<TItemType>.Remove(TItemType item) => this.Remove(item);

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
  }
}
