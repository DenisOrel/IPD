// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributableExtensions
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Kernel;

public static class DBAttributableExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObjectInterface GetAttrServerObjLinkOrNull<TDBObjectInterface>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID)
    where TDBObjectInterface : DBObject
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
      return default (TDBObjectInterface);
    long asInteger = attributeById.AsInteger;
    return asInteger == 0L ? default (TDBObjectInterface) : iDbAttributable.Session.GetServerObject<TDBObjectInterface>(asInteger);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DBObject GetAttrServerObjLinkOrNull(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
      return (DBObject) null;
    long asInteger = attributeById.AsInteger;
    return asInteger == 0L ? (DBObject) null : iDbAttributable.Session.GetObject(asInteger).CastInterfaceToClass<IDBObject, DBObject>();
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrServerObjLink<TDBObjectInterface>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out TDBObjectInterface result)
    where TDBObjectInterface : DBObject
  {
    result = iDbAttributable.GetAttrObjLinkOrNull<TDBObjectInterface>(attributeID);
    return (object) result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrServerObjLink(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out DBObject result)
  {
    ref DBObject local = ref result;
    IDBObject attrObjLinkOrNull = iDbAttributable.GetAttrObjLinkOrNull(attributeID);
    DBObject dbObject = attrObjLinkOrNull != null ? attrObjLinkOrNull.CastInterfaceToClass<IDBObject, DBObject>() : (DBObject) null;
    local = dbObject;
    return result != null;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObjectInterface GetAttrSureServerObjLink<TDBObjectInterface>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
    where TDBObjectInterface : DBObject
  {
    long asInteger = iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsInteger;
    if (asInteger == 0L)
      throw iDbAttributable.CreateArgumentAttributeValueIsEmptyException(attributeID, exceptionMessage);
    return iDbAttributable.Session.GetServerObject<TDBObjectInterface>(asInteger);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DBObject GetAttrSureServerObjLink(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    long asInteger = iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsInteger;
    if (asInteger == 0L)
      throw iDbAttributable.CreateArgumentAttributeValueIsEmptyException(attributeID, exceptionMessage);
    return iDbAttributable.Session.GetObject(asInteger).CastInterfaceToClass<IDBObject, DBObject>();
  }
}
