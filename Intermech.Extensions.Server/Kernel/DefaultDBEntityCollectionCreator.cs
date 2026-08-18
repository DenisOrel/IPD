// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DefaultDBEntityCollectionCreator
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Kernel;

internal class DefaultDBEntityCollectionCreator : 
  IDBObjectCollectionCreator,
  IDBRelationCollectionCreator
{
  [NotNull]
  private static readonly Type[] DefaultObjectConstructorParamTypes = new Type[2]
  {
    typeof (UserSession),
    typeof (int)
  };
  [NotNull]
  private static readonly object _syncObject = new object();
  [CanBeNull]
  private static DefaultDBEntityCollectionCreator _instance;
  [NotNull]
  private readonly ConcurrentDictionary<Guid, ConstructorInfo> _knownTypes = new ConcurrentDictionary<Guid, ConstructorInfo>();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void RegisterObjectType([NotEmpty] int objectTypeID, [NotNull] Type dbObjectType, bool overwrite = false)
  {
    DefaultDBEntityCollectionCreator.RegisterObjectType(MetaDataHelperService.Instance.GetObjectTypeGuid(objectTypeID), dbObjectType, overwrite);
  }

  public static void RegisterObjectType([NotEmpty] Guid objectTypeGuid, [NotNull] Type dbObjectType, bool overwrite = false)
  {
    ConstructorInfo constructorInfo = ((IEnumerable<ConstructorInfo>) dbObjectType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).FirstOrDefault<ConstructorInfo>((Func<ConstructorInfo, bool>) (constructor => ((IEnumerable<ParameterInfo>) constructor.GetParameters()).Select<ParameterInfo, Type>((Func<ParameterInfo, Type>) (paramInfo => paramInfo.ParameterType)).SequenceEqual<Type>((IEnumerable<Type>) DefaultDBEntityCollectionCreator.DefaultObjectConstructorParamTypes)));
    Intermech.Diagnostics.Check.NotNull<ConstructorInfo>(constructorInfo, $"DBObjectCollection type {dbObjectType} must have constructor with parameters ({typeof (UserSession)}, {typeof (int)})");
    if (overwrite)
      DefaultDBEntityCollectionCreator.Instance._knownTypes.AddOrUpdate(objectTypeGuid, constructorInfo, (Func<Guid, ConstructorInfo, ConstructorInfo>) ((_, __) => constructorInfo));
    else if (!DefaultDBEntityCollectionCreator.Instance._knownTypes.TryAdd(objectTypeGuid, constructorInfo))
      throw new Exception($"Object type guid {objectTypeGuid} already registered!");
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void RegisterRelationType([NotEmpty] int relationTypeID, [NotNull] Type dbRelationType, bool overwrite = false)
  {
    DefaultDBEntityCollectionCreator.RegisterRelationType(MetaDataHelperService.Instance.GetRelationTypeGuid(relationTypeID), dbRelationType, overwrite);
  }

  public static void RegisterRelationType(
    [NotEmpty] Guid relationTypeGuid,
    [NotNull] Type dbRelationType,
    bool overwrite = false)
  {
    ConstructorInfo constructorInfo = ((IEnumerable<ConstructorInfo>) dbRelationType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).FirstOrDefault<ConstructorInfo>((Func<ConstructorInfo, bool>) (constructor => ((IEnumerable<ParameterInfo>) constructor.GetParameters()).Select<ParameterInfo, Type>((Func<ParameterInfo, Type>) (paramInfo => paramInfo.ParameterType)).SequenceEqual<Type>((IEnumerable<Type>) DefaultDBEntityCollectionCreator.DefaultObjectConstructorParamTypes)));
    Intermech.Diagnostics.Check.NotNull<ConstructorInfo>(constructorInfo, $"DBRelationCollection type {dbRelationType} must have constructor with parameters ({typeof (UserSession)}, {typeof (int)})");
    if (overwrite)
      DefaultDBEntityCollectionCreator.Instance._knownTypes.AddOrUpdate(relationTypeGuid, constructorInfo, (Func<Guid, ConstructorInfo, ConstructorInfo>) ((_, __) => constructorInfo));
    else if (!DefaultDBEntityCollectionCreator.Instance._knownTypes.TryAdd(relationTypeGuid, constructorInfo))
      throw new Exception($"Relation type guid {relationTypeGuid} already registered!");
  }

  [NotNull]
  public static DefaultDBEntityCollectionCreator Instance
  {
    get
    {
      if (DefaultDBEntityCollectionCreator._instance == null)
      {
        lock (DefaultDBEntityCollectionCreator._syncObject)
        {
          if (DefaultDBEntityCollectionCreator._instance == null)
          {
            DefaultDBEntityCollectionCreator._instance = new DefaultDBEntityCollectionCreator();
            Thread.MemoryBarrier();
          }
        }
      }
      return DefaultDBEntityCollectionCreator._instance;
    }
  }

  [NotNull]
  public IDBObjectCollection CreateObjectCollection(
    [NotNull] IUserSession uSession,
    Guid guid,
    [NotEmpty] int objectTypeID)
  {
    ConstructorInfo constructorInfo;
    if (!this._knownTypes.TryGetValue(guid, out constructorInfo))
      throw new KeyNotFoundException($"Object collection type with guid {guid} not registered in this creator");
    return constructorInfo.Invoke(new object[2]
    {
      (object) (UserSession) uSession,
      (object) objectTypeID
    }).CastToInterface<IDBObjectCollection>();
  }

  [NotNull]
  public IDBRelationCollection CreateRelationCollection(
    [NotNull] IUserSession uSession,
    Guid guid,
    [NotEmpty] int relationTypeID)
  {
    ConstructorInfo constructorInfo;
    if (!this._knownTypes.TryGetValue(guid, out constructorInfo))
      throw new KeyNotFoundException($"Relation collection type with guid {guid} not registered in this creator");
    return constructorInfo.Invoke(new object[2]
    {
      (object) (UserSession) uSession,
      (object) relationTypeID
    }).CastToInterface<IDBRelationCollection>();
  }
}
