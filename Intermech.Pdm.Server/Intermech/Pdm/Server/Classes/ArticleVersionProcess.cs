// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Classes.ArticleVersionProcess
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Pdm.Server.Classes;

internal sealed class ArticleVersionProcess : GroupInstanceProcess
{
  private readonly string _sessionKey = "ArticleVersionProcess_SelfCreate";

  protected override void OnRun(IUserSession session, IDBObject dbObject, IDBObject parentObject)
  {
    new SpecificationVersionHandle().Handle(session, dbObject, parentObject);
    new ElementListVersionHandle().Handle(session, dbObject, parentObject);
    this.GroupInstanceHandle(session, dbObject, parentObject);
  }

  private void GroupInstanceHandle(
    IUserSession session,
    IDBObject dbObject,
    IDBObject parentObject)
  {
    if (session.GetSessionPluginsData((object) this._sessionKey) != null)
      return;
    Guid guid1 = new Guid("cad001f9-306c-11d8-b4e9-00304f19f545");
    IDBAttribute attributeByGuid = parentObject.GetAttributeByGuid(guid1);
    if (attributeByGuid == null || !GuidHelper.IsGuid(attributeByGuid.AsString))
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(guid1);
    Guid guid2 = Guid.NewGuid();
    (dbObject.GetAttributeByGuid(guid1) ?? dbObject.Attributes.AddAttribute(attributeTypeId, false)).Value = (object) guid2;
    IArticleService customService = (IArticleService) session.GetCustomService(typeof (IArticleService));
    if (customService == null)
      throw new Exception(LocalizationHolder.rm.GetString(sc_16997.ssp_pdm_server_16998()));
    IPairedObjectsCreatorService service = ServiceUtils.GetService<IPairedObjectsCreatorService>((object) ServerServices.ServiceContainer, true);
    List<long> listInstances = customService.GetListInstances(attributeByGuid.Value, (object) session);
    if (listInstances == null || listInstances.Count <= 0)
      return;
    for (int index = 0; index < listInstances.Count; ++index)
    {
      long objectID = listInstances[index];
      IDBObject dbObject1 = session.GetObject(objectID, false);
      if (dbObject1 != null && dbObject1.ID != dbObject.ID)
      {
        IDBObject dbObject2 = service.FindCreatedVersion(session, dbObject1.ObjectID);
        if (dbObject2 == null)
        {
          IDBObjectCollection objectCollection = session.GetObjectCollection(dbObject1.ObjectType);
          session.SetSessionPluginsData((object) this._sessionKey, (object) true);
          try
          {
            dbObject2 = objectCollection.CreateVersion(dbObject1.ObjectID);
          }
          finally
          {
            session.RemoveSessionPluginsData((object) this._sessionKey);
          }
        }
        (dbObject2.GetAttributeByGuid(guid1) ?? dbObject2.Attributes.AddAttribute(attributeTypeId, false)).Value = (object) guid2;
      }
    }
  }
}
