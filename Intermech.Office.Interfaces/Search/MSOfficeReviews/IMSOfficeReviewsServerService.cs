// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MSOfficeReviews.IMSOfficeReviewsServerService
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using System;

#nullable disable
namespace Intermech.Search.MSOfficeReviews;

public interface IMSOfficeReviewsServerService
{
  long FindOwnReviewForDocument(Guid userSessionGuid, long documentVersionID);

  long CreateReviewForDocument(Guid userSessionGuid, long documentVersionID);

  bool IsActualReview(Guid userSessionGuid, long reviewVersionID, long documentVersionID);

  bool DontShowOldReviewNotification(Guid userSessionGuid, long reviewVersionID);

  void SetDontShowOldReviewNotification(Guid userSessionGuid, long reviewVersionID);

  void ReplaceReviewByDocument(Guid userSessionGuid, long reviewVersionID, long documentVersionID);

  long[] FindAllActualReviewsForDocument(Guid userSessionGuid, long documentVersionID);

  long[] FindAllReviewsForDocument(Guid userSessionGuid, long documentVersionID);

  void ReplaceDocumentByReview(Guid userSessionGuid, long documentVersionID, long reviewVersionID);

  void RemoveOwnReviewForDocument(Guid userSessionGuid, long documentVersionID);

  void RemoveAllReviewsForDocument(Guid userSessionGuid, long documentVersionID);

  void ReplaceOrRemoveReviewForNewDocumentVersion(
    Guid userSessionGuid,
    long documentVersionID,
    long reviewID);
}
