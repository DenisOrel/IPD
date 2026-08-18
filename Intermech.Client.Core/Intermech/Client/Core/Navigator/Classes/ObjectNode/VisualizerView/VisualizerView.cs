
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.VisualizerView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using Intermech.Client.Core.Visualizers;
using Intermech.Collections;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.Redline;
using Intermech.Tools.CommonTasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

/// <summary>Вкладка "Просмотр"</summary>
[ViewDescriptionProvider(typeof (Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.VisualizerView.VisualizerViewDescriptionProvider))]
public class VisualizerView : UserControl, IView
{
  /// <summary>Идентификатор объекта, для которого используется вид</summary>
  protected long _objectId;
  /// <summary>Тип объекта, для которого используется вид</summary>
  protected int _objectType = -1;
  /// <summary>Признак загружен ли просмотр</summary>
  protected bool _dataLoaded;
  /// <summary>
  /// Отображаемые файлы - как реальные файлы, так и виртуальные элементы
  /// </summary>
  private IList<FileItem> _fileItems = (IList<FileItem>) new List<FileItem>();
  /// <summary>Выбранный отображаемый файл</summary>
  private FileItem _selectedFileItem;
  /// <summary>
  /// 
  /// </summary>
  private int _cbPagesMinimumControlWidth = 150;
  /// <summary>
  /// 
  /// </summary>
  private int _cbFilesMinimumControlWidth = 250;
  /// <summary>
  /// 
  /// </summary>
  private bool _ignoreChanges;
  /// <summary>Наблюдатель за файлом</summary>
  private FileSystemWatcher _fileWatcher = new FileSystemWatcher();
  /// <summary>Дерево из которого была открыта закладка</summary>
  private NavigatorTreeView _parentFocusTree;
  /// <summary>Окно из которого была открыта закладка</summary>
  private ChildrenView _parentFocusView;
  /// <summary>
  /// 
  /// </summary>
  private RelationPair _relationPairKey;
  /// <summary>Элементы пространства навигации для текущей вкладки</summary>
  private ISelectedItems _items;
  /// <summary>Кэш id объекта - имя конфигурации CAD-модели</summary>
  private Dictionary<long, string> _cadModelNameConfigurationDict = new Dictionary<long, string>();
  /// <summary>Таймер перед звгрузкой данных на вкладку</summary>
  private Timer _timer = new Timer() { Interval = 500 };
  /// <summary>
  /// Расширения нативных файлов, которые не извлекаются на диск и не считываются в поток
  /// </summary>
  private string[] _imDocumentsFileExtension = new string[6]
  {
    "imdx",
    "zimd",
    "spx",
    "pex",
    "revx",
    "idcx"
  };
  /// <summary>
  /// Расширения старых бланков, которые не извлекаются на диск и не считываются в поток
  /// </summary>
  private string[] _oldBlankFileExtension = new string[2]
  {
    "bln",
    "lib"
  };
  /// <summary>Расширения виртуальных файлов</summary>
  private string[] _virtualFileExtension = new string[2]
  {
    ExtensionsConsts.ExactSpecificationExtension,
    ExtensionsConsts.LibraryImageExtension
  };
  /// <summary>
  /// Признак восстановления фокуса на родительском элементе
  /// </summary>
  private bool _needRestoreFocus;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Bars.ToolBar toolBar;
  private ComboBoxItem cbFiles;
  private ButtonItem btFirstPage;
  private ButtonItem btPrevPage;
  private ComboBoxItem cbPages;
  private ButtonItem btNextPage;
  private ButtonItem btLastPage;
  private ButtonItem btRedView;
  private ButtonItem btRedInfo;
  private ButtonItem btColorDWG;
  private ButtonItem btOverview;
  private ButtonItem btZoomPrevious;
  private ButtonItem btZoomIn;
  private ButtonItem btZoomOut;
  private ButtonItem btZoom1to1;
  private ButtonItem btZoomAll;
  private ButtonItem btDistance;
  private ButtonItem btEmpty;
  private MenuBar menuBar;
  private ContextMenuBarItem zoomContextMenu;
  private MenuButtonItem mnZoomPrevious;
  private MenuButtonItem mnZoomIn;
  private MenuButtonItem mnZoomOut;
  private MenuButtonItem mnZoom1to1;
  private MenuButtonItem mnZoomAll;
  private MenuButtonItem mnRedNoteProperties;
  private ViewerHost viewerHost;
  private ButtonItem btExternalRedliningEditor;

  /// <summary>Коофициент растяжения графики (DpiX / 96)</summary>
  private float FactorDpiX { get; }

  /// <summary>
  /// Сервис для хранения икон, привязанных к категориям и(или) типам
  /// </summary>
  private ICategoryTypeIconService CategoryTypeIconService { get; } = ServiceUtils.GetService<ICategoryTypeIconService>((object) ServicesManager.ServiceContainer, true);

