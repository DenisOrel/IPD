// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.FileProcessEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Tools.LaunchActions;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

[Serializable]
public class FileProcessEventArgs : EventArgs
{
  private bool isHandled;
  private int objectType;
  private long objectId;
  /// <summary>объект-связь</summary>
  private AttributableElements attributableElement;
  private int attributeId;
  private int valueIndex;
  private BlobInformation blobInformation;
  private LaunchType launchType;

  /// <summary>Если true, то уже обработан другими обработчиками</summary>
  public bool IsHandled
  {
    get => this.isHandled;
    set => this.isHandled = value;
  }

  /// <summary>Тип объекта</summary>
  public int ObjectType => this.objectType;

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectId => this.objectId;

  public AttributableElements AttributableElement => this.attributableElement;

  /// <summary>Идентификатор атрибута</summary>
  public int AttributeId => this.attributeId;

  /// <summary>Индекс в файловом атрибуте</summary>
  public int ValueIndex => this.valueIndex;

  /// <summary>BlobInformation значения файлового атрибута</summary>
  public BlobInformation BlobInformation => this.blobInformation;

  /// <summary>Тип запуска - просмотр или редактирование</summary>
  public LaunchType LaunchType => this.launchType;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId">Версия объекта</param>
  /// <param name="attributableElement">объект-связь</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="valueIndex">Индекс в файловом атрибуте</param>
  /// <param name="bi">BlobInformation значения файлового атрибута</param>
  /// <param name="launchType">Тип запуска - просмотр или редактирование</param>
  public FileProcessEventArgs(
    long objectId,
    AttributableElements attributableElement,
    int objectType,
    int attributeId,
    int valueIndex,
    BlobInformation bi,
    LaunchType launchType)
  {
    this.objectId = objectId;
    this.attributableElement = attributableElement;
    this.objectType = objectType;
    this.attributeId = attributeId;
    this.valueIndex = valueIndex;
    this.blobInformation = bi;
    this.launchType = launchType;
  }
}
