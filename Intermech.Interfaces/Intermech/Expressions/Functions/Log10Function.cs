
// Type: Intermech.Expressions.Functions.Log10Function
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Log10 function.</summary>
    public class Log10Function : Function
    {
      public override object Evaluate(object[] values)
      {
        double d = Convert.ToDouble(values[0]);
        if (d < 0.0)
          throw new Log10NegNumberException();
        return !d.Equals(0.0) ? (object) Math.Log10(d) : throw new Log10ZeroException();
      }

      public override string Name => "LOG10";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_666");
    }
}
