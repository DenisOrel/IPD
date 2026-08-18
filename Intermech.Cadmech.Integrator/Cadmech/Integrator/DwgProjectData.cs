// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgProjectData
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class DwgProjectData
{
  private bool baseProject;

  public DwgProjectData(bool baseProject) => this.baseProject = baseProject;

  public bool BaseProject => this.baseProject;
}
