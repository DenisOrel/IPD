
// Type: Intermech.Kernel.Search.IndexerAttributeSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Класс для хранения настроек индексирования атрибута в общем поисковом индексе
    /// </summary>
    [Serializable]
    public class IndexerAttributeSettings
    {
      /// <summary>
      /// Набор символов-разделителей, которые используются по умолчанию для отделения слов
      /// </summary>
      public static char[] DefaultDelimiterChars = new char[28]
      {
        ' ',
        ',',
        '.',
        ':',
        '\t',
        '!',
        '?',
        '\n',
        ';',
        '"',
        '+',
        '(',
        ')',
        '[',
        ']',
        '|',
        '<',
        '>',
        '=',
        '/',
        '*',
        '-',
        '^',
        '{',
        '}',
        '»',
        '«',
        '\\'
      };
      /// <summary>
      /// Имя параметра для хранения расширенных настроек индексации
      /// </summary>
      public static string GlobalIndexParamName = "GlobalIndex";
      /// <summary>Имя лог-файла общего поискового индекса</summary>
      public static string GlobalIndexLogFileName = "indexing.log";
      /// <summary>Опции индексирования атрибута</summary>
      public GlobalIndexOptions Options;

      /// <summary>
      /// Конструктор инициализирует класс значениями по-умолчанию
      /// </summary>
      public IndexerAttributeSettings() => this.Options = GlobalIndexOptions.None;

      public IndexerAttributeSettings(GlobalIndexOptions options) => this.Options = options;

      public IndexerAttributeSettings(int options) => this.Options = (GlobalIndexOptions) options;
    }
}
