
// Type: Intermech.Tools.LaunchActions.ShellVerbSettingsValidator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Tools.Settings;
using System;


namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Реализует валидатор настроек для команды запуска приложения операционной системы средствами shell verb.
/// </summary>
public sealed class ShellVerbSettingsValidator : LaunchActionSettingsValidator
{
  /// <summary>
  /// Проверяет корректность объекта, содержащего настройки запуска приложения. В случае наличия
  /// ошибок метод возвращает текст с описанием ошибки.
  /// </summary>
  /// <param name="settingsObject">Объект с настройками</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <returns>Текст ошибки или null</returns>
  protected override string DoValidate(
    ISettingsObject settingsObject,
    SettingsValidatorContext context)
  {
    if (settingsObject == null)
      throw new ArgumentNullException(nameof (settingsObject));
    return string.IsNullOrEmpty(((ShellVerbSettings) settingsObject).Verb) ? LocalizationHolder.rm.GetString("Interfaces_701") : base.DoValidate(settingsObject, context);
  }
}
