// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element.Commands.RouteElemObjectsDeleteCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.TechCard.Client.Commands.Edit;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element.Commands;

internal class RouteElemObjectsDeleteCommand : RouteElemObjectsBaseCommand
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbObject"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  protected override BaseCommandResult DoEditCommand(IDBObject dbObject, int index)
  {
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(MetaDataHelper.GetAttributeID((object) "cadd9668-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) Math.Abs(this._routeElementTemplateObjectId), (object) null, LogicalOperators.OR, 0, false, AttributeSourceTypes.Object),
      new ConditionStructure(TechCardConsts.AttributeTypes.ElemRouteTemplateReferenceAttrID, RelationalOperators.Equal, (object) Math.Abs(this._routeElementTemplateObjectId), (object) null, LogicalOperators.OR, 0, false, AttributeSourceTypes.Object)
    };
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) dbObject.Session, true);
    CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      new ObjInfoItem(dbObject)
    }, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ElemRouteID), (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, ObjInfoDbScheme.GetSourceTableColumns(), (IEnumerable<ConditionStructure>) conditions, true, false, -1, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule);
    DataTable dataTable = service.LoadComplexCompositions((object) dbObject.Session.SessionGUID, loadingParams);
    if (dataTable == null)
      return BaseCommandResult.OK;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      dbObject.Session.GetObject(DataSetProcessor.GetInt64Value(row, 0, 0L), false)?.Delete(0L);
    return BaseCommandResult.OK;
  }
}
