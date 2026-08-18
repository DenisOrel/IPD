// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributeType4Object
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;


namespace Intermech.Kernel;

internal class DBAttributeType4Object : 
  DBAttributeType4Category,
  IDBAttributeType4Object,
  IDBAttributeType4,
  IDBAttributeType
{
  internal bool _FreezeUpdateAttributesViewHash;

  public DBAttributeType4Object(UserSession uSession, DataRow row)
    : base(uSession, Convert.ToInt32(row["F_ATTRIBUTE_ID"]), Convert.ToInt32(row["F_OBJECT_TYPE"]), 4)
  {
    this._TableName = "IMS_ATTR4OBJ_TYPES";
    this._KeyName = "F_OBJECT_TYPE";
    this.paramsTable.Create(row);
  }

  protected override DBAttributableType ParentType
  {
    get => this.UserSession.GetObjectType(this._TypeID) as DBAttributableType;
  }

  public int[] GetRelatedFormulaAttributes()
  {
    DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS").Select(string.Format("F_FORMULA_ID = {0} AND F_OBJECT_TYPE = {1} AND F_RELATION_TYPE = -1 AND F_MODE_ID = " + Consts.Attribute4Formula.ToString(), (object) this.AttributeID, (object) this._TypeID));
    int[] formulaAttributes = new int[dataRowArray.Length];
    for (int index = 0; index < dataRowArray.Length; ++index)
      formulaAttributes[index] = Convert.ToInt32(dataRowArray[index]["F_ATTRIBUTE_ID"]);
    return formulaAttributes;
  }

  public object[] GetPossibleValuesArray() => this._AttributeType.GetPossibleValuesArray();

  protected override int CategoryType4EvenLog => 4;

  public override string ObjectName
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Kernel_157"), (object) this.Name, (object) this.UserSession.GetObjectType(this._TypeID).ObjectTypeName);
    }
  }

  private void RebuildInView(
    int objectTypeID,
    OptimizationModes oldMode,
    OptimizationModes newMode,
    bool isEmpty)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DBObjectType objectType = this.UserSession.GetObjectType(objectTypeID) as DBObjectType;
    DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Select($"F_OBJECT_TYPE = {objectTypeID} AND F_ATTRIBUTE_ID <> {this.AttributeID} AND F_INVIEW <> {0}");
    if (newMode == OptimizationModes.Write)
    {
      if (dataRowArray.Length == 0)
      {
        try
        {
          dataManager.SetAdminCommandTimeout();
          dataManager.DataProvider.DropTableIfExists(dataManager, objectType.ViewName);
          return;
        }
        catch
        {
          return;
        }
        finally
        {
          dataManager.SetNormalCommandTimeout();
        }
      }
    }
    else if (dataRowArray.Length == 0)
    {
      try
      {
        dataManager.DataProvider.CheckTableExists(objectType.ViewName, "F_OBJECT_ID", dataManager);
      }
      catch
      {
        List<string> indexesList = new List<string>();
        dataManager.DataProvider.CreateObjectTypeView(objectType.ViewName, "", dataManager, indexesList);
        foreach (string commandText in indexesList)
          dataManager.ExecuteNonQuery(commandText);
        objectType.InsertIntoView(-1);
      }
    }
    string[] indexFieldNames = this._AttributeType.IndexFieldNames;
    string[] fieldNames = this._AttributeType.FieldNames;
    try
    {
      if (oldMode == OptimizationModes.Seek)
      {
        foreach (string fldName in indexFieldNames)
        {
          try
          {
            dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropIndexSQL(objectType.ViewName, fldName, SortOrders.ASC));
          }
          catch (Exception ex)
          {
            this.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_158"), (object) dataManager.DataProvider.GetDropIndexSQL(objectType.ViewName, fldName, SortOrders.ASC), (object) ex.Message), Consts.traceAlways, "sql_errors.log");
          }
        }
      }
      if (newMode == OptimizationModes.Write)
      {
        if (oldMode == OptimizationModes.Seek || oldMode == OptimizationModes.Read)
        {
          foreach (string columnName in fieldNames)
            dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropColumnsSQL(objectType.ViewName, columnName));
        }
      }
      else if (oldMode == OptimizationModes.Write)
      {
        dataManager.ExecuteNonQuery(dataManager.DataProvider.GetAddColumnsSQL(objectType.ViewName, this._AttributeType.ColumnSQL));
        if (!isEmpty)
          this._AttributeType.UpdateViewFields(objectType.ViewName, this.UserSession.DBCache.GetAttributesTableName(this._TypeID), fieldNames, "F_OBJECT_ID");
      }
      if (newMode != OptimizationModes.Seek)
        return;
      foreach (string fldName in indexFieldNames)
        dataManager.ExecuteNonQuery(dataManager.DataProvider.GetIndexSQL(objectType.ViewName, fldName, SortOrders.ASC));
    }
    catch (Exception ex)
    {
      this.EventHelper.AddEvent(0L, 0L, 3, (long) this.AttributeID, string.Format(LocalizationHolder.rm.GetString("Kernel_160"), (object) this.Name, (object) objectType.ObjectTypeName), string.Format(LocalizationHolder.rm.GetString("Kernel_161"), (object) objectType.ViewName, (object) ex.Message), ActionType.EditProperties, EventlogRecordType.Warning, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
    }
  }

  private void RebuildAttributesViewHash(int objTypeID, OptimizationModes mode)
  {
    List<string> stringList = new List<string>();
    Attribute4ID key = new Attribute4ID(this.AttributeID, objTypeID, -1);
    (this.UserSession.DBCache as CacheDataset).AttributesInViewsHash.Remove((object) key);
    string str = SqlHelper.viewForObjectTypePrefix + objTypeID.ToString();
    if (mode == OptimizationModes.Read || mode == OptimizationModes.Seek)
      stringList.Add(str);
    int objectTypeParentId;
    for (objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(objTypeID); objectTypeParentId > -1; objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(objectTypeParentId))
    {
      DataRow dataRow = this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Rows.Find(new object[2]
      {
        (object) this.AttributeID,
        (object) objectTypeParentId
      });
      if (dataRow != null && (Convert.ToInt32(dataRow["F_INVIEW"]) == 1 || Convert.ToInt32(dataRow["F_INVIEW"]) == 2))
        stringList.Add(SqlHelper.viewForObjectTypePrefix + objectTypeParentId.ToString());
    }
    DataRow dataRow1 = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) this.AttributeID);
    if (Convert.ToInt32(dataRow1["F_INVIEW"]) == 1 || Convert.ToInt32(dataRow1["F_INVIEW"]) == 2)
      stringList.Add("IMS_OBJECTS_VIEW");
    string[] array1 = stringList.Count != 0 ? stringList.ToArray() : (string[]) null;
    (this.UserSession.DBCache as CacheDataset).AttributesInViewsHash[(object) key] = (object) new Attribute4Props(mode, array1, this.Options);
    if (!((this.UserSession.DBCache as CacheDataset).AttributesInViewsHash[(object) new Attribute4ID(-1, objTypeID, -1)] is Attribute4Props attribute4Props))
    {
      stringList.Clear();
      if (!this.UserSession.GetObjectType(objTypeID).IsLocalType)
      {
        if (this.OptimizedAttributeExists(objTypeID, mode))
          stringList.Add(SqlHelper.viewForObjectTypePrefix + objTypeID.ToString());
        for (objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(objTypeID); objectTypeParentId > -1; objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(objectTypeParentId))
        {
          if (this.OptimizedAttributeExists(objectTypeParentId, OptimizationModes.Write))
            stringList.Add(SqlHelper.viewForObjectTypePrefix + objectTypeParentId.ToString());
        }
        stringList.Add("IMS_OBJECTS_VIEW");
      }
      else
        stringList.Add(SqlHelper.viewForObjectTypePrefix + objTypeID.ToString());
      string[] array2 = stringList.ToArray();
      (this.UserSession.DBCache as CacheDataset).AttributesInViewsHash[(object) new Attribute4ID(-1, objTypeID, -1)] = (object) new Attribute4Props(OptimizationModes.Seek, array2, AttributeOptions.None);
    }
    else
    {
      if (attribute4Props.Tables == null)
        return;
      bool flag = false;
      for (int index = 0; index < attribute4Props.Tables.Length; ++index)
      {
        if (attribute4Props.Tables[index] == str)
        {
          flag = true;
          break;
        }
      }
      if (mode != OptimizationModes.Read && mode != OptimizationModes.Seek || flag)
        return;
      string[] strArray = new string[attribute4Props.Tables.Length + 1];
      for (int index = 0; index < attribute4Props.Tables.Length; ++index)
        strArray[index] = attribute4Props.Tables[index];
      strArray[attribute4Props.Tables.Length] = str;
      attribute4Props.Tables = strArray;
    }
  }

  private bool OptimizedAttributeExists(int objTypeID, OptimizationModes mode)
  {
    bool flag = mode == OptimizationModes.Read || mode == OptimizationModes.Seek;
    if (!flag)
    {
      DataTable dataTable = this.UserSession.GetObjectType(objTypeID).Attributes.Select(string.Empty);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        switch ((OptimizationModes) Convert.ToInt32(dataTable.Rows[index]["F_INVIEW"]))
        {
          case OptimizationModes.Read:
          case OptimizationModes.Seek:
            flag = true;
            goto label_6;
          default:
            continue;
        }
      }
    }
