// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.IDHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Kernel;

public class IDHelper : LongLifeObject, IIDHelper
{
  private int _DeletedID;
  private long _SysdbaID;
  private long _SystemID;
  private string _DefaultLanguageID = "";
  private int _UsersTypeID;
  private int _GroupsTypeID;
  private int _StorageTypeID;
  private int _LoginNameID;
  private int _PasswordID;
  private int _ExternalUserID;
  private int _UserNameID;
  private int _RolesTypeID;
  private int _MeasureTypeID;
  private int _NameID;
  private int _ShortNameID;
  private int _DesignationID;
  private int _SimpleRelationTypeID;
  private int _SortedRelationTypeID;
  private int _SPRelationTypeID;
  private int _DocRelationTypeID;
  private long _AllUsersGroupID;
  private int _PhysicValueTypeID;
  private int _ConfigDataTypeID;
  private int _WorkspaceTypeID;
  private int _PersonalLevelID;
  private int _FileAttributeID;
  private int _ConfigFileAttributeID;
  private int _CreatedLevelID;
  private long _OwnerGroupID;
  private long _ObjectCreatorGroupID;
  private long _RelationCreatorGroupID;
  private long _AdminRoleID;
  private long _InternalServiceRoleID;
  private int _PluginTypeID;
  private int _FolderKeyID;
  private int _RanksTypeID;
  private int _ProjectsTypeID;
  private int _SortIndexID;
  private HybridDictionary _SortedRelationTypes;
  private int _ModifyContentDateID;
  private int _CompositionVersionID;
  private int _CompositionVersionBackup;
  private int _SettingsAttributeID;
  private int _SubstitutesGroupNoID;
  private int _SubstituteInGroup;
  private int _SubstituteGroupName;
  private int _SubstituteName;
  private int _objtypeVersionRule;
  private int _objtypeVersionRuleCommon;
  private int _objtypeVersionRuleUser;
  private int _objtypeVersionRuleSystem;
  private int _attributeSecurityLevelID;
  private ICacheDataset _cache;
  private int _AnnulmentLevelID;
  private int _KeepingLevelID;
  private int _LiteraID;
  private int _InternalRegNumber;
  private int _attributeVersionInRelation;
  private int _ActiveSnapshotID;
  private int _AttributeRedlining;
  private int _attributePublicationNecessary;
  private int _attributeOptionPublication;
  private int _objtypeIncompleteObject;
  private int _attributeAccessCondition;
  private int _attributeLastEditorID;

  private void SaveErrorToTrace(string aGUID)
  {
    IEventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    service.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_842"), (object) aGUID), Consts.traceAlways, "");
    service.AddToTrace(Environment.StackTrace, Consts.traceAlways, string.Empty);
  }

  private int ReadFromCache(string aTableName, string aKeyField, string aGUID)
  {
    DataTable table = this._cache.GetTable(aTableName);
    lock (table)
    {
      DataRow[] dataRowArray = table.Select("F_GUID = " + SqlHelper.QString(aGUID));
      if (dataRowArray.Length == 0)
      {
        this.SaveErrorToTrace(aGUID);
      }
      else
      {
        object obj = dataRowArray[0][aKeyField];
        if (obj != DBNull.Value)
          return Convert.ToInt32(obj);
        this.SaveErrorToTrace(aGUID);
      }
    }
    return 0;
  }

  public IDHelper(IDbManager db) => this.LoadData(db);

  public int GetAttributeID(string attributeGuid)
  {
    return this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", attributeGuid);
  }

  public int GetObjectTypeID(string otGuid)
  {
    int objectTypeId = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", otGuid);
    if (objectTypeId == 0 && otGuid.ToString() != "CAD00001-306C-11D8-B4E9-00304F19F545")
      objectTypeId = -1;
    return objectTypeId;
  }

  public int GetRelationTypeID(string rtGuid)
  {
    return this.ReadFromCache("IMS_RELATION_TYPES", "F_RELATION_TYPE", rtGuid);
  }

  public bool IsSortedRelationType(int typeID)
  {
    if (this._SortedRelationTypes == null)
    {
      this._SortedRelationTypes = new HybridDictionary();
      lock (this._SortedRelationTypes)
      {
        foreach (DataRow dataRow in this._cache.GetTable("IMS_ATTR4RELATION_TYPES").Select("F_ATTRIBUTE_ID = " + this._SortIndexID.ToString()))
          this._SortedRelationTypes[(object) Convert.ToInt32(dataRow["F_RELATION_TYPE"])] = (object) true;
      }
    }
    lock (this._SortedRelationTypes)
      return this._SortedRelationTypes.Contains((object) typeID);
  }

