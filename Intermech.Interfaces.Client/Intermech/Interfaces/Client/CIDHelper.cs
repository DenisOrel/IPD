// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CIDHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for IDHelperClient.</summary>
internal class CIDHelper : MarshalByRefObject, IIDHelper
{
  private ClientSession _clientSession;
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
  private int _SPRelationTypeID;
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
  /// <summary>ID атрибута "Идентификатор версии в составе"</summary>
  private int _CompositionVersionID;
  /// <summary>
  /// ID атрибута "Сохранённый идентификатор версии в составе"
  /// </summary>
  private int _CompositionVersionBackup;
  /// <summary>ID атрибута "Настройки"</summary>
  private int _SettingsAttributeID;
  /// <summary>ID атрибута "Номер группы заменителей"</summary>
  private int _SubstitutesGroupNoID;
  /// <summary>RELATION_TYPE Документации на изделие</summary>
  private int _DocRelationTypeID;
  /// <summary>ID атрибута "Номер заменителя в группе"</summary>
  private int _SubstituteInGroup;
  /// <summary>ID атрибута "Название группы заменителей"</summary>
  private int _SubstituteGroupName;
  /// <summary>ID атрибута "Название заменителя"</summary>
  private int _SubstituteName;
  /// <summary>RELATION_TYPE простой связи с сортировкой</summary>
  private int _SortedRelationTypeID;
  /// <summary>ID типа объекта "Правило подбора версий"</summary>
  private int _objtypeVersionRule;
  /// <summary>ID типа объекта "Общее правило подбора версий"</summary>
  private int _objtypeVersionRuleCommon;
  /// <summary>ID типа объекта "Персональное правило подбора версий"</summary>
  private int _objtypeVersionRuleUser;
  /// <summary>ID типа объекта "Системное правило подбора версий"</summary>
  private int _objtypeVersionRuleSystem;
  /// <summary>ID атрибута "Уровень безопасности"</summary>
  private int _attributeSecurityLevelID;
  /// <summary>LEVEL_ID Аннулирование</summary>
  private int _AnnulmentLevelID;
  /// <summary>LEVEL_ID Хранение</summary>
  private int _KeepingLevelID;
  /// <summary>ATTRIBUTE_ID Литера</summary>
  private int _LiteraID;
  /// <summary>ATTRIBUTE_ID Внутренний регистрационный номер</summary>
  private int _InternalRegNumber;
  /// <summary>ATTRIBUTE_ID "Идентификатор версии в составе"</summary>
  private int _attributeVersionInRelation;
  /// <summary>ATTRIBUTE_ID "Идентификатор активной итерации"</summary>
  private int _ActiveSnapshotID;
  /// <summary>ATTRIBUTE_ID "Графические замечания к документам"</summary>
  private int _AttributeRedlining;
  /// <summary>ATTRIBUTE_ID "Необходима публикация на портал"</summary>
  private int _attributePublicationNecessary;
  /// <summary>ATTRIBUTE_ID "Опции публикации"</summary>
  private int _attributeOptionPublication;
  /// <summary>ID типа "Неполный ссылочный объект"</summary>
  private int _objtypeIncompleteObject;
  /// <summary>Атрибут "Условие проверки прав доступа"</summary>
  private int _attributeAccessCondition;
  /// <summary>Атрибут "Изменил карточку объекта"</summary>
  private int _attributeLastEditorID;

