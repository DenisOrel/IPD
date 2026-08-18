
// Type: Intermech.Interfaces.IDBAttributeTypeInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для получения информации о типе атрибута</summary>
    public interface IDBAttributeTypeInfo
    {
      /// <summary>Идентификатор атрибута (только для чтения)</summary>
      int AttributeID { get; }

      /// <summary>Опции атрибута (см. описание AttributeOptions)</summary>
      AttributeOptions Options { get; }

      /// <summary>Маска ввода значения атрибута</summary>
      string Mask { get; }

      /// <summary>Имя атрибута</summary>
      string Name { get; }

      /// <summary>Глобальный идентификатор атрибута</summary>
      Guid GUID { get; }

      /// <summary>Короткое имя атрибута</summary>
      string ShortName { get; }

      /// <summary>
      /// Альтернативное имя атрибута (для хранения понятий Техкарда)
      /// </summary>
      string Alias { get; }

      /// <summary>Комментарии</summary>
      string Note { get; }

      /// <summary>Тип атрибута (строковый, числовой, т.д.)</summary>
      FieldTypes AttributeType { get; }

      /// <summary>Значение по умолчанию</summary>
      object DefaultValue { get; }

      /// <summary>Возвращает текстовую расшифровку поля DefaultValue</summary>
      string DefaultValueDescription { get; }

      /// <summary>
      /// Может ли принимать множественные значения и каким образом.
      /// </summary>
      MultiValueModes MultipleValued { get; }

      /// <summary>Способ вычисления параметра.</summary>
      ComputeValueModes Computed { get; }

      /// <summary>
      /// 1. для строковых параметров - максимальная длина строки,
      /// 2. для ссылки на объект - идентификатор типа объекта,
      /// 3. для таблицы из внешней БД - ссылка на объект, описывающий эту БД, таблицу и
      /// поля с ключом и значением.
      /// 4. ид. физ. величины для единиц измерения
      /// </summary>
      long SizeType { get; }

      /// <summary>Возвращает текстовую расшифровку поля SizeType</summary>
      string SizeTypeDescription { get; }

      /// <summary>
      /// Формула вычисления значения поля. Для ссылок на объекты содержит номер атрибута,
      /// значение которого будет показываться методом AsString атрибута.
      /// </summary>
      string Formula { get; }

      /// <summary>Метод контроля уникальности значений атрибута</summary>
      UniqueValueModes UniqueMode { get; }

      /// <summary>Возвращает список допустимых значений</summary>
      DataTable GetPossibleValues();

      object[] GetPossibleValuesArray();

      DataRow[] GetPossibleValuesRows();

      /// <summary>
      /// Свойство позволяет прочитать или изменить несколько свойств атрибута посредством структуры AttributeTypeProperties
      /// </summary>
      AttributeTypeProperties PropertiesStructure { get; }

      /// <summary>Идентификатор уровня продвижения.</summary>
      int LevelID { get; }

      /// <summary>
      /// Возвращает true, если тип newType можно преобразовать к данному типу
      /// </summary>
      bool IsCompatibleType(FieldTypes newType);

      /// <summary>
      /// Имя поля, которое используется для хранения отображаемого значения атрибута
      /// </summary>
      string TextFieldName { get; }

      /// <summary>
      /// Имя поля, которое используется для хранения значения атрибута
      /// </summary>
      string ValueFieldName { get; }

      /// <summary>
      /// Имя поля, которое используется для хранения значения допустимых значений атрибута
      /// </summary>
      string PossibleValueFieldName { get; }

      /// <summary>
      /// Возвращает true, если атрибут может быть в гридах в качестве отображаемого атрибута
      /// </summary>
      bool IsGridable { get; }

      /// <summary>
      /// Возвращает список операторов, применимых к атрибуту данного типа
      /// </summary>
      RelationalOperators[] EnabledOperators { get; }

      /// <summary>
      /// Правило валидации правильности вводимых в атрибут значений
      /// </summary>
      string ValidationRule { get; }

      /// <summary>
      /// Возвращает true, если данный атрибут может быть вычисляемым и участвовать в формулах
      /// вычисления и проверки значений других атрибутов.
      /// </summary>
      bool ComputableAttribute { get; }

      /// <summary>Задает способ оптимизации операций с атрибутом.</summary>
      OptimizationModes OptimizationMode { get; }

      /// <summary>
      /// Возвращает список имен полей в таблицах представления данных для атрибутов этого типа
      /// </summary>
      string[] FieldNames { get; }

      /// <summary>
      /// Возвращает массив идентификаторов групп, в которых входит данный атрибут
      /// </summary>
      int[] GetGroupsList();

      /// <summary>
      /// Хранит ли атрибут содержимое объекта (изменение такого атрибута влияет на дату модификации объекта,
      /// если таковая у объекта имеется)
      /// </summary>
      bool IsContent { get; }

      /// <summary>
      /// Идентификатор атрибута, из которого данный атрибут будет выбирать данные при присвоении
      /// значения мастер-атрибуту
      /// </summary>
      int SourceAttributeID { get; }

      /// <summary>Идентификатор мастер-атрибута для данного атрибута</summary>
      int MasterAttributeID { get; }
    }
}
