
// Type: Intermech.Expressions.Functions.InFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>In function.</summary>
    public class InFunction : Function
    {
      public override object Evaluate(object[] values, bool caseSensitive)
      {
        bool flag = false;
        Array destinationArray;
        if (values.GetUpperBound(0) == 1 && values[1].GetType().IsArray)
        {
          destinationArray = (Array) values[1];
        }
        else
        {
          destinationArray = (Array) new object[values.Length - 1];
          Array.Copy((Array) values, 1, destinationArray, 0, values.Length - 1);
        }
        string strA = values[0].ToString();
        for (int index = 0; index <= destinationArray.GetUpperBound(0); ++index)
        {
          if (!Convert.IsDBNull(destinationArray.GetValue(index)) && string.Compare(strA, Convert.ToString(destinationArray.GetValue(index)), !caseSensitive) == 0)
          {
            flag = true;
            break;
          }
        }
        return (object) flag;
      }

      public override Type GetReturnType(Type[] types) => typeof (bool);

      public override bool IsNullable(object[] values)
      {
        bool flag = false;
        if (Convert.IsDBNull(values.GetValue(0)))
          flag = true;
        return flag;
      }

      public override bool MultArgsSupported(int count) => count >= 2;

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag1 = true;
        if (types[0].Equals(typeof (string)))
        {
          if (types[1].Equals(typeof (string)))
          {
            for (int index = 2; index <= types.GetUpperBound(0); ++index)
            {
              if (!types[index].Equals(typeof (string)))
              {
                flag1 = false;
                invalidArgument = index;
                break;
              }
            }
            return flag1;
          }
          if (types.GetUpperBound(0) == 1 && types[1].IsArray && !types[1].GetElementType().Equals(typeof (string)))
          {
            flag1 = false;
            invalidArgument = 1;
          }
          return flag1;
        }
        bool flag2 = false;
        invalidArgument = 0;
        return flag2;
      }

      public override string Name => "IN";

      public override FunctionCategory Category => FunctionCategory.String;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_658");
    }
}
