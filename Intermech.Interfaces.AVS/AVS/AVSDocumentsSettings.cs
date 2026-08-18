// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AVSDocumentsSettings
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Общие настройки конструкторских документов</summary>
public class AVSDocumentsSettings : IWriteReadXml
{
  public static AVSDocumentsSettings Instance = new AVSDocumentsSettings();
  [Obsolete("Устарел. Необходимо использовать вместо него documentTemplatesForTypes")]
  private Dictionary<AVSDocumentType, Dictionary<AVSDocumentForm, Guid>> documentTemplates = new Dictionary<AVSDocumentType, Dictionary<AVSDocumentForm, Guid>>();
  /// <summary>Словарь шаблонов. Ключ - внутренний Guid типа конструкторского документа, значение - словарь шаблонов для форм документа
  /// Вложенный словарь: код формы документа - Guid шаблона. -1 - общий шаблон</summary>
  private Dictionary<Guid, Dictionary<int, Guid>> documentTemplatesForTypes = new Dictionary<Guid, Dictionary<int, Guid>>();
  private List<AVSDocumentTypeSettings> avsDocumentTypes;
  private bool loaded;

  [Obsolete("Устарел. Необходимо использовать вместо него documentTemplatesForTypes")]
  private static Dictionary<AVSDocumentType, Dictionary<AVSDocumentForm, Guid>> DocumentTemplates
  {
    get => AVSDocumentsSettings.Instance.documentTemplates;
  }

  /// <summary>Получить документы</summary>
  /// <param name="userSession">Сессия</param>
  /// <returns></returns>
  public static List<AVSDocumentTypeSettings> GetAvsDocumentTypes(IUserSession userSession)
  {
    if (!AVSDocumentsSettings.Instance.loaded)
      AVSDocumentsSettings.Instance.LoadFromDB(userSession);
    return AVSDocumentsSettings.Instance.AvsDocumentTypes;
  }

  /// <summary>Добавить тип конструкторских документов</summary>
  /// <param name="settings">Настройки типа конструкторских документов</param>
  public static void AddAVSDocumentTypeSettings(AVSDocumentTypeSettings settings)
  {
    AVSDocumentsSettings.Instance.AvsDocumentTypes.Add(settings);
  }

  /// <summary>Удалить тип конструкторских документов</summary>
  /// <param name="settings">Настройки типа конструкторских документов</param>
  public static void RemoveAVSDocumentTypeSettings(AVSDocumentTypeSettings settings)
  {
    AVSDocumentsSettings.Instance.AvsDocumentTypes.Remove(settings);
    if (!AVSDocumentsSettings.Instance.documentTemplatesForTypes.ContainsKey(settings.TypeGuid))
      return;
    AVSDocumentsSettings.Instance.documentTemplatesForTypes.Remove(settings.TypeGuid);
  }

