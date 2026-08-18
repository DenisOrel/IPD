
// Type: Intermech.Navigator.Controls.PageViewsManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Менеджер закладок навигатора</summary>
public class PageViewsManager : 
  UserControl,
  IViewsManager,
  IViewsContainer,
  IContextAware,
  ICommandTarget,
  ICanCloseViews
{
  /// <summary>Имя последней активной вкладки</summary>
  private string prevViewName = string.Empty;
  /// <summary>
  /// Список уникальных идентификаторов элементов пространства навигации
  /// </summary>
  protected List<INodeID> _selectedNodes = new List<INodeID>();
  private LeftRightAlignment tabsAlignment;
  private IServiceContainer services;
  private string[] allowedViews;
  private string[] suppressedViews;
  private ViewPages viewPages;
  /// <summary>
  /// Запрещает или разрешает работу механизма переключения закладок с помощью
  /// пользовательского интерфейса.
  /// </summary>
  private bool allowUIPaging;
  /// <summary>
  /// Содержит коллекцию тех закладок, которые являются закладками навигатора. Следует помнить,
  /// что некоторые закладки могут иметь произвольную реализацию.
  /// </summary>
  private List<IViewPage> nativeViewPages;
  /// <summary>
  /// Специальная закладка, используемая для сохранения изменений в активной закладке.
  /// </summary>
  public static readonly IView BlackHoleView = (IView) new PageViewsManager.BlackHole();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected PageControl pcViewsArea;

  /// <summary>Создать экземпляр класса</summary>
  public PageViewsManager()
  {
    this.InitializeComponent();
    this.tabsAlignment = LeftRightAlignment.Left;
    this.services = (IServiceContainer) null;
    this.allowedViews = (string[]) null;
    this.suppressedViews = (string[]) null;
    this.allowUIPaging = true;
    this.viewPages = (ViewPages) new ContainerViewPages((IViewsContainer) this);
    this.nativeViewPages = (List<IViewPage>) null;
    this.pcViewsArea.SelectedPageChanging += new PageControlCancelEventHandler(this.PageChanging);
    this.pcViewsArea.SelectedPageChanged += new EventHandler(this.PageChanged);
    this.pcViewsArea.HelpRequested += new HelpEventHandler(this.pcViewsArea_HelpRequested);
    if (Holder.NamedImageList == null)
      return;
    this.pcViewsArea.ImageList = Holder.NamedImageList.ImageList;
  }

  /// <summary>Список имён допустимых к отображению закладок</summary>
  [Browsable(true)]
  [DefaultValue(null)]
  [Category("Behavior")]
  [CustomDescription("Attribute.Client.Core_134")]
  public string[] AllowedViews
  {
    [DebuggerStepThrough] get => this.allowedViews;
    set
    {
      if (!PageViewsManager.IsDifferent(this.allowedViews, value))
        return;
      this.allowedViews = value;
      this.RemoveUnallowedViews();
    }
  }

  /// <summary>Список имён запрещённых к отображению закладок</summary>
  [Browsable(true)]
  [DefaultValue(null)]
  [Category("Behavior")]
  [CustomDescription("Attribute.Client.Core_135")]
  public string[] SuppressedViews
  {
    [DebuggerStepThrough] get => this.suppressedViews;
    set
    {
      if (!PageViewsManager.IsDifferent(this.suppressedViews, value))
        return;
      this.suppressedViews = value;
      this.RemoveUnallowedViews();
    }
  }

  /// <summary>Выравнивание заголовков закладок</summary>
  [Browsable(true)]
  [DefaultValue(Intermech.Docking.TabAlignment.Top)]
  [Category("Appearance")]
  [CustomDescription("Attribute.Client.Core_136")]
  public Intermech.Docking.TabAlignment HeaderAlignment
  {
    [DebuggerStepThrough] get => this.pcViewsArea.TabAlignment;
    set => this.pcViewsArea.TabAlignment = value;
  }

  /// <summary>Выравнивание закладок</summary>
  [Browsable(true)]
  [DefaultValue(LeftRightAlignment.Left)]
  [Category("Appearance")]
  [CustomDescription("Attribute.Client.Core_137")]
  public LeftRightAlignment TabsAligment
  {
    [DebuggerStepThrough] get => this.tabsAlignment;
    set
    {
      if (this.tabsAlignment == value)
        return;
      this.tabsAlignment = value;
      this.LockUpdate();
      try
      {
        Intermech.Docking.TabPage selectedPage = this.pcViewsArea.SelectedPage;
        this.OrderViews();
        if (selectedPage == this.pcViewsArea.SelectedPage)
          return;
        this.pcViewsArea.SelectedPage = selectedPage;
      }
      finally
      {
        this.UnlockUpdate();
      }
    }
  }

  public event EventHandler<PageViewsManager.FilterViewsEventArgs> FilterViews;

  /// <summary>
  /// Обновляет коллекцию видимых закладок навигатора в соответствии с выбранными
  /// элементами навигации.
  /// </summary>
  /// <param name="items">Объект, описывающий выбранные элементы навигации</param>
  public void UpdateViews(ISelectedItems items, bool throwExceptions = true)
  {
    this.LockUpdate();
    List<INodeID> nodeIdList = new List<INodeID>((IEnumerable<INodeID>) this.GetSelectedNodeIds(items));
    INodeID[] viewSelectedNodeIds = (INodeID[]) null;
    try
    {
      ViewsTable viewsTable = this.GetViewsTable(items);
      if (this.pcViewsArea.SelectedPage != null)
      {
        PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(this.pcViewsArea.SelectedPage);
        if (viewAdapter != null)
        {
          if (this.CompareNodeIds(this._selectedNodes, nodeIdList))
            viewSelectedNodeIds = viewAdapter.GetSelectedNodeIds();
          this.prevViewName = viewAdapter.Name;
          viewAdapter.Deactivate((PageViewsManager.ViewAdapter) null);
        }
      }
      for (int index = 0; index < this.pcViewsArea.TabPages.Count; ++index)
      {
        PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(this.pcViewsArea.TabPages[index]);
        if (viewAdapter != null)
        {
          if ((viewsTable.ViewNames == null ? -1 : Array.IndexOf<string>(viewsTable.ViewNames, viewAdapter.Name)) >= 0)
          {
            ViewInfo info = viewAdapter.Info;
            ViewInfo viewInfo = viewsTable[viewAdapter.Name].ViewInfo;
            if (info.CreatorCallback.Equals((object) viewInfo.CreatorCallback) && info.AdditionalInfo.Equals(viewInfo.AdditionalInfo))
            {
              viewsTable.Remove(viewAdapter.Name);
              continue;
            }
          }
          viewAdapter.Redundant = true;
        }
      }
      this.CreateNewViews(viewsTable, items);
      this.RemoveRedundantViews();
      this.InitViews(items, throwExceptions);
      this.OrderViews();
      if (items.Count == 0)
      {
        int count = this.pcViewsArea.TabPages.Count;
      }
      if (ServicesManager.GetService(typeof (IViewsManagerService)) is IViewsManagerService service)
      {
        ActivateViewEventArgs e = new ActivateViewEventArgs(this._selectedNodes, nodeIdList, this.prevViewName, this.ActiveViewName, this.ActiveViewPage != null ? this.ActiveViewPage.View : (IView) null);
        service.FireActivateViewEvent((object) this, e);
        this._selectedNodes = nodeIdList;
        if (e.NewViewName != string.Empty && e.NewViewName != this.ActiveViewName)
          this.ActivateView(e.NewViewName, nodeIdList);
        else
          this.CheckSettingsAndActivateView(this.prevViewName, nodeIdList, viewSelectedNodeIds);
      }
      else
        this.CheckSettingsAndActivateView(this.prevViewName, nodeIdList, viewSelectedNodeIds);
    }
    finally
    {
      this.UnlockUpdate();
    }
    if (this.ActiveViewName != this.prevViewName)
      this.RaiseActivePageChanged();
    this.RaiseViewsUpdated();
  }

  private INodeID[] GetSelectedNodeIds(ISelectedItems items)
  {
    List<INodeID> nodeIdList = new List<INodeID>(items != null ? items.Count : 0);
    if (items != null)
    {
      for (int index = 0; index < items.Count; ++index)
        nodeIdList.Add(items.GetItemID(index));
    }
    return nodeIdList.ToArray();
  }

  /// <summary>
  /// Заставляет активную закладку сохранить все сделанные изменения.
  /// </summary>
  /// <param name="doReload">Перегружать активную вьюшку, или нет</param>
  internal void SaveChanges(bool doReload)
  {
    IViewPage activeViewPage = this.ActiveViewPage;
    if (this.ActiveViewPage == null)
      return;
    if (doReload)
    {
      this.ActiveViewPage.View.Deactivate(PageViewsManager.BlackHoleView);
      if (this.ActiveViewPage == null)
        return;
      this.ActiveViewPage.View.Activate(PageViewsManager.BlackHoleView);
    }
    else
      this.ActiveViewPage.View.Deactivate((IView) null);
  }

  /// <summary>
  /// Заставляет активную закладку сохранить все сделанные изменения.
  /// </summary>
  public void SaveChanges() => this.SaveChanges(true);

  /// <summary>Закрывает все закладки навигатора.</summary>
  public void CloseViews()
  {
    int count = this.pcViewsArea.TabPages.Count;
    this.LockUpdate();
    try
    {
      if (this.pcViewsArea.SelectedPage != null)
      {
        PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(this.pcViewsArea.SelectedPage);
        if (viewAdapter != null)
        {
          this.prevViewName = viewAdapter.Name;
          viewAdapter.Deactivate((PageViewsManager.ViewAdapter) null);
        }
      }
      int index = 0;
      while (index < this.pcViewsArea.TabPages.Count)
      {
        PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(this.pcViewsArea.TabPages[index]);
        if (viewAdapter != null)
        {
          viewAdapter.Dispose();
          this.pcViewsArea.TabPages.RemoveAt(index);
        }
        else
          ++index;
      }
      this.InvalidateNativeViewPages();
    }
    finally
    {
      this.UnlockUpdate();
    }
    if (count == 0)
      return;
    this.RaiseActivePageChanged();
  }

  /// <summary>
  /// Возвращает коллекцию закладок навигатора, оторбражаемых менеджером.
  /// </summary>
  [Browsable(false)]
  public ViewPages ViewPages
  {
    [DebuggerStepThrough] get => this.viewPages;
  }

  /// <summary>Возвращает активную закладку навигатора.</summary>
  [Browsable(false)]
  public IViewPage ActiveViewPage
  {
    get
    {
      if (this.pcViewsArea.SelectedPage != null)
      {
        IViewPage viewAdapter = (IViewPage) this.GetViewAdapter(this.pcViewsArea.SelectedPage);
        if (viewAdapter != null)
          return viewAdapter;
      }
      return (IViewPage) null;
    }
    set
    {
      IViewPage activeViewPage = this.ActiveViewPage;
      if (activeViewPage == value)
        return;
      if (value != null)
      {
        for (int index = 0; index < this.pcViewsArea.TabPages.Count; ++index)
        {
          Intermech.Docking.TabPage tabPage = this.pcViewsArea.TabPages[index];
          PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(tabPage);
          if (viewAdapter != null && viewAdapter.Name == value.Name)
          {
            this.pcViewsArea.SelectedPage = tabPage;
            return;
          }
        }
        throw new InvalidOperationException(LocalizationHolder.rm.GetString("Client.Core_533"));
      }
      if (activeViewPage != null)
        throw new InvalidOperationException(LocalizationHolder.rm.GetString("Client.Core_534"));
    }
  }

  /// <summary>
  /// Срабатывает, если меняется активная закладка навигатора.
  /// </summary>
  public event EventHandler ActiveViewPageChanged;

  /// <summary>Срабатывает после обновления закладок</summary>
  public event EventHandler ViewsUpdated;

  /// <summary>Возвращает количество закладок.</summary>
  int IViewsContainer.Count
  {
    get
    {
      this.RefreshNativeViewPages();
      return this.nativeViewPages.Count;
    }
  }

  /// <summary>Возвращает указанную закладку.</summary>
  /// <param name="index">Индекс закладки</param>
  /// <returns>Закладка навигатора</returns>
  IViewPage IViewsContainer.this[int index]
  {
    get
    {
      this.RefreshNativeViewPages();
      return this.nativeViewPages[index];
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.IServiceProvider Services
  {
    get => (System.IServiceProvider) this.services;
    set => this.services = (IServiceContainer) new ServiceContainer(value);
  }

  public IView ActiveView
  {
    get
    {
      if (this.pcViewsArea.SelectedPage != null)
      {
        PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(this.pcViewsArea.SelectedPage);
        if (viewAdapter != null)
          return viewAdapter.View;
      }
      return (IView) null;
    }
  }

  public bool QueryStatus(ICommandState commandState)
  {
    return this.ActiveView is ICommandTarget activeView && activeView.QueryStatus(commandState);
  }

  public bool Execute(ICommandState commandState)
  {
    return this.ActiveView is ICommandTarget activeView && activeView.Execute(commandState);
  }

  private ViewsTable GetViewsTable(ISelectedItems items)
  {
    AdjustableViews service = ServicesManager.GetService(typeof (AdjustableViews)) as AdjustableViews;
    ViewsTable viewsTable1 = new ViewsCollector(items, (System.IServiceProvider) this.services).Execute();
    if (viewsTable1.ViewNames == null)
      return viewsTable1;
    string[] allowedViews = this.allowedViews;
    if (this.FilterViews != null)
    {
      PageViewsManager.FilterViewsEventArgs e = new PageViewsManager.FilterViewsEventArgs(viewsTable1.ViewNames);
      this.FilterViews((object) this, e);
      allowedViews = e.AllowedViews;
    }
    if (allowedViews == null && this.suppressedViews == null && service == null)
      return viewsTable1;
    ViewsTable viewsTable2;
    if (allowedViews == null)
    {
      viewsTable2 = viewsTable1;
    }
    else
    {
      viewsTable2 = new ViewsTable();
      for (int index = 0; index < allowedViews.Length; ++index)
      {
        if (viewsTable1.Contains(allowedViews[index]))
          viewsTable2.Add(allowedViews[index], viewsTable1[allowedViews[index]]);
      }
    }
    if (this.suppressedViews != null)
    {
      for (int index = 0; index < this.suppressedViews.Length; ++index)
        viewsTable2.Remove(this.suppressedViews[index]);
    }
    string[] viewNames = viewsTable2.ViewNames;
    if (service != null && viewNames != null)
    {
      int index = 0;
      for (int count = items.Count; index < count; ++index)
      {
        if (!(items.GetItemID(index) is NodeID itemId))
        {
          foreach (string str in viewNames)
          {
            AdjustableView view = service.FindView(str);
            if (view != null)
            {
              List<int> objectTypes = view.ObjectTypes;
              if (!view.Visible && objectTypes.Count == 0)
                viewsTable2.Remove(str);
              else if (objectTypes.Count != 0)
                viewsTable2.Remove(str);
            }
          }
        }
        else
        {
          foreach (string str in viewNames)
          {
            AdjustableView view = service.FindView(str);
            if (view != null)
            {
              List<int> objectTypes = view.ObjectTypes;
              if (!view.Visible || objectTypes.Count != 0)
              {
                if (!view.Visible && objectTypes.Count == 0)
                  viewsTable2.Remove(str);
                else if (view.Visible && objectTypes.Count != 0)
                {
                  if (!objectTypes.Contains(itemId.ObjectTypeID))
                    viewsTable2.Remove(str);
                }
                else if (!view.Visible && objectTypes.Count != 0 && objectTypes.Contains(itemId.ObjectTypeID))
                  viewsTable2.Remove(str);
              }
            }
          }
        }
      }
    }
    return viewsTable2;
  }

  private void CreateNewViews(ViewsTable viewsTable, ISelectedItems items)
  {
    if (viewsTable.ViewNames == null)
      return;
    for (int index = 0; index < viewsTable.ViewNames.Length; ++index)
    {
      try
      {
        this.CreateTabPage(viewsTable.ViewNames[index], viewsTable[viewsTable.ViewNames[index]].ViewInfo, items, (System.IServiceProvider) this.services);
      }
      catch
      {
      }
    }
  }

  private void RemoveRedundantViews()
  {
    int index = 0;
    while (index < this.pcViewsArea.TabPages.Count)
    {
      PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(this.pcViewsArea.TabPages[index]);
      if (viewAdapter != null && viewAdapter.Redundant)
      {
        viewAdapter.Dispose();
        this.pcViewsArea.TabPages.RemoveAt(index);
      }
      else
        ++index;
    }
  }

  /// <summary>
  /// Удаляет все закладки, которые не подходят под условия фильтра по
  /// именам закладок.
  /// </summary>
  private void RemoveUnallowedViews()
  {
    string str = string.Empty;
    this.LockUpdate();
    try
    {
      str = this.ActiveViewName;
      this.RemoveUnallowedViewsCore();
    }
    finally
    {
      this.UnlockUpdate();
    }
    if (!(this.ActiveViewName != str))
      return;
    this.RaiseActivePageChanged();
  }

  private void RemoveUnallowedViewsCore()
  {
    AdjustableViews service = ServicesManager.GetService(typeof (AdjustableViews)) as AdjustableViews;
    for (int index = 0; index < this.pcViewsArea.TabPages.Count; ++index)
    {
      PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(this.pcViewsArea.TabPages[index]);
      if (viewAdapter != null)
      {
        bool flag = this.allowedViews == null || Array.IndexOf<string>(this.allowedViews, viewAdapter.Name) >= 0;
        if (flag)
        {
          if (this.suppressedViews != null && Array.IndexOf<string>(this.suppressedViews, viewAdapter.Name) >= 0)
            flag = false;
          if (service != null)
          {
            AdjustableView view = service.FindView(viewAdapter.Name);
            if (view != null && !view.Visible && view.ObjectTypes.Count == 0)
              flag = false;
          }
        }
        if (!flag)
          viewAdapter.Redundant = true;
      }
    }
    for (int index = this.pcViewsArea.TabPages.Count - 1; index >= 0; --index)
    {
      PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(this.pcViewsArea.TabPages[index]);
      if (viewAdapter != null && viewAdapter.Redundant && index != this.pcViewsArea.SelectedPage.Index)
      {
        this.pcViewsArea.TabPages.RemoveAt(index);
        viewAdapter.Dispose();
      }
    }
    if (this.pcViewsArea.SelectedPage != null)
    {
      PageViewsManager.ViewAdapter viewAdapter1 = this.GetViewAdapter(this.pcViewsArea.SelectedPage);
      if (viewAdapter1 != null && viewAdapter1.Redundant)
      {
        int index = this.pcViewsArea.SelectedPage.Index - 1;
        if (index < 0 && this.pcViewsArea.TabPages.Count > 1)
          index = 1;
        PageViewsManager.ViewAdapter viewAdapter2 = index >= 0 ? this.GetViewAdapter(this.pcViewsArea.TabPages[index]) : (PageViewsManager.ViewAdapter) null;
        viewAdapter1.Deactivate(viewAdapter2);
        viewAdapter2?.Activate(viewAdapter1);
      }
    }
    this.InvalidateNativeViewPages();
  }

  /// <summary>Инициализация вьюшек.</summary>
  private void InitViews(ISelectedItems items, bool throwExceptions = true)
  {
    List<Intermech.Docking.TabPage> tabPageList = new List<Intermech.Docking.TabPage>();
    for (int index = 0; index < this.pcViewsArea.TabPages.Count; ++index)
    {
      Intermech.Docking.TabPage tabPage = this.pcViewsArea.TabPages[index];
      PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(tabPage);
      if (viewAdapter != null)
      {
        if (throwExceptions)
        {
          viewAdapter.Initialize(items, (System.IServiceProvider) this.services);
          tabPage.Text = viewAdapter.Caption;
          tabPage.TabImageIndex = viewAdapter.ImageIndex;
        }
        else
        {
          try
          {
            viewAdapter.Initialize(items, (System.IServiceProvider) this.services);
            tabPage.Text = viewAdapter.Caption;
            tabPage.TabImageIndex = viewAdapter.ImageIndex;
          }
          catch
          {
            tabPageList.Add(tabPage);
          }
        }
      }
    }
    foreach (Intermech.Docking.TabPage tabPage in tabPageList)
      this.pcViewsArea.TabPages.Remove(tabPage);
  }

  private void OrderViews()
  {
    Intermech.Docking.TabPage[] array = this.pcViewsArea.TabPages.Cast<Intermech.Docking.TabPage>().OrderBy<Intermech.Docking.TabPage, int>((Func<Intermech.Docking.TabPage, int>) (o =>
    {
      PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(o);
      if (viewAdapter == null)
        return int.MaxValue;
      AdjustableView view = ServiceLocator.Get<AdjustableViews>().FindView(viewAdapter.Name);
      return view == null ? viewAdapter.OrderID : view.OrderID;
    })).ToArray<Intermech.Docking.TabPage>();
    for (int index = 0; index < array.Length; ++index)
      array[index].Index = index;
    this.InvalidateNativeViewPages();
  }

  private PageViewsManager.ViewAdapter GetViewAdapter(Intermech.Docking.TabPage tabPage)
  {
    return tabPage?.Tag as PageViewsManager.ViewAdapter;
  }

  /// <summary>
  /// Возвращает имя текущей закладки или пустую строку, если закладка отсутствует.
  /// </summary>
  private string ActiveViewName => this.ActiveViewPage?.Name ?? string.Empty;

  private void CreateTabPage(
    string viewName,
    ViewInfo viewInfo,
    ISelectedItems items,
    System.IServiceProvider services)
  {
    PageViewsManager.ViewAdapter viewAdapter = new PageViewsManager.ViewAdapter(this, viewName, viewInfo, items, services, this.pcViewsArea);
  }

  /// <summary>Активировать закладку</summary>
  /// <param name="activeViewName">Имя активируемой закладки</param>
  /// <param name="newSelectedNodes">Список новых идентификаторов, по которым были построены закладки</param>
  private void ActivateView(
    string activeViewName,
    List<INodeID> newSelectedNodes,
    INodeID[] viewSelectedNodeIds = null)
  {
    if (this.pcViewsArea.TabPages.Count <= 0)
      return;
    Intermech.Docking.TabPage tabPage1 = (Intermech.Docking.TabPage) null;
    if (activeViewName != string.Empty)
    {
      for (int index = 0; index < this.pcViewsArea.TabPages.Count; ++index)
      {
        Intermech.Docking.TabPage tabPage2 = this.pcViewsArea.TabPages[index];
        PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(tabPage2);
        if (viewAdapter != null && (UISettings.AlwaysShowFirstTab && index == 0 || viewAdapter.Name == activeViewName))
        {
          this.pcViewsArea.SelectedPage = tabPage2;
          tabPage1 = tabPage2;
          break;
        }
      }
    }
    if (tabPage1 == null)
      this.pcViewsArea.SelectedPage = this.pcViewsArea.TabPages[0];
    PageViewsManager.ViewAdapter viewAdapter1 = this.GetViewAdapter(this.pcViewsArea.SelectedPage);
    if (viewAdapter1 == null)
      return;
    viewAdapter1.Activate((PageViewsManager.ViewAdapter) null);
    viewAdapter1.SetSelectedNodeIds(viewSelectedNodeIds);
  }

  private void LockUpdate()
  {
    this.allowUIPaging = false;
    this.pcViewsArea.SuspendLayout();
  }

  private void UnlockUpdate()
  {
    this.pcViewsArea.ResumeLayout();
    this.allowUIPaging = true;
  }

  /// <summary>
  /// Делает невалидной коллекцию закладок навигатора после изменения коллекции
  /// всех закладок менеджера.
  /// </summary>
  private void InvalidateNativeViewPages() => this.nativeViewPages = (List<IViewPage>) null;

  /// <summary>
  /// Перестраивает коллекцию закладок навигатора, в нее попадут только те закладки,
  /// которые реализуют интерфейс IView.
  /// </summary>
  private void RefreshNativeViewPages()
  {
    if (this.nativeViewPages != null)
      return;
    this.nativeViewPages = new List<IViewPage>();
    for (int index = 0; index < this.pcViewsArea.TabPages.Count; ++index)
    {
      if (this.pcViewsArea.TabPages[index].Tag is IViewPage tag)
        this.nativeViewPages.Add(tag);
    }
  }

  private static bool IsDifferent(string[] oldValue, string[] newValue)
  {
    if (newValue == null)
      return oldValue != null;
    if (oldValue == null || oldValue.Length != newValue.Length)
      return true;
    for (int index = 0; index < newValue.Length; ++index)
    {
      if (Array.IndexOf<string>(oldValue, newValue[index]) < 0)
        return true;
    }
    return false;
  }

  private void RaiseActivePageChanged()
  {
    (this.services != null ? this.services.GetService(typeof (ICommandManager)) as ICommandManager : (ICommandManager) null)?.QueryStatus();
    if (this.ActiveViewPageChanged == null)
      return;
    this.ActiveViewPageChanged((object) this, EventArgs.Empty);
  }

  private void RaiseViewsUpdated()
  {
    if (this.ViewsUpdated == null)
      return;
    this.ViewsUpdated((object) this, EventArgs.Empty);
  }

  private void PageChanging(object sender, PageControlCancelEventArgs e)
  {
    if (!this.allowUIPaging)
      return;
    Intermech.Docking.TabPage selectedPage = this.pcViewsArea.SelectedPage;
    Intermech.Docking.TabPage tabPage = e.TabPage;
    PageViewsManager.ViewAdapter viewAdapter1 = selectedPage == null ? (PageViewsManager.ViewAdapter) null : this.GetViewAdapter(selectedPage);
    PageViewsManager.ViewAdapter viewAdapter2 = tabPage == null ? (PageViewsManager.ViewAdapter) null : this.GetViewAdapter(tabPage);
    if (viewAdapter1 != null)
    {
      if (viewAdapter1.IsCanDeactivateViewSupported)
      {
        e.Cancel = !viewAdapter1.CanDeactivate(this);
        if (e.Cancel)
          return;
      }
      viewAdapter1.Deactivate(viewAdapter2);
    }
    viewAdapter2?.Activate(viewAdapter1);
  }

  private void PageChanged(object sender, EventArgs e)
  {
    if (!this.allowUIPaging)
      return;
    this.RaiseActivePageChanged();
  }

  /// <summary>
  /// Выполнить запрос, можно ли закрывать форму, на которой расположены закладки
  /// </summary>
  /// <param name="sender">Отправитель запроса</param>
  /// <returns>true - закладка разрешает закрытие формы, false - закладка запрещает закрытие формы</returns>
  public bool CanClose(object sender)
  {
    bool flag = true;
    for (int index = 0; index < this.pcViewsArea.TabPages.Count; ++index)
    {
      PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(this.pcViewsArea.TabPages[index]);
      if (viewAdapter != null)
      {
        if (viewAdapter.IsCanCloseViewsSupported)
          flag &= viewAdapter.CanClose(this);
        if (!flag)
          return flag;
      }
    }
    return flag;
  }

  /// <summary>показать раздел справки для данного контрола</summary>
  private void pcViewsArea_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(this.pcViewsArea.SelectedPage);
    if (viewAdapter == null)
      return;
    HelpProvidersClass.ShowHelpTopic(viewAdapter.HelpID, viewAdapter.HelpPath);
  }

  private void CheckSettingsAndActivateView(
    string prevViewName,
    List<INodeID> nodeIds,
    INodeID[] viewSelectedNodeIds = null)
  {
    if (UISettings.SwitchToCard && !string.IsNullOrEmpty(prevViewName) && this.pcViewsArea.TabPages.Cast<Intermech.Docking.TabPage>().Any<Intermech.Docking.TabPage>((Func<Intermech.Docking.TabPage, bool>) (o =>
    {
      PageViewsManager.ViewAdapter viewAdapter = this.GetViewAdapter(o);
      return viewAdapter != null && viewAdapter.IsPropertiesView && viewAdapter.Name == prevViewName;
    })))
    {
      PageViewsManager.ViewAdapter viewAdapter = this.pcViewsArea.TabPages.Cast<Intermech.Docking.TabPage>().Select<Intermech.Docking.TabPage, PageViewsManager.ViewAdapter>((Func<Intermech.Docking.TabPage, PageViewsManager.ViewAdapter>) (o => this.GetViewAdapter(o))).FirstOrDefault<PageViewsManager.ViewAdapter>((Func<PageViewsManager.ViewAdapter, bool>) (o => o != null && o.IsFormDesignerView));
      if (viewAdapter != null)
      {
        this.ActivateView(viewAdapter.Name, nodeIds, viewSelectedNodeIds);
        return;
      }
    }
    this.ActivateView(prevViewName, nodeIds, viewSelectedNodeIds);
  }

  private bool CompareNodeIds(List<INodeID> nodeIds, List<INodeID> otherNodeIds)
  {
    if (nodeIds.Count != otherNodeIds.Count)
      return false;
    foreach (INodeID nodeId in nodeIds)
    {
      if (!otherNodeIds.Contains(nodeId))
        return false;
    }
    return true;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.pcViewsArea.SelectedPageChanging -= new PageControlCancelEventHandler(this.PageChanging);
      this.pcViewsArea.SelectedPageChanged -= new EventHandler(this.PageChanged);
      this.pcViewsArea.HelpRequested -= new HelpEventHandler(this.pcViewsArea_HelpRequested);
      this.pcViewsArea.Dispose();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PageViewsManager));
    this.pcViewsArea = new PageControl();
    this.SuspendLayout();
    this.pcViewsArea.BorderStyle = Intermech.Docking.Rendering.BorderStyle.None;
    this.pcViewsArea.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this.pcViewsArea, "pcViewsArea");
    this.pcViewsArea.Flat = false;
    this.pcViewsArea.Name = "pcViewsArea";
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CausesValidation = false;
    this.Controls.Add((Control) this.pcViewsArea);
    this.Name = nameof (PageViewsManager);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Реализует специфическую закладку, которая используется для сохранения изменений в
  /// активной закладке. Потребность в такой закладке возникла из-за несовершенства
  /// интерфейса IView - отсутствия в нем метода QueryClose.
  /// </summary>
  private class BlackHole : UserControl, IView
  {
    public void Initialize(ISelectedItems items, System.IServiceProvider provider)
    {
    }

    public void Activate(IView previousView)
    {
    }

    public void Deactivate(IView nextView)
    {
    }

    public string Caption => string.Empty;

    public int ImageIndex => -1;

    public int OrderID => -1;
  }

  public sealed class FilterViewsEventArgs : EventArgs
  {
    public FilterViewsEventArgs(string[] views) => this.Views = views;

    public string[] Views { get; private set; }

    public string[] AllowedViews { get; set; }
  }

  public sealed class ViewAdapter : IDisposable, IViewPage
  {
    public ViewAdapter(
      PageViewsManager pageViewsManager,
      string viewName,
      ViewInfo viewInfo,
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider,
      PageControl pageControl)
    {
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentException();
      if (viewInfo == null)
        throw new ArgumentNullException(nameof (viewInfo));
      if (selectedItems == null)
        throw new ArgumentNullException(nameof (selectedItems));
      if (serviceProvider == null)
        throw new ArgumentNullException(nameof (serviceProvider));
      if (pageControl == null)
        throw new ArgumentNullException(nameof (pageControl));
      this.PageViewsManager = pageViewsManager;
      this.Name = viewName;
      this.Info = viewInfo;
      this.SelectedItems = selectedItems;
      this.ServiceProvider = serviceProvider;
      this.PageControl = pageControl;
      this.GetViewDescription();
      this.CreateTab();
      if (this.ViewDescription != null)
        return;
      this.CreateView();
    }

    public PageViewsManager PageViewsManager { get; private set; }

    public ViewInfo Info { get; private set; }

    public ISelectedItems SelectedItems { get; private set; }

    public System.IServiceProvider ServiceProvider { get; private set; }

    public PageControl PageControl { get; private set; }

    public ViewDescription ViewDescription { get; private set; }

    public Intermech.Docking.TabPage TabPage { get; private set; }

    public bool IsCanDeactivateViewSupported
    {
      get => typeof (ICanDeactivateView).IsAssignableFrom(this.Info.ControlType);
    }

    public bool IsCanCloseViewsSupported
    {
      get => typeof (ICanCloseViews).IsAssignableFrom(this.Info.ControlType);
    }

    public bool IsPropertiesView
    {
      get
      {
        if (this.Info == null || !(this.Info.ControlType != (System.Type) null))
          return false;
        return this.Info.ControlType == typeof (PropertiesView) || this.Info.ControlType.IsSubclassOf(typeof (PropertiesView));
      }
    }

    public bool IsFormDesignerView
    {
      get
      {
        if (this.Info == null || !(this.Info.ControlType != (System.Type) null))
          return false;
        return this.Info.ControlType == typeof (FormDesignerView) || this.Info.ControlType.IsSubclassOf(typeof (FormDesignerView));
      }
    }

    public bool Redundant { get; set; }

    public bool CanDeactivate(PageViewsManager pageViewsManager)
    {
      if (this.View == null)
        return false;
      return !(this.View is ICanDeactivateView view) || view.CanDeactivate((object) pageViewsManager);
    }

    public bool CanClose(PageViewsManager pageViewsManager)
    {
      return this.View == null ? this.IsCanCloseViewsSupported : this.View is ICanCloseViews view && view.CanClose((object) pageViewsManager);
    }

    public INodeID[] GetSelectedNodeIds()
    {
      List<INodeID> nodeIdList = new List<INodeID>();
      if (this.View is ISelectedItemsHost view)
      {
        for (int index = 0; index < view.SelectedItems.Count; ++index)
          nodeIdList.Add(view.SelectedItems.GetItemID(index));
      }
      return nodeIdList.ToArray();
    }

    public void SetSelectedNodeIds(INodeID[] nodeIds)
    {
      if (nodeIds == null || !(this.View is ChildrenView view))
        return;
      view.SelectNodes(((IEnumerable<INodeID>) nodeIds).ToList<INodeID>());
    }

    public void Dispose()
    {
      if (!(this.View is IDisposable))
        return;
      ((IDisposable) this.View).Dispose();
    }

    public string Name { get; private set; }

    public Control Control { get; private set; }

    public IView View { get; private set; }

    /// <summary>вернуть раздел справки для закладки</summary>
    public string HelpID
    {
      get
      {
        PageViewsManager.ViewAdapter viewAdapter = this;
        while (viewAdapter.Control is IEmbeddedViews && (viewAdapter.Control as IEmbeddedViews).IsOpen && (viewAdapter.Control as UserControl).ActiveControl is PageViewsManager activeControl)
          viewAdapter = activeControl.ActiveViewPage as PageViewsManager.ViewAdapter;
        return viewAdapter.ViewDescription == null ? viewAdapter.Info.HelpTopicID : viewAdapter.ViewDescription.HelpTopicId;
      }
    }

    /// <summary>вернуть путь к справке</summary>
    public string HelpPath
    {
      get
      {
        PageViewsManager.ViewAdapter viewAdapter = this;
        while (viewAdapter.Control is IEmbeddedViews && (viewAdapter.Control as IEmbeddedViews).IsOpen && (viewAdapter.Control as UserControl).ActiveControl is PageViewsManager activeControl)
          viewAdapter = activeControl.ActiveViewPage as PageViewsManager.ViewAdapter;
        return viewAdapter.ViewDescription == null ? viewAdapter.Info.HelpPath : viewAdapter.ViewDescription.HelpPath;
      }
    }

    public string Caption
    {
      get => this.ViewDescription == null ? this.View.Caption : this.ViewDescription.Caption;
    }

    public int ImageIndex
    {
      get => this.ViewDescription == null ? this.View.ImageIndex : this.ViewDescription.ImageIndex;
    }

    public int OrderID
    {
      get => this.ViewDescription == null ? this.View.OrderID : this.ViewDescription.OrderID;
    }

    public void Activate(PageViewsManager.ViewAdapter previousView)
    {
      if (this.ViewDescription != null && this.View == null)
      {
        this.CreateView();
        AdvancedServiceContainer provider = new AdvancedServiceContainer(this.ServiceProvider);
        provider.AddService(typeof (IViewsManager), (object) this.PageViewsManager);
        this.View.Initialize(this.SelectedItems, (System.IServiceProvider) provider);
      }
      this.View.Activate(previousView?.View);
    }

    public void Deactivate(PageViewsManager.ViewAdapter nextView)
    {
      this.View.Deactivate(nextView?.View);
    }

    public void Initialize(ISelectedItems items, System.IServiceProvider provider)
    {
      this.SelectedItems = items;
      this.ServiceProvider = provider;
      this.GetViewDescription();
      this.UpdateTabPage();
      AdvancedServiceContainer provider1 = new AdvancedServiceContainer(provider);
      provider1.AddService(typeof (IViewsManager), (object) this.PageViewsManager);
      if (this.View == null)
        return;
      this.View.Initialize(items, (System.IServiceProvider) provider1);
    }

    private void GetViewDescription()
    {
      if (!(this.Info.ControlType != (System.Type) null) || !(Attribute.GetCustomAttribute((MemberInfo) this.Info.ControlType, typeof (ViewDescriptionProviderAttribute)) is ViewDescriptionProviderAttribute customAttribute))
        return;
      this.ViewDescription = customAttribute.GetViewDescription(this.SelectedItems, this.ServiceProvider);
    }

    private void CreateTab()
    {
      this.TabPage = new Intermech.Docking.TabPage();
      this.TabPage.BorderStyle = Intermech.Docking.Rendering.BorderStyle.None;
      this.TabPage.Tag = (object) this;
      this.UpdateTabPage();
      this.PageControl.TabPages.Add(this.TabPage);
    }

    private void UpdateTabPage()
    {
      if (this.ViewDescription == null)
        return;
      this.TabPage.Text = this.ViewDescription.Caption;
      this.TabPage.TabImageIndex = this.ViewDescription.ImageIndex;
    }

    private void CreateView()
    {
      this.Control = this.Info.CreatorCallback(this.SelectedItems, this.ServiceProvider, this.Info.AdditionalInfo);
      this.Control.Dock = DockStyle.Fill;
      this.Control.Tag = (object) this.Name;
      this.TabPage.Controls.Add(this.Control);
      this.View = (IView) this.Control;
    }
  }
}
