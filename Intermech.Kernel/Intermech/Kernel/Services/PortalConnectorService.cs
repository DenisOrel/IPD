// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalConnectorService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using Intermech.Kernel.Services.PortalServices;
using Intermech.Localization;
using Intermech.Portal.Connector;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Kernel.Services;

public class PortalConnectorService : LongLifeObject, IPortalConnector
{
  private IPortalProxy _proxy;
  private SiteInfo _currentSite;
  private IUserSession _replicatorSession;

  public PortalConnectorService(
    IUserSession replicatorSession,
    ConnectionSettings settings,
    SiteInfo currentSite)
  {
    this._currentSite = currentSite;
    this._proxy = PortalProxyHelper.GetProxy(settings.Url, settings.ProxyAddress, settings.ProxyPort, settings.AsyncSupported);
    this.IsOffline = ConnectionHelper.IsOffline(settings.Url);
    this._replicatorSession = replicatorSession;
  }

  public Guid Login(Guid sessionGuid)
  {
    return UserSession.GetSessionByID(sessionGuid) is UserSession sessionById ? this.Login(sessionById) : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1053"), (object) sessionGuid));
  }

  private Guid Login(UserSession session)
  {
    if (this._currentSite == null)
      throw new Exception(LocalizationHolder.rm.GetString("Kernel_1054"));
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write($"start login session={session.SessionGUID} loginName={session.LoginName}");
    string g = this._proxy.LoginEx2(PortalConsts.GlobalLoginName(this._currentSite.Code, session.LoginName), session.Password, Convert.ToString((object) this._currentSite.GUID), session.ComputerName, session.TimeZoneOffset.Hours);
    if (string.IsNullOrEmpty(g))
      throw new LoginException(LocalizationHolder.rm.GetString("Kernel_1055"));
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write("end login connectGuid=" + g);
    return new Guid(g);
  }

  public void Logout(Guid connectGuid)
  {
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write($"start logout connectGuid={connectGuid}");
    this._proxy.Logout(connectGuid.ToString());
  }

  public PortalObjectType[] GetObjectTypesTree(Guid connectGuid)
  {
    return this._proxy.GetObjectTypesTree(connectGuid.ToString());
  }

  public DateTime LastModifyMetadata(Guid connectGuid)
  {
    return DateTimeHelper.ToDateTime(this._proxy.LastModifyMetadata(connectGuid.ToString()));
  }

  public void EndUpdateUnit(Guid connectGuid, string updateGUID)
  {
    this._proxy.EndUpdateUnit(connectGuid.ToString(), updateGUID);
  }

  public void EndUpdateUnit(Guid connectGuid, string updateGUID, string[] guids)
  {
    this._proxy.EndUpdateUnitEx(connectGuid.ToString(), updateGUID, guids);
  }

  public string[] GetUpdates(Guid connectGuid, Guid sessionGuid)
  {
    IPublishTypesConfiguration customService = (UserSession.GetSessionByID(sessionGuid) as UserSession).GetCustomService(typeof (IPublishTypesConfiguration)) as IPublishTypesConfiguration;
    return this._proxy.GetUpdates(connectGuid.ToString(), customService.GetCompositionApplicabilities());
  }

  public TransferedObject[] GetUpdateUnit(Guid connectGuid, string updateGUID)
  {
    return this._proxy.GetUpdateUnit(connectGuid.ToString(), updateGUID);
  }

  public void StartUpdateUnit(Guid connectGuid, string updateGUID)
  {
    this._proxy.StartUpdateUnit(connectGuid.ToString(), updateGUID);
  }

  public byte[] GetUpdateAttributesFile(
    Guid connectGuid,
    Guid transferedGuid,
    string fileName,
    long startPosition)
  {
    return this._proxy.GetUpdateAttributesFile(connectGuid.ToString(), transferedGuid.ToString(), fileName, startPosition);
  }

