// Decompiled with JetBrains decompiler
// Type: Intermech.Project.XmlPredecessor
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Project;

internal class XmlPredecessor
{
  [NotNull]
  public string UID { get; }

  public DependencyType Type { get; }

  [NotNull]
  public string ProjectName { get; }

  public double Lag { get; set; }

  [CanBeNull]
  public WorkTimeUnit LagUnit { get; set; }

  public XmlPredecessor([NotNull] string uid, DependencyType type, [NotNull] string projectName)
  {
    this.UID = uid;
    this.Type = type;
    this.ProjectName = projectName;
  }
}
