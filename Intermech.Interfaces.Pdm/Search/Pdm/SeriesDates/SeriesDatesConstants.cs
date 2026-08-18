// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.SeriesDatesConstants
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

public static class SeriesDatesConstants
{
  public const int SeriesDatesHelpTopicID = 2769;
  public static readonly Guid SeriesDatesApplicabilityAttributeTypeGuid = new Guid("cadd940c-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid HeadProductObjectTypeGuid = new Guid("cadd940b-306c-11d8-b4e9-00304f19f545");

  public static int SeriesDatesApplicabilityAttributeTypeID
  {
    get
    {
      return SeriesDatesConstants.GetAttributeTypeID(SeriesDatesConstants.SeriesDatesApplicabilityAttributeTypeGuid);
    }
  }

  public static int HeadProductObjectTypeID
  {
    get => SeriesDatesConstants.GetObjectTypeID(SeriesDatesConstants.HeadProductObjectTypeGuid);
  }

  private static int GetAttributeTypeID(Guid attributeTypeGuid)
  {
    return MetaDataHelper.GetAttributeTypeID(attributeTypeGuid);
  }

  private static int GetObjectTypeID(Guid objectTypeGuid)
  {
    return MetaDataHelper.GetObjectTypeID(objectTypeGuid);
  }
}