  public long AllUsersGroupID
  {
    get
    {
      if (this._AllUsersGroupID == 0L)
      {
        object obj;
        using (IDbManager dbManager = (ServerServices.GetService(typeof (IDbManagerService)) as IDbManagerService).CreateDbManager())
          obj = dbManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_GUID WHERE F_GUID = :guidPar", dbManager.Parameter("guidPar", (object) new Guid("cad00017-306c-11d8-b4e9-00304f19f545")));
        this._AllUsersGroupID = obj != null ? Convert.ToInt64(obj) : 6L;
      }
      return this._AllUsersGroupID;
    }
  }

  public ObligatoryObjectAttributes GetObligatoryAttributeID(Guid guid)
  {
    int attributeId = this.GetAttributeID(guid.ToString());
    return attributeId > 0 ? ObligatoryObjectAttributes.None : (ObligatoryObjectAttributes) attributeId;
  }

  public int AttributeRedlining => this._AttributeRedlining;

  public int ActiveSnapshotID => this._ActiveSnapshotID;

  public int CompositionVersionID => this._CompositionVersionID;

  public int CompositionVersionBackup => this._CompositionVersionBackup;

  public int SettingsAttributeID => this._SettingsAttributeID;

  public int SubstitutesGroupNoID => this._SubstitutesGroupNoID;

  public int SubstituteInGroup => this._SubstituteInGroup;

  public int SubstituteGroupName => this._SubstituteGroupName;

  public int SubstituteName => this._SubstituteName;

  public int ModifyContentDateID => this._ModifyContentDateID;

  public int SortIndexID => this._SortIndexID;

  public int FolderKeyID => this._FolderKeyID;

  public int PluginTypeID => this._PluginTypeID;

  public int SPRelationTypeID => this._SPRelationTypeID;

  public int DocRelationTypeID => this._DocRelationTypeID;

  public int WorkspaceTypeID => this._WorkspaceTypeID;

  public int PersonalLevelID => this._PersonalLevelID;

  public int CreatedLevelID => this._CreatedLevelID;

  public int ConfigFileAttributeID => this._ConfigFileAttributeID;

  public int FileAttributeID => this._FileAttributeID;

  public int ConfigDataTypeID => this._ConfigDataTypeID;

  public int PhysicValueTypeID => this._PhysicValueTypeID;

  public int DeletedID => this._DeletedID;

  public long SysdbaID => this._SysdbaID;

  public long SystemID => this._SystemID;

  public long OwnerGroupID => this._OwnerGroupID;

  public long ObjectCreatorGroupID => this._ObjectCreatorGroupID;

  public long RelationCreatorGroupID => this._RelationCreatorGroupID;

  public long AdminRoleID => this._AdminRoleID;

  public long InternalServiceRoleID => this._InternalServiceRoleID;

  public string DefaultLanguageID => this._DefaultLanguageID;

  public int UsersTypeID => this._UsersTypeID;

  public int StorageTypeID => this._StorageTypeID;

  public int GroupsTypeID => this._GroupsTypeID;

  public int LoginNameID => this._LoginNameID;

  public int PasswordID => this._PasswordID;

  public int ExternalUserID => this._ExternalUserID;

  public int UserNameID => this._UserNameID;

  public int RolesTypeID => this._RolesTypeID;

  public int RanksTypeID => this._RanksTypeID;

  public int ProjectsTypeID => this._ProjectsTypeID;

  public int MeasureTypeID => this._MeasureTypeID;

  public int NameID => this._NameID;

  public int ShortNameID => this._ShortNameID;

  public int DesignationID => this._DesignationID;

  public int SimpleRelationTypeID => this._SimpleRelationTypeID;

  public int SortedRelationTypeID => this._SortedRelationTypeID;

  public int objtypeVersionRule => this._objtypeVersionRule;

  public int objtypeVersionRuleCommon => this._objtypeVersionRuleCommon;

  public int objtypeVersionRuleUser => this._objtypeVersionRuleUser;

  public int objtypeVersionRuleSystem => this._objtypeVersionRuleSystem;

  public int SecurityLevelID => this._attributeSecurityLevelID;

  public int AnnulmentLevelID => this._AnnulmentLevelID;

  public int KeepingLevelID => this._KeepingLevelID;

  public int LiteraID => this._LiteraID;

  public int InternalRegNumber => this._InternalRegNumber;

  public int AttributeVersionInRelation => this._attributeVersionInRelation;

  public int AttributePublicationNecessary => this._attributePublicationNecessary;

  public int AttributeOptionPublication => this._attributeOptionPublication;

