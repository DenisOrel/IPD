
// Type: Intermech.Search.Classifiers.ClassifiersHelper
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


namespace Intermech.Search.Classifiers
{
    public static class ClassifiersHelper
    {
      public static long[] GetRootClassifierVersionIdsForObjects(long[] objectVersionIds)
      {
        if (objectVersionIds == null)
          throw new ArgumentNullException(nameof (objectVersionIds));
        if (ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds))
          throw new ArgumentException();
        long[] first = (long[]) null;
        foreach (long objectVersionId in objectVersionIds)
        {
          long[] versionIdsForObject = ClassifiersHelper.GetRootClassifierVersionIdsForObject(objectVersionId);
          first = first != null ? ((IEnumerable<long>) first).Intersect<long>((IEnumerable<long>) versionIdsForObject).ToArray<long>() : ((IEnumerable<long>) versionIdsForObject).ToArray<long>();
        }
        return first;
      }

      public static long[] GetRootClassifierVersionIdsForObject(long objectVersionID)
      {
        if (ObjectHelper.IsUnknownObjectID(objectVersionID))
          throw new ArgumentException();
        List<long> longList = new List<long>();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionID);
          foreach (int classifierObjectTypeId in ClassifiersConstants.RootClassifierObjectTypeIds)
          {
            IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(classifierObjectTypeId);
            DBRecordSetParams paramSet = new DBRecordSetParams()
            {
              Columns = new object[1]
              {
                (object) ObligatoryObjectAttributes.F_OBJECT_ID
              },
              RecordCount = -1,
              Tags = new HybridDictionary()
              {
                {
                  (object) "{7FB30639-2F65-4407-B78E-523547B1B133}",
                  (object) true
                }
              }
            };
            List<ConditionStructure> conditionStructureList1 = new List<ConditionStructure>();
            ConditionStructure conditionStructure1 = new ConditionStructure();
            conditionStructure1.Attribute = (object) ClassifiersConstants.ClassifierTypeAttributeTypeID;
            conditionStructure1.RelationalOperator = RelationalOperators.Equal;
            conditionStructure1.Value = (object) 4;
            conditionStructure1.SQL = string.Empty;
            conditionStructure1.LogicalOperator = LogicalOperators.OR;
            conditionStructureList1.Add(conditionStructure1);
            conditionStructure1 = new ConditionStructure();
            conditionStructure1.Attribute = (object) ClassifiersConstants.ObjectTypeGuidsAttributeTypeID;
            conditionStructure1.RelationalOperator = RelationalOperators.Equal;
            conditionStructure1.Value = (object) MetaDataHelper.GetAttributeTypeGuid(dbObject.TypeID);
            conditionStructure1.SQL = string.Empty;
            conditionStructure1.LogicalOperator = LogicalOperators.OR;
            conditionStructureList1.Add(conditionStructure1);
            List<ConditionStructure> conditionStructureList2 = conditionStructureList1;
            if (((IEnumerable<int>) ClassifiersConstants.AllDocumentsObjectTypeIds).Contains<int>(dbObject.TypeID))
            {
              List<ConditionStructure> conditionStructureList3 = conditionStructureList2;
              conditionStructure1 = new ConditionStructure();
              conditionStructure1.Attribute = (object) ClassifiersConstants.ClassifierTypeAttributeTypeID;
              conditionStructure1.RelationalOperator = RelationalOperators.Equal;
              conditionStructure1.Value = (object) 2;
              conditionStructure1.SQL = string.Empty;
              conditionStructure1.LogicalOperator = LogicalOperators.OR;
              ConditionStructure conditionStructure2 = conditionStructure1;
              conditionStructureList3.Add(conditionStructure2);
              IDBAttribute attributeById = dbObject.GetAttributeByID(ClassifiersConstants.ArchiveAttributeTypeID);
              long asInteger = attributeById != null ? attributeById.AsInteger : 0L;
              if (!ObjectHelper.IsUnknownObjectID(asInteger))
              {
                List<ConditionStructure> conditionStructureList4 = conditionStructureList2;
                conditionStructure1 = new ConditionStructure();
                conditionStructure1.Attribute = (object) ClassifiersConstants.ArchivesAttributeTypeID;
                conditionStructure1.RelationalOperator = RelationalOperators.Equal;
                conditionStructure1.Value = (object) (int) asInteger;
                conditionStructure1.SQL = string.Empty;
                conditionStructure1.LogicalOperator = LogicalOperators.OR;
                ConditionStructure conditionStructure3 = conditionStructure1;
                conditionStructureList4.Add(conditionStructure3);
              }
            }
            paramSet.Conditions = conditionStructureList2.ToArray();
            foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
            {
              long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
              if (!ObjectHelper.IsUnknownObjectID(int64Value) && !longList.Contains(int64Value))
                longList.Add(int64Value);
            }
          }
        }
        return longList.ToArray();
      }

      public static string GetPathForClassifier(long classifierVersionID)
      {
        if (ObjectHelper.IsUnknownObjectID(classifierVersionID))
          throw new ArgumentNullException(nameof (classifierVersionID));
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          string pathForClassifier = sessionKeeper.Session.GetObject(classifierVersionID).Caption;
          long num = classifierVersionID;
          do
          {
            IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(ClassifiersConstants.SimpleRelationWithSortingRelationTypeID);
            relationCollection.ChildObjectTypes = (IList<int>) new List<int>()
            {
              ClassifiersConstants.CommonClassifierObjectTypeID,
              ClassifiersConstants.PersonalClassifierObjectTypeID,
              ClassifiersConstants.ClassifierFolderObjectTypeID
            };
            DBRecordSetParams paramSet = new DBRecordSetParams()
            {
              Columns = new object[2]
              {
                (object) ObligatoryObjectAttributes.CAPTION,
                (object) ObligatoryObjectAttributes.F_OBJECT_ID
              }
            };
            DataTable dataTable = relationCollection.EntersInVersion(paramSet, num);
            if (dataTable.Rows.Count > 0)
            {
              pathForClassifier = $"{DataSetProcessor.GetStringValue(dataTable.Rows[0], 0, string.Empty)}//{pathForClassifier}";
              num = DataSetProcessor.GetInt64Value(dataTable.Rows[0], 1, 0L);
            }
            else
              break;
          }
          while (!ObjectHelper.IsUnknownObjectID(num));
          return pathForClassifier;
        }
      }
    }
}
