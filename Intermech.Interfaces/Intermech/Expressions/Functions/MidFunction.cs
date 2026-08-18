
// Type: Intermech.Expressions.Functions.MidFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    public class MidFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        string str1 = Convert.ToString(values[0]);
        int int32_1 = Convert.ToInt32(values[1]);
        int int32_2 = Convert.ToInt32(values[2]);
        if (string.IsNullOrEmpty(str1))
          return (object) string.Empty;
        int length = str1.Length;
        if (int32_1 >= length)
          return (object) string.Empty;
        string str2;
        try
        {
          str2 = int32_1 + int32_2 < length ? str1.Substring(int32_1, int32_2) : str1.Substring(int32_1);
        }
        catch (System.ArgumentOutOfRangeException ex)
        {
          throw new Intermech.Expressions.Exceptions.ArgumentOutOfRangeException();
        }
        return (object) str2;
      }

      public override Type GetReturnType(Type[] types) => typeof (string);

      protected override bool InputTypeSupported(Type type, int index)
      {
        bool flag = false;
        switch (index)
        {
          case 0:
            return type.Equals(typeof (string));
          case 1:
          case 2:
            return ExpTypeConverter.CanConvert(type, typeof (double));
          default:
            return flag;
        }
      }

      public override bool MultArgsSupported(int count) => count == 3;

      public override string Name => "MID";

      public override FunctionCategory Category => FunctionCategory.String;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_670");
    }
}
