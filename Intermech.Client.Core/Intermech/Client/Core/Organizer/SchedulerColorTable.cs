
// Type: Intermech.Client.Core.Organizer.SchedulerColorTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Provides color information of calendar graphical elements
/// </summary>
public class SchedulerColorTable
{
  /// <summary>Background color of calendar.</summary>
  public readonly Color Background = Color.FromArgb(227, 239, (int) byte.MaxValue);
  /// <summary>Background color of days in even months.</summary>
  public readonly Color DayBackgroundEven = Color.FromArgb(165, 191, 225);
  /// <summary>Background color of days in odd months.</summary>
  public readonly Color DayBackgroundOdd = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
  /// <summary>Background color of selected days.</summary>
  public readonly Color DayBackgroundSelected = Color.FromArgb(230, 237, 247);
  /// <summary>Border of.</summary>
  public readonly Color DayBorder = Color.FromArgb(93, 140, 201);
  /// <summary>Background color of day headers.</summary>
  public readonly Color DayHeaderBackground = Color.FromArgb(223, 232, 245);
  /// <summary>Color of text of day headers.</summary>
  public readonly Color DayHeaderText = Color.Black;
  /// <summary>Color of secondary text in headers.</summary>
  public readonly Color DayHeaderSecondaryText = Color.Black;
  /// <summary>Color of border of the top part of the days.</summary>
  /// <remarks>
  /// The DayTop is the zone of the calendar where items that lasts all or more are placed.
  /// </remarks>
  public readonly Color DayTopBorder = Color.FromArgb(93, 140, 201);
  /// <summary>
  /// Color of border of the top parth of the days when selected.
  /// </summary>
  /// <remarks>
  /// The DayTop is the zone of the calendar where items that lasts all or more are placed.
  /// </remarks>
  public readonly Color DayTopSelectedBorder = Color.FromArgb(93, 140, 201);
  /// <summary>Background color of day tops.</summary>
  /// <remarks>
  /// The DayTop is the zone of the calendar where items that lasts all or more are placed.
  /// </remarks>
  public readonly Color DayTopBackground = Color.FromArgb(165, 191, 225);
  /// <summary>Background color of selected day tops.</summary>
  /// <remarks>
  /// The DayTop is the zone of the calendar where items that lasts all or more are placed.
  /// </remarks>
  public readonly Color DayTopSelectedBackground = Color.FromArgb(41, 76, 122);
  /// <summary>
  /// 
  /// </summary>
  public readonly Color HeaderBackground = Color.FromArgb(173, 209, (int) byte.MaxValue);
  /// <summary>Color of items borders.</summary>
  public readonly Color ItemBorder = Color.FromArgb(93, 140, 201);
  /// <summary>Background color of items.</summary>
  public readonly Color ItemBackground = Color.FromArgb(192 /*0xC0*/, 211, 234);
  /// <summary>Forecolor of items.</summary>
  public readonly Color ItemText = Color.Black;
  /// <summary>Color of secondary text on items (Dates and times).</summary>
  public readonly Color ItemSecondaryText = Color.FromArgb(41, 76, 122);
  /// <summary>Color of items shadow.</summary>
  public readonly Color ItemShadow = Color.FromArgb(50, Color.Black);
  /// <summary>Color of items selected border.</summary>
  public readonly Color ItemSelectedBorder = Color.Black;
  /// <summary>Background color of selected items.</summary>
  public readonly Color ItemSelectedBackground = Color.FromArgb(192 /*0xC0*/, 211, 234);
  /// <summary>Forecolor of selected items.</summary>
  public readonly Color ItemSelectedText = Color.Black;
  /// <summary>Background color of week headers.</summary>
  public readonly Color WeekHeaderBackground = Color.FromArgb(223, 232, 245);
  /// <summary>Border color of week headers.</summary>
  public readonly Color WeekHeaderBorder = Color.FromArgb(93, 140, 201);
  /// <summary>Forecolor of week headers.</summary>
  public readonly Color WeekHeaderText = Color.FromArgb(93, 140, 201);
  /// <summary>Forecolor of day names.</summary>
  public readonly Color WeekDayName = Color.FromArgb(101, 147, 207);
  /// <summary>Border color of today day.</summary>
  public readonly Color TodayBorder = Color.FromArgb(238, 147, 17);
  /// <summary>Background color of today's DayTop.</summary>
  public readonly Color TodayTopBackground = Color.FromArgb(238, 147, 17);
  /// <summary>Color of lines in timescale.</summary>
  public readonly Color TimeScaleLine = Color.FromArgb(101, 147, 207);
  /// <summary>Color of text representing hours on the timescale.</summary>
  public readonly Color TimeScaleHours = Color.FromArgb(101, 147, 207);
  /// <summary>Color of text representing minutes on the timescale.</summary>
  public readonly Color TimeScaleMinutes = Color.FromArgb(101, 147, 207);
  /// <summary>Background color of time units.</summary>
  public readonly Color TimeUnitBackground = Color.FromArgb(230, 237, 247);
  /// <summary>Background color of highlighted time units.</summary>
  public readonly Color TimeUnitHighlightedBackground = Color.White;
  /// <summary>Background color of selected time units.</summary>
  public readonly Color TimeUnitSelectedBackground = Color.FromArgb(41, 76, 122);
  /// <summary>Color of light border of time units.</summary>
  public readonly Color TimeUnitBorderLight = Color.FromArgb(213, 225, 241);
  /// <summary>Color of dark border of time units.</summary>
  public readonly Color TimeUnitBorderDark = Color.FromArgb(165, 191, 225);
  /// <summary>Border color of the overflow indicators.</summary>
  public readonly Color DayOverflowBorder = Color.White;
  /// <summary>Background color of the overflow indicators.</summary>
  public readonly Color DayOverflowBackground = SystemColors.ControlDark;
  /// <summary>Background color of selected overflow indicators.</summary>
  public readonly Color DayOverflowSelectedBackground = Color.Orange;
  public readonly Color ButtonLight = Color.FromArgb(192 /*0xC0*/, 219, (int) byte.MaxValue);
  public readonly Color ButtonDark = Color.FromArgb(173, 209, (int) byte.MaxValue);
  public readonly Color ButtonHighlightDark = Color.FromArgb(196, 221, (int) byte.MaxValue);
  public readonly Color ButtonHighlightLight = Color.FromArgb(227, 239, (int) byte.MaxValue);
  public readonly Color ButtonHoveredLight = Color.FromArgb((int) byte.MaxValue, 230, 159);
  public readonly Color ButtonHoveredDark = Color.FromArgb((int) byte.MaxValue, 215, 103);
  public readonly Color ButtonHoveredHighlightDark = Color.FromArgb((int) byte.MaxValue, 233, 168);
  public readonly Color ButtonHoveredHighlightLight = Color.FromArgb((int) byte.MaxValue, 254, 228);
  public readonly Color ButtonClickedLight = Color.FromArgb((int) byte.MaxValue, 211, 101);
  public readonly Color ButtonClickedDark = Color.FromArgb(251, 140, 60);
  public readonly Color ButtonClickedHighlightDark = Color.FromArgb((int) byte.MaxValue, 173, 67);
  public readonly Color ButtonClickedHighlightLight = Color.FromArgb((int) byte.MaxValue, 189, 105);
  public readonly Color ButtonActiveLight = Color.FromArgb(254, 225, 122);
  public readonly Color ButtonActiveDark = Color.FromArgb((int) byte.MaxValue, 171, 63 /*0x3F*/);
  public readonly Color ButtonActiveHighlightDark = Color.FromArgb((int) byte.MaxValue, 188, 111);
  public readonly Color ButtonActiveHighlightLight = Color.FromArgb((int) byte.MaxValue, 217, 170);
}
