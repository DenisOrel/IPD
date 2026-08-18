
// Type: Intermech.Expressions.Operators.AndOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>And operator.</summary>
    internal class AndOperator : Operator
    {
      public override object Evaluate(object[] values) => AndOperator.staticEvaluate(values);

      internal override OperatorType GetOperatorType() => OperatorType.andOperator;

      public override Type GetReturnType(Type[] types) => typeof (bool);

      public override bool IsNullable(object[] values) => false;

      public static object staticEvaluate(object[] values)
      {
        if (Convert.IsDBNull(values[0]))
        {
          if (!ExpTypeConverter.CanConvert(values[1], typeof (bool)))
            return (object) DBNull.Value;
          return Convert.ToBoolean(values[1]) ? (object) DBNull.Value : (object) false;
        }
        if (!Convert.IsDBNull(values[1]))
          return (object) (bool) (!Convert.ToBoolean(values[0]) ? 0 : (Convert.ToBoolean(values[1]) ? 1 : 0));
        if (!ExpTypeConverter.CanConvert(values[0], typeof (bool)))
          return (object) DBNull.Value;
        return Convert.ToBoolean(values[0]) ? (object) DBNull.Value : (object) false;
      }

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag1 = true;
        if (types[0].Equals(typeof (bool)))
        {
          if (!types[1].Equals(typeof (bool)))
          {
            flag1 = false;
            invalidArgument = 1;
          }
          return flag1;
        }
        bool flag2 = false;
        invalidArgument = 0;
        return flag2;
      }

      public override string Name => "&&";
    }
}
