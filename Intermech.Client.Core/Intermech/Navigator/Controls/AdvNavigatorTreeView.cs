
// Type: Intermech.Navigator.Controls.AdvNavigatorTreeView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.ComponentModel;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Более продвинутое дерево навигатора.</summary>
public class AdvNavigatorTreeView : 
  NavigatorTreeView,
  IContextAware,
  ISelectedItemsHost,
  IIOSource,
  ICommandsProvider,
  ICommandTarget,
  ISupportInitialize,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  /// <summary>Флаг для хранения признака того, показывать плюсик только в случае, если внутри действительно есть подузлы
  /// Если null - используется глобальная настройка</summary>
  private bool? _overrideBackgroundTreeTasks;
  /// <summary>Счётчик вложенности вызовов FireStartLoadTreeComposition/FireFinishLoadTreeComposition</summary>
  private int _loadTreeCompositionCounter;
  /// <summary>Словарь нод, которые поставлены в очередь на автоматическую загрузку состава.
  /// Ключом в словаре выступает нода, значением - функция контроля продолжения загрузки состава</summary>
  [NotNull]
  private readonly Dictionary<NavigatorTreeNode, Func<NavigatorTreeNode, bool>> _autoLoadCompositionTreeNodesQueue = new Dictionary<NavigatorTreeNode, Func<NavigatorTreeNode, bool>>((IEqualityComparer<NavigatorTreeNode>) NavigatorTreeNode.LinksComparer);

  /// <summary>Перечисление всех нод дерева</summary>
  [NotNull]
  [ItemNotNull]
  public IEnumerable<NavigatorTreeNode> AllNodes => this.NodesEnumeration();

  /// <summary>Сфокусированная в данный момент нода дерева</summary>
  [Browsable(false)]
  [CanBeNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NavigatorTreeNode FocusedTreeNode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetFocusedTreeNode();
    }
  }

  /// <summary>Интерфейс идентификатора сфокусированной в дереве ноды</summary>
  [Browsable(false)]
  [CanBeNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INodeID FocusedNodeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetFocusedNodeID();
    }
  }

  /// <summary>Идентификатор категории сфокусированной в данной момент в дереве сущности</summary>
  [Browsable(false)]
  [CanBeNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int? FocusedCategoryID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetFocusedCategoryID();
    }
  }

  /// <summary>Идентификатор типа сфокусированной в данной момент в дереве сущности</summary>
  [Browsable(false)]
  [CanBeNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int? FocusedTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetFocusedTypeID();
    }
  }

  /// <summary>Перечисление выбранных нод дерева навигатора без какой-либо фильтрации</summary>
  [Browsable(false)]
  [NotNull]
  [ItemNotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<NavigatorTreeNode> SelectedNodesList
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetSelectedNodes();
    }
  }

  /// <summary>Перечисление интерфейсов идентификаторов выбранных нод без какой-либо фильтрации</summary>
  [Browsable(false)]
  [NotNull]
  [ItemNotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<INodeID> SelectedNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetSelectedNodeIDs();
    }
  }

  /// <summary>Перечисление идентификаторов категорий выбранных сущностей без какой-либо фильтрации</summary>
  [Browsable(false)]
  [NotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<int> SelectedCategoryIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetSelectedCategoryIDs();
    }
  }

  /// <summary>Перечисление выбранных нод дерева навигатора
  /// При этом те ноды, у которых выбрана какая-нибудь из вышестоящих нод в перечисление не попадает</summary>
  [Browsable(false)]
  [NotNull]
  [ItemNotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<NavigatorTreeNode> SelectedClosestToRootNodes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetSelectedNodesClosestToRoot();
    }
  }

  /// <summary>Перечисление интерфейсов идентификаторов выбранных сущностей
  /// При этом те ноды, у которых выбрана какая-нибудь из вышестоящих нод в перечисление не попадает</summary>
  [Browsable(false)]
  [NotNull]
  [ItemNotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<INodeID> SelectedClosestToRootNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetSelectedClosestToRootNodeIDs();
    }
  }

  /// <summary>Перечисление идентификаторов категорий выбранных сущностей
  /// При этом те ноды, у которых выбрана какая-нибудь из вышестоящих нод в перечисление не попадает</summary>
  [Browsable(false)]
  [NotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<int> SelectedClosestToRootCategoryIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetSelectedClosestToRootCategoryIDs();
    }
  }

  /// <summary>Список отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [Browsable(false)]
  [NotNull]
  [ItemNotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<NavigatorTreeNode> CheckedNodesList
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetCheckedNodes();
    }
  }

  /// <summary>Перечисление интерфейсов идентификаторов отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [Browsable(false)]
  [NotNull]
  [ItemNotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<INodeID> CheckedNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetCheckedNodeIDs();
    }
  }

  /// <summary>Наше дерево показывает плюсик только в случае, если внутри действительно есть подузлы!</summary>
  protected override bool BackgroundTreeTasks
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._overrideBackgroundTreeTasks ?? base.BackgroundTreeTasks;
    }
  }

  /// <summary>Флаг для хранения признака того, показывать плюсик только в случае, если внутри действительно есть подузлы
  /// Если null - используется глобальная настройка</summary>
  [Category("Appearance")]
  [Browsable(true)]
  [Description("Показывать ли плюсики только в случае, если внутри действительно есть подузлы")]
  [DefaultValue(AdvNavigatorTreeView.NullAbleBoolDefault.NotSet)]
  public AdvNavigatorTreeView.NullAbleBoolDefault BackgroundAutoLoadComposition
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (!this._overrideBackgroundTreeTasks.HasValue)
        return AdvNavigatorTreeView.NullAbleBoolDefault.NotSet;
      return !this._overrideBackgroundTreeTasks.Value ? AdvNavigatorTreeView.NullAbleBoolDefault.False : AdvNavigatorTreeView.NullAbleBoolDefault.True;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._overrideBackgroundTreeTasks = value == AdvNavigatorTreeView.NullAbleBoolDefault.NotSet ? new bool?() : new bool?(value == AdvNavigatorTreeView.NullAbleBoolDefault.True);
    }
  }

  /// <summary>Функция проверки необходимости рекурсивной загрузки состава</summary>
  protected virtual bool DefaultCompositionLoadCheckFunc([NotNull] NavigatorTreeNode node) => true;

  /// <summary>Загрузка состава до уровня</summary>
  /// <param name="nodes">Список нод</param>
  /// <param name="toAbsoluteNodesLevel">Уровень вложенности, до которого (включительно) должна производится загрузка
  /// состава (первый уровень = 1). То есть, например, если передать 2, то состав будет загружен нодах в корне (уровень
  /// 1) и вложенных в них (уровень 2)</param>
  public void LoadCompositionToLevel(
    [CanBeNull] IReadOnlyCollection<NavigatorTreeNode> nodes,
    [PositiveNumber] int toAbsoluteNodesLevel)
  {
    this.LoadComposition(nodes, (Func<NavigatorTreeNode, bool>) (navTreeNode => navTreeNode.Level <= toAbsoluteNodesLevel && this.DefaultCompositionLoadCheckFunc(navTreeNode)));
  }

  /// <summary>Загрузка состава до уровня</summary>
  /// <param name="node">Нода, состав которой должен быть загружен</param>
  /// <param name="level">Уровень вложенности, до которого (включительно) должна производится загрузка
  /// состава (первый уровень = 1). То есть, например, если передать 2, то состав будет загружен нодах в корне (уровень
  /// 1) и вложенных в них (уровень 2)</param>
  public void LoadCompositionToLevel([NotNull] NavigatorTreeNode node, [ZeroOrPositiveNumber] int level)
  {
    if (level <= node.Level)
      return;
    this.LoadCompositionToLevel((IReadOnlyCollection<NavigatorTreeNode>) new NavigatorTreeNode[1]
    {
      node
    }, level);
  }

  /// <summary>Загрузка состава переданного числа дочерних уровней у переданных нод</summary>
  /// <param name="node">Нода, состав которой должен быть загружен</param>
  /// <param name="loadLevels">Сколько уровней вложенности переданной ноды должно быть загружено. То есть, если передать 0,
  /// то будет загружен лишь состав переданной ноды, если 1 - переданной ноды и входящих в неё и т.д.</param>
  public void LoadCompositionLevels([NotNull] NavigatorTreeNode node, [PositiveNumber] int loadLevels)
  {
    if (loadLevels == 0 || !node.HasChildren)
      return;
    this.LoadCompositionLevels((IReadOnlyCollection<NavigatorTreeNode>) new NavigatorTreeNode[1]
    {
      node
    }, loadLevels);
  }

  /// <summary>Загрузка состава переданного числа дочерних уровней у переданных нод</summary>
  /// <param name="nodes">Перечисление нод, состав которых должен быть загружен</param>
  /// <param name="loadLevels">Сколько уровней вложенности переданной ноды должно быть загружено. То есть, если передать 0,
  /// то будет загружен лишь состав переданной ноды, если 1 - переданной ноды и входящих в неё и т.д.</param>
  public void LoadCompositionLevels([NotNull, ItemNotNull] IReadOnlyCollection<NavigatorTreeNode> nodes, [PositiveNumber] int loadLevels)
  {
    if (loadLevels == 0)
      return;
    bool flag = false;
    foreach (NavigatorTreeNode navigatorTreeNode in (IEnumerable<NavigatorTreeNode>) nodes.Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node => node.HasChildren)).OrderByDescending<NavigatorTreeNode, int>((Func<NavigatorTreeNode, int>) (navNode => navNode.Level)))
    {
      int targetLevel = navigatorTreeNode.Level + loadLevels;
      List<NavigatorTreeNode> list = ((IEnumerable<NavigatorTreeNode>) new NavigatorTreeNode[1]
      {
        navigatorTreeNode
      }).EnumerationWithChilds((Func<NavigatorTreeNode, bool>) (childNode => childNode.HasChildren && !childNode.Full), new Func<NavigatorTreeNode, bool>(ContinueLoadCompositionCheck)).ToList<NavigatorTreeNode>();
      flag = flag || this.LoadComposition((IReadOnlyCollection<NavigatorTreeNode>) list, new Func<NavigatorTreeNode, bool>(ContinueLoadCompositionCheck), true);

      bool ContinueLoadCompositionCheck(NavigatorTreeNode childNode)
      {
        return childNode.Level < targetLevel && this.DefaultCompositionLoadCheckFunc(childNode);
      }
    }
    if (!flag || !this._autoLoadCompositionTreeNodesQueue.Any<KeyValuePair<NavigatorTreeNode, Func<NavigatorTreeNode, bool>>>())
      return;
    this.FireStartLoadTreeComposition(nodes);
    this.ReduceJobQueue();
  }

  /// <summary>Загрузка полного состава у переданной ноды</summary>
  /// <param name="node">Нода, полный состав которой должен быть загружен</param>
  public void LoadFullComposition([NotNull] NavigatorTreeNode node)
  {
    this.LoadComposition((IReadOnlyCollection<NavigatorTreeNode>) new NavigatorTreeNode[1]
    {
      node
    }, new Func<NavigatorTreeNode, bool>(this.DefaultCompositionLoadCheckFunc));
  }

  /// <summary>Загрузка полного состава у переданных нод (или у всех нод, если передан null)</summary>
  /// <param name="nodes">Перечисление нод, полный состав которых должен быть загружен (если null - загружаются все ноды дерева)</param>
  public void LoadFullComposition([CanBeNull] IReadOnlyCollection<NavigatorTreeNode> nodes = null)
  {
    this.LoadComposition(nodes, new Func<NavigatorTreeNode, bool>(this.DefaultCompositionLoadCheckFunc));
  }

  /// <summary>Функция высшего порядка для рекурсивной загрузки состава дерева</summary>
  /// <param name="nodes">Список нод. Если null, то загрузка состава запускается для всех уже загруженных нод</param>
  /// <param name="continueLoadCheckFunc">Делегат функции проверки необходимости загрузки состава ноды в рамках данной операции (принимает
  /// параметром ноду)</param>
  /// <param name="externalNodesProcessing">Признак того, что фильтр</param>
  /// <returns>true если очередь нод, поставленных на обработку выросла, иначе false</returns>
  public bool LoadComposition(
    [CanBeNull] IReadOnlyCollection<NavigatorTreeNode> nodes,
    [CanBeNull] Func<NavigatorTreeNode, bool> continueLoadCheckFunc,
    bool externalNodesProcessing = false)
  {
    IReadOnlyCollection<NavigatorTreeNode> nodes1 = externalNodesProcessing ? nodes : (IReadOnlyCollection<NavigatorTreeNode>) ((nodes != null ? nodes.MinusSelfContains().EnumerationWithChilds() : (IEnumerable<NavigatorTreeNode>) null) ?? this.AllNodes).Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node =>
    {
      if (!node.HasChildren || node.Full)
        return false;
      Func<NavigatorTreeNode, bool> func = continueLoadCheckFunc;
      return func == null || func(node);
    })).ToList<NavigatorTreeNode>();
    int num = this.AutoLoadChildNodes((IEnumerable<NavigatorTreeNode>) nodes1, continueLoadCheckFunc) ? 1 : 0;
    if (num == 0)
      return num != 0;
    if (externalNodesProcessing)
      return num != 0;
    this.FireStartLoadTreeComposition(nodes1);
    this.ReduceJobQueue();
    return num != 0;
  }

  /// <summary>Прервать текущую опера</summary>
  public void CancelLoadComposition()
  {
    this._autoLoadCompositionTreeNodesQueue.Clear();
    this.CancelUpdateJobs((object) null, false);
    this.FireFinishLoadTreeComposition();
  }

  /// <summary>Вызывается в начале операции рекурсивной загрузки состава. Можно например показать прогресс-бар и
  /// заблокировать до окончании операции управляющие элементы UI</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event AdvNavigatorTreeView.StartLoadTreeCompositionDelegate OnStartLoadTreeComposition;

  protected virtual void FireStartLoadTreeComposition([NotNull] IReadOnlyCollection<NavigatorTreeNode> nodes)
  {
    if (Interlocked.Increment(ref this._loadTreeCompositionCounter) != 1)
      return;
    AdvNavigatorTreeView.StartLoadTreeCompositionDelegate loadTreeComposition = this.OnStartLoadTreeComposition;
    if (loadTreeComposition == null)
      return;
    loadTreeComposition(this, nodes);
  }

  /// <summary>Вызывается по окончании операции рекурсивной загрузки состава. Можно например скрыть прогресс-бар и убрать
  /// блокировки с управляющих элементов UI</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event AdvNavigatorTreeView.FinishLoadTreeCompositionDelegate OnFinishLoadTreeComposition;

  public virtual void FireFinishLoadTreeComposition()
  {
    if (Interlocked.Decrement(ref this._loadTreeCompositionCounter) != 0)
      return;
    AdvNavigatorTreeView.FinishLoadTreeCompositionDelegate loadTreeComposition = this.OnFinishLoadTreeComposition;
    if (loadTreeComposition == null)
      return;
    loadTreeComposition(this);
  }

  /// <summary>Вызывается после обработки каждой ноды в рамках операции рекурсивной загрузки состава</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event AdvNavigatorTreeView.LoadTreeCompositionProgressDelegate OnLoadTreeCompositionProgress;

  public virtual void FireLoadTreeCompositionProgress()
  {
    if (this._loadTreeCompositionCounter <= 0)
      return;
    AdvNavigatorTreeView.LoadTreeCompositionProgressDelegate compositionProgress = this.OnLoadTreeCompositionProgress;
    if (compositionProgress == null)
      return;
    compositionProgress(this);
  }

  /// <summary>Вызывается после загрузки содержимого ноды</summary>
  protected override void FireAfterNodeChildsLoaded([CanBeNull] NavigatorTreeNode node)
  {
    if (node != null && this._autoLoadCompositionTreeNodesQueue.Any<KeyValuePair<NavigatorTreeNode, Func<NavigatorTreeNode, bool>>>())
      this.ProcessAutoLoadCompositionQuery(node);
    base.FireAfterNodeChildsLoaded(node);
  }

  /// <summary>Обработка ноды в рамках операции рекурсивной загрузки состава дерева</summary>
  private void ProcessAutoLoadCompositionQuery([NotNull] NavigatorTreeNode node)
  {
    lock (this._autoLoadCompositionTreeNodesQueue)
    {
      Func<NavigatorTreeNode, bool> checkContinueLoadCompositionFunction;
      if (!this._autoLoadCompositionTreeNodesQueue.TryGetValue(node, out checkContinueLoadCompositionFunction))
        return;
      this._autoLoadCompositionTreeNodesQueue.Remove(node);
      this.FireLoadTreeCompositionProgress();
      if (node.HasChildren && node.Full && node.Children != null && node.Children.Count > 0 && this.AutoLoadChildNodes((IEnumerable<NavigatorTreeNode>) node.Children.Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (childNode => childNode.HasChildren && !childNode.Full)).ToList<NavigatorTreeNode>(), checkContinueLoadCompositionFunction))
        this.ReduceJobQueue();
      if (this._autoLoadCompositionTreeNodesQueue.Count != 0)
        return;
      this.CancelLoadComposition();
    }
  }

  /// <summary>Постановка в очередь на загрузку последовательности нод</summary>
  /// <param name="nodes">Список нод</param>
  /// <param name="checkContinueLoadCompositionFunction">Функция проверки необходимости продолжать загрузку</param>
  /// <returns>true в очередь на загрузку были поставлены новые ноды, иначе false</returns>
  private bool AutoLoadChildNodes(
    [CanBeNull, ItemNotNull] IEnumerable<NavigatorTreeNode> nodes,
    [CanBeNull] Func<NavigatorTreeNode, bool> checkContinueLoadCompositionFunction)
  {
    bool flag = false;
    if (nodes != null)
    {
      lock (this._autoLoadCompositionTreeNodesQueue)
      {
        foreach (NavigatorTreeNode navigatorTreeNode in nodes.Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node =>
        {
          Func<NavigatorTreeNode, bool> func = checkContinueLoadCompositionFunction;
          return func == null || func(node);
        })))
        {
          this._autoLoadCompositionTreeNodesQueue[navigatorTreeNode] = checkContinueLoadCompositionFunction;
          this.QueuePlusJob(navigatorTreeNode);
          flag = true;
        }
      }
    }
    return flag;
  }

  /// <summary>Построить дерево на основе указанного дескриптора</summary>
  /// <param name="descriptor">Описание корневого узла дерева</param>
  protected override void BuildCore([NotNull] IDescriptor descriptor)
  {
    this.RaiseBeforeBuildTreeEvent();
    base.BuildCore(descriptor);
  }

  /// <summary>Сгенерировать событие "Сейчас дерево будет перестраиваться"</summary>
  protected virtual void RaiseBeforeBuildTreeEvent()
  {
    EventHandler beforeBuildTree = this.BeforeBuildTree;
    if (beforeBuildTree == null)
      return;
    beforeBuildTree((object) this, EventArgs.Empty);
  }

  /// <summary>Событие вызывается перед построением дерева</summary>
  [Browsable(true)]
  public event EventHandler BeforeBuildTree;

  [TypeConverter(typeof (EnumCustomConverter))]
  public enum NullAbleBoolDefault
  {
    [Description("Default")] NotSet,
    [Description("True")] True,
    [Description("False")] False,
  }

  public delegate void StartLoadTreeCompositionDelegate(
    [NotNull] AdvNavigatorTreeView tree,
    [NotNull] IReadOnlyCollection<NavigatorTreeNode> nodes);

  public delegate void FinishLoadTreeCompositionDelegate([NotNull] AdvNavigatorTreeView tree);

  public delegate void LoadTreeCompositionProgressDelegate([NotNull] AdvNavigatorTreeView tree);
}
