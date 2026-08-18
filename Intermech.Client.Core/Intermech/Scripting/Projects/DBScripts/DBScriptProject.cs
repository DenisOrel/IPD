
// Type: Intermech.Scripting.Projects.DBScripts.DBScriptProject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Scripting.Common.DesignTime;
using System.Diagnostics;


namespace Intermech.Scripting.Projects.DBScripts;

/// <summary>
/// Класс проектов IDE для сценариев, хранящихся в базе данных IPS.
/// Реализация не является thread safe.
/// </summary>
/// <remarks>
/// Сценарные проекты являются аналогом .csproj-файлов в Visual Studio.
/// Каждый проект хранит код и свойства одного сценария, задающие способ выполнения и отладки этого сценария.
/// </remarks>
public class DBScriptProject : ScriptProject
{
  /// <summary>Создает проект.</summary>
  /// <param name="languageInfo">Язык сценария</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="languageInfo" /> не должен быть равен null</exception>
  public DBScriptProject(LanguageInfo languageInfo)
    : base(languageInfo)
  {
    this.ObjectTypeId = -1;
    this.RunAtClientSide = true;
  }

  /// <summary>
  /// Возвращает идентификатор версии сценария в базе данных.
  /// </summary>
  public long ObjectId
  {
    [DebuggerStepThrough] get
    {
      return !this.IsNew ? ((DBScriptRepositoryKey) this.RepositoryKey).ObjectId : 0L;
    }
  }

  /// <summary>
  /// Возвращает или задает идентификатор типа сценария в базе данных.
  /// </summary>
  public int ObjectTypeId { get; set; }

  /// <summary>
  /// Возвращает или задает признак выполнения сценария на клиентской или серверной стороне.
  /// По умолчанию все сценарии выполняются на клиентской стороне.
  /// </summary>
  public bool RunAtClientSide { get; set; }
}
