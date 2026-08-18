// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.PDMObjectsCancelChangesAnalyzer
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Pdm.Server;

internal class PDMObjectsCancelChangesAnalyzer : IObjectsChangingAnalyzer
{
  private static ICacheDataset cache;
  private static IArticleService artSvc;
  private static int artTypeID = -1;
  private static int docTypeID = -1;
  private static int spcTypeID = -1;
  private static int attrArtID = -1;
  private Guid guid = Guid.NewGuid();

  protected virtual void FillAttrs(IUserSession session)
  {
    if (PDMObjectsCancelChangesAnalyzer.artTypeID == -1)
      PDMObjectsCancelChangesAnalyzer.artTypeID = session.IdentHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545");
    if (PDMObjectsCancelChangesAnalyzer.docTypeID == -1)
      PDMObjectsCancelChangesAnalyzer.docTypeID = session.IdentHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
    if (PDMObjectsCancelChangesAnalyzer.spcTypeID == -1)
      PDMObjectsCancelChangesAnalyzer.spcTypeID = session.IdentHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    if (PDMObjectsCancelChangesAnalyzer.attrArtID != -1)
      return;
    PDMObjectsCancelChangesAnalyzer.attrArtID = session.IdentHelper.GetAttributeID("cad001f9-306c-11d8-b4e9-00304f19f545");
  }

