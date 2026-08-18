
// Type: Intermech.Expressions.Functions.UpperFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Upper function.</summary>
    public class UpperFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        return (object) Convert.ToString(values[0]).ToUpper();
      }

      public override Type GetReturnType(Type[] types) => typeof (string);

      protected override bool InputTypeSupported(Type type, int index)
      {
        return index == 0 && type.Equals(typeof (string));
      }

      public override string Name => "UPPER";

      public override FunctionCategory Category => FunctionCategory.String;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_691");
    }
}
