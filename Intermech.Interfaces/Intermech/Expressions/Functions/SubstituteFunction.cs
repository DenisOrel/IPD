
// Type: Intermech.Expressions.Functions.SubstituteFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Text.RegularExpressions;


namespace Intermech.Expressions.Functions
{
    /// <summary>Substitute function.</summary>
    public class SubstituteFunction : Function
    {
      public override object Evaluate(object[] values, bool caseSensitive)
      {
        string input = Convert.ToString(values[0]);
        string str1 = Convert.ToString(values[1]);
        string str2 = Convert.ToString(values[2]);
        return caseSensitive ? (object) input.Replace(str1, str2) : (object) Regex.Replace(input, str1, str2, RegexOptions.IgnoreCase);
      }

      public override Type GetReturnType(Type[] types) => typeof (string);

      protected override bool InputTypeSupported(Type type, int index) => type.Equals(typeof (string));

      public override bool MultArgsSupported(int count) => count == 3;

      public override string Name => "SUBSTITUTE";

      public override FunctionCategory Category => FunctionCategory.String;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_686");
    }
}
