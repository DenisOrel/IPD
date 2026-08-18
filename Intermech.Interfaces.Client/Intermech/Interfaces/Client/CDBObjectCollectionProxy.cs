// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CDBObjectCollectionProxy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует клиентскую обертку для серверной коллекции объектов IDBObjectCollection.
/// </summary>
internal sealed class CDBObjectCollectionProxy : 
  ClientSessionObjectProxy,
  IDBObjectCollection,
  IDBRecords,
  IDBSessionable,
  IDBAttributableCollection,
  IClientDBObjectCollection
{
  private readonly IDBObjectCollection _serverObject;

  internal CDBObjectCollectionProxy(ClientSession ius, IDBObjectCollection serverObject)
    : base(ius, (MarshalByRefObject) serverObject)
  {
    this._serverObject = serverObject;
  }

  private IDBObjectCollection ServerObject
  {
    [DebuggerStepThrough] get => this._serverObject;
  }

  /// <summary>
  /// Метод проверяет правильность новых значений атрибутов при создании объектов/связей по прототипу от других объектов/связей
  /// </summary>
  /// <param name="ckeckedValues">Словарь ид. версии объекта/связи=набор новых значений атрибутов</param>
  /// <returns>Массив ошибок при проверке (если ошибок нет массив пусто)</returns>
  public CheckAttributeValueResult[] CheckAttributesValues(
    Dictionary<long, AttributeValues[]> ckeckedValues)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.CheckAttributesValues(ckeckedValues);
  }

  public int ObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      this.ClientSession.Guard.ValidateCall();
      return this.ServerObject.ObjectTypeID;
    }
    [DebuggerStepThrough] set
    {
      this.ClientSession.Guard.ValidateCall();
      this.ServerObject.ObjectTypeID = value;
    }
  }

  public IDBObject Create(int objectType)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.Create(objectType);
  }

  public IDBObject Create(Guid versionGuid)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.Create(versionGuid);
  }

  public IDBObject Create()
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.Create();
  }

  public IDBObject Create(IDBObject prototype)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.Create(prototype);
  }

  public DataTable GetAllValues(int attributeID)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.GetAllValues(attributeID);
  }

  public IDBObject Create(long prototypeID)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.Create(prototypeID);
  }

  public long[] CreateEx(int objectType)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.CreateEx(objectType);
  }

  public long[] CreateEx(Guid versionGuid)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.CreateEx(versionGuid);
  }

  public long[] CreateEx()
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.CreateEx();
  }

  public long[] CreateEx(long prototypeID)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.CreateEx(prototypeID);
  }

  /// <summary>
  /// Создает версию указанного объекта, учитывая возможность создания версий парных объектов.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта, для которого нужно создать новую версию</param>
  /// <returns>
  /// Объект, содержащий результат создания версии объекта. ВНИМАНИЕ! Процесс создания версии объекта не является
  /// завершенным. Необходимо подтвердить или отменить создание версии методами Commit/Rollback.
  /// </returns>
  public CreateVersionResult CreateVersionInternal(long objectId)
  {
    this.ClientSession.Guard.ValidateCall();
    IDBTransactions service = ServiceUtils.GetService<IDBTransactions>((object) this.ClientSession, true);
    bool inCreationLogMode = service.InCreationLogMode;
    try
    {
      if (!inCreationLogMode)
        service.StartCreationLog();
      IDBObject version = this.ServerObject.CreateVersion(objectId);
      List<CategoryValue> categoryValueList = new List<CategoryValue>((IEnumerable<CategoryValue>) service.GetCreationLog());
      CreateVersionResult versionInternal = new CreateVersionResult(objectId, version);
      versionInternal.SourceVersions.Add(this.LoadDescription(objectId, ObjectCheckedOutVersionMode.None));
      versionInternal.TargetVersions.Add(this.LoadDescription(version, ObjectCheckedOutVersionMode.NewVersion));
      long num = Math.Abs(version.ObjectID);
      foreach (CategoryValue categoryValue in categoryValueList)
      {
        if (categoryValue.CategoryType == 1 && categoryValue.ActionID == ActionType.Create && Math.Abs(categoryValue.CategoryID) != num)
        {
          IDBObject dbObject = this.ClientSession.GetObject(categoryValue.CategoryID, false);
          if (dbObject != null && dbObject.ParentVersionID != -1L)
          {
            if (dbObject.IsCreationMode)
              dbObject.CommitCreation(true, UISettings.AutoCheckOutNewObjects);
            versionInternal.SourceVersions.Add(this.LoadDescription(this.SwitchToWorkCopy(dbObject.ParentVersionID), ObjectCheckedOutVersionMode.None));
            versionInternal.TargetVersions.Add(this.LoadDescription(dbObject, ObjectCheckedOutVersionMode.NewVersion));
          }
        }
      }
      service.SuspendCreationLog();
      return versionInternal;
    }
    catch
    {
      if (!inCreationLogMode && service.InCreationLogMode)
        service.RollBackCreationLog();
      throw;
    }
    finally
    {
      if (!inCreationLogMode && service.InCreationLogMode)
        service.SuspendCreationLog();
    }
  }

  private long SwitchToWorkCopy(long objectId)
  {
    if (objectId <= 0L)
      return objectId;
    long objectID = -objectId;
    return !this.ClientSession.GetObjectInfo(objectID).Empty ? objectID : objectId;
  }

  private ObjectCheckOutVersionDescription LoadDescription(
    long objectId,
    ObjectCheckedOutVersionMode mode)
  {
    return new ObjectCheckOutVersionDescription(this.ClientSession.GetObject(objectId, true))
    {
      Mode = mode
    };
  }

  private ObjectCheckOutVersionDescription LoadDescription(
    IDBObject dbObject,
    ObjectCheckedOutVersionMode mode)
  {
    return new ObjectCheckOutVersionDescription(dbObject)
    {
      Mode = mode
    };
  }

  /// <summary>
  /// Создает версию объекта с учетом создания совместных версий объектов.
  /// </summary>
  /// <param name="objectID">Идентификатор исходной версии объекта</param>
  /// <returns>Массив идентификаторов версий созданных объектов</returns>
  public long[] CreateVersionEx(long objectID)
  {
    this.ClientSession.Guard.ValidateCall();
    long[] versionEx = this.ServerObject.CreateVersionEx(objectID);
    List<ObjectCheckOutVersionDescription> sourceVersions = new List<ObjectCheckOutVersionDescription>(versionEx.Length);
    List<ObjectCheckOutVersionDescription> resultVersions = new List<ObjectCheckOutVersionDescription>(versionEx.Length);
    for (int index = 0; index < versionEx.Length; ++index)
    {
      IDBObject dbObject = this.ClientSession.GetObject(versionEx[index], false);
      if (dbObject != null && dbObject.ParentVersionID != -1L)
      {
        sourceVersions.Add(this.LoadDescription(this.SwitchToWorkCopy(dbObject.ParentVersionID), ObjectCheckedOutVersionMode.None));
        resultVersions.Add(this.LoadDescription(dbObject, ObjectCheckedOutVersionMode.NewVersion));
      }
    }
    bool flag = false;
    try
    {
      IObjectsCheckOutService service = ServiceUtils.GetService<IObjectsCheckOutService>((object) ServicesManager.ServiceContainer, true);
      ObjectsCheckOutEventArgs checkOutEventArgs = new ObjectsCheckOutEventArgs(sourceVersions, resultVersions);
      ObjectsCheckOutEventArgs e = checkOutEventArgs;
      service.FireObjectsCheckOutEvent((object) this, e);
      if (checkOutEventArgs.Handled && checkOutEventArgs.Rollback)
      {
        QuickObjectInfo objectInfo = this.ClientSession.GetObjectInfo(objectID);
        flag = checkOutEventArgs.Cancel;
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_135"), (object) objectInfo.Caption));
      }
      return versionEx;
    }
    catch
    {
      ServiceUtils.GetService<IDBTransactions>((object) this.ClientSession, true).RollBackCreationLog(versionEx);
      if (flag)
        return (long[]) null;
      throw;
    }
  }

  /// <summary>
  /// Создает версию объекта с учетом создания парных версий объектов.
  /// </summary>
  /// <param name="objectID">Идентификатор исходной версии объекта</param>
  /// <returns>Созданная версия объекта</returns>
  public IDBObject CreateVersion(long objectID)
  {
    this.ClientSession.Guard.ValidateCall();
    CreateVersionResult versionInternal = this.CreateVersionInternal(objectID);
    try
    {
      using (new RemoteLock((object) versionInternal.NewObjectVersion))
      {
        IObjectsCheckOutService service = ServiceUtils.GetService<IObjectsCheckOutService>((object) ServicesManager.ServiceContainer, true);
        ObjectsCheckOutEventArgs checkOutEventArgs = new ObjectsCheckOutEventArgs(versionInternal.SourceVersions, versionInternal.TargetVersions);
        ObjectsCheckOutEventArgs e = checkOutEventArgs;
        service.FireObjectsCheckOutEvent((object) this, e);
        if (checkOutEventArgs.Handled)
        {
          if (checkOutEventArgs.Rollback)
          {
            QuickObjectInfo objectInfo = this.ClientSession.GetObjectInfo(objectID);
            throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_135"), (object) objectInfo.Caption));
          }
        }
      }
      versionInternal.Commit((IUserSession) this.ClientSession);
      return versionInternal.NewObjectVersion;
    }
    catch
    {
      versionInternal.Rollback((IUserSession) this.ClientSession);
      throw;
    }
  }

  public CommandResult AddAttribute(
    long[] objectIDs,
    object attributeID,
    object[] values,
    bool ignoreExceptions)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.AddAttribute(objectIDs, attributeID, values, ignoreExceptions);
  }

  public CommandResult EditAttribute(
    long[] objectIDs,
    object attributeID,
    object[] values,
    bool ignoreExceptions)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.EditAttribute(objectIDs, attributeID, values, ignoreExceptions);
  }

  public CommandResult DeleteAttribute(long[] objectIDs, object attributeID, bool ignoreExceptions)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.DeleteAttribute(objectIDs, attributeID, ignoreExceptions);
  }

  /// <summary>
  /// Отыскать версию объекта, которая существована на указанную дату
  /// на указанном уровне продвижения или на указанном шаге ЖЦ
  /// (значения даты и уровня/шага ЖЦ находятся в правиле подбора версий).
  /// Если версия не найдена, вернётся значение -1
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="rule">Правило подбора версий, в которое включены:
  /// - дата актуального состава,
  /// - уровень продвижения или шаг жизненного цикла</param>
  /// <param name="state">Статус найденной версии</param>
  /// <returns>Найденный идентификатор версии объекта или -1</returns>
  public long ActualDateObjectVersion(
    long objectID,
    VersionsRule rule,
    out ObjectFiltrationState state)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.ActualDateObjectVersion(objectID, rule, out state);
  }

  /// <summary>
  /// Получить статус версии объекта согласно указанному правилу подбора версий
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="rule">Правило подбора версий</param>
  /// <returns>Статус версии объекта согласно указанному правилу подбора версий</returns>
  public ObjectFiltrationState GetObjectVersionFiltrationState(long objectID, VersionsRule rule)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.GetObjectVersionFiltrationState(objectID, rule);
  }

  /// <summary>Показывать версии из всех контекстов редактирования</summary>
  public bool ShowAllModifications
  {
    [DebuggerStepThrough] get
    {
      this.ClientSession.Guard.ValidateCall();
      return this.ServerObject.ShowAllModifications;
    }
    [DebuggerStepThrough] set
    {
      this.ClientSession.Guard.ValidateCall();
      this.ServerObject.ShowAllModifications = value;
    }
  }

  public DataTable SelectWithLocalObjects(DBRecordSetParams paramSet)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.SelectWithLocalObjects(paramSet);
  }

  public void CopyObjectRelations(
    int relTypeID,
    List<int> objTypeIDs,
    IDBObject newObject,
    IDBObject prototype)
  {
    this.ClientSession.Guard.ValidateCall();
    this.ServerObject.CopyObjectRelations(relTypeID, objTypeIDs, newObject, prototype);
  }

  public bool LocalTypesMode
  {
    [DebuggerStepThrough] get
    {
      this.ClientSession.Guard.ValidateCall();
      return this.ServerObject.LocalTypesMode;
    }
    [DebuggerStepThrough] set
    {
      this.ClientSession.Guard.ValidateCall();
      this.ServerObject.LocalTypesMode = value;
    }
  }

  public bool TrashMode
  {
    [DebuggerStepThrough] get
    {
      this.ClientSession.Guard.ValidateCall();
      return this.ServerObject.TrashMode;
    }
    [DebuggerStepThrough] set
    {
      this.ClientSession.Guard.ValidateCall();
      this.ServerObject.TrashMode = value;
    }
  }

  public void SetDisabledPrototypeRelationTypes(List<int> relationTypes)
  {
    this.ServerObject.SetDisabledPrototypeRelationTypes(relationTypes);
  }

  public void SetDisabledPrototypeAttributes(List<int> attributes)
  {
    this.ServerObject.SetDisabledPrototypeAttributes(attributes);
  }

  public DataTable Select(DBRecordSetParams paramSet)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.Select(paramSet);
  }

  public DataTable SelectWithDescriptions(DBRecordSetParams paramSet)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.SelectWithDescriptions(paramSet);
  }

  public int Delete(long[] idList, bool throwException, long deleteMode)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.Delete(idList, throwException, deleteMode);
  }

  public int SetAttributesValues(
    long[] idList,
    AttributeValues[] valuesList,
    bool addIfNotExists,
    bool throwException)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.SetAttributesValues(idList, valuesList, addIfNotExists, throwException);
  }

  public DataTable GetAttributeValues(ICollection<long> idList, int attrID, bool allFields)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.GetAttributeValues(idList, attrID, allFields);
  }

  public DataTable GetAttributeValues(int attrID, bool allFields)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.GetAttributeValues(attrID, allFields);
  }

  public DataTable GetAttributeValues(Guid attrGuid, bool allFields)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.GetAttributeValues(attrGuid, allFields);
  }

  public bool RecordsExists(ConditionStructure[] conditions, HybridDictionary tags)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.RecordsExists(conditions, tags);
  }

  public bool RecordsExists(ConditionStructure[] conditions)
  {
    this.ClientSession.Guard.ValidateCall();
    return this.ServerObject.RecordsExists(conditions);
  }

  public IUserSession Session
  {
    [DebuggerStepThrough] get
    {
      this.ClientSession.Guard.ValidateCall();
      return (IUserSession) this.ClientSession;
    }
  }
}