  protected virtual void AnalyzeArticle(
    IUserSession session,
    ChangingObject article,
    bool findArticles)
  {
    if (session == null || article == null)
      return;
    long[] mainDocuments = PDMObjectsCancelChangesAnalyzer.artSvc.FindMainDocuments(article.ObjectID, string.Empty, (object) session);
    if (mainDocuments != null)
    {
      for (int index = 0; index < mainDocuments.Length; ++index)
      {
        if (mainDocuments[index] <= 0L && article.Items.FindChangingObjectFromRoot(mainDocuments[index]) == null && article.Items.FindChangingObjectFromRoot(-mainDocuments[index]) == null)
          article.Items.FindRootParent().Add(mainDocuments[index], Math.Abs(mainDocuments[index]), ObjectChangingAction.CancelChanges, true, false, LocalizationHolder.rm.GetString("Pdm.Server_2")).LoadDescription(session);
      }
    }
    if (!findArticles)
      return;
    try
    {
      IDBObject dbObject = session.GetObject(article.ObjectID, false);
      if (dbObject == null)
        return;
      IDBAttribute attributeById = dbObject.GetAttributeByID(PDMObjectsCancelChangesAnalyzer.attrArtID);
      if (attributeById == null || attributeById.IsNull || !GuidHelper.IsGuid(attributeById.AsString))
        return;
      long[] articlesByGroupId = PDMObjectsCancelChangesAnalyzer.artSvc.FindArticlesByGroupID(article.ObjectID, (object) session);
      ChangingObjects rootParent = article.Items.FindRootParent();
      if (articlesByGroupId == null)
        return;
      for (int index = 0; index < articlesByGroupId.Length; ++index)
      {
        if (articlesByGroupId[index] != article.ObjectID && articlesByGroupId[index] < 0L)
        {
          ChangingObject article1 = rootParent.Add(articlesByGroupId[index], Math.Abs(articlesByGroupId[index]), ObjectChangingAction.CancelChanges, true, false, LocalizationHolder.rm.GetString("Pdm.Server_3"));
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
    ChangingObject specification,
    bool findArticles)
  {
    if (session == null)
      return;
    if (specification == null)
      return;
    try
    {
      IDBObject baseArticle = PDMObjectsCancelChangesAnalyzer.artSvc.FindBaseArticle(specification.ObjectID, string.Empty, (object) session);
      if (baseArticle == null || !PDMObjectsCancelChangesAnalyzer.cache.IsInhertitedFrom(baseArticle.ObjectType, PDMObjectsCancelChangesAnalyzer.artTypeID) || specification.Items.FindChangingObjectFromRoot(baseArticle.ObjectID) != null || specification.Items.FindChangingObjectFromRoot(-baseArticle.ObjectID) != null || baseArticle.ObjectID > 0L)
        return;
      ChangingObject article = specification.Items.FindRootParent().Add(baseArticle.ObjectID, Math.Abs(baseArticle.ObjectID), ObjectChangingAction.CancelChanges, true, false, LocalizationHolder.rm.GetString("Pdm.Server_5"));
      article.LoadDescription(session);
      this.AnalyzeArticle(session, article, findArticles);
    }
    catch
    {
    }
  }

  protected virtual void AnalyzeDocument(
    IUserSession session,
    ChangingObject document,
    bool findArticles)
  {
    if (session == null)
      return;
    if (document == null)
      return;
    try
    {
      IDBObject baseArticle = PDMObjectsCancelChangesAnalyzer.artSvc.FindBaseArticle(document.ObjectID, string.Empty, (object) session);
      if (baseArticle == null || !PDMObjectsCancelChangesAnalyzer.cache.IsInhertitedFrom(baseArticle.ObjectType, PDMObjectsCancelChangesAnalyzer.artTypeID) || document.Items.FindChangingObjectFromRoot(baseArticle.ObjectID) != null || document.Items.FindChangingObjectFromRoot(-baseArticle.ObjectID) != null || baseArticle.ObjectID > 0L)
        return;
      ChangingObject article = document.Items.FindRootParent().Add(baseArticle.ObjectID, Math.Abs(baseArticle.ObjectID), ObjectChangingAction.CancelChanges, true, false, LocalizationHolder.rm.GetString("Pdm.Server_7"));
      article.LoadDescription(session);
      this.AnalyzeArticle(session, article, findArticles);
    }
    catch
    {
    }
  }

  public virtual ObjectChangingAction Action => ObjectChangingAction.CancelChanges;

  public virtual Guid Guid => this.guid;

  public virtual int Analyze(IUserSession session, ChangingObjects changingObjects)
  {
    if (changingObjects == null || changingObjects.Count == 0 || session == null)
      return 0;
    if (PDMObjectsCancelChangesAnalyzer.artSvc == null)
      PDMObjectsCancelChangesAnalyzer.artSvc = ServerServices.GetService(typeof (IArticleService)) as IArticleService;
    if (PDMObjectsCancelChangesAnalyzer.artSvc == null)
      return 0;
    if (PDMObjectsCancelChangesAnalyzer.cache == null)
      PDMObjectsCancelChangesAnalyzer.cache = ServerServices.GetService(typeof (ICacheDataset)) as ICacheDataset;
    if (PDMObjectsCancelChangesAnalyzer.cache == null)
      return 0;
    this.FillAttrs(session);
    List<ChangingObject> changingObjects1 = changingObjects.ExtractChangingObjects();
    int num = 0;
    if (changingObjects1 == null)
      return 0;
    for (int index = 0; index < changingObjects1.Count; ++index)
    {
      ChangingObject changingObject = changingObjects1[index];
      if (changingObject.ObjectID <= 0L)
      {
        changingObject.LoadDescription(session);
        bool flag1 = PDMObjectsCancelChangesAnalyzer.cache.IsArticle(changingObject.ObjectType);
        bool flag2 = PDMObjectsCancelChangesAnalyzer.cache.IsDocument(changingObject.ObjectType);
        bool flag3 = PDMObjectsCancelChangesAnalyzer.cache.IsSpecification(changingObject.ObjectType);
        if (flag1 || flag2 || flag3)
        {
          if (flag1)
            this.AnalyzeArticle(session, changingObject, true);
          if (flag3)
            this.AnalyzeSpecification(session, changingObject, true);
          if (flag2 && !flag3)
            this.AnalyzeDocument(session, changingObject, true);
        }
      }
    }
    return num;
  }
}
