// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.RangeCollectionBase`1
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

[Serializable]
public abstract class RangeCollectionBase<T> : BindingList<T> where T : IRange
{
  public RangeCollectionBase(SeriesDatesGroup owner)
  {
    this.Owner = owner != null ? owner : throw new ArgumentNullException(nameof (owner));
  }

  public SeriesDatesGroup Owner { get; private set; }

  public void AddRange(T[] items)
  {
    foreach (T obj in items)
      this.Add(obj);
  }

  protected override void ClearItems()
  {
    foreach (T obj in this.ToList<T>())
      this.Remove(obj);
  }

  protected override void InsertItem(int index, T item)
  {
    if (item.Group == this.Owner)
      return;
    item.Group = this.Owner;
    base.InsertItem(index, item);
  }

  protected override void RemoveItem(int index)
  {
    T obj = this[index];
    if (obj.Group == null)
      return;
    obj.Group = (SeriesDatesGroup) null;
    base.RemoveItem(index);
  }

  protected override void SetItem(int index, T item)
  {
    if (item.Group == this.Owner)
      return;
    item.Group = this.Owner;
    base.SetItem(index, item);
  }

  [System.Runtime.Serialization.OnDeserialized]
  private void OnDeserialized(StreamingContext context)
  {
    T[] array = this.Items.ToArray<T>();
    this.ClearItems();
    foreach (T obj in array)
      this.Add(obj);
  }
}
