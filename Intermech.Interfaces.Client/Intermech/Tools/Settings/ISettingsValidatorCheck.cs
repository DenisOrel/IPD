// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Settings.ISettingsValidatorCheck
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Settings;

/// <summary>
/// Позволяет реализовать проверку некоторой части настроек интегратора.
/// </summary>
public interface ISettingsValidatorCheck
{
  /// <summary>Выполняет проверку настроек интегратора.</summary>
  /// <param name="settingsObject">Объект с настройками интегратора</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <returns>null, если проверка успешно пройдена, иначе - текст с детальным описанием проблемы</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект настроек не может быть null</exception>
  string PerformCheck(ISettingsObject settingsObject, SettingsValidatorContext context);
}
