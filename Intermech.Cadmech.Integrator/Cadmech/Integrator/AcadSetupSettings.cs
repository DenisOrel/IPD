// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadSetupSettings
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadSetupSettings
{
  private bool useSpecificProfile;
  private string profileName;
  private string workDirectory;

  public bool UseSpecificProfile
  {
    get => this.useSpecificProfile;
    set => this.useSpecificProfile = value;
  }

  public string ProfileName
  {
    get => this.profileName;
    set => this.profileName = value;
  }

  public string WorkDirectory
  {
    get => this.workDirectory;
    set => this.workDirectory = value;
  }
}
