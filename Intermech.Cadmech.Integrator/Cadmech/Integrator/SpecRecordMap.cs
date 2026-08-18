// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.SpecRecordMap
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class SpecRecordMap
{
  private string projDesignation;
  private List<RowData> rows;
  private SpecRecord record;

  public SpecRecordMap() => this.rows = new List<RowData>();

  public string ProjectDesignation
  {
    get => this.projDesignation;
    set => this.projDesignation = value;
  }

  public List<RowData> Rows => this.rows;

  public SpecRecord Record
  {
    get => this.record;
    set => this.record = value;
  }
}
