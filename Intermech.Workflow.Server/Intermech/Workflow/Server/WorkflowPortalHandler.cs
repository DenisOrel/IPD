// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WorkflowPortalHandler
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.PortalServices;
using Intermech.Project;
using Intermech.Workflow.Server.Activities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;

#nullable disable
namespace Intermech.Workflow.Server;

public class WorkflowPortalHandler
{
  private const string varPrefix = "Var.";

  public static List<long> ForwardDataFlow(IUserSession session, WFActivity FromAct, StringList sl)
  {
    List<long> longList = new List<long>();
    AttachmentList attachments = FromAct.Attachments;
    int count = attachments.Count;
    sl.Values["Att.Count"] = count.ToString();
    for (int index = 0; index < count; ++index)
    {
      Attachment attachment = attachments[index];
      IDBObject dbObject = session.GetObject(attachment.ObjectID, false);
      if (dbObject != null)
      {
        longList.Add(attachment.ObjectID);
        sl.Values["Att." + index.ToString()] = dbObject.ObjectGUID.ToString();
      }
    }
    VarList variableList = FromAct.VariableList;
    for (int index = sl.Count - 1; index >= 0; --index)
    {
      if (sl[index].StartsWith("Var."))
        sl.RemoveAt(index);
    }
    foreach (Variable variable in variableList)
      sl.Values["Var." + variable.Name] = StringList.StringToCommaText(variable.Value);
    sl.Values["Src"] = "IPS";
    IDBAttribute attributeById = FromAct.GetAttributeByID(wfConsts.AttrExecHistoryID);
    if (attributeById != null)
    {
      StringList stringList = new StringList();
      int valuesCount = attributeById.ValuesCount;
      long[] numArray = new long[valuesCount];
      for (int index = 0; index < valuesCount; ++index)
      {
        long int64 = Convert.ToInt64(attributeById.Values[index]);
        numArray[index] = int64;
        stringList.Add(int64.ToString());
      }
      sl.Values["HistArray"] = stringList.CommaText;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) numArray, LogicalOperators.AND, 0, false),
        new ConditionStructure(wfConsts.AttrActivityMessageID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, true)
      }, new ColumnDescriptor[8]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
        new ColumnDescriptor((object) wfConsts.AttrRecipID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
        new ColumnDescriptor((object) wfConsts.AttrActivityMessageID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
        new ColumnDescriptor((object) wfConsts.AttrActivityResultID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
        new ColumnDescriptor((object) wfConsts.AttrActivityMessageID, AttributeSourceTypes.Auto, ColumnContents.Date, ColumnNameMapping.Default, SortOrders.NONE, 0),
        new ColumnDescriptor((object) wfConsts.AttrRecipID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
      });
      if (paramSet.Tags == null)
        paramSet.Tags = new HybridDictionary();
      paramSet.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesByObjectIDsSelector(numArray);
      DataTable dataTable = session.GetObjectCollection(wfConsts.ActivitiesTypeID).Select(paramSet);
      string empty = string.Empty;
      stringList.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        object[] itemArray = row.ItemArray;
        ActivityInfo byId = ActivityInfos.FindByID(Convert.ToInt32(itemArray[4]));
        if (byId != null)
          itemArray[4] = (object) (int) byId.Kind;
        if (itemArray[6] != DBNull.Value)
        {
          DateTime universalTime = Convert.ToDateTime(itemArray[6], (IFormatProvider) CultureInfo.InvariantCulture).ToUniversalTime();
          itemArray[6] = (object) universalTime;
        }
        stringList.Values[row[0].ToString()] = StringList.ObjectArrayToCommaText(itemArray);
      }
      string commaText = stringList.CommaText;
      if (!string.IsNullOrEmpty(commaText))
        sl.Values["Messages"] = commaText;
      else
        sl.Values.Remove("Messages");
    }
    else
      sl.Values.Remove("Messages");
    return longList;
  }

  private static void ForwardDataFlow(IUserSession session, StringList sl, WFActivity toAct)
  {
    foreach (string str in (List<string>) sl)
    {
      string name = string.Empty;
      string empty = string.Empty;
      if (str.StartsWith("Var."))
      {
        int length = "Var.".Length;
        int num = str.IndexOf("=");
        name = str.Substring(length, num - length);
        empty = StringList.CommaTextToString(str.Substring(num + 1));
      }
      if (!string.IsNullOrEmpty(name))
      {
        Variable variable = toAct.VariableList.GetVariable(name);
        if (variable != null)
          variable.Value = empty;
      }
    }
    toAct.SaveVariables();
    int intDef = MiscFunx.StrToIntDef(sl.Values["Att.Count"], 0);
    for (int index = 0; index < intDef; ++index)
    {
      Guid objectGUID = new Guid(sl.Values["Att." + index.ToString()]);
      IDBObject dbObject = session.GetObject(objectGUID, false);
      if (dbObject != null)
      {
        long objectId = dbObject.ObjectID;
        if (objectId != 0L)
          toAct.Attachments.AddAttachment(objectId);
      }
    }
    toAct.SaveAttachments();
  }

  public static void RemoteProcessImported(object sender, ImportTaskCompletedEventArgs e)
  {
    if (e.Data == null)
      return;
    StringList sl = new StringList();
    sl.CommaText = e.Data.Data;
    if (e.Data.RemoteMessage == null && sl.Values["Launch"] == "1")
    {
      Guid objectGUID1 = new Guid(sl.Values["RTGuid"]);
      IDBObject objectBaseVersionById = e.Session.GetObject(objectGUID1, true);
      if (!objectBaseVersionById.IsBaseVersion)
        objectBaseVersionById = e.Session.GetObjectBaseVersionByID(objectBaseVersionById.ID, true);
      long objectId1 = objectBaseVersionById.ObjectID;
      IDBObject dbObject = e.Session.GetObjectCollection(wfConsts.ProcessesTypeID).Create(objectId1);
      dbObject.CommitCreation(false);
      long objectId2 = dbObject.ObjectID;
      WFProcess activity = dbObject as WFProcess;
      string str = activity.Validate(true, (List<long>) null);
      if (!string.IsNullOrEmpty(str))
      {
        activity.Delete(0L);
        throw new WorkflowException(LocalizationHolder.rm.GetString("Workflow.Server_28") + str);
      }
      sl.Values["RPGuid"] = activity.ObjectGUID.ToString();
      sl.Values["Launch"] = "0";
      if (e.Session.GetCustomService(typeof (ISitesCacheService)) is ISitesCacheService customService)
        sl.Values["RSiteName"] = customService.Info.Caption;
      List<ExtPropertiesFlag> extPropertiesFlagList = new List<ExtPropertiesFlag>((IEnumerable<ExtPropertiesFlag>) new ExtPropertiesFlag[1]
      {
        ExtPropertiesFlag.Portal
      });
      if (!string.IsNullOrEmpty(sl.Values["Messages"]))
        extPropertiesFlagList.Add(ExtPropertiesFlag.Messages);
      activity.ExtProps.Write("PortalInfo", sl.CommaText, extPropertiesFlagList.ToArray());
      activity.ExtProps.Save((IDBObject) activity);
      WorkflowPortalHandler.ForwardDataFlow(e.Session, sl, (WFActivity) activity.StartActivity);
      activity.StartActivity.AllowSystemParticipant = true;
      activity.StartProcess();
      if (!(sl.Values["Kind"] == RemoteProcessKind.ImProject) || !(sl.Values["Command"] == "Execute"))
        return;
      Guid objectGUID2 = new Guid(sl.Values["ProjectGuid"]);
      if (!(e.Session.GetObject(objectGUID2, false) is IProject project))
        return;
      project.Execute();
    }
    else
    {
      if (e.Session.GetCustomService(typeof (ISitesCacheService)) is ISitesCacheService customService && customService.Info.GUID.ToString() != sl.Values["SrcSite"])
        return;
      if (sl.Values["Kind"] == RemoteProcessKind.ImProject)
      {
        Guid objectGUID = new Guid(sl.Values["ProjectGuid"]);
        if (!(e.Session.GetObject(objectGUID, false) is IProject project))
          return;
        if (e.Data.RemoteMessage == null)
          project.RemoteStatus = RemoteProcessStatus.Completed;
        else if (string.IsNullOrEmpty(e.Data.RemoteMessage.Message))
          project.RemoteStatus = RemoteProcessStatus.InProgress;
        else
          project.RemoteStatus = RemoteProcessStatus.RemoteExecError;
      }
      else
      {
        int intDef = MiscFunx.StrToIntDef(sl.Values["AID"], 0);
        if (!(e.Session.GetObject((long) intDef, false) is RemoteProcess remoteProcess))
          return;
        if (e.Data.RemoteMessage == null)
        {
          remoteProcess.RemoteStatus = RemoteProcessStatus.Completed;
          List<ExtPropertiesFlag> extPropertiesFlagList = new List<ExtPropertiesFlag>((IEnumerable<ExtPropertiesFlag>) new ExtPropertiesFlag[1]
          {
            ExtPropertiesFlag.Portal
          });
          if (!string.IsNullOrEmpty(sl.Values["Messages"]))
            extPropertiesFlagList.Add(ExtPropertiesFlag.Messages);
          remoteProcess.ExtProps.Write("PortalInfo", sl.CommaText, extPropertiesFlagList.ToArray());
          remoteProcess.ExtProps.Save((IDBObject) remoteProcess);
          WorkflowPortalHandler.ForwardDataFlow(e.Session, sl, (WFActivity) remoteProcess);
          remoteProcess.NextStep(sl.Values["Forward"] != "0");
          new WFActivityProxy(remoteProcess.ProcessID, (WFActivity) remoteProcess).ExecuteNextActivity(remoteProcess.SenderID, remoteProcess.ObjectID, remoteProcess.NextStepLinks, remoteProcess.VariableList);
        }
        else if (string.IsNullOrEmpty(e.Data.RemoteMessage.Message))
        {
          remoteProcess.RemoteStatus = RemoteProcessStatus.InProgress;
        }
        else
        {
          remoteProcess.RemoteStatus = RemoteProcessStatus.RemoteExecError;
          WorkflowPortalHandler.ForwardDataFlow(e.Session, sl, (WFActivity) remoteProcess);
          remoteProcess.MessageText = $"{LocalizationHolder.GetString("PortalRemoteExecError")}\r\n{e.Data.RemoteMessage.Message}";
          remoteProcess.NextStep(false);
          new WFActivityProxy(remoteProcess.ProcessID, (WFActivity) remoteProcess).ExecuteNextActivity(remoteProcess.SenderID, remoteProcess.ObjectID, remoteProcess.NextStepLinks, remoteProcess.VariableList);
        }
      }
    }
  }

  public static void StartResolveBaseVersionConflict(
    object sender,
    StartResolveBaseVersionConflictEventArgs e)
  {
    IUserSession sessionById = UserSession.GetSessionByID(e.SessionGuid);
    IProcess process = (sessionById.GetCustomService(typeof (IRouterService)) as IRouterService).CreateProcess(sessionById.SessionGUID, e.TemplateID);
    if (process.StartActivity == null)
      throw new Exception(LocalizationHolder.GetString("ErrStartActivityNotFound"));
    process.StartActivity.MessageText = string.Format(LocalizationHolder.GetString("ErrBaseVersionConflict"), (object) e.ConflictedObjectID, (object) MiscFunx.GetObjectCaption(sessionById, e.ConflictedObjectID));
    process.StartProcess();
  }

  public static void GetTaskByTypeEvent(object sender, GetTaskByTypeEventArgs e)
  {
    if (e.Handled || e.Type != TaskType.ProcessPublish)
      return;
    e.Task = (ITask) new AutoTransferPublishTask(e.TaskObject.GetAttributeByGuid(PortalConsts.attributeTaskFiles));
    e.Handled = true;
  }

  public static void ContinueExecutionAtSender(StringList sl, bool goNext, WFActivity sender)
  {
    IUserSession sessionTemporaryClone = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service1 ? service1.GetSystemSessionTemporaryClone("WFS.WorkflowPortalHandler.ContinueExecutionAtSender") : (IUserSession) null;
    if (sessionTemporaryClone == null)
      return;
    try
    {
      if (!(ApplicationServices.Container.GetService(typeof (ICustomPublisherService)) is ICustomPublisherService service2))
      {
        WorkflowPortalHandler.CreatePortalFinishEvent(goNext, sender.ObjectID, sender.UserSession.DataManager, sessionTemporaryClone, service1, 4);
      }
      else
      {
        sl.Values["Forward"] = goNext ? "1" : "0";
        sl.Values["Launch"] = "0";
        sl.Values["RProcessName"] = sender.ProcessName;
        int num = sl.Values["Src"] == "IPS" ? 1 : 0;
        List<long> attachments = WorkflowPortalHandler.ForwardDataFlow(sessionTemporaryClone, sender, sl);
        bool flag = false;
        sl.Values.TryGetValue("GiveOwnership", ref flag);
        Packet4Publish packet = (Packet4Publish) null;
        if (num != 0)
        {
          string str = "WFPACKET " + DateTime.Now.ToString("ddMMyyyyHHmmffff");
          packet = new Packet4Publish(str, str, string.Empty);
        }
        bool createReceipt = num != 0 && sl.Values["CreateReceipt"] == "1";
        string str1 = $"{LocalizationHolder.GetString("PortalReturnProcessPrefix")} \"{sl.Values["SrcSiteName"]} / {sl.Values["SrcProcessName"]}\"";
        ISitesCacheService customService = sessionTemporaryClone.GetCustomService(typeof (ISitesCacheService)) as ISitesCacheService;
        SiteInfo site = customService.GetSite(new Guid(sl.Values["SrcSite"]));
        char? nullable = new char?();
        if (flag)
          nullable = new char?(site.Code);
        ExtendedPublishOptions options = new ExtendedPublishOptions(PublishCompositionOptions.WithLinkedObjects | PublishCompositionOptions.IncludeFreeChangeAttributes, -1, (List<int>) null, (List<int>) null, (FiltrationSettings) null, customService.Info.Code.ToString() + site.Code.ToString(), false, nullable, nullable);
        CustomPublishDataInfo processInfo = new CustomPublishDataInfo(str1, site.Code, attachments, sl.CommaText, options, string.Empty);
        service2.CustomPublish(sessionTemporaryClone.SessionGUID, (IPublisher) RemoteProcessPublisher.Create(sessionTemporaryClone, processInfo, packet, createReceipt, sender.Process.ObjectID, sender.ObjectID), str1, TaskPriority.Normal);
      }
    }
    finally
    {
      sessionTemporaryClone.Logout("WFS.WorkflowPortalHandler.ContinueExecutionAtSender");
    }
  }

  public static void ContinueExecutionAtSender(StringList sl, bool goNext, WFProcess sender)
  {
    IUserSession sessionTemporaryClone = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service1 ? service1.GetSystemSessionTemporaryClone("WFS.WorkflowPortalHandler.ContinueExecutionAtSender") : (IUserSession) null;
    if (sessionTemporaryClone == null)
      return;
    try
    {
      if (!(ApplicationServices.Container.GetService(typeof (ICustomPublisherService)) is ICustomPublisherService service2))
      {
        WorkflowPortalHandler.CreatePortalFinishEvent(goNext, sender.ObjectID, sender.UserSession.DataManager, sessionTemporaryClone, service1, 5);
      }
      else
      {
        sl.Values["Forward"] = goNext ? "1" : "0";
        sl.Values["Launch"] = "0";
        sl.Values["RProcessName"] = sender.Caption;
        int num = sl.Values["Src"] == "IPS" ? 1 : 0;
        List<long> attachments = WorkflowPortalHandler.ForwardDataFlow(sessionTemporaryClone, (WFActivity) sender.StartActivity, sl);
        bool flag = false;
        sl.Values.TryGetValue("GiveOwnership", ref flag);
        Packet4Publish packet = (Packet4Publish) null;
        if (num != 0)
        {
          string str = "WFPACKET " + DateTime.Now.ToString("ddMMyyyyHHmmffff");
          packet = new Packet4Publish(str, str, string.Empty);
        }
        bool createReceipt = num != 0 && sl.Values["CreateReceipt"] == "1";
        string str1 = $"{LocalizationHolder.GetString("PortalReturnProcessPrefix")} \"{sl.Values["SrcSiteName"]} / {sl.Values["SrcProcessName"]}\"";
        ISitesCacheService customService = sessionTemporaryClone.GetCustomService(typeof (ISitesCacheService)) as ISitesCacheService;
        SiteInfo site = customService.GetSite(new Guid(sl.Values["SrcSite"]));
        char? nullable = new char?();
        if (flag)
          nullable = new char?(site.Code);
        ExtendedPublishOptions options = new ExtendedPublishOptions(PublishCompositionOptions.WithLinkedObjects | PublishCompositionOptions.IncludeFreeChangeAttributes, -1, (List<int>) null, (List<int>) null, (FiltrationSettings) null, customService.Info.Code.ToString() + site.Code.ToString(), false, nullable, nullable);
        CustomPublishDataInfo processInfo = new CustomPublishDataInfo(str1, site.Code, attachments, sl.CommaText, options, string.Empty);
        service2.CustomPublish(sessionTemporaryClone.SessionGUID, (IPublisher) RemoteProcessPublisher.Create(sessionTemporaryClone, processInfo, packet, createReceipt, sender.ObjectID, sender.StartActivity.ObjectID), str1, TaskPriority.Normal);
      }
    }
    finally
    {
      sessionTemporaryClone.Logout("WFS.WorkflowPortalHandler.ContinueExecutionAtSender");
    }
  }

  private static void CreatePortalFinishEvent(
    bool goNext,
    long senderObjectID,
    IDbManager dataManager,
    IUserSession session,
    IDBTimedEvents te,
    int portalEventKind)
  {
    string str1 = session.Configurations.ReadString("KERNEL", "PortalProps", "PortalServerName", string.Empty, DBConfigMode.GlobalOnly);
    if (string.IsNullOrEmpty(str1))
      throw new Exception("Интерфейс запуска удалённого подпроцесса ICustomPublisher не найден");
    if (ApplicationServices.Container.GetService(typeof (IAppServers)) is IAppServers service && str1 == service.ServerName)
      throw new Exception($"Интерфейс запуска удалённого подпроцесса ICustomPublisher для заданного сервера '{str1}' не найден");
    if (str1.IndexOf(':') > -1)
      str1 = str1.Substring(0, str1.IndexOf(':'));
    TimedEventProperties properties = new TimedEventProperties(0, DateTime.UtcNow.AddSeconds(10.0), DateTime.MinValue, wfConsts.WorkflowPortalDelayStarterGuid, senderObjectID, 0L, goNext.ToString(), portalEventKind, 0)
    {
      ServerName = str1
    };
    int num = te.AddEvent(properties, dataManager);
    string str2 = portalEventKind == 4 ? "действия" : "процесса";
    te.AddToTrace($"Событие N{num} отложенного завершения процесса для {str2} N{senderObjectID} зарегистрировано.", true);
  }
}
