// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadIntegratorSettingsValidator
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Settings;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadIntegratorSettingsValidator : IntegratorSettingsValidator
{
  public AcadIntegratorSettingsValidator(string integratorName)
    : base(integratorName)
  {
    this.AddCheck((ISettingsValidatorCheck) new StartupConfigurationsCheck());
    this.AddCheck((ISettingsValidatorCheck) new MechanicalSchemaCheck());
    this.AddCheck((ISettingsValidatorCheck) new ConstructionalSchemaCheck());
    this.AddCheck((ISettingsValidatorCheck) new DocumentTypeUniquenessCheck());
  }
}
