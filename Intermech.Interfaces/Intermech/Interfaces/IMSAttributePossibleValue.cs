
// Type: Intermech.Interfaces.IMSAttributePossibleValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс, хранящий одно допустимое значение для атрибута
    /// </summary>
    internal sealed class IMSAttributePossibleValue : 
      IAssignable,
      ICloneable,
      IComparable<IMSAttributePossibleValue>
    {
      /// <summary>
      /// Порядковый номер значения в списке допустимых значений атрибута
      /// </summary>
      public int F_INLIST_ID;
      /// <summary>Значение в виде Int32</summary>
      public long F_INTEGER_VALUE;
      /// <summary>Значение в виде String</summary>
      public string F_STRING_VALUE;
      /// <summary>Значение в виде Double</summary>
      public double F_DOUBLE_VALUE;
      /// <summary>Значение в виде DateTime</summary>
      public DateTime F_DATE_VALUE;
      /// <summary>Описание допустимого значения атрибута</summary>
      public object F_DESCRIPTION;

      /// <summary>Создать пустой экземпляр класса</summary>
      public IMSAttributePossibleValue()
      {
      }

      /// <summary>
      /// Создать пустой экземпляр класса, заполнить его информацией из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public IMSAttributePossibleValue(object source) => this.Assign(source);

      /// <summary>
      /// Загрузить информацию из строки таблицы IMS_POSSIBLE_VALUES
      /// </summary>
      /// <param name="row">Строка из таблицы IMS_POSSIBLE_VALUES</param>
      internal void Load(DataRow row)
      {
        this.Clear();
        if (row == null)
          return;
        this.F_INLIST_ID = DataSetProcessor.GetInt32Value(row, "F_INLIST_ID", 0);
        this.F_INTEGER_VALUE = DataSetProcessor.GetInt64Value(row, "F_INTEGER_VALUE", 0L);
        this.F_STRING_VALUE = DataSetProcessor.GetStringValue(row, "F_STRING_VALUE", string.Empty);
        this.F_DOUBLE_VALUE = DataSetProcessor.GetDoubleValue(row, "F_DOUBLE_VALUE", 0.0);
        this.F_DATE_VALUE = DataSetProcessor.GetDateTimeValue(row, "F_DATE_VALUE", DateTime.MinValue);
        this.F_DESCRIPTION = row["F_DESCRIPTION"];
      }

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this.F_INLIST_ID = 0;
        this.F_INTEGER_VALUE = 0L;
        this.F_STRING_VALUE = string.Empty;
        this.F_DOUBLE_VALUE = 0.0;
        this.F_DATE_VALUE = DateTime.MinValue;
        this.F_DESCRIPTION = (object) DBNull.Value;
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        switch (source)
        {
          case IMSAttributePossibleValue attributePossibleValue:
            this.F_INLIST_ID = attributePossibleValue.F_INLIST_ID;
            this.F_INTEGER_VALUE = attributePossibleValue.F_INTEGER_VALUE;
            this.F_STRING_VALUE = attributePossibleValue.F_STRING_VALUE;
            this.F_DOUBLE_VALUE = attributePossibleValue.F_DOUBLE_VALUE;
            this.F_DATE_VALUE = attributePossibleValue.F_DATE_VALUE;
            this.F_DESCRIPTION = attributePossibleValue.F_DESCRIPTION;
            break;
          case DataRow row:
            this.Load(row);
            break;
        }
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new IMSAttributePossibleValue((object) this);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты равны</returns>
      public override bool Equals(object obj)
      {
        return obj is IMSAttributePossibleValue attributePossibleValue && this.F_INLIST_ID == attributePossibleValue.F_INLIST_ID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.F_INLIST_ID.GetHashCode();

      /// <summary>Вернуть строковое представление значения</summary>
      /// <returns>Строковое представление значения</returns>
      public override string ToString()
      {
        return $"[{this.F_INLIST_ID}] \"{this.F_DESCRIPTION}\" [{this.F_INTEGER_VALUE}] [{this.F_STRING_VALUE}] [{this.F_DOUBLE_VALUE}] [{this.F_DATE_VALUE}]";
      }

      /// <summary>Сравнить с другим объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(IMSAttributePossibleValue other)
      {
        return other == null ? 1 : this.F_INLIST_ID.CompareTo(other.F_INLIST_ID);
      }
    }
}
