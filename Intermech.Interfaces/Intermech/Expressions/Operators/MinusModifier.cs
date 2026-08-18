
// Type: Intermech.Expressions.Operators.MinusModifier
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>Minus modifier.</summary>
    internal class MinusModifier : Operator
    {
      public override object Evaluate(object[] values) => (object) -Convert.ToDouble(values[0]);

      internal override OperatorType GetOperatorType() => OperatorType.minusModifier;

      public override string Name => "-";

      public override byte OperandsSupported => 1;
    }
}
