
// Type: Intermech.Data.SqlDialect
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Data
{
    public static class SqlDialect
    {
      public static long LastInsertRowId(ISqlProviderServices sqlServices, IDbCommand command)
      {
        return ((sqlServices != null ? sqlServices.TryGetLastInsertService() : throw new ArgumentNullException(nameof (sqlServices))) ?? throw new NotSupportedException(string.Format(LocalizationHolder.rm.GetString("SR_1675"), (object) typeof (ISqlLastInsertService).FullName))).LastInsertRowId(command);
      }
    }
}
