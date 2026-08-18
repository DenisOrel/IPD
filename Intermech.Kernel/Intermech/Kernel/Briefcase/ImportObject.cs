// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Kernel.Services.PortalServices;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel.Briefcase;

public class ImportObject : Importer
{
  private readonly List<FoundObjectInfo> _foundObjects;
  private List<IDСorresponds> _importingObjectIDs;
  protected HashSet<long> createdIDs = new HashSet<long>();
  public long NeedRefreshFolderKey;
  public int BriefcaseIndex = -1;
  protected bool createLinksArray = true;
  protected ImportingObject briefObject;
  public bool UnknownType;
  protected bool clearComposition;
  private readonly List<int> _deletedAttributes = new List<int>();

  public ImportObject(
    UserSession session,
    DataSet metadata,
    ImportingObject briefObject,
    List<FoundObjectInfo> foundObjects,
    List<IDСorresponds> importingObjectIDs,
    HashSet<long> createdIDs)
    : base(session, string.Empty, "F_OBJECT_ID", false)
  {
    this._foundObjects = foundObjects;
    this._importingObjectIDs = importingObjectIDs;
    this.createdIDs = createdIDs;
    this.metadata = metadata;
    this.briefObject = briefObject;
    DataRow dataRow = this.metadata.Tables["IMS_OBJECT_TYPES"].Rows.Find((object) this.briefObject.Object.ObjectType);
    IDBObjectType dbObjectType = this.session.GetObjectType(new Guid(dataRow["F_GUID"].ToString()), false) ?? this.session.GetObjectType(Convert.ToString(dataRow["F_OBJ_TYPE_NAME"]), false);
    if (dbObjectType != null)
    {
      this.briefObject.Object.ObjectType = dbObjectType.ObjectType;
      this.attributeTable = dbObjectType.IsLocalType ? "IMV_A" + this.briefObject.Object.ObjectType.ToString() : "IMS_OBJECT_ATTRS";
    }
    else
      this.UnknownType = true;
    this.uniIdentifiler = string.Format(BriefcaseConsts.logFormatObject, this.briefObject.Object.Caption == string.Empty ? (object) $"{{{this.briefObject.Object.ObjectGuid}}}" : (object) $"\"{this.briefObject.Object.Caption}\"");
  }

  public ImportObject(
    UserSession session,
    ImportingObject briefObject,
    bool createLinksArray,
    bool packetMode)
    : base(session, string.Empty, "F_OBJECT_ID", packetMode)
  {
    this.createLinksArray = createLinksArray;
    this._importingObjectIDs = new List<IDСorresponds>(1);
    this.briefObject = briefObject;
    this.attributeTable = this.session.GetObjectType(briefObject.Object.ObjectType, true).IsLocalType ? "IMV_A" + briefObject.Object.ObjectType.ToString() : "IMS_OBJECT_ATTRS";
  }

  protected IDBObject FindObjectOnIDAttributes(IDBObjectType objType)
  {
    long objectOnIdAttributes = ObjectSearchEngine.FindObjectOnIDAttributes((IUserSession) this.session, objType, this.briefObject);
    return objectOnIdAttributes == 0L ? (IDBObject) null : this.session.GetObject(objectOnIdAttributes);
  }

