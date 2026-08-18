// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Classes.ArticleWithSpecificationVersionCreator
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server.Classes;

internal sealed class ArticleWithSpecificationVersionCreator : PairedObjectsCreator
{
  private IgnoredSessionsBag disablePairedArticlesSwitch;
  private List<ArticleWithSpecificationVersionCreator.NewArticleVersionData> newArticles;
  private SpecificationVersionCreatorIDCache idCache;

  public ArticleWithSpecificationVersionCreator(
    IgnoredSessionsBag disablePairedArticlesSwitch,
    SpecificationVersionCreatorIDCache idCache)
  {
    this.disablePairedArticlesSwitch = disablePairedArticlesSwitch;
    this.newArticles = new List<ArticleWithSpecificationVersionCreator.NewArticleVersionData>();
    this.idCache = idCache;
  }

  protected override void OnAfterCreateObject(IDBObject newObject, IDBObject prototype)
  {
    base.OnAfterCreateObject(newObject, prototype);
    if (Consts.IsUndefinedObjectId(newObject.ParentVersionID) || !AvsIDCache.IsProductForSpecification(prototype.ObjectType) || this.disablePairedArticlesSwitch.Contains(newObject.Session.SessionGUID))
      return;
    this.CreatePairedSpecificationVersionsByArticle(newObject, prototype);
  }

  private void CreatePairedSpecificationVersionsByArticle(
    IDBObject newArticle,
    IDBObject oldArticle)
  {
    IUserSession session = newArticle.Session;
    long? linkedSpecification1 = this.TryFindLinkedSpecification(oldArticle);
    if (!linkedSpecification1.HasValue)
      return;
    long? linkedSpecification2 = this.TryFindLinkedSpecification(newArticle);
    long? nullable = linkedSpecification1;
    if (!(linkedSpecification2.GetValueOrDefault() == nullable.GetValueOrDefault() & linkedSpecification2.HasValue == nullable.HasValue))
      return;
    this.newArticles.Add(new ArticleWithSpecificationVersionCreator.NewArticleVersionData(this.CreateSpecificationVersion(session, linkedSpecification1.Value), linkedSpecification1.Value, newArticle.ObjectID));
  }

  private long? TryFindLinkedSpecification(IDBObject articleObj)
  {
    IDBRelationCollection relationCollection = articleObj.Session.GetRelationCollection(this.idCache.ArticleToDocuments.Id);
    relationCollection.ObjectTypeID = this.idCache.Specifications.Id;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable = relationCollection.ConsistFrom(paramSet, articleObj.ObjectID);
    return dataTable.Rows.Count != 0 ? new long?(Convert.ToInt64(dataTable.Rows[0][0])) : new long?();
  }

  private IDBObject CreateSpecificationVersion(IUserSession session, long oldSpecificationID)
  {
    return this.FindCreatedVersion(oldSpecificationID) ?? session.GetObjectCollection(-1).CreateVersion(oldSpecificationID);
  }

  protected override void OnEndCreation()
  {
    base.OnEndCreation();
    foreach (ArticleWithSpecificationVersionCreator.NewArticleVersionData newArticle in this.newArticles)
      this.FixArticleToSpecificationRelations(newArticle);
  }

  private void FixArticleToSpecificationRelations(
    ArticleWithSpecificationVersionCreator.NewArticleVersionData newArticleVersionData)
  {
    IUserSession session = newArticleVersionData.NewSpecification.Session;
    long id = session.GetObjectInfo(newArticleVersionData.OldSpecificationId).ID;
    long objectId = newArticleVersionData.NewSpecification.ObjectID;
    AttributeValues[] valuesList = new AttributeValues[1]
    {
      new AttributeValues(this.idCache.FixedRelation.Id, (object) Math.Abs(objectId))
    };
    IDBRelationCollection relationCollection = session.GetRelationCollection(this.idCache.ArticleToDocuments.Id);
    (session.GetRelation(newArticleVersionData.NewArticleId, id, false) ?? relationCollection.Create(newArticleVersionData.NewArticleId, objectId)).SetAttributesValues(valuesList);
  }

  private sealed class NewArticleVersionData
  {
    public NewArticleVersionData(
      IDBObject newSpecification,
      long oldSpecificationId,
      long newArticleId)
    {
      this.NewSpecification = newSpecification;
      this.OldSpecificationId = oldSpecificationId;
      this.NewArticleId = newArticleId;
    }

    public IDBObject NewSpecification { get; private set; }

    public long OldSpecificationId { get; private set; }

    public long NewArticleId { get; private set; }
  }
}
