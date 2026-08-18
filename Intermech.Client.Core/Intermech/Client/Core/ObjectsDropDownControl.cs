
// Type: Intermech.Client.Core.ObjectsDropDownControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;


namespace Intermech.Client.Core;

/// <summary>
/// "Обёртка" над элементом Intermech.Bars.DropDownMenuItem, позволяющая
/// формировать список, связанный с объектами информационной системы
/// </summary>
public class ObjectsDropDownControl
{
  /// <summary>
  /// Коллекция объектов, которая "привязывается" к указанному меню
  /// </summary>
  protected List<MyObjectElement> items;
  /// <summary>Группирующий элемент</summary>
  protected MyObjectElement groupItem;
  /// <summary>Выделенный элемент</summary>
  protected long selectedItem;
  /// <summary>
  /// Список типов объектов, создание которых отслеживается контролом
  /// </summary>
  protected List<int> monitoredTypes;
  /// <summary>Меню, с которым связан данный компонент</summary>
  protected DropDownMenuItem menu;
  /// <summary>Опции</summary>
  protected ObjectsDropDownOptions options;
  /// <summary>Заголовок кнопки</summary>
  protected string caption;
  /// <summary>Изображение кнопки</summary>
  protected Image image;
  /// <summary>Список именованных значков</summary>
  protected INamedImageList namedImageList;
  /// <summary>
  /// Сервис для хранения изображений элементов навигации, привязанных к
  /// категориям, типам и состояниям элементов.
  /// </summary>
  protected ICategoryTypeIconService categoryImageService;
  /// <summary>Информация о текущем пользователе и его настройках</summary>
  protected ICurrentUserAndRole userAndRole;
  /// <summary>Служба уведомлений</summary>
  protected INotificationService notifyService;
  /// <summary>Обработчик событий от службы уведомлений</summary>
  protected NotificationEventHandler notifyHandler;
  /// <summary>Служба по созданию объектов</summary>
  protected IObjectCreatorService creatorService;
  /// <summary>Обработчик событий от службы по созданию объектов</summary>
  protected AfterObjectCreatedEventHandler creatorHandler;
  /// <summary>Обработчик для главной кнопки</summary>
  protected EventHandler handlerMainButton;
  /// <summary>Обработчик для группы</summary>
  protected EventHandler handlerGroupItem;
  /// <summary>Обработчик для элементов</summary>
  protected EventHandler handlerItem;

  /// <summary>Выделенный в списке элемент</summary>
  public virtual long SelectedItem
  {
    [DebuggerStepThrough] get => this.selectedItem;
    set
    {
      if (this.selectedItem == value)
      {
        if (this.Find(this.selectedItem) != null || this.groupItem == null)
          return;
        this.selectedItem = this.groupItem.ObjectID;
      }
      else
      {
        this.selectedItem = value;
        MyObjectElement groupItem = this.Find(this.selectedItem);
        if (groupItem == null && this.groupItem != null)
        {
          this.selectedItem = this.groupItem.ObjectID;
          groupItem = this.groupItem;
        }
        if ((this.options & ObjectsDropDownOptions.MoveSelectedOnTop) == ObjectsDropDownOptions.MoveSelectedOnTop && groupItem != this.groupItem && groupItem != null)
        {
          this.items.Remove(groupItem);
          this.items.Insert(0, groupItem);
        }
        this.FillDropDownMenu();
        this.UpdateControls();
      }
    }
  }

  /// <summary>
  /// Выполнена ли инициализация сервисов компонента и осуществлена "привязка" к компоненту типа DropDownMenuItem
  /// </summary>
  public virtual bool ServicesInitialized
  {
    [DebuggerStepThrough] get => this.menu != null && this.userAndRole != null;
  }

