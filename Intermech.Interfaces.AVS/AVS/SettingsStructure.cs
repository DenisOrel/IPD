// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.SettingsStructure
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Expert;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary> Описание уровней настроек  </summary>
public class SettingsStructure
{
  /// <summary>Список всех уровней</summary>
  public SettingsLevel[] _allLevels;

  /// <summary>Список всех уровней</summary>
  public SettingsLevel[] AllLevels => this._allLevels;

  /// <summary>Конструктор</summary>
  protected SettingsStructure() => this.Init();

  /// <summary>Функция инициализации</summary>
  protected virtual void Init()
  {
  }

  /// <summary> Получение дескриптора уровня настроек по идентификатору типа их сохраняющего </summary>
  /// <param name="typeID"> Идентификатор типа </param>
  /// <returns> Дескриптора уровня настроек </returns>
  public SettingsLevel GetSettingsLevel(InheritanceSettingsLevel level)
  {
    if (this.AllLevels != null)
    {
      for (int index = 0; index < this.AllLevels.Length; ++index)
      {
        if (this.AllLevels[index].InheritanceLevel == level)
          return this.AllLevels[index];
      }
    }
    return (SettingsLevel) null;
  }

  /// <summary>Создание объекта с настройками
  /// связанного с некоторым объектом (спецификацией, шаблоном и т.п.)</summary>
  /// <param name="iUserSession">сессия</param>
  /// <param name="objectID">ID объекта в атрибутах которого хранятся настройки</param>
  /// <param name="objectType">ID типа переданного объекта (-1 если неизвестен). Ускоряет работу</param>
  /// <param name="templateID">ID шаблона СП (-1 если это не СП)</param>
  /// <param name="settingsHolderAttributeID">ID Атрибута который хранит настройки сортировки в XML формате</param>
  /// <param name="settingsType">Тип объекта-контейнера создаваемых настроек</param>
  /// <returns> Объект "Настройки нумерации позиций" </returns>
  public object CreateSettingsLevelFromObject(
    IUserSession iUserSession,
    long objectID,
    int objectType,
    long templateID,
    int settingsHolderAttributeID,
    Type settingsType)
  {
    return this.CreateSettingsLevelFromObject(iUserSession, objectID, objectType, templateID, settingsHolderAttributeID, settingsType, (List<Triple>) null);
  }

