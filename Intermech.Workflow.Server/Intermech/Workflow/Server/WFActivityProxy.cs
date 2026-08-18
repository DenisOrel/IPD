// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WFActivityProxy
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Workflow.Server.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace Intermech.Workflow.Server;

public class WFActivityProxy
{
  private long _originalActivityID;
  private readonly long _currentProcessID;
  private readonly long _currentUserStarterID;
  private long _senderID = -1;
  private IEventLogHelper _eventLogHelper;
  private string _wfActivityProxyLogFileName = "wfActivityProxy.log";
  private string _wfProxyErrorCaption = "Workflow proxy error: ";
  private int _nonUserActivitiesCounter;
  private AttachmentList _savedAttachmentListFromCase;
  private bool _savedCaseIsError;
  private long _currentActingUserID;

  private event WFActivityProxy.ProcessExecutedHandler ProcessExecutedComleted;

  public WFActivityProxy(
    long currentProcessID,
    WFActivity originalSenderActivity,
    int nonUserActivitiesCounter = 0,
    WFActivityProxy.ProcessExecutedHandler processExecutedCompetedHandler = null)
  {
    this._currentProcessID = currentProcessID;
    this._originalActivityID = originalSenderActivity.ObjectID;
    this._currentUserStarterID = originalSenderActivity.UserSession.UserID;
    this._currentActingUserID = originalSenderActivity.UserSession.ActingUserID;
    this._eventLogHelper = ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    this._nonUserActivitiesCounter = nonUserActivitiesCounter;
    this.ProcessExecutedComleted = processExecutedCompetedHandler;
  }

  public WFActivityProxy(
    long currentProcessID,
    long originalSenderActivityID,
    long starterUserID,
    int nonUserActivitiesCounter = 0,
    WFActivityProxy.ProcessExecutedHandler processExecutedCompetedHandler = null)
  {
    this._currentProcessID = currentProcessID;
    this._originalActivityID = originalSenderActivityID;
    this._currentUserStarterID = starterUserID;
    this._eventLogHelper = ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    this._nonUserActivitiesCounter = nonUserActivitiesCounter;
    this.ProcessExecutedComleted = processExecutedCompetedHandler;
  }

  public void ExecuteNextAsync(
    long senderID,
    long senderActivityID,
    List<WFLink> nextStepLinks,
    VarList senderVariableList)
  {
    new Thread(new ParameterizedThreadStart(this.ExecuteNext)).Start((object) new object[4]
    {
      (object) senderID,
      (object) senderActivityID,
      (object) nextStepLinks,
      (object) senderVariableList
    });
  }

  public void NextStepAsync(bool goNext)
  {
    new Thread(new ParameterizedThreadStart(this.NextStep)).Start((object) goNext);
  }

