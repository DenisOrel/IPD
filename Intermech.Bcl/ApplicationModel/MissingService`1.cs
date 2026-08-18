
// Type: Intermech.ApplicationModel.MissingService`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Провайдер опционального сервиса, используемый в случае отсутствия сервиса.
    /// </summary>
    /// <typeparam name="T">Тип сервиса, предоставляемого провайдером</typeparam>
    public class MissingService<T> : IOptionalService<T>
    {
      /// <summary>
      /// Возвращает объект сервиса или нулевое значение для данного типа объекта, если объект не может быть получен.
      /// </summary>
      /// <returns>Объект или нулевое значение для данного типа объектов</returns>
      public T TryGet() => default (T);
    }
}
