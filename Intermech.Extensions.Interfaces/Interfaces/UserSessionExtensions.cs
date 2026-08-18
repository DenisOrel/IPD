// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.UserSessionExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces;

public static class UserSessionExtensions
{
  [CanBeNull]
  public static T SessionGuarantee<T>(
    [CanBeNull] this IUserSession session,
    [NotNull] UserSessionExtensions.NotNullSessionFunc<T> predicate)
  {
    if (session != null)
      return predicate(session);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return predicate(sessionKeeper.Session);
  }

  public static void SessionGuarantee(
    [CanBeNull] this IUserSession session,
    [NotNull] UserSessionExtensions.NotNullSessionAction action)
  {
    if (session != null)
    {
      action(session);
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        action(sessionKeeper.Session);
    }
  }

  [ContractAnnotation("throwExceptionIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetCustomService<T>(
    [NotNull] this IUserSession session,
    bool throwExceptionIfNotFound = true,
    [CanBeNull] string exceptionMessageIfFail = null)
  {
    object customService = session.GetCustomService(typeof (T));
    if (customService == null & throwExceptionIfNotFound)
      throw new InvalidOperationException(exceptionMessageIfFail ?? $"Session must contains custom service \"{typeof (T).Name}\"");
    return customService == null ? default (T) : (T) customService;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetCustomService<T>([NotNull] this IUserSession session, [NotNull] string exceptionMessageIfFail)
  {
    return (T) (session.GetCustomService(typeof (T)) ?? throw new InvalidOperationException(exceptionMessageIfFail));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IUserSession GetCustomService<T>(
    [NotNull] this IUserSession session,
    [NotNull] out T service,
    [CanBeNull] string exceptionMessageIfFail = null)
  {
    service = (T) (session.GetCustomService(typeof (T)) ?? throw new InvalidOperationException(exceptionMessageIfFail ?? $"Session must contains custom service \"{typeof (T).Name}\""));
    return session;
  }

  public static bool TryGetCustomService<T>([NotNull] this IUserSession session, [CanBeNull] out T service)
  {
    object customService = session.GetCustomService(typeof (T));
    service = customService != null ? (T) customService : default (T);
    return customService != null;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T EnsureCustomServiceInitialized<T>(
    [NotNull] this IUserSession session,
    [NotNull] ref T service,
    [CanBeNull] string exceptionMessageIfFail = null)
    where T : class
  {
    if ((object) service == null)
      service = (T) (session.GetCustomService(typeof (T)) ?? throw new InvalidOperationException(exceptionMessageIfFail ?? $"Session must contains custom service \"{typeof (T).Name}\""));
    return service;
  }

  [ContractAnnotation("throwExceptOnError:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDbObjectInterface GetObject<TDbObjectInterface>(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    bool throwExceptOnError = true)
    where TDbObjectInterface : class, IDBObject
  {
    IDBObject dbObject = session.GetObject(objectVersionID, false);
    if (dbObject == null)
    {
      if (throwExceptOnError)
        throw new ObjectVersionNotFoundException(objectVersionID);
      return default (TDbObjectInterface);
    }
    return dbObject is TDbObjectInterface dbObjectInterface ? dbObjectInterface : throw new ObjectVersionNotFoundException(objectVersionID);
  }

  [Obsolete("Используйте GetObject<TDbObjectInterface, TObjectVersionNotFoundException>")]
  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDbObjectInterface GetObject<TDbObjectInterface, TObjectNotFoundException, TInvalidCastException>(
    [NotNull] this IUserSession session,
    long objectID,
    bool throwExceptOnError = true)
    where TDbObjectInterface : IDBObject
    where TObjectNotFoundException : ObjectNotFoundException
    where TInvalidCastException : InvalidCastException
  {
    IDBObject dbObject = session.GetObject(objectID, false);
    if (dbObject == null & throwExceptOnError)
      throw (object) (TObjectNotFoundException) Activator.CreateInstance(typeof (TObjectNotFoundException), (object) objectID);
    if (dbObject == null || dbObject is TDbObjectInterface)
      return (TDbObjectInterface) dbObject;
    TInvalidCastException instance;
    if (!typeof (IObjectException).IsAssignableFrom(typeof (TInvalidCastException)))
      instance = (TInvalidCastException) Activator.CreateInstance(typeof (TInvalidCastException));
    else
      instance = (TInvalidCastException) Activator.CreateInstance(typeof (TInvalidCastException), (object) objectID);
    throw (object) instance;
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDbObjectInterface GetObject<TDbObjectInterface, TObjectVersionNotFoundException>(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    bool throwExceptOnError = true)
    where TDbObjectInterface : class, IDBObject
    where TObjectVersionNotFoundException : ObjectVersionNotFoundException
  {
    IDBObject dbObject = session.GetObject(objectVersionID, false);
    if (dbObject == null)
    {
      if (throwExceptOnError)
        throw (object) (TObjectVersionNotFoundException) Activator.CreateInstance(typeof (TObjectVersionNotFoundException), (object) objectVersionID);
      return default (TDbObjectInterface);
    }
    return dbObject is TDbObjectInterface dbObjectInterface ? dbObjectInterface : throw (object) (TObjectVersionNotFoundException) Activator.CreateInstance(typeof (TObjectVersionNotFoundException), (object) objectVersionID);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObject(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    out IDBObject result)
  {
    result = session.GetObject(objectVersionID, false);
    return result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObject<TDbObjectInterface>(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    out TDbObjectInterface result)
    where TDbObjectInterface : class, IDBObject
  {
    IDBObject result1;
    if (session.TryGetObject(objectVersionID, out result1) && result1 is TDbObjectInterface dbObjectInterface)
    {
      result = dbObjectInterface;
      return true;
    }
    result = default (TDbObjectInterface);
    return false;
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDbObjectInterface GetObject<TDbObjectInterface>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid objectVersionGuid,
    bool throwExceptOnError = true)
    where TDbObjectInterface : class, IDBObject
  {
    IDBObject dbObject = session.GetObject(objectVersionGuid, false);
    if (dbObject == null)
    {
      if (throwExceptOnError)
        throw new ObjectVersionNotFoundException(objectVersionGuid);
      return default (TDbObjectInterface);
    }
    return dbObject is TDbObjectInterface dbObjectInterface ? dbObjectInterface : throw new ObjectVersionNotFoundException(objectVersionGuid);
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDbObjectInterface GetObject<TDbObjectInterface, TObjectVersionNotFoundException>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid objectVersionGuid,
    bool throwExceptOnError = true)
    where TDbObjectInterface : class, IDBObject
    where TObjectVersionNotFoundException : ObjectVersionNotFoundException
  {
    IDBObject dbObject = session.GetObject(objectVersionGuid, false);
    if (dbObject == null)
    {
      if (throwExceptOnError)
        throw (object) (TObjectVersionNotFoundException) Activator.CreateInstance(typeof (TObjectVersionNotFoundException), (object) objectVersionGuid);
      return default (TDbObjectInterface);
    }
    return dbObject is TDbObjectInterface dbObjectInterface ? dbObjectInterface : throw (object) (TObjectVersionNotFoundException) Activator.CreateInstance(typeof (TObjectVersionNotFoundException), (object) objectVersionGuid);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObject(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid objectVersionGuid,
    out IDBObject result)
  {
    result = session.GetObject(objectVersionGuid, false);
    return result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObject<TDbObjectInterface>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid objectVersionGuid,
    out TDbObjectInterface result)
    where TDbObjectInterface : class, IDBObject
  {
    IDBObject result1;
    if (session.TryGetObject(objectVersionGuid, out result1) && result1 is TDbObjectInterface dbObjectInterface)
    {
      result = dbObjectInterface;
      return true;
    }
    result = default (TDbObjectInterface);
    return false;
  }

  [ContractAnnotation("throwExceptOnError:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDbRelationInterface GetRelation<TDbRelationInterface>(
    [NotNull] this IUserSession session,
    [NotEmpty] long relationID,
    bool throwExceptOnError = true)
    where TDbRelationInterface : class, IDBRelation
  {
    IDBRelation relation = session.GetRelation(relationID, false);
    if (relation == null)
    {
      if (throwExceptOnError)
        throw new RelationNotFoundException(relationID);
      return default (TDbRelationInterface);
    }
    return relation is TDbRelationInterface relationInterface ? relationInterface : throw new RelationNotFoundException(relationID);
  }

  [Obsolete("Используйте GetRelation<TDbRelationInterface, TRelationNotFoundException>")]
  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDbRelationInterface GetRelation<TDbRelationInterface, TRelationNotFoundException, TInvalidCastException>(
    [NotNull] this IUserSession session,
    long relationID,
    bool throwExceptOnError = true)
    where TDbRelationInterface : IDBRelation
    where TRelationNotFoundException : RelationNotFoundException
    where TInvalidCastException : InvalidCastException
  {
    IDBRelation relation = session.GetRelation(relationID, false);
    if (relation == null & throwExceptOnError)
      throw (object) (TRelationNotFoundException) Activator.CreateInstance(typeof (TRelationNotFoundException), (object) relationID);
    if (relation == null || relation is TDbRelationInterface)
      return (TDbRelationInterface) relation;
    TInvalidCastException instance;
    if (!typeof (IRelationException).IsAssignableFrom(typeof (TInvalidCastException)))
      instance = (TInvalidCastException) Activator.CreateInstance(typeof (TInvalidCastException));
    else
      instance = (TInvalidCastException) Activator.CreateInstance(typeof (TInvalidCastException), (object) relationID);
    throw (object) instance;
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDbRelationInterface GetRelation<TDbRelationInterface, TRelationNotFoundException>(
    [NotNull] this IUserSession session,
    [NotEmpty] long relationID,
    bool throwExceptOnError = true)
    where TDbRelationInterface : class, IDBRelation
    where TRelationNotFoundException : RelationNotFoundException
  {
    IDBRelation relation = session.GetRelation(relationID, false);
    if (relation == null)
    {
      if (throwExceptOnError)
        throw (object) (TRelationNotFoundException) Activator.CreateInstance(typeof (TRelationNotFoundException), (object) relationID);
      return default (TDbRelationInterface);
    }
    return relation is TDbRelationInterface relationInterface ? relationInterface : throw (object) (TRelationNotFoundException) Activator.CreateInstance(typeof (TRelationNotFoundException), (object) relationID);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetRelation(
    [NotNull] this IUserSession session,
    [NotEmpty] long relationID,
    out IDBRelation result)
  {
    result = session.GetRelation(relationID, false);
    return result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetRelation<TDbRelationInterface>(
    [NotNull] this IUserSession session,
    [NotEmpty] long relationID,
    out TDbRelationInterface result)
    where TDbRelationInterface : class, IDBRelation
  {
    IDBRelation result1;
    if (session.TryGetRelation(relationID, out result1) && result1 is TDbRelationInterface relationInterface)
    {
      result = relationInterface;
      return true;
    }
    result = default (TDbRelationInterface);
    return false;
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDbRelationInterface GetRelation<TDbRelationInterface>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid relationGuid,
    bool throwExceptOnError = true)
    where TDbRelationInterface : class, IDBRelation
  {
    IDBRelation relation = session.GetRelation(relationGuid, false);
    if (relation == null)
    {
      if (throwExceptOnError)
        throw new RelationNotFoundException(relationGuid);
      return default (TDbRelationInterface);
    }
    return relation is TDbRelationInterface relationInterface ? relationInterface : throw new RelationNotFoundException(relationGuid);
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDbRelationInterface GetRelation<TDbRelationInterface, TRelationNotFoundException>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid relationGuid,
    bool throwExceptOnError = true)
    where TDbRelationInterface : class, IDBRelation
    where TRelationNotFoundException : RelationNotFoundException
  {
    IDBRelation relation = session.GetRelation(relationGuid, false);
    if (relation == null)
    {
      if (throwExceptOnError)
        throw (object) (TRelationNotFoundException) Activator.CreateInstance(typeof (TRelationNotFoundException), (object) relationGuid);
      return default (TDbRelationInterface);
    }
    return relation is TDbRelationInterface relationInterface ? relationInterface : throw (object) (TRelationNotFoundException) Activator.CreateInstance(typeof (TRelationNotFoundException), (object) relationGuid);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetRelation(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid relationGuid,
    out IDBRelation result)
  {
    result = session.GetRelation(relationGuid, false);
    return result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetRelation<TDbRelationInterface>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid relationGuid,
    out TDbRelationInterface result)
    where TDbRelationInterface : class, IDBRelation
  {
    IDBRelation result1;
    if (session.TryGetRelation(relationGuid, out result1) && result1 is TDbRelationInterface relationInterface)
    {
      result = relationInterface;
      return true;
    }
    result = default (TDbRelationInterface);
    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectInfo(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    out QuickObjectInfo objectInfo)
  {
    objectInfo = session.GetObjectInfo(objectID);
    return !objectInfo.Empty;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectInfo(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid objectGuid,
    out QuickObjectInfo objectInfo)
  {
    objectInfo = session.GetObjectInfo(objectGuid);
    return !objectInfo.Empty;
  }

  [ContractAnnotation("throwExceptionIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBObjectType GetObjectType(
    [NotNull] this IUserSession session,
    [NotEmpty] int objectTypeID,
    bool throwExceptionIfNotFound,
    [CanBeNull] string exceptionMessage = null)
  {
    IDBObjectType objectType = session.GetObjectType(objectTypeID);
    return !(objectType == null & throwExceptionIfNotFound) ? objectType : throw new ObjectTypeNotFoundException(objectTypeID, exceptionMessage);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectType(
    [NotNull] this IUserSession session,
    [NotEmpty] int objectTypeID,
    out IDBObjectType result)
  {
    result = session.GetObjectType(objectTypeID, false);
    return result != null;
  }

  [NotNull]
  public static TDBObjectCollection GetObjectsCollection<TDBObjectCollection>(
    [NotNull] this IUserSession session,
    [NotEmpty] int objectTypeID)
    where TDBObjectCollection : class, IDBObjectCollection
  {
    return session.GetObjectCollection(objectTypeID).CastInterfaceToClass<IDBObjectCollection, TDBObjectCollection>();
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObjectCollection GetObjectsCollection<TDBObjectCollection>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid objectTypeGuid)
    where TDBObjectCollection : class, IDBObjectCollection
  {
    return session.GetObjectCollection(objectTypeGuid).CastInterfaceToClass<IDBObjectCollection, TDBObjectCollection>();
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TRelationCollectionType GetRelationCollection<TRelationCollectionType>(
    [NotNull] this IUserSession session,
    int relationTypeID,
    [CanBeNull, CanBeEmpty] string filtrationOwnerID = null)
    where TRelationCollectionType : class, IDBRelationCollection
  {
    return !string.IsNullOrWhiteSpace(filtrationOwnerID) ? session.GetRelationCollection(relationTypeID).CastInterfaceToClass<IDBRelationCollection, TRelationCollectionType>() : session.GetRelationCollection(relationTypeID, filtrationOwnerID).CastInterfaceToClass<IDBRelationCollection, TRelationCollectionType>();
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TRelationCollectionType GetRelationCollection<TRelationCollectionType>(
    [NotNull] this IUserSession session,
    [NotEmpty] int relationTypeID,
    [CanBeNull] VersionsRule rule)
    where TRelationCollectionType : class, IDBRelationCollection
  {
    return rule == null ? session.GetRelationCollection(relationTypeID).CastInterfaceToClass<IDBRelationCollection, TRelationCollectionType>() : session.GetRelationCollection(relationTypeID, rule).CastInterfaceToClass<IDBRelationCollection, TRelationCollectionType>();
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable SelectObjects(
    [NotNull] this IUserSession session,
    [NotEmpty] int objectTypeID,
    [CanBeNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    [CanBeNull, ItemNotNull] object[] sortColumns = null,
    [CanBeNull] SortOrders[] orders = null,
    long lastKeyValue = 0,
    [CanBeNull] object lastOrderValue = null,
    int recordCount = -1,
    bool failIfNotFound = true,
    [CanBeNull] string tableName = null)
  {
    return session.GetObjectCollection(objectTypeID).Select(columns, conditions, sortColumns, orders, lastKeyValue, lastOrderValue, recordCount, failIfNotFound, tableName);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable SelectObjects(
    [NotNull] this IUserSession session,
    [NotEmpty] int objectTypeID,
    [CanBeNull] ConditionStructure[] conditions,
    [CanBeNull] ColumnDescriptor[] columns,
    [CanBeNull, ItemNotNull] object[] sortColumns = null,
    [CanBeNull] SortOrders[] orders = null,
    long lastKeyValue = 0,
    [CanBeNull] object lastOrderValue = null,
    int recordCount = -1,
    bool failIfNotFound = true,
    [CanBeNull] string tableName = null)
  {
    return session.GetObjectCollection(objectTypeID).Select(columns, conditions, sortColumns, orders, lastKeyValue, lastOrderValue, recordCount, failIfNotFound, tableName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable SelectWithLocalObjects(
    [NotNull] this IUserSession session,
    [NotEmpty] int objectTypeID,
    [CanBeNull] ConditionStructure[] conditions,
    [CanBeNull] ColumnDescriptor[] columns,
    long lastKeyValue = 0,
    [CanBeNull] object lastOrderValue = null,
    int recordCount = -1,
    bool failIfNotFound = true,
    [CanBeNull] string tableName = null)
  {
    return session.GetObjectCollection(objectTypeID).SelectWithLocalObjects(columns, conditions, lastKeyValue, lastOrderValue, recordCount, failIfNotFound, tableName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable SelectWithLocalObjects(
    [NotNull] this IUserSession session,
    [NotEmpty] int objectTypeID,
    [CanBeNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    long lastKeyValue = 0,
    [CanBeNull] object lastOrderValue = null,
    int recordCount = -1,
    bool failIfNotFound = true,
    [CanBeNull] string tableName = null)
  {
    return session.GetObjectCollection(objectTypeID).SelectWithLocalObjects(columns, conditions, lastKeyValue, lastOrderValue, recordCount, failIfNotFound, tableName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable ObjectConsistOf(
    [NotNull] this IUserSession session,
    [NotEmpty] long projectID,
    [NotEmpty] int relationTypeID,
    [NotNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return session.GetRelationCollection(relationTypeID).ConsistOf(projectID, conditions, columns, recursive, actualDate);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable ObjectConsistOf(
    [NotNull] this IUserSession session,
    [NotEmpty] long projectID,
    [NotEmpty] int relationTypeID,
    [CanBeNull] ConditionStructure[] conditions,
    [NotNull] ColumnDescriptor[] columns,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return session.GetRelationCollection(relationTypeID).ConsistOf(projectID, conditions, columns, recursive, actualDate);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable GetObjectComposition(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    [CanBeEmpty] int objectTypeID,
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchRelationTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchObjectTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> expandObjectTypes = null,
    bool grouping = false,
    [CanBeNull] VersionsRule rule = null,
    [NotNull] string filtrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545",
    int loadLevels = -1)
  {
    ICompositionLoadService customService = session.GetCustomService<ICompositionLoadService>();
    if (objectTypeID == -1)
    {
      QuickObjectInfo objectInfo;
      if (!session.TryGetObjectInfo(objectVersionID, out objectInfo))
        throw new ObjectNotFoundException(objectVersionID);
      objectTypeID = objectInfo.ObjectTypeID;
    }
    if (searchRelationTypes == null || searchObjectTypes == null)
    {
      List<IMSApplicability> typeApplicabilities = MetaDataHelperService.Instance.GetObjectTypeApplicabilities(objectTypeID);
      if (typeApplicabilities.Count == 0)
      {
        searchRelationTypes = (IReadOnlyCollection<int>) Array.Empty<int>();
        searchObjectTypes = (IReadOnlyCollection<int>) Array.Empty<int>();
      }
      else
      {
        searchRelationTypes = (IReadOnlyCollection<int>) typeApplicabilities.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (typeApplicability => typeApplicability.RelationTypeID)).Distinct<int>().ToList<int>();
        searchObjectTypes = (IReadOnlyCollection<int>) typeApplicabilities.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (typeApplicability => typeApplicability.ChildObjectTypeID)).Distinct<int>().ToList<int>();
      }
    }
    // ISSUE: variable of a boxed type
    __Boxed<Guid> sessionGuid = (System.ValueType) session.SessionGUID;
    long objectId = objectVersionID;
    int objectType = objectTypeID;
    IReadOnlyCollection<int> searchRelationTypes1 = searchRelationTypes;
    IReadOnlyCollection<int> searchObjectTypes1 = searchObjectTypes;
    IReadOnlyCollection<ColumnDescriptor> columns1 = columns;
    int num = grouping ? 1 : 0;
    VersionsRule rule1 = rule;
    IReadOnlyCollection<ConditionStructure> conditions1 = conditions;
    string filtrationOwnerId = filtrationOwnerID;
    int loadLevels1 = loadLevels;
    IReadOnlyCollection<int> expandObjectTypes1 = expandObjectTypes;
    return customService.LoadComposition((object) sessionGuid, objectId, objectType, (IEnumerable<int>) searchRelationTypes1, (IEnumerable<int>) searchObjectTypes1, (IEnumerable<ColumnDescriptor>) columns1, true, num != 0, rule1, (IEnumerable<ConditionStructure>) conditions1, filtrationOwnerId, (HybridDictionary) null, loadLevels1, (IEnumerable<int>) expandObjectTypes1);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable GetObjectComposition(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchRelationTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchObjectTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> expandObjectTypes = null,
    bool grouping = false,
    [CanBeNull] VersionsRule rule = null,
    [NotNull] string filtrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545",
    int loadLevels = -1)
  {
    return session.GetObjectComposition(objectVersionID, -1, columns, conditions, searchRelationTypes, searchObjectTypes, expandObjectTypes, grouping, rule, filtrationOwnerID, loadLevels);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable ObjectEntersIn(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotEmpty] int relationTypeID,
    [NotNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return session.GetRelationCollection(relationTypeID).EntersIn(new DBRecordSetParams(conditions, columns), objectID, recursive, actualDate ?? DateTime.UtcNow + session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable ObjectEntersIn(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotEmpty] int relationTypeID,
    [CanBeNull] ConditionStructure[] conditions,
    [NotNull] ColumnDescriptor[] columns,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return session.GetRelationCollection(relationTypeID).EntersIn(new DBRecordSetParams(conditions, columns), objectID, recursive, actualDate ?? DateTime.UtcNow + session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable ObjectEntersInVersion(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relationTypeID,
    [NotNull] ColumnDescriptor[] columns,
    [CanBeNull] ConditionStructure[] conditions = null,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return session.GetRelationCollection(relationTypeID).EntersInVersion(new DBRecordSetParams(conditions, columns), objectVersionID, recursive, actualDate ?? DateTime.UtcNow + session.TimeZoneOffset);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable ObjectEntersInVersion(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relationTypeID,
    [CanBeNull] ConditionStructure[] conditions,
    [NotNull] ColumnDescriptor[] columns,
    bool recursive = false,
    DateTime? actualDate = null)
  {
    return session.GetRelationCollection(relationTypeID).EntersInVersion(new DBRecordSetParams(conditions, columns), objectVersionID, recursive, actualDate ?? DateTime.UtcNow + session.TimeZoneOffset);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable GetObjectApplicability(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    [CanBeEmpty] int objectTypeID,
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchRelationTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchObjectTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> expandObjectTypes = null,
    bool grouping = false,
    [CanBeNull] VersionsRule rule = null,
    [NotNull] string filtrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545",
    int loadLevels = -1)
  {
    ICompositionLoadService customService = session.GetCustomService<ICompositionLoadService>();
    if (objectTypeID == -1)
    {
      QuickObjectInfo objectInfo;
      if (!session.TryGetObjectInfo(objectVersionID, out objectInfo))
        throw new ObjectNotFoundException(objectVersionID);
      objectTypeID = objectInfo.ObjectTypeID;
    }
    if (searchRelationTypes == null || searchObjectTypes == null)
    {
      List<IMSApplicability> parentApplicabilities = MetaDataHelperService.Instance.GetObjectTypeParentApplicabilities(objectTypeID);
      if (parentApplicabilities.Count == 0)
      {
        searchRelationTypes = (IReadOnlyCollection<int>) Array.Empty<int>();
        searchObjectTypes = (IReadOnlyCollection<int>) Array.Empty<int>();
      }
      else
      {
        searchRelationTypes = (IReadOnlyCollection<int>) parentApplicabilities.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (typeApplicability => typeApplicability.RelationTypeID)).Distinct<int>().ToList<int>();
        searchObjectTypes = (IReadOnlyCollection<int>) parentApplicabilities.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (typeApplicability => typeApplicability.InObjectType)).Distinct<int>().ToList<int>();
      }
    }
    // ISSUE: variable of a boxed type
    __Boxed<Guid> sessionGuid = (System.ValueType) session.SessionGUID;
    long objectId = objectVersionID;
    int objectType = objectTypeID;
    IReadOnlyCollection<int> searchRelationTypes1 = searchRelationTypes;
    IReadOnlyCollection<int> searchObjectTypes1 = searchObjectTypes;
    IReadOnlyCollection<ColumnDescriptor> columns1 = columns;
    int num = grouping ? 1 : 0;
    VersionsRule rule1 = rule;
    IReadOnlyCollection<ConditionStructure> conditions1 = conditions;
    string filtrationOwnerId = filtrationOwnerID;
    int loadLevels1 = loadLevels;
    IReadOnlyCollection<int> expandObjectTypes1 = expandObjectTypes;
    return customService.LoadComposition((object) sessionGuid, objectId, objectType, (IEnumerable<int>) searchRelationTypes1, (IEnumerable<int>) searchObjectTypes1, (IEnumerable<ColumnDescriptor>) columns1, false, num != 0, rule1, (IEnumerable<ConditionStructure>) conditions1, filtrationOwnerId, (HybridDictionary) null, loadLevels1, (IEnumerable<int>) expandObjectTypes1);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DataTable GetObjectApplicability(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    [NotNull] IReadOnlyCollection<ColumnDescriptor> columns,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchRelationTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> searchObjectTypes = null,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> expandObjectTypes = null,
    bool grouping = false,
    [CanBeNull] VersionsRule rule = null,
    [NotNull] string filtrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545",
    int loadLevels = -1)
  {
    return session.GetObjectApplicability(objectVersionID, -1, columns, conditions, searchRelationTypes, searchObjectTypes, expandObjectTypes, grouping, rule, filtrationOwnerID, loadLevels);
  }

  [NotNull]
  [MustUseReturnValue]
  public static IDisposableServiceProvider GetServiceProvider(
    [NotNull] this IUserSession session,
    [CanBeNull] string callingContextName = null)
  {
    return (IDisposableServiceProvider) new UserSessionExtensions.UserSessionServiceProvider(session, callingContextName);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object GetObjectAttributeValue(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    return attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid) ? (object) null : attributeValueByGuid;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectIntAttr(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute,
    out int result)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    if (attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid))
    {
      result = 0;
      return false;
    }
    result = Convert.ToInt32(attributeValueByGuid);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectLongAttr(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute,
    out long result)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    if (attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid))
    {
      result = 0L;
      return false;
    }
    result = Convert.ToInt64(attributeValueByGuid);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectObjLinkAttr(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute,
    out long result)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    if (attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid))
    {
      result = 0L;
      return false;
    }
    result = Convert.ToInt64(attributeValueByGuid);
    return !Intermech.Check.ObjectIdIsEmpty(result);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool CheckObjectHasObjLink(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    return attributeValueByGuid != null && !DBNull.Value.Equals(attributeValueByGuid) && !Intermech.Check.ObjectIdIsEmpty(Convert.ToInt64(attributeValueByGuid));
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectLongAttr(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute,
    out string result)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    if (attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid))
    {
      result = (string) null;
      return false;
    }
    result = Convert.ToString(attributeValueByGuid);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectBoolAttr(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute,
    out bool result)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    if (attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid))
    {
      result = false;
      return false;
    }
    result = Convert.ToBoolean(attributeValueByGuid);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectDateTimeAttr(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute,
    out DateTime result)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    if (attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid))
    {
      result = new DateTime();
      return false;
    }
    result = Convert.ToDateTime(attributeValueByGuid, (IFormatProvider) CultureInfo.InvariantCulture);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectGuidAttr(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute,
    out Guid result)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    if (attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid))
    {
      result = new Guid();
      return false;
    }
    switch (attributeValueByGuid)
    {
      case Guid guid:
        result = guid;
        return true;
      case string input:
        result = string.IsNullOrWhiteSpace(input) ? Guid.Empty : Guid.Parse(input);
        return true;
      default:
        throw new InvalidOperationException("Unknown field type for Guid: " + attributeValueByGuid.GetType().Name);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectEnumAttr<TEnum>(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute,
    out TEnum result)
    where TEnum : struct, Enum
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    if (attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid))
    {
      result = default (TEnum);
      return false;
    }
    result = (TEnum) attributeValueByGuid;
    return true;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectMeasuredValueAttr(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute,
    [NotNull] MeasureDescriptor defaultMeasure,
    out MeasuredValue result)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    if (attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid))
    {
      result = (MeasuredValue) null;
      return false;
    }
    switch (attributeValueByGuid)
    {
      case IDBMeasureAttribute measureAttribute:
        result = measureAttribute.Value;
        return true;
      case string mValue:
        if (string.IsNullOrWhiteSpace(mValue))
        {
          result = (MeasuredValue) null;
          return false;
        }
        result = MeasureHelper.Instance.ConvertToMeasuredValue(mValue, defaultMeasure, true);
        return true;
      default:
        throw new InvalidOperationException("Unknown field type for MeasuredValue: " + attributeValueByGuid.GetType().Name);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetObjectDoubleAttr(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectID,
    [NotNull] SystemAttribute attribute,
    out double result)
  {
    object attributeValueByGuid = session.GetObjectAttributeValueByGuid(objectID, attribute.Guid);
    if (attributeValueByGuid == null || DBNull.Value.Equals(attributeValueByGuid))
    {
      result = 0.0;
      return false;
    }
    result = Convert.ToDouble(attributeValueByGuid, (IFormatProvider) CultureInfo.InvariantCulture);
    return true;
  }

  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ITransactionKeeper Transaction(
    [NotNull] this IUserSession userSession,
    [NotNull, NotWhitespace] string name,
    TransactionKeeperDisposeAction disposeAction = TransactionKeeperDisposeAction.AutoCommit,
    ExternalTransactionRelationship externalTransactionRelationship = ExternalTransactionRelationship.CreateNestedTransaction,
    [CanBeNull] Func<bool> getCanCommit = null,
    [CanBeNull] Action beforeCommit = null,
    [CanBeNull] Action afterCommit = null,
    [CanBeNull] Action<ExactRollbackCause> beforeRollback = null,
    [CanBeNull] Action<ExactRollbackCause> afterRollback = null,
    [CanBeNull] SynchronizationContext synchronizationContext = null,
    [CanBeNull] CancellationToken? cancellationToken = null,
    [CanBeNull, CallerFilePath] string callerFilePath = null)
  {
    return (ITransactionKeeper) new TransactionKeeper(userSession, name, disposeAction, externalTransactionRelationship, getCanCommit, beforeCommit, afterCommit, beforeRollback, afterRollback, synchronizationContext, cancellationToken, callerFilePath);
  }

  [CanBeNull]
  public delegate T NotNullSessionFunc<T>([NotNull] IUserSession session);

  [CanBeNull]
  public delegate void NotNullSessionAction([NotNull] IUserSession session);

  private class UserSessionServiceProvider : 
    IDisposableServiceProvider,
    IServiceProvider,
    IDisposable
  {
    [NotNull]
    private IUserSession _session;
    [CanBeNull]
    private readonly string _creationContextName;

    public UserSessionServiceProvider([NotNull] IUserSession session, [CanBeNull] string creationContextName)
    {
      this._session = session;
      this._creationContextName = creationContextName;
    }

    [CanBeNull]
    public object GetService([NotNull] Type serviceType)
    {
      return this._session != null ? this._session.GetCustomService(serviceType) : throw new ObjectDisposedException(this._creationContextName != null ? "UserSessionServiceProvider: " + this._creationContextName : nameof (UserSessionServiceProvider));
    }

    public void Dispose() => this._session = (IUserSession) null;
  }
}
