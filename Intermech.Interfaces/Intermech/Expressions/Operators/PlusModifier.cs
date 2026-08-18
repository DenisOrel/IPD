
// Type: Intermech.Expressions.Operators.PlusModifier
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Expressions.Operators
{
    /// <summary>Plus modifier.</summary>
    internal class PlusModifier : Operator
    {
      public override object Evaluate(object[] values) => values[0];

      internal override OperatorType GetOperatorType() => OperatorType.plusModifier;

      public override string Name => "+";

      public override byte OperandsSupported => 1;
    }
}
