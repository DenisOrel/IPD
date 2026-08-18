
// Type: Intermech.Scripting.Projects.DBScripts.DBScriptProjectOptionsBehavior
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Scripting.Common.DesignTime;
using Intermech.Scripting.CSharp.DesignTime;
using System;
using System.Collections.Generic;


namespace Intermech.Scripting.Projects.DBScripts;

/// <summary>
/// Класс поведения сценариев во время разбора исходного текста, компиляции, выполнения и отладки.
/// Реализация не является thread safe.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="scriptProject">Проект сценария</param>
/// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null</exception>
public class DBScriptProjectOptionsBehavior(DBScriptProject scriptProject) : 
  DBScriptBehavior(scriptProject),
  IScriptProjectOptionsBehavior
{
  /// <summary>
  /// Возвращает опции сценария, которые могут включать опции языка и опции среды выполнения
  /// </summary>
  /// <returns>Опции сценария</returns>
  public Dictionary<string, string> GetProjectOptions() => this.DoGetProjectOptions();

  protected virtual Dictionary<string, string> DoGetProjectOptions()
  {
    if (this.ScriptProject.LanguageInfo.Name == "C#")
      return CSharpScriptProjectOptions.ToDictionary(new CSharpScriptProjectOptions()
      {
        RunAtClientSide = this.ScriptProject.RunAtClientSide
      });
    if (!this.ScriptProject.RunAtClientSide)
      throw new NotSupportedException("Выполнение сценариев на сервере приложений поддерживается только для C#-сценариев.");
    return new Dictionary<string, string>(0);
  }
}
