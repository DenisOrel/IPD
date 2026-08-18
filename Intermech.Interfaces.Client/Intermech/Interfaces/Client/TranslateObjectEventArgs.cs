// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.TranslateObjectEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// 
/// </summary>
public class TranslateObjectEventArgs
{
  private long _objectId;
  private IUserSession _session;
  private int _typeId;
  private long _newObjectId;

  /// <summary>
  /// 
  /// </summary>
  public long NewObjectId
  {
    get => this._newObjectId;
    set => this._newObjectId = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public long ObjectId => this._objectId;

  /// <summary>
  /// 
  /// </summary>
  public IUserSession Session
  {
    get => this._session;
    set => this._session = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public int TypeId => this._typeId;

  /// <summary>Конструктор.</summary>
  /// <param name="session"></param>
  /// <param name="objectId"></param>
  /// <param name="objectType"></param>
  public TranslateObjectEventArgs(IUserSession session, long objectId, int objectType)
  {
    this._session = session;
    this._objectId = objectId;
    this._typeId = objectType;
    this._newObjectId = -1L;
  }
}
