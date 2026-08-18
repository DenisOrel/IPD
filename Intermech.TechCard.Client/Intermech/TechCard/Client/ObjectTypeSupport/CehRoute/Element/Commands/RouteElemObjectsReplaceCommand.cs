// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element.Commands.RouteElemObjectsReplaceCommand
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

internal class RouteElemObjectsReplaceCommand : RouteElemObjectsBaseCommand
{
  private long _newRouteElemTemplateObjectId;

  /// <summary>Диалог выбора РЭ</summary>
  /// <returns></returns>
  protected override bool SelectRouteElemObjects()
  {
    this._routeElementTemplateObjectId = TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.ElemRouteTemplateGuid, "Выберите заменяемый элемент маршрута");
    if (Intermech.Consts.IsUndefinedObjectId(this._routeElementTemplateObjectId))
      return false;
    this._newRouteElemTemplateObjectId = TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.ElemRouteTemplateGuid, "Выберите элемент маршрута - заменитель");
    return !Intermech.Consts.IsUndefinedObjectId(this._routeElementTemplateObjectId) && this._routeElementTemplateObjectId != this._newRouteElemTemplateObjectId;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbObject"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  protected override BaseCommandResult DoEditCommand(IDBObject dbObject, int index)
  {
    int attributeId = MetaDataHelper.GetAttributeID((object) "cadd9668-306c-11d8-b4e9-00304f19f545");
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(attributeId, RelationalOperators.Equal, (object) Math.Abs(this._routeElementTemplateObjectId), (object) null, LogicalOperators.OR, 0, false, AttributeSourceTypes.Object),
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
    IDBObject dbObject1 = dbObject.Session.GetObject(this._newRouteElemTemplateObjectId, true);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      IDBObject dbObject2 = dbObject.Session.GetObject(DataSetProcessor.GetInt64Value(row, 0, 0L), false);
      if (dbObject2 != null)
      {
        bool flag = dbObject2.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject2.CheckoutBy != dbObject.Session.UserID;
        if (flag)
          dbObject2 = dbObject2.CheckOut();
        dbObject2.Attributes.AssignPossibleAttributes(dbObject1.Attributes, 0);
        dbObject2.SetAttributesValues(new AttributeValues[2]
        {
          new AttributeValues(attributeId, (object) Math.Abs(this._newRouteElemTemplateObjectId)),
          new AttributeValues(TechCardConsts.AttributeTypes.ElemRouteTemplateReferenceAttrID, (object) Math.Abs(this._newRouteElemTemplateObjectId))
        });
        if (flag)
          dbObject2.CheckIn();
      }
    }
    return BaseCommandResult.OK;
  }
}
