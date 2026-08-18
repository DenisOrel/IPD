
// Type: Intermech.Expressions.Functions._ArcCosFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    public class _ArcCosFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double d = Convert.ToDouble(values[0]);
        return d <= 1.0 && d >= -1.0 ? (object) Math.Acos(d) : throw new ACosOutOfBoundsException();
      }

      public override string Name => "_ACOS";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_627");
    }
}
