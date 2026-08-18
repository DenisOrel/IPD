// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.StructureView.ArchiveStructureColumnScheme
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Archives.StructureView;

/// <summary>
/// схема отобаржения колонок для закладки Структура архива
/// </summary>
internal class ArchiveStructureColumnScheme : INodeColumnScheme
{
  public static Guid ArchiveStructureShemeGuid = new Guid("0CC75EF6-4250-4534-9F51-E092900D44F6");

  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public string Name => ServiceHolder.rm.GetString("Archives_77");

  /// <summary>
  /// Возвращает постоянное имя колонки, которое можно использовать
  /// для долговременного хранения (т.е. между сеансами работы
  /// универсального клиента).
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Постоянное имя колонки</returns>
  public string ColumnIDToPersistName(object columnID)
  {
    if (columnID == null)
      throw new Exception("ColumnID must not be null");
    int index = -Convert.ToInt32(columnID) - 10000;
    return ConstsHolder.ArchiveStructureColumns[index];
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
    if (!string.IsNullOrEmpty(persistName))
      return (object) -(ConstsHolder.ArchiveStructureColumns.IndexOf(persistName) - 10000);
    throw new Exception("PersistName must not be null or empty");
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
    NodeColumn column = this.CreateColumn(schemeGuid, columnID, NodeColumnSortOrder.None, -1);
    if (column != null)
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
    if (!schemeGuid.Equals(ArchiveStructureColumnScheme.ArchiveStructureShemeGuid) || columnID == null)
      return (NodeColumn) null;
    string str = columnID.ToString();
    if (ConstsHolder.ColumnCaptionsCach.ContainsKey(str))
      str = ConstsHolder.ColumnCaptionsCach[str];
    columnID = (object) (-ConstsHolder.ArchiveStructureColumns.IndexOf(columnID.ToString()) - 10000);
    return new NodeColumn(schemeGuid, columnID, typeof (string), FieldTypes.ftString, str, sortOrder, sortIndex)
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
  public INodeColumnTransform GetDefaultTransform(object columnID) => (INodeColumnTransform) null;
}
