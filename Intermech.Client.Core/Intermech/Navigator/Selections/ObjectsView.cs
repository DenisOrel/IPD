
// Type: Intermech.Navigator.Selections.ObjectsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Intermech.Navigator.Selections;

/// <summary>
/// Закладка со списком объектов, найденных по условиям выборки
/// </summary>
public class ObjectsView : ObjectsViewBase
{
  private int _parentCategory;
  private int _parentType;
  private string _parentSuffix;

  /// <summary>
  /// Узел дерева "Навигатора", в контексте которого работает закладка
  /// </summary>
  private NavigatorTreeNode _parentTreeNode
  {
    get
    {
      if (!(this._services.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service) || service.FocusedNode == null)
        return (NavigatorTreeNode) null;
      NavigatorTreeNode focusedNode;
      return (focusedNode = service.FocusedNode) == null || focusedNode.NodeID == null || !focusedNode.InTree ? (NavigatorTreeNode) null : focusedNode;
    }
  }

  public override ContentType ViewContentType => ContentType.NonFolders;

  public override string Caption
  {
    get
    {
      return this._parentNode.GetData(this._nodeID, typeof (IBinding)) is IBinding data ? data.ViewCaption : LocalizationHolder.rm.GetString("Client.Core_277");
    }
  }

  public override int ImageIndex => Holder.NamedImageList.ImageIndex("imgFind");

  /// <summary>
  /// Отыскать родительские категорию, тип и дополнительное имя
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  private void FindParentCategoryType(int category, int type, string suffix)
  {
    this._parentCategory = category;
    this._parentType = type;
    this._parentSuffix = suffix;
    if (this._navigatorColumnsService.GetNavigatorColumns(category, type, suffix, false) != null)
      return;
    foreach (INodeID nodeId in this._path.Cast<INodeID>().Reverse<INodeID>().Skip<INodeID>(1).ToArray<INodeID>())
    {
      if (nodeId.CategoryID != 1 || !MetaDataHelper.IsObjectTypeChildOf(nodeId.TypeID, MetaDataHelper.GetObjectTypeID("cad00119-306c-11d8-b4e9-00304f19f545")))
        break;
      string empty = string.Empty;
      if (nodeId is SelectionNodeID selectionNodeId)
        empty = selectionNodeId.ObjectID.ToString();
      NavigatorColumns navigatorColumns = this._navigatorColumnsService.GetNavigatorColumns(this._parentCategory, this._parentType, empty, false);
      if (navigatorColumns != null)
      {
        this._parentCategory = navigatorColumns.Category;
        this._parentType = navigatorColumns.Type;
        this._parentSuffix = navigatorColumns.Suffix;
        break;
      }
    }
  }

  protected override int StateStreamCategoryID
  {
    get
    {
      return this._parentNode.GetData(this._nodeID, typeof (IBinding)) is IBinding data && data is IBindingStateStream ? (data as IBindingStateStream).CategoryID : base.StateStreamCategoryID;
    }
  }

  protected override int StateStreamCategoryType
  {
    get
    {
      return this._parentNode.GetData(this._nodeID, typeof (IBinding)) is IBinding data && data is IBindingStateStream ? (data as IBindingStateStream).CategoryType : base.StateStreamCategoryType;
    }
  }

  public override string StateStreamPrefix
  {
    get
    {
      if (this._parentNode.GetData(this._nodeID, typeof (IBinding)) is IBinding data1 && data1 is IBindingStateStream)
      {
        IBindingStateStream bindingStateStream = data1 as IBindingStateStream;
        if (bindingStateStream.Prefix != null)
          return bindingStateStream.Prefix;
      }
      INode parentNode = this._parentNode;
      return parentNode != null && parentNode.GetData(this._path.LastID, typeof (IDBObjectID)) is IDBObjectID data2 ? Math.Abs(data2.Value).ToString() : base.StateStreamPrefix;
    }
    set => base.StateStreamPrefix = value;
  }

  /// <summary>Загрузить настройки грида</summary>
  /// <param name="stateStream">Поток, из которого требуется загрузить состояние грида,
  /// или null, если грузить из потока по умолчанию</param>
  public override void GridLoadState(Stream stateStream)
  {
    bool flag = true;
    List<int> groups = new List<int>();
    NavigatorColumns navigatorColumns = this.GetNavigatorColumns();
    NodeColumnCollection columns1 = (NodeColumnCollection) null;
    try
    {
      if (navigatorColumns == null || navigatorColumns.Empty || navigatorColumns.Columns == null || navigatorColumns.Columns.Count <= 0)
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
          Intermech.Navigator.DBObjects.Helper.AddObligatoryColumns(columns2, true, false);
          columns1 = columns2;
        }
        groups.Clear();
      }
      this.GridSetColumns(columns1, false);
      this.GridSetGroups(columns1, groups, false);
    }
  }

  private NavigatorColumns GetNavigatorColumns()
  {
    this.FindParentCategoryType(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix);
    return this._navigatorColumnsService.GetNavigatorColumns(this._parentCategory, this._parentType, this._parentSuffix, true) ?? this._navigatorColumnsService.GetNavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix, true);
  }

  /// <summary>Сохраним состояние грида</summary>
  /// <param name="stateStream">Поток, в который надо сохранять состояние. Если указать null,
  /// грид сохранится в свой стандартный поток</param>
  public override void GridSaveState(Stream stateStream, NodeColumnCollection nodeColumns = null)
  {
    NavigatorColumns navigatorColumns = this.GetNavigatorColumns();
    if (navigatorColumns == null)
      navigatorColumns = new NavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix)
      {
        Columns = this.Node?.GetDefaultColumns(this.ViewContentType),
        Groups = new List<int>()
      };
    NavigatorColumns columns = new NavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix)
    {
      Columns = this.GetNodeColumns()
    };
    columns.Groups = this.GridGetGroupColumns(columns.Columns);
    if (navigatorColumns != null)
    {
      navigatorColumns = navigatorColumns.Clone() as NavigatorColumns;
      navigatorColumns.Inherited = false;
      navigatorColumns.Category = columns.Category;
      navigatorColumns.Type = columns.Type;
      navigatorColumns.Suffix = columns.Suffix;
    }
    if (columns.Equals((object) navigatorColumns))
      return;
    this._navigatorColumnsService.CreateNavigatorColumns(columns);
  }

  /// <summary>Получено очередное событие от службы уведомлений</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  protected override void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (e.EventName == "ObjectsChanged")
    {
      if (e is DBObjectsEventArgs objectsEventArgs && this._nodeID is Intermech.Navigator.DBObjects.NodeID nodeId && objectsEventArgs.ObjectIDs != null && objectsEventArgs.ObjectIDs.Contains(nodeId.ObjectID))
      {
        base.NotificationEventFired(sender, e);
        return;
      }
      this.ReloadItems();
    }
    base.NotificationEventFired(sender, e);
  }
}
