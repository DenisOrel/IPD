
// Type: Intermech.Navigator.Views.NavBaseView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.Views;

/// <summary>
/// Реализация базовой закладки для просмотра/редактирования объектов/связей
/// </summary>
public class NavBaseView : UserControl, IView, ICanCloseViews, ICanDeactivateView
{
  /// <summary>
  /// Обработчик события "Перед завершением редактирования объекта"
  /// </summary>
  private EventHandler<BeforeObjectCommandArgs> _commandsBeforeCheckInHandler;
  /// <summary>
  /// Обработчик события "Перед отменой редактирования объекта"
  /// </summary>
  private EventHandler<BeforeObjectCommandArgs> _commandsBeforeCancelEditHandler;
  /// <summary>
  /// Обработчик событий от службы уведомлений окна "Навигатора", на котором расположена закладка
  /// </summary>
  private NotificationEventHandler _notifyHandler;
  /// <summary>Обработчик событий от глобальной службы уведомлений</summary>
  private NotificationEventHandler _globalNotifyHandler;
  /// <summary>
  /// Родительский узел, на основании которого построена закладка
  /// </summary>
  protected INode _parentNode;
  /// <summary>
  /// Идентификатор родительского узла, на основании которого построена закладка
  /// </summary>
  protected INodeID _nodeID;
  /// <summary>Идентификатор типа объекта</summary>
  protected int _objTypeID;
  /// <summary>Идентификатор версии дочернего объекта</summary>
  protected long _objID;
  /// <summary>Идентификатор версии родительского объекта</summary>
  protected long _projID;
  /// <summary>Идентификатор связи</summary>
  protected long _prjLinkID;
  /// <summary>Контейнер сервисов</summary>
  protected System.IServiceProvider _services;
  /// <summary>Состояние закладки</summary>
  protected IViewState _viewState;
  /// <summary>Требуется ли инициализация закладки</summary>
  protected bool _reinitialize;
  /// <summary>Индекс изображения</summary>
  protected int _imageIndex;
  /// <summary>Modified флаг</summary>
  protected bool _modified;
  /// <summary>Признак активной закладки</summary>
  protected bool _isViewActive;
  /// <summary>Флаг / признак необходимости сохранения изменений</summary>
  protected bool _needSaveChanges = true;
  /// <summary>Заголовок закладки</summary>
  protected string _caption = string.Empty;
  /// <summary>Сообщение в диалоге сохранения изменений</summary>
  protected string _locMessageTxt = string.Empty;
  /// <summary>Заголовок диалога сохранения изменений</summary>
  protected string _locMessageCpt = string.Empty;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  /// <summary>
  /// 
  /// </summary>
  protected Panel pnButtons;
  /// <summary>
  /// 
  /// </summary>
  protected Button btApply;
  /// <summary>
  /// 
  /// </summary>
  protected Button btCancel;

  /// <summary>Инициализация пользовательских компонент</summary>
  protected virtual void InitializeCustomControls()
  {
    if (this.DesignMode)
      return;
    this.InitializeCustomMessages();
  }

  /// <summary>Инициализация пользовательских сообщений</summary>
  protected virtual void InitializeCustomMessages()
  {
    this._caption = this.GetType().ToString();
    this._locMessageTxt = LocalizationHolder.rm.GetString("Client.Core_1612");
    this._locMessageCpt = LocalizationHolder.rm.GetString("Client.Core_1613");
  }

  /// <summary>Инициализировать ресурсы закладки</summary>
  protected virtual void InitResources() => this._imageIndex = -1;

  /// <summary>Освободить ресурсы закладки</summary>
  protected virtual void ReleaseResources()
  {
  }

  /// <summary>Подписываем обработчиков закладки на события</summary>
  /// <remarks>
  /// 
  /// </remarks>
  protected virtual void RegisterEventHandlers()
  {
    if (this._commandsBeforeCheckInHandler == null)
    {
      this._commandsBeforeCheckInHandler = new EventHandler<BeforeObjectCommandArgs>(this.CommandsBeforeCheckIn);
      ObjectCommandEvents.Checkin.Before += this._commandsBeforeCheckInHandler;
      ObjectCommandEvents.SaveChanges.Before += this._commandsBeforeCheckInHandler;
    }
    if (this._commandsBeforeCancelEditHandler == null)
    {
      this._commandsBeforeCancelEditHandler = new EventHandler<BeforeObjectCommandArgs>(this.CommandsBeforeCancelEdit);
      ObjectCommandEvents.CancelChanges.Before += this._commandsBeforeCancelEditHandler;
    }
    if (this._globalNotifyHandler == null)
    {
      INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
      if (service != null)
      {
        this._globalNotifyHandler = new NotificationEventHandler(this.GlobalNotificationEventFired);
        service.Subscribe(this._globalNotifyHandler);
      }
    }
    if (this._notifyHandler != null)
      return;
    INotificationService service1 = ServiceUtils.GetService<INotificationService>((object) this._services, false);
    if (service1 == null)
      return;
    this._notifyHandler = new NotificationEventHandler(this.NotificationEventFired);
    service1.Subscribe(this._notifyHandler);
  }

