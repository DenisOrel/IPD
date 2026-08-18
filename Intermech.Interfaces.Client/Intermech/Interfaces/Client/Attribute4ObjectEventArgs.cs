// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.Attribute4ObjectEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

[Serializable]
public class Attribute4ObjectEventArgs : DBObjectsEventArgs
{
  private int attributeID;
  private int attributeType;

  public int AttributeID => this.attributeID;

  /// <summary>0 - тип не указан</summary>
  public int AttributeType => this.attributeType;

  /// <summary>
  /// Подготовить аргументы события для объекта, с атрибутом attributeID которого произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="objectID">Идентификатор объекта</param>
  public Attribute4ObjectEventArgs(string eventName, int attributeID, long objectID)
    : this(eventName, attributeID, 0, objectID)
  {
  }

  public Attribute4ObjectEventArgs(
    string eventName,
    int attributeID,
    int attributeType,
    long objectID)
    : this(eventName, attributeID, attributeType, objectID, -1)
  {
  }

  /// <summary>
  /// Подготовить аргументы события для объекта, с атрибутом attributeID которого произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="objectID">Идентификатор объекта</param>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  public Attribute4ObjectEventArgs(
    string eventName,
    int attributeID,
    int attributeType,
    long objectID,
    int objectTypeID)
    : base(eventName, objectID, objectTypeID)
  {
    this.attributeID = attributeID;
    this.attributeType = attributeType;
  }
}
