
// Type: Intermech.Expressions.Functions.LeftFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    public class LeftFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        string str1 = Convert.ToString(values[0]);
        int int32 = Convert.ToInt32(values[1]);
        string str2;
        try
        {
          str2 = int32 > 0 ? str1.Substring(0, int32) : str1;
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
        if (index == 0)
          return type.Equals(typeof (string));
        return index == 1 ? ExpTypeConverter.CanConvert(type, typeof (double)) : flag;
      }

      public override bool MultArgsSupported(int count) => count == 2;

      public override string Name => "LEFT";

      public override FunctionCategory Category => FunctionCategory.String;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_662");
    }
}
