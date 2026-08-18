
// Type: Intermech.Expressions.Functions.IsDBNullFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>IsDBNull function</summary>
    public class IsDBNullFunction : Function
    {
      public override object Evaluate(object[] values) => (object) Convert.IsDBNull(values[0]);

      public override Type GetReturnType(Type[] types) => typeof (bool);

      protected override bool InputTypeSupported(Type type, int index)
      {
        return type.IsSubclassOf(typeof (object)) && !type.IsArray;
      }

      public override bool IsNullable(object[] values) => false;

      public override bool MultArgsSupported(int count) => count == 1;

      public override string Name => "IsDBNull";

      public override FunctionCategory Category => FunctionCategory.Other;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_660");
    }
}
