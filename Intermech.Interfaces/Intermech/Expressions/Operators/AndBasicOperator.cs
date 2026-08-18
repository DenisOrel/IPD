
// Type: Intermech.Expressions.Operators.AndBasicOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Expressions.Operators
{
    /// <summary>AndBasic operator.</summary>
    internal class AndBasicOperator : AndOperator
    {
      internal override OperatorType GetOperatorType() => OperatorType.andBasicOperator;

      public override string Name => "AND";
    }
}
