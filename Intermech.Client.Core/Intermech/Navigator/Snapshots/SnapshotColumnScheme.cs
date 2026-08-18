
// Type: Intermech.Navigator.Snapshots.SnapshotColumnScheme
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Snapshots;

/// <summary>схема для колонок закладки с итерациями</summary>
public class SnapshotColumnScheme : INodeColumnScheme
{
  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public string Name => LocalizationHolder.rm.GetString("Client.Core_1405");

  /// <summary>
  /// Возвращает постоянное имя колонки, которое можно использовать
  /// для долговременного хранения (т.е. между сеансами работы
  /// универсального клиента).
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Постоянное имя колонки</returns>
  public string ColumnIDToPersistName(object columnID) => columnID.ToString();

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
    int result;
    if (!int.TryParse(persistName, out result))
      throw new Exception($"Unknown snapshot column scheme column id persist format: \"{persistName}\". Must be convertable to int string");
    return (object) result;
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
    return this.CreateColumn(schemeGuid, columnID, NodeColumnSortOrder.None, -1);
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
    if (!(columnID is int))
      return (NodeColumn) null;
    FieldTypes attrType1 = FieldTypes.ftInteger;
    Type dataType1 = typeof (long);
    string caption1 = DataSetProcessor.GetCaption("F_SNAPSHOT_ID");
    if (Convert.ToInt32(columnID) == SnapshotConsts.SNAPSHOT_DATE)
    {
      attrType1 = FieldTypes.ftDateTime;
      dataType1 = typeof (DateTime);
      caption1 = DataSetProcessor.GetCaption("F_SNAPSHOT_DATE");
    }
    else if (Convert.ToInt32(columnID) == SnapshotConsts.F_NAME)
    {
      attrType1 = FieldTypes.ftString;
      dataType1 = typeof (string);
      caption1 = DataSetProcessor.GetCaption("F_NAME");
    }
    else if (Convert.ToInt32(columnID) == SnapshotConsts.F_COMPARE_RESULT)
    {
      FieldTypes attrType2 = FieldTypes.ftInteger;
      Type dataType2 = typeof (CompositionCompareResult);
      string caption2 = "Результат сравнения";
      VirtualNodeColumn column = new VirtualNodeColumn(schemeGuid, columnID, dataType2, attrType2, caption2);
      column.Priority = SchemeColumnPriority.Highest;
      return (NodeColumn) column;
    }
    return new NodeColumn(schemeGuid, columnID, dataType1, attrType1, caption1, sortOrder, sortIndex)
    {
      Priority = SchemeColumnPriority.Highest
    };
  }

  /// <summary>
  /// Возвращает преобразование по умолчанию для указанной виртуальной
  /// колонки. Если преобразование не задано, то метод вернет null.
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Преобразование по умолчанию</returns>
  public INodeColumnTransform GetDefaultTransform(object columnID)
  {
    return columnID.Equals((object) SnapshotConsts.F_COMPARE_RESULT) ? CompositionCompareResultTransform.Instance : (INodeColumnTransform) null;
  }
}
