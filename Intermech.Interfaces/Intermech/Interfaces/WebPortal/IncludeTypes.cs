
// Type: Intermech.Interfaces.WebPortal.IncludeTypes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Типы (не)включения в пакет</summary>
    public enum IncludeTypes
    {
      /// <summary>Включен</summary>
      [EnablePublish(UnitPublishType.Publish)] Include,
      /// <summary>Пустышка</summary>
      [EnablePublish(UnitPublishType.Dummy)] NoChanged,
      /// <summary>Запрещен по уровню доступа</summary>
      [EnablePublish(UnitPublishType.Forbidden)] NoAccess,
      /// <summary>Публикация ввиде контейнера атрибутов</summary>
      [EnablePublish(UnitPublishType.Publish)] FCAttributesOnly,
      /// <summary>Включен как ссылка (Владелец)</summary>
      [EnablePublish(UnitPublishType.Publish)] ObjectLink,
      /// <summary>Запрещен фильтрацией ОТД</summary>
      [EnablePublish(UnitPublishType.Dummy)] FilteredByOTD,
      /// <summary>Отфильтрован по типу</summary>
      [EnablePublish(UnitPublishType.Dummy)] FilteredByTypes,
      /// <summary>Запрещен вручную</summary>
      [EnablePublish(UnitPublishType.Forbidden)] Forbidden,
      /// <summary>Находится в запрещенном фильтрацией ОТД составе</summary>
      [EnablePublish(UnitPublishType.Dummy)] FilteredCompositionByOTD,
      /// <summary>
      /// Публикация ввиде контейнера атрибутов только с файлами
      /// </summary>
      [EnablePublish(UnitPublishType.Publish)] FCFileAttributesOnly,
    }
}
