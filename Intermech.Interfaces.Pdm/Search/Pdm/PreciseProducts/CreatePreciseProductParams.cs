// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.PreciseProducts.CreatePreciseProductParams
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Pdm.PreciseProducts;

[Serializable]
public sealed class CreatePreciseProductParams
{
  public static bool Check(CreatePreciseProductParams @params)
  {
    if (@params == null)
      throw new ArgumentNullException("@params");
    return @params.Blanks != null;
  }

  public CreatePreciseProductParams(long relationID, long productVersionID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(productVersionID))
      throw new ArgumentException();
    this.RelationID = relationID;
    this.ProductVersionID = productVersionID;
    this.Blanks = new List<PreciseProductBlank>();
    this.SpecificationArchiveVersionID = 0L;
  }

  public long RelationID { get; private set; }

  public long ProductVersionID { get; private set; }

  public List<PreciseProductBlank> Blanks { get; set; }

  public long SpecificationArchiveVersionID { get; set; }

  public bool CopyDocumentation { get; set; }

  public bool KeepCheckedOutCreatedObjects { get; set; }

  public bool UseExistsProducts { get; set; }
}
