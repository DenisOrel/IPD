
// Type: Intermech.Redline.RedliningSettingsEditorModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Mvp;
using Intermech.UI.PropertyPages;
using System;
using System.IO;


namespace Intermech.Redline;

/// <summary>
/// Модуль инициализации основных сервисов красного карандаша.
/// </summary>
internal sealed class RedliningSettingsEditorModule : InitializerModule
{
  private IPropertyPagesService propertyPagesService;

  public RedliningSettingsEditorModule(IPropertyPagesService propertyPagesService)
  {
    this.propertyPagesService = propertyPagesService != null ? propertyPagesService : throw new ArgumentNullException(nameof (propertyPagesService));
  }

  /// <summary>
  /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
  /// </summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.InstallSettingsEditor();
  }

  private void InstallSettingsEditor()
  {
    RedliningCommonSettingsEditorModel model = new RedliningCommonSettingsEditorModel();
    RedliningCommonSettingsPage commonSettingsPage = new RedliningCommonSettingsPage();
    RedliningCommonSettingsPresenter presenter = new RedliningCommonSettingsPresenter();
    presenter.Model = model;
    presenter.View = (IRedliningCommonSettingsView) commonSettingsPage;
    PropertyPageMvpAdapter page = new PropertyPageMvpAdapter("Общие настройки", (IPropertyPageMvpModel) model, (IView) commonSettingsPage, (IPropertyPageMvpPresenter) presenter);
    this.propertyPagesService.AddPage(Path.Combine("Система\\Красный карандаш", page.PageName), (IPropertyPage) page);
  }
}
