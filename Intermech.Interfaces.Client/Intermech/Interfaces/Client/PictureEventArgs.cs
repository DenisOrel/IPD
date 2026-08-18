// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.PictureEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// 
/// </summary>
public class PictureEventArgs : EventArgs
{
  private object _picture;
  private long _objectId;
  private int _sessionId;

  /// <summary>
  /// 
  /// </summary>
  public long ObjectId => this._objectId;

  /// <summary>
  /// 
  /// </summary>
  public object Picture => this._picture;

  /// <summary>
  /// 
  /// </summary>
  public int Session => this._sessionId;

  /// <summary>Конструктор.</summary>
  /// <param name="objectId"></param>
  /// <param name="sessionId"></param>
  /// <param name="picture"></param>
  public PictureEventArgs(long objectId, int sessionId, object picture)
  {
    this._sessionId = sessionId;
    this._objectId = objectId;
    this._picture = picture;
  }
}
