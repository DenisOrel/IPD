
// Type: Intermech.Interfaces.IDBObjectType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;


namespace Intermech.Interfaces
{
    public interface IDBObjectType : IDBAttributableType
    {
      /// <summary>Идентификатор типа объекта (только для чтения)</summary>
      int ObjectType { get; }

      /// <summary>Наименование типа объектов</summary>
      string ObjectTypeName { get; set; }

      /// <summary>Краткое наименование типа объектов</summary>
      string ObjectTypeShortName { get; set; }

      /// <summary>Наименование объекта данного типа (например, Деталь)</summary>
      string ObjectInstanceName { get; set; }

      /// <summary>Иконка для отображения типа объектов</summary>
      byte[] Icon { get; set; }

      /// <summary>
      /// 0 - абстрактный тип объекта (контейнер для группировки других типов объектов);
      /// 1 - объекты данного типа не могут иметь версий;
      /// 2 - объекты данного типа могут иметь версии.
      /// </summary>
      ObjectVersionModes Versionable { get; set; }

      /// <summary>Комментарии</summary>
      string Note { get; set; }

      /// <summary>
      /// Ид. типа связи, который показывается по умолчанию в дереве универсального
      /// клиента для объектов данного типа.
      /// </summary>
      int DefaultRelation { get; set; }

      /// <summary>Удалить тип объектов</summary>
      /// <param name="DeleteMode"></param>
      /// <returns></returns>
      int Delete(long DeleteMode);

      /// <summary>
      /// Добавить в список дочерних типов объектов типы с номерами objectTypes.
      /// </summary>
      int IncludeObjectType(params int[] objectTypes);

      /// <summary>
      /// Идентификатор типа объектов, от которого унаследован данный тип
      /// </summary>
      int ParentTypeID { get; set; }

      /// <summary>
      /// Ид. атрибута, который используется для отображения данного типа объектов
      /// в списках.
      /// </summary>
      int CaptionAttribute { get; set; }

      /// <summary>
      /// Наследует ли тип объектов схему жизненного цикла от родительского объекта или схема собственная
      /// </summary>
      InheritModes PublicLC { get; set; }

      /// <summary>
      /// Структура, позволяющая прочитать или записать сразу все параметры типа объектов.
      /// </summary>
      ObjectTypeProperties PropertiesStructure { get; set; }

      /// <summary>
      /// Возвращает хэш ид_типа_объекта=ид_типа_связи всех неабстрактных типов объектов, которые можно включать
      /// в состав объектов данного типа (во завернул)
      /// </summary>
      Hashtable GetPossibleChildren();

      /// <summary>
      /// Возвращает хэш ид_типа_объекта=ид_типа_связи всех типов объектов, которые можно включать
      /// в состав объектов данного типа. Абстрактные дочерние типы объектов так же попадают в этот список
      /// </summary>
      Hashtable GetAllChildren();

      /// <summary>
      /// Пересоздает таблицу-представление данных для этого типа объектов
      /// </summary>
      void RebuildView();

      /// <summary>
      /// Количество дней, в течение которых нельзя физически уничтожать удаленные объекты данного типа
      /// (время жизни удаленных объектов).
      /// </summary>
      int LifetimeReserve { get; set; }

      /// <summary>
      /// Опции (содержат битовые флаги для управления свойствами типа объектов)
      /// </summary>
      ObjectTypeOptions Options { get; set; }

      /// <summary>Идентификатор схемы ЖЦ для объектов данного типа</summary>
      int SchemaID { get; set; }

      /// <summary>Является ли данный тип объектов локальным</summary>
      bool IsLocalType { get; }

      /// <summary>
      /// Возвращает количество неудаленных объектов и итераций объектов данного типа
      /// </summary>
      /// <param name="objectsCount">Количество неудалённые объектов</param>
      /// <param name="snapshotsCount">Количество итераций</param>
      void GetObjectsInfo(out int objectsCount, out int snapshotsCount);

      /// <summary>
      /// Заполняет массив objsTreeList идентификаторами дочерних к данному типу объектов
      /// </summary>
      /// <param name="objsTreeList">Массив целочисленных ид. типов объектов</param>
      void FillChildrenList(ArrayList objsTreeList);

      /// <summary>
      /// Имя таблицы-представления данных для получения списков объектов данного типа
      /// </summary>
      string ViewName { get; }

      /// <summary>
      /// Имя таблицы атрибутов данного типа объектов (работает для всех типов, а не только локальных)
      /// </summary>
      string AttributesTableName { get; }
    }
}
