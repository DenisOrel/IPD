// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBStringAttributeType
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

internal class DBStringAttributeType : DBAttributeType
{
  public DBStringAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftString, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._UniquedAttribute = true;
    this.CompatibleTypes = new FieldTypes[10]
    {
      FieldTypes.ftBoolean,
      FieldTypes.ftString,
      FieldTypes.ftInteger,
      FieldTypes.ftAutoInc,
      FieldTypes.ftDateTime,
      FieldTypes.ftDouble,
      FieldTypes.ftGuid,
      FieldTypes.ftObjectLink,
      FieldTypes.ftMeasured,
      FieldTypes.ftExternalLink
    };
  }

  protected override string GetNullOperator()
  {
    return string.Format("(({0} = '') OR ({0} IS NULL))", (object) this._ValueFieldName);
  }

  internal override string ColumnSQL
  {
    get
    {
      return $"{base.ColumnSQL} {this.UserSession.DataManager.DataProvider.NVARCHARType(Convert.ToInt32(this.SizeType))}";
    }
  }

  public override RelationalOperators[] EnabledOperators
  {
    get
    {
      if (this.MultipleValued == MultiValueModes.MultiValues)
        return new RelationalOperators[8]
        {
          RelationalOperators.Empty,
          RelationalOperators.NotExistsOrEmpty,
          RelationalOperators.NotEmpty,
          RelationalOperators.Equal,
          RelationalOperators.NotEqual,
          RelationalOperators.Substring,
          RelationalOperators.StartString,
          RelationalOperators.AttributeExists
        };
      if (this.MultipleValued != MultiValueModes.MultiValuesFromList)
        return this._EnabledOperators;
      return new RelationalOperators[6]
      {
        RelationalOperators.Empty,
        RelationalOperators.NotExistsOrEmpty,
        RelationalOperators.NotEmpty,
        RelationalOperators.NotEqual,
        RelationalOperators.Equal,
        RelationalOperators.AttributeExists
      };
    }
  }

  public override void ValidateDefaultValue(object newValue)
  {
    if (newValue == null)
      return;
    long num = this._PreventedProperties.AttributeID != 0 ? this._PreventedProperties.SizeType : this.SizeType;
    if ((long) newValue.ToString().Length > num)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12714.ssp_appserver_12715()), (object) num));
  }

  public override object DefaultValue
  {
    get
    {
      object defaultValue = base.DefaultValue;
      return defaultValue == DBNull.Value ? (object) null : (object) Convert.ToString(defaultValue);
    }
  }

  public override void ValidateSizeType(long newValue)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxStringSize);
    this.CheckZeroSize(newValue);
    if (newValue < this.SizeType)
    {
      if (this.MultipleValued == MultiValueModes.MultiValuesFromList || this.MultipleValued == MultiValueModes.SingleValueFromList)
      {
        foreach (string possibleValues in this.GetPossibleValuesArray())
        {
          if ((long) possibleValues.Length > newValue)
            throw new KernelExceptionID(sc_12714.ssp_appserver_12716(1090070873), (object) newValue, (object) possibleValues, (object) possibleValues.Length);
        }
      }
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("attrID", (object) this.AttributeID);
      IDbDataParameter dbDataParameter2 = dataManager.Parameter("newValue1", (object) newValue);
      DataTable dataTable1 = dataManager.ExecuteDataTable($"{sc_12714.ssp_appserver_12717()}{dataManager.DataProvider.Length("F_STRING_VALUE")} > :newValue1)", dbDataParameter1, dbDataParameter2);
      if (dataTable1.Rows.Count > 0)
      {
        long[] relationsID = new long[dataTable1.Rows.Count];
        for (int index = 0; index < dataTable1.Rows.Count; ++index)
          relationsID[index] = Convert.ToInt64(dataTable1.Rows[index][0]);
        throw new RelationsFoundException(string.Format(sc_12714.ssp_appserver_12718(), (object) dataTable1.Rows.Count, (object) this.Name, (object) newValue), $"Связи, у которых значение атрибута '{this.Name}' превышает {newValue}:", relationsID);
      }
      List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
      DataTable toTable = (DataTable) null;
      for (int index = 0; index < objectAttrsTables.Count; ++index)
      {
        DataTable dataTable2 = dataManager.ExecuteDataTable($"SELECT DISTINCT F_OBJECT_ID FROM {objectAttrsTables[index]} WHERE (F_ATTRIBUTE_ID = :attrID) AND ({dataManager.DataProvider.Length("F_STRING_VALUE")} > :newValue1)", dbDataParameter1, dbDataParameter2);
        if (dataTable2.Rows.Count > 0)
        {
          if (toTable == null)
            toTable = dataTable2;
          else
            SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) dataTable2.Select());
        }
      }
      if (toTable != null)
      {
        long[] objectsID = new long[toTable.Rows.Count];
        for (int index = 0; index < toTable.Rows.Count; ++index)
          objectsID[index] = Convert.ToInt64(toTable.Rows[index][0]);
        throw new ObjectsFoundException(string.Format(sc_12714.ssp_appserver_12719(), (object) toTable.Rows.Count, (object) this.Name, (object) newValue), $"Объекты, у которых значение атрибута '{this.Name}' превышает {newValue}:", objectsID);
      }
    }
    foreach (string tableName in this.GetViews4Modify())
    {
      bool flag;
      try
      {
        dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropIndexSQL(tableName, "F" + this.AttributeID.ToString(), SortOrders.ASC));
        flag = true;
      }
      catch
      {
        flag = false;
      }
      dataManager.ExecuteNonQuery(dataManager.DataProvider.GetModifyColumnSQL(tableName, "F" + this.AttributeID.ToString(), dataManager.DataProvider.NVARCHARType(Convert.ToInt32(newValue))));
      if (flag)
        dataManager.ExecuteNonQuery(dataManager.DataProvider.GetIndexSQL(tableName, "F" + this.AttributeID.ToString(), SortOrders.ASC));
    }
  }

  private void CopyToMemo(string tblName, string fldName)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable($"SELECT * FROM {tblName} WHERE F_ATTRIBUTE_ID = {this.AttributeID.ToString()}");
    string commandText1 = string.Format(sc_12714.ssp_appserver_12720(), (object) tblName, (object) fldName);
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("fkey", (object) 0);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("aid", (object) 0);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("oid", (object) 0);
    IDbDataParameter dbDataParameter4 = dataManager.Parameter("iid", (object) 0);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      dbDataParameter2.Value = row["F_ATTRIBUTE_ID"];
      dbDataParameter3.Value = row[fldName];
      dbDataParameter4.Value = row["F_INLIST_ID"];
      long num = dataManager.DataProvider.BeforeInsertID("IMS_MEMOS_GEN", dataManager);
      string commandText2 = num != 0L ? $"INSERT INTO IMS_MEMOS (F_KEY, F_VALUE) select {num}, F_STRING_VALUE from {tblName} WHERE F_ATTRIBUTE_ID = :aid AND {fldName} = :oid AND F_INLIST_ID = :iid" : $"INSERT INTO IMS_MEMOS (F_VALUE) select F_STRING_VALUE from {tblName} WHERE F_ATTRIBUTE_ID = :aid AND {fldName} = :oid AND F_INLIST_ID = :iid";
      dataManager.ExecuteNonQuery(commandText2, dbDataParameter2, dbDataParameter3, dbDataParameter4);
      if (num == 0L)
        num = dataManager.DataProvider.AfterInsertID(dataManager);
      dbDataParameter1.Value = (object) num;
      dataManager.ExecuteNonQuery(commandText1, dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4);
    }
  }

  private void ConvertValues(FieldTypes newType, string tableName)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    string str = !(tableName != "IMS_RELATION_ATTRS") || !(tableName != "IMS_REL_SNAPATTRS") ? "F_PRJLINK_ID" : "F_OBJECT_ID";
    DataTable dataTable = dataManager.ExecuteDataTable(string.Format(sc_12714.ssp_appserver_12721(), (object) str, (object) tableName, (object) this.AttributeID));
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("keyPar", (object) 0L);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("inlistID", (object) 0L);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("valuePar", (object) 0L);
    string commandText1 = $"UPDATE {tableName} SET F_STRING_VALUE = NULL, F_INTEGER_VALUE = NULL, F_DOUBLE_VALUE = NULL, F_DATE_VALUE = NULL WHERE {str} = :keyPar AND F_INLIST_ID = :inlistID AND F_ATTRIBUTE_ID = {this.AttributeID}";
    string commandText2;
    switch (newType)
    {
      case FieldTypes.ftInteger:
        commandText2 = string.Format(sc_12714.ssp_appserver_12722(), (object) tableName, (object) str, (object) this.AttributeID);
        break;
      case FieldTypes.ftDouble:
        commandText2 = string.Format(sc_12714.ssp_appserver_12723(), (object) tableName, (object) str, (object) this.AttributeID);
        break;
      case FieldTypes.ftDateTime:
        commandText2 = string.Format(sc_12714.ssp_appserver_12724(), (object) tableName, (object) str, (object) this.AttributeID);
        break;
      default:
        commandText2 = sc_12714.ssp_appserver_12725();
        break;
    }
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      dbDataParameter1.Value = (object) Convert.ToInt64(dataTable.Rows[index][1]);
      string s = dataTable.Rows[index][0].ToString().Trim();
      dbDataParameter2.Value = (object) Convert.ToInt32(dataTable.Rows[index][2]);
      if (s != string.Empty)
      {
        bool flag = false;
        switch (newType)
        {
          case FieldTypes.ftInteger:
            long result1;
            if (long.TryParse(s, out result1))
            {
              dbDataParameter3.Value = (object) result1;
              dataManager.ExecuteNonQuery(commandText2, dbDataParameter3, dbDataParameter1, dbDataParameter2);
              break;
            }
            flag = true;
            break;
          case FieldTypes.ftDouble:
            double result2;
            if (!double.TryParse(s, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result2) && !double.TryParse(s, out result2))
            {
              flag = true;
              break;
            }
            dbDataParameter3.Value = (object) result2;
            dataManager.ExecuteNonQuery(commandText2, dbDataParameter3, dbDataParameter1, dbDataParameter2);
            break;
          case FieldTypes.ftDateTime:
            DateTime result3;
            if (!DateTime.TryParse(s, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result3) && !DateTime.TryParse(s, out result3))
            {
              flag = true;
              break;
            }
            dbDataParameter3.Value = (object) result3;
            dataManager.ExecuteNonQuery(commandText2, dbDataParameter3, dbDataParameter1, dbDataParameter2);
            break;
        }
        if (flag)
          dataManager.ExecuteNonQuery(commandText1, dbDataParameter1, dbDataParameter2);
      }
    }
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS").Select("F_FORMULA_ID = " + this.AttributeID.ToString());
    if (dataRowArray.Length != 0)
    {
      StringBuilder stringBuilder = new StringBuilder(Environment.NewLine + this.UserSession.GetAttributeType(Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"])).Name);
      for (int index = 1; index < dataRowArray.Length; ++index)
        stringBuilder.Append($",{Environment.NewLine}{this.UserSession.GetAttributeType(Convert.ToInt32(dataRowArray[index]["F_ATTRIBUTE_ID"])).Name}");
      throw new KernelExceptionID(sc_12714.ssp_appserver_12726(2070535189), (object) stringBuilder.ToString());
    }
    IDbManager dataManager = this.UserSession.DataManager;
    switch (newType)
    {
      case FieldTypes.ftPassword:
        List<string> objectAttrsTables1 = this.UserSession.DBCache.GetObjectAttrsTables();
        objectAttrsTables1.Add("IMS_RELATION_ATTRS");
        objectAttrsTables1.Add("IMS_OBJ_SNAPATTRS");
        objectAttrsTables1.Add("IMS_REL_SNAPATTRS");
        for (int index = 0; index < objectAttrsTables1.Count; ++index)
          dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables1} SET F_STRING_VALUE = '0'||F_STRING_VALUE WHERE F_ATTRIBUTE_ID = {this.AttributeID}");
        break;
      case FieldTypes.ftMemo:
        List<string> objectAttrsTables2 = this.UserSession.DBCache.GetObjectAttrsTables();
        for (int index = 0; index < objectAttrsTables2.Count; ++index)
          this.CopyToMemo(objectAttrsTables2[index], "F_OBJECT_ID");
        this.CopyToMemo("IMS_RELATION_ATTRS", "F_PRJLINK_ID");
        break;
      case FieldTypes.ftBoolean:
        this.ClearValues("F_INTEGER_VALUE");
        List<string> objectAttrsTables3 = this.UserSession.DBCache.GetObjectAttrsTables();
        objectAttrsTables3.Add("IMS_RELATION_ATTRS");
        objectAttrsTables3.Add("IMS_OBJ_SNAPATTRS");
        objectAttrsTables3.Add("IMS_REL_SNAPATTRS");
        for (int index = 0; index < objectAttrsTables3.Count; ++index)
        {
          IDbManager dbManager1 = dataManager;
          string[] strArray1 = new string[5]
          {
            "UPDATE ",
            objectAttrsTables3[index],
            " SET F_INTEGER_VALUE = 1 WHERE (F_ATTRIBUTE_ID = ",
            null,
            null
          };
          int attributeId = this.AttributeID;
          strArray1[3] = attributeId.ToString();
          strArray1[4] = LocalizationHolder.rm.GetString("Kernel_128");
          string commandText1 = string.Concat(strArray1);
          dbManager1.ExecuteNonQuery(commandText1);
          IDbManager dbManager2 = dataManager;
          string[] strArray2 = new string[5]
          {
            "UPDATE ",
            objectAttrsTables3[index],
            " SET F_INTEGER_VALUE = 0 WHERE (F_ATTRIBUTE_ID = ",
            null,
            null
          };
          attributeId = this.AttributeID;
          strArray2[3] = attributeId.ToString();
          strArray2[4] = LocalizationHolder.rm.GetString("Kernel_130");
          string commandText2 = string.Concat(strArray2);
          dbManager2.ExecuteNonQuery(commandText2);
        }
        break;
      case FieldTypes.ftGuid:
        List<string> objectAttrsTables4 = this.UserSession.DBCache.GetObjectAttrsTables();
        objectAttrsTables4.Add("IMS_RELATION_ATTRS");
        objectAttrsTables4.Add("IMS_OBJ_SNAPATTRS");
        objectAttrsTables4.Add("IMS_REL_SNAPATTRS");
        for (int index = 0; index < objectAttrsTables4.Count; ++index)
          this.ValidateGuidConvert(objectAttrsTables4[index]);
        break;
      default:
        List<string> objectAttrsTables5 = this.UserSession.DBCache.GetObjectAttrsTables();
        objectAttrsTables5.Add("IMS_RELATION_ATTRS");
        objectAttrsTables5.Add("IMS_OBJ_SNAPATTRS");
        objectAttrsTables5.Add("IMS_REL_SNAPATTRS");
        for (int index = 0; index < objectAttrsTables5.Count; ++index)
          this.ConvertValues(newType, objectAttrsTables5[index]);
        break;
    }
  }

  private void ValidateGuidConvert(string tblName)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.DataManager.ExecuteDataTable($"SELECT F_STRING_VALUE FROM {tblName} WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND F_STRING_VALUE IS NOT NULL AND F_STRING_VALUE <> ''").Rows)
    {
      try
      {
        Guid guid = new Guid(row[0].ToString());
      }
      catch
      {
        throw new KernelExceptionID(sc_12714.ssp_appserver_12727(40938974), row[0]);
      }
    }
  }

  internal override bool CompareValues(object value1, object value2)
  {
    return CompareValuesHelper.CompareStringValues(value1, value2);
  }
}
