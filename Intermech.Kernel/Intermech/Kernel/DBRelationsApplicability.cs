// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRelationsApplicability
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

public class DBRelationsApplicability : DBSessionable, IDBRelationsApplicability, IDeletable
{
  private int _ApplicabilityID;
  private string _ApplicapilityName;
  private long _EventID2;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(3);

  static DBRelationsApplicability()
  {
    DBRelationsApplicability.metadataActions.Add(ActionType.GetAccess, false);
    DBRelationsApplicability.metadataActions.Add(ActionType.SetAccess, false);
    DBRelationsApplicability.metadataActions.Add(ActionType.EditLink, false);
  }

  public DBRelationsApplicability(UserSession uSession, int applicabilityID)
    : base(uSession)
  {
    this._ApplicabilityID = applicabilityID;
    DataRow row = uSession.DBCache.GetTable("IMS_TYPES_APPLICABILITY").Rows.Find((object) applicabilityID);
    if (row == null)
      throw new KernelExceptionID(sc_13578.ssp_appserver_13579(69280410), (object) applicabilityID);
    this.paramsTable.Create(row);
    this.InitSecurityOptions(4, Convert.ToInt64(row["F_OBJECT_TYPE"]));
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBRelationsApplicability.metadataActions);
  }

  private void InvalidateInCache()
  {
    MyCompositeKey key1 = new MyCompositeKey(new object[3]
    {
      (object) this.RelationType,
      (object) this.ObjectType,
      (object) this.InObjectType
    });
    MyCompositeKey key2 = new MyCompositeKey(new object[3]
    {
      (object) -1,
      (object) this.ObjectType,
      (object) -1
    });
    MyCompositeKey key3 = new MyCompositeKey(new object[3]
    {
      (object) -1,
      (object) -1,
      (object) this.InObjectType
    });
    MyCompositeKey key4 = new MyCompositeKey(new object[3]
    {
      (object) this.RelationType,
      (object) -1,
      (object) this.InObjectType
    });
    MyCompositeKey key5 = new MyCompositeKey(new object[3]
    {
      (object) this.RelationType,
      (object) this.ObjectType,
      (object) -1
    });
    DataTable dataTable;
    DBRelationsApplicabilityCollection._ApplCache.TryRemove(key1, out dataTable);
    DBRelationsApplicabilityCollection._ApplCache.TryRemove(key2, out dataTable);
    DBRelationsApplicabilityCollection._ApplCache.TryRemove(key3, out dataTable);
    DBRelationsApplicabilityCollection._ApplCache.TryRemove(key4, out dataTable);
    DBRelationsApplicabilityCollection._ApplCache.TryRemove(key5, out dataTable);
  }

  private long CheckEditMode(string note)
  {
    this.CheckAccess(ActionType.EditLink);
    long num = this.AddEvent(0L, ActionType.EditLink, EventlogRecordType.AccessGranted, note);
    this._EventID2 = this.EventHelper.AddEvent(0L, 0L, 4, (long) this.InObjectType, this.ObjectName, note, ActionType.EditLink, EventlogRecordType.AccessGranted, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
    return num;
  }

  protected bool CheckChangeEnable(string propertyID, bool throwException)
  {
    if (this.UserSession.CanChangeObjectElement(19, (object) this._ApplicabilityID, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      return true;
    if (throwException)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_927"), (object) DataSetProcessor.GetCaption(propertyID)));
    return false;
  }

  protected bool CheckChangeEnable(string propertyID) => this.CheckChangeEnable(propertyID, true);

  protected bool CheckChangeEnableOptions(ApplicabilityOptions value, bool throwException)
  {
    foreach (ApplicabilityOptions optionsFlag in (ApplicabilityOptions[]) Enum.GetValues(typeof (ApplicabilityOptions)))
    {
      if ((value & optionsFlag) != (this.Options & optionsFlag) && !this.UserSession.CanChangeObjectElement(19, (object) this._ApplicabilityID, ObligatoryElementKeys.GetKeyForObjectOptionsFlag((int) optionsFlag)))
      {
        if (throwException)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_927"), (object) EnumDescConverter.GetEnumDescription((Enum) optionsFlag)));
        return false;
      }
    }
    return true;
  }

  public static void ClearAppCache() => DBRelationsApplicabilityCollection._ApplCache.Clear();

  public override string ObjectName
  {
    get
    {
      if (this._ApplicapilityName == null)
      {
        IDBObjectType objectType1 = this.UserSession.GetObjectType(this.ObjectType);
        IDBObjectType objectType2 = this.UserSession.GetObjectType(this.InObjectType);
        IDBRelationType relationType = this.UserSession.GetRelationType(this.RelationType);
        this._ApplicapilityName = string.Format(LocalizationHolder.rm.GetString("Kernel_505"), (object) objectType1.ObjectTypeName, (object) objectType2.ObjectTypeName, (object) relationType.Description);
      }
      return this._ApplicapilityName;
    }
  }

  public int ApplicabilityID => this._ApplicabilityID;

  public int ObjectType => Convert.ToInt32(this.paramsTable[86]);

  public int InObjectType => Convert.ToInt32(this.paramsTable[170]);

  public int RelationType => Convert.ToInt32(this.paramsTable[145]);

  public bool CloneChildRelations
  {
    get => Convert.ToBoolean(this.paramsTable[167]);
    set
    {
      if (this.CloneChildRelations == value)
        return;
      this.InvalidateInCache();
      string str1 = LocalizationHolder.rm.GetString("Kernel_506");
      if (!value)
        str1 = LocalizationHolder.rm.GetString("Kernel_507");
      string note = string.Format(LocalizationHolder.rm.GetString("Kernel_508"), (object) str1);
      long EventID = this.CheckEditMode(note);
      this.CheckChangeEnable("F_CLONE_RELATIONS");
      try
      {
        int newValue = 1;
        if (!value)
          newValue = 0;
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13578.ssp_appserver_13580()}{newValue.ToString()} WHERE F_APPLICABILITY_ID = {this.ApplicabilityID.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13578.ssp_appserver_13581() + this.ApplicabilityID.ToString(), "IMS_TYPES_APPLICABILITY", "F_CLONE_RELATIONS", (object) newValue, (IUserSession) this.UserSession);
        this.paramsTable[167] = (object) newValue;
      }
      catch (Exception ex)
      {
        string str2 = string.Format(LocalizationHolder.rm.GetString(sc_13578.ssp_appserver_13582()), (object) note, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str2);
        this.CloseEvent(this._EventID2, EventlogRecordType.Error, str2);
        throw new KernelException(str2, ex);
      }
    }
  }

  public bool CheckoutFiles
  {
    get => Convert.ToBoolean(this.paramsTable[142]);
    set
    {
      if (this.CheckoutFiles == value)
        return;
      this.InvalidateInCache();
      string str1 = LocalizationHolder.rm.GetString("Kernel_510");
      if (!value)
        str1 = LocalizationHolder.rm.GetString("Kernel_511");
      string note = string.Format(LocalizationHolder.rm.GetString("Kernel_512"), (object) str1);
      long EventID = this.CheckEditMode(note);
      this.CheckChangeEnable("F_CHKOUTFILE");
      try
      {
        int newValue = 1;
        if (!value)
          newValue = 0;
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13578.ssp_appserver_13583()}{newValue.ToString()} WHERE F_APPLICABILITY_ID = {this.ApplicabilityID.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13578.ssp_appserver_13584() + this.ApplicabilityID.ToString(), "IMS_TYPES_APPLICABILITY", "F_CHKOUTFILE", (object) newValue, (IUserSession) this.UserSession);
        this.paramsTable[142] = (object) newValue;
      }
      catch (Exception ex)
      {
        string str2 = string.Format(LocalizationHolder.rm.GetString(sc_13578.ssp_appserver_13585()), (object) note, (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str2);
        this.CloseEvent(this._EventID2, EventlogRecordType.Error, str2);
        throw new KernelException(str2, ex);
      }
    }
  }

  internal string GetInObjectTypes()
  {
    DBObjectType objectType = this.UserSession.GetObjectType(this.InObjectType) as DBObjectType;
    ArrayList arrayList = new ArrayList();
    ArrayList objsTreeList = arrayList;
    objectType.FillChildrenList(objsTreeList);
    if (arrayList.Count == 1)
      return arrayList[0].ToString();
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      stringBuilder.Append(arrayList[0].ToString());
      for (int index = 1; index < arrayList.Count; ++index)
      {
        IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(this.RelationType, this.ObjectType, Convert.ToInt32(arrayList[index]));
        if (applicability != null && applicability.ApplicabilityID == this.ApplicabilityID)
          stringBuilder.Append("," + arrayList[index].ToString());
      }
      return stringBuilder.ToString();
    }
  }

  public int MaximumLinks
  {
    get => Convert.ToInt32(this.paramsTable[169]);
    set
    {
      if (this.MaximumLinks == value)
        return;
      this.InvalidateInCache();
      long EventID = this.CheckEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_514"), (object) value));
      this.CheckChangeEnable("F_MAX_LINKS");
      try
      {
        if (value < 1)
          throw new KernelExceptionID(sc_13578.ssp_appserver_13586(722507640));
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13578.ssp_appserver_13587()}{value.ToString()} WHERE F_APPLICABILITY_ID = {this.ApplicabilityID.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13578.ssp_appserver_13588() + this.ApplicabilityID.ToString(), "IMS_TYPES_APPLICABILITY", "F_MAX_LINKS", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[169] = (object) value;
      }
      catch (Exception ex)
      {
        string str = string.Format(LocalizationHolder.rm.GetString(sc_13578.ssp_appserver_13589()), (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        this.CloseEvent(this._EventID2, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public RelationConstraintModes RelationConstraintMode
  {
    get => (RelationConstraintModes) Convert.ToInt32(this.paramsTable[166]);
    set
    {
      if (this.RelationConstraintMode == value)
        return;
      this.InvalidateInCache();
      long EventID = this.CheckEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_516"), (object) RelationConstraintModesHelper.GetCaption(value)));
      this.CheckChangeEnable("F_CONSTRAINT_MODE");
      try
      {
        this.UserSession.GetRelationType(this.RelationType);
        IDbManager dataManager = this.UserSession.DataManager;
        string str1 = sc_13578.ssp_appserver_13590();
        int num = Convert.ToInt32((object) value);
        string str2 = num.ToString();
        num = this.ApplicabilityID;
        string str3 = num.ToString();
        string commandText = $"{str1}{str2} WHERE F_APPLICABILITY_ID = {str3}";
        dataManager.ExecuteNonQuery(commandText);
        ICacheDataset dbCache = this.UserSession.DBCache;
        string str4 = sc_13578.ssp_appserver_13591();
        num = this.ApplicabilityID;
        string str5 = num.ToString();
        string filterStr = str4 + str5;
        __Boxed<int> int32 = (System.ValueType) Convert.ToInt32((object) value);
        UserSession userSession = this.UserSession;
        dbCache.ChangeTableValue(filterStr, "IMS_TYPES_APPLICABILITY", "F_CONSTRAINT_MODE", (object) int32, (IUserSession) userSession);
        this.paramsTable[166] = (object) Convert.ToInt32((object) value);
      }
      catch (Exception ex)
      {
        string str = string.Format(LocalizationHolder.rm.GetString(sc_13578.ssp_appserver_13592()), (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        this.CloseEvent(this._EventID2, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public ApplicabilityModes ApplicabilityMode
  {
    get => (ApplicabilityModes) Convert.ToInt32(this.paramsTable[168]);
    set
    {
      if (this.ApplicabilityMode == value)
        return;
      this.InvalidateInCache();
      long EventID = this.CheckEditMode(string.Format(LocalizationHolder.rm.GetString("Kernel_518"), (object) ApplicabilityModesHelper.GetCaption(value)));
      this.CheckChangeEnable("F_MIN_LINKS");
      try
      {
        if (value == ApplicabilityModes.Required && this.UserSession.GetObjectType(this.InObjectType).Versionable == ObjectVersionModes.Abstract)
          throw new KernelExceptionID(sc_13578.ssp_appserver_13593(1262017791));
        IDbManager dataManager = this.UserSession.DataManager;
        string str1 = sc_13578.ssp_appserver_13594();
        string str2 = Convert.ToInt32((object) value).ToString();
        int applicabilityId = this.ApplicabilityID;
        string str3 = applicabilityId.ToString();
        string commandText = $"{str1}{str2} WHERE F_APPLICABILITY_ID = {str3}";
        dataManager.ExecuteNonQuery(commandText);
        ICacheDataset dbCache = this.UserSession.DBCache;
        string str4 = sc_13578.ssp_appserver_13595();
        applicabilityId = this.ApplicabilityID;
        string str5 = applicabilityId.ToString();
        string filterStr = str4 + str5;
        __Boxed<int> int32 = (System.ValueType) Convert.ToInt32((object) value);
        UserSession userSession = this.UserSession;
        dbCache.ChangeTableValue(filterStr, "IMS_TYPES_APPLICABILITY", "F_MIN_LINKS", (object) int32, (IUserSession) userSession);
        this.paramsTable[168] = (object) Convert.ToInt32((object) value);
      }
      catch (Exception ex)
      {
        string str = string.Format(LocalizationHolder.rm.GetString(sc_13578.ssp_appserver_13596()), (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        this.CloseEvent(this._EventID2, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public bool IsContent
  {
    get => Convert.ToInt32(this.paramsTable[39]) == 1;
    set
    {
      if (this.IsContent == value)
        return;
      this.InvalidateInCache();
      long EventID = this.CheckEditMode(LocalizationHolder.rm.GetString("Kernel_520") + Consts.ConvertBoolToString(value));
      this.CheckChangeEnable("F_CONTENT");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13578.ssp_appserver_13597()}{(value ? 1 : 0).ToString()} WHERE F_APPLICABILITY_ID = {this.ApplicabilityID.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13578.ssp_appserver_13598() + this.ApplicabilityID.ToString(), "IMS_TYPES_APPLICABILITY", "F_CONTENT", (object) (value ? 1 : 0), (IUserSession) this.UserSession);
        this.paramsTable[39] = (object) (value ? 1 : 0);
      }
      catch (Exception ex)
      {
        string str = string.Format(LocalizationHolder.rm.GetString(sc_13578.ssp_appserver_13599()), (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        this.CloseEvent(this._EventID2, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public ApplicabilityOptions Options
  {
    get => (ApplicabilityOptions) Convert.ToInt32(this.paramsTable[36]);
    set
    {
      if (this.Options == value)
        return;
      bool flag = (value & ApplicabilityOptions.ChangeLCStep) != (this.Options & ApplicabilityOptions.ChangeLCStep);
      if (!flag)
        flag = (value & ApplicabilityOptions.SyncCheckin) != (this.Options & ApplicabilityOptions.SyncCheckin);
      this.InvalidateInCache();
      long EventID = this.CheckEditMode(LocalizationHolder.rm.GetString("Kernel_522") + Consts.ConvertBoolToString((object) value));
      this.CheckChangeEnableOptions(value, true);
      if ((value & ApplicabilityOptions.SoftInstantiation) == ApplicabilityOptions.SoftInstantiation && (this.Options & ApplicabilityOptions.SoftInstantiation) == ApplicabilityOptions.None)
      {
        IDBRelationType relationType = this.UserSession.GetRelationType(this.RelationType);
        IDBAttributeType attributeType = relationType.GetAttributeType(this.UserSession.IdentHelper.CompositionVersionID);
        IDBObjectType objectType = this.UserSession.GetObjectType(this.ObjectType);
        if (objectType.Versionable == ObjectVersionModes.SingleVersion)
          throw new KernelExceptionID(sc_13578.ssp_appserver_13600(1977396067), (object) relationType.Description, (object) objectType.ObjectTypeName);
        if (attributeType == null)
          throw new KernelExceptionID(sc_13578.ssp_appserver_13601(1005687641), (object) relationType.Description, (object) this.UserSession.GetAttributeType(this.UserSession.IdentHelper.CompositionVersionID).Name);
        if ((attributeType as IDBAttributeType4Relation).Required == RequiredModes.AutoRequired)
          throw new KernelExceptionID(sc_13578.ssp_appserver_13602(168161643), (object) relationType.Description);
        if (relationType.GetAttributeType(this.UserSession.IdentHelper.GetAttributeID("cadd955d-306c-11d8-b4e9-00304f19f545")) == null)
          throw new KernelExceptionID(sc_13578.ssp_appserver_13603(875162467), (object) relationType.Description, (object) this.UserSession.GetAttributeType(new Guid("cadd955d-306c-11d8-b4e9-00304f19f545")).Name);
        if ((objectType as IDBGuid).GUID.Equals(new Guid("cad00133-306c-11d8-b4e9-00304f19f545")))
          throw new KernelException(string.Format(sc_13578.ssp_appserver_13604(), (object) objectType.ObjectTypeName));
      }
      if (!ServerConsts.EnableSyncCheckin && (value & ApplicabilityOptions.SyncCheckin) == ApplicabilityOptions.SyncCheckin && (this.Options & ApplicabilityOptions.SyncCheckin) == ApplicabilityOptions.None)
      {
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.ObjectType);
        childrenIdRecursive.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.InObjectType));
        foreach (int num in childrenIdRecursive)
        {
          if (MetaDataHelper.GetAttribute4ObjectType(num, this.UserSession.IdentHelper.FileAttributeID) != null)
            throw new KernelExceptionID(461, (object) MetaDataHelper.GetObjectTypeName(num));
        }
      }
      try
      {
        IDbManager dataManager = this.UserSession.DataManager;
        string str1 = sc_13578.ssp_appserver_13605();
        string str2 = Convert.ToInt32((object) value).ToString();
        int applicabilityId = this.ApplicabilityID;
        string str3 = applicabilityId.ToString();
        string commandText = $"{str1}{str2} WHERE F_APPLICABILITY_ID = {str3}";
        dataManager.ExecuteNonQuery(commandText);
        ICacheDataset dbCache = this.UserSession.DBCache;
        string str4 = sc_13578.ssp_appserver_13606();
        applicabilityId = this.ApplicabilityID;
        string str5 = applicabilityId.ToString();
        string filterStr = str4 + str5;
        __Boxed<int> int32 = (System.ValueType) Convert.ToInt32((object) value);
        UserSession userSession = this.UserSession;
        dbCache.ChangeTableValue(filterStr, "IMS_TYPES_APPLICABILITY", "F_OPTIONS", (object) int32, (IUserSession) userSession);
        this.paramsTable[36] = (object) Convert.ToInt32((object) value);
        if (!flag)
          return;
        (this.UserSession.DBCache as CacheDataset).FillSyncParentObjectTypes(this.UserSession.DataManager);
      }
      catch (Exception ex)
      {
        string str = string.Format(LocalizationHolder.rm.GetString(sc_13578.ssp_appserver_13607()), (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        this.CloseEvent(this._EventID2, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public int RelationsCount
  {
    get
    {
      return Convert.ToInt32(this.UserSession.DataManager.ExecuteScalar($"SELECT COUNT(R.F_PRJLINK_ID) FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE R.F_RELATION_TYPE = {this.RelationType} AND O.F_OBJECT_ID = R.F_PROJ_ID AND O.F_OBJECT_TYPE = {this.InObjectType} AND EXISTS(SELECT F_OBJECT_ID FROM IMS_OBJECTS O2 WHERE O2.F_ID = R.F_PART_ID AND O2.F_OBJECT_TYPE = {this.ObjectType})"));
    }
  }

  public int Delete()
  {
    this.InvalidateInCache();
    long EventID = this.CheckEditMode(LocalizationHolder.rm.GetString("Kernel_524"));
    IDbManager dataManager = this.UserSession.DataManager;
    (this.EventHelper as EventLogHelper).OnBeforeDeleteApplicability((IUserSession) this.UserSession, this.PropertiesStructure);
    bool flag = true;
    int objectTypeParentId1 = MetaDataHelper.GetObjectTypeParentID(this.InObjectType);
    if (objectTypeParentId1 != -1)
    {
      IDBRelationsApplicability applicability = this.UserSession.GetRelationsApplicabilityCollection().GetApplicability(this.RelationType, this.ObjectType, objectTypeParentId1);
      if (applicability != null && (applicability.ApplicabilityMode == this.ApplicabilityMode || this.CheckChangeEnable("F_MIN_LINKS", false)) && (applicability.CheckoutFiles == this.CheckoutFiles || this.CheckChangeEnable("F_CHKOUTFILE", false)) && (applicability.CloneChildRelations == this.CloneChildRelations || this.CheckChangeEnable("F_CLONE_RELATIONS", false)) && (applicability.IsContent == this.IsContent || this.CheckChangeEnable("F_CONTENT", false)) && (applicability.MaximumLinks == this.MaximumLinks || this.CheckChangeEnable("F_MAX_LINKS", false)) && (applicability.RelationConstraintMode == this.RelationConstraintMode || this.CheckChangeEnable("F_CONSTRAINT_MODE", false)) && (applicability.Options == this.Options || this.CheckChangeEnableOptions(applicability.Options, false)))
        flag = false;
    }
    if (flag)
    {
      int objectTypeParentId2 = MetaDataHelper.GetObjectTypeParentID(this.ObjectType);
      if (objectTypeParentId2 != -1)
      {
        IDBRelationsApplicability applicability = this.UserSession.GetRelationsApplicabilityCollection().GetApplicability(this.RelationType, objectTypeParentId2, this.InObjectType);
        if (applicability != null && (applicability.ApplicabilityMode == this.ApplicabilityMode || this.CheckChangeEnable("F_MIN_LINKS", false)) && (applicability.CheckoutFiles == this.CheckoutFiles || this.CheckChangeEnable("F_CHKOUTFILE", false)) && (applicability.CloneChildRelations == this.CloneChildRelations || this.CheckChangeEnable("F_CLONE_RELATIONS", false)) && (applicability.IsContent == this.IsContent || this.CheckChangeEnable("F_CONTENT", false)) && (applicability.MaximumLinks == this.MaximumLinks || this.CheckChangeEnable("F_MAX_LINKS", false)) && (applicability.RelationConstraintMode == this.RelationConstraintMode || this.CheckChangeEnable("F_CONSTRAINT_MODE", false)) && (applicability.Options == this.Options || this.CheckChangeEnableOptions(applicability.Options, false)))
          flag = false;
      }
    }
    if (!this.UserSession.CanChangeObject(19, (object) this._ApplicabilityID) && flag)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_928"), (object) this._ApplicapilityName));
    this.UserSession.StartTransaction();
    try
    {
      if (flag)
      {
        DataTable dataTable = dataManager.ExecuteDataTable(string.Format(sc_13578.ssp_appserver_13608(), (object) this.RelationType, (object) this.InObjectType, (object) this.ObjectType));
        if (dataTable.Rows.Count > 0)
        {
          if (this.IsContent)
            throw new KernelExceptionID(sc_13578.ssp_appserver_13609(763836569));
          for (int index = 0; index < dataTable.Rows.Count; ++index)
            this.UserSession.GetRelation(Convert.ToInt64(dataTable.Rows[index][0]), false)?.Delete((long) Consts.PurgeMode);
        }
      }
      dataManager.ExecuteNonQuery(sc_13578.ssp_appserver_13610() + this.ApplicabilityID.ToString());
      this.UserSession.DBCache.DeleteRecords("IMS_TYPES_APPLICABILITY", sc_13578.ssp_appserver_13611() + this.ApplicabilityID.ToString(), (IUserSession) this.UserSession);
      (this.EventHelper as EventLogHelper).OnAfterDeleteApplicability((IUserSession) this.UserSession, this.PropertiesStructure);
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.CloseEvent(EventID, EventlogRecordType.Error, string.Format(LocalizationHolder.rm.GetString(sc_13578.ssp_appserver_13612()), (object) ex.Message));
      this.CloseEvent(this._EventID2, EventlogRecordType.Error, string.Format(LocalizationHolder.rm.GetString(sc_13578.ssp_appserver_13613()), (object) ex.Message));
      throw;
    }
    return 0;
  }

  public RelationsApplicabilityProperties PropertiesStructure
  {
    get
    {
      return new RelationsApplicabilityProperties(this.ApplicabilityID, this.ObjectType, this.InObjectType, this.RelationType, this.CloneChildRelations, this.MaximumLinks, this.ApplicabilityMode, this.RelationConstraintMode, this.CheckoutFiles, this.IsContent, this.Options);
    }
    set
    {
      this.UserSession.StartTransaction();
      try
      {
        this.InvalidateInCache();
        if (this.ApplicabilityID != value.ApplicabilityID)
          throw new KernelExceptionID(sc_13578.ssp_appserver_13614(1504851475), (object) value.ApplicabilityID);
        this.CloneChildRelations = value.CloneChildRelations;
        this.ApplicabilityMode = value.ApplicabilityMode;
        this.RelationConstraintMode = value.RelationConstraintMode;
        this.MaximumLinks = value.MaximumLinks;
        this.CheckoutFiles = value.CheckoutFiles;
        this.IsContent = value.IsContent;
        this.Options = value.Options;
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  int IDeletable.Delete(long DeleteMode) => this.Delete();
}
