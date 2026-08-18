// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadIntegratorSettingsService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using Intermech.Tools.Settings;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadIntegratorSettingsService(IIntegrator owner) : 
  IntegratorSettingsService<AcadIntegratorSettings>(owner),
  IIntegratorSettingsViewModelService
{
  protected override IntegratorSettingsCodec CreateSettingsCodec()
  {
    return (IntegratorSettingsCodec) new AcadIntegratorSettingsCodec(this.Integrator.DisplayName);
  }

  protected override IntegratorSettingsValidator CreateSettingsValidator()
  {
    return (IntegratorSettingsValidator) new AcadIntegratorSettingsValidator(this.Integrator.DisplayName);
  }

  object IIntegratorSettingsViewModelService.CreateViewModel(ISettingsObject settingsObject)
  {
    if (settingsObject == null)
      throw new ArgumentNullException(nameof (settingsObject));
    this.RequireReadyState();
    return (object) new SettingsSurrogate((AcadIntegratorSettings) settingsObject);
  }

  ISettingsObject IIntegratorSettingsViewModelService.CreateSettingsFromViewModel(
    object viewModelObject)
  {
    if (viewModelObject == null)
      throw new ArgumentNullException(nameof (viewModelObject));
    this.RequireReadyState();
    return (ISettingsObject) ((SettingsSurrogate) viewModelObject).ToSettings();
  }

  public AcadSetupSettings GetAppSetupSettings()
  {
    this.RequireReadyState();
    AcadStartupConfiguration startupConfiguration;
    lock (this.Integrator.SyncRoot)
    {
      this.CheckServer();
      startupConfiguration = this.FindStartupConfiguration();
    }
    if (startupConfiguration == null)
      throw this.NoStartupConfigurationForUser();
    return new AcadSetupSettings()
    {
      UseSpecificProfile = startupConfiguration.UseSpecificProfile,
      ProfileName = startupConfiguration.ProfileName,
      WorkDirectory = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true).WorkArea.AreaPath
    };
  }

  private AcadStartupConfiguration FindStartupConfiguration()
  {
    Guid roleId = ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true).RoleGuid;
    return this.GetSettings().StartupConfigurations.Find((Predicate<AcadStartupConfiguration>) (item => item.UserRole != null && item.UserRole.Id == roleId)) ?? this.GetSettings().StartupConfigurations.Find((Predicate<AcadStartupConfiguration>) (item => item.UserRole == null));
  }

  private BadIntegratorSettingsException NoStartupConfigurationForUser()
  {
    Guid roleGuid = ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true).RoleGuid;
    string caption;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      caption = sessionKeeper.Session.GetObject(roleGuid, true).Caption;
    return new BadIntegratorSettingsException(this.Integrator.DisplayName, $"Настройки интегратора не содержат параметров подключения к приложению для роли '{caption}'.");
  }
}