  public CIDHelper(ClientSession clientSession)
  {
    this._clientSession = clientSession;
    this._DeletedID = this.ReadFromCache("IMS_LEVELS", "F_LEVEL_ID", "cad0000e-306c-11d8-b4e9-00304f19f545");
    this._SysdbaID = this._clientSession.Session.IdentHelper.SysdbaID;
    this._SystemID = this._clientSession.Session.IdentHelper.SystemID;
    this._OwnerGroupID = this._clientSession.Session.IdentHelper.OwnerGroupID;
    this._AdminRoleID = this._clientSession.Session.IdentHelper.AdminRoleID;
    this._DefaultLanguageID = this._clientSession.Session.IdentHelper.DefaultLanguageID;
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
    this._CompositionVersionID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad001c2-306c-11d8-b4e9-00304f19f545");
    this._CompositionVersionBackup = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cadd955d-306c-11d8-b4e9-00304f19f545");
    this._SettingsAttributeID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad001f1-306c-11d8-b4e9-00304f19f545");
    this._SubstitutesGroupNoID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad001c0-306c-11d8-b4e9-00304f19f545");
    this._SubstituteInGroup = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad001c1-306c-11d8-b4e9-00304f19f545");
    this._SubstituteGroupName = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00817-306c-11d8-b4e9-00304f19f545");
    this._SubstituteName = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00818-306c-11d8-b4e9-00304f19f545");
    this._attributeSecurityLevelID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad00816-306c-11d8-b4e9-00304f19f545");
    this._LiteraID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad0038b-306c-11d8-b4e9-00304f19f545");
    this._InternalRegNumber = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cadd9430-306c-11d8-b4e9-00304f19f545");
    this._attributeVersionInRelation = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad001c2-306c-11d8-b4e9-00304f19f545");
    this._ActiveSnapshotID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cadd94ce-306c-11d8-b4e9-00304f19f545");
    this._AttributeRedlining = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cad0036f-306c-11d8-b4e9-00304f19f545");
    this._attributePublicationNecessary = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", PortalConsts.attributePublicationNecessary.ToString());
    this._attributeOptionPublication = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", PortalConsts.attributePublishOptions.ToString());
    this._attributeAccessCondition = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cadd9a26-306c-11d8-b4e9-00304f19f545");
    this._attributeLastEditorID = this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", "cadd9b77-306c-11d8-b4e9-00304f19f545");
    this._objtypeVersionRule = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad001b3-306c-11d8-b4e9-00304f19f545");
    this._objtypeVersionRuleCommon = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad001b4-306c-11d8-b4e9-00304f19f545");
    this._objtypeVersionRuleUser = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad001b5-306c-11d8-b4e9-00304f19f545");
    this._objtypeVersionRuleSystem = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cad00278-306c-11d8-b4e9-00304f19f545");
    this._objtypeIncompleteObject = this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", "cadd960d-306c-11d8-b4e9-00304f19f545");
  }

  private void SaveErrorToTrace(string aGUID)
  {
    this._clientSession.EventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_25"), (object) aGUID), Consts.traceAlways, "");
    this._clientSession.EventLog.AddToTrace(Environment.StackTrace, Consts.traceAlways, string.Empty);
  }

  /// <summary>
  /// Прочитать из кэша значение целочисленного ключа по GUIDу
  /// </summary>
  private int ReadFromCache(string aTableName, string aKeyField, string aGUID)
  {
    DataRow[] dataRowArray = this._clientSession.ClientCache.GetTable(aTableName).Select("F_GUID = " + DataSetProcessor.QString(aGUID));
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
    return 0;
  }

  /// <summary>
  /// Возвращает ид. атрибута по строковому представлению его глобального идентификатора
  /// </summary>
  public int GetAttributeID(string attributeGuid)
  {
    this._clientSession.Guard.ValidateCall();
    return this.ReadFromCache("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID", attributeGuid);
  }

  /// <summary>
  /// Возвращает ид. типа объектов по строковому представлению его глобального идентификатора
  /// </summary>
  public int GetObjectTypeID(string otGuid)
  {
    this._clientSession.Guard.ValidateCall();
    return this.ReadFromCache("IMS_OBJECT_TYPES", "F_OBJECT_TYPE", otGuid);
  }

  /// <summary>
  /// Возвращает ид. типа связей по строковому представлению его глобального идентификатора
  /// </summary>
  public int GetRelationTypeID(string rtGuid)
  {
    this._clientSession.Guard.ValidateCall();
    return this.ReadFromCache("IMS_RELATION_TYPES", "F_RELATION_TYPE", rtGuid);
  }

