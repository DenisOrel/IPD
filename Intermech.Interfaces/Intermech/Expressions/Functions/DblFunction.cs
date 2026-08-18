
// Type: Intermech.Expressions.Functions.DblFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Dbl function. Convert To Double</summary>
    public class DblFunction : FloorFunction
    {
      public override object Evaluate(object[] values)
      {
        return values[0] is MeasuredValue ? (object) (values[0] as MeasuredValue).Value : (object) Convert.ToDouble(values[0]);
      }

      public override string Name => "Dbl";

      protected override bool InputTypeSupported(Type type, int index) => true;

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_644");
    }
}