label_6:
    return flag;
  }

  public OptimizationModes OptimizationMode
  {
    get => (OptimizationModes) Convert.ToInt32(this.paramsTable[44]);
    set
    {
      if (this.OptimizationMode == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_162"), (object) this._AttributeType.Name, (object) OptimizationModesHelper.GetCaption(value)));
      this.CheckChangeEnable("F_INVIEW");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        foreach (int objectTypeID in objectTypesForModify)
          this.RebuildInView(objectTypeID, this.OptimizationMode, value, false);
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num in objectTypesForModify)
          {
            stringBuilder.Append(num.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_INVIEW = :inView WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("inView", (object) Convert.ToInt32((object) value)), dataManager.Parameter("objType", (object) num), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_INVIEW", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
          this.UserSession.DBCache.EnterReadLocker();
          try
          {
            ((CacheDataset) this.UserSession.DBCache).FillAttributeID4ObjectHash(this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES"), this.UserSession.DataManager);
          }
          finally
          {
            this.UserSession.DBCache.ExitReadLocker();
          }
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12466(), (object) Convert.ToInt32((object) value), (object) objectTypesForModify[0], (object) this.AttributeID));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_INVIEW", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
          this.RebuildAttributesViewHash(objectTypesForModify[0], value);
        }
        this.paramsTable[44] = (object) Convert.ToInt32((object) value);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_163"), (object) this._AttributeType.Name, (object) ex.Message);
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        (this.UserSession.DBCache as CacheDataset).FillAttributeID4ObjectHash(this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES"), this.UserSession.DataManager);
        if (!(ex is AccessDeniedException))
          throw new KernelException(str, ex);
        throw;
      }
    }
  }

  private DataRow[] GetParentAttributeRow(int typeID)
  {
    DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE").Select("F_OBJECT_TYPE = " + typeID.ToString());
    if (dataRowArray.Length == 0)
      this.RaiseNotFoundException();
    int int32 = Convert.ToInt32(dataRowArray[0]["F_PARENT_ID"]);
    DataRow[] parentAttributeRow = this.UserSession.DBCache.GetTable(this._TableName).Select($"F_ATTRIBUTE_ID = {this._AttributeType.AttributeID} AND F_OBJECT_TYPE = {int32}");
    if (parentAttributeRow.Length == 0)
      return this.GetParentAttributeRow(int32);
    if (Convert.ToInt32(parentAttributeRow[0]["F_PUBLIC"]) != 0)
      return parentAttributeRow;
    this.RaiseNotFoundException();
    return (DataRow[]) null;
  }

  private void RaiseNotFoundException()
  {
    IDBObjectType objectType = this.UserSession.GetObjectType(this._TypeID);
    throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString(sc_12465.ssp_appserver_12467()), (object) objectType.ObjectTypeName, (object) this._AttributeType.Name));
  }

  protected override long ValidateEditMode(string note) => base.ValidateEditMode(note);

  private void ValidateDeleteTypeTree(bool includeThis)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (int anObjectTypeID in this.GetObjectTypesForModify())
    {
      if (includeThis || anObjectTypeID != this._TypeID)
      {
        IDBObjectType objectType = this.UserSession.GetObjectType(anObjectTypeID);
        if (objectType.IsLocalType)
        {
          if (!objectType.AnyAttributes)
          {
            DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable(string.Format("SELECT DISTINCT O.F_OBJECT_ID FROM IMS_OBJECTS O, {3} A WHERE O.F_OBJECT_TYPE = {0} AND O.F_OBJECT_VER_TYPE <> -1 AND O.F_LEVEL_ID <> {1} AND A.F_OBJECT_ID = O.F_OBJECT_ID AND A.F_ATTRIBUTE_ID = {2}", (object) objectType.ObjectType, (object) this.UserSession.IdentHelper.DeletedID, (object) this.AttributeID, (object) (objectType as DBObjectType).AttributesTableName));
            if (dataTable.Rows.Count > 0)
            {
              string str = !this.IsContent ? string.Empty : sc_12465.ssp_appserver_12468();
              long[] objectsID = new long[dataTable.Rows.Count];
              for (int index = 0; index < dataTable.Rows.Count; ++index)
                objectsID[index] = Convert.ToInt64(dataTable.Rows[index][0]);
              throw new ObjectsFoundException(string.Format(sc_12465.ssp_appserver_12469(), (object) this.Name, (object) dataTable.Rows.Count, (object) objectType.ObjectTypeName, (object) str), $"Объекты с атрибутом '{this.Name}':", objectsID);
            }
          }
        }
        else if (!objectType.AnyAttributes)
          stringBuilder.Append(anObjectTypeID.ToString() + ",");
      }
    }
    if (stringBuilder.Length <= 0)
      return;
    --stringBuilder.Length;
    DataTable dataTable1 = this.UserSession.DataManager.ExecuteDataTable($"SELECT DISTINCT O.F_OBJECT_ID FROM IMS_OBJECTS O, IMS_OBJECT_ATTRS A WHERE O.F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND O.F_OBJECT_VER_TYPE <> -1 AND O.F_LEVEL_ID <> {this.UserSession.IdentHelper.DeletedID} AND A.F_OBJECT_ID = O.F_OBJECT_ID AND A.F_ATTRIBUTE_ID = {this.AttributeID}");
    if (dataTable1.Rows.Count > 0)
    {
      string str = !this.IsContent ? string.Empty : sc_12465.ssp_appserver_12470();
      IDBObjectType objectType = this.UserSession.GetObjectType(this._TypeID);
      long[] objectsID = new long[dataTable1.Rows.Count];
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
        objectsID[index] = Convert.ToInt64(dataTable1.Rows[index][0]);
      throw new ObjectsFoundException(string.Format(sc_12465.ssp_appserver_12471(), (object) this.Name, (object) objectType.ObjectTypeName, (object) dataTable1.Rows.Count, (object) str), $"Объекты с атрибутом '{this.Name}':", objectsID);
    }
  }

  private void ModifyField(string fldName, int fldIndex, object newValue)
  {
    if (!(this.paramsTable[fldIndex].ToString() != newValue.ToString()))
      return;
    this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12472(), (object) fldName), this.UserSession.DataManager.Parameter("val", newValue), this.UserSession.DataManager.Parameter("typeID", (object) this._TypeID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID));
    this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4OBJ_TYPES", fldName, newValue, (IUserSession) this.UserSession);
    this.paramsTable[fldIndex] = newValue;
  }

  protected override void DoDelete(long DeleteMode)
  {
    if (!this.UserSession.CanChangeObjectElement(4, (object) this._TypeID, ObligatoryElementKeys.GetKeyForAttributePresence(this._AttributeType.AttributeID)))
    {
      bool flag = true;
      int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(this._TypeID);
      if (objectTypeParentId != -1 && this.UserSession.GetObjectType(objectTypeParentId).Attributes.GetAttributeByID(this._AttributeType.AttributeID) is IDBAttributeType4Object attributeById && attributeById.InheritMode != InheritModes.Private && (attributeById.Computed == this.Computed || this.CheckChangeEnable("F_COMPUTED", false)) && (attributeById.DefaultValue == this.DefaultValue || this.CheckChangeEnable("F_DEFAULT_VALUE", false)) && (attributeById.Formula == this.Formula || this.CheckChangeEnable("F_FORMULA", false)) && (attributeById.IsContent == this.IsContent || this.CheckChangeEnable("F_CONTENT", false)) && (attributeById.LevelID == this.LevelID || this.CheckChangeEnable("F_LEVEL_ID", false)) && (attributeById.MasterAttributeID == this.MasterAttributeID || this.CheckChangeEnable("F_MASTER_ID", false)) && (attributeById.OptimizationMode == this.OptimizationMode || this.CheckChangeEnable("F_INVIEW", false)) && (attributeById.Options == this.Options || this.CheckChangeEnableOptions(attributeById.Options, false)) && (attributeById.SourceAttributeID == this.SourceAttributeID || this.CheckChangeEnable("F_SOURCE_ID", false)) && (attributeById.PropertiesStructure.Unique == this.UniqueMode || this.CheckChangeEnable("F_UNIQUE", false)))
        flag = false;
      if (flag)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_908"), (object) this.Name));
    }
    if (this._AttributeType.AttributeType == FieldTypes.ftObjectLink)
    {
      foreach (int anObjectTypeID in this.GetObjectTypesForModify())
      {
        DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Select($"F_MASTER_ID = {this.AttributeID} AND F_OBJECT_TYPE = {anObjectTypeID}");
        if (dataRowArray.Length != 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (DataRow dataRow in dataRowArray)
            stringBuilder.AppendFormat("{0}'{1}', ", (object) stringBuilder, (object) this.UserSession.GetAttributeType(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"])).Name);
          stringBuilder.Length -= 2;
          throw new KernelExceptionID(sc_12465.ssp_appserver_12473(324187754), (object) this.UserSession.GetObjectType(anObjectTypeID).ObjectTypeName, (object) stringBuilder.ToString());
        }
      }
    }
    if (this.InheritMode == InheritModes.Inherited)
      throw new KernelExceptionID(sc_12465.ssp_appserver_12474(1286430076), (object) this._AttributeType.Name, (object) this.UserSession.GetObjectType(this._TypeID).ObjectTypeName);
    if ((DeleteMode & (long) Consts.DeleteInstances) == (long) Consts.DeleteInstances && this.IsContent)
      throw new KernelExceptionID(sc_12465.ssp_appserver_12475(226900001), (object) this.Name);
    IDBObjectType objectType = this.UserSession.GetObjectType(this._TypeID);
    DBAttributeType4Object attributeType4Object = (DBAttributeType4Object) null;
    if (objectType.ParentTypeID > -1)
      attributeType4Object = this.UserSession.GetObjectType(objectType.ParentTypeID).Attributes.GetAttributeByID(this.AttributeID, false) as DBAttributeType4Object;
    byte num = attributeType4Object != null ? (attributeType4Object.InheritMode != InheritModes.Private ? (this.InheritMode != InheritModes.Private ? (byte) 2 : (byte) 1) : (this.InheritMode != InheritModes.Private ? (byte) 5 : (byte) 3)) : (this.InheritMode != InheritModes.Private ? (byte) 6 : (byte) 4);
    if (num > (byte) 2)
    {
      this.DeleteInheritAttribute(DeleteMode, true);
      if (!objectType.AnyAttributes && objectType.CaptionAttribute == this.AttributeID)
        objectType.CaptionAttribute = 0;
      base.DoDelete(DeleteMode);
      this.RebuildAttributesViewHash(this._TypeID, OptimizationModes.Write);
      this.RebuildInView(this._TypeID, this.OptimizationMode, OptimizationModes.Write, false);
    }
    else
    {
      OptimizationModes optimizationMode = this.OptimizationMode;
      this.ModifyField("F_PUBLIC", 85, (object) 2);
      this.ModifyField("F_REQUIRED", 84, (object) (int) attributeType4Object.Required);
      this.ModifyField("F_VALIDATION_RULE", 83, (object) attributeType4Object.ValidationRule);
      this.ModifyField("F_COMPUTED", 107, (object) (int) attributeType4Object.Computed);
      this.ModifyField("F_FORMULA", 73, (object) attributeType4Object.Formula);
      this.ModifyField("F_UNIQUE", 59, (object) (int) attributeType4Object.UniqueMode);
      this.ModifyField("F_LEVEL_ID", 72, (object) attributeType4Object.LevelID);
      this.ModifyField("F_DEFAULT_VALUE", 104, attributeType4Object.DefaultValue);
      this.ModifyField("F_INVIEW", 44, (object) (int) attributeType4Object.OptimizationMode);
      this.ModifyField("F_OPTIONS", 36, (object) (int) attributeType4Object.Options);
      this.ModifyField("F_CONTENT", 39, (object) (attributeType4Object.IsContent ? 1 : 0));
      this.ModifyField("F_MASK", 35, (object) attributeType4Object.Mask);
      this.ModifyField("F_MASTER_ID", 172, (object) attributeType4Object.MasterAttributeID);
      this.ModifyField("F_SOURCE_ID", 173, (object) attributeType4Object.SourceAttributeID);
      if (num == (byte) 1)
        this.AddInheritAttribute(-1, false);
      if (optimizationMode == attributeType4Object.OptimizationMode)
        return;
      foreach (int objectTypeID in this.GetObjectTypesForModify())
        this.RebuildInView(objectTypeID, optimizationMode, this.OptimizationMode, false);
    }
  }

  public override int LevelID
  {
    get => Convert.ToInt32(this.paramsTable[72]);
    set
    {
      if (this.LevelID == value)
        return;
      string str1 = value != 0 ? this.UserSession.GetLifecycleLevel(value).LevelName : LocalizationHolder.rm.GetString("Kernel_167");
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_168"), (object) this._AttributeType.Name, (object) str1));
      this.CheckChangeEnable("F_LEVEL_ID");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num in objectTypesForModify)
          {
            stringBuilder.Append(num.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_LEVEL_ID = :levelID WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("levelID", (object) value), dataManager.Parameter("objType", (object) num), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_LEVEL_ID", (object) value, (IUserSession) this.UserSession);
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12476(), (object) value, (object) objectTypesForModify[0], (object) this.AttributeID));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_LEVEL_ID", (object) value, (IUserSession) this.UserSession);
        }
        this.paramsTable[72] = (object) value;
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str2 = string.Format(LocalizationHolder.rm.GetString("Kernel_169"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str2);
        throw new KernelException(str2, ex);
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
          throw new KernelExceptionID(sc_12465.ssp_appserver_12477(1877637124), (object) name);
      }
      else
        name = LocalizationHolder.rm.GetString("Kernel_170");
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_171"), (object) this._AttributeType.Name, (object) name));
      this.CheckChangeEnable("F_MASTER_ID");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        if (value != 0)
        {
          if (this.UserSession.GetAttributeType(value, true).AttributeType != FieldTypes.ftObjectLink)
            throw new KernelExceptionID(sc_12465.ssp_appserver_12478(1422077879));
          if (this.UserSession.GetObjectType(this._TypeID).Attributes.GetAttributeByID(value, false) == null)
            throw new KernelExceptionID(sc_12465.ssp_appserver_12479(1560519580), (object) this.UserSession.GetAttributeType(value, true).Name, (object) this.UserSession.GetObjectType(this._TypeID).ObjectTypeName);
        }
        else
          this.SourceAttributeID = 0;
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num in objectTypesForModify)
          {
            stringBuilder.Append(num.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_MASTER_ID = :masterID WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("masterID", (object) value), dataManager.Parameter("objType", (object) num), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_MASTER_ID", (object) value, (IUserSession) this.UserSession);
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12480(), (object) value, (object) objectTypesForModify[0], (object) this.AttributeID));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_MASTER_ID", (object) value, (IUserSession) this.UserSession);
        }
        this.paramsTable[172] = (object) value;
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_172"), (object) this._AttributeType.Name, (object) ex.Message);
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
      string str1 = value == 0 ? LocalizationHolder.rm.GetString("Kernel_173") : this.UserSession.GetAttributeType(value).Name;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_174"), (object) this._AttributeType.Name, (object) str1));
      this.CheckChangeEnable("F_SOURCE_ID");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        if (value != 0)
        {
          if (this.MasterAttributeID == 0)
            throw new KernelExceptionID(sc_12465.ssp_appserver_12481(2084989347));
          IDBAttributeType attributeType = this.UserSession.GetAttributeType(value, true);
          if (attributeType.AttributeType != this.AttributeType)
            throw new KernelExceptionID(sc_12465.ssp_appserver_12482(778272357));
          this.ValidateAssign(attributeType);
        }
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num in objectTypesForModify)
          {
            stringBuilder.Append(num.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_SOURCE_ID = :sourceID WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("sourceID", (object) value), dataManager.Parameter("objType", (object) num), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_SOURCE_ID", (object) value, (IUserSession) this.UserSession);
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12483(), (object) value, (object) objectTypesForModify[0], (object) this.AttributeID));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_SOURCE_ID", (object) value, (IUserSession) this.UserSession);
        }
        this.paramsTable[173] = (object) value;
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str2 = string.Format(LocalizationHolder.rm.GetString("Kernel_175"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str2);
        throw new KernelException(str2, ex);
      }
    }
  }

  public override DataTable GetPossibleValues() => this._AttributeType.GetPossibleValues();

  public override DataRow[] GetPossibleValuesRows() => this._AttributeType.GetPossibleValuesRows();

  public override void DoSetPossibleValues(DataTable valuesTable)
  {
    this._AttributeType.SetPossibleValues(valuesTable, this._TypeID, -1);
  }

  public override object DefaultValue
  {
    set
    {
      if (this._AttributeType.CompareValues(this.DefaultValue, value))
        return;
      if (this.AttributeType == FieldTypes.ftDouble && value != null && value.ToString() != string.Empty)
        value = (object) Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
      long EventID = this.ValidateEditMode(value != null ? string.Format(LocalizationHolder.rm.GetString("Kernel_176"), (object) this._AttributeType.Name, (object) value.ToString()) : string.Format(LocalizationHolder.rm.GetString("Kernel_177"), (object) this._AttributeType.Name));
      this.CheckChangeEnable("F_DEFAULT_VALUE");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        this._AttributeType.ValidateDefaultValue(value);
        string str = value != null ? Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture) : "";
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num in objectTypesForModify)
          {
            stringBuilder.Append(num.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_DEFAULT_VALUE = :defVal1 WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("defVal1", (object) str), dataManager.Parameter("objType", (object) num), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_DEFAULT_VALUE", (object) str, (IUserSession) this.UserSession);
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12484(), (object) SqlHelper.QString(str), (object) objectTypesForModify[0], (object) this.AttributeID));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_DEFAULT_VALUE", (object) str, (IUserSession) this.UserSession);
        }
        this.paramsTable[104] = (object) str;
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_178"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public override bool IsContent
  {
    set
    {
      if (this.IsContent == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_179"), (object) this._AttributeType.Name, (object) Consts.ConvertBoolToString(value)));
      this.CheckChangeEnable("F_CONTENT");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num in objectTypesForModify)
          {
            stringBuilder.Append(num.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_CONTENT = :val WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", this.UserSession.DataManager.Parameter("val", (object) (value ? 1 : 0)), dataManager.Parameter("objType", (object) num), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_CONTENT", (object) (value ? 1 : 0), (IUserSession) this.UserSession);
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12485(), (object) objectTypesForModify[0], (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) (value ? 1 : 0)));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_CONTENT", (object) (value ? 1 : 0), (IUserSession) this.UserSession);
        }
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

  public override UniqueValueModes UniqueMode
  {
    get => (UniqueValueModes) Convert.ToInt32(this.paramsTable[59]);
    set
    {
      if (this.UniqueMode == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_180"), (object) this._AttributeType.Name, (object) UniqueValueModesHelper.GetCaption(value)));
      this.CheckChangeEnable("F_UNIQUE");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        if (value != UniqueValueModes.NotUnique && !this._AttributeType.UniquedAttribute)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12465.ssp_appserver_12486()), (object) this._AttributeType.TypeCaption));
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num in objectTypesForModify)
          {
            stringBuilder.Append(num.ToString() + ",");
            this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_UNIQUE = :uniqVal WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("uniqVal", (object) Convert.ToInt32((object) value)), dataManager.Parameter("objType", (object) num), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_UNIQUE", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12487(), (object) Convert.ToInt32((object) value), (object) objectTypesForModify[0], (object) this.AttributeID));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_UNIQUE", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        }
        this.paramsTable[59] = (object) Convert.ToInt32((object) value);
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_182"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public AttributeTypeProperties PropertiesStructure
  {
    get
    {
      return new AttributeTypeProperties(this.AttributeID, this.Name, this.ShortName, this.Alias, this.Note, this.AttributeType, this.DefaultValue, this.MultipleValued, this.Computed, this.SizeType, this.Formula, this.UniqueMode, this.LevelID, this.LanguageID, this.SubjectAreas, this.GUID, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID);
    }
    set => throw new OperationNotApplicableException();
  }

  private void AddInheritAttribute(int fromObjTypeID, int toObjectType, bool isEmpty)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    List<int> intList = new List<int>();
    if (toObjectType >= 0)
    {
      intList.Add(toObjectType);
    }
    else
    {
      this.UserSession.DBCache.EnterReadLocker();
      try
      {
        foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE").Select("F_PARENT_ID = " + fromObjTypeID.ToString()))
          intList.Add(Convert.ToInt32(dataRow["F_OBJECT_TYPE"]));
      }
      finally
      {
        this.UserSession.DBCache.ExitReadLocker();
      }
    }
    for (int index = 0; index < intList.Count; ++index)
    {
      this.UserSession.DBCache.EnterReadLocker();
      DataRow[] dataRowArray;
      try
      {
        dataRowArray = this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Select($"F_OBJECT_TYPE = {intList[index]} AND F_ATTRIBUTE_ID = {this.AttributeID}");
      }
      finally
      {
        this.UserSession.DBCache.ExitReadLocker();
      }
      if (dataRowArray.Length == 0)
      {
        int int32 = Convert.ToInt32(intList[index]);
        IDbDataParameter dbDataParameter1 = dataManager.Parameter("attrID", (object) this.AttributeID);
        IDbDataParameter dbDataParameter2 = dataManager.Parameter("typeID", (object) int32);
        dataManager.ExecuteNonQuery("INSERT INTO IMS_ATTR4OBJ_TYPES (F_ATTRIBUTE_ID, F_OBJECT_TYPE, F_PUBLIC, F_REQUIRED, F_VALIDATION_RULE, F_COMPUTED, F_FORMULA, F_UNIQUE, F_LEVEL_ID, F_DEFAULT_VALUE, F_INVIEW, F_CONTENT, F_OPTIONS, F_MASK, F_MASTER_ID, F_SOURCE_ID) VALUES (:attrID, :typeID, :inherit1, :required1, :valid_rule, :computed1, :formula1, :unique1, :level1, :def_value, :inview, :content1, :opt, :mask1, :master1, :source1)", dbDataParameter1, dbDataParameter2, dataManager.Parameter("inherit1", (object) Convert.ToInt32((object) InheritModes.Inherited)), dataManager.Parameter("required1", (object) Convert.ToInt32((object) this.Required)), dataManager.Parameter("valid_rule", (object) this.ValidationRule), dataManager.Parameter("computed1", (object) Convert.ToInt32((object) this.Computed)), dataManager.Parameter("formula1", (object) this.Formula), dataManager.Parameter("unique1", (object) Convert.ToInt32((object) this.UniqueMode)), dataManager.Parameter("level1", (object) this.LevelID), dataManager.Parameter("def_value", (object) this.DefaultValue.ToString()), dataManager.Parameter("inview", (object) Convert.ToInt32((object) this.OptimizationMode)), dataManager.Parameter("content1", (object) (this.IsContent ? 1 : 0)), dataManager.Parameter("opt", (object) (int) this.Options), dataManager.Parameter("mask1", (object) this.Mask), dataManager.Parameter("master1", (object) this.MasterAttributeID), dataManager.Parameter("source1", (object) this.SourceAttributeID));
        DataTable dataTable1 = dataManager.ExecuteDataTable("SELECT TA.*, A.F_AREA_ID, A.F_LANGUAGE_ID, A.F_ATTRIBUTE_TYPE FROM IMS_ATTR4OBJ_TYPES TA, IMS_ATTRIBUTES A WHERE TA.F_ATTRIBUTE_ID = :attrID AND TA.F_OBJECT_TYPE = :typeID AND A.F_ATTRIBUTE_ID = TA.F_ATTRIBUTE_ID", dbDataParameter1, dbDataParameter2);
        if (dataTable1.Rows.Count > 0)
          this.UserSession.DBCache.AddRow("IMS_ATTR4OBJ_TYPES", dataTable1.Rows[0], (IUserSession) this.UserSession);
        if ((this.Required == RequiredModes.Auto || this.Required == RequiredModes.AutoRequired) && !isEmpty)
        {
          DataTable dataTable2;
          if (this.LevelID > 0)
            dataTable2 = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :typeID AND F_LEVEL_ID = :levelID", dbDataParameter2, dataManager.Parameter("levelID", (object) this.LevelID));
          else
            dataTable2 = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :typeID", dbDataParameter2);
          foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          {
            IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(row[0]), false);
            if (dbObject != null)
              (dbObject.Attributes as DBObjectAttributeCollection).AddAttribute(this.AttributeID, false, false);
          }
        }
        if (this.Computed != ComputeValueModes.NotComputableValue)
          this._AttributeType.SaveFormulaLinks(int32, -1, this.Formula, Consts.Attribute4Formula, true);
        if (this.OptimizationMode == OptimizationModes.Read || this.OptimizationMode == OptimizationModes.Seek)
          this.RebuildInView(int32, OptimizationModes.Write, this.OptimizationMode, isEmpty);
        if (!this._FreezeUpdateAttributesViewHash)
          this.RebuildAttributesViewHash(int32, this.OptimizationMode);
        this.AddInheritAttribute(int32, -1, false);
      }
    }
  }

  internal void AddInheritAttribute(int toObjTypeID, bool isEmpty)
  {
    this.AddInheritAttribute(this._TypeID, toObjTypeID, isEmpty);
  }

  internal void DeleteInheritAttribute(long deleteMode, bool includeThis)
  {
    if ((deleteMode & (long) Consts.DeleteInstances) == 0L)
      this.ValidateDeleteTypeTree(includeThis);
    int[] objectTypesForModify = this.GetObjectTypesForModify();
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("p2", (object) this.AttributeID);
    foreach (int num in objectTypesForModify)
    {
      if (num != this._TypeID)
      {
        dataManager.ExecuteNonQuery("DELETE FROM IMS_ATTR4OBJ_TYPES WHERE F_OBJECT_TYPE = :p1 AND F_ATTRIBUTE_ID = :p2", dataManager.Parameter("p1", (object) num), dbDataParameter);
        this.UserSession.DBCache.DeleteRecords("IMS_ATTR4OBJ_TYPES", $"F_OBJECT_TYPE = {num} AND F_ATTRIBUTE_ID = {this.AttributeID}", (IUserSession) this.UserSession);
        this.RebuildAttributesViewHash(num, OptimizationModes.Write);
        this.RebuildInView(num, this.OptimizationMode, OptimizationModes.Write, false);
      }
      if ((deleteMode & (long) Consts.DeleteInstances) == (long) Consts.DeleteInstances)
      {
        string commandText = this.Required != RequiredModes.AutoRequired ? $"SELECT O.F_OBJECT_ID FROM IMS_OBJECTS O, {this.UserSession.DBCache.GetAttributesTableName(num)} A WHERE O.F_OBJECT_TYPE = :ot AND O.F_OBJECT_VER_TYPE <> -1 AND A.F_OBJECT_ID = O.F_OBJECT_ID AND A.F_ATTRIBUTE_ID = {this.AttributeID.ToString()}" : "SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :ot AND F_OBJECT_VER_TYPE <> -1";
        foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable(commandText, dataManager.Parameter("ot", (object) num)).Rows)
        {
          IDBAttribute attributeById = this.UserSession.GetObject(Convert.ToInt64(row[0])).GetAttributeByID(this.AttributeID);
          if (attributeById != null)
            (attributeById as DBAttribute).Purge(false);
        }
      }
    }
  }

  public InheritModes InheritMode
  {
    get => (InheritModes) Convert.ToInt32(this.paramsTable[85]);
    set
    {
      if (this.InheritMode == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_183"), (object) this._AttributeType.Name, (object) EnumTypeHelper.GetCaption((Enum) value)));
      this.CheckChangeEnable("F_PUBLIC");
      this.UserSession.StartTransaction();
      try
      {
        if (value == InheritModes.Inherited)
          throw new KernelExceptionID(sc_12465.ssp_appserver_12488(1802715481));
        IDbManager dataManager = this.UserSession.DataManager;
        if (value == InheritModes.Private)
        {
          if (this.InheritMode == InheritModes.Public)
            this.DeleteInheritAttribute(0L, false);
        }
        else
          this.AddInheritAttribute(-1, false);
        string commandText = string.Format(sc_12465.ssp_appserver_12489(), (object) Convert.ToInt32((object) value), (object) this._TypeID, (object) this.AttributeID);
        dataManager.ExecuteNonQuery(commandText);
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4OBJ_TYPES", "F_PUBLIC", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[85] = (object) Convert.ToInt32((object) value);
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_184"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, this.UserSession.DataManager, "IMS_ATTR4OBJ_TYPES");
        if (!(ex is AccessDeniedException))
          throw new KernelException(str, ex);
        throw;
      }
    }
  }

  private void AddObjectChildsForModify(int objectTypeID, ArrayList lst)
  {
    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE").Select("F_PARENT_ID = " + objectTypeID.ToString()))
    {
      int int32 = Convert.ToInt32(dataRow["F_OBJECT_TYPE"]);
      if (this.UserSession.GetObjectType(int32).Attributes.GetAttributeByID(this.AttributeID, false) is IDBAttributeType4Object attributeById && attributeById.InheritMode == InheritModes.Inherited)
      {
        lst.Add((object) int32);
        this.AddObjectChildsForModify(int32, lst);
      }
    }
  }

  public int[] GetObjectTypesForModify()
  {
    ArrayList lst = new ArrayList();
    lst.Add((object) this._TypeID);
    if (this.InheritMode == InheritModes.Public)
      this.AddObjectChildsForModify(this._TypeID, lst);
    return (int[]) lst.ToArray(this._TypeID.GetType());
  }

  public override RequiredModes Required
  {
    set
    {
      if (this.Required == value)
        return;
      RequiredModes required = this.Required;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_185"), (object) this._AttributeType.Name, (object) EnumTypeHelper.GetCaption((Enum) value)));
      this.CheckChangeEnable("F_REQUIRED");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        int index;
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num in objectTypesForModify)
          {
            stringBuilder.Append(num.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_REQUIRED = :req1 WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("req1", (object) Convert.ToInt32((object) value)), dataManager.Parameter("objType", (object) num), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_REQUIRED", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        }
        else
        {
          dataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12490(), (object) Convert.ToInt32((object) value), (object) objectTypesForModify[0], (object) this.AttributeID));
          ICacheDataset dbCache = this.UserSession.DBCache;
          index = this.AttributeID;
          string filterStr = $"F_ATTRIBUTE_ID = {index.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}";
          __Boxed<int> int32 = (System.ValueType) Convert.ToInt32((object) value);
          UserSession userSession = this.UserSession;
          dbCache.ChangeTableValue(filterStr, "IMS_ATTR4OBJ_TYPES", "F_REQUIRED", (object) int32, (IUserSession) userSession);
        }
        this.paramsTable[84] = (object) Convert.ToInt32((object) value);
        if (value == RequiredModes.AutoRequired || value == RequiredModes.Auto)
        {
          string commandText = "SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :ot";
          if (this.LevelID > 0)
          {
            string str1 = commandText;
            index = this.LevelID;
            string str2 = index.ToString();
            commandText = $"{str1} AND F_LEVEL_ID = {str2}";
          }
          int[] numArray = objectTypesForModify;
          for (index = 0; index < numArray.Length; ++index)
          {
            int num = numArray[index];
            foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable(commandText, dataManager.Parameter("ot", (object) num)).Rows)
            {
              IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(row[0]), false);
              if (dbObject != null)
              {
                if (this.AutoPatchMode)
                  (dbObject.Attributes as DBObjectAttributeCollection)._AssignMode = 4096 /*0x1000*/;
                (dbObject.Attributes as DBObjectAttributeCollection).AddAttribute(this.AttributeID, false, false);
              }
            }
          }
        }
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {this._TypeID.ToString()}", "IMS_ATTR4OBJ_TYPES", "F_REQUIRED", (object) Convert.ToInt32((object) required), (IUserSession) this.UserSession);
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_186"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        if (!(ex is AccessDeniedException))
          throw new KernelException(str, ex);
        throw;
      }
    }
  }

  public override AttributeOptions Options
  {
    set
    {
      if (this.Options == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_187"), (object) this._AttributeType.Name, (object) AttributeOptionsHelper.GetCaptions(value)));
      this.CheckChangeEnableOptions(value);
      this._AttributeType.ValidateOptions(value);
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        bool flag = false;
        if ((value & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
        {
          if ((this.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.None)
          {
            this.UserSession.GlobalIndex.AddToQueue((IDBAttributeType) this);
            flag = true;
          }
        }
        else if ((this.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
          this.UserSession.GlobalIndex.DeleteFromIndex((IDBAttributeType) this);
        if ((value & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.DisableSplitIndexValue)
        {
          if ((value & AttributeOptions.AddToGlobalIndex) == AttributeOptions.None)
            throw new KernelExceptionID(398, (object) AttributeOptionsHelper.GetCaption(AttributeOptions.AddToGlobalIndex));
          if (this.AttributeType != FieldTypes.ftString)
            throw new KernelExceptionID(397);
          if ((this.Options & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.None && !flag)
            this.UserSession.GlobalIndex.AddToQueue((IDBAttributeType) this);
        }
        else if ((this.Options & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.DisableSplitIndexValue && !flag)
          this.UserSession.GlobalIndex.AddToQueue((IDBAttributeType) this);
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int objectTypeID in objectTypesForModify)
          {
            if ((value & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls && (this.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
              this._AttributeType.ValidateNotNull(objectTypeID, -1);
            stringBuilder.Append(objectTypeID.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_OPTIONS = :opt1 WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("opt1", (object) Convert.ToInt32((object) value)), dataManager.Parameter("objType", (object) objectTypeID), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_OPTIONS", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        }
        else
        {
          if ((value & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls && (this.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
            this._AttributeType.ValidateNotNull(objectTypesForModify[0], -1);
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12491(), (object) Convert.ToInt32((object) value), (object) objectTypesForModify[0], (object) this.AttributeID));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_OPTIONS", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        }
        this.paramsTable[36] = (object) Convert.ToInt32((object) value);
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_188"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public override string Mask
  {
    set
    {
      if (!(this.Mask != value))
        return;
      long EventID = this.ValidateEditMode(value != string.Empty ? string.Format(LocalizationHolder.rm.GetString("Kernel_189"), (object) this._AttributeType.Name, (object) value) : string.Format(LocalizationHolder.rm.GetString("Kernel_190"), (object) this._AttributeType.Name));
      this.CheckChangeEnable("F_MASK");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num in objectTypesForModify)
          {
            stringBuilder.Append(num.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_MASK = :val WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("val", (object) value), dataManager.Parameter("objType", (object) num), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_MASK", (object) value, (IUserSession) this.UserSession);
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12492(), (object) objectTypesForModify[0], (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_MASK", (object) value, (IUserSession) this.UserSession);
        }
        this.paramsTable[35] = (object) value;
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_191"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public Attribute4ObjectTypeProperties Attribute4ObjectPropertiesStructure
  {
    get
    {
      return new Attribute4ObjectTypeProperties(this.AttributeID, this._TypeID, this.InheritMode, this.Required, this.ValidationRule, this.Computed, this.Formula, this.UniqueMode, this.LevelID, this.DefaultValue, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID)
      {
        FieldType = this.AttributeType
      };
    }
    set
    {
      if (this.AttributeID != value.AttributeID)
        throw new KernelExceptionID(sc_12465.ssp_appserver_12493(461068343), (object) this.AttributeID);
      if (this._TypeID != value.ObjectType)
        throw new KernelExceptionID(sc_12465.ssp_appserver_12494(419930373), (object) value.ObjectType);
      this.UserSession.StartTransaction();
      try
      {
        this.InheritMode = value.InheritMode;
        this.ValidationRule = value.ValidationRule;
        this.Computed = value.ComputeValueMode;
        this.Formula = value.Formula;
        this.UniqueMode = value.UniqueValueMode;
        this.LevelID = value.LevelID;
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

  protected virtual void SaveRuleFormulaLinks(int childID, string newValue)
  {
    this._AttributeType.SaveFormulaLinks(childID, -1, newValue, Consts.Attribute4ValidationRule, false);
  }

  public override string ValidationRule
  {
    set
    {
      if (!(this.ValidationRule != value))
        return;
      long EventID = this.ValidateEditMode(value != string.Empty ? string.Format(LocalizationHolder.rm.GetString("Kernel_192"), (object) this._AttributeType.Name, (object) value) : string.Format(LocalizationHolder.rm.GetString("Kernel_193"), (object) this._AttributeType.Name));
      this.CheckChangeEnable("F_VALIDATION_RULE");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        string newValue = this._AttributeType.TransposeFormula(value, Consts.Attribute4ValidationRule);
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int childID in objectTypesForModify)
          {
            this.SaveRuleFormulaLinks(childID, newValue);
            stringBuilder.Append(childID.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_VALIDATION_RULE = :val WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("val", (object) newValue), dataManager.Parameter("objType", (object) childID), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_VALIDATION_RULE", (object) newValue, (IUserSession) this.UserSession);
        }
        else
        {
          this.SaveRuleFormulaLinks(objectTypesForModify[0], newValue);
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12495(), (object) objectTypesForModify[0], (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) newValue));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_VALIDATION_RULE", (object) newValue, (IUserSession) this.UserSession);
        }
        this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, this.UserSession.DataManager, "IMS_FORMULA_ATTRS");
        this.paramsTable[83] = (object) newValue;
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_194"), (object) this._AttributeType.Name, (object) ex.Message);
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
      long EventID = this.ValidateEditMode(value != string.Empty ? string.Format(LocalizationHolder.rm.GetString("Kernel_195"), (object) this._AttributeType.Name, (object) value) : string.Format(LocalizationHolder.rm.GetString("Kernel_196"), (object) this._AttributeType.Name));
      this.CheckChangeEnable("F_FORMULA");
      this.UserSession.StartTransaction();
      try
      {
        string str = this._AttributeType.TransposeFormula(value, Consts.Attribute4Formula);
        IDbDataParameter dbDataParameter = this.UserSession.DataManager.Parameter("val", (object) str);
        foreach (int objectTypeID in this.GetObjectTypesForModify())
        {
          this._AttributeType.SaveFormulaLinks(objectTypeID, -1, str, Consts.Attribute4Formula, true);
          this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_FORMULA = :val WHERE F_OBJECT_TYPE = :objTypeID AND F_ATTRIBUTE_ID = :attrID", this.UserSession.DataManager.Parameter("objTypeID", (object) objectTypeID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), dbDataParameter);
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypeID.ToString()}", "IMS_ATTR4OBJ_TYPES", "F_FORMULA", (object) str, (IUserSession) this.UserSession);
          this.paramsTable[73] = (object) str;
          if (this.Computed == ComputeValueModes.StoredValue || this.Computed == ComputeValueModes.IndexValue)
            this._AttributeType.RecomputeValues(objectTypeID, -1);
        }
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_197"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public override ComputeValueModes Computed
  {
    set
    {
      if (this.Computed == value)
        return;
      long EventID = this.ValidateEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_198"), (object) this._AttributeType.Name, (object) ComputeValueModesHelper.GetCaption(value)));
      this.CheckChangeEnable("F_COMPUTED");
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        this._AttributeType.CheckJITValue(value);
        if ((value == ComputeValueModes.JITValue || value == ComputeValueModes.StoredValue || value == ComputeValueModes.IndexValue) && !this._AttributeType.ComputableAttribute)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12465.ssp_appserver_12496()), (object) this._AttributeType.TypeCaption));
        this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12497(), (object) Convert.ToInt32((object) value), (object) this._TypeID, (object) this.AttributeID));
        int[] objectTypesForModify = this.GetObjectTypesForModify();
        if (objectTypesForModify.Length > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int num in objectTypesForModify)
          {
            stringBuilder.Append(num.ToString() + ",");
            dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_COMPUTED = :comp1 WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("comp1", (object) Convert.ToInt32((object) value)), dataManager.Parameter("objType", (object) num), dataManager.Parameter("attrID", (object) this.AttributeID));
          }
          --stringBuilder.Length;
          this.UserSession.DBCache.ChangeTableValue($"F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {this.AttributeID}", "IMS_ATTR4OBJ_TYPES", "F_COMPUTED", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12465.ssp_appserver_12498(), (object) Convert.ToInt32((object) value), (object) objectTypesForModify[0], (object) this.AttributeID));
          this.UserSession.DBCache.ChangeTableValue($"F_ATTRIBUTE_ID = {this.AttributeID.ToString()} AND F_OBJECT_TYPE = {objectTypesForModify[0].ToString()}", "IMS_ATTR4OBJ_TYPES", "F_COMPUTED", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        }
        this.paramsTable[107] = (object) Convert.ToInt32((object) value);
        if (value == ComputeValueModes.StoredValue || value == ComputeValueModes.IndexValue)
        {
          foreach (int objectTypeID in objectTypesForModify)
            this._AttributeType.RecomputeValues(objectTypeID, -1);
        }
        this.UserSession.Commit();
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_200"), (object) this._AttributeType.Name, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        if (!(ex is AccessDeniedException))
          throw new KernelException(str, ex);
        throw;
      }
    }
  }
}
