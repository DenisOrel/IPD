// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.Timer
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class Timer : SystemActivity
{
  internal bool ResetTimer;

  public Timer(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this._autoStep = false;
  }

  public override ActivityKind Kind => ActivityKind.Timer;

  public override bool Abort()
  {
    IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    int eventID = service.FindEvent(wfConsts.WorkflowTimerServiceGuid, 1, this.ObjectID, this.UserSession.DataManager);
    if (eventID > 0)
      service.DeleteEventID(eventID, this.UserSession.DataManager);
    return base.Abort();
  }

  internal override void PrepareActivity()
  {
    if (this.ResetTimer)
    {
      this.Abort();
    }
    else
    {
      base.PrepareActivity();
      PeriodInformation periodInformation = new PeriodInformation((IUserSession) this.UserSession);
      string str = this.ExtProps.Read("TimerPeriod");
      if (string.IsNullOrEmpty(str) && this.ExtProps.Ini.Root.Name == "Period")
        str = this.ExtProps.Ini.AsString;
      periodInformation.AsString = str;
      DateTime execTime = periodInformation.GetExecTime((IDBObject) this);
      if (execTime <= DateTime.UtcNow)
      {
        this._autoStep = true;
      }
      else
      {
        IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
        service.AddToTrace($"Событие N{service.AddEvent(new TimedEventProperties(0, execTime, DateTime.MinValue, wfConsts.WorkflowTimerServiceGuid, this.ObjectID, 0L, this.NonUserActivitiesCounter.ToString(), 1, 0), this.UserSession.DataManager)} для объекта N{this.ObjectID} зарегистрировано.", true);
      }
    }
  }

  public void ReplaceLink(long oldLinkId, long newLinkId)
  {
    IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrObjectListID);
    if (attributeById == null || attributeById.IsNull)
      return;
    List<long> longList = new List<long>();
    foreach (object obj in attributeById.Values)
      longList.Add((long) Convert.ToInt32(obj));
    int index1 = longList.IndexOf(Math.Abs(oldLinkId));
    if (index1 == -sc_22158.ssp_workflow_server_22159(1943595017))
      return;
    longList[index1] = Math.Abs(newLinkId);
    object[] objArray = new object[longList.Count];
    for (int index2 = 0; index2 < longList.Count; ++index2)
      objArray[index2] = (object) longList[index2];
    attributeById.Values = objArray;
  }

  public bool IsResetLink(WFLink link)
  {
    if (link == null)
      return false;
    IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrObjectListID);
    if (attributeById != null && !attributeById.IsNull)
    {
      List<long> longList = new List<long>();
      foreach (object obj in attributeById.Values)
        longList.Add((long) Convert.ToInt32(obj));
      if (longList.IndexOf(Math.Abs(link.ObjectID)) != -1)
        return true;
    }
    return false;
  }
}
