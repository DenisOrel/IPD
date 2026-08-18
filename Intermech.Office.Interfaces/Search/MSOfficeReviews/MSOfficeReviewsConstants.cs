// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MSOfficeReviews.MSOfficeReviewsConstants
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.MSOfficeReviews;

public static class MSOfficeReviewsConstants
{
  public static readonly Guid ReviewObjectTypeGuid = new Guid("cadd9723-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MSOfficeReviewObjectTypeGuid = new Guid("cadd9724-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MSWordReviewObjectTypeGuid = new Guid("cadd9726-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MSExelReviewObjectTypeGuid = new Guid("cadd9725-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ReviewsRelationTypeGuid = new Guid("cadd9728-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid DocumentObjectTypeGuid = new Guid("cad00070-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ReviewAttributeGroupGuid = new Guid("cadd9727-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid SourceDocumentLastModificationDateTimeAttributeTypeGuid = new Guid("cadd9729-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid FileAttributeTypeGuid = new Guid("cad0004b-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid DontShowOldReviewNotificationAttributeTypeGuid = new Guid("cadd9737-306c-11d8-b4e9-00304f19f545");
  private static int[] _documentObjectTypesIds;

  public static int ReviewObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(MSOfficeReviewsConstants.ReviewObjectTypeGuid);
  }

  public static int MSOfficeReviewObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(MSOfficeReviewsConstants.MSOfficeReviewObjectTypeGuid);
  }

  public static int MSWordReviewObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(MSOfficeReviewsConstants.MSWordReviewObjectTypeGuid);
  }

  public static int MSExelReviewObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(MSOfficeReviewsConstants.MSExelReviewObjectTypeGuid);
  }

  public static int ReviewsRelationTypeID
  {
    get => MetaDataHelper.GetRelationTypeID(MSOfficeReviewsConstants.ReviewsRelationTypeGuid);
  }

  public static int DocumentObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(MSOfficeReviewsConstants.DocumentObjectTypeGuid);
  }

  public static int[] DocumentObjectTypesIds
  {
    get
    {
      if (MSOfficeReviewsConstants._documentObjectTypesIds == null)
        MSOfficeReviewsConstants._documentObjectTypesIds = MSOfficeReviewsConstants.GetDescendentAndSelfObjectTypeIds(MSOfficeReviewsConstants.DocumentObjectTypeID);
      return MSOfficeReviewsConstants._documentObjectTypesIds;
    }
  }

  public static int ReviewAttributeGroupID
  {
    get => MetaDataHelper.GetAttributeGroupID(MSOfficeReviewsConstants.ReviewAttributeGroupGuid);
  }

  public static int SourceDocumentContentLastModificationDateTimeAttributeTypeID
  {
    get
    {
      return MetaDataHelper.GetAttributeTypeID(MSOfficeReviewsConstants.SourceDocumentLastModificationDateTimeAttributeTypeGuid);
    }
  }

  public static int FileAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID(MSOfficeReviewsConstants.FileAttributeTypeGuid);
  }

  public static int DontShowOldReviewNotificationAttributeTypeID
  {
    get
    {
      return MetaDataHelper.GetAttributeTypeID(MSOfficeReviewsConstants.DontShowOldReviewNotificationAttributeTypeGuid);
    }
  }

  private static int[] GetDescendentAndSelfObjectTypeIds(int objectTypeID)
  {
    List<int> intList = new List<int>();
    intList.Add(objectTypeID);
    intList.AddRange((IEnumerable<int>) (MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeID) ?? new List<int>(0)));
    return intList.ToArray();
  }
}
