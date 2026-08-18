// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.DocumentVersionHandle
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Pdm.Server;

internal abstract class DocumentVersionHandle
{
  protected RevisionInstantiationMode revisionInstantiationMode;

  public void Handle(IUserSession session, IDBObject dbObject, IDBObject parentObject)
  {
    List<Tuple<long, int>> documents = this.FindDocuments(session, parentObject.ObjectID);
    if (documents == null)
      return;
    foreach (Tuple<long, int> tuple in documents)
      this.CreateVersion(session, dbObject, parentObject, tuple.Item1, tuple.Item2);
  }

  protected abstract List<Tuple<long, int>> FindDocuments(IUserSession session, long articleID);

  protected void CreateVersion(
    IUserSession session,
    IDBObject article,
    IDBObject parentArticle,
    long parentVersionDocumentID,
    int documentTypeID)
  {
    IPairedObjectsCreatorService service = ServiceUtils.GetService<IPairedObjectsCreatorService>((object) ServerServices.ServiceContainer, true);
    if (!this.NeedCreateVersion(session, parentArticle.ObjectID, parentVersionDocumentID))
      return;
    IDBObjectCollection objectCollection = session.GetObjectCollection(documentTypeID);
    IDBObject dbObject = service.FindCreatedVersion(session, parentVersionDocumentID) ?? objectCollection.CreateVersion(parentVersionDocumentID);
    IDBRelation relation = session.GetRelation(article.ObjectID, dbObject.ID);
    if (relation == null)
      return;
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545"), (object) Math.Abs(dbObject.ObjectID)));
    if (this.revisionInstantiationMode == RevisionInstantiationMode.Hard)
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cadd9609-306c-11d8-b4e9-00304f19f545"), (object) 1L));
    relation.SetAttributesValues(attributeValuesList.ToArray());
  }

  protected virtual bool NeedCreateVersion(
    IUserSession session,
    long parentArticleID,
    long parentVersionDocumentID)
  {
    return true;
  }
}
