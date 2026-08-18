// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.UpdateResult
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class UpdateResult
{
  private List<SpecRecord> newRecords;

  public UpdateResult() => this.newRecords = new List<SpecRecord>();

  public List<SpecRecord> NewRecords => this.newRecords;
}
