
// Type: Intermech.Navigator.NameColumnScheme
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator;

/// <summary>
/// Схема виртуальных колонок, состоящая только из одной колонки - F_CAPTION.
/// Предназначена для использования во всевозможных виртуальных элементах
/// из пространства навигации.
/// </summary>
internal class NameColumnScheme : INodeColumnScheme
{
  /// <summary>Название схемы колонок</summary>
  private static readonly string _schemeName = LocalizationHolder.rm.GetString("Client.Core_842");
  /// <summary>Идентификатор колонки</summary>
  private const string _schemeColumnID = "F_CAPTION";
  /// <summary>Заголовок колонки</summary>
  private static readonly string _schemeColumnCaption = LocalizationHolder.rm.GetString("Client.Core_843");

  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public string Name => NameColumnScheme._schemeName;

  /// <summary>
  /// Возвращает постоянное имя колонки, которое можно использовать
  /// для долговременного хранения (т.е. между сеансами работы
  /// универсального клиента).
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Постоянное имя колонки</returns>
  public string ColumnIDToPersistName(object columnID)
  {
    return columnID.Equals((object) "F_CAPTION") ? "F_CAPTION" : string.Empty;
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
    return persistName == "F_CAPTION" ? (object) "F_CAPTION" : (object) null;
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
    if (!columnID.Equals((object) "F_CAPTION"))
      return (NodeColumn) null;
    NodeColumn column = this.CreateColumn(schemeGuid, NodeColumnSortOrder.None, -1);
    column.Priority = SchemeColumnPriority.Highest;
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
    if (!columnID.Equals((object) "F_CAPTION"))
      return (NodeColumn) null;
    NodeColumn column = this.CreateColumn(schemeGuid, sortOrder, sortIndex);
    column.Priority = SchemeColumnPriority.Highest;
    return column;
  }

  /// <summary>
  /// Возвращает преобразование по умолчанию для указанной виртуальной
  /// колонки. Если преобразование не задано, то метод вернет null.
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Преобразование по умолчанию</returns>
  public INodeColumnTransform GetDefaultTransform(object columnID) => (INodeColumnTransform) null;

  /// <summary>
  /// Создает виртуальную колонку с заданным направлением сортировки по
  /// указанному идентификатору. Если колонки с такми идентификатором в
  /// схеме нет - то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Очерёдность сортировки (-1 - не сортируется)</param>
  /// <returns>Виртуальная колонка</returns>
  private NodeColumn CreateColumn(Guid schemeGuid, NodeColumnSortOrder sortOrder, int sortIndex)
  {
    return new NodeColumn(schemeGuid, (object) "F_CAPTION", typeof (string), FieldTypes.ftString, NameColumnScheme._schemeColumnCaption, sortOrder, sortIndex)
    {
      Priority = SchemeColumnPriority.Highest
    };
  }
}
