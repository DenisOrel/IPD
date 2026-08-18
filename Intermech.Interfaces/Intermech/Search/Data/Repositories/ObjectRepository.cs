
// Type: Intermech.Search.Data.Repositories.ObjectRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Search.Data.Repositories
{
    public sealed class ObjectRepository : AttributableRepositoryBase, IObjectRepository
    {
      private LazyService<ITypeProvider> _typeProvider = new LazyService<ITypeProvider>();

      public long AddOrUpdate(_Object @object)
      {
        if (@object == null)
          throw new ArgumentNullException("@object");
        if (@object.TypeID == -1)
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
          customService.StartTransaction();
          try
          {
            IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(@object.TypeID);
            IDBObject dbObject = ObjectHelper.IsUnknownObjectVersionID(@object.VersionID) ? (ObjectHelper.IsUnknownObjectID(@object.ID) ? objectCollection.Create() : objectCollection.CreateVersion(sessionKeeper.Session.GetObjectByID(@object.ID, true).ObjectID)) : sessionKeeper.Session.GetObject(@object.VersionID);
            AttributeValues[] array = @object.Attributes.Where<_Attribute>((System.Func<_Attribute, bool>) (o => o.TypeID != MetaDataHelper.GetAttributeTypeID("cad0013a-306c-11d8-b4e9-00304f19f545"))).Select<_Attribute, AttributeValues>((System.Func<_Attribute, AttributeValues>) (o => new AttributeValues(o.TypeID, o.Value))).ToArray<AttributeValues>();
            dbObject.SetAttributesValues(array);
            if (dbObject.IsCreationMode)
              dbObject.CommitCreation(false, false);
            long objectId = dbObject.ObjectID;
            customService.Commit();
            return objectId;
          }
          catch
          {
            customService.Rollback();
            throw;
          }
        }
      }

      public _Object Find(long objectVersionID, bool includeBlobs = true)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject attributable = sessionKeeper.Session.GetObject(objectVersionID);
          GetAttributeValuesModes modes = GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions;
          if (includeBlobs)
            modes |= GetAttributeValuesModes.IncludeBlobs;
          AttributeValues[] attributesValues = attributable.GetAttributesValues(modes);
          _Object @object = this.CreateObject(attributable.ObjectType);
          foreach (AttributeValues attributeValue in attributesValues)
            @object.Attributes.Add(new _Attribute(attributeValue.AttributeID)
            {
              IsReadOnly = new bool?(attributeValue.ReadOnly),
              Value = this.GetAttributeValue((IDBAttributable) attributable, attributeValue)
            });
          try
          {
            @object.Attributes.Add(new _Attribute(ObligatoryObjectAttributes.F_MODIFY_DATE)
            {
              Value = (object) attributable.ModifyDate,
              IsReadOnly = new bool?(true)
            });
          }
          catch
          {
          }
          return @object;
        }
      }

      public List<_Object> Find(int objectTypeID)
      {
        return this.Find(objectTypeID, (List<ConditionStructure>) null);
      }

      public List<_Object> Find(List<ConditionStructure> conditions)
      {
        throw new NotImplementedException();
      }

      public List<_Object> Find(int objectTypeID, List<ConditionStructure> conditions)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectTypeID);
          objectCollection.ShowAllModifications = true;
          IAttributeTypeForObjectRepository objectRepository = ServiceLocator.Get<IAttributeTypeForObjectRepository>();
          List<int> intList = new List<int>();
          intList.AddRange(new List<ObligatoryObjectAttributes>()
          {
            ObligatoryObjectAttributes.F_BASE_VERSION,
            ObligatoryObjectAttributes.F_CHKOUT_BY,
            ObligatoryObjectAttributes.F_GUID,
            ObligatoryObjectAttributes.F_ID,
            ObligatoryObjectAttributes.F_LC_STEP,
            ObligatoryObjectAttributes.F_LEVEL_ID,
            ObligatoryObjectAttributes.F_MODIFICATION_ID,
            ObligatoryObjectAttributes.F_MODIFY_DATE,
            ObligatoryObjectAttributes.F_OBJ_CREATE,
            ObligatoryObjectAttributes.F_OBJ_GUID,
            ObligatoryObjectAttributes.F_OBJECT_ID,
            ObligatoryObjectAttributes.F_OBJECT_TYPE,
            ObligatoryObjectAttributes.F_OWNER_ID,
            ObligatoryObjectAttributes.F_VERSION_ID,
            ObligatoryObjectAttributes.CAPTION
          }.Cast<int>());
          intList.AddRange(objectRepository.Find(objectTypeID).Select<IMSAttribute4ObjectType, int>((System.Func<IMSAttribute4ObjectType, int>) (o => o.AttributeID)));
          return this.CreateObjectsFromDataTable(objectCollection.Select(new DBRecordSetParams()
          {
            Columns = intList.Cast<object>().ToArray<object>(),
            RecordCount = -1,
            Conditions = conditions == null || conditions.Count <= 0 ? (ConditionStructure[]) null : conditions.ToArray()
          }), intList);
        }
      }

      public List<_Object> Find(
        FindObjectCollectionOptions findObjectCollectionParams)
      {
        DBRecordSetParams dbRecordSetParams1 = findObjectCollectionParams != null ? this.CreateRecordSetParamsFromFindObjectCollectionParams(findObjectCollectionParams) : throw new ArgumentNullException("options");
        int[] fromRecordSetParams = this.GetAttributeTypeIdsFromRecordSetParams(dbRecordSetParams1);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (ObjectTypeHelper.IsUnknownObjectTypeID(findObjectCollectionParams.ObjectTypeID))
          {
            Dictionary<int, List<long>> dictionary;
            if (findObjectCollectionParams.ObjectVersionIdsByObjectTypeIDDictionary != null)
            {
              dictionary = findObjectCollectionParams.ObjectVersionIdsByObjectTypeIDDictionary;
            }
            else
            {
              dictionary = new Dictionary<int, List<long>>();
              IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
              objectCollection.LocalTypesMode = true;
              if (findObjectCollectionParams.DisableEditingContextFiltration)
                objectCollection.ShowAllModifications = true;
              DBRecordSetParams dbRecordSetParams2 = new DBRecordSetParams();
              dbRecordSetParams2.Columns = new object[2]
              {
                (object) ObligatoryObjectAttributes.F_OBJECT_ID,
                (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
              };
              // ISSUE: explicit reference operation
              (^ref dbRecordSetParams2).Conditions = new ConditionStructure[1]
              {
                new ConditionStructure()
                {
                  Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
                  RelationalOperator = RelationalOperators.In,
                  Value = (object) findObjectCollectionParams.ObjectVersionIds,
                  SQL = ""
                }
              };
              dbRecordSetParams2.RecordCount = -1;
              DBRecordSetParams paramSet = dbRecordSetParams2;
              foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
              {
                long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
                int int32Value = DataSetProcessor.GetInt32Value(row, 1, -1);
                List<long> longList = (List<long>) null;
                if (!dictionary.TryGetValue(int32Value, out longList))
                {
                  longList = new List<long>();
                  dictionary.Add(int32Value, longList);
                }
                longList.Add(int64Value);
              }
            }
            List<_Object> objectList = new List<_Object>();
            foreach (KeyValuePair<int, List<long>> keyValuePair in dictionary)
            {
              IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(keyValuePair.Key);
              findObjectCollectionParams.ObjectVersionIds = keyValuePair.Value.ToArray();
              DBRecordSetParams collectionParams = this.CreateRecordSetParamsFromFindObjectCollectionParams(findObjectCollectionParams);
              if (findObjectCollectionParams.DisableEditingContextFiltration)
                objectCollection.ShowAllModifications = true;
              DataTable dataTable = objectCollection.Select(collectionParams);
              objectList.AddRange((IEnumerable<_Object>) this.CreateObjectsFromDataTable(dataTable, ((IEnumerable<int>) fromRecordSetParams).ToList<int>()));
            }
            return objectList;
          }
          IDBObjectCollection objectCollection1 = sessionKeeper.Session.GetObjectCollection(findObjectCollectionParams.ObjectTypeID);
          if (findObjectCollectionParams.DisableEditingContextFiltration)
            objectCollection1.ShowAllModifications = true;
          return this.CreateObjectsFromDataTable(objectCollection1.Select(dbRecordSetParams1), ((IEnumerable<int>) fromRecordSetParams).ToList<int>());
        }
      }

      public void Remove(long objectVersionID)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          sessionKeeper.Session.GetObject(objectVersionID).Delete(0L);
      }

      public void Remove(int objectTypeID) => throw new NotImplementedException();

      public void Remove(List<ConditionStructure> conditions) => throw new NotImplementedException();

      public int FindCount(int objectTypeID) => throw new NotImplementedException();

      public int FindCount(FindObjectCountOptions options) => throw new NotImplementedException();

      private List<_Object> CreateObjectsFromDataTable(DataTable dataTable, List<int> attributeTypeIds)
      {
        return dataTable.Rows.Cast<DataRow>().Select<DataRow, _Object>((System.Func<DataRow, _Object>) (o => this.CreateObjectFromDataRow(o, attributeTypeIds))).ToList<_Object>();
      }

      private _Object CreateObjectFromDataRow(DataRow dataRow, List<int> attributeTypeIds)
      {
        IAttributeCollection collectionFromDataRow = this.CreateAttributeCollectionFromDataRow(dataRow, attributeTypeIds);
        _Object objectFromDataRow = this.CreateObject((int) (collectionFromDataRow.Where<_Attribute>((System.Func<_Attribute, bool>) (o => o.TypeID == -7)).FirstOrDefault<_Attribute>() ?? throw new Exception()).Value);
        objectFromDataRow.Attributes.AddRange((IEnumerable<_Attribute>) collectionFromDataRow);
        return objectFromDataRow;
      }

      private _Object CreateObject(int objectTypeID)
      {
        Type type = this._typeProvider.Value.GetObjectType(objectTypeID);
        if ((object) type == null)
          type = typeof (_Object);
        return Activator.CreateInstance(type) as _Object;
      }

      private DBRecordSetParams CreateRecordSetParamsFromFindObjectCollectionParams(
        FindObjectCollectionOptions findObjectCollectionParams)
      {
        List<int> source = new List<int>() { -2, -7 };
        if (findObjectCollectionParams.AttributeTypeIds != null)
        {
          foreach (int attributeTypeId in findObjectCollectionParams.AttributeTypeIds)
          {
            if (!source.Contains(attributeTypeId))
            {
              bool flag = AttributeTypeHelper.IsSystemAttributeTypeID(attributeTypeId);
              if (!flag || flag && ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeTypeId) == AttributeSourceTypes.Object)
                source.Add(attributeTypeId);
            }
          }
        }
        if (findObjectCollectionParams.SortAttributeTypeIds != null)
        {
          foreach (int sortAttributeTypeId in findObjectCollectionParams.SortAttributeTypeIds)
          {
            if (!source.Contains(sortAttributeTypeId))
              source.Add(sortAttributeTypeId);
          }
        }
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
        if (findObjectCollectionParams.Conditions != null)
          conditionStructureList.AddRange((IEnumerable<ConditionStructure>) findObjectCollectionParams.Conditions);
        if (findObjectCollectionParams.ObjectVersionIds != null)
          conditionStructureList.Add(new ConditionStructure()
          {
            Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
            RelationalOperator = RelationalOperators.In,
            Value = (object) findObjectCollectionParams.ObjectVersionIds,
            LogicalOperator = LogicalOperators.AND,
            SQL = ""
          });
        DBRecordSetParams collectionParams = new DBRecordSetParams()
        {
          Columns = source.Cast<object>().ToArray<object>(),
          RecordCount = -1
        };
        if (conditionStructureList.Count > 0)
          collectionParams.Conditions = conditionStructureList.ToArray();
        if (findObjectCollectionParams.SortAttributeTypeIds != null && findObjectCollectionParams.SortAttributeTypeIds.Count > 0 && findObjectCollectionParams.SortDirections != null && findObjectCollectionParams.SortDirections.Count > 0)
        {
          collectionParams.SortColumns = findObjectCollectionParams.SortAttributeTypeIds.Cast<object>().ToArray<object>();
          collectionParams.Orders = findObjectCollectionParams.SortDirections.ToArray();
        }
        return collectionParams;
      }

      private int[] GetAttributeTypeIdsFromRecordSetParams(DBRecordSetParams recordSetParams)
      {
        return recordSetParams.Columns.Cast<int>().ToArray<int>();
      }
    }
}
