// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks.ArtsCompositionTaskActionTechProc
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;

/// <summary>
/// Действие задачи по развороту состава технологической сборочной единицы (ТП)
/// </summary>
internal class ArtsCompositionTaskActionTechProc : CompositionTaskActionBase
{
  /// <summary>
  /// 
  /// </summary>
  private readonly List<long> _compositionContextIds;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="compositionContextIds">Контексты состава ( фильтрация контекстов состава )</param>
  public ArtsCompositionTaskActionTechProc(List<long> compositionContextIds)
  {
    this._compositionContextIds = compositionContextIds;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override ICompositionService StartCompositionService(IUserSession session)
  {
    ICompositionService service = ServiceUtils.GetService<ICompositionService>((object) session, false);
    if (service == null)
      return (ICompositionService) null;
    HybridDictionary Tags = new HybridDictionary(0, true)
    {
      [(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) this._compositionContextIds
    };
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.EdinicaSostavaID);
    RuntimeSearchScheme compositionQuantityScheme = RuntimeSearchScheme.GetCompositionQuantityScheme(session, childrenIdRecursive.ToArray(), new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    });
    List<ColumnDescriptor> schemeDescriptors = RuntimeSearchScheme.GetCompositionQuantitySchemeDescriptors(session);
    int num1 = -1;
    int num2 = -1;
    int num3 = -1;
    for (int index = 0; index < schemeDescriptors.Count; ++index)
    {
      ColumnDescriptor columnDescriptor = schemeDescriptors[index];
      if (columnDescriptor.AttributeID != null)
      {
        object attributeId = columnDescriptor.AttributeID;
        Type type = attributeId.GetType();
        if (type == typeof (int) && attributeId.Equals((object) TechCardConsts.AttributeTypes.ObjectRefAttrID) || type == typeof (Guid) && attributeId.Equals((object) TechCardConsts.AttributeTypes.ObjectRefAttrGuid))
          num1 = index;
        if (type == typeof (int) && attributeId.Equals((object) TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrID) || type == typeof (Guid) && attributeId.Equals((object) TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrGUID))
          num3 = index;
        if (type == typeof (int) && attributeId.Equals((object) -21))
          num2 = index;
        if (num1 != -1 && num2 != -1 && num3 != -1)
          break;
      }
    }
    if (num1 == -1)
      schemeDescriptors.Add(new ColumnDescriptor((object) TechCardConsts.AttributeTypes.ObjectRefAttrID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    if (num2 == -1)
      schemeDescriptors.Add(new ColumnDescriptor((object) -21, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    if (num3 == -1)
      schemeDescriptors.Add(new ColumnDescriptor((object) TechCardConsts.AttributeTypes.MemberOfSborkaObjectAttrID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    service.CancelSelect(this._taskGuid);
    service.Select(session.SessionGUID, this._objInfoItem.ObjectID, compositionQuantityScheme, schemeDescriptors, this._taskGuid, string.Empty, Tags);
    return service;
  }
}
