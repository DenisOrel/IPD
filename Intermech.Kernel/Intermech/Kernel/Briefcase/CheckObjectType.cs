// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckObjectType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.LifeCycles;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckObjectType(
  UserSession session,
  DataSet metaData,
  DataRow briefRow,
  CheckOptions options) : CheckAttributableType<IDBObjectType, DataRow>(session, metaData, 4, briefRow, options)
{
  public override void Initialize()
  {
    Guid guid = new Guid(Convert.ToString(this.briefRow["F_GUID"]));
    this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatGUID, (object) guid);
    this.item = this.session.GetObjectType(guid, false);
    string str = Convert.ToString(this.briefRow["F_OBJ_TYPE_NAME"]);
    if (this.item == null)
    {
      if (this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeNotFound, Helper.ValueToLog((object) str, (object) guid, true), string.Empty);
      if (this.session.GetObjectType(str, false) == null || !this.synhronizingError)
        return;
      this.AddErrorToLog("В базе назначения уже существует тип объектов с таким наименованием", str);
    }
    else
    {
      this.isSystemGUID = (this.item as IDBGuid).IsSystemGUID;
      if (this.item.ObjectTypeName != str)
      {
        if (this.session.GetObjectType(str, false) == null || !this.synhronizingError)
          return;
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeNamePresent, str);
      }
      else
        this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatName, (object) this.item.ObjectTypeName);
    }
  }

  protected override void OnCheck()
  {
    if (this.synhronizingError)
    {
      if (!CheckHelper.CompareString(this.briefRow, "F_OBJ_TYPE_NAME", this.item.ObjectTypeName))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logName, Convert.ToString(this.briefRow["F_OBJ_TYPE_NAME"]), this.item.ObjectTypeName);
      if (!CheckHelper.CompareString(this.briefRow, "F_OBJ_NAME", this.item.ObjectInstanceName))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logObjectTypeInstanceName, Convert.ToString(this.briefRow["F_OBJ_NAME"]), this.item.ObjectInstanceName);
      if (!CheckHelper.CompareString(this.briefRow, "F_SHORT_NAME", this.item.ObjectTypeShortName))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeShortName, Convert.ToString(this.briefRow["F_SHORT_NAME"]), this.item.ObjectTypeShortName);
      if (!CheckHelper.CheckIcons(this.briefRow, this.item.Icon))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logObjectTypeIcon);
      if (!CheckHelper.CompareString(this.briefRow, "F_NOTE", this.item.Note))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logAttributeNote, Convert.ToString(this.briefRow["F_NOTE"]), this.item.Note);
      if (!this.CheckPublicLC(this.briefRow, this.item.PublicLC))
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logObjectTypePublicLC, EnumDescConverter.GetEnumDescription((Enum) (InheritModes) Convert.ToInt32(this.briefRow["F_PUBLIC_LC"])), EnumDescConverter.GetEnumDescription((Enum) this.item.PublicLC));
      if (!CheckHelper.CheckArea(this.session, this.metaData, this.briefRow, (this.item as IDBSubjectArea).SubjectAreas))
      {
        IDBSubjectAreaCollection subjectAreaCollection = this.session.GetSubjectAreaCollection();
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logObjectTypeSubjectAreas, subjectAreaCollection.GetAreasCaption(Helper.GetConformitySubjectAreas((IUserSession) this.session, this.metaData, Convert.ToString(this.briefRow["F_AREA_ID"]))), subjectAreaCollection.GetAreasCaption((this.item as IDBSubjectArea).SubjectAreas));
      }
      InheritModes int32 = (InheritModes) Convert.ToInt32(this.briefRow["F_PUBLIC_LC"]);
      if (this.item.PublicLC != InheritModes.Inherited && int32 == InheritModes.Inherited && this.session.ObjectsSelect(this.item.ObjectType, new DBRecordSetParams((ConditionStructure[]) null)).Rows.Count > 0)
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeLCStepScheme, EnumDescConverter.GetEnumDescription((Enum) int32), EnumDescConverter.GetEnumDescription((Enum) this.item.PublicLC));
    }
    CheckResult checkResult1 = this.CheckVersionModes(this.briefRow, this.item.Versionable);
    if (checkResult1 == CheckResult.Error || checkResult1 == CheckResult.ErrorSinhronize && this.synhronizingError || checkResult1 == CheckResult.ErrorNotSinhronize && this.noneSynhronizingError)
      this.AddErrorToLog(BriefcaseConsts.logObjectTypeVersionMode, EnumDescConverter.GetEnumDescription((Enum) (ObjectVersionModes) Convert.ToInt32(this.briefRow["F_VERSIONABLE"])), EnumDescConverter.GetEnumDescription((Enum) this.item.Versionable));
    CheckResult checkResult2 = this.CheckDefaultRelation(this.item.DefaultRelation);
    if (checkResult2 != CheckResult.Equal)
    {
      DataRow dataRow = this.metaData.Tables["IMS_RELATION_TYPES"].Rows.Find(this.briefRow["F_DEFAULT_RELATION"]);
      string briefValue = dataRow != null ? Helper.ValueToLog(dataRow["F_DESCRIPTION"], dataRow["F_GUID"], true) : string.Empty;
      if (checkResult2 == CheckResult.NotFound && this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logRelationTypeNotFound, briefValue, string.Empty);
      if (checkResult2 == CheckResult.NotEqual)
      {
        IDBRelationType relationType = this.session.GetRelationType(this.item.DefaultRelation, false);
        this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logObjectTypeDefaultRelation, briefValue, relationType != null ? Helper.ValueToLog((object) relationType.Description, (object) (relationType as IDBGuid).GUID, true) : string.Empty);
      }
    }
    CheckResult checkResult3 = this.CheckParentType(this.item.ParentTypeID);
    if (checkResult3 != CheckResult.Equal)
    {
      string dbValue1 = LocalizationHolder.rm.GetString("Kernel_281");
      DataRow[] dataRowArray = this.metaData.Tables["IMS_OBJTYPES_TREE"].Select($"{"F_OBJECT_TYPE"}={Convert.ToInt32(this.briefRow["F_OBJECT_TYPE"])}");
      string empty1 = string.Empty;
      string briefValue;
      if (dataRowArray.Length != 0)
      {
        DataRow dataRow = this.metaData.Tables["IMS_OBJECT_TYPES"].Rows.Find(dataRowArray[0]["F_PARENT_ID"]);
        briefValue = Helper.ValueToLog(dataRow["F_OBJ_TYPE_NAME"], dataRow["F_GUID"], true);
      }
      else
        briefValue = dbValue1;
      if (checkResult3 == CheckResult.NotFound && this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeParentTypeNotFound, briefValue, dbValue1);
      if (checkResult3 == CheckResult.NotEqual)
      {
        string empty2 = string.Empty;
        string dbValue2;
        if (this.item.ParentTypeID > 0)
        {
          IDBObjectType objectType = this.session.GetObjectType(this.item.ParentTypeID, false);
          dbValue2 = objectType != null ? Helper.ValueToLog((object) objectType.ObjectTypeName, (object) (objectType as IDBGuid).GUID, true) : string.Empty;
        }
        else
          dbValue2 = dbValue1;
        if (this.noneSynhronizingError)
          this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logObjectTypeParentType, briefValue, dbValue2);
        else
          this.AddErrorToLog(BriefcaseConsts.logObjectTypeParentType, briefValue, dbValue2);
      }
    }
    if (this.noneSynhronizingError)
    {
      CheckArraysResult checkArraysResult = this.CheckChild(this.item);
      if (checkArraysResult["notFoundInDB"].Count > 0)
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeChildTypeNotFound, checkArraysResult.ToString("notFoundInDB"), string.Empty);
      if (checkArraysResult["notFoundInDBObjectType"].Count > 0)
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeChildTypeNotEqual, checkArraysResult.ToString("notFoundInDBObjectType"), string.Empty);
    }
    CheckResult checkResult4 = this.CheckСаptionAttribute(this.item.CaptionAttribute);
    if (checkResult4 != CheckResult.Equal)
    {
      DataRow dataRow = this.metaData.Tables["IMS_ATTRIBUTES"].Rows.Find(this.briefRow["F_CAPTION_ATTRIBUTE"]);
      string briefValue = dataRow != null ? Helper.ValueToLog(dataRow["F_NAME"], dataRow["F_GUID"], true) : string.Empty;
      if (checkResult4 == CheckResult.ErrorSinhronize && this.synhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeСаptionNotValidAttribute, briefValue, string.Empty);
      if (checkResult4 == CheckResult.NotFound && this.noneSynhronizingError)
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeСаptionAttributeNotFound, briefValue, string.Empty);
      if (checkResult4 == CheckResult.NotEqual && this.noneSynhronizingError)
      {
        IDBAttributeType attributeType = this.session.GetAttributeType(this.item.CaptionAttribute, false);
        string dbValue = attributeType != null ? Helper.ValueToLog((object) attributeType.Name, (object) (attributeType as IDBGuid).GUID, true) : string.Empty;
        this.AddErrorToLog(BriefcaseConsts.logObjectTypeСаptionAttribute, briefValue, dbValue);
      }
    }
    if (this.noneSynhronizingError)
    {
      IDBLCSchema lcSchema = this.session.GetLCSchema(this.item.SchemaID);
      DataRow dataRow = this.metaData.Tables["IMS_LC_SCHEMAS"].Rows.Find((object) Convert.ToInt32(this.briefRow["F_SCHEMA_ID"]));
      if (lcSchema != null && dataRow != null)
      {
        Guid guid = new Guid(Convert.ToString(dataRow["F_GUID"]));
        if (!guid.Equals(lcSchema.GUID))
        {
          string objectTypeLcScheme = BriefcaseConsts.logObjectTypeLCScheme;
          string briefValue = Convert.ToString(dataRow["F_NAME"]);
          guid = lcSchema.GUID;
          string dbValue = guid.ToString();
          this.AddWarningToLog(objectTypeLcScheme, briefValue, dbValue);
        }
      }
    }
    CheckResult checkResult5 = CheckHelper.CheckAnyAttributes(this.briefRow, this.item.AnyAttributes);
    if (checkResult5 == CheckResult.Error)
      this.AddErrorToLog(BriefcaseConsts.logObjectTypeAnyAttributes, Convert.ToBoolean(this.briefRow["F_ANY_ATTRIBUTES"]) ? Consts.YesValue : Consts.NoValue, this.item.AnyAttributes ? Consts.YesValue : Consts.NoValue);
    if (checkResult5 == CheckResult.Warning && this.synhronizingError)
      this.AddInfoInLog(this.isSystemGUID ? CheckMetadataLogItemType.WarningSystem : CheckMetadataLogItemType.Warning, BriefcaseConsts.logObjectTypeAnyAttributes, Convert.ToBoolean(this.briefRow["F_ANY_ATTRIBUTES"]) ? Consts.YesValue : Consts.NoValue, this.item.AnyAttributes ? Consts.YesValue : Consts.NoValue);
    this.CheckAttributes();
    this.CheckRelations();
  }

  protected override DataRow[] GetTypeAttributes()
  {
    return this.metaData.Tables["IMS_ATTR4OBJ_TYPES"].Select($"{"F_OBJECT_TYPE"}={Convert.ToInt32(this.briefRow["F_OBJECT_TYPE"])}");
  }

  protected override List<string> GetObligatoryAttributes()
  {
    List<string> obligatoryAttributes = new List<string>();
    foreach (ObligatoryObjectAttributes objectAttributes in Enum.GetValues(typeof (ObligatoryObjectAttributes)))
    {
      if (ObligatoryObjectAttributesHelper.GetAttributeSourceType(objectAttributes) == AttributeSourceTypes.Object && ObligatoryObjectAttributesHelper.CanUseInFormula(objectAttributes))
        obligatoryAttributes.Add(MetaDataHelper.GetAttributeTypeName((int) objectAttributes));
    }
    return obligatoryAttributes;
  }

  protected override int CheckAttribute(
    DataRow attrRow,
    IDictionary<string, bool> formulaAttributes)
  {
    CheckAttribute4ObjectType attribute4ObjectType = new CheckAttribute4ObjectType(this.session, this.item, formulaAttributes, this.metaData, attrRow, this.options);
    attribute4ObjectType.Initialize();
    attribute4ObjectType.Check();
    if (attribute4ObjectType.AttributeID != 0 && attribute4ObjectType.Log.Count > 0)
      this.infoLog.AddRange((IEnumerable<CheckMetadataLogItem>) attribute4ObjectType.Log);
    return attribute4ObjectType.AttributeID;
  }

  protected override void CheckAttributesCollection(List<int> presentAttributes)
  {
    CheckAttribute4ObjectTypeCollection objectTypeCollection = new CheckAttribute4ObjectTypeCollection(this.session, presentAttributes, this.item, this.UniIdentifiler, this.options);
    objectTypeCollection.Compare();
    if (objectTypeCollection.Log.Count <= 0)
      return;
    this.infoLog.AddRange((IEnumerable<CheckMetadataLogItem>) objectTypeCollection.Log);
  }

  private CheckResult CheckDefaultRelation(int defaultRelation)
  {
    if (this.briefRow["F_DEFAULT_RELATION"] != null || Convert.ToString(this.briefRow["F_DEFAULT_RELATION"]) != string.Empty)
    {
      DataRow dataRow = this.metaData.Tables["IMS_RELATION_TYPES"].Rows.Find(this.briefRow["F_DEFAULT_RELATION"]);
      if (dataRow != null)
      {
        IDBRelationType dbRelationType = this.session.GetRelationType(new Guid(Convert.ToString(dataRow["F_GUID"])), false) ?? this.session.GetRelationType(Convert.ToString(dataRow["F_DESCRIPTION"]), false);
        if (dbRelationType == null)
          return CheckResult.NotFound;
        if (dbRelationType.RelationType != defaultRelation)
          return CheckResult.NotEqual;
      }
    }
    else if (defaultRelation > 0)
      return CheckResult.NotEqual;
    return CheckResult.Equal;
  }

  private CheckResult CheckParentType(int parentTypeID)
  {
    DataRow[] dataRowArray = this.metaData.Tables["IMS_OBJTYPES_TREE"].Select($"{"F_OBJECT_TYPE"}={Convert.ToInt32(this.briefRow["F_OBJECT_TYPE"])}");
    if (dataRowArray.Length != 0)
    {
      int objectType = Helper.FindObjectType(this.session, this.metaData, Convert.ToInt32(dataRowArray[0]["F_PARENT_ID"]));
      if (objectType == -1)
        return CheckResult.NotFound;
      if (objectType != parentTypeID)
        return CheckResult.NotEqual;
    }
    else if (parentTypeID > 0)
      return CheckResult.NotEqual;
    return CheckResult.Equal;
  }

  private CheckResult CheckСаptionAttribute(int captionAttribute)
  {
    if (this.briefRow["F_CAPTION_ATTRIBUTE"] != null && Convert.ToInt32(this.briefRow["F_CAPTION_ATTRIBUTE"]) > 0)
    {
      DataRow dataRow = this.metaData.Tables["IMS_ATTRIBUTES"].Rows.Find(this.briefRow["F_CAPTION_ATTRIBUTE"]);
      if (dataRow != null)
      {
        if (this.session.GetAttributeType(new Guid(Convert.ToString(dataRow["F_GUID"]))) == null)
          return CheckResult.NotFound;
        if (captionAttribute <= 0)
          return CheckResult.NotEqual;
        DataRow[] dataRowArray = this.metaData.Tables["IMS_ATTR4OBJ_TYPES"].Select($"{"F_OBJECT_TYPE"}={Convert.ToInt32(this.briefRow["F_OBJECT_TYPE"])} AND {"F_ATTRIBUTE_ID"}={this.briefRow["F_CAPTION_ATTRIBUTE"]}");
        if (dataRowArray.Length == 1)
        {
          switch ((ComputeValueModes) Convert.ToInt32(dataRowArray[0]["F_COMPUTED"]))
          {
            case ComputeValueModes.JITValue:
            case ComputeValueModes.IndexValue:
              return CheckResult.ErrorSinhronize;
          }
        }
      }
      else if (captionAttribute > 0)
        return CheckResult.NotEqual;
    }
    else if (captionAttribute > 0)
      return CheckResult.NotEqual;
    return CheckResult.Equal;
  }

  private CheckArraysResult CheckChild(IDBObjectType objType)
  {
    CheckArraysResult checkArraysResult = new CheckArraysResult(new string[3]
    {
      "notFoundInDB",
      "notFoundInDBObjectType",
      "notFoundInBriefObjType"
    });
    DataTable dataTable = this.session.GetObjectTypeCollection(objType.ObjectType).Select(string.Empty);
    List<string> stringList = new List<string>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      stringList.Add(Convert.ToString(row["F_GUID"]));
    DataRow[] dataRowArray = this.metaData.Tables["IMS_OBJTYPES_TREE"].Select($"{"F_PARENT_ID"} = {this.briefRow["F_OBJECT_TYPE"]}");
    if (dataRowArray.Length != 0)
    {
      foreach (DataRow dataRow1 in dataRowArray)
      {
        DataRow dataRow2 = this.metaData.Tables["IMS_OBJECT_TYPES"].Rows.Find(dataRow1["F_OBJECT_TYPE"]);
        if (this.session.GetObjectType(new Guid(Convert.ToString(dataRow2["F_GUID"])), false) == null)
          checkArraysResult.Add("notFoundInDB", (object) Helper.ValueToLog(dataRow2["F_OBJ_TYPE_NAME"], dataRow2["F_GUID"], true));
        else if (!stringList.Contains(Convert.ToString(dataRow2["F_GUID"])))
          checkArraysResult.Add("notFoundInDBObjectType", (object) Helper.ValueToLog(dataRow2["F_OBJ_TYPE_NAME"], dataRow2["F_GUID"], true));
      }
    }
    return checkArraysResult;
  }

  private void CheckRelations()
  {
    foreach (DataRow briefRow in this.metaData.Tables["IMS_TYPES_APPLICABILITY"].Select(string.Format("{0}={1} OR {2}={1}", (object) "F_INOBJECT_TYPE", this.briefRow["F_OBJECT_TYPE"], (object) "F_OBJECT_TYPE")))
    {
      CheckApplicability checkApplicability = new CheckApplicability(this.session, this.metaData, briefRow, this.UniIdentifiler, Convert.ToInt32(this.briefRow["F_OBJECT_TYPE"]), this.options);
      checkApplicability.Initialize();
      if (checkApplicability.Existing)
        checkApplicability.Check();
      if (checkApplicability.Log.Count > 0)
        this.infoLog.AddRange((IEnumerable<CheckMetadataLogItem>) checkApplicability.Log);
    }
    CheckApplicabilityCollection applicabilityCollection = new CheckApplicabilityCollection(this.session, this.metaData, this.item.ObjectType, this.UniIdentifiler, this.options);
    applicabilityCollection.Compare();
    if (applicabilityCollection.Log.Count <= 0)
      return;
    this.infoLog.AddRange((IEnumerable<CheckMetadataLogItem>) applicabilityCollection.Log);
  }

  private DataRow[] LCStepsForObjectType(DataRow objectType)
  {
    if (Convert.ToInt32(objectType["F_PUBLIC_LC"]) != 2)
      return this.metaData.Tables["IMS_LC_STEPS"].Select($"{"F_OBJECT_TYPE"}={Convert.ToInt32(objectType["F_OBJECT_TYPE"])} AND {"F_DELETED"} = 0");
    DataRow[] dataRowArray = this.metaData.Tables["IMS_OBJTYPES_TREE"].Select($"{"F_OBJECT_TYPE"}={Convert.ToInt32(objectType["F_OBJECT_TYPE"])}");
    if (dataRowArray.Length != 0)
    {
      DataRow objectType1 = this.metaData.Tables["IMS_OBJECT_TYPES"].Rows.Find(dataRowArray[0]["F_PARENT_ID"]);
      if (objectType1 != null)
        return this.LCStepsForObjectType(objectType1);
    }
    return (DataRow[]) null;
  }

  private CheckResult CheckVersionModes(DataRow briefRow, ObjectVersionModes objectVersionMode)
  {
    ObjectVersionModes int32 = (ObjectVersionModes) Convert.ToInt32(briefRow["F_VERSIONABLE"]);
    if (int32 == ObjectVersionModes.Abstract && objectVersionMode != ObjectVersionModes.Abstract)
      return CheckResult.ErrorSinhronize;
    if (int32 == ObjectVersionModes.SingleVersion && objectVersionMode == ObjectVersionModes.Abstract)
      return CheckResult.Error;
    if (int32 == ObjectVersionModes.SingleVersion && objectVersionMode == ObjectVersionModes.MultiVersion)
      return CheckResult.ErrorSinhronize;
    if (int32 == ObjectVersionModes.MultiVersion && objectVersionMode == ObjectVersionModes.Abstract)
      return CheckResult.Error;
    return int32 == ObjectVersionModes.MultiVersion && objectVersionMode == ObjectVersionModes.SingleVersion ? CheckResult.ErrorNotSinhronize : CheckResult.Equal;
  }

  private bool CheckPublicLC(DataRow briefRow, InheritModes inheritMode)
  {
    return (InheritModes) Convert.ToInt32(briefRow["F_PUBLIC_LC"]) == inheritMode;
  }
}
