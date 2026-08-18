// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.SubProcess
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class SubProcess(UserSession uSession, DataTable objectsTable) : 
  SystemActivity(uSession, objectsTable),
  ISubProcess,
  IExecutedActivity,
  IActivity,
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity
{
  private long _subSchemeID = -1;
  private long _subProcessID = -1;

  public override ActivityKind Kind => ActivityKind.SubProcess;

  private bool UseActualSchemeVersion
  {
    get
    {
      return this.ExtProps.HasFlag(ExtPropertiesFlag.SubProcess) && this.ExtProps.ReadBool(nameof (UseActualSchemeVersion));
    }
  }

  private bool UseCustomParticipant
  {
    get
    {
      return this.ExtProps.HasFlag(ExtPropertiesFlag.SubProcess) && this.ExtProps.ReadBool(nameof (UseCustomParticipant));
    }
  }

  private ParticipantList CustomParticipant
  {
    get
    {
      if (!this.ExtProps.HasFlag(ExtPropertiesFlag.SubProcess))
        return new ParticipantList((IUserSession) this.UserSession);
      string str = this.ExtProps.Ini.ReadString("Props", nameof (CustomParticipant), new ParticipantList((IUserSession) this.UserSession).AsString);
      return new ParticipantList((IUserSession) this.UserSession)
      {
        AsString = str
      };
    }
  }

  public long SubSchemeID
  {
    get
    {
      if (this._subSchemeID == -1L)
      {
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrSubprocessSchemeID);
        this._subSchemeID = attributeById != null ? attributeById.AsInteger : 0L;
        if (this.UseActualSchemeVersion)
        {
          IDBObject dbObject = this.UserSession.GetObject(this._subSchemeID, false);
          if (dbObject != null && !dbObject.IsBaseVersion)
          {
            IDBObject objectBaseVersionById = this.UserSession.GetObjectBaseVersionByID(dbObject.ID, false);
            if (objectBaseVersionById != null)
              this._subSchemeID = objectBaseVersionById.ObjectID;
          }
        }
      }
      return this._subSchemeID;
    }
  }

  internal long SubProcessID
  {
    get
    {
      if (this._subProcessID == -1L)
      {
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrSubprocessID);
        this._subProcessID = attributeById != null ? attributeById.AsInteger : 0L;
      }
      return this._subProcessID;
    }
    set
    {
      if (this._subProcessID == value)
        return;
      this._subProcessID = value;
      this.Attributes.AddAttribute(wfConsts.AttrSubprocessID, false, new object[1]
      {
        (object) this._subProcessID
      });
    }
  }

  public override string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    string s = base.Validate(checkSubProcessSchemes, checkedSchemesList);
    if (this.SubSchemeID == 0L)
      MiscFunx.AddNewLined(ref s, MiscFunx.ActivityIncomplete(this.Name));
    IDBObject dbObject = this.UserSession.GetObject(this.SubSchemeID, false);
    if (dbObject != null && dbObject is WFScheme wfScheme)
    {
      if (wfScheme.SchemeStatus >= ~SchemeStatus.Valid || wfScheme.SchemeStatus == SchemeStatus.Invalid)
        MiscFunx.AddNewLined(ref s, $"Шаблона процесса '{wfScheme.Caption}' настроенный на текущее действие некорректный.");
      WFScheme process;
      if (checkSubProcessSchemes && this.Process != null && (process = this.Process) != null && !process.SchemeDebugMode && wfScheme.SchemeDebugMode)
        MiscFunx.AddNewLined(ref s, "Шаблон процесса используемый в данном действии является отладочным. Наличие такого шаблона не допускается в рабочей версии.");
    }
    return s;
  }

  private string GenerateName(string procName)
  {
    string name = procName;
    string str1 = string.Empty;
    IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrSubprocFormatID);
    if (attributeById != null)
      str1 = attributeById.AsString;
    if (!string.IsNullOrEmpty(str1))
    {
      string str2 = str1.Replace("%1%", this.ProcessName).Replace("%2%", procName);
      DateTime now = DateTime.Now;
      name = str2.Replace("%3%", now.ToShortDateString()).Replace("%4%", now.ToShortTimeString());
    }
    return name;
  }

  internal override void PrepareActivity()
  {
    base.PrepareActivity();
    this._autoStep = !this.WaitForCompletion;
    IDBObject dbObject = this.UserSession.GetObjectCollection(wfConsts.ProcessesTypeID).Create(this.SubSchemeID);
    ((WFScheme) dbObject).prototype = (WFScheme) null;
    IDBAttribute byId = dbObject.Attributes.FindByID(wfConsts.AttrNameID);
    byId.AsString = this.GenerateName(byId.AsString);
    dbObject.CommitCreation(false);
    this.SubProcessID = dbObject.ObjectID;
    if (!(dbObject is WFProcess toProcess))
      return;
    string str = toProcess.Validate(true, (List<long>) null);
    if (!string.IsNullOrEmpty(str))
    {
      toProcess.Delete(0L);
      throw new WorkflowException(LocalizationHolder.rm.GetString("Workflow.Server_28") + str);
    }
    toProcess.Attributes.AddAttribute(wfConsts.AttrParentProcessID, false, new object[1]
    {
      (object) this.ProcessID
    });
    this.ForwardDataFlow(toProcess);
    this.ForwardDataFlow((WFActivity) toProcess.StartActivity, nonUserActivitiesCounter: this.NonUserActivitiesCounter);
    if (this.UseCustomParticipant)
    {
      ParticipantList participantList = new ParticipantList((IUserSession) this.UserSession);
      participantList.Assign(this.CustomParticipant);
      MiscFunx.ExpandParticipants((IDBAttributable) this, participantList);
      CheckParticipant(participantList);
      long id = participantList[0].ID;
      if (id == wfConsts.SystemUserID || id == 0L || id == -2L)
        toProcess.StartSubProcess(this.SenderID);
      else
        toProcess.StartSubProcess(id);
    }
    else
      toProcess.StartSubProcess(this.SenderID);

    void CheckParticipant(ParticipantList parts)
    {
      if (parts.Count == 0)
        parts.AddParticipant(ParticipantKind.User, this.SenderID);
      else if (parts[0].Kind != ParticipantKind.User)
        parts[0].ID = this.SenderID;
      if (parts.Count != 1 || parts[0].ID != wfConsts.SystemUserID)
        return;
      parts[0].ID = this.SenderID;
    }
  }

  internal override void SetStatus(
    IDBAttribute attr,
    ActivityStatus value,
    ActivityStatus oldStatus)
  {
    base.SetStatus(attr, value, oldStatus);
    if (oldStatus != ActivityStatus.Executed || value != ActivityStatus.Terminated || this.SubProcessID == -1L || !(this.UserSession.GetObject(this.SubProcessID, false) is WFProcess wfProcess))
      return;
    wfProcess.CheckAdminRights = false;
    try
    {
      wfProcess.StopProcess((IActivity) null, true);
      this.ActivityResult = ActivityResult.Back;
    }
    finally
    {
      wfProcess.CheckAdminRights = true;
    }
  }
}
