// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseDBObject
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseDBObject : DBClassifier, IImbaseDBObject
{
  private bool _allowSkipSiteCheck;

  internal ImbaseDBObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
  }

  private bool IsArrayEmpty(object[] initValues)
  {
    return initValues == null || !((IEnumerable<object>) initValues).Any<object>((System.Func<object, bool>) (x => x != null && x != DBNull.Value));
  }

  protected override void DoBeforeAddAttribute(int attributeID, object[] initValues)
  {
    if (!this.IsCreationMode && this.ObjectModifyMode == ObjectModifyModes.InBase)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeID);
      if (attributeType != null && !this.IsArrayEmpty(initValues))
      {
        object obj = ((IEnumerable<object>) initValues).FirstOrDefault<object>((System.Func<object, bool>) (x => x != null));
        if (!((IEnumerable<FieldTypes>) TableLoadHelper.ForbiddenAttrTypesForAddToTable).Contains<FieldTypes>(attributeType.FieldType) && attributeID != Intermech.Imbase.Consts.ClassifFolderKeyAttId && this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService)
        {
          if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
          {
            long tableId = this.GetTableID();
            customService.CheckUniqueBeforeAttrInTableRefChange(this.UserSession.SessionGUID, this.ObjectID, tableId, attributeID, obj);
          }
          else if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)
            customService.CheckUniqueBeforeAttrInTableChange(this.UserSession.SessionGUID, this.ObjectID, attributeID, obj);
        }
      }
    }
    base.DoBeforeAddAttribute(attributeID, initValues);
  }

  protected override void DoAfterAddAttribute(IDBAttribute attribute)
  {
    base.DoAfterAddAttribute(attribute);
    if (this.IsCreationMode || this.ObjectModifyMode != ObjectModifyModes.InBase || attribute.DataType == FieldTypes.ftObjectLink || attribute.AttributeID == Intermech.Imbase.Consts.ClassifFolderKeyAttId || ((IEnumerable<FieldTypes>) TableLoadHelper.ForbiddenAttrTypesForAddToTable).Contains<FieldTypes>(attribute.DataType) || !this.IsArrayEmpty(attribute.Values) || !(this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService))
      return;
    if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
    {
      long tableId = this.GetTableID();
      customService.UpdateAfterAttrInTableRefChanged(this.UserSession.SessionGUID, this.ObjectID, tableId, attribute.AttributeID, attribute.Value);
    }
    else
    {
      if (this.ObjectType != Intermech.Imbase.Consts.ImbaseTableTypeID)
        return;
      customService.UpdateAfterAttrInTableChanged(this.UserSession.SessionGUID, this.ObjectID, attribute.AttributeID);
    }
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    if (this.IsCreationMode || this.ObjectModifyMode != ObjectModifyModes.InBase || ((IEnumerable<FieldTypes>) TableLoadHelper.ForbiddenAttrTypesForAddToTable).Contains<FieldTypes>(attribute.DataType) || attribute.AttributeID == Intermech.Imbase.Consts.ClassifFolderKeyAttId || !(this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService))
      return;
    if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
    {
      if (attribute.AttributeID == Intermech.Imbase.Consts.ImbaseTableRefAttID)
      {
        long result = 0;
        if (!long.TryParse(Convert.ToString(newValue), out result) || result == 0L)
          return;
        customService.CheckUniqueBeforeTableRefAttrChange(this.UserSession.SessionGUID, this.ObjectID, result);
      }
      else
      {
        long tableId = this.GetTableID();
        customService.CheckUniqueBeforeAttrInTableRefChange(this.UserSession.SessionGUID, this.ObjectID, tableId, attribute.AttributeID, newValue);
      }
    }
    else
      customService.CheckUniqueBeforeAttrInTableChange(this.UserSession.SessionGUID, this.ObjectID, attribute.AttributeID, newValue);
  }

  protected override void DoAfterSetComplexAttributeValue(IDBAttribute attribute)
  {
    if (!this.IsCreationMode && this.ObjectModifyMode == ObjectModifyModes.InBase && !((IEnumerable<FieldTypes>) TableLoadHelper.ForbiddenAttrTypesForAddToTable).Contains<FieldTypes>(attribute.DataType) && attribute.AttributeID != Intermech.Imbase.Consts.ClassifFolderKeyAttId && this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService)
    {
      if (attribute.AttributeID == Intermech.Imbase.Consts.ImbaseTableRefAttID)
        customService.UpdateAfterTableRefAttrChanged(this.UserSession.SessionGUID, this.ObjectID);
      else if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        long tableId = this.GetTableID();
        customService.UpdateAfterAttrInTableRefChanged(this.UserSession.SessionGUID, this.ObjectID, tableId, attribute.AttributeID, attribute.Value);
      }
      else if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)
        customService.UpdateAfterAttrInTableChanged(this.UserSession.SessionGUID, this.ObjectID, attribute.AttributeID);
    }
    base.DoAfterSetComplexAttributeValue(attribute);
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoAfterSetAdditionalAttributeValue(attribute);
    if (this.IsCreationMode || this.ObjectModifyMode != ObjectModifyModes.InBase || ((IEnumerable<FieldTypes>) TableLoadHelper.ForbiddenAttrTypesForAddToTable).Contains<FieldTypes>(attribute.DataType) || attribute.AttributeID == Intermech.Imbase.Consts.ClassifFolderKeyAttId || !(this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService))
      return;
    if (attribute.DataType != FieldTypes.ftObjectLink)
    {
      if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        long tableId = this.GetTableID();
        customService.UpdateAfterAttrInTableRefChanged(this.UserSession.SessionGUID, this.ObjectID, tableId, attribute.AttributeID, attribute.Value);
      }
      else
      {
        if (this.ObjectType != Intermech.Imbase.Consts.ImbaseTableTypeID)
          return;
        customService.UpdateAfterAttrInTableChanged(this.UserSession.SessionGUID, this.ObjectID, attribute.AttributeID);
      }
    }
    else
    {
      if (!this.IsArrayEmpty(attribute.Values))
        return;
      if (attribute.AttributeID == Intermech.Imbase.Consts.ImbaseTableRefAttID && this.IsArrayEmpty(attribute.Values))
        customService.UpdateAfterTableRefAttrChanged(this.UserSession.SessionGUID, this.ObjectID);
      else if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        long tableId = this.GetTableID();
        customService.UpdateAfterAttrInTableRefChanged(this.UserSession.SessionGUID, this.ObjectID, tableId, attribute.AttributeID, attribute.Value);
      }
      else
      {
        if (this.ObjectType != Intermech.Imbase.Consts.ImbaseTableTypeID)
          return;
        customService.UpdateAfterAttrInTableChanged(this.UserSession.SessionGUID, this.ObjectID, attribute.AttributeID);
      }
    }
  }

  protected override void DoBeforeDeleteAttribute(IDBAttribute attribute)
  {
    if (!this.IsCreationMode && this.ObjectModifyMode == ObjectModifyModes.InBase && !((IEnumerable<FieldTypes>) TableLoadHelper.ForbiddenAttrTypesForAddToTable).Contains<FieldTypes>(attribute.DataType) && attribute.AttributeID != Intermech.Imbase.Consts.ClassifFolderKeyAttId && this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService)
    {
      if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        long tableId = this.GetTableID();
        customService.CheckUniqueBeforeAttrInTableRefDelete(this.UserSession.SessionGUID, this.ObjectID, tableId, attribute.AttributeID);
      }
      else if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)
        customService.CheckUniqueBeforeAttrInTableDelete(this.UserSession.SessionGUID, this.ObjectID, attribute.AttributeID);
    }
    base.DoBeforeDeleteAttribute(attribute);
  }

  protected override void DoAfterDeleteAdditionalAttribute(IDBAttribute attribute)
  {
    base.DoAfterDeleteAdditionalAttribute(attribute);
    if (this.IsCreationMode || this.ObjectModifyMode != ObjectModifyModes.InBase || ((IEnumerable<FieldTypes>) TableLoadHelper.ForbiddenAttrTypesForAddToTable).Contains<FieldTypes>(attribute.DataType) || attribute.AttributeID == Intermech.Imbase.Consts.ClassifFolderKeyAttId || !(this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService))
      return;
    if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
    {
      long tableId = this.GetTableID();
      customService.UpdateAfterAttrInTableRefChanged(this.UserSession.SessionGUID, this.ObjectID, tableId, attribute.AttributeID, (object) null);
    }
    else
    {
      if (this.ObjectType != Intermech.Imbase.Consts.ImbaseTableTypeID)
        return;
      customService.UpdateAfterAttrInTableChanged(this.UserSession.SessionGUID, this.ObjectID, attribute.AttributeID);
    }
  }

  protected override void DoDelete()
  {
    if (this.IsCreationMode)
      return;
    IImbaseIndexingService customService = this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
    try
    {
      if (customService != null && this.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogTypeID && !customService.CheckBeforeObjectDelete(this.UserSession.SessionGUID, this.ObjectID, this.ObjectType))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_CanNotDeleteCatalog"), (object) this.Caption, (object) this.ObjectID)).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
      base.DoDelete();
    }
    catch (IndexingException ex)
    {
      throw new KernelException(ex.Message);
    }
  }

  protected override void DoPurge(long DeleteMode)
  {
    base.DoPurge(DeleteMode);
    if (this.IsCreationMode)
      return;
    if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)
    {
      new ImbaseAttSecurity(this.Session as UserSession, (DBObject) this, 0).PurgeAllAccessData();
      new ImbaseRecordSecurity(this.Session as UserSession, (DBObject) this, 0L).PurgeAllAccessData();
    }
    IImbaseIndexingService customService = this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
    try
    {
      customService?.UpdateAfterObjectDelete(this.UserSession.SessionGUID, this.ObjectID, this.ObjectType);
    }
    catch (IndexingException ex)
    {
      throw new KernelException(ex.Message);
    }
  }

  protected override void DoBeforeCommitCreation()
  {
    if (this.ObjectType != Intermech.Imbase.Consts.ImbaseTableTypeID)
      base.DoBeforeCommitCreation();
    ActionType anAction = ActionType.Unknown;
    if (this.ObjectType == Intermech.Imbase.Consts.ImbaseFolderTypeID || this.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)
      anAction = ActionType.CreateFolderOrRecordInCatalog;
    else if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      anAction = ActionType.CreateTableLinkInCatalog;
    if (anAction == ActionType.Unknown)
      return;
    string asString = this.Attributes.FindByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId)?.AsString;
    if (string.IsNullOrEmpty(asString))
      return;
    string classifKey = asString.Substring(0, 2);
    if (!(ServiceUtils.GetService<IImbaseServer>((object) this.Session, true) is ImbaseServer service) || !(this.Session.GetObject(service.GetCatalogByClassyfKey(this.Session, classifKey)) is DBObject dbObject))
      return;
    dbObject.CheckAccess(anAction, true, true);
  }

  public override IDBObject DoCheckout()
  {
    IDBObject dbObject = base.DoCheckout();
    if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
    {
      try
      {
        long tableReference = TableLoadHelper.GetTableReference((IUserSession) this.UserSession, this.ObjectID);
        if (tableReference != 0L)
        {
          IDBObject objectActualCopy = this.UserSession.GetObjectActualCopy(tableReference, false);
          if (objectActualCopy != null)
          {
            if (objectActualCopy.CheckoutBy == 0L)
            {
              if (objectActualCopy.ObjectModifyMode == ObjectModifyModes.Checkout)
              {
                try
                {
                  objectActualCopy.CheckOut();
                }
                catch (Exception ex)
                {
                  throw new KernelException(ex.Message);
                }
              }
            }
          }
        }
      }
      catch
      {
      }
    }
    else if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)
    {
      List<long> tableRefIdsByTableId = TableLoadHelper.GetListTableRefIDsByTableID((IUserSession) this.UserSession, Math.Abs(this.ObjectID));
      if (tableRefIdsByTableId != null && tableRefIdsByTableId.Count == 1)
      {
        IDBObject objectActualCopy = this.UserSession.GetObjectActualCopy(tableRefIdsByTableId[0], false);
        if (objectActualCopy != null && objectActualCopy.CheckoutBy == 0L)
        {
          if (objectActualCopy.ObjectModifyMode == ObjectModifyModes.Checkout)
          {
            try
            {
              objectActualCopy.CheckOut();
            }
            catch (Exception ex)
            {
              throw new KernelException(ex.Message);
            }
          }
        }
      }
    }
    return dbObject;
  }

  protected override void DoCheckIn()
  {
    IImbaseIndexingService customService = this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
    try
    {
      if (customService != null)
      {
        List<long> keys = (List<long>) null;
        List<int> uIndexes = (List<int>) null;
        DataTable dataTable = (DataTable) null;
        string msg = string.Empty;
        if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        {
          dataTable = customService.CheckUniqueBeforeTableRefCheckIn(this.UserSession.SessionGUID, this.ObjectID, out uIndexes, out keys);
          msg = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_TableRef_NotUniqueData"), (object) this.Caption, (object) this.ObjectID);
        }
        else if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)
        {
          dataTable = customService.CheckUniqueBeforeTableCheckIn(this.UserSession.SessionGUID, this.ObjectID, out uIndexes, out keys);
          msg = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_Table_NotUniqueData"), (object) this.Caption, (object) this.ObjectID);
        }
        if (dataTable == null && (uIndexes == null || uIndexes.Count <= 0))
        {
          if (keys != null)
          {
            if (keys.Count <= 0)
              goto label_11;
          }
          else
            goto label_11;
        }
        throw new NotUniqueIndexValueException(msg)
        {
          NotUniqueIndexes = uIndexes,
          RowNumbers = keys,
          Table = dataTable
        };
      }
    }
    catch (IndexingException ex)
    {
      throw new KernelException(ex.Message);
    }
