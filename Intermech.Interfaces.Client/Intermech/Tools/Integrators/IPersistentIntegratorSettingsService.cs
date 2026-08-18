// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IPersistentIntegratorSettingsService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Tools.Settings;
using System.Xml;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Интерфейс расширения для сервиса настроек интегратора, предоставляющий методы для сохранения/восстановления настроек интегратора из некоторого контейнера.
/// </summary>
public interface IPersistentIntegratorSettingsService : 
  IIntegratorSettingsService,
  IIntegratorService
{
  /// <summary>
  /// Выполняет преобразование объекта с настройками в xml-документ.
  /// </summary>
  /// <param name="settingsObject">Объект с настройками</param>
  /// <returns>Настройки в форме xml-документа</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на объект с настройками не может быть null</exception>
  XmlDocument EncodeSettings(ISettingsObject settingsObject);

  /// <summary>
  /// Выполняет преобразование xml-документа в объект с настройками.
  /// </summary>
  /// <param name="settingsXml">Настройки в форме xml-документа</param>
  /// <returns>Объект с настройками</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на xml-документ не могжет быть null</exception>
  /// <exception cref="T:System.NotSupportedException">Неизвестная версия формата xml-документа</exception>
  ISettingsObject DecodeSettings(XmlDocument settingsXml);

  /// <summary>Выполняет проверку корректности настроек.</summary>
  /// <param name="settingsObject">Объект с настройками</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект с настройками не может быть null</exception>
  /// <exception cref="T:System.Exception">Настройки содержат ошибку</exception>
  void ValidateSettings(ISettingsObject settingsObject, SettingsValidatorContext context);
}
