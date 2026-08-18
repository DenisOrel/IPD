
// Type: Intermech.Expressions.Functions.PosFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    public class PosFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        string str1 = Convert.ToString(values[0]);
        string str2 = Convert.ToString(values[1]);
        int num;
        try
        {
          num = str2.IndexOf(str1);
        }
        catch (System.ArgumentOutOfRangeException ex)
        {
          throw new Intermech.Expressions.Exceptions.ArgumentOutOfRangeException();
        }
        return (object) num;
      }

      public override Type GetReturnType(Type[] types) => typeof (int);

      protected override bool InputTypeSupported(Type type, int index) => type.Equals(typeof (string));

      public override bool MultArgsSupported(int count) => count == 2;

      public override string Name => "POS";

      public override FunctionCategory Category => FunctionCategory.String;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_677");
    }
}
