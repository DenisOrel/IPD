
// Type: Intermech.Navigator.DBObjects.ContextsSearchView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.DBObjects;

/// <summary>Закладка для поиска контекстов и извещений</summary>
public class ContextsSearchView : ChildrenView
{
  /// <summary>Выполняется ли обработка события</summary>
  private bool _inEvent;
  /// <summary>Родительская коллекция</summary>
  private ISelectedItems _items;
  /// <summary>Таймер</summary>
  private System.Windows.Forms.Timer timerRefresh;
  /// <summary>Кэш найденных группирующих объектов</summary>
  internal IGroupingObjectsCache _cache;
  /// <summary>Индекс изображения</summary>
  internal static int _imageGroupingObject = -1;
  /// <summary>
  /// Объект для потокобезопасного доступа к переменным во время работы фонового потока
  /// </summary>
  private object lockView = new object();
  /// <summary>
  /// Уникальный идентификатор задания по поиску группирующих объектов
  /// </summary>
  private Guid jobID;
  /// <summary>Состояние текущей задачи</summary>
  private SearchGroupingObjectJobStatus jobStatus;
  /// <summary>
  /// Фоновый поток, в рамках которого выполняется фоновый поиск группирующих объектов
  /// </summary>
  private Thread thread;
  /// <summary>Имя текущего потока</summary>
  private string threadName = string.Empty;
  /// <summary>
  /// Список идентификаторов версий выделенных объектов (верхний уровень)
  /// </summary>
  private List<long> _topItems = new List<long>();
  /// <summary>Список найденных группирующих объектов</summary>
  private SearchGroupingObjects _groupingObjects = new SearchGroupingObjects();
  private string _selectedAnalyzerName;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ComboBoxItem comboBoxMode;
  protected ButtonItem btnAnalyze;
  private ButtonItem btnStop;
  private ButtonItem btnClear;
  private ButtonItem btnOpenInNewWindow;

  /// <summary>Создать экземпляр класса</summary>
  public ContextsSearchView()
  {
    this.InitializeComponent();
    this.InitViewResources();
  }

  /// <summary>Инициализация ресурсов закладки</summary>
  public void InitViewResources()
  {
    this.timerRefresh = new System.Windows.Forms.Timer();
    this.timerRefresh.Interval = 1000;
    this.timerRefresh.Tick += new EventHandler(this.timerRefresh_Tick);
    this._cache = ServicesManager.GetService(typeof (IGroupingObjectsCache)) as IGroupingObjectsCache;
    ContextsSearchView._imageGroupingObject = ChildrenView._namedImageList == null || ContextsSearchView._imageGroupingObject >= 0 ? ContextsSearchView._imageGroupingObject : ChildrenView._namedImageList.ImageIndex("imgTreeView");
    this.comboBoxMode.ComboBox.SelectedIndexChanged += new EventHandler(this.searchModeSelectedIndexChanged);
    int index = this._filtersComboBoxItem.Index;
    this.comboBoxMode.Index = index;
    int num1 = index + 1;
    this.btnAnalyze.Index = num1;
    int num2 = num1 + 1;
    this.btnStop.Index = num2;
    int num3 = num2 + 1;
    this.btnClear.Index = num3;
    this.btnOpenInNewWindow.Index = num3;
  }

  /// <summary>Освобождение ресурсов закладки</summary>
  public void DisposeViewResources() => this.StopThread();

  /// <summary>Заголовок закладки</summary>
  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_614");

  /// <summary>Индекс изображения</summary>
  public override int ImageIndex => ContextsSearchView._imageGroupingObject;

  /// <summary>
  /// Порядковый номер закладки (прописан в файле Вьюшки.txt)
  /// </summary>
  public override int OrderID => 27;

  /// <summary>
  /// Возвращает тип элементов навигации, которые зачитываются и отображаются в гриде.
  /// </summary>
  public override ContentType ViewContentType
  {
    [DebuggerStepThrough] get => ContentType.NonFolders;
  }

