
// Type: Intermech.Interfaces.WebPortal.PublishObjectsColumn
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Колонка в таблице с результатами запроса опубликованных объектов на портал
    /// </summary>
    [Serializable]
    public struct PublishObjectsColumn
    {
      /// <summary>Данные строки</summary>
      public string Name;
      /// <summary>Код типа данных</summary>
      public ColumnTypeCode TypeCode;

      public PublishObjectsColumn(DataColumn col)
      {
        this.Name = col.ColumnName;
        this.TypeCode = (ColumnTypeCode) Type.GetTypeCode(col.DataType);
      }
    }
}
