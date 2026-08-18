// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImEventObjData
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImEventObjData : ImEventBaseData
{
  protected IDBObject _object;

  public IDBObject Object => this._object;

  public ImEventObjData(IDBObject aObject, ImEventType eventType)
    : this(aObject, (EventArgs) null, eventType)
  {
  }

  public ImEventObjData(IDBObject dbObject, EventArgs eventArgs, ImEventType eventType)
    : base(eventArgs, eventType)
  {
    this._object = dbObject;
  }
}
