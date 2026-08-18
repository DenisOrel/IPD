// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeConsts
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Константы</summary>
public sealed class XmlExchangeConsts
{
  /// <summary>
  /// 
  /// </summary>
  public static class Common
  {
    /// <summary>Ид. для выгружаемых "кастом" объектов</summary>
    public static readonly long ExportCustomObjectID = 2000000000;
    /// <summary>
    /// Гл. ид. типа объектов с конфигурациями импорта/экспорта
    /// </summary>
    public static readonly Guid ImportExportSettObjTypeGuid = new Guid("cadd9457-306c-11d8-b4e9-00304f19f545");
    /// <summary>Гл. ид. типа объектов с конфигурациями импорта</summary>
    public static readonly Guid ImportSettObjTypeGuid = new Guid("cadd9458-306c-11d8-b4e9-00304f19f545");
    /// <summary>Гл. ид. типа объектов с конфигурациями экспорта</summary>
    public static readonly Guid ExportSettObjTypeGuid = new Guid("cadd9444-306c-11d8-b4e9-00304f19f545");
    /// <summary>Тип атрибута "Данные"</summary>
    public static readonly Guid DataAttrTypeGuid = new Guid("cad001b2-306c-11d8-b4e9-00304f19f545");
    /// <summary>Имя файла пакета экспорта по умолчанию</summary>
    public static readonly string XmlPacketFileName = "exportdata_{0:yyyyMMddHHmmssfffffff}.zip";
    /// <summary>Имя файла для "сокращенных" метаданных</summary>
    public static readonly string XmlMetaBriedFileName = "MetaDataBrief.xml";
    /// <summary>Имя папки для выгрузки иконок</summary>
    public static readonly string IconFolderName = "Icon";
    /// <summary>Размер пакета входных данных</summary>
    public static readonly int PacketChunkSize = -1;
    /// <summary>
    /// Шаблон для формирования имени директории для "подзадачи"
    /// </summary>
    public static readonly string PacketChunkDirFormat = "Task {0}";
  }

  /// <summary>
  /// 
  /// </summary>
  public static class XML
  {
    /// <summary>Секция в XML для метаданных</summary>
    public static readonly string XmlMetaBriefDataName = "MetaDataBrief".ToUpper();
    /// <summary>Секция в XML для атрибутов</summary>
    public static readonly string XmlAttrDataName = "Attributes".ToUpper();
    /// <summary>Секция в XML для типов атрибутов</summary>
    public static readonly string XmlAttrTypesName = "Attribute_Types".ToUpper();
    /// <summary>Секция в XML для типа атрибута</summary>
    public static readonly string XmlAttrTypeName = "Attribute_Type".ToUpper();
    /// <summary>Секция в XML для типов объектов</summary>
    public static readonly string XmlObjTypesName = "Object_Types".ToUpper();
    /// <summary>Секция в XML для типа объекта</summary>
    public static readonly string XmlObjTypeName = "Object_Type".ToUpper();
    /// <summary>Секция в XML для типов связей</summary>
    public static readonly string XmlRelTypesName = "Relation_Types".ToUpper();
    /// <summary>Секция в XML для типа связи</summary>
    public static readonly string XmlRelTypeName = "Relation_Type".ToUpper();
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_OBJ_TYPE = nameof (F_OBJ_TYPE);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_VALUE = nameof (F_VALUE);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_BASE_VALUE = nameof (F_BASE_VALUE);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_BASE_ID = nameof (F_BASE_ID);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_BASE_CODE = nameof (F_BASE_CODE);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_EI = nameof (F_EI);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_EI_OKEI = nameof (F_EI_OKEI);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_PROJ_OBJ = nameof (F_PROJ_OBJ);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_PART_OBJ = nameof (F_PART_OBJ);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_USER_ID = nameof (F_USER_ID);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_USER_ALIAS = nameof (F_USER_ALIAS);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string F_USER_NAME = nameof (F_USER_NAME);
    /// <summary>Настройки импорта пакетов XML - "XMLImportSettings"</summary>
    public const string xmlXMLImportSettings = "XMLImportSettings";
    /// <summary>
    /// Правила поиска объектов (Rules of search for objects) - "SearchRules"
    /// </summary>
    public const string xmlSearchRules = "SearchRules";
    /// <summary>
    /// Секция правила поиска, правила создания - "object_type"
    /// </summary>
    public const string xmlObject_type = "object_type";
    /// <summary>
    /// Правила создания версий объектов - "ObjectCreationRules"
    /// </summary>
    public const string xmlObjectCreationRules = "ObjectCreationRules";
    /// <summary>GUID элемента - "guid" (атрибут XML)</summary>
    public const string attrGuid = "guid";
    /// <summary>Название элемента - "name" (атрибут XML)</summary>
    public const string attrName = "name";
    /// <summary>
    /// Чувствительность к регистру букв при поиске - "casesensitive" (атрибут XML)
    /// </summary>
    public const string attrCaseSensitive = "casesensitive";
    /// <summary>
    /// Игнорировать пробелы в значениях при поиске - "skipspaces" (атрибут XML)
    /// </summary>
    public const string attrSkipSpaces = "skipspaces";
    /// <summary>Правило - "rule" (атрибут XML)</summary>
    public const string attrRule = "rule";
    /// <summary>
    /// Правило "Создавать новую версию объекта" - "createNew" (значение атрибута "rule")
    /// </summary>
    public const string ruleCreateNew = "createNew";
    /// <summary>
    /// Правило "Отыскивать и обновлять базовую версию объекта" - "refreshBase" (значение атрибута "rule")
    /// </summary>
    public const string ruleRefreshBase = "refreshBase";
    /// <summary>
    /// Правило "Создавать версию на основе НСИ" - "createByDictionary" (значение атрибута "rule")
    /// </summary>
    public const string ruleCreateByDictionary = "createByDictionary";
    /// <summary>
    /// Правило "Пропускать объект" - "skip" (значение атрибута "rule")
    /// </summary>
    public const string ruleSkip = "skip";
  }
}
