// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ExtendEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

public class ExtendEventArgs : EventArgs
{
  /// <summary>Идентификатор базового объекта</summary>
  private long _objectId;
  /// <summary>Тип базового объекта</summary>
  private int _objectType;
  /// <summary>
  /// Предпочтительный идентификатор блоба для для просмотра
  /// </summary>
  private long _preferedBlobId;
  /// <summary>Список идентификаторов объектов и блобов</summary>
  private List<FileBlobItem> _items;

  /// <summary>Контсруктор</summary>
  /// <param name="objectType">Тип базового объекта</param>
  /// <param name="objectId">Идентификатор базового объекта</param>
  /// <param name="items">Список идентификаторов объектов и блобов</param>
  public ExtendEventArgs(int objectType, long objectId, List<FileBlobItem> items)
  {
    this._objectId = objectId;
    this._objectType = objectType;
    this._items = items;
    this._preferedBlobId = -1L;
  }

  /// <summary>Тип базового объекта</summary>
  public int ObjectType => this._objectType;

  /// <summary>Идентификатор базового объекта</summary>
  public long ObjectID => this._objectId;

  /// <summary>Список идентификаторов объектаов и блобов</summary>
  public List<FileBlobItem> Items => this._items;

  /// <summary>Предпочтительный идентификатор блоба для просмотра</summary>
  public long PreferedBlobID
  {
    get => this._preferedBlobId;
    set => this._preferedBlobId = value;
  }
}
