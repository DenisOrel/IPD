// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImEventSession
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImEventSession
{
  protected int _refCount;
  protected Guid _sessionGuid = Guid.Empty;
  protected List<ImEventBaseData> _eventDataList = new List<ImEventBaseData>();

  public ImEventSession(Guid sessionGuid) => this._sessionGuid = sessionGuid;

  public int RefCount
  {
    get => this._refCount;
    set => this._refCount = value;
  }

  public Guid SessionGuid => this._sessionGuid;

  public List<ImEventBaseData> EventDataList => this._eventDataList;
}
