
// Type: Intermech.Tools.CommonTasks.StandaloneViewOptionsPresenter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Mvp;
using Intermech.Runtime;
using System;


namespace Intermech.Tools.CommonTasks;

/// <summary>
/// Посредник вида MVP для редактора опций просмотра по команде "Смотреть...".
/// </summary>
internal sealed class StandaloneViewOptionsPresenter : Presenter<IStandaloneViewOptionsEditorView>
{
  private StandaloneViewAdjustmentOptions adjustmentOptions;
  private StandaloneViewOperationModifiers operationModifiers;

  /// <summary>
  /// Возвращает или задает редактируемый набор опций регулировки настроек автономного просмотра.
  /// </summary>
  public StandaloneViewAdjustmentOptions AdjustmentOptions
  {
    get => this.adjustmentOptions;
    set
    {
      if (this.adjustmentOptions == value)
        return;
      this.CheckAllowPropertyChange();
      this.adjustmentOptions = value;
    }
  }

  /// <summary>
  /// Возвращает или задает редактируемый набор модификаторов для операции подготовки документа к автономному просмотру.
  /// </summary>
  public StandaloneViewOperationModifiers OperationModifiers
  {
    get => this.operationModifiers;
    set
    {
      if (this.operationModifiers == value)
        return;
      this.CheckAllowPropertyChange();
      this.operationModifiers = value;
    }
  }

  /// <summary>
  /// Позволяет проверить корректность инициализации посредника MVP (presenter).
  /// Метод вызывается непосредственно перед подключением посредника к виду MVP (view).
  /// Необработанное исключение в этом методе прерывает процесс подключения.
  /// </summary>
  /// <exception cref="T:Intermech.Mvp.PresenterPropertyException">Указанное свойство посредника некорректно</exception>
  /// <exception cref="T:Intermech.Mvp.MvpException">Посредник не был корректно инициализирован</exception>
  protected override void DoValidate()
  {
    base.DoValidate();
    if (this.AdjustmentOptions == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "AdjustmentOptions");
    if (this.OperationModifiers == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "OperationModifiers");
  }

  /// <summary>
  /// Позволяет обработать событие подключения посредника MVP (presenter) к виду MVP (view).
  /// Посредник должен заполнить свой вид исходными данными и подписаться на события вида.
  /// Необработанное исключение в этом методе прерывает процесс подключения и запускает процесс отключения.
  /// </summary>
  protected override void OnAttachView()
  {
    base.OnAttachView();
    this.View.EnableInjectSigns = this.AdjustmentOptions.EnableInjectSigns;
    this.View.EnableInjectFileChecksum = this.AdjustmentOptions.EnableInjectFileChecksum;
    this.View.EnableInjectAttributes = this.AdjustmentOptions.EnableInjectAttributes;
    this.View.InjectSignNamesOnly = this.OperationModifiers.InjectSignNamesOnly;
    this.View.OperationConfirmed += new EventHandler(this.OnApplyChanges);
  }

  /// <summary>
  /// Позволяет обработать событие отключения посредника MVP (presenter) от вида MVP (view).
  /// Посредник должен очистить вид и отписаться от всех событий вида.
  /// Метод вызывается как при закрытии вида, так и в случае ошибки подключения к виду.
  /// </summary>
  protected override void OnDetachView()
  {
    base.OnDetachView();
    this.View.OperationConfirmed -= new EventHandler(this.OnApplyChanges);
    this.View.EnableInjectSigns = false;
    this.View.EnableInjectFileChecksum = false;
    this.View.EnableInjectAttributes = false;
  }

  /// <summary>Обработчик события "Применить изменения" от вида MVP.</summary>
  /// <param name="sender">Вид MVP</param>
  /// <param name="e">Аргументы события</param>
  private void OnApplyChanges(object sender, EventArgs e)
  {
    this.AdjustmentOptions.EnableInjectSigns = this.View.EnableInjectSigns;
    this.AdjustmentOptions.EnableInjectFileChecksum = this.View.EnableInjectFileChecksum;
    this.AdjustmentOptions.EnableInjectAttributes = this.View.EnableInjectAttributes;
    this.OperationModifiers.InjectSignNamesOnly = this.View.InjectSignNamesOnly;
  }
}
