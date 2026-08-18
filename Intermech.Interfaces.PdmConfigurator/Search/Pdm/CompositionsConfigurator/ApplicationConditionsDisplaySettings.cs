// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionsConfigurator.ApplicationConditionsDisplaySettings
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Search.Pdm.CompositionsConfigurator;

[Serializable]
public sealed class ApplicationConditionsDisplaySettings
{
  public ApplicationConditionsDisplaySettings.OptionNameReplacement NameReplacement { get; set; }

  public ApplicationConditionsDisplaySettings.OptionValueReplacement ValueReplacement { get; set; }

  public ApplicationConditionsDisplaySettings.OperatorDisplayType RelationOperatorDisplayType { get; set; }

  public ApplicationConditionsDisplaySettings Clone()
  {
    return new ApplicationConditionsDisplaySettings()
    {
      NameReplacement = this.NameReplacement,
      ValueReplacement = this.ValueReplacement,
      RelationOperatorDisplayType = this.RelationOperatorDisplayType
    };
  }

  public enum OptionNameReplacement
  {
    [Description("Наименование опции")] OptionName,
    [Description("Код опции")] OptionCode,
  }

  public enum OptionValueReplacement
  {
    [Description("Значение опции")] OptionValue,
    [Description("Код значения")] OptionValueCode,
    [Description("Описание значения")] OptionValueDescription,
  }

  public enum OperatorDisplayType
  {
    [Description("Слова")] Words,
    [Description("Спецсимволы")] Symbols,
  }
}
