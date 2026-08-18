
// Type: Intermech.Navigator.Controls.PropertiesWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Форма, позволяющая показывать закладки свойств объектов
/// </summary>
public class PropertiesWindow : Form, ICommandTarget, IIODispatcher
{
  private bool icoAssignedOnce;
  /// <summary>Максимальное количество кэшируемых форм</summary>
  internal static readonly int CachedFormsMaxCount = 0;
  /// <summary>
  /// Кэш форм (в кэше хранится до CachedFormsMaxCount разнотипных форм)
  /// </summary>
  internal static List<PropertiesWindow> PropWinCache = new List<PropertiesWindow>(PropertiesWindow.CachedFormsMaxCount);
  /// <summary>Дата и время последнего вызова формы</summary>
  public DateTime AccessTime = DateTime.UtcNow;
  /// <summary>Контейнер сервисов</summary>
  private IServiceContainer _services;
  /// <summary>Менеджер команд</summary>
  private ICommandManager _commandManager;
  /// <summary>Служба уведомлений</summary>
  private INotificationService _notificationService;
  /// <summary>Коллекция разных настроек контролов формы</summary>
  private HybridDictionary _controlsSettings = new HybridDictionary(0, true);
  /// <summary>
  /// Установка этого значка в true означает, что внутри формы есть изменения
  /// </summary>
  private bool _isChanged;
  /// <summary>ID версии объекта</summary>
  private long _objID;
  /// <summary>ID типа объекта</summary>
  private int _objTypeID = -1;
  /// <summary>Название объекта</summary>
  private string _objCaption = string.Empty;
  /// <summary>Название типа объекта</summary>
  private string _objType = string.Empty;
  /// <summary>Примечание для пользователя</summary>
  private string _description = string.Empty;
  /// <summary>
  /// Описание корневого элемента, для которого показываются вьюшки
  /// </summary>
  private IDescriptor _rootDescriptor;
  /// <summary>Выделенные элементы - результат работы формы</summary>
  private ISelectedItems _selectedItems;
  /// <summary>Полученные извне выделенные элементы</summary>
  private ISelectedItems _inSelectedItems;
  /// <summary>
  /// Сервис службы "горячих клавиш" и связанных с ними команд
  /// </summary>
  private IHotKeysManager _hotKeysManager;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private DockContainer bottomDock;
  private DockContainer dockContainer1;
  private DockContainer dockContainer2;
  private PageViewsManager pages;
  private Button btnCancel;

  /// <summary>Создать экземпляр формы</summary>
  public PropertiesWindow()
  {
    this.InitializeComponent();
    this.ServicesInitialization();
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Size = new Size(primaryWorkingArea.Width / 100 * 90, primaryWorkingArea.Height / 100 * 90);
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
  }

