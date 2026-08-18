// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MSOfficeReviews.MSOfficeReviewsServerService
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Search.MSOfficeReviews;

public sealed class MSOfficeReviewsServerService : LongLifeObject, IMSOfficeReviewsServerService
{
  private static readonly Regex MSWordDocumentFileNameRegex = new Regex(".*\\.(doc|docx)$", RegexOptions.IgnoreCase);
  private static readonly Regex MSExelDocumentFileNameRegex = new Regex(".*\\.(xls|xlsx)$", RegexOptions.IgnoreCase);

  public long FindOwnReviewForDocument(Guid userSessionGuid, long documentVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(documentVersionID) ? this.FindOwnReviewForDocument(documentVersionID) : throw new ArgumentException();
  }

  public long CreateReviewForDocument(Guid userSessionGuid, long documentVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(documentVersionID) ? this.CreateReviewForDocument(documentVersionID) : throw new ArgumentException();
  }

  public bool IsActualReview(Guid userSessionGuid, long reviewVersionID, long documentVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(reviewVersionID))
        throw new ArgumentException();
      return !ObjectHelper.IsUnknownObjectVersionID(documentVersionID) ? this.IsActualReview(reviewVersionID, documentVersionID) : throw new ArgumentException();
    }
  }

  public bool DontShowOldReviewNotification(Guid userSessionGuid, long reviewVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(reviewVersionID) ? this.DontShowOldReviewNotificaiton(reviewVersionID) : throw new ArgumentException();
  }

  public void SetDontShowOldReviewNotification(Guid userSessionGuid, long reviewVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(reviewVersionID))
        throw new ArgumentException();
      this.SetDontShowOldReviewNotification(reviewVersionID);
    }
  }

  public void ReplaceReviewByDocument(
    Guid userSessionGuid,
    long reviewVersionID,
    long documentVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(reviewVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(documentVersionID))
        throw new ArgumentException();
      this.ReplaceReviewByDocument(reviewVersionID, documentVersionID);
    }
  }

  public long[] FindAllActualReviewsForDocument(Guid userSessionGuid, long documentVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(documentVersionID) ? this.FindAllActualReviewsForDocument(documentVersionID) : throw new ArgumentException();
  }

  public long[] FindAllReviewsForDocument(Guid userSessionGuid, long documentVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(documentVersionID) ? this.FindAllReviewsForDocument(documentVersionID) : throw new ArgumentException();
  }

  public void ReplaceDocumentByReview(
    Guid userSessionGuid,
    long documentVersionID,
    long reviewVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(documentVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(reviewVersionID))
        throw new ArgumentException();
      this.ReplaceDocumentByReview(documentVersionID, reviewVersionID);
    }
  }

  public void RemoveOwnReviewForDocument(Guid userSessionGuid, long documentVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(documentVersionID))
        throw new ArgumentException();
      this.RemoveOwnReviewForDocument(documentVersionID);
    }
  }

  public void RemoveAllReviewsForDocument(Guid userSessionGuid, long documentVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(documentVersionID))
        throw new ArgumentException();
      this.RemoveAllReviewsForDocument(documentVersionID);
    }
  }

  public void ReplaceOrRemoveReviewForNewDocumentVersion(
    Guid userSessionGuid,
    long documentVersionID,
    long reviewID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(documentVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectID(reviewID))
        throw new ArgumentException();
      this.ReplaceOrRemoveReviewForNewDocumentVersion(documentVersionID, reviewID);
    }
  }

  private long FindOwnReviewForDocument(long documentVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MSOfficeReviewsConstants.ReviewsRelationTypeID);
      relationCollection.ChildObjectTypes = (IList<int>) new List<int>()
      {
        MSOfficeReviewsConstants.MSWordReviewObjectTypeID,
        MSOfficeReviewsConstants.MSExelReviewObjectTypeID
      };
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
      dbRecordSetParams.Columns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      };
      // ISSUE: explicit reference operation
      (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) ObligatoryObjectAttributes.F_CREATOR_ID,
          RelationalOperator = RelationalOperators.Equal,
          Value = (object) sessionKeeper.Session.UserID,
          SQL = string.Empty
        }
      };
      DBRecordSetParams paramSet = dbRecordSetParams;
      DataTable dataTable = relationCollection.ConsistFrom(paramSet, documentVersionID);
      return dataTable.Rows.Count > 0 ? DataSetProcessor.GetInt64Value(dataTable.Rows[0], 0, 0L) : 0L;
    }
  }

  private long CreateReviewForDocument(long documentVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(documentVersionID);
      (dbObject1.GetAttributeByID(MSOfficeReviewsConstants.FileAttributeTypeID) as IBlobReader).OpenBlob(-1);
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
      IDBObject dbObject2;
      if (this.IsExcelReviewDocument(dbObject1.ObjectType))
      {
        dbObject2 = objectCollection.Create(MSOfficeReviewsConstants.MSExelReviewObjectTypeID);
      }
      else
      {
        if (!this.IsWordReviewDocument(dbObject1.ObjectType))
          throw new NotSupportedException($"Для типа #{dbObject1.ObjectType} рецензии не поддерживаются. Проверьте настройки рецензий.");
        dbObject2 = objectCollection.Create(MSOfficeReviewsConstants.MSWordReviewObjectTypeID);
      }
      dbObject2.Caption = this.CreateCaptionForReview(sessionKeeper.Session.UserName, dbObject1.Caption, (long) dbObject1.VersionID);
      dbObject2.CommitCreation(true);
      IDBObject dbObject3 = sessionKeeper.Session.GetObject(dbObject2.ObjectID);
      this.ReplaceReviewByDocument(dbObject3.ObjectID, documentVersionID);
      sessionKeeper.Session.GetRelationCollection(MSOfficeReviewsConstants.ReviewsRelationTypeID).Create(documentVersionID, dbObject3.ObjectID);
      return dbObject3.ObjectID;
    }
  }

  private string CreateCaptionForReview(
    string userName,
    string documentCaption,
    long documentVersionNumber)
  {
    return documentVersionNumber != 0L ? $"{userName} {documentCaption} [{documentVersionNumber}]" : $"{userName} {documentCaption}";
  }

  private bool IsExcelReviewDocument(int documentTypeID)
  {
    return ((IEnumerable<int>) this.GetObjectTypeWithChildrenIds(MSOfficeReviewsHelper.GetExcelReviewDocumentTypeIds())).Contains<int>(documentTypeID);
  }

  private bool IsWordReviewDocument(int documentTypeID)
  {
    return ((IEnumerable<int>) this.GetObjectTypeWithChildrenIds(MSOfficeReviewsHelper.GetWordReviewDocumentTypeIds())).Contains<int>(documentTypeID);
  }

  private int[] GetObjectTypeWithChildrenIds(int[] objectTypeIds)
  {
    List<int> intList = new List<int>();
    intList.AddRange((IEnumerable<int>) objectTypeIds);
    foreach (int objectTypeId in objectTypeIds)
      intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId));
    return intList.ToArray();
  }

  private bool IsActualReview(long reviewVersionID, long documentVersionID)
  {
    DateTime modificationDateTime1 = this.GetSourceDocumentContentLastModificationDateTime(reviewVersionID);
    DateTime modificationDateTime2 = this.GetDocumentContentLastModificationDateTime(documentVersionID);
    return modificationDateTime1 != DateTime.MinValue && modificationDateTime1 == modificationDateTime2;
  }

  private bool DontShowOldReviewNotificaiton(long reviewVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(reviewVersionID).GetAttributeByID(MSOfficeReviewsConstants.DontShowOldReviewNotificationAttributeTypeID);
      return attributeById != null && attributeById.AsBoolean;
    }
  }

  private void SetDontShowOldReviewNotification(long reviewVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(reviewVersionID).SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(MSOfficeReviewsConstants.DontShowOldReviewNotificationAttributeTypeID, (object) true)
      });
  }

  private DateTime GetSourceDocumentContentLastModificationDateTime(long reviewVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(reviewVersionID).GetAttributeByID(MSOfficeReviewsConstants.SourceDocumentContentLastModificationDateTimeAttributeTypeID);
      return this.DiscardSeconds(attributeById != null ? attributeById.AsDateTime : DateTime.Now);
    }
  }

  private DateTime GetDocumentContentLastModificationDateTime(long documentVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.DiscardSeconds((sessionKeeper.Session.GetObject(documentVersionID).GetAttributeByID(MSOfficeReviewsConstants.FileAttributeTypeID) as IBlobReader).OpenBlob(-1).ModifyDate);
  }

  private void ReplaceReviewByDocument(long reviewVersionID, long documentVersionID)
  {
    this.ReplaceFile(documentVersionID, reviewVersionID, true);
    this.UpdateReviewSourceDocumentContentLastModificationDateTime(reviewVersionID, documentVersionID);
  }

  private void ReplaceFile(long sourceVersionID, long destinationVersionID, bool setFileName = false)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(sourceVersionID);
      IDBObject dbObject2 = sessionKeeper.Session.GetObject(destinationVersionID);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        IDBAttribute attributeById1 = dbObject1.GetAttributeByID(MSOfficeReviewsConstants.FileAttributeTypeID);
        new BlobProcReader(attributeById1, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        IDBAttribute attributeById2 = dbObject2.GetAttributeByID(MSOfficeReviewsConstants.FileAttributeTypeID);
        BlobInformation aBlobInformation = (attributeById2 as IBlobReader).OpenBlob(-1) with
        {
          ArcMethod = ArcMethods.ZLibPacked,
          RealFileSize = memoryStream.Length,
          ModifyDate = DateTime.Now
        };
        if (string.IsNullOrEmpty(aBlobInformation.FileName) & setFileName)
        {
          BlobInformation blobInformation = (attributeById1 as IBlobReader).OpenBlob(-1);
          aBlobInformation.FileName = Guid.NewGuid().ToString() + blobInformation.FileName;
        }
        memoryStream.Seek(0L, SeekOrigin.Begin);
        new BlobProcWriter(attributeById2, 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      }
      dbObject2.SaveChanges();
    }
  }

  private void UpdateReviewSourceDocumentContentLastModificationDateTime(
    long reviewVersionID,
    long documentVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(reviewVersionID).SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(MSOfficeReviewsConstants.SourceDocumentContentLastModificationDateTimeAttributeTypeID, (object) (sessionKeeper.Session.GetObject(documentVersionID).GetAttributeByID(MSOfficeReviewsConstants.FileAttributeTypeID) as IBlobReader).OpenBlob(-1).ModifyDate)
      });
  }

  private long[] FindAllActualReviewsForDocument(long documentVersionID)
  {
    DateTime modificationDateTime = this.GetDocumentContentLastModificationDateTime(documentVersionID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MSOfficeReviewsConstants.ReviewsRelationTypeID);
      relationCollection.ChildObjectTypes = (IList<int>) new List<int>()
      {
        MSOfficeReviewsConstants.MSWordReviewObjectTypeID,
        MSOfficeReviewsConstants.MSExelReviewObjectTypeID
      };
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
      dbRecordSetParams.Columns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      };
      // ISSUE: explicit reference operation
      (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) MSOfficeReviewsConstants.SourceDocumentContentLastModificationDateTimeAttributeTypeID,
          AttributeSource = AttributeSourceTypes.Object,
          RelationalOperator = RelationalOperators.GreaterOrEqual,
          Value = (object) modificationDateTime,
          SQL = string.Empty
        }
      };
      DBRecordSetParams paramSet = dbRecordSetParams;
      return relationCollection.ConsistFrom(paramSet, documentVersionID).Rows.Cast<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (o => DataSetProcessor.GetInt64Value(o, 0, 0L))).ToArray<long>();
    }
  }

  private long[] FindAllReviewsForDocument(long documentVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MSOfficeReviewsConstants.ReviewsRelationTypeID);
      relationCollection.LocalTypesMode = true;
      DBRecordSetParams paramSet = new DBRecordSetParams()
      {
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }
      };
      return relationCollection.ConsistFrom(paramSet, documentVersionID).Rows.Cast<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (o => DataSetProcessor.GetInt64Value(o, 0, 0L))).ToArray<long>();
    }
  }

  private void ReplaceDocumentByReview(long documentVersionID, long reviewVersionID)
  {
    this.ReplaceFile(reviewVersionID, documentVersionID);
    this.UpdateReviewSourceDocumentContentLastModificationDateTime(reviewVersionID, documentVersionID);
  }

  private void RemoveOwnReviewForDocument(long documentVersionID)
  {
    long reviewForDocument = this.FindOwnReviewForDocument(documentVersionID);
    if (ObjectHelper.IsUnknownObjectVersionID(reviewForDocument))
      return;
    this.DeleteObject(reviewForDocument);
  }

  private void RemoveAllReviewsForDocument(long documentVersionID)
  {
    foreach (long objectVersionID in this.FindAllReviewsForDocument(documentVersionID))
      this.DeleteObject(objectVersionID);
  }

  private void DeleteObject(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(objectVersionID).Delete((long) Consts.PurgeMode);
  }

  private DateTime DiscardSeconds(DateTime dateTime)
  {
    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
  }

  private void ReplaceOrRemoveReviewForNewDocumentVersion(long documentVersionID, long reviewID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MSOfficeReviewsConstants.ReviewsRelationTypeID);
      relationCollection.FiltrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545";
      DBRecordSetParams paramSet = new DBRecordSetParams()
      {
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_PROJ_ID
        }
      };
      long num = relationCollection.EntersIn(paramSet, reviewID).Rows.Cast<DataRow>().Select<DataRow, long>((System.Func<DataRow, long>) (o => DataSetProcessor.GetInt64Value(o, 0, 0L))).FirstOrDefault<long>((System.Func<long, bool>) (o => o != documentVersionID));
      if (ObjectHelper.IsUnknownObjectVersionID(num))
        return;
      try
      {
        IDBObject objectById = sessionKeeper.Session.GetObjectByID(reviewID, true);
        if (!this.IsActualReview(objectById.ObjectID, num))
          return;
        IDBObject dbObject1 = sessionKeeper.Session.GetObjectCollection(objectById.ObjectType).Create(objectById.ObjectID);
        IDBObject dbObject2 = sessionKeeper.Session.GetObject(documentVersionID);
        dbObject1.Caption = this.CreateCaptionForReview(sessionKeeper.Session.UserName, dbObject2.Caption, (long) dbObject2.VersionID);
        dbObject1.SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(MSOfficeReviewsConstants.SourceDocumentContentLastModificationDateTimeAttributeTypeID, (object) this.GetDocumentContentLastModificationDateTime(documentVersionID))
        });
        dbObject1.CommitCreation(true);
        relationCollection.Create(documentVersionID, dbObject1.ObjectID);
      }
      finally
      {
        sessionKeeper.Session.GetRelation(documentVersionID, reviewID).Delete((long) Consts.PurgeMode);
      }
    }
  }
}
