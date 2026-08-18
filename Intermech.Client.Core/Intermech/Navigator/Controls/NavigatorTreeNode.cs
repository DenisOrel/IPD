
// Type: Intermech.Navigator.Controls.NavigatorTreeNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Узел в дереве "Навигатора"</summary>
public class NavigatorTreeNode : ICloneable
{
  /// <summary>
  /// Массив для того, чтобы дерево "изображало" наличие вложенных узлов, если они ещё реально не были созданы
  /// </summary>
  private static int[] _fakeItems = new int[1];
  /// <summary>Дерево-владелец</summary>
  private NavigatorTreeView _tree;
  /// <summary>Родительский узел дерева "Навигатора"</summary>
  private NavigatorTreeNode _parent;
  /// <summary>Значения ячеек узла (для отображения на экране)</summary>
  private object[] _values;
  /// <summary>
  /// Значения ячеек узла (исходные данные, до преобразования)
  /// </summary>
  private object[] _rawValues;
  /// <summary>Описание узла</summary>
  private INodeID _nodeID;
  /// <summary>Узел</summary>
  private INode _handler;
  /// <summary>Флажки узла</summary>
  private TreeNodeFlags _flags;
  /// <summary>Закладка</summary>
  private object _bookmark;
  /// <summary>
  /// В узел прочитаны все данные, пакетное чтение не должно трогать этот узел
  /// </summary>
  private volatile bool _full;
  /// <summary>
  /// Набор состояний колонок (валидны ли значения в колонках или нет - маска колонок)
  /// </summary>
  private StatesRecord _validColumns;
  /// <summary>
  /// Циклическая зависимость ("петля"), обнаруженная для данного узла
  /// </summary>
  private NavigatorNodeCycle _cycle;
  /// <summary>
  /// Ссылка на строку дерева, с которой связан данный экземпляр класса
  /// </summary>
  private Row _handle;
  /// <summary>
  /// Есть ли в узле дочерние элементы. Свойство можно использовать для того,
  /// чтобы показать [+] в узле дерева, даже если у NavigatorTreeNode ещё нет
  /// дочерних элементов.
  /// </summary>
  private bool _hasChildren;
  /// <summary>Коллекция изображений</summary>
  private ImageList _images;
  private Dictionary<NodeColumn, bool> _cellReadOnlyDictionary = new Dictionary<NodeColumn, bool>();
  /// <summary>Индекс изображения в коллекции изображений</summary>
  private int _imageIndex = -1;
  /// <summary>
  /// Индекс изображения в коллекции изображений для выделенной строки
  /// </summary>
  private int _selectedImageIndex = -1;
  /// <summary>Индекс изображения в коллекции изображений</summary>
  private int _stateImageIndex = -1;
  /// <summary>
  /// Значок для строки (приоритет выше, чем у свойства ImageIndex)
  /// </summary>
  private Icon _icon;
  /// <summary>Статус узла</summary>
  public CheckState _checkState;
  /// <summary>Показывать ли чекбокс у данной конкретной ноды в том случае, если настройки дерева предполагают отображение чекбоксов</summary>
  protected bool _showCheckState = true;
  /// <summary>Количество отмеченных узлов</summary>
  protected int _checkedCount;
  /// <summary>Количество частично отмеченных узлов</summary>
  protected int _indeterminateCount;
  /// <summary>Дополнительное поле</summary>
  private object _tag;
  /// <summary>
  /// Используется для определения, была ли установка отметки начальной операцией, или пришло от родителя/дочернего элемента
  /// </summary>
  public NavigatorTreeNode.UpdateState State;
  /// <summary>кастомное сравнение нод сравнивающее не по NodeID (у NavigatorTreeNode перекрыт Equals и сравнение идёт по NodeID),
  /// а по ссылке на объект. Позволяет например нормально запихивать в Dictionary несколько разных нод с одинаковым NodeID</summary>
  public static readonly NavigatorTreeNodeLinksComparer LinksComparer = new NavigatorTreeNodeLinksComparer();

  public void SetCellReadOnly(NodeColumn nodeColumn, bool readOnly)
  {
    if (nodeColumn == null)
      throw new ArgumentNullException(nameof (nodeColumn));
    this._cellReadOnlyDictionary[nodeColumn] = readOnly;
  }

  public bool IsCellReadOnly(NodeColumn nodeColumn)
  {
    if (nodeColumn == null)
      throw new ArgumentNullException(nameof (nodeColumn));
    bool flag = false;
    this._cellReadOnlyDictionary.TryGetValue(nodeColumn, out flag);
    return flag;
  }

  /// <summary>Создать экземпляр класса NavigatorTreeNode</summary>
  /// <param name="tree">Дерево-владелец</param>
  /// <param name="parent">Родительский узел дерева "Навигатора"</param>
  /// <param name="nodeID">Описание узла</param>
  public NavigatorTreeNode(NavigatorTreeView tree, NavigatorTreeNode parent, INodeID nodeID)
    : this(tree, parent, nodeID, (object[]) null, (object[]) null)
  {
  }

