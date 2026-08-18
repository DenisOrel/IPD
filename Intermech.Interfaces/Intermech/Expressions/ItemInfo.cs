
// Type: Intermech.Expressions.ItemInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Expressions
{
    /// <summary>
    /// Represents a token item. Tokens are stored in Polish Notation array.
    /// </summary>
    internal struct ItemInfo
    {
      public ItemType type;
      public int position;
      public int index;
      public int paramCount;
      public object itemValue;

      public Function AsFunction => this.itemValue as Function;

      public Operator AsOperator => this.itemValue as Operator;

      public Constant AsConstant => this.itemValue as Constant;

      public Variable AsVariable => this.itemValue as Variable;

      public override string ToString() => this.itemValue != null ? this.itemValue.ToString() : "null";
    }
}
