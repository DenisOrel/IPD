// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.PreciseProducts.PreciseProductsHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Pdm.PreciseProducts;

public static class PreciseProductsHelper
{
  public static bool IsObjectTypeSuitableForCreatePreciseProduct(int objectTypeID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
      throw new ArgumentException();
    List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(PreciseProductsConstants.ProductObjectTypeID);
    List<int> applicabilityRelationTypesId = MetaDataHelper.GetApplicabilityRelationTypesID(objectTypeID);
    return objectTypeChildrenId != null && objectTypeChildrenId.Contains(objectTypeID) && applicabilityRelationTypesId != null && applicabilityRelationTypesId.Contains(PreciseProductsConstants.ProductCompositionRelationTypeID);
  }
}
