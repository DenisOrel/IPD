// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.DocumsObject
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Archives;

/// <summary>Закладка "Архивы" ("Документы")</summary>
internal class DocumsObject : ObjectsViewBase
{
  /// <summary>
  /// Узел дерева "Навигатора", в контексте которого работает закладка
  /// </summary>
  private NavigatorTreeNode _parentTreeNode;
  /// <summary>Роль, под которой зашел юзер</summary>
  private long _userRoleId;
  /// <summary>Служба уведомлений</summary>
  private INotificationService _ns;
  private IArchiveColumnsSettingsCacheService _archiveColumnsSettingsCache;
  private ArchiveHierarchyService _archiveHierarchyService;
  /// <summary>Для какого архива показываем вкладку "Документы"</summary>
  private long _archiveObjectId;
  private bool _hasViewArchives;

  /// <summary>Создать закладку "Архивы" ("Документы")</summary>
  public DocumsObject()
  {
    this._ns = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this._ns == null)
      return;
    this._ns.Subscribe("ObjectsChanged", new NotificationEventHandler(this.NotifyEvent));
    this._ns.Subscribe("ArchiveChanged", new NotificationEventHandler(this.NotifyEvent));
  }

  /// <summary>Освободить ресурсы закладки</summary>
  /// <param name="disposing">true - удаление компонента</param>
  protected override void Dispose(bool disposing)
  {
    this._ns.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.NotifyEvent));
    this._ns.Unsubscribe("ArchiveChanged", new NotificationEventHandler(this.NotifyEvent));
    base.Dispose(disposing);
  }

  /// <summary>Заголовок закладки</summary>
  public override string Caption
  {
    get
    {
      return this._services.GetService(typeof (ViewArchives)) != null ? ServiceHolder.rm.GetString("Archives_3") : ServiceHolder.rm.GetString("Archives_4");
    }
  }

  /// <summary>Порядковый номер закладки</summary>
  public override int OrderID
  {
    [DebuggerStepThrough] get => 1;
  }

  /// <summary>Содержимое закладки</summary>
  public override ContentType ViewContentType
  {
    get => this._hasViewArchives ? ContentType.Folders : ContentType.NonFolders;
  }

  /// <summary>Уведомление</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void NotifyEvent(object sender, NotificationEventArgs e)
  {
    if (!e.EventName.Equals("ArchiveChanged") && !e.EventName.Equals("ObjectsChanged"))
      return;
    this.ReloadItems();
  }

  /// <summary>Активация закладки</summary>
  /// <param name="previousView">Предыдущая закладка</param>
  public override void Activate(IView previousView)
  {
    this._parentTreeNode = !(this._services.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service1) || service1.FocusedNode == null ? (NavigatorTreeNode) null : service1.FocusedNode;
    if (this._parentTreeNode == null || this._parentTreeNode.NodeID == null || !this._parentTreeNode.InTree)
      this._parentTreeNode = (NavigatorTreeNode) null;
    this.SetArchiveId();
    this._hasViewArchives = this._services.GetService(typeof (ViewArchives)) != null;
    this._archiveColumnsSettingsCache = ApplicationServices.Container.GetService(typeof (IArchiveColumnsSettingsCacheService)) as IArchiveColumnsSettingsCacheService;
    this._archiveHierarchyService = ApplicationServices.Container.GetService(typeof (ArchiveHierarchyService)) as ArchiveHierarchyService;
    if (ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service2)
      this._userRoleId = service2.RoleID;
    base.Activate(previousView);
  }

  /// <summary>
  /// Высчитываем Ид архива, для которого открыта текущая вкладка "Документы"
  /// Если какие-то проблемы, то записываем Intermech.Consts.UnknownObjectId
  /// </summary>
  private void SetArchiveId()
  {
    INode parentNode = this._parentNode;
    if (parentNode == null)
      this._archiveObjectId = 0L;
    else if (parentNode.GetData(this._path.LastID, typeof (IDBObjectID)) is IDBObjectID data)
      this._archiveObjectId = data.Value;
    else
      this._archiveObjectId = 0L;
  }

  /// <summary>
  /// Отыскать и назначить родительские категорию, тип и дополнительное имя
  /// Родительские - это те, на которые есть настройки. Если есть у текущего архива - он сам себе родитель в этом плане
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  [CanBeNull]
  private NavigatorColumns FindNavigatorColumnsInSettings(int category, int type, string suffix)
  {
    int category1 = category;
    int type1 = type;
    string suffix1 = suffix;
    NavigatorColumns navigatorColumns1 = this._navigatorColumnsService.GetNavigatorColumns(category1, type1, suffix1, false);
    if (navigatorColumns1 != null)
      return navigatorColumns1;
    if (this._archiveObjectId == 0L)
      return this.GetNavigatorColumnsForArchivesByDefault();
    NodeColumnCollection columnsSettingsForRole1 = this._archiveColumnsSettingsCache.GetArchiveColumnsSettingsForRole(this._archiveObjectId, this._userRoleId);
    if (columnsSettingsForRole1 != null)
    {
      NavigatorColumns columnsInSettings = new NavigatorColumns(category1, type1, suffix1);
      columnsInSettings.Assign((object) columnsSettingsForRole1);
      return columnsInSettings;
    }
    for (DBSimpleObject archiveParentFromCache = this._archiveHierarchyService.GetArchiveParentFromCache(this._archiveObjectId); archiveParentFromCache.ObjectID != 0L; archiveParentFromCache = this._archiveHierarchyService.GetArchiveParentFromCache(archiveParentFromCache.ObjectID))
    {
      string suffix2 = Constants.ArchiveStateStreamPrefix + (object) Math.Abs(archiveParentFromCache.ObjectID);
      int objectTypeId = archiveParentFromCache.ObjectTypeID;
      NavigatorColumns navigatorColumns2 = this._navigatorColumnsService.GetNavigatorColumns(category1, objectTypeId, suffix2, false);
      if (navigatorColumns2 != null)
        return navigatorColumns2;
      NodeColumnCollection columnsSettingsForRole2 = this._archiveColumnsSettingsCache.GetArchiveColumnsSettingsForRole(archiveParentFromCache.ObjectID, this._userRoleId);
      if (columnsSettingsForRole2 != null)
      {
        NavigatorColumns columnsInSettings = new NavigatorColumns(category1, objectTypeId, suffix2);
        columnsInSettings.Assign((object) columnsSettingsForRole2);
        return columnsInSettings;
      }
    }
    return this.GetNavigatorColumnsForArchivesByDefault();
  }

  /// <summary>
  /// Метод возвращает настройки колонок по умолчанию для узла Архивы и документы
  /// </summary>
  /// <returns></returns>
  private NavigatorColumns GetNavigatorColumnsForArchivesByDefault()
  {
    string suffix = "";
    int type = 0;
    return this._navigatorColumnsService.GetNavigatorColumns(Consts.CategoryArchivesNode, type, suffix, false);
  }

  /// <summary>
  /// 
  /// </summary>
  public override string StateStreamPrefix
  {
    get
    {
      INode parentNode = this._parentNode;
      return parentNode != null && parentNode.GetData(this._path.LastID, typeof (IDBObjectID)) is IDBObjectID data ? Constants.ArchiveStateStreamPrefix + Math.Abs(data.Value).ToString() : base.StateStreamPrefix;
    }
    set => base.StateStreamPrefix = value;
  }

  protected override int StateStreamCategoryType
  {
    get
    {
      return this._parentNode.GetData(this._nodeID, typeof (IDBTypedObjectID)) is IDBTypedObjectID data ? data.ObjectType : base.StateStreamCategoryType;
    }
  }

  /// <summary>Загрузить настройки грида</summary>
  /// <param name="stateStream">Поток, из которого требуется загрузить состояние грида,
  /// или null, если грузить из потока по умолчанию</param>
  public override void GridLoadState(Stream stateStream)
  {
    bool flag = true;
    List<int> groups = new List<int>();
    NavigatorColumns navigatorColumns = this.FindNavigatorColumnsInSettings(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix) ?? this._navigatorColumnsService.GetNavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix, true);
    NodeColumnCollection columns1 = (NodeColumnCollection) null;
    try
    {
      if (navigatorColumns == null || navigatorColumns.Empty)
        return;
      columns1 = navigatorColumns.Columns.Clone() as NodeColumnCollection;
      if (navigatorColumns.Groups != null)
        groups = new List<int>((IEnumerable<int>) navigatorColumns.Groups);
      flag = false;
    }
    finally
    {
      if (flag)
      {
        columns1 = this.Node.GetDefaultColumns(this.ViewContentType);
        if (columns1 == null || columns1.Count == 0)
        {
          NodeColumnCollection columns2 = new NodeColumnCollection();
          Helper.AddObligatoryColumns(columns2, true, false);
          columns1 = columns2;
        }
        groups.Clear();
      }
      this.GridSetColumns(columns1, false);
      this.GridSetGroups(columns1, groups, false);
    }
  }

  /// <summary>Сохраним состояние грида</summary>
  /// <param name="stateStream">Поток, в который надо сохранять состояние. Если указать null,
  /// грид сохранится в свой стандартный поток</param>
  public override void GridSaveState(Stream stateStream, NodeColumnCollection nodeColumns = null)
  {
    NavigatorColumns navigatorColumns1 = this.FindNavigatorColumnsInSettings(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix) ?? this._navigatorColumnsService.GetNavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix, true);
    if (navigatorColumns1 == null)
    {
      navigatorColumns1 = new NavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix);
      navigatorColumns1.Columns = this.Node != null ? this.Node.GetDefaultColumns(this.ViewContentType) : (NodeColumnCollection) null;
      navigatorColumns1.Groups = new List<int>();
    }
    NavigatorColumns columns = new NavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix)
    {
      Columns = this.GetNodeColumns()
    };
    columns.Groups = this.GridGetGroupColumns(columns.Columns);
    NavigatorColumns navigatorColumns2 = navigatorColumns1.Clone() as NavigatorColumns;
    navigatorColumns2.Inherited = false;
    navigatorColumns2.Category = columns.Category;
    navigatorColumns2.Type = columns.Type;
    navigatorColumns2.Suffix = columns.Suffix;
    if (columns.Equals((object) navigatorColumns2))
      return;
    this._navigatorColumnsService.CreateNavigatorColumns(columns);
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocumsObject));
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.GrayText;
    this._grid.GroupBox.HintBackColor = SystemColors.GrayText;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    componentResourceManager.ApplyResources((object) this._grid, "grid");
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (DocumsObject);
    this.Tag = (object) " ";
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  protected override INode GetNode()
  {
    INode node = base.GetNode();
    if (node != null && node is IContextAware)
    {
      IContextAware contextAware = (IContextAware) node;
      if (contextAware.Services is ServiceContainer)
      {
        ServiceContainer services = (ServiceContainer) contextAware.Services;
        if (this._hasViewArchives)
        {
          if (services.GetService(typeof (ViewArchives)) == null)
            services.AddService(typeof (ViewArchives), (object) new ViewArchives());
        }
        else if (services.GetService(typeof (ViewArchives)) != null)
          services.RemoveService(typeof (ViewArchives));
      }
    }
    return node;
  }
}
