// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ComponentsGroupHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

internal static class ComponentsGroupHelper
{
  public const string ReplaceAndTuningPositionSymbol = "$";
  public const string ReplacePositionSymbol = "@";
  public const string TuningPositionSymbol = "&";
  public const string SimplePositionSymbol = "#";

  public static string GetGroupID(
    FunctionalGroup functionalGroup,
    string posDesignation,
    bool replace,
    bool tuning)
  {
    if (replace & tuning && functionalGroup != null)
      return $"{"$"}{functionalGroup.PosDesignation}{posDesignation}";
    if (replace && functionalGroup != null)
      return $"{"@"}{functionalGroup.PosDesignation}{posDesignation}";
    if (tuning && functionalGroup != null)
      return $"{"&"}{functionalGroup.PosDesignation}{posDesignation}";
    if (replace & tuning)
      return $"{"$"}{posDesignation}";
    if (replace)
      return $"{"@"}{posDesignation}";
    if (tuning)
      return $"{"&"}{posDesignation}";
    return functionalGroup != null ? functionalGroup.PosDesignation : "#";
  }
}
