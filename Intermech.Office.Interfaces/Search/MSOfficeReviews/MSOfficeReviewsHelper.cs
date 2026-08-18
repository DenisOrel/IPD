// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MSOfficeReviews.MSOfficeReviewsHelper
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Search.MSOfficeReviews;

public static class MSOfficeReviewsHelper
{
  public static bool IsObjectTypeSupportsReview(int objectTypeID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
      throw new ArgumentException();
    if (!((IEnumerable<int>) MSOfficeReviewsConstants.DocumentObjectTypesIds).Contains<int>(objectTypeID))
      return false;
    List<int> childObjectTypesId = MetaDataHelper.GetApplicabilityChildObjectTypesID(objectTypeID, MSOfficeReviewsConstants.ReviewsRelationTypeID);
    return childObjectTypesId != null && childObjectTypesId.Count > 0;
  }

  public static int[] GetExcelReviewDocumentTypeIds()
  {
    return MSOfficeReviewsHelper.GetDocumentTypeIdsForReviewType(MSOfficeReviewsConstants.MSExelReviewObjectTypeID);
  }

  public static int[] GetWordReviewDocumentTypeIds()
  {
    return MSOfficeReviewsHelper.GetDocumentTypeIdsForReviewType(MSOfficeReviewsConstants.MSWordReviewObjectTypeID);
  }

  private static int[] GetDocumentTypeIdsForReviewType(int reviewTypeID)
  {
    List<int> source = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(MSOfficeReviewsConstants.ReviewsRelationTypeID, reviewTypeID, -1).Rows)
      {
        int int32Value = DataSetProcessor.GetInt32Value(row, "F_INOBJECT_TYPE", -1);
        if (!ObjectTypeHelper.IsUnknownObjectTypeID(int32Value))
          source.Add(int32Value);
      }
    }
    return source.Distinct<int>().ToArray<int>();
  }
}
