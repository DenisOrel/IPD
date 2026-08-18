// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Services.ComponentSelectionService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server.Services;

internal sealed class ComponentSelectionService : LongLifeObject, IComponentSelectionService
{
  private int relationTypeComponentSelection;
  private int attributeSelectionForPosDesignation;
  private int attributeCountOnRegulation;

  public ComponentSelectionService(IUserSession session, IServiceProvider serviceProvider)
  {
    this.relationTypeComponentSelection = session.GetRelationType(ComponentSelectionConsts.relationTypeComponentSelection).RelationType;
    this.attributeCountOnRegulation = session.GetAttributeType(ComponentSelectionConsts.attributeCountOnRegulation).AttributeID;
    this.attributeSelectionForPosDesignation = session.GetAttributeType(ComponentSelectionConsts.attributeSelectionForPosDesignation).AttributeID;
    (serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper).BeforeDeleteRelationEvent += new DeleteRelationHandler(this.eventLogHelper_BeforeDeleteRelationEvent);
  }

  private void eventLogHelper_BeforeDeleteRelationEvent(
    IDBRelation relation,
    long deleteMode,
    IUserSession session)
  {
    if (!ComponentSelectionHelper.IsMainComponent(relation, out string _))
      return;
    (session as UserSession).StartTransaction();
    try
    {
      this.DeleteComponentSelectionRelations(session, relation, out List<long> _);
      (session as UserSession).Commit();
    }
    catch
    {
      (session as UserSession).Rollback();
    }
  }

  public long CreateComponentSelection(
    Guid sessionGuid,
    long projectID,
    long objectID,
    string posDesignation,
    MeasuredValue countOnRegulation)
  {
    if (string.IsNullOrEmpty(posDesignation))
      throw new ArgumentNullException(nameof (posDesignation));
    IDBRelation dbRelation = UserSession.GetSessionByID(sessionGuid).GetRelationCollection(this.relationTypeComponentSelection).Create(projectID, objectID);
    (dbRelation.GetAttributeByID(this.attributeSelectionForPosDesignation) ?? dbRelation.Attributes.AddAttribute(this.attributeSelectionForPosDesignation, false, (object[]) null)).Value = (object) posDesignation;
    if (countOnRegulation != null)
      (dbRelation.GetAttributeByID(this.attributeCountOnRegulation) ?? dbRelation.Attributes.AddAttribute(this.attributeCountOnRegulation, false, (object[]) null)).Value = (object) countOnRegulation;
    return dbRelation.RelationID;
  }

  public void ResetComponentSelection(
    Guid sessionGuid,
    long projectID,
    Guid relationGuid,
    out long changedRelationId,
    out List<long> removedRelationIds)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    IDBRelation relation = sessionById.GetRelation(relationGuid, projectID);
    sessionById.GetObject(projectID).CheckEdit();
    removedRelationIds = (List<long>) null;
    changedRelationId = relation.RelationID;
    (sessionById as UserSession).StartTransaction();
    try
    {
      this.DeleteComponentSelectionRelations(sessionById, relation, out removedRelationIds);
      this.DeleteAttributeFromRelation(relation, ComponentSelectionConsts.attributeReplace);
      this.DeleteAttributeFromRelation(relation, ComponentSelectionConsts.attributeNominals);
      (sessionById as UserSession).Commit();
    }
    catch
    {
      (sessionById as UserSession).Rollback();
    }
  }

  private void DeleteComponentSelectionRelations(
    IUserSession session,
    IDBRelation relation,
    out List<long> removedRelations)
  {
    removedRelations = new List<long>();
    IDBAttribute attributeByGuid = relation.GetAttributeByGuid(new Guid("cad01478-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null || string.IsNullOrEmpty(attributeByGuid.AsString))
      return;
    foreach (DataRow row in (InternalDataCollectionBase) session.GetRelationCollection(this.relationTypeComponentSelection).ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(this.attributeSelectionForPosDesignation, RelationalOperators.Equal, (object) attributeByGuid.AsString, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -20 }), relation.ProjID).Rows)
    {
      IDBRelation relation1 = session.GetRelation(Convert.ToInt64(row[0]));
      removedRelations.Add(relation1.RelationID);
      relation1.Delete(0L);
    }
  }

  private void DeleteAttributeFromRelation(IDBRelation relation, Guid attributeGuid)
  {
    relation.GetAttributeByGuid(attributeGuid, false)?.Delete(0L);
  }
}
