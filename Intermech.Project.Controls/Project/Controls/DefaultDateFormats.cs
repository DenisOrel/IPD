// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.DefaultDateFormats
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project.Controls;

public class DefaultDateFormats
{
  [NotNull]
  public static readonly Dictionary<ScaleType, List<string>> GanttFormats = new Dictionary<ScaleType, List<string>>();
  [NotNull]
  [ItemNotNull]
  public static readonly List<string> DateFormats = new List<string>();
  public static int DefaultDateFormatIndex;

  static DefaultDateFormats()
  {
    DefaultDateFormats.GanttFormats.Add(ScaleType.Weeks, new List<string>()
    {
      "dd MMM \"'\"yy",
      "dd MMMM yyyy",
      "dd.MM.yy",
      "dd MMMM"
    });
    DefaultDateFormats.GanttFormats.Add(ScaleType.Days, new List<string>()
    {
      "ddd dd MMM",
      "ddd dd MMMM",
      "ddd dd MMM \"'\"yy"
    });
    DefaultDateFormats.GanttFormats.Add(ScaleType.Months, new List<string>()
    {
      "MMMM yyyy",
      "MMM \"'\"yy",
      "MMMM",
      "MM"
    });
    DefaultDateFormats.DateFormats.Add("dd.MM.yy");
    DefaultDateFormats.DateFormats.Add("dd.MM.yyyy");
    DefaultDateFormats.DateFormats.Add("dd.MM.yy H:mm");
    DefaultDateFormats.DefaultDateFormatIndex = DefaultDateFormats.DateFormats.Count - 1;
    DefaultDateFormats.DateFormats.Add("dd MMM");
    DefaultDateFormats.DateFormats.Add("dd MMM \"'\"yy");
    DefaultDateFormats.DateFormats.Add("dd MMM yyyy");
    DefaultDateFormats.DateFormats.Add("dd MMM yyyy H:mm");
    DefaultDateFormats.DateFormats.Add("dd MMMM");
    DefaultDateFormats.DateFormats.Add("dd MMMM yyyy");
    DefaultDateFormats.DateFormats.Add("dd MMMM yyyy H:mm");
    int count = DefaultDateFormats.DateFormats.Count;
    for (int index = 0; index < count; ++index)
      DefaultDateFormats.DateFormats.Add("ddd " + DefaultDateFormats.DateFormats[index]);
  }
}
