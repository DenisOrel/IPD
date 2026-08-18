// Decompiled with JetBrains decompiler
// Type: Intermech.Document.DBCore.ReferenceToSignCore
// Assembly: Intermech.Document.DBCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CF4D99-832B-4258-9FE1-B244E517D790
// Assembly location: D:\IPS\Client\Intermech.Document.DBCore.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Signs.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.Document.DBCore;

[Serializable]
public class ReferenceToSignCore : ReferenceToSignBase
{
  public new static object EmptyConstructor() => (object) new ReferenceToSignCore();

  public new static object EmptyConstructorActiveLink()
  {
    ReferenceToSignCore referenceToSignCore = new ReferenceToSignCore();
    referenceToSignCore.passiveLink = false;
    return (object) referenceToSignCore;
  }

  public ReferenceToSignCore()
  {
  }

  public ReferenceToSignCore(DocumentTreeNode ownerNode, bool passiveLink)
    : base(ownerNode, passiveLink)
  {
  }

  public ReferenceToSignCore(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    string attrName,
    bool passiveLink)
    : base(ownerNode, refType, dbObjectInfo, attrName, passiveLink)
  {
  }

  protected virtual bool HasDocumentControl() => false;

  protected virtual DocumentViewMode GetDocumentViewMode() => DocumentViewMode.Normal;

  public override bool CanUpdateReference(UpdateReferencesMode mode)
  {
    return mode.HasFlag((Enum) UpdateReferencesMode.Signes);
  }

