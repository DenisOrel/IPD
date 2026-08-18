
// Type: Intermech.Expressions.Functions.MaxFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>Max function.</summary>
    /// <remarks>
    /// DBNull parameters are ignored.
    /// If all input parameters are of type DBNull, the result is DBNull.
    /// </remarks>
    public class MaxFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        Type type = values.GetValue(0).GetType();
        if (type.Equals(typeof (DBNull)))
        {
          for (int index = 1; index <= values.GetUpperBound(0); ++index)
          {
            if (!Convert.IsDBNull(values.GetValue(index)))
            {
              type = values.GetValue(index).GetType();
              break;
            }
          }
        }
        if (ExpTypeConverter.CanConvert(type, typeof (double)))
        {
          double minValue = double.MinValue;
          int index = 0;
          bool flag = true;
          for (; index <= values.GetUpperBound(0); ++index)
          {
            object obj = values.GetValue(index);
            if (!Convert.IsDBNull(obj))
            {
              flag = false;
              if (Convert.ToDouble(obj) > minValue)
                minValue = Convert.ToDouble(obj);
            }
          }
          return !flag ? (object) minValue : (object) DBNull.Value;
        }
        DateTime t2 = DateTime.MinValue;
        int index1 = 0;
        bool flag1 = true;
        for (; index1 <= values.GetUpperBound(0); ++index1)
        {
          object obj = values.GetValue(index1);
          if (!Convert.IsDBNull(obj))
          {
            flag1 = false;
            if (DateTime.Compare(Convert.ToDateTime(obj), t2) > 0)
              t2 = Convert.ToDateTime(obj);
          }
        }
        return !flag1 ? (object) t2 : (object) DBNull.Value;
      }

      public override Type GetReturnType(Type[] types)
      {
        return types.GetUpperBound(0) == 0 && types[0].IsArray ? (ExpTypeConverter.CanConvert(types[0].GetElementType(), typeof (double)) ? typeof (double) : typeof (DateTime)) : (ExpTypeConverter.CanConvert(types[0], typeof (double)) ? typeof (double) : typeof (DateTime));
      }

      public override bool IsNullable(object[] values) => false;

      public override bool MultArgsSupported(int count) => count > 0;

      public override bool Validate(Type[] types, ref int invalidArgument)
      {
        bool flag1 = true;
        if (ExpTypeConverter.CanConvert(types[0], typeof (double)))
        {
          for (int index = 1; index <= types.GetUpperBound(0); ++index)
          {
            if (!ExpTypeConverter.CanConvert(types[index], typeof (double)))
            {
              flag1 = false;
              invalidArgument = index;
              break;
            }
          }
          return flag1;
        }
        if (types[0].Equals(typeof (DateTime)))
        {
          for (int index = 1; index <= types.GetUpperBound(0); ++index)
          {
            if (!types[index].Equals(typeof (DateTime)))
            {
              flag1 = false;
              invalidArgument = index;
              break;
            }
          }
          return flag1;
        }
        if (types.GetUpperBound(0) == 0 && types[0].IsArray)
        {
          if (!ExpTypeConverter.CanConvert(types[0].GetElementType(), typeof (double)) && !types[0].GetElementType().Equals(typeof (DateTime)))
          {
            flag1 = false;
            invalidArgument = 0;
          }
          return flag1;
        }
        bool flag2 = false;
        invalidArgument = 0;
        return flag2;
      }

      public override string Name => "MAX";

      public override FunctionCategory Category => FunctionCategory.Math;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_669");
    }
}
