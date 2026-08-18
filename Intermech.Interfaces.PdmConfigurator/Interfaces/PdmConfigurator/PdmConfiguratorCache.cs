// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmConfiguratorCache
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Класс, организующий кэши, используемые конфигуратором составов IPS
/// </summary>
public static class PdmConfiguratorCache
{
  /// <summary>Объект для синхронизации</summary>
  private static object syncRoot = new object();
  /// <summary>
  /// Кэш описаний опций.
  /// [Guid опции] =&gt; [Описание указанной опции]
  /// </summary>
  public static Dictionary<Guid, OptionHolder> OptionsCache = new Dictionary<Guid, OptionHolder>();
  /// <summary>
  /// Кэш описаний опций.
  /// [F_OBJECT_ID опции] =&gt; [Описание указанной опции]
  /// </summary>
  public static Dictionary<long, OptionHolder> OptionsCacheID = new Dictionary<long, OptionHolder>();
  /// <summary>
  /// Кэш категорий опций.
  /// [F_OBJECT_ID категории опции] =&gt; [Описание указанной категории]
  /// </summary>
  public static Dictionary<long, OptionObjectDescription> CategoriesCache = new Dictionary<long, OptionObjectDescription>();
  /// <summary>
  /// Словарь для преобразования Int64-идентификаторов версий объектов в Guid
  /// </summary>
  public static Dictionary<long, Guid> IDToGuids = new Dictionary<long, Guid>();

  /// <summary>Очистить содержимое кэша конфигуратора составов IPS</summary>
  public static void CacheClear()
  {
    lock (PdmConfiguratorCache.syncRoot)
    {
      PdmConfiguratorCache.IDToGuids.Clear();
      PdmConfiguratorCache.OptionsCache.Clear();
      PdmConfiguratorCache.OptionsCacheID.Clear();
    }
  }

