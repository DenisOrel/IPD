// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.PreciseProducts.CreatePreciseProductsBlanksParams
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.Utilities;
using System;

#nullable disable
namespace Intermech.Search.Pdm.PreciseProducts;

[Serializable]
public sealed class CreatePreciseProductsBlanksParams
{
  public CreatePreciseProductsBlanksParams(long relationID, long productVersionID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(productVersionID))
      throw new ArgumentException();
    this.RelationID = relationID;
    this.ProductVersionID = productVersionID;
  }

  public long RelationID { get; private set; }

  public long ProductVersionID { get; private set; }
}
