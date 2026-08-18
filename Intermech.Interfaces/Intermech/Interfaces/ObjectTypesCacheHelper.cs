
// Type: Intermech.Interfaces.ObjectTypesCacheHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>Summary description for ObjectTypesCacheHelper.</summary>
    public class ObjectTypesCacheHelper
    {
      public static DataTable GetUsedByAttribute(
        DataTable attr4objTypes,
        DataTable objTypes,
        int attributeID)
      {
        StringBuilder stringBuilder = new StringBuilder();
        DataRow[] dataRowArray = attr4objTypes.Select("F_ATTRIBUTE_ID = " + attributeID.ToString());
        int columnIndex = attr4objTypes.Columns.IndexOf("F_OBJECT_TYPE");
        if (dataRowArray.Length == 0)
        {
          stringBuilder.Append("-1");
        }
        else
        {
          stringBuilder.Append(dataRowArray[0][columnIndex].ToString());
          for (int index = 1; index < dataRowArray.Length; ++index)
            stringBuilder.AppendFormat(",{0}", dataRowArray[index][columnIndex]);
        }
        DataTable usedByAttribute = objTypes.Clone();
        DataRow[] fromRows = objTypes.Select($"F_OBJECT_TYPE IN ({stringBuilder.ToString()})");
        DataSetProcessor.AssignRows(usedByAttribute, (IEnumerable<DataRow>) fromRows);
        usedByAttribute.Columns.Add("F_PUBLIC", Type.GetType("System.Int32"));
        DataSetProcessor.FillCaptions(usedByAttribute);
        foreach (DataRow row in (InternalDataCollectionBase) usedByAttribute.Rows)
        {
          int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
          foreach (DataRow dataRow in dataRowArray)
          {
            if (Convert.ToInt32(dataRow["F_OBJECT_TYPE"]) == int32)
            {
              row["F_PUBLIC"] = dataRow["F_PUBLIC"];
              break;
            }
          }
        }
        usedByAttribute.AcceptChanges();
        return usedByAttribute;
      }

      public static string GetParentSQL(
        DataTable objTypesTreeTable,
        int parentTypeID,
        int[] visibleIDs)
      {
        if (parentTypeID == -2)
          return string.Empty;
        if (parentTypeID > -1)
        {
          DataRow[] fromRows = objTypesTreeTable.Select("F_PARENT_ID = " + parentTypeID.ToString());
          if (fromRows.Length == 0)
            return "F_OBJECT_TYPE = -1";
          DataTable dataTable = objTypesTreeTable.Clone();
          if (visibleIDs != null)
          {
            for (int index = 0; index < fromRows.Length; ++index)
            {
              if (Array.IndexOf<int>(visibleIDs, Convert.ToInt32(fromRows[index]["F_OBJECT_TYPE"])) >= 0)
                DataSetProcessor.AddRow(dataTable, fromRows[index], false);
            }
          }
          else
            DataSetProcessor.AssignRows(dataTable, (IEnumerable<DataRow>) fromRows, true);
          dataTable.AcceptChanges();
          return dataTable.Rows.Count > 0 ? ObjectTypesCacheHelper.GetWhereSQL(dataTable, false, objTypesTreeTable.Columns.IndexOf("F_OBJECT_TYPE")) : "F_OBJECT_TYPE = -1";
        }
        if (parentTypeID != -1 || objTypesTreeTable.Rows.Count <= 0)
          return string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(ObjectTypesCacheHelper.GetWhereSQL(objTypesTreeTable, true, objTypesTreeTable.Columns.IndexOf("F_OBJECT_TYPE")));
        if (visibleIDs != null)
        {
          stringBuilder.Append(" AND ");
          stringBuilder.Append("(F_OBJECT_TYPE IN (");
          for (int index = 0; index < visibleIDs.Length; ++index)
            stringBuilder.Append(visibleIDs[index].ToString() + ",");
          --stringBuilder.Length;
          stringBuilder.Append("))");
        }
        return stringBuilder.ToString();
      }

      private static string GetWhereSQL(DataTable table, bool notIN, int indexRow)
      {
        StringBuilder stringBuilder = new StringBuilder(string.Empty);
        string str = notIN ? "NOT" : "";
        stringBuilder.Append($"(F_OBJECT_TYPE {str} IN (");
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
          stringBuilder.Append(row[indexRow].ToString() + ",");
        --stringBuilder.Length;
        stringBuilder.Append("))");
        return stringBuilder.ToString();
      }

      public static DataTable AddInfoToTable(DataSet cacheDataSet, DataTable table, object[] addInfo)
      {
        if (addInfo != null && addInfo.Length != 0 && addInfo[0] is bool && (bool) addInfo[0])
        {
          for (int index = 0; index < 5; ++index)
            table.Columns.Add("T" + index.ToString(), typeof (string));
          foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
          {
            row["T1"] = (object) ObjectVersionModesHelper.GetCaption((ObjectVersionModes) Convert.ToInt32(row["F_VERSIONABLE"]));
            row["T2"] = (object) MetaDataHelper.GetRelationTypeName(Convert.ToInt32(row["F_DEFAULT_RELATION"]));
            row["F_AREA_ID"] = (object) SubjectAreasHelper.GetAreasCaption(cacheDataSet.Tables["IMS_SUBJECT_AREAS"], row["F_AREA_ID"].ToString());
            row["T3"] = (object) MetaDataHelper.GetAttributeTypeName(Convert.ToInt32(row["F_CAPTION_ATTRIBUTE"]));
            row["T4"] = (object) Consts.ConvertBoolToString(row["F_ANY_ATTRIBUTES"]);
            row["T5"] = (object) InheritModesHelper.GetCaption((InheritModes) Convert.ToInt32(row["F_PUBLIC_LC"]));
          }
          table.AcceptChanges();
          table.Columns.Remove("F_VERSIONABLE");
          table.Columns.Remove("F_DEFAULT_RELATION");
          table.Columns.Remove("F_CAPTION_ATTRIBUTE");
          table.Columns.Remove("F_ANY_ATTRIBUTES");
          table.Columns.Remove("F_PUBLIC_LC");
          table.Columns["T1"].ColumnName = "F_VERSIONABLE";
          table.Columns["T2"].ColumnName = "F_DEFAULT_RELATION";
          table.Columns["T3"].ColumnName = "F_CAPTION_ATTRIBUTE";
          table.Columns["T4"].ColumnName = "F_ANY_ATTRIBUTES";
          table.Columns["T5"].ColumnName = "F_PUBLIC_LC";
        }
        return table;
      }

      /// <summary>
      /// Получает коллекцию идентификаторов всех дочерних типов объектов
      /// </summary>
      /// <param name="session">сессия</param>
      /// <param name="parentType">родительский тип</param>
      /// <returns></returns>
      public static List<int> GetChildTypes(IUserSession session, int parentType)
      {
        List<int> childTypes1 = new List<int>();
        foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectTypeCollection(parentType).Select(string.Empty).Rows)
        {
          int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
          childTypes1.Add(int32);
          List<int> childTypes2 = ObjectTypesCacheHelper.GetChildTypes(session, int32);
          if (childTypes2.Count > 0)
            childTypes1.AddRange((IEnumerable<int>) childTypes2);
        }
        return childTypes1;
      }

      /// <summary>
      /// Получает коллекцию глобальных идентификаторов всех дочерних типов объектов
      /// </summary>
      /// <param name="session">сессия</param>
      /// <param name="parentType">родительский тип</param>
      /// <returns></returns>
      public static List<Guid> GetChildTypeGuids(IUserSession session, int parentType)
      {
        List<Guid> childTypeGuids1 = new List<Guid>();
        foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectTypeCollection(parentType).Select(string.Empty).Rows)
        {
          string str = Convert.ToString(row["F_GUID"]);
          if (GuidHelper.IsGuid(str))
            childTypeGuids1.Add(new Guid(str));
          List<Guid> childTypeGuids2 = ObjectTypesCacheHelper.GetChildTypeGuids(session, Convert.ToInt32(row["F_OBJECT_TYPE"]));
          if (childTypeGuids2.Count > 0)
            childTypeGuids1.AddRange((IEnumerable<Guid>) childTypeGuids2);
        }
        return childTypeGuids1;
      }

      /// <summary>Получает корневой тип документа для childType</summary>
      /// <param name="session">сессия</param>
      /// <param name="childType">тип, для которого получить корневой тип документа</param>
      /// <returns></returns>
      public static int GetRootType(int childType)
      {
        int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(childType);
        return objectTypeParentId != -1 ? ObjectTypesCacheHelper.GetRootType(objectTypeParentId) : childType;
      }
    }
}
