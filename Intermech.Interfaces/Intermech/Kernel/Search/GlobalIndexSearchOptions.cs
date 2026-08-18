
// Type: Intermech.Kernel.Search.GlobalIndexSearchOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    /// <summary>Опции, управляющие поиском в общем поисковом индексе</summary>
    [Flags]
    public enum GlobalIndexSearchOptions
    {
      None = 0,
      /// <summary>Сортировать по релевантности запроса</summary>
      OrderByRelevance = 1,
      /// <summary>Искать подстроку</summary>
      SubstringSearch = 2,
      /// <summary>Поиск с учётом общей словоформы</summary>
      StemmedWords = 4,
    }
}
