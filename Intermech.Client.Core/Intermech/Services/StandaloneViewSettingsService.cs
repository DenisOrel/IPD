
// Type: Intermech.Services.StandaloneViewSettingsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Cache;
using Intermech.Interfaces;
using Intermech.Interfaces.StandaloneView;
using System;


namespace Intermech.Services;

/// <summary>
/// Клиентский сервис для доступа к настройкам типов объектов для режима автономного просмотра.
/// Реализация является thread safe.
/// </summary>
internal sealed class StandaloneViewSettingsService : IStandaloneViewSettingsService
{
  private WriteSeqKeyValueCache<int, StandaloneViewObjectTypeSettings> effectiveSettingsCache;
  private object cacheSyncRoot;

  /// <summary>Создает объект.</summary>
  public StandaloneViewSettingsService()
  {
    this.effectiveSettingsCache = new WriteSeqKeyValueCache<int, StandaloneViewObjectTypeSettings>(TimeSpan.FromMinutes(10.0), new Func<int, StandaloneViewObjectTypeSettings>(this.GetEffectiveSettingsFromServer), new Func<long>(this.GetEffectiveSettingsWriteSeq));
    this.cacheSyncRoot = new object();
  }

  private void ClearCache()
  {
    lock (this.cacheSyncRoot)
      this.effectiveSettingsCache.Clear();
  }

  /// <summary>
  /// Возвращает настройки для указанного типа объектов с учетом настроек базовых типов объектов.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <returns>Объект с настройками</returns>
  public StandaloneViewObjectTypeSettings GetEffectiveSettings(int objectType)
  {
    if (objectType == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов.", nameof (objectType));
    lock (this.cacheSyncRoot)
      return this.effectiveSettingsCache.GetValue(objectType);
  }

  private StandaloneViewObjectTypeSettings GetEffectiveSettingsFromServer(int objectType)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IStandaloneViewServerService>((object) sessionKeeper.Session, true).GetEffectiveSettings(sessionKeeper.Session.SessionGUID, objectType);
  }

  private long GetEffectiveSettingsWriteSeq()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IStandaloneViewServerService>((object) sessionKeeper.Session, true).GetWriteSequence(sessionKeeper.Session.SessionGUID);
  }

  /// <summary>
  /// Загружает собственные настройки для указанного типа объектов, при этом настройки для базовых типов объектов не используются.
  /// Метод используется редактором настроек.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  /// <returns>Объект с настройками или null</returns>
  /// <exception cref="T:ArgumentException">objectType</exception>
  public StandaloneViewObjectTypeSettings TryLoadSettings(int objectType)
  {
    if (objectType == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов.", nameof (objectType));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IStandaloneViewServerService>((object) sessionKeeper.Session, true).TryLoadSettings(sessionKeeper.Session.SessionGUID, objectType);
  }

  /// <summary>
  /// Сохраняет собственные настройки для указанного типа объектов.
  /// Метод используется редактором настроек.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  /// <param name="settings">Объект с настройками</param>
  /// <exception cref="T:ArgumentException">objectType</exception>
  /// <exception cref="T:ArgumentNullException">settings</exception>
  public void SaveSettings(int objectType, StandaloneViewObjectTypeSettings settings)
  {
    if (objectType == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов.", nameof (objectType));
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IStandaloneViewServerService>((object) sessionKeeper.Session, true).SaveSettings(sessionKeeper.Session.SessionGUID, objectType, settings);
    this.ClearCache();
  }

  /// <summary>
  /// Удаляет собственные настройки для указанного типа объектов.
  /// Метод используется редактором настроек.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объектов</param>
  public void RemoveSettings(int objectType)
  {
    if (objectType == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов.", nameof (objectType));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IStandaloneViewServerService>((object) sessionKeeper.Session, true).RemoveSettings(sessionKeeper.Session.SessionGUID, objectType);
    this.ClearCache();
  }
}
