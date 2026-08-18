
// Type: Intermech.Expressions.Functions.IIFFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>IIf function.</summary>
    public class IIFFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        return !Convert.ToBoolean(values[0]) ? values[2] : values[1];
      }

      public override Type GetReturnType(Type[] types) => types[1];

      protected override bool InputTypeSupported(Type type, int index)
      {
        bool flag = false;
        switch (index)
        {
          case 0:
            return type.Equals(typeof (bool));
          case 1:
          case 2:
            return true;
          default:
            return flag;
        }
      }

      public override bool IsNullable(object[] values) => Convert.IsDBNull(values[0]);

      public override bool MultArgsSupported(int count) => count == 3;

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        if (base.Validate(types, ref invalidArgument))
        {
          if (ExpTypeConverter.CanConvert(types[1], types[2]) || ExpTypeConverter.CanConvert(types[2], types[1]) || types[1].Equals(typeof (DBNull)) || types[2].Equals(typeof (DBNull)))
            return true;
          invalidArgument = 2;
        }
        return false;
      }

      public override string Name => "IIF";

      public override FunctionCategory Category => FunctionCategory.Logical;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_657");
    }
}
