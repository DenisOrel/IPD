
// Type: Intermech.Navigator.NavigatorColumnScheme
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Specialized;


namespace Intermech.Navigator;

/// <summary>
/// Схема виртуальных колонок, состоящая только из одной колонки - F_CAPTION.
/// Используется деревом навигатора в режиме одной колонки. Должна
/// поддерживаться всем реализациями INode.
/// </summary>
public class NavigatorColumnScheme : INodeColumnScheme
{
  /// <summary>Коллекция преобразователей</summary>
  private IDictionary _transforms = (IDictionary) new HybridDictionary();
  /// <summary>Название схемы колонок</summary>
  private static readonly string SchemeName = LocalizationHolder.rm.GetString("Client.Core_842");
  /// <summary>Колонки по умолчанию</summary>
  private static readonly NavigatorColumnScheme.ColumnInfo[] Columns = new NavigatorColumnScheme.ColumnInfo[2]
  {
    new NavigatorColumnScheme.ColumnInfo("F_CAPTION", typeof (string), FieldTypes.ftString, LocalizationHolder.rm.GetString("Client.Core_843")),
    new NavigatorColumnScheme.ColumnInfo("F_STATUSES", typeof (byte[]), FieldTypes.ftSystem, LocalizationHolder.rm.GetString("Client.Core_844"))
  };

  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public string Name => NavigatorColumnScheme.SchemeName;

  /// <summary>
  /// Возвращает постоянное имя колонки, которое можно использовать
  /// для долговременного хранения (т.е. между сеансами работы
  /// универсального клиента).
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Постоянное имя колонки</returns>
  public string ColumnIDToPersistName(object columnID)
  {
    int index = this.IndexOf(columnID);
    return index < 0 ? string.Empty : NavigatorColumnScheme.Columns[index].Id;
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
    int index = this.IndexOf((object) persistName);
    return index < 0 ? (object) null : (object) NavigatorColumnScheme.Columns[index].Id;
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
    int index = this.IndexOf(columnID);
    return index < 0 ? (NodeColumn) null : this.CreateColumn(schemeGuid, NavigatorColumnScheme.Columns[index], NodeColumnSortOrder.None, -1);
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
    int index = this.IndexOf(columnID);
    return index < 0 ? (NodeColumn) null : this.CreateColumn(schemeGuid, NavigatorColumnScheme.Columns[index], sortOrder, sortIndex);
  }

  /// <summary>
  /// Возвращает преобразование по умолчанию для указанной виртуальной
  /// колонки. Если преобразование не задано, то метод вернет null.
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Преобразование по умолчанию</returns>
  public INodeColumnTransform GetDefaultTransform(object columnID)
  {
    lock (this._transforms)
    {
      if (this._transforms.Count == 0)
      {
        CaptionTransform captionTransform = new CaptionTransform();
        this._transforms.Add((object) -50, (object) captionTransform);
        this._transforms.Add((object) ObligatoryObjectAttributes.CAPTION, (object) captionTransform);
        this._transforms.Add((object) "F_CAPTION", (object) captionTransform);
        this._transforms.Add((object) "CAPTION", (object) captionTransform);
      }
      if (this._transforms.Contains(columnID))
        return (INodeColumnTransform) this._transforms[columnID];
    }
    return (INodeColumnTransform) null;
  }

  /// <summary>
  /// Создает виртуальную колонку с заданным направлением сортировки по
  /// указанному идентификатору. Если колонки с такми идентификатором в
  /// схеме нет - то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnInfo">Описание колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Очерёдность сортировки (-1 - не сортируется)</param>
  /// <returns>Виртуальная колонка</returns>
  private NodeColumn CreateColumn(
    Guid schemeGuid,
    NavigatorColumnScheme.ColumnInfo columnInfo,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    return new NodeColumn(schemeGuid, (object) columnInfo.Id, columnInfo.DataType, columnInfo.AttrType, columnInfo.Caption, sortOrder, sortIndex)
    {
      Priority = SchemeColumnPriority.Highest
    };
  }

  /// <summary>Определить индекс колонки</summary>
  /// <param name="columnId">Идентификатор колонки</param>
  /// <returns>Индекс колонки или -1</returns>
  public int IndexOf(object columnId)
  {
    for (int index = 0; index < NavigatorColumnScheme.Columns.Length; ++index)
    {
      if (NavigatorColumnScheme.Columns[index].Id.Equals(columnId))
        return index;
    }
    return -1;
  }

  /// <summary>Описание колонки</summary>
  internal class ColumnInfo
  {
    /// <summary>Идентификатор колонки</summary>
    private string id;
    /// <summary>Тип данных колонки</summary>
    private Type dataType;
    /// <summary>Тип атрибута колонки</summary>
    private FieldTypes attrType;
    /// <summary>Заголовок колонки</summary>
    private string caption;

    /// <summary>Создать экземпляр класса</summary>
    /// <param name="id">Идентификатор колонки</param>
    /// <param name="dataType">Тип данных колонки</param>
    /// <param name="attrType">Тип атрибута колонки</param>
    /// <param name="caption">Заголовок колонки</param>
    public ColumnInfo(string id, Type dataType, FieldTypes attrType, string caption)
    {
      this.id = id;
      this.dataType = dataType;
      this.attrType = attrType;
      this.caption = caption;
    }

    /// <summary>Идентификатор колонки</summary>
    public string Id => this.id;

    /// <summary>Тип данных колонки</summary>
    public Type DataType => this.dataType;

    /// <summary>Тип атрибута колонки</summary>
    public FieldTypes AttrType => this.attrType;

    /// <summary>Заголовок колонки</summary>
    public string Caption => this.caption;

    /// <summary>Сравнить два объекта</summary>
    /// <param name="obj">Объект для сравнения</param>
    /// <returns>true, если объекты равны</returns>
    public override bool Equals(object obj)
    {
      return !(obj is NavigatorColumnScheme.ColumnInfo columnInfo) ? base.Equals(obj) : this.id == columnInfo.id;
    }

    /// <summary>32-битный хэш-код</summary>
    /// <returns>32-битный хэш-код</returns>
    public override int GetHashCode() => this.id.GetHashCode();
  }
}
