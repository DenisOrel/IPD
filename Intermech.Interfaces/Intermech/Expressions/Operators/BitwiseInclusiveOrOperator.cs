
// Type: Intermech.Expressions.Operators.BitwiseInclusiveOrOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>BitwiseInclusiveOr operator.</summary>
    internal class BitwiseInclusiveOrOperator : Operator
    {
      public override object Evaluate(object[] values)
      {
        return (object) (Convert.ToInt64(values[0]) | Convert.ToInt64(values[1]));
      }

      internal override OperatorType GetOperatorType() => OperatorType.bitwiseInclusiveOrOperator;

      public override Type GetReturnType(Type[] types) => typeof (int);

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag1 = true;
        if (ExpTypeConverter.CanConvert(types[0], typeof (double)))
        {
          if (!ExpTypeConverter.CanConvert(types[1], typeof (double)))
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

      public override string Name => "|";
    }
}
