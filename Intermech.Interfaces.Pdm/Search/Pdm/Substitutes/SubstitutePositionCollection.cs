// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SubstitutePositionCollection
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

[Serializable]
public sealed class SubstitutePositionCollection : IEnumerable<SubstitutePosition>, IEnumerable
{
  private Substitute _owner;
  private List<SubstitutePosition> _substitutePositions = new List<SubstitutePosition>();

  public SubstitutePositionCollection(Substitute owner)
  {
    this._owner = owner != null ? owner : throw new ArgumentNullException(nameof (owner));
  }

  public void Add(SubstitutePosition substitutePosition)
  {
    if (this._substitutePositions.Contains(substitutePosition))
      return;
    this._substitutePositions.Add(substitutePosition);
    substitutePosition.Substitute = this._owner;
  }

  public void Remove(SubstitutePosition substitutePosition)
  {
    if (!this._substitutePositions.Contains(substitutePosition))
      return;
    this._substitutePositions.Remove(substitutePosition);
    substitutePosition.Substitute = (Substitute) null;
  }

  public void Clear()
  {
    SubstitutePosition[] array = this._substitutePositions.ToArray();
    this._substitutePositions.Clear();
    foreach (SubstitutePosition substitutePosition in array)
      substitutePosition.Substitute = (Substitute) null;
  }

  public IEnumerator<SubstitutePosition> GetEnumerator()
  {
    return (IEnumerator<SubstitutePosition>) this._substitutePositions.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
