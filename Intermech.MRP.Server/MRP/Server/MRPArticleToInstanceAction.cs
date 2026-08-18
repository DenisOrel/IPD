// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPArticleToInstanceAction
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.MRP;
using Intermech.Pdm.InstancesAndParties;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPArticleToInstanceAction : 
  MRPBaseAction,
  IMRPAction,
  IMRPContext,
  IMRPTypedObjectRef,
  IMRPObjectRef,
  IMRPGuidItem,
  IMRPUpdateableItemRef,
  IMRPTypedItem,
  IMRPRelationRef
{
  private IMRPTypedObjectRef proj;
  private MRPCompositionObject article;
  private IMRPTypedObjectRef instanceObjRef;
  private IMRPRelationRef instanceRelRef;
  private RelationPath rootObjectPath;
  private BoughtArticleItemSettings settings;
  private MovingItemSettings movingSettings;
  private bool _isExecuted;

  public MRPArticleToInstanceAction(
    IServiceProvider services,
    IMRPTypedObjectRef proj,
    MRPCompositionObject article,
    RelationPath rootObjectPath,
    BoughtArticleItemSettings settings,
    MovingItemSettings movingSettings)
    : base(services)
  {
    if (proj == null)
      throw new ArgumentNullException(nameof (proj));
    if (article == null)
      throw new ArgumentNullException(nameof (article));
    if (rootObjectPath == null)
      throw new ArgumentNullException(nameof (rootObjectPath));
    this.proj = proj;
    this.article = article;
    this.rootObjectPath = rootObjectPath;
    this.settings = settings;
    this.movingSettings = movingSettings;
  }

  public MRPArticleToInstanceAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  public override void Clear()
  {
    base.Clear();
    this.proj = (IMRPTypedObjectRef) null;
    this.article = (MRPCompositionObject) null;
    this.instanceObjRef = (IMRPTypedObjectRef) null;
    this.instanceRelRef = (IMRPRelationRef) null;
    this.rootObjectPath = (RelationPath) null;
    this.settings = (BoughtArticleItemSettings) null;
  }

  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPArticleToInstanceAction toInstanceAction))
      return;
    this.proj = toInstanceAction.proj;
    this.article = toInstanceAction.article;
    this.instanceObjRef = toInstanceAction.instanceObjRef;
    this.instanceRelRef = toInstanceAction.instanceRelRef;
    this.rootObjectPath = toInstanceAction.rootObjectPath;
    this.settings = toInstanceAction.settings;
  }

  public long ObjectID
  {
    get
    {
      if (this.instanceObjRef == null)
        this.Execute();
      return this.instanceObjRef == null ? 0L : this.instanceObjRef.ObjectID;
    }
  }

  public Guid Guid
  {
    [DebuggerStepThrough] get
    {
      return this.instanceRelRef == null ? Guid.Empty : this.instanceRelRef.Guid;
    }
  }

  public void UpdateItemID(long newItemID)
  {
    if (this.instanceObjRef == null)
      return;
    this.instanceObjRef.UpdateItemID(newItemID);
  }

  public bool IsNewRelation
  {
    [DebuggerStepThrough] get => this.instanceRelRef != null && this.instanceRelRef.IsNewRelation;
  }

  public long ProjectID
  {
    [DebuggerStepThrough] get => this.instanceRelRef == null ? 0L : this.instanceRelRef.ProjectID;
  }

  public long PrjLinkID
  {
    [DebuggerStepThrough] get => this.instanceRelRef == null ? 0L : this.instanceRelRef.PrjLinkID;
  }

  public int TypeID
  {
    [DebuggerStepThrough] get => this.instanceObjRef == null ? -1 : this.instanceObjRef.TypeID;
  }

  public override void Execute() => this.Execute((IServiceProvider) null);

  public override void Execute(IServiceProvider context)
  {
    if (this.proj.ObjectID == 0L)
      throw new ArgumentException();
    if (this.article.F_OBJECT_ID == 0L)
      throw new ArgumentException();
    if (this._isExecuted)
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      this.Services.GetService(typeof (ManufactureOrderHolder));
      if (this.article.ProductionAccountingOfParts == 0L)
        this.MakeParty(contextSession, this.settings, this.movingSettings);
      else
        this.MakeInstance(contextSession, this.settings, this.movingSettings);
    }
    this._isExecuted = true;
  }

  private void MakeParty(
    IUserSession session,
    BoughtArticleItemSettings settings,
    MovingItemSettings movingSettings)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    this.settings = settings;
    MRPParsedLinks service1 = this.Services.GetService(typeof (MRPParsedLinks)) as MRPParsedLinks;
    int typeId4ObjectTypeId = InstancePartyObjectType4ObjectTypeHelper.GetPartyObjectTypeID4ObjectTypeID(session, this.article.F_OBJECT_TYPE);
    if (typeId4ObjectTypeId == -1)
      return;
    IMRPCheckInObjectsRef service2 = this.Services.GetService(typeof (IMRPCheckInObjectsRef)) as IMRPCheckInObjectsRef;
    this.instanceObjRef = MRPPartiesHelper.FindParty(session, this.Services, (IMRPTypedObjectRef) this.article, settings != null ? settings.IsBoughtArticle : this.article.IsBoughtArticle);
    bool flag1 = this.instanceObjRef == null;
    if (this.instanceObjRef == null)
    {
      this.instanceObjRef = (IMRPTypedObjectRef) new MRPCreateBlankInstanceAction(this.Services, (IMRPTypedObjectRef) this.article, typeId4ObjectTypeId);
      (this.instanceObjRef as IMRPAction).Execute();
    }
    bool parIsCheckedOut = false;
    MRPRelationRef sourceRel = new MRPRelationRef(this.Services, this.article.F_PROJ_ID, this.article.F_PRJLINK_ID, this.article.LINK_GUID, this.article.F_RELATION_TYPE, false);
    bool flag2 = MetaDataHelper.IsObjectTypeChildOf(this.proj.TypeID, MetaDataHelper.GetObjectTypeID("cadd92e9-306c-11d8-b4e9-00304f19f545"));
    if (movingSettings != null && Math.Abs(movingSettings.NewArticleID) == Math.Abs(this.article.F_OBJECT_ID) && movingSettings.NewArticleLinkID == this.article.F_PRJLINK_ID)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(movingSettings.SourceProjID, true);
      this.proj = (IMRPTypedObjectRef) new MRPTypedObjectRef(this.Services, objectActualCopy.ObjectID, objectActualCopy.ObjectGUID, objectActualCopy.ObjectType);
      IDBRelation relation = session.GetRelation(movingSettings.SourceLinkID);
      new MRPDeleteRelationAction(this.Services, (IMRPObjectRef) this.proj, relation.GUID, relation.RelationType).Execute();
    }
    this.instanceRelRef = MRPCreateRelationAction.CreateRelation(this.Services, session, this.proj, this.instanceObjRef, (IMRPRelationRef) sourceRel, !flag1 && !flag2, MetaDataHelper.GetRelationTypeID("cad00584-306c-11d8-b4e9-00304f19f545"), out parIsCheckedOut);
    new MRPFixRelationPartAction(this.Services, this.instanceRelRef, (IMRPObjectRef) this.instanceObjRef).Execute();
    if (settings != null)
    {
      if (flag1)
      {
        IDBObject dbObject = session.GetObject(this.instanceObjRef.ObjectID);
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid("cad0038f-306c-11d8-b4e9-00304f19f545"));
        IDBAttribute attributeById = dbObject.GetAttributeByID(attributeType.AttributeID);
        long num = attributeById != null ? DataSetProcessor.GetInt64Value(attributeById.Value, 1L) : 1L;
        if (attributeById != null && num == 1L)
          dbObject.TryToAddOrDelAttribute(attributeType.AttributeID, (object) null);
        else if (settings.IsBoughtArticle != num || attributeById == null && settings.IsBoughtArticle != 1L)
          dbObject.TryToAddOrDelAttribute(attributeType.AttributeID, (object) settings.IsBoughtArticle);
      }
      if (settings.SourceQuantity != null && settings.BoughtQuantity != null && !service1.Exists(this.instanceRelRef.PrjLinkID))
      {
        new MRPWriteRelationAttributesAction(this.Services, this.instanceRelRef, new AttributeValues[1]
        {
          new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), settings.IsBoughtArticle == 2L ? (object) settings.BoughtQuantity : (object) settings.SourceQuantity)
        }).Execute();
        service1.Add(this.instanceRelRef.PrjLinkID);
      }
      new MRPWriteRelationAttributesAction(this.Services, this.instanceRelRef, new AttributeValues[1]
      {
        new AttributeValues(MetaDataHelper.GetAttributeTypeID("cadd92ec-306c-11d8-b4e9-00304f19f545"), (object) sourceRel.Guid)
      }).Execute();
    }
    if (flag1)
    {
      new MRPCommitBlankObjectAction(this.Services, (IMRPObjectRef) this.instanceObjRef).Execute();
      if (service2 != null)
        service2.Add((IMRPObjectRef) this.instanceObjRef);
      else
        new MRPCheckInAction(this.Services, (IMRPObjectRef) this.instanceObjRef, true).Execute();
    }
    if (!parIsCheckedOut)
      return;
    if (service2 != null)
      service2.Add((IMRPObjectRef) this.proj);
    else
      new MRPCheckInAction(this.Services, (IMRPObjectRef) this.proj, true).Execute();
  }

  private void MakeInstance(
    IUserSession session,
    BoughtArticleItemSettings settings,
    MovingItemSettings movingSettings)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    int typeId4ObjectTypeId = InstancePartyObjectType4ObjectTypeHelper.GetInstanceObjectTypeID4ObjectTypeID(session, this.article.F_OBJECT_TYPE);
    if (typeId4ObjectTypeId == -1)
      return;
    IMRPCheckInObjectsRef service = this.Services.GetService(typeof (IMRPCheckInObjectsRef)) as IMRPCheckInObjectsRef;
    this.instanceObjRef = (IMRPTypedObjectRef) new MRPCreateBlankInstanceAction(this.Services, (IMRPTypedObjectRef) this.article, typeId4ObjectTypeId);
    (this.instanceObjRef as IMRPAction).Execute();
    bool parIsCheckedOut = false;
    MRPRelationRef sourceRel = new MRPRelationRef(this.Services, this.article.F_PROJ_ID, this.article.F_PRJLINK_ID, session.GetRelation(this.article.F_PRJLINK_ID).GUID, this.article.F_RELATION_TYPE, false);
    if (movingSettings != null && Math.Abs(movingSettings.NewArticleID) == Math.Abs(this.article.F_OBJECT_ID) && movingSettings.NewArticleLinkID == this.article.F_PRJLINK_ID)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(movingSettings.SourceProjID, true);
      this.proj = (IMRPTypedObjectRef) new MRPTypedObjectRef(this.Services, objectActualCopy.ObjectID, objectActualCopy.ObjectGUID, objectActualCopy.ObjectType);
      IDBRelation relation = session.GetRelation(movingSettings.SourceLinkID);
      new MRPDeleteRelationAction(this.Services, (IMRPObjectRef) this.proj, relation.GUID, relation.RelationType).Execute();
    }
    this.instanceRelRef = MRPCreateRelationAction.CreateRelation(this.Services, session, this.proj, this.instanceObjRef, (IMRPRelationRef) sourceRel, false, MetaDataHelper.GetRelationTypeID("cad00584-306c-11d8-b4e9-00304f19f545"), out parIsCheckedOut);
    new MRPFixRelationPartAction(this.Services, this.instanceRelRef, (IMRPObjectRef) this.instanceObjRef).Execute();
    if (settings != null)
    {
      IDBObject dbObject = session.GetObject(this.instanceObjRef.ObjectID);
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid("cad0038f-306c-11d8-b4e9-00304f19f545"));
      IDBAttribute attributeById = dbObject.GetAttributeByID(attributeType.AttributeID);
      long num = attributeById != null ? DataSetProcessor.GetInt64Value(attributeById.Value, 1L) : 1L;
      if (settings.IsBoughtArticle != num || attributeById == null && settings.IsBoughtArticle != 1L)
        dbObject.TryToAddOrDelAttribute(attributeType.AttributeID, (object) settings.IsBoughtArticle);
      if (settings.SourceQuantity != null && settings.BoughtQuantity != null)
        new MRPWriteRelationAttributesAction(this.Services, this.instanceRelRef, new AttributeValues[2]
        {
          new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), settings.IsBoughtArticle == 2L ? (object) settings.BoughtQuantity : (object) settings.SourceQuantity),
          new AttributeValues(MetaDataHelper.GetAttributeTypeID("cadd92ec-306c-11d8-b4e9-00304f19f545"), (object) sourceRel.Guid)
        }).Execute();
    }
    new MRPCommitBlankObjectAction(this.Services, (IMRPObjectRef) this.instanceObjRef).Execute();
    if (service != null)
      service.Add((IMRPObjectRef) this.instanceObjRef);
    else
      new MRPCheckInAction(this.Services, (IMRPObjectRef) this.instanceObjRef, true).Execute();
    if (!parIsCheckedOut)
      return;
    if (service != null)
      service.Add((IMRPObjectRef) this.proj);
    else
      new MRPCheckInAction(this.Services, (IMRPObjectRef) this.proj, true).Execute();
  }
}
