
// Type: Intermech.Scripting.Projects.DBScripts.DBScriptDisplayBehavior
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Scripting.Common.DesignTime;


namespace Intermech.Scripting.Projects.DBScripts;

/// <summary>
/// Класс поведения сценариев IPS во время отображения в IDE.
/// Реализация не является thread safe.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="scriptProject">Проект сценария</param>
/// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null</exception>
public sealed class DBScriptDisplayBehavior(DBScriptProject scriptProject) : 
  DBScriptBehavior(scriptProject),
  IScriptDisplayBehavior
{
  /// <summary>Возвращает имя сценария для отображения в окнах IDE.</summary>
  /// <returns>Имя сценария для отображения в окнах IDE</returns>
  public string GetDisplayName()
  {
    return !Consts.IsUndefinedObjectId(this.ScriptProject.ObjectId) ? $"{this.ScriptProject.Name} (ID: {this.ScriptProject.ObjectId})" : this.ScriptProject.Name;
  }
}
