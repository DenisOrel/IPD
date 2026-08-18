// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.wfServerPlugin
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.ApplicationModel;
using Intermech.Forums;
using Intermech.Interfaces;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.Email;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Protection;
using Intermech.Workflow.Base;
using Intermech.Workflow.Server.Activities;
using Intermech.Workflow.Server.AutoNotifications;
using Intermech.Workflow.Server.BM2;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

public class wfServerPlugin : IPackage, IPackageExtension
{
  private ObjChangedNotifService _notifService;
  private IServiceProvider _serviceProvider;
  private IEventLogHelper _eventLogHelper;
  private IDBObject _currentObject;
  public static bool _inAfterCommitCreation;
  private bool _postInited;

  public void Load(IServiceProvider serviceProvider)
  {
    this._serviceProvider = serviceProvider;
    if (!(serviceProvider.GetService(typeof (ILicenser)) is ILicenser service1))
      throw new ProtectionException(LocalizationHolder.rm.GetString("Workflow.Server_39"));
    service1.AllocateLicense(365);
    ApplicationServices.Container.GetService(typeof (IDBObjectService));
    this._eventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    if (this._eventLogHelper != null)
    {
      this._eventLogHelper.GetUsedAttributesEvent += new GetUsedAttributesHandler(this.eventLogHelper_GetUsedAttributesEvent);
      this._eventLogHelper.BeforeDeleteAttributeTypeEvent += new DeleteAttributeTypeHandler(this.eventLogHelper_BeforeDeleteAttributeTypeEvent);
      this._eventLogHelper.AfterCommitCreationObjectEvent += new ObjectEventHandler(this.AfterCommitCreationObjectEvent);
      this._eventLogHelper.AfterPurgeObjectEvent += new ObjectEventHandler(this.eventLogHelper_AfterPurgeObjectEvent);
      if (this._eventLogHelper is EventLogHelper eventLogHelper)
        eventLogHelper.AfterClearTrash += new ClearTrashHandler(this.HelperClass_AfterClearTrash);
    }
    IIDLinkTranslate service2 = (IIDLinkTranslate) ApplicationServices.Container.GetService(typeof (IIDLinkTranslate));
    if (service2 != null)
      service2.IsIDLinkEvent += new IsIDLinkEventHandler(this.linkTranslate_IsIDLinkEvent);
    if (serviceProvider.GetService(typeof (ICustomServices)) is ICustomServices service4)
    {
      service4.AddService(typeof (IForumsService), (object) new ForumsService());
      ApplicationServices.Container.AddService(typeof (IForumExtend), (object) new ForumExtend());
      if (ApplicationServices.Container.GetService(typeof (ILinkedObjectsService)) is ILinkedObjectsService service3)
        service3.RegisterHandler((ILinkedObjectsHandler) new ForumsLinkedObjectsHandler());
      AutoNotificationsService serviceInstance = new AutoNotificationsService();
      service4.AddService(typeof (IAutoNotificationsService), (object) serviceInstance);
      ApplicationServices.Container.AddService(typeof (IAutoNotificationsService), (object) serviceInstance);
      service4.AddService(typeof (IEmailDownloadService), (object) new EmailDownloadService());
      IUserSession sessionTemporaryClone = (ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("WFS.Load");
      try
      {
        this._notifService = new ObjChangedNotifService(sessionTemporaryClone);
        service4.AddService(typeof (INotifySubscriberService), (object) this._notifService);
        ApplicationServices.Container.AddService(typeof (INotifySubscriberService), (object) this._notifService);
      }
      finally
      {
        sessionTemporaryClone?.Logout("WFS.Load");
      }
    }
    WorkflowExporter.Init();
    if (!(ApplicationServices.Container.GetService(typeof (IConsoleCommandRegistry)) is IConsoleCommandRegistry service5))
      return;
    service5.Add(new ConsoleCommandInfo("wfcopycoords", string.Empty, string.Empty, new ConsoleCommandMethod(this.CopyCoordsConsoleCommand)));
  }

  private string getMacroValue(string Name)
  {
    object[] valuesByName = this._currentObject?.GetValuesByName(Name, false);
    return valuesByName != null && valuesByName.Length != 0 ? valuesByName[0].ToString() : string.Empty;
  }

  private void AfterCommitCreationObjectEvent(IDBObject sender, IUserSession session)
  {
    if (!AutoLaunchSettings.AllTypeIDs.Contains(sender.ObjectType) || wfServerPlugin._inAfterCommitCreation)
      return;
    List<int> AllowedTypeIDs = new List<int>();
    foreach (AutoLaunchInfo autoLaunchInfo in (List<AutoLaunchInfo>) AutoLaunchSettings.All)
    {
      if (!AllowedTypeIDs.Contains(autoLaunchInfo.TypeID))
        AllowedTypeIDs.Add(autoLaunchInfo.TypeID);
    }
    int mostAppropriateType = MiscFunx.GetMostAppropriateType(sender.ObjectType, AllowedTypeIDs);
    if (mostAppropriateType == 0)
      return;
    wfServerPlugin._inAfterCommitCreation = true;
    try
    {
      foreach (AutoLaunchInfo autoLaunchInfo in (List<AutoLaunchInfo>) AutoLaunchSettings.All)
      {
        if (autoLaunchInfo.TypeID == mostAppropriateType)
        {
          long num = autoLaunchInfo.SchemeID;
          IDBObject dbObject = session.GetObject(num, false);
          if (GlobalMailSettings.Cfg.LaunchBaseSchemesOnly && !dbObject.IsBaseVersion)
            num = session.GetObjectBaseVersionByID(dbObject.ID, true).ObjectID;
          WFProcess wfProcess = session.GetObjectCollection(wfConsts.ProcessesTypeID).Create(num) as WFProcess;
          wfProcess.CommitCreation(false);
          Start startActivity = wfProcess.StartActivity;
          if (startActivity == null)
            throw new Exception(LocalizationHolder.GetString("ErrStartActivityNotFound"));
          wfProcess.Priority = autoLaunchInfo.ProcessPriority;
          string name = wfProcess.Name;
          if (name.Contains("%"))
            wfProcess.Name = StringFuncs.ReplaceMacros(name, new StringFuncs.GetMacroValueDelegate(this.getMacroValue));
          IDBAttribute attributeById = wfProcess.GetAttributeByID(wfConsts.AttrDescriptionID);
          if (attributeById != null)
          {
            string asString = attributeById.AsString;
            if (!string.IsNullOrEmpty(asString))
              asString += "\r\n";
            string str = $"{asString}/* {LocalizationHolder.GetString("AutoStartedDesc")} */";
            attributeById.AsString = str;
          }
          ((IActivity) startActivity).Attachments.Add(Math.Abs(sender.ObjectID));
          wfProcess.StartProcess();
        }
      }
    }
    finally
    {
      wfServerPlugin._inAfterCommitCreation = false;
    }
  }

  private void linkTranslate_IsIDLinkEvent(object sender, IDLinkEventArgs e)
  {
    if (e.Handled || !(e.AttributeGUID == wfConsts.AttrToActivityGuid) && !(e.AttributeGUID == wfConsts.AttrFromActivityGuid))
      return;
    e.Handled = true;
    e.IsIDLink = true;
  }

  public void Unload()
  {
    if (this._notifService == null)
      return;
    this._notifService.Close();
    this._notifService = (ObjChangedNotifService) null;
  }

  public string Name => LocalizationHolder.rm.GetString("Workflow.Server_41");

  public bool PostInit()
  {
    if (this._postInited)
      return true;
    this._postInited = true;
    IUserSession sessionTemporaryClone = (ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("WFS.PostInit");
    try
    {
      GlobalMailSettings.Init(sessionTemporaryClone);
      ClearOldProcessSettings.Init(sessionTemporaryClone);
      if (ApplicationServices.Container.GetService(typeof (IFormDesignerServer)) is IFormDesignerServer service)
        FormsHandler.RegisterHandlers(service, sessionTemporaryClone);
      AutoLaunchSettings.All.Load(sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone?.Logout("WFS.PostInit");
    }
    ICreatorContainer service1 = ApplicationServices.Container.GetService(typeof (IDBObjectService)) as ICreatorContainer;
    WFObjectCreator creatorInstance1 = new WFObjectCreator();
    if (service1 != null)
    {
      foreach (KeyValuePair<Guid, Type> knownType in creatorInstance1.KnownTypes)
        service1.AddCreator((object) knownType.Key, (object) creatorInstance1);
      service1.AddCreator((object) new Guid("cad00627-306c-11d8-b4e9-00304f19f545"), (object) new DBNotifyCreator());
    }
    ICreatorContainer service2 = ApplicationServices.Container.GetService(typeof (IDBObjectCollectionService)) as ICreatorContainer;
    IDBObjectCollectionCreator creatorInstance2 = (IDBObjectCollectionCreator) new WFProcessCollectionCreator();
    if (service2 != null)
    {
      service2.AddCreator((object) wfConsts.ProcessesGuid, (object) creatorInstance2);
      service2.AddCreator((object) wfConsts.SchemesGuid, (object) creatorInstance2);
    }
    if (ApplicationServices.Container.GetService(typeof (IDBRelationService)) is ICreatorContainer service3)
    {
      service3.AddCreator((object) wfConsts.AttachmentRelationGuid, (object) new wfRelationCreator());
      service3.AddCreator((object) wfConsts.ScriptRelationGuid, (object) new wfRelationCreator());
    }
    if (ApplicationServices.Container.GetService(typeof (IDBRelationCollectionService)) is ICreatorContainer service4)
    {
      WFAttachmentRelationCollectionCreator creatorInstance3 = new WFAttachmentRelationCollectionCreator();
      service4.AddCreator((object) wfConsts.AttachmentRelationGuid, (object) creatorInstance3);
    }
    WorkflowTimerService.Register();
    RouterService.Register();
    ApproveGraphValueReplaceService.Register();
    ExecuteService.Register();
    WorkflowPortalDelayStarter.Register();
    IEventLogHelper service5 = ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    ApplicationServices.Container.AddService(typeof (IDelayProcessStarter), (object) new StartProcessAfterTransactionCommitService(service5));
    service5.AddActionName(1, (long) wfConsts.ProcessesTypeID, ActionType.wfAdminProcess, LocalizationHolder.rm.GetString("AccessRightProcessAdmin"));
    service5.AddActionName(1, (long) wfConsts.SchemeCategoriesID, ActionType.wfLaunchProcess, LocalizationHolder.rm.GetString("AccessRightCategoryLaunch"));
    service5.AddActionName(2, (long) wfConsts.ProcessesTypeID, ActionType.wfAbortProcess, "Прерывание процесса");
    if (ApplicationServices.Container.GetService(typeof (IPortalEventsService)) is IPortalEventsService service6)
    {
      service6.ImportTaskCompletedEvent += new ImportTaskCompletedEventHandler(WorkflowPortalHandler.RemoteProcessImported);
      service6.GetTaskByTypeEvent += new GetTaskByTypeEventHandler(WorkflowPortalHandler.GetTaskByTypeEvent);
      service6.StartResolveBaseVersionConflictEvent += new StartResolveBaseVersionConflictEventHandler(WorkflowPortalHandler.StartResolveBaseVersionConflict);
    }
    return true;
  }

  private void eventLogHelper_GetUsedAttributesEvent(
    IUserSession session,
    UsedAttributesEventArgs args)
  {
    DataTable table1 = MiscFunx.SimpleSelect(session, wfConsts.ProcessesTypeID, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }, (ConditionStructure[]) null, recordCount: -1);
    DataTable table2 = MiscFunx.SimpleSelect(session, wfConsts.SchemesTypeID, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }, (ConditionStructure[]) null, recordCount: -1);
    HashSet<int> variablesAttrIDs = new HashSet<int>();
    this.GetVariableList(session, table2, variablesAttrIDs);
    this.GetVariableList(session, table1, variablesAttrIDs);
    foreach (int attrID in variablesAttrIDs)
      args.AddAttribute(attrID);
  }

  private void GetVariableList(
    IUserSession session,
    DataTable table,
    HashSet<int> variablesAttrIDs)
  {
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      long result;
      if (row.ItemArray[0] != DBNull.Value && long.TryParse(row.ItemArray[0].ToString(), out result) && session.GetObject(result, false) is WFScheme wfScheme)
        variablesAttrIDs.UnionWith((IEnumerable<int>) wfScheme.Variables.TypeIDs);
    }
  }