  /// <summary>
  /// Возвращает true, если данный тип связи сортируемый вручную (имеет атрибут Сортировка)
  /// </summary>
  public bool IsSortedRelationType(int typeID)
  {
    this._clientSession.Guard.ValidateCall();
    if (this._SortedRelationTypes == null)
    {
      this._SortedRelationTypes = new HybridDictionary();
      lock (this._SortedRelationTypes)
      {
        foreach (DataRow dataRow in this._clientSession.ClientCache.GetTable("IMS_ATTR4RELATION_TYPES").Select("F_ATTRIBUTE_ID = " + this._SortIndexID.ToString()))
          this._SortedRelationTypes[(object) Convert.ToInt32(dataRow["F_RELATION_TYPE"])] = (object) true;
      }
    }
    lock (this._SortedRelationTypes)
      return this._SortedRelationTypes.Contains((object) typeID);
  }

  /// <summary>Получает F_OBJECT_ID для группы ВСЕ ПОЛЬЗОВАТЕЛИ</summary>
  public long AllUsersGroupID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      if (this._AllUsersGroupID == 0L)
        this._AllUsersGroupID = this._clientSession.Session.IdentHelper.AllUsersGroupID;
      return this._AllUsersGroupID;
    }
  }

  /// <summary>
  /// Возвращает идентификатор обязательного атрибута по его глобальному идентификатору.
  /// Если атрибута с таким гуидом не существует, то возвращается ObligatoryObjectAttributes.Zero.
  /// Если атрибут является не обязательным, то возвращает ObligatoryObjectAttributes.None;
  /// </summary>
  public ObligatoryObjectAttributes GetObligatoryAttributeID(Guid guid)
  {
    this._clientSession.Guard.ValidateCall();
    int attributeId = this.GetAttributeID(guid.ToString());
    return attributeId > 0 ? ObligatoryObjectAttributes.None : (ObligatoryObjectAttributes) attributeId;
  }

  /// <summary>ID атрибута "Идентификатор версии в составе"</summary>
  public int CompositionVersionID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._CompositionVersionID;
    }
  }

  /// <summary>
  /// ID атрибута "Сохранённый идентификатор версии в составе"
  /// </summary>
  public int CompositionVersionBackup
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._CompositionVersionBackup;
    }
  }

  /// <summary>ID атрибута "Настройки"</summary>
  public int SettingsAttributeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SettingsAttributeID;
    }
  }

  /// <summary>ID атрибута "Номер группы заменителей"</summary>
  public int SubstitutesGroupNoID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SubstitutesGroupNoID;
    }
  }

  /// <summary>ID атрибута "Номер заменителя в группе"</summary>
  public int SubstituteInGroup
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SubstituteInGroup;
    }
  }

  /// <summary>ID атрибута "Название группы заменителей"</summary>
  public int SubstituteGroupName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SubstituteGroupName;
    }
  }

  /// <summary>ID атрибута "Название заменителя"</summary>
  public int SubstituteName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SubstituteName;
    }
  }

  /// <summary>
  /// ATTRIBUTE_ID атрибута "Дата модификации содержимого объекта"
  /// </summary>
  public int ModifyContentDateID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ModifyContentDateID;
    }
  }

  /// <summary>ATTRIBUTE_ID атрибута "Сортировка"</summary>
  public int SortIndexID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SortIndexID;
    }
  }

  /// <summary>ATTRIBUTE_ID атрибута "Ключ папки классификатора"</summary>
  public int FolderKeyID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._FolderKeyID;
    }
  }

  /// <summary>OBJECT_TYPE типа "Загружаемый модуль"</summary>
  public int PluginTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._PluginTypeID;
    }
  }

  /// <summary>RELATION_TYPE проектной связи</summary>
  public int SPRelationTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SPRelationTypeID;
    }
  }

  /// <summary>OBJECT_TYPE "Рабочий стол"</summary>
  public int WorkspaceTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._WorkspaceTypeID;
    }
  }

  /// <summary>LEVEL_ID "Персональный объект"</summary>
  public int PersonalLevelID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._PersonalLevelID;
    }
  }

  /// <summary>LEVEL_ID "Созданный объект"</summary>
  public int CreatedLevelID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._CreatedLevelID;
    }
  }

  /// <summary>ATTRIBUTE_ID "Файл"</summary>
  public int FileAttributeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._FileAttributeID;
    }
  }

  /// <summary>ATTRIBUTE_ID "Конфигурационные файлы"</summary>
  public int ConfigFileAttributeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ConfigFileAttributeID;
    }
  }

  /// <summary>OBJECT_TYPE типа Конфигурационные данные</summary>
  public int ConfigDataTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ConfigDataTypeID;
    }
  }

  /// <summary>OBJECT_TYPE типа Физическая величина</summary>
  public int PhysicValueTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._PhysicValueTypeID;
    }
  }

  /// <summary>LEVEL_ID удаленных объектов</summary>
  public int DeletedID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._DeletedID;
    }
  }

  /// <summary>OBJECT_ID системного администратора</summary>
  public long SysdbaID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SysdbaID;
    }
  }

  /// <summary>OBJECT_ID пользователя Система</summary>
  public long SystemID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SystemID;
    }
  }

  /// <summary>OBJECT_ID группы "Владелец объекта"</summary>
  public long OwnerGroupID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._OwnerGroupID;
    }
  }

  /// <summary>OBJECT_ID группы "СОЗДАТЕЛЬ_ОБЪЕКТА"</summary>
  public long ObjectCreatorGroupID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      if (this._ObjectCreatorGroupID == 0L)
        this._ObjectCreatorGroupID = this._clientSession.Session.IdentHelper.ObjectCreatorGroupID;
      return this._ObjectCreatorGroupID;
    }
  }

  /// <summary>OBJECT_ID группы "СОЗДАТЕЛЬ_СВЯЗИ"</summary>
  public long RelationCreatorGroupID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      if (this._RelationCreatorGroupID == 0L)
        this._RelationCreatorGroupID = this._clientSession.Session.IdentHelper.RelationCreatorGroupID;
      return this._RelationCreatorGroupID;
    }
  }

  /// <summary>OBJECT_ID роли "Внутренняя служба IPS"</summary>
  public long InternalServiceRoleID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      if (this._InternalServiceRoleID == 0L)
        this._InternalServiceRoleID = this._clientSession.Session.IdentHelper.InternalServiceRoleID;
      return this._InternalServiceRoleID;
    }
  }

  /// <summary>OBJECT_ID роли "Администратор"</summary>
  public long AdminRoleID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._AdminRoleID;
    }
  }

  /// <summary>LANGUAGE_ID, принятый по умолчанию</summary>
  public string DefaultLanguageID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._DefaultLanguageID;
    }
  }

  /// <summary>OBJECT_TYPE объектов типа Пользователи</summary>
  public int UsersTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._UsersTypeID;
    }
  }

  /// <summary>OBJECT_TYPE объектов типа Пользователи</summary>
  public int StorageTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._StorageTypeID;
    }
  }

  /// <summary>OBJECT_TYPE объектов типа Группы</summary>
  public int GroupsTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._GroupsTypeID;
    }
  }

  /// <summary>ATTRIBUTE_ID имени пользователя для входа в систему</summary>
  public int LoginNameID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._LoginNameID;
    }
  }

  /// <summary>ATTRIBUTE_ID пароля</summary>
  public int PasswordID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._PasswordID;
    }
  }

  /// <summary>ATTRIBUTE_ID атрибута "Внешний пользователь"</summary>
  public int ExternalUserID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ExternalUserID;
    }
  }

  /// <summary>ATTRIBUTE_ID имени пользователя для отображения</summary>
  public int UserNameID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._UserNameID;
    }
  }

  /// <summary>OBJECT_TYPE объектов типа Роли</summary>
  public int RolesTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._RolesTypeID;
    }
  }

  /// <summary>OBJECT_TYPE объектов типа Должности</summary>
  public int RanksTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._RanksTypeID;
    }
  }

  /// <summary>OBJECT_TYPE объектов типа Проекты</summary>
  public int ProjectsTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ProjectsTypeID;
    }
  }

  /// <summary>OBJECT_TYPE типа Единица измерения</summary>
  public int MeasureTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._MeasureTypeID;
    }
  }

  /// <summary>ATTRIBUTE_ID наименования</summary>
  public int NameID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._NameID;
    }
  }

  /// <summary>ATTRIBUTE_ID краткого наименования</summary>
  public int ShortNameID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ShortNameID;
    }
  }

  /// <summary>ATTRIBUTE_ID обозначения</summary>
  public int DesignationID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._DesignationID;
    }
  }

  /// <summary>RELATION_TYPE простой вертикальной связи</summary>
  public int SimpleRelationTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SimpleRelationTypeID;
    }
  }

  /// <summary>RELATION_TYPE простой связи с сортировкой</summary>
  public int SortedRelationTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._SortedRelationTypeID;
    }
  }

  /// <summary>ID типа объекта "Правило подбора версий"</summary>
  public int objtypeVersionRule
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._objtypeVersionRule;
    }
  }

  /// <summary>ID типа объекта "Общее правило подбора версий"</summary>
  public int objtypeVersionRuleCommon
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._objtypeVersionRuleCommon;
    }
  }

  /// <summary>ID типа объекта "Персональное правило подбора версий"</summary>
  public int objtypeVersionRuleUser
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._objtypeVersionRuleUser;
    }
  }

  /// <summary>ID типа объекта "Системное правило подбора версий"</summary>
  public int objtypeVersionRuleSystem
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._objtypeVersionRuleSystem;
    }
  }

  /// <summary>RELATION_TYPE документации на изделие</summary>
  public int DocRelationTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._DocRelationTypeID;
    }
  }

  /// <summary>ID атрибута "Уровень безопасности"</summary>
  public int SecurityLevelID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._attributeSecurityLevelID;
    }
  }

  /// <summary>LEVEL_ID "Аннулирование"</summary>
  public int AnnulmentLevelID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._AnnulmentLevelID;
    }
  }

  /// <summary>LEVEL_ID "Хранение"</summary>
  public int KeepingLevelID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._KeepingLevelID;
    }
  }

  /// <summary>ATTRIBUTE_ID "Литера"</summary>
  public int LiteraID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._LiteraID;
    }
  }

  /// <summary>ATTRIBUTE_ID Внутренний регистрационный номер</summary>
  public int InternalRegNumber
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._InternalRegNumber;
    }
  }

  /// <summary>ATTRIBUTE_ID "Идентификатор версии в составе"</summary>
  public int AttributeVersionInRelation
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._attributeVersionInRelation;
    }
  }

  /// <summary>ID атрибута "Идентификатор активной итерации"</summary>
  public int ActiveSnapshotID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ActiveSnapshotID;
    }
  }

  /// <summary>ID атрибута "Графические замечания к документам"</summary>
  public int AttributeRedlining
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._AttributeRedlining;
    }
  }

  /// <summary>ATTRIBUTE_ID "Необходима публикация на портал"</summary>
  public int AttributePublicationNecessary
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._attributePublicationNecessary;
    }
  }

  /// <summary>ATTRIBUTE_ID "Опции публикации"</summary>
  public int AttributeOptionPublication
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._attributeOptionPublication;
    }
  }

  /// <summary>ID типа объекта "Неполный ссылочный объект"</summary>
  public int objtypeIncompleteObject
  {
    [DebuggerStepThrough] get => this._objtypeIncompleteObject;
  }

  /// <summary>Атрибут "Условие проверки прав доступа"</summary>
  public int AttributeAccessCondition
  {
    [DebuggerStepThrough] get => this._attributeAccessCondition;
  }

  /// <summary>Атрибут "Изменил карточку объекта"</summary>
  public int AttributeLastEditorID
  {
    [DebuggerStepThrough] get => this._attributeLastEditorID;
  }
}