  protected virtual bool RefreshObject(
    IDBObject dbObject,
    IDBObjectType objType,
    bool throwException)
  {
    try
    {
      this.session.StartTransaction();
      if (!this.RefreshAttributes(dbObject, objType, false, throwException))
      {
        this.session.Rollback();
        return false;
      }
      if (!this.UpdateObjectProperties(false, objType, dbObject, throwException))
      {
        this.session.Rollback();
        return false;
      }
      dbObject = this.session.GetObject(dbObject.ObjectID);
      if (!this.UpdateViews(dbObject, throwException))
      {
        this.session.Rollback();
        return false;
      }
      (dbObject as DBObject).SetCaption(this.briefObject.Object.Caption);
      if (this.clearComposition)
        this.DeleteRelations(dbObject);
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

  protected bool UpdateViews(IDBObject newObject, bool throwException)
  {
    try
    {
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      for (int AttrIndex = 0; AttrIndex < newObject.Attributes.Count; ++AttrIndex)
      {
        IDBAttribute attribute = newObject.Attributes[AttrIndex];
        if (!this.updatedAttributes.Contains((object) attribute.AttributeID) || this.temporaryAttributes.Contains((object) attribute.AttributeID))
        {
          if (!this.calculatedAttributes.Contains(attribute.AttributeID))
          {
            (attribute as DBAttribute).Compute(true);
            this.calculatedAttributes.Add(attribute.AttributeID);
          }
        }
        else
        {
          string[] updateTables = this.session.DBCache.GetUpdateTables(attribute.AttributeID, newObject.ObjectType, -1);
          if (updateTables != null && updateTables.Length != 0)
          {
            AttributeRecord updatedAttribute = (AttributeRecord) this.updatedAttributes[(object) attribute.AttributeID];
            arrayList1.Add((object) this.AddViewFieldsToSQL(updateTables, updatedAttribute));
            foreach (string str in updateTables)
            {
              if (arrayList2.BinarySearch((object) str) < 0)
                arrayList2.Add((object) str);
            }
          }
        }
      }
      foreach (string str in arrayList2)
      {
        StringBuilder stringBuilder = new StringBuilder();
        ArrayList arrayList3 = new ArrayList();
        foreach (Importer.UpdatingAttribute updatingAttribute in arrayList1)
        {
          if (updatingAttribute.Tables.Contains(str))
          {
            foreach (Tuple<string, DbType, object> fieldsAndValue in updatingAttribute.FieldsAndValues)
            {
              stringBuilder.Append(fieldsAndValue.Item1);
              stringBuilder.Append(" = :");
              stringBuilder.Append(fieldsAndValue.Item1);
              stringBuilder.Append(',');
              if (this.packetMode)
                arrayList3.Add((object) this.session.DataManager.BatchParameter(fieldsAndValue.Item1, fieldsAndValue.Item2, fieldsAndValue.Item3));
              else
                arrayList3.Add((object) this.session.DataManager.Parameter(fieldsAndValue.Item1, fieldsAndValue.Item3));
            }
          }
        }
        if (stringBuilder.Length > 1 && arrayList3.Count > 0)
        {
          stringBuilder.Remove(stringBuilder.Length - 1, 1);
          string commandText = $"UPDATE {str} SET {stringBuilder.ToString()} WHERE F_OBJECT_ID = :objID";
          if (this.packetMode)
          {
            arrayList3.Add((object) this.session.DataManager.BatchParameter("objID", DbType.Int64, (object) newObject.ObjectID));
            this.session.DataManager.AddBatchSQL(commandText, (DbCommandParam[]) arrayList3.ToArray(typeof (DbCommandParam)));
          }
          else
          {
            arrayList3.Add((object) this.session.DataManager.Parameter("objID", (object) newObject.ObjectID));
            this.session.DataManager.ExecuteNonQuery(commandText, (IDbDataParameter[]) arrayList3.ToArray(typeof (IDbDataParameter)));
          }
        }
      }
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

  protected bool NeedDeleteAttribute(
    IDBAttributeType baseAttr,
    AttributeRecord attr,
    IDBObjectType objType,
    bool isNewObject)
  {
    bool flag = false;
    if (this._deletedAttributes.Contains(baseAttr.AttributeID))
      return false;
    if (!isNewObject)
    {
      flag = true;
    }
    else
    {
      IDBAttributeType4 attributeById = objType.Attributes.GetAttributeByID(baseAttr.AttributeID);
      if (attributeById != null && attributeById.Required != RequiredModes.Manual)
        flag = true;
    }
    return flag;
  }

  protected void ImportBlob(
    AttributeRecord attr,
    IDBAttributeType baseAttr,
    long objectID,
    long id,
    bool isNewBlob)
  {
    if (isNewBlob)
      attr.IntegerValue = (object) 0L;
    if (attr.FileAuthor != null)
    {
      string str = Convert.ToString(attr.FileAuthor);
      if (GuidHelper.IsGuid(str))
      {
        QuickObjectInfo objectInfo = this.session.GetObjectInfo(new Guid(str));
        attr.FileAuthor = (object) (objectInfo.Empty ? 0L : objectInfo.ObjectID);
        if (objectInfo.Empty)
          this.AddWarningMessage($"Для файлового атрибута {baseAttr.Name} объекта {objectID} не удалость установить автора по значению FileAuthor={{{str}}}");
      }
    }
    if (attr.DateValue is DateTime dateValue && dateValue == DateTime.MinValue)
      attr.DateValue = (object) null;
    if (attr.DoubleValue is double doubleValue && doubleValue.Equals(double.MinValue))
      attr.DoubleValue = (object) null;
    if (baseAttr.AttributeID == this.session.IdentHelper.FileAttributeID)
      this.CheckFileName(attr, id, !isNewBlob, true);
    Intermech.Kernel.Briefcase.ImportBlob importBlob = new Intermech.Kernel.Briefcase.ImportBlob(this.session, this.hintAppendEnable);
    attr.IntegerValue = (object) importBlob.Import(objectID, attr, baseAttr.AttributeType, isNewBlob);
    foreach (string warning in (IEnumerable<string>) importBlob.GetWarnings())
      this.AddWarningMessage(warning);
    if (baseAttr.AttributeID != this.session.IdentHelper.FileAttributeID)
      return;
    this.AddFileNameIntoTable(attr, objectID, id, !isNewBlob);
  }

  protected virtual void HandleSpecialFileName(AttributeRecord attr)
  {
    string str1 = Convert.ToString(attr.StringValue);
    if (string.IsNullOrEmpty(str1))
      return;
    string str2 = ".RLF2";
    if (!str1.ToUpper().EndsWith(str2))
      return;
    attr.StringValue = (object) str1.Remove(str1.Length - str2.Length);
    attr.FileType = (object) FileTypes.ftRedlining;
  }

  protected bool FindFileAttribute(
    IDBObject newObject,
    AttributeRecord attr,
    IDBAttributeType baseAttr)
  {
    IDBAttribute attributeById = newObject.GetAttributeByID(baseAttr.AttributeID);
    if (baseAttr.AttributeID == this.session.IdentHelper.FileAttributeID)
      this.HandleSpecialFileName(attr);
    bool flag = false;
    if (attributeById != null)
    {
      for (int index = 0; index < attributeById.ValuesCount; ++index)
      {
        attributeById.Index = index;
        if (Convert.ToString(attr.StringValue).Equals(attributeById.AsString))
        {
          attr.InlistId = index;
          attr.IntegerValue = (object) attributeById.AsInteger;
          flag = true;
          break;
        }
      }
    }
    if (!flag && attributeById != null && attributeById.ValuesCount > 0)
    {
      attr.InlistId = attributeById.ValuesCount;
      attr.IntegerValue = (object) 0L;
    }
    return !flag;
  }

  protected virtual void RefreshAttribute(
    ISpecHandleAttributes specAttrService,
    AttributeRecord attr,
    IDBAttributeType baseAttr,
    IDBObject newObject,
    IDBObjectType objType,
    bool isNewObject,
    bool handled)
  {
    if (this.NeedDeleteAttribute(baseAttr, attr, objType, isNewObject))
    {
      this.DeleteAttributeValues(attr, baseAttr.AttributeType, baseAttr.AttributeID, newObject.ObjectID);
      this._deletedAttributes.Add(baseAttr.AttributeID);
    }
    try
    {
      if (attr.IntegerValue != null)
      {
        if (attr.IntegerValue != DBNull.Value)
          Convert.ToInt64(attr.IntegerValue);
      }
    }
    catch (Exception ex)
    {
      if (!(ex is FormatException))
        throw;
    }
    if (!this.withAttributesCustomHandlers || !handled)
    {
      IIDLinkTranslate service = ServerServices.ServiceContainer.GetService<IIDLinkTranslate>();
      if (baseAttr.AttributeType == FieldTypes.ftBlob || baseAttr.AttributeType == FieldTypes.ftFile || baseAttr.AttributeType == FieldTypes.ftMemo || baseAttr.AttributeType == FieldTypes.ftShortBlob)
      {
        bool fileAttribute = this.FindFileAttribute(newObject, attr, baseAttr);
        this.ImportBlob(attr, baseAttr, newObject.ObjectID, newObject.ID, fileAttribute);
      }
      if ((baseAttr.AttributeType == FieldTypes.ftObjectLink || baseAttr.AttributeType == FieldTypes.ftObjectLinkByID) && this.createLinksArray)
      {
        this.ObjectLinks.Add((object) new Intermech.Kernel.Briefcase.ObjectLinks(newObject.ObjectID, baseAttr.AttributeID, attr.InlistId, Convert.ToInt64(attr.IntegerValue), Convert.ToString(attr.StringValue), newObject.ObjectType, baseAttr.AttributeType == FieldTypes.ftObjectLinkByID));
        attr.IntegerValue = (object) null;
        attr.StringValue = (object) null;
      }
      if (baseAttr.AttributeType == FieldTypes.ftInteger && this.createLinksArray && service != null && service.IsIDLink((baseAttr as IDBGuid).GUID))
      {
        this.ObjectLinks.Add((object) new Intermech.Kernel.Briefcase.ObjectLinks(newObject.ObjectID, baseAttr.AttributeID, attr.InlistId, Convert.ToInt64(attr.IntegerValue), string.Empty, newObject.ObjectType, false));
        attr.IntegerValue = (object) null;
      }
      if (baseAttr.AttributeType == FieldTypes.ftString)
      {
        string str = Convert.ToString(attr.StringValue);
        if (baseAttr.SizeType < (long) str.Length)
          attr.StringValue = (object) str.Substring(0, Convert.ToInt32(baseAttr.SizeType));
      }
    }
    this.InsertAttribute(baseAttr, attr, newObject.ObjectID);
    if (attr.InlistId == 0)
    {
      if (this.updatedAttributes.Contains((object) baseAttr.AttributeID))
        this.updatedAttributes[(object) baseAttr.AttributeID] = (object) attr;
      else
        this.updatedAttributes.Add((object) baseAttr.AttributeID, (object) attr);
    }
    this.AddToGlobalIndex(attr, baseAttr, objType.ObjectType, newObject.ObjectID, newObject.ID);
  }

  protected void AddToGlobalIndex(
    AttributeRecord attr,
    IDBAttributeType baseAttr,
    int objectType,
    long objectID,
    long id)
  {
    if ((baseAttr.Options & AttributeOptions.AddToGlobalIndex) != AttributeOptions.AddToGlobalIndex)
      return;
    this.session.AddAttrToIndexQueue(objectID, attr.AttributeId, attr.InlistId, id, Convert.ToString(attr.StringValue), baseAttr.Options, baseAttr.AttributeType);
  }

  protected override void OnDeleteAttribute(
    AttributeRecord attr,
    FieldTypes fieldType,
    int attributeID,
    long attributableID)
  {
    if (fieldType != FieldTypes.ftObjectLink)
      return;
    this.session.DataManager.ExecuteDataTable("DELETE FROM IMS_OBJECT_LINKS WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID", this.session.DataManager.Parameter("objID", (object) attributableID), this.session.DataManager.Parameter("attrID", (object) attributeID));
  }

  protected override void AttributeAdditionalActions(
    bool insert,
    FieldTypes fieldType,
    int attributeID,
    AttributeRecord attributeRecord,
    long keyID)
  {
    if (fieldType != FieldTypes.ftObjectLink)
      return;
    string commandText = insert ? "INSERT INTO IMS_OBJECT_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TOOBJECT_ID) VALUES (:objID, :attrID, :listID, :toObjID)" : "UPDATE IMS_OBJECT_LINKS SET F_TOOBJECT_ID = :toObjID WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :listID";
    if (this.packetMode)
      DBHelper.AddBatchSQL((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, commandText, new DbCommandParam[4]
      {
        this.session.DataManager.BatchParameter("objID", DbType.Int64, (object) keyID),
        this.session.DataManager.BatchParameter("attrID", DbType.Int32, (object) attributeID),
        this.session.DataManager.BatchParameter("listID", DbType.Int32, (object) attributeRecord.InlistId),
        this.session.DataManager.BatchParameter("toObjID", DbType.Int64, CompareValuesHelper.NormalizedValue(attributeRecord.IntegerValue) != null ? attributeRecord.IntegerValue : (object) 0L)
      });
    else
      DBHelper.ExecuteNonQuery((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, commandText, this.session.DataManager.Parameter("objID", (object) keyID), this.session.DataManager.Parameter("attrID", (object) attributeID), this.session.DataManager.Parameter("listID", (object) attributeRecord.InlistId), this.session.DataManager.Parameter("toObjID", CompareValuesHelper.NormalizedValue(attributeRecord.IntegerValue) != null ? attributeRecord.IntegerValue : (object) 0L));
  }

  protected bool FinalyAddImportedObject(
    long id,
    long ownerID,
    long projectID,
    long creatorID,
    bool throwException)
  {
    return this.FinalyAddImportedObject(id, ownerID, projectID, creatorID, throwException, out IDBObject _);
  }

  protected bool FinalyAddImportedObject(
    long id,
    long ownerID,
    long projectID,
    long creatorID,
    bool throwException,
    out IDBObject importedObject)
  {
    importedObject = this.AddImportedObject(id, throwException);
    if (importedObject == null)
      return false;
    if (importedObject.ObjectType == this.session.IdentHelper.UsersTypeID)
    {
      if (this.session.GetRelation(this.session.IdentHelper.AllUsersGroupID, importedObject.ID, this.session.IdentHelper.SimpleRelationTypeID) == null)
        this.session.GetRelationCollection(this.session.IdentHelper.SimpleRelationTypeID).Create(this.session.IdentHelper.AllUsersGroupID, importedObject.ObjectID);
      DBUserObject.AfterAddUser((IUserSession) this.session, importedObject.ObjectID);
    }
    this.SaveLinks(this.briefObject.Object.Object_id, this.briefObject.Object.Id, new ImportedInfo(importedObject.ObjectGUID, importedObject.ID, importedObject.ObjectID, true), this.briefObject.Object.ObjectType, ownerID, projectID, creatorID);
    this.briefObject.Object.Object_id = importedObject.ObjectID;
    this.briefObject.Object.Id = importedObject.ID;
    ServerServices.ServiceContainer.GetService<ICustomImport>().FireCustomImported((object) this, new CustomImportedEventArgs((IUserSession) this.session, 1, (object) importedObject));
    return true;
  }

  private bool RefreshOldObject(
    IDBObject dbObj,
    IDBObjectType objType,
    bool langEquals,
    long oldOwner,
    long oldProjID,
    long oldCreatorID)
  {
    if ((objType.ObjectType == this.session.IdentHelper.UsersTypeID || objType.ObjectType == this.session.IdentHelper.GroupsTypeID) && SystemGUIDs.IsSystemGUID(dbObj.GUID))
      return true;
    ImportedInfo info = new ImportedInfo(dbObj.ObjectGUID, dbObj.ID, dbObj.ObjectID, false);
    if (langEquals)
    {
      if (!this.RefreshObject(dbObj, objType, false))
        return false;
      this.SaveLinks(this.briefObject.Object.Object_id, this.briefObject.Object.Id, info, dbObj.ObjectType, oldOwner, oldProjID, oldCreatorID);
      if (this.BriefcaseIndex >= 0)
        ((IBriefcaseProcesses) ServerServices.GetService(typeof (IBriefcaseProcesses))).ImportObject(this.BriefcaseIndex, dbObj, this.briefObject);
      ServerServices.ServiceContainer.GetService<ICustomImport>().FireCustomImported((object) this, new CustomImportedEventArgs((IUserSession) this.session, 1, (object) dbObj));
      return true;
    }
    this._importingObjectIDs.Add(new IDСorresponds(this.briefObject.Object.Object_id, this.briefObject.Object.Id, info.ObjectId, info.Id, info.IsNew));
    return true;
  }

  private void DeleteRelations(IDBObject dbObject)
  {
    IDBRelationCollection relationCollection = this.session.GetRelationCollection(-1);
    relationCollection.LocalTypesMode = true;
    DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -20,
      (object) -23
    }), dbObject.ObjectID);
    int relationTypeId = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
    bool flag = false;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      int int32 = Convert.ToInt32(dataTable.Rows[index][1]);
      if (relationTypeId == int32)
        flag = true;
      IDBRelation relation = this.session.GetRelation(Convert.ToInt64(dataTable.Rows[index][0]));
      (relation as DBRelation).DontDeleteChildObjectMode = true;
      (relation as DBRelation).DeleteWithoutCheck((long) Consts.PurgeMode);
    }
    if (!flag)
      return;
    ContextHelper.ClearContext((IUserSession) this.session, dbObject.ObjectID);
  }

  private IDBObject AddImportedObject(long id, bool throwException)
  {
    try
    {
      this.session.StartTransaction();
      bool flag = id == 0L;
      IDBObjectType objectType = this.session.GetObjectType(this.briefObject.Object.ObjectType, false);
      long newObjectID = 0;
      long newID = 0;
      int newVersionID = 0;
      if (this.briefObject.Object.ParentVersionId > 0L && this.session.GetObjectInfo(this.briefObject.Object.ParentVersionId).Empty)
        this.briefObject.Object.ParentVersionId = 0L;
      this.ExecAddImportedObject(ref newObjectID, ref newID, ref newVersionID, id, ObjectRecordKind.Import, this.briefObject.Object.ParentVersionId, this.briefObject.Object.Caption, this.briefObject.Object.ObjCreate, this.briefObject.Object.ModifyDate, 0L, this.briefObject.Object.SiteID);
      if (flag)
        this.createdIDs.Add(newID);
      IDBObject newObject1 = this.session.GetObject(newObjectID, false);
      Importer.AppendObligatoryAttributes(Helper.GetAttributesForObjectType((IUserSession) this.session, newObject1.ObjectType), (ImportingAttributable) this.briefObject, newObject1.ObjectID, this.temporaryAttributes);
      if (!this.RefreshAttributes(newObject1, objectType, true, throwException))
      {
        this.session.Rollback();
        return (IDBObject) null;
      }
      this.briefObject.Object.Caption = newObject1.Caption;
      if (!this.UpdateObjectProperties(true, objectType, newObjectID, throwException))
      {
        this.session.Rollback();
        return (IDBObject) null;
      }
      IDBObject newObject2 = this.session.GetObject(newObject1.ObjectID);
      if (!flag && this.briefObject.Object.IsBaseVersion)
        this.SetBaseVersion(newObject2, true);
      if (!this.UpdateViews(newObject2, throwException))
      {
        this.session.Rollback();
        return (IDBObject) null;
      }
      this.session.Commit();
      if (this.BriefcaseIndex >= 0)
        ((IBriefcaseProcesses) ServerServices.GetService(typeof (IBriefcaseProcesses))).ImportObject(this.BriefcaseIndex, newObject2, this.briefObject);
      return newObject2;
    }
    catch (Exception ex)
    {
      this.session.Rollback();
      if (throwException)
        throw;
      this.AddErrorMessage(ex);
      return (IDBObject) null;
    }
  }

  protected virtual void SetBaseVersion(IDBObject newObject, bool createdVersion)
  {
    if (!this.createdIDs.Contains(newObject.ID))
      return;
    (newObject as DBObject).SetBaseVersion();
  }

  private bool UpdateObjectProperties(
    bool IsCreationMode,
    IDBObjectType objType,
    long ObjectID,
    bool throwException)
  {
    return this.UpdateObjectProperties(IsCreationMode, objType, this.session.GetObject(ObjectID, false), throwException);
  }

  protected virtual void SetStartLCSteps(bool isCreationMode, IDBObject newObject)
  {
    if (!isCreationMode)
      this.session.DataManager.ExecuteNonQuery("DELETE FROM IMS_LCSTART_DATE WHERE F_OBJECT_ID = :objID", this.session.DataManager.Parameter("objID", (object) Math.Abs(newObject.ObjectID)));
    foreach (LCStepRecord lcStep in this.briefObject.LCSteps)
    {
      int num1;
      if (lcStep.LCStep != this.briefObject.Object.Lc_step)
      {
        num1 = Helper.GetConformityLCStep(this.session, this.metadata.Tables["IMS_LC_STEPS"], lcStep.LCStep);
        if (num1 == -1)
        {
          DataRow dataRow = this.metadata.Tables["IMS_LC_STEPS"].Rows.Find((object) lcStep.LCStep);
          this.AddIntoLog($"Шаг {dataRow["F_LC_NAME"]} ({dataRow["F_GUID"]}) из истории продвижения по жизненному циклу импортируемого объекта {newObject.NameInMessages} не найден в базе назначения.");
          continue;
        }
      }
      else
        num1 = this.briefObject.Object.Lc_step;
      long num2 = 0;
      this.session.DataManager.ExecuteSpNonQuery("IMS_ADD_LCSTART_DATE", this.session.DataManager.Parameter("inOBJECT_ID", (object) Math.Abs(newObject.ObjectID)), this.session.DataManager.Parameter("inLC_STEP", (object) num1), this.session.DataManager.Parameter("inSTART_DATE", (object) lcStep.LCStartDate), this.session.DataManager.OutputParameter("outKEY_ID", (object) num2));
    }
  }

  protected bool UpdateObjectProperties(
    bool isCreationMode,
    IDBObjectType objType,
    IDBObject newObject,
    bool throwException)
  {
    try
    {
      if (newObject == null)
        throw new ArgumentNullException(nameof (newObject));
      bool flag = false;
      if (newObject.ObjectType != this.briefObject.Object.ObjectType)
      {
        IDBObjectType objectType1 = this.session.GetObjectType(newObject.ObjectType, false);
        IDBObjectType objectType2 = this.session.GetObjectType(this.briefObject.Object.ObjectType, false);
        this.AddIntoLog(string.Format(BriefcaseConsts.logObjectChangeObjectType, (object) objectType1.ObjectTypeName, (object) objectType2.ObjectTypeName));
        flag = true;
      }
      string empty = string.Empty;
      string str1;
      if (!string.IsNullOrEmpty(newObject.SiteID) && !string.IsNullOrEmpty(this.briefObject.Object.SiteID) && (int) newObject.SiteID[0] != (int) this.briefObject.Object.SiteID[0])
      {
        str1 = empty + newObject.SiteID[0].ToString();
        if (this.briefObject.Object.SiteID.Length > 1)
          str1 += this.briefObject.Object.SiteID.Substring(1, this.briefObject.Object.SiteID.Length - 1);
      }
      else
        str1 = this.briefObject.Object.SiteID;
      long num = newObject.ID;
      if (flag && newObject.ObjectType == MetaDataHelper.GetObjectTypeID("cadd960d-306c-11d8-b4e9-00304f19f545") && !newObject.GUID.Equals((Guid) this.briefObject.Object.IdGuid))
      {
        IDbDataParameter dbDataParameter1 = this.session.DataManager.Parameter("guid", (object) Convert.ToString(this.briefObject.Object.IdGuid));
        IDbDataParameter dbDataParameter2 = this.session.DataManager.Parameter("typ", (object) 2);
        object obj = this.session.DataManager.ExecuteScalar("SELECT F_ID FROM IMS_GUID_RESOLVE WHERE F_GUID = :guid AND F_CATEGORY_TYPE = :typ", dbDataParameter1, dbDataParameter2);
        if (obj == null || obj == DBNull.Value)
          this.session.DataManager.ExecuteNonQuery("UPDATE IMS_GUID_RESOLVE SET F_GUID = :guid WHERE F_ID = :id1 AND F_CATEGORY_TYPE = :typ", dbDataParameter1, this.session.DataManager.Parameter("id1", (object) num), dbDataParameter2);
        else
          num = Convert.ToInt64(obj);
      }
      IDbDataParameter dbDataParameter3 = this.session.DataManager.Parameter("objID", (object) newObject.ObjectID);
      IDbDataParameter dbDataParameter4 = this.session.DataManager.Parameter("id1", (object) num);
      IDbDataParameter dbDataParameter5 = this.session.DataManager.Parameter("lcStep", (object) this.briefObject.Object.Lc_step);
      IDbDataParameter dbDataParameter6 = this.session.DataManager.Parameter("verID", (object) newObject.VersionID);
      IDbDataParameter dbDataParameter7 = this.session.DataManager.Parameter("caption1", (object) this.briefObject.Object.Caption);
      IDbDataParameter dbDataParameter8 = this.session.DataManager.Parameter("objCreate", (object) this.briefObject.Object.ObjCreate);
      IDbDataParameter dbDataParameter9 = this.session.DataManager.Parameter("modifDate", (object) this.briefObject.Object.ModifyDate);
      IDbDataParameter dbDataParameter10 = this.session.DataManager.Parameter("obType", (object) this.briefObject.Object.ObjectType);
      IDbDataParameter dbDataParameter11 = this.session.DataManager.Parameter("level1", (object) this.briefObject.Object.LevelId);
      IDbDataParameter dbDataParameter12 = this.session.DataManager.Parameter("verType", (object) 1);
      IDbDataParameter dbDataParameter13 = this.session.DataManager.Parameter("owner1", (object) this.briefObject.Object.OwnerId);
      IDbDataParameter dbDataParameter14 = this.session.DataManager.Parameter("project", (object) this.briefObject.Object.ProjectId);
      IDbDataParameter dbDataParameter15 = this.session.DataManager.Parameter("siteID", (object) str1);
      IDbDataParameter dbDataParameter16 = this.session.DataManager.Parameter("access1", (object) this.briefObject.Object.AccessLevel);
      this.session.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_LC_STEP =:lcStep, F_OBJECT_VER_TYPE = :verType, F_OBJECT_TYPE = :obType, F_OWNER_ID = :owner1, F_MODIFY_DATE = :modifDate, F_LEVEL_ID = :level1, F_OBJ_CREATE = :objCreate, F_PROJECT_ID = :project, F_SITE_ID = :siteID, F_ACCESS = :access1, F_ID = :id1 WHERE F_OBJECT_ID = :objID", dbDataParameter5, dbDataParameter12, dbDataParameter10, dbDataParameter13, dbDataParameter9, dbDataParameter11, dbDataParameter8, dbDataParameter14, dbDataParameter15, dbDataParameter16, dbDataParameter4, dbDataParameter3);
      this.SetStartLCSteps(isCreationMode, newObject);
      IDbDataParameter dbDataParameter17 = this.session.DataManager.Parameter("guidPar", (object) newObject.ObjectGUID.ToString());
      if (flag)
      {
        string[] updateTables = this.session.DBCache.GetUpdateTables(-1, newObject.ObjectType, -1);
        if (updateTables != null)
        {
          foreach (string str2 in updateTables)
            this.session.DataManager.ExecuteNonQuery($"DELETE FROM {str2} WHERE  F_OBJECT_ID = :objID", dbDataParameter3);
        }
      }
      foreach (string updateTable in this.session.DBCache.GetUpdateTables(-1, this.briefObject.Object.ObjectType, -1))
      {
        if (Convert.ToInt32(this.session.DataManager.ExecuteScalar($"SELECT COUNT(*) FROM {updateTable} WHERE F_OBJECT_ID = :objID", dbDataParameter3)) == 0)
          DBHelper.ExecuteNonQuery((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, $"INSERT INTO {updateTable} (F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, F_GUID, CAPTION, F_OBJ_CREATE, F_PROJECT_ID, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID) SELECT F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, :verType , F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, :guidPar, :caption1, :objCreate, :project, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dbDataParameter12, dbDataParameter17, dbDataParameter7, dbDataParameter8, dbDataParameter14, dbDataParameter3);
        else
          this.session.DataManager.ExecuteNonQuery($"UPDATE {updateTable} SET F_ID = :id1, F_LC_STEP = :lcStep, F_VERSION_ID = :verID, F_OBJECT_VER_TYPE = :verType, F_OBJECT_TYPE = :obType, F_OWNER_ID = :owner1, F_LEVEL_ID = :level1, F_GUID = :guidPar, CAPTION = :caption1, F_OBJ_CREATE = :objCreate,  F_PROJECT_ID = :project, F_SITE_ID = :siteID, F_ACCESS = :access1 WHERE  F_OBJECT_ID = :objID", dbDataParameter4, dbDataParameter5, dbDataParameter6, dbDataParameter12, dbDataParameter10, dbDataParameter13, dbDataParameter11, dbDataParameter17, dbDataParameter7, dbDataParameter8, dbDataParameter14, dbDataParameter15, dbDataParameter16, dbDataParameter3);
      }
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

  protected virtual bool AfterRefreshAttributes(
    IDBObject newObject,
    IDBObjectType objType,
    bool isNewObject,
    bool throwException)
  {
    return true;
  }

  protected SpecHandleAttributeEventArgs BeforeRefreshAttributeEx(
    IDBObject newObject,
    int objType,
    ref IDBAttributeType baseAttr,
    AttributeRecord attr,
    ISpecHandleAttributes specAttrService,
    bool isNewObject,
    bool isOwner = true)
  {
    SpecHandleAttributeEventArgs e = new SpecHandleAttributeEventArgs((IUserSession) this.session, newObject.ObjectID, (IDBAttributable) newObject, objType, baseAttr.AttributeID, (baseAttr as IDBGuid).GUID, attr, isNewObject, isOwner);
    this.SpecHandleObjectAttributeEvent(e);
    if (this.withAttributesCustomHandlers)
      specAttrService.FireEventForObjectAttribute(e);
    if (e.NewAttributeID != 0)
      baseAttr = this.GetAttributeType4(e.NewAttributeID, objType);
    return e;
  }

  protected bool RefreshAttributes(
    IDBObject newObject,
    IDBObjectType objType,
    bool isNewObject,
    bool throwException,
    bool notIsContentOnly = false)
  {
    ISpecHandleAttributes service = ServerServices.ServiceContainer.GetService<ISpecHandleAttributes>();
    ISitesCacheService customService = (ISitesCacheService) this.session.GetCustomService(typeof (ISitesCacheService));
    try
    {
      for (int index = 0; index < this.briefObject.Attributes.Count; ++index)
      {
        AttributeRecord attribute = this.briefObject.Attributes[index];
        IDBAttributeType attributeType4 = this.GetAttributeType4(attribute.AttributeId, objType.Attributes);
        if ((!(!isNewObject & notIsContentOnly) || !attributeType4.IsContent) && (isNewObject || !service.IsNotUpdatingAttribute((attributeType4 as IDBGuid).GUID) || newObject.GetAttributeByGuid((attributeType4 as IDBGuid).GUID) == null) && this.CheckAttribute(newObject, attributeType4, objType, isNewObject))
        {
          SpecHandleAttributeEventArgs attributeEventArgs = this.BeforeRefreshAttributeEx(newObject, objType.ObjectType, ref attributeType4, attribute, service, isNewObject, !SiteIDHelper.IsForeign(customService, newObject.SiteID));
          if (!attributeEventArgs.NotUpdate)
            this.RefreshAttribute(service, attribute, attributeType4, newObject, objType, isNewObject, attributeEventArgs.Handled);
          if (objType.CaptionAttribute == attributeType4.AttributeID)
            this.briefObject.Object.Caption = Convert.ToString(attribute.StringValue);
        }
      }
      newObject = this.session.GetObject(newObject.ObjectID);
      this.ComputeAttributes(newObject.Attributes, objType.Attributes);
      return this.AfterRefreshAttributes(newObject, objType, isNewObject, throwException);
    }
    catch (Exception ex)
    {
      if (throwException)
        throw;
      this.AddErrorMessage(ex);
      return false;
    }
  }

  protected override IDBAttribute4TypeCollection GetAttributesCollection(int typeID)
  {
    return this.session.GetObjectType(typeID).Attributes;
  }

  protected virtual bool CheckAttribute(
    IDBObject newObject,
    IDBAttributeType attributeType,
    IDBObjectType objType,
    bool isNewObject)
  {
    return true;
  }

  private void SpecHandleObjectAttributeEvent(SpecHandleAttributeEventArgs e)
  {
    if (e.AttributeID == this.session.IdentHelper.FileAttributeID && e.IsOwner && e.Attributable != null && e.Value.FileType is FileTypes && (FileTypes) e.Value.FileType == FileTypes.ftNotContent)
    {
      IDBAttribute attributeById = e.Attributable.GetAttributeByID(e.AttributeID);
      string str = Convert.ToString(e.Value.StringValue);
      for (int index = 0; index < attributeById.ValuesCount; ++index)
      {
        attributeById.Index = index;
        if (!attributeById.IsNull && (attributeById as IBlobReader).OpenBlob(-1).FileName == str)
        {
          e.NotUpdate = true;
          break;
        }
      }
    }
    Dictionary<string, object> tag = new Dictionary<string, object>()
    {
      {
        "error",
        (object) null
      },
      {
        "needRefreshFolderKey",
        (object) null
      }
    };
    if (this.metadata != null)
      tag.Add("metadata", (object) this.metadata);
    if (ServerServices.ServiceContainer.GetService<IImportAttributesHandlerService>(false) is AttributesHandlerService service)
      service.HandleValue(e, tag);
    if (!e.Handled)
      return;
    if (tag["error"] != null)
      this.AddErrorMessage((Exception) tag["error"]);
    if (tag["needRefreshFolderKey"] == null)
      return;
    this.NeedRefreshFolderKey = (long) tag["needRefreshFolderKey"];
  }

  private void SaveLinks(
    long oldObjectID,
    long oldID,
    ImportedInfo info,
    int newObjectType,
    long oldOwnerID,
    long oldProjectID,
    long oldCreatorID)
  {
    if (oldOwnerID != -1L || oldProjectID != -1L || oldCreatorID != -1L)
    {
      ObjectPropertiesLinks objectPropertiesLinks = new ObjectPropertiesLinks(info.ObjectId, newObjectType);
      if (oldOwnerID != -1L)
        objectPropertiesLinks.OldOwnerID = oldOwnerID;
      if (oldProjectID != -1L)
        objectPropertiesLinks.OldProjectID = oldProjectID;
      if (oldCreatorID != -1L)
        objectPropertiesLinks.OldCreatorID = oldCreatorID;
      this.ObjectLinks.Add((object) objectPropertiesLinks);
    }
    this._importingObjectIDs.Add(new IDСorresponds(oldObjectID, oldID, info.ObjectId, info.Id, info.IsNew));
    this.AddIntoLog(BriefcaseConsts.logOKImported);
  }

  private void ExecAddImportedObject(
    ref long newObjectID,
    ref long newID,
    ref int newVersionID,
    long id,
    ObjectRecordKind objRec,
    long parentVersionId,
    string caption,
    DateTime objectCreate,
    DateTime objectModify,
    long projectID,
    string siteID)
  {
    IDbDataParameter dbDataParameter1 = objectModify != DateTime.MinValue ? this.session.DataManager.Parameter("inMODIFY_DATE", (object) objectModify) : this.session.DataManager.Parameter("inMODIFY_DATE", (object) null);
    IDbDataParameter dbDataParameter2 = objectCreate != DateTime.MinValue ? this.session.DataManager.Parameter("inCREATE_DATE", (object) objectCreate) : this.session.DataManager.Parameter("inCREATE_DATE", (object) null);
    string str1 = siteID;
    string str2 = string.Empty;
    if (str1 != null && str1.Length > 2 && this.session.DataManager.DataProvider.Name == "Sql")
    {
      str2 = siteID;
      str1 = string.Empty;
    }
    this.session.DataManager.ExecuteSpNonQuery("IMS_ADD_OBJECT", this.session.DataManager.Parameter("inID", (object) id), this.session.DataManager.Parameter("inOBJECT_TYPE", (object) this.briefObject.Object.ObjectType), this.session.DataManager.Parameter("inOWNER_ID", (object) this.briefObject.Object.OwnerId), this.session.DataManager.Parameter("inLC_STEP", (object) this.briefObject.Object.Lc_step), this.session.DataManager.Parameter("inGUID", (object) (Guid) this.briefObject.Object.ObjectGuid), this.session.DataManager.Parameter("inOBJECT_VER_TYPE", (object) (int) objRec), this.session.DataManager.Parameter("inCAPTION", (object) caption), dbDataParameter1, dbDataParameter2, this.session.DataManager.Parameter("inPROJECT_ID", (object) projectID), this.session.DataManager.Parameter("inMODIFICATION_ID", (object) 0L), this.session.DataManager.Parameter("inSITE_ID", (object) str1), this.session.DataManager.Parameter("inCREATOR_ID", (object) this.briefObject.Object.CreatorID), this.session.DataManager.OutputParameter("outOBJECT_ID", (object) newObjectID), this.session.DataManager.OutputParameter("outID", (object) newID), this.session.DataManager.OutputParameter("outVERSION_ID", (object) newVersionID));
    newObjectID = Convert.ToInt64(this.session.DataManager.GetOutputParameterValue("outOBJECT_ID"));
    newID = Convert.ToInt64(this.session.DataManager.GetOutputParameterValue("outID"));
    if (str2 != string.Empty)
      this.session.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_SITE_ID = :siteID1 WHERE F_OBJECT_ID = :objID", this.session.DataManager.Parameter("siteID1", (object) str2), this.session.DataManager.Parameter("objID", (object) newObjectID));
    if (id == 0L)
      DBHelper.ExecuteNonQuery((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_GUID_RESOLVE (F_GUID, F_ID, F_CATEGORY_TYPE) VALUES (:guid, :v_id, :typ)", this.session.DataManager.Parameter("guid", (object) (Guid) this.briefObject.Object.IdGuid), this.session.DataManager.Parameter("v_id", (object) newID), this.session.DataManager.Parameter("typ", (object) 2));
    if (parentVersionId <= 0L)
      return;
    IDbDataParameter dbDataParameter3 = this.session.DataManager.Parameter("projID", (object) parentVersionId);
    IDbDataParameter dbDataParameter4 = this.session.DataManager.Parameter("partID", (object) newObjectID);
    DBHelper.ExecuteNonQuery((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_VERSIONS_TREE (F_PARENT_ID, F_OBJECT_ID) VALUES (:projID, :partID)", dbDataParameter3, dbDataParameter4);
  }

  protected virtual bool AfterRefreshAttributes4Container(IDBObject importObj) => false;

  public virtual object Import() => (object) null;

  public virtual bool Import(bool langEquals)
  {
    try
    {
      IDBObjectType objectType = this.session.GetObjectType(this.briefObject.Object.ObjectType, true);
      if (objectType.ObjectType == this.session.IdentHelper.StorageTypeID)
        throw new Exception(LocalizationHolder.rm.GetString("Kernel_952"));
      int conformityLcStep = Helper.GetConformityLCStep(this.session, this.metadata.Tables["IMS_LC_STEPS"], this.briefObject.Object.Lc_step);
      if (conformityLcStep == -1)
      {
        DataRow dataRow = this.metadata.Tables["IMS_LC_STEPS"].Rows.Find((object) this.briefObject.Object.Lc_step);
        if (dataRow != null)
          throw new Exception(string.Format(BriefcaseConsts.ImportLCStepNotFound, dataRow["F_GUID"]));
        throw new Exception(BriefcaseConsts.logLCStepNotFound);
      }
      IDBLifecycleStep lifecycleStep = this.session.GetLifecycleStep(conformityLcStep);
      this.briefObject.Object.Lc_step = lifecycleStep.LCStep;
      this.briefObject.Object.LevelId = lifecycleStep.LevelID;
      FoundObjectInfo foundObjectInfo = this._foundObjects.Find((Predicate<FoundObjectInfo>) (x => x.BriefcaseObjectID == this.briefObject.Object.Object_id));
      IDBObject dbObject1;
      if (foundObjectInfo.DBObjectID == 0L)
      {
        dbObject1 = (IDBObject) null;
      }
      else
      {
        IDBObject dbObject2 = dbObject1 = this.session.GetObject(foundObjectInfo.DBObjectID, true);
      }
      IDBObject dbObj = dbObject1;
      long id = dbObj != null ? dbObj.ID : this.FindID((Guid) this.briefObject.Object.IdGuid);
      long num1;
      if (this.briefObject.Object.OwnerId >= 0L)
      {
        num1 = this.briefObject.Object.OwnerId;
        this.briefObject.Object.OwnerId = this.session.UserID;
      }
      else
      {
        this.briefObject.Object.OwnerId = dbObj != null ? dbObj.OwnerID : -1L * this.briefObject.Object.OwnerId;
        num1 = -1L;
      }
      long num2;
      if (this.briefObject.Object.ProjectId != 0L && this.briefObject.Object.ProjectId != -1L)
      {
        num2 = this.briefObject.Object.ProjectId;
        this.briefObject.Object.ProjectId = 0L;
      }
      else
      {
        if (this.briefObject.Object.ProjectId == -1L)
          this.briefObject.Object.ProjectId = 0L;
        num2 = -1L;
      }
      long num3;
      if (this.briefObject.Object.CreatorID >= 0L)
      {
        num3 = this.briefObject.Object.CreatorID;
        this.briefObject.Object.CreatorID = this.session.UserID;
      }
      else
      {
        this.briefObject.Object.CreatorID = dbObj != null ? dbObj.CreatorID : -1L * this.briefObject.Object.CreatorID;
        num3 = -1L;
      }
      this.RemoveAttribute(MetaDataHelper.GetAttributeTypeID("cad0020f-306c-11d8-b4e9-00304f19f545"));
      return dbObj != null ? this.RefreshOldObject(dbObj, objectType, langEquals, num1, num2, num3) : this.FinalyAddImportedObject(id, num1, num2, num3, false);
    }
    catch (Exception ex)
    {
      this.ErrorException = ex;
      return false;
    }
  }

  private void RemoveAttribute(int attributeID)
  {
    AttributeRecord attributeRecord = this.briefObject.Attributes.Find((Predicate<AttributeRecord>) (x => x.AttributeId == attributeID));
    if (attributeRecord == null)
      return;
    this.briefObject.Attributes.Remove(attributeRecord);
  }
}
