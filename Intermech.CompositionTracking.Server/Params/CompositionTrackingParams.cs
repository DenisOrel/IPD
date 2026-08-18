// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Params.CompositionTrackingParams
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.CompositionTracking.Server.Params;

internal class CompositionTrackingParams
{
  private readonly IDBObject _dbObject;

  public CompositionTrackingParams(IDBObject dbObject)
  {
    this._dbObject = dbObject != null ? dbObject : throw new ArgumentNullException(nameof (dbObject));
  }

  public IDBObject DbObject => this._dbObject;

  public IUserSession Session => this._dbObject.Session;
}
