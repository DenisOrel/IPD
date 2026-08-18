
// Type: Intermech.Expressions.Functions.NowFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Now function.</summary>
    public class NowFunction : Function
    {
      public override object Evaluate(object[] values) => (object) DateTime.Now;

      public override Type GetReturnType(Type[] types) => typeof (DateTime);

      public override bool MultArgsSupported(int count) => count == 0;

      public override string Name => "NOW";

      public override FunctionCategory Category => FunctionCategory.Date;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_675");
    }
}
