
// Type: Intermech.Expressions.Functions.AverageFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Average function.</summary>
    public class AverageFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double num = 0.0;
        for (int index = 0; index <= values.GetUpperBound(0); ++index)
          num = (num * (double) index + Convert.ToDouble(values.GetValue(index))) / (double) (index + 1);
        return (object) num;
      }

      public override bool MultArgsSupported(int count) => count > 0;

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag1 = true;
        if (ExpTypeConverter.CanConvert(types[0], typeof (double)))
        {
          for (int index = 1; index <= types.GetUpperBound(0); ++index)
          {
            if (!ExpTypeConverter.CanConvert(types[index], typeof (double)))
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
          if (!ExpTypeConverter.CanConvert(types[0].GetElementType(), typeof (double)))
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

      public override string Name => "AVERAGE";

      public override FunctionCategory Category => FunctionCategory.Other;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_633");
    }
}
