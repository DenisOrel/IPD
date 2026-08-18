// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Mbom.MbomConstants
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Search.Mbom;

public static class MbomConstants
{
  public static readonly Guid MbomObjectTypeGuid = new Guid("cadd98aa-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MbomCompositionRelationTypeGuid = new Guid("cadd98ac-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MbomBindingRelationTypeGuid = new Guid("cadd98ab-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid EbomCompositionRelationTypeGuid = new Guid("cad00023-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid TechnologicalAssemblyUnitObjectTypeGuid = new Guid("cad00650-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ThingMeasureUnitObjectVersionGuid = new Guid("cad002e8-306c-11d8-b4e9-00304f19f545");

  public static int AssemblyUnitObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545");
  }

  public static int MbomObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(MbomConstants.MbomObjectTypeGuid);
  }

  public static int MbomCompositionRelationTypeID
  {
    get => MetaDataHelper.GetRelationTypeID(MbomConstants.MbomCompositionRelationTypeGuid);
  }

  public static int MbomBindingRelationTypeID
  {
    get => MetaDataHelper.GetRelationTypeID(MbomConstants.MbomBindingRelationTypeGuid);
  }

  public static int EbomCompositionRelationTypeID
  {
    get => MetaDataHelper.GetRelationTypeID(MbomConstants.EbomCompositionRelationTypeGuid);
  }

  public static int TechnologicalAssemblyUnitObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(MbomConstants.TechnologicalAssemblyUnitObjectTypeGuid);
  }
}
