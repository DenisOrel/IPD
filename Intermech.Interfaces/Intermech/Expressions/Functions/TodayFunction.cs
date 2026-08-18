
// Type: Intermech.Expressions.Functions.TodayFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Today function.</summary>
    public class TodayFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        DateTime now = DateTime.Now;
        return (object) new DateTime(now.Year, now.Month, now.Day);
      }

      public override Type GetReturnType(Type[] types) => typeof (DateTime);

      public override bool MultArgsSupported(int count) => count == 0;

      public override string Name => "TODAY";

      public override FunctionCategory Category => FunctionCategory.Date;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_689");
    }
}
