// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPCreateBlankInstanceAction
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MRP;
using Intermech.Pdm.InstancesAndParties;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPCreateBlankInstanceAction : 
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
  private int instanceTypeID;
  private IMRPTypedObjectRef instanceObjRef;

  public MRPCreateBlankInstanceAction(
    IServiceProvider services,
    IMRPTypedObjectRef article,
    int instanceTypeID)
    : base(services)
  {
    if (article == null)
      throw new ArgumentNullException(nameof (article));
    if (instanceTypeID == -1)
      throw new ArgumentException();
    this.article = article;
    this.instanceTypeID = instanceTypeID;
  }

  public MRPCreateBlankInstanceAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  public override void Clear()
  {
    base.Clear();
    this.article = (IMRPTypedObjectRef) null;
    this.instanceTypeID = -1;
  }

  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPCreateBlankInstanceAction blankInstanceAction))
      return;
    this.article = blankInstanceAction.article;
    this.instanceTypeID = blankInstanceAction.instanceTypeID;
  }

  public long ObjectID
  {
    [DebuggerStepThrough] get => this.instanceObjRef == null ? 0L : this.instanceObjRef.ObjectID;
  }

  public Guid Guid
  {
    [DebuggerStepThrough] get
    {
      return this.instanceObjRef == null ? Guid.Empty : this.instanceObjRef.Guid;
    }
  }

  public void UpdateItemID(long newItemID)
  {
    if (this.instanceObjRef == null)
      return;
    this.instanceObjRef.UpdateItemID(newItemID);
  }

  public int TypeID
  {
    [DebuggerStepThrough] get => this.instanceObjRef == null ? -1 : this.instanceObjRef.TypeID;
  }

  public override void Execute() => this.Execute((IServiceProvider) null);

  public override void Execute(IServiceProvider context)
  {
    if (this.article.ObjectID == 0L)
      throw new ArgumentException();
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
      this.instanceObjRef = this.CreateBlankInstance(MRPContextHelper.GetContextSession((IMRPContext) this) ?? throw new ArgumentNullException("session"), this.instanceTypeID);
  }

  private IMRPTypedObjectRef CreateBlankInstance(IUserSession session, int instanceTypeID)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (instanceTypeID == -1)
      return (IMRPTypedObjectRef) null;
    IMRPTypedObjectRef blankInstance = (IMRPTypedObjectRef) new MRPTypedObjectRef(this.Services, 0L, Guid.Empty, instanceTypeID);
    new MRPCreateBlankObjectAction(this.Services, instanceTypeID, (IMRPUpdateableItemRef) blankInstance).Execute();
    session.GetObjectInfo(blankInstance.ObjectID);
    IDBObject dbObject = session.GetObject(this.article.ObjectID);
    IDBAttribute attributeById1 = dbObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute attributeById2 = dbObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"));
    new MRPCopyArticleToInstanceAttrs(this.Services, this.article, blankInstance).Execute();
    List<AttributeValues> attributeValuesList = new List<AttributeValues>()
    {
      new AttributeValues(MetaDataHelper.GetAttributeTypeID("cadd93c3-306c-11d8-b4e9-00304f19f545"), (object) MRPContextHelper.GetOrderNumber((IMRPContext) this))
    };
    if (attributeById1 != null)
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), attributeById1.Value));
    if (attributeById2 != null)
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), attributeById2.Value));
    if (Intermech.Pdm.InstancesAndParties.MaterialHelper.IsInstanceOrPartyMaterial(session, instanceTypeID))
      attributeValuesList.Add(new AttributeValues(Constants.MaterialReferenceAttributeTypeID, (object) Math.Abs(this.article.ObjectID)));
    else if (Intermech.Pdm.InstancesAndParties.MaterialHelper.IsInstanceOrPartyCompositeMaterial(session, instanceTypeID))
      attributeValuesList.Add(new AttributeValues(Constants.CompositeMaterialReferenceAttributeTypeID, (object) Math.Abs(this.article.ObjectID)));
    else if (Intermech.Pdm.InstancesAndParties.MaterialHelper.IsInstanceOrPartyMaterialMark(session, instanceTypeID))
    {
      attributeValuesList.Add(new AttributeValues(Constants.MaterialMarkReferenceAttributeTypeID, (object) Math.Abs(this.article.ObjectID)));
    }
    else
    {
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00622-306c-11d8-b4e9-00304f19f545"), (object) Math.Abs(this.article.ObjectID)));
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cadd92f0-306c-11d8-b4e9-00304f19f545"), (object) Math.Abs(this.article.ObjectID)));
    }
    new MRPWriteObjectAttributesAction(this.Services, (IMRPObjectRef) blankInstance, attributeValuesList.ToArray()).Execute();
    return blankInstance;
  }
}
