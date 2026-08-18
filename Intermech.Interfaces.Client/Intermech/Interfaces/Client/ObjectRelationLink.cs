// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ObjectRelationLink
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Структура для определения идентификатора объекта и идентификатора типа связи
/// при создании нового экземпляра объекта с заданными связями
/// </summary>
[Serializable]
public class ObjectRelationLink : ICloneable
{
  /// <summary>
  /// идентификатор объекта, с которым необходимо создать связь
  /// </summary>
  public long ObjectID { get; private set; }

  /// <summary>идентификатор типа связи, которую нужно создать</summary>
  public int RelationTypeID { get; private set; }

  /// <summary>идентификатор связи</summary>
  public long LinkID { get; set; }

  /// <summary>
  /// Список атрибутов и значений, которые нужно установить новой связи
  /// </summary>
  public Dictionary<int, object> Attributes { get; set; }

  public ObjectRelationLink(long aObjectID, int aRelationTypeID)
    : this(aObjectID, aRelationTypeID, 0L)
  {
  }

  public ObjectRelationLink(long aObjectID, int aRelationTypeID, long linkID)
  {
    this.ObjectID = aObjectID;
    this.RelationTypeID = aRelationTypeID;
    this.LinkID = linkID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public object Clone() => this.MemberwiseClone();
}
