// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ModelConfigurationUtils
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public static class ModelConfigurationUtils
{
  public static List<Tuple<ModelConfigurationPath, ModelConfigurationProxy>> GetConfigurationList(
    CADDocumentProxy document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document), LocalizationHolder.rm.GetString("Tools.Components_292"));
    List<Tuple<ModelConfigurationPath, ModelConfigurationProxy>> configList = new List<Tuple<ModelConfigurationPath, ModelConfigurationProxy>>(256 /*0x0100*/);
    ModelConfigurationUtils.CollectConfigurationList((IModelConfigurationsContainer) document, (ModelConfigurationPath) null, configList);
    return configList;
  }

  private static void CollectConfigurationList(
    IModelConfigurationsContainer provider,
    ModelConfigurationPath parentPath,
    List<Tuple<ModelConfigurationPath, ModelConfigurationProxy>> configList)
  {
    List<ModelConfigurationProxy> configurations = provider.GetConfigurations();
    for (int index = 0; index < configurations.Count; ++index)
    {
      ModelConfigurationProxy provider1 = configurations[index];
      ModelConfigurationPath parentPath1 = parentPath != null ? parentPath.Clone() : new ModelConfigurationPath();
      parentPath1.Add((string) provider1.Name);
      configList.Add(new Tuple<ModelConfigurationPath, ModelConfigurationProxy>(parentPath1, provider1));
      ModelConfigurationUtils.CollectConfigurationList((IModelConfigurationsContainer) provider1, parentPath1, configList);
    }
  }

  public static ModelConfigurationProxy GetConfigurationByPath(
    CADDocumentProxy document,
    ModelConfigurationPath path)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document), LocalizationHolder.rm.GetString("Tools.Components_292"));
    if (path == null)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_306"), nameof (path));
    IModelConfigurationsContainer configurationsContainer = (IModelConfigurationsContainer) document;
    ModelConfigurationProxy configurationByPath = (ModelConfigurationProxy) null;
    foreach (string name in path)
    {
      configurationByPath = configurationsContainer.GetConfiguration(name);
      configurationsContainer = configurationByPath != null ? (IModelConfigurationsContainer) configurationByPath : throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_307"), (object) name, (object) document.MasterFile));
    }
    return configurationByPath;
  }
}