  /// <summary>Инициализировать закладку</summary>
  /// <param name="items">Коллекция выделенных элементов пространства навигации</param>
  /// <param name="provider">Контейнер сервисов</param>
  public override void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (this._inEvent)
      return;
    try
    {
      this._inEvent = true;
      this._items = items;
      this.Initialize((IDescriptor) new VirtualGrouingObjectsDescriptor(Intermech.Navigator.Consts.CategoryGroupingObjectsNode, 0, LocalizationHolder.rm.GetString("Client.Core_614"), (IList) new List<long>()), provider);
    }
    finally
    {
      this._inEvent = false;
    }
  }

  /// <summary>
  /// Активировать закладку (чтение из базы данных, загрузка информации и т.п.)
  /// </summary>
  /// <param name="previousView">Предыдущая закладка</param>
  public override void Activate(IView previousView)
  {
    if (this._inEvent)
      return;
    try
    {
      this._inEvent = true;
      base.Activate(previousView);
      if (this.IsDisposed)
        return;
      this.StopThread();
      if (this._services.GetService(typeof (ContextsSearchView)) is ContextsSearchView)
        this._services.RemoveService(typeof (ContextsSearchView));
      this._services.AddService(typeof (ContextsSearchView), (object) this);
      this.comboBoxMode.Items.Clear();
      this.comboBoxMode.Items.AddRange((object[]) ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISearchGroupingObjectsService)) as ISearchGroupingObjectsService).AnalyzerNames);
      if (!string.IsNullOrEmpty(this._selectedAnalyzerName))
        this.comboBoxMode.ComboBox.SelectedItem = (object) this._selectedAnalyzerName;
      else if (this.comboBoxMode.ComboBox.Items.Count > 0)
        this.comboBoxMode.ComboBox.SelectedIndex = 0;
      this.UpdateControls();
    }
    finally
    {
      this._inEvent = false;
    }
  }

  /// <summary>Деактивировать закладку</summary>
  /// <param name="nextView">Следующая закладка</param>
  public override void Deactivate(IView nextView)
  {
    this.StopThread();
    this._selectedAnalyzerName = this.comboBoxMode.ComboBox != null ? this.comboBoxMode.ComboBox.SelectedItem as string : (string) null;
    this.GridSaveState((Stream) null);
    if (this._services.GetService(typeof (ContextsSearchView)) is ContextsSearchView)
      this._services.RemoveService(typeof (ContextsSearchView));
    base.Deactivate(nextView);
  }

  /// <summary>Прочитаны ли все данные</summary>
  protected override bool Eof
  {
    [DebuggerStepThrough] get => true;
  }

  /// <summary>
  /// Отыскать в списке полученных выделенных элементов подходящие объекты
  /// </summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="provider">Контейнер сервисов</param>
  /// <returns>Список выделенных объектов верхнего уровня</returns>
  public SearchGroupingObjects FindTopItems(ISelectedItems items, System.IServiceProvider provider)
  {
    SearchGroupingObjects topItems = new SearchGroupingObjects();
    if (items == null || items.Count == 0)
      return topItems;
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && MetaDataHelper.HasObjectTypeGrouppedRelTypes(itemData.ObjectType))
        topItems.Add(itemData.ObjectID, itemData.ObjectType, -1L, -1);
    }
    return topItems;
  }

  /// <summary>
  /// Создать корневой дескриптор для получения списка группирующих объектов, упорядоченных по их типам
  /// </summary>
  /// <param name="groupingObjects">Группирующие объекты</param>
  /// <returns></returns>
  private IDescriptor RootDescriptior(SearchGroupingObjects groupingObjects)
  {
    if (groupingObjects == null)
      return (IDescriptor) null;
    Dictionary<int, List<long>> groupingObjectIds = groupingObjects.GetGroupingObjectIDs();
    return (IDescriptor) new DictDescriptor(Intermech.Navigator.Consts.CategoryGroupingObjectsNode, 0, LocalizationHolder.rm.GetString("Client.Core_614"), groupingObjectIds);
  }

  /// <summary>Заполняем закладку найденными данными</summary>
  protected void FillView()
  {
    this.timerRefresh.Enabled = false;
    this._grid.Enabled = true;
    this.UpdateControls();
    if (this._groupingObjects.Count == 0)
      return;
    this.WriteToCache();
    this.Initialize(this.RootDescriptior(this._groupingObjects), this._services.AdvancedProvider);
    this.StateStreamPrefix = "GroupingObjects_";
    this.Activate((IView) null);
    this.UpdateControls();
  }

  /// <summary>Заполняем закладку найденными данными</summary>
  protected bool FillViewFromCache()
  {
    this.timerRefresh.Enabled = false;
    this._grid.Enabled = true;
    this.UpdateControls();
    if (this._items == null || this._items.Count <= 0 || this._cache == null)
      return false;
    this._groupingObjects = this._cache.GetGroupingObjects(this._items.GetItemID(0), this._selectedAnalyzerName);
    if (this._groupingObjects == null)
      this._groupingObjects = new SearchGroupingObjects();
    this.Initialize(this.RootDescriptior(this._groupingObjects), this._services.AdvancedProvider);
    this.StateStreamPrefix = "GroupingObjects_";
    base.Activate((IView) this);
    this.UpdateControls();
    return true;
  }

  /// <summary>Управление контролами на закладке</summary>
  protected override void UpdateControls()
  {
    base.UpdateControls();
    if (this.btnAnalyze == null)
      return;
    lock (this.lockView)
    {
      this.btnAnalyze.Enabled = this.thread == null && this._items != null && this._items.Count > 0;
      this.btnStop.Enabled = this.thread != null;
      this.btnClear.Enabled = this.thread == null && this._groupingObjects.Count > 0;
      this.btnOpenInNewWindow.Enabled = this.thread == null && this._groupingObjects.Count > 0;
    }
  }

  /// <summary>Нажата кнопка "Поиск"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы событий</param>
  private void DoStart(object sender, EventArgs e)
  {
    lock (this.lockView)
    {
      if (this.thread != null || this._items == null)
        return;
      if (this._items.Count == 0)
        return;
    }
    this.StartThread();
  }

  /// <summary>Нажата кнопка "Прервать"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы событий</param>
  private void DoStop(object sender, EventArgs e)
  {
    lock (this.lockView)
    {
      this.StopThread();
      if (this.jobID == Guid.Empty)
        return;
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISearchGroupingObjectsService)) is ISearchGroupingObjectsService customService)
        customService.CancelJob(this.jobID);
      this.jobStatus = (SearchGroupingObjectJobStatus) null;
      this.jobID = Guid.Empty;
    }
  }

  /// <summary>Нажата кнопка "Очистить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы событий</param>
  private void DoClear(object sender, EventArgs e)
  {
    this.StopThread();
    this.ClearInCache();
    this.FillViewFromCache();
  }

  /// <summary>Нажата кнопка "В новом окне"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы событий</param>
  private void DoNewWindow(object sender, EventArgs e)
  {
    if (this._groupingObjects.Count == 0)
      return;
    Intermech.Navigator.Utils.OpenNewWindow(this.RootDescriptior(this._groupingObjects), (System.IServiceProvider) null);
  }

  /// <summary>
  /// Остановить фоновый поток, выполняющий поиск группирующих объектов
  /// </summary>
  private void StopThread()
  {
    lock (this.lockView)
    {
      if (this.thread == null)
        return;
      this.thread = (Thread) null;
      this.threadName = string.Empty;
    }
    this._groupingObjects.Clear();
    this.Clear();
    this._grid.Enabled = true;
    this.UpdateControls();
  }

  /// <summary>
  /// Запустить фоновый поток, выполняющий поиск группирующих объектов
  /// </summary>
  private void StartThread()
  {
    this.StopThread();
    this._selectedAnalyzerName = this.comboBoxMode.ComboBox.SelectedItem as string;
    this._groupingObjects = this.FindTopItems(this._items, (System.IServiceProvider) this._services);
    this._grid.Enabled = false;
    using (FixEditingContext fixEditingContext = new FixEditingContext())
    {
      this.thread = new Thread(fixEditingContext.SendEditingContextToThread(new ThreadStart(this.ThreadMethod)));
      this.thread.IsBackground = true;
      this.thread.Name = "Navigator.ContextSearchView." + Guid.NewGuid().ToString();
      this.thread.Start();
      this.threadName = this.thread.Name;
    }
    this.timerRefresh.Enabled = true;
    this.UpdateControls();
  }

  /// <summary>Фоновое обращение к серверу приложений</summary>
  protected virtual void ThreadMethod()
  {
    try
    {
      lock (this.lockView)
        this.jobStatus = (SearchGroupingObjectJobStatus) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (ISearchGroupingObjectsService)) is ISearchGroupingObjectsService customService))
        {
          lock (this.lockView)
            this.jobStatus = (SearchGroupingObjectJobStatus) null;
          this.thread = (Thread) null;
          return;
        }
        this.jobID = customService.Analyze(sessionKeeper.Session.SessionGUID, this._selectedAnalyzerName, this._groupingObjects);
        while (!(this.jobID == Guid.Empty))
        {
          SearchGroupingObjectJobStatus groupingObjectJobStatus = customService.QueryJobStatus(this.jobID);
          lock (this.lockView)
            this.jobStatus = groupingObjectJobStatus;
          if (groupingObjectJobStatus != null)
          {
            if (groupingObjectJobStatus.Progress != SearchGroupingObjectJobProgress.NotStarted && groupingObjectJobStatus.Progress != SearchGroupingObjectJobProgress.Working)
            {
              this._groupingObjects = groupingObjectJobStatus.Items;
              break;
            }
            Thread.Sleep(1000);
          }
          else
            break;
        }
      }
      this.thread = (Thread) null;
      this.Invoke((Delegate) new MethodInvoker(this.FillView));
    }
    catch (Exception ex)
    {
    }
  }

  /// <summary>События от таймера</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void timerRefresh_Tick(object sender, EventArgs e)
  {
    this.timerRefresh.Enabled = false;
    lock (this.lockView)
    {
      if (this.thread == null || this.jobStatus != null && this.jobStatus.Progress != SearchGroupingObjectJobProgress.NotStarted && this.jobStatus.Progress != SearchGroupingObjectJobProgress.Working)
        this.StopThread();
      this.UpdateControls();
      this.timerRefresh.Enabled = this.thread != null;
    }
  }

  /// <summary>Внести данные в кэш</summary>
  public void WriteToCache()
  {
    if (this._groupingObjects.Count <= 0 || this._items == null || this._items.Count <= 0 || this._cache == null)
      return;
    this._cache.SetGroupingObjects(this._items.GetItemID(0), this._selectedAnalyzerName, this._groupingObjects);
  }

  /// <summary>Очистить в кэше данные</summary>
  public void ClearInCache()
  {
    if (this._items == null || this._items.Count <= 0 || this._cache == null)
      return;
    this._cache.RemoveGroupingObjects(this._items.GetItemID(0), this._selectedAnalyzerName);
  }

  /// <summary>Изменился выделенный элемент в списке режимов поиска</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void searchModeSelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._inEvent)
      return;
    try
    {
      this._inEvent = true;
      this._selectedAnalyzerName = this.comboBoxMode.ComboBox.SelectedItem as string;
      this.FillViewFromCache();
    }
    finally
    {
      this._inEvent = false;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ContextsSearchView));
    this.comboBoxMode = new ComboBoxItem();
    this.btnAnalyze = new ButtonItem();
    this.btnStop = new ButtonItem();
    this.btnClear = new ButtonItem();
    this.btnOpenInNewWindow = new ButtonItem();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._toolBar, "tbViewBar");
    this._toolBar.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.comboBoxMode,
      (ToolbarItemBase) this.btnAnalyze,
      (ToolbarItemBase) this.btnStop,
      (ToolbarItemBase) this.btnOpenInNewWindow,
      (ToolbarItemBase) this.btnClear
    });
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._toolBar, componentResourceManager.GetString("tbViewBar.ToolTip"));
    componentResourceManager.ApplyResources((object) this._embeddedViewsDropDownMenuItem, "btViewNames");
    componentResourceManager.ApplyResources((object) this._toggleManualSortingButtonItem, "btClearSorting");
    componentResourceManager.ApplyResources((object) this._grid, "grid");
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this._grid.DefaultRow.Key = componentResourceManager.GetString("resource.Key");
    this._grid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._grid, componentResourceManager.GetString("grid.ToolTip"));
    componentResourceManager.ApplyResources((object) this._collapseAllGroupsButtonItem, "btCollapseAll");
    componentResourceManager.ApplyResources((object) this._expandAllGroupsButtonItem, "btExpandAll");
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "ViewsManager");
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._pageViewsManager, componentResourceManager.GetString("ViewsManager.ToolTip"));
    componentResourceManager.ApplyResources((object) this.buttonHeightSet, "buttonHeightSet");
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this._filtersComboBoxItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._filtersComboBoxItem, "listObjectsFiltration");
    this._filtersComboBoxItem.Enabled = false;
    this._filtersComboBoxItem.MinimumControlWidth = (int) byte.MaxValue;
    this._filtersComboBoxItem.MinimumSize = 150;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this._manualSortingSetupButtonItem, "btSetupSorting");
    componentResourceManager.ApplyResources((object) this._toggleGroupingButtonItem, "btClearGrouping");
    componentResourceManager.ApplyResources((object) this._refreshButtonItem, "btRefresh");
    componentResourceManager.ApplyResources((object) this._gridHeaderMenuBar, "menuHeader");
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._gridHeaderMenuBar, componentResourceManager.GetString("menuHeader.ToolTip"));
    componentResourceManager.ApplyResources((object) this._gridHeaderContextMenuBarItem, "contextMenuHeader");
    componentResourceManager.ApplyResources((object) this._changeGridColumnsMenuButtonItem, "mnpSetupColumns");
    componentResourceManager.ApplyResources((object) this._collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem, "btCollapseAndShow");
    componentResourceManager.ApplyResources((object) this._pictureBox, "pictureView");
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._pictureBox, componentResourceManager.GetString("pictureView.ToolTip"));
    componentResourceManager.ApplyResources((object) this._currentVersionsRuleButtonItem, "buttonVersionsRule");
    this._currentVersionsRuleButtonItem.Importance = ToolBarItemImportance.Low;
    componentResourceManager.ApplyResources((object) this.comboBoxMode, "comboBoxMode");
    this.comboBoxMode.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxMode.Importance = ToolBarItemImportance.Highest;
    this.comboBoxMode.Items.AddRange(new object[2]
    {
      (object) "В выделенных объектах",
      (object) "В выделенных объектах и их составах"
    });
    this.comboBoxMode.MinimumControlWidth = (int) byte.MaxValue;
    this.comboBoxMode.MinimumSize = 150;
    this.comboBoxMode.Padding.Bottom = 0;
    this.comboBoxMode.Padding.Left = 1;
    this.comboBoxMode.Padding.Right = 1;
    this.comboBoxMode.Padding.Top = 0;
    this.comboBoxMode.Stretch = true;
    componentResourceManager.ApplyResources((object) this.btnAnalyze, "btnAnalyze");
    this.btnAnalyze.Enabled = false;
    this.btnAnalyze.Image = (Image) componentResourceManager.GetObject("btnAnalyze.Image");
    this.btnAnalyze.Importance = ToolBarItemImportance.Highest;
    this.btnAnalyze.Click += new EventHandler(this.DoStart);
    componentResourceManager.ApplyResources((object) this.btnStop, "btnStop");
    this.btnStop.Enabled = false;
    this.btnStop.Image = (Image) componentResourceManager.GetObject("btnStop.Image");
    this.btnStop.Importance = ToolBarItemImportance.Highest;
    this.btnStop.Click += new EventHandler(this.DoStop);
    componentResourceManager.ApplyResources((object) this.btnClear, "btnClear");
    this.btnClear.Enabled = false;
    this.btnClear.Image = (Image) componentResourceManager.GetObject("btnClear.Image");
    this.btnClear.Importance = ToolBarItemImportance.Highest;
    this.btnClear.Click += new EventHandler(this.DoClear);
    componentResourceManager.ApplyResources((object) this.btnOpenInNewWindow, "btnOpenInNewWindow");
    this.btnOpenInNewWindow.Enabled = false;
    this.btnOpenInNewWindow.Image = (Image) componentResourceManager.GetObject("btnOpenInNewWindow.Image");
    this.btnOpenInNewWindow.Importance = ToolBarItemImportance.Highest;
    this.btnOpenInNewWindow.Click += new EventHandler(this.DoNewWindow);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (ContextsSearchView);
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this, componentResourceManager.GetString("$this.ToolTip"));
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
