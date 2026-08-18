
// Type: Intermech.Data.KeyValueStores.IKeyValueContentVersion
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Data.KeyValueStores
{
    /// <summary>
    /// Общий интерфейс для всех объектов хранилища, содержащих данные. Он позволяет проверить версию хранимых данных.
    /// </summary>
    public interface IKeyValueContentVersion
    {
      /// <summary>Возвращает версию содержимого хранилища.</summary>
      int ContentVersion { get; }
    }
}
