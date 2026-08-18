
// Type: Intermech.Navigator.EventLog.ColumnScheme
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Specialized;


namespace Intermech.Navigator.EventLog;

/// <summary>
/// Схема виртуальных колонок, описывающая атрибуты записей из журнала событий.
/// Идентификаторы виртуальных колонок - это значения перечисления
/// ObligatoryObjectAttributes.
/// </summary>
internal class ColumnScheme : INodeColumnScheme
{
  /// <summary>Коллекция преобразователей</summary>
  private IDictionary _transforms = (IDictionary) new HybridDictionary();
  /// <summary>Название схемы</summary>
  private static readonly string _schemeName = LocalizationHolder.rm.GetString("Client.Core_607");

  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public string Name => ColumnScheme._schemeName;

  /// <summary>
  /// Возвращает постоянное имя колонки, которое можно использовать
  /// для долговременного хранения (т.е. между сеансами работы
  /// универсального клиента).
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Постоянное имя колонки</returns>
  public string ColumnIDToPersistName(object columnID)
  {
    return columnID is ObligatoryObjectAttributes ? ((int) columnID).ToString() : string.Empty;
  }

  /// <summary>
  /// Восстанавливает идентификатор виртуальной колонки по ее
  /// постоянному имени, которое действительно только на текущий сеанс
  /// работы универсального клиента. Если восстанавливаемая колонка не
  /// существует, то метод должен вернуть null.
  /// </summary>
  /// <param name="persistName">Постоянное имя колонки</param>
  /// <returns>Идентификатор виртуальной колонки</returns>
  public object PersistNameToColumnID(string persistName)
  {
    try
    {
      return (object) (ObligatoryObjectAttributes) int.Parse(persistName);
    }
    catch
    {
    }
    return (object) null;
  }

  /// <summary>
  /// Создает виртуальную колонку без сортировки по указанному
  /// идентификатору. Если колонки с заданным идентификатором в схеме нет -
  /// то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Виртуальная колонка</returns>
  public NodeColumn CreateColumn(Guid schemeGuid, object columnID)
  {
    if (!(columnID is ObligatoryObjectAttributes columnID1))
      return (NodeColumn) null;
    NodeColumn column = this.CreateColumn(schemeGuid, columnID1, NodeColumnSortOrder.None, -1);
    column.Priority = SchemeColumnPriority.High;
    return column;
  }

  /// <summary>
  /// Создает виртуальную колонку с заданным направлением сортировки по
  /// указанному идентификатору. Если колонки с такми идентификатором в
  /// схеме нет - то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Очерёдность сортировки (-1 - не сортируется)</param>
  /// <returns>Виртуальная колонка</returns>
  public NodeColumn CreateColumn(
    Guid schemeGuid,
    object columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    if (!(columnID is ObligatoryObjectAttributes columnID1))
      return (NodeColumn) null;
    NodeColumn column = this.CreateColumn(schemeGuid, columnID1, sortOrder, sortIndex);
    column.Priority = SchemeColumnPriority.High;
    return column;
  }

  /// <summary>
  /// Создает виртуальную колонку с заданным направлением сортировки по
  /// указанному идентификатору. Если колонки с такми идентификатором в
  /// схеме нет - то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Очерёдность сортировки (-1 - не сортируется)</param>
  /// <returns>Виртуальная колонка</returns>
  private NodeColumn CreateColumn(
    Guid schemeGuid,
    ObligatoryObjectAttributes columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    NodeColumn column = new NodeColumn(schemeGuid, (object) columnID, this.GetColumnType(columnID), this.GetColumnAttrType(columnID), ObligatoryObjectAttributesHelper.GetCaption(columnID), sortOrder, sortIndex);
    if (columnID == ObligatoryObjectAttributes.F_AUDIT_TYPE || columnID == ObligatoryObjectAttributes.F_EVENT_TYPE || columnID == ObligatoryObjectAttributes.F_CATEGORY_TYPE || columnID == ObligatoryObjectAttributes.F_USER_ID || columnID == ObligatoryObjectAttributes.F_NOTE)
    {
      column.DisableSorting = true;
      column.DisableGrouping = true;
    }
    column.Priority = SchemeColumnPriority.High;
    return column;
  }

  /// <summary>
  /// Возвращает преобразование по умолчанию для указанной виртуальной
  /// колонки. Если преобразование не задано, то метод вернет null.
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Преобразование по умолчанию</returns>
  public INodeColumnTransform GetDefaultTransform(object columnID) => (INodeColumnTransform) null;

  /// <summary>Определить тип колонки</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>Тип данных в колонке</returns>
  private Type GetColumnType(ObligatoryObjectAttributes columnID)
  {
    switch (columnID)
    {
      case ObligatoryObjectAttributes.F_AUDIT_TYPE:
        return typeof (int);
      case ObligatoryObjectAttributes.F_END_DATE:
        return typeof (DateTime);
      case ObligatoryObjectAttributes.F_BEGIN_DATE:
        return typeof (DateTime);
      case ObligatoryObjectAttributes.F_EVENT_TYPE:
        return typeof (int);
      case ObligatoryObjectAttributes.F_NOTE:
        return typeof (string);
      case ObligatoryObjectAttributes.F_COMPUTER_NAME:
        return typeof (string);
      case ObligatoryObjectAttributes.F_USER_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_OBJECT_NAME:
        return typeof (string);
      case ObligatoryObjectAttributes.F_RELATION_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_CATEGORY_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_CATEGORY_TYPE:
        return typeof (int);
      case ObligatoryObjectAttributes.F_EVENT_ID:
        return typeof (long);
      case ObligatoryObjectAttributes.F_OBJECT_ID:
        return typeof (long);
      default:
        return (Type) null;
    }
  }

  /// <summary>
  /// Определить тип данных FieldType для идентификатора колонки
  /// </summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>Тип данных FieldType</returns>
  private FieldTypes GetColumnAttrType(ObligatoryObjectAttributes columnID)
  {
    switch (columnID)
    {
      case ObligatoryObjectAttributes.F_AUDIT_TYPE:
        return FieldTypes.ftInteger;
      case ObligatoryObjectAttributes.F_END_DATE:
        return FieldTypes.ftDateTime;
      case ObligatoryObjectAttributes.F_BEGIN_DATE:
        return FieldTypes.ftDateTime;
      case ObligatoryObjectAttributes.F_EVENT_TYPE:
        return FieldTypes.ftInteger;
      case ObligatoryObjectAttributes.F_NOTE:
        return FieldTypes.ftString;
      case ObligatoryObjectAttributes.F_COMPUTER_NAME:
        return FieldTypes.ftString;
      case ObligatoryObjectAttributes.F_USER_ID:
        return FieldTypes.ftInteger;
      case ObligatoryObjectAttributes.F_OBJECT_NAME:
        return FieldTypes.ftString;
      case ObligatoryObjectAttributes.F_RELATION_ID:
        return FieldTypes.ftInteger;
      case ObligatoryObjectAttributes.F_CATEGORY_ID:
        return FieldTypes.ftInteger;
      case ObligatoryObjectAttributes.F_CATEGORY_TYPE:
        return FieldTypes.ftInteger;
      case ObligatoryObjectAttributes.F_EVENT_ID:
        return FieldTypes.ftInteger;
      case ObligatoryObjectAttributes.F_OBJECT_ID:
        return FieldTypes.ftInteger;
      default:
        return FieldTypes.ftUnknown;
    }
  }
}
