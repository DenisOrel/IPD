
// Type: Intermech.Files.FileSettingsEditorModule
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


namespace Intermech.Files;

/// <summary>
/// Реализует модуль, обеспечивающий инициализацию и завершение работы для объектов настроек файлового хранилища.
/// </summary>
internal sealed class FileSettingsEditorModule : InitializerModule
{
  private IFileVaultSettingsService fileVaultSettingsService;
  private IPropertyPagesService propertyPagesService;

  public FileSettingsEditorModule(
    IFileVaultSettingsService fileVaultSettingsService,
    IPropertyPagesService propertyPagesService)
  {
    if (fileVaultSettingsService == null)
      throw new ArgumentNullException(nameof (fileVaultSettingsService));
    if (propertyPagesService == null)
      throw new ArgumentNullException(nameof (propertyPagesService));
    this.fileVaultSettingsService = fileVaultSettingsService;
    this.propertyPagesService = propertyPagesService;
  }

  /// <summary>
  /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
  /// </summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.CreateFileSettingsEditor();
  }

  private void CreateFileSettingsEditor()
  {
    FileSettingEditorModel model = new FileSettingEditorModel(this.fileVaultSettingsService);
    FileSettingsView fileSettingsView = new FileSettingsView();
    FileSettingsPresenter presenter = new FileSettingsPresenter();
    presenter.Model = model;
    presenter.View = (IFileSettingsView) fileSettingsView;
    PropertyPageMvpAdapter page = new PropertyPageMvpAdapter("Общие настройки", (IPropertyPageMvpModel) model, (IView) fileSettingsView, (IPropertyPageMvpPresenter) presenter);
    this.propertyPagesService.AddPage(Path.Combine("Файловое хранилище", page.PageName), (IPropertyPage) page);
  }
}
