// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AVSDocumentTypeSettings
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Настройки для типа конструкторских документов</summary>
public class AVSDocumentTypeSettings : IWriteReadXml
{
  /// <summary>Внутренний идентификатор типа</summary>
  public Guid TypeGuid;
  /// <summary>Тип конструкторского документа</summary>
  public AVSDocumentType AVSDocType;
  /// <summary>Наименование типа</summary>
  public string TypeName;
  /// <summary>Список типов объектов-документов БД соответствующих этому типу</summary>
  public List<Guid> DBObjectTypeList;
  /// <summary>Структура наследования настроек</summary>
  public SettingsStructure SettingsInheritanceStructure;

  /// <summary>Конструктор</summary>
  public AVSDocumentTypeSettings()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeGuid">Внутренний идентификатор типа</param>
  /// <param name="avsDocType">Тип конструкторского документа</param>
  /// <param name="typeName">Наименование типа</param>
  /// <param name="dbObjectTypeList">Список типов объектов-документов БД соответсвующих этому типу</param>
  public AVSDocumentTypeSettings(
    Guid typeGuid,
    AVSDocumentType avsDocType,
    string typeName,
    List<Guid> dbObjectTypeList)
    : this(typeGuid, avsDocType, typeName, dbObjectTypeList, (SettingsStructure) null)
  {
    switch (avsDocType)
    {
      case AVSDocumentType.Specification:
        this.SettingsInheritanceStructure = (SettingsStructure) new SpecificationSettingsStructure();
        break;
      case AVSDocumentType.AutoIndustrySpecification:
        this.SettingsInheritanceStructure = (SettingsStructure) new AutopromSpecificationSettingsStructure();
        break;
      case AVSDocumentType.ExportSpecification:
        this.SettingsInheritanceStructure = (SettingsStructure) new ExportSpecificationSettingsStructure();
        break;
      case AVSDocumentType.ElementList:
        this.SettingsInheritanceStructure = (SettingsStructure) new ElementListSettingsStructure();
        break;
      default:
        this.SettingsInheritanceStructure = (SettingsStructure) new UserAVSDocumentSettingsStructure();
        break;
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeGuid">Внутренний идентификатор типа</param>
  /// <param name="avsDocType">Тип конструкторского документа</param>
  /// <param name="typeName">Наименование типа</param>
  /// <param name="dbObjectTypeList">Список типов объектов-документов БД соответсвующих этому типу</param>
  /// <param name="settingsStructure">Структура наследования настроек для данного типа документов</param>
  public AVSDocumentTypeSettings(
    Guid typeGuid,
    AVSDocumentType avsDocType,
    string typeName,
    List<Guid> dbObjectTypeList,
    SettingsStructure settingsStructure)
  {
    this.TypeGuid = typeGuid;
    this.AVSDocType = avsDocType;
    this.TypeName = typeName;
    this.DBObjectTypeList = dbObjectTypeList;
    this.SettingsInheritanceStructure = settingsStructure;
  }

  /// <summary>Получить настройки конструкторского документа по умолчанию</summary>
  /// <param name="avsDocType">Тип документа</param>
  /// <returns></returns>
  public static AVSDocumentTypeSettings GetDefaultAVSDocumentTypeSettings(AVSDocumentType avsDocType)
  {
    switch (avsDocType)
    {
      case AVSDocumentType.Specification:
        return new AVSDocumentTypeSettings(AvsIDCache.AVSDocTypeGuid_Specification, avsDocType, "Спецификация ЕСКД", new List<Guid>((IEnumerable<Guid>) new Guid[1]
        {
          new Guid("cad00133-306c-11d8-b4e9-00304f19f545")
        }), (SettingsStructure) new SpecificationSettingsStructure());
      case AVSDocumentType.AutoIndustrySpecification:
        return new AVSDocumentTypeSettings(AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification, avsDocType, "Спецификация автомобильная", new List<Guid>((IEnumerable<Guid>) new Guid[1]
        {
          new Guid("cad00133-306c-11d8-b4e9-00304f19f545")
        }), (SettingsStructure) new AutopromSpecificationSettingsStructure());
      case AVSDocumentType.ExportSpecification:
        return new AVSDocumentTypeSettings(AvsIDCache.AVSDocTypeGuid_ExportSpecification, avsDocType, "Спецификация экспортная", new List<Guid>((IEnumerable<Guid>) new Guid[1]
        {
          new Guid("cad00133-306c-11d8-b4e9-00304f19f545")
        }), (SettingsStructure) new ExportSpecificationSettingsStructure());
      case AVSDocumentType.ElementList:
        return new AVSDocumentTypeSettings(AvsIDCache.AVSDocTypeGuid_ElementList, avsDocType, "Перечень элементов", new List<Guid>((IEnumerable<Guid>) new Guid[8]
        {
          AvsIDCache.ObjType_ElementList0Guid,
          AvsIDCache.ObjType_ElementList1Guid,
          AvsIDCache.ObjType_ElementList2Guid,
          AvsIDCache.ObjType_ElementList3Guid,
          AvsIDCache.ObjType_ElementList4Guid,
          AvsIDCache.ObjType_ElementList5Guid,
          AvsIDCache.ObjType_ElementList6Guid,
          AvsIDCache.ObjType_ElementList7Guid
        }), (SettingsStructure) new ElementListSettingsStructure());
      default:
        return new AVSDocumentTypeSettings(Guid.NewGuid(), avsDocType, "Пользовательский конструкторский документ", new List<Guid>(), (SettingsStructure) new UserAVSDocumentSettingsStructure());
    }
  }

  /// <summary>Получить Guid стандартного типа конструкторского документа</summary>
  /// <param name="avsDocType">Тип документа</param>
  /// <returns></returns>
  public static Guid GetStdDocTypeGuid(AVSDocumentType avsDocType)
  {
    switch (avsDocType)
    {
      case AVSDocumentType.Specification:
        return AvsIDCache.AVSDocTypeGuid_Specification;
      case AVSDocumentType.AutoIndustrySpecification:
        return AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification;
      case AVSDocumentType.ExportSpecification:
        return AvsIDCache.AVSDocTypeGuid_ExportSpecification;
      case AVSDocumentType.ElementList:
        return AvsIDCache.AVSDocTypeGuid_ElementList;
      default:
        return Guid.Empty;
    }
  }

  /// <summary>Преобразовать тип спецификации в тип конструкторского документа</summary>
  /// <param name="specType">Тип спецификации</param>
  /// <returns></returns>
  public static AVSDocumentType ConvertSpecificationTypeToAVSDocType(AVSSpecificationType specType)
  {
    switch (specType)
    {
      case AVSSpecificationType.ESKD:
        return AVSDocumentType.Specification;
      case AVSSpecificationType.AutoProm:
        return AVSDocumentType.AutoIndustrySpecification;
      case AVSSpecificationType.Export:
        return AVSDocumentType.ExportSpecification;
      default:
        return AVSDocumentType.Specification;
    }
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    xw.WriteAttributeString("TypeGuid", this.TypeGuid.ToString());
    xw.WriteAttributeString("AVSDocType", this.AVSDocType.ToString());
    xw.WriteAttributeString("TypeName", this.TypeName);
    if (this.DBObjectTypeList != null && this.DBObjectTypeList.Count > 0)
      WriteReadXmlHelper.WriteListToXml("DBObjectTypeList", (IList) this.DBObjectTypeList, "DBDocType", xw, objectRefId);
    xw.WriteEndElement();
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
    if (this.AVSDocType == AVSDocumentType.Specification)
      this.SettingsInheritanceStructure = (SettingsStructure) new SpecificationSettingsStructure();
    else if (this.AVSDocType == AVSDocumentType.AutoIndustrySpecification)
      this.SettingsInheritanceStructure = (SettingsStructure) new AutopromSpecificationSettingsStructure();
    else if (this.AVSDocType == AVSDocumentType.ExportSpecification)
      this.SettingsInheritanceStructure = (SettingsStructure) new ExportSpecificationSettingsStructure();
    else if (this.AVSDocType == AVSDocumentType.ElementList)
      this.SettingsInheritanceStructure = (SettingsStructure) new ElementListSettingsStructure();
    else
      this.SettingsInheritanceStructure = (SettingsStructure) new UserAVSDocumentSettingsStructure();
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if ("TypeGuid" == readArgs.Reader.LocalName)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      this.TypeGuid = new Guid(readArgs.Reader.Value);
      return true;
    }
    if ("AVSDocType" == readArgs.Reader.LocalName)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      this.AVSDocType = (AVSDocumentType) Enum.Parse(typeof (AVSDocumentType), readArgs.Reader.Value);
      return true;
    }
    if ("TypeName" == readArgs.Reader.LocalName)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      this.TypeName = readArgs.Reader.Value;
      return true;
    }
    if (!("DBObjectTypeList" == readArgs.Reader.LocalName))
      return false;
    if (this.DBObjectTypeList == null)
      this.DBObjectTypeList = new List<Guid>();
    WriteReadXmlHelper.ReadListFromXml((IList) this.DBObjectTypeList, typeof (Guid), readArgs);
    return true;
  }
}
