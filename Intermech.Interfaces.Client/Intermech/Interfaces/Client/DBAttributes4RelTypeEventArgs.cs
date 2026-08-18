// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBAttributes4RelTypeEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Список идентификаторов атрибутов, с которыми произошло некоторое событие, для типов связей
/// </summary>
[Serializable]
public class DBAttributes4RelTypeEventArgs : DBAttributesEventArgs
{
  /// <summary>Тип связи, с которой произошло событие</summary>
  public int RelationType = -1;

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="AnRelationType">Тип связи, с которой произошло событие</param>
  public DBAttributes4RelTypeEventArgs(string eventName, int attributeID, int AnRelationType)
    : base(eventName, attributeID)
  {
    this.RelationType = AnRelationType;
  }

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  /// <param name="AnRelationType">Тип связи, с которой произошло событие</param>
  public DBAttributes4RelTypeEventArgs(
    string eventName,
    int attributeID,
    bool firePrePostEvents,
    int AnRelationType)
    : base(eventName, attributeID, firePrePostEvents)
  {
    this.RelationType = AnRelationType;
  }

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="attributeIDs">Идентификаторы атрибутов</param>
  /// <param name="AnRelationType">Тип связи, с которой произошло событие</param>
  public DBAttributes4RelTypeEventArgs(
    string eventName,
    IList<int> attributeIDs,
    int AnRelationType)
    : base(eventName, attributeIDs)
  {
    this.RelationType = AnRelationType;
  }

  /// <summary>
  /// Подготовить список идентификаторов объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="attributeIDs">Идентификаторы атрибутов</param>
  /// <param name="firePrePostEvents">"Дёргать" дополнительные события "Перед основным событием" и "После основного события" у сервиса событий</param>
  /// <param name="AnRelationType">Тип связи, с которой произошло событие</param>
  public DBAttributes4RelTypeEventArgs(
    string eventName,
    IList<int> attributeIDs,
    bool firePrePostEvents,
    int AnRelationType)
    : base(eventName, attributeIDs, firePrePostEvents)
  {
    this.RelationType = AnRelationType;
  }

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public override bool MergeWith(object obj)
  {
    return obj is DBAttributes4RelTypeEventArgs relTypeEventArgs && relTypeEventArgs.RelationType == this.RelationType && base.MergeWith(obj);
  }
}
