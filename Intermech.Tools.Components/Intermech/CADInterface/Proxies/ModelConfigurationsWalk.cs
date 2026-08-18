// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ModelConfigurationsWalk
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

internal sealed class ModelConfigurationsWalk
{
  public IEnumerable<ModelConfigurationProxy> Walk(CADDocumentProxy document, bool openVisible)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (document.HasConfigurations)
    {
      ICollection<string> alreadyProcessed = (ICollection<string>) new OrderedList<string>();
      ModelConfigurationProxy firstConfiguration = document.DefaultConfiguration;
      yield return firstConfiguration;
      alreadyProcessed.Add((string) firstConfiguration.Name);
      foreach (ModelConfigurationProxy walkChild in this.WalkChildren((IModelConfigurationsContainer) firstConfiguration, openVisible, alreadyProcessed))
        yield return walkChild;
      foreach (ModelConfigurationProxy walkChild in this.WalkChildren((IModelConfigurationsContainer) document, openVisible, alreadyProcessed))
        yield return walkChild;
    }
  }

  private IEnumerable<ModelConfigurationProxy> WalkChildren(
    IModelConfigurationsContainer parent,
    bool openVisible,
    ICollection<string> alreadyProcessed)
  {
    List<string> configurationNames = parent.GetConfigurationNames();
    List<ModelConfigurationProxy> queue = new List<ModelConfigurationProxy>(configurationNames.Count);
    foreach (string str in configurationNames)
    {
      string name = str;
      if (!alreadyProcessed.Contains(name))
      {
        ModelConfigurationProxy childConfiguration = parent.GetConfiguration(name, openVisible);
        yield return childConfiguration;
        alreadyProcessed.Add(name);
        queue.Add(childConfiguration);
        childConfiguration = (ModelConfigurationProxy) null;
        name = (string) null;
      }
    }
    foreach (IModelConfigurationsContainer parent1 in queue)
    {
      foreach (ModelConfigurationProxy walkChild in this.WalkChildren(parent1, openVisible, alreadyProcessed))
        yield return walkChild;
    }
  }
}
