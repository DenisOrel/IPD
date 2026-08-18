
// Type: Intermech.Client.Core.ObjectCreator.Controls.ObjectCreatorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator.Controls;

public class ObjectCreatorControl : UserControl, IObjectCreator
{
  /// <summary>Индекс шага в мастере (0...N)</summary>
  public int PageIndex;
  /// <summary>
  /// Ссылка на объект, содержащий параметры создаваемого объекта
  /// </summary>
  public CreatedObjectItem CreatedObject;
  /// <summary>
  /// Поле для хранения признака - нужно ли отслеживать готовность данного шага
  /// </summary>
  protected bool _StepIsReadyCheckRequired;
  protected bool _NeedSaveWhenNotVisible;
  protected bool _SaveInTransaction = true;
  protected bool _SaveAfterCommitCreation;
  /// <summary>
  /// Поле для хранения признака завершенности данного шага мастера создания объектов
  /// </summary>
  protected bool _StepIsReady = true;
  /// <summary>
  /// Поле для хранения признака разрешения на данном шаге мастера создания объектов переходить к следующему
  /// </summary>
  protected bool _NextIsAccessible = true;
  protected bool _showBeforeDesForms;

  /// <summary>Конструктор</summary>
  public ObjectCreatorControl()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="createdObject">Объект с информацией о создаваемом экземпляре</param>
  public ObjectCreatorControl(CreatedObjectItem createdObject)
  {
    this.CreatedObject = createdObject;
  }

  /// <summary>
  /// Признак - нужно ли отслеживать готовность данного шага для определения
  /// доступности кнопки "Готово" мастера создания объектов
  /// </summary>
  public virtual bool StepIsReadyCheckRequired => this._StepIsReadyCheckRequired;

  /// <summary>
  /// Признак того, нужно ли вызывать шагу Save по кнопке "Готово", если контрол
  /// не является видимым на данный момент
  /// </summary>
  public bool NeedSaveWhenNotVisible => this._NeedSaveWhenNotVisible;

  /// <summary>
  /// Признак сохранения результатов только после завершения создания объекта
  /// </summary>
  public bool SaveAfterCommitCreation => this._SaveAfterCommitCreation;

  /// <summary>Сохранение результатов происходит в общей транзакции</summary>
  public bool SaveInTransaction => this._SaveInTransaction;

  /// <summary>
  /// Признак завершенности данного шага мастера создания объектов
  /// (т.е. если true, то по данной закладке можно можно разрешить нажатие на кнопку "Готово")
  /// </summary>
  public virtual bool StepIsReady => this._StepIsReady;

  /// <summary>
  /// Признак разрешения на данном шаге мастера создания объектов переходить к следующему
  /// </summary>
  public virtual bool NextIsAccessible => this._NextIsAccessible;

  /// <summary>
  /// По умолчанию в мастере создания объектов сначала показываются формы редактирования,
  /// после них остальные контролы. Данный признак позволят управлять порядком контролов относительно форм.
  /// </summary>
  public virtual bool ShowBeforeDesForms => this._showBeforeDesForms;

  /// <summary>
  /// Обновление элементов управления в соответствии с данными полей объекта CreatedObject
  /// </summary>
  /// <param name="args">Информации для метода обновления шага мастера создания объектов</param>
  /// <returns></returns>
  public virtual bool Refresh(PageRefreshArgs args)
  {
    args.Error = (Exception) null;
    return true;
  }

  /// <summary>Сохранение данных в объекте CreatedObject</summary>
  /// <param name="args">Информации для метода сохранения шага мастера создания объектов</param>
  /// <returns></returns>
  public virtual bool Save(PageSaveArgs args)
  {
    args.Error = (Exception) null;
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  public virtual void StartErrorCheck()
  {
  }

  /// <summary>
  /// будем возвращать id раздела справки для данного шага
  /// мастера создания. если показать нечего - общая для всех
  /// </summary>
  /// <returns></returns>
  public virtual int HelpTopicID => 686;

  public virtual bool SaveAfterCommit(IUserSession session, long newObjectID) => true;

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectCreatorControl));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (ObjectCreatorControl);
    this.ResumeLayout(false);
  }
}
