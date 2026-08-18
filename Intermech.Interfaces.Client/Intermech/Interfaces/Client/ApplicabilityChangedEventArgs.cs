// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ApplicabilityChangedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

[Serializable]
public class ApplicabilityChangedEventArgs : NotificationEventArgs
{
  /// <summary>ID типа связи</summary>
  private int relationType;
  /// <summary>ID типа объекта</summary>
  private int objectType;
  /// <summary>ID типа объекта</summary>
  private int inObjectType;

  /// <summary>ID типа связи</summary>
  public int RelationType
  {
    get => this.relationType;
    set => this.relationType = value;
  }

  /// <summary>ID типа объекта</summary>
  public int ObjectType
  {
    get => this.objectType;
    set => this.objectType = value;
  }

  /// <summary>ID типа объекта</summary>
  public int InObjectType
  {
    get => this.inObjectType;
    set => this.inObjectType = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="eventName"> имя события (под этот тип аргументов ApplicabilityAdded, ApplicabilityRemoved)</param>
  /// <param name="_relationType">тип добавленной/удалённой связи</param>
  /// <param name="_objectType"> тип дочернего объекта</param>
  /// <param name="_inObjectType">тип родительского объекта</param>
  public ApplicabilityChangedEventArgs(
    string eventName,
    int _relationType,
    int _objectType,
    int _inObjectType)
    : base(eventName)
  {
    this.relationType = _relationType;
    this.objectType = _objectType;
    this.inObjectType = _inObjectType;
  }
}
