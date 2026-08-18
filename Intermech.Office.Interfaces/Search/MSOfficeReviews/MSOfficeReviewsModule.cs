// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MSOfficeReviews.MSOfficeReviewsModule
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Search.MSOfficeReviews;

public sealed class MSOfficeReviewsModule
{
  public void Load()
  {
    IConfigurationOptionInfoProvider optionInfoProvider = ServiceLocator.Get<IConfigurationOptionInfoProvider>();
    if (optionInfoProvider == null)
      return;
    optionInfoProvider.Register(new ConfigurationOptionInfo(typeof (int[]))
    {
      CheckAdmin = true,
      CustomGetHandler = new Func<object>(this.GetExcelReviewDocumentTypeIds),
      CustomSetHandler = new Action<object>(this.SetExcelReviewDocumentTypeIds),
      Description = "Типы документов, для которых разрешено создание рецензий MS Office Excel.",
      DisplayName = "Типы документов поддерживающие рецензии MS Office Excel",
      Key = MSOfficeReviewsConfigurationOptionKeys.ExcelReviewDocumentTypes,
      Page = "Система/Красный карандаш/Рецензирование MS Office",
      TypeConverter = typeof (ObjectTypeIdsTypeConverter)
    });
    optionInfoProvider.Register(new ConfigurationOptionInfo(typeof (int[]))
    {
      CheckAdmin = true,
      CustomGetHandler = new Func<object>(this.GetWordReviewDocumentTypeIds),
      CustomSetHandler = new Action<object>(this.SetWordReviewDocumentTypeIds),
      Description = "Типы документов, для которых разрешено создание рецензий MS Office Word.",
      DisplayName = "Типы документов поддерживающие рецензии MS Office Word",
      Key = MSOfficeReviewsConfigurationOptionKeys.WordReviewDocumentTypes,
      Page = "Система/Красный карандаш/Рецензирование MS Office",
      TypeConverter = typeof (ObjectTypeIdsTypeConverter)
    });
  }

  private object GetExcelReviewDocumentTypeIds()
  {
    return (object) MSOfficeReviewsHelper.GetExcelReviewDocumentTypeIds();
  }

  private void SetExcelReviewDocumentTypeIds(object value)
  {
    if (!(value is int[] numArray))
      numArray = new int[0];
    int[] newDocumentTypeIds = numArray;
    this.SetDocumentTypesForReviewType((int[]) this.GetExcelReviewDocumentTypeIds(), newDocumentTypeIds, MSOfficeReviewsConstants.MSExelReviewObjectTypeID);
  }

  private void SetDocumentTypesForReviewType(
    int[] oldDocumentTypeIds,
    int[] newDocumentTypeIds,
    int reviewTypeID)
  {
    int[] array1 = ((IEnumerable<int>) newDocumentTypeIds).Where<int>((Func<int, bool>) (o => !((IEnumerable<int>) oldDocumentTypeIds).Contains<int>(o))).ToArray<int>();
    int[] array2 = ((IEnumerable<int>) oldDocumentTypeIds).Where<int>((Func<int, bool>) (o => !((IEnumerable<int>) newDocumentTypeIds).Contains<int>(o))).ToArray<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
      foreach (int num in array1)
      {
        RelationsApplicabilityProperties applicabilityProperties = new RelationsApplicabilityProperties()
        {
          InObjectType = num,
          RelationType = MSOfficeReviewsConstants.ReviewsRelationTypeID,
          ObjectType = reviewTypeID,
          MaximumLinks = int.MaxValue,
          RelationConstraintMode = RelationConstraintModes.ChildDelete
        };
        applicabilityCollection.Create(applicabilityProperties);
      }
      foreach (int inObjectType in array2)
        applicabilityCollection.GetApplicability(MSOfficeReviewsConstants.ReviewsRelationTypeID, reviewTypeID, inObjectType)?.Delete();
    }
  }

  private object GetWordReviewDocumentTypeIds()
  {
    return (object) MSOfficeReviewsHelper.GetWordReviewDocumentTypeIds();
  }

  private void SetWordReviewDocumentTypeIds(object value)
  {
    if (!(value is int[] numArray))
      numArray = new int[0];
    int[] newDocumentTypeIds = numArray;
    this.SetDocumentTypesForReviewType((int[]) this.GetWordReviewDocumentTypeIds(), newDocumentTypeIds, MSOfficeReviewsConstants.MSWordReviewObjectTypeID);
  }
}
