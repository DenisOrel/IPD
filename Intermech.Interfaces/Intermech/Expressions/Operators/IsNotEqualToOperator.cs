
// Type: Intermech.Expressions.Operators.IsNotEqualToOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>Inequality ("!=") operator.</summary>
    internal class IsNotEqualToOperator : Operator
    {
      public override object Evaluate(object[] values, bool caseSensitive)
      {
        if (ExpTypeConverter.CanConvert(values[0], typeof (double)) && ExpTypeConverter.CanConvert(values[1], typeof (double)))
          return (object) !Convert.ToDouble(values[0]).Equals(Convert.ToDouble(values[1]));
        return ExpTypeConverter.GetValueType(values[0]).Equals(typeof (string)) ? (object) (string.Compare(Convert.ToString(values[0]), Convert.ToString(values[1]), !caseSensitive) != 0) : (object) !values[0].Equals(values[1]);
      }

      internal override OperatorType GetOperatorType() => OperatorType.isNotEqualToOperator;

      public override Type GetReturnType(Type[] types) => typeof (bool);

      public override bool IsNullable(object[] values)
      {
        return ExpressionTree.ANSINulls && base.IsNullable(values);
      }

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag = true;
        if (ExpTypeConverter.CanConvert(types[0], typeof (double)))
        {
          if (!ExpTypeConverter.CanConvert(types[1], typeof (double)))
          {
            flag = false;
            invalidArgument = 1;
          }
          return flag;
        }
        if (!types[0].Equals(types[1]))
        {
          flag = false;
          invalidArgument = 1;
        }
        return flag;
      }

      public override string Name => "!=";
    }
}
