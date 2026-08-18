// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRP2ServerService
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.MRP2;
using Intermech.Search.Pdm.Substitutes;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRP2ServerService : LongLifeObject, IMRP2ServerService
{
  private int[] _copyAttrs;
  private int[] _objectAttrs;
  private static int _relationTypeEco = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");

  public void RecalculateProductionCopyHash(Guid sessionGuid, long objectId)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      throw new KernelException($"Сессия {sessionGuid} не найдена");
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(126);
    this.recalcHash(sessionById, objectId);
  }

  private int[] getCopyAttributes()
  {
    if (this._copyAttrs == null)
      this._copyAttrs = new int[3]
      {
        MRP2Consts.attrIdArticleLink,
        -7,
        MRP2Consts.attrIdSupplyMethod
      };
    return this._copyAttrs;
  }

  private int[] getObjectAttributes()
  {
    if (this._objectAttrs == null)
      this._objectAttrs = new int[3]
      {
        -18,
        MetaDataHelper.GetAttributeID((object) "cad0013a-306c-11d8-b4e9-00304f19f545"),
        -50
      };
    return this._objectAttrs;
  }

  private string getSimpleHash(IUserSession session, long objectId, out int objectType)
  {
    AttributeValues[] attributesValues1 = session.GetObjectAttributesValues(objectId, this.getCopyAttributes(), GetAttributeValuesModes.IncludeObligatoryAttributes, false);
    long asInteger = attributesValues1[0].AsInteger;
    objectType = (int) attributesValues1[1].AsInteger;
    MRP2Consts.ArticleSupplyMethod? nullable = new MRP2Consts.ArticleSupplyMethod?();
    if (attributesValues1[2] != null)
      nullable = MRP2Consts.StringToArticleSupplyMethod(attributesValues1[2].AsString);
    AttributeValues[] attributesValues2 = session.GetObjectAttributesValues(asInteger, this.getObjectAttributes(), GetAttributeValuesModes.IncludeObligatoryAttributes, false);
    return MRP2Consts.HashData($"{attributesValues2[0].AsString.ToLower()}-{(attributesValues2[1] == null ? attributesValues2[2].AsString : ((DateTime) attributesValues2[1].Value).ToUniversalTime().ToString("ddMMyyyyHHmmssfff"))}-{objectType}-{nullable.ToString()}");
  }

  private string recalcHash(IUserSession session, long objectId)
  {
    int objectType;
    string simpleHash = this.getSimpleHash(session, objectId, out objectType);
    List<string> stringList = new List<string>();
    foreach (DataRow row in (InternalDataCollectionBase) session.GetRelationCollection(MRP2Consts.reltypeIdProductComposition).ConsistFrom(new DBRecordSetParams(new ConditionStructure[0], new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) MRP2Consts.attrIdCount, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }), objectId).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      string str = this.recalcHash(session, int64);
      string stringValue = DataSetProcessor.GetStringValue(row, 1, "");
      stringList.Add($"{str}-{stringValue}");
    }
    stringList.Sort();
    stringList.Add(simpleHash);
    string initValue = MRP2Consts.HashData(string.Join("\r\n", stringList.ToArray()));
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, MRP2Consts.objtypeIdProductionCopy))
      session.SetObjectAttributesValues(objectId, true, new AttributeValues[1]
      {
        new AttributeValues(MRP2Consts.attrIdHash, (object) initValue)
      });
    return initValue;
  }

  internal void AfterCreateRelationEvent(IDBRelation sender, IUserSession session, int assignMode)
  {
    if (sender.RelationType != MRP2ServerService._relationTypeEco || Consts.IsUndefinedObjectId(sender.PartObjectID) || !MetaDataHelper.IsObjectTypeChildOf(session.GetObjectInfo(sender.ProjID).ObjectTypeID, new Guid("cadd9bb3-306c-11d8-b4e9-00304f19f545")))
      return;
    AttributeValues[] attributesValues = session.GetObjectAttributesValues(sender.ProjID, new int[1]
    {
      MRP2Consts.attrIdChangeBase
    }, GetAttributeValuesModes.None, false);
    if (attributesValues == null || attributesValues[0] == null)
      return;
    AttributeValues[] attributeValues = new AttributeValues[1]
    {
      new AttributeValues(MRP2Consts.attrIdChangeBase, (object) attributesValues[0].Values)
    };
    session.SetObjectAttributesValues(sender.PartObjectID, false, attributeValues);
  }

  private void SetAttrChangeBaseByEco(
    IUserSession session,
    long ecoObjectId,
    int attrId,
    object newValue)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(MRP2ServerService._relationTypeEco);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[0], new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    AttributeValues[] attributeValues = new AttributeValues[1]
    {
      new AttributeValues(attrId, newValue)
    };
    DataTable dataTable = relationCollection.ConsistFrom(paramSet, ecoObjectId);
    if (dataTable == null || dataTable.Rows.Count <= 0)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
      if (int64Value != 0L)
        session.SetObjectAttributesValues(int64Value, false, attributeValues);
    }
  }

  internal void WriteAttributeValuesHandler(IDBAttribute attribute, AttributeValuesEventArgs args)
  {
    if (args == null || !(attribute is DBAttribute dbAttribute) || !dbAttribute.IsObjectAttribute || dbAttribute.AttributeID != MRP2Consts.attrIdChangeBase || !(dbAttribute.ParentObject is DBObject parentObject) || parentObject.IsCreationMode || !MetaDataHelper.IsObjectTypeChildOf(parentObject.ObjectType, new Guid("cadd9bb3-306c-11d8-b4e9-00304f19f545")))
      return;
    this.SetAttrChangeBaseByEco(parentObject.Session, parentObject.ObjectID, dbAttribute.AttributeID, args.Values);
  }

  internal void WriteAttributeValueHandler(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    if (args == null || args.NewValue == args.OldValue || !(attribute is DBAttribute dbAttribute) || !dbAttribute.IsObjectAttribute || dbAttribute.AttributeID != MRP2Consts.attrIdChangeBase || !(dbAttribute.ParentObject is DBObject parentObject) || parentObject.IsCreationMode || !MetaDataHelper.IsObjectTypeChildOf(parentObject.ObjectType, new Guid("cadd9bb3-306c-11d8-b4e9-00304f19f545")))
      return;
    this.SetAttrChangeBaseByEco(parentObject.Session, parentObject.ObjectID, dbAttribute.AttributeID, args.Value);
  }

  public void SetPLForCopy(
    Guid sessionGuid,
    long oldRelationID,
    long PlObjectID,
    long copyObjectID)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      throw new KernelException($"Сессия {sessionGuid} не найдена");
    DataTable dataTable = sessionById.GetRelationCollection(MRP2Consts.reltypeIdProductComposition).ConsistFrom(new DBRecordSetParams(new ConditionStructure[0], new object[3]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
      (object) ObligatoryObjectAttributes.F_PRJ_GUID
    }), copyObjectID);
    IDBTransactions customService = sessionById.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
    customService.StartTransaction();
    try
    {
      sessionById.GetRelation(oldRelationID).Attributes.AddAttribute(MRP2Consts.attrIdProductionListLink, false).Value = (object) PlObjectID;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row[1]);
        sessionById.GetRelation(int64).Delete(0L);
      }
      customService.Commit();
    }
    catch
    {
      customService.Rollback();
      throw;
    }
  }

  public void ReplacePartFromSubstitute(
    Guid sessionGuid,
    long relationID1,
    long copyGroupId,
    long versionPL,
    long projObjectID,
    List<long> relIds,
    SubstituteObjects substitutes)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    IDBRelation dbRelation1 = sessionById != null ? sessionById.GetRelation(relationID1) : throw new KernelException($"Сессия {sessionGuid} не найдена");
    IDBTransactions customService = sessionById.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
    customService.StartTransaction();
    try
    {
      IDBRelationCollection relationCollection = sessionById.GetRelationCollection(dbRelation1.RelationType);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(SubstituteObjects.attrSubstituteGroupNo, RelationalOperators.Equal, (object) copyGroupId, LogicalOperators.AND, 0, false)
      }, new object[2]
      {
        (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, dbRelation1.ProjID).Rows)
        sessionById.GetRelation(Convert.ToInt64(row[0])).SetAttributesValues(new List<AttributeValues>()
        {
          new AttributeValues(MRP2Consts.attrIdDeleteTag, (object) 3),
          new AttributeValues(MRP2Consts.attrIdVersionNumberPL, (object) versionPL),
          new AttributeValues(MRP2Consts.attrIdChangeCode, (object) MRP2Consts.ProductionLinkFlag.Deleted)
        }.ToArray());
      foreach (long relId in relIds)
      {
        long relationObject = substitutes.RelationObjects[relId];
        IDBObject dbObj = sessionById.GetObject(relationObject);
        Dictionary<long, string> hashDict = new Dictionary<long, string>();
        int copyType = MRP2Consts.GetCopyType(sessionById, dbObj.ObjectType);
        MRP2Consts.CalculateHashForObject(dbObj, copyType, new MRP2Consts.ArticleSupplyMethod?(), true, hashDict);
        long objectCopy = MRP2Consts.CreateObjectCopy(dbObj, 0L, copyType, versionPL, new MRP2Consts.ArticleSupplyMethod?(), true, hashDict, (AttributeValues[]) null);
        IDBRelation relation = sessionById.GetRelation(relId);
        IDBRelation dbRelation2 = relationCollection.Create(projObjectID, objectCopy);
        dbRelation2.Attributes.AssignPossibleAttributes(relation.Attributes, Consts.CreateMode);
        dbRelation2.Attributes.AddAttribute(MRP2Consts.attrIdCreatedByRelation, false).Value = (object) relation.GUID;
        int[] numArray = new int[6]
        {
          SubstitutesConstants.SubstituteGroupNumberAttributeTypeID,
          SubstitutesConstants.SubstituteGroupNameAttributeTypeID,
          SubstitutesConstants.SubstituteNumberAttributeTypeID,
          SubstitutesConstants.SubstituteNameAttributeTypeID,
          SubstitutesConstants.DesignActualVariantAttributeTypeID,
          SubstitutesConstants.SubstitutePositionTypeAttributeTypeID
        };
        foreach (int AttributeID in numArray)
          dbRelation2.Attributes.FindByID(AttributeID)?.Delete(0L);
      }
      customService.Commit();
    }
    catch
    {
      customService.Rollback();
      throw;
    }
  }
}
