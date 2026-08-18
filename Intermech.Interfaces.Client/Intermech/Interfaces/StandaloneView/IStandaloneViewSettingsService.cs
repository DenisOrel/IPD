// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.StandaloneView.IStandaloneViewSettingsService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.StandaloneView;

/// <summary>
/// Клиентский сервис для доступа к настройкам типов объектов для режима автономного просмотра.
/// </summary>
public interface IStandaloneViewSettingsService
{
  /// <summary>
  /// Возвращает настройки для указанного типа объектов с учетом настроек базовых типов объектов.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <returns>Объект с настройками</returns>
  StandaloneViewObjectTypeSettings GetEffectiveSettings(int objectType);

  /// <summary>
  /// Загружает собственные настройки для указанного типа объектов, при этом настройки для базовых типов объектов не используются.
  /// Метод используется редактором настроек.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  /// <returns>Объект с настройками или null</returns>
  /// <exception cref="T:ArgumentException">objectType</exception>
  StandaloneViewObjectTypeSettings TryLoadSettings(int objectType);

  /// <summary>
  /// Сохраняет собственные настройки для указанного типа объектов.
  /// Метод используется редактором настроек.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  /// <param name="settings">Объект с настройками</param>
  /// <exception cref="T:ArgumentException">objectType</exception>
  /// <exception cref="T:ArgumentNullException">settings</exception>
  void SaveSettings(int objectType, StandaloneViewObjectTypeSettings settings);

  /// <summary>
  /// Удаляет собственные настройки для указанного типа объектов.
  /// Метод используется редактором настроек.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  void RemoveSettings(int objectType);
}