  /// <summary>Список типов конструкторских документов</summary>
  public List<AVSDocumentTypeSettings> AvsDocumentTypes
  {
    get
    {
      if (this.avsDocumentTypes == null)
        this.SetDefaultTemplateSettings(false, (IUserSession) null);
      return this.avsDocumentTypes;
    }
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public static object EmptyConstructor() => (object) new AVSDocumentsSettings();

  /// <summary>Получить список допустимых групповых форм для конструкторских документов</summary>
  /// <param name="docType">Тип конструкторского документа</param>
  /// <returns></returns>
  public static AVSDocumentForm[] GetAllowableDocumentForm(AVSDocumentType docType)
  {
    switch (docType)
    {
      case AVSDocumentType.Specification:
        return new AVSDocumentForm[4]
        {
          AVSDocumentForm.Single,
          AVSDocumentForm.A,
          AVSDocumentForm.B,
          AVSDocumentForm.V
        };
      case AVSDocumentType.AutoIndustrySpecification:
        return new AVSDocumentForm[4]
        {
          AVSDocumentForm.Single,
          AVSDocumentForm.A,
          AVSDocumentForm.B,
          AVSDocumentForm.Mirror
        };
      case AVSDocumentType.ExportSpecification:
        return new AVSDocumentForm[2]
        {
          AVSDocumentForm.Single,
          AVSDocumentForm.A
        };
      case AVSDocumentType.ElementList:
      case AVSDocumentType.UserElementList:
        return new AVSDocumentForm[2]
        {
          AVSDocumentForm.Single,
          AVSDocumentForm.A
        };
      case AVSDocumentType.UserAVSDocument:
        return new AVSDocumentForm[1];
      case AVSDocumentType.Vedomost:
        return new AVSDocumentForm[1];
      case AVSDocumentType.UserSpecification:
        return new AVSDocumentForm[3]
        {
          AVSDocumentForm.Single,
          AVSDocumentForm.A,
          AVSDocumentForm.B
        };
      default:
        return new AVSDocumentForm[1];
    }
  }

  /// <summary>Получить список допустимых групповых форм для конструкторских документов</summary>
  /// <param name="docType">Тип конструкторского документа</param>
  /// <param name="docForm">Форма конструкторского документа</param>
  /// <returns></returns>
  public static bool IsAllowableDocumentForm(AVSDocumentType docType, AVSDocumentForm docForm)
  {
    foreach (AVSDocumentForm avsDocumentForm in AVSDocumentsSettings.GetAllowableDocumentForm(docType))
    {
      if (avsDocumentForm == docForm)
        return true;
    }
    return false;
  }

  /// <summary>Получить идентификатор стандартного шаблона спецификации</summary>
  /// <param name="avsDocType">Тип конструкторского документа</param>
  /// <param name="specForm">Форма конструкторского документа. Если null, то возвращает общий шаблон</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="failIfNotFound">Выдать исключение, если шаблон не найден</param>
  /// <returns>Ид шаблона</returns>
  public long GetTemplate(
    AVSDocumentType avsDocType,
    AVSDocumentForm? docForm,
    out Guid templateGuid,
    IUserSession userSession,
    bool failIfNotFound)
  {
    return this.GetTemplate(AVSDocumentTypeSettings.GetStdDocTypeGuid(avsDocType), docForm, out templateGuid, userSession, failIfNotFound);
  }

  /// <summary>Получить идентификатор стандартного шаблона спецификации</summary>
  /// <param name="avsDocType">Внутренний Guid типа конструкторского документа</param>
  /// <param name="specForm">Форма конструкторского документа. Если null, то возвращает общий шаблон</param>
  /// <param name="templateGuid">Возвращает глобальный ид шаблона</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="failIfNotFound">Выдать исключение, если шаблон не найден</param>
  /// <returns>Ид шаблона</returns>
  public long GetTemplate(
    Guid avsDocTypeGuid,
    AVSDocumentForm? docForm,
    out Guid templateGuid,
    IUserSession userSession,
    bool failIfNotFound)
  {
    templateGuid = Guid.Empty;
    if (!this.loaded)
      this.LoadFromDB(userSession);
    Dictionary<int, Guid> dictionary;
    if (this.documentTemplatesForTypes != null && this.documentTemplatesForTypes.TryGetValue(avsDocTypeGuid, out dictionary) && dictionary != null)
    {
      int key = -1;
      if (docForm.HasValue)
        key = (int) docForm.Value;
      if (dictionary.TryGetValue(key, out templateGuid) && templateGuid != Guid.Empty)
      {
        IDBObject dbObject = userSession.GetObject(templateGuid, false);
        if (dbObject != null)
          return dbObject.ObjectID;
        if (failIfNotFound)
          throw new Exception($"Не найден шаблон для документа типа: \"{this.GetAVSDocumentTypeSettings(avsDocTypeGuid).TypeName}\" {{{templateGuid.ToString()}}}");
      }
    }
    return AvsIDCache.GetStdTemplateId(userSession, avsDocTypeGuid, docForm, out templateGuid, failIfNotFound);
  }

  /// <summary>Назначить новый шаблон конструкторского документа</summary>
  /// <param name="avsDocType">Внутренний Guid типа конструкторского документа</param>
  /// <param name="specForm">Форма конструкторского документа. Если null, то возвращает общий шаблон</param>
  /// <param name="templateGuid">Глобальный идентификатор шаблона</param>
  /// <param name="saveToDB">Сохранение новых настроек в базу</param>
  /// <param name="userSession">Пользовательская сессия</param>
  public void SetTemplate(
    Guid avsDocType,
    AVSDocumentForm? docForm,
    Guid templateGuid,
    bool saveToDB,
    IUserSession userSession)
  {
    if (!this.loaded && userSession != null)
      this.LoadFromDB(userSession);
    Dictionary<int, Guid> dictionary;
    if (!this.documentTemplatesForTypes.TryGetValue(avsDocType, out dictionary))
      this.documentTemplatesForTypes.Add(avsDocType, dictionary = new Dictionary<int, Guid>());
    else if (dictionary == null)
      this.documentTemplatesForTypes[avsDocType] = dictionary = new Dictionary<int, Guid>();
    int key = -1;
    if (docForm.HasValue)
      key = (int) docForm.Value;
    if (!dictionary.ContainsKey(key))
      dictionary.Add(key, templateGuid);
    else
      dictionary[key] = templateGuid;
    if (!saveToDB || userSession == null)
      return;
    this.saveSettingsToDB(userSession);
  }

  /// <summary>Получить настройки типа документов AVS</summary>
  /// <param name="avsDocTypeGuid">Внутренний Guid типа конструкторского документа</param>
  public AVSDocumentTypeSettings GetAVSDocumentTypeSettings(Guid avsDocTypeGuid)
  {
    for (int index = 0; index < this.AvsDocumentTypes.Count; ++index)
    {
      if (this.AvsDocumentTypes[index].TypeGuid == avsDocTypeGuid)
        return this.AvsDocumentTypes[index];
    }
    return (AVSDocumentTypeSettings) null;
  }

  /// <summary>Получить типов документов AVS по умолчанию, для занного типа объекта БД</summary>
  /// <param name="dbObjectTypeID">Идентификатор типа объекта БД</param>
  /// <param name="defaultSpecificationType">Тип спецификации по умолчанию, заданный в настройках</param>
  public AVSDocumentTypeSettings GetDefaultDocumentTypeForDBObjectType(
    int dbObjectTypeID,
    AVSDocumentType defaultSpecificationType)
  {
    return dbObjectTypeID != -1 ? this.GetDefaultDocumentTypeForDBObjectType(MetaDataHelper.GetObjectTypeGuid(dbObjectTypeID), defaultSpecificationType) : this.GetDefaultDocumentTypeForDBObjectType(Guid.Empty, defaultSpecificationType);
  }

  /// <summary>Получить настройки типа документов AVS по умолчанию, для занного типа объекта БД</summary>
  /// <param name="dbObjectType">Глобальный идентификатор типа объекта БД</param>
  /// <param name="defaultAvsDocType">Тип спецификации по умолчанию, заданный в настройках</param>
  public AVSDocumentTypeSettings GetDefaultDocumentTypeForDBObjectType(
    Guid dbObjectType,
    AVSDocumentType defaultAvsDocType)
  {
    AVSDocumentTypeSettings documentTypeSettings = (AVSDocumentTypeSettings) null;
    AVSDocumentTypeSettings typeForDbObjectType1 = (AVSDocumentTypeSettings) null;
    AVSDocumentTypeSettings typeForDbObjectType2 = (AVSDocumentTypeSettings) null;
    if (dbObjectType == Guid.Empty)
      return (AVSDocumentTypeSettings) null;
    List<AVSDocumentTypeSettings> typesForDbObjectType = this.GetAVSDocumentTypesForDBObjectType(dbObjectType);
    for (int index = 0; index < typesForDbObjectType.Count; ++index)
    {
      if (documentTypeSettings == null && typesForDbObjectType[index].DBObjectTypeList != null && typesForDbObjectType[index].DBObjectTypeList.Contains(dbObjectType))
        documentTypeSettings = typesForDbObjectType[index];
      if (typesForDbObjectType[index].AVSDocType == defaultAvsDocType)
        typeForDbObjectType1 = typesForDbObjectType[index];
      else if (typesForDbObjectType[index].AVSDocType == AVSDocumentType.ElementList)
        typeForDbObjectType2 = typesForDbObjectType[index];
    }
    if (typeForDbObjectType1?.DBObjectTypeList != null && typeForDbObjectType1.DBObjectTypeList.Contains(dbObjectType))
      return typeForDbObjectType1;
    if (typeForDbObjectType2?.DBObjectTypeList != null && typeForDbObjectType2.DBObjectTypeList.Contains(dbObjectType))
      return typeForDbObjectType2;
    AVSDocumentTypeSettings typeForDbObjectType3 = documentTypeSettings;
    if (typeForDbObjectType3 != null)
      return typeForDbObjectType3;
    return typesForDbObjectType == null ? (AVSDocumentTypeSettings) null : typesForDbObjectType.FirstOrDefault<AVSDocumentTypeSettings>();
  }

  /// <summary>Получить настройки типа документов AVS, для занного шаблона</summary>
  /// <param name="templateGuid">Глобальный идентификатор версии объекта шаблона</param>
  /// <param name="inheritanceLevel">Уровень шаблона в иерархии наследования настроек</param>
  public AVSDocumentTypeSettings GetDocumentTypeSettingsForTemplate(
    Guid templateGuid,
    out InheritanceSettingsLevel inheritanceLevel)
  {
    inheritanceLevel = InheritanceSettingsLevel.Template;
    Guid avsDocTypeGuid = Guid.Empty;
    Guid guid = Guid.Empty;
    Guid empty = Guid.Empty;
    if (templateGuid != Guid.Empty)
    {
      foreach (KeyValuePair<Guid, Dictionary<int, Guid>> templatesForType in AVSDocumentsSettings.Instance.documentTemplatesForTypes)
      {
        foreach (KeyValuePair<int, Guid> keyValuePair in templatesForType.Value)
        {
          if (keyValuePair.Value == templateGuid)
          {
            if (keyValuePair.Key == -1)
            {
              guid = templatesForType.Key;
              inheritanceLevel = InheritanceSettingsLevel.CommonTemplate;
              break;
            }
            avsDocTypeGuid = templatesForType.Key;
            inheritanceLevel = InheritanceSettingsLevel.Template;
            break;
          }
        }
        if (avsDocTypeGuid != Guid.Empty)
          break;
      }
      if (avsDocTypeGuid == Guid.Empty)
      {
        avsDocTypeGuid = guid;
        inheritanceLevel = InheritanceSettingsLevel.CommonTemplate;
      }
      if (avsDocTypeGuid == Guid.Empty)
      {
        if (templateGuid == AvsIDCache.StdTemplateSingleSpecification || templateGuid == AvsIDCache.StdTemplateSpecificationFormB || templateGuid == AvsIDCache.StdTemplateSpecificationFormV)
        {
          avsDocTypeGuid = AvsIDCache.AVSDocTypeGuid_Specification;
          inheritanceLevel = InheritanceSettingsLevel.Template;
        }
        else if (templateGuid == AvsIDCache.StdTemplateSingleAutopromSpecification || templateGuid == AvsIDCache.StdTemplateAutopromSpecificationFormB || templateGuid == AvsIDCache.StdTemplateMirrorSpecification)
        {
          avsDocTypeGuid = AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification;
          inheritanceLevel = InheritanceSettingsLevel.Template;
        }
        else if (templateGuid == AvsIDCache.StdTemplateExportSpecification)
        {
          avsDocTypeGuid = AvsIDCache.AVSDocTypeGuid_ExportSpecification;
          inheritanceLevel = InheritanceSettingsLevel.Template;
        }
        else if (templateGuid == AvsIDCache.StdTemplateCommonSpecification)
        {
          avsDocTypeGuid = AvsIDCache.AVSDocTypeGuid_Specification;
          inheritanceLevel = InheritanceSettingsLevel.CommonTemplate;
        }
        else if (templateGuid == AvsIDCache.StdTemplateElementList)
        {
          avsDocTypeGuid = AvsIDCache.AVSDocTypeGuid_ElementList;
          inheritanceLevel = InheritanceSettingsLevel.Template;
        }
      }
      if (avsDocTypeGuid != Guid.Empty)
        return AVSDocumentsSettings.Instance.GetAVSDocumentTypeSettings(avsDocTypeGuid);
    }
    return (AVSDocumentTypeSettings) null;
  }

  /// <summary>Получить список типов документов AVS связанных с данным типом объекта БД</summary>
  /// <param name="dbObjectType">Глобальный идентификатор типа объекта БД</param>
  public List<AVSDocumentTypeSettings> GetAVSDocumentTypesForDBObjectType(Guid dbObjectType)
  {
    List<AVSDocumentTypeSettings> typesForDbObjectType = new List<AVSDocumentTypeSettings>();
    if (dbObjectType == Guid.Empty)
      return typesForDbObjectType;
    for (int index1 = 0; index1 < this.AvsDocumentTypes.Count; ++index1)
    {
      if (this.AvsDocumentTypes[index1].DBObjectTypeList != null)
      {
        for (int index2 = 0; index2 < this.AvsDocumentTypes[index1].DBObjectTypeList.Count; ++index2)
        {
          if (MetaDataHelper.IsObjectTypeChildOf(dbObjectType, this.AvsDocumentTypes[index1].DBObjectTypeList[index2]))
          {
            typesForDbObjectType.Add(this.AvsDocumentTypes[index1]);
            break;
          }
        }
      }
    }
    return typesForDbObjectType;
  }

  /// <summary>Поддерживается ли тип объекта БД как документ AVS</summary>
  /// <param name="documentType">Идентификатор типа объекта БД</param>
  public static List<Guid> GetObjectTypeGuidsForAVSDocumentType(Guid avsDocumentTypeGuid)
  {
    AVSDocumentTypeSettings documentTypeSettings = AVSDocumentsSettings.Instance.GetAVSDocumentTypeSettings(avsDocumentTypeGuid);
    return documentTypeSettings != null ? new List<Guid>((IEnumerable<Guid>) documentTypeSettings.DBObjectTypeList) : new List<Guid>();
  }

  /// <summary>Поддерживается ли тип объекта БД как документ AVS</summary>
  /// <param name="dbObjectTypeID">Идентификатор типа объекта БД</param>
  public bool IsAVSDocumentSupportedType(int dbObjectTypeID)
  {
    Guid dbObjectTypeGuid = !Consts.IsUndefinedObjectId((long) dbObjectTypeID) ? MetaDataHelper.GetObjectTypeGuid(dbObjectTypeID) : throw new ArgumentException("Недопустимое значение аргумента " + (object) dbObjectTypeID, nameof (dbObjectTypeID));
    return !(dbObjectTypeGuid == Guid.Empty) && this.AvsDocumentTypes.Any<AVSDocumentTypeSettings>((Func<AVSDocumentTypeSettings, bool>) (docType => docType.DBObjectTypeList != null && docType.DBObjectTypeList.Any<Guid>((Func<Guid, bool>) (dbType => MetaDataHelper.IsObjectTypeChildOf(dbObjectTypeGuid, dbType)))));
  }

  /// <summary>
  /// Данный тип является родительским для какого-либо перечня элементов
  /// </summary>
  /// <param name="dbObjectTypeID">Идентификатор типа объекта БД</param>
  /// <returns></returns>
  public bool IsAVSElementListParentType(int dbObjectTypeID)
  {
    Guid dbObjectTypeGuid = !Consts.IsUndefinedObjectId((long) dbObjectTypeID) ? MetaDataHelper.GetObjectTypeGuid(dbObjectTypeID) : throw new ArgumentException("Недопустимое значение аргумента " + (object) dbObjectTypeID, nameof (dbObjectTypeID));
    return !(dbObjectTypeGuid == Guid.Empty) && AVSDocumentsSettings.GetObjectTypeGuidsForAVSDocumentType(AvsIDCache.AVSDocTypeGuid_ElementList).Any<Guid>((Func<Guid, bool>) (dbType => MetaDataHelper.IsObjectTypeChildOf(dbType, dbObjectTypeGuid)));
  }

  /// <summary>Поддерживается ли тип объекта БД как документ AVS</summary>
  /// <param name="dbObjectTypeID">Идентификатор типа объекта БД</param>
  public bool IsAVSElementList(int dbObjectTypeID)
  {
    Guid dbObjectTypeGuid = !Consts.IsUndefinedObjectId((long) dbObjectTypeID) ? MetaDataHelper.GetObjectTypeGuid(dbObjectTypeID) : throw new ArgumentException("Недопустимое значение аргумента " + (object) dbObjectTypeID, nameof (dbObjectTypeID));
    return !(dbObjectTypeGuid == Guid.Empty) && AVSDocumentsSettings.GetObjectTypeGuidsForAVSDocumentType(AvsIDCache.AVSDocTypeGuid_ElementList).Any<Guid>((Func<Guid, bool>) (dbType => MetaDataHelper.IsObjectTypeChildOf(dbObjectTypeGuid, dbType)));
  }

  /// <summary>Получить список типов документов AVS связанных с данным типом объекта БД</summary>
  /// <param name="dbObjectTypeID">Идентификатор типа объекта БД</param>
  public List<AVSDocumentTypeSettings> GetAVSDocumentTypesForDBObjectType(int dbObjectTypeID)
  {
    return dbObjectTypeID != -1 ? this.GetAVSDocumentTypesForDBObjectType(MetaDataHelper.GetObjectTypeGuid(dbObjectTypeID)) : this.GetAVSDocumentTypesForDBObjectType(Guid.Empty);
  }

  /// <summary>Получить список типов документов AVS</summary>
  /// <returns></returns>
  public List<Guid> GetDBObjectTypesForAllAVSDocuments(IUserSession userSession)
  {
    List<Guid> forAllAvsDocuments = new List<Guid>();
    foreach (AVSDocumentTypeSettings avsDocumentType in AVSDocumentsSettings.GetAvsDocumentTypes(userSession))
    {
      foreach (Guid dbObjectType in avsDocumentType.DBObjectTypeList)
      {
        if (!forAllAvsDocuments.Contains(dbObjectType))
          forAllAvsDocuments.Add(dbObjectType);
      }
    }
    return forAllAvsDocuments;
  }

  /// <summary>Данный объект является шаблоном спецификации</summary>
  /// <returns></returns>
  public bool IsSpecificationTemplate(Guid templateGuid)
  {
    if (templateGuid == Guid.Empty)
      return false;
    AVSDocumentTypeSettings settingsForTemplate = this.GetDocumentTypeSettingsForTemplate(templateGuid, out InheritanceSettingsLevel _);
    return settingsForTemplate != null && AVSDocumentsSettings.IsSpecificationDocType(settingsForTemplate.AVSDocType);
  }

  /// <summary>Тип документа относится к спецификациям</summary>
  /// <param name="avsDocType">Тип конструкторского документа</param>
  public static bool IsSpecificationDocType(AVSDocumentType avsDocType)
  {
    return avsDocType == AVSDocumentType.Specification || avsDocType == AVSDocumentType.AutoIndustrySpecification || avsDocType == AVSDocumentType.ExportSpecification || avsDocType == AVSDocumentType.UserSpecification;
  }

  /// <summary>Данный объект является шаблоном перечня элементов</summary>
  /// <returns></returns>
  public bool IsElementListTemplate(Guid templateGuid)
  {
    if (templateGuid == Guid.Empty)
      return false;
    AVSDocumentTypeSettings settingsForTemplate = this.GetDocumentTypeSettingsForTemplate(templateGuid, out InheritanceSettingsLevel _);
    return settingsForTemplate != null && AVSDocumentsSettings.IsElementListDocType(settingsForTemplate.AVSDocType);
  }

  /// <summary>Тип документа относится к Перечням элементов</summary>
  /// <param name="avsDocType">Тип конструкторского документа</param>
  public static bool IsElementListDocType(AVSDocumentType avsDocType)
  {
    return avsDocType == AVSDocumentType.ElementList || avsDocType == AVSDocumentType.UserElementList;
  }

  /// <summary>Получить родительский шаблон для заданного шаблона. Если не найден, то возвращает Guid.Empty</summary>
  /// <param name="template">Глобальный идентификатор версии объекта БД шаблона документа</param>
  /// <param name="templateLevel">Уровень наследования шаблона</param>
  /// <returns></returns>
  public static Guid GetParentTemplate(Guid template)
  {
    Guid empty = Guid.Empty;
    if (template != Guid.Empty)
    {
      foreach (KeyValuePair<Guid, Dictionary<int, Guid>> templatesForType in AVSDocumentsSettings.Instance.documentTemplatesForTypes)
      {
        foreach (KeyValuePair<int, Guid> keyValuePair in templatesForType.Value)
        {
          if (keyValuePair.Value == template)
          {
            if (keyValuePair.Key == -1)
              return Guid.Empty;
            templatesForType.Value.TryGetValue(-1, out empty);
            return empty;
          }
        }
      }
      if (empty == Guid.Empty && (template == AvsIDCache.StdTemplateSingleSpecification || template == AvsIDCache.StdTemplateSpecificationFormB || template == AvsIDCache.StdTemplateSpecificationFormV || template == AvsIDCache.StdTemplateSingleAutopromSpecification || template == AvsIDCache.StdTemplateAutopromSpecificationFormB || template == AvsIDCache.StdTemplateMirrorSpecification || template == AvsIDCache.StdTemplateExportSpecification))
        return AvsIDCache.StdTemplateCommonSpecification;
    }
    return empty;
  }

  /// <summary>Получить уровень наследования настроек</summary>
  /// <param name="settingsDBObjectGuid">Глобальный идентификатор версии объекта БД шаблона документа</param>
  /// <returns></returns>
  public static InheritanceSettingsLevel GetSettingsLevel(
    Guid settingsDBObjectGuid,
    int settingsObjectType)
  {
    if (MetaDataHelper.IsObjectTypeChildOf(settingsObjectType, AvsIDCache.ObjType_Document))
      return InheritanceSettingsLevel.Document;
    bool flag = false;
    if (!(settingsDBObjectGuid != Guid.Empty))
      return InheritanceSettingsLevel.Template;
    foreach (KeyValuePair<Guid, Dictionary<int, Guid>> templatesForType in AVSDocumentsSettings.Instance.documentTemplatesForTypes)
    {
      foreach (KeyValuePair<int, Guid> keyValuePair in templatesForType.Value)
      {
        if (keyValuePair.Value == settingsDBObjectGuid)
        {
          if (keyValuePair.Key != -1)
            return InheritanceSettingsLevel.Template;
          flag = true;
        }
      }
    }
    if (flag)
      return InheritanceSettingsLevel.CommonTemplate;
    if (!(settingsDBObjectGuid == AvsIDCache.StdTemplateSingleSpecification) && !(settingsDBObjectGuid == AvsIDCache.StdTemplateSpecificationFormB) && !(settingsDBObjectGuid == AvsIDCache.StdTemplateSpecificationFormV) && !(settingsDBObjectGuid == AvsIDCache.StdTemplateSingleAutopromSpecification) && !(settingsDBObjectGuid == AvsIDCache.StdTemplateAutopromSpecificationFormB) && !(settingsDBObjectGuid == AvsIDCache.StdTemplateMirrorSpecification))
    {
      int num = settingsDBObjectGuid == AvsIDCache.StdTemplateExportSpecification ? 1 : 0;
    }
    return InheritanceSettingsLevel.Template;
  }

  /// <summary>
  /// Получить идентификатор типа шаблона и код формы в словаре типов документов по id шаблона
  /// </summary>
  public IEnumerable<(Guid docType, AVSDocumentForm docForm)> FindTypeAndFormForTemplate(
    long templateId,
    IUserSession userSession,
    bool failIfNotFound)
  {
    if (!this.loaded)
      this.LoadFromDB(userSession);
    IDBObject dbObject = userSession.GetObject(templateId, false);
    if (dbObject != null)
    {
      Guid templateGuid = dbObject.ObjectGUID;
      if (this.documentTemplatesForTypes != null)
      {
        foreach (KeyValuePair<Guid, Dictionary<int, Guid>> templatesForType in this.documentTemplatesForTypes)
        {
          KeyValuePair<Guid, Dictionary<int, Guid>> docFormsForType = templatesForType;
          foreach (KeyValuePair<int, Guid> keyValuePair in docFormsForType.Value)
          {
            if (keyValuePair.Value == templateGuid)
              yield return (docFormsForType.Key, (AVSDocumentForm) keyValuePair.Key);
          }
          docFormsForType = new KeyValuePair<Guid, Dictionary<int, Guid>>();
        }
      }
      templateGuid = new Guid();
    }
  }

  /// <summary>Сохранить настройки в объект базы</summary>
  /// <param name="userSession">Пользовательская сессия</param>
  public static void SaveSettingsToDB(IUserSession userSession)
  {
    AVSDocumentsSettings.Instance.saveSettingsToDB(userSession);
  }

  /// <summary>Сохранить настройки в объект базы</summary>
  /// <param name="userSession">Пользовательская сессия</param>
  private void saveSettingsToDB(IUserSession userSession)
  {
    MemoryStream memoryStream = new MemoryStream();
    try
    {
      this.SaveToXml((Stream) memoryStream);
      long length = memoryStream.Length;
      memoryStream.Position = 0L;
      byte[] buffer1 = memoryStream.GetBuffer();
      byte[] buffer2 = new byte[length];
      byte[] dst = buffer2;
      int count = (int) length;
      Buffer.BlockCopy((Array) buffer1, 0, (Array) dst, 0, count);
      MemoryStream baseOutputStream = new MemoryStream();
      try
      {
        DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream((Stream) baseOutputStream);
        try
        {
          deflaterOutputStream.Write(buffer2, 0, buffer2.Length);
        }
        finally
        {
          deflaterOutputStream.Finish();
        }
        BlobInformation config_info = new BlobInformation(length, baseOutputStream.Length, DateTime.Now, nameof (AVSDocumentsSettings), ArcMethods.ZLibPacked, string.Empty);
        userSession.Configurations.WriteConfigData(config_info, baseOutputStream.ToArray(), 0L);
      }
      finally
      {
        baseOutputStream.Close();
      }
    }
    finally
    {
      memoryStream.Close();
    }
  }

  /// <summary>Загрузить данные из БД</summary>
  /// <param name="userSession">Пользовательская сессия</param>
  public void LoadFromDB(IUserSession userSession)
  {
    if (AVSDocumentsSettings.Instance.loaded)
      return;
    AVSDocumentsSettings.Instance.loaded = true;
    AVSDocumentsSettings.DocumentTemplates.Clear();
    this.documentTemplatesForTypes.Clear();
    MemoryStream inStream = (MemoryStream) null;
    IDBConfigurations configurations = userSession.Configurations;
    long num = 0;
    BlobInformation blobInformation1;
    ref BlobInformation local1 = ref blobInformation1;
    byte[] buffer;
    ref byte[] local2 = ref buffer;
    long userID = num;
    configurations.LoadConfigData(nameof (AVSDocumentsSettings), out local1, out local2, userID);
    BlobInformation blobInformation2 = blobInformation1;
    if (blobInformation2.RealFileSize > 0L)
    {
      try
      {
        if (buffer.Length != 0)
        {
          inStream = new MemoryStream(buffer);
          if (inStream.Length > 0L)
          {
            inStream.Seek(0L, SeekOrigin.Begin);
            inStream.Write(buffer, 0, buffer.Length);
            inStream.Seek(0L, SeekOrigin.Begin);
            if (blobInformation2.ArcMethod == ArcMethods.ZLibPacked)
            {
              MemoryStream outStream = new MemoryStream();
              ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) outStream);
              inStream = outStream;
              inStream.Seek(0L, SeekOrigin.Begin);
            }
            WriteReadXmlHelper.LoadFromXmlDocument(userSession, (Stream) inStream, (IWriteReadXml) this, nameof (AVSDocumentsSettings));
          }
        }
      }
      finally
      {
        inStream?.Close();
      }
    }
    AVSDocumentsSettings.Instance.loaded = true;
    if (AVSDocumentsSettings.DocumentTemplates != null)
      AVSDocumentsSettings.DocumentTemplates.Clear();
    this.SetDefaultTemplateSettings(false, userSession);
  }

  /// <summary>Получить настройки по умолчанию</summary>
  public void SetDefaultTemplateSettings(bool overrideSettings, IUserSession userSession)
  {
    if (overrideSettings || this.avsDocumentTypes == null)
      this.avsDocumentTypes = new List<AVSDocumentTypeSettings>();
    if (this.FindAVSDocumentTypeSettings(AvsIDCache.AVSDocTypeGuid_Specification) == null)
      this.avsDocumentTypes.Add(AVSDocumentTypeSettings.GetDefaultAVSDocumentTypeSettings(AVSDocumentType.Specification));
    if (this.FindAVSDocumentTypeSettings(AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification) == null)
      this.avsDocumentTypes.Add(AVSDocumentTypeSettings.GetDefaultAVSDocumentTypeSettings(AVSDocumentType.AutoIndustrySpecification));
    if (this.FindAVSDocumentTypeSettings(AvsIDCache.AVSDocTypeGuid_ExportSpecification) == null)
      this.avsDocumentTypes.Add(AVSDocumentTypeSettings.GetDefaultAVSDocumentTypeSettings(AVSDocumentType.ExportSpecification));
    if (this.FindAVSDocumentTypeSettings(AvsIDCache.AVSDocTypeGuid_ElementList) == null)
      this.avsDocumentTypes.Add(AVSDocumentTypeSettings.GetDefaultAVSDocumentTypeSettings(AVSDocumentType.ElementList));
    Dictionary<int, Guid> dictionary;
    if (!this.documentTemplatesForTypes.TryGetValue(AvsIDCache.AVSDocTypeGuid_Specification, out dictionary))
      this.documentTemplatesForTypes.Add(AvsIDCache.AVSDocTypeGuid_Specification, dictionary = new Dictionary<int, Guid>());
    if (overrideSettings || !dictionary.ContainsKey(-1))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_Specification, new AVSDocumentForm?(), AvsIDCache.StdTemplateCommonSpecification, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(0))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_Specification, new AVSDocumentForm?(AVSDocumentForm.Single), AvsIDCache.StdTemplateSingleSpecification, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(1))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_Specification, new AVSDocumentForm?(AVSDocumentForm.A), AvsIDCache.StdTemplateSingleSpecification, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(2))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_Specification, new AVSDocumentForm?(AVSDocumentForm.B), AvsIDCache.StdTemplateSpecificationFormB, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(4))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_Specification, new AVSDocumentForm?(AVSDocumentForm.V), AvsIDCache.StdTemplateSpecificationFormV, false, userSession);
    if (!this.documentTemplatesForTypes.TryGetValue(AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification, out dictionary))
      this.documentTemplatesForTypes.Add(AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification, dictionary = new Dictionary<int, Guid>());
    if (overrideSettings || !dictionary.ContainsKey(-1))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification, new AVSDocumentForm?(), AvsIDCache.StdTemplateCommonSpecification, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(0))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification, new AVSDocumentForm?(AVSDocumentForm.Single), AvsIDCache.StdTemplateSingleAutopromSpecification, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(1))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification, new AVSDocumentForm?(AVSDocumentForm.A), AvsIDCache.StdTemplateSingleAutopromSpecification, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(2))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification, new AVSDocumentForm?(AVSDocumentForm.B), AvsIDCache.StdTemplateAutopromSpecificationFormB, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(3))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_AutoIndustrySpecification, new AVSDocumentForm?(AVSDocumentForm.Mirror), AvsIDCache.StdTemplateMirrorSpecification, false, userSession);
    if (!this.documentTemplatesForTypes.TryGetValue(AvsIDCache.AVSDocTypeGuid_ExportSpecification, out dictionary))
      this.documentTemplatesForTypes.Add(AvsIDCache.AVSDocTypeGuid_ExportSpecification, dictionary = new Dictionary<int, Guid>());
    if (overrideSettings || !dictionary.ContainsKey(-1))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_ExportSpecification, new AVSDocumentForm?(), AvsIDCache.StdTemplateCommonSpecification, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(0))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_ExportSpecification, new AVSDocumentForm?(AVSDocumentForm.Single), AvsIDCache.StdTemplateExportSpecification, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(1))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_ExportSpecification, new AVSDocumentForm?(AVSDocumentForm.A), AvsIDCache.StdTemplateExportSpecification, false, userSession);
    if (!this.documentTemplatesForTypes.TryGetValue(AvsIDCache.AVSDocTypeGuid_ElementList, out dictionary))
      this.documentTemplatesForTypes.Add(AvsIDCache.AVSDocTypeGuid_ElementList, dictionary = new Dictionary<int, Guid>());
    if (overrideSettings || !dictionary.ContainsKey(-1))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_ElementList, new AVSDocumentForm?(), AvsIDCache.StdTemplateElementList, false, userSession);
    if (overrideSettings || !dictionary.ContainsKey(0))
      this.SetTemplate(AvsIDCache.AVSDocTypeGuid_ElementList, new AVSDocumentForm?(AVSDocumentForm.Single), AvsIDCache.StdTemplateElementList, false, userSession);
    if (!overrideSettings && dictionary.ContainsKey(1))
      return;
    this.SetTemplate(AvsIDCache.AVSDocTypeGuid_ElementList, new AVSDocumentForm?(AVSDocumentForm.A), AvsIDCache.StdTemplateElementList, false, userSession);
  }

  /// <summary>Получить настройки по умолчанию</summary>
  public static List<AVSDocumentTypeSettings> GetDefault()
  {
    return new List<AVSDocumentTypeSettings>()
    {
      AVSDocumentTypeSettings.GetDefaultAVSDocumentTypeSettings(AVSDocumentType.Specification),
      AVSDocumentTypeSettings.GetDefaultAVSDocumentTypeSettings(AVSDocumentType.AutoIndustrySpecification),
      AVSDocumentTypeSettings.GetDefaultAVSDocumentTypeSettings(AVSDocumentType.ExportSpecification),
      AVSDocumentTypeSettings.GetDefaultAVSDocumentTypeSettings(AVSDocumentType.ElementList)
    };
  }

  /// <summary>Найти настройки для заданного типа конструкторского документа. Не путать с типом объекта в БД!</summary>
  /// <param name="avsDocumentTypeGuid">Внутренний идентификатор типа документа. Не путать с типом объекта в БД</param>
  /// <returns></returns>
  public AVSDocumentTypeSettings FindAVSDocumentTypeSettings(Guid avsDocumentTypeGuid)
  {
    return avsDocumentTypeGuid != Guid.Empty ? this.AvsDocumentTypes.FirstOrDefault<AVSDocumentTypeSettings>((Func<AVSDocumentTypeSettings, bool>) (s => s.TypeGuid == avsDocumentTypeGuid)) : (AVSDocumentTypeSettings) null;
  }

  /// <summary>Найти настройки для заданного типа конструкторского документа</summary>
  /// <param name="avsDocumentTypeGuid">Внутренний идентификатор типа документа</param>
  /// <returns></returns>
  public AVSDocumentTypeSettings FindAVSDocumentTypeSettings(AVSDocumentType docType)
  {
    for (int index = 0; index < this.AvsDocumentTypes.Count; ++index)
    {
      if (this.AvsDocumentTypes[index].AVSDocType == docType)
        return this.AvsDocumentTypes[index];
    }
    return (AVSDocumentTypeSettings) null;
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    if (!this.documentTemplates.IsEmpty<KeyValuePair<AVSDocumentType, Dictionary<AVSDocumentForm, Guid>>>())
      WriteReadXmlHelper.WriteDictionaryToXml("DocumentTemplates", (IDictionary) AVSDocumentsSettings.DocumentTemplates, "AVSDocType", "DocTemplate", (string) null, (IList<Type>) new Type[2]
      {
        typeof (AVSDocumentForm),
        typeof (AVSDocumentType)
      }, (IList<Type>) new Type[2]
      {
        typeof (Guid),
        typeof (Dictionary<AVSDocumentForm, Guid>)
      }, xw, objectRefId);
    if (this.documentTemplatesForTypes != null && this.documentTemplatesForTypes.Count > 0)
      WriteReadXmlHelper.WriteDictionaryToXml("DocumentTemplatesForTypes", (IDictionary) this.documentTemplatesForTypes, "DocType", "DocForms", (string) null, (IList<Type>) new Type[2]
      {
        typeof (int),
        typeof (Guid)
      }, (IList<Type>) new Type[2]
      {
        typeof (Guid),
        typeof (Dictionary<int, Guid>)
      }, xw, objectRefId);
    if (this.avsDocumentTypes != null && this.avsDocumentTypes.Count > 0)
    {
      this.avsDocumentTypes = this.avsDocumentTypes.GroupBy<AVSDocumentTypeSettings, Guid>((Func<AVSDocumentTypeSettings, Guid>) (t => t.TypeGuid)).Select<IGrouping<Guid, AVSDocumentTypeSettings>, AVSDocumentTypeSettings>((Func<IGrouping<Guid, AVSDocumentTypeSettings>, AVSDocumentTypeSettings>) (g => g.First<AVSDocumentTypeSettings>())).ToList<AVSDocumentTypeSettings>();
      WriteReadXmlHelper.WriteListToXml("AVSDocumentTypes", (IList) this.avsDocumentTypes, "DocType", xw, objectRefId);
    }
    xw.WriteEndElement();
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if ("DocumentTemplates" == readArgs.Reader.LocalName)
    {
      if (this.documentTemplates == null)
        this.documentTemplates = new Dictionary<AVSDocumentType, Dictionary<AVSDocumentForm, Guid>>();
      WriteReadXmlHelper.ReadDictionaryFromXml((IDictionary) this.documentTemplates, typeof (AVSDocumentType), typeof (Dictionary<AVSDocumentForm, Guid>), readArgs);
      return true;
    }
    if ("DBDocTypes" == readArgs.Reader.LocalName)
    {
      WriteReadXmlHelper.ReadDictionaryFromXml((IDictionary) new Dictionary<Guid, Dictionary<AVSDocumentType, Dictionary<AVSDocumentForm, Guid>>>(), typeof (Guid), typeof (Dictionary<AVSDocumentType, Dictionary<AVSDocumentForm, Guid>>), readArgs);
      return true;
    }
    if ("DocumentTemplatesForTypes" == readArgs.Reader.LocalName)
    {
      this.documentTemplatesForTypes = new Dictionary<Guid, Dictionary<int, Guid>>();
      WriteReadXmlHelper.ReadDictionaryFromXml((IDictionary) this.documentTemplatesForTypes, typeof (Guid), typeof (Dictionary<int, Guid>), readArgs);
      return true;
    }
    if (!("AVSDocumentTypes" == readArgs.Reader.LocalName))
      return false;
    if (this.avsDocumentTypes == null)
      this.avsDocumentTypes = new List<AVSDocumentTypeSettings>();
    WriteReadXmlHelper.ReadListFromXml((IList) this.avsDocumentTypes, typeof (AVSDocumentTypeSettings), readArgs);
    this.avsDocumentTypes = this.avsDocumentTypes.GroupBy<AVSDocumentTypeSettings, Guid>((Func<AVSDocumentTypeSettings, Guid>) (t => t.TypeGuid)).Select<IGrouping<Guid, AVSDocumentTypeSettings>, AVSDocumentTypeSettings>((Func<IGrouping<Guid, AVSDocumentTypeSettings>, AVSDocumentTypeSettings>) (g => g.First<AVSDocumentTypeSettings>())).ToList<AVSDocumentTypeSettings>();
    return true;
  }

  /// <summary>Создать и загрузить cсылку из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public static AVSDocumentsSettings LoadFromXml(XmlReadArgs readArgs)
  {
    AVSDocumentsSettings documentsSettings = new AVSDocumentsSettings();
    documentsSettings.ReadFromXml(readArgs);
    return documentsSettings;
  }

  /// <summary>Сохранить в XML</summary>
  /// <param name="stream">Поток данных</param>
  public void SaveToXml(Stream stream)
  {
    WriteReadXmlHelper.WriteXmlDocument(stream, (IWriteReadXml) this, nameof (AVSDocumentsSettings));
  }
}
