
// Type: Intermech.Kernel.Search.ConditionFormula
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Класс, содержащий формулу для подстановки в качестве значения в ConditionStructure. Предназначен для создания условий, где в качестве значений можно подставлять ссылки на другие атрибуты
    /// этого же объекта (либо формулы с ссылками на атрибуты)
    /// </summary>
    [TypeConverter(typeof (ToBase64StringTypeConverter<ConditionFormula>))]
    [Serializable]
    public class ConditionFormula : ICloneable
    {
      /// <summary>Параметры для подстановки в SQL-запрос</summary>
      public DBDataParam[] Values { get; private set; }

      public ConditionFormula(string value, params DBDataParam[] values)
      {
        this.Formula = value;
        this.Values = values;
      }

      /// <summary>
      /// Строковое представление значения (как оно записано в выборке)
      /// </summary>
      public string Formula { get; private set; }

      public object Clone()
      {
        return this.Values == null ? (object) new ConditionFormula(this.Formula, Array.Empty<DBDataParam>()) : (object) new ConditionFormula(this.Formula, (DBDataParam[]) this.Values.Clone());
      }
    }
}
