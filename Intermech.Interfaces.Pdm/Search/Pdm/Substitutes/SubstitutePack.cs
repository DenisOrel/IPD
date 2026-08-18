// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SubstitutePack
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

[Serializable]
public sealed class SubstitutePack
{
  public SubstitutePack() => this.Groups = new SubstituteGroupCollection(this);

  public SubstituteGroupCollection Groups { get; private set; }

  public IEnumerable<Substitute> GetSubstitutes()
  {
    foreach (SubstituteGroup group in (Collection<SubstituteGroup>) this.Groups)
    {
      foreach (Substitute substitute in (Collection<Substitute>) group.Substitutes)
        yield return substitute;
    }
  }

  public IEnumerable<SubstitutePosition> GetPositions()
  {
    foreach (Substitute substitute in this.GetSubstitutes())
    {
      foreach (SubstitutePosition position in substitute.Positions)
        yield return position;
    }
  }
}