  public int objtypeIncompleteObject => this._objtypeIncompleteObject;

  public int AttributeAccessCondition => this._attributeAccessCondition;

  public int AttributeLastEditorID => this._attributeLastEditorID;

  internal void LoadData(IDbManager db)
  {
    this._cache = ServerServices.GetService(typeof (ICacheDataset)) as ICacheDataset;
    this._DeletedID = this.ReadFromCache("IMS_LEVELS", "F_LEVEL_ID", "cad0000e-306c-11d8-b4e9-00304f19f545");
    this._SysdbaID = Convert.ToInt64(db.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_GUID WHERE F_GUID = :p_guid", db.Parameter("p_guid", (object) new Guid("cad00016-306c-11d8-b4e9-00304f19f545"))) ?? throw new SysGUIDNotFoundException("cad00016-306c-11d8-b4e9-00304f19f545"));
    object obj1 = db.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_GUID WHERE F_GUID = :p_guid", db.Parameter("p_guid", (object) new Guid("cad00059-306c-11d8-b4e9-00304f19f545")));
    if (obj1 == null)
      this.SaveErrorToTrace("cad00059-306c-11d8-b4e9-00304f19f545");
    else
      this._OwnerGroupID = Convert.ToInt64(obj1);
    object obj2 = db.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_GUID WHERE F_GUID = :p_guid", db.Parameter("p_guid", (object) new Guid("cadd96b1-306c-11d8-b4e9-00304f19f545")));
    if (obj2 == null)
      this.SaveErrorToTrace("cadd96b1-306c-11d8-b4e9-00304f19f545");
    else
      this._ObjectCreatorGroupID = Convert.ToInt64(obj2);
    object obj3 = db.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_GUID WHERE F_GUID = :p_guid", db.Parameter("p_guid", (object) new Guid("cadd96b3-306c-11d8-b4e9-00304f19f545")));
    if (obj3 == null)
      this.SaveErrorToTrace("cadd96b3-306c-11d8-b4e9-00304f19f545");
    else
      this._RelationCreatorGroupID = Convert.ToInt64(obj3);
    object obj4 = db.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_GUID WHERE F_GUID = :p_guid", db.Parameter("p_guid", (object) new Guid("cad00006-306c-11d8-b4e9-00304f19f545")));
    if (obj4 == null)
      this.SaveErrorToTrace("cad00006-306c-11d8-b4e9-00304f19f545");
    else
      this._AdminRoleID = Convert.ToInt64(obj4);
    object obj5 = db.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_GUID WHERE F_GUID = :p_guid", db.Parameter("p_guid", (object) new Guid("cadd96ad-306c-11d8-b4e9-00304f19f545")));
    if (obj5 == null)
      this.SaveErrorToTrace("cadd96ad-306c-11d8-b4e9-00304f19f545");
    else
      this._InternalServiceRoleID = Convert.ToInt64(obj5);
    object obj6 = db.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_GUID WHERE F_GUID = :p_guid", db.Parameter("p_guid", (object) new Guid("cad0000d-306c-11d8-b4e9-00304f19f545")));
    if (obj6 == null)
      this.SaveErrorToTrace("cad0000d-306c-11d8-b4e9-00304f19f545");
    else
      this._SystemID = Convert.ToInt64(obj6);
    this._DefaultLanguageID = (db.ExecuteScalar(sc_13062.ssp_appserver_13063()) ?? throw new KernelException(sc_13062.ssp_appserver_13064())).ToString();
    this._UsersTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad00002-306c-11d8-b4e9-00304f19f545");
    this._GroupsTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad00003-306c-11d8-b4e9-00304f19f545");
    this._StorageTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad00014-306c-11d8-b4e9-00304f19f545");
    this._LoginNameID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00018-306c-11d8-b4e9-00304f19f545");
    this._PasswordID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00019-306c-11d8-b4e9-00304f19f545");
    this._ExternalUserID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad002df-306c-11d8-b4e9-00304f19f545");
    this._UserNameID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad0001d-306c-11d8-b4e9-00304f19f545");
    this._RolesTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad00007-306c-11d8-b4e9-00304f19f545");
    this._MeasureTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad0000b-306c-11d8-b4e9-00304f19f545");
    this._NameID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00020-306c-11d8-b4e9-00304f19f545");
    this._ShortNameID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00005-306c-11d8-b4e9-00304f19f545");
    this._DesignationID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad0001f-306c-11d8-b4e9-00304f19f545");
    this._SimpleRelationTypeID = this.ReadFromCache("IMS_RELATION_TYPES", "F_RELATION_TYPE", "cad00022-306c-11d8-b4e9-00304f19f545");
    this._SortedRelationTypeID = this.ReadFromCache("IMS_RELATION_TYPES", "F_RELATION_TYPE", "cad00151-306c-11d8-b4e9-00304f19f545");
    this._SPRelationTypeID = this.ReadFromCache("IMS_RELATION_TYPES", "F_RELATION_TYPE", "cad00023-306c-11d8-b4e9-00304f19f545");
    this._DocRelationTypeID = this.ReadFromCache("IMS_RELATION_TYPES", "F_RELATION_TYPE", "cad00154-306c-11d8-b4e9-00304f19f545");
    this._PhysicValueTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad00048-306c-11d8-b4e9-00304f19f545");
    this._ConfigDataTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad00045-306c-11d8-b4e9-00304f19f545");
    this._WorkspaceTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad0004a-306c-11d8-b4e9-00304f19f545");
    this._PersonalLevelID = this.ReadFromCache("IMS_LEVELS", "F_LEVEL_ID", "cad00049-306c-11d8-b4e9-00304f19f545");
    this._AnnulmentLevelID = this.ReadFromCache("IMS_LEVELS", "F_LEVEL_ID", "cad00012-306c-11d8-b4e9-00304f19f545");
    this._KeepingLevelID = this.ReadFromCache("IMS_LEVELS", "F_LEVEL_ID", "cad009de-306c-11d8-b4e9-00304f19f545");
    this._FileAttributeID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad0004b-306c-11d8-b4e9-00304f19f545");
    this._ConfigFileAttributeID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad014d4-306c-11d8-b4e9-00304f19f545");
    this._CreatedLevelID = this.ReadFromCache("IMS_LEVELS", "F_LEVEL_ID", "cad00013-306c-11d8-b4e9-00304f19f545");
    this._PluginTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad0005b-306c-11d8-b4e9-00304f19f545");
    this._RanksTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad00147-306c-11d8-b4e9-00304f19f545");
    this._ProjectsTypeID = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad00812-306c-11d8-b4e9-00304f19f545");
    this._FolderKeyID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad0014d-306c-11d8-b4e9-00304f19f545");
    this._SortIndexID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00202-306c-11d8-b4e9-00304f19f545");
    this._ModifyContentDateID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad0013a-306c-11d8-b4e9-00304f19f545");
    this._LiteraID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad0038b-306c-11d8-b4e9-00304f19f545");
    this._InternalRegNumber = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cadd9430-306c-11d8-b4e9-00304f19f545");
    this._CompositionVersionID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad001c2-306c-11d8-b4e9-00304f19f545");
    this._CompositionVersionBackup = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cadd955d-306c-11d8-b4e9-00304f19f545");
    this._SettingsAttributeID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad001f1-306c-11d8-b4e9-00304f19f545");
    this._SubstitutesGroupNoID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad001c0-306c-11d8-b4e9-00304f19f545");
    this._SubstituteInGroup = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad001c1-306c-11d8-b4e9-00304f19f545");
    this._SubstituteGroupName = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00817-306c-11d8-b4e9-00304f19f545");
    this._SubstituteName = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00818-306c-11d8-b4e9-00304f19f545");
    this._attributeAccessCondition = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cadd9a26-306c-11d8-b4e9-00304f19f545");
    this._attributeLastEditorID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cadd9b77-306c-11d8-b4e9-00304f19f545");
    this._objtypeVersionRule = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad001b3-306c-11d8-b4e9-00304f19f545");
    this._objtypeVersionRuleCommon = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad001b4-306c-11d8-b4e9-00304f19f545");
    this._objtypeVersionRuleUser = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad001b5-306c-11d8-b4e9-00304f19f545");
    this._objtypeVersionRuleSystem = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad00278-306c-11d8-b4e9-00304f19f545");
    this._attributeSecurityLevelID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00816-306c-11d8-b4e9-00304f19f545");
    this._attributeVersionInRelation = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad001c2-306c-11d8-b4e9-00304f19f545");
    this._ActiveSnapshotID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cadd94ce-306c-11d8-b4e9-00304f19f545");
    this._AttributeRedlining = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad0036f-306c-11d8-b4e9-00304f19f545");
    this._attributePublicationNecessary = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", PortalConsts.attributePublicationNecessary.ToString());
    this._attributeOptionPublication = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", PortalConsts.attributePublishOptions.ToString());
    this._objtypeIncompleteObject = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cadd960d-306c-11d8-b4e9-00304f19f545");
  }
}
