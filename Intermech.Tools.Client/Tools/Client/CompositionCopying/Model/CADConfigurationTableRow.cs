// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADConfigurationTableRow
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CADConfigurationTableRow
{
  public CADConfigurationTableRow(string masterPath, string name, string configurationPath)
  {
    this.MasterPath = masterPath;
    this.Name = name;
    this.ConfigurationPath = configurationPath;
  }

  public string MasterPath { get; private set; }

  public string Name { get; private set; }

  public string ConfigurationPath { get; private set; }
}
