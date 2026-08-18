// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.PossibleValuesCollection
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class PossibleValuesCollection : CollectionBase
{
  public void Add(PossibleValue pv) => this.List.Add((object) pv);

  public PossibleValue this[int index] => (PossibleValue) this.List[index];
}
