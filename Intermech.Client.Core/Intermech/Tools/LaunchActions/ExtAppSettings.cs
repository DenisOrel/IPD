
// Type: Intermech.Tools.LaunchActions.ExtAppSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;
using System;
using System.Diagnostics;


namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Содержит настройки команды запуска для приложения операционной системы.
/// </summary>
public sealed class ExtAppSettings : ISettingsObject
{
  /// <summary>
  /// Возвращает глобальный идентификатор обработчика команда запуска приложений операционной системы.
  /// </summary>
  public static readonly Guid HandlerId = new Guid("E79383A3-7379-47DE-A492-294A67BE489B");
  private string appName;
  private string executable;
  private string workDirectory;
  private string arguments;
  private ProcessWindowStyle windowStyle;

  /// <summary>
  /// Возвращает или задает название приложения, отображаемое пользователю. Это свойство не должно содержать
  /// пустых значений.
  /// </summary>
  public string ApplicationName
  {
    get => this.appName;
    set => this.appName = value;
  }

  /// <summary>
  /// Возвращает или задает путь к исполняемому файлу приложения. Это свойство не должно содержать
  /// пустых значений.
  /// </summary>
  public string Executable
  {
    get => this.executable;
    set => this.executable = value;
  }

  /// <summary>
  /// Возвращает или задает путь к рабочему каталогу запускаемого приложения. Это свойство может
  /// быть не задано.
  /// </summary>
  public string WorkDirectory
  {
    get => this.workDirectory;
    set => this.workDirectory = value;
  }

  /// <summary>
  /// Возвращает или задает аргументы, передаваемые приложению при запуске. Это свойство не должно
  /// содержать пустых значений, т.к. приложение не сможет получить путь к файлу документа. Можно
  /// использовать следующие шаблоны подстановки - !.!, !, ?.? и ?.
  /// </summary>
  public string Arguments
  {
    get => this.arguments;
    set => this.arguments = value;
  }

  /// <summary>
  /// Возвращает или задает вид окна запускаемого приложения.
  /// </summary>
  public ProcessWindowStyle WindowStyle
  {
    get => this.windowStyle;
    set => this.windowStyle = value;
  }
}
