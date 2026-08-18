// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ScriptPad.WorkflowScriptReplacementBehavior
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Scripting.Common.DesignTime;
using Intermech.Scripting.Projects.DBScripts;
using System;

#nullable disable
namespace Intermech.Workflow.Design.ScriptPad;

/// <summary>
/// Класс поведения сценариев Workflow во время выполнения команды "Заменить" другим сценарием.
/// Это исключительная особенность Workflow, используемая только в диалоговом режиме IDE.
/// Реализация не является thread safe.
/// </summary>
internal sealed class WorkflowScriptReplacementBehavior : 
  DBScriptBehavior,
  IScriptReplacementBehavior
{
  private Func<long, DBScriptProject> openScriptAction;
  private Action<DBScriptProject> closeScriptAction;
  private Action<DBScriptProject> customizeScriptAction;

  /// <summary>Создает объект.</summary>
  /// <param name="scriptProject">Проект сценария</param>
  /// <param name="openScriptAction">Обработчик открытия проекта сценария</param>
  /// <param name="closeScriptAction">Обработчик закрытия проекта сценария</param>
  /// <param name="customizeScriptAction">Обработчик для тонкой настройки проекта сценария, может быть не задан</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null; параметр <paramref name="openScriptAction" /> не должен быть равен null; параметр <paramref name="closeScriptAction" /> не должен быть равен null</exception>
  public WorkflowScriptReplacementBehavior(
    DBScriptProject scriptProject,
    Func<long, DBScriptProject> openScriptAction,
    Action<DBScriptProject> closeScriptAction,
    Action<DBScriptProject> customizeScriptAction = null)
    : base(scriptProject)
  {
    if (openScriptAction == null)
      throw new ArgumentNullException(nameof (openScriptAction));
    if (closeScriptAction == null)
      throw new ArgumentNullException(nameof (closeScriptAction));
    this.openScriptAction = openScriptAction;
    this.closeScriptAction = closeScriptAction;
    this.customizeScriptAction = customizeScriptAction;
  }

  /// <summary>
  /// Выбирает из хранилища другой сценарий для замены текущего сценария в IDE.
  /// </summary>
  /// <returns>Проект сценария или null</returns>
  public Intermech.Scripting.Common.DesignTime.ScriptProject TryGetAnotherScriptProject()
  {
    long[] numArray = SelectionWindow.SelectObjects("Выбор сценария", "Выберите сценарий для замены текущего сценария, открытого в IDE", this.ScriptProject.ObjectTypeId, SelectionOptions.Default);
    return numArray != null && numArray.Length != 0 ? this.GetAnotherScriptProject(numArray[0]) : (Intermech.Scripting.Common.DesignTime.ScriptProject) null;
  }

  private Intermech.Scripting.Common.DesignTime.ScriptProject GetAnotherScriptProject(
    long anotherScriptId)
  {
    DBScriptProject anotherScriptProject = this.openScriptAction(anotherScriptId);
    if (this.customizeScriptAction != null)
      this.customizeScriptAction(anotherScriptProject);
    return (Intermech.Scripting.Common.DesignTime.ScriptProject) anotherScriptProject;
  }

  /// <summary>
  /// Обработчик события, вызывающийся после успешной замены текущего сценария в IDE.
  /// </summary>
  /// <param name="anotherScriptProject">Сценарный проект, на который была выполнена замена</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="anotherScriptProject" /> не должен быть равен null</exception>
  public void AfterReplace(Intermech.Scripting.Common.DesignTime.ScriptProject anotherScriptProject)
  {
    if (anotherScriptProject == null)
      throw new ArgumentNullException(nameof (anotherScriptProject));
    this.closeScriptAction(this.ScriptProject);
  }
}
