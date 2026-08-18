// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.SeriesDatesGroupCollection
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

[Serializable]
public sealed class SeriesDatesGroupCollection : BindingList<SeriesDatesGroup>
{
  public SeriesDatesGroupCollection(SeriesDatesPack owner)
  {
    this.Owner = owner != null ? owner : throw new ArgumentNullException(nameof (owner));
  }

  public SeriesDatesPack Owner { get; private set; }

  protected override void ClearItems()
  {
    foreach (SeriesDatesGroup seriesDatesGroup in this.ToList<SeriesDatesGroup>())
      this.Remove(seriesDatesGroup);
  }

  protected override void InsertItem(int index, SeriesDatesGroup item)
  {
    if (item.Pack == this.Owner)
      return;
    item.Pack = this.Owner;
    base.InsertItem(index, item);
  }

  protected override void RemoveItem(int index)
  {
    SeriesDatesGroup seriesDatesGroup = this[index];
    if (seriesDatesGroup.Pack == null)
      return;
    seriesDatesGroup.Pack = (SeriesDatesPack) null;
    base.RemoveItem(index);
  }

  protected override void SetItem(int index, SeriesDatesGroup item)
  {
    if (item.Pack == this.Owner)
      return;
    item.Pack = this.Owner;
    base.SetItem(index, item);
  }

  [System.Runtime.Serialization.OnDeserialized]
  private void OnDeserialized(StreamingContext context)
  {
    List<SeriesDatesGroup> list = this.Items.ToList<SeriesDatesGroup>();
    this.ClearItems();
    foreach (SeriesDatesGroup seriesDatesGroup in list)
      this.Add(seriesDatesGroup);
  }
}
