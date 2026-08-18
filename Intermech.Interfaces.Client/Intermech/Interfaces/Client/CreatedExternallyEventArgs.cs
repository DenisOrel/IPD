// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CreatedExternallyEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Список идентификаторов версий объектов, с которыми произошло некоторое событие (интеграторы на это событие не реагируют)
/// </summary>
[Serializable]
public sealed class CreatedExternallyEventArgs : DBObjectsEventArgs
{
  /// <summary>
  /// Список идентификаторов версий объектов, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName"></param>
  /// <param name="objectId"></param>
  public CreatedExternallyEventArgs(string eventName, long objectId)
    : base(eventName, objectId)
  {
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и списком идентификаторов версий объектов
  /// </summary>
  /// <param name="eventName">Имя события обновления</param>
  /// <param name="objectIDs">
  /// Список идентификаторов версий объектов. Может быть любым объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  public CreatedExternallyEventArgs(string eventName, IList<long> objectIDs)
    : base(eventName, objectIDs)
  {
  }

  /// <summary>
  /// Создает новый экземпляр объекта с указанными именем события обновления и списком идентификаторов версий объектов
  /// </summary>
  /// <param name="eventName">Имя события обновления</param>
  /// <param name="objectIDs">
  /// Список идентификаторов версий объектов. Может быть любым объектов,
  /// поддерживающим интерфейс IList и содержащим значения типа Int64.
  /// </param>
  /// <param name="objectTypeIDs">Список идентификаторов типов созданных объектов (если типы неизвестен, можно подсунуть Intermech.Consts.UnknownObjectTypeId)</param>
  public CreatedExternallyEventArgs(
    string eventName,
    IList<long> objectIDs,
    IList<int> objectTypeIDs)
    : base(eventName, objectIDs, objectTypeIDs)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool MergeWith(object obj) => base.MergeWith(obj);
}
