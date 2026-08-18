// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Services.EventLoggerService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Services;

internal class EventLoggerService : LongLifeObject, IEventLoggerService
{
  public event HandlerEventDelegate HandlerEvent;

  public event HandlerExceptionDelegate HandlerException;

  public void AddMessage(Guid taskGuid, EventType type, string eventText)
  {
    HandlerEventDelegate handlerEvent = this.HandlerEvent;
    if (handlerEvent == null)
      return;
    handlerEvent(taskGuid, type, eventText);
  }

  public void AddException(Guid taskGuid, Exception e)
  {
    HandlerExceptionDelegate handlerException = this.HandlerException;
    if (handlerException == null)
      return;
    handlerException(taskGuid, e);
  }
}
