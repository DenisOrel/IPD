// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.Creator.ProcRouteObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Services.ClassifyObject;
using Intermech.TechCard.Client.TcObjectsTypes;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.Creator;

/// <summary>ProcRouteObjectCreatorService</summary>
internal class ProcRouteObjectCreatorService : TechObjectCreatorRiderCustomService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="newObjectId"></param>
  /// <returns></returns>
  public override bool AfterCreate(long newObjectId)
  {
    if (this._creatorArgs == null || this._creatorArgs.IsVersion || this._creatorArgs.TemplateObjectIDs != null && ((IEnumerable<long>) this._creatorArgs.TemplateObjectIDs).Any<long>((Func<long, bool>) (item => item != 0L && item != -1L)) || this._creatorArgs?.RelatedObjectIDs == null || this._creatorArgs.RelatedObjectIDs.Length == 0)
      return true;
    List<ObjInfoItem> objectInfoList = ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) this._creatorArgs.RelatedObjectIDs);
    if (objectInfoList == null || objectInfoList.Count == 0)
      return true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ObjInfoItem classifyObjectItem = new ObjInfoItem(newObjectId, this._creatorArgs.ObjectTypeIDs[0]);
      ObjInfoItem objInfoItem = new ObjInfoItem(objectInfoList[0].ObjectID);
      IEnumerable<RelObjInfoItem> relObjInfoItems;
      IEnumerable<ObjInfoItem> objInfoItems = !(this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams) || !TechcardClientControlsUtils.GetItemsApplicabilityInfo(creatorExtraParams.Items, (IServiceProvider) ApplicationServices.Container, out relObjInfoItems) ? (IEnumerable<ObjInfoItem>) null : relObjInfoItems.Select<RelObjInfoItem, ObjInfoItem>((Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo));
      ObjInfoItem contextObjectItem = objInfoItem;
      TechCardClassifyObjectParams classifyBaseObjectParams = new TechCardClassifyObjectParams(classifyObjectItem, contextObjectItem)
      {
        ExtraContextObjInfoItems = objInfoItems
      };
      List<AttributeValues> attributeValuesList = ProcRouteHelper.ClassifyNewObject(sessionKeeper.Session, classifyBaseObjectParams);
      if (attributeValuesList.Count == 0)
        return true;
      if (ProcRouteHelper.GetDefaultProcRouteForArticle(objectInfoList[0].ObjectID, sessionKeeper.Session, false) == 0L)
        attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.ProcRouteDefaultAttrID, ProcRouteHelper.RouteProcDefaultAttrValue));
      IDBObject dbObject = sessionKeeper.Session.GetObject(newObjectId, false);
      if (dbObject == null)
        return true;
      dbObject.SetAttributesValues(attributeValuesList.ToArray());
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectTypeId"></param>
  /// <param name="templateObjectId"></param>
  /// <param name="relationTypeIDs"></param>
  /// <param name="relatedObjectIDs"></param>
  /// <param name="startDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public override bool AcceptDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    if (this._creatorArgs == null)
      this._creatorArgs = new TechObjectCreatorArgs(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
    base.AcceptDialog(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
    return false;
  }

  /// <summary>CreateObjectDialog</summary>
  /// <param name="objectTypeId"></param>
  /// <param name="templateObjectId"></param>
  /// <param name="relationTypeIDs"></param>
  /// <param name="relatedObjectIDs"></param>
  /// <param name="startDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public override long CreateObjectDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    return base.CreateObjectDialog(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      IDictionary<ObjectCreatePages, bool> visiblePages = base.VisiblePages;
      if (!visiblePages.ContainsKey(ObjectCreatePages.Relations))
        visiblePages.Add(ObjectCreatePages.Relations, true);
      else
        visiblePages[ObjectCreatePages.Relations] = true;
      return visiblePages;
    }
  }

  public override bool OnCommitAction(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    ISelectedItems navigatorSelection = SelectedItemsHelper.GetNavigatorSelection();
    if (navigatorSelection != null)
      ProcRouteEntryHelper.CreateProcRouteEntry(session, new ObjInfoItem(newObjectId, this._creatorArgs.ObjectTypeIDs[0]), false, navigatorSelection);
    return base.OnCommitAction(session, newObjectId, nea);
  }
}
