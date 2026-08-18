
// Type: Intermech.Expressions.Functions.CeilingFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Ceiling function.</summary>
    public class CeilingFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        return (object) Math.Ceiling(Convert.ToDouble(values[0]));
      }

      public override string Name => "CEIL";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_634");
    }
}