  /// <summary>Удаление подписки обработчиков закладки</summary>
  protected virtual void UnRegisterEventHandlers()
  {
    if (this._commandsBeforeCheckInHandler != null)
    {
      ObjectCommandEvents.Checkin.Before -= this._commandsBeforeCheckInHandler;
      ObjectCommandEvents.SaveChanges.Before -= this._commandsBeforeCheckInHandler;
      this._commandsBeforeCheckInHandler = (EventHandler<BeforeObjectCommandArgs>) null;
    }
    if (this._commandsBeforeCancelEditHandler != null)
    {
      ObjectCommandEvents.CancelChanges.Before -= this._commandsBeforeCancelEditHandler;
      this._commandsBeforeCancelEditHandler = (EventHandler<BeforeObjectCommandArgs>) null;
    }
    if (this._notifyHandler != null)
    {
      ServiceUtils.GetService<INotificationService>((object) this._services, false)?.Unsubscribe(this._notifyHandler);
      this._notifyHandler = (NotificationEventHandler) null;
    }
    if (this._globalNotifyHandler == null)
      return;
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.Unsubscribe(this._globalNotifyHandler);
    this._globalNotifyHandler = (NotificationEventHandler) null;
  }

  /// <summary>Выполнить инициализацию сервисов закладки</summary>
  /// <param name="services">Контейнер сервисов</param>
  protected virtual void InitServices(System.IServiceProvider services)
  {
    this._viewState = ServiceUtils.GetService<IViewState>((object) services, true);
  }

  /// <summary>Выполнить деинициализацию сервисов закладки</summary>
  protected virtual void ReleaseServices() => this._viewState = (IViewState) null;

  /// <summary>
  /// Событие возникает перед завершением изменений в объекте
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void CommandsBeforeCheckIn(object sender, BeforeObjectCommandArgs e)
  {
    if (e.ObjectId != this._objID || !this.Visible)
      return;
    this.SaveData();
  }

  /// <summary>Событие возникает перед отменой изменений в объекте</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void CommandsBeforeCancelEdit(object sender, BeforeObjectCommandArgs e)
  {
    if (e.ObjectId != this._objID || !this.Modified)
      return;
    this.LoadData();
  }

