// Decompiled with JetBrains decompiler
// Type: Intermech.Document.DBCore.IdExtensionMethods
// Assembly: Intermech.Document.DBCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CF4D99-832B-4258-9FE1-B244E517D790
// Assembly location: D:\IPS\Client\Intermech.Document.DBCore.dll

using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Document.DBCore;

public static class IdExtensionMethods
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsUndefinedId(this long id) => !id.IsDefinedId();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsDefinedId(this long id) => id != 0L && id != -1L;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsUndefinedTypeId(this int typeId) => typeId == -1;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsDefinedTypeId(this int typeId) => !typeId.IsUndefinedTypeId();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsEmpty(this Guid? id) => !id.HasValue || id.Value == Guid.Empty;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long ConvertToInt64(this AttributeValues dbAttribute, long defaultFalue = -1)
  {
    return dbAttribute == null || dbAttribute.Value == null || dbAttribute.Value is DBNull ? defaultFalue : dbAttribute.AsInteger;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long ConvertToInt64(this IDBAttribute dbAttribute, long defaultFalue = -1)
  {
    return dbAttribute == null || dbAttribute.Value == null || dbAttribute.Value is DBNull ? defaultFalue : dbAttribute.AsInteger;
  }
}
