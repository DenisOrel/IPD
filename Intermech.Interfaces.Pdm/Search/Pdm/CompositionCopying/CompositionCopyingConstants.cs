// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionCopying.CompositionCopyingConstants
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Search.Pdm.CompositionCopying;

public sealed class CompositionCopyingConstants
{
  public static readonly Guid PrototypeReferenceAttributeTypeGuid = new Guid("cadd9668-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StandardProductObjectTypeGuid = new Guid("cad00252-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid OtherProductObjectTypeGuid = new Guid("cad0038d-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MaterialObjectTyepGuid = new Guid("cad00170-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StandardProductComputerModelObjectTypeGuid = new Guid("cad015cb-306c-11d8-b4e9-00304f19f545");
  public static readonly int[] ForbiddenForCreateCopyBaseObjectTypes = new int[4]
  {
    CompositionCopyingConstants.StandardProductObjectTypeID,
    CompositionCopyingConstants.OtherProductObjectTypeID,
    CompositionCopyingConstants.MaterialObjectTypeID,
    CompositionCopyingConstants.StandardProductComputerModelObjectTypeID
  };
  public static readonly Guid ObjectReferenceAssociatedWithDocumentElementAttributeTypeGuid = new Guid("cad001a6-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid DocumentObjectTypeGuid = new Guid("cad00070-306c-11d8-b4e9-00304f19f545");
  public static readonly int[] ForbiddenForCreateCopyAssociatedWithDocumentElementBaseObjectTypes = new int[3]
  {
    CompositionCopyingConstants.StandardProductObjectTypeID,
    CompositionCopyingConstants.OtherProductObjectTypeID,
    CompositionCopyingConstants.MaterialObjectTypeID
  };
  public static readonly Guid ProductCompositionRelationTypeGuid = new Guid("cad00023-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ComplexObjectTypeGuid = new Guid("cad0025e-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid SetOfProductsObjectTypeGuid = new Guid("cad0025f-306c-11d8-b4e9-00304f19f545");
  public static readonly int[] AllowableForCreateCopyBaseObjectTypes = new int[3]
  {
    Constants.AssemblyUnitObjectTypeID,
    CompositionCopyingConstants.ComplexObjectTypeID,
    CompositionCopyingConstants.SetOfProductsObjectTypeID
  };

  public static int PrototypeReferenceAttributeTypeID
  {
    get
    {
      return MetaDataHelper.GetAttributeTypeID(CompositionCopyingConstants.PrototypeReferenceAttributeTypeGuid);
    }
  }

  public static int StandardProductObjectTypeID
  {
    get
    {
      return MetaDataHelper.GetObjectTypeID(CompositionCopyingConstants.StandardProductObjectTypeGuid);
    }
  }

  public static int OtherProductObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(CompositionCopyingConstants.OtherProductObjectTypeGuid);
  }

  public static int MaterialObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(CompositionCopyingConstants.MaterialObjectTyepGuid);
  }

  public static int StandardProductComputerModelObjectTypeID
  {
    get
    {
      return MetaDataHelper.GetObjectTypeID(CompositionCopyingConstants.StandardProductComputerModelObjectTypeGuid);
    }
  }

  public static int ObjectReferenceAssociatedWithDocumentElementAttributeTypeID
  {
    get
    {
      return MetaDataHelper.GetAttributeTypeID(CompositionCopyingConstants.ObjectReferenceAssociatedWithDocumentElementAttributeTypeGuid);
    }
  }

  public static int DocumentObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(CompositionCopyingConstants.DocumentObjectTypeGuid);
  }

  public static int ProductCompositionRelationTypeID
  {
    get
    {
      return MetaDataHelper.GetRelationTypeID(CompositionCopyingConstants.ProductCompositionRelationTypeGuid);
    }
  }

  public static int ComplexObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(CompositionCopyingConstants.ComplexObjectTypeGuid);
  }

  public static int SetOfProductsObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(CompositionCopyingConstants.SetOfProductsObjectTypeGuid);
  }
}
