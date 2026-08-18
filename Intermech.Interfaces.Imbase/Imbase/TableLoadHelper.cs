// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableLoadHelper
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Expressions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Receptures;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

#nullable disable
namespace Intermech.Imbase;

public static class TableLoadHelper
{
  private static ITablesCache _cache;
  private static int _longBlobTableDataAttId;
  private static DataTable catalogsTable;

  /// <summary>
  /// 
  /// </summary>
  public static GetAttributeValuesModes ImbaseAttValuesModes
  {
    get
    {
      return GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeAlias | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.IncludeCaption;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public static ITablesCache TablesCache
  {
    set => TableLoadHelper._cache = value;
  }

  /// <summary>
  /// Идентификатор атрибута длинного блоба для сохранения данных больших таблиц
  /// </summary>
  public static int LongBlobTableDataAttId
  {
    get
    {
      if (TableLoadHelper._longBlobTableDataAttId == 0)
        TableLoadHelper._longBlobTableDataAttId = MetaDataHelper.GetAttributeTypeID("cad001b2-306c-11d8-b4e9-00304f19f545");
      return TableLoadHelper._longBlobTableDataAttId;
    }
  }

  /// <summary>
  /// Идентификатор атрибута короткого блоба для сохранения данных небольших таблиц
  /// </summary>
  public static int ShortBlobTableDataAttId => Consts.ImbaseTableDataAttID;

  /// <summary>
  /// Получить список типов атрибутов, которые невозможно добавить таблице IMBASE.
  /// </summary>
  public static FieldTypes[] ForbiddenAttrTypesForAddToTable
  {
    get
    {
      return new FieldTypes[7]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftFile,
        FieldTypes.ftShortBlob,
        FieldTypes.ftSystem,
        FieldTypes.ftExternalLink,
        FieldTypes.ftPassword,
        FieldTypes.ftAutoInc
      };
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="linkId"></param>
  /// <param name="tableId"></param>
  /// <param name="recordsTable"></param>
  /// <param name="attTable"></param>
  /// <param name="columnsAttributes"></param>
  /// <param name="cc"></param>
  /// <param name="keyInfo"></param>
  public static void AssignAttributes(
    IUserSession session,
    long linkId,
    long tableId,
    DataTable recordsTable,
    DataTable attTable,
    out AttributeTypeProperties[] columnsAttributes,
    List<CalculatedColumn> cc,
    ref ImbaseKeyInfo keyInfo)
  {
    List<int> computed = new List<int>();
    IDBAttributable dbAttributable1 = (IDBAttributable) null;
    IDBAttributable dbAttributable2 = (IDBAttributable) null;
    int objectTypeId = MetaDataHelper.GetObjectTypeID(Consts.ImbaseTableRecordTypeGUID);
    DataRowCollection rows = attTable.Rows;
    DataRow[] dataRowArray = attTable.Select(string.Empty, string.Empty, DataViewRowState.Deleted);
    columnsAttributes = new AttributeTypeProperties[rows.Count - dataRowArray.Length];
    CalcContext calcContext = (CalcContext) null;
    if (recordsTable.ExtendedProperties.ContainsKey((object) "CalcContext"))
      calcContext = recordsTable.ExtendedProperties[(object) "CalcContext"] as CalcContext;
    List<int> intList = new List<int>();
    try
    {
      recordsTable.BeginLoadData();
      int index1 = 0;
      for (int index2 = 0; index2 < rows.Count; ++index2)
      {
        DataRow dataRow = rows[index2];
        if (dataRow.RowState != DataRowState.Deleted)
        {
          bool flag1 = false;
          IDBAttribute dbAttribute = (IDBAttribute) null;
          string str1 = Convert.ToString(dataRow["F_ATTRIBUTE_GUID"]);
          IDBAttributeType attributeType1 = session.GetAttributeType(new Guid(str1), false);
          if (attributeType1 == null)
          {
            DataColumn column = recordsTable.Columns[str1];
            if (column != null)
              column.Caption = str1.ToString();
          }
          else
          {
            columnsAttributes[index1] = attributeType1.PropertiesStructure;
            columnsAttributes[index1].Computed = (ComputeValueModes) Convert.ToInt32(dataRow["F_COMPUTED"]);
            columnsAttributes[index1].DefaultValue = dataRow["F_DEFAULT_VALUE"];
            columnsAttributes[index1].Options = (AttributeOptions) Convert.ToInt64(dataRow["F_OPTIONS"]);
            columnsAttributes[index1].Unique = (UniqueValueModes) Convert.ToInt32(dataRow["F_UNIQUE"]);
            columnsAttributes[index1].Formula = Convert.ToString(dataRow["F_FORMULA"]);
            bool flag2 = (columnsAttributes[index1].Options & AttributeOptions.Imbase_DontUseDefaultsWithNull) == AttributeOptions.None;
            RequiredModes int32 = (RequiredModes) Convert.ToInt32(dataRow["F_REQUIRED"]);
            IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objectTypeId, attributeType1.AttributeID);
            bool flag3 = (attributeType1.Options & AttributeOptions.DontCopyPrototypeValue) != 0;
            if (!flag3 && attribute4ObjectType != null)
              flag3 = (attribute4ObjectType.Options & AttributeOptions.DontCopyPrototypeValue) != 0;
            DataColumn col = recordsTable.Columns[str1];
            if (col == null)
            {
              col = recordsTable.Columns.Add(attributeType1.PropertiesStructure.AttributeGuid.ToString(), ImbaseHelper.AttTypeToType(attributeType1.AttributeType));
              flag1 = true;
              col.ExtendedProperties[(object) "F_VIRTUAL"] = (object) true;
            }
            else if (calcContext != null && ((columnsAttributes[index1].Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef || (attributeType1.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef))
              calcContext.AddRefColumn(col.Ordinal);
            if (flag3 && !col.ExtendedProperties.ContainsKey((object) "F_DONTCOPY"))
              col.ExtendedProperties.Add((object) "F_DONTCOPY", (object) true);
            col.Caption = attributeType1.AttributeID.ToString();
            string g = Convert.ToString(dataRow["F_UNITS"]);
            if (g.Length > 0)
            {
              QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(g));
              if (!objectInfo.Empty)
              {
                long measureID = Math.Abs(objectInfo.ObjectID);
                col.ExtendedProperties[(object) "F_MEASURE"] = (object) measureID;
                MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measureID);
                if (descriptor != null)
                  col.ExtendedProperties[(object) "F_MEASURE_U"] = (object) descriptor.ShortName;
                int num = col.DataType != typeof (double) ? 1 : 0;
              }
            }
            if (columnsAttributes[index1].Computed == ComputeValueModes.JITValue)
            {
              computed.Add(index1);
              col.ExtendedProperties[(object) "F_VIRTUAL"] = (object) true;
            }
            else if (int32 == RequiredModes.Manual || !flag1)
            {
              object obj = (object) null;
              int attributeId = attributeType1.AttributeID;
              long num1 = -1;
              if (linkId != -1L && dbAttributable2 == null)
                dbAttributable2 = (IDBAttributable) session.GetObject(linkId);
              if (dbAttributable2 != null)
              {
                dbAttribute = dbAttributable2.Attributes.FindByID(attributeId);
                num1 = linkId;
              }
              if (dbAttribute == null || dbAttribute.Value == null)
              {
                if (dbAttributable1 == null)
                  dbAttributable1 = (IDBAttributable) session.GetObject(tableId);
                dbAttribute = dbAttributable1.Attributes.FindByID(attributeId);
                num1 = tableId;
              }
              if (dbAttribute != null)
                obj = dbAttribute.Value;
              if (flag2 && TableLoadHelper.IsNull(obj))
              {
                num1 = -1L;
                obj = columnsAttributes[index1].DefaultValue;
                string mValue = Convert.ToString(obj);
                if (columnsAttributes[index1].FieldType == FieldTypes.ftMeasured && !string.IsNullOrEmpty(mValue))
                  obj = (object) MeasureHelper.ConvertToMeasuredValue(mValue);
              }
              if (flag1)
              {
                if (!TableLoadHelper.IsNull(obj))
                {
                  if (obj is MeasuredValue)
                  {
                    MeasuredValue mValue = obj as MeasuredValue;
                    long num2 = Consts.mmUnitID;
                    if (col.ExtendedProperties.ContainsKey((object) "F_MEASURE"))
                      num2 = Convert.ToInt64(col.ExtendedProperties[(object) "F_MEASURE"]);
                    if (MeasureHelper.GetBaseMeasureID_ByMeasureID(mValue.MeasureID) == MeasureHelper.GetBaseMeasureID_ByMeasureID(num2))
                      obj = (object) MeasureHelper.ConvertToMeasuredValue(mValue, num2);
                    obj = (object) (obj as MeasuredValue).Value;
                  }
                  if (attributeType1.MultipleValued == MultiValueModes.SingleValueFromList)
                  {
                    IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(attributeType1.AttributeID);
                    if (attributeType2.PossibleValues != null)
                    {
                      int index3 = attributeType2.PossibleValues.IndexOf(obj);
                      if (index3 != -1)
                      {
                        string str2 = Convert.ToString(attributeType2.PossibleValuesDescriptions[index3]);
                        if (!string.IsNullOrEmpty(str2))
                          col.ExtendedProperties[(object) "F_DISPLAY"] = (object) str2;
                      }
                    }
                  }
                  if (attributeType1.AttributeType == FieldTypes.ftObjectLink && !TableLoadHelper.IsNull(obj) && !GuidHelper.IsGuid(Convert.ToString(obj)))
                  {
                    QuickObjectInfo objectInfo = session.GetObjectInfo(Convert.ToInt64(obj));
                    obj = objectInfo.Empty ? (object) DBNull.Value : (object) objectInfo.VersionGuid;
                  }
                  col.Expression = TableLoadHelper.QuoteString(obj.ToString());
                  if (num1 != -1L)
                    col.ExtendedProperties[(object) "F_OBJECTID"] = (object) num1;
                }
              }
              else
              {
                if (attributeType1.AttributeType == FieldTypes.ftObjectLink && !TableLoadHelper.IsNull(obj) && !GuidHelper.IsGuid(Convert.ToString(obj)))
                {
                  QuickObjectInfo objectInfo = session.GetObjectInfo(Convert.ToInt64(obj));
                  obj = objectInfo.Empty ? (object) DBNull.Value : (object) objectInfo.VersionGuid;
                }
                TableLoadHelper.FillColumn(col, obj, true);
              }
            }
            ++index1;
          }
        }
      }
      if (computed.Count > 0)
        TableLoadHelper.CalcComputedColumns(session, recordsTable, columnsAttributes, computed, cc);
    }
    finally
    {
      recordsTable.EndLoadData();
    }
    DataColumn column1 = recordsTable.Columns["F_KEY"];
    int int32_1;
    if (column1 != null)
    {
      DataColumn dataColumn = column1;
      int32_1 = Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID);
      string str = int32_1.ToString();
      dataColumn.Caption = str;
    }
    DataColumn column2 = recordsTable.Columns["F_GUID"];
    if (column2 != null)
    {
      DataColumn dataColumn = column2;
      int32_1 = Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID);
      string str = int32_1.ToString();
      dataColumn.Caption = str;
    }
    if (keyInfo.CatalogId == 0L)
      return;
    if (dbAttributable1 == null)
      dbAttributable1 = (IDBAttributable) session.GetObject(tableId);
    IDBAttribute attributeById1 = dbAttributable1.GetAttributeByID(Consts.ImbaseInternalTableNameAttID);
    if (attributeById1 != null)
      keyInfo.TableName = Convert.ToString(attributeById1.Value);
    if (dbAttributable2 == null && linkId != -1L)
      dbAttributable2 = (IDBAttributable) session.GetObject(linkId);
    if (dbAttributable2 == null)
      return;
    IDBAttribute attributeById2 = dbAttributable2.GetAttributeByID(Consts.ClassifFolderKeyAttId);
    if (attributeById2 == null)
      return;
    string str3 = Convert.ToString(attributeById2.Value);
    if (str3.Length <= 2)
      return;
    string classifCode = str3.Substring(0, 2);
    TableLoadHelper.GetCatalogData(session, classifCode, out keyInfo.CatalogId, out keyInfo.CatalogName);
  }