  /// <summary>Создание объекта с настройками
  /// связанного с некоторым объектом (документом, шаблоном и т.п.)</summary>
  /// <param name="iUserSession">сессия</param>
  /// <param name="objectID">ID объекта в атрибутах которого хранятся настройки</param>
  /// <param name="objectType">ID типа переданного объекта (-1 если неизвестен). Ускоряет работу</param>
  /// <param name="templateID">ID шаблона СП (-1 если это не СП)</param>
  /// <param name="settingsHolderAttributeID">ID Атрибута который хранит настройки сортировки в XML формате</param>
  /// <param name="settingsType">Тип объекта-контейнера создаваемых настроек</param>
  /// <param name="tripleList"></param>
  /// <returns> Объект "Настройки нумерации позиций" </returns>
  public object CreateSettingsLevelFromObject(
    IUserSession iUserSession,
    long objectID,
    int objectType,
    long templateID,
    int settingsHolderAttributeID,
    Type settingsType,
    List<Triple> tripleList)
  {
    object parentLevel = (object) null;
    SettingsLevel level = (SettingsLevel) null;
    long num = -1;
    int objectType1 = -1;
    if (objectID.IsDefinedId())
    {
      IDBObject objectActual = iUserSession.GetObjectActual(objectID, true);
      if (objectType.IsUndefinedTypeId())
        objectType = objectActual.ObjectType;
      IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_SpecificationForm);
      AVSDocumentForm? nullable = new AVSDocumentForm?();
      if (attributeById != null)
        nullable = SpecificationFormMethods.DecodeSpecificationFormAttrValue(attributeById.AsString);
      if (!nullable.HasValue)
        nullable = new AVSDocumentForm?(AVSDocumentForm.Single);
      Guid templateGuid;
      if (MetaDataHelper.IsObjectTypeChildOf(objectType, AvsIDCache.ObjType_ConstructorDocumentTemplate))
      {
        Guid parentTemplate = AVSDocumentsSettings.GetParentTemplate(objectActual.ObjectGUID);
        if (parentTemplate == Guid.Empty)
        {
          num = -1L;
          level = this.GetSettingsLevel(AVSDocumentsSettings.GetSettingsLevel(objectActual.ObjectGUID, objectType));
        }
        else
        {
          IDBObject dbObject = iUserSession.GetObject(parentTemplate);
          num = dbObject.ObjectID;
          objectType1 = dbObject.ObjectType;
          level = this.GetSettingsLevel(InheritanceSettingsLevel.Template);
        }
      }
      else if (MetaDataHelper.IsObjectTypeChildOf(objectActual.ObjectType, AvsIDCache.ObjType_Specification))
      {
        if (templateID.IsUndefinedId())
        {
          num = AVSDocumentsSettings.Instance.GetTemplate(AvsIDCache.AVSDocTypeGuid_Specification, new AVSDocumentForm?(nullable.Value), out templateGuid, iUserSession, false);
          objectType1 = AvsIDCache.ObjType_ConstructorDocumentTemplate;
        }
        else
        {
          num = templateID;
          objectType1 = AvsIDCache.ObjType_ConstructorDocumentTemplate;
        }
        level = this.GetSettingsLevel(InheritanceSettingsLevel.Document);
      }
      else if (AvsIDCache.IsElementList(objectType))
      {
        if (templateID.IsUndefinedId())
        {
          num = AVSDocumentsSettings.Instance.GetTemplate(AvsIDCache.AVSDocTypeGuid_ElementList, new AVSDocumentForm?(nullable.Value), out templateGuid, iUserSession, false);
          objectType1 = AvsIDCache.ObjType_ConstructorDocumentTemplate;
        }
        else
        {
          num = templateID;
          objectType1 = AvsIDCache.ObjType_ConstructorDocumentTemplate;
        }
        level = this.GetSettingsLevel(InheritanceSettingsLevel.Document);
      }
      else if (MetaDataHelper.IsObjectTypeChildOf(objectActual.ObjectType, AvsIDCache.ObjType_Document))
      {
        if (templateID.IsUndefinedId())
        {
          AVSDocumentTypeSettings typeForDbObjectType = AVSDocumentsSettings.Instance.GetDefaultDocumentTypeForDBObjectType(objectActual.ObjectType, AVSDocumentType.Specification);
          if (typeForDbObjectType != null)
          {
            num = AVSDocumentsSettings.Instance.GetTemplate(typeForDbObjectType.TypeGuid, new AVSDocumentForm?(nullable.Value), out templateGuid, iUserSession, false);
            objectType1 = AvsIDCache.ObjType_ConstructorDocumentTemplate;
          }
          else
            num = -1L;
        }
        else
        {
          num = templateID;
          objectType1 = AvsIDCache.ObjType_ConstructorDocumentTemplate;
        }
        level = this.GetSettingsLevel(InheritanceSettingsLevel.Document);
      }
      else
      {
        num = -1L;
        objectType1 = 0;
        level = this.GetSettingsLevel(InheritanceSettingsLevel.Document);
      }
    }
    else if (!templateID.IsUndefinedId())
    {
      IDBObject dbObject = iUserSession.GetObject(templateID);
      num = dbObject.ObjectID;
      objectType1 = dbObject.ObjectType;
      level = this.GetSettingsLevel(InheritanceSettingsLevel.Template);
      objectID = num;
    }
    if (num.IsDefinedId())
      parentLevel = this.CreateSettingsLevelFromObject(iUserSession, num, objectType1, -1L, settingsHolderAttributeID, settingsType);
    if (typeof (SortSchema).IsAssignableFrom(settingsType))
      return (object) new SortSchema(iUserSession, (SortSchema) parentLevel, objectID, level, tripleList);
    return Activator.CreateInstance(settingsType, parentLevel, (object) objectID, (object) level);
  }

  /// <summary>Получить структуру настроек для шаблона</summary>
  /// <param name="objectGuid">Глобальный идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="inheritanceLevel">Уровень в структуре наследования</param>
  /// <returns></returns>
  public static SettingsStructure GetSettingsStructure(
    Guid objectGuid,
    int objectType,
    out InheritanceSettingsLevel inheritanceLevel)
  {
    AVSDocumentTypeSettings settingsForTemplate = AVSDocumentsSettings.Instance.GetDocumentTypeSettingsForTemplate(objectGuid, out inheritanceLevel);
    SettingsStructure settingsStructure;
    if (settingsForTemplate != null)
    {
      settingsStructure = settingsForTemplate.SettingsInheritanceStructure;
    }
    else
    {
      inheritanceLevel = AVSDocumentsSettings.GetSettingsLevel(objectGuid, objectType);
      AVSDocumentTypeSettings typeForDbObjectType = AVSDocumentsSettings.Instance.GetDefaultDocumentTypeForDBObjectType(objectType, AVSDocumentType.Specification);
      settingsStructure = typeForDbObjectType == null ? (SettingsStructure) new UserAVSDocumentSettingsStructure() : typeForDbObjectType.SettingsInheritanceStructure;
    }
    return settingsStructure;
  }

  /// <summary>Создание объекта с настройками
  /// связанного с некоторым объектом (документом, шаблоном и т.п.)</summary>
  /// <param name="iUserSession">сессия</param>
  /// <param name="objectID">ID объекта в атрибутах которого хранятся настройки</param>
  /// <param name="objectType">ID типа переданного объекта (-1 если неизвестен). Ускоряет работу</param>
  /// <param name="templateID">ID шаблона СП (-1 если это не СП)</param>
  /// <param name="settingsHolderAttributeID">ID Атрибута который хранит настройки сортировки в XML формате</param>
  /// <param name="settingsType">Тип объекта-контейнера создаваемых настроек</param>
  /// <param name="tripleList"></param>
  /// <returns> Объект "Настройки нумерации позиций" </returns>
  public static object StaticCreateSettingsStructureFromObject(
    IUserSession iUserSession,
    long objectID,
    int objectType,
    long templateID,
    int settingsHolderAttributeID,
    Type settingsType,
    List<Triple> tripleList)
  {
    return SettingsStructure.GetSettingsStructure(iUserSession.GetObjectInfo(objectID).VersionGuid, objectType, out InheritanceSettingsLevel _).CreateSettingsLevelFromObject(iUserSession, objectID, objectType, templateID, settingsHolderAttributeID, settingsType, tripleList);
  }
}
