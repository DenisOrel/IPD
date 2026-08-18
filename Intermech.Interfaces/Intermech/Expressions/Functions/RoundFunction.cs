
// Type: Intermech.Expressions.Functions.RoundFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Round function.</summary>
    public class RoundFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        return values.Length > 1 ? (object) Math.Round(Convert.ToDouble(values[0]), Convert.ToInt32(values[1])) : (object) Math.Round(Convert.ToDouble(values[0]));
      }

      public override bool MultArgsSupported(int count) => count == 1 || count == 2;

      public override string Name => "ROUND";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_680");
    }
}