  /// <summary>Событие от локальной службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (e == null)
      return;
    DBObjectsEventArgs objectsEventArgs = e as DBObjectsEventArgs;
    DBRelationsEventArgs relationsEventArgs = e as DBRelationsEventArgs;
    DBObjectsCheckOutEventArgs checkOutEventArgs = e as DBObjectsCheckOutEventArgs;
    ApplicationClosingEventArgs closingEventArgs = e as ApplicationClosingEventArgs;
    switch (e.EventName)
    {
      case "ObjectsChanged":
      case "RelationsChanged":
        if ((objectsEventArgs == null || !objectsEventArgs.ObjectIDs.Contains(this._objID)) && (relationsEventArgs == null || !relationsEventArgs.RelationIDs.Contains(this._prjLinkID)))
          break;
        this._reinitialize = true;
        if (!this._isViewActive)
          break;
        this.Activate((IView) null);
        break;
      case "ObjectsChangesCancelled":
      case "ObjectsCheckedIn":
        if (objectsEventArgs == null || !objectsEventArgs.ObjectIDs.Contains(this._objID))
          break;
        this._objID = Math.Abs(this._objID);
        this._reinitialize = true;
        if (!this._isViewActive)
          break;
        this.Activate((IView) null);
        break;
      case "ObjectsCheckedOut":
        if (checkOutEventArgs == null || !checkOutEventArgs.ObjectIDs.Contains(this._objID))
          break;
        int index = checkOutEventArgs.ObjectIDs.IndexOf(this._objID);
        this._objID = checkOutEventArgs.NewObjectIDs[index];
        this._reinitialize = true;
        if (!this._isViewActive)
          break;
        this.Activate((IView) null);
        break;
      case "ApplicationClosing":
        if (closingEventArgs == null)
          break;
        this.Deactivate((IView) null);
        break;
    }
  }

  /// <summary>Событие от глобальной службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GlobalNotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (!(e.EventName == "ApplicationClosing"))
      return;
    ApplicationClosingEventArgs closingEventArgs = e as ApplicationClosingEventArgs;
    if (!this.Modified)
      return;
    DialogResult dialogResult = MessageBox.Show(this._locMessageTxt, this._locMessageCpt, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
    if (dialogResult == DialogResult.Yes)
      this.SaveData(false);
    if (closingEventArgs != null && dialogResult == DialogResult.Cancel)
      closingEventArgs.Cancel = true;
    this.Modified = false;
  }

  /// <summary>Загрузка настроек</summary>
  protected virtual void LoadSettings()
  {
  }

  /// <summary>Сохранение настроек</summary>
  protected virtual void SaveSettings()
  {
  }

  /// <summary>Создать закладку</summary>
  public NavBaseView()
  {
    this.InitializeComponent();
    this.InitializeCustomControls();
    this._reinitialize = false;
    this.InitResources();
  }

  /// <summary>Инициализировать закладку</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <param name="relationId">Идентификатор связи</param>
  /// <param name="services">Контейнер сервисов</param>
  public virtual void Initialize(
    long objectId,
    int objectType,
    long relationId,
    System.IServiceProvider services)
  {
    this.UnRegisterEventHandlers();
    this._services = services;
    this.RegisterEventHandlers();
    this._parentNode = (INode) null;
    this._nodeID = (INodeID) null;
    this._objID = objectId;
    this._objTypeID = objectType;
    this._prjLinkID = relationId;
    this._reinitialize = true;
    this.UpdateControls();
  }

  /// <summary>
  /// Выполняет инициализацию закладки после ее создания. Реализация
  /// этого метода должна работать быстро, т.е. все длительные операции
  /// желательно выполнять при первом вызове метода Activate.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="services">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public virtual void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this.UnRegisterEventHandlers();
    this._services = services;
    this.RegisterEventHandlers();
    this._parentNode = items.Count != 0 ? items.GetItemData(0, typeof (INode)) as INode : (INode) null;
    this._nodeID = items.Count != 0 ? items.GetItemID(0) : (INodeID) null;
    this._projID = items.Count == 0 || !(items.GetParentData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData) ? 0L : parentData.ObjectID;
    this.GetDataFromNodeId();
    this._reinitialize = true;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране. Этот метод вызывается при
  /// первом показе закладки, а также при переключении на нее с другой закладки.
  /// </summary>
  /// <param name="previousView">
  /// Закладка, с которой осуществляется переключение. Может быть null для самой первой
  /// показываемой на экране закладки.
  /// </param>
  public virtual void Activate(IView previousView)
  {
    this._isViewActive = true;
    if (!this._reinitialize)
      return;
    this.InitServices(this._services);
    this.LoadSettings();
    this.LoadData();
    this._reinitialize = false;
    this._needSaveChanges = true;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">
  /// Закладка, на которую осуществляется переключение. Может быть null, если выполняется
  /// не переключение, а удаление закладок.
  /// </param>
  public virtual void Deactivate(IView nextView)
  {
    this._isViewActive = false;
    if (this._needSaveChanges)
      this.SaveIfModified(false, false);
    this.SaveSettings();
    this.ReleaseServices();
  }

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет инициализирована
  /// в методе Initialize.
  /// </summary>
  public virtual string Caption => this._caption;

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет инициализирована в методе Initialize.
  /// </summary>
  public virtual int ImageIndex
  {
    get
    {
      int imageIndex = this._imageIndex;
      return this._imageIndex;
    }
  }

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет инициализирована в
  /// методе Initialize.
  /// </summary>
  public virtual int OrderID => throw new Exception("Method not implemented");

  /// <summary>
  /// Выполнить запрос, можно ли закрывать форму, на которой расположены закладки.
  /// </summary>
  /// <param name="sender">Отправитель запроса</param>
  /// <returns>true - закладка разрешает закрытие формы, false - закладка запрещает закрытие формы</returns>
  public bool CanClose(object sender)
  {
    bool flag = this.SaveIfModified(true);
    this._needSaveChanges = !flag;
    return flag;
  }

  /// <summary>
  /// Выполнить запрос, можно ли деактивировать текущую закладку.
  /// </summary>
  /// <param name="sender">Отправитель запроса</param>
  /// <returns>true - закладку можно деактивировать, false - закладку нельзя деактивировать</returns>
  public bool CanDeactivate(object sender) => this.CanClose(sender);

  /// <summary>Нажата клавиша "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btCancel_Click(object sender, EventArgs e) => this.ViewCancelClick(sender, e);

  /// <summary>Нажата кнопка "Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btApply_Click(object sender, EventArgs e) => this.ViewApplyClick(sender, e);

  /// <summary>Нажата клавиша "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ViewCancelClick(object sender, EventArgs e)
  {
    if (!this.Visible)
      return;
    this.CancelChanges();
  }

  /// <summary>Нажата кнопка "Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ViewApplyClick(object sender, EventArgs e)
  {
    if (!this.Visible)
      return;
    this.SaveData();
  }

  /// <summary>Изменилась информация на закладке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ViewObjectValueChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>Загрузить информацию в закладку</summary>
  protected virtual void GetDataFromNodeId()
  {
    if (this._parentNode == null || this._nodeID == null)
      return;
    IDBRelationID data1 = (IDBRelationID) this._parentNode.GetData(this._nodeID, typeof (IDBRelationID));
    IDBTypedObjectID data2 = (IDBTypedObjectID) this._parentNode.GetData(this._nodeID, typeof (IDBTypedObjectID));
    if (data2 != null)
    {
      this._objID = data2.ObjectID;
      this._objTypeID = data2.ObjectType;
    }
    else
    {
      this._objID = 0L;
      this._objTypeID = -1;
    }
    this._prjLinkID = data1 == null ? -1L : data1.Value;
  }

  /// <summary>
  /// Сохранить изменения из закладки в объект (связь) после диалога с пользователем
  /// </summary>
  /// <param name="canCancel">Наличие кнопки "Cancel" в MessageBox</param>
  /// <param name="needNotifications">Необходимость отправки уведомлений</param>
  /// <returns>false - если пользователь отменил сохранение (выбрано Cancel)</returns>
  protected virtual bool SaveIfModified(bool canCancel, bool sendNotifications = true)
  {
    bool flag = true;
    if (this.Modified)
    {
      switch (MessageBox.Show(this._locMessageTxt, this._locMessageCpt, canCancel ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo, MessageBoxIcon.Question))
      {
        case DialogResult.Cancel:
          flag = false;
          break;
        case DialogResult.Yes:
          this.SaveData(sendNotifications);
          break;
      }
    }
    return flag;
  }

  /// <summary>Загрузить информацию в закладку</summary>
  protected virtual void LoadData()
  {
    this.Modified = false;
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual void CancelChanges() => this.LoadData();

  /// <summary>Сохранить информацию из закладки</summary>
  /// <param name="needNotifications">Необходимость отправки уведомлений</param>
  protected virtual void SaveData(bool sendNotifications = true)
  {
    this.Modified = false;
    this.UpdateControls();
  }

  /// <summary>Обновить состояние элементов управления закладки</summary>
  protected virtual void UpdateControls()
  {
    this.btCancel.Enabled = this.btApply.Enabled = this.Modified;
  }

  /// <summary>Modified flag</summary>
  public virtual bool Modified
  {
    get => this._modified;
    set
    {
      if (this._modified == value)
        return;
      this._modified = value;
      this.UpdateControls();
    }
  }

  /// <summary>Can modifying flag</summary>
  public virtual bool CanModify
  {
    get
    {
      return this._viewState == null || (this._viewState.ViewState & ViewStateFlags.ReadOnly) != ViewStateFlags.ReadOnly;
    }
  }

  /// <summary>Освобождение ресурсов закладки</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.ReleaseServices();
      this.UnRegisterEventHandlers();
      this.ReleaseResources();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NavBaseView));
    this.pnButtons = new Panel();
    this.btCancel = new Button();
    this.btApply = new Button();
    this.pnButtons.SuspendLayout();
    this.SuspendLayout();
    this.pnButtons.Controls.Add((Control) this.btCancel);
    this.pnButtons.Controls.Add((Control) this.btApply);
    componentResourceManager.ApplyResources((object) this.pnButtons, "pnButtons");
    this.pnButtons.Name = "pnButtons";
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.Name = "btCancel";
    this.btCancel.Click += new EventHandler(this.btCancel_Click);
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    this.btApply.Name = "btApply";
    this.btApply.Click += new EventHandler(this.btApply_Click);
    this.Controls.Add((Control) this.pnButtons);
    this.Name = "TechCardBaseView";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "    ";
    this.pnButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
