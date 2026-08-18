// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.PublishTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Kernel.Services.PortalServices;

public class PublishTask : ExportTask
{
  protected ExtendedPublishOptions options;
  private Packet4Publish _packet;
  private long _receiptID;
  private long _packetID;
  private string[] _ownerCompleteGuids;

  public PublishTask(IDBAttribute attributeTaskFiles)
    : base(attributeTaskFiles)
  {
  }

  public PublishTask(
    long userID,
    Guid userGuid,
    string name,
    TaskType taskType,
    TaskPriority priority,
    List<PublishCompositionObject> publishedObjects,
    ExtendedPublishOptions options,
    ITransferedObject[] units,
    IDBAttribute attributeTaskFiles)
    : this(userID, userGuid, name, taskType, priority, publishedObjects, options, units, (Packet4Publish) null, 0L, attributeTaskFiles)
  {
  }

  public PublishTask(
    long userID,
    Guid userGuid,
    string name,
    TaskType taskType,
    TaskPriority priority,
    List<PublishCompositionObject> publishedObjects,
    ExtendedPublishOptions options,
    ITransferedObject[] units,
    Packet4Publish packet,
    long createReceiptID,
    IDBAttribute attributeTaskFiles)
    : base(userID, userGuid, name, taskType, priority, units, options.EnableSites, publishedObjects, attributeTaskFiles)
  {
    this.options = options;
    this._packet = packet;
    this._receiptID = createReceiptID;
    this._packetID = 0L;
  }

  protected override void OnTaskStarted(
    IUserSession session,
    Guid connectionGuid,
    IPortalConnector connector)
  {
    if (this.portalTaskID == 0L)
    {
      this.WriteToDetailedLog("Начало " + this.Name);
      this.portalTaskID = connector.StartPublishingTask(connectionGuid, this.Name, this.enabledSites);
    }
    if (this._packet != null)
    {
      this.WriteToDetailedLog($"Создание публикуемого пакета {this._packet.Name} {this._packet.Designation} ({this._packet.GUID}) на портале.");
      this._packetID = connector.CreatePacket(connectionGuid, this.portalTaskID, this._packet.GUID, this._packet.Name, this._packet.Designation, this._packet.Note, this.options.EnableSites);
    }
    base.OnTaskStarted(session, connectionGuid, connector);
    this.WriteToDetailedLog($"Передача публикуемых данных на портал. Количество передаваемых юнитов {this.Units.Length}");
  }

  protected override void AfterCompletePublish(
    IUserSession session,
    Guid connectionGuid,
    IPortalConnector connector)
  {
    if (!this.options.OwnerSite.HasValue)
      return;
    List<string> stringList = SqlHelper.GetObjectGUIDs((ICollection<long>) this.publishedObjectIDs.FindAll((Predicate<Tuple<long, bool, bool>>) (x => !x.Item3)).ConvertAll<long>((Converter<Tuple<long, bool, bool>, long>) (x => x.Item1)), (session as UserSession).DataManager).ConvertAll<string>((Converter<Tuple<long, string>, string>) (x => x.Item2));
    this.WriteToDetailedLog($"Передача владения узлу {this.options.OwnerSite} опубликованных объектов в количестве {stringList.Count} шт.");
    this._ownerCompleteGuids = connector.OwnComplete(connectionGuid, stringList.ToArray(), Convert.ToString((object) this.options.OwnerSite));
  }

  protected virtual void SetCodes(IDBObject publishObject, bool isLink, char currentSiteCode)
  {
    if (this._ownerCompleteGuids != null && !isLink)
    {
      if (!Array.Exists<string>(this._ownerCompleteGuids, (Predicate<string>) (guid => guid.Equals(publishObject.ObjectGUID.ToString(), StringComparison.CurrentCultureIgnoreCase))))
        return;
      string siteID = string.IsNullOrEmpty(publishObject.SiteID) ? currentSiteCode.ToString() : publishObject.SiteID[0].ToString();
      (publishObject as DBObject).SetSiteID(siteID);
    }
    else
    {
      if (publishObject.SiteID != null && publishObject.SiteID.Length != 0)
        return;
      (publishObject as DBObject).SetSiteID(string.Format("{0}{0}{0}", (object) currentSiteCode));
    }
  }

