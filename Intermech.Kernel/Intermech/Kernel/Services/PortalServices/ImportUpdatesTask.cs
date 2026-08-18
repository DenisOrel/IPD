// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportUpdatesTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Briefcase;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportUpdatesTask : ImportTask
{
  private Dictionary<Guid, ImportedInfo> _links;
  private readonly IEventLogHelper _eventHelper;
  private RemoteData _exData;
  private readonly ObjectImportedEventHandler _objectImportedEvent;
  private readonly RelationImportedEventHandler _relationImportedEvent;
  private readonly ImportTaskCompletedEventHandler _importTaskCompletedEvent;
  private readonly ImportTaskErrorEventHandler _importTaskErrorEvent;
  private ImportReceipt _receipt;
  private long _packetID;
  private ImportVersionsModes _importVersionsMode = ImportVersionsModes.None;
  private string _offlineFileName;
  private List<long> _updateFolderKeyObjects;
  private List<Tuple<long, Guid, long>> _changesGroupNums;
  private List<Tuple<Guid, Guid, long, List<Guid>>> _contexts = new List<Tuple<Guid, Guid, long, List<Guid>>>();
  private List<Tuple<Guid, List<Guid>>> _importedCompositions = new List<Tuple<Guid, List<Guid>>>();
  private List<int> _loggingTransferObjectTypes;
  private Dictionary<long, Guid> _parentVersions = new Dictionary<long, Guid>();

  private string _temporaryFilesStorage
  {
    get
    {
      return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.Combine(PortalConsts.StorageFolder, "Client_ImportObjects"));
    }
  }

  public ImportUpdatesTask(
    IEventLogHelper eventHelper,
    ObjectImportedEventHandler objectImportedEvent,
    RelationImportedEventHandler relationImportedEvent,
    ImportTaskCompletedEventHandler importTaskCompletedEvent,
    ImportTaskErrorEventHandler importTaskErrorEvent)
  {
    this._links = new Dictionary<Guid, ImportedInfo>(0);
    this._updateFolderKeyObjects = new List<long>();
    this._eventHelper = eventHelper;
    this._objectImportedEvent = objectImportedEvent;
    this._relationImportedEvent = relationImportedEvent;
    this._importTaskCompletedEvent = importTaskCompletedEvent;
    this._importTaskErrorEvent = importTaskErrorEvent;
  }

  public ImportUpdatesTask(
    long userID,
    Guid userGuid,
    string name,
    TaskPriority priority,
    string updateGUID,
    TransferedObject[] units,
    ObjectImportedEventHandler objectImportedEvent,
    RelationImportedEventHandler relationImportedEvent,
    ImportTaskCompletedEventHandler importTaskCompletedEvent)
    : base(userID, userGuid, name, TaskType.ImportUpdates, priority, GuidHelper.IsGuid(updateGUID) ? updateGUID : Guid.NewGuid().ToString(), units)
  {
    this._eventHelper = ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    this._links = new Dictionary<Guid, ImportedInfo>(units.Length);
    this._objectImportedEvent = objectImportedEvent;
    this._relationImportedEvent = relationImportedEvent;
    this._importTaskCompletedEvent = importTaskCompletedEvent;
    this._changesGroupNums = new List<Tuple<long, Guid, long>>(units.Length);
    this._contexts = new List<Tuple<Guid, Guid, long, List<Guid>>>();
    if (GuidHelper.IsGuid(updateGUID))
      return;
    this._offlineFileName = updateGUID;
  }

  protected override void OnTaskStarted(
    IUserSession session,
    Guid connectionGuid,
    IPortalConnector connector)
  {
    this.WriteToDetailedLog("Начало " + this.Name);
    base.OnTaskStarted(session, connectionGuid, connector);
    this._loggingTransferObjectTypes = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true).LoggingTransferObjectTypesWithChildTypes;
    this.WriteToDetailedLog($"Получение импортируемых данных из портала ({this.Units.Length} юнитов) и создание или обновление соотвествующих объектов и связей в БД.");
  }

  protected override void Begining(
    IUserSession session,
    Guid connectionGuid,
    IPortalConnector connector,
    ITransferedObject unit)
  {
    string str = Path.Combine(this._temporaryFilesStorage, unit.GUID);
    Guid transferedGuid = new Guid(unit.GUID);
    if (!Directory.Exists(str))
      Directory.CreateDirectory(str);
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write($"ImportUpdatesTask.Begining TaskID = {this.TaskID} session = {session.SessionGUID} unit = {unit.GUID} ({unit.Category})");
    if (unit.DataFiles != null)
    {
      for (int index = 0; index < unit.DataFiles.Length; ++index)
      {
        bool flag = false;
        using (FileStream fileStream = new FileStream(Path.Combine(str, unit.DataFiles[index]), FileMode.Append, FileAccess.Write))
        {
          long startPosition = 0;
          if (fileStream.Length > 0L)
          {
            long attributesFileLength = connector.GetUpdateAttributesFileLength(connectionGuid, transferedGuid, unit.DataFiles[index]);
            if (fileStream.Length == attributesFileLength)
              flag = true;
            else if (attributesFileLength > 0L)
              startPosition = attributesFileLength - 1L;
          }
          if (!flag)
          {
            for (byte[] updateAttributesFile = connector.GetUpdateAttributesFile(connectionGuid, transferedGuid, unit.DataFiles[index], startPosition); updateAttributesFile != null; updateAttributesFile = connector.GetUpdateAttributesFile(connectionGuid, transferedGuid, unit.DataFiles[index], startPosition))
            {
              if (updateAttributesFile.Length != 0)
              {
                fileStream.Write(updateAttributesFile, 0, updateAttributesFile.Length);
                if (updateAttributesFile.Length >= PortalConsts.DefaultFileTransferBufferLength)
                  startPosition += (long) PortalConsts.DefaultFileTransferBufferLength;
                else
                  break;
              }
              else
                break;
            }
          }
        }
      }
    }
    switch (unit.Category)
    {
      case TransferedObjectCategory.AutoTransfer:
        this._exData = AttributesFile.GetAutoTransferAttributes(XmlHelper.ReadMainFile(unit, str));
        break;
      case TransferedObjectCategory.Packet:
        PacketTag tag = (PacketTag) unit.Tag;
        this._packetID = tag.PacketID;
        if (tag.ReceiptNeed)
          this._receipt = ImportReceipt.Create(session, this.TaskID, tag.Caption, tag.EnableSites, tag.PacketID, tag.PacketGuid);
        IDBAttribute attributeByGuid = session.GetObject(this.TaskID).GetAttributeByGuid(PortalConsts.attributeImportVersionsModes);
        if (attributeByGuid != null)
        {
          this._importVersionsMode = (ImportVersionsModes) attributeByGuid.AsInteger;
          break;
        }
        break;
      default:
        ImportedInfo iInfo = UnitImporter.Import(this._packetID != 0L ? (ImportArgs) new ImportPacketObjectArgs(session, unit, str, this._links, this.UserID, this.UserGuid, this._eventHelper, this._updateFolderKeyObjects, this._changesGroupNums, this._contexts, this._importedCompositions, this._receipt, this._importVersionsMode, this._parentVersions) : new ImportArgs(session, unit, str, this._links, this.UserID, this.UserGuid, this._eventHelper, this._updateFolderKeyObjects, this._changesGroupNums, this._contexts, this._importedCompositions, this._parentVersions));
        if (iInfo is ExtendedImportedInfo extendedImportedInfo)
          this.WriteToDetailedLog(extendedImportedInfo.ActionCaption);
        if (iInfo != null && !this._links.ContainsKey(iInfo.Guid))
        {
          this._links.Add(iInfo.Guid, iInfo);
          if (SiteTraceLog.Enabled)
            SiteTraceLog.Write($"unit imported: ObjectID = {iInfo.ObjectId} ObjectGuid = {iInfo.Guid} IsLink = {iInfo.IsLink}");
          if (iInfo.Category == TransferedObjectCategory.Object || iInfo.Category == TransferedObjectCategory.ObjectLink && !iInfo.IsLink)
          {
            if (iInfo.ObjectId != 0L && iInfo.ObjectId != -1L)
            {
              IDBObject importedObject = session.GetObject(iInfo.ObjectId);
              if (this._loggingTransferObjectTypes != null && this._loggingTransferObjectTypes.Contains(importedObject.ObjectType))
                (session as UserSession).EventLogHelper.AddEvent(importedObject.ObjectID, 0L, 1, 0L, importedObject.NameInMessages, $"Импорт в составе задачи \"{this.Name}\"", ActionType.Import, EventlogRecordType.Information, this.UserID, session.ComputerName, session);
              this.WriteToDetailedLog($"Обработка импорта объекта \"{importedObject.NameInMessages}\" (ид.версии={importedObject.ObjectID}) подписчиками.");
              this._objectImportedEvent((object) this, new ObjectImportedEventArgs(session, importedObject, iInfo.BaseVersionId, iInfo.SystemType, iInfo.IsNew));
              break;
            }
            this._eventHelper.AddToTrace($"Unit guid = {unit.GUID} category = {unit.Category} не импортирован. info.Guid = {iInfo.Guid}", Consts.traceAlways, string.Empty);
            break;
          }
          if ((iInfo.Category == TransferedObjectCategory.PacketRelation || iInfo.Category == TransferedObjectCategory.Relation) && iInfo.ObjectId > 0L)
          {
            KeyValuePair<Guid, ImportedInfo> keyValuePair = this._links.FirstOrDefault<KeyValuePair<Guid, ImportedInfo>>((System.Func<KeyValuePair<Guid, ImportedInfo>, bool>) (x => x.Value.Id.Equals(iInfo.BaseVersionId)));
            this._relationImportedEvent((object) this, new RelationImportedEventArgs(session, Convert.ToInt32(iInfo.Id), iInfo.ObjectId, iInfo.BaseVersionId, keyValuePair.Value != null && keyValuePair.Value.IsNew));
            break;
          }
          break;
        }
        break;
    }
    Directory.Delete(str, true);
  }

  protected override void OnTaskError(
    IUserSession session,
    IPortalConnector connector,
    Exception ex)
  {
    ImportTaskErrorEventHandler importTaskErrorEvent = this._importTaskErrorEvent;
    if (importTaskErrorEvent != null)
      importTaskErrorEvent((object) this, new ImportTaskErrorEventArgs(session, this.TaskID, ex));
    if (this._exData == null || this._exData.RemoteMessage != null)
      return;
    Guid connectGuid = connector.Login(session.SessionGUID);
    string author = string.Empty;
    try
    {
      author = connector.GetUpdateAuthor(connectGuid, this.updateGuid);
    }
    finally
    {
      if (connectGuid != Guid.Empty && connector != null)
        connector.Logout(connectGuid);
    }
    if (author == null || !(author != string.Empty))
      return;
    this.SendRemoteMessage(session, author, new RemoteMessage());
  }

  private void SendImportReceipt(IUserSession session)
  {
    ICustomPublisherService service1 = ServerServices.ServiceContainer.GetService<ICustomPublisherService>();
    IPublishRulesService service2 = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true);
    Guid sessionGuid = session.SessionGUID;
    ImportReceiptPublisher receiptPublisher = new ImportReceiptPublisher(this._receipt);
    string taskName = $"Публикация квитанции импорта для пакета {this._receipt.PacketGUID}";
    int receipt4packetTaskPriority = (int) service2.Receipt4packetTaskPriority;
    long taskID = service1.CustomPublish(sessionGuid, (IPublisher) receiptPublisher, taskName, (TaskPriority) receipt4packetTaskPriority);
    ServerServices.ServiceContainer.GetService<IPortalTasksQueue>().StartTask(taskID);
  }

  private void SendRemoteMessage(IUserSession session, string author, RemoteMessage message)
  {
    ICustomPublisherService service1 = ServerServices.ServiceContainer.GetService<ICustomPublisherService>();
    IPublishRulesService service2 = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true);
    Guid sessionGuid = session.SessionGUID;
    MessagePublisher messagePublisher = new MessagePublisher(new RemoteData(this._exData.Data, message), author);
    string taskName = $"Отчет импорта '{this.Name}'";
    int answerTaskPriority = (int) service2.AnswerTaskPriority;
    long taskID = service1.CustomPublish(sessionGuid, (IPublisher) messagePublisher, taskName, (TaskPriority) answerTaskPriority);
    ServerServices.ServiceContainer.GetService<IPortalTasksQueue>().StartTask(taskID);
  }

  protected override void OnTaskCompleted(IUserSession session, IPortalConnector connector)
  {
    IDBTimedEvents service1 = ServerServices.ServiceContainer.GetService<IDBTimedEvents>();
    IUserSession userSession = (IUserSession) null;
    Exception error = (Exception) null;
    string updateGUID = !string.IsNullOrEmpty(this._offlineFileName) ? this._offlineFileName : this.updateGuid;
    this.FireTaskStatusChanged(new TaskStatusChangedEventArgs(session, TaskStatus.ApplyingChangesSite, 51.0));
    string sessionName = $"ImportUpdatesTask.OnTaskCompleted_{Guid.NewGuid()}";
    try
    {
      userSession = service1.GetSystemSessionTemporaryClone(sessionName);
      if (SiteTraceLog.Enabled)
        SiteTraceLog.Write($"Create session = {userSession.SessionGUID} ({sessionName}) from \"ImportUpdatesTask.OnTaskCompleted\".");
      if (this._updateFolderKeyObjects != null && this._updateFolderKeyObjects.Count > 0)
      {
        this.WriteToDetailedLog($"Перестройка ключей папки классификатора у {this._updateFolderKeyObjects.Count}) объектов.");
        DBClassifier.RebuildKeys(userSession, this._updateFolderKeyObjects.ToArray());
      }
      if (this._parentVersions != null && this._parentVersions.Count > 0)
      {
        IDbManager dataManager = (userSession as UserSession).DataManager;
        foreach (KeyValuePair<long, Guid> parentVersion in this._parentVersions)
        {
          long num = 0;
          ImportedInfo importedInfo;
          if (this._links != null && this._links.TryGetValue(parentVersion.Value, out importedInfo))
          {
            num = importedInfo.ObjectId;
          }
          else
          {
            QuickObjectInfo objectInfo = userSession.GetObjectInfo(parentVersion.Value);
            if (!objectInfo.Empty && objectInfo.ObjectID != 0L)
              num = objectInfo.ObjectID;
          }
          if (num != 0L)
            DBHelper.ExecuteNonQuery(userSession, false, "INSERT INTO IMS_VERSIONS_TREE (F_PARENT_ID, F_OBJECT_ID) VALUES (:projID, :partID)", dataManager.Parameter("projID", (object) num), dataManager.Parameter("partID", (object) parentVersion.Key));
          else
            this.WriteToDetailedLog($"В пакете и в базе для объекта {parentVersion.Key} не найдена родительская версия {parentVersion.Value}.");
        }
      }
      ISitesCacheService customService1 = (ISitesCacheService) userSession.GetCustomService(typeof (ISitesCacheService));
      IPublishTypesConfiguration customService2 = (IPublishTypesConfiguration) userSession.GetCustomService(typeof (IPublishTypesConfiguration));
      List<Tuple<long, int>> objectIDs = new List<Tuple<long, int>>();
      List<Tuple<long, long>> tupleList = new List<Tuple<long, long>>();
      if (this._receipt != null)
        objectIDs.Add(new Tuple<long, int>(this._receipt.ReceiptID, MetaDataHelper.GetObjectTypeID(PortalConsts.objtypeReceipt)));
      foreach (KeyValuePair<Guid, ImportedInfo> link in this._links)
      {
        if ((link.Value.Category == TransferedObjectCategory.Object || link.Value.Category == TransferedObjectCategory.ObjectLink && !link.Value.IsLink) && link.Value.ObjectId > 0L)
        {
          QuickObjectInfo objectInfo = userSession.GetObjectInfo(link.Value.ObjectId);
          objectIDs.Add(new Tuple<long, int>(link.Value.ObjectId, objectInfo.ObjectTypeID));
        }
      }
      ImportTaskCompletedEventHandler taskCompletedEvent = this._importTaskCompletedEvent;
      if (taskCompletedEvent != null)
        taskCompletedEvent((object) this, new ImportTaskCompletedEventArgs(userSession, this._exData, objectIDs));
      if (this._contexts != null && this._contexts.Count > 0)
      {
        this.WriteToDetailedLog("Добавление импортированных объектов в соотвествующие контексты.");
        IDBEditingContextsServerService service2 = ServerServices.ServiceContainer.GetService<IDBEditingContextsServerService>();
        IDBRelationCollection relationCollection = (IDBRelationCollection) null;
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cadd9645-306c-11d8-b4e9-00304f19f545");
        IDBTransactions customService3 = (IDBTransactions) userSession.GetCustomService(typeof (IDBTransactions));
        customService3.StartTransaction();
        try
        {
          foreach (Tuple<Guid, Guid, long, List<Guid>> context in this._contexts)
          {
            long objectId = this._links[context.Item1].ObjectId;
            long linkedContextNumber = context.Item3;
            List<long> fIDs = new List<long>();
            List<long> versionIDs = new List<long>();
            foreach (Guid key1 in context.Item4)
            {
              ImportedInfo link1 = this._links[key1];
              if (link1.SystemType == SystemTypes.Search)
              {
                if (relationCollection == null)
                  relationCollection = userSession.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"));
                DataTable dataTable = relationCollection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
                {
                  (object) -12
                }), link1.ObjectId);
                if (dataTable.Rows.Count > 0)
                {
                  Guid key2 = new Guid(Convert.ToString(dataTable.Rows[0][0]));
                  if (this._links.ContainsKey(key2))
                  {
                    ImportedInfo link2 = this._links[key2];
                    if (link2 != null && !versionIDs.Contains(link2.ObjectId) && !service2.ExistsInContext((object) userSession, objectId, link2.ObjectId))
                    {
                      fIDs.Add(link2.Id);
                      versionIDs.Add(link2.ObjectId);
                      this.CreateEcoLinkAttribute(session, link2.ObjectId, objectId, attributeTypeId);
                    }
                  }
                  else
                    continue;
                }
              }
              if (!service2.ExistsInContext((object) userSession, objectId, link1.ObjectId))
              {
                fIDs.Add(link1.Id);
                versionIDs.Add(link1.ObjectId);
                if (link1.SystemType == SystemTypes.Search)
                  this.CreateEcoLinkAttribute(session, link1.ObjectId, objectId, attributeTypeId);
              }
            }
            if (fIDs.Count > 0)
              (service2 as DBEditingContextsService).AddToContext((object) userSession, objectId, linkedContextNumber, (IList<long>) fIDs, (IList<long>) versionIDs, true, true, false);
          }
          customService3.Commit();
        }
        catch (Exception ex)
        {
          customService3.Rollback();
          throw;
        }
      }
      if (this._receipt != null)
      {
        this.WriteToDetailedLog($"Создание квитанции импорта для импортированного пакета пакета {this._receipt.PacketGUID}.");
        IDBObject receipt = userSession.GetObject(this._receipt.ReceiptID);
        if (this._receipt.Content != null)
          this._receipt.SaveContent(receipt);
        receipt.GetAttributeByGuid(PortalConsts.attributeReceiptActualFlag).AsBoolean = true;
      }
      this.FireTaskStatusChanged(new TaskStatusChangedEventArgs(session, TaskStatus.ApplyingChangesPortal, 90.0));
      if (this._receipt != null)
      {
        this.WriteToDetailedLog($"Публикация квитанции импорта для пакета {this._receipt.PacketGUID}.");
        this.SendImportReceipt(session);
      }
      this.WriteToDetailedLog("Завершение импорта на портале.");
      Guid connectGuid = connector.Login(session.SessionGUID);
      string author = string.Empty;
      try
      {
        if (this._exData != null && this._exData.RemoteMessage == null)
          author = connector.GetUpdateAuthor(connectGuid, updateGUID);
        connector.EndUpdateUnit(connectGuid, updateGUID);
        if (this._receipt != null)
          connector.PacketImportComplete(connectGuid, this._receipt.PacketID);
      }
      finally
      {
        if (connectGuid != Guid.Empty && connector != null)
          connector.Logout(connectGuid);
      }
      if (!(author != string.Empty))
        return;
      this.WriteToDetailedLog("Отправка отчета импорта для узла-инициатора удаленного процесса.");
      this.SendRemoteMessage(session, author, new RemoteMessage());
    }
    catch (Exception ex)
    {
      error = ex;
      throw;
    }
    finally
    {
      List<long> processedObjects = new List<long>(this._links.Count);
      foreach (KeyValuePair<Guid, ImportedInfo> link in this._links)
      {
        if ((link.Value.Category == TransferedObjectCategory.ObjectLink || link.Value.Category == TransferedObjectCategory.Object) && !processedObjects.Contains(link.Value.ObjectId))
          processedObjects.Add(link.Value.ObjectId);
      }
      ICustomImport service3 = ServerServices.ServiceContainer.GetService<ICustomImport>();
      if (service3 != null)
      {
        this.WriteToDetailedLog("ФИнальная обработка импортированных данных подписчиками.");
        service3.FireAfterImportObjects((object) this, new AfterCustomImportEventArgs(userSession, processedObjects, error));
      }
      PortalServicesSessionHelper.LogoutSession(userSession, sessionName, "ImportUpdatesTask.OnTaskCompleted");
      this.CloseLog();
    }
  }

  private void CreateEcoLinkAttribute(
    IUserSession session,
    long objectId,
    long contextID,
    int attributeEcoLinkID)
  {
    IDBObject dbObject = session.GetObject(objectId);
    (dbObject.GetAttributeByID(attributeEcoLinkID) ?? dbObject.Attributes.AddAttribute(attributeEcoLinkID, false)).AsInteger = contextID;
  }

  public override byte[] Save(IUserSession session, IDBObject backupObject)
  {
    this.SaveUpdateGuid(backupObject);
    if (this._receipt != null)
      this._receipt.Save(session);
    using (MemoryStream output = new MemoryStream())
    {
      BinaryWriter binaryWriter = new BinaryWriter((Stream) output, Encoding.UTF8);
      try
      {
        new ImportTaskDataV2().SaveLinks(this._links, binaryWriter);
        if (this._exData != null)
        {
          binaryWriter.Write(1);
          this.WriteString(binaryWriter, this._exData.Data);
          if (this._exData.RemoteMessage != null)
          {
            binaryWriter.Write(1);
            this.WriteString(binaryWriter, this._exData.RemoteMessage.Message);
            this.WriteString(binaryWriter, this._exData.RemoteMessage.AdditionalData);
          }
          else
            binaryWriter.Write(0);
        }
        else
          binaryWriter.Write(0);
        if (this._updateFolderKeyObjects != null && this._updateFolderKeyObjects.Count > 0)
        {
          binaryWriter.Write(this._updateFolderKeyObjects.Count);
          for (int index = 0; index < this._updateFolderKeyObjects.Count; ++index)
            binaryWriter.Write(this._updateFolderKeyObjects[index]);
        }
        else
          binaryWriter.Write(0);
        binaryWriter.Write(this._packetID);
        binaryWriter.Write((int) this._importVersionsMode);
        if (this._changesGroupNums.Count > 0)
        {
          binaryWriter.Write(this._changesGroupNums.Count);
          foreach (Tuple<long, Guid, long> changesGroupNum in this._changesGroupNums)
          {
            binaryWriter.Write(changesGroupNum.Item1);
            this.WriteGuid(binaryWriter, changesGroupNum.Item2);
            binaryWriter.Write(changesGroupNum.Item3);
          }
        }
        else
          binaryWriter.Write(0);
        if (this._contexts != null && this._contexts.Count > 0)
        {
          binaryWriter.Write(this._contexts.Count);
          foreach (Tuple<Guid, Guid, long, List<Guid>> context in this._contexts)
          {
            this.WriteGuid(binaryWriter, context.Item1);
            this.WriteGuid(binaryWriter, context.Item2);
            binaryWriter.Write(context.Item3);
            this.WriteListGuid(binaryWriter, context.Item4);
          }
        }
        else
          binaryWriter.Write(0);
        if (this._importedCompositions != null && this._importedCompositions.Count > 0)
        {
          binaryWriter.Write(this._importedCompositions.Count);
          foreach (Tuple<Guid, List<Guid>> importedComposition in this._importedCompositions)
          {
            this.WriteGuid(binaryWriter, importedComposition.Item1);
            this.WriteListGuid(binaryWriter, importedComposition.Item2);
          }
        }
        else
          binaryWriter.Write(0);
        this.WriteString(binaryWriter, this._offlineFileName);
      }
      finally
      {
        binaryWriter.Flush();
      }
      return output.ToArray();
    }
  }

  public override void Load(IUserSession session, IDBObject backupObject, byte[] bytes)
  {
    this.LoadUpdateGuid(backupObject);
    DataTable dataTable = session.GetRelationCollection(session.IdentHelper.SimpleRelationTypeID).ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) MetaDataHelper.GetObjectTypeID(PortalConsts.objtypeReceipt), LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }), this.TaskID);
    if (dataTable.Rows.Count == 1)
      this._receipt = ImportReceipt.Open(session, Convert.ToInt64(dataTable.Rows[0][0]));
    using (BinaryReader binaryReader = new BinaryReader((Stream) new MemoryStream(bytes), Encoding.UTF8))
    {
      int count = binaryReader.ReadInt32();
      IImportTaskData importTaskData = (IImportTaskData) null;
      if (count >= 0)
      {
        importTaskData = (IImportTaskData) new ImportTaskDataV1();
      }
      else
      {
        if (count == -2)
          importTaskData = (IImportTaskData) new ImportTaskDataV2();
        count = binaryReader.ReadInt32();
      }
      this._links = importTaskData.ReadLinks(count, binaryReader);
      if (binaryReader.ReadInt32() == 1)
      {
        string data = this.ReadString(binaryReader);
        RemoteMessage message = (RemoteMessage) null;
        if (binaryReader.ReadInt32() == 1)
          message = new RemoteMessage(this.ReadString(binaryReader), this.ReadString(binaryReader));
        this._exData = new RemoteData(data, message);
      }
      int capacity1 = binaryReader.ReadInt32();
      this._updateFolderKeyObjects = new List<long>(capacity1);
      if (capacity1 > 0)
      {
        for (int index = 0; index < capacity1; ++index)
          this._updateFolderKeyObjects.Add(binaryReader.ReadInt64());
      }
      this._packetID = binaryReader.ReadInt64();
      this._importVersionsMode = (ImportVersionsModes) binaryReader.ReadInt32();
      int num1 = binaryReader.ReadInt32();
      this._changesGroupNums = new List<Tuple<long, Guid, long>>();
      for (int index = 0; index < num1; ++index)
        this._changesGroupNums.Add(new Tuple<long, Guid, long>(binaryReader.ReadInt64(), this.ReadGuid(binaryReader), binaryReader.ReadInt64()));
      int num2 = binaryReader.ReadInt32();
      this._contexts = new List<Tuple<Guid, Guid, long, List<Guid>>>();
      for (int index = 0; index < num2; ++index)
        this._contexts.Add(new Tuple<Guid, Guid, long, List<Guid>>(this.ReadGuid(binaryReader), this.ReadGuid(binaryReader), binaryReader.ReadInt64(), this.ReadListGuid(binaryReader)));
      int capacity2 = binaryReader.ReadInt32();
      this._importedCompositions = new List<Tuple<Guid, List<Guid>>>(capacity2);
      for (int index = 0; index < capacity2; ++index)
        this._importedCompositions.Add(new Tuple<Guid, List<Guid>>(this.ReadGuid(binaryReader), this.ReadListGuid(binaryReader)));
      this._offlineFileName = this.ReadString(binaryReader);
    }
  }

  public override void OnTaskDelete(Guid connectionGuid, IPortalConnector connector)
  {
    if (string.IsNullOrEmpty(this.updateGuid))
      return;
    connector.EndUpdateUnit(connectionGuid, this.updateGuid);
  }
}
