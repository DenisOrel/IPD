
// Type: Intermech.Cache.IPolledExpiration
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache
{
    /// <summary>
    /// Позволяет реализовать различные алгоритмы устаревания элементов кэша,
    /// основанные на периодической проверке какого-либо условия.
    /// помещенных в кэш.
    /// </summary>
    public interface IPolledExpiration
    {
      /// <summary>
      /// Выполняет проверку условия, от которого зависит устаревание элемента.
      /// </summary>
      void CheckExpired();
    }
}
