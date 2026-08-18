
// Type: Intermech.Expressions.Functions.DateFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Date function.</summary>
    public class DateFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        return values.Length == 3 ? (object) new DateTime(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[2])) : (object) new DateTime(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), Convert.ToInt32(values[3]), Convert.ToInt32(values[4]), Convert.ToInt32(values[5]));
      }

      public override Type GetReturnType(Type[] types) => typeof (DateTime);

      public override bool MultArgsSupported(int count) => count == 3 || count == 6;

      public override string Name => "DATE";

      public override FunctionCategory Category => FunctionCategory.Date;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_641");
    }
}
