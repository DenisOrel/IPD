// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.Configuration
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

[Serializable]
public class Configuration : IEquatable<Configuration>
{
  public ConfigurationType Type { get; set; }

  public string Name { get; set; }

  public List<ColumnConfiguration> ColumnConfigurations { get; } = new List<ColumnConfiguration>();

  public CommonImportOptions CommonImportOptions { get; set; }

  public override bool Equals(object obj) => this.Equals(obj as Configuration);

  public bool Equals(Configuration other)
  {
    return other != null && this.Type == other.Type && this.Name == other.Name && this.ColumnConfigurations.SequenceEqual<ColumnConfiguration>((IEnumerable<ColumnConfiguration>) other.ColumnConfigurations) && this.CommonImportOptions == other.CommonImportOptions;
  }

  public override int GetHashCode()
  {
    return (((1907092536 * -1521134295 + this.Type.GetHashCode()) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Name)) * -1521134295 + EqualityComparer<List<ColumnConfiguration>>.Default.GetHashCode(this.ColumnConfigurations)) * -1521134295 + this.CommonImportOptions.GetHashCode();
  }
}
