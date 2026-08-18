
// Type: Intermech.Cache.IStorage
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache
{
    /// <summary>
    /// Позволяет реализовать различные хранилища для элементов, помещенных в кэш.
    /// </summary>
    public interface IStorage
    {
      /// <summary>Добавляет элемент с указанным ключем в хранилище.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      void Add(object key, object data);

      /// <summary>Удаляет из хранилища элемент с указанным ключем.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      void Remove(object key);

      /// <summary>Очищает хранилище, удаляя все элементы.</summary>
      void Flush();

      /// <summary>
      /// Возвращает из хранилища элемент с указанным ключем. Если элемента с указанным ключем
      /// нет в хранилище, то результатом будет null.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <returns>Элемент</returns>
      object GetData(object key);
    }
}
