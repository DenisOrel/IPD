// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSessionExtensions
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Kernel;

public static class UserSessionExtensions
{
  [ContractAnnotation("throwExceptOnError:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObject GetServerObject<TDBObject>(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    bool throwExceptOnError = true)
    where TDBObject : DBObject
  {
    IDBObject dbObject1 = session.GetObject(objectVersionID, false);
    if (dbObject1 == null)
    {
      if (throwExceptOnError)
        throw new ObjectVersionNotFoundException(objectVersionID);
      return default (TDBObject);
    }
    return dbObject1 is TDBObject dbObject2 ? dbObject2 : throw new ObjectVersionNotFoundException(objectVersionID);
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObject GetServerObject<TDBObject, TObjectVersionNotFoundException>(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    bool throwExceptOnError = true)
    where TDBObject : DBObject
    where TObjectVersionNotFoundException : ObjectVersionNotFoundException
  {
    IDBObject dbObject1 = session.GetObject(objectVersionID, false);
    if (dbObject1 == null)
    {
      if (throwExceptOnError)
        throw (object) (TObjectVersionNotFoundException) Activator.CreateInstance(typeof (TObjectVersionNotFoundException), (object) objectVersionID);
      return default (TDBObject);
    }
    return dbObject1 is TDBObject dbObject2 ? dbObject2 : throw (object) (TObjectVersionNotFoundException) Activator.CreateInstance(typeof (TObjectVersionNotFoundException), (object) objectVersionID);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetServerObject(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    out DBObject result)
  {
    ref DBObject local = ref result;
    IDBObject dbObject1 = session.GetObject(objectVersionID, false);
    DBObject dbObject2 = dbObject1 != null ? dbObject1.CastToClass<DBObject>() : (DBObject) null;
    local = dbObject2;
    return result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetServerObject<TDBObject>(
    [NotNull] this IUserSession session,
    [NotEmpty] long objectVersionID,
    out TDBObject result)
    where TDBObject : DBObject
  {
    IDBObject result1;
    if (session.TryGetObject(objectVersionID, out result1))
    {
      result = result1.CastInterfaceToClass<IDBObject, TDBObject>();
      return true;
    }
    result = default (TDBObject);
    return false;
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObject GetServerObject<TDBObject>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid objectVersionGuid,
    bool throwExceptOnError = true)
    where TDBObject : DBObject
  {
    IDBObject dbObject1 = session.GetObject(objectVersionGuid, false);
    if (dbObject1 == null)
    {
      if (throwExceptOnError)
        throw new ObjectVersionNotFoundException(objectVersionGuid);
      return default (TDBObject);
    }
    return dbObject1 is TDBObject dbObject2 ? dbObject2 : throw new ObjectVersionNotFoundException(objectVersionGuid);
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObject GetServerObject<TDBObject, TObjectVersionNotFoundException>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid objectVersionGuid,
    bool throwExceptOnError = true)
    where TDBObject : DBObject
    where TObjectVersionNotFoundException : ObjectVersionNotFoundException
  {
    IDBObject dbObject1 = session.GetObject(objectVersionGuid, false);
    if (dbObject1 == null)
    {
      if (throwExceptOnError)
        throw (object) (TObjectVersionNotFoundException) Activator.CreateInstance(typeof (TObjectVersionNotFoundException), (object) objectVersionGuid);
      return default (TDBObject);
    }
    return dbObject1 is TDBObject dbObject2 ? dbObject2 : throw (object) (TObjectVersionNotFoundException) Activator.CreateInstance(typeof (TObjectVersionNotFoundException), (object) objectVersionGuid);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetServerObject(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid objectVersionGuid,
    out DBObject result)
  {
    ref DBObject local = ref result;
    IDBObject dbObject1 = session.GetObject(objectVersionGuid, false);
    DBObject dbObject2 = dbObject1 != null ? dbObject1.CastToClass<DBObject>() : (DBObject) null;
    local = dbObject2;
    return result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetServerObject<TDBObject>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid objectVersionGuid,
    out TDBObject result)
    where TDBObject : DBObject
  {
    IDBObject result1;
    if (session.TryGetObject(objectVersionGuid, out result1))
    {
      result = result1.CastInterfaceToClass<IDBObject, TDBObject>();
      return true;
    }
    result = default (TDBObject);
    return false;
  }

  [ContractAnnotation("throwExceptOnError:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBRelation GetServerRelation<TDBRelation>(
    [NotNull] this IUserSession session,
    [NotEmpty] long relationID,
    bool throwExceptOnError = true)
    where TDBRelation : DBRelation
  {
    IDBRelation relation = session.GetRelation(relationID, false);
    if (relation == null)
    {
      if (throwExceptOnError)
        throw new RelationNotFoundException(relationID);
      return default (TDBRelation);
    }
    return relation is TDBRelation dbRelation ? dbRelation : throw new RelationNotFoundException(relationID);
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBRelation GetServerRelation<TDBRelation, TRelationNotFoundException>(
    [NotNull] this IUserSession session,
    [NotEmpty] long relationID,
    bool throwExceptOnError = true)
    where TDBRelation : DBRelation
    where TRelationNotFoundException : RelationNotFoundException
  {
    IDBRelation relation = session.GetRelation(relationID, false);
    if (relation == null)
    {
      if (throwExceptOnError)
        throw (object) (TRelationNotFoundException) Activator.CreateInstance(typeof (TRelationNotFoundException), (object) relationID);
      return default (TDBRelation);
    }
    return relation is TDBRelation dbRelation ? dbRelation : throw (object) (TRelationNotFoundException) Activator.CreateInstance(typeof (TRelationNotFoundException), (object) relationID);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetServerRelation(
    [NotNull] this IUserSession session,
    [NotEmpty] long relationID,
    out DBRelation result)
  {
    ref DBRelation local = ref result;
    IDBRelation relation = session.GetRelation(relationID, false);
    DBRelation dbRelation = relation != null ? relation.CastInterfaceToClass<IDBRelation, DBRelation>() : (DBRelation) null;
    local = dbRelation;
    return result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetServerRelation<TDBRelation>(
    [NotNull] this IUserSession session,
    [NotEmpty] long relationID,
    out TDBRelation result)
    where TDBRelation : DBRelation
  {
    IDBRelation result1;
    if (session.TryGetRelation(relationID, out result1))
    {
      result = result1.CastInterfaceToClass<IDBRelation, TDBRelation>();
      return true;
    }
    result = default (TDBRelation);
    return false;
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBRelation GetServerRelation<TDBRelation>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid relationGuid,
    bool throwExceptOnError = true)
    where TDBRelation : DBRelation
  {
    IDBRelation relation = session.GetRelation(relationGuid, false);
    if (relation == null)
    {
      if (throwExceptOnError)
        throw new RelationNotFoundException(relationGuid);
      return default (TDBRelation);
    }
    return relation is TDBRelation dbRelation ? dbRelation : throw new RelationNotFoundException(relationGuid);
  }

  [ContractAnnotation("throwExceptOnError:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBRelation GetServerRelation<TDBRelation, TRelationNotFoundException>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid relationGuid,
    bool throwExceptOnError = true)
    where TDBRelation : DBRelation
    where TRelationNotFoundException : RelationNotFoundException
  {
    IDBRelation relation = session.GetRelation(relationGuid, false);
    if (relation == null)
    {
      if (throwExceptOnError)
        throw (object) (TRelationNotFoundException) Activator.CreateInstance(typeof (TRelationNotFoundException), (object) relationGuid);
      return default (TDBRelation);
    }
    return relation is TDBRelation dbRelation ? dbRelation : throw (object) (TRelationNotFoundException) Activator.CreateInstance(typeof (TRelationNotFoundException), (object) relationGuid);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetServerRelation(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid relationGuid,
    out DBRelation result)
  {
    ref DBRelation local = ref result;
    IDBRelation relation = session.GetRelation(relationGuid, false);
    DBRelation dbRelation = relation != null ? relation.CastInterfaceToClass<IDBRelation, DBRelation>() : (DBRelation) null;
    local = dbRelation;
    return result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetServerRelation<TDBRelation>(
    [NotNull] this IUserSession session,
    [NotEmpty] Guid relationGuid,
    out TDBRelation result)
    where TDBRelation : DBRelation
  {
    IDBRelation result1;
    if (session.TryGetRelation(relationGuid, out result1))
    {
      result = result1.CastInterfaceToClass<IDBRelation, TDBRelation>();
      return true;
    }
    result = default (TDBRelation);
    return false;
  }

  [NotNull]
  public static TDBObjectCollection GetServerObjectsCollection<TDBObjectCollection>(
    [NotNull] this IUserSession userSession,
    [NotEmpty] int objectTypeID)
    where TDBObjectCollection : DBObjectCollection
  {
    return ((UserSession) userSession).GetObjectCollection(objectTypeID).CastInterfaceToClass<IDBObjectCollection, TDBObjectCollection>();
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObjectCollection GetServerObjectsCollection<TDBObjectCollection>(
    [NotNull] this IUserSession userSession,
    [NotEmpty] Guid objectTypeGuid)
    where TDBObjectCollection : DBObjectCollection
  {
    return ((UserSession) userSession).GetObjectCollection(objectTypeGuid).CastInterfaceToClass<IDBObjectCollection, TDBObjectCollection>();
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DBRelation CreateRelationCopy(
    [NotNull] this IUserSession session,
    [NotNull] IDBRelation relationPrototype,
    [NotEmpty] long projectObjectID,
    [NotEmpty] long partID,
    [NotEmpty] long partObjectID)
  {
    return session.CreateRelationCopy<DBRelation>(relationPrototype, projectObjectID, partID, partObjectID);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBRelation CreateRelationCopy<TDBRelation>(
    [NotNull] this IUserSession session,
    [NotNull] IDBRelation relationPrototype,
    [NotEmpty] long projectObjectID,
    [NotEmpty] long partID,
    [NotEmpty] long partObjectID)
    where TDBRelation : DBRelation
  {
    IDBRelationCollection relationCollection1 = session.GetRelationCollection(relationPrototype.TypeID);
    if (relationCollection1 is IServerDBRelationCollection relationCollection2)
      relationCollection2.AssignMode = 8192 /*0x2000*/;
    return relationCollection1.Create(new NewRelationProperties()
    {
      BeginDate = relationPrototype.CreateDate,
      PrototypeRelation = relationPrototype,
      PrototypeRelationID = relationPrototype.RelationID,
      ProjectObjectID = projectObjectID,
      PartID = partID,
      PartObjectID = partObjectID
    }).CastInterfaceToClass<IDBRelation, TDBRelation>();
  }
}
