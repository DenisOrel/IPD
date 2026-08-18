
// Type: Intermech.Navigator.Controls.ChildrenViewSelectedItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Реализует коллекцию выделенных в гриде элементов навигации.
/// </summary>
public sealed class ChildrenViewSelectedItems : ISelectedItems, ISimpleSelectedItems, ICloneable
{
  /// <summary>Коллекция элементов навигации</summary>
  private NodeItems _nodeItems;
  /// <summary>Владелец</summary>
  private ChildrenView _childrenView;
  /// <summary>Список корректен</summary>
  private bool _valid;
  /// <summary>Путь обработчика</summary>
  private NodeIDPath _nodeIDPath;
  /// <summary>Обработчик</summary>
  private INode _node;

  /// <summary>
  /// Вернуть текущий набор выделенных записей из ChildrenView
  /// </summary>
  /// <param name="owner">Вьюшка</param>
  /// <returns>Текущий набор выделенных записей</returns>
  public static ISelectedItems GetSelectedItems(ChildrenView owner)
  {
    NodeIDPath handlerPath = owner._parentPath;
    if ((owner.Options & ChildrenViewOptions.DisablePathProcessing) != (ChildrenViewOptions) 0)
    {
      IDescriptor emptyPathDescriptor = owner.GetEmptyPathDescriptor();
      INodeQuery nodeQuery = emptyPathDescriptor != null ? new EtherealNode(emptyPathDescriptor).GetQuery(ContentType.Folders) : throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_3902.ssp_imclient_3903()));
      nodeQuery.Execute((object) null, 1);
      handlerPath = new NodeIDPath(emptyPathDescriptor);
      handlerPath.Add(nodeQuery.GetRecordNodeID(0));
    }
    NodeItems selectedItems = new NodeItems(handlerPath, owner._parentNode, new NodeIDCollection(), (IServiceProvider) owner.Services);
    NodeIDCollection nodeIds = selectedItems.NodeIDs;
    try
    {
      int count = owner._grid.Rows.Count;
      List<int> intList = new List<int>();
      iGCell curCell = owner._grid.CurCell;
      if (curCell != null && curCell.RowIndex >= 0 && curCell.RowIndex < count && curCell.Row.Type == iGRowType.Normal && !intList.Contains(curCell.RowIndex))
      {
        INodeID nodeIdForRow = curCell.Row != null ? owner.GetNodeIDForRow(curCell.Row) : (INodeID) null;
        if (nodeIdForRow != null)
          nodeIds.Add(nodeIdForRow);
        intList.Add(curCell.RowIndex);
      }
      for (int index = 0; index < owner._grid.SelectedCells.Count; ++index)
      {
        iGCell selectedCell = owner._grid.SelectedCells[index];
        if (selectedCell != null && selectedCell.RowIndex >= 0 && selectedCell.RowIndex < count && selectedCell.Row.Type == iGRowType.Normal && !intList.Contains(selectedCell.RowIndex))
        {
          INodeID nodeIdForRow = selectedCell.Row != null ? owner.GetNodeIDForRow(selectedCell.Row) : (INodeID) null;
          if (nodeIdForRow != null)
            nodeIds.Add(nodeIdForRow);
          intList.Add(selectedCell.RowIndex);
        }
      }
    }
    catch
    {
      nodeIds.Clear();
    }
    return (ISelectedItems) selectedItems;
  }

  private ChildrenViewSelectedItems()
  {
  }

  /// <summary>Создает коллекцию.</summary>
  /// <param name="handlerPath">Полный путь родительского элемента навигации</param>
  /// <param name="handler">Родительский элемент навигации</param>
  /// <param name="owner">Родительская закладка с гридом</param>
  public ChildrenViewSelectedItems(NodeIDPath handlerPath, INode handler, ChildrenView owner)
  {
    this._nodeIDPath = handlerPath;
    this._node = handler;
    if ((owner.Options & ChildrenViewOptions.DisablePathProcessing) != (ChildrenViewOptions) 0)
    {
      IDescriptor emptyPathDescriptor = owner.GetEmptyPathDescriptor();
      INodeQuery nodeQuery = emptyPathDescriptor != null ? new EtherealNode(emptyPathDescriptor).GetQuery(ContentType.Folders) : throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_3902.ssp_imclient_3904()));
      nodeQuery.Execute((object) null, 1);
      this._nodeIDPath = new NodeIDPath(emptyPathDescriptor);
      this._nodeIDPath.Add(nodeQuery.GetRecordNodeID(0));
    }
    this._nodeItems = new NodeItems(this._nodeIDPath, handler, new NodeIDCollection(), (IServiceProvider) owner.Services);
    this._childrenView = owner;
  }

  /// <summary>
  /// Указать, что список является некорректным и требует пересмотра
  /// </summary>
  public void Invalidate()
  {
    this._valid = false;
    this._nodeItems.NodeIDs.Clear();
  }

  /// <summary>Создать точную копию коллекции</summary>
  /// <returns>Точная копия коллекции</returns>
  public object Clone()
  {
    ChildrenViewSelectedItems viewSelectedItems = new ChildrenViewSelectedItems()
    {
      _node = this._node,
      _nodeIDPath = this._nodeIDPath,
      _childrenView = this._childrenView,
      _valid = this._valid
    };
    viewSelectedItems._nodeItems = new NodeItems(viewSelectedItems._nodeIDPath, viewSelectedItems._node, new NodeIDCollection(), (IServiceProvider) viewSelectedItems._childrenView.Services);
    if (this._nodeItems.Count > 0)
    {
      for (int index = 0; index < this._nodeItems.Count; ++index)
        viewSelectedItems._nodeItems.NodeIDs.Add(this._nodeItems.NodeIDs[index]);
    }
    return (object) viewSelectedItems;
  }

  /// <summary>
  /// Возвращает true, если коллекция содержит разнородные идентификаторы
  /// элементов (т.е. созданные разными элементами навигации). Такие
  /// разнородные коллекции образуются при множественном выделении в дереве
  /// навигатора и других подобных этой ситуациях.
  /// </summary>
  public bool IsCollage
  {
    get
    {
      this.CheckValid();
      return this._nodeItems.IsCollage;
    }
  }

  /// <summary>
  /// Возвращает количество идентификаторов элементов навигации в коллеции.
  /// </summary>
  public int Count
  {
    get
    {
      this.CheckValid();
      return this._nodeItems.Count;
    }
  }

  /// <summary>
  /// Возвращает данные указанного формата для элемента коллекции. Если элемент
  /// не поддерживает указанный формат, то результатом будет null.
  /// </summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Данные в указанном формате.</returns>
  public object GetItemData(int index, Type dataFormat)
  {
    this.CheckValid();
    return this._nodeItems.GetItemData(index, dataFormat);
  }

  /// <summary>Возвращает идентификатор элемента в коллекции.</summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <returns>Идентификатор элемента.</returns>
  public INodeID GetItemID(int index)
  {
    this.CheckValid();
    return this._nodeItems.GetItemID(index);
  }

  /// <summary>
  /// Возвращает данные требуемого формата для родительского элемента,
  /// создавшего указанный идентификатор элемента. Если родительский элемент
  /// не поддерживает запрошенный формат данных, то результатом будет null.
  /// </summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Данные в указанном формате.</returns>
  public object GetParentData(int index, Type dataFormat)
  {
    if ((this._childrenView.Options & ChildrenViewOptions.DisablePathProcessing) != (ChildrenViewOptions) 0)
      return (object) null;
    this.CheckValid();
    return this._nodeItems.GetParentData(index, dataFormat);
  }

  /// <summary>
  /// Возвращает полный путь родительского элемента для указанного
  /// идентификатора в коллекции.
  /// </summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <returns>Путь родительского элемента.</returns>
  public NodeIDPath GetParentPath(int index)
  {
    this.CheckValid();
    return this._nodeItems.GetParentPath(index);
  }

  /// <summary>
  /// Обновляет коллекцию выделенных в гриде элементов навигации.
  /// </summary>
  private void CheckValid()
  {
    if (this._valid)
      return;
    NodeIDCollection nodeIds = this._nodeItems.NodeIDs;
    nodeIds.Clear();
    try
    {
      int count = this._childrenView._grid.Rows.Count;
      int groupRowsCount = this._childrenView._groupRowsCount;
      List<int> intList = new List<int>();
      iGCell curCell = this._childrenView._grid.CurCell;
      int num = -1;
      if (curCell != null && curCell.Selected && curCell.RowIndex >= 0 && curCell.RowIndex < count && curCell.Row.Type == iGRowType.Normal)
      {
        INodeID nodeIdForRow = curCell.Row != null ? this._childrenView.GetNodeIDForRow(curCell.Row) : (INodeID) null;
        if (nodeIdForRow != null)
          num = curCell.Row.Index;
        if (nodeIdForRow != null)
          nodeIds.Add(nodeIdForRow);
        intList.Add(curCell.RowIndex);
      }
      for (int index = 0; index < this._childrenView._grid.SelectedCells.Count; ++index)
      {
        iGCell selectedCell = this._childrenView._grid.SelectedCells[index];
        iGRow row = selectedCell != null ? this._childrenView._grid.Rows[selectedCell.RowIndex] : (iGRow) null;
        if (row != null && row.Type == iGRowType.Normal && row.Index != num)
        {
          INodeID nodeIdForRow = selectedCell.Row != null ? this._childrenView.GetNodeIDForRow(selectedCell.Row) : (INodeID) null;
          if (nodeIdForRow != null)
            nodeIds.Add(nodeIdForRow);
          intList.Add(selectedCell.RowIndex);
        }
      }
      this._valid = true;
    }
    catch
    {
      this._valid = false;
      nodeIds.Clear();
    }
  }
}
