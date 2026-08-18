
// Type: Intermech.Interfaces.WebPortal.PossibleValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Допустимое значение</summary>
    [Serializable]
    public class PossibleValue
    {
      /// <summary>Имя поля со значением</summary>
      public string ValueFieldName { get; set; }

      /// <summary>Строковая составляющая</summary>
      public string StringValue { get; set; }

      /// <summary>Строковая составляющая</summary>
      public string Description { get; set; }

      /// <summary>Вещественная составляющая</summary>
      public double DoubleValue { get; set; }

      /// <summary>Временная составляющая в InvariantCulture</summary>
      public string DateTimeValue { get; set; }

      /// <summary>Целочисленная составляющая</summary>
      public long IntegerValue { get; set; }
    }
}
