// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientMetadataCacheService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.LifeCycles;
using Intermech.Threading;
using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Служба для получения классов с инфой о метаданных
/// Реализация является thread safe.
/// </summary>
internal sealed class ClientMetadataCacheService : IClientMetadataCache
{
  /// <summary>Общий контекст для всех дочерних элементов</summary>
  private readonly MetadataInfoParentContext _serviceContext;
  private AtomicInt32 _FileAttributeID;
  private AtomicInt32 _GroupsTypeID;
  private AtomicInt32 _PluginTypeID;
  private AtomicInt32 _RolesTypeID;
  private AtomicInt32 _UsersTypeID;

  /// <summary>Создает объект</summary>
  /// <param name="clientCache">Сервис клиентского кэша метаданных</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="clientCache" /> содержит null</exception>
  public ClientMetadataCacheService(IClientCache clientCache)
  {
    this._serviceContext = clientCache != null ? new MetadataInfoParentContext(this, clientCache) : throw new ArgumentNullException(nameof (clientCache));
    this._serviceContext.ClientCache.Cleared += new EventHandler(this.OnClientCacheCleared);
    this._serviceContext.ClientCache.Reloaded += new EventHandler<ClientCacheReloadedEventArgs>(this.OnClientCacheReloaded);
    this._FileAttributeID = new AtomicInt32(0);
    this._GroupsTypeID = new AtomicInt32(0);
    this._PluginTypeID = new AtomicInt32(0);
    this._RolesTypeID = new AtomicInt32(0);
    this._UsersTypeID = new AtomicInt32(0);
  }

  private void OnClientCacheCleared(object sender, EventArgs e) => this.ResetCachedValues();

  private void OnClientCacheReloaded(object sender, ClientCacheReloadedEventArgs e)
  {
    this.ResetCachedValues();
  }

  private void ResetCachedValues()
  {
    this._FileAttributeID.Value = 0;
    this._GroupsTypeID.Value = 0;
    this._PluginTypeID.Value = 0;
    this._RolesTypeID.Value = 0;
    this._UsersTypeID.Value = 0;
  }

  public IDBObjectTypeInfo GetObjectType(int objectTypeID)
  {
    return (IDBObjectTypeInfo) new CObjectTypeInfo(this._serviceContext, objectTypeID);
  }

  public IDBAttributeTypeInfo GetAttributeType(int attributeTypeID)
  {
    return (IDBAttributeTypeInfo) new CAttributeTypeInfo(this._serviceContext, attributeTypeID);
  }

  public IDBRelationTypeInfo GetRelationType(int relationTypeID)
  {
    return (IDBRelationTypeInfo) new CRelationTypeInfo(this._serviceContext, relationTypeID);
  }

  public IDBLifecycleLevelInfo GetLifecycleLevel(int levelID)
  {
    return (IDBLifecycleLevelInfo) new CLifecycleLevelInfo(this._serviceContext, levelID);
  }

  public IDBLifecycleLevelInfoCollection GetLifecycleLevelCollection()
  {
    return (IDBLifecycleLevelInfoCollection) new CLifecycleLevelInfoCollection(this._serviceContext, (object) string.Empty, false);
  }

  public IDBLCSchemaInfo GetLCSchema(int schemaID)
  {
    return (IDBLCSchemaInfo) new CLCSchemaInfo(this._serviceContext, schemaID);
  }

  public IDBLCSchemaInfo GetLCSchema(int schemaID, bool throwException)
  {
    if (this._serviceContext.ClientCache.GetTable("IMS_LC_SCHEMAS").Rows.Find((object) schemaID) != null)
      return this.GetLCSchema(schemaID);
    if (throwException)
      throw new KernelExceptionID(247, (object) schemaID);
    return (IDBLCSchemaInfo) null;
  }

  public IDBLCSchemaInfo GetLCSchema(Guid schemaGuid) => this.GetLCSchema(schemaGuid, true);

