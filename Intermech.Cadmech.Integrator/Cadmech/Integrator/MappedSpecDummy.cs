// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.MappedSpecDummy
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class MappedSpecDummy : SpecDummy
{
  private List<SpecRecordMap> recordMaps;

  public MappedSpecDummy() => this.recordMaps = new List<SpecRecordMap>();

  public List<SpecRecordMap> RecordMaps
  {
    get => this.recordMaps;
    set => this.recordMaps = value;
  }
}
