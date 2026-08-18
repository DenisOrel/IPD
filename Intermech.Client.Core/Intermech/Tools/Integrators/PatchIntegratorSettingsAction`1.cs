
// Type: Intermech.Tools.Integrators.PatchIntegratorSettingsAction`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Settings;
using System;
using System.Xml;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать автоматический патч для настроек интегратора при входе в клиент IPS пользователя с соответствующими правами.
/// </summary>
public abstract class PatchIntegratorSettingsAction<TSettings> : IAction where TSettings : class, ISettingsObject
{
  private readonly IIntegrator integrator;
  private IPersistentIntegratorSettingsService settingsSvc;
  private IOutputView outputView;
  private bool validSettingsOnly;

  protected PatchIntegratorSettingsAction(IIntegrator integrator, bool validSettingsOnly)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
    this.validSettingsOnly = validSettingsOnly;
  }

  public void Perform()
  {
    try
    {
      this.Initialize();
      TSettings settingsToPatch = this.TryGetSettingsToPatch();
      if ((object) settingsToPatch == null || !this.HasPatchRights())
        return;
      this.ApplyPatch(settingsToPatch);
    }
    finally
    {
      this.Cleanup();
    }
  }

  protected virtual void Initialize()
  {
    this.settingsSvc = ServiceUtils.GetService<IPersistentIntegratorSettingsService>((object) this.integrator, true);
    this.outputView = ServiceUtils.GetService<IOutputView>((object) ServicesManager.ServiceContainer, true);
  }

  protected virtual void Cleanup()
  {
    this.settingsSvc = (IPersistentIntegratorSettingsService) null;
    this.outputView = (IOutputView) null;
  }

  protected virtual bool HasPatchRights()
  {
    if (!ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true).IsAdmin)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if ((ServiceUtils.GetService<IToolSecurity>((object) sessionKeeper.Session, true).GetUserRights() & ToolSecurityRights.EditPublicSettings) == ToolSecurityRights.None)
        return false;
    }
    return true;
  }

  protected virtual TSettings TryGetSettingsToPatch()
  {
    if (!IntegratorServices.Exists(this.integrator.Id))
      return default (TSettings);
    XmlDocument settingsXml = new XmlDocument();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IIntegratorServer service = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true);
      settingsXml.LoadXml(service.GetIntegratorData(this.Integrator.Id));
    }
    ISettingsObject settingsObject = this.settingsSvc.DecodeSettings(settingsXml);
    if (this.validSettingsOnly)
    {
      try
      {
        this.settingsSvc.ValidateSettings(settingsObject, SettingsValidatorContext.SettingsObjectOnly);
      }
      catch (BadIntegratorSettingsException ex)
      {
        this.OutputView.WriteString("Ошибки", $"Не удалось выполнить автоматическое обновление настроек интегратора из-за ошибки в настройках. {ex.IntegratorName}: {ex.Message}");
        this.OutputView.WriteString("Ошибки", "Исправьте настройки интегратора, и тогда при следующем входе в систему к ним будет применено необходимое обновление.");
        settingsObject = (ISettingsObject) null;
      }
      catch (Exception ex)
      {
        this.OutputView.WriteString("Ошибки", $"Не удалось выполнить автоматическое обновление настроек интегратора из-за ошибки. {ex.Message}");
        this.OutputView.WriteString("Ошибки", "Исправьте ошибку, и тогда при следующем входе в систему к ним будет применено необходимое обновление.");
        settingsObject = (ISettingsObject) null;
      }
    }
    return (TSettings) settingsObject;
  }

  private void ApplyPatch(TSettings settingsToPatch)
  {
    if (!this.PatchSettings(settingsToPatch))
      return;
    if (this.validSettingsOnly)
    {
      try
      {
        this.settingsSvc.ValidateSettings((ISettingsObject) settingsToPatch, SettingsValidatorContext.SettingsObjectOnly);
      }
      catch (Exception ex)
      {
        this.OutputView.WriteString("Ошибки", $"При выполнении автоматического обновления настроек интегратора произошла ошибка. {this.integrator.DisplayName}: {ex.Message}");
        return;
      }
    }
    this.SaveSettings(settingsToPatch);
  }

  protected virtual bool PatchSettings(TSettings settingsToPatch) => false;

  private void SaveSettings(TSettings settingsToPatch)
  {
    XmlDocument xmlDocument = this.settingsSvc.EncodeSettings((ISettingsObject) settingsToPatch);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).SetIntegratorData(this.Integrator.Id, xmlDocument.OuterXml);
    ServiceUtils.GetService<IntegratorSettingsCacheManager>((object) ApplicationServices.Container, true).ResetCache();
  }

  protected IIntegrator Integrator => this.integrator;

  protected IPersistentIntegratorSettingsService SettingsService => this.settingsSvc;

  protected IOutputView OutputView => this.outputView;
}
