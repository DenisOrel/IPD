
// Type: Intermech.Scripting.Projects.DBScripts.DBScriptDebugBehavior
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Mvp;
using Intermech.Mvp.Components.Dialogs;
using Intermech.Scripting.Common.Debugging;
using Intermech.Scripting.Common.DesignTime;
using Intermech.Scripting.CSharp.Debugging;
using Intermech.Scripting.CSharp.DesignTime;
using System;
using System.Collections.Generic;


namespace Intermech.Scripting.Projects.DBScripts;

/// <summary>
/// Базовый класс поведения сценариев IPS во время отладки в IDE.
/// Реализация не является thread safe.
/// </summary>
public abstract class DBScriptDebugBehavior : DBScriptBehavior, IScriptDebugBehavior
{
  /// <summary>Создает объект.</summary>
  /// <param name="scriptProject">Проект сценария</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null</exception>
  public DBScriptDebugBehavior(DBScriptProject scriptProject)
    : base(scriptProject)
  {
  }

  /// <summary>
  /// Позволяет изменить аргументы сценария, передаваемые ему во время выполнения.
  /// </summary>
  public virtual void EditArguments()
  {
    MvpContext.ViewService.ShowModal((IPresenter) new SimpleMessagePresenter("У сценария нет параметров запуска, доступных для редактирования.", "Сообщение", MessageIcon.Information));
  }

  /// <summary>Выполняет сценарий.</summary>
  /// <param name="languageSession">Языковая сессия исполнителя</param>
  /// <param name="scriptCode">Код сценария</param>
  /// <returns>Результат выполнения сценария</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="languageSession" /> не должен быть равен null; параметр <paramref name="scriptCode" /> не должен быть равен null</exception>
  public ScriptDebugInvocationResult Execute(ILanguageSession languageSession, string scriptCode)
  {
    if (scriptCode == null)
      throw new ArgumentNullException(nameof (scriptCode));
    if (languageSession == null)
      throw new ArgumentNullException(nameof (languageSession));
    ScriptDebugInvocationParameters invocationParameters = this.DoCreateInvocationParameters();
    return this.DoExecute(languageSession, scriptCode, invocationParameters);
  }

  /// <summary>
  /// Создает параметры выполнения сценария для языковой сессии исполнителя сценариев.
  /// </summary>
  /// <returns>Параметры выполнения сценария</returns>
  protected virtual ScriptDebugInvocationParameters DoCreateInvocationParameters()
  {
    IScriptProjectOptionsBehavior projectOptionsBehavior = this.ScriptProject.Behaviors.GetProjectOptionsBehavior(false);
    Dictionary<string, string> dictionary = projectOptionsBehavior != null ? projectOptionsBehavior.GetProjectOptions() : new Dictionary<string, string>(0);
    return new ScriptDebugInvocationParameters()
    {
      ProjectOptions = dictionary
    };
  }

  /// <summary>Выполняет сценарий.</summary>
  /// <param name="languageSession">Языковая сессия исполнителя</param>
  /// <param name="scriptCode">Код сценария</param>
  /// <param name="invocationParameters">Параметры выполнения сценария</param>
  /// <returns>Результат выполнения сценария</returns>
  protected abstract ScriptDebugInvocationResult DoExecute(
    ILanguageSession languageSession,
    string scriptCode,
    ScriptDebugInvocationParameters invocationParameters);

  /// <summary>
  /// Создает и возвращает специальную сессию сервера приложений для режима отладки сценариев.
  /// </summary>
  /// <param name="invocationParameters">Параметры выполнения сценария</param>
  /// <returns>Сессия сервера приложений</returns>
  protected (IUserSession, string) CreateDebugSystemSession(
    ScriptDebugInvocationParameters invocationParameters)
  {
    if (invocationParameters == null)
      throw new ArgumentNullException(nameof (invocationParameters));
    if (this.ScriptProject.LanguageInfo.Name == "C#" && CSharpScriptProjectOptions.FromDictionary(invocationParameters.ProjectOptions).RunAtClientSide)
      throw new NotSupportedException("Выполнение сценариев от имени системной сессии сервера приложений поддерживается только для серверных сценариев.");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Tuple<IUserSession, string> debugSystemSession = ((ICSharpDebugExecutor) sessionKeeper.Session.GetCustomService(typeof (ICSharpScriptExecutor))).CreateDebugSystemSession(ClientTokenProvider.Default.GetClientToken());
      return (debugSystemSession.Item1, debugSystemSession.Item2);
    }
  }
}
