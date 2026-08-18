
// Type: Intermech.Search.Data.Repositories.RelationRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;


namespace Intermech.Search.Data.Repositories
{
    public sealed class RelationRepository : AttributableRepositoryBase, IRelationRepository
    {
      private LazyService<IAttributeTypeForRelationRepository> _attributeTypeForRelationRepository = new LazyService<IAttributeTypeForRelationRepository>();

      public long AddOrUpdate(Relation relation)
      {
        if (relation == null)
          throw new ArgumentNullException(nameof (relation));
        if (RelationHelper.IsUnknownRelationID(relation.ID) && relation.TypeID == -1)
          throw new ArgumentException();
        if (relation.TypeID == -1)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBRelation relation1 = sessionKeeper.Session.GetRelation(relation.ID);
            relation.TypeID = relation1.TypeID;
          }
        }
        object attributeValue = relation.Attributes.GetAttributeValue(ObligatoryObjectAttributes.F_INTEGER_VALUE);
        num = 0L;
        if (!(attributeValue is long num))
          ;
        List<_Attribute> list = relation.Attributes.Where<_Attribute>((System.Func<_Attribute, bool>) (o => !AttributeTypeHelper.IsSystemAttributeTypeID(o.TypeID) && this._attributeTypeForRelationRepository.Value.Find(new AttributeTypeForRelationKey(o.TypeID, relation.TypeID)) != null)).ToList<_Attribute>();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relation.TypeID);
          if (RelationHelper.IsUnknownRelationID(relation.ID))
          {
            AttributeValues[] attributeCollection = this.CreateAttributesValuesFromAttributeCollection((IEnumerable<_Attribute>) list);
            return relationCollection.Create(new NewRelationProperties()
            {
              PartID = relation.PartID,
              ProjectObjectID = relation.ProjectVersionID,
              PrototypeRelationID = num,
              ValuesList = attributeCollection,
              EndDate = DateTime.MaxValue,
              PartObjectID = relation.PartVersionID
            }).RelationID;
          }
          IDBRelation relation2 = sessionKeeper.Session.GetRelation(relation.ID);
          foreach (_Attribute attribute in list.ToList<_Attribute>())
          {
            if (attribute.Value == null)
            {
              IMSAttribute4RelationType attribute4RelationType = this._attributeTypeForRelationRepository.Value.Find(new AttributeTypeForRelationKey(attribute.TypeID, relation.TypeID));
              if (attribute4RelationType.Required == RequiredModes.Manual)
              {
                relation2.Attributes.FindByID(attribute.TypeID)?.Delete(0L);
                list.Remove(attribute);
              }
              else if (attribute4RelationType.Options.HasFlag((Enum) AttributeOptions.DisableNulls))
              {
                if (attribute4RelationType.FieldType == FieldTypes.ftDouble)
                  attribute.Value = (object) 0.0;
                else if (attribute4RelationType.FieldType == FieldTypes.ftGuid)
                  attribute.Value = (object) Guid.Empty;
                else if (attribute4RelationType.FieldType == FieldTypes.ftInteger)
                  attribute.Value = (object) 0L;
                else if (attribute4RelationType.FieldType == FieldTypes.ftObjectLink)
                  attribute.Value = (object) 0L;
                else if (attribute4RelationType.FieldType == FieldTypes.ftString)
                  attribute.Value = (object) "";
                else
                  list.Remove(attribute);
              }
            }
          }
          AttributeValues[] attributeCollection1 = this.CreateAttributesValuesFromAttributeCollection((IEnumerable<_Attribute>) list);
          relation2.SetAttributesValues(attributeCollection1);
          return relation2.RelationID;
        }
      }

      public Relation Find(long relationID)
      {
        if (RelationHelper.IsUnknownRelationID(relationID))
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(relationID);
          AttributeValues[] attributesValues = relation.GetAttributesValues(GetAttributeValuesModes.IncludeBlobs | GetAttributeValuesModes.IncludeObligatoryAttributes);
          return new Relation(this.CreateAttributeCollectionFromAttributesValues(relation, attributesValues));
        }
      }

      public List<Relation> Find(List<ConditionStructure> conditions)
      {
        throw new NotImplementedException();
      }

      public List<Relation> Find(int relationTypeID) => throw new NotImplementedException();

      public List<Relation> Find(FindRelationsParams @params)
      {
        if (@params == null)
          throw new ArgumentNullException("@params");
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(@params.RelationTypeID);
          if (object.Equals(@params.Conditions[0].Attribute, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE))
          {
            int num = (int) @params.Conditions[0].Value;
            relationCollection.ObjectTypeID = num;
          }
          else
            relationCollection.LocalTypesMode = true;
          List<int> attributeTypeIds = new List<int>();
          attributeTypeIds.AddRange(new List<ObligatoryObjectAttributes>()
          {
            ObligatoryObjectAttributes.F_PRJLINK_ID,
            ObligatoryObjectAttributes.F_PROJ_ID,
            ObligatoryObjectAttributes.F_PART_ID,
            ObligatoryObjectAttributes.F_RELATION_TYPE
          }.Cast<int>());
          attributeTypeIds.AddRange((IEnumerable<int>) this.GetAllAllowableAttributeTypes(@params.RelationTypeID));
          DBRecordSetParams paramSet = new DBRecordSetParams()
          {
            Columns = attributeTypeIds.Cast<object>().ToArray<object>(),
            Conditions = this.PrepareConditions(((IEnumerable<ConditionStructure>) @params.Conditions).ToList<ConditionStructure>())
          };
          if (@params.DisableFiltration)
          {
            paramSet.Tags = new HybridDictionary();
            paramSet.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] = (object) true;
            paramSet.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) true;
            paramSet.Tags[(object) "{529FFE92-FDA7-48B8-AADF-ADB1EE6EF584}"] = (object) true;
            paramSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) true;
          }
          return relationCollection.Select(paramSet).Rows.Cast<DataRow>().Select<DataRow, Relation>((System.Func<DataRow, Relation>) (o => new Relation(this.CreateAttributeCollectionFromDataRow(o, attributeTypeIds)))).ToList<Relation>();
        }
      }

      private int[] GetAllAllowableAttributeTypes(int relationType)
      {
        List<int> intList = new List<int>();
        IAttributeTypeForRelationRepository relationRepository = ServiceLocator.Get<IAttributeTypeForRelationRepository>();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (IMSAttribute4RelationType attribute4RelationType in relationRepository.Find(relationType))
          {
            if (sessionKeeper.Session.GetAttributeType(attribute4RelationType.AttributeID) is IDBSecurity attributeType && attributeType.CheckAccess(ActionType.List, true, false))
              intList.Add(attribute4RelationType.AttributeID);
          }
        }
        return intList.ToArray();
      }

      public List<Relation> Find(int relationTypeID, List<ConditionStructure> conditions)
      {
        return relationTypeID != -1 ? this.Find(new FindRelationsParams()
        {
          Conditions = conditions?.ToArray(),
          DisableFiltration = false,
          RelationTypeID = relationTypeID
        }) : throw new ArgumentException();
      }

      public List<Relation> FindCollection(List<long> projectVersionIds)
      {
        if (projectVersionIds == null)
          throw new ArgumentNullException(nameof (projectVersionIds));
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          List<Relation> collection = new List<Relation>();
          foreach (DataRow row in (InternalDataCollectionBase) (session.GetCustomService(typeof (IRelationRepositoryServerService)) as IRelationRepositoryServerService).Select(session.SessionGUID, projectVersionIds).Rows)
          {
            long int64Value1 = DataSetProcessor.GetInt64Value(row, "F_PRJLINK_ID", 0L);
            long int64Value2 = DataSetProcessor.GetInt64Value(row, "F_PROJ_ID", 0L);
            long int64Value3 = DataSetProcessor.GetInt64Value(row, "F_PART_ID", 0L);
            int int32Value = DataSetProcessor.GetInt32Value(row, "F_RELATION_TYPE", -1);
            Relation relation = new Relation();
            relation.Attributes.AddRange((IEnumerable<_Attribute>) new _Attribute[4]
            {
              new _Attribute(ObligatoryObjectAttributes.F_PRJLINK_ID)
              {
                Value = (object) int64Value1
              },
              new _Attribute(ObligatoryObjectAttributes.F_PROJ_ID)
              {
                Value = (object) int64Value2
              },
              new _Attribute(ObligatoryObjectAttributes.F_PART_ID)
              {
                Value = (object) int64Value3
              },
              new _Attribute(ObligatoryObjectAttributes.F_RELATION_TYPE)
              {
                Value = (object) int32Value
              }
            });
            collection.Add(relation);
          }
          return collection;
        }
      }

      public void Remove(long relationID)
      {
        if (RelationHelper.IsUnknownRelationID(relationID))
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          sessionKeeper.Session.GetRelation(relationID).Delete(0L);
      }

      public void Remove(int relationTypeID) => throw new NotImplementedException();

      public void Remove(List<ConditionStructure> conditions)
      {
        if (conditions == null)
          throw new ArgumentNullException(nameof (conditions));
        if (conditions.Count == 0)
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(-1);
          DBRecordSetParams paramSet = new DBRecordSetParams(conditions.ToArray(), new object[1]
          {
            (object) ObligatoryObjectAttributes.F_PRJLINK_ID
          });
          foreach (DataRow row in (InternalDataCollectionBase) relationCollection.Select(paramSet).Rows)
          {
            long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
            sessionKeeper.Session.GetRelation(int64Value, false)?.Delete((long) Consts.PurgeMode);
          }
        }
      }

      private AttributeValues[] CreateAttributesValuesFromAttributeCollection(
        IEnumerable<_Attribute> attributeCollection)
      {
        return attributeCollection.Where<_Attribute>((System.Func<_Attribute, bool>) (o => !AttributeTypeHelper.IsSystemAttributeTypeID(o.TypeID))).Select<_Attribute, AttributeValues>((System.Func<_Attribute, AttributeValues>) (o => new AttributeValues(o.TypeID, o.Value))).ToArray<AttributeValues>();
      }

      private IAttributeCollection CreateAttributeCollectionFromAttributesValues(
        IDBRelation relation,
        AttributeValues[] attributesValues)
      {
        return (IAttributeCollection) new AttributeCollection(((IEnumerable<AttributeValues>) attributesValues).Select<AttributeValues, _Attribute>((System.Func<AttributeValues, _Attribute>) (o => new _Attribute(o.AttributeID)
        {
          Value = this.GetAttributeValue((IDBAttributable) relation, o)
        })));
      }

      private List<Relation> CreateRelationsFromDataTable(
        DataTable dataTable,
        List<int> attributeTypeIds)
      {
        return dataTable.Rows.Cast<DataRow>().Select<DataRow, Relation>((System.Func<DataRow, Relation>) (o => this.CreateRelationFromDataRow(o, attributeTypeIds))).ToList<Relation>();
      }

      private Relation CreateRelationFromDataRow(DataRow dataRow, List<int> attributeTypeIds)
      {
        return new Relation(this.CreateAttributeCollectionFromDataRow(dataRow, attributeTypeIds));
      }

      private ConditionStructure[] PrepareConditions(List<ConditionStructure> conditions)
      {
        return conditions.Select<ConditionStructure, ConditionStructure>((System.Func<ConditionStructure, ConditionStructure>) (o => new ConditionStructure()
        {
          Attribute = o.Attribute,
          RelationalOperator = o.RelationalOperator,
          Value = o.Value,
          LogicalOperator = o.LogicalOperator,
          SQL = o.SQL ?? ""
        })).ToArray<ConditionStructure>();
      }
    }
}
