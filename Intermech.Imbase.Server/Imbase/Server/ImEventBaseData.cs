// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImEventBaseData
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImEventBaseData
{
  protected EventArgs _eventArg;
  protected ImEventType _eventType;

  public EventArgs EventArg => this._eventArg;

  public ImEventType EventType => this._eventType;

  public ImEventBaseData(ImEventType eventType)
    : this((EventArgs) null, eventType)
  {
  }

  public ImEventBaseData(EventArgs eventArg, ImEventType eventType)
  {
    this._eventArg = eventArg;
    this._eventType = eventType;
  }
}
