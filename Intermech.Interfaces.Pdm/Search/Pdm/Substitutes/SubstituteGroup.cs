// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SubstituteGroup
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
public sealed class SubstituteGroup
{
  private long _number;

  public SubstituteGroup() => this.Substitutes = new SubstituteCollection(this);

  public event EventHandler<SubstituteGroupNumberEventArgs> NumberChanging;

  public string Name { get; set; }

  public long Number
  {
    get => this._number;
    set
    {
      if (this._number == value)
        return;
      this.OnNumberChanging(value);
      this._number = value;
    }
  }

  public SubstituteCollection Substitutes { get; private set; }

  public IEnumerable<SubstitutePosition> GetPositions()
  {
    foreach (Substitute substitute in (Collection<Substitute>) this.Substitutes)
    {
      foreach (SubstitutePosition position in substitute.Positions)
        yield return position;
    }
  }

  private void OnNumberChanging(long newNumber)
  {
    EventHandler<SubstituteGroupNumberEventArgs> numberChanging = this.NumberChanging;
    if (numberChanging == null)
      return;
    numberChanging((object) this, new SubstituteGroupNumberEventArgs(newNumber));
  }
}
