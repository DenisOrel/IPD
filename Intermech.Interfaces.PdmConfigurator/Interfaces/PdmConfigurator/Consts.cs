// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.Consts
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Константы конфигуратора составов IPS</summary>
public static class Consts
{
  /// <summary>Внимание</summary>
  public static readonly string Dialog1 = LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_1");
  /// <summary>
  /// На сервере приложений не загружен плагин \"Intermech.PdmConfigurator.Server\".\nРабота клиентского плагина \"Intermech.PdmConfigurator\" будет заблокирована.
  /// </summary>
  public static readonly string Dialog2 = LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_2");
  /// <summary>Название плагина - "InterMech.PdmConfigurator"</summary>
  public static readonly string PDMPluginName = LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_71");
  /// <summary>
  /// Файл с рисунками плагина - "Intermech.PdmConfigurator.Resources.Options.bmp"
  /// </summary>
  public const string PDMPluginBitmaps = "Intermech.PdmConfigurator.Resources.Options.bmp";
  /// <summary>imgPdmConfigurator.Options</summary>
  public const string imgOptions = "imgPdmConfigurator.Options";
  /// <summary>imgPdmConfigurator.Configurator</summary>
  public const string imgConfigurator = "imgPdmConfigurator.Configurator";
  /// <summary>
  /// Название плагина - "Серверная часть InterMech.PDMConfigurator"
  /// </summary>
  public static readonly string PDMConfiguratorPluginName = LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_3");
  /// <summary>ID системного объекта "Нет категории"</summary>
  public static long objectNoCategoryID = 0;
  /// <summary>ID типа объекта "Опция"</summary>
  public static int objtypeOptionID = -1;
  /// <summary>ID типа объекта "Категории опций"</summary>
  public static int objtypeOptionsGroupID = -1;
  /// <summary>ID типа объекта "Объекты конфигуратора составов"</summary>
  public static int objtypeConfiguratorObjectsID = -1;
  /// <summary>ID типа объекта "Комплектации"</summary>
  public static int objtypeComplementsID = -1;
  /// <summary>ID атрибута "Видимые значения опции"</summary>
  public static int attributeVisibleOptionValuesID = 0;
  /// <summary>ID атрибута "Ссылка на опции"</summary>
  public static int attributeOptionsLinkID = 0;
  /// <summary>ID атрибута "Условия применения объекта"</summary>
  public static int attributeObjectApplicabilityCondID = 0;
  /// <summary>ID атрибута "Условия несовместимости опций"</summary>
  public static int attributeOptionsIncompatibilityID = 0;
  /// <summary>ID атрибута "Значения опции"</summary>
  public static int attributeOptionValuesID = 0;
  /// <summary>ID атрибута "Код опции"</summary>
  public static int attributeOptionCodeID = 0;
  /// <summary>ID атрибута "Тип данных опции"</summary>
  public static int attributeOptionDataTypeID = 0;
  /// <summary>ID атрибута "Название опции"</summary>
  public static int attributeOptionCaptionID = 0;
  /// <summary>ID атрибута "Название группы опций"</summary>
  public static int attributeOptionsGroupCaptionID = 0;
  /// <summary>ID атрибута "Контекст конфигуратора составов"</summary>
  public static int attributeConfiguratorContextID = 0;
  /// <summary>ID атрибута "Ссылка на категорию опции"</summary>
  public static int attributeCategoryLinkID = 0;
  /// <summary>ID атрибута "Изображение конфигуратора составов"</summary>
  public static int attributeConfiguratorImageID = 0;
  /// <summary>ID атрибута "Флажки опции"</summary>
  public static int attributeOptionFlagsID = 0;
  /// <summary>Узел &lt;guids&gt;</summary>
  public const string xmlnodeGuids = "a";
  /// <summary>Узел &lt;guid&gt;</summary>
  public const string xmlnodeGuid = "b";
  /// <summary>Узел &lt;options&gt;</summary>
  public const string xmlnodeOptions = "c";
  /// <summary>Узел &lt;items&gt;</summary>
  public const string xmlnodeItems = "d";
  /// <summary>Узел &lt;item&gt;</summary>
  public const string xmlnodeItem = "e";
  /// <summary>Узел &lt;linkedoptions&gt;</summary>
  public const string xmlnodeLinkedOptions = "f";
  /// <summary>Узел &lt;option&gt;</summary>
  public const string xmlnodeOption = "g";
  /// <summary>Узел &lt;value&gt;</summary>
  public const string xmlnodeValue = "h";
  /// <summary>Узел &lt;configurationcode&gt;</summary>
  public const string xmlnodeConfigurationCode = "i";
  /// <summary>Узел &lt;codepart&gt;</summary>
  public const string xmlnodeCodePart = "j";
  /// <summary>Атрибут &lt;value&gt;</summary>
  public const string xmlattrValue = "a";
  /// <summary>Атрибут &lt;option&gt;</summary>
  public const string xmlattrOption = "b";
  /// <summary>Атрибут &lt;guid&gt;</summary>
  public const string xmlattrGuid = "c";
  /// <summary>Атрибут &lt;conflict&gt;</summary>
  public const string xmlattrConflict = "d";
  /// <summary>Атрибут &lt;id&gt;</summary>
  public const string xmlattrId = "e";
  /// <summary>Атрибут &lt;codepartproperties&gt;</summary>
  public const string xmlnodeCodePartProperties = "f";
  /// <summary>
  /// Ключ для размещения в параметрах запроса. Запрещает трассировать составы покупных изделий
  /// </summary>
  public const string keyDisableBoughtArticles = "{78C6A7F1-3B57-4CF9-8E3C-B5D308593A6B}";
  /// <summary>
  /// Ключ для размещения в параметрах запроса. Запрещает трассировать допустимые замены
  /// </summary>
  public const string keyDisableSubstitutes = "{7C0E9952-C5C7-4505-AA53-2F662A4E9D2B}";

