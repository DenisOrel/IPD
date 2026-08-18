
// Type: Intermech.Interfaces.IDBAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для работы с атрибутом</summary>
    public interface IDBAttribute : IDBSessionable
    {
      /// <summary>Имя атрибута (только для чтения)</summary>
      string Name { get; }

      /// <summary>Идентификатор атрибута (только для чтения).</summary>
      int AttributeID { get; }

      /// <summary>
      /// Идентификатор объекта или связи, атрибутом которых он является (только для
      /// чтения).
      /// </summary>
      long DBObjectID { get; }

      /// <summary>
      /// Идентификатор объекта, атрибутом которых он является (можно вызывать только для атрибутов объектов).
      /// </summary>
      long DB_ID { get; }

      /// <summary>
      /// Возвращает true, если атрибут содержит пустое значение (NULL).
      /// </summary>
      bool IsNull { get; }

      /// <summary>
      /// Метод присваивает атрибуту пустое значение (если это допускается данным атрибутом).
      /// </summary>
      void Clear();

      /// <summary>Строковое значение атрибута</summary>
      string AsString { get; set; }

      /// <summary>Целочисленное значение атрибута</summary>
      long AsInteger { get; set; }

      /// <summary>Дробное значение атрибута</summary>
      double AsDouble { get; set; }

      /// <summary>Значение атрибута как дата и время</summary>
      DateTime AsDateTime { get; set; }

      /// <summary>Значение атрибута как логическая величина</summary>
      bool AsBoolean { get; set; }

      /// <summary>Количество значений в списке значений атрибута</summary>
      int ValuesCount { get; }

      /// <summary>
      /// Индекс текущего значения в списке значений. По умолчанию - 0.
      /// </summary>
      int Index { get; set; }

      /// <summary>
      /// Добавляет значение в список значений атрибута (для miltivalued атрибутов) и
      /// возвращает номер добавленного значения в списке. Если newValue != null, то оно
      /// записывается в качестве значения атрибута.
      /// </summary>
      /// <param name="newValue">Номер добавленного значения</param>
      int AddValue(object newValue);

      /// <summary>
      /// Удаляет текущее значение из списка значений атрибута. Последнее значение
      /// удалить нельзя.
      /// </summary>
      int DeleteValue();

      /// <summary>Типа атрибута (только для чтения).</summary>
      FieldTypes DataType { get; }

      /// <summary>Если true - это системный атрибут (только для чтения)</summary>
      bool IsSystem { get; }

      /// <summary>Удалить атрибут</summary>
      /// <param name="DeleteMode">Зарезервировано.</param>
      /// <returns></returns>
      int Delete(long DeleteMode);

      /// <summary>
      /// Копирует значение атрибута sourceAttribute в данный атрибут
      /// </summary>
      void Assign(IDBAttribute sourceAttribute);

      /// <summary>
      /// Возвращает описатель применимости атрибута в данном типе связи или объекта, т.е. его
      /// можно пробовать приводить к типам IDBAttributeType4, IDBAttributeType4Object,
      /// IDBAttributeType4Relation в зависимости от обстоятельств. Но следует учитывать,
      /// что в некоторых случаях он может оставаться только IDBAttributeType.
      /// </summary>
      IDBAttributeType AttributeType { get; }

      /// <summary>
      /// Список значений атрибута (для всяких блобов возвращает null и не дает записывать значения)
      /// </summary>
      object[] Values { get; set; }

      /// <summary>
      /// Значений атрибута с номером Index (для всяких блобов возвращает null и не дает записывать значения)
      /// </summary>
      object Value { get; set; }

      /// <summary>
      /// Возвращает расшифрованное текстовое представление значения атрибута.
      /// </summary>
      string Description { get; }

      /// <summary>
      /// Возвращает расшифрованное текстовое представление всех значений атрибута.
      /// </summary>
      string[] Descriptions { get; }

      /// <summary>
      /// Возвращает таблицу со списком значений, которые может принимать атрибут
      /// </summary>
      DataTable GetPossibleValues();

      /// <summary>Возвращает true, если атрибут нельзя модифицировать</summary>
      bool ReadOnly { get; }

      /// <summary>
      /// Удаляет все значения многозначного атрибута и чистит последнее значение
      /// </summary>
      void ClearValues();

      /// <summary>
      /// Если == true, то это временный атрибут, который присутствует только в памяти
      /// </summary>
      bool TemporaryAttribute { get; }

      /// <summary>
      /// Возвращает имя первой из групп, в которые входит данный атрибут
      /// </summary>
      string GroupName { get; }

      /// <summary>
      /// Возвращает true, если данный атрибут виден в пользовательском интерфейсе
      /// текущей сессии пользователя (по правам доступа и фильтрам)
      /// </summary>
      bool Visible { get; }
    }
}
