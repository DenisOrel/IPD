
// Type: Intermech.Expressions.Functions.ArcTanFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>ArcTan function.</summary>
    public class ArcTanFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        return (object) (Math.Atan(Convert.ToDouble(values[0])) * (180.0 / Math.PI));
      }

      public override string Name => "ATAN";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_632");
    }
}
