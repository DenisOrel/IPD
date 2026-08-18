
// Type: Intermech.Cache.ILimitedStorage
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache
{
    /// <summary>
    /// Позволяет реализовать хранилище, имеющее ограниченный объем.
    /// </summary>
    public interface ILimitedStorage
    {
      /// <summary>
      /// Возвращает true, если у хранилища включен режим ограничения объема.
      /// </summary>
      bool LimitsEnabled { get; }

      /// <summary>Возвращает объем хранилища.</summary>
      long TotalSpace { get; }

      /// <summary>Возвращает объем свободного пространства в хранилище.</summary>
      long FreeSpace { get; }

      /// <summary>
      /// Возвращает объем, который займет элемент после помещения в кэш.
      /// </summary>
      /// <param name="data">Элемент</param>
      /// <returns>Объем элемента</returns>
      long EstimateSpace(object data);
    }
}
