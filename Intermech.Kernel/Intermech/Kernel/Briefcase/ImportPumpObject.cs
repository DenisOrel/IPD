// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportPumpObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportPumpObject : ImportObject
{
  private readonly Dictionary<Int96, long> _versions;

  public ImportPumpObject(
    UserSession Session,
    ImportingObject briefObject,
    bool createLinksArray,
    Dictionary<Int96, long> versions,
    bool packetMode)
    : base(Session, briefObject, createLinksArray, packetMode)
  {
    this._versions = versions;
    this.hintAppendEnable = true;
    this.uniIdentifiler = $"Объект тип={briefObject.Object.ObjectType}, Guid={{{briefObject.Object.ObjectGuid}}}, Заголовок=\"{briefObject.Object.Caption}\"";
  }

  public override object Import()
  {
    long newObjectID = 0;
    long newID = 0;
    bool flag1 = false;
    if (this.briefObject.Object.VersionId > 0 && !Helper.IsVersionabe((IUserSession) this.session, this.briefObject.Object.ObjectType))
      return (object) new ImportedObjectInfo(new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_954"), (object) this.briefObject.Object.ObjectType)));
    if (this.briefObject.Object.Object_id > 0L)
    {
      newObjectID = this.briefObject.Object.Object_id;
      newID = this.briefObject.Object.Id;
      flag1 = true;
    }
    else
    {
      long id;
      if (this.briefObject.Object.Id > 0L)
      {
        id = this.briefObject.Object.Id;
      }
      else
      {
        id = Helper.GetID(this.session, (Guid) this.briefObject.Object.IdGuid);
        this.briefObject.Object.Id = id;
      }
      this.ExecAddObject4Import(ref newObjectID, ref newID, id);
      if (newObjectID == -1L)
        return (object) null;
    }
    Dictionary<string, IDBAttributeType> dictionary1 = new Dictionary<string, IDBAttributeType>(this.briefObject.Attributes.Count);
    Dictionary<int, IDBAttributeType> dictionary2 = new Dictionary<int, IDBAttributeType>(this.briefObject.Attributes.Count);
    Dictionary<string, AttributeRecord> dictionary3 = new Dictionary<string, AttributeRecord>(this.briefObject.Attributes.Count);
    for (int index = 0; index < this.briefObject.Attributes.Count; ++index)
    {
      AttributeRecord attribute = this.briefObject.Attributes[index];
      if (!dictionary2.ContainsKey(attribute.AttributeId))
      {
        IDBAttributeType attributeType = this.session.GetAttributeType(attribute.AttributeId);
        if (attributeType != null)
        {
          dictionary1.Add(attributeType.Name.ToUpper(), attributeType);
          dictionary2.Add(attributeType.AttributeID, attributeType);
          dictionary3.Add(attributeType.Name, attribute);
        }
        else
          this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_955"), (object) attribute.AttributeId, (object) newObjectID));
      }
    }
    ArrayList arrayList = new ArrayList();
    List<AttributeRecord> attributeRecordList = new List<AttributeRecord>();
    MetaDataHelper.GetAttributeTypeID("cad014ff-306c-11d8-b4e9-00304f19f545");
    for (int index1 = 0; index1 < this.briefObject.Attributes.Count; ++index1)
    {
      AttributeRecord attribute = this.briefObject.Attributes[index1];
      try
      {
        IDBAttributeType baseAttr = dictionary2[attribute.AttributeId];
        if (baseAttr.AttributeID < 0)
        {
          attributeRecordList.Add(attribute);
        }
        else
        {
          if (baseAttr.AttributeType == FieldTypes.ftBlob || baseAttr.AttributeType == FieldTypes.ftFile || baseAttr.AttributeType == FieldTypes.ftMemo || baseAttr.AttributeType == FieldTypes.ftShortBlob)
          {
            attribute.IntegerValue = (object) 0L;
            Intermech.Kernel.Briefcase.ImportBlob importBlob = new Intermech.Kernel.Briefcase.ImportBlob(this.session, this.hintAppendEnable);
            attribute.IntegerValue = (object) importBlob.Import(newObjectID, attribute, baseAttr.AttributeType, true);
            if (baseAttr.AttributeID == this.session.IdentHelper.FileAttributeID)
              DBHelper.AddBatchSQL((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_FILENAMES (F_FILENAME, F_KEY, F_ID) VALUES (:fname, :objID, :id1)", new DbCommandParam[3]
              {
                this.session.DataManager.BatchParameter("fname", DbType.String, (object) attribute.StringValue.ToString().Trim().ToUpper()),
                this.session.DataManager.BatchParameter("objID", DbType.Int64, (object) newObjectID),
                this.session.DataManager.BatchParameter("id1", DbType.Int64, (object) newID)
              });
          }
          else if ((baseAttr.AttributeType == FieldTypes.ftObjectLink || baseAttr.AttributeType == FieldTypes.ftObjectLinkByID) && this.createLinksArray)
            this.ObjectLinks.Add((object) new Intermech.Kernel.Briefcase.ObjectLinks(newObjectID, baseAttr.AttributeID, attribute.InlistId, Convert.ToInt64(attribute.IntegerValue), Convert.ToString(attribute.StringValue), this.briefObject.Object.ObjectType, baseAttr.AttributeType == FieldTypes.ftObjectLinkByID));
          if ((baseAttr.Computed == ComputeValueModes.StoredValue || baseAttr.Computed == ComputeValueModes.IndexValue) && attribute.InlistId == 0)
          {
            bool flag2 = true;
            try
            {
              switch (baseAttr.TextFieldName)
              {
                case "F_INTEGER_VALUE":
                  if (attribute.IntegerValue != null)
                  {
                    flag2 = false;
                    break;
                  }
                  break;
                case "F_STRING_VALUE":
                  if (attribute.StringValue != null)
                  {
                    if (Convert.ToString(attribute.StringValue) != string.Empty)
                    {
                      flag2 = false;
                      break;
                    }
                    break;
                  }
                  break;
                case "F_DOUBLE_VALUE":
                  if (attribute.DoubleValue != null)
                  {
                    flag2 = false;
                    break;
                  }
                  break;
                case "F_DATE_VALUE":
                  if (attribute.DateValue != null)
                  {
                    flag2 = false;
                    break;
                  }
                  break;
              }
            }
            catch (Exception ex)
            {
              this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_956"), (object) baseAttr.AttributeID, (object) newObjectID, (object) ex.Message));
            }
            if (flag2)
            {
              object obj = (object) string.Empty;
              string text = baseAttr.Formula.Trim();
              if (text != string.Empty)
              {
                ExpressionTree expressionTree;
                using (Parser parser = new Parser())
                {
                  parser.AutoDetectVariables = true;
                  parser.Validate = false;
                  expressionTree = parser.Parse(text);
                }
                if (expressionTree != null)
                {
                  ExpressionVariablesCollection variables = expressionTree.Variables;
                  object[] values = new object[variables.Count];
                  for (int index2 = 0; index2 < variables.Count; ++index2)
                  {
                    AttributeRecord attributeRecord;
                    if (variables[index2].Name.ToUpper() == baseAttr.Name.ToUpper())
                      attributeRecord = attribute;
                    else
                      dictionary3.TryGetValue(variables[index2].Name, out attributeRecord);
                    if (attributeRecord != null)
                    {
                      if (baseAttr.AttributeType == FieldTypes.ftString)
                      {
                        values[index2] = attributeRecord.StringValue;
                      }
                      else
                      {
                        IDBAttributeType dbAttributeType;
                        if (dictionary2.TryGetValue(attributeRecord.AttributeId, out dbAttributeType))
                        {
                          switch (dbAttributeType.TextFieldName)
                          {
                            case "F_INTEGER_VALUE":
                              values[index2] = attributeRecord.IntegerValue;
                              continue;
                            case "F_STRING_VALUE":
                              values[index2] = attributeRecord.StringValue;
                              continue;
                            case "F_DOUBLE_VALUE":
                              values[index2] = attributeRecord.DoubleValue;
                              continue;
                            case "F_DATE_VALUE":
                              values[index2] = attributeRecord.DateValue;
                              continue;
                            default:
                              values[index2] = (object) DBNull.Value;
                              continue;
                          }
                        }
                        else
                          values[index2] = (object) DBNull.Value;
                      }
                    }
                    else
                      values[index2] = (object) DBNull.Value;
                  }
                  try
                  {
                    object indexedString = expressionTree.Evaluate(values);
                    if (baseAttr.Computed == ComputeValueModes.IndexValue && indexedString is string)
                      indexedString = (object) this.session.StringNormalizer.GetIndexedString(indexedString.ToString());
                    obj = indexedString;
                  }
                  catch (Exception ex)
                  {
                    obj = (object) DBNull.Value;
                    this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_957"), (object) baseAttr.AttributeID, (object) newObjectID, (object) ex.Message));
                  }
                }
              }
              try
              {
                switch (baseAttr.TextFieldName)
                {
                  case "F_INTEGER_VALUE":
                    attribute.IntegerValue = (object) Convert.ToInt64(obj);
                    break;
                  case "F_STRING_VALUE":
                    attribute.StringValue = (object) Convert.ToString(obj);
                    break;
                  case "F_DOUBLE_VALUE":
                    attribute.DoubleValue = (object) Convert.ToDouble(obj, (IFormatProvider) CultureInfo.InvariantCulture);
                    break;
                  case "F_DATE_VALUE":
                    attribute.DateValue = (object) Convert.ToDateTime(obj, (IFormatProvider) CultureInfo.InvariantCulture);
                    break;
                }
              }
              catch (Exception ex)
              {
                this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_956"), (object) baseAttr.AttributeID, (object) newObjectID, (object) ex.Message));
              }
            }
          }
          if (baseAttr.AttributeType == FieldTypes.ftString)
          {
            string str = Convert.ToString(attribute.StringValue);
            if (str != null && str.Length > 0 && (long) str.Length > baseAttr.SizeType)
            {
              attribute.StringValue = (object) str.Remove(Convert.ToInt32(baseAttr.SizeType));
              this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_958"), (object) baseAttr.AttributeID, (object) str.Length, (object) newObjectID, (object) baseAttr.SizeType));
            }
          }
          if (attribute.IsNew)
            this.InsertAttribute(baseAttr, attribute, newObjectID);
          else
            this.UpdateAttribute(baseAttr, attribute, newObjectID);
          if (attribute.InlistId == 0)
          {
            string[] updateTables = this.session.DBCache.GetUpdateTables(baseAttr.AttributeID, this.briefObject.Object.ObjectType, -1);
            if (updateTables != null && updateTables.Length != 0)
              arrayList.Add((object) this.AddViewFieldsToSQL(updateTables, attribute));
          }
          this.AddToGlobalIndex(attribute, baseAttr, this.briefObject.Object.ObjectType, newObjectID, newID);
        }
      }
      catch (Exception ex)
      {
        this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_292"), (object) attribute.AttributeId, (object) newObjectID, (object) ex.Message));
      }
    }
    string[] updateTables1 = this.session.DBCache.GetUpdateTables(-1, this.briefObject.Object.ObjectType, -1);
    if (updateTables1 != null)
    {
      if (!flag1)
      {
        string format = "INSERT INTO {0} (F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_PROJECT_ID, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, F_GUID, CAPTION, F_BASE_VERSION, F_ACCESS, F_OBJ_CREATE{1}) VALUES (:v_objID, :v_id, :v_lcStep, :v_version_id, :v_checkoutBy, :v_projectId, :v_verType, :v_objtype_id, :v_owner, :v_level, :v_guidPar, :v_caption, :v_baseVersion, :v_access, :v_objCreate{2})";
        DbCommandParam[] collection = new DbCommandParam[15]
        {
          this.session.DataManager.BatchParameter("v_objID", DbType.Int64, (object) newObjectID),
          this.session.DataManager.BatchParameter("v_id", DbType.Int64, (object) newID),
          this.session.DataManager.BatchParameter("v_lcStep", DbType.Int32, (object) this.briefObject.Object.Lc_step),
          this.session.DataManager.BatchParameter("v_version_id", DbType.Int32, (object) this.briefObject.Object.VersionId),
          this.session.DataManager.BatchParameter("v_checkoutBy", DbType.Int32, (object) 0),
          this.session.DataManager.BatchParameter("v_projectId", DbType.Int32, (object) 0),
          this.session.DataManager.BatchParameter("v_verType", DbType.Int32, (object) 0),
          this.session.DataManager.BatchParameter("v_objtype_id", DbType.Int32, (object) this.briefObject.Object.ObjectType),
          this.session.DataManager.BatchParameter("v_owner", DbType.Int64, (object) this.briefObject.Object.OwnerId),
          this.session.DataManager.BatchParameter("v_level", DbType.Int32, (object) this.briefObject.Object.LevelId),
          this.session.DataManager.BatchParameter("v_guidPar", DbType.Guid, (object) (Guid) this.briefObject.Object.ObjectGuid),
          this.session.DataManager.BatchParameter("v_caption", DbType.String, (object) this.briefObject.Object.Caption),
          this.session.DataManager.BatchParameter("v_objCreate", DbType.Date, (object) this.briefObject.Object.ObjCreate),
          this.session.DataManager.BatchParameter("v_baseVersion", DbType.Int32, (object) (this.briefObject.Object.IsBaseVersion ? 1 : 0)),
          this.session.DataManager.BatchParameter("v_access", DbType.Int32, (object) this.briefObject.Object.AccessLevel)
        };
        foreach (string str in updateTables1)
        {
          try
          {
            List<DbCommandParam> dbCommandParamList = new List<DbCommandParam>((IEnumerable<DbCommandParam>) collection);
            string empty = string.Empty;
            string commandText;
            if (str.ToUpper() == "IMS_OBJECTS_VIEW")
            {
              commandText = string.Format(format, (object) str, (object) string.Empty, (object) string.Empty);
            }
            else
            {
              StringBuilder stringBuilder1 = new StringBuilder();
              StringBuilder stringBuilder2 = new StringBuilder();
              foreach (Importer.UpdatingAttribute updatingAttribute in arrayList)
              {
                if (updatingAttribute.Tables.Contains(str))
                {
                  foreach (Tuple<string, DbType, object> fieldsAndValue in updatingAttribute.FieldsAndValues)
                  {
                    stringBuilder1.Append(',');
                    stringBuilder1.Append(fieldsAndValue.Item1);
                    stringBuilder2.Append(", :");
                    stringBuilder2.Append(fieldsAndValue.Item1);
                    dbCommandParamList.Add(this.session.DataManager.BatchParameter(fieldsAndValue.Item1, fieldsAndValue.Item2, fieldsAndValue.Item3));
                  }
                }
              }
              commandText = string.Format(format, (object) str, (object) stringBuilder1, (object) stringBuilder2);
            }
            DBHelper.AddBatchSQL((IUserSession) this.session, this.hintAppendEnable, commandText, dbCommandParamList.ToArray());
          }
          catch (Exception ex)
          {
            this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_959"), (object) str, (object) newObjectID, (object) ex.Message));
          }
        }
      }
      else
      {
        string format = "UPDATE {0} SET {1} WHERE F_OBJECT_ID = :v_objID";
        if (attributeRecordList.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          List<DbCommandParam> dbCommandParamList = new List<DbCommandParam>();
          foreach (AttributeRecord attributeRecord in attributeRecordList)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(", ");
            switch (attributeRecord.AttributeId)
            {
              case -15:
                stringBuilder.Append("F_MODIFICATION_ID=:v_modif");
                dbCommandParamList.Add(this.session.DataManager.BatchParameter("v_modif", DbType.Int64, attributeRecord.IntegerValue));
                continue;
              case -5:
                stringBuilder.Append("F_VERSION_ID=:v_version_id");
                dbCommandParamList.Add(this.session.DataManager.BatchParameter("v_version_id", DbType.Int32, (object) Convert.ToInt32(attributeRecord.IntegerValue)));
                continue;
              default:
                continue;
            }
          }
          dbCommandParamList.Add(this.session.DataManager.BatchParameter("v_objID", DbType.Int64, (object) newObjectID));
          this.session.DataManager.AddBatchSQL(string.Format(format, (object) "IMS_OBJECTS", (object) stringBuilder), dbCommandParamList.ToArray());
          foreach (string str in updateTables1)
            this.session.DataManager.AddBatchSQL(string.Format(format, (object) str, (object) stringBuilder), dbCommandParamList.ToArray());
        }
        if (arrayList.Count > 0)
        {
          foreach (string str in updateTables1)
          {
            try
            {
              List<DbCommandParam> dbCommandParamList = new List<DbCommandParam>();
              string empty = string.Empty;
              if (str.ToUpper() != "IMS_OBJECTS_VIEW")
              {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (Importer.UpdatingAttribute updatingAttribute in arrayList)
                {
                  if (updatingAttribute.Tables.Contains(str))
                  {
                    foreach (Tuple<string, DbType, object> fieldsAndValue in updatingAttribute.FieldsAndValues)
                    {
                      stringBuilder.Append(", ");
                      stringBuilder.Append(fieldsAndValue.Item1);
                      stringBuilder.Append(" = :");
                      stringBuilder.Append(fieldsAndValue.Item1);
                      dbCommandParamList.Add(this.session.DataManager.BatchParameter(fieldsAndValue.Item1, fieldsAndValue.Item2, fieldsAndValue.Item3));
                    }
                  }
                }
                if (stringBuilder.Length > 0)
                {
                  stringBuilder.Remove(0, 1);
                  string commandText = string.Format(format, (object) str, (object) stringBuilder);
                  dbCommandParamList.Add(this.session.DataManager.BatchParameter("v_objID", DbType.Int64, (object) newObjectID));
                  this.session.DataManager.AddBatchSQL(commandText, dbCommandParamList.ToArray());
                }
              }
            }
            catch (Exception ex)
            {
              this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_960"), (object) str, (object) newObjectID, (object) ex.Message));
            }
          }
        }
      }
    }
    if (this.briefObject.Object.ParentVersionId != -1L && this.briefObject.Object.ParentVersionId != 0L || this.briefObject.Object.ParentVersionNo >= 0)
    {
      long dataValue = this.briefObject.Object.ParentVersionId;
      switch (dataValue)
      {
        case -1:
        case 0:
          if (this._versions != null && !this._versions.TryGetValue(new Int96(newID, (long) this.briefObject.Object.ParentVersionNo), out dataValue))
          {
            dataValue = -1L;
            break;
          }
          break;
      }
      try
      {
        if (dataValue != -1L)
        {
          if (dataValue != 0L)
          {
            this.session.DataManager.Parameter("projID", (object) dataValue);
            this.session.DataManager.Parameter("partID", (object) newObjectID);
            DBHelper.AddBatchSQL((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_VERSIONS_TREE (F_PARENT_ID, F_OBJECT_ID) VALUES (:projID, :partID)", new DbCommandParam[2]
            {
              this.session.DataManager.BatchParameter("projID", DbType.Int64, (object) dataValue),
              this.session.DataManager.BatchParameter("partID", DbType.Int64, (object) newObjectID)
            });
          }
        }
      }
      catch (Exception ex)
      {
        this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_961"), (object) newObjectID, (object) dataValue, (object) ex.Message));
      }
    }
    return (object) new ImportedObjectInfo(newObjectID, newID);
  }

  private void ExecAddObject4Import(ref long newObjectID, ref long newID, long id)
  {
    DateTime dateTime = new DateTime(1980, 1, 1, 0, 0, 0, 0);
    IDbDataParameter dbDataParameter1 = this.briefObject.Object.ModifyDate.Equals(DateTime.MinValue) || this.briefObject.Object.ModifyDate.Equals(dateTime) ? this.session.DataManager.Parameter("inMODIFY_DATE", (object) DBNull.Value) : this.session.DataManager.Parameter("inMODIFY_DATE", (object) this.briefObject.Object.ModifyDate);
    IDbDataParameter dbDataParameter2 = this.briefObject.Object.ObjCreate.Equals(DateTime.MinValue) || this.briefObject.Object.ObjCreate.Equals(dateTime) ? this.session.DataManager.Parameter("inCREATE_DATE", (object) DateTime.UtcNow) : this.session.DataManager.Parameter("inCREATE_DATE", (object) this.briefObject.Object.ObjCreate);
    this.session.DataManager.ExecuteSpNonQuery("IMS_IMPORT_OBJECT", this.session.DataManager.Parameter("inID", (object) id), this.session.DataManager.Parameter("inOBJECT_TYPE", (object) this.briefObject.Object.ObjectType), this.session.DataManager.Parameter("inOWNER_ID", (object) this.briefObject.Object.OwnerId), this.session.DataManager.Parameter("inLC_STEP", (object) this.briefObject.Object.Lc_step), this.session.DataManager.Parameter("inGUID", (object) (Guid) this.briefObject.Object.ObjectGuid), this.session.DataManager.Parameter("inCAPTION", (object) this.briefObject.Object.Caption), dbDataParameter1, dbDataParameter2, this.session.DataManager.Parameter("inLEVEL_ID", (object) Helper.GetLevelForStep((IUserSession) this.session, this.briefObject.Object.Lc_step)), this.session.DataManager.Parameter("inVERSION_ID", (object) this.briefObject.Object.VersionId), this.session.DataManager.Parameter("inBASE_VERSION", (object) (this.briefObject.Object.IsBaseVersion ? 1 : 0)), this.session.DataManager.Parameter("inMODIFICATION_ID", (object) this.briefObject.Object.ModificationID), this.session.DataManager.Parameter("inCREATOR_ID", (object) 0), this.session.DataManager.OutputParameter("outOBJECT_ID", (object) newObjectID), this.session.DataManager.OutputParameter("outID", (object) newID));
    newObjectID = Convert.ToInt64(this.session.DataManager.GetOutputParameterValue("outOBJECT_ID"));
    newID = Convert.ToInt64(this.session.DataManager.GetOutputParameterValue("outID"));
    if (this.briefObject.Object.AccessLevel > 0)
    {
      string commandText = "UPDATE IMS_OBJECTS SET F_ACCESS = :v_access WHERE F_OBJECT_ID= :v_object_id";
      if (this.packetMode)
        DBHelper.AddBatchSQL((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, commandText, new DbCommandParam[2]
        {
          this.session.DataManager.BatchParameter("v_access", DbType.Int32, (object) this.briefObject.Object.AccessLevel),
          this.session.DataManager.BatchParameter("v_object_id", DbType.Int64, (object) newObjectID)
        });
      else
        DBHelper.ExecuteNonQuery((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, commandText, this.session.DataManager.Parameter("v_access", (object) this.briefObject.Object.AccessLevel), this.session.DataManager.Parameter("v_object_id", (object) newObjectID));
    }
    if (id != 0L)
      return;
    string commandText1 = "INSERT INTO IMS_GUID_RESOLVE (F_GUID, F_ID, F_CATEGORY_TYPE) VALUES (:v_guid, :v_id, :v_type)";
    if (this.packetMode)
      DBHelper.AddBatchSQL((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, commandText1, new DbCommandParam[3]
      {
        this.session.DataManager.BatchParameter("v_guid", DbType.Guid, (object) (Guid) this.briefObject.Object.IdGuid),
        this.session.DataManager.BatchParameter("v_id", DbType.Int64, (object) newID),
        this.session.DataManager.BatchParameter("v_type", DbType.Int32, (object) 2)
      });
    else
      DBHelper.ExecuteNonQuery((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, commandText1, this.session.DataManager.Parameter("v_guid", (object) (Guid) this.briefObject.Object.IdGuid), this.session.DataManager.Parameter("v_id", (object) newID), this.session.DataManager.Parameter("v_type", (object) 2));
    Helper.AddID(this.session, (Guid) this.briefObject.Object.IdGuid, newID);
  }
}
