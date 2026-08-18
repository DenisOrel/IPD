// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ConfigurationOptionChangedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

[Serializable]
public sealed class ConfigurationOptionChangedEventArgs : NotificationEventArgs
{
  public ConfigurationOptionChangedEventArgs(
    string moduleName,
    string sectionId,
    string paramName,
    object newValue)
    : base("ConfigurationOptionChanged")
  {
    if (string.IsNullOrEmpty(moduleName))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(sectionId))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(paramName))
      throw new ArgumentException();
    this.ModuleName = moduleName;
    this.SectionId = sectionId;
    this.ParamName = paramName;
    this.NewValue = newValue;
  }

  public string ModuleName { get; set; }

  public string SectionId { get; set; }

  public string ParamName { get; set; }

  public object NewValue { get; set; }
}
