
// Type: Intermech.Expressions.Functions.StrFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Expressions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Format function.</summary>
    public class StrFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        object obj = values[0];
        if (values.Length > 1)
        {
          string format = Convert.ToString(values[1]);
          Type type = obj.GetType();
          if (ExpTypeConverter.CanConvert(type, typeof (double)))
          {
            double num = Convert.ToDouble(obj);
            try
            {
              return (object) num.ToString(format);
            }
            catch (FormatException ex)
            {
              return (object) ((int) num).ToString(format);
            }
          }
          else if (ExpTypeConverter.CanConvert(type, typeof (DateTime)))
            return (object) ((DateTime) obj).ToString(format);
        }
        else if (obj is NamedValue namedValue)
          return (object) namedValue.Name;
        return (object) obj.ToString();
      }

      public override Type GetReturnType(Type[] types) => typeof (string);

      protected override bool InputTypeSupported(Type type, int index)
      {
        return index != 1 || type.Equals(typeof (string));
      }

      public override bool MultArgsSupported(int count) => count == 1 || count == 2;

      public override string Name => "STR";

      public override FunctionCategory Category => FunctionCategory.String;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_685");
    }
}
