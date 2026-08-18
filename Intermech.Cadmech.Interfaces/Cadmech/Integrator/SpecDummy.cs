// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.SpecDummy
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>Реализует макет спецификации.</summary>
public class SpecDummy
{
  private List<SpecRecord> records;
  private List<PartData> parts;

  /// <summary>
  /// Конструктор спрятан, чтобы нельзя было создать экземпляр этого класса.
  /// </summary>
  protected SpecDummy()
  {
    this.records = new List<SpecRecord>();
    this.parts = new List<PartData>();
  }

  /// <summary>
  /// 
  /// </summary>
  public List<SpecRecord> Records => this.records;

  /// <summary>
  /// 
  /// </summary>
  public List<PartData> Parts => this.parts;
}
