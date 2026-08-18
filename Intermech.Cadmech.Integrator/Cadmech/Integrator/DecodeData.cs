// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DecodeData
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System.Data;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class DecodeData
{
  private CreateSpecJob job;
  private DataTable structTable;
  private FileContent fieldLayoutFile;
  private StructFile structFile;

  public DecodeData() => this.structFile = new StructFile();

  public CreateSpecJob Job
  {
    get => this.job;
    set => this.job = value;
  }

  public DataTable StructTable
  {
    get => this.structTable;
    set => this.structTable = value;
  }

  public FileContent FieldLayoutFile
  {
    get => this.fieldLayoutFile;
    set => this.fieldLayoutFile = value;
  }

  public StructFile StructFile => this.structFile;
}
