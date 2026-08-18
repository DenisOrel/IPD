
// Type: Intermech.Expressions.Operators.BitwiseAndOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>
    /// BitwiseAnd operator. ( + String Concatenation operator)
    /// </summary>
    internal class BitwiseAndOperator : Operator
    {
      public override object Evaluate(object[] values)
      {
        return (object) (Convert.ToInt64(values[0]) & Convert.ToInt64(values[1]));
      }

      internal override OperatorType GetOperatorType() => OperatorType.bitwiseAndOperator;

      public override Type GetReturnType(Type[] types) => typeof (int);

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        if (!ExpTypeConverter.CanConvert(types[0], typeof (long)))
        {
          invalidArgument = 0;
          return false;
        }
        if (ExpTypeConverter.CanConvert(types[1], typeof (long)))
          return true;
        invalidArgument = 1;
        return false;
      }

      public override string Name => "&";
    }
}
