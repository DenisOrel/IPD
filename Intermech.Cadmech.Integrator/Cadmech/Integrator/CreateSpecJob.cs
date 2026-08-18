// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.CreateSpecJob
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class CreateSpecJob : BaseSpecJob
{
  private string baseProjDesignation;
  private string baseProjName;

  public string BaseProjectDesignation
  {
    get => this.baseProjDesignation;
    set => this.baseProjDesignation = value;
  }

  public string BaseProjectName
  {
    get => this.baseProjName;
    set => this.baseProjName = value;
  }
}
