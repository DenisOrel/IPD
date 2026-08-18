// Decompiled with JetBrains decompiler
// Type: Intermech.Document.DBCore.ReferenceToDBObjectAttributeCore
// Assembly: Intermech.Document.DBCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CF4D99-832B-4258-9FE1-B244E517D790
// Assembly location: D:\IPS\Client\Intermech.Document.DBCore.dll

using Intermech.Checksums;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Document.DBCore;

[Serializable]
public class ReferenceToDBObjectAttributeCore : ReferenceToDBObjectAttributeBase
{
  private readonly object syncRoot = new object();

  public new static object EmptyConstructor() => (object) new ReferenceToDBObjectAttributeCore();

  public new static object EmptyConstructorActiveLink()
  {
    ReferenceToDBObjectAttributeCore objectAttributeCore = new ReferenceToDBObjectAttributeCore();
    objectAttributeCore.passiveLink = false;
    return (object) objectAttributeCore;
  }

  public ReferenceToDBObjectAttributeCore()
  {
  }

  public ReferenceToDBObjectAttributeCore(DocumentTreeNode ownerNode, bool passiveLink)
    : base(ownerNode, passiveLink)
  {
  }

  public ReferenceToDBObjectAttributeCore(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    Guid attrGuid,
    int attrID,
    string attrName,
    bool passiveLink)
    : base(ownerNode, refType, dbObjectInfo, attrGuid, attrID, attrName, passiveLink)
  {
  }

