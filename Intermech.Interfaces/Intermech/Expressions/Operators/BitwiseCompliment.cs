
// Type: Intermech.Expressions.Operators.BitwiseCompliment
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Expressions.Operators
{
    /// <summary>BitwiseCompliment operator.</summary>
    internal class BitwiseCompliment : Operator
    {
      public override object Evaluate(object[] values) => (object) ~Convert.ToInt64(values[0]);

      internal override OperatorType GetOperatorType() => OperatorType.bitwiseCompliment;

      public override Type GetReturnType(Type[] types) => typeof (int);

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag = true;
        if (!ExpTypeConverter.CanConvert(types[0], typeof (double)) && !ExpTypeConverter.CanConvert(types[0], typeof (long)))
        {
          flag = false;
          invalidArgument = 0;
        }
        return flag;
      }

      public override string Name => "~";

      public override byte OperandsSupported => 1;
    }
}
