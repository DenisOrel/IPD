// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.Importer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel.Briefcase;

public class Importer
{
  protected UserSession session;
  protected DataSet metadata;
  protected string uniIdentifiler = string.Empty;
  public Exception ErrorException;
  public ArrayList Log;
  public ArrayList ObjectLinks;
  protected Hashtable updatedAttributes;
  protected Hashtable temporaryAttributes;
  protected List<int> calculatedAttributes;
  protected string attributeTable = string.Empty;
  protected string keyField = string.Empty;
  protected bool packetMode;
  protected bool withAttributesCustomHandlers;
  protected bool hintAppendEnable;

  public Importer(UserSession session, string attributeTable, string keyField)
    : this(session, attributeTable, keyField, false)
  {
  }

  public Importer(UserSession session, string attributeTable, string keyField, bool packetMode)
  {
    this.session = session;
    this.attributeTable = attributeTable;
    this.keyField = keyField;
    this.ObjectLinks = new ArrayList();
    this.updatedAttributes = new Hashtable();
    this.temporaryAttributes = new Hashtable();
    this.calculatedAttributes = new List<int>();
    this.packetMode = packetMode;
    this.Log = new ArrayList();
  }

  protected void UpdateAttribute(
    IDBAttributeType baseAttr,
    AttributeRecord attributeRecord,
    long keyID)
  {
    this.CheckValue(baseAttr, attributeRecord);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("UPDATE {0} SET ", (object) this.attributeTable);
    stringBuilder.Append("F_INTEGER_VALUE= :v_int,");
    stringBuilder.Append("F_STRING_VALUE= :v_string,");
    stringBuilder.Append("F_DOUBLE_VALUE= :v_double,");
    stringBuilder.Append("F_DATE_VALUE= :v_date");
    stringBuilder.AppendFormat(" WHERE F_ATTRIBUTE_ID = :v_attrID AND {0} = :v_keyID AND F_INLIST_ID = :v_inlistId", (object) this.keyField);
    if (this.packetMode)
      this.session.DataManager.AddBatchSQL(stringBuilder.ToString(), new DbCommandParam[7]
      {
        this.session.DataManager.BatchParameter("v_int", DbType.Int64, attributeRecord.IntegerValue),
        this.session.DataManager.BatchParameter("v_string", DbType.String, attributeRecord.StringValue),
        this.session.DataManager.BatchParameter("v_double", DbType.Double, attributeRecord.DoubleValue),
        this.session.DataManager.BatchParameter("v_date", DbType.DateTime, attributeRecord.DateValue),
        this.session.DataManager.BatchParameter("v_attrID", DbType.Int32, (object) baseAttr.AttributeID),
        this.session.DataManager.BatchParameter("v_keyID", DbType.Int64, (object) keyID),
        this.session.DataManager.BatchParameter("v_inlistId", DbType.Int32, (object) attributeRecord.InlistId)
      });
    else
      this.session.DataManager.ExecuteNonQuery(stringBuilder.ToString(), this.session.DataManager.Parameter("v_int", attributeRecord.IntegerValue), this.session.DataManager.Parameter("v_string", attributeRecord.StringValue), this.session.DataManager.Parameter("v_double", attributeRecord.DoubleValue), this.session.DataManager.Parameter("v_date", attributeRecord.DateValue), this.session.DataManager.Parameter("v_attrID", (object) baseAttr.AttributeID), this.session.DataManager.Parameter("v_keyID", (object) keyID), this.session.DataManager.Parameter("v_inlistId", (object) attributeRecord.InlistId));
    this.AttributeAdditionalActions(false, baseAttr.AttributeType, baseAttr.AttributeID, attributeRecord, keyID);
  }

