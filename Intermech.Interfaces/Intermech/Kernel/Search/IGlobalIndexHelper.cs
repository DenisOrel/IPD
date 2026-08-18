
// Type: Intermech.Kernel.Search.IGlobalIndexHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Интерфейс службы поискового индекса, доступный на клиенте
    /// </summary>
    public interface IGlobalIndexHelper
    {
      /// <summary>
      /// Добавляет атрибуты объектов objectsID в очередь на индексацию
      /// </summary>
      /// <param name="sessionGuid">Гуид сессии</param>
      /// <param name="objectsID">Массив ObjectID на индексацию</param>
      /// <returns>Количество атрибутов, добавленных в очередь на индексацию</returns>
      int AddToQueue(Guid sessionGuid, long[] objectsID);

      /// <summary>
      /// Возвращает поисковые запросы, начало которых наиболее похоже на запрос beginStr, набираемый пользователем в строке поиска.
      /// Приоритетно выбирает наиболее частые и свежие запросы.
      /// </summary>
      /// <param name="sessionGuid">Гуид клиентской сесии</param>
      /// <param name="beginStr">Начальная строка запроса</param>
      /// <param name="maxStrings">Начальная строка запроса</param>
      /// <returns>Отсортированный массив запросов</returns>
      string[] GetSimilarQueries(Guid sessionGuid, string beginStr, int maxStrings);

      /// <summary>
      /// Возвращает всю историю поисковых запросов всех юзеров. Требует прав администратора.
      /// </summary>
      /// <param name="sessionGuid">Гуид клиентской сессии</param>
      /// <returns>Таблица запросов, отсортированная по дате и времени запроса</returns>
      DataTable GetQueriesHistory(Guid sessionGuid);

      /// <summary>
      /// Возвращает историю поисковых запросов юзера userID. Для просмотра чужих запросов требует прав администратора.
      /// </summary>
      /// <param name="sessionGuid">Гуид клиентской сессии</param>
      /// <param name="userID">Ид. пользователя, чьи запросы нужно получить (-1 если нужны запросы всех юзеров)</param>
      /// <param name="beginDate">Дата, с которой нужно вернуть запросы (если с самого начала, то DateTime.MinValue)</param>
      /// <param name="endDate">Дата, по которую нужно вернуть запросы (если до последнего, то DateTime.MaxValue)</param>
      /// <returns>Таблица запросов, отсортированная по дате и времени запроса</returns>
      DataTable GetQueriesHistory(Guid sessionGuid, long userID, DateTime beginDate, DateTime endDate);
    }
}
