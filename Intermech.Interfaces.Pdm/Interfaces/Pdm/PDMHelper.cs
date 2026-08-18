// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.PDMHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Kernel.Search;
using Intermech.Pdm.InstancesAndParties;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// 
/// </summary>
public class PDMHelper
{
  /// <summary>тип связи "Состав экземпляров и партий изделий"</summary>
  public static Guid relationTypeInstances = new Guid("cad00584-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут "Учет изделий в производстве"</summary>
  public static Guid attributeStorageArticle = new Guid("cad0058a-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут "Активная партия"</summary>
  public static Guid attributeActiveParty = new Guid("cad0058f-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут "Серийный номер"</summary>
  public static Guid attributeSerialNo = new Guid("cad00586-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Типы объектов для которых требуется разворачивать состав
  /// </summary>
  public static Guid attributeTypesToExpand = new Guid("cadd999b-306c-11d8-b4e9-00304f19f545");
  /// <summary>Типы объектов для которых нельзя разворачивать состав</summary>
  public static Guid attributeTypesToDisableExpand = new Guid("cadd999c-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип объектов "Экземпляры сборочных единиц"</summary>
  public static Guid objtypeInstAssemblyUnit = new Guid("cad0058b-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип объектов "Партии сборочных единиц"</summary>
  public static Guid objtypePartyAssemblyUnit = new Guid("cad0058c-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип объектов "Экземпляры деталей"</summary>
  public static Guid objtypeInstPart = new Guid("cad0058d-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип объектов "Партии деталей"</summary>
  public static Guid objtypePartyPart = new Guid("cad0058e-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип объектов "Экземпляры стандартных изделий"</summary>
  public static Guid objtypeInstStandard = new Guid("cad0063c-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип объектов "Партии стандартных изделий"</summary>
  public static Guid objtypePartyStandard = new Guid("cad0063d-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// атрибут "Глобальный идентификатор типа объектов экземпляра"
  /// </summary>
  public static Guid attributeGuidInstance = new Guid("cad0063f-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// атрибут "Глобальный идентификатор типа объектов партии"
  /// </summary>
  public static Guid attributeGuidParty = new Guid("cad0063e-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут "Идентификатор версии в составе"</summary>
  public static Guid attributeIdVersionInComposition = new Guid("cad001c2-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип объектов "Общие правила сравнения составов"</summary>
  public static Guid objtypeCommonCompositionRules = new Guid("cadd9a9a-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// тип объектов "Персональные правила сравнения составов"
  /// </summary>
  public static Guid objtypePersonalCompositionRules = new Guid("cadd9a99-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут "Изменения"</summary>
  public static Guid attributeChanges = new Guid("cadd9abb-306c-11d8-b4e9-00304f19f545");
  /// <summary>Имя модуля в настройках Configurations</summary>
  public const string ModuleName = "PDM";
  /// <summary>
  /// Имя секции с настройками синхронизации атрибутов изделий и конструкторских документов
  /// </summary>
  public const string AttributesSyncSection = "AttrSyncSection";

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="oldDesignation"></param>
  /// <param name="objectType"></param>
  /// <returns></returns>
  public static string GetDesignationWithoutCode(
    IUserSession session,
    string oldDesignation,
    int objectType)
  {
    DocumentTypeSettings settings = (session.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService).GetSettings(session.SessionGUID, objectType);
    return PDMHelper.GetDesignationWithoutCode(session, oldDesignation, settings);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="oldDesignation"></param>
  /// <param name="dts"></param>
  /// <returns></returns>
  public static string GetDesignationWithoutCode(
    IUserSession session,
    string oldDesignation,
    DocumentTypeSettings dts)
  {
    if (dts.DocumentTypeCodeInDesignation && dts.DocumentTypeCode != string.Empty)
      oldDesignation = DocumentsHelper.RemoveDocCode(session, oldDesignation, dts.DocumentTypeCode);
    return oldDesignation;
  }

  /// <summary>Получить тип объектов выпускаемых для изделия</summary>
  public static IDBObjectType GetInstanceObjectType(
    IUserSession session,
    int articleObjectType,
    ArticlesInManufacture inManuf)
  {
    IDBObjectType objectType = session.GetObjectType(articleObjectType);
    if (objectType == null)
      return (IDBObjectType) null;
    (objectType as IDBGuid).GUID.ToString();
    int anObjectType = -1;
    switch (inManuf)
    {
      case ArticlesInManufacture.Parties:
        anObjectType = InstancePartyObjectType4ObjectTypeHelper.GetPartyObjectTypeID4ObjectTypeID(session, articleObjectType);
        break;
      case ArticlesInManufacture.Instances:
        anObjectType = InstancePartyObjectType4ObjectTypeHelper.GetInstanceObjectTypeID4ObjectTypeID(session, articleObjectType);
        break;
    }
    if (anObjectType != -1)
      return session.GetObjectType(anObjectType);
    IDBObject containerForObjectType = (session.GetCustomService(typeof (IContainerService)) as IContainerService).GetContainerForObjectType((object) session.SessionGUID, articleObjectType);
    if (containerForObjectType != null)
    {
      switch (inManuf)
      {
        case ArticlesInManufacture.Parties:
          IDBAttribute attributeByGuid1 = containerForObjectType.GetAttributeByGuid(PDMHelper.attributeGuidParty);
          if (attributeByGuid1 != null && CompareValuesHelper.NormalizedValue(attributeByGuid1.Value) != null)
            return session.GetObjectType(new Guid(attributeByGuid1.Value.ToString()));
          break;
        case ArticlesInManufacture.Instances:
          IDBAttribute attributeByGuid2 = containerForObjectType.GetAttributeByGuid(PDMHelper.attributeGuidInstance);
          if (attributeByGuid2 != null && CompareValuesHelper.NormalizedValue(attributeByGuid2.Value) != null)
            return session.GetObjectType(new Guid(attributeByGuid2.Value.ToString()));
          break;
      }
    }
    return (IDBObjectType) null;
  }

  /// <summary>
  /// Функция проверяет изделиe articleID на наличие в составе 3D моделей (объекты типов Электронная модель сборки и Электронная модель детали)
  /// и возвращает True, если такие объекты найдены
  /// </summary>
  /// <param name="session"></param>
  /// <param name="articleID"></param>
  /// <returns></returns>
  public static bool Validation3DModelInComposition(IUserSession session, long articleID)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.DocRelationTypeID);
    List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) new List<int>((IEnumerable<int>) new int[2]
    {
      MetaDataHelper.GetObjectTypeID(new Guid("cad0078f-306c-11d8-b4e9-00304f19f545")),
      MetaDataHelper.GetObjectTypeID(new Guid("cad00768-306c-11d8-b4e9-00304f19f545"))
    }));
    relationCollection.ChildObjectTypes = (IList<int>) childrenIdRecursive;
    return relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) -2
    }), articleID).Rows.Count > 0;
  }

  /// <summary>
  /// Функция проверяет изделиe articleID на наличие в составе спецификаций
  /// и возвращает True, если такие объекты найдены
  /// </summary>
  /// <param name="session"></param>
  /// <param name="articleID"></param>
  /// <returns></returns>
  public static bool ValidationSpecificationInComposition(IUserSession session, long articleID)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.DocRelationTypeID);
    List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) new List<int>((IEnumerable<int>) new int[1]
    {
      MetaDataHelper.GetObjectTypeID(new Guid("cad00133-306c-11d8-b4e9-00304f19f545"))
    }));
    relationCollection.ChildObjectTypes = (IList<int>) childrenIdRecursive;
    return relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) -2
    }), articleID).Rows.Count > 0;
  }
}
