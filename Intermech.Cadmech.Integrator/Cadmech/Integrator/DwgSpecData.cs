// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgSpecData
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class DwgSpecData
{
  private StructFile structFile;
  private SpecDummy spec;

  public DwgSpecData(StructFile structFile, SpecDummy spec)
  {
    if (structFile == null)
      throw new ArgumentNullException();
    if (spec == null)
      throw new ArgumentNullException();
    this.structFile = structFile;
    this.spec = spec;
  }

  public StructFile StructFile => this.structFile;

  public SpecDummy Spec => this.spec;
}