  /// <summary>Убрать за собой</summary>
  /// <param name="disposing">true если ресурс был освобождение, иначе false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.ServicesFinalization();
      if (this.components != null)
        this.components.Dispose();
    }
    if (disposing && this.Icon != null && this.icoAssignedOnce)
    {
      Icon icon = this.Icon;
      this.Icon = (Icon) null;
      icon.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Очистить все внутренние структуры</summary>
  internal void Clear()
  {
    this._objCaption = string.Empty;
    this._objID = 0L;
    this.Text = string.Empty;
    this._description = string.Empty;
    this._rootDescriptor = (IDescriptor) null;
    this._selectedItems = (ISelectedItems) null;
    this.UpdateControls();
  }

  /// <summary>Загрузить информацию в форму</summary>
  /// <param name="Caption">Заголовок формы</param>
  /// <param name="Description">Примечание для пользователя</param>
  /// <param name="RootDescriptor">Описание корневого элемента, для которого показываются вьюшки</param>
  /// <param name="ObjID">ID версии объекта</param>
  /// <param name="readOnly">Указать закладкам карточки то, что включён режим "Только чтение"</param>
  /// <returns>true, если загрузка прошла успешно</returns>
  internal bool LoadData(
    string Caption,
    string Description,
    IDescriptor RootDescriptor,
    long ObjID,
    bool readOnly,
    ISelectedItems selectedItems = null)
  {
    this.Clear();
    IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) null;
    if (selectedItems != null)
      dbTypedObjectId = (IDBTypedObjectID) selectedItems.GetItemData(0, typeof (IDBTypedObjectID));
    if (dbTypedObjectId != null)
    {
      this._objCaption = dbTypedObjectId.Caption;
      this._objID = dbTypedObjectId.ObjectID;
      this._objTypeID = dbTypedObjectId.ObjectType;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(ObjID, false);
        if (dbObject == null)
          return false;
        this._objCaption = dbObject.Caption;
        this._objID = ObjID;
        this._objTypeID = dbObject.ObjectType;
      }
    }
    IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(this._objTypeID);
    this._objType = objectType.ObjectTypeName;
    using (MemoryStream memoryStream = new MemoryStream(objectType.Icon))
    {
      if (memoryStream.Length > 0L)
      {
        Icon icon1 = new Icon((Stream) memoryStream, 16 /*0x10*/, 16 /*0x10*/);
        if (icon1.Height != icon1.Width)
        {
          int num = Math.Min(icon1.Height, icon1.Width);
          Rectangle rect = new Rectangle(0, 0, num, num);
          using (Bitmap bitmap = icon1.ToBitmap())
          {
            using (Bitmap bmp = bitmap.Clone(rect, bitmap.PixelFormat))
            {
              icon1.Dispose();
              icon1 = ImageHelper.BitmapToIcon(bmp);
            }
          }
        }
        Icon icon2 = this.Icon;
        this.Icon = icon1;
        if (icon2 != null && this.icoAssignedOnce)
          icon2.Dispose();
        this.icoAssignedOnce = true;
      }
    }
    this.Text = Caption == string.Empty ? string.Format(PropertiesWindow.PropertiesWindowConsts.FormCaption, (object) this._objID, (object) this._objCaption, (object) this._objType) : Caption;
    this._description = Description;
    this._rootDescriptor = RootDescriptor;
    this._selectedItems = selectedItems ?? PropertiesWindow.GetItems(this, this._rootDescriptor, (System.IServiceProvider) this._services);
    this._services.RemoveService(typeof (IViewState));
    if (readOnly)
      this._services.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.ReadOnly | ViewStateFlags.NoCompositionView | ViewStateFlags.NoContainsInView | ViewStateFlags.NoPluginsViews | ViewStateFlags.InParametersCard));
    else
      this._services.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.NoCompositionView | ViewStateFlags.NoContainsInView | ViewStateFlags.NoPluginsViews | ViewStateFlags.InParametersCard));
    this.pages.UpdateViews(this._selectedItems, true);
    this.UpdateControls();
    return true;
  }

  /// <summary>
  /// Впендюрить указанную форму в кэш, удалив при этом самые старые формы
  /// </summary>
  /// <param name="win">Форма</param>
  internal static void InsertPropWin(PropertiesWindow win)
  {
    if (PropertiesWindow.CachedFormsMaxCount == 0 || win == null)
      return;
    if (PropertiesWindow.PropWinCache.Count < PropertiesWindow.CachedFormsMaxCount)
    {
      PropertiesWindow.PropWinCache.Insert(0, win);
    }
    else
    {
      PropertiesWindow propertiesWindow = PropertiesWindow.PropWinCache[PropertiesWindow.PropWinCache.Count - 1];
      DateTime accessTime = win.AccessTime;
      int index1 = 0;
      for (int index2 = 0; index2 < PropertiesWindow.PropWinCache.Count; ++index2)
      {
        if (!(PropertiesWindow.PropWinCache[index2].AccessTime >= accessTime))
        {
          propertiesWindow = PropertiesWindow.PropWinCache[index2];
          accessTime = propertiesWindow.AccessTime;
          index1 = index2;
        }
      }
      propertiesWindow?.Dispose();
      PropertiesWindow.PropWinCache[index1] = win;
    }
  }

  /// <summary>Найти в кэше указанную форму</summary>
  /// <param name="rootDescriptor">Дескриптор формы</param>
  /// <returns>Форма или null</returns>
  internal static PropertiesWindow GetPropWin(IDescriptor rootDescriptor)
  {
    if (PropertiesWindow.CachedFormsMaxCount == 0)
      return (PropertiesWindow) null;
    if (PropertiesWindow.PropWinCache.Count == 0 || rootDescriptor == null)
      return (PropertiesWindow) null;
    for (int index = 0; index < PropertiesWindow.PropWinCache.Count; ++index)
    {
      IDescriptor rootDescriptor1 = PropertiesWindow.PropWinCache[index]._rootDescriptor;
      if (rootDescriptor1 != null && rootDescriptor.Equals((object) rootDescriptor1))
        return PropertiesWindow.PropWinCache[index];
    }
    return (PropertiesWindow) null;
  }

  /// <summary>Вызвать форму с указанными параметрами</summary>
  /// <param name="Caption">Заголовок формы</param>
  /// <param name="Description">Примечание для пользователя</param>
  /// <param name="ObjectID">ID объекта</param>
  /// <param name="pageName">Имя закладки, которую следует сделать активной</param>
  /// <returns>Вернёт то, что пользователь выберет в форме</returns>
  [STAThread]
  public static DialogResult Execute(
    string Caption,
    string Description,
    long ObjectID,
    params string[] pageName)
  {
    IDescriptor descriptor = (IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(ObjectID);
    PropertiesWindow win = PropertiesWindow.GetPropWin(descriptor);
    if (win == null)
    {
      win = new PropertiesWindow();
      PropertiesWindow.InsertPropWin(win);
    }
    if (!win.LoadData(Caption, Description, descriptor, ObjectID, false))
    {
      if (PropertiesWindow.CachedFormsMaxCount == 0)
        win.Dispose();
      return DialogResult.Abort;
    }
    int num = (int) win.ShowDialog(pageName.Length != 0 ? pageName[0] : string.Empty);
    if (PropertiesWindow.CachedFormsMaxCount != 0)
      return (DialogResult) num;
    win.Dispose();
    return (DialogResult) num;
  }

  /// <summary>Вызвать форму с указанными параметрами</summary>
  /// <param name="Caption">Заголовок формы</param>
  /// <param name="Description">Примечание для пользователя</param>
  /// <param name="ObjectID">ID объекта</param>
  /// <param name="ReadOnly">Указать закладкам карточки то, что включён режим "Только чтение"</param>
  /// <param name="pageName">Имя закладки, которую следует сделать активной</param>
  /// <returns>Вернёт то, что пользователь выберет в форме</returns>
  [STAThread]
  public static DialogResult Execute(
    string Caption,
    string Description,
    long ObjectID,
    bool ReadOnly,
    params string[] pageName)
  {
    IDescriptor descriptor = (IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(ObjectID);
    PropertiesWindow win = PropertiesWindow.GetPropWin(descriptor);
    if (win == null)
    {
      win = new PropertiesWindow();
      PropertiesWindow.InsertPropWin(win);
    }
    if (!win.LoadData(Caption, Description, descriptor, ObjectID, ReadOnly))
    {
      if (PropertiesWindow.CachedFormsMaxCount == 0)
        win.Dispose();
      return DialogResult.Abort;
    }
    int num = (int) win.ShowDialog(pageName.Length != 0 ? pageName[0] : string.Empty);
    if (PropertiesWindow.CachedFormsMaxCount != 0)
      return (DialogResult) num;
    win.Dispose();
    return (DialogResult) num;
  }

  /// <summary>
  /// Вызвать форму с указанными параметрами поверх всех окон винды
  /// </summary>
  /// <param name="Caption">Заголовок формы</param>
  /// <param name="Description">Примечание для пользователя</param>
  /// <param name="ObjectID">ID объекта</param>
  /// <param name="pageName">Имя закладки, которую следует сделать активной</param>
  /// <returns>Вернёт то, что пользователь выберет в форме</returns>
  [STAThread]
  public static DialogResult ExecuteTop(
    string Caption,
    string Description,
    long ObjectID,
    params string[] pageName)
  {
    IDescriptor descriptor = (IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(ObjectID);
    PropertiesWindow win = PropertiesWindow.GetPropWin(descriptor);
    if (win == null)
    {
      win = new PropertiesWindow();
      PropertiesWindow.InsertPropWin(win);
    }
    win.TopMost = true;
    if (!win.LoadData(Caption, Description, descriptor, ObjectID, false))
    {
      if (PropertiesWindow.CachedFormsMaxCount == 0)
        win.Dispose();
      return DialogResult.Abort;
    }
    int num = (int) win.ShowDialog(pageName.Length != 0 ? pageName[0] : string.Empty);
    if (PropertiesWindow.CachedFormsMaxCount != 0)
      return (DialogResult) num;
    win.Dispose();
    return (DialogResult) num;
  }

  public static DialogResult Execute(
    ISelectedItems selectedItems,
    string activePageName = null,
    bool readOnly = false)
  {
    if (selectedItems == null)
      throw new ArgumentNullException(nameof (selectedItems));
    using (PropertiesWindow propertiesWindow = new PropertiesWindow())
    {
      propertiesWindow.Text = "Свойства (Карточка)";
      if (selectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
      {
        Intermech.Navigator.DBObjects.Descriptor RootDescriptor = new Intermech.Navigator.DBObjects.Descriptor(itemData.Value);
        propertiesWindow.LoadData(string.Empty, string.Empty, (IDescriptor) RootDescriptor, itemData.Value, readOnly, selectedItems);
      }
      else
      {
        propertiesWindow._services.RemoveService(typeof (IViewState));
        ViewStateFlags flags = ViewStateFlags.InDialog | ViewStateFlags.NoCompositionView | ViewStateFlags.NoContainsInView | ViewStateFlags.NoPluginsViews | ViewStateFlags.InParametersCard;
        if (readOnly)
          flags |= ViewStateFlags.ReadOnly;
        propertiesWindow._services.AddService(typeof (IViewState), (object) new ViewStateService(flags));
        propertiesWindow.pages.UpdateViews(selectedItems, false);
        propertiesWindow.UpdateControls();
      }
      return !string.IsNullOrEmpty(activePageName) ? propertiesWindow.ShowDialog(activePageName) : propertiesWindow.ShowDialog();
    }
  }

  /// <summary>
  /// Открыть форму, активировать на ней закладку с указанным именем
  /// </summary>
  /// <param name="pageName">Имя активируемой закладки или String.Empty</param>
  /// <returns>Результат вызова формы</returns>
  public virtual DialogResult ShowDialog(string pageName)
  {
    if (pageName != string.Empty)
    {
      for (int index = 0; index < this.pages.ViewPages.Count; ++index)
      {
        if (this.pages.ViewPages[index].Name == pageName)
        {
          this.pages.ActiveViewPage = this.pages.ViewPages[index];
          break;
        }
      }
    }
    return this.ShowDialog();
  }

  /// <summary>Получение списка элементов для менеджера закладок</summary>
  /// <param name="form">Форма</param>
  /// <param name="descriptor">Описание родительского объекта</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns>Список элементов для менеджера закладок</returns>
  internal static ISelectedItems GetItems(
    PropertiesWindow form,
    IDescriptor descriptor,
    System.IServiceProvider services)
  {
    if (form._inSelectedItems != null)
      return form._inSelectedItems;
    NodeIDPath handlerPath = new NodeIDPath(descriptor);
    INode handler = (INode) new EtherealNode(handlerPath.RootDescriptor);
    NodeIDCollection nodeIDs = new NodeIDCollection();
    INodeQuery query = handler.GetQuery(ContentType.Folders);
    query.Execute((object) null, 1);
    nodeIDs.Add(query.GetRecordNodeID(0), "0");
    return (ISelectedItems) new NodeItems(handlerPath, handler, nodeIDs, services);
  }

  /// <summary>Инициализировать сервисы</summary>
  private void ServicesInitialization()
  {
    this._services = (IServiceContainer) new ServiceContainer();
    this._notificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this._services.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.NoCompositionView | ViewStateFlags.NoContainsInView | ViewStateFlags.NoPluginsViews));
    if (!this.DesignMode)
      this._commandManager = (ICommandManager) new CommandManager();
    if (this._commandManager != null)
      this._services.AddService(typeof (ICommandManager), (object) this._commandManager);
    if (this._notificationService != null)
    {
      this._services.AddService(typeof (INotificationService), (object) this._notificationService);
      this._notificationService.Subscribe(new NotificationEventHandler(this.NotificationEventFired));
    }
    this._services.AddService(typeof (IIODispatcher), (object) this);
    this._hotKeysManager = ServicesManager.GetService(typeof (IHotKeysManager)) as IHotKeysManager;
    this.pages.Services = (System.IServiceProvider) this._services;
  }

  /// <summary>Деинициализировать сервисы</summary>
  private void ServicesFinalization()
  {
    this._services.RemoveService(typeof (IIODispatcher));
    this._services.RemoveService(typeof (INotificationService));
    this._services.RemoveService(typeof (ICommandManager));
    this._services.RemoveService(typeof (IViewState));
    if (this._notificationService == null)
      return;
    this._notificationService.Unsubscribe(new NotificationEventHandler(this.NotificationEventFired));
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public void UpdateControls()
  {
    this.btnCancel.Enabled = true;
    this.btnCancel.Visible = true;
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void PropertiesWindow_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this, (IDictionary) this._controlsSettings);
    if (this._controlsSettings == null)
      this._controlsSettings = new HybridDictionary(0, true);
    this.SetControlsState(this._controlsSettings);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void PropertiesWindow_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.GetControlsState(this._controlsSettings);
    FormStorage.SaveLayout((Control) this, (IDictionary) this._controlsSettings);
  }

  /// <summary>
  /// Считать хоть-что нибудь из коллекции по указанному ключу
  /// </summary>
  /// <param name="collection">Коллекция настроек</param>
  /// <param name="key">Ключ</param>
  /// <param name="defaultValue">Значение по умолчанию</param>
  /// <returns>Что-нибудь да и вернёт</returns>
  private object GetDicValue(HybridDictionary collection, object key, object defaultValue)
  {
    return collection == null || key == null ? defaultValue : collection[key] ?? defaultValue;
  }

  /// <summary>
  /// Собрать у контролов разные настройки типа ширины, т.п.
  /// </summary>
  /// <param name="controlsState">Коллекция с настройками контролов</param>
  private void GetControlsState(HybridDictionary controlsState)
  {
  }

  /// <summary>
  /// Установить контролам разные настройки типа ширины, т.п.
  /// </summary>
  /// <param name="controlsState">Коллекция с настройками контролов</param>
  private void SetControlsState(HybridDictionary controlsState)
  {
  }

  /// <summary>Выполнить указанное действие</summary>
  /// <param name="commandState">Действие</param>
  /// <returns>true, если действие выполнено</returns>
  public bool Execute(ICommandState commandState) => false;

  /// <summary>Установить статус для указанного действия</summary>
  /// <param name="commandState">Действие</param>
  /// <returns>true, если статус установлен</returns>
  public bool QueryStatus(ICommandState commandState) => false;

  /// <summary>
  /// Вызвать выполнение первой разрешённой команды контекстного меню для указанного события
  /// </summary>
  /// <param name="commands">Команды контекстного меню</param>
  /// <param name="ioEvent">Событие</param>
  /// <returns>true, если команда обработана</returns>
  internal bool ExecuteMenuCommand(List<IHotKeysCommand> commands, IIOEvent ioEvent)
  {
    if (commands == null || commands.Count == 0 || ioEvent == null || ioEvent.Source.SelectedItems == null)
      return false;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(ioEvent.Source.SelectedItems, ioEvent.Source.Services, false);
    string commandName = string.Empty;
    for (int index = 0; index < commands.Count; ++index)
    {
      if (commandsTable.Contains(commands[index].Command))
      {
        commandName = commands[index].Command;
        break;
      }
    }
    if (commandName == string.Empty)
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, ioEvent.Source.Services);
    return true;
  }

  /// <summary>
  /// Вызвать выполнение указанной команды контекстного меню для указанного события
  /// </summary>
  /// <param name="command">Команда контекстного меню</param>
  /// <param name="ioEvent">Событие</param>
  /// <returns>true, если команда обработана</returns>
  internal bool ExecuteMenuCommand(string command, IIOEvent ioEvent)
  {
    if (command == string.Empty || ioEvent == null || ioEvent.Source.SelectedItems == null)
      return false;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(ioEvent.Source.SelectedItems, ioEvent.Source.Services, false);
    if (!commandsTable.Contains(command))
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(command, commandsTable, ioEvent.Source.Services);
    return true;
  }

  /// <summary>
  /// Вызвать выполнение указанной команды контекстного меню для указанного события
  /// </summary>
  /// <param name="command">Команда контекстного меню</param>
  /// <param name="ioEvent">Событие</param>
  /// <returns>true, если команда обработана</returns>
  internal bool ExecuteMenuCommand(IDefaultCommand command, IIOEvent ioEvent)
  {
    return command != null && ioEvent != null && ioEvent.Source.SelectedItems != null && this.ExecuteMenuCommand(command.DefaultCommandName, ioEvent);
  }

  /// <summary>Зарегистрировать указанный обработчик событий</summary>
  /// <param name="Destination">Обработчик событий</param>
  public void RegisterDestination(IIODestination Destination)
  {
  }

  /// <summary>Удалить указанный разработчик событий</summary>
  /// <param name="Destination">Обработчик событий</param>
  public void UnregisterDestination(IIODestination Destination)
  {
  }

  /// <summary>Обработать указанное событие</summary>
  /// <param name="Event">Событие</param>
  public void ProcessEvent(IIOEvent Event)
  {
    if (Event == null || Event.EventType != IOEventType.evKeyUp && Event.EventType != IOEventType.evKeyDown || this._hotKeysManager == null)
      return;
    KeyEventArgs eventData = (KeyEventArgs) Event.EventData;
    if (Event.EventType == IOEventType.evKeyDown && eventData.KeyCode == eventData.KeyData && eventData.Modifiers == Keys.None || Event.EventType == IOEventType.evKeyUp && eventData.KeyCode != eventData.KeyData && eventData.Modifiers != Keys.None)
      return;
    List<IHotKeysCommand> commands = this._hotKeysManager[((KeyEventArgs) Event.EventData).KeyCode | ((KeyEventArgs) Event.EventData).Modifiers];
    if (commands == null || commands.Count <= 0)
      return;
    ((KeyEventArgs) Event.EventData).Handled = true;
    this.ExecuteMenuCommand(commands, Event);
  }

  /// <summary>Попытка закрыть форму</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void PropertiesWindow_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.pages.CanClose((object) this))
      e.Cancel = true;
    else
      this.pages.SaveChanges();
    if (e.Cancel)
      return;
    this.pages.CloseViews();
  }

  /// <summary>Обработать событие от службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  public void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (!(e.EventName == "ObjectsChanged") || !(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null)
      return;
    objectsEventArgs.ObjectIDs.IndexOf(this._objID);
  }

  /// <summary>нажата кнопка вызова помощи - показать справку</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void PropertiesWindow_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    e.Cancel = true;
    IViewPage activeViewPage = this.pages.ActiveViewPage;
    if (activeViewPage == null)
      return;
    HelpProvidersClass.ShowHelpTopic(activeViewPage.HelpID, activeViewPage.HelpPath);
  }

  /// <summary>нажата f1 - показать справку</summary>
  /// <param name="sender"></param>
  /// <param name="hlpevent"></param>
  private void PropertiesWindow_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    IViewPage activeViewPage = this.pages.ActiveViewPage;
    if (activeViewPage == null)
      return;
    HelpProvidersClass.ShowHelpTopic(activeViewPage.HelpID, activeViewPage.HelpPath);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PropertiesWindow));
    this.bottomDock = new DockContainer();
    this.dockContainer1 = new DockContainer();
    this.dockContainer2 = new DockContainer();
    this.btnCancel = new Button();
    this.pages = new PageViewsManager();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.bottomDock, "bottomDock");
    this.bottomDock.Guid = new Guid("13d3ed8f-906d-4ae4-8b15-5e6e12838558");
    this.bottomDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.bottomDock.Manager = (DockManager) null;
    this.bottomDock.Name = "bottomDock";
    this.bottomDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.dockContainer1, "dockContainer1");
    this.dockContainer1.Guid = new Guid("13d3ed8f-906d-4ae4-8b15-5e6e12838558");
    this.dockContainer1.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.dockContainer1.Manager = (DockManager) null;
    this.dockContainer1.Name = "dockContainer1";
    this.dockContainer1.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.dockContainer2, "dockContainer2");
    this.dockContainer2.Guid = new Guid("13d3ed8f-906d-4ae4-8b15-5e6e12838558");
    this.dockContainer2.LayoutSystem = new SplitLayoutSystem(250, 400);
    this.dockContainer2.Manager = (DockManager) null;
    this.dockContainer2.Name = "dockContainer2";
    this.dockContainer2.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.pages.ActiveViewPage = (IViewPage) null;
    componentResourceManager.ApplyResources((object) this.pages, "pages");
    this.pages.CausesValidation = false;
    this.pages.Name = "pages";
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.pages);
    this.Controls.Add((Control) this.dockContainer2);
    this.Controls.Add((Control) this.dockContainer1);
    this.Controls.Add((Control) this.bottomDock);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (PropertiesWindow);
    this.SizeGripStyle = SizeGripStyle.Show;
    this.HelpButtonClicked += new CancelEventHandler(this.PropertiesWindow_HelpButtonClicked);
    this.FormClosing += new FormClosingEventHandler(this.PropertiesWindow_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.PropertiesWindow_FormClosed);
    this.Load += new EventHandler(this.PropertiesWindow_Load);
    this.HelpRequested += new HelpEventHandler(this.PropertiesWindow_HelpRequested);
    this.ResumeLayout(false);
  }

  /// <summary>Свалка констант для формы PropertiesWindow</summary>
  internal static class PropertiesWindowConsts
  {
    /// <summary>
    /// Заголовок формы - "Карточка объекта [{0}] \"{1}\" (\"{2}\")"
    /// </summary>
    internal static readonly string FormCaption = LocalizationHolder.rm.GetString("Client.Core_600");
    /// <summary>Ошибка</summary>
    internal static readonly string Dialog0 = LocalizationHolder.rm.GetString("Client.Core_82");
    /// <summary>Объект с ID версии = [{0}] не найден в базе данных.</summary>
    internal static readonly string Dialog1 = LocalizationHolder.rm.GetString("Client.Core_601");
  }
}