  /// <summary>Создать экземпляр класса NavigatorTreeNode</summary>
  /// <param name="tree">Дерево-владелец</param>
  /// <param name="parent">Родительский узел дерева "Навигатора"</param>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла (для отображения на экране)</param>
  /// <param name="rawValues">Значения ячеек узла (исходные данные)</param>
  public NavigatorTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeID,
    object[] values,
    object[] rawValues)
    : this(tree, parent, nodeID, values, rawValues, (INode) null)
  {
  }

  /// <summary>Создать экземпляр класса NavigatorTreeNode</summary>
  /// <param name="tree">Дерево-владелец</param>
  /// <param name="parent">Родительский узел дерева "Навигатора"</param>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла (для отображения на экране)</param>
  /// <param name="rawValues">Значения ячеек узла (исходные данные)</param>
  /// <param name="handler">Узел</param>
  public NavigatorTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeID,
    object[] values,
    object[] rawValues,
    INode handler)
    : this(tree, parent, nodeID, values, rawValues, handler, TreeNodeFlags.ImageOutdated)
  {
  }

  /// <summary>Создать экземпляр класса NavigatorTreeNode</summary>
  /// <param name="tree">Дерево-владелец</param>
  /// <param name="parent">Родительский узел дерева "Навигатора"</param>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла (для отображения на экране)</param>
  /// <param name="rawValues">Значения ячеек узла (исходные данные)</param>
  /// <param name="handler">Узел</param>
  /// <param name="flags">Флажки узла</param>
  public NavigatorTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeID,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags)
    : this(tree, parent, nodeID, values, rawValues, handler, flags, (object) null)
  {
  }

  /// <summary>Создать экземпляр класса NavigatorTreeNode</summary>
  /// <param name="tree">Дерево-владелец</param>
  /// <param name="parent">Родительский узел дерева "Навигатора"</param>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла (для отображения на экране)</param>
  /// <param name="rawValues">Значения ячеек узла (исходные данные)</param>
  /// <param name="handler">Узел</param>
  /// <param name="flags">Флажки узла</param>
  /// <param name="bookmark">Закладка</param>
  public NavigatorTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeID,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark)
    : this(tree, parent, nodeID, values, rawValues, handler, flags, bookmark, false)
  {
  }

  /// <summary>Создать экземпляр класса NavigatorTreeNode</summary>
  /// <param name="tree">Дерево-владелец</param>
  /// <param name="parent">Родительский узел дерева "Навигатора"</param>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла (для отображения на экране)</param>
  /// <param name="rawValues">Значения ячеек узла (исходные данные)</param>
  /// <param name="handler">Узел</param>
  /// <param name="flags">Флажки узла</param>
  /// <param name="bookmark">Закладка</param>
  /// <param name="full">В узел прочитаны все данные, пакетное чтение не должно трогать этот узел</param>
  public NavigatorTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeID,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark,
    bool full)
    : this(tree, parent, nodeID, values, rawValues, handler, flags, bookmark, full, (StatesRecord) null)
  {
  }

  /// <summary>
  /// Полная версия конструктора - создать экземпляр класса NavigatorTreeNode
  /// </summary>
  /// <param name="tree">Дерево-владелец</param>
  /// <param name="parent">Родительский узел дерева "Навигатора"</param>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла (для отображения на экране)</param>
  /// <param name="rawValues">Значения ячеек узла (исходные данные)</param>
  /// <param name="handler">Узел</param>
  /// <param name="flags">Флажки узла</param>
  /// <param name="bookmark">Закладка</param>
  /// <param name="full">В узел прочитаны все данные, пакетное чтение не должно трогать этот узел</param>
  /// <param name="validColumns">Набор состояний колонок (валидны ли значения в колонках или нет - маска колонок)</param>
  public NavigatorTreeNode(
    NavigatorTreeView tree,
    NavigatorTreeNode parent,
    INodeID nodeID,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark,
    bool full,
    StatesRecord validColumns)
  {
    this._tree = tree;
    this._parent = parent;
    this._nodeID = nodeID;
    this._values = values;
    this._rawValues = rawValues;
    this._handler = handler;
    this._flags = flags;
    this._bookmark = bookmark;
    this._full = full;
    this._validColumns = validColumns;
    if (this._parent != null)
      this._parent.Children.Add(this);
    this.Children = new NavigatorTreeNodes(this._tree, this);
  }

  /// <summary>
  /// Ссылка на строку дерева, с которой связан данный экземпляр класса
  /// </summary>
  public Row Handle
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._handle;
    set => this._handle = value;
  }

  /// <summary>Дерево-владелец</summary>
  public NavigatorTreeView Tree
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._tree;
  }

  /// <summary>Родительский узел дерева "Навигатора"</summary>
  public NavigatorTreeNode Parent
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._parent;
    set => this._parent = value;
  }

  /// <summary>
  /// Массив для того, чтобы дерево "изображало" наличие вложенных узлов, если они ещё реально не были созданы
  /// </summary>
  public int[] FakeItems
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return NavigatorTreeNode._fakeItems;
    }
  }

  /// <summary>Значения ячеек узла (для отображения на экране)</summary>
  public object[] Values
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this._values == null && this._tree.Columns != null)
        this._values = new object[this._tree.Columns.Count];
      return this._values;
    }
    set
    {
      this._values = value;
      if (this._values != null || this._tree.Columns == null)
        return;
      this._values = new object[this._tree.Columns.Count];
    }
  }

  /// <summary>
  /// Значения ячеек узла (исходные данные, до преобразования)
  /// </summary>
  public object[] RawValues
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this._rawValues == null && this._tree.Columns != null)
        this._rawValues = new object[this._tree.Columns.Count];
      return this._rawValues;
    }
    set
    {
      this._rawValues = value;
      if (this._rawValues == null && this._tree.Columns != null)
        this._rawValues = new object[this._tree.Columns.Count];
      for (int index = 0; index < this._rawValues.Length; ++index)
      {
        if (this._rawValues[index] is byte[])
          this._rawValues[index] = (object) null;
      }
    }
  }

  /// <summary>Описание узла</summary>
  public INodeID NodeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._nodeID;
    set => this._nodeID = value;
  }

  /// <summary>Узел</summary>
  public INode Handler
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._handler;
    set => this._handler = value;
  }

  /// <summary>Флажки узла</summary>
  public TreeNodeFlags Flags
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._flags;
    set => this._flags = value;
  }

  /// <summary>Закладка</summary>
  public object Bookmark
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._bookmark;
    set => this._bookmark = value;
  }

  /// <summary>
  /// В узел прочитаны все данные, пакетное чтение не должно трогать этот узел
  /// </summary>
  public bool Full
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      lock (this)
        return this._full;
    }
    set
    {
      lock (this)
        this._full = value;
    }
  }

  /// <summary>
  /// Набор состояний колонок (валидны ли значения в колонках или нет - маска колонок)
  /// </summary>
  public StatesRecord ValidColumns
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._validColumns;
    }
    set => this._validColumns = value;
  }

  /// <summary>
  /// Значок для строки (приоритет выше, чем у свойства ImageIndex)
  /// </summary>
  public Icon Icon
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._icon;
    set => this._icon = value;
  }

  public void Fetch()
  {
    if (this.Full)
      return;
    this.Reload();
  }

  public void Reload()
  {
    lock (this)
    {
      this.Children.Clear();
      this.HasChildren = false;
      this.Full = true;
      try
      {
        if (this.Handler == null || this.Cycle != NavigatorNodeCycle.None)
          return;
        INodeQuery query = this.Handler.GetQuery(ContentType.Folders);
        if (query == null)
          return;
        this.Tree.SetQueryColumns(query, this.Tree._treeColumns);
        query.Execute((object) null, 2147483646);
        if (query.RecordCount > 0)
        {
          for (int index = 0; index < query.RecordCount; ++index)
          {
            NavigatorTreeNode node = this.Tree.CreateNode(this, query.GetRecordNodeID(index), query.GetRecordValues(index), query.GetRawRecordValues(index), this.Tree._treeColumns, false);
            if (node != null)
              node.Flags ^= TreeNodeFlags.ImageOutdated;
          }
        }
        this.HasChildren = this.Children.Count > 0;
      }
      catch
      {
        this.Full = false;
        this.HasChildren = false;
        this.Children.Clear();
        throw;
      }
    }
  }

  public void ClearChildren()
  {
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this.Children)
    {
      child.Handle = (Row) null;
      child.Parent = (NavigatorTreeNode) null;
      child.ClearChildren();
    }
    this.Children.Clear();
  }

  public void EnsureVisible()
  {
    if (this._handle != null && (this._handle == null || this._handle.Visible && this.GetAncestors().All<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (o => o.Expanded))))
      return;
    List<NavigatorTreeNode> source1 = new List<NavigatorTreeNode>();
    foreach (NavigatorTreeNode navigatorTreeNode in this.GetAncestorsAndSelf())
    {
      source1.Add(navigatorTreeNode);
      if (navigatorTreeNode.Visible)
        break;
    }
    IEnumerable<NavigatorTreeNode> source2 = source1.Reverse<NavigatorTreeNode>();
    NavigatorTreeNode navigatorTreeNode1 = source2.LastOrDefault<NavigatorTreeNode>();
    foreach (NavigatorTreeNode navigatorTreeNode2 in source2)
    {
      if (navigatorTreeNode2.Handle != null)
        navigatorTreeNode2.Handle.EnsureVisible();
      if (navigatorTreeNode2 != navigatorTreeNode1)
        navigatorTreeNode2.Expand();
    }
  }

  public void Focus()
  {
    if (this._tree == null)
      return;
    this._tree.FocusedNode = this;
  }

  public void Expand()
  {
    if (this._handle != null && (this._handle == null || this._handle.Expanded) || this.Cycle != NavigatorNodeCycle.None)
      return;
    this.EnsureVisible();
    if (this._handle == null)
      return;
    this._handle.Expand();
  }

  public void ExpandRecursive()
  {
    if (!this.Full)
      return;
    this.Expand();
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this.Children)
      child.ExpandRecursive();
  }

  public void FocusThenExpand()
  {
    this.Focus();
    this.Expand();
  }

  public void Select()
  {
    this.EnsureVisible();
    if (this.Handle == null)
      return;
    this.Handle.Selected = true;
  }

  /// <summary>
  /// Отыскать в коллекции описание узла дерева "Навигатора".
  /// Поиск также будет проходить в дочерних коллекциях.
  /// </summary>
  /// <param name="nodeID">Описание узла дерева "Навигатора"</param>
  /// <returns>null, если описание узла не найдено</returns>
  public NavigatorTreeNode FindNodeID(INodeID nodeID)
  {
    if (nodeID == null)
      return (NavigatorTreeNode) null;
    if (this.NodeID.Equals((object) nodeID))
      return this;
    for (int index = 0; index < this.Children.Count; ++index)
    {
      NavigatorTreeNode nodeId = this.Children[index].FindNodeID(nodeID);
      if (nodeId != null)
        return nodeId;
    }
    return (NavigatorTreeNode) null;
  }

  /// <summary>
  /// Установка значения CheckState узлу и его дочерним узлам.
  /// При необходимости меняются значения и у родительских узлов
  /// </summary>
  /// <param name="value">Значение</param>
  public virtual void SetCheckState(CheckState value)
  {
    if (this.HasChildren && this.Full)
      this._tree.FireBeforeSetCheckedPacket(this);
    try
    {
      this.SetCheckState(value, true);
    }
    finally
    {
      if (this.HasChildren && this.Full)
        this._tree.FireAfterSetCheckedPacket(this);
    }
  }

  /// <summary>
  /// Установка значения CheckState узлу и его дочерним узлам.
  /// При необходимости меняются значения и у родительских узлов
  /// </summary>
  /// <param name="value">Значение</param>
  /// <param name="updateParents">Распространить на родительские узлы</param>
  /// <param name="updateChildren">Распространить на дочерние узлы</param>
  /// <param name="callBeforeSetCheckState">Вызывать обработчик NavigatorTreeView.BeforeSetCheckState, если имеется</param>
  public virtual void SetCheckState(
    CheckState value,
    bool updateParents,
    bool updateChildren = true,
    bool callBeforeSetCheckState = true)
  {
    if (!this.ShowCheckState || this._checkState == value)
      return;
    CheckState checkState1 = this._checkState;
    CheckState checkState2 = value;
    bool updateParents1 = false;
    if (((this.Tree == null ? 0 : (this.Tree.BeforeSetCheckState != null ? 1 : 0)) & (callBeforeSetCheckState ? 1 : 0)) != 0)
    {
      this.Tree.BeforeSetCheckState(this, ref value);
      updateParents1 = checkState2 != value;
    }
    if (value == CheckState.Checked)
    {
      this._checkedCount = this.Children.Count;
      this._indeterminateCount = 0;
      if (this.Tree != null)
      {
        CheckState newValue = CheckState.Checked;
        this.Tree.RaiseCheckStateChanging(this, this._checkState, ref newValue);
      }
      this._checkState = CheckState.Checked;
      if (this.Tree != null)
        this.Tree.UpdateTreeNode(this);
    }
    else
    {
      this._checkedCount = 0;
      this._indeterminateCount = 0;
      if (this.Tree != null)
        this.Tree.RaiseCheckStateChanging(this, this._checkState, ref value);
      this._checkState = value;
      if (this.Tree != null)
        this.Tree.UpdateTreeNode(this);
    }
    if (this.Tree != null)
      this.Tree.RaiseCheckStateChanged(this);
    if (this._tree == null || this._tree.CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.ThreeState)
      return;
    if (updateParents)
      this.UpdateParents(this, checkState1, value);
    if (!updateChildren)
      return;
    for (int index = 0; index < this.Children.Count; ++index)
    {
      NavigatorTreeNode child = this.Children[index];
      child.State |= NavigatorTreeNode.UpdateState.UpdatedAsChild;
      try
      {
        child.SetCheckState(checkState2, updateParents1);
      }
      finally
      {
        child.State &= ~NavigatorTreeNode.UpdateState.UpdatedAsChild;
      }
    }
  }

  /// <summary>Обновить чек-боксы у родительских узлов</summary>
  /// <param name="node">Узел</param>
  /// <param name="newState">Новое значение</param>
  private void UpdateParents(NavigatorTreeNode node, CheckState oldState, CheckState newState)
  {
    if (node == null || node.Parent == null || !node.Parent.ShowCheckState)
      return;
    NavigatorTreeNode parent = node.Parent;
    CheckState checkState = parent.CheckState;
    switch (oldState)
    {
      case CheckState.Checked:
        --parent._checkedCount;
        break;
      case CheckState.Indeterminate:
        --parent._indeterminateCount;
        break;
    }
    switch (newState)
    {
      case CheckState.Checked:
        ++parent._checkedCount;
        break;
      case CheckState.Indeterminate:
        ++parent._indeterminateCount;
        break;
    }
    CheckState newValue = this.CalcState(parent);
    if (newValue == CheckState.Unchecked && node.Tree != null && node.Tree.AllowCheckParentWithoutChildren)
      newValue = CheckState.Indeterminate;
    this.UpdateParents(parent, parent.CheckState, newValue);
    if (parent.Tree != null)
      parent.Tree.RaiseCheckStateChanging(parent, parent._checkState, ref newValue);
    parent._checkState = newValue;
    if (parent.Tree == null)
      return;
    parent.Tree.UpdateTreeNode(parent);
    if (checkState == parent.CheckState)
      return;
    parent.Tree.RaiseCheckStateChanged(parent);
  }

  /// <summary>Рассчитать значение чек-бокса у узла</summary>
  /// <param name="node">Узел</param>
  /// <returns>Значение чек-бокса у узла</returns>
  private CheckState CalcState(NavigatorTreeNode node)
  {
    if (node._indeterminateCount > 0)
      return CheckState.Indeterminate;
    if (node._checkedCount == 0)
      return CheckState.Unchecked;
    return node._checkedCount != node.Children.Count ? CheckState.Indeterminate : CheckState.Checked;
  }

  /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return !(obj is NavigatorTreeNode navigatorTreeNode) || this._nodeID == null ? base.Equals(obj) : this._nodeID.Equals((object) navigatorTreeNode.NodeID);
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    return this._nodeID == null ? base.GetHashCode() : this._nodeID.GetHashCode();
  }

  /// <summary>
  /// Извлечь все узлы, включая узлы из дочерних коллекций, в линейный список
  /// </summary>
  /// <param name="nodes">Коллекция узлов, в которую добавляются узлы</param>
  /// <param name="onlyChecked">Если true, то извлекаются узлы, у которых свойство Checked не равно None</param>
  /// <returns>Все узлы, включая узлы из дочерних коллекций, в линейный список</returns>
  protected virtual void ExtractNodes(NavigatorTreeNodes nodes, bool onlyChecked)
  {
    if (nodes == null)
      return;
    if (!onlyChecked || this._checkState != CheckState.Unchecked)
      nodes.Add(this);
    for (int index = 0; index < this.Children.Count; ++index)
      this.Children[index].ExtractNodes(nodes, onlyChecked);
  }

  /// <summary>
  /// Извлечь все узлы, включая узлы из дочерних коллекций, в линейный список
  /// </summary>
  /// <param name="onlyChecked">Если true, то извлекаются узлы, у которых свойство Checked не равно None</param>
  /// <returns>Все узлы, включая узлы из дочерних коллекций, в линейный список</returns>
  public virtual NavigatorTreeNodes ExtractNodes(bool onlyChecked)
  {
    NavigatorTreeNodes nodes = new NavigatorTreeNodes();
    this.ExtractNodes(nodes, onlyChecked);
    return nodes;
  }

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public virtual object Clone()
  {
    NavigatorTreeNode navigatorTreeNode = new NavigatorTreeNode(this.Tree, this.Parent, this.NodeID, this.Values, this.RawValues, this.Handler, this.Flags, this.Bookmark, this.Full, this.ValidColumns);
    navigatorTreeNode.Cycle = this.Cycle;
    for (int index = 0; index < this.Children.Count; ++index)
      navigatorTreeNode.Children.Add(this.Children[index].Clone() as NavigatorTreeNode);
    return (object) navigatorTreeNode;
  }

  /// <summary>Есть ли в узле дочерние элементы</summary>
  public bool HasChildren
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._hasChildren;
    }
    set => this._hasChildren = value;
  }

  /// <summary>Раскрыт ли узел</summary>
  public bool Expanded
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._handle != null && this._handle.Expanded;
    }
    set
    {
      if (this._handle == null)
        return;
      lock (this._handle)
      {
        this._handle.UpdateChildren(true, false);
        this.Children.RebuildHandles();
        if (value)
        {
          if (this.Cycle != NavigatorNodeCycle.None)
            return;
          this.EnsureVisible();
          this._handle.Expand();
        }
        else
        {
          if (this._handle.ChildItems != null)
            this._handle.CollapseChildren(false);
          this._handle.Expanded = false;
        }
        if (this._parent == null)
          return;
        this._parent.Children.RebuildHandles();
      }
    }
  }

  public bool HasFocus
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Handle != null && this.Handle.HasFocus;
    }
  }

  /// <summary>Является ли узел видимым</summary>
  public bool Visible
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Handle != null && this.Handle.Item == this && this.Handle.Visible;
    }
  }

  /// <summary>Позиция узла в списке у его родительского узла</summary>
  public int Id
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._handle == null ? 0 : this._handle.ChildIndex;
    }
  }

  /// <summary>Индекс изображения в коллекции изображений</summary>
  public int ImageIndex
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._imageIndex;
    set => this._imageIndex = value;
  }

  /// <summary>
  /// Индекс изображения в коллекции изображений для выделенной строки
  /// </summary>
  public int SelectedImageIndex
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._selectedImageIndex;
    }
    set => this._selectedImageIndex = value;
  }

  /// <summary>Индекс статусного изображения для узла</summary>
  public int StateImageIndex
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._stateImageIndex;
    }
    set => this._stateImageIndex = value;
  }

  /// <summary>Статус узла</summary>
  public CheckState CheckState
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return !this.ShowCheckState ? CheckState.Unchecked : this._checkState;
    }
    set => this.SetCheckState(value);
  }

  /// <summary>Показывать ли чекбокс у данной конкретной ноды в том случае, если настройки дерева предполагают отображение чекбоксов</summary>
  public bool ShowCheckState
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._showCheckState;
    }
    [DebuggerStepThrough] set
    {
      if (this._showCheckState == value)
        return;
      this._showCheckState = value;
      if (!this._showCheckState || this.Parent == null || !this.Parent.ShowCheckState)
        return;
      switch (this.Parent.CheckState)
      {
        case CheckState.Unchecked:
          this.SetCheckState(CheckState.Unchecked, false, false, false);
          break;
        case CheckState.Checked:
          this.SetCheckState(CheckState.Checked, false, false, false);
          break;
      }
    }
  }

  /// <summary>
  /// Циклическая зависимость ("петля"), обнаруженная для данного узла
  /// </summary>
  public NavigatorNodeCycle Cycle
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._cycle;
    set => this._cycle = value;
  }

  public NavigatorTreeNodes Children { get; protected set; }

  /// <summary>Получить следующий узел</summary>
  /// <returns>Узел дерева навигатора</returns>
  public NavigatorTreeNode GetNextSibling()
  {
    if (this.Parent == null)
      return (NavigatorTreeNode) null;
    int index = 0;
    for (int count = this.Parent.Children.Count; index < count; ++index)
    {
      if (this == this.Parent.Children[index])
        return index < count - 1 ? this.Parent.Children[index + 1] : (NavigatorTreeNode) null;
    }
    return (NavigatorTreeNode) null;
  }

  /// <summary>Получить предыдущий узел</summary>
  /// <returns>Узел дерева навигатора</returns>
  public NavigatorTreeNode GetPreviousSibling()
  {
    if (this.Parent == null)
      return (NavigatorTreeNode) null;
    int index = 0;
    for (int count = this.Parent.Children.Count; index < count; ++index)
    {
      if (this == this.Parent.Children[index] && index > 0)
        return index > 0 ? this.Parent.Children[index - 1] : (NavigatorTreeNode) null;
    }
    return (NavigatorTreeNode) null;
  }

  public NavigatorTreeNode GetPreviousSiblingOrParent() => this.GetPreviousSibling() ?? this.Parent;

  /// <summary>Получить всех потомков</summary>
  /// <param name="fetch">Заполнять дочерние узлы</param>
  /// <returns>Узлы дерева навигатора</returns>
  public IEnumerable<NavigatorTreeNode> GetDescendants(
    bool fetch = false,
    Predicate<NavigatorTreeNode> filter = null)
  {
    if (fetch)
      this.Fetch();
    foreach (NavigatorTreeNode childNode in (List<NavigatorTreeNode>) this.Children)
    {
      if (filter == null || filter != null && filter(childNode))
      {
        yield return childNode;
        foreach (NavigatorTreeNode descendant in childNode.GetDescendants(fetch, filter))
          yield return descendant;
      }
    }
  }

  public IEnumerable<NavigatorTreeNode> GetDescendantsAndSelf(bool fetch = false)
  {
    yield return this;
    foreach (NavigatorTreeNode descendant in this.GetDescendants(fetch))
      yield return descendant;
  }

  public IEnumerable<NavigatorTreeNode> GetAncestors()
  {
    for (NavigatorTreeNode parent = this.Parent; parent != null; parent = parent.Parent)
      yield return parent;
  }

  public IEnumerable<NavigatorTreeNode> GetAncestorsAndSelf()
  {
    yield return this;
    foreach (NavigatorTreeNode ancestor in this.GetAncestors())
      yield return ancestor;
  }

  public NavigatorTreeNode GetNextSiblingOrAncestorNextSibling()
  {
    for (NavigatorTreeNode navigatorTreeNode = this; navigatorTreeNode != null; navigatorTreeNode = navigatorTreeNode.Parent)
    {
      NavigatorTreeNode nextSibling = navigatorTreeNode.GetNextSibling();
      if (nextSibling != null)
        return nextSibling;
    }
    return (NavigatorTreeNode) null;
  }

  public IEnumerable<NavigatorTreeNode> GetAllNext(bool fetch = false, Predicate<NavigatorTreeNode> filter = null)
  {
    if (filter == null || filter(this))
    {
      foreach (NavigatorTreeNode descendant in this.GetDescendants(fetch, filter))
        yield return descendant;
    }
    NavigatorTreeNode ancestorNextSibling = this.GetNextSiblingOrAncestorNextSibling();
    if (ancestorNextSibling != null)
    {
      foreach (NavigatorTreeNode navigatorTreeNode in ancestorNextSibling.GetAllNextAndSelf(fetch, filter))
        yield return navigatorTreeNode;
    }
  }

  public IEnumerable<NavigatorTreeNode> GetAllNextAndSelf(
    bool fetch = false,
    Predicate<NavigatorTreeNode> filter = null)
  {
    if (filter == null || filter(this))
      yield return this;
    foreach (NavigatorTreeNode navigatorTreeNode in this.GetAllNext(fetch, filter))
      yield return navigatorTreeNode;
  }

  public IEnumerable<NavigatorTreeNode> GetAllNextThenAllPrevious(
    bool fetch = false,
    Predicate<NavigatorTreeNode> filter = null)
  {
    foreach (NavigatorTreeNode nextThenAllPreviou in this.GetAllNext(fetch, filter))
      yield return nextThenAllPreviou;
    foreach (NavigatorTreeNode allPreviou in this.GetAllPrevious(fetch, filter))
      yield return allPreviou;
  }

  public IEnumerable<NavigatorTreeNode> GetAllNextThenAllPreviousThenSelf(
    bool fetch = false,
    Predicate<NavigatorTreeNode> filter = null)
  {
    foreach (NavigatorTreeNode nextThenAllPreviou in this.GetAllNextThenAllPrevious(fetch, filter))
      yield return nextThenAllPreviou;
    yield return this;
  }

  public IEnumerable<NavigatorTreeNode> GetAllPrevious(
    bool fetch = false,
    Predicate<NavigatorTreeNode> filter = null)
  {
    NavigatorTreeNode root = this.GetRoot();
    if (this != root)
    {
      foreach (NavigatorTreeNode allPreviou in root.GetAllNext(fetch, filter))
      {
        if (allPreviou != this)
          yield return allPreviou;
        else
          break;
      }
    }
  }

  public NavigatorTreeNode GetRoot()
  {
    return this.InTree ? this._tree.RootNode : this.GetAncestors().LastOrDefault<NavigatorTreeNode>();
  }

  public bool ContainsText(string text, bool matchCase)
  {
    if (string.IsNullOrEmpty(text))
      throw new ArgumentNullException(nameof (text));
    if (this.Tree != null)
    {
      for (int columnIndex = 0; columnIndex < this.Tree.Columns.Count; ++columnIndex)
      {
        string str = this.GetDisplayText(columnIndex);
        if (!string.IsNullOrEmpty(str))
        {
          if (!matchCase)
          {
            str = str.ToLower();
            text = text.ToLower();
          }
          if (str.IndexOf(text) >= 0)
            return true;
        }
      }
    }
    return false;
  }

  public bool IsMatch(Regex regex)
  {
    if (regex == null)
      throw new ArgumentNullException(nameof (regex));
    if (this.Tree != null)
    {
      for (int columnIndex = 0; columnIndex < this.Tree.Columns.Count; ++columnIndex)
      {
        string displayText = this.GetDisplayText(columnIndex);
        if (!string.IsNullOrEmpty(displayText) && regex.IsMatch(displayText))
          return true;
      }
    }
    return false;
  }

  public INodeID[] GetPath()
  {
    List<INodeID> nodeIdList = new List<INodeID>();
    foreach (NavigatorTreeNode navigatorTreeNode in this.GetAncestorsAndSelf().Reverse<NavigatorTreeNode>())
    {
      if (navigatorTreeNode.NodeID != null)
        nodeIdList.Add(navigatorTreeNode.NodeID);
    }
    return nodeIdList.ToArray();
  }

  /// <summary>
  /// Коллекция дочерних узлов как коллекция выделенных элементов
  /// </summary>
  public ISelectedItems NodesAsSelectedItems
  {
    get
    {
      return !this.InTree || this.Tree == null || this.Children == null || this.Children.Count == 0 ? (ISelectedItems) null : this.GetNodesAsSelectedItems(this.Children);
    }
  }

  /// <summary>
  /// Сформировать из указанных узлов дерева Навигатора коллекцию выделенных элементов
  /// </summary>
  /// <param name="nodes">Список узлов (допускается использовать разноуровневые узлы)</param>
  /// <returns></returns>
  private ISelectedItems GetNodesAsSelectedItems(NavigatorTreeNodes nodes)
  {
    if (nodes == null || nodes.Count == 0)
      return (ISelectedItems) null;
    return nodes.Count != 1 ? (ISelectedItems) new NavigatorTreeViewSelectedItems(this.Tree, nodes.ToArray()) : (ISelectedItems) new NavigatorTreeViewSelectedItem(this.Tree, nodes[0]);
  }

  /// <summary>Сам узел в виде коллекции выделенных элементов</summary>
  public ISelectedItems NodeAsSelectedItem
  {
    get
    {
      if (!this.InTree || this.Tree == null)
        return (ISelectedItems) null;
      return this.GetNodesAsSelectedItems(new NavigatorTreeNodes(this.Tree, this.Parent)
      {
        this
      });
    }
  }

  /// <summary>Пристыкован ли узел к дереву</summary>
  public bool InTree
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._handle != null && this._handle.Item != null;
    }
  }

  /// <summary>Тег (уже не является ссылкой на TreeNodeData !!!)</summary>
  public object Tag
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._tag;
    set => this._tag = value;
  }

  /// <summary>Уровень узла</summary>
  public int Level
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._handle == null ? 0 : this._handle.Level;
    }
  }

  /// <summary>Вернуть значение ячейки</summary>
  /// <param name="columnIndex">Индекс ячейки</param>
  /// <returns>Значение ячейки</returns>
  public object GetCellValue(int columnIndex)
  {
    if (this._values == null)
      this._values = new object[this._tree.Columns.Count];
    if (columnIndex >= this._values.Length)
      Array.Resize<object>(ref this._values, columnIndex + 1);
    return this._values[columnIndex];
  }

  /// <summary>Вернуть оригинальное значение ячейки</summary>
  /// <param name="columnIndex">Индекс ячейки</param>
  /// <returns>Оригинальное значение ячейки</returns>
  public object GetRawCellValue(int columnIndex)
  {
    if (this._rawValues == null)
      this._rawValues = new object[this._tree.Columns.Count];
    if (columnIndex >= this._values.Length)
      Array.Resize<object>(ref this._rawValues, columnIndex + 1);
    return this._rawValues[columnIndex];
  }

  /// <summary>Установить значение ячейки</summary>
  /// <param name="columnIndex">Индекс ячейки</param>
  /// <param name="value">Значение ячейки</param>
  public void SetCellValue(int columnIndex, object value)
  {
    if (this._values == null)
      this._values = new object[this._tree.Columns.Count];
    if (columnIndex >= this._values.Length)
      Array.Resize<object>(ref this._values, columnIndex + 1);
    this._values[columnIndex] = value;
  }

  /// <summary>Установить оригинальное значение ячейки</summary>
  /// <param name="columnIndex">Индекс ячейки</param>
  /// <param name="value">Оригинальное значение ячейки</param>
  public void SetRawCellValue(int columnIndex, object value)
  {
    if (this._rawValues == null)
      this._rawValues = new object[this._tree.Columns.Count];
    if (columnIndex >= this._rawValues.Length)
      Array.Resize<object>(ref this._rawValues, columnIndex + 1);
    if (value is byte[])
      return;
    this._rawValues[columnIndex] = value;
  }

  /// <summary>Вернуть отображаемый текст для ячейки</summary>
  /// <param name="columnIndex">Индекс ячейки</param>
  /// <returns>Отображаемый текст для ячейки</returns>
  public string GetDisplayText(int columnIndex) => Convert.ToString(this.GetCellValue(columnIndex));

  /// <summary>Вызвать переданную функцию для всех дочерних нод</summary>
  /// <param name="predicate">Функция обработки нод</param>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeWithChilds(
    [NotNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    this.EnumerationWithChilds((System.Func<NavigatorTreeNode, bool>) null, recursive, autoPopulateNodes).InvokeForAll<NavigatorTreeNode>(predicate);
  }

  /// <summary>Вызвать переданную функцию для тех дочерних нод, которые удовлетворяют переданному условию</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="predicate">Функция обработки нод</param>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeWithChilds(
    [CanBeNull, InstantHandle] System.Func<NavigatorTreeNode, bool> condition,
    [NotNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    this.EnumerationWithChilds(condition, recursive, autoPopulateNodes).InvokeForAll<NavigatorTreeNode>(predicate);
  }

  /// <summary>Вызвать переданную функцию для всех дочерних нод</summary>
  /// <param name="predicate">Функция обработки нод. Если вернёт false, то обработка прекращается</param>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeWithChilds(
    [NotNull, InstantHandle] System.Func<NavigatorTreeNode, bool> predicate,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    this.EnumerationWithChilds((System.Func<NavigatorTreeNode, bool>) null, recursive, autoPopulateNodes).InvokeWhile<NavigatorTreeNode>(predicate);
  }

  /// <summary>Вызвать переданную функцию для тех дочерних нод, которые удовлетворяют переданному условию</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="predicate">Функция обработки нод. Если вернёт false, то обработка прекращается</param>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeWithChilds(
    [CanBeNull, InstantHandle] System.Func<NavigatorTreeNode, bool> condition,
    [NotNull, InstantHandle] System.Func<NavigatorTreeNode, bool> predicate,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    this.EnumerationWithChilds(condition, recursive, autoPopulateNodes).InvokeWhile<NavigatorTreeNode>(predicate);
  }

  /// <summary>Вызвать переданную функцию для тех дочерних нод, которые удовлетворяют переданному условию</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="invokeForChilds">Перебирать ли дочерние ноды данной</param>
  /// <param name="predicate">Метод обработки нод</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeWithChilds(
    [CanBeNull, InstantHandle] System.Func<NavigatorTreeNode, bool> condition,
    [NotNull, InstantHandle] System.Func<NavigatorTreeNode, bool> invokeForChilds,
    [NotNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    bool autoPopulateNodes = false)
  {
    this.EnumerationWithChilds(condition, invokeForChilds, autoPopulateNodes).InvokeForAll<NavigatorTreeNode>(predicate);
  }

  /// <summary>Поиск первой ноды в дереве, соответствующей заданному условию</summary>
  /// <param name="condition">Условие, которому должна соответствовать разыскиваемая нода дерева</param>
  /// <param name="findInChilds">Перебирать ли дочерние ноды данной. Если null, то безусловно перебираются все дочерние ноды</param>
  /// <param name="autoPopulateNodes">Загружать ли автоматически состав незагруженных нод если выше по иерархии не найдено ни одной ноды,
  /// удовлетворяющей переданному условию</param>
  /// <returns>Первая найденная нода, соответствующая переданному условию, или null, если ни одна нода условию не соответствует</returns>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public NavigatorTreeNode FindFirstNode(
    [NotNull, InstantHandle] System.Func<NavigatorTreeNode, bool> condition,
    [CanBeNull, InstantHandle] System.Func<NavigatorTreeNode, bool> findInChilds = null,
    bool autoPopulateNodes = false)
  {
    if (findInChilds == null)
      findInChilds = (System.Func<NavigatorTreeNode, bool>) (treeNode => true);
    return this.EnumerationWithChilds((System.Func<NavigatorTreeNode, bool>) null, findInChilds, autoPopulateNodes).FirstOrDefault<NavigatorTreeNode>(condition);
  }

  /// <summary>Вызвать переданную функцию для тех дочерних нод, которые удовлетворяют переданному условию</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="invokeForChilds">Перебирать ли дочерние ноды данной</param>
  /// <param name="predicate">Метод обработки нод</param>
  /// <param name="afterChildsProcessed">Метод обработки ноды после того, как обработаны все дочерние ноды</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  public void InvokeWithChilds(
    [CanBeNull, InstantHandle] System.Func<NavigatorTreeNode, bool> condition,
    [CanBeNull, InstantHandle] System.Func<NavigatorTreeNode, bool> invokeForChilds,
    [CanBeNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    [CanBeNull, InstantHandle] Action<NavigatorTreeNode> afterChildsProcessed,
    bool autoPopulateNodes = false)
  {
    bool flag = this.NodeID != null && (predicate != null || afterChildsProcessed != null) && (condition == null || condition(this));
    if (flag && predicate != null)
      predicate(this);
    if (this.HasChildren && this.Children != null && this.Children.Count > 0 && (this.Full || autoPopulateNodes && this.PopulateAndWaitForFull()) && (invokeForChilds != null ? (invokeForChilds(this) ? 1 : 0) : 1) != 0)
    {
      foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this.Children)
        child.InvokeWithChilds(condition, invokeForChilds, predicate, afterChildsProcessed, autoPopulateNodes);
    }
    if (!flag || afterChildsProcessed == null)
      return;
    afterChildsProcessed(this);
  }

  /// <summary>Вызвать переданную функцию для всех дочерних нод</summary>
  /// <param name="predicate">Функция обработки нод</param>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeForChilds(
    [NotNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    this.ChildsEnumeration((System.Func<NavigatorTreeNode, bool>) null, recursive, autoPopulateNodes).InvokeForAll<NavigatorTreeNode>(predicate);
  }

  /// <summary>Вызвать переданную функцию для тех дочерних нод, которые удовлетворяют переданному условию</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="predicate">Функция обработки нод</param>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InvokeForChilds(
    [CanBeNull, InstantHandle] System.Func<NavigatorTreeNode, bool> condition,
    [NotNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    this.ChildsEnumeration(condition, recursive, autoPopulateNodes).InvokeForAll<NavigatorTreeNode>(predicate);
  }

  /// <summary>Последовательность всех дочерних нод, рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerable<NavigatorTreeNode> EnumerationWithChilds(
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    return this.EnumerationWithChilds((System.Func<NavigatorTreeNode, bool>) null, recursive, autoPopulateNodes);
  }

  /// <summary>Последовательность всех дочерних нод, удовлетворяющих некому условию (которое необязательно указывать), рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerable<NavigatorTreeNode> EnumerationWithChilds(
    [CanBeNull] System.Func<NavigatorTreeNode, bool> condition,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    return this.EnumerationWithChilds(condition, (System.Func<NavigatorTreeNode, bool>) (node => recursive), autoPopulateNodes);
  }

  /// <summary>Последовательность всех дочерних нод, удовлетворяющих некому условию (которое необязательно указывать), рекурсивная или нет,
  /// с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="invokeForChilds">Перебирать ли дочерние ноды данной</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerable<NavigatorTreeNode> EnumerationWithChilds(
    [CanBeNull] System.Func<NavigatorTreeNode, bool> condition,
    [NotNull] System.Func<NavigatorTreeNode, bool> invokeForChilds,
    bool autoPopulateNodes = false)
  {
    System.Func<NavigatorTreeNode, bool> func = condition;
    if ((func != null ? (func(this) ? 1 : 0) : 1) != 0)
      yield return this;
    if (this.HasChildren && invokeForChilds(this) && (this.Full || autoPopulateNodes && this.PopulateAndWaitForFull()))
    {
      foreach (NavigatorTreeNode navigatorTreeNode in this.ChildsEnumeration(condition, invokeForChilds, autoPopulateNodes))
        yield return navigatorTreeNode;
    }
  }

  /// <summary>Последовательность всех дочерних нод, рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerable<NavigatorTreeNode> ChildsEnumeration(bool recursive = true, bool autoPopulateNodes = false)
  {
    return this.ChildsEnumeration((System.Func<NavigatorTreeNode, bool>) null, recursive, autoPopulateNodes);
  }

  /// <summary>Последовательность всех дочерних нод, удовлетворяющих некому условию (которое необязательно указывать), рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerable<NavigatorTreeNode> ChildsEnumeration(
    [CanBeNull] System.Func<NavigatorTreeNode, bool> condition,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    return this.ChildsEnumeration(condition, (System.Func<NavigatorTreeNode, bool>) (node => recursive), autoPopulateNodes);
  }

  /// <summary>Последовательность всех дочерних нод, удовлетворяющих некому условию (которое необязательно указывать), рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="invokeForChilds">Перебирать ли дочерние ноды данной</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerable<NavigatorTreeNode> ChildsEnumeration(
    [CanBeNull] System.Func<NavigatorTreeNode, bool> condition,
    [NotNull] System.Func<NavigatorTreeNode, bool> invokeForChilds,
    bool autoPopulateNodes = false)
  {
    if (this.Full && this.Children != null && this.Children.Count > 0)
    {
      foreach (NavigatorTreeNode treeNode in (List<NavigatorTreeNode>) this.Children)
      {
        System.Func<NavigatorTreeNode, bool> func = condition;
        if ((func != null ? (func(treeNode) ? 1 : 0) : 1) != 0)
          yield return treeNode;
        if (treeNode.HasChildren && invokeForChilds(treeNode) && (treeNode.Full || autoPopulateNodes && treeNode.PopulateAndWaitForFull()))
        {
          foreach (NavigatorTreeNode navigatorTreeNode in treeNode.ChildsEnumeration(condition, invokeForChilds, autoPopulateNodes))
            yield return navigatorTreeNode;
        }
      }
    }
  }

  /// <summary>Загрузить процесс загрузки дочерних нод и дождаться его окончания</summary>
  /// <param name="millisecondsTimeout">Таймаут ожидания в миллисекундах. -1 соответствует бесконечному ожиданию</param>
  /// <returns>True если загрузка прошла успешно или дочерних нод у переданной нет, False если таймаут</returns>
  public bool PopulateAndWaitForFull(int millisecondsTimeout = 20000)
  {
    if (this.Tree == null)
      throw new NoNullAllowedException("Tree");
    if (!this.HasChildren || this.Full)
      return true;
    this.Tree.PopulateNode(this);
    return SpinWait.SpinUntil((Func<bool>) (() => this.Full), millisecondsTimeout);
  }

  [System.Flags]
  public enum UpdateState
  {
    None = 0,
    UpdatedAsChild = 1,
  }

  /// <summary>Итератор по узлам дерева навигатора</summary>
  public sealed class NavigatorTreeNodeEnumerator : 
    IEnumerator<NavigatorTreeNode>,
    IDisposable,
    IEnumerator
  {
    private volatile NavigatorTreeNode _current;
    private Stack<NavigatorTreeNode> _parents = new Stack<NavigatorTreeNode>();
    private volatile bool _loadChidren;
    private bool _isEnd;

    /// <summary>Конструктор</summary>
    /// <param name="node">Узел дерева навигатора</param>
    public NavigatorTreeNodeEnumerator(NavigatorTreeNode node)
    {
      this.Node = node != null ? node : throw new ArgumentNullException(nameof (node));
      this.Reset();
    }

    /// <summary>Узел дерева навигатора, с которого начался обход</summary>
    public NavigatorTreeNode Node { get; private set; }

    /// <summary>Подгружать содержимое дочерних узлов</summary>
    public bool LoadChildren
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._loadChidren;
      set => this._loadChidren = value;
    }

    public NavigatorTreeNode Current
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._current;
    }

    public void Dispose() => throw new NotImplementedException();

    object IEnumerator.Current
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (object) this.Current;
    }

    public bool MoveNext()
    {
      if (this._isEnd)
        return false;
      if (this.LoadChildren && this._current.InTree)
        this._current.Tree.PopulateNode(this._current, true);
      if (this._current.Children.Count > 0)
      {
        this._parents.Push(this._current);
        this._current = this._current.Children[0];
        return true;
      }
      NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
      while (this._parents.Count != 0)
      {
        navigatorTreeNode = this._current.GetNextSibling();
        if (navigatorTreeNode != null)
        {
          this._current = navigatorTreeNode;
          break;
        }
        this._current = this._parents.Pop();
      }
      this._isEnd = navigatorTreeNode == null;
      return navigatorTreeNode != null;
    }

    public void Reset() => this._current = this.Node;
  }
}
