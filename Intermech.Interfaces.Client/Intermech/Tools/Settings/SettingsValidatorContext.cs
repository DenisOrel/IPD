// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Settings.SettingsValidatorContext
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Settings;

/// <summary>
/// Задает контекст, в рамках которого выполняется проверка настроек. Использование специальных контекстов позволяет
/// ограничить набор используемых проверок.
/// </summary>
public enum SettingsValidatorContext
{
  /// <summary>
  /// Общий контекст. Валидатор должен выполнить все возможные проверки, включая проверки для настроек системы, не хранящихся непосредственно
  /// в объекте настроек.
  /// </summary>
  Generic,
  /// <summary>
  /// Контекст редактора настроек. Валидатор должен выполнить только те проверки, которые проверяют настройки, хранящиеся непосредственно в объекте настроек.
  /// </summary>
  SettingsObjectOnly,
}
