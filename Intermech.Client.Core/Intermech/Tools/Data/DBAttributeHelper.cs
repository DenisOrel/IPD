
// Type: Intermech.Tools.Data.DBAttributeHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Tools.Data;

/// <summary>
/// Содержит утилиты для работы с атрибутами объектов и связей.
/// </summary>
public static class DBAttributeHelper
{
  private static readonly FieldTypes[] simpleFieldTypes = new FieldTypes[10]
  {
    FieldTypes.ftAutoInc,
    FieldTypes.ftInteger,
    FieldTypes.ftString,
    FieldTypes.ftBoolean,
    FieldTypes.ftDateTime,
    FieldTypes.ftDouble,
    FieldTypes.ftGuid,
    FieldTypes.ftMeasured,
    FieldTypes.ftObjectLink,
    FieldTypes.ftObjectLinkByID
  };

  /// <summary>
  /// Возвращает список атрибутов, которые могут быть у элемента данных IPS (объекта, связи и т.д.).
  /// </summary>
  /// <param name="attrTypeRef">Ссылка на метаданные элемента данных</param>
  /// <param name="modes">Фильтр режимов создания для необязательных атрибутов. Если этот массив пуст, то метод вернет только обязательные атрибуты элемента данных</param>
  /// <returns>Список ключей атрибутов элемента данных</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на метаданные элемента данных IPS не может быть null</exception>
  /// <exception cref="T:System.ArgumentNullException">Ссылка фильтр режимов создания необязательных атрибутов не может быть null</exception>
  public static List<StringKey> GetAttributeLayout(
    IDBAttributableTypeRef attrTypeRef,
    params RequiredModes[] modes)
  {
    if (attrTypeRef == null)
      throw new ArgumentNullException(nameof (attrTypeRef));
    if (modes == null)
      throw new ArgumentNullException(nameof (modes));
    List<StringKey> attributeLayout = new List<StringKey>(64 /*0x40*/);
    attributeLayout.AddRange((IEnumerable<StringKey>) DBAttributeHelper.GetObligatoryAttributes(attrTypeRef));
    attributeLayout.AddRange((IEnumerable<StringKey>) DBAttributeHelper.GetCustomAttributes(attrTypeRef, modes));
    return attributeLayout;
  }

  private static List<StringKey> GetObligatoryAttributes(IDBAttributableTypeRef attrTypeRef)
  {
    Array values = Enum.GetValues(typeof (ObligatoryObjectAttributes));
    AttributeSourceTypes attributeSourceType = attrTypeRef.GetAttributeSourceType();
    List<StringKey> obligatoryAttributes = new List<StringKey>(values.Length);
    foreach (ObligatoryObjectAttributes objectAttributes in values)
    {
      if (ObligatoryObjectAttributesHelper.GetAttributeSourceType(objectAttributes) == attributeSourceType)
        obligatoryAttributes.Add((StringKey) ObligatoryObjectAttributesHelper.GetCaption(objectAttributes));
    }
    return obligatoryAttributes;
  }