  /// <summary>
  /// Использовать вместо оригинального метода AssignAttributes.
  /// </summary>
  /// <param name="session"></param>
  /// <param name="linkID"></param>
  /// <param name="tableID"></param>
  /// <param name="dataTable"></param>
  /// <param name="attributeTable"></param>
  /// <param name="columnsAttributes"></param>
  /// <param name="cc"></param>
  /// <param name="keyInfo"></param>
  /// <remarks>В оригинальном методе со временем скопилось много мусора, но чтобы все не поломать, было решено постепенно заменять старый метод новым</remarks>
  public static void AssignAttributes2(
    IUserSession session,
    long linkID,
    long tableID,
    DataTable dataTable,
    DataTable attributeTable,
    out AttributeTypeProperties[] columnsAttributes,
    List<CalculatedColumn> cc,
    ref ImbaseKeyInfo keyInfo)
  {
    List<DataRow> list = attributeTable.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => x.RowState != DataRowState.Deleted && MetaDataHelper.GetAttributeTypeID(Convert.ToString(x["F_ATTRIBUTE_GUID"])) != -10000)).ToList<DataRow>();
    columnsAttributes = new AttributeTypeProperties[list.Count];
    IDBAttributable dbAttributable1 = (IDBAttributable) session.GetObject(tableID);
    IDBAttributable dbAttributable2 = linkID != 0L ? (IDBAttributable) session.GetObject(linkID) : (IDBAttributable) null;
    dataTable.BeginLoadData();
    try
    {
      List<int> computed = new List<int>();
      int index1 = 0;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      dataTable.Columns["F_KEY"].Caption = Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID).ToString();
      DataColumn column1 = dataTable.Columns["F_GUID"];
      int num1 = Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID);
      string str1 = num1.ToString();
      column1.Caption = str1;
      foreach (DataRow row in list)
      {
        string str2 = Convert.ToString(row["F_ATTRIBUTE_GUID"]);
        IDBAttributeType attributeType1 = session.GetAttributeType(new Guid(str2), false);
        AttributeTypeProperties attributeTypeProperties = TableLoadHelper.FillAttributeTypeProperties(attributeType1, row);
        bool flag = (attributeTypeProperties.Options & AttributeOptions.Imbase_DontUseDefaultsWithNull) == AttributeOptions.None;
        columnsAttributes[index1] = attributeTypeProperties;
        bool isNew = false;
        DataColumn column2 = TableLoadHelper.GetColumn(dataTable, str2, attributeType1.AttributeType, out isNew);
        DataColumn dataColumn = column2;
        num1 = attributeType1.AttributeID;
        string str3 = num1.ToString();
        dataColumn.Caption = str3;
        PropertyCollection extendedProperties = column2.ExtendedProperties;
        if (isNew || attributeTypeProperties.Computed == ComputeValueModes.JITValue)
          extendedProperties[(object) "F_VIRTUAL"] = (object) true;
        if ((attributeType1.Options & AttributeOptions.DontCopyPrototypeValue) != AttributeOptions.None)
          extendedProperties[(object) "F_DONTCOPY"] = (object) true;
        MeasureDescriptor measureDescriptor = TableLoadHelper.GetMeasureDescriptor(session, row);
        if (measureDescriptor != null)
        {
          extendedProperties[(object) "F_MEASURE"] = (object) measureDescriptor.MeasureID;
          extendedProperties[(object) "F_MEASURE_U"] = (object) measureDescriptor.ShortName;
        }
        if (attributeTypeProperties.Computed == ComputeValueModes.JITValue)
          computed.Add(index1);
        else if (Convert.ToInt32(row["F_REQUIRED"]) == 0 || !isNew)
        {
          long num2 = 0;
          object obj = (object) null;
          if (dbAttributable2 != null)
          {
            obj = dbAttributable2.GetAttributeByID(attributeType1.AttributeID)?.Value;
            num2 = linkID;
          }
          if (TableLoadHelper.IsNull(obj))
          {
            obj = dbAttributable1.GetAttributeByID(attributeType1.AttributeID)?.Value;
            num2 = tableID;
          }
          if (flag && TableLoadHelper.IsNull(obj))
          {
            num2 = 0L;
            string mValue = Convert.ToString(attributeTypeProperties.DefaultValue);
            obj = attributeTypeProperties.FieldType != FieldTypes.ftMeasured || string.IsNullOrEmpty(mValue) ? attributeTypeProperties.DefaultValue : (object) MeasureHelper.ConvertToMeasuredValue(mValue);
          }
          if (isNew)
          {
            if (!TableLoadHelper.IsNull(obj))
            {
              if (obj is MeasuredValue mValue)
              {
                long num3 = extendedProperties.ContainsKey((object) "F_MEASURE") ? Convert.ToInt64(extendedProperties[(object) "F_MEASURE"]) : Consts.mmUnitID;
                obj = MeasureHelper.GetBaseMeasureID_ByMeasureID(mValue.MeasureID) != MeasureHelper.GetBaseMeasureID_ByMeasureID(num3) ? (object) mValue.Value : (object) MeasureHelper.ConvertToMeasuredValue(mValue, num3).Value;
              }
              else if (attributeType1.AttributeType == FieldTypes.ftObjectLink)
              {
                QuickObjectInfo objectInfo = session.GetObjectInfo(Convert.ToInt64(obj));
                obj = objectInfo.Empty ? (object) DBNull.Value : (object) objectInfo.VersionGuid;
              }
              if (attributeType1.MultipleValued == MultiValueModes.SingleValueFromList)
              {
                IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(attributeType1.AttributeID);
                List<object> possibleValues = attributeType2.PossibleValues;
                if (possibleValues != null)
                {
                  int index2 = possibleValues.IndexOf(obj);
                  if (index2 != -1)
                  {
                    string str4 = Convert.ToString(attributeType2.PossibleValuesDescriptions[index2]);
                    if (!string.IsNullOrEmpty(str4))
                      extendedProperties.Add((object) "F_DISPLAY", (object) str4);
                  }
                }
              }
              column2.Expression = TableLoadHelper.QuoteString(Convert.ToString(obj));
              if (num2 != 0L)
                extendedProperties.Add((object) "F_OBJECTID", (object) num2);
            }
          }
          else
            TableLoadHelper.FillColumn(column2, obj, true);
        }
        ++index1;
      }
      if (computed.Count > 0)
        TableLoadHelper.CalcComputedColumns(session, dataTable, columnsAttributes, computed, cc);
    }
    finally
    {
      dataTable.EndLoadData();
    }
    IDBAttribute attributeById = dbAttributable1.GetAttributeByID(Consts.ImbaseInternalTableNameAttID);
    keyInfo.TableName = attributeById != null ? Convert.ToString(attributeById.Value) : keyInfo.TableName;
    if (dbAttributable2 == null)
      return;
    long catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID(session, linkID);
    IDBObject dbObject = session.GetObject(catalogIdByObjectId);
    keyInfo.CatalogId = catalogIdByObjectId;
    keyInfo.CatalogName = dbObject.Caption;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrType"></param>
  /// <param name="row"></param>
  /// <returns></returns>
  private static AttributeTypeProperties FillAttributeTypeProperties(
    IDBAttributeType attrType,
    DataRow row)
  {
    return attrType.PropertiesStructure with
    {
      Computed = (ComputeValueModes) Convert.ToInt32(row["F_COMPUTED"]),
      DefaultValue = row["F_DEFAULT_VALUE"],
      Options = (AttributeOptions) Convert.ToInt64(row["F_OPTIONS"]),
      Unique = (UniqueValueModes) Convert.ToInt32(row["F_UNIQUE"]),
      Formula = Convert.ToString(row["F_FORMULA"])
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="table"></param>
  /// <param name="columnName"></param>
  /// <param name="attrType"></param>
  /// <param name="isNew"></param>
  /// <returns></returns>
  private static DataColumn GetColumn(
    DataTable table,
    string columnName,
    FieldTypes attrType,
    out bool isNew)
  {
    isNew = false;
    DataColumn column = table.Columns[columnName];
    if (column == null)
    {
      column = table.Columns.Add(columnName, ImbaseHelper.AttTypeToType(attrType));
      isNew = true;
    }
    return column;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="row"></param>
  /// <returns></returns>
  private static MeasureDescriptor GetMeasureDescriptor(IUserSession session, DataRow row)
  {
    MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
    string g = Convert.ToString(row["F_UNITS"]);
    if (!string.IsNullOrEmpty(g))
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(g));
      if (!objectInfo.Empty)
        measureDescriptor = MeasureHelper.FindDescriptor(objectInfo.ObjectID);
    }
    return measureDescriptor;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="linkId"></param>
  /// <param name="tableId"></param>
  /// <param name="recordsTable"></param>
  /// <param name="attTable"></param>
  /// <param name="columnsAttributes"></param>
  /// <param name="cc"></param>
  /// <param name="keyInfo"></param>
  /// <param name="values"></param>
  /// <param name="ignoreTableAttr"></param>
  public static void AssignAttributes(
    IUserSession session,
    long linkId,
    long tableId,
    DataTable recordsTable,
    DataTable attTable,
    out AttributeTypeProperties[] columnsAttributes,
    List<CalculatedColumn> cc,
    ref ImbaseKeyInfo keyInfo,
    Dictionary<int, object> values,
    bool ignoreTableAttr)
  {
    List<int> computed = new List<int>();
    IDBAttributable dbAttributable1 = (IDBAttributable) null;
    IDBAttributable dbAttributable2 = (IDBAttributable) null;
    string empty = string.Empty;
    values = values ?? new Dictionary<int, object>(0);
    IDBObjectType objectType = session.GetObjectType(Consts.ImbaseTableRecordTypeGUID);
    DataRowCollection rows = attTable.Rows;
    DataRow[] dataRowArray = attTable.Select(string.Empty, string.Empty, DataViewRowState.Deleted);
    columnsAttributes = new AttributeTypeProperties[rows.Count - dataRowArray.Length];
    Guid AttributeGUID = new Guid("cad00005-306c-11d8-b4e9-00304f19f545");
    try
    {
      recordsTable.BeginLoadData();
      int index1 = 0;
      for (int index2 = 0; index2 < rows.Count; ++index2)
      {
        DataRow dataRow = rows[index2];
        if (dataRow.RowState != DataRowState.Deleted)
        {
          bool flag1 = false;
          IDBAttribute dbAttribute = (IDBAttribute) null;
          string str1 = Convert.ToString(dataRow["F_ATTRIBUTE_GUID"]);
          IDBAttributeType attributeType1 = session.GetAttributeType(new Guid(str1), false);
          if (attributeType1 != null)
          {
            IDBAttributeType4 attributeById = objectType.Attributes.GetAttributeByID(attributeType1.AttributeID, false);
            RequiredModes int32 = (RequiredModes) Convert.ToInt32(dataRow["F_REQUIRED"]);
            columnsAttributes[index1] = attributeType1.PropertiesStructure;
            columnsAttributes[index1].Computed = (ComputeValueModes) Convert.ToInt32(dataRow["F_COMPUTED"]);
            columnsAttributes[index1].DefaultValue = dataRow["F_DEFAULT_VALUE"];
            columnsAttributes[index1].Options = (AttributeOptions) Convert.ToInt64(dataRow["F_OPTIONS"]);
            columnsAttributes[index1].Unique = (UniqueValueModes) Convert.ToInt32(dataRow["F_UNIQUE"]);
            columnsAttributes[index1].Formula = Convert.ToString(dataRow["F_FORMULA"]);
            bool flag2 = (columnsAttributes[index1].Options & AttributeOptions.Imbase_DontUseDefaultsWithNull) == AttributeOptions.None;
            bool flag3 = (attributeType1.Options & AttributeOptions.DontCopyPrototypeValue) != 0;
            if (!flag3 && attributeById != null)
              flag3 = (attributeById.Options & AttributeOptions.DontCopyPrototypeValue) != 0;
            DataColumn col = recordsTable.Columns[str1];
            if (col == null)
            {
              col = recordsTable.Columns.Add(attributeType1.PropertiesStructure.AttributeGuid.ToString(), ImbaseHelper.AttTypeToType(attributeType1.AttributeType));
              flag1 = true;
              col.ExtendedProperties[(object) "F_VIRTUAL"] = (object) true;
            }
            if (flag3 && !col.ExtendedProperties.ContainsKey((object) "F_DONTCOPY"))
              col.ExtendedProperties.Add((object) "F_DONTCOPY", (object) true);
            col.Caption = attributeType1.AttributeID.ToString();
            string str2 = Convert.ToString(dataRow["F_UNITS"]);
            if (GuidHelper.IsGuid(str2))
            {
              QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(str2));
              IDBObject objectActualCopy = session.GetObjectActualCopy(objectInfo.ObjectID, false);
              if (objectActualCopy != null)
              {
                col.ExtendedProperties[(object) "F_MEASURE"] = (object) objectActualCopy.ObjectID;
                IDBAttribute byGuid = objectActualCopy.Attributes.FindByGUID(AttributeGUID);
                col.ExtendedProperties[(object) "F_MEASURE_U"] = byGuid != null ? (object) byGuid.AsString : (object) string.Empty;
              }
            }
            if (columnsAttributes[index1].Computed == ComputeValueModes.JITValue)
            {
              computed.Add(index1);
              col.ExtendedProperties[(object) "F_VIRTUAL"] = (object) true;
            }
            else if (int32 == RequiredModes.Manual || !flag1)
            {
              object obj = (object) null;
              long num1 = 0;
              dbAttributable1 = dbAttributable1 ?? (IDBAttributable) session.GetObjectActualCopy(tableId, false);
              if (linkId != 0L && dbAttributable2 == null)
                dbAttributable2 = (IDBAttributable) session.GetObjectActualCopy(linkId, false);
              if (values.ContainsKey(attributeType1.AttributeID))
              {
                obj = values[attributeType1.AttributeID];
                num1 = linkId;
              }
              else if (dbAttributable2 != null)
              {
                dbAttribute = dbAttributable2.Attributes.FindByID(attributeType1.AttributeID);
                num1 = linkId;
                obj = dbAttribute?.Value;
              }
              if ((obj == null || obj == DBNull.Value) && !ignoreTableAttr)
              {
                dbAttribute = dbAttributable1.Attributes.FindByID(attributeType1.AttributeID);
                num1 = tableId;
              }
              if (dbAttribute != null)
                obj = dbAttribute.Value;
              if (flag2 && TableLoadHelper.IsNull(obj))
              {
                num1 = 0L;
                obj = columnsAttributes[index1].DefaultValue;
                string mValue = Convert.ToString(obj);
                if (columnsAttributes[index1].FieldType == FieldTypes.ftMeasured && !string.IsNullOrEmpty(mValue))
                  obj = (object) MeasureHelper.ConvertToMeasuredValue(mValue);
              }
              if (flag1)
              {
                if (!TableLoadHelper.IsNull(obj))
                {
                  if (obj is MeasuredValue)
                  {
                    MeasuredValue mValue = obj as MeasuredValue;
                    long num2 = Consts.mmUnitID;
                    if (col.ExtendedProperties.ContainsKey((object) "F_MEASURE"))
                      num2 = Convert.ToInt64(col.ExtendedProperties[(object) "F_MEASURE"]);
                    if (MeasureHelper.GetBaseMeasureID(mValue.MeasureID) == MeasureHelper.GetBaseMeasureID(num2))
                      obj = (object) MeasureHelper.ConvertToMeasuredValue(mValue, num2);
                    obj = (object) (obj as MeasuredValue).Value;
                  }
                  if (attributeType1.MultipleValued == MultiValueModes.SingleValueFromList)
                  {
                    IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(attributeType1.AttributeID);
                    if (attributeType2.PossibleValues != null)
                    {
                      int index3 = attributeType2.PossibleValues.IndexOf(obj);
                      if (index3 != -1)
                      {
                        string str3 = Convert.ToString(attributeType2.PossibleValuesDescriptions[index3]);
                        if (!string.IsNullOrEmpty(str3))
                          col.ExtendedProperties[(object) "F_DISPLAY"] = (object) str3;
                      }
                    }
                  }
                  col.Expression = TableLoadHelper.QuoteString(Convert.ToString(obj));
                  if (num1 != 0L)
                    col.ExtendedProperties[(object) "F_OBJECTID"] = (object) num1;
                }
              }
              else
                TableLoadHelper.FillColumn(col, obj, true);
            }
            ++index1;
          }
        }
      }
      if (computed.Count > 0)
        TableLoadHelper.CalcComputedColumns(session, recordsTable, columnsAttributes, computed, cc);
    }
    finally
    {
      recordsTable.EndLoadData();
    }
    DataColumn column1 = recordsTable.Columns["F_KEY"];
    int int32_1;
    if (column1 != null)
    {
      DataColumn dataColumn = column1;
      int32_1 = Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID);
      string str = int32_1.ToString();
      dataColumn.Caption = str;
    }
    DataColumn column2 = recordsTable.Columns["F_GUID"];
    if (column2 != null)
    {
      DataColumn dataColumn = column2;
      int32_1 = Convert.ToInt32((object) ObligatoryObjectAttributes.F_GUID);
      string str = int32_1.ToString();
      dataColumn.Caption = str;
    }
    IDBAttribute attributeById1 = (dbAttributable1 ?? (IDBAttributable) session.GetObjectActualCopy(tableId, false)).GetAttributeByID(Consts.ImbaseInternalTableNameAttID);
    if (attributeById1 != null)
      keyInfo.TableName = Convert.ToString(attributeById1.Value);
    if (dbAttributable2 == null && linkId != 0L)
      dbAttributable2 = (IDBAttributable) session.GetObjectActualCopy(linkId, false);
    if (dbAttributable2 == null)
      return;
    IDBAttribute attributeById2 = dbAttributable2.GetAttributeByID(Consts.ClassifFolderKeyAttId);
    if (attributeById2 == null)
      return;
    string str4 = Convert.ToString(attributeById2.Value);
    if (str4.Length <= 2)
      return;
    string classifCode = str4.Substring(0, 2);
    TableLoadHelper.GetCatalogData(session, classifCode, out keyInfo.CatalogId, out keyInfo.CatalogName);
  }

  public static void GetCatalogData(
    IUserSession session,
    string classifCode,
    out long catalogId,
    out string catalogName)
  {
    catalogName = string.Empty;
    catalogId = -1L;
    if (TableLoadHelper.catalogsTable == null)
    {
      TableLoadHelper.catalogsTable = session.GetObjectCollection(Consts.ImbaseCatalogTypeID).Select(new DBRecordSetParams(new ConditionStructure[0], new object[3]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) Consts.ClassifFolderKeyAttId,
        (object) Consts.ImbaseInternalTableNameAttID
      }));
      try
      {
        TableLoadHelper.catalogsTable.CaseSensitive = true;
        TableLoadHelper.catalogsTable.PrimaryKey = new DataColumn[1]
        {
          TableLoadHelper.catalogsTable.Columns[1]
        };
      }
      catch
      {
      }
    }
    DataRow[] dataRowArray = TableLoadHelper.catalogsTable.Select($"[{TableLoadHelper.catalogsTable.Columns[1].ColumnName}]='{classifCode}'");
    if (dataRowArray.Length == 0)
      return;
    catalogId = Convert.ToInt64(dataRowArray[0][0]);
    catalogName = Convert.ToString(dataRowArray[0][2]);
  }

  /// <summary>Получить полный путь для папки каталога Imbase.</summary>
  /// <param name="dtSource"></param>
  /// <param name="session"></param>
  /// <returns>Наименование колонки куда записан полный путь</returns>
  public static string BuildFullPathForObject(DataTable dtSource, IUserSession session)
  {
    string columnName1 = "Key";
    string columnName2 = "Path";
    string columnName3 = "FullPath";
    if (dtSource == null || dtSource.Columns.IndexOf(columnName1) == -1 || dtSource.Columns.IndexOf(columnName2) == -1)
      return string.Empty;
    if (dtSource.Columns.IndexOf(columnName3) == -1)
    {
      DataColumn column = new DataColumn(columnName3);
      dtSource.Columns.Add(column);
    }
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    foreach (DataRow row in (InternalDataCollectionBase) dtSource.Rows)
    {
      string str1 = row[columnName1].ToString();
      for (int length = str1.Length - 2; length > 0; length -= 2)
      {
        string str2 = str1.Substring(0, length);
        if (length == 2)
        {
          if (!stringList1.Contains(str2))
            stringList1.Add(str2);
        }
        else if (!stringList2.Contains(str2))
          stringList2.Add(str2);
      }
    }
    IDBObjectCollection objectCollection1 = session.GetObjectCollection(Consts.ImbaseCatalogTypeID);
    ConditionStructure conditionStructure = new ConditionStructure(Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) stringList1.ToArray(), LogicalOperators.NONE, 0, true);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new object[2]
    {
      (object) Consts.ClassifFolderKeyAttId,
      (object) ObligatoryObjectAttributes.CAPTION
    });
    paramSet.Contents = new ColumnContents[2]
    {
      ColumnContents.String,
      ColumnContents.String
    };
    foreach (DataRow row in (InternalDataCollectionBase) objectCollection1.Select(paramSet).Rows)
    {
      string data = row[0].ToString();
      string str = row[1].ToString();
      foreach (DataRow dataRow in dtSource.Select($"Key LIKE '{SQLStringHelper.QuoteLikeString(data)}%'"))
        dataRow[columnName3] = (object) str;
    }
    IDBObjectCollection objectCollection2 = session.GetObjectCollection(Consts.ImbaseFolderTypeID);
    conditionStructure = new ConditionStructure(Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) stringList2.ToArray(), LogicalOperators.NONE, 0, true);
    paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new object[2]
    {
      (object) Consts.ClassifFolderKeyAttId,
      (object) ObligatoryObjectAttributes.CAPTION
    });
    foreach (DataRow row in (InternalDataCollectionBase) objectCollection2.Select(paramSet).Rows)
    {
      string data = row[0].ToString();
      string str = row[1].ToString();
      DataRow[] dataRowArray = dtSource.Select($"Key LIKE '{SQLStringHelper.QuoteLikeString(data)}%' AND Key<>'{data}'");
      for (int index = 0; index < dataRowArray.Length; ++index)
        dataRowArray[index][columnName3] = (object) $"{dataRowArray[index][columnName3]}/{str}";
    }
    foreach (DataRow row in (InternalDataCollectionBase) dtSource.Rows)
      row[columnName3] = (object) $"{row[columnName3]}/{row[columnName2]}";
    return columnName3;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="recordsTable"></param>
  /// <param name="columnsAttributes"></param>
  /// <param name="computed"></param>
  /// <param name="list"></param>
  private static void CalcComputedColumns(
    IUserSession session,
    DataTable recordsTable,
    AttributeTypeProperties[] columnsAttributes,
    List<int> computed,
    List<CalculatedColumn> list)
  {
    IMSAttributeType[] namedValuesData;
    NamedValue[] namedValues;
    TableLoadHelper.GetNamedValuesData(recordsTable, columnsAttributes, out namedValuesData, out namedValues);
    CalcContext calcContext = (CalcContext) null;
    if (recordsTable.ExtendedProperties.ContainsKey((object) "CalcContext"))
      calcContext = recordsTable.ExtendedProperties[(object) "CalcContext"] as CalcContext;
    if (calcContext != null && calcContext.HasColumns)
    {
      List<int> columnsList = calcContext.ColumnsList;
      List<string> keyValues = new List<string>(columnsList.Count * recordsTable.Rows.Count);
      foreach (int columnIndex in columnsList)
      {
        foreach (DataRow row in (InternalDataCollectionBase) recordsTable.Rows)
        {
          string str = Convert.ToString(row[columnIndex]);
          if (str.Length > 2 && str[0] == 'I' && str[1] == 'K')
          {
            int num = keyValues.BinarySearch(str);
            if (num < 0)
              keyValues.Insert(~num, str);
          }
        }
      }
      IImbaseServer customService = session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
      if (calcContext.LinkId != 0L)
        keyValues.Insert(0, "BadId=" + calcContext.LinkId.ToString());
      calcContext.SetRecordsMap(customService.NameRecordReferences(session.SessionGUID, keyValues));
    }
    using (Parser parser = new Parser())
    {
      parser.CreateVariable += new CreateVariableEventHandler(TableLoadHelper.Parser_CreateVariable);
      try
      {
        parser.AutoDetectVariables = true;
        parser.Context = (object) columnsAttributes;
        int count = computed.Count;
        for (int index = 0; index < count; ++index)
        {
          string formula = columnsAttributes[computed[index]].Formula;
          string columnName = columnsAttributes[computed[index]].AttributeGuid.ToString();
          ExpressionTree tree;
          try
          {
            tree = parser.Parse(formula);
          }
          catch (Exception ex)
          {
            tree = (ExpressionTree) null;
            columnName = $"{columnName}!!!{ex.Message}";
          }
          list.Add(new CalculatedColumn(tree, columnName, recordsTable));
        }
        int cycledColumnIndex = -1;
        CalculatedColumn[] collection = CalculatedColumn.Sort(list.ToArray(), ref cycledColumnIndex);
        int length = collection.Length;
        for (int index = 0; index < length; ++index)
          collection[index].Calculate(recordsTable, calcContext, namedValuesData, namedValues);
        list.Clear();
        list.AddRange((IEnumerable<CalculatedColumn>) collection);
      }
      finally
      {
        parser.CreateVariable -= new CreateVariableEventHandler(TableLoadHelper.Parser_CreateVariable);
      }
    }
  }

  public static void GetNamedValuesData(
    DataTable recordsTable,
    AttributeTypeProperties[] columnsAttributes,
    out IMSAttributeType[] namedValuesData,
    out NamedValue[] namedValues)
  {
    namedValuesData = new IMSAttributeType[recordsTable.Columns.Count];
    namedValues = new NamedValue[recordsTable.Columns.Count];
    int length = columnsAttributes.Length;
    for (int index1 = 0; index1 < length; ++index1)
    {
      if (columnsAttributes[index1].MultiValueMode == MultiValueModes.SingleValueFromList)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(columnsAttributes[index1].AttributeID);
        if (attributeType.PossibleValues != null)
        {
          int index2 = recordsTable.Columns.IndexOf(columnsAttributes[index1].AttributeGuid.ToString());
          if (index2 == -1)
            index2 = recordsTable.Columns.IndexOf(columnsAttributes[index1].AttributeID.ToString());
          if (index2 != -1)
          {
            namedValuesData[index2] = attributeType;
            namedValues[index2] = new NamedValue();
          }
        }
      }
    }
  }

  /// <summary>
  /// Проверить, является ли объект Таблицей IMBASE или ссылкой на таблицу IMBASE.
  /// Получить идентификаторы ссылки на таблицу IMBASE и таблицы на которую она ссылается.
  /// </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="linkId">Идентификатор ссылки на таблицу</param>
  /// <param name="tableId">Шдентификатор таблицы</param>
  public static void CheckObjectId(
    IUserSession session,
    long objectId,
    ref long linkId,
    ref long tableId)
  {
    QuickObjectInfo quickObjectInfo = new QuickObjectInfo()
    {
      ObjectTypeID = -1
    };
    if (objectId != -1L)
      quickObjectInfo = session.GetObjectInfo(objectId);
    if (objectId == -1L)
      return;
    int objectTypeId = quickObjectInfo.ObjectTypeID;
    if (objectTypeId != Consts.ImbaseTableTypeID && objectTypeId != Consts.ImbaseTableRefTypeID)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Imbase_8"), nameof (objectId));
    if (objectTypeId == Consts.ImbaseTableRefTypeID)
    {
      linkId = objectId;
      tableId = TableLoadHelper.GetTableReference(session, objectId);
    }
    else
      tableId = objectId;
  }

  public static int IndexOfAttProp(int attributeId, AttributeTypeProperties[] props)
  {
    if (props != null)
    {
      int length = props.Length;
      for (int index = 0; index < length; ++index)
      {
        if (props[index].AttributeID == attributeId)
          return index;
      }
    }
    return -1;
  }

  public static int IndexOfAttProp(Guid attGuid, AttributeTypeProperties[] props)
  {
    if (props != null)
    {
      int length = props.Length;
      for (int index = 0; index < length; ++index)
      {
        if (props[index].AttributeGuid.Equals(attGuid))
          return index;
      }
    }
    return -1;
  }

  public static DataTable CreateStbstTable(
    DataTable source,
    ref string filter,
    List<int> substColumns,
    AttributeTypeProperties[] rowsAttProps)
  {
    DataTable stbstTable = source;
    int count = substColumns.Count;
    if (count > 0)
    {
      stbstTable = source.Copy();
      for (int index1 = 0; index1 < count; ++index1)
      {
        int substColumn = substColumns[index1];
        string str1 = substColumn.ToString();
        string newValue = $"[{substColumn}_]";
        string str2 = $"[{substColumn}]";
        int index2 = TableLoadHelper.IndexOfAttProp(substColumn, rowsAttProps);
        AttributeTypeProperties rowsAttProp = rowsAttProps[index2];
        stbstTable.Columns.Add(str1 + "_", TableLoadHelper.GetDataType(rowsAttProp.FieldType), str2);
        filter = filter.Replace(str2, newValue);
      }
    }
    return stbstTable;
  }

  public static Type GetDataType(FieldTypes fieldType)
  {
    switch (fieldType)
    {
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        fieldType = FieldTypes.ftString;
        break;
      case FieldTypes.ftMeasured:
        fieldType = FieldTypes.ftDouble;
        break;
    }
    return AttributesTypeHelper.GetTypeOfAttributeValue(fieldType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dataTable"></param>
  /// <param name="attType"></param>
  public static DataColumn CreateDataColumn(DataTable dataTable, IDBAttributeType attType)
  {
    return TableLoadHelper.CreateDataColumn(dataTable, attType.PropertiesStructure.AttributeGuid.ToString(), attType.AttributeType, TableLoadHelper.IsArray(attType));
  }

  public static DataColumn CreateDataColumn(
    DataTable dataTable,
    string columnName,
    FieldTypes attributeType,
    bool isArray)
  {
    DataColumn dataColumn1 = (DataColumn) null;
    if (dataTable.Columns.Contains(columnName))
      return dataColumn1;
    DataColumn dataColumn2;
    switch (attributeType)
    {
      case FieldTypes.ftObjectLink:
        if (isArray)
        {
          Type type = typeof (string);
          DataColumn column = new DataColumn(columnName, typeof (ValuesArray));
          column.ExtendedProperties.Add((object) "dataType", (object) type);
          dataTable.Columns.Add(column);
          dataColumn2 = column;
          break;
        }
        dataColumn2 = dataTable.Columns.Add(columnName, typeof (string));
        break;
      case FieldTypes.ftMeasured:
        if (isArray)
        {
          Type ofAttributeValue = AttributesTypeHelper.GetTypeOfAttributeValue(FieldTypes.ftDouble);
          DataColumn column = new DataColumn(columnName, typeof (ValuesArray));
          column.ExtendedProperties.Add((object) "dataType", (object) ofAttributeValue);
          dataTable.Columns.Add(column);
          dataColumn2 = column;
          break;
        }
        dataColumn2 = dataTable.Columns.Add(columnName, AttributesTypeHelper.GetTypeOfAttributeValue(FieldTypes.ftDouble));
        break;
      default:
        if (isArray)
        {
          Type ofAttributeValue = AttributesTypeHelper.GetTypeOfAttributeValue(attributeType);
          DataColumn column = new DataColumn(columnName, typeof (ValuesArray));
          column.ExtendedProperties.Add((object) "dataType", (object) ofAttributeValue);
          dataTable.Columns.Add(column);
          dataColumn2 = column;
          break;
        }
        dataColumn2 = dataTable.Columns.Add(columnName, AttributesTypeHelper.GetTypeOfAttributeValue(attributeType));
        break;
    }
    return dataColumn2;
  }

  public static bool IsArray(IDBAttributeType attType)
  {
    return attType.MultipleValued == MultiValueModes.MultiValuesFromList || attType.MultipleValued == MultiValueModes.MultiValues;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public static DataSet CreateDataSet()
  {
    DataTable dataTable1 = new DataTable("IMS_ATTR_TYPES");
    DataColumn column = new DataColumn("F_ATTRIBUTE_GUID", typeof (string));
    dataTable1.Columns.Add(column);
    dataTable1.Columns.Add(new DataColumn("F_REQUIRED", typeof (int)));
    dataTable1.Columns.Add(new DataColumn("F_COMPUTED", typeof (int)));
    dataTable1.Columns.Add(new DataColumn("F_FORMULA", typeof (string)));
    dataTable1.Columns.Add(new DataColumn("F_UNIQUE", typeof (int)));
    dataTable1.Columns.Add(new DataColumn("F_DEFAULT_VALUE", typeof (string)));
    dataTable1.Columns.Add(new DataColumn("F_OPTIONS", typeof (int)));
    dataTable1.Columns.Add(new DataColumn("F_MASK", typeof (string)));
    dataTable1.Columns.Add(new DataColumn("F_UNITS", typeof (string)));
    dataTable1.Columns.Add(new DataColumn("F_DISPLAY", typeof (string)));
    dataTable1.PrimaryKey = new DataColumn[1]{ column };
    dataTable1.AcceptChanges();
    DataTable dataTable2 = new DataTable("IMS_DATA");
    dataTable2.Columns.Add(new DataColumn("F_GUID", typeof (Guid)));
    dataTable2.Columns.Add(new DataColumn("F_KEY", typeof (int))
    {
      AutoIncrement = true,
      AutoIncrementSeed = 1L
    });
    dataTable2.AcceptChanges();
    return new DataSet("IMS_TABLE_RECORDS")
    {
      Tables = {
        dataTable1,
        dataTable2
      }
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="col"></param>
  /// <param name="value"></param>
  /// <param name="onlyEmpty"></param>
  public static void FillColumn(DataColumn col, object value, bool onlyEmpty)
  {
    if (TableLoadHelper.IsNull(value))
      return;
    if (value is MeasuredValue)
      value = (object) ((MeasuredValue) value).Value;
    if (col.DataType.Equals(typeof (DateTime)) && Intermech.Consts.CurrentDateFunction.Equals(value))
      value = (object) DateTime.Now;
    if (col.ExtendedProperties.ContainsKey((object) "dataType"))
    {
      Type dataType = (Type) null;
      object extendedProperty = col.ExtendedProperties[(object) "dataType"];
      if (extendedProperty != null)
      {
        if (!(extendedProperty is string typeName))
        {
          if (extendedProperty is Type type)
            dataType = type;
        }
        else
          dataType = Type.GetType(typeName);
      }
      if (dataType != (Type) null)
        value = (object) TableLoadHelper.CreateArray(dataType, value);
    }
    foreach (DataRow row in (InternalDataCollectionBase) col.Table.Rows)
    {
      if (row.RowState != DataRowState.Deleted)
      {
        if (onlyEmpty)
        {
          if (TableLoadHelper.IsNull(row[col]))
          {
            try
            {
              row[col] = value;
            }
            catch
            {
            }
          }
        }
        else
          row[col] = value;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="attTable"></param>
  /// <returns></returns>
  public static AttributeTypeProperties[] GetAttProperties(IUserSession session, DataTable attTable)
  {
    DataRowCollection rows = attTable.Rows;
    int count = rows.Count;
    AttributeTypeProperties[] attProperties = new AttributeTypeProperties[count];
    Guid guid = new Guid("cad00005-306c-11d8-b4e9-00304f19f545");
    for (int index = 0; index < count; ++index)
    {
      DataRow dataRow = rows[index];
      string g = Convert.ToString(dataRow["F_ATTRIBUTE_GUID"]);
      IDBAttributeType attributeType = session.GetAttributeType(new Guid(g), false);
      if (attributeType != null)
      {
        attProperties[index] = attributeType.PropertiesStructure;
        Convert.ToInt32(dataRow["F_REQUIRED"]);
        attProperties[index].Computed = (ComputeValueModes) Convert.ToInt32(dataRow["F_COMPUTED"]);
        attProperties[index].DefaultValue = dataRow["F_DEFAULT_VALUE"];
        attProperties[index].Options = (AttributeOptions) Convert.ToInt64(dataRow["F_OPTIONS"]);
        attProperties[index].Unique = (UniqueValueModes) Convert.ToInt32(dataRow["F_UNIQUE"]);
        attProperties[index].Formula = Convert.ToString(dataRow["F_FORMULA"]);
      }
    }
    return attProperties;
  }

  /// <summary>
  /// Получить идентификатор таблицы на которую указывает ссылка на таблицу.
  /// </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="linkId">Идентификатор ссылки на таблицу</param>
  /// <returns>Идентификатор таблицы</returns>
  public static long GetTableReference(IUserSession session, long linkId)
  {
    long num = 0;
    if (linkId != 0L)
    {
      IDBObject dbObject = session.GetObject(linkId, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(Consts.ImbaseTableRefAttID);
        if (attributeById != null && attributeById.Values[0] != null)
          num = attributeById.AsInteger;
      }
    }
    return num != 0L ? num : throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Imbase_7"), "ObjectId");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="tableId"></param>
  /// <returns></returns>
  public static DataSet GetTables(IUserSession session, long tableId, bool allowNull)
  {
    DataSet tables = TableLoadHelper._cache == null ? TableLoadHelper.GetTablesInternal(session, tableId) : TableLoadHelper._cache.Load(session, tableId);
    if (tables == null && !allowNull)
      tables = TableLoadHelper.CreateDataSet();
    return tables;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="tableId"></param>
  /// <returns></returns>
  public static DataSet GetTablesInternal(IUserSession session, long tableId, bool actualCopy = true)
  {
    return TableLoadHelper.GetTablesInternal(!actualCopy ? session.GetObject(tableId, true) : session.GetObjectActualCopy(tableId, true));
  }

  /// <summary>
  /// Метод распаковывает (при необходимости) таблицу Imbase из массива байтов Data
  /// </summary>
  /// <param name="Data">Массив с данными таблицы</param>
  /// <param name="arcMethod">Метод запаковки</param>
  /// <param name="realFileSize">Реальная длина данных (после распаковки)</param>
  /// <returns></returns>
  public static DataSet GetTablesDatasetInternal(
    byte[] Data,
    ArcMethods arcMethod,
    long realFileSize)
  {
    DataSet tablesDatasetInternal = (DataSet) null;
    MemoryStream inStream = new MemoryStream(Data);
    MemoryStream memoryStream = arcMethod == ArcMethods.ZLibPacked ? new MemoryStream(Convert.ToInt32(realFileSize)) : inStream;
    bool flag = false;
    try
    {
      try
      {
        if (arcMethod == ArcMethods.ZLibPacked)
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream, (Stream) inStream);
      }
      catch
      {
        memoryStream = inStream;
        flag = true;
      }
      memoryStream.Position = 0L;
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      try
      {
        tablesDatasetInternal = (DataSet) binaryFormatter.Deserialize((Stream) memoryStream);
        tablesDatasetInternal.RemotingFormat = SerializationFormat.Binary;
      }
      catch
      {
        tablesDatasetInternal = (DataSet) null;
      }
    }
    finally
    {
      inStream.Close();
      if (arcMethod == ArcMethods.ZLibPacked && !flag)
        memoryStream.Close();
    }
    return tablesDatasetInternal;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="tableObject"></param>
  /// <returns></returns>
  public static DataSet GetTablesInternal(IDBObject tableObject)
  {
    DataSet tablesInternal = (DataSet) null;
    if (tableObject != null)
    {
      IDBAttribute dbAttribute = tableObject.GetAttributeByID(TableLoadHelper.ShortBlobTableDataAttId);
      if (dbAttribute != null && dbAttribute is IDBShortBlobAttribute shortBlobAttribute)
      {
        ShortBlobValue blobValue = shortBlobAttribute.GetBlobValue();
        if (blobValue.RealFileSize != 0L)
          return TableLoadHelper.GetTablesDatasetInternal(blobValue.Value, blobValue.ArcMethod, blobValue.RealFileSize);
        dbAttribute = (IDBAttribute) null;
      }
      if (dbAttribute == null)
        dbAttribute = tableObject.GetAttributeByID(TableLoadHelper.LongBlobTableDataAttId);
      if (dbAttribute != null)
      {
        IBlobReader blobReader = (IBlobReader) dbAttribute;
        if (blobReader != null)
        {
          BlobInformation blobInformation = blobReader.OpenBlob(0);
          try
          {
            if (blobInformation.RealFileSize > 0L)
            {
              byte[] Data = blobReader.ReadDataBlock(0);
              if (Data != null)
                tablesInternal = TableLoadHelper.GetTablesDatasetInternal(Data, blobInformation.ArcMethod, blobInformation.RealFileSize);
            }
          }
          finally
          {
            blobReader.CloseBlob();
          }
        }
      }
    }
    return tablesInternal;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public static bool IsNull(object value)
  {
    return value == null || DBNull.Value.Equals(value) || string.Empty.Equals(value);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dataSet"></param>
  /// <returns></returns>
  private static bool IsValidDataSet(DataSet dataSet)
  {
    return dataSet != null && !(dataSet.DataSetName != "IMS_TABLE_RECORDS") && dataSet.Tables.Count == 2 && dataSet.Tables.Contains("IMS_DATA") && dataSet.Tables.Contains("IMS_ATTR_TYPES");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="vea"></param>
  public static void Parser_CreateVariable(object sender, VariableEventArgs vea)
  {
    AttributeTypeProperties[] context = (sender as Parser).Context as AttributeTypeProperties[];
    int length = context.Length;
    for (int index = 0; index < length; ++index)
    {
      if (context[index].AttributeGuid.ToString().Equals(vea.Variable.Name, StringComparison.InvariantCultureIgnoreCase))
      {
        Type type = ImbaseHelper.AttTypeToType(context[index].FieldType);
        if (type.Equals(vea.Variable.Type))
          break;
        vea.Variable = new Variable(vea.Name, type);
        break;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dataTable"></param>
  /// <param name="attType"></param>
  public static void RemoveDataColumn(DataTable dataTable, IDBAttributeType attType)
  {
    string name = attType.PropertiesStructure.AttributeGuid.ToString();
    DataColumn column = dataTable.Columns[name];
    if (column == null)
      return;
    dataTable.Columns.Remove(column);
  }

  public static void StoreData(
    IUserSession session,
    long tableId,
    DataSet dataSet,
    ITablesIndexer indexingService)
  {
    if (!TableLoadHelper.IsValidDataSet(dataSet))
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Imbase_9"));
    IDBObject objectActualCopy = session.GetObjectActualCopy(tableId, true);
    TableLoadHelper.StoreData(session, dataSet, tableId, objectActualCopy, indexingService);
  }

  /// <summary>Сохраняет в базе данных измененный DataSet таблицы</summary>
  /// <param name="session">сессия</param>
  /// <param name="tableId">идентификатор таблицы</param>
  /// <param name="dataSet">измененные данные</param>
  /// <param name="indexingService">сервис индексирования</param>
  public static void StoreData(
    IUserSession session,
    DataSet dataSet,
    long tableId,
    IDBObject tableObject,
    ITablesIndexer indexingService)
  {
    DataTable table = dataSet.Tables["IMS_DATA"];
    if (table.Columns.Contains("F_GUID"))
    {
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        if (!GuidHelper.IsGuid(Convert.ToString(row["F_GUID"])))
          row["F_GUID"] = (object) Guid.NewGuid();
      }
      table.AcceptChanges();
    }
    dataSet.RemotingFormat = SerializationFormat.Binary;
    using (MemoryStream memoryStream = new MemoryStream(32000))
    {
      new BinaryFormatter().Serialize((Stream) memoryStream, (object) dataSet);
      using (MemoryStream outStream = new MemoryStream(Convert.ToInt32(memoryStream.Length / 2L)))
      {
        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) memoryStream, Convert.ToInt32((object) ZLibCompressLevels.LevelMax));
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(TableLoadHelper.ShortBlobTableDataAttId);
        IDBAttribute dbAttribute = tableObject.GetAttributeByID(TableLoadHelper.ShortBlobTableDataAttId);
        IDBAttribute attributeById = tableObject.GetAttributeByID(TableLoadHelper.LongBlobTableDataAttId);
        IDBTransactions customService1 = session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
        if (tableObject is IImbaseDBObject imbaseDbObject)
          imbaseDbObject.AllowSkipSiteCheck = true;
        try
        {
          customService1.StartTransaction();
          if (session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService2)
            customService2.LogDataChanges(session.SessionGUID, tableId, dataSet);
          if (attributeType.SizeType > outStream.Length)
          {
            attributeById?.Delete(0L);
            if (dbAttribute == null)
            {
              tableObject.Attributes.AddAttribute(TableLoadHelper.ShortBlobTableDataAttId, true);
              dbAttribute = tableObject.GetAttributeByID(TableLoadHelper.ShortBlobTableDataAttId);
            }
          }
          else
          {
            dbAttribute?.Clear();
            dbAttribute = attributeById;
            if (dbAttribute == null)
            {
              tableObject.Attributes.AddAttribute(TableLoadHelper.LongBlobTableDataAttId, true);
              dbAttribute = tableObject.GetAttributeByID(TableLoadHelper.LongBlobTableDataAttId);
            }
          }
          IBlobWriter blobWriter = dbAttribute as IBlobWriter;
          blobWriter.OpenBlob(new BlobInformation(memoryStream.Length, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, $"Data records for table {tableId}"), false);
          blobWriter.WriteDataBlock(outStream.ToArray());
          customService1.Commit();
        }
        catch (Exception ex)
        {
          customService1.Rollback();
          throw;
        }
        finally
        {
          if (imbaseDbObject != null)
            imbaseDbObject.AllowSkipSiteCheck = false;
        }
        (indexingService ?? session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer)?.UpdateTable(session.SessionGUID, tableId);
        if (tableObject.ObjectType != Consts.ImbaseTableMixTypeID || !(session.GetCustomService(typeof (IRecepturesService)) is IRecepturesService customService3))
          return;
        customService3.UpdateCacheAfterTableMixEdit(session, tableObject.ObjectID, table);
        customService3.UpdateCacheOnAnotherServers(session, tableObject.ObjectID);
      }
    }
  }

  /// <summary>Сохраняет в базе данных измененный DataSet таблицы</summary>
  /// <param name="session">сессия</param>
  /// <param name="tableID">идентификатор таблицы</param>
  /// <param name="ds">измененные данные</param>
  /// <param name="indexer">сервис индексирования</param>
  public static void StoreDataAndIndexes(
    IUserSession session,
    long tableID,
    DataSet ds,
    ITablesIndexer indexer)
  {
    if (!(session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService1))
      return;
    customService1.StartTransaction();
    try
    {
      TableLoadHelper.StoreData(session, tableID, ds, indexer);
      IImbaseIndexingService customService2 = session.GetCustomService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
      customService1.Commit();
    }
    catch (Exception ex)
    {
      customService1.Rollback();
      throw;
    }
  }

  /// <summary>
  /// Получить идентификатор каталога по идентификатору объекта IMBASE, который принадлежит данному каталогу.
  /// </summary>
  /// <param name="session"></param>
  /// <param name="objID"></param>
  /// <returns></returns>
  public static long GetCatalogIDByObjectID(IUserSession session, long objID)
  {
    long catalogIdByObjectId = 0;
    if (session != null && objID != 0L)
    {
      IDBObject dbObject = session.GetObject(objID, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(Consts.ClassifFolderKeyAttId);
        if (attributeById != null)
        {
          IDBObjectCollection objectCollection = session.GetObjectCollection(Consts.ImbaseCatalogTypeID);
          if (objectCollection != null)
          {
            string asString = attributeById.AsString;
            if (asString.Length > 3)
            {
              string conditionValue = asString.Substring(0, 2);
              ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
              DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
              {
                new ConditionStructure(Consts.ClassifFolderKeyAttId, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0, true)
              }, new ColumnDescriptor[1]{ columnDescriptor });
              DataTable dataTable = objectCollection.Select(paramSet);
              catalogIdByObjectId = dataTable == null || dataTable.Rows.Count <= 0 ? 0L : Convert.ToInt64(dataTable.Rows[0][0]);
            }
          }
        }
      }
    }
    return catalogIdByObjectId;
  }

  /// <summary>
  /// Получить таблицу идентификаторов ссылок на таблицу и соответствующих им идентификаторов таблиц.
  /// </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="classifKey">Ключ папки классификатора объекта IMBASE (каталога/папки)</param>
  /// <returns>Таблица идентификаторов</returns>
  /// <remarks>Выбрать все яр IMBASE, у которых ключ папки классификатора начинается с classifKe и у которых заполнен атрибут "Ссылка на таблицу IMBASE"</remarks>
  public static DataTable GetTableRefData(IUserSession session, string classifKey)
  {
    DataTable dataTable = (DataTable) null;
    IDBObjectCollection objectCollection = session.GetObjectCollection(Consts.ImbaseTableRefTypeID);
    if (objectCollection != null)
    {
      ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
      ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
      ColumnDescriptor columnDescriptor3 = new ColumnDescriptor((object) Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKey, LogicalOperators.AND, 0, false),
        new ConditionStructure(Consts.ImbaseTableRefAttID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
      }, new ColumnDescriptor[3]
      {
        columnDescriptor1,
        columnDescriptor2,
        columnDescriptor3
      });
      dataTable = objectCollection.Select(paramSet);
    }
    return dataTable == null || dataTable.Rows.Count != 0 ? dataTable : (DataTable) null;
  }

  /// <summary>
  /// Получить объекты типа "Ссылка на таблицу IMBASE", которые ссылаются на указанную таблицу.
  /// </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="tableID">Идентификатор таблицы IMBASE</param>
  /// <returns>Таблица идентификаторов</returns>
  /// <remarks>Сортируется по ключу папки классификатора</remarks>
  public static DataTable GetTableRefIDsByTableID(IUserSession session, long tableID)
  {
    DataTable tableRefIdsByTableId = (DataTable) null;
    IDBObjectCollection objectCollection = session.GetObjectCollection(Consts.ImbaseTableRefTypeID);
    if (objectCollection != null)
    {
      ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
      ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.ASC, 0);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(Consts.ImbaseTableRefAttID, RelationalOperators.Equal, (object) Math.Abs(tableID), LogicalOperators.NONE, 0, false)
      }, new ColumnDescriptor[2]
      {
        columnDescriptor1,
        columnDescriptor2
      });
      tableRefIdsByTableId = objectCollection.Select(paramSet);
      if (tableRefIdsByTableId != null && tableRefIdsByTableId.Rows.Count > 0)
      {
        tableRefIdsByTableId.Columns[0].ColumnName = "F_LINK_ID";
        tableRefIdsByTableId.Columns[1].ColumnName = "F_KEY";
      }
      else
        tableRefIdsByTableId = (DataTable) null;
    }
    return tableRefIdsByTableId;
  }

  /// <summary>
  /// Получение идентификаторов каталога по их ключам папок классификаторов.
  /// </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="keys">Ключи папок классификаторов каталога</param>
  /// <param name="descriptors"></param>
  /// <param name="conditions"></param>
  /// <returns>Словарь "Идентификатор каталога - ключ папки классификатора"</returns>
  public static DataTable GetCatalogsInfoByClassifKeys(
    IUserSession session,
    IEnumerable<string> keys,
    IEnumerable<ColumnDescriptor> descriptors = null,
    IEnumerable<ConditionStructure> conditions = null)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Consts.ImbaseCatalogTypeID);
    if (objectCollection == null)
      throw new IndexingException(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_Catalog_Error"));
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.ASC, 0)
    };
    if (descriptors != null)
      columnDescriptorList.AddRange(descriptors);
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>()
    {
      new ConditionStructure(Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) keys.ToArray<string>(), LogicalOperators.NONE, 0, false)
    };
    if (conditions != null)
      conditionStructureList.AddRange(conditions);
    DataTable dataTable = objectCollection.Select(new DBRecordSetParams(conditionStructureList.ToArray(), columnDescriptorList.ToArray()));
    return dataTable == null || dataTable.Rows.Count <= 0 ? (DataTable) null : dataTable;
  }

  /// <summary>
  /// Получить объекты типа "Ссылка на таблицу IMBASE", которые ссылаются на указанную таблицу.
  /// </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="tableID">Идентификатор таблицы IMBASE</param>
  /// <returns>Таблица идентификаторов</returns>
  /// <remarks>Сортируется по ключу папки классификатора</remarks>
  public static List<long> GetListTableRefIDsByTableID(IUserSession session, long tableID)
  {
    List<long> longList = (List<long>) null;
    DataTable tableRefIdsByTableId = TableLoadHelper.GetTableRefIDsByTableID(session, tableID);
    if (tableRefIdsByTableId != null)
      longList = tableRefIdsByTableId.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Math.Abs(Convert.ToInt64(x["F_LINK_ID"])))).ToList<long>().Distinct<long>().ToList<long>();
    return longList == null || longList.Count <= 0 ? (List<long>) null : longList;
  }

  /// <summary>Создать по прототипу объект типа "Таблица IMBASE".</summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="prototypeTableID">Идентификатор протатипа таблицы</param>
  /// <param name="clearData">Необходимость очистки данных</param>
  /// <returns>Идентификатор созданного объекта</returns>
  public static long CreateTableByPrototype(
    IUserSession session,
    long prototypeTableID,
    bool clearData = false)
  {
    long tableByPrototype = 0;
    if (session != null && prototypeTableID != 0L)
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(Consts.ImbaseTableTypeID);
      if (objectCollection == null)
        throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_TableType_NullCollection"));
      IDBObject tableObject = (IDBObject) null;
      try
      {
        tableObject = objectCollection.Create(prototypeTableID);
        if (tableObject != null)
        {
          List<AttributeValues> attributeValuesList = new List<AttributeValues>(2);
          IDBAttribute attributeByGuid = tableObject.GetAttributeByGuid(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null)
          {
            if (attributeByGuid.AttributeType is IDBAttributeType4 attributeType && attributeType.Required == RequiredModes.AutoRequired)
            {
              if ((attributeType.Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
                attributeValuesList.Add(new AttributeValues(attributeByGuid.AttributeID, (object) 0));
              else
                attributeValuesList.Add(new AttributeValues(attributeByGuid.AttributeID, (object) DBNull.Value));
            }
            else
              attributeByGuid.Delete(0L);
          }
          IDBAttribute attributeById = tableObject.GetAttributeByID(Consts.ImbaseInternalTableNameAttID);
          if (attributeById != null)
            attributeValuesList.Add(new AttributeValues(Consts.ImbaseInternalTableNameAttID, (object) ImbaseHelper.CreateInternalTableName(session, Convert.ToString(attributeById.Value))));
          tableObject.SetAttributesValues(attributeValuesList.ToArray());
          DataSet tablesInternal = TableLoadHelper.GetTablesInternal(tableObject);
          if (tablesInternal != null && tablesInternal.Tables.Contains("IMS_DATA"))
          {
            DataTable table = tablesInternal.Tables["IMS_DATA"];
            if (clearData)
            {
              table.Clear();
              table.AcceptChanges();
            }
            else
              TableLoadHelper.ChangeRecordGuids(table);
            TableLoadHelper.StoreData(session, tableObject.ObjectID, tablesInternal, session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
          }
          tableObject.CommitCreation(false, true);
          tableByPrototype = tableObject.ObjectID;
        }
      }
      catch
      {
        tableObject?.Delete(0L);
      }
    }
    return tableByPrototype;
  }

  /// <summary>
  /// Создать по прототипу объект типа "Ссылка на таблицу IMBASE".
  /// </summary>
  /// <param name="session">Сессия пользователя</param>
  /// <param name="parentID">Идентификатор родительского объекта</param>
  /// <param name="prototypeTableRefID">Идентификатор прототипа ссылки на таблицу IMBASE</param>
  /// <param name="relationType">Идентификатор связи</param>
  /// <param name="tableID">Идентификатор таблицы, на которую будет ссылаться созданный объект</param>
  /// <param name="clearTableRefAttr">Необходимость очистки значения атрибута "Ссылка на таблицу IMBASE"</param>
  /// <returns>Пара значений - идентификатор созданного объекта, идентификатор созданной записи</returns>
  public static Tuple<long, long> CreateTableRefByPrototype(
    IUserSession session,
    long parentID,
    long prototypeTableRefID,
    int relationType,
    long tableID = 0,
    bool clearTableRefAttr = false)
  {
    Tuple<long, long> tableRefByPrototype = (Tuple<long, long>) null;
    if (session != null && parentID != 0L && prototypeTableRefID != 0L && relationType != -1)
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(Consts.ImbaseTableRefTypeID);
      if (objectCollection == null)
        throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_TableRefType_NullCollection"));
      IDBRelationCollection relationCollection = session.GetRelationCollection(relationType);
      if (relationCollection == null)
        throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Imbase_NamedRelationCollection_Null"), (object) MetaDataHelper.GetRelationTypeName(relationType)));
      IDBObject dbObject = (IDBObject) null;
      IDBRelation dbRelation = (IDBRelation) null;
      try
      {
        dbObject = objectCollection.Create(prototypeTableRefID);
        if (dbObject != null)
        {
          List<AttributeValues> attributeValuesList = new List<AttributeValues>(2);
          if (clearTableRefAttr)
            attributeValuesList.Add(new AttributeValues(Consts.ImbaseTableRefAttID, (object) DBNull.Value));
          else if (tableID != 0L)
            attributeValuesList.Add(new AttributeValues(Consts.ImbaseTableRefAttID, (object) Math.Abs(tableID)));
          IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null)
          {
            if (attributeByGuid.AttributeType is IDBAttributeType4 attributeType && attributeType.Required == RequiredModes.AutoRequired)
            {
              if ((attributeType.Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
                attributeValuesList.Add(new AttributeValues(attributeByGuid.AttributeID, (object) 0));
              else
                attributeValuesList.Add(new AttributeValues(attributeByGuid.AttributeID, (object) DBNull.Value));
            }
            else
              attributeByGuid.Delete(0L);
          }
          dbObject.SetAttributesValues(attributeValuesList.ToArray());
          dbRelation = relationCollection.Create(parentID, dbObject.ObjectID, DateTime.Now);
          dbObject.CommitCreation(false, true);
          tableRefByPrototype = Tuple.Create<long, long>(dbObject.ObjectID, dbRelation.RelationID);
        }
      }
      catch
      {
        dbRelation?.Delete(0L);
        dbObject?.Delete(0L);
      }
    }
    return tableRefByPrototype;
  }

  public static string QuoteString(string value)
  {
    string oldValue = "'";
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(oldValue);
    stringBuilder.Append(value.Replace(oldValue, oldValue + oldValue));
    stringBuilder.Append(oldValue);
    return stringBuilder.ToString();
  }

  public static ValuesArray CreateArray(Type dataType, object defValue)
  {
    TypeConverter converter = TypeDescriptor.GetConverter(dataType);
    Array instance = Array.CreateInstance(typeof (object), 1);
    if (converter != null && converter.IsValid(defValue))
      instance.SetValue(converter.ConvertFrom(defValue), 0);
    else
      instance.SetValue((object) DBNull.Value, 0);
    return new ValuesArray(instance, dataType);
  }

  public static void ChangeRecordGuids(DataTable dataTable)
  {
    if (dataTable == null || !dataTable.Columns.Contains("F_GUID"))
      return;
    DataColumn column = dataTable.Columns["F_GUID"];
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      row[column] = (object) Guid.NewGuid().ToString();
    dataTable.AcceptChanges();
  }
}
