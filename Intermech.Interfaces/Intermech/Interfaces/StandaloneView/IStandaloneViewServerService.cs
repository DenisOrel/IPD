
// Type: Intermech.Interfaces.StandaloneView.IStandaloneViewServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.StandaloneView
{
    /// <summary>
    /// Серверный сервис для доступа к настройкам типов объектов для режима автономного просмотра.
    /// </summary>
    public interface IStandaloneViewServerService
    {
      /// <summary>
      /// Возвращает настройки для указанного типа объектов с учетом настроек базовых типов объектов.
      /// </summary>
      /// <param name="sessionGuid">Идентификатор сессии</param>
      /// <param name="objectType">Идентификатор типа объектов</param>
      /// <returns>Объект с настройками</returns>
      StandaloneViewObjectTypeSettings GetEffectiveSettings(Guid sessionGuid, int objectType);

      /// <summary>
      /// Загружает собственные настройки для указанного типа объектов, при этом настройки для базовых типов объектов не используются.
      /// Метод используется редактором настроек.
      /// </summary>
      /// <param name="sessionGuid">Идентификатор сессии</param>
      /// <param name="objectType">Идентификатор типа объектов</param>
      /// <returns>Объект с настройками или null</returns>
      /// <exception cref="T:ArgumentException">objectType</exception>
      StandaloneViewObjectTypeSettings TryLoadSettings(Guid sessionGuid, int objectType);

      /// <summary>
      /// Сохраняет собственные настройки для указанного типа объектов.
      /// Метод используется редактором настроек.
      /// </summary>
      /// <param name="sessionGuid">Идентификатор сессии</param>
      /// <param name="objectType">Идентификатор типа объектов</param>
      /// <param name="settings">Объект с настройками</param>
      /// <exception cref="T:ArgumentException">objectType</exception>
      /// <exception cref="T:ArgumentNullException">settings</exception>
      void SaveSettings(Guid sessionGuid, int objectType, StandaloneViewObjectTypeSettings settings);

      /// <summary>
      /// Удаляет собственные настройки для указанного типа объектов.
      /// Метод используется редактором настроек.
      /// </summary>
      /// <param name="sessionGuid">Идентификатор сессии</param>
      /// <param name="objectType">Идентификатор типа объектов</param>
      void RemoveSettings(Guid sessionGuid, int objectType);

      /// <summary>
      /// Возвращает значение счетчика изменений в настройках сервиса. Используется при реализации кэширования настроек.
      /// </summary>
      /// <param name="sessionGuid">Идентификатор сессии</param>
      long GetWriteSequence(Guid sessionGuid);
    }
}
