// Decompiled with JetBrains decompiler
// Type: Intermech.Document.DBCore.ReferenceToDBObjectCore
// Assembly: Intermech.Document.DBCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CF4D99-832B-4258-9FE1-B244E517D790
// Assembly location: D:\IPS\Client\Intermech.Document.DBCore.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.DBCore;

[Serializable]
public class ReferenceToDBObjectCore : ReferenceToDBObjectBase
{
  public new static object EmptyConstructor() => (object) new ReferenceToDBObjectCore();

  public new static object EmptyConstructorActiveLink()
  {
    ReferenceToDBObjectCore referenceToDbObjectCore = new ReferenceToDBObjectCore();
    referenceToDbObjectCore.passiveLink = false;
    return (object) referenceToDbObjectCore;
  }

  public ReferenceToDBObjectCore()
  {
  }

  public ReferenceToDBObjectCore(DocumentTreeNode ownerNode, bool passiveLink)
    : base(ownerNode, passiveLink)
  {
  }

  public ReferenceToDBObjectCore(DocumentTreeNode ownerNode, IDBObject dbObject, bool passiveLink)
    : base(ownerNode, passiveLink)
  {
    Guid objectGuid = Guid.Empty;
    long objectID = -1;
    int objectType = -1;
    string objectCaption = (string) null;
    if (dbObject != null)
    {
      objectGuid = dbObject.ObjectGUID;
      objectID = dbObject.ObjectID;
      objectType = dbObject.ObjectType;
      objectCaption = dbObject.Caption;
    }
    this.dbObjectInfo = (DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo(objectGuid, objectID, objectType, objectCaption);
    this.refType = RefToDBObjectType.rtSelectedObject;
  }

  public ReferenceToDBObjectCore(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    bool passiveLink)
    : base(ownerNode, refType, dbObjectInfo, passiveLink)
  {
  }

  public ReferenceToDBObjectCore(
    RefToDBObjectType refType,
    Guid objectVersionGuid,
    bool passiveLink)
    : base((DocumentTreeNode) null, refType, (DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo(objectVersionGuid), passiveLink)
  {
  }

  public ReferenceToDBObjectCore(
    RefToDBObjectType refType,
    Guid relationGuid,
    Guid objectVersionGuid,
    bool passiveLink)
    : base((DocumentTreeNode) null, refType, (DBObjectInfoBase) new DBRelationInfo(relationGuid, objectVersionGuid), passiveLink)
  {
  }

  [Browsable(false)]
  public override bool UseLinkAttribute
  {
    [DebuggerStepThrough] get
    {
      return this.refType == RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute;
    }
  }

  public override string LinkAttributeName
  {
    get
    {
      if (this.linkAttributeName != null && this.linkAttributeName != "")
        return this.linkAttributeName;
      if (this.linkAttributeID == -1 && this.linkAttributeGuid != Guid.Empty)
        this.linkAttributeID = MetaDataHelper.GetAttributeTypeID(this.linkAttributeGuid);
      return this.linkAttributeID != -1 ? (this.linkAttributeName = MetaDataHelper.GetAttributeTypeName(this.linkAttributeID)) : "";
    }
    set => this.linkAttributeName = value;
  }

  public override void UpdateLink(
    object userSession,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> relAttrCache,
    bool forceUpdate,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsSuspendedUpdatesFromDB || !forceUpdate && this.PassiveLink)
      return;
    if (userSession is IUserSession userSession1)
      this.UpdateLink(userSession1, objAttrCache, relAttrCache, forceUpdate, updateUI, updateLayout);
    else
      this.UpdateLink(forceUpdate, updateUI, updateLayout);
  }

  public virtual void UpdateLink(
    IUserSession userSession,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> relAttrCache,
    bool forceUpdate,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsSuspendedUpdatesFromDB || !forceUpdate && this.PassiveLink)
      return;
    this.GetParentDBObjectInfo(userSession);
    this.UpdateDBObjectInfo(userSession);
  }

  public static IDBObject GetOwnerDocumentDBObject(
    DocumentTreeNode owner,
    IUserSession session,
    string filtrationSettings)
  {
    IDBObject documentDbObject = (IDBObject) null;
    INodeWithReference nodeWithReference = (INodeWithReference) null;
    if (owner is IDocumentElement documentElement)
      nodeWithReference = (INodeWithReference) documentElement.OwnerDocument;
    if (nodeWithReference != null && nodeWithReference.Reference is ReferenceToDBObjectBase reference)
      documentDbObject = ReferenceToDBObjectCore.GetDBObject(session, reference, (string) null);
    return documentDbObject;
  }

