// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAdditionalAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Dictionary;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.DelayedNotifications;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Helpers;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

public abstract class DBAdditionalAttribute : DBAttribute, IDBAttributeEx
{
  private int _AttributeID;
  protected string _ValuesTableName;
  private bool _IsObjectAttribute;
  protected IDBAttributeType _AttributeType;
  internal HybridTable _ValuesTable = new HybridTable();
  private bool _BatchMode;
  protected bool _CheckRepeatedValues;
  protected bool _AutoSaveHistory = true;
  internal OptimizationInfo optimStat;
  private static System.Collections.Generic.Dictionary<ActionType, bool> attributeActions = new System.Collections.Generic.Dictionary<ActionType, bool>(4);

  static DBAdditionalAttribute()
  {
    DBAdditionalAttribute.attributeActions.Add(ActionType.GetAccess, false);
    DBAdditionalAttribute.attributeActions.Add(ActionType.SetAccess, false);
    DBAdditionalAttribute.attributeActions.Add(ActionType.Delete, true);
    DBAdditionalAttribute.attributeActions.Add(ActionType.Write, true);
  }

  public DBAdditionalAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession)
  {
    this.SetSource(attributeTypeRow, valuesTable, values_index, parent);
    if (AdminUtilsService.OptimizerStatisticsON)
      this.optimStat = new OptimizationInfo(uSession.DataManager);
    this.InitSecurityOptions(3, (long) this._AttributeID);
  }

  public DBAdditionalAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession)
  {
    this._TemporaryAttribute = temporary;
    if (temporary)
      this.ValidatingOn = false;
    else if (AdminUtilsService.OptimizerStatisticsON)
      this.optimStat = new OptimizationInfo(uSession.DataManager);
    this.SetSource(attributeTypeRow, valuesTable, values_index, parent);
    this.InitSecurityOptions(3, (long) this._AttributeID);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBAdditionalAttribute.attributeActions);
  }

  internal void SetOptimizerStat()
  {
    if (this.optimStat == null)
      return;
    OptimizationValue optimizationValue = new OptimizationValue(this.AttributeID, -1, -1, RequestOperations.Write);
    if (this.IsObjectAttribute)
      optimizationValue.ObjectTypeID = this.TypeID;
    else
      optimizationValue.RelationTypeID = this.TypeID;
    this.optimStat.Records.Add((object) optimizationValue);
    this.optimStat.StartOperation();
  }

  public virtual object DefaultValue => this.AttributeType.DefaultValue;

  public override string ObjectName
  {
    get
    {
      return this.IsObjectAttribute ? string.Format(LocalizationHolder.rm.GetString("Kernel_2"), (object) this.Name, (object) this._DBObjectID) : string.Format(LocalizationHolder.rm.GetString("Kernel_3"), (object) this.Name, (object) this._DBRelationID);
    }
  }

  public int SetSource(
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
  {
    this._AttributeID = Convert.ToInt32(valuesTable.Rows[values_index]["F_ATTRIBUTE_ID"]);
    this._ParentObject = parent;
    if ((Convert.ToInt32(attributeTypeRow["F_OPTIONS"]) & 524288 /*0x080000*/) == 524288 /*0x080000*/)
      this.UseAccessCache = false;
    string columnName;
    if (valuesTable.Columns.Contains("F_OBJECT_ID"))
    {
      this._DBObjectID = Convert.ToInt64(valuesTable.Rows[values_index]["F_OBJECT_ID"]);
      this._IsObjectAttribute = true;
      columnName = "F_OBJECT_ID";
      if (valuesTable.Columns.Contains("F_OBJECT_TYPE"))
        this._TypeID = Convert.ToInt32(valuesTable.Rows[0]["F_OBJECT_TYPE"]);
      else
        this._TypeID = (parent as DBObject).ObjectType;
      if (valuesTable.Columns.Contains("F_ID"))
        this._DB_ID = Convert.ToInt64(valuesTable.Rows[values_index]["F_ID"]);
      this._ValuesTableName = !(parent as DBObject).ObjectTypeClass.IsLocalType ? "IMS_OBJECT_ATTRS" : "IMV_A" + this._TypeID.ToString();
    }
    else
    {
      this._ValuesTableName = "IMS_RELATION_ATTRS";
      this._IsObjectAttribute = false;
      columnName = "F_PRJLINK_ID";
      this._DBRelationID = Convert.ToInt64(valuesTable.Rows[values_index]["F_PRJLINK_ID"]);
      if (valuesTable.Columns.Contains("F_RELATION_TYPE"))
        this._TypeID = Convert.ToInt32(valuesTable.Rows[0]["F_RELATION_TYPE"]);
      else
        this._TypeID = (parent as DBRelation).RelationType;
    }
    this._ValuesTable.Create(valuesTable.Columns);
    int index;
    for (index = values_index; index < valuesTable.Rows.Count && this._AttributeID == Convert.ToInt32(valuesTable.Rows[index]["F_ATTRIBUTE_ID"]) && this.DBObjectID == Convert.ToInt64(valuesTable.Rows[index][columnName]); ++index)
    {
      HybridRow hybridRow = new HybridRow(this._ValuesTable.Columns);
      hybridRow.Create(valuesTable.Rows[index], true);
      this._ValuesTable.Rows.Add(hybridRow);
      ++this._ValuesCount;
    }
    return index;
  }

  public override int AttributeID => this._AttributeID;

  public override bool IsObjectAttribute => this._IsObjectAttribute;

  public override int TypeID
  {
    get
    {
      if (this._TypeID == -1)
      {
        if (this._Attributes != null)
          this._TypeID = this._Attributes.ObjectType;
        else if (this._ParentObject != null)
        {
          this._TypeID = (this._ParentObject as IDBAttributable).TypeID;
        }
        else
        {
          object obj;
          if (this.IsObjectAttribute)
            obj = (object) this.UserSession.GetObjectInfo(this._DBObjectID).ObjectTypeID;
          else
            obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_RELATION_TYPE FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :p0", this.UserSession.DataManager.Parameter("p0", (object) this._DBRelationID));
          if (obj != DBNull.Value && obj != null)
            this._TypeID = Convert.ToInt32(obj);
        }
      }
      return this._TypeID;
    }
  }

  public long AddEvent(ActionType EventType, EventlogRecordType AuditType, string Note)
  {
    return this.AddEvent(this._DBObjectID, this._DBRelationID, EventType, AuditType, Note);
  }

  public long AddEvent(ActionType EventType, EventlogRecordType AuditType)
  {
    return !this.TemporaryAttribute && (this.AttributeType.Options & AttributeOptions.SaveInLog) == AttributeOptions.SaveInLog && (this.Attributes.AssignMode & Intermech.Consts.CheckOutMode) == 0 ? this.AddEvent(this._DBObjectID, this._DBRelationID, EventType, AuditType) : 0L;
  }

  public override long AddEvent(
    long objectID,
    long relationID,
    ActionType eventType,
    EventlogRecordType auditType,
    string note)
  {
    return !this.TemporaryAttribute && (this.AttributeType.Options & AttributeOptions.SaveInLog) == AttributeOptions.SaveInLog && (this.Attributes.AssignMode & Intermech.Consts.CheckOutMode) == 0 ? base.AddEvent(objectID, relationID, eventType, auditType, note) : 0L;
  }

  private ComputeValueModes ComputeMode => this.AttributeType.Computed;

  internal override void Compute(bool SilentMode)
  {
    try
    {
      if (this.ComputeMode != ComputeValueModes.StoredValue && this.ComputeMode != ComputeValueModes.IndexValue)
        return;
      this.SetCalculatedValue(this.GetCalculatedValue((DBAttribute) null), true);
    }
    catch (Exception ex)
    {
      if (!SilentMode)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12366.ssp_appserver_12367()), (object) this.Name, (object) ex.Message));
      this.AddEvent(ActionType.Compute, EventlogRecordType.Warning, ex.Message);
    }
  }

  public virtual string ValuesTableName => this._ValuesTableName;

  public string ValuesKeyName => this.IsObjectAttribute ? "F_OBJECT_ID" : "F_PRJLINK_ID";

  public override bool IsNull
  {
    get
    {
      if (this.AttributeType.Computed == ComputeValueModes.JITValue)
      {
        object calculatedValue = this.GetCalculatedValue((DBAttribute) null);
        return calculatedValue == DBNull.Value || calculatedValue == null;
      }
      return this._ValuesTable[this._Index]["F_STRING_VALUE"] == DBNull.Value && this._ValuesTable[this._Index]["F_INTEGER_VALUE"] == DBNull.Value && this._ValuesTable[this._Index]["F_DATE_VALUE"] == DBNull.Value && this._ValuesTable[this._Index]["F_DOUBLE_VALUE"] == DBNull.Value;
    }
  }

  protected virtual void DoClear()
  {
    if (!this._TemporaryAttribute)
    {
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET F_STRING_VALUE = NULL, F_INTEGER_VALUE = NULL, F_DATE_VALUE = NULL, F_DOUBLE_VALUE = NULL WHERE {this.ValuesKeyName} = :p0 AND F_ATTRIBUTE_ID = :p1 AND F_INLIST_ID = :p2", this.UserSession.DataManager.Parameter("p0", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("p1", (object) this.AttributeID), this.UserSession.DataManager.Parameter("p2", (object) this._Index));
      this.UpdateModifyValue();
    }
    this._ValuesTable[this._Index]["F_STRING_VALUE"] = (object) DBNull.Value;
    this._ValuesTable[this._Index]["F_INTEGER_VALUE"] = (object) DBNull.Value;
    this._ValuesTable[this._Index]["F_DOUBLE_VALUE"] = (object) DBNull.Value;
    this._ValuesTable[this._Index]["F_DATE_VALUE"] = (object) DBNull.Value;
  }

  internal virtual void InternalClear()
  {
    this.Check4ObjectLinkAttributes(string.Empty);
    this.DoClear();
    string[] fieldNames = this.AttributeType.FieldNames;
    if (fieldNames != null)
    {
      foreach (string fldName in fieldNames)
        this.UpdateViewValue(fldName, (object) DBNull.Value, this.DBObjectID);
    }
    this._ParentObject.AfterSetAdditionalAttributeValue((IDBAttribute) this);
    this.ChangeComputedValues(true);
  }

  public override void Clear()
  {
    if (!this.ValidateDirectWrite((object) DBNull.Value))
      return;
    if (this.ParentObject.MustCheckValidatingRule)
    {
      if ((this.AttributeType.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls)
      {
        string str = !(this.ParentObject is IDBObject) ? (!(this.ParentObject is DBRelation) ? string.Empty : string.Format(LocalizationHolder.rm.GetString("Kernel_942"), (object) (this.ParentObject as DBRelation).ObjectName)) : string.Format(LocalizationHolder.rm.GetString("Kernel_941"), (object) (this.ParentObject as IDBObject).NameInMessages);
        throw new KernelExceptionID(sc_12366.ssp_appserver_12368(591816100), (object) this.Name, (object) str);
      }
      if (this.AttributeType.ValidationRule != "")
        this.ValidateRule(this.AttributeID, (object) DBNull.Value);
      if (this.Index == 0)
      {
        int[] formulasId = this.UserSession.DBCache.GetFormulasID(this.AttributeID, this.TypeID, Intermech.Consts.Attribute4ValidationRule, this.IsObjectAttribute);
        for (int index = 0; index < formulasId.Length; ++index)
        {
          if (formulasId[index] != this.AttributeID && this.Attributes.FindByID(formulasId[index]) is DBAttribute byId)
            byId.ValidateRule(this.AttributeID, (object) DBNull.Value);
        }
      }
    }
    if (this.IsNull)
      return;
    this.UserSession.StartTransaction();
    try
    {
      this.SetOptimizerStat();
      if (this.IsGenerateWriteEvent())
        (this.EventHelper as EventLogHelper).OnAttributeWriteEvent((IDBAttribute) this, new AttributeValueEventArgs((object) DBNull.Value, this.Value, this._BatchMode, (IUserSession) this.UserSession));
      this.InternalClear();
      if (this.optimStat != null)
        this.optimStat.SaveToCache();
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  private bool IsGenerateWriteEvent()
  {
    if (this.TemporaryAttribute)
      return false;
    return this._Attributes == null || (this._Attributes.AssignMode & Intermech.Consts.CheckOutMode) == 0;
  }

  public override void ClearValues()
  {
    this.UserSession.StartTransaction();
    try
    {
      this.Index = this.ValuesCount - 1;
      while (this.Index > 0)
        this.DeleteValue();
      this.Clear();
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  protected virtual bool ValidateDirectWrite(object newvalue)
  {
    if (this.ValidatingOn)
    {
      if (!this.TemporaryAttribute)
      {
        try
        {
          bool flag = this.AttributeType.Computed == ComputeValueModes.NotComputableValue;
          if (!flag)
            throw new KernelExceptionID(sc_12366.ssp_appserver_12369(382380736), (object) this.Name);
          if (this.ParentObject.MustCheckValidatingRule)
          {
            if (this.AttributeID == this.UserSession.IdentHelper.LiteraID)
            {
              flag = !this.IsReadOnlyLitera;
              if (!flag)
                throw new KernelExceptionID(sc_12366.ssp_appserver_12370(538105562));
            }
            else
            {
              flag = flag && this.CheckAccess(ActionType.Write, true, true);
              if (this.IsObjectAttribute)
              {
                DBObject parentObject = this.ParentObject as DBObject;
                if (ServerConsts.CheckAttributeLCStepSecurity)
                {
                  IDBSecurity attributeSecurity = parentObject.LCStepObject.GetAttributeSecurity(this.AttributeID);
                  (attributeSecurity as DBSessionable)._AccessOwnerID = parentObject.OwnerID;
                  flag = flag && attributeSecurity.CheckAccess(ActionType.Write, true, true);
                }
                if (newvalue != null)
                  parentObject.BeforeSetAdditionalAttributeValue((IDBAttribute) this, newvalue);
                parentObject.CheckEditMode((this.AttributeType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None, this.AttributeType.IsContent, false);
              }
              else
              {
                if (newvalue != null)
                  (this.ParentObject as DBRelation).BeforeSetAdditionalAttributeValue((IDBAttribute) this, newvalue);
                (this.ParentObject as DBRelation).ValidateEditObject((this.AttributeType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None, this.AttributeType.IsContent);
              }
            }
          }
          if (newvalue != null && !this._BatchMode && !this.IsCreationMode)
            this.AddEvent(ActionType.Write, flag ? EventlogRecordType.AccessGranted : EventlogRecordType.AccessDenied);
          return flag;
        }
        catch (Exception ex)
        {
          if (newvalue != null && !this._BatchMode && !this.IsCreationMode)
            this.AddEvent(ActionType.Write, EventlogRecordType.AccessDenied, ex.Message);
          throw;
        }
      }
    }
    return true;
  }

  public bool IsCreationMode
  {
    get
    {
      return this.DBObjectID < 0L && this.IsObjectAttribute && (this.ParentObject as DBObject).IsCreationMode;
    }
  }

  private MultiValueModes MultiMode => this.AttributeType.MultipleValued;

  protected void ValidateMultiValueWrite(string fldName, object val)
  {
    if (!(fldName == this.AttributeType.PossibleValueFieldName))
      return;
    if (this.MultiMode == MultiValueModes.SingleValueFromList || this.MultiMode == MultiValueModes.MultiValuesFromList)
    {
      string str = val == null ? string.Empty : (fldName == "F_INTEGER_VALUE" || fldName == "F_DOUBLE_VALUE" ? SqlHelper.ToSqlDouble(val) : SqlHelper.QString(val.ToString()));
      if (this.UserSession.DBCache.GetTable("IMS_POSSIBLE_VALUES").Select($"F_ATTRIBUTE_ID = {this.AttributeID} AND {fldName} = {str}").Length == 0)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12366.ssp_appserver_12371()), (object) this.AttributeType.Name, (object) this.GetValueDescription(val)));
    }
    if (!this._CheckRepeatedValues || val == null || val == DBNull.Value || this.MultiMode != MultiValueModes.MultiValues && this.MultiMode != MultiValueModes.MultiValuesFromList)
      return;
    for (int index = 0; index < this._ValuesTable.RowsCount; ++index)
    {
      if (index != this.Index && this._ValuesTable[index][fldName].Equals(val))
        throw new KernelExceptionID(sc_12366.ssp_appserver_12372(710898256), (object) this.Name, (object) val.ToString());
    }
  }

  protected virtual bool ValidateValue(object newValue) => true;

  protected virtual string GetValueDescription(object val)
  {
    if (this.MultiMode == MultiValueModes.SingleValueFromList || this.MultiMode == MultiValueModes.MultiValuesFromList)
    {
      DataRow[] possibleValuesRows = this.AttributeType.GetPossibleValuesRows();
      for (int index = 0; index < possibleValuesRows.Length; ++index)
      {
        if (possibleValuesRows[index][1].Equals(val))
        {
          if (possibleValuesRows[index][2].ToString() != string.Empty)
            return possibleValuesRows[index][2].ToString();
          break;
        }
      }
    }
    return val.ToString();
  }

  private void Check4ObjectLinkAttributes(string newValue)
  {
    if (this._TemporaryAttribute || !this.IsObjectAttribute)
      return;
    object captionAttribute = (this.UserSession.DBCache as CacheDataset).CaptionAttributes[(object) this.AttributeID];
    if (captionAttribute == null || (captionAttribute as ArrayList).BinarySearch((object) this.TypeID) < 0)
      return;
    (this.ParentObject as DBObject).SetCaption(newValue);
  }

  internal override void SetCalculatedValue(object newValue, bool postedWrite)
  {
    if (postedWrite && this.ParentObject.NewValues != null && DBAttributeType.CanSkipInit(this.DataType))
    {
      this.ParentObject.AddComputedValue(this.AttributeID, newValue);
    }
    else
    {
      object obj = this.Value;
      if (!(obj == null || newValue == null ? obj != newValue : !obj.Equals(newValue)))
        return;
      if (newValue is DateTime)
        newValue = (object) (Convert.ToDateTime(newValue) - this.UserSession.TimeZoneOffset);
      if (this._Attributes != null)
        this._Attributes.AddDeltaValue(this.AttributeID);
      this.CheckUniqueValue(new object[1]{ newValue }, true);
      int int32 = Convert.ToInt32(this.AttributeType.SizeType);
      if (this.AttributeType.AttributeType == FieldTypes.ftString && newValue != null && newValue.ToString().Length > int32)
        newValue = (object) newValue.ToString().Substring(0, int32);
      this.ValidateValue(newValue);
      string valueFieldName = this.AttributeType.ValueFieldName;
      if (this.DataType == FieldTypes.ftMemo && newValue is string)
      {
        bool validatingOn = this.ValidatingOn;
        try
        {
          this.ValidatingOn = false;
          this.AsString = newValue.ToString();
        }
        finally
        {
          this.ValidatingOn = validatingOn;
        }
      }
      else
      {
        if (!this._TemporaryAttribute)
        {
          this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET {valueFieldName} = :val WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :index1", this.UserSession.DataManager.Parameter("val", newValue), this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("index1", (object) this._Index));
          this.UpdateModifyValue();
          AttributeValueField fldType;
          switch (valueFieldName)
          {
            case "F_STRING_VALUE":
              this.Check4ObjectLinkAttributes(newValue.ToString());
              fldType = AttributeValueField.String;
              break;
            case "F_INTEGER_VALUE":
              fldType = AttributeValueField.Integer;
              break;
            case "F_DOUBLE_VALUE":
              fldType = AttributeValueField.Double;
              break;
            default:
              fldType = AttributeValueField.Date;
              break;
          }
          string inViewFieldName = this.GetInViewFieldName(fldType);
          if (inViewFieldName != string.Empty)
            this.UpdateViewValue(inViewFieldName, newValue, this.DBObjectID);
        }
        this._ValuesTable[this._Index][valueFieldName] = newValue;
        this.ChangeComputedValues(postedWrite);
      }
    }
  }

  internal void DirectSetValues(
    object strValue,
    object intValue,
    object dblValue,
    object dateValue)
  {
    if (!this._TemporaryAttribute)
    {
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        IDbManager dataManager = this.UserSession.DataManager;
        List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>(6);
        dbDataParameterList.Add(dataManager.Parameter("objID", (object) this.DBObjectID));
        dbDataParameterList.Add(dataManager.Parameter("attrID", (object) this.AttributeID));
        dbDataParameterList.Add(dataManager.Parameter("index1", (object) this._Index));
        if (strValue != null)
        {
          stringBuilder.Append("F_STRING_VALUE = :strValue,");
          dbDataParameterList.Add(dataManager.Parameter(nameof (strValue), strValue));
          this.Check4ObjectLinkAttributes(strValue.ToString());
          string inViewFieldName = this.GetInViewFieldName(AttributeValueField.String);
          if (inViewFieldName != string.Empty)
            this.UpdateViewValue(inViewFieldName, strValue, this.DBObjectID);
        }
        if (intValue != null)
        {
          stringBuilder.Append("F_INTEGER_VALUE = :intValue,");
          dbDataParameterList.Add(dataManager.Parameter(nameof (intValue), intValue));
          string inViewFieldName = this.GetInViewFieldName(AttributeValueField.Integer);
          if (inViewFieldName != string.Empty)
            this.UpdateViewValue(inViewFieldName, intValue, this.DBObjectID);
        }
        if (dblValue != null)
        {
          stringBuilder.Append("F_DOUBLE_VALUE = :dblValue,");
          dbDataParameterList.Add(dataManager.Parameter(nameof (dblValue), dblValue));
          string inViewFieldName = this.GetInViewFieldName(AttributeValueField.Double);
          if (inViewFieldName != string.Empty)
            this.UpdateViewValue(inViewFieldName, dblValue, this.DBObjectID);
        }
        if (dateValue != null)
        {
          dateValue = !(this.AttributeType.Mask == Intermech.Consts.OnlyDateFunction) ? (object) (Convert.ToDateTime(dateValue) - this.UserSession.TimeZoneOffset) : (object) Convert.ToDateTime(dateValue).Date;
          stringBuilder.Append("F_DATE_VALUE = :dateValue,");
          dbDataParameterList.Add(dataManager.Parameter(nameof (dateValue), dateValue));
          string inViewFieldName = this.GetInViewFieldName(AttributeValueField.Date);
          if (inViewFieldName != string.Empty)
            this.UpdateViewValue(inViewFieldName, dateValue, this.DBObjectID);
        }
        --stringBuilder.Length;
        this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET {stringBuilder.ToString()} WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :index1", dbDataParameterList.ToArray());
      }
    }
    if (strValue != null)
      this._ValuesTable[this._Index]["F_STRING_VALUE"] = strValue;
    if (intValue != null)
      this._ValuesTable[this._Index]["F_INTEGER_VALUE"] = intValue;
    if (dblValue != null)
      this._ValuesTable[this._Index]["F_DOUBLE_VALUE"] = dblValue;
    if (dateValue == null)
      return;
    if (this.TemporaryAttribute)
      dateValue = !(this.AttributeType.Mask == Intermech.Consts.OnlyDateFunction) ? (object) (Convert.ToDateTime(dateValue) - this.UserSession.TimeZoneOffset) : (object) Convert.ToDateTime(dateValue).Date;
    this._ValuesTable[this._Index]["F_DATE_VALUE"] = dateValue;
  }

  internal override void DirectSetValue(string fieldName, object newValue)
  {
    if (newValue is DateTime)
      newValue = !(this.AttributeType.Mask == Intermech.Consts.OnlyDateFunction) ? (object) (Convert.ToDateTime(newValue) - this.UserSession.TimeZoneOffset) : (object) Convert.ToDateTime(newValue).Date;
    if (!this._TemporaryAttribute)
    {
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET {fieldName} = :val WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :index1", this.UserSession.DataManager.Parameter("val", newValue), this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("index1", (object) this._Index));
      AttributeValueField fldType;
      switch (fieldName)
      {
        case "F_STRING_VALUE":
          this.Check4ObjectLinkAttributes(newValue.ToString());
          fldType = AttributeValueField.String;
          break;
        case "F_INTEGER_VALUE":
          fldType = AttributeValueField.Integer;
          break;
        case "F_DOUBLE_VALUE":
          fldType = AttributeValueField.Double;
          break;
        default:
          fldType = AttributeValueField.Date;
          break;
      }
      string inViewFieldName = this.GetInViewFieldName(fldType);
      if (inViewFieldName != string.Empty)
        this.UpdateViewValue(inViewFieldName, newValue, this.DBObjectID);
    }
    this._ValuesTable[this._Index][fieldName] = newValue;
  }

  public override void DirectSetValues(object[] values)
  {
    if (this.AttributeType.MultipleValued == MultiValueModes.SingleValue || this.AttributeType.MultipleValued == MultiValueModes.SingleValueFromList)
      throw new KernelException("Метод DirectSetValues предназначен для присвоения списка значений многозначным атрибутам.");
    if (this.TemporaryAttribute)
      throw new KernelException("Метод DirectSetValues не предназначен для временных атрибутов.");
    if (this.DataType != FieldTypes.ftDateTime && this.DataType != FieldTypes.ftDouble && this.DataType != FieldTypes.ftGuid && this.DataType != FieldTypes.ftInteger && this.DataType != FieldTypes.ftString)
      throw new KernelException("Метод DirectSetValues не поддерживается для типа данных " + this.DataType.ToString());
    if (!this.ValidateDirectWrite((object) null))
      return;
    if (values.Length == 0)
    {
      this.ClearValues();
    }
    else
    {
      string valueFieldName = this.AttributeType.ValueFieldName;
      this.UserSession.StartTransaction();
      try
      {
        this._Index = 0;
        IDbManager dataManager = this.UserSession.DataManager;
        if (!this.Value.Equals(values[0]))
          this.DirectSetValue(valueFieldName, values[0]);
        if (values.Length > 1)
        {
          DbType dataType;
          switch (valueFieldName)
          {
            case "F_INTEGER_VALUE":
              dataType = DbType.Int64;
              break;
            case "F_DATE_VALUE":
              dataType = DbType.DateTime;
              break;
            case "F_DOUBLE_VALUE":
              dataType = DbType.Double;
              break;
            default:
              dataType = DbType.String;
              break;
          }
          for (int index = 1; index < values.Length; ++index)
          {
            if (index >= this._ValuesCount)
            {
              dataManager.AddBatchSQL($"INSERT INTO {this.ValuesTableName} ({this.ValuesKeyName}, F_ATTRIBUTE_ID, F_INLIST_ID, {valueFieldName}) VALUES (:keyID1, :attrID, :inlistID, :val1)", new DbCommandParam[4]
              {
                dataManager.BatchParameter("keyID1", DbType.Int64, (object) this.DBObjectID),
                dataManager.BatchParameter("attrID", DbType.Int32, (object) this.AttributeID),
                dataManager.BatchParameter("inlistID", DbType.Int32, (object) index),
                dataManager.BatchParameter("val1", dataType, values[index])
              });
              this.AddRowToValuesTable();
              this._ValuesTable[index][valueFieldName] = values[index];
            }
            else if (!this._ValuesTable[index][valueFieldName].Equals(values[index]))
            {
              dataManager.AddBatchSQL($"UPDATE {this.ValuesTableName} SET {valueFieldName} = :val1 WHERE {this.ValuesKeyName} = :keyID1 AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :index1", new DbCommandParam[4]
              {
                dataManager.BatchParameter("keyID1", DbType.Int64, (object) this.DBObjectID),
                dataManager.BatchParameter("attrID", DbType.Int32, (object) this.AttributeID),
                dataManager.BatchParameter("index1", DbType.Int32, (object) index),
                dataManager.BatchParameter("val1", dataType, values[index])
              });
              this._ValuesTable[index][valueFieldName] = values[index];
            }
          }
          dataManager.ExecuteBatchSQL();
        }
        if (this._ValuesTable.RowsCount > values.Length)
        {
          dataManager.ExecuteNonQuery($"DELETE FROM {this.ValuesTableName} WHERE {this.ValuesKeyName} = :keyID1 AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID >= :inlistID", dataManager.Parameter("keyID1", (object) this.DBObjectID), dataManager.Parameter("attrID", (object) this.AttributeID), dataManager.Parameter("inlistID", (object) values.Length));
          for (int index = this._ValuesTable.RowsCount - 1; index >= values.Length; --index)
          {
            this._ValuesTable.RemoveAt(index);
            --this._ValuesCount;
          }
        }
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  private DataRow[] GetFormulaAttributes(string fldName, int mode)
  {
    string columnName1 = !(fldName == "F_FORMULA_ID") ? "F_FORMULA_ID" : "F_ATTRIBUTE_ID";
    string columnName2;
    string str;
    IDBAttributableType attributableType;
    if (this.IsObjectAttribute)
    {
      columnName2 = "F_OBJECT_TYPE";
      str = $"F_OBJECT_TYPE IN (-1, {this.TypeID}) AND F_RELATION_TYPE = -1";
      attributableType = (IDBAttributableType) this.UserSession.GetObjectType(this.TypeID);
    }
    else
    {
      columnName2 = "F_RELATION_TYPE";
      str = $"F_RELATION_TYPE IN (-1, {this.TypeID}) AND F_OBJECT_TYPE = -1";
      attributableType = (IDBAttributableType) this.UserSession.GetRelationType(this.TypeID);
    }
    DataTable table = this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS");
    DataRow[] dataRowArray = table.Select($"{fldName} = {this.AttributeID} AND {str} AND F_MODE_ID = {mode}");
    DataTable toTable = table.Clone();
    for (int index = 0; index < dataRowArray.Length; ++index)
    {
      if (Convert.ToInt32(dataRowArray[index][columnName2]) != -1 || attributableType.Attributes.GetAttributeByID(Convert.ToInt32(dataRowArray[index][columnName1]), false) == null)
        SqlHelper.AssignRow(toTable, dataRowArray[index]);
    }
    return toTable.Select();
  }

  internal override object GetCalculatedValue(DBAttribute changedAttribute)
  {
    if (changedAttribute != null && changedAttribute.Index > 0)
      return (object) null;
    string text = this.AttributeType.Formula.Trim();
    if (text == string.Empty)
      return (object) DBNull.Value;
    ExpressionTree expressionTree;
    ExpressionVariablesCollection variables;
    using (Parser parser = new Parser())
    {
      parser.AutoDetectVariables = true;
      parser.Validate = false;
      expressionTree = parser.Parse(text);
      variables = expressionTree.Variables;
    }
    object[] values = new object[variables.Count];
    for (int index = 0; index < variables.Count; ++index)
    {
      DBAttribute attr = changedAttribute == null || !(variables[index].Name.ToUpper() == changedAttribute.Name.ToUpper()) ? this.Attributes.FindByName(variables[index].Name) as DBAttribute : changedAttribute;
      if (attr != null && !attr.Deleted)
      {
        attr.Index = 0;
        if (this.DataType == FieldTypes.ftString)
        {
          if ((attr.AttributeType.Options & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent)
          {
            IDictionaryServerService service = ServerServices.GetService(typeof (IDictionaryServerService)) as IDictionaryServerService;
            values[index] = (object) service.GetDescription((IDBAttribute) attr);
          }
          else
            values[index] = !AttributesTypeHelper.IsComplexAttributeType(attr.DataType) ? attr.Value : (object) attr.AsString;
        }
        else if ((attr.AttributeType.Options & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent)
        {
          IDictionaryServerService service = ServerServices.GetService(typeof (IDictionaryServerService)) as IDictionaryServerService;
          values[index] = (object) service.GetDescription((IDBAttribute) attr);
        }
        else
          values[index] = attr.Value;
      }
      else if (this.IsObjectAttribute)
      {
        IDBAttributeType attributeType = this.UserSession.GetAttributeType(variables[index].Name, false);
        if (attributeType != null && attributeType.AttributeType == FieldTypes.ftSystem && (attributeType as DBAttributeType).CanUseInFormula)
        {
          if (this.DataType == FieldTypes.ftString && (attributeType.AttributeID == -9 || attributeType.AttributeID == -4 || attributeType.AttributeID == -7 || attributeType.AttributeID == -14))
          {
            string[] descriptionsByGuid = this.ParentObject.GetDescriptionsByGuid((attributeType as IDBGuid).GUID, true);
            values[index] = (object) descriptionsByGuid[0];
          }
          else
          {
            object[] valuesByGuid = this.ParentObject.GetValuesByGuid((attributeType as IDBGuid).GUID, true);
            if (attributeType.AttributeID == -2)
              valuesByGuid[0] = (object) Math.Abs(Convert.ToInt64(valuesByGuid[0]));
            values[index] = valuesByGuid[0];
          }
        }
        else
          values[index] = (object) DBNull.Value;
      }
      else
        values[index] = (object) DBNull.Value;
    }
    try
    {
      object indexedString = expressionTree.Evaluate(values);
      if (this.AttributeType.Computed == ComputeValueModes.IndexValue && indexedString is string)
        indexedString = (object) this.UserSession.StringNormalizer.GetIndexedString(indexedString.ToString());
      return indexedString;
    }
    catch
    {
      return (object) DBNull.Value;
    }
  }

  internal override void ChangeComputedValues(bool postedWrite)
  {
    foreach (int AttributeID in this.UserSession.DBCache.GetFormulasID(this.AttributeID, this.TypeID, Intermech.Consts.Attribute4Formula, this.IsObjectAttribute))
    {
      if (this.Attributes.FindByID(AttributeID) is DBAttribute byId && (byId.AttributeType.Computed == ComputeValueModes.StoredValue || byId.AttributeType.Computed == ComputeValueModes.IndexValue) && this.Index <= 0)
      {
        object calculatedValue = byId.GetCalculatedValue((DBAttribute) this);
        if (calculatedValue != null)
          byId.SetCalculatedValue(calculatedValue, postedWrite);
      }
    }
    if (!this.IsObjectAttribute)
      return;
    int[] decodingAttributes = this.UserSession.DBCache.GetDecodingAttributes(this.TypeID);
    if (decodingAttributes == null)
      return;
    IDictionaryServerService service = ServiceUtils.GetService<IDictionaryServerService>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    foreach (int AttributeID in decodingAttributes)
    {
      if (AttributeID == this.AttributeID)
        break;
      IDBAttribute byId = this.Attributes.FindByID(AttributeID);
      if (byId != null && service.IsAttributeExistsInValue(this.AttributeType, byId.AsString) && byId is DBAttribute dbAttribute)
        dbAttribute.ChangeComputedValues(postedWrite);
    }
  }

  protected virtual string ValidationRuleFormula => this.AttributeType.ValidationRule;

  internal override void ValidateRule(int attributeID, object newValue)
  {
    if (this.ValidationRuleFormula == string.Empty)
      return;
    ExpressionTree expressionTree;
    ExpressionVariablesCollection variables;
    using (Parser parser = new Parser())
    {
      parser.AutoDetectVariables = true;
      parser.Validate = false;
      expressionTree = parser.Parse(this.ValidationRuleFormula);
      variables = expressionTree.Variables;
    }
    object[] values = new object[variables.Count];
    for (int index1 = 0; index1 < variables.Count; ++index1)
    {
      if (attributeID == this.AttributeID && (variables[index1].Name.ToUpper() == this.Name.ToUpper() || variables[index1].Name.ToUpper() == "VALUE"))
      {
        values[index1] = newValue;
      }
      else
      {
        IDBAttribute byName = this.Attributes.FindByName(variables[index1].Name);
        if (byName != null)
        {
          byName.Index = 0;
          if (byName.AttributeID == attributeID)
          {
            values[index1] = newValue;
          }
          else
          {
            bool flag = false;
            if (this.ParentObject.NewValues != null)
            {
              for (int index2 = 0; index2 < this.ParentObject.NewValues.Length; ++index2)
              {
                if (this.ParentObject.NewValues[index2].AttributeID == byName.AttributeID && this.ParentObject.NewValues[index2].Values != null && this.ParentObject.NewValues[index2].Values.Length != 0 && this.ParentObject.NewValues[index2].Values[0] != null)
                {
                  values[index1] = this.ParentObject.NewValues[index2].Values[0];
                  flag = true;
                  break;
                }
              }
            }
            if (!flag)
            {
              if ((byName.AttributeType.Options & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent)
              {
                IDictionaryServerService service = ServerServices.GetService(typeof (IDictionaryServerService)) as IDictionaryServerService;
                values[index1] = (object) service.GetDescription(byName);
              }
              else
                values[index1] = byName.Value;
            }
          }
        }
        else
          values[index1] = (object) string.Empty;
      }
    }
    bool boolean;
    try
    {
      boolean = Convert.ToBoolean(expressionTree.Evaluate(values));
    }
    catch (Exception ex)
    {
      throw new KernelExceptionID(sc_12366.ssp_appserver_12373(235402664), (object) this.AttributeType.ValidationRule, (object) this.Name, (object) ex.Message);
    }
    if (!boolean)
    {
      string str = attributeID != this.AttributeID ? this.UserSession.GetAttributeType(attributeID).Name : this.Name;
      throw new KernelExceptionID(sc_12366.ssp_appserver_12374(307510861), newValue, (object) str, (object) $"'{this.Name}': {this.AttributeType.ValidationRule}");
    }
  }

  protected virtual bool IsNullValue(object newValue)
  {
    if (newValue is string)
      return (newValue as string).Trim() == string.Empty;
    return newValue == null || newValue == DBNull.Value;
  }

  internal override void CheckNotNullValue(object newValue)
  {
    if (this.IsNullValue(newValue))
    {
      string str = !(this.ParentObject is IDBObject) ? (!(this.ParentObject is DBRelation) ? string.Empty : string.Format(LocalizationHolder.rm.GetString("Kernel_942"), (object) (this.ParentObject as DBRelation).ObjectName)) : string.Format(LocalizationHolder.rm.GetString("Kernel_941"), (object) (this.ParentObject as IDBObject).NameInMessages);
      throw new KernelExceptionID(sc_12366.ssp_appserver_12375(1504515572), (object) this.Name, (object) str);
    }
  }

  private void CheckValidationRule(object newValue)
  {
    if (!this.ParentObject.MustCheckValidatingRule || !this.AttributeType.ComputableAttribute && this.AttributeType.AttributeType != FieldTypes.ftGuid && this.AttributeType.AttributeType != FieldTypes.ftObjectLink)
      return;
    if ((this.AttributeType.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls && !this.TemporaryAttribute)
      this.CheckNotNullValue(newValue);
    this.CheckUniqueValue(new object[1]{ newValue }, true);
    if (this.AttributeType.ValidationRule != "")
      this.ValidateRule(this.AttributeID, newValue);
    if (this.Index != 0)
      return;
    int[] formulasId = this.UserSession.DBCache.GetFormulasID(this.AttributeID, this.TypeID, Intermech.Consts.Attribute4ValidationRule, this.IsObjectAttribute);
    for (int index = 0; index < formulasId.Length; ++index)
    {
      if (formulasId[index] != this.AttributeID && this.Attributes.FindByID(formulasId[index]) is DBAttribute byId)
        byId.ValidateRule(this.AttributeID, newValue);
    }
  }

  public override string AsString
  {
    get
    {
      object obj = this.AttributeType.Computed != ComputeValueModes.JITValue ? this._ValuesTable[this._Index]["F_STRING_VALUE"] : this.GetCalculatedValue((DBAttribute) null);
      return obj == DBNull.Value || obj == null ? string.Empty : obj.ToString();
    }
    set
    {
      if (!(this.AsString != value) || !this.ValidateDirectWrite((object) value))
        return;
      this.CheckUpdateValue(false);
      this.UserSession.StartTransaction();
      try
      {
        this.CheckValidationRule((object) value);
        this.SetOptimizerStat();
        if (this.IsGenerateWriteEvent())
        {
          AttributeValueEventArgs args = new AttributeValueEventArgs((object) value, (object) this.AsString, this._BatchMode, (IUserSession) this.UserSession);
          (this.EventHelper as EventLogHelper).OnAttributeWriteEvent((IDBAttribute) this, args);
          if (args.NewValue != null)
          {
            if (args.NewValue == DBNull.Value)
            {
              this.Clear();
              this.UserSession.Commit();
              return;
            }
            value = Convert.ToString(args.NewValue);
            if (this._Attributes != null)
              this._Attributes.AddDeltaValue(this.AttributeID);
          }
        }
        this.ValidateMultiValueWrite("F_STRING_VALUE", (object) value);
        if (!this._TemporaryAttribute)
        {
          this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET F_STRING_VALUE = :val WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :index1", this.UserSession.DataManager.Parameter("val", (object) value), this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("index1", (object) this._Index));
          this.UpdateModifyValue();
          this.Check4ObjectLinkAttributes(value);
          string inViewFieldName = this.GetInViewFieldName(AttributeValueField.String);
          if (inViewFieldName != string.Empty)
            this.UpdateViewValue(inViewFieldName, (object) value, this.DBObjectID);
        }
        this._ValuesTable[this._Index]["F_STRING_VALUE"] = (object) value;
        this.ChangeComputedValues(true);
        this.SaveHistoryValues(false);
        this.GenerateDelayedNotification((object) value);
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  public override long AsInteger
  {
    get
    {
      object obj = this.AttributeType.Computed != ComputeValueModes.JITValue ? this._ValuesTable[this._Index]["F_INTEGER_VALUE"] : this.GetCalculatedValue((DBAttribute) null);
      return obj == DBNull.Value || obj == null ? 0L : Convert.ToInt64(obj);
    }
    set
    {
      if (this.AsInteger == value && !this.IsNull || !this.ValidateDirectWrite((object) value))
        return;
      this.CheckUpdateValue(false);
      this.UserSession.StartTransaction();
      try
      {
        this.CheckValidationRule((object) value);
        this.SetOptimizerStat();
        if (this.IsGenerateWriteEvent())
        {
          AttributeValueEventArgs args = new AttributeValueEventArgs((object) value, (object) this.AsInteger, this._BatchMode, (IUserSession) this.UserSession);
          (this.EventHelper as EventLogHelper).OnAttributeWriteEvent((IDBAttribute) this, args);
          if (args.NewValue != null)
          {
            if (args.NewValue == DBNull.Value)
            {
              this.Clear();
              this.UserSession.Commit();
              return;
            }
            value = Convert.ToInt64(args.NewValue);
            if (this._Attributes != null)
              this._Attributes.AddDeltaValue(this.AttributeID);
          }
        }
        this.ValidateMultiValueWrite("F_INTEGER_VALUE", (object) value);
        if (!this._TemporaryAttribute)
        {
          this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET F_INTEGER_VALUE = :val WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :index1", this.UserSession.DataManager.Parameter("val", (object) value), this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("index1", (object) this._Index));
          this.UpdateModifyValue();
          string inViewFieldName = this.GetInViewFieldName(AttributeValueField.Integer);
          if (inViewFieldName != string.Empty)
            this.UpdateViewValue(inViewFieldName, (object) value, this.DBObjectID);
        }
        this._ValuesTable[this._Index]["F_INTEGER_VALUE"] = (object) value;
        this.ChangeComputedValues(true);
        this.SaveHistoryValues(false);
        this.GenerateDelayedNotification((object) value);
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  public override double AsDouble
  {
    get
    {
      object obj = this.AttributeType.Computed != ComputeValueModes.JITValue ? this._ValuesTable[this._Index]["F_DOUBLE_VALUE"] : this.GetCalculatedValue((DBAttribute) null);
      return obj == DBNull.Value || obj == null ? 0.0 : Math.Round(Convert.ToDouble(obj), Intermech.Consts.MaxPrecision);
    }
    set
    {
      value = Math.Round(value, Intermech.Consts.MaxPrecision);
      if (this.AsDouble == value && !this.IsNull || !this.ValidateDirectWrite((object) value))
        return;
      this.CheckUpdateValue(false);
      this.UserSession.StartTransaction();
      try
      {
        this.CheckValidationRule((object) value);
        this.SetOptimizerStat();
        if (this.IsGenerateWriteEvent())
        {
          AttributeValueEventArgs args = new AttributeValueEventArgs((object) value, (object) this.AsDouble, this._BatchMode, (IUserSession) this.UserSession);
          (this.EventHelper as EventLogHelper).OnAttributeWriteEvent((IDBAttribute) this, args);
          if (args.NewValue != null)
          {
            if (args.NewValue == DBNull.Value)
            {
              this.Clear();
              this.UserSession.Commit();
              return;
            }
            value = Math.Round(Convert.ToDouble(args.NewValue), Intermech.Consts.MaxPrecision);
            if (this._Attributes != null)
              this._Attributes.AddDeltaValue(this.AttributeID);
          }
        }
        this.ValidateMultiValueWrite("F_DOUBLE_VALUE", (object) value);
        if (!this._TemporaryAttribute)
        {
          this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET F_DOUBLE_VALUE = :val WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :index1", this.UserSession.DataManager.Parameter("val", (object) value), this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("index1", (object) this._Index));
          this.UpdateModifyValue();
          string inViewFieldName = this.GetInViewFieldName(AttributeValueField.Double);
          if (inViewFieldName != string.Empty)
            this.UpdateViewValue(inViewFieldName, (object) value, this.DBObjectID);
        }
        this._ValuesTable[this._Index]["F_DOUBLE_VALUE"] = (object) value;
        this.ChangeComputedValues(true);
        this.SaveHistoryValues(false);
        this.GenerateDelayedNotification((object) value);
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  public override DateTime AsDateTime
  {
    get
    {
      object obj = this.AttributeType.Computed != ComputeValueModes.JITValue ? this._ValuesTable[this._Index]["F_DATE_VALUE"] : this.GetCalculatedValue((DBAttribute) null);
      if (obj == DBNull.Value || obj == null)
        return DateTime.MinValue;
      DateTime asDateTime = Convert.ToDateTime(obj);
      if (this.AttributeType.Mask == Intermech.Consts.OnlyDateFunction)
        asDateTime = asDateTime.Date;
      else if (this.AttributeType.Computed != ComputeValueModes.JITValue)
      {
        try
        {
          asDateTime += this.UserSession.TimeZoneOffset;
        }
        catch
        {
        }
      }
      return asDateTime;
    }
    set
    {
      if (!(this.AsDateTime != value) || !this.ValidateDirectWrite((object) value))
        return;
      this.CheckUpdateValue(false);
      this.UserSession.StartTransaction();
      try
      {
        if (value > DateTime.MaxValue - TimeSpan.FromHours(24.0))
          throw new KernelExceptionID(sc_12366.ssp_appserver_12376(1463875154), (object) this.Name);
        this.CheckValidationRule((object) value);
        this.SetOptimizerStat();
        if (this.IsGenerateWriteEvent())
        {
          AttributeValueEventArgs args = new AttributeValueEventArgs((object) value, (object) this.AsDateTime, this._BatchMode, (IUserSession) this.UserSession);
          (this.EventHelper as EventLogHelper).OnAttributeWriteEvent((IDBAttribute) this, args);
          if (args.NewValue != null)
          {
            if (args.NewValue == DBNull.Value)
            {
              this.Clear();
              this.UserSession.Commit();
              return;
            }
            value = Convert.ToDateTime(args.NewValue);
            if (this._Attributes != null)
              this._Attributes.AddDeltaValue(this.AttributeID);
          }
        }
        if (this.AttributeType.Mask == Intermech.Consts.OnlyDateFunction)
          value = value.Date;
        else
          value -= this.UserSession.TimeZoneOffset;
        this.ValidateMultiValueWrite("F_DATE_VALUE", (object) value);
        if (!this._TemporaryAttribute)
        {
          this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET F_DATE_VALUE = :val WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :index1", this.UserSession.DataManager.Parameter("val", (object) value), this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("index1", (object) this._Index));
          this.UpdateModifyValue();
          string inViewFieldName = this.GetInViewFieldName(AttributeValueField.Date);
          if (inViewFieldName != string.Empty)
            this.UpdateViewValue(inViewFieldName, (object) value, this.DBObjectID);
        }
        this._ValuesTable[this._Index]["F_DATE_VALUE"] = (object) value;
        this.ChangeComputedValues(true);
        this.SaveHistoryValues(false);
        this.GenerateDelayedNotification((object) value);
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  public override bool AsBoolean
  {
    get
    {
      object obj = this._ValuesTable[this._Index]["F_INTEGER_VALUE"];
      return obj != DBNull.Value && Convert.ToBoolean(obj);
    }
    set
    {
      if (value)
        this.AsInteger = 1L;
      else
        this.AsInteger = 0L;
    }
  }

  private void AddRowToValuesTable()
  {
    this._Index = this._ValuesCount++;
    HybridRow hrow = this._ValuesTable.NewRow();
    hrow["F_ATTRIBUTE_ID"] = (object) this.AttributeID;
    hrow[this.ValuesKeyName] = (object) this.DBObjectID;
    hrow["F_INLIST_ID"] = (object) this._Index;
    this._ValuesTable.Add(hrow);
  }

  protected virtual void DoAddValue(object newValue)
  {
    this.AddRowToValuesTable();
    this.SetOptimizerStat();
    if (!this._TemporaryAttribute)
    {
      this.UserSession.DataManager.ExecuteNonQuery($"INSERT INTO {this._ValuesTableName} (F_ATTRIBUTE_ID, {this.ValuesKeyName}, F_INLIST_ID) VALUES (:attrID, :objID, :index1)", this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("index1", (object) this._Index));
      this.UpdateModifyValue();
    }
    if (this.optimStat != null)
      this.optimStat.SaveToCache();
    if (newValue == null)
    {
      if (this.DataType != FieldTypes.ftBlob && this.DataType != FieldTypes.ftFile && this.DataType != FieldTypes.ftMemo && this.DataType != FieldTypes.ftShortBlob && (this.AttributeType.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls)
      {
        string str = !(this.ParentObject is IDBObject) ? (!(this.ParentObject is DBRelation) ? string.Empty : string.Format(LocalizationHolder.rm.GetString("Kernel_942"), (object) (this.ParentObject as DBRelation).ObjectName)) : string.Format(LocalizationHolder.rm.GetString("Kernel_941"), (object) (this.ParentObject as IDBObject).NameInMessages);
        throw new KernelExceptionID(sc_12366.ssp_appserver_12377(1538026150), (object) this.Name, (object) str);
      }
      this.InitDefaultValue();
    }
    else
      this.Value = newValue;
  }

  public override int AddValue(object newValue)
  {
    if (this.ValidateDirectWrite((object) null))
    {
      if (this.AttributeType.MultipleValued == MultiValueModes.SingleValue || this.AttributeType.MultipleValued == MultiValueModes.SingleValueFromList)
        throw new KernelExceptionID(sc_12366.ssp_appserver_12378(1747990944), (object) this.Name);
      this.StartTransaction();
      try
      {
        this.DoAddValue(newValue);
        this.Commit();
      }
      catch
      {
        this.Rollback();
        throw;
      }
    }
    return this._Index;
  }

  protected virtual void DoDeleteValue()
  {
    this.StartTransaction();
    try
    {
      this.AddEvent(ActionType.Write, EventlogRecordType.AccessGranted);
      this.LoggingOn = false;
      try
      {
        this._ParentObject.BeforeDeleteAdditionalAttributeValue((IDBAttribute) this);
        (this.EventHelper as EventLogHelper).OnAttributeDeleteValueEvent((IDBAttribute) this, new AttributeDeleteValueEventArgs(this.Index, this._BatchMode, (IUserSession) this.UserSession));
        AttributeDataTableValue deletedValue = new AttributeDataTableValue(this._ValuesTable[this._Index]);
        if (!this._TemporaryAttribute)
        {
          this.UserSession.DataManager.ExecuteNonQuery($"DELETE FROM {this._ValuesTableName} WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :index1", this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("index1", (object) this._Index));
          this.UpdateModifyValue();
        }
        this._ValuesTable.Remove(this._ValuesTable[this._Index]);
        --this._ValuesCount;
        if (!this._TemporaryAttribute && this._Index == 0)
          this.InsertIntoView(1, true);
        if (this._Index == this._ValuesCount)
        {
          --this._Index;
        }
        else
        {
          if (!this._TemporaryAttribute)
          {
            this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this._ValuesTableName} SET F_INLIST_ID = F_INLIST_ID - 1 WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID > :index1", this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("index1", (object) this._Index));
            this.UpdateModifyValue();
          }
          for (int index = 0; index < this._ValuesTable.RowsCount; ++index)
          {
            int int32 = Convert.ToInt32(this._ValuesTable[index]["F_INLIST_ID"]);
            if (int32 > this._Index)
              this._ValuesTable[index]["F_INLIST_ID"] = (object) (int32 - 1);
          }
        }
        this._ParentObject.AfterDeleteAdditionalAttributeValue((IDBAttribute) this, deletedValue);
        this.Commit();
      }
      finally
      {
        this.LoggingOn = true;
      }
    }
    catch (Exception ex)
    {
      this.Rollback();
      if (ex is AccessDeniedException)
        this.AddEvent(ActionType.Write, EventlogRecordType.AccessDenied);
      throw;
    }
  }

  internal void UpdateModifyValue()
  {
    if (this.IsObjectAttribute)
      this.UserSession.AddToModificationsHistory((CategoryValue) new ModificationEvent(1, this.DBObjectID, ActionType.Edit, this.TypeID));
    else
      this.UserSession.AddToModificationsHistory((CategoryValue) new RelationModificationEvent(5, this.DBObjectID, ActionType.EditLink, this.TypeID, (this.ParentObject as DBRelation).GUID, (this.ParentObject as DBRelation).ProjID));
  }

  public override int DeleteValue()
  {
    if (this.ValidateDirectWrite((object) null))
    {
      if (this.ValuesCount <= 1)
        throw new KernelExceptionID(sc_12366.ssp_appserver_12379(1205857739));
      this.StartTransaction();
      try
      {
        this.SetOptimizerStat();
        this.DoDeleteValue();
        if (this.optimStat != null)
          this.optimStat.SaveToCache();
        this.Commit();
      }
      catch
      {
        this.Rollback();
        throw;
      }
    }
    return this.ValuesCount;
  }

  public new virtual FieldTypes DataType
  {
    get
    {
      return this._AttributeType != null ? this._AttributeType.AttributeType : this.UserSession.GetAttributeType(this.AttributeID).AttributeType;
    }
  }

  public override bool IsSystem => false;

  public override string LanguageID
  {
    get => (this.AttributeType as IDBLanguage).LanguageID;
    set => throw new OperationNotApplicableException();
  }

  public override string LanguageName => (this.AttributeType as IDBLanguage).LanguageName;

  public override bool IsDefaultLanguage => (this.AttributeType as IDBLanguage).IsDefaultLanguage;

  public void SetGUID(Guid guid) => throw new OperationNotApplicableException();

  public override string SubjectAreas
  {
    get => (this.AttributeType as IDBSubjectArea).SubjectAreas;
    set => throw new OperationNotApplicableException();
  }

  public override string SubjectAreasCaption
  {
    get => this.UserSession.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
  }

  protected virtual int DoDelete()
  {
    if (!this._TemporaryAttribute)
    {
      this.StartTransaction();
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"DELETE FROM {this._ValuesTableName} WHERE {this.ValuesKeyName} = :objID AND F_ATTRIBUTE_ID = :attrID", this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID));
        this.UpdateModifyValue();
        string[] fieldNames = this.AttributeType.FieldNames;
        if (fieldNames != null)
        {
          foreach (string fldName in fieldNames)
            this.UpdateViewValue(fldName, (object) DBNull.Value, this.DBObjectID);
        }
        if (this._Attributes != null)
        {
          (this._Attributes as DBAttributeCollection).RemoveAttribute(this.AttributeID);
        }
        else
        {
          DBAttributable parentObject = this._ParentObject;
          if (parentObject != null && parentObject.InternalAttributesCollection != null)
            (parentObject.InternalAttributesCollection as DBAttributeCollection).RemoveAttribute(this.AttributeID);
        }
        this.Deleted = true;
        this.SetContentDate();
        this.ChangeComputedValues(true);
        this.Commit();
      }
      catch
      {
        this.Rollback();
        throw;
      }
    }
    else
    {
      if (this._Attributes != null)
      {
        (this._Attributes as DBAttributeCollection).RemoveAttribute(this.AttributeID);
      }
      else
      {
        DBAttributable parentObject = this._ParentObject;
        if (parentObject != null && parentObject.InternalAttributesCollection != null)
          (parentObject.InternalAttributesCollection as DBAttributeCollection).RemoveAttribute(this.AttributeID);
      }
      this.Deleted = true;
    }
    this._ParentObject.AfterDeleteAdditionalAttribute((IDBAttribute) this);
    return 0;
  }

  private void CheckUpdateValue(bool delete)
  {
    if (this.IsObjectAttribute)
    {
      if (!this.UserSession.CanChangeObjectElement(2, (object) this._DBObjectID, ObligatoryElementKeys.GetKeyForAttributeValue(this.AttributeType)))
        throw new KernelException(delete ? string.Format(LocalizationHolder.rm.GetString("Kernel_933"), (object) this.Name) : string.Format(LocalizationHolder.rm.GetString("Kernel_935"), (object) this.Name));
    }
    else if (!this.UserSession.CanChangeObjectElement(5, (object) this._DBRelationID, ObligatoryElementKeys.GetKeyForAttributeValue(this.AttributeType)))
      throw new KernelException(delete ? string.Format(LocalizationHolder.rm.GetString("Kernel_934"), (object) this.Name) : string.Format(LocalizationHolder.rm.GetString("Kernel_936"), (object) this.Name));
  }

  public override int Delete(long DeleteMode)
  {
    long EventID = 0;
    if ((DeleteMode & (long) Intermech.Consts.PurgeMode) == 0L)
    {
      try
      {
        this.CheckAccess(ActionType.Write);
      }
      catch
      {
        this.AddEvent(ActionType.Write, EventlogRecordType.AccessDenied);
        throw;
      }
      EventID = this.AddEvent(ActionType.Write, EventlogRecordType.AccessGranted, LocalizationHolder.rm.GetString("DeleteAttributeNote"));
    }
    this.CheckUpdateValue(true);
    this.StartTransaction();
    try
    {
      if (this.AttributeType is IDBAttributeType4 && (DeleteMode & (long) Intermech.Consts.PurgeMode) == 0L && (this.AttributeType as IDBAttributeType4).Required == RequiredModes.AutoRequired)
        throw new KernelExceptionID(sc_12366.ssp_appserver_12380(188266260), (object) this.Name);
      if (this.IsObjectAttribute && (DeleteMode & (long) Intermech.Consts.PurgeMode) == 0L)
        (this.ParentObject as DBObject).CheckEditMode((this.AttributeType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None, this.AttributeType.IsContent, false);
      this.ParentObject.BeforeDeleteAttribute((IDBAttribute) this);
      (this.EventHelper as EventLogHelper).OnAttributeDeleteEvent((IDBAttribute) this, new AttributeDeleteEventArgs(DeleteMode, (IUserSession) this.UserSession));
      int num = this.DoDelete();
      this.Commit();
      return num;
    }
    catch (Exception ex)
    {
      this.Rollback();
      string str = string.Format(LocalizationHolder.rm.GetString("Kernel_6"), (object) this.Name, (object) this.ParentObject.ObjectName, (object) ex.Message);
      if (EventID != 0L)
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
      if (!(ex is AccessDeniedException))
        throw new KernelException(str, ex);
      throw;
    }
  }

  public override void Assign(IDBAttribute sourceAttribute)
  {
    this.AttributeType.ValidateAssign(sourceAttribute.AttributeType);
    IBlobReader blobReader = sourceAttribute as IBlobReader;
    IBlobWriter blobWriter = this as IBlobWriter;
    if (blobReader != null && blobWriter != null)
    {
      this.StartTransaction();
      try
      {
        int index1 = this.Index;
        int index2 = sourceAttribute.Index;
        int num1 = 0;
        for (int index3 = 0; index3 < sourceAttribute.ValuesCount; ++index3)
        {
          sourceAttribute.Index = index3;
          if (sourceAttribute.AttributeID == this.UserSession.IdentHelper.FileAttributeID && ((this.Attributes.AssignMode & Intermech.Consts.CreateMode) == Intermech.Consts.CreateMode || (this.Attributes.AssignMode & 1024 /*0x0400*/) == 1024 /*0x0400*/))
          {
            switch ((sourceAttribute as IDBFileAttribute).FileType)
            {
              case FileTypes.ftRedlining:
                continue;
              case FileTypes.ftAuthentical:
                if (!ServerConsts.CopyAuthenticalFiles)
                  continue;
                break;
            }
          }
          if (index3 >= this.ValuesCount)
            this.AddValue((object) null);
          else
            this.Index = num1++;
          string flName = sourceAttribute.AsString;
          if (this.AttributeID == this.UserSession.IdentHelper.FileAttributeID)
          {
            IFileNamesService service = ServerServices.GetService(typeof (IFileNamesService)) as IFileNamesService;
            long num2 = !this.IsObjectAttribute ? this.DBObjectID : (this.ParentObject as IDBObject).ID;
            string fileName = flName;
            long id = num2;
            Guid sessionGuid = this.UserSession.SessionGUID;
            flName = service.GetUniqueFileName(fileName, id, sessionGuid);
          }
          if (!(this is DBStorageAttribute) || !(this as DBStorageAttribute).IsCloneFile(flName, sourceAttribute))
          {
            BlobInformation blobInfo = blobReader.OpenBlob(Intermech.Consts.BlobTransferBufferLength);
            if (flName != blobInfo.FileName)
              blobInfo.FileName = flName;
            if (blobWriter.OpenBlob(blobInfo, false))
            {
              if (blobWriter is DBStorageAttribute)
                (blobWriter as DBStorageAttribute)._DataBlockSize = Intermech.Consts.BlobTransferBufferLength;
              byte[] data = blobReader.ReadDataBlock();
              while (blobWriter.WriteDataBlock(data))
                data = blobReader.ReadDataBlock();
            }
          }
          blobReader.CloseBlob();
        }
        this.Index = index1;
        sourceAttribute.Index = index2;
        while (this.ValuesCount > sourceAttribute.ValuesCount)
        {
          this.Index = this.ValuesCount - 1;
          this.DeleteValue();
        }
        this.Commit();
      }
      catch
      {
        this.Rollback();
        throw;
      }
    }
    else
      this.Values = sourceAttribute.Values;
  }

  protected virtual object GetDefaultValue()
  {
    object defaultValue = this.AttributeType.DefaultValue;
    if (this.AttributeType.Computed == ComputeValueModes.StoredValue || this.AttributeType.Computed == ComputeValueModes.IndexValue)
      defaultValue = this.GetCalculatedValue((DBAttribute) null);
    return (this.EventHelper as EventLogHelper).OnGetAttributeDefaultValue((IDBAttribute) this, defaultValue, (IUserSession) this.UserSession);
  }

  protected virtual void SetDefaultValue(object defValue) => this.CheckValidationRule(defValue);

  private void InitDefaultValue()
  {
    object defaultValue = this.GetDefaultValue();
    if (defaultValue == DBNull.Value)
      return;
    this.LoggingOn = false;
    try
    {
      this.SetDefaultValue(defaultValue);
    }
    finally
    {
      this.LoggingOn = true;
    }
  }

  public override object[] Values
  {
    get
    {
      object[] values = new object[this.ValuesCount];
      int index1 = this.Index;
      try
      {
        for (int index2 = 0; index2 < this.ValuesCount; ++index2)
        {
          this.Index = index2;
          values[index2] = this.Value;
        }
      }
      finally
      {
        this.Index = index1;
      }
      return values;
    }
    set
    {
      if (value.Length == 0)
        throw new KernelExceptionID(sc_12366.ssp_appserver_12381(102794976), (object) this.Name);
      bool checkRepeatedValues = this._CheckRepeatedValues;
      if (checkRepeatedValues && value.Length > 1)
      {
        for (int index1 = 0; index1 < value.Length; ++index1)
        {
          for (int index2 = index1 + 1; index2 < value.Length; ++index2)
          {
            if (value[index1] != null && value[index1] != DBNull.Value && value[index1].Equals(value[index2]))
              throw new KernelExceptionID(sc_12366.ssp_appserver_12382(532449236), (object) this.Name, value[index1]);
          }
        }
        this._CheckRepeatedValues = false;
      }
      this.StartTransaction();
      try
      {
        this.AddEvent(ActionType.Write, EventlogRecordType.AccessGranted);
        this.LoggingOn = false;
        this._BatchMode = true;
        try
        {
          if (this.AttributeType.MultipleValued == MultiValueModes.MultiValues || this.AttributeType.MultipleValued == MultiValueModes.MultiValuesFromList)
            (this.EventHelper as EventLogHelper).OnAttributeValuesWriteEvent((IDBAttribute) this, new AttributeValuesEventArgs(value, this.Values, (IUserSession) this.UserSession));
          AttributeValues[] oldValues = (AttributeValues[]) null;
          if (!this._TemporaryAttribute && this.IsObjectAttribute && this.DBObjectID > 0L && (this.ParentObject.AttributesState & Intermech.Consts.AssignValuesMode) == 0)
            oldValues = (this.ParentObject as DBObject).GetAttributes4Notification((DBAttribute) this);
          for (int index = 0; index < value.Length; ++index)
          {
            if (index == this.ValuesCount)
            {
              this.AddValue(value[index]);
            }
            else
            {
              this.Index = index;
              this.Value = value[index];
            }
          }
          while (this.ValuesCount > value.Length)
          {
            this.Index = this.ValuesCount - 1;
            this.DeleteValue();
          }
          if (!this._TemporaryAttribute && this.IsObjectAttribute && this.DBObjectID > 0L && (this.ParentObject.AttributesState & Intermech.Consts.AssignValuesMode) == 0)
            this.UserSession.AddDelayedNotification((DelayedNotification) new AttributeValuesWriteDelayedNotification(this.UserSession.RealUserID, ActionType.Write, oldValues, (AttributeValues[]) null, this.DBObjectID, this.TypeID, value, this.AttributeID));
          this.Commit();
          this._CheckRepeatedValues = checkRepeatedValues;
        }
        finally
        {
          this.LoggingOn = true;
          this._BatchMode = false;
        }
      }
      catch (Exception ex)
      {
        this.Rollback();
        this._CheckRepeatedValues = checkRepeatedValues;
        if (ex is AccessDeniedException)
          this.AddEvent(ActionType.Write, EventlogRecordType.AccessDenied);
        throw;
      }
    }
  }

  public override void DoAfterCreate()
  {
    this.InitDefaultValue();
    (this.EventHelper as EventLogHelper).OnCreateAttribute((IDBAttribute) this, (IUserSession) this.UserSession);
  }

  public override DataTable GetPossibleValues() => this.AttributeType.GetPossibleValues();

  public override void CheckUniqueValue(object[] newValues, bool excludeThis)
  {
    if (this.AttributeType.UniqueMode == UniqueValueModes.NotUnique || newValues != null && !this.ParentObject.MustCheckValidatingRule || !this.IsObjectAttribute)
      return;
    bool flag1 = true;
    if (newValues == null)
      newValues = this.Values;
    foreach (object newValue in newValues)
    {
      if (newValue != null && newValue != DBNull.Value)
      {
        if (this.DataType == FieldTypes.ftInteger || this.DataType == FieldTypes.ftObjectLink || this.DataType == FieldTypes.ftAutoInc)
        {
          flag1 = false;
          break;
        }
        if (newValue is string && ((string) newValue).Trim() != string.Empty)
        {
          flag1 = false;
          break;
        }
      }
    }
    if (flag1)
      return;
    int num = -1;
    bool flag2 = true;
    if ((this.ParentObject as DBObject).ObjectTypeClass.IsLocalType)
      num = this.TypeID;
    else if (this.AttributeType.UniqueMode != UniqueValueModes.AllVerTypes)
    {
      num = this.TypeID;
      if (this.AttributeType is IDBAttributeType4Object attributeType4Object1)
      {
        while (attributeType4Object1.InheritMode == InheritModes.Inherited)
        {
          int objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(num);
          if (objectTypeParentId != -1)
          {
            if (this.UserSession.GetObjectType(objectTypeParentId).Attributes.GetAttributeByID(this.AttributeID, false) is IDBAttributeType4Object attributeType4Object1)
            {
              if (!ServerConsts.OldUniqueAttributesCheck)
                flag2 = false;
              num = objectTypeParentId;
            }
            else
              break;
          }
        }
      }
    }
    DBRecordSetParams paramSet = new DBRecordSetParams(1);
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    if (excludeThis)
    {
      if (this.AttributeType.UniqueMode == UniqueValueModes.TypeOnly)
      {
        conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.NotEqual, (object) Math.Abs(this._DBObjectID), LogicalOperators.AND, 0, true));
        if (this.ParentObject is IDBObject parentObject)
          conditionStructureList.Add(new ConditionStructure(-3, RelationalOperators.NotEqual, (object) parentObject.ID, LogicalOperators.AND, 0, true));
      }
      else
      {
        conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.NotEqual, (object) this._DBObjectID, LogicalOperators.AND, 0, true));
        conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.NotEqual, (object) -this._DBObjectID, LogicalOperators.AND, 0, true));
      }
    }
    for (int index = 0; index < newValues.Length; ++index)
    {
      int groupID = index != 0 || newValues.Length <= 1 ? (index != newValues.Length - 1 || newValues.Length <= 1 ? 0 : -1) : 1;
      if (newValues[index] != DBNull.Value)
        conditionStructureList.Add(new ConditionStructure(this.AttributeID, RelationalOperators.Equal, newValues[index], LogicalOperators.OR, groupID, true));
    }
    paramSet.Conditions = conditionStructureList.ToArray();
    paramSet.Conditions[paramSet.Conditions.Length - 1].LogicalOperator = LogicalOperators.AND;
    DBObjectCollection objectCollection = this.UserSession.GetObjectCollection(num) as DBObjectCollection;
    objectCollection.GlobalSelectMode = true;
    DataTable dataTable = objectCollection.Select(paramSet, false);
    if (dataTable.Rows.Count <= 0)
      return;
    if (flag2)
    {
      this.ThrowObjectAlreadyExistsException(Convert.ToInt64(dataTable.Rows[0][0]));
    }
    else
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        if (Convert.ToInt32(dataTable.Rows[index][1]) == this.TypeID)
          this.ThrowObjectAlreadyExistsException(Convert.ToInt64(dataTable.Rows[index][0]));
        if (this.UserSession.GetObjectType(Convert.ToInt32(dataTable.Rows[index][1])).Attributes.GetAttributeByID(this.AttributeID, false) is IDBAttributeType4Object attributeById && attributeById.InheritMode == InheritModes.Inherited)
          this.ThrowObjectAlreadyExistsException(Convert.ToInt64(dataTable.Rows[index][0]));
      }
    }
  }

  private void ThrowObjectAlreadyExistsException(long objectID)
  {
    IDBObject dbObject1 = this.UserSession.GetObject(Convert.ToInt64(objectID));
    string dopInfo = string.Empty;
    if (dbObject1.ObjectID > 0L && dbObject1.CheckoutBy == this.UserSession.UserID)
    {
      dopInfo = " Внимание! Данный объект взят Вами на изменение, поэтому в списках объектов Вы видите только рабочую копию, у которой значение данного атрибута может отличаться от соответствующего значения архивной копии.";
      if (this.AttributeType.MultipleValued == MultiValueModes.SingleValue || this.AttributeType.MultipleValued == MultiValueModes.SingleValueFromList)
      {
        IDBObject dbObject2 = this.UserSession.GetObject(-dbObject1.ObjectID, false);
        if (dbObject2 != null)
        {
          IDBAttribute attributeById1 = dbObject2.GetAttributeByID(this.AttributeID);
          if (attributeById1 != null)
          {
            IDBAttribute attributeById2 = dbObject1.GetAttributeByID(this.AttributeID);
            if (attributeById2 != null && attributeById1.Value != null && attributeById2.Value != null && attributeById1.Value.Equals(attributeById2.Value))
              dopInfo = string.Empty;
          }
        }
      }
    }
    throw new ObjectAlreadyExists(dbObject1.ObjectID, this.Name, dbObject1.NameInMessages, dopInfo);
  }

  internal virtual void PurgeValue()
  {
  }

  internal override void Purge(bool purgeOwner)
  {
    if (this._TemporaryAttribute)
      return;
    this.StartTransaction();
    try
    {
      if (!purgeOwner)
      {
        this.ParentObject.BeforeDeleteAttribute((IDBAttribute) this);
        this.UserSession.DataManager.ExecuteNonQuery($"DELETE FROM {this._ValuesTableName} WHERE {this.ValuesKeyName} = :p0 AND F_ATTRIBUTE_ID = :p1", this.UserSession.DataManager.Parameter("p0", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("p1", (object) this.AttributeID));
        string[] fieldNames = this.AttributeType.FieldNames;
        if (fieldNames != null)
        {
          foreach (string fldName in fieldNames)
            this.UpdateViewValue(fldName, (object) DBNull.Value, this.DBObjectID);
        }
        this.ChangeComputedValues(true);
      }
      this.Commit();
    }
    catch
    {
      this.Rollback();
      throw;
    }
  }

  private bool IsReadOnlyLitera
  {
    get
    {
      bool isReadOnlyLitera;
      if (this.UserSession.IsSystemSession)
        isReadOnlyLitera = false;
      else if (this.DBObjectID < 0L)
      {
        object[] valuesById = this.ParentObject.GetValuesByID(-6, false);
        isReadOnlyLitera = valuesById == null || valuesById.Length == 0 || Convert.ToInt64(valuesById[0]) != this.UserSession.UserID;
      }
      else
        isReadOnlyLitera = true;
      return isReadOnlyLitera;
    }
  }

  public override bool ReadOnly
  {
    get
    {
      bool flag;
      if (this.AttributeID == this.UserSession.IdentHelper.ModifyContentDateID)
        flag = true;
      else if (this.AttributeID == this.UserSession.IdentHelper.LiteraID)
      {
        flag = this.IsReadOnlyLitera;
      }
      else
      {
        flag = this.AttributeType.Computed != 0 | !this.CheckAccess(ActionType.Write, true, false);
        if (!flag)
        {
          if (ServerConsts.CheckAttributeLCStepSecurity && this.IsObjectAttribute)
          {
            DBObject parentObject = this.ParentObject as DBObject;
            IDBSecurity attributeSecurity = parentObject.LCStepObject.GetAttributeSecurity(this.AttributeID);
            (attributeSecurity as DBSessionable)._AccessOwnerID = parentObject.OwnerID;
            if (!attributeSecurity.CheckAccess(ActionType.Write, true, false))
              return true;
          }
          flag = !this.IsObjectAttribute ? !(this._ParentObject as DBRelation).ValidateEditObject((this.AttributeType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None, this.AttributeType.IsContent, false) : !(this._ParentObject as DBObject).CheckEditMode((this.AttributeType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None, this.AttributeType.IsContent, false, false);
        }
      }
      return flag;
    }
  }

  public override string GroupName
  {
    get
    {
      DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_ATTR_IN_GROUPS").Select("F_ATTRIBUTE_ID = " + this.AttributeID.ToString());
      return dataRowArray.Length != 0 ? this.UserSession.DBCache.GetTable("IMS_ATTR_GROUPS").Rows.Find(dataRowArray[0][0])["F_GROUP_NAME"].ToString() : string.Empty;
    }
  }

  internal void UpdateViewValue(string fldName, object newValue, long dbObjectID)
  {
    if (this.Index == 0)
    {
      int objectTypeID = -1;
      int relationTypeID = -1;
      string keyFld;
      if (this.IsObjectAttribute)
      {
        objectTypeID = this.TypeID;
        keyFld = "F_OBJECT_ID";
      }
      else
      {
        relationTypeID = this.TypeID;
        keyFld = "F_PRJLINK_ID";
      }
      string[] updateTables = this.UserSession.DBCache.GetUpdateTables(this.AttributeID, objectTypeID, relationTypeID);
      if (updateTables != null)
      {
        if (this.ParentObject.ViewsUpdaterInited)
        {
          foreach (string viewName in updateTables)
            this.ParentObject.ViewsUpdaterAddValue(viewName, dbObjectID, keyFld, newValue, fldName);
        }
        else
        {
          IDbManager dataManager = this.UserSession.DataManager;
          IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) dbObjectID);
          IDbDataParameter dbDataParameter2 = dataManager.Parameter("newVal", newValue);
          string format = $"UPDATE {"{0}"} SET {fldName} = :newVal WHERE {keyFld} = :objID";
          foreach (string str in updateTables)
            dataManager.ExecuteNonQuery(string.Format(format, (object) str), dbDataParameter2, dbDataParameter1);
        }
      }
    }
    if (this.DataType == FieldTypes.ftMemo || this.DataType == FieldTypes.ftFile)
      return;
    this.WriteToGlobalIndex(newValue);
  }

  internal void WriteToGlobalIndex(object newValue)
  {
    if (!this.IsObjectAttribute || (this.AttributeType.Options & AttributeOptions.AddToGlobalIndex) != AttributeOptions.AddToGlobalIndex || (this.ParentObject as DBObject).IsCreationMode || this._Attributes != null && (this._Attributes.AssignMode & Intermech.Consts.CheckOutMode) == Intermech.Consts.CheckOutMode)
      return;
    if (newValue == null || newValue == DBNull.Value)
    {
      this.UserSession.AddAttrToIndexQueue(string.Empty, (IDBAttribute) this);
    }
    else
    {
      if (!(newValue is string))
        return;
      this.UserSession.AddAttrToIndexQueue(newValue.ToString(), (IDBAttribute) this);
    }
  }

  internal override void InsertIntoView(int sign, bool writeNulls = false)
  {
    object newValue1 = this._ValuesTable[0]["F_STRING_VALUE"];
    if (newValue1 != DBNull.Value | writeNulls)
    {
      string inViewFieldName = this.GetInViewFieldName(AttributeValueField.String);
      if (inViewFieldName != string.Empty)
        this.UpdateViewValue(inViewFieldName, newValue1, (long) sign * this.DBObjectID);
    }
    object newValue2 = this._ValuesTable[0]["F_INTEGER_VALUE"];
    if (newValue2 != DBNull.Value | writeNulls)
    {
      string inViewFieldName = this.GetInViewFieldName(AttributeValueField.Integer);
      if (inViewFieldName != string.Empty)
        this.UpdateViewValue(inViewFieldName, newValue2, (long) sign * this.DBObjectID);
    }
    object newValue3 = this._ValuesTable[0]["F_DOUBLE_VALUE"];
    if (newValue3 != DBNull.Value | writeNulls)
    {
      string inViewFieldName = this.GetInViewFieldName(AttributeValueField.Double);
      if (inViewFieldName != string.Empty)
        this.UpdateViewValue(inViewFieldName, newValue3, (long) sign * this.DBObjectID);
    }
    object newValue4 = this._ValuesTable[0]["F_DATE_VALUE"];
    if (!(newValue4 != DBNull.Value | writeNulls))
      return;
    string inViewFieldName1 = this.GetInViewFieldName(AttributeValueField.Date);
    if (!(inViewFieldName1 != string.Empty))
      return;
    this.UpdateViewValue(inViewFieldName1, newValue4, (long) sign * this.DBObjectID);
  }

  protected virtual string GetInViewFieldName(AttributeValueField fldType) => string.Empty;

  protected virtual void SaveHistoryValues(bool alwaysSave)
  {
    if (this.optimStat != null)
      this.optimStat.SaveToCache();
    this.SetContentDate();
    this._ParentObject.AfterSetAdditionalAttributeValue((IDBAttribute) this);
    if (this.TemporaryAttribute || !alwaysSave && !this._AutoSaveHistory || (this.AttributeType.Options & AttributeOptions.SaveCommonHistory) != AttributeOptions.SaveCommonHistory && (this.AttributeType.Options & AttributeOptions.SavePrivateHistory) != AttributeOptions.SavePrivateHistory || !this.IsGenerateWriteEvent())
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    int objectType;
    int relationType;
    if (this.IsObjectAttribute)
    {
      objectType = this.TypeID;
      relationType = -1;
    }
    else
    {
      relationType = this.TypeID;
      objectType = -1;
    }
    if (this.UserSession.IsDelayedAttrHistory)
      this.UserSession.AddAttrHistory(new AttrHistoryProperties(this.AttributeID, objectType, relationType, this.UserSession.UserID, DateTime.UtcNow, this._ParentObject.HistoryObjectID, this._ValuesTable[this._Index]["F_INTEGER_VALUE"], this._ValuesTable[this._Index]["F_STRING_VALUE"], this._ValuesTable[this._Index]["F_DATE_VALUE"], this._ValuesTable[this._Index]["F_DOUBLE_VALUE"]));
    else
      dataManager.ExecuteSpNonQuery("IMS_ADD_ATTR_HISTORY", dataManager.Parameter("aID", (object) this.AttributeID), dataManager.Parameter("oType", (object) objectType), dataManager.Parameter("rType", (object) relationType), dataManager.Parameter("uID", (object) this.UserSession.UserID), dataManager.Parameter("id", (object) this._ParentObject.HistoryObjectID), dataManager.Parameter("intVal", this._ValuesTable[this._Index]["F_INTEGER_VALUE"]), dataManager.Parameter("strVal", this._ValuesTable[this._Index]["F_STRING_VALUE"]), dataManager.Parameter("dblVal", this._ValuesTable[this._Index]["F_DOUBLE_VALUE"]), dataManager.Parameter("datVal", this._ValuesTable[this._Index]["F_DATE_VALUE"]));
  }

  private void SetPublicationFlag(PublicationNecessary publicationNecessary)
  {
    if (this.AttributeID == this.UserSession.IdentHelper.AttributePublicationNecessary || this.AttributeID == this.UserSession.IdentHelper.AttributeOptionPublication)
      return;
    (this.ParentObject as DBObject).DoSetPublicationFlag(publicationNecessary);
  }

  internal virtual void SetContentDate()
  {
    if (this.TemporaryAttribute || this.AttributeID == this.UserSession.IdentHelper.ModifyContentDateID || this._Attributes != null && (this._Attributes.AssignMode & Intermech.Consts.CheckOutMode) == Intermech.Consts.CheckOutMode || this.IsCreationMode && DBAttribute._DontUpdateContentDateInBlanks.IndexOf(this.AttributeID) >= 0)
      return;
    IDBAttribute dbAttribute = (IDBAttribute) null;
    if (this.IsObjectAttribute)
    {
      DataRow dataRow = this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Rows.Find(new object[2]
      {
        (object) this.AttributeID,
        (object) this.TypeID
      }) ?? this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) this.AttributeID);
      if (dataRow != null && Convert.ToInt32(dataRow["F_CONTENT"]) != 0)
      {
        dbAttribute = (this.ParentObject as IDBAttributable).GetAttributeByID(this.UserSession.IdentHelper.ModifyContentDateID);
        if (dbAttribute == null)
          this.SetPublicationFlag(PublicationNecessary.Object);
      }
      else
        this.SetPublicationFlag(PublicationNecessary.FCAttributes);
    }
    else
    {
      DataRow dataRow = this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES").Rows.Find(new object[2]
      {
        (object) this.TypeID,
        (object) this.AttributeID
      }) ?? this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) this.AttributeID);
      if (dataRow != null && Convert.ToInt32(dataRow["F_CONTENT"]) != 0)
      {
        DBRelation dbRelation = !(this.ParentObject is DBRelation) ? this.UserSession.GetRelation(this.DBObjectID) as DBRelation : this.ParentObject as DBRelation;
        if (dbRelation.IsCheckParentReadOnly)
          dbAttribute = dbRelation.ProjObject.GetAttributeByID(this.UserSession.IdentHelper.ModifyContentDateID);
      }
    }
    if (dbAttribute == null)
      return;
    (dbAttribute as DBDateAttribute).WriteContentDate();
  }

  protected virtual string GetDescription() => this.Value.ToString();

  public override string Description
  {
    get
    {
      string description = this.GetDescription();
      if (this.AttributeType.MultipleValued == MultiValueModes.SingleValueFromList || this.AttributeType.MultipleValued == MultiValueModes.MultiValuesFromList)
        (this.UserSession.GetAttributeType(this.AttributeType.AttributeID) as DBAttributeType).GetPossibleValueDescription(this.Value, ref description);
      return description;
    }
  }

  public override string[] Descriptions
  {
    get
    {
      string[] descriptions = new string[this.ValuesCount];
      int index1 = this.Index;
      try
      {
        for (int index2 = 0; index2 < this.ValuesCount; ++index2)
        {
          this.Index = index2;
          descriptions[index2] = this.Description;
        }
      }
      finally
      {
        this.Index = index1;
      }
      return descriptions;
    }
  }

  public override bool VisibleByFilters
  {
    get
    {
      if ((this.AttributeType.Options & AttributeOptions.Internal) == AttributeOptions.Internal || (this.AttributeType.Options & AttributeOptions.LocalImbaseAttribute) == AttributeOptions.LocalImbaseAttribute || this.AttributeType.Computed == ComputeValueModes.IndexValue || !DBSubjectAreaCollection.IsVisibleArea(this.SubjectAreas, this.UserSession.AreaID))
        return false;
      if (this.LanguageID.Length > 0)
      {
        if (this.UserSession.LanguageID == string.Empty)
        {
          if (this.LanguageID != DBLanguageCollection.DefaultLanguage)
            return false;
        }
        else if (this.UserSession.LanguageID.IndexOf(this.LanguageID[0]) < 0)
          return false;
      }
      return true;
    }
  }

  public override bool VisibleByAccess
  {
    get => this.CheckAccess(ActionType.List, this.GetDefaultAccess(ActionType.List), false);
  }

  public override bool Visible
  {
    get
    {
      return this.VisibleByFilters && this.CheckAccess(ActionType.List, this.ParentObject.GetDefaultAccess(ActionType.List), false);
    }
  }

  public void StartTransaction()
  {
    if (this.TemporaryAttribute)
      return;
    this.UserSession.StartTransaction();
  }

  public void Commit()
  {
    if (this.TemporaryAttribute)
      return;
    this.UserSession.Commit();
  }

  public void Rollback()
  {
    if (this.TemporaryAttribute)
      return;
    this.UserSession.Rollback();
  }

  public override long AccessOwnerID => this.UseAccessCache ? 0L : this.ParentObject.AccessOwnerID;

  protected override string GetExtendedAccessSQL()
  {
    string extendedAccessSql = base.GetExtendedAccessSQL();
    if (this.ParentObject.CreatorID == this.UserSession.UserID)
      extendedAccessSql = !(extendedAccessSql == string.Empty) ? $"{extendedAccessSql},{this.UserSession.IdentHelper.ObjectCreatorGroupID.ToString()}" : this.UserSession.IdentHelper.ObjectCreatorGroupID.ToString();
    return extendedAccessSql;
  }

  protected virtual bool CheckLCStepAccess(ActionType actionType, bool throwException)
  {
    return !this.IsObjectAttribute || ((this.ParentObject as DBObject).LCStepObject as IDBSecurity).CheckAccess(actionType, true, throwException);
  }

  internal void GenerateDelayedNotification(object value)
  {
    if (this._BatchMode || this._TemporaryAttribute || !this.IsObjectAttribute || this.DBObjectID <= 0L || (this.ParentObject.AttributesState & Intermech.Consts.AssignValuesMode) != 0)
      return;
    this.UserSession.AddDelayedNotification((DelayedNotification) new AttributeValueWriteDelayedNotification(this.UserSession.RealUserID, ActionType.Write, (this.ParentObject as DBObject).GetAttributes4Notification((DBAttribute) this), (AttributeValues[]) null, this.DBObjectID, this.TypeID, value, this.AttributeID, this.Index));
  }

  int IDBAttributeEx.ValuesCount
  {
    get
    {
      if (this.ValuesCount > 0)
        return this.ValuesCount;
      return this.IsNull ? 0 : 1;
    }
  }

  int IDBAttributeEx.AddValue(object newValue)
  {
    return this.ValuesCount <= 1 && this.Index == 0 && this.IsNull ? this.Index : this.AddValue(newValue);
  }

  int IDBAttributeEx.DeleteValue()
  {
    if (this.Index > 0)
      return this.DeleteValue();
    this.Clear();
    return -1;
  }

  bool IDBAttributeEx.IsNull => this.ValuesCount < 1 && this.IsNull;

  int IDBAttributeEx.Index
  {
    get => this.ValuesCount < 1 && this.IsNull && this.Index == 0 ? -1 : this.Index;
    set
    {
      if (value == 0 && this.Index == 0 && this.ValuesCount < 1 && this.IsNull)
        throw new KernelException($"Ошибка позиционирования значения атрибута '{this.Name}'. Атрибут не имеет значений.");
      this.Index = value;
    }
  }
}
