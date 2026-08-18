// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPCopyArticleToInstanceAttrs
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MRP;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPCopyArticleToInstanceAttrs : 
  MRPBaseAction,
  IMRPAction,
  IMRPContext,
  IMRPTypedObjectRef,
  IMRPObjectRef,
  IMRPGuidItem,
  IMRPUpdateableItemRef,
  IMRPTypedItem
{
  private IMRPTypedObjectRef article;
  private IMRPTypedObjectRef instance;
  private static Dictionary<Tuple<int, int>, List<int>> toSyncAttrs = new Dictionary<Tuple<int, int>, List<int>>();

  public MRPCopyArticleToInstanceAttrs(
    IServiceProvider services,
    IMRPTypedObjectRef article,
    IMRPTypedObjectRef instance)
    : base(services)
  {
    if (article == null)
      throw new ArgumentNullException(nameof (article));
    if (instance == null)
      throw new ArgumentNullException(nameof (instance));
    this.article = article;
    this.instance = instance;
  }

  public MRPCopyArticleToInstanceAttrs(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  public override void Clear()
  {
    base.Clear();
    this.article = (IMRPTypedObjectRef) null;
    this.instance = (IMRPTypedObjectRef) null;
  }

  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPCopyArticleToInstanceAttrs articleToInstanceAttrs))
      return;
    this.article = articleToInstanceAttrs.article;
    this.instance = articleToInstanceAttrs.instance;
  }

  public long ObjectID
  {
    [DebuggerStepThrough] get => this.instance == null ? 0L : this.instance.ObjectID;
  }

  public Guid Guid
  {
    [DebuggerStepThrough] get => this.instance == null ? Guid.Empty : this.instance.Guid;
  }

  public void UpdateItemID(long newItemID)
  {
    if (this.instance == null)
      return;
    this.instance.UpdateItemID(newItemID);
  }

  public int TypeID
  {
    [DebuggerStepThrough] get => this.instance == null ? -1 : this.instance.TypeID;
  }

  public override void Execute() => this.Execute((IServiceProvider) null);

  public override void Execute(IServiceProvider context)
  {
    if (this.article == null || this.article.ObjectID == 0L || this.instance == null || this.instance.ObjectID == 0L || Math.Abs(this.article.ObjectID) == Math.Abs(this.instance.ObjectID))
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      List<int> intList = contextSession != null ? this.GetAttrsToSync(contextSession) : throw new ArgumentNullException("session");
      if (intList.Count == 0)
        return;
      IDBObject dbObject = contextSession.GetObject(this.article.ObjectID);
      List<AttributeValues> attributeValuesList = new List<AttributeValues>(intList.Count);
      for (int index = 0; index < intList.Count; ++index)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(intList[index]);
        if (attributeById != null)
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(intList[index]);
          AttributeValues attributeValues = new AttributeValues(attributeById.AttributeID, attributeById.DataType, attributeType.MultiValueMode, attributeById.Values);
          attributeValuesList.Add(attributeValues);
        }
      }
      if (attributeValuesList.Count <= 0)
        return;
      new MRPWriteObjectAttributesAction(this.Services, (IMRPObjectRef) this.instance, attributeValuesList.ToArray()).Execute();
    }
  }

  private List<int> GetAttrsToSync(IUserSession session)
  {
    if (session == null || this.article.ObjectID == 0L || this.instance.ObjectID == 0L)
      return new List<int>();
    int ObjectTypeID1 = this.article.TypeID;
    if (ObjectTypeID1 == -1)
      ObjectTypeID1 = session.GetObjectInfo(this.article.ObjectID).ObjectTypeID;
    int ObjectTypeID2 = this.instance.TypeID;
    if (ObjectTypeID2 == -1)
      ObjectTypeID2 = session.GetObjectInfo(this.instance.ObjectID).ObjectTypeID;
    if (ObjectTypeID1 == -1 || ObjectTypeID2 == -1)
      return new List<int>();
    Tuple<int, int> key = new Tuple<int, int>(ObjectTypeID1, ObjectTypeID2);
    lock (MRPCopyArticleToInstanceAttrs.toSyncAttrs)
    {
      if (MRPCopyArticleToInstanceAttrs.toSyncAttrs.ContainsKey(key))
        return MRPCopyArticleToInstanceAttrs.toSyncAttrs[key];
    }
    List<IMSAttribute4ObjectType> attribute4ObjectTypeList1 = MetaDataHelper.GetAttribute4ObjectTypeList(ObjectTypeID1);
    List<IMSAttribute4ObjectType> attribute4ObjectTypeList2 = MetaDataHelper.GetAttribute4ObjectTypeList(ObjectTypeID2);
    attribute4ObjectTypeList1.RemoveAll((Predicate<IMSAttribute4ObjectType>) (attrType => attrType.AttributeID < 0));
    attribute4ObjectTypeList2.RemoveAll((Predicate<IMSAttribute4ObjectType>) (attrType => attrType.AttributeID < 0));
    Dictionary<int, IMSAttribute4ObjectType> artAttrsDict = new Dictionary<int, IMSAttribute4ObjectType>(attribute4ObjectTypeList1.Count);
    attribute4ObjectTypeList1.ForEach((Action<IMSAttribute4ObjectType>) (attrType => artAttrsDict[attrType.AttributeID] = attrType));
    attribute4ObjectTypeList2.RemoveAll((Predicate<IMSAttribute4ObjectType>) (attrType => !artAttrsDict.ContainsKey(attrType.AttributeID) || attrType.Computed != 0));
    List<int> attrsToSync = attribute4ObjectTypeList2.ConvertAll<int>((Converter<IMSAttribute4ObjectType, int>) (attrType => attrType.AttributeID));
    lock (MRPCopyArticleToInstanceAttrs.toSyncAttrs)
      MRPCopyArticleToInstanceAttrs.toSyncAttrs[key] = attrsToSync;
    return attrsToSync;
  }
}
