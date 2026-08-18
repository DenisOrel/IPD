
// Type: Intermech.Kernel.Search.GlobalIndexSearchValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>Класс-описатель запроса в общий поисковый индекс</summary>
    [TypeConverter(typeof (ToBase64StringTypeConverter<GlobalIndexSearchValue>))]
    [Serializable]
    public class GlobalIndexSearchValue : ICloneable
    {
      /// <summary>
      /// Максимально допустимое количество записей в истории поиска
      /// </summary>
      public static int HistoryLimit = 15;
      /// <summary>Искомая строка</summary>
      public string Value;
      /// <summary>Опции, управляющие поиском</summary>
      public GlobalIndexSearchOptions SearchOptions;
      /// <summary>История поиска</summary>
      [NonSerialized]
      public List<string> History;
      public static readonly GlobalIndexSearchValue Empty = new GlobalIndexSearchValue(string.Empty, GlobalIndexSearchOptions.None, new List<string>());

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="value">Искомое значение</param>
      /// <param name="searchOptions">Опции поиска</param>
      public GlobalIndexSearchValue(string value, GlobalIndexSearchOptions searchOptions)
      {
        this.Value = value;
        this.SearchOptions = searchOptions;
      }

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="value">Искомое значение</param>
      /// <param name="searchOptions">Опции поиска</param>
      /// <param name="history">История поиска</param>
      public GlobalIndexSearchValue(
        string value,
        GlobalIndexSearchOptions searchOptions,
        List<string> history)
      {
        this.Value = value;
        this.SearchOptions = searchOptions;
        this.History = history != null ? new List<string>((IEnumerable<string>) history) : (List<string>) null;
      }

      /// <summary>Добавить текст в историю поиска</summary>
      /// <param name="text">Искомый текст</param>
      public void AddToHistory(string text)
      {
        if (string.IsNullOrEmpty(text))
          return;
        this.History = this.History ?? new List<string>(GlobalIndexSearchValue.HistoryLimit);
        string lower = text.Trim().ToLower();
        for (int index = this.History.Count - 1; index >= 0; --index)
        {
          if (this.History[index].ToString().Trim().ToLower().Equals(lower))
            this.History.RemoveAt(index);
        }
        while (this.History.Count >= GlobalIndexSearchValue.HistoryLimit)
          this.History.RemoveAt(this.History.Count - 1);
        this.History.Insert(0, text);
      }

      public object Clone()
      {
        return (object) new GlobalIndexSearchValue(this.Value, this.SearchOptions, this.History != null ? new List<string>((IEnumerable<string>) this.History) : this.History);
      }

      public override bool Equals(object obj)
      {
        return obj is GlobalIndexSearchValue indexSearchValue && this.Value.Equals(indexSearchValue.Value) && this.SearchOptions.Equals((object) indexSearchValue.SearchOptions);
      }

      public override int GetHashCode() => (int) this.SearchOptions << 24 ^ this.Value.GetHashCode();
    }
}
