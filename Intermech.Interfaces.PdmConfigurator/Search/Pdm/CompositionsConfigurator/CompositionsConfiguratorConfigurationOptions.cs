// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionsConfigurator.CompositionsConfiguratorConfigurationOptions
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Search.Configuration;

#nullable disable
namespace Intermech.Search.Pdm.CompositionsConfigurator;

public static class CompositionsConfiguratorConfigurationOptions
{
  public static ApplicationConditionsDisplaySettings ApplicationConditionsDisplaySettings
  {
    get
    {
      string text = ServiceLocator.Get<IConfigurationOptionRepository>().Find(CompositionsConfiguratorConfigurationOptionKeys.ApplicationConditionsDisplaySettings) as string;
      return !string.IsNullOrEmpty(text) ? CompositionsConfiguratorHelper.ConvertStringLoadedFromConfigurationToApplicationConditionsDisplaySettings(text) : (ApplicationConditionsDisplaySettings) null;
    }
  }
}
