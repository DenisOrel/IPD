
// Type: Intermech.Data.DataTableUtils
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Data
{
    /// <summary>Содержит утилиты для работы с DataTable.</summary>
    public static class DataTableUtils
    {
      public static DataTable Merge(IList<DataTable> tables)
      {
        if (tables == null)
          throw new ArgumentNullException(nameof (tables));
        DataTable dataTable = tables.Count != 0 ? tables[0].Clone() : throw new ArgumentOutOfRangeException(nameof (tables));
        foreach (DataTable table in (IEnumerable<DataTable>) tables)
          dataTable.Merge(table);
        dataTable.AcceptChanges();
        return dataTable;
      }
    }
}
