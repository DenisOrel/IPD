// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Evaluator.PropInfos
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project.Evaluator;

public static class PropInfos
{
  [CanBeNull]
  private static List<PropInfo> _all;

  [NotNull]
  private static PropInfo Add([NotNull] string name)
  {
    PropInfo propInfo = new PropInfo(name);
    PropInfos.All.Add(propInfo);
    return propInfo;
  }

  [NotNull]
  public static List<PropInfo> All
  {
    get
    {
      if (PropInfos._all == null)
      {
        PropInfos._all = new List<PropInfo>();
        PropInfos.Add("PercentCompleted");
        PropInfos.Add("ConstraintDate");
        PropInfos.Add("ConstraintType");
        PropInfos.Add("Completed");
        PropInfos.Add("CompletedWork");
        PropInfos.Add("Duration");
        PropInfos.Add("Estimation");
        PropInfos.Add("FactStart");
        PropInfos.Add("FactFinish");
        PropInfos.Add("Finish");
        PropInfos.Add("HasSubTasks");
        PropInfos.Add("IndentLevel");
        PropInfos.Add("DispIndex");
        PropInfos.Add("IsCritical");
        PropInfos.Add("IsExecuted");
        PropInfos.Add("Milestone");
        PropInfos.Add("Name");
        PropInfos.Add("Priority");
        PropInfos.Add("Start");
        PropInfos.Add("Status");
        PropInfos.Add("WbsCode");
        PropInfos.Add("Work");
        PropInfos.Add("SrcData.Count");
        PropInfos.Add("Results.Count");
        PropInfos.Add("Assignments");
        PropInfos.Add("AssignmentsString");
        PropInfos.Add("ChiefString");
        PropInfos.Add("PlannedPercentCompleted");
        PropInfos._all.Sort((Comparison<PropInfo>) ((pi1, pi2) => string.Compare(pi1.DisplayName, pi2.DisplayName, StringComparison.Ordinal)));
      }
      return PropInfos._all;
    }
  }

  [CanBeNull]
  public static PropInfo Find([CanBeNull] string name)
  {
    return PropInfos.All.Find((Predicate<PropInfo>) (p => p.Name == name));
  }
}