  public virtual void UpdateAttributeValue(
    IUserSession session,
    bool forceUpdate,
    bool updateFromDB,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsSuspendedUpdatesFromDB || !forceUpdate && this.PassiveLink)
      return;
    try
    {
      if (!this.IsConnected)
        return;
      string attributeValue = this.attributeValue;
      if (this.UpdateAttributeValueAtrrPocessor())
      {
        updateFromDB = false;
      }
      else
      {
        LogManager.AddLine("ReferenceToDBObjectAttribute.UpdateAttributeValue(forceUpdate, bool updateFromDB, bool updateUI, bool updateLayout)");
        AttributeValues[] attributesValues;
        if (this.IsRelationAttribute)
          attributesValues = session.GetRelationAttributesValues(this.DBRelationID, new int[1]
          {
            this.attributeID
          }, GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions, false);
        else
          attributesValues = session.GetObjectAttributesValues(this.DBObjectID, new int[1]
          {
            this.attributeID
          }, GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions, false);
        AttributeValues attributeValues = attributesValues != null ? ((IEnumerable<AttributeValues>) attributesValues).FirstOrDefault<AttributeValues>() : (AttributeValues) null;
        if (attributeValues != null)
        {
          if (attributeValues.AttributeType == FieldTypes.ftObjectLink || attributeValues.AttributeType == FieldTypes.ftObjectLinkByID)
            this.attributeLinkObjectID = attributeValues.ConvertToInt64();
          this.attributeValue = this.GetAttributeValue(attributeValues);
          this.readOnlyAttr = new bool?(attributeValues.ReadOnly);
        }
        else
          this.attributeValue = (string) null;
      }
      if (!(this.attributeValue != attributeValue))
        return;
      this.OnTextChanged(attributeValue, this.attributeValue, true, !this.PassiveLink, updateUI, updateLayout);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  protected bool GetCustomAttributeValue(AttributeValues attributeValues, out string attributeValue)
  {
    if (this.AttributeGuid == new Guid("cad014af-306c-11d8-b4e9-00304f19f545"))
    {
      attributeValue = new ChecksumClass(ChecksumAlgorithm.Crc32, attributeValues.Value).ToString();
      return true;
    }
    if (this.OwnerNode is TextData ownerNode && !string.IsNullOrWhiteSpace(ownerNode.TextFormat))
    {
      if (ownerNode.UseTextFormatForRefs)
      {
        try
        {
          if (attributeValues.AttributeType == FieldTypes.ftDateTime)
          {
            ref string local1 = ref attributeValue;
            DateTime? nullable = attributeValues.Value as DateTime?;
            ref DateTime? local2 = ref nullable;
            string str = local2.HasValue ? local2.GetValueOrDefault().ToString(ownerNode.TextFormat.Trim()) : (string) null;
            local1 = str;
            return attributeValue != null;
          }
          if (attributeValues.AttributeType == FieldTypes.ftDouble)
          {
            ref string local3 = ref attributeValue;
            double? nullable = attributeValues.Value as double?;
            ref double? local4 = ref nullable;
            string str = local4.HasValue ? local4.GetValueOrDefault().ToString(ownerNode.TextFormat.Trim()) : (string) null;
            local3 = str;
            return attributeValue != null;
          }
          if (attributeValues.AttributeType == FieldTypes.ftInteger)
          {
            ref string local5 = ref attributeValue;
            long? nullable = attributeValues.Value as long?;
            ref long? local6 = ref nullable;
            string str = local6.HasValue ? local6.GetValueOrDefault().ToString(ownerNode.TextFormat.Trim()) : (string) null;
            local5 = str;
            return attributeValue != null;
          }
        }
        catch
        {
          attributeValue = (string) null;
          return false;
        }
      }
    }
    attributeValue = (string) null;
    return false;
  }

  protected string GetAttributeValue(AttributeValueCache cache)
  {
    object obj = cache.Value;
    return !(obj is DateTime) ? Convert.ToString(obj) : (this.AttributeID <= 0 ? Convert.ToString(obj) : ((DateTime) obj).ToShortDateString());
  }

  protected string GetAttributeValue(AttributeValues attributeValues)
  {
    string attributeValue = (string) null;
    if (!this.GetCustomAttributeValue(attributeValues, out attributeValue))
    {
      if (attributeValues.AttributeType == FieldTypes.ftDateTime)
      {
        object obj = attributeValues.Value;
        if (obj is DateTime)
          attributeValue = attributeValues.AttributeID <= 0 ? ((DateTime) obj).ToString() : ((DateTime) obj).ToShortDateString();
      }
      if (attributeValues.AttributeType == FieldTypes.ftString || attributeValues.AttributeType == FieldTypes.ftMemo)
        attributeValue = attributeValues.AsString;
      if (attributeValue == null)
      {
        if (attributeValues.Descriptions != null)
        {
          object[] descriptions = attributeValues.Descriptions;
          attributeValue = Convert.ToString(descriptions != null ? ((IEnumerable<object>) descriptions).FirstOrDefault<object>() : (object) null);
        }
        else
          attributeValue = attributeValues.AsString;
      }
    }
    return attributeValue;
  }

  protected virtual bool UpdateAttributeValueAtrrPocessor() => false;

  public override bool CanUpdateReference(UpdateReferencesMode mode)
  {
    return mode.HasFlag((Enum) UpdateReferencesMode.Attributes);
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

  public void UpdateLink(
    IUserSession userSession,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> relAttrCache,
    bool forceUpdate,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsSuspendedUpdatesFromDB || !forceUpdate && this.PassiveLink || this.OwnerDocument != null && !this.CanUpdateReference(this.OwnerDocument.UpdateReferencesMode))
      return;
    try
    {
      this.GetParentDBObjectInfo(userSession);
      if (this.IsEmptyObjectRef)
        return;
      this.UpdateDBObjectInfo(userSession);
      Dictionary<Guid, AttributeValueCache> dictionary = (Dictionary<Guid, AttributeValueCache>) null;
      AttributeValueCache cache;
      if (this.IsConnectedObjectRef && this.AttributeGuid != Guid.Empty)
      {
        if (!this.IsReferenceToRelation && objAttrCache != null)
          objAttrCache.TryGetValue(this.DBObjectGuid, out dictionary);
        else if (this.IsReferenceToRelation && relAttrCache != null)
          relAttrCache.TryGetValue(this.DBRelationGuid, out dictionary);
        if (dictionary != null && dictionary.TryGetValue(this.AttributeGuid, out cache))
        {
          string attributeValue = this.attributeValue;
          this.attributeValue = (string) null;
          if (cache != null && cache.HasValue)
          {
            if (this.OwnerNode != null && !cache.ReferenceOwnerList.Contains((INodeWithReference) this.OwnerNode))
              cache.ReferenceOwnerList.Add((INodeWithReference) this.OwnerNode);
            if (cache.Value != null)
              this.attributeValue = this.GetAttributeValue(cache);
            if (!(this.attributeValue != attributeValue))
              return;
            this.OnTextChanged(attributeValue, this.attributeValue, true, !this.PassiveLink, updateUI, updateLayout);
            return;
          }
        }
      }
      this.UpdateAttributeInfo();
      this.UpdateAttributeValue(userSession, false, true, updateUI, updateLayout);
      if (!this.IsReferenceToRelation && objAttrCache != null)
      {
        lock (objAttrCache)
        {
          if (!objAttrCache.TryGetValue(this.DBObjectGuid, out dictionary))
          {
            dictionary = new Dictionary<Guid, AttributeValueCache>();
            objAttrCache.Add(this.DBObjectGuid, dictionary);
          }
        }
      }
      else if (this.IsReferenceToRelation && relAttrCache != null)
      {
        lock (relAttrCache)
        {
          if (!relAttrCache.TryGetValue(this.DBRelationGuid, out dictionary))
          {
            dictionary = new Dictionary<Guid, AttributeValueCache>();
            relAttrCache.Add(this.DBRelationGuid, dictionary);
          }
        }
      }
      if (dictionary == null)
        return;
      lock (this.syncRoot)
      {
        if (!dictionary.TryGetValue(this.AttributeGuid, out cache))
        {
          dictionary.Add(this.AttributeGuid, new AttributeValueCache((object) this.attributeValue, (INodeWithReference) this.OwnerNode));
        }
        else
        {
          if (this.OwnerNode != null && !cache.ReferenceOwnerList.Contains((INodeWithReference) this.OwnerNode))
            cache.ReferenceOwnerList.Add((INodeWithReference) this.OwnerNode);
          cache.Value = (object) this.attributeValue;
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public override void GetParentDBObjectInfo(IUserSession session, DocumentTreeNode owner)
  {
    if (this.refType == RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute)
    {
      this.dbObjectInfo = (DBObjectInfoBase) null;
      int attributeID = this.linkAttributeID;
      if (this.linkAttributeID == -1)
      {
        if (this.linkAttributeGuid != Guid.Empty)
          this.linkAttributeID = attributeID = MetaDataHelper.GetAttributeTypeID(this.linkAttributeGuid);
        else if (!string.IsNullOrEmpty(this.LinkAttributeName))
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(MetaDataHelper.GetAttributeByTypeNameID(this.LinkAttributeName));
          if (attributeType != null && attributeType.FieldType == FieldTypes.ftObjectLink)
          {
            this.linkAttributeID = attributeID = attributeType.AttributeID;
            this.linkAttributeGuid = MetaDataHelper.GetAttributeTypeGuid(this.linkAttributeID);
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
            long? id = attributeValueCache.Id;
            if (id.HasValue)
            {
              id = attributeValueCache.Id;
              long num = id.Value;
              if (num.IsDefinedId())
              {
                Intermech.Interfaces.Document.DBObjectInfo dbObjectInfo = (Intermech.Interfaces.Document.DBObjectInfo) null;
                if (this.OwnerDocument.ObjectsInfoId.TryGetValue(num, out dbObjectInfo) && dbObjectInfo != null)
                {
                  this.dbObjectInfo = (DBObjectInfoBase) dbObjectInfo;
                  flag = true;
                }
              }
            }
          }
        }
      }
      if (!flag)
      {
        IDBObject documentDbObject = ReferenceToDBObjectCore.GetOwnerDocumentDBObject(owner, session, (string) null);
        if (documentDbObject != null)
        {
          IDBAttribute attributeById = documentDbObject.GetAttributeByID(attributeID);
          long? nullable = new long?();
          if (attributeById != null && attributeById.DataType == FieldTypes.ftObjectLink && attributeById.Value != DBNull.Value)
          {
            nullable = new long?(attributeById.AsInteger);
            QuickObjectInfo objectInfo = session.GetObjectInfo(nullable.Value);
            this.dbObjectInfo = (DBObjectInfoBase) new Intermech.Interfaces.Document.DBObjectInfo(objectInfo.VersionGuid, nullable.Value, objectInfo.ObjectTypeID, objectInfo.Caption);
          }
        }
      }
      if (this.OwnerDocument == null || this.OwnerDocument.ObjAttrCache == null)
        return;
      long? id1 = new long?();
      if (this.dbObjectInfo != null)
        id1 = new long?(this.dbObjectInfo.ObjectID);
      Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache1 = this.OwnerDocument.ObjAttrCache;
      Dictionary<Guid, AttributeValueCache> dictionary1 = (Dictionary<Guid, AttributeValueCache>) null;
      lock (objAttrCache1)
      {
        if (!objAttrCache1.TryGetValue(this.OwnerDocument.DBObjectGuid, out dictionary1))
        {
          dictionary1 = new Dictionary<Guid, AttributeValueCache>();
          objAttrCache1.Add(this.OwnerDocument.DBObjectGuid, dictionary1);
        }
        AttributeValueCache attributeValueCache = (AttributeValueCache) null;
        if (!dictionary1.TryGetValue(this.linkAttributeGuid, out attributeValueCache))
        {
          dictionary1.Add(this.linkAttributeGuid, new AttributeValueCache((INodeWithReference) this.OwnerNode, id1));
        }
        else
        {
          if (this.OwnerNode != null && !attributeValueCache.ReferenceOwnerList.Contains((INodeWithReference) this.OwnerNode))
            attributeValueCache.ReferenceOwnerList.Add((INodeWithReference) this.OwnerNode);
          attributeValueCache.Id = id1;
        }
      }
    }
    else
      base.GetParentDBObjectInfo(session, owner);
  }

  public override void DisconnectLink()
  {
    if (this.IsConnected && this.OwnerNode != null && this.OwnerDocument != null)
    {
      Dictionary<Guid, AttributeValueCache> dictionary = (Dictionary<Guid, AttributeValueCache>) null;
      if (!this.IsReferenceToRelation)
        this.OwnerDocument.ObjAttrCache.TryGetValue(this.DBObjectGuid, out dictionary);
      else
        this.OwnerDocument.RelAttrCache.TryGetValue(this.DBRelationGuid, out dictionary);
      AttributeValueCache attributeValueCache;
      if (dictionary != null && dictionary.TryGetValue(this.AttributeGuid, out attributeValueCache) && attributeValueCache != null)
      {
        int index = attributeValueCache.ReferenceOwnerList.IndexOf((INodeWithReference) this.OwnerNode);
        if (index != -1)
          attributeValueCache.ReferenceOwnerList.RemoveAt(index);
        if (attributeValueCache.ReferenceOwnerList.Count == 0)
        {
          if (!this.IsReferenceToRelation)
          {
            lock (this.OwnerDocument.ObjAttrCache)
              this.OwnerDocument.ObjAttrCache.Remove(this.DBObjectGuid);
          }
          else
          {
            lock (this.OwnerDocument.RelAttrCache)
              this.OwnerDocument.RelAttrCache.Remove(this.DBRelationGuid);
          }
        }
      }
    }
    base.DisconnectLink();
  }

  protected override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    if (this.attributeID != -1 && this.attributeGuid == Guid.Empty && string.IsNullOrEmpty(this.attributeName))
      this.UpdateAttributeInfo();
    base.WriteXmlAttributes(xw, objectRefId);
  }

  public virtual IMSAttributeType GetAttributeType()
  {
    if (this.attributeID != -1)
      return MetaDataHelper.GetAttributeType(this.attributeID);
    if (this.attributeGuid != Guid.Empty)
      return MetaDataHelper.GetAttributeType(this.attributeGuid);
    return !string.IsNullOrEmpty(this.attributeName) ? MetaDataHelper.GetAttributeType(MetaDataHelper.GetAttributeByTypeNameID(this.attributeName)) : (IMSAttributeType) null;
  }

  public override void UpdateAttributeInfo()
  {
    if (this.AttributeID == -1 && this.AttributeGuid == Guid.Empty && Convert.ToString(this.AttributeName) == string.Empty)
      return;
    this.UpdateAttributeInfo(this.GetAttributeType());
  }

  public virtual void UpdateAttributeInfo(IMSAttributeType attrType)
  {
    if (attrType == null)
      return;
    this.attributeID = attrType.AttributeID;
    this.attributeGuid = attrType.AttributeGuid;
    this.attributeName = attrType.Name;
    if (attrType.FieldType != FieldTypes.ftObjectLink || !this.attributeLinkObjectID.IsUndefinedId() || this.dbObjectInfo == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute(this.dbObjectInfo.ObjectID, (object) this.attributeID, false, false);
      if (objectAttribute == null)
        return;
      this.attributeLinkObjectID = objectAttribute.ConvertToInt64();
    }
  }

  [Browsable(false)]
  public override bool UseLinkAttribute
  {
    [DebuggerStepThrough] get
    {
      return this.refType == RefToDBObjectType.rtUseLinkFromDocumentObjectAttribute;
    }
  }

  public override bool IsUpdateDBObjectInfoBatch
  {
    get
    {
      return !(this.AttributeGuid == new Guid("cad014af-306c-11d8-b4e9-00304f19f545")) && base.IsUpdateDBObjectInfoBatch;
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

  public void UpdateCachedValue()
  {
    Dictionary<Guid, AttributeValueCache> dictionary = (Dictionary<Guid, AttributeValueCache>) null;
    ImDocumentData ownerDocument = this.OwnerDocument;
    if (this.IsRelationAttribute)
    {
      if (this.DBRelationGuid != Guid.Empty && ownerDocument != null)
        ownerDocument.RelAttrCache.TryGetValue(this.DBRelationGuid, out dictionary);
    }
    else if (this.DBObjectGuid != Guid.Empty && ownerDocument != null)
      ownerDocument.ObjAttrCache.TryGetValue(this.DBObjectGuid, out dictionary);
    if (dictionary == null)
      return;
    AttributeValueCache attributeValueCache = (AttributeValueCache) null;
    dictionary.TryGetValue(this.AttributeGuid, out attributeValueCache);
    if (attributeValueCache == null || !attributeValueCache.HasValue || !(Convert.ToString(attributeValueCache.Value) != this.attributeValue))
      return;
    attributeValueCache.Value = (object) this.attributeValue;
    for (int index = 0; index < attributeValueCache.ReferenceOwnerList.Count; ++index)
    {
      if (attributeValueCache.ReferenceOwnerList[index].Reference != null && attributeValueCache.ReferenceOwnerList[index].Reference is ReferenceToDBObjectAttributeCore reference && reference != this)
        reference.SetText(this.attributeValue, false, false, false);
    }
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
      if (this.OwnerDocument != null && this.OwnerDocument.ObjectsInfoGuid.TryGetValue(this.DBObjectGuid, out dbObjectInfo))
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
