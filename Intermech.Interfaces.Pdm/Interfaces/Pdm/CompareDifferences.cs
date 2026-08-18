// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.CompareDifferences
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Изменения в составе</summary>
public class CompareDifferences
{
  /// <summary>
  /// Флаг того, что включена автоматическая группировка по идентификаторам версий объектов в составе1
  /// </summary>
  public bool Grouping;
  /// <summary>Отличия текущего состава для ChildrenView</summary>
  public Dictionary<long, List<int>> Differences;
}
