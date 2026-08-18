// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.Helper
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Вспомогательный статический класс</summary>
public static class Helper
{
  /// <summary>Разделитель между элементами кодированной строки</summary>
  public static string[] Splitter = new string[1]{ "|" };
  /// <summary>Разделитель между элементами кодированной строки</summary>
  public static char SplitterChar = '|';
  /// <summary>Символ '*'</summary>
  public static char AsteriskChar = '*';
  /// <summary>Символ '*'</summary>
  public static string AsteriskString = "*";
  /// <summary>Максимальная длина кода значения опции (20)</summary>
  public const int MaxLenOptionValueCode = 20;
  /// <summary>Максимальная длина значения опции (200)</summary>
  public const int MaxLenOptionValue = 200;
  /// <summary>Максимальная длина описания значения опции (1000)</summary>
  public const int MaxLenOptionValueDescr = 1000;
  /// <summary>Допустимые типы данных значений опции</summary>
  public static List<FieldTypes> ValidDataTypes = new List<FieldTypes>((IEnumerable<FieldTypes>) new FieldTypes[5]
  {
    FieldTypes.ftBoolean,
    FieldTypes.ftDateTime,
    FieldTypes.ftDouble,
    FieldTypes.ftInteger,
    FieldTypes.ftString
  });

  /// <summary>
  /// Преобразовать значение типа Bool в тип PdmConfiguratorResult
  /// </summary>
  /// <param name="value">Исходное значение</param>
  /// <returns>Преобразованное значение</returns>
  public static PdmConfiguratorResult Bool2PdmConfiguratorResult(bool value)
  {
    return !value ? PdmConfiguratorResult.False : PdmConfiguratorResult.True;
  }

  /// <summary>
  /// Преобразовать значение типа PdmConfiguratorResult в тип Bool
  /// </summary>
  /// <param name="value">Исходное значение</param>
  /// <returns>Преобразованное значение</returns>
  public static bool PdmConfiguratorResult2Bool(PdmConfiguratorResult value)
  {
    return value == PdmConfiguratorResult.True;
  }

  /// <summary>
  /// Выполнить логическое объединение двух значений с помощью указанной логической функции
  /// </summary>
  /// <param name="value1">Первое значение</param>
  /// <param name="value2">Второе значение</param>
  /// <param name="func">Логическая функция</param>
  /// <returns>Результат объединения двух значений</returns>
  public static PdmConfiguratorResult Combine(
    PdmConfiguratorResult value1,
    PdmConfiguratorResult value2,
    LogicalFunction func)
  {
    if (value1 > PdmConfiguratorResult.True || value1 == PdmConfiguratorResult.Unknown)
      return value1;
    if (value2 > PdmConfiguratorResult.True || value2 == PdmConfiguratorResult.Unknown)
      return value2;
    bool flag1 = Helper.PdmConfiguratorResult2Bool(value1);
    bool flag2 = Helper.PdmConfiguratorResult2Bool(value2);
    bool flag3 = true;
    switch (func)
    {
      case LogicalFunction.And:
        flag3 = flag1 & flag2;
        break;
      case LogicalFunction.Or:
        flag3 = flag1 | flag2;
        break;
    }
    return Helper.Bool2PdmConfiguratorResult(flag3);
  }

  /// <summary>Преобразовать название типа в строку</summary>
  /// <param name="value">Тип данных</param>
  /// <returns>Название типа данных</returns>
  public static string FieldTypesToString(FieldTypes value)
  {
    switch (value)
    {
      case FieldTypes.ftInteger:
        return LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_39");
      case FieldTypes.ftDouble:
        return LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_40");
      case FieldTypes.ftDateTime:
        return LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_41");
      case FieldTypes.ftBoolean:
        return LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_42");
      default:
        return LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_43");
    }
  }

