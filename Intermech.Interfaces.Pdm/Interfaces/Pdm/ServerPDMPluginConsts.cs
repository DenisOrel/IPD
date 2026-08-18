// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ServerPDMPluginConsts
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Свалка констант для класса ServerPDMPlugin (серверный плагин "PDM")
/// </summary>
public static class ServerPDMPluginConsts
{
  /// <summary>Название плагина - "Серверная часть InterMech.PDM"</summary>
  public static readonly string PDMPluginName = LocalizationHolder.rm.GetString("Interfaces.Pdm_53");
  /// <summary>
  /// Нельзя удалять связь (ID = {0}), поскольку она входит в состав допустимых заменителей. Перед удалением необходимо исключить её из группы допустимых заменителей.
  /// </summary>
  public static readonly string Exception1 = LocalizationHolder.rm.GetString("Interfaces.Pdm_54");
}
