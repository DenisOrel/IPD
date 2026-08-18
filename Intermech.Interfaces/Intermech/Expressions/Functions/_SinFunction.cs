
// Type: Intermech.Expressions.Functions._SinFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    public class _SinFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        return (object) Math.Sin(Convert.ToDouble(values[0]));
      }

      public override string Name => "_SIN";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_683");
    }
}
