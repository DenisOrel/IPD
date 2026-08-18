
// Type: Intermech.Expressions.Functions.SqrtFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Sqrt function.</summary>
    public class SqrtFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double d = Convert.ToDouble(values[0]);
        return d >= 0.0 ? (object) Math.Sqrt(d) : throw new SqrtNegNumberException();
      }

      public override string Name => "SQRT";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_684");
    }
}
