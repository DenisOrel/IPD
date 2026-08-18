// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.PreciseProducts.PreciseProductsConstants
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Search.Pdm.PreciseProducts;

public static class PreciseProductsConstants
{
  public static readonly Guid AppliedPdmConfiguratorOptionsAttributeTypeGuid = new Guid("cadd966a-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ProcessingRouteObjectTypeGuid = new Guid("cad0016f-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AssemblyUnitModelObjectTypeGuid = new Guid("cad00768-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ArchiveAttributeTypeGuid = SystemGUIDs.attributeArchive;
  public static readonly Guid PdmConfiguratorAttributeGroupGuid = new Guid("cad015a0-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid DesignationAttributeTypeGuid = new Guid("cad0001f-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid NameAttributeTypeGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid SpecificationObjectTypeGuid = new Guid("cad00133-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ProductCompositionRelationTypeGuid = new Guid("cad00023-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid PdmConfiguratorContextAttributeTypeGuid = new Guid("cad015a6-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid PdmConfiguratorOptionsLinkAttributeTypeGuid = new Guid("cad015a9-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ProductObjectTypeGuid = new Guid("cad00268-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid OrderObjectTypeGuid = new Guid("cad00580-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ComplementObjectTypeGuid = new Guid("cad015b1-306c-11d8-b4e9-00304f19f545");

  public static int AppliedPdmConfiguratorOptionsAttributeTypeID
  {
    get
    {
      return MetaDataHelper.GetAttributeTypeID(PreciseProductsConstants.AppliedPdmConfiguratorOptionsAttributeTypeGuid);
    }
  }

  public static int ProcessingRouteObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(PreciseProductsConstants.ProcessingRouteObjectTypeGuid);
  }

  public static int AssemblyUnitModelObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(PreciseProductsConstants.AssemblyUnitModelObjectTypeGuid);
  }

  public static int ProductGroupIDAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID("cad001f9-306c-11d8-b4e9-00304f19f545");
  }

  public static int ArchiveAttributeTypeID
  {
    get
    {
      return PreciseProductsConstants.GetAttributeTypeID(PreciseProductsConstants.ArchiveAttributeTypeGuid);
    }
  }

  public static int PdmConfiguratorAttributeGroupID
  {
    get
    {
      return PreciseProductsConstants.GetAttributeGroupID(PreciseProductsConstants.PdmConfiguratorAttributeGroupGuid);
    }
  }

  public static int DesignationAttributeTypeID
  {
    get
    {
      return PreciseProductsConstants.GetAttributeTypeID(PreciseProductsConstants.DesignationAttributeTypeGuid);
    }
  }

  public static int NameAttributeTypeID
  {
    get
    {
      return PreciseProductsConstants.GetAttributeTypeID(PreciseProductsConstants.NameAttributeTypeGuid);
    }
  }

  public static int SpecificationObjectTypeID
  {
    get
    {
      return PreciseProductsConstants.GetObjectTypeID(PreciseProductsConstants.SpecificationObjectTypeGuid);
    }
  }

  public static int ProductCompositionRelationTypeID
  {
    get
    {
      return PreciseProductsConstants.GetRelationTypeID(PreciseProductsConstants.ProductCompositionRelationTypeGuid);
    }
  }

  public static int PdmConfiguratorContextAttributeTypeID
  {
    get
    {
      return PreciseProductsConstants.GetAttributeTypeID(PreciseProductsConstants.PdmConfiguratorContextAttributeTypeGuid);
    }
  }

  public static int PdmConfiguratorOptionsLinkAttributeTypeID
  {
    get
    {
      return PreciseProductsConstants.GetAttributeTypeID(PreciseProductsConstants.PdmConfiguratorOptionsLinkAttributeTypeGuid);
    }
  }

  public static int ProductObjectTypeID
  {
    get => PreciseProductsConstants.GetObjectTypeID(PreciseProductsConstants.ProductObjectTypeGuid);
  }

  public static int OrderObjectTypeID
  {
    get => PreciseProductsConstants.GetObjectTypeID(PreciseProductsConstants.OrderObjectTypeGuid);
  }

  public static int ComplementObjectTypeID
  {
    get
    {
      return PreciseProductsConstants.GetObjectTypeID(PreciseProductsConstants.ComplementObjectTypeGuid);
    }
  }

  public static int GetAttributeTypeID(Guid attributeTypeGuid)
  {
    return MetaDataHelper.GetAttributeTypeID(attributeTypeGuid);
  }

  public static int GetObjectTypeID(Guid objectTypeGuid)
  {
    return MetaDataHelper.GetObjectTypeID(objectTypeGuid);
  }

  public static int GetRelationTypeID(Guid relationTypeGuid)
  {
    return MetaDataHelper.GetRelationTypeID(relationTypeGuid);
  }

  public static int GetAttributeGroupID(Guid attributeGroupGuid)
  {
    return MetaDataHelper.GetAttributeGroupID(attributeGroupGuid);
  }
}
