// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.CreateVersion.TechRelObjInfoItemsEcoProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.TechCard.Client.Services.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Commands.CreateVersion;

/// <summary>
/// Провайдер для получения применяемости объектов в извещениям, включая связанные
/// </summary>
internal class TechRelObjInfoItemsEcoProvider : 
  ITechCardDataEnumerableProvider<RelObjInfoItem>,
  ITechCardDataProvider<IEnumerable<RelObjInfoItem>>
{
  /// <summary>
  /// Список исходных объектов, для которых требуется найти извещения
  /// </summary>
  private readonly IEnumerable<ObjInfoItem> _objInfoItems;

  /// <summary>Конструктор</summary>
  /// <param name="objInfoItems"></param>
  public TechRelObjInfoItemsEcoProvider([NotNull] IEnumerable<ObjInfoItem> objInfoItems)
  {
    this._objInfoItems = objInfoItems;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerable<RelObjInfoItem> Execute()
  {
    List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      int relationTypeId = MetaDataHelper.GetRelationTypeID(new Guid("cad0036b-306c-11d8-b4e9-00304f19f545"));
      List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(this._objInfoItems.ToList<ObjInfoItem>(), session, new int[1]
      {
        relationTypeId
      }, false, (ConditionStructure[]) null, (Dictionary<string, ColumnDescriptor>) null);
      foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
      {
        if (sostavTreeItem != null)
          relObjInfoItemList.Add(new RelObjInfoItem(sostavTreeItem.LinkID, sostavTreeItem.LinkTypeID)
          {
            ProjInfo = new ObjInfoItem(sostavTreeItem.PartID),
            PartInfo = new ObjInfoItem(sostavTreeItem.ProjID, sostavTreeItem.ObjectTypeID)
          });
      }
      List<RelObjInfoItem> collection = new List<RelObjInfoItem>();
      IDBEditingContextsService service = ServiceUtils.GetService<IDBEditingContextsService>((object) sessionKeeper.Session, true);
      foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
      {
        List<long> linkedContexts = service.GetLinkedContexts((object) sessionKeeper.Session.SessionGUID, sostavTreeItem.ProjID);
        if (linkedContexts != null && linkedContexts.Count != 0)
        {
          foreach (long objectId in linkedContexts)
          {
            if (objectId != sostavTreeItem.ProjID)
            {
              RelObjInfoItem relObjInfoItem = new RelObjInfoItem(0L)
              {
                ProjInfo = new ObjInfoItem(sostavTreeItem.ProjID),
                PartInfo = new ObjInfoItem(objectId)
              };
              collection.Add(relObjInfoItem);
            }
          }
        }
      }
      relObjInfoItemList.AddRange((IEnumerable<RelObjInfoItem>) collection);
    }
    return (IEnumerable<RelObjInfoItem>) relObjInfoItemList;
  }
}
