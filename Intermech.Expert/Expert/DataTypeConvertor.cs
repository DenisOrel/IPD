// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.DataTypeConvertor
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Expert;

/// <summary>Datatype conversion class</summary>
public class DataTypeConvertor
{
  /// <summary>Одна штука - значение по умолчанию для Measured</summary>
  private static readonly MeasuredValue OneShtuk = new MeasuredValue(1.0, ExpertConsts.Consts.measureShtuk);

  /// <summary>Получить строку, описывающую тип данных</summary>
  /// <param name="dt">Тип данных</param>
  /// <returns>Строка, описывающая тип данных</returns>
  public static string DataTypeName(DataType dt)
  {
    switch (dt)
    {
      case DataType.Integer:
        return LocalizationHolder.rm.GetString("Expert_62");
      case DataType.Float:
        return LocalizationHolder.rm.GetString("Expert_60");
      case DataType.Measured:
        return LocalizationHolder.rm.GetString("Expert_61");
      case DataType.String:
        return LocalizationHolder.rm.GetString("Expert_67");
      case DataType.Date:
        return LocalizationHolder.rm.GetString("Expert_59");
      case DataType.Boolean:
        return LocalizationHolder.rm.GetString("Expert_58");
      case DataType.ObjectLink:
        return LocalizationHolder.rm.GetString("Expert_63");
      case DataType.Packet:
        return LocalizationHolder.rm.GetString("Expert_65");
      case DataType.Diap:
        return LocalizationHolder.rm.GetString("Expert_66");
      case DataType.Attribute:
        return LocalizationHolder.rm.GetString("Expert_64");
      case DataType.ObjectIdLink:
        return LocalizationHolder.rm.GetString("Expert_281");
      default:
        return LocalizationHolder.rm.GetString("Expert_68");
    }
  }

  /// <summary>
  /// Преобразовать системный тип данных (FieldTypes) в тип данных экспертной системы (DataType)
  /// </summary>
  /// <param name="attrType">Системный тип данных</param>
  /// <returns>Тип данных ЭС</returns>
  public static DataType AttrType2DataType(FieldTypes attrType)
  {
    switch (attrType)
    {
      case FieldTypes.ftUnknown:
        throw new EInvalidAttrType(LocalizationHolder.rm.GetString("Expert_69"));
      case FieldTypes.ftString:
      case FieldTypes.ftPassword:
      case FieldTypes.ftMemo:
      case FieldTypes.ftGuid:
        return DataType.String;
      case FieldTypes.ftInteger:
      case FieldTypes.ftAutoInc:
        return DataType.Integer;
      case FieldTypes.ftDouble:
        return DataType.Float;
      case FieldTypes.ftDateTime:
        return DataType.Date;
      case FieldTypes.ftObjectLink:
        return DataType.ObjectLink;
      case FieldTypes.ftBlob:
        return DataType.Unknown;
      case FieldTypes.ftBoolean:
        return DataType.Boolean;
      case FieldTypes.ftMeasured:
        return DataType.Measured;
      case FieldTypes.ftObjectLinkByID:
        return DataType.ObjectIdLink;
      default:
        throw new EInvalidAttrType(LocalizationHolder.rm.GetString("Expert_70"));
    }
  }

  /// <summary>Получить значение по умолчанию для данного типа</summary>
  /// <param name="attrType">Системный тип данных</param>
  /// <returns>Значение по умолчанию</returns>
  public static object DefForAttrType(FieldTypes attrType)
  {
    switch (attrType)
    {
      case FieldTypes.ftUnknown:
        return (object) null;
      case FieldTypes.ftString:
      case FieldTypes.ftPassword:
      case FieldTypes.ftMemo:
      case FieldTypes.ftGuid:
        return (object) "";
      case FieldTypes.ftInteger:
      case FieldTypes.ftAutoInc:
        return (object) 0;
      case FieldTypes.ftDouble:
        return (object) 0.0;
      case FieldTypes.ftDateTime:
        return (object) DateTime.Now;
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        return (object) 0;
      case FieldTypes.ftBlob:
        return (object) null;
      case FieldTypes.ftBoolean:
        return (object) false;
      case FieldTypes.ftMeasured:
        return (object) DataTypeConvertor.OneShtuk;
      default:
        return (object) null;
    }
  }

  /// <summary>
  /// Преобразовать системный тип данных (FieldTypes) в тип данных экспертной системы (DataType)
  /// с проверкой системных атрибутов (ftSystem)
  /// </summary>
  /// <param name="ft"></param>
  /// <param name="AttributeID"></param>
  /// <returns></returns>
  public static DataType AttrType2DataType(FieldTypes ft, int AttributeID)
  {
    return ft != FieldTypes.ftSystem ? DataTypeConvertor.AttrType2DataType(ft) : DataTypeConvertor.AttrType2DataType(ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) AttributeID));
  }

  /// <summary>
  /// Преобразовать системный тип данных (FieldTypes) в тип объекта, пригодный для этих данных
  /// </summary>
  /// <param name="ft">Системный тип данных</param>
  /// <param name="AttributeID">ИД атрибута (на случай системности)</param>
  /// <returns>Type, который может хранить данные этого типа (в случае неизвестного - строка)</returns>
  public static Type FieldType2DataType(FieldTypes ft, int AttributeID)
  {
    switch (DataTypeConvertor.AttrType2DataType(ft, AttributeID))
    {
      case DataType.Integer:
      case DataType.ObjectLink:
      case DataType.ObjectIdLink:
        return typeof (long);
      case DataType.Float:
        return typeof (double);
      case DataType.Measured:
        return typeof (MeasuredValue);
      case DataType.String:
        return typeof (string);
      case DataType.Date:
        return typeof (DateTime);
      case DataType.Boolean:
        return typeof (bool);
      default:
        return typeof (string);
    }
  }

  /// <summary>Получить DataType для хранения объекта</summary>
  /// <param name="O">Любой объект</param>
  /// <returns>DataType, пригодный для его хранения</returns>
  public static DataType GetDataType(object O)
  {
    Type type = O.GetType();
    if (type == typeof (int) || type == typeof (long) || type == typeof (Decimal))
      return DataType.Integer;
    if (type == typeof (bool))
      return DataType.Boolean;
    if (type == typeof (float) || type == typeof (double))
      return DataType.Float;
    if (type == typeof (string))
      return DataType.String;
    return type == typeof (MeasuredValue) ? DataType.Measured : DataType.Unknown;
  }

  public static Type AttrType2Type(FieldTypes attrType)
  {
    return DataTypeConvertor.DataType2Type(DataTypeConvertor.AttrType2DataType(attrType));
  }

  /// <summary>
  /// Преобразовать тип данных ЭС в тип объекта, пригодный для этих данных
  /// </summary>
  /// <param name="dataType">Тип данных ЭС</param>
  /// <returns>Тип объекта, пригодный для хранения данных</returns>
  public static Type DataType2Type(DataType dataType)
  {
    switch (dataType)
    {
      case DataType.Integer:
        return typeof (long);
      case DataType.Float:
        return typeof (double);
      case DataType.Measured:
        return typeof (MeasuredValue);
      case DataType.String:
        return typeof (string);
      case DataType.Date:
        return typeof (DateTime);
      case DataType.Boolean:
        return typeof (bool);
      case DataType.ObjectLink:
      case DataType.ObjectIdLink:
        return typeof (long);
      case DataType.Packet:
        return typeof (PacketValue);
      case DataType.Diap:
        return typeof (DiapValue);
      default:
        return (Type) null;
    }
  }
}
