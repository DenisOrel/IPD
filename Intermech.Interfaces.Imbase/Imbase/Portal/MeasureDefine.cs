// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.MeasureDefine
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Imbase.Portal;

/// <summary>
/// Поиск значения единицы измерения исходя из значения записанного в старом Imbase.
/// Базовый класс для различных задач импорта.
/// </summary>
internal class MeasureDefine
{
  protected virtual Guid FindMeasureGuid(string unit) => Guid.Empty;

  protected virtual Guid FindDefaultMeasureGuid(long physicalValueID) => Guid.Empty;

  internal Guid GetMeasure(long physicalValueID, string unit)
  {
    Guid measure = Guid.Empty;
    if (unit != null && unit != string.Empty)
      measure = this.FindMeasureGuid(unit);
    if (measure == Guid.Empty && physicalValueID != 0L)
      measure = this.FindDefaultMeasureGuid(physicalValueID);
    return measure;
  }
}
