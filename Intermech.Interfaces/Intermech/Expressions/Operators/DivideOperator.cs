
// Type: Intermech.Expressions.Operators.DivideOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>Divide operator.</summary>
    internal class DivideOperator : Operator
    {
      public override object Evaluate(object[] values)
      {
        double num1 = Convert.ToDouble(values[0]);
        double num2 = Convert.ToDouble(values[1]);
        double num3 = !num2.Equals(0.0) ? num2 : throw new DivisionByZeroException();
        return (object) (num1 / num3);
      }

      internal override OperatorType GetOperatorType() => OperatorType.divideOperator;

      public override string Name => "/";
    }
}