  protected void InsertAttribute(
    IDBAttributeType baseAttr,
    AttributeRecord attributeRecord,
    long keyID)
  {
    this.CheckValue(baseAttr, attributeRecord);
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.AppendFormat("INSERT INTO {0} (F_ATTRIBUTE_ID, {1}, F_INLIST_ID", (object) this.attributeTable, (object) this.keyField);
    ArrayList arrayList = new ArrayList();
    StringBuilder stringBuilder2 = new StringBuilder();
    stringBuilder2.Append(" VALUES (:v_attributeID, :v_keyID, :v_inlistID");
    if (this.packetMode)
    {
      arrayList.Add((object) this.session.DataManager.BatchParameter("v_attributeID", DbType.Int32, (object) baseAttr.AttributeID));
      arrayList.Add((object) this.session.DataManager.BatchParameter("v_keyID", DbType.Int32, (object) keyID));
      arrayList.Add((object) this.session.DataManager.BatchParameter("v_inlistID", DbType.Int32, (object) attributeRecord.InlistId));
    }
    else
    {
      arrayList.Add((object) this.session.DataManager.Parameter("v_attributeID", (object) baseAttr.AttributeID));
      arrayList.Add((object) this.session.DataManager.Parameter("v_keyID", (object) keyID));
      arrayList.Add((object) this.session.DataManager.Parameter("v_inlistID", (object) attributeRecord.InlistId));
    }
    StringBuilder stringBuilder3 = new StringBuilder();
    StringBuilder stringBuilder4 = new StringBuilder();
    if (attributeRecord.IntegerValue != null && attributeRecord.IntegerValue.ToString() != string.Empty && Convert.ToInt64(attributeRecord.IntegerValue) != long.MinValue)
    {
      stringBuilder3.Append(", F_INTEGER_VALUE");
      stringBuilder4.Append(", :v_int");
      if (this.packetMode)
        arrayList.Add((object) this.session.DataManager.BatchParameter("v_int", DbType.Int64, attributeRecord.IntegerValue));
      else
        arrayList.Add((object) this.session.DataManager.Parameter("v_int", attributeRecord.IntegerValue));
    }
    if (attributeRecord.StringValue != null && attributeRecord.StringValue.ToString() != string.Empty)
    {
      stringBuilder3.Append(", F_STRING_VALUE");
      stringBuilder4.Append(", :v_string");
      if (this.packetMode)
        arrayList.Add((object) this.session.DataManager.BatchParameter("v_string", DbType.String, attributeRecord.StringValue));
      else
        arrayList.Add((object) this.session.DataManager.Parameter("v_string", attributeRecord.StringValue));
    }
    if (attributeRecord.DoubleValue != null && attributeRecord.DoubleValue.ToString() != string.Empty && Convert.ToDouble(attributeRecord.DoubleValue) != double.MinValue)
    {
      stringBuilder3.Append(", F_DOUBLE_VALUE");
      stringBuilder4.Append(", :v_double");
      if (this.packetMode)
        arrayList.Add((object) this.session.DataManager.BatchParameter("v_double", DbType.Double, attributeRecord.DoubleValue));
      else
        arrayList.Add((object) this.session.DataManager.Parameter("v_double", attributeRecord.DoubleValue));
    }
    if (attributeRecord.DateValue != null && attributeRecord.DateValue.ToString() != string.Empty && Convert.ToDateTime(attributeRecord.DateValue) != DateTime.MinValue)
    {
      stringBuilder3.Append(", F_DATE_VALUE");
      stringBuilder4.Append(", :v_date");
      if (this.packetMode)
        arrayList.Add((object) this.session.DataManager.BatchParameter("v_date", DbType.DateTime, attributeRecord.DateValue));
      else
        arrayList.Add((object) this.session.DataManager.Parameter("v_date", attributeRecord.DateValue));
    }
    stringBuilder1.Append(stringBuilder3.ToString());
    stringBuilder1.Append(')');
    stringBuilder1.Append(stringBuilder2.ToString());
    stringBuilder1.Append(stringBuilder4.ToString());
    stringBuilder1.Append(')');
    try
    {
      if (this.packetMode)
        DBHelper.AddBatchSQL((IUserSession) this.session, this.hintAppendEnable, stringBuilder1.ToString(), (DbCommandParam[]) arrayList.ToArray(typeof (DbCommandParam)));
      else
        DBHelper.ExecuteNonQuery((IUserSession) this.session, this.hintAppendEnable, stringBuilder1.ToString(), (IDbDataParameter[]) arrayList.ToArray(typeof (IDbDataParameter)));
      this.AttributeAdditionalActions(true, baseAttr.AttributeType, baseAttr.AttributeID, attributeRecord, keyID);
    }
    catch
    {
      this.UpdateAttribute(baseAttr, attributeRecord, keyID);
    }
  }

