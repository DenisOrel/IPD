
// Type: Intermech.Scripting.Services.DBScriptProjectInitializer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Scripting.Projects.DBScripts;
using System;
using System.Diagnostics;


namespace Intermech.Scripting.Services;

/// <summary>
/// Базовый класс для инициализации типов сценариев IPS.
/// Реализация является thread safe.
/// </summary>
public class DBScriptProjectInitializer
{
  private string nameTemplate;
  private string scriptCodeTemplate;

  /// <summary>Создает объект.</summary>
  /// <param name="scriptCodeTemplate">Шаблон кода для новых сценариев</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptCodeTemplate" /> не должен быть равен null</exception>
  public DBScriptProjectInitializer(string scriptCodeTemplate)
  {
    if (scriptCodeTemplate == null)
      throw new ArgumentNullException(nameof (scriptCodeTemplate));
    this.nameTemplate = "Новый сценарий";
    this.scriptCodeTemplate = scriptCodeTemplate;
  }

  /// <summary>Возвращает шаблон имени для новых сценариев.</summary>
  public string NameTemplate
  {
    [DebuggerStepThrough] get => this.nameTemplate;
  }

  /// <summary>Возвращает шаблон код для новых сценариев.</summary>
  public string ScriptCodeTemplate
  {
    [DebuggerStepThrough] get => this.scriptCodeTemplate;
  }

  /// <summary>
  /// Выполняет инициализацию сценарного проекта.
  /// Метод вызывается как для новых проектов, так и для загруженных из базы данных.
  /// </summary>
  /// <param name="scriptProject">Сценарный проект</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null</exception>
  public void Initialize(DBScriptProject scriptProject)
  {
    if (scriptProject == null)
      throw new ArgumentNullException(nameof (scriptProject));
    this.DoInitialize(scriptProject);
  }

  /// <summary>
  /// Выполняет инициализацию сценарного проекта.
  /// Метод вызывается как для новых проектов, так и для загруженных из базы данных.
  /// </summary>
  /// <param name="scriptProject">Сценарный проект</param>
  protected virtual void DoInitialize(DBScriptProject scriptProject)
  {
  }
}
