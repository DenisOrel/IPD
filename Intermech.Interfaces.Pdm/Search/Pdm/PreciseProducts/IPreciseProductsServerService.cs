// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.PreciseProducts.IPreciseProductsServerService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Search.Pdm.PreciseProducts;

public interface IPreciseProductsServerService
{
  PreciseProductBlank[] CreatePreciseProductsBlanks(
    Guid userSessionGuid,
    CreatePreciseProductsBlanksParams createPreciseProductsBlanksParams);

  CreatePreciseProductResult CreatePreciseProduct(
    Guid userSessionGuid,
    CreatePreciseProductParams createPreciseProductParams);
}
