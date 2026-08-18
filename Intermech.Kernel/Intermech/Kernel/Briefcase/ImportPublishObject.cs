// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportPublishObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Kernel.Services.PortalServices;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportPublishObject : ImportObject
{
  private readonly char _currentSiteCode;
  private readonly ImportReceipt _receipt;
  private readonly ImportVersionsModes _importVersionsMode;
  private readonly RemarkRecordHandlerCollection _remarkRecordHandlers;
  private readonly bool _isContainer;
  private readonly SiteInfo _creatorInfo;
  private readonly bool _withComposition;
  private bool _isForeign;

  public ImportPublishObject(
    UserSession Session,
    ImportingObject briefObject,
    char currentSiteCode,
    bool isContainer,
    SiteInfo creatorInfo)
    : this(Session, briefObject, currentSiteCode, (ImportReceipt) null, ImportVersionsModes.None, creatorInfo, false)
  {
    this._isContainer = isContainer;
  }

  public ImportPublishObject(
    UserSession session,
    ImportingObject briefObject,
    char currentSiteCode,
    ImportReceipt receipt,
    ImportVersionsModes importVersionsMode,
    SiteInfo creatorInfo,
    bool withComposition)
    : base(session, briefObject, false, false)
  {
    this._currentSiteCode = currentSiteCode;
    this._receipt = receipt;
    this.withAttributesCustomHandlers = true;
    this._creatorInfo = creatorInfo;
    this._importVersionsMode = importVersionsMode;
    this._withComposition = withComposition;
    RemarkRecordHandlerCollection handlerCollection = new RemarkRecordHandlerCollection();
    handlerCollection.Add((RemarkRecordHandler) new RedLineRemarkRecordHandler());
    handlerCollection.Add((RemarkRecordHandler) new XMLRemarkRecordHandler());
    this._remarkRecordHandlers = handlerCollection;
  }

  protected override bool AfterRefreshAttributes4Container(IDBObject importObj)
  {
    return this._isContainer && this.SetOwners4Container(importObj);
  }

  protected override void SetStartLCSteps(bool isCreationMode, IDBObject newObject)
  {
    if (newObject.LCStep == this.briefObject.Object.Lc_step || SiteIDHelper.IsOwner(this._currentSiteCode, newObject.SiteID))
      return;
    long num = 0;
    this.session.DataManager.ExecuteSpNonQuery("IMS_ADD_LCSTART_DATE", this.session.DataManager.Parameter("inOBJECT_ID", (object) Math.Abs(newObject.ObjectID)), this.session.DataManager.Parameter("inLC_STEP", (object) this.briefObject.Object.Lc_step), this.session.DataManager.Parameter("inSTART_DATE", (object) DateTime.UtcNow), this.session.DataManager.OutputParameter("outKEY_ID", (object) num));
  }

  protected override bool CheckFileName(
    AttributeRecord attr,
    long id,
    bool refresh,
    bool throwException)
  {
    IImportRulesService service;
    if ((service = ServiceUtils.GetService<IImportRulesService>((object) ServerServices.ServiceContainer, false)) == null || !service.RenameCoincidenceFileNames || attr.StringValue == null)
      return base.CheckFileName(attr, id, refresh, throwException);
    if (!base.CheckFileName(attr, id, refresh, false))
    {
      string fileName = Convert.ToString(attr.StringValue);
      FileInfo fileInfo = new FileInfo(fileName);
      attr.StringValue = (object) fileName.Insert(fileName.Length - fileInfo.Extension.Length, $"_{Guid.NewGuid()}");
    }
    return true;
  }

  public TypedImportedInfo ImportAttributes(bool isNewObject, out IDBObject importObj)
  {
    importObj = this.briefObject.Object.ObjectGuid == null || !((Guid) this.briefObject.Object.ObjectGuid != Guid.Empty) ? this.session.GetObject(this.briefObject.Object.Object_id, false) : this.session.GetObject((Guid) this.briefObject.Object.ObjectGuid, false);
    if (importObj == null)
      return new TypedImportedInfo((Guid) this.briefObject.Object.ObjectGuid, 0L, 0L, TransferedObjectCategory.Object, true, -1);
    IDBObjectType objectType = this.session.GetObjectType(importObj.ObjectType, true);
    try
    {
      this.session.StartTransaction();
      this.RefreshAttributes(importObj, objectType, isNewObject, true, !this._isForeign && SiteIDHelper.IsOwner(this._currentSiteCode, importObj.SiteID));
      this.AfterRefreshAttributes4Container(importObj);
      this.UpdateViews(importObj, true);
      this.session.Commit();
      return new TypedImportedInfo((Guid) this.briefObject.Object.ObjectGuid, importObj.ID, importObj.ObjectID, TransferedObjectCategory.Object, false, importObj.ObjectType);
    }
    catch
    {
      this.session.Rollback();
      throw;
    }
  }

  public override object Import()
  {
    ImportActions importActions = ImportActions.None;
    IDBObjectType objectType = this.session.GetObjectType(this.briefObject.Object.ObjectType, true);
    IDBObject dbObject1 = this.session.GetObject((Guid) this.briefObject.Object.ObjectGuid, false);
    long num1 = 0;
    if (dbObject1 == null)
    {
      num1 = this.FindID((Guid) this.briefObject.Object.IdGuid);
      if (num1 != 0L)
      {
        if (objectType.Versionable == ObjectVersionModes.SingleVersion)
        {
          dbObject1 = this.session.GetObjectByVersionsRule(num1, "cad001df-306c-11d8-b4e9-00304f19f545", true);
          importActions = ImportActions.RefreshObject;
        }
        else
        {
          int attributeChangeNoID = MetaDataHelper.GetAttributeTypeID("cad00770-306c-11d8-b4e9-00304f19f545");
          AttributeRecord attributeRecord = this.briefObject.Attributes.Find((Predicate<AttributeRecord>) (x => x.AttributeId == attributeChangeNoID));
          if (attributeRecord != null && CompareValuesHelper.NormalizedValue(attributeRecord.StringValue) != null)
          {
            if (this.session.GetObjectCollection(objectType.ObjectType).Select(new DBRecordSetParams(new ConditionStructure[2]
            {
              new ConditionStructure(-3, RelationalOperators.Equal, (object) num1, LogicalOperators.AND, 0, false),
              new ConditionStructure(attributeChangeNoID, RelationalOperators.Equal, attributeRecord.StringValue, LogicalOperators.AND, 0, false)
            }, new object[1]{ (object) -2 })).Rows.Count > 0)
              throw new Exception($"Версия объекта {num1} с Номером изменения {attributeRecord.StringValue} уже существует в базе назначения.");
          }
          importActions = ImportActions.CreateVersion;
        }
      }
    }
    if (importActions == ImportActions.None)
    {
      bool flag1 = dbObject1 != null && dbObject1.ObjectType == MetaDataHelper.GetObjectTypeID("cadd960d-306c-11d8-b4e9-00304f19f545");
      if (dbObject1 == null | flag1)
      {
        IDBObject objectOnIdAttributes = this.FindObjectOnIDAttributes(objectType);
        if (objectOnIdAttributes != null)
        {
          if ((ServerServices.GetService(typeof (IImportRulesService)) as IImportRulesService).CentralizedNSI)
          {
            try
            {
              importActions = ImbaseAttributesCompare.Compare((IUserSession) this.session, this.briefObject, objectOnIdAttributes);
            }
            catch (NotEqualImbaseLinksImportExceptions ex)
            {
              if (SiteTraceLog.Enabled)
              {
                long objectId = dbObject1 != null ? dbObject1.ObjectID : 0L;
                SiteTraceLog.Write($"Ошибка: {ex.Message}. GUID= {(Guid) this.briefObject.Object.ObjectGuid} isIncompleteObject = {flag1} importObjectID = {objectId} foundObject = {objectOnIdAttributes.ObjectID} objType = {objectType.ObjectType}");
              }
              throw;
            }
          }
          dbObject1 = objectOnIdAttributes;
        }
        else if (!flag1)
          importActions = ImportActions.CreateObject;
      }
      if (importActions == ImportActions.None)
      {
        if (flag1)
        {
          importActions = ImportActions.RefreshObject;
        }
        else
        {
          AttributeRecord attributeRecord = this.briefObject.Attributes.Find((Predicate<AttributeRecord>) (x => x.AttributeId == MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeForeign)));
          IDBAttribute attributeByGuid = dbObject1.GetAttributeByGuid(PortalConsts.attributeForeign, false);
          int num2 = attributeRecord == null ? 0 : ((long) attributeRecord.IntegerValue == 1L ? 1 : 0);
          bool flag2 = attributeByGuid != null && attributeByGuid.AsBoolean;
          if (num2 != 0)
            importActions = ImportActions.Ignore;
          else if (flag2)
          {
            importActions = ImportActions.RefreshObject;
            this._isForeign = true;
          }
          else
            importActions = ImportActions.RefreshObject;
        }
      }
    }
    switch (importActions - 1)
    {
      case ImportActions.None:
        return (object) new TypedImportedInfo((Guid) this.briefObject.Object.ObjectGuid, dbObject1.ID, dbObject1.ObjectID, TransferedObjectCategory.Object, false, dbObject1.ObjectType);
      case ImportActions.Ignore:
      case ImportActions.CreateVersion:
        long num3 = 0;
        if (num1 != 0L)
        {
          IDBObject objectBaseVersionById = this.session.GetObjectBaseVersionByID(num1, false);
          if (objectBaseVersionById != null)
            num3 = objectBaseVersionById.ObjectID;
        }
        IDBObject importedObject;
        this.FinalyAddImportedObject(num1, this.briefObject.Object.OwnerId, this.briefObject.Object.ProjectId, this.briefObject.Object.CreatorID, true, out importedObject);
        TypedImportedInfo info = new TypedImportedInfo((Guid) this.briefObject.Object.ObjectGuid, this.briefObject.Object.Id, this.briefObject.Object.Object_id, TransferedObjectCategory.Object, true, this.briefObject.Object.ObjectType);
        if (num3 != 0L)
          info.BaseVersionId = num3;
        return importedObject != null ? (object) new ExtendedImportedInfo(info, $"Создан {importedObject.NameInMessages} (ид.версии={importedObject.ObjectID})") : (object) info;
      case ImportActions.CreateObject:
        this.ClearErrorAttribute(dbObject1);
        try
        {
          if (ServerServices.GetService(typeof (IPortalEventsService)) is IPortalEventsService service)
            ((PortalTasksQueue) service).FireBeforeObjectRefreshEvent((object) this, new BeforeObjectRefreshEventArgs(dbObject1));
          if (this._importVersionsMode != ImportVersionsModes.None && this._importVersionsMode != ImportVersionsModes.ReplaceAll)
          {
            int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad0013a-306c-11d8-b4e9-00304f19f545");
            IDBAttribute attributeById = dbObject1.GetAttributeByID(attributeTypeId);
            if (attributeById != null && !attributeById.IsNull && this.ModifyDate(attributeTypeId, this.briefObject.Attributes) < attributeById.AsDateTime - this.session.TimeZoneOffset)
            {
              if (this._importVersionsMode == ImportVersionsModes.StopImport)
                throw new Exception($"Попытка импорта более старой версии {dbObject1.NameInMessages} чем существующей в базе.");
              if (this._importVersionsMode == ImportVersionsModes.ReplaceOld)
              {
                if (this._receipt != null)
                  this._receipt.AddObjectRecord((IUserSession) this.session, dbObject1, this.briefObject, $"Более старая версия из портала была заменена на {dbObject1.NameInMessages}");
                if (!string.IsNullOrEmpty(this.briefObject.Object.SiteID) && dbObject1.SiteID != this.briefObject.Object.SiteID)
                  (dbObject1 as DBObject).SetSiteID(this.briefObject.Object.SiteID);
                return (object) new TypedImportedInfo((Guid) this.briefObject.Object.ObjectGuid, dbObject1.ID, dbObject1.ObjectID, TransferedObjectCategory.Object, false, dbObject1.ObjectType);
              }
            }
          }
          if (this._receipt != null)
            ObjectsComparer.Compare((IUserSession) this.session, dbObject1, this.briefObject, this._receipt);
          if (dbObject1.CheckoutBy != 0L && dbObject1.CheckoutBy != this.session.UserID)
          {
            IDBObject dbObject2 = this.session.GetObject(dbObject1.CheckoutBy);
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_953"), (object) dbObject1.NameInMessages, (object) dbObject2.NameInMessages));
          }
          this.RefreshObject(dbObject1, objectType, true);
          return (object) new ExtendedImportedInfo(new TypedImportedInfo((Guid) this.briefObject.Object.ObjectGuid, dbObject1.ID, dbObject1.ObjectID, TransferedObjectCategory.Object, false, dbObject1.ObjectType), $"Обновлен {dbObject1.NameInMessages} (ид.версии={dbObject1.ObjectID})");
        }
        catch (Exception ex)
        {
          this.WriteErrorAttribute(objectType, dbObject1, ex);
          throw;
        }
      default:
        return (object) null;
    }
  }

  protected override bool CheckAttribute(
    IDBObject newObject,
    IDBAttributeType attributeType,
    IDBObjectType objType,
    bool isNewObject)
  {
    if (!isNewObject && SiteIDHelper.IsOwner(this._currentSiteCode, newObject.SiteID))
    {
      if (attributeType.AttributeID.Equals(this.session.IdentHelper.ModifyContentDateID))
        return false;
      IDBAttributeType4 attributeById = objType.Attributes.GetAttributeByID(attributeType.AttributeID);
      if (attributeById != null && attributeById.IsContent)
        return false;
    }
    return true;
  }

  protected override void SetBaseVersion(IDBObject dbObject, bool createdVersion)
  {
    if (SiteIDHelper.IsOwner(this._currentSiteCode, dbObject.SiteID) & createdVersion)
    {
      if (dbObject.IsBaseVersion == this.briefObject.Object.IsBaseVersion)
        return;
      IImportRulesService service = ServiceUtils.GetService<IImportRulesService>((object) ServerServices.ServiceContainer, true);
      if (service.BaseVersionTemplate == 0L)
        this.AddWarningMessage($"При установке базовой версии {dbObject.NameInMessages} не настроен Процесс согласования базовой версии");
      else
        (ServerServices.GetService(typeof (IPortalTasksQueue)) as PortalTasksQueue).FireStartResolveBaseVersionConflict((object) this, new StartResolveBaseVersionConflictEventArgs(this.session.SessionGUID, service.BaseVersionTemplate, dbObject.ObjectID));
    }
    else
    {
      if (!this.briefObject.Object.IsBaseVersion || SiteIDHelper.IsOwner(this._currentSiteCode, this.session.GetObjectBaseVersionByID(dbObject.ID, false).SiteID))
        return;
      (dbObject as DBObject).SetBaseVersion();
    }
  }

  private void ClearErrorAttribute(IDBObject obj)
  {
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(PortalConsts.attributeImportError, false);
    if (attributeByGuid == null || attributeByGuid.IsNull)
      return;
    attributeByGuid.Clear();
  }

  private void WriteErrorAttribute(IDBObjectType objType, IDBObject obj, Exception ex)
  {
    if (objType.Attributes.GetAttributeByGUID(PortalConsts.attributeImportError) == null)
      return;
    IDBAttribute dbAttribute = obj.GetAttributeByGuid(PortalConsts.attributeImportError, false) ?? obj.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeImportError), false);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(ex.Message);
    stringBuilder.Append(ex.StackTrace);
    dbAttribute.AsString = stringBuilder.ToString();
  }

  private DateTime ModifyDate(int attrModifyDate, List<AttributeRecord> attributes)
  {
    AttributeRecord attributeRecord = attributes.Find((Predicate<AttributeRecord>) (x => x.AttributeId == attrModifyDate));
    return attributeRecord != null && attributeRecord.DateValue != null && attributeRecord.DateValue != DBNull.Value && attributeRecord.DateValue is DateTime ? (DateTime) attributeRecord.DateValue : DateTime.MinValue;
  }

  protected override bool AfterRefreshAttributes(
    IDBObject newObject,
    IDBObjectType objType,
    bool isNewObject,
    bool throwException)
  {
    if (string.IsNullOrEmpty(this.briefObject.Object.Caption) && objType.CaptionAttribute != 0)
    {
      IDBAttribute attributeById = newObject.GetAttributeByID(objType.CaptionAttribute);
      if (attributeById != null && !string.IsNullOrEmpty(attributeById.AsString))
        this.briefObject.Object.Caption = attributeById.AsString;
    }
    return this.RefreshRemarkAttributes(newObject, objType, isNewObject, throwException, out bool _, SiteIDHelper.IsOwner(this._currentSiteCode, newObject.SiteID));
  }

  private string SetOwnerCode4Container(string currentSiteID, int ownerIndex)
  {
    string str1 = currentSiteID;
    if (!string.IsNullOrEmpty(this.briefObject.Object.SiteID) && this.briefObject.Object.SiteID.Length >= ownerIndex + 1 && !string.IsNullOrEmpty(str1))
    {
      char ch1 = this.briefObject.Object.SiteID[ownerIndex];
      char ch2 = str1.Length >= ownerIndex + 1 ? str1[ownerIndex] : this._currentSiteCode;
      if ((int) ch2 != (int) this._currentSiteCode && (int) ch2 != (int) ch1)
      {
        string str2 = str1.Substring(0, ownerIndex) + this.briefObject.Object.SiteID[ownerIndex].ToString();
        if (str1.Length > ownerIndex + 1)
          str2 += str1.Substring(ownerIndex + 1);
        str1 = str2;
      }
    }
    return str1;
  }

  private bool SetOwners4Container(IDBObject dbObject)
  {
    string siteID = this.SetOwnerCode4Container(this.SetOwnerCode4Container(dbObject.SiteID, 1), 2);
    if (siteID.Equals(dbObject.SiteID))
      return false;
    (dbObject as DBObject).SetSiteID(siteID);
    return true;
  }

  protected override bool RefreshObject(
    IDBObject dbObject,
    IDBObjectType objType,
    bool throwException)
  {
    if (!this._isForeign)
    {
      if (SiteIDHelper.IsOwner(this._currentSiteCode, dbObject.SiteID))
      {
        try
        {
          this.session.StartTransaction();
          if (dbObject.ObjectType == MetaDataHelper.GetObjectTypeID("cadd960d-306c-11d8-b4e9-00304f19f545"))
            base.RefreshObject(dbObject, objType, throwException);
          bool changed;
          if (!this.RefreshRemarkAttributes(dbObject, objType, false, throwException, out changed, true))
          {
            this.session.Rollback();
            return false;
          }
          if (this.SetOwners4Container(dbObject))
            changed = true;
          if (changed && !this.UpdateViews(dbObject, throwException))
          {
            this.session.Rollback();
            return false;
          }
          this.session.Commit();
          return true;
        }
        catch (Exception ex)
        {
          this.session.Rollback();
          if (throwException)
            throw;
          this.AddErrorMessage(ex);
          return false;
        }
      }
    }
    if (this._creatorInfo.SystemType == SystemTypes.Search)
      this.briefObject.Object.ObjCreate = dbObject.CreateDate;
    ISitesCacheService customService = (ISitesCacheService) this.session.GetCustomService(typeof (ISitesCacheService));
    this.clearComposition = this._withComposition && (SiteIDHelper.IsCompositionForeign(customService, dbObject.SiteID) || this._creatorInfo.SystemType == SystemTypes.Search);
    int num = base.RefreshObject(dbObject, objType, throwException) ? 1 : 0;
    this.SetBaseVersion(dbObject, false);
    return num != 0;
  }

  private bool RefreshRemarkAttributes(
    IDBObject newObject,
    IDBObjectType objType,
    bool isNewObject,
    bool throwException,
    out bool changed,
    bool isOwner)
  {
    changed = false;
    ISpecHandleAttributes service = ServerServices.GetService(typeof (ISpecHandleAttributes)) as ISpecHandleAttributes;
    try
    {
      for (int index = 0; index < this.briefObject.Remarks.Count; ++index)
      {
        RemarkRecord remark = this.briefObject.Remarks[index];
        IDBAttributeType attributeType4 = this.GetAttributeType4(remark.AttributeId, objType.Attributes);
        if (isNewObject || !service.IsNotUpdatingAttribute((attributeType4 as IDBGuid).GUID))
        {
          if (!this._remarkRecordHandlers.HandleRecord(remark, newObject))
          {
            SpecHandleAttributeEventArgs attributeEventArgs = this.BeforeRefreshAttributeEx(newObject, objType.ObjectType, ref attributeType4, (AttributeRecord) remark, service, isNewObject, isOwner);
            if (!attributeEventArgs.NotUpdate)
            {
              this.RefreshAttribute(service, (AttributeRecord) remark, attributeType4, newObject, objType, isNewObject, attributeEventArgs.Handled);
              if (index < this.briefObject.Remarks.Count - 1)
                newObject = this.session.GetObject(newObject.ObjectID);
            }
          }
          changed = true;
        }
      }
      newObject = this.session.GetObject(newObject.ObjectID);
      this._remarkRecordHandlers.OnComplete(newObject);
      if (changed)
        this.ComputeAttributes(newObject.Attributes, objType.Attributes);
      return true;
    }
    catch (Exception ex)
    {
      if (throwException)
        throw;
      this.AddErrorMessage(ex);
      return false;
    }
  }

  protected override void RefreshAttribute(
    ISpecHandleAttributes specAttrService,
    AttributeRecord attr,
    IDBAttributeType baseAttr,
    IDBObject newObject,
    IDBObjectType objType,
    bool isNewObject,
    bool handled)
  {
    if (baseAttr.AttributeID == this.session.IdentHelper.FileAttributeID)
    {
      if (!string.IsNullOrEmpty(Convert.ToString(attr.StringValue)) && new FileInfo(Convert.ToString(attr.StringValue)).Extension.ToLower() == ".rxml")
        return;
      if (!isNewObject)
      {
        bool fileAttribute = this.FindFileAttribute(newObject, attr, baseAttr);
        this.ImportBlob(attr, baseAttr, newObject.ObjectID, newObject.ID, fileAttribute);
        if (!fileAttribute)
          this.UpdateAttribute(baseAttr, attr, newObject.ObjectID);
        else
          this.InsertAttribute(baseAttr, attr, newObject.ObjectID);
        if (attr.InlistId == 0)
        {
          if (this.updatedAttributes.Contains((object) baseAttr.AttributeID))
            this.updatedAttributes[(object) baseAttr.AttributeID] = (object) attr;
          else
            this.updatedAttributes.Add((object) baseAttr.AttributeID, (object) attr);
        }
        this.AddToGlobalIndex(attr, baseAttr, objType.ObjectType, newObject.ObjectID, newObject.ID);
        return;
      }
    }
    base.RefreshAttribute(specAttrService, attr, baseAttr, newObject, objType, isNewObject, handled);
  }
}
