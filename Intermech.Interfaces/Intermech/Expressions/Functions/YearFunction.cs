
// Type: Intermech.Expressions.Functions.YearFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Year function.</summary>
    public class YearFunction : Function
    {
      public override object Evaluate(object[] values) => (object) Convert.ToDateTime(values[0]).Year;

      protected override bool InputTypeSupported(Type type, int index)
      {
        return index == 0 && type.Equals(typeof (DateTime));
      }

      public override string Name => "YEAR";

      public override FunctionCategory Category => FunctionCategory.Date;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_694");
    }
}
