// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.СObjectType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Прокси-класс для работы с метаданными - описаниями типов объектов
/// </summary>
internal class СObjectType : 
  CMetadataExtentions,
  IDBObjectType,
  IDBAttributableType,
  IDBSubjectArea,
  IDeletable,
  IDBSecurity
{
  /// <summary>
  /// Коллекция пар значений [(int)Тип объекта] = [(int)Тип родительского объекта]
  /// </summary>
  public static SortedList<int, int> ParentsArrays = new SortedList<int, int>();
  /// <summary>Коллекция атрибутов типа</summary>
  private IDBAttribute4TypeCollection _Attributes;

  /// <summary>Создать прокси-класс для описания типа объекта</summary>
  /// <param name="uSession">Клиентская сессия</param>
  /// <param name="anObjectTypeID">Идентификатор типа объекта</param>
  public СObjectType(ClientSession uSession, int anObjectTypeID)
    : base(uSession, anObjectTypeID)
  {
    this._ObjectTypeID = anObjectTypeID;
    this.InitOptions(4, (long) anObjectTypeID, "IMS_OBJECT_TYPES", LocalizationHolder.rm.GetString("Interfaces.Client_31"));
    if (СObjectType.ParentsArrays.IndexOfKey(this.ObjectType) >= 0)
      return;
    СObjectType.CachedParentTypeID((IClientSession) uSession, this.ObjectType);
  }

  public void FillChildrenList(ArrayList objsTreeList)
  {
    if (objsTreeList == null)
      throw new ArgumentNullException(nameof (objsTreeList));
    this._clientSession.Guard.ValidateCall();
    this.AddChildrenForTypeInternal(this.ObjectType, objsTreeList);
  }

  /// <summary>
  /// Заполнить указанный массив полной иерархией родительских типов указанного типа объекта
  /// </summary>
  /// <param name="objsTreeList">Массив, в котором собирается иерархия родительских типов</param>
  public void _FillParentsArray(ArrayList objsTreeList)
  {
    this._clientSession.Guard.ValidateCall();
    СObjectType.CachedFillParentsArray((IClientSession) this._clientSession, this.ObjectType, objsTreeList);
  }

  /// <summary>
  /// Вытащить из кэш-коллекции или таблицы кэша метаданных идентификатор родительского типа объекта
  /// </summary>
  /// <param name="session">Клиентская сессия</param>
  /// <param name="ObjectTypeID">Идентификатор дочернего типа объекта, для которого отыскивается родительский объект</param>
  /// <returns>-1 или идентификатор родительского типа объекта</returns>
  public static int CachedParentTypeID(IClientSession session, int ObjectTypeID)
  {
    lock (СObjectType.ParentsArrays)
    {
      if (СObjectType.ParentsArrays.ContainsKey(ObjectTypeID))
        return СObjectType.ParentsArrays[ObjectTypeID];
    }
    string filterExpression = $"{"F_OBJECT_TYPE"} = {ObjectTypeID}";
    DataRow[] dataRowArray = session.ClientCache.GetTable("IMS_OBJTYPES_TREE").Select(filterExpression);
    if (dataRowArray == null || dataRowArray.Length == 0)
    {
      lock (СObjectType.ParentsArrays)
        СObjectType.ParentsArrays[ObjectTypeID] = -1;
      return -1;
    }
    lock (СObjectType.ParentsArrays)
    {
      СObjectType.ParentsArrays[ObjectTypeID] = Convert.ToInt32(dataRowArray[0]["F_PARENT_ID"]);
      return СObjectType.ParentsArrays[ObjectTypeID];
    }
  }

  /// <summary>
  /// Заполнить указанный массив полной иерархией родительских типов указанного типа объекта
  /// </summary>
  /// <param name="session">Клиентская сессия</param>
  /// <param name="StartObjectType">Стартовый идентификатор типа объекта, для которого собирается иерархия</param>
  /// <param name="objsTreeList">Массив, в котором собирается иерархия родительских типов</param>
  public static void CachedFillParentsArray(
    IClientSession session,
    int StartObjectType,
    ArrayList objsTreeList)
  {
    objsTreeList.Add((object) StartObjectType);
    for (int ObjectTypeID = СObjectType.CachedParentTypeID(session, StartObjectType); ObjectTypeID > -1; ObjectTypeID = СObjectType.CachedParentTypeID(session, ObjectTypeID))
      objsTreeList.Add((object) ObjectTypeID);
  }

  public override object GetServerObject()
  {
    this._clientSession.Guard.ValidateCall();
    return (object) this._clientSession.Session.GetObjectType(this._id);
  }

  /// <summary>Перечитывает клиентский кэш и paramsTable</summary>
  protected override void ReloadClientCache()
  {
    lock (СObjectType.ParentsArrays)
      СObjectType.ParentsArrays.Clear();
    base.ReloadClientCache();
  }

  /// <summary>
  /// Возвращает количество неудаленных объектов и итераций объектов данного типа
  /// </summary>
  /// <param name="objectsCount">Количество неудалённые объектов</param>
  /// <param name="snapshotsCount">Количество итераций</param>
  public void GetObjectsInfo(out int objectsCount, out int snapshotsCount)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetObjectType(this._id).GetObjectsInfo(out objectsCount, out snapshotsCount);
  }

  public bool IsLocalType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this.Options & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType;
    }
  }

  public int SchemaID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_SCHEMA_ID"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.SchemaID == value)
        return;
      this._clientSession.Session.GetObjectType(this._id).SchemaID = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>
  /// Собрать в массив список всех дочерних типов объектов (рекурсивно)
  /// </summary>
  /// <param name="objTypeID">Идентификатор типов объектов</param>
  /// <param name="objsTreeList">Массив, в котором собирается иерархия типов</param>
  public void AddChildrenForType(int objTypeID, ArrayList objsTreeList)
  {
    if (objsTreeList == null)
      throw new ArgumentNullException(nameof (objsTreeList));
    this._clientSession.Guard.ValidateCall();
    this.AddChildrenForTypeInternal(objTypeID, objsTreeList);
  }

  private void AddChildrenForTypeInternal(int objTypeID, ArrayList objsTreeList)
  {
    objsTreeList.Add((object) objTypeID);
    foreach (DataRow dataRow in this._clientSession.ClientCache.GetTable("IMS_OBJTYPES_TREE").Select("F_PARENT_ID = " + objTypeID.ToString()))
      this.AddChildrenForTypeInternal(Convert.ToInt32(dataRow["F_OBJECT_TYPE"]), objsTreeList);
  }

  /// <summary>Получить список дочерних типов объектов</summary>
  /// <returns>Коллекция дочерних типов объектов</returns>
  public Hashtable GetPossibleChildren()
  {
    this._clientSession.Guard.ValidateCall();
    Hashtable possibleChildren = new Hashtable();
    List<int> intList = new List<int>();
    foreach (IMSApplicability typeApplicability in MetaDataHelper.GetObjectTypeApplicabilities(this.ObjectType))
    {
      if (typeApplicability.ApplicabilityMode != ApplicabilityModes.Disabled)
      {
        int childObjectTypeId = typeApplicability.ChildObjectTypeID;
        int relationTypeId = typeApplicability.RelationTypeID;
        foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(childObjectTypeId))
        {
          IMSApplicability applicability = MetaDataHelper.GetApplicability(this.ObjectType, num, relationTypeId);
          IMSObjectType objectType = MetaDataHelper.GetObjectType(num);
          if (applicability != null && objectType != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled)
          {
            int options = (int) applicability.Options;
            if ((applicability.Options & ApplicabilityOptions.DefaultRelation) == ApplicabilityOptions.DefaultRelation || !possibleChildren.ContainsKey((object) num))
              possibleChildren[(object) num] = (object) relationTypeId;
          }
        }
      }
    }
    return possibleChildren;
  }

  /// <summary>
  /// Получить список дочерних типов объектов, включая абстрактные
  /// </summary>
  /// <returns>Коллекция дочерних типов объектов, включая абстрактные</returns>
  public Hashtable GetAllChildren()
  {
    this._clientSession.Guard.ValidateCall();
    Hashtable allChildren = new Hashtable();
    ArrayList objsTreeList = new ArrayList();
    IDBRelationsApplicabilityCollection applicabilityCollection = this._clientSession.GetRelationsApplicabilityCollection();
    DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(-1, -1, this.ObjectType);
    DataTable table = this._clientSession.ClientCache.GetTable("IMS_OBJECT_TYPES");
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
    {
      if (Convert.ToInt32(row["F_MIN_LINKS"]) != -1)
      {
        objsTreeList.Clear();
        int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
        this.AddChildrenForTypeInternal(Convert.ToInt32(row["F_OBJECT_TYPE"]), objsTreeList);
        foreach (int num in objsTreeList)
        {
          IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(int32, num, this.ObjectType);
          if (applicability != null && applicability.ApplicabilityMode == ApplicabilityModes.Disabled)
          {
            allChildren.Remove((object) num);
            if (dictionary.ContainsKey(num))
              dictionary.Remove(num);
          }
          else
          {
            bool flag = dictionary.ContainsKey(num) && allChildren.ContainsKey((object) num);
            if (table.Rows.Find((object) num) != null && !flag)
            {
              ApplicabilityOptions options = applicability.Options;
              if (allChildren.ContainsKey((object) num))
              {
                if ((options & ApplicabilityOptions.DefaultRelation) == ApplicabilityOptions.DefaultRelation)
                {
                  dictionary[num] = int32;
                  allChildren[(object) num] = (object) int32;
                }
              }
              else
                allChildren[(object) num] = (object) int32;
            }
          }
        }
      }
    }
    return allChildren;
  }

  public void RebuildView()
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetObjectType(this._id).RebuildView();
  }

  public IDBAttribute4TypeCollection VisibleAttributes
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (IDBAttribute4TypeCollection) new CAttribute4ObjectTypeCollection(this._clientSession, this.ObjectType, true);
    }
  }

  public bool HasAttribute(int attributeID)
  {
    this._clientSession.Guard.ValidateCall();
    if (attributeID < 0)
      return ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeID) == AttributeSourceTypes.Object;
    return this.AnyAttributes || this.Attributes.GetAttributeByID(attributeID, false) != null;
  }

  public int DefaultRelation
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_DEFAULT_RELATION"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.DefaultRelation == value)
        return;
      this._clientSession.Session.GetObjectType(this._id).DefaultRelation = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>
  /// Имя таблицы-представления данных для получения списков объектов данного типа
  /// </summary>
  public string ViewName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.Session.GetObjectType(this._id).ViewName;
    }
  }

  /// <summary>
  /// Имя таблицы атрибутов данного типа объектов (работает для всех типов, а не только локальных)
  /// </summary>
  public string AttributesTableName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.Session.GetObjectType(this._id).AttributesTableName;
    }
  }

  public string ObjectTypeShortName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_SHORT_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.ObjectTypeShortName != value))
        return;
      this._clientSession.Session.GetObjectType(this._id).ObjectTypeShortName = value;
      this.ReloadClientCache();
    }
  }

  public bool AnyAttributes
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToBoolean(this.paramsTable[0]["F_ANY_ATTRIBUTES"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.AnyAttributes == value)
        return;
      this._clientSession.Session.GetObjectType(this._id).AnyAttributes = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>Идентификатор родительского типа объекта</summary>
  public int ParentTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      lock (СObjectType.ParentsArrays)
      {
        if (СObjectType.ParentsArrays.IndexOfKey(this.ObjectType) >= 0)
          return СObjectType.ParentsArrays[this.ObjectType];
      }
      DataRow[] dataRowArray = this._clientSession.ClientCache.GetTable("IMS_OBJTYPES_TREE").Select("F_OBJECT_TYPE = " + this.ObjectType.ToString());
      lock (СObjectType.ParentsArrays)
      {
        СObjectType.ParentsArrays[this.ObjectType] = dataRowArray.Length != 0 ? Convert.ToInt32(dataRowArray[0]["F_PARENT_ID"]) : -1;
        return СObjectType.ParentsArrays[this.ObjectType];
      }
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.ParentTypeID == value)
        return;
      this._clientSession.Session.GetObjectType(this._id).ParentTypeID = value;
      this.ReloadClientCache();
    }
  }

  public string ObjectTypeName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_OBJ_TYPE_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.ObjectTypeName != value))
        return;
      this._clientSession.Session.GetObjectType(this._id).ObjectTypeName = value;
      this.ReloadClientCache();
    }
  }

  public InheritModes PublicLC
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (InheritModes) Convert.ToInt32(this.paramsTable[0]["F_PUBLIC_LC"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.PublicLC == value)
        return;
      this._clientSession.Session.GetObjectType(this._id).PublicLC = value;
      this.ReloadClientCache();
    }
  }

  public string ObjectInstanceName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_OBJ_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.ObjectInstanceName != value))
        return;
      this._clientSession.Session.GetObjectType(this._id).ObjectInstanceName = value;
      this.ReloadClientCache();
    }
  }

  public IDBAttribute4TypeCollection Attributes
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      if (this._Attributes == null)
        this._Attributes = (IDBAttribute4TypeCollection) new CAttribute4ObjectTypeCollection(this._clientSession, this.ObjectType, false);
      return this._Attributes;
    }
  }

  public int ObjectType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._id;
    }
  }

  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetObjectType(this._id).Delete(DeleteMode);
    this.ReloadClientCache();
    return num;
  }

  public int LifetimeReserve
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_DEL_TIME"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.LifetimeReserve == value)
        return;
      this._clientSession.Session.GetObjectType(this._id).LifetimeReserve = value;
      this.ReloadClientCache();
    }
  }

  public int CaptionAttribute
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_CAPTION_ATTRIBUTE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.CaptionAttribute == value)
        return;
      this._clientSession.Session.GetObjectType(this._id).CaptionAttribute = value;
      this.ReloadClientCache();
    }
  }

  public int IncludeObjectType(params int[] objectTypes)
  {
    this._clientSession.Guard.ValidateCall();
    return this._clientSession.Session.GetObjectType(this._id).IncludeObjectType(objectTypes);
  }

  public byte[] Icon
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_ICON"] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[0]["F_ICON"];
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      this._clientSession.Session.GetObjectType(this._id).Icon = value;
      this.ReloadClientCache();
    }
  }

  public ObjectTypeProperties PropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new ObjectTypeProperties(this.ObjectType, this.ObjectTypeName, this.ObjectInstanceName, this.Note, this.Versionable, this.DefaultRelation, this.SubjectAreas, this.GUID, this.CaptionAttribute, this.AnyAttributes, this.PublicLC, this.ObjectTypeShortName, this.LifetimeReserve, this.Options, this.SchemaID);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this._clientSession.Session.GetObjectType(this._id).PropertiesStructure.AreaID != value.AreaID)
        this._clientSession.ClientCache.ClearVisibleList(4);
      this._clientSession.Session.GetObjectType(this._id).PropertiesStructure = value;
      this.ReloadClientCache();
    }
  }

  public string Note
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_NOTE"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Note != value))
        return;
      this._clientSession.Session.GetObjectType(this._id).Note = value;
      this.ReloadClientCache();
    }
  }

  public ObjectVersionModes Versionable
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (ObjectVersionModes) Convert.ToInt32(this.paramsTable[0]["F_VERSIONABLE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.Versionable == value)
        return;
      this._clientSession.Session.GetObjectType(this._id).Versionable = value;
      this.ReloadClientCache();
    }
  }

  public ObjectTypeOptions Options
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (ObjectTypeOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.Options == value)
        return;
      this._clientSession.Session.GetObjectType(this._id).Options = value;
      this.ReloadClientCache();
    }
  }

  public override Guid GUID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return base.GUID;
    }
  }

  public string SubjectAreasCaption
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.Session.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
    }
  }

  public string SubjectAreas
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_AREA_ID"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.SubjectAreas != value))
        return;
      (this._clientSession.Session.GetObjectType(this._id) as IDBSubjectArea).SubjectAreas = value;
      this._clientSession.ClientCache.ClearVisibleList(4);
      this.ReloadClientCache();
    }
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_32"), (object) this.ObjectTypeName);
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetObjectType(this._id) as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetObjectType(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetObjectType(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetObjectType(this._id) as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetObjectType(this._id) as IDBSecurity).IsLastDefault;
    }
  }

  public CategoryDescriptor Descriptor
  {
    get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(this._CategoryType, this._CategoryID);
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetObjectType(this._id) as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.ClientCache.ClearVisibleList(4);
    (this._clientSession.Session.GetObjectType(this._id) as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetObjectType(this._id) as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetObjectType(this._id) as IDBSecurity).RestoreAdminAccess();
  }

  /// <summary>
  /// Возвращает описатель типа атрибута номер attributeID применительно к данному типу объектов/связей.
  /// Если тип не может принимать такие атрибуты, то функция возвращает null.
  /// </summary>
  public IDBAttributeType GetAttributeType(int attributeID)
  {
    this._clientSession.Guard.ValidateCall();
    IDBAttributeType attributeType = (IDBAttributeType) this.Attributes.GetAttributeByID(attributeID, false);
    if (attributeType == null && this.AnyAttributes)
      attributeType = this._clientSession.GetAttributeType(attributeID, false);
    return attributeType;
  }

  /// <summary>
  /// Возвращает описатель типа атрибута с именем attributeName применительно к данному типу объектов/связей.
  /// Если тип не может принимать такие атрибуты, то функция возвращает null.
  /// </summary>
  public IDBAttributeType GetAttributeType(string attributeName)
  {
    this._clientSession.Guard.ValidateCall();
    IDBAttributeType attributeType = this._clientSession.GetAttributeType(attributeName, false);
    return attributeType == null ? (IDBAttributeType) null : this.GetAttributeType(attributeType.AttributeID);
  }
}