  /// <summary>Инициализировать константы</summary>
  public static void Initialize()
  {
    Consts.objtypeOptionID = MetaDataHelper.GetObjectTypeID("cad015b0-306c-11d8-b4e9-00304f19f545");
    Consts.objtypeOptionsGroupID = MetaDataHelper.GetObjectTypeID("cad015af-306c-11d8-b4e9-00304f19f545");
    Consts.objtypeConfiguratorObjectsID = MetaDataHelper.GetObjectTypeID("cad00592-306c-11d8-b4e9-00304f19f545");
    Consts.objtypeComplementsID = MetaDataHelper.GetObjectTypeID("cad015b1-306c-11d8-b4e9-00304f19f545");
    Consts.attributeVisibleOptionValuesID = MetaDataHelper.GetAttributeTypeID("cad015a1-306c-11d8-b4e9-00304f19f545");
    Consts.attributeOptionsLinkID = MetaDataHelper.GetAttributeTypeID("cad015a9-306c-11d8-b4e9-00304f19f545");
    Consts.attributeObjectApplicabilityCondID = MetaDataHelper.GetAttributeTypeID("cad015ac-306c-11d8-b4e9-00304f19f545");
    Consts.attributeOptionsIncompatibilityID = MetaDataHelper.GetAttributeTypeID("cad015ab-306c-11d8-b4e9-00304f19f545");
    Consts.attributeOptionValuesID = MetaDataHelper.GetAttributeTypeID("cad015a2-306c-11d8-b4e9-00304f19f545");
    Consts.attributeOptionCodeID = MetaDataHelper.GetAttributeTypeID("cad015a5-306c-11d8-b4e9-00304f19f545");
    Consts.attributeOptionDataTypeID = MetaDataHelper.GetAttributeTypeID("cad015aa-306c-11d8-b4e9-00304f19f545");
    Consts.attributeOptionCaptionID = MetaDataHelper.GetAttributeTypeID("cad015a8-306c-11d8-b4e9-00304f19f545");
    Consts.attributeOptionsGroupCaptionID = MetaDataHelper.GetAttributeTypeID("cad015a7-306c-11d8-b4e9-00304f19f545");
    Consts.attributeConfiguratorContextID = MetaDataHelper.GetAttributeTypeID("cad015a6-306c-11d8-b4e9-00304f19f545");
    Consts.attributeCategoryLinkID = MetaDataHelper.GetAttributeTypeID("cad015a4-306c-11d8-b4e9-00304f19f545");
    Consts.attributeConfiguratorImageID = MetaDataHelper.GetAttributeTypeID("cad015a3-306c-11d8-b4e9-00304f19f545");
    Consts.attributeOptionFlagsID = MetaDataHelper.GetAttributeTypeID("cad015ad-306c-11d8-b4e9-00304f19f545");
  }

  /// <summary>
  /// Инициализировать константы, для получения которых требуется сессия
  /// </summary>
  /// <param name="session">Сессия</param>
  public static void Initialize(IUserSession session)
  {
    if (Consts.objectNoCategoryID != 0L || session == null)
      return;
    IDBObject dbObject = session.GetObject(new Guid("cad0159f-306c-11d8-b4e9-00304f19f545"), false);
    if (dbObject == null)
      return;
    Consts.objectNoCategoryID = dbObject.ObjectID;
  }
}
