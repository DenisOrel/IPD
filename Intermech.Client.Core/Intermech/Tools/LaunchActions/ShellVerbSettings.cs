
// Type: Intermech.Tools.LaunchActions.ShellVerbSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;
using System;


namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Содержит настройки команды запуска для приложения операционной системы средствами shell verb.
/// </summary>
public sealed class ShellVerbSettings : ISettingsObject
{
  /// <summary>
  /// Возвращает глобальный идентификатор обработчика команда запуска приложений операционной системы
  /// средствами shell verb.
  /// </summary>
  public static readonly Guid HandlerId = new Guid("E3ABC902-A841-4B92-989C-83D0C397CC2C");
  private string verb;

  /// <summary>
  /// Возвращает или задает shell verb, используемый для запуска приложения. Это свойство не должно
  /// содержать пустых значений.
  /// </summary>
  public string Verb
  {
    get => this.verb;
    set => this.verb = value;
  }
}
