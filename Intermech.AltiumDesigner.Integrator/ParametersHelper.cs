// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ParametersHelper
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal static class ParametersHelper
{
  public static string GetParameterName(
    ADIntegratorSettings settings,
    string attributeName,
    bool throwNotSettings)
  {
    Tuple<StringKey, StringKey, bool> tuple = settings.PartAttributesTable.Find((Predicate<Tuple<StringKey, StringKey, bool>>) (val => val.Item1.Equals(attributeName)));
    if (tuple != null)
      return (string) tuple.Item2;
    if (throwNotSettings)
      throw new Exception($"Для компонентов схемы должен быть настроен синхронизируемый параметр, который соответствует атрибуту {attributeName}");
    return (string) null;
  }

  public static object GetParameterValue(Parameter[] parametersCollection, string parameterName)
  {
    return Array.Find<Parameter>(parametersCollection, (Predicate<Parameter>) (p => string.Compare(p.Name, parameterName, true) == 0))?.Value;
  }

  public static object GetParameterValue(
    ADIntegratorSettings settings,
    string attributeName,
    Parameter[] parametersCollection,
    bool throwNotSettings)
  {
    string parameterName = ParametersHelper.GetParameterName(settings, attributeName, throwNotSettings);
    return string.IsNullOrEmpty(parameterName) ? (object) null : ParametersHelper.GetParameterValue(parametersCollection, parameterName);
  }
}
