// Decompiled with JetBrains decompiler
// Type: Intermech.MRP2.MRP2Consts
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.MRP2;

public static class MRP2Consts
{
  /// <summary>Тип объекта "Объекты производственной ведомости"</summary>
  public const string objtypeProductionObjects = "cadd9a56-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Объекты производственной ведомости"</summary>
  public static int objtypeIdProductionObjects = 0;
  /// <summary>Тип объекта "Производственная ведомость"</summary>
  public const string objtypeProductionLists = "cadd9a5c-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Производственная ведомость"</summary>
  public static int objtypeIdProductionLists = 0;
  /// <summary>Тип объекта "Производственные копии"</summary>
  public const string objtypeProductionCopy = "cadd9a5d-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Производственные копии"</summary>
  public static int objtypeIdProductionCopy = 0;
  /// <summary>Тип объекта "Выходные сборки"</summary>
  public const string objtypeExitAssembly = "cadd9a9b-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Выходные сборки"</summary>
  public static int objtypeIdExitAssembly = 0;
  /// <summary>Тип объекта "Производственные копии деталей"</summary>
  public const string objtypePartCopy = "cadd9a64-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Производственные копии деталей"</summary>
  public static int objtypeIdPartCopy = 0;
  /// <summary>Тип объекта "Производственные копии комплексов"</summary>
  public const string objtypeСomplexCopy = "cadd9a62-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Производственные копии комплексов"</summary>
  public static int objtypeIdСomplexCopy = 0;
  /// <summary>Тип объекта "Производственные копии комплектов"</summary>
  public const string objtypePackageCopy = "cadd9a63-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Производственные копии комплектов"</summary>
  public static int objtypeIdPackageCopy = 0;
  /// <summary>Тип объекта "Производственные копии материалов"</summary>
  public const string objtypeMaterialCopy = "cadd9a67-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Производственные копии материалов"</summary>
  public static int objtypeIdMaterialCopy = 0;
  /// <summary>Тип объекта "Производственные копии прочих изделий"</summary>
  public const string objtypeOthersCopy = "cadd9a66-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Производственные копии прочих изделий"</summary>
  public static int objtypeIdOthersCopy = 0;
  /// <summary>Тип объекта "Производственные копии сборочных единиц"</summary>
  public const string objtypeAssemblyCopy = "cadd9a61-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Производственные копии сборочных единиц"</summary>
  public static int objtypeIdAssemblyCopy = 0;
  /// <summary>
  /// Тип объекта "Производственные копии стандартных изделий"
  /// </summary>
  public const string objtypeStandardCopy = "cadd9a65-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Тип объекта "Производственные копии стандартных изделий"
  /// </summary>
  public static int objtypeIdStandardCopy = 0;
  /// <summary>Тип объекта "Производственные копии узлов и деталей"</summary>
  public const string objtypePartsNodeCopy = "cadd9a60-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Производственные копии узлов и деталей"</summary>
  public static int objtypeIdPartsNodeCopy = 0;
  /// <summary>тип объекта "узлы и детали"</summary>
  public const string objtypeParts = "cadd9b4e-306c-11d8-b4e9-00304f19f545";
  /// <summary>тип объекта "узлы и детали"</summary>
  public static int objtypeIdParts = 0;
  /// <summary>Тип объекта "Документ"</summary>
  public const string objtypeDocument = "cad00070-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип объекта "Документ"</summary>
  public static int objtypeIdDocument = 0;
  /// <summary>Аттрибут Признак проведения изменений в ПВ</summary>
  public const string attributeChangesFromEcoAccepted = "cadd9bdb-306c-11d8-b4e9-00304f19f545";
  /// <summary>Аттрибут Признак проведения изменений в ПВ</summary>
  public static int attrIdChangesFromEcoAccepted = 0;
  /// <summary>Атрибут Идентификатор ПК ДСЕ</summary>
  public const string attributePKDSE_Id = "cadd9bcc-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут Идентификатор ПК ДСЕ</summary>
  public static int attrIdPKDSE_Id = 0;
  /// <summary>Атрибут "Применяемость в комплектах"</summary>
  public const string attributeApplicabilityInKomplekt = "cadd9bcf-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Применяемость в комплектах"</summary>
  public static int attrIdApplicabilityInKomplekt = 0;
  /// <summary>Атрибут "Применяемость в ПВ"</summary>
  public const string attributeApplicabilityinPL = "cadd9bc0-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Применяемость в ПВ"</summary>
  public static int attrIdApplicabilityinPL = 0;
  /// <summary>Атрибут "Ведомость №"</summary>
  public const string attributeProductionListNumber = "cadd9a81-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Ведомость №"</summary>
  public static int attrIdProductionListNumber = 0;
  /// <summary>Атрибут "Ссылка на ПВ"</summary>
  public const string attributeProductionListLink = "cadd9a71-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Ссылка на ПВ"</summary>
  public static int attrIdProductionListLink = 0;
  /// <summary>Атрибут "Метод обработки поставки"</summary>
  public const string attributeSupplyMethod = "cadd9a72-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Метод обработки поставки"</summary>
  public static int attrIdSupplyMethod = 0;
  /// <summary>Атрибут "Код изменения"</summary>
  public const string attributeChangeCode = "cadd9a73-306c-11d8-b4e9-00304f19f545";
  public static int attrIdChangeCode = 0;
  /// <summary>Атрибут "Признак изменения"</summary>
  public const string attributeChangeTag = "cadd9a77-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Признак изменения"</summary>
  public static int attrIdChangeTag = 0;
  /// <summary>Атрибут "Признак исключения"</summary>
  public const string attributeDeleteTag = "cadd9a76-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Признак исключения"</summary>
  public static int attrIdDeleteTag = 0;
  /// <summary>Аттрибует "Создана на основе связи"</summary>
  public const string attributeCreatedByRelation = "cadd92ec-306c-11d8-b4e9-00304f19f545";
  /// <summary>Аттрибует "Создана на основе связи"</summary>
  public static int attrIdCreatedByRelation = 0;
  /// <summary>Ссылка на изделие для ПВ</summary>
  public const string attributeArticleLink = "cadd9a8c-306c-11d8-b4e9-00304f19f545";
  /// <summary>Ссылка на изделие для ПВ</summary>
  public static int attrIdArticleLink = 0;
  /// <summary>Атрибут Номер версии объекта при создании ЭС ПВ</summary>
  public const string attributeArtilceVersionNumber = "cadd9a8e-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут Номер версии объекта при создании ЭС ПВ</summary>
  public static int attrIdArtilceVersionNumber;
  /// <summary>Атрибут связи "Заменен на"</summary>
  public const string attributeReplacedBy = "cadd9a7a-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Заменен на"</summary>
  public static int attrIdReplacedBy = 0;
  /// <summary>Атрибут ФИО Конструктора</summary>
  public const string attributeFIOConstructor = "cadd9a7b-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут ФИО Конструктора</summary>
  public static int attrIdFIOConstructor = 0;
  /// <summary>Атрибут "Поставляется отдельно"</summary>
  public const string attributeSeparateDelivery = "cadd9a8f-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Поставляется отдельно"</summary>
  public static int attrIdSeparateDelivery = 0;
  /// <summary>атрибут количество</summary>
  public const string attributeCount = "cad00267-306c-11d8-b4e9-00304f19f545";
  public static int attrIdCount = 0;
  /// <summary>атрибут корректировка количества</summary>
  public const string attributeCountCorrect = "cadd9a70-306c-11d8-b4e9-00304f19f545";
  public static int attrIdCountCorrect = 0;
  /// <summary>атрибут Количество на всю ПВ</summary>
  public const string attributeCountForPL = "cadd9a6c-306c-11d8-b4e9-00304f19f545";
  public static int attrIdCountForPL = 0;
  /// <summary>атрибут Количество на выходную сборку</summary>
  public const string attributeCountForExitAssembly = "cadd9a6b-306c-11d8-b4e9-00304f19f545";
  public static int attrIdCountForExitAssembly = 0;
  /// <summary>
  /// атрибут Количество на сборку входимости первого уровня
  /// </summary>
  public const string attributeCountFor1stAssembly = "cadd9a6a-306c-11d8-b4e9-00304f19f545";
  public static int attrIdCountFor1stAssembly = 0;
  /// <summary>Атрибут номер версии ПВ</summary>
  public const string attributeVersionNumberPL = "cadd9a79-306c-11d8-b4e9-00304f19f545";
  public static int attrIdVersionNumberPL = 0;
  /// <summary>Атрибут Хэш</summary>
  public const string attributeHash = "cadd9b6a-306c-11d8-b4e9-00304f19f545";
  public static int attrIdHash = 0;
  /// <summary>Атрибут Хэш из сорча</summary>
  public const string attributeHashSearch = "cadd9c01-306c-11d8-b4e9-00304f19f545";
  public static int attrIdHashSearch = 0;
  /// <summary>Атрибут Основание изменения</summary>
  public const string attributeChangeBase = "cadd9a8b-306c-11d8-b4e9-00304f19f545";
  public static int attrIdChangeBase = 0;
  /// <summary>атрибут "Идентификатор версии в составе"</summary>
  public const string attributeCompositionVersionID = "cad001c2-306c-11d8-b4e9-00304f19f545";
  public static int attrIdCompositionVersionID = 0;
  /// <summary>атрибут "Дата окончания действия"</summary>
  public const string attributeEndDate = "cadd96df-306c-11d8-b4e9-00304f19f545";
  /// <summary>атрибут "Дата окончания действия"</summary>
  public static int attrIdEndDate = 0;
  public const string attributeFromComplect = "cadd9a74-306c-11d8-b4e9-00304f19f545";
  /// <summary>Аттрибут "С комплекта"</summary>
  public static int attrIdFromComplect = 0;
  public const string attributeToComplect = "cadd9a75-306c-11d8-b4e9-00304f19f545";
  /// <summary>Аттрибут "По комплект"</summary>
  public static int attrIdToComplect = 0;
  /// <summary>Тип связи "Состав ЭС ПВ"</summary>
  public const string reltypeProductComposition = "cadd9a57-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип связи "Состав ЭС ПВ"</summary>
  public static int reltypeIdProductComposition = 0;
  /// <summary>Тип связи "Документация на ПВ"</summary>
  public const string reltypeDocumentComposition = "cadd9c34-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип связи "Документация на ПВ"</summary>
  public static int reltypeIdDocumentComposition = 0;
  /// <summary>Тип связи "Документация на изделие"</summary>
  public const string reltypeDocumentation = "cad00154-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип связи "Документация на изделие"</summary>
  public static int reltypeIdDocumentation = 0;
  /// <summary>Тип связи "Состав изделий" (ранее "Проектная связь")</summary>
  public const string reltypeSP = "cad00023-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип связи "Состав изделий" (ранее "Проектная связь")</summary>
  public static int reltypeIdSP = 0;
  /// <summary>команда создать производственную ведомость</summary>
  public const string cmdCreateProductionList = "MRP2.CreateProductionList";
  /// <summary>команда Удалить все исключенные ПК ДСЕ</summary>
  public const string cmdExcludeAllDeleted = "MRP2.ExcludeAllDeleted";
  /// <summary>команда заменить изделие</summary>
  public const string cmdReplacePart = "MRP2.ReplacePart";
  /// <summary>Заменить изделие из констукторских допзамен</summary>
  public const string cmdReplacePartZ = "MRP2.ReplacePartZ";
  /// <summary>команда добавить из состава ПВ</summary>
  public const string cmdAddFromPL = "MRP2.AddFromPL";
  /// <summary>команда скрыть удаленные</summary>
  public const string cmdHideDeleted = "MRP2.HideDeleted";
  /// <summary>команда указать ПВ</summary>
  public const string cmdSelectPL = "MRP2.SelectPL";
  /// <summary>Команда "Изменить способ поставки"</summary>
  public const string cmdSeparateDelivery = "MRP2.SeparateDelivery";
  /// <summary>Комманда "Заменить версию"</summary>
  public const string cmdReplaceVersion = "MRP2.ReplaceVersion";
  /// <summary>Команда "Запустить процесс для ПВ"</summary>
  public const string cmdLaunchProcess = "MRP2.LaunchProcess";
  /// <summary>Команда "запустить проверку ЭС ПВ"</summary>
  public const string cmdStartPLCheck = "MRP2.StartPLCheck";
  /// <summary>Команда "Пересчитать количеств"</summary>
  public const string cmdRecalcCounts = "MRP2.RecalcCounts";
  /// <summary>Команда "Применить копию в других ПВ"</summary>
  public const string cmdApplyCopyInPL = "MRP2.ApplyCopyInPL";
  /// <summary>Команда "Указать применяемость в ПВ"</summary>
  public static string cmdIndicateApplicability = "MRP2.ECO.IndicateApplicability";
  /// <summary>команда "Провести изменения в группе ПВ"</summary>
  public static string cmdApplyChangesByEco = "MRP2.ECO.ApplyChanges";
  /// <summary>команда "применить изменения в ПВ"</summary>
  public static string cmdApplyChangesInPL = "MRP2.ECO.ApplyChangesInPL";
  /// <summary>Команда "Добавить в состав объекты ПВ"</summary>
  public static string cmdAddMRP2 = "MRP2.Add";
  /// <summary>Команда "Фильтр по срокам действия"</summary>
  public const string cmdFilterByDateMenu = "MRP2.FilterByDateMenu";
  /// <summary>Команда "Установить сроки действия связи"</summary>
  public const string cmdAddLinkDateAttributes = "MRP2.AddLinkDateAttributes";
  /// <summary>Команда "Удалить сроки действия связи"</summary>
  public const string cmdRemoveLinkDateAttributes = "MRP2.RemoveLinkDateAttributes";
  /// <summary>гуид группы шаблонов процессов для ПВ</summary>
  public static readonly Guid WorkFlowGroupGuid = new Guid("cadd9b57-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Массив строк - возможных значений атрибута "Метод обработки состава"
  /// </summary>
  public static string[] SupplyMethods;
  /// <summary>
  /// Флаг true/false показывать ли в составе ПВ удаленные позиции.
  /// </summary>
  public const string HideDeletedPositionsInComposition = "9854400D-D3EB-4A82-ADD3-00163FB748FC";
  /// <summary>
  /// Флаг true/false: фильтровать ли связи по атрибуту дата окончания действия.
  /// </summary>
  public const string FilterByDateInCompositionEnabled = "CC4B5C20-3E62-4436-89E8-699262510FD5";
  /// <summary>
  /// Фильтр DateTime: дата, на которую необходимо фильтровать связи по атрибуту дата окончания действия.
  /// </summary>
  public const string FilterByDateInComposition = "85357DBA-2685-4F94-8B40-7889D08B322A";
  private static Dictionary<int, int> _typesettings = (Dictionary<int, int>) null;
  private static bool dopConditionsInited = false;
  private static ConditionStructure[] _dopConditions = (ConditionStructure[]) null;

  static MRP2Consts() => MRP2Consts.InitializeConsts();

  private static void InitializeConsts()
  {
    Type type = typeof (MRP2Consts);
    foreach (FieldInfo field1 in type.GetFields(BindingFlags.Static | BindingFlags.Public))
    {
      if (field1.IsLiteral && !field1.IsInitOnly)
      {
        if (field1.Name.StartsWith("objtype") && !field1.Name.StartsWith("objtypeId"))
        {
          string name = field1.Name.Insert(7, "Id");
          FieldInfo field2 = type.GetField(name);
          if (field2 != (FieldInfo) null)
            field2.SetValue((object) null, (object) MetaDataHelper.GetObjectTypeID((string) field1.GetValue((object) null)));
        }
        else if (field1.Name.StartsWith("attribute"))
        {
          string name = "attrId" + field1.Name.Substring(9);
          FieldInfo field3 = type.GetField(name);
          if (field3 != (FieldInfo) null)
            field3.SetValue((object) null, (object) MetaDataHelper.GetAttributeID((object) (string) field1.GetValue((object) null)));
        }
        else if (field1.Name.StartsWith("reltype") && !field1.Name.StartsWith("reltypeId"))
        {
          string name = field1.Name.Insert(7, "Id");
          FieldInfo field4 = type.GetField(name);
          if (field4 != (FieldInfo) null)
            field4.SetValue((object) null, (object) MetaDataHelper.GetRelationTypeID((string) field1.GetValue((object) null)));
        }
      }
    }
    if (MRP2Consts.attrIdSupplyMethod <= 0)
      return;
    MRP2Consts.SupplyMethods = MetaDataHelper.GetAttributeType(MRP2Consts.attrIdSupplyMethod).PossibleValues.Select<object, string>((System.Func<object, string>) (i => i.ToString())).ToArray<string>();
  }

  public static MRP2Consts.ArticleSupplyMethod? StringToArticleSupplyMethod(string Value)
  {
    Type enumType = typeof (MRP2Consts.ArticleSupplyMethod);
    string[] names = Enum.GetNames(enumType);
    for (int index = 0; index < names.Length; ++index)
    {
      FieldInfo field = enumType.GetField(names[index]);
      if (field != (FieldInfo) null)
      {
        if (Value == names[index])
          return new MRP2Consts.ArticleSupplyMethod?((MRP2Consts.ArticleSupplyMethod) field.GetValue((object) null));
        if (Attribute.GetCustomAttribute((MemberInfo) field, typeof (DescriptionAttribute)) is DescriptionAttribute customAttribute && Value == customAttribute.Description)
          return new MRP2Consts.ArticleSupplyMethod?((MRP2Consts.ArticleSupplyMethod) field.GetValue((object) null));
      }
    }
    return new MRP2Consts.ArticleSupplyMethod?();
  }

  public static string ArticleSupplyMethodToString(MRP2Consts.ArticleSupplyMethod? Value)
  {
    if (!Value.HasValue)
      return "";
    FieldInfo field = typeof (MRP2Consts.ArticleSupplyMethod).GetField(Value.ToString());
    return field != (FieldInfo) null && Attribute.GetCustomAttribute((MemberInfo) field, typeof (DescriptionAttribute)) is DescriptionAttribute customAttribute ? customAttribute.Description : Value.ToString();
  }

  public static void InitCopyTypesSettings(IUserSession session)
  {
    MRP2Consts._typesettings = new Dictionary<int, int>();
    BlobInformation config_info;
    byte[] config_file;
    session.Configurations.LoadConfigData("mrp2settings.xml", out config_info, out config_file, 0L);
    if (config_info.RealFileSize <= 0L)
      return;
    MemoryStream inStream = new MemoryStream(config_file);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load((Stream) inStream);
    foreach (XmlNode childNode in xmlDocument.FirstChild.ChildNodes)
    {
      IMSObjectType objectType1 = MetaDataHelper.GetObjectType(new Guid(childNode.Attributes["F_OBJECT_TYPE"].Value));
      IMSObjectType objectType2 = MetaDataHelper.GetObjectType(new Guid(childNode.Attributes["F_COPY_TYPE"].Value));
      if (objectType1 != null)
      {
        if (objectType2 != null)
        {
          try
          {
            MRP2Consts._typesettings.Add(objectType1.ObjectTypeID, objectType2.ObjectTypeID);
          }
          catch (Exception ex)
          {
          }
        }
      }
    }
  }

  /// <summary>
  /// Вернёт по типу изделия (деталь, сборка итп) тип соответсвующей производственной копии
  /// если тип непонятный - то вернет тип производственных копий прочих изделий.
  /// TODO: сделать настройку этого соответствия вместо хардкода
  /// </summary>
  /// <param name="objectType">Тип изделия</param>
  /// <returns>Тип производственной копии</returns>
  public static int GetCopyType(IUserSession session, int objectType, int defaultType = 0)
  {
    if (MRP2Consts._typesettings == null)
      MRP2Consts.InitCopyTypesSettings(session);
    foreach (int key in MRP2Consts._typesettings.Keys)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(objectType, key))
        return MRP2Consts._typesettings[key];
    }
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, new Guid("cadd9b4e-306c-11d8-b4e9-00304f19f545")))
      return MRP2Consts.objtypeIdExitAssembly;
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, new Guid("cad00132-306c-11d8-b4e9-00304f19f545")))
      return MRP2Consts.objtypeIdAssemblyCopy;
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, new Guid("cad00252-306c-11d8-b4e9-00304f19f545")))
      return MRP2Consts.objtypeIdStandardCopy;
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, new Guid("cad0025e-306c-11d8-b4e9-00304f19f545")))
      return MRP2Consts.objtypeIdСomplexCopy;
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, new Guid("cad00250-306c-11d8-b4e9-00304f19f545")))
      return MRP2Consts.objtypeIdPartCopy;
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, new Guid("cad0025f-306c-11d8-b4e9-00304f19f545")))
      return MRP2Consts.objtypeIdPackageCopy;
    if (MetaDataHelper.IsObjectTypeChildOf(objectType, new Guid("cad00170-306c-11d8-b4e9-00304f19f545")))
      return MRP2Consts.objtypeIdMaterialCopy;
    return defaultType == 0 ? MRP2Consts.objtypeIdOthersCopy : defaultType;
  }

  internal static string GetDBObjectHash(
    IDBObject o,
    int newType,
    MRP2Consts.ArticleSupplyMethod? supplyMethod)
  {
    string caption;
    try
    {
      DateTime dateTime = o.ModifyDate;
      dateTime = dateTime.ToUniversalTime();
      caption = dateTime.ToString("ddMMyyyyHHmmssfff");
    }
    catch (KernelException ex)
    {
      caption = o.Caption;
    }
    return MRP2Consts.HashData($"{o.ObjectGUID.ToString().ToLower()}-{caption}-{newType}-{supplyMethod.ToString()}");
  }

  public static string HashData(string data)
  {
    using (SHA256 shA256 = SHA256.Create())
      return BitConverter.ToString(shA256.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
  }

  public static string CalculateHashForObject(
    IDBObject dbObj,
    int newType,
    MRP2Consts.ArticleSupplyMethod? supplyMethod,
    bool IgnoreSupplyMethod,
    Dictionary<long, string> hashDict)
  {
    string dbObjectHash = MRP2Consts.GetDBObjectHash(dbObj, newType, supplyMethod);
    List<string> stringList = new List<string>();
    if (supplyMethod.HasValue | IgnoreSupplyMethod)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dbObj.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545")).ConsistFrom(new DBRecordSetParams(new ConditionStructure[0], new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) MRP2Consts.attrIdCount, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      }), dbObj.ObjectID).Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        IDBObject dbObj1 = dbObj.Session.GetObject(int64);
        string hashForObject = MRP2Consts.CalculateHashForObject(dbObj1, MRP2Consts.GetCopyType(dbObj.Session, dbObj1.ObjectType), new MRP2Consts.ArticleSupplyMethod?(), IgnoreSupplyMethod, hashDict);
        string stringValue = DataSetProcessor.GetStringValue(row, 1, "");
        stringList.Add($"{hashForObject}-{stringValue}");
      }
    }
    stringList.Sort();
    stringList.Add(dbObjectHash);
    string hashForObject1 = MRP2Consts.HashData(string.Join("\r\n", stringList.ToArray()));
    hashDict[dbObj.ObjectID] = hashForObject1;
    return hashForObject1;
  }

  public static void FillCopyProperties(
    IDBObject newInstance,
    int newType,
    MRP2Consts.ArticleSupplyMethod? supplyMethod,
    IDBObject dbObj,
    string hash)
  {
    List<AttributeValues> attributeValuesList = new List<AttributeValues>()
    {
      new AttributeValues(MRP2Consts.attrIdArticleLink, (object) dbObj.ObjectID),
      new AttributeValues(MRP2Consts.attrIdHash, (object) hash),
      new AttributeValues(MRP2Consts.attrIdPKDSE_Id, (object) dbObj.GUID)
    };
    if (supplyMethod.HasValue)
      attributeValuesList.Add(new AttributeValues(MRP2Consts.attrIdSupplyMethod, (object) MRP2Consts.ArticleSupplyMethodToString(supplyMethod)));
    newInstance.SetAttributesValues(attributeValuesList.ToArray());
  }

  /// <summary>Создать копию объекта без состава</summary>
  /// <param name="Session"></param>
  /// <param name="masterObjectID"></param>
  /// <param name="objectTypeID"></param>
  /// <param name="setArticleLink"></param>
  /// <returns></returns>
  public static IDBObject CreateObjectCopy(
    IUserSession Session,
    long masterObjectID,
    int objectTypeID)
  {
    IDBObjectCollection objectCollection = Session.GetObjectCollection(objectTypeID);
    objectCollection.SetDisabledPrototypeRelationTypes(new List<int>()
    {
      MRP2Consts.reltypeIdProductComposition,
      MRP2Consts.reltypeIdDocumentComposition
    });
    return objectCollection.Create(masterObjectID);
  }

  /// <summary>Создать производственную копию по составу изделия</summary>
  /// <param name="dbObj"></param>
  /// <param name="protoTypeObjectId">Ид. объекта - прототипа (ПК ДСЕ)</param>
  /// <param name="newType"></param>
  /// <param name="versionPL"></param>
  /// <param name="supplyMethod"></param>
  /// <param name="IgnoreSupplyMethod"></param>
  /// <returns></returns>
  public static long CreateObjectCopy(
    IDBObject dbObj,
    long protoTypeObjectId,
    int newType,
    long versionPL,
    MRP2Consts.ArticleSupplyMethod? supplyMethod,
    bool IgnoreSupplyMethod,
    Dictionary<long, string> hashDict,
    AttributeValues[] values)
  {
    IDBObject newInstance = Consts.IsUndefinedObjectId(protoTypeObjectId) ? dbObj.Session.GetObjectCollection(newType).Create() : MRP2Consts.CreateObjectCopy(dbObj.Session, protoTypeObjectId, newType);
    newInstance.Attributes.AssignPossibleAttributes(dbObj.Attributes, Consts.CreateMode);
    MRP2Consts.SafeSetAttributeValues((IDBAttributable) newInstance, values);
    MRP2Consts.FillCopyProperties(newInstance, newType, supplyMethod, dbObj, hashDict[dbObj.ObjectID]);
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    if (supplyMethod.HasValue | IgnoreSupplyMethod)
    {
      attributeValuesList.Clear();
      if (!IgnoreSupplyMethod)
      {
        attributeValuesList.Add(new AttributeValues(MRP2Consts.attrIdDeleteTag, (object) true));
        attributeValuesList.Add(new AttributeValues(MRP2Consts.attrIdVersionNumberPL, (object) versionPL));
        attributeValuesList.Add(new AttributeValues(MRP2Consts.attrIdChangeCode, (object) MRP2Consts.ProductionLinkFlag.Deleted));
      }
      attributeValuesList.Add(new AttributeValues(MRP2Consts.attrIdCreatedByRelation, (object) Guid.Empty));
      AttributeValues[] array1 = attributeValuesList.ToArray();
      IDBRelationCollection relationCollection1 = dbObj.Session.GetRelationCollection(MRP2Consts.reltypeIdSP);
      IDBRelationCollection relationCollection2 = dbObj.Session.GetRelationCollection(MRP2Consts.reltypeIdProductComposition);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[0], new object[3]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
        (object) ObligatoryObjectAttributes.F_PRJ_GUID
      });
      foreach (DataRow row in (InternalDataCollectionBase) relationCollection1.ConsistFrom(paramSet, dbObj.ObjectID).Rows)
      {
        long int64_1 = Convert.ToInt64(row[0]);
        IDBObject dbObj1 = dbObj.Session.GetObject(int64_1);
        long objectCopy = MRP2Consts.CreateObjectCopy(dbObj1, 0L, MRP2Consts.GetCopyType(dbObj.Session, dbObj1.ObjectType), versionPL, new MRP2Consts.ArticleSupplyMethod?(), IgnoreSupplyMethod, hashDict, (AttributeValues[]) null);
        long int64_2 = Convert.ToInt64(row[1]);
        Guid guid = new Guid(Convert.ToString(row[2]));
        array1[array1.Length - 1].Values[0] = (object) guid;
        NewRelationProperties properties = new NewRelationProperties()
        {
          ProjectObjectID = newInstance.ObjectID,
          PartObjectID = objectCopy,
          PrototypeRelationID = int64_2,
          ValuesList = array1
        };
        relationCollection2.Create(properties);
      }
      attributeValuesList.Add(new AttributeValues(MRP2Consts.attrIdCompositionVersionID, (object) 0));
      AttributeValues[] array2 = attributeValuesList.ToArray();
      IDBRelationCollection relationCollection3 = dbObj.Session.GetRelationCollection(MRP2Consts.reltypeIdDocumentation);
      IDBRelationCollection relationCollection4 = dbObj.Session.GetRelationCollection(MRP2Consts.reltypeIdDocumentComposition);
      paramSet.Conditions = MRP2Consts.GetDocConditions(dbObj.Session);
      foreach (DataRow row in (InternalDataCollectionBase) relationCollection3.ConsistFrom(paramSet, dbObj.ObjectID).Rows)
      {
        long int64_3 = Convert.ToInt64(row[0]);
        long int64_4 = Convert.ToInt64(row[1]);
        Guid guid = new Guid(Convert.ToString(row[2]));
        array2[array2.Length - 2].Values[0] = (object) guid;
        array2[array2.Length - 1].Values[0] = (object) int64_3;
        NewRelationProperties properties = new NewRelationProperties()
        {
          ProjectObjectID = newInstance.ObjectID,
          PartObjectID = int64_3,
          PrototypeRelationID = int64_4,
          ValuesList = array2
        };
        relationCollection4.Create(properties);
      }
    }
    newInstance.CommitCreation(true, false);
    return newInstance.ObjectID;
  }

  public static void SafeSetAttributeValues(IDBAttributable newInstance, AttributeValues[] values)
  {
    if (values == null)
      return;
    foreach (AttributeValues attributeValues in values)
      attributeValues.ThrowSetException = false;
    foreach (KeyValuePair<string, Exception> keyValuePair in newInstance.SetAttributesValuesEx(values, false, true, false, GetAttributeValuesModes.None))
    {
      if (!(keyValuePair.Value is KernelExceptionID kernelExceptionId))
        throw keyValuePair.Value;
      if (kernelExceptionId.ErrorID != 21 && kernelExceptionId.ErrorID != 49)
        throw kernelExceptionId;
    }
  }

  /// <summary>
  /// Условия для типов документов которые могут быть включены в ПВ
  /// </summary>
  public static ConditionStructure[] GetDocConditions(IUserSession session)
  {
    if (!MRP2Consts.dopConditionsInited)
    {
      MRP2Consts._dopConditions = (ConditionStructure[]) null;
      List<int> source = new List<int>();
      foreach (ApplicabilitiesKey applicabilitiesKey in MetaDataHelper.GetObjectTypeApplicabilities(MRP2Consts.objtypeIdProductionCopy).Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (a => a.RelationTypeID == MRP2Consts.reltypeIdDocumentComposition)).ToList<IMSApplicability>().GetEnableChildApplicabilitiesKey())
        source.Add(applicabilitiesKey.ChildType);
      List<int> list = source.Distinct<int>().ToList<int>();
      if (list.Count > 0)
      {
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
        ConditionStructure conditionStructure = new ConditionStructure(-7, RelationalOperators.In, (object) list.ToArray(), LogicalOperators.AND, 0, false);
        conditionStructureList.Add(conditionStructure);
        long conditionValue = -1;
        IDBObject dbObject = session.GetObject(new Guid("cad00256-306c-11d8-b4e9-00304f19f545"), false);
        if (dbObject != null)
          conditionValue = dbObject.ObjectID;
        if (conditionValue != -1L)
        {
          conditionStructure = new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad00266-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0, false);
          conditionStructureList.Add(conditionStructure);
        }
        MRP2Consts._dopConditions = conditionStructureList.ToArray();
      }
      MRP2Consts.dopConditionsInited = true;
    }
    return MRP2Consts._dopConditions;
  }

  /// <summary>Метод поставки/обработки изделия</summary>
  public enum ArticleSupplyMethod
  {
    [Description("Изготовление")] Production,
    [Description("Входной контроль")] InputControl,
    [Description("Консервация")] Conservation,
    [Description("Испытания")] Trial,
  }

  /// <summary>Тип для значение поля "Код изменения"</summary>
  public enum ProductionLinkFlag
  {
    Copied = 0,
    Added = 1,
    Deleted = 2,
    Modified = 4,
  }
}
