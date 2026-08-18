// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Utilities
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Project;

[Obsolete("Class will be removed in future releases!")]
public static class Utilities
{
  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.CandidatesPostSymbol")]
  public static string CandidatesPostSymbol => IMProject.CandidatesPostSymbol;

  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.CandidatesPreSymbol")]
  public static string CandidatesPreSymbol => IMProject.CandidatesPreSymbol;

  [Obsolete("Use IMProject.DefaultOvertimeWorkSupplementalHourCost")]
  public static double DefaultOvertimeWorkSupplementalHourCost
  {
    get => IMProject.DefaultOvertimeWorkSupplementalHourCost;
  }

  [Obsolete("Use IMProject.DefaultWorkDuration")]
  public static double DefaultWorkDuration => IMProject.DefaultWorkDuration;

  [Obsolete("Use IMProject.DefaultWorkHourCost")]
  public static double DefaultWorkHourCost => IMProject.DefaultWorkHourCost;

  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.EndToEndDependencySymbol")]
  public static string EndToEndDependencySymbol => IMProject.EndToEndDependencySymbol;

  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.EstimationSymbol")]
  public static string EstimationSymbol => IMProject.EstimationSymbol;

  [Obsolete("Use IMProject.LevelingAssignmentStep")]
  public static double LevelingAssignmentStep => IMProject.LevelingAssignmentStep;

  [Obsolete("Use IMProject.LevelingTimeoutSeconds")]
  public static double LevelingTimeoutSeconds => IMProject.LevelingTimeoutSeconds;

  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.ListSeparatorSymbol")]
  public static string ListSeparatorSymbol => IMProject.ListSeparatorSymbol;

  [Obsolete("Use IMProject.MaximumCompletionTryCount")]
  public static int MaximumCompletionTryCount => IMProject.MaximumCompletionTryCount;

  [Obsolete("Use IMProject.MaximumDateTryCount")]
  public static int MaximumDateTryCount => IMProject.MaximumDateTryCount;

  [Obsolete("Use IMProject.MaximumIncreaseIndentLevel")]
  public static int MaximumIncreaseIndentLevel => IMProject.MaximumIncreaseIndentLevel;

  [Obsolete("Use IMProject.MaximumLevelingPostponeDays")]
  public static int MaximumLevelingPostponeDays => IMProject.MaximumLevelingPostponeDays;

  [Obsolete("Use IMProject.MaximumTaskWork")]
  public static double MaximumTaskWork => IMProject.MaximumTaskWork;

  [Obsolete("Use IMProject.MultiplyIncorrectPrioritiesFactor")]
  public static double MultiplyIncorrectPrioritiesFactor
  {
    get => IMProject.MultiplyIncorrectPrioritiesFactor;
  }

  [Obsolete("Use IMProject.MultiplyOverAllocationFactor")]
  public static double MultiplyOverAllocationFactor => IMProject.MultiplyOverAllocationFactor;

  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.PercentSymbol")]
  public static string PercentSymbol => IMProject.PercentSymbol;

  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.UnitPostSymbol")]
  public static string UnitPostSymbol => IMProject.UnitPostSymbol;

  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.UnitPreSymbol")]
  public static string UnitPreSymbol => IMProject.UnitPreSymbol;

  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.UnitSeparatorSymbol")]
  public static string UnitSeparatorSymbol => IMProject.UnitSeparatorSymbol;

  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.Unknown")]
  public static string Unknown => IMProject.Unknown;

  [NotNull]
  [NotEmpty]
  [Obsolete("Use IMProject.WbsCodeSeparator")]
  public static string WbsCodeSeparator => IMProject.WbsCodeSeparator;
}
