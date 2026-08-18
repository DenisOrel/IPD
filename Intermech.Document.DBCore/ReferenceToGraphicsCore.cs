// Decompiled with JetBrains decompiler
// Type: Intermech.Document.DBCore.ReferenceToGraphicsCore
// Assembly: Intermech.Document.DBCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CF4D99-832B-4258-9FE1-B244E517D790
// Assembly location: D:\IPS\Client\Intermech.Document.DBCore.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Document.DBCore;

[Serializable]
public class ReferenceToGraphicsCore : ReferenceToGraphicsBase
{
  public new static object EmptyConstructor() => (object) new ReferenceToGraphicsCore();

  public new static object EmptyConstructorActiveLink()
  {
    ReferenceToGraphicsCore referenceToGraphicsCore = new ReferenceToGraphicsCore();
    referenceToGraphicsCore.passiveLink = false;
    return (object) referenceToGraphicsCore;
  }

  public ReferenceToGraphicsCore()
  {
  }

  public ReferenceToGraphicsCore(
    DocumentTreeNode ownerNode,
    RefToDBObjectType refType,
    DBObjectInfoBase dbObjectInfo,
    Guid fileAttrGuid,
    string attributeName,
    string fileName,
    List<string> layers,
    bool passiveLink)
    : base(ownerNode, refType, dbObjectInfo, fileAttrGuid, attributeName, fileName, layers, passiveLink)
  {
  }

  public ReferenceToGraphicsCore(
    DocumentTreeNode ownerNode,
    Guid dbObjectGuid,
    List<string> layers,
    bool passiveLink)
    : base(ownerNode, dbObjectGuid, layers, passiveLink)
  {
  }

  public ReferenceToGraphicsCore(Guid dbObjectGuid, List<string> layers, bool passiveLink)
    : base(dbObjectGuid, layers, passiveLink)
  {
  }

  public ReferenceToGraphicsCore(Guid dbObjectGuid, bool passiveLink)
    : base(dbObjectGuid, passiveLink)
  {
  }

  public void AssignAttributeInfo(int attrID, string fileName, List<string> layers)
  {
    if (attrID != -1)
    {
      this.fileAttrID = attrID;
      this.fileAttrGuid = Guid.Empty;
      this.UpdateAttributeInfo();
    }
    else
    {
      this.fileAttrID = -1;
      this.fileAttrGuid = Guid.Empty;
    }
    this.fileName = fileName;
    this.layers = layers;
    if (this.OwnerNode == null)
      return;
    this.OwnerNode.overrideFlags2 |= OverrideFlags2.Reference;
  }

  public virtual IMSAttributeType GetAttributeType()
  {
    if (this.fileAttrID != -1)
      return MetaDataHelper.GetAttributeType(this.fileAttrID);
    if (this.fileAttrGuid != Guid.Empty)
      return MetaDataHelper.GetAttributeType(this.fileAttrGuid);
    return !string.IsNullOrEmpty(this.attributeName) ? MetaDataHelper.GetAttributeType(MetaDataHelper.GetAttributeByTypeNameID(this.attributeName)) : (IMSAttributeType) null;
  }

  public override void UpdateAttributeInfo()
  {
    if (this.fileAttrID == -1 && this.fileAttrGuid == Guid.Empty && Convert.ToString(this.AttributeName) == string.Empty)
      return;
    this.UpdateAttributeInfo(this.GetAttributeType());
  }

  public virtual void UpdateAttributeInfo(IMSAttributeType attrType)
  {
    if (attrType == null)
      return;
    this.fileAttrID = attrType.AttributeID;
    this.fileAttrGuid = attrType.AttributeGuid;
    this.attributeName = attrType.Name;
  }

  public override Stream GetGraphicsStream()
  {
    if (!this.CanShowReference())
      return (Stream) null;
    if (!this.IsEmptyObjectRef)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributable dbAttributable;
        if (this.IsReferenceToRelation)
        {
          dbAttributable = (IDBAttributable) this.GetDBRelation(sessionKeeper.Session, out IDBObject _);
          long dbRelationId = this.DBRelationID;
        }
        else
        {
          dbAttributable = (IDBAttributable) this.GetDBObject(sessionKeeper.Session);
          long dbObjectId = this.DBObjectID;
        }
        if (dbAttributable != null)
        {
          if (!this.IsConnectedAttributeRef)
            this.UpdateAttributeInfo();
          if (this.fileAttrID != -1)
          {
            IDBAttribute attributeById = dbAttributable.GetAttributeByID(this.fileAttrID);
            if (attributeById != null)
            {
              int num = 0;
              if (this.fileName != null)
              {
                num = -1;
                string[] descriptions = attributeById.Descriptions;
                if (descriptions != null)
                {
                  for (int index = 0; index < descriptions.Length; ++index)
                  {
                    if (descriptions[index] == this.fileName)
                    {
                      num = index;
                      break;
                    }
                  }
                }
                if (num == -1)
                  return (Stream) null;
              }
              attributeById.Index = num;
              ImChunkedStream aDestStream = new ImChunkedStream();
              new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
              aDestStream.Position = 0L;
              return (Stream) aDestStream;
            }
          }
        }
      }
    }
    return (Stream) null;
  }

  public override void UpdateLink(
    object userSession,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> objAttrCache,
    Dictionary<Guid, Dictionary<Guid, AttributeValueCache>> relAttrCache,
    bool forceUpdate,
    bool updateUI,
    bool updateLayout)
  {
    this.UpdateLink(userSession as IUserSession, forceUpdate, updateUI, updateLayout);
  }

  public virtual void UpdateLink(
    IUserSession userSession,
    bool forceUpdate,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsSuspendedUpdatesFromDB || !forceUpdate && this.PassiveLink)
      return;
    this.GetParentDBObjectInfo(userSession);
    this.UpdateDBObjectInfo(userSession, (string) null);
    this.UpdateAttributeInfo();
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

  public void UpdateDBObjectInfo(IUserSession session, string filtrationSettings)
  {
    if (this.IsConnectedObjectRef)
      return;
    IDBObject dbObject = (IDBObject) null;
    if (this.IsReferenceToRelation)
      this.GetDBRelation(session, out dbObject, filtrationSettings);
    else
      this.GetDBObject(session, filtrationSettings);
  }

  public void UpdateDBObjectInfo(IDBRelation dbRelation, IDBObject dbObject)
  {
    ReferenceToDBObjectCore.UpdateDBObjectInfo(dbRelation, dbObject, (ReferenceToDBObjectBase) this);
  }
}
