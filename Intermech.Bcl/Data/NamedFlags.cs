
// Type: Intermech.Data.NamedFlags
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Data
{
    /// <summary>
    /// Библиотека стандартных и часто используемых флагов общего назначения
    /// </summary>
    public static class NamedFlags
    {
      /// <summary>Признак read-only значения.</summary>
      public static readonly StringKey ReadOnly = new StringKey(nameof (ReadOnly));
      /// <summary>
      /// Признак, что при неудачной записи значения в контейнер следует бросать исключение.
      /// </summary>
      public static readonly StringKey ThrowSetException = new StringKey(nameof (ThrowSetException));
    }
}
