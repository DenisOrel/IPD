// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBSubstitutesEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Список идентификаторов связей, с которыми произошло событие, связанное с допзаменами
/// </summary>
[Serializable]
public class DBSubstitutesEventArgs : DBRelationsEventArgs
{
  /// <summary>Идентификатор родительского объекта</summary>
  private long _projID;

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло событие, связанное с допзаменами
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationID">Идентификатор связи</param>
  public DBSubstitutesEventArgs(string eventName, long relationID)
    : this(eventName, (IList<long>) new long[1]
    {
      relationID
    })
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло событие, связанное с допзаменами
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationIDs">Список идентификаторов связей</param>
  public DBSubstitutesEventArgs(string eventName, IList<long> relationIDs)
    : base(eventName, relationIDs)
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло событие, связанное с допзаменами
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationIDs">Список идентификаторов связей</param>
  /// <param name="projID">Идентификатор родительского объекта</param>
  public DBSubstitutesEventArgs(string eventName, IList<long> relationIDs, long projID)
    : base(eventName, relationIDs)
  {
    this._projID = projID;
  }

  /// <summary>Идентификатор родительского объекта</summary>
  public long ProjID => this._projID;

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public override bool MergeWith(object obj)
  {
    return obj is DBSubstitutesEventArgs substitutesEventArgs && substitutesEventArgs._projID == this._projID && base.MergeWith(obj);
  }
}
