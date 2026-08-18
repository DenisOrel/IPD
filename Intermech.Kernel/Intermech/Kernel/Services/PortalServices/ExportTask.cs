// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ExportTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;


namespace Intermech.Kernel.Services.PortalServices;

public class ExportTask : Task
{
  protected long portalTaskID;
  protected string enabledSites;
  protected List<Tuple<long, bool, bool>> publishedObjectIDs;
  protected IDBAttribute attributeTaskFiles;
  private IBlobReader _reader;

  public ExportTask(IDBAttribute attributeTaskFiles)
  {
    this.attributeTaskFiles = attributeTaskFiles;
  }

  internal long PortalTaskID => this.portalTaskID;

  public ExportTask(
    long userID,
    Guid userGuid,
    string name,
    TaskType type,
    TaskPriority priority,
    ITransferedObject[] units,
    string enabledSites,
    List<PublishCompositionObject> publishedObjects,
    IDBAttribute attributeTaskFiles)
    : base(userID, userGuid, name, type, priority, units)
  {
    this.enabledSites = enabledSites;
    this.attributeTaskFiles = attributeTaskFiles;
    if (publishedObjects != null)
      this.publishedObjectIDs = publishedObjects.ConvertAll<Tuple<long, bool, bool>>((Converter<PublishCompositionObject, Tuple<long, bool, bool>>) (pco => new Tuple<long, bool, bool>(pco.ObjectID, pco.Root, pco.Include == IncludeTypes.ObjectLink)));
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(PortalConsts.objtypeUpdateTasks, PortalConsts.attributeTaskTransferEnabled);
    bool defaultValue = attribute4ObjectType == null || Convert.ToBoolean(attribute4ObjectType.DefaultValue);
    if (defaultValue)
      this.Enabled = true;
    else
      this.Enabled = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true).IsEnableTrueTaskForSites(enabledSites, defaultValue);
  }

  protected virtual void AfterCompletePublish(
    IUserSession session,
    Guid connectionGuid,
    IPortalConnector connector)
  {
  }

  private void CloseBlob()
  {
    if (this._reader == null)
      return;
    this._reader.CloseBlob();
  }

  protected override void OnTaskError(
    IUserSession session,
    IPortalConnector connector,
    Exception ex)
  {
    base.OnTaskError(session, connector, ex);
    this.CloseBlob();
    this.CloseLog();
  }

  protected sealed override void OnTaskCompleted(IUserSession session, IPortalConnector connector)
  {
    this.CloseBlob();
    this.FireTaskStatusChanged(new TaskStatusChangedEventArgs(session, TaskStatus.ApplyingChangesPortal, 51.0));
    Guid guid = connector.Login(session.SessionGUID);
    try
    {
      if (connector.GetTaskStatus(guid, this.portalTaskID) != TaskStatus.Successfully)
      {
        this.WriteToDetailedLog($"Обработка переданных данных на портале (Идентификатор версии задачи на портале {this.portalTaskID}).");
        connector.CompletePublish(guid, this.portalTaskID, false);
        this.AfterCompletePublish(session, guid, connector);
      }
    }
    finally
    {
      if (guid != Guid.Empty && connector != null)
        connector.Logout(guid);
    }
    this.FireTaskStatusChanged(new TaskStatusChangedEventArgs(session, TaskStatus.ApplyingChangesSite, 90.0));
    this.HandlePublishedObjects(session);
    if (this.publishedObjectIDs != null && this.publishedObjectIDs.Count > 0 && !string.IsNullOrWhiteSpace(this.enabledSites))
    {
      ISitesCacheService customService = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
      List<long> parameter = new List<long>();
      foreach (char enabledSite in this.enabledSites)
      {
        char siteCode = enabledSite;
        if (!siteCode.Equals(customService.Info.Code))
        {
          SiteInfo siteInfo = customService.Sites.Find((Predicate<SiteInfo>) (x => x.Code.Equals(siteCode)));
          if (siteInfo != null)
            parameter.Add(siteInfo.ID);
        }
      }
      if (parameter.Count > 0)
        new Thread(new ParameterizedThreadStart(this.FireObjectsPublishedEvent))
        {
          IsBackground = true,
          Name = "ExportTask_ObjectsPublishedEventHandler"
        }.Start((object) parameter);
    }
    this.FireTaskStatusChanged(new TaskStatusChangedEventArgs(session, TaskStatus.DeletingPortalTask, 98.0));
    this.WriteToDetailedLog($"Удаление задачи {this.portalTaskID} на портале.");
    Guid connectGuid = connector.Login(session.SessionGUID);
    try
    {
      connector.DeletePublishTask(connectGuid, this.portalTaskID);
    }
    finally
    {
      if (connectGuid != Guid.Empty && connector != null)
        connector.Logout(connectGuid);
    }
    this.WriteToDetailedLog("Завершение работы.");
    this.CloseLog();
  }

  private void FireObjectsPublishedEvent(object args)
  {
    try
    {
      (ServerServices.GetService(typeof (IPortalTasksQueue)) as PortalTasksQueue).FireObjectsPublished((object) this, new ObjectsPublishedEventArgs(this.UserID, this.publishedObjectIDs.ConvertAll<long>((Converter<Tuple<long, bool, bool>, long>) (p => p.Item1)), args as List<long>));
    }
    catch (Exception ex)
    {
      TasksHelper.AddMessageToLog($"Ошибка при обработке события публикации объектов: {ex.Message}");
      TasksHelper.AddMessageToLog($"StackTrace: {ex.StackTrace}");
    }
  }

  protected virtual void HandlePublishedObjects(IUserSession session)
  {
  }

  protected override void OnTaskStarted(
    IUserSession session,
    Guid connectionGuid,
    IPortalConnector connector)
  {
    if (this.portalTaskID == 0L)
      this.portalTaskID = connector.StartPublishingTask(connectionGuid, this.Name, this.enabledSites);
    this.attributeTaskFiles.Index = DBTask.GetFileIndex(this.attributeTaskFiles, BackupConsts.DataFileName);
    this._reader = this.attributeTaskFiles as IBlobReader;
    this._reader.OpenBlob(0);
  }

  protected override void Begining(
    IUserSession session,
    Guid connectionGuid,
    IPortalConnector connector,
    ITransferedObject unit)
  {
    ITransferedObjectExporter transferedObjectExporter = (ITransferedObjectExporter) null;
    switch (unit)
    {
      case ExtendedTransferedObject unit1:
        transferedObjectExporter = (ITransferedObjectExporter) new ExtendedTransferedObjectExporter(this.portalTaskID, this._reader, unit1);
        break;
      case PersistentObject unit2:
        transferedObjectExporter = (ITransferedObjectExporter) new PersistentObjectExporter(this.portalTaskID, unit2);
        break;
      default:
        PersistentRelation persistentRelation = unit as PersistentRelation;
        break;
    }
    if (transferedObjectExporter == null)
      throw new Exception("Необрабатываемый тип ITransferedObject");
    transferedObjectExporter.Publish(session, connectionGuid, connector);
  }

  protected virtual void SaveData(BinaryWriter bw)
  {
    if (this.enabledSites != null && this.enabledSites.Length > 0)
    {
      bw.Write(this.enabledSites.Length);
      bw.Write(this.enabledSites.ToCharArray());
    }
    else
      bw.Write(0);
    bw.Write(this.portalTaskID);
    if (this.publishedObjectIDs != null && this.publishedObjectIDs.Count > 0)
    {
      bw.Write(this.publishedObjectIDs.Count);
      foreach (Tuple<long, bool, bool> publishedObjectId in this.publishedObjectIDs)
      {
        bw.Write(publishedObjectId.Item1);
        bw.Write(publishedObjectId.Item2);
        bw.Write(publishedObjectId.Item3);
      }
    }
    else
      bw.Write(0);
    bw.Write(this.UserID);
  }

  protected virtual void LoadData(BinaryReader br)
  {
    int length = br.ReadInt32();
    this.enabledSites = length > 0 ? Helper.GetString(length, br) : string.Empty;
    this.portalTaskID = br.ReadInt64();
    int capacity = br.ReadInt32();
    this.publishedObjectIDs = new List<Tuple<long, bool, bool>>(capacity);
    if (capacity > 0)
    {
      for (int index = 0; index < capacity; ++index)
        this.publishedObjectIDs.Add(new Tuple<long, bool, bool>(br.ReadInt64(), br.ReadBoolean(), br.ReadBoolean()));
    }
    this.UserID = br.ReadInt64();
  }

  public override byte[] Save(IUserSession session, IDBObject backupObject)
  {
    using (ImChunkedStream output = new ImChunkedStream())
    {
      BinaryWriter bw = new BinaryWriter((Stream) output, Encoding.UTF8);
      try
      {
        this.SaveData(bw);
      }
      finally
      {
        bw.Flush();
      }
      return output.ToArray();
    }
  }

  public override void Load(IUserSession session, IDBObject backupObject, byte[] bytes)
  {
    using (BinaryReader br = new BinaryReader((Stream) new MemoryStream(bytes), Encoding.UTF8))
      this.LoadData(br);
  }

  public override void LoadTransferedObjects(BinaryReader reader)
  {
    List<TransferedObject> transferedObjectList = new List<TransferedObject>();
    this.WriteToDetailedLog($"Чтение из БД данных для публикации. Объем данных {reader.BaseStream.Length} байт.");
    while (reader.BaseStream.Position < reader.BaseStream.Length)
      transferedObjectList.Add(TransferedObjectHelper.LoadFor(reader, true));
    this.Units = (ITransferedObject[]) transferedObjectList.ToArray();
  }

  public override string GetIncludesInfo(IUserSession session)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("{\\rtf1\\ansi");
    stringBuilder.Append(this.GeneralIncludesInfo());
    ISitesCacheService customService = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
    stringBuilder.Append("\\line\\b");
    stringBuilder.Append(this.enabledSites.Length == 1 ? "Публикация для узла:" : "Публикация для узлов:");
    stringBuilder.Append("\\b0");
    stringBuilder.Append(" " + SiteIDHelper.GetCaption(customService, this.enabledSites));
    stringBuilder.Append("\\line\\line");
    if (this.publishedObjectIDs != null)
    {
      stringBuilder.Append("\\bРутовые объекты:\\b0\\line");
      foreach (Tuple<long, bool, bool> publishedObjectId in this.publishedObjectIDs)
      {
        if (publishedObjectId.Item2)
        {
          IDBObject dbObject = session.GetObject(publishedObjectId.Item1, false);
          if (dbObject != null)
            stringBuilder.Append($"{dbObject.NameInMessages} (ObjectID={publishedObjectId.Item1}))");
          else
            stringBuilder.Append($"Несуществующий объект ObjectID = {publishedObjectId.Item1}");
          stringBuilder.Append("\\line");
        }
      }
    }
    stringBuilder.AppendLine("}");
    return stringBuilder.ToString();
  }

  protected override ITransferSettingsService GetSettingsService()
  {
    return (ITransferSettingsService) ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true);
  }
}
