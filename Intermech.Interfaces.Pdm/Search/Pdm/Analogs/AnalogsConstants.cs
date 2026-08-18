// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Analogs.AnalogsConstants
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Search.Pdm.Analogs;

public static class AnalogsConstants
{
  public static readonly Guid AnalogsRelationTypeGuid = new Guid("cadd96dd-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StartDateAttributeTypeGuid = new Guid("cadd96de-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid EndDateAttributeTypeGuid = new Guid("cadd96df-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid PriorityAnalogAttributeTypeGuid = new Guid("cadd96e0-306c-11d8-b4e9-00304f19f545");
  public const string AnalogSelectionModeRecordSetParamsTagsKey = "B6002FDD-2998-4EE8-986C-66728CBBFBD7";
  public const string AnalogsModuleGuid = "2B55A281-C8CE-4D0E-9F78-737301FA9369";

  public static int AnalogsRelationTypeID
  {
    get => MetaDataHelper.GetRelationTypeID(AnalogsConstants.AnalogsRelationTypeGuid);
  }

  public static int StartDateAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID(AnalogsConstants.StartDateAttributeTypeGuid);
  }

  public static int EndDateAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID(AnalogsConstants.EndDateAttributeTypeGuid);
  }

  public static int PriorityAnalogAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID(AnalogsConstants.PriorityAnalogAttributeTypeGuid);
  }
}
