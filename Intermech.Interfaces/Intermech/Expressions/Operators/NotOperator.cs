
// Type: Intermech.Expressions.Operators.NotOperator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>Not operator.</summary>
    internal class NotOperator : Operator
    {
      public override object Evaluate(object[] values) => (object) !Convert.ToBoolean(values[0]);

      internal override OperatorType GetOperatorType() => OperatorType.notOperator;

      public override Type GetReturnType(Type[] types) => typeof (bool);

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag = true;
        if (!types[0].Equals(typeof (bool)))
        {
          flag = false;
          invalidArgument = 0;
        }
        return flag;
      }

      public override string Name => "!";

      public override byte OperandsSupported => 1;
    }
}
