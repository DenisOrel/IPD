
// Type: Intermech.Interfaces.MetadataUpdates.UpdateScriptAttributeValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.MetadataUpdates
{
    /// <summary>
    /// Значение атрибута для объекта, прочитанное из скрипта автообновления метаданных.
    /// </summary>
    public sealed class UpdateScriptAttributeValue
    {
      /// <summary>Флаг того, что значение пустое</summary>
      public bool IsEmpty { get; set; }

      /// <summary>Порядковый номер в списке</summary>
      public int InLisID { get; set; }

      /// <summary>Целочисленная составляющая</summary>
      public long IntegerValue { get; set; }

      /// <summary>Вещественная состовляющая</summary>
      public double DoubleValue { get; set; }

      /// <summary>Строковая составляющая</summary>
      public string StringValue { get; set; }

      /// <summary>Временная составляющая</summary>
      public DateTime DateTimeValue { get; set; }

      /// <summary>Дополнительные данные</summary>
      public object Tag { get; set; }

      public UpdateScriptAttributeValue()
      {
        this.IsEmpty = true;
        this.InLisID = 0;
        this.IntegerValue = long.MinValue;
        this.DoubleValue = double.MinValue;
        this.StringValue = string.Empty;
        this.DateTimeValue = DateTime.MinValue;
      }
    }
}
