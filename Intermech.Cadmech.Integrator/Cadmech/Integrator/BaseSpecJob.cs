// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.BaseSpecJob
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class BaseSpecJob
{
  private StructFileProcessingModes processingMode;
  private bool suffixMode;

  public StructFileProcessingModes ProcessingMode
  {
    get => this.processingMode;
    set => this.processingMode = value;
  }

  public bool SuffixMode
  {
    get => this.suffixMode;
    set => this.suffixMode = value;
  }
}
