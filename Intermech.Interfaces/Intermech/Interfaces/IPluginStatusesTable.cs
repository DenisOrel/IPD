
// Type: Intermech.Interfaces.IPluginStatusesTable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для передачи со стороны серверных плагинов таблицы со статусами, текстовыми описаниями и изображениями
    /// </summary>
    public interface IPluginStatusesTable
    {
      /// <summary>
      /// Получить у плагина таблицу, в которой хранится коллекция статусов, их текстовых описаний и изображений
      /// </summary>
      /// <param name="PluginGuid">Guid плагина</param>
      /// <param name="IncludeIcons">Включить в таблицу иконки</param>
      /// <param name="statuses">
      /// Массив идентификаторов статусов, для которых необходимо получить информацию. Если указать
      /// null, то метод вернет описания всех возможных статусов
      /// </param>
      /// <returns>Таблица со статусами, их текстовыми описаниями и изображениями (если они запрошены)</returns>
      DataTable GetPluginStatusesTable(string PluginGuid, bool IncludeIcons, params int[] statuses);

      /// <summary>
      /// Добавить статус с описанием и изображением в таблицу указанного плагина
      /// </summary>
      /// <param name="PluginGuid">Guid плагина</param>
      /// <param name="status">Статус</param>
      /// <param name="description">Описание статуса</param>
      /// <param name="image">Изображение</param>
      void AddStatus(string PluginGuid, int status, string description, byte[] image);

      /// <summary>Удалить статус из таблицы указанного плагина</summary>
      /// <param name="PluginGuid">Guid плагина</param>
      /// <param name="status">Статус</param>
      void RemoveStatus(string PluginGuid, int status);

      /// <summary>Удалить все статусы из таблицы указанного плагина</summary>
      /// <param name="PluginGuid">Guid плагина</param>
      void RemoveStatuses(string PluginGuid);
    }
}
