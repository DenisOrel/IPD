// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Evaluator.PossibleValue
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Project.Evaluator;

public class PossibleValue
{
  [NotNull]
  [NotEmpty]
  public readonly string Name;
  [CanBeNull]
  public readonly object Value;

  public PossibleValue([NotNull] string name, [CanBeNull] object value)
  {
    this.Name = name;
    this.Value = value;
  }

  public override string ToString() => this.Name;

  public override int GetHashCode() => this.Name.GetHashCode();
}
