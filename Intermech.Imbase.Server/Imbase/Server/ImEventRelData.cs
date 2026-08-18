// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImEventRelData
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImEventRelData : ImEventBaseData
{
  protected IDBRelation _relation;

  public ImEventRelData(IDBRelation relation, ImEventType eventType)
    : this(relation, (EventArgs) null, eventType)
  {
  }

  public ImEventRelData(IDBRelation relation, EventArgs eventArgs, ImEventType eventType)
    : base(eventArgs, eventType)
  {
    this._relation = relation;
  }

  public IDBRelation Relation => this._relation;
}
