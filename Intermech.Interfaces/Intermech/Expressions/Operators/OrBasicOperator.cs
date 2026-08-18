
// Type: Intermech.Expressions.Operators.OrBasicOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Expressions.Operators
{
    /// <summary>OrBasic operator.</summary>
    internal class OrBasicOperator : OrOperator
    {
      internal override OperatorType GetOperatorType() => OperatorType.orBasicOperator;

      public override string Name => "OR";
    }
}
