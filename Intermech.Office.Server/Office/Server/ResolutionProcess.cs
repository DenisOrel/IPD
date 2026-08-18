// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.ResolutionProcess
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Office.Interfaces;
using Intermech.Office.Server.AdditionalAttributes;
using Intermech.Workflow;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Office.Server;

internal abstract class ResolutionProcess : IResolutionProcess
{
  protected bool _Control;
  [NotNull]
  protected string _Name;
  protected Guid _ProcessTemplate;
  [NotNull]
  private readonly IAdditionalActivitiesAttributes[] _addAttributes;
  protected const string VariableCommissionUser = "COMMISSION_USER";
  protected const string VariableCommissionAuthor = "COMMISSION_AUTHOR";
  protected const string VariablePlannedData = "PLANNED_DATA";
  protected const string VariableControlUser = "CONTROL_USER";
  protected const string VariableExecutionOrder = "EXECUTION_ORDER";

  protected ResolutionProcess([NotNull] string name, bool controlResolution)
  {
    this._Name = name;
    this._Control = controlResolution;
    this._addAttributes = new IAdditionalActivitiesAttributes[2]
    {
      (IAdditionalActivitiesAttributes) new IsControlAdditionalAttribute(controlResolution),
      (IAdditionalActivitiesAttributes) new PrivateRegNumAdditionalAttribute()
    };
  }

  public void Execute(
    IUserSession session,
    [NotNull] IDBObject resolution,
    [NotNull] ResolutionProcessExecuteArgs args)
  {
    IProcess process = this.CreateProcess(session, resolution.ObjectID, args);
    this.OnExecute(session, resolution, process, args.ExecutorIDs);
    process.StartProcess();
  }

  protected abstract void OnExecute(
    [NotNull] IUserSession session,
    [NotNull] IDBObject resolution,
    [NotNull] IProcess process,
    [NotNull] IList<long> executorIDs);

  protected abstract void Initialize([NotNull] OrderProcessTemplates processTemplates);

  [CanBeNull]
  public static IResolutionProcess GetProcess(
    ResolutionExecution execType,
    [NotNull] OrderProcessTemplates processTemplates,
    [NotNull] string name,
    bool controlResolution)
  {
    ResolutionProcess process = (ResolutionProcess) null;
    switch (execType)
    {
      case ResolutionExecution.Parallel:
        process = (ResolutionProcess) new ParallelResolutionProcess(name, controlResolution);
        break;
      case ResolutionExecution.Successive:
      case ResolutionExecution.Combined:
        process = (ResolutionProcess) new SuccessiveResolutionProcess(name, controlResolution);
        break;
    }
    process?.Initialize(processTemplates);
    return (IResolutionProcess) process;
  }

  internal static long GetTemplateIDofNonDocumentResolution(
    [NotNull] OfficeGeneralSettings officeGeneralSettings,
    ResolutionExecution execType,
    bool controlResolution)
  {
    switch (execType)
    {
      case ResolutionExecution.Parallel:
        return !controlResolution ? officeGeneralSettings.ParallelNonControlResolutionTemplateID : officeGeneralSettings.ParallelControlResolutionTemplateID;
      case ResolutionExecution.Successive:
      case ResolutionExecution.Combined:
        return !controlResolution ? officeGeneralSettings.ConsistentNonControlResolutionTemplateID : officeGeneralSettings.ConsistentControlResolutionTemplateID;
      default:
        throw new Exception("Неизвестный тип исполнения поручения");
    }
  }

  [CanBeNull]
  public static IResolutionProcess GetNonDocumentProcess(
    ResolutionExecution execType,
    Guid templateGuid,
    [NotNull] string name,
    bool controlResolution)
  {
    ResolutionProcess nonDocumentProcess = (ResolutionProcess) null;
    switch (execType)
    {
      case ResolutionExecution.Parallel:
        nonDocumentProcess = (ResolutionProcess) new ParallelResolutionProcess(name, controlResolution);
        break;
      case ResolutionExecution.Successive:
      case ResolutionExecution.Combined:
        nonDocumentProcess = (ResolutionProcess) new SuccessiveResolutionProcess(name, controlResolution);
        break;
    }
    if (nonDocumentProcess != null)
      nonDocumentProcess._ProcessTemplate = templateGuid;
    return (IResolutionProcess) nonDocumentProcess;
  }

  [NotNull]
  protected IProcess CreateProcess(
    [NotNull] IUserSession session,
    long resolutionID,
    [NotNull] ResolutionProcessExecuteArgs args)
  {
    QuickObjectInfo objectInfo = session.GetObjectInfo(this._ProcessTemplate);
    if (objectInfo.Empty)
      throw new Exception(Localization.GetString("Office.Server_7", (object) this._ProcessTemplate));
    IProcess process = session.GetCustomService<IRouterService>().CreateProcess(session.SessionGUID, objectInfo.ObjectID);
    process.Name = this._Name;
    process.Attributes.AddAttribute(OfficeConsts.AttrResolutionIdentityID, false, new object[1]
    {
      (object) Math.Abs(resolutionID)
    });
    if (process.StartActivity == null)
      throw new Exception("Start activity not found!");
    if (args.OfficeDocID != 0L)
      process.StartActivity.Attachments.Add(args.OfficeDocID);
    IVariable variable1 = process.StartActivity.Variables.Find("COMMISSION_AUTHOR");
    if (variable1 == null)
      throw new VariableMissingException("COMMISSION_AUTHOR");
    ParticipantList participantList1 = new ParticipantList(session);
    participantList1.AddParticipant(ParticipantKind.User, session.UserID);
    variable1.Value = participantList1.AsString;
    if (args.PlannedDate != DateTime.MinValue)
    {
      IVariable variable2 = process.StartActivity.Variables.Find("PLANNED_DATA");
      if (variable2 == null)
        throw new VariableMissingException("PLANNED_DATA");
      variable2.TypedValue = (object) args.PlannedDate;
    }
    if (this._Control)
    {
      IVariable variable3 = process.StartActivity.Variables.Find("CONTROL_USER");
      if (variable3 == null)
        throw new VariableMissingException("CONTROL_USER");
      ParticipantList participantList2 = new ParticipantList(session);
      participantList2.AddParticipant(ParticipantKind.User, args.ControlUserID);
      variable3.Value = participantList2.AsString;
    }
    foreach (IAdditionalActivitiesAttributes addAttribute in this._addAttributes)
      addAttribute.CreateAttributes(session, process, args);
    return process;
  }
}
