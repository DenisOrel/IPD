
// Type: Intermech.Expressions.Operators.PowerOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>Power operator.</summary>
    internal class PowerOperator : Operator
    {
      public override object Evaluate(object[] values)
      {
        double x = Convert.ToDouble(values[0]);
        double y = Convert.ToDouble(values[1]);
        return !x.Equals(0.0) || y >= 0.0 ? (object) Math.Pow(x, y) : throw new DivisionByZeroException();
      }

      internal override OperatorType GetOperatorType() => OperatorType.powerOperator;

      public override string Name => "^";
    }
}
