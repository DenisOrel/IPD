
// Type: Intermech.Search.Data.Repositories.RelationObjectRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search.Data.Adapters;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Search.Data.Repositories
{
    /// <summary>Репозиторий связей/объектов</summary>
    public class RelationObjectRepository : IRelationObjectRepository
    {
      /// <summary>Искать часть состава</summary>
      /// <param name="relationID">Идентификатор связи</param>
      /// <returns>Часть состава</returns>
      public RelationObject FindCompositionPart(long relationID)
      {
        long partVersionID = !RelationHelper.IsUnknownRelationID(relationID) ? this.GetExplicitPartVersionID(relationID) : throw new ArgumentException();
        return partVersionID == 0L ? (RelationObject) null : this.FindCompositionPart(relationID, partVersionID);
      }

      /// <summary>Искать часть состава</summary>
      /// <param name="relationID">Идентификатор связи</param>
      /// <param name="partVersionID">Идентификатор версии дочернего объекта</param>
      /// <returns>Часть состава</returns>
      public RelationObject FindCompositionPart(long relationID, long partVersionID)
      {
        if (RelationHelper.IsUnknownRelationID(relationID))
          throw new ArgumentException();
        if (ObjectHelper.IsUnknownObjectVersionID(partVersionID))
          throw new ArgumentException();
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
        ref DBRecordSetParams local = ref dbRecordSetParams;
        ConditionStructure[] conditionStructureArray = new ConditionStructure[2];
        ConditionStructure conditionStructure = new ConditionStructure();
        conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_PRJLINK_ID;
        conditionStructure.RelationalOperator = RelationalOperators.Equal;
        conditionStructure.Value = (object) relationID;
        conditionStructure.LogicalOperator = LogicalOperators.AND;
        conditionStructure.SQL = "";
        conditionStructureArray[0] = conditionStructure;
        conditionStructure = new ConditionStructure();
        conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
        conditionStructure.RelationalOperator = RelationalOperators.In;
        conditionStructure.Value = (object) new object[2]
        {
          (object) partVersionID,
          (object) (-1L * partVersionID)
        };
        conditionStructure.SQL = "";
        conditionStructureArray[1] = conditionStructure;
        local.Conditions = conditionStructureArray;
        dbRecordSetParams.Columns = this.GetDefaultColumns();
        dbRecordSetParams.RecordCount = -1;
        DBRecordSetParams @params = dbRecordSetParams;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          IDBRelation relation = session.GetRelation(relationID, true);
          IDBRelationCollection relationCollection = session.GetRelationCollection(-1);
          relationCollection.FiltrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545";
          if (MetaDataHelper.GetObjectType(session.GetObject(Math.Abs(partVersionID), true).ObjectType).IsLocalType)
            relationCollection.LocalTypesMode = true;
          DataTable dataTable = relationCollection.ConsistFrom(@params, relation.ProjID);
          return this.CreateRelationObjectCollection(ref @params, dataTable)[0];
        }
      }

      /// <summary>Искать состав</summary>
      /// <param name="options">Опции</param>
      /// <returns>Состав</returns>
      public RelationObjectCollection FindComposition(
        RelationObjectRepository.FindCompositionOptions options)
      {
        if (options == null)
          throw new ArgumentNullException(nameof (options));
        if (!this.IsValidOptions(options))
          throw new ArgumentException();
        DBRecordSetParams @params = new DBRecordSetParams()
        {
          Columns = this.GetDefaultColumns(),
          RecordCount = -1
        };
        List<RelationObject> collection = new List<RelationObject>();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(options.RelationTypeID);
          relationCollection.FiltrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545";
          foreach (long projectVersionId in options.ProjectVersionIds)
          {
            DataTable dataTable = relationCollection.ConsistFrom(@params, projectVersionId);
            collection.AddRange((IEnumerable<RelationObject>) this.CreateRelationObjectCollection(ref @params, dataTable));
          }
        }
        return new RelationObjectCollection((IEnumerable<RelationObject>) collection);
      }

      private bool IsValidOptions(
        RelationObjectRepository.FindCompositionOptions options)
      {
        return options.ProjectVersionIds != null && options.ProjectVersionIds.Count != 0;
      }

      private object[] GetDefaultColumns()
      {
        return new object[9]
        {
          (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
          (object) ObligatoryObjectAttributes.F_PART_ID,
          (object) ObligatoryObjectAttributes.F_PROJ_ID,
          (object) ObligatoryObjectAttributes.F_RELATION_TYPE,
          (object) Constants.ExplicitPartVersionIDAttributeTypeID,
          (object) ObligatoryObjectAttributes.F_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          (object) ObligatoryObjectAttributes.F_LC_STEP
        };
      }

      private long GetExplicitPartVersionID(long relationID)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute attributeById = sessionKeeper.Session.GetRelation(relationID, true).GetAttributeByID(Constants.ExplicitPartVersionIDAttributeTypeID);
          return attributeById != null ? attributeById.AsInteger : 0L;
        }
      }

      private List<RelationObject> CreateRelationObjectCollection(
        ref DBRecordSetParams @params,
        DataTable dataTable)
      {
        List<RelationObject> objectCollection = new List<RelationObject>();
        RecordSetParamsAdapter params1 = new RecordSetParamsAdapter(@params);
        IAttributeValueConverter attributeValueConverter = ServiceLocator.Get<IAttributeValueConverter>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          AttributeCollectionDataRowAdapter attributes = new AttributeCollectionDataRowAdapter(row, (IRecordSetParamsAdapter) params1, attributeValueConverter);
          _Object @object = new _Object((IAttributeCollection) attributes);
          Relation relation = new Relation((IAttributeCollection) attributes);
          objectCollection.Add(new RelationObject(relation, @object));
        }
        return objectCollection;
      }

      public sealed class FindCompositionOptions
      {
        public FindCompositionOptions() => this.RelationTypeID = -1;

        public List<long> ProjectVersionIds { get; set; }

        public int RelationTypeID { get; set; }
      }
    }
}
