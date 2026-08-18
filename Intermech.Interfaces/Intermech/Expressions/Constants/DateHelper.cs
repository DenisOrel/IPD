
// Type: Intermech.Expressions.Constants.DateHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;


namespace Intermech.Expressions.Constants
{
    internal static class DateHelper
    {
      internal static DateInterval IntervalFromString(string value)
      {
        switch (value.ToLower())
        {
          case "d":
            return DateInterval.Day;
          case "h":
            return DateInterval.Hour;
          case "m":
            return DateInterval.Month;
          case "n":
            return DateInterval.Minute;
          case "q":
            return DateInterval.Quarter;
          case "s":
            return DateInterval.Second;
          case "w":
            return DateInterval.Weekday;
          case "ww":
            return DateInterval.WeekOfYear;
          case "y":
            return DateInterval.DayOfYear;
          case "yyyy":
            return DateInterval.Year;
          default:
            throw new InvalidArgumentException();
        }
      }
    }
}
