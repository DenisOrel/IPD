// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.Substitute
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

[Serializable]
public sealed class Substitute
{
  private long _number;
  private bool _isDesignerActualVariant;
  private SubstituteGroup _group;

  public Substitute() => this.Positions = new SubstitutePositionCollection(this);

  public event EventHandler<SubsituteNumberEventArgs> NumberChanging;

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

  public SubstitutePositionCollection Positions { get; private set; }

  public SubstituteType Type
  {
    get => this.Number != 0L ? SubstituteType.Allowable : SubstituteType.Actual;
  }

  public bool IsDesignerActualVariant
  {
    get => this._isDesignerActualVariant;
    set
    {
      if (this._isDesignerActualVariant == value)
        return;
      this._isDesignerActualVariant = value;
    }
  }

  public SubstituteGroup Group
  {
    get => this._group;
    set
    {
      if (this._group == value)
        return;
      if (this._group != null)
        this._group.Substitutes.Remove(this);
      this._group = value;
      if (this._group == null)
        return;
      this._group.Substitutes.Add(this);
    }
  }

  private void OnNumberChanging(long newNumber)
  {
    EventHandler<SubsituteNumberEventArgs> numberChanging = this.NumberChanging;
    if (numberChanging == null)
      return;
    numberChanging((object) this, new SubsituteNumberEventArgs(newNumber));
  }
}
