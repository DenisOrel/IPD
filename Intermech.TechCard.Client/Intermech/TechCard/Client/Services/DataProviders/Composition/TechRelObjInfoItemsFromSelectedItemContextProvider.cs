// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.DataProviders.Composition.TechRelObjInfoItemsFromSelectedItemContextProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Services.DataProviders.Composition;

/// <summary>
/// Провайдер данных о текущих объектов с контекстом связей
/// </summary>
internal class TechRelObjInfoItemsFromSelectedItemContextProvider : 
  ITechCardDataEnumerableProvider<RelObjInfoItem>,
  ITechCardDataProvider<IEnumerable<RelObjInfoItem>>
{
  /// <summary>Список выделенных объектов</summary>
  protected readonly ISelectedItems _selectedItems;
  /// <summary>
  /// 
  /// </summary>
  protected readonly IServiceProvider _contextServices;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relObjInfoItems"></param>
  /// <param name="index"></param>
  protected void CollectDataFromObjectNode([NotNull] IList<RelObjInfoItem> relObjInfoItems, int index)
  {
    IDBTypedObjectID itemData1 = this._selectedItems.GetItemData<IDBTypedObjectID>(index, false);
    if (itemData1 == null)
      return;
    ObjInfoItem partInfo = (ObjInfoItem) new ObjInfoIDItem(itemData1.ObjectID, itemData1.ObjectType, itemData1.ID);
    IDBRelationID itemData2 = this._selectedItems.GetItemData<IDBRelationID>(index, false);
    if (itemData2 == null)
      return;
    IDBTypedObjectID parentData = this._selectedItems.GetParentData<IDBTypedObjectID>(index, false);
    RelObjInfoItem relObjInfoItem = (RelObjInfoItem) new SortedRelObjInfoItem(new RelInfoItem(itemData2.Value, itemData2.RelationType), parentData != null ? (ObjInfoItem) new ObjInfoIDItem(parentData.ObjectID, parentData.ObjectType, parentData.ID) : (ObjInfoItem) new ObjInfoIDItem(itemData2.ProjID), partInfo, itemData2.Sorting);
    if (this.RelationItemFilter != null && !this.RelationItemFilter(relObjInfoItem) || relObjInfoItems.Contains<RelObjInfoItem>(relObjInfoItem, this.RelationItemComparer))
      return;
    relObjInfoItems.Add(relObjInfoItem);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedItems">Список выделенных объектов</param>
  /// <param name="contextServices">Контейнер сервисов окружения</param>
  public TechRelObjInfoItemsFromSelectedItemContextProvider(
    [NotNull] ISelectedItems selectedItems,
    [NotNull] IServiceProvider contextServices)
  {
    this._selectedItems = selectedItems;
    this._contextServices = contextServices;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public virtual IEnumerable<RelObjInfoItem> Execute()
  {
    IList<RelObjInfoItem> relObjInfoItems = (IList<RelObjInfoItem>) new List<RelObjInfoItem>();
    if (this._selectedItems.Count == 0)
      return (IEnumerable<RelObjInfoItem>) relObjInfoItems;
    for (int index = 0; index < this._selectedItems.Count; ++index)
      this.CollectDataFromObjectNode(relObjInfoItems, index);
    return (IEnumerable<RelObjInfoItem>) relObjInfoItems;
  }

  /// <summary>Фильтр обрабатываемых данных</summary>
  /// <remarks>По умолчанию это технологический состав</remarks>
  /// &gt;
  public Func<RelObjInfoItem, bool> RelationItemFilter { get; set; }

  /// <summary>
  /// Правило сравнения элементов при формировании списка, одинаковые элементы игнорируем
  /// </summary>
  public IEqualityComparer<RelObjInfoItem> RelationItemComparer { get; set; } = (IEqualityComparer<RelObjInfoItem>) EqualityComparer<RelObjInfoItem>.Default;
}
