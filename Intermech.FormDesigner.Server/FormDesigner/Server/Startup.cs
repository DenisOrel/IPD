// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.Startup
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.FormDesigner.Server;

public class Startup : IPackage
{
  public string Name => LocalizationHolder.rm.GetString("FormDesigner.Server_1");

  public void Load(IServiceProvider serviceProvider)
  {
    StartupHolder.ServiceProvider = serviceProvider;
    if (ServerServices.GetService(typeof (IDBObjectService)) is ICreatorContainer service1)
      service1.AddCreator((object) StartupHolder.DataEditFormsType, (object) new DBFormObjectCreator());
    if (ServerServices.GetService(typeof (IDBObjectCollectionService)) is ICreatorContainer service2)
      service2.AddCreator((object) StartupHolder.DataEditFormsType, (object) new FormDBObjectCollectionCreator());
    StartupHolder.EventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    StartupHolder.EventLogHelper.AfterCheckinEvent += new ObjectEventHandler(this.EventLogHelper_AfterCheckInEvent);
    StartupHolder.EventLogHelper.AfterCheckoutEvent += new ObjectEventHandler(this.EventLogHelper_AfterCheckOutEvent);
    StartupHolder.EventLogHelper.AfterUndoCheckoutEvent += new ObjectEventHandler(this.EventLogHelper_AfterUndoCheckOutEvent);
    StartupHolder.EventLogHelper.AfterCreateObjectTypeEvent += new AfterCreateObjectTypeHandler(this.OnEventLogHelper_AfterCreateObjectTypeEvent);
    StartupHolder.EventLogHelper.AfterDeleteObjectTypeEvent += new DeleteObjectTypeHandler(this.OnEventLogHelper_AfterDeleteObjectTypeEvent);
    StartupHolder.EventLogHelper.BeforeCombineAttributesEvent += new CombineAttributesHandler(this.EventLogHelper_BeforeCombineAttributesEvent);
    StartupHolder.EventLogHelper.AfterCombineAttributesEvent += new CombineAttributesHandler(this.EventLogHelper_AfterCombineAttributesEvent);
    StartupHolder.EventLogHelper.AddAttributeWriteHandler((object) 0, new WriteAttributeValueHandler(this.OnEventLogHelper_AttributeWriteHandler));
    IFormDesignerService serviceInstance1 = (IFormDesignerService) new FormDesignerService();
    ServerServices.AddService(typeof (IFormDesignerService), (object) serviceInstance1);
    ServerServices.AddService(typeof (IFormDesignerServer), (object) serviceInstance1);
    IServerFormsCache serviceInstance2 = (IServerFormsCache) new ServerFormsCache(StartupHolder.EventLogHelper);
    ServerServices.AddService(typeof (IServerFormsCache), (object) serviceInstance2);
    if (ServerServices.GetService(typeof (ICustomServices)) is ICustomServices service3)
    {
      service3.AddService(typeof (IFormDesignerService), (object) serviceInstance1);
      service3.AddService(typeof (IServerFormsCache), (object) serviceInstance2);
    }
    IServerSynchronizersManager service4 = ServerServices.ServiceContainer.GetService(typeof (IServerSynchronizersManager)) as IServerSynchronizersManager;
    FormsCacheSynchronizer serviceInstance3 = new FormsCacheSynchronizer();
    FormsCacheSynchronizer synchronizer = serviceInstance3;
    service4.RegisterSynchronizer((IServerSynchronizer) synchronizer);
    ServerServices.AddService(typeof (IFormsCacheSynchronizer), (object) serviceInstance3);
    if (!(ServerServices.GetService(typeof (ICategoryExportManager)) is ICategoryExportManager service5))
      return;
    service5.RegisterCategoryExport(3, (ICategoryExport) new BriefcaseSupport());
  }

  public void Unload()
  {
    StartupHolder.EventLogHelper.AfterCheckinEvent -= new ObjectEventHandler(this.EventLogHelper_AfterCheckInEvent);
    StartupHolder.EventLogHelper.AfterCheckoutEvent -= new ObjectEventHandler(this.EventLogHelper_AfterCheckOutEvent);
    StartupHolder.EventLogHelper.AfterUndoCheckoutEvent -= new ObjectEventHandler(this.EventLogHelper_AfterUndoCheckOutEvent);
    StartupHolder.EventLogHelper.AfterCreateObjectTypeEvent -= new AfterCreateObjectTypeHandler(this.OnEventLogHelper_AfterCreateObjectTypeEvent);
    StartupHolder.EventLogHelper.AfterDeleteObjectTypeEvent -= new DeleteObjectTypeHandler(this.OnEventLogHelper_AfterDeleteObjectTypeEvent);
    StartupHolder.EventLogHelper.BeforeCombineAttributesEvent -= new CombineAttributesHandler(this.EventLogHelper_BeforeCombineAttributesEvent);
    StartupHolder.EventLogHelper.AfterCombineAttributesEvent -= new CombineAttributesHandler(this.EventLogHelper_AfterCombineAttributesEvent);
  }