  /// <summary>
  /// Создать "обёртку" для указанного меню без инициализации меню
  /// </summary>
  /// <param name="menu">Меню, для которого требуется создать "обёртку"</param>
  /// <param name="options">Опции</param>
  /// <param name="caption">Заголовок кнопки</param>
  /// <param name="image">Изображение кнопки</param>
  /// <param name="groupItem">Группирующий элемент (null, если не задана опция WithGroupItem)</param>
  public ObjectsDropDownControl(
    DropDownMenuItem menu,
    ObjectsDropDownOptions options,
    string caption,
    Image image,
    MyObjectElement groupItem)
  {
    if (menu == null)
      throw new ArgumentNullException(nameof (menu), LocalizationHolder.rm.GetString("Client.Core_1390"));
    if (menu.Tag != null)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Client.Core_1391"), nameof (menu));
    this.InitServices();
    this.caption = caption;
    this.image = image;
    this.menu = menu;
    this.items = new List<MyObjectElement>();
    this.monitoredTypes = new List<int>();
    this.options = options;
    this.groupItem = groupItem;
    this.groupItem = (this.options & ObjectsDropDownOptions.WithGroupItem) != ObjectsDropDownOptions.WithGroupItem ? (MyObjectElement) null : this.groupItem ?? new MyObjectElement(0L, LocalizationHolder.rm.GetString("Client.Core_1392"), (object) null, -1);
    this.menu.Tag = (object) this;
  }

  /// <summary>
  /// Создать "обёртку" для указанного меню, инициализировать меню
  /// </summary>
  /// <param name="menu">Меню, для которого требуется создать "обёртку"</param>
  /// <param name="options">Опции</param>
  /// <param name="caption">Заголовок кнопки</param>
  /// <param name="image">Изображение кнопки</param>
  /// <param name="objectIDs">Список идентификаторов версий объектов</param>
  /// <param name="monitoredTypes">Список отслеживаемых типов объектов (или null)</param>
  /// <param name="selectedItem">Выделенный в списке элемент</param>
  /// <param name="groupItem">Группирующий элемент (null, если не задана опция WithGroupItem)</param>
  public ObjectsDropDownControl(
    DropDownMenuItem menu,
    ObjectsDropDownOptions options,
    string caption,
    Image image,
    MyObjectElement groupItem,
    IList<long> objectIDs,
    IList<int> monitoredTypes,
    long selectedItem)
    : this(menu, options, caption, image, groupItem)
  {
    this.Load(caption, image, objectIDs, monitoredTypes, selectedItem);
  }

  /// <summary>
  /// Выполнить проверку контрола на инициализацию сервисов и привязку к меню,
  /// сгенерировать исключение, если инициализация не выполнена
  /// </summary>
  protected virtual void CheckServicesInitialization()
  {
    if (!this.ServicesInitialized)
      throw new Exception(LocalizationHolder.rm.GetString("Client.Core_1393"));
  }

  /// <summary>
  /// Загрузить информацию из базы данных и "привязать" её к меню
  /// </summary>
  /// <param name="objectIDs">Список идентификаторов версий объектов</param>
  /// <param name="selectedItem">Выделенный в списке элемент</param>
  public virtual void Load(IList<long> objectIDs, long selectedItem)
  {
    this.Load(this.caption, this.image, objectIDs, (IList<int>) this.monitoredTypes, selectedItem);
  }

  /// <summary>
  /// Загрузить информацию из базы данных и "привязать" её к меню
  /// </summary>
  /// <param name="caption">Заголовок кнопки</param>
  /// <param name="image">Изображение кнопки</param>
  /// <param name="objectIDs">Список идентификаторов версий объектов</param>
  /// <param name="monitoredTypes">Список отслеживаемых типов объектов (или null)</param>
  /// <param name="selectedItem">Выделенный в списке элемент</param>
  public virtual void Load(
    string caption,
    Image image,
    IList<long> objectIDs,
    IList<int> monitoredTypes,
    long selectedItem)
  {
    this.CheckServicesInitialization();
    this.caption = caption;
    this.image = image;
    this.Initialize(objectIDs, monitoredTypes, selectedItem);
    this.FillDropDownMenu();
    this.UpdateControls();
  }

  /// <summary>
  /// Выполнить инициализацию элемента управления исходными данными (только загрузить из базы данных)
  /// </summary>
  /// <param name="objectIDs">Список идентификаторов версий объектов</param>
  /// <param name="monitoredTypes">Список отслеживаемых типов объектов (или null)</param>
  public virtual void Initialize(IList<long> objectIDs, IList<int> monitoredTypes)
  {
    this.Initialize(objectIDs, monitoredTypes, 0L);
  }

