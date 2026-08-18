// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.IdentifiedEntity
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Базовый класс для идентифицируемых сущностей
/// по наименованию и обозначению
/// </summary>
[Serializable]
public class IdentifiedEntity
{
  /// <summary>Обозначение</summary>
  public string Designation { get; private set; }

  /// <summary>Наименование</summary>
  public string Name { get; private set; }

  public IdentifiedEntity(string designation, string name)
  {
    this.Designation = designation;
    this.Name = name;
  }
}
