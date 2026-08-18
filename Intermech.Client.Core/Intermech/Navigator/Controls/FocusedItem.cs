
// Type: Intermech.Navigator.Controls.FocusedItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;


namespace Intermech.Navigator.Controls;

/// <summary>Класс сфокусированного узла в списке</summary>
internal class FocusedItem : IFocusedItem
{
  /// <summary>Колонка сфокусированного узла</summary>
  private NodeColumn _focusedColumn;
  /// <summary>Идентификатор сфокусированного узла</summary>
  private INodeID _focusedItem;
  /// <summary>Путь к родительскому узлу</summary>
  private NodeIDPath _parentPath;
  /// <summary>Узел</summary>
  private INode _handler;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _services;

  /// <summary>Создать экземпляр сфокусированного узла</summary>
  /// <param name="focusedColumn">Колонка сфокусированного узла</param>
  /// <param name="focusedItem">Идентификатор сфокусированного узла</param>
  /// <param name="parentPath">Путь к родительскому узлу</param>
  /// <param name="handler">Узел</param>
  /// <param name="services">Контейнер сервисов</param>
  public FocusedItem(
    NodeColumn focusedColumn,
    INodeID focusedItem,
    NodeIDPath parentPath,
    INode handler,
    IServiceProvider services)
  {
    this._focusedColumn = focusedColumn;
    this._focusedItem = focusedItem;
    this._parentPath = parentPath;
    this._handler = handler;
    this._services = services;
  }

  /// <summary>Колонка сфокусированного узла</summary>
  public NodeColumn FocusedColumn
  {
    [DebuggerStepThrough] get => this._focusedColumn;
  }

  /// <summary>Идентификатор сфокусированного узла</summary>
  public INodeID ItemID
  {
    [DebuggerStepThrough] get => this._focusedItem;
  }

  /// <summary>Путь к родительскому узлуs</summary>
  public NodeIDPath ParentPath
  {
    [DebuggerStepThrough] get => this._parentPath;
  }

  /// <summary>Извлечь из узла данные указанного типа</summary>
  /// <param name="dataFormat">Тип данных</param>
  /// <returns>Данные указанного типа или null</returns>
  public object GetItemData(Type dataFormat)
  {
    return this._handler.GetData(this._focusedItem, dataFormat);
  }

  /// <summary>Извлечь из родительского узла данные указанного типа</summary>
  /// <param name="dataFormat">Тип данных</param>
  /// <returns>Данные указанного типа или null</returns>
  public object GetParentData(Type dataFormat)
  {
    return Utils.GetDataFromPath(this._parentPath, dataFormat, this._services);
  }
}