  private void OnEventLogHelper_AfterCreateObjectTypeEvent(
    IDBObjectType sender,
    IUserSession session)
  {
    if (!(ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is FormDesignerService service))
      return;
    service.UpdateHandlerList();
  }

  private void OnEventLogHelper_AfterDeleteObjectTypeEvent(
    IDBObjectType sender,
    IUserSession session)
  {
    if (!(ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is FormDesignerService service))
      return;
    service.DeleteHandlersAfterDeleteBaseType(sender.ObjectType, AttributableElements.Object);
    service.UpdateHandlerList();
  }

  private void EventLogHelper_AfterCheckInEvent(IDBObject sender, IUserSession session)
  {
    if (sender is DBFormObject dbFormObject)
      dbFormObject.ChangeCheckInInfo();
    this.RemoteAttributableFromCache(sender as DBAttributable);
  }

  private void EventLogHelper_AfterCheckOutEvent(IDBObject sender, IUserSession session)
  {
    if (!(sender is DBFormObject dbFormObject))
      return;
    dbFormObject.ChangeCheckOutInfo();
  }

  private void EventLogHelper_AfterUndoCheckOutEvent(IDBObject sender, IUserSession session)
  {
    if (!(sender is DBFormObject dbFormObject))
      return;
    dbFormObject.UndoCheckOutInfo();
  }

