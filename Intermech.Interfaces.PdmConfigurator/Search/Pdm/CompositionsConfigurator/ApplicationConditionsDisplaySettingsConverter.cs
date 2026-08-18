// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionsConfigurator.ApplicationConditionsDisplaySettingsConverter
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Extensions;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Search.Pdm.CompositionsConfigurator;

public sealed class ApplicationConditionsDisplaySettingsConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    ApplicationConditionsDisplaySettings conditionsDisplaySettings = (ApplicationConditionsDisplaySettings) null;
    if (value is ApplicationConditionsDisplaySettings)
      conditionsDisplaySettings = (ApplicationConditionsDisplaySettings) value;
    else if (value is string && !string.IsNullOrEmpty((string) value))
      conditionsDisplaySettings = CompositionsConfiguratorHelper.ConvertStringLoadedFromConfigurationToApplicationConditionsDisplaySettings((string) value);
    return conditionsDisplaySettings != null ? (object) $"{conditionsDisplaySettings.NameReplacement.GetDescription<ApplicationConditionsDisplaySettings.OptionNameReplacement>()}, {conditionsDisplaySettings.ValueReplacement.GetDescription<ApplicationConditionsDisplaySettings.OptionValueReplacement>()}, {conditionsDisplaySettings.RelationOperatorDisplayType.GetDescription<ApplicationConditionsDisplaySettings.OperatorDisplayType>()}" : (object) null;
  }
}
