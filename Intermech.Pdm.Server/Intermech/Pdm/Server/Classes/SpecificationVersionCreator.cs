// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Classes.SpecificationVersionCreator
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server.Classes;

internal sealed class SpecificationVersionCreator : PairedObjectsCreator
{
  private IgnoredSessionsBag disablePairedArticlesSwitch;
  private List<SpecificationVersionCreator.NewSpecificationVersionData> newSpecifications;
  private SpecificationVersionCreatorIDCache idCache;

  public SpecificationVersionCreator(
    IgnoredSessionsBag disablePairedArticlesSwitch,
    SpecificationVersionCreatorIDCache idCache)
  {
    this.disablePairedArticlesSwitch = disablePairedArticlesSwitch;
    this.newSpecifications = new List<SpecificationVersionCreator.NewSpecificationVersionData>();
    this.idCache = idCache;
  }

  protected override void OnAfterCreateObject(IDBObject newObject, IDBObject prototype)
  {
    base.OnAfterCreateObject(newObject, prototype);
    if (newObject.ParentVersionID == -1L || !newObject.isParentType(this.idCache.Specifications.Guid) || this.disablePairedArticlesSwitch.Contains(newObject.Session.SessionGUID))
      return;
    this.CreatePairedArticleVersionsBySpecification(newObject, prototype);
  }

  private void CreatePairedArticleVersionsBySpecification(
    IDBObject newSpecification,
    IDBObject oldSpecification)
  {
    IUserSession session = newSpecification.Session;
    SpecificationVersionCreator.NewSpecificationVersionData specificationVersionData = new SpecificationVersionCreator.NewSpecificationVersionData(newSpecification, oldSpecification);
    this.newSpecifications.Add(specificationVersionData);
    long? linkedArticle = this.TryFindLinkedArticle(oldSpecification);
    if (!linkedArticle.HasValue)
      return;
    long[] articlesByGroupId = ((IArticleService) ServerServices.GetService(typeof (IArticleService))).FindArticlesByGroupID(linkedArticle.Value, (object) session.SessionGUID);
    if (articlesByGroupId != null && articlesByGroupId.Length != 0)
    {
      Guid guid = Guid.NewGuid();
      foreach (long oldArticleID in articlesByGroupId)
      {
        IDBObject articleVersion = this.CreateArticleVersion(session, newSpecification, oldSpecification, oldArticleID);
        articleVersion.Attributes.AddAttribute(this.idCache.InstanceGroupId.Id, false, new object[1]
        {
          (object) guid
        });
        specificationVersionData.NewArticleIds.Add(articleVersion.ObjectID);
      }
    }
    else
    {
      IDBObject articleVersion = this.CreateArticleVersion(session, newSpecification, oldSpecification, linkedArticle.Value);
      specificationVersionData.NewArticleIds.Add(articleVersion.ObjectID);
    }
  }

  private long? TryFindLinkedArticle(IDBObject specificationObj)
  {
    DataTable dataTable = specificationObj.Session.GetRelationCollection(this.idCache.ArticleToDocuments.Id).EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }), specificationObj.ObjectID);
    return dataTable.Rows.Count != 0 ? new long?(Convert.ToInt64(dataTable.Rows[0][0])) : new long?();
  }

  private IDBObject CreateArticleVersion(
    IUserSession session,
    IDBObject newSpecification,
    IDBObject oldSpecification,
    long oldArticleID)
  {
    return this.FindCreatedVersion(oldArticleID) ?? session.GetObjectCollection(session.GetObjectInfo(oldArticleID).ObjectTypeID).CreateVersion(oldArticleID);
  }

  protected override void OnEndCreation()
  {
    base.OnEndCreation();
    foreach (SpecificationVersionCreator.NewSpecificationVersionData newSpecification in this.newSpecifications)
    {
      if (newSpecification.NewArticleIds.Count != 0)
        this.FixArticleToSpecificationRelations(newSpecification);
    }
  }

  private void FixArticleToSpecificationRelations(
    SpecificationVersionCreator.NewSpecificationVersionData newSpecificationData)
  {
    long id = newSpecificationData.OldSpecification.ID;
    long objectId = newSpecificationData.NewSpecification.ObjectID;
    AttributeValues[] valuesList = new AttributeValues[1]
    {
      new AttributeValues(this.idCache.FixedRelation.Id, (object) Math.Abs(objectId))
    };
    IUserSession session = newSpecificationData.NewSpecification.Session;
    IDBRelationCollection relationCollection = session.GetRelationCollection(this.idCache.ArticleToDocuments.Id);
    foreach (long newArticleId in (IEnumerable<long>) newSpecificationData.NewArticleIds)
      (session.GetRelation(newArticleId, id, false) ?? relationCollection.Create(newArticleId, objectId)).SetAttributesValues(valuesList);
  }

  private sealed class NewSpecificationVersionData
  {
    public NewSpecificationVersionData(IDBObject newSpecification, IDBObject oldSpecification)
    {
      this.NewSpecification = newSpecification;
      this.OldSpecification = oldSpecification;
      this.NewArticleIds = (ICollection<long>) new List<long>();
    }

    public IDBObject NewSpecification { get; private set; }

    public IDBObject OldSpecification { get; private set; }

    public ICollection<long> NewArticleIds { get; private set; }
  }
}
