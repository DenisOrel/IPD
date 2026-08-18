// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionsConfigurator.CompositionsConfiguratorModule
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Search.Configuration;

#nullable disable
namespace Intermech.Search.Pdm.CompositionsConfigurator;

public sealed class CompositionsConfiguratorModule
{
  public void Load()
  {
    ServiceLocator.Get<IConfigurationOptionInfoProvider>().Register(new ConfigurationOptionInfo(typeof (string))
    {
      Description = "Настройки отображения условий применения в редакторе и спецификации",
      DisplayName = "Настройки отображения условий применения",
      Key = CompositionsConfiguratorConfigurationOptionKeys.ApplicationConditionsDisplaySettings,
      Mode = DBConfigMode.GlobalOnly,
      Page = "Система/Конфигуратор составов",
      TypeConverter = typeof (ApplicationConditionsDisplaySettingsConverter)
    });
  }
}