  protected override void HandlePublishedObjects(IUserSession session)
  {
    IDBTransactions customService1 = session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
    try
    {
      customService1.StartTransaction();
      List<long> longList1 = new List<long>();
      List<long> longList2 = new List<long>();
      if (this._receiptID != 0L)
        session.GetObject(this._receiptID).GetAttributeByGuid(PortalConsts.attributeReceiptActualFlag).AsBoolean = true;
      if (this.publishedObjectIDs != null)
      {
        List<int> typesWithChildTypes = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true).LoggingTransferObjectTypesWithChildTypes;
        ISitesCacheService customService2 = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
        IPublishTypesConfiguration customService3 = (IPublishTypesConfiguration) session.GetCustomService(typeof (IPublishTypesConfiguration));
        byte[] data = (byte[]) null;
        if (this.options.AutoReplication)
          data = PublishOptionsHelper.Serialize(this.options);
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(PortalConsts.attributePublicationNecessary);
        this.WriteToDetailedLog($"Установка атрибутов опубликованным объектам в количестве {this.publishedObjectIDs.Count} шт.:");
        foreach (Tuple<long, bool, bool> publishedObjectId in this.publishedObjectIDs)
        {
          IDBObject publishObject = session.GetObject(publishedObjectId.Item1);
          this.WriteToDetailedLog($"{publishObject.NameInMessages} (ид.версии={publishObject.ObjectID})");
          this.SetCodes(publishObject, publishedObjectId.Item3, customService2.Info.Code);
          if (typesWithChildTypes != null && typesWithChildTypes.Contains(publishObject.ObjectType))
            (session as UserSession).EventLogHelper.AddEvent(publishObject.ObjectID, 0L, 1, 0L, publishObject.NameInMessages, $"Публикация на портал в составе задачи \"{this.Name}\" для узлов: {this.enabledSites}", ActionType.Export, EventlogRecordType.Information, this.UserID, session.ComputerName, session);
          IDBAttribute dbAttribute = publishObject.GetAttributeByID(attributeTypeId);
          if (dbAttribute != null || SiteIDHelper.IsOwner(customService2.Info.Code, publishObject.SiteID))
          {
            if (dbAttribute == null)
              dbAttribute = publishObject.Attributes.AddAttribute(attributeTypeId, false);
            dbAttribute.AsInteger = 0L;
          }
          if (publishedObjectId.Item2)
          {
            if (this.options.AutoReplication)
            {
              this.WriteToDetailedLog("Запись опций публикации для автореплицируемого объекта.");
              IBlobWriter blobWriter = (IBlobWriter) (publishObject.GetAttributeByGuid(PortalConsts.attributePublishOptions, false) ?? publishObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributePublishOptions), false));
              blobWriter.OpenBlob(new BlobInformation((long) data.Length, (long) data.Length, DateTime.Now, string.Empty, ArcMethods.NotPacked, string.Empty), false);
              blobWriter.WriteDataBlock(data);
              if (SiteIDHelper.IsOwner(customService2.Info.Code, publishObject.SiteID))
                longList2.Add(publishedObjectId.Item1);
            }
            else
              longList1.Add(publishedObjectId.Item1);
          }
        }
      }
      ISelectionsService customService4 = session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
      if (longList1.Count > 0)
      {
        this.WriteToDetailedLog($"Исключение опубликованных объектов из выборки автопубликуемых объектов в количестве {longList1.Count} шт.");
        IDBObject dbObject = session.GetObject(PortalConsts.selectionAutoPublish);
        customService4.ExcludeObjects((object) session.SessionGUID, dbObject.ObjectID, longList1.ToArray());
      }
      if (longList2.Count > 0)
      {
        this.WriteToDetailedLog($"Включение опубликованных объектов из выборки автопубликуемых объектов  в количестве {longList2.Count} шт.");
        customService4.IncludeObjects((object) session.SessionGUID, PortalConsts.selectionAutoPublish, longList2.ToArray());
      }
      customService1.Commit();
    }
    catch
    {
      customService1.Rollback();
      throw;
    }
  }

  protected override void SaveData(BinaryWriter bw)
  {
    base.SaveData(bw);
    byte[] buffer = PublishOptionsHelper.Serialize(this.options);
    bw.Write(buffer.Length);
    bw.Write(buffer);
    if (this._packet != null)
    {
      bw.Write(1);
      this.WriteString(this._packet.Designation, bw);
      this.WriteString(this._packet.Name, bw);
      this.WriteString(this._packet.Note, bw);
      this.WriteString(this._packet.GUID.ToString(), bw);
    }
    else
      bw.Write(0);
    bw.Write(this._packetID);
    bw.Write(this._receiptID);
  }

  protected override void LoadData(BinaryReader br)
  {
    base.LoadData(br);
    int count = br.ReadInt32();
    this.options = PublishOptionsHelper.Deserialize(br.ReadBytes(count));
    if (br.ReadInt32() == 1)
      this._packet = new Packet4Publish(this.ReadString(br), this.ReadString(br), this.ReadString(br), new Guid(this.ReadString(br)));
    this._packetID = br.ReadInt64();
    this._receiptID = br.ReadInt64();
  }

  private void WriteString(string str, BinaryWriter bw)
  {
    if (!string.IsNullOrEmpty(str))
    {
      bw.Write(str.Length);
      bw.Write(str.ToCharArray());
    }
    else
      bw.Write(0);
  }

  public override void OnTaskDelete(Guid connectionGuid, IPortalConnector connector)
  {
    if (this.portalTaskID == 0L)
      return;
    this.WriteToDetailedLog($"Удаление задачи {this.portalTaskID} на портале.");
    connector.DeletePublishTask(connectionGuid, this.portalTaskID, 0);
  }
}