  /// <summary>Преобразровать логическое значение в строку</summary>
  /// <param name="value">Логическое значение</param>
  /// <returns>Строка</returns>
  public static string Bool2String(bool value)
  {
    return !value ? LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_45") : LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_44");
  }

  /// <summary>Получить описание указанного типа данных</summary>
  /// <param name="type">Тип данных</param>
  /// <returns>Описание указанного типа данных</returns>
  public static MyElement GetTypeElement(FieldTypes type)
  {
    return new MyElement((object) type, Helper.FieldTypesToString(type), (object) type);
  }

  /// <summary>Получить список поддерживаемых типов данных</summary>
  /// <returns></returns>
  public static List<MyElement> GetSupportedTypes()
  {
    return new List<MyElement>(5)
    {
      Helper.GetTypeElement(FieldTypes.ftInteger),
      Helper.GetTypeElement(FieldTypes.ftDouble),
      Helper.GetTypeElement(FieldTypes.ftDateTime),
      Helper.GetTypeElement(FieldTypes.ftBoolean),
      Helper.GetTypeElement(FieldTypes.ftString)
    };
  }

  /// <summary>Сравнить два объекта</summary>
  /// <param name="obj1">Первый объект</param>
  /// <param name="obj2">Второй объект</param>
  /// <returns>true - объекты равны</returns>
  public static bool CompareObjects(object obj1, object obj2)
  {
    if (obj1 == obj2)
      return true;
    if (obj1 == null && obj2 != null || obj1 != null && obj2 == null || !Helper.CompareLists(obj1 as IList, obj2 as IList) || !Helper.CompareDictionaries(obj1 as IDictionary, obj2 as IDictionary))
      return false;
    switch (obj1)
    {
      case IList _:
      case IDictionary _:
        return true;
      default:
        return object.Equals(obj1, obj2);
    }
  }

  /// <summary>Сравнить поэлементно два списка</summary>
  /// <param name="list1">Первый список</param>
  /// <param name="list2">Второй список</param>
  /// <returns>true - списки идентичны</returns>
  public static bool CompareLists(IList list1, IList list2)
  {
    if (list1 == list2)
      return true;
    if (list1 == null || list2 == null || list1.Count != list2.Count)
      return false;
    if (list1.Count == list2.Count && list1.Count == 0)
      return true;
    for (int index = 0; index < list1.Count; ++index)
    {
      if (!Helper.CompareObjects(list1[index], list2[index]))
        return false;
    }
    return true;
  }

  /// <summary>Сравнить поэлементно два словарика</summary>
  /// <param name="dict1">Первый словарик</param>
  /// <param name="dict2">Второй словарик</param>
  /// <returns>true - словарики идентичны</returns>
  public static bool CompareDictionaries(IDictionary dict1, IDictionary dict2)
  {
    if (dict1 == dict2)
      return true;
    if (dict1 == null || dict2 == null || dict1.Count != dict2.Count)
      return false;
    if (dict1.Count == dict2.Count && dict1.Count == 0)
      return true;
    foreach (DictionaryEntry dictionaryEntry in dict1)
    {
      object key = dict2.Contains(dictionaryEntry.Key) ? dictionaryEntry.Key : (object) null;
      if (dictionaryEntry.Key != null && key == null)
        return false;
      object obj = dict2[key];
      if (!Helper.CompareObjects(dictionaryEntry.Value, obj))
        return false;
    }
    return true;
  }

  /// <summary>Отключить все возможные фильтрации состава плагинами</summary>
  /// <param name="paramsSet">Параметры запроса в базу данных</param>
  public static void BlockPluginFiltrations(ref DBRecordSetParams paramsSet)
  {
    paramsSet.Tags = paramsSet.Tags != null ? paramsSet.Tags : new HybridDictionary(3, true);
    paramsSet.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] = (object) true;
    paramsSet.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) true;
    paramsSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) true;
  }

  /// <summary>Создать ключ контекста конфигуратора составов</summary>
  /// <param name="handle">Уникальный идентификатор сеанса клиента IPS с сервером приложений</param>
  /// <param name="topObjectID">Идентификатор корневого объекта конфигурируемого состава</param>
  /// <param name="topObjectType">Идентификатор типа корневого объекта конфигурируемого состава</param>
  /// <param name="userID">Идентификатор пользователя</param>
  /// <param name="relID">Идентификатор связи</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Ключ контекста конфигуратора составов</returns>
  public static RelationPair CreateKey(
    long handle,
    long topObjectID,
    int topObjectType,
    long userID,
    long relID,
    int relTypeID,
    long objID,
    int objTypeID)
  {
    RelationPair relationPair = (RelationPair) null;
    if (relID != 0L && MetaDataHelper.IsPdmPartiallyConfigurableRelationType(relTypeID))
      relationPair = new RelationPair(handle, topObjectID, topObjectType, relID, userID, objID, relTypeID, objTypeID);
    else if (objID != 0L)
      relationPair = new RelationPair(handle, topObjectID, topObjectType, 0L, userID, objID, relTypeID, objTypeID);
    return relationPair ?? new RelationPair();
  }
}
