// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SqlHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;


namespace Intermech.Kernel;

public class SqlHelper
{
  private static readonly string CharIDs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$&()+-/[\\]:;<=>";
  public static readonly string[] VarcharFields = new string[8]
  {
    "F_STRING_VALUE",
    "F_NOTE",
    "F_OBJ_NAME",
    "F_OBJECT_NAME",
    "F_OBJ_TYPE_NAME",
    "F_AREA_ID",
    "CAPTION",
    "F_COMPUTER_NAME"
  };
  internal static string viewForObjectTypePrefix = "IMV_O";
  internal static string viewForRelationTypePrefix = "IMV_R";

  public static int GetCharID(char ch1) => SqlHelper.CharIDs.LastIndexOf(ch1);

  public static List<Tuple<long, int>> GetObjectTypes(ICollection<long> objectIDs, IDbManager db)
  {
    int count = objectIDs.Count;
    int num1 = count >= 900 ? 900 : count;
    int num2 = count - num1;
    List<Tuple<long, int>> objectTypes = new List<Tuple<long, int>>(objectIDs.Count);
    StringBuilder stringBuilder = new StringBuilder(num1 * 10);
    foreach (long objectId in (IEnumerable<long>) objectIDs)
    {
      stringBuilder.Append(objectId);
      stringBuilder.Append(',');
      --num1;
      if (num1 == 0)
      {
        --stringBuilder.Length;
        DataTable dataTable = db.ExecuteDataTable($"SELECT F_OBJECT_ID, F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_LEVEL_ID <> :delLevel AND F_OBJECT_ID IN ({stringBuilder.ToString()})", db.Parameter("delLevel", (object) (ServerServices.GetService(typeof (IIDHelper)) as IIDHelper).DeletedID));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          Tuple<long, int> tuple = Tuple.Create<long, int>(Convert.ToInt64(dataTable.Rows[index][0]), Convert.ToInt32(dataTable.Rows[index][1]));
          objectTypes.Add(tuple);
        }
        stringBuilder.Clear();
        num1 = num2 >= 900 ? 900 : num2;
        num2 -= num1;
      }
    }
    return objectTypes;
  }

  public static DataTable GetObjectInfoByGUIDs(ICollection<Guid> objectGUIDs, IDbManager db)
  {
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>(objectGUIDs.Count);
    int num = 0;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Guid objectGuiD in (IEnumerable<Guid>) objectGUIDs)
    {
      string parameterName = "par" + num++.ToString();
      dbDataParameterList.Add(db.Parameter(parameterName, (object) objectGuiD));
      stringBuilder.AppendFormat(":{0},", (object) parameterName);
    }
    --stringBuilder.Length;
    return db.ExecuteDataTable($"SELECT G.F_OBJECT_ID, G.F_GUID, G.CAPTION, O.F_ID, O.F_OBJECT_TYPE, O.F_LC_STEP, O.F_BASE_VERSION, O.F_CHKOUT_BY FROM IMS_GUID G, IMS_OBJECTS O WHERE O.F_OBJECT_ID = G.F_OBJECT_ID AND G.F_GUID IN ({stringBuilder.ToString()})", dbDataParameterList.ToArray());
  }

  public static List<Tuple<long, string>> GetObjectGUIDs(ICollection<long> objectIDs, IDbManager db)
  {
    int num1 = db.DataProvider.MaximumINOperands - 1;
    int count = objectIDs.Count;
    int num2 = count >= num1 ? num1 : count;
    int num3 = count - num2;
    List<Tuple<long, string>> objectGuiDs = new List<Tuple<long, string>>(objectIDs.Count);
    StringBuilder stringBuilder = new StringBuilder(num2 * 10);
    foreach (long objectId in (IEnumerable<long>) objectIDs)
    {
      stringBuilder.Append(objectId);
      stringBuilder.Append(',');
      --num2;
      if (num2 == 0)
      {
        --stringBuilder.Length;
        DataTable dataTable = db.ExecuteDataTable($"SELECT G.F_OBJECT_ID, G.F_GUID FROM IMS_GUID G, IMS_OBJECTS O WHERE G.F_OBJECT_ID IN ({stringBuilder.ToString()}) AND O.F_OBJECT_ID = G.F_OBJECT_ID AND O.F_LEVEL_ID <> :delLevel", db.Parameter("delLevel", (object) (ServerServices.GetService(typeof (IIDHelper)) as IIDHelper).DeletedID));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          Tuple<long, string> tuple = Tuple.Create<long, string>(Convert.ToInt64(dataTable.Rows[index][0]), Convert.ToString(dataTable.Rows[index][1]));
          objectGuiDs.Add(tuple);
        }
        stringBuilder.Clear();
        num2 = num3 >= num1 ? num1 : num3;
        num3 -= num2;
      }
    }
    return objectGuiDs;
  }

  public static DataTable PrepareDateTimeColumns(DataTable tbl)
  {
    for (int index = 0; index < tbl.Columns.Count; ++index)
    {
      if (tbl.Columns[index].DataType == typeof (DateTime))
        tbl.Columns[index].DateTimeMode = DataSetDateTime.Unspecified;
    }
    return tbl;
  }

  internal static string GetEntersInSQL(string IDs, string objectTypeIDstr, IDbManager db)
  {
    return $"SELECT A.F_OBJECT_ID, A.F_ID FROM IMS_RELATIONS R, IMS_OBJECTS A WHERE (R.F_PART_ID IN ({IDs})) AND A.F_OBJECT_ID = R.F_PROJ_ID AND A.F_OBJECT_TYPE IN ({objectTypeIDstr})";
  }

  public static string GetEntersInSQL(
    long partID,
    string fieldsList,
    string wherePart,
    IDbManager db)
  {
    return $"SELECT {fieldsList} FROM IMS_RELATIONS, IMS_OBJECTS WHERE F_PART_ID = :partID AND IMS_OBJECTS.F_OBJECT_ID = IMS_RELATIONS.F_PROJ_ID AND (IMS_RELATIONS.F_CREATE_DATE <= {db.DataProvider.Now}) AND {wherePart}";
  }

  public static long GetIDByObjectID(long objectID, IDbManager db)
  {
    object obj = db.ExecuteScalar("SELECT F_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :oid", db.Parameter("oid", (object) objectID));
    return obj != null && obj != DBNull.Value ? Convert.ToInt64(obj) : throw new ObjectNotFoundException(objectID);
  }

  public static long GetIDByGuid(Guid guid, IDbManager db, bool throwException)
  {
    object obj = db.ExecuteScalar("SELECT F_ID FROM IMS_GUID_RESOLVE WHERE F_GUID = :v_guid AND F_CATEGORY_TYPE = :typ", db.Parameter("v_guid", (object) guid), db.Parameter("typ", (object) 2));
    if (obj != null && obj != DBNull.Value)
      return Convert.ToInt64(obj);
    if (throwException)
      throw new Exception($"Объект GUID={guid} не найден.");
    return 0;
  }

  public static long GetObjectIDByID(long ID, IDbManager db)
  {
    object obj = db.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :oid", db.Parameter("oid", (object) ID));
    return obj != null && obj != DBNull.Value ? Convert.ToInt64(obj) : throw new ObjectNotFoundException(ID);
  }

  internal static DataTable GetObjectsList(
    long id,
    string objectTypeIDs,
    bool needOrder,
    IDbManager db)
  {
    string str = !needOrder ? string.Empty : "ORDER BY A.CAPTION";
    return db.ExecuteDataTable($"SELECT A.F_OBJECT_ID, A.CAPTION, A.F_GUID, A.F_ID FROM IMS_RELATIONS R, IMS_OBJECTS_VIEW A WHERE (R.F_PART_ID = :partID) AND A.F_OBJECT_ID = R.F_PROJ_ID AND A.F_OBJECT_TYPE IN ({objectTypeIDs}) AND A.F_LEVEL_ID <> :levID {str}", db.Parameter("partID", (object) id), db.Parameter("levID", (object) (ServerServices.GetService(typeof (IIDHelper)) as IIDHelper).DeletedID));
  }

  internal static string GetConsistFromSQL(string objIDs, string objectTypeIDstr, IDbManager db)
  {
    return $"SELECT A.F_OBJECT_ID FROM IMS_RELATIONS R, IMS_OBJECTS A WHERE (R.F_PROJ_ID IN ({objIDs})) AND A.F_ID = R.F_PART_ID AND A.F_OBJECT_TYPE IN ({objectTypeIDstr})";
  }

  internal static string MakeCASTString(
    string sourceTableName,
    string sourceFieldName,
    IDBAttributeType destAttribute,
    IDbDataProvider dataProvider)
  {
    if (!(destAttribute.TextFieldName == "F_STRING_VALUE"))
      return sourceFieldName;
    int len;
    switch (destAttribute.AttributeType)
    {
      case FieldTypes.ftString:
        len = Convert.ToInt32(destAttribute.SizeType);
        break;
      case FieldTypes.ftMeasured:
        len = 80 /*0x50*/;
        break;
      case FieldTypes.ftGuid:
        len = 40;
        break;
      default:
        len = Consts.MaxStringSize;
        break;
    }
    return dataProvider.NVARCHARCast(sourceFieldName, len, sourceTableName);
  }

  public static string QString(string aValue) => DataSetProcessor.QString(aValue);

  public static string ToSqlDouble(object val)
  {
    return val.ToString().Replace(NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator, ".");
  }

  public static DataRow AssignRow(DataTable toTable, DataRow fromRow, int pos)
  {
    return DataSetProcessor.AssignRow(toTable, fromRow, pos, false);
  }

  public static DataRow AssignRow(DataTable toTable, DataRow fromRow)
  {
    return SqlHelper.AssignRow(toTable, fromRow, toTable.Rows.Count + 1);
  }

  public static void AssignRows(DataTable toTable, IEnumerable<DataRow> fromRows)
  {
    DataSetProcessor.AssignRows(toTable, fromRows);
  }

  public static char NextLetter(DataRowCollection rows)
  {
    int num1 = 0;
    if (rows == null || rows.Count == 0)
      return SqlHelper.CharIDs[0];
    foreach (DataRow row in (InternalDataCollectionBase) rows)
    {
      int num2 = SqlHelper.CharIDs.IndexOf(Convert.ToChar(row[0]));
      if (num2 > num1)
        num1 = num2;
    }
    if (num1 < SqlHelper.CharIDs.Length - 1)
      return SqlHelper.CharIDs[num1 + 1];
    for (int index = 0; index < SqlHelper.CharIDs.Length; ++index)
    {
      bool flag = false;
      foreach (DataRow row in (InternalDataCollectionBase) rows)
      {
        if ((int) SqlHelper.CharIDs[index] == (int) Convert.ToChar(row[0]))
          flag = true;
      }
      if (!flag)
        return SqlHelper.CharIDs[index];
    }
    throw new KernelException(sc_13066.ssp_appserver_13067());
  }

  public static bool IsVarcharField(string fldName) => DataSetProcessor.IsVarcharField(fldName);

  public static void ValidateEmptyValue(string val, string paramName)
  {
    if (val.Trim() == "")
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13066.ssp_appserver_13068()), (object) paramName));
  }

  public static void ValidateFieldLength(string fldName, int len, int max_len)
  {
    if (len > max_len)
      throw new KernelExceptionID(sc_13066.ssp_appserver_13069(1873062423), (object) fldName, (object) max_len);
  }

  public static bool IsEqual(byte[] a1, byte[] a2)
  {
    int num1 = 0;
    if (a1 != null)
      num1 = a1.Length;
    int num2 = 0;
    if (a2 != null)
      num2 = a2.Length;
    if (num1 != num2)
      return false;
    for (int index = 0; index < num1; ++index)
    {
      if ((int) a1[index] != (int) a2[index])
        return false;
    }
    return true;
  }

  public static bool HasCustomAttributes(
    DBRecordSetParams dbrsp,
    AttributeSourceTypes defSourceType,
    out Dictionary<AttributeSourceTypes, bool> type2CustomAttr)
  {
    int int32 = Convert.ToInt32((object) AttributeSourceTypes.Other);
    type2CustomAttr = new Dictionary<AttributeSourceTypes, bool>(int32);
    int length1 = dbrsp.Columns != null ? dbrsp.Columns.Length : 0;
    int length2 = dbrsp.Conditions != null ? dbrsp.Conditions.Length : 0;
    int capacity = length1 + length2;
    if (capacity == 0)
      return false;
    List<object> attrList = new List<object>(capacity);
    List<AttributeSourceTypes> attrSourceTypeList = new List<AttributeSourceTypes>(capacity);
    if (length1 > 0)
    {
      attrList.AddRange((IEnumerable<object>) dbrsp.Columns);
      if (dbrsp.ColumnsInfo != null)
      {
        foreach (Intermech.Kernel.Search.ColumnInfo columnInfo in dbrsp.ColumnsInfo)
          attrSourceTypeList.Add(columnInfo.AttributeSource);
      }
      else
      {
        for (int index = 0; index < length1; ++index)
          attrSourceTypeList.Add(AttributeSourceTypes.Auto);
      }
    }
    Action<ConditionStructure[]> conditionAttrCollect = (Action<ConditionStructure[]>) null;
    conditionAttrCollect = (Action<ConditionStructure[]>) (conditions =>
    {
      if (conditions == null)
        return;
      int length3 = conditions.Length;
      for (int index = 0; index < length3; ++index)
      {
        attrList.Add(conditions[index].Attribute);
        attrSourceTypeList.Add(conditions[index].AttributeSource);
        conditionAttrCollect(conditions[index].NestedConditions);
      }
    });
    conditionAttrCollect(dbrsp.Conditions);
    for (int index = 0; index < attrList.Count; ++index)
    {
      object attributeID = attrList[index];
      if (attributeID != null)
      {
        AttributeSourceTypes key = attrSourceTypeList[index] == AttributeSourceTypes.Auto ? defSourceType : attrSourceTypeList[index];
        if (!(attributeID is int num))
          num = MetaDataHelper.GetAttributeID(attributeID);
        if (num != -10000 && num != 0)
        {
          if (num > 0)
          {
            type2CustomAttr[key] = true;
          }
          else
          {
            bool flag = false;
            if (key == AttributeSourceTypes.Object)
              flag = num == -50 || num == -12;
            if (flag)
              type2CustomAttr[key] = true;
          }
        }
      }
    }
    return type2CustomAttr.Count > 0;
  }
}
