// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.Navigator.TechCardNavTreeViewControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls.Navigator;

/// <summary>TechCard custom navigator tree view</summary>
public class TechCardNavTreeViewControl : NavigatorTreeView
{
  /// <summary>Tree view checkout mode</summary>
  private TechCheckoutMode _checkoutMode;
  /// <summary>Check root node flag</summary>
  private bool _checkRootNode;
  /// <summary>Custom context menu</summary>
  private ContextMenuBarItem _contextMenuBarItem;

  /// <summary>Initialize control data</summary>
  private void InitializeData()
  {
    this._disableTreeEvents = true;
    this.DisableIMContextMenu = true;
    this.DisableKeyUpEvents = true;
  }

  /// <summary>Show custom menu handler</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected virtual void DoShowContextMenu(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right || e.Y < this.HeaderHeight)
      return;
    if (this._contextMenuBarItem != null && !this._contextMenuBarItem.Visible)
    {
      this._contextMenuBarItem.Show((Control) this, e.Location);
    }
    else
    {
      ContextMenuStrip contextMenuStrip = base.ContextMenuStrip;
      if (contextMenuStrip == null || contextMenuStrip.Visible)
        return;
      contextMenuStrip.Show((Control) this, e.Location);
    }
  }

  /// <summary>Constructor</summary>
  public TechCardNavTreeViewControl() => this.InitializeData();

  /// <summary>Constructor</summary>
  /// <param name="services"></param>
  public TechCardNavTreeViewControl(System.IServiceProvider services)
    : base(services)
  {
  }

  /// <summary>Constructor</summary>
  /// <param name="services"></param>
  /// <param name="columns"></param>
  public TechCardNavTreeViewControl(System.IServiceProvider services, NodeColumnCollection columns)
    : base(services, columns)
  {
  }

  /// <summary>Инициализировать ресурсы дерева</summary>
  protected override void InitTreeResources()
  {
    base.InitTreeResources();
    this._rootNode = (NavigatorTreeNode) new TechcardNavTreeNode((NavigatorTreeView) this, (NavigatorTreeNode) null, (INodeID) null);
  }

  /// <summary>
  /// Добавить в коллекцию колонок дерева очередную колонку "Навигатора"
  /// </summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <param name="columns">Вся коллекция колонок</param>
  /// <returns>Вновь добавленная или найденная колонка из дерева</returns>
  protected override NavigatorTreeColumn AddColumn(NodeColumn column, NodeColumnCollection columns)
  {
    return this.GetColumn(column) ?? (NavigatorTreeColumn) new TechCardNavTreeColumn((NavigatorTreeView) this, column, columns);
  }

  /// <summary>Создать строку в дереве "Навигатора"</summary>
  /// <param name="panelWidget">Панель</param>
  /// <param name="row">Строка</param>
  /// <returns>Новая строка</returns>
  protected override RowWidget CreateRowWidget(PanelWidget panelWidget, Row row)
  {
    return (RowWidget) new TechcardNavRowWidget(panelWidget, row);
  }

  /// <summary>Создать ячейку в строке дерева "Навигатора"</summary>
  /// <param name="rowWidget">Строка</param>
  /// <param name="column">Колонка</param>
  /// <returns>Новая ячейка</returns>
  protected override CellWidget CreateCellWidget(RowWidget rowWidget, Column column)
  {
    return column is NavigatorTreeColumn navigatorTreeColumn && navigatorTreeColumn.NavigatorColumn.ID.Equals((object) "F_STATUSES") && (navigatorTreeColumn.NavigatorColumn.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid || navigatorTreeColumn.NavigatorColumn.SchemeGuid == Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid) ? (CellWidget) new TechCardStatusesCellWidget(rowWidget, column) : (CellWidget) new TechCardNavCellWidget(rowWidget, column);
  }

  /// <summary>Добавить узел в состав указанного родительского узла</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="fieldValues">Поля, на основании которых строится новый узел</param>
  /// <param name="rawFieldValues">Поля, на основании которых строится новый узел (исходные значения)</param>
  /// <param name="nodeId">Идентификатор узла </param>
  /// <returns>Вновь добавленный узел</returns>
  public override NavigatorTreeNode AppendNode(
    NavigatorTreeNode parent,
    object[] fieldValues,
    object[] rawFieldValues,
    INodeID nodeId)
  {
    if (parent == null)
      parent = this.RootNode;
    return (NavigatorTreeNode) new TechcardNavTreeNode((NavigatorTreeView) this, parent, (INodeID) null, fieldValues, rawFieldValues);
  }

  /// <summary>
  /// Выполняет полную пересортировку дерева навигации.
  /// <remarks>Override base method to save check states </remarks>
  /// </summary>
  protected override void SortTree()
  {
    IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper> checkedNodesStates1 = this.CheckedNodesStates;
    ++this._lockClearTreeEvent;
    ++this._lockFocusedItemEvent;
    ++this._lockSelectionChanged;
    try
    {
      base.SortTree();
    }
    finally
    {
      IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper> checkedNodesStates2 = this.CheckedNodesStates;
      if (checkedNodesStates2 == null || checkedNodesStates2.Count == 0)
        this.CheckedNodesStates = checkedNodesStates1;
      --this._lockSelectionChanged;
      --this._lockFocusedItemEvent;
      --this._lockClearTreeEvent;
    }
    this.RootNode.Expanded = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void NavigatorTreeView_KeyUp(object sender, KeyEventArgs e)
  {
    base.NavigatorTreeView_KeyUp(sender, e);
    KeyEventArgs eventArgs = e;
    if (eventArgs == null || eventArgs.Handled || this._contextMenuBarItem == null)
      return;
    Keys pressed = eventArgs.KeyCode;
    if (eventArgs.Control)
      pressed |= Keys.Control;
    if (eventArgs.Shift)
      pressed |= Keys.Shift;
    if (eventArgs.Alt)
      pressed |= Keys.Alt;
    Action<MenuItemBase.MenuItemCollection> checkShortCut = (Action<MenuItemBase.MenuItemCollection>) null;
    checkShortCut = (Action<MenuItemBase.MenuItemCollection>) (items =>
    {
      if (items == null)
        return;
      foreach (MenuButtonItem menuButtonItem in (CollectionBase) items)
      {
        if (menuButtonItem != null)
        {
          if (menuButtonItem.PrimaryShortcut == pressed)
          {
            menuButtonItem.PerformClick();
            eventArgs.SuppressKeyPress = true;
            break;
          }
          checkShortCut(menuButtonItem.Items);
        }
      }
    });
    checkShortCut(this._contextMenuBarItem.Items);
  }

  /// <summary>
  /// При необходимости генерировать событие о том, что происходит изменение статуса у узла
  /// </summary>
  /// <remarks>We have to declare this method to make method RaiseCheckStateChanging internal visible </remarks>
  /// <param name="node">Узел</param>
  /// <param name="oldValue">Старое значение</param>
  /// <param name="newValue">Новое значение</param>
  internal virtual void DoRaiseCheckStateChanging(
    NavigatorTreeNode node,
    CheckState oldValue,
    ref CheckState newValue)
  {
    this.RaiseCheckStateChanging(node, oldValue, ref newValue);
  }

  /// <summary>
  /// При необходимости генерировать событие о том, что произошло изменение статуса у узла
  /// </summary>
  /// <remarks>We have to declare this method to make method RaiseCheckStateChanged internal visible </remarks>
  /// <param name="node">Узел</param>
  internal virtual void DoRaiseCheckStateChanged(NavigatorTreeNode node)
  {
    this.RaiseCheckStateChanged(node);
  }

  /// <summary>
  /// Set directly check state, without calling events and updating tree
  /// </summary>
  internal virtual void SetCheckBoxesStyleInternal(NavigatorTreeViewCheckBoxStyle checkBoxStyle)
  {
    this._checkBoxesStyle = checkBoxStyle;
  }

  /// <summary>Set tree view columns</summary>
  /// <param name="columns">Columns collection</param>
  /// <param name="descriptor">Root descriptor (to correct set supported columns)</param>
  public virtual void SetColumns(NodeColumnCollection columns, IDescriptor descriptor)
  {
    this.RootDescriptor = descriptor;
    this.SetColumns(columns);
  }

  /// <summary>Очистить внутренние структуры дерева</summary>
  /// <param name="preserveEthereal">Сохранить описание корневого узла</param>
  public virtual void ClearTreeCore(bool preserveEthereal) => this.ClearCore(preserveEthereal);

  /// <summary>Checkout mode</summary>
  [Browsable(true)]
  [DisplayName("Checkout Mode")]
  public TechCheckoutMode CheckoutMode
  {
    get => this._checkoutMode;
    set => this._checkoutMode = value;
  }

  /// <summary>Check root node flag</summary>
  [Browsable(true)]
  [DisplayName("CheckRootNode")]
  public bool CheckRootNode
  {
    get => this._checkRootNode;
    set
    {
      if (this._checkRootNode == value)
        return;
      this._checkRootNode = value;
      if (this.RootNode == null)
        return;
      this.UpdateTreeNode(this.RootNode);
    }
  }

  /// <summary>Custom context menu strip</summary>
  [Browsable(true)]
  [DisplayName("ContextMenuStrip")]
  [Obsolete("Use ContextMenuBarItem instead")]
  public override ContextMenuStrip ContextMenuStrip
  {
    get => base.ContextMenuStrip;
    set
    {
      this.ShowContextMenu -= new MouseEventHandler(this.DoShowContextMenu);
      base.ContextMenuStrip = value;
      if (value == null)
        return;
      this.ShowContextMenu += new MouseEventHandler(this.DoShowContextMenu);
    }
  }

  /// <summary>Custom context menu strip</summary>
  [Browsable(true)]
  [DisplayName("ContextMenuBarItem")]
  public ContextMenuBarItem ContextMenuBarItem
  {
    get => this._contextMenuBarItem;
    set
    {
      this.ShowContextMenu -= new MouseEventHandler(this.DoShowContextMenu);
      this._contextMenuBarItem = value;
      if (value == null)
        return;
      this.ShowContextMenu += new MouseEventHandler(this.DoShowContextMenu);
    }
  }

  /// <summary>
  /// Коллекция поддерживаемых колонок в дереве "Навигатора"
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override NodeColumnCollection SupportedColumns
  {
    get
    {
      if (this._supportedColumns == null)
      {
        if (this._onGetSupportedColumnsEventHandler != null)
        {
          GetSupportedColumnsEventHandler columnsEventHandler = this._onGetSupportedColumnsEventHandler;
          this._supportedColumns = columnsEventHandler != null ? columnsEventHandler((object) this) : (NodeColumnCollection) null;
        }
        else
          this._supportedColumns = this.RootDescriptor is ISupportedColumns rootDescriptor ? rootDescriptor.GetSupportedColumns() : Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending);
      }
      return this._supportedColumns;
    }
  }

  /// <summary>
  /// Список статусов отмеченных узлов
  /// <remarks></remarks>
  /// </summary>
  [Browsable(false)]
  public virtual IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper> CheckedNodesStates
  {
    get
    {
      NavigatorTreeNode[] checkedNodes = this.CheckedNodes;
      Dictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper> checkedNodesStates = new Dictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>();
      foreach (NavigatorTreeNode node in checkedNodes)
      {
        if (node.CheckState != CheckState.Unchecked)
        {
          NodeIDPath nodeIdPath = this.GetNodeIDPath(node);
          if (nodeIdPath != null)
            checkedNodesStates.Add(nodeIdPath, new TechcardNavTreeNode.NodeStateKeeper(node));
        }
      }
      return (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) checkedNodesStates;
    }
    set
    {
      if (value == null || value.Count == 0)
        return;
      ++this._lockFocusedItemEvent;
      ++this._lockSelectionChanged;
      NavigatorTreeNode node1;
      try
      {
        NodeIDPath focusedPath = this.FocusedPath;
        NavigatorTreeNode node2 = (NavigatorTreeNode) null;
        foreach (KeyValuePair<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper> keyValuePair in (IEnumerable<KeyValuePair<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>>) value)
        {
          if (keyValuePair.Key != null)
          {
            if (this.TryBrowse(keyValuePair.Key))
              node2 = this.FocusedNode;
            if (node2 is TechcardNavTreeNode node3)
              keyValuePair.Value.RestoreState((NavigatorTreeNode) node3);
          }
        }
        node1 = focusedPath == null || !this.TryBrowse(focusedPath) ? this.RootNode : this.FocusedNode;
        if (node2 != null)
          this.UpdateTreeNode(node2);
        else if (node1 != null)
          this.UpdateTreeNode(node1);
      }
      finally
      {
        --this._lockSelectionChanged;
        --this._lockFocusedItemEvent;
      }
      node1?.FocusThenExpand();
      this.RaiseSelectedItemsChanged();
    }
  }
}
