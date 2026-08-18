// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.PDMObjectsDeleteAnalyzer
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Pdm.Server;

internal class PDMObjectsDeleteAnalyzer : ObjectsDeleteAnalyzer
{
  private static ICacheDataset cache;
  private static IArticleService artSvc;
  private static int artTypeID = -1;
  private static int docTypeID = -1;
  private static int spcTypeID = -1;
  private static int attrArtID = -1;

  protected virtual void FillTypes(IUserSession session)
  {
    if (PDMObjectsDeleteAnalyzer.artTypeID == -1)
      PDMObjectsDeleteAnalyzer.artTypeID = session.IdentHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545");
    if (PDMObjectsDeleteAnalyzer.docTypeID == -1)
      PDMObjectsDeleteAnalyzer.docTypeID = session.IdentHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
    if (PDMObjectsDeleteAnalyzer.spcTypeID == -1)
      PDMObjectsDeleteAnalyzer.spcTypeID = session.IdentHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    if (PDMObjectsDeleteAnalyzer.attrArtID != -1)
      return;
    PDMObjectsDeleteAnalyzer.attrArtID = session.IdentHelper.GetAttributeID("cad001f9-306c-11d8-b4e9-00304f19f545");
  }

  protected virtual void AnalyzeArticle(
    IUserSession session,
    DeletingObject article,
    bool findArticles)
  {
    if (session == null || article == null)
      return;
    long[] mainDocuments = PDMObjectsDeleteAnalyzer.artSvc.FindMainDocuments(article.ObjectID, string.Empty, (object) session);
    if (mainDocuments != null)
    {
      for (int index = 0; index < mainDocuments.Length; ++index)
      {
        if (article.Items.FindDeletingObjectFromRoot(mainDocuments[index]) == null && article.Items.FindDeletingObjectFromRoot(-mainDocuments[index]) == null)
          article.Items.FindRootParent().Add(0L, 0L, mainDocuments[index], true, LocalizationHolder.rm.GetString("Pdm.Server_18")).LoadDescription(session);
      }
    }
    if (!findArticles)
      return;
    try
    {
      IDBObject dbObject = session.GetObject(article.ObjectID, false);
      if (dbObject == null)
        return;
      IDBAttribute attributeById = dbObject.GetAttributeByID(PDMObjectsDeleteAnalyzer.attrArtID);
      if (attributeById == null || !GuidHelper.IsGuid(attributeById.AsString))
        return;
      long[] articlesByGroupId = PDMObjectsDeleteAnalyzer.artSvc.FindArticlesByGroupID(article.ObjectID, (object) session);
      if (articlesByGroupId == null)
        return;
      for (int index = 0; index < articlesByGroupId.Length; ++index)
      {
        if (articlesByGroupId[index] != article.ObjectID)
        {
          DeletingObject article1 = article.Items.FindRootParent().Add(0L, 0L, articlesByGroupId[index], false, LocalizationHolder.rm.GetString("Pdm.Server_19"));
          article1.LoadDescription(session);
          this.AnalyzeArticle(session, article1, false);
        }
      }
    }
    catch
    {
    }
  }

  protected virtual void AnalyzeSpecification(
    IUserSession session,
    DeletingObject specification,
    bool findArticles)
  {
    if (session == null)
      return;
    if (specification == null)
      return;
    try
    {
      IDBObject baseArticle = PDMObjectsDeleteAnalyzer.artSvc.FindBaseArticle(specification.ObjectID, string.Empty, (object) session);
      if (baseArticle == null || !PDMObjectsDeleteAnalyzer.cache.IsInhertitedFrom(baseArticle.ObjectType, PDMObjectsDeleteAnalyzer.artTypeID) || specification.Items.FindDeletingObjectFromRoot(baseArticle.ObjectID) != null || specification.Items.FindDeletingObjectFromRoot(-baseArticle.ObjectID) != null)
        return;
      DeletingObject article = specification.Items.FindRootParent().Add(0L, baseArticle.ID, baseArticle.ObjectID, false, LocalizationHolder.rm.GetString("Pdm.Server_21"));
      article.LoadDescription(session);
      this.AnalyzeArticle(session, article, findArticles);
    }
    catch
    {
    }
  }

  protected virtual void AnalyzeDocument(
    IUserSession session,
    DeletingObject document,
    bool findArticles)
  {
    if (session == null)
      return;
    if (document == null)
      return;
    try
    {
      IDBObject baseArticle = PDMObjectsDeleteAnalyzer.artSvc.FindBaseArticle(document.ObjectID, string.Empty, (object) session);
      if (baseArticle == null || !PDMObjectsDeleteAnalyzer.cache.IsInhertitedFrom(baseArticle.ObjectType, PDMObjectsDeleteAnalyzer.artTypeID) || document.Items.FindDeletingObjectFromRoot(baseArticle.ObjectID) != null || document.Items.FindDeletingObjectFromRoot(-baseArticle.ObjectID) != null)
        return;
      DeletingObject article = document.Items.FindRootParent().Add(0L, baseArticle.ID, baseArticle.ObjectID, false, LocalizationHolder.rm.GetString("Pdm.Server_23"));
      article.LoadDescription(session);
      this.AnalyzeArticle(session, article, findArticles);
    }
    catch
    {
    }
  }

  public override int Analyze(
    IUserSession session,
    DeletingObjects deletingObjects,
    DeleteAnalyzerOptions options)
  {
    if (deletingObjects == null || deletingObjects.Count == 0 || session == null)
      return 0;
    if (PDMObjectsDeleteAnalyzer.artSvc == null)
      PDMObjectsDeleteAnalyzer.artSvc = ServerServices.GetService(typeof (IArticleService)) as IArticleService;
    if (PDMObjectsDeleteAnalyzer.artSvc == null)
      return 0;
    if (PDMObjectsDeleteAnalyzer.cache == null)
      PDMObjectsDeleteAnalyzer.cache = ServerServices.GetService(typeof (ICacheDataset)) as ICacheDataset;
    if (PDMObjectsDeleteAnalyzer.cache == null)
      return 0;
    this.FillTypes(session);
    List<DeletingObject> deletingObjects1 = deletingObjects.ExtractDeletingObjects();
    int num = 0;
    if (deletingObjects1 == null)
      return 0;
    if ((options & DeleteAnalyzerOptions.FindLinkedObjects) > DeleteAnalyzerOptions.None)
    {
      for (int index = 0; index < deletingObjects1.Count; ++index)
      {
        DeletingObject deletingObject = deletingObjects1[index];
        deletingObject.LoadDescription(session);
        bool flag1 = PDMObjectsDeleteAnalyzer.cache.IsArticle(deletingObject.ObjectType);
        bool flag2 = PDMObjectsDeleteAnalyzer.cache.IsDocument(deletingObject.ObjectType);
        bool flag3 = PDMObjectsDeleteAnalyzer.cache.IsSpecification(deletingObject.ObjectType);
        if (flag1 || flag2 || flag3)
        {
          if (flag1)
            this.AnalyzeArticle(session, deletingObject, true);
          if (flag3)
            this.AnalyzeSpecification(session, deletingObject, true);
          if (flag2 && !flag3)
            this.AnalyzeDocument(session, deletingObject, true);
        }
      }
    }
    if (this.AnalyzeAllVersions(session, deletingObjects, options) > 0)
      this.Analyze(session, deletingObjects, options);
    return num;
  }
}
