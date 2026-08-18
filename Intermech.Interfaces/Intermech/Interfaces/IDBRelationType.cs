
// Type: Intermech.Interfaces.IDBRelationType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для работы с типом связи</summary>
    public interface IDBRelationType : IDBAttributableType
    {
      /// <summary>Ид. типа связи (только для чтения).</summary>
      int RelationType { get; }

      /// <summary>
      /// Наименование типа связи с точки зрения родительского объекта (например, состоит из...).
      /// </summary>
      string TypeName { get; set; }

      /// <summary>
      /// Наименование типа связи с точки зрения дочернего объекта  (например, Входит в...).
      /// </summary>
      string ReverseName { get; set; }

      /// <summary>Комментарии</summary>
      string Note { get; set; }

      /// <summary>
      /// Нужно ли извлекать на диск файлы объектов, объединённых данной связью.
      /// </summary>
      bool CheckoutFile { get; set; }

      /// <summary>Иконка для отображения типа связей</summary>
      byte[] Icon { get; set; }

      /// <summary>
      /// Нужно ли сохранять историю изменения связей в рамках одной версии.
      /// </summary>
      bool SaveHistory { get; }

      /// <summary>Удалить тип связи</summary>
      /// <param name="DeleteMode"></param>
      /// <returns></returns>
      int Delete(long DeleteMode);

      /// <summary>
      /// Уникальное описание типа связи (например, Проектная связь)
      /// </summary>
      string Description { get; set; }

      /// <summary>Краткое наименование типа связи (может быть пустым)</summary>
      string ShortName { get; set; }

      /// <summary>Опции типа связей</summary>
      RelationTypeOptions Options { get; set; }

      /// <summary>
      /// Структура, позволяющая прочитать или записать сразу все параметры типа связей.
      /// </summary>
      RelationTypeProperties PropertiesStructure { get; set; }

      /// <summary>
      /// Пересоздает таблицу-представление данных для этого типа связей
      /// </summary>
      void RebuildView();
    }
}
