// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ZoneRecord
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Описывает информацию о зонах для позиции изделия в спецификации.
/// </summary>
public sealed class ZoneRecord
{
  private Guid occurenceGuid;
  private string zone;

  /// <summary>Создает объект.</summary>
  internal ZoneRecord()
  {
  }

  /// <summary>
  /// Возвращает глобальный идентификатор входимости. Он однозначно идентифицирует позицию в рамках состава конкретного изделия.
  /// Это значит, что позиции, составляющение общую часть в исполнениях изделия, будут иметь одинаковый идентификатор входимости
  /// в рамках составов исполнений изделия.
  /// </summary>
  public Guid OccurenceGuid
  {
    get => this.occurenceGuid;
    internal set => this.occurenceGuid = value;
  }

  /// <summary>
  /// Возвращает зоны для выносок позиции, перечисленные через запятую.
  /// </summary>
  public string Zone
  {
    get => this.zone;
    internal set => this.zone = value;
  }
}
