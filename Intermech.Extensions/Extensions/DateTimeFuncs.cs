// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DateTimeFuncs
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Globalization;

#nullable disable
namespace Intermech.Extensions;

public static class DateTimeFuncs
{
  public const string Iso8601DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
  [CanBeNull]
  private static string _timeZoneStr;

  [NotNull]
  [NotWhitespace]
  public static string TimeZoneStr
  {
    get
    {
      if (DateTimeFuncs._timeZoneStr == null)
      {
        TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
        DateTimeFuncs._timeZoneStr = utcOffset >= TimeSpan.Zero ? "+" + utcOffset.ToString("hh\\:mm", (IFormatProvider) CultureInfo.InvariantCulture) : "-" + utcOffset.ToString("hh\\:mm", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      return DateTimeFuncs._timeZoneStr;
    }
  }

  [NotNull]
  [NotWhitespace]
  public static string ToIso8601(this DateTime dateTime, bool includeTimeZone = true)
  {
    if (dateTime.Kind != DateTimeKind.Utc)
      dateTime = dateTime.ToUniversalTime();
    DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
    return !includeTimeZone ? dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss", (IFormatProvider) CultureInfo.InvariantCulture) : dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss", (IFormatProvider) CultureInfo.InvariantCulture) + DateTimeFuncs.TimeZoneStr;
  }

  public static DateTime ParseIso8601([NotNull] string value)
  {
    int length1 = value.IndexOf("+", StringComparison.Ordinal);
    if (length1 > 0)
    {
      value = value.Substring(0, length1);
    }
    else
    {
      int length2 = value.IndexOf("-", value.IndexOf("T", StringComparison.Ordinal), StringComparison.Ordinal);
      if (length2 > 0)
        value = value.Substring(0, length2);
    }
    DateTime iso8601 = DateTime.ParseExact(value, "s", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
    if (iso8601.Kind != DateTimeKind.Local)
      iso8601 = DateTime.SpecifyKind(iso8601, DateTimeKind.Local);
    return iso8601;
  }

  public static bool TryParseIso8601([NotNull] string value, out DateTime result)
  {
    int length1 = value.IndexOf("+", StringComparison.Ordinal);
    if (length1 > 0)
    {
      value = value.Substring(0, length1);
    }
    else
    {
      int length2 = value.IndexOf("-", value.IndexOf("T", StringComparison.Ordinal), StringComparison.Ordinal);
      if (length2 > 0)
        value = value.Substring(0, length2);
    }
    if (!DateTime.TryParseExact(value, "s", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result))
      return false;
    if (result.Kind != DateTimeKind.Local)
      result = DateTime.SpecifyKind(result, DateTimeKind.Local);
    return true;
  }
}
