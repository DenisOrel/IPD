
// Type: Intermech.Expressions.Functions.HCotFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>HyperbolicCot function.</summary>
    public class HCotFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double num = Convert.ToDouble(values[0]);
        if (Math.Tanh(num).Equals(0.0))
          throw new InfiniteHCotException();
        return (object) (1.0 / Math.Tanh(num));
      }

      public override string Name => "COTH";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_650");
    }
}
