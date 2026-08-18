// Decompiled with JetBrains decompiler
// Type: Intermech.Project.WorkTimeValue
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Project;

public class WorkTimeValue
{
  public readonly double Value;
  [CanBeNull]
  public readonly WorkTimeUnit Unit;
  public readonly bool Estimation;

  public WorkTimeValue(double value, [CanBeNull] WorkTimeUnit unit, bool estimation)
  {
    this.Value = value;
    this.Unit = unit;
    this.Estimation = estimation;
  }
}