  public override void GetParentDBObjectInfo(IUserSession session, DocumentTreeNode owner)
  {
    if (this.refType == RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute)
    {
      this.dbObjectInfo = (DBObjectInfoBase) null;
      if (this.linkAttributeID == -1 && this.linkAttributeGuid != Guid.Empty)
        this.linkAttributeID = MetaDataHelper.GetAttributeTypeID(this.linkAttributeGuid);
      if (this.linkAttributeID == -1)
        return;
      IDBObject documentDbObject = ReferenceToDBObjectCore.GetOwnerDocumentDBObject(owner, session, (string) null);
      if (documentDbObject == null)
        return;
      IDBAttribute attributeById = documentDbObject.GetAttributeByID(this.linkAttributeID);
      if (attributeById == null || attributeById.DataType != FieldTypes.ftObjectLink || attributeById.Value == DBNull.Value)
        return;
      long asInteger = attributeById.AsInteger;
      QuickObjectInfo objectInfo = session.GetObjectInfo(asInteger);
      this.dbObjectInfo = (DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo(objectInfo.VersionGuid, asInteger, objectInfo.ObjectTypeID, objectInfo.Caption);
    }
    else
      base.GetParentDBObjectInfo(session, owner);
  }

  public virtual IDBRelation GetDBRelation(
    IUserSession session,
    out IDBObject dbObject,
    string filtrationSettings)
  {
    return ReferenceToDBObjectCore.GetDBRelation(session, out dbObject, (ReferenceToDBObjectBase) this, filtrationSettings);
  }

  public static IDBRelation GetDBRelation(
    IUserSession session,
    out IDBObject dbObject,
    ReferenceToDBObjectBase refToDBObj,
    string filtrationSettings)
  {
    IDBRelation dbRelation = (IDBRelation) null;
    dbObject = (IDBObject) null;
    try
    {
      if (refToDBObj.IsReferenceToRelation)
      {
        if (refToDBObj.IsEmptyObjectRef)
          refToDBObj.GetParentDBObjectInfo(session);
        if (refToDBObj.IsEmptyObjectRef)
          return (IDBRelation) null;
        if (refToDBObj.DBRelationID != -1L)
          dbRelation = session.GetRelation(refToDBObj.DBRelationID, false);
        else if (refToDBObj.DBRelationGuid != Guid.Empty)
          dbRelation = session.GetRelation(refToDBObj.DBRelationGuid, false);
        if (dbRelation == null)
        {
          long prjID = refToDBObj.DBProjectID;
          if (prjID == -1L && refToDBObj.DBProjectGuid != Guid.Empty)
          {
            IDBObject dbObject1 = session.GetObject(refToDBObj.DBProjectGuid, false);
            if (dbObject1 != null)
              prjID = dbObject1.ObjectID;
          }
          if (prjID != -1L)
            dbRelation = session.GetRelation(refToDBObj.DBRelationGuid, prjID, false);
        }
        if (dbRelation != null && dbRelation.PartObjectID != 0L && dbRelation.PartObjectID != -1L)
          dbObject = session.GetObjectActual(dbRelation.PartObjectID, false);
        else if (refToDBObj.DBObjectID != -1L)
          dbObject = session.GetObjectActual(refToDBObj.DBObjectID, false);
        else if (refToDBObj.DBObjectGuid != Guid.Empty)
          dbObject = session.GetObject(refToDBObj.DBObjectGuid, false);
        else if (dbRelation != null && filtrationSettings != null)
          dbObject = session.GetObjectByVersionsRule(dbRelation.PartID, filtrationSettings, false);
        ReferenceToDBObjectCore.UpdateDBObjectInfo(dbRelation, dbObject, refToDBObj);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      dbRelation = (IDBRelation) null;
    }
    return dbRelation;
  }

  public virtual IDBObject GetDBObject(IUserSession session, string filtrationSettings)
  {
    return ReferenceToDBObjectCore.GetDBObject(session, (ReferenceToDBObjectBase) this, filtrationSettings);
  }

  public static IDBObject GetDBObject(
    IUserSession session,
    ReferenceToDBObjectBase refToDBObj,
    string filtrationSettings)
  {
    IDBObject dbObject = (IDBObject) null;
    try
    {
      if (refToDBObj.IsEmptyObjectRef)
        refToDBObj.GetParentDBObjectInfo(session);
      if (refToDBObj.IsEmptyObjectRef)
        return (IDBObject) null;
      if (refToDBObj.IsReferenceToRelation && !refToDBObj.IsConnectedObjectRef)
        ReferenceToDBObjectCore.GetDBRelation(session, out dbObject, refToDBObj, filtrationSettings);
      else if (refToDBObj.DBObjectID != -1L && refToDBObj.DBObjectID != 0L)
        dbObject = session.GetObjectActual(refToDBObj.DBObjectID, false);
      else if (refToDBObj.DBObjectGuid != Guid.Empty)
        dbObject = session.GetObject(refToDBObj.DBObjectGuid, false);
      ReferenceToDBObjectCore.UpdateDBObjectInfo((IDBRelation) null, dbObject, refToDBObj);
      if (dbObject != null)
      {
        if (refToDBObj.OwnerNode is ImDocumentData)
          (refToDBObj.OwnerNode as ImDocumentData).DBObjectModifyMode = new ObjectModifyModes?(dbObject.ObjectModifyMode);
      }
    }
    catch (Exception ex)
    {
      dbObject = (IDBObject) null;
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return dbObject;
  }

  public override void UpdateDBObjectInfo(object userSession, string filtrationSettings)
  {
    this.UpdateDBObjectInfo((IUserSession) userSession, filtrationSettings);
  }

  public void UpdateDBObjectInfo(IUserSession session)
  {
    this.UpdateDBObjectInfo(session, (string) null);
  }

  public void UpdateDBObjectInfo(IUserSession session, string filtrationSettings)
  {
    if (this.IsConnectedObjectRef)
      return;
    IDBObject dbObject = (IDBObject) null;
    if (this.IsReferenceToRelation)
    {
      ReferenceToDBObjectCore.GetDBRelation(session, out dbObject, (ReferenceToDBObjectBase) this, filtrationSettings);
    }
    else
    {
      if (this.IsEmptyObjectRef)
        this.GetParentDBObjectInfo(session);
      Intermech.Interfaces.Document.DBObjectInfo dbObjectInfo;
      if (this.OwnerDocument.ObjectsInfoGuid.TryGetValue(this.DBObjectGuid, out dbObjectInfo))
        this.dbObjectInfo = (DBObjectInfoBase) dbObjectInfo;
      else
        this.GetDBObject(session, filtrationSettings);
    }
  }

  public void UpdateDBObjectInfo(IDBRelation dbRelation, IDBObject dbObject)
  {
    ReferenceToDBObjectCore.UpdateDBObjectInfo(dbRelation, dbObject, (ReferenceToDBObjectBase) this);
  }

  public static void UpdateDBObjectInfo(
    IDBRelation dbRelation,
    IDBObject dbObject,
    ReferenceToDBObjectBase refToDBObj)
  {
    Guid objectGuid = Guid.Empty;
    long objectID = -1;
    int objectType = -1;
    string objectCaption = (string) null;
    if (dbObject != null)
    {
      objectGuid = dbObject.ObjectGUID;
      objectID = dbObject.ObjectID;
      objectType = dbObject.ObjectType;
      objectCaption = dbObject.Caption;
    }
    if (refToDBObj.IsReferenceToRelation)
    {
      Guid relationGuid = Guid.Empty;
      long relationID = -1;
      int relationType = -1;
      Guid projGuid = Guid.Empty;
      long num = -1;
      if (dbRelation != null)
      {
        relationGuid = ((IDBGuid) dbRelation).GUID;
        relationID = dbRelation.RelationID;
        relationType = dbRelation.RelationType;
        num = dbRelation.ProjID;
        if (num != -1L)
          projGuid = dbRelation.Session.GetObjectInfo(num).VersionGuid;
      }
      if (refToDBObj.DBObjectInfo == null || !(refToDBObj.DBObjectInfo is DBRelationInfo))
        refToDBObj.AssignDBObjectInfo((DBObjectInfoBase) new DBRelationInfo(relationGuid, relationID, relationType, projGuid, num, objectGuid, objectID, objectType, objectCaption), true);
      else if (dbRelation == null)
      {
        if (refToDBObj.ReferenceType != RefToDBObjectType.rtSelectedRelation)
          refToDBObj.AssignDBObjectInfo((DBObjectInfoBase) null, true);
        else
          refToDBObj.DBObjectInfo.SetDBRelationInfo(refToDBObj.DBObjectInfo.RelationGuid, -1L, -1, refToDBObj.DBObjectInfo.ProjGuid, -1L, objectGuid, objectID, objectType, objectCaption);
      }
      else
        refToDBObj.DBObjectInfo.SetDBRelationInfo(relationGuid, relationID, relationType, projGuid, num, objectGuid, objectID, objectType, objectCaption);
    }
    else if (refToDBObj.DBObjectInfo == null || !(refToDBObj.DBObjectInfo is Intermech.Interfaces.Document.DBObjectInfo))
      refToDBObj.AssignDBObjectInfo((DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo(objectGuid, objectID, objectType, objectCaption), true);
    else if (dbObject == null)
    {
      if (refToDBObj.ReferenceType != RefToDBObjectType.rtSelectedObject)
        refToDBObj.AssignDBObjectInfo((DBObjectInfoBase) null, true);
      else
        refToDBObj.DBObjectInfo.SetDBObjectInfo(refToDBObj.DBObjectInfo.ObjectGuid, -1L, -1, (string) null);
    }
    else
      refToDBObj.DBObjectInfo.SetDBObjectInfo(objectGuid, objectID, objectType, objectCaption);
  }
}
