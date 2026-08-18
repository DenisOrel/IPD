// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IColumnSchemes
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Интерфейс, управляющий коллекцией схем колонок</summary>
public interface IColumnSchemes
{
  /// <summary>Зарегистрировать указанную схему колонок по её Guid</summary>
  /// <param name="schemeGuid">Guid регистрируемой схемы колонок</param>
  /// <param name="scheme">Схема колонок</param>
  void Register(Guid schemeGuid, INodeColumnScheme scheme);

  /// <summary>
  /// Удалить указанную схему колонок из внутренней коллекции
  /// </summary>
  /// <param name="schemeGuid">Guid удаляемой схемы колонокs</param>
  void Unregister(Guid schemeGuid);

  /// <summary>Отыскать схему колонок по её Guid</summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <returns>Найденная схема колонок или null</returns>
  INodeColumnScheme this[Guid schemeGuid] { get; }

  /// <summary>
  /// Преобразовать указанный ID колонки указанной схемы в постоянное имя
  /// </summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <param name="columnID">ID колонки</param>
  /// <returns></returns>
  string ColumnIDToPersistName(Guid schemeGuid, object columnID);

  /// <summary>
  /// Преобразовать постоянное имя указанной схемы в ID колонки
  /// </summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <param name="persistName">Постоянное имя колонки</param>
  /// <returns></returns>
  object PersistNameToColumnID(Guid schemeGuid, string persistName);

  /// <summary>Создать новую колонку в указанной схеме</summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <param name="columnID">ID колонки</param>
  /// <returns>Новая колонка в схеме</returns>
  NodeColumn CreateColumn(Guid schemeGuid, object columnID);

  /// <summary>
  /// Создать новую колонку в указанной схеме, с учётом направления сортировки
  /// </summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <param name="columnID">ID колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <returns>Новая колонка в схеме</returns>
  /// <param name="sortIndex">Очерёдность сортировки (-1 - не сортируется)</param>
  NodeColumn CreateColumn(
    Guid schemeGuid,
    object columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex);

  /// <summary>
  /// Найти интерфейс преобразователя значений указанной схемы для указанной колонки
  /// </summary>
  /// <param name="schemeGuid">Guid схемы колонок</param>
  /// <param name="columnID">ID колонки</param>
  /// <returns>Преобразователь значений или null</returns>
  INodeColumnTransform GetDefaultTransform(Guid schemeGuid, object columnID);
}
