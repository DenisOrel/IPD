
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypeColumnScheme
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Схема виртуальных колонок, описывающая атрибуты типов объектов.
/// Идентификаторы виртуальных колонок - это имена атрибутов из Intermech.Consts.
/// Поддерживаются следующие колонки: F_OBJECT_TYPE, F_OBJ_TYPE_NAME, F_OBJ_NAME.
/// </summary>
public sealed class ObjectTypeColumnScheme : INodeColumnScheme
{
  /// <summary>Название схемы колонок</summary>
  private static readonly string _schemeName = LocalizationHolder.rm.GetString("Client.Core_381");

  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public string Name => ObjectTypeColumnScheme._schemeName;

  /// <summary>
  /// Возвращает постоянное имя колонки, которое можно использовать
  /// для долговременного хранения (т.е. между сеансами работы
  /// универсального клиента).
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Постоянное имя колонки</returns>
  public string ColumnIDToPersistName(object columnID)
  {
    return this.IsSupportedColumnID(columnID) ? (string) columnID : string.Empty;
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
    return this.IsSupportedColumnID((object) persistName) ? (object) persistName : (object) null;
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
    return this.IsSupportedColumnID(columnID) ? this.CreateColumn(schemeGuid, (string) columnID, NodeColumnSortOrder.None, -1) : (NodeColumn) null;
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
    return this.IsSupportedColumnID(columnID) ? this.CreateColumn(schemeGuid, (string) columnID, sortOrder, sortIndex) : (NodeColumn) null;
  }

  /// <summary>
  /// Возвращает преобразование по умолчанию для указанной виртуальной
  /// колонки. Если преобразование не задано, то метод вернет null.
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Преобразование по умолчанию</returns>
  public INodeColumnTransform GetDefaultTransform(object columnID) => (INodeColumnTransform) null;

  /// <summary>Поддерживается ли колонка</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>true, если колонка поддерживается</returns>
  private bool IsSupportedColumnID(object columnID)
  {
    return columnID.Equals((object) "F_OBJECT_TYPE") || columnID.Equals((object) "F_OBJ_TYPE_NAME") || columnID.Equals((object) "F_OBJ_NAME");
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
    string columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    return new NodeColumn(schemeGuid, (object) columnID, this.GetColumnType(columnID), this.GetColumnAttrType(columnID), this.GetCaption(columnID), sortOrder, sortIndex);
  }

  /// <summary>Вернуть тип данных для колонки</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>Тип данных для колонки</returns>
  private Type GetColumnType(string columnID)
  {
    switch (columnID)
    {
      case "F_OBJECT_TYPE":
        return typeof (int);
      case "F_OBJ_TYPE_NAME":
        return typeof (string);
      case "F_OBJ_NAME":
        return typeof (string);
      default:
        return (Type) null;
    }
  }

  /// <summary>Вернуть тип данных FieldTypes для колонки</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>Тип данных FieldTypes для колонки</returns>
  private FieldTypes GetColumnAttrType(string columnID)
  {
    switch (columnID)
    {
      case "F_OBJECT_TYPE":
        return FieldTypes.ftInteger;
      case "F_OBJ_TYPE_NAME":
        return FieldTypes.ftString;
      case "F_OBJ_NAME":
        return FieldTypes.ftString;
      default:
        return FieldTypes.ftUnknown;
    }
  }

  /// <summary>Вернуть заголовок колонки</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>Заголовок колонки</returns>
  private string GetCaption(string columnID)
  {
    switch (columnID)
    {
      case "F_OBJECT_TYPE":
        return LocalizationHolder.rm.GetString("Client.Core_382");
      case "F_OBJ_TYPE_NAME":
        return LocalizationHolder.rm.GetString("Client.Core_383");
      case "F_OBJ_NAME":
        return LocalizationHolder.rm.GetString("Client.Core_384");
      default:
        return string.Empty;
    }
  }
}
