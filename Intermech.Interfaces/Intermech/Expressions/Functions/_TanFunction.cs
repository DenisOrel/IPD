
// Type: Intermech.Expressions.Functions._TanFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    public class _TanFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double num = Convert.ToDouble(values[0]);
        return !Math.Cos(num).Equals(0.0) ? (object) Math.Tan(num) : throw new InfiniteTanException();
      }

      public override string Name => "_TAN";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_688");
    }
}
