
// Type: Intermech.Interfaces.IPluginsData
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для записи и чтения в экземпляры системных объектов дополнительных данных плагинов (для хранения и проверки состояний в памяти)
    /// </summary>
    public interface IPluginsData
    {
      /// <summary>
      /// Получить из экземпляра объекта информацию модуля расширения
      /// </summary>
      /// <param name="key">Ключ</param>
      /// <returns>Значение или null</returns>
      object GetPluginsData(object key);

      /// <summary>
      /// Записывает в экземпляр объекта информацию модуля расширения.
      /// </summary>
      /// <param name="key">Ключ</param>
      /// <param name="value">Значение</param>
      void SetPluginsData(object key, object value);

      /// <summary>
      /// Удаляет из экземпляра объекта информацию модуля расширения
      /// </summary>
      /// <param name="key">Ключ</param>
      void RemovePluginsData(object key);
    }
}
