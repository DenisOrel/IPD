
// Type: Intermech.Expressions.Operators.PlusOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>Plus operator.</summary>
    internal class PlusOperator : Operator
    {
      public override object Evaluate(object[] values)
      {
        if (ExpTypeConverter.CanConvert(values[0], typeof (double)) && ExpTypeConverter.CanConvert(values[1], typeof (double)))
          return (object) (Convert.ToDouble(values[0]) + Convert.ToDouble(values[1]));
        return ExpTypeConverter.CanConvert(values[0], typeof (long)) && ExpTypeConverter.CanConvert(values[1], typeof (long)) ? (object) (Convert.ToInt64(values[0]) + Convert.ToInt64(values[1])) : (object) (values[0].ToString() + values[1].ToString());
      }

      internal override OperatorType GetOperatorType() => OperatorType.plusOperator;

      public override Type GetReturnType(Type[] types)
      {
        return ExpTypeConverter.CanConvert(types[0], typeof (double)) && ExpTypeConverter.CanConvert(types[1], typeof (double)) ? typeof (double) : typeof (string);
      }

      public override bool IsNullable(object[] values)
      {
        bool flag1 = false;
        bool flag2 = false;
        int length = values.Length;
        for (int index = 0; index < length; ++index)
        {
          if (Convert.IsDBNull(values[index]))
            flag1 = true;
          else if (values[index] != null && values[index].GetType().Equals(typeof (string)))
            flag2 = true;
        }
        return !flag2 && flag1;
      }

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag1 = ExpTypeConverter.CanConvert(types[0], typeof (double));
        if (!flag1)
          flag1 = ExpTypeConverter.CanConvert(types[0], typeof (long));
        bool flag2 = ExpTypeConverter.CanConvert(types[1], typeof (double));
        if (!flag2)
          flag2 = ExpTypeConverter.CanConvert(types[1], typeof (long));
        if (flag1 & flag2 || types[0].Equals(typeof (string)) & types[1].Equals(typeof (string)) || ExpTypeConverter.CanConvert(types[0], typeof (double)) & types[1].Equals(typeof (string)) || types[0].Equals(typeof (string)) & ExpTypeConverter.CanConvert(types[1], typeof (double)))
          return true;
        invalidArgument = 0;
        return false;
      }

      public override string Name => "+";
    }
}
