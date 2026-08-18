
// Type: Intermech.Client.Core.CompositionView.CompositionViewSortingInfoItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Структура записи кэша сортировки</summary>
internal class CompositionViewSortingInfoItem : CompositionSortingInfoItem
{
  /// <summary>Конструктор</summary>
  /// <param name="dbRelationId"></param>
  /// <param name="dbTypedObjectId"></param>
  /// <param name="treeNode"></param>
  public CompositionViewSortingInfoItem(
    IDBRelationID dbRelationId,
    IDBTypedObjectID dbTypedObjectId,
    NavigatorTreeNode treeNode = null)
  {
    if (dbRelationId != null)
    {
      this.PrjLinkID = dbRelationId.Value;
      this.RelTypeID = dbRelationId.RelationType;
      this.Sorting = dbRelationId.Sorting;
    }
    if (dbTypedObjectId != null)
      this.PartObjType = dbTypedObjectId.ObjectType;
    this.TreeNode = treeNode;
  }

  /// <summary>
  /// 
  /// </summary>
  public NavigatorTreeNode TreeNode { get; }
}
