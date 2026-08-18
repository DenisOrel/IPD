
// Type: Intermech.Cache.RemoveCause
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache
{
    /// <summary>
    /// Описывает причины, по которым элемент был удален из кэша.
    /// </summary>
    public enum RemoveCause
    {
      /// <summary>Удален из кэша политикой замещения элементов</summary>
      Evicted,
      /// <summary>Удален из кэша по причине устаревания</summary>
      Expired,
      /// <summary>Удален из кэша пользователем</summary>
      Removed,
      /// <summary>Удален в результате полной очистки кэша пользователем</summary>
      Flushed,
    }
}
