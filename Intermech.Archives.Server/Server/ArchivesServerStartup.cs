// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.ArchivesServerStartup
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Archives.Common;
using Intermech.Archives.Copies;
using Intermech.Interfaces;
using Intermech.Interfaces.Copies;
using Intermech.Interfaces.ECO;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.Misc;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Interfaces.Copies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Resources;
using System.Xml;

#nullable disable
namespace Intermech.Archives.Server;

public class ArchivesServerStartup : IPackage
{
  private IEventLogHelper eventLogHelper;
  private IDBTimedEvents dbTimedEvents;
  internal static ArchiveStorageIDService StorageIDService;
  private ArchiveService _arcService;

  public string Name => ArchivesServerHolder.rm.GetString("Archives.Server_3");

  public void Load(IServiceProvider serviceProvider)
  {
    if (serviceProvider.GetService(typeof (IPluginManager)) is IPluginManager service1)
      service1.LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
    ArchivesServerHolder.CacheDataSet = serviceProvider.GetService(typeof (ICacheDataset)) as ICacheDataset;
    this.dbTimedEvents = serviceProvider.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    ArchivesServerHolder.rm = new ResourceManager("Intermech.Archives.Server.ArchivesServerResources", Assembly.GetExecutingAssembly());
    IUserSession sessionTemporaryClone = this.dbTimedEvents.GetSystemSessionTemporaryClone("ArchivesServer.Load");
    try
    {
      ConstsHolder.ArchiveAttrID = sessionTemporaryClone.GetAttributeType(ConstsHolder.ArcAttrGuid).AttributeID;
      ConstsHolder.ArcTypeID = sessionTemporaryClone.GetObjectType(ConstsHolder.ArcTypeGuid).ObjectType;
      ConstsHolder.DocTypeID = sessionTemporaryClone.GetObjectType(ConstsHolder.DocTypeGuid).ObjectType;
      ConstsHolder.ArchiveStructureAttrID = sessionTemporaryClone.GetAttributeType(ConstsHolder.ArchiveStructureAttrGuid).AttributeID;
      ConstsHolder.DocTypeID = sessionTemporaryClone.GetObjectType(ConstsHolder.DocTypeGuid).ObjectType;
      this._arcService = new ArchiveService(sessionTemporaryClone);
      ServerServices.AddService(typeof (IArchiveService), (object) this._arcService);
      (serviceProvider.GetService(typeof (IDBObjectService)) as ICreatorContainer).AddCreator((object) ConstsHolder.ArcTypeGuid, (object) new ArchiveDBObjectCreator());
      if (MetaDataHelper.GetAttribute4ObjectType(ConstsHolder.ArcTypeGuid, new Guid("cad0005c-306c-11d8-b4e9-00304f19f545")) != null)
        ArchivesServerStartup.StorageIDService = new ArchiveStorageIDService(sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone?.Logout("ArchivesServer.Load");
    }
    this.eventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    if (this.eventLogHelper != null)
    {
      this.eventLogHelper.GetObjectSecurity += new GetObjectSecurityHandler(this.eventLogHelper_GetObjectSecurity);
      this.eventLogHelper.AddAttributeWriteHandler((object) ConstsHolder.ArchiveAttrID, new WriteAttributeValueHandler(this.WriteArchiveAttributeValue));
      this.eventLogHelper.AddAttributeWriteHandler((object) ConstsHolder.InventoryNumberID, new WriteAttributeValueHandler(this.OnWriteInventoryNumberValue));
      this.eventLogHelper.BeforeObjectPrintEvent += new ObjectEventHandler(this.eventLogHelper_BeforeObjectPrintEvent);
      this.eventLogHelper.BeforeObjectSaveToDiskEvent += new ObjectEventHandler(this.eventLogHelper_BeforeObjectSaveToDiskEvent);
      this.eventLogHelper.AfterPurgeObjectEvent += new ObjectEventHandler(this.eventLogHelper_AfterPurgeObjectEvent);
      this.eventLogHelper.AfterCreateObjectEvent += new AfterCreateObjectHandler(this.eventLogHelper_AfterCreateObjectEvent);
      this.eventLogHelper.AfterChangeObjectTypeEvent += new ObjectTypeChangeHandler(this.eventLogHelper_AfterChangeObjectTypeEvent);
      this.eventLogHelper.CreateObjectEvent += new ObjectEventHandler(this.eventLogHelper_CreateObjectEvent);
      this.eventLogHelper.AfterNextLCStepEvent += new NextLCStepHandler(this.eventLogHelper_AfterNextLCStepEvent);
      this.eventLogHelper.BeforeNextLCStepEvent += new NextLCStepHandler(this.eventLogHelper_BeforeNextLCStepEvent);
      this.eventLogHelper.AfterCreateRelationExEvent += new CreateRelationExHandler(this.eventLogHelper_CreateRelationExEvent);
      this.eventLogHelper.AfterCacheReload += new CacheReloadHandler(this.eventLogHelper_AfterCacheReload);
      this.eventLogHelper.AfterServerSettingsReload += new ServerSettingsReloadHandler(this.eventLogHelper_AfterServerSettingsReload);
      this.eventLogHelper.AfterCommitCreationObjectEvent += new ObjectEventHandler(this.eventLogHelper_AfterCommitCreationObjectEvent);
    }
    ArchiveAutoPlaceCacheService placeCacheService = new ArchiveAutoPlaceCacheService();
    CopiesService copiesService = new CopiesService();
    ApplicationServices.Container.AddService<ArchiveAutoPlaceCacheService>(placeCacheService);
    ApplicationServices.Container.AddService<CopiesService>(copiesService);
    ServerServices.AddService(typeof (IArchiveAutoPlaceCacheService), (object) placeCacheService);
    if (serviceProvider.GetService(typeof (ICustomServices)) is ICustomServices service2)
    {
      service2.AddService(typeof (IArchiveAutoPlaceCacheService), (object) placeCacheService);
      service2.AddService(typeof (ICopiesService), (object) copiesService);
      service2.AddService(typeof (IDocumentCopyService), (object) new DocumentCopyService());
      service2.AddService(typeof (IInventoryNumberGenerator), (object) new InventoryNumberGenerator());
      service2.AddService(typeof (IArchiveService), (object) this._arcService);
      service2.AddService(typeof (IColumnCaptionsHelper), (object) new ColumnCaptionsHelper());
    }
    if (ServerServices.GetService(typeof (IPortalEventsService)) is IPortalEventsService service3)
    {
      service3.ObjectsPublishedEvent += new ObjectsPublishedEventHandler(this.portalEvents_ObjectPublishedEvent);
      service3.ReadImportedObjectAttributesEvent += new ReadImportedObjectAttributesEventHandler(this.PortalEvents_ReadImportedObjectAttributesEvent);
    }
    IServerSynchronizersManager service4 = ApplicationServices.Container.GetService<IServerSynchronizersManager>();
    ArchiveAutoPlaceCacheSynchronizer cacheSynchronizer = new ArchiveAutoPlaceCacheSynchronizer();
    ArchiveAutoPlaceCacheSynchronizer synchronizer = cacheSynchronizer;
    service4.RegisterSynchronizer((IServerSynchronizer) synchronizer);
    placeCacheService.ServersSynchronizer = cacheSynchronizer;
    ISpecHandleAttributes service5 = ServiceUtils.GetService<ISpecHandleAttributes>((object) ServerServices.ServiceContainer, false);
    if (service5 == null)
      return;
    service5.SpecHandleObjectAttributeEvent += new SpecHandleAttributeEventHandler(this.HandleObjectAttributeArchiveEventOnImport);
  }

  private void HandleObjectAttributeArchiveEventOnImport(
    object sender,
    SpecHandleAttributeEventArgs e)
  {
    if (e.AttributeID != ConstsHolder.ArchiveAttrID || ServiceUtils.GetService<IImportRulesService>((object) ServerServices.ServiceContainer, false).RewriteArchive)
      return;
    e.NotUpdate = true;
    if (!e.IsNewObject)
      return;
    IDBObjectType objectType = e.Session.GetObjectType(e.TypeID, false);
    IDBAttributeType dbAttributeType = objectType != null ? objectType.GetAttributeType(ConstsHolder.ArchiveAttrID) : e.Session.GetAttributeType(ConstsHolder.ArchiveAttrID);
    long result;
    if (dbAttributeType == null || dbAttributeType.DefaultValue == null || !long.TryParse(Convert.ToString(dbAttributeType.DefaultValue), out result))
      return;
    QuickObjectInfo objectInfo = e.Session.GetObjectInfo(result);
    if (objectInfo.Empty)
      return;
    e.Value.IntegerValue = (object) result;
    e.Value.StringValue = (object) objectInfo.Caption;
    e.NotUpdate = false;
  }

  private void PortalEvents_ReadImportedObjectAttributesEvent(
    object sender,
    ReadImportedObjectAttributesEventArgs e)
  {
    XmlNode node = e.RootNode.SelectSingleNode("SYSATTRIBUTE");
    if (node == null)
      return;
    XmlAttribute nodeAttribute1 = this.GetNodeAttribute(node, e.Object.Attributes, "F_OTD_REGNUM", ConstsHolder.InventoryNumberID);
    if (nodeAttribute1 != null)
    {
      AttributeRecord attributeRecord = new AttributeRecord(ConstsHolder.InventoryNumberID)
      {
        StringValue = (object) nodeAttribute1.Value
      };
      e.Object.Attributes.Add(attributeRecord);
    }
    XmlAttribute nodeAttribute2 = this.GetNodeAttribute(node, e.Object.Attributes, "F_OTD_REG", ConstsHolder.OTDRegisteredDateID);
    DateTime result;
    if (nodeAttribute2 != null && DateTime.TryParse(nodeAttribute2.Value, out result))
    {
      AttributeRecord attributeRecord = new AttributeRecord(ConstsHolder.OTDRegisteredDateID)
      {
        DateValue = (object) result
      };
      e.Object.Attributes.Add(attributeRecord);
    }
    XmlAttribute nodeAttribute3 = this.GetNodeAttribute(node, e.Object.Attributes, "F_OTD_PREVREGNUM", ConstsHolder.PreviousInventoryNumberID);
    if (nodeAttribute3 == null)
      return;
    AttributeRecord attributeRecord1 = new AttributeRecord(ConstsHolder.PreviousInventoryNumberID)
    {
      StringValue = (object) nodeAttribute3.Value
    };
    e.Object.Attributes.Add(attributeRecord1);
  }

  private XmlAttribute GetNodeAttribute(
    XmlNode node,
    List<AttributeRecord> attributesCollection,
    string attributeName,
    int attributeID)
  {
    XmlAttribute attribute = node.Attributes[attributeName];
    return attribute != null && attribute.Value != string.Empty && !attributesCollection.Exists((Predicate<AttributeRecord>) (x => x.AttributeId.Equals(attributeID))) ? attribute : (XmlAttribute) null;
  }

  private void eventLogHelper_AfterServerSettingsReload(IUserSession session)
  {
    this._arcService.LoadInternalSettings(session);
  }

  public void Unload()
  {
    this.eventLogHelper.RemoveAttributeWriteHandler((object) ConstsHolder.ArchiveAttrID, new WriteAttributeValueHandler(this.WriteArchiveAttributeValue));
    this.eventLogHelper.RemoveAttributeWriteHandler((object) ConstsHolder.InventoryNumberID, new WriteAttributeValueHandler(this.OnWriteInventoryNumberValue));
    this.eventLogHelper.GetObjectSecurity -= new GetObjectSecurityHandler(this.eventLogHelper_GetObjectSecurity);
    this.eventLogHelper.BeforeObjectPrintEvent -= new ObjectEventHandler(this.eventLogHelper_BeforeObjectPrintEvent);
    this.eventLogHelper.BeforeObjectSaveToDiskEvent -= new ObjectEventHandler(this.eventLogHelper_BeforeObjectSaveToDiskEvent);
    this.eventLogHelper.AfterPurgeObjectEvent -= new ObjectEventHandler(this.eventLogHelper_AfterPurgeObjectEvent);
    this.eventLogHelper.AfterCreateObjectEvent -= new AfterCreateObjectHandler(this.eventLogHelper_AfterCreateObjectEvent);
    this.eventLogHelper.AfterChangeObjectTypeEvent -= new ObjectTypeChangeHandler(this.eventLogHelper_AfterChangeObjectTypeEvent);
    this.eventLogHelper.CreateObjectEvent -= new ObjectEventHandler(this.eventLogHelper_CreateObjectEvent);
    this.eventLogHelper.AfterNextLCStepEvent -= new NextLCStepHandler(this.eventLogHelper_AfterNextLCStepEvent);
    this.eventLogHelper.BeforeNextLCStepEvent -= new NextLCStepHandler(this.eventLogHelper_BeforeNextLCStepEvent);
    this.eventLogHelper.AfterCreateRelationExEvent -= new CreateRelationExHandler(this.eventLogHelper_CreateRelationExEvent);
    this.eventLogHelper.AfterCommitCreationObjectEvent -= new ObjectEventHandler(this.eventLogHelper_AfterCommitCreationObjectEvent);
  }

  private void pluginManager_LoadComplete(object sender, EventArgs e)
  {
    this.FillArchiveAutoPlaceCache();
  }

  private void FillArchiveAutoPlaceCache()
  {
    if (!(ServerServices.GetService(typeof (IArchiveAutoPlaceCacheService)) is IArchiveAutoPlaceCacheService service))
      return;
    service.FillCache();
  }

  private void portalEvents_ObjectPublishedEvent(object sender, ObjectsPublishedEventArgs e)
  {
    IUserSession sessionTemporaryClone = this.dbTimedEvents.GetSystemSessionTemporaryClone("ArchivesServer.ObjectPublishedEvent");
    try
    {
      IDocumentCopyService customService1 = sessionTemporaryClone.GetCustomService(typeof (IDocumentCopyService)) as IDocumentCopyService;
      ICopiesService customService2 = sessionTemporaryClone.GetCustomService(typeof (ICopiesService)) as ICopiesService;
      if (customService1 == null || customService2 == null || !sessionTemporaryClone.Configurations.ReadBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.AUTO_CREATE_COPY, false, DBConfigMode.GlobalOnly))
        return;
      int int32 = Convert.ToInt32(sessionTemporaryClone.Configurations.ReadInteger(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.LEVEL, 0L, DBConfigMode.GlobalOnly));
      if (int32 == 0)
        return;
      foreach (long objectId in e.ObjectIDs)
      {
        IDBObject dbObject1 = sessionTemporaryClone.GetObject(objectId);
        if (MetaDataHelper.IsObjectTypeChildOf(dbObject1.TypeID, ConstsHolder.DocTypeID) && sessionTemporaryClone.GetLifecycleStep(dbObject1.LCStep).LevelID == int32)
        {
          IDBAttribute attributeById1 = dbObject1.GetAttributeByID(ConstsHolder.InventoryNumberID);
          if (attributeById1 != null && attributeById1.Value != DBNull.Value && !(attributeById1.AsString == string.Empty))
          {
            long objectID = customService2.GetDeliveryListID(sessionTemporaryClone.SessionGUID, dbObject1.ID);
            if (objectID == 0L)
              objectID = customService2.CreateDeliveryList(sessionTemporaryClone.SessionGUID, dbObject1.ObjectID);
            IDBObject dbObject2 = sessionTemporaryClone.GetObject(objectID);
            IDBAttribute attributeById2 = dbObject2.GetAttributeByID(ConstsHolder.SubscribersID);
            IDBAttribute attributeById3 = dbObject2.GetAttributeByID(ConstsHolder.NumberOfCopiesID);
            IDBAttribute attributeById4 = dbObject2.GetAttributeByID(ConstsHolder.ListOwnerID);
            IDBAttribute attributeById5 = dbObject2.GetAttributeByID(ConstsHolder.SubscribersDateID);
            IDBAttribute attributeById6 = dbObject2.GetAttributeByID(ConstsHolder.ActualCopyID);
            List<long> longList = new List<long>();
            List<int> intList = new List<int>();
            for (int index = 0; index < attributeById2.ValuesCount; ++index)
            {
              if (attributeById2.Values[index] != DBNull.Value && attributeById3.Values[index] != DBNull.Value)
              {
                longList.Add(Convert.ToInt64(attributeById2.Values[index]));
                intList.Add(Convert.ToInt32(attributeById3.Values[index]));
              }
            }
            foreach (long site in e.Sites)
            {
              if (!longList.Contains(site))
              {
                if (attributeById2.Values[0] == DBNull.Value && attributeById2.ValuesCount == 1)
                {
                  attributeById2.Value = (object) site;
                  attributeById3.Value = (object) 1;
                  attributeById5.Value = (object) DateTime.Now;
                  attributeById4.Value = (object) e.UserID;
                  attributeById6.Value = (object) 0L;
                }
                else
                {
                  attributeById2.AddValue((object) site);
                  attributeById3.AddValue((object) 1);
                  attributeById5.AddValue((object) DateTime.Now);
                  attributeById4.AddValue((object) e.UserID);
                  attributeById6.AddValue((object) 0L);
                }
              }
            }
            foreach (long site in e.Sites)
            {
              long copy = customService1.CreateCopies(objectId, 1, CopyKind.Electronic, (object) sessionTemporaryClone.SessionGUID)[0];
              IDocumentCopyService documentCopyService = customService1;
              long subscriberID = site;
              long listID = objectID;
              List<long> copiesID = new List<long>();
              copiesID.Add(copy);
              DateTime now = DateTime.Now;
              // ISSUE: variable of a boxed type
              __Boxed<Guid> sessionGuid = (System.ValueType) sessionTemporaryClone.SessionGUID;
              documentCopyService.SendCopies(subscriberID, 0L, listID, copiesID, now, 0L, (object) sessionGuid);
            }
          }
        }
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("ArchivesServer.ObjectPublishedEvent");
    }
  }

  private void eventLogHelper_AfterCacheReload(IDbManager db) => this.FillArchiveAutoPlaceCache();

  private void eventLogHelper_AfterNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    int lcLevelId = MetaDataHelper.GetLCLevelID("cad0000e-306c-11d8-b4e9-00304f19f545");
    if (nextstep.LevelID != lcLevelId)
      return;
    IDBObjectCollection objectCollection = session.GetObjectCollection(-1);
    if (!MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, ConstsHolder.DocTypeID))
      return;
    if (sender.IsBaseVersion && ConstsHolder.DeliveryListID != -1)
    {
      objectCollection.ObjectTypeID = ConstsHolder.DeliveryListID;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) sender.ID, LogicalOperators.NONE, 0, false)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        session.GetObject(int64, false)?.Delete(0L);
      }
    }
    if (ConstsHolder.CopyOfDocumentID == -1)
      return;
    objectCollection.ObjectTypeID = ConstsHolder.CopyOfDocumentID;
    DBRecordSetParams paramSet1 = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) sender.ID, LogicalOperators.AND, 0, false),
      new ConditionStructure(ConstsHolder.OriginalObjectVersionID, RelationalOperators.Equal, (object) Math.Abs(sender.ObjectID), LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet1).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      session.GetObject(int64, false)?.Delete(0L);
    }
  }

  private void eventLogHelper_BeforeNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    int lcLevelId = MetaDataHelper.GetLCLevelID("cad0000e-306c-11d8-b4e9-00304f19f545");
    if (nextstep.LevelID != lcLevelId || sender.ObjectType != ConstsHolder.CopyOfDocumentID)
      return;
    List<long> copiesID = new List<long>();
    copiesID.Add(sender.ObjectID);
    if (!(session.GetCustomService(typeof (IDocumentCopyService)) is IDocumentCopyService customService))
      return;
    customService.RemoveCopiesReferences(copiesID, (object) session);
  }

  private void eventLogHelper_CreateObjectEvent(IDBObject sender, IUserSession session)
  {
    if (!MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, ConstsHolder.DocTypeID) || sender.IsBaseVersion)
      return;
    ArchivesServerStartup.SetArchiveAttr(sender);
  }

  private void eventLogHelper_BeforeObjectSaveToDiskEvent(IDBObject sender, IUserSession session)
  {
    if (!ArchivesServerHolder.CacheDataSet.IsDocument(sender.ObjectType))
      return;
    IDBAttribute attributeById = sender.GetAttributeByID(ConstsHolder.ArchiveAttrID);
    if (attributeById == null || attributeById.IsNull || attributeById.AsInteger <= 0L)
      return;
    if (!(session.GetObject(attributeById.AsInteger, false) is ArchiveDBObject archiveDbObject))
      return;
    try
    {
      archiveDbObject._ArchivedObject = sender;
      archiveDbObject.AccessChecker.CheckAccess(ActionType.SaveToDisk);
    }
    catch
    {
      archiveDbObject.AddEvent(archiveDbObject.ObjectID, ActionType.SaveToDisk, EventlogRecordType.AccessDenied);
      throw;
    }
  }

  private void eventLogHelper_GetObjectSecurity(
    IDBObject sender,
    GetObjectSecurityEventArgs args,
    IUserSession session)
  {
    if (!ArchivesServerHolder.CacheDataSet.IsDocument(sender.ObjectType) && (!this._arcService.CheckArticlesAccessMode || !ArchivesServerHolder.CacheDataSet.IsArticle(sender.ObjectType) && !ArchivesServerHolder.CacheDataSet.IsProduct(sender.ObjectType) || MetaDataHelper.GetAttribute4ObjectType(sender.ObjectType, ConstsHolder.ArchiveAttrID) == null))
      return;
    IDBAttribute attributeById = sender.GetAttributeByID(ConstsHolder.ArchiveAttrID);
    if (attributeById == null || attributeById.IsNull || attributeById.AsInteger <= 0L || !(session.GetObject(attributeById.AsInteger, false) is ArchiveDBObject archiveDbObject) || args.SetAccessMode && !archiveDbObject.CheckAccess(ActionType.GetAccess, false, false))
      return;
    archiveDbObject._ArchivedObject = sender;
    if (args.SecurityList == null)
      args.SecurityList = new List<IDBSecurity>();
    args.SecurityList.Add(archiveDbObject.AccessChecker);
  }

  private void eventLogHelper_BeforeObjectPrintEvent(IDBObject sender, IUserSession session)
  {
    if (!ArchivesServerHolder.CacheDataSet.IsDocument(sender.ObjectType) && !MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, ConstsHolder.ComplectTechDocID))
      return;
    IDBAttribute attributeById = sender.GetAttributeByID(ConstsHolder.ArchiveAttrID);
    if (attributeById == null || attributeById.IsNull || attributeById.AsInteger <= 0L)
      return;
    if (!(session.GetObject(attributeById.AsInteger, false) is ArchiveDBObject archiveDbObject))
      return;
    try
    {
      archiveDbObject._ArchivedObject = sender;
      archiveDbObject.AccessChecker.CheckAccess(ActionType.Print);
    }
    catch
    {
      archiveDbObject.AddEvent(archiveDbObject.ObjectID, ActionType.Print, EventlogRecordType.AccessDenied);
      throw;
    }
  }

  private void eventLogHelper_AfterCreateObjectEvent(
    IDBObject newobject,
    IDBObject prototype,
    IUserSession session)
  {
    if (!MetaDataHelper.IsObjectTypeChildOf(newobject.ObjectType, ConstsHolder.DocTypeID))
      return;
    ArchivesServerStartup.PlaceDocumentIntoArchive(newobject, session);
  }

  private void eventLogHelper_AfterChangeObjectTypeEvent(
    IDBObject sender,
    int objecttypeid,
    IUserSession session)
  {
    if (!MetaDataHelper.IsObjectTypeChildOf(objecttypeid, MetaDataHelper.GetObjectTypeID(new Guid("cadd9712-306c-11d8-b4e9-00304f19f545"))))
      return;
    ArchivesServerStartup.PlaceDocumentIntoArchive(sender, session);
  }

  private void eventLogHelper_AfterCommitCreationObjectEvent(IDBObject sender, IUserSession session)
  {
    IDBAttribute attributeById = sender.GetAttributeByID(ConstsHolder.InventoryNumberID);
    if (attributeById == null || attributeById.Value.IsNullOrDBNull())
      return;
    ArchivesServerStartup.DocDeliveryListProcessing(session, sender, attributeById.Value.ToString(), session.UserID);
  }

  private void eventLogHelper_AfterPurgeObjectEvent(IDBObject sender, IUserSession session)
  {
    if (MetaDataHelper.GetObjectTypeParentID(sender.ObjectType) != ConstsHolder.ArcTypeID || !(ServerServices.GetService(typeof (IArchiveAutoPlaceCacheService)) is IArchiveAutoPlaceCacheService service))
      return;
    service.DeleteArchiveFromCache(sender.ObjectID);
  }

  private void eventLogHelper_CreateRelationExEvent(
    IDBRelation dbRelation,
    IUserSession session,
    int assignMode)
  {
    if (dbRelation.RelationType != MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545"))
      return;
    IECOServer service = ServerServices.GetService(typeof (IECOServer)) as IECOServer;
    ICopiesService customService = session.GetCustomService(typeof (ICopiesService)) as ICopiesService;
    if (service == null || customService == null || !service.GetDeliveryListParam())
      return;
    IDBObject dbObject = session.GetObject(dbRelation.ProjID);
    long deliveryListId = customService.GetDeliveryListID(session.SessionGUID, dbObject.ID);
    if (deliveryListId == 0L)
      return;
    customService.AddSubscrsFromEcoToDoc(deliveryListId, dbRelation.PartID, dbRelation.PartObjectID, session.SessionGUID);
  }

  private void WriteArchiveAttributeValue(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    IUserSession session = args.Session;
    if (!(attribute is DBAttribute dbAttribute) || !(dbAttribute.ParentObject is IDBObject parentObject) || args.Value == null || args.Value == DBNull.Value)
      return;
    long int64 = Convert.ToInt64(args.Value);
    IDBObject dbObject = args.Session.GetObject(int64);
    if (dbObject == null)
      return;
    if (this._arcService.CopyArcVisibility && dbObject.GetAttributeByID(ObjectsVisibilityHelper.AttrVisibilityId) != null && MetaDataHelper.GetAttribute4ObjectType(parentObject.ObjectType, ObjectsVisibilityHelper.AttrVisibilityId) != null)
    {
      ICacheDataset dbCache = (session as UserSession).DBCache;
      if (dbCache.IsArticle(parentObject.ObjectType) || dbCache.IsDocument(parentObject.ObjectType) || dbCache.IsProduct(parentObject.ObjectType))
      {
        IDBObject projObject = (IDBObject) null;
        if (ServerConsts.CopyProjectVisibility && parentObject.ProjectID > 0L)
          projObject = session.GetObject(parentObject.ProjectID, false);
        ObjectsVisibilityHelper.SetArcProjVisibility(parentObject, dbObject, projObject);
      }
    }
    if (!MetaDataHelper.IsObjectTypeChildOf(parentObject.ObjectType, ConstsHolder.DocTypeID))
      return;
    this._arcService.ValidatePlaceToArchive(dbObject, parentObject);
    this.WriteArchiveStructureAttrsToDoc(parentObject, dbObject, session);
    ArchivesServerStartup.RemoveBlobs(parentObject, int64, args.Session);
  }

  private void WriteArchiveStructureAttrsToDoc(
    IDBObject document,
    IDBObject archiveObject,
    IUserSession session)
  {
    IArchiveService service = ServiceUtils.GetService<IArchiveService>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    List<Guid> guidsFromAttribute = this.GetArchiveStructureGuidsFromAttribute(archiveObject);
    Dictionary<Guid, object> defaultAttrValues = service.GetArchiveStructureDefaultAttrValues(archiveObject.ObjectID, session.SessionGUID);
    List<Guid> absentInDocumentStructureAttrs = new List<Guid>();
    if (!(session is UserSession userSession))
      return;
    userSession.StartTransaction();
    try
    {
      this.WriteDefaultAttrStructureValuesInsteadDocumentsNullOrEmptyAttrValues(document, guidsFromAttribute, defaultAttrValues, ref absentInDocumentStructureAttrs);
      this.AddAbsentStructureAttributesToDoc(document, absentInDocumentStructureAttrs, defaultAttrValues);
      userSession.Commit();
    }
    catch (Exception ex)
    {
      userSession.Rollback();
      throw ex;
    }
  }

  private void AddAbsentStructureAttributesToDoc(
    IDBObject document,
    List<Guid> archiveStructureGuids,
    Dictionary<Guid, object> archiveStructureAttrDefaultValues)
  {
    if (archiveStructureGuids.Count <= 0)
      return;
    foreach (Guid archiveStructureGuid in archiveStructureGuids)
      this.AddDefaultAttrValueToDocument(document, archiveStructureGuid, archiveStructureAttrDefaultValues);
  }

  private void WriteDefaultAttrStructureValuesInsteadDocumentsNullOrEmptyAttrValues(
    IDBObject document,
    List<Guid> archiveStructureGuids,
    Dictionary<Guid, object> archiveStructureAttrDefaultValues,
    ref List<Guid> absentInDocumentStructureAttrs)
  {
    absentInDocumentStructureAttrs = new List<Guid>((IEnumerable<Guid>) archiveStructureGuids);
    foreach (AttributeValues attributesValue in document.GetAttributesValues(GetAttributeValuesModes.IncludeGuid))
    {
      Guid attributeGuid = attributesValue.AttributeGuid;
      if (archiveStructureGuids.Contains(attributeGuid))
      {
        absentInDocumentStructureAttrs.Remove(attributeGuid);
        if (AttributeValues.IsNullOrEmptyString(attributesValue.Values[0]))
        {
          object obj;
          archiveStructureAttrDefaultValues.TryGetValue(attributeGuid, out obj);
          if (obj != null)
            ArchivesServerStartup.WriteAttrValueToDocument(document, attributeGuid, obj);
        }
      }
    }
  }

  private void AddDefaultAttrValueToDocument(
    IDBObject document,
    Guid attrGuid,
    Dictionary<Guid, object> archiveStructureAttrDefaultValues)
  {
    object obj;
    archiveStructureAttrDefaultValues.TryGetValue(attrGuid, out obj);
    if (obj != null)
      ArchivesServerStartup.WriteAttrValueToDocument(document, attrGuid, obj);
    else
      document.Attributes.AddAttribute(MetaDataHelper.GetAttributeID((object) attrGuid), false);
  }

  private static void WriteAttrValueToDocument(
    IDBObject documentToPlaceInArchiveObject,
    Guid attrGuid,
    object value)
  {
    AttributeValues attributeValues = new AttributeValues(MetaDataHelper.GetAttributeID((object) attrGuid), value);
    documentToPlaceInArchiveObject.SetAttributesValues(new AttributeValues[1]
    {
      attributeValues
    });
  }

  private List<Guid> GetArchiveStructureGuidsFromAttribute(IDBObject archiveObject)
  {
    List<Guid> guidsFromAttribute = new List<Guid>();
    IDBAttribute attributeByGuid = archiveObject.GetAttributeByGuid(ConstsHolder.ArchiveStructureAttrGuid, true);
    for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
    {
      attributeByGuid.Index = index;
      if (!attributeByGuid.IsNull)
      {
        try
        {
          guidsFromAttribute.Add(new Guid(attributeByGuid.AsString));
        }
        catch (FormatException ex)
        {
        }
      }
    }
    return guidsFromAttribute;
  }

  internal static void RemoveBlobs(IDBObject obj, long arcID, IUserSession session)
  {
    if (ArchivesServerStartup.StorageIDService == null)
      return;
    long storageId = ArchivesServerStartup.StorageIDService.GetStorageID(arcID);
    if (storageId <= 0L)
      return;
    List<long> longList = new List<long>(1);
    for (int AttrIndex = 0; AttrIndex < obj.Attributes.Count; ++AttrIndex)
    {
      IDBAttribute attribute = obj.Attributes[AttrIndex];
      if (attribute.DataType == FieldTypes.ftBlob || attribute.DataType == FieldTypes.ftFile)
      {
        for (int index = 0; index < attribute.ValuesCount; ++index)
        {
          attribute.Index = index;
          long int64 = Convert.ToInt64(attribute.AsDouble);
          if (int64 != storageId && session.GetObject(int64) is IPerformer performer)
          {
            long[] fileIDs = new long[1]
            {
              attribute.AsInteger
            };
            long toStor = storageId;
            IDBAttribute sender = attribute;
            performer.Perform1(fileIDs, toStor, sender);
          }
        }
      }
    }
    if ((MetaDataHelper.GetObjectType(obj.ObjectType).Options & ObjectTypeOptions.CreateSnapshots) != ObjectTypeOptions.CreateSnapshots)
      return;
    IDbManager dataManager = (session as UserSession).DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT * FROM IMS_OBJ_SNAPATTRS WHERE F_OBJECT_ID = :objID", dataManager.Parameter("objID", (object) obj.ObjectID));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"]));
      if (attributeType != null && (attributeType.FieldType == FieldTypes.ftBlob || attributeType.FieldType == FieldTypes.ftFile))
      {
        long int64 = Convert.ToInt64(dataTable.Rows[index]["F_DOUBLE_VALUE"]);
        if (int64 != storageId && session.GetObject(int64) is IPerformer performer)
        {
          long[] fileIDs = new long[1]
          {
            Convert.ToInt64(dataTable.Rows[index]["F_INTEGER_VALUE"])
          };
          long toStor = storageId;
          performer.Perform1(fileIDs, toStor, (IDBAttribute) null);
        }
      }
    }
  }

  private void OnWriteInventoryNumberValue(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    if (this.dbTimedEvents == null)
      return;
    IUserSession session = attribute.Session;
    if (!(attribute is DBAttribute dbAttribute) || !(dbAttribute.ParentObject is IDBObject parentObject) || parentObject.IsCreationMode || parentObject.ObjectType != ConstsHolder.DocTypeID && !MetaDataHelper.IsObjectTypeChildOf(parentObject.ObjectType, ConstsHolder.DocTypeID))
      return;
    if (args.Value == null || args.Value.ToString() == string.Empty)
    {
      IDBAttribute byId1 = parentObject.Attributes.FindByID(ConstsHolder.OTDRegisteredDateID);
      if (byId1 != null)
        byId1.Value = (object) null;
      IDBAttribute byId2 = parentObject.Attributes.FindByID(ConstsHolder.OTDRegistratorID);
      if (byId2 == null)
        return;
      byId2.Value = (object) null;
    }
    else
    {
      parentObject.Attributes.AddAttribute(ConstsHolder.OTDRegisteredDateID, false, new object[1]
      {
        (object) DateTime.Now
      });
      parentObject.Attributes.AddAttribute(ConstsHolder.OTDRegistratorID, false, new object[1]
      {
        (object) args.Session.UserID
      });
      ArchivesServerStartup.DocDeliveryListProcessing(session, parentObject, args.Value.ToString(), args.Session.UserID);
    }
  }

  private static void PlaceDocumentIntoArchive(IDBObject newobject, IUserSession session)
  {
    if (!(ServerServices.GetService(typeof (IArchiveAutoPlaceCacheService)) is IArchiveAutoPlaceCacheService service))
      return;
    long archiveIdFromCaсhe = service.GetArchiveIdFromCaсhe(newobject.TypeID, newobject.OwnerID, session.SessionGUID);
    if (archiveIdFromCaсhe == 0L)
      return;
    AttributeValues attributeValues = new AttributeValues(ConstsHolder.ArchiveAttrID, (object) archiveIdFromCaсhe);
    newobject.SetAttributesValues(new AttributeValues[1]
    {
      attributeValues
    });
  }

  private static void DocDeliveryListProcessing(
    IUserSession session,
    IDBObject docObject,
    string inventoryNumber,
    long currentUserId)
  {
    if (!(ServerServices.GetService(typeof (ICustomServices)) is ICustomServices service1) || !(service1.GetService(typeof (ICopiesService)) is ICopiesService service2))
      return;
    DataTable dataTable = session.GetObjectCollection(ConstsHolder.DeliveryListID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) docObject.ID, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }));
    IDBObject dbObject1;
    if (dataTable == null || dataTable.Rows.Count == 0)
    {
      long deliveryList = service2.CreateDeliveryList(session.SessionGUID, docObject.ObjectID);
      dbObject1 = session.GetObject(deliveryList);
    }
    else
    {
      dbObject1 = session.GetObject(Convert.ToInt64(dataTable.Rows[0][0]));
      IDBAttribute dbAttribute = dbObject1.Attributes.AddAttribute(ConstsHolder.ActualCopyID, false);
      for (int index = 0; index < dbAttribute.ValuesCount; ++index)
      {
        dbAttribute.Index = index;
        dbAttribute.Clear();
      }
    }
    string message = string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_17"), (object) docObject.NameInMessages, (object) inventoryNumber);
    string subject = ArchivesServerHolder.rm.GetString("Archives.Server_18");
    if (!session.Configurations.ReadBool("Archive", "Settings", "EmailNotify", false, DBConfigMode.GlobalOnly))
      return;
    IEmailService customService = (IEmailService) session.GetCustomService(typeof (IEmailService));
    if (customService == null)
      return;
    EmailAccaunt[] accaunts = customService.GetAccaunts(currentUserId, false);
    if (accaunts == null || accaunts.Length == 0)
      return;
    Guid guid = accaunts[0].Guid;
    IDBAttribute dbAttribute1 = dbObject1.Attributes.AddAttribute(ConstsHolder.SubscribersID, false);
    if (dbAttribute1 == null)
      return;
    for (int index = 0; index < dbAttribute1.ValuesCount; ++index)
    {
      if (dbAttribute1.Values[index] != DBNull.Value)
      {
        long int64 = Convert.ToInt64(dbAttribute1.Values[index]);
        if (int64 != 0L)
        {
          IDBObject dbObject2 = session.GetObject(int64, false);
          if (dbObject2 != null)
          {
            IDBAttribute attributeByGuid = dbObject2.GetAttributeByGuid(new Guid("cad002de-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid != null && !(attributeByGuid.AsString == string.Empty))
            {
              string asString = attributeByGuid.AsString;
              customService.SendMessage(session.SessionGUID, guid, asString, subject, message);
            }
          }
        }
      }
    }
  }

  private static void SetArchiveAttr(IDBObject sender)
  {
    if (sender.VersionID == 0)
      return;
    IDBAttributeType attributeType = (sender as DBObject).ObjectTypeClass.GetAttributeType(ConstsHolder.ArchiveAttrID);
    object initValue = attributeType == null || attributeType.DefaultValue == null || !(attributeType.DefaultValue.ToString().Trim() != string.Empty) ? (object) DBNull.Value : (object) Convert.ToInt64(attributeType.DefaultValue);
    AttributeValues attributeValues = new AttributeValues(ConstsHolder.ArchiveAttrID, initValue);
    sender.SetAttributesValues(new AttributeValues[1]
    {
      attributeValues
    });
  }
}
