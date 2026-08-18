
// Type: Intermech.Interfaces.IDBRelationTypeInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для получения инфы о типе связей</summary>
    public interface IDBRelationTypeInfo : IDBAttributableTypeInfo
    {
      /// <summary>Ид. типа связи (только для чтения).</summary>
      int RelationType { get; }

      /// <summary>
      /// Наименование типа связи с точки зрения родительского объекта (например, состоит из...).
      /// </summary>
      string TypeName { get; }

      /// <summary>
      /// Наименование типа связи с точки зрения дочернего объекта  (например, Входит в...).
      /// </summary>
      string ReverseName { get; }

      /// <summary>Комментарии</summary>
      string Note { get; }

      /// <summary>
      /// Нужно ли извлекать на диск файлы объектов, объединённых данной связью.
      /// </summary>
      bool CheckoutFile { get; }

      /// <summary>Иконка для отображения типа связей</summary>
      byte[] Icon { get; }

      /// <summary>
      /// Нужно ли сохранять историю изменения связей в рамках одной версии.
      /// </summary>
      bool SaveHistory { get; }

      /// <summary>
      /// Уникальное описание типа связи (например, Проектная связь)
      /// </summary>
      string Description { get; }

      /// <summary>Краткое наименование типа связи (может быть пустым)</summary>
      string ShortName { get; }

      /// <summary>Опции типа связей</summary>
      RelationTypeOptions Options { get; }

      /// <summary>Гуид типа объектов</summary>
      Guid GUID { get; }

      /// <summary>
      /// Структура, позволяющая прочитать или записать сразу все параметры типа связей.
      /// </summary>
      RelationTypeProperties PropertiesStructure { get; }
    }
}
