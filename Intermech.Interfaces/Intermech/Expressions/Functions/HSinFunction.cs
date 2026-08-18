
// Type: Intermech.Expressions.Functions.HSinFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>HyperbolicSin function.</summary>
    public class HSinFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        return (object) Math.Sinh(Convert.ToDouble(values[0]));
      }

      public override string Name => "SINH";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_654");
    }
}
