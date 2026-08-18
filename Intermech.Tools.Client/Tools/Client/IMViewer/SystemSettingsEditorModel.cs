// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.SystemSettingsEditorModel
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.UI.PropertyPages;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal class SystemSettingsEditorModel : IPropertyPageMvpModel
{
  private readonly ICurrentUserAndRole currentUserService;
  private readonly IIMViewerClientService imviewerService;
  private IMViewerSystemSettings originalState;
  private IMViewerSystemSettings editableState;

  public SystemSettingsEditorModel(
    ICurrentUserAndRole currentUserService,
    IIMViewerClientService imviewerService)
  {
    this.currentUserService = currentUserService;
    this.imviewerService = imviewerService;
  }

  public ICurrentUserAndRole CurrentUserService
  {
    [DebuggerStepThrough] get => this.currentUserService;
  }

  public IIMViewerClientService IMViewerService
  {
    [DebuggerStepThrough] get => this.imviewerService;
  }

  public IMViewerSystemSettings OriginalState
  {
    [DebuggerStepThrough] get
    {
      this.LazyInitialize();
      return this.originalState;
    }
  }

  public IMViewerSystemSettings EditableState
  {
    [DebuggerStepThrough] get
    {
      this.LazyInitialize();
      return this.editableState;
    }
  }

  public void Reset()
  {
    this.originalState = (IMViewerSystemSettings) null;
    this.editableState = (IMViewerSystemSettings) null;
  }

  public void SaveChanges()
  {
    if (this.originalState == null || this.editableState == null || this.originalState.EnableIntegration == this.editableState.EnableIntegration)
      return;
    IMViewerSystemSettings newSettings = this.editableState.Clone();
    newSettings.Freeze();
    this.UpdateServerSettings(newSettings);
    this.originalState = newSettings;
  }

  private void LazyInitialize()
  {
    if (this.originalState != null)
      return;
    this.originalState = this.GetCurrentServerSettings();
    this.editableState = this.originalState.Clone();
  }

  private IMViewerSystemSettings GetCurrentServerSettings()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IIMViewerServerService>((object) sessionKeeper.Session, true).Settings;
  }

  private void UpdateServerSettings(IMViewerSystemSettings newSettings)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IIMViewerServerService>((object) sessionKeeper.Session, true).UpdateSettings(sessionKeeper.Session, newSettings);
  }
}