  private void CheckValue(IDBAttributeType baseAttr, AttributeRecord attributeRecord)
  {
    if (baseAttr.AttributeType == FieldTypes.ftString)
    {
      string str1 = Convert.ToString(attributeRecord.StringValue);
      if (baseAttr.SizeType >= (long) str1.Length)
        return;
      string str2 = str1.Substring(0, Convert.ToInt32(baseAttr.SizeType));
      this.AddIntoLog($"Обрезано значение \"{baseAttr.Name}\": {attributeRecord.StringValue}=>{str2}");
      attributeRecord.StringValue = (object) str2;
    }
    else if (baseAttr.AttributeType == FieldTypes.ftInteger)
    {
      if (attributeRecord.IntegerValue == null || attributeRecord.IntegerValue == DBNull.Value || long.TryParse(Convert.ToString(attributeRecord.IntegerValue), out long _))
        return;
      this.AddIntoLog($"Неверный тип данных \"{baseAttr.Name}\": {attributeRecord.IntegerValue}=>0");
      attributeRecord.IntegerValue = (object) DBNull.Value;
    }
    else if (baseAttr.AttributeType == FieldTypes.ftDouble)
    {
      if (attributeRecord.DoubleValue == null || attributeRecord.DoubleValue == DBNull.Value || double.TryParse(Convert.ToString(attributeRecord.DoubleValue), out double _))
        return;
      this.AddIntoLog($"Неверный тип данных \"{baseAttr.Name}\": {attributeRecord.DoubleValue}=>0");
      attributeRecord.DoubleValue = (object) DBNull.Value;
    }
    else
    {
      if (baseAttr.AttributeType != FieldTypes.ftDateTime || attributeRecord.DateValue == null || attributeRecord.DateValue == DBNull.Value || !(Convert.ToString(attributeRecord.DateValue) != Consts.CurrentDateFunction) || DateTime.TryParse(Convert.ToString(attributeRecord.DateValue), out DateTime _))
        return;
      this.AddIntoLog($"Неверный тип данных \"{baseAttr.Name}\": {attributeRecord.DateValue}=>null");
      attributeRecord.DateValue = (object) DBNull.Value;
    }
  }

  protected virtual void AttributeAdditionalActions(
    bool insert,
    FieldTypes fieldType,
    int attributeID,
    AttributeRecord attributeRecord,
    long keyID)
  {
  }