  /// <summary>Загрузить в кэш все опции системы</summary>
  /// <param name="session">Сессия</param>
  public static void CacheLoadOptions(IUserSession session)
  {
    PdmConfiguratorCache.CacheClear();
    if (session == null)
      return;
    DataTable dataTable = (DataTable) null;
    ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[OptionHolder.GetSelectColumns().Count];
    OptionHolder.GetSelectColumns().CopyTo(columnDescriptorArray);
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[0], columnDescriptorArray);
    IDBObjectCollection objectCollection = session.GetObjectCollection(Consts.objtypeOptionID);
    try
    {
      if (objectCollection != null)
        dataTable = objectCollection.Select(paramSet);
    }
    catch
    {
    }
    if (dataTable == null)
      return;
    try
    {
      dataTable.ExtendedProperties[(object) "IUserSession"] = (object) session;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        OptionHolder option = new OptionHolder((object) dataTable.Rows[index]);
        if (!option.OptionGuid.Equals(Guid.Empty) && option.OptionObjectID != 0L)
          PdmConfiguratorCache.CacheAddOption(option);
      }
    }
    finally
    {
      dataTable.ExtendedProperties[(object) "IUserSession"] = (object) null;
      dataTable.Dispose();
    }
  }

  /// <summary>Загрузить в кэш указанные опции системы</summary>
  /// <param name="session">Сессия</param>
  /// <param name="options">Список идентификаторов версий объектов-опций</param>
  public static void CacheLoadOptions(IUserSession session, IList<long> options)
  {
    if (session == null || options == null || options.Count == 0)
      return;
    List<long> longList = new List<long>();
    for (int index = 0; index < options.Count; ++index)
    {
      if (longList.IndexOf(options[index]) < 0)
        longList.Add(options[index]);
    }
    if (longList.Count == 0)
      return;
    longList.Sort();
    DataTable dataTable = (DataTable) null;
    ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[OptionHolder.GetSelectColumns().Count];
    OptionHolder.GetSelectColumns().CopyTo(columnDescriptorArray);
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, longList.Count == 1 ? RelationalOperators.Equal : RelationalOperators.In, longList.Count == 1 ? (object) longList[0] : (object) longList.ToArray(), (object) null, LogicalOperators.NONE, 0, true, AttributeSourceTypes.Object, ColumnContents.Text)
    }, columnDescriptorArray);
    IDBObjectCollection objectCollection = session.GetObjectCollection(Consts.objtypeOptionID);
    try
    {
      if (objectCollection != null)
        dataTable = objectCollection.Select(paramSet);
    }
    catch
    {
    }
    if (dataTable == null)
      return;
    try
    {
      dataTable.ExtendedProperties[(object) "IUserSession"] = (object) session;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        OptionHolder option = new OptionHolder((object) dataTable.Rows[index]);
        if (!option.OptionGuid.Equals(Guid.Empty) && option.OptionObjectID != 0L)
          PdmConfiguratorCache.CacheAddOption(option);
      }
    }
    finally
    {
      dataTable.ExtendedProperties[(object) "IUserSession"] = (object) null;
      dataTable.Dispose();
    }
  }

  /// <summary>Загрузить в кэш все категории системы</summary>
  /// <param name="session">Сессия</param>
  public static void CacheLoadCategories(IUserSession session)
  {
    lock (PdmConfiguratorCache.syncRoot)
      PdmConfiguratorCache.CategoriesCache.Clear();
    if (session == null)
      return;
    List<object> objectList = ObjectVersionDescriptionsHelper.LoadDescriptions(session, typeof (OptionObjectDescription), Consts.objtypeOptionsGroupID);
    lock (PdmConfiguratorCache.syncRoot)
    {
      for (int index = 0; index < objectList.Count; ++index)
      {
        OptionObjectDescription objectDescription = (OptionObjectDescription) objectList[index];
        PdmConfiguratorCache.CategoriesCache[objectDescription.F_OBJECT_ID] = objectDescription;
      }
    }
  }

  /// <summary>
  /// Вернуть список всех категорий. В начале списка будет объект "Нет категории"
  /// </summary>
  /// <returns>Список всех категорий. В начале списка будет объект "Нет категории"</returns>
  public static List<OptionObjectDescription> CacheGetCategoriesList()
  {
    lock (PdmConfiguratorCache.syncRoot)
    {
      List<OptionObjectDescription> categoriesList = new List<OptionObjectDescription>((IEnumerable<OptionObjectDescription>) PdmConfiguratorCache.CategoriesCache.Values);
      for (int index = 0; index < categoriesList.Count; ++index)
      {
        OptionObjectDescription objectDescription = categoriesList[index];
        if (objectDescription.F_OBJECT_ID == Consts.objectNoCategoryID)
        {
          categoriesList.RemoveAt(index);
          categoriesList.Sort();
          categoriesList.Insert(0, objectDescription);
          break;
        }
      }
      return categoriesList;
    }
  }

  /// <summary>Отыскать в кэше категорию</summary>
  /// <param name="category">Идентификатор версии объекта категории</param>
  /// <returns>Описание категории или null</returns>
  public static OptionObjectDescription CacheFindCategory(long category)
  {
    lock (PdmConfiguratorCache.syncRoot)
      return PdmConfiguratorCache.CategoriesCache.ContainsKey(category) ? PdmConfiguratorCache.CategoriesCache[category] : (OptionObjectDescription) null;
  }

  /// <summary>Найти в кэше описание опции</summary>
  /// <param name="option">Идентификатор версии объекта опции</param>
  /// <returns>Описание опции или null</returns>
  public static OptionHolder CacheFindOption(long option)
  {
    lock (PdmConfiguratorCache.syncRoot)
      return PdmConfiguratorCache.OptionsCacheID.ContainsKey(option) ? PdmConfiguratorCache.OptionsCacheID[option] : (OptionHolder) null;
  }

  /// <summary>Найти в кэше описание опции</summary>
  /// <param name="option">Guid опции</param>
  /// <returns>Описание опции или null</returns>
  public static OptionHolder CacheFindOption(Guid option)
  {
    lock (PdmConfiguratorCache.syncRoot)
      return PdmConfiguratorCache.OptionsCache.ContainsKey(option) ? PdmConfiguratorCache.OptionsCache[option] : (OptionHolder) null;
  }

  /// <summary>
  /// Отыскать в кэше идентификатор версии объекта опции с указанным Guid
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <returns>Идентификатор версии объекта опции или Intermech.Consts.UnknownObjectId, если опция не найдена в кэше</returns>
  public static long CacheFindOptionID(Guid option)
  {
    lock (PdmConfiguratorCache.syncRoot)
      return PdmConfiguratorCache.OptionsCache.ContainsKey(option) ? PdmConfiguratorCache.OptionsCache[option].OptionObjectID : 0L;
  }

  /// <summary>
  /// Отыскать в кэше Guid опции с указанным идентификатором версии объекта
  /// </summary>
  /// <param name="option">Идентификатор версии объекта опции</param>
  /// <returns>Guid опции или Guid.Empty, если опция не найдена в кэше</returns>
  public static Guid CacheFindOptionGuid(long option)
  {
    lock (PdmConfiguratorCache.syncRoot)
      return PdmConfiguratorCache.IDToGuids.ContainsKey(option) ? PdmConfiguratorCache.IDToGuids[option] : Guid.Empty;
  }

  /// <summary>
  /// Отыскать в кэше ID значения указанной опции, если известен порядковый номер значения в списке
  /// </summary>
  /// <param name="option">Идентификатор версии объекта опции</param>
  /// <param name="value">Порядковый номер значения в списке значений опции</param>
  /// <returns>ID значения опции или String.Empty, если информация не найдена в кэше, либо задан неверный порядковый номер</returns>
  public static string CacheFindOptionValueGuid(long option, int value)
  {
    OptionHolder option1 = PdmConfiguratorCache.CacheFindOption(option);
    lock (PdmConfiguratorCache.syncRoot)
      return option1 == null || option1.OptionValues.Count <= value ? string.Empty : option1.OptionValues[value].ID;
  }

  /// <summary>
  /// Отыскать в кэше порядковый номер значения указанной опции
  /// </summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="value">ID значения опции</param>
  /// <returns>Порядковый номер значения опции или -1, если информация не найдена в кэше</returns>
  public static int CacheFindOptionValueID(Guid optionGuid, string value)
  {
    OptionHolder option = PdmConfiguratorCache.CacheFindOption(optionGuid);
    lock (PdmConfiguratorCache.syncRoot)
    {
      OptionValue optionValue = option?.OptionValues.FindValue(value);
      return option != null ? option.OptionValues.IndexOf(optionValue) : -1;
    }
  }

  /// <summary>
  /// Найти опцию в кэше. Если в кэше опции нет - попытаться загрузить её из базы данных
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="optionID">Идентификатор версии объекта типа "Опции"</param>
  /// <returns>Опция или null</returns>
  public static OptionHolder CacheFindOrLoadOption(IUserSession session, long optionID)
  {
    return PdmConfiguratorCache.CacheFindOption(optionID) ?? PdmConfiguratorCache.CacheAddOption(session, optionID);
  }

  /// <summary>
  /// Найти опцию в кэше. Если в кэше опции нет - попытаться загрузить её из базы данных
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="option">Guid версии объекта типа "Опции"</param>
  /// <returns>Опция или null</returns>
  public static OptionHolder CacheFindOrLoadOption(IUserSession session, Guid option)
  {
    return PdmConfiguratorCache.CacheFindOption(option) ?? PdmConfiguratorCache.CacheAddOption(session, option);
  }

  /// <summary>Добавить опцию и её значения в кэш</summary>
  /// <param name="option">Описание опции</param>
  public static void CacheAddOption(OptionHolder option)
  {
    if (option == null)
      return;
    PdmConfiguratorCache.CacheRemoveOption(option.OptionGuid);
    lock (PdmConfiguratorCache.syncRoot)
    {
      PdmConfiguratorCache.OptionsCache[option.OptionGuid] = option;
      PdmConfiguratorCache.OptionsCacheID[option.OptionObjectID] = option;
      PdmConfiguratorCache.IDToGuids[option.OptionObjectID] = option.OptionGuid;
    }
  }

  /// <summary>Добавить опцию и её значения в кэш</summary>
  /// <param name="session">Сессия</param>
  /// <param name="optionID">Идентификатор версии объекта типа "Опции"</param>
  /// <returns>Опция</returns>
  public static OptionHolder CacheAddOption(IUserSession session, long optionID)
  {
    if (session == null)
      return (OptionHolder) null;
    return session.GetObject(optionID, false) is IDBConfiguratorOption option ? PdmConfiguratorCache.CacheAddOption(option) : (OptionHolder) null;
  }

  /// <summary>Добавить опцию и её значения в кэш</summary>
  /// <param name="session">Сессия</param>
  /// <param name="optionGuid">Guid версии объекта типа "Опции"</param>
  /// <returns>Опция</returns>
  public static OptionHolder CacheAddOption(IUserSession session, Guid optionGuid)
  {
    if (session == null)
      return (OptionHolder) null;
    return session.GetObject(optionGuid, false) is IDBConfiguratorOption option ? PdmConfiguratorCache.CacheAddOption(option) : (OptionHolder) null;
  }

  /// <summary>Добавить опцию и её значения в кэш</summary>
  /// <param name="option">Обработчик объекта типа "Опции"</param>
  /// <returns>Опция</returns>
  public static OptionHolder CacheAddOption(IDBConfiguratorOption option)
  {
    if (option == null)
      return (OptionHolder) null;
    OptionHolder option1 = new OptionHolder((object) option);
    PdmConfiguratorCache.CacheAddOption(option1);
    return option1;
  }

  /// <summary>Удалить из кэша информацию об указанной опции</summary>
  /// <param name="option">Guid опции</param>
  public static void CacheRemoveOption(Guid option)
  {
    lock (PdmConfiguratorCache.syncRoot)
    {
      OptionHolder optionHolder = PdmConfiguratorCache.OptionsCache.ContainsKey(option) ? PdmConfiguratorCache.OptionsCache[option] : (OptionHolder) null;
      if (optionHolder == null)
        return;
      PdmConfiguratorCache.OptionsCache.Remove(option);
      PdmConfiguratorCache.OptionsCacheID.Remove(optionHolder.OptionObjectID);
      PdmConfiguratorCache.IDToGuids.Remove(optionHolder.OptionObjectID);
    }
  }

  /// <summary>Получить значение опции в виде Int64-числа</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде Int64-числа или исключение,
  /// если тип данных значения опции не совместим с Int64 или не найдено значение опции</returns>
  public static long CacheGetAsInt64(Guid optionGuid, string valueID)
  {
    return (PdmConfiguratorCache.CacheFindOption(optionGuid) ?? throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_4"), (object) optionGuid))).GetAsInt64(valueID);
  }

  /// <summary>Установить значение опции в виде Int64-числа</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде строки, которая будет преобразована в значение типа Int64</param>
  /// <returns>true - значение успешно преобразовано, false - ошибочная строка, исключение,
  /// если тип данных значения опции не совместим с Int64 или не найдено значение опции</returns>
  public static bool CacheSetAsInt64(Guid optionGuid, string valueID, string value)
  {
    return (PdmConfiguratorCache.CacheFindOption(optionGuid) ?? throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_4"), (object) optionGuid))).SetAsInt64(valueID, value);
  }

  /// <summary>Получить значение опции в виде Double-числа</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде Double-числа или исключение,
  /// если тип данных значения опции не совместим с Double или не найдено значение опции</returns>
  public static double CacheGetAsDouble(Guid optionGuid, string valueID)
  {
    return (PdmConfiguratorCache.CacheFindOption(optionGuid) ?? throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_4"), (object) optionGuid))).GetAsDouble(valueID);
  }

  /// <summary>Установить значение опции в виде Double-числа</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде строки, которая будет преобразована в значение типа Double</param>
  /// <returns>true - значение успешно преобразовано, false - ошибочная строка, исключение,
  /// если тип данных значения опции не совместим с Double или не найдено значение опции</returns>
  public static bool CacheSetAsDouble(Guid optionGuid, string valueID, string value)
  {
    return (PdmConfiguratorCache.CacheFindOption(optionGuid) ?? throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_4"), (object) optionGuid))).SetAsDouble(valueID, value);
  }

  /// <summary>Получить значение опции в виде DateTime</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде DateTime или исключение,
  /// если тип данных значения опции не совместим с DateTime или не найдено значение опции</returns>
  public static DateTime CacheGetAsDateTime(Guid optionGuid, string valueID)
  {
    return (PdmConfiguratorCache.CacheFindOption(optionGuid) ?? throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_4"), (object) optionGuid))).GetAsDateTime(valueID);
  }

  /// <summary>Установить значение опции в виде DateTime-числа</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде строки, которая будет преобразована в значение типа DateTime</param>
  /// <returns>true - значение успешно преобразовано, false - ошибочная строка, исключение,
  /// если тип данных значения опции не совместим с DateTime или не найдено значение опции</returns>
  public static bool CacheSetAsDateTime(Guid optionGuid, string valueID, string value)
  {
    return (PdmConfiguratorCache.CacheFindOption(optionGuid) ?? throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_4"), (object) optionGuid))).SetAsDateTime(valueID, value);
  }

  /// <summary>Получить значение опции в виде Boolean</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде Boolean или исключение,
  /// если тип данных значения опции не совместим с DateTime или не найдено значение опции</returns>
  public static bool CacheGetAsBoolean(Guid optionGuid, string valueID)
  {
    return (PdmConfiguratorCache.CacheFindOption(optionGuid) ?? throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_4"), (object) optionGuid))).GetAsBoolean(valueID);
  }

  /// <summary>Установить значение опции в виде Boolean</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде Boolean</param>
  /// <returns>true - значение успешно установлено, false - значение не найдено, исключение,
  /// если тип данных значения опции не совместим с DateTime или не найдено значение опции</returns>
  public static bool CacheSetAsBoolean(Guid optionGuid, string valueID, bool value)
  {
    return (PdmConfiguratorCache.CacheFindOption(optionGuid) ?? throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_4"), (object) optionGuid))).SetAsBoolean(valueID, value);
  }

  /// <summary>Получить значение опции в виде String</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде String или исключение,
  /// если не найдено значение опции</returns>
  public static string CacheGetAsString(Guid optionGuid, string valueID)
  {
    return (PdmConfiguratorCache.CacheFindOption(optionGuid) ?? throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_4"), (object) optionGuid))).GetAsString(valueID);
  }

  /// <summary>Задать значение опции в виде String</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <param name="value">Значение опции в виде String</param>
  /// <returns>true - значение успешно установлено, false - ошибка, исключение,
  /// если не найдено значение опции</returns>
  public static bool CacheSetAsString(Guid optionGuid, string valueID, string value)
  {
    return (PdmConfiguratorCache.CacheFindOption(optionGuid) ?? throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_4"), (object) optionGuid))).SetAsString(valueID, value);
  }

  /// <summary>Получить значение опции в виде строки для сравнения</summary>
  /// <param name="optionGuid">Guid опции</param>
  /// <param name="valueID">ID значения опции</param>
  /// <returns>Значение опции в виде строки для сравнения или исключение, если не найдено значение опции</returns>
  public static string CacheGetAsCompareString(Guid optionGuid, string valueID)
  {
    OptionHolder option = PdmConfiguratorCache.CacheFindOption(optionGuid);
    if (option == null)
      throw new PdmConfiguratorTypeCastExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_5"), (object) valueID));
    switch (option.OptionDataType)
    {
      case FieldTypes.ftInteger:
        return PdmConfiguratorCache.CacheGetAsInt64(optionGuid, valueID).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      case FieldTypes.ftDouble:
        return PdmConfiguratorCache.CacheGetAsDouble(optionGuid, valueID).ToString("G", (IFormatProvider) CultureInfo.InvariantCulture);
      case FieldTypes.ftDateTime:
        return PdmConfiguratorCache.CacheGetAsDateTime(optionGuid, valueID).ToString("G", (IFormatProvider) CultureInfo.InvariantCulture);
      case FieldTypes.ftBoolean:
        return PdmConfiguratorCache.CacheGetAsBoolean(optionGuid, valueID).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      default:
        return PdmConfiguratorCache.CacheGetAsString(optionGuid, valueID);
    }
  }

  /// <summary>Сравнить два значения опций</summary>
  /// <param name="option1Guid">Guid первой опции</param>
  /// <param name="value1ID">ID первого значения</param>
  /// <param name="option2Guid">Guid второй опции</param>
  /// <param name="value2ID">ID второго значения</param>
  /// <returns>-1, 0, 1, либо исключение, если какое-то из значений не совместимо
  /// по типу данных с другим значением, либо не найдено</returns>
  public static int CacheCompareValues(
    Guid option1Guid,
    string value1ID,
    Guid option2Guid,
    string value2ID)
  {
    OptionHolder option1 = PdmConfiguratorCache.CacheFindOption(option1Guid);
    OptionHolder option2 = PdmConfiguratorCache.CacheFindOption(option2Guid);
    if (option1 == null || option2 == null)
      throw new PdmConfiguratorTypeCastExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_6"));
    switch (option1.OptionDataType)
    {
      case FieldTypes.ftInteger:
        if (option2.OptionDataType != FieldTypes.ftDouble && option2.OptionDataType != FieldTypes.ftInteger)
          throw new PdmConfiguratorTypeCastExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_10"));
        break;
      case FieldTypes.ftDouble:
        if (option2.OptionDataType != FieldTypes.ftDouble && option2.OptionDataType != FieldTypes.ftInteger)
          throw new PdmConfiguratorTypeCastExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_9"));
        break;
      case FieldTypes.ftDateTime:
        if (option2.OptionDataType != FieldTypes.ftDateTime)
          throw new PdmConfiguratorTypeCastExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_8"));
        break;
      case FieldTypes.ftBoolean:
        if (option2.OptionDataType != FieldTypes.ftBoolean)
          throw new PdmConfiguratorTypeCastExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_7"));
        break;
      default:
        if (option2.OptionDataType != FieldTypes.ftString)
          throw new PdmConfiguratorTypeCastExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_11"));
        break;
    }
    switch (option1.OptionDataType)
    {
      case FieldTypes.ftInteger:
      case FieldTypes.ftDouble:
        return PdmConfiguratorCache.CacheGetAsDouble(option1Guid, value1ID).CompareTo(PdmConfiguratorCache.CacheGetAsDouble(option2Guid, value2ID));
      case FieldTypes.ftDateTime:
        return PdmConfiguratorCache.CacheGetAsDateTime(option1Guid, value1ID).CompareTo(PdmConfiguratorCache.CacheGetAsDateTime(option2Guid, value2ID));
      case FieldTypes.ftBoolean:
        return PdmConfiguratorCache.CacheGetAsBoolean(option1Guid, value1ID).CompareTo(PdmConfiguratorCache.CacheGetAsBoolean(option2Guid, value2ID));
      default:
        return StringComparer.CurrentCultureIgnoreCase.Compare(PdmConfiguratorCache.CacheGetAsString(option1Guid, value1ID), PdmConfiguratorCache.CacheGetAsString(option2Guid, value2ID));
    }
  }
}
