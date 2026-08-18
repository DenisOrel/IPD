
// Type: Intermech.Expressions.Functions.ValFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Len function.</summary>
    public class ValFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        string str1 = Convert.ToString(values[0]);
        string str2 = Convert.ToString(values[1]);
        string empty = string.Empty;
        if (values.Length > 2)
          empty = Convert.ToString(values[2]);
        return string.IsNullOrEmpty(str2) ? (object) string.Empty : (object) (str1 + str2 + empty);
      }

      public override Type GetReturnType(Type[] types) => typeof (string);

      protected override bool InputTypeSupported(Type type, int index) => true;

      public override bool MultArgsSupported(int count) => count == 2 || count == 3;

      public override bool IsNullable(object[] values) => false;

      public override string Name => "VAL";

      public override FunctionCategory Category => FunctionCategory.String;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_692");
    }
}
