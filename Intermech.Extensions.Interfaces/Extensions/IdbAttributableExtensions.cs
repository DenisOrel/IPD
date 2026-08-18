// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IdbAttributableExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IdbAttributableExtensions
{
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetIdOrUnknown([NotNull] this IDBAttributable iDbAttributable)
  {
    switch (iDbAttributable)
    {
      case IDBObject dbObject:
        return dbObject.ObjectID;
      case IDBRelation dbRelation:
        return dbRelation.RelationID;
      default:
        return 0;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long UpdateLinkToObjectWithCheck(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeEmpty] long objectVersionID,
    bool deleteAttributeIfNoTargetObject = true)
  {
    if (objectVersionID != 0L)
    {
      if (iDbAttributable.Session.GetObjectInfo(objectVersionID).Empty)
        objectVersionID = 0L;
      else
        iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
        {
          (object) objectVersionID
        });
    }
    if (deleteAttributeIfNoTargetObject && objectVersionID == 0L)
      iDbAttributable.Attributes.FindByID(attributeID)?.Delete(0L);
    return objectVersionID;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Guid GetAttrGuidValueOrDefault(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    Guid defaultValue = default (Guid))
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
      return defaultValue;
    switch (attributeById.DataType)
    {
      case FieldTypes.ftString:
      case FieldTypes.ftGuid:
        string asString = attributeById.AsString;
        return string.IsNullOrWhiteSpace(asString) ? defaultValue : Guid.Parse(asString);
      default:
        throw new OperationNotApplicableException();
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Guid? GetAttrGuidValueOrNull([NotNull] this IDBAttributable iDbAttributable, [NotEmpty] int attributeID)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
      return new Guid?();
    switch (attributeById.DataType)
    {
      case FieldTypes.ftString:
      case FieldTypes.ftGuid:
        string asString = attributeById.AsString;
        return string.IsNullOrWhiteSpace(asString) ? new Guid?() : new Guid?(Guid.Parse(asString));
      default:
        throw new OperationNotApplicableException();
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrGuidValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out Guid result)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
    {
      result = Guid.Empty;
      return false;
    }
    switch (attributeById.DataType)
    {
      case FieldTypes.ftString:
      case FieldTypes.ftGuid:
        if (Guid.TryParse(attributeById.AsString, out result))
          return true;
        result = Guid.Empty;
        return false;
      default:
        throw new OperationNotApplicableException();
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Guid GetAttrSureGuidValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    string asString = iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsString;
    return !string.IsNullOrWhiteSpace(asString) ? Guid.Parse(asString) : throw iDbAttributable.CreateArgumentAttributeValueIsEmptyException(attributeID, exceptionMessage);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrGuidValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    Guid newValue,
    bool autoAddAttrIfNotFound = true,
    bool autoDelAttrIfEmpty = false)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      if (newValue != Guid.Empty || !autoDelAttrIfEmpty)
      {
        IUserSession session = iDbAttributable.Session;
        iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
        {
          (object) newValue
        });
      }
    }
    else if (newValue == Guid.Empty)
    {
      if (autoDelAttrIfEmpty)
        byId.Delete(0L);
      else
        byId.Clear();
    }
    else
      byId.AsString = newValue.ToString();
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrNullableGuidValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    Guid? newValue,
    bool autoAddAttrIfNotFound = true)
  {
    int num = newValue.HasValue ? 1 : 0;
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId != null)
    {
      if (newValue.HasValue)
        byId.AsString = newValue.ToString();
      else
        byId.Delete(0L);
    }
    else if (newValue.HasValue)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      IUserSession session = iDbAttributable.Session;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue.Value
      });
    }
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetAttrIntValueOrDefault(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    long defaultValue = 0)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? defaultValue : attributeById.AsInteger;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long? GetAttrIntValueOrNull([NotNull] this IDBAttributable iDbAttributable, [NotEmpty] int attributeID)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? new long?() : new long?(attributeById.AsInteger);
  }

  [Obsolete("[Переименование] Используйте метод GetAttrIntValueOrDefault")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetAttrIntValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    long defaultValue = 0)
  {
    return iDbAttributable.GetAttrIntValueOrDefault(attributeID, defaultValue);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrIntValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out long result)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
    {
      result = 0L;
      return false;
    }
    result = attributeById.AsInteger;
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetAttrSureIntValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    return iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsInteger;
  }

  [Obsolete("[Переименование] Используйте метод GetAttrSureIntValue")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetIntValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    return iDbAttributable.GetAttrSureIntValue(attributeID, exceptionMessage);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrIntValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    long newValue,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue
      });
    }
    else
      byId.AsInteger = newValue;
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrNullableIntValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    long? newValue,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId != null)
    {
      if (newValue.HasValue)
        byId.AsInteger = newValue.Value;
      else
        byId.Delete(0L);
    }
    else if (newValue.HasValue)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue.Value
      });
    }
    return true;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static MeasuredValue GetAttrMeasuredValueOrDefault(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] MeasureDescriptor defaultMeasure,
    [CanBeNull] MeasuredValue defaultValue = null)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? defaultValue : attributeById.GetAsMeasuredValueOrDefault(defaultMeasure);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrMeasuredValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] MeasureDescriptor defaultMeasure,
    out MeasuredValue result)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
    {
      result = (MeasuredValue) null;
      return false;
    }
    result = attributeById.GetAsMeasuredValueOrDefault(defaultMeasure);
    return result != null;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static MeasuredValue GetAttrSureMeasuredValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] MeasureDescriptor defaultMeasure,
    [CanBeNull] string exceptionMessage = null)
  {
    return iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).GetAsMeasuredValueOrDefault(defaultMeasure) ?? throw iDbAttributable.CreateArgumentAttributeValueIsEmptyException(attributeID, exceptionMessage);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrMeasuredValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] MeasuredValue newValue,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId != null)
    {
      if (newValue != null)
        byId.SetAsMeasuredValue(newValue);
      else
        byId.Delete(0L);
    }
    else if (newValue != null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue.Value
      });
    }
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TEnum GetAttrEnumValueOrDefault<TEnum>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    TEnum defaultValue = default (TEnum))
    where TEnum : struct, Enum
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
      return defaultValue;
    object obj = attributeById.Value;
    if (obj == null)
      return defaultValue;
    TEnum enumValueOrDefault = (TEnum) obj;
    Intermech.Diagnostics.Check.EnumInRange<TEnum>(enumValueOrDefault);
    return enumValueOrDefault;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TEnum? GetAttrEnumValueOrNull<TEnum>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID)
    where TEnum : struct, Enum
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
      return new TEnum?();
    object obj = attributeById.Value;
    if (obj == null)
      return new TEnum?();
    TEnum @enum = (TEnum) obj;
    Intermech.Diagnostics.Check.EnumInRange<TEnum>(@enum);
    return new TEnum?(@enum);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrEnumValue<TEnum>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out TEnum result)
    where TEnum : struct, Enum
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if ((attributeById != null ? (attributeById.IsNull ? 1 : 0) : 1) != 0)
    {
      result = default (TEnum);
      return false;
    }
    object obj = attributeById.Value;
    if (obj == null)
    {
      result = default (TEnum);
      return false;
    }
    result = (TEnum) obj;
    Intermech.Diagnostics.Check.EnumInRange<TEnum>(result);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TEnum GetAttrSureEnumValue<TEnum>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
    where TEnum : struct, Enum
  {
    TEnum attrSureEnumValue = (TEnum) (iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).Value ?? throw iDbAttributable.CreateAttributeValueIsEmptyException(attributeID, exceptionMessage));
    Intermech.Diagnostics.Check.EnumInRange<TEnum>(attrSureEnumValue);
    return attrSureEnumValue;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrEnumValue<TEnum>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    TEnum newValue,
    bool autoAddAttrIfNotFound = true)
    where TEnum : struct, Enum
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) Convert.ToInt64((object) newValue)
      });
    }
    else
      byId.AsInteger = Convert.ToInt64((object) newValue);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrNullableEnumValue<TEnum>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    TEnum? newValue,
    bool autoAddAttrIfNotFound = true)
    where TEnum : struct, Enum
  {
    int num = newValue.HasValue ? 1 : 0;
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId != null)
    {
      if (newValue.HasValue)
        byId.AsInteger = Convert.ToInt64((object) newValue.Value);
      else
        byId.Delete(0L);
    }
    else if (newValue.HasValue)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) Convert.ToInt64((object) newValue.Value)
      });
    }
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetAttrObjLinkValueOrDefault(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    long defaultValue = 0)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? defaultValue : attributeById.AsInteger;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long? GetAttrObjLinkValueOrNull(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? new long?() : new long?(attributeById.AsInteger);
  }

  [Obsolete("[Переименование] Используйте метод GetAttrObjLinkValueOrDefault")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetAttrObjLinkValue([NotNull] this IDBAttributable iDbAttributable, [NotEmpty] int attributeID)
  {
    return iDbAttributable.GetAttrObjLinkValueOrDefault(attributeID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrObjLinkValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out long result)
  {
    result = iDbAttributable.GetAttrObjLinkValueOrDefault(attributeID);
    return result != 0L;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetAttrSureObjLinkValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    return iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsInteger;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrObjLinkValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    long newValue,
    bool autoAddAttrIfNotFound = true,
    bool autoDelAttrIfEmpty = false)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      if (newValue != 0L || !autoDelAttrIfEmpty)
      {
        if (iDbAttributable.Session.GetObjectInfo(newValue).Empty)
          return false;
        iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
        {
          (object) newValue
        });
      }
    }
    else if (newValue == 0L)
    {
      if (autoDelAttrIfEmpty)
        byId.Delete(0L);
      else
        byId.Clear();
    }
    else if (byId.AsInteger != newValue)
    {
      if (iDbAttributable.Session.GetObjectInfo(newValue).Empty)
        return false;
      byId.AsInteger = newValue;
    }
    return true;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObjectInterface GetAttrObjLinkOrNull<TDBObjectInterface>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID)
    where TDBObjectInterface : class, IDBObject
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
      return default (TDBObjectInterface);
    long asInteger = attributeById.AsInteger;
    return asInteger == 0L ? default (TDBObjectInterface) : iDbAttributable.Session.GetObject<TDBObjectInterface>(asInteger);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBObject GetAttrObjLinkOrNull(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
      return (IDBObject) null;
    long asInteger = attributeById.AsInteger;
    return asInteger == 0L ? (IDBObject) null : iDbAttributable.Session.GetObject(asInteger);
  }

  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrObjLinkId(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeEmpty] out long result,
    bool returnFalseIfUnknownObjectId = true)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
    {
      result = 0L;
      return false;
    }
    result = attributeById.AsInteger;
    if (Intermech.Check.ObjectIdIsEmpty(result))
      result = 0L;
    else if (iDbAttributable.Session.GetObjectInfo(result).Empty)
      result = 0L;
    return !(result == 0L & returnFalseIfUnknownObjectId);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrObjLink<TDBObjectInterface>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out TDBObjectInterface result)
    where TDBObjectInterface : class, IDBObject
  {
    result = iDbAttributable.GetAttrObjLinkOrNull<TDBObjectInterface>(attributeID);
    return (object) result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrObjLink(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out IDBObject result)
  {
    result = iDbAttributable.GetAttrObjLinkOrNull(attributeID);
    return result != null;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObjectInterface GetAttrSureObjLink<TDBObjectInterface>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
    where TDBObjectInterface : class, IDBObject
  {
    long asInteger = iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsInteger;
    if (asInteger == 0L)
      throw iDbAttributable.CreateArgumentAttributeValueIsEmptyException(attributeID, exceptionMessage);
    return iDbAttributable.Session.GetObject<TDBObjectInterface>(asInteger);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBObject GetAttrSureObjLink(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    long asInteger = iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsInteger;
    if (asInteger == 0L)
      throw iDbAttributable.CreateArgumentAttributeValueIsEmptyException(attributeID, exceptionMessage);
    return iDbAttributable.Session.GetObject(asInteger);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrObjLink<TDBObjectInterface>(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] TDBObjectInterface newValue,
    bool autoAddAttrIfNotFound = true,
    bool autoDelAttrIfEmpty = false)
    where TDBObjectInterface : class, IDBObject
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      if ((object) newValue != null || !autoDelAttrIfEmpty)
      {
        IDBAttributeCollection attributes = iDbAttributable.Attributes;
        int attributeID1 = attributeID;
        object[] initValues = new object[1];
        // ISSUE: variable of a boxed type
        __Boxed<TDBObjectInterface> local = (object) newValue;
        initValues[0] = (object) (local != null ? local.ObjectID : 0L);
        attributes.AddAttribute(attributeID1, false, initValues);
      }
    }
    else if ((object) newValue == null)
    {
      if (autoDelAttrIfEmpty)
        byId.Delete(0L);
      else
        byId.Clear();
    }
    else
      byId.Value = (object) newValue.ObjectID;
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool GetAttrBoolValueOrDefault(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    bool defaultValue = false)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? defaultValue : attributeById.AsBoolean;
  }

  [Obsolete("[Переименование] Используйте метод GetAttrBoolValueOrDefault")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool GetAttrBoolValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    bool defaultValue = false)
  {
    return iDbAttributable.GetAttrBoolValueOrDefault(attributeID, defaultValue);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrBoolValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out bool result)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.IsNull)
    {
      result = false;
      return false;
    }
    result = attributeById.AsBoolean;
    return true;
  }

  [Obsolete("[Переименование] Используйте метод TryGetAttrBoolValue")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool HasAttrBoolValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out bool result,
    bool defaultValue = false)
  {
    if (iDbAttributable.TryGetAttrBoolValue(attributeID, out result))
      return true;
    result = defaultValue;
    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool GetAttrSureBoolValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    return iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsBoolean;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrBoolValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    bool newValue,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue
      });
    }
    else
      byId.AsBoolean = newValue;
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime GetAttrDateTimeValueOrDefault(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    DateTime defaultValue)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? defaultValue : attributeById.AsDateTime;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime? GetAttrDateTimeValueOrNull(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? new DateTime?() : new DateTime?(attributeById.AsDateTime);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime GetAttrDateTimeValueOrDefault(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID)
  {
    return iDbAttributable.GetAttrDateTimeValueOrDefault(attributeID, DateTime.MinValue);
  }

  [Obsolete("[Переименование] Используйте метод GetAttrBoolValueOrDefault")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime GetAttrDateTimeValue([NotNull] this IDBAttributable iDbAttributable, [NotEmpty] int attributeID)
  {
    return iDbAttributable.GetAttrDateTimeValueOrDefault(attributeID, DateTime.MinValue);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrDateTimeValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out DateTime result)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if ((attributeById != null ? (attributeById.IsNull ? 1 : 0) : 1) != 0)
    {
      result = DateTime.MinValue;
      return false;
    }
    result = attributeById.AsDateTime;
    return true;
  }

  [Obsolete("[Переименование] Используйте метод TryGetAttrDateTimeValue")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool HasAttrDateTimeValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out DateTime result)
  {
    return iDbAttributable.TryGetAttrDateTimeValue(attributeID, out result);
  }

  [Obsolete("[Переименование] Используйте метод TryGetAttrDateTimeValue")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool HasAttrDateTimeValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out DateTime result,
    DateTime defaultValue)
  {
    if (iDbAttributable.TryGetAttrDateTimeValue(attributeID, out result))
      return true;
    result = defaultValue;
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime GetAttrSureDateTimeValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    return iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsDateTime;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrDateTimeValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    DateTime newValue,
    bool autoAddAttrIfNotFound = true,
    bool autoDelAttrIfEmpty = false)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound || autoDelAttrIfEmpty && !(newValue != DateTime.MinValue))
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue
      });
    }
    else if (newValue != DateTime.MinValue || !autoDelAttrIfEmpty)
      byId.AsDateTime = newValue;
    else
      byId.Delete(0L);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrNullableDateTimeValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    DateTime? newValue,
    bool autoAddAttrIfNotFound = true)
  {
    int num = newValue.HasValue ? 1 : 0;
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId != null)
    {
      if (newValue.HasValue)
        byId.AsDateTime = newValue.Value;
      else
        byId.Delete(0L);
    }
    else if (newValue.HasValue)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue.Value
      });
    }
    return true;
  }

  [NotNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttrStrValueOrEmpty([NotNull] this IDBAttributable iDbAttributable, [NotEmpty] int attributeID)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? string.Empty : attributeById.AsString ?? string.Empty;
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttrStrValueOrDefault(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string defaultValue)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? defaultValue : attributeById.AsString;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrStrValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out string result)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if ((attributeById != null ? (attributeById.IsNull ? 1 : 0) : 1) != 0)
    {
      result = string.Empty;
      return false;
    }
    result = attributeById.AsString ?? string.Empty;
    return true;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttrSureStrValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    bool checkNonEmpty = false,
    [CanBeNull] string exceptionMessage = null)
  {
    string asString = iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsString;
    if (checkNonEmpty && string.IsNullOrEmpty(asString))
      throw iDbAttributable.CreateAttributeValueIsEmptyException(attributeID, exceptionMessage);
    return asString ?? string.Empty;
  }

  [Obsolete("[Переименование] Используйте метод GetAttrSureStrValue с теми же самыми параметрами")]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetStrValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    bool checkNonEmpty = false,
    [CanBeNull] string exceptionMessage = null)
  {
    return iDbAttributable.GetAttrSureStrValue(attributeID, checkNonEmpty, exceptionMessage);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrStrValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string newValue,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue
      });
    }
    else
      byId.AsString = newValue;
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double GetAttrDoubleValueOrDefault(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    double defaultValue = double.NaN)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    return attributeById == null || attributeById.IsNull ? defaultValue : attributeById.AsDouble;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrDoubleValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    out double result)
  {
    IDBAttribute attributeById = iDbAttributable.GetAttributeByID(attributeID);
    if ((attributeById != null ? (attributeById.IsNull ? 1 : 0) : 1) != 0)
    {
      result = double.NaN;
      return false;
    }
    result = attributeById.AsDouble;
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double GetAttrSureDoubleValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    return iDbAttributable.GetAttributeAndCheckNotEmpty(attributeID, exceptionMessage).AsDouble;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrDoubleValue(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    double newValue,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue
      });
    }
    else
      byId.AsDouble = newValue;
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrMultiIntValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IEnumerable<long> newValues,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, (newValues != null ? newValues.AsArrayOf<object>() : (object[]) null) ?? Array.Empty<object>());
    }
    else if ((newValues != null ? (!newValues.Any<long>() ? 1 : 0) : 1) != 0)
      byId.ClearValues();
    else
      byId.Values = newValues.AsArrayOf<object>();
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AddValueToMultiIntAttr(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    long newValue,
    bool autoAddAttrIfNotFound = true,
    bool dontAddIfAlreadyInList = false)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue
      });
    }
    else
    {
      if (dontAddIfAlreadyInList && !byId.IsNull && byId.ValuesCount > 0)
      {
        object[] values = byId.Values;
        if ((values != null ? (values.ConvertAll2Long().Contains<long>((Predicate<long>) (val => val == newValue)) ? 1 : 0) : 0) != 0)
          return false;
      }
      byId.AddValue((object) newValue);
    }
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrMultiObjLinkValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IEnumerable<long> newValues,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, (newValues != null ? newValues.AsArrayOf<object>() : (object[]) null) ?? Array.Empty<object>());
    }
    else if ((newValues != null ? (!newValues.Any<long>() ? 1 : 0) : 1) != 0)
      byId.ClearValues();
    else
      byId.Values = newValues.AsArrayOf<object>();
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AddValueToMultiObjLinkAttr(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    long newValue,
    bool autoAddAttrIfNotFound = true,
    bool dontAddIfAlreadyInList = false)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      if (newValue == 0L)
      {
        iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
        {
          (object) newValue
        });
      }
      else
      {
        if (iDbAttributable.Session.GetObjectInfo(newValue).Empty)
          return false;
        iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
        {
          (object) newValue
        });
      }
    }
    else
    {
      if (dontAddIfAlreadyInList && !byId.IsNull && byId.ValuesCount > 0)
      {
        object[] values = byId.Values;
        if ((values != null ? (values.ConvertAll2Long().Contains<long>((Predicate<long>) (val => val == newValue)) ? 1 : 0) : 0) != 0)
          return false;
      }
      if (newValue == 0L)
      {
        byId.AddValue((object) newValue);
      }
      else
      {
        if (iDbAttributable.Session.GetObjectInfo(newValue).Empty)
          return false;
        byId.AddValue((object) newValue);
      }
    }
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrMultiStrValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IEnumerable<string> newValues,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, (newValues != null ? newValues.AsArrayOf<object>() : (object[]) null) ?? Array.Empty<object>());
    }
    else if ((newValues != null ? (!newValues.Any<string>() ? 1 : 0) : 1) != 0)
      byId.ClearValues();
    else
      byId.Values = newValues.AsArrayOf<object>();
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AddValueToMultiStrAttr(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] string newValue,
    bool autoAddAttrIfNotFound = true,
    bool dontAddIfAlreadyInList = false)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue
      });
    }
    else
    {
      if (dontAddIfAlreadyInList && !byId.IsNull && byId.ValuesCount > 0)
      {
        object[] values = byId.Values;
        if ((values != null ? (values.ConvertAll2String().Contains<string>((Predicate<string>) (val => val == newValue)) ? 1 : 0) : 0) != 0)
          return false;
      }
      byId.AddValue((object) newValue);
    }
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrMultiBoolValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IEnumerable<bool> newValues,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, (newValues != null ? newValues.AsArrayOf<object>() : (object[]) null) ?? Array.Empty<object>());
    }
    else if ((newValues != null ? (!newValues.Any<bool>() ? 1 : 0) : 1) != 0)
      byId.ClearValues();
    else
      byId.Values = newValues.AsArrayOf<object>();
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AddValueToMultiBoolAttr(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    bool newValue,
    bool autoAddAttrIfNotFound = true,
    bool dontAddIfAlreadyInList = false)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue
      });
    }
    else
    {
      if (dontAddIfAlreadyInList && !byId.IsNull && byId.ValuesCount > 0)
      {
        object[] values = byId.Values;
        if ((values != null ? (values.ConvertAll2Bool().Contains<bool>((Predicate<bool>) (val => val == newValue)) ? 1 : 0) : 0) != 0)
          return false;
      }
      byId.AddValue((object) newValue);
    }
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrMultiDoubleValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IEnumerable<double> newValues,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, (newValues != null ? newValues.AsArrayOf<object>() : (object[]) null) ?? Array.Empty<object>());
    }
    else if ((newValues != null ? (!newValues.Any<double>() ? 1 : 0) : 1) != 0)
      byId.ClearValues();
    else
      byId.Values = newValues.AsArrayOf<object>();
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AddValueToMultiDoubleAttr(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    double newValue,
    bool autoAddAttrIfNotFound = true,
    bool dontAddIfAlreadyInList = false)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue
      });
    }
    else
    {
      if (dontAddIfAlreadyInList && !byId.IsNull && byId.ValuesCount > 0)
      {
        object[] values = byId.Values;
        if ((values != null ? (values.ConvertAll2Double().Contains<double>((Predicate<double>) (val => val == newValue)) ? 1 : 0) : 0) != 0)
          return false;
      }
      byId.AddValue((object) newValue);
    }
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SetAttrMultiIntValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IEnumerable<DateTime> newValues,
    bool autoAddAttrIfNotFound = true)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, (newValues != null ? newValues.AsArrayOf<object>() : (object[]) null) ?? Array.Empty<object>());
    }
    else if ((newValues != null ? (!newValues.Any<DateTime>() ? 1 : 0) : 1) != 0)
      byId.ClearValues();
    else
      byId.Values = newValues.AsArrayOf<object>();
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AddValueToMultiDateTimeAttr(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    DateTime newValue,
    bool autoAddAttrIfNotFound = true,
    bool dontAddIfAlreadyInList = false)
  {
    IDBAttribute byId = iDbAttributable.Attributes.FindByID(attributeID);
    if (byId == null)
    {
      if (!autoAddAttrIfNotFound)
        return false;
      iDbAttributable.Attributes.AddAttribute(attributeID, false, new object[1]
      {
        (object) newValue
      });
    }
    else
    {
      if (dontAddIfAlreadyInList && !byId.IsNull && byId.ValuesCount > 0)
      {
        object[] values = byId.Values;
        if ((values != null ? (values.ConvertAll2DateTime().Contains<DateTime>((Predicate<DateTime>) (val => val == newValue)) ? 1 : 0) : 0) != 0)
          return false;
      }
      byId.AddValue((object) newValue);
    }
    return true;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<long> GetAttrIntValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleIntValues(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrIntValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out IReadOnlyList<long> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleIntValues(out result, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<long> GetAttrSureIntValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    IReadOnlyList<long> result;
    if (iDbAttributable.TryGetAttrIntValues(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<long> GetAttrSureIntValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    IReadOnlyList<long> result;
    if (iDbAttributable.TryGetAttrIntValues(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<long> GetAttrObjLinkValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleObjLinkValues(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrObjLinkValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out IReadOnlyList<long> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleObjLinkValues(out result, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<long> GetAttrSureObjLinkValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    IReadOnlyList<long> result;
    if (iDbAttributable.TryGetAttrObjLinkValues(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<long> GetAttrSureObjLinkValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    IReadOnlyList<long> result;
    if (iDbAttributable.TryGetAttrObjLinkValues(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<string> GetAttrStrValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleStrValues(formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<string> GetAttrNotEmptyStrValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleNotEmptyStrValues(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrStrValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out IReadOnlyList<string> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleStrValues(out result, false, formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrNonEmptyStrValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out IReadOnlyList<string> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleStrValues(out result, true, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<string> GetAttrSureStrValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    bool checkNonEmpty = false,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    if (checkNonEmpty)
    {
      IReadOnlyList<string> result;
      if (iDbAttributable.TryGetAttrNonEmptyStrValues(attributeID, out result, formatProvider))
        return result;
      throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
    }
    IReadOnlyList<string> result1;
    if (iDbAttributable.TryGetAttrStrValues(attributeID, out result1, formatProvider))
      return result1;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<string> GetAttrSureStrValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    bool checkNonEmpty,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (checkNonEmpty)
    {
      IReadOnlyList<string> result;
      if (iDbAttributable.TryGetAttrNonEmptyStrValues(attributeID, out result, formatProvider))
        return result;
      throw new Exception(exceptionMessage);
    }
    IReadOnlyList<string> result1;
    if (iDbAttributable.TryGetAttrStrValues(attributeID, out result1, formatProvider))
      return result1;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<bool> GetAttrBoolValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleBoolValues(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrBoolValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out IReadOnlyList<bool> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleBoolValues(out result, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<bool> GetAttrSureBoolValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    IReadOnlyList<bool> result;
    if (iDbAttributable.TryGetAttrBoolValues(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<bool> GetAttrSureBoolValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    IReadOnlyList<bool> result;
    if (iDbAttributable.TryGetAttrBoolValues(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<double> GetAttrDoubleValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleDoubleValues(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrDoubleValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out IReadOnlyList<double> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleDoubleValues(out result, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<double> GetAttrSureDoubleValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    IReadOnlyList<double> result;
    if (iDbAttributable.TryGetAttrDoubleValues(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<double> GetAttrSureDoubleValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    IReadOnlyList<double> result;
    if (iDbAttributable.TryGetAttrDoubleValues(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<DateTime> GetAttrDateTimeValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleDateTimeValues(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrDateTimeValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out IReadOnlyList<DateTime> result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleDateTimeValues(out result, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<DateTime> GetAttrSureDateTimeValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    IReadOnlyList<DateTime> result;
    if (iDbAttributable.TryGetAttrDateTimeValues(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<DateTime> GetAttrSureDateTimeValues(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    IReadOnlyList<DateTime> result;
    if (iDbAttributable.TryGetAttrDateTimeValues(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long[] GetAttrIntValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleIntValuesArray(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrIntValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out long[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleIntValuesArray(out result, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long[] GetAttrSureIntValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    long[] result;
    if (iDbAttributable.TryGetAttrIntValuesArray(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long[] GetAttrSureIntValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    long[] result;
    if (iDbAttributable.TryGetAttrIntValuesArray(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long[] GetAttrObjLinkValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleObjLinkValuesArray(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrObjLinkValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out long[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleObjLinkValuesArray(out result, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long[] GetAttrSureObjLinkValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    long[] result;
    if (iDbAttributable.TryGetAttrObjLinkValuesArray(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long[] GetAttrSureObjLinkValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    long[] result;
    if (iDbAttributable.TryGetAttrObjLinkValuesArray(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [ItemNotNull]
  [ItemCanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string[] GetAttrStrValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleStrValuesArray(formatProvider);
  }

  [NotNull]
  [ItemNotNull]
  [ItemNotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string[] GetAttrNotEmptyStrValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleNotEmptyStrValuesArray(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrStrValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out string[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleStrValuesArray(out result, false, formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrNonEmptyStrValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out string[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleStrValuesArray(out result, true, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string[] GetAttrSureStrValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    bool checkNonEmpty = false,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    if (checkNonEmpty)
    {
      string[] result;
      if (iDbAttributable.TryGetAttrNonEmptyStrValuesArray(attributeID, out result, formatProvider))
        return result;
      throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
    }
    string[] result1;
    if (iDbAttributable.TryGetAttrStrValuesArray(attributeID, out result1, formatProvider))
      return result1;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string[] GetAttrSureStrValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    bool checkNonEmpty,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (checkNonEmpty)
    {
      string[] result;
      if (iDbAttributable.TryGetAttrNonEmptyStrValuesArray(attributeID, out result, formatProvider))
        return result;
      throw new Exception(exceptionMessage);
    }
    string[] result1;
    if (iDbAttributable.TryGetAttrStrValuesArray(attributeID, out result1, formatProvider))
      return result1;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool[] GetAttrBoolValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleBoolValuesArray(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrBoolValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out bool[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleBoolValuesArray(out result, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool[] GetAttrSureBoolValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    bool[] result;
    if (iDbAttributable.TryGetAttrBoolValuesArray(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool[] GetAttrSureBoolValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    bool[] result;
    if (iDbAttributable.TryGetAttrBoolValuesArray(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double[] GetAttrDoubleValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleDoubleValuesArray(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrDoubleValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out double[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleDoubleValuesArray(out result, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double[] GetAttrSureDoubleValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    double[] result;
    if (iDbAttributable.TryGetAttrDoubleValuesArray(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static double[] GetAttrSureDoubleValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    double[] result;
    if (iDbAttributable.TryGetAttrDoubleValuesArray(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime[] GetAttrDateTimeValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).GetMultipleDateTimeValuesArray(formatProvider);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAttrDateTimeValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull] out DateTime[] result,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID).TryGetAnyOfMultipleDateTimeValuesArray(out result, formatProvider);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime[] GetAttrSureDateTimeValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [CanBeNull] IFormatProvider formatProvider = null,
    [CanBeNull] string exceptionMessage = null)
  {
    DateTime[] result;
    if (iDbAttributable.TryGetAttrDateTimeValuesArray(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage ?? IdbAttributableExtensions.FormatMessageAttrNotFound(iDbAttributable, attributeID));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DateTime[] GetAttrSureDateTimeValuesArray(
    [NotNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    [NotNull, NotWhitespace] string exceptionMessage,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    DateTime[] result;
    if (iDbAttributable.TryGetAttrDateTimeValuesArray(attributeID, out result, formatProvider))
      return result;
    throw new Exception(exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static string FormatMessageAttrNotFound([NotNull] IDBAttributable iDbAttributable, [NotEmpty] int attributeID)
  {
    switch (iDbAttributable)
    {
      case IDBObject dbObject:
        return $"Attribute \"{MetaDataHelperService.Instance.GetAttributeTypeName(attributeID)}\" not found or empty in object \"{dbObject.Caption}\" with type \"{MetaDataHelperService.Instance.GetObjectTypeName(dbObject.TypeID)}\" and object version id = {dbObject.ObjectID}";
      case IDBRelation dbRelation:
        return $"Attribute \"{MetaDataHelperService.Instance.GetAttributeTypeName(attributeID)}\" not found or empty in relation with type \"{MetaDataHelperService.Instance.GetRelationTypeName(dbRelation.TypeID)}\" and relation id = {dbRelation.RelationID}";
      default:
        return $"Attribute \"{MetaDataHelperService.Instance.GetAttributeTypeName(attributeID)}\" not found or empty in IDBAttributable with unknown type \"{iDbAttributable.GetType().FullName}\"";
    }
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBAttributable DeleteAttribute(
    [CanBeNull] this IDBAttributable iDbAttributable,
    [NotEmpty] int attributeID,
    long deleteMode = 0)
  {
    iDbAttributable?.Attributes.FindByID(attributeID)?.Delete(deleteMode);
    return iDbAttributable;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBAttribute AttributeByID(
    [NotNull] this IDBAttributable iDbAttributable,
    int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    return iDbAttributable.GetAttributeByID(attributeID) ?? throw iDbAttributable.CreateAttributeNotFoundException(attributeID, exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBAttribute AttributeByGuid(
    [NotNull] this IDBAttributable iDbAttributable,
    Guid attributeGuid,
    [CanBeNull] string exceptionMessage = null)
  {
    return iDbAttributable.GetAttributeByGuid(attributeGuid) ?? throw iDbAttributable.CreateAttributeNotFoundException(attributeGuid, exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBAttribute GetAttributeAndCheckNotEmpty(
    [NotNull] this IDBAttributable iDbAttributable,
    int attributeID,
    [CanBeNull] string exceptionMessage = null)
  {
    IDBAttribute dbAttribute = iDbAttributable.AttributeByID(attributeID, exceptionMessage);
    return !dbAttribute.IsNull ? dbAttribute : throw iDbAttributable.CreateAttributeValueIsEmptyException(attributeID, exceptionMessage);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBAttribute GetAttributeAndCheckNotEmpty(
    [NotNull] this IDBAttributable iDbAttributable,
    Guid attributeGuid,
    [CanBeNull] string exceptionMessage = null)
  {
    int attributeTypeId = MetaDataHelperService.Instance.GetAttributeTypeID(attributeGuid);
    IDBAttribute dbAttribute = iDbAttributable.AttributeByID(attributeTypeId, exceptionMessage);
    return !dbAttribute.IsNull ? dbAttribute : throw iDbAttributable.CreateAttributeValueIsEmptyException(attributeTypeId, exceptionMessage);
  }
}
