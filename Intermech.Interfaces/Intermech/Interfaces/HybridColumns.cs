
// Type: Intermech.Interfaces.HybridColumns
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Колонки</summary>
    public class HybridColumns
    {
      /// <summary>Список колонок</summary>
      private HybridColumns.HybridColumn[] _columns;
      /// <summary>Индекс по имени</summary>
      private HybridColumns.IndexCache _indexCache;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="columns"></param>
      public HybridColumns(DataColumnCollection columns)
      {
        int count = columns.Count;
        this._columns = new HybridColumns.HybridColumn[count];
        this._indexCache = new HybridColumns.IndexCache(count);
        for (int index = 0; index < count; ++index)
        {
          DataColumn column = columns[index];
          string columnName = column.ColumnName;
          this._columns[index] = new HybridColumns.HybridColumn(columnName, column.DataType);
          this._indexCache.Add(columnName, index);
        }
      }

      public HybridColumns()
      {
      }

      /// <summary>Индекс колонки в коллекции</summary>
      /// <param name="columnName">Название</param>
      /// <returns></returns>
      public HybridColumns.HybridColumn this[string columnName]
      {
        get => this._columns[this.GetIndexByName(columnName)];
      }

      /// <summary>Колонка</summary>
      /// <param name="index">Индекс в коллекции</param>
      /// <returns></returns>
      public HybridColumns.HybridColumn this[int index] => this._columns[index];

      /// <summary>Получение индекса колонки по имени</summary>
      /// <param name="columnName"></param>
      public int GetIndexByName(string columnName) => this._indexCache.Get(columnName);

      /// <summary>Количество колонок</summary>
      public int Count => this._columns.Length;

      /// <summary>Класс для описания колонки</summary>
      /// <summary>Конструктор</summary>
      /// <param name="columnName">Наименование столбца</param>
      /// <param name="dataType">Тип данных</param>
      public struct HybridColumn(string columnName, Type dataType)
      {
        /// <summary>Наименование столбца</summary>
        public string ColumnName = columnName;
        /// <summary>Тип данных</summary>
        public Type DataType = dataType;
      }

      /// <summary>Кеш для индексов</summary>
      /// <remarks>Для ускорения повторного поиска полей Only</remarks>
      internal sealed class IndexCache
      {
        /// <summary>Последний ключ</summary>
        private string _key;
        /// <summary>Последнее значение</summary>
        private int _value = -1;
        /// <summary>Внутренний кеш</summary>
        private Dictionary<string, int> _cache;

        /// <summary>Конструктор</summary>
        /// <param name="capacity"></param>
        public IndexCache(int capacity) => this._cache = new Dictionary<string, int>(capacity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, int value) => this._cache.Add(key, value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Get(string key)
        {
          if (this._key == key)
            return this._value;
          int num = -1;
          if (!this._cache.TryGetValue(key, out num))
            num = -1;
          this._key = key;
          this._value = num;
          return num;
        }
      }
    }
}