  /// <summary>
  /// Выполнить инициализацию элемента управления исходными данными (только загрузить из базы данных)
  /// </summary>
  /// <param name="objectIDs">Список идентификаторов версий объектов</param>
  /// <param name="monitoredTypes">Список отслеживаемых типов объектов (или null)</param>
  /// <param name="selectedObjectID">Версия объекта, которая должна быть выделена в списке</param>
  public virtual void Initialize(
    IList<long> objectIDs,
    IList<int> monitoredTypes,
    long selectedObjectID)
  {
    this.monitoredTypes = new List<int>((IEnumerable<int>) monitoredTypes);
    this.items = this.LoadDescriptions(objectIDs);
    this.selectedItem = this.Find(selectedObjectID) != null ? selectedObjectID : 0L;
  }

  /// <summary>
  /// Заполнить меню элементами из коллекции (без загрузки из базы данных)
  /// </summary>
  protected virtual void FillDropDownMenu()
  {
    this.CheckServicesInitialization();
    this.menu.Items.Clear();
    this.menu.MenuImageList = this.categoryImageService.ImageList;
    MyObjectElement groupItem = this.Find(this.SelectedItem);
    if (groupItem == null && this.groupItem != null && Math.Abs(this.groupItem.ObjectID) == Math.Abs(this.SelectedItem))
      groupItem = this.groupItem;
    if ((this.options & ObjectsDropDownOptions.SelectOnly) != ObjectsDropDownOptions.SelectOnly)
    {
      if (groupItem != null)
      {
        this.menu.Text = groupItem.Caption;
        this.menu.Tag = (object) groupItem;
        this.menu.ToolTipText = groupItem.Caption;
        int index = this.categoryImageService.IndexOf(4, groupItem.ObjectType);
        this.menu.Image = index >= 0 ? this.categoryImageService.ImageList.Images[index] : this.image;
        this.menu.Checked = (this.options & ObjectsDropDownOptions.SelectOnly) != ObjectsDropDownOptions.SelectOnly && Math.Abs(groupItem.ObjectID) == Math.Abs(this.SelectedItem) && (this.groupItem == null || Math.Abs(groupItem.ObjectID) != Math.Abs(this.groupItem.ObjectID));
      }
    }
    else
    {
      this.menu.Text = this.caption;
      this.menu.ToolTipText = this.caption;
      this.menu.Tag = (object) null;
      this.menu.Image = this.image;
    }
    this.menu.ShowText = (this.options & ObjectsDropDownOptions.ShowText) == ObjectsDropDownOptions.ShowText;
    this.menu.Stretch = (this.options & ObjectsDropDownOptions.Stretch) == ObjectsDropDownOptions.Stretch;
    if (this.handlerMainButton == null)
    {
      this.handlerMainButton = new EventHandler(this.OnMainButtonClick);
      this.menu.Click += this.handlerMainButton;
    }
    if ((this.options & ObjectsDropDownOptions.SelectOnly) != ObjectsDropDownOptions.SelectOnly && (this.options & ObjectsDropDownOptions.WithGroupItem) == ObjectsDropDownOptions.WithGroupItem && this.groupItem != null)
    {
      int index = this.categoryImageService.IndexOf(4, this.groupItem.ObjectType);
      MenuButtonItem menuButtonItem = new MenuButtonItem(this.groupItem.Caption, new EventHandler(this.OnGroupItemClick), -1);
      menuButtonItem.Tag = (object) this.groupItem;
      menuButtonItem.ToolTipText = this.groupItem.Caption;
      menuButtonItem.AutoToggle = AutoToggleType.Single;
      menuButtonItem.Image = index >= 0 ? this.categoryImageService.ImageList.Images[index] : this.image;
      this.menu.Items.Add((ToolbarItemBase) menuButtonItem);
      menuButtonItem.AutoToggle = AutoToggleType.Radio;
      menuButtonItem.Checked = (this.options & ObjectsDropDownOptions.SelectOnly) != ObjectsDropDownOptions.SelectOnly && Math.Abs(this.groupItem.ObjectID) == Math.Abs(this.SelectedItem);
    }
    for (int index1 = 0; index1 < this.items.Count; ++index1)
    {
      int index2 = this.categoryImageService.IndexOf(4, this.items[index1].ObjectType);
      MenuButtonItem menuButtonItem = new MenuButtonItem(this.items[index1].Caption, new EventHandler(this.OnItemClick), -1);
      menuButtonItem.Tag = (object) this.items[index1];
      menuButtonItem.AutoToggle = AutoToggleType.Single;
      if ((this.options & ObjectsDropDownOptions.ShowItemsImages) == ObjectsDropDownOptions.ShowItemsImages)
        menuButtonItem.Image = index2 >= 0 ? this.categoryImageService.ImageList.Images[index2] : this.image;
      IMSObjectType objectType = MetaDataHelper.GetObjectType(this.items[index1].ObjectType);
      menuButtonItem.ToolTipText = objectType != null ? objectType.ObjectName : string.Empty;
      this.menu.Items.Add((ToolbarItemBase) menuButtonItem);
      menuButtonItem.AutoToggle = AutoToggleType.Radio;
      menuButtonItem.Checked = (this.options & ObjectsDropDownOptions.SelectOnly) != ObjectsDropDownOptions.SelectOnly && Math.Abs(this.items[index1].ObjectID) == Math.Abs(this.SelectedItem);
      if ((this.options & ObjectsDropDownOptions.MoveSelectedOnTop) == ObjectsDropDownOptions.MoveSelectedOnTop && menuButtonItem.Checked)
        menuButtonItem.Index = 1;
    }
    if (this.groupItem == null || this.menu.Items.Count <= 1)
      return;
    this.menu.Items[1].BeginGroup = true;
  }

