// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBIntegerAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

internal class DBIntegerAttributeType : DBAttributeType
{
  public DBIntegerAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftInteger, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._DataType = typeof (long);
    this._UniquedAttribute = true;
    this.CompatibleTypes = new FieldTypes[5]
    {
      FieldTypes.ftAutoInc,
      FieldTypes.ftInteger,
      FieldTypes.ftDouble,
      FieldTypes.ftObjectLink,
      FieldTypes.ftString
    };
  }

  internal override string ColumnSQL
  {
    get => $"{base.ColumnSQL} {this.UserSession.DataManager.DataProvider.INTEGERType}";
  }

  public override void ValidateDefaultValue(object newValue)
  {
    if (newValue == null || !(newValue.ToString() != string.Empty))
      return;
    Convert.ToInt64(newValue);
  }

  public override object DefaultValue
  {
    get
    {
      object defaultValue = base.DefaultValue;
      return defaultValue == DBNull.Value || defaultValue == null || defaultValue.ToString() == string.Empty ? (object) null : (object) Convert.ToInt64(defaultValue);
    }
  }

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxNumericSize);
  }

  public override string SizeTypeDescription => string.Empty;

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    string str = string.Empty;
    switch (newType)
    {
      case FieldTypes.ftString:
        str = $"F_STRING_VALUE = CAST(F_INTEGER_VALUE AS {this.UserSession.DataManager.DataProvider.NVARCHARType(Convert.ToInt32(this.SizeType))})";
        break;
      case FieldTypes.ftDouble:
        str = "F_DOUBLE_VALUE = F_INTEGER_VALUE";
        break;
      case FieldTypes.ftObjectLink:
        this.ConvertToObjectLinkType();
        break;
      case FieldTypes.ftAutoInc:
        List<string> objectAttrsTables1 = this.UserSession.DBCache.GetObjectAttrsTables();
        objectAttrsTables1.Add("IMS_RELATION_ATTRS");
        long num = 0;
        for (int index = 0; index < objectAttrsTables1.Count; ++index)
        {
          object obj = this.UserSession.DataManager.ExecuteScalar($"SELECT MAX(F_INTEGER_VALUE) FROM {objectAttrsTables1[index]} WHERE F_ATTRIBUTE_ID = {this.AttributeID}");
          long int64 = obj == null || obj == DBNull.Value ? 0L : Convert.ToInt64(obj);
          if (int64 > num)
            num = int64;
        }
        this.UserSession.DataManager.ExecuteNonQuery(this.UserSession.DataManager.DataProvider.CreateGeneratorString($"IMT_A{this.AttributeID.ToString()}_GEN", num + 1L, 1));
        break;
      case FieldTypes.ftGuid:
        this.ConvertToGuidType();
        break;
    }
    if (!(str != string.Empty) && newType != FieldTypes.ftBoolean)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    List<string> objectAttrsTables2 = this.UserSession.DBCache.GetObjectAttrsTables();
    objectAttrsTables2.Add("IMS_RELATION_ATTRS");
    objectAttrsTables2.Add("IMS_OBJ_SNAPATTRS");
    objectAttrsTables2.Add("IMS_REL_SNAPATTRS");
    for (int index = 0; index < objectAttrsTables2.Count; ++index)
    {
      if (newType == FieldTypes.ftBoolean)
      {
        dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables2[index]} SET F_INTEGER_VALUE = 1 WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND F_INTEGER_VALUE <> 0");
        dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables2[index]} SET F_INTEGER_VALUE = 0 WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND F_INTEGER_VALUE = 0");
      }
      else
        dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables2[index]} SET {str} WHERE F_ATTRIBUTE_ID = {this.AttributeID}");
    }
    if (newType == FieldTypes.ftBoolean)
      return;
    this.ClearValues("F_INTEGER_VALUE");
  }

  private void ConvertToObjectLinkType()
  {
    IDbManager dataManager = this.UserSession.DataManager;
    List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
    objectAttrsTables.Add("IMS_RELATION_ATTRS");
    objectAttrsTables.Add("IMS_OBJ_SNAPATTRS");
    objectAttrsTables.Add("IMS_REL_SNAPATTRS");
    IDbDataParameter dbDataParameter = dataManager.Parameter("attrID", (object) this.AttributeID);
    for (int index1 = 0; index1 < objectAttrsTables.Count; ++index1)
    {
      string columnName = objectAttrsTables[index1] == "IMS_RELATION_ATTRS" || objectAttrsTables[index1] == "IMS_REL_SNAPATTRS" ? "F_PRJLINK_ID" : "F_OBJECT_ID";
      DataTable dataTable1 = dataManager.ExecuteDataTable($"SELECT * FROM {objectAttrsTables[index1]} WHERE (F_ATTRIBUTE_ID = :attrID) AND (F_INTEGER_VALUE IS NOT NULL)", dbDataParameter);
      bool flag = dataTable1.Columns.IndexOf("F_SNAPSHOT_ID") >= 0;
      for (int index2 = 0; index2 < dataTable1.Rows.Count; ++index2)
      {
        long int64 = Convert.ToInt64(dataTable1.Rows[index2]["F_INTEGER_VALUE"]);
        DataTable dataTable2 = dataManager.ExecuteDataTable("SELECT * FROM IMS_GUID WHERE F_OBJECT_ID = :objID", dataManager.Parameter("objID", (object) Math.Abs(int64)));
        if (dataTable2.Rows.Count > 0)
        {
          string str1 = string.Empty;
          if (flag)
            str1 = " AND F_SNAPSHOT_ID = " + dataTable1.Rows[index2]["F_SNAPSHOT_ID"].ToString();
          string str2 = dataTable2.Rows[0]["CAPTION"].ToString();
          if (str2 == string.Empty)
            str2 = dataTable2.Rows[0]["F_WORK_CAPTION"].ToString();
          string str3 = string.Empty;
          if (int64 < 0L)
            str3 = ", F_INTEGER_VALUE = " + Math.Abs(int64).ToString();
          dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables[index1]} SET F_STRING_VALUE = :capt1{str3} WHERE {columnName} = :keyID1 AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :inlistID{str1}", dataManager.Parameter("capt1", (object) str2), dataManager.Parameter("keyID1", (object) Convert.ToInt64(dataTable1.Rows[index2][columnName])), dbDataParameter, dataManager.Parameter("inlistID", (object) Convert.ToInt32(dataTable1.Rows[index2]["F_INLIST_ID"])));
          if (columnName == "F_OBJECT_ID" && !flag)
            dataManager.ExecuteNonQuery("INSERT INTO IMS_OBJECT_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TOOBJECT_ID) VALUES (:objID, :attrID, :inlistID, :toobjID)", dataManager.Parameter("objID", (object) Convert.ToInt64(dataTable1.Rows[index2][columnName])), dbDataParameter, dataManager.Parameter("inlistID", (object) Convert.ToInt32(dataTable1.Rows[index2]["F_INLIST_ID"])), dataManager.Parameter("toobjID", (object) Math.Abs(int64)));
        }
        else
          dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables[index1]} SET F_INTEGER_VALUE = NULL WHERE F_ATTRIBUTE_ID = :attrID AND {columnName} = :objID AND F_INLIST_ID = :inlistID", dbDataParameter, dataManager.Parameter("objID", (object) Convert.ToInt64(dataTable1.Rows[index2][columnName])), dataManager.Parameter("inlistID", (object) Convert.ToInt32(dataTable1.Rows[index2]["F_INLIST_ID"])));
      }
    }
  }

  private void ConvertToGuidType()
  {
    Dictionary<long, string> dictionary = new Dictionary<long, string>();
    IDbManager dataManager = this.UserSession.DataManager;
    List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
    objectAttrsTables.Add("IMS_RELATION_ATTRS");
    objectAttrsTables.Add("IMS_OBJ_SNAPATTRS");
    objectAttrsTables.Add("IMS_REL_SNAPATTRS");
    IDbDataParameter dbDataParameter = dataManager.Parameter("attrID", (object) this.AttributeID);
    for (int index1 = 0; index1 < objectAttrsTables.Count; ++index1)
    {
      string columnName = objectAttrsTables[index1] == "IMS_RELATION_ATTRS" || objectAttrsTables[index1] == "IMS_REL_SNAPATTRS" ? "F_PRJLINK_ID" : "F_OBJECT_ID";
      DataTable dataTable = dataManager.ExecuteDataTable($"SELECT * FROM {objectAttrsTables[index1]} WHERE (F_ATTRIBUTE_ID = :attrID) AND (F_INTEGER_VALUE IS NOT NULL) AND (F_INTEGER_VALUE <> 0)", dbDataParameter);
      for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
      {
        long int64 = Convert.ToInt64(dataTable.Rows[index2]["F_INTEGER_VALUE"]);
        string str1;
        if (!dictionary.TryGetValue(int64, out str1))
        {
          str1 = Guid.NewGuid().ToString();
          dictionary.Add(int64, str1);
        }
        string str2 = objectAttrsTables[index1] == "IMS_REL_SNAPATTRS" || objectAttrsTables[index1] == "IMS_OBJ_SNAPATTRS" ? " AND F_SNAPSHOT_ID = " + dataTable.Rows[index2]["F_SNAPSHOT_ID"].ToString() : string.Empty;
        dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables[index1]} SET F_STRING_VALUE = :guid_val WHERE {columnName} = :key_val1 AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :inlistID{str2}", dataManager.Parameter("guid_val", (object) str1), dataManager.Parameter("key_val1", dataTable.Rows[index2][columnName]), dbDataParameter, dataManager.Parameter("inlistID", (object) Convert.ToInt32(dataTable.Rows[index2]["F_INLIST_ID"])));
      }
    }
    this.ClearValues("F_INTEGER_VALUE");
  }

  internal override bool CompareValues(object value1, object value2)
  {
    return CompareValuesHelper.CompareIntValues(value1, value2);
  }

  public override void SetPossibleValues(DataTable valuesTable, int objectType, int relationType)
  {
    if (this.AttributeID == this.UserSession.IdentHelper.SecurityLevelID)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < valuesTable.Rows.Count; ++index)
        stringBuilder.Append(valuesTable.Rows[index][this._PossibleValueFieldName].ToString() + ",");
      --stringBuilder.Length;
      DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable(string.Format(sc_12690.ssp_appserver_12691(), (object) stringBuilder.ToString()));
      if (dataTable.Rows.Count > 0)
      {
        long[] objectsID = new long[dataTable.Rows.Count];
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          objectsID[index] = Convert.ToInt64(dataTable.Rows[index][0]);
        throw new ObjectsFoundException(string.Format(sc_12690.ssp_appserver_12692(), (object) this.Name, (object) dataTable.Rows.Count), $"Объекты с удаляемым значением атрибута '{this.Name}':", objectsID);
      }
    }
    base.SetPossibleValues(valuesTable, objectType, relationType);
  }

  protected override bool NeedRebuildView4ChangeAttrType(FieldTypes newType)
  {
    if (newType != FieldTypes.ftDouble && newType != FieldTypes.ftString)
      return true;
    IDbManager dataManager = this.UserSession.DataManager;
    if (dataManager.DataProvider.Name == "Oracle" && newType == FieldTypes.ftString)
      return true;
    string[] views4Modify = this.GetViews4Modify();
    for (int index = 0; index < views4Modify.Length; ++index)
    {
      bool flag = false;
      if (dataManager.DataProvider.Name == "Sql")
      {
        try
        {
          dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropIndexSQL(views4Modify[index], "F" + this.AttributeID.ToString(), SortOrders.ASC));
          flag = true;
        }
        catch
        {
        }
      }
      string fldType = newType != FieldTypes.ftDouble ? dataManager.DataProvider.NVARCHARType(Convert.ToInt32(this.SizeType)) : dataManager.DataProvider.FLOATType;
      dataManager.ExecuteNonQuery(dataManager.DataProvider.GetModifyColumnSQL(views4Modify[index], "F" + this.AttributeID.ToString(), fldType));
      if (flag)
        dataManager.ExecuteNonQuery(dataManager.DataProvider.GetIndexSQL(views4Modify[index], "F" + this.AttributeID.ToString(), SortOrders.ASC));
    }
    return false;
  }
}