  private static List<StringKey> GetCustomAttributes(
    IDBAttributableTypeRef attrTypeRef,
    RequiredModes[] modes)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = attrTypeRef.GetAttributableType(sessionKeeper.Session).Select(string.Empty);
      List<StringKey> customAttributes = new List<StringKey>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        RequiredModes int32_1 = (RequiredModes) Convert.ToInt32(row["F_REQUIRED"]);
        if (Array.IndexOf<RequiredModes>(modes, int32_1) >= 0)
        {
          int int32_2 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(int32_2, true);
          if (DBAttributeHelper.IsSimpleAttributeType(attributeType))
            customAttributes.Add(new StringKey(attributeType.Name));
        }
      }
      return customAttributes;
    }
  }

  /// <summary>
  /// Читает значения атрибутов указанного элемента данных в базе IPS.
  /// </summary>
  /// <param name="attrTypeRef">Ссылка на метаданные элемента данных</param>
  /// <param name="rawValues">Массив значений атрибутов элемента данных в формате, возвращаемом сервером приложений IPS</param>
  /// <returns>Список прочитанных значений атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на метаданные элемента данных IPS не может быть null</exception>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на массив значений атрибутов не может быть null</exception>
  public static List<ValueRecord> ReadEntityValues(
    IDBAttributableTypeRef attrTypeRef,
    ICollection<AttributeValues> rawValues)
  {
    if (attrTypeRef == null)
      throw new ArgumentNullException(nameof (attrTypeRef));
    List<ValueRecord> valueRecordList = rawValues != null ? new List<ValueRecord>(rawValues.Count) : throw new ArgumentNullException(nameof (rawValues));
    foreach (AttributeValues rawValue in (IEnumerable<AttributeValues>) rawValues)
    {
      ValueRecord valueRecord = DBAttributeHelper.TryReadEntityValue(attrTypeRef, rawValue);
      if (valueRecord != null)
        valueRecordList.Add(valueRecord);
    }
    return valueRecordList;
  }

  /// <summary>
  /// Пытается прочитать значение атрибута указанного элемента данных в базе IPS.
  /// </summary>
  /// <param name="attrTypeRef">Ссылка на метаданные элемента данных</param>
  /// <param name="rawValue">Значение атрибута элемента данных в формате, возвращаемом сервером приложений IPS</param>
  /// <returns>Прочитанное значение или null</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на метаданные элемента данных IPS не может быть null</exception>
  /// <exception cref="T:System.ArgumentNullException">ССылка на значение атрибута не может быть null</exception>
  public static ValueRecord TryReadEntityValue(
    IDBAttributableTypeRef attrTypeRef,
    AttributeValues rawValue)
  {
    if (attrTypeRef == null)
      throw new ArgumentNullException(nameof (attrTypeRef));
    if (rawValue == null)
      throw new ArgumentNullException(nameof (rawValue));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType dbAttrType = sessionKeeper.Session.GetAttributeType(rawValue.AttributeID, false);
      if (dbAttrType != null && dbAttrType.AttributeType != FieldTypes.ftSystem)
        dbAttrType = (IDBAttributeType) attrTypeRef.GetAttributableType(sessionKeeper.Session).GetAttributeByID(rawValue.AttributeID, false);
      if (dbAttrType == null || !DBAttributeHelper.IsSimpleAttributeType(dbAttrType))
        return (ValueRecord) null;
      StringKey key = new StringKey(dbAttrType.Name);
      Type dataType = DBAttributeHelper.GetDataType(dbAttrType);
      object obj = DBAttributeHelper.ConvertValue(dbAttrType, dataType, rawValue.Values[0]);
      return new ValueRecord(key, obj)
      {
        Flags = {
          [NamedFlags.ReadOnly] = rawValue.ReadOnly || DBAttributeHelper.IsReadOnly(attrTypeRef, dbAttrType)
        }
      };
    }
  }

  /// <summary>
  /// Возвращает значения атрибутов еще не созданных объектов и связей. Значения определяются на основании
  /// метаданных объекта/связи.
  /// </summary>
  /// <param name="attrTypeRef">Ссылка на метаданные элемента данных</param>
  /// <param name="modes">Фильтр режимов создания для необязательных атрибутов. Если этот массив пуст, то метод вернет только обязательные атрибуты элемента данных</param>
  /// <returns>Список значений атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на метаданные элемента данных IPS не может быть null</exception>
  /// <exception cref="T:System.ArgumentNullException">Ссылка фильтр режимов создания необязательных атрибутов не может быть null</exception>
  public static List<ValueRecord> ReadBlankValues(
    IDBAttributableTypeRef attrTypeRef,
    params RequiredModes[] modes)
  {
    if (attrTypeRef == null)
      throw new ArgumentNullException(nameof (attrTypeRef));
    List<StringKey> stringKeyList = modes != null ? DBAttributeHelper.GetAttributeLayout(attrTypeRef, modes) : throw new ArgumentNullException(nameof (modes));
    List<ValueRecord> valueRecordList = new List<ValueRecord>(stringKeyList.Count);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (StringKey anAttributeName in stringKeyList)
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType((string) anAttributeName, true);
        bool isReadOnly = DBAttributeHelper.IsReadOnly(attrTypeRef, attributeType);
        valueRecordList.Add(DBAttributeHelper.GetBlankValue(attributeType, isReadOnly));
      }
    }
    return valueRecordList;
  }

  public static AttributeValues[] ToAttributeValues(IList<ValueRecord> items)
  {
    List<AttributeValues> attributeValuesList = items != null ? new List<AttributeValues>(items.Count) : throw new ArgumentNullException(nameof (items));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (ValueRecord valueRecord in (IEnumerable<ValueRecord>) items)
      {
        bool flag = valueRecord.Flags[NamedFlags.ThrowSetException];
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType((string) valueRecord.Key, flag);
        if (attributeType != null)
          attributeValuesList.Add(new AttributeValues(attributeType.AttributeID, valueRecord.Value)
          {
            AttributeName = (string) valueRecord.Key,
            ThrowSetException = flag,
            IsNew = true
          });
      }
    }
    return attributeValuesList.ToArray();
  }

  public static AttributeValues[] ToAttributeValues(params ValueRecord[] items)
  {
    return DBAttributeHelper.ToAttributeValues((IList<ValueRecord>) items);
  }

  internal static FieldTypes GetFieldType(IDBAttributeType dbAttrType)
  {
    return dbAttrType.AttributeType == FieldTypes.ftSystem ? ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) dbAttrType.AttributeID) : dbAttrType.AttributeType;
  }

  internal static bool IsNullable(IDBAttributeType dbAttrType)
  {
    if (dbAttrType == null)
      throw new ArgumentNullException();
    return (dbAttrType.Options & AttributeOptions.DisableNulls) == AttributeOptions.None;
  }

  internal static bool IsReadOnly(IDBAttributableTypeRef attrTypeRef, IDBAttributeType dbAttrType)
  {
    if (dbAttrType.AttributeID == -50)
    {
      int captionAttribute = attrTypeRef.GetCaptionAttribute();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (captionAttribute > 0)
          return DBAttributeHelper.IsReadOnly(attrTypeRef, (IDBAttributeType) attrTypeRef.GetAttributableType(sessionKeeper.Session).GetAttributeByID(captionAttribute, true));
      }
      return false;
    }
    return dbAttrType.AttributeType == FieldTypes.ftSystem || dbAttrType.AttributeType == FieldTypes.ftAutoInc || dbAttrType.Computed != 0;
  }

  internal static bool IsSimpleAttributeType(IDBAttributeType dbAttrType)
  {
    return (dbAttrType.MultipleValued == MultiValueModes.SingleValue || dbAttrType.MultipleValued == MultiValueModes.SingleValueFromList) && Array.IndexOf<FieldTypes>(DBAttributeHelper.simpleFieldTypes, DBAttributeHelper.GetFieldType(dbAttrType)) >= 0;
  }

  public static Type TryGetDataType(IDBAttributeType dbAttrType)
  {
    if (dbAttrType == null)
      throw new ArgumentNullException(nameof (dbAttrType));
    switch (DBAttributeHelper.GetFieldType(dbAttrType))
    {
      case FieldTypes.ftString:
        return typeof (string);
      case FieldTypes.ftInteger:
        return typeof (long);
      case FieldTypes.ftDouble:
        return typeof (double);
      case FieldTypes.ftDateTime:
        return typeof (DateTime);
      case FieldTypes.ftObjectLink:
        return typeof (long);
      case FieldTypes.ftBoolean:
        return typeof (bool);
      case FieldTypes.ftMeasured:
        return typeof (MeasuredValue);
      case FieldTypes.ftAutoInc:
        return typeof (long);
      case FieldTypes.ftGuid:
        return typeof (Guid);
      case FieldTypes.ftObjectLinkByID:
        return typeof (long);
      default:
        return (Type) null;
    }
  }

  internal static Type GetDataType(IDBAttributeType dbAttrType)
  {
    Type dataType = DBAttributeHelper.TryGetDataType(dbAttrType);
    return !(dataType == (Type) null) ? dataType : throw new NotImplementedException(string.Format(LocalizationHolder.rm.GetString("SR_1620"), (object) dbAttrType.Name, (object) DBAttributeHelper.GetFieldType(dbAttrType)));
  }

  internal static object ConvertValue(IDBAttributeType dbAttrType, Type dataType, object value)
  {
    if (ValueRecord.IsNullValue(value) || object.Equals(value, (object) string.Empty))
      return !DBAttributeHelper.IsNullable(dbAttrType) ? DBAttributeHelper.GetEmptyValue(dbAttrType, dataType) : (object) TypedNull.Instance(dataType);
    switch (DBAttributeHelper.GetFieldType(dbAttrType))
    {
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        long int64 = Convert.ToInt64(value);
        switch (int64)
        {
          case -1:
          case 0:
            return (object) TypedNull.Instance(dataType);
          default:
            return (object) Math.Abs(int64);
        }
      default:
        if (value.GetType() == dataType)
          return value;
        if (dataType == typeof (MeasuredValue))
        {
          MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(Convert.ToString(value), DBAttributeHelper.GetDefaultMeasure(dbAttrType), true);
          MeasureHelper.CorrectCaption(measuredValue);
          return (object) measuredValue;
        }
        if (dataType == typeof (bool))
        {
          string strA = Convert.ToString(value);
          if (string.Compare(strA, "true", true) == 0)
            return (object) true;
          if (string.Compare(strA, "on", true) == 0)
            return (object) true;
          if (string.Compare(strA, "1", true) == 0)
            return (object) true;
          if (string.Compare(strA, Consts.TrueValue, true) == 0)
            return (object) true;
          if (string.Compare(strA, Consts.YesValue, true) == 0)
            return (object) true;
          if (string.Compare(strA, "false", true) == 0)
            return (object) false;
          if (string.Compare(strA, "off", true) == 0)
            return (object) false;
          if (string.Compare(strA, "0", true) == 0)
            return (object) false;
          if (string.Compare(strA, Consts.FalseValue, true) == 0)
            return (object) false;
          if (string.Compare(strA, Consts.NoValue, true) == 0)
            return (object) false;
          throw new InvalidCastException(string.Format(LocalizationHolder.rm.GetString("SR_1621"), value, (object) dbAttrType.Name));
        }
        if (dataType == typeof (Guid))
          return (object) new Guid(Convert.ToString(value));
        try
        {
          return Convert.ChangeType(value, dataType);
        }
        catch (FormatException ex)
        {
          return DBAttributeHelper.GetEmptyValue(dbAttrType, dataType);
        }
    }
  }

  private static object GetEmptyValue(IDBAttributeType dbAttrType, Type dataType)
  {
    if (dataType == typeof (MeasuredValue))
    {
      MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue("0", DBAttributeHelper.GetDefaultMeasure(dbAttrType), true);
      MeasureHelper.CorrectCaption(measuredValue);
      return (object) measuredValue;
    }
    if (dataType == typeof (string))
      return (object) string.Empty;
    if (dataType == typeof (long))
      return (object) 0L;
    if (dataType == typeof (double))
      return (object) 0.0;
    if (dataType == typeof (bool))
      return (object) false;
    if (dataType == typeof (DateTime))
      return (object) new DateTime(0L);
    if (dataType == typeof (Guid))
      return (object) Guid.Empty;
    throw new NotImplementedException(string.Format(LocalizationHolder.rm.GetString("SR_1622"), (object) dbAttrType.Name));
  }

  private static MeasureDescriptor GetDefaultMeasure(IDBAttributeType dbAttrType)
  {
    long defaultMeasure = DBAttributeHelper.GetDefaultMeasure(dbAttrType.AttributeID, dbAttrType.SizeType);
    switch (defaultMeasure)
    {
      case -1:
      case 0:
        throw new NotSupportedException(string.Format(LocalizationHolder.rm.GetString("SR_1623"), (object) dbAttrType.Name));
      default:
        MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(defaultMeasure);
        if (!descriptor.Empty)
          return descriptor;
        goto case -1;
    }
  }

  private static long GetDefaultMeasure(int attrId, long sizeType)
  {
    if (sizeType != 0L && sizeType != -1L)
      return MeasureHelper.GetBaseMeasureID(sizeType);
    if (attrId == DBAttributeHelper.InternalIDCache.Count.Id)
      return DBAttributeHelper.InternalIDCache.ItemsMeasure.Id;
    return attrId == DBAttributeHelper.InternalIDCache.Mass.Id ? DBAttributeHelper.InternalIDCache.KilogramMeasure.Id : DBAttributeHelper.InternalIDCache.ItemsMeasure.Id;
  }

  internal static ValueRecord GetBlankValue(IDBAttributeType dbAttrType, bool isReadOnly)
  {
    StringKey name = (StringKey) dbAttrType.Name;
    Type dataType = DBAttributeHelper.GetDataType(dbAttrType);
    object defaultValue = dbAttrType.DefaultValue;
    object obj = ValueRecord.IsNullValue(defaultValue) || object.Equals(defaultValue, (object) string.Empty) ? (DBAttributeHelper.IsNullable(dbAttrType) ? (object) TypedNull.Instance(dataType) : DBAttributeHelper.GetEmptyValue(dbAttrType, dataType)) : DBAttributeHelper.ConvertValue(dbAttrType, dataType, defaultValue);
    return new ValueRecord(name, obj)
    {
      Flags = {
        [NamedFlags.ReadOnly] = isReadOnly
      }
    };
  }

  private static class InternalIDCache
  {
    public static readonly AttributeTypeResolver Count = MetadataResolvers.Factory.AttributeTypeResolver(new Guid("CAD00267-306C-11D8-B4E9-00304F19F545"));
    public static readonly AttributeTypeResolver Mass = MetadataResolvers.Factory.AttributeTypeResolver(new Guid("CAD00275-306C-11D8-B4E9-00304F19F545"));
    public static readonly SpecialObjectResolver ItemsMeasure = MetadataResolvers.Factory.SpecialObjectResolver(new Guid("CAD002E8-306C-11D8-B4E9-00304F19F545"));
    public static readonly SpecialObjectResolver KilogramMeasure = MetadataResolvers.Factory.SpecialObjectResolver(new Guid("CAD002EB-306C-11D8-B4E9-00304F19F545"));
  }
}
