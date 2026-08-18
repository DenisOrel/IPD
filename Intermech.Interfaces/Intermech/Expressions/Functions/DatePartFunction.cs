
// Type: Intermech.Expressions.Functions.DatePartFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Constants;
using Intermech.Expressions.Exceptions;
using Intermech.Localization;
using System;


namespace Intermech.Expressions.Functions
{
    /// <summary>DatePart function.</summary>
    public class DatePartFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        Type type = values[0].GetType();
        DateInterval dateInterval = !(type == typeof (DateInterval)) ? (!(type == typeof (string)) ? (DateInterval) Convert.ToInt32(values[0]) : DateHelper.IntervalFromString(Convert.ToString(values[0]))) : (DateInterval) values[0];
        DateTime dateTime = Convert.ToDateTime(values[1]);
        switch (dateInterval)
        {
          case DateInterval.Year:
            return (object) dateTime.Year;
          case DateInterval.Quarter:
            return (object) ((dateTime.Month - 1) / 3);
          case DateInterval.Month:
            return (object) dateTime.Month;
          case DateInterval.DayOfYear:
            return (object) dateTime.DayOfYear;
          case DateInterval.Day:
            return (object) dateTime.Day;
          case DateInterval.Hour:
            return (object) dateTime.Hour;
          case DateInterval.Minute:
            return (object) dateTime.Minute;
          case DateInterval.Second:
            return (object) dateTime.Second;
          default:
            throw new InvalidArgumentException();
        }
      }

      protected override bool InputTypeSupported(Type type, int index)
      {
        bool flag = false;
        switch (index)
        {
          case 0:
            return ExpTypeConverter.CanConvert(type, typeof (double)) || type.Equals(typeof (DateInterval)) || type.Equals(typeof (string));
          case 1:
            return type.Equals(typeof (DateTime));
          case 2:
            return ExpTypeConverter.CanConvert(type, typeof (double)) || type.Equals(typeof (FirstDayOfWeek));
          case 3:
            return ExpTypeConverter.CanConvert(type, typeof (double)) || type.Equals(typeof (FirstWeekOfYear));
          default:
            return flag;
        }
      }

      public override bool MultArgsSupported(int nCount) => nCount >= 2 && nCount <= 4;

      public override string Name => "DATEPART";

      public override FunctionCategory Category => FunctionCategory.Date;

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_642");
    }
}