  private void eventLogHelper_AfterPurgeObjectEvent(IDBObject sender, IUserSession session)
  {
    if (sender.ObjectType != wfConsts.AutoNotificationTypeID || !(ApplicationServices.Container.GetService(typeof (IAutoNotificationsService)) is IAutoNotificationsService service))
      return;
    service.DeleteSettingsFromCashe(sender.ObjectID);
  }

  private void eventLogHelper_BeforeDeleteAttributeTypeEvent(
    IDBAttributeType sender,
    IUserSession session)
  {
    if (Array.IndexOf<int>(sender.GetGroupsList(), wfConsts.WorkflowVarsGroupID) == -1)
      return;
    string applicabilityString = MiscFunx.GetVariableApplicabilityString(session, sender.AttributeID, 0L);
    if (!string.IsNullOrEmpty(applicabilityString))
      throw new WorkflowException(string.Format(LocalizationHolder.rm.GetString("CantDeleteVarAttribute"), (object) sender.Name, (object) applicabilityString));
  }

  private void HelperClass_AfterClearTrash(IUserSession session, List<string> clearLog)
  {
    DataTable dataTable1 = session.GetAttributesGroup(wfConsts.GlobalVariablesGroupID).Attributes.Select(string.Empty, (object[]) null);
    if (dataTable1 != null)
    {
      int columnIndex = dataTable1.Columns.IndexOf("F_ATTRIBUTE_ID");
      if (columnIndex >= 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
        {
          int int32Value = DataSetProcessor.GetInt32Value(row, columnIndex, 0);
          if (int32Value != 0)
          {
            IDBAttributeType attributeType = session.GetAttributeType(int32Value);
            try
            {
              attributeType?.Delete(0L);
            }
            catch
            {
            }
          }
        }
      }
    }
    (session as UserSession).ReloadConfigurations();
    if (ClearOldProcessSettings.Cfg == null)
      ClearOldProcessSettings.Init(session);
    else
      ClearOldProcessSettings.Cfg.Load(session);
    if (!ClearOldProcessSettings.Cfg.EnableClearOldProcess)
      return;
    object[] conditionValue1;
    if (ClearOldProcessSettings.Cfg.ComletedTypeClear == (short) 0)
      conditionValue1 = new object[2]
      {
        (object) ActivityStatus.Terminated,
        (object) ActivityStatus.Completed
      };
    else if (ClearOldProcessSettings.Cfg.ComletedTypeClear == (short) 1)
    {
      conditionValue1 = new object[1]
      {
        (object) ActivityStatus.Completed
      };
    }
    else
    {
      if (ClearOldProcessSettings.Cfg.ComletedTypeClear != (short) 2)
        return;
      conditionValue1 = new object[1]
      {
        (object) ActivityStatus.Terminated
      };
    }
    DateTime conditionValue2;
    ref DateTime local = ref conditionValue2;
    DateTime now = DateTime.Now;
    int year = now.Year;
    now = DateTime.Now;
    int month = now.Month;
    now = DateTime.Now;
    int day = now.Day;
    local = new DateTime(year, month, day, 23, 59, 59);
    switch (ClearOldProcessSettings.Cfg.TimeTypeComboBoxSelectedIndex)
    {
      case 0:
        conditionValue2 = conditionValue2.AddDays((double) -ClearOldProcessSettings.Cfg.ClearOldProcessStartTimeValue);
        break;
      case 1:
        conditionValue2 = conditionValue2.AddDays((double) (-ClearOldProcessSettings.Cfg.ClearOldProcessStartTimeValue * 7));
        break;
      case 2:
        conditionValue2 = conditionValue2.AddMonths(-ClearOldProcessSettings.Cfg.ClearOldProcessStartTimeValue);
        break;
      case 3:
        conditionValue2 = conditionValue2.AddYears(-ClearOldProcessSettings.Cfg.ClearOldProcessStartTimeValue);
        break;
    }
    ConditionStructure[] conds = new ConditionStructure[2]
    {
      new ConditionStructure(wfConsts.AttrActivityStatusID, RelationalOperators.In, (object) conditionValue1, LogicalOperators.AND, 0, false),
      new ConditionStructure(wfConsts.AttrCompletedID, RelationalOperators.Less, (object) conditionValue2, LogicalOperators.AND, 0, false)
    };
    DataTable dataTable2 = MiscFunx.SimpleSelect(session, wfConsts.ProcessesTypeID, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }, conds, recordCount: -1);
    if (dataTable2.Rows.Count <= 0)
      return;
    clearLog.Add($"Начато удаление устаревших процессов. Найдено {dataTable2.Rows.Count} устаревших процессов.");
    int num = 0;
    try
    {
      for (int index = 0; index < dataTable2.Rows.Count; ++index)
      {
        long int64 = Convert.ToInt64(dataTable2.Rows[index].ItemArray[0]);
        IDBObject dbObject = session.GetObject(int64, false);
        try
        {
          if (dbObject != null)
          {
            dbObject.Delete((long) (Consts.PurgeMode | 16 /*0x10*/));
            ++num;
          }
        }
        catch (Exception ex)
        {
          clearLog.Add($"Ошибка удаления объекта процесса '{int64}': {ex.Message}");
          if (((UserSession) session).InTransaction)
            throw;
        }
      }
    }
    finally
    {
      clearLog.Add($"Удаление устаревших процессов завершено. Удалено {num} процесс(ов).");
    }
  }

  private void CopyCoordsConsoleCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    IUserSession sessionTemporaryClone = (ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("WFS.CopyCoords");
    try
    {
      Updater.CopyCoordsFromSchemesToProcesses(consoleService, sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone?.Logout("WFS.CopyCoords");
    }
  }
}
