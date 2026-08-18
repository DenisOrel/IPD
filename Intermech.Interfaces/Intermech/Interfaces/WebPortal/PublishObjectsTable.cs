
// Type: Intermech.Interfaces.WebPortal.PublishObjectsTable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Таблица с результатами запроса опубликованных объектов на портал
    /// </summary>
    [Serializable]
    public struct PublishObjectsTable
    {
      /// <summary>Строки таблицы</summary>
      public PublishObjectsRow[] Rows;
      /// <summary>Колонки таблицы</summary>
      public PublishObjectsColumn[] Columns;
      /// <summary>Имя таблицы</summary>
      public string Name;

      public PublishObjectsTable(DataTable table)
      {
        this.Name = table.TableName;
        this.Columns = new PublishObjectsColumn[table.Columns.Count];
        for (int index = 0; index < table.Columns.Count; ++index)
          this.Columns[index] = new PublishObjectsColumn(table.Columns[index]);
        this.Rows = new PublishObjectsRow[table.Rows.Count];
        for (int index = 0; index < table.Rows.Count; ++index)
          this.Rows[index] = new PublishObjectsRow(table.Rows[index], table.Columns.Count);
      }

      /// <summary>Строка</summary>
      /// <param name="index">Индекс</param>
      /// <returns></returns>
      public PublishObjectsRow this[int index]
      {
        get => this.Rows[index];
        set => this.Rows[index] = value;
      }
    }
}
