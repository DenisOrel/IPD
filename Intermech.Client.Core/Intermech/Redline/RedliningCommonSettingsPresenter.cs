
// Type: Intermech.Redline.RedliningCommonSettingsPresenter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Mvp;
using Intermech.UI.PropertyPages;
using System;
using System.Diagnostics;


namespace Intermech.Redline;

/// <summary>
/// Реализует презентер "Общие настройки" для системы красного карандаша. В соответствии с паттерном MVP вся логика взаимодействия с пользователем и
/// обновления визуального контрола реализуется в этом классе. А все взаимодействие с контролом выполняется только через интерфейс IRedliningCommonSettingsView.
/// </summary>
internal sealed class RedliningCommonSettingsPresenter : 
  Presenter<IRedliningCommonSettingsView>,
  IPropertyPageMvpPresenter,
  IPresenter
{
  private RedliningCommonSettingsEditorModel model;

  /// <summary>
  /// Возвращает или задает модель данных для страницы.
  /// Значение свойства должно быть задано до начала использования MVP-посредника.
  /// </summary>
  public RedliningCommonSettingsEditorModel Model
  {
    [DebuggerStepThrough] get => this.model;
    set
    {
      this.CheckAllowPropertyChange();
      this.model = value;
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
    this.View.EditableStateChanged += new EventHandler(this.OnViewEditableStateChanged);
  }

  /// <summary>
  /// Обрабатывает отключение MVP-посредника от MVP-вида.
  /// Метод вызывается как при закрытии вида, так и в случае ошибки подключения к виду.
  /// </summary>
  protected override void OnDetachView()
  {
    this.View.EditableStateChanged -= new EventHandler(this.OnViewEditableStateChanged);
    this.ResetViewState();
    base.OnDetachView();
  }

  /// <summary>
  /// Заполняет элементы MVP-вида значениями из модели (из редактируемого набора настроек).
  /// </summary>
  private void SetupViewState()
  {
    this.View.LaunchScreenShooter = this.Model.EditableState.LaunchScreenShooter.RawValue;
  }

  /// <summary>
  /// Очищает элементы MVP-вида, заполняя их значениями по умолчанию.
  /// </summary>
  private void ResetViewState() => this.View.LaunchScreenShooter = false;

  private void OnViewEditableStateChanged(object sender, EventArgs e)
  {
    if (this.SettingsChanged == null)
      return;
    this.SettingsChanged((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// Сохраняет сделанные пользователем изменения в хранилище настроек.
  /// </summary>
  public void AcceptChanges()
  {
    this.Model.EditableState.LaunchScreenShooter.RawValue = this.View.LaunchScreenShooter;
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
