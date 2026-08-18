
// Type: Intermech.Interfaces.HybridTable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Таблица для замены DataTable</summary>
    public class HybridTable
    {
      private HybridColumns _columns;
      private List<HybridRow> _rows;

      /// <summary>
      /// Создание пустого HybridTable для таблицы с колонками columns
      /// </summary>
      public void Create(DataColumnCollection columns)
      {
        this._columns = new HybridColumns(columns);
        this._rows = new List<HybridRow>();
      }

      /// <summary>Строки</summary>
      public List<HybridRow> Rows => this._rows;

      /// <summary>Создание таблицы из DataTable</summary>
      /// <param name="table">Исходная DataTable</param>
      public void Create(DataTable table)
      {
        if (table == null)
          return;
        this._columns = new HybridColumns(table.Columns);
        this._rows = new List<HybridRow>(table.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        {
          HybridRow hybridRow = new HybridRow(this._columns);
          hybridRow.Create(row, true);
          this._rows.Add(hybridRow);
        }
      }

      /// <summary>Создание таблицы из DataRow</summary>
      /// <param name="row">Исходная DataRow</param>
      public void Create(DataRow row)
      {
        if (row == null)
          return;
        this._columns = new HybridColumns(row.Table.Columns);
        this._rows = new List<HybridRow>(1);
        HybridRow hybridRow = new HybridRow(this._columns);
        hybridRow.Create(row, true);
        this._rows.Add(hybridRow);
      }

      /// <summary>Количество строк в таблице</summary>
      public int RowsCount => this._rows == null ? 0 : this._rows.Count;

      /// <summary>Очистка таблицы</summary>
      public void Clear()
      {
        this._columns = (HybridColumns) null;
        this._rows = (List<HybridRow>) null;
      }

      /// <summary>Строка</summary>
      /// <param name="index">Индекс</param>
      /// <returns></returns>
      public HybridRow this[int index]
      {
        get => this._rows[index];
        set => this._rows[index] = value;
      }

      /// <summary>Колонки</summary>
      public HybridColumns Columns => this._columns;

      /// <summary>Добавить строку</summary>
      /// <param name="row"></param>
      public void Add(DataRow row)
      {
        HybridRow hybridRow = new HybridRow(this._columns);
        hybridRow.Create(row);
        this._rows.Add(hybridRow);
      }

      /// <summary>
      /// Возвращает гибридную строку для данной гибридной таблицы
      /// </summary>
      public HybridRow NewRow()
      {
        HybridRow hybridRow = new HybridRow(this._columns);
        hybridRow.InitData();
        return hybridRow;
      }

      /// <summary>Добавляет гибридную строку</summary>
      public void Add(HybridRow hrow) => this._rows.Add(hrow);

      /// <summary>Удалить строку</summary>
      /// <param name="index">Индекс строки</param>
      public void RemoveAt(int index) => this._rows.RemoveAt(index);

      /// <summary>Удалить строку</summary>
      /// <param name="hRow">Строка</param>
      public void Remove(HybridRow hRow) => this._rows.Remove(hRow);

      /// <summary>Выборка всей таблицы в DataTable</summary>
      /// <param name="table">DataTable</param>
      /// <returns></returns>
      public DataTable Select(DataTable table)
      {
        for (int index1 = 0; index1 < this._rows.Count; ++index1)
        {
          DataRow dataRow = table.NewRow();
          HybridRow row = this._rows[index1];
          for (int index2 = 0; index2 < this._columns.Count; ++index2)
          {
            string columnName = this._columns[index2].ColumnName;
            dataRow[columnName] = row[index1];
          }
        }
        table.AcceptChanges();
        return table;
      }
    }
}
