
// Type: Intermech.Kernel.Search.SearchQueryProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    /// <summary>Класс для хранения поискового запроса</summary>
    [Serializable]
    public class SearchQueryProperties
    {
      /// <summary>
      /// Префикс, означающий что дальше будет SQL-запрос, а не поиск по индексу
      /// </summary>
      public const char SQLPrefix = '?';
      /// <summary>
      /// Префикс для поиска по идентификаторам (в верхнем регистре)
      /// </summary>
      public const char IdentifierPrefixUpper = 'N';
      /// <summary>
      /// Префикс для поиска по идентификаторам (в нижнем регистре)
      /// </summary>
      public const char IdentifierPrefixLower = 'n';

      /// <summary>Строка запроса</summary>
      public string QueryStr { get; set; }

      /// <summary>Ид. хозяина запроса</summary>
      public long UserID { get; set; }

      /// <summary>Дата и время запроса</summary>
      public DateTime QueryTime { get; set; }

      /// <summary>Уровень доступа хозяйской сессии</summary>
      public int AccessLevel { get; set; }

      public SearchQueryProperties(string queryStr, long userID, DateTime queryDate, int level)
      {
        this.QueryStr = queryStr;
        this.QueryTime = queryDate;
        this.UserID = userID;
        this.AccessLevel = level;
      }
    }
}
