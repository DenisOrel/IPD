
// Type: Intermech.Expressions.Functions.FindFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Find function.</summary>
    public class FindFunction : Function
    {
      public override object Evaluate(object[] values, bool caseSensitive)
      {
        string str1 = Convert.ToString(values[0]);
        string str2 = Convert.ToString(values[1]);
        int startIndex = 0;
        if (values.Length > 2)
          startIndex = Convert.ToInt32(values[2]);
        int num;
        try
        {
          num = str2.IndexOf(str1, startIndex);
        }
        catch (System.ArgumentOutOfRangeException ex)
        {
          throw new Intermech.Expressions.Exceptions.ArgumentOutOfRangeException();
        }
        return (object) num;
      }

      public override Type GetReturnType(Type[] types) => typeof (int);

      protected override bool InputTypeSupported(Type type, int index)
      {
        bool flag = false;
        switch (index)
        {
          case 0:
          case 1:
            return type.Equals(typeof (string));
          case 2:
            return ExpTypeConverter.CanConvert(type, typeof (double));
          default:
            return flag;
        }
      }

      public override bool MultArgsSupported(int nCount) => nCount == 3;

      public override string Name => "FIND";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_646");
    }
}
