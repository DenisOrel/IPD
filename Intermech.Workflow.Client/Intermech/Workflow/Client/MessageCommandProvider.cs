// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MessageCommandProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using ImSSP;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Remoting.Sponsors;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class MessageCommandProvider : ICommandsProvider
{
  private ICheckMailService _checkMailService;
  private HashSet<string> _superfluousCommands = new HashSet<string>((IEnumerable<string>) new string[21]
  {
    "PrintDocument",
    "ViewDocument",
    "PDM.HiddenChilds",
    "OpenWith",
    "ViewWithOptions",
    "EditDocument",
    "SetLifecycleStep",
    "CheckOut",
    "Delete",
    "OpenDocument",
    "PDM.HiddenComposition",
    "Add",
    "CreateInclude",
    "DeleteAttribute",
    "DeleteAttributeGroup",
    "AddAttribute",
    "CreateNew",
    "CreateProto",
    "AddAttributeGroup",
    "CreateInclude",
    "EditAttributeValue"
  });
  private volatile ActivityPerformMailInfo _activityPerformMailInfo = new ActivityPerformMailInfo();

  public MessageCommandProvider(ICheckMailService checkMailService)
  {
    this._checkMailService = checkMailService;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider services)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    int num1 = MailItemsView.NodeCategoryID(items, services);
    bool flag1 = num1 == Intermech.Navigator.Consts.CategoryMailTrash;
    bool flag2 = ((num1 == Intermech.Navigator.Consts.CategoryMailInbox || num1 == Intermech.Navigator.Consts.CategoryMailOutbox ? 1 : (num1 == Intermech.Navigator.Consts.CategoryMailProcessed ? 1 : 0)) | (flag1 ? 1 : 0)) != 0;
    int num2 = flag2 ? 0 : (num1 == Intermech.Navigator.Consts.CategoryMail ? 1 : 0);
    if (num1 == Intermech.Navigator.Consts.CategoryMailInbox)
    {
      mergedCommands.Add("SendToNext", new CommandInfo(0, new ClickEventHandler(this.SendToNextCommand)));
      bool flag3 = true;
      if (items.Count == 1 && (items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectType == wfConsts.StartTypeID)
        flag3 = false;
      if (flag3)
        mergedCommands.Add("SendToBack", new CommandInfo(0, new ClickEventHandler(this.SendToBackCommand)));
      else
        mergedCommands.Add("SendToBack", new CommandInfo(0));
      mergedCommands.Add("MarkRead", new CommandInfo(0, new ClickEventHandler(this.MarkRead)));
      mergedCommands.Add("MarkUnread", new CommandInfo(0, new ClickEventHandler(this.MarkUnread)));
    }
    wfFunx.OrganizerContext(services);
    bool flag4 = false;
    bool flag5 = false;
    if (flag2)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemID(index) is MailNodeID itemId && itemId.ProcessID != 0L && wfConsts.IsWorkflowMessage(itemId.ObjectTypeID))
        {
          flag4 = true;
          break;
        }
      }
    }
    else
    {
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && (itemData.ObjectType == wfConsts.ProcessesTypeID || wfConsts.IsActivity(itemData.ObjectType)))
        {
          flag4 = true;
          if (itemData.ObjectType == wfConsts.ProcessesTypeID)
            flag5 = true;
        }
        if (flag5 & flag4)
          break;
      }
    }
    if (flag4)
    {
      mergedCommands.Add("ViewProcess", new CommandInfo(0, new ClickEventHandler(this.OpenProcessCommand), (object) false));
      mergedCommands.Add("AbortProcess", new CommandInfo(0, new ClickEventHandler(this.AbortProcessCommand)));
      mergedCommands.Add("ProcessHistory", new CommandInfo(0, new ClickEventHandler(this.ProcessHistoryCommand)));
      mergedCommands.Add("Recall", new CommandInfo(0, new ClickEventHandler(this.RecallCommand)));
      mergedCommands.Add("ReplaceParticipant", new CommandInfo(0, new ClickEventHandler(this.ReplaceParticipantCommand)));
    }
    if (flag2)
      mergedCommands.Add("DelMessage", new CommandInfo(0, new ClickEventHandler(this.DelMessage)));
    if (flag1)
      mergedCommands.Add("UndelMessage", new CommandInfo(0, new ClickEventHandler(this.UndelMessage)));
    if (Intermech.Workflow.Design.Holder.IsAdmin && (flag5 || !flag4 && !flag2))
      this._superfluousCommands.Remove("Delete");
    if (Intermech.Workflow.Design.Holder.IsAdmin && ControlFuncs.IsKeyPressed(Keys.ControlKey) && ControlFuncs.IsKeyPressed(Keys.ShiftKey))
      this._superfluousCommands.Clear();
    foreach (string superfluousCommand in this._superfluousCommands)
      mergedCommands.Add(superfluousCommand, new CommandInfo(0));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  protected void PerformMailRefresh(
    System.IServiceProvider services,
    ActivityPerformMailInfo activityPerformMailInfo = null)
  {
    if (activityPerformMailInfo == null)
    {
      NotificationEventArgs e = (NotificationEventArgs) new MailRefreshWithoutFormPopupEventArgs("MailRefresh");
      BaseHolder.NotificationService.FireEvent((object) null, e);
      wfFunx.OrganizerContext(services)?.Refresh();
    }
    else
      new Thread(new ParameterizedThreadStart(this.PerformMailThread)).Start((object) activityPerformMailInfo);
  }

  private void PerformMailThread(object activityPerformMailInfoParam)
  {
    this._activityPerformMailInfo = activityPerformMailInfoParam as ActivityPerformMailInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetObject(this._activityPerformMailInfo.ActivityID, false) is IActivity activity))
        return;
      for (int index = 30; this._activityPerformMailInfo.ActivityStatus == activity.Status && index > 0; --index)
      {
        Thread.Sleep(500);
        if (index % 15 == 0)
        {
          NotificationEventArgs e = (NotificationEventArgs) new MailRefreshWithoutFormPopupEventArgs("MailRefresh");
          BaseHolder.NotificationService.FireEvent((object) null, e);
        }
      }
      NotificationEventArgs e1 = (NotificationEventArgs) new MailRefreshWithoutFormPopupEventArgs("MailRefresh");
      BaseHolder.NotificationService.FireEvent((object) null, e1);
    }
  }

  private bool ConfirmSendTo(ISelectedItems items, bool goNext)
  {
    if (goNext)
    {
      if (!MailSettings.Cfg.ConfirmSendNext)
        return true;
    }
    else if (!MailSettings.Cfg.ConfirmSendBack)
      return true;
    string str1 = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        if (sessionKeeper.Session.GetObject(itemData.ObjectID, false) is IActivity activity)
        {
          IDBAttribute attributeById = activity.GetAttributeByID(wfConsts.AttrProcessID);
          str1 += "\r\n";
          if (attributeById != null)
            str1 = $"{str1}{attributeById.AsString}.";
          str1 += itemData.Caption;
        }
      }
    }
    if (!(str1 != ""))
      return true;
    string str2 = goNext ? LocalizationHolder.GetString("SendToNext") : LocalizationHolder.GetString("SendToBack");
    return MessageBox.Show(string.Format(LocalizationHolder.GetString("SendToPrompt"), (object) str2, (object) str1), LocalizationHolder.GetString("Confirmation"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
  }

  protected void SendTo(ISelectedItems items, bool goNext, System.IServiceProvider viewServices)
  {
    if (items.Count <= sc_21648.ssp_workflow_21649(1985740084) || !this.ConfirmSendTo(items, goNext))
      return;
    AttachmentList attachmentList = new AttachmentList();
    long num1 = 0;
    if ((viewServices.GetService(typeof (IViewsManager)) is IViewsManager service1 ? service1.ActiveViewPage : (IViewPage) null)?.Control is MailItemsView control1)
    {
      Control activeControl = control1.GetViewsManager().ActiveControl;
      Control control = (Control) null;
      try
      {
        if (activeControl is FormDesignerView formDesignerView)
        {
          num1 = formDesignerView.FormID;
          control = formDesignerView.Parent;
          formDesignerView.Parent = (Control) null;
        }
        control1.SaveEmbeddedViewsData();
      }
      finally
      {
        if (control != null)
          (activeControl as FormDesignerView).Parent = control;
      }
    }
    ActivityPerformMailInfo activityPerformMailInfo = new ActivityPerformMailInfo();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        try
        {
          IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
          if (!(sessionKeeper.Session.GetObject(itemData.ObjectID, false) is IExecutedActivity executedActivity))
            throw new NotificationException(string.Format(LocalizationHolder.rm.GetString("ActivityNotFound"), (object) itemData.Caption));
          using (RemoteLock remoteLock = new RemoteLock())
          {
            remoteLock.Add((object) executedActivity);
            if (!wfConsts.ExecStatuses.Contains(executedActivity.Status))
              throw new NotificationException(string.Format(LocalizationHolder.rm.GetString("ActivityAlreadyCompleted"), (object) itemData.Caption));
            if (!goNext && executedActivity.RollbackKind == RollbackKind.Disabled)
            {
              string message = LocalizationHolder.rm.GetString("Workflow.Server_12");
              if (items.Count > 1)
                message = $"{message} [{executedActivity.Caption}]";
              throw new NotificationException(message);
            }
            if (!goNext)
            {
              ActivityFlags activityFlags = executedActivity.Flags | ActivityFlags.Rollback;
              executedActivity.Flags = activityFlags;
            }
            attachmentList.Load((IDBObject) executedActivity);
            AttachmentList workCopies = attachmentList.WorkCopies;
            if (workCopies.Count > 0 && (MessageBox.Show((IWin32Window) null, string.Format(LocalizationHolder.rm.GetString("Workflow.Client_17"), (object) string.Empty), LocalizationHolder.rm.GetString("Workflow.Client_18"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK || !CheckInObjectsForm.CheckInAttachments(workCopies)))
              return;
            DialogResult dialogResult = DialogResult.Yes;
            if (attachmentList.CheckOutByOtherUser.Count > 0)
              dialogResult = MessageBox.Show("Отправляемое действие содержит вложения, взятые другим пользователем на изменение. Это может привести к ошибкам в дальнейшей работе процесса. Хотите продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.No)
              return;
            wfFunx.ExecClientScript(ScriptKind.BeforeExec, (IActivity) executedActivity);
            IDBAttribute dbAttribute = (IDBAttribute) null;
            if (!goNext)
            {
              IDBObject dbObject1 = sessionKeeper.Session.GetObject(executedActivity.Process.ObjectID, false);
              if (dbObject1 != null)
              {
                IDBAttribute attributeById = dbObject1.GetAttributeByID(wfConsts.AttrPrototypeID);
                IDBObject dbObject2 = sessionKeeper.Session.GetObject(attributeById.AsInteger, false);
                if (dbObject2 != null)
                  dbAttribute = dbObject2.GetAttributeByID(wfConsts.AttrShowFormWithActivityBackID);
              }
            }
            if (goNext || dbAttribute != null && dbAttribute.AsBoolean)
            {
              long formid = 0;
              IDBAttribute attributeById = executedActivity.GetAttributeByID(wfConsts.AttrFormID);
              if (attributeById != null)
                formid = attributeById.AsInteger;
              if (formid > 0L && num1 != formid)
              {
                if (FormDlg.EditForm(itemData.ObjectID, formid, !goNext))
                  executedActivity.Changed(ActivityChanged.Variables);
                else
                  continue;
              }
            }
            if (goNext && executedActivity.ObjectType == wfConsts.RemoteSubProcessTypeID)
            {
              if (!(ApplicationServices.Container.GetService(typeof (IPublicationService)) is IPublicationService service2))
                throw new Exception("Сервис публикации не найден!");
              List<Tuple<long, int>> items1 = new List<Tuple<long, int>>();
              foreach (IAttachment attachment in (IEnumerable<IAttachment>) executedActivity.Attachments)
                items1.Add(new Tuple<long, int>(attachment.ObjectID, attachment.ObjectType));
              ExtProperties extProperties1 = new ExtProperties((IDBObject) executedActivity, wfConsts.AttrAddInfoID);
              ExtendedPublishOptions options = new ExtendedPublishOptions(PublishCompositionOptions.None, (int) extProperties1.ReadInteger("MaxCompositionLevel", -1L), extProperties1.ReadList<int>("FRelTypes"), extProperties1.ReadList<int>("FTypes"), (FiltrationSettings) null);
              if (extProperties1.ReadBool("GiveOwnership"))
                options.OwnerSite = new char?('Y');
              options.AutoReplication = extProperties1.Ini.ReadBoolean("Props", "AutoPublishReplication", true);
              options.TaskPriority = (TaskPriority) extProperties1.ReadInteger("RemoteTaskPriority", 0L);
              string str = extProperties1.Read("Site");
              options.EnableSites = str;
              if (service2.ShowPublishOptions(items1, options))
              {
                ExtProperties extProperties2 = extProperties1;
                char? ownerSite = options.OwnerSite;
                int? nullable = ownerSite.HasValue ? new int?((int) ownerSite.GetValueOrDefault()) : new int?();
                int num2 = 89;
                int num3 = nullable.GetValueOrDefault() == num2 & nullable.HasValue ? 1 : 0;
                extProperties2.WriteBool("GiveOwnership", num3 != 0, ExtPropertiesFlag.RemoteSubprocess);
                extProperties1.Write("MaxCompositionLevel", (long) options.CountLevels, ExtPropertiesFlag.RemoteSubprocess);
                extProperties1.WriteList<int>("FTypes", options.EnableTypes, ExtPropertiesFlag.RemoteSubprocess);
                extProperties1.WriteList<int>("FRelTypes", options.EnableRelationTypes, ExtPropertiesFlag.RemoteSubprocess);
                extProperties1.WriteBool("AutoPublishReplication", options.AutoReplication, ExtPropertiesFlag.RemoteSubprocess);
                extProperties1.Write("RemoteTaskPriority", (long) options.TaskPriority, ExtPropertiesFlag.RemoteSubprocess);
                extProperties1.Save((IDBObject) executedActivity);
                executedActivity.Changed(ActivityChanged.ExtProps);
              }
              else
                continue;
            }
            wfFunx.ExecClientScript(ScriptKind.AfterExec, (IActivity) executedActivity);
            activityPerformMailInfo.ActivityStatus = executedActivity.Status;
            activityPerformMailInfo.ActivityID = executedActivity.ObjectID;
            if (!goNext && executedActivity.Flags.HasFlag((Enum) ActivityFlags.RequireAnswerText) && string.IsNullOrEmpty(executedActivity.MessageText.Trim()))
              throw new NotificationException(LocalizationHolder.rm.GetString("AnswerRequiredErr"));
            if (executedActivity is IApproveActivity approveActivity & goNext)
            {
              approveActivity.CheckAllSigned(false, out HashSet<long> _);
              approveActivity.Flags |= ActivityFlags.SignsChecked;
            }
            executedActivity.NextStep(goNext);
          }
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
      }
    }
    this.PerformMailRefresh(viewServices, activityPerformMailInfo);
  }

  public void SendToNextCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this.SendTo(items, true, viewServices);
  }

  public void SendToBackCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this.SendTo(items, false, viewServices);
  }

  public void AbortProcessCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.Count <= sc_21648.ssp_workflow_21650(693362742) || MessageBox.Show(LocalizationHolder.rm.GetString("Workflow.Client_20"), LocalizationHolder.rm.GetString("Workflow.Client_21"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int i = 0; i < items.Count; ++i)
      {
        long processId = this.GetProcessID(sessionKeeper.Session, items, i);
        if (processId != 0L)
          wfFunx.AbortProcess(processId);
      }
      this.PerformMailRefresh(viewServices);
    }
  }

  private long GetProcessID(IUserSession session, ISelectedItems items, int i)
  {
    IDBTypedObjectID itemData = items.GetItemData(i, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    long processId = 0;
    if (itemData.ObjectType == wfConsts.ProcessesTypeID)
      processId = itemData.ObjectID;
    else if (wfConsts.IsWorkflowMessage(itemData.ObjectType))
      processId = session.GetObjectAttribute(itemData.ObjectID, (object) wfConsts.AttrProcessID, true, false).AsInteger;
    return processId;
  }

  public void OpenProcessCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int i = 0; i < items.Count; ++i)
      {
        long processId = this.GetProcessID(sessionKeeper.Session, items, i);
        if (processId != 0L)
          wfFunx.OpenProcess(processId, Convert.ToBoolean(additionalInfo));
      }
    }
  }

  private void SetDeletionStatus(
    ISelectedItems items,
    DeletionStatus status,
    System.IServiceProvider services)
  {
    if (items.Count <= sc_21648.ssp_workflow_21651(1251297813))
      return;
    int num1 = MailItemsView.NodeCategoryID(items, services);
    MailFolder folder = MailFolder.Inbox;
    if (num1 == Intermech.Navigator.Consts.CategoryMailOutbox)
      folder = MailFolder.Outbox;
    else if (num1 == Intermech.Navigator.Consts.CategoryMailProcessed)
      folder = MailFolder.Completed;
    else if (num1 == Intermech.Navigator.Consts.CategoryMailTrash)
      folder = MailFolder.Deleted;
    this._checkMailService?.BeginUpdate();
    int num2 = 0;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        List<string> stringList = new List<string>();
        for (int index = 0; index < items.Count; ++index)
        {
          IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
          if (itemData.ObjectType == wfConsts.WorkOfferTypeID && status != DeletionStatus.Normal)
          {
            stringList.Add(itemData.Caption);
          }
          else
          {
            if (sessionKeeper.Session.GetObject(itemData.ObjectID) is IMailObject mailObject)
              mailObject.SetDeletionStatus(folder, status);
            if (items.GetItemID(index) is MailNodeID itemId && itemId.RecipStatus == RecipStatus.Unread)
              ++num2;
          }
        }
        if (stringList.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Удаление почтового предложения запрещено. Для удаления предложения из почты нужно либо принять его, либо отказаться. Список предложений которые не были удалены: ");
          stringBuilder.AppendLine();
          foreach (string str in stringList)
          {
            stringBuilder.Append(str);
            stringBuilder.AppendLine();
          }
          throw new Exception(stringBuilder.ToString());
        }
      }
    }
    finally
    {
      this._checkMailService?.EndUpdate(-num2);
      this.PerformMailRefresh(services);
    }
  }

  public void DelMessage(ISelectedItems items, System.IServiceProvider services, object additionalInfo)
  {
    if (MailSettings.Cfg.WarnOnDeletion && MessageBox.Show(LocalizationHolder.rm.GetString("Workflow.Client_24"), LocalizationHolder.rm.GetString("Workflow.Client_25"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.SetDeletionStatus(items, DeletionStatus.Deleted, services);
  }

  public void UndelMessage(ISelectedItems items, System.IServiceProvider services, object additionalInfo)
  {
    this.SetDeletionStatus(items, DeletionStatus.Normal, services);
  }

  public void ProcessHistoryCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int i = 0; i < items.Count; ++i)
      {
        long processId = this.GetProcessID(sessionKeeper.Session, items, i);
        if (processId != 0L)
          wfFunx.ShowProcessHistory(processId);
      }
    }
  }

  private void MarkReadUnread(
    ISelectedItems items,
    RecipStatus status,
    System.IServiceProvider viewServices)
  {
    int count = 0;
    this._checkMailService?.BeginUpdate();
    try
    {
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemID(index) is MailNodeID itemId && itemId.RecipStatus != status)
        {
          itemId.RecipStatus = status;
          if (itemId.RecipStatus == status)
            ++count;
        }
      }
      if (!((viewServices.GetService(typeof (IViewsManager)) is IViewsManager service ? service.ActiveViewPage : (IViewPage) null)?.Control is MailItemsView control))
        return;
      control.Refresh();
    }
    finally
    {
      if (status == RecipStatus.Read)
        count = -count;
      this._checkMailService?.EndUpdate(count);
    }
  }

  public void MarkRead(ISelectedItems items, System.IServiceProvider viewServices, object additionalInfo)
  {
    this.MarkReadUnread(items, RecipStatus.Read, viewServices);
  }

  public void MarkUnread(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this.MarkReadUnread(items, RecipStatus.Unread, viewServices);
  }

  public void ReplaceParticipantCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    using (ReplaceUserForm replaceUserForm = new ReplaceUserForm())
    {
      if (replaceUserForm.ShowDialog() != DialogResult.OK || replaceUserForm.UserID <= 0L || replaceUserForm.ToUserID <= 0L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int i = 0; i < items.Count; ++i)
        {
          long processId = this.GetProcessID(sessionKeeper.Session, items, i);
          if (processId != 0L && sessionKeeper.Session.GetObject(processId, false) is IProcess process)
            process.ReplaceParticipant(replaceUserForm.UserID, replaceUserForm.ToUserID);
        }
      }
      this.PerformMailRefresh(viewServices);
    }
  }

  public void RecallCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    string str1 = "";
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      HashSet<long> longSet = new HashSet<long>();
      for (int i = 0; i < items.Count; ++i)
      {
        long processId = this.GetProcessID(sessionKeeper.Session, items, i);
        if (processId != 0L && !longSet.Contains(processId))
        {
          longSet.Add(processId);
          if (sessionKeeper.Session.GetObject(processId, false) is IProcess process)
          {
            string str2 = "";
            foreach (IActivity activity in process.Activities)
            {
              if (activity.Executed && wfConsts.RollbackActivityKinds.Contains(activity.Kind))
              {
                if (str2 != "")
                  str2 += ", ";
                str2 += activity.Name;
              }
            }
            if (str2 != "")
            {
              longList.Add(processId);
              string str3 = $"\r\n{LocalizationHolder.rm.GetString("Workflow.Client_40")} \"{process.Name}\": {str2}.\r\n";
              if (str1 != "")
                str1 += "\r\n";
              str1 += str3;
            }
          }
        }
      }
    }
    if (longList.Count <= 0)
      throw new NotificationException(LocalizationHolder.rm.GetString("NothingToRecall"));
    if (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("RecallPrompt"), (object) str1), "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectID in longList)
      {
        Dictionary<long, string> dictionary = sessionKeeper.Session.GetObject(objectID, false) is IProcess process ? process.Recall() : (Dictionary<long, string>) null;
        if (dictionary != null && dictionary.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Не все действия были отозваны, возврат назад с текущих шагов запрещён, либо попытка отозвать подпроцесс в котором выполняется действие с запретом возврата. Список действий которые невозможно отозвать:\n");
          foreach (KeyValuePair<long, string> keyValuePair in dictionary)
            stringBuilder.Append($"\"{keyValuePair.Value}\" ({keyValuePair.Key})\n");
          this.PerformMailRefresh(viewServices);
          throw new WorkflowException(stringBuilder.ToString());
        }
      }
    }
    this.PerformMailRefresh(viewServices);
  }
}
