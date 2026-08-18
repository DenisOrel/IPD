
// Type: Intermech.Expressions.Functions.HSecFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>HyperbolicSec function.</summary>
    public class HSecFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double num = Convert.ToDouble(values[0]);
        if (Math.Cosh(num).Equals(0.0))
          throw new InfiniteHSecException();
        return (object) (1.0 / Math.Cosh(num));
      }

      public override string Name => "SECH";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_653");
    }
}
