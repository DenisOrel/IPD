
// Type: Intermech.Navigator.ColumnSchemes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator;

/// <summary>Класс, управляющий коллекцией схем колонок</summary>
public class ColumnSchemes : IColumnSchemes
{
  /// <summary>Коллекция схем колонок</summary>
  private readonly ConcurrentDictionary<Guid, INodeColumnScheme> _schemes = new ConcurrentDictionary<Guid, INodeColumnScheme>();

  /// <summary>Зарегистрировать указанную схему колонок по её Guid</summary>
  /// <param name="schemeGuid">Guid регистрируемой схемы колонок</param>
  /// <param name="scheme">Схема колонок</param>
  void IColumnSchemes.Register(Guid schemeGuid, INodeColumnScheme scheme)
  {
    this.Validate(schemeGuid);
    this.Validate(scheme);
    if (this._schemes.ContainsKey(schemeGuid))
      return;
    this._schemes.TryAdd(schemeGuid, scheme);
  }

  /// <summary>
  /// Удалить указанную схему колонок из внутренней коллекции
  /// </summary>
  /// <param name="schemeGuid">Guid удаляемой схемы колонокs</param>
  void IColumnSchemes.Unregister(Guid schemeGuid)
  {
    this.Validate(schemeGuid);
    this._schemes.TryRemove(schemeGuid, out INodeColumnScheme _);
  }

  /// <summary>Отыскать схему колонок по её Guid</summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <returns>Найденная схема колонок или null</returns>
  public INodeColumnScheme this[Guid schemeGuid]
  {
    get
    {
      this.Validate(schemeGuid);
      return this.GetScheme(schemeGuid, false);
    }
  }

  /// <summary>
  /// Преобразовать указанный ID колонки указанной схемы в постоянное имя
  /// </summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <param name="columnID">ID колонки</param>
  /// <returns></returns>
  string IColumnSchemes.ColumnIDToPersistName(Guid schemeGuid, object columnID)
  {
    this.Validate(schemeGuid);
    this.Validate(columnID);
    return this.GetScheme(schemeGuid).ColumnIDToPersistName(columnID);
  }

  /// <summary>
  /// Преобразовать постоянное имя указанной схемы в ID колонки
  /// </summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <param name="persistName">Постоянное имя колонки</param>
  /// <returns></returns>
  object IColumnSchemes.PersistNameToColumnID(Guid schemeGuid, string persistName)
  {
    this.Validate(schemeGuid);
    this.Validate(persistName);
    return this.GetScheme(schemeGuid).PersistNameToColumnID(persistName);
  }

  /// <summary>Создать новую колонку в указанной схеме</summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <param name="columnID">ID колонки</param>
  /// <returns>Новая колонка в схеме</returns>
  NodeColumn IColumnSchemes.CreateColumn(Guid schemeGuid, object columnID)
  {
    this.Validate(schemeGuid);
    this.Validate(columnID);
    return this.GetScheme(schemeGuid).CreateColumn(schemeGuid, columnID);
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
  NodeColumn IColumnSchemes.CreateColumn(
    Guid schemeGuid,
    object columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    this.Validate(schemeGuid);
    this.Validate(columnID);
    return this.GetScheme(schemeGuid).CreateColumn(schemeGuid, columnID, sortOrder, sortIndex);
  }

  /// <summary>
  /// Найти интерфейс преобразователя значений указанной схемы для указанной колонки
  /// </summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <param name="columnID">ID колонки</param>
  /// <returns>Преобразователь значений или null</returns>
  INodeColumnTransform IColumnSchemes.GetDefaultTransform(Guid schemeGuid, object columnID)
  {
    this.Validate(schemeGuid);
    this.Validate(columnID);
    return this.GetScheme(schemeGuid).GetDefaultTransform(columnID);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void Validate(Guid schemeGuid)
  {
    if (schemeGuid == Guid.Empty)
      throw new ArgumentException("Column scheme guid cannot be empty!", nameof (schemeGuid));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void Validate(INodeColumnScheme scheme)
  {
    if (scheme == null)
      throw new ArgumentNullException(nameof (scheme), "Column scheme cannot be null!");
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void Validate(object columnID)
  {
    if (columnID == null)
      throw new ArgumentNullException(nameof (columnID), "Column id cannot be null!");
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void Validate(string persistName)
  {
    if (persistName == null)
      throw new ArgumentNullException(nameof (persistName), "Column persist name cannot be null!");
    if (persistName == string.Empty)
      throw new ArgumentException("Column persist name cannot be empty!", nameof (persistName));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private INodeColumnScheme GetScheme(Guid schemeGuid, bool throwException = true)
  {
    INodeColumnScheme scheme;
    if (!this._schemes.TryGetValue(schemeGuid, out scheme) & throwException)
      throw new ArgumentException("Column scheme with this guid is not registered!", nameof (schemeGuid));
    return scheme;
  }
}
