
// Type: Intermech.Scripting.Projects.DBScripts.DBScriptSaveAsParameters
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Scripting.Common.DesignTime;
using System;
using System.Diagnostics;


namespace Intermech.Scripting.Projects.DBScripts;

/// <summary>
/// Параметры сохранения сценария IPS под новым именем. Используется при сохрании и новых сценариев, и
/// существующих сценариев под новым именем.
/// </summary>
public class DBScriptSaveAsParameters : ScriptSaveAsParameters
{
  private string name;

  /// <summary>Создает объект.</summary>
  /// <param name="name">Имя сценария при сохранении в базу данных</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="name" /> не должен быть равен null</exception>
  public DBScriptSaveAsParameters(string name)
  {
    this.name = name != null ? name : throw new ArgumentNullException(nameof (name));
  }

  /// <summary>Возвращает имя сценария при сохранении в базу данных.</summary>
  public string Name
  {
    [DebuggerStepThrough] get => this.name;
  }
}
