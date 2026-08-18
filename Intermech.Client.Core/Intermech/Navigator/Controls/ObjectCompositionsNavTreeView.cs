
// Type: Intermech.Navigator.Controls.ObjectCompositionsNavTreeView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Intermech.Bars;
using Intermech.Client.Core.Forms;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Дерево навигатора с панелью настроек сортировки и фильтрации по типу объекта, со скрываемой панелью свойств выбранного
/// объекта внизу дерева</summary>
public class ObjectCompositionsNavTreeView : 
  NavTreeViewWithProps,
  ITreeListColumns,
  ICommandTarget,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  /// <summary>Коллекция дескрипторов корневых узлов изделий, чья структура отображается</summary>
  [CanBeNull]
  [ItemNotNull]
  protected IReadOnlyList<Intermech.Navigator.DBObjects.Descriptor> _RootDescriptors;
  /// <summary>Колонка поля "Статус"</summary>
  [CanBeNull]
  private NodeColumn _statusColumn;
  private bool _statusColumnPropLoaded;
  /// <summary>Фигурирующие в дереве типы объектов</summary>
  [NotNull]
  private readonly List<int> _visibleObjectTypes = new List<int>();
  /// <summary>Идентификаторы типов объектов, которые должны быть доступны для выбора</summary>
  [CanBeNull]
  protected IReadOnlyCollection<int> _ObjectTypeIDs;
  /// <summary>Ссылка на форму "Идёт загрузка состава"</summary>
  [CanBeNull]
  protected internal ObjectStructureIsLoadingForm _ObjectStructureIsLoadingForm;
  /// <summary>Наименование операции загрузки состава, блокирующей сохранение</summary>
  private const string LoadCompositionContextName = "LoadComposition";
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Тип контрола дерева, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного контрола, в этом случае дерево будет создано указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public new static System.Type OverrideTreeViewClass
  {
    [DebuggerStepThrough] get => NavigatorTreeViewWithObjectTypeFiltration.OverrideTreeViewClass;
    [DebuggerStepThrough] set
    {
      NavigatorTreeViewWithObjectTypeFiltration.OverrideTreeViewClass = !(value != (System.Type) null) || !(value != typeof (ObjectsCompositionNavigatorTree)) || value.IsSubclassOf(typeof (ObjectsCompositionNavigatorTree)) ? value : throw new Exception($"Tree class must be {typeof (ObjectsCompositionNavigatorTree).FullName} or it`s child class");
    }
  }

  /// <summary>Default constructor</summary>
  public ObjectCompositionsNavTreeView()
  {
    this.InitializeComponent();
    this.TreeView.BuildTree += new EventHandler(this._treeView_BuildTree);
    this.TreeView.JobsUpdateCanceled += new EventHandler(this.TreeView_JobsUpdateCanceled);
    this.ViewsInTree.Visible = false;
  }

  /// <summary>Инициализация доступных для выбора в диалоге "Настройка отображения"</summary>
  protected override void InitDefaultSupportedColumns()
  {
    this.TreeView.SupportedColumns = Intermech.Navigator.Utils.NavigatorColumns(NodeColumnSortOrder.Ascending);
  }

  /// <summary>Инициализация колонок</summary>
  protected override void InitVisibleColumns()
  {
    this.TreeView.SetColumns(Intermech.Navigator.Utils.CaptionAndStatesesColumns(NodeColumnSortOrder.Ascending));
  }

  /// <summary>Тип дерева навигатора по-умолчанию</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected override System.Type DefaultNavigatorTreeViewClass
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return typeof (ObjectsCompositionNavigatorTree);
    }
  }

  /// <summary>Creates tree view</summary>
  protected override void CreateTreeView()
  {
    base.CreateTreeView();
    this.TreeView.OnStartLoadTreeComposition += new AdvNavigatorTreeView.StartLoadTreeCompositionDelegate(this.OnTreeViewStartLoadTreeComposition);
    this.TreeView.OnLoadTreeCompositionProgress += new AdvNavigatorTreeView.LoadTreeCompositionProgressDelegate(this.OnTreeViewLoadTreeCompositionProgress);
    this.TreeView.OnFinishLoadTreeComposition += new AdvNavigatorTreeView.FinishLoadTreeCompositionDelegate(this.OnTreeViewFinishLoadTreeComposition);
    this.TreeView.BeforeBuildTree += new EventHandler(this.TreeView_BeforeBuildTree);
  }

  private void TreeView_BeforeBuildTree([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._visibleObjectTypes.Clear();
  }

  /// <summary>Вызывается при построении дерева</summary>
  protected override void BuildTree([CanBeNull] object sender, [NotNull] EventArgs e)
  {
  }

  /// <summary>UI: Дерево состава объекта</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public ObjectsCompositionNavigatorTree TreeView
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (ObjectsCompositionNavigatorTree) base.TreeView;
    }
  }

  /// <summary>Колонка поля "Статус" (!!! может быть null)</summary>
  [CanBeNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public NodeColumn StatusColumn
  {
    [DebuggerStepThrough] get
    {
      if (!this._statusColumnPropLoaded)
      {
        NodeColumn nodeColumn = (NodeColumn) null;
        foreach (NodeColumn treeColumn in (List<NodeColumn>) this.TreeView.TreeColumns)
        {
          if (treeColumn.ID != null && treeColumn.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && treeColumn.ID.Equals((object) "F_STATUSES"))
          {
            nodeColumn = treeColumn;
            break;
          }
        }
        this._statusColumn = nodeColumn;
        this._statusColumnPropLoaded = true;
      }
      return this._statusColumn;
    }
  }

  /// <summary>Колонка дерева содержащая заголовок (!!! может быть null)</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NavigatorTreeColumn StatusTreeColumn
  {
    [DebuggerStepThrough] get
    {
      return this.StatusColumn == null ? (NavigatorTreeColumn) null : this.TreeView.GetColumn(this.StatusColumn);
    }
  }

  /// <summary>Автоматически подстраивать ширину колонки "Заголовок объекта" после построения дерева</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_289")]
  [DefaultValue(true)]
  public bool AutoSizeCaptionColumnAfterBuildTree { get; set; } = true;

  /// <summary>Автоматически подстраивать ширину колонки "Статус" после построения дерева</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_290")]
  [DefaultValue(true)]
  public bool AutoSizeStatusColumnAfterBuildTree { get; set; } = true;

  /// <summary>Список идентификаторов версий корневых объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual IReadOnlyList<long> RootObjectVersionIDs
  {
    get
    {
      IReadOnlyList<Intermech.Navigator.DBObjects.Descriptor> rootDescriptors = this._RootDescriptors;
      return (rootDescriptors != null ? (IReadOnlyList<long>) rootDescriptors.Select<Intermech.Navigator.DBObjects.Descriptor, long>((Func<Intermech.Navigator.DBObjects.Descriptor, long>) (descriptor => descriptor.ObjectID)).ToList<long>(this._RootDescriptors.Count) : (IReadOnlyList<long>) null) ?? (IReadOnlyList<long>) Array.Empty<long>();
    }
    set
    {
      if (this.RootObjectVersionIDs.Equals((object) value))
        return;
      this.TreeView.Build((IReadOnlyCollection<long>) this.RootObjectVersionIDs);
    }
  }

  /// <summary>Список нод корневых объектов</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  [ItemNotNull]
  public IReadOnlyList<NavigatorTreeNode> RootObjectNavigatorTreeNodes
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this.TreeView.RootNode == null)
        return (IReadOnlyList<NavigatorTreeNode>) Array.Empty<NavigatorTreeNode>();
      if (this.TreeView.IsRootMultipleObjects)
        return (IReadOnlyList<NavigatorTreeNode>) this.TreeView.RootNode.Children ?? (IReadOnlyList<NavigatorTreeNode>) Array.Empty<NavigatorTreeNode>();
      return (IReadOnlyList<NavigatorTreeNode>) new NavigatorTreeNode[1]
      {
        this.TreeView.RootNode
      };
    }
  }

  /// <summary>Список идентификаторов нод корневых объектов</summary>
  [NotNull]
  [ItemNotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<NodeID> RootObjectNavigatorTreeNodeIDs
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.RootObjectNavigatorTreeNodes.MapListReadOnly<NavigatorTreeNode, NodeID>((Func<NavigatorTreeNode, NodeID>) (node => (NodeID) node.NodeID));
    }
  }

  /// <summary>Список идентификаторов типов корневых объектов</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<int> RootObjectTypes
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.RootObjectNavigatorTreeNodeIDs.Map<NodeID, int>((Func<NodeID, int>) (nodeID => nodeID.ObjectTypeID)).DistinctWithCapacity<int>().AsList<int>();
    }
  }

  /// <summary>Типы объектов, фигурирующие в дереве</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<int> VisibleObjectTypes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IReadOnlyList<int>) this._visibleObjectTypes;
    }
  }

  /// <summary>Типы объектов, фигурирующие в дереве + предки онных</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<int> VisibleObjectTypesWithParents
  {
    [DebuggerStepThrough] get
    {
      return (IReadOnlyList<int>) this._visibleObjectTypes.Concat<int>(this._visibleObjectTypes.SelectMany<int, int>(new Func<int, IEnumerable<int>>(MetaDataHelper.GetObjectTypeParentsID))).Distinct<int>().ToList<int>(checked (this._visibleObjectTypes.Count + this._visibleObjectTypes.Count >> 1));
    }
  }

  /// <summary>Перечисление нод объектов без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<NavigatorTreeNode> ObjectNodes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.ObjectNodes;
    }
  }

  /// <summary>Число объектов (из тех, кто уже загружены в составе)</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int LoadedObjectsCount
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      int result = 0;
      this.TreeView.InvokeForTreeNodes((Action<NavigatorTreeNode>) (node =>
      {
        if (node == null)
          return;
        INodeID nodeId = node.NodeID;
        bool? nullable = nodeId != null ? new bool?(nodeId.IsObjectCategory()) : new bool?();
        bool flag = true;
        if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
          return;
        ++result;
      }));
      return result;
    }
  }

  /// <summary>Число объектов (из тех, кто уже загружены в составе), состав которых не загружен</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int LoadedObjectsWithNotLoadedChildsCount
  {
    [DebuggerStepThrough] get
    {
      int result = 0;
      this.TreeView.InvokeForTreeNodes((Action<NavigatorTreeNode>) (node =>
      {
        if (node == null)
          return;
        INodeID nodeId = node.NodeID;
        bool? nullable = nodeId != null ? new bool?(nodeId.IsObjectCategory()) : new bool?();
        bool flag = true;
        if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) || !node.HasChildren || node.Full)
          return;
        ++result;
      }));
      return result;
    }
  }

  /// <summary>Идентификатор сфокусированной в дереве ноды</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NodeID FocusedNodeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.FocusedNodeID;
    }
  }

  /// <summary>Признак того, что в дереве сфокусирован объект</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ObjectIsFocused
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.ObjectIsFocused;
    }
  }

  /// <summary>Идентификатор версии сфокусированного в дереве объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long? FocusedObjectVersionID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.FocusedObjectVersionID;
    }
  }

  /// <summary>Идентификатор сфокусированного в дереве объекта (!!! НЕ ВЕРСИИ !!!)</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long? FocusedObjectID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.FocusedObjectID;
    }
  }

  /// <summary>Идентификатор типа сфокусированного в дереве объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int? FocusedObjectTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.FocusedObjectTypeID;
    }
  }

  /// <summary>Идентификатор связи сфокусированного в дереве объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long? FocusedObjectPrjLinkID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.FocusedObjectPrjLinkID;
    }
  }

  /// <summary>Идентификатор владельца сфокусированного в дереве объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long? FocusedObjectOwner
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.FocusedObjectOwner;
    }
  }

  /// <summary>Заголовок сфокусированного в дереве объекта</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string FocusedObjectCaption
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.FocusedObjectCaption;
    }
  }

  /// <summary>Последовательность идентификаторов выбранных в дереве нод</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerableWithCapacity<NodeID> SelectedObjectNodeIDsEnumeration
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedObjectNodeIDsEnumeration;
    }
  }

  /// <summary>Последовательность идентификаторов выбранных в дереве нод</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<NodeID> SelectedObjectNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedObjectNodeIDs;
    }
  }

  /// <summary>Признак того, что в дереве выбран и виден объект</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ObjectIsSelected
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.ObjectIsSelected;
    }
  }

  /// <summary>Последовательность идентификаторов версий выбранных в дереве объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedObjectVersionIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedObjectVersionIDs;
    }
  }

  /// <summary>Последовательность идентификаторов выбранных объектов (!!! НЕ ВЕРСИЙ !!!)</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedObjectIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedObjectIDs;
    }
  }

  /// <summary>Последовательность идентификаторов типов выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<int> SelectedObjectTypeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedObjectTypeIDs;
    }
  }

  /// <summary>Последовательность идентификаторов связей выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedObjectPrjLinkIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedObjectPrjLinkIDs;
    }
  }

  /// <summary>Последовательность заголовков выбранных в объекта</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<string> SelectedObjectCaptions
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedObjectCaptions;
    }
  }

  /// <summary>Последовательность идентификаторов выбранных в дереве нод</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerableWithCapacity<NodeID> SelectedClosestToRootObjectNodeIDsEnumeration
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedClosestToRootObjectNodeIDsEnumeration;
    }
  }

  /// <summary>Последовательность идентификаторов выбранных в дереве нод</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<NodeID> SelectedClosestToRootObjectNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedClosestToRootObjectNodeIDs;
    }
  }

  /// <summary>Последовательность идентификаторов версий выбранных в дереве объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedClosestToRootObjectVersionIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedClosestToRootObjectVersionIDs;
    }
  }

  /// <summary>Последовательность идентификаторов выбранных объектов (!!! НЕ ВЕРСИЙ !!!)</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedClosestToRootObjectIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedClosestToRootObjectIDs;
    }
  }

  /// <summary>Последовательность идентификаторов типов выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<int> SelectedClosestToRootObjectTypeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedClosestToRootObjectTypeIDs;
    }
  }

  /// <summary>Последовательность идентификаторов связей выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedClosestToRootObjectPrjLinkIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedClosestToRootObjectPrjLinkIDs;
    }
  }

  /// <summary>Последовательность заголовков выбранных в объекта</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<string> SelectedClosestToRootObjectCaptions
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.SelectedClosestToRootObjectCaptions;
    }
  }

  /// <summary>Инициализация пользовательского компонента</summary>
  public void Init([NotNull] IReadOnlyList<long> objectVersionIDs, [CanBeNull] IReadOnlyList<int> objectTypeIDs)
  {
    this.Init((System.IServiceProvider) null, (IReadOnlyCollection<long>) objectVersionIDs, (IReadOnlyCollection<int>) objectTypeIDs);
  }

  /// <summary>Инициализация пользовательского компонента</summary>
  /// <param name="advancedServices">Дополнительные сервисы логического контекста, в рамках которого был инициализирован контрол</param>
  public virtual void Init(
    [CanBeNull] System.IServiceProvider advancedServices,
    [NotNull] IReadOnlyCollection<long> objectVersionIDs,
    [CanBeNull] IReadOnlyCollection<int> objectTypeIDs)
  {
    if (this.Disposing || this.IsDisposed)
      return;
    this._ObjectTypeIDs = objectTypeIDs;
    if (advancedServices != null)
      this.Services = advancedServices;
    this.TreeView.Build(objectVersionIDs);
  }

  /// <summary>Очистить дерево</summary>
  public void ClearTree()
  {
    if (this.Disposing || this.IsDisposed)
      return;
    this._RootDescriptors = (IReadOnlyList<Intermech.Navigator.DBObjects.Descriptor>) null;
    this.TreeView.Build((IDescriptor) null);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.components.Dispose();
      this.components = (IContainer) null;
    }
    base.Dispose(disposing);
  }

  /// <summary>Вызывается после построения дерева</summary>
  private void _treeView_BuildTree([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.TreeView.RootNode == null || !this.TreeView.PopulateNodeAndWaitForFull(this.TreeView.RootNode))
      return;
    if (this.AutoSizeCaptionColumnAfterBuildTree)
      this.CaptionTreeColumn?.BestFit();
    if (!this.AutoSizeStatusColumnAfterBuildTree)
      return;
    this.StatusTreeColumn?.BestFit();
  }

  /// <summary>Список колонок был изменён</summary>
  protected override void Columns_ListChanged([CanBeNull] object sender, ListChangedEventArgs e)
  {
    this._statusColumnPropLoaded = false;
    this._statusColumn = (NodeColumn) null;
    base.Columns_ListChanged(sender, e);
  }

  /// <summary>Вызывается после загрузки всех дочерних нод</summary>
  protected override void AfterChildsLoaded(NavigatorTreeNode node)
  {
    if (node.Children != null && node.Children.Count > 0)
      this._visibleObjectTypes.SafeAddRange<int>(node.Children.SelectNotNull<NavigatorTreeNode, NodeID>((Func<NavigatorTreeNode, NodeID>) (navTreeNode => navTreeNode.NodeID as NodeID)).Select<NodeID, int>((Func<NodeID, int>) (nodeID => nodeID.TypeID)));
    base.AfterChildsLoaded(node);
    Application.DoEvents();
  }

  protected virtual void AfterObjectStructureIsLoadingForm() => this.EnableControls();

  /// <summary>Последовательность нод дерева, представляющих корневые объекты</summary>
  [NotNull]
  [ItemNotNull]
  public IReadOnlyList<NavigatorTreeNode> GetRootObjectsTreeNodes()
  {
    if (this.Disposing || this.IsDisposed || this.TreeView.RootNode?.Handler == null || !this.TreeView.RootNode.HasChildren)
      return (IReadOnlyList<NavigatorTreeNode>) Array.Empty<NavigatorTreeNode>();
    if (!this.TreeView.IsRootMultipleObjects)
      return (IReadOnlyList<NavigatorTreeNode>) new NavigatorTreeNode[1]
      {
        this.TreeView.RootNode
      };
    if (this.TreeView.RootNode != null)
    {
      this.TreeView.SetNodeExpanded(this.TreeView.RootNode, true);
      SpinWait.SpinUntil((Func<bool>) (() =>
      {
        NavigatorTreeNode rootNode = this.TreeView.RootNode;
        return rootNode == null || rootNode.Full;
      }), 60000);
    }
    return (IReadOnlyList<NavigatorTreeNode>) this.TreeView.RootNode?.Children ?? (IReadOnlyList<NavigatorTreeNode>) Array.Empty<NavigatorTreeNode>();
  }

  /// <summary>Загрузка полного состава</summary>
  public void LoadComposition([CanBeNull, ItemNotNull] IReadOnlyList<NavigatorTreeNode> nodes = null)
  {
    LevelsNumForm.QueryResult queryResult = LevelsNumForm.QueryForComposition(true);
    if (queryResult != null)
    {
      List<NavigatorTreeNode> navigatorTreeNodeList = (nodes != null ? nodes.ToList<NavigatorTreeNode>() : (List<NavigatorTreeNode>) null) ?? new List<NavigatorTreeNode>();
      if (!navigatorTreeNodeList.IsEmpty<NavigatorTreeNode>())
      {
        switch (queryResult.ResultType)
        {
          case LevelsNumForm.ResultType.Levels:
            this.TreeView.LoadCompositionLevels((IReadOnlyCollection<NavigatorTreeNode>) navigatorTreeNodeList, queryResult.Levels);
            break;
          case LevelsNumForm.ResultType.LevelsBreak:
            this.TreeView.LoadCompositionToLevel((IReadOnlyCollection<NavigatorTreeNode>) navigatorTreeNodeList, this.TreeView.IsRootMultipleObjects ? queryResult.Levels + 1 : queryResult.Levels);
            break;
          case LevelsNumForm.ResultType.All:
            this.TreeView.LoadFullComposition((IReadOnlyCollection<NavigatorTreeNode>) navigatorTreeNodeList);
            break;
        }
      }
    }
    this.TreeView.ReduceJobQueue();
  }

  private void CancelAutoLoad()
  {
    this._ObjectStructureIsLoadingForm = (ObjectStructureIsLoadingForm) null;
    this.TreeView.FireFinishLoadTreeComposition();
    this.EnableControls();
    this.AfterObjectStructureIsLoadingForm();
    this.UnlockSave("LoadComposition");
  }

  /// <summary>Вызывается при начале пакетной загрузки состава деревом</summary>
  protected virtual void OnTreeViewStartLoadTreeComposition(
    [NotNull] AdvNavigatorTreeView tree,
    [NotNull] IReadOnlyCollection<NavigatorTreeNode> nodes)
  {
    if (this._ObjectStructureIsLoadingForm != null)
      return;
    this.LockSave("LoadComposition");
    this._ObjectStructureIsLoadingForm = ObjectStructureIsLoadingForm.Init(this.FindForm(), new Action(this.CancelAutoLoad));
    this.DisableControls();
  }

  public event EventHandler OnControlsEnabled;

  protected virtual void EnableControls()
  {
    if (this.Enabled)
      return;
    this.Enabled = true;
    EventHandler onControlsEnabled = this.OnControlsEnabled;
    if (onControlsEnabled == null)
      return;
    onControlsEnabled((object) this, EventArgs.Empty);
  }

  public event EventHandler OnControlsDisabled;

  protected virtual void DisableControls()
  {
    if (!this.Enabled)
      return;
    this.Enabled = false;
    EventHandler controlsDisabled = this.OnControlsDisabled;
    if (controlsDisabled == null)
      return;
    controlsDisabled((object) this, EventArgs.Empty);
  }

  /// <summary>Вызывается при завершении пакетной загрузки состава деревом</summary>
  protected virtual void OnTreeViewFinishLoadTreeComposition([NotNull] AdvNavigatorTreeView tree)
  {
    if (this._ObjectStructureIsLoadingForm == null)
      return;
    this._ObjectStructureIsLoadingForm.Close();
  }

  /// <summary>Вызывается при обработке одной ноды в контексте пакетной загрузки состава деревом</summary>
  protected virtual void OnTreeViewLoadTreeCompositionProgress([NotNull] AdvNavigatorTreeView tree)
  {
    if (this._ObjectStructureIsLoadingForm == null)
      return;
    ++this._ObjectStructureIsLoadingForm.ObjectsLoaded;
  }

  /// <summary>Загрузка состава объектов на заданную глубину</summary>
  public void LoadComposition(
    [CanBeNull] IReadOnlyList<NavigatorTreeNode> nodes,
    int levelsToExpand,
    bool isBreakLevel)
  {
    nodes = nodes ?? this.RootObjectNavigatorTreeNodes;
    if (nodes.IsEmpty<NavigatorTreeNode>())
      return;
    if (isBreakLevel)
      this.TreeView.LoadCompositionToLevel((IReadOnlyCollection<NavigatorTreeNode>) nodes, this.TreeView.IsRootMultipleObjects ? levelsToExpand + 1 : levelsToExpand);
    else
      this.TreeView.LoadCompositionLevels((IReadOnlyCollection<NavigatorTreeNode>) nodes, levelsToExpand);
  }

  /// <summary>Обработчик события очистки очереди запланированных работ</summary>
  private void TreeView_JobsUpdateCanceled([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._ObjectStructureIsLoadingForm == null)
      return;
    this._ObjectStructureIsLoadingForm.Close();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._treeView.BeginInit();
    this.SuspendLayout();
    this._treeView.BackgroundImageMode = ImageDrawMode.Tile;
    this._treeView.BorderStyle = BorderStyle.Fixed3D;
    this._treeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._treeView.RowEvenStyle.WordWrap = false;
    this._treeView.RowOddStyle.WordWrap = false;
    this._treeView.RowSelectedStyle.WordWrap = false;
    this._treeView.RowStyle.BorderColor = SystemColors.Control;
    this._treeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this._treeView.RowStyle.BorderWidth = 1;
    this._treeView.RowStyle.WordWrap = false;
    this._treeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this._treeView.Size = new Size(773, 485);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (ObjectCompositionsNavTreeView);
    this.Size = new Size(773, 512 /*0x0200*/);
    this._treeView.EndInit();
    this.ResumeLayout(false);
  }
}
