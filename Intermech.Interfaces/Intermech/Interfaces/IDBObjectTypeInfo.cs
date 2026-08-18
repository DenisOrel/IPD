
// Type: Intermech.Interfaces.IDBObjectTypeInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для чтения инфы о типе объектов</summary>
    public interface IDBObjectTypeInfo : IDBAttributableTypeInfo
    {
      /// <summary>Идентификатор типа объекта (только для чтения)</summary>
      int ObjectType { get; }

      /// <summary>Наименование типа объектов</summary>
      string ObjectTypeName { get; }

      /// <summary>Краткое наименование типа объектов</summary>
      string ObjectTypeShortName { get; }

      /// <summary>Наименование объекта данного типа (например, Деталь)</summary>
      string ObjectInstanceName { get; }

      /// <summary>Иконка для отображения типа объектов</summary>
      byte[] Icon { get; }

      /// <summary>
      /// 0 - абстрактный тип объекта (контейнер для группировки других типов объектов);
      /// 1 - объекты данного типа не могут иметь версий;
      /// 2 - объекты данного типа могут иметь версии.
      /// </summary>
      ObjectVersionModes Versionable { get; }

      /// <summary>Комментарии</summary>
      string Note { get; }

      /// <summary>
      /// Ид. типа связи, который показывается по умолчанию в дереве универсального
      /// клиента для объектов данного типа.
      /// </summary>
      int DefaultRelation { get; }

      /// <summary>
      /// Идентификатор типа объектов, от которого унаследован данный тип
      /// </summary>
      int ParentTypeID { get; }

      /// <summary>
      /// Ид. атрибута, который используется для отображения данного типа объектов
      /// в списках.
      /// </summary>
      int CaptionAttribute { get; }

      /// <summary>
      /// Наследует ли тип объектов схему жизненного цикла от родительского объекта или схема собственная
      /// </summary>
      InheritModes PublicLC { get; }

      /// <summary>
      /// Структура, позволяющая прочитать или записать сразу все параметры типа объектов.
      /// </summary>
      ObjectTypeProperties PropertiesStructure { get; }

      /// <summary>
      /// Возвращает хэш ид_типа_объекта=ид_типа_связи всех неабстрактных типов объектов, которые можно включать
      /// в состав объектов данного типа (во завернул)
      /// </summary>
      Dictionary<int, int> GetPossibleChildren();

      /// <summary>
      /// Количество дней, в течение которых нельзя физически уничтожать удаленные объекты данного типа
      /// (время жизни удаленных объектов).
      /// </summary>
      int LifetimeReserve { get; }

      /// <summary>
      /// Опции (содержат битовые флаги для управления свойствами типа объектов)
      /// </summary>
      ObjectTypeOptions Options { get; }

      /// <summary>Идентификатор схемы ЖЦ для объектов данного типа</summary>
      int SchemaID { get; }

      /// <summary>Является ли данный тип объектов локальным</summary>
      bool IsLocalType { get; }

      /// <summary>
      /// Возвращает true, если у данного типа могут быть исходящие связи любого типа
      /// </summary>
      /// <returns></returns>
      bool HasPossibleChildren();

      /// <summary>Гуид типа объектов</summary>
      Guid GUID { get; }
    }
}
