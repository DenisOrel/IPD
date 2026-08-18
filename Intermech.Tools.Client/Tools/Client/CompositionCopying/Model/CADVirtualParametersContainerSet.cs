// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADVirtualParametersContainerSet
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CADVirtualParametersContainerSet
{
  private CADVirtualParametersContainer documentContainer;
  private Dictionary<string, CADVirtualParametersContainer> configurationContainers;

  public bool IsEmpty => this.documentContainer == null && this.configurationContainers == null;

  public CADVirtualParametersContainer GetOrCreateDocumentContainer()
  {
    if (this.documentContainer == null)
      this.documentContainer = new CADVirtualParametersContainer();
    return this.documentContainer;
  }

  public CADVirtualParametersContainer GetOrCreateConfigurationContainer(string configurationName)
  {
    if (configurationName == null)
      throw new ArgumentNullException(nameof (configurationName));
    if (this.configurationContainers == null)
      this.configurationContainers = new Dictionary<string, CADVirtualParametersContainer>();
    CADVirtualParametersContainer configurationContainer;
    if (!this.configurationContainers.TryGetValue(configurationName, out configurationContainer))
    {
      configurationContainer = new CADVirtualParametersContainer();
      this.configurationContainers.Add(configurationName, configurationContainer);
    }
    return configurationContainer;
  }
}
