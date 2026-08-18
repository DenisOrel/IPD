// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SubstituteCollection
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

[Serializable]
public sealed class SubstituteCollection : BindingList<Substitute>
{
  public SubstituteCollection(SubstituteGroup owner)
  {
    this.Owner = owner != null ? owner : throw new ArgumentNullException(nameof (owner));
  }

  public SubstituteGroup Owner { get; private set; }

  public Substitute this[long number]
  {
    get => this.FirstOrDefault<Substitute>((Func<Substitute, bool>) (o => o.Number == number));
  }

  protected override void ClearItems()
  {
    foreach (Substitute substitute in (Collection<Substitute>) this)
      substitute.NumberChanging -= new EventHandler<SubsituteNumberEventArgs>(this.Substitute_NumberChanging);
    foreach (Substitute substitute in this.ToList<Substitute>())
      substitute.Group = (SubstituteGroup) null;
  }

  protected override void InsertItem(int index, Substitute item)
  {
    this.ValidateSubsituteNumber(item.Number);
    if (item.Group == this.Owner)
      return;
    item.NumberChanging += new EventHandler<SubsituteNumberEventArgs>(this.Substitute_NumberChanging);
    item.Group = this.Owner;
    base.InsertItem(index, item);
  }

  protected override void RemoveItem(int index)
  {
    if (index < 0 || index >= this.Count || this[(long) index].Group == null)
      return;
    this[(long) index].NumberChanging -= new EventHandler<SubsituteNumberEventArgs>(this.Substitute_NumberChanging);
    this[(long) index].Group = (SubstituteGroup) null;
    base.RemoveItem(index);
  }

  protected override void SetItem(int index, Substitute item)
  {
    this.ValidateSubsituteNumber(item.Number);
    if (item.Group == this.Owner)
      return;
    item.NumberChanging += new EventHandler<SubsituteNumberEventArgs>(this.Substitute_NumberChanging);
    item.Group = this.Owner;
    base.SetItem(index, item);
  }

  private void Substitute_NumberChanging(object sender, SubsituteNumberEventArgs e)
  {
    this.ValidateSubsituteNumber(e.NewNumber);
  }

  private void ValidateSubsituteNumber(long number)
  {
    if (this.Where<Substitute>((Func<Substitute, bool>) (o => o.Number == number)).Count<Substitute>() > 0)
      throw new Exception();
  }
}