  /// <summary>Обновить состояние элементов</summary>
  protected virtual void UpdateControls()
  {
  }

  /// <summary>Инициализировать сервисы, подписаться на события</summary>
  protected virtual void InitServices()
  {
    if (this.userAndRole != null)
      return;
    this.namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this.categoryImageService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this.userAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this.notifyService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this.creatorService = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    if (this.notifyHandler == null)
    {
      this.notifyHandler = new NotificationEventHandler(this.NotificationEventFired);
      this.notifyService.Subscribe(this.notifyHandler);
    }
    if (this.creatorHandler != null)
      return;
    this.creatorHandler = new AfterObjectCreatedEventHandler(this.ObjectCreatorCompleatedEventHandler);
    this.creatorService.AfterObjectCreatedEvent += this.creatorHandler;
  }

  /// <summary>
  /// Освободить ссылки на сервисы, отменить подписки, "отвязать" компонент от меню, удалить все ссылки, очистить коллекцию объектов
  /// </summary>
  protected virtual void ReleaseServices()
  {
    if (this.userAndRole == null)
      return;
    if (this.notifyHandler != null)
    {
      this.notifyService.Unsubscribe(this.notifyHandler);
      this.notifyHandler = (NotificationEventHandler) null;
    }
    if (this.creatorHandler != null)
    {
      this.creatorService.AfterObjectCreatedEvent -= this.creatorHandler;
      this.creatorHandler = (AfterObjectCreatedEventHandler) null;
    }
    this.namedImageList = (INamedImageList) null;
    this.categoryImageService = (ICategoryTypeIconService) null;
    this.userAndRole = (ICurrentUserAndRole) null;
    this.notifyService = (INotificationService) null;
    this.creatorService = (IObjectCreatorService) null;
    this.menu.Tag = (object) null;
    this.items = new List<MyObjectElement>();
    this.monitoredTypes = new List<int>();
    this.options = ObjectsDropDownOptions.None;
    this.caption = string.Empty;
    this.image = (Image) null;
  }

