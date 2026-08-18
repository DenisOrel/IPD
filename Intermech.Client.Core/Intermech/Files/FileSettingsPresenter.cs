
// Type: Intermech.Files.FileSettingsPresenter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Mvp;
using Intermech.UI.PropertyPages;
using System;
using System.Diagnostics;


namespace Intermech.Files;

internal sealed class FileSettingsPresenter : 
  Presenter<IFileSettingsView>,
  IPropertyPageMvpPresenter,
  IPresenter
{
  private FileSettingEditorModel model;
  private bool? isAdmin;

  public FileSettingsPresenter() => this.isAdmin = new bool?();

  /// <summary>
  /// Возвращает или задает модель данных для страницы.
  /// Значение свойства должно быть задано до начала использования MVP-посредника.
  /// </summary>
  public FileSettingEditorModel Model
  {
    [DebuggerStepThrough] get => this.model;
    set
    {
      this.CheckAllowPropertyChange();
      this.model = value;
    }
  }

  /// <summary>
  /// Возвращает, является ли текущий пользователь администратором или нет.
  /// Данное значение кэшируется.
  /// </summary>
  private bool IsAdmin
  {
    get
    {
      if (!this.isAdmin.HasValue)
        this.LoadIsAdmin();
      return this.isAdmin.Value;
    }
  }

  /// <summary>
  /// Проверяет корректность инициализации MVP-посредника.
  /// Метод вызывается непосредственно перед подключением MVP-посредника к MVP-виду.
  /// Необработанное исключение в этом методе прерывает процесс подключения.
  /// </summary>
  /// <exception cref="T:Intermech.Mvp.PresenterPropertyException">Указанное свойство посредника некорректно</exception>
  /// <exception cref="T:Intermech.Mvp.MvpException">Посредник не был корректно инициализирован</exception>
  protected override void DoValidate()
  {
    base.DoValidate();
    if (this.Model == null)
      throw new PresenterPropertyException("Model");
  }

  /// <summary>
  /// Выполняет подключение MVP-посредника к MVP-виду.
  /// Необработанное исключение в этом методе прерывает процесс подключения и
  /// запускает процесс отключения.
  /// </summary>
  protected override void OnAttachView()
  {
    base.OnAttachView();
    this.SetupViewState();
    this.View.AttachPageChangedHandlers();
    this.View.EditableStateChanged += new EventHandler(this.OnViewEditableStateChanged);
  }

  protected override void OnDetachView()
  {
    this.View.EditableStateChanged -= new EventHandler(this.OnViewEditableStateChanged);
    this.ResetViewState();
    this.View.DetachPageChangedHandlers();
    base.OnDetachView();
  }

  /// <summary>
  /// Заполняет элементы MVP-вида значениями из модели (из редактируемого набора настроек).
  /// </summary>
  private void SetupViewState()
  {
    this.View.DriveLetter = this.Model.EditableState.DriveLetter.RawValue;
    this.View.EnableDriveLetter(this.IsAdmin);
    this.View.SymlinkFolder = this.Model.EditableState.SymlinkFolder.RawValue;
    this.View.EnableSymlinkFolder(this.IsAdmin);
    this.View.LeaveSourcesOfImportedFiles = this.Model.EditableState.LeaveSourcesOfImportedFiles.RawValue;
    this.View.EnableImportOptions(this.IsAdmin);
  }

  /// <summary>
  /// Очищает элементы MVP-вида, заполняя их значениями по умолчанию.
  /// </summary>
  private void ResetViewState()
  {
    this.View.DriveLetter = char.MinValue;
    this.View.SymlinkFolder = (string) null;
    this.View.LeaveSourcesOfImportedFiles = false;
  }

  private void OnViewEditableStateChanged(object sender, EventArgs e)
  {
    if (this.SettingsChanged == null)
      return;
    this.SettingsChanged((object) this, EventArgs.Empty);
  }

  private void LoadIsAdmin()
  {
    this.isAdmin = new bool?(ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true).IsAdmin);
  }

  /// <summary>
  /// Сохраняет сделанные пользователем изменения в хранилище настроек.
  /// </summary>
  public void AcceptChanges()
  {
    this.Model.EditableState.DriveLetter.RawValue = this.View.DriveLetter;
    this.Model.EditableState.SymlinkFolder.RawValue = this.View.SymlinkFolder;
    this.Model.EditableState.LeaveSourcesOfImportedFiles.RawValue = this.View.LeaveSourcesOfImportedFiles;
  }

  /// <summary>
  /// Отменяет сделанные пользователем изменения и восстанавливает настройки из хранилища.
  /// </summary>
  public void RevertChanges()
  {
    this.Model.Reset();
    this.SetupViewState();
  }

  /// <summary>Событие изменения параметров пользователем.</summary>
  public event EventHandler SettingsChanged;
}
