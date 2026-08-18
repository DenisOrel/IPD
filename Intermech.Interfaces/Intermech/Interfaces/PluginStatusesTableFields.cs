
// Type: Intermech.Interfaces.PluginStatusesTableFields
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Названия и описания полей в таблице со статусами плагинов
    /// </summary>
    public static class PluginStatusesTableFields
    {
      /// <summary>Название таблицы - "PluginStatusesTable"</summary>
      public static string tableName = "PluginStatusesTable";
      /// <summary>
      /// Название колонки со статусом (первичный ключ) - "STATUS"
      /// </summary>
      public static string columnStatus = "STATUS";
      /// <summary>Название колонки с описанием статуса - "DESCRIPTION"</summary>
      public static string columnDescription = "DESCRIPTION";
      /// <summary>Название колонки с изображением для статуса - "IMAGE"</summary>
      public static string columnImage = "IMAGE";
      /// <summary>
      /// Название колонки с контрольной суммой CRC32 изображения - "IMAGECRC32"
      /// </summary>
      public static string columnImageCRC32 = "IMAGECRC32";
      /// <summary>
      /// Заголовок колонки со статусом (первичный ключ) - "Статус"
      /// </summary>
      public static string captionStatus = LocalizationHolder.rm.GetString("Interfaces_71");
      /// <summary>Заголовок колонки с описанием статуса - "Описание"</summary>
      public static string captionDescription = LocalizationHolder.rm.GetString("Interfaces_565");
      /// <summary>
      /// Заголовок колонки с изображением для статуса - "Изображение"
      /// </summary>
      public static string captionImage = LocalizationHolder.rm.GetString("Interfaces_72");
      /// <summary>
      /// Заголовок колонки с контрольной суммой CRC32 изображения - "CRC32 изображения"
      /// </summary>
      public static string captionImageCRC32 = LocalizationHolder.rm.GetString("Interfaces_73");
    }
}