  public IDBLCSchemaInfo GetLCSchema(Guid schemaGuid, bool throwException)
  {
    DataRow[] dataRowArray = this._serviceContext.ClientCache.GetTable("IMS_LC_SCHEMAS").Select("F_GUID = " + DataSetProcessor.QString(schemaGuid.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetLCSchema(Convert.ToInt32(dataRowArray[0]["F_SCHEMA_ID"]));
    if (throwException)
      throw new KernelExceptionID(248, (object) schemaGuid.ToString());
    return (IDBLCSchemaInfo) null;
  }

  /// <summary>
  /// Возвращает интерфейс с информацией о шаге жизненного цикла
  /// </summary>
  /// <param name="stepID">Ид. шага</param>
  /// <returns></returns>
  public IDBLifecycleStepInfo GetLCStep(int stepID)
  {
    return stepID == 0 || stepID == -1 ? (IDBLifecycleStepInfo) null : (IDBLifecycleStepInfo) new CDBLifecycleStepInfo(this._serviceContext, stepID);
  }

  public IDBObjectTypeInfo GetObjectType(int objectTypeID, bool notFoundException)
  {
    try
    {
      return this.GetObjectType(objectTypeID);
    }
    catch
    {
      if (!notFoundException)
        return (IDBObjectTypeInfo) null;
      throw;
    }
  }

  /// <summary>Возвращает интерфейс с информацией об атрибуте</summary>
  /// <param name="attributeGUID">Глобальный ид. атрибута</param>
  /// <param name="notFoundException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  public IDBAttributeTypeInfo GetAttributeType(Guid attributeGUID, bool notFoundException)
  {
    DataRow[] dataRowArray = this._serviceContext.ClientCache.GetTable("IMS_ATTRIBUTES").Select("F_GUID = " + DataSetProcessor.QString(attributeGUID.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetAttributeType(Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]));
    if (notFoundException)
      throw new KernelExceptionID(85, (object) attributeGUID);
    return (IDBAttributeTypeInfo) null;
  }

  /// <summary>Возвращает интерфейс с информацией об атрибуте</summary>
  /// <param name="attributeName">Наименование атрибута</param>
  /// <param name="notFoundException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  public IDBAttributeTypeInfo GetAttributeType(string attributeName, bool notFoundException)
  {
    DataRow[] dataRowArray = this._serviceContext.ClientCache.GetTable("IMS_ATTRIBUTES").Select("F_NAME = " + DataSetProcessor.QString(attributeName.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetAttributeType(Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]));
    if (notFoundException)
      throw new KernelExceptionID(84, (object) attributeName);
    return (IDBAttributeTypeInfo) null;
  }

  public IDBAttributeTypeInfo GetAttributeType(int attributeTypeID, bool notFoundException)
  {
    try
    {
      return this.GetAttributeType(attributeTypeID);
    }
    catch
    {
      if (!notFoundException)
        return (IDBAttributeTypeInfo) null;
      throw;
    }
  }

  public IDBRelationTypeInfo GetRelationType(int relationTypeID, bool notFoundException)
  {
    try
    {
      return this.GetRelationType(relationTypeID);
    }
    catch
    {
      if (!notFoundException)
        return (IDBRelationTypeInfo) null;
      throw;
    }
  }

  /// <summary>Возвращает интерфейс с информацией о типе связей</summary>
  /// <param name="relationGUID">Глобальный ид. типа связей</param>
  /// <param name="notFoundException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  public IDBRelationTypeInfo GetRelationType(Guid relationGUID, bool notFoundException)
  {
    DataRow[] dataRowArray = this._serviceContext.ClientCache.GetTable("IMS_RELATION_TYPES").Select("F_GUID = " + DataSetProcessor.QString(relationGUID.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetRelationType(Convert.ToInt32(dataRowArray[0]["F_RELATION_TYPE"]));
    if (notFoundException)
      throw new KernelExceptionID(122, (object) relationGUID);
    return (IDBRelationTypeInfo) null;
  }

  public IDBLifecycleLevelInfo GetLifecycleLevel(int levelID, bool notFoundException)
  {
    try
    {
      return this.GetLifecycleLevel(levelID);
    }
    catch
    {
      if (!notFoundException)
        return (IDBLifecycleLevelInfo) null;
      throw;
    }
  }

  public IDBObjectTypeInfo GetObjectType(Guid anObjectTypeGuid, bool throwException)
  {
    DataRow[] dataRowArray = this._serviceContext.ClientCache.GetTable("IMS_OBJECT_TYPES").Select("F_GUID = " + DataSetProcessor.QString(anObjectTypeGuid.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetObjectType(Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]));
    if (throwException)
      throw new KernelExceptionID(99, (object) anObjectTypeGuid);
    return (IDBObjectTypeInfo) null;
  }

  public IDBObjectTypeInfo GetObjectType(string anObjectTypeName, bool throwException)
  {
    DataRow[] dataRowArray = this._serviceContext.ClientCache.GetTable("IMS_OBJECT_TYPES").Select("F_OBJ_TYPE_NAME = " + DataSetProcessor.QString(anObjectTypeName));
    if (dataRowArray.Length != 0 || dataRowArray.Length != 0)
      return this.GetObjectType(Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]));
    if (throwException)
      throw new KernelExceptionID(97, (object) anObjectTypeName);
    return (IDBObjectTypeInfo) null;
  }

  /// <summary>
  /// Возвращает коллекцию типов объектов, входящих в состав типа parentTypeID.
  /// </summary>
  /// <param name="parentTypeID">Идентификатор родительского типа объектов. Если == -1,
  /// то возвращает корневые типы объектво. Если == -2, то возвращает ВСЕ типы объектов</param>
  public IDBObjectTypeInfoCollection GetObjectTypeCollection(int parentTypeID)
  {
    return this.GetObjectTypeCollection(parentTypeID, false);
  }

  /// <summary>
  /// Возвращает коллекцию типов объектов, входящих в состав типа parentTypeID.
  /// </summary>
  /// <param name="parentTypeID">Идентификатор родительского типа объектов. Если == -1,
  /// то возвращает корневые типы объектво. Если == -2, то возвращает ВСЕ типы объектов</param>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  public IDBObjectTypeInfoCollection GetObjectTypeCollection(int parentTypeID, bool filterRecs)
  {
    return (IDBObjectTypeInfoCollection) new CObjectTypeInfoCollection(this._serviceContext, (object) parentTypeID, filterRecs);
  }

  /// <summary>Возвращает полный список типов связей.</summary>
  public IDBRelationTypeInfoCollection GetRelationTypeCollection()
  {
    return this.GetRelationTypeCollection(false);
  }

  /// <summary>Возвращает список типов связей.</summary>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  public IDBRelationTypeInfoCollection GetRelationTypeCollection(bool filterRecs)
  {
    return (IDBRelationTypeInfoCollection) new CRelationTypeInfoCollection(this._serviceContext, (object) 0, filterRecs);
  }

  public IDBAttributesGroupInfoCollection GetAttributesGroupCollection(int groupID, bool filterRecs)
  {
    return (IDBAttributesGroupInfoCollection) new CAttributesGroupInfoCollection(this._serviceContext, (object) groupID, filterRecs);
  }

  public IDBAttributeTypeInfoCollection GetAttributeTypeCollection(int groupID, bool filterRecs)
  {
    return (IDBAttributeTypeInfoCollection) new CAttributeTypeInfoCollection(this._serviceContext, (object) groupID, filterRecs);
  }

  /// <summary>Получить группу атрибутов номер aGroupID</summary>
  public IDBAttributesGroupInfo GetAttributesGroup(int aGroupID)
  {
    return (IDBAttributesGroupInfo) new СAttributesGroupInfo(this._serviceContext, aGroupID);
  }

  public int FileAttributeID
  {
    get
    {
      if (this._FileAttributeID.Value == 0)
        this._FileAttributeID.Value = this.GetAttributeType(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), true).AttributeID;
      return this._FileAttributeID.Value;
    }
  }

  /// <summary>Группы пользователей</summary>
  public int GroupsTypeID
  {
    get
    {
      if (this._GroupsTypeID.Value == 0)
        this._GroupsTypeID.Value = this.GetObjectType(new Guid("cad00003-306c-11d8-b4e9-00304f19f545"), true).ObjectType;
      return this._GroupsTypeID.Value;
    }
  }

  /// <summary>Пользователи</summary>
  public int UsersTypeID
  {
    get
    {
      if (this._UsersTypeID.Value == 0)
        this._UsersTypeID.Value = this.GetObjectType(new Guid("cad00002-306c-11d8-b4e9-00304f19f545"), true).ObjectType;
      return this._UsersTypeID.Value;
    }
  }

  /// <summary>Роли</summary>
  public int RolesTypeID
  {
    get
    {
      if (this._RolesTypeID.Value == 0)
        this._RolesTypeID.Value = this.GetObjectType(new Guid("cad00007-306c-11d8-b4e9-00304f19f545"), true).ObjectType;
      return this._RolesTypeID.Value;
    }
  }

  public int PluginTypeID
  {
    get
    {
      if (this._PluginTypeID.Value == 0)
        this._PluginTypeID.Value = this.GetObjectType(new Guid("cad0005b-306c-11d8-b4e9-00304f19f545"), true).ObjectType;
      return this._PluginTypeID.Value;
    }
  }
}
