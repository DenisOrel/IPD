
// Type: Intermech.Techcard.TechComposition
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;


namespace Intermech.Techcard;

/// <summary>Helper для работы с технологическим составом. Иногда требуется с ним работать без reference-ов на либы техкарда.
/// Например работа с технологическим составом в IMProject (см. 1219161 в BugBase)</summary>
public static class TechComposition
{
  /// <summary>Получения маршрута обработки по-умолчанию для версии объекта</summary>
  /// <param name="objVerID">Идентификатор версии объекта</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется, можно указать
  /// константу Intermech.SystemGUIDs.filtrationAllVersions</param>
  /// <param name="resultObjectIdType">Получать идентификатор объекта или версии объекта (по-умолчанию - объекта)</param>
  /// <param name="throwException">Выбрасывать ли exception при отсутствии, либо вернуть Consts.UnknownObjectId (по умолчанию - вернёт Consts.UnknownObjectId)</param>
  /// <returns>Идентификатор маршрута обработки по-умолчанию для изделия</returns>
  public static long GetDefaultProcRoute(
    long objVerID,
    [NotNull] IUserSession session,
    [CanBeNull] VersionsRule versionsRule = null,
    [CanBeNull] string filtrationOwnerID = null,
    ObjectIDType resultObjectIdType = ObjectIDType.ObjectID,
    bool throwException = false)
  {
    if (objVerID == 0L)
    {
      if (throwException)
        throw new ArgumentOutOfRangeException(LocalizationHolder.rm.GetString("ObjectIDNotSet"));
      return 0;
    }
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(TechConsts.Attr_IsDefaultProcessingRoute_ID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2),
      new ColumnDescriptor((object) -22)
    };
    DataTable dataTable = ServiceUtils.GetService<ICompositionLoadService>((object) session, true).LoadComposition((object) session.SessionGUID, objVerID, session.GetObjectInfo(objVerID).ObjectTypeID, (IEnumerable<int>) new int[1]
    {
      TechConsts.RelType_TechComposition_ID
    }, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechConsts.ObjType_ProcessingRoute_ID), (IEnumerable<ColumnDescriptor>) columns, true, false, versionsRule, (IEnumerable<ConditionStructure>) conditions, filtrationOwnerID ?? string.Empty, (HybridDictionary) null, 1);
    if (dataTable == null || dataTable.Rows.Count == 0)
    {
      if (throwException)
        throw new Exception(LocalizationHolder.GetString("ObjectDefaultProcessingRouteNotFound", (object) objVerID));
      return 0;
    }
    object obj = resultObjectIdType == ObjectIDType.ObjectVersionID ? dataTable.Rows[0][0] : dataTable.Rows[0][1];
    if (obj != null && !DBNull.Value.Equals(obj))
      return Convert.ToInt64(obj);
    if (throwException)
      throw new Exception(LocalizationHolder.GetString("ObjectDefaultProcessingRouteNotFound", (object) objVerID));
    return 0;
  }

  /// <summary>Получения маршрутов обработки для версии объекта</summary>
  /// <exception cref="T:System.Exception">Thrown when an exception error condition occurs.</exception>
  /// <param name="objVerID">Идентификатор версии объекта</param>
  /// <param name="onlyDefault">Получать только маршруты обработки по-умолчанию (по-умолчанию false)</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется, можно указать
  /// константу Intermech.SystemGUIDs.filtrationAllVersions</param>
  /// <param name="resultObjectIdType">Получать идентификатор объекта или версии объекта (по-умолчанию - объекта)</param>
  /// <param name="throwException">Выбрасывать ли exception при отсутствии, либо вернуть пустой список (по умолчанию - вернёт пустой список)</param>
  /// <returns>Идентификатор маршрута обработки по-умолчанию для изделия</returns>
  [NotNull]
  public static List<long> GetProcRoutes(
    long objVerID,
    [NotNull] IUserSession session,
    bool onlyDefault = false,
    [CanBeNull] VersionsRule versionsRule = null,
    [CanBeNull] string filtrationOwnerID = null,
    ObjectIDType resultObjectIdType = ObjectIDType.ObjectID,
    bool throwException = false)
  {
    if (objVerID == 0L)
    {
      if (throwException)
        throw new ArgumentOutOfRangeException(LocalizationHolder.rm.GetString("ObjectIDNotSet"));
      return new List<long>(0);
    }
    ConditionStructure[] conditionStructureArray;
    if (!onlyDefault)
      conditionStructureArray = Array.Empty<ConditionStructure>();
    else
      conditionStructureArray = new ConditionStructure[1]
      {
        new ConditionStructure(TechConsts.Attr_IsDefaultProcessingRoute_ID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object)
      };
    ConditionStructure[] conditions = conditionStructureArray;
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2),
      new ColumnDescriptor((object) -22)
    };
    DataTable dataTable = ServiceUtils.GetService<ICompositionLoadService>((object) session, true).LoadComposition((object) session.SessionGUID, objVerID, session.GetObjectInfo(objVerID).ObjectTypeID, (IEnumerable<int>) new int[1]
    {
      TechConsts.RelType_TechComposition_ID
    }, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechConsts.ObjType_ProcessingRoute_ID), (IEnumerable<ColumnDescriptor>) columns, true, false, versionsRule, (IEnumerable<ConditionStructure>) conditions, filtrationOwnerID ?? string.Empty, (HybridDictionary) null, 1);
    if (dataTable != null && dataTable.Rows.Count != 0)
      return dataTable.FieldAsLongListDef(resultObjectIdType == ObjectIDType.ObjectVersionID ? 0 : 1).ToList<long>(dataTable.Rows.Count);
    if (throwException)
      throw new Exception(LocalizationHolder.GetString("ProcessingRouteForObjectWithIdNotFound", (object) objVerID, onlyDefault ? (object) LocalizationHolder.rm.GetString("ByDefault") : (object) string.Empty));
    return new List<long>(0);
  }

  /// <summary>Получения расцеховки по-умолчанию для версии объекта</summary>
  /// <param name="objVerID">Идентификатор версии объекта</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется, можно указать
  /// константу Intermech.SystemGUIDs.filtrationAllVersions</param>
  /// <param name="resultObjectIdType">Получать идентификатор объекта или версии объекта (по-умолчанию - объекта)</param>
  /// <param name="throwException">Выбрасывать ли exception при отсутствии, либо вернуть Consts.UnknownObjectId (по умолчанию - вернёт
  /// Consts.UnknownObjectId)</param>
  /// <returns>Идентификатор расцеховки для изделия</returns>
  public static long GetDefaultTechRoute(
    long objVerID,
    [NotNull] IUserSession session,
    [CanBeNull] VersionsRule versionsRule = null,
    [CanBeNull] string filtrationOwnerID = null,
    ObjectIDType resultObjectIdType = ObjectIDType.ObjectID,
    bool throwException = false)
  {
    if (objVerID == 0L)
    {
      if (throwException)
        throw new ArgumentOutOfRangeException(LocalizationHolder.rm.GetString("ObjectIDNotSet"));
      return 0;
    }
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(-7, TechConsts.ObjType_ProcessingRoute_IDs.Count > 1 ? RelationalOperators.NotIn : RelationalOperators.NotEqual, TechConsts.ObjType_ProcessingRoute_IDs.Count > 1 ? (object) TechConsts.ObjType_ProcessingRoute_IDs : (object) TechConsts.ObjType_ProcessingRoute_ID, (object) null, LogicalOperators.OR, 0, false, AttributeSourceTypes.Object),
      new ConditionStructure(TechConsts.Attr_IsDefaultProcessingRoute_ID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2),
      new ColumnDescriptor((object) -22)
    };
    DataTable dataTable = ServiceUtils.GetService<ICompositionLoadService>((object) session, true).LoadComposition((object) session.SessionGUID, objVerID, session.GetObjectInfo(objVerID).ObjectTypeID, (IEnumerable<int>) new int[1]
    {
      TechConsts.RelType_TechComposition_ID
    }, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechConsts.ObjType_TechRoute_ID), (IEnumerable<ColumnDescriptor>) columns, true, false, versionsRule, (IEnumerable<ConditionStructure>) conditions, filtrationOwnerID ?? string.Empty, (HybridDictionary) null, 2, (IEnumerable<int>) Enumeration.Create<int>(session.GetObjectInfo(objVerID).ObjectTypeID).Concat<int>((IEnumerable<int>) TechConsts.ObjType_ProcessingRoute_IDs).ToArray<int>(TechConsts.ObjType_ProcessingRoute_IDs.Count + 1));
    if (dataTable == null || dataTable.Rows.Count == 0)
    {
      if (throwException)
        throw new Exception(LocalizationHolder.GetString("ObjectDefaultTechRouteNotFound", (object) objVerID));
      return 0;
    }
    object obj = resultObjectIdType == ObjectIDType.ObjectVersionID ? dataTable.Rows[0][0] : dataTable.Rows[0][1];
    if (obj != null && !DBNull.Value.Equals(obj))
      return Convert.ToInt64(obj);
    if (throwException)
      throw new Exception(LocalizationHolder.GetString("ObjectDefaultTechRouteNotFound", (object) objVerID));
    return 0;
  }

  /// <summary>Получения расцеховок для версии объекта</summary>
  /// <param name="objVerID">Идентификатор версии объекта</param>
  /// <param name="onlyDefault">Получать только маршруты обработки по-умолчанию (по-умолчанию false)</param>
  /// <param name="resultObjectIdType">Получать идентификатор объекта или версии объекта (по-умолчанию - объекта)</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется, можно указать
  /// константу Intermech.SystemGUIDs.filtrationAllVersions</param>
  /// <param name="throwException">Выбрасывать ли exception при отсутствии, либо вернуть пустой список (по умолчанию - вернёт пустой список)</param>
  /// <returns>Идентификатор расцеховки для изделия</returns>
  [NotNull]
  public static List<long> GetTechRoutes(
    long objVerID,
    [NotNull] IUserSession session,
    bool onlyDefault = false,
    [CanBeNull] VersionsRule versionsRule = null,
    [CanBeNull] string filtrationOwnerID = null,
    ObjectIDType resultObjectIdType = ObjectIDType.ObjectID,
    bool throwException = false)
  {
    if (objVerID == 0L)
    {
      if (throwException)
        throw new ArgumentOutOfRangeException(LocalizationHolder.rm.GetString("ObjectIDNotSet"));
      return new List<long>(0);
    }
    ConditionStructure[] conditionStructureArray;
    if (!onlyDefault)
      conditionStructureArray = Array.Empty<ConditionStructure>();
    else
      conditionStructureArray = new ConditionStructure[2]
      {
        new ConditionStructure(-7, TechConsts.ObjType_ProcessingRoute_IDs.Count > 1 ? RelationalOperators.NotIn : RelationalOperators.NotEqual, TechConsts.ObjType_ProcessingRoute_IDs.Count > 1 ? (object) TechConsts.ObjType_ProcessingRoute_IDs : (object) TechConsts.ObjType_ProcessingRoute_ID, (object) null, LogicalOperators.OR, 0, false, AttributeSourceTypes.Object),
        new ConditionStructure(TechConsts.Attr_IsDefaultProcessingRoute_ID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
      };
    ConditionStructure[] conditions = conditionStructureArray;
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2),
      new ColumnDescriptor((object) -22)
    };
    DataTable dataTable = ServiceUtils.GetService<ICompositionLoadService>((object) session, true).LoadComposition((object) session.SessionGUID, objVerID, session.GetObjectInfo(objVerID).ObjectTypeID, (IEnumerable<int>) new int[1]
    {
      TechConsts.RelType_TechComposition_ID
    }, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechConsts.ObjType_TechRoute_ID), (IEnumerable<ColumnDescriptor>) columns, true, false, versionsRule, (IEnumerable<ConditionStructure>) conditions, filtrationOwnerID ?? string.Empty, (HybridDictionary) null, 2, (IEnumerable<int>) Enumeration.Create<int>(session.GetObjectInfo(objVerID).ObjectTypeID).Concat<int>((IEnumerable<int>) TechConsts.ObjType_ProcessingRoute_IDs).ToArray<int>(TechConsts.ObjType_ProcessingRoute_IDs.Count + 1));
    if (dataTable != null && dataTable.Rows.Count != 0)
      return dataTable.FieldAsLongListDef(resultObjectIdType == ObjectIDType.ObjectVersionID ? 0 : 1).ToList<long>(dataTable.Rows.Count);
    if (throwException)
      throw new Exception(LocalizationHolder.GetString("TechRoutesForObjectWithIdNotFound", (object) objVerID, onlyDefault ? (object) LocalizationHolder.rm.GetString("ByDefault") : (object) string.Empty));
    return new List<long>(0);
  }

  /// <summary>Получение списка идентификаторов базовых шаблонов расцеховки в расцеховке по-умолчанию для версии объекта</summary>
  /// <param name="objVerID">Идентификатор версии объекта</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется, можно указать
  /// константу Intermech.SystemGUIDs.filtrationAllVersions</param>
  /// <param name="resultObjectIdType">Получать идентификатор объекта или версии объекта (по-умолчанию - объекта)</param>
  /// <param name="throwException">Выбрасывать ли exception при отсутствии, либо вернуть пустой список (по умолчанию - вернёт пустой список)</param>
  /// <returns>Список идентификаторов базовых шаблонов расцеховки в расцеховке по-умолчанию для версии объекта</returns>
  [NotNull]
  public static List<long> GetObjectDefaultTechRouteTemplatesList(
    long objVerID,
    [NotNull] IUserSession session,
    [CanBeNull] VersionsRule versionsRule = null,
    [CanBeNull] string filtrationOwnerID = null,
    ObjectIDType resultObjectIdType = ObjectIDType.ObjectID,
    bool throwException = false)
  {
    if (objVerID == 0L)
    {
      if (throwException)
        throw new ArgumentOutOfRangeException(LocalizationHolder.rm.GetString("ObjectIDNotSet"));
      return new List<long>(0);
    }
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(-7, TechConsts.ObjType_ProcessingRoute_IDs.Count > 1 ? RelationalOperators.NotIn : RelationalOperators.NotEqual, TechConsts.ObjType_ProcessingRoute_IDs.Count > 1 ? (object) TechConsts.ObjType_ProcessingRoute_IDs : (object) TechConsts.ObjType_ProcessingRoute_ID, (object) null, LogicalOperators.OR, 0, false, AttributeSourceTypes.Object),
      new ConditionStructure(TechConsts.Attr_IsDefaultProcessingRoute_ID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) -2),
      new ColumnDescriptor((object) -22),
      new ColumnDescriptor((object) -21)
    };
    DataTable dataTable = ServiceUtils.GetService<ICompositionLoadService>((object) session, true).LoadComposition((object) session.SessionGUID, objVerID, session.GetObjectInfo(objVerID).ObjectTypeID, (IEnumerable<int>) new int[1]
    {
      TechConsts.RelType_TechComposition_ID
    }, (IEnumerable<int>) TechConsts.ObjType_TechRouteTemplate_IDs, (IEnumerable<ColumnDescriptor>) columns, true, false, versionsRule, (IEnumerable<ConditionStructure>) conditions, filtrationOwnerID ?? string.Empty, (HybridDictionary) null, 3, (IEnumerable<int>) Enumeration.Create<int>(session.GetObjectInfo(objVerID).ObjectTypeID).Concat<int>((IEnumerable<int>) TechConsts.ObjType_ProcessingRoute_IDs).Concat<int>((IEnumerable<int>) TechConsts.ObjType_TechRoute_IDs).ToArray<int>(1 + TechConsts.ObjType_ProcessingRoute_IDs.Count + TechConsts.ObjType_TechRoute_IDs.Count));
    if (dataTable == null || dataTable.Rows.Count == 0)
    {
      if (throwException)
        throw new Exception(LocalizationHolder.GetString("TechRouteTemplatesOrTechRoutesNotFound", (object) objVerID));
      return new List<long>(0);
    }
    long firstDefaultObjectTechRouteID = dataTable.Rows[0].FieldAsLongDef(2);
    return dataTable.Where((System.Func<DataRow, bool>) (dataRow => dataRow.FieldAsLongDef(2) == firstDefaultObjectTechRouteID)).FieldAsLongEnumerationDef(resultObjectIdType == ObjectIDType.ObjectVersionID ? 0 : 1).ToList<long>(dataTable.Rows.Count);
  }

  /// <summary>Получение списка дескрипторов базовых шаблонов расцеховки в расцеховках для версии объекта</summary>
  /// <param name="objVerID">Идентификатор версии объекта</param>
  /// <param name="onlyDefault">Получать только маршруты обработки по-умолчанию (по-умолчанию false)</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется, можно указать
  /// константу Intermech.SystemGUIDs.filtrationAllVersions</param>
  /// <param name="resultObjectIdType">Получать идентификатор объекта или версии объекта (по-умолчанию - объекта)</param>
  /// <param name="throwException">Выбрасывать ли exception при отсутствии, либо вернуть пустой список (по умолчанию - вернёт пустой список)</param>
  /// <returns>Список дескрипторов базовых шаблонов расцеховки в расцеховках для версии объекта</returns>
  [NotNull]
  public static List<TechComposition.TechRouteTemplate> GetObjectTechRouteTemplatesList(
    long objVerID,
    [NotNull] IUserSession session,
    bool onlyDefault = false,
    [CanBeNull] VersionsRule versionsRule = null,
    [CanBeNull] string filtrationOwnerID = null,
    ObjectIDType resultObjectIdType = ObjectIDType.ObjectID,
    bool throwException = false)
  {
    if (objVerID == 0L)
    {
      if (throwException)
        throw new ArgumentOutOfRangeException(LocalizationHolder.rm.GetString("ObjectIDNotSet"));
      return new List<TechComposition.TechRouteTemplate>(0);
    }
    ConditionStructure[] conditionStructureArray;
    if (!onlyDefault)
      conditionStructureArray = Array.Empty<ConditionStructure>();
    else
      conditionStructureArray = new ConditionStructure[2]
      {
        new ConditionStructure(-7, TechConsts.ObjType_ProcessingRoute_IDs.Count > 1 ? RelationalOperators.NotIn : RelationalOperators.NotEqual, TechConsts.ObjType_ProcessingRoute_IDs.Count > 1 ? (object) TechConsts.ObjType_ProcessingRoute_IDs : (object) TechConsts.ObjType_ProcessingRoute_ID, (object) null, LogicalOperators.OR, 0, false, AttributeSourceTypes.Object),
        new ConditionStructure(TechConsts.Attr_IsDefaultProcessingRoute_ID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
      };
    ConditionStructure[] conditions = conditionStructureArray;
    ColumnDescriptor[] columns = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) -2),
      new ColumnDescriptor((object) -22),
      new ColumnDescriptor((object) -21)
    };
    DataTable dataTable = ServiceUtils.GetService<ICompositionLoadService>((object) session, true).LoadComposition((object) session.SessionGUID, objVerID, session.GetObjectInfo(objVerID).ObjectTypeID, (IEnumerable<int>) new int[1]
    {
      TechConsts.RelType_TechComposition_ID
    }, (IEnumerable<int>) TechConsts.ObjType_TechRouteTemplate_IDs, (IEnumerable<ColumnDescriptor>) columns, true, false, versionsRule, (IEnumerable<ConditionStructure>) conditions, filtrationOwnerID ?? string.Empty, (HybridDictionary) null, 3, (IEnumerable<int>) Enumeration.Create<int>(session.GetObjectInfo(objVerID).ObjectTypeID).Concat<int>((IEnumerable<int>) TechConsts.ObjType_ProcessingRoute_IDs).Concat<int>((IEnumerable<int>) TechConsts.ObjType_TechRoute_IDs).ToArray<int>(1 + TechConsts.ObjType_ProcessingRoute_IDs.Count + TechConsts.ObjType_TechRoute_IDs.Count));
    if (dataTable != null && dataTable.Rows.Count != 0)
      return dataTable.Rows.Select<TechComposition.TechRouteTemplate>((System.Func<DataRow, TechComposition.TechRouteTemplate>) (dataRow => new TechComposition.TechRouteTemplate(dataRow.FieldAsLongDef(2), dataRow.FieldAsLongDef(resultObjectIdType == ObjectIDType.ObjectVersionID ? 0 : 1)))).ToList<TechComposition.TechRouteTemplate>(dataTable.Rows.Count);
    if (throwException)
      throw new Exception(LocalizationHolder.GetString("TechRouteTemplatesOrTechRoutesNotFound2", (object) objVerID, onlyDefault ? (object) LocalizationHolder.rm.GetString("ByDefault") : (object) string.Empty));
    return new List<TechComposition.TechRouteTemplate>(0);
  }

  /// <summary>Получение дескрипторов расцеховочных элементов в расцеховке по-умолчанию для версии объекта</summary>
  /// <param name="objVerID">Идентификатор версии объекта</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется, можно указать
  /// константу Intermech.SystemGUIDs.filtrationAllVersions</param>
  /// <param name="resultObjectIdType">Получать идентификатор объекта или версии объекта (по-умолчанию - объекта)</param>
  /// <param name="throwException">Выбрасывать ли exception при отсутствии, либо вернуть пустой список (по умолчанию - вернёт пустой список)</param>
  /// <returns>Список дескрипторов расцеховочных элементов в расцеховке по-умолчанию для версии объекта</returns>
  [NotNull]
  public static List<TechComposition.TechRouteElement> GetObjectDefaultTechRouteElements(
    long objVerID,
    [NotNull] IUserSession session,
    [CanBeNull] VersionsRule versionsRule = null,
    [CanBeNull] string filtrationOwnerID = null,
    ObjectIDType resultObjectIdType = ObjectIDType.ObjectID,
    bool throwException = false)
  {
    if (objVerID == 0L)
    {
      if (throwException)
        throw new ArgumentOutOfRangeException(LocalizationHolder.rm.GetString("ObjectIDNotSet"));
      return new List<TechComposition.TechRouteElement>(0);
    }
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(-7, TechConsts.ObjType_ProcessingRoute_IDs.Count > 1 ? RelationalOperators.NotIn : RelationalOperators.NotEqual, TechConsts.ObjType_ProcessingRoute_IDs.Count > 1 ? (object) TechConsts.ObjType_ProcessingRoute_IDs : (object) TechConsts.ObjType_ProcessingRoute_ID, (object) null, LogicalOperators.OR, 0, false, AttributeSourceTypes.Object),
      new ConditionStructure(TechConsts.Attr_IsDefaultProcessingRoute_ID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[4]
    {
      new ColumnDescriptor((object) -2),
      new ColumnDescriptor((object) -22),
      new ColumnDescriptor((object) -21),
      new ColumnDescriptor((object) -7)
    };
    DataTable dataTable = ServiceUtils.GetService<ICompositionLoadService>((object) session, true).LoadComposition((object) session.SessionGUID, objVerID, session.GetObjectInfo(objVerID).ObjectTypeID, (IEnumerable<int>) new int[1]
    {
      TechConsts.RelType_TechComposition_ID
    }, (IEnumerable<int>) TechConsts.ObjType_TechRouteTemplate_IDs.Concat<int>((IEnumerable<int>) TechConsts.ObjType_TechRouteElement_IDs).ToArray<int>(TechConsts.ObjType_TechRouteTemplate_IDs.Count + TechConsts.ObjType_TechRouteElement_IDs.Count), (IEnumerable<ColumnDescriptor>) columns, true, false, versionsRule, (IEnumerable<ConditionStructure>) conditions, filtrationOwnerID ?? string.Empty, (HybridDictionary) null, 4, (IEnumerable<int>) Enumeration.Create<int>(session.GetObjectInfo(objVerID).ObjectTypeID).Concat<int>((IEnumerable<int>) TechConsts.ObjType_ProcessingRoute_IDs).Concat<int>((IEnumerable<int>) TechConsts.ObjType_TechRoute_IDs).Concat<int>((IEnumerable<int>) TechConsts.ObjType_TechRouteTemplate_IDs).ToArray<int>(1 + TechConsts.ObjType_ProcessingRoute_IDs.Count + TechConsts.ObjType_TechRoute_IDs.Count + TechConsts.ObjType_TechRouteTemplate_IDs.Count));
    if (dataTable == null || dataTable.Rows.Count == 0)
    {
      if (throwException)
        throw new Exception(LocalizationHolder.GetString("TechRouteElementsOrTechRoutesNotFound", (object) objVerID));
      return new List<TechComposition.TechRouteElement>(0);
    }
    long firstDefaultObjectTechRouteID = 0;
    Dictionary<long, long> techRouteTemplateId2TechRouteID = dataTable.Rows.Where((System.Func<DataRow, bool>) (dataRow =>
    {
      if (!TechConsts.TypeIsTechRouteTemplate(dataRow.FieldAsIntDef(3)))
        return false;
      if (firstDefaultObjectTechRouteID != 0L)
        return dataRow.FieldAsLongDef(2) == firstDefaultObjectTechRouteID;
      firstDefaultObjectTechRouteID = dataRow.FieldAsLongDef(2);
      return true;
    })).ToDictionary<DataRow, long, long>((System.Func<DataRow, long>) (dataRow => dataRow.FieldAsLongDef(0)), (System.Func<DataRow, long>) (dataRow => dataRow.FieldAsLongDef(2)));
    if (!techRouteTemplateId2TechRouteID.IsEmpty<KeyValuePair<long, long>>())
      return dataTable.Rows.Where((System.Func<DataRow, bool>) (dataRow => TechConsts.TypeIsTechRouteElement(dataRow.FieldAsIntDef(3)) && techRouteTemplateId2TechRouteID.ContainsKey(dataRow.FieldAsLongDef(2)))).Select<DataRow, TechComposition.TechRouteElement>((System.Func<DataRow, TechComposition.TechRouteElement>) (dataRow =>
      {
        long num = dataRow.FieldAsLongDef(2);
        return new TechComposition.TechRouteElement(techRouteTemplateId2TechRouteID[num], num, dataRow.FieldAsLongDef(resultObjectIdType == ObjectIDType.ObjectVersionID ? 0 : 1));
      })).ToList<TechComposition.TechRouteElement>(dataTable.Rows.Count);
    if (throwException)
      throw new Exception(LocalizationHolder.GetString("TechRouteTemplatesOrTechRoutesNotFound", (object) objVerID));
    return new List<TechComposition.TechRouteElement>(0);
  }

  /// <summary>Идентификатор расцеховки + идентификатор шаблона расцеховки базового</summary>
  public class TechRouteTemplate
  {
    public readonly long TechRouteID;
    public readonly long TechRouteTemplateID;

    private TechRouteTemplate()
    {
    }

    public TechRouteTemplate(long techRouteID, long techRouteTemplateID)
    {
      this.TechRouteID = techRouteID;
      this.TechRouteTemplateID = techRouteTemplateID;
    }
  }

  /// <summary>Идентификатор расцеховки + идентификатор шаблона расцеховки + идентификатор расцеховочного элемента</summary>
  public class TechRouteElement
  {
    public readonly long TechRouteID;
    public readonly long TechRouteTemplateID;
    public readonly long TechRouteElementID;

    private TechRouteElement()
    {
    }

    public TechRouteElement(long techRouteID, long techRouteTemplateID, long techRouteElementID)
    {
      this.TechRouteID = techRouteID;
      this.TechRouteTemplateID = techRouteTemplateID;
      this.TechRouteElementID = techRouteElementID;
    }
  }
}
