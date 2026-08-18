
// Type: Intermech.Expressions.Functions.CscFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Csc function.</summary>
    public class CscFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double a = Convert.ToDouble(values[0]);
        if (Math.Sin(a).Equals(0.0))
          throw new InfiniteCscException();
        return (object) (1.0 / Math.Sin(a));
      }

      public override string Name => "CSC";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_638");
    }
}
