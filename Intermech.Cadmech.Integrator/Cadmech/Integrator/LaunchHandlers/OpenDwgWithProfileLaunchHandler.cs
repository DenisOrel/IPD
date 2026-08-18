// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.LaunchHandlers.OpenDwgWithProfileLaunchHandler
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.Files;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Tools;
using Intermech.Tools.Integrators;
using Intermech.Tools.LaunchActions;
using Intermech.Tools.Settings;
using System;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace Intermech.Cadmech.Integrator.LaunchHandlers;

internal sealed class OpenDwgWithProfileLaunchHandler : ILaunchHandler, ILaunchHandlerFileEvents
{
  private IFileVault fileVaultService;
  private IIntegratorRegistry integratorRegistry;
  private OpenDwgWithProfileSettingCodec settingsCodec;
  private OpenDwgWithProfileSettingsValidator settingsValidator;

  public OpenDwgWithProfileLaunchHandler(
    IFileVault fileVaultService,
    IIntegratorRegistry integratorRegistry)
  {
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (integratorRegistry == null)
      throw new ArgumentNullException(nameof (integratorRegistry));
    this.fileVaultService = fileVaultService;
    this.integratorRegistry = integratorRegistry;
    this.settingsCodec = new OpenDwgWithProfileSettingCodec();
    this.settingsValidator = new OpenDwgWithProfileSettingsValidator();
  }

  public Guid Id => OpenDwgWithProfileSettings.HandlerID;

  public string DisplayName => "Открыть в AutoCAD с профилем";

  public string GetServerObjectTemplate()
  {
    return this.settingsCodec.Encode(this.settingsCodec.CreateEmptySettings()).InnerXml;
  }

  public DataEditorControl CreateSettingsEditor()
  {
    return (DataEditorControl) new OpenDwgWithProfileSettingsEditor();
  }

  public void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    OpenDwgWithProfileSettings withProfileSettings = handlerData != null ? (OpenDwgWithProfileSettings) this.settingsCodec.Decode(handlerData) : throw new ArgumentNullException(nameof (handlerData));
    this.settingsValidator.Validate((ISettingsObject) withProfileSettings, SettingsValidatorContext.Generic);
    if (string.IsNullOrEmpty(launchParams.ObjectFileName))
      launchParams.ObjectFileName = this.fileVaultService.DBFilesInfo.GetMasterFileName(launchParams.ObjectId, true);
    if (launchParams.FileArea == null)
      launchParams.FileArea = launchParams.LaunchType == LaunchType.Edit ? (IFileArea) this.fileVaultService.WorkArea : (IFileArea) this.fileVaultService.ViewArea;
    launchParams.ResultFilePath = this.fileVaultService.PublishTree(launchParams.ObjectId, launchParams.ObjectFileName, launchParams.VersionsRule, launchParams.FileArea);
    if (this.AfterPublishFile != null)
      this.AfterPublishFile((object) this, new LaunchHandlerEventArgs(launchParams));
    using (AcadApiSession apiSession = new AcadApiSession(this.integratorRegistry.GetIntegrator(new IntegratorObject(AcadConsts.IntegratorId, AcadConsts.IntegratorName), true)))
    {
      this.SetAppProfile(apiSession, withProfileSettings.ProfileName);
      this.OpenAppDocument(apiSession, launchParams);
    }
  }

  private void SetAppProfile(AcadApiSession apiSession, string profileName)
  {
    ICadProxy application = apiSession.Application;
    if (application.ActiveProfile.Equals(profileName))
      return;
    try
    {
      application.ActiveProfile = profileName;
    }
    catch (ApplicationProxyException ex)
    {
      if (ex.InnerException != null && ((ExternalException) ex.InnerException).ErrorCode == -2147467259 /*0x80004005*/)
        throw new FaultException($"Не удалось установить в AutoCAD профиль '{profileName}', так как указанный профиль не найден. Измените имя профиля в настройках команды запуска приложения или создайте указанный профиль в AutoCAD, а затем повторите попытку.");
      throw;
    }
  }

  private void OpenAppDocument(AcadApiSession apiSession, LaunchParams launchParams)
  {
    ICadProxy application = apiSession.Application;
    application.OpenDocument(launchParams.ResultFilePath).Activate();
    application.SwitchToApp();
  }

  public void BeforeLaunch(LaunchParams launchParams, XmlDocument handlerData)
  {
  }

  public event EventHandler<LaunchHandlerEventArgs> AfterPublishFile;
}