label_11:
    base.DoCheckIn();
  }

  protected override void DoAfterCheckIn()
  {
    base.DoAfterCheckIn();
    IImbaseIndexingService customService = this.UserSession.GetCustomService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
    try
    {
      if (customService == null)
        return;
      long objectID = 0;
      if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        long num = Math.Abs(this.ObjectID);
        long tableID = 0;
        try
        {
          tableID = TableLoadHelper.GetTableReference((IUserSession) this.UserSession, num);
        }
        catch
        {
        }
        List<long> tableRefIdsByTableId = tableID != 0L ? TableLoadHelper.GetListTableRefIDsByTableID((IUserSession) this.UserSession, Math.Abs(tableID)) : (List<long>) null;
        if (tableRefIdsByTableId != null && tableRefIdsByTableId.Count == 1)
        {
          IDBObject objectActualCopy = this.UserSession.GetObjectActualCopy(Math.Abs(tableID), false);
          objectID = objectActualCopy == null || objectActualCopy.CheckoutBy != this.UserSession.UserID ? 0L : tableID;
        }
        customService.UpdateAfterTableRefCheckIn(this.UserSession.SessionGUID, num, tableID);
      }
      else if (this.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)
      {
        long tableID = Math.Abs(this.ObjectID);
        List<long> tableRefIdsByTableId = TableLoadHelper.GetListTableRefIDsByTableID((IUserSession) this.UserSession, tableID);
        if (tableRefIdsByTableId != null && tableRefIdsByTableId.Count > 0)
        {
          if (tableRefIdsByTableId.Count == 1)
          {
            objectID = tableRefIdsByTableId[0];
            IDBObject objectActualCopy = this.UserSession.GetObjectActualCopy(objectID, false);
            if (objectActualCopy != null && objectActualCopy.CheckoutBy != this.UserSession.UserID)
              customService.UpdateAfterTableCheckIn(this.UserSession.SessionGUID, tableID);
          }
          else
            customService.UpdateAfterTableCheckIn(this.UserSession.SessionGUID, tableID);
        }
      }
      if (objectID == 0L)
        return;
      IDBObject objectActualCopy1 = this.UserSession.GetObjectActualCopy(objectID, false);
      if (objectActualCopy1 == null)
        return;
      if (objectActualCopy1.CheckoutBy != this.UserSession.UserID)
        return;
      try
      {
        objectActualCopy1.CheckIn();
      }
      catch (NotUniqueIndexValueException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        throw new KernelException(ex.Message);
      }
    }
    catch (IndexingException ex)
    {
      throw new KernelException(ex.Message);
    }
  }

  private long GetTableID()
  {
    long tableId = 0;
    IDBAttribute attributeById = this.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
    if (attributeById != null && attributeById.Value != null && attributeById.Value != DBNull.Value)
      tableId = Convert.ToInt64(attributeById.Value);
    return tableId;
  }

  public override bool ReadonlyPublishedObject(bool isRelationCheck)
  {
    return !this._allowSkipSiteCheck && base.ReadonlyPublishedObject(isRelationCheck);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    if (this.ObjectType == Intermech.Imbase.Consts.ImbaseCatalogTypeID)
    {
      this.AccessActions.Add(ActionType.CreateFolderOrRecordInCatalog, true);
      this.AccessActions.Add(ActionType.CreateTableLinkInCatalog, true);
      this.AccessActions.Add(ActionType.ManageCatalogIndexes, false);
    }
    else
    {
      if (this.ObjectType != Intermech.Imbase.Consts.ImbaseTableTypeID)
        return;
      this.AccessActions.Add(ActionType.EditTableStructureAndProperties, true);
      this.AccessActions.Add(ActionType.EditTableData, true);
      this.AccessActions.Add(ActionType.AddNewRows, true);
    }
  }

  public override ActionCategory GetActionCategory(ActionType actionType)
  {
    switch (actionType)
    {
      case ActionType.CreateFolderOrRecordInCatalog:
      case ActionType.CreateTableLinkInCatalog:
      case ActionType.EditTableStructureAndProperties:
      case ActionType.EditTableData:
      case ActionType.ManageCatalogIndexes:
      case ActionType.AddNewRows:
        return ActionCategory.Write;
      default:
        return base.GetActionCategory(actionType);
    }
  }

  public bool AllowSkipSiteCheck
  {
    get => this._allowSkipSiteCheck;
    set => this._allowSkipSiteCheck = value;
  }

  public bool ReadonlyPublished
  {
    get
    {
      try
      {
        return !string.IsNullOrEmpty(this.SiteID) && base.ReadonlyPublishedObject(false);
      }
      catch (Exception ex)
      {
        return true;
      }
    }
  }
}
