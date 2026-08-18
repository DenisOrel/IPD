// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.EmptyModelConfigurationNameMangler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public sealed class EmptyModelConfigurationNameMangler : IModelConfigurationNameMangler
{
  public string ToSafeName(string documentFile, string rawName)
  {
    if (documentFile == null)
      throw new ArgumentNullException(nameof (documentFile));
    return rawName;
  }

  public string ToRawName(string documentFile, string safeName)
  {
    if (documentFile == null)
      throw new ArgumentNullException(nameof (documentFile));
    return safeName;
  }
}