  public void ExecuteNextActivity(
    long senderID,
    long senderActivityID,
    List<WFLink> nextStepLinks,
    VarList senderVariableList)
  {
    UserSession sessionTemporaryClone = (ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents)?.GetSystemSessionTemporaryClone("WFS.WFActivityProxy.ExecuteNextActivity") as UserSession;
    this._senderID = senderID;
    if (sessionTemporaryClone != null)
    {
      WFProcess process = sessionTemporaryClone.GetObject(this._currentProcessID, false) as WFProcess;
      try
      {
        if (process == null)
          throw new KernelException($"Объект процесса с идентификатором '{this._currentProcessID}' не найден. Запуск невозможен.");
        WFActivity wfActivity = sessionTemporaryClone.GetObject(senderActivityID, false) as WFActivity;
        if (this._savedAttachmentListFromCase != null)
        {
          wfActivity?.Attachments.Clear();
          wfActivity?.Attachments.AddList(this._savedAttachmentListFromCase);
          this._savedAttachmentListFromCase = (AttachmentList) null;
        }
        if (this._savedCaseIsError && wfActivity is Case case1)
        {
          case1.FilterCheckError = this._savedCaseIsError;
          this._savedCaseIsError = false;
        }
        process.MarkAsPreExecuted(senderActivityID, false);
        this.MoveDownCollectors(nextStepLinks);
        Dictionary<long, ActivityNextStepInfo> source = new Dictionary<long, ActivityNextStepInfo>();
        for (int index = 0; index < nextStepLinks.Count; ++index)
        {
          WFActivity activity = sessionTemporaryClone.GetObject(nextStepLinks[index].ToID, false) as WFActivity;
          ++this._nonUserActivitiesCounter;
          if (activity is Intermech.Workflow.Server.Activities.Timer timer)
          {
            timer.GetRealClones((WFScheme) process);
            if (timer.Clones != null && timer.Clones.Count > 0 && timer.IsResetLink(nextStepLinks[index]))
            {
              WFActivity last = timer.Clones.FindLast((Predicate<WFActivity>) (x => x is Intermech.Workflow.Server.Activities.Timer));
              if (last != null)
              {
                nextStepLinks[index].ToID = last.ObjectID;
                activity = last;
              }
            }
          }
          if (this.CheckActivity(ref activity, nextStepLinks[index], process, senderVariableList))
          {
            process.MarkAsPreExecuted(nextStepLinks[index].ToID, false);
            process.MarkAsPreExecuted(activity.ObjectID, true);
            nextStepLinks[index].ToID = activity.ObjectID;
          }
          if (activity.LCStep == wfConsts.ActivityExecLCStepID)
          {
            activity.SenderID = this._senderID;
            wfActivity?.ForwardDataFlow(activity, nonUserActivitiesCounter: this._nonUserActivitiesCounter);
            if (activity.ReadyToGo(senderActivityID))
            {
              if (activity.Execute(true))
              {
                if (activity.NextStepLinks.Count > 0)
                {
                  ActivityNextStepInfo activityNextStepInfo = new ActivityNextStepInfo(activity.ObjectID)
                  {
                    NextStepLinks = activity.NextStepLinks,
                    VariableList = activity.VariableList
                  };
                  if (activity.ModifyAttachmentInCaseActivity)
                    activityNextStepInfo.SavedAttachmentList = activity.Attachments;
                  if (activity is Case case2)
                    activityNextStepInfo.SavedCaseIsError = case2.FilterCheckError;
                  if (source.ContainsKey(activity.ObjectID))
                    source[activity.ObjectID] = activityNextStepInfo;
                  else
                    source.Add(activity.ObjectID, activityNextStepInfo);
                }
              }
              else
                nextStepLinks[index] = (WFLink) null;
            }
            else
            {
              process.MarkAsPreExecuted(activity.ObjectID, false);
              if (activity.Flags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers) && activity.ErrorOccured && activity.ActivityResult == ActivityResult.Back)
              {
                bool flag = false;
                try
                {
                  activity.NextStep(false);
                  flag = true;
                }
                catch (Exception ex)
                {
                  activity.DumpException(ex);
                  activity.Status = ActivityStatus.Completed;
                  activity.Attributes.AddAttribute(wfConsts.AttrCompletedID, false, new object[1]
                  {
                    (object) DateTime.Now
                  });
                  activity.PrepareNextStepLinks();
                  activity.MarkNextStepActivitiesAsPreExecuted();
                  this.ExecuteNextActivity(activity.SenderID, activity.ObjectID, activity.NextStepLinks, activity.VariableList);
                }
                if (flag)
                {
                  activity.MarkNextStepActivitiesAsPreExecuted();
                  this.ExecuteNextActivity(activity.SenderID, activity.ObjectID, activity.NextStepLinks, activity.VariableList);
                }
              }
            }
          }
          else
          {
            nextStepLinks[index] = (WFLink) null;
            this._eventLogHelper.AddToTrace($"{this._wfProxyErrorCaption}Попытка обращения к действию '{activity.Caption}' ({activity.ObjectID}) на этапе создания в процессе '{process.Caption}' ({process.ObjectID}). Операция пропущена.", Consts.traceAlways, this._wfActivityProxyLogFileName);
          }
        }
        for (int index1 = source.Count - 1; index1 >= 0; --index1)
        {
          for (int index2 = index1 - 1; index2 >= 0; --index2)
          {
            KeyValuePair<long, ActivityNextStepInfo> keyValuePair = source.ElementAt<KeyValuePair<long, ActivityNextStepInfo>>(index1);
            ActivityNextStepInfo firstActivityInfo = keyValuePair.Value;
            keyValuePair = source.ElementAt<KeyValuePair<long, ActivityNextStepInfo>>(index2);
            ActivityNextStepInfo secondActivityInfo = keyValuePair.Value;
            if (!this.DeleteDuplicateNextCollectors(firstActivityInfo, secondActivityInfo))
              break;
          }
        }
        foreach (KeyValuePair<long, ActivityNextStepInfo> keyValuePair in source)
        {
          this._savedAttachmentListFromCase = keyValuePair.Value.SavedAttachmentList;
          this._savedCaseIsError = keyValuePair.Value.SavedCaseIsError;
          this.ExecuteNextActivity(senderID, keyValuePair.Key, keyValuePair.Value.NextStepLinks, keyValuePair.Value.VariableList);
        }
      }
      catch (Exception ex1)
      {
        if (ex1 is AbortException)
          throw;
        try
        {
          if (!(sessionTemporaryClone.GetObject(this._originalActivityID, false) is WFActivity wfActivity1))
            throw new KernelException($"Невозможно продолжить процесс '{this._currentProcessID}'. Невозможно создать следующее действие.");
          WFActivity toAct = wfActivity1.Clone(senderVariableList);
          toAct.DumpException(ex1);
          if (!toAct.ReadyToGo(senderActivityID))
            return;
          if (sessionTemporaryClone.GetObject(senderActivityID, false) is WFActivity wfActivity2)
            wfActivity2.ForwardDataFlow(toAct);
          toAct.Execute(false);
        }
        catch (Exception ex2)
        {
          this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex2, this._wfActivityProxyLogFileName);
        }
      }
      finally
      {
        sessionTemporaryClone.Logout("WFS.WFActivityProxy.ExecuteNextActivity");
      }
    }
    else
    {
      try
      {
        throw new KernelException($"Невозможно выполнить действия после '{senderActivityID}'. Системная сессия не получена.");
      }
      catch (Exception ex)
      {
        this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex, this._wfActivityProxyLogFileName);
      }
    }
  }

  private void ExecuteNext(object executeParams)
  {
    IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    userSession = (UserSession) null;
    try
    {
      object[] objArray = executeParams as object[];
      long senderID = Convert.ToInt64(objArray[0]);
      long senderActivityID = Convert.ToInt64(objArray[1]);
      List<WFLink> nextStepLinks = objArray[2] as List<WFLink>;
      VarList senderVariableList = objArray[3] as VarList;
      if (service?.GetSystemSessionTemporaryClone("WFS.WFActivityProxy.ExecuteNextThread") is UserSession userSession)
      {
        Action action = (Action) (() => this.ExecuteNextActivity(senderID, senderActivityID, nextStepLinks, senderVariableList));
        IAsyncResult result = action.BeginInvoke((AsyncCallback) null, (object) null);
        try
        {
          int num = 4320;
          bool flag;
          for (flag = result.AsyncWaitHandle.WaitOne(10000); !flag && num > 0; --num)
            flag = result.AsyncWaitHandle.WaitOne(10000);
          if (!flag)
            throw new TimeoutException("Время ожидания запуска процесса истекло.");
          action.EndInvoke(result);
          if (this.ProcessExecutedComleted == null)
            return;
          this.ProcessExecutedComleted(this._currentProcessID, this._currentUserStarterID);
        }
        catch (Exception ex)
        {
          this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex, this._wfActivityProxyLogFileName);
        }
      }
      else
      {
        try
        {
          throw new KernelException($"Невозможно выполнить действия после '{senderActivityID}'. Системная сессия не получена.");
        }
        catch (Exception ex)
        {
          this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex, this._wfActivityProxyLogFileName);
        }
      }
    }
    catch (Exception ex)
    {
      this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex, this._wfActivityProxyLogFileName);
    }
    finally
    {
      userSession?.Logout("WFS.WFActivityProxy.ExecuteNextThread");
    }
  }

  private void NextStep(object startParams)
  {
    try
    {
      bool goNext = Convert.ToBoolean(startParams);
      Action action = (Action) (() => this.NextStepMethod(goNext, nameof (NextStep)));
      IAsyncResult result = action.BeginInvoke((AsyncCallback) null, (object) null);
      try
      {
        int num = 4320;
        bool flag;
        for (flag = result.AsyncWaitHandle.WaitOne(10000); !flag && num > 0; --num)
          flag = result.AsyncWaitHandle.WaitOne(10000);
        if (!flag)
          throw new TimeoutException("Время ожидания продолжения процесса истекло.");
        action.EndInvoke(result);
        if (this.ProcessExecutedComleted == null)
          return;
        this.ProcessExecutedComleted(this._currentProcessID, this._currentUserStarterID);
      }
      catch (Exception ex)
      {
        this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex, this._wfActivityProxyLogFileName);
      }
    }
    catch (Exception ex)
    {
      this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex, this._wfActivityProxyLogFileName);
    }
  }

  public void RecallNextStep(bool goNext) => this.NextStepMethod(goNext, nameof (RecallNextStep));

  private void NextStepMethod(bool goNext, string sessionName)
  {
    IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    userSession = (UserSession) null;
    try
    {
      if (service?.GetSystemSessionTemporaryClone("WFS.WFActivityProxy." + sessionName) is UserSession userSession)
      {
        WFActivity wfActivity = userSession.GetObject(this._originalActivityID, false) as WFActivity;
        bool flag = false;
        if (wfActivity == null)
          return;
        if (!wfActivity.Executed)
          return;
        try
        {
          switch (wfActivity)
          {
            case UserActivity userActivity:
              userActivity.SetActingUserID(this._currentActingUserID);
              break;
            case Script script:
              script.SetActingUserID(this._currentActingUserID);
              break;
            case RemoteProcess remoteProcess:
              remoteProcess.SetActingUserID(this._currentActingUserID);
              break;
          }
          wfActivity.NextStep(goNext);
          flag = true;
        }
        catch (Exception ex)
        {
          wfActivity.DumpException(ex);
          wfActivity.Status = ActivityStatus.Completed;
          wfActivity.Attributes.AddAttribute(wfConsts.AttrCompletedID, false, new object[1]
          {
            (object) DateTime.Now
          });
          wfActivity.PrepareNextStepLinks();
          wfActivity.MarkNextStepActivitiesAsPreExecuted();
          this.ExecuteNextActivity(wfActivity.SenderID, wfActivity.ObjectID, wfActivity.NextStepLinks, wfActivity.VariableList);
        }
        if (!flag)
          return;
        wfActivity.MarkNextStepActivitiesAsPreExecuted();
        this.ExecuteNextActivity(wfActivity.SenderID, wfActivity.ObjectID, wfActivity.NextStepLinks, wfActivity.VariableList);
      }
      else
      {
        try
        {
          throw new KernelException($"Невозможно выполнить действия после '{this._originalActivityID}'. Системная сессия не получена.");
        }
        catch (Exception ex)
        {
          this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex, this._wfActivityProxyLogFileName);
          if (!(sessionName == "RecallNextStep"))
            return;
          throw;
        }
      }
    }
    finally
    {
      userSession?.Logout("WFS.WFActivityProxy." + sessionName);
    }
  }

  public bool CheckActivity(
    ref WFActivity activity,
    WFLink execLink,
    WFProcess process,
    VarList senderVariablesList)
  {
    bool isAlien = false;
    bool correspondingActivity = this.FindCorrespondingActivity(ref activity, ref isAlien, process);
    ActivityStatus status = activity.Status;
    activity.GetRealClones((WFScheme) process);
    if (activity.Collector)
    {
      if (status == ActivityStatus.CollectorWaiting)
        return correspondingActivity;
      foreach (WFActivity clone in activity.Clones)
      {
        if (clone.Status == ActivityStatus.CollectorWaiting)
        {
          activity = clone;
          return true;
        }
      }
    }
    if (activity is Intermech.Workflow.Server.Activities.Timer timer && timer.IsResetLink(execLink))
    {
      timer.ResetTimer = true;
      return correspondingActivity;
    }
    bool flag = status != 0;
    if (!flag & isAlien)
    {
      activity = process.CreateActivity(activity, true, ownerID: this._senderID);
      activity.VariableList.Assign(senderVariablesList);
      activity.SaveVariables(false);
      activity.CommitCreation(false);
      activity.VariableList.Clear();
      activity.LCStep = wfConsts.ActivityExecLCStepID;
      return true;
    }
    if (flag)
      activity = activity.Clone(senderVariablesList);
    return flag;
  }

  protected bool FindCorrespondingActivity(
    ref WFActivity activity,
    ref bool isAlien,
    WFProcess process)
  {
    isAlien = activity.IsAlien(process.ObjectID);
    bool correspondingActivity = false;
    if (isAlien)
    {
      foreach (WFActivity activity1 in process.Activities)
      {
        if (activity1.ParentActivityID == activity.ObjectID)
        {
          activity = activity1;
          isAlien = false;
          correspondingActivity = true;
          break;
        }
      }
    }
    return correspondingActivity;
  }

  public void ExecuteCurrentActivity()
  {
    this._eventLogHelper.AddToTrace($"Workflow proxy: \"Вызван метод аварийного запуска процесса ExecuteCurrentActivity. ID процесса '{this._currentProcessID}'. ID запускаемого действия '{this._originalActivityID}'. ID пользователя '{this._currentUserStarterID}'\"", Consts.traceAlways, this._wfActivityProxyLogFileName);
    IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    userSession = (UserSession) null;
    this._senderID = this._currentUserStarterID;
    try
    {
      if (service?.GetSystemSessionTemporaryClone("WFS.WFActivityProxy.ExecuteCurrentActivity") is UserSession userSession)
      {
        if (!(userSession.GetObject(this._currentProcessID, false) is WFProcess wfProcess))
          throw new KernelException($"Объект процесса с идентификатором '{this._currentProcessID}' не найден. Запуск невозможен.");
        if (wfProcess.ProcessStatus != ActivityStatus.Executed)
          throw new KernelException($"Процесс с идентификатором '{this._currentProcessID}' имеет статус отличный от 'Выполняется'. Запуск невозможен.");
        if (!(userSession.GetObject(this._originalActivityID, false) is WFActivity wfActivity))
          return;
        WFActivity activity = wfActivity;
        if (activity is SystemActivity)
          ++this._nonUserActivitiesCounter;
        this.CheckActivity(ref activity, (WFLink) null, wfActivity.Process as WFProcess, wfActivity.VariableList);
        activity.SenderID = this._senderID;
        wfActivity.ForwardDataFlow(activity, nonUserActivitiesCounter: this._nonUserActivitiesCounter);
        if (activity.ReadyToGo(wfActivity.SenderActivityID))
        {
          if (!activity.Execute(true) || activity.NextStepLinks.Count <= 0)
            return;
          if (activity.ModifyAttachmentInCaseActivity)
            this._savedAttachmentListFromCase = activity.Attachments;
          if (activity is Case @case)
            this._savedCaseIsError = @case.FilterCheckError;
          this.ExecuteNextActivity(activity.SenderID, activity.ObjectID, activity.NextStepLinks, activity.VariableList);
        }
        else
        {
          if (!activity.Flags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers) || !activity.ErrorOccured || activity.ActivityResult != ActivityResult.Back)
            return;
          bool flag = false;
          try
          {
            activity.NextStep(false);
            flag = true;
          }
          catch (Exception ex)
          {
            activity.DumpException(ex);
            activity.Status = ActivityStatus.Completed;
            activity.Attributes.AddAttribute(wfConsts.AttrCompletedID, false, new object[1]
            {
              (object) DateTime.Now
            });
            activity.PrepareNextStepLinks();
            activity.MarkNextStepActivitiesAsPreExecuted();
            this.ExecuteNextActivity(activity.SenderID, activity.ObjectID, activity.NextStepLinks, activity.VariableList);
          }
          if (!flag)
            return;
          activity.MarkNextStepActivitiesAsPreExecuted();
          this.ExecuteNextActivity(activity.SenderID, activity.ObjectID, activity.NextStepLinks, activity.VariableList);
        }
      }
      else
      {
        try
        {
          throw new KernelException($"Невозможно выполнить действия после '{this._originalActivityID}'. Системная сессия не получена.");
        }
        catch (Exception ex)
        {
          this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex, this._wfActivityProxyLogFileName);
          throw;
        }
      }
    }
    finally
    {
      userSession?.Logout("WFS.WFActivityProxy.ExecuteCurrentActivity");
    }
  }

  public void ExecuteCustomSender(long activityID)
  {
    this._eventLogHelper.AddToTrace($"Workflow proxy: \"Вызван метод аварийного запуска процесса ExecuteCustomSender. ID процесса '{this._currentProcessID}'. ID запускаемого действия '{activityID}'. ID пользователя '{this._currentUserStarterID}'\"", Consts.traceAlways, this._wfActivityProxyLogFileName);
    IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    userSession = (UserSession) null;
    this._senderID = this._currentUserStarterID;
    try
    {
      if (service?.GetSystemSessionTemporaryClone("WFS.WFActivityProxy.ExecuteCustomSender") is UserSession userSession)
      {
        if (!(userSession.GetObject(this._currentProcessID, false) is WFProcess wfProcess))
          throw new KernelException($"Объект процесса с идентификатором '{this._currentProcessID}' не найден. Запуск невозможен.");
        if (wfProcess.ProcessStatus != ActivityStatus.Executed)
          throw new KernelException($"Процесс с идентификатором '{this._currentProcessID}' имеет статус отличный от 'Выполняется'. Запуск невозможен.");
        WFActivity wfActivity = userSession.GetObject(this._originalActivityID, false) as WFActivity;
        WFActivity activity = userSession.GetObject(activityID, false) as WFActivity;
        if (wfActivity == null || activity == null)
          return;
        if (activity is SystemActivity)
          ++this._nonUserActivitiesCounter;
        this.CheckActivity(ref activity, (WFLink) null, wfActivity.Process as WFProcess, wfActivity.VariableList);
        activity.SenderID = this._senderID;
        wfActivity.ForwardDataFlow(activity, nonUserActivitiesCounter: this._nonUserActivitiesCounter);
        if (activity.ReadyToGo(wfActivity.SenderActivityID))
        {
          if (!activity.Execute(true) || activity.NextStepLinks.Count <= 0)
            return;
          if (activity.ModifyAttachmentInCaseActivity)
            this._savedAttachmentListFromCase = activity.Attachments;
          if (activity is Case @case)
            this._savedCaseIsError = @case.FilterCheckError;
          this.ExecuteNextActivity(activity.SenderID, activity.ObjectID, activity.NextStepLinks, activity.VariableList);
        }
        else
        {
          if (!activity.Flags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers) || !activity.ErrorOccured || activity.ActivityResult != ActivityResult.Back)
            return;
          bool flag = false;
          try
          {
            activity.NextStep(false);
            flag = true;
          }
          catch (Exception ex)
          {
            activity.DumpException(ex);
            activity.Status = ActivityStatus.Completed;
            activity.Attributes.AddAttribute(wfConsts.AttrCompletedID, false, new object[1]
            {
              (object) DateTime.Now
            });
            activity.PrepareNextStepLinks();
            activity.MarkNextStepActivitiesAsPreExecuted();
            this.ExecuteNextActivity(activity.SenderID, activity.ObjectID, activity.NextStepLinks, activity.VariableList);
          }
          if (!flag)
            return;
          activity.MarkNextStepActivitiesAsPreExecuted();
          this.ExecuteNextActivity(activity.SenderID, activity.ObjectID, activity.NextStepLinks, activity.VariableList);
        }
      }
      else
      {
        try
        {
          throw new KernelException($"Невозможно выполнить действия после '{this._originalActivityID}'. Системная сессия не получена.");
        }
        catch (Exception ex)
        {
          this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex, this._wfActivityProxyLogFileName);
          throw;
        }
      }
    }
    finally
    {
      userSession?.Logout("WFS.WFActivityProxy.ExecuteCustomSender");
    }
  }

  private void MoveDownCollectors(List<WFLink> links)
  {
    for (int index = links.Count - 1; index >= 0; --index)
      links[index].Index = index;
    try
    {
      links.Sort(new Comparison<WFLink>(this.CompareLinksByCollectors));
    }
    catch (Exception ex)
    {
      this._eventLogHelper.TraceExeption(this._wfProxyErrorCaption, ex, this._wfActivityProxyLogFileName);
    }
  }

  private int CompareLinksByCollectors(WFLink l1, WFLink l2)
  {
    int num = l1.Index;
    int index = l2.Index;
    IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    userSession = (UserSession) null;
    try
    {
      WFActivity wfActivity1 = (service?.GetSystemSessionTemporaryClone("WFS.WFActivityProxy.CompareLinksByCollectors") is UserSession userSession ? userSession.GetObject(l1.ToID, false) : (IDBObject) null) as WFActivity;
      WFActivity wfActivity2 = userSession?.GetObject(l2.ToID, false) as WFActivity;
      if (wfActivity1 == null || wfActivity2 == null)
        return num - index;
      if (wfActivity1.Kind == ActivityKind.Stop)
        num = 10000;
      else if (wfActivity1.Collector)
        num += 1000;
      if (wfActivity2.Kind == ActivityKind.Stop)
        index += 10000;
      else if (wfActivity2.Collector)
        index += 1000;
    }
    finally
    {
      userSession?.Logout("WFS.WFActivityProxy.CompareLinksByCollectors");
    }
    return num - index;
  }

  private bool DeleteDuplicateNextCollectors(
    ActivityNextStepInfo firstActivityInfo,
    ActivityNextStepInfo secondActivityInfo)
  {
    bool flag = false;
    if (firstActivityInfo.ActivityID == secondActivityInfo.ActivityID)
      return false;
    for (int i = 0; i < firstActivityInfo.NextStepLinks.Count; i++)
    {
      if (firstActivityInfo.NextStepLinks[i].To.Collector || firstActivityInfo.NextStepLinks[i].To.Kind == ActivityKind.Stop)
      {
        flag = true;
        int index = secondActivityInfo.NextStepLinks.FindIndex((Predicate<WFLink>) (x => x.ToID == firstActivityInfo.NextStepLinks[i].ToID));
        if (index != -1)
          secondActivityInfo.NextStepLinks[index] = (WFLink) null;
      }
    }
    this.Pack(secondActivityInfo.NextStepLinks);
    return flag;
  }

  public void Pack(List<WFLink> linksToPack)
  {
    linksToPack.RemoveAll(new Predicate<WFLink>(this.EmptyPointer));
  }

  private bool EmptyPointer(WFLink link) => link == null;

  public delegate void ProcessExecutedHandler(long processID, long userID);
}
