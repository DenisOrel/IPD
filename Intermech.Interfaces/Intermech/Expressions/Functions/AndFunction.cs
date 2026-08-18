
// Type: Intermech.Expressions.Functions.AndFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Operators;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>And function.</summary>
    public class AndFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        object[] values1 = new object[2]{ values[0], null };
        for (int index = 1; index < values.Length; ++index)
        {
          values1[1] = values[index];
          values1[0] = AndOperator.staticEvaluate(values1);
        }
        return values1[0];
      }

      public override Type GetReturnType(Type[] types) => typeof (bool);

      public override bool IsNullable(object[] values) => false;

      public override bool MultArgsSupported(int count) => count > 0;

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag1 = true;
        if (ExpTypeConverter.CanConvert(types[0], typeof (bool)))
        {
          for (int index = 1; index <= types.GetUpperBound(0); ++index)
          {
            if (!ExpTypeConverter.CanConvert(types[index], typeof (bool)))
            {
              flag1 = false;
              invalidArgument = index;
              break;
            }
          }
          return flag1;
        }
        if (types.GetUpperBound(0) == 0 && types[0].IsArray)
        {
          if (!ExpTypeConverter.CanConvert(types[0].GetElementType(), typeof (bool)))
          {
            flag1 = false;
            invalidArgument = 0;
          }
          return flag1;
        }
        bool flag2 = false;
        invalidArgument = 0;
        return flag2;
      }

      public override string Name => "AND";

      public override FunctionCategory Category => FunctionCategory.Logical;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_626");
    }
}
