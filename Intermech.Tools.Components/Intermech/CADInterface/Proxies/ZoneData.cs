// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ZoneData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.CADInterface.Proxies;

internal sealed class ZoneData
{
  public readonly ModelConfigurationProxy ProjectConfiguration;
  public readonly string Zone;

  public ZoneData(ModelConfigurationProxy projectConfiguration, string zone)
  {
    this.ProjectConfiguration = projectConfiguration;
    this.Zone = zone;
  }
}
