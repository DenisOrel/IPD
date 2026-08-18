
// Type: Intermech.Expressions.Functions.ArcCosFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>ArcCos function.</summary>
    public class ArcCosFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        double d = Convert.ToDouble(values[0]);
        if (d > 1.0 || d < -1.0)
          throw new ACosOutOfBoundsException();
        return (object) (Math.Acos(d) * (180.0 / Math.PI));
      }

      public override string Name => "ACOS";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_627");
    }
}
