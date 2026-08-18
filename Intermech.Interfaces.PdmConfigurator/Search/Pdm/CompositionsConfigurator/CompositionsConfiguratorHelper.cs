// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionsConfigurator.CompositionsConfiguratorHelper
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Interfaces;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Intermech.Search.Pdm.CompositionsConfigurator;

public static class CompositionsConfiguratorHelper
{
  public static bool IsConfigurableRelationTypeID(int relationTypeID)
  {
    return !RelationTypeHelper.IsUnknownRelationTypeID(relationTypeID) ? MetaDataHelper.IsPdmConfigurableRelationType(relationTypeID) : throw new ArgumentException();
  }

  public static bool IsAllConfigurableRelationTypeIds(IEnumerable<int> relationTypeIds)
  {
    if (relationTypeIds == null)
      throw new ArgumentNullException(nameof (relationTypeIds));
    return relationTypeIds.Where<int>((Func<int, bool>) (o => !CompositionsConfiguratorHelper.IsConfigurableRelationTypeID(o))).Count<int>() == 0;
  }

  public static string ConvertApplicationConditionsDisplaySettingsToStringForSaveToConfiguration(
    ApplicationConditionsDisplaySettings settings)
  {
    if (settings == null)
      throw new ArgumentNullException("settigns");
    return $"{(int) settings.NameReplacement}|{(int) settings.ValueReplacement}|{(int) settings.RelationOperatorDisplayType}";
  }

  public static ApplicationConditionsDisplaySettings ConvertStringLoadedFromConfigurationToApplicationConditionsDisplaySettings(
    string text)
  {
    string[] strArray = !string.IsNullOrEmpty(text) ? text.Split('|') : throw new ArgumentException();
    ApplicationConditionsDisplaySettings conditionsDisplaySettings = new ApplicationConditionsDisplaySettings()
    {
      NameReplacement = (ApplicationConditionsDisplaySettings.OptionNameReplacement) Convert.ToInt32(strArray[0]),
      ValueReplacement = (ApplicationConditionsDisplaySettings.OptionValueReplacement) Convert.ToInt32(strArray[1])
    };
    if (strArray.Length > 2)
      conditionsDisplaySettings.RelationOperatorDisplayType = (ApplicationConditionsDisplaySettings.OperatorDisplayType) Convert.ToInt32(strArray[2]);
    return conditionsDisplaySettings;
  }

  public static string GetOptionNameReplacemenetForDisplayApplicationConditions(
    OptionHolder optionHolder,
    ApplicationConditionsDisplaySettings settings)
  {
    if (optionHolder == null)
      throw new ArgumentNullException(nameof (optionHolder));
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    if (settings.NameReplacement == ApplicationConditionsDisplaySettings.OptionNameReplacement.OptionName)
      return optionHolder.OptionCaption;
    if (settings.NameReplacement != ApplicationConditionsDisplaySettings.OptionNameReplacement.OptionCode)
      throw new NotSupportedEnumException((Enum) settings.NameReplacement);
    return string.IsNullOrEmpty(optionHolder.OptionCode) ? optionHolder.OptionCaption : optionHolder.OptionCode;
  }

  public static string GetOptionValueReplacementForDisplayApplicationConditions(
    OptionHolder optionHolder,
    OptionValue optionValue,
    ApplicationConditionsDisplaySettings settings)
  {
    if (optionHolder == null)
      throw new ArgumentNullException(nameof (optionHolder));
    if (optionValue == null)
      throw new ArgumentNullException(nameof (optionValue));
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    if (settings.ValueReplacement == ApplicationConditionsDisplaySettings.OptionValueReplacement.OptionValue)
      return optionValue.GetDisplayValue(optionHolder);
    if (settings.ValueReplacement == ApplicationConditionsDisplaySettings.OptionValueReplacement.OptionValueCode)
      return string.IsNullOrEmpty(optionValue.Code) ? optionValue.GetDisplayValue(optionHolder) : optionValue.Code;
    if (settings.ValueReplacement != ApplicationConditionsDisplaySettings.OptionValueReplacement.OptionValueDescription)
      throw new NotSupportedEnumException((Enum) settings.ValueReplacement);
    return string.IsNullOrEmpty(optionValue.Description) ? optionValue.GetDisplayValue(optionHolder) : optionValue.Description;
  }

  public static string GetOperatorForDisplayApplicationConditions(
    Operator @operator,
    ApplicationConditionsDisplaySettings settings)
  {
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    if (settings.RelationOperatorDisplayType == ApplicationConditionsDisplaySettings.OperatorDisplayType.Symbols)
    {
      switch (@operator)
      {
        case Operator.Undefined:
          return string.Empty;
        case Operator.Less:
          return "<";
        case Operator.LessEquals:
          return "≤";
        case Operator.Equals:
          return "=";
        case Operator.GreaterEquals:
          return "≥";
        case Operator.Greater:
          return ">";
        case Operator.NotEquals:
          return "≠";
        default:
          throw new NotSupportedEnumException((Enum) @operator);
      }
    }
    else
    {
      if (settings.RelationOperatorDisplayType == ApplicationConditionsDisplaySettings.OperatorDisplayType.Words)
        return CompositionsConfiguratorHelper.GetDescriptionForEnum((Enum) @operator).ToLowerInvariant();
      throw new NotSupportedEnumException((Enum) settings.RelationOperatorDisplayType);
    }
  }

  private static string GetDescriptionForEnum(Enum @enum)
  {
    return !(Attribute.GetCustomAttribute((MemberInfo) @enum.GetType().GetField(@enum.ToString()), typeof (DescriptionAttribute)) is DescriptionAttribute customAttribute) ? string.Empty : customAttribute.Description;
  }
}
