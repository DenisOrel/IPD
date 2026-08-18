
// Type: Intermech.Expressions.Operators.MultiplyOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>Multiply operator.</summary>
    internal class MultiplyOperator : Operator
    {
      public override object Evaluate(object[] values)
      {
        double result1 = 0.0;
        double result2 = 0.0;
        if (ExpTypeConverter.CanConvert(values[0], typeof (double)) && ExpTypeConverter.CanConvert(values[1], typeof (double)))
          return (object) (Convert.ToDouble(values[0]) * Convert.ToDouble(values[1]));
        return double.TryParse(values[0].ToString(), out result1) && double.TryParse(values[1].ToString(), out result2) ? (object) (result1 * result2) : (object) (Convert.ToInt64(values[0]) * Convert.ToInt64(values[1]));
      }

      internal override OperatorType GetOperatorType() => OperatorType.multiplyOperator;

      public override string Name => "*";
    }
}