  public override void UpdateLink(
    object userSession,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> relAttrCache,
    bool forceUpdate,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsSuspendedUpdatesFromDB || this.OwnerDocument != null && !this.CanUpdateReference(this.OwnerDocument.UpdateReferencesMode))
      return;
    if (userSession is IUserSession session)
    {
      this.GetParentDBObjectInfo(session);
      if (this.IsEmptyObjectRef)
        return;
      this.UpdateDBObjectInfo(session);
      ObjectModifyModes? nullable = new ObjectModifyModes?();
      if (this.OwnerDocument != null)
        nullable = this.OwnerDocument.DBObjectModifyMode;
      if (!SignsHolder.SignOutputEnabledDevelop && nullable.HasValue && (nullable.Value == ObjectModifyModes.InBase || nullable.Value == ObjectModifyModes.Checkout))
      {
        this.attributeValue = "";
      }
      else
      {
        string attributeValue = this.attributeValue;
        this.attributeValue = (string) null;
        if (this.DBObjectID != -1L)
        {
          if (this.HasDocumentControl())
            this.attributeValue = "";
          if (this.OwnerDocument != null)
          {
            if (!this.OwnerDocument.Signes.ContainsKey(this.DBObjectID) && session.GetCustomService(typeof (ISignsService)) is ISignsService customService)
              this.OwnerDocument.Signes[this.DBObjectID] = new ArrayList((ICollection) customService.GetObjectSignsParams(this.DBObjectID, session.SessionGUID, true));
            if (this.OwnerDocument.Signes.ContainsKey(this.DBObjectID))
            {
              List<SignParams> signParamsList = new List<SignParams>();
              foreach (SignParams signParams in this.OwnerDocument.Signes[this.DBObjectID])
                signParamsList.Add(signParams);
              List<SignParams> list = signParamsList.FindAll((Predicate<SignParams>) (x => x.GraphName == this.SignField)).OrderByDescending<SignParams, DateTime>((Func<SignParams, DateTime>) (x => x.SignDate)).ToList<SignParams>();
              if (list.Count > 0)
              {
                SignParams signParams = list[0];
                if (this.GetDocumentViewMode().HasFlag((Enum) DocumentViewMode.ShowSigns))
                {
                  if (this.AttributeName == "Фамилия подписавшего")
                    this.attributeValue = signParams.Surname;
                  if (!this.GetDocumentViewMode().HasFlag((Enum) DocumentViewMode.ShowOnlySignName))
                  {
                    if (this.AttributeName == "Значение подписи")
                      this.attributeValue = signParams.SignValue;
                    if (this.AttributeName == "Дата подписи")
                      this.attributeValue = signParams.SignDateAsFormattedString;
                    if (this.AttributeName == "Должность")
                      this.attributeValue = signParams.Rank;
                    if (this.AttributeName == "Наименование графы")
                      this.attributeValue = signParams.GraphName;
                  }
                }
              }
            }
          }
        }
        if (!(this.attributeValue != attributeValue))
          return;
        this.OnTextChanged(attributeValue, this.attributeValue, true, !this.PassiveLink, updateUI, updateLayout);
      }
    }
    else
      this.UpdateLink(forceUpdate, updateUI, updateLayout);
  }

  public override void GetParentDBObjectInfo(IUserSession session, DocumentTreeNode owner)
  {
    if (this.refType == RefToDBObjectType.rtUseLinkFromDocumentObjectSign)
    {
      this.dbObjectInfo = (DBObjectInfoBase) null;
      int attributeID = this.linkAttributeID;
      if (this.linkAttributeID == -1)
      {
        if (this.linkAttributeGuid != Guid.Empty)
          this.linkAttributeID = attributeID = MetaDataHelper.GetAttributeTypeID(this.linkAttributeGuid);
        else if (this.LinkAttributeName != null && this.LinkAttributeName != "")
        {
          IDBAttributeType attributeType = session.GetAttributeType(this.LinkAttributeName, false);
          if (attributeType != null && attributeType.AttributeType == FieldTypes.ftObjectLink)
          {
            this.linkAttributeID = attributeID = attributeType.AttributeID;
            this.linkAttributeGuid = attributeType.GUID;
          }
        }
      }
      if (attributeID < 0)
        return;
      bool flag = false;
      if (this.OwnerDocument != null && this.OwnerDocument.ObjAttrCache != null)
      {
        Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache = this.OwnerDocument.ObjAttrCache;
        Dictionary<Guid, AttributeValueCache> dictionary = (Dictionary<Guid, AttributeValueCache>) null;
        Guid dbObjectGuid = this.OwnerDocument.DBObjectGuid;
        ref Dictionary<Guid, AttributeValueCache> local = ref dictionary;
        if (objAttrCache.TryGetValue(dbObjectGuid, out local))
        {
          AttributeValueCache attributeValueCache = (AttributeValueCache) null;
          if (dictionary.TryGetValue(this.linkAttributeGuid, out attributeValueCache))
          {
            try
            {
              long? id = attributeValueCache.Id;
              if (id.HasValue)
              {
                id = attributeValueCache.Id;
                long num = id.Value;
                if (num.IsDefinedId())
                {
                  Intermech.Interfaces.Document.DBObjectInfo dbObjectInfo = (Intermech.Interfaces.Document.DBObjectInfo) null;
                  if (this.OwnerDocument.ObjectsInfoId.TryGetValue(num, out dbObjectInfo))
                  {
                    if (dbObjectInfo != null)
                    {
                      this.dbObjectInfo = (DBObjectInfoBase) dbObjectInfo;
                      flag = true;
                    }
                  }
                }
              }
            }
            catch (FormatException ex)
            {
            }
          }
        }
      }
      if (flag)
        return;
      IDBObject documentDbObject = ReferenceToDBObjectCore.GetOwnerDocumentDBObject(owner, session, (string) null);
      if (documentDbObject == null)
        return;
      long? nullable = new long?();
      IDBAttribute attributeById = documentDbObject.GetAttributeByID(attributeID);
      if (attributeById != null && attributeById.DataType == FieldTypes.ftObjectLink && attributeById.Value != DBNull.Value)
      {
        nullable = new long?(attributeById.AsInteger);
        IDBObject objectActual = session.GetObjectActual(nullable.Value, false);
        if (objectActual != null)
          nullable = new long?(objectActual.ObjectID);
        QuickObjectInfo objectInfo = session.GetObjectInfo(nullable.Value);
        this.dbObjectInfo = (DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo(objectInfo.VersionGuid, nullable.Value, objectInfo.ObjectTypeID, objectInfo.Caption);
      }
      if (this.OwnerDocument == null || this.OwnerDocument.ObjAttrCache == null || !nullable.HasValue)
        return;
      Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache1 = this.OwnerDocument.ObjAttrCache;
      lock (objAttrCache1)
      {
        Dictionary<Guid, AttributeValueCache> dictionary;
        if (!objAttrCache1.TryGetValue(documentDbObject.ObjectGUID, out dictionary))
        {
          dictionary = new Dictionary<Guid, AttributeValueCache>();
          objAttrCache1.Add(documentDbObject.ObjectGUID, dictionary);
        }
        AttributeValueCache attributeValueCache = (AttributeValueCache) null;
        if (!dictionary.TryGetValue(this.linkAttributeGuid, out attributeValueCache))
        {
          dictionary.Add(this.linkAttributeGuid, new AttributeValueCache((INodeWithReference) this.OwnerNode, new long?(nullable.Value)));
        }
        else
        {
          if (this.OwnerNode != null && !attributeValueCache.ReferenceOwnerList.Contains((INodeWithReference) this.OwnerNode))
            attributeValueCache.ReferenceOwnerList.Add((INodeWithReference) this.OwnerNode);
          attributeValueCache.Id = new long?(nullable.Value);
        }
      }
    }
    else
      base.GetParentDBObjectInfo(session, owner);
  }

  public override List<string> GetSignFieldsList()
  {
    List<string> signFieldsList = new List<string>();
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(SignsHolder.GraphAttrTypeGuid);
    if (attributeType != null)
    {
      foreach (object valuesDescription in attributeType.PossibleValuesDescriptions)
      {
        if (valuesDescription is string)
          signFieldsList.Add((string) valuesDescription);
      }
    }
    return signFieldsList;
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

  public void UpdateCachedValue()
  {
  }

  [Browsable(false)]
  public override bool UseLinkAttribute
  {
    [DebuggerStepThrough] get => this.refType == RefToDBObjectType.rtUseLinkFromDocumentObjectSign;
  }

  public override void SetText(
    string value,
    bool saveToDB,
    bool fireTextChanged,
    bool updateOwner,
    bool updateUI,
    bool updateLayout)
  {
    if (!(this.attributeValue != value))
      return;
    string attributeValue = this.attributeValue;
    this.attributeValue = value;
    if (!fireTextChanged)
      return;
    this.OnTextChanged(attributeValue, this.attributeValue, updateOwner, !this.PassiveLink, updateUI, updateLayout);
  }

  public virtual IDBRelation GetDBRelation(
    IUserSession session,
    out IDBObject dbObject,
    string filtrationSettings = null)
  {
    return ReferenceToDBObjectCore.GetDBRelation(session, out dbObject, (ReferenceToDBObjectBase) this, filtrationSettings);
  }

  public virtual IDBObject GetDBObject(IUserSession session, string filtrationSettings = null)
  {
    return ReferenceToDBObjectCore.GetDBObject(session, (ReferenceToDBObjectBase) this, filtrationSettings);
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
      if (this.IsEmptyObjectRef)
        this.GetParentDBObjectInfo(session);
      if (this.IsConnectedObjectRef)
        return;
      this.GetDBRelation(session, out dbObject, filtrationSettings);
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
}