  /// <summary>Получено очередное событие от службы уведомлений</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (e.EventName == "ObjectsRemoved" || e.EventName == "ObjectsChanged" || e.EventName == "ObjectsCheckedIn" || e.EventName == "ObjectsChangesCancelled")
    {
      if (!(e is DBObjectsEventArgs e1) || e1.ObjectIDs == null)
        return;
      List<MyObjectElement> all = this.FindAll(this.ConvertTo(e1.ObjectIDs, false));
      if (all == null || all.Count == 0)
        return;
      switch (e.EventName)
      {
        case "ObjectsRemoved":
          this.ObjectsDeleted(sender, e1, all);
          break;
        case "ObjectsChanged":
          this.ObjectsChanged(sender, e1, all);
          break;
        case "ObjectsCheckedIn":
          this.ObjectsCheckedIn(sender, e1, all);
          break;
        case "ObjectsChangesCancelled":
          this.ObjectsChangesCancelled(sender, e1, all);
          break;
      }
    }
    else
    {
      if (!(e.EventName == "ObjectsCheckedOut") || !(e is DBObjectsCheckOutEventArgs e2) || e2.ObjectIDs == null)
        return;
      List<MyObjectElement> all = this.FindAll(this.ConvertTo(e2.ObjectIDs, false));
      if (all == null || all.Count == 0)
        return;
      this.ObjectsCheckedOut(sender, e2, all);
    }
  }

  /// <summary>Создан новый объект</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="ea">Аргументы события</param>
  protected virtual void ObjectCreatorCompleatedEventHandler(
    object sender,
    AfterObjectCreatedEventArgs ea)
  {
    if ((this.options & ObjectsDropDownOptions.AutoAppendNewObjects) != ObjectsDropDownOptions.AutoAppendNewObjects || ea == null || this.monitoredTypes.Count == 0 || !this.monitoredTypes.Exists((Predicate<int>) (monitoredType => MetaDataHelper.IsObjectTypeChildOf(ea.ObjectTypeID, monitoredType))))
      return;
    this.ObjectCreated(sender, ea);
  }

  /// <summary>Добавить/обновить/удалить указанный объект</summary>
  /// <param name="objectID">Версия обновляемого объекта</param>
  /// <param name="useActualCopy">Если равно true, система отыщет актуальную копию указанного объекта</param>
  /// <param name="moveOnTop">Переместить указанный объект в начало коллекции</param>
  /// <param name="select">Отметить этот элемент в меню</param>
  public virtual void AlterObject(long objectID, bool useActualCopy, bool moveOnTop, bool select)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = objectID == 0L ? (IDBObject) null : (useActualCopy ? sessionKeeper.Session.GetObjectActualCopy(Math.Abs(objectID), false) : sessionKeeper.Session.GetObject(objectID, false));
      if (dbObject == null)
      {
        this.Remove(objectID, true);
        if (objectID != 0L)
          return;
        this.SelectedItem = 0L;
        return;
      }
      MyObjectElement myObjectElement1 = this.Find(objectID);
      if (myObjectElement1 == null)
      {
        MyObjectElement myObjectElement2 = new MyObjectElement(dbObject.ObjectID, dbObject.Caption, (object) null, dbObject.ObjectType);
        if ((this.options & ObjectsDropDownOptions.MoveSelectedOnTop) == ObjectsDropDownOptions.MoveSelectedOnTop)
          this.items.Insert(0, myObjectElement2);
        else
          this.items.Add(myObjectElement2);
        if (select)
        {
          if ((this.options & ObjectsDropDownOptions.SelectOnly) != ObjectsDropDownOptions.SelectOnly)
            this.SelectedItem = myObjectElement2.ObjectID;
        }
      }
      else
      {
        bool flag = Math.Abs(myObjectElement1.ObjectID) == Math.Abs(this.SelectedItem);
        myObjectElement1.ObjectID = dbObject.ObjectID;
        myObjectElement1.Caption = dbObject.Caption;
        myObjectElement1.ObjectType = dbObject.ObjectType;
        if (select)
        {
          if (!flag)
          {
            if ((this.options & ObjectsDropDownOptions.SelectOnly) != ObjectsDropDownOptions.SelectOnly)
              this.SelectedItem = myObjectElement1.ObjectID;
          }
        }
      }
    }
    this.FillDropDownMenu();
    this.UpdateControls();
  }

  /// <summary>Добавить/обновить/удалить указанные объекты</summary>
  /// <param name="objectIDs">Версиb обновляемых объектов</param>
  /// <param name="useActualCopy">Если равно true, система отыщет актуальную копию указанного объекта</param>
  public virtual void AlterObjects(IList<long> objectIDs, bool useActualCopy)
  {
    if (objectIDs == null || objectIDs.Count == 0)
      return;
    if (objectIDs.Count == 1)
    {
      this.AlterObject(objectIDs[0], useActualCopy, false, false);
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = 0; index < objectIDs.Count; ++index)
        {
          long objectId = objectIDs[index];
          IDBObject dbObject = useActualCopy ? sessionKeeper.Session.GetObjectActualCopy(Math.Abs(objectId), false) : sessionKeeper.Session.GetObject(objectId, false);
          if (dbObject == null)
          {
            this.Remove(objectId, false);
          }
          else
          {
            MyObjectElement myObjectElement = this.Find(objectId);
            if (myObjectElement == null)
            {
              this.items.Add(new MyObjectElement(dbObject.ObjectID, dbObject.Caption, (object) null, dbObject.ObjectType));
            }
            else
            {
              myObjectElement.ObjectID = dbObject.ObjectID;
              myObjectElement.Caption = dbObject.Caption;
              myObjectElement.ObjectType = dbObject.ObjectType;
            }
          }
        }
      }
      this.FillDropDownMenu();
      this.UpdateControls();
    }
  }

  /// <summary>Удалить информацию об указанном объекте</summary>
  /// <param name="objectID">Удаляемая версия объекта</param>
  /// <param name="updateControl">true - перестроить меню</param>
  protected virtual void Remove(long objectID, bool updateControl)
  {
    if (objectID == 0L)
      return;
    MyObjectElement myObjectElement = this.Find(objectID);
    if (myObjectElement == null)
      return;
    if (Math.Abs(this.SelectedItem) == Math.Abs(myObjectElement.ObjectID))
      this.selectedItem = this.groupItem != null ? this.groupItem.ObjectID : 0L;
    this.items.Remove(myObjectElement);
    if (!updateControl)
      return;
    this.FillDropDownMenu();
    this.UpdateControls();
  }

  /// <summary>Удалить информацию об указанных объектах</summary>
  /// <param name="objectIDs">Удаляемые версии объектов</param>
  /// <param name="updateControl">true - перестроить меню</param>
  protected virtual void Remove(IList<long> objectIDs, bool updateControl)
  {
    if (objectIDs == null || objectIDs.Count == 0)
      return;
    for (int index = 0; index < objectIDs.Count; ++index)
    {
      MyObjectElement myObjectElement = this.Find(objectIDs[index]);
      if (myObjectElement != null)
      {
        if (Math.Abs(this.SelectedItem) == Math.Abs(myObjectElement.ObjectID))
          this.selectedItem = this.groupItem != null ? this.groupItem.ObjectID : 0L;
        this.items.Remove(myObjectElement);
      }
    }
    if (!updateControl)
      return;
    this.FillDropDownMenu();
    this.UpdateControls();
  }

  /// <summary>
  /// Загрузить описания указанных объектов, сохранив порядок
  /// </summary>
  /// <param name="source">Версии объектов</param>
  /// <returns>Описания указанных объектов</returns>
  protected virtual List<MyObjectElement> LoadDescriptions(IList<long> source)
  {
    List<MyObjectElement> myObjectElementList = new List<MyObjectElement>();
    if (source == null || source.Count == 0)
      return myObjectElementList;
    List<long> objectIDs = this.ConvertTo(source, false);
    Dictionary<long, MyObjectElement> dictionary1 = new Dictionary<long, MyObjectElement>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<Tuple<long, int>> objectTypes = sessionKeeper.Session.GetObjectTypes((ICollection<long>) objectIDs);
      if (objectTypes == null || objectTypes.Count == 0)
        return myObjectElementList;
      Dictionary<int, List<long>> dictionary2 = new Dictionary<int, List<long>>();
      for (int index = 0; index < objectTypes.Count; ++index)
      {
        int parentObjectTypeId = MetaDataHelper.GetTopParentObjectTypeID(objectTypes[index].Item2);
        if (!dictionary2.ContainsKey(parentObjectTypeId))
          dictionary2.Add(parentObjectTypeId, new List<long>());
        dictionary2[parentObjectTypeId].Add(objectTypes[index].Item1);
      }
      foreach (KeyValuePair<int, List<long>> keyValuePair in dictionary2)
      {
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(keyValuePair.Key);
        ColumnDescriptor[] columns = new ColumnDescriptor[3]
        {
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0)
        };
        object[] objArray = new object[0];
        SortOrders[] sortOrdersArray = new SortOrders[0];
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) keyValuePair.Value.ToArray(), LogicalOperators.NONE, 0, true)
        }, columns);
        DataTable dataTable;
        try
        {
          dataTable = objectCollection.Select(paramSet);
        }
        catch (Exception ex)
        {
          dataTable = (DataTable) null;
        }
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
            if (!dictionary1.ContainsKey(int64Value))
            {
              int int32Value = DataSetProcessor.GetInt32Value(row, 1, -1);
              string caption = row[2].ToString();
              MyObjectElement myObjectElement = new MyObjectElement(int64Value, caption, (object) null, int32Value);
              dictionary1.Add(int64Value, myObjectElement);
            }
          }
        }
        dataTable?.Dispose();
      }
      for (int index = 0; index < objectTypes.Count; ++index)
      {
        if (dictionary1.ContainsKey(objectTypes[index].Item1))
          myObjectElementList.Add(dictionary1[objectTypes[index].Item1]);
      }
    }
    return myObjectElementList;
  }

  /// <summary>
  /// Преобразовать список, реализующий интерфейс IList[Int64], в список типа List[Int64]
  /// (для удобства работы с методами, поддерживающими аргументы-делегаты)
  /// </summary>
  /// <param name="source">Исходные данные</param>
  /// <param name="toAbs">Преобразовывать к абсолютным величинам</param>
  /// <returns>Список-результат</returns>
  protected virtual List<long> ConvertTo(IList<long> source, bool toAbs)
  {
    if (source is List<long> longList1)
      return longList1;
    if (source == null)
      return new List<long>();
    List<long> longList2 = new List<long>(source.Count);
    for (int index = 0; index < source.Count; ++index)
      longList2.Add(toAbs ? Math.Abs(source[index]) : source[index]);
    return longList2;
  }

  /// <summary>
  /// Отыскать индекс указанной версии объекта (используется абсолютное значение для поиска)
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта (абсолютное значение)</param>
  /// <returns>Индекс найденной версии или -1</returns>
  protected virtual int IndexOf(long objectID)
  {
    objectID = Math.Abs(objectID);
    return this.items.FindIndex((Predicate<MyObjectElement>) (item => Math.Abs(item.ObjectID) == objectID));
  }

  /// <summary>
  /// Отыскать описание указанной версии объекта (используется абсолютное значение для поиска)
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта (абсолютное значение)</param>
  /// <returns>Описание найденной версии или null</returns>
  protected virtual MyObjectElement Find(long objectID)
  {
    objectID = Math.Abs(objectID);
    return this.items.Find((Predicate<MyObjectElement>) (item => Math.Abs(item.ObjectID) == objectID));
  }

  /// <summary>
  /// Отыскать описания указанных версий объекта (используется абсолютное значение для поиска)
  /// </summary>
  /// <param name="objectIDs">Идентификаторы версий объектов (абсолютные значения)</param>
  /// <returns>Список с найденными описаниями</returns>
  protected virtual List<MyObjectElement> FindAll(List<long> objectIDs)
  {
    objectIDs = objectIDs.ConvertAll<long>((Converter<long, long>) (item => Math.Abs(item)));
    return this.items.FindAll((Predicate<MyObjectElement>) (item => objectIDs.IndexOf(Math.Abs(item.ObjectID)) >= 0));
  }

  /// <summary>
  /// Проверить наличие любой указанных версий объекта (используется абсолютное значение для поиска) в списке
  /// </summary>
  /// <param name="objectIDs">Идентификаторы версий объектов (абсолютные значения)</param>
  /// <returns>true - в списке найдена как минимум одна из указанных версий объектов</returns>
  protected virtual bool ExistsAny(List<long> objectIDs)
  {
    objectIDs = objectIDs.ConvertAll<long>((Converter<long, long>) (item => Math.Abs(item)));
    return this.items.Exists((Predicate<MyObjectElement>) (item => objectIDs.IndexOf(Math.Abs(item.ObjectID)) >= 0));
  }

  /// <summary>
  /// Создан новый объект, тип данных которого найден в фильтре
  /// </summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ObjectCreated(object sender, AfterObjectCreatedEventArgs e)
  {
    this.CheckServicesInitialization();
    if ((this.options & ObjectsDropDownOptions.AutoAppendNewObjects) != ObjectsDropDownOptions.AutoAppendNewObjects)
      return;
    this.AlterObject(e.ObjectID, true, false, false);
  }

  /// <summary>Изменён объект, найденный в списке</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  /// <param name="foundVersions">Список описаний версий объектов из события, которые найдены в объектах класса</param>
  protected virtual void ObjectsChanged(
    object sender,
    DBObjectsEventArgs e,
    List<MyObjectElement> foundVersions)
  {
    this.CheckServicesInitialization();
    if (foundVersions == null || foundVersions.Count == 0)
      return;
    this.AlterObjects((IList<long>) foundVersions.ConvertAll<long>((Converter<MyObjectElement, long>) (item => item.ObjectID)), true);
  }

  /// <summary>Удалён объект, который найден в списке</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  /// <param name="foundVersions">Список описаний версий объектов из события, которые найдены в объектах класса</param>
  protected virtual void ObjectsDeleted(
    object sender,
    DBObjectsEventArgs e,
    List<MyObjectElement> foundVersions)
  {
    if (foundVersions == null || foundVersions.Count == 0)
      return;
    this.Remove((IList<long>) foundVersions.ConvertAll<long>((Converter<MyObjectElement, long>) (item => item.ObjectID)), true);
  }

  /// <summary>У объекта, найденного в списке, завершены изменения</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  /// <param name="foundVersions">Список описаний версий объектов из события, которые найдены в объектах класса</param>
  protected virtual void ObjectsCheckedIn(
    object sender,
    DBObjectsEventArgs e,
    List<MyObjectElement> foundVersions)
  {
    this.CheckServicesInitialization();
    if (foundVersions == null || foundVersions.Count == 0)
      return;
    this.AlterObjects((IList<long>) foundVersions.ConvertAll<long>((Converter<MyObjectElement, long>) (item => item.ObjectID)), true);
  }

  /// <summary>У объект, найденого в списке, отменены изменения</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  /// <param name="foundVersions">Список описаний версий объектов из события, которые найдены в объектах класса</param>
  protected virtual void ObjectsChangesCancelled(
    object sender,
    DBObjectsEventArgs e,
    List<MyObjectElement> foundVersions)
  {
    this.CheckServicesInitialization();
    if (foundVersions == null || foundVersions.Count == 0)
      return;
    this.AlterObjects((IList<long>) foundVersions.ConvertAll<long>((Converter<MyObjectElement, long>) (item => item.ObjectID)), true);
  }

  /// <summary>Объект, найденный в списке, взят на изменение</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  /// <param name="foundVersions">Список описаний версий объектов из события, которые найдены в объектах класса</param>
  protected virtual void ObjectsCheckedOut(
    object sender,
    DBObjectsCheckOutEventArgs e,
    List<MyObjectElement> foundVersions)
  {
    this.CheckServicesInitialization();
    if (foundVersions == null || foundVersions.Count == 0)
      return;
    this.AlterObjects((IList<long>) foundVersions.ConvertAll<long>((Converter<MyObjectElement, long>) (item => item.ObjectID)), true);
  }

  /// <summary>Нажата главная кнопка меню (должно открыться меню)</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnMainButtonClick(object sender, EventArgs e)
  {
    this.CheckServicesInitialization();
    if (this.menu.Items.Count == 0)
      return;
    this.menu.Show();
  }

  /// <summary>Вызван группирующий элемент меню</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnGroupItemClick(object sender, EventArgs e)
  {
    this.CheckServicesInitialization();
    if (this.groupItem == null)
      return;
    this.SelectedItem = this.groupItem.ObjectID;
  }

  /// <summary>Вызван обычный элемент меню</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnItemClick(object sender, EventArgs e)
  {
    this.CheckServicesInitialization();
    MyObjectElement tag = sender is MenuButtonItem menuButtonItem ? menuButtonItem.Tag as MyObjectElement : (MyObjectElement) null;
    if (tag == null)
      return;
    this.SelectedItem = tag.ObjectID;
  }
}
