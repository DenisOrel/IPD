// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.DataProviders.Composition.TechRelObjInfoItemsFromSelectedItemApplicabilityProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Services.DataProviders.Composition;

/// <summary>
/// Провайдер данных об иерархии входимости текущих объектов
/// </summary>
/// <summary>
/// 
/// </summary>
/// <param name="selectedItems">Список выделенных объектов</param>
/// <param name="contextServices">Контейнер сервисов окружения</param>
internal class TechRelObjInfoItemsFromSelectedItemApplicabilityProvider(
  [NotNull] ISelectedItems selectedItems,
  [NotNull] IServiceProvider contextServices) : TechRelObjInfoItemsFromSelectedItemContextProvider(selectedItems, contextServices)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="relObjInfoItems"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  private bool CollectDataFromTreeNode([NotNull] IList<RelObjInfoItem> relObjInfoItems, int index)
  {
    NavigatorTreeNode itemData = this._selectedItems.GetItemData<NavigatorTreeNode>(index, false);
    if (itemData == null)
      return false;
    IDBTypedObjectID dbTypedObjectId = this._selectedItems.GetItemData<IDBTypedObjectID>(index, false);
    if (dbTypedObjectId == null)
      return false;
    IDBTypedObjectID projTypedObjectId;
    IDBRelationID dbRelationId;
    for (NavigatorTreeNode treeNode = itemData; treeNode != null && treeNode.Level != 0 && TechcardClientControlsUtils.GetObjectInfo(treeNode, out dbTypedObjectId, out projTypedObjectId, out dbRelationId, false) && dbRelationId != null; treeNode = treeNode.Parent)
    {
      ObjInfoItem partInfo = (ObjInfoItem) new ObjInfoIDItem(dbTypedObjectId.ObjectID, dbTypedObjectId.ObjectType, dbTypedObjectId.ID);
      RelObjInfoItem relObjInfoItem = (RelObjInfoItem) new SortedRelObjInfoItem(new RelInfoItem(dbRelationId.Value, dbRelationId.RelationType), projTypedObjectId != null ? (ObjInfoItem) new ObjInfoIDItem(projTypedObjectId.ObjectID, projTypedObjectId.ObjectType, projTypedObjectId.ID) : (ObjInfoItem) new ObjInfoIDItem(dbRelationId.ProjID), partInfo, dbRelationId.Sorting);
      if (!relObjInfoItems.Contains<RelObjInfoItem>(relObjInfoItem, this.RelationItemComparer))
        relObjInfoItems.Add(relObjInfoItem);
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override IEnumerable<RelObjInfoItem> Execute()
  {
    IList<RelObjInfoItem> relObjInfoItems = (IList<RelObjInfoItem>) new List<RelObjInfoItem>();
    if (this._selectedItems.Count == 0)
      return (IEnumerable<RelObjInfoItem>) relObjInfoItems;
    for (int index = 0; index < this._selectedItems.Count; ++index)
    {
      if (!this.CollectDataFromTreeNode(relObjInfoItems, index))
        this.CollectDataFromObjectNode(relObjInfoItems, index);
    }
    return (IEnumerable<RelObjInfoItem>) relObjInfoItems;
  }
}
