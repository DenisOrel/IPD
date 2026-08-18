// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributeType4Category
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;


namespace Intermech.Kernel;

internal abstract class DBAttributeType4Category : 
  DBSessionable,
  IDeletable,
  IDBGuid,
  IDBSubjectArea,
  IDBLanguage,
  IDBSecurity
{
  protected DBAttributeType _AttributeType;
  protected int _TypeID;
  protected string _TableName;
  protected string _KeyName;
  private int _CategoryType4ChangesGuard;
  internal bool AutoPatchMode;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(7);

  public DBAttributeType4Category(
    UserSession uSession,
    int attributeID,
    int typeID,
    int categoryType4ChangesGuard)
    : base(uSession)
  {
    this._AttributeType = uSession.GetAttributeType(attributeID) as DBAttributeType;
    this._TypeID = typeID;
    this._CategoryType4ChangesGuard = categoryType4ChangesGuard;
    this.InitSecurityOptions(3, (long) attributeID);
  }

  static DBAttributeType4Category()
  {
    DBAttributeType4Category.metadataActions.Add(ActionType.GetAccess, false);
    DBAttributeType4Category.metadataActions.Add(ActionType.SetAccess, false);
    DBAttributeType4Category.metadataActions.Add(ActionType.EditProperties, false);
    DBAttributeType4Category.metadataActions.Add(ActionType.Delete, false);
    DBAttributeType4Category.metadataActions.Add(ActionType.Write, true);
    DBAttributeType4Category.metadataActions.Add(ActionType.List, true);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBAttributeType4Category.metadataActions);
  }

  protected abstract int CategoryType4EvenLog { get; }

  public override bool CheckAccess(
    ActionType anAction,
    bool aDefaultAccess,
    CheckAccessFlags flags)
  {
    return this._AttributeType.CheckAccess(anAction, aDefaultAccess, flags);
  }

  protected virtual long ValidateEditMode(string note)
  {
    this.AddEvent(0L, 0L, (long) this._TypeID, this.CategoryType4EvenLog, ActionType.EditProperties, EventlogRecordType.AccessDenied, note);
    this.CheckAccess(ActionType.EditProperties);
    return this._LastEventID;
  }

  protected abstract DBAttributableType ParentType { get; }

  protected virtual void DoDelete(long DeleteMode)
  {
    string condition = $"F_ATTRIBUTE_ID = {this._AttributeType.AttributeID} AND {this._KeyName} = {this._TypeID}";
    this.UserSession.DataManager.ExecuteNonQuery($"DELETE FROM {this._TableName} WHERE {condition}");
    this.UserSession.DBCache.DeleteRecords(this._TableName, condition, (IUserSession) this.UserSession);
  }

  public int Delete(long DeleteMode)
  {
    long EventID = this.ParentType.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_153"), (object) this._AttributeType.Name));
    this.CheckAccess(ActionType.EditProperties);
    this.UserSession.StartTransaction();
    try
    {
      this.DoDelete(DeleteMode);
      this.ParentType.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      string str = string.Format(LocalizationHolder.rm.GetString("Kernel_154"), (object) this.Name, (object) ex.Message);
      this.UserSession.Rollback();
      this.CloseEvent(EventID, EventlogRecordType.Error, str);
      throw new KernelException(str, ex);
    }
    return 0;
  }

  public RelationalOperators[] GetEnabledOperators(ColumnContents content)
  {
    return this._AttributeType.GetEnabledOperators(content);
  }

  public void ValidateAssign(IDBAttributeType source) => this._AttributeType.ValidateAssign(source);

  public int[] GetGroupsList() => this._AttributeType.GetGroupsList();

  public string[] FieldNames => this._AttributeType.FieldNames;

  public bool ComputableAttribute => this._AttributeType.ComputableAttribute;

  public bool IsCompatibleType(FieldTypes newType) => this._AttributeType.IsCompatibleType(newType);

  public virtual string SizeTypeDescription => this._AttributeType.SizeTypeDescription;

  public virtual string DefaultValueDescription => this._AttributeType.DefaultValueDescription;

  public virtual string ValidationRule
  {
    get
    {
      object obj = this.paramsTable[83];
      return obj == null ? "" : obj.ToString();
    }
    set
    {
    }
  }

  public virtual int LevelID
  {
    get => this._AttributeType.LevelID;
    set => throw new OperationNotApplicableException();
  }

  public FieldTypes AttributeType
  {
    get => this._AttributeType.AttributeType;
    set => throw new OperationNotApplicableException();
  }

  public string ShortName
  {
    get => this._AttributeType.ShortName;
    set => throw new OperationNotApplicableException();
  }

  public MultiValueModes MultipleValued
  {
    get => this._AttributeType.MultipleValued;
    set => throw new OperationNotApplicableException();
  }

  public abstract UniqueValueModes UniqueMode { set; get; }

  public int AttributeID => this._AttributeType.AttributeID;

  public abstract DataTable GetPossibleValues();

  public abstract DataRow[] GetPossibleValuesRows();

  public virtual string Formula
  {
    get
    {
      object obj = this.paramsTable[73];
      return obj == DBNull.Value ? "" : Convert.ToString(obj);
    }
    set
    {
    }
  }

  public virtual bool IsContent
  {
    get => Convert.ToInt32(this.paramsTable[39]) == 1;
    set
    {
    }
  }

  public long SizeType
  {
    get => this._AttributeType.SizeType;
    set => throw new OperationNotApplicableException();
  }

  public abstract void DoSetPossibleValues(DataTable valuesTable);

  public void SetNewPossibleValues(DataTable valuesTable)
  {
    this._AttributeType.SetNewPossibleValues(valuesTable);
  }

  public void SetPossibleValues(DataTable valuesTable)
  {
    long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_155"), (object) this.Name));
    try
    {
      this.DoSetPossibleValues(valuesTable);
    }
    catch (Exception ex)
    {
      string str = string.Format(LocalizationHolder.rm.GetString("Kernel_156"), (object) this.Name, (object) ex.Message);
      this.CloseEvent(EventID, EventlogRecordType.Error, str);
      throw new KernelException(str, ex);
    }
  }

  public string Name
  {
    get => this._AttributeType.Name;
    set => throw new OperationNotApplicableException();
  }

  public virtual RequiredModes Required
  {
    get => (RequiredModes) Convert.ToInt32(this.paramsTable[84]);
    set
    {
    }
  }

  public virtual object DefaultValue
  {
    get
    {
      object obj = this.paramsTable[104];
      if (obj == DBNull.Value || obj == null || obj.ToString() == "")
        return (object) DBNull.Value;
      return this.AttributeType == FieldTypes.ftDouble ? (object) Convert.ToDouble(obj, (IFormatProvider) CultureInfo.InvariantCulture) : obj;
    }
    set
    {
    }
  }

  public virtual ComputeValueModes Computed
  {
    get => (ComputeValueModes) Convert.ToInt32(this.paramsTable[107]);
    set
    {
    }
  }

  public string Alias
  {
    get => this._AttributeType.Alias;
    set => throw new OperationNotApplicableException();
  }

  public void ValidateSizeType(long newValue) => this._AttributeType.ValidateSizeType(newValue);

  public string Note
  {
    get => this._AttributeType.Note;
    set => throw new OperationNotApplicableException();
  }

  public bool IsGridable => this._AttributeType.IsGridable;

  public string TextFieldName => this._AttributeType.TextFieldName;

  public string ValueFieldName => this._AttributeType.ValueFieldName;

  public string PossibleValueFieldName => this._AttributeType.PossibleValueFieldName;

  public RelationalOperators[] EnabledOperators => this._AttributeType.EnabledOperators;

  public virtual AttributeOptions Options
  {
    get => (AttributeOptions) Convert.ToInt32(this.paramsTable[36]);
    set
    {
    }
  }

  public virtual string Mask
  {
    get => this.paramsTable[35].ToString();
    set
    {
    }
  }

  public virtual int MasterAttributeID
  {
    get => Convert.ToInt32(this.paramsTable[172]);
    set
    {
    }
  }

  public virtual int SourceAttributeID
  {
    get => Convert.ToInt32(this.paramsTable[173]);
    set
    {
    }
  }

  public bool IsSystemGUID => this._AttributeType.IsSystemGUID;

  public Guid GUID => this._AttributeType.GUID;

  public string SubjectAreas
  {
    get => this._AttributeType.SubjectAreas;
    set => throw new OperationNotApplicableException();
  }

  public string SubjectAreasCaption
  {
    get => this.UserSession.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
  }

  public string LanguageName
  {
    get => this._AttributeType.LanguageName;
    set => throw new OperationNotApplicableException();
  }

  public string LanguageID
  {
    get => this._AttributeType.LanguageID;
    set => throw new OperationNotApplicableException();
  }

  public bool IsDefaultLanguage
  {
    get => this._AttributeType.IsDefaultLanguage;
    set => throw new OperationNotApplicableException();
  }

  public void SetGUID(Guid guid) => throw new OperationNotApplicableException();

  protected bool CheckChangeEnable(string propertyID, bool throwException)
  {
    int num = this.UserSession.CanChangeObjectElement(this._CategoryType4ChangesGuard, (object) this._TypeID, ObligatoryElementKeys.GetKeyForAttributeProperty(this._AttributeType.AttributeID, propertyID)) ? 1 : 0;
    if (!(num == 0 & throwException))
      return num != 0;
    throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_926"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  protected bool CheckChangeEnable(string propertyID) => this.CheckChangeEnable(propertyID, true);

  protected bool CheckChangeEnableOptions(AttributeOptions value)
  {
    return this.CheckChangeEnableOptions(value, true);
  }

  protected bool CheckChangeEnableOptions(AttributeOptions value, bool throwException)
  {
    foreach (AttributeOptions optionsFlag in (AttributeOptions[]) Enum.GetValues(typeof (AttributeOptions)))
    {
      if ((value & optionsFlag) != (this.Options & optionsFlag) && !this.UserSession.CanChangeObjectElement(this._CategoryType4ChangesGuard, (object) this._TypeID, ObligatoryElementKeys.GetKeyForAttributeOptionsFlag(this._AttributeType.AttributeID, (int) optionsFlag)))
      {
        if (throwException)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_906"), (object) EnumDescConverter.GetEnumDescription((Enum) optionsFlag)));
        return false;
      }
    }
    return true;
  }
}
