
// Type: Intermech.Kernel.Search.GlobalIndexOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Опции, управляющие индексированием значений атрибутов в общем поисковом индексе
    /// </summary>
    [Flags]
    public enum GlobalIndexOptions
    {
      None = 0,
      /// <summary>Не разбивать значение на слова</summary>
      DisableSplitValue = 1,
      /// <summary>Не приводить слова к общей форме</summary>
      DisableStemmWords = 2,
    }
}
