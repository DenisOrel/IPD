
// Type: Intermech.Interfaces.AttributeDataTableValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура для хранения всех составляющих значения атрибута
    /// </summary>
    public struct AttributeDataTableValue
    {
      /// <summary>Строковая часть</summary>
      public object StringField;
      /// <summary>Целочисленная часть</summary>
      public object IntegerField;
      /// <summary>Дробная часть</summary>
      public object DoubleField;
      /// <summary>Прочие данные</summary>
      public object DateField;
      /// <summary>Индекс значения</summary>
      public object InlistIDField;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="row">Строка с данными</param>
      public AttributeDataTableValue(HybridRow row)
      {
        this.StringField = row["F_STRING_VALUE"];
        this.IntegerField = row["F_INTEGER_VALUE"];
        this.DoubleField = row["F_DOUBLE_VALUE"];
        this.DateField = row["F_DATE_VALUE"];
        this.InlistIDField = row["F_INLIST_ID"];
      }

      /// <summary>Строковая часть</summary>
      public string StringValue => this.StringField.ToString();

      /// <summary>Целочисленная часть</summary>
      public long IntegerValue => Convert.ToInt64(this.IntegerField);

      /// <summary>Дробная часть</summary>
      public double DoubleValue => Convert.ToDouble(this.DoubleField);

      /// <summary>Дата и время</summary>
      public DateTime DateValue => Convert.ToDateTime(this.DateField);

      /// <summary>Индекс значения</summary>
      public int InlistID => Convert.ToInt32(this.InlistIDField);
    }
}
