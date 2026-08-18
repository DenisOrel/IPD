// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SubstituteGroupCollection
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
public sealed class SubstituteGroupCollection : BindingList<SubstituteGroup>
{
  public SubstituteGroupCollection(SubstitutePack owner)
  {
    this.Owner = owner != null ? owner : throw new ArgumentNullException(nameof (owner));
  }

  public SubstitutePack Owner { get; private set; }

  public SubstituteGroup this[long number]
  {
    get
    {
      return this.FirstOrDefault<SubstituteGroup>((Func<SubstituteGroup, bool>) (o => o.Number == number));
    }
  }

  protected override void ClearItems()
  {
    foreach (SubstituteGroup substituteGroup in (Collection<SubstituteGroup>) this)
      substituteGroup.NumberChanging -= new EventHandler<SubstituteGroupNumberEventArgs>(this.SubstituteGroup_NumberChanging);
    base.ClearItems();
  }

  protected override void InsertItem(int index, SubstituteGroup item)
  {
    if (this.Contains(item))
      throw new ArgumentException();
    this.ValidateSubstituteGroupNumber(item.Number);
    item.NumberChanging += new EventHandler<SubstituteGroupNumberEventArgs>(this.SubstituteGroup_NumberChanging);
    base.InsertItem(index, item);
  }

  protected override void RemoveItem(int index)
  {
    this[(long) index].NumberChanging -= new EventHandler<SubstituteGroupNumberEventArgs>(this.SubstituteGroup_NumberChanging);
    base.RemoveItem(index);
  }

  protected override void SetItem(int index, SubstituteGroup item)
  {
    if (!this.Contains(item))
    {
      this.ValidateSubstituteGroupNumber(item.Number);
      item.NumberChanging += new EventHandler<SubstituteGroupNumberEventArgs>(this.SubstituteGroup_NumberChanging);
    }
    base.SetItem(index, item);
  }

  private void SubstituteGroup_NumberChanging(object sender, SubstituteGroupNumberEventArgs e)
  {
    this.ValidateSubstituteGroupNumber(e.NewNumber);
  }

  private void ValidateSubstituteGroupNumber(long number)
  {
    if (this.Where<SubstituteGroup>((Func<SubstituteGroup, bool>) (o => o.Number == number)).Count<SubstituteGroup>() > 0)
      throw new Exception();
  }
}
