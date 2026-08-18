
// Type: Intermech.Expressions.Operators.ShiftRightOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>Power operator.</summary>
    internal class ShiftRightOperator : Operator
    {
      public override object Evaluate(object[] values)
      {
        return (object) (Convert.ToInt64(values[0]) >> Convert.ToInt32(values[1]));
      }

      internal override OperatorType GetOperatorType() => OperatorType.shiftRightOperator;

      public override string Name => ">>";
    }
}
