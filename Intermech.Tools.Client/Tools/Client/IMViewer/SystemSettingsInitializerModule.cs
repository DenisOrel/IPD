// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.SystemSettingsInitializerModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Mvp;
using Intermech.UI.PropertyPages;
using System;
using System.IO;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal sealed class SystemSettingsInitializerModule : InitializerModule
{
  private IPropertyPagesService propertyPagesService;
  private Func<SystemSettingsEditorModel> systemSettingsEditorModelFactory;

  public SystemSettingsInitializerModule(
    IPropertyPagesService propertyPagesService,
    Func<SystemSettingsEditorModel> systemSettingsEditorModelFactory)
  {
    if (propertyPagesService == null)
      throw new ArgumentNullException(nameof (propertyPagesService));
    if (systemSettingsEditorModelFactory == null)
      throw new ArgumentNullException(nameof (systemSettingsEditorModelFactory));
    this.propertyPagesService = propertyPagesService;
    this.systemSettingsEditorModelFactory = systemSettingsEditorModelFactory;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.RegisterSystemSettingsEditorPage();
  }

  private void RegisterSystemSettingsEditorPage()
  {
    SystemSettingsEditorModel model = this.systemSettingsEditorModelFactory();
    SystemSettingsEditorView settingsEditorView = new SystemSettingsEditorView();
    SystemSettingsEditorPresenter presenter = new SystemSettingsEditorPresenter();
    presenter.Model = model;
    presenter.View = (ISystemSettingsEditorView) settingsEditorView;
    PropertyPageMvpAdapter page = new PropertyPageMvpAdapter("Интеграция с IMViewer", (IPropertyPageMvpModel) model, (IView) settingsEditorView, (IPropertyPageMvpPresenter) presenter);
    this.propertyPagesService.AddPage(Path.Combine("Система", page.PageName), (IPropertyPage) page);
  }
}
