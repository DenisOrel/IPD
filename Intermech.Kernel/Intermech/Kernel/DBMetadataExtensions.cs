// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBMetadataExtensions
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBMetadataExtensions(UserSession session) : DBSessionable(session), IDBMetadataExtensions
{
  private int _AttributeTypeID = -1;
  private int _ObjectTypeID = -1;
  private int _RelationTypeID = -1;

  protected void SetMDExtensionsType(int attributeTypeID, int objectTypeID, int relationTypeID)
  {
    this._AttributeTypeID = attributeTypeID;
    this._ObjectTypeID = objectTypeID;
    this._RelationTypeID = relationTypeID;
  }

  public DataTable GetExtensionsTable() => this.UserSession.DBCache.GetTable("IMS_MD_EXTENSIONS");

  public DataTable ExtensionsTable => this.GetExtensionsTable();

  protected void DeleteMDExtensions()
  {
    this.UserSession.DataManager.ExecuteNonQuery(sc_12738.ssp_appserver_12739(), this.UserSession.DataManager.Parameter("attrID", (object) this._AttributeTypeID), this.UserSession.DataManager.Parameter("objType", (object) this._ObjectTypeID), this.UserSession.DataManager.Parameter("relType", (object) this._RelationTypeID));
    this.UserSession.DBCache.DeleteRecords("IMS_MD_EXTENSIONS", $"F_ATTRIBUTE_ID = {this._AttributeTypeID} AND F_OBJECT_TYPE = {this._ObjectTypeID} AND F_RELATION_TYPE = {this._RelationTypeID}", (IUserSession) this.UserSession);
  }

  public void DeleteMDExtensions(string paramName)
  {
    this.UserSession.DataManager.ExecuteNonQuery(sc_12738.ssp_appserver_12740(), this.UserSession.DataManager.Parameter("attrID", (object) this._AttributeTypeID), this.UserSession.DataManager.Parameter("objType", (object) this._ObjectTypeID), this.UserSession.DataManager.Parameter("relType", (object) this._RelationTypeID), this.UserSession.DataManager.Parameter("parName", (object) paramName));
    this.UserSession.DBCache.DeleteRecords("IMS_MD_EXTENSIONS", $"F_ATTRIBUTE_ID = {this._AttributeTypeID} AND F_OBJECT_TYPE = {this._ObjectTypeID} AND F_RELATION_TYPE = {this._RelationTypeID} AND F_PARAM_NAME = {SqlHelper.QString(paramName)}", (IUserSession) this.UserSession);
  }

  public void SetMDValues(string valueName, int categoryType, string[] valuesList)
  {
    DataTable extensionsTable = this.GetExtensionsTable();
    DataRow[] dataRowArray = extensionsTable.Select($"F_ATTRIBUTE_ID = {this._AttributeTypeID} AND F_OBJECT_TYPE = {this._ObjectTypeID} AND F_RELATION_TYPE = {this._RelationTypeID} AND F_PARAM_NAME = {SqlHelper.QString(valueName)}", sc_12738.ssp_appserver_12741());
    bool flag = false;
    for (int index = 0; index < dataRowArray.Length; ++index)
    {
      if (index < valuesList.Length && Convert.ToString(dataRowArray[index]["F_VALUE"]) != valuesList[index])
      {
        dataRowArray[index]["F_VALUE"] = (object) valuesList[index];
        dataRowArray[index]["F_CATEGORY_TYPE"] = (object) categoryType;
        this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_MD_EXTENSIONS SET F_VALUE = :value1, F_CATEGORY_TYPE = :catType WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECT_TYPE = :objType AND F_RELATION_TYPE = :relType AND F_PARAM_NAME = :parName AND F_INLIST_ID = :lstID", this.UserSession.DataManager.Parameter("value1", (object) valuesList[index]), this.UserSession.DataManager.Parameter("catType", (object) categoryType), this.UserSession.DataManager.Parameter("attrID", (object) this._AttributeTypeID), this.UserSession.DataManager.Parameter("objType", (object) this._ObjectTypeID), this.UserSession.DataManager.Parameter("relType", (object) this._RelationTypeID), this.UserSession.DataManager.Parameter("parName", (object) valueName), this.UserSession.DataManager.Parameter("lstID", (object) index));
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this._AttributeTypeID} AND F_OBJECT_TYPE = {this._ObjectTypeID} AND F_RELATION_TYPE = {this._RelationTypeID} AND F_PARAM_NAME = {SqlHelper.QString(valueName)} AND F_INLIST_ID = {index}", "IMS_MD_EXTENSIONS", "F_VALUE", (object) valuesList[index], (IUserSession) this.UserSession);
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this._AttributeTypeID} AND F_OBJECT_TYPE = {this._ObjectTypeID} AND F_RELATION_TYPE = {this._RelationTypeID} AND F_PARAM_NAME = {SqlHelper.QString(valueName)} AND F_INLIST_ID = {index}", "IMS_MD_EXTENSIONS", "F_CATEGORY_TYPE", (object) categoryType, (IUserSession) this.UserSession);
        flag = true;
      }
    }
    if (valuesList.Length < dataRowArray.Length)
    {
      this.UserSession.DataManager.ExecuteNonQuery(sc_12738.ssp_appserver_12742(), this.UserSession.DataManager.Parameter("attrID", (object) this._AttributeTypeID), this.UserSession.DataManager.Parameter("objType", (object) this._ObjectTypeID), this.UserSession.DataManager.Parameter("relType", (object) this._RelationTypeID), this.UserSession.DataManager.Parameter("parName", (object) valueName), this.UserSession.DataManager.Parameter("lstID", (object) valuesList.Length));
      this.UserSession.DBCache.DeleteRecords("IMS_MD_EXTENSIONS", $"F_ATTRIBUTE_ID = {this._AttributeTypeID} AND F_OBJECT_TYPE = {this._ObjectTypeID} AND F_RELATION_TYPE = {this._RelationTypeID} AND F_PARAM_NAME = {SqlHelper.QString(valueName)} AND F_INLIST_ID >= {valuesList.Length}", (IUserSession) this.UserSession);
      flag = true;
    }
    else
    {
      for (int length = dataRowArray.Length; length < valuesList.Length; ++length)
      {
        this.UserSession.DataManager.ExecuteNonQuery("INSERT INTO IMS_MD_EXTENSIONS (F_ATTRIBUTE_ID, F_OBJECT_TYPE, F_RELATION_TYPE, F_PARAM_NAME, F_INLIST_ID, F_CATEGORY_TYPE, F_VALUE) VALUES (:attrID, :objType, :relType, :parName, :lstID, :catType, :value1)", this.UserSession.DataManager.Parameter("attrID", (object) this._AttributeTypeID), this.UserSession.DataManager.Parameter("objType", (object) this._ObjectTypeID), this.UserSession.DataManager.Parameter("relType", (object) this._RelationTypeID), this.UserSession.DataManager.Parameter("parName", (object) valueName), this.UserSession.DataManager.Parameter("value1", (object) valuesList[length]), this.UserSession.DataManager.Parameter("catType", (object) categoryType), this.UserSession.DataManager.Parameter("lstID", (object) length));
        DataRow row = extensionsTable.NewRow();
        row["F_ATTRIBUTE_ID"] = (object) this._AttributeTypeID;
        row["F_OBJECT_TYPE"] = (object) this._ObjectTypeID;
        row["F_RELATION_TYPE"] = (object) this._RelationTypeID;
        row["F_PARAM_NAME"] = (object) valueName;
        row["F_INLIST_ID"] = (object) length;
        row["F_CATEGORY_TYPE"] = (object) categoryType;
        row["F_VALUE"] = (object) valuesList[length];
        extensionsTable.Rows.Add(row);
        flag = true;
      }
    }
    if (!flag)
      return;
    extensionsTable.AcceptChanges();
  }

  public void SetMDValues(string valueName, int categoryType, int[] valuesList)
  {
    string[] valuesList1 = new string[valuesList.Length];
    for (int index = 0; index < valuesList1.Length; ++index)
      valuesList1[index] = valuesList[index].ToString();
    this.SetMDValues(valueName, categoryType, valuesList1);
  }

  public void SetMDValues(string valueName, int categoryType, long[] valuesList)
  {
    string[] valuesList1 = new string[valuesList.Length];
    for (int index = 0; index < valuesList1.Length; ++index)
      valuesList1[index] = valuesList[index].ToString();
    this.SetMDValues(valueName, categoryType, valuesList1);
  }

  public void SetMDValues(string valueName, int categoryType, Guid[] valuesList)
  {
    string[] valuesList1 = new string[valuesList.Length];
    for (int index = 0; index < valuesList1.Length; ++index)
      valuesList1[index] = valuesList[index].ToString();
    this.SetMDValues(valueName, categoryType, valuesList1);
  }

  public void SetMDValues(string valueName, string[] valuesList)
  {
    this.SetMDValues(valueName, 0, valuesList);
  }

  public void SetMDValue(string valueName, int categoryType, string value)
  {
    this.SetMDValues(valueName, categoryType, new string[1]
    {
      value
    });
  }

  public void SetMDValue(string valueName, string value) => this.SetMDValue(valueName, 0, value);

  public string[] GetMDValues(string valueName)
  {
    DataRow[] dataRowArray = this.GetExtensionsTable().Select($"F_ATTRIBUTE_ID = {this._AttributeTypeID} AND F_OBJECT_TYPE = {this._ObjectTypeID} AND F_RELATION_TYPE = {this._RelationTypeID} AND F_PARAM_NAME = {SqlHelper.QString(valueName)}", "F_INLIST_ID ASC");
    string[] mdValues = new string[dataRowArray.Length];
    for (int index = 0; index < mdValues.Length; ++index)
      mdValues[index] = Convert.ToString(dataRowArray[index]["F_VALUE"]);
    return mdValues;
  }

  public int[] GetMDValuesInt(string valueName)
  {
    string[] mdValues = this.GetMDValues(valueName);
    int[] mdValuesInt = new int[mdValues.Length];
    for (int index = 0; index < mdValuesInt.Length; ++index)
      mdValuesInt[index] = Convert.ToInt32(mdValues[index]);
    return mdValuesInt;
  }

  public Guid[] GetMDValuesGuid(string valueName)
  {
    string[] mdValues = this.GetMDValues(valueName);
    Guid[] mdValuesGuid = new Guid[mdValues.Length];
    for (int index = 0; index < mdValuesGuid.Length; ++index)
      mdValuesGuid[index] = new Guid(mdValues[index]);
    return mdValuesGuid;
  }

  public long[] GetMDValuesInt64(string valueName)
  {
    string[] mdValues = this.GetMDValues(valueName);
    long[] mdValuesInt64 = new long[mdValues.Length];
    for (int index = 0; index < mdValuesInt64.Length; ++index)
      mdValuesInt64[index] = Convert.ToInt64(mdValues[index]);
    return mdValuesInt64;
  }

  public string GetMDValue(string valueName)
  {
    DataRow[] dataRowArray = this.GetExtensionsTable().Select($"F_ATTRIBUTE_ID = {this._AttributeTypeID} AND F_OBJECT_TYPE = {this._ObjectTypeID} AND F_RELATION_TYPE = {this._RelationTypeID} AND F_PARAM_NAME = {SqlHelper.QString(valueName)}", "F_INLIST_ID ASC");
    return dataRowArray.Length != 0 ? Convert.ToString(dataRowArray[0]["F_VALUE"]) : string.Empty;
  }
}
