
// Type: Intermech.Kernel.Search.IGlobalIndexSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Серверная служба для работы с настройками общего поискового индекса
    /// </summary>
    public interface IGlobalIndexSettings
    {
      /// <summary>
      /// Устанавливает минимальную длину слов для попадания в индекс
      /// </summary>
      /// <param name="sessionGUID">Ид. сессии</param>
      /// <param name="minLen">Длина слова</param>
      void SetMinWordLength(Guid sessionGUID, int minLen);

      /// <summary>Минимальная длина слова, добавляемого в индекс</summary>
      int MinWordLength { get; }

      /// <summary>
      /// Текущее количество значений атрибутов в очереди на индексацию
      /// </summary>
      long QueueLength { get; }

      /// <summary>Список загруженных конвертеров файлов</summary>
      string[] ConvertersList { get; }

      /// <summary>
      /// Метод с помощью ObjectsFoundException возвращает список объектов на индексацию. Если метод выполнился без исключений - объектов на индексацию не найдено.
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии юзера</param>
      void GetIndexQueue(Guid sessionGUID);

      /// <summary>Сохранять ли историю поисковых запросов</summary>
      bool IsSaveSearchQueryHistory { get; }

      /// <summary>
      /// Изменяет настройку режима сохранения истории поисковых запросов
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии</param>
      /// <param name="value">Значение</param>
      void SetSaveSearchQueryHistoryMode(Guid sessionGUID, bool value);

      /// <summary>Список расширений, файлы которых нельзя индексировать</summary>
      string NotIndexingExtensions { get; }

      /// <summary>Изменяет список запрещённых для индексации расширений</summary>
      /// <param name="sessionGUID">Гуид сессии</param>
      /// <param name="value">Строка, в которой через запятую перечислены запретные расширения</param>
      void SetNotIndexingExtensions(Guid sessionGUID, string value);
    }
}
