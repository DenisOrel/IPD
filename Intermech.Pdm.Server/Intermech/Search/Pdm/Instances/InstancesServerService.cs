// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Instances.InstancesServerService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Search.Pdm.Instances;

public sealed class InstancesServerService : LongLifeObject, IInstancesServerService
{
  public long[] FindInstances(Guid userSessionGuid, long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? this.FindInstancesInternal(objectVersionID) : throw new ArgumentException();
  }

  public long[] CreateInstances(Guid userSessionGuid, CreateInstancesParams createInstancesParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (createInstancesParams == null)
        throw new ArgumentNullException(nameof (createInstancesParams));
      return CreateInstancesParams.CheckCreateInstancesParams(createInstancesParams) ? this.CreateInstancesInternal(createInstancesParams) : throw new ArgumentException();
    }
  }

  public void MakeInstance(
    Guid userSessionGuid,
    long objectVersionID,
    long needingMakeInstanceVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(needingMakeInstanceVersionID))
        throw new ArgumentException();
      this.MakeInstance(objectVersionID, needingMakeInstanceVersionID);
    }
  }

  private long[] FindInstancesInternal(long objectVersionID)
  {
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionID);
      IDBAttribute attributeById = dbObject.GetAttributeByID(InstancesConstants.GroupProductIDAttributeTypeID);
      Guid result = Guid.Empty;
      if (attributeById != null)
      {
        if (Guid.TryParse(attributeById.AsString, out result))
        {
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(dbObject.TypeID);
          objectCollection.ShowAllModifications = true;
          DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
          dbRecordSetParams.Columns = new object[1]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID
          };
          // ISSUE: explicit reference operation
          (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
          {
            new ConditionStructure()
            {
              Attribute = (object) InstancesConstants.GroupProductIDAttributeTypeID,
              RelationalOperator = RelationalOperators.Equal,
              Value = (object) result,
              SQL = string.Empty
            }
          };
          dbRecordSetParams.RecordCount = -1;
          DBRecordSetParams paramSet = dbRecordSetParams;
          foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
          {
            long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
            if (int64Value != objectVersionID)
              longList.Add(int64Value);
          }
        }
      }
    }
    return longList.ToArray();
  }

  private long[] CreateInstancesInternal(CreateInstancesParams createInstancesParams)
  {
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService1 = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
      customService1.StartTransaction();
      try
      {
        long prototypeVersionId = createInstancesParams.Blanks[0].PrototypeVersionID;
        IDBObject objectActualCopy = sessionKeeper.Session.GetObject(prototypeVersionId);
        IDBAttribute attributeById1 = objectActualCopy.GetAttributeByID(InstancesConstants.GroupProductIDAttributeTypeID);
        Guid result;
        if (attributeById1 == null || !Guid.TryParse(attributeById1.AsString, out result))
        {
          result = Guid.NewGuid();
          bool flag = false;
          if (objectActualCopy.ObjectModifyMode == ObjectModifyModes.Checkout && ObjectHelper.IsUnknownObjectID(objectActualCopy.CheckoutBy))
          {
            objectActualCopy.CheckOut();
            objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(prototypeVersionId, true);
            flag = true;
          }
          objectActualCopy.SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(InstancesConstants.GroupProductIDAttributeTypeID, (object) result)
          });
          if (flag)
          {
            objectActualCopy.CheckIn();
            objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(prototypeVersionId, true);
          }
        }
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectActualCopy.ObjectType);
        foreach (InstanceBlank blank in createInstancesParams.Blanks)
        {
          IDBObject version;
          if (!ObjectHelper.IsUnknownObjectVersionID(blank.BasedOnVersionID))
          {
            IGroupInstanceService customService2 = sessionKeeper.Session.GetCustomService(typeof (IGroupInstanceService)) as IGroupInstanceService;
            Guid sessionGuid = sessionKeeper.Session.SessionGUID;
            customService2.AddIgnoreSessionGuid(sessionGuid);
            try
            {
              version = objectCollection.CreateVersion(blank.BasedOnVersionID);
            }
            finally
            {
              customService2.RemoveIgnoreSessionGuid(sessionGuid);
            }
          }
          else if (blank.CopyCompositionAndAttributesOfPrototype)
          {
            version = objectCollection.Create(objectActualCopy);
            this.CopyComposition(objectActualCopy.ObjectID, version.ObjectID, MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545"));
            this.CopyComposition(objectActualCopy.ObjectID, version.ObjectID, MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"));
          }
          else
          {
            version = objectCollection.Create();
            IDBAttribute attributeById2 = objectActualCopy.GetAttributeByID(InstancesConstants.NameAttributeTypeID);
            if (attributeById2 != null)
              version.SetAttributesValues(new AttributeValues[1]
              {
                new AttributeValues(InstancesConstants.NameAttributeTypeID, attributeById2.Value)
              });
          }
          version.SetAttributesValues(new AttributeValues[2]
          {
            new AttributeValues(InstancesConstants.DesignationAttributeTypeID, (object) blank.Designation),
            new AttributeValues(InstancesConstants.GroupProductIDAttributeTypeID, (object) result)
          });
          version.CommitCreation(true, true);
          longList.Add(version.ObjectID);
        }
        customService1.Commit();
      }
      catch
      {
        customService1.Rollback();
        throw;
      }
    }
    return longList.ToArray();
  }

  private void CopyComposition(long prototypeVersionID, long objectVersionID, int relationTypeID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeID);
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
        (object) ObligatoryObjectAttributes.F_PART_ID
      });
      foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, prototypeVersionID).Rows)
      {
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, 1, 0L);
        if (sessionKeeper.Session.GetRelation(objectVersionID, int64Value2, relationTypeID) == null)
        {
          NewRelationProperties properties = new NewRelationProperties(int64Value1, objectVersionID, int64Value2);
          relationCollection.Create(properties);
        }
      }
    }
  }

  private void MakeInstance(long objectVersionID, long needingMakeInstanceVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionID);
      IDBAttribute attributeById = dbObject.GetAttributeByID(InstancesConstants.GroupProductIDAttributeTypeID);
      Guid result;
      if (attributeById == null || !Guid.TryParse(attributeById.AsString, out result))
      {
        result = Guid.NewGuid();
        this.SetGroupProductID(dbObject, result);
      }
      this.SetGroupProductID(needingMakeInstanceVersionID, result);
    }
  }

  private void SetGroupProductID(IDBObject dbObject, Guid groupProductID)
  {
    dbObject.SetAttributesValues(new AttributeValues[1]
    {
      new AttributeValues(InstancesConstants.GroupProductIDAttributeTypeID, (object) groupProductID)
    });
  }

  private void SetGroupProductID(long objectVersionID, Guid groupProductID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.SetGroupProductID(sessionKeeper.Session.GetObject(objectVersionID), groupProductID);
  }
}