  private void EventLogHelper_BeforeCombineAttributesEvent(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode,
    List<string> log)
  {
    if (fromAttribute == null)
      return;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(StartupHolder.DataEditFormsType);
    if (childrenIdRecursive == null)
      return;
    string strAttrGuid = Convert.ToString((object) fromAttribute.GUID);
    foreach (int objectType in childrenIdRecursive)
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(objectType);
      if (objectCollection != null)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_CHKOUT_BY
        });
        DataTable dataTable = objectCollection.Select(paramSet);
        if (dataTable != null && dataTable.Rows.Count != 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64 = Convert.ToInt64(row[1]);
            if (int64 != 0L)
            {
              long objectID = Math.Abs(Convert.ToInt64(row[0]));
              IDBObject dbObject = session.GetObject(objectID, false);
              if (dbObject != null)
              {
                if (this.CheckExistenceAttr(session, dbObject, strAttrGuid))
                {
                  string objectName = MetaDataHelper.GetObjectName(dbObject.ObjectType);
                  throw new Exception(string.Format(LocalizationHolder.rm.GetString("Attribute_Union_FinishEditingForm"), (object) fromAttribute.Name, (object) toAttribute.Name, (object) dbObject.Caption, (object) objectName, (object) Convert.ToString(dbObject.ObjectID)));
                }
                if (int64 != session.UserID)
                {
                  IDBObject objectActualCopy = session.GetObjectActualCopy(-objectID, false);
                  if (objectActualCopy != null && this.CheckExistenceAttr(session, objectActualCopy, strAttrGuid))
                  {
                    string objectName = MetaDataHelper.GetObjectName(objectActualCopy.ObjectType);
                    throw new Exception(string.Format(LocalizationHolder.rm.GetString("Attribute_Union_FinishEditingForm"), (object) fromAttribute.Name, (object) toAttribute.Name, (object) objectActualCopy.Caption, (object) objectName, (object) Convert.ToString(objectActualCopy.ObjectID)));
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  private void EventLogHelper_AfterCombineAttributesEvent(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode,
    List<string> log)
  {
    if (fromAttribute == null || toAttribute == null)
      return;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(StartupHolder.DataEditFormsType);
    if (childrenIdRecursive == null)
      return;
    string str = Convert.ToString((object) fromAttribute.GUID);
    string strNewAttrGuid = Convert.ToString((object) toAttribute.GUID);
    foreach (int objectType in childrenIdRecursive)
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(objectType);
      if (objectCollection != null)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_CHKOUT_BY
        });
        DataTable dataTable = objectCollection.Select(paramSet);
        if (dataTable != null && dataTable.Rows.Count != 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64_1 = Convert.ToInt64(row[1]);
            if (int64_1 <= 0L || int64_1 == session.UserID)
            {
              long int64_2 = Convert.ToInt64(row[0]);
              IDBObject objectActualCopy = session.GetObjectActualCopy(int64_2, false);
              if (objectActualCopy.CheckoutBy == session.UserID)
                this.ReplaceAttr(session, objectActualCopy, str, strNewAttrGuid);
              else if (this.CheckExistenceAttr(session, objectActualCopy, str))
              {
                try
                {
                  IDBObject dbObject = objectActualCopy.CheckOut();
                  this.ReplaceAttr(session, dbObject, str, strNewAttrGuid);
                  dbObject.CheckIn();
                }
                catch
                {
                }
              }
            }
          }
        }
      }
    }
  }

  private bool CheckExistenceAttr(IUserSession session, IDBObject obj, string strAttrGuid)
  {
    bool flag = false;
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(new Guid("cad0011d-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid != null)
    {
      if (!attributeByGuid.IsNull)
      {
        try
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            BlobProcReader blobProcReader = new BlobProcReader(attributeByGuid, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
            blobProcReader.ReadData(session);
            if (blobProcReader.Result)
            {
              if (memoryStream.Length > 0L)
              {
                memoryStream.Position = 0L;
                using (XmlReader reader = (XmlReader) new XmlTextReader((Stream) memoryStream))
                {
                  XmlDocument xmlDocument = new XmlDocument();
                  xmlDocument.Load(reader);
                  flag = xmlDocument.InnerText.IndexOf(strAttrGuid) > -1;
                }
              }
            }
          }
        }
        catch
        {
        }
      }
    }
    return flag;
  }

  private void ReplaceAttr(
    IUserSession session,
    IDBObject obj,
    string strOldAttrGuid,
    string strNewAttrGuid)
  {
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(new Guid("cad0011d-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null)
      return;
    if (attributeByGuid.IsNull)
      return;
    try
    {
      bool flag = false;
      XmlDocument xmlDocument = new XmlDocument();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        BlobProcReader blobProcReader = new BlobProcReader(attributeByGuid, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
        blobProcReader.ReadData(session);
        if (blobProcReader.Result)
        {
          if (memoryStream.Length > 0L)
          {
            memoryStream.Position = 0L;
            using (XmlReader reader = (XmlReader) new XmlTextReader((Stream) memoryStream))
            {
              xmlDocument.Load(reader);
              string str = xmlDocument.InnerXml.Replace(strOldAttrGuid, strNewAttrGuid);
              xmlDocument.InnerXml = str;
              flag = true;
            }
          }
        }
      }
      if (!flag)
        return;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (XmlWriter w = (XmlWriter) new XmlTextWriter((Stream) memoryStream, Encoding.UTF8))
        {
          xmlDocument.Save(w);
          w.Flush();
          memoryStream.Position = 0L;
          BlobInformation aBlobInformation = new BlobInformation(memoryStream.Length, 0L, DateTime.Now, Convert.ToString(obj.ObjectID) + ".xml", ArcMethods.ZLibPacked, LocalizationHolder.rm.GetString("FormDesigner_109"));
          new BlobProcWriter(attributeByGuid, 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData(session);
        }
      }
    }
    catch
    {
    }
  }

  private void OnEventLogHelper_AttributeWriteHandler(
    IDBAttribute attribute,
    AttributeValueEventArgs args)
  {
    if (!(attribute is DBAttribute dbAttribute))
      return;
    DBAttributable parentObject = dbAttribute.ParentObject;
    if (parentObject == null || (parentObject.AttributesState & Consts.PurgeMode) == Consts.PurgeMode || (parentObject.AttributesState & Consts.CheckInMode) == Consts.CheckInMode || (parentObject.AttributesState & Consts.CheckOutMode) == Consts.CheckOutMode || (parentObject.AttributesState & Consts.CreateMode) == Consts.CreateMode || (parentObject.AttributesState & 512 /*0x0200*/) == 512 /*0x0200*/ || (parentObject.AttributesState & 1024 /*0x0400*/) == 1024 /*0x0400*/ || (parentObject.AttributesState & 2048 /*0x0800*/) == 2048 /*0x0800*/ || (parentObject.AttributesState & 4096 /*0x1000*/) == 4096 /*0x1000*/ || (parentObject.AttributesState & Consts.RelationConstraintMode) == Consts.RelationConstraintMode)
      return;
    this.RemoteAttributableFromCache(parentObject);
  }

  private void RemoteAttributableFromCache(DBAttributable dbAttributable)
  {
    if (dbAttributable == null)
      return;
    AttributableElements attributableElements = AttributableElements.None;
    long num = 0;
    if (dbAttributable is DBObject dbObject)
    {
      if (dbObject.IsCreationMode)
        return;
      attributableElements = AttributableElements.Object;
      num = dbObject.ObjectID;
    }
    else if (dbAttributable is DBRelation dbRelation)
    {
      attributableElements = AttributableElements.Relation;
      num = dbRelation.RelationID;
    }
    if (attributableElements == AttributableElements.None || !(ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is FormDesignerService service))
      return;
    List<object> objectList = new List<object>();
    foreach (KeyValuePair<OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.Key<VersionCacheItem>, OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.CachedForms> keyValuePair in (IEnumerable<KeyValuePair<OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.Key<VersionCacheItem>, OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.CachedForms>>) service._usrVerCache.CacheData)
    {
      OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.Key<VersionCacheItem> key = keyValuePair.Key;
      VersionCacheItem versionCacheItem = key?.Value;
      if (versionCacheItem != null)
      {
        switch (attributableElements)
        {
          case AttributableElements.Object:
            if (versionCacheItem.ID == num)
            {
              objectList.Add((object) key);
              continue;
            }
            continue;
          case AttributableElements.Relation:
            if (versionCacheItem.ID == num || versionCacheItem.RelationID == num)
            {
              objectList.Add((object) key);
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    foreach (OnlineCacheBase<VersionCacheItem, FormDesignerService.FormAccess>.Key<VersionCacheItem> key in objectList)
    {
      if (key != null)
        service._usrVerCache.CacheData.Remove(key);
    }
  }
}
