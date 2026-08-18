// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.IdbAttributableExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Interfaces;

public static class IdbAttributableExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static AttributeNotFoundException CreateAttributeNotFoundException(
    [NotNull] this IDBAttributable dbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    switch (dbAttributable)
    {
      case IDBObject dbObject:
        return (AttributeNotFoundException) new ObjectAttributeNotFoundException(attributeID, dbObject.ObjectID, exceptionMessage);
      case IDBRelation dbRelation:
        return (AttributeNotFoundException) new RelationAttributeNotFoundException(attributeID, dbRelation.RelationID, exceptionMessage);
      default:
        return (AttributeNotFoundException) new CustomAttributeNotFoundException(attributeID, 0L, exceptionMessage);
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static AttributeNotFoundException CreateAttributeNotFoundException(
    [NotNull] this IDBAttributable dbAttributable,
    [NotEmpty] Guid attributeGuid,
    [CanBeNull] string exceptionMessage = null)
  {
    switch (dbAttributable)
    {
      case IDBObject dbObject:
        return (AttributeNotFoundException) new ObjectAttributeNotFoundException(attributeGuid, dbObject.ObjectID);
      case IDBRelation dbRelation:
        return (AttributeNotFoundException) new RelationAttributeNotFoundException(attributeGuid, dbRelation.RelationID);
      default:
        return (AttributeNotFoundException) new CustomAttributeNotFoundException(MetaDataHelperService.Instance.GetAttributeTypeID(attributeGuid), 0L, exceptionMessage);
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static AttributeValueIsEmptyException CreateAttributeValueIsEmptyException(
    [NotNull] this IDBAttributable dbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string message = null)
  {
    switch (dbAttributable)
    {
      case IDBObject dbObject:
        return (AttributeValueIsEmptyException) new ObjectAttributeValueIsEmptyException(attributeID, dbObject.ObjectID, message);
      case IDBRelation dbRelation:
        return (AttributeValueIsEmptyException) new RelationAttributeValueIsEmptyException(attributeID, dbRelation.RelationID, message);
      default:
        return new AttributeValueIsEmptyException(attributeID, 0L, message);
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static AttributeValueIsEmptyException CreateAttributeValueIsEmptyException(
    [NotNull] this IDBAttributable dbAttributable,
    [CanBeNull, CanBeEmpty, InvokerParameterName] string attributeName,
    [NotEmpty] int attributeID,
    [CanBeNull] string message = null)
  {
    switch (dbAttributable)
    {
      case IDBObject dbObject:
        return (AttributeValueIsEmptyException) new ObjectAttributeValueIsEmptyException(attributeName, attributeID, dbObject.ObjectID, message);
      case IDBRelation dbRelation:
        return (AttributeValueIsEmptyException) new RelationAttributeValueIsEmptyException(attributeName, attributeID, dbRelation.RelationID, message);
      default:
        return new AttributeValueIsEmptyException(attributeName, attributeID, 0L, message);
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ArgumentAttributeValueIsEmptyException CreateArgumentAttributeValueIsEmptyException(
    [NotNull] this IDBAttributable dbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string message = null)
  {
    switch (dbAttributable)
    {
      case IDBObject dbObject:
        return (ArgumentAttributeValueIsEmptyException) new ArgumentObjectAttributeValueIsEmptyException(attributeID, dbObject.ObjectID, message);
      case IDBRelation dbRelation:
        return (ArgumentAttributeValueIsEmptyException) new ArgumentRelationAttributeValueIsEmptyException(attributeID, dbRelation.RelationID, message);
      default:
        return new ArgumentAttributeValueIsEmptyException(attributeID, 0L, message);
    }
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ArgumentAttributeValueIsEmptyException CreateArgumentAttributeValueIsEmptyException(
    [NotNull] this IDBAttributable dbAttributable,
    [CanBeNull, CanBeEmpty, InvokerParameterName] string attributeName,
    [NotEmpty] int attributeID,
    [CanBeNull] string message = null)
  {
    switch (dbAttributable)
    {
      case IDBObject dbObject:
        return (ArgumentAttributeValueIsEmptyException) new ArgumentObjectAttributeValueIsEmptyException(attributeName, attributeID, dbObject.ObjectID, message);
      case IDBRelation dbRelation:
        return (ArgumentAttributeValueIsEmptyException) new ArgumentRelationAttributeValueIsEmptyException(attributeName, attributeID, dbRelation.RelationID, message);
      default:
        return new ArgumentAttributeValueIsEmptyException(attributeName, attributeID, 0L, message);
    }
  }
}