  public long GetUpdateAttributesFileLength(Guid connectGuid, Guid transferedGuid, string fileName)
  {
    return this._proxy.GetUpdateAttributesFileLength(connectGuid.ToString(), transferedGuid.ToString(), fileName);
  }

  public long StartPublishingTask(
    Guid connectGuid,
    string taskName,
    string enabledSites,
    long packetID)
  {
    return this._proxy.StartPublishingTask2(connectGuid.ToString(), taskName, enabledSites, packetID);
  }

  public long StartPublishingTask(Guid connectGuid, string taskName, string enabledSites)
  {
    return this.StartPublishingTask(connectGuid, taskName, enabledSites, 0L);
  }

  public void PublishUnit(Guid connectGuid, long taskID, TransferedObject unit)
  {
    this._proxy.PublishUnit(connectGuid.ToString(), taskID, unit);
  }

  public void TransferPublishUnitFile(
    Guid connectGuid,
    string unitGuid,
    string fileName,
    byte[] bytes,
    bool continuation)
  {
    this._proxy.TransferPublishUnitFile(connectGuid.ToString(), unitGuid, fileName, bytes, continuation);
  }

  public void CompletePublish(Guid connectGuid, long taskID, bool deleteTask)
  {
    this._proxy.CompletePublishEx(connectGuid.ToString(), taskID, deleteTask);
  }

  public void DeletePublishTask(Guid connectGuid, long taskID)
  {
    this._proxy.DeletePublishTask(connectGuid.ToString(), taskID);
  }

  public void DeletePublishTask(Guid connectGuid, long taskID, int deleteMode)
  {
    this._proxy.DeletePublishTaskEx(connectGuid.ToString(), taskID, deleteMode);
  }

  public TaskStatus GetTaskStatus(Guid connectGuid, long taskID)
  {
    return (TaskStatus) this._proxy.GetTaskStatus(connectGuid.ToString(), taskID);
  }

  public PublishObjectsTable SelectPublishObjects(
    Guid connectGuid,
    int objectType,
    DBQueryParams dbParams)
  {
    return this._proxy.SelectPublishObjects(connectGuid.ToString(), objectType, dbParams);
  }

  public void ImportPackets(
    Guid sessionGuid,
    TaskPriority priority,
    long[] packetIDs,
    ImportVersionsModes importVersionsMode,
    bool startImmediately)
  {
    new ImportPacketsThread(this._proxy).CreateTask((ImportThreadArgs) new ImportPacketThreadArgs(UserSession.GetSessionByID(sessionGuid), Guid.NewGuid().ToString(), 0L, packetIDs, importVersionsMode, startImmediately));
  }

  public void ImportObjects(
    Guid sessionGuid,
    TaskPriority priority,
    long[] objectsIDs,
    int[] filteredTypes,
    bool setOwner,
    bool autoUpdate,
    SelectCompositionType compositionType,
    bool startImmediately)
  {
    new ImportObjectsThread(this._proxy).CreateTask((ImportThreadArgs) new ImportObjectsThreadArgs(UserSession.GetSessionByID(sessionGuid), Guid.NewGuid().ToString(), 0L, objectsIDs, filteredTypes, setOwner, autoUpdate, compositionType, startImmediately));
  }