  /// <summary>Сервис для работы с именоваными иконками</summary>
  private INamedImageList NamedImageList { get; } = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, true);

  /// <summary>Ядро службы уведомлений клиента</summary>
  private INotificationService NotificationService { get; } = ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, true);

  /// <summary>
  /// Интерфейс позволяет расширять список просматриваемых файлов объекта путем регистрации подписчиков
  /// </summary>
  private IPreviewExtender PreviewExtender { get; } = ServiceUtils.GetService<IPreviewExtender>((object) ServicesManager.ServiceContainer, true);

  /// <summary>Интерфейс клиентского сервиса интеграции с IMViewer</summary>
  private IIMViewerClientService iMViewerClientService { get; } = ServiceUtils.GetService<IIMViewerClientService>((object) ServicesManager.ServiceContainer, true);

  /// <summary>Сервис BarManager</summary>
  private BarManager BarManager { get; } = ServiceUtils.GetService<BarManager>((object) ServicesManager.ServiceContainer, true);

  /// <summary>
  /// 
  /// </summary>
  private bool ViewEnabled => this.viewerHost.Visible;

  /// <summary>
  /// 
  /// </summary>
  private bool IsVisible
  {
    get
    {
      return this.Parent != null && this.Parent.Visible && this.Height != 0 && this.Width != 0 && this.Visible;
    }
  }

  /// <summary>Текущий просмотровщик</summary>
  private IViewer CurrentViewer => this.viewerHost.CurrentView;

  /// <summary>Default constructor</summary>
  public VisualizerView()
  {
    this.InitializeComponent();
    if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
      this.viewerHost.InitializeServices();
    this._dataLoaded = false;
    this.InitializeToolbar();
    this.SubsribeWatcherEvents();
    this.SubscribeClientServices();
    this.SubscribeFilesComboBoxEvents();
    this.SubcribeTimerEvents();
    using (Graphics graphics = this.CreateGraphics())
      this.FactorDpiX = graphics.DpiX / 96f;
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
    this._items = items;
    this._parentFocusView = services.GetService(typeof (ChildrenView)) as ChildrenView;
    this._parentFocusTree = items.GetItemData(0, typeof (NavigatorTreeNode)) is NavigatorTreeNode itemData1 ? itemData1.Tree : (NavigatorTreeView) null;
    this._needRestoreFocus = true;
    ChildrenView service = services.GetService(typeof (ChildrenView)) as ChildrenView;
    this._relationPairKey = (RelationPair) null;
    if (service != null)
      this._relationPairKey = service.GetRootObjectKey();
    if (itemData1?.Handler is IContextAware handler)
      this._relationPairKey = handler.Services.GetService(typeof (RelationPair)) as RelationPair;
    if (items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData2)
    {
      this._objectId = itemData2.ObjectID;
      this._objectType = itemData2.ObjectType;
    }
    else
    {
      this._objectId = ((IDBObjectID) items.GetItemData(0, typeof (IDBObjectID))).Value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._objectId);
        if (objectInfo.Empty)
        {
          this._objectId = -this._objectId;
          objectInfo = sessionKeeper.Session.GetObjectInfo(this._objectId);
        }
        this._objectType = objectInfo.ObjectTypeID;
      }
    }
    this._dataLoaded = false;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране. Этот метод вызывается при
  /// первом показе закладки, а также при переключении на нее с другой закладки.
  /// </summary>
  /// <param name="previousView">
  /// Закладка, с которой осуществляется переключение. Может быть null для самой первой
  /// показываемой на экране закладки.
  /// </param>
  public void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    this.UpdateButtons();
    this.CheckLoadData();
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">
  /// Закладка, на которую осуществляется переключение. Может быть null, если выполяется
  /// не переключение, а удаление закладок.
  /// </param>
  public void Deactivate(IView nextView)
  {
    if (!(this.CurrentViewer is IRedlinerSupport currentViewer))
      return;
    currentViewer.ResetRankSignature();
  }

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  public virtual string Caption => LocalizationHolder.rm.GetString("Client.Core_378");

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  public virtual int OrderID => 40;

  /// <summary>Изображение</summary>
  public int ImageIndex
  {
    get
    {
      INamedImageList namedImageList = this.NamedImageList;
      return namedImageList == null ? -1 : namedImageList.ImageIndex("imgView");
    }
  }

  /// <summary>Подписка на события клиентских сервисов</summary>
  private void SubscribeClientServices()
  {
    this.SubscribeNotificationService();
    this.SubscribeRenderChanged();
  }

  /// <summary>Подписка на события INotificationService</summary>
  private void SubscribeNotificationService()
  {
    this.NotificationService.Subscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectChanged));
    this.NotificationService.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.OnObjectCheckedInOut));
    this.NotificationService.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.OnObjectCheckedInOut));
    this.NotificationService.Subscribe("ObjectsRemoved", new NotificationEventHandler(this.OnObjectRemoved));
  }

  /// <summary>Подписка на события BarManager</summary>
  private void SubscribeRenderChanged()
  {
    this.BarManager.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
    this.ToolbarRendererChanged((object) this.BarManager, EventArgs.Empty);
  }

  /// <summary>Отписка от событий клиентских сервисов</summary>
  private void UnsubscribeClientServices()
  {
    this.UnsubscribeNotificationService();
    this.UnsubscribeRenderChanges();
  }

  /// <summary>Отписка от событий INotificationService</summary>
  private void UnsubscribeNotificationService()
  {
    this.NotificationService.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectChanged));
    this.NotificationService.Unsubscribe("ObjectsCheckedIn", new NotificationEventHandler(this.OnObjectCheckedInOut));
    this.NotificationService.Unsubscribe("ObjectsCheckedOut", new NotificationEventHandler(this.OnObjectCheckedInOut));
    this.NotificationService.Unsubscribe("ObjectsRemoved", new NotificationEventHandler(this.OnObjectRemoved));
  }

  /// <summary>Отписка от событий BarManager</summary>
  private void UnsubscribeRenderChanges()
  {
    this.menuBar.Renderer = this.toolBar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
    this.BarManager.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
  }

  /// <summary>Подписка на события наблюдателя</summary>
  private void SubsribeWatcherEvents()
  {
    this._fileWatcher.Changed += new FileSystemEventHandler(this.Watcher_Changed);
    this._fileWatcher.Created += new FileSystemEventHandler(this.Watcher_Changed);
    this._fileWatcher.Deleted += new FileSystemEventHandler(this.Watcher_Changed);
    this._fileWatcher.Renamed += new RenamedEventHandler(this.Watcher_Changed);
  }

  /// <summary>Отписка от событий наблюдателя</summary>
  private void UnsubscribeWatcherEvents()
  {
    this._fileWatcher.Changed -= new FileSystemEventHandler(this.Watcher_Changed);
    this._fileWatcher.Created -= new FileSystemEventHandler(this.Watcher_Changed);
    this._fileWatcher.Deleted -= new FileSystemEventHandler(this.Watcher_Changed);
    this._fileWatcher.Renamed -= new RenamedEventHandler(this.Watcher_Changed);
  }

  /// <summary>Запуск наблюдателя</summary>
  /// <param name="path"></param>
  private void StartWatcher(string path)
  {
    this._fileWatcher.Path = Path.GetDirectoryName(path);
    this._fileWatcher.Filter = Path.GetFileName(path);
    this._fileWatcher.EnableRaisingEvents = true;
  }

  /// <summary>Остановка наблюдателя</summary>
  private void StopWatcher() => this._fileWatcher.EnableRaisingEvents = false;

  /// <summary>уничтожаем объект наблюдателя</summary>
  private void DisposeWatcher()
  {
    this._fileWatcher.EnableRaisingEvents = false;
    this.UnsubscribeWatcherEvents();
    this._fileWatcher.Dispose();
  }

  /// <summary>Подписка на события ComboBox</summary>
  private void SubscribeFilesComboBoxEvents()
  {
    this.cbFiles.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.cbFiles.ComboBox.DrawItem += new DrawItemEventHandler(this.FilesListCombo_DrawItem);
    this.cbFiles.ComboBox.SelectedIndexChanged += new EventHandler(this.FilesCombo_SelectedIndexChanged);
    this.cbFiles.ComboBox.DropDown += new EventHandler(this.filesCombo_DropDown);
  }

  /// <summary>Отписка от событий ComboBox</summary>
  private void UnsubscribeFilesComboBoxEvents()
  {
    this.cbFiles.ComboBox.DrawItem -= new DrawItemEventHandler(this.FilesListCombo_DrawItem);
    this.cbFiles.ComboBox.SelectedIndexChanged -= new EventHandler(this.FilesCombo_SelectedIndexChanged);
    this.cbFiles.ComboBox.DropDown -= new EventHandler(this.filesCombo_DropDown);
    this.cbFiles.ComboBox.Items.Clear();
  }

  /// <summary>Подписка на события таймера</summary>
  private void SubcribeTimerEvents() => this._timer.Tick += new EventHandler(this._timer_Tick);

  /// <summary>Отписка от событий таймера</summary>
  private void UnsubcribeTimerEvents()
  {
    this._timer.Stop();
    this._timer.Tick -= new EventHandler(this._timer_Tick);
  }

  /// <summary>Инициализация панели инструментов</summary>
  private void InitializeToolbar()
  {
    this.toolBar.ImageList = this.NamedImageList.ImageList;
    this.menuBar.ImageList = this.NamedImageList.ImageList;
    this.cbFiles.DefaultText = LocalizationHolder.rm.GetString("NoFiles");
    this.cbFiles.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_297");
    this.cbFiles.Text = LocalizationHolder.rm.GetString("Files");
    this.cbFiles.ComboBox.FlatStyle = this.cbPages.ComboBox.FlatStyle = FlatStyle.Flat;
    this.btOverview.ImageIndex = this.NamedImageList.ImageIndex("imgOverview");
    this.btOverview.ToolTipText = LocalizationHolder.rm.GetString("CommonDocumentView");
    this.btZoomAll.ImageIndex = this.mnZoomAll.ImageIndex = this.NamedImageList.ImageIndex("imgZoomAll");
    this.btZoomAll.ToolTipText = this.mnZoomAll.Text = LocalizationHolder.rm.GetString("AllDocument");
    this.btZoom1to1.ImageIndex = this.mnZoom1to1.ImageIndex = this.NamedImageList.ImageIndex("imgZoom1to1");
    this.btZoom1to1.ToolTipText = this.mnZoom1to1.Text = LocalizationHolder.rm.GetString("OriginalSize");
    this.btZoomIn.ImageIndex = this.mnZoomIn.ImageIndex = this.NamedImageList.ImageIndex("imgZoomIn");
    this.btZoomIn.ToolTipText = this.mnZoomIn.Text = LocalizationHolder.rm.GetString("ZoomIn");
    this.btZoomOut.ImageIndex = this.mnZoomOut.ImageIndex = this.NamedImageList.ImageIndex("imgZoomOut");
    this.btZoomOut.ToolTipText = this.mnZoomOut.Text = LocalizationHolder.rm.GetString("ZoomOut");
    this.btZoomPrevious.ImageIndex = this.mnZoomPrevious.ImageIndex = this.NamedImageList.ImageIndex("imgZoomPrevious");
    this.btZoomPrevious.ToolTipText = this.mnZoomPrevious.Text = LocalizationHolder.rm.GetString("PriorView");
    this.btFirstPage.ToolTipText = LocalizationHolder.rm.GetString("FirstPage");
    this.btFirstPage.ImageIndex = this.NamedImageList.ImageIndex("imgPageFirst");
    this.btPrevPage.ToolTipText = LocalizationHolder.rm.GetString("PriorPage");
    this.btPrevPage.ImageIndex = this.NamedImageList.ImageIndex("imgPagePrev");
    this.btNextPage.ToolTipText = LocalizationHolder.rm.GetString("NextPage");
    this.btNextPage.ImageIndex = this.NamedImageList.ImageIndex("imgPageNext");
    this.btLastPage.ToolTipText = LocalizationHolder.rm.GetString("LastPage");
    this.btLastPage.ImageIndex = this.NamedImageList.ImageIndex("imgPageLast");
    this.btRedView.ImageIndex = this.NamedImageList.ImageIndex("imgRedEdit");
    this.btRedView.ToolTipText = LocalizationHolder.rm.GetString("EditNotes");
    this.btRedInfo.ImageIndex = this.NamedImageList.ImageIndex("imgRedViewOnly");
    this.btRedInfo.ToolTipText = LocalizationHolder.rm.GetString("ViewNotes");
    this.btDistance.ToolTipText = LocalizationHolder.rm.GetString("MeasureDistance");
    this.btDistance.ImageIndex = this.NamedImageList.ImageIndex("imgDistance");
    this.mnRedNoteProperties.Text = LocalizationHolder.rm.GetString("CommentProperties");
    this.btColorDWG.ToolTipText = LocalizationHolder.rm.GetString("ChangeColorAndWidthLine");
    this.btExternalRedliningEditor.ToolTipText = LocalizationHolder.rm.GetString("ExternalRedliningEditor");
    this.btExternalRedliningEditor.ImageIndex = this.NamedImageList.ImageIndex("imgExtRedliningEditor");
  }

  /// <summary>Принудительная перезагрузка данных</summary>
  private void ForceReloadData()
  {
    this._dataLoaded = false;
    if (!this.IsVisible)
      return;
    this.LoadData();
    this.UpdateButtons();
  }

  /// <summary>Привязка контекстного меню</summary>
  /// <param name="ctrl"></param>
  private void AttachContextMenu(Control ctrl)
  {
    this.menuBar.SetPopupMenu(ctrl, (MenuBarItem) this.zoomContextMenu);
  }

  /// <summary>Отвязка контекстного меню</summary>
  /// <param name="ctrl"></param>
  private void DetachContextMenu(Control ctrl)
  {
    this.menuBar.SetPopupMenu(ctrl, (MenuBarItem) null);
  }

  /// <summary>Востановить фокус у вызвовшего панель просмотра</summary>
  private void OnParentFocus()
  {
    this._parentFocusView?._grid.Focus();
    this._parentFocusTree?.Focus();
    Application.DoEvents();
  }

  /// <summary>Проверка загружены ли данные - если нет, то загружает</summary>
  private void CheckLoadData()
  {
    if (this._dataLoaded)
      return;
    this.TryLoadData();
    this.UpdateButtons();
  }

  /// <summary>
  /// Запуск таймера перед загрузкой данных
  /// Сама загрузка происходит в LoadData
  /// </summary>
  private void TryLoadData()
  {
    this._timer.Stop();
    this._timer.Start();
  }

  /// <summary>Загрузить данные для просмотра</summary>
  private void LoadData()
  {
    this.CloseCurrentViewer();
    if (this._objectId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectId, false);
      if (dbObject == null)
        return;
      if (this._objectType == -1)
        this._objectType = dbObject.ObjectType;
      if (!this.IsPicture(dbObject))
        this.FillFileItems(dbObject);
    }
    this._dataLoaded = true;
  }

  /// <summary>Объект содержит в себе только картинку</summary>
  /// <param name="dbObject"></param>
  /// <returns></returns>
  private bool IsPicture(IDBObject dbObject)
  {
    IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Client.Core.Thumbnail.Consts.ImageAttTypeID);
    if (dbObject.ObjectType != Intermech.Client.Core.Thumbnail.Consts.ImageLibraryItemTypeID && (attributeById == null || attributeById.AsInteger < 0L))
      return false;
    this.Open(new FileItem(dbObject.ObjectID, dbObject.ObjectType, dbObject.Caption, -1)
    {
      FileName = "img." + ExtensionsConsts.LibraryImageExtension
    });
    return true;
  }

  /// <summary>Наполнение списка просматриваемых файлов</summary>
  /// <param name="dbObject"></param>
  private void FillFileItems(IDBObject dbObject)
  {
    this.cbFiles.ComboBox.Items.Clear();
    this._fileItems.Clear();
    this._selectedFileItem = (FileItem) null;
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0013a-306c-11d8-b4e9-00304f19f545"), false);
    DateTime contentModifyDate = attributeByGuid == null || attributeByGuid.IsNull ? DateTime.MinValue : attributeByGuid.AsDateTime;
    List<FileBlobItem> items = new List<FileBlobItem>();
    long preferedBlobID = -1;
    List<FileBlobItem> fileBlobItems = this.GetFileBlobItems(items, ref preferedBlobID);
    this.FillFileBlobItems(dbObject, fileBlobItems);
    this.PopulateFileItems(fileBlobItems, contentModifyDate);
    this.FillFilesCombo(this.GetPreferedBlobId(dbObject, preferedBlobID));
  }

  /// <summary>Получение blobid</summary>
  /// <param name="dbObject"></param>
  /// <param name="preferedBlobID"></param>
  /// <returns></returns>
  private long GetPreferedBlobId(IDBObject dbObject, long preferedBlobID)
  {
    long preferedBlobId = this.ReplaceByPriorAuthFile(this.DeterminePrefferedBlobId(preferedBlobID));
    return this.ReplaceByImViewItems(dbObject, preferedBlobId);
  }

  /// <summary>Определение приоритетного blobid</summary>
  /// <param name="preferedBlobId"></param>
  /// <returns></returns>
  private long DeterminePrefferedBlobId(long preferedBlobId)
  {
    long prefferedBlobId = preferedBlobId;
    if (preferedBlobId != -1L)
      return prefferedBlobId;
    string masterFile = ClientContext.FileVault.DBFilesInfo.GetMasterFileName(this._objectId, false);
    FileItem fileItem = this._fileItems.FirstOrDefault<FileItem>((System.Func<FileItem, bool>) (x => string.Compare(x.FileName, masterFile, true) == 0));
    return fileItem == null ? prefferedBlobId : fileItem.BlobID;
  }

  /// <summary>
  /// Замена blobid на аутентичный, если такое есть в настройках
  /// </summary>
  /// <param name="preferedBlobId"></param>
  /// <returns></returns>
  private long ReplaceByPriorAuthFile(long preferedBlobId)
  {
    long num = preferedBlobId;
    FileItem prefFileItem = this._fileItems.FirstOrDefault<FileItem>((System.Func<FileItem, bool>) (x => x.BlobID == preferedBlobId));
    if (prefFileItem == null)
      return num;
    IExtensionsService service1 = ApplicationServices.Container.GetService<IExtensionsService>();
    IReadOnlyCollection<int> authenticFileObjTypes = service1.GetPriorityViewAuthenticFileObjTypes();
    if (!authenticFileObjTypes.Any<int>())
      return num;
    foreach (int parentType in (IEnumerable<int>) authenticFileObjTypes)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(prefFileItem.ObjectType, parentType))
      {
        if (prefFileItem.FileType == FileTypes.ftAuthentical)
          return num;
        List<FileItem> list = this._fileItems.Where<FileItem>((System.Func<FileItem, bool>) (x => x.ObjectId == prefFileItem.ObjectId && x.FileType == FileTypes.ftAuthentical)).ToList<FileItem>();
        if (!list.Any<FileItem>())
          return num;
        FileItem fileItem = list.FirstOrDefault<FileItem>((System.Func<FileItem, bool>) (x => string.Compare(Path.GetFileNameWithoutExtension(x.FileName), Path.GetFileNameWithoutExtension(prefFileItem.FileName), true) == 0));
        if (service1.DebugMode)
        {
          IOutputView service2 = ServiceUtils.GetService<IOutputView>((object) ServicesManager.ServiceContainer, false);
          if (service2 != null)
          {
            service2.WriteString(LocalizationHolder.rm.GetString("Client.Core_378"), "");
            service2.WriteString(LocalizationHolder.rm.GetString("Client.Core_378"), "Для просмотра выбран аутентичный файл согласно приоритетному порядку отображения");
          }
        }
        return fileItem != null ? fileItem.BlobID : list[0].BlobID;
      }
    }
    return num;
  }

  /// <summary>
  /// Получим от различных подписчиков перечень необходимых для извлечения файловых блобов
  /// </summary>
  /// <param name="items"></param>
  /// <param name="preferedBlobID"></param>
  /// <returns></returns>
  private List<FileBlobItem> GetFileBlobItems(List<FileBlobItem> items, ref long preferedBlobID)
  {
    if (this.PreviewExtender is Intermech.Client.Core.Visualizers.PreviewExtender previewExtender)
    {
      previewExtender.GetObjects(this._objectType, this._objectId, items, ref preferedBlobID);
      if (items.Count > 1)
        items = items.Distinct<FileBlobItem>().ToList<FileBlobItem>();
    }
    return items;
  }

  /// <summary>Получение файловых blob</summary>
  /// <param name="dbObject"></param>
  /// <param name="items"></param>
  /// <remarks>
  /// Просматривается только атрибут "Файл", т.к. для других файловых атрибутов
  /// нет API для извлечения файлов в область просмотра
  /// </remarks>
  private void FillFileBlobItems(IDBObject dbObject, List<FileBlobItem> items)
  {
    IDBAttribute attributeByGuid = dbObject?.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid == null)
      return;
    int valuesCount = attributeByGuid.ValuesCount;
    for (int valueIndex = 0; valueIndex < valuesCount; ++valueIndex)
    {
      attributeByGuid.Index = valueIndex;
      if (attributeByGuid is IBlobReader blobReader && !string.IsNullOrEmpty(blobReader.OpenBlob(-1).FileName))
      {
        FileBlobItem fileBlobItem = new FileBlobItem(dbObject.ObjectID, attributeByGuid.AttributeID, valueIndex);
        if (!items.Contains(fileBlobItem))
          items.Add(fileBlobItem);
      }
    }
  }

  /// <summary>Наполение кэша имен конфигураций CAD моделей</summary>
  /// <param name="dbObject"></param>
  private void FillCadModelNameDict(IDBObject dbObject)
  {
    if (!this.iMViewerClientService.Settings.EnableIntegration)
      return;
    this._cadModelNameConfigurationDict.Clear();
    if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00268-306c-11d8-b4e9-00304f19f545")).IndexOf(dbObject.ObjectType) == -1)
      return;
    IDBRelationCollection relationCollection = dbObject.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"));
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cadd95af-306c-11d8-b4e9-00304f19f545");
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(attributeTypeId, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) attributeTypeId, AttributeSourceTypes.Relation, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable source = relationCollection.ConsistFrom(paramSet, dbObject.ObjectID);
    if (source.Rows.Count <= 0)
      return;
    this._cadModelNameConfigurationDict = source.AsEnumerable().GroupBy<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToDictionary<IGrouping<long, DataRow>, long, string>((System.Func<IGrouping<long, DataRow>, long>) (x => x.Key), (System.Func<IGrouping<long, DataRow>, string>) (y => y.Select<DataRow, string>((System.Func<DataRow, string>) (x => Convert.ToString(x[1]))).FirstOrDefault<string>()));
  }

  /// <summary>Формирование списка отображаемых файлов</summary>
  /// <param name="items"></param>
  /// <param name="contentModifyDate"></param>
  private void PopulateFileItems(List<FileBlobItem> items, DateTime contentModifyDate)
  {
    if (this.IsExactSpecification(this._objectType))
      this._fileItems.Add(new FileItem(this._objectId, this._objectType, "Точная Спецификация", this.CategoryTypeIconService.IndexOf(4, this._objectType), 0)
      {
        FileName = "Точная Спецификация." + ExtensionsConsts.ExactSpecificationExtension
      });
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (IGrouping<long, FileBlobItem> grouping in items.GroupBy<FileBlobItem, long>((System.Func<FileBlobItem, long>) (x => x.ObjectId)))
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(grouping.Key);
        int imageIndex = this.CategoryTypeIconService.IndexOf(4, dbObject.TypeID);
        this._fileItems.Add(new FileItem(dbObject.ObjectID, dbObject.ObjectType, dbObject.Caption, imageIndex));
        foreach (FileBlobItem fileBlobItem in (IEnumerable<FileBlobItem>) grouping)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(fileBlobItem.AttId);
          if (attributeById != null)
          {
            attributeById.Index = fileBlobItem.ValueIndex;
            if (attributeById is IBlobReader blobReader)
            {
              BlobInformation blobInformation = blobReader.OpenBlob(-1);
              if (!(blobInformation.FileName == "") && blobInformation.RealFileSize != 0L)
              {
                FileItem fileItem = new FileItem(attributeById.DBObjectID, dbObject.ObjectType, fileBlobItem.AttId, blobInformation, attributeById.Index);
                AttributeSingleValueClass singleValueClass = new AttributeSingleValueClass(blobInformation, -1L);
                singleValueClass.InitializeColorText(blobInformation.FileType, contentModifyDate);
                fileItem.ColorText = singleValueClass.ColorText;
                this._fileItems.Add(fileItem);
              }
            }
          }
        }
      }
    }
  }

  /// <summary>Заменить на файлы ImViewer при их наличии</summary>
  /// <param name="preferedBlobId"></param>
  /// <returns></returns>
  private long ReplaceByImViewItems(IDBObject dbObject, long preferedBlobId)
  {
    if (!this.iMViewerClientService.Settings.EnableIntegration)
      return preferedBlobId;
    long num = preferedBlobId;
    this.FillCadModelNameDict(dbObject);
    foreach (IGrouping<long, FileItem> grouping in this._fileItems.GroupBy<FileItem, long>((System.Func<FileItem, long>) (x => x.ObjectId)))
    {
      IGrouping<long, FileItem> itemByObject = grouping;
      FileItem fileItem1;
      if (this.IsImviewerFile(itemByObject.Key, itemByObject.First<FileItem>().ObjectType, out fileItem1))
      {
        List<FileItem> list = this._fileItems.Where<FileItem>((System.Func<FileItem, bool>) (x => x.IsFile && x.ObjectId == itemByObject.Key && x.FileType == FileTypes.ftNormal)).ToList<FileItem>();
        foreach (FileItem fileItem2 in list)
          this._fileItems.Remove(fileItem2);
        this._fileItems.Insert(this._fileItems.IndexOf(this._fileItems.FirstOrDefault<FileItem>((System.Func<FileItem, bool>) (x => !x.IsFile && x.ObjectId == itemByObject.Key))) + 1, fileItem1);
        if (list.Any<FileItem>((System.Func<FileItem, bool>) (x => x.BlobID == preferedBlobId)))
          num = fileItem1.BlobID;
      }
    }
    return num;
  }

  /// <summary>Есть ли у объекта файл ImViewer</summary>
  /// <param name="objectId"></param>
  /// <param name="objectTypeId"></param>
  /// <param name="fileItem"></param>
  /// <returns></returns>
  private bool IsImviewerFile(long objectId, int objectTypeId, out FileItem fileItem)
  {
    fileItem = (FileItem) null;
    if (!this.iMViewerClientService.HasViewerObject(objectId, objectTypeId))
      return false;
    List<IMViewerPublishItem> dataForOpenFiles = this.iMViewerClientService.GetViewerDataForOpenFiles(objectId, objectTypeId, VersionsRuleSources.GetCurrentWindowRule());
    if (this.IsPublishDataNotPresentOrNotSet(dataForOpenFiles[0]))
      return false;
    bool ImViewerFileState = this.IsActualData(dataForOpenFiles);
    CollectionUtils.RemoveAll<IMViewerPublishItem>((IList<IMViewerPublishItem>) dataForOpenFiles, (Predicate<IMViewerPublishItem>) (x => x.SidecarObject == null));
    PublishedObject publishedObject = ClientContext.FileVault.ViewArea.Publish((IList<DBObjectState>) CollectionUtils.ConvertAsList<IMViewerPublishItem, DBObjectState>((ICollection<IMViewerPublishItem>) dataForOpenFiles, (Converter<IMViewerPublishItem, DBObjectState>) (x => x.SidecarObject)));
    if (publishedObject.MasterFile == null)
      return false;
    fileItem = new FileItem(publishedObject.DBObject.ObjectId, objectTypeId, publishedObject.MasterFile.FullName, publishedObject.MasterFile.BlobId, true, ImViewerFileState);
    string savedConfigurationName;
    if (this._cadModelNameConfigurationDict.TryGetValue(objectId, out savedConfigurationName))
    {
      string configurationName = this.iMViewerClientService.GetViewerModelConfigurationName(objectId, objectTypeId, savedConfigurationName);
      fileItem.CadModelNameConfiguration = configurationName;
    }
    return true;
  }

  /// <summary>Проверяет акутальны ли данные для imviewer</summary>
  /// <param name="imvPublishData"></param>
  /// <returns></returns>
  private bool IsActualData(List<IMViewerPublishItem> imvPublishData)
  {
    return !imvPublishData.Any<IMViewerPublishItem>(new System.Func<IMViewerPublishItem, bool>(this.IsPublishDataNotPresentOrNotActual));
  }

  private bool IsPublishDataNotPresentOrNotSet(IMViewerPublishItem imvPublishItem)
  {
    return imvPublishItem.SidecarObject == null || imvPublishItem.SidecarContentStatus == ObjectContentStatus.NotSet;
  }

  private bool IsPublishDataNotPresentOrNotActual(IMViewerPublishItem imvPublishItem)
  {
    return imvPublishItem.SidecarObject == null || imvPublishItem.SidecarContentStatus != ObjectContentStatus.Actual;
  }

  /// <summary>Наполнение combobox</summary>
  /// <param name="preferedBlobId">предпочитаемый bolb для отображения</param>
  private void FillFilesCombo(long preferedBlobId)
  {
    this.cbFiles.ComboBox.BeginUpdate();
    this.cbFiles.ComboBox.Items.Clear();
    int num1 = -1;
    try
    {
      foreach (FileItem fileItem in (IEnumerable<FileItem>) this._fileItems)
      {
        int num2 = this.cbFiles.ComboBox.Items.Add((object) fileItem);
        if (preferedBlobId != -1L)
        {
          if (fileItem.BlobID == preferedBlobId)
            num1 = num2;
        }
        else if (num1 == -1 && fileItem.FileName != null && fileItem.FileName.ToLower().EndsWith(".dwf"))
          num1 = num2;
      }
    }
    finally
    {
      this.cbFiles.ComboBox.EndUpdate();
      if (this.cbFiles.ComboBox.Items.Count > 0)
      {
        if (num1 == -1)
          num1 = Math.Min(1, this.cbFiles.ComboBox.Items.Count - 1);
        this.cbFiles.ComboBox.SelectedIndex = num1;
      }
      this.ChangMinimumControlWidth();
    }
  }

  /// <summary>проверяем, если этот объект(заказ или комплектация или Сборочная Единица) конфигурируемый,и объект может содержать спецификацию </summary>
  /// <param name="objectTypeID">Тип объекта - заказ или комплектация или Сборочная Единица</param>
  /// <returns>true = создание точной спецификации</returns>
  private bool IsExactSpecification(int objectTypeID)
  {
    return ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service && service.EnabledPdmConfigurator && ServiceUtils.GetService<IVisualizerService>((object) ServicesManager.ServiceContainer, false)?.GetVisualizer(ExtensionsConsts.ExactSpecificationExtension) != null && (MetaDataHelper.IsPdmRootObjectType(objectTypeID) || objectTypeID == MetaDataHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545")) && MetaDataHelper.IsPdmConfigurableObjectType(objectTypeID) && MetaDataHelper.HasApplicability(objectTypeID, MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545"), MetaDataHelper.GetRelationTypeID(ExpertObjGUIDs.linkDocForIzd));
  }

  /// <summary>
  /// Является ли файл внутренним файлом ips,
  /// который не следует извлекать на диск и считывать в поток
  /// </summary>
  /// <param name="fileName"></param>
  /// <returns></returns>
  private bool IsIpsDocument(string fileName)
  {
    string str = fileName != null ? this.GetExtension(fileName).ToLower() : throw new ArgumentNullException(nameof (fileName));
    return ((IEnumerable<string>) this._imDocumentsFileExtension).Contains<string>(str) || ((IEnumerable<string>) this._oldBlankFileExtension).Contains<string>(str) || ((IEnumerable<string>) this._virtualFileExtension).Contains<string>(str);
  }

  /// <summary>получить расширение файла без точки</summary>
  /// <param name="fileName">имя файла</param>
  /// <returns>расширение файла без точки</returns>
  private string GetExtension(string fileName)
  {
    string str = Path.GetExtension(fileName)?.ToLower();
    if (str != null && str.StartsWith("."))
      str = str.Substring(1);
    return str;
  }

  /// <summary>очистить содержимое _axViewer</summary>
  private void CloseCurrentViewer()
  {
    if (this.CurrentViewer is IPagerSupport currentViewer1)
    {
      currentViewer1.PagesAdded -= new PagesAddEventHandler(this.PagerSupport_PagesAdded);
      currentViewer1.PageChanged -= new EventHandler(this.PagerSupport_PageChanged);
      this.ClearPageCombobox();
    }
    if (this.CurrentViewer is IZoomSupport currentViewer2)
    {
      currentViewer2.ZoomChanging -= new EventHandler(this.ZoomSupport_ZoomChanging);
      this.DetachContextMenu(currentViewer2.GetControlForContextMenu());
    }
    if (this.CurrentViewer is IDistanceMeasureSupport currentViewer3)
      currentViewer3.DistanceMeasureStateChanged -= new EventHandler(this.DistanceMeasureSupport_DistanceMeasureStateChanged);
    if (this.CurrentViewer is IColorDwgSupport currentViewer4)
      currentViewer4.DwgColorChanged -= new DwgColorChangedEventHandler(this.ColorDwgSupport_DwgColorChanged);
    if (this.CurrentViewer is IRedlinerSupport currentViewer5)
      currentViewer5.FileItemSetCurent -= new SetFileItemEventHandler(this.SetFileItemSupport_FileItemSetCurent);
    this.viewerHost.CloseCurrentViewer();
  }

  /// <summary>Очистка viewerHost</summary>
  private void ClearViewers() => this.viewerHost?.Clear();

  /// <summary>Открыть файл во вьювере</summary>
  /// <param name="fileName">файл</param>
  private void Open(FileItem fileName)
  {
    ServiceContainer serviceContainer = new ServiceContainer();
    if (this._fileItems != null)
      serviceContainer.AddService(typeof (IList<FileItem>), (object) this._fileItems);
    if (this._relationPairKey != null)
      serviceContainer.AddService(typeof (RelationPair), (object) this._relationPairKey);
    if (this._items != null)
      serviceContainer.AddService(typeof (ISelectedItems), (object) this._items);
    this.viewerHost.Visible = true;
    this.viewerHost.Open(fileName, (System.IServiceProvider) serviceContainer);
    this.InitializeSupportedInterfaces(this.CurrentViewer);
    this.UpdateButtons();
    this.RestoreParentFocus();
  }

  /// <summary>
  /// Инициализация поддерживаемых интерфейсов для просмотровщика
  /// </summary>
  /// <param name="currentView"></param>
  private void InitializeSupportedInterfaces(IViewer currentView)
  {
    if (currentView == null)
      return;
    if (currentView is IPagerSupport pagerSupport)
    {
      this.InitializePageCombobox();
      pagerSupport.PagesAdded += new PagesAddEventHandler(this.PagerSupport_PagesAdded);
      pagerSupport.PageChanged += new EventHandler(this.PagerSupport_PageChanged);
      pagerSupport.RaiseLoadedPages();
    }
    if (currentView is IZoomSupport zoomSupport)
    {
      this.AttachContextMenu(zoomSupport.GetControlForContextMenu());
      zoomSupport.ZoomChanging += new EventHandler(this.ZoomSupport_ZoomChanging);
    }
    if (currentView is IDistanceMeasureSupport distanceMeasureSupport)
      distanceMeasureSupport.DistanceMeasureStateChanged += new EventHandler(this.DistanceMeasureSupport_DistanceMeasureStateChanged);
    if (currentView is IColorDwgSupport colorDwgSupport)
      colorDwgSupport.DwgColorChanged += new DwgColorChangedEventHandler(this.ColorDwgSupport_DwgColorChanged);
    if (!(this.CurrentViewer is IRedlinerSupport currentViewer))
      return;
    currentViewer.FileItemSetCurent += new SetFileItemEventHandler(this.SetFileItemSupport_FileItemSetCurent);
  }

  private void SetFileItemSupport_FileItemSetCurent(object sender, SetFileItemEventArgs e)
  {
    if (this.cbFiles.ComboBox.SelectedItem as FileItem == e.FileItem || !this._fileItems.Contains(e.FileItem))
      return;
    this.cbFiles.ComboBox.SelectedItem = (object) e.FileItem;
  }

  /// <summary>Изменился статус интсрумента измерения расстояния</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DistanceMeasureSupport_DistanceMeasureStateChanged(object sender, EventArgs e)
  {
    this.UpdateButtons_Distance();
  }

  /// <summary>Инициализация комбобокса работы со страницами</summary>
  private void InitializePageCombobox()
  {
    this.cbPages.Items.Clear();
    ComboBox comboBox = this.cbPages.ComboBox;
    comboBox.DropDown += new EventHandler(this.pagesCombo_DropDown);
    comboBox.SelectedIndexChanged += new EventHandler(this.Pager_SelectedIndexChanged);
    this.ChangMinimumControlWidth();
  }

  /// <summary>Очистка комбобокса работы со страницами</summary>
  private void ClearPageCombobox()
  {
    ComboBox comboBox = this.cbPages.ComboBox;
    comboBox.BeginUpdate();
    this.cbPages.Stretch = false;
    comboBox.DropDown -= new EventHandler(this.pagesCombo_DropDown);
    comboBox.SelectedIndexChanged -= new EventHandler(this.Pager_SelectedIndexChanged);
    comboBox.Items.Clear();
    comboBox.SelectedItem = (object) null;
    this.cbPages.ToolTipText = string.Empty;
    comboBox.EndUpdate();
  }

  /// <summary>Добавление страниц в комбобокс</summary>
  /// <param name="pages"></param>
  private void AppendPagesComboBox(object[] pages)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.VisualizerView.AppendPagesComboBoxDelegate(this.AppendPagesComboBox), (object) pages);
    }
    else
    {
      ComboBox comboBox = this.cbPages.ComboBox;
      if (comboBox == null)
        return;
      comboBox.BeginUpdate();
      foreach (object page in pages)
      {
        if (!comboBox.Items.Contains(page))
          comboBox.Items.Add(page);
      }
      comboBox.EndUpdate();
      comboBox.Update();
    }
  }

  /// <summary>Установка файла текущим для просмотра</summary>
  /// <param name="fi"></param>
  private void SetValueIndex(FileItem fi)
  {
    if (this._selectedFileItem == fi)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetObjectInfo(fi.ObjectId).Empty)
        fi.ObjectId = -fi.ObjectId;
    }
    this._selectedFileItem = fi;
    this.LoadFile();
  }

  /// <summary>Загрузка выбранного файла для отображения</summary>
  private void LoadFile()
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new MethodInvoker(this.LoadFile));
    }
    else
    {
      this.CloseCurrentViewer();
      this.UpdateButtons();
      if (this._selectedFileItem == null || this._selectedFileItem.ValueIndex == -1)
        return;
      this.LoadViewData();
    }
  }

  /// <summary>Открытие выбранного файла</summary>
  private void LoadViewData()
  {
    this.StopWatcher();
    this.ExtractFiles(this._selectedFileItem);
    if (File.Exists(this._selectedFileItem.FileFullName))
      this.StartWatcher(this._selectedFileItem.FileFullName);
    this.Open(this._selectedFileItem);
  }

  /// <summary>
  /// Восстановление фокуса на родительском элементе
  /// Восстанавливать фокус нужно с задержкой, т.к. если в качестве просмотрщика
  /// используется внешний процесс, то после запуска внутри процесса
  /// идет непосредственно активация com контрола, который забирает на себя фокус.
  /// См. метод - AxHost.AxContainer.OnUIActivate(AxHost site) в нем f.ActiveControl = site забирает на себя фокус.
  /// Чтобы обработать данную ситуацию фокус нужно восстанавливать с задержкой.
  /// Как показала практика 0,5 с для этого достаточно. При необходимости можно увеличить
  /// </summary>
  private void RestoreParentFocus()
  {
    if (!this._needRestoreFocus)
      return;
    this._needRestoreFocus = false;
    Timer timer = new Timer();
    timer.Interval = 500;
    timer.Tick += (EventHandler) ((s, e) =>
    {
      ((Timer) s).Stop();
      this.OnParentFocus();
    });
    timer.Start();
  }

  /// <summary>
  /// Извлекает файлы в область просмотра и прописывает полные пути к файлам в _fileItems
  /// </summary>
  private void ExtractFiles(FileItem selectedFileItem)
  {
    FileItem[] array = this._fileItems.Where<FileItem>((System.Func<FileItem, bool>) (x => x.ObjectId == selectedFileItem.ObjectId && x.IsFile)).ToArray<FileItem>();
    IEnumerable<string> source = ((IEnumerable<FileItem>) array).Select<FileItem, string>((System.Func<FileItem, string>) (x => x.FileName));
    if (selectedFileItem.IsImViewerFile || source.All<string>(new System.Func<string, bool>(this.IsIpsDocument)))
      return;
    List<PublishedFile> objectFiles = ClientContext.FileVault.ViewArea.Publish((IList<DBObjectState>) ClientContext.FileVault.DBObjectsInfo.CreateStateListForObjectTree(selectedFileItem.ObjectId, VersionsRuleSources.GetCurrentWindowRule())).ObjectFiles;
    foreach (FileItem fileItem1 in array)
    {
      FileItem fileItem = fileItem1;
      PublishedFile publishedFile = objectFiles != null ? objectFiles.FirstOrDefault<PublishedFile>((System.Func<PublishedFile, bool>) (x => x.BlobId == fileItem.BlobID)) : (PublishedFile) null;
      if (publishedFile != null)
        fileItem.FileFullName = publishedFile.FullName;
    }
    if (ApplicationServices.Container.GetService<IExtensionsService>().WriteSignsAndParams)
    {
      InjectStandaloneViewDataTask standaloneViewDataTask = ApplicationServices.Container.GetService<DocumentFilesTaskFactory>().InjectStandaloneViewData();
      standaloneViewDataTask.Initialize(selectedFileItem.ObjectId, selectedFileItem.FileName, selectedFileItem.FileFullName);
      if (standaloneViewDataTask.CanPerform)
        standaloneViewDataTask.Perform();
    }
    IClientRedliningService crs;
    if ((crs = ServicesManager.GetService(typeof (IClientRedliningService)) as IClientRedliningService) == null)
      return;
    List<Tuple<long, string>> list = ((IEnumerable<FileItem>) array).Where<FileItem>((System.Func<FileItem, bool>) (x => File.Exists(x.FileFullName))).Select<FileItem, Tuple<long, string>>((System.Func<FileItem, Tuple<long, string>>) (x => new Tuple<long, string>(x.ObjectId, x.FileFullName))).ToList<Tuple<long, string>>();
    if (!list.Any<Tuple<long, string>>())
      return;
    list.ForEach((Action<Tuple<long, string>>) (item => crs.AddObject(item.Item1, item.Item2)));
  }

  /// <summary>Обновление состояния кнопок зума</summary>
  private void UpdateButtons_Zoom()
  {
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.toolBar.Items)
    {
      switch (toolbarItemBase.CommandName)
      {
        case "ZoomAll":
        case "ZoomIn":
        case "ZoomOut":
        case "Zoom1to1":
          toolbarItemBase.Enabled = this.CurrentViewer is IZoomSupport;
          continue;
        case "ZoomPrevious":
          toolbarItemBase.Enabled = this.CurrentViewer is IZoomSupport currentViewer && currentViewer.PreviousViewEnabled();
          continue;
        default:
          continue;
      }
    }
  }

  /// <summary>Обновление состояния кнопки изменить расстояние</summary>
  private void UpdateButtons_Distance()
  {
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.toolBar.Items)
    {
      if (!(toolbarItemBase.CommandName != "RedDistance"))
      {
        toolbarItemBase.Visible = this.CurrentViewer is IDistanceMeasureSupport;
        toolbarItemBase.Enabled = this.CurrentViewer is IDistanceMeasureSupport currentViewer1 && currentViewer1.RedDistanceMeasureEnabled();
        ((ButtonItemBase) toolbarItemBase).Checked = toolbarItemBase.Enabled && this.CurrentViewer is IDistanceMeasureSupport currentViewer2 && currentViewer2.RedDistanceMeasureChecked();
      }
    }
  }

  /// <summary>Изменение иконки изменить цвет dwg</summary>
  /// <param name="isBlack"></param>
  private void Change_ColorDWG_Icon(bool isBlack)
  {
    if (this.btColorDWG.Image == null)
      this.btColorDWG.Image = (Image) new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    using (Graphics graphics = Graphics.FromImage(this.btColorDWG.Image))
    {
      graphics.Clear(Color.Empty);
      if (isBlack)
      {
        graphics.DrawRectangle(Pens.Black, 0, 0, 16 /*0x10*/, 15);
        graphics.FillPolygon(Brushes.Black, new Point[3]
        {
          new Point(0, 0),
          new Point(16 /*0x10*/, 16 /*0x10*/),
          new Point(16 /*0x10*/, 0)
        });
      }
      else
      {
        graphics.FillRectangle(Brushes.Red, 0, 0, 3, 16 /*0x10*/);
        graphics.FillRectangle(Brushes.Yellow, 4, 0, 3, 16 /*0x10*/);
        graphics.FillRectangle(Brushes.Green, 8, 0, 3, 16 /*0x10*/);
        graphics.FillRectangle(Brushes.Blue, 12, 0, 3, 16 /*0x10*/);
      }
    }
  }

  /// <summary>Изменение состояния кнопки изменить цвет dwg</summary>
  private void UpdateButtons_ColorDWG()
  {
    if (this.ViewEnabled && this.CurrentViewer is IColorDwgSupport currentViewer && currentViewer.IsShowDwg())
    {
      if (this.btColorDWG.Visible && currentViewer.ColorNotChanged())
        return;
      this.Change_ColorDWG_Icon(currentViewer.IsAllColorsToBlack());
      this.btColorDWG.Visible = this.btColorDWG.Enabled = true;
    }
    else
      this.btColorDWG.Visible = this.btColorDWG.Enabled = false;
  }

  /// <summary>Обновление состояния кнопок редактора замечаний</summary>
  private void UpdateButtons_RedView()
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    if (this.CurrentViewer is IRedlinerSupport currentViewer)
    {
      flag3 = currentViewer.RedLineEnabled();
      flag1 = currentViewer.HasLayers();
      flag2 = currentViewer.GetViewNotesVisible();
    }
    this.btRedView.Visible = this.btRedView.Enabled = flag3;
    this.btRedInfo.Visible = this.btRedInfo.Enabled = false;
    if (!flag3)
    {
      this.btRedView.Checked = this.btRedInfo.Checked = false;
    }
    else
    {
      this.btRedView.Checked = flag1;
      if (flag2)
      {
        this.btRedView.Checked = true;
        this.btRedView.ImageIndex = this.NamedImageList.ImageIndex("imgFullScreen");
        this.btRedView.ToolTipText = LocalizationHolder.rm.GetString("CompleteEditingOfNotes");
      }
      else
      {
        this.btRedView.ImageIndex = this.NamedImageList.ImageIndex("imgRedEdit");
        this.btRedView.ToolTipText = LocalizationHolder.rm.GetString("EditNotes");
      }
      this.btRedInfo.Checked = flag1;
      this.btRedInfo.ToolTipText = this.btRedInfo.Checked ? LocalizationHolder.rm.GetString("ViewNotes") : LocalizationHolder.rm.GetString("NoNotes");
    }
  }

  /// <summary>Текущая страница</summary>
  /// <returns> -1 нет страницы, 0-в середине списка, 1-в начале списка, 2-в конце списка</returns>
  private int IsPage()
  {
    ComboBox comboBox = this.cbPages.ComboBox;
    if (comboBox.SelectedItem == null)
      return -1;
    int num = comboBox.Items.IndexOf(comboBox.SelectedItem);
    if (num == -1)
      return -1;
    int count = comboBox.Items.Count;
    if (count == 0)
      return -1;
    bool flag1 = num == 0;
    bool flag2 = num == count - 1;
    return (flag1 ? 1 : 0) | (flag2 ? 2 : 0);
  }

  /// <summary>Найти индекс предыдущего файла</summary>
  /// <returns>индекс предыдущего файла,иначе -1</returns>
  private int PrevIndexfilesCombo()
  {
    ComboBox comboBox = this.cbFiles.ComboBox;
    if (comboBox == null || comboBox.SelectedItem == null)
      return -1;
    int curentIndex = comboBox.Items.IndexOf(comboBox.SelectedItem);
    return curentIndex < 0 ? -1 : ((IEnumerable<int>) comboBox.Items.Cast<FileItem>().Select((value, index) => new
    {
      value = value,
      index = index
    }).Where(fi => fi.value != null && fi.value.IsFile).Select(x => x.index).ToArray<int>()).Where<int>((System.Func<int, bool>) (index => index < curentIndex)).DefaultIfEmpty<int>(-1).Max();
  }

  /// <summary>Найти индекс следующего файла</summary>
  /// <returns>индекс следующего файла,иначе -1</returns>
  private int NextIndexfilesCombo()
  {
    ComboBox comboBox = this.cbFiles.ComboBox;
    if (comboBox == null || comboBox.SelectedItem == null)
      return -1;
    int curentIndex = comboBox.Items.IndexOf(comboBox.SelectedItem);
    return curentIndex < 0 ? -1 : ((IEnumerable<int>) comboBox.Items.Cast<FileItem>().Select((value, index) => new
    {
      value = value,
      index = index
    }).Where(fi => fi.value != null && fi.value.IsFile).Select(x => x.index).ToArray<int>()).Where<int>((System.Func<int, bool>) (index => index > curentIndex)).DefaultIfEmpty<int>(-1).Min();
  }

  /// <summary>Обновление контролов для страниц</summary>
  private void UpdatePageControls()
  {
    if (this.CurrentViewer is IPagerSupport currentViewer)
    {
      object objB = currentViewer.CurrentPage();
      if (!object.Equals(this.cbPages.ComboBox.SelectedItem, objB))
      {
        try
        {
          this._ignoreChanges = true;
          this.cbPages.ComboBox.SelectedItem = objB;
          this.cbPages.ToolTipText = this.cbPages.ComboBox.SelectedItem != null ? this.cbPages.ComboBox.SelectedItem.ToString() : "";
        }
        finally
        {
          this._ignoreChanges = false;
        }
      }
    }
    this.UpdateButtons_Pages();
  }

  /// <summary>Обновить видимость кнопок работы со страницами</summary>
  private void UpdateButtons_Pages()
  {
    ComboBox comboBox1 = this.cbFiles.ComboBox;
    int count1 = comboBox1 != null ? comboBox1.Items.Count : 0;
    ComboBox comboBox2 = this.cbPages.ComboBox;
    int count2 = comboBox2 != null ? comboBox2.Items.Count : 0;
    int num1 = count2 == 0 || comboBox2 == null || comboBox2.SelectedItem == null ? 0 : comboBox2.Items.IndexOf(comboBox2.SelectedItem);
    bool flag1 = count2 != 0;
    bool flag2 = num1 == 0;
    bool flag3 = num1 == count2 - 1;
    int num2 = !flag1 ? 0 : (!flag2 ? 1 : 0);
    bool flag4 = flag1 && !flag3;
    bool flag5 = count1 != 0 | flag1;
    this.btFirstPage.BeginGroup = flag5;
    this.btFirstPage.Visible = flag5;
    this.btFirstPage.Enabled = flag1 && !flag2;
    this.btPrevPage.Visible = flag5;
    if (num2 != 0)
    {
      this.btPrevPage.Enabled = true;
      this.btPrevPage.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1242");
    }
    else
    {
      this.btPrevPage.Enabled = this.PrevIndexfilesCombo() != -1;
      this.btPrevPage.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1244");
    }
    this.cbPages.Visible = flag5;
    this.cbPages.Enabled = count2 > 1;
    this.btNextPage.Visible = flag5;
    if (flag4)
    {
      this.btNextPage.Enabled = true;
      this.btNextPage.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1243");
    }
    else
    {
      this.btNextPage.Enabled = this.NextIndexfilesCombo() != -1;
      this.btNextPage.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1245");
    }
    this.btLastPage.Visible = flag5;
    this.btLastPage.Enabled = flag1 && !flag3;
  }

  /// <summary>найти максимальную длинну для набора Items</summary>
  /// <typeparam name="T">тип преобразования</typeparam>
  /// <param name="myCombo">ComboBox</param>
  /// <param name="description">функция o =&gt; o.ToString()</param>
  /// <returns>максимальная длинна для набора Items</returns>
  private int AutoDropDownWidth<T>(ComboBox myCombo, System.Func<T, string> description)
  {
    int verticalScrollBarWidth = myCombo.Items.Count > myCombo.MaxDropDownItems ? SystemInformation.VerticalScrollBarWidth : 0;
    return myCombo.Items.OfType<T>().Select<T, int>((System.Func<T, int>) (x => TextRenderer.MeasureText(description(x), myCombo.Font).Width)).DefaultIfEmpty<int>(0).Max() + verticalScrollBarWidth;
  }

  /// <summary>найти длинну для выбранного элемента</summary>
  /// <typeparam name="T">тип преобразования</typeparam>
  /// <param name="myCombo">ComboBox</param>
  /// <param name="description">функция o =&gt; o.ToString()</param>
  /// <returns> длинна для SelectedItem</returns>
  private int AutoSelectedItemWidth<T>(ComboBox myCombo, System.Func<T, string> description)
  {
    return new object[1]{ myCombo.SelectedItem }.OfType<T>().Select<T, int>((System.Func<T, int>) (x => TextRenderer.MeasureText(description(x), myCombo.Font).Width)).DefaultIfEmpty<int>(0).Max();
  }

  /// <summary>Изменение ширины контролов</summary>
  private void ChangMinimumControlWidth()
  {
    bool flag1 = this.ViewEnabled && this.CurrentViewer is IColorDwgSupport currentViewer && currentViewer.IsShowDwg();
    bool flag2 = this.ViewEnabled && this.btRedView.Visible;
    bool flag3 = this.ViewEnabled && this.btRedInfo.Visible;
    bool viewEnabled = this.ViewEnabled;
    int num1 = this.cbFiles.ToolBar.Size.Width - (361 + (flag1 ? 16 /*0x10*/ : 0) + (flag2 ? 16 /*0x10*/ : 0) + (flag3 ? 16 /*0x10*/ : 0) + (viewEnabled ? 16 /*0x10*/ : 0));
    this._cbFilesMinimumControlWidth = this._cbPagesMinimumControlWidth = num1 / 2;
    int num2 = this.AutoSelectedItemWidth<object>(this.cbFiles.ComboBox, (System.Func<object, string>) (o => o.ToString()));
    int num3 = this.AutoSelectedItemWidth<object>(this.cbPages.ComboBox, (System.Func<object, string>) (o => o.ToString()));
    int num4 = (int) ((double) (num2 + 3 + 16 /*0x10*/ + 16 /*0x10*/ + 20) * (double) this.FactorDpiX);
    int num5 = (int) ((double) (num3 + 3 + 16 /*0x10*/) * (double) this.FactorDpiX);
    if (num4 > this._cbFilesMinimumControlWidth || num5 > this._cbPagesMinimumControlWidth)
    {
      int num6 = (num1 - num4 - num5) / 2;
      this._cbFilesMinimumControlWidth = num4 + num6;
      this._cbPagesMinimumControlWidth = num5 + num6;
    }
    if (this.cbFiles.MinimumControlWidth != this._cbFilesMinimumControlWidth)
      this.cbFiles.MinimumControlWidth = this._cbFilesMinimumControlWidth;
    if (this.cbPages.MinimumControlWidth == this._cbPagesMinimumControlWidth)
      return;
    this.cbPages.MinimumControlWidth = this._cbPagesMinimumControlWidth;
  }

  /// <summary>Обновление состояния кнопок/команд</summary>
  private void UpdateButtons()
  {
    if (this.toolBar != null)
    {
      this.toolBar.BeginUpdate();
      this.UpdateCombobox_Files();
      this.UpdateButtons_Pages();
      this.UpdateButtons_Zoom();
      this.UpdateButtons_Distance();
      this.UpdateButtons_ColorDWG();
      this.UpdateButtons_RedView();
      this.UpdateButtons_Overview();
      this.toolBar.EndUpdate();
    }
    if (!(this.CurrentViewer is IOverviewSupport currentViewer))
      return;
    currentViewer.SetOverView();
  }

  /// <summary>Обновление состояния комбобокса файлы</summary>
  private void UpdateCombobox_Files()
  {
    this.cbFiles.Enabled = this._fileItems != null && this._fileItems.Count > 0;
  }

  /// <summary>Обновление состояник кнопки обзор</summary>
  private void UpdateButtons_Overview()
  {
    if (this.ViewEnabled && this.CurrentViewer is IOverviewSupport)
      this.btOverview.Visible = this.btOverview.Enabled = true;
    else
      this.btOverview.Visible = this.btOverview.Enabled = false;
  }

  /// <summary>НАжаатие кнопки</summary>
  /// <param name="msg"></param>
  /// <param name="keyData"></param>
  /// <returns></returns>
  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    bool flag = false;
    if (this.CurrentViewer is IPagerSupport currentViewer)
    {
      flag = true;
      switch (keyData)
      {
        case Keys.Prior:
          int num1 = this.IsPage();
          if (num1 != -1 && (num1 & 1) == 0)
          {
            currentViewer.PrevPage();
            break;
          }
          int num2 = this.PrevIndexfilesCombo();
          if (num2 != -1)
          {
            this.cbFiles.ComboBox.SelectedIndex = num2;
            break;
          }
          break;
        case Keys.Next:
          int num3 = this.IsPage();
          if (num3 != -1 && (num3 & 2) == 0)
          {
            currentViewer.NextPage();
            break;
          }
          int num4 = this.NextIndexfilesCombo();
          if (num4 != -1)
          {
            this.cbFiles.ComboBox.SelectedIndex = num4;
            break;
          }
          break;
        case Keys.End:
          currentViewer.LastPage();
          break;
        case Keys.Home:
          currentViewer.FirstPage();
          break;
        default:
          flag = false;
          break;
      }
    }
    if (!flag && this.ViewEnabled && this.CurrentViewer is IZoomSupport)
    {
      ButtonItem buttonItem = (ButtonItem) null;
      switch (keyData)
      {
        case Keys.Multiply:
          buttonItem = this.btZoom1to1;
          break;
        case Keys.Add:
          buttonItem = this.btZoomIn;
          break;
        case Keys.Subtract:
          buttonItem = this.btZoomOut;
          break;
        case Keys.Divide:
          buttonItem = this.btZoomAll;
          break;
      }
      if (buttonItem != null && buttonItem.Enabled)
      {
        flag = true;
        buttonItem.PerformClick();
      }
    }
    return flag || base.ProcessCmdKey(ref msg, keyData);
  }

  /// <summary>Обработчик события срабатывания таймера</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _timer_Tick(object sender, EventArgs e)
  {
    this._timer.Stop();
    this.LoadData();
    if (this.CurrentViewer != null || this.cbFiles.ComboBox.SelectedIndex >= this.cbFiles.ComboBox.Items.Count - 1)
      return;
    ++this.cbFiles.ComboBox.SelectedIndex;
  }

  /// <summary>Обработчик события наблюдателя за файлом</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Watcher_Changed(object sender, FileSystemEventArgs e) => this.LoadFile();

  /// <summary>
  /// Обработчик события "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ToolbarRendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = sender is BarManager barManager ? barManager.Renderer : (IToolBarRenderer) null;
    if (renderer == null)
      return;
    this.menuBar.Renderer = this.toolBar.Renderer = renderer;
  }

  /// <summary>Изменился выбранный элемент в комбобоксе</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FilesCombo_SelectedIndexChanged(object sender, EventArgs e)
  {
    FileItem selectedItem = this.cbFiles.ComboBox.SelectedItem as FileItem;
    this.ChangMinimumControlWidth();
    if (selectedItem == null)
      return;
    if (selectedItem.IsFile)
    {
      this.SetValueIndex(selectedItem);
    }
    else
    {
      if (this.cbFiles.ComboBox.SelectedIndex >= this.cbFiles.ComboBox.Items.Count - 1)
        return;
      ++this.cbFiles.ComboBox.SelectedIndex;
    }
  }

  /// <summary>Отрисовка элементов комбобокса файл</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FilesListCombo_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    if (e.Index == -1 || !(this.cbFiles.ComboBox.Items[e.Index] is FileItem fileItem))
      return;
    Rectangle targetRect = new Rectangle(e.Bounds.Left, e.Bounds.Top, 16 /*0x10*/, 16 /*0x10*/);
    if (fileItem.IsFile)
      targetRect.X += 16 /*0x10*/;
    int num = 16 /*0x10*/;
    if (fileItem.Icon != null)
      e.Graphics.DrawIcon(fileItem.Icon, targetRect);
    else if (fileItem.ImageIndex != -1)
    {
      this.CategoryTypeIconService.ImageList.Draw(e.Graphics, targetRect.X, targetRect.Y, fileItem.ImageIndex);
      num = this.CategoryTypeIconService.ImageList.ImageSize.Width;
    }
    Brush brush = SystemBrushes.ControlText;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
    {
      Brush highlightText = SystemBrushes.HighlightText;
      e.Graphics.DrawString(fileItem.Caption, this.cbFiles.ComboBox.Font, highlightText, (float) (targetRect.Left + 2 + num), (float) (targetRect.Top + 2));
    }
    else
    {
      if (fileItem.ColorText != Color.Empty)
        brush = (Brush) new SolidBrush(fileItem.ColorText);
      e.Graphics.DrawString(fileItem.Caption, this.cbFiles.ComboBox.Font, brush, (float) (targetRect.Left + 2 + num), (float) (targetRect.Top + 2));
      if (!(fileItem.ColorText != Color.Empty))
        return;
      brush.Dispose();
    }
  }

  /// <summary>Объект удален</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnObjectRemoved(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || !(objectsEventArgs.EventName == "ObjectsRemoved") || !objectsEventArgs.ObjectIDs.Contains(this._objectId))
      return;
    this._objectId = 0L;
    this.ForceReloadData();
  }

  /// <summary>Объект взят на изменение</summary>
  /// <param name="sender"></param>
  /// <param name="ne"></param>
  private void OnObjectCheckedInOut(object sender, NotificationEventArgs ne)
  {
    if (!(ne is DBObjectsEventArgs objectsEventArgs))
      return;
    switch (objectsEventArgs.EventName)
    {
      case "ObjectsCheckedIn":
        if (!objectsEventArgs.ObjectIDs.Contains(this._objectId) || this._objectId >= 0L)
          break;
        this._objectId = -this._objectId;
        this.ForceReloadData();
        break;
      case "ObjectsCheckedOut":
        if (!objectsEventArgs.ObjectIDs.Contains(this._objectId) || this._objectId <= 0L)
          break;
        this._objectId = -this._objectId;
        this.ForceReloadData();
        break;
    }
  }

  /// <summary>Объект изменен</summary>
  /// <param name="sender"></param>
  /// <param name="ne"></param>
  private void OnObjectChanged(object sender, NotificationEventArgs ne)
  {
    if (!(ne is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.EventName != "ObjectsChanged" || !objectsEventArgs.ObjectIDs.Contains(this._objectId))
      return;
    this._dataLoaded = false;
  }

  /// <summary>Изменение размера тулбара</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void toolBar_Resize(object sender, EventArgs e) => this.ChangMinimumControlWidth();

  /// <summary>Изменение выбранного элемента PageCombobox</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Pager_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._ignoreChanges || !(sender is ComboBox comboBox) || !(this.CurrentViewer is IPagerSupport currentViewer))
      return;
    currentViewer.SetCurrentPage(comboBox.SelectedItem);
    this.cbPages.ToolTipText = this.cbPages.ComboBox.SelectedItem != null ? this.cbPages.ComboBox.SelectedItem.ToString() : "";
    this.ChangMinimumControlWidth();
  }

  /// <summary>Редактирование замечаний</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btRedView_Click(object sender, EventArgs e)
  {
    if (this.viewerHost == null || !(this.CurrentViewer is IRedlinerSupport currentViewer))
      return;
    bool viewNotesVisible = currentViewer.GetViewNotesVisible();
    currentViewer.SetViewNotesVisible(!viewNotesVisible);
    currentViewer.SetRedLineEdit(true);
    this.UpdateButtons_RedView();
  }

  /// <summary>Просмотр замечаний</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btRedInfo_Click(object sender, EventArgs e)
  {
    if (!(this.CurrentViewer is IRedlinerSupport currentViewer))
      return;
    currentViewer.SetViewNotesVisible(true);
    currentViewer.SetRedLineEdit(false);
    this.UpdateButtons_RedView();
  }

  /// <summary>Обработка кнопок зум</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ZoomButtons_Click(object sender, EventArgs e)
  {
    if (!(sender is ButtonItemBase buttonItemBase))
      return;
    switch (buttonItemBase.CommandName)
    {
      case "ZoomAll":
        if (!(this.CurrentViewer is IZoomSupport currentViewer1))
          break;
        currentViewer1.ZoomToFit();
        break;
      case "ZoomIn":
        if (!(this.CurrentViewer is IZoomSupport currentViewer2))
          break;
        currentViewer2.ZoomIn();
        break;
      case "ZoomOut":
        if (!(this.CurrentViewer is IZoomSupport currentViewer3))
          break;
        currentViewer3.ZoomOut();
        break;
      case "Zoom1to1":
        if (!(this.CurrentViewer is IZoomSupport currentViewer4))
          break;
        currentViewer4.Zoom1to1();
        break;
      case "ZoomPrevious":
        if (!(this.CurrentViewer is IZoomSupport currentViewer5))
          break;
        currentViewer5.ZoomPrevious();
        break;
      case "RedDistance":
        if (!(this.CurrentViewer is IDistanceMeasureSupport currentViewer6))
          break;
        currentViewer6.RedDistanceMeasure();
        break;
    }
  }

  /// <summary>Обработка кнопки Обзор</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Overview_Click(object sender, EventArgs e)
  {
    if (!(sender is ButtonItem) || !(this.CurrentViewer is IOverviewSupport currentViewer))
      return;
    currentViewer.StartOverView();
    this.UpdateButtons();
  }

  /// <summary>Изменение зума</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ZoomSupport_ZoomChanging(object sender, EventArgs e) => this.UpdateButtons_Zoom();

  /// <summary>Обработка добавления страниц (после разбивки)</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void PagerSupport_PagesAdded(object sender, PagerAddPagesEventArgs e)
  {
    if (e.Pages == null || e.Pages.Length == 0)
      return;
    this.AppendPagesComboBox(e.Pages);
  }

  /// <summary>Обработка изменения страницы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void PagerSupport_PageChanged(object sender, EventArgs e) => this.UpdatePageControls();

  /// <summary>Изменение цвета dwg</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ColorDwgSupport_DwgColorChanged(object sender, DwgColorChangedEventArgs e)
  {
    this.Change_ColorDWG_Icon(e.Black);
  }

  /// <summary>Нажатие кнопки цвет DWG</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btColorDWG_Click(object sender, EventArgs e)
  {
    if (!this.ViewEnabled || !(this.CurrentViewer is IColorDwgSupport currentViewer) || !currentViewer.IsShowDwg())
      return;
    currentViewer.SwitchColorToBlack();
  }

  /// <summary>Выпадение комбобокса страница - изменение ширины</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void pagesCombo_DropDown(object sender, EventArgs e)
  {
    ComboBox myCombo = (ComboBox) sender;
    int num = this.AutoDropDownWidth<object>(myCombo, (System.Func<object, string>) (o => o.ToString()));
    myCombo.DropDownWidth = (int) ((double) (num + 3 + 16 /*0x10*/) * (double) this.FactorDpiX);
  }

  /// <summary>ВЫпадение комбобокса файл - изменение ширины</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void filesCombo_DropDown(object sender, EventArgs e)
  {
    ComboBox myCombo = (ComboBox) sender;
    int num = this.AutoDropDownWidth<object>(myCombo, (System.Func<object, string>) (o => o.ToString()));
    myCombo.DropDownWidth = (int) ((double) (num + 3 + 16 /*0x10*/ + 16 /*0x10*/ + 20) * (double) this.FactorDpiX);
  }

  /// <summary>Обработка кнопок управления страницами</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void PageButton_Click(object sender, EventArgs e)
  {
    if (!(sender is ButtonItem buttonItem))
      return;
    if (this.CurrentViewer is IPagerSupport currentViewer)
    {
      switch (buttonItem.CommandName)
      {
        case "FirstPage":
          currentViewer.FirstPage();
          break;
        case "PrevPage":
          int num1 = this.IsPage();
          if (num1 != -1 && (num1 & 1) == 0)
          {
            currentViewer.PrevPage();
            break;
          }
          int num2 = this.PrevIndexfilesCombo();
          if (num2 == -1)
            break;
          this.cbFiles.ComboBox.SelectedIndex = num2;
          break;
        case "NextPage":
          int num3 = this.IsPage();
          if (num3 != -1 && (num3 & 2) == 0)
          {
            currentViewer.NextPage();
            break;
          }
          int num4 = this.NextIndexfilesCombo();
          if (num4 == -1)
            break;
          this.cbFiles.ComboBox.SelectedIndex = num4;
          break;
        case "LastPage":
          currentViewer.LastPage();
          break;
      }
    }
    else
    {
      switch (buttonItem.CommandName)
      {
        case "PrevPage":
          int num5 = this.PrevIndexfilesCombo();
          if (num5 == -1)
            break;
          this.cbFiles.ComboBox.SelectedIndex = num5;
          break;
        case "NextPage":
          int num6 = this.NextIndexfilesCombo();
          if (num6 == -1)
            break;
          this.cbFiles.ComboBox.SelectedIndex = num6;
          break;
      }
    }
  }

  /// <summary>Покидаем контрол</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void VisualizerView_Leave(object sender, EventArgs e)
  {
    if (this.CurrentViewer == null)
      return;
    this.UpdateButtons();
  }

  /// <summary>Изменение видимости контрола</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void VisualizerView_VisibleChanged(object sender, EventArgs e)
  {
    if (this.Disposing || !this.IsVisible)
      return;
    this.viewerHost.Visible = true;
    this.UpdateButtons();
    this.CheckLoadData();
  }

  /// <summary>
  /// Установка видимости команд перед вызовом контекстного меню
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void zoomContextMenu_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    this.mnRedNoteProperties.Visible = false;
    if (this.CurrentViewer is IZoomSupport currentViewer1)
      this.mnZoomPrevious.Enabled = currentViewer1.PreviousViewEnabled();
    if (!(this.CurrentViewer is IRedlinerSupport currentViewer2))
      return;
    this.mnRedNoteProperties.Visible = currentViewer2.GetRedNotePropertiesVisible();
  }

  /// <summary>Свойства пометки в точке</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mnRedNoteProperties_Click(object sender, EventArgs e)
  {
    if (!(this.CurrentViewer is IRedlinerSupport currentViewer))
      return;
    currentViewer.ShowNoteProperties();
    this.UpdateButtons();
  }

  /// <summary>Запуск внешнего редактора замечаний</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btExternalRedliningEditor_Click(object sender, EventArgs e)
  {
    IExternalRedliningEditorService service = ServiceUtils.GetService<IExternalRedliningEditorService>((object) ServicesManager.ServiceContainer, false);
    if (service == null)
      return;
    service.ReportFileOpenAction(this._objectId, false);
    if (service.LaunchScreenShooter())
      return;
    int num = (int) MessageBox.Show("Не удалось запустить внешний редактор замечаний 'ИНТЕРМЕХ'. Возможно, приложение не установлено.", "IPS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    if (disposing)
    {
      this.UnsubcribeTimerEvents();
      this.UnsubscribeClientServices();
      this.UnsubscribeFilesComboBoxEvents();
      this.DisposeWatcher();
      this.ClearViewers();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.toolBar = new Intermech.Bars.ToolBar();
    this.cbFiles = new ComboBoxItem();
    this.btFirstPage = new ButtonItem();
    this.btPrevPage = new ButtonItem();
    this.cbPages = new ComboBoxItem();
    this.btNextPage = new ButtonItem();
    this.btLastPage = new ButtonItem();
    this.btRedView = new ButtonItem();
    this.btRedInfo = new ButtonItem();
    this.btColorDWG = new ButtonItem();
    this.btEmpty = new ButtonItem();
    this.btOverview = new ButtonItem();
    this.btZoomPrevious = new ButtonItem();
    this.btZoomIn = new ButtonItem();
    this.btZoomOut = new ButtonItem();
    this.btZoom1to1 = new ButtonItem();
    this.btZoomAll = new ButtonItem();
    this.btDistance = new ButtonItem();
    this.btExternalRedliningEditor = new ButtonItem();
    this.menuBar = new MenuBar();
    this.zoomContextMenu = new ContextMenuBarItem();
    this.mnZoomPrevious = new MenuButtonItem();
    this.mnZoomIn = new MenuButtonItem();
    this.mnZoomOut = new MenuButtonItem();
    this.mnZoom1to1 = new MenuButtonItem();
    this.mnZoomAll = new MenuButtonItem();
    this.mnRedNoteProperties = new MenuButtonItem();
    this.viewerHost = new ViewerHost();
    this.viewerHost.SuspendLayout();
    this.SuspendLayout();
    this.toolBar.FullMenus = true;
    this.toolBar.Guid = new Guid("e2d3176f-294b-40fb-b837-b81edc269027");
    this.toolBar.Hidden = false;
    this.toolBar.Items.AddRange(new ToolbarItemBase[18]
    {
      (ToolbarItemBase) this.cbFiles,
      (ToolbarItemBase) this.btFirstPage,
      (ToolbarItemBase) this.btPrevPage,
      (ToolbarItemBase) this.cbPages,
      (ToolbarItemBase) this.btNextPage,
      (ToolbarItemBase) this.btLastPage,
      (ToolbarItemBase) this.btRedView,
      (ToolbarItemBase) this.btRedInfo,
      (ToolbarItemBase) this.btColorDWG,
      (ToolbarItemBase) this.btEmpty,
      (ToolbarItemBase) this.btOverview,
      (ToolbarItemBase) this.btZoomPrevious,
      (ToolbarItemBase) this.btZoomIn,
      (ToolbarItemBase) this.btZoomOut,
      (ToolbarItemBase) this.btZoom1to1,
      (ToolbarItemBase) this.btZoomAll,
      (ToolbarItemBase) this.btDistance,
      (ToolbarItemBase) this.btExternalRedliningEditor
    });
    this.toolBar.Location = new Point(0, 0);
    this.toolBar.Name = "toolBar";
    this.toolBar.Size = new Size(901, 24);
    this.toolBar.TabIndex = 0;
    this.toolBar.Text = "";
    this.toolBar.Resize += new EventHandler(this.toolBar_Resize);
    this.cbFiles.CommandName = "Files";
    this.cbFiles.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbFiles.MinimumControlWidth = 250;
    this.cbFiles.Padding.Bottom = 0;
    this.cbFiles.Padding.Left = 1;
    this.cbFiles.Padding.Right = 1;
    this.cbFiles.Padding.Top = 0;
    this.btFirstPage.BeginGroup = true;
    this.btFirstPage.CommandName = "FirstPage";
    this.btFirstPage.Visible = false;
    this.btFirstPage.Click += new EventHandler(this.PageButton_Click);
    this.btPrevPage.CommandName = "PrevPage";
    this.btPrevPage.Visible = false;
    this.btPrevPage.Click += new EventHandler(this.PageButton_Click);
    this.cbPages.CommandName = "cbPages";
    this.cbPages.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbPages.MinimumControlWidth = 150;
    this.cbPages.Padding.Bottom = 0;
    this.cbPages.Padding.Left = 1;
    this.cbPages.Padding.Right = 1;
    this.cbPages.Padding.Top = 0;
    this.cbPages.Visible = false;
    this.btNextPage.CommandName = "NextPage";
    this.btNextPage.Visible = false;
    this.btNextPage.Click += new EventHandler(this.PageButton_Click);
    this.btLastPage.CommandName = "LastPage";
    this.btLastPage.Visible = false;
    this.btLastPage.Click += new EventHandler(this.PageButton_Click);
    this.btRedView.CommandName = "btColorDWG";
    this.btRedView.Click += new EventHandler(this.btRedView_Click);
    this.btRedInfo.CommandName = "btRedInfo";
    this.btRedInfo.Click += new EventHandler(this.btRedInfo_Click);
    this.btColorDWG.BeginGroup = true;
    this.btColorDWG.CommandName = "btColorDWG";
    this.btColorDWG.Click += new EventHandler(this.btColorDWG_Click);
    this.btEmpty.CommandName = "btEmpty";
    this.btEmpty.Enabled = false;
    this.btEmpty.IconSize = new Size(1, 1);
    this.btEmpty.Importance = ToolBarItemImportance.Lowest;
    this.btEmpty.Padding.Bottom = 0;
    this.btEmpty.Padding.Left = 0;
    this.btEmpty.Padding.Right = 0;
    this.btEmpty.Padding.Top = 0;
    this.btEmpty.Stretch = true;
    this.btOverview.CommandName = "Overview";
    this.btOverview.Importance = ToolBarItemImportance.Lowest;
    this.btOverview.Click += new EventHandler(this.Overview_Click);
    this.btZoomPrevious.BeginGroup = true;
    this.btZoomPrevious.CommandName = "ZoomPrevious";
    this.btZoomPrevious.Click += new EventHandler(this.ZoomButtons_Click);
    this.btZoomIn.CommandName = "ZoomIn";
    this.btZoomIn.Click += new EventHandler(this.ZoomButtons_Click);
    this.btZoomOut.CommandName = "ZoomOut";
    this.btZoomOut.Click += new EventHandler(this.ZoomButtons_Click);
    this.btZoom1to1.CommandName = "Zoom1to1";
    this.btZoom1to1.Importance = ToolBarItemImportance.Low;
    this.btZoom1to1.Click += new EventHandler(this.ZoomButtons_Click);
    this.btZoomAll.CommandName = "ZoomAll";
    this.btZoomAll.Click += new EventHandler(this.ZoomButtons_Click);
    this.btDistance.AutoToggle = AutoToggleType.Radio;
    this.btDistance.BeginGroup = true;
    this.btDistance.CommandName = "RedDistance";
    this.btDistance.Importance = ToolBarItemImportance.High;
    this.btDistance.Click += new EventHandler(this.ZoomButtons_Click);
    this.btExternalRedliningEditor.BeginGroup = true;
    this.btExternalRedliningEditor.CommandName = "btExternalRedliningEditor";
    this.btExternalRedliningEditor.Click += new EventHandler(this.btExternalRedliningEditor_Click);
    this.menuBar.Guid = new Guid("5649158c-2aaa-4a90-bbd6-56df2403c666");
    this.menuBar.Hidden = false;
    this.menuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.zoomContextMenu
    });
    this.menuBar.Location = new Point(0, 0);
    this.menuBar.Name = "menuBar";
    this.menuBar.OwnerForm = (Form) null;
    this.menuBar.Size = new Size(901, 26);
    this.menuBar.TabIndex = 1;
    this.menuBar.Text = "";
    this.menuBar.Visible = false;
    this.zoomContextMenu.CommandName = "ZoomContextMenu";
    this.zoomContextMenu.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnZoomPrevious,
      (ToolbarItemBase) this.mnZoomIn,
      (ToolbarItemBase) this.mnZoomOut,
      (ToolbarItemBase) this.mnZoom1to1,
      (ToolbarItemBase) this.mnZoomAll,
      (ToolbarItemBase) this.mnRedNoteProperties
    });
    this.zoomContextMenu.ShowText = true;
    this.zoomContextMenu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.zoomContextMenu_BeforePopup);
    this.mnZoomPrevious.CommandName = "ZoomPrevious";
    this.mnZoomPrevious.ShowText = true;
    this.mnZoomPrevious.Click += new EventHandler(this.ZoomButtons_Click);
    this.mnZoomIn.CommandName = "ZoomIn";
    this.mnZoomIn.ShowText = true;
    this.mnZoomIn.Click += new EventHandler(this.ZoomButtons_Click);
    this.mnZoomOut.CommandName = "ZoomOut";
    this.mnZoomOut.ShowText = true;
    this.mnZoomOut.Click += new EventHandler(this.ZoomButtons_Click);
    this.mnZoom1to1.CommandName = "Zoom1to1";
    this.mnZoom1to1.ShowText = true;
    this.mnZoom1to1.Click += new EventHandler(this.ZoomButtons_Click);
    this.mnZoomAll.CommandName = "ZoomAll";
    this.mnZoomAll.ShowText = true;
    this.mnZoomAll.Click += new EventHandler(this.ZoomButtons_Click);
    this.mnRedNoteProperties.CommandName = "mnRedNoteProperties";
    this.mnRedNoteProperties.ShowText = true;
    this.mnRedNoteProperties.Visible = false;
    this.mnRedNoteProperties.Click += new EventHandler(this.mnRedNoteProperties_Click);
    this.viewerHost.Controls.Add((Control) this.menuBar);
    this.viewerHost.Dock = DockStyle.Fill;
    this.viewerHost.Location = new Point(0, 24);
    this.viewerHost.Name = "viewerHost";
    this.viewerHost.Size = new Size(901, 468);
    this.viewerHost.TabIndex = 1;
    this.AutoScaleDimensions = new SizeF(96f, 96f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.Controls.Add((Control) this.viewerHost);
    this.Controls.Add((Control) this.toolBar);
    this.Name = nameof (VisualizerView);
    this.Size = new Size(901, 492);
    this.VisibleChanged += new EventHandler(this.VisualizerView_VisibleChanged);
    this.Leave += new EventHandler(this.VisualizerView_Leave);
    this.viewerHost.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public delegate void AppendPagesComboBoxDelegate(object[] pages);

  private sealed class VisualizerViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, true);
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_378"),
        ImageIndex = service.ImageIndex("imgView"),
        OrderID = 40
      };
    }
  }
}
