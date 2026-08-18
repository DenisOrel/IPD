
// Type: Intermech.Interfaces.IDBShortBlobAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для работы с атрибутами типа ftShortBlob</summary>
    public interface IDBShortBlobAttribute
    {
      /// <summary>
      /// Возвращает описание и содержимое текущего значения короткого блоба
      /// </summary>
      /// <returns>Описание и содержимое блоба</returns>
      ShortBlobValue GetBlobValue();

      /// <summary>
      /// Записывает в текущее значение описание и содержимое блоба
      /// </summary>
      /// <param name="blobValue">Что нужно записать в блоб</param>
      void SetBlobValue(ShortBlobValue blobValue);

      /// <summary>Возвращает массив значений атрибута Короткий блоб</summary>
      /// <returns></returns>
      ShortBlobValue[] GetBlobValues();

      /// <summary>
      /// Присваивает атрибуту список значений типа Короткий блоб
      /// </summary>
      /// <param name="blobValues">Массив новых значений</param>
      void SetBlobValues(ShortBlobValue[] blobValues);

      /// <summary>
      /// Возвращает содержимое короткого блоба БЕЗ РАСПАКОВКИ.
      /// </summary>
      /// <returns>Массив с данными. Если чтение не удалось - массив пустой, а не null. Данные не распакованы!</returns>
      byte[] GetData();
    }
}
