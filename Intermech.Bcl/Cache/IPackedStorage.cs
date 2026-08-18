
// Type: Intermech.Cache.IPackedStorage
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache
{
    /// <summary>
    /// Позволяет реализовать хранилище, которое преобразует
    /// помещаемые в него элементы.
    /// </summary>
    public interface IPackedStorage
    {
      /// <summary>
      /// Упаковывает исходный элемент в объект, пригодный для
      /// помещения в хранилище.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <returns>Упакованное представление элемента</returns>
      object PackObject(object key, object data);

      /// <summary>
      /// Восстанавливает элемент из упакованного после извлечения из хранилища.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="packedData">Упакованное представление элемента</param>
      /// <returns>Элемент</returns>
      object UnpackObject(object key, object packedData);
    }
}
