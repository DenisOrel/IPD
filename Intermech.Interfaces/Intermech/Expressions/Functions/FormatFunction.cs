
// Type: Intermech.Expressions.Functions.FormatFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Format function.</summary>
    public class FormatFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        string format = Convert.ToString(values[0]);
        int length1 = values.Length;
        if (length1 <= 1)
          return (object) format;
        int length2 = length1 - 1;
        object[] destinationArray = new object[length2];
        Array.Copy((Array) values, 1, (Array) destinationArray, 0, length2);
        return (object) string.Format(format, destinationArray);
      }

      public override Type GetReturnType(Type[] types) => typeof (string);

      protected override bool InputTypeSupported(Type type, int index)
      {
        return index != 0 || type.Equals(typeof (string));
      }

      public override bool MultArgsSupported(int count) => count > 0;

      public override string Name => "FORMAT";

      public override FunctionCategory Category => FunctionCategory.String;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_648");
    }
}
