
// Type: Intermech.Navigator.DBObjects.PropertiesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Notifications;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Закладка "Свойства объекта"</summary>
[ViewDescriptionProvider(typeof (PropertiesView.PropertiesViewDescriptionProvider))]
public class PropertiesView : UserControl, INavigatorView, IView, INodeView
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private ObjectPropertyGrid propertyGrid;
  protected Panel panel1;
  protected Panel pnButtons;
  protected Button btApply;
  protected Button btCancel;
  /// <summary>Ширина сплиттера</summary>
  private static Dictionary<ViewStateFlags, int> splitWidth = new Dictionary<ViewStateFlags, int>();
  /// <summary>Вид сортировки</summary>
  private static Dictionary<ViewStateFlags, PropertySort> propertySort = new Dictionary<ViewStateFlags, PropertySort>();
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
  /// <summary>
  /// Служба уведомлений окна "Навигатора", на котором расположена закладка
  /// </summary>
  protected INotificationService _notificationService;
  /// <summary>Глобальная служба уведомлений</summary>
  protected INotificationService _globalNotificationService;
  /// <summary>
  /// Обработчик событий от службы уведомлений окна "Навигатора", на котором расположена закладка
  /// </summary>
  protected NotificationEventHandler _notifyHandler;
  /// <summary>Обработчик событий от глобальной службы уведомлений</summary>
  protected NotificationEventHandler _globalNotifyHandler;
  /// <summary>
  /// Обработчик события "Перед завершением редактирования объекта"
  /// </summary>
  protected EventHandler<BeforeObjectCommandArgs> _commandsBeforeCheckInHandler;
  /// <summary>Контейнер сервисов</summary>
  protected System.IServiceProvider _services;
  /// <summary>Состояние закладки</summary>
  protected IViewState _viewState;
  /// <summary>Требуется ли инициализация закладки</summary>
  protected bool _reinitialize;
  /// <summary>Выполнена ли инициализация некоторых полей</summary>
  protected bool _firstInitialized;
  /// <summary>Индекс изображения</summary>
  protected int _imageIndex;
  /// <summary>Режим работы закладки</summary>
  protected const GetAttributeValuesModes _gridMode = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility;
  /// <summary>Типы данных</summary>
  protected static readonly System.Type[] tabTypes = new System.Type[1]
  {
    typeof (ObjectAllAttributesGridTab)
  };

  /// <summary>Освобождение ресурсов закладки</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.ReleaseServices();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PropertiesView));
    this.pnButtons = new Panel();
    this.btCancel = new Button();
    this.btApply = new Button();
    this.panel1 = new Panel();
    this.propertyGrid = new ObjectPropertyGrid();
    this.pnButtons.SuspendLayout();
    this.panel1.SuspendLayout();
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
    this.panel1.Controls.Add((Control) this.propertyGrid);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.propertyGrid.CategoryForeColor = SystemColors.InactiveCaptionText;
    this.propertyGrid.CommandsActiveLinkColor = SystemColors.ActiveCaption;
    this.propertyGrid.CommandsDisabledLinkColor = SystemColors.ControlDark;
    this.propertyGrid.CommandsLinkColor = SystemColors.ActiveCaption;
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.InternalMenuEnabled = true;
    this.propertyGrid.LineColor = SystemColors.ScrollBar;
    this.propertyGrid.LockTypeChange = false;
    this.propertyGrid.Name = "propertyGrid";
    this.propertyGrid.PropertySort = PropertySort.Alphabetical;
    this.propertyGrid.GridChanged += new ObjectPropertyGrid.GridChangedDelegate(this.PropsGrid_GridChanged);
    this.propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PropsGrid_PropertyValueChanged);
    this.propertyGrid.PropertyTabChanged += new PropertyTabChangedEventHandler(this.propertyGrid_PropertyTabChanged);
    this.propertyGrid.PropertySortChanged += new EventHandler(this.propertyGrid_PropertySortChanged);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.pnButtons);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (PropertiesView);
    this.Tag = (object) "    ";
    this.SizeChanged += new EventHandler(this.PropertiesView_SizeChanged);
    this.pnButtons.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Редактор свойств</summary>
  public virtual ObjectPropertyGrid PropertyGrid
  {
    [DebuggerStepThrough] get => this.propertyGrid;
  }

  /// <summary>Статчический конструктор</summary>
  static PropertiesView()
  {
    PropertiesView.splitWidth.Add(ViewStateFlags.InDialog, 0);
    PropertiesView.splitWidth.Add(ViewStateFlags.InParametersCard, 0);
    PropertiesView.splitWidth.Add(ViewStateFlags.NodeInTree, 0);
    PropertiesView.splitWidth.Add(ViewStateFlags.NodeInViews, 0);
    PropertiesView.splitWidth.Add(ViewStateFlags.NodeUnderTree, 0);
    PropertiesView.propertySort.Add(ViewStateFlags.InDialog, PropertySort.CategorizedAlphabetical);
    PropertiesView.propertySort.Add(ViewStateFlags.InParametersCard, PropertySort.CategorizedAlphabetical);
    PropertiesView.propertySort.Add(ViewStateFlags.NodeInTree, PropertySort.CategorizedAlphabetical);
    PropertiesView.propertySort.Add(ViewStateFlags.NodeInViews, PropertySort.CategorizedAlphabetical);
    PropertiesView.propertySort.Add(ViewStateFlags.NodeUnderTree, PropertySort.Alphabetical);
  }

  /// <summary>Создать закладку</summary>
  public PropertiesView()
  {
    this.InitializeComponent();
    this._reinitialize = false;
    this.InitResources();
  }

  /// <summary>Инициализировать ресурсы закладки</summary>
  protected virtual void InitResources() => this._imageIndex = -1;

  /// <summary>Освободить ресурсы закладки</summary>
  protected virtual void ReleaseResources()
  {
  }

  /// <summary>Выполнить инициализацию сервисов закладки</summary>
  /// <param name="services">Контейнер сервисов</param>
  protected virtual void InitServices(System.IServiceProvider services)
  {
    if (this._commandsBeforeCheckInHandler == null)
    {
      this._commandsBeforeCheckInHandler = new EventHandler<BeforeObjectCommandArgs>(this.CommandsBeforeCheckIn);
      ObjectCommandEvents.Checkin.Before += this._commandsBeforeCheckInHandler;
    }
    if (services != null)
    {
      if (this._notificationService != null && this._notifyHandler != null)
      {
        this._notificationService.Unsubscribe(this._notifyHandler);
        this._notifyHandler = (NotificationEventHandler) null;
      }
      this._notificationService = services.GetService(typeof (INotificationService)) as INotificationService;
      this._viewState = services.GetService(typeof (IViewState)) as IViewState;
    }
    else
    {
      this._notificationService = (INotificationService) null;
      this._viewState = (IViewState) null;
    }
    if (this._notifyHandler == null && this._notificationService != null)
    {
      this._notifyHandler = new NotificationEventHandler(this.NotificationEventFired);
      this._notificationService.Subscribe(this._notifyHandler);
    }
    if (this._globalNotificationService != null)
      return;
    this._globalNotificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this._globalNotifyHandler != null || this._globalNotificationService == null)
      return;
    this._globalNotifyHandler = new NotificationEventHandler(this.GlobalNotificationEventFired);
    this._globalNotificationService.Subscribe(this._globalNotifyHandler);
  }

  /// <summary>Выполнить деинициализацию сервисов закладки</summary>
  protected virtual void ReleaseServices()
  {
    if (this._commandsBeforeCheckInHandler != null)
    {
      ObjectCommandEvents.Checkin.Before -= this._commandsBeforeCheckInHandler;
      this._commandsBeforeCheckInHandler = (EventHandler<BeforeObjectCommandArgs>) null;
    }
    if (this._globalNotificationService == null)
      return;
    if (this._notifyHandler != null && this._notificationService != null)
      this._notificationService.Unsubscribe(this._notifyHandler);
    if (this._globalNotifyHandler != null && this._globalNotificationService != null)
      this._globalNotificationService.Unsubscribe(this._globalNotifyHandler);
    this._globalNotificationService = (INotificationService) null;
    this._notifyHandler = (NotificationEventHandler) null;
    this._globalNotifyHandler = (NotificationEventHandler) null;
    this._viewState = (IViewState) null;
  }

  /// <summary>Инициализировать закладку</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="objType">Идентификатор типа объекта</param>
  /// <param name="relID">Идентификатор связи</param>
  /// <param name="services">Контейнер сервисов</param>
  public virtual void Initialize(long objID, int objType, long relID, System.IServiceProvider services)
  {
    this._services = services;
    this.InitServices(this._services);
    this._parentNode = (INode) null;
    this._nodeID = (INodeID) null;
    this._objID = objID;
    this._objTypeID = objType;
    this._prjLinkID = relID;
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
    this._services = services;
    if (!this._firstInitialized)
    {
      NavigatorViewOptions service = this._services != null ? this._services.GetService(typeof (NavigatorViewOptions)) as NavigatorViewOptions : (NavigatorViewOptions) null;
      this.propertyGrid.HelpVisible = service == null || service.Context == NavigatorViewContext.MainViews;
      if (!this.propertyGrid.HelpVisible)
        this.propertyGrid.PropertySort = PropertySort.Alphabetical;
      else
        this.propertyGrid.PropertySort = PropertySort.CategorizedAlphabetical;
      this._firstInitialized = true;
    }
    this._parentNode = items.GetItemData(0, typeof (INode)) as INode;
    this._nodeID = items.GetItemID(0);
    this._projID = items.GetParentData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData ? parentData.ObjectID : 0L;
    this.GetDataFromNodeId();
    this.InitServices(this._services);
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
    if (previousView == PageViewsManager.BlackHoleView || !this._reinitialize)
      return;
    this.LoadData();
    this._reinitialize = false;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">
  /// Закладка, на которую осуществляется переключение. Может быть null, если выполяется
  /// не переключение, а удаление закладок.
  /// </param>
  public virtual void Deactivate(IView nextView)
  {
    this.SaveIfModified();
    this._reinitialize = true;
  }

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  public virtual string Caption => PropertiesView.GetCaption(this._services);

  private static string GetCaption(System.IServiceProvider services)
  {
    NavigatorViewOptions service = services != null ? services.GetService(typeof (NavigatorViewOptions)) as NavigatorViewOptions : (NavigatorViewOptions) null;
    return service == null || service.Context == NavigatorViewContext.MainViews ? LocalizationHolder.rm.GetString("Client.Core_146") : LocalizationHolder.rm.GetString("Client.Core_1356");
  }

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет проинициализирована в методе Initialize.
  /// </summary>
  public virtual int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgProp");
      return this._imageIndex;
    }
  }

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  public virtual int OrderID
  {
    [DebuggerStepThrough] get => 10;
  }

  /// <summary>
  /// Событие возникает перед завершением изменений в объекте
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void CommandsBeforeCheckIn(object sender, BeforeObjectCommandArgs e)
  {
    if (e.ObjectId != this._objID || !this.PropertyGrid.Visible)
      return;
    this.SaveData();
  }

  /// <summary>Событие от локальной службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (sender == this || this._parentNode == null)
      return;
    IUpdateAnalyser analyser = this._parentNode.GetAnalyser(this.Capabilities, sender, e);
    if (analyser == null)
      return;
    UpdateManager.UpdateView((INodeView) this, analyser);
  }

  /// <summary>Событие от глобальной службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GlobalNotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (!(e.EventName == "ApplicationClosing"))
      return;
    ApplicationClosingEventArgs closingEventArgs = e as ApplicationClosingEventArgs;
    if (!this.PropertyGrid.IsChanged)
      return;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_315"), LocalizationHolder.rm.GetString("Client.Core_316"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
    if (num == 6)
      this.SaveData();
    if (num != 2)
      return;
    closingEventArgs.Cancel = true;
  }

  /// <summary>Нажата клавиша "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btCancel_Click(object sender, EventArgs e) => this.ViewCancelClick(sender, e);

  /// <summary>Нажата кнопка "Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btApply_Click(object sender, EventArgs e) => this.ViewApplyClick(sender, e);

  /// <summary>Изменилась информация в редакторе свойств</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void PropsGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
  {
    this.ViewPropertyValueChanged(sender, e);
  }

  /// <summary>Изменилась внутренняя информация в редакторе свойств</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void PropsGrid_GridChanged(object sender, GridChangedEventArgs e)
  {
    this.ViewGridChanged(sender, e);
  }

  /// <summary>Нажата клавиша "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ViewCancelClick(object sender, EventArgs e)
  {
    if (!this.PropertyGrid.Visible)
      return;
    this.LoadData();
  }

  /// <summary>Нажата кнопка "Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ViewApplyClick(object sender, EventArgs e)
  {
    if (!this.PropertyGrid.Visible)
      return;
    this.SaveData();
  }

  /// <summary>Изменилась информация в редакторе свойств</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ViewPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>Изменилась внутренняя информация в редакторе свойств</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ViewGridChanged(object sender, GridChangedEventArgs e)
  {
    if (!e.ApplyNeeded)
      return;
    this.UpdateControls();
  }

  /// <summary>Загрузить информацию в закладку</summary>
  protected virtual void GetDataFromNodeId()
  {
    IDBTypedObjectID data1 = (IDBTypedObjectID) this._parentNode.GetData(this._nodeID, typeof (IDBTypedObjectID));
    IDBRelationID data2 = (IDBRelationID) this._parentNode.GetData(this._nodeID, typeof (IDBRelationID));
    this._objID = data1.ObjectID;
    this._objTypeID = data1.ObjectType;
    this._prjLinkID = data2 == null ? -1L : data2.Value;
  }

  /// <summary>
  /// Сохранить изменения из редактора свойств в объект (связь) после диалога с пользователем
  /// </summary>
  protected virtual void SaveIfModified()
  {
    if (!this.PropertyGrid.IsChanged)
      return;
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_315"), LocalizationHolder.rm.GetString("Client.Core_316"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      this.SaveData();
    else
      this.LoadData();
  }

  /// <summary>Загрузить информацию в редактор свойств</summary>
  protected virtual void LoadData()
  {
    Control parent = this.PropertyGrid.Parent;
    try
    {
      this.PropertyGrid.Parent = (Control) null;
      this.PropertyGrid.Load(this._objID, AttributableElements.Object, GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility, false, PropertiesView.tabTypes);
    }
    finally
    {
      this.PropertyGrid.Parent = parent;
    }
    this.UpdateControls();
  }

  /// <summary>Сохранить информацию из редактора свойств</summary>
  protected virtual void SaveData()
  {
    if (this.PropertyGrid.IsChanged)
      this.PropertyGrid.Save();
    this.UpdateControls();
  }

  /// <summary>Обновить состояние элементов управления закладки</summary>
  protected virtual void UpdateControls()
  {
    this.btApply.Enabled = this.PropertyGrid.IsChanged;
    this.btCancel.Enabled = this.btApply.Enabled;
    bool isReadOnly = this.IsReadOnly;
    if (this.pnButtons.Visible == isReadOnly)
      this.pnButtons.Visible = !isReadOnly;
    if (!isReadOnly)
      return;
    this.ForceGridToReadOnly();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsReadOnly
  {
    [DebuggerStepThrough] get
    {
      return this._viewState != null && (this._viewState.ViewState & ViewStateFlags.ReadOnly) == ViewStateFlags.ReadOnly;
    }
  }

  /// <summary>Делаю все свойства доступными только для чтения</summary>
  protected void ForceGridToReadOnly()
  {
    if (this.propertyGrid == null || this.propertyGrid.objPDH == null || this.propertyGrid.objPDH.pdcGeneralList == null)
      return;
    foreach (PropDescriptor pdcGeneral in this.propertyGrid.objPDH.pdcGeneralList)
    {
      if (pdcGeneral != null)
      {
        if (!pdcGeneral.IsReadOnly)
          pdcGeneral.SetReadOnly(true);
        if (pdcGeneral.Editor != null)
          pdcGeneral.Editor = (object) null;
      }
    }
    int count1 = this.propertyGrid.objPDH.pdcGeneralList.Count;
    this.propertyGrid.Refresh();
    int count2 = this.propertyGrid.objPDH.pdcGeneralList.Count;
    if (count1 == count2)
      return;
    foreach (PropDescriptor pdcGeneral in this.propertyGrid.objPDH.pdcGeneralList)
    {
      if (pdcGeneral != null && !pdcGeneral.IsReadOnly)
      {
        if (!pdcGeneral.IsReadOnly)
          pdcGeneral.SetReadOnly(true);
        if (pdcGeneral.Editor != null)
          pdcGeneral.Editor = (object) null;
      }
    }
    this.propertyGrid.Refresh();
  }

  private void propertyGrid_PropertyTabChanged(object s, PropertyTabChangedEventArgs e)
  {
    if (!this.IsReadOnly)
      return;
    this.ForceGridToReadOnly();
  }

  /// <summary>
  /// Содержит сведения о возможностях вида, который для получения выводимой информации использует элементы навигации.
  /// </summary>
  public virtual NodeViewCapabilities Capabilities
  {
    get
    {
      ContentType contentType = ContentType.None;
      if (this._parentNode != null && this._nodeID != null)
        contentType = (this._parentNode.GetAttributesOf(this._nodeID) & ContentAttributes.Folder) != ContentAttributes.None ? ContentType.Folders : ContentType.NonFolders;
      return new NodeViewCapabilities(contentType, (NodeColumnCollection) null, false);
    }
  }

  /// <summary>Количество узлов</summary>
  public virtual int Count
  {
    [DebuggerStepThrough] get => 1;
  }

  /// <summary>Получить узел с указанным индексом</summary>
  /// <param name="index">Индекс узла</param>
  /// <returns></returns>
  public virtual INodeID this[int index]
  {
    [DebuggerStepThrough] get => this._nodeID;
  }

  /// <summary>Добавить в коллекцию дополнительные узлы</summary>
  /// <param name="nodeIDs">Коллекция дополнительных узлов</param>
  public virtual void Append(NodeIDCollection nodeIDs)
  {
  }

  /// <summary>Обновить коллекцию узлов с указанными индексами</summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется обновить</param>
  public virtual void Update(IList indexes)
  {
    this.Deactivate((IView) null);
    this._reinitialize = true;
    this.Activate((IView) null);
  }

  /// <summary>
  /// Выполнить замену узлов с указанными индексами данными из дополнительной коллекции
  /// </summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется заменить</param>
  /// <param name="replacementNodeIDs">Коллекция новых узлов взамен старых</param>
  public virtual void Replace(IList indexes, NodeIDCollection replacementNodeIDs)
  {
    bool reinitialize = this._reinitialize;
    try
    {
      if (!this._reinitialize)
        this.Deactivate((IView) null);
      this._nodeID = replacementNodeIDs[0];
      this.GetDataFromNodeId();
    }
    finally
    {
      if (!reinitialize)
        this.Activate((IView) null);
    }
  }

  /// <summary>Удалить узлы с указанными индексами</summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется удалить</param>
  public virtual void Remove(IList indexes)
  {
  }

  /// <summary>Текущее состояние узла грида</summary>
  protected virtual ViewStateFlags CurrentState
  {
    get
    {
      if (this._viewState == null)
        return ViewStateFlags.NodeInViews;
      if ((this._viewState.ViewState & ViewStateFlags.NodeUnderTree) == ViewStateFlags.NodeUnderTree)
        return ViewStateFlags.NodeUnderTree;
      if ((this._viewState.ViewState & ViewStateFlags.InDialog) == ViewStateFlags.InDialog)
        return ViewStateFlags.InDialog;
      if ((this._viewState.ViewState & ViewStateFlags.InParametersCard) == ViewStateFlags.InParametersCard)
        return ViewStateFlags.InParametersCard;
      return (this._viewState.ViewState & ViewStateFlags.NodeInTree) == ViewStateFlags.NodeInTree ? ViewStateFlags.NodeInTree : ViewStateFlags.NodeInViews;
    }
  }

  /// <summary>Сохранение положения разделителя в гриде</summary>
  protected virtual void SaveGridSplitterPos()
  {
    PropertiesView.propertySort[this.CurrentState] = this.propertyGrid.PropertySort;
    try
    {
      object target = this.propertyGrid.GetType().BaseType.GetField("gridView", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) this.propertyGrid);
      int num = (int) target.GetType().InvokeMember("GetLabelWidth", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, (Binder) null, target, new object[0]);
      if (num <= 0)
        return;
      PropertiesView.splitWidth[this.CurrentState] = num;
    }
    catch
    {
    }
  }

  /// <summary>Восстановление положения разделителя в гриде</summary>
  protected virtual void RestoreGridSplitterPos()
  {
    if (PropertiesView.splitWidth[this.CurrentState] == 0)
      return;
    this.propertyGrid.PropertySort = PropertiesView.propertySort[this.CurrentState];
    try
    {
      object target = this.propertyGrid.GetType().BaseType.GetField("gridView", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) this.propertyGrid);
      System.Type type = target.GetType();
      int num = PropertiesView.splitWidth[this.CurrentState];
      type.InvokeMember("MoveSplitterTo", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, target, new object[1]
      {
        (object) num
      });
      type.InvokeMember("MoveSplitterTo", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, target, new object[1]
      {
        (object) num
      });
    }
    catch
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void propertyGrid_PropertySortChanged(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void PropertiesView_SizeChanged(object sender, EventArgs e)
  {
  }

  protected class PropertiesViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = PropertiesView.GetCaption(serviceProvider),
        ImageIndex = Holder.NamedImageList.ImageIndex("imgProp"),
        OrderID = 10
      };
    }
  }
}
