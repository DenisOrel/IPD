// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgOutputData
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class DwgOutputData
{
  private StructFile structFile;
  private SpecDummy spec;

  public StructFile StructFile
  {
    get => this.structFile;
    set => this.structFile = value;
  }

  public SpecDummy Spec
  {
    get => this.spec;
    set => this.spec = value;
  }
}
