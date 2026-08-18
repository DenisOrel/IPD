// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ImbaseSyncTypes
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

[Description("Результаты синхронизации")]
public enum ImbaseSyncTypes
{
  /// <summary>Не определено</summary>
  [Description("Не определено")] Unknown = -1, // 0xFFFFFFFF
  /// <summary>Не задано искомое значение</summary>
  [Description("Не задано искомое значение")] EmptyValue = 0,
  /// <summary>Не найден</summary>
  [Description("Не найден в Imbase")] NotFound = 1,
  /// <summary>Запрещен к применению</summary>
  [Description(" Запрещен к применению")] Forbidden = 2,
  /// <summary>Синхронизация</summary>
  [Description("Синхронизация")] Normal = 3,
}
