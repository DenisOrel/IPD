// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.JTLinkManager
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Kernel.Search;
using Intermech.Tools.Data;
using System;
using System.Data;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public static class JTLinkManager
{
  public static long FindJTDocument(long sourceDocumentId, string articleExternalKey)
  {
    if (sourceDocumentId == 0L)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(articleExternalKey))
      throw new ArgumentException();
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    paramSet.ColumnsInfo = new ColumnInfo[1]
    {
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, (object) null)
    };
    paramSet.Conditions = new ConditionStructure[2]
    {
      new ConditionStructure(IDCache.Default.JTSourceDocumentReference.Id, RelationalOperators.Equal, (object) Math.Abs(sourceDocumentId), LogicalOperators.AND, 0, true),
      new ConditionStructure(IDCache.Default.ObjectExternalKey.Id, RelationalOperators.Equal, (object) articleExternalKey, LogicalOperators.NONE, 0, true)
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(IDCache.Default.JTDocuments.Id).Select(paramSet);
      return dataTable.Rows.Count != 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
    }
  }

  internal static long FindJTDocumentFromParentVersion(
    long sourceDocumentId,
    string articleExternalKey)
  {
    if (sourceDocumentId == 0L)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(articleExternalKey))
      throw new ArgumentException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (long index = sessionKeeper.Session.GetObject(sourceDocumentId, true).ParentVersionID; index != -1L; index = sessionKeeper.Session.GetObject(index, true).ParentVersionID)
      {
        if (sessionKeeper.Session.HasMyWorkCopy(index))
          index = -index;
        long jtDocument = JTLinkManager.FindJTDocument(index, articleExternalKey);
        if (jtDocument != 0L)
          return jtDocument;
      }
      return 0;
    }
  }

  internal static void WriteReferenceToSourceDocument(
    long jtDocumentId,
    long sourceDocumentId,
    string articleExternalKey)
  {
    if (jtDocumentId == 0L)
      throw new ArgumentException();
    if (sourceDocumentId == 0L)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(articleExternalKey))
      throw new ArgumentException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      JTLinkManager.WriteReferenceToSourceDocument(sessionKeeper.Session.GetObject(jtDocumentId), sourceDocumentId, articleExternalKey);
  }

  internal static void WriteReferenceToSourceDocument(
    IDBObject jtDocument,
    long sourceDocumentId,
    string articleExternalKey)
  {
    if (jtDocument == null)
      throw new ArgumentNullException(nameof (jtDocument));
    if (sourceDocumentId == 0L)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(articleExternalKey))
      throw new ArgumentException();
    AttributeValues[] valuesList = new AttributeValues[2]
    {
      new AttributeValues(IDCache.Default.JTSourceDocumentReference.Id, (object) sourceDocumentId),
      new AttributeValues(IDCache.Default.ObjectExternalKey.Id, (object) articleExternalKey)
    };
    jtDocument.SetAttributesValues(valuesList);
  }

  public static IObjectLocator SourceDocumentFromJTDocument(IDBObjectRef jtDocumentRef)
  {
    return jtDocumentRef != null ? (IObjectLocator) new SourceDocumentFromJTDocumentLocator(jtDocumentRef) : throw new ArgumentNullException(nameof (jtDocumentRef));
  }

  public static IObjectLocator SourceDocumentFromJTDocument(long jtDocumentId)
  {
    return jtDocumentId != 0L ? JTLinkManager.SourceDocumentFromJTDocument((IDBObjectRef) new DirectDBObjectRef(jtDocumentId)) : throw new ArgumentException();
  }

  public static IObjectLocator ArticleFromJTDocument(IDBObjectRef jtDocumentRef)
  {
    return (IObjectLocator) new ArticleFromJTDocumentLocator(jtDocumentRef);
  }

  public static IObjectLocator ArticleFromJTDocument(long jtDocumentId)
  {
    return jtDocumentId != 0L ? JTLinkManager.ArticleFromJTDocument((IDBObjectRef) new DirectDBObjectRef(jtDocumentId)) : throw new ArgumentException();
  }

  public static IObjectLocator JTDocumentFromDerviedDocument(IDBObjectRef derivedDocumentRef)
  {
    return derivedDocumentRef != null ? (IObjectLocator) new JTDocumentFromDerviedDocumentLocator(derivedDocumentRef) : throw new ArgumentNullException(nameof (derivedDocumentRef));
  }

  public static IObjectLocator JTDocumentFromDerviedDocument(long derivedDocumentId)
  {
    return derivedDocumentId != 0L ? JTLinkManager.JTDocumentFromDerviedDocument((IDBObjectRef) new DirectDBObjectRef(derivedDocumentId)) : throw new ArgumentException();
  }

  public static IObjectLocator DerivedDocumentFromArticle(
    IDBObjectRef articleRef,
    int derivedDocumentType)
  {
    if (articleRef == null)
      throw new ArgumentNullException(nameof (articleRef));
    return derivedDocumentType != -1 ? (IObjectLocator) new DerivedDocumentFromArticleLocator(articleRef, derivedDocumentType) : throw new ArgumentException();
  }

  public static IObjectLocator DerivedDocumentFromArticle(long articleId, int derivedDocumentType)
  {
    if (articleId == 0L)
      throw new ArgumentException();
    return derivedDocumentType != -1 ? JTLinkManager.DerivedDocumentFromArticle((IDBObjectRef) new DirectDBObjectRef(articleId), derivedDocumentType) : throw new ArgumentException();
  }
}
