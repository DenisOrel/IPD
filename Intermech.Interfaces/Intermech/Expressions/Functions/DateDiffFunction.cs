
// Type: Intermech.Expressions.Functions.DateDiffFunction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Constants;
using Intermech.Localization;
using System;
using System.Globalization;
using System.Threading;


namespace Intermech.Expressions.Functions
{
    public class DateDiffFunction : Function
    {
      public override object Evaluate(object[] values)
      {
        Type type = values[0].GetType();
        DateInterval dateInterval = !(type == typeof (DateInterval)) ? (!(type == typeof (string)) ? (DateInterval) Convert.ToInt32(values[0]) : DateHelper.IntervalFromString(Convert.ToString(values[0]))) : (DateInterval) values[0];
        DateTime dateTime1 = Convert.ToDateTime(values[1]);
        DateTime dateTime2 = Convert.ToDateTime(values[2]);
        Calendar calendar = Thread.CurrentThread.CurrentCulture.Calendar;
        TimeSpan timeSpan = dateTime2.Subtract(dateTime1);
        switch (dateInterval)
        {
          case DateInterval.Year:
            return (object) (long) (calendar.GetYear(dateTime2) - calendar.GetYear(dateTime1));
          case DateInterval.Quarter:
            return (object) (long) ((calendar.GetYear(dateTime2) - calendar.GetYear(dateTime1)) * 4 + (calendar.GetMonth(dateTime2) - 1) / 3 - (calendar.GetMonth(dateTime1) - 1) / 3);
          case DateInterval.Month:
            return (object) (long) ((calendar.GetYear(dateTime2) - calendar.GetYear(dateTime1)) * 12 + calendar.GetMonth(dateTime2) - calendar.GetMonth(dateTime1));
          case DateInterval.DayOfYear:
          case DateInterval.Day:
            return (object) (long) Math.Round(DateDiffFunction.Fix(timeSpan.TotalDays));
          case DateInterval.WeekOfYear:
            DateTime dateTime3 = dateTime1.AddDays((double) -(int) dateTime1.DayOfWeek);
            return (object) ((long) Math.Round(DateDiffFunction.Fix(dateTime2.AddDays((double) -(int) dateTime2.DayOfWeek).Subtract(dateTime3).TotalDays)) / 7L);
          case DateInterval.Weekday:
            return (object) ((long) Math.Round(DateDiffFunction.Fix(timeSpan.TotalDays)) / 7L);
          case DateInterval.Hour:
            return (object) (long) Math.Round(DateDiffFunction.Fix(timeSpan.TotalHours));
          case DateInterval.Minute:
            return (object) (long) Math.Round(DateDiffFunction.Fix(timeSpan.TotalMinutes));
          case DateInterval.Second:
            return (object) (long) Math.Round(DateDiffFunction.Fix(timeSpan.TotalSeconds));
          default:
            return (object) 0;
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
          case 2:
            return type.Equals(typeof (DateTime));
          case 3:
            return ExpTypeConverter.CanConvert(type, typeof (double)) || type.Equals(typeof (FirstDayOfWeek));
          case 4:
            return ExpTypeConverter.CanConvert(type, typeof (double)) || type.Equals(typeof (FirstWeekOfYear));
          default:
            return flag;
        }
      }

      public override bool MultArgsSupported(int count) => count >= 3 && count <= 5;

      public override string Name => "DATEDIFF";

      public override FunctionCategory Category => FunctionCategory.Date;

      public static double Fix(double Number)
      {
        return Number >= 0.0 ? Math.Floor(Number) : -Math.Floor(-Number);
      }

      public override string Description => LocalizationHolder.rm.GetString("Interfaces_640");
    }
}
