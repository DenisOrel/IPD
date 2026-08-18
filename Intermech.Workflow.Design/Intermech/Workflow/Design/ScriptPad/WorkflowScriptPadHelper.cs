// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ScriptPad.WorkflowScriptPadHelper
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core;
using Intermech.Commands;
using Intermech.Scripting.Common.DesignTime;
using Intermech.Scripting.Projects.DBScripts;
using Intermech.Scripting.Services;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ScriptPad;

/// <summary>Класс для интеграции сценариев Worflow с IDE.</summary>
internal class WorkflowScriptPadHelper : ScriptPadHelper
{
  private Form ownerForm;

  /// <summary>Создает объект.</summary>
  /// <param name="scriptType">Тип сценариев</param>
  public WorkflowScriptPadHelper(ScriptTypes scriptType = ScriptTypes.Workflow, Form ownerForm = null)
    : base(scriptType)
  {
    this.ownerForm = ownerForm;
  }

  /// <summary>Обеспечивает интеграцию Workflow c IDE.</summary>
  /// <param name="scriptId">Идентификатор версии объекта сценария, может быть не задан</param>
  /// <param name="workflowLocalName">Имя сценария для локальных сценариев Workflow или пусто для всех остальных</param>
  /// <param name="workflowExecSide">Сторона выполнения сценария</param>
  /// <param name="readOnlyMode">Признак режима только чтение</param>
  /// <param name="activityID">Аргументы для запуска сценария в режиме отладки</param>
  /// <returns>Идентификатор версии объекта сценария</returns>
  public long EditScript(
    long scriptId,
    string workflowLocalName,
    ScriptExecSide workflowExecSide,
    bool readOnlyMode,
    long activityID)
  {
    if (workflowLocalName == null)
      throw new ArgumentNullException(nameof (workflowLocalName));
    if (Consts.IsUndefinedObjectId(activityID))
      throw new ArgumentException("Не задан идентификатор версии объекта.", nameof (activityID));
    DBScriptProject scriptProject = Consts.IsUndefinedObjectId(scriptId) ? this.IDEService.CreateEmptyScriptProject(ScriptTypeHelper.GetObjType4ScriptType(this.ScriptType)) : this.IDEService.GetScriptProject(readOnlyMode ? scriptId : this.AutoCheckout(scriptId));
    if (scriptProject.IsNew && !string.IsNullOrEmpty(workflowLocalName))
      scriptProject.Name = workflowLocalName;
    scriptProject.RunAtClientSide = workflowExecSide == ScriptExecSide.Client;
    this.SetupScriptProjectBehaviors(scriptProject, readOnlyMode, activityID);
    IScriptPadService ideService = this.IDEService;
    DBScriptProject dbScriptProject1 = scriptProject;
    OpenInScriptPadParameters parameters = new OpenInScriptPadParameters();
    parameters.ReadOnlyMode = readOnlyMode;
    Form ownerForm = this.ownerForm;
    DBScriptProject dbScriptProject2 = (DBScriptProject) ideService.OpenScriptInDialogMode((ScriptProject) dbScriptProject1, parameters, ownerForm);
    if (dbScriptProject2 == null)
      return 0;
    return !readOnlyMode ? this.AutoCheckin(dbScriptProject2.ObjectId) : dbScriptProject2.ObjectId;
  }

  private void SetupScriptProjectBehaviors(
    DBScriptProject scriptProject,
    bool readOnlyMode,
    long activityID)
  {
    scriptProject.Behaviors.AddDebugBehavior((IScriptDebugBehavior) new WorkflowScriptDebugBehavior(scriptProject, activityID));
    if (readOnlyMode)
      return;
    scriptProject.Behaviors.AddSaveChangesBehavior((IScriptSaveChangesBehavior) new WorkflowScriptSaveChangesBehavior(scriptProject));
    if (this.ScriptType != ScriptTypes.WorkflowCommon)
      return;
    WorkflowScriptReplacementBehavior behavior = new WorkflowScriptReplacementBehavior(scriptProject, (Func<long, DBScriptProject>) (anotherScriptId => this.IDEService.GetScriptProject(this.AutoCheckout(anotherScriptId))), (Action<DBScriptProject>) (currentScriptProject => this.AutoCheckin(currentScriptProject.ObjectId)), (Action<DBScriptProject>) (anotherScriptProject => this.SetupScriptProjectBehaviors(anotherScriptProject, readOnlyMode, activityID)));
    scriptProject.Behaviors.AddReplacementBehavior((IScriptReplacementBehavior) behavior);
  }

  private long AutoCheckout(long scriptId)
  {
    if (Consts.IsUndefinedObjectId(scriptId) || scriptId <= 0L)
      return scriptId;
    ObjectCopyCommand checkoutCommand = ObjectCommandFactory.CreateCheckoutCommand(true);
    checkoutCommand.ObjectId = scriptId;
    checkoutCommand.Execute();
    return checkoutCommand.NewObjectId;
  }

  private long AutoCheckin(long scriptId)
  {
    if (Consts.IsUndefinedObjectId(scriptId) || scriptId >= 0L)
      return scriptId;
    ObjectCopyCommand checkinCommand = ObjectCommandFactory.CreateCheckinCommand(true);
    checkinCommand.ObjectId = scriptId;
    checkinCommand.Execute();
    return checkinCommand.NewObjectId;
  }
}
