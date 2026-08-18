// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ScriptPad.WorkflowScriptDebugBehavior
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Scripting.Common.DesignTime;
using Intermech.Scripting.Projects.DBScripts;
using System;

#nullable disable
namespace Intermech.Workflow.Design.ScriptPad;

/// <summary>
/// Класс поведения сценариев Workflow во время отладки в IDE.
/// Реализация не является thread safe.
/// </summary>
internal sealed class WorkflowScriptDebugBehavior : DBScriptDebugBehavior
{
  private long activityId;

  /// <summary>Создает объект.</summary>
  /// <param name="scriptProject">Проект сценария</param>
  /// <param name="activityId">Аргументы для запуска сценария</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null</exception>
  public WorkflowScriptDebugBehavior(DBScriptProject scriptProject, long activityId)
    : base(scriptProject)
  {
    this.activityId = !Consts.IsUndefinedObjectId(activityId) ? activityId : throw new ArgumentException("Не задан идентификатор версии объекта.", nameof (activityId));
  }

  /// <summary>Выполняет сценарий.</summary>
  /// <param name="languageSession">Языковая сессия исполнителя</param>
  /// <param name="scriptCode">Код сценария</param>
  /// <param name="invocationParameters">Параметры выполнения сценария</param>
  /// <returns>Результат выполнения сценария</returns>
  protected override ScriptDebugInvocationResult DoExecute(
    ILanguageSession languageSession,
    string scriptCode,
    ScriptDebugInvocationParameters invocationParameters)
  {
    (IUserSession session, string sessionName) = this.CreateDebugUserSession(invocationParameters);
    try
    {
      bool oldStateOfTransaction = MiscFunx.CheckForActiveTransaction(session);
      IActivity activity = (IActivity) session.GetObject(this.activityId, true);
      activity.Flags |= ActivityFlags.InheritVars;
      invocationParameters.Arguments.Add((object) activity);
      ScriptDebugInvocationResult invocationResult = languageSession.Execute(scriptCode, invocationParameters);
      MiscFunx.CheckForActiveTransaction(session, activity, "[STR2]", oldStateOfTransaction);
      return invocationResult;
    }
    finally
    {
      session.Logout(sessionName);
    }
  }

  private (IUserSession, string) CreateDebugUserSession(
    ScriptDebugInvocationParameters invocationParameters)
  {
    if (!this.ScriptProject.RunAtClientSide)
      return this.CreateDebugSystemSession(invocationParameters);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string sessionName = "WorkflowDebugSession";
      return (sessionKeeper.Session.Clone(sessionName), sessionName);
    }
  }
}
