
// Type: Intermech.Navigator.Controls.NavigatorTreeView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.Navigator.Controls;
using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Notifications;
using Intermech.Navigator.Parts;
using Intermech.PropertyEditors;
using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.Search.NavigatorTreeViewEditing;
using Intermech.Search.ObjectGroups;
using Intermech.Search.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Navigator.Controls;

/// <summary>Дерево "Навигатора"</summary>
public class NavigatorTreeView : 
  Infralution.Controls.VirtualTree.VirtualTree,
  ISupportInitialize,
  IContextAware,
  ISelectedItemsHost,
  ISelectedItemsModeHost,
  IIOSource,
  ICommandsProvider,
  ICommandTarget
{
  /// <summary>
  /// Обработчик, позволяющий менять стиль текста узла перед выводом. НЕ СОБЫТИЕ, т.е. допускается только один!
  /// </summary>
  /// <remarks>Добавлено Лембиевским О.</remarks>
  [Browsable(false)]
  public BeforePaintTextEventHandler BeforePaintText;
  /// <summary>
  /// Обработчик, позволяющий подменить значение CheckState перед установкой его у NavigatorTreeNode. НЕ СОБЫТИЕ, т.е. допускается только один!
  /// </summary>
  /// <remarks>Добавлено Лембиевским О.</remarks>
  [Browsable(false)]
  public BeforeSetCheckStateEventHandler BeforeSetCheckState;
  /// <summary>
  /// Строка, на которую "сваливаются" перетаскиваемые объекты
  /// </summary>
  private Row _dropTargetRow;
  protected NavigatorTreeViewSelectedItem _dragdropItem;
  private Row _previousFocusedRow;
  private Row _previousTopRow;
  private bool _disableFocusNodeAfterAdded;
  private static readonly Regex NewLineRegex = new Regex("[\n\r]", RegexOptions.Compiled);
  /// <summary>Максимальное количество фоновых задач</summary>
  private const int UpdateJobLimit = 1;
  /// <summary>Метка фоновой задачи</summary>
  private const string UpdateJobMarker = "UpdateJob";
  private ICommandsProvider _commandsProvider;
  /// <summary>Запретить сортировку в колонках дерева "Навигатора"</summary>
  private bool _disableColumnsSorting;
  /// <summary>Запретить перемещение колонок в дереве "Навигатора"</summary>
  private bool _disableColumnsMoving;
  /// <summary>
  /// Запретить изменение ширины колонок в дереве "Навигатора"
  /// </summary>
  private bool _disableColumnsSizing;
  /// <summary>Запретить пакетное чтение в дереве</summary>
  private bool _disablePacketsReading = true;
  /// <summary>
  /// Запретить показ колонки с информацией о взятом на изменение объекте
  /// </summary>
  private bool _disableCheckedOutColumn;
  /// <summary>Вспомогательный класс для контекстных меню</summary>
  private NavigatorTreeViewContextMenuHelper _contextMenuHelper;
  /// <summary>Коллекция колонок дерева "Навигатора"</summary>
  internal NodeColumnCollection _treeColumns;
  /// <summary>Контейнер сервисов, используемых в дереве</summary>
  protected AdvancedServiceContainer _services;
  /// <summary>Коллекция поддерживаемых колонок дерева "Навигатора"</summary>
  protected NodeColumnCollection _supportedColumns;
  /// <summary>Коллекция выделенных в дереве элементов</summary>
  private NavigatorTreeViewSelectedItems _selectedItems;
  /// <summary>Коллекция отмеченных элементов</summary>
  private ISelectedItems _checkedItems;
  /// <summary>Корневой узел в дереве "Навигатора"</summary>
  protected NavigatorTreeNode _rootNode;
  /// <summary>Служба уведомлений</summary>
  private INotificationService _notificationService;
  /// <summary>Диспетчер событий</summary>
  private IIODispatcher _ioDispatcher;
  /// <summary>Текущий пользователь, его роль</summary>
  internal ICurrentUserAndRole _currentUserAndRole;
  /// <summary>Состояние вьюшки</summary>
  private IViewState _viewState;
  /// <summary>Кэш графических объектов "Навигатора"</summary>
  private INavGraphicsCache _navGraphicsCache;
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService _categoryTypeIconService;
  /// <summary>Прямоугольник, в котором "всё началось"</summary>
  private Rectangle _dragBoxFromMouseDown;
  /// <summary>Смещение</summary>
  private Point _screenOffset;
  /// <summary>
  /// Запрет на обработку событий от дерева "Навигатора" во время перестраивания дерева
  /// </summary>
  protected bool _disableTreeEvents;
  /// <summary>Заблокировать ручную сортировку</summary>
  private int _lockManualSortEvent;
  /// <summary>
  /// Заблокировать события при смене сфокусированного элемента
  /// </summary>
  protected int _lockFocusedItemEvent;
  /// <summary>Заблокировать событие очистки дерева</summary>
  protected int _lockClearTreeEvent;
  /// <summary>Заблокировать события при смене выделенногоs элемента</summary>
  protected int _lockSelectionChanged;
  /// <summary>Менеджер фоновых задач</summary>
  private IJobManager _jobManager;
  /// <summary>Очередь фоновых задач</summary>
  private Queue _jobQueue;
  /// <summary>Менеджер записей состояния</summary>
  private StatesRecordManager _statesManager = new StatesRecordManager();
  /// <summary>Таймер для обновления очередей</summary>
  private System.Windows.Forms.Timer _queueUpdatesTimer;
  /// <summary>Таймер для обновления изменений</summary>
  private System.Windows.Forms.Timer _applyUpdatesTimer;
  /// <summary>
  /// Таймер для отслеживания изменения верхней строки в дереве "Навигатора"
  /// </summary>
  private System.Windows.Forms.Timer _topRowMonitorTimer;
  /// <summary>Вид чек-боксов в дереве</summary>
  protected NavigatorTreeViewCheckBoxStyle _checkBoxesStyle;
  /// <summary>Индекс верхней строки дерева "Навигатора"</summary>
  private int _topRowIndex;
  /// <summary>Get supported column's handler</summary>
  protected GetSupportedColumnsEventHandler _onGetSupportedColumnsEventHandler;
  private bool _mouseDownOnHeader;
  private CommandsTable _currentCommandsTable;
  private System.IServiceProvider _currentContextMenuServiceProvider;
  /// <summary>Предыдущий узел в дереве Навигатора</summary>
  private NavigatorTreeNode _previousNode;
  /// <summary>
  /// Класс для проверки выделенных элементов на возможность выполнения определённых команд
  /// </summary>
  internal CheckInOutCommandsProvider _checkInOutCommandsProvider;
  /// <summary>Для сортировки видимых узлов</summary>
  internal VisibleNodesComparer _nodesComparer = new VisibleNodesComparer();
  /// <summary>
  /// Значение true - быстрое обновление, но с глюками. Используется только
  /// для пакетного разворачивания узлов
  /// </summary>
  protected internal volatile bool SearchModePopulating;
  private CorrectNodeExpansionNavigatorTreeViewExtension _correctNodeExpansionNavigatorTreeViewExtension;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ContextMenuStrip _headerContextMenuStrip;
  private ToolStripMenuItem _changeColumnsToolStripMenuItem;
  private NavigatorTreeViewEditingComponent _navigatorTreeViewEditingComponent;

  /// <summary>Коллекция узлов в дереве</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NavigatorTreeNodes Nodes
  {
    get => this._rootNode == null ? (NavigatorTreeNodes) null : this._rootNode.Children;
  }

  /// <summary>Сфокусированный узел в дереве</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NavigatorTreeNode FocusedNode
  {
    get
    {
      Row row = this.FocusRow ?? this.SelectedRow;
      return row == null ? (NavigatorTreeNode) null : row.Item as NavigatorTreeNode;
    }
    set
    {
      if (value != null && value.Tree == this)
      {
        NavigatorTreeNode node = value;
        if (node.Handle != null && node.Handle.Disposed && node.Parent != null)
          node.Parent.Children.RebuildHandles();
        node.EnsureVisible();
        if (node.Handle == null)
          return;
        if (this.SelectedRow != node.Handle)
        {
          this.SelectedRow = node.Handle;
          this.RaiseSelectedItemsChanged();
          if (this.FocusRow == node.Handle)
            this.RaiseAfterFocusNode(node);
        }
        if (this.FocusRow == node.Handle)
          return;
        this.FocusRow = node.Handle;
        this.RaiseAfterFocusNode(node);
      }
      else
      {
        this.SelectedRow = (Row) null;
        this.RaiseSelectedItemsChanged();
        this.FocusRow = (Row) null;
        this.RaiseAfterFocusNode(value);
      }
    }
  }

  /// <summary>Список выделенных узлов</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual NavigatorTreeNode[] SelectedNodes
  {
    get
    {
      List<NavigatorTreeNode> navigatorTreeNodeList = new List<NavigatorTreeNode>();
      for (int index = 0; index < this.SelectedRows.Count; ++index)
      {
        if (this.SelectedRows[index].Item is NavigatorTreeNode navigatorTreeNode && navigatorTreeNode.NodeID != null && navigatorTreeNode.InTree)
          navigatorTreeNodeList.Add(navigatorTreeNode);
      }
      return navigatorTreeNodeList.ToArray();
    }
  }

  /// <summary>Список отмеченных узлов</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual NavigatorTreeNode[] CheckedNodes
  {
    get
    {
      return this.CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.None && this._rootNode != null ? this._rootNode.GetDescendants().Where<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (o => o.CheckState == CheckState.Checked)).ToArray<NavigatorTreeNode>() : new NavigatorTreeNode[0];
    }
  }

  /// <summary>Коллекция отмеченных элементов</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ISelectedItems CheckedItems
  {
    get => (ISelectedItems) new NavigatorTreeViewSelectedItems(this, this.CheckedNodes);
  }

  /// <summary>Вид чек-боксов в дереве</summary>
  [Category("Appearance")]
  [DefaultValue(NavigatorTreeViewCheckBoxStyle.None)]
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_103")]
  public NavigatorTreeViewCheckBoxStyle CheckBoxStyle
  {
    [DebuggerStepThrough] get => this._checkBoxesStyle;
    set
    {
      this._checkBoxesStyle = value;
      this.RebuildTree();
    }
  }

  /// <summary>
  /// Запретить появляться контекстному меню IMClient-а (если компоненту в дизайнере ручками назначается другое меню)
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_107")]
  public bool DisableIMContextMenu { get; set; }

  /// <summary>Запретить в дереве Drag and Drop</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_286")]
  public bool DisableDragAndDrop { get; set; }

  /// <summary>Запретить сортировку в колонках дерева "Навигатора"</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_108")]
  public bool DisableColumnsSorting
  {
    [DebuggerStepThrough] get => this._disableColumnsSorting;
    set
    {
      this._disableColumnsSorting = value;
      this.SetColumns(this.ReflectTreeColumsChanges(), false);
    }
  }

  /// <summary>Запретить перемещение колонок в дереве "Навигатора"</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_109")]
  public bool DisableColumnsMoving
  {
    [DebuggerStepThrough] get => this._disableColumnsMoving;
    set
    {
      this._disableColumnsMoving = value;
      this.SetColumns(this.ReflectTreeColumsChanges(), false);
    }
  }

  /// <summary>
  /// Запретить изменение ширины колонок в дереве "Навигатора"
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_110")]
  public bool DisableColumnsSizing
  {
    [DebuggerStepThrough] get => this._disableColumnsSizing;
    set
    {
      this._disableColumnsSizing = value;
      this.SetColumns(this.ReflectTreeColumsChanges(), false);
    }
  }

  /// <summary>
  /// Значение true позволяет одновременно выбирать несколько элементов в дереве "Навигатора"
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_111")]
  public bool MultiSelect
  {
    [DebuggerStepThrough] get => this.AllowMultiSelect;
    set => this.AllowMultiSelect = value;
  }

  /// <summary>
  /// Запретить генерацию событий IIOEvent типа "IOEventTypes.evKeyDown"
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_114")]
  public bool DisableKeyDownEvents { get; set; }

  /// <summary>
  /// Запретить генерацию событий IIOEvent типа "IOEventTypes.evKeyUp"
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_115")]
  public bool DisableKeyUpEvents { get; set; }

  /// <summary>Запретить пакетное чтение в дереве</summary>
  [Category("Appearance")]
  [DefaultValue(true)]
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_116")]
  public virtual bool DisablePacketsReading
  {
    [DebuggerStepThrough] get => this._disablePacketsReading;
    set => this._disablePacketsReading = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool DisableChangeSelectedNodeDuringNotificationProcessing { get; set; }

  /// <summary>
  /// Запретить показ колонки с информацией о взятом на изменение объекте
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  public bool DisableCheckedOutColumn
  {
    [DebuggerStepThrough] get => this._disableCheckedOutColumn || !UISettings.ShowTreeChkoutColumn;
    set
    {
      this._disableCheckedOutColumn = value;
      this.Update();
    }
  }

  /// <summary>
  /// Является ли дерево сфокусированным контролом (может ли обрабатывать события от ICommandManager)
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool TreeFocused { get; private set; }

  /// <summary>Включен ли режим ручной сортировки в дереве</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ManualSort
  {
    get
    {
      for (int index = 0; index < this._treeColumns.Count; ++index)
      {
        if (this._treeColumns[index].SortOrder != NodeColumnSortOrder.None)
          return false;
      }
      return true;
    }
  }

  /// <summary>Корневой узел в дереве Навигатора</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NavigatorTreeNode RootNode
  {
    [DebuggerStepThrough] get
    {
      return this._rootNode == null || this._rootNode.Children.Count <= 0 ? (NavigatorTreeNode) null : this._rootNode.Children[0];
    }
  }

  /// <summary>
  /// Дескриптор, описывающий корневой элемент дерева "Навигатора"
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IDescriptor RootDescriptor { get; protected set; }

  /// <summary>Описание корневого узла дерева "Навигатора"</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INodeID RootNodeID => this.RootNode != null ? this.RootNode.NodeID : (INodeID) null;

  /// <summary>Сфокусированный элемент</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IFocusedItem FocusedItem
  {
    get => this.FocusedNode != null ? this.GetFocusedItem(this.FocusedNode) : (IFocusedItem) null;
  }

  /// <summary>
  /// Интерфейс элемента навигации, с помощью которого можно выполнять операции над корнем дерева навигации
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INode RootHandler { get; protected set; }

  /// <summary>Коллекция колонок в дереве "Навигатора"</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NodeColumnCollection TreeColumns
  {
    [DebuggerStepThrough] get => this.GetColumns();
    set => this.SetColumns(value);
  }

  /// <summary>
  /// Коллекция поддерживаемых колонок в дереве "Навигатора"
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual NodeColumnCollection SupportedColumns
  {
    get
    {
      if (this._supportedColumns == null)
        this._supportedColumns = this._onGetSupportedColumnsEventHandler == null ? Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending) : this._onGetSupportedColumnsEventHandler((object) this);
      return this._supportedColumns;
    }
    set
    {
      if (value == null || value.Count <= 0)
        return;
      this._supportedColumns = value;
    }
  }

  /// <summary>Путь к сфокусированному узлу в дереве</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NodeIDPath FocusedPath
  {
    get
    {
      return this.FocusedNode != null ? this.GetNodeIDPath(this.FocusedNode) : this.GetNodeIDPath(this.RootNode);
    }
  }

  /// <summary>Строка с адресом сфокусированного узла</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string FocusedAddress
  {
    get => this.FocusedNode != null ? this.GetNodeAddress(this.FocusedNode) : string.Empty;
  }

  /// <summary>
  /// В режиме чекбоксов CheckBoxesStyle.ThreeState не убирать отметку с родительских узлов при снятии отметок со всех дочерних
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool AllowCheckParentWithoutChildren { get; set; }

  /// <summary>
  /// Обработчик события, запрашивающего список поддерживаемых колонок для дерева
  /// !!! Подписываемся до вызова метода SetColumns !!!!
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_117")]
  public event GetSupportedColumnsEventHandler OnGetSupportedColumnsEventHandler
  {
    add
    {
      if (value.Method == (MethodInfo) null)
        throw new ArgumentException();
      if (!NavigatorTreeView.TestMethodIsSerializable(value.Method))
        throw new ArgumentException("Провайдер допустимых колонок навигатора должен быть реализован как открытый метод открытого класса");
      this._onGetSupportedColumnsEventHandler += value;
    }
    remove => this._onGetSupportedColumnsEventHandler -= value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool OnGetSupportedColumnsEventHandlerAssigned
  {
    [DebuggerStepThrough] get => this._onGetSupportedColumnsEventHandler != null;
  }

  /// <summary>
  /// Событие вызывается перед выполнением сортировки в дереве
  /// </summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_118")]
  public event EventHandler BeforeColumnsSorting;

  /// <summary>Событие вызывается после построения дерева</summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_119")]
  public event EventHandler BuildTree;

  /// <summary>Событие вызывается после очистки дерева</summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_120")]
  public event EventHandler ClearTree;

  /// <summary>
  /// Событие вызывается после создания нового узла в дереве
  /// </summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_121")]
  public event EventHandler<NodeEventArgs> AfterCreateNode;

  /// <summary>Событие вызывается после раскрытия узла</summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_123")]
  public event EventHandler<NodeEventArgs> AfterExpand;

  /// <summary>Событие вызывается после сворачивания узла</summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_125")]
  public event EventHandler<NodeEventArgs> AfterCollapse;

  /// <summary>
  /// Событие вызывается перед фокусированием на очередном узле
  /// </summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_126")]
  public event EventHandler<NavigatorTreeNodeEventArgs> BeforeFocusNode;

  /// <summary>
  /// Событие вызывается после фокусирования на очередном узле
  /// </summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_127")]
  public event EventHandler<NavigatorTreeNodeEventArgs> AfterFocusNode;

  /// <summary>
  /// Событие вызывается во время изменения состояния узла (CheckState)
  /// </summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_128")]
  public event EventHandler<CheckStateEventArgs> CheckStateChanging;

  /// <summary>
  /// Событие вызывается после завершения изменения состояния узла (CheckState)
  /// </summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_129")]
  public event EventHandler<NodeEventArgs> CheckStateChanged;

  /// <summary>Событие вызывается после замены корневого узла дерева</summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_130")]
  public event EventHandler RootNodeReplaced;

  /// <summary>
  /// Событие вызывается после модификации корневого узла дерева
  /// </summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_131")]
  public event EventHandler RootNodeModified;

  /// <summary>
  /// Событие вызывается после модификации корневого узла дерева
  /// </summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_132")]
  public event MouseEventHandler ShowContextMenu;

  /// <summary>Событие вызывается перед началом рекурсивной обработки клика по чекбоксу ноды</summary>
  public event EventHandler<NavigatorTreeNodeEventArgs> BeforeSetCheckedPacket;

  public void FireBeforeSetCheckedPacket(NavigatorTreeNode node)
  {
    if (this.BeforeSetCheckedPacket == null)
      return;
    this.BeforeSetCheckedPacket((object) this, new NavigatorTreeNodeEventArgs(node));
  }

  /// <summary>Событие вызывается перед началом по завершении рекурсивной обработки клика по чекбоксу ноды</summary>
  public event EventHandler<NavigatorTreeNodeEventArgs> AfterSetCheckedPacket;

  public void FireAfterSetCheckedPacket(NavigatorTreeNode node)
  {
    if (this.AfterSetCheckedPacket == null)
      return;
    this.AfterSetCheckedPacket((object) this, new NavigatorTreeNodeEventArgs(node));
  }

  /// <summary>Событие вызывается после фоновой зачитки дочерних узлов</summary>
  /// <remarks>Добавлено Лембиевским О.</remarks>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_285")]
  public event PlusJobCompletedEventHandler PlusJobCompleted;

  /// <summary>Вызов события PlusJobCompleted</summary>
  protected virtual void FirePlusJobCompleted(NavigatorTreeNode node)
  {
    if (this.PlusJobCompleted != null)
      this.PlusJobCompleted(node);
    this.FireAfterNodeChildsLoaded(node);
  }

  /// <summary>Событие вызывается после первой загрузки состава ноды (PopulateNode)</summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_284")]
  public event EventHandler<NodeEventArgs> AfterPopulateNode;

  /// <summary>Событие вызывается после первой загрузки списка дочерних нод у ноды. В отличии от AfterPopulateNode вызывается не только после ручной загрузки (пользователь нажал на (+) у ноды), но при фоновой загрузке</summary>
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_293")]
  public event NavTreeeNodeEventHandler AfterNodeChildsLoaded;

  /// <summary>Вызов события AfterNodeChildsLoaded</summary>
  protected virtual void FireAfterNodeChildsLoaded(NavigatorTreeNode node)
  {
    if (this.AfterNodeChildsLoaded == null)
      return;
    this.AfterNodeChildsLoaded(node);
  }

  public NavigatorTreeView()
  {
    this.InitializeComponent();
    if (Holder.NamedImageList != null)
      this._changeColumnsToolStripMenuItem.Image = Holder.NamedImageList.ImageList.Images[Holder.NamedImageList.ImageIndex("imgViewSettings")];
    this._changeColumnsToolStripMenuItem.Click += new EventHandler(this.ChangeColumnsToolStripMenuItem_Click);
    this.InitTreeResources();
    this.InitTreeServices();
    this.InitializeStyles();
    this.InitEventHandlers();
    this.InitializeJobSystem();
    this.ApplySettings();
    this._navigatorTreeViewEditingComponent.Control = (Control) this;
    this._navigatorTreeViewEditingComponent.AttributePropertyDescriberService = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService;
    this._navigatorTreeViewEditingComponent.NotificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this._correctNodeExpansionNavigatorTreeViewExtension = new CorrectNodeExpansionNavigatorTreeViewExtension(this);
  }

  /// <summary>
  /// Создать дерево "Навигатора", задать ему определённый контекст (контейнер сервисов)
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  public NavigatorTreeView(System.IServiceProvider services)
    : this()
  {
    this._services.AdvancedProvider = services;
  }

  /// <summary>
  /// Создать дерево "Навигатора", задать ему определённый контекст (контейнер сервисов), а также набор колонок
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="columns">Набор колонок "Навигатора", который будет отображаться в дереве</param>
  public NavigatorTreeView(System.IServiceProvider services, NodeColumnCollection columns)
    : this(services)
  {
    this.TreeColumns = columns;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool EditingMode
  {
    get => this._navigatorTreeViewEditingComponent.Enabled;
    set
    {
      this._navigatorTreeViewEditingComponent.Enabled = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool SupportedEditing => this.RootDescriptor != null && this.RootDescriptor is Intermech.Navigator.DBObjects.Descriptor;

  public ICommandsProvider CommandsProvider
  {
    get
    {
      if (this._commandsProvider == null)
        this._commandsProvider = this.CreateCommandProvider();
      return this._commandsProvider;
    }
    set
    {
      if (this._commandsProvider == value)
        return;
      this._commandsProvider = value;
    }
  }

  protected virtual ICommandsProvider CreateCommandProvider()
  {
    return (ICommandsProvider) new NavigatorTreeViewCommandsProvider(this);
  }

  /// <summary>Инициализировать ресурсы дерева</summary>
  protected virtual void InitTreeResources()
  {
    this.RootDescriptor = (IDescriptor) null;
    this.RootHandler = (INode) null;
    this._rootNode = new NavigatorTreeNode(this, (NavigatorTreeNode) null, (INodeID) null);
    this._selectedItems = new NavigatorTreeViewSelectedItems(this, this.SelectedNodes);
    this._dragdropItem = new NavigatorTreeViewSelectedItem(this, (NavigatorTreeNode) null);
    this._checkedItems = (ISelectedItems) new NavigatorTreeViewSelectedItems(this, this.CheckedNodes);
    this._queueUpdatesTimer = new System.Windows.Forms.Timer(this.components);
    this._applyUpdatesTimer = new System.Windows.Forms.Timer(this.components);
    this._topRowMonitorTimer = new System.Windows.Forms.Timer(this.components);
    this._queueUpdatesTimer.Tick += new EventHandler(this.QueueUpdateJobs);
    this._applyUpdatesTimer.Interval = 50;
    this._applyUpdatesTimer.Tick += new EventHandler(this.UpdateTreeView);
    this._topRowMonitorTimer.Interval = 200;
    this._topRowMonitorTimer.Tick += new EventHandler(this.TopRowMonitor);
  }

  /// <summary>
  /// Выполнить перестройку содержимого дерева (если есть изменения в коллекции Nodes)
  /// </summary>
  public virtual void RebuildTree()
  {
    this.UpdateRows(true);
    this.UpdateRowData();
    this.ApplySettings();
  }

  /// <summary>
  /// Возвращает полный адрес указанного узла дерева, который предназначен для вывода в строке адреса
  /// </summary>
  /// <param name="node">Узел, для которого требуется получить адрес</param>
  /// <returns>Адрес узла</returns>
  public string GetNodeAddress(NavigatorTreeNode node)
  {
    if (node == null || node.Parent == null)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    INodeID nodeId1 = node?.NodeID;
    INode nodeHandler1 = this.GetNodeHandler(node);
    if (nodeHandler1 != null && nodeId1 != null)
    {
      stringBuilder.Append(nodeHandler1.GetAddress(nodeId1));
      for (node = node.Parent; node.Parent != null; node = node.Parent)
      {
        INode nodeHandler2 = this.GetNodeHandler(node);
        INodeID nodeId2 = node?.NodeID;
        if (nodeHandler2 != null && nodeId2 != null)
        {
          stringBuilder.Insert(0, '\\');
          stringBuilder.Insert(0, nodeHandler2.GetAddress(nodeId2));
        }
        else
          break;
      }
    }
    return stringBuilder.ToString();
  }

  /// <summary>Очистить внутренние структуры дерева</summary>
  /// <param name="preserveEthereal">Сохранить описание корневого узла</param>
  protected internal void ClearCore(bool preserveEthereal)
  {
    this.CancelUpdateJobs(false);
    if (this._rootNode != null)
      this._rootNode.ClearChildren();
    if (preserveEthereal)
      return;
    this.RootDescriptor = (IDescriptor) null;
    this.RootHandler = (INode) null;
  }

  /// <summary>Построить дерево на основе указанного дескриптора</summary>
  /// <param name="rootDescriptor">Описание корневого узла дерева</param>
  public void Build(IDescriptor rootDescriptor)
  {
    this.BuildWithPath(rootDescriptor, (NodeIDPath) null);
  }

  /// <summary>Построить дерево на основе указанного пути</summary>
  /// <param name="path">Путь</param>
  public virtual void Build(NodeIDPath path) => this.BuildWithPath(path.RootDescriptor, path);

  /// <summary>Построить дерево на основе указанного дескриптора</summary>
  /// <param name="descriptor">Описание корневого узла дерева</param>
  protected virtual void BuildCore(IDescriptor descriptor)
  {
    this.ClearCore(false);
    this.RootDescriptor = descriptor;
    this.RootHandler = (INode) new EtherealNode(this.RootDescriptor);
    this.CreateRootNode();
    this.RebuildTree();
    if (this.RootNode == null)
      return;
    this.RootNode.FocusThenExpand();
  }

  /// <summary>
  /// Построить дерево "Навигатора" на основе указанного дексриптора и пути
  /// </summary>
  /// <param name="descriptor">Описание корневого узла дерева</param>
  /// <param name="path">Путь</param>
  public virtual void BuildWithPath(IDescriptor descriptor, NodeIDPath path)
  {
    this.BuildCore(descriptor);
    this.RaiseBuildTreeEvent();
    if (path == null)
      return;
    this.TryBrowse(path);
    if (this.FocusedNode == null)
      return;
    this.FocusedNode = this.FocusedNode;
    this.RaiseAfterFocusNode(this.FocusedNode);
    this.FocusedNode.Expand();
  }

  /// <summary>
  /// Вернуть копию коллекции колонок, отображаемых в данный момент в дереве "Навигатора"
  /// </summary>
  /// <returns>Копия коллекции колонок, отображаемых в данный момент в дереве "Навигатора"</returns>
  public NodeColumnCollection GetColumns()
  {
    if (this._treeColumns == null)
      this._treeColumns = new NodeColumnCollection();
    return this._treeColumns;
  }

  /// <summary>
  /// Установить новую коллекцию колонок в дерево "Навигатора"
  /// </summary>
  /// <param name="nodeColumnCollection">Новая коллекция колонок для дерева "Навигатора"</param>
  public virtual void SetColumns(
    NodeColumnCollection nodeColumnCollection,
    bool equalsWithExistingColumns = true)
  {
    NodeColumn navigatorColumn1 = this.SortColumn is NavigatorTreeColumn sortColumn1 ? sortColumn1.NavigatorColumn : (NodeColumn) null;
    if (nodeColumnCollection == null || nodeColumnCollection.Count == 0)
      nodeColumnCollection = Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending);
    nodeColumnCollection.RemoveInvalidColumns();
    if (equalsWithExistingColumns)
    {
      NodeColumnCollection firstNodeColumnCollection = this.ReflectTreeColumsChanges();
      if (firstNodeColumnCollection != null && (firstNodeColumnCollection.Count == 1 && nodeColumnCollection.Count == 1 && NodeColumnCollection.EqualsWithNoWidth(firstNodeColumnCollection, nodeColumnCollection) || NodeColumnCollection.Equals(firstNodeColumnCollection, nodeColumnCollection)))
        return;
    }
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    for (int index = 0; index < this.Columns.Count; ++index)
    {
      if (this.Columns[index] is NavigatorTreeColumn column)
        dictionary.Add(column.NavigatorColumn.Key, column.AbsoluteIndex);
    }
    NodeIDPath focusedPath = this.FocusedPath;
    try
    {
      this.CancelUpdateJobs((object) "UpdateJob", true);
      ++this._lockManualSortEvent;
      this.Columns.Clear();
      if (this._treeColumns != null)
        this._treeColumns.Clear();
      if (nodeColumnCollection == null || nodeColumnCollection.Count == 0)
        return;
      this._treeColumns = nodeColumnCollection;
      for (int index = 0; index < this._treeColumns.Count; ++index)
      {
        if (this._treeColumns[index].SortOrder == NodeColumnSortOrder.None)
          this._treeColumns[index].SortIndex = -1;
        this.AddColumn(this._treeColumns[index], this._treeColumns).AbsoluteIndex = index;
      }
    }
    finally
    {
      NodeColumnCollection sortedColumns = NodeColumnCollection.GetSortedColumns(this._treeColumns);
      if (sortedColumns != null && sortedColumns.Count > 0)
        this.SortColumn = (Column) this.GetColumn(sortedColumns[0]);
      else
        this.SortColumn = (Column) null;
      NodeColumn navigatorColumn2 = this.SortColumn is NavigatorTreeColumn sortColumn2 ? sortColumn2.NavigatorColumn : (NodeColumn) null;
      int num = navigatorColumn1 != navigatorColumn2 ? 1 : (navigatorColumn1 == null || navigatorColumn2 == null ? 0 : (!navigatorColumn1.Equals((object) navigatorColumn2) ? 1 : 0));
      if (this.Nodes.Count > 0)
        this.SortTree();
      else
        this.RebuildTree();
      this.TryBrowse(focusedPath);
      --this._lockManualSortEvent;
      if (this.RootDescriptor != null)
        this.QueueUpdateJobs(false);
    }
  }

  /// <summary>
  /// Добавить в коллекцию колонок дерева очередную колонку "Навигатора"
  /// </summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <param name="columns">Вся коллекция колонок</param>
  /// <returns>Вновь добавленная или найденная колонка из дерева</returns>
  protected virtual NavigatorTreeColumn AddColumn(NodeColumn column, NodeColumnCollection columns)
  {
    return this.GetColumn(column) ?? new NavigatorTreeColumn(this, column, columns);
  }

  /// <summary>
  /// Возвращает колонку из дерева, соответствующую указанной колонке "Навигатора"
  /// </summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Колонка грида или null</returns>
  public NavigatorTreeColumn GetColumn(NodeColumn column)
  {
    return column == null ? (NavigatorTreeColumn) null : this.Columns[column.Key] as NavigatorTreeColumn;
  }

  /// <summary>
  /// Вернуть узел XML, в котором будет сохранён сериализованный делегат для получения списка поддерживаемых колонок
  /// </summary>
  /// <param name="xmlDoc">Документ XML</param>
  /// <returns>Узел XML, в котором будет сохранён сериализованный делегат для получения списка поддерживаемых колонок</returns>
  public XmlNode GetSupportedColumnsNode(XmlDocument xmlDoc)
  {
    XmlNode element = (XmlNode) xmlDoc.CreateElement("SupportedColumns");
    if (this._onGetSupportedColumnsEventHandler == null)
      return element;
    using (MemoryStream serializationStream = new MemoryStream())
    {
      try
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) this._onGetSupportedColumnsEventHandler);
        element.InnerText = Convert.ToBase64String(serializationStream.ToArray());
      }
      catch
      {
      }
    }
    return element;
  }

  /// <summary>
  /// Восстановить из указанного узла делегат для получения коллекции поддерживаемых колонок
  /// </summary>
  /// <param name="settingsNode">Узел с сериализованным делегатом для получения коллекции поддерживаемых колонок</param>
  public void RestoreFromSupportedColumnsNode(XmlNode settingsNode)
  {
    XmlNode xmlNode = settingsNode.SelectSingleNode("SupportedColumns");
    if (xmlNode != null)
    {
      if (xmlNode.InnerText != string.Empty)
      {
        try
        {
          using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(xmlNode.InnerText)))
            this._onGetSupportedColumnsEventHandler = new BinaryFormatter().Deserialize((Stream) serializationStream) as GetSupportedColumnsEventHandler;
        }
        catch
        {
          this.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(Intermech.Navigator.Utils.GetNavigatorColumns);
        }
      }
    }
    if (this._onGetSupportedColumnsEventHandler != null)
      return;
    this.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(Intermech.Navigator.Utils.GetNavigatorColumns);
  }

  /// <summary>Расшариваем корректные колонки</summary>
  /// <param name="columns">Коллекция колонок "Навигатора"</param>
  /// <returns>Состояние колонок</returns>
  public StatesRecord ShareValidColumns(NodeColumnCollection columns)
  {
    StatesRecord record = new StatesRecord(this.Columns.Count, false);
    for (int index1 = 0; index1 < columns.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.Columns.Count; ++index2)
      {
        if (this.Columns[index2] is NavigatorTreeColumn && (this.Columns[index2] as NavigatorTreeColumn).NavigatorColumn.SchemeGuid.Equals(columns[index1].SchemeGuid) && (this.Columns[index2] as NavigatorTreeColumn).NavigatorColumn.ID.Equals(columns[index1].ID))
        {
          record[(this.Columns[index2] as NavigatorTreeColumn).AbsoluteIndex] = true;
          break;
        }
      }
    }
    return this._statesManager.Share(record);
  }

  /// <summary>
  /// Получить информацию о политике, применяемой к дочерним элементам
  /// </summary>
  /// <param name="sender">Оправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void TreeGetChildPolicy(object sender, GetChildPolicyEventArgs e)
  {
    NavigatorTreeNode navigatorTreeNode = e.Row != null ? (e.Row as Row).Item as NavigatorTreeNode : (NavigatorTreeNode) null;
    if (navigatorTreeNode == null || navigatorTreeNode.HasChildren && navigatorTreeNode.Children.Count == 0)
      e.ChildPolicy = RowChildPolicy.LoadOnExpand;
    else
      e.ChildPolicy = RowChildPolicy.Normal;
  }

  /// <summary>Получить информацию о дочерних элементах</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void TreeGetChildren(object sender, GetChildrenEventArgs e)
  {
    if (this.RootRow == null || !(e.Row.Item is NavigatorTreeNode navigatorTreeNode))
      return;
    if (navigatorTreeNode != null)
      navigatorTreeNode.Handle = e.Row;
    if (navigatorTreeNode.HasChildren && navigatorTreeNode.Children.Count == 0)
      e.Children = (IList) navigatorTreeNode.FakeItems;
    else
      e.Children = (IList) navigatorTreeNode.Children;
  }

  /// <summary>
  /// Получить цвета для ячейки объекта, взятого на изменение текущим пользователем
  /// </summary>
  protected virtual StyleDelta CheckedOutByCurrentUserStyleDelta(long currentID)
  {
    return new StyleDelta()
    {
      BackColor = this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkStartColor,
      GradientColor = (this._navGraphicsCache.CurrentColorsScheme.Gradient & GradientUsing.CheckOut) == GradientUsing.CheckOut ? this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkEndColor : this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkStartColor,
      GradientMode = this._navGraphicsCache.CurrentColorsScheme.CheckedOutGradientMode,
      ForeColor = this._navGraphicsCache.CurrentColorsScheme.ForegroundCheckedOut
    };
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool DisableTreeRowExpand { get; set; }

  /// <summary>Узел начинает раскрываться</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void TreeRowExpand(object sender, RowEventArgs e)
  {
    if (this.DisableTreeRowExpand || this.RootRow == null)
      return;
    this.UpdateCommandsTable();
    this.UpdateCommandManagerItems();
    if (!(e.Row.Item is NavigatorTreeNode node) || this._correctNodeExpansionNavigatorTreeViewExtension.TryCorrentIncorrectNodeExpansion(node))
      return;
    node.Handle = e.Row;
    this.CancelUpdateJobs((object) "UpdateJob", true);
    if (node.HasChildren && node.Children.Count == 0 || e.Row.ChildItems == null || e.Row.ChildItems.Count != node.Children.Count)
      this.PopulateNode(node);
    if (node.Handle != null)
      node.Handle.UpdateChildren(true, false);
    node.Children.RebuildHandles();
    this.QueueUpdateJobs(true);
    if (this.AfterExpand == null)
      return;
    this.AfterExpand((object) this, new NodeEventArgs(node));
  }

  public void RebuildHandles()
  {
    if (this._rootNode == null)
      return;
    this.RebuildHandles(this._rootNode);
  }

  private void RebuildHandles(NavigatorTreeNode navigatorTreeNode)
  {
    navigatorTreeNode.Children.RebuildHandles();
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) navigatorTreeNode.Children)
      this.RebuildHandles(child);
  }

  /// <summary>Нажата клавиша мышки в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void TreeMouseDown(object sender, MouseEventArgs e)
  {
    if (e.Button == MouseButtons.Left && e.Y <= this.HeaderHeight)
      this._mouseDownOnHeader = true;
    if (this._disableTreeEvents)
      return;
    if (!this.DisableDragAndDrop && e.Button == MouseButtons.Left)
    {
      ISelectedItems selectedItems = this.SelectedItems;
      if (selectedItems != null && selectedItems.Count > 0)
      {
        Size dragSize = SystemInformation.DragSize;
        dragSize.Width += 8;
        dragSize.Height += 8;
        this._dragBoxFromMouseDown = new Rectangle(e.Location, dragSize);
        return;
      }
    }
    this._dragBoxFromMouseDown = Rectangle.Empty;
  }

  public NavigatorTreeNode DragDropLastDestNode
  {
    [DebuggerStepThrough] get
    {
      return this._dropTargetRow == null ? (NavigatorTreeNode) null : this._dropTargetRow.Item as NavigatorTreeNode;
    }
  }

  /// <summary>Попробовать начать drag'n'drop с указанной точки</summary>
  /// <param name="location">Точка начала drag'n'drop</param>
  protected virtual void TreeStartDragDrop(Point location)
  {
    if (this.DisableDragAndDrop || this._disableTreeEvents || location.Y < this.HeaderHeight || this._mouseDownOnHeader || this._dragBoxFromMouseDown.IsEmpty)
      return;
    NavigatorTreeNode nodeAt = this.GetNodeAt(this._dragBoxFromMouseDown.X, this._dragBoxFromMouseDown.Y);
    if (nodeAt == null || this.SelectedRow == null)
    {
      this._dragBoxFromMouseDown = Rectangle.Empty;
    }
    else
    {
      Size dragSize = SystemInformation.DragSize;
      Rectangle rectangle1 = new Rectangle(this._dragBoxFromMouseDown.X - dragSize.Width / 2, this._dragBoxFromMouseDown.Y - dragSize.Height / 2, dragSize.Width, dragSize.Height);
      Infralution.Controls.VirtualTree.RowWidget rowWidget = this.PinnedPanel.GetRowWidget(nodeAt.Handle);
      int y1 = location.Y;
      Rectangle rectangle2 = rowWidget.Bounds;
      int top = rectangle2.Top;
      if (y1 >= top)
      {
        int y2 = location.Y;
        rectangle2 = rowWidget.Bounds;
        int bottom = rectangle2.Bottom;
        if (y2 <= bottom)
          return;
      }
      if (location.Y >= rectangle1.Top && location.Y <= rectangle1.Bottom)
        return;
      if (nodeAt.NodeID is ObjectGroupNodeID)
      {
        this._dragBoxFromMouseDown = Rectangle.Empty;
      }
      else
      {
        rectangle2 = SystemInformation.WorkingArea;
        this._screenOffset = rectangle2.Location;
        int num = (int) this.DoDragDrop((object) new IOSource(((IIOSource) this).Control, this.Services, ((IIOSource) this).SelectedItems), System.Windows.Forms.DragDropEffects.All);
      }
    }
  }

  /// <summary>В дерево пришло событие drag'n'drop</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void TreeDragEnter(object sender, System.Windows.Forms.DragEventArgs e)
  {
    if (this.DisableDragAndDrop || this._disableTreeEvents)
      return;
    e.Effect = this.GetDragDropEffects(e);
  }

  private System.Windows.Forms.DragDropEffects GetDragDropEffects(System.Windows.Forms.DragEventArgs e)
  {
    if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop) || e.Data.GetDataPresent(typeof (IOSource)))
    {
      if (!this._mouseDownOnHeader)
        return System.Windows.Forms.DragDropEffects.Move;
    }
    else if (e.Data.GetDataPresent(typeof (NavigatorTreeColumn)))
      return System.Windows.Forms.DragDropEffects.Move;
    return System.Windows.Forms.DragDropEffects.None;
  }

  /// <summary>Над деревом перетаскиваются объекты</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void TreeDragOver(object sender, System.Windows.Forms.DragEventArgs e)
  {
    if (this.DisableDragAndDrop || this._disableTreeEvents)
      return;
    e.Effect = this.GetDragDropEffects(e);
  }

  /// <summary>В дереве завершён drag'n'drop</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  protected virtual void TreeDragDrop(object sender, System.Windows.Forms.DragEventArgs e)
  {
    if (this.DisableDragAndDrop || this._disableTreeEvents)
      return;
    System.Windows.Forms.IDataObject data1 = e.Data;
    if (data1.GetDataPresent(typeof (NavigatorTreeColumn)))
    {
      NavigatorTreeColumn data2 = data1.GetData(typeof (NavigatorTreeColumn)) as NavigatorTreeColumn;
      if (!this.MayDropNavigatorTreeColumn(data2))
        return;
      this.DropNavigatorTreeColumn(data2, e);
    }
    else
    {
      if (this._mouseDownOnHeader)
        return;
      if (data1.GetDataPresent(typeof (IOSource)))
      {
        IOSource data3 = data1.GetData(typeof (IOSource)) as IOSource;
        if (!this.MayDropIOSource(data3))
          return;
        System.Windows.Forms.ContextMenu menu4DropIoSource = this.CreateContextMenu4DropIOSource(data3, this.IsNavigatorContextMenuCommandExist(data3.SelectedItems, data3.Services, "Cut"));
        Point client = this.PointToClient(new Point(e.X, e.Y));
        WidgetControl widgetControl = this.GetWidget(client.X, client.Y).WidgetControl;
        Point pos = client;
        menu4DropIoSource.Show((Control) widgetControl, pos);
      }
      else
      {
        if (!data1.GetDataPresent(System.Windows.DataFormats.FileDrop))
          return;
        string[] data4 = data1.GetData(System.Windows.DataFormats.FileDrop) as string[];
        if (!this.MayDropFiles(data4))
          return;
        System.Windows.Forms.ContextMenu contextMenu4DropFiles = this.CreateContextMenu4DropFiles(data4);
        Point client = this.PointToClient(new Point(e.X, e.Y));
        WidgetControl widgetControl = this.GetWidget(client.X, client.Y).WidgetControl;
        Point pos = client;
        contextMenu4DropFiles.Show((Control) widgetControl, pos);
      }
    }
  }

  private bool MayDropNavigatorTreeColumn(NavigatorTreeColumn navigatorTreeColumn)
  {
    return navigatorTreeColumn != null;
  }

  private void DropNavigatorTreeColumn(NavigatorTreeColumn navigatorTreeColumn, System.Windows.Forms.DragEventArgs e)
  {
    Point client = this.PointToClient(new Point(e.X, e.Y));
    NavigatorTreeColumn columnAt = this.GetColumnAt(client.X, client.Y);
    if (navigatorTreeColumn == columnAt || columnAt == null)
      return;
    int index = this.Columns.IndexOf((Column) columnAt);
    this.Columns.Remove((Column) navigatorTreeColumn);
    if (index >= this.Columns.Count)
      this.Columns.Add((Column) navigatorTreeColumn);
    else
      this.Columns.Insert(index, (Column) navigatorTreeColumn);
    if (this.Columns.Count > 0)
      this.MainColumn = this.Columns[0];
    this.RebuildTree();
  }

  protected bool MayDropIOSource(IOSource ioSource)
  {
    return ioSource != null && ioSource.SelectedItems != null && ioSource.SelectedItems.Count != 0;
  }

  private System.Windows.Forms.ContextMenu CreateContextMenu4DropIOSource(
    IOSource ioSource,
    bool allowCut = true)
  {
    System.Windows.Forms.ContextMenu menu4DropIoSource = new System.Windows.Forms.ContextMenu(new MenuItem[1]
    {
      new MenuItem("Копировать", (EventHandler) ((sender, e) => this.DropIOSource(ioSource, "Copy")))
    });
    if (allowCut)
      menu4DropIoSource.MenuItems.Add(new MenuItem("Переместить", (EventHandler) ((sender, e) => this.DropIOSource(ioSource, "Cut"))));
    menu4DropIoSource.MenuItems.Add(new MenuItem("-"));
    menu4DropIoSource.MenuItems.Add(new MenuItem("Отменить", (EventHandler) ((sender, e) => this._dropTargetRow = (Row) null)));
    return menu4DropIoSource;
  }

  private void DropIOSource(IOSource ioSource, string contextMenuCommand)
  {
    this.WorkThroughtClipboard((Action) (() =>
    {
      if (!this.ExecuteNavigatorContextMenuCommand(ioSource.SelectedItems, ioSource.Services, contextMenuCommand))
        return;
      this.ExecuteNavigatorContextMenuCommand(this.GetDropTarget(), (System.IServiceProvider) this._services, "Paste");
    }));
    this.ExpandLastDropNode();
    this._dropTargetRow = (Row) null;
  }

  private bool MayDropFiles(string[] files) => files != null && files.Length != 0;

  private System.Windows.Forms.ContextMenu CreateContextMenu4DropFiles(string[] files)
  {
    return new System.Windows.Forms.ContextMenu(new MenuItem[3]
    {
      new MenuItem("Переместить", (EventHandler) ((sender, e) => this.DropFiles(files))),
      new MenuItem("-"),
      new MenuItem("Отменить", (EventHandler) ((sender, e) => this._dropTargetRow = (Row) null))
    });
  }

  private void DropFiles(string[] files)
  {
    IClipboard clipboard = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
    ArrayList clipboardObjects = new ArrayList();
    try
    {
      ClientContext.FileImporter.BatchImport((ICollection<string>) files, (Action<long>) (importedObjectVersionId =>
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(importedObjectVersionId);
          clipboardObjects.Add((object) new ClipboardObject((IDBTypedObjectID) new DBTypedObjectID(dbObject.ObjectType, importedObjectVersionId, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, -1L, (string) null, dbObject.ModificationID), (IDBRelationID) null));
        }
      }));
    }
    catch
    {
    }
    this.WorkThroughtClipboard((Action) (() =>
    {
      clipboard.SetDataObject((object) new ClipboardObjectsList(clipboardObjects, false));
      this.ExecuteNavigatorContextMenuCommand(this.GetDropTarget(), (System.IServiceProvider) this._services, "Paste");
    }));
    this.ExpandLastDropNode();
    this._dropTargetRow = (Row) null;
  }

  private void WorkThroughtClipboard(Action action)
  {
    IClipboard service = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
    service.Push();
    try
    {
      action();
    }
    catch (InvalidOperationException ex)
    {
    }
    finally
    {
      service.Pop();
    }
  }

  protected bool IsNavigatorContextMenuCommandExist(
    ISelectedItems selectedItems,
    System.IServiceProvider serviceProvider,
    string contextMenuCommand)
  {
    return Intermech.Navigator.ContextMenu.Services.GetCommandsTable(selectedItems, serviceProvider, false).Contains(contextMenuCommand);
  }

  protected bool ExecuteNavigatorContextMenuCommand(
    ISelectedItems selectedItems,
    System.IServiceProvider serviceProvider,
    string contextMenuCommand)
  {
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(selectedItems, serviceProvider, false);
    if (!commandsTable.Contains(contextMenuCommand))
      return false;
    try
    {
      this.IsDragDrop = true;
      NavigatorTreeNode[] nodes = this.BeforeExecuteMenuCommand();
      try
      {
        Intermech.Navigator.ContextMenu.Services.InvokeCommand(contextMenuCommand, commandsTable, serviceProvider);
      }
      finally
      {
        this.AfterExecuteMenuCommand(nodes);
      }
    }
    finally
    {
      this.IsDragDrop = false;
    }
    return true;
  }

  [Obsolete]
  public bool IsDragDrop { get; private set; }

  protected ISelectedItems GetDropTarget()
  {
    this._dragdropItem.Node = this.DragDropLastDestNode;
    return (ISelectedItems) this._dragdropItem;
  }

  private void ExpandLastDropNode()
  {
    NavigatorTreeNode dropLastDestNode = this.DragDropLastDestNode;
    if (dropLastDestNode == null)
      return;
    dropLastDestNode.Expanded = true;
  }

  protected override RowDropLocation AllowedRowDropLocations(Row row, System.Windows.Forms.IDataObject data)
  {
    return RowDropLocation.OnRow;
  }

  protected override System.Windows.Forms.DragDropEffects RowDropEffect(
    Row row,
    RowDropLocation dropLocation,
    System.Windows.Forms.IDataObject data)
  {
    if (this.DisableDragAndDrop || this._disableTreeEvents || this._mouseDownOnHeader || (data.GetDataPresent(System.Windows.DataFormats.FileDrop) ? 1 : (data.GetDataPresent(typeof (IOSource)) ? 1 : 0)) == 0)
      return System.Windows.Forms.DragDropEffects.None;
    this._dropTargetRow = row;
    return System.Windows.Forms.DragDropEffects.Move;
  }

  /// <summary>Обновить указанный узел</summary>
  /// <param name="node">Обновляемый узел</param>
  public void UpdateTreeNode(NavigatorTreeNode node)
  {
    if (node == null || node.Handle == null)
      return;
    this.UpdateRow(node.Handle);
  }

  /// <summary>Обновить указанную строку</summary>
  /// <param name="row">Обновляемая строка</param>
  internal void UpdateRow(Row row)
  {
    if (row == null)
      return;
    row.Tree.UpdateRowData(row);
    if (row.ChildItems != null && row.ChildItems.Count > 0)
      row.UpdateChildren(true, false);
    row.Tree.UpdateRows();
  }

  /// <summary>Очистить сортировку в колонках дерева</summary>
  protected internal void ColumnsClearSorting()
  {
    if (this._treeColumns == null)
      return;
    NodeColumnCollection nodeColumnCollection = this.ReflectTreeColumsChanges();
    nodeColumnCollection.RemoveSortInfo();
    this.SetColumns(nodeColumnCollection);
  }

  /// <summary>Добавить узел в дерево</summary>
  /// <param name="node">Добавить узел</param>
  /// <param name="nodeIDs">Описания узлов</param>
  internal void ProcessAddChildren(NavigatorTreeNode node, NodeIDCollection nodeIDs)
  {
    NavigatorTreeNode navigatorTreeNode1 = (NavigatorTreeNode) null;
    if (node == null || nodeIDs.Count <= 0)
      return;
    if (node.HasChildren && node.Children.Count == 0 || !node.Full)
    {
      this.CancelUpdateJobs((object) "UpdateJob", true);
      this.MakeNodeUnpopulated(node);
      this.PopulateNode(node);
      this.QueueUpdateJobs(true);
    }
    INodeQuery query = this.GetChildHandler(node).GetQuery(ContentType.Folders);
    if (query == null)
      return;
    NodeColumnCollection treeColumns = this._treeColumns;
    this.SetQueryColumns(query, treeColumns);
    query.Execute(nodeIDs);
    for (int index = 0; index < query.RecordCount; ++index)
    {
      INodeID nid = query.GetRecordNodeID(index);
      if (!node.Children.Contains<NavigatorTreeNode>((Predicate<NavigatorTreeNode>) (o => object.Equals((object) o.NodeID, (object) nid))))
      {
        NavigatorTreeNode node1 = this.CreateNode(node, nid, query.GetRecordValues(index), query.GetRawRecordValues(index), treeColumns, true);
        if (!this.ManualSort)
          this.SetNodeIndex(node1, this.CalcNodeIndex(node1));
        if (node1 != null)
          navigatorTreeNode1 = node1;
      }
    }
    if (navigatorTreeNode1 == null && query.RecordCount > 0)
    {
      INodeID lastAddedNodeID = query.GetRecordNodeID(query.RecordCount - 1);
      navigatorTreeNode1 = node.Children.FirstOrDefault<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (o => object.Equals((object) o.NodeID, (object) lastAddedNodeID)));
    }
    NavigatorTreeNode navigatorTreeNode2 = node;
    if (navigatorTreeNode2 != null && navigatorTreeNode2.Handle != null)
      navigatorTreeNode2.Handle.Tree.UpdateRows(true);
    if (navigatorTreeNode1 == null || this.DragDropLastDestNode != null || this._disableFocusNodeAfterAdded || !this.Focused)
      return;
    navigatorTreeNode1.FocusThenExpand();
  }

  /// <summary>Обновить узел в дереве</summary>
  /// <param name="node">Обновляемый узел</param>
  /// <param name="affectedColumns">Изменённые колонки</param>
  /// <param name="indexes">Индексы</param>
  internal void ProcessUpdateChildren(
    NavigatorTreeNode node,
    NodeColumnCollection affectedColumns,
    IList indexes)
  {
    NodeColumnCollection treeColumns = this._treeColumns;
    bool flag = false;
    if (affectedColumns != null)
    {
      for (int index = 0; index < this._treeColumns.Count; ++index)
      {
        if (affectedColumns.Contains(treeColumns[index]))
        {
          flag = true;
          break;
        }
      }
    }
    if (!flag && affectedColumns != null)
      return;
    NodeIDCollection nodeIDs = new NodeIDCollection();
    Hashtable hashtable = new Hashtable();
    for (int index = 0; index < node.Children.Count; ++index)
    {
      if (indexes.Contains((object) index))
      {
        NavigatorTreeNode child = node.Children[index];
        nodeIDs.Add(child.NodeID);
        hashtable[(object) child.NodeID] = (object) child;
      }
    }
    if (nodeIDs.Count <= 0)
      return;
    INodeQuery query = this.GetChildHandler(node).GetQuery(ContentType.Folders);
    if (query == null)
      return;
    this.SetQueryColumns(query, treeColumns);
    query.Execute(nodeIDs);
    for (int index = 0; index < query.RecordCount; ++index)
    {
      INodeID recordNodeId = query.GetRecordNodeID(index);
      NavigatorTreeNode node1 = (NavigatorTreeNode) hashtable[(object) recordNodeId];
      if (node1 == null && index < nodeIDs.Count)
        node1 = (NavigatorTreeNode) hashtable[(object) nodeIDs[index]];
      if (node1 != null)
      {
        node1.NodeID = recordNodeId;
        NodeColumn nodeColumn = treeColumns.FirstOrDefault<NodeColumn>((System.Func<NodeColumn, bool>) (o => o.SortOrder != 0));
        object objA = (object) null;
        if (nodeColumn != null)
          objA = node1.GetRawCellValue(treeColumns.IndexOf(nodeColumn));
        object[] fieldValues = query.GetRecordValues(index);
        if (node1 == this.RootNode)
          fieldValues = this.ApplyCaptionNodeColumnTransform(fieldValues, recordNodeId, this.RootHandler);
        this.UpdateNodeFields(node1, fieldValues, query.GetRawRecordValues(index), treeColumns);
        if (nodeColumn != null)
        {
          object rawCellValue = node1.GetRawCellValue(treeColumns.IndexOf(nodeColumn));
          if (!object.Equals(objA, rawCellValue) && !this.ManualSort)
            this.SetNodeIndex(node1, this.CalcNodeIndex(node1));
        }
      }
    }
  }

  /// <summary>Удалить узел из дерева</summary>
  /// <param name="node">Узел</param>
  /// <param name="indexes">Индексы</param>
  internal void ProcessRemoveChildren(NavigatorTreeNode node, IList indexes)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    NavigatorTreeNode navigatorTreeNode1 = (NavigatorTreeNode) null;
    for (int index = node.Children.Count - 1; index >= 0; --index)
    {
      if (indexes.Contains((object) index))
      {
        if (node.Children[index].InTree && this.SelectedRows.Contains(node.Children[index].Handle))
          flag2 = true;
        if (node.Children[index] == this.FocusedNode)
        {
          flag3 = true;
          if (node.Children.Count > index + 1)
            navigatorTreeNode1 = node.Children[index + 1];
        }
        if (index < node.Children.Count)
        {
          NavigatorTreeNode child = node.Children[index];
          child.Parent = (NavigatorTreeNode) null;
          child.Handle = (Row) null;
          child.ClearChildren();
        }
        node.Children.RemoveAt(index);
        flag1 = true;
      }
      else if (flag3 && navigatorTreeNode1 == null)
        navigatorTreeNode1 = node.Children[index];
    }
    if (!flag1)
      return;
    NavigatorTreeNode navigatorTreeNode2 = navigatorTreeNode1 ?? node;
    NavigatorTreeNode node1 = node;
    if (node1 != null && node1.Handle != null)
    {
      if (node1.Children.Count == 0)
        this.MakeNodeUnpopulated(node1);
      else
        node1.Handle.Tree.UpdateRows(true);
    }
    if (flag3 && navigatorTreeNode2 != null)
    {
      this.FocusedNode = navigatorTreeNode2;
    }
    else
    {
      if (!flag2)
        return;
      this.TreeSelectionChanged((object) this, (EventArgs) null);
    }
  }

  /// <summary>Обновить текущий узел</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public virtual void TreeRefreshNodeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this.CancelUpdateJobs((object) "UpdateJob", true);
    try
    {
      if (this.FocusedNode == this.RootNode && this.RootDescriptor != null)
        this.Build(this.RootDescriptor);
      else
        this.RefreshNode(this.FocusedNode);
    }
    finally
    {
      this.QueueUpdateJobs(true);
    }
  }

  /// <summary>Заблокировать некоторые события в дереве</summary>
  public void LockTreeEvents()
  {
    ++this._lockFocusedItemEvent;
    ++this._lockSelectionChanged;
  }

  /// <summary>Разблокировать некоторые события в дереве</summary>
  public void UnlockTreeEvents()
  {
    --this._lockSelectionChanged;
    --this._lockFocusedItemEvent;
  }

  /// <summary>Развернуть нод</summary>
  /// <param name="node"></param>
  /// <param name="expandNode"></param>
  public void SetNodeExpanded(NavigatorTreeNode node, bool expandNode)
  {
    if (expandNode)
    {
      node.Expanded = true;
      node.Tag = (object) new NavigatorTreeView.NavigatorNodeTag(true);
    }
    else
    {
      node.Expanded = false;
      node.Tag = (object) null;
    }
  }

  /// <summary>
  /// Раскрыть следующий нод (без заполнения тегов у узлов и автоскрытия родительских узлов)
  /// </summary>
  /// <param name="currentNode">Текущий нод</param>
  /// <param name="withChild"></param>
  /// <returns>Очередной узел</returns>
  public NavigatorTreeNode ExpandNextNode(NavigatorTreeNode currentNode, bool withChild)
  {
    if (withChild)
    {
      NavigatorTreeNode navigatorTreeNode = currentNode;
      if (navigatorTreeNode != null && navigatorTreeNode.InTree && !navigatorTreeNode.Expanded && navigatorTreeNode.Cycle == NavigatorNodeCycle.None)
      {
        navigatorTreeNode.Expanded = true;
        DateTime utcNow = DateTime.UtcNow;
        TimeSpan timeSpan = new TimeSpan(0, 0, 60);
        while (!navigatorTreeNode.Full)
        {
          Thread.Sleep(0);
          if (DateTime.UtcNow - utcNow > timeSpan)
            break;
        }
      }
      if (currentNode.Children.Count > 0)
        return currentNode.Children[0];
    }
    if (currentNode.Parent == null)
      return (NavigatorTreeNode) null;
    int id = currentNode.Id;
    return id == currentNode.Parent.Children.Count - 1 ? this.ExpandNextNode(currentNode.Parent, false) : currentNode.Parent.Children[id + 1];
  }

  /// <summary>Вызывает окно "Ручная сортировка", если оно доступно</summary>
  public void TreeManualSortingSetupCommand()
  {
    ISelectedItems selectedItems = this.SelectedItems;
    if (selectedItems == null || selectedItems.Count == 0)
      return;
    long[] ChRels = (long[]) null;
    if (ManualSortingEditForm.Execute(string.Empty, selectedItems, (System.IServiceProvider) this._services, out ChRels) != DialogResult.OK || ChRels.Length == 0)
      return;
    this._notificationService.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("SortedRelationsChanged", (IList<long>) ChRels));
    this._notificationService.FireEvent((object) this, new NotificationEventArgs("FiltrationChanged"));
  }

  public bool Execute(string commandName)
  {
    switch (commandName)
    {
      case "AdminCancelChanges":
      case "CancelChanges":
      case "CheckIn":
      case "CheckOut":
      case "Copy":
      case "Cut":
      case "Delete":
      case "Exclude":
      case "ParametersCard":
      case "Paste":
      case "Print":
      case "PrintDocument":
      case "Refresh":
      case "SaveChanges":
      case "ViewDocument":
        if (commandName == "Print")
          commandName = "PrintDocument";
        return this.ExecuteMenuCommand(commandName);
      case "Find":
        return this.ExecuteMenuCommand("SeekInTree");
      case null:
        throw new ArgumentNullException(nameof (commandName));
      default:
        return this.ExecuteMenuCommand(commandName);
    }
  }

  public event EventHandler CommandsTableUpdated;

  private void UpdateCommandsTable()
  {
    this._currentContextMenuServiceProvider = (System.IServiceProvider) this.GetContextMenuServices((System.IServiceProvider) this._services);
    this._currentCommandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, this._currentContextMenuServiceProvider, false);
    if (this._currentCommandsTable != null)
    {
      CommandLink commandLink = this._currentCommandsTable["SetupColumns"];
      this._changeColumnsToolStripMenuItem.Visible = commandLink != null && commandLink.CommandInfo != null;
    }
    else
      this._changeColumnsToolStripMenuItem.Visible = false;
    EventHandler commandsTableUpdated = this.CommandsTableUpdated;
    if (commandsTableUpdated == null)
      return;
    commandsTableUpdated((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// Сгенерировать событие "Выделенные элементы изменились"
  /// </summary>
  protected internal virtual void RaiseSelectedItemsChanged()
  {
    this.UpdateCommandManagerItems();
    if (this.SelectedItemsChanged == null)
      return;
    this.SelectedItemsChanged((object) this, EventArgs.Empty);
  }

  /// <summary>Сгенерировать событие "Дерево перестроилось"</summary>
  protected virtual void RaiseBuildTreeEvent()
  {
    if (this.BuildTree == null)
      return;
    this.BuildTree((object) this, EventArgs.Empty);
  }

  /// <summary>Сгенерировать событие "Перед фокусированием узла"</summary>
  /// <param name="node">Текущий узел</param>
  /// <param name="newNode">Новый узел</param>
  protected virtual void RaiseBeforeFocusNode(NavigatorTreeNode node, NavigatorTreeNode newNode)
  {
    if (this.BeforeFocusNode == null || this._lockFocusedItemEvent != 0)
      return;
    this.BeforeFocusNode((object) this, new NavigatorTreeNodeEventArgs(node));
  }

  /// <summary>Сгенерировать событие "После фокусирования узла"</summary>
  /// <param name="node">Новый узел</param>
  protected internal virtual void RaiseAfterFocusNode(NavigatorTreeNode node)
  {
    if (this.AfterFocusNode == null || this._lockFocusedItemEvent != 0)
      return;
    this.AfterFocusNode((object) this, new NavigatorTreeNodeEventArgs(node));
  }

  /// <summary>Сгенерировать событие "Корневой узел заменён"</summary>
  protected internal virtual void RaiseRootNodeReplaced()
  {
    if (this.RootNodeReplaced == null)
      return;
    this.RootNodeReplaced((object) this, EventArgs.Empty);
  }

  /// <summary>Сгенерировать событие "Корневой узел изменился"</summary>
  protected internal virtual void RaiseRootNodeModified()
  {
    if (this.RootNodeModified == null)
      return;
    this.RootNodeModified((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// При необходимости сгенерировать событие о том, что происходит изменение статуса у узла
  /// </summary>
  /// <param name="node">Узел</param>
  /// <param name="oldValue">Старое значение</param>
  /// <param name="newValue">Новое значение</param>
  protected internal virtual void RaiseCheckStateChanging(
    NavigatorTreeNode node,
    CheckState oldValue,
    ref CheckState newValue)
  {
    if (node == null || oldValue == newValue || this.CheckStateChanging == null)
      return;
    CheckStateEventArgs e = new CheckStateEventArgs(node, oldValue, newValue);
    this.CheckStateChanging((object) this, e);
    newValue = e.NewValue;
  }

  /// <summary>
  /// При необходимости сгенерировать событие о том, что произошло изменение статуса у узла
  /// </summary>
  /// <param name="node">Узел</param>
  protected internal virtual void RaiseCheckStateChanged(NavigatorTreeNode node)
  {
    this.UpdateCommandsTable();
    if (node == null || this.CheckStateChanged == null)
      return;
    this.CheckStateChanged((object) this, new NodeEventArgs(node));
  }

  /// <summary>Установить колонки запросу</summary>
  /// <param name="query">Запрос</param>
  /// <param name="columns">Колонки</param>
  internal void SetQueryColumns(INodeQuery query, NodeColumnCollection columns)
  {
    IColumnSchemes service = ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes;
    for (int index = 0; index < columns.Count; ++index)
      query.AddColumn(columns[index], service.GetDefaultTransform(columns[index].SchemeGuid, columns[index].ID));
  }

  /// <summary>Обновить данные в узле</summary>
  /// <param name="node">Узел дерева "Навигатора"</param>
  /// <param name="fieldValues">Новые значения ячеек узла</param>
  /// <param name="rawFieldValues">Новые исходные значения ячеек узла</param>
  /// <param name="columns">Коллекция колонок дерева "Навигатора"</param>
  internal void UpdateNodeFields(
    NavigatorTreeNode node,
    object[] fieldValues,
    object[] rawFieldValues,
    NodeColumnCollection columns)
  {
    this.SetNodeFields(node, fieldValues, rawFieldValues, columns);
    NavigatorTreeNode navigatorTreeNode = node;
    navigatorTreeNode.ValidColumns = this.ShareValidColumns(navigatorTreeNode.ValidColumns, columns);
    this.UpdateTreeNode(node);
  }

  /// <summary>
  /// Создает элемент навигации, который будет работать с содержимым указанного узла дерева
  /// </summary>
  /// <param name="node">Узел дерева "Навигатора"</param>
  /// <returns>Элемент навигации, который будет работать с содержимым указанного узла дерева</returns>
  internal INode CreateChildHandler(NavigatorTreeNode node)
  {
    INode nodeHandler = this.GetNodeHandler(node);
    INode child = this.GetNodeHandler(node).GetChild(node.NodeID);
    IContextAware contextAware = child as IContextAware;
    IContextAware handler = node.Parent != null ? node.Parent.Handler as IContextAware : (IContextAware) null;
    if (contextAware == null || nodeHandler == null)
      return child;
    AdvancedServiceContainer serviceContainer = new AdvancedServiceContainer();
    IDBRelationID data1 = nodeHandler.GetData(node.NodeID, typeof (IDBRelationID)) as IDBRelationID;
    IDBTypedObjectID data2 = nodeHandler.GetData(node.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (data1 != null || data2 != null)
    {
      IDBTypedObjectID compositionObject = this.GetTopCompositionObject(node);
      this._currentUserAndRole = this._currentUserAndRole ?? ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      RelationPair serviceInstance = new RelationPair(0L, compositionObject != null ? compositionObject.ObjectID : 0L, compositionObject != null ? compositionObject.ObjectType : -1, data1 == null || !MetaDataHelper.IsPdmPartiallyConfigurableRelationType(data1.RelationType) ? 0L : data1.Value, this._currentUserAndRole.UserID, data2 != null ? data2.ObjectID : 0L, data1 == null || !MetaDataHelper.IsPdmPartiallyConfigurableRelationType(data1.RelationType) ? -1 : data1.RelationType, data2 != null ? data2.ObjectType : -1);
      if (!serviceInstance.Empty && serviceInstance.TOP_OBJECT_ID != 0L)
        serviceContainer.AddService(typeof (RelationPair), (object) serviceInstance);
    }
    serviceContainer.AdvancedProvider = handler != null ? handler.Services : this.Services;
    contextAware.Services = (System.IServiceProvider) serviceContainer;
    return child;
  }

  /// <summary>
  /// Инициализирует ресурсы, ассоциированные с узлом дерева.
  /// </summary>
  /// <param name="node">Узел дерева "Навигатора"</param>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="columns">Коллекция колонок</param>
  protected void InitNodeData(NavigatorTreeNode node, INodeID nodeID, NodeColumnCollection columns)
  {
    node.NodeID = nodeID;
    node.Handler = this.CreateChildHandler(node);
    node.ValidColumns = this.ShareValidColumns(columns);
  }

  /// <summary>Создать корневой узел в дереве "Навигатора"</summary>
  /// <returns>Корневой узел в дереве "Навигатора"</returns>
  protected virtual NavigatorTreeNode CreateRootNode()
  {
    this.ShowRootRow = false;
    INodeQuery query = this.RootHandler.GetQuery(ContentType.Folders);
    this.SetQueryColumns(query, this._treeColumns);
    query.Execute((object) null, 1);
    if (this._rootNode != null)
      this._rootNode.Children.Clear();
    INodeID nodeID = (INodeID) null;
    try
    {
      nodeID = query.GetRecordNodeID(0);
    }
    catch (IndexOutOfRangeException ex)
    {
    }
    INode rootHandler = this.RootHandler;
    NavigatorTreeNode rootNode = (NavigatorTreeNode) null;
    if (nodeID != null)
      rootNode = this.CreateNode(this._rootNode, nodeID, this.ApplyCaptionNodeColumnTransform(query.GetRecordValues(0), nodeID, rootHandler), query.GetRawRecordValues(0), this._treeColumns, false);
    this.DataSource = (object) this._rootNode;
    return rootNode;
  }

  private object[] ApplyCaptionNodeColumnTransform(
    object[] fieldValues,
    INodeID nodeID,
    INode nodeHandler)
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    object[] objArray = fieldValues == null || fieldValues.Length == 0 ? new object[0] : new object[fieldValues.Length];
    if (fieldValues != null && fieldValues.Length >= this._treeColumns.Count && nodeHandler != null && nodeID != null)
    {
      for (int index = 0; index < this._treeColumns.Count; ++index)
      {
        NodeColumn treeColumn = this._treeColumns[index];
        if (service.GetDefaultTransform(treeColumn.SchemeGuid, treeColumn.ID) is CaptionTransform)
        {
          IDBTypedObjectID data = nodeHandler.GetData(nodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
          objArray[index] = data != null ? (object) CaptionTransform.GetCaption(data.Caption, data.Version) : fieldValues[index];
          if (objArray[index] is string)
            objArray[index] = (object) NavigatorTreeView.NewLineRegex.Replace((string) objArray[index], string.Empty);
        }
        else
          objArray[index] = fieldValues[index];
      }
    }
    return objArray;
  }

  public bool TryBrowse(NodeIDPath nodeIDPath)
  {
    if (nodeIDPath == null)
      throw new ArgumentNullException(nameof (nodeIDPath));
    NavigatorTreeNode lastNode = (NavigatorTreeNode) null;
    this.TryFind(nodeIDPath, out lastNode);
    lastNode?.Focus();
    return lastNode != null;
  }

  public bool TryFind(NodeIDPath nodeIDPath, out NavigatorTreeNode lastNode)
  {
    if (nodeIDPath == null)
      throw new ArgumentNullException(nameof (nodeIDPath));
    lastNode = this.RootNode;
    if (lastNode == null)
      return false;
    foreach (INodeID nodeId in nodeIDPath.Cast<INodeID>().Skip<INodeID>(1))
    {
      INodeID nodeID = nodeId;
      lastNode.Fetch();
      NavigatorTreeNode navigatorTreeNode = lastNode.Children.FirstOrDefault<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (o => object.Equals((object) o.NodeID, (object) nodeID)));
      if (navigatorTreeNode == null)
        return false;
      lastNode = navigatorTreeNode;
    }
    return true;
  }

  public bool TryFind(string path, out NavigatorTreeNode lastNode)
  {
    if (string.IsNullOrEmpty(path))
      throw new ArgumentException();
    lastNode = this.RootNode;
    foreach (string address in ((IEnumerable<string>) path.Split('\\')).Skip<string>(1))
    {
      if (lastNode == null)
        return false;
      lastNode.Fetch();
      INode childHandler = this.GetChildHandler(lastNode);
      if (childHandler == null)
        return false;
      INodeID nodeId = childHandler.ParseAddress(address);
      if (nodeId == null)
        return false;
      NavigatorTreeNode navigatorTreeNode = lastNode.Children.FirstOrDefault<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (o => object.Equals((object) o.NodeID, (object) nodeId)));
      if (navigatorTreeNode == null)
        return false;
      lastNode = navigatorTreeNode;
    }
    return true;
  }

  public bool TryBrowse(string path)
  {
    if (string.IsNullOrEmpty(path))
      throw new ArgumentException();
    NavigatorTreeNode lastNode = (NavigatorTreeNode) null;
    this.TryFind(path, out lastNode);
    lastNode?.Focus();
    return lastNode != null;
  }

  /// <summary>Открыть в дереве указанный путь</summary>
  /// <param name="path">Путь</param>
  /// <param name="completeFind">Выполнять поиск в дереве</param>
  public virtual void Browse(string path, bool completeFind = true)
  {
    if (path.StartsWith(Convert.ToString(Path.DirectorySeparatorChar)))
      path = path.Remove(0, 1);
    if (path.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
      path = path.Remove(path.Length - 1, 1);
    string[] strArray = path.Split('\\');
    NavigatorTreeNode navigatorTreeNode1 = this.RootNode != null ? this.RootNode.Parent : (NavigatorTreeNode) null;
    int index = 0;
    int num;
    for (num = strArray.Length - 1; index < num; ++index)
    {
      INodeID address = this.GetChildHandler(navigatorTreeNode1).ParseAddress(strArray[index]);
      NavigatorTreeNode existingNode = navigatorTreeNode1 == null || address == null ? (NavigatorTreeNode) null : this.FindExistingNode(navigatorTreeNode1, address);
      if (existingNode != null)
      {
        navigatorTreeNode1 = existingNode;
        this.InitNodeHandle(navigatorTreeNode1);
      }
      else
        break;
    }
    if (index == num)
    {
      INodeID address = this.GetChildHandler(navigatorTreeNode1).ParseAddress(strArray[index]);
      NavigatorTreeNode existingNode = address == null ? (NavigatorTreeNode) null : this.FindExistingNode(navigatorTreeNode1, address);
      if (existingNode != null)
      {
        navigatorTreeNode1 = existingNode;
        this.InitNodeHandle(navigatorTreeNode1);
        ++index;
      }
    }
    if (index <= num)
    {
      for (; index < num; ++index)
      {
        INodeID address = this.GetChildHandler(navigatorTreeNode1).ParseAddress(strArray[index]);
        NavigatorTreeNode fetchedNode = navigatorTreeNode1 == null || address == null ? (NavigatorTreeNode) null : this.FindFetchedNode(navigatorTreeNode1, address, completeFind);
        if (fetchedNode != null)
        {
          navigatorTreeNode1 = fetchedNode;
          this.InitNodeHandle(navigatorTreeNode1);
        }
        else
          break;
      }
      if (index == num)
      {
        this.RefreshNode(navigatorTreeNode1);
        INodeID address = this.GetChildHandler(navigatorTreeNode1).ParseAddress(strArray[index]);
        NavigatorTreeNode navigatorTreeNode2 = (navigatorTreeNode1 == null || address == null ? (NavigatorTreeNode) null : this.FindExistingNode(navigatorTreeNode1, address)) ?? (navigatorTreeNode1 == null || address == null ? (NavigatorTreeNode) null : this.FindFetchedNode(navigatorTreeNode1, address, completeFind));
        if (navigatorTreeNode2 != null)
        {
          navigatorTreeNode1 = navigatorTreeNode2;
          this.InitNodeHandle(navigatorTreeNode1);
        }
      }
    }
    (navigatorTreeNode1 == null || navigatorTreeNode1.Level == 0 ? this.RootNode : navigatorTreeNode1)?.FocusThenExpand();
    this.QueueUpdateJobs(false);
  }

  /// <summary>
  /// Отразить изменения в колонках дерева "Навигатора" на колонки "Навигатора"
  /// </summary>
  public NodeColumnCollection ReflectTreeColumsChanges()
  {
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    for (int index = 0; index < this.Columns.Count; ++index)
    {
      if (this.Columns[index] is NavigatorTreeColumn column && column.NavigatorColumn != null)
      {
        NodeColumn nodeColumn = column.NavigatorColumn.Clone() as NodeColumn;
        nodeColumn.SortOrder = this.SortColumn == column ? NavigatorTreeViewHelper.TreeToNavigatorSortDirection(column.SortDirection) : NodeColumnSortOrder.None;
        nodeColumn.SortIndex = this.SortColumn == column ? 0 : -1;
        nodeColumn.GroupIndex = -1;
        nodeColumn.Width = column.Width;
        columnCollection.Add(nodeColumn);
      }
    }
    return columnCollection;
  }

  /// <summary>Выполняет полную пересортировку дерева навигации.</summary>
  protected virtual void SortTree()
  {
    NodeIDPath nodeIDPath = this.FocusedPath;
    if (this.FocusedNode == null)
    {
      NavigatorTreeNode rootNode = this.RootNode;
      nodeIDPath = this.GetNodeIDPath(this.RootNode);
    }
    ++this._lockClearTreeEvent;
    ++this._lockFocusedItemEvent;
    ++this._lockSelectionChanged;
    try
    {
      this.ClearCore(true);
      this.CreateRootNode();
      this.RebuildTree();
    }
    finally
    {
      --this._lockSelectionChanged;
      --this._lockFocusedItemEvent;
      --this._lockClearTreeEvent;
    }
    if (nodeIDPath != null && nodeIDPath.Length > 1)
    {
      this.TryBrowse(nodeIDPath);
    }
    else
    {
      if (this.RootNode == null)
        return;
      this.RootNode.FocusThenExpand();
    }
  }

  /// <summary>Добавить узел в состав указанного родительского узла</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="fieldValues">Поля, на основании которых строится новый узел (значения для отображения на экране)</param>
  /// <param name="rawValues">Поля, на основании которых строится новый узел (исходные значения)</param>
  /// <returns>Вновь добавленный узел</returns>
  public virtual NavigatorTreeNode AppendNode(
    NavigatorTreeNode parent,
    object[] fieldValues,
    object[] rawValues,
    INodeID nodeID)
  {
    if (parent == null)
      parent = this.RootNode;
    System.IServiceProvider services = (parent == null || parent.Handler == null ? (IContextAware) null : parent.Handler as IContextAware)?.Services;
    NavigatorTreeNode navTreeNode = (services != null ? (ITreeNodesFactory) services.GetService(typeof (ITreeNodesFactory)) : (ITreeNodesFactory) null)?.CreateNavTreeNode(this, parent, nodeID, fieldValues, rawValues);
    if (navTreeNode == null && parent.Handler != null)
      navTreeNode = parent.Handler.GetData(nodeID, typeof (ITreeNodesFactory)) is ITreeNodesFactory data ? data.CreateNavTreeNode(this, parent, nodeID, fieldValues, rawValues) : (NavigatorTreeNode) null;
    if (navTreeNode == null && this.Services != null)
      navTreeNode = ((ITreeNodesFactory) this.Services.GetService(typeof (ITreeNodesFactory)))?.CreateNavTreeNode(this, parent, nodeID, fieldValues, rawValues);
    return navTreeNode ?? new NavigatorTreeNode(this, parent, (INodeID) null, fieldValues, rawValues);
  }

  /// <summary>Очистка списка отмеченных узлов</summary>
  public virtual void CheckedNodesClear()
  {
    foreach (NavigatorTreeNode checkedNode in this.CheckedNodes)
      checkedNode.SetCheckState(CheckState.Unchecked);
  }

  /// <summary>
  /// Метод позволяет перестроить коллекцию выделенных элементов после того,
  /// как были обновлены внутренние структуры в дереве
  /// </summary>
  public void InvalidateSelectedItems()
  {
    for (int index = 0; index < this.SelectedRows.Count; ++index)
      this.UpdateRowData(this.SelectedRows[index]);
  }

  /// <summary>Получить узел из указанной точки</summary>
  /// <param name="x">Координата X</param>
  /// <param name="y">Координата Y</param>
  /// <returns>Найденный узел ли null</returns>
  public NavigatorTreeNode GetNodeAt(int x, int y)
  {
    Hashtable rows = new Hashtable();
    this.GetRows(this.TopRowIndex, this.BottomRowIndex + 1, rows);
    if (rows.Count > 0)
    {
      foreach (DictionaryEntry dictionaryEntry in rows)
      {
        Infralution.Controls.VirtualTree.RowWidget rowWidget = this.PinnedPanel.GetRowWidget(dictionaryEntry.Value as Row);
        if (rowWidget != null && rowWidget.Bounds.Top <= y && rowWidget.Bounds.Bottom >= y)
          return (dictionaryEntry.Value as Row).Item as NavigatorTreeNode;
      }
    }
    return (NavigatorTreeNode) null;
  }

  /// <summary>Начать обновление дерева</summary>
  public void BeginUpdate() => this.BeginInit();

  /// <summary>Завершить обновление дерева</summary>
  public void EndUpdate()
  {
    this.EndInit();
    this.RebuildTree();
  }

  /// <summary>Сделать узел невалидным</summary>
  /// <param name="node">Обрабатываемый узел</param>
  public void MakeNodeUnpopulated(NavigatorTreeNode node)
  {
    NavigatorTreeNode navigatorTreeNode = node;
    navigatorTreeNode.Bookmark = (object) null;
    this.RowCollapse -= new RowEventHandler(this.NavigatorTreeView_RowCollapse);
    try
    {
      navigatorTreeNode.Expanded = false;
      navigatorTreeNode.Full = false;
      navigatorTreeNode.Flags = TreeNodeFlags.ImageOutdated;
      navigatorTreeNode.Icon = (Icon) null;
      navigatorTreeNode.ClearChildren();
      navigatorTreeNode.HasChildren = true;
      try
      {
        this.BeforeFocusRowChanged -= new BeforeFocusedRowChangedHandler(this.NavigatorTreeView_BeforeFocusRowChanged);
        this.FocusRowChanged -= new EventHandler(this.NavigatorTreeView_FocusRowChanged);
        this.UpdateTreeNode(node);
      }
      finally
      {
        this.BeforeFocusRowChanged += new BeforeFocusedRowChangedHandler(this.NavigatorTreeView_BeforeFocusRowChanged);
        this.FocusRowChanged += new EventHandler(this.NavigatorTreeView_FocusRowChanged);
      }
      this.UpdateRowData(navigatorTreeNode.Handle);
    }
    finally
    {
      this.RowCollapse += new RowEventHandler(this.NavigatorTreeView_RowCollapse);
    }
  }

  /// <summary>
  /// Обновить поля узла, а также перечитать список его дочерних узлов
  /// </summary>
  /// <param name="node">Обновляемый узел</param>
  public void RefreshNode(NavigatorTreeNode node)
  {
    this.RefreshNodeFields(node);
    NodeIDPath focusedPath = this.FocusedPath;
    bool expanded = node.Expanded;
    bool hasFocus = node.HasFocus;
    Row row = (Row) null;
    if (this._services.GetService(typeof (NavWindowBase)) is NavWindowBase service && !service.IsActivated)
    {
      node.Expanded = false;
      node.Full = false;
      node.Flags = TreeNodeFlags.ImageOutdated;
      node.Icon = (Icon) null;
      node.ClearChildren();
      node.HasChildren = true;
    }
    else
    {
      if (node.Handle != null && !node.Handle.Visible)
      {
        row = this.TopRow;
        node.Handle.EnsureVisible();
      }
      try
      {
        NavigatorTreeNode navigatorTreeNode = node;
        if (navigatorTreeNode != null && navigatorTreeNode.InTree && navigatorTreeNode.Handle.ChildIndex >= 0 && navigatorTreeNode.Parent != null && navigatorTreeNode.Parent.InTree)
          this.ProcessUpdateChildren(navigatorTreeNode.Parent, (NodeColumnCollection) null, (IList) new List<int>(1)
          {
            navigatorTreeNode.Handle.ChildIndex
          });
        this.MakeNodeUnpopulated(node);
        this.GetChildHandler(node)?.Refresh();
        this.PopulateNode(node);
      }
      finally
      {
        node.Expanded = expanded;
        if (row != null)
          this.TopRow = row;
        NavigatorTreeNode focusedNode = this.FocusedNode;
        if (focusedPath != null && (hasFocus || focusedNode == null || !focusedNode.InTree))
        {
          this.FocusRowChanged -= new EventHandler(this.NavigatorTreeView_FocusRowChanged);
          try
          {
            this.FocusRow = (Row) null;
          }
          finally
          {
            this.FocusRowChanged += new EventHandler(this.NavigatorTreeView_FocusRowChanged);
          }
          this.TryBrowse(focusedPath);
        }
      }
    }
  }

  /// <summary>Загрузить информацию в узел</summary>
  /// <param name="node">Обрабатываемый узел</param>
  public void PopulateNode(NavigatorTreeNode node, bool isSilently = false)
  {
    if (node.Full)
      return;
    int count = this.SelectedRows.Count;
    INodeQuery query = this.GetChildHandler(node)?.GetQuery(ContentType.Folders);
    try
    {
      if (query != null)
      {
        try
        {
          this.SetQueryColumns(query, this._treeColumns);
          query.Execute(node.Bookmark, this.GetFetchCount());
          if (query.RecordCount > 0)
          {
            for (int index = 0; index < query.RecordCount; ++index)
            {
              INodeID recordNodeId = query.GetRecordNodeID(index);
              this.CreateNode(node, recordNodeId, query.GetRecordValues(index), query.GetRawRecordValues(index), this._treeColumns, false);
            }
            node.Bookmark = query.Bookmark;
            node.HasChildren = node.Children.Count > 0;
            node.Full = query.Bookmark == null;
            if (node.Parent != null)
            {
              node.Parent.Children.RebuildHandles();
              if (!isSilently && node.Parent.Handle != null)
                this.UpdateRowData(node.Parent.Handle);
            }
            if (isSilently)
              return;
            this.UpdateRowData(node.Handle);
            if (this.SearchModePopulating)
              return;
            this.UpdateTreeNode(node);
            return;
          }
        }
        catch (Exception ex)
        {
          node.HasChildren = false;
          node.Full = true;
          if (node.Parent != null)
          {
            node.Parent.Children.RebuildHandles();
            if (!isSilently && node.Parent.Handle != null)
              this.UpdateRowData(node.Parent.Handle);
          }
          if (!isSilently)
          {
            this.UpdateRowData(node.Handle);
            this.UpdateTreeNode(node);
          }
          throw;
        }
      }
      node.Bookmark = (object) null;
      node.HasChildren = node.Children.Count != 0;
      node.Full = true;
      if (isSilently)
        return;
      if (!this.SearchModePopulating)
        this.UpdateTreeNode(node);
      this.RaiseSelectedItemsChanged();
    }
    finally
    {
      this.FireAfterPopulateNode(node);
    }
  }

  /// <summary>Загрузить процесс загрузки дочерних ноды переданной ноды и дождаться его окончания</summary>
  /// <param name="node">Обновляемый узел</param>
  /// <param name="millisecondsTimeout">Таймаут ожидания в милисекундах. -1 соответствует бесконечному ожиданию</param>
  /// <returns>True если загрузка прошла успешно или дочерних нод у переданной нет, False если таймаут</returns>
  public bool PopulateNodeAndWaitForFull(NavigatorTreeNode node, int millisecondsTimeout = 20000)
  {
    if (node == null)
      throw new NoNullAllowedException(nameof (node));
    if (!node.HasChildren || node.Full)
      return true;
    this.PopulateNode(node);
    return SpinWait.SpinUntil((Func<bool>) (() => node.Full), millisecondsTimeout);
  }

  /// <summary>Отменяет выполнение всех фоновых заданий обновления.</summary>
  /// <param name="applyCompleted">Применить выполненные задачи</param>
  public void CancelUpdateJobs(bool applyCompleted)
  {
    if (this.DesignMode)
      return;
    this._jobManager.Cancel();
    this.CancelUpdateJobsCore(applyCompleted);
  }

  /// <summary>
  /// Отменяет выполнение всех фоновых заданий обновления, чьи метки совпадают с указанной.
  /// </summary>
  /// <param name="marker">Иденификатор задачи</param>
  /// <param name="applyCompleted">Применить выполненные задачи</param>
  public void CancelUpdateJobs(object marker, bool applyCompleted)
  {
    if (this.DesignMode)
      return;
    this._jobManager.Cancel(marker != null ? marker : (object) "UpdateJob");
    this.CancelUpdateJobsCore(applyCompleted);
  }

  /// <summary>
  /// Анализирует длину очереди завершенных фоновых заданий. Если очередь достигла своего максимума,
  /// то выполняет обновление дерева и очистку очереди.
  /// </summary>
  public void ReduceJobQueue()
  {
    if (this.DesignMode)
      return;
    this._applyUpdatesTimer.Stop();
    if (this._jobQueue.Count >= 1)
      this.UpdateTreeViewCore();
    else
      this._applyUpdatesTimer.Start();
  }

  /// <summary>
  /// Выполнять фоновую проверку наличия дочерних узлов, или нет
  /// </summary>
  /// <remarks>Добавлено Лембиевским О.</remarks>
  protected virtual bool BackgroundTreeTasks => OptimizationSettings.BackgroundTreeTasks;

  public void QueuePlusJob(NavigatorTreeNode node, string jobMarker = null)
  {
    NavigatorFetchChildrenJob fetchChildrenJob = new NavigatorFetchChildrenJob(node, this._treeColumns, 2147483646);
    fetchChildrenJob.Complete += new NavigatorFetchChildrenJob.CompleteEventHandler(this.ApplyPlusJob);
    this._jobManager.Queue((IJob) fetchChildrenJob, jobMarker != null ? (object) jobMarker : (object) "UpdateJob");
  }

  /// <summary>
  /// Чтобы лишний раз не дёргать опрос статуса ВСЕХ команд у ICommandManager,
  /// вызовем опрос только для команд, которые реализуются в дереве Навигатора и списках
  /// </summary>
  /// <param name="forceTarget">true - принудительно назначить дерево активным обработчиком</param>
  public void UpdateCommandManagerItems(bool forceTarget)
  {
    ICommandManager service = this._services != null ? this._services.GetService(typeof (ICommandManager)) as ICommandManager : (ICommandManager) null;
    if (service == null)
      return;
    if (forceTarget)
      service.ActiveTarget = (ICommandTarget) this;
    this.UpdateCommandManagerItems();
  }

  /// <summary>
  /// Чтобы лишний раз не дёргать опрос статуса ВСЕХ команд у ICommandManager,
  /// вызовем опрос только для команд, которые реализуются в дереве Навигатора и списках
  /// </summary>
  public void UpdateCommandManagerItems()
  {
    (this._services != null ? this._services.GetService(typeof (ICommandManager)) as ICommandManager : (ICommandManager) null)?.QueryStatus();
  }

  /// <summary>
  /// Метод позволяет отыскать для указанного узла (с объектом) в дереве
  /// коныигурируемый объект верхнего уровня, в составе которого содержится данный узел.
  /// Если узел не содержит объект, будет возвращено значение Intermech.Consts.UnknownObjectId.
  /// </summary>
  /// <param name="node">Узел, содержащий объект</param>
  /// <returns>Конфигурируемый родительский объект верхнего уровня,
  /// в составе которого содержится указанный узел, или null</returns>
  public IDBTypedObjectID GetTopCompositionObject(NavigatorTreeNode node)
  {
    NavigatorTreeNode topCompositionNode = this.GetTopCompositionNode(node);
    if (topCompositionNode == null)
      return (IDBTypedObjectID) null;
    INode nodeHandler = this.GetNodeHandler(topCompositionNode);
    return nodeHandler != null ? nodeHandler.GetData(topCompositionNode.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
  }

  public new void BeginInit() => base.BeginInit();

  public new void EndInit()
  {
    base.EndInit();
    this.ApplyUserColorSchema();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.IServiceProvider Services
  {
    get => (System.IServiceProvider) this.GetContextMenuServices((System.IServiceProvider) this._services);
    set
    {
      if (this._services.AdvancedProvider != value)
      {
        if (this._services.AdvancedProvider != null)
        {
          this.SetNotificationService((INotificationService) null);
          this._ioDispatcher = (IIODispatcher) null;
        }
        this._services.AdvancedProvider = value;
        if (this._services.AdvancedProvider != null)
        {
          this.SetNotificationService((INotificationService) this._services.GetService(typeof (INotificationService)));
          this._ioDispatcher = this._services.GetService(typeof (IIODispatcher)) as IIODispatcher;
          if (this._ioDispatcher == null)
            this._ioDispatcher = ServicesManager.GetService(typeof (IIODispatcher)) as IIODispatcher;
          this._currentUserAndRole = this._services.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
          if (this._currentUserAndRole == null)
          {
            this._currentUserAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
            if (this._currentUserAndRole != null)
              this._services.AddService(typeof (ICurrentUserAndRole), (object) this._currentUserAndRole);
          }
        }
      }
      NavigatorTreeViewEditingComponent editingComponent = this._navigatorTreeViewEditingComponent;
      if (!(this._services.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service))
        service = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService;
      editingComponent.AttributePropertyDescriberService = service;
    }
  }

  public INode GetChildHandler(NavigatorTreeNode node)
  {
    return node != null && node != this._rootNode ? node.Handler : this.RootHandler;
  }

  public INode GetNodeHandler(NavigatorTreeNode node) => this.GetChildHandler(node.Parent);

  public NodeIDPath GetNodeIDPath(NavigatorTreeNode node)
  {
    NodeIDPath nodeIdPath = new NodeIDPath(this.RootDescriptor);
    for (; node != null; node = node.Parent)
    {
      INodeID nodeId = node.NodeID;
      if (nodeId != null)
        nodeIdPath.Add(nodeId, false);
      else
        break;
    }
    return nodeIdPath;
  }

  /// <summary>Список фокусед узлов, не зависит от checked режима</summary>
  /// <remarks>Для возможности получить Focuded узлы в Checked режиме</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual ISelectedItems FocusedItems
  {
    get
    {
      if (this._selectedItems == null)
        this._selectedItems = new NavigatorTreeViewSelectedItems(this, this.SelectedNodes);
      else
        this._selectedItems.Nodes = this.SelectedNodes;
      return (ISelectedItems) this._selectedItems;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual ISelectedItems SelectedItems
  {
    get
    {
      switch (this.ItemsMode)
      {
        case SelectedItemsMode.FocusedItems:
          return this.FocusedItems;
        case SelectedItemsMode.CheckedItems:
          return this.CheckedItems;
        default:
          return this.CheckedItems.Count <= 0 ? this.FocusedItems : this.CheckedItems;
      }
    }
  }

  public event EventHandler SelectedItemsChanged;

  /// <summary>Текущий режим выбора элементов</summary>
  public SelectedItemsMode ItemsMode { get; set; }

  object IIOSource.Control
  {
    [DebuggerStepThrough] get => (object) this;
    set
    {
    }
  }

  ISelectedItems IIOSource.SelectedItems
  {
    [DebuggerStepThrough] get => this.SelectedItems;
    set
    {
    }
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return this.CommandsProvider.GetMergedCommands(items, viewServices);
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return this.CommandsProvider.GetGroupCommands(items, viewServices);
  }

  public bool QueryStatus(ICommandState commandState)
  {
    if (this.IsDisposed || commandState == null || commandState.Sender != null)
      return false;
    this._checkInOutCommandsProvider = this._checkInOutCommandsProvider == null ? new CheckInOutCommandsProvider() : this._checkInOutCommandsProvider;
    int length = this.CheckedNodes.Length;
    switch (commandState.CommandName)
    {
      case "AdminCancelChanges":
        this._checkInOutCommandsProvider.CheckSelectedItems(this.SelectedItems, (System.IServiceProvider) this.GetContextMenuServices((System.IServiceProvider) this._services));
        commandState.Enabled = this._checkInOutCommandsProvider.AllowAdminCancel;
        return true;
      case "CancelChanges":
        this._checkInOutCommandsProvider.CheckSelectedItems(this.SelectedItems, (System.IServiceProvider) this.GetContextMenuServices((System.IServiceProvider) this._services));
        commandState.Enabled = this._checkInOutCommandsProvider.AllowCancel;
        return true;
      case "CheckIn":
        this._checkInOutCommandsProvider.CheckSelectedItems(this.SelectedItems, (System.IServiceProvider) this.GetContextMenuServices((System.IServiceProvider) this._services));
        commandState.Enabled = this._checkInOutCommandsProvider.AllowCheckIn;
        return true;
      case "CheckOut":
        this._checkInOutCommandsProvider.CheckSelectedItems(this.SelectedItems, (System.IServiceProvider) this.GetContextMenuServices((System.IServiceProvider) this._services));
        commandState.Enabled = this._checkInOutCommandsProvider.AllowCheckOut;
        return true;
      case "Copy":
        bool flag1 = true;
        for (int index = 0; index < this.SelectedItems.Count; ++index)
        {
          if (this.SelectedItems.GetItemData(index, typeof (IDBObjectID)) == null)
          {
            flag1 = false;
            break;
          }
        }
        commandState.Enabled = this.SelectedItems.Count > 0 & flag1;
        return true;
      case "Cut":
        bool flag2 = true;
        for (int index = 0; index < this.SelectedItems.Count; ++index)
        {
          if (this.SelectedItems.GetParentData(index, typeof (IDBObjectID)) == null)
          {
            flag2 = false;
            break;
          }
        }
        commandState.Enabled = this.SelectedItems.Count > 0 & flag2;
        return true;
      case "Delete":
        commandState.Enabled = ContextCommandProvider.CanDeleteObjects(this.SelectedItems, this.Services);
        return true;
      case "Exclude":
        IDBRelationID itemData1 = this.SelectedItems.Count > 0 ? this.SelectedItems.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID : (IDBRelationID) null;
        commandState.Enabled = this.SelectedItems.Count > 0 && itemData1 != null && itemData1.Value != -1L && !this.SelectedItems.IsCollage;
        return true;
      case "Find":
        return this.QueryCommandStatus("SeekInTree");
      case "ParametersCard":
        commandState.Enabled = this.SelectedItems.Count == 1;
        if (commandState.Enabled)
        {
          IDBObjectID itemData2 = this.SelectedItems.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID;
          commandState.Enabled = itemData2 != null;
        }
        return true;
      case "Paste":
        IDBObjectTypedIDCollection dataObject = ((IClipboard) ServicesManager.GetService(typeof (IClipboard))).GetDataObject() as IDBObjectTypedIDCollection;
        commandState.Enabled = dataObject != null && dataObject.Count > 0;
        return true;
      case "Print":
      case "PrintDocument":
      case "ViewDocument":
        commandState.Enabled = this.SelectedItems.Count > 0;
        return true;
      case "Refresh":
        commandState.Enabled = this.FocusedNode != null;
        return true;
      case "SaveChanges":
        this._checkInOutCommandsProvider.CheckSelectedItems(this.SelectedItems, (System.IServiceProvider) this.GetContextMenuServices((System.IServiceProvider) this._services));
        commandState.Enabled = this._checkInOutCommandsProvider.AllowSave;
        return true;
      default:
        return this.QueryStatusCurrentContext(commandState);
    }
  }

  public virtual bool Execute(ICommandState commandState) => this.Execute(commandState.CommandName);

  protected override void InitializeStyles()
  {
    base.InitializeStyles();
    this.AllowMultiSelect = false;
    this.AllowUserPinnedColumns = false;
    this.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.LineStyle = LineStyle.Dot;
    this.RowEvenStyle.WordWrap = false;
    this.RowOddStyle.WordWrap = false;
    this.RowSelectedStyle.WordWrap = false;
    this.RowStyle.BorderColor = System.Drawing.SystemColors.Control;
    this.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.RowStyle.BorderWidth = 1;
    this.RowStyle.WordWrap = false;
    this.SelectBeforeEdit = true;
    this.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.ShowRootRow = false;
    this.SuppressErrorMessages = true;
    if (this._navGraphicsCache == null)
      return;
    this.ApplyUserColorSchema();
    this._navGraphicsCache.UIColorsSchemeChanged += new EventHandler(this.NavGraphicsCache_UIColorsSchemeChanged);
  }

  private void ApplyUserColorSchema()
  {
    if (this._navGraphicsCache == null)
      return;
    this.RowStyle.ForeColor = this._navGraphicsCache.CurrentColorsScheme.Foreground;
    this.RowStyle.BackColor = this._navGraphicsCache.CurrentColorsScheme.Background;
    this.RowStyle.BorderColor = this._navGraphicsCache.CurrentColorsScheme.Background;
    this.RowSelectedStyle.ForeColor = this._navGraphicsCache.CurrentColorsScheme.ForegroundSelected;
    this.RowSelectedStyle.BackColor = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelected;
    this.RowSelectedStyle.BorderColor = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelected;
    this.RowSelectedUnfocusedStyle.ForeColor = this._navGraphicsCache.CurrentColorsScheme.ForegroundSelectedInactive;
    this.RowSelectedUnfocusedStyle.BackColor = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelectedInactive;
    this.RowSelectedUnfocusedStyle.BorderColor = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelectedInactive;
  }

  public override ContextMenuStrip CreateHeaderContextMenu(bool addToContainer)
  {
    return this._headerContextMenuStrip;
  }

  /// <summary>Отработало событие горизонтальной прокрутки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected override void OnHorizontalScroll(object sender, ScrollEventArgs e)
  {
    base.OnHorizontalScroll(sender, e);
    this.CancelUpdateJobs((object) "UpdateJob", true);
    this.QueueUpdateJobs(true);
  }

  /// <summary>Отработало событие вертикальной прокрутки</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  protected override void OnVerticalScroll(object sender, ScrollEventArgs e)
  {
    this._topRowIndex = this.TopRowIndex;
    base.OnVerticalScroll(sender, e);
    if (this._topRowIndex == this.TopRowIndex)
      return;
    this.TreeTopNodeChanged();
  }

  protected override Infralution.Controls.VirtualTree.RowWidget CreateRowWidget(
    PanelWidget panelWidget,
    Row row)
  {
    return (Infralution.Controls.VirtualTree.RowWidget) new NavigatorRowWidget(panelWidget, row);
  }

  protected override CellWidget CreateCellWidget(Infralution.Controls.VirtualTree.RowWidget rowWidget, Column column)
  {
    CellWidget cellWidget = ServiceUtils.GetService<INavigatorTreeViewCellWidgetProvider>((object) this.Services, false)?.GetCellWidget(this, rowWidget, column);
    if (cellWidget != null)
      return cellWidget;
    if (column is NavigatorTreeColumn column1 && rowWidget != null && rowWidget.Row != null && rowWidget.Row.Item != null && rowWidget.Row.Item is NavigatorTreeNode node && node.Handler != null)
    {
      INode nodeHandler = this.GetNodeHandler(node);
      if (nodeHandler != null && nodeHandler is INodeCustomUI nodeCustomUi)
      {
        CellWidget customCellWidget = nodeCustomUi.GetCustomCellWidget(rowWidget, column1);
        if (customCellWidget != null)
          return customCellWidget;
      }
    }
    return column1 != null && column1.NavigatorColumn.ID.Equals((object) "F_STATUSES") && (column1.NavigatorColumn.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid || column1.NavigatorColumn.SchemeGuid == Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid) ? (CellWidget) new StatusesCellWidget(rowWidget, column) : (CellWidget) new NavigatorCellWidget(rowWidget, column);
  }

  /// <summary>Фокус пришёл в дерево "Навигатора"</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnEnter(EventArgs e)
  {
    base.OnEnter(e);
    this.TreeFocused = true;
    if (this.IsSimpleSelectedItemsSuppoted())
    {
      if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
        ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
      ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) this._selectedItems);
    }
    this.UpdateCommandManagerItems();
  }

  /// <summary>Фокус покинул дерево "Навигатора"</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnLeave(EventArgs e)
  {
    if (this.IsSimpleSelectedItemsSuppoted() && ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
      ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
    this.TreeFocused = false;
    base.OnLeave(e);
    (this._services != null ? this._services.GetService(typeof (ICommandManager)) as ICommandManager : (ICommandManager) null)?.QueryStatus();
  }

  protected override bool ProcessEditCmdKeys(Keys keys)
  {
    return !this._navigatorTreeViewEditingComponent.IsEditorVisible && base.ProcessEditCmdKeys(keys);
  }

  protected override bool ProcessNormalCmdKeys(Keys keys)
  {
    return !this._navigatorTreeViewEditingComponent.IsEditorVisible && base.ProcessNormalCmdKeys(keys);
  }

  private void NavigatorTreeView_GetChildPolicy(object sender, GetChildPolicyEventArgs e)
  {
    this.TreeGetChildPolicy(sender, e);
  }

  private void NavigatorTreeView_GetChildren(object sender, GetChildrenEventArgs e)
  {
    this.TreeGetChildren(sender, e);
  }

  private void NavigatorTreeView_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (this.RootRow == null || !(e.Row.Item is NavigatorTreeNode node))
      return;
    node.Handle = e.Row;
    INodeID nodeId = node.NodeID;
    INode nodeHandler = this.GetNodeHandler(node);
    nodeHandler?.GetData(nodeId, typeof (IDBObjectID));
    IDBCheckedOutByID data1 = nodeHandler != null ? nodeHandler.GetData(nodeId, typeof (IDBCheckedOutByID)) as IDBCheckedOutByID : (IDBCheckedOutByID) null;
    INodeStatusesInfo service = nodeHandler != null ? (INodeStatusesInfo) nodeHandler.GetService(typeof (INodeStatusesInfo)) : (INodeStatusesInfo) null;
    IContextAware contextAware = nodeHandler as IContextAware;
    IDBObjectFiltrationState data2 = nodeHandler != null ? nodeHandler.GetData(nodeId, typeof (IDBObjectFiltrationState)) as IDBObjectFiltrationState : (IDBObjectFiltrationState) null;
    ObjectFiltrationState columnValue = data2 != null ? data2.State : ObjectFiltrationState.fsNotRequired;
    Font font = service != null ? service.GetFont(contextAware?.Services, nodeId, (object) columnValue, this.Font) : this.Font;
    bool flag = false;
    if (this._currentUserAndRole != null && data1 != null && (data1.CheckedOutBy > 0L || data1.ObjectID < 0L))
    {
      StyleDelta styleDelta = data1.CheckedOutBy == this._currentUserAndRole.UserID ? this.CheckedOutByCurrentUserStyleDelta(data1.ObjectID) : this.CheckedOutByOtherUserStyleDelta(data1.ObjectID);
      if (styleDelta != null)
      {
        StyleDelta delta1 = new StyleDelta();
        delta1.BackColor = styleDelta.BackColor;
        delta1.GradientColor = styleDelta.GradientColor;
        delta1.GradientMode = styleDelta.GradientMode;
        delta1.ForeColor = styleDelta.ForeColor;
        if (font != null)
          delta1.Font = font;
        e.CellData.OddStyle = new Infralution.Controls.Style(e.Row.Tree.RowOddStyle, delta1);
        StyleDelta delta2 = new StyleDelta();
        delta2.BackColor = styleDelta.BackColor;
        delta2.GradientColor = styleDelta.GradientColor;
        delta2.GradientMode = styleDelta.GradientMode;
        delta2.ForeColor = styleDelta.ForeColor;
        if (font != null)
          delta2.Font = font;
        e.CellData.EvenStyle = new Infralution.Controls.Style(e.Row.Tree.RowEvenStyle, delta2);
        flag = true;
      }
    }
    if (!flag && font != null)
    {
      e.CellData.OddStyle = new Infralution.Controls.Style(e.Row.Tree.RowOddStyle, new StyleDelta()
      {
        Font = font
      });
      e.CellData.EvenStyle = new Infralution.Controls.Style(e.Row.Tree.RowEvenStyle, new StyleDelta()
      {
        Font = font
      });
    }
    if (!(e.Column is NavigatorTreeColumn column))
      return;
    e.CellData.Value = node.GetCellValue(column.AbsoluteIndex);
  }

  protected virtual Image GetNodeImage(NavigatorTreeNode node)
  {
    return Images32x16_Cache.GetImage32x16(node.NodeID.CategoryID, node.NodeID.TypeID, node);
  }

  private void NavigatorTreeView_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is NavigatorTreeNode node))
      return;
    node.Handle = e.Row;
    if (e.Row != null & e.Row.ParentRow != null && e.Row.ParentRow.Item is NavigatorTreeNode navigatorTreeNode && navigatorTreeNode.InTree && navigatorTreeNode.Full && e.Row.ChildIndex >= 0 && e.Row.ChildIndex < navigatorTreeNode.Children.Count)
      node.NodeID = navigatorTreeNode.Children[e.Row.ChildIndex].NodeID;
    if (node.NodeID == null)
      return;
    INode handler = node.Handler;
    INodeCustomUI nodeCustomUi = handler == null ? (INodeCustomUI) null : handler as INodeCustomUI;
    Image prefixIcon = nodeCustomUi == null ? (Image) null : nodeCustomUi.GetPrefixIcon();
    int width = prefixIcon != null ? prefixIcon.Width : 0;
    int num = width;
    Image image = nodeCustomUi?.GetMainIcon() ?? this.GetNodeImage(node);
    if (image != null)
      num += image.Width;
    if (!this.DisableCheckedOutColumn && this.IsRootNodeObject())
      num += NavigatorCellWidget.CheckOutWidth + NavigatorCellWidget.VersionWidth + 6;
    e.RowData.ImageSize = num;
    if (prefixIcon != null && image == null)
      e.RowData.Image = prefixIcon;
    else if (prefixIcon == null && image != null)
    {
      e.RowData.Image = image;
    }
    else
    {
      Bitmap bitmap = new Bitmap(e.RowData.ImageSize, 16 /*0x10*/);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        if (prefixIcon != null)
          graphics.DrawImageUnscaled(prefixIcon, 0, 0);
        if (image != null)
          graphics.DrawImageUnscaled(image, prefixIcon != null ? width : 0, 0);
        graphics.Save();
        graphics.Flush();
      }
      e.RowData.Image = (Image) bitmap;
    }
  }

  private void NavigatorTreeView_SortColumnChanged(object sender, EventArgs e)
  {
    if (this.BeforeColumnsSorting != null)
      this.BeforeColumnsSorting((object) this, e);
    if (this._lockManualSortEvent != 0)
      return;
    this.SetColumns(this.ReflectTreeColumsChanges(), false);
  }

  private void NavigatorTreeView_SelectionChanged(object sender, EventArgs e)
  {
    this.TreeSelectionChanged(sender, e);
  }

  private void NavigatorTreeView_BeforeFocusRowChanged(
    object sender,
    BeforeFocusedRowChangedEventArgs e)
  {
    if (this._lockFocusedItemEvent > 0)
      return;
    if (this.CancelSelectionChanging)
    {
      this._previousFocusedRow = e.PrevRow;
      this._previousTopRow = this.TopRow;
    }
    else
    {
      this._previousNode = e == null || e.PrevRow == null ? (NavigatorTreeNode) null : e.PrevRow.Item as NavigatorTreeNode;
      NavigatorTreeNode navigatorTreeNode = e == null || e.NewRow == null ? (NavigatorTreeNode) null : e.NewRow.Item as NavigatorTreeNode;
      if (this.BeforeFocusNode == null || this._previousNode == navigatorTreeNode)
        return;
      this.BeforeFocusNode((object) this, new NavigatorTreeNodeEventArgs(this._previousNode));
    }
  }

  private void NavigatorTreeView_FocusRowChanged(object sender, EventArgs e)
  {
    if (this._lockFocusedItemEvent > 0)
      return;
    if (this.CancelSelectionChanging)
    {
      this.CancelSelectionChanging = false;
      this._lockFocusedItemEvent = 1;
      try
      {
        this.FocusRow = this._previousFocusedRow;
        this.TopRow = this._previousTopRow;
      }
      finally
      {
        this._lockFocusedItemEvent = 0;
      }
    }
    else
    {
      this.UpdateCommandsTable();
      this.UpdateCommandManagerItems();
      NavigatorTreeNode node = this.FocusRow != null ? this.FocusRow.Item as NavigatorTreeNode : (NavigatorTreeNode) null;
      if (this.AfterFocusNode != null)
        this.AfterFocusNode((object) this, new NavigatorTreeNodeEventArgs(node));
      this._previousNode = (NavigatorTreeNode) null;
    }
  }

  private void NavigatorTreeView_RowExpand(object sender, RowEventArgs e)
  {
    this.TreeRowExpand(sender, e);
  }

  private void NavigatorTreeView_RowCollapse(object sender, RowEventArgs e)
  {
    if (this.RootRow == null)
      return;
    NavigatorTreeNode node = e.Row.Item as NavigatorTreeNode;
    this.CancelUpdateJobs((object) "UpdateJob", true);
    this.QueueUpdateJobs(true);
    this.UpdateCommandsTable();
    this.UpdateCommandManagerItems();
    if (this.AfterCollapse == null || node == null)
      return;
    this.AfterCollapse((object) this, new NodeEventArgs(node));
  }

  private void NavigatorTreeView_CellMouseUp(object sender, MouseEventArgs e)
  {
    this._dragBoxFromMouseDown = Rectangle.Empty;
  }

  private void NavigatorTreeView_MouseMove(object sender, MouseEventArgs e)
  {
    if (this._disableTreeEvents || this.DisableDragAndDrop || (e.Button & MouseButtons.Left) != MouseButtons.Left)
      return;
    this.TreeStartDragDrop(e.Location);
  }

  private void NavigatorTreeView_MouseDown(object sender, MouseEventArgs e)
  {
    this.TreeMouseDown(sender, e);
  }

  private void NavigatorTreeView_MouseUp(object sender, MouseEventArgs e)
  {
    this.TreeMouseUp(sender, e);
  }

  private void NavigatorTreeView_MouseWheel(object sender, MouseEventArgs e)
  {
    this.CancelUpdateJobs((object) "UpdateJob", true);
    this.QueueUpdateJobs(true);
  }

  protected virtual void NavigatorTreeView_KeyDown(object sender, KeyEventArgs e)
  {
    this._topRowMonitorTimer.Enabled = false;
    this._topRowMonitorTimer.Enabled = true;
    if (e.KeyData == Keys.Escape)
      this._dragBoxFromMouseDown = Rectangle.Empty;
    if (e.KeyCode == Keys.Apps && !this.DisableKeyUpEvents)
    {
      NavigatorTreeNode focusedNode = this.FocusedNode;
      if (focusedNode != null && focusedNode.InTree)
      {
        Infralution.Controls.VirtualTree.RowWidget rowWidget = this.PinnedPanel.GetRowWidget(focusedNode.Handle);
        Rectangle bounds = rowWidget.Bounds;
        int x = bounds.Left + 3;
        bounds = rowWidget.Bounds;
        int y = bounds.Top + 3;
        MouseEventArgs e1 = new MouseEventArgs(MouseButtons.Right, 1, x, y, 0);
        this.TreeMouseUp(sender, e1);
        e.Handled = true;
        return;
      }
    }
    if (e.KeyCode == Keys.Space && !this.DisableKeyUpEvents && this.CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.None)
    {
      NavigatorTreeNode focusedNode = this.FocusedNode;
      if (focusedNode != null && focusedNode.InTree)
      {
        switch (focusedNode.CheckState)
        {
          case CheckState.Unchecked:
          case CheckState.Indeterminate:
            focusedNode.CheckState = CheckState.Checked;
            break;
          case CheckState.Checked:
            focusedNode.CheckState = CheckState.Unchecked;
            break;
        }
        e.Handled = true;
        return;
      }
    }
    if (this._ioDispatcher == null || this.DisableKeyUpEvents)
      return;
    IOEvent Event = new IOEvent((IIOSource) this, IOEventFlags.efNone, IOEventType.evKeyUp, (object) e, (object) this.FocusedPath);
    NavigatorTreeNode[] nodes = this.BeforeExecuteMenuCommand();
    try
    {
      this._ioDispatcher.ProcessEvent((IIOEvent) Event);
    }
    finally
    {
      this.AfterExecuteMenuCommand(nodes);
    }
  }

  protected virtual void NavigatorTreeView_KeyUp(object sender, KeyEventArgs e)
  {
  }

  private void NavigatorTreeView_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
  {
    this.TreeDragEnter(sender, e);
  }

  private void NavigatorTreeView_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
  {
    this.TreeDragOver(sender, e);
  }

  private void NavigatorTreeView_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
  {
    this.TreeDragDrop(sender, e);
  }

  private void NavigatorTreeView_Resize(object sender, EventArgs e)
  {
    this.CancelUpdateJobs((object) "UpdateJob", true);
    this.QueueUpdateJobs(true);
  }

  private void NavigatorTreeView_Enter(object sender, EventArgs e)
  {
    if (ServicesManager.GetService(typeof (NavigatorTreeView)) != null)
      ServicesManager.RemoveService(typeof (NavigatorTreeView));
    ServicesManager.AddService(typeof (NavigatorTreeView), (object) this);
    this.AddSimpleSelectionItemsToGlobalServiceContainer();
  }

  private void NavigatorTreeView_Leave(object sender, EventArgs e)
  {
    if (ServicesManager.GetService(typeof (NavigatorTreeView)) == null)
      return;
    ServicesManager.RemoveService(typeof (NavigatorTreeView));
  }

  private void ChangeColumnsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (this._currentCommandsTable == null)
      return;
    CommandLink commandLink = this._currentCommandsTable["SetupColumns"];
    if (commandLink == null || commandLink.CommandInfo == null)
      return;
    commandLink.CommandInfo.ClickHandler((ISelectedItems) this._selectedItems, this._currentContextMenuServiceProvider, (object) null);
  }

  private void NavGraphicsCache_UIColorsSchemeChanged(object sender, EventArgs e)
  {
    this.RowStyle.ForeColor = this._navGraphicsCache.CurrentColorsScheme.Foreground;
    this.RowStyle.BackColor = this._navGraphicsCache.CurrentColorsScheme.Background;
    this.RowStyle.BorderColor = this._navGraphicsCache.CurrentColorsScheme.Background;
    this.RowSelectedStyle.ForeColor = this._navGraphicsCache.CurrentColorsScheme.ForegroundSelected;
    this.RowSelectedStyle.BackColor = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelected;
    this.RowSelectedStyle.BorderColor = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelected;
    this.RowSelectedUnfocusedStyle.ForeColor = this._navGraphicsCache.CurrentColorsScheme.ForegroundSelectedInactive;
    this.RowSelectedUnfocusedStyle.BackColor = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelectedInactive;
    this.RowSelectedUnfocusedStyle.BorderColor = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelectedInactive;
  }

  private void NotificationService_ManagedRelationsInsert(object sender, NotificationEventArgs e)
  {
    if (e.EventName != "ManagedRelationsInsert" || !(e is DBRelationsManagedEventArgs managedEventArgs) || managedEventArgs.Control != this || managedEventArgs.RelationIDs == null || managedEventArgs.RelationIDs.Count == 0)
      return;
    NavigatorTreeNode node1 = managedEventArgs != null ? managedEventArgs.Node as NavigatorTreeNode : (NavigatorTreeNode) null;
    NavigatorTreeNode navigatorTreeNode1 = node1;
    if (managedEventArgs == null || node1 == null)
      return;
    if (managedEventArgs.InsertPosition == NodesInsertPosition.After || managedEventArgs.InsertPosition == NodesInsertPosition.Before)
      navigatorTreeNode1 = node1.Parent;
    if (navigatorTreeNode1 == null)
      return;
    NavigatorTreeNode navigatorTreeNode2 = navigatorTreeNode1 ?? (NavigatorTreeNode) null;
    INodeIDCreator handler = navigatorTreeNode2 != null ? navigatorTreeNode2.Handler as INodeIDCreator : (INodeIDCreator) null;
    if (handler == null)
      return;
    INode childHandler = this.GetChildHandler(navigatorTreeNode1);
    if (childHandler is ObjectNode && ((CompositeNode) childHandler).FolderSlots.Any<PartSlot>((System.Func<PartSlot, bool>) (o => o.Object is ObjectAndObjectGroupNodePart)))
      return;
    NodeColumnCollection treeColumns = this._treeColumns;
    int num = this.IndexOf(node1);
    for (int index = 0; index < managedEventArgs.RelationIDs.Count; ++index)
    {
      long relationId = managedEventArgs.RelationIDs[index];
      INodeID nodeId = handler.Create(relationId);
      NodeIDCollection nodeIDs = new NodeIDCollection();
      nodeIDs.Add(nodeId);
      INodeQuery query = childHandler.GetQuery(ContentType.Folders);
      if (query != null)
      {
        this.SetQueryColumns(query, treeColumns);
        query.Execute(nodeIDs);
        if (query.RecordCount == 0)
          break;
        NavigatorTreeNode node2 = this.CreateNode(navigatorTreeNode1, query.GetRecordNodeID(0), query.GetRecordValues(0), query.GetRawRecordValues(0), treeColumns, false);
        if (node2 == null)
          break;
        switch (managedEventArgs.InsertPosition)
        {
          case NodesInsertPosition.Start:
            this.SetNodeIndex(node2, index);
            break;
          case NodesInsertPosition.Before:
            this.SetNodeIndex(node2, num + index);
            break;
          case NodesInsertPosition.After:
            this.SetNodeIndex(node2, num + 1 + index);
            break;
          default:
            this.SetNodeIndex(node2, node1.Parent.Children.Count - 1);
            break;
        }
        navigatorTreeNode1.HasChildren = navigatorTreeNode1.Children.Count != 0;
        NavigatorTreeNode navigatorTreeNode3 = navigatorTreeNode1;
        if (navigatorTreeNode3 != null && navigatorTreeNode3.Handle != null)
          this.RebuildTree();
      }
    }
  }

  private void NotificationService_EventFired(object sender, NotificationEventArgs e)
  {
    INotificationServiceStatesHolder service1 = this.Services.GetService(typeof (INotificationServiceStatesHolder)) as INotificationServiceStatesHolder;
    NavWindowBase service2 = this._services.GetService(typeof (NavWindowBase)) as NavWindowBase;
    if (service1 != null && ((service1.States & NotificationServiceStates.InactiveDialog) == NotificationServiceStates.InactiveDialog || (service1.States & NotificationServiceStates.InactiveForm) == NotificationServiceStates.InactiveForm) && (!(e is ICriticalEventArgs criticalEventArgs) || !criticalEventArgs.IsCritical) && !NotificationEventNames.CriticalEventNames.Contains(e.EventName) && (service2 == null || !service2.IsOpen))
      return;
    if (e.EventName == "AttributeRemoved" && e is DBAttributesEventArgs)
    {
      DBAttributesEventArgs attributesEventArgs = (DBAttributesEventArgs) e;
      if (attributesEventArgs.AttributeIDs != null)
      {
        foreach (NodeColumn treeColumsChange in (List<NodeColumn>) this.ReflectTreeColumsChanges())
        {
          if (treeColumsChange.Attribute != null && attributesEventArgs.AttributeIDs.Contains(treeColumsChange.Attribute.AttributeID))
            this.RemoveNodeColumn(treeColumsChange);
        }
      }
    }
    if (this.Nodes == null || this.Nodes.Count <= 0)
      return;
    this.CancelUpdateJobs(true);
    if (e.EventName == "ApplicationClosing")
      return;
    bool disableTreeEvents = this._disableTreeEvents;
    NodeIDPath nodeIdPath = this.GetNodeIDPath(this.FocusedNode);
    try
    {
      this._disableTreeEvents = true;
      NavigatorTreeNode rootNode = this.RootNode;
      this.NotifyRootNode(rootNode, sender, e);
      if (rootNode.InTree)
      {
        bool alwaysShowFirstTab = UISettings.AlwaysShowFirstTab;
        UISettings.AlwaysShowFirstTab = false;
        try
        {
          this.NotifyNode(rootNode, sender, e);
        }
        finally
        {
          if (!UISettings.AlwaysShowFirstTab)
            UISettings.AlwaysShowFirstTab = alwaysShowFirstTab;
        }
      }
    }
    finally
    {
      this._disableTreeEvents = disableTreeEvents;
      this.RebuildTree();
      this.QueueUpdateJobs(false);
      if (nodeIdPath != null && this.DisableChangeSelectedNodeDuringNotificationProcessing)
        this.TryBrowse(nodeIdPath);
    }
    if (this.Nodes.Count == 0)
      this.RaiseClearTreeEvent();
    else if (this.RootNode != null && !this.RootNode.Expanded)
    {
      this.RootNode.Expanded = true;
      this.TryBrowse(nodeIdPath);
    }
    if (!(e.EventName == "ObjectsCheckedIn") && !(e.EventName == "ObjectsCheckedOut") && !(e.EventName == "ObjectsChangesCancelled"))
      return;
    this.UpdateCommandsTable();
  }

  /// <summary>Расшариваем корректные колонки</summary>
  /// <param name="record">Состояние колонк</param>
  /// <param name="columns">Коллекция колонок "Навигатора"</param>
  private StatesRecord ShareValidColumns(StatesRecord record, NodeColumnCollection columns)
  {
    StatesRecord record1 = new StatesRecord(record);
    for (int index1 = 0; index1 < columns.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.Columns.Count; ++index2)
      {
        if (this.Columns[index2] is NavigatorTreeColumn && (this.Columns[index2] as NavigatorTreeColumn).NavigatorColumn.SchemeGuid.Equals(columns[index1].SchemeGuid) && (this.Columns[index2] as NavigatorTreeColumn).NavigatorColumn.ID.Equals(columns[index1].ID))
        {
          record1[(this.Columns[index2] as NavigatorTreeColumn).AbsoluteIndex] = true;
          break;
        }
      }
    }
    StatesRecord statesRecord = this._statesManager.Share(record1);
    this._statesManager.Unshare(record);
    return statesRecord;
  }

  /// <summary>Вернуть коллекцию ошибочных колонок в дереве</summary>
  /// <param name="records">Состояния колонок</param>
  /// <returns>Коллекция ошибочных колонок в дереве</returns>
  private NodeColumnCollection TreeGetInvalidColumns(StatesRecordCollection records)
  {
    StatesRecord statesRecord = new StatesRecord(this.Columns.Count, true);
    for (int index1 = 0; index1 < this.Columns.Count; ++index1)
    {
      for (int index2 = 0; index2 < records.Count; ++index2)
      {
        if (!records[index2][index1])
        {
          statesRecord[index1] = false;
          break;
        }
      }
    }
    NodeColumnCollection invalidColumns = new NodeColumnCollection();
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    for (int index = 0; index < statesRecord.Length; ++index)
    {
      if (!statesRecord[index] && this.Columns[index] is NavigatorTreeColumn column)
      {
        NodeColumn navigatorColumn = column.NavigatorColumn;
        if (column.Sortable)
          navigatorColumn.SortOrder = column.SortDirection != ListSortDirection.Ascending ? NodeColumnSortOrder.Descending : NodeColumnSortOrder.Ascending;
        navigatorColumn.Width = column.Width;
        invalidColumns.Add(navigatorColumn);
      }
    }
    return invalidColumns;
  }

  /// <summary>
  /// Поддерживается ли взаимодействие с сервисом IsSimpleSelectedItems
  /// </summary>
  private bool IsSimpleSelectedItemsSuppoted()
  {
    IViewState service = this._services != null ? this._services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    return service != null && (service.ViewState & ViewStateFlags.InDialog) != ViewStateFlags.InDialog && (service.ViewState & ViewStateFlags.InObjectCreatorDialog) != ViewStateFlags.InObjectCreatorDialog && (service.ViewState & ViewStateFlags.InParametersCard) != ViewStateFlags.InParametersCard && (service.ViewState & ViewStateFlags.InSelectionWindow) != ViewStateFlags.InSelectionWindow;
  }

  /// <summary>Получить ссылки на необходимые сервисы</summary>
  protected virtual void InitTreeServices()
  {
    this._services = new AdvancedServiceContainer();
    this._contextMenuHelper = new NavigatorTreeViewContextMenuHelper(this);
    this._services.AddService(typeof (INavigatorTreeViewContextMenuHelper), (object) this._contextMenuHelper);
    this._notificationService = (INotificationService) null;
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._categoryTypeIconService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._viewState = this._services.GetService(typeof (IViewState)) as IViewState;
  }

  /// <summary>Назначить дереву обработчики событий</summary>
  private void InitEventHandlers()
  {
    this.GetChildPolicy += new GetChildPolicyHandler(this.NavigatorTreeView_GetChildPolicy);
    this.GetChildren += new GetChildrenHandler(this.NavigatorTreeView_GetChildren);
    this.GetCellData += new GetCellDataHandler(this.NavigatorTreeView_GetCellData);
    this.GetRowData += new GetRowDataHandler(this.NavigatorTreeView_GetRowData);
    this.SortColumnChanged += new EventHandler(this.NavigatorTreeView_SortColumnChanged);
    this.SelectionChanged += new EventHandler(this.NavigatorTreeView_SelectionChanged);
    this.SelectionChanging += new SelectionChangingHandler(this.NavigatorTreeView_SelectionChanging);
    this.BeforeFocusRowChanged += new BeforeFocusedRowChangedHandler(this.NavigatorTreeView_BeforeFocusRowChanged);
    this.FocusRowChanged += new EventHandler(this.NavigatorTreeView_FocusRowChanged);
    this.RowExpand += new RowEventHandler(this.NavigatorTreeView_RowExpand);
    this.RowCollapse += new RowEventHandler(this.NavigatorTreeView_RowCollapse);
    this.CellMouseUp += new MouseEventHandler(this.NavigatorTreeView_CellMouseUp);
    this.MouseMove += new MouseEventHandler(this.NavigatorTreeView_MouseMove);
    this.MouseDown += new MouseEventHandler(this.NavigatorTreeView_MouseDown);
    this.MouseUp += new MouseEventHandler(this.NavigatorTreeView_MouseUp);
    this.MouseWheel += new MouseEventHandler(this.NavigatorTreeView_MouseWheel);
    this.KeyDown += new KeyEventHandler(this.NavigatorTreeView_KeyDown);
    this.KeyUp += new KeyEventHandler(this.NavigatorTreeView_KeyUp);
    this.DragEnter += new System.Windows.Forms.DragEventHandler(this.NavigatorTreeView_DragEnter);
    this.DragOver += new System.Windows.Forms.DragEventHandler(this.NavigatorTreeView_DragOver);
    this.DragDrop += new System.Windows.Forms.DragEventHandler(this.NavigatorTreeView_DragDrop);
    this.Resize += new EventHandler(this.NavigatorTreeView_Resize);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal bool CancelSelectionChanging { get; set; }

  private void NavigatorTreeView_SelectionChanging(object sender, SelectionChangingEventArgs e)
  {
    if (!this.CancelSelectionChanging)
      return;
    e.Cancel = true;
  }

  private bool IsRootNodeObject()
  {
    INodeID rootNodeId = this.RootNodeID;
    IDBObjectID data = rootNodeId == null || this.RootHandler == null ? (IDBObjectID) null : this.RootHandler.GetData(rootNodeId, typeof (IDBObjectID)) as IDBObjectID;
    return data != null && data.Value != 0L;
  }

  private void SetNotificationService(INotificationService value)
  {
    if (this._notificationService == value)
      return;
    if (this._notificationService != null)
    {
      this._notificationService.Unsubscribe("ManagedRelationsInsert", new NotificationEventHandler(this.NotificationService_ManagedRelationsInsert));
      this._notificationService.Unsubscribe(new NotificationEventHandler(this.NotificationService_EventFired));
    }
    this._notificationService = value;
    if (this._notificationService == null)
      return;
    this._notificationService.Subscribe(new NotificationEventHandler(this.NotificationService_EventFired));
    this._notificationService.Subscribe("ManagedRelationsInsert", new NotificationEventHandler(this.NotificationService_ManagedRelationsInsert));
  }

  private int GetFetchCount()
  {
    if (this.DisablePacketsReading)
      return 2147483646;
    if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service)
      return service.MaxRows;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.MaxRows;
  }

  private static bool TestMethodIsSerializable(MethodInfo method)
  {
    if (!method.IsPublic || !(method.DeclaringType != (System.Type) null))
      return false;
    return method.DeclaringType.IsPublic || NavigatorTreeView.TestTypeIsNestedPublic(method.DeclaringType);
  }

  private static bool TestTypeIsNestedPublic(System.Type type)
  {
    return type.IsNestedPublic && type.DeclaringType != (System.Type) null && (type.DeclaringType.IsPublic || NavigatorTreeView.TestTypeIsNestedPublic(type.DeclaringType));
  }

  private void ApplySettings()
  {
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>())
      return;
    this.Font = ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_TreeFont) as Font;
    int num = FontHelper.MeasureStringFast(this.Font, "Ay").Height + 6;
    if (this.HeaderHeight != num)
      this.HeaderHeight = num;
    if (this.RowHeight == num)
      return;
    this.RowHeight = num;
  }

  /// <summary>Освобождаем ресурсы, занятые деревом</summary>
  private void DeactivateTreeResources()
  {
    this.RootHandler = (INode) null;
    this.RootDescriptor = (IDescriptor) null;
    if (this._treeColumns != null)
      this._treeColumns.Clear();
    if (this._rootNode != null && this._rootNode.Children != null)
      this._rootNode.Children.Clear();
    this._rootNode = (NavigatorTreeNode) null;
    this._selectedItems = (NavigatorTreeViewSelectedItems) null;
    this._dragdropItem = (NavigatorTreeViewSelectedItem) null;
  }

  /// <summary>Освобождение ссылок на сервисы</summary>
  private void DeactivateTreeServices()
  {
    this.SetNotificationService((INotificationService) null);
    if (this._navGraphicsCache != null)
      this._navGraphicsCache.UIColorsSchemeChanged -= new EventHandler(this.NavGraphicsCache_UIColorsSchemeChanged);
    this._navGraphicsCache = (INavGraphicsCache) null;
  }

  /// <summary>
  /// Получить цвета для ячейки объекта, взятого на изменение другим пользователем
  /// </summary>
  /// <returns></returns>
  private StyleDelta CheckedOutByOtherUserStyleDelta(long currentID)
  {
    return new StyleDelta()
    {
      BackColor = this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkStartColor,
      GradientColor = (this._navGraphicsCache.CurrentColorsScheme.Gradient & GradientUsing.CheckedOutOther) == GradientUsing.CheckedOutOther ? this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkEndColor : this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkStartColor,
      GradientMode = this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherGradientMode,
      ForeColor = this._navGraphicsCache.CurrentColorsScheme.ForegroundCheckedOutOther
    };
  }

  /// <summary>Изменилась выделенная строка в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void TreeSelectionChanged(object sender, EventArgs e)
  {
    this.UpdateCommandsTable();
    if (this._lockSelectionChanged > 0)
      return;
    if (this._selectedItems == null)
      this._selectedItems = new NavigatorTreeViewSelectedItems(this, this.SelectedNodes);
    else
      this._selectedItems.Nodes = this.SelectedNodes;
    this.AddSimpleSelectionItemsToGlobalServiceContainer();
    this.RaiseSelectedItemsChanged();
  }

  private void AddSimpleSelectionItemsToGlobalServiceContainer()
  {
    if (!this.IsSimpleSelectedItemsSuppoted())
      return;
    if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
      ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
    ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) this._selectedItems);
  }

  /// <summary>Отпущена клавиша мышки в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void TreeMouseUp(object sender, MouseEventArgs e)
  {
    this._dragBoxFromMouseDown = Rectangle.Empty;
    this._mouseDownOnHeader = false;
    if (e.Button != MouseButtons.Right)
      return;
    if (this.ShowContextMenu != null)
    {
      ISimpleSelectedItems serviceInstance = (ISimpleSelectedItems) null;
      if (this.IsSimpleSelectedItemsSuppoted())
        serviceInstance = ServicesManager.GetService(typeof (ISimpleSelectedItems)) as ISimpleSelectedItems;
      try
      {
        if (this.IsSimpleSelectedItemsSuppoted())
        {
          if (serviceInstance != null)
            ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
          ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) this._selectedItems);
        }
        this.ShowContextMenu(sender, e);
      }
      finally
      {
        if (this.IsSimpleSelectedItemsSuppoted())
        {
          if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
            ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
          if (serviceInstance != null)
            ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) serviceInstance);
        }
      }
    }
    else
    {
      if (this.DisableIMContextMenu || e.Y < this.HeaderHeight)
        return;
      this.ShowStandardContextMenu(e.X, e.Y);
    }
  }

  private void ShowStandardContextMenu(int x, int y)
  {
    NavigatorTreeNode nodeAt = this.GetNodeAt(x, y);
    if (nodeAt == null)
      return;
    if (this.FocusedNode != nodeAt)
      this.FocusedNode = nodeAt;
    ServiceContainer contextMenuServices = this.GetContextMenuServices(nodeAt.Handler is IContextAware handler ? handler.Services : (System.IServiceProvider) this._services);
    MenuBarItem menuBarItem = (MenuBarItem) null;
    if (Control.ModifierKeys != Keys.Control)
      menuBarItem = Intermech.Navigator.ContextMenu.Services.GetMenuForObjectType(this.SelectedItems, (System.IServiceProvider) contextMenuServices);
    if (menuBarItem == null)
      menuBarItem = Intermech.Navigator.ContextMenu.Services.GetMenu(this.SelectedItems, (System.IServiceProvider) contextMenuServices);
    if (menuBarItem == null)
      return;
    NavigatorTreeNode[] nodes = this.BeforeExecuteMenuCommand();
    try
    {
      menuBarItem.Show((Control) this, new Point(x, y));
    }
    finally
    {
      this.AfterExecuteMenuCommand(nodes);
    }
  }

  /// <summary>Изменился верхний видимый узел в дереве</summary>
  private void TreeTopNodeChanged()
  {
    this._topRowIndex = this.TopRowIndex;
    this.CancelUpdateJobs((object) "UpdateJob", true);
    this.QueueUpdateJobs(true);
  }

  /// <summary>Вернуть контейнер сервисов для контекстного меню</summary>
  /// <param name="nodeContext">Контекст узла</param>
  /// <returns>Контейнер сервисов для контекстного меню</returns>
  private ServiceContainer GetContextMenuServices(System.IServiceProvider nodeContext)
  {
    AdvancedServiceContainer contextMenuServices = new AdvancedServiceContainer(nodeContext);
    contextMenuServices.AddService(typeof (ICommandsProvider), (object) this);
    contextMenuServices.AddService(typeof (NavigatorTreeView), (object) this);
    contextMenuServices.AddService(typeof (INavigatorTreeViewContextMenuHelper), (object) this._contextMenuHelper);
    contextMenuServices.AddService(typeof (ISelectedItemsHost), (object) this);
    return (ServiceContainer) contextMenuServices;
  }

  /// <summary>Уведомить корневой узел</summary>
  /// <param name="rootNode">Узел</param>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void NotifyRootNode(NavigatorTreeNode rootNode, object sender, NotificationEventArgs e)
  {
    NavigatorEtherealNodeView etherealNodeView = new NavigatorEtherealNodeView(this, rootNode);
    IUpdateAnalyser analyser = this.RootHandler.GetAnalyser(etherealNodeView.Capabilities, sender, e);
    bool alwaysShowFirstTab = UISettings.AlwaysShowFirstTab;
    UISettings.AlwaysShowFirstTab = false;
    try
    {
      if (analyser == null)
        return;
      UpdateManager.UpdateView((INodeView) etherealNodeView, analyser);
    }
    finally
    {
      if (!UISettings.AlwaysShowFirstTab)
        UISettings.AlwaysShowFirstTab = alwaysShowFirstTab;
    }
  }

  /// <summary>Уведомить узел</summary>
  /// <param name="node">Узел</param>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void NotifyNode(NavigatorTreeNode node, object sender, NotificationEventArgs e)
  {
    NavigatorRegularNodeView nodeView = new NavigatorRegularNodeView(this);
    try
    {
      nodeView.Bind(node);
      INode childHandler1 = this.GetChildHandler(node);
      if (childHandler1 == null)
        return;
      IUpdateAnalyser analyser = childHandler1.GetAnalyser(nodeView.Capabilities, sender, e);
      if (this.GetChildHandler(node) is INodeNotifications childHandler2)
      {
        switch (childHandler2.Process(e, (object) nodeView.Capabilities.Columns))
        {
          case ProcessResult.RefreshNodeFields:
            this.RefreshNodeFields(node);
            return;
          case ProcessResult.RefreshNode:
            if (node == this.RootNode && this.RootDescriptor != null)
            {
              this.Build(this.FocusedPath);
              return;
            }
            this.RefreshNode(node);
            return;
          case ProcessResult.RefreshNodeAndColumns:
            if (node != null && node.Parent != null)
            {
              this.MakeNodeUnpopulated(node.Parent);
              return;
            }
            if (node == null || node.Parent != null)
              return;
            this.BuildCore(this.RootDescriptor);
            return;
        }
      }
      if (e is DBObjectsEventArgs && (e.EventName == "ObjectsCheckedIn" || e.EventName == "ObjectsCheckedOut" || e.EventName == "ObjectsChangesCancelled"))
      {
        NodeID nodeId = node.NodeID as NodeID;
        DBObjectsEventArgs objectsEventArgs = (DBObjectsEventArgs) e;
        if (nodeId != null && objectsEventArgs.ObjectIDs != null && objectsEventArgs.ObjectIDs.Contains(nodeId.ObjectID))
        {
          node.NodeID = (INodeID) nodeId.InverseCheckedNode();
          this.RefreshNode(node);
          return;
        }
      }
      for (int index = 0; index < node.Children.Count; ++index)
      {
        NavigatorTreeNode child = node.Children[index];
        bool flag = false;
        try
        {
          if (child.Handler is INodeNotificaionSupport handler)
          {
            if (handler.DisableFocusAfterNodeAdded)
            {
              this._disableFocusNodeAfterAdded = true;
              try
              {
                this.NotifyNode(child, sender, e);
              }
              finally
              {
                this._disableFocusNodeAfterAdded = false;
              }
            }
            else
              this.NotifyNode(child, sender, e);
          }
          else
            this.NotifyNode(child, sender, e);
        }
        catch
        {
          flag = true;
        }
        if (flag)
        {
          NodeIDPath focusedPath = this.FocusedPath;
          if (node.Parent != null)
          {
            this.RefreshNode(node.Parent);
            if (focusedPath != null)
              this.TryBrowse(focusedPath);
          }
        }
      }
      if (analyser == null)
        return;
      UpdatePlan updatePlan = NavigatorTreeView.AnalyzeAndExecutePlan(node, nodeView, analyser);
      if (e.EventName == "RelationsCreated" && updatePlan.AppendedItems.Any<INodeID>((System.Func<INodeID, bool>) (o => o is ObjectGroupNodeID)))
      {
        INodeID objectGroupNodeID = updatePlan.AppendedItems.FirstOrDefault<INodeID>((System.Func<INodeID, bool>) (o => o is ObjectGroupNodeID));
        NavigatorTreeNode navigatorTreeNode = node.Children.FirstOrDefault<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (o => object.Equals((object) o.NodeID, (object) objectGroupNodeID)));
        if (navigatorTreeNode != null)
        {
          navigatorTreeNode.FocusThenExpand();
          this.FocusedNode = navigatorTreeNode.Children.FirstOrDefault<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (o => o.NodeID is NodeID && ((DBRelationsEventArgs) e).RelationIDs.Contains(((NodeID) o.NodeID).PrjLinkID)));
        }
      }
      if (!(e.EventName == "RelationsCreated") || updatePlan.AppendedItems.Count != 1 || !(updatePlan.AppendedItems[0] is NodeID) || !(node.NodeID is ObjectGroupNodeID))
        return;
      node?.FocusThenExpand();
      this.FocusedNode = node.Children.FirstOrDefault<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (o => o.NodeID is NodeID && ((NodeID) o.NodeID).PrjLinkID == ((NodeID) updatePlan.AppendedItems[0]).PrjLinkID));
    }
    catch
    {
      if (node.Parent != null && node.Parent.InTree)
      {
        NavigatorTreeNode parent = node.Parent;
        this.RefreshNode(parent);
        parent.FocusThenExpand();
      }
      else
      {
        this.RefreshNode(this.RootNode);
        this.RootNode.FocusThenExpand();
      }
    }
  }

  private static UpdatePlan AnalyzeAndExecutePlan(
    NavigatorTreeNode node,
    NavigatorRegularNodeView nodeView,
    IUpdateAnalyser analyser)
  {
    UpdatePlan plan = new UpdatePlan();
    analyser.Preprocess((IUpdatePlan) plan);
    for (int index = 0; index < ((INodeView) nodeView).Count; ++index)
    {
      plan.CurrentIndex = index;
      analyser.Process(((INodeView) nodeView)[index], (IUpdatePlan) plan);
      if (analyser is INavigatorTreeViewUpdateAnalyzer)
        ((INavigatorTreeViewUpdateAnalyzer) analyser).Process(node.Children[index], (IUpdatePlan) plan);
    }
    analyser.Postprocess((IUpdatePlan) plan);
    plan.Execute((INodeView) nodeView);
    return plan;
  }

  /// <summary>Развернуть вверх узлы</summary>
  /// <param name="startNode">Стартовый узел</param>
  private void ExpandParent(NavigatorTreeNode startNode)
  {
    if (startNode.Parent == null || startNode.Parent.Expanded)
      return;
    startNode.Parent.Expanded = true;
    this.ExpandParent(startNode.Parent);
  }

  /// <summary>
  /// Вызвать выполнение указанной команды контекстного меню для указанного события
  /// </summary>
  /// <param name="command">Команда контекстного меню</param>
  /// <returns>true, если команда обработана</returns>
  private bool ExecuteMenuCommand(string commandName)
  {
    if (string.IsNullOrEmpty(commandName) || this._currentCommandsTable == null || !this._currentCommandsTable.Contains(commandName) || this._currentContextMenuServiceProvider == null)
      return false;
    NavigatorTreeNode[] nodes = this.BeforeExecuteMenuCommand();
    try
    {
      Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, this._currentCommandsTable, this._currentContextMenuServiceProvider);
    }
    finally
    {
      this.AfterExecuteMenuCommand(nodes);
    }
    return true;
  }

  /// <summary>
  /// Получить статус указанной команды в текущем контексте дерева
  /// </summary>
  /// <param name="commandState">Статус команды</param>
  /// <returns>true, если статус корректно установлен</returns>
  private bool QueryStatusCurrentContext(ICommandState commandState)
  {
    if (string.IsNullOrEmpty(commandState.CommandName))
      return false;
    bool flag = this.QueryCommandStatus(commandState.CommandName);
    commandState.Enabled = flag;
    commandState.Checked = this.IsContextMenuItemChecked(commandState.CommandName);
    return flag;
  }

  private bool QueryCommandStatus(string commandName)
  {
    return this._currentCommandsTable != null && this._currentCommandsTable.Contains(commandName);
  }

  private bool IsContextMenuItemChecked(string commandName)
  {
    if (this._currentCommandsTable.Contains(commandName))
    {
      CommandLink commandLink = this._currentCommandsTable[commandName];
      if (commandLink != null && commandLink.CommandInfo != null && commandLink.CommandInfo.State != null)
        return commandLink.CommandInfo.State.State == ContextMenuCheckState.Checked;
    }
    return false;
  }

  /// <summary>Сгенерировать событие "Дерево очищено"</summary>
  private void RaiseClearTreeEvent()
  {
    if (this.ClearTree == null || this._lockClearTreeEvent != 0)
      return;
    this.ClearTree((object) this, EventArgs.Empty);
  }

  /// <summary>Записать в узел данные</summary>
  /// <param name="node">Узел дерева "Навигатора"</param>
  /// <param name="fieldValues">Устанавливаемые значения ячеек узла</param>
  /// <param name="rawFieldValues">Исходные значения ячеек узла</param>
  /// <param name="columns">Коллекция колонок дерева "Навигатора"</param>
  private void SetNodeFields(
    NavigatorTreeNode node,
    object[] fieldValues,
    object[] rawFieldValues,
    NodeColumnCollection columns)
  {
    for (int index = 0; index < columns.Count; ++index)
    {
      int columnIndex = this._treeColumns.IndexOf(columns[index]);
      if (columnIndex >= 0)
      {
        node.SetCellValue(columnIndex, fieldValues[index]);
        node.SetRawCellValue(columnIndex, rawFieldValues[index]);
      }
    }
  }

  /// <summary>
  /// Связать указанный узел с соответствующей строкой в дереве
  /// </summary>
  /// <param name="node">Обновляемый узел</param>
  private void InitNodeHandle(NavigatorTreeNode node)
  {
    if (node == null || node.Parent == null)
      return;
    node.Parent.Children.RebuildHandles();
  }

  /// <summary>Вернуть сфокусированный элемент</summary>
  /// <param name="node">Узел</param>
  /// <returns>Сфокусированный элемент</returns>
  private IFocusedItem GetFocusedItem(NavigatorTreeNode node)
  {
    return (IFocusedItem) new Intermech.Navigator.Controls.FocusedItem((NodeColumn) null, node.NodeID, this.GetNodeIDPath(node.Parent), this.GetNodeHandler(node), this.Services);
  }

  /// <summary>
  /// Проверить возможность добавления данного узла в дерево - на наличие "петель"
  /// </summary>
  /// <param name="node">Заготовка узла, ещё не связанная с деревом,
  /// но с заполненным свойством Parent, а также со всей информацией внутри (NodeID, т.п.)</param>
  /// <returns>NavigatorNodeCycle.Link - обнаружена "петля" с таким же самым родителем (PrjLinkID) - узел скрывать,
  /// NavigatorNodeCycle.Object - обнаружена "петля" с таким же объектом состава (ObjectID) и разными родителями - узел показывать без состава,
  /// NavigatorNodeCycle.None - "петля" не найдена</returns>
  private NavigatorNodeCycle CheckForCycle(NavigatorTreeNode node)
  {
    if (node == null || node.Parent == null || node.NodeID == null || node.Handler == null)
      return NavigatorNodeCycle.None;
    INode nodeHandler1 = this.GetNodeHandler(node);
    IDBRelationID data1 = nodeHandler1 != null ? nodeHandler1.GetData(node.NodeID, typeof (IDBRelationID)) as IDBRelationID : (IDBRelationID) null;
    IDBObjectID data2 = nodeHandler1 != null ? nodeHandler1.GetData(node.NodeID, typeof (IDBObjectID)) as IDBObjectID : (IDBObjectID) null;
    if (data1 == null || data1.Value == 0L || data1.Value == -1L || data2 == null || data2.Value == 0L)
      return NavigatorNodeCycle.None;
    List<long> longList1 = new List<long>();
    longList1.Add(Math.Abs(data1.Value));
    List<long> longList2 = new List<long>();
    longList2.Add(Math.Abs(data2.Value));
    for (node = node.Parent; node != null && node.NodeID != null && node.Handler != null; node = node.Parent)
    {
      INode nodeHandler2 = this.GetNodeHandler(node);
      IDBObjectID data3 = nodeHandler2 != null ? nodeHandler2.GetData(node.NodeID, typeof (IDBObjectID)) as IDBObjectID : (IDBObjectID) null;
      if (data3 == null || data3.Value == 0L)
        return NavigatorNodeCycle.None;
      IDBRelationID data4 = nodeHandler2 != null ? nodeHandler2.GetData(node.NodeID, typeof (IDBRelationID)) as IDBRelationID : (IDBRelationID) null;
      if (data4 == null || data4.Value == 0L || data4.Value == -1L)
        return longList2.IndexOf(Math.Abs(data3.Value)) >= 0 ? NavigatorNodeCycle.Object : NavigatorNodeCycle.None;
      if (longList1.IndexOf(Math.Abs(data4.Value)) >= 0)
        return NavigatorNodeCycle.Link;
      if (longList2.IndexOf(Math.Abs(data3.Value)) >= 0)
        return NavigatorNodeCycle.Object;
      longList2.Add(Math.Abs(data3.Value));
      longList1.Add(Math.Abs(data4.Value));
    }
    return NavigatorNodeCycle.None;
  }

  /// <summary>Создаём узел</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="fieldValues">Значения ячеек узла</param>
  /// <param name="rawFieldValues">Значения ячеек узла (исходные данные)</param>
  /// <param name="columns">Список колонок узла</param>
  /// <param name="moveNodeWithSorting">true - выполнить перемещение узла в списке согласно его сортировке</param>
  /// <returns>Вновь созданный узел или null</returns>
  internal NavigatorTreeNode CreateNode(
    NavigatorTreeNode parentNode,
    INodeID nodeID,
    object[] fieldValues,
    object[] rawFieldValues,
    NodeColumnCollection columns,
    bool moveNodeWithSorting)
  {
    NavigatorTreeNode node = this.AppendNode(parentNode, (object[]) null, (object[]) null, nodeID);
    node.HasChildren = true;
    node.ImageIndex = -1;
    node.SelectedImageIndex = -1;
    this.SetNodeFields(node, fieldValues, rawFieldValues, columns);
    this.InitNodeData(node, nodeID, columns);
    node.Cycle = this.CheckForCycle(node);
    if (node.Cycle == NavigatorNodeCycle.Link)
    {
      parentNode.Children.Remove(node);
      return (NavigatorTreeNode) null;
    }
    this.InitNodeHandle(node);
    if (moveNodeWithSorting)
      this.MoveNodeWithSortingAfterCreateNode(parentNode, node);
    if (node.Cycle == NavigatorNodeCycle.Object)
    {
      node.HasChildren = false;
      node.Children.Clear();
    }
    if (nodeID is ObjectGroupNodeID)
      node.ShowCheckState = false;
    if (this.AfterCreateNode != null)
      this.AfterCreateNode((object) this, new NodeEventArgs(node));
    return node;
  }

  private void MoveNodeWithSortingAfterCreateNode(
    NavigatorTreeNode parentNode,
    NavigatorTreeNode createdNode)
  {
    if (parentNode == null || !parentNode.InTree || parentNode.Handle.NumChildren == 0)
      return;
    INode nodeHandler1 = this.GetNodeHandler(createdNode);
    if (nodeHandler1 == null || !(nodeHandler1.GetData(createdNode.NodeID, typeof (IDBRelationID)) is IDBRelationID data1) || !this.ManualSort)
      return;
    Dictionary<int, List<Tuple<int, IDBRelationID>>> dictionary = new Dictionary<int, List<Tuple<int, IDBRelationID>>>();
    int childIndex = 0;
    for (int index = parentNode.Handle.NumChildren - 1; childIndex < index; ++childIndex)
    {
      if (parentNode.Handle.ChildRowByIndex(childIndex).Item is NavigatorTreeNode node)
      {
        INode nodeHandler2 = this.GetNodeHandler(node);
        if (nodeHandler2 != null && nodeHandler2.GetData(node.NodeID, typeof (IDBRelationID)) is IDBRelationID data2)
        {
          Tuple<int, IDBRelationID> tuple = new Tuple<int, IDBRelationID>(childIndex, data2);
          if (!dictionary.ContainsKey(data2.RelationType))
            dictionary.Add(data2.RelationType, new List<Tuple<int, IDBRelationID>>());
          dictionary[data2.RelationType].Add(tuple);
        }
      }
    }
    if (dictionary.ContainsKey(data1.RelationType))
    {
      List<Tuple<int, IDBRelationID>> source = dictionary[data1.RelationType];
      bool flag = false;
      foreach (Tuple<int, IDBRelationID> tuple in source)
      {
        if (tuple.Item2.Sorting > data1.Sorting)
        {
          this.SetNodeIndex(createdNode, tuple.Item1);
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      Tuple<int, IDBRelationID> tuple1 = source.LastOrDefault<Tuple<int, IDBRelationID>>();
      if (tuple1 == null)
        return;
      this.SetNodeIndex(createdNode, tuple1.Item1 + 1);
    }
    else
    {
      NodeID nodeId = parentNode.NodeID as NodeID;
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      if (nodeId == null || service == null)
        return;
      List<int> visibleRelations = service.Rule.GetObjectTypeVisibleRelations(nodeId.ObjectTypeID, true);
      int num = visibleRelations.IndexOf(data1.RelationType);
      if (num == -1)
        return;
      for (int index = num; index >= 0; --index)
      {
        if (dictionary.ContainsKey(visibleRelations[index]))
        {
          Tuple<int, IDBRelationID> tuple = dictionary[visibleRelations[index]].LastOrDefault<Tuple<int, IDBRelationID>>();
          if (tuple != null)
          {
            this.SetNodeIndex(createdNode, tuple.Item1 + 1);
            break;
          }
        }
      }
    }
  }

  /// <summary>Отыскать существующий узел</summary>
  /// <param name="parentNode">Родительский узел</param>
  /// <param name="nodeID">Описание узла</param>
  /// <returns>Найденный узел или null</returns>
  private NavigatorTreeNode FindExistingNode(NavigatorTreeNode parentNode, INodeID nodeID)
  {
    for (int index = 0; index < parentNode.Children.Count; ++index)
    {
      if (parentNode.Children[index].NodeID.Equals((object) nodeID))
        return parentNode.Children[index];
    }
    return (NavigatorTreeNode) null;
  }

  /// <summary>Отыскать зачитанный узел</summary>
  /// <param name="parentNode">Родительский узел</param>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="completeFind">Полный или частичный поиск</param>
  /// <returns>Найденный узел или null</returns>
  private NavigatorTreeNode FindFetchedNode(
    NavigatorTreeNode parentNode,
    INodeID nodeID,
    bool completeFind)
  {
    if (nodeID == null || parentNode == null)
      return (NavigatorTreeNode) null;
    int fetchCount = this.GetFetchCount();
    NodeColumnCollection treeColumns = this._treeColumns;
    while (!parentNode.Full)
    {
      INodeQuery query = this.GetChildHandler(parentNode).GetQuery(ContentType.Folders);
      if (query != null)
      {
        this.SetQueryColumns(query, treeColumns);
        query.Execute(parentNode.Bookmark, fetchCount);
        int count = parentNode.Children.Count;
        for (int index = 0; index < query.RecordCount; ++index)
          this.CreateNode(parentNode, query.GetRecordNodeID(index), query.GetRecordValues(index), query.GetRawRecordValues(index), treeColumns, false);
        parentNode.Bookmark = query.Bookmark;
        parentNode.Full = query.Bookmark == null;
        parentNode.HasChildren = parentNode.Children.Count != 0;
        for (int index = 0; index < query.RecordCount; ++index)
        {
          if (query.GetRecordNodeID(index).Equals((object) nodeID))
            return parentNode.Children[count + index];
        }
        if (!completeFind)
          return (NavigatorTreeNode) null;
        fetchCount *= 3;
      }
      else
      {
        parentNode.Bookmark = (object) null;
        parentNode.Full = true;
        parentNode.HasChildren = parentNode.Children.Count != 0;
        return (NavigatorTreeNode) null;
      }
    }
    return (NavigatorTreeNode) null;
  }

  /// <summary>Переместить узел на указанную позицию</summary>
  /// <param name="node">Перемещаемый узел</param>
  /// <param name="index">Новая позиция</param>
  private void SetNodeIndex(NavigatorTreeNode node, int index)
  {
    if (node == null || index < 0)
      return;
    NavigatorTreeNode node1 = node;
    if (node1 == null)
      return;
    NavigatorTreeNode parent = node1.Parent;
    if (parent == null || parent.Children.IndexOf(node1) == index)
      return;
    parent.Children.Remove(node1);
    parent.Children.Insert(index, node1);
    parent.Tree.UpdateRow(parent.Handle);
    this.InitNodeHandle(node1);
  }

  /// <summary>Получить индекс узла в дереве</summary>
  /// <param name="node">Узел</param>
  /// <returns>Индекс узла в дереве</returns>
  private int IndexOf(NavigatorTreeNode node)
  {
    NavigatorTreeNode navigatorTreeNode = node;
    if (navigatorTreeNode == null)
      return -1;
    NavigatorTreeNode parent = navigatorTreeNode.Parent;
    return parent == null ? -1 : parent.Children.IndexOf(navigatorTreeNode);
  }

  /// <summary>
  /// Получить упорядоченный список дочерних узлов дерева Навигатора при условии,
  /// что он является составом объекта IPS, который отображается в виде родительского
  /// узла parentNode. Сортировка учитывает текущее правило отображения и сортировки
  /// составов, а также сортируемую в дереве колонку
  /// </summary>
  /// <param name="pNode">Родительский узел дерева Навигатора</param>
  /// <returns>Упорядоченный список дочерних узлов или null</returns>
  private List<NavigatorTreeNode> SortChildNodes(NavigatorTreeNode pNode)
  {
    if (pNode == null || pNode.NodeID == null || !pNode.HasChildren || !pNode.InTree || pNode.Children.Count <= 1)
      return (List<NavigatorTreeNode>) null;
    NodeColumnCollection sortedColumns = NodeColumnCollection.GetSortedColumns(this._treeColumns);
    if (sortedColumns == null || sortedColumns.Count == 0)
      return (List<NavigatorTreeNode>) null;
    NodeColumn nodeColumn = sortedColumns[0];
    int sortColumnIdx = this.Columns.IndexOf(this.SortColumn);
    INode nodeHandler1 = this.GetNodeHandler(pNode);
    IDBTypedObjectID data1 = nodeHandler1 != null ? nodeHandler1.GetData(pNode.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    if (data1 == null)
      return (List<NavigatorTreeNode>) null;
    NavigatorTreeNode child = pNode.Children[0];
    if (child == null)
      return (List<NavigatorTreeNode>) null;
    INode nodeHandler2 = this.GetNodeHandler(child);
    IDBRelationID data2 = nodeHandler2 == null || child.NodeID == null ? (IDBRelationID) null : nodeHandler2.GetData(child.NodeID, typeof (IDBRelationID)) as IDBRelationID;
    if (data2 == null || data2.Value == 0L || data2.RelationType == -1)
      return (List<NavigatorTreeNode>) null;
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    CompositionsAutosortRule rule = data1 == null || service == null ? (CompositionsAutosortRule) null : service.Rule;
    List<NavigatorTreeNode> navigatorTreeNodeList = new List<NavigatorTreeNode>((IEnumerable<NavigatorTreeNode>) pNode.Children);
    navigatorTreeNodeList.Sort((IComparer<NavigatorTreeNode>) new NavigatorTreeNodeComparer(data1, rule, nodeHandler2, sortColumnIdx, nodeColumn.SortOrder));
    return navigatorTreeNodeList;
  }

  /// <summary>
  /// Рассчитать "правильную" позицию узла в дереве на основе информации
  /// из сортируемой колонки (с учётом того, что .NET и СУБД могут сортировать
  /// совершенно по-разному).
  /// Внимание! Дочерние узлы в составе родительского узла должны быть размещены
  /// согласно текущему правилу сортировки и отображения составов, а также отсортированы
  /// по сортируемой колонке. В остальных случаях расчёт индекса может вернуть
  /// непредсказуемое значение
  /// </summary>
  /// <param name="node">Узел, для которого требуется определить корректную позицию</param>
  /// <returns>Корректная позиция или -1, если её рассчитать не удалось</returns>
  private int CalcNodeIndex(NavigatorTreeNode node)
  {
    NavigatorTreeNode navigatorTreeNode = node;
    if (navigatorTreeNode == null || navigatorTreeNode.NodeID == null)
      return -1;
    NavigatorTreeNode parent = navigatorTreeNode.Parent;
    if (parent == null || parent.NodeID == null || !parent.HasChildren || !parent.InTree || parent.Children.Count <= 1)
      return -1;
    NodeColumnCollection sortedColumns = NodeColumnCollection.GetSortedColumns(this._treeColumns);
    if (sortedColumns == null || sortedColumns.Count == 0)
      return -1;
    List<NavigatorTreeNode> navigatorTreeNodeList1 = this.SortChildNodes(parent);
    if (navigatorTreeNodeList1 != null && navigatorTreeNodeList1.Count > 1)
    {
      int num = navigatorTreeNodeList1.IndexOf(navigatorTreeNode);
      if (num >= 0)
        return num;
    }
    int num1 = parent.Children.IndexOf(navigatorTreeNode);
    NodeColumn sortColumn = sortedColumns[0];
    int index1 = this.Columns.IndexOf(this.SortColumn);
    object rawValue1 = index1 >= 0 ? navigatorTreeNode.RawValues[index1] : (object) null;
    INode nodeHandler1 = this.GetNodeHandler(parent);
    IDBTypedObjectID parObjID = nodeHandler1 == null || parent.NodeID == null ? (IDBTypedObjectID) null : nodeHandler1.GetData(parent.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (parObjID == null)
      return -1;
    INode nodeHandler = this.GetNodeHandler(node);
    IDBTypedObjectID nodeObjID = nodeHandler == null || node == null || navigatorTreeNode.NodeID == null ? (IDBTypedObjectID) null : nodeHandler.GetData(navigatorTreeNode.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IDBRelationID nodeRelID = nodeHandler == null || node == null || navigatorTreeNode.NodeID == null ? (IDBRelationID) null : nodeHandler.GetData(navigatorTreeNode.NodeID, typeof (IDBRelationID)) as IDBRelationID;
    int num2 = sortColumn.SortOrder == NodeColumnSortOrder.Ascending ? parent.Children.Count - 1 : 0;
    IDBRelationID data1 = nodeHandler == null || parent.Children[0] == null || parent.Children[0].NodeID == null ? (IDBRelationID) null : nodeHandler.GetData(parent.Children[0].NodeID, typeof (IDBRelationID)) as IDBRelationID;
    if (data1 == null || data1.Value == 0L || data1.RelationType == -1)
      return -1;
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    CompositionsAutosortRule rule = parObjID == null || service == null ? (CompositionsAutosortRule) null : service.Rule;
    List<NavigatorTreeNode> navigatorTreeNodeList2 = parent.Children.FindAll((Predicate<NavigatorTreeNode>) (item =>
    {
      IDBTypedObjectID data2 = nodeHandler == null || item == null || item.NodeID == null ? (IDBTypedObjectID) null : nodeHandler.GetData(item.NodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      IDBRelationID data3 = nodeHandler == null || item == null || item.NodeID == null ? (IDBRelationID) null : nodeHandler.GetData(item.NodeID, typeof (IDBRelationID)) as IDBRelationID;
      return (parObjID == null || data2 == null || data3 == null || data3.Value == 0L || rule == null ? 0 : rule.CompareTo(parObjID.ObjectType, data3.RelationType, nodeRelID.RelationType, sortColumn.SortOrder == NodeColumnSortOrder.Ascending ? data2.ObjectType : nodeObjID.ObjectType, sortColumn.SortOrder == NodeColumnSortOrder.Ascending ? nodeObjID.ObjectType : data2.ObjectType, OptimizationSettings.FullCompositionsSorting)) == 0;
    }));
    if (navigatorTreeNodeList2.Count == 0)
      navigatorTreeNodeList2 = (List<NavigatorTreeNode>) parent.Children;
    List<int> intList1 = navigatorTreeNodeList2.ConvertAll<int>((Converter<NavigatorTreeNode, int>) (item => item.Parent.Children.IndexOf(item)));
    intList1.Sort();
    int num3 = intList1.Count > 0 ? intList1[intList1.Count - 1] : (sortColumn.SortOrder == NodeColumnSortOrder.Ascending ? parent.Children.Count - 1 : 0);
    List<int> intList2 = new List<int>();
    for (int index2 = 0; index2 < intList1.Count; ++index2)
    {
      NavigatorTreeNode child = parent.Children[intList1[index2]];
      int num4 = intList1[index2];
      if (ObjectsCompareHelper.CompareTo(index1 >= 0 ? child.RawValues[index1] : (object) null, rawValue1) == 0)
        intList2.Add(num4);
    }
    int num5 = -1;
    int num6 = -1;
    bool flag = false;
    for (int index3 = 0; index3 < intList1.Count; ++index3)
    {
      NavigatorTreeNode child = parent.Children[intList1[index3]];
      int num7 = intList1[index3];
      if (num7 > num3)
        num3 = num7;
      if (ObjectsCompareHelper.CompareTo(index1 >= 0 ? child.RawValues[index1] : (object) null, rawValue1) == 0 && !flag)
      {
        if (num5 == -1)
          num5 = num7;
        num6 = num7;
      }
      else
      {
        if (intList2.Count > 1 && num5 == num1 && num6 == num1)
        {
          num5 = -1;
          num6 = -1;
        }
        if (num6 >= 0)
          flag = true;
      }
    }
    if (num1 >= num5 && num1 <= num6 && intList2.Count > 1)
      return num1;
    if (num6 >= 0 && intList2.Count > 1)
      return num1 <= num6 ? num6 : num6 + 1;
    int num8 = -2;
    int num9 = -1;
    if (intList1.Count == 1)
      return intList1[0];
    if (intList1.IndexOf(num1) >= 0 && intList1.Count > 1)
    {
      for (int index4 = 1; index4 < intList1.Count; ++index4)
      {
        if (Math.Abs(intList1[index4] - intList1[index4 - 1]) > 1)
        {
          int num10 = index4;
          int count = intList1.Count - num10;
          if (num10 >= count)
          {
            intList1.RemoveRange(index4, count);
            break;
          }
          intList1.RemoveRange(0, num10 + 1);
          break;
        }
      }
    }
    int num11 = intList1.Count > 0 ? intList1[intList1.Count - 1] : (sortColumn.SortOrder == NodeColumnSortOrder.Ascending ? parent.Children.Count - 1 : 0);
    for (int index5 = 0; index5 < intList1.Count; ++index5)
    {
      NavigatorTreeNode child = parent.Children[intList1[index5]];
      int num12 = intList1[index5];
      if (num12 > num11)
        num11 = num12;
      if (num12 == num1 && index5 == 0)
      {
        num8 = 0;
        num9 = num12;
      }
      else
      {
        object rawValue2 = index1 >= 0 ? child.RawValues[index1] : (object) null;
        int num13 = ObjectsCompareHelper.CompareTo(rawValue1, rawValue2);
        if (num8 == 0 && (sortColumn.SortOrder == NodeColumnSortOrder.Ascending && num13 < 0 || sortColumn.SortOrder == NodeColumnSortOrder.Descending && num13 > 0) || Math.Abs(num13 - num8) == 2)
          return intList1.IndexOf(num1) < 0 && num9 + 1 < parent.Children.Count ? num9 + 1 : num9;
        if (sortColumn.SortOrder == NodeColumnSortOrder.Ascending && num13 < 0 || sortColumn.SortOrder == NodeColumnSortOrder.Descending && num13 > 0)
          return num12;
        num8 = num13;
        num9 = num12;
      }
    }
    return num11 >= parent.Children.Count ? parent.Children.Count - 1 : num11;
  }

  /// <summary>Получить колонку, содержащую указанную точку</summary>
  /// <param name="x">Координата X</param>
  /// <param name="y">Координата Y</param>
  /// <returns>Колонка или null</returns>
  private NavigatorTreeColumn GetColumnAt(int x, int y)
  {
    int num = 0;
    for (int index = 0; index < this.Columns.Count; ++index)
    {
      Column column = this.Columns[index];
      if (x >= num && x <= column.Width + num)
        return column as NavigatorTreeColumn;
      num += column.Width;
    }
    return (NavigatorTreeColumn) null;
  }

  /// <summary>Получить список видимых на экране узлов</summary>
  /// <returns>Cписок видимых на экране узлов</returns>
  private NavigatorTreeNode[] GetVisibleNodes()
  {
    Hashtable rows = new Hashtable();
    List<NavigatorTreeNode> navigatorTreeNodeList = new List<NavigatorTreeNode>();
    this.GetRows(this.TopRowIndex, this.BottomRowIndex, rows);
    if (rows.Count > 0)
    {
      foreach (DictionaryEntry dictionaryEntry in rows)
      {
        NavigatorTreeNode navigatorTreeNode = dictionaryEntry.Value is Row row ? row.Item as NavigatorTreeNode : (NavigatorTreeNode) null;
        if (navigatorTreeNode != null)
          navigatorTreeNodeList.Add(navigatorTreeNode);
      }
    }
    navigatorTreeNodeList.Sort((IComparer<NavigatorTreeNode>) this._nodesComparer);
    return navigatorTreeNodeList.ToArray();
  }

  /// <summary>
  /// Обновить только поля самого узла, дочерние узлы не трогать
  /// </summary>
  /// <param name="node">Обновляемый узел</param>
  private void RefreshNodeFields(NavigatorTreeNode node)
  {
    NavigatorTreeNode navigatorTreeNode = node;
    if (navigatorTreeNode == null || !navigatorTreeNode.InTree || navigatorTreeNode.Handle.ChildIndex < 0 || navigatorTreeNode.Parent == null || !navigatorTreeNode.Parent.InTree)
      return;
    this.ProcessUpdateChildren(navigatorTreeNode.Parent, (NodeColumnCollection) null, (IList) new List<int>(1)
    {
      navigatorTreeNode.Handle.ChildIndex
    });
  }

  private void FireAfterPopulateNode(NavigatorTreeNode node)
  {
    if (this.AfterPopulateNode != null)
      this.AfterPopulateNode((object) this, new NodeEventArgs(node));
    this.FireAfterNodeChildsLoaded(node);
  }

  /// <summary>Инициализирует подсистему фоновых обновлений дерева.</summary>
  private void InitializeJobSystem()
  {
    if (this.DesignMode)
      return;
    this._jobManager = (IJobManager) new ThreadPoolJobManager();
    this._jobManager.Complete += new JobCompleteEventHandler(this.JobComplete);
    this._jobQueue = new Queue();
  }

  /// <summary>
  /// Завершает работу подсистемы фоновых обновлений и освобождает
  /// используемые ею ресурсы.
  /// </summary>
  private void DisposeJobSystem()
  {
    if (this.DesignMode)
      return;
    this._jobManager.Cancel();
    this._jobManager.Complete -= new JobCompleteEventHandler(this.JobComplete);
    this.CancelUpdateJobsCore(false);
  }

  /// <summary>
  /// Анализирует видимые в дереве узлы на предмет наличия устаревших сведений и
  /// выполняет постановку в очередь фоновых заданий по обновлению дерева.
  /// </summary>
  /// <param name="delayed">Отложенный старт задач</param>
  private void QueueUpdateJobs(bool delayed)
  {
    if (this.DesignMode)
      return;
    if (delayed)
    {
      this._queueUpdatesTimer.Stop();
      this._queueUpdatesTimer.Start();
    }
    else
      this.QueueUpdateJobsCore();
  }

  /// <summary>
  /// Анализирует результаты выполнения фонового задания и заносит его в очередь выполненных заданий.
  /// Когда длина очереди достигает некоторой величины, происходит обновление дерева навигатора.
  /// </summary>
  /// <param name="jobInfo">Сведения о результатах выполнения фонового задания.</param>
  private void JobComplete(JobInfo jobInfo)
  {
    if (this.DesignMode || !this.IsHandleCreated || jobInfo.State == JobState.Failed)
      return;
    lock (this._jobQueue.SyncRoot)
      this._jobQueue.Enqueue((object) jobInfo.Job);
    if (this.IsDisposed)
      return;
    if (!this.IsHandleCreated)
      return;
    try
    {
      this.Invoke((Delegate) new MethodInvoker(this.ReduceJobQueue));
    }
    catch
    {
    }
  }

  /// <summary>
  /// Активирует алгоритм пострановки в очередь фоновых заданий по обновлению дерева.
  /// Выполняется при срабатывании таймера отложенного обновления (tmQueueUpdates).
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void QueueUpdateJobs(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this._queueUpdatesTimer.Stop();
    if (this.IsDisposed)
      return;
    this.QueueUpdateJobsCore();
  }

  /// <summary>
  /// Анализирует видимые в дереве узлы на предмет наличия устаревших сведений и
  /// выполняет постановку в очередь фоновых заданий по обновлению дерева.
  /// </summary>
  private void QueueUpdateJobsCore()
  {
    if (this.DesignMode)
      return;
    NavigatorTreeViewVisibleNodes nodes = new NavigatorTreeViewVisibleNodes((IList) this.GetVisibleNodes());
    if (nodes.Count <= 0 || this.DisableJobs)
      return;
    this.QueueIconsJobs(nodes);
    this.QueuePlusJobs(nodes);
    this.QueueFieldsJobs(nodes);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool DisableJobs { get; set; }

  /// <summary>Остановить фоновые задачи</summary>
  /// <param name="applyCompleted">Применить выполненные задачи</param>
  private void CancelUpdateJobsCore(bool applyCompleted)
  {
    if (this.DesignMode)
      return;
    this._queueUpdatesTimer.Stop();
    this._applyUpdatesTimer.Stop();
    if (applyCompleted)
    {
      this.UpdateTreeViewCore();
    }
    else
    {
      lock (this._jobQueue.SyncRoot)
        this._jobQueue.Clear();
    }
    this.FireJobsUpdateCanceled();
  }

  /// <summary>Событие очистки очереди запланированных работ</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public event EventHandler JobsUpdateCanceled;

  /// <summary>Вызов события JobsUpdateCanceled</summary>
  protected virtual void FireJobsUpdateCanceled()
  {
    if (this.JobsUpdateCanceled == null)
      return;
    this.JobsUpdateCanceled((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// Метод вызывается после активации таймера, после чего таймер останавливается до следующей надобности
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void TopRowMonitor(object sender, EventArgs e)
  {
    this._topRowMonitorTimer.Enabled = false;
    if (this._topRowIndex == this.TopRowIndex)
      return;
    this.TreeTopNodeChanged();
  }

  /// <summary>
  /// Активирует алгоритм обновления дерева по результатам выполнения фоновых заданий.
  /// Выполняется при срабатывании таймера применения обновлений (tmApplyUpdates).
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void UpdateTreeView(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this._applyUpdatesTimer.Stop();
    if (this.IsDisposed)
      return;
    this.UpdateTreeViewCore();
  }

  /// <summary>
  /// Обновляет дерево навигатора в соответствии с информацией, собранной фоновыми
  /// потоками.
  /// </summary>
  private void UpdateTreeViewCore()
  {
    if (this.DesignMode)
      return;
    lock (this._jobQueue.SyncRoot)
    {
      while (this._jobQueue.Count > 0)
        ((ITreeViewJob) this._jobQueue.Dequeue()).UpdateTreeView();
    }
  }

  /// <summary>Обновление иконок узловs</summary>
  /// <param name="nodes">Узлы</param>
  private void QueueIconsJobs(NavigatorTreeViewVisibleNodes nodes)
  {
    for (int index1 = 0; index1 < nodes.Groups.Length; ++index1)
    {
      List<NavigatorTreeNode> nodes1 = new List<NavigatorTreeNode>();
      for (int index2 = 0; index2 < nodes.Groups[index1].Count; ++index2)
      {
        NavigatorTreeNode navigatorTreeNode = nodes.Groups[index1][index2];
        if (navigatorTreeNode.Parent != null && navigatorTreeNode.NodeID != null && (navigatorTreeNode.Flags & TreeNodeFlags.ImageOutdated) != TreeNodeFlags.None)
          nodes1.Add(nodes.Groups[index1][index2]);
      }
      if (nodes1.Count > 0)
      {
        NavigatorCollectIconsJob navigatorCollectIconsJob = new NavigatorCollectIconsJob(this.GetChildHandler(nodes.Groups[index1].Parent), nodes1);
        navigatorCollectIconsJob.Complete += new NavigatorCollectIconsJob.CompleteEventHandler(this.ApplyIconsJob);
        this._jobManager.Queue((IJob) navigatorCollectIconsJob, (object) "UpdateJob");
      }
    }
  }

  /// <summary>Применить обновления иконок</summary>
  /// <param name="nodes">Узлы</param>
  /// <param name="imageIndexes">Индексы изображений</param>
  private void ApplyIconsJob(List<NavigatorTreeNode> nodes, List<int> imageIndexes)
  {
    for (int index = 0; index < nodes.Count; ++index)
    {
      NavigatorTreeNode node1 = nodes[index];
      if (node1.InTree)
      {
        NavigatorTreeNode node2 = node1;
        if ((node2.Flags & TreeNodeFlags.ImageOutdated) != TreeNodeFlags.None)
        {
          node2.Flags ^= TreeNodeFlags.ImageOutdated;
          this.UpdateTreeNode(node2);
        }
      }
    }
  }

  /// <summary>
  /// Запустить задание по определению наличия дочерних узлов
  /// </summary>
  /// <param name="nodes">Список анализируемых узлов</param>
  private void QueuePlusJobs(NavigatorTreeViewVisibleNodes nodes)
  {
    NodeColumnCollection treeColumns = this._treeColumns;
    int fetchCount = this.GetFetchCount();
    if (!this.BackgroundTreeTasks)
    {
      if (nodes.Groups.Length == 0)
        return;
      for (int index1 = 0; index1 < nodes.Groups.Length; ++index1)
      {
        NavigatorTreeViewVisibleNodesGroup group = nodes.Groups[index1];
        this.GetChildHandler(group.Parent);
        for (int index2 = 0; index2 < group.Count; ++index2)
        {
          NavigatorTreeNode navigatorTreeNode = group[index2];
        }
      }
    }
    else
    {
      for (int index = 0; index < nodes.Count; ++index)
      {
        if (nodes[index].HasChildren && nodes[index].Children.Count == 0)
        {
          NavigatorFetchChildrenJob fetchChildrenJob = new NavigatorFetchChildrenJob(nodes[index], treeColumns, fetchCount);
          fetchChildrenJob.Complete += new NavigatorFetchChildrenJob.CompleteEventHandler(this.ApplyPlusJob);
          this._jobManager.Queue((IJob) fetchChildrenJob, (object) "UpdateJob");
        }
      }
    }
  }

  /// <summary>
  /// Применить результаты выполнения задачи по определению наличия дочерних узлов
  /// </summary>
  /// <param name="node">Узел</param>
  /// <param name="resultPacket">Пакет с результатами запроса к источнику данных</param>
  private void ApplyPlusJob(NavigatorTreeNode node, NavigatorJobResultPacket resultPacket)
  {
    MethodInvoker method = (MethodInvoker) (() =>
    {
      NavigatorTreeNode node1 = node;
      if (!node.InTree || !node.HasChildren || node.Children.Count != 0)
        return;
      if (resultPacket != null)
      {
        for (int index = 0; index < resultPacket.NodeIDs.Count; ++index)
          this.CreateNode(node, resultPacket.NodeIDs[index], (object[]) resultPacket.ItemValues[index], (object[]) resultPacket.RawItemValues[index], resultPacket.Columns, false);
        node.Bookmark = resultPacket.Bookmark;
        node.HasChildren = node.Children.Count != 0;
        node.Full = resultPacket.Bookmark == null;
      }
      else
      {
        node.Bookmark = (object) null;
        node.HasChildren = node.Children.Count != 0;
        node.Full = true;
      }
      this.FirePlusJobCompleted(node1);
      this.UpdateTreeNode(node);
    });
    if (this.InvokeRequired)
    {
      if (this.IsHandleCreated)
      {
        try
        {
          this.Invoke((Delegate) method);
          return;
        }
        catch
        {
          return;
        }
      }
    }
    method();
  }

  private void QueueFieldsJobs(NavigatorTreeViewVisibleNodes nodes)
  {
    for (int index1 = 0; index1 < nodes.Groups.Length; ++index1)
    {
      StatesRecordCollection records = new StatesRecordCollection();
      for (int index2 = 0; index2 < nodes.Groups[index1].Count; ++index2)
      {
        NavigatorTreeNode navigatorTreeNode = nodes.Groups[index1][index2];
        if (navigatorTreeNode.Parent != null && navigatorTreeNode.ValidColumns != null && navigatorTreeNode.ValidColumns.IsPartial && !records.Contains(navigatorTreeNode.ValidColumns))
          records.Add(navigatorTreeNode.ValidColumns);
      }
      if (records.Count > 0)
      {
        NodeColumnCollection invalidColumns = this.TreeGetInvalidColumns(records);
        NavigatorFetchFieldsJob navigatorFetchFieldsJob = new NavigatorFetchFieldsJob(this.GetChildHandler(nodes.Groups[index1].Parent), invalidColumns, nodes.Groups[index1]);
        navigatorFetchFieldsJob.Complete += new NavigatorFetchFieldsJob.CompleteEventHandler(this.ApplyFieldsJob);
        this._jobManager.Queue((IJob) navigatorFetchFieldsJob, (object) "UpdateJob");
      }
    }
  }

  private void ApplyFieldsJob(
    NavigatorTreeViewVisibleNodesGroup nodes,
    NavigatorJobResultPacket resultPacket)
  {
    MethodInvoker method = (MethodInvoker) (() =>
    {
      if (resultPacket == null)
        return;
      for (int index1 = 0; index1 < resultPacket.NodeIDs.Count; ++index1)
      {
        for (int index2 = 0; index2 < nodes.Count; ++index2)
        {
          NavigatorTreeNode node = nodes[index2];
          NavigatorTreeNode navigatorTreeNode = node;
          if (navigatorTreeNode.NodeID.Equals((object) resultPacket.NodeIDs[index1]))
          {
            if (node.InTree && navigatorTreeNode.ValidColumns.IsPartial)
            {
              this.UpdateNodeFields(node, (object[]) resultPacket.ItemValues[index1], (object[]) resultPacket.RawItemValues[index1], resultPacket.Columns);
              break;
            }
            break;
          }
        }
      }
    });
    if (this.InvokeRequired)
    {
      if (this.IsHandleCreated)
      {
        try
        {
          this.Invoke((Delegate) method);
          return;
        }
        catch
        {
          return;
        }
      }
    }
    method();
  }

  /// <summary>
  /// Метод позволяет отыскать для указанного узла (с объектом) в дереве
  /// конфигурируемый родительский узел верхнего уровня, в составе которого содержится данный узел.
  /// Если узел не содержит объект, будет возвращено значение null
  /// </summary>
  /// <param name="node">Узел, содержащий объект</param>
  /// <returns>Конфигурируемый родительский узел верхнего уровня, в составе которого содержится указанный узел</returns>
  private NavigatorTreeNode GetTopCompositionNode(NavigatorTreeNode node)
  {
    if (node == null)
      return (NavigatorTreeNode) null;
    NavigatorTreeNode topCompositionNode = (NavigatorTreeNode) null;
    NavigatorTreeNode node1 = node;
    while (true)
    {
      INode nodeHandler = this.GetNodeHandler(node1);
      if (nodeHandler != null)
      {
        IDBRelationID data = nodeHandler.GetData(node1.NodeID, typeof (IDBRelationID)) as IDBRelationID;
        if (nodeHandler.GetData(node1.NodeID, typeof (IDBObjectID)) is IDBObjectID)
        {
          topCompositionNode = node1;
          if (data != null && data.Value != 0L && MetaDataHelper.IsPdmPartiallyConfigurableRelationType(data.RelationType) && node1.Parent != null && node1.Parent.InTree)
            node1 = node1.Parent;
          else
            goto label_8;
        }
        else
          break;
      }
      else
        goto label_9;
    }
    return topCompositionNode;
label_8:
    return topCompositionNode;
label_9:
    return topCompositionNode;
  }

  private NavigatorTreeNode[] BeforeExecuteMenuCommand()
  {
    this._contextMenuHelper.MenuNode = this.FocusedNode;
    this._contextMenuHelper.CanRestoreFocusedNode = true;
    return this.GetSuitableNodesForFocusAfterTreeChanged(this.FocusedNode);
  }

  private void AfterExecuteMenuCommand(NavigatorTreeNode[] nodes)
  {
    if (!this._contextMenuHelper.CanRestoreFocusedNode)
      return;
    foreach (NavigatorTreeNode node in nodes)
    {
      if (node.InTree && node.Parent != null && node.Parent.Children.Contains(node))
      {
        if (this.FocusedNode == node)
          break;
        this.FocusedNode = node;
        break;
      }
    }
  }

  private NavigatorTreeNode[] GetSuitableNodesForFocusAfterTreeChanged(NavigatorTreeNode node)
  {
    List<NavigatorTreeNode> navigatorTreeNodeList = new List<NavigatorTreeNode>();
    navigatorTreeNodeList.Add(node);
    while (node != null)
    {
      node = node.GetPreviousSiblingOrParent();
      if (node != null)
        navigatorTreeNodeList.Add(node);
      else
        break;
    }
    return navigatorTreeNodeList.ToArray();
  }

  private void RemoveNodeColumn(NodeColumn nodeColumn)
  {
    NavigatorTreeColumn navigatorTreeColumn = this.Columns.Cast<NavigatorTreeColumn>().FirstOrDefault<NavigatorTreeColumn>((System.Func<NavigatorTreeColumn, bool>) (o => o.NavigatorColumn == nodeColumn));
    if (navigatorTreeColumn == null)
      return;
    this.Columns.Remove((Column) navigatorTreeColumn);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      if (!this.DesignMode)
      {
        this.DisposeJobSystem();
        this.DeactivateTreeServices();
        this.DeactivateTreeResources();
      }
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this._queueUpdatesTimer = new System.Windows.Forms.Timer(this.components);
    this._applyUpdatesTimer = new System.Windows.Forms.Timer(this.components);
    this._headerContextMenuStrip = new ContextMenuStrip(this.components);
    this._changeColumnsToolStripMenuItem = new ToolStripMenuItem();
    this._navigatorTreeViewEditingComponent = new NavigatorTreeViewEditingComponent(this.components);
    this._headerContextMenuStrip.SuspendLayout();
    this.BeginInit();
    this.SuspendLayout();
    this._queueUpdatesTimer.Tick += new EventHandler(this.QueueUpdateJobs);
    this._applyUpdatesTimer.Interval = 50;
    this._applyUpdatesTimer.Tick += new EventHandler(this.UpdateTreeView);
    this._headerContextMenuStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this._changeColumnsToolStripMenuItem
    });
    this._headerContextMenuStrip.Name = "_headerContextMenuStrip";
    this._headerContextMenuStrip.Size = new Size(223, 26);
    this._changeColumnsToolStripMenuItem.Name = "_changeColumnsToolStripMenuItem";
    this._changeColumnsToolStripMenuItem.Size = new Size(222, 22);
    this._changeColumnsToolStripMenuItem.Text = "Настройка отображения ...";
    this.BackgroundImageMode = ImageDrawMode.Tile;
    this.BorderStyle = BorderStyle.Fixed3D;
    this.LineStyle = LineStyle.Solid;
    this.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.Enter += new EventHandler(this.NavigatorTreeView_Enter);
    this.Leave += new EventHandler(this.NavigatorTreeView_Leave);
    this._headerContextMenuStrip.ResumeLayout(false);
    this.EndInit();
    this.ResumeLayout(false);
  }

  private sealed class NavigatorNodeTag
  {
    /// <summary>Флаг того, что нод был раскрыт автоматически</summary>
    public bool AutoExpanded;

    /// <summary>Создать экземляр класса</summary>
    /// <param name="autoExpanded">Флаг того, что нод был раскрыт автоматически</param>
    public NavigatorNodeTag(bool autoExpanded) => this.AutoExpanded = autoExpanded;
  }
}
