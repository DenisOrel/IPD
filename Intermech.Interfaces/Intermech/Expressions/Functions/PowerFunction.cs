
// Type: Intermech.Expressions.Functions.PowerFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Power function.</summary>
    public class PowerFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double x = Convert.ToDouble(values[0]);
        double y = Convert.ToDouble(values[1]);
        return !x.Equals(0.0) || y >= 0.0 ? (object) Math.Pow(x, y) : throw new DivisionByZeroException();
      }

      public override bool MultArgsSupported(int count) => count == 2;

      public override string Name => "POWER";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_678");
    }
}
