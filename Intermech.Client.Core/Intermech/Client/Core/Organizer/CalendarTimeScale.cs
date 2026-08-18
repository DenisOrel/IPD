
// Type: Intermech.Client.Core.Organizer.CalendarTimeScale
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Organizer;

/// <summary>Enumerates possible timescales for Calendar control</summary>
public enum CalendarTimeScale
{
  /// <summary>Makes calendar show intervals of 5 minutes</summary>
  FiveMinutes = 5,
  /// <summary>Makes calendar show intervals of 6 minutes</summary>
  SixMinutes = 6,
  /// <summary>Makes calendar show intervals of 10 minutes</summary>
  TenMinutes = 10, // 0x0000000A
  /// <summary>Makes calendar show intervals of 15 minutes</summary>
  FifteenMinutes = 15, // 0x0000000F
  /// <summary>Makes calendar show intervals of 30 minutes</summary>
  ThirtyMinutes = 30, // 0x0000001E
  /// <summary>Makes calendar show intervals of 60 minutes</summary>
  SixtyMinutes = 60, // 0x0000003C
}
