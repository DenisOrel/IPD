// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IGridContextSearch
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Служба контекстного поиска в элементах управления Навигатора, содержащих список строк,
/// для вызова из модулей расширения (для программного поиска)
/// </summary>
public interface IGridContextSearch
{
  /// <summary>
  /// Отыскать колонки в гриде, которые содержит значения указанного атрибута
  /// (объекта, связи, т.п.)
  /// </summary>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  /// <returns>Колонки, содержащие значения указанного атрибута, либо null, если колонки не найдены</returns>
  NodeColumn[] FindColumns(int attrID);

  /// <summary>
  /// Отыскать колонки в гриде, которые содержат значения указанного атрибута
  /// (объекта, связи, т.п.)
  /// </summary>
  /// <param name="attrGuid">Глобальный идентификатор типа атрибута</param>
  /// <returns>Колонки, содержащие значения указанного атрибута, либо null, если колонки не найдены</returns>
  NodeColumn[] FindColumns(Guid attrGuid);

  /// <summary>Количество строк, загруженных в элемент управления</summary>
  long RowsCount { get; }

  /// <summary>
  /// Загружены ли все строки в элемент управления (True) или ещё нет
  /// </summary>
  bool Eof { get; }

  /// <summary>Загрузить очередной пакет с данными</summary>
  void FetchNext();

  /// <summary>Загрузить все оставшиеся пакеты с данными</summary>
  void FetchAll();

  /// <summary>
  /// Контейнер настроек для контекстного поиска в элементе управления Навигатора, содержащем список строк
  /// (для программного поиска)
  /// </summary>
  IGridContextSearchHolder Holder { get; set; }

  /// <summary>
  /// Выполнить поиск по указанным в Holder критериям. В зависимости от значения selectAllMode,
  /// поиск выделит в элементе сразу все найденные строки, либо отыщет и выделит следующую
  /// подходящую строку (поиск выполняется циклично)
  /// </summary>
  /// <param name="columns">Колонки, в которых следует выполнять поиск.
  /// Если не задано ни одной колонки, поиск будет выполняться во всех видимых колонках</param>
  /// <returns>true - найдена как минимум одна подходящая строка, false - ничего подходящего не найдено</returns>
  bool Search(params NodeColumn[] columns);
}
