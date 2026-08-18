// Decompiled with JetBrains decompiler
// Type: Intermech.Project.IMProject
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Project;

/// <summary>Общие статические методы, константы и данные ядра IMProject-а</summary>
public abstract class IMProject
{
  [NotNull]
  [NotEmpty]
  public static string CandidatesPostSymbol { get; set; } = ")";

  [NotNull]
  [NotEmpty]
  public static string CandidatesPreSymbol { get; set; } = "(";

  public static double DefaultOvertimeWorkSupplementalHourCost { get; set; } = 5.0;

  public static double DefaultWorkDuration { get; set; } = 8.0;

  public static double DefaultWorkHourCost { get; set; } = 10.0;

  [NotNull]
  [NotEmpty]
  public static string EndToEndDependencySymbol { get; set; } = "e";

  [NotNull]
  [NotEmpty]
  public static string EstimationSymbol { get; set; } = "?";

  public static double LevelingAssignmentStep { get; set; } = 0.5;

  public static double LevelingTimeoutSeconds { get; set; } = 20.0;

  [NotNull]
  [NotEmpty]
  public static string ListSeparatorSymbol { get; set; } = Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator;

  public static int MaximumCompletionTryCount { get; set; } = 2;

  public static int MaximumDateTryCount { get; set; } = 7;

  public static int MaximumIncreaseIndentLevel { get; set; } = 5;

  public static int MaximumLevelingPostponeDays { get; set; } = 60;

  public static double MaximumTaskWork { get; set; } = 32000.0;

  public static double MultiplyIncorrectPrioritiesFactor { get; set; } = 0.5;

  public static double MultiplyOverAllocationFactor { get; set; } = 0.1;

  [NotNull]
  [NotEmpty]
  public static string PercentSymbol { get; set; } = "%";

  [NotNull]
  [NotEmpty]
  public static string UnitPostSymbol { get; set; } = "]";

  [NotNull]
  [NotEmpty]
  public static string UnitPreSymbol { get; set; } = "[";

  [NotNull]
  [NotEmpty]
  public static string UnitSeparatorSymbol { get; set; } = "-";

  [NotNull]
  [NotEmpty]
  public static string Unknown { get; set; } = "?";

  [NotNull]
  [NotEmpty]
  public static string WbsCodeSeparator { get; set; } = ".";
}
