// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributeType4Relation
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

internal class DBAttributeType4Relation : 
  DBAttributeType4Category,
  IDBAttributeType4Relation,
  IDBAttributeType4,
  IDBAttributeType
{
  public DBAttributeType4Relation(UserSession uSession, DataRow row)
    : base(uSession, Convert.ToInt32(row["F_ATTRIBUTE_ID"]), Convert.ToInt32(row["F_RELATION_TYPE"]), 6)
  {
    this._TableName = "IMS_ATTR4RELATION_TYPES";
    this._KeyName = "F_RELATION_TYPE";
    this.paramsTable.Create(row);
  }

  protected override DBAttributableType ParentType
  {
    get => this.UserSession.GetRelationType(this._TypeID) as DBAttributableType;
  }

  public int[] GetRelatedFormulaAttributes()
  {
    DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS").Select(string.Format("F_FORMULA_ID = {0} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = {1} AND F_MODE_ID = " + Consts.Attribute4Formula.ToString(), (object) this.AttributeID, (object) this._TypeID));
    int[] formulaAttributes = new int[dataRowArray.Length];
    for (int index = 0; index < dataRowArray.Length; ++index)
      formulaAttributes[index] = Convert.ToInt32(dataRowArray[index]["F_ATTRIBUTE_ID"]);
    return formulaAttributes;
  }

  protected override int CategoryType4EvenLog => 6;

  public override string ObjectName
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Kernel_874"), (object) this.Name, (object) this.UserSession.GetRelationType(this._TypeID).Description);
    }
  }

  public object[] GetPossibleValuesArray() => this._AttributeType.GetPossibleValuesArray();

  public override DataTable GetPossibleValues() => this._AttributeType.GetPossibleValues();

  public override DataRow[] GetPossibleValuesRows() => this._AttributeType.GetPossibleValuesRows();

  public override void DoSetPossibleValues(DataTable valuesTable)
  {
    this._AttributeType.SetPossibleValues(valuesTable, -1, this._TypeID);
  }

  public override UniqueValueModes UniqueMode
  {
    get => UniqueValueModes.NotUnique;
    set => throw new OperationNotApplicableException();
  }

  public AttributeTypeProperties PropertiesStructure
  {
    get
    {
      return new AttributeTypeProperties(this.AttributeID, this.Name, this.ShortName, this.Alias, this.Note, this.AttributeType, this.DefaultValue, this.MultipleValued, this.Computed, this.SizeType, this.Formula, this.UniqueMode, this.LevelID, this.LanguageID, this.SubjectAreas, this.GUID, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID);
    }
    set => throw new OperationNotApplicableException();
  }

  public Attribute4RelationTypeProperties Attribute4RelationPropertiesStructure
  {
    get
    {
      return new Attribute4RelationTypeProperties(this.AttributeID, this._TypeID, this.Required, this.ValidationRule, this.Computed, this.Formula, this.DefaultValue, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID)
      {
        FieldType = this.AttributeType
      };
    }
    set
    {
      if (this.AttributeID != value.AttributeID)
        throw new KernelExceptionID(sc_12411.ssp_appserver_12412(357069371), (object) value.AttributeID);
      if (this._TypeID != value.RelationType)
        throw new KernelExceptionID(sc_12411.ssp_appserver_12413(688453716), (object) value.RelationType);
      this.UserSession.StartTransaction();
      try
      {
        this.ValidationRule = value.ValidationRule;
        this.Computed = value.ComputeValueMode;
        this.Formula = value.Formula;
        if (value.DefaultValue == null)
          this.DefaultValue = (object) null;
        else
          this.DefaultValue = value.DefaultValue;
        this.Required = value.RequiredMode;
        this.OptimizationMode = value.OptimizationMode;
        this.IsContent = value.IsContent;
        this.Options = value.Options;
        this.Mask = value.Mask;
        this.MasterAttributeID = value.MasterAttributeID;
        this.SourceAttributeID = value.SourceAttributeID;
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  public override int MasterAttributeID
  {
    set
    {
      if (this.MasterAttributeID == value)
        return;
      string name;
      if (value > 0)
      {
        IDBAttributeType attributeType = this.UserSession.GetAttributeType(value, true);
        name = attributeType.Name;
        if (attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList)
          throw new KernelExceptionID(sc_12411.ssp_appserver_12414(1826930247), (object) name);
      }
      else
        name = LocalizationHolder.rm.GetString("Kernel_201");
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_202"), (object) this._AttributeType.Name, (object) name));
      this.CheckChangeEnable("F_MASTER_ID");
      try
      {
        if (value != 0)
        {
          if (this.UserSession.GetAttributeType(value, true).AttributeType != FieldTypes.ftObjectLink)
            throw new KernelExceptionID(sc_12411.ssp_appserver_12415(535943133));
          if (this.UserSession.GetRelationType(this._TypeID).Attributes.GetAttributeByID(value, false) == null)
            throw new KernelExceptionID(sc_12411.ssp_appserver_12416(1742568904), (object) this.UserSession.GetAttributeType(value, true).Name, (object) this.UserSession.GetRelationType(this._TypeID).Description);
        }
        else
          this.SourceAttributeID = 0;
        this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12411.ssp_appserver_12417(), (object) value, (object) this._TypeID, (object) this.AttributeID));
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_MASTER_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[172] = (object) value;
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_203"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public override int SourceAttributeID
  {
    set
    {
      if (this.SourceAttributeID == value)
        return;
      string str1 = value == 0 ? LocalizationHolder.rm.GetString("Kernel_204") : this.UserSession.GetAttributeType(value).Name;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_205"), (object) this._AttributeType.Name, (object) str1));
      this.CheckChangeEnable("F_SOURCE_ID");
      try
      {
        if (value != 0)
        {
          if (this.MasterAttributeID == 0)
            throw new KernelExceptionID(sc_12411.ssp_appserver_12418(585303328));
          IDBAttributeType attributeType = this.UserSession.GetAttributeType(value, true);
          if (attributeType.AttributeType != this.AttributeType)
            throw new KernelExceptionID(sc_12411.ssp_appserver_12419(83178249));
          this.ValidateAssign(attributeType);
        }
        this.UserSession.DataManager.ExecuteNonQuery($"UPDATE IMS_ATTR4RELATION_TYPES SET F_SOURCE_ID = {value} WHERE F_RELATION_TYPE = {this._TypeID} AND F_ATTRIBUTE_ID = {this.AttributeID}");
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_SOURCE_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[173] = (object) value;
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        string str2 = string.Format(LocalizationHolder.rm.GetString("Kernel_206"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str2);
        throw new KernelException(str2, ex);
      }
    }
  }

  public override RequiredModes Required
  {
    set
    {
      if (this.Required == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_207"), (object) this._AttributeType.Name, (object) EnumTypeHelper.GetCaption((Enum) value)));
      this.CheckChangeEnable("F_REQUIRED");
      this.UserSession.StartTransaction();
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"UPDATE IMS_ATTR4RELATION_TYPES SET F_REQUIRED = {Convert.ToInt32((object) value)} WHERE F_RELATION_TYPE = {this._TypeID} AND F_ATTRIBUTE_ID = {this.AttributeID}");
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_REQUIRED", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[84] = (object) Convert.ToInt32((object) value);
        if (value == RequiredModes.AutoRequired || value == RequiredModes.Auto)
        {
          foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.DataManager.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_RELATION_TYPE = :rt", this.UserSession.DataManager.Parameter("rt", (object) this._TypeID)).Rows)
          {
            IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row[0]), false);
            if (relation != null)
              (relation.Attributes as DBRelationAttributeCollection).AddAttribute(this.AttributeID, false, false);
          }
        }
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_208"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public override AttributeOptions Options
  {
    set
    {
      if (this.Options == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_209"), (object) this._AttributeType.Name, (object) AttributeOptionsHelper.GetCaptions(value)));
      this.CheckChangeEnableOptions(value);
      this.UserSession.StartTransaction();
      try
      {
        if ((value & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls && (this.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
          this._AttributeType.ValidateNotNull(-1, this._TypeID);
        this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12411.ssp_appserver_12420(), (object) Convert.ToInt32((object) value), (object) this._TypeID, (object) this.AttributeID));
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_OPTIONS", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[36] = (object) Convert.ToInt32((object) value);
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_210"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public override string Mask
  {
    get => this.AttributeID == -24 ? Consts.OnlyDateFunction : base.Mask;
    set
    {
      if (!(this.Mask != value))
        return;
      long EventID = this.ValidateEditMode(value != string.Empty ? string.Format(LocalizationHolder.rm.GetString("Kernel_211"), (object) this._AttributeType.Name, (object) value) : string.Format(LocalizationHolder.rm.GetString("Kernel_212"), (object) this._AttributeType.Name));
      this.CheckChangeEnable("F_MASK");
      this.UserSession.StartTransaction();
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12411.ssp_appserver_12421(), (object) this._TypeID, (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_MASK", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[35] = (object) value;
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_213"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  protected virtual string SaveRuleFormulaLinks(string newValue)
  {
    string newFormula = this._AttributeType.TransposeFormula(newValue, Consts.Attribute4ValidationRule);
    this._AttributeType.SaveFormulaLinks(-1, this._TypeID, newFormula, Consts.Attribute4ValidationRule, true);
    return newFormula;
  }

  public override string ValidationRule
  {
    set
    {
      if (!(this.ValidationRule != value))
        return;
      long EventID = this.ValidateEditMode(value != string.Empty ? string.Format(LocalizationHolder.rm.GetString("Kernel_214"), (object) this._AttributeType.Name, (object) value) : string.Format(LocalizationHolder.rm.GetString("Kernel_215"), (object) this._AttributeType.Name));
      this.CheckChangeEnable("F_VALIDATION_RULE");
      this.UserSession.StartTransaction();
      try
      {
        value = this.SaveRuleFormulaLinks(value);
        this.UserSession.DataManager.ExecuteNonQuery($"UPDATE IMS_ATTR4RELATION_TYPES SET F_VALIDATION_RULE = :val WHERE F_RELATION_TYPE = {this._TypeID} AND F_ATTRIBUTE_ID = {this.AttributeID}", this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_VALIDATION_RULE", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[83] = (object) value;
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_216"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public override string Formula
  {
    set
    {
      if (!(this.Formula != value))
        return;
      long EventID = this.ValidateEditMode(value != string.Empty ? string.Format(LocalizationHolder.rm.GetString("Kernel_217"), (object) this._AttributeType.Name, (object) value) : string.Format(LocalizationHolder.rm.GetString("Kernel_218"), (object) this._AttributeType.Name));
      this.CheckChangeEnable("F_FORMULA");
      this.UserSession.StartTransaction();
      try
      {
        value = this._AttributeType.TransposeFormula(value, Consts.Attribute4Formula);
        this._AttributeType.SaveFormulaLinks(-1, this._TypeID, value, Consts.Attribute4Formula, true);
        this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12411.ssp_appserver_12422(), (object) this._TypeID, (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_FORMULA", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[73] = (object) value;
        if (this.Computed == ComputeValueModes.StoredValue || this.Computed == ComputeValueModes.IndexValue)
          this._AttributeType.RecomputeValues(-1, this._TypeID);
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_219"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        if (!(ex is AccessDeniedException))
          throw new KernelException(str, ex);
        throw;
      }
    }
  }

  public override bool IsContent
  {
    set
    {
      if (this.IsContent == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_220"), (object) this._AttributeType.Name, (object) Consts.ConvertBoolToString(value)));
      this.CheckChangeEnable("F_CONTENT");
      this.UserSession.StartTransaction();
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12411.ssp_appserver_12423(), (object) this._TypeID, (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) (value ? 1 : 0)));
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_CONTENT", (object) (value ? 1 : 0), (IUserSession) this.UserSession);
        this.paramsTable[39] = (object) (value ? 1 : 0);
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
        throw;
      }
    }
  }

  public override ComputeValueModes Computed
  {
    set
    {
      if (this.Computed == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_221"), (object) this._AttributeType.Name, (object) ComputeValueModesHelper.GetCaption(value)));
      this.CheckChangeEnable("F_COMPUTED");
      this.UserSession.StartTransaction();
      try
      {
        this._AttributeType.CheckJITValue(value);
        if ((value == ComputeValueModes.JITValue || value == ComputeValueModes.StoredValue || value == ComputeValueModes.IndexValue) && !this._AttributeType.ComputableAttribute)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12411.ssp_appserver_12424()), (object) this._AttributeType.TypeCaption));
        this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12411.ssp_appserver_12425(), (object) Convert.ToInt32((object) value), (object) this._TypeID, (object) this.AttributeID));
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_COMPUTED", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[107] = (object) Convert.ToInt32((object) value);
        if (value == ComputeValueModes.StoredValue || value == ComputeValueModes.IndexValue)
          this._AttributeType.RecomputeValues(-1, this._TypeID);
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_223"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        if (!(ex is AccessDeniedException))
          throw new KernelException(str, ex);
        throw;
      }
    }
  }

  private void PrepareView(OptimizationModes newMode)
  {
    DBRelationType relationType = this.UserSession.GetRelationType(this._TypeID) as DBRelationType;
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable($"SELECT * FROM IMS_ATTR4RELATION_TYPES WHERE F_RELATION_TYPE = {this._TypeID} AND F_ATTRIBUTE_ID <> {this.AttributeID} AND F_INVIEW <> {0}");
    if (newMode == OptimizationModes.Write)
    {
      if (dataTable.Rows.Count != 0)
        return;
      try
      {
        dataManager.SetAdminCommandTimeout();
        dataManager.DataProvider.DropTableIfExists(dataManager, relationType.ViewName);
      }
      catch
      {
      }
      finally
      {
        dataManager.SetNormalCommandTimeout();
      }
    }
    else
    {
      if (dataTable.Rows.Count != 0)
        return;
      try
      {
        dataManager.SetAdminCommandTimeout();
        dataManager.DataProvider.DropTableIfExists(dataManager, relationType.ViewName);
        List<string> indexesList = new List<string>();
        dataManager.DataProvider.CreateRelationTypeView(relationType.ViewName, string.Empty, dataManager, indexesList);
        foreach (string commandText in indexesList)
          dataManager.ExecuteNonQuery(commandText);
        relationType.InsertIntoView();
      }
      finally
      {
        dataManager.SetNormalCommandTimeout();
      }
    }
  }

  public OptimizationModes OptimizationMode
  {
    get => (OptimizationModes) Convert.ToInt32(this.paramsTable[44]);
    set
    {
      if (this.OptimizationMode == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_224"), (object) this._AttributeType.Name, (object) OptimizationModesHelper.GetCaption(value)));
      this.CheckChangeEnable("F_INVIEW");
      this.UserSession.StartTransaction();
      try
      {
        IDbManager dataManager = this.UserSession.DataManager;
        this.PrepareView(value);
        string[] indexFieldNames = this._AttributeType.IndexFieldNames;
        string[] fieldNames = this._AttributeType.FieldNames;
        DBRelationType relationType = this.UserSession.GetRelationType(this._TypeID) as DBRelationType;
        if (this.OptimizationMode == OptimizationModes.Seek)
        {
          foreach (string fldName in indexFieldNames)
          {
            try
            {
              dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropIndexSQL(relationType.ViewName, fldName, SortOrders.ASC));
            }
            catch (Exception ex)
            {
              this.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_225"), (object) dataManager.DataProvider.GetDropIndexSQL(relationType.ViewName, fldName, SortOrders.ASC), (object) ex.Message), Consts.traceAlways, "sql_errors.log");
            }
            try
            {
              dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropIndexSQL(relationType.ViewName, fldName, SortOrders.DESC));
            }
            catch (Exception ex)
            {
              this.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_226"), (object) dataManager.DataProvider.GetDropIndexSQL(relationType.ViewName, fldName, SortOrders.DESC), (object) ex.Message), Consts.traceAlways, "sql_errors.log");
            }
          }
        }
        if (value == OptimizationModes.Write)
        {
          foreach (string columnName in fieldNames)
            dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropColumnsSQL(relationType.ViewName, columnName));
        }
        else if (this.OptimizationMode == OptimizationModes.Write)
        {
          dataManager.ExecuteNonQuery(dataManager.DataProvider.GetAddColumnsSQL(relationType.ViewName, this._AttributeType.ColumnSQL));
          this._AttributeType.UpdateViewFields(relationType.ViewName, "IMS_RELATION_ATTRS", fieldNames, "F_PRJLINK_ID");
        }
        if (value == OptimizationModes.Seek)
        {
          foreach (string fldName in indexFieldNames)
            dataManager.ExecuteNonQuery(dataManager.DataProvider.GetIndexSQL(relationType.ViewName, fldName, SortOrders.ASC));
        }
        dataManager.ExecuteNonQuery(string.Format(sc_12411.ssp_appserver_12426(), (object) Convert.ToInt32((object) value), (object) this._TypeID, (object) this.AttributeID));
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_INVIEW", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[44] = (object) Convert.ToInt32((object) value);
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
        this.UserSession.DBCache.EnterReadLocker();
        try
        {
          (this.UserSession.DBCache as CacheDataset).FillAttributeID4RelationHash(this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES"), dataManager);
        }
        finally
        {
          this.UserSession.DBCache.ExitReadLocker();
        }
      }
      catch (Exception ex)
      {
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_227"), (object) this._AttributeType.Name, (object) ex.Message);
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        if (!(ex is AccessDeniedException))
          throw new KernelException(str, ex);
        throw;
      }
    }
  }

  public override object DefaultValue
  {
    set
    {
      if (this._AttributeType.CompareValues(this.DefaultValue, value))
        return;
      if (this.AttributeType == FieldTypes.ftDouble && value != null && value.ToString() != string.Empty)
        value = (object) Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
      long EventID = this.ValidateEditMode(value == null || !(value.ToString() != string.Empty) ? string.Format(LocalizationHolder.rm.GetString("Kernel_229"), (object) this._AttributeType.Name) : string.Format(LocalizationHolder.rm.GetString("Kernel_228"), (object) this._AttributeType.Name, (object) value.ToString()));
      this.CheckChangeEnable("F_DEFAULT_VALUE");
      try
      {
        this._AttributeType.ValidateDefaultValue(value);
        string str = "";
        if (value != null)
          str = Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
        this.UserSession.DataManager.ExecuteNonQuery($"UPDATE IMS_ATTR4RELATION_TYPES SET F_DEFAULT_VALUE = {SqlHelper.QString(str)} WHERE F_RELATION_TYPE = {this._TypeID} AND F_ATTRIBUTE_ID = {this.AttributeID}");
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_RELATION_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4RELATION_TYPES", "F_DEFAULT_VALUE", (object) str, (IUserSession) this.UserSession);
        this.paramsTable[104] = (object) str;
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_230"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  protected override void DoDelete(long DeleteMode)
  {
    if (!this.UserSession.CanChangeObjectElement(6, (object) this._TypeID, ObligatoryElementKeys.GetKeyForAttributePresence(this._AttributeType.AttributeID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_908"), (object) this.Name));
    DBRelationType relationType = this.UserSession.GetRelationType(this._TypeID) as DBRelationType;
    IDbManager dataManager = this.UserSession.DataManager;
    if (this._AttributeType.AttributeType == FieldTypes.ftObjectLink)
    {
      DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES").Select($"F_MASTER_ID = {this.AttributeID} AND F_RELATION_TYPE = {this._TypeID}");
      if (dataRowArray.Length != 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (DataRow dataRow in dataRowArray)
          stringBuilder.AppendFormat("{0}'{1}', ", (object) stringBuilder, (object) this.UserSession.GetAttributeType(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"])).Name);
        stringBuilder.Length -= 2;
        throw new KernelExceptionID(sc_12411.ssp_appserver_12427(1078207691), (object) stringBuilder.ToString());
      }
    }
    if ((DeleteMode & (long) Consts.DeleteInstances) == (long) Consts.DeleteInstances)
    {
      if (this.IsContent)
        throw new KernelExceptionID(sc_12411.ssp_appserver_12428(668969650), (object) this.Name);
      string commandText = this.Required != RequiredModes.AutoRequired ? "SELECT R.F_PRJLINK_ID FROM IMS_RELATIONS R, IMS_RELATION_ATTRS A WHERE R.F_RELATION_TYPE = :rt AND A.F_PRJLINK_ID = R.F_PRJLINK_ID AND A.F_ATTRIBUTE_ID = " + this.AttributeID.ToString() : "SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_RELATION_TYPE = :rt";
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable(commandText, dataManager.Parameter("rt", (object) this._TypeID)).Rows)
      {
        IDBAttribute attributeById = this.UserSession.GetRelation(Convert.ToInt64(row[0])).GetAttributeByID(this.AttributeID);
        if (attributeById != null)
          (attributeById as DBAttribute).Purge(false);
      }
    }
    else if (!relationType.AnyAttributes)
    {
      DataTable dataTable = dataManager.ExecuteDataTable(string.Format(sc_12411.ssp_appserver_12429(), (object) this._TypeID, (object) this.AttributeID));
      if (dataTable.Rows.Count > 0)
      {
        long[] relationsID = new long[dataTable.Rows.Count];
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          relationsID[index] = Convert.ToInt64(dataTable.Rows[index][0]);
        throw new RelationsFoundException(string.Format(sc_12411.ssp_appserver_12430(), (object) this.Name, (object) dataTable.Rows.Count, (object) relationType.Description), $"Связи с атрибутом '{this.Name}':", relationsID);
      }
    }
    base.DoDelete(DeleteMode);
    if (this.OptimizationMode == OptimizationModes.Write)
      return;
    foreach (string indexFieldName in this._AttributeType.IndexFieldNames)
    {
      try
      {
        dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropIndexSQL(relationType.ViewName, indexFieldName, SortOrders.ASC));
      }
      catch (Exception ex)
      {
        this.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_231"), (object) dataManager.DataProvider.GetDropIndexSQL(relationType.ViewName, indexFieldName, SortOrders.ASC), (object) ex.Message), Consts.traceAlways, "sql_errors.log");
      }
    }
    foreach (string fieldName in this._AttributeType.FieldNames)
      dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropColumnsSQL(relationType.ViewName, fieldName));
    (this.UserSession.DBCache as CacheDataset).FillAttributeID4RelationHash(this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES"), dataManager);
    if (this.UserSession.DBCache.GetUpdateTables(-1, -1, this._TypeID) != null)
      return;
    try
    {
      dataManager.SetAdminCommandTimeout();
      dataManager.DataProvider.DropTableIfExists(dataManager, relationType.ViewName);
    }
    catch
    {
    }
    finally
    {
      dataManager.SetNormalCommandTimeout();
    }
  }
}
