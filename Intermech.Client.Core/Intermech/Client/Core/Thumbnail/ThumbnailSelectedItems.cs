
// Type: Intermech.Client.Core.Thumbnail.ThumbnailSelectedItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core.Thumbnail;

/// <summary>Реализует коллекцию выделенных в списке изображений.</summary>
internal class ThumbnailSelectedItems : ISelectedItems, ISimpleSelectedItems
{
  private NodeItems _nodeItems;
  private ThumbnailView _view;
  private bool _valid;

  /// <summary>Создает коллекцию.</summary>
  /// <param name="handlerPath">Полный путь родительского элемента навигации</param>
  /// <param name="handler">Родительский элемент навигации</param>
  /// <param name="owner">Родительская закладка с видом</param>
  public ThumbnailSelectedItems(NodeIDPath handlerPath, INode handler, ThumbnailView owner)
  {
    this._nodeItems = new NodeItems(handlerPath, handler, new NodeIDCollection(), (IServiceProvider) owner._services);
    this._view = owner;
  }

  /// <summary>
  /// 
  /// </summary>
  public void Invalidate() => this._valid = false;

  /// <summary>
  /// 
  /// </summary>
  public int Count
  {
    get
    {
      this.Validate();
      return this._nodeItems.Count;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool IsCollage
  {
    get
    {
      this.Validate();
      return this._nodeItems.IsCollage;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <param name="dataFormat"></param>
  /// <returns></returns>
  public object GetItemData(int index, Type dataFormat)
  {
    this.Validate();
    return this._nodeItems.GetItemData(index, dataFormat);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public INodeID GetItemID(int index)
  {
    this.Validate();
    return this._nodeItems.GetItemID(index);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <param name="dataFormat"></param>
  /// <returns></returns>
  public object GetParentData(int index, Type dataFormat)
  {
    this.Validate();
    return this._nodeItems.GetParentData(index, dataFormat);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public NodeIDPath GetParentPath(int index)
  {
    this.Validate();
    return this._nodeItems.GetParentPath(index);
  }

  /// <summary>
  /// Обновляет коллекцию выделенных в виде элементов навигации.
  /// </summary>
  private void Validate()
  {
    if (this._valid)
      return;
    NodeIDCollection nodeIds = this._nodeItems.NodeIDs;
    nodeIds.Clear();
    try
    {
      ThumbnailItem selectedItem = this._view.SelectedItem;
      if (selectedItem != null)
        nodeIds.Add(selectedItem.NodeID);
      this._valid = true;
    }
    catch
    {
      this._valid = false;
      nodeIds.Clear();
    }
  }
}
