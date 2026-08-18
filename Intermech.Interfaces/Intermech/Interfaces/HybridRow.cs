
// Type: Intermech.Interfaces.HybridRow
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Строка в таблице</summary>
    public class HybridRow
    {
      private HybridColumns _columns;
      private object[] _data;

      /// <summary>Конструктор</summary>
      /// <param name="columns">Колонки</param>
      public HybridRow(HybridColumns columns)
      {
        this._columns = columns;
        this._data = (object[]) null;
      }

      /// <summary>Создание строки</summary>
      /// <param name="row">DataRow</param>
      /// <param name="fullCopyMode">Режим "полного" копирования</param>
      public void Create(DataRow row, bool fullCopyMode = false)
      {
        if (row == null)
          return;
        if (fullCopyMode)
        {
          this._data = row.ItemArray;
        }
        else
        {
          int count = this._columns.Count;
          this._data = new object[count];
          for (int index = 0; index < count; ++index)
            this._data[index] = row[this._columns[index].ColumnName];
        }
      }

      /// <summary>Элемент строки</summary>
      /// <param name="columnName">Название колонки</param>
      /// <returns></returns>
      public object this[string columnName]
      {
        get => this._data[this._columns.GetIndexByName(columnName)];
        set => this._data[this._columns.GetIndexByName(columnName)] = value;
      }

      /// <summary>Элемент строки</summary>
      /// <param name="index">Индекс</param>
      /// <returns></returns>
      public object this[int index]
      {
        get => this._data[index];
        set => this._data[index] = value;
      }

      /// <summary>Колонки</summary>
      public HybridColumns Column => this._columns;

      /// <summary>Возвращает строку в виде DataRow</summary>
      public DataRow AsDataRow
      {
        get
        {
          DataTable dataTable = new DataTable();
          int count1 = this._columns.Count;
          for (int index = 0; index < count1; ++index)
          {
            HybridColumns.HybridColumn column = this._columns[index];
            dataTable.Columns.Add(new DataColumn(column.ColumnName, column.DataType));
          }
          int count2 = dataTable.Columns.Count;
          DataRow asDataRow = dataTable.NewRow();
          for (int index = 0; index < count2; ++index)
          {
            string columnName = dataTable.Columns[index].ColumnName;
            asDataRow[index] = this[columnName];
          }
          return asDataRow;
        }
      }

      /// <summary>
      /// Инициализирует массив данных, т.к. в конструкторе этого не делается
      /// </summary>
      internal void InitData() => this._data = new object[this._columns.Count];
    }
}
