
// Type: Intermech.Interfaces.WebPortal.PublishObjectsRow
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Строка в таблице с результатами запроса опубликованных объектов на портал
    /// </summary>
    [Serializable]
    public struct PublishObjectsRow
    {
      /// <summary>Данные строки</summary>
      public object[] Data;

      public PublishObjectsRow(DataRow row, int columnsCount)
      {
        this.Data = new object[columnsCount];
        for (int columnIndex = 0; columnIndex < columnsCount; ++columnIndex)
        {
          if (row[columnIndex] != DBNull.Value)
            this.Data[columnIndex] = row[columnIndex];
        }
      }

      /// <summary>Элемент строки</summary>
      /// <param name="index">Индекс</param>
      /// <returns></returns>
      public object this[int index]
      {
        get => this.Data[index];
        set => this.Data[index] = value;
      }
    }
}
