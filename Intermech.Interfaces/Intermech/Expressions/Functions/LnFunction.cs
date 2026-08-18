
// Type: Intermech.Expressions.Functions.LnFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Ln function.</summary>
    public class LnFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double d = Convert.ToDouble(values[0]);
        if (d < 0.0)
          throw new LnNegNumberException();
        return !d.Equals(0.0) ? (object) Math.Log(d) : throw new LnZeroException();
      }

      public override string Name => "LN";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_665");
    }
}
