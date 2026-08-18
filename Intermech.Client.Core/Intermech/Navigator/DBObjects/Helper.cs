
// Type: Intermech.Navigator.DBObjects.Helper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Содержит методы, полезные при создании частей элементов навигации,
/// работающих с объектами базы данных.
/// </summary>
public sealed class Helper
{
  /// <summary>Коллекция колонок для всех атрибутов объектов</summary>
  internal static NodeColumnCollection allObjColumns = new NodeColumnCollection();
  /// <summary>Коллекция колонок для всех атрибутов связей</summary>
  internal static NodeColumnCollection allRelColumns = new NodeColumnCollection();

  public static object MapColumnToFieldName(NodeColumn column)
  {
    if (column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && column.ID.Equals((object) "F_CAPTION"))
      return (object) ObligatoryObjectAttributes.CAPTION;
    return column.SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid ? column.ID : (object) null;
  }

  public static string GetAddress(long objID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objID).Caption;
  }

  /// <summary>
  /// Возвращает адрес объекта базы данных, который будет выводиться в
  /// адресной строке навигатора.
  /// </summary>
  /// <param name="nodeID">Идентификатор, описывающий объект базы данных</param>
  /// <returns>Адрес объекта базы данны</returns>
  public static string GetAddress(INodeID nodeID) => Helper.GetAddress((nodeID as NodeID).ObjectID);

  /// <summary>
  /// Добавляет в коллекцию виртуальные колонки навигатора,
  /// соответствующие обязательным атрибутам объекта. С помощью параметра
  /// <paramref name="includeCaption" /> можно управлять появлением в
  /// коллекции атрибута CAPTION.
  /// </summary>
  /// <remarks>
  /// Добавлять атрибут CAPTION необходимо при сборке коллекции
  /// колонок для списка объектов, тип которых неизвестен, т.к. кроме
  /// обязательных атрибутов в коллекцию больше ничего не попадет.
  /// </remarks>
  /// <param name="columns">Коллекция виртуальных колонок</param>
  /// <param name="includeCaption">Признак необходимости добавления атрибута CAPTION.</param>
  /// <param name="includeType"></param>
  public static void AddObligatoryColumns(
    NodeColumnCollection columns,
    bool includeCaption,
    bool includeType)
  {
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    if (includeType)
      columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE), 120);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID), 75);
    if (includeCaption)
      columns.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION, NodeColumnSortOrder.Ascending, 0), 345);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_OWNER_ID), includeCaption ? 100 : 130);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_CHKOUT_BY), includeCaption ? 100 : 130);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_LEVEL_ID), includeCaption ? 100 : 190);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_PROJECT_ID), 100);
    if (!((ICurrentUserAndRole) ServicesManager.GetService(typeof (ICurrentUserAndRole))).PortalClient)
      return;
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_SITE_ID), includeCaption ? 100 : 190);
  }

  /// <summary>
  /// Добавляет в коллекцию дополнительные виртуальные колонки навигатора,
  /// соответствующие обязательным атрибутам объекта. С помощью параметра
  /// </summary>
  /// <param name="columns">Коллекция виртуальных колонок</param>
  public static void AddObligatoryColumnsAdv(NodeColumnCollection columns)
  {
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJ_CREATE), 125);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_GUID), 150);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_ID), 100);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_VERSION_ID), 100);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_LC_STEP), 150);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_MODIFICATION_ID), 100);
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_CREATOR_ID), 150);
    if (((ICurrentUserAndRole) ServicesManager.GetService(typeof (ICurrentUserAndRole))).PortalClient)
      return;
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_SITE_ID), 100);
  }

  /// <summary>
  /// Добавляет в коллекцию виртуальные колонки навигатора, соответствующие обязательным атрибутам связей.
  /// </summary>
  /// <param name="columns">Коллекция виртуальных колонок</param>
  public static void AddObligatoryColumnsRelation(NodeColumnCollection columns)
  {
    Guid columnSchemeGuid = Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    columns.Add(Holder.ColumnSchemes.CreateColumn(Intermech.Navigator.Consts.NavigatorColumnSchemeGuid, (object) "F_STATUSES"), 100);
  }

  /// <summary>
  /// Добавляет в коллекцию дополнительные виртуальные колонки навигатора, соответствующие обязательным атрибутам связей.
  /// </summary>
  /// <param name="columns">Коллекция виртуальных колонок</param>
  public static void AddObligatoryColumnsRelationAdv(NodeColumnCollection columns)
  {
    Guid columnSchemeGuid = Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    columns.Add(Holder.ColumnSchemes.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_PRJLINK_ID), 65);
    columns.Add(Holder.ColumnSchemes.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_PROJ_ID), 65);
    columns.Add(Holder.ColumnSchemes.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_PART_ID), 65);
    columns.Add(Holder.ColumnSchemes.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_RELATION_TYPE), 90);
    columns.Add(Holder.ColumnSchemes.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_REL_CREATOR), 150);
  }

  /// <summary>
  /// Добавляет в коллекцию виртуальные колонки навигатора, соответствующие
  /// атрибутам, назначенным указанному с помощью
  /// <paramref name="objTypeID" /> типу объекта.
  /// </summary>
  /// <param name="columns">Коллекция виртуальных колонок</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  public static void AddObjectTypeColumns(NodeColumnCollection columns, int objTypeID)
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectColumnSchemeGuid;
    List<IMSAttributeType> attributesForObjectType = Helper.GetAttributesForObjectType(objTypeID);
    for (int index = 0; index < attributesForObjectType.Count; ++index)
    {
      if (!columns.ColumnIDExists((object) attributesForObjectType[index].AttributeID, columnSchemeGuid) && !columns.ColumnIDExists((object) attributesForObjectType[index].AttributeID, Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid))
      {
        NodeColumn column = service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) attributesForObjectType[index].AttributeID);
        if (column != null && !columns.Contains(column))
          columns.Add(column);
      }
    }
  }

  /// <summary>
  /// Добавляет в коллекцию виртуальные колонки навигатора, соответствующие
  /// атрибутам, назначенным указанному с помощью
  /// <paramref name="relTypeID" /> типу связи.
  /// </summary>
  /// <param name="columns">Коллекция виртуальных колонок</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  public static void AddRelationTypeColumns(NodeColumnCollection columns, int relTypeID)
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    Guid columnSchemeGuid = Intermech.Navigator.Consts.RelationColumnSchemeGuid;
    List<IMSAttributeType> attributesForRelationType = Helper.GetAttributesForRelationType(relTypeID);
    for (int index = 0; index < attributesForRelationType.Count; ++index)
    {
      if (!columns.ColumnIDExists((object) attributesForRelationType[index].AttributeID, columnSchemeGuid) && !columns.ColumnIDExists((object) attributesForRelationType[index].AttributeID, Intermech.Navigator.Consts.CurrentRelationColumnSchemeGuid))
      {
        NodeColumn column = service.CreateColumn(Intermech.Navigator.Consts.CurrentRelationColumnSchemeGuid, (object) attributesForRelationType[index].AttributeID);
        if (column != null && !columns.Contains(column))
          columns.Add(column);
      }
    }
  }

  /// <summary>
  /// Статический метод для очистки кэша колонок.
  /// Рекомендуется вызывать после того, как с атрибутами
  /// поработали в DatabaseConfigurator
  /// </summary>
  public static void ClearNodeColumnsCache()
  {
    Helper.allObjColumns.Clear();
    Helper.allRelColumns.Clear();
  }

  /// <summary>Перечитать кэш колонок "Навигатора"</summary>
  private static void ReloadColumnsCache()
  {
    Helper.ClearNodeColumnsCache();
    List<IMSAttributeType> attributeTypesList = MetaDataHelper.GetAttributeTypesList();
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    for (int index = 0; index < attributeTypesList.Count; ++index)
    {
      bool flag = ObligatoryObjectAttributesHelper.IsObligatoryAttribute(attributeTypesList[index].AttributeID);
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeTypesList[index].AttributeID);
      if (attributeType != null && attributeType.IsGridable && (!flag || ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeTypesList[index].AttributeID) == AttributeSourceTypes.Object || ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeTypesList[index].AttributeID) == AttributeSourceTypes.Relation))
      {
        if (flag && ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeTypesList[index].AttributeID) == AttributeSourceTypes.Object || !flag)
        {
          NodeColumn column = service.CreateColumn(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) attributeTypesList[index].AttributeID);
          if (column != null)
            Helper.allObjColumns.Add(column);
        }
        if (flag && ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeTypesList[index].AttributeID) == AttributeSourceTypes.Relation || !flag)
        {
          NodeColumn column = service.CreateColumn(Intermech.Navigator.Consts.RelationColumnSchemeGuid, (object) attributeTypesList[index].AttributeID);
          if (column != null)
            Helper.allRelColumns.Add(column);
        }
      }
    }
    attributeTypesList.Clear();
  }

  /// <summary>
  /// Добавляет в коллекцию виртуальные колонки навигатора, соответствующие
  /// всем существующим в базе данных атрибутам.
  /// </summary>
  /// <remarks>
  /// Этот метод полезен при реализации метода
  /// <c>INodePart.GetSupportedColumns</c> в случае, когда тип объекта или
  /// тип связи допускает появление атрибутов любого типа.
  /// </remarks>
  /// <param name="columns">Коллекция виртуальных колонок</param>
  public static void AddAllColumns(NodeColumnCollection columns)
  {
    if (Helper.allObjColumns.Count == 0)
      Helper.ReloadColumnsCache();
    Guid columnSchemeGuid1 = Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid;
    Guid columnSchemeGuid2 = Intermech.Navigator.Consts.ObjectColumnSchemeGuid;
    for (int index = 0; index < Helper.allObjColumns.Count; ++index)
    {
      if (!columns.ColumnIDExists(Helper.allObjColumns[index].ID, columnSchemeGuid1) && !columns.ColumnIDExists(Helper.allObjColumns[index].ID, columnSchemeGuid2))
        columns.Add(Helper.allObjColumns[index].Clone() as NodeColumn);
    }
  }

  /// <summary>
  /// Добавляет в коллекцию виртуальные колонки навигатора, соответствующие
  /// всем существующим в базе данных атрибутам.
  /// </summary>
  /// <remarks>
  /// Этот метод полезен при реализации метода
  /// <c>INodePart.GetSupportedColumns</c> в случае, когда тип связи допускает появление атрибутов любого типа.
  /// </remarks>
  /// <param name="columns">Коллекция виртуальных колонок</param>
  public static void AddAllColumnsRelation(NodeColumnCollection columns)
  {
    if (Helper.allRelColumns.Count == 0)
      Helper.ReloadColumnsCache();
    Guid columnSchemeGuid1 = Intermech.Navigator.Consts.CurrentRelationColumnSchemeGuid;
    Guid columnSchemeGuid2 = Intermech.Navigator.Consts.RelationColumnSchemeGuid;
    for (int index = 0; index < Helper.allRelColumns.Count; ++index)
    {
      if (!columns.ColumnIDExists(Helper.allRelColumns[index].ID, columnSchemeGuid1) && !columns.ColumnIDExists(Helper.allRelColumns[index].ID, columnSchemeGuid2))
        columns.Add(Helper.allRelColumns[index].Clone() as NodeColumn);
    }
  }

  /// <summary>
  /// Выполнить конвертацию "Тип данных атрибута" - "Тип данных .NET"
  /// </summary>
  /// <param name="fieldType">Тип данных атрибута</param>
  /// <returns>Тип данных .NET</returns>
  public static Type ConvertType(FieldTypes fieldType)
  {
    switch (fieldType)
    {
      case FieldTypes.ftString:
        return typeof (string);
      case FieldTypes.ftInteger:
        return typeof (long);
      case FieldTypes.ftDouble:
        return typeof (double);
      case FieldTypes.ftDateTime:
        return typeof (DateTime);
      case FieldTypes.ftShortBlob:
        return typeof (string);
      case FieldTypes.ftFile:
        return typeof (string);
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        return typeof (string);
      case FieldTypes.ftMemo:
        return typeof (string);
      case FieldTypes.ftBlob:
        return typeof (string);
      case FieldTypes.ftBoolean:
        return typeof (bool);
      case FieldTypes.ftMeasured:
        return typeof (string);
      case FieldTypes.ftAutoInc:
        return typeof (long);
      default:
        return (Type) null;
    }
  }

  /// <summary>
  /// Получить тип данных .NET для указанного типа системного атрибута
  /// </summary>
  /// <param name="columnID">Идентификатор системного атрибута</param>
  /// <returns>Тип данных .NET</returns>
  public static Type GetColumnType(ObligatoryObjectAttributes columnID)
  {
    switch (columnID)
    {
      case ObligatoryObjectAttributes.F_REL_CREATOR:
        return typeof (long);
      case ObligatoryObjectAttributes.F_CREATOR_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_ACCESS:
        return typeof (int);
      case ObligatoryObjectAttributes.F_ELEMENT_STATUSES:
        return typeof (byte[]);
      case ObligatoryObjectAttributes.F_OBJECTLINK_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_ZIPSIZE:
        return typeof (long);
      case ObligatoryObjectAttributes.F_FILEDATE:
        return typeof (DateTime);
      case ObligatoryObjectAttributes.F_FILESIZE:
        return typeof (long);
      case ObligatoryObjectAttributes.F_FILENAME:
        return typeof (string);
      case ObligatoryObjectAttributes.F_FILE_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.CAPTION:
        return typeof (string);
      case ObligatoryObjectAttributes.F_NOTE:
        return typeof (string);
      case ObligatoryObjectAttributes.F_OBJECT_NAME:
        return typeof (string);
      case ObligatoryObjectAttributes.F_DELETE_DATE:
        return typeof (DateTime);
      case ObligatoryObjectAttributes.F_CREATE_DATE:
        return typeof (DateTime);
      case ObligatoryObjectAttributes.F_RELATION_TYPE:
        return typeof (int);
      case ObligatoryObjectAttributes.F_PART_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_PROJ_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_PRJLINK_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_MODIFICATION_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_PROJECT_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_OBJ_CREATE:
        return typeof (DateTime);
      case ObligatoryObjectAttributes.F_GUID:
        return typeof (string);
      case ObligatoryObjectAttributes.F_MODIFY_DATE:
        return typeof (DateTime);
      case ObligatoryObjectAttributes.F_LEVEL_ID:
        return typeof (int);
      case ObligatoryObjectAttributes.F_OWNER_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_OBJECT_TYPE:
        return typeof (int);
      case ObligatoryObjectAttributes.F_CHKOUT_BY:
        return typeof (long);
      case ObligatoryObjectAttributes.F_VERSION_ID:
        return typeof (int);
      case ObligatoryObjectAttributes.F_LC_STEP:
        return typeof (int);
      case ObligatoryObjectAttributes.F_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_OBJECT_ID:
        return typeof (long);
      default:
        return (Type) null;
    }
  }

  /// <summary>
  /// Получить тип данных атрибута по указанному идентификатору системного атрибута
  /// </summary>
  /// <param name="columnID">Идентификатор системного атрибута</param>
  /// <returns>Тип данных атрибута</returns>
  public static FieldTypes GetColumnAttrType(ObligatoryObjectAttributes columnID)
  {
    return AttributeCacheHelper.GetColumnAttrType(columnID);
  }

  /// <summary>Вернуть атрибуты для указанного типа объекта</summary>
  /// <param name="keeper">Сессия</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Атрибуты для указанного типа объекта</returns>
  public static List<int> GetTypesForObjectType(SessionKeeper keeper, int objTypeID)
  {
    IDBObjectType objectType = keeper.Session.GetObjectType(objTypeID, false);
    if (objectType == null)
      return new List<int>();
    IDBCollection attributes = (IDBCollection) objectType.Attributes;
    return Helper.GetTypesFromCollection(keeper, attributes);
  }

  /// <summary>Вернуть атрибуты для указанного типа объекта</summary>
  /// <param name="keeper">Сессия</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Атрибуты для указанного типа объекта</returns>
  public static List<IMSAttributeType> GetAttributesForObjectType(int objTypeID)
  {
    IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(objTypeID, false);
    return objectType == null ? new List<IMSAttributeType>() : Helper.GetAttributesFromCollection(objectType.Attributes as IDBCollection);
  }

  /// <summary>Получить атрибуты для указанного типа связи</summary>
  /// <param name="keeper">Сессия</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <returns>Атрибуты для указанного типа связи</returns>
  public static List<int> GetTypesForRelationType(SessionKeeper keeper, int relTypeID)
  {
    IDBRelationType relationType = keeper.Session.GetRelationType(relTypeID, false);
    if (relationType == null)
      return new List<int>();
    IDBCollection attributes = (IDBCollection) relationType.Attributes;
    return Helper.GetTypesFromCollection(keeper, attributes);
  }

  /// <summary>Получить атрибуты для указанного типа связи</summary>
  /// <param name="keeper">Сессия</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <returns>Атрибуты для указанного типа связи</returns>
  public static List<IMSAttributeType> GetAttributesForRelationType(int relTypeID)
  {
    IDBRelationTypeInfo relationType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetRelationType(relTypeID, false);
    return relationType == null ? new List<IMSAttributeType>() : Helper.GetAttributesFromCollection(relationType.Attributes as IDBCollection);
  }

  /// <summary>
  /// Получить коллекцию атрибутов, которые могут отображаться в гриде
  /// </summary>
  /// <param name="keeper">Сессия</param>
  /// <param name="attributeTypesCollection">Коллекция атрибутов</param>
  /// <returns>Коллекция атрибутов, которые могут отображаться в гриде</returns>
  private static List<int> GetTypesFromCollection(
    SessionKeeper keeper,
    IDBCollection attributeTypesCollection)
  {
    DataTable dataTable = attributeTypesCollection.Select("");
    List<int> typesFromCollection = new List<int>();
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      int int32 = Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"]);
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(int32);
      if (attributeType != null && attributeType.IsGridable && Helper.ConvertType(attributeType.RealFieldType) != (Type) null)
        typesFromCollection.Add(int32);
    }
    dataTable.Dispose();
    return typesFromCollection;
  }

  /// <summary>
  /// Получить коллекцию атрибутов, которые могут отображаться в гриде
  /// </summary>
  /// <param name="keeper">Сессия</param>
  /// <param name="attributeTypesCollection">Коллекция атрибутов</param>
  /// <returns>Коллекция атрибутов, которые могут отображаться в гриде</returns>
  private static List<IMSAttributeType> GetAttributesFromCollection(
    IDBCollection attributeTypesCollection)
  {
    DataTable dataTable = attributeTypesCollection.Select("");
    List<IMSAttributeType> attributesFromCollection = new List<IMSAttributeType>();
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"]));
      if (attributeType != null && attributeType.IsGridable && Helper.ConvertType(attributeType.RealFieldType) != (Type) null)
        attributesFromCollection.Add(attributeType);
    }
    dataTable.Dispose();
    return attributesFromCollection;
  }
}
