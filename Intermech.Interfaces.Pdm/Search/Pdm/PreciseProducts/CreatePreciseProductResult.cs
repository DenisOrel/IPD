// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.PreciseProducts.CreatePreciseProductResult
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Pdm.PreciseProducts;

[Serializable]
public sealed class CreatePreciseProductResult
{
  public CreatePreciseProductResult()
  {
    this.CreatedPreciseProductVersionIDDictionaryByCompositionPartID = new Dictionary<Tuple<long, long>, long>();
  }

  public Dictionary<Tuple<long, long>, long> CreatedPreciseProductVersionIDDictionaryByCompositionPartID { get; set; }
}