  public static void AppendObligatoryAttributes(
    DataRow[] attributes4Type,
    ImportingAttributable attributable,
    long attributableID,
    Hashtable temporaryAttributes)
  {
    if (attributes4Type == null || attributes4Type.Length == 0 || attributable == null)
      return;
    foreach (DataRow dataRow in attributes4Type)
    {
      switch ((RequiredModes) Convert.ToInt32(dataRow["F_REQUIRED"]))
      {
        case RequiredModes.Auto:
        case RequiredModes.AutoRequired:
          int int32_1 = Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]);
          bool flag = false;
          for (int index = 0; index < attributable.Attributes.Count; ++index)
          {
            if (attributable.Attributes[index].AttributeId == int32_1)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            AttributeRecord attributeRecord = (AttributeRecord) null;
            switch (Convert.ToInt32(dataRow["F_COMPUTED"]))
            {
              case 0:
                object obj = dataRow["F_DEFAULT_VALUE"];
                attributeRecord = new AttributeRecord(int32_1, attributableID, 0, (object) null, (object) null, (object) null, (object) null, (object) null, (object) null);
                if (CompareValuesHelper.NormalizedValue(obj) != null)
                {
                  FieldTypes int32_2 = (FieldTypes) Convert.ToInt32(dataRow["F_ATTRIBUTE_TYPE"]);
                  FieldTypes fieldTypes = int32_2 == FieldTypes.ftSystem ? ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) int32_1) : int32_2;
                  try
                  {
                    switch (fieldTypes)
                    {
                      case FieldTypes.ftString:
                      case FieldTypes.ftGuid:
                        attributeRecord.StringValue = (object) obj.ToString();
                        break;
                      case FieldTypes.ftInteger:
                      case FieldTypes.ftAutoInc:
                        attributeRecord.IntegerValue = (object) Convert.ToInt64(obj);
                        break;
                      case FieldTypes.ftDouble:
                        attributeRecord.DoubleValue = (object) Convert.ToDouble(obj);
                        break;
                      case FieldTypes.ftDateTime:
                        attributeRecord.DateValue = !(obj.ToString() == Consts.CurrentDateFunction) ? (object) DateTimeHelper.ToDateTime(obj.ToString()) : (object) DateTime.Today;
                        break;
                      case FieldTypes.ftShortBlob:
                      case FieldTypes.ftFile:
                      case FieldTypes.ftBlob:
                        attributeRecord = (AttributeRecord) null;
                        break;
                      case FieldTypes.ftObjectLink:
                        attributeRecord = (AttributeRecord) null;
                        break;
                      case FieldTypes.ftBoolean:
                        attributeRecord.IntegerValue = (object) (bool.Parse(obj.ToString()) ? 1 : 0);
                        break;
                      case FieldTypes.ftMeasured:
                        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(obj.ToString());
                        attributeRecord.IntegerValue = (object) measuredValue.MeasureID;
                        attributeRecord.DoubleValue = (object) measuredValue.Value;
                        attributeRecord.StringValue = (object) measuredValue.ToString();
                        break;
                      default:
                        attributeRecord = (AttributeRecord) null;
                        break;
                    }
                  }
                  catch (Exception ex)
                  {
                    if (!(ex is FormatException))
                      throw ex;
                    attributeRecord = (AttributeRecord) null;
                    break;
                  }
                }
                else
                  break;
                break;
              case 1:
              case 3:
                if (temporaryAttributes != null)
                {
                  attributeRecord = new AttributeRecord(int32_1, attributableID, 0, (object) null, (object) null, (object) null, (object) null, (object) null, (object) null);
                  temporaryAttributes.Add((object) int32_1, (object) attributeRecord);
                  break;
                }
                break;
            }
            if (attributeRecord != null)
            {
              attributable.Attributes.Add(attributeRecord);
              break;
            }
            break;
          }
          break;
      }
    }
  }

  protected void ComputeAttributes(
    IDBAttributeCollection attributes,
    IDBAttribute4TypeCollection attrCollection)
  {
    for (int AttrIndex = 0; AttrIndex < attributes.Count; ++AttrIndex)
    {
      IDBAttribute attribute = attributes[AttrIndex];
      IDBAttributeType4 attributeById = attrCollection.GetAttributeByID(attribute.AttributeID);
      if (attributeById != null)
      {
        if (this.updatedAttributes.ContainsKey((object) attribute.AttributeID) && attribute.Value != DBNull.Value && !this.temporaryAttributes.ContainsKey((object) attribute.AttributeID))
        {
          if (attributeById.UniqueMode != UniqueValueModes.NotUnique)
            (attribute as DBAttribute).CheckUniqueValue(attribute.Values, true);
        }
        else if (attributeById.AttributeType == FieldTypes.ftAutoInc)
        {
          if (attribute.Value == DBNull.Value || attribute.Value == null)
            (attribute as DBAttribute).DoAfterCreate();
          this.calculatedAttributes.Add(attribute.AttributeID);
        }
        else if ((attributeById.Computed == ComputeValueModes.StoredValue || attributeById.Computed == ComputeValueModes.IndexValue) && !this.calculatedAttributes.Contains(attribute.AttributeID))
        {
          (attribute as DBAttribute).Compute(false);
          this.calculatedAttributes.Add(attribute.AttributeID);
        }
      }
    }
  }

  protected long FindObjectID(Guid ObjectGuid)
  {
    return ObjectSearchEngine.FindObjectID(this.session, ObjectGuid);
  }

  protected long FindID(Guid IDGuid) => ObjectSearchEngine.FindID(this.session, IDGuid);

  protected void DeleteAttributeValues(
    AttributeRecord attr,
    FieldTypes fieldType,
    int attributeID,
    long attributableID)
  {
    IDbDataParameter dbDataParameter1 = this.session.DataManager.Parameter("objID", (object) attributableID);
    IDbDataParameter dbDataParameter2 = this.session.DataManager.Parameter("attrID", (object) attributeID);
    this.session.DataManager.Parameter("inlistID", (object) attr.InlistId);
    if (fieldType == FieldTypes.ftBlob || fieldType == FieldTypes.ftFile || fieldType == FieldTypes.ftMemo || fieldType == FieldTypes.ftShortBlob)
    {
      DataTable dataTable = this.session.DataManager.ExecuteDataTable($"SELECT * FROM {this.attributeTable} WHERE {this.keyField} = :objID AND F_ATTRIBUTE_ID = :attrID", dbDataParameter1, dbDataParameter2);
      IBlobStoragesPool blobStoragesPool = (IBlobStoragesPool) null;
      IBlobStorage Storage = (IBlobStorage) null;
      if (fieldType == FieldTypes.ftBlob || fieldType == FieldTypes.ftFile)
      {
        blobStoragesPool = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
        Storage = blobStoragesPool.GetStorage(blobStoragesPool.GetActiveStorageID((IUserSession) this.session), (IUserSession) this.session);
      }
      try
      {
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          if (dataTable.Rows[index]["F_INTEGER_VALUE"] != DBNull.Value && dataTable.Rows[index]["F_INTEGER_VALUE"] != null)
          {
            int int32 = Convert.ToInt32(dataTable.Rows[index]["F_INTEGER_VALUE"]);
            switch (fieldType)
            {
              case FieldTypes.ftShortBlob:
                this.session.DataManager.ExecuteNonQuery("DELETE FROM IMS_BLOBS WHERE F_KEY=:id1", this.session.DataManager.Parameter("id1", (object) int32));
                continue;
              case FieldTypes.ftFile:
              case FieldTypes.ftBlob:
                Storage.DeleteFile((long) int32);
                continue;
              case FieldTypes.ftMemo:
                this.session.DataManager.ExecuteNonQuery("DELETE FROM IMS_MEMOS WHERE F_KEY=:id1", this.session.DataManager.Parameter("id1", (object) int32));
                continue;
              default:
                continue;
            }
          }
        }
      }
      finally
      {
        if (fieldType == FieldTypes.ftBlob || fieldType == FieldTypes.ftFile)
          blobStoragesPool.ReleaseStorage(Storage);
      }
      if (attributeID == this.session.IdentHelper.FileAttributeID)
        this.session.DataManager.ExecuteNonQuery("DELETE FROM IMS_FILENAMES WHERE F_KEY = :objID", dbDataParameter1);
    }
    this.session.DataManager.ExecuteDataTable($"DELETE FROM {this.attributeTable} WHERE {this.keyField} = :objID AND F_ATTRIBUTE_ID = :attrID", dbDataParameter1, dbDataParameter2);
    this.OnDeleteAttribute(attr, fieldType, attributeID, attributableID);
  }

  protected virtual void OnDeleteAttribute(
    AttributeRecord attr,
    FieldTypes fieldType,
    int attributeID,
    long attributableID)
  {
  }

  protected virtual bool CheckFileName(
    AttributeRecord attr,
    long id,
    bool refresh,
    bool throwException)
  {
    string upper = Convert.ToString(attr.StringValue).ToUpper();
    if (upper == string.Empty)
      return true;
    DataTable dataTable = this.session.DataManager.ExecuteDataTable("SELECT F_ID, F_KEY FROM IMS_FILENAMES WHERE F_FILENAME = :fname", this.session.DataManager.Parameter("fname", (object) upper));
    if (dataTable.Rows.Count > 0)
    {
      long int64_1 = Convert.ToInt64(dataTable.Rows[0][0]);
      long int64_2 = Convert.ToInt64(dataTable.Rows[0][1]);
      if (int64_1 != id && !refresh)
      {
        if (throwException)
        {
          string str = $"{(int64_2 != int64_1 ? (object) LocalizationHolder.rm.GetString("Kernel_946") : (object) LocalizationHolder.rm.GetString("Kernel_947"))} {int64_2}";
          throw new Exception(string.Format(KernelErrorMessages.GetErrorMessage(324), attr.StringValue, (object) str, (object) ""));
        }
        return false;
      }
    }
    return true;
  }

  protected void AddFileNameIntoTable(
    AttributeRecord attr,
    long attributableID,
    long id,
    bool refresh)
  {
    string upper = Convert.ToString(attr.StringValue).ToUpper();
    if (upper == string.Empty)
      return;
    DBHelper.ExecuteNonQuery((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_FILENAMES (F_FILENAME, F_KEY, F_ID) VALUES (:fname, :objID, :id1)", this.session.DataManager.Parameter("fname", (object) upper), this.session.DataManager.Parameter("objID", (object) attributableID), this.session.DataManager.Parameter("id1", (object) id));
  }

  protected Importer.UpdatingAttribute AddViewFieldsToSQL(string[] tables, AttributeRecord attr)
  {
    IDBAttributeType attributeType = this.session.GetAttributeType(attr.AttributeId);
    if (attributeType.AttributeID < 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (tables != null)
      {
        foreach (string table in tables)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(table);
        }
      }
      throw new Exception($"Попытка использования при вставке в таблицы {stringBuilder.ToString()} значений системного атрибута {attributeType.Name}({attr.AttributeId})");
    }
    Importer.UpdatingAttribute sql = new Importer.UpdatingAttribute(attributeType.AttributeID, tables);
    foreach (string fieldName in attributeType.FieldNames)
    {
      object val = (object) null;
      DbType type = DbType.String;
      if (fieldName == $"F{attributeType.AttributeID}")
      {
        if (attributeType.AttributeType != FieldTypes.ftObjectLink && attributeType.AttributeType != FieldTypes.ftMeasured)
        {
          val = Helper.GetValueFromField(attributeType.ValueFieldName, attr);
          if (attributeType.ValueFieldName == "F_INTEGER_VALUE")
            type = DbType.Int64;
          else if (attributeType.ValueFieldName == "F_STRING_VALUE")
            type = DbType.String;
          else if (attributeType.ValueFieldName == "F_DOUBLE_VALUE")
            type = DbType.Double;
          else if (attributeType.ValueFieldName == "F_DATE_VALUE")
            type = DbType.Date;
        }
        else
        {
          val = attr.StringValue;
          type = DbType.String;
        }
      }
      else if (fieldName == $"F{attributeType.AttributeID}ID")
      {
        val = attr.IntegerValue;
        type = DbType.Int64;
      }
      else if (fieldName == $"F{attributeType.AttributeID}ID2")
      {
        val = attr.DoubleValue;
        type = DbType.Double;
      }
      else if (fieldName == $"F{attributeType.AttributeID}ID3")
      {
        val = attr.DateValue;
        type = DbType.DateTime;
      }
      sql.AddValue(fieldName, type, val);
    }
    return sql;
  }

  protected void AddErrorMessage(Exception ex)
  {
    this.ErrorException = new Exception($"{this.uniIdentifiler}: {ex.Message}", ex);
    this.Log.Add((object) $"{BriefcaseConsts.logErrorString}{this.ErrorException.Message}");
  }

  protected void AddWarningMessage(string message) => this.AddIntoLog(message);

  protected void AddIntoLog(string message)
  {
    this.Log.Add((object) $"{this.uniIdentifiler}: {message}");
  }

  protected IDBAttributeType GetAttributeType4(
    int attributeID,
    IDBAttribute4TypeCollection attributesCollection)
  {
    return (IDBAttributeType) attributesCollection.GetAttributeByID(attributeID) ?? this.session.GetAttributeType(attributeID);
  }

  protected IDBAttributeType GetAttributeType4(int attributeID, int typeID)
  {
    IDBAttribute4TypeCollection attributesCollection = this.GetAttributesCollection(typeID);
    return this.GetAttributeType4(attributeID, attributesCollection);
  }

  protected virtual IDBAttribute4TypeCollection GetAttributesCollection(int typeID)
  {
    throw new MissingMethodException();
  }

  protected class UpdatingAttribute
  {
    public int AttributeID;
    public List<Tuple<string, DbType, object>> FieldsAndValues;
    public List<string> Tables;

    public UpdatingAttribute(int attributeID, string[] tables)
    {
      this.AttributeID = attributeID;
      this.Tables = new List<string>();
      foreach (string table in tables)
        this.Tables.Add(table);
      this.FieldsAndValues = new List<Tuple<string, DbType, object>>();
    }

    public void AddValue(string field, DbType type, object val)
    {
      this.FieldsAndValues.Add(new Tuple<string, DbType, object>(field, type, val));
    }
  }
}
