
// Type: Intermech.Expressions.Functions.DateAddFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Constants;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>DateAdd function.</summary>
    public class DateAddFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        Type type = values[0].GetType();
        DateInterval dateInterval = !type.Equals(typeof (DateInterval)) ? (!type.Equals(typeof (string)) ? (DateInterval) Convert.ToInt32(values[0]) : DateHelper.IntervalFromString(Convert.ToString(values[0]))) : (DateInterval) values[0];
        double months = Convert.ToDouble(values[1]);
        DateTime dateTime = Convert.ToDateTime(values[2]);
        switch (dateInterval)
        {
          case DateInterval.Year:
            dateTime = dateTime.AddYears((int) months);
            break;
          case DateInterval.Quarter:
            dateTime.AddMonths(4);
            break;
          case DateInterval.Month:
            dateTime = dateTime.AddMonths((int) months);
            break;
          case DateInterval.Day:
            dateTime = dateTime.AddDays(months);
            break;
          case DateInterval.Hour:
            dateTime = dateTime.AddHours(months);
            break;
          case DateInterval.Minute:
            dateTime = dateTime.AddMinutes(months);
            break;
          case DateInterval.Second:
            dateTime = dateTime.AddSeconds(months);
            break;
        }
        return (object) dateTime;
      }

      public override Type GetReturnType(Type[] types) => typeof (DateTime);

      protected override bool InputTypeSupported(Type type, int index)
      {
        bool flag = false;
        switch (index)
        {
          case 0:
            return ExpTypeConverter.CanConvert(type, typeof (double)) || type.Equals(typeof (DateInterval)) || type.Equals(typeof (string));
          case 1:
            return ExpTypeConverter.CanConvert(type, typeof (double));
          case 2:
            return type.Equals(typeof (DateTime));
          default:
            return flag;
        }
      }

      public override bool MultArgsSupported(int count) => count == 3;

      public override string Name => "DATEADD";

      public override FunctionCategory Category => FunctionCategory.Date;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_639");
    }
}
