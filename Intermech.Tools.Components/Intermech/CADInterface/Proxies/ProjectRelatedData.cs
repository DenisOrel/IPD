// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ProjectRelatedData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.CADInterface.Proxies;

internal sealed class ProjectRelatedData
{
  public readonly ModelConfigurationProxy ProjectConfiguration;
  public readonly MeasuredValue Count;
  public readonly long SubstGroup;
  public readonly long SubstNumber;

  public ProjectRelatedData(
    ModelConfigurationProxy projectConfiguration,
    MeasuredValue count,
    long substGroup,
    long substNumber)
  {
    this.ProjectConfiguration = projectConfiguration;
    this.Count = count;
    this.SubstGroup = substGroup;
    this.SubstNumber = substNumber;
  }
}
