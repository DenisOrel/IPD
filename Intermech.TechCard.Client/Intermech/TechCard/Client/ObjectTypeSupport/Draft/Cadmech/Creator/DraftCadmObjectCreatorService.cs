// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech.Creator.DraftCadmObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.TechAcad.Connector;
using Intermech.TechAcad.Interfaces;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech.Creator;

/// <summary>Сервис создания объектов типа "Эскиз Cadmech-T"</summary>
internal class DraftCadmObjectCreatorService : TechObjectCreatorRiderCustomService
{
  /// <summary>Добавленный эскиз (слой DWG)</summary>
  private ISketchObject _sketchObject;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="newObjectId"></param>
  /// <returns></returns>
  public override bool AfterCreate(long newObjectId)
  {
    if (this._creatorArgs == null || this._creatorArgs.IsVersion || this._creatorArgs.TemplateObjectIDs != null && ((IEnumerable<long>) this._creatorArgs.TemplateObjectIDs).Any<long>((Func<long, bool>) (item => item != 0L && item != -1L)))
      return true;
    ITechAcadService service = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, true);
    if (!service.LoadAcad(TechAcadLoadMode.Normal))
      return false;
    service.ShowAcadWindow(WindowMode.Minimize);
    if (!service.CreatePicture(newObjectId))
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<ObjInfoItem> objectInfoList = ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) this._creatorArgs.RelatedObjectIDs);
      ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objectInfoList, sessionKeeper.Session);
      ObjInfoItem objectItem = objectInfoList.FirstOrDefault<ObjInfoItem>((Func<ObjInfoItem, bool>) (item => TechCardConsts.Utils.IsTechcardObjectType((object) item.ObjTypeID)));
      if ((TypedInfoItem) objectItem == (TypedInfoItem) null)
        return true;
      NavWindow activeDockControl = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, true).ActiveDockControl as NavWindow;
      ITPObject tpObject = TechAcadApplication.GetTpObject(objectItem, activeDockControl);
      IMSObjectType objectType = MetaDataHelper.GetObjectType(((IEnumerable<int>) this._creatorArgs.ObjectTypeIDs).FirstOrDefault<int>());
      List<AttributeValues> attributeValuesList = new List<AttributeValues>();
      if (objectType.AnyAttributes || MetaDataHelper.GetAttribute4ObjectType(objectType.ObjectTypeID, TechCardConsts.AttributeTypes.NameAttrTypeID) != null)
        attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) tpObject.Name));
      if (objectType.AnyAttributes || MetaDataHelper.GetAttribute4ObjectType(objectType.ObjectTypeID, TechCardConsts.AttributeTypes.DesignationAttrTypeID) != null)
      {
        string initValue = tpObject.Designation;
        long parentTp = TechCardUtils.GetParentTP(tpObject.ObjID, sessionKeeper.Session);
        if (parentTp != 0L && parentTp != tpObject.ObjID)
          initValue = $"{sessionKeeper.Session.GetObjectAttribute(parentTp, (object) TechCardConsts.AttributeTypes.DesignationAttrTypeID, false, true)?.AsString ?? string.Empty} ({initValue})";
        attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) initValue));
      }
      if (attributeValuesList.Count != 0)
        sessionKeeper.Session.SetObjectAttributesValues(newObjectId, true, attributeValuesList.ToArray());
      IDraftObject draftObject = TechAcadApplication.GetDraftObject(new ObjInfoItem(newObjectId), activeDockControl);
      if (tpObject != null)
      {
        if (draftObject != null)
        {
          if (tpObject.SketchCollection == null)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("TechCard.TechAcadDraftRelationNotAllowed"), (object) tpObject.TPObjectType.Name, (object) tpObject.Name, (object) MetaDataHelper.GetRelationTypeName(TechCardConsts.RelTypes.TechDraftRelationID), (object) objectType.ObjectName));
          this._sketchObject = tpObject.SketchCollection.Add("Эскиз 1", draftObject, tpObject);
          service.OpenPicture(newObjectId);
          Intermech.TechAcad.Connector.TechAcad.CopyOper(string.Empty, string.Empty, this._sketchObject.SketchID, this._sketchObject.Name);
          service.SaveOnlyPicture(newObjectId);
        }
      }
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
    this._sketchObject = (ISketchObject) null;
    return base.AcceptDialog(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
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
    ITechAcadService service = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, true);
    service.OpenPicture(newObjectId);
    if (this._sketchObject != null)
      Intermech.TechAcad.Connector.TechAcad.ShowOper(this._sketchObject.DraftObject.Extract(0), this._sketchObject.SketchID, this._sketchObject.Name);
    service.ShowAcadWindow(WindowMode.Restore);
    return true;
  }
}
