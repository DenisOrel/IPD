
// Type: Intermech.Scripting.Projects.DBScripts.DBScriptBehavior
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;


namespace Intermech.Scripting.Projects.DBScripts;

/// <summary>Базовый класс для всех поведений сценариев IPS.</summary>
public abstract class DBScriptBehavior
{
  private DBScriptProject scriptProject;

  /// <summary>Создает объект.</summary>
  /// <param name="scriptProject">Проект сценария</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null</exception>
  protected DBScriptBehavior(DBScriptProject scriptProject)
  {
    this.scriptProject = scriptProject != null ? scriptProject : throw new ArgumentNullException(nameof (scriptProject));
  }

  /// <summary>Возвращает проект сценария.</summary>
  protected DBScriptProject ScriptProject
  {
    [DebuggerStepThrough] get => this.scriptProject;
  }
}
