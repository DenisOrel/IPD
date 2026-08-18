// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.IDBTypedObjectIDExtensions
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.DataFormats;

public static class IDBTypedObjectIDExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Is([NotNull] this IDBTypedObjectID typedObjectID, [NotEmpty] int objectTypeID)
  {
    if (typedObjectID.ObjectType == objectTypeID)
      return true;
    List<int> childrenIdRecursive = MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(typedObjectID.ObjectType);
    return childrenIdRecursive.Count > 1 && childrenIdRecursive.Contains(objectTypeID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Is(
    [NotNull] this IDBTypedObjectID typedObjectID,
    [NotEmpty] Guid objectTypeGuid,
    bool throwExceptionIfTypeNotFound = true)
  {
    int objectTypeId = MetaDataHelperService.Instance.GetObjectTypeID(objectTypeGuid);
    if (objectTypeId != -1)
      return MetaDataHelperService.Instance.GetObjectTypeChildrenIDRecursive(typedObjectID.ObjectType).Contains(objectTypeId);
    if (throwExceptionIfTypeNotFound)
      throw new ObjectTypeNotFoundException(objectTypeGuid);
    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Is([NotNull] this IDBTypedObjectID typedObjectID, [NotNull] SystemObjectType systemObjectType)
  {
    return systemObjectType.IsTypeOrChild(typedObjectID.ObjectType);
  }
}
