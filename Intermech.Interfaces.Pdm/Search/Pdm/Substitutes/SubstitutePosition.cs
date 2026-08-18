// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SubstitutePosition
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.Utilities;
using System;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

[Serializable]
public sealed class SubstitutePosition
{
  private Substitute _substitute;

  public SubstitutePosition(long relationID, long objectID)
  {
    if (ObjectHelper.IsUnknownObjectID(objectID))
      throw new ArgumentException();
    this.RelationID = relationID;
    this.ObjectID = objectID;
  }

  public long RelationID { get; private set; }

  public long ObjectID { get; private set; }

  public long ObjectVersionID { get; set; }

  public bool IsAuxiliary { get; set; }

  public bool IsEqual { get; set; }

  public long Number { get; set; }

  public Substitute Substitute
  {
    get => this._substitute;
    set
    {
      if (this._substitute == value)
        return;
      if (this._substitute != null)
        this._substitute.Positions.Remove(this);
      this._substitute = value;
      if (this._substitute == null)
        return;
      this._substitute.Positions.Add(this);
    }
  }
}
