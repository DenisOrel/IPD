
// Type: Intermech.Expressions.Functions.CotFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Cot function.</summary>
    public class CotFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double a = Convert.ToDouble(values[0]);
        if (Math.Sin(a).Equals(0.0))
          throw new InfiniteCotException();
        return (object) (1.0 / Math.Tan(a));
      }

      public override string Name => "COT";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_636");
    }
}
