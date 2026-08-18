// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPFindTechRouteAction
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.MRP;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPFindTechRouteAction : 
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
  private DataTable table;
  private RelationPath rootObjectPath;
  private ManufactureOrderHolder holder;
  private IMRPTypedObjectRef techObjRef;
  private IMRPRelationRef techRelRef;

  public MRPFindTechRouteAction(
    IServiceProvider services,
    DataTable table,
    RelationPath rootObjectPath,
    ManufactureOrderHolder holder)
    : base(services)
  {
    this.table = table != null ? table : throw new ArgumentNullException(nameof (table));
    this.rootObjectPath = rootObjectPath;
    this.holder = holder;
  }

  public MRPFindTechRouteAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  public override void Clear()
  {
    base.Clear();
    this.table = (DataTable) null;
    this.rootObjectPath = (RelationPath) null;
    this.holder = (ManufactureOrderHolder) null;
    this.techObjRef = (IMRPTypedObjectRef) null;
    this.techRelRef = (IMRPRelationRef) null;
  }

  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPFindTechRouteAction findTechRouteAction))
      return;
    this.table = findTechRouteAction.table;
    this.rootObjectPath = findTechRouteAction.rootObjectPath;
    this.holder = findTechRouteAction.holder;
    this.techObjRef = findTechRouteAction.techObjRef;
    this.techRelRef = findTechRouteAction.techRelRef;
  }

  public long ObjectID
  {
    [DebuggerStepThrough] get => this.techObjRef == null ? 0L : this.techObjRef.ObjectID;
  }

  public Guid Guid
  {
    [DebuggerStepThrough] get => this.techObjRef == null ? Guid.Empty : this.techObjRef.Guid;
  }

  public void UpdateItemID(long newItemID)
  {
    if (this.techObjRef == null)
      return;
    this.techObjRef.UpdateItemID(newItemID);
  }

  public bool IsNewRelation
  {
    [DebuggerStepThrough] get => this.techRelRef != null && this.techRelRef.IsNewRelation;
  }

  public long ProjectID
  {
    [DebuggerStepThrough] get => this.techRelRef == null ? 0L : this.techRelRef.ProjectID;
  }

  public long PrjLinkID
  {
    [DebuggerStepThrough] get => this.techRelRef == null ? 0L : this.techRelRef.PrjLinkID;
  }

  public int TypeID
  {
    [DebuggerStepThrough] get => this.techObjRef == null ? -1 : this.techObjRef.TypeID;
  }

  public override void Execute() => this.Execute((IServiceProvider) null);

  public override void Execute(IServiceProvider context)
  {
    if (this.table == null)
      return;
    if (this.table.Rows.Count == 0)
      return;
    try
    {
      using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
      {
        IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
        if (contextSession == null)
          throw new ArgumentNullException("session");
        TechnologicalItemSettings pathSetting = this.holder != null ? this.holder.GetPathSetting(this.rootObjectPath, typeof (TechnologicalItemSettings)) as TechnologicalItemSettings : (TechnologicalItemSettings) null;
        for (int index = 0; index < this.table.Rows.Count; ++index)
        {
          MRPCompositionObject compositionObject = new MRPCompositionObject(this.table.Rows[index]);
          if (MetaDataHelper.IsObjectTypeChildOf(compositionObject.F_OBJECT_TYPE, MetaDataHelper.GetObjectTypeID("cad0016f-306c-11d8-b4e9-00304f19f545")) && compositionObject.F_RELATION_TYPE == MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"))
          {
            if (pathSetting != null && Math.Abs(pathSetting.RouteLinkID) == Math.Abs(compositionObject.F_PRJLINK_ID))
            {
              this.techObjRef = (IMRPTypedObjectRef) compositionObject;
              this.techRelRef = (IMRPRelationRef) compositionObject;
              break;
            }
            if (this.techObjRef == null && !string.IsNullOrEmpty(DataSetProcessor.GetStringValue(contextSession.GetObject(compositionObject.F_OBJECT_ID).GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad005b9-306c-11d8-b4e9-00304f19f545")).Value, string.Empty)))
            {
              this.techObjRef = (IMRPTypedObjectRef) compositionObject;
              this.techRelRef = (IMRPRelationRef) compositionObject;
            }
          }
        }
      }
    }
    finally
    {
      this.table = (DataTable) null;
      this.holder = (ManufactureOrderHolder) null;
    }
  }
}
