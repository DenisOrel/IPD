// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.DataHelper
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Спец. класс для облегчения работы с локальными объектами как в составе,
/// так и в простом списке
/// </summary>
public static class DataHelper
{
  /// <summary>Перечень общих столбцов</summary>
  /// <remarks>Для совместимости</remarks>
  private static readonly IList<ColumnDescriptor> DefaultColumns = DataHelper.GetDefaultColumns();

  /// <summary>Получение общего списка атрибутов</summary>
  /// <returns></returns>
  private static IList<ColumnDescriptor> GetDefaultColumns()
  {
    return (IList<ColumnDescriptor>) new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -23, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -22, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    };
  }

  /// <summary>
  /// Генерация ColumnDescriptor[] на основе тек параметров / условий на столбцы
  /// </summary>
  /// <param name="dbrsp">Параметры запроса</param>
  public static ColumnDescriptor[] GetColumnDescriptors(DBRecordSetParams dbrsp)
  {
    if (dbrsp.Columns == null)
      return (ColumnDescriptor[]) null;
    List<DataHelper.ColumnsDKeeper> columnsDkeeperList = new List<DataHelper.ColumnsDKeeper>(dbrsp.Columns.Length);
    Dictionary<int, DataHelper.ColumnsDKeeper> dictionary = new Dictionary<int, DataHelper.ColumnsDKeeper>(dbrsp.Columns.Length);
    DataHelper.ColumnsDKeeper columnsDkeeper;
    foreach (object column in dbrsp.Columns)
    {
      int key = column is int || column is long ? (int) column : (!GuidHelper.IsGuid(column.ToString()) ? MetaDataHelper.GetAttributeByTypeNameID(column.ToString()) : MetaDataHelper.GetAttributeTypeID(new Guid(column.ToString())));
      columnsDkeeper = new DataHelper.ColumnsDKeeper(new ColumnDescriptor(column));
      columnsDkeeperList.Add(columnsDkeeper);
      if (key != 0 && key != -10000 && !dictionary.ContainsKey(key))
        dictionary.Add(key, columnsDkeeper);
    }
    if (dbrsp.SortColumns != null)
    {
      for (int index = 0; index < dbrsp.SortColumns.Length; ++index)
      {
        object sortColumn = dbrsp.SortColumns[index];
        int key = sortColumn is int || sortColumn is long ? (int) sortColumn : (!GuidHelper.IsGuid(sortColumn.ToString()) ? MetaDataHelper.GetAttributeByTypeNameID(sortColumn.ToString()) : MetaDataHelper.GetAttributeTypeID((Guid) sortColumn));
        if (dictionary.TryGetValue(key, out columnsDkeeper))
        {
          if (dbrsp.SortSources != null)
            columnsDkeeper.Data.AttributeSource = dbrsp.SortSources[index];
          if (dbrsp.SortContents != null)
            columnsDkeeper.Data.Contents = dbrsp.SortContents[index];
          columnsDkeeper.Data.OrderByID = index;
          columnsDkeeper.Data.Sort = dbrsp.Orders[index];
        }
      }
    }
    if (dbrsp.ColumnsInfo != null)
    {
      for (int index = 0; index < dbrsp.ColumnsInfo.Length; ++index)
        columnsDkeeperList[index].Data.AttributeSource = dbrsp.ColumnsInfo[index].AttributeSource;
    }
    if (dbrsp.Contents != null)
    {
      for (int index = 0; index < dbrsp.Contents.Length; ++index)
        columnsDkeeperList[index].Data.Contents = dbrsp.Contents[index];
    }
    if (dbrsp.ColumnNames != null)
    {
      for (int index = 0; index < dbrsp.ColumnNames.Length; ++index)
        columnsDkeeperList[index].Data.ColumnName = dbrsp.ColumnNames[index];
    }
    ColumnDescriptor[] columnDescriptors = new ColumnDescriptor[columnsDkeeperList.Count];
    for (int index = 0; index < columnsDkeeperList.Count; ++index)
      columnDescriptors[index] = columnsDkeeperList[index].Data;
    return columnDescriptors;
  }

  /// <summary>
  /// Объединение списков описаний столбцов с проверкой на уникальность
  /// </summary>
  /// <param name="partOne">Первая часть списка для объединения</param>
  /// <param name="partTwo">Вторая часть списка для объединения</param>
  /// <param name="defSourceType">Принадлежность атрибута по умолчанию</param>
  /// <returns></returns>
  public static List<ColumnDescriptor> CombineColumnsDescrs(
    ColumnDescriptor[] partOne,
    ColumnDescriptor[] partTwo,
    AttributeSourceTypes defSourceType)
  {
    int length1 = partOne != null ? partOne.Length : 0;
    int length2 = partTwo != null ? partTwo.Length : 0;
    List<ColumnDescriptor> columnDescriptorList1 = new List<ColumnDescriptor>(length1 + length2);
    if (length1 != 0)
      columnDescriptorList1.AddRange((IEnumerable<ColumnDescriptor>) partOne);
    if (length2 != 0)
    {
      Dictionary<int, List<ColumnDescriptor>> dictionary = new Dictionary<int, List<ColumnDescriptor>>();
      List<ColumnDescriptor> columnDescriptorList2;
      foreach (ColumnDescriptor columnDescriptor in columnDescriptorList1)
      {
        int attributeId = DataHelper.GetAttributeID(columnDescriptor.AttributeID, false);
        if (!dictionary.TryGetValue(attributeId, out columnDescriptorList2))
        {
          columnDescriptorList2 = new List<ColumnDescriptor>();
          dictionary.Add(attributeId, columnDescriptorList2);
        }
        columnDescriptorList2.Add(columnDescriptor);
      }
      ColumnNameMapping[] array = new ColumnNameMapping[4]
      {
        ColumnNameMapping.FieldName,
        ColumnNameMapping.Guid,
        ColumnNameMapping.ID,
        ColumnNameMapping.Name
      };
      foreach (ColumnDescriptor columnDescriptor1 in partTwo)
      {
        bool flag = false;
        int attributeId = DataHelper.GetAttributeID(columnDescriptor1.AttributeID, false);
        if (!dictionary.TryGetValue(attributeId, out columnDescriptorList2))
        {
          columnDescriptorList2 = new List<ColumnDescriptor>();
          dictionary.Add(attributeId, columnDescriptorList2);
        }
        else
        {
          foreach (ColumnDescriptor columnDescriptor2 in columnDescriptorList2)
          {
            if (Array.IndexOf<ColumnNameMapping>(array, columnDescriptor1.ColumnName) != -1 && Array.IndexOf<ColumnNameMapping>(array, columnDescriptor2.ColumnName) != -1)
            {
              if (columnDescriptor1.AttributeSource == columnDescriptor2.AttributeSource)
              {
                flag = true;
                break;
              }
              if ((columnDescriptor1.AttributeSource == AttributeSourceTypes.Auto || columnDescriptor2.AttributeSource == AttributeSourceTypes.Auto) && (attributeId < 0 || columnDescriptor1.AttributeSource == defSourceType || columnDescriptor2.AttributeSource == defSourceType))
              {
                flag = true;
                break;
              }
            }
          }
        }
        if (!flag)
        {
          columnDescriptorList2.Add(columnDescriptor1);
          columnDescriptorList1.Add(columnDescriptor1);
        }
      }
    }
    Dictionary<int, int> dictionary1 = new Dictionary<int, int>(columnDescriptorList1.Count);
    foreach (ColumnDescriptor columnDescriptor in columnDescriptorList1)
    {
      if (columnDescriptor.Sort != SortOrders.NONE)
      {
        int attributeId = DataHelper.GetAttributeID(columnDescriptor.AttributeID, false);
        int num;
        if (dictionary1.TryGetValue(attributeId, out num))
        {
          if (columnDescriptor.OrderByID < num)
            dictionary1[attributeId] = columnDescriptor.OrderByID;
        }
        else
          dictionary1.Add(attributeId, columnDescriptor.OrderByID);
      }
    }
    for (int index = 0; index < columnDescriptorList1.Count; ++index)
    {
      ColumnDescriptor columnDescriptor = columnDescriptorList1[index];
      if (columnDescriptor.Sort != SortOrders.NONE)
      {
        int attributeId = DataHelper.GetAttributeID(columnDescriptor.AttributeID, false);
        int num;
        if (dictionary1.TryGetValue(attributeId, out num) && columnDescriptor.OrderByID > num)
        {
          columnDescriptor.Sort = SortOrders.NONE;
          columnDescriptorList1[index] = columnDescriptor;
        }
      }
    }
    return columnDescriptorList1;
  }

  /// <summary>
  /// Возвращает числовой ид. атрибута по его имени, Guid или числовому ид. attributeID
  /// </summary>
  public static int GetAttributeID(object attribute, bool failIfNotFound)
  {
    int result = -10000;
    if (attribute == null)
      return result;
    if (attribute is ObligatoryObjectAttributes)
      return (int) attribute;
    if (attribute is int attributeId)
      return attributeId;
    if (attribute is Guid attrTypeGuid)
    {
      result = MetaDataHelper.GetAttributeTypeID(attrTypeGuid);
      if (result != -10000)
        return result;
      if (failIfNotFound)
        throw new Exception(string.Format(KernelErrorMessages.GetErrorMessage(Convert.ToInt32(84)), (object) attrTypeGuid));
    }
    if (attribute is string attrName)
    {
      result = MetaDataHelper.GetAttributeByTypeNameID(attrName);
      if (result == -10000)
      {
        string str = attrName;
        if (GuidHelper.IsGuid(str))
          result = MetaDataHelper.GetAttributeTypeID(new Guid(str));
      }
      if (result != -10000)
        return result;
      if (failIfNotFound)
        throw new Exception(string.Format(KernelErrorMessages.GetErrorMessage(Convert.ToInt32(84)), (object) attrName));
    }
    if (!int.TryParse(attribute.ToString(), out result))
      result = -10000;
    return result;
  }

  /// <summary>
  /// Разделение столбцов из списка на те что можно получить для связей/локальных типов
  /// и для столбцов самих объектов
  /// </summary>
  /// <param name="commonColumns"></param>
  /// <param name="relColumns"></param>
  /// <param name="objColumns"></param>
  public static void ParseRelColumns(
    ColumnDescriptor[] commonColumns,
    ref List<ColumnDescriptor> relColumns,
    ref List<ColumnDescriptor> objColumns)
  {
    if (relColumns == null || objColumns == null)
      return;
    relColumns.Clear();
    objColumns.Clear();
    if (commonColumns == null || commonColumns.Length == 0)
      return;
    foreach (ColumnDescriptor commonColumn in commonColumns)
    {
      if (commonColumn.AttributeSource == AttributeSourceTypes.Object)
      {
        object attributeId = commonColumn.AttributeID;
        if (attributeId != null)
        {
          int num1;
          if (attributeId is int num2)
          {
            num1 = num2;
          }
          else
          {
            num1 = DataHelper.GetAttributeID(attributeId, false);
            if (num1 == -10000 || num1 == 0)
              continue;
          }
          if (num1 > 0 || num1 == -50 || num1 == -12)
            objColumns.Add(commonColumn);
          else
            relColumns.Add(commonColumn);
        }
      }
      else
        relColumns.Add(commonColumn);
    }
  }

  /// <summary>
  /// Разделение условия из списка на те что можно отдельно применить для связей и
  /// условия на объекты (возможно + связи)
  /// </summary>
  /// <param name="commonCondItems"></param>
  /// <param name="relCondList"></param>
  /// <param name="objCondList"></param>
  public static void ParseRelConditions(
    ConditionStructure[] commonCondItems,
    ref List<ConditionStructure> relCondList,
    ref List<ConditionStructure> objCondList)
  {
    if (commonCondItems == null || relCondList == null || objCondList == null)
      return;
    relCondList.Clear();
    objCondList.Clear();
    bool flag = false;
    foreach (ConditionStructure commonCondItem in commonCondItems)
    {
      if (commonCondItem.AttributeSource == AttributeSourceTypes.Object)
      {
        int attributeId = DataHelper.GetAttributeID(commonCondItem.Attribute, false);
        if (attributeId != -10000 && attributeId != 0 && (attributeId > 0 || attributeId == -50 || attributeId == -12))
        {
          flag = true;
          break;
        }
      }
    }
    if (flag)
      objCondList.AddRange((IEnumerable<ConditionStructure>) commonCondItems);
    else
      relCondList.AddRange((IEnumerable<ConditionStructure>) commonCondItems);
  }

  /// <summary>
  /// Разделение столбцов из списка на обязательные атрибуты объектов и атрибуты
  /// самих объектов
  /// </summary>
  /// <param name="commonColumns"></param>
  /// <param name="systemColumns"></param>
  /// <param name="customColumns"></param>
  public static void ParseObjColumns(
    ColumnDescriptor[] commonColumns,
    ref List<ColumnDescriptor> systemColumns,
    ref List<ColumnDescriptor> customColumns)
  {
    if (systemColumns == null || customColumns == null)
      return;
    systemColumns.Clear();
    customColumns.Clear();
    if (commonColumns == null || commonColumns.Length == 0)
      return;
    foreach (ColumnDescriptor commonColumn in commonColumns)
    {
      switch (commonColumn.AttributeSource)
      {
        case AttributeSourceTypes.Auto:
        case AttributeSourceTypes.Object:
          object attributeId = commonColumn.AttributeID;
          if (attributeId != null)
          {
            int num1;
            if (attributeId is int num2)
            {
              num1 = num2;
            }
            else
            {
              num1 = DataHelper.GetAttributeID(attributeId, false);
              if (num1 == -10000 || num1 == 0)
                break;
            }
            if (num1 > 0 || num1 == -50 || num1 == -12)
              customColumns.Add(commonColumn);
            systemColumns.Add(commonColumn);
            break;
          }
          break;
        default:
          systemColumns.Add(commonColumn);
          break;
      }
    }
  }

  /// <summary>
  /// Разделение условия из списка на те что можно отдельно применить на системные атрибуты объектов и
  /// условия на прочие атрибуты объектов (возможно + связи)
  /// </summary>
  /// <param name="commonCondItems"></param>
  /// <param name="systemCondList"></param>
  /// <param name="customCondList"></param>
  public static void ParseObjConditions(
    ConditionStructure[] commonCondItems,
    ref List<ConditionStructure> systemCondList,
    ref List<ConditionStructure> customCondList)
  {
    if (commonCondItems == null || systemCondList == null || customCondList == null)
      return;
    systemCondList.Clear();
    customCondList.Clear();
    bool flag = false;
    foreach (ConditionStructure commonCondItem in commonCondItems)
    {
      if (commonCondItem.AttributeSource == AttributeSourceTypes.Object || commonCondItem.AttributeSource == AttributeSourceTypes.Auto)
      {
        int attributeId = DataHelper.GetAttributeID(commonCondItem.Attribute, false);
        if (attributeId != -10000 && attributeId != 0 && (attributeId > 0 || attributeId == -50 || attributeId == -12))
        {
          flag = true;
          break;
        }
      }
    }
    if (flag)
      customCondList.AddRange((IEnumerable<ConditionStructure>) commonCondItems);
    else
      systemCondList.AddRange((IEnumerable<ConditionStructure>) commonCondItems);
  }

  /// <summary>
  /// Раскрутка состава объекта вниз (Оставил в целях совместимости)
  /// </summary>
  /// <param name="projId"></param>
  /// <param name="userSession"></param>
  /// <param name="relations"></param>
  /// <returns></returns>
  public static DataTable GetChildSostavData(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations)
  {
    return DataHelper.GetChildSostavData(new ObjInfoItem(projId), userSession, relations);
  }

  /// <summary>
  /// Раскрутка состава объекта вниз (Оставил в целях совместимости)
  /// </summary>
  /// <param name="projObj"></param>
  /// <param name="userSession"></param>
  /// <param name="relations"></param>
  /// <returns></returns>
  public static DataTable GetChildSostavData(
    ObjInfoItem projObj,
    IUserSession userSession,
    IEnumerable<int> relations)
  {
    return DataHelper.GetChildSostavData(projObj, userSession, relations, true);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projId">идентификатор объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive)
  {
    return DataHelper.GetChildSostavData(new ObjInfoItem(projId), userSession, relations, recursive, (IEnumerable<ConditionStructure>) new ConditionStructure[0]);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projObj">Объект для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    ObjInfoItem projObj,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive)
  {
    return DataHelper.GetChildSostavData(projObj, userSession, relations, recursive, (IEnumerable<ConditionStructure>) new ConditionStructure[0]);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projId">идентификатор объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Дополнительные условия на выбор объектов</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions)
  {
    return DataHelper.GetChildSostavData(new ObjInfoItem(projId), userSession, relations, recursive, conditions, (IEnumerable<ColumnDescriptor>) null);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projObj">Объект для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Дополнительные условия на выбор объектов</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    ObjInfoItem projObj,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions)
  {
    return DataHelper.GetChildSostavData(projObj, userSession, relations, recursive, conditions, (IEnumerable<ColumnDescriptor>) null);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projId">Идентификатор объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Условия на связи</param>
  /// <param name="columns">Дополнительные столбцы для выбора</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns)
  {
    return DataHelper.GetChildSostavData(new ObjInfoItem(projId), userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projId">идентификатор объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Дополнительные условия на выбор объектов</param>
  /// <param name="columns">Дополнительные столбцы для выбора</param>
  /// <param name="tags">Фильтрация</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    HybridDictionary tags)
  {
    return DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      new ObjInfoItem(projId)
    }, userSession, relations, recursive, conditions, columns, tags);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projId">идентификатор объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Дополнительные условия на выбор объектов</param>
  /// <param name="columns">Дополнительные столбцы для выбора</param>
  /// <param name="tags">Фильтрация</param>
  /// <param name="filtrationOwnerId"></param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    HybridDictionary tags = null,
    string filtrationOwnerId = "")
  {
    List<ObjInfoItem> projObjList = DataHelper.ComposeOIIList(projId);
    DBRecordSetParams dbRecordSetParams = DataHelper.ComposeDBRsp(conditions, columns, tags);
    int num = recursive ? -1 : 1;
    if (string.IsNullOrEmpty(filtrationOwnerId))
      filtrationOwnerId = DataHelper.Consts.cnt_def_filtrationRule;
    IUserSession userSession1 = userSession;
    IEnumerable<int> relations1 = relations;
    int recursiveLevel = num;
    DBRecordSetParams dbRsp = dbRecordSetParams;
    string filtrationOwnerId1 = filtrationOwnerId;
    return DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList, userSession1, relations1, recursiveLevel, dbRsp, (VersionsRule) null, filtrationOwnerId1, (Dictionary<long, HybridDictionary>) null);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projId">идентификатор объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="dbrsp">Дополнительные условия на выбор объектов</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    DBRecordSetParams dbrsp)
  {
    return DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      new ObjInfoItem(projId)
    }, userSession, relations, recursive, dbrsp);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projObj">Объект для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Условия на связи</param>
  /// <param name="columns">Дополнительные столбцы для выбора</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    ObjInfoItem projObj,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns)
  {
    if ((TypedInfoItem) projObj == (TypedInfoItem) null || projObj.ObjectID == 0L || projObj.ObjectID == -1L)
      return (DataTable) null;
    return DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      projObj
    }, userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projObj">Объект для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Условия на связи</param>
  /// <param name="columns">Дополнительные столбцы для выбора</param>
  /// <param name="tags">Фильтрация</param>
  /// <param name="filtrationOwnerId"></param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    ObjInfoItem projObj,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    HybridDictionary tags,
    string filtrationOwnerId = null)
  {
    if ((TypedInfoItem) projObj == (TypedInfoItem) null || projObj.ObjectID == 0L || projObj.ObjectID == -1L)
      return (DataTable) null;
    return DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      projObj
    }, userSession, relations, recursive, conditions, columns, tags, filtrationOwnerId);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projObj">Объект для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="dbrsp">Условия</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    ObjInfoItem projObj,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    DBRecordSetParams dbrsp)
  {
    if ((TypedInfoItem) projObj == (TypedInfoItem) null || projObj.ObjectID == 0L || projObj.ObjectID == -1L)
      return (DataTable) null;
    return DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      projObj
    }, userSession, relations, recursive, dbrsp);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projObjList">Идентификаторы объектов для которых надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Условия на связи, объекты (Со скобками :) )</param>
  /// <param name="columns">Дополнительные столбцы для выбора</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    IEnumerable<ObjInfoItem> projObjList,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns)
  {
    return DataHelper.GetChildSostavData(projObjList, userSession, relations, recursive, conditions, columns, (HybridDictionary) null);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projObjList">Идентификаторы объектов для которых надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Условия на связи, объекты (Со скобками :) )</param>
  /// <param name="columns">Дополнительные столбцы для выбора</param>
  /// <param name="tags">Фильтрация</param>
  /// <param name="filtrationOwnerId"></param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    IEnumerable<ObjInfoItem> projObjList,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    HybridDictionary tags,
    string filtrationOwnerId = null)
  {
    DBRecordSetParams dbRsp = DataHelper.ComposeDBRsp(conditions, columns, tags);
    return DataHelper.GetChildSostavData(projObjList, userSession, relations, recursive, dbRsp, filtrationOwnerId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="conditions"></param>
  /// <param name="columns"></param>
  /// <param name="tags"></param>
  /// <returns></returns>
  public static DBRecordSetParams ComposeDBRsp(
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    HybridDictionary tags)
  {
    return new DBRecordSetParams(conditions != null ? conditions.ToArray<ConditionStructure>() : (ConditionStructure[]) null, columns != null ? columns.ToArray<ColumnDescriptor>() : (ColumnDescriptor[]) null)
    {
      Tags = tags
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objId"></param>
  /// <returns></returns>
  public static List<ObjInfoItem> ComposeOIIList(long objId)
  {
    return new List<ObjInfoItem>()
    {
      new ObjInfoItem(objId)
    };
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projObjList">Идентификаторы объектов для которых надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="dbRsp">Условия на связи, объекты (Со скобками :) )</param>
  /// <param name="filtrationOwnerId"></param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetChildSostavData(
    IEnumerable<ObjInfoItem> projObjList,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    DBRecordSetParams dbRsp,
    string filtrationOwnerId = null)
  {
    int recursiveLevel = recursive ? -1 : 1;
    return DataHelper.GetChildSostavData(projObjList, userSession, relations, recursiveLevel, dbRsp, (VersionsRule) null, string.IsNullOrEmpty(filtrationOwnerId) ? DataHelper.Consts.cnt_def_filtrationRule : filtrationOwnerId, (Dictionary<long, HybridDictionary>) null);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projObjList">Идентификаторы объектов для которых надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursiveLevel">Количество уровней разворота состава ( для получения рекурсивного состава -1)</param>
  /// <param name="dbRsp">Условия на связи, объекты (Со скобками :) )</param>
  /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
  /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.</param>
  /// <param name="tags">Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
  /// Например, для включения режима актуализации состава, для работы в определённом контексте состава, т.п.</param>
  /// <param name="enabledTypes">Типы искомых объектов</param>
  /// <param name="typesToExpand">Если не null, указывает, состав объектов каких типов нужно разворачивать.
  /// Данное условие применяется только к объектам состава и не распространяется на объекты objects</param>
  /// <returns></returns>
  public static DataTable GetChildSostavData(
    IEnumerable<ObjInfoItem> projObjList,
    IUserSession userSession,
    IEnumerable<int> relations,
    int recursiveLevel,
    DBRecordSetParams dbRsp,
    VersionsRule rule,
    string filtrationOwnerId,
    Dictionary<long, HybridDictionary> tags,
    IEnumerable<int> enabledTypes = null,
    IEnumerable<int> typesToExpand = null)
  {
    if (projObjList == null)
      return (DataTable) null;
    if (!(projObjList is ObjInfoItem[] objInfoItemArray1))
      objInfoItemArray1 = projObjList.ToArray<ObjInfoItem>();
    ObjInfoItem[] objInfoItemArray2 = objInfoItemArray1;
    if (!((IEnumerable<ObjInfoItem>) objInfoItemArray2).Any<ObjInfoItem>())
      return (DataTable) null;
    if (userSession == null || relations == null)
      return (DataTable) null;
    if (!(relations is int[] numArray))
      numArray = relations.ToArray<int>();
    int[] source = numArray;
    if (!((IEnumerable<int>) source).Any<int>())
      return (DataTable) null;
    if (!(userSession.GetCustomService(typeof (ICompositionLoadService)) is ICompositionLoadService customService))
      return (DataTable) null;
    List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>((IEnumerable<ObjInfoItem>) objInfoItemArray2);
    DataHelper.UpdateUnknownTypes(objInfoItemList, userSession);
    if (!objInfoItemList.Any<ObjInfoItem>())
      return (DataTable) null;
    ConditionStructure[] conditions = dbRsp.Conditions;
    ColumnDescriptor[] columnDescriptors = DataHelper.GetColumnDescriptors(dbRsp);
    List<ColumnDescriptor> columns = new List<ColumnDescriptor>();
    columns.AddRange((IEnumerable<ColumnDescriptor>) DataHelper.DefaultColumns);
    if (columnDescriptors != null && columnDescriptors.Length != 0)
      columns = DataHelper.CombineColumnsDescrs(columnDescriptors, columns.ToArray(), AttributeSourceTypes.Relation);
    if (source.Length == 1 && ((IEnumerable<int>) source).First<int>() == -1)
    {
      List<IMSRelationType> relationTypesList = MetaDataHelper.GetRelationTypesList();
      List<int> intList = new List<int>(relationTypesList.Count);
      foreach (IMSRelationType imsRelationType in relationTypesList)
        intList.Add(imsRelationType.RelationTypeID);
      source = intList.ToArray();
    }
    Guid guid = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(guid);
    List<int> collection1 = new List<int>(source.Length);
    List<int> collection2 = new List<int>(source.Length);
    foreach (int num in source)
    {
      IMSRelationType relationType = MetaDataHelper.GetRelationType(num);
      if (relationType != null)
      {
        if (!relationType.AnyAttributes && MetaDataHelper.GetAttribute4RelationType(num, attributeTypeId) == null)
          collection2.Add(num);
        else
          collection1.Add(num);
      }
    }
    List<int> searchRelationTypes = new List<int>(source.Length);
    searchRelationTypes.AddRange((IEnumerable<int>) collection1);
    searchRelationTypes.AddRange((IEnumerable<int>) collection2);
    if (collection1.Count > 0)
    {
      int attributeId = DataHelper.GetAttributeID((object) guid, false);
      bool flag = false;
      foreach (ColumnDescriptor columnDescriptor in columns)
      {
        if ((columnDescriptor.AttributeSource == AttributeSourceTypes.Auto || columnDescriptor.AttributeSource == AttributeSourceTypes.Relation) && DataHelper.GetAttributeID(columnDescriptor.AttributeID, false) == attributeId)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        columns.Add(new ColumnDescriptor((object) guid, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, DataHelper.Consts.cnt_idx_FixedSortMin));
    }
    if (tags == null && dbRsp.Tags != null)
    {
      tags = new Dictionary<long, HybridDictionary>();
      foreach (ObjInfoItem objInfoItem in objInfoItemList)
        tags.Add(objInfoItem.ObjectID, dbRsp.Tags);
    }
    return DataHelper.SortCompositionData(customService.LoadComplexCompositions((object) userSession.SessionGUID, (IEnumerable<ObjInfoItem>) objInfoItemList, (IEnumerable<int>) searchRelationTypes, enabledTypes, (IEnumerable<ColumnDescriptor>) columns, true, false, rule, (IEnumerable<ConditionStructure>) conditions, filtrationOwnerId, tags, recursiveLevel, typesToExpand), columns.ToArray(), objInfoItemList);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partId">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    long partId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive)
  {
    return DataHelper.GetParentSostavData(new ObjInfoItem(partId), userSession, relations, recursive, (IEnumerable<ConditionStructure>) new ConditionStructure[0]);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partObj">Описание версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    ObjInfoItem partObj,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive)
  {
    return DataHelper.GetParentSostavData(partObj, userSession, relations, recursive, (IEnumerable<ConditionStructure>) new ConditionStructure[0]);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partId">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    long partId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions)
  {
    return DataHelper.GetParentSostavData(new ObjInfoItem(partId), userSession, relations, recursive, conditions, (IEnumerable<ColumnDescriptor>) null);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partObj">Объект для которого требуется получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    ObjInfoItem partObj,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions)
  {
    return DataHelper.GetParentSostavData(partObj, userSession, relations, recursive, conditions, (IEnumerable<ColumnDescriptor>) null);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partId">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    long partId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns)
  {
    if (partId == 0L)
      return (DataTable) null;
    return DataHelper.GetParentSostavData((IEnumerable<long>) new List<long>()
    {
      partId
    }, userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partId">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <param name="tags">Фильтрация</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    long partId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    HybridDictionary tags)
  {
    if (partId == 0L)
      return (DataTable) null;
    return DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      new ObjInfoItem(partId)
    }, userSession, relations, recursive, conditions, columns, tags);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partId">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="dbrsp">Условия на выбор связи</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    long partId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    DBRecordSetParams dbrsp)
  {
    if (partId == 0L)
      return (DataTable) null;
    return DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      new ObjInfoItem(partId)
    }, userSession, relations, recursive, dbrsp);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partObj">Объект для которого требуется получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    ObjInfoItem partObj,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns)
  {
    if ((TypedInfoItem) partObj == (TypedInfoItem) null || partObj.ObjectID == 0L)
      return (DataTable) null;
    return DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      partObj
    }, userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partObj">Объект для которого требуется получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <param name="tags"></param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    ObjInfoItem partObj,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    HybridDictionary tags)
  {
    if ((TypedInfoItem) partObj == (TypedInfoItem) null || partObj.ObjectID == 0L)
      return (DataTable) null;
    return DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      partObj
    }, userSession, relations, recursive, conditions, columns, tags);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partObj">Объект для которого требуется получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="dbrsp">Условия на выбор связи</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    ObjInfoItem partObj,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    DBRecordSetParams dbrsp)
  {
    if ((TypedInfoItem) partObj == (TypedInfoItem) null || partObj.ObjectID == 0L)
      return (DataTable) null;
    return DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      partObj
    }, userSession, relations, recursive, dbrsp);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partIdList">идентификатор версии объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    IEnumerable<long> partIdList,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns)
  {
    if (!(partIdList is long[] numArray))
      numArray = partIdList != null ? partIdList.ToArray<long>() : (long[]) null;
    long[] source = numArray;
    if (source == null || !((IEnumerable<long>) source).Any<long>())
      return (DataTable) null;
    List<ObjInfoItem> partObjList = new List<ObjInfoItem>(source.Length);
    foreach (long objectId in source)
    {
      switch (objectId)
      {
        case -1:
        case 0:
          continue;
        default:
          partObjList.Add(new ObjInfoItem(objectId));
          continue;
      }
    }
    return DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) partObjList, userSession, relations, recursive, conditions, columns);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partObjList">Объекты для которых требуется получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Columns Key = column name</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    IEnumerable<ObjInfoItem> partObjList,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns)
  {
    return DataHelper.GetParentSostavData(partObjList, userSession, relations, recursive, conditions, columns, (HybridDictionary) null);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partObjList">Объекты для которых требуется получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="conditions">Условия на выбор связи</param>
  /// <param name="columns">Дополнительные столбцы для выбора</param>
  /// <param name="tags"></param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    IEnumerable<ObjInfoItem> partObjList,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    HybridDictionary tags)
  {
    return DataHelper.GetParentSostavData(partObjList, userSession, relations, recursive, new DBRecordSetParams(conditions != null ? conditions.ToArray<ConditionStructure>() : (ConditionStructure[]) null, columns != null ? columns.ToArray<ColumnDescriptor>() : (ColumnDescriptor[]) null)
    {
      Tags = tags
    });
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partObjList">Объекты для которых требуется получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive"></param>
  /// <param name="dbrsp">Условия на выбор связи</param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    IEnumerable<ObjInfoItem> partObjList,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    DBRecordSetParams dbrsp)
  {
    int recursiveLevel = recursive ? -1 : 1;
    return DataHelper.GetParentSostavData(partObjList, userSession, relations, recursiveLevel, dbrsp, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule, (Dictionary<long, HybridDictionary>) null);
  }

  /// <summary>Раскрутка состава объекта вниз</summary>
  /// <param name="projId">идентификатор объекта для которого надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursive">Разворачивать рекурсивно</param>
  /// <param name="conditions">Дополнительные условия на выбор объектов</param>
  /// <param name="columns">Дополнительные столбцы для выбора</param>
  /// <param name="tags">Фильтрация</param>
  /// <param name="filtrationOwnerId"></param>
  /// <returns>DataTable as is</returns>
  public static DataTable GetParentSostavData(
    long projId,
    IUserSession userSession,
    IEnumerable<int> relations,
    bool recursive,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    HybridDictionary tags = null,
    string filtrationOwnerId = "")
  {
    List<ObjInfoItem> partObjList = DataHelper.ComposeOIIList(projId);
    DBRecordSetParams dbRecordSetParams = DataHelper.ComposeDBRsp(conditions, columns, tags);
    int num = recursive ? -1 : 1;
    if (string.IsNullOrEmpty(filtrationOwnerId))
      filtrationOwnerId = DataHelper.Consts.cnt_def_filtrationRule;
    IUserSession userSession1 = userSession;
    IEnumerable<int> relations1 = relations;
    int recursiveLevel = num;
    DBRecordSetParams dbRsp = dbRecordSetParams;
    string filtrationOwnerId1 = filtrationOwnerId;
    return DataHelper.GetParentSostavData((IEnumerable<ObjInfoItem>) partObjList, userSession1, relations1, recursiveLevel, dbRsp, (VersionsRule) null, filtrationOwnerId1, (Dictionary<long, HybridDictionary>) null);
  }

  /// <summary>Раскрутка состава объекта вверх</summary>
  /// <param name="partObjList">Идентификаторы объектов для которых надо получить состав</param>
  /// <param name="userSession">интерфейс пользовательской сессии</param>
  /// <param name="relations">массив идентификаторов типов связей по которым надо производить
  /// раскрутку состава</param>
  /// <param name="recursiveLevel">Количество уровней разворота состава ( для получения рекурсивного состава -1)</param>
  /// <param name="dbRsp">Условия на связи, объекты (Со скобками :) )</param>
  /// <param name="rule">Правило подбора версий, по которому будет фильтроваться состав</param>
  /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.</param>
  /// <param name="tags">Дополнительные параметры, которые будут добавлены к параметрам запроса в базу.
  /// Например, для включения режима актуализации состава, для работы в определённом контексте состава, т.п.</param>
  /// <param name="enabledTypes">Типы искомых объектов</param>
  /// <param name="typesToExpand">Если не null, указывает, состав объектов каких типов нужно разворачивать.
  /// Данное условие применяется только к объектам состава и не распространяется на объекты objects</param>
  /// <returns></returns>
  public static DataTable GetParentSostavData(
    IEnumerable<ObjInfoItem> partObjList,
    IUserSession userSession,
    IEnumerable<int> relations,
    int recursiveLevel,
    DBRecordSetParams dbRsp,
    VersionsRule rule,
    string filtrationOwnerId,
    Dictionary<long, HybridDictionary> tags,
    IEnumerable<int> enabledTypes = null,
    IEnumerable<int> typesToExpand = null)
  {
    if (!(partObjList is ObjInfoItem[] objInfoItemArray))
      objInfoItemArray = partObjList != null ? partObjList.ToArray<ObjInfoItem>() : (ObjInfoItem[]) null;
    ObjInfoItem[] collection1 = objInfoItemArray;
    if (collection1 == null || collection1.Length == 0)
      return (DataTable) null;
    if (!(relations is int[] numArray))
      numArray = relations != null ? relations.ToArray<int>() : (int[]) null;
    int[] source = numArray;
    if (userSession == null || source == null || !((IEnumerable<int>) source).Any<int>())
      return (DataTable) null;
    List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>((IEnumerable<ObjInfoItem>) collection1);
    DataHelper.UpdateUnknownTypes(objInfoItemList, userSession);
    if (!objInfoItemList.Any<ObjInfoItem>())
      return (DataTable) null;
    if (!(userSession.GetCustomService(typeof (ICompositionLoadService)) is ICompositionLoadService customService))
      return (DataTable) null;
    ConditionStructure[] conditions = dbRsp.Conditions;
    ColumnDescriptor[] columnDescriptors = DataHelper.GetColumnDescriptors(dbRsp);
    List<ColumnDescriptor> columns = new List<ColumnDescriptor>();
    columns.AddRange((IEnumerable<ColumnDescriptor>) DataHelper.DefaultColumns);
    if (columnDescriptors != null && columnDescriptors.Length != 0)
      columns = DataHelper.CombineColumnsDescrs(columnDescriptors, columns.ToArray(), AttributeSourceTypes.Relation);
    if (source.Length == 1 && ((IEnumerable<int>) source).First<int>() == -1)
    {
      List<IMSRelationType> relationTypesList = MetaDataHelper.GetRelationTypesList();
      List<int> intList = new List<int>(relationTypesList.Count);
      foreach (IMSRelationType imsRelationType in relationTypesList)
        intList.Add(imsRelationType.RelationTypeID);
      source = intList.ToArray();
    }
    Guid guid = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(guid);
    List<int> collection2 = new List<int>(source.Length);
    List<int> collection3 = new List<int>(source.Length);
    foreach (int num in source)
    {
      IMSRelationType relationType = MetaDataHelper.GetRelationType(num);
      if (relationType != null)
      {
        if (!relationType.AnyAttributes && MetaDataHelper.GetAttribute4RelationType(num, attributeTypeId) == null)
          collection3.Add(num);
        else
          collection2.Add(num);
      }
    }
    List<int> searchRelationTypes = new List<int>(source.Length);
    searchRelationTypes.AddRange((IEnumerable<int>) collection2);
    searchRelationTypes.AddRange((IEnumerable<int>) collection3);
    if (collection2.Count > 0)
    {
      int attributeId = DataHelper.GetAttributeID((object) guid, false);
      bool flag = false;
      foreach (ColumnDescriptor columnDescriptor in columns)
      {
        if ((columnDescriptor.AttributeSource == AttributeSourceTypes.Auto || columnDescriptor.AttributeSource == AttributeSourceTypes.Relation) && DataHelper.GetAttributeID(columnDescriptor.AttributeID, false) == attributeId)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        columns.Add(new ColumnDescriptor((object) guid, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, DataHelper.Consts.cnt_idx_FixedSortMin));
    }
    return customService.LoadComplexCompositions((object) userSession.SessionGUID, (IEnumerable<ObjInfoItem>) objInfoItemList, (IEnumerable<int>) searchRelationTypes, enabledTypes, (IEnumerable<ColumnDescriptor>) columns, false, false, rule, (IEnumerable<ConditionStructure>) conditions, filtrationOwnerId, tags, recursiveLevel, typesToExpand);
  }

  /// <summary>Получение данных по объектам</summary>
  /// <param name="objTypeId">Ид. типа объекта</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="conditions">Условия выборки</param>
  /// <param name="columns">Описание столбов</param>
  /// <returns></returns>
  public static DataTable GetObjectData(
    int objTypeId,
    IUserSession userSession,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns)
  {
    return DataHelper.GetObjectData(new int[1]{ objTypeId }, userSession, conditions, columns);
  }

  /// <summary>Получение данных по объектам</summary>
  /// <param name="objTypeIDs">Ид. типа объекта</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="conditions">Условия выборки</param>
  /// <param name="columns">Описание столбов</param>
  /// <returns></returns>
  public static DataTable GetObjectData(
    int[] objTypeIDs,
    IUserSession userSession,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns)
  {
    return DataHelper.GetObjectData((IEnumerable<int>) objTypeIDs, userSession, conditions, columns, (IEnumerable<long>) null);
  }

  /// <summary>Получение данных по объектам</summary>
  /// <param name="objTypeId">Ид. типа объекта</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="conditions">Условия выборки</param>
  /// <param name="columns">Описание столбов</param>
  /// <param name="objIdList">Перечень ид. версий объектов (опционально)</param>
  /// <returns></returns>
  public static DataTable GetObjectData(
    int objTypeId,
    IUserSession userSession,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    IEnumerable<long> objIdList)
  {
    return DataHelper.GetObjectData((IEnumerable<int>) new int[1]
    {
      objTypeId
    }, userSession, conditions, columns, objIdList);
  }

  /// <summary>Получение данных по объектам</summary>
  /// <param name="objTypeIDs">Ид. типа объекта</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="conditions">Условия выборки</param>
  /// <param name="columns">Описание столбов</param>
  /// <param name="objIdList">Перечень ид. версий объектов (опционально)</param>
  /// <returns></returns>
  public static DataTable GetObjectData(
    IEnumerable<int> objTypeIDs,
    IUserSession userSession,
    IEnumerable<ConditionStructure> conditions,
    IEnumerable<ColumnDescriptor> columns,
    IEnumerable<long> objIdList)
  {
    DBRecordSetParams dbRsp = new DBRecordSetParams(conditions != null ? conditions.ToArray<ConditionStructure>() : (ConditionStructure[]) null, columns != null ? columns.ToArray<ColumnDescriptor>() : (ColumnDescriptor[]) null);
    return DataHelper.GetObjectData(objTypeIDs, userSession, dbRsp, objIdList);
  }

  /// <summary>Получение данных по объектам</summary>
  /// <remarks>Параметры и условия меняются, часть параметров не передается в запрос
  /// копируем только следующее:
  /// 
  /// Conditions
  /// Columns
  /// FailIfNotFound
  /// LastKeyValue
  /// LastOrderValue
  /// RecordCount
  /// TableName
  /// Tags
  /// 
  /// так что осторожно - на свой страх и риск</remarks>
  /// <param name="objTypeId">Ид. типа объекта</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="dbRsp">Параметры выборки </param>
  /// <param name="objIdList">Перечень ид. версий объектов (опционально)</param>
  /// <returns></returns>
  public static DataTable GetObjectData(
    int objTypeId,
    IUserSession userSession,
    DBRecordSetParams dbRsp,
    IEnumerable<long> objIdList)
  {
    return DataHelper.GetObjectData((IEnumerable<int>) new int[1]
    {
      objTypeId
    }, userSession, dbRsp, objIdList);
  }

  /// <summary>Получение данных по объектам</summary>
  /// <remarks>Параметры и условия меняются, часть параметров не передается в запрос
  /// копируем только следующее:
  /// 
  /// Conditions
  /// Columns
  /// FailIfNotFound
  /// LastKeyValue
  /// LastOrderValue
  /// RecordCount
  /// TableName
  /// Tags
  /// 
  /// так что осторожно - на свой страх и риск</remarks>
  /// <param name="objTypeId">Ид. типа объекта, если все типы заданы в objIDList, то
  /// допускается передавать -1</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="dbRsp">Параметры выборки </param>
  /// <param name="objIdList">Перечень ид. версий объектов (опционально)</param>
  /// <returns></returns>
  public static DataTable GetObjectDataEx(
    int objTypeId,
    IUserSession userSession,
    DBRecordSetParams dbRsp,
    IEnumerable<ObjInfoItem> objIdList)
  {
    return DataHelper.GetObjectDataEx((IEnumerable<int>) new int[1]
    {
      objTypeId
    }, userSession, dbRsp, objIdList);
  }

  /// <summary>Получение данных по объектам</summary>
  /// <remarks>Параметры и условия меняются, часть параметров не передается в запрос
  /// копируем только следующее:
  /// 
  /// Conditions
  /// Columns
  /// FailIfNotFound
  /// LastKeyValue
  /// LastOrderValue
  /// RecordCount
  /// TableName
  /// Tags
  /// 
  /// так что осторожно - на свой страх и риск</remarks>
  /// <param name="objTypeIDs">Ид. типов объекта</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="dbRsp">Параметры выборки </param>
  /// <param name="objIdList">Перечень ид. версий объектов (опционально)</param>
  /// <returns></returns>
  public static DataTable GetObjectData(
    IEnumerable<int> objTypeIDs,
    IUserSession userSession,
    DBRecordSetParams dbRsp,
    IEnumerable<long> objIdList)
  {
    return DataHelper.GetObjectDataEx(objTypeIDs, userSession, dbRsp, (IEnumerable<ObjInfoItem>) ObjInfoHelper.GetObjectInfoList(objIdList));
  }

  /// <summary>Получение данных по объектам</summary>
  /// <remarks>Параметры и условия меняются, часть параметров не передается в запрос
  /// копируем только следующее:
  /// 
  /// Conditions
  /// Columns
  /// FailIfNotFound
  /// LastKeyValue
  /// LastOrderValue
  /// RecordCount
  /// TableName
  /// Tags
  /// 
  /// так что осторожно - на свой страх и риск
  /// Сортировка объектов не работает - т.к. для локальных объектов отдельные запросы
  /// </remarks>
  /// <param name="objTypeIDs">Ид. типов объекта</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="dbRsp">Параметры выборки </param>
  /// <param name="objIdList">Перечень ид. версий объектов (опционально)</param>
  /// <returns></returns>
  public static DataTable GetObjectDataEx(
    IEnumerable<int> objTypeIDs,
    IUserSession userSession,
    DBRecordSetParams dbRsp,
    IEnumerable<ObjInfoItem> objIdList)
  {
    DataTable objectDataEx = (DataTable) null;
    if (userSession == null)
      return (DataTable) null;
    if (!(objTypeIDs is int[] numArray1))
      numArray1 = objTypeIDs != null ? objTypeIDs.ToArray<int>() : (int[]) null;
    int[] numArray2 = numArray1;
    if (numArray2 == null || !((IEnumerable<int>) numArray2).Any<int>())
      return (DataTable) null;
    Dictionary<int, List<ObjInfoItem>> dictionary = new Dictionary<int, List<ObjInfoItem>>();
    if (!(objIdList is ObjInfoItem[] objInfoItemArray))
      objInfoItemArray = objIdList != null ? objIdList.ToArray<ObjInfoItem>() : (ObjInfoItem[]) null;
    ObjInfoItem[] source = objInfoItemArray;
    List<ObjInfoItem> list1;
    if (source != null && ((IEnumerable<ObjInfoItem>) source).Count<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => (TypedInfoItem) item != (TypedInfoItem) null)) > 0)
    {
      foreach (ObjInfoItem objInfoItem in source)
      {
        if (!dictionary.TryGetValue(objInfoItem.ObjTypeID, out list1))
        {
          list1 = new List<ObjInfoItem>();
          dictionary.Add(objInfoItem.ObjTypeID, list1);
        }
        list1.Add(objInfoItem);
      }
    }
    else
      dictionary.Add(-1, new List<ObjInfoItem>());
    ConditionStructure[] conditions = dbRsp.Conditions;
    int length1 = conditions != null ? conditions.Length : 0;
    List<ConditionStructure> systemCondList = new List<ConditionStructure>(length1);
    List<ConditionStructure> customCondList = new List<ConditionStructure>(length1);
    if (length1 != 0)
      DataHelper.ParseObjConditions(conditions, ref systemCondList, ref customCondList);
    ColumnDescriptor[] columnDescriptors = DataHelper.GetColumnDescriptors(dbRsp);
    int length2 = columnDescriptors != null ? columnDescriptors.Length : 0;
    List<ColumnDescriptor> systemColumns = new List<ColumnDescriptor>(length2);
    List<ColumnDescriptor> customColumns = new List<ColumnDescriptor>(length2);
    if (length2 != 0)
      DataHelper.ParseObjColumns(columnDescriptors, ref systemColumns, ref customColumns);
    foreach (KeyValuePair<int, List<ObjInfoItem>> keyValuePair in dictionary)
    {
      int key = keyValuePair.Key;
      list1 = keyValuePair.Value;
      List<int> list2 = new List<int>();
      List<int> resultData = new List<int>();
      List<int> intList1 = new List<int>();
      if (key != -1)
      {
        resultData.Add(key);
        list2.Add(key);
        intList1.Clear();
      }
      else
      {
        foreach (int parentTypeID in numArray2)
        {
          if (parentTypeID != -1)
          {
            intList1.AddRange((IEnumerable<int>) MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(parentTypeID));
            list2.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(parentTypeID));
          }
          else
          {
            foreach (IMSObjectType objectTypes in MetaDataHelper.GetObjectTypesList())
            {
              if (objectTypes.IsLocalType)
                intList1.Add(objectTypes.ObjectTypeID);
            }
          }
        }
        GenericListHelper.MakeUnique<int>(list2);
        GenericListHelper.MakeUnique<int>(intList1);
        if (customCondList.Count == 0 && customColumns.Count == 0)
          resultData = new List<int>((IEnumerable<int>) numArray2);
        else
          GenericListHelper.GetDifference<int>((IList<int>) new List<int>((IEnumerable<int>) numArray2), (IList<int>) intList1, GenericListHelper.SearchMode.smNotExistInB, out resultData);
        List<int> enabledObjectTypes = MetaDataHelper.GetTopParentEnabledObjectTypes((IEnumerable<int>) resultData);
        if (enabledObjectTypes.Count == 1)
        {
          if (resultData.IndexOf(enabledObjectTypes[0]) < 0)
          {
            enabledObjectTypes.Clear();
            enabledObjectTypes.AddRange((IEnumerable<int>) resultData);
          }
        }
        else
        {
          enabledObjectTypes.Clear();
          enabledObjectTypes.AddRange((IEnumerable<int>) resultData);
        }
        resultData = enabledObjectTypes;
      }
      IDBObjectCollection objectCollection = userSession.GetObjectCollection(-1);
      if (objectCollection == null)
        return objectDataEx;
      object tag;
      if (dbRsp.Tags?[(object) "ShowAllModifications"] != null && (tag = dbRsp.Tags[(object) "ShowAllModifications"]) is bool)
      {
        bool flag = (bool) tag;
        objectCollection.ShowAllModifications = flag;
      }
      ConditionStructure[] joinedConditions1;
      if (list2.Count != 0)
        joinedConditions1 = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) list2.ToArray(), LogicalOperators.NONE, 0, false)
        };
      else
        joinedConditions1 = new ConditionStructure[0];
      ObjInfoItem defaultValue = new ObjInfoItem();
      List<ObjInfoItem>[] objInfoItemListArray;
      if (list1.Count > 10)
        objInfoItemListArray = GenericListHelper.SplitByChanks<ObjInfoItem>((IList<ObjInfoItem>) list1, 150, true, defaultValue);
      else
        objInfoItemListArray = new List<ObjInfoItem>[1]
        {
          list1
        };
      foreach (IEnumerable<ObjInfoItem> objInfoList1 in objInfoItemListArray)
      {
        List<long> objectIds = ObjInfoHelper.GetObjectIDs(objInfoList1);
        DBRecordSetParams paramSet = dbRsp;
        if (objectIds != null && objectIds.Count > 0)
        {
          ConditionStructure[] joinedConditions2 = new ConditionStructure[1]
          {
            new ConditionStructure(-2, RelationalOperators.In, (object) objectIds.ToArray(), LogicalOperators.NONE, 0, false)
          };
          paramSet.Conditions = ConditionStructure.Join(joinedConditions2, paramSet.Conditions);
        }
        if (customCondList.Count == 0 && customColumns.Count == 0)
        {
          if (intList1 != null && intList1.Count == 0)
          {
            foreach (int num in resultData)
            {
              objectCollection.ObjectTypeID = num;
              DataTable dataTable = objectCollection.Select(paramSet);
              if (objectDataEx == null)
                objectDataEx = dataTable;
              else if (dataTable != null && dataTable.Rows.Count > objectDataEx.Rows.Count)
              {
                DataSetProcessor.AddTable(dataTable, objectDataEx, false);
                objectDataEx = dataTable;
              }
              else
                DataSetProcessor.AddTable(objectDataEx, dataTable, false);
            }
          }
          else
          {
            objectCollection.ObjectTypeID = -1;
            try
            {
              objectCollection.LocalTypesMode = true;
              paramSet.Conditions = ConditionStructure.Join(joinedConditions1, paramSet.Conditions);
              objectDataEx = objectCollection.Select(paramSet);
            }
            finally
            {
              objectCollection.LocalTypesMode = false;
            }
          }
        }
        else
        {
          if (objectIds == null || objectIds.Count == 0)
          {
            resultData.AddRange((IEnumerable<int>) intList1);
            GenericListHelper.MakeUnique<int>(resultData);
          }
          foreach (int num in resultData)
          {
            objectCollection.ObjectTypeID = num;
            DataTable dataTable = objectCollection.Select(paramSet);
            if (objectDataEx == null)
              objectDataEx = dataTable;
            else if (dataTable != null && dataTable.Rows.Count > objectDataEx.Rows.Count)
            {
              DataSetProcessor.AddTable(dataTable, objectDataEx, false);
              objectDataEx = dataTable;
            }
            else
              DataSetProcessor.AddTable(objectDataEx, dataTable, false);
          }
          if (objectIds != null && objectIds.Count != 0)
          {
            if (intList1 == null || intList1.Count > 0)
            {
              List<ObjInfoItem> objInfoList2 = ServiceUtils.GetService<ITypedInfoService>((object) userSession, true).UpdateUnknownTypes((IEnumerable<ObjInfoItem>) SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList((IEnumerable<long>) objectIds), (object) userSession.SessionGUID);
              List<int> intList2 = (List<int>) null;
              if (objInfoList2 != null)
              {
                intList2 = ObjInfoHelper.GetObjectTypes((IEnumerable<ObjInfoItem>) objInfoList2);
                GenericListHelper.MakeUnique<int>(intList2);
                intList2.Remove(-1);
                if (intList1 != null)
                {
                  List<int> allChildLocalTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) intList1);
                  intList2 = intList2.Where<int>((System.Func<int, bool>) (item => allChildLocalTypes.Contains(item))).ToList<int>();
                }
              }
              intList1 = intList2;
            }
            if (intList1 != null)
            {
              foreach (int num in intList1)
              {
                objectCollection.ObjectTypeID = num;
                DataTable dataTable = objectCollection.Select(paramSet);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                  if (objectDataEx == null)
                    objectDataEx = dataTable;
                  else if (dataTable.Rows.Count > objectDataEx.Rows.Count)
                  {
                    DataSetProcessor.AddTable(dataTable, objectDataEx, false);
                    objectDataEx = dataTable;
                  }
                  else
                    DataSetProcessor.AddTable(objectDataEx, dataTable, false);
                }
              }
            }
          }
        }
      }
    }
    return objectDataEx;
  }

  /// <summary>Получение строки сортировки для заданных условия</summary>
  /// <remarks>ColumnNameMapping.Default не поддерживаем в данный момент для сортировки</remarks>
  /// <param name="columns">Параметры сортировки</param>
  /// <returns></returns>
  public static string GetSortOrder(ColumnDescriptor[] columns)
  {
    if (columns == null || columns.Length == 0)
      return string.Empty;
    List<string> stringList = new List<string>();
    List<ColumnDescriptor> columnDescriptorList1 = new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) columns);
    List<ColumnDescriptor> columnDescriptorList2 = new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) columns);
    columnDescriptorList2.Sort((Comparison<ColumnDescriptor>) ((x, y) => x.OrderByID.CompareTo(y.OrderByID)));
    foreach (ColumnDescriptor columnDescriptor in columnDescriptorList2)
    {
      string str1 = string.Empty;
      string str2;
      switch (columnDescriptor.Sort)
      {
        case SortOrders.NONE:
          continue;
        case SortOrders.DESC:
          str2 = "DESC";
          break;
        default:
          str2 = "ASC";
          break;
      }
      int attributeId = DataHelper.GetAttributeID(columnDescriptor.AttributeID, false);
      if (attributeId != 0)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeId);
        if (attributeType != null)
        {
          switch (columnDescriptor.ColumnName)
          {
            case ColumnNameMapping.Default:
              continue;
            case ColumnNameMapping.ID:
              str1 = attributeId.ToString();
              break;
            case ColumnNameMapping.Guid:
              str1 = attributeType.AttributeGuid.ToString();
              break;
            case ColumnNameMapping.Alias:
              str1 = attributeType.Alias;
              break;
            case ColumnNameMapping.ShortName:
              str1 = attributeType.ShortName;
              break;
            case ColumnNameMapping.Name:
              str1 = attributeType.Name;
              break;
            case ColumnNameMapping.FieldName:
              str1 = attributeType.ValueFieldName;
              break;
            case ColumnNameMapping.Index:
              str1 = columnDescriptorList1.IndexOf(columnDescriptor).ToString();
              break;
          }
          if (!(str1 == string.Empty))
            stringList.Add(string.Format(DataHelper.Consts.cnt_fld_Sort_Template, (object) str1, (object) str2));
        }
      }
    }
    return string.Join(",", stringList.ToArray());
  }

  /// <summary>Сортировка таблицы согласно заданным условиям</summary>
  /// <remarks>ColumnNameMapping.Default не поддерживаем в данный момент для сортировки</remarks>
  /// <param name="table">Таблица с данными</param>
  /// <param name="columns">Параметры сортировки</param>
  /// <returns></returns>
  public static DataTable SortData(DataTable table, ColumnDescriptor[] columns)
  {
    if (table == null || columns == null || columns.Length == 0)
      return table;
    string sortOrder = DataHelper.GetSortOrder(columns);
    return sortOrder == string.Empty ? table : DataHelper.SortData(table, sortOrder);
  }

  /// <summary>Сортировка таблицы согласно заданным условиям</summary>
  /// <param name="table">Таблица с данными</param>
  /// <param name="sortOrder">Строка сортировки</param>
  /// <returns></returns>
  public static DataTable SortData(DataTable table, string sortOrder)
  {
    if (table == null || table.Rows.Count <= 1 || string.IsNullOrEmpty(sortOrder))
      return table;
    table.DefaultView.Sort = sortOrder;
    return table.DefaultView.ToTable();
  }

  /// <summary>
  /// Сортировка таблицы по составу (не применяемости) согласно заданным условиям
  /// </summary>
  /// <param name="table"></param>
  /// <param name="columns">Параметры сортировки</param>
  /// <param name="objInfoList">Список объектов для построения дерева</param>
  /// <returns></returns>
  public static DataTable SortCompositionData(
    DataTable table,
    ColumnDescriptor[] columns,
    List<ObjInfoItem> objInfoList)
  {
    return DataHelper.SortCompostionData(table, columns, objInfoList, false);
  }

  /// <summary>
  /// Сортировка таблицы по составу/применяемости согласно заданным условиям
  /// </summary>
  /// <remarks>ColumnNameMapping.Default не поддерживаем в данный момент для сортировки</remarks>
  /// <param name="table">Таблица с данными</param>
  /// <param name="columns">Параметры сортировки</param>
  /// <param name="objInfoList">Список объектов для построения дерева</param>
  /// <param name="applMode">Режим применяемости</param>
  /// <returns></returns>
  public static DataTable SortCompostionData(
    DataTable table,
    ColumnDescriptor[] columns,
    List<ObjInfoItem> objInfoList,
    bool applMode)
  {
    if (table == null || table.Rows.Count <= 1 || columns == null || columns.Length == 0 || objInfoList == null || objInfoList.Count == 0)
      return table;
    string sortOrder = DataHelper.GetSortOrder(columns);
    if (sortOrder == string.Empty)
      return table;
    int fldIdxObject = -1;
    int num = -1;
    int columnIndex = -1;
    for (int index = 0; index < table.Columns.Count; ++index)
    {
      string columnName = table.Columns[index].ColumnName;
      int attributeId = DataHelper.GetAttributeID((object) columnName, false);
      if (columnIndex == -1 && (columnName == "F_PRJLINK_ID" || attributeId == -20))
        columnIndex = index;
      else if (applMode)
      {
        if (num == -1 && columnName == DataHelper.Consts.cnt_fld_PartObjID)
          num = index;
        else if (fldIdxObject == -1 && (columnName == "F_PROJ_ID" || attributeId == -21))
          fldIdxObject = index;
      }
      else if (num == -1 && (columnName == "F_PROJ_ID" || attributeId == -21))
        num = index;
      else if (fldIdxObject == -1 && (columnName == "F_OBJECT_ID" || attributeId == -2))
        fldIdxObject = index;
    }
    if (columnIndex == -1 || fldIdxObject == -1 || num == -1)
      return table;
    table.DefaultView.Sort = sortOrder;
    Dictionary<long, List<DataRow>> ownerData = new Dictionary<long, List<DataRow>>(applMode ? table.Rows.Count : 0);
    foreach (DataRowView dataRowView in table.DefaultView)
    {
      if ((dataRowView.Row.RowState & DataRowState.Detached) != DataRowState.Detached && (dataRowView.Row.RowState & DataRowState.Deleted) != DataRowState.Deleted)
      {
        long int64 = Convert.ToInt64(dataRowView.Row[num]);
        List<DataRow> dataRowList;
        if (!ownerData.TryGetValue(int64, out dataRowList))
        {
          dataRowList = new List<DataRow>();
          ownerData.Add(int64, dataRowList);
        }
        dataRowList.Add(dataRowView.Row);
      }
    }
    DataTable sortedData = table.Clone();
    sortedData.MinimumCapacity = table.Rows.Count;
    sortedData.BeginLoadData();
    try
    {
      int count = sortedData.Columns.Count;
      object[] objArray = new object[count];
      Dictionary<long, bool> proceededList = new Dictionary<long, bool>(ownerData.Count);
      DataHelper.SortCompositionData(num, fldIdxObject, ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) objInfoList), ownerData, sortedData, proceededList, objArray);
      if (sortedData.Rows.Count == 0)
        return table;
      if (sortedData.Rows.Count == table.Rows.Count)
        return sortedData;
      List<long> list = new List<long>(sortedData.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) sortedData.Rows)
        list.Add(Convert.ToInt64(row[columnIndex]));
      GenericListHelper.MakeUnique<long>(list);
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        long int64 = Convert.ToInt64(row[columnIndex]);
        if (list.BinarySearch(int64) < 0)
        {
          DataSetProcessor.CopyDataToBuffer(row, objArray, count);
          sortedData.Rows.Add(objArray);
        }
      }
    }
    finally
    {
      sortedData.EndLoadData();
    }
    return sortedData;
  }

  /// <summary>Заполнение отсортированной таблицы согласно составу</summary>
  /// <param name="fldIdxOwner">Индекс поля - родителя</param>
  /// <param name="fldIdxObject">Индекс паля - объекта</param>
  /// <param name="ownerList">Список объектов для построения дерева</param>
  /// <param name="ownerData">Структура с данными для разворота дерева</param>
  /// <param name="sortedData">Таблица с отсортированными данными</param>
  /// <param name="proceededList">Список "обработанных" объектов</param>
  /// <param name="rowDataBuffer">Временный буфер для ускорения вставки значений</param>
  private static void SortCompositionData(
    int fldIdxOwner,
    int fldIdxObject,
    List<long> ownerList,
    Dictionary<long, List<DataRow>> ownerData,
    DataTable sortedData,
    Dictionary<long, bool> proceededList,
    object[] rowDataBuffer)
  {
    if (ownerList == null || ownerList.Count == 0 || ownerData == null || ownerData.Count == 0 || sortedData == null)
      return;
    List<long> ownerList1 = new List<long>();
    int length = rowDataBuffer.Length;
    foreach (long owner in ownerList)
    {
      List<DataRow> dataRowList;
      if (!proceededList.ContainsKey(owner) && ownerData.TryGetValue(owner, out dataRowList))
      {
        if (ownerList1.Capacity < dataRowList.Count)
          ownerList1.Capacity = dataRowList.Count;
        proceededList.Add(owner, true);
        foreach (DataRow dataRow in dataRowList)
        {
          ownerList1.Clear();
          ownerList1.Add(Convert.ToInt64(dataRow[fldIdxObject]));
          DataSetProcessor.CopyDataToBuffer(dataRow, rowDataBuffer, length);
          sortedData.Rows.Add(rowDataBuffer);
          DataHelper.SortCompositionData(fldIdxOwner, fldIdxObject, ownerList1, ownerData, sortedData, proceededList, rowDataBuffer);
        }
      }
    }
  }

  /// <summary>
  /// Обновление информации по неопределенным типам объектов
  /// </summary>
  /// <param name="objInfoItems"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static bool UpdateUnknownTypes(List<ObjInfoItem> objInfoItems, IUserSession session)
  {
    if (objInfoItems == null || !objInfoItems.Any<ObjInfoItem>())
      return false;
    SomeTypedInfoHelper<ObjInfoItem>.RemoveDuplicateEmpty(objInfoItems);
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objInfoItems, session);
    return true;
  }

  /// <summary>
  /// Разделение списка типов объектов на обычные и локальные
  /// </summary>
  /// <param name="objInfoItems">Общий список типов объектов</param>
  /// <param name="normalChildTypes">Список "нормальных" типов объектов</param>
  /// <param name="localChildTypes">Список локальных типов</param>
  /// <returns></returns>
  [Obsolete("Will be removed in IPS 8.0")]
  public static bool ParseObjectTypes(
    List<ObjInfoItem> objInfoItems,
    ref List<int> normalChildTypes,
    ref List<int> localChildTypes)
  {
    return DataHelper.ParseObjectTypes(objInfoItems, ref normalChildTypes, ref localChildTypes, false);
  }

  /// <summary>
  /// Разделение списка типов объектов на обычные и локальные
  /// </summary>
  /// <param name="objTypeIds">Общий список типов объектов</param>
  /// <param name="normalChildTypes">Список "нормальных" типов объектов</param>
  /// <param name="localChildTypes">Список локальных типов</param>
  [Obsolete("Will be removed in IPS 8.0")]
  public static bool ParseObjectTypes(
    List<int> objTypeIds,
    ref List<int> normalChildTypes,
    ref List<int> localChildTypes)
  {
    return DataHelper.ParseObjectTypes(objTypeIds, ref normalChildTypes, ref localChildTypes, false);
  }

  /// <summary>
  /// Разделение списка типов объектов на обычные и локальные
  /// </summary>
  /// <param name="objInfoItems">Общий список типов объектов</param>
  /// <param name="normalChildTypes">Список "нормальных" типов объектов</param>
  /// <param name="localChildTypes">Список локальных типов</param>
  /// <param name="parentTypesOnly">Признак исключения из списка "нормальных" всех не родительских типов</param>
  [Obsolete("Will be removed in IPS 8.0")]
  public static bool ParseObjectTypes(
    List<ObjInfoItem> objInfoItems,
    ref List<int> normalChildTypes,
    ref List<int> localChildTypes,
    bool parentTypesOnly)
  {
    if (objInfoItems == null || objInfoItems.Count == 0)
      return false;
    List<int> objTypeIds = new List<int>(objInfoItems.Count);
    foreach (ObjInfoItem objInfoItem in objInfoItems)
    {
      if (objInfoItem.ObjTypeID != -1)
        objTypeIds.Add(objInfoItem.ObjTypeID);
    }
    objTypeIds.Sort();
    for (int index = objTypeIds.Count - 1; index > 0; --index)
    {
      if (objTypeIds[index] == objTypeIds[index - 1])
        objTypeIds.RemoveAt(index);
    }
    return DataHelper.ParseObjectTypes(objTypeIds, ref normalChildTypes, ref localChildTypes, parentTypesOnly);
  }

  /// <summary>
  /// Разделение списка типов объектов на обычные и локальные
  /// </summary>
  /// <param name="objTypeIds">Общий список типов объектов</param>
  /// <param name="normalChildTypes">Список "нормальных" типов объектов</param>
  /// <param name="localChildTypes">Список локальных типов</param>
  /// <param name="parentTypesOnly">Признак исключения из списка "нормальных" всех не родительских типов</param>
  [Obsolete("Will be removed in IPS 8.0")]
  public static bool ParseObjectTypes(
    List<int> objTypeIds,
    ref List<int> normalChildTypes,
    ref List<int> localChildTypes,
    bool parentTypesOnly)
  {
    if (normalChildTypes == null || localChildTypes == null)
      return false;
    if (objTypeIds == null || objTypeIds.Count == 0)
      return true;
    List<int> intList = (List<int>) null;
    if (parentTypesOnly)
    {
      if (parentTypesOnly)
        intList = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) objTypeIds);
    }
    else
      intList = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) objTypeIds);
    if (intList == null)
      return false;
    foreach (int objTypeID in intList)
    {
      if (objTypeID != -1)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
        if (objectType != null)
        {
          if (objectType.IsLocalType)
            localChildTypes.Add(objTypeID);
          else
            normalChildTypes.Add(objTypeID);
        }
      }
    }
    return true;
  }

  /// <summary>Загрузка данных из таблицы в список объектов</summary>
  /// <param name="sourceTable">Исходная таблица с данными</param>
  /// <param name="objInfoItems">Результирующий список</param>
  /// <returns></returns>
  [Obsolete("Use ObjInfoDbScheme.ParseItems instead. Will be removed in IPS 8.0")]
  public static bool ParseObjInfoItems(DataTable sourceTable, IList<ObjInfoItem> objInfoItems)
  {
    return sourceTable != null && new ObjInfoDbScheme(sourceTable.Columns.IndexOf("F_OBJECT_ID"), sourceTable.Columns.IndexOf("F_OBJECT_TYPE")).ParseItems((IEnumerable<DataRow>) sourceTable.AsEnumerable(), (ICollection<ObjInfoItem>) objInfoItems);
  }

  /// <summary>Загрузка данных из таблицы в список объектов</summary>
  /// <param name="sourceTable">Исходная таблица с данными</param>
  /// <param name="objInfoItems">Результирующий список</param>
  /// <param name="objectIdField">Имя поля для ид. версий объектов</param>
  /// <param name="objectTypeField">Имя поля для типа объектов</param>
  /// <returns></returns>
  [Obsolete("Use ObjInfoDbScheme.ParseItems instead. Will be removed in IPS 8.0")]
  public static bool ParseObjInfoItems(
    DataTable sourceTable,
    IList<ObjInfoItem> objInfoItems,
    string objectIdField,
    string objectTypeField)
  {
    return sourceTable != null && new ObjInfoDbScheme(sourceTable.Columns.IndexOf(objectIdField), sourceTable.Columns.IndexOf(objectTypeField)).ParseItems((IEnumerable<DataRow>) sourceTable.AsEnumerable(), (ICollection<ObjInfoItem>) objInfoItems);
  }

  /// <summary>Загрузка данных из таблицы в список связей</summary>
  /// <param name="sourceTable">Исходная таблица с данными</param>
  /// <param name="composition"></param>
  /// <param name="relInfoItems">Результирующий список</param>
  /// <returns></returns>
  [Obsolete("Use RelInfoDbScheme.ParseItems instead. Will be removed in IPS 8.0")]
  public static bool ParseRelInfoItems(
    DataTable sourceTable,
    bool composition,
    ref List<RelInfoItem> relInfoItems)
  {
    return new RelInfoDbScheme().ParseItems((IEnumerable<DataRow>) sourceTable.AsEnumerable(), (ICollection<RelInfoItem>) relInfoItems);
  }

  /// <summary>Загрузка данных из таблицы в список связей</summary>
  /// <param name="sourceTable">Исходная таблица с данными</param>
  /// <param name="relInfoItems">Результирующий список</param>
  /// <param name="relationIdField">Имя поля для ид. связи</param>
  /// <param name="relationTypeField">Имя поля для типа связи</param>
  /// <returns></returns>
  [Obsolete("Use RelInfoDbScheme.ParseItems instead. Will be removed in IPS 8.0")]
  public static bool ParseRelInfoItems(
    DataTable sourceTable,
    ref List<RelInfoItem> relInfoItems,
    string relationIdField,
    string relationTypeField)
  {
    return sourceTable != null && new RelInfoDbScheme(relationIdField, relationTypeField).ParseItems(sourceTable != null ? (IEnumerable<DataRow>) sourceTable.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<RelInfoItem>) relInfoItems);
  }

  /// <summary>Загрузка данных из таблицы в список связей</summary>
  /// <param name="sourceTable">Исходная таблица с данными</param>
  /// <param name="isComposition"></param>
  /// <param name="relInfoItems">Результирующий список</param>
  /// <param name="session">Если пользовательская сессия не задана - информация по отсутствующим типам загружена не будет</param>
  /// <returns></returns>
  [Obsolete("Use RelInfoDbScheme.ParseItems instead. Will be removed in IPS 8.0")]
  public static bool ParseRelInfoItems(
    DataTable sourceTable,
    bool isComposition,
    ref List<RelObjInfoItem> relInfoItems,
    IUserSession session = null)
  {
    return sourceTable != null && new RelObjInfoDbScheme<ObjInfoItem>(isComposition).ParseInfoItems(session, sourceTable != null ? (IEnumerable<DataRow>) sourceTable.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<RelObjInfoItem>) relInfoItems);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sourceTable"></param>
  /// <param name="relInfoItems"></param>
  /// <param name="session"></param>
  /// <param name="relationIdField"></param>
  /// <param name="relationTypeField"></param>
  /// <param name="projObjIdField"></param>
  /// <param name="partObjIdField"></param>
  /// <param name="projObjTypeField"></param>
  /// <param name="partObjTypeField"></param>
  /// <returns></returns>
  [Obsolete("Use RelInfoDbScheme.ParseItems instead. Will be removed in IPS 8.0")]
  public static bool ParseRelInfoItems(
    DataTable sourceTable,
    ref List<RelObjInfoItem> relInfoItems,
    IUserSession session = null,
    string relationIdField = "F_PRJLINK_ID",
    string relationTypeField = "F_RELATION_TYPE",
    string projObjIdField = "F_PROJ_ID",
    string partObjIdField = "F_OBJECT_ID",
    string projObjTypeField = "",
    string partObjTypeField = "F_OBJECT_TYPE")
  {
    return sourceTable != null && new RelObjInfoDbScheme<ObjInfoItem>(relationIdField, relationTypeField, projObjIdField, partObjIdField, projObjTypeField, partObjTypeField).ParseInfoItems(session, sourceTable != null ? (IEnumerable<DataRow>) sourceTable.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<RelObjInfoItem>) relInfoItems);
  }

  /// <summary>Constant's keeper</summary>
  public static class Consts
  {
    /// <summary>Размерность пакетов в запросах</summary>
    /// <remarks>
    /// Быстрее всего выполняются SQL запросы, когда используется "разогретый кэш " СУБД.
    /// В этом можно убедиться, запустив повторное выполнение запроса с теми же параметрами - оно займет крайне малое время.
    /// По этой причине куча мелких запросов (с IN условием)  в "разогретый кэш" выполниться в разы быстрее чем один большой запрос в "холодный" кэш с тем же
    /// общим количеством значений в IN, при условии что запросы параметризованы . Это касается таких СУБД как Oracle и MsSQL.
    /// Кроме того имеет значение и кол-во данных в самом условии IN. В результате многочисленных тестов установлено, что значение находиться
    /// в диапазоне от 100 до 400 - в зависимости от типа СУБД.
    /// 
    /// В результате всего выше изложенного следует - для ускорения запросов с IN следует в условии задавать жестко количество данных = SQL_PACKET_SIZE в параметризованных
    /// запросах и недостающие значение заполнять пустышками (например 0 )
    /// </remarks>
    public const int SQL_RECOMMENDED_PACKET_SIZE = 150;
    /// <summary>Нижний индекс для системных сортировок</summary>
    public static int cnt_idx_FixedSortMin = 900;
    /// <summary>Верхний индекс системных сортировок</summary>
    /// <remarks>999 уже используется во внешних сортировках</remarks>
    public static int cnt_idx_FixedSortMax = 998;
    /// <summary>Шаблон для поля сортировки</summary>
    public static string cnt_fld_Sort_Template = "[{0}] {1}";
    /// <summary>
    /// Правило фильтрации по умолчанию при раскрытии составов
    /// </summary>
    public static string cnt_def_filtrationRule = "cad001e2-306c-11d8-b4e9-00304f19f545";
    /// <summary>
    /// Данный ключ может передаваться в дополнительных настройках Tag параметров запроса в коллекцию объектов.
    /// Позволяет указать опции выбора объектов без учета контекста
    /// </summary>
    public const string ShowAllModifications = "ShowAllModifications";
    /// <summary>
    /// F_PART_OBJ_ID (Поле с ид. версии дочернего узла при поиске применяемости объекта)
    /// </summary>
    public static string cnt_fld_PartObjID = "F_PART_OBJ_ID";
  }

  /// <summary>
  /// 
  /// </summary>
  /// <remarks>Для возможности кэшировать структуры</remarks>
  private class ColumnsDKeeper
  {
    /// <summary>Данные столбца</summary>
    public ColumnDescriptor Data;

    /// <summary>Конструктор</summary>
    /// <param name="value"></param>
    public ColumnsDKeeper(ColumnDescriptor value) => this.Data = value;
  }
}
