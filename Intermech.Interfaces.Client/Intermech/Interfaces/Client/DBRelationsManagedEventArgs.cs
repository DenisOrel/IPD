// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBRelationsManagedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Список идентификаторов связей, с которыми произошло некоторое событие
/// </summary>
public class DBRelationsManagedEventArgs : DBRelationsEventArgs
{
  /// <summary>Допускается ли данное событие к обработке</summary>
  public bool AcceptEvent = true;
  /// <summary>Элемент управления, которому предназначено событие</summary>
  public object Control;
  /// <summary>
  /// Узел, который является "точкой отсчёта" при вставке в список новых узлов
  /// (ссылка на ITreeNode)
  /// </summary>
  public object Node;
  /// <summary>Куда добавляются вновь созданные узлы</summary>
  public NodesInsertPosition InsertPosition = NodesInsertPosition.After;

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="AnAcceptEvent">Допускается ли данное событие к обработке</param>
  public DBRelationsManagedEventArgs(string eventName, long relationID, bool AnAcceptEvent)
    : base(eventName, relationID)
  {
    this.AcceptEvent = AnAcceptEvent;
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationIDs">Список идентификаторов связей</param>
  /// <param name="AnAcceptEvent">Допускается ли данное событие к обработке</param>
  public DBRelationsManagedEventArgs(string eventName, IList<long> relationIDs, bool AnAcceptEvent)
    : base(eventName, relationIDs)
  {
    this.AcceptEvent = AnAcceptEvent;
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие.
  /// Связи должны быть добавлены в список в указанную позицию
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="AnAcceptEvent">Допускается ли данное событие к обработке</param>
  /// <param name="control">Элемент управления, которому предназначено событие</param>
  /// <param name="node">Узел, который является "точкой отсчёта" при вставке в список новых узлов (ссылка на ITreeNode)</param>
  /// <param name="insertPosition">Куда добавляются вновь созданные узлы</param>
  public DBRelationsManagedEventArgs(
    string eventName,
    long relationID,
    bool AnAcceptEvent,
    object control,
    object node,
    NodesInsertPosition insertPosition)
    : base(eventName, relationID)
  {
    this.AcceptEvent = AnAcceptEvent;
    this.Control = control;
    this.Node = node;
    this.InsertPosition = insertPosition;
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие.
  /// Связи должны быть добавлены в список в указанную позицию
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationIDs">Список идентификаторов связей</param>
  /// <param name="AnAcceptEvent">Допускается ли данное событие к обработке</param>
  /// <param name="control">Элемент управления, которому предназначено событие</param>
  /// <param name="node">Узел, который является "точкой отсчёта" при вставке в список новых узлов (ссылка на ITreeNode)</param>
  /// <param name="insertPosition">Куда добавляются вновь созданные узлы</param>
  public DBRelationsManagedEventArgs(
    string eventName,
    IList<long> relationIDs,
    bool AnAcceptEvent,
    object control,
    object node,
    NodesInsertPosition insertPosition)
    : base(eventName, relationIDs)
  {
    this.AcceptEvent = AnAcceptEvent;
    this.Control = control;
    this.Node = node;
    this.InsertPosition = insertPosition;
  }

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public override bool MergeWith(object obj) => false;
}
