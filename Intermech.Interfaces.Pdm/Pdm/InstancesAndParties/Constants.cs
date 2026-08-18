// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.InstancesAndParties.Constants
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Pdm.InstancesAndParties;

public static class Constants
{
  private static bool _initialized;
  private static int _materialReferenceAttributeTypeID = 0;
  private static int _compositeMaterialReferenceAttributeTypeID = 0;
  private static int _compositeMaterialObjectTypeID = -1;
  private static int _instanceCompositeMaterialObjectTypeID = -1;
  private static int _instanceMaterialObjectTypeID = -1;
  private static int _materialObjectTypeID = -1;
  private static int _partyCompositeMaterialObjectTypeID = -1;
  private static int _partyMaterialObjectTypeID = -1;
  private static int _materialMarkObjectTypeID = -1;
  private static int _instanceMaterialMarkObjectTypeID = -1;
  private static int _partyMaterialMarkObjectTypeID = -1;
  private static int _materialMarkReferenceAttributeTypeID = -1;
  public static readonly Guid MaterialReferenceAttributeTypeGuid = new Guid("cadd950a-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid CompositeMaterialReferenceAttributeTypeGuid = new Guid("cadd9512-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MaterialObjectTypeGuid = new Guid("cad00172-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid CompositeMaterialObjectTypeGuid = new Guid("cad00173-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid PartyMaterialObjectTypeGuid = new Guid("cadd950e-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid InstanceMaterialObjectTypeGuid = new Guid("cadd950f-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid PartyCompositeMaterialObjectTypeGuid = new Guid("cadd9510-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid InstanceCompositeMaterialObjectTypeGuid = new Guid("cadd9511-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MaterialMarkObjectTypeGuid = new Guid("cad00171-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid InstanceMaterialMarkObjectTypeGuid = new Guid("cadd9516-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid PartyMaterialMarkObjectTypeGuid = new Guid("cadd9515-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MaterialMarkReferenceAttributeTypeGuid = new Guid("cadd9513-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MaterialBaseObjectTypeGuid = new Guid("cad00170-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AccountingInProductionAttributeTypeGuid = new Guid("cad0058a-306c-11d8-b4e9-00304f19f545");

  public static int MaterialReferenceAttributeTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._materialReferenceAttributeTypeID;
    }
  }

  public static int CompositeMaterialReferenceAttributeTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._compositeMaterialReferenceAttributeTypeID;
    }
  }

  public static int MaterialObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._materialObjectTypeID;
    }
  }

  public static int CompositeMaterialObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._compositeMaterialObjectTypeID;
    }
  }

  public static int PartyMaterialObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._partyMaterialObjectTypeID;
    }
  }

  public static int InstanceMaterialObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._instanceMaterialObjectTypeID;
    }
  }

  public static int PartyCompositeMaterialObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._partyCompositeMaterialObjectTypeID;
    }
  }

  public static int InstanceCompositeMaterialObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._instanceCompositeMaterialObjectTypeID;
    }
  }

  public static int MaterialMarkObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._materialMarkObjectTypeID;
    }
  }

  public static int InstanceMaterialMarkObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._instanceMaterialMarkObjectTypeID;
    }
  }

  public static int PartyMaterialMarkObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._partyMaterialMarkObjectTypeID;
    }
  }

  public static int MaterialMarkReferenceAttributeTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._materialMarkReferenceAttributeTypeID;
    }
  }

  private static void InitializeIfNotInitialized()
  {
    if (Constants._initialized)
      return;
    Constants.Initialize();
  }

  private static void Initialize()
  {
    Constants._materialReferenceAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.MaterialReferenceAttributeTypeGuid);
    Constants._compositeMaterialReferenceAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.CompositeMaterialReferenceAttributeTypeGuid);
    Constants._compositeMaterialObjectTypeID = Constants.GetObjectTypeID4ObjectTypeGuid(Constants.CompositeMaterialObjectTypeGuid);
    Constants._instanceCompositeMaterialObjectTypeID = Constants.GetObjectTypeID4ObjectTypeGuid(Constants.InstanceCompositeMaterialObjectTypeGuid);
    Constants._instanceMaterialObjectTypeID = Constants.GetObjectTypeID4ObjectTypeGuid(Constants.InstanceMaterialObjectTypeGuid);
    Constants._materialObjectTypeID = Constants.GetObjectTypeID4ObjectTypeGuid(Constants.MaterialObjectTypeGuid);
    Constants._partyCompositeMaterialObjectTypeID = Constants.GetObjectTypeID4ObjectTypeGuid(Constants.PartyCompositeMaterialObjectTypeGuid);
    Constants._partyMaterialObjectTypeID = Constants.GetObjectTypeID4ObjectTypeGuid(Constants.PartyMaterialObjectTypeGuid);
    Constants._materialMarkObjectTypeID = Constants.GetObjectTypeID4ObjectTypeGuid(Constants.MaterialMarkObjectTypeGuid);
    Constants._instanceMaterialMarkObjectTypeID = Constants.GetObjectTypeID4ObjectTypeGuid(Constants.InstanceMaterialMarkObjectTypeGuid);
    Constants._partyMaterialMarkObjectTypeID = Constants.GetObjectTypeID4ObjectTypeGuid(Constants.PartyMaterialMarkObjectTypeGuid);
    Constants._materialMarkReferenceAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.MaterialMarkReferenceAttributeTypeGuid);
    Constants._initialized = true;
  }

  private static int GetAttributeTypeID4AttributeTypeGuid(Guid attributeTypeGuid)
  {
    return MetaDataHelper.GetAttributeTypeID(attributeTypeGuid);
  }

  private static int GetObjectTypeID4ObjectTypeGuid(Guid objectTypeGuid)
  {
    return MetaDataHelper.GetObjectTypeID(objectTypeGuid);
  }
}
