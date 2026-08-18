// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttribute4TypeInfoCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для описания коллекций атрибутов у типов
/// </summary>
internal abstract class CAttribute4TypeInfoCollection : 
  MetadataInfoCollection,
  IDBAttribute4TypeInfoCollection,
  IDBMetadataInfoCollection
{
  public CAttribute4TypeInfoCollection(
    MetadataInfoParentContext serviceContext,
    int parentID,
    bool filtering)
    : base(serviceContext, (object) parentID, filtering)
  {
  }

  /// <summary>Создает обработчик атрибута применительно к типу</summary>
  /// <param name="attr_row">Запись из IMS_ATTRIBUTES</param>
  /// <param name="attr4type_row">Запись из таблицы применения атрибута к типу</param>
  /// <returns></returns>
  protected abstract IDBAttributeTypeInfo4 CreateAttributeTypeInfo4(
    DataRow attr_row,
    DataRow attr4type_row);

  /// <summary>Выдает ошибку о том, что атрибут не найден</summary>
  /// <param name="attributeID"></param>
  protected abstract void ThrowNotFoundException(int attributeID);

  /// <summary>
  /// Ищет в кэше строку с описанием атрибута применительно к типу
  /// </summary>
  /// <param name="attributeID">Ид. атрибута</param>
  /// <returns></returns>
  protected abstract DataRow FindRow(int attributeID);

  public IDBAttributeTypeInfo4 GetAttributeByID(int attributeID, bool throwNotFoundException)
  {
    DataRow row = this.FindRow(attributeID);
    if (row != null)
      return this.CreateAttributeTypeInfo4(this.ServiceContext.ClientCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID), row);
    if (throwNotFoundException)
      this.ThrowNotFoundException(attributeID);
    return (IDBAttributeTypeInfo4) null;
  }

  public IDBAttributeTypeInfo4 GetAttributeByID(int attributeID)
  {
    return this.GetAttributeByID(attributeID, false);
  }

  public IDBAttributeTypeInfo4 GetAttributeByName(string attributeName, bool throwNotFoundException)
  {
    int attributeByTypeNameId = MetaDataHelper.GetAttributeByTypeNameID(attributeName);
    if (attributeByTypeNameId != -10000)
      return this.GetAttributeByID(attributeByTypeNameId, throwNotFoundException);
    if (throwNotFoundException)
      throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString("AttributeTypeNameNotFound"), (object) attributeName));
    return (IDBAttributeTypeInfo4) null;
  }

  public IDBAttributeTypeInfo4 GetAttributeByName(string attributeName)
  {
    return this.GetAttributeByName(attributeName, false);
  }

  public IDBAttributeTypeInfo4 GetAttributeByGUID(Guid attributeGuid, bool throwNotFoundException)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attributeGuid);
    if (attributeTypeId != -10000)
      return this.GetAttributeByID(attributeTypeId, throwNotFoundException);
    if (throwNotFoundException)
      throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString("AttributeTypeGuidNotFound"), (object) attributeGuid));
    return (IDBAttributeTypeInfo4) null;
  }

  public IDBAttributeTypeInfo4 GetAttributeByGUID(Guid attributeGuid)
  {
    return this.GetAttributeByGUID(attributeGuid, false);
  }

  public IDBAttributeTypeInfo[] GetAttributeTypeList(object[] idList, bool failIfNotFound)
  {
    throw new NotImplementedException();
  }

  public abstract bool IsEnabledAttribute(int attributeID);
}
