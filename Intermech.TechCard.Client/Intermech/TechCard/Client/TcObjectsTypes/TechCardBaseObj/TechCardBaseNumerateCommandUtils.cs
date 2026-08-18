// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardBaseNumerateCommandUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechNumeration;
using Intermech.Navigator.Controls;
using Intermech.Remoting.Sponsors;
using Intermech.TechCard.Client.TcNumerationRules;
using Intermech.TechCard.Client.UI.Controls;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>
/// 
/// </summary>
public static class TechCardBaseNumerateCommandUtils
{
  /// <summary>Нумерация объекта</summary>
  /// <param name="objInfoItem">Описание нумеруемого объекта</param>
  /// <param name="relInfoItem">Описание связи нумерумемого объекта</param>
  /// <param name="projInfoItem">Описание родительского объекта</param>
  /// <param name="fixedObjMode">Признак является ли область нумерации фиксированной</param>
  /// <param name="objItemTreeNode">Узел дерева навигатора (если нумерация вызывается для объекта из навигатора)</param>
  public static bool NumerateObject(
    ObjInfoItem objInfoItem,
    RelInfoItem relInfoItem,
    ObjInfoItem projInfoItem = null,
    bool fixedObjMode = false,
    NavigatorTreeNode objItemTreeNode = null)
  {
    if ((TypedInfoItem) objInfoItem == (TypedInfoItem) null)
      throw new ArgumentNullException(nameof (objInfoItem));
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) objInfoItem) || objInfoItem.ObjTypeID == -1)
      return false;
    projInfoItem = projInfoItem ?? new ObjInfoItem(0L);
    TechNumerationRule numRule1 = new TechNumerationRule();
    TechNumerationNode numNode1 = new TechNumerationNode();
    TechNumerationObjectModes objectMode = TechNumerationObjectModes.FirstObj;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      ITechNumerationService customService = (ITechNumerationService) session.GetCustomService(typeof (ITechNumerationService));
      if (customService == null)
        return false;
      ITechNumerationRule numRule2;
      ITechNumerationNode numNode2;
      if (customService.GetNumerationRule(objInfoItem.ObjTypeID, projInfoItem.ObjTypeID, session.SessionGUID, out numRule2, out numNode2))
      {
        numRule1.CopyFrom(numRule2);
        numNode1.CopyFrom(numNode2);
      }
      else
      {
        numRule1.NumerationMethod = TechNumerationMethods.Manual;
        numNode1.AttributeTypeGuid = TechCardConsts.AttributeTypes.ObjectNumAttrGuid;
        numNode1.ObjectTypeGuid = MetaDataHelper.GetObjectTypeGuid(objInfoItem.ObjTypeID);
        if ((TypedInfoItem) relInfoItem != (TypedInfoItem) null)
        {
          int relTypeID = relInfoItem.RelTypeID;
          if (relTypeID == -1)
          {
            IDBRelation relation = sessionKeeper.Session.GetRelation(relInfoItem.RelationID);
            if (relation != null)
              relTypeID = relation.RelationType;
          }
          if (relTypeID != -1)
            numNode1.RelationTypeGuids.Add(MetaDataHelper.GetRelationTypeGuid(relTypeID));
        }
      }
    }
    if (!NumRuleFormDialog.ShowDialog(!fixedObjMode, ref numRule1, ref numNode1, ref objectMode))
      return false;
    IDBTypedObjectID dbTypedObjectId;
    if (numRule1.NumerationArea == TechNumerationAreas.TechProccess && objItemTreeNode != null)
    {
      for (; !MetaDataHelper.IsObjectTypeChildOf(projInfoItem.ObjTypeID, TechCardConsts.ObjectTypes.TechProcBaseID) && objItemTreeNode.Parent != null; projInfoItem = dbTypedObjectId != null ? new ObjInfoItem(dbTypedObjectId.ObjectID, dbTypedObjectId.ObjectType) : new ObjInfoItem(0L))
      {
        objItemTreeNode = objItemTreeNode.Parent;
        if (!TechcardClientControlsUtils.GetObjectInfo(objItemTreeNode.Parent, out dbTypedObjectId, out IDBRelationID _, false))
          break;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session1 = sessionKeeper.Session;
      ITechNumerationService customService = (ITechNumerationService) session1.GetCustomService(typeof (ITechNumerationService));
      if (customService == null)
        return false;
      ITechNumerationSession session2 = customService.CreateSession(sessionKeeper.Session.SessionGUID);
      if (session2 == null)
        return false;
      using (new RemoteLock((object) session2))
      {
        try
        {
          session2.BeginLogging();
          if (!RelInfoItem.IsEmpty(relInfoItem) && numRule1.NumerationArea != TechNumerationAreas.TechProccess)
          {
            if (numRule1.NumerationMethod.Equals((object) TechNumerationMethods.Manual))
              session2.NumerateObject(relInfoItem.RelationID, (ITechNumerationRule) numRule1, (ITechNumerationNode) numNode1, objectMode, session1.SessionGUID);
            else
              session2.NumerateObject(relInfoItem.RelationID, objectMode, session1.SessionGUID, TechNumerationMethods.Manual);
          }
          else if (numRule1.NumerationMethod.Equals((object) TechNumerationMethods.Manual) || numRule1.NumerationArea == TechNumerationAreas.TechProccess)
            session2.NumerateObject(objInfoItem.ObjectID, projInfoItem.ObjectID, (ITechNumerationRule) numRule1, (ITechNumerationNode) numNode1, objectMode, session1.SessionGUID);
          else
            session2.NumerateObject(objInfoItem.ObjectID, projInfoItem.ObjectID, objectMode, session1.SessionGUID, TechNumerationMethods.Manual);
          return true;
        }
        finally
        {
          ITechNumerationLog numerationLog = session2.GetNumerationLog();
          if (numerationLog != null)
          {
            INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
            if (service != null)
            {
              service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", numerationLog.ObjectsLog, true));
              service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", numerationLog.RelationsLog));
            }
          }
          customService.DisposeSession(sessionKeeper.Session.SessionGUID);
        }
      }
    }
  }
}
