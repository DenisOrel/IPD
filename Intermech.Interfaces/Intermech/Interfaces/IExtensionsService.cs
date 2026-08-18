
// Type: Intermech.Interfaces.IExtensionsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>служба для работы с настройками просмотра файлов</summary>
    public interface IExtensionsService
    {
      /// <summary>Методы для открытия просмотра</summary>
      string Methods { get; }

      /// <summary>Свойства для открытия просмотра</summary>
      string Properties { get; }

      /// <summary>
      /// Отладочный режим - включает/выключает запись отладочной информации при просмотре файлов
      /// </summary>
      bool DebugMode { get; }

      /// <summary>
      /// Записывать подписи и параметры в файл перед просмотром
      /// </summary>
      bool WriteSignsAndParams { get; }

      /// <summary>Описания настроек открытия файлов</summary>
      IReadOnlyCollection<FileExtensionsInfo> GetStoredFileExtensionsInfo();

      /// <summary>
      /// Получить перечень типов объектов, для которых приоритетный порядок отображения аутентичных файлов
      /// </summary>
      /// <returns></returns>
      IReadOnlyCollection<int> GetPriorityViewAuthenticFileObjTypes();

      /// <summary>Изменить настройки для просмотра файлов</summary>
      /// <param name="settings">новые настройки</param>
      /// <param name="methods">новое значение методы для открытия просмотра</param>
      /// <param name="properties">новое значние свойства для открытия просмотра</param>
      /// <param name="debugMode">отладочный режим</param>
      /// <param name="writeSignsAndParams"></param>
      /// <param name="priorViewAuthFilesObjTypes"></param>
      void ChangeSettings(
        IReadOnlyCollection<FileExtensionsInfo> settings,
        string methods,
        string properties,
        bool debugMode,
        bool writeSignsAndParams,
        IReadOnlyCollection<int> priorViewAuthFilesObjTypes);

      /// <summary>Обновить возможные настройки</summary>
      void CheckDefaultFileExtensions();

      /// <summary>Перечитать настройки</summary>
      void ReloadParams();

      /// <summary>Получить настройки просмотра для расширения</summary>
      /// <param name="extension"></param>
      /// <returns></returns>
      IReadOnlyCollection<FileExtensionsInfo> GetFileExtensionsInfo(string @extension);

      /// <summary>
      /// Добавить настройку в кэш спешно просматриваемых, для данного расшиерния
      /// </summary>
      /// <param name="extension"></param>
      /// <param name="fileExtensionsInfo"></param>
      void AddFileExtensionInfoToCache(string @extension, FileExtensionsInfo fileExtensionsInfo);
    }
}
