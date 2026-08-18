
// Type: Intermech.Cache.IExpiration
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache
{
    /// <summary>
    /// Позволяет реализовать различные алгоритмы устаревания элементов,
    /// помещенных в кэш.
    /// </summary>
    public interface IExpiration
    {
      /// <summary>
      /// Возвращает true, если элемент устарел и его нельзя использовать.
      /// </summary>
      bool HasExpired { get; }

      /// <summary>
      /// Вызывается всякий раз, когда пользователь обращается к контролируемому
      /// элементу кэша.
      /// </summary>
      void Notify();
    }
}
