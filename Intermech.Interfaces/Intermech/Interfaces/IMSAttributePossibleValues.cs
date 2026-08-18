
// Type: Intermech.Interfaces.IMSAttributePossibleValues
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс, хранящий допустимые значения для атрибута
    /// </summary>
    internal sealed class IMSAttributePossibleValues : IAssignable, ICloneable
    {
      /// <summary>Идентификатор атрибута</summary>
      public int F_ATTRIBUTE_ID;
      /// <summary>Идентификатор типа объекта</summary>
      public int F_OBJECT_TYPE = -1;
      /// <summary>Идентификатор типа связи</summary>
      public int F_RELATION_TYPE = -1;
      /// <summary>Список допустимых значений</summary>
      private List<IMSAttributePossibleValue> _possibleValues = new List<IMSAttributePossibleValue>();

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="attributeID">Идентификатор атрибута</param>
      /// <param name="objectType">Идентификатор типа объекта</param>
      /// <param name="relationType">Идентификатор типа связи</param>
      public IMSAttributePossibleValues(int attributeID, int objectType, int relationType)
      {
        this.F_ATTRIBUTE_ID = attributeID;
        this.F_OBJECT_TYPE = objectType;
        this.F_RELATION_TYPE = relationType;
      }

      /// <summary>
      /// Создать пустой экземпляр класса, заполнить его информацией из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public IMSAttributePossibleValues(object source) => this.Assign(source);

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this.F_ATTRIBUTE_ID = 0;
        this.F_OBJECT_TYPE = -1;
        this.F_RELATION_TYPE = -1;
        this._possibleValues.Clear();
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        if (!(source is IMSAttributePossibleValues attributePossibleValues))
          return;
        this.F_ATTRIBUTE_ID = attributePossibleValues.F_ATTRIBUTE_ID;
        this.F_OBJECT_TYPE = attributePossibleValues.F_OBJECT_TYPE;
        this.F_RELATION_TYPE = attributePossibleValues.F_RELATION_TYPE;
        for (int index = 0; index < attributePossibleValues._possibleValues.Count; ++index)
          this._possibleValues.Add(attributePossibleValues._possibleValues[index].Clone() as IMSAttributePossibleValue);
        this._possibleValues.Sort();
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new IMSAttributePossibleValues((object) this);

      /// <summary>Количество допустимых значений</summary>
      public int Count
      {
        [DebuggerStepThrough] get => this._possibleValues.Count;
      }

      /// <summary>По имени поля получить список допустимых значений</summary>
      /// <param name="fieldName">Имя поля таблицы IMS_POSSIBLE_VALUES</param>
      /// <returns>Список допустимых значений или null</returns>
      public List<object> this[string fieldName]
      {
        get
        {
          switch (fieldName)
          {
            case "F_INTEGER_VALUE":
              return this.PossibleInt64Values;
            case "F_STRING_VALUE":
              return this.PossibleStringValues;
            case "F_DOUBLE_VALUE":
              return this.PossibleDoubleValues;
            case "F_DATE_VALUE":
              return this.PossibleDateTimeValues;
            default:
              return (List<object>) null;
          }
        }
      }

      /// <summary>
      /// По указанному индексу и имени поля получить допустимое значение
      /// </summary>
      /// <param name="F_INLIST_ID">Индекс</param>
      /// <param name="fieldName">Имя поля таблицы IMS_POSSIBLE_VALUES</param>
      /// <returns>Допустимое значение или null</returns>
      public object this[int F_INLIST_ID, string fieldName]
      {
        get
        {
          IMSAttributePossibleValue possibleValue = this._possibleValues[F_INLIST_ID];
          switch (fieldName)
          {
            case "F_INTEGER_VALUE":
              return (object) possibleValue.F_INTEGER_VALUE;
            case "F_STRING_VALUE":
              return (object) possibleValue.F_STRING_VALUE;
            case "F_DOUBLE_VALUE":
              return (object) possibleValue.F_DOUBLE_VALUE;
            case "F_DATE_VALUE":
              return (object) possibleValue.F_DATE_VALUE;
            case nameof (F_INLIST_ID):
              return (object) possibleValue.F_INLIST_ID;
            default:
              return possibleValue.F_DESCRIPTION;
          }
        }
      }

      /// <summary>Список описаний допустимых значений или null</summary>
      public List<object> Descriptions
      {
        get
        {
          if (this._possibleValues.Count == 0)
            return (List<object>) null;
          List<object> descriptions = new List<object>(this._possibleValues.Count);
          for (int index = 0; index < this._possibleValues.Count; ++index)
            descriptions.Add(this._possibleValues[index].F_DESCRIPTION);
          return descriptions;
        }
      }

      /// <summary>Список допустимых Int64-значений или null</summary>
      private List<object> PossibleInt64Values
      {
        get
        {
          if (this._possibleValues.Count == 0)
            return (List<object>) null;
          List<object> possibleInt64Values = new List<object>(this._possibleValues.Count);
          for (int index = 0; index < this._possibleValues.Count; ++index)
            possibleInt64Values.Add((object) this._possibleValues[index].F_INTEGER_VALUE);
          return possibleInt64Values;
        }
      }

      /// <summary>Список допустимых String-значений или null</summary>
      private List<object> PossibleStringValues
      {
        get
        {
          if (this._possibleValues.Count == 0)
            return (List<object>) null;
          List<object> possibleStringValues = new List<object>(this._possibleValues.Count);
          for (int index = 0; index < this._possibleValues.Count; ++index)
            possibleStringValues.Add((object) this._possibleValues[index].F_STRING_VALUE);
          return possibleStringValues;
        }
      }

      /// <summary>Список допустимых Double-значений или null</summary>
      private List<object> PossibleDoubleValues
      {
        get
        {
          if (this._possibleValues.Count == 0)
            return (List<object>) null;
          List<object> possibleDoubleValues = new List<object>(this._possibleValues.Count);
          for (int index = 0; index < this._possibleValues.Count; ++index)
            possibleDoubleValues.Add((object) this._possibleValues[index].F_DOUBLE_VALUE);
          return possibleDoubleValues;
        }
      }

      /// <summary>Список допустимых DateTime-значений или null</summary>
      private List<object> PossibleDateTimeValues
      {
        get
        {
          if (this._possibleValues.Count == 0)
            return (List<object>) null;
          List<object> possibleDateTimeValues = new List<object>(this._possibleValues.Count);
          for (int index = 0; index < this._possibleValues.Count; ++index)
            possibleDateTimeValues.Add((object) this._possibleValues[index].F_DATE_VALUE);
          return possibleDateTimeValues;
        }
      }

      /// <summary>
      /// Выполнить загрузку информации о допустимых значениях атрибутов из таблицы
      /// </summary>
      /// <param name="table">Таблица IMS_POSSIBLE_VALUES</param>
      /// <returns>Список допустимых значений для атрибутов</returns>
      internal static Dictionary<int, IMSAttributePossibleValues> LoadFromDataTable(DataTable table)
      {
        Dictionary<int, IMSAttributePossibleValues> dictionary = new Dictionary<int, IMSAttributePossibleValues>(0);
        if (table == null)
          return dictionary;
        int count = table.Rows.Count;
        for (int index = 0; index < count; ++index)
        {
          DataRow row = table.Rows[index];
          int int32Value1 = DataSetProcessor.GetInt32Value(row, "F_ATTRIBUTE_ID", 0);
          int int32Value2 = DataSetProcessor.GetInt32Value(row, "F_OBJECT_TYPE", 0);
          int int32Value3 = DataSetProcessor.GetInt32Value(row, "F_RELATION_TYPE", 0);
          if (int32Value1 != 0)
          {
            IMSAttributePossibleValue attributePossibleValue = new IMSAttributePossibleValue((object) row);
            IMSAttributePossibleValues attributePossibleValues;
            if (!dictionary.ContainsKey(int32Value1))
            {
              attributePossibleValues = new IMSAttributePossibleValues(int32Value1, int32Value2, int32Value3);
              dictionary[int32Value1] = attributePossibleValues;
            }
            else
              attributePossibleValues = dictionary[int32Value1];
            attributePossibleValues._possibleValues.Add(attributePossibleValue);
          }
        }
        foreach (KeyValuePair<int, IMSAttributePossibleValues> keyValuePair in dictionary)
          keyValuePair.Value._possibleValues.Sort();
        return dictionary;
      }
    }
}