  public long[] DeleteObjects(Guid sessionGuid, long[] objectIDs)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    Guid guid = Guid.Empty;
    try
    {
      guid = this.Login(sessionById);
      string[] strArray = this._proxy.DeleteObjectsEx(guid.ToString(), objectIDs);
      if (strArray != null)
      {
        List<long> longList = new List<long>(strArray.Length);
        for (int index = 0; index < strArray.Length; ++index)
        {
          IDBObject dbObject = sessionById.GetObject(new Guid(strArray[index]), false);
          if (dbObject != null)
          {
            if (!string.IsNullOrEmpty(dbObject.SiteID))
              (dbObject as DBObject).SetSiteID(string.Empty);
            dbObject.GetAttributeByGuid(PortalConsts.attributePublicationNecessary)?.Delete(0L);
            longList.Add(dbObject.ObjectID);
          }
        }
        if (longList.Count > 0)
          return longList.ToArray();
      }
      return (long[]) null;
    }
    finally
    {
      if (guid != Guid.Empty)
        this._proxy.Logout(guid.ToString());
    }
  }

  public PublishAttribute[] GetObjectAttributes(
    Guid connectGuid,
    long objectID,
    params string[] attrIDs)
  {
    return this._proxy.GetObjectAttributes(connectGuid.ToString(), objectID, attrIDs);
  }

  public PublishAttribute[] GetRelationAttributes(
    Guid connectGuid,
    long ralationID,
    params string[] attrIDs)
  {
    return this._proxy.GetRelationAttributes(connectGuid.ToString(), ralationID, attrIDs);
  }

  public PublishObjectsTable SelectComposition(
    Guid sessionGuid,
    Guid connectGuid,
    long objectID,
    DBQueryParams dbParams,
    int countLevels)
  {
    UserSession.GetSessionByID(sessionGuid);
    return this._proxy.SelectComposition(connectGuid.ToString(), objectID, dbParams, countLevels);
  }

  public PortalAttributeType[] GetPublishRelationAttributes(Guid connectGuid)
  {
    return this._proxy.GetPublishRelationAttributes(connectGuid.ToString());
  }

  public AttributePossibleValues[] GetAttributePossibleValues(Guid connectGuid)
  {
    return this._proxy.GetAttributePossibleValues(connectGuid.ToString());
  }

  public DateTime GetLastSitesInfoUpdate(Guid connectGuid)
  {
    return this._proxy.GetLastSitesInfoUpdate(connectGuid.ToString());
  }

  public SiteInfo[] GetSitesInfo(Guid connectGuid)
  {
    return this._proxy.GetSitesInfo(connectGuid.ToString());
  }

  public void ChangeUserPassword(Guid sessionGuid, string login, string newPassword)
  {
    this.PortalMethod(sessionGuid, (PortalConnectorService.UnsafePortalConnectorMethodHandler) (connectionGuid => this._proxy.ChangeUserPassword(connectionGuid.ToString(), PortalConsts.GlobalLoginName(this._currentSite.Code, login), newPassword)));
  }

  public void AddUser(
    Guid sessionGuid,
    string userName,
    string login,
    string password,
    Guid userGuid)
  {
    this.PortalMethod(sessionGuid, (PortalConnectorService.UnsafePortalConnectorMethodHandler) (connectionGuid => this._proxy.AddUserEx(connectionGuid.ToString(), PortalConsts.GlobalUserName(this._currentSite.Caption, userName), PortalConsts.GlobalLoginName(this._currentSite.Code, login), password, userGuid.ToString())));
  }

  public void DeleteUser(Guid sessionGuid, string login)
  {
    this.PortalMethod(sessionGuid, (PortalConnectorService.UnsafePortalConnectorMethodHandler) (connectionGuid => this._proxy.DeleteUser(connectionGuid, PortalConsts.GlobalLoginName(this._currentSite.Code, login))));
  }

  public ProcessTemplateInfo[] GetProcessTemplates(Guid siteGuid)
  {
    Guid guid = Guid.Empty;
    try
    {
      guid = this.Login(this._replicatorSession as UserSession);
      return this._proxy.GetProcessTemplates(guid.ToString(), siteGuid);
    }
    finally
    {
      if (guid != Guid.Empty)
        this._proxy.Logout(guid.ToString());
    }
  }

  public string[] OwnComplete(Guid connectionGuid, string[] objectGUIDs, string ownerSites)
  {
    return this._proxy.OwnCompleteExG(connectionGuid.ToString(), objectGUIDs, ownerSites, (string[]) null, (string[]) null, false, true, false);
  }

  public string[] OwnComplete(
    Guid sessionGuid,
    long[] objectIDs,
    string ownerSites,
    CompositionApplicabilities applic,
    bool withComposition,
    bool autoUpdate)
  {
    return this.PortalMethod<string[]>(sessionGuid, (PortalConnectorService.UnsafePortalConnectorMethodHandler<string[]>) (connectionGuid => this._proxy.OwnComplete(connectionGuid, objectIDs, ownerSites, withComposition, false, autoUpdate)));
  }

  public string[][] SelectPublishObjectsFlt(
    Guid sessionGuid,
    int objectType,
    string[] columns,
    int recordCount,
    string[] attributes,
    int[] relationalOperators,
    string[] values,
    string[] values2,
    int[] logicalOperators,
    int[] groupIDs,
    bool[] caseSensitives)
  {
    return this.PortalMethod<string[][]>(sessionGuid, (PortalConnectorService.UnsafePortalConnectorMethodHandler<string[][]>) (connectionGuid => this._proxy.SelectPublishObjectsFlt(connectionGuid, objectType, columns, recordCount, attributes, relationalOperators, values, values2, logicalOperators, groupIDs, caseSensitives)));
  }

  public PublishObjectsTable GetSiteUsers(
    Guid connectionGuid,
    Guid siteGuid,
    DBQueryParams dbParams)
  {
    return this._proxy.GetSiteUsers(connectionGuid.ToString(), siteGuid.ToString(), dbParams);
  }

  public void ImportUsers(Guid sessionGuid, long[] userIDs)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    string updateGuid = Guid.NewGuid().ToString();
    string str = string.Format(userIDs.Length == 1 ? LocalizationHolder.rm.GetString("Kernel_1059") : LocalizationHolder.rm.GetString("Kernel_1060"), (object) userIDs[0]);
    ImportTask importTask = new ImportTask(sessionById.UserID, sessionById.UserGUID, LocalizationHolder.rm.GetString("Kernel_1058") + str, TaskType.ImportObjects, TaskPriority.Normal, updateGuid, (TransferedObject[]) null);
    importTask.Status = TaskStatus.Waiting;
    ImportTask newTask = importTask;
    Guid connectGuid = Guid.Empty;
    try
    {
      connectGuid = this.Login(sessionById.SessionGUID);
      this._proxy.ImportUsers(connectGuid.ToString(), updateGuid.ToString(), userIDs);
      BackupStorage.CreateTask((IUserSession) sessionById, (ITask) newTask, out IDBObject _);
    }
    finally
    {
      if (connectGuid != Guid.Empty)
        this.Logout(connectGuid);
    }
  }

  public bool IsAdmin(Guid sessionGuid)
  {
    return this.PortalMethod<bool>(sessionGuid, (PortalConnectorService.UnsafePortalConnectorMethodHandler<bool>) (connectionGuid => this._proxy.IsAdmin(connectionGuid)));
  }

  public string GetUpdateAuthor(Guid connectGuid, string updateGUID)
  {
    return this._proxy.GetUpdateAuthor(connectGuid.ToString(), updateGUID);
  }

  public long CreatePacket(
    Guid connectionGuid,
    long taskID,
    Guid guid,
    string name,
    string designation,
    string note,
    string enableSites)
  {
    return this._proxy.CreatePacket(connectionGuid.ToString(), taskID, guid.ToString(), name, designation, note, enableSites);
  }

  public DataTable GetPacketContent(Guid connectionGuid, long packetID)
  {
    return this.GetReceiptContentDataTable(this._proxy.GetPacketContent(connectionGuid.ToString(), packetID));
  }

  public PublicationReceipt[] GetImportReceipts(Guid connectionGuid, long packetID)
  {
    return this._proxy.GetImportReceipts(connectionGuid.ToString(), packetID);
  }

  public DataTable GetReceiptContent(Guid connectionGuid, long receiptID)
  {
    return this.GetReceiptContentDataTable(this._proxy.GetReceiptContent(connectionGuid.ToString(), receiptID));
  }

  private DataTable GetReceiptContentDataTable(byte[] data)
  {
    if (data == null || data.Length == 0)
      return (DataTable) null;
    using (MemoryStream inStream = new MemoryStream(data))
    {
      inStream.Position = 0L;
      using (ImChunkedStream imChunkedStream = new ImChunkedStream())
      {
        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) imChunkedStream, (Stream) inStream);
        imChunkedStream.Position = 0L;
        return (DataTable) new BinaryFormatter().Deserialize((Stream) imChunkedStream);
      }
    }
  }

  public long[] GetImportComposition(
    Guid sessionGuid,
    long[] objectID,
    int[] filteredTypes,
    int countLevels)
  {
    return this.PortalMethod<long[]>(sessionGuid, (PortalConnectorService.UnsafePortalConnectorMethodHandler<long[]>) (connectionGuid => this._proxy.GetImportComposition(connectionGuid, objectID, Intermech.Kernel.Services.PortalServices.Helper.GetObjectTypeGuidsList(filteredTypes), countLevels)));
  }

  public string[] AutoImportComplete(Guid sessionGuid, long[] objectIDs, bool withComposition)
  {
    return this.PortalMethod<string[]>(sessionGuid, (PortalConnectorService.UnsafePortalConnectorMethodHandler<string[]>) (connectionGuid => this._proxy.AutoImportComplete(connectionGuid, objectIDs, withComposition)));
  }

  public void DeletePackets(Guid connectionGuid, long[] packetIDs)
  {
    this._proxy.DeletePackets(connectionGuid.ToString(), packetIDs);
  }

  public bool IsOffline { get; private set; }

  public string[] OfflineImportFilesList
  {
    get
    {
      return !this.IsOffline ? (string[]) null : this._proxy.GetUpdates(string.Empty, (CompositionApplicabilities) null);
    }
  }

  public string PortalVersion => this._proxy.Version();

  private T PortalMethod<T>(
    Guid sessionGuid,
    PortalConnectorService.UnsafePortalConnectorMethodHandler<T> method)
  {
    if (method == null)
      throw new ArgumentNullException();
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    Guid guid = Guid.Empty;
    try
    {
      guid = this.Login(sessionById);
      return method(guid.ToString());
    }
    finally
    {
      if (guid != Guid.Empty)
        this._proxy.Logout(guid.ToString());
    }
  }

  private void PortalMethod(
    Guid sessionGuid,
    PortalConnectorService.UnsafePortalConnectorMethodHandler method)
  {
    if (method == null)
      throw new ArgumentNullException();
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    Guid guid = Guid.Empty;
    try
    {
      guid = this.Login(sessionById);
      method(guid.ToString());
    }
    finally
    {
      if (guid != Guid.Empty)
        this._proxy.Logout(guid.ToString());
    }
  }

  public void PacketImportComplete(Guid connectGuid, long packetID)
  {
    this._proxy.PacketImportComplete(connectGuid.ToString(), packetID);
  }

  public void ChangeUserPassword(Guid sessionGuid, string login, PswPackage newPassword)
  {
    this.PortalMethod(sessionGuid, (PortalConnectorService.UnsafePortalConnectorMethodHandler) (connectionGuid => this._proxy.ChangeUserPasswordEx(connectionGuid.ToString(), PortalConsts.GlobalLoginName(this._currentSite.Code, login), newPassword)));
  }

  public void AddUser(
    Guid sessionGuid,
    string userName,
    string login,
    PswPackage password,
    Guid userGuid)
  {
    this.PortalMethod(sessionGuid, (PortalConnectorService.UnsafePortalConnectorMethodHandler) (connectionGuid => this._proxy.AddUserEx2(connectionGuid.ToString(), PortalConsts.GlobalUserName(this._currentSite.Caption, userName), PortalConsts.GlobalLoginName(this._currentSite.Code, login), password, userGuid.ToString())));
  }

  private delegate void UnsafePortalConnectorMethodHandler(string connectionGuid);

  private delegate T UnsafePortalConnectorMethodHandler<T>(string connectionGuid);
}
