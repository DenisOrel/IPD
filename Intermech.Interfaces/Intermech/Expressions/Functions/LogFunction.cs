
// Type: Intermech.Expressions.Functions.LogFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Log function.</summary>
    public class LogFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double d1 = Convert.ToDouble(values[0]);
        double d2 = Convert.ToDouble(values[1]);
        if (d1 < 0.0)
          throw new LogNegNumberException();
        if (d1.Equals(0.0))
          throw new LogZeroException();
        if (d2 < 0.0)
          throw new LogNegBaseException();
        if (d2.Equals(0.0))
          throw new LogZeroBaseException();
        if (d2.Equals(1.0))
          throw new LogBaseEq1Exception();
        return (object) (Math.Log10(d1) / Math.Log10(d2));
      }

      public override bool MultArgsSupported(int count) => count == 2;

      public override string Name => "LOG";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_667");
    }
}
