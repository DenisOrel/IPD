// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.DynamicModelConfigurationNameMangler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.IO;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public sealed class DynamicModelConfigurationNameMangler : IModelConfigurationNameMangler
{
  private readonly string SafeEmptyConfigurationName;

  public DynamicModelConfigurationNameMangler(string safeEmptyConfigurationName)
  {
    this.SafeEmptyConfigurationName = !string.IsNullOrEmpty(safeEmptyConfigurationName) ? safeEmptyConfigurationName : throw new ArgumentException();
  }

  public string ToSafeName(string documentFile, string rawName)
  {
    if (documentFile == null)
      throw new ArgumentNullException(nameof (documentFile));
    return string.IsNullOrEmpty(rawName) ? this.DynamicEmptyConfigurationName(documentFile) : rawName;
  }

  public string ToRawName(string documentFile, string safeName)
  {
    if (documentFile == null)
      throw new ArgumentNullException(nameof (documentFile));
    return safeName == this.SafeEmptyConfigurationName || safeName == this.DynamicEmptyConfigurationName(documentFile) ? string.Empty : safeName;
  }

  private string DynamicEmptyConfigurationName(string documentFile)
  {
    return $"{Path.GetFileNameWithoutExtension(documentFile)} Master Configuration";
  }
}
