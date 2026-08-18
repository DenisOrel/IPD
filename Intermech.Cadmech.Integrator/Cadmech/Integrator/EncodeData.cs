// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.EncodeData
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Data;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class EncodeData
{
  private BaseSpecJob job;
  private StructFile structFile;
  private DataTable structTable;

  public BaseSpecJob Job
  {
    get => this.job;
    set => this.job = value;
  }

  public StructFile StructFile
  {
    get => this.structFile;
    set => this.structFile = value;
  }

  public DataTable StructTable
  {
    get => this.structTable;
    set => this.structTable = value;
  }
}
